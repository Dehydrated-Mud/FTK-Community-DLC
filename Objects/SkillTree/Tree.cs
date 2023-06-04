using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityDLC.UIElements;
using Epic.OnlineServices.Achievements;
using FTKAPI.Managers;
using FTKAPI.Objects;
using GridEditor;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Logger = FTKAPI.Utils.Logger;

namespace CommunityDLC.Objects.SkillTree
{
    public enum branchType
    {
        None = 0,
        Single,
        Multiple
    }
    public enum buttonState
    {
        None = 0,
        Locked,
        Unlocked,
        Active
    }
    public enum MileStonePrimary
    {
        None,
        Level,
        Scourge,
        Mob,
        Encounter
    }

    [Flags]
    public enum MileStoneScourge
    {
        None = 0,
        DrollSolo = 1 << 0
    }

    public class MileStoneContainer : IEquatable<MileStoneContainer>
    {
        private MileStonePrimary m_Primary = MileStonePrimary.None;
        private int m_Level = 0;
        private MileStoneScourge m_Scourge = MileStoneScourge.None;
        private List<FTK_enemyCombat.ID> m_Enemies = new();
        private FTK_miniEncounter.ID m_Encounter = FTK_miniEncounter.ID.None;
        private FTK_slotOutput.ID m_SlotOutput = FTK_slotOutput.ID.None;
        private string m_Name;
        private string m_Flair;
        public bool Equals(MileStoneContainer other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return CheckEnemyEquality(other) && CheckSolo(other) && Primary.Equals(other.Primary) && Level.Equals(other.Level) && Scourge.Equals(other.Scourge) && Encounter.Equals(other.Encounter) && SlotOutput.Equals(other.SlotOutput);
        }

