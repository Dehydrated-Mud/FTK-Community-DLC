using FTKAPI.APIs.BattleAPI;
using FTKAPI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.CharacterSkills
{
    internal class AlwaysPrepared : FTKAPI_CharacterSkill
    {
        SpecialCombatAction combatAction = new SpecialCombatAction
        {
            EquipWeapon = true
        };
        public AlwaysPrepared() 
        {
            Name = new("Skill: Always Prepared");
            Description = new("At the beginning of combat the character is allowed to switch weapons for free.");
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
