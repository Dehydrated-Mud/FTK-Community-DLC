using CommunityDLC.Objects.CharacterSkills;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTKAPI.Managers;
using Logger = FTKAPI.Utils.Logger;
using CommunityDLC.Objects.SkillTree.Leaves;
using CommunityDLC.Objects.SkillTree.Trees;
using CommunityDLC.UIElements.Behaviors;
using IL.Rewired.UI.ControlMapper;

namespace CommunityDLC.Objects.SkillTree
{
    public class TreeManager
    {
        public static TreeManager instance;
        public static TreeManager Instance
        {
            get
            {
                if (instance == null)
                {
                    Logger.LogWarning("No TreeManager instance found, initializing instance...");
                    instance = new TreeManager();
                }
                return instance;
            }
        }

        public Dictionary<int, ModifierTree> m_modTrees;
        public Dictionary<Leaf.LeafID, FTK_characterModifier.ID> m_Leaves;
        public SkillSyncer Syncer { get; set; }
        //Call at the beginning of the TableManager Postfix patch
        public void Reset()
        {
            m_modTrees = new Dictionary<int, ModifierTree>();
            m_Leaves = new Dictionary<Leaf.LeafID, FTK_characterModifier.ID>();
            InitializeLeaves();
        }
        public void RegisterTree(int playerID, FTK_playerGameStart.ID characterID, CharacterStats characterStats)
        {
            if (!m_modTrees.ContainsKey(playerID))
            {
                switch (characterID)
                {
                    case FTK_playerGameStart.ID.woodcutter:
                        m_modTrees[playerID] = new WoodCutterTree() { Stats = characterStats };
                        break;
                    case FTK_playerGameStart.ID.monk:
                        m_modTrees[playerID] = new MonkTree() { Stats = characterStats };
                        break;
                    case FTK_playerGameStart.ID.scholar:
                        m_modTrees[playerID] = new ScholarTree() { Stats = characterStats };
                        break;
                    case FTK_playerGameStart.ID.treasureHunter:
                        m_modTrees[playerID] = new TreasureHunterTree() { Stats = characterStats };
                        break;
                    case FTK_playerGameStart.ID.blacksmith:
                        m_modTrees[playerID] = new BlackSmithTree() { Stats = characterStats };
                        break;
                    case FTK_playerGameStart.ID.hobo:
                        m_modTrees[playerID] = new HoboTree() { Stats = characterStats };
                        break;
                    case FTK_playerGameStart.ID.hunter:
                        m_modTrees[playerID] = new HunterTree() { Stats = characterStats };
                        break;
                    case FTK_playerGameStart.ID.gladiator:
                        m_modTrees[playerID] = new GladiatorTree() { Stats = characterStats };
                        break;
                    default:
                        m_modTrees[playerID] = new TestTree() { Stats = characterStats };
                        break;
                }
                m_modTrees[playerID].InitializeData();
            }
            else
            {
                Logger.LogWarning("Tried to register Tree for player that is already in the dictionary: " + playerID);
            }
        }

        public void InitializeLeaves()
        {
            m_Leaves = LeafInitializer.Initialize();
        }

        public void DeactivatePanels()
        {
            foreach(ModifierTree modifierTree in m_modTrees.Values)
            {
                if ((bool)modifierTree.SkillUI.Panel)
                {
                    modifierTree.SkillUI.Panel.SetActive(value: false);
                    foreach(TreeButton treeButton in modifierTree.SkillUI.Buttons)
                    {
                        treeButton.gameObject.SetActive(false);
                    }
                }
                else
                {
                    Logger.LogWarning("UI Panel does not exist");
                }
            }
        }
    }
}
