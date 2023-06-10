using CommunityDLC.Objects.Modifiers;
using FTKAPI.Managers;
using FTKAPI.Objects;
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
            // Ensure that other modifiers are added first
            InitializeBasicStatMods.Init();
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
                dict[(LeafID)i] = AddMod(new GenericIndicator((LeafID)i));
            }
            dict[LeafID.WoodCutterFocus1] = AddMod(new GenericMaxFocus(LeafID.WoodCutterFocus1, 1));
            dict[LeafID.WoodCutterFocus2] = AddMod(new GenericMaxFocus(LeafID.WoodCutterFocus2, 2));
            dict[LeafID.WoodCutterOffa1] = AddMod(new GenericTalent(LeafID.WoodCutterOffa1, 0.05f));
            dict[LeafID.WoodCutterOffa2] = AddMod(new GenericTalent(LeafID.WoodCutterOffa2, 0.05f));
            dict[LeafID.WoodCutterOffa3] = AddMod(new GenericTalent(LeafID.WoodCutterOffa3, 0.05f));
            dict[LeafID.WoodCutterOffb1] = AddMod(new ModWoodCutterIntVit(LeafID.WoodCutterOffb1));
            dict[LeafID.WoodCutterOffb2] = AddMod(new ModWoodCutterIntVit(LeafID.WoodCutterOffb2));
            dict[LeafID.WoodCutterOffb3] = AddMod(new ModWoodCutterIntVit(LeafID.WoodCutterOffb3));
            dict[LeafID.WoodCutterDen] = AddMod(new GenericTalent(LeafID.WoodCutterDen, 0.06f));
            dict[LeafID.WoodCutterCamper] = AddMod(new ModFindPouch(LeafID.WoodCutterCamper));
            dict[LeafID.WoodCutterTable] = AddMod(new ModBleedImmunity(LeafID.WoodCutterTable));
            dict[LeafID.WoodCutterJustice] = AddMod(new ModJustice(LeafID.WoodCutterJustice)); //unused
            dict[LeafID.WoodCutterVindication] = AddMod(new ModVindication(LeafID.WoodCutterVindication));
            dict[LeafID.WoodCutterDoubleEdge] = AddMod(new ModDoubleEdge(LeafID.WoodCutterDoubleEdge));
            dict[LeafID.WoodCutterLute1] = AddMod(new WoodCutterLute(LeafID.WoodCutterLute1));
            dict[LeafID.WoodCutterLute2] = AddMod(new WoodCutterLute(LeafID.WoodCutterLute2));

            dict[LeafID.MonkOffa1] = AddMod(new GenericAwareness(LeafID.MonkOffa1, 0.06f));
            dict[LeafID.MonkOffa2] = AddMod(new GenericAwareness(LeafID.MonkOffa2, 0.06f));
            dict[LeafID.MonkOffa3] = AddMod(new GenericAwareness(LeafID.MonkOffa3, 0.06f));
            dict[LeafID.MonkOffb1] = AddMod(new GenericVitality(LeafID.MonkOffb1, 0.03f));
            dict[LeafID.MonkOffb2] = AddMod(new GenericVitality(LeafID.MonkOffb2, 0.03f));
            dict[LeafID.MonkOffb3] = AddMod(new GenericVitality(LeafID.MonkOffb3, 0.03f));
            dict[LeafID.MonkUnarmedDmg1] = AddMod(new MonkUnarmedDmg(LeafID.MonkUnarmedDmg1, 1f));
            dict[LeafID.MonkUnarmedDmg2] = AddMod(new MonkUnarmedDmg(LeafID.MonkUnarmedDmg2, 2f));
            dict[LeafID.MonkCombatMeditate] = AddMod(new MonkCombatMeditate(LeafID.MonkCombatMeditate));
            dict[LeafID.MonkGroupMeditate] = AddMod(new MonkGroupMeditation(LeafID.MonkGroupMeditate));
            dict[LeafID.MonkDisciplineUpgrade] = AddMod(new MonkDisciplineThreshold(LeafID.MonkDisciplineUpgrade, 4));
            dict[LeafID.MonkDisciplineUpgrade2] = AddMod(new MonkDisciplineThreshold(LeafID.MonkDisciplineUpgrade2, 1));
            dict[LeafID.MonkRefocusChance] = AddMod(new GenericRefocusChance(LeafID.MonkRefocusChance, 0.15f));
            dict[LeafID.MonkPrimordialOak] = AddMod(new GenericGiveItem(LeafID.MonkPrimordialOak, FTK_itembase.ID.staffGoatWizard));
            dict[LeafID.MonkTrainer] = AddMod(new MonkEvade(LeafID.MonkTrainer, 0.1f));

            dict[LeafID.ScholarOffA1] = AddMod(new ScholarOffA(LeafID.ScholarOffA1));
            dict[LeafID.ScholarOffA2] = AddMod(new ScholarOffA(LeafID.ScholarOffA2));
            dict[LeafID.ScholarOffA3] = AddMod(new ScholarOffA(LeafID.ScholarOffA3));
            dict[LeafID.ScholarOffB1] = AddMod(new ScholarOffB(LeafID.ScholarOffB1));
            dict[LeafID.ScholarOffB2] = AddMod(new ScholarOffB(LeafID.ScholarOffB2));
            dict[LeafID.ScholarOffB3] = AddMod(new ScholarOffB(LeafID.ScholarOffB3));
            dict[LeafID.ScholarXPTome] = AddMod(new XPWithTome(LeafID.ScholarXPTome));
            dict[LeafID.ScholarDMGWand] = AddMod(new ScholarWandDamage(LeafID.ScholarDMGWand, 2));
            dict[LeafID.ScholarPartyStaff] = AddMod(new ScholarStaffDef(LeafID.ScholarPartyStaff));
            dict[LeafID.ScholarMageFire] = AddMod(new MageChoiceFire(LeafID.ScholarMageFire));
            dict[LeafID.ScholarMageWater] = AddMod(new MageChoiceWater(LeafID.ScholarMageWater));
            dict[LeafID.ScholarMageLightning] = AddMod(new MageChoiceLightning(LeafID.ScholarMageLightning));
            dict[LeafID.ScholarMageIce] = AddMod(new MageChoiceIce(LeafID.ScholarMageIce));
            dict[LeafID.ScholarWandRefocus] = AddMod(new WandFocus(LeafID.ScholarWandRefocus));
            dict[LeafID.ScholarBloodRush] = AddMod(new BloodRushLeaf(LeafID.ScholarBloodRush));

            dict[LeafID.THPartyInt] = AddMod(new GenericPermanentAura(LeafID.THPartyInt, "GenericIntelligence02"));
            dict[LeafID.THPartyStr] = AddMod(new GenericPermanentAura(LeafID.THPartyStr, "GenericStrength02"));
            dict[LeafID.THPartyAwr] = AddMod(new GenericPermanentAura(LeafID.THPartyAwr, "GenericAwareness04"));
            dict[LeafID.THPartyLuck] = AddMod(new GenericPermanentAura(LeafID.THPartyLuck, "GenericLuck10"));
            dict[LeafID.THPartyTal] = AddMod(new GenericPermanentAura(LeafID.THPartyTal, "GenericTalent02"));
            dict[LeafID.THPartyEvade] = AddMod(new GenericPermanentAura(LeafID.THPartyEvade, "GenericEvasion02"));
            dict[LeafID.THPartyFocus] = AddMod(new GenericPermanentAura(LeafID.THPartyFocus, "GenericFocus01"));
            dict[LeafID.THPartyFire] = AddMod(new THFireImmunity(LeafID.THPartyFire));
            dict[LeafID.THPartyConfuse] = AddMod(new THConfuseImmunity(LeafID.THPartyConfuse));
            dict[LeafID.THGoldMult] = AddMod(new THGoldMult(LeafID.THGoldMult));
            dict[LeafID.THSpecialAction] = AddMod(new THAlwaysPrepared(LeafID.THSpecialAction));

            dict[LeafID.BlackSmithOffA1] = AddMod(new BlackSmithOffA(LeafID.BlackSmithOffA1));
            dict[LeafID.BlackSmithOffA2] = AddMod(new BlackSmithOffA(LeafID.BlackSmithOffA2));
            dict[LeafID.BlackSmithOffA3] = AddMod(new BlackSmithOffA(LeafID.BlackSmithOffA3));
            dict[LeafID.BlackSmithOffB1] = AddMod(new BlackSmithOffB(LeafID.BlackSmithOffB1));
            dict[LeafID.BlackSmithOffB2] = AddMod(new BlackSmithOffB(LeafID.BlackSmithOffB2));
            dict[LeafID.BlackSmithOffB3] = AddMod(new BlackSmithOffB(LeafID.BlackSmithOffB3));
            dict[LeafID.BlackSmithBFTrauma] = AddMod(new BluntTrauma(LeafID.BlackSmithBFTrauma));
            dict[LeafID.BlackSmithRebuttal] = AddMod(new Rebuttal(LeafID.BlackSmithRebuttal));
            dict[LeafID.BlackSmithRetaliate] = AddMod(new Retaliation(LeafID.BlackSmithRetaliate));
            dict[LeafID.BlackSmithSteady] = AddMod(new IncreasedSteady(LeafID.BlackSmithSteady));
            dict[LeafID.BlackSmithCrush] = AddMod(new CrushingBlow(LeafID.BlackSmithCrush));
            dict[LeafID.BlackSmithLeatherBack] = AddMod(new LeatherBack(LeafID.BlackSmithLeatherBack));

            dict[LeafID.HoboFindSpirit] = AddMod(new FindDrink(LeafID.HoboFindSpirit));
            dict[LeafID.HoboSnickerDoodle] = AddMod(new FindFood(LeafID.HoboSnickerDoodle));
            dict[LeafID.HoboRumsTheWord] = AddMod(new HoboRum(LeafID.HoboRumsTheWord));
            dict[LeafID.HoboFindShelter] = AddMod(new HoboNook(LeafID.HoboFindShelter));
            dict[LeafID.HoboFouldStench] = AddMod(new WhatsThatSmell(LeafID.HoboFouldStench));

            dict[LeafID.HunterOffA1] = AddMod(new HunterOffA(LeafID.HunterOffA1));
            dict[LeafID.HunterOffA2] = AddMod(new HunterOffA(LeafID.HunterOffA2));
            dict[LeafID.HunterOffA3] = AddMod(new HunterOffA(LeafID.HunterOffA3));
            dict[LeafID.HunterOffB1] = AddMod(new HunterOffB(LeafID.HunterOffB1));
            dict[LeafID.HunterOffB2] = AddMod(new HunterOffB(LeafID.HunterOffB2));
            dict[LeafID.HunterOffB3] = AddMod(new HunterOffB(LeafID.HunterOffB3));
            dict[LeafID.HunterCalledRush] = AddMod(new ChainShot(LeafID.HunterCalledRush));
            dict[LeafID.HunterCalledShot] = AddMod(new CalledShotChance(LeafID.HunterCalledShot));
            //dict[LeafID.HunterCalledShot2] = AddMod(new CalledShotChance(LeafID.HunterCalledShot2));
            dict[LeafID.HunterBonusGold] = AddMod(new GoldForBeasts(LeafID.HunterBonusGold));
            dict[LeafID.HunterBonusXP] = AddMod(new XPForBeasts(LeafID.HunterBonusXP));
            dict[LeafID.HunterGun] = AddMod(new GunDMG(LeafID.HunterGun, 0.1f));
            dict[LeafID.HunterBow] = AddMod(new BowDMG(LeafID.HunterBow, 0.1f));

            dict[LeafID.GladiatorOffA1] = AddMod(new GenericVitality(LeafID.GladiatorOffA1, 0.02f));
            dict[LeafID.GladiatorOffA2] = AddMod(new GenericVitality(LeafID.GladiatorOffA2, 0.02f));
            dict[LeafID.GladiatorOffA3] = AddMod(new GenericVitality(LeafID.GladiatorOffA3, 0.02f));
            dict[LeafID.GladiatorOffB1] = AddMod(new GenericTalent(LeafID.GladiatorOffB1, 0.03f));
            dict[LeafID.GladiatorOffB2] = AddMod(new GenericTalent(LeafID.GladiatorOffB2, 0.03f));
            dict[LeafID.GladiatorOffB3] = AddMod(new GenericTalent(LeafID.GladiatorOffB3, 0.03f));
            dict[LeafID.GladiatorLifesteal] = AddMod(new GladiatorLifesteal(LeafID.GladiatorLifesteal));
            dict[LeafID.GladiatorBerserk] = AddMod(new GladiatorBerserk(LeafID.GladiatorBerserk));
            dict[LeafID.GladiatorLifeDrain] = AddMod(new GladiatorLifeDrain(LeafID.GladiatorLifeDrain));
            dict[LeafID.GladiatorCritical] = AddMod(new GladiatorCrit(LeafID.GladiatorCritical));
            dict[LeafID.GladiatorBloodLust] = AddMod(new GladiatorLust(LeafID.GladiatorBloodLust));
            dict[LeafID.GladiatorDirtyTactics] = AddMod(new GladiatorDirty(LeafID.GladiatorDirtyTactics));
            return dict;
        }

        private static FTK_characterModifier.ID AddMod(CustomModifier mod)
        {
            return (FTK_characterModifier.ID)ModifierManager.AddModifier(mod);
        }
    }
}
