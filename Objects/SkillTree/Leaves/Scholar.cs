using CommunityDLC.Objects.CharacterSkills;
using CommunityDLC.PhotonHooks;
using FTKAPI.APIs.BattleAPI;
using FTKAPI.Objects;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.SkillTree.Leaves
{
    public class WandFocus : Leaf
    {
        public WandFocus(LeafID leafID) : base(leafID) 
        {
            Conditional = true;
        }
        public override void ConditionalTally(ref CharacterStats _stats, ref CustomCharacterStatsDLC _customStats, Method defense)
        {
            switch (defense)
            {
                case Method.Mods:
                    Weapon weapon = BattleHelpers.GetWeapon(_stats.m_CharacterOverworld);
                    if (weapon?.m_WeaponType == Weapon.WeaponType.wand)
                    {
                        _customStats.m_RefocusChance += 0.3f;
                    }
                    break;
            }
        }
    }

    public class BloodRushLeaf : Leaf
    {
        public BloodRushLeaf(LeafID leafID) : base(leafID) 
        {
            m_CharacterSkills = new CustomCharacterSkills
            {
                Skills = new List<FTKAPI_CharacterSkill> { SkillContainer.Instance.bloodRush }
            };
        }
    }

    public class MageChoiceFire : Leaf
    {
        public MageChoiceFire(LeafID leafID): base(leafID)
        {
            BonusAgainstRace1 = GridEditor.FTK_enemyCombat.EnemyRace.Ice;
            BonusAgainstRace1Value = 0.25f;

            Fire = new WeaponMod
            {
                m_AtkFac = +0.25f
            };
            Ice = new WeaponMod
            {
                m_AtkFac = -0.15f
            };
            Water = new WeaponMod
            {
                m_AtkFac = -0.15f
            };
        }
    }

    public class MageChoiceWater : Leaf
    {
        public MageChoiceWater(LeafID leafID) : base(leafID)
        {
            BonusAgainstRace1 = GridEditor.FTK_enemyCombat.EnemyRace.Fire;
            BonusAgainstRace1Value = 0.25f;
            BonusAgainstRace2 = GridEditor.FTK_enemyCombat.EnemyRace.Lightning;
            BonusAgainstRace1Value = 0.25f;

            Water = new WeaponMod
            {
                m_AtkFac = +0.35f
            };
            Fire = new WeaponMod
            {
                m_AtkFac = -0.15f
            };
            Lightning = new WeaponMod
            {
                m_AtkFac = -0.15f
            };
        }
    }

    public class MageChoiceIce : Leaf
    {
        public MageChoiceIce(LeafID leafID) : base(leafID)
        {
            BonusAgainstRace1 = GridEditor.FTK_enemyCombat.EnemyRace.Water;
            BonusAgainstRace1Value = 0.25f;

            Fire = new WeaponMod
            {
                m_AtkFac = -0.15f
            };
            Ice = new WeaponMod
            {
                m_AtkFac = +0.25f
            };
        }
    }

    public class MageChoiceLightning : Leaf
    {
        public MageChoiceLightning(LeafID leafID) : base(leafID)
        {
            BonusAgainstRace1 = GridEditor.FTK_enemyCombat.EnemyRace.Water;
            BonusAgainstRace1Value = 0.25f;

            Lightning = new WeaponMod
            {
                m_AtkFac = +0.25f
            };
            Water = new WeaponMod
            {
                m_AtkFac = -0.15f
            };
        }
    }

    public class XPWithTome : Leaf
    {
        public XPWithTome(LeafID leafID) : base(leafID)
        {
            Conditional = true;
        }
        public override void ConditionalTally(ref CharacterStats _stats, ref CustomCharacterStatsDLC _customStats, Method method)
        {
            switch (method)
            {
                case Method.Mods:
                    Weapon weapon = BattleHelpers.GetWeapon(_stats.m_CharacterOverworld);
                    if (weapon?.m_WeaponType == Weapon.WeaponType.book)
                    {
                        _stats.m_ModXp += 0.15f;
                    }
                    break;
            }
        }
    }

    public class ScholarWandDamage : Leaf
    {
        public ScholarWandDamage(LeafID leafID, int damage) : base(leafID)
        {
            Wand = new WeaponMod
            {
                m_AtkAdd = 2
            };
        }
    }

    public class ScholarStaffDef : Leaf
    {
        public ScholarStaffDef(LeafID leafID): base(leafID)
        {
            Conditional = true;
        }
        public override void ConditionalTally(ref CharacterStats _stats, ref CustomCharacterStatsDLC _customStats, Method method)
        {
            switch (method)
            {
                case Method.Defense:
                    Weapon weapon = BattleHelpers.GetWeapon(_stats.m_CharacterOverworld);
                    if (weapon?.m_WeaponType == Weapon.WeaponType.staff)
                    {
                        _stats.m_PartyCombatArmor += 1;
                        _stats.m_PartyCombatResist += 1;
                    }
                    break;
            }
        }
    }

    public class ScholarOffA : Leaf
    {
        public ScholarOffA(LeafID leafID): base(leafID)
        {
            Strength = 0.02f;
        }
    }

    public class ScholarOffB : Leaf
    {
        public ScholarOffB(LeafID leafID) : base(leafID)
        {
            Vitality = 0.02f;
        }
    }
}
