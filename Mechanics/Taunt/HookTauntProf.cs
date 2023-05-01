using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTKAPI.APIs.BattleAPI;
using FTKAPI.Objects;
using FTKAPI.Objects.SkillHooks;
using static ProficiencyBase;
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
            if (_dummy.m_SufferingProficiencies.ContainsKey(Category.Taunt))
            {
                _dummy.m_SufferingProficiencies[Category.Taunt].m_Proficiency.End(_dummy);
            }
            _orig(_self, _dummy);
            BattleAPI.Instance.SetAStat(_dummy, _dummy.m_TauntArmor, SetFloats.ImperviousArmor, CombatValueOperators.Add);
            BattleAPI.Instance.SetAStat(_dummy, _dummy.m_TauntResist, SetFloats.ImperviousResist, CombatValueOperators.Add);
        }

        private void EndHook(On.ProficiencyTaunt.orig_End _orig, ProficiencyTaunt _self, CharacterDummy _dummy)
        {
            BattleAPI.Instance.SetAStat(_dummy, _dummy.m_TauntArmor, SetFloats.ImperviousArmor, CombatValueOperators.Subtract);
            BattleAPI.Instance.SetAStat(_dummy, _dummy.m_TauntResist, SetFloats.ImperviousResist, CombatValueOperators.Subtract);
            _orig(_self, _dummy);
        }

        public override void Unload()
        {
            On.ProficiencyTaunt.AddToDummy -= AddToDummyHook;
            On.ProficiencyTaunt.End -= EndHook;
        }
    }
}
