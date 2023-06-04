using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityDLC.PhotonHooks;
using CommunityDLC.Objects.CharacterSkills;
using FTKAPI.Objects;

namespace CommunityDLC.Objects.SkillTree.Leaves
{
    public class GoldForBeasts : Leaf
    {
        public GoldForBeasts(LeafID leafID) : base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills() { Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.livlihood } };
        }
    }

    public class XPForBeasts : Leaf
    {
        public XPForBeasts(LeafID leafID) : base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills() { Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.calloftheHunter } };
        }
    }

    public class BowDMG : Leaf
    {
        public BowDMG(LeafID leafID, float _dmg) : base(leafID)
        {
            Ranged = new WeaponMod()
            {
                m_AtkFac = _dmg
            };
        }
    }

    public class GunDMG : Leaf
    {
        public GunDMG(LeafID leafID, float _dmg) : base(leafID)
        {
            Firearm = new WeaponMod()
            {
                m_AtkFac = _dmg
            };
        }
    }

    public class ChainShot : Leaf
    {
        public ChainShot(LeafID leafID): base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills() { Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.calledRush } };
        }
    }

    public class CalledShotChance : Leaf
    {
        public CalledShotChance(LeafID leafID): base(leafID)
        {
            CalledShotChance = 0.1f;
            m_CharacterSkills = new CustomCharacterSkills() { Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.calledShot } };
        }
    }

    public class HunterOffA : Leaf
    {
        public HunterOffA(LeafID leafID): base(leafID)
        {
            Intelligence = 0.03f;
        }
    }

    public class HunterOffB : Leaf
    {
        public HunterOffB(LeafID leafID) : base(leafID)
        {
            Talent = 0.02f;
        }
    }
}
