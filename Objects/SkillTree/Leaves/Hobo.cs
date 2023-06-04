using CommunityDLC.Objects.CharacterSkills;
using FTKAPI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.SkillTree.Leaves
{
    public class WhatsThatSmell : Leaf
    {
        public WhatsThatSmell(LeafID leafID) : base(leafID) 
        {
            m_CharacterSkills = new CustomCharacterSkills { Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.autoTaunt } };
        }
    }

    public class FindFood : Leaf
    {
        public FindFood(LeafID leafID) : base(leafID) 
        {
            m_CharacterSkills = new CustomCharacterSkills { Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.eatThat} };
        }
    }

    public class FindDrink : Leaf
    {
        public FindDrink(LeafID leafID) : base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills { Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.drinkThat } };
        }
    }

    public class HoboRum : Leaf
    {
        public HoboRum(LeafID leafID) : base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills { Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.rumsTheWord } };
        }
    }

    public class HoboNook : Leaf
    {
        public HoboNook(LeafID leafID) : base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills { Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.findNook } };
        }
    }
}
