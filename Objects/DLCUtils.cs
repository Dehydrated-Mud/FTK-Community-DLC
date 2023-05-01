using System;
using FTKAPI.APIs.BattleAPI;
using FTKAPI.Utils;
using FTKAPI.Managers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google2u;
using GridEditor;
using CommunityDLC.Objects.SkillTree;

namespace CommunityDLC.Objects
{
    public class DLCUtils
    {

        public static List<string> fire = new List<string> { "fire", "volcanic", "burnt" };
        public static List<string> ice = new List<string> { "ice", "frost", "frozen", "glacier" };
        public static List<string> water = new List<string> { "water" };
        public static List<string> lightning = new List<string> { "lightning", "thunder", "storm", "shock" };
        /// <summary>
        /// Returns the amount to heal a character by based a percentage of their health
        /// </summary>
        /// <param name="_dum">dummy to heal</param>
        /// <param name="_fac">health percentage</param>
        /// <returns></returns>
        public static int HealByPercentage(CharacterDummy _dum, float _fac)
        {
            return (int)Math.Min((float)(_dum.m_CharacterOverworld.m_CharacterStats.MaxHealth * _fac), (float)(_dum.m_CharacterOverworld.m_CharacterStats.MaxHealth - _dum.GetCurrentHealth()));
        }

        public static FTK_characterModifier.ID GetMod(string id)
        {
            return (FTK_characterModifier.ID)ModifierManager.Instance.enums[id];
        }

        public static void AddModifierToAllPlayers (FTK_characterModifier.ID _id)
        {
            foreach (CharacterOverworld player in FTKHub.Instance.m_CharacterOverworlds)
            {
                TreeManager.Instance.Syncer.AddRemoveModifier(player.m_FTKPlayerID, _id, adding: true);
            }
        }

        public static void ValidateWeaponType(CharacterOverworld _player)
        {

            bool checkSubtype = false;
            string id = _player.m_WeaponID.ToString();
            Weapon weapon = BattleHelpers.GetWeapon(_player);
            if (weapon != null)
            {
                if (id.Contains("wand", StringComparison.OrdinalIgnoreCase))
                {
                    weapon.m_WeaponType = Weapon.WeaponType.wand;
                    checkSubtype = true;
                }
                else if (id.Contains("staff", StringComparison.OrdinalIgnoreCase))
                {
                    weapon.m_WeaponType = Weapon.WeaponType.staff;
                    checkSubtype = true;
                }
                else if (id.Contains("tome", StringComparison.OrdinalIgnoreCase) || id.Contains("book", StringComparison.OrdinalIgnoreCase))
                {
                    weapon.m_WeaponType = Weapon.WeaponType.magic;
                    checkSubtype = true;
                }

                if (checkSubtype)
                {

                    ValidateWeaponSubType(id, weapon);
                }
                
            }
        }

        private static void ValidateWeaponSubType(string id, Weapon weapon)
        {

            if (id.ContainsAny(fire))
            {
                weapon.m_WeaponSubType = Weapon.WeaponSubType.fire;
            }
            else if (id.ContainsAny(lightning))
            {
                weapon.m_WeaponSubType = Weapon.WeaponSubType.lightning;
            }
            else if (id.ContainsAny(ice))
            {
                weapon.m_WeaponSubType= Weapon.WeaponSubType.ice;
            }
            else if (id.ContainsAny(water))
            {
                weapon.m_WeaponSubType = Weapon.WeaponSubType.water;
            }
        }
    }
}
