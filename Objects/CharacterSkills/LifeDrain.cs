using FTKAPI.APIs.BattleAPI;
using FTKAPI.Managers;
using FTKAPI.Objects;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logger = FTKAPI.Utils.Logger;

namespace CommunityDLC.Objects.CharacterSkills
{
    using ProficiencyManager = FTKAPI.Managers.ProficiencyManager;
    internal class LifeDrain : FTKAPI_CharacterSkill
    {
        private Dictionary<FTKPlayerID, bool> m_Cooldowns = new();
        public LifeDrain() 
        {
            Trigger = TriggerType.DamageCalcEnd;
            Name = new("Skill: Blood Thirst");
            Description = new("The thirst for blood will never be sated. Steals a significant amount of health and inflicts bleed upon the victim.");
            SpecialProf = new ProfInfoContainer
            {
                AttackProficiency = FTK_proficiencyTable.ID.bleed1,
                CategoryDescription = new CustomLocalizedString("Blood Thirst").GetLocalizedString(),
            };
        }
        public override void Skill(CharacterOverworld _cow, Query query)
        {
            switch(query)
            {
                case Query.StartCombatTurn:
                    if (!m_Cooldowns.ContainsKey(_cow.m_FTKPlayerID))
                    {
                        m_Cooldowns[_cow.m_FTKPlayerID] = false;
                    }
                    BattleAPI.Instance.SendProfInfo(new ProfInfoContainer(SpecialProf)
                    {
                        IsCoolingDown = m_Cooldowns[_cow.m_FTKPlayerID]
                    }) ;
                    break;
                case Query.EndCombat:
                    m_Cooldowns.Clear();
                    break;
            }
        }
        public override void Skill(CharacterOverworld cow, TriggerType trig, AttackAttempt atk)
        {
            switch(trig)
            {
                case TriggerType.DamageCalcEnd:
                    Logger.LogWarning($"Proficiency Match? {atk.m_AttackProficiency == SpecialProf.AttackProficiency}");
                    if (atk.m_AttackProficiency == SpecialProf.AttackProficiency && atk.m_DamagedDummy is EnemyDummy)
                    {
                        
                        int extraSteal = - FTKUtil.RoundToInt(atk.m_TotalReceivedDMG / 2);
                        Logger.LogWarning($"Extra steal: {extraSteal}");
                        BattleAPI.Instance.SetAFloat(atk.m_AttackingDummy, extraSteal, SetFloats.DamageReflection, CombatValueOperators.Add); //Sync this?
                        m_Cooldowns[cow.m_FTKPlayerID] = true;
                    }
                    break;
            }
        }


    }
}
