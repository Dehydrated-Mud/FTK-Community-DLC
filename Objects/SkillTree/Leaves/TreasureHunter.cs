using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityDLC.Objects.CharacterSkills;
using FTKAPI.Objects;

namespace CommunityDLC.Objects.SkillTree.Leaves
{
    public class THConfuseImmunity : Leaf
    {
        public THConfuseImmunity(LeafID leafID) : base(leafID) 
        {
            PartyImmuneConfuse = true;
        }
    }

    public class THFireImmunity : Leaf
    {
        public THFireImmunity(LeafID leafID) : base(leafID)
        {
            PartyImmuneFire = true;
        }
    }

    public class THGoldMult : Leaf
    {
        public THGoldMult(LeafID leafID) : base(leafID)
        {
            GoldMultiplier = 0.1f;
        }
    }

    public class THAlwaysPrepared : Leaf
    {
        public THAlwaysPrepared(LeafID leafID) : base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills
            {
                Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.alwaysPrepared }
            };
        }
    }
}
