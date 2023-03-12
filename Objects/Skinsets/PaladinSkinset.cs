using FTKAPI.Objects;
using GridEditor;
using FTKAPI.Managers;
using System.Reflection;
using UnityEngine;
using Logger = FTKAPI.Utils.Logger;
using FullInspector;
using FTKAPI.Utils;

namespace CommunityDLC
{
    public class PaladinSkinset : CustomSkinset
    {
        public PaladinSkinset()
        {
            ID = "paladin_Female";
            // All skinset items below can be set via a custom prefab, or with a skinset ID like:
            // Avatar = MakeAvatar(FTK_skinset.ID.blacksmith_Male); // This will set the male blacksmith as the avatar for this skinset (the avatar will be shared between the two classes)
            
            Avatar = MakeAvatar(CommunityDLC.assetBundleSkins.LoadAsset<GameObject>("Assets/playerPaladin.prefab"));
            Armor = MakeArmor(CommunityDLC.assetBundleSkins.LoadAsset<GameObject>("Assets/armorPaladin1.prefab"));
            Boot = MakeBoots(CommunityDLC.assetBundleSkins.LoadAsset<GameObject>("Assets/bootsPaladin.prefab"));
            Helmet = MakeHelmet(CommunityDLC.assetBundleSkins.LoadAsset<GameObject>("Assets/helmetPaladin.prefab"));
            Backpack = MakeBackpack(new GameObject());
        }
    }
}