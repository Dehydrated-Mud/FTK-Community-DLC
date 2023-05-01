using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityDLC.Objects.CharacterSkills;
using CommunityDLC.PhotonHooks;
using FTKAPI.Objects;

namespace CommunityDLC.Objects.SkillTree.Leaves
{
    public class BlackSmithOffA : Leaf
    {
        public BlackSmithOffA(LeafID leafID) : base(leafID) 
        {
            Intelligence = 0.08f;
        }
    }

    public class BlackSmithOffB : Leaf
    {
        public BlackSmithOffB(LeafID leafID) : base(leafID)
        {
            Speed = 0.02f;
        }
    }
    public class IncreasedSteady : Leaf
    {
        public IncreasedSteady(LeafID leafID) : base(leafID) 
        {
            SteadfastChance = 0.08f;
            m_CharacterSkills = new CustomCharacterSkills() { Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.steadfast } };
        }
    }

    public class LeatherBack : Leaf
    {
        public LeatherBack(LeafID leafID) : base(leafID)
        {
            Name = new("+0.5 Impervious Armor & Resistance per level");
            Conditional = true;
            m_Method = Method.Defense;
        }

        public override void ConditionalTally(ref CharacterStats _stats, ref CustomCharacterStatsDLC _customStats, Method defense)
        {
            switch(defense)
            {
                case Method.Defense:
                    int level = _stats.m_PlayerLevel;
                    _customStats.ImperviousResist += FTKUtil.RoundToInt(0.5f * level);
                    _customStats.ImperviousArmor += FTKUtil.RoundToInt(0.5f * level);
                    _stats.m_ModDefensePhysical += FTKUtil.RoundToInt(0.5f * level);
                    _stats.m_ModDefenseMagic += FTKUtil.RoundToInt(0.5f * level);
                    break;
            }
        }
    }

    public class Retaliation : Leaf
    {
        public Retaliation(LeafID leafID) : base(leafID)
        {
            Conditional = true;
            m_Method = Method.Mods;
            Name = new("+1 Damage Reflection per Level");
        }

        public override void ConditionalTally(ref CharacterStats _stats, ref CustomCharacterStatsDLC _customStats, Method defense)
        {
            switch(defense) 
            { 
                case Method.Mods:
                    _stats.m_ReflectDamage += _stats.m_PlayerLevel;
                    break;
            }
        }
    }

    public class BluntTrauma : Leaf
    {
        public BluntTrauma(LeafID leafID) : base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills
            {
                Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.bluntForceTrauma }
            };
        }
    }

    public class Rebuttal : Leaf
    {
        public Rebuttal(LeafID leafID) : base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills
            {
                Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.rebuttal }
            };
        }
    }

    public class CrushingBlow : Leaf
    {
        public CrushingBlow(LeafID leafID) : base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills
            {
                Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.crushingBlow }
            };
        }
    }
}
