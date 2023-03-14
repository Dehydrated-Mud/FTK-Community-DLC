using FTKAPI.Objects;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace CommunityDLC {
    public class SteelSmite : CustomProficiency {
        public SteelSmite() {
            ID = "steelsmite";
            Name = new("Silver Edge");
            SlotOverride = 4;
            ProficiencyPrefab = new ProficiencyAttack();
            Category = Category.Armor;
            CustomValue = -10f;
            FullSlots = true;
            PerSlotSkillRoll = 0f;
            DmgMultiplier = 1f;
            Tint = new UnityEngine.Color(.7f, .7f, .8f, 1f);
            BattleButton = CommunityDLC.assetBundleIcons.LoadAsset<Sprite>("Assets/Icons/weaponBlade.png");
        }
    }
}
