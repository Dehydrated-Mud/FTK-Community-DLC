using FTKAPI.Managers;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeafID = CommunityDLC.Objects.SkillTree.Leaf.LeafID;

namespace CommunityDLC.Objects.SkillTree.Leaves
{
    public class LeafInitializer
    {
        public static Dictionary<LeafID, FTK_characterModifier.ID> Initialize()
        { 
            // Adds our leaf as a modifier, and adds it to the lookup. This allows us to use LeafIDs instead of IDs (which are strings)
            // This way we can check that all the IDs exist at compile time rather than runtime.

            //There's gotta be a better way to do this...
            Dictionary<LeafID, FTK_characterModifier.ID> dict = new Dictionary<Leaf.LeafID, FTK_characterModifier.ID>();
            dict[LeafID.None] = FTK_characterModifier.ID.None;
            dict[LeafID.Test] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new TestLeaf(LeafID.Test));
            dict[LeafID.Test2] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new TestLeaf2(LeafID.Test2));
            /*dict[LeafID.TestIndicator] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new TestLeafIndicator());
            dict[LeafID.TestIndicator2] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new TestLeafIndicator2());*/
            for (int i = 1; i < 29; i++)
            {
                dict[(LeafID)i] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new GenericIndicator((LeafID)i));
            }
            dict[LeafID.WoodCutterFocus1] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new GenericMaxFocus(LeafID.WoodCutterFocus1, 1));
            dict[LeafID.WoodCutterFocus2] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new GenericMaxFocus(LeafID.WoodCutterFocus2, 2));
            dict[LeafID.WoodCutterOffa1] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new GenericTalent(LeafID.WoodCutterOffa1, 0.05f));
            dict[LeafID.WoodCutterOffa2] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new GenericTalent(LeafID.WoodCutterOffa2, 0.05f));
            dict[LeafID.WoodCutterOffa3] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new GenericTalent(LeafID.WoodCutterOffa3, 0.05f));
            dict[LeafID.WoodCutterOffb1] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new ModWoodCutterIntVit(LeafID.WoodCutterOffb1));
            dict[LeafID.WoodCutterOffb2] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new ModWoodCutterIntVit(LeafID.WoodCutterOffb2));
            dict[LeafID.WoodCutterOffb3] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new ModWoodCutterIntVit(LeafID.WoodCutterOffb3));
            dict[LeafID.WoodCutterDen] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new GenericTalent(LeafID.WoodCutterDen, 0.06f));
            dict[LeafID.WoodCutterCamper] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new ModFindPouch(LeafID.WoodCutterCamper));
            dict[LeafID.WoodCutterTable] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new ModBleedImmunity(LeafID.WoodCutterTable));
            dict[LeafID.WoodCutterJustice] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new ModJustice(LeafID.WoodCutterJustice)); //unused
            dict[LeafID.WoodCutterVindication] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new ModVindication(LeafID.WoodCutterVindication));
            dict[LeafID.WoodCutterDoubleEdge] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new ModDoubleEdge(LeafID.WoodCutterDoubleEdge));
            dict[LeafID.WoodCutterLute1] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new WoodCutterLute(LeafID.WoodCutterLute1));
            dict[LeafID.WoodCutterLute2] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new WoodCutterLute(LeafID.WoodCutterLute2));

            dict[LeafID.MonkOffa1] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new GenericAwareness(LeafID.MonkOffa1, 0.06f));
            dict[LeafID.MonkOffa2] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new GenericAwareness(LeafID.MonkOffa2, 0.06f));
            dict[LeafID.MonkOffa3] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new GenericAwareness(LeafID.MonkOffa3, 0.06f));
            dict[LeafID.MonkOffb1] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new GenericVitality(LeafID.MonkOffb1, 0.03f));
            dict[LeafID.MonkOffb2] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new GenericVitality(LeafID.MonkOffb2, 0.03f));
            dict[LeafID.MonkOffb3] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new GenericVitality(LeafID.MonkOffb3, 0.03f));
            dict[LeafID.MonkUnarmedDmg1] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new MonkUnarmedDmg(LeafID.MonkUnarmedDmg1, 1f));
            dict[LeafID.MonkUnarmedDmg2] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new MonkUnarmedDmg(LeafID.MonkUnarmedDmg2, 2f));
            dict[LeafID.MonkCombatMeditate] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new MonkCombatMeditate(LeafID.MonkCombatMeditate));
            dict[LeafID.MonkGroupMeditate] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new MonkGroupMeditation(LeafID.MonkGroupMeditate));
            dict[LeafID.MonkDisciplineUpgrade] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new MonkDisciplineThreshold(LeafID.MonkDisciplineUpgrade, 4));
            dict[LeafID.MonkDisciplineUpgrade2] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new MonkDisciplineThreshold(LeafID.MonkDisciplineUpgrade2, 1));
            dict[LeafID.MonkRefocusChance] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new GenericRefocusChance(LeafID.MonkRefocusChance, 0.15f));
            dict[LeafID.MonkPrimordialOak] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new GenericGiveItem(LeafID.MonkPrimordialOak, FTK_itembase.ID.staffGoatWizard));
            dict[LeafID.MonkTrainer] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new MonkEvade(LeafID.MonkTrainer, 0.1f));

            dict[LeafID.ScholarOffA1] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new ScholarOffA(LeafID.ScholarOffA1));
            dict[LeafID.ScholarOffA2] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new ScholarOffA(LeafID.ScholarOffA2));
            dict[LeafID.ScholarOffA3] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new ScholarOffA(LeafID.ScholarOffA3));
            dict[LeafID.ScholarOffB1] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new ScholarOffB(LeafID.ScholarOffB1));
            dict[LeafID.ScholarOffB2] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new ScholarOffB(LeafID.ScholarOffB2));
            dict[LeafID.ScholarOffB3] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new ScholarOffB(LeafID.ScholarOffB3));
            dict[LeafID.ScholarXPTome] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new XPWithTome(LeafID.ScholarXPTome));
            dict[LeafID.ScholarDMGWand] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new ScholarWandDamage(LeafID.ScholarDMGWand, 2));
            dict[LeafID.ScholarPartyStaff] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new ScholarStaffDef(LeafID.ScholarPartyStaff));
            dict[LeafID.ScholarMageFire] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new MageChoiceFire(LeafID.ScholarMageFire));
            dict[LeafID.ScholarMageWater] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new MageChoiceWater(LeafID.ScholarMageWater));
            dict[LeafID.ScholarMageLightning] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new MageChoiceLightning(LeafID.ScholarMageLightning));
            dict[LeafID.ScholarMageIce] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new MageChoiceIce(LeafID.ScholarMageIce));
            dict[LeafID.ScholarWandRefocus] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new WandFocus(LeafID.ScholarWandRefocus));
            dict[LeafID.ScholarBloodRush] = (FTK_characterModifier.ID)ModifierManager.AddModifier(new BloodRushLeaf(LeafID.ScholarBloodRush));
            return dict;
        }
    }
}
