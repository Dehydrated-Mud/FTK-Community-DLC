using CommunityDLC.Objects.SkillTree.MileStones;
using FTKAPI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CommunityDLC.Objects.SkillTree.Leaf;

namespace CommunityDLC.Objects.SkillTree.Trees
{
    internal class BlackSmithTree : ModifierTree
    {
        private CustomLocalizedString offA = new("Coordination");
        private CustomLocalizedString offB = new("Coordination");

        private CustomLocalizedString rebuttal = new("You're gonna regret that");
        private CustomLocalizedString retaliate = new("Spikey Exterior");

        private CustomLocalizedString crush = new("Hammer Down");
        private CustomLocalizedString trauma = new("Blunt Force Trauma");

        private CustomLocalizedString steady = new("Steady");
        private CustomLocalizedString leatherback = new("Leather Back");

        public BlackSmithTree()
        {
            LeafButton OffA1 = new LeafButton(LeafID.BlackSmithOffA1, LeafID.IndicatorA1)
            {
                Title = offA
            };
            LeafButton OffB1 = new LeafButton(LeafID.BlackSmithOffB1, LeafID.IndicatorB1)
            {
                Title = offB
            };

            LeafButton OffA2 = new LeafButton(LeafID.BlackSmithOffA2, LeafID.IndicatorA2)
            {
                Title = offA
            };
            LeafButton OffB2 = new LeafButton(LeafID.BlackSmithOffB2, LeafID.IndicatorB2)
            {
                Title = offB
            };

            LeafButton OffA3 = new LeafButton(LeafID.BlackSmithOffA3, LeafID.IndicatorA3)
            {
                Title = offA
            };
            LeafButton OffB3 = new LeafButton(LeafID.BlackSmithOffB3, LeafID.IndicatorB3)
            {
                Title = offB
            };

            LeafButton Rebuttal = new LeafButton(LeafID.BlackSmithRebuttal, LeafID.IndicatorA4)
            {
                Title = rebuttal
            };
            LeafButton Retaliate = new LeafButton(LeafID.BlackSmithRetaliate, LeafID.IndicatorB4)
            {
                Title = retaliate
            };

            LeafButton CrushingBlow = new LeafButton(LeafID.BlackSmithCrush, LeafID.IndicatorA5)
            {
                Title = crush
            };
            LeafButton Trauma = new LeafButton(LeafID.BlackSmithBFTrauma, LeafID.IndicatorB5)
            {
                Title = trauma
            };

            LeafButton Steady = new LeafButton(LeafID.BlackSmithSteady, LeafID.IndicatorA6)
            {
                Title = steady
            };
            LeafButton LeatherBack = new LeafButton(LeafID.BlackSmithLeatherBack, LeafID.IndicatorB6)
            {
                Title = leatherback
            };


            Branch Branch1 = new Branch(new List<LeafButton>() { OffA1, OffB1 })
            {
                MileStone = new LevelMilestone(4)
            };
            Branch Branch2 = new Branch(new List<LeafButton>() { Rebuttal, Retaliate })
            {
                MileStone = new LevelMilestone(5)
            };
            Branch Branch3 = new Branch(new List<LeafButton>() { OffA2, OffB2 })
            {
                MileStone = new LevelMilestone(6)
            };
            Branch Branch4 = new Branch(new List<LeafButton>() { CrushingBlow, Trauma })
            {
                MileStone = new LevelMilestone(7)
            };
            Branch Branch5 = new Branch(new List<LeafButton>() { OffA3, OffB3 })
            {
                MileStone = new LevelMilestone(8)
            };
            Branch Branch6 = new Branch(new List<LeafButton>() { Steady, LeatherBack })
            {
                MileStone = new LevelMilestone(9)
            };


            List<Branch> branches = new List<Branch> { Branch1, Branch2, Branch3, Branch4, Branch5, Branch6 };
            branches.Reverse();
            Branches = branches;
        }
    }
}
