using FTKAPI.Managers;
using FTKAPI.Objects;
using GridEditor;
using UnityEngine;


namespace CommunityDLC {
    using ProficiencyManager = FTKAPI.Managers.ProficiencyManager;
    public class BladePugio : CustomItem {
        public BladePugio() {
            ID = "Pugio";
            Name = new("Pugio");
            Prefab = CommunityDLC.assetBundle.LoadAsset<GameObject>("Assets/Pugio.prefab");
            ObjectSlot = FTK_itembase.ObjectSlot.oneHand;
            ObjectType = FTK_itembase.ObjectType.weapon; // This is required for the item to be registered as a weapon
            SkillType = FTK_weaponStats2.SkillType.vitality;
            WeaponType = Weapon.WeaponType.bladed;
            ProficiencyEffects = new() { // these are the weapon attacks/skills for this custom item
                [FTK_proficiencyTable.ID.bladePierceReg] = FTK_hitEffect.ID.bladePierceReg,
            };
            AnimationController = AssetManager.GetAnimationControllers<Weapon>().Find(i => i.name == "player_1H_Bladed_Combat");
            Slots = 3;
            MaxDmg = 8;
            DmgType = FTK_weaponStats2.DamageType.physical;
            ShopStock = 1;
            TownMarket = false;
            DungeonMerchant= false;
            ItemRarity = FTK_itemRarityLevel.ID.common;
            NoRegularAttack = true;
            GoldValue = 10;
            Icon = CommunityDLC.assetBundleIcons.LoadAsset<Sprite>("Assets/Icons/weaponBlade.png");
            IconNonClickable = CommunityDLC.assetBundleIcons.LoadAsset<Sprite>("Assets/Icons/weaponBlade.png");
        }
    }
}