        private bool CheckEnemyEquality(MileStoneContainer other)
        {
            if (Enemies != null && Enemies.Count > 0) // Do we require any enemies to have been defeated?
            {
                // If we require certain enemies to be defeated and the other milestone has none, return false
                if (other.Enemies == null || !other.Enemies.Any()) 
                { 
                    return false; 
                }
                if (AllEnemies)
                {
                    if (Enemies.Count > other.Enemies.Count)
                        return false;
                    List<FTK_enemyCombat.ID> tmpEnemies = new(other.Enemies);
                    foreach (FTK_enemyCombat.ID enemy in Enemies)
                    {
                        if (tmpEnemies.Contains(enemy))
                        {
                            tmpEnemies.Remove(enemy);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return Enemies.Intersect(other.Enemies).Any();
            }
            return true; // If we do not specify an enemy requirement, then the milestone passes the enemy requirement
        }
        public bool CheckSolo(MileStoneContainer other)
        {
            // If we don't require solo, then the condition doesn't matter and we return true
            if (Solo)
            {
                if (other.Solo)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            int hashPrimary = Primary.GetHashCode();
            int hashLevel = Level.GetHashCode();
            int hashScourge = Scourge.GetHashCode();
            int hashEncounter = Encounter.GetHashCode();
            int hashSlot = SlotOutput.GetHashCode();

            return hashPrimary ^ hashLevel ^ hashScourge ^ hashEncounter ^ hashSlot;
        }
        public MileStonePrimary Primary { get => m_Primary; set => m_Primary = value; }
        public int Level { get => m_Level; set => m_Level = value; }
        public MileStoneScourge Scourge { get => m_Scourge; set => m_Scourge = value; }
        public FTK_miniEncounter.ID Encounter { get => m_Encounter; set => m_Encounter = value; }
        public FTK_slotOutput.ID SlotOutput { get => m_SlotOutput; set => m_SlotOutput = value; }
        public List<FTK_enemyCombat.ID> Enemies { get => m_Enemies; set => m_Enemies = value; }
        public bool AllEnemies { get; set; } = false;
        public string Milestone { get; set; }
        public bool Solo { get; set; } = false;
        /* private CustomLocalizedString milestone;
        public CustomLocalizedString m_Milestone
        {
            get => this.milestone;
            set
            {
                this.milestone = value;
                this.Milestone = this.milestone.GetLocalizedString();
            }
        } */
        public string Flair { get => m_Flair; set => m_Flair = value; }
    }
    public class ModifierTree
    {
        List<Branch> m_Branches = new List<Branch>();
        List<LeafButton> m_Buttons = new List<LeafButton>();
        Dictionary<FTK_characterModifier.ID, LeafButton> m_Map = new Dictionary<FTK_characterModifier.ID, LeafButton>();
        CharacterStats m_CharacterStats;
        public SkillTreeUI SkillUI { get; set; }

        public void InitializeData()
        {

            if (!(bool)Stats)
            {
                Logger.LogError("No character stats assigned to SkillTree!");
            }
            else
            {
                foreach (Branch branch in m_Branches)
                {
                    foreach (LeafButton button in branch.Buttons)
                    {
                        m_Buttons.Add(button);
                        button.Stats = Stats;
                        m_Map[button.Active] = button;
                        if (button.Indicator != FTK_characterModifier.ID.None)
                        {
                            m_Map[button.Indicator] = button;
                        }
                    }
                }
                SkillUI = new SkillTreeUI(this);
            }
        }

        public void Achieved(MileStoneContainer _mileStone)
        {
            Logger.LogInfo("Tree has received a milestone.");
            foreach (Branch branch in m_Branches)
            {
                if (branch.MileStone.Equals(_mileStone))
                {
                    Logger.LogInfo("Found a branch in this tree that matches the milestone");
                    branch.UnlockAll();
                }
            }
        }
        public Dictionary<FTK_characterModifier.ID, LeafButton> Map { get => m_Map; }
        public List<Branch> Branches { get => m_Branches; set { m_Branches = value; } }
        public List<LeafButton> Buttons { get => m_Buttons; }
        public CharacterStats Stats { get => m_CharacterStats; set => m_CharacterStats = value; }
    }

    public class Branch
    { // The branches are collections of mutually exclusive buttons. Some branches may only have one button.
        branchType m_BranchType = branchType.None;
        MileStoneContainer m_MileStone;
        List<LeafButton> m_Buttons = new List<LeafButton>();
        public Branch(LeafButton _button)
        {
            m_BranchType = branchType.Single;
            _button.ParentBranch = this;
            m_Buttons.Add(_button);
        }
        public Branch(List<LeafButton> _buttons)
        {
            m_BranchType = branchType.Multiple;
            foreach(LeafButton _button in _buttons) 
            { 
                _button.ParentBranch = this;
                m_Buttons.Add(_button);
            }
        }

        // On click
        public void SetActive(LeafButton _button)
        {
            foreach (LeafButton button in m_Buttons)
            {
                if (button == _button)
                {
                    button.SetActive();
                }
                else
                {
                    button.SetInactive();
                }
            }
        }
        // When multiple buttons should be unlocked to choose from
        public void UnlockAll()
        {
            foreach (LeafButton button in m_Buttons)
            {
                button.Unlock();
            }
        }
        public branchType BranchType { get => m_BranchType; }
        public MileStoneContainer MileStone { get => m_MileStone; set => m_MileStone = value; }
        public List<LeafButton> Buttons { get => m_Buttons; }
    }

    public class LeafButton
    { // A button is a pair of mutually exclusive leaves
        Branch m_Branch;
        public CharacterStats Stats { get; set; }
        FTK_characterModifier.ID modActive;
        FTK_characterModifier.ID modIndicator;
        buttonState m_State = buttonState.Locked;

        public LeafButton(Leaf.LeafID _active, Leaf.LeafID _indicator = Leaf.LeafID.None)
        {
            modActive = TreeManager.Instance.m_Leaves[_active];
            modIndicator = TreeManager.Instance.m_Leaves[_indicator];
        }

        public void Unlock()
        {
            if (modIndicator == FTK_characterModifier.ID.None || m_Branch.BranchType == branchType.Single)
            {
                SetActive();
            }
            else
            {
                TreeManager.Instance.Syncer.AddRemoveModifier(Stats.m_CharacterOverworld.m_FTKPlayerID, modIndicator, adding: true);
                m_State = buttonState.Unlocked; // Should this be done in the RPC as well? Likely not but maybe
            }
        }
        internal void SetActive()
        {
            TreeManager.Instance.Syncer.AddRemoveModifier(Stats.m_CharacterOverworld.m_FTKPlayerID, modActive, adding: true);
            // Get our leaf, see if it wants to do an action on add (like give item or set aura) and do that action. It is on the AddAction
            // Method to make sure that the action is synced properly.
            Leaf leaf = (Leaf) ModifierManager.Instance.customDictionary[(int)modActive];
            if (leaf.OnAdd)
            {
                leaf.AddAction(Stats.m_CharacterOverworld);
            }
            if (modIndicator != FTK_characterModifier.ID.None)
            {
                TreeManager.Instance.Syncer.AddRemoveModifier(Stats.m_CharacterOverworld.m_FTKPlayerID, modIndicator, adding: false);
            }
            m_State = buttonState.Active;
        }
        internal void SetInactive()
        {
            TreeManager.Instance.Syncer.AddRemoveModifier(Stats.m_CharacterOverworld.m_FTKPlayerID, modActive, adding: false);
            if (modIndicator != FTK_characterModifier.ID.None)
            {
                TreeManager.Instance.Syncer.AddRemoveModifier(Stats.m_CharacterOverworld.m_FTKPlayerID, modIndicator, adding: false);
            }
            m_State = buttonState.Locked;
        }
        // If we rely on the above methods to set the state, then the information won't persist after save/reload
        // Thus we determine what the state should be every time we TallyCharacterMods(), that way our data persists via the modifiers
        public void SetState(Leaf _leaf)
        {
            m_State = _leaf.State;
        }
        public void SetState(buttonState _state)
        {
            m_State = _state;
        }
        public buttonState State { get => m_State; }
        public Branch ParentBranch { get => m_Branch; set => m_Branch = value; }
        public FTK_characterModifier.ID Active { get => modActive; set => modActive = value; }
        public FTK_characterModifier.ID Indicator { get => modIndicator; set => modIndicator = value; }
        public string m_Title; // This is the string displayed as the title when the user mouses over the button
        private CustomLocalizedString title;
        public CustomLocalizedString Title
        {
            get => this.title;
            set
            {
                this.title = value;
                this.m_Title = this.title.GetLocalizedString();
            }
        }
    }

}
