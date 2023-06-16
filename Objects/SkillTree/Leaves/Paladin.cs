using FTKAPI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.SkillTree.Leaves
{
    public class PaladinThreshold : Leaf
    {
        public PaladinThreshold(LeafID leafID) : base(leafID) 
        {
            DIThreshold = 0.5f;
        }
    }

    public class PaladinSelf : Leaf
    {
        public PaladinSelf(LeafID leafID) : base(leafID)
        {
            Name = new("Divine Intervention can protect self");
            DISelf = true;
        }
    }

    public class PaladinHeal : Leaf
    {
        public PaladinHeal(LeafID leafID) : base(leafID)
        {
            FocusHeal = 0.05f;
        }
    }

    public class PaladinEncourage : Leaf
    {
        public PaladinEncourage(LeafID leafID) : base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills { m_Encourage = true };
        }
    }

    public class PaladinInspire : Leaf
    {
        public PaladinInspire(LeafID leafID) : base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills { m_Inspire = true };
        }
    }

}
