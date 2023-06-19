using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logger = FTKAPI.Utils.Logger;
using FTKAPI.Objects;

namespace CommunityDLC.Objects.CharacterSkills
{
    internal class Inspirational : FTKAPI_CharacterSkill
    {
        public Inspirational() 
        {
            Trigger = TriggerType.PerfectEncounterRoll | TriggerType.EndTurn;
            Name = new("Inspirational");
            Description = new("On a perfect skill check, nearby players are inspired at the end of the turn.");
        }

        public override void Skill(CharacterOverworld _cow, TriggerType _trig)
        {
            switch(_trig)
            {
                case TriggerType.PerfectEncounterRoll:
                    proc = true;
                    break;
                case TriggerType.EndTurn:
                    if (proc)
                    {
                        proc = false;
                        MigratedSkills.InspireKernel(_cow, true);
                    }
                    break;
            }
        }
    }
}
