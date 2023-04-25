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
    public class WoodCutterTree : ModifierTree
    {
        private string offA = "Avid Hobyist";
        private string offB = "Fortitude";
        private string luteDmg = "Enthusiasm over Skill";
        private string doubleEdge = "Double Edge";
        private string vindication = "Vindication";
        private string focus = "Anger Management";
        private string table = "Noble Sacrifice";
        private string den = "Stage Fright";
        private string camper = "Lessons from a Friend";
        CustomLocalizedString stoneTable = new CustomLocalizedString("Sacrifice at the Stone Table");

        public WoodCutterTree() 
        {
            LeafButton OffA1 = new LeafButton(LeafID.WoodCutterOffa1, LeafID.IndicatorA1)
            {
                Title = new CustomLocalizedString(offA)
            };
            LeafButton OffB1 = new LeafButton(LeafID.WoodCutterOffb1, LeafID.IndicatorB1)
            {
                Title = new CustomLocalizedString(offB)
            };

            LeafButton OffA2 = new LeafButton(LeafID.WoodCutterOffa2, LeafID.IndicatorA2)
            {
                Title = new CustomLocalizedString(offA)
            };
            LeafButton OffB2 = new LeafButton(LeafID.WoodCutterOffb2, LeafID.IndicatorB2)
            {
                Title = new CustomLocalizedString(offB)
            };

            LeafButton OffA3 = new LeafButton(LeafID.WoodCutterOffa3, LeafID.IndicatorA3)
            {
                Title = new CustomLocalizedString(offA)
            };
            LeafButton OffB3 = new LeafButton(LeafID.WoodCutterOffb3, LeafID.IndicatorB3)
            {
                Title = new CustomLocalizedString(offB)
            };

            LeafButton LuteDmg1 = new LeafButton(LeafID.WoodCutterLute1, LeafID.IndicatorA4)
            {
                Title = new CustomLocalizedString(luteDmg)
            };
            LeafButton MaxFocus1 = new LeafButton(LeafID.WoodCutterFocus1, LeafID.IndicatorB4)
            {
                Title = new CustomLocalizedString(focus)
            };

            LeafButton Vindication = new LeafButton(LeafID.WoodCutterVindication, LeafID.IndicatorA5)
            {
                Title = new CustomLocalizedString(vindication)
            };
            LeafButton MaxFocus2 = new LeafButton(LeafID.WoodCutterFocus2, LeafID.IndicatorB5)
            {
                Title = new CustomLocalizedString(focus)
            };

            LeafButton LuteDmg2 = new LeafButton(LeafID.WoodCutterLute2, LeafID.IndicatorA6)
            {
                Title = new CustomLocalizedString(luteDmg)
            };
            LeafButton DoubleEdge = new LeafButton(LeafID.WoodCutterDoubleEdge, LeafID.IndicatorB6)
            {
                Title = new CustomLocalizedString(doubleEdge)
            };

            LeafButton Gambler = new LeafButton(LeafID.WoodCutterDen)
            {
                Title = new CustomLocalizedString(den)
            };
            LeafButton StoneTable = new LeafButton(LeafID.WoodCutterTable)
            {
                Title = new CustomLocalizedString(table)
            };
            LeafButton Camper = new LeafButton(LeafID.WoodCutterCamper)
            {
                Title = new CustomLocalizedString(camper)
            };

            Branch Branch1 = new Branch(new List<LeafButton>() { OffA1, OffB1 })
            {
                MileStone = new LevelMilestone(4)
            };
            Branch Branch2 = new Branch(new List<LeafButton>() {LuteDmg1, MaxFocus1 })
            {
                MileStone = new LevelMilestone(5)
            };
            Branch Branch3 = new Branch(new List<LeafButton>() { OffA2, OffB2 })
            {
                MileStone = new LevelMilestone(6)
            };
            Branch Branch4 = new Branch(new List<LeafButton>() { Vindication, MaxFocus2 })
            {
                MileStone = new LevelMilestone(7)
            };
            Branch Branch5 = new Branch(new List<LeafButton>() { OffA3, OffB3 })
            {
                MileStone = new LevelMilestone(8)
            };
            Branch Branch6 = new Branch(new List<LeafButton>() { LuteDmg2, DoubleEdge })
            {
                MileStone = new LevelMilestone(9)
            };

            Branch BranchDen = new Branch(Gambler)
            {
                MileStone = new EncounterMilestone(FTK_miniEncounter.ID.GamblingDen)
            };

            Branch BranchTable = new Branch(StoneTable)
            {
                MileStone = new EncounterMilestone(FTK_miniEncounter.ID.BloodRitual)
            };

            Branch BranchCamper = new Branch(Camper)
            {
                MileStone = new EncounterMilestone(FTK_miniEncounter.ID.FriendlyCamper)
            };

            List<Branch> branches = new List<Branch> { Branch1, Branch2, Branch3, Branch4, Branch5, Branch6, BranchDen, BranchTable, BranchCamper };
            branches.Reverse();
            Branches = branches;
        }

    }
}
