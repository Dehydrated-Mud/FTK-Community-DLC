using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking.Match;
using CommunityDLC.UIElements;
using WeaponType = Weapon.WeaponType;
using WeaponSubType = Weapon.WeaponSubType;
using System.Reflection;
using FTKAPI.Objects;
using Logger = FTKAPI.Utils.Logger;
namespace CommunityDLC.PhotonHooks
{
    public class CustomCharacterStatsDLC : CustomCharacterStats
    {
        public enum EventType
        {
            None = 0,
            AttackFac,
            AttackAdd,
            DefendFac,
            DefendAdd
        }

        public float m_JusticeChance;
        public float m_RefocusChance;

        public int m_DisciplineFocus;

        public float m_EvasionMod;

        public bool GuaranteeCrit { get; set; }
        public Dictionary<WeaponType, WeaponMod> DamageModifiers { get; set; } = new Dictionary<WeaponType, WeaponMod>();
        public Dictionary<WeaponSubType, WeaponMod> SubDamageModifiers { get; set; } = new Dictionary<WeaponSubType, WeaponMod>();
        public void Awake()
        {
            ClearMods();
            ClearDefense();
        }
        public void ClearMods()
        {
            DamageModifiers = new Dictionary<WeaponType, WeaponMod> {
                {WeaponType.music, new WeaponMod() },
                {WeaponType.book, new WeaponMod() },
                {WeaponType.spear, new WeaponMod() },
                {WeaponType.none, new WeaponMod() },
                {WeaponType.monster, new WeaponMod() },
                {WeaponType.axe, new WeaponMod() },
                {WeaponType.blunt, new WeaponMod() },
                {WeaponType.bladed, new WeaponMod() },
                {WeaponType.firearm, new WeaponMod() },
                {WeaponType.magic, new WeaponMod() },
                {WeaponType.ranged, new WeaponMod() },
                {WeaponType.staff, new WeaponMod() },
                {WeaponType.unarmed, new WeaponMod() },
                {WeaponType.wand, new WeaponMod() }
                };
            SubDamageModifiers = new Dictionary<WeaponSubType, WeaponMod>
            {
                {WeaponSubType.none, new WeaponMod() },
                {WeaponSubType.fire, new WeaponMod() },
                {WeaponSubType.water, new WeaponMod() },
                {WeaponSubType.lightning, new WeaponMod() },
                {WeaponSubType.chaos, new WeaponMod() },
                {WeaponSubType.ice, new WeaponMod() },
            };
            m_JusticeChance = 0f;
            m_RefocusChance = 0f;

            m_DisciplineFocus = 0;
            
        }
        public void ClearDefense()
        {
            Logger.LogWarning("Clearing Defense");
            m_EvasionMod = 0f;
        }
    }

    public class WeaponMod
    {
        public float m_AtkFac { get; set; }
        public float m_AtkAdd { get; set; }
        public float m_DefFac { get; set; }
        public float m_DefAdd { get; set; }
        public WeaponMod()
        {
            m_AtkAdd = 0f;
            m_DefAdd = 0f;
            m_AtkFac = 0f;
            m_DefFac = 0f;
        }

        public void Add(WeaponMod mod)
        {
            m_AtkAdd += mod.m_AtkAdd;
            m_AtkFac += mod.m_AtkFac;
            m_DefAdd += mod.m_DefAdd;
            m_DefFac += mod.m_DefFac;
        }
        public void Add(WeaponMod mod1, WeaponMod mod2)
        {
            m_AtkAdd += mod1.m_AtkAdd + mod2.m_AtkAdd;
            m_AtkFac += mod1.m_AtkFac + mod2.m_AtkFac;
            m_DefAdd += mod1.m_DefAdd + mod2.m_DefAdd;
            m_DefFac += mod1.m_DefFac + mod2.m_DefFac;
        }
    }
}
