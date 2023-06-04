using FTKAPI.APIs.BattleAPI;
using FTKAPI.Objects;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static FTKAPI.Objects.FTKAPI_CharacterSkill;

namespace CommunityDLC.Objects.CharacterSkills
{
    public class DirtyTactics : FTKAPI_CharacterSkill
    {
        CharacterDummy victim;
        public DirtyTactics() 
        { 
            Trigger = TriggerType.PerfectCombatRoll | TriggerType.RespondToHit | TriggerType.AnyLandedAttack;
            Name = new ("Passive Skill: Dirty Tactics");
            Description = new ("Sand is a weapon, too. Perfect combat rolls inflict shock.");
        }

        public override void Skill(CharacterOverworld cow, TriggerType trig, AttackAttempt atk)
        {
            switch (trig)
            {
                case TriggerType.AnyLandedAttack:
                    proc &= (atk.m_DamagedDummy is EnemyDummy);
                    victim = atk.m_DamagedDummy;
                    break;
            }
        }

        public override void Skill(CharacterOverworld _cow, TriggerType _trig)
        {
            switch (_trig)
            {
                case TriggerType.PerfectCombatRoll:
                    proc = true;
                    break;
                case TriggerType.RespondToHit:
                    if (proc && victim != null)
                    {
                        FTK_proficiencyTable.ID[] prof = { FTK_proficiencyTable.ID.polearmLightningReg };
                        victim.AddProfToDummy(prof, true, true);
                    }
                    proc = false;
                    break;
            }
        }
    }
}

