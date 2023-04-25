using FTKAPI.Managers;
using FTKAPI.Objects;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityDLC.Objects.CustomSkills;
using Logger = FTKAPI.Utils.Logger;
using CommunityDLC.PhotonHooks;
using FTKAPI.APIs.BattleAPI;

namespace CommunityDLC.Objects.CharacterSkills
{
    public class Discipline : FTKAPI_CharacterSkill
    {
        private bool IsProtected = true;
        private bool IsCrit = false;
        public Discipline()
        {
            Trigger = TriggerType.KillShot | TriggerType.Critical | TriggerType.AnyLandedAttack | TriggerType.RespondToHit | TriggerType.SpecialAttackAnim;
            Name = new CustomLocalizedString("Passive Skill: Discipline");
            Description = new CustomLocalizedString("Restore focus to allies when scoring a killing blow.");
            SkillInfo = FTK_characterSkill.ID.Discipline;
        }
        public override void Skill(CharacterOverworld _cow, Query query)
        {
            switch (query)
            {
                case Query.StartCombatTurn:
                    IsProtected = true;
                    IsCrit = false;
                    Logger.LogWarning($"Setting protected to {IsProtected} and crit to {IsCrit}");  
                    break;
            }
        }
        public override void Skill(CharacterOverworld _player, TriggerType _trig, AttackAttempt _atk)
        {
            switch (_trig)
            {

                case TriggerType.AnyLandedAttack:
                    if (!_atk.m_DamagedDummy.m_CharacterOverworld)
                    {
                        if (_atk.m_DamagedDummy.Protected || _atk.m_DamagedDummy.Shielded)
                        {
                            IsProtected = true;
                        }
                        else
                        {
                            IsProtected = false;
                        }
                        Logger.LogWarning($"Responding to attack, protect is {IsProtected} and crit is {IsCrit}");
                        if (IsCrit && !IsProtected && FocusCondition(_player))
                        {
                            SkillContainer.Instance.SyncDiscipline(true);
                        }
                    }
                    break;
                case TriggerType.KillShot:
                    if (FocusCondition(_player) && !IsProtected && !_atk.m_DamagedDummy.m_CharacterOverworld)
                    {
                        SkillContainer.Instance.SyncDiscipline(true);
                    }
                    break;
            }
        }
        public override void Skill(CharacterOverworld _player, TriggerType _trig)
        {
            switch (_trig) {
                case TriggerType.Critical:
                    IsCrit = true;
                    Logger.LogWarning($"Received crit flag, crit now {IsCrit}");
                    break;
                case TriggerType.RespondToHit:
                    Logger.LogWarning("We are responding to a hit! proc is: " + proc);
                    if (proc)
                    {
                        FocusCondition(_player, addfocus: true);
                        proc = false;
                    }
                    break;
            }
        }

        private bool FocusCondition(CharacterOverworld _player, bool addfocus = false) //Dual purpose
        {
            CustomCharacterStatsDLC customStats = _player.gameObject.GetComponent<CustomCharacterStatsDLC>();
            if (!customStats)
            {
                Logger.LogError("No customStats object, exiting discipline");
                return false;
            }
            int threshold = customStats.m_DisciplineFocus;
            List<CharacterDummy> allies = EncounterSession.Instance.GetOtherCombatPlayerMembers(_player.m_CurrentDummy);
            foreach (CharacterDummy ally in allies)
            {
                CharacterStats stats = ally.m_CharacterOverworld.m_CharacterStats;
                if(stats.m_FocusPoints <= threshold && stats.m_FocusPoints < stats.MaxFocus)
                {
                    if (addfocus)
                    {
                        stats.UpdateFocusPoints(1);
                        stats.m_CharacterOverworld.PlayCharacterAbilityEventRPC(FTK_characterSkill.ID.None);
                        stats.m_CharacterOverworld.SpawnHudTextRPC("+1 Focus");
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
