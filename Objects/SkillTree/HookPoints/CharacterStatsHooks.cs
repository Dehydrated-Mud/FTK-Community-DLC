using CommunityDLC.Objects.Modifiers;
using CommunityDLC.PhotonHooks;
using FTKAPI.Objects.SkillHooks;
using FTKAPI.APIs.BattleAPI;
using FTKAPI.Objects;
using FTKAPI.Utils;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Logger = FTKAPI.Utils.Logger;
using WeaponType = Weapon.WeaponType;
using WeaponSubType = Weapon.WeaponSubType;
using Method = CommunityDLC.Objects.Modifiers.DLCCustomModifier.Method;
using static HitEffect;
using UnityEngine;
using MonoMod.Utils;

namespace CommunityDLC.Objects.SkillTree
{
    public class HookCharacterStatsSerialize : BaseModule
    {
        // This adds the modifier array to the serialized fields so that the data can persist.
        public override void Initialize()
        {
            Unload();
            On.CharacterStats.Awake += AwakeHook;
        }

        private void AwakeHook(On.CharacterStats.orig_Awake _orig, CharacterStats _this)
        {
            _orig(_this);
            FieldInfo modifiers = _this.GetType().GetField("m_CharacterMods", BindingFlags.Instance | BindingFlags.NonPublic);
            Logger.LogWarning("Hooked characterstats serialize, found field: " + modifiers.Name);
            _this.m_SerializeFields.Add(modifiers.Name, modifiers);
        }

        public override void Unload()
        {
            On.CharacterStats.Awake -= AwakeHook;
        }
    }

    public class HookTallyCharacterMods : BaseModule
    {
        
        public override void Initialize()
        {
            Unload();
            On.CharacterStats.TallyCharacterMods += TallyCharacterModsHook;
            On.CharacterStats.TallyCharacterDefense += TallyCharacterDefenseHook;
        }

