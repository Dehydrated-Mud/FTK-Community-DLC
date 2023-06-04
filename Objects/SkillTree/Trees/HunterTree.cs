using CommunityDLC.Objects.SkillTree.MileStones;
using FTKAPI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CommunityDLC.Objects.SkillTree.Leaf;

namespace CommunityDLC.Objects.SkillTree.Trees
{
    internal class HunterTree : ModifierTree
    {
        private CustomLocalizedString offA = new("Coordination");
        private CustomLocalizedString offB = new("Coordination");

        private CustomLocalizedString gold = new("Hunter's Creed");
        private CustomLocalizedString xp = new("Call of the Hunter");

        private CustomLocalizedString shot = new("Deadeye");
        private CustomLocalizedString rush = new("Extra attack on Called Shot");

        private CustomLocalizedString gun = new("Firearms Expert");
        private CustomLocalizedString bow = new("It's tradition.");

        public HunterTree()
        {
            LeafButton OffA1 = new LeafButton(LeafID.HunterOffA1, LeafID.IndicatorA1)
            {
                Title = offA
            };
            LeafButton OffB1 = new LeafButton(LeafID.HunterOffB1, LeafID.IndicatorB1)
            {
                Title = offB
            };

            LeafButton OffA2 = new LeafButton(LeafID.HunterOffA2, LeafID.IndicatorA2)
            {
                Title = offA
            };
            LeafButton OffB2 = new LeafButton(LeafID.HunterOffB2, LeafID.IndicatorB2)
            {
                Title = offB
            };

            LeafButton OffA3 = new LeafButton(LeafID.HunterOffA3, LeafID.IndicatorA3)
            {
                Title = offA
            };
            LeafButton OffB3 = new LeafButton(LeafID.HunterOffB3, LeafID.IndicatorB3)
            {
                Title = offB
            };

            LeafButton Gold = new LeafButton(LeafID.HunterBonusGold, LeafID.IndicatorA4)
            {
                Title = gold
            };
            LeafButton XP = new LeafButton(LeafID.HunterBonusXP, LeafID.IndicatorB4)
            {
                Title = xp
            };

            LeafButton Shot = new LeafButton(LeafID.HunterCalledShot, LeafID.IndicatorA5)
            {
                Title = shot
            };
            LeafButton Rush = new LeafButton(LeafID.HunterCalledRush, LeafID.IndicatorB5)
            {
                Title = rush
            };

            LeafButton Bow = new LeafButton(LeafID.HunterBow, LeafID.IndicatorA6)
            {
                Title = bow
            };
            LeafButton Gun = new LeafButton(LeafID.HunterGun, LeafID.IndicatorB6)
            {
                Title = gun
            };
            /*LeafButton Shot2 = new LeafButton(LeafID.HunterCalledShot2, LeafID.IndicatorA7)
            {
                Title = shot
            };*/


            Branch Branch1 = new Branch(new List<LeafButton>() { OffA1, OffB1 })
            {
                MileStone = new LevelMilestone(4)
            };
            Branch Branch2 = new Branch(new List<LeafButton>() { Gold, XP })
            {
                MileStone = new LevelMilestone(5)
            };
            Branch Branch3 = new Branch(new List<LeafButton>() { OffA2, OffB2 })
            {
                MileStone = new LevelMilestone(6)
            };
            Branch Branch4 = new Branch(new List<LeafButton>() { Shot, Rush })
            {
                MileStone = new LevelMilestone(7)
            };
            Branch Branch5 = new Branch(new List<LeafButton>() { OffA3, OffB3 })
            {
                MileStone = new LevelMilestone(8)
            };
            Branch Branch6 = new Branch(new List<LeafButton>() { Gun, Bow})
            {
                MileStone = new LevelMilestone(9)
            };


            List<Branch> branches = new List<Branch> { Branch1, Branch2, Branch3, Branch4, Branch5, Branch6 };
            branches.Reverse();
            Branches = branches;
        }
    }
}
