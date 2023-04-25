using FTKAPI.Managers;
using FTKAPI.Objects;
using GridEditor;
using UnityEngine;


namespace CommunityDLC {
    //using ProficiencyManager = FTKAPI.Managers.ProficiencyManager;
    public class HammerLightning : CustomItem {
        public HammerLightning() {
            //int customProf = ProficiencyManager.AddProficiency(new ProficiencySilverSmite());
            ID = "CustomHammerLightning";
            Name = new("The Defibrillator");
            Prefab = CommunityDLC.assetBundle.LoadAsset<GameObject>("Assets/customHammerLightning.prefab");
            ObjectSlot = FTK_itembase.ObjectSlot.oneHand;
            ObjectType = FTK_itembase.ObjectType.weapon; // This is required for the item to be registered as a weapon
            SkillType = FTK_weaponStats2.SkillType.vitality;
            WeaponType = Weapon.WeaponType.blunt;
            ProficiencyEffects = new() { // these are the weapon attacks/skills for this custom item
                //[(FTK_proficiencyTable.ID)customProf] = FTK_hitEffect.ID.bladeHeavyCrit,
                [FTK_proficiencyTable.ID.bluntLightningReg] = FTK_hitEffect.ID.bluntHeavyProf,
                [FTK_proficiencyTable.ID.lightningsplash1] = FTK_hitEffect.ID.bluntSplashProf
                
            };
            AnimationController = AssetManager.GetAnimationControllers<Weapon>().Find(i => i.name == "player_1H_Blunt_Combat");
            Slots = 3;
            MaxDmg = 30;
            DmgType = FTK_weaponStats2.DamageType.physical;
            DmgGain = 1;
            ShopStock = 1;
            TownMarket = true;
            DungeonMerchant = true;
            NightMarket = true;
            ItemRarity = FTK_itemRarityLevel.ID.rare;
            GoldValue = 233;
            MinLevel = 4;
            MaxLevel = 13;
            PriceScale = false;
            Icon = CommunityDLC.assetBundleIcons.LoadAsset<Sprite>("Assets/Icons/weaponBlunt.png");
            IconNonClickable = CommunityDLC.assetBundleIcons.LoadAsset<Sprite>("Assets/Icons/weaponBlunt.png");
            //WeaponSize = (FTK_ragdollDeath.ID)3; //Causes game to not start
        }
    }
}
