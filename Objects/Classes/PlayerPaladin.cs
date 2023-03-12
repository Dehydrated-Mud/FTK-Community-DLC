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
            Vitality = 0.82f;
            Intelligence = 0.55f;
            Awareness = 0.6f;
            Talent = 0.65f;
            Speed = 0.65f;
            Luck = 0.5f;
            IsMale = false;
            DefaultHeadSize = false;
        }
    }
}