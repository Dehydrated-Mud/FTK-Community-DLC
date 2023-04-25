using BepInEx.Logging;
using FTKAPI.Objects.SkillHooks;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Logger = FTKAPI.Utils.Logger;

namespace CommunityDLC.Objects.SkillTree
{
    public class HookAddRemoveModifiers: BaseModule
    {
        public override void Initialize()
        {
            Unload();
            On.CharacterOverworld.AddOrRemoveCharacterModifier += AddOrRemoveCharacterModifierHook;
            On.CharacterDummy.CreateAvatar +=CreateAvatarHook;
        }
        private void AddOrRemoveCharacterModifierHook(On.CharacterOverworld.orig_AddOrRemoveCharacterModifier orig, CharacterOverworld _this, FTK_itembase.ID _item, bool _add)
        {
            // This hook makes sure that when we equip weapons without modifiers stats are still updated (necessary for conditional modifiers to work properly)
            orig(_this, _item, _add);
            if (!FTK_characterModifierDB.GetDB().IsContainID(_item.ToString()))
            {
                _this.m_CharacterStats.UpdateAllCharacterStats();
            }
        }
        private void CreateAvatarHook(On.CharacterDummy.orig_CreateAvatar orig, CharacterDummy _this, bool _lightprobe)
        {
            orig(_this, _lightprobe);
            _this.m_CharacterOverworld.m_CharacterStats.UpdateAllCharacterStats();
        }
        public override void Unload()
        {
            On.CharacterOverworld.AddOrRemoveCharacterModifier -= AddOrRemoveCharacterModifierHook;
            On.CharacterDummy.CreateAvatar -= CreateAvatarHook;
        }
    }
}
