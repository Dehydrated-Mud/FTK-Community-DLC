using FTKAPI.Objects;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Logger = FTKAPI.Utils.Logger;

namespace CommunityDLC.Objects.CharacterSkills
{
    public class DivineIntervention : FTKAPI_CharacterSkill
    {
        public DivineIntervention() 
        {
            Trigger = TriggerType.KillShot | TriggerType.RespondToHit | TriggerType.SpecialAttackAnim;
            Name = new CustomLocalizedString("Passive Skill: Divine Intervention");
            Description = new CustomLocalizedString("When scoring a killing blow, the lowest health ally is given protection.");
            SkillInfo = FTK_characterSkill.ID.Discipline;
        }

        public override void Skill(CharacterOverworld _player, TriggerType _trig, AttackAttempt _atk)
        {
            switch (_trig)
            {
                case TriggerType.KillShot:
                    
                    if(!(_atk.m_DamagedDummy.Protected || _atk.m_DamagedDummy.Shielded))
                    {
                        Logger.LogWarning("We're getting a kill shot! Setting proc to true");
                        proc = true;
                    }
                    break;
            }
        }
        public override void Skill(CharacterOverworld _player, TriggerType _trig)
        {
            switch (_trig) {
                case TriggerType.RespondToHit:
                    Logger.LogWarning("We are responding to a hit! proc is: " + proc);
                    if(proc)
                    {
                        List<CharacterDummy> otherCombatPlayerMembers = EncounterSession.Instance.GetOtherCombatPlayerMembers(_player.m_CurrentDummy);
                        if (otherCombatPlayerMembers != null)
                        {
                            CharacterDummy leastHealth = otherCombatPlayerMembers.OrderBy(p => p.m_CharacterOverworld.m_CharacterStats.m_HealthCurrent).First();
                            if (leastHealth != null)
                            {
                                leastHealth.SpawnHudTextRPC("Divine Protection", string.Empty);
                                leastHealth.AddProfToDummy(new FTK_proficiencyTable.ID[] { FTK_proficiencyTable.ID.enProtectSelf }, true, false);
                                leastHealth.PlayCharacterAbilityEventRPC(FTK_characterSkill.ID.None);
                            }
                        }
                        else
                        {
                            _player.m_CurrentDummy.AddProfToDummy(new FTK_proficiencyTable.ID[] { FTK_proficiencyTable.ID.enProtectSelf }, true, false);
                        }
                        proc = false;
                    }
                    break;
            }
        }
    }
}
