using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using CommunityDLC.Objects.SkillTree;
using CommunityDLC.UIElements.Behaviors;
using FTKAPI;
using FTKAPI.Utils;
using GridEditor;
using SimpleBind.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Logger = FTKAPI.Utils.Logger;
namespace CommunityDLC.UIElements
{
    public class SkillTreeUI
    {
        ModifierTree modTree;
        private GameObject skillTree;
        //private KeyListener keyListener;
        TreeButton[] buttons;
        internal SkillTreeUI(ModifierTree tree)
        {
            modTree = tree;
            buttons = new TreeButton[modTree.Buttons.Count()];
        }

        private void OnKeyPressChanged(KeyListener listener)
        {

        }
        public void Update()
        {
            if (buttons != null && buttons.Count() > 0)
            {
                Logger.LogInfo("Made it to the Update loop");
                foreach (TreeButton treeButton in buttons)
                {
                    if(treeButton == null)
                    {
                        Logger.LogError("tree button is null!");
                    }
                    treeButton.SetState();
                }
            }
        }
        public void InitLoadoutPanel(RectTransform parentRect)
        {

            var panel = new GameObject("skilltree-panel-"+modTree.Stats.name);
            panel.layer = LayerMask.NameToLayer("UI");
            this.skillTree = panel;
            var transform = panel.AddComponent<RectTransform>();
            transform.ScaleResolutionBased().SetParent(parentRect);

            transform.anchorMin = new Vector2(0.5f, 0f);
            transform.anchorMax = new Vector2(0.5f,0f);
            //transform.anchoredPosition = new Vector2(400, -815); //Top left
            transform.pivot = new Vector2(0f,0f);
            transform.anchoredPosition = new Vector2(0, 15);
            transform.sizeDelta = new Vector2(60, 60);
            
            Image img = panel.AddComponent<Image>();
            img.color = new Color(.7f,.7f,1f,.05f);
            //img.type = Image.Type.Sliced;

            /*var sizeFitter = panel.AddComponent<ContentSizeFitter>();
            sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;*/

            //this.keyListener = panel.AddComponent<KeyListener>();
            //this.keyListener.Init(OnKeyPressChanged);
            var levelPanel = new GameObject("skilltree-panel-" + modTree.Stats.name + "-level-panel");
            levelPanel.layer = LayerMask.NameToLayer("UI");
            var levelTransform = levelPanel.AddComponent<RectTransform>();
            levelTransform.ScaleResolutionBased().SetParent(transform);
            levelTransform.anchorMin = new Vector2(0f, 0f);
            levelTransform.anchorMax = new Vector2(0.8f, 1f);
            levelTransform.pivot = new Vector2(0f, 0f);
            levelTransform.anchoredPosition = new Vector2(0, 0);
            levelTransform.sizeDelta = new Vector2(1f, 1f);
            /*Image levelImg = levelPanel.AddComponent<Image>();
            levelImg.color = new Color(.7f, .7f, 1f, 1f);*/
            var levelLayout = levelPanel.AddComponent<VerticalLayoutGroup>();
            levelLayout.childControlHeight = false;
            levelLayout.childControlWidth = true;
            levelLayout.childAlignment = TextAnchor.MiddleCenter;
            levelLayout.childForceExpandHeight = false;
            levelLayout.spacing = 0;

            var milestonePanel = new GameObject("skilltree-panel-" + modTree.Stats.name + "-milestone-panel");
            milestonePanel.layer = LayerMask.NameToLayer("UI");
            var milestoneTransform = milestonePanel.AddComponent<RectTransform>();
            milestoneTransform.ScaleResolutionBased().SetParent(transform);
            milestoneTransform.anchorMin = new Vector2(0.8f, 0f);
            milestoneTransform.anchorMax = new Vector2(1f, 1f);
            milestoneTransform.pivot = new Vector2(0.8f, 0f);
            milestoneTransform.anchoredPosition = new Vector2(0, -5f);
            milestoneTransform.sizeDelta = new Vector2(1f,1f);
            /*Image milestoneImg = milestonePanel.AddComponent<Image>();
            milestoneImg.color = new Color(.7f, 1f, .7f, 1f);*/
            var layout = milestonePanel.AddComponent<VerticalLayoutGroup>();
            layout.childControlHeight = false;
            layout.childControlWidth = false;
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.spacing = 1;
            layout.childForceExpandHeight = false;
            int loadedSingles = 0; // Number of single-button branches loaded (controls the offset)
            int loadedButtons = 0;
            // Create buttons here
            foreach (Branch branch in modTree.Branches)
            {
                if(branch.BranchType == branchType.Single)
                {
                    this.buttons[loadedButtons] = CreateButton(milestoneTransform, loadedSingles, branch.Buttons[0]);
                    loadedSingles++;
                    loadedButtons++;
                }
                else if(branch.BranchType == branchType.Multiple)
                {
                    int level = branch.MileStone.Level;
                    var subPanel = new GameObject($"Level Panel {level}");
                    subPanel.layer = LayerMask.NameToLayer("UI");
                    var subTransform = subPanel.AddComponent<RectTransform>();
                    subTransform.ScaleResolutionBased().SetParent(levelTransform, true);
                    subTransform.anchorMin = new Vector2(0, 0);
                    subTransform.anchorMax = new Vector2(0, 0);
                    subTransform.sizeDelta = new Vector2(1, 10);
                    subTransform.pivot = new Vector2(0, 0);
                    subTransform.anchoredPosition = new Vector2(0, 0);

                    
                    /*Image subImage = subPanel.AddComponent<Image>();

                    subImage.color = new Color(0.1f, 0.1f, 0.3f);*/
                    // Halve spacing for every button on the branch past two
                    float fac = (float)Math.Pow(1.5,Math.Max(0,branch.Buttons.Count - 2));
                    var subLayout = subPanel.AddComponent<HorizontalLayoutGroup>();
                    subLayout.childControlHeight = false;
                    subLayout.childControlWidth = false;
                    subLayout.spacing = 8 / fac;
                    subLayout.childAlignment = TextAnchor.MiddleCenter;
                    subLayout.childForceExpandWidth = false;

                    /*Text levelText = subPanel.AddComponent<Text>();
                    levelText.text = "Level "+level;
                    levelText.alignment = TextAnchor.MiddleCenter;
                    levelText.color = new Color(1, 1, 1);*/
                    for (int i = 0; i < branch.Buttons.Count(); i++)
                    {
                        LeafButton button = branch.Buttons[i];
                        this.buttons[loadedButtons] = CreateButton(subTransform, i, level, button);
                        loadedButtons++;
                    }
                }
            }
        }
        private TreeButton CreateButton(RectTransform parentObject, int idy, LeafButton leafButton)
        {
            return CreateButton(parentObject, 0, idy, leafButton, true);
        }
        private TreeButton CreateButton(RectTransform parentRect, int xPos, int yPos, LeafButton leafButton, bool layout = false)
        {
            GameObject button = new GameObject($"Tree-button-{xPos},{yPos},{layout}");
            button.layer = LayerMask.NameToLayer("UI");
            var transform = button.AddComponent<RectTransform>();
            transform.SetParent(parentRect, false);

            transform.anchorMin = new Vector2(0.5f, 0f);
            transform.anchorMax = new Vector2(0.5f, 0f);
            transform.pivot = new Vector2(0f, 0f);
            transform.anchoredPosition = new Vector2(xPos * 10, 0f);
            transform.sizeDelta = new Vector2(5, 5);
            transform.SetHeight(8);
            transform.SetWidth(8);
            /*if (layout)
            {
                var layoutElement = button.AddComponent<LayoutElement>();
                layoutElement.preferredHeight = 8;
                layoutElement.preferredWidth = 8;
            }*/

            var handler = button.AddComponent<TreeButtonPointerHandler>();
            handler.Callback = OnClick;
            //handler.HoverCallback = OnHover;
            //handler.BlurCallback = OnBlur;

            var tooltip = button.AddComponent<uiToolTipGeneral>();
            tooltip.m_IsFollowHoriz = false;
            tooltip.m_ReturnRawInfo = true;
            tooltip.m_Info = leafButton.m_Title;
            string txt = leafButton.ParentBranch.MileStone.Milestone;
            txt = txt == null ? "" : txt + "\n";
            tooltip.m_DetailInfo = txt + CharacterSkills.GetModDisplay(FTK_characterModifierDB.Get(leafButton.Active), _format: true);
            int numLines = tooltip.m_DetailInfo.Split('\n').Length;
            tooltip.m_ToolTipOffset = new Vector2(0, -42*numLines);
            
            var treeButton = button.AddComponent<TreeButton>();
            treeButton.tooltip = tooltip;
            treeButton.leafButton = leafButton;
            handler.Button = treeButton;

            return treeButton;
            }

        private void OnClick(TreeButton treeButton, PointerEventData.InputButton inputButton)
        {
            if (treeButton.State == buttonState.Unlocked)
            {
                Branch branch = treeButton.leafButton.ParentBranch;
                branch.SetActive(treeButton.leafButton);
                Update();
            }
        }

        public GameObject Panel { get => skillTree; }
        public ModifierTree ModTree { get => modTree; }
        public TreeButton[] Buttons { get => buttons; }
    }
}
