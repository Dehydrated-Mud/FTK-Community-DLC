using FTKAPI.Objects;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeafID = CommunityDLC.Objects.SkillTree.Leaf.LeafID;
using UnityEngine;
using Google2u;
using Logger = FTKAPI.Utils.Logger;
using IL.Rewired.UI.ControlMapper;

namespace CommunityDLC.Objects.SkillTree.Leaves
{
    public class GenericIndicator : Leaf
    {
        public GenericIndicator(LeafID id) : base(id) 
        {
            State = buttonState.Unlocked;
        }
    }

    public class GenericMaxFocus : Leaf
    {
        public GenericMaxFocus(LeafID id, int focus) : base(id)
        {
            ExtraFocus = focus;
        }
    }

    public class GenericTalent : Leaf
    {
        public GenericTalent(LeafID id, float talent) : base(id)
        {
            Talent = talent;
        }
    }
    public class GenericAwareness : Leaf
    {
        public GenericAwareness(LeafID id, float awareness) : base(id)
        {
            Awareness = awareness;
        }
    }
    public class GenericVitality : Leaf
    {
        public GenericVitality(LeafID id, float vitality) : base(id)
        {
            Vitality = vitality;
        }
    }
    public class GenericRefocusChance : Leaf
    {
        public GenericRefocusChance(LeafID LeafID, float chance) : base(LeafID)
        {
            RefocusChance = chance;
        }
    }
    public class GenericGiveItem : Leaf
    {
        FTK_itembase.ID item = FTK_itembase.ID.staffGoatWizard;
        bool disclose;
        string itemName = "Item";
        public GenericGiveItem(LeafID leafID, FTK_itembase.ID item, bool disclose = true) : base(leafID)
        {
            OnAdd = true;
            this.item = item;
            this.disclose = disclose;
        }

        public override void AddAction(CharacterOverworld player)
        {
            player.AddItemToBackpack(item);
        }

        // Out leaves are instantiated before the itemDB, so we cannot directly set the name at instantiation. This function is called in CustomModDisplayName
        public string GetLocalizedName()
        {
            if(m_DisplayName != "")
            {
                return m_DisplayName;
            }
            string name = "Gives a mysterious item...";
            if (disclose)
            {
                name = "Gives " + FTKUI.GetKeyInfoRichText(Color.magenta, _bold: false, FTKHub.Localized<TextItems>("STR_" + item.ToString()));
            }
            return name;
        }
    }
}
