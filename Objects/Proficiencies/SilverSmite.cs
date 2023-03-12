using FTKAPI.Objects;
using HutongGames.PlayMaker.Actions;

namespace CommunityDLC {
    public class SilverSmite : CustomProficiency {
        public SilverSmite() {
            ID = "silversmite";
            Name = new("Silver Edge");
            SlotOverride = 4;
            IgnoresArmor = true;
            DmgTypeOverride = GridEditor.FTK_weaponStats2.DamageType.magic;
            ProficiencyPrefab = null;
            Category = Category.Resist;
            FullSlots = true;
            IsEndOnTurn = true;
            PerSlotSkillRoll = 0f;
            DmgMultiplier= 1f;
            Tint = new UnityEngine.Color(.7f, .7f, .8f, 1f);
        }
    }
}
