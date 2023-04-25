using FTKAPI.Objects.SkillHooks;
using CommunityDLC.Objects.SkillTree;
using FTKAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using GridEditor;
using FTKAPI;
using Logger = FTKAPI.Utils.Logger;
using CommunityDLC.UIElements.Behaviors;
using FTKAPI.Objects;

namespace CommunityDLC.UIElements
{
    public class HookInventory : BaseModule
    {
        // This adds the modifier array to the serialized fields so that the data can persist.
        public override void Initialize()
        {
            Unload();
            On.uiPlayerInventory.ShowCharacterInventory += OnShowCharacterInventory;
            On.uiPlayerInventory.OnClose += OnInventoryClose;
            On.uiPlayerInventory.ShowStats += OnShowStats;
            On.uiPlayerStats.UpdateDisplay += OnUpdateSkillsText;
        }

        //Should be moved to the API!
        private void OnUpdateSkillsText(On.uiPlayerStats.orig_UpdateDisplay _orig, uiPlayerStats _this, CharacterOverworld _cow)
        {
            _orig(_this, _cow);
            CharacterSkills characterSkills = FTK_playerGameStartDB.Get(_cow.m_CharacterStats.m_CharacterClass).m_CharacterSkills;
            List<ModDisplayName> eachSkillDisplay = characterSkills.GetEachSkillDisplay();
            List<string> intrinsicDisp = new List<string>(eachSkillDisplay.Select(o => o.m_DisplayName).ToList());
            List<string> intrinsicTip = new List<string>(eachSkillDisplay.Select(o => o.m_ToolTip).ToList());

            CharacterSkills equippedSkills = _cow.m_CharacterStats.GetEquippedSkills();
            List<ModDisplayName> eachSkillDisplay2 = equippedSkills.GetEachSkillDisplay();
            List<string> equippedDisp = new List<string>(eachSkillDisplay2.Select(o => o.m_DisplayName).ToList());
            List<string> equippedTip = new List<string>(eachSkillDisplay2.Select(o => o.m_ToolTip).ToList());

            CustomCharacterSkills intrinsic;
            CustomCharacterSkills total;
            if (characterSkills is CustomCharacterSkills)
            {
                intrinsic = (CustomCharacterSkills)characterSkills;
            }
            else 
            { 
                intrinsic = new CustomCharacterSkills();
            }
            if(_cow.m_CharacterStats.m_CharacterSkills is CustomCharacterSkills)
            {
                total = (CustomCharacterSkills)_cow.m_CharacterStats.m_CharacterSkills;
            }
            else
            {
                total = new CustomCharacterSkills();
            }
            List<FTKAPI_CharacterSkill> intrinsicList = new List<FTKAPI_CharacterSkill>(intrinsic.Skills);
            List<FTKAPI_CharacterSkill> totalList = new List<FTKAPI_CharacterSkill>(total.Skills);

            List<string> intrinsicStringListDisp = new List<string>(intrinsicList.Select(o => o.m_DisplayName).ToList());
            List<string> intrinsicStringListTip = new List<string>(intrinsicList.Select(o => o.m_ToolTip).ToList());

            List<FTKAPI_CharacterSkill> equippedList = totalList.Except(intrinsicList).ToList();
            List<string> equippedStringListDisp = new List<string>(equippedList.Select(o => o.m_DisplayName).ToList());
            List<string> equippedStringListTip = new List<string>(equippedList.Select(o => o.m_ToolTip).ToList());

            List<string> totalIntrinsicDisp = intrinsicDisp.Concat(intrinsicStringListDisp).ToList();
            List<string> totalIntrinsicTip = intrinsicTip.Concat(intrinsicStringListTip).ToList();
            List<string> totalEquippedDisp = equippedDisp.Concat(equippedStringListDisp).ToList();
            List<string> totalEquippedTip = equippedTip.Concat(equippedStringListTip).ToList();

            for (int k = 0; k < _this.m_ClassSkills.Count; k++)
            {
                if (k < totalIntrinsicDisp.Count)
                {
                    _this.m_ClassSkills[k].gameObject.SetActive(value: true);
                    _this.m_ClassSkills[k].SetTextValue(totalIntrinsicDisp[k]);
                    _this.m_ClassSkills[k].SetToolTipInfo(totalIntrinsicDisp[k], totalIntrinsicTip[k], _raw: true);
                }
                else
                {
                    _this.m_ClassSkills[k].gameObject.SetActive(value: false);
                }
            }
            for (int k = 0; k < _this.m_EquippedSkills.Count; k++)
            {
                if (k < totalEquippedDisp.Count)
                {
                    _this.m_EquippedSkills[k].gameObject.SetActive(value: true);
                    _this.m_EquippedSkills[k].SetTextValue(totalEquippedDisp[k]);
                    _this.m_EquippedSkills[k].SetToolTipInfo(totalEquippedDisp[k], totalEquippedTip[k], _raw: true);
                }
                else
                {
                    _this.m_EquippedSkills[k].gameObject.SetActive(value: false);
                }
            }
        }

        private void OnShowStats(On.uiPlayerInventory.orig_ShowStats _orig, uiPlayerInventory _this, CharacterOverworld _cow, bool _cycler)
        {
            _orig(_this, _cow, _cycler);
            TreeManager.Instance.DeactivatePanels();
            SkillTreeUI treeUI = TreeManager.Instance.m_modTrees[_cow.m_FTKPlayerID.m_TurnIndex]?.SkillUI;
            if (treeUI != null)
            {
                treeUI.Panel.SetActive(false);
            }
        }
        private void OnShowCharacterInventory(On.uiPlayerInventory.orig_ShowCharacterInventory orig, uiPlayerInventory self, CharacterOverworld cow, bool cycler)
        {
            TreeManager.Instance.DeactivatePanels();
            orig(self, cow, cycler);
            SkillTreeUI treeUI = TreeManager.Instance.m_modTrees[cow.m_FTKPlayerID.m_TurnIndex].SkillUI;
            if (!treeUI.Panel || treeUI.Panel == null)
            {
                // If a tree panel is null, it is the first time the user has clicked an inventory
                // We need to initialize all the panels at the same time, so that when we use the Left/Right cycler the panel size is still correct
                foreach (ModifierTree tree in TreeManager.Instance.m_modTrees.Values)
                {
                    SkillTreeUI _treeUI = tree.SkillUI;
                    if (!_treeUI.Panel || _treeUI.Panel == null)
                    {
                        var rect = (RectTransform)self.gameObject.transform.Find("InventoryBackground").transform;
                        _treeUI.InitLoadoutPanel(rect);
                        _treeUI.Panel.SetActive(false);
                    }
                }
                treeUI.Panel.SetActive(true);

            }
            else
            {
                treeUI.Panel.SetActive(true);
                foreach (TreeButton treeButton in treeUI.Buttons)
                {
                    treeButton.gameObject.SetActive(true);
                }
            }
            treeUI.Update();
        }

        private void OnInventoryClose(On.uiPlayerInventory.orig_OnClose orig, uiPlayerInventory self)
        {
            orig(self);
            TreeManager.Instance.DeactivatePanels();
        }

        public override void Unload()
        {
            On.uiPlayerInventory.ShowCharacterInventory -= OnShowCharacterInventory;
            On.uiPlayerInventory.OnClose -= OnInventoryClose;
            On.uiPlayerInventory.ShowStats -= OnShowStats;
        }
    }
}
