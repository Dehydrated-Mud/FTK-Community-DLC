using FTKAPI.APIs.BattleAPI;
using FTKAPI.Objects;
using FullInspector;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.CharacterSkills
{
    internal class BluntForceTrauma : FTKAPI_CharacterSkill
    {
        CharacterDummy victim;
        public BluntForceTrauma() 
        {
            Trigger = TriggerType.PerfectCombatRoll | TriggerType.RespondToHit | TriggerType.AnyLandedAttack;
            Name = new("Passive Skill: Blunt Force Trauma");
            Description = new("When using a blunt weapon, a perfect roll will stun the enemy target.");
        }

        public override void Skill(CharacterOverworld cow, TriggerType trig, AttackAttempt atk)
        {
            switch(trig)
            {
                case TriggerType.AnyLandedAttack:
                    proc &= (atk.m_DamagedDummy is EnemyDummy && BattleHelpers.GetWeapon(cow).m_WeaponType == Weapon.WeaponType.blunt);
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
                        FTK_proficiencyTable.ID[] prof = { FTK_proficiencyTable.ID.bluntStun };
                        victim.AddProfToDummy(prof, true, true);
                        proc = false;
                    }
                    break;
            }
        }

    }
}