        private void TallyCharacterModsHook(On.CharacterStats.orig_TallyCharacterMods _orig, CharacterStats _this)
        {
            _orig(_this);
            ModifierTree tree = TreeManager.Instance.m_modTrees[_this.m_CharacterOverworld.m_FTKPlayerID.TurnIndex]; //Get player's tree
            bool flag = false;
            CustomCharacterStatsDLC customStats = _this.gameObject.GetComponent<CustomCharacterStatsDLC>();
            if (customStats != null)
            {
                customStats.ClearMods();
                DLCUtils.ValidateWeaponType(_this.m_CharacterOverworld);
            }
            foreach (FTK_characterModifier.ID characterMod in _this.m_CharacterMods)
            {
                FTK_characterModifier entry = FTK_characterModifierDB.GetDB().GetEntry(characterMod);
                if (entry is Leaf) // Check if modifier is one of our indicator leafs. 
                {
                    Leaf entry2 = (Leaf)entry;
                    //Logger.LogInfo("Found a leaf named: " + entry2.LID + " for Player " + _this.m_CharacterOverworld.m_FTKPlayerID.TurnIndex);
                    LeafButton button = null;
                    try
                    {
                        button = tree.Map[characterMod];
                    }
                    catch(KeyNotFoundException)
                    {
                        Logger.LogWarning("Could not find a leafbutton in the dictionary for this leaf. Is it an aura modifier from another player?");
                    }
                    if (button != null)
                    {
                        //Logger.LogInfo("button not null, attempting to set state.");
                        flag = true;
                        button.SetState(entry2);
                    }
                }
                if (entry is DLCCustomModifier)
                {
                    //Logger.LogWarning("Found a DLCCustomModifier");
                    if (customStats)
                    {
                        //Logger.LogWarning("CustomStats not null");
                        DLCCustomModifier entry3 = (DLCCustomModifier)entry;
                        customStats.JusticeChance += entry3.JusticeChance;
                        customStats.RefocusChance += entry3.RefocusChance;
                        customStats.DisciplineFocus += entry3.DisciplineFocus;
                        customStats.SteadfastChance += entry3.SteadfastChance;
                        customStats.CalledShotChance += entry3.CalledShotChance;
                        customStats.LifestealFac += entry3.LifeStealFac;
                        customStats.DIThreshold += entry3.DIThreshold;
                        customStats.DISelf |= entry3.DISelf;
                        customStats.FocusHeal += entry3.FocusHeal;
                        customStats.InspireXP += entry3.InspireXP;
                        customStats.InspireChance += entry3.InspireChance;
                        customStats.EncourageChance += entry3.EncourageChance;
                        customStats.DistractChance += entry3.DistractChance;
                        
                        customStats.DamageModifiers[WeaponType.axe].Add(entry3.Axe);
                        customStats.DamageModifiers[WeaponType.bladed].Add(entry3.Bladed);
                        customStats.DamageModifiers[WeaponType.music].Add(entry3.Music);
                        customStats.DamageModifiers[WeaponType.magic].Add(entry3.Magic);
                        customStats.DamageModifiers[WeaponType.ranged].Add(entry3.Ranged);
                        customStats.DamageModifiers[WeaponType.spear].Add(entry3.Spear);
                        customStats.DamageModifiers[WeaponType.unarmed].Add(entry3.Unarmed);
                        customStats.DamageModifiers[WeaponType.staff].Add(entry3.Staff);
                        customStats.DamageModifiers[WeaponType.book].Add(entry3.Book);
                        customStats.DamageModifiers[WeaponType.monster].Add(entry3.Monster);
                        customStats.DamageModifiers[WeaponType.firearm].Add(entry3.Firearm);
                        customStats.DamageModifiers[WeaponType.wand].Add(entry3.Wand);
                        customStats.DamageModifiers[WeaponType.blunt].Add(entry3.Blunt);
                        customStats.SubDamageModifiers[WeaponSubType.fire].Add(entry3.Fire);
                        customStats.SubDamageModifiers[WeaponSubType.water].Add(entry3.Water);
                        customStats.SubDamageModifiers[WeaponSubType.ice].Add(entry3.Ice);
                        customStats.SubDamageModifiers[WeaponSubType.none].Add(entry3.None);
                        customStats.SubDamageModifiers[WeaponSubType.chaos].Add(entry3.Chaos);
                        customStats.SubDamageModifiers[WeaponSubType.lightning].Add(entry3.Lightning);

                        if (entry3.Conditional && (entry3.m_Method & Method.Mods) == Method.Mods)
                        {
                            entry3.ConditionalTally(ref _this, ref customStats, Method.Mods);
                        }
                    }
                }
            }
            
            if (flag && tree.SkillUI.Panel != null)
            {
                tree.SkillUI.Update();
            }
            if (customStats)
            {
                _this.m_ModAttackAll += GetDamage(_this.m_CharacterOverworld, customStats);
            }
            //TODO: Tally custom and apply
        }

        private void TallyCharacterDefenseHook(On.CharacterStats.orig_TallyCharacterDefense _orig, CharacterStats _this)
        {
            _orig(_this);
            ModifierTree tree = TreeManager.Instance.m_modTrees[_this.m_CharacterOverworld.m_FTKPlayerID.TurnIndex]; //Get player's tree
            CustomCharacterStatsDLC customStats = _this.gameObject.GetComponent<CustomCharacterStatsDLC>();
            if (customStats != null)
            {
                customStats.ClearDefense();

                foreach (FTK_characterModifier.ID characterMod in _this.m_CharacterMods)
                {
                    FTK_characterModifier entry = FTK_characterModifierDB.GetDB().GetEntry(characterMod);
                    if (entry is DLCCustomModifier) // Check if modifier is one of our indicator leafs. 
                    {
                        DLCCustomModifier entry2 = (DLCCustomModifier)entry;
                        customStats.ImperviousArmor += entry2.ImperviousArmor;
                        customStats.ImperviousResist += entry2.ImperviousResist;
                        if (entry2.Conditional && ((entry2.m_Method & Method.Defense) == Method.Defense))
                        {
                            Logger.LogInfo("Attempting to call conditionalTally");
                            entry2.ConditionalTally(ref _this, ref customStats, Method.Defense);
                        }
                    }
                }
                int[] customDefenseMods = GetDefense(_this.m_CharacterOverworld, customStats);
                
                _this.m_ModAttackPhysical += customDefenseMods[0];
                _this.m_ModAttackMagic += customDefenseMods[1];
                _this.m_ModEvadeRating += customStats.EvasionMod; // Is this necessary? Can't we just add directly to character stats in the conditional tally?
            }
        }
        public override void Unload()
        {
            On.CharacterStats.TallyCharacterMods -= TallyCharacterModsHook;
        }

