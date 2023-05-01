using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTKAPI.Managers;
using FTKAPI.Objects;
using GridEditor;

namespace CommunityDLC.Objects.Modifiers
{
    public class InitializeBasicStatMods
    {
        public static void Init()
        {
            ModifierManager.AddModifier(new BasicAwareness(0.04f, "GenericAwareness04"));
            ModifierManager.AddModifier(new BasicInt(0.02f, "GenericIntelligence02"));
            ModifierManager.AddModifier(new BasicLuck(0.1f, "GenericLuck10"));
            ModifierManager.AddModifier(new BasicEvasion(0.02f, "GenericEvasion02"));
            ModifierManager.AddModifier(new BasicStrength(0.02f, "GenericStrength02"));
            ModifierManager.AddModifier(new BasicTalent(0.02f, "GenericTalent02"));
            ModifierManager.AddModifier(new CustomModifier(FTK_characterModifier.ID.trinketFocus1)
            {
                ID = "GenericFocus01",
            }) ;
        }
    }
    public class BasicStrength : DLCCustomModifier
    {
        public BasicStrength(float strength, string id) 
        {
            ID = id;
            Strength = strength;
        }
    }

    public class BasicTalent : DLCCustomModifier
    {
        public BasicTalent(float value, string id)
        {
            ID = id;
            Talent = value;
        }
    }

    public class BasicInt : DLCCustomModifier
    {
        public BasicInt(float value, string id)
        {
            ID = id;
            Intelligence = value;
        }
    }
    public class BasicAwareness : DLCCustomModifier
    {
        public BasicAwareness(float value, string id)
        {
            ID = id;
            Awareness = value;
        }
    }

    public class BasicLuck : DLCCustomModifier
    {
        public BasicLuck(float value, string id)
        {
            ID = id;
            Luck = value;
        }
    }
    public class BasicVitality : DLCCustomModifier
    {
        public BasicVitality(float value, string id)
        {
            ID = id;
            Vitality = value;
        }
    }
    public class BasicSpeed : DLCCustomModifier
    {
        public BasicSpeed(float value, string id)
        {
            ID = id;
            Speed = value;
        }
    }
    public class BasicEvasion : DLCCustomModifier
    {
        public BasicEvasion (float value, string id)
        {
            ID = id;
            EvadeRating = value;
        }
    }

}
