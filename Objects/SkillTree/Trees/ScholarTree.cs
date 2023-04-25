using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeafID = CommunityDLC.Objects.SkillTree.Leaf.LeafID;
using CommunityDLC.Objects.SkillTree.MileStones;
using GridEditor;
using FTKAPI.Objects;

namespace CommunityDLC.Objects.SkillTree.Trees
{
    public class ScholarTree : ModifierTree
    {
        CustomLocalizedString offA = new CustomLocalizedString("Brains and Brawn");
        CustomLocalizedString offB = new CustomLocalizedString("Mind over Matter");
        CustomLocalizedString partyStaff = new CustomLocalizedString("Stalwart Ally");
        CustomLocalizedString wandDmg = new CustomLocalizedString("Nuker");
        CustomLocalizedString tomeXP = new CustomLocalizedString("Book Worm");
        CustomLocalizedString fireMage = new CustomLocalizedString("Fire Mage");
        CustomLocalizedString iceMage = new CustomLocalizedString("Ice Mage");
        CustomLocalizedString lightningMage = new CustomLocalizedString("Lightning Mage");
        CustomLocalizedString waterMage = new CustomLocalizedString("Water Mage");
        CustomLocalizedString bloodRush = new CustomLocalizedString("Blood Rush");
        CustomLocalizedString wandRefocus = new CustomLocalizedString("Ethereal Communion");

        //CustomLocalizedString stoneTable = ("Sacrifice at the Stone Table");

        public ScholarTree() 
        {
            LeafButton OffA1 = new LeafButton(LeafID.ScholarOffA1, LeafID.IndicatorA1)
            {
                Title = (offA)
            };
            LeafButton OffB1 = new LeafButton(LeafID.ScholarOffB1, LeafID.IndicatorB1)
            {
                Title = (offB)
            };

            LeafButton OffA2 = new LeafButton(LeafID.ScholarOffA2, LeafID.IndicatorA2)
            {
                Title = (offA)
            };
            LeafButton OffB2 = new LeafButton(LeafID.ScholarOffB2, LeafID.IndicatorB2)
            {
                Title = (offB)
            };

            LeafButton OffA3 = new LeafButton(LeafID.ScholarOffA3, LeafID.IndicatorA3)
            {
                Title = (offA)
            };
            LeafButton OffB3 = new LeafButton(LeafID.ScholarOffB3, LeafID.IndicatorB3)
            {
                Title = (offB)
            };

            LeafButton XPTome = new LeafButton(LeafID.ScholarXPTome, LeafID.IndicatorB4)
            {
                Title = tomeXP
            };
            LeafButton PartyStaff = new LeafButton(LeafID.ScholarPartyStaff, LeafID.IndicatorA4)
            {
                Title = partyStaff
            };
            LeafButton WandDMG = new LeafButton(LeafID.ScholarDMGWand, LeafID.IndicatorA7)
            {
                Title = wandDmg
            };

            LeafButton MageFire = new LeafButton(LeafID.ScholarMageFire, LeafID.IndicatorA5)
            {
                Title = fireMage
            };
            LeafButton MageIce = new LeafButton(LeafID.ScholarMageIce, LeafID.IndicatorB5)
            {
                Title = iceMage
            };
            LeafButton MageLightning = new LeafButton(LeafID.ScholarMageLightning, LeafID.IndicatorA8)
            {
                Title = lightningMage
            };
            LeafButton MageWater = new LeafButton(LeafID.ScholarMageWater, LeafID.IndicatorB8)
            {
                Title = waterMage
            };

            LeafButton BloodRush = new LeafButton(LeafID.ScholarBloodRush, LeafID.IndicatorA6)
            {
                Title = bloodRush
            };
            LeafButton WandRefocus = new LeafButton(LeafID.ScholarWandRefocus, LeafID.IndicatorB6)
            {
                Title = wandRefocus
            };

            Branch Branch1 = new Branch(new List<LeafButton>() { OffA1, OffB1 })
            {
                MileStone = new LevelMilestone(4)
            };
            Branch Branch2 = new Branch(new List<LeafButton>() {XPTome, PartyStaff, WandDMG })
            {
                MileStone = new LevelMilestone(5)
            };
            Branch Branch3 = new Branch(new List<LeafButton>() { OffA2, OffB2 })
            {
                MileStone = new LevelMilestone(6)
            };
            Branch Branch4 = new Branch(new List<LeafButton>() { MageFire, MageIce, MageLightning, MageWater })
            {
                MileStone = new LevelMilestone(7)
            };
            Branch Branch5 = new Branch(new List<LeafButton>() { OffA3, OffB3 })
            {
                MileStone = new LevelMilestone(8)
            };
            Branch Branch6 = new Branch(new List<LeafButton>() { BloodRush, WandRefocus})
            {
                MileStone = new LevelMilestone(9)
            };

            List<Branch> branches = new List<Branch> { Branch1, Branch2, Branch3, Branch4, Branch5, Branch6 };
            branches.Reverse();
            Branches = branches;
        }

    }
}
