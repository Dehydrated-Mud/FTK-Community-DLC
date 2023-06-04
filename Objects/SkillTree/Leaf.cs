using FTKAPI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityDLC.Objects.Modifiers;
namespace CommunityDLC.Objects.SkillTree
{
    public class Leaf : DLCCustomModifier
    { // A leaf is a custom modifier that belongs on a tree. 
      // It carries a buttonState, which tells us whether the leaf indicates a modifier that is available to choose or a modifier that has already 
      // been chosen.
        public Leaf(LeafID id)
        {
            LID = id;
            ID = id.ToString();
        }
        public enum LeafID
        {
            None = 0,
            IndicatorA1,
            IndicatorA2,
            IndicatorA3,
            IndicatorA4,
            IndicatorA5,
            IndicatorA6,
            IndicatorA7,
            IndicatorA8,
            IndicatorA9,
            IndicatorA10,
            IndicatorA11,
            IndicatorA12,
            IndicatorA13,
            IndicatorA14,
            IndicatorB1,
            IndicatorB2,
            IndicatorB3,
            IndicatorB4,
            IndicatorB5,
            IndicatorB6,
            IndicatorB7,
            IndicatorB8,
            IndicatorB9,
            IndicatorB10,
            IndicatorB11,
            IndicatorB12,
            IndicatorB13,
            IndicatorB14,
            WoodCutterOffa1,
            WoodCutterOffa2,
            WoodCutterOffa3,
            WoodCutterOffb1,
            WoodCutterOffb2,
            WoodCutterOffb3,
            WoodCutterFocus1,
            WoodCutterFocus2,
            WoodCutterLute1,
            WoodCutterLute2,
            WoodCutterVindication,
            WoodCutterDoubleEdge,
            WoodCutterOak,
            WoodCutterCamper,
            WoodCutterDen,
            WoodCutterTable,
            WoodCutterJustice,
            MonkOffa1,
            MonkOffa2,
            MonkOffa3,
            MonkOffb1,
            MonkOffb2,
            MonkOffb3,
            MonkCombatMeditate,
            MonkUnarmedDmg1,
            MonkUnarmedDmg2,
            MonkTrainer,
            MonkGroupMeditate,
            MonkDisciplineUpgrade,
            MonkDisciplineUpgrade2,
            MonkRefocusChance,
            MonkPrimordialOak,
            ScholarOffA1,
            ScholarOffA2,
            ScholarOffA3,
            ScholarOffB1,
            ScholarOffB2,
            ScholarOffB3,
            ScholarMageFire,
            ScholarMageWater,
            ScholarMageLightning,
            ScholarMageIce,
            ScholarXPTome,
            ScholarDMGWand,
            ScholarPartyStaff,
            ScholarBloodRush,
            ScholarWandRefocus,
            THPartyInt,
            THPartyStr,
            THPartyTal,
            THPartyEvade,
            THPartyFocus,
            THPartyAwr,
            THPartyConfuse,
            //THPartyBlood, use woodcuttertable
            THPartyFire,
            THPartyLuck,
            THSpecialAction,
            THGoldMult,
            BlackSmithOffA1,
            BlackSmithOffA2,
            BlackSmithOffA3,
            BlackSmithOffB1,
            BlackSmithOffB2,
            BlackSmithOffB3,
            BlackSmithRebuttal,
            BlackSmithRetaliate,
            BlackSmithSteady,
            BlackSmithBFTrauma,
            BlackSmithCrush,
            BlackSmithLeatherBack,
            HoboOffA1,
            HoboOffA2,
            HoboOffA3,
            HoboOffB1,
            HoboOffB2,
            HoboOffB3,
            HoboFindShelter,
            HoboFouldStench,
            HoboFindSpirit,
            HoboRumsTheWord,
            HoboSnickerDoodle,
            HunterOffA1,
            HunterOffA2,
            HunterOffA3,
            HunterOffB1,
            HunterOffB2,
            HunterOffB3,
            HunterCalledShot,
            HunterCalledShot2,
            HunterCalledRush,
            HunterBow,
            HunterGun,
            HunterBonusXP,
            HunterBonusGold,
            Test,
            Test2
        }
        LeafID m_LeafID = LeafID.None;
        buttonState m_State = buttonState.Active;
        public buttonState State { get => m_State; set => m_State = value; }
        public LeafID LID { get => m_LeafID; set => m_LeafID = value; }
        private bool onadd = false;
        public bool OnAdd { get => onadd; set => onadd = value; }

        public virtual void AddAction(CharacterOverworld player)
        {

        }
    }
}
