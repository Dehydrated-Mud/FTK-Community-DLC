using CommunityDLC.PhotonHooks;
using CommunityDLC.Objects.CharacterSkills;
using FTKAPI.APIs.BattleAPI;
using FTKAPI.Objects;
using UnityEngine;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.SkillTree.Leaves
{
    internal class GladiatorCrit : Leaf
    {
        float crit = 0.1f;
        public GladiatorCrit(LeafID leafID) : base(leafID) 
        {
            ModTrigger = ModTriggerType.None;
            Conditional = true;
            m_Method = Method.Mods;
            Color color = VisualParams.Instance.m_ColorTints.m_CharacterModTypeColor[ModType.StatMod];
            Name = new CustomLocalizedString($"{FTKUI.GetKeyInfoRichText(color, _bold: false, $"+{FTKUtil.RoundToInt(100f * crit)}% Crit chance")} when using an awareness weapon.");
        }
        public override void ConditionalTally(ref CharacterStats _stats, ref CustomCharacterStatsDLC _customStats, Method defense)
        {
            switch(defense)
            {
                case Method.Mods:
                    FTK_weaponStats2 weapon = FTK_weaponStats2DB.Get(_stats.m_CharacterOverworld.m_WeaponID);
                    if (weapon._skilltest == FTK_weaponStats2.SkillType.awareness)
                    {
                        _stats.m_ModCritChance += crit;
                    }
                    break;
            }
        }
    }

    internal class GladiatorLifesteal : Leaf
    {
        public GladiatorLifesteal(LeafID leafID) : base(leafID)
        {
            LifeStealFac = 1; //+100%
        }
    }

    internal class GladiatorDirty : Leaf
    {
        public GladiatorDirty(LeafID leafID) : base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills { Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.dirtyTactics } };
        }
    }

    internal class GladiatorLust : Leaf
    {
        public GladiatorLust(LeafID leafID) : base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills { Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.thrillKill } };
        }
    }

    internal class GladiatorBerserk : Leaf
    {
        public GladiatorBerserk(LeafID leafID) : base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills { Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.berserker } };
        }
    }

    internal class GladiatorLifeDrain : Leaf
    {
        public GladiatorLifeDrain(LeafID leafID): base(leafID)
        {
            m_CharacterSkills = new CustomCharacterSkills { Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.lifeDrain } };
        }
    }
}
