using CommunityDLC.PhotonHooks;
using CommunityDLC.UIElements;
using FTKAPI.Objects;
using GridEditor;
using HutongGames.PlayMaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.Objects.Modifiers
{
    public class DLCCustomModifier : CustomModifier
    {
        //Used to determine which hook a conditional modifier is applied to
        // Defense -> modifications regarding Armor, resist, and evasion
        // Mods -> All else
        [Flags]
        public enum Method
        {
            None = 0,
            Defense = 1 << 0,
            Mods = 1 << 1
        }
        public Method m_Method = Method.None;
        private float m_JusticeChance = 0;
        private float m_RefocusChance = 0;
        private float m_SteadfastChance = 0;
        private float m_CalledShotChance = 0;
        private int m_DisciplineFocus = 0;

        [CustomModDisplayNameDLC(" Chance to trigger Called Shot", "", ModType.StatMod, CustomModType.None, _percent: true)]
        public float CalledShotChance { get => m_CalledShotChance; set => m_CalledShotChance = value; }

        [CustomModDisplayNameDLC(" Chance to trigger Justice", "", ModType.StatMod, CustomModType.None, _percent: true)]
        public float JusticeChance { get => m_JusticeChance; set => m_JusticeChance = value; }
        [CustomModDisplayNameDLC(" Chance to trigger Refocus", "", ModType.StatMod, CustomModType.None, _percent: true)]
        public virtual float RefocusChance { get => m_RefocusChance; set => m_RefocusChance = value; }

        [CustomModDisplayNameDLC(" Chance to trigger Steadfast", "", ModType.StatMod, CustomModType.None, _percent: true)]
        public float SteadfastChance { get => m_SteadfastChance; set => m_SteadfastChance = value;}

        [CustomModDisplayNameDLC(" Threshold focus to trigger Discipline", "", ModType.StatMod, CustomModType.None, _percent: false)]
        public int DisciplineFocus { get => m_DisciplineFocus; set => m_DisciplineFocus = value; }

        [CustomModDisplayNameDLC("Impervious Armor", "", ModType.StatMod, CustomModType.None, _percent: false)]
        public int ImperviousArmor { get; set; } = 0;

        [CustomModDisplayNameDLC("Impervious Resistance", "", ModType.StatMod, CustomModType.None, _percent: false)]
        public int ImperviousResist { get; set; } = 0;

        private WeaponMod m_Ice = new WeaponMod();
        private WeaponMod m_Fire = new WeaponMod();
        private WeaponMod m_Lightning = new WeaponMod();
        private WeaponMod m_Chaos = new WeaponMod();
        private WeaponMod m_Water = new WeaponMod();
        private WeaponMod m_None = new WeaponMod();
        
        private WeaponMod m_Bladed = new WeaponMod();
        private WeaponMod m_Blunt = new WeaponMod();
        private WeaponMod m_Magic = new WeaponMod();
        private WeaponMod m_Ranged = new WeaponMod();
        private WeaponMod m_Spear = new WeaponMod();
        private WeaponMod m_Unarmed = new WeaponMod();
        private WeaponMod m_Music = new WeaponMod();
        private WeaponMod m_Axe = new WeaponMod();
        private WeaponMod m_Staff = new WeaponMod();
        private WeaponMod m_Book = new WeaponMod();
        private WeaponMod m_Monster = new WeaponMod();
        private WeaponMod m_Firearm = new WeaponMod();
        private WeaponMod m_Wand = new WeaponMod();

        [CustomModDisplayNameDLC(" Damage with Ice Weapons", "", ModType.StatMod, CustomModType.WeaponMod, _percent: true)]
        public WeaponMod Ice { get => m_Ice; set => m_Ice = value; }

        [CustomModDisplayNameDLC(" Damage with Fire Weapons", "", ModType.StatMod, CustomModType.WeaponMod, _percent: true)]
        public WeaponMod Fire { get => m_Fire; set => m_Fire = value; }

        [CustomModDisplayNameDLC(" Damage with Lightning Weapons", "", ModType.StatMod, CustomModType.WeaponMod, _percent: true)]
        public WeaponMod Lightning { get => m_Lightning; set => m_Lightning = value; }

        [CustomModDisplayNameDLC(" Damage with Chaos Weapons", "", ModType.StatMod, CustomModType.WeaponMod, _percent: true)]
        public WeaponMod Chaos { get => m_Chaos; set => m_Chaos = value;  }

        [CustomModDisplayNameDLC(" Damage with Water Weapons", "", ModType.StatMod, CustomModType.WeaponMod, _percent: true)]
        public WeaponMod Water { get => m_Water; set => m_Water = value; }

        [CustomModDisplayNameDLC(" Damage with un-Enchanted Weapons", "", ModType.StatMod, CustomModType.WeaponMod, _percent: true)]
        public WeaponMod None { get => m_None; set => m_None = value; }

        [CustomModDisplayNameDLC(" Damage with swords", "", ModType.StatMod, CustomModType.WeaponMod, _percent: true)]
        public WeaponMod Bladed { get => m_Bladed; set => m_Bladed = value;  }

        [CustomModDisplayNameDLC(" Damage with blunt weapons", "", ModType.StatMod, CustomModType.WeaponMod, _percent: true)]
        public WeaponMod Blunt { get => m_Blunt; set => m_Blunt = value;  }

        [CustomModDisplayNameDLC(" Damage with tomes", "", ModType.StatMod, CustomModType.WeaponMod, _percent: true)]
        public WeaponMod Magic { get => m_Magic; set => m_Magic = value; }

        [CustomModDisplayNameDLC(" Damage with bows", "", ModType.StatMod, CustomModType.WeaponMod, _percent: true)]
        public WeaponMod Ranged { get => m_Ranged; set => m_Ranged = value;  }

        [CustomModDisplayNameDLC(" Damage with spears", "", ModType.StatMod, CustomModType.WeaponMod, _percent: true)]
        public WeaponMod Spear { get => m_Spear; set => m_Spear = value;  }

        [CustomModDisplayNameDLC(" Damage when unarmed", "", ModType.StatMod, CustomModType.WeaponMod, _percent: true)]
        public WeaponMod Unarmed { get => m_Unarmed; set => m_Unarmed = value;  }

        [CustomModDisplayNameDLC(" Damage with lutes", "", ModType.StatMod, CustomModType.WeaponMod, _percent: true)]
        public WeaponMod Music { get => m_Music; set => m_Music = value; }

        [CustomModDisplayNameDLC(" Damage with axes", "", ModType.StatMod, CustomModType.WeaponMod, _percent: true)]
        public WeaponMod Axe { get => m_Axe; set => m_Axe = value;  }

        [CustomModDisplayNameDLC(" Damage with staffs", "", ModType.StatMod, CustomModType.WeaponMod, _percent: true)]
        public WeaponMod Staff { get => m_Staff; set => m_Staff = value; }

        
        public WeaponMod Book { get => m_Book; set => m_Book = value; }

        public WeaponMod Monster { get => m_Monster; set => m_Monster = value; }

        [CustomModDisplayNameDLC(" Damage with firearms", "", ModType.StatMod, CustomModType.WeaponMod, _percent: true)]
        public WeaponMod Firearm { get => m_Firearm; set => m_Firearm = value;  }

        [CustomModDisplayNameDLC(" Damage with wands", "", ModType.StatMod, CustomModType.WeaponMod, _percent: true)]
        public WeaponMod Wand { get => m_Wand; set => m_Wand = value;  }

        public string m_DisplayName = "";
        private CustomLocalizedString name;
        public CustomLocalizedString Name
        {
            get => this.name;
            set
            {
                this.name = value;
                this.m_DisplayName = this.name.GetLocalizedString();
            }
        }
        
        public DLCCustomModifier(ID baseModifier = FTK_characterModifier.ID.None) : base(baseModifier) { }

        public virtual void ConditionalTally(ref CharacterStats _stats)
        {

        }
        public virtual void ConditionalTally(ref CharacterStats _stats, ref CustomCharacterStatsDLC _customStats, Method defense)
        {

        }
    }
}
