using CommunityDLC.Objects.SkillTree.MileStones;
using FTKAPI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CommunityDLC.Objects.SkillTree.Leaf;

namespace CommunityDLC.Objects.SkillTree.Trees
{
    internal class PaladinTree : ModifierTree
    {
        private CustomLocalizedString offA = new("Coordination");
        private CustomLocalizedString offB = new("Coordination");

        private CustomLocalizedString inspire = new("Motivational Speaker");
        private CustomLocalizedString autotaunt = new("Stalwart Ally");

        private CustomLocalizedString threshold = new("Divine Favor");
        private CustomLocalizedString myself = new("Chosen One");

        private CustomLocalizedString encourage = new("Lead by Example");
        private CustomLocalizedString heal = new("Healer");

        public PaladinTree()
        {
            LeafButton OffA1 = new LeafButton(LeafID.PaladinOffA1, LeafID.IndicatorA1)
            {
                Title = offA
            };
            LeafButton OffB1 = new LeafButton(LeafID.PaladinOffB1, LeafID.IndicatorB1)
            {
                Title = offB
            };

            LeafButton OffA2 = new LeafButton(LeafID.PaladinOffA2, LeafID.IndicatorA2)
            {
                Title = offA
            };
            LeafButton OffB2 = new LeafButton(LeafID.PaladinOffB2, LeafID.IndicatorB2)
            {
                Title = offB
            };

            LeafButton OffA3 = new LeafButton(LeafID.PaladinOffA3, LeafID.IndicatorA3)
            {
                Title = offA
            };
            LeafButton OffB3 = new LeafButton(LeafID.PaladinOffB3, LeafID.IndicatorB3)
            {
                Title = offB
            };

            LeafButton Rebuttal = new LeafButton(LeafID.PaladinInspire, LeafID.IndicatorA4)
            {
                Title = inspire
            };
            LeafButton Retaliate = new LeafButton(LeafID.HoboFouldStench, LeafID.IndicatorB4)
            {
                Title = autotaunt
            };

            LeafButton CrushingBlow = new LeafButton(LeafID.PaladinThreshold, LeafID.IndicatorA5)
            {
                Title = threshold
            };
            LeafButton Trauma = new LeafButton(LeafID.PaladinSelf, LeafID.IndicatorB5)
            {
                Title = myself
            };

            LeafButton Steady = new LeafButton(LeafID.PaladinEncourage, LeafID.IndicatorA6)
            {
                Title = encourage
            };
            LeafButton LeatherBack = new LeafButton(LeafID.PaladinHeal, LeafID.IndicatorB6)
            {
                Title = heal
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
