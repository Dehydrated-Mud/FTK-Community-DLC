using GridEditor;
using FTKAPI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.CustomSkills
{
    public class DivineInterventionInfo : CustomSkill
    {
        public DivineInterventionInfo() 
        {
            ID = "DivineIntervention";
            BaseSkill = FTK_characterSkill.ID.Discipline;
            HudDisplay = new CustomLocalizedString("Divine Intervention");
        }
    }
}
