using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTKAPI.Objects;
using FTKAPI.Objects.SkillHooks;
using Logger = FTKAPI.Utils.Logger;
namespace CommunityDLC.Mechanics
{
    internal class HookTauntProf : BaseModule
    {
        public override void Initialize()
        {
            Unload();
            On.ProficiencyTaunt.AddToDummy += AddToDummyHook;
            On.ProficiencyTaunt.End += EndHook;
        }

        private void AddToDummyHook(On.ProficiencyTaunt.orig_AddToDummy _orig, ProficiencyTaunt _self, CharacterDummy _dummy) 
        {
            _orig(_self, _dummy);
            CustomCharacterStats customStats = _dummy.m_CharacterOverworld.gameObject.GetComponent<CustomCharacterStats>();
            if (customStats != null)
            {
                customStats.imperviousArmor += _dummy.m_TauntArmor;
                customStats.imperviousResistance += _dummy.m_TauntResist;
            }
            else
            {
                Logger.LogWarning("Could not find customstats on AddToDummy!");
            }
        }

        private void EndHook(On.ProficiencyTaunt.orig_End _orig, ProficiencyTaunt _self, CharacterDummy _dummy)
        {
            CustomCharacterStats customStats = _dummy.m_CharacterOverworld.gameObject.GetComponent<CustomCharacterStats>();
            if (customStats != null)
            {
                customStats.imperviousArmor -= _dummy.m_TauntArmor;
                customStats.imperviousResistance -= _dummy.m_TauntResist;
            }
            else
            {
                Logger.LogWarning("Could not find customstats on AddToDummy!");
            }
            _orig(_self, _dummy);
        }

        public override void Unload()
        {
            On.ProficiencyTaunt.AddToDummy -= AddToDummyHook;
            On.ProficiencyTaunt.End -= EndHook;
        }
    }
}
