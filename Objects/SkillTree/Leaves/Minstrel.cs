using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityDLC.Objects.CharacterSkills;
using FTKAPI.Objects;

namespace CommunityDLC.Objects.SkillTree.Leaves
{
    internal class MinstrelEncourage : Leaf
    {
        public MinstrelEncourage(LeafID leafID) : base(leafID) 
        {
            EncourageChance = .1f;
        }
    }

    internal class MinstrelInspireChance : Leaf
    {
        public MinstrelInspireChance(LeafID leafID) : base(leafID)
        {
            InspireChance = 0.1f;
        }
    }

    internal class MinstrelInspireXP : Leaf
    {
        public MinstrelInspireXP(LeafID leafID) : base(leafID)
        {
            InspireXP = 1f;
        }
    }

    internal class MinstrelInspirational : Leaf
    {
        public MinstrelInspirational(LeafID leafID) : base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills { Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.inspirational } };
        }
    }

    internal class MinstrelInn : Leaf
    {
        public MinstrelInn(LeafID leafID) : base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills { Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.freeInn } };
        }
    }

    internal class MinstrelOffA : Leaf
    {
        public MinstrelOffA(LeafID leafID) : base(leafID)
        {
            EvadeRating = 0.02f;
        }
    }

    internal class MinstrelOffB : Leaf
    {
        public MinstrelOffB(LeafID leafID) : base(leafID)
        {
            Vitality = 0.04f;
        }
    }
}
