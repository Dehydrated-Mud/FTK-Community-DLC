using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.SkillTree.Leaves
{
    public class TestLeaf : Leaf
    {
        public TestLeaf(LeafID id) : base(id)
        {
            Intelligence = 0.01f;
            State = buttonState.Active;
        }
    }
    public class TestLeaf2 : Leaf
    {
        public TestLeaf2(LeafID id) : base(id)
        {
            Strength = 0.01f;
            State = buttonState.Active;
        }
    }

    /*public class TestLeafIndicator : Leaf
    {
        public TestLeafIndicator()
        {
            LID = LeafID.TestIndicator;
            ID = "MyFirstTestLeafIndicator";
            State = buttonState.Unlocked;
        }
    }



    public class TestLeafIndicator2 : Leaf
    {
        public TestLeafIndicator2()
        {
            LID = LeafID.TestIndicator2;
            ID = "MySecondTestLeafIndicator";
            State = buttonState.Unlocked;
        }
    }*/
}
