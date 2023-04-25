using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using CommunityDLC.Objects.SkillTree;
using Logger = FTKAPI.Utils.Logger;
namespace CommunityDLC.UIElements.Behaviors;


public class TreeButton : MonoBehaviour
{
    public buttonState State = buttonState.Locked;
    public LeafButton leafButton;
    private Button button;
    private Image image;
    public uiToolTipGeneral tooltip;

    void Awake()
    {
        this.button = this.gameObject.AddComponent<Button>();
        this.image = this.gameObject.AddComponent<Image>();
        this.image.type = Image.Type.Sliced;
        this.button.transition = Selectable.Transition.ColorTint;
        this.button.targetGraphic = this.image;
        var colors = this.button.colors;
        colors.disabledColor = Color.grey;
        colors.normalColor = Color.green;
        colors.highlightedColor = Color.cyan;
        colors.pressedColor = Color.cyan;
        this.button.colors = colors;
        this.button.navigation = new Navigation
        {
            mode = Navigation.Mode.None
        };
        SetState(buttonState.Locked);

    }
    public void SetState()
    {
        //Logger.LogInfo("Attempting to set state: " + leafButton.State);
        SetState(leafButton.State);
    }
    public void SetState(buttonState state)
    {
        State = state;
        switch(state)
        {
            case (buttonState.Locked):
                this.button.enabled = true;
                this.button.interactable = false;
                break;
            case (buttonState.Unlocked):
                this.button.enabled = true;
                this.button.interactable = true;
                break;
            case (buttonState.Active):
                this.button.enabled = false;
                break;
        }
    }
}