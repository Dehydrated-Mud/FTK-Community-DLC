using CommunityDLC.Objects.SkillTree.MileStones;
using FTKAPI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CommunityDLC.Objects.SkillTree.Leaf;

namespace CommunityDLC.Objects.SkillTree.Trees
{
    internal class HoboTree : ModifierTree
    {

        private CustomLocalizedString drinkThat = new("Are you gonna drink that?");
        private CustomLocalizedString eatThat = new("Are you gonna eat that?");

        private CustomLocalizedString rum = new("Rum's the word!");
        private CustomLocalizedString shelter = new("Hidey-Hole");

        private CustomLocalizedString smell = new("What's that smell?");
        private CustomLocalizedString prepared = new("Versatile");

        public HoboTree()
        {
            LeafButton Drink = new LeafButton(LeafID.HoboFindSpirit, LeafID.IndicatorA4)
            {
                Title = drinkThat
            };
            LeafButton Eat = new LeafButton(LeafID.HoboSnickerDoodle, LeafID.IndicatorB4)
            {
                Title = eatThat
            };

            LeafButton Rum = new LeafButton(LeafID.HoboRumsTheWord, LeafID.IndicatorA5)
            {
                Title = rum
            };
            LeafButton Nook = new LeafButton(LeafID.HoboFindShelter, LeafID.IndicatorB5)
            {
                Title = shelter
            };

            LeafButton Smell = new LeafButton(LeafID.HoboFouldStench, LeafID.IndicatorA6)
            {
                Title = smell
            };
            LeafButton Prepared = new LeafButton(LeafID.THSpecialAction, LeafID.IndicatorB6)
            {
                Title = prepared
            };


            Branch Branch2 = new Branch(new List<LeafButton>() { Drink, Eat })
            {
                MileStone = new LevelMilestone(4)
            };

            Branch Branch4 = new Branch(new List<LeafButton>() { Rum, Nook })
            {
                MileStone = new LevelMilestone(6)
            };

            Branch Branch6 = new Branch(new List<LeafButton>() { Smell, Prepared })
            {
                MileStone = new LevelMilestone(8)
            };


            List<Branch> branches = new List<Branch> { Branch2, Branch4, Branch6 };
            branches.Reverse();
            Branches = branches;
        }
    }
}
