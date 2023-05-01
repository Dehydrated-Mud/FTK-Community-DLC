using FTKAPI.Objects;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.Proficiencies
{
    internal class ProficiencyCrushingBlow : CustomProficiency
    {
        public ProficiencyCrushingBlow() 
        {
            TargetFriendly = false;
            Category = Category.None;
            ProficiencyPrefab = this;
            SlotOverride = 1;
            PerSlotSkillRoll = 1f;
            //BattleButton = CommunityDLC.assetBundleIcons.LoadAsset<Sprite>("Assets/Icons/weaponBlade.png");
            Name = new("Crushing Blow");
            ID = "CrushingBlowProf";
            Tint = new Color(.5f, .0f, 0f, 1f);
        }
    }
}
