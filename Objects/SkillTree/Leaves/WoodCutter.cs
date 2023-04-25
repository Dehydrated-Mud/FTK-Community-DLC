using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityDLC.Objects.CharacterSkills;
using CommunityDLC.PhotonHooks;
using FTKAPI.Objects;

namespace CommunityDLC.Objects.SkillTree.Leaves
{
    public class ModFindPouch : Leaf
    {
        public ModFindPouch(LeafID iD) : base(iD) 
        {
            ModCharacterSkills = new CustomCharacterSkills()
            {
                Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.findPouch }
            };
        }
    }
    public class ModBleedImmunity : Leaf
    {
        public ModBleedImmunity(LeafID leafID) : base(leafID) 
        {
            PartyImmuneBleed = true;
        }
    }
    public class ModWoodCutterIntVit : Leaf
    {
        public ModWoodCutterIntVit(LeafID id) : base(id)
        {
            Vitality = 0.02f;
            Intelligence = 0.07f;
        }
    }

    public class ModJustice : Leaf
    {
        public ModJustice(LeafID id) : base(id)
        {
            JusticeChance = 0.15f;
        }
    }

    public class ModVindication : Leaf
    {
        public ModVindication(LeafID id) : base(id)
        {
            m_CharacterSkills = new CustomCharacterSkills() { Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.justiceHeavyDamage } };
        }
    }

    public class ModDoubleEdge : Leaf
    {
        public ModDoubleEdge(LeafID id) : base(id)
        {
            Strength = 0.03f; //Placeholder
        }
    }

    public class WoodCutterLute : Leaf
    {
        public WoodCutterLute(LeafID id) : base(id)
        {
            Music = new WeaponMod()
            {
                m_AtkFac = 1f
            };
        }
    }
}
