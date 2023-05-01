using CommunityDLC.Objects.SkillTree.MileStones;
using FTKAPI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CommunityDLC.Objects.SkillTree.Leaf;

namespace CommunityDLC.Objects.SkillTree.Trees
{
    internal class TreasureHunterTree : ModifierTree
    {

        private CustomLocalizedString offA = new("Coordination");

        private CustomLocalizedString goldMult = new("Keen Eye");
        private CustomLocalizedString ptyConfuse = new("Stick to the Plan");

        private CustomLocalizedString prepared = new("Always Prepared");
        private CustomLocalizedString luck = new("Improvise");

        private CustomLocalizedString bleed = new("First Aid Training");
        private CustomLocalizedString fire = new("Stop, Drop, and Roll");

        public TreasureHunterTree()
        {
            LeafButton OffA1 = new LeafButton(LeafID.THPartyInt, LeafID.IndicatorA1)
            {
                Title = offA
            };
            LeafButton OffB1 = new LeafButton(LeafID.THPartyTal, LeafID.IndicatorB1)
            {
                Title = offA
            };

            LeafButton OffA2 = new LeafButton(LeafID.THPartyStr, LeafID.IndicatorA2)
            {
                Title = offA
            };
            LeafButton OffB2 = new LeafButton(LeafID.THPartyEvade, LeafID.IndicatorB2)
            {
                Title = offA
            };

            LeafButton OffA3 = new LeafButton(LeafID.THPartyFocus, LeafID.IndicatorA3)
            {
                Title = offA
            };
            LeafButton OffB3 = new LeafButton(LeafID.THPartyAwr, LeafID.IndicatorB3)
            {
                Title = offA
            };

            LeafButton GoldMult = new LeafButton(LeafID.THGoldMult, LeafID.IndicatorA4)
            {
                Title = goldMult
            };
            LeafButton PtyConfuse = new LeafButton(LeafID.THPartyConfuse, LeafID.IndicatorB4)
            {
                Title = ptyConfuse
            };

            LeafButton SpecialAction = new LeafButton(LeafID.THSpecialAction, LeafID.IndicatorA5)
            {
                Title = prepared
            };
            LeafButton Luck = new LeafButton(LeafID.THPartyLuck, LeafID.IndicatorB5)
            {
                Title = luck
            };

            LeafButton PtyFire = new LeafButton(LeafID.THPartyFire, LeafID.IndicatorA6)
            {
                Title = fire
            };
            LeafButton PtyBleed = new LeafButton(LeafID.WoodCutterTable, LeafID.IndicatorB6)
            {
                Title = bleed
            };


            Branch Branch1 = new Branch(new List<LeafButton>() { OffA1, OffB1 })
            {
                MileStone = new LevelMilestone(4)
            };
            Branch Branch2 = new Branch(new List<LeafButton>() { GoldMult, PtyConfuse })
            {
                MileStone = new LevelMilestone(5)
            };
            Branch Branch3 = new Branch(new List<LeafButton>() { OffA2, OffB2 })
            {
                MileStone = new LevelMilestone(6)
            };
            Branch Branch4 = new Branch(new List<LeafButton>() { SpecialAction, Luck })
            {
                MileStone = new LevelMilestone(7)
            };
            Branch Branch5 = new Branch(new List<LeafButton>() { OffA3, OffB3 })
            {
                MileStone = new LevelMilestone(8)
            };
            Branch Branch6 = new Branch(new List<LeafButton>() { PtyFire, PtyBleed })
            {
                MileStone = new LevelMilestone(9)
            };


            List<Branch> branches = new List<Branch> { Branch1, Branch2, Branch3, Branch4, Branch5, Branch6 };
            branches.Reverse();
            Branches = branches;
        }
    }
}