        /// <summary>
        /// Returns the ADDITIONAL damage
        /// </summary>
        /// <param name="player"></param>
        /// <param name="stats"></param>
        /// <returns></returns>
        private int GetDamage(CharacterOverworld player, CustomCharacterStatsDLC stats)
        {
            FTK_weaponStats2 entry = FTK_weaponStats2DB.GetDB().GetEntry(player.m_WeaponID);
            CharacterEventListener listener = BattleHelpers.GetListener(player);
            WeaponMod mod = new WeaponMod();
            if (listener)
            {
                Weapon.WeaponType weaponType = listener.m_Weapon.m_WeaponType;
                Weapon.WeaponSubType weaponSubType = listener.m_Weapon.m_WeaponSubType;
                Logger.LogWarning(stats.DamageModifiers[weaponType] is null);
                WeaponMod mainMod = stats.DamageModifiers[weaponType];
                WeaponMod subMod = stats.SubDamageModifiers[weaponSubType];

                mod.Add(mainMod, subMod);
                // Additional damage by percent
                float dmg = Math.Max(0f,(entry._dmggain * (float)player.m_CharacterStats.m_PlayerLevel + entry._maxdmg + mod.m_AtkAdd)) * mod.m_AtkFac;
                // the mod.m_AtkAdd is to make sure that the added damage is included in the percent increase, but we still have to add it to the total increase
                dmg += mod.m_AtkAdd;
                Logger.LogInfo($"Returning {FTKUtil.RoundToInt(dmg)} damage");
                return FTKUtil.RoundToInt(dmg);
            }
            Logger.LogWarning("Could not get player dummy, returning 0 for weapon type modifier");
            return 0;
        }

        private int[] GetDefense(CharacterOverworld _player, CustomCharacterStatsDLC _stats)
        {
            CharacterStats characterStats = _player.m_CharacterStats;
            FTK_weaponStats2 weaponStats = FTK_weaponStats2DB.GetDB().GetEntry(_player.m_WeaponID);
            Weapon weapon = BattleHelpers.GetWeapon(_player);
            if(weapon != null)
            {
                Weapon.WeaponType weaponType = weapon.m_WeaponType;
                Weapon.WeaponSubType weaponSubType = weapon.m_WeaponSubType;

                WeaponMod mainMod = _stats.DamageModifiers[weaponType];
                WeaponMod subMod = _stats.SubDamageModifiers[weaponSubType];
                WeaponMod mod = new WeaponMod();
                mod.Add(mainMod, subMod);

                float armorFromFactor = (characterStats.TotalArmor + mod.m_DefAdd) * mod.m_DefFac;
                float newArmor = armorFromFactor + mod.m_DefAdd;

                float resistFromFactor = (characterStats.TotalResist + mod.m_DefAdd) * mod.m_DefFac;
                float newResist = resistFromFactor + mod.m_DefAdd;
                Logger.LogWarning($"Granting +{newArmor} physical and +{newResist} magical defense");
                return new int[2] { (int)newArmor, (int)newResist };
            }
            Logger.LogWarning("Could not get player dummy, returning 0 for weapon type modifier");
            return new int[2] { 0, 0 };
        }
    }
}
