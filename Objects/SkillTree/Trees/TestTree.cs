using CommunityDLC.Objects.SkillTree.MileStones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeafID = CommunityDLC.Objects.SkillTree.Leaf.LeafID;
namespace CommunityDLC.Objects.SkillTree.Trees
{
    public class TestTree : ModifierTree
    {
        public TestTree()
        {
            LeafButton testButton = new LeafButton(LeafID.Test);
            LeafButton testButton2 = new LeafButton(LeafID.Test2);
            Branch branchTest = new Branch(testButton)
            {
                MileStone = new LevelMilestone(1)
            };
            Branch branchTest2 = new Branch(testButton2)
            {
                MileStone = new LevelMilestone(2)
            };
            Branches = new List<Branch> { branchTest, branchTest2 };


        }
    }
    /*public class TestTree2 : ModifierTree
    {
        public TestTree2()
        {
            LeafButton testButton = new LeafButton(LeafID.Test, LeafID.TestIndicator);
            LeafButton testButton2 = new LeafButton(LeafID.Test2, LeafID.TestIndicator2);
            Branch branchTest = new Branch(new List<LeafButton> { testButton, testButton2 })
            {
                MileStone = new LevelMilestone(1)
            };
            Branch branchTest2 = new Branch(new List<LeafButton> { testButton, testButton2 })
            {
                MileStone = new LevelMilestone(2)
            };
            Branches = new List<Branch> { branchTest, branchTest2 };
        }
    }*/
}
