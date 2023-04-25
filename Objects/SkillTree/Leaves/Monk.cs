using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityDLC.PhotonHooks;
using CommunityDLC.Objects.CharacterSkills;
using FTKAPI.Objects;
using static FTKAPI.Objects.CustomModifier;
using UnityEngine;
using Logger = FTKAPI.Utils.Logger;
using GridEditor;

namespace CommunityDLC.Objects.SkillTree.Leaves
{
    public class MonkUnarmedDmg : Leaf
    {
        public MonkUnarmedDmg(LeafID leafID, float dmg) : base(leafID) 
        {
            Unarmed = new WeaponMod()
            {
                m_AtkFac = dmg
            };
        }
    }

    public class MonkCombatMeditate : Leaf
    {
        public MonkCombatMeditate(LeafID leafID) : base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills()
            {
                Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.combatMeditation }
            };
        }
    }

    public class MonkEvade : Leaf
    {
        protected float evasion;
        public MonkEvade(LeafID leafID, float evade) : base(leafID)
        {
            evasion = evade;
            Conditional = true;
            ModTrigger = ModTriggerType.None; // Indicates tally usage as opposed to combat
            Color color = VisualParams.Instance.m_ColorTints.m_CharacterModTypeColor[ModType.StatMod];
            Name = new CustomLocalizedString($"{FTKUI.GetKeyInfoRichText(color, _bold: false, $"+{FTKUtil.RoundToInt(100f * evasion)} Evasion")} when unarmed");
            m_Method = Method.Defense;
        }

        public override void ConditionalTally(ref CharacterStats _stats, ref CustomCharacterStatsDLC _customStats, Method method)
        {
            switch (method)
            {
                case Method.Defense:
                    if (_stats.m_CharacterOverworld.GetCurrentDummy())
                    {
                        if (_stats.m_CharacterOverworld.GetCurrentDummy().m_EventListener.m_Weapon.m_WeaponType == Weapon.WeaponType.unarmed)
                        {
                            _customStats.m_EvasionMod += evasion;
                        }
                    }
                    break;

                default: break;
            }
        }
    }

    public class MonkGroupMeditation : Leaf
    {
        public MonkGroupMeditation(LeafID LeafID) : base(LeafID) 
        {
            m_CharacterSkills = new CustomCharacterSkills()
            {
                Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.groupMeditate }
            };
        }
    }

    public class MonkDisciplineThreshold : Leaf
    {
        public MonkDisciplineThreshold(LeafID LeafID, int threshold) : base(LeafID)
        {
            DisciplineFocus = threshold;
        }
    }


}
