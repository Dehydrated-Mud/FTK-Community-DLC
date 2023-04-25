using Logger = FTKAPI.Utils.Logger;
using FTKAPI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeaponType = Weapon.WeaponType;
using CommunityDLC.PhotonHooks;
using FullInspector.Generated;
using HutongGames.PlayMaker.Actions;

namespace CommunityDLC.Objects.CharacterSkills
{
    public class DamageMods : FTKAPI_CharacterSkill
    {
        public DamageMods() 
        {
            Trigger = TriggerType.None;
            ModTrigger = ModTriggerType.DamageAdd | ModTriggerType.DamageFac;
            Name = new CustomLocalizedString("DamageMod Skill");
            Description = new CustomLocalizedString("Skill intrinsic to all characters to help with damage calculation");
        }

        public override float ModifyCombatValue(CharacterOverworld cow, ModTriggerType trig, AttackAttempt atk, bool attacker)
        {
            WeaponMod mainMod = new WeaponMod();
            WeaponMod subMod = new WeaponMod();
            // Try and get our damage modifiers. If customstats does not exist or we can't find the key in the dictionary, proceed with no damage modification.
            try
            {
                mainMod = cow.gameObject.GetComponent<CustomCharacterStatsDLC>().DamageModifiers[atk.m_WeaponType];
            }
            catch (Exception e) 
            {
                if (e is NullReferenceException || e is KeyNotFoundException)
                {
                    Logger.LogError(e.Message);
                    Logger.LogError("Could not get custom stat damage modifier for " + cow.m_CharacterStats.m_CharacterClass.ToString());
                }
                else
                {
                    throw;
                }
            }

            try
            {
                subMod = cow.gameObject.GetComponent<CustomCharacterStatsDLC>().SubDamageModifiers[atk.m_WeaponSubType];
            }
            catch (Exception e)
            {
                if (e is NullReferenceException || e is KeyNotFoundException)
                {
                    Logger.LogError(e.Message);
                    Logger.LogError("Could not get custom stat damage modifier for " + cow.m_CharacterStats.m_CharacterClass.ToString());
                }
                else
                {
                    throw;
                }
            }
            Logger.LogWarning($"Main Mod fac: {mainMod.m_AtkFac}");
            switch (trig)
            {
                case ModTriggerType.DamageAdd:
                    if (attacker)
                    {
                        return mainMod.m_AtkAdd + subMod.m_AtkAdd;
                    }
                    else
                    {
                        return mainMod.m_DefAdd + subMod.m_DefAdd;
                    }
                case ModTriggerType.DamageFac:
                    if (attacker)
                    {
                        return mainMod.m_AtkFac + subMod.m_AtkFac; 
                    }
                    else
                    {
                        return -Math.Min((Math.Min(mainMod.m_DefFac,1) + Math.Min(subMod.m_DefFac,1)),0.5f);
                    }
                default:
                    Logger.LogError("ModifyCombatValue called with invalid triggertype!");
                    return 1;
            }
        }
    }
}
