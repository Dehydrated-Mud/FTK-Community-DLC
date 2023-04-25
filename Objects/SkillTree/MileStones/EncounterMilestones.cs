using GridEditor;
using Mono.CompilerServices.SymbolWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTKAPI.Objects;
using HutongGames.PlayMaker.Actions;
using Google2u;

namespace CommunityDLC.Objects.SkillTree.MileStones
{
    public class EncounterMilestone : MileStoneContainer
    {
        public EncounterMilestone(string id) 
        {
            FTK_miniEncounter.ID myID = FTK_miniEncounter.GetEnum(id);
            Encounter = myID;
            FTK_miniEncounter encounter = FTK_miniEncounterDB.Get(myID);
            CustomLocalizedString locale = new CustomLocalizedString("Pass the " + encounter.GetDisplayName() + " Encounter");
            Milestone = locale.GetLocalizedString();
        }
        public EncounterMilestone(FTK_miniEncounter.ID id)
        {
            Encounter = id;
            FTK_miniEncounter encounter = FTK_miniEncounterDB.Get(id);
            CustomLocalizedString locale = new CustomLocalizedString("Pass the " + encounter.GetDisplayName() + " Encounter");
            Milestone = locale.GetLocalizedString();
        }
    }
}
