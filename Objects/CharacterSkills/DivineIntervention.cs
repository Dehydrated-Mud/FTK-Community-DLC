using FTKAPI.Managers;
using FTKAPI.Objects;
using FTKAPI.APIs.BattleAPI;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityDLC.Objects.CustomSkills;
using Logger = FTKAPI.Utils.Logger;
using CommunityDLC.PhotonHooks;
using HarmonyLib;
using UnityEngine.UI;
using System.Runtime.Remoting.Lifetime;

namespace CommunityDLC.Objects.CharacterSkills
{
    public class DivineIntervention : FTKAPI_CharacterSkill
    {
        internal SkillContainer skillContainer;
        private CharacterDummy protectedDummy = null;
        public DivineIntervention(SkillContainer _container) 
        {
            skillContainer = _container;
            int customSkill = SkillManager.AddSkill(new DivineInterventionInfo());
            Trigger = TriggerType.KillShot | TriggerType.RespondToHit | TriggerType.SpecialAttackAnim ;
            Name = new CustomLocalizedString("Passive Skill: Divine Intervention");
            Description = new CustomLocalizedString("When scoring a killing blow, the lowest health ally is given protection.");
            SkillInfo = (FTK_characterSkill.ID)customSkill;
        }

        public override void Skill(CharacterOverworld _player, TriggerType _trig, AttackAttempt _atk)
        {
            switch (_trig)
            {
                case TriggerType.KillShot:
                    object[] results = Proc(_player, _trig, _atk);
                    if ((bool)results[0])
                    {
                        protectedDummy = (CharacterDummy)results[1];
                        Logger.LogWarning("We're getting a kill shot! Setting proc to true");
                        skillContainer.SyncDivine(true);
                    }
                    break;
            }
        }
        public override void Skill(CharacterOverworld _player, TriggerType _trig)
        {
            switch (_trig) {
                case TriggerType.RespondToHit:
                    CustomCharacterStatsDLC stats = _player.gameObject.GetComponent<CustomCharacterStatsDLC>();
                    Logger.LogWarning("We are responding to a hit! proc is: " + proc);
                    if (proc)
                    {
                        if (protectedDummy != null) 
                        {
                            if (protectedDummy != _player.GetCurrentDummy())
                            {
                                protectedDummy.AddProfToDummy(new FTK_proficiencyTable.ID[] { FTK_proficiencyTable.ID.enProtectSelf }, true, true);
                                protectedDummy.PlayCharacterAbilityEventRPC(FTK_characterSkill.ID.None);
                            }
                            else
                            {
                                BattleAPI.Instance.AddProfToAttacker(FTK_proficiencyTable.ID.enProtectSelf);
                            }
                        }
                        proc = false;
                    }
                    break;
            }
        }

        private object[] Proc(CharacterOverworld _player, TriggerType _trig, AttackAttempt _atk)
        {
            CustomCharacterStatsDLC stats = _player.gameObject.GetComponent<CustomCharacterStatsDLC>();
            List<CharacterDummy> allies = EncounterSession.Instance.GetOtherCombatPlayerMembers(_player.m_CurrentDummy);
            float threshold = stats.DIThreshold;
            if (stats != null)
            {
                threshold = Math.Min(stats.DIThreshold, 1f);
            }
            bool isProtected = (_atk.m_DamagedDummy.Protected || _atk.m_DamagedDummy.Shielded);
            bool others = allies.Count > 0;
            bool self = stats != null && stats.DISelf;
            bool shouldProc = false;
            CharacterDummy toProtect = null;
            if(!isProtected && (others || self))
            {
                if (self)
                {
                    allies = allies.AddItem(_atk.m_AttackingDummy).ToList();
                    Logger.LogWarning(DLCUtils.GetDummyHealthPercent(_atk.m_AttackingDummy));
                }

                if (allies.Count > 0)
                {
                    Logger.LogWarning(allies.Count);
                    int minHealth = allies[0].GetCurrentHealth();
                    float minRatio = DLCUtils.GetDummyHealthPercent(allies[0]);
                    CharacterDummy leastRatio = allies[0];
                    CharacterDummy leastHealth = allies[0];
                    foreach (CharacterDummy dummy in allies)
                    {
                        if (dummy.GetCurrentHealth() < minHealth)
                        {
                            minHealth = dummy.GetCurrentHealth();
                            leastHealth = dummy;
                        }

                        if (DLCUtils.GetDummyHealthPercent(dummy) < minRatio)
                        {
                            minRatio = DLCUtils.GetDummyHealthPercent(dummy);
                            leastRatio = dummy;
                        }
                    }
                    if (DLCUtils.GetDummyHealthPercent(leastHealth) <= threshold)
                    {
                        shouldProc = true;
                        toProtect = leastHealth;
                    }
                    else if (DLCUtils.GetDummyHealthPercent(leastRatio) <= threshold)
                    {
                        shouldProc = true;
                        toProtect = leastRatio;
                    }
                }
            }
            return new object[2] { shouldProc, toProtect };
        }
    }
}
