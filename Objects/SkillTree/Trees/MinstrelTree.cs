using CommunityDLC.Objects.SkillTree.MileStones;
using FTKAPI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CommunityDLC.Objects.SkillTree.Leaf;

namespace CommunityDLC.Objects.SkillTree.Trees
{
    internal class MinstrelTree : ModifierTree
    {
        private CustomLocalizedString offA = new("Coordination");
        private CustomLocalizedString offB = new("Coordination");

        private CustomLocalizedString xp = new("Breath Taking");
        private CustomLocalizedString chance = new("The show must go on");

        private CustomLocalizedString inspirational = new("Mastery");
        private CustomLocalizedString inn = new("Popularity");

        private CustomLocalizedString encourage = new("Encourage");
        private CustomLocalizedString distract = new("Distract");

        public MinstrelTree()
        {
            LeafButton OffA1 = new LeafButton(LeafID.MinstrelOffA1, LeafID.IndicatorA1)
            {
                Title = offA
            };
            LeafButton OffB1 = new LeafButton(LeafID.MinstrelOffB1, LeafID.IndicatorB1)
            {
                Title = offB
            };

            LeafButton OffA2 = new LeafButton(LeafID.MinstrelOffA2, LeafID.IndicatorA2)
            {
                Title = offA
            };
            LeafButton OffB2 = new LeafButton(LeafID.MinstrelOffB2, LeafID.IndicatorB2)
            {
                Title = offB
            };

            LeafButton OffA3 = new LeafButton(LeafID.MinstrelOffA3, LeafID.IndicatorA3)
            {
                Title = offA
            };
            LeafButton OffB3 = new LeafButton(LeafID.MinstrelOffB3, LeafID.IndicatorB3)
            {
                Title = offB
            };

            LeafButton XP = new LeafButton(LeafID.MinstrelInspireXP, LeafID.IndicatorA4)
            {
                Title = xp
            };
            LeafButton Chance = new LeafButton(LeafID.MinstrelInspireChance, LeafID.IndicatorB4)
            {
                Title = chance
            };

            LeafButton Inspirational = new LeafButton(LeafID.MinstrelInspirational, LeafID.IndicatorA5)
            {
                Title = inspirational
            };
            LeafButton Inn = new LeafButton(LeafID.MinstrelInn, LeafID.IndicatorB5)
            {
                Title = inn
            };

            LeafButton Encourage = new LeafButton(LeafID.MinstrelEncourage, LeafID.IndicatorA6)
            {
                Title = encourage
            };
            LeafButton Distract = new LeafButton(LeafID.MinstrelDistract, LeafID.IndicatorB6)
            {
                Title = distract
            };


            Branch Branch1 = new Branch(new List<LeafButton>() { OffA1, OffB1 })
            {
                MileStone = new LevelMilestone(4)
            };
            Branch Branch2 = new Branch(new List<LeafButton>() { XP, Chance })
            {
                MileStone = new LevelMilestone(5)
            };
            Branch Branch3 = new Branch(new List<LeafButton>() { OffA2, OffB2 })
            {
                MileStone = new LevelMilestone(6)
            };
            Branch Branch4 = new Branch(new List<LeafButton>() { Inspirational, Inn })
            {
                MileStone = new LevelMilestone(7)
            };
            Branch Branch5 = new Branch(new List<LeafButton>() { OffA3, OffB3 })
            {
                MileStone = new LevelMilestone(8)
            };
            Branch Branch6 = new Branch(new List<LeafButton>() { Encourage, Distract })
            {
                MileStone = new LevelMilestone(9)
            };


            List<Branch> branches = new List<Branch> { Branch1, Branch2, Branch3, Branch4, Branch5, Branch6 };
            branches.Reverse();
            Branches = branches;
        }
    }
}
