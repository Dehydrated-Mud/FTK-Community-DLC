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
    internal class CrushingBlow : FTKAPI_CharacterSkill
    {
        private Dictionary<FTKPlayerID, bool> m_Cooldowns = new();
        public CrushingBlow() 
        {
            Trigger = TriggerType.DamageCalcStart;
            Name = new("Skill: Crushing Blow");
            Description = new("50% of Enemy's missing HP is added as damage.");
            SpecialProf = new ProfInfoContainer
            {
                AttackProficiency = (FTK_proficiencyTable.ID)ProficiencyManager.Instance.enums["CrushingBlowProf"],
                CategoryDescription = new CustomLocalizedString("Crushing Blow").GetLocalizedString(),
                OverrideSkillCheck = FTK_weaponStats2.SkillType.toughness,
                WeaponType = Weapon.WeaponType.blunt
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
                case TriggerType.DamageCalcStart:
                    Logger.LogWarning($"Proficiency Match? {atk.m_AttackProficiency == SpecialProf.AttackProficiency}");
                    if (atk.m_AttackProficiency == SpecialProf.AttackProficiency && atk.m_DamagedDummy is EnemyDummy)
                    {
                        EnemyDummy enemy = (EnemyDummy)atk.m_DamagedDummy;
                        int extraDmg = (int)(0.5f * (enemy.m_EnemyCombat.GetHealthTotal() - enemy.GetCurrentHealth()));

                        BattleAPI.Instance.SetAFloat(atk.m_AttackingDummy, extraDmg, SetFloats.OutgoingTotalDmg, CombatValueOperators.Add);
                        m_Cooldowns[cow.m_FTKPlayerID] = true;

                    }
                    break;
            }
        }


    }
}
