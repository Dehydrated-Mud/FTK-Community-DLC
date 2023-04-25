using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityDLC.PhotonHooks;
using FTKAPI.Objects.SkillHooks;
using GridEditor;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using CommunityDLC.UIElements;
using Logger = FTKAPI.Utils.Logger;

namespace CommunityDLC.Objects.CharacterSkills
{
    public class MigratedSkills : BaseModule
    {
        public override void Initialize()
        {
            Unload();
            On.CharacterSkills.Justice += JusticeHook;
            On.CharacterSkills.Refocus += RefocusHook;
            /*IL.CharacterSkills.Discipline += DisciplineHook;
            IL.CharacterDummy.RespondToHit += RespondToHitHook;*/
        }

        public override void Unload()
        {
            On.CharacterSkills.Justice -= JusticeHook;
            On.CharacterSkills.Refocus -= RefocusHook;
            /*IL.CharacterSkills.Discipline -= DisciplineHook;
            IL.CharacterDummy.RespondToHit -= RespondToHitHook;*/
        }

        private bool JusticeHook(On.CharacterSkills.orig_Justice _orig, CharacterOverworld _player, FTK_proficiencyTable.ID _attemptedProf)
        {
            return Justice(_player, _attemptedProf);
        }
        private bool RefocusHook(On.CharacterSkills.orig_Refocus _orig, CharacterOverworld _player)
        {
            return Refocus(_player);
        }

        /*private void DisciplineHook(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            c.GotoNext(
                x => x.MatchLdloc(1),
                x => x.MatchLdfld<CharacterDummy>("m_CharacterOverworld"),
                x => x.MatchLdfld<CharacterOverworld>("m_CharacterStats"), // This characterstats object does not belong to Monk!
                x => x.MatchLdfld<CharacterStats>("m_FocusPoints")
                );
            c.Index += 3;
            c.Remove();
            c.EmitDelegate(DisciplineHelper);
        }

        private void RespondToHitHook(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            c.GotoNext(
                x => x.MatchLdfld<CharacterDummy>("m_CharacterOverworld"),
                x => x.MatchLdfld<CharacterOverworld>("m_CharacterStats"), // This characterstats object does not belong to Monk!
                x => x.MatchLdfld<CharacterStats>("m_FocusPoints")
                );
            c.Index += 2;
            c.Remove();
            c.EmitDelegate(DisciplineHelper);
        }*/
        public static bool Justice(CharacterOverworld _player, FTK_proficiencyTable.ID _attemptedProf)
        {
            float justice = 0;
            CustomCharacterStatsDLC customStats = _player.gameObject.GetComponent<CustomCharacterStatsDLC>(); 
            if (customStats != null)
            {
                justice = customStats.m_JusticeChance;
            }
            if (!_player.m_CharacterStats.m_CharacterSkills.m_Justice)
            {
                return false;
            }
            if (_player.m_CurrentDummy.m_SpecialAttack != 0)
            {
                return false;
            }
            if (_player.m_CurrentDummy.m_EventListener == null)
            {
                return false;
            }
            if (FTK_weaponStats2DB.GetDB().GetEntry(_player.m_WeaponID).m_ObjectSlot != FTK_itembase.ObjectSlot.twoHands)
            {
                return false;
            }
            Weapon.WeaponType bestWeaponType = _player.m_CurrentDummy.m_EventListener.GetBestWeaponType(_attemptedProf);
            if (bestWeaponType != Weapon.WeaponType.axe && bestWeaponType != Weapon.WeaponType.blunt && bestWeaponType != 0 && bestWeaponType != Weapon.WeaponType.spear)
            {
                return false;
            }
            if (_player.m_CurrentDummy.Shocked)
            {
                return false;
            }
            if (_attemptedProf != FTK_proficiencyTable.ID.None)
            {
                FTK_proficiencyTable fTK_proficiencyTable = FTK_proficiencyTableDB.Get(_attemptedProf);
                if (fTK_proficiencyTable.m_TargetFriendly || fTK_proficiencyTable.m_Target != 0)
                {
                    return false;
                }
            }
            CharacterDummy.TargetType targetType = CharacterDummy.TargetType.Splash;
            CharacterDummy currentEnemy = EncounterSession.Instance.GetCurrentEnemy();
            if (EncounterSession.Instance.GetAoeTargets(currentEnemy, targetType).Count > 0)
            {
                float num = 0.15f + justice;
                num += 0.03f * (float)_player.m_CharacterStats.SpentFocus;
                if (UnityEngine.Random.value < num || FTKUI.Instance.m_PlayerSlots.m_CheatAttack == SlotControl.AttackCheatType.TriggerAbility)
                {
                    _player.m_CurrentDummy.m_SpecialAttack = CharacterDummy.SpecialAttack.Justice;
                    return true;
                }
            }
            return false;
        }

        public static bool Refocus(CharacterOverworld _player)
        {
            float refocus = 0;
            CustomCharacterStatsDLC customStats = _player.gameObject.GetComponent<CustomCharacterStatsDLC>();
            if (customStats != null)
            {
                refocus = customStats.m_RefocusChance;
            }
            if (!_player.m_CharacterStats.m_CharacterSkills.m_Refocus)
            {
                return false;
            }
            if (_player.m_WaitForRespawn || _player.m_CharacterStats.IsPoisoned)
            {
                return false;
            }
            if (_player.m_CharacterStats.m_FocusPoints >= _player.m_CharacterStats.MaxFocus)
            {
                return false;
            }
            bool flag = false;
            if (_player.IsInDungeon())
            {
                flag = true;
            }
            else if (!GameLogic.Instance.m_WeatherManager.IsItRaining())
            {
                flag = true;
            }
            if (flag)
            {
                float num = _player.m_CharacterStats.Fortitude * (0.65f + refocus);
                if (_player.IsInDungeon())
                {
                    MiniHexDungeon miniHexDungeon = (MiniHexDungeon)_player.GetPOI();
                    num /= (float)miniHexDungeon.GetAlivePlayersInside().Count;
                }
                if (UnityEngine.Random.value < num)
                {
                    int focus = 1;
                    _player.m_CharacterStats.UpdateFocusPoints(focus);
                    if ((bool)_player.GetCurrentDummy())
                    {
                        _player.GetCurrentDummy().PlayCharacterAbilityEvent(FTK_characterSkill.ID.Refocus);
                    }
                    else
                    {
                        _player.PlayCharacterAbilityEvent(FTK_characterSkill.ID.Refocus);
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
