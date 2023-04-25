using BepInEx;
using CommunityDLC.Mechanics;
using CommunityDLC.Objects.Proficiencies;
using CommunityDLC.Objects.CharacterSkills;
using FTKAPI.Managers;
using FTKAPI.Objects;
using GridEditor;
using HarmonyLib;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Logger = FTKAPI.Utils.Logger;
using CommunityDLC.Objects.Sanctums;
using CommunityDLC.PhotonHooks;
using CommunityDLC.Objects.SkillTree;
using CommunityDLC.UIElements;
using CommunityDLC.Objects.SkillTree.HookPoints;
using CommunityDLC.Savegame;
//using CommunityDLC.Objects.ModifierTree;

namespace CommunityDLC
{
    using ProficiencyManager = FTKAPI.Managers.ProficiencyManager;
    [BepInPlugin("CommunityDLC", "CommunityDLC", "0.1.0")]
    [BepInDependency("FTKAPI")]
    public class CommunityDLC : BaseUnityPlugin
    {
        public static AssetBundle assetBundle;
        public static AssetBundle assetBundleSkins;
        public static AssetBundle assetBundleIcons;
        public static BaseUnityPlugin Instance;

        internal HookSetAttackDecision hookSetAttackDecision = new HookSetAttackDecision();
        internal HookInstantiate hookInstantiate = new HookInstantiate();
        internal FindHerbs hookFindHerbs = new FindHerbs();
        internal HookTauntProf hookTauntProf = new HookTauntProf();
        internal HookLevelUp hookLevelUp = new HookLevelUp();
        internal HookDoInstantiate hookDoInstantiate = new HookDoInstantiate();
        internal HookCharacterStatsSerialize hookStatsSerialize = new HookCharacterStatsSerialize();
        internal HookInventory hookInventory = new HookInventory();
        internal HookTallyCharacterMods hookTallyCharacterMods = new HookTallyCharacterMods();
        internal HookGetModDisplay hookGetModDisplay = new HookGetModDisplay();
        internal HookAddRemoveModifiers hookAddRemoveModifiers = new HookAddRemoveModifiers();
        internal MigratedSkills migratedSkills = new MigratedSkills();
        internal HookEncounters hookEncounters = new HookEncounters();
        internal SaveFilePath SaveFilePathhook = new ();
        
        private void Awake()
        {
            Instance = this;
            Logger.LogInfo($"Plugin {Info.Metadata.GUID} is loaded!");
            // When adding another asset bundle, be sure to edit FTKModLib.Example and add the ItemGroup flags for your bundle
            assetBundle = AssetManager.LoadAssetBundleFromResources("customitemsbundle", Assembly.GetExecutingAssembly());
            assetBundleSkins = AssetManager.LoadAssetBundleFromResources("customskinsbundle", Assembly.GetExecutingAssembly());
            assetBundleIcons = AssetManager.LoadAssetBundleFromResources("customiconsbundle", Assembly.GetExecutingAssembly());
            NetworkManager.RegisterNetObject("CommunityDLC.SkillSyncer",
                gameObject => gameObject.AddComponent<SkillSyncer>(),
                new()
            );
            Harmony harmony = new Harmony(Info.Metadata.GUID);
            harmony.PatchAll();
            hookSetAttackDecision.Initialize();
            hookFindHerbs.Initialize();
            hookInstantiate.Initialize();
            hookTauntProf.Initialize();
            hookLevelUp.Initialize();
            hookDoInstantiate.Initialize();
            hookStatsSerialize.Initialize();
            hookInventory.Initialize();
            hookTallyCharacterMods.Initialize();
            hookGetModDisplay.Initialize();
            hookAddRemoveModifiers.Initialize();
            migratedSkills.Initialize();
            hookEncounters.Initialize();
            //SaveFilePathhook.Initialize(); Does not work yet

            SkipIntro.Init();
        }

