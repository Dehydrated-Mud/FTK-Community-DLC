using FTKAPI.APIs.BattleAPI;
using FTKAPI.Objects;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.CharacterSkills
{
    internal class CalledRush : FTKAPI_CharacterSkill
    {
        public CalledRush() 
        {
            Trigger = TriggerType.CalledShot;
            Name = new("Lightning Hands");
            Description = new("Legolas ain't sh**. After a called shot proc, the character is rushed to the front of the battle queue.");
        }

        public override void Skill(CharacterOverworld cow, TriggerType trig)
        {
            switch (trig)
            {
                case TriggerType.CalledShot:
                    cow.m_CurrentDummy.RPCAllSelf("AddProfToDummy", 
                        new object[3] { new FTK_proficiencyTable.ID[] { FTK_proficiencyTable.ID.musicRush }, true, true }
                        );
                    break;
            }
        }
    }
}
