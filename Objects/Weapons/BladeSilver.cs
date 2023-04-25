using FTKAPI.Managers;
using FTKAPI.Objects;
using GridEditor;
using UnityEngine;


namespace CommunityDLC {
    using ProficiencyManager = FTKAPI.Managers.ProficiencyManager;
    public class BladeSilver : CustomItem {
        public BladeSilver() {
            int customProf = ProficiencyManager.AddProficiency(new SilverSmite());
            int customProf2 = ProficiencyManager.AddProficiency(new SteelSmite());
            ID = "CustomBladeSilver";
            Name = new("Silver Longsword");
            Prefab = CommunityDLC.assetBundle.LoadAsset<GameObject>("Assets/customBladeSilver.prefab");
            ObjectSlot = FTK_itembase.ObjectSlot.twoHands;
            ObjectType = FTK_itembase.ObjectType.weapon; // This is required for the item to be registered as a weapon
            SkillType = FTK_weaponStats2.SkillType.awareness;
            WeaponType = Weapon.WeaponType.bladed;
            ProficiencyEffects = new() { // these are the weapon attacks/skills for this custom item
                [(FTK_proficiencyTable.ID)customProf] = FTK_hitEffect.ID.bladeHeavyCrit,
                [(FTK_proficiencyTable.ID)customProf2] = FTK_hitEffect.ID.bladeHeavyProf
            };
            AnimationController = AssetManager.GetAnimationControllers<Weapon>().Find(i => i.name == "player_2H_Blunt_Combat");
            Slots = 4;
            MaxDmg = 26;
            DmgType = FTK_weaponStats2.DamageType.physical;
            DmgGain = 1;
            ShopStock = 1;
            TownMarket = true;
            DungeonMerchant = true;
            NightMarket = true;
            MaxLevel = 13;
            MinLevel = 3;
            GoldValue = 180;
            PriceScale = false;
            ItemRarity = FTK_itemRarityLevel.ID.rare;
            NoRegularAttack = true;
            Icon = CommunityDLC.assetBundleIcons.LoadAsset<Sprite>("Assets/Icons/weaponBlade.png");
            IconNonClickable = CommunityDLC.assetBundleIcons.LoadAsset<Sprite>("Assets/Icons/weaponBlade.png");
            //WeaponSize = (FTK_ragdollDeath.ID)3; //Causes game to not start
        }
    }
}
