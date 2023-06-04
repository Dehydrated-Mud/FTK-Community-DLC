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
    public class MonkTree : ModifierTree
    {
        private string offA = "Zen";
        private string offB = "Endurance";
        private string handDmg = "Martial Arts Master";
        private string combatMeditate = "Combat Meditation";
        private string groupMeditate = "Group Meditation";
        private string refocus = "Way of the Mind";
        private string oak = "Sacred Ground";
        private string trainer = "One Master to Another";
        private string disc = "Teach by Example";

        //CustomLocalizedString stoneTable = new CustomLocalizedString("Sacrifice at the Stone Table");

        public MonkTree() 
        {
            LeafButton OffA1 = new LeafButton(LeafID.MonkOffa1, LeafID.IndicatorA1)
            {
                Title = new CustomLocalizedString(offA)
            };
            LeafButton OffB1 = new LeafButton(LeafID.MonkOffb1, LeafID.IndicatorB1)
            {
                Title = new CustomLocalizedString(offB)
            };

            LeafButton OffA2 = new LeafButton(LeafID.MonkOffa2, LeafID.IndicatorA2)
            {
                Title = new CustomLocalizedString(offA)
            };
            LeafButton OffB2 = new LeafButton(LeafID.MonkOffb2, LeafID.IndicatorB2)
            {
                Title = new CustomLocalizedString(offB)
            };

            LeafButton OffA3 = new LeafButton(LeafID.MonkOffa3, LeafID.IndicatorA3)
            {
                Title = new CustomLocalizedString(offA)
            };
            LeafButton OffB3 = new LeafButton(LeafID.MonkOffb3, LeafID.IndicatorB3)
            {
                Title = new CustomLocalizedString(offB)
            };

            LeafButton CombatMeditate = new LeafButton(LeafID.MonkCombatMeditate, LeafID.IndicatorB4)
            {
                Title = new CustomLocalizedString(combatMeditate)
            };
            LeafButton Master1 = new LeafButton(LeafID.MonkUnarmedDmg1, LeafID.IndicatorA4)
            {
                Title = new CustomLocalizedString(handDmg)
            };

            LeafButton GroupMeditation = new LeafButton(LeafID.MonkGroupMeditate, LeafID.IndicatorA5)
            {
                Title = new CustomLocalizedString(groupMeditate)
            };
            LeafButton Master2 = new LeafButton(LeafID.MonkUnarmedDmg2, LeafID.IndicatorB5)
            {
                Title = new CustomLocalizedString(handDmg)
            };
            LeafButton Disc1 = new LeafButton(LeafID.MonkDisciplineUpgrade, LeafID.IndicatorA8)
            {
                Title = new CustomLocalizedString(disc)
            };

            LeafButton Refocus = new LeafButton(LeafID.MonkRefocusChance, LeafID.IndicatorA6)
            {
                Title = new CustomLocalizedString(refocus)
            };
            LeafButton Disc2 = new LeafButton(LeafID.MonkDisciplineUpgrade2, LeafID.IndicatorB6)
            {
                Title = new CustomLocalizedString(disc)
            };

            LeafButton Trainer = new LeafButton(LeafID.MonkTrainer)
            {
                Title = new CustomLocalizedString(trainer)
            };
            LeafButton Oak = new LeafButton(LeafID.MonkPrimordialOak)
            {
                Title = new CustomLocalizedString(oak)
            };

            Branch Branch1 = new Branch(new List<LeafButton>() { OffA1, OffB1 })
            {
                MileStone = new LevelMilestone(4)
            };
            Branch Branch2 = new Branch(new List<LeafButton>() { Master1, CombatMeditate })
            {
                MileStone = new LevelMilestone(5)
            };
            Branch Branch3 = new Branch(new List<LeafButton>() { OffA2, OffB2 })
            {
                MileStone = new LevelMilestone(6)
            };
            Branch Branch4 = new Branch(new List<LeafButton>() { Master2, Disc1, GroupMeditation })
            {
                MileStone = new LevelMilestone(7)
            };
            Branch Branch5 = new Branch(new List<LeafButton>() { OffA3, OffB3 })
            {
                MileStone = new LevelMilestone(8)
            };
            Branch Branch6 = new Branch(new List<LeafButton>() { Disc2, Refocus})
            {
                MileStone = new LevelMilestone(9)
            };

            Branch BranchTrainer = new Branch(Trainer)
            {
                MileStone = new EncounterMilestone(FTK_miniEncounter.ID.Trainer)
            };

            Branch BranchOak = new Branch(Oak)
            {
                MileStone = new EncounterMilestone(FTK_miniEncounter.ID.PrimordialOak)
                /*MileStone = new MileStoneContainer
                {
                    Enemies = new List<FTK_enemyCombat.ID> { FTK_enemyCombat.ID.beastmanA, FTK_enemyCombat.ID.beastmanB, FTK_enemyCombat.ID.beastmanC, FTK_enemyCombat.ID.beastmanD },
                    AllEnemies = false
                }*/
            };

            List<Branch> branches = new List<Branch> { Branch1, Branch2, Branch3, Branch4, Branch5, Branch6, BranchTrainer, BranchOak};
            branches.Reverse();
            Branches = branches;
        }
    }
}
