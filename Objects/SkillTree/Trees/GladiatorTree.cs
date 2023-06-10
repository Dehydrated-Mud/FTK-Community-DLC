using CommunityDLC.Objects.SkillTree.MileStones;
using FTKAPI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CommunityDLC.Objects.SkillTree.Leaf;

namespace CommunityDLC.Objects.SkillTree.Trees
{
    internal class GladiatorTree : ModifierTree
    {
        private CustomLocalizedString offA = new("Bravery");
        private CustomLocalizedString offB = new("Skill");

        private CustomLocalizedString lifesteal = new("Kill or be Killed");
        private CustomLocalizedString berserk = new("Berserker");

        private CustomLocalizedString lifeDrain = new("Life Drain");
        private CustomLocalizedString crit = new("An Eye for Death");

        private CustomLocalizedString lust = new("Thrill of the Kill");
        private CustomLocalizedString dirty = new("That's not fair.");

        public GladiatorTree()
        {
            LeafButton OffA1 = new LeafButton(LeafID.GladiatorOffA1, LeafID.IndicatorA1)
            {
                Title = offA
            };
            LeafButton OffB1 = new LeafButton(LeafID.GladiatorOffB1, LeafID.IndicatorB1)
            {
                Title = offB
            };

            LeafButton OffA2 = new LeafButton(LeafID.GladiatorOffA2, LeafID.IndicatorA2)
            {
                Title = offA
            };
            LeafButton OffB2 = new LeafButton(LeafID.GladiatorOffB2, LeafID.IndicatorB2)
            {
                Title = offB
            };

            LeafButton OffA3 = new LeafButton(LeafID.GladiatorOffA3, LeafID.IndicatorA3)
            {
                Title = offA
            };
            LeafButton OffB3 = new LeafButton(LeafID.GladiatorOffB3, LeafID.IndicatorB3)
            {
                Title = offB
            };

            LeafButton Lifesteal = new LeafButton(LeafID.GladiatorLifesteal, LeafID.IndicatorA4)
            {
                Title = lifesteal
            };
            LeafButton Berserk = new LeafButton(LeafID.GladiatorBerserk, LeafID.IndicatorB4)
            {
                Title = berserk
            };

            LeafButton LifeDrain = new LeafButton(LeafID.GladiatorLifeDrain, LeafID.IndicatorA5)
            {
                Title = lifeDrain
            };
            LeafButton Crit = new LeafButton(LeafID.GladiatorCritical, LeafID.IndicatorB5)
            {
                Title = crit
            };

            LeafButton BloodLust = new LeafButton(LeafID.GladiatorBloodLust, LeafID.IndicatorA6)
            {
                Title = lust
            };
            LeafButton DirtyTactics = new LeafButton(LeafID.GladiatorDirtyTactics, LeafID.IndicatorB6)
            {
                Title = dirty
            };


            Branch Branch1 = new Branch(new List<LeafButton>() { OffA1, OffB1 })
            {
                MileStone = new LevelMilestone(4)
            };
            Branch Branch2 = new Branch(new List<LeafButton>() { Lifesteal, Berserk })
            {
                MileStone = new LevelMilestone(5)
            };
            Branch Branch3 = new Branch(new List<LeafButton>() { OffA2, OffB2 })
            {
                MileStone = new LevelMilestone(6)
            };
            Branch Branch4 = new Branch(new List<LeafButton>() { LifeDrain, Crit })
            {
                MileStone = new LevelMilestone(7)
            };
            Branch Branch5 = new Branch(new List<LeafButton>() { OffA3, OffB3 })
            {
                MileStone = new LevelMilestone(8)
            };
            Branch Branch6 = new Branch(new List<LeafButton>() { BloodLust, DirtyTactics })
            {
                MileStone = new LevelMilestone(9)
            };


            List<Branch> branches = new List<Branch> { Branch1, Branch2, Branch3, Branch4, Branch5, Branch6 };
            branches.Reverse();
            Branches = branches;
        }
    }
}
