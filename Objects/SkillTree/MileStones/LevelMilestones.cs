using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTKAPI.Objects;

namespace CommunityDLC.Objects.SkillTree.MileStones
{
    internal class LevelMilestone : MileStoneContainer
    {
        public LevelMilestone(int lvl, string flair = null) 
        {
            Level = lvl;
            CustomLocalizedString locale = new CustomLocalizedString("Reach Level");
            Milestone = locale.GetLocalizedString() + " " +lvl;
        }
    }
}
