using FTKAPI.APIs.BattleAPI;
using FTKAPI.Objects;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.CharacterSkills
{
    using ProficiencyManager = FTKAPI.Managers.ProficiencyManager;
    public class RushPlus : FTKAPI_CharacterSkill
    {
        public RushPlus() 
        {
            Trigger = TriggerType.None;        
            Name = new CustomLocalizedString("Skill: Blood Rush");
            Description = new CustomLocalizedString("Rushes an ally and guarantees that their next attack will be a critical strike.");
            SpecialProf = new ProfInfoContainer()
            {
                AttackProficiency = (FTK_proficiencyTable.ID)ProficiencyManager.Instance.enums["GreaterRushPlus"],
                OverrideSkillCheck = FTK_weaponStats2.SkillType.fortitude,
                OverrideAttackAnim = true,
                AttackAnim = CharacterDummy.AttackAnim.DirectAttack,
                WeaponType = Weapon.WeaponType.staff
            };
        }

        public override void Skill(CharacterOverworld _cow, Query query)
        {
            switch(query)
            {
                case Query.StartCombatTurn:
                    ProfInfoContainer container = new ProfInfoContainer(SpecialProf);
                    BattleAPI.Instance.SendProfInfo(container);
                    break;
            }
        }
    }
}
