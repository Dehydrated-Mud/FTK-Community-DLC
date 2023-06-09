﻿using BepInEx;
using CommunityDLC.Mechanics;
using CommunityDLC.Objects.Proficiencies;
using CommunityDLC.Objects.CharacterSkills;
using FTKAPI.Managers;
using FTKAPI.Objects;
using GridEditor;
using HarmonyLib;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Logger = FTKAPI.Utils.Logger;

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

        HookSetAttackDecision hookSetAttackDecision = new HookSetAttackDecision();
        FindHerbs hookFindHerbs = new FindHerbs();

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

                    /* Base skills example
                    FTK_playerGameStartDB playerGameStartDB = TableManager.Instance.Get<FTK_playerGameStartDB>();
                    FTK_playerGameStart baseSkills = playerGameStartDB.m_Array[(int)FTK_playerGameStart.ID.busker];
                    FTKAPI.Utils.Logger.LogWarning(baseSkills is null); */
                    // Add Long Taunt

                    // Add Paladin
                    // Skills
                    SkillContainer.Instance.Reset();
                    FTKAPI_CharacterSkill focusHealer = SkillContainer.Instance.focusHealer;
                    FTKAPI_CharacterSkill divine = SkillContainer.Instance.divine;
                    FTKAPI_CharacterSkill[] paladinSkills = { focusHealer, divine };
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

                    HookApplySlotCombatAction.Instance.Prof = taunt02;
                    HookApplySlotCombatAction.Instance.Initialize();
                }
            }
        }
    }
}
