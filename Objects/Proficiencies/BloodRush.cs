using FTKAPI.Objects;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityDLC.PhotonHooks;
using FTKAPI.APIs.BattleAPI;

namespace CommunityDLC.Objects.Proficiencies
{
    public class BloodRush : CustomProficiency
    {
        public BloodRush() 
        {
            Target = CharacterDummy.TargetType.PickFriendly;
            TargetFriendly = true;
            Harmless = true;
            Category = Category.Rush;
            ProficiencyPrefab = this;
            SlotOverride = 4;
            PerSlotSkillRoll = -.1f;
            FullSlots = true;
            //BattleButton = CommunityDLC.assetBundleIcons.LoadAsset<Sprite>("Assets/Icons/weaponBlade.png");
            Name = new("Blood Rush");
            ID = "GreaterRushPlus";
            Tint = new Color(1f, .65f, .25f, 1f);
        }

        public override void AddToDummy(CharacterDummy _dummy)
        {
            _dummy.m_RushInterrupt = Proficiency_RushInterrupt.Rush;
            //Guarantee critical strike next turn
            BattleAPI.Instance.SetAFlag(_dummy, true, SetFlags.Crit, _override: false);
        }
    }
}