        class HarmonyPatches
        {
            /// <summary>
            /// Most calls to managers will most likely require to be called after TableManager.Initialize.
            /// So just make all your changes in this postfix patch unless you know what you're doing.
            /// </summary>
            [HarmonyPatch(typeof(TableManager), "Initialize")]
            class TableManager_Initialize_Patch
            {
                static void Postfix()
                {
                    ProficiencyManager.AddProficiency(new ProficiencyCombatMeditate());
                    ProficiencyManager.AddProficiency(new BloodRush());
                    /* Base skills example
                    FTK_playerGameStartDB playerGameStartDB = TableManager.Instance.Get<FTK_playerGameStartDB>();
                    FTK_playerGameStart baseSkills = playerGameStartDB.m_Array[(int)FTK_playerGameStart.ID.busker];
                    FTKAPI.Utils.Logger.LogWarning(baseSkills is null); */
                    // Add Paladin
                    // Skills
                    /*DamageMods damageMods = new DamageMods();
                    SkillManager.AddGlobal(new CustomCharacterSkills()
                    {
                        Skills = new List<FTKAPI_CharacterSkill> { damageMods }
                    });*/
                    SkillContainer.Instance.Reset();
                    FTKAPI_CharacterSkill focusHealer = SkillContainer.Instance.focusHealer;
                    FTKAPI_CharacterSkill divine = SkillContainer.Instance.divine;
                    List<FTKAPI_CharacterSkill> paladinSkills = new List<FTKAPI_CharacterSkill>() { focusHealer, divine };
                    CustomCharacterSkills paladinCharacterSkills = new CustomCharacterSkills()
                    {
                        Skills = paladinSkills
                    };
                    // Skinset
                    int customSkinset = SkinsetManager.AddSkinset(new PaladinSkinset(), Instance);
                    FTK_skinset.ID[] customSkinsets = new FTK_skinset.ID[] { (FTK_skinset.ID)customSkinset };
                    // Starting Weapon
                    int bladePugio = ItemManager.AddItem(new BladePugio(), Instance);
                    // Paladin
                    ClassManager.AddClass(new PlayerPaladin() { Skinsets = customSkinsets, CharacterSkills = paladinCharacterSkills, StartWeapon = (FTK_itembase.ID)bladePugio }, Instance); //Adds our paladin class

                    // Modify Monk with new Discipline
                    List<FTKAPI_CharacterSkill> monkSkills = new List<FTKAPI_CharacterSkill>() { SkillContainer.Instance.discipline, SkillContainer.Instance.combatMeditation, SkillContainer.Instance.bloodRush };
                    CustomCharacterSkills monkCharacterSkills = new CustomCharacterSkills()
                    {
                        Skills = monkSkills,
                        m_PartyHeal = true,
                    };
                    ClassManager.ModifyClass(FTK_playerGameStart.ID.monk, new CustomClass(FTK_playerGameStart.ID.monk)
                    {
                        CharacterSkills = monkCharacterSkills
                    });
                    // Proficiencies
                    int taunt02 = ProficiencyManager.AddProficiency(new Taunt02());


                    // Items
                    int bladeSilver = ItemManager.AddItem(new BladeSilver(), Instance);
                    int hammerLightning = ItemManager.AddItem(new HammerLightning(), Instance);

                    /* ClassManager.ModifyClass(//Modifies the hobo
                         FTK_playerGameStart.ID.hobo,
                         new CustomClass(FTK_playerGameStart.ID.hobo)
                         {
                             StartWeapon = (FTK_itembase.ID)bladeSilver,
                         }.AddToStartItems(new FTK_itembase.ID[] {
                             (FTK_itembase.ID)hammerLightning
                         })
                     );*/
                    //Modify sanctums
                    //SanctumStatsManager.ModifySanctum(FTK_sanctumStats.ID.Sanctum05, new Courage());
                    //ModifierManager.ModifyModifier(FTK_characterModifier.ID.Sanctum05, new CustomModifier());
                    TreeManager.Instance.Reset();
                    //for IL hooks that need to be initialized in the TableManager Postfix patch
                    HookApplySlotCombatAction.Instance.Prof = taunt02;
                    HookApplySlotCombatAction.Instance.Initialize();
                }
            }
        }
    }
}
