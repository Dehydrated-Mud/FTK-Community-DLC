using FTKAPI.APIs.BattleAPI;
using FTKAPI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.CharacterSkills
{
    internal class AutoTaunt : FTKAPI_CharacterSkill
    {

        SpecialCombatAction combatAction = new SpecialCombatAction
        {
            Taunt = true
        };
        public AutoTaunt()
        {
            Name = new("Skill: What's that smell?");
            Description = new("At the beginning of combat the character is given a chance to taunt.");
        }

        public override void Skill(CharacterOverworld _cow, Query query)
        {
            switch (query)
            {
                case Query.StartCombat:
                    if (_cow.GetCurrentDummy())
                    {
                        BattleAPI.Instance.DeclareSpecialAction(_cow.GetCurrentDummy(), combatAction);
                    }
                    break;
            }
        }
    }
}

