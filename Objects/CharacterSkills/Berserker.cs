using FTKAPI.APIs.BattleAPI;
using FTKAPI.Managers;
using FTKAPI.Objects;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.CharacterSkills
{
    internal class Berserker : FTKAPI_CharacterSkill
    {
        Dictionary<FTKPlayerID, bool> m_Proc = new();
        public Berserker()
        {
            Trigger = TriggerType.TakeHeavyDamage;
            Name = new("Passive Skill: Berserker");
            Description = new("After taking heavy damage, the hero flies into a rage, significantly boosting damage.");
            SkillInfo = (FTK_characterSkill.ID)SkillManager.AddSkill(new CustomSkill(FTK_characterSkill.ID.Glory)
            {
                HudDisplay = new("Berserker"),
                ID = "DLCBerserker"
            });
        }

        public override void Skill(CharacterOverworld _cow, Query query)
        {
            switch(query)
            {
                case Query.StartCombatTurn:
                    if (m_Proc.ContainsKey(_cow.m_FTKPlayerID))
                    {
                        if (m_Proc[_cow.m_FTKPlayerID])
                        {
                            _cow.GetCurrentDummy().RPCAllSelf("AddProfToDummy", new object[3] { new FTK_proficiencyTable.ID[] { FTK_proficiencyTable.ID.orbAttackUp }, true, true });
                            _cow.GetCurrentDummy().PlayCharacterAbilityEvent(SkillInfo);
                            m_Proc[_cow.m_FTKPlayerID] = false;
                        }
                    }
                    break;
            }
        }

        public override void Skill(CharacterOverworld cow, TriggerType trig, AttackAttempt atk)
        {
            switch(trig)
            {
                case TriggerType.TakeHeavyDamage:
                    m_Proc[cow.m_FTKPlayerID] = true; //Sync this?
                    break;
            }
        }
    }
}
