using FTKAPI.Objects;
using GridEditor;

namespace CommunityDLC {
    public class PlayerPaladin : CustomClass {
        public PlayerPaladin() {
            ID = "Paladin";
            Name = new CustomLocalizedString("Paladin");
            Description = new CustomLocalizedString("Sworn servant and protector of all Fahrul.");
            StartingGold = 3;
            Strength = 0.7f;
            Vitality = 0.84f;
            Intelligence = 0.40f;
            Awareness = 0.6f;
            Talent = 0.5f;
            Speed = 0.64f;
            Luck = 0.5f;
            IsMale = false;
            DefaultHeadSize = false;
        }
    }
}