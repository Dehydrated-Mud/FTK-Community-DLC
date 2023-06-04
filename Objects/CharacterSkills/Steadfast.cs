using CommunityDLC.PhotonHooks;
using FTKAPI.APIs.BattleAPI;
using FTKAPI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.CharacterSkills
{
    internal class Steadfast : FTKAPI_CharacterSkill
    {
        // Uses the BattleAPI to send a modified 
        public Steadfast() 
        {
            Name = new("Steady II");
            Description = new("Increases this character's chance to proc Steadfast");
        }

        public override void Skill(CharacterOverworld _cow, Query query)
        {
            switch(query)
            {
                case Query.StartCombatTurn:
                    CustomCharacterStatsDLC stats = _cow.gameObject.GetComponent<CustomCharacterStatsDLC>();
                    if (stats != null && _cow.GetCurrentDummy())
                    {
                        BattleAPI.Instance.SetAFloat(_cow.GetCurrentDummy(), stats.SteadfastChance, SetFloats.SteadFast, CombatValueOperators.Add);
                    }
                    break;
            }
        }
    }

    internal class CalledShot : FTKAPI_CharacterSkill
    {
        // Uses the BattleAPI to send a modified 
        public CalledShot()
        {
            Name = new("CalledShot II");
            Description = new("Increases this character's chance to proc CalledShot");
        }

        public override void Skill(CharacterOverworld _cow, Query query)
        {
            switch (query)
            {
                case Query.StartCombatTurn:
                    CustomCharacterStatsDLC stats = _cow.gameObject.GetComponent<CustomCharacterStatsDLC>();
                    if (stats != null && _cow.GetCurrentDummy())
                    {
                        BattleAPI.Instance.SetAFloat(_cow.GetCurrentDummy(), stats.CalledShotChance, SetFloats.CalledShot, CombatValueOperators.Add);
                    }
                    break;
            }
        }
    }
}
