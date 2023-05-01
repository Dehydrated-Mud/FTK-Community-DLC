using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityDLC.PhotonHooks;
using FTKAPI.Objects;

namespace CommunityDLC.Objects.CharacterSkills
{
    public class JusticeHeavyDamage : FTKAPI_CharacterSkill
    {
        public JusticeHeavyDamage() 
        {
            Trigger = TriggerType.TakeHeavyDamage; //Trigger when we take heavy damage
            Name = new CustomLocalizedString("Passive Skill: Vindication");
            Description = new CustomLocalizedString("Each time the character takes heavy damage, the character's chance to proc Justice is doubled until the end of battle.");
        }

        public override void Skill(CharacterOverworld cow, TriggerType trig, AttackAttempt atk)
        {
            switch (trig)
            {
                case TriggerType.TakeHeavyDamage:
                    CustomCharacterStatsDLC customStats = cow.gameObject.GetComponent<CustomCharacterStatsDLC>();
                    if (customStats != null)
                    {
                        if (customStats.JusticeChance == 0f)
                        {
                            customStats.JusticeChance = 1f;
                            break;
                        }
                        customStats.JusticeChance *= 2f;
                    }
                    // I do not think this needs to be RPCd, as all the clients will execute this in response to the heavy damage.
                    // No, I think it does. We should set up an RPC to sync customcharacterstats. Or, we could move damage reactions to the damage react location
                    break;
            }
        }
    }
}
