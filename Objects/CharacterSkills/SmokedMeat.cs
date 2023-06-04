using FTKAPI.APIs.BattleAPI;
using FTKAPI.Objects;
using FTKAPI.Utils;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.CharacterSkills
{
    public class SmokedMeat : FTKAPI_CharacterSkill
    {
        public SmokedMeat()
        {
            Trigger = TriggerType.KillShot;
            Name = new("Passive Skill: Smoked Meat");
            Description = new("Character harvests food from certain dead enemies.");
        }

        public override void Skill(CharacterOverworld _cow, TriggerType _trig, AttackAttempt _atk)
        {
            switch (_trig)
            {
                case TriggerType.KillShot:
                    BattleAPI.Instance.AddDrop(FTK_itembase.ID.conCookie);
                    break;
            }
        }
    }
}
