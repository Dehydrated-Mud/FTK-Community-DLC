using FTKAPI.APIs.BattleAPI;
using FTKAPI.Objects;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.CharacterSkills
{
    internal class ThrillKill : FTKAPI_CharacterSkill
    {
        public ThrillKill() 
        {
            Trigger = TriggerType.KillShot;
            Name = new("Thrill of the Kill");
            Description = new("Bloodlust is as thrilling as it is deadly. On scoring a killshot, the character can attack again immediately.");
        }

        public override void Skill(CharacterOverworld cow, TriggerType trig, AttackAttempt _atk)
        {
            switch (trig)
            {
                case TriggerType.KillShot:
                    if (_atk.m_DamagedDummy is EnemyDummy && !_atk.m_DamagedDummy.Protected)
                    {
                        cow.m_CurrentDummy.RPCAllSelf("AddProfToDummy",
                            new object[3] { new FTK_proficiencyTable.ID[] { FTK_proficiencyTable.ID.musicRush }, true, true }
                        );
                    }
                    break;
            }
        }
    }
}
