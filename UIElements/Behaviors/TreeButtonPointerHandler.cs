using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CommunityDLC.UIElements.Behaviors;

public class TreeButtonPointerHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Action<TreeButton, PointerEventData.InputButton> Callback;
    public Action<TreeButton> HoverCallback;
    public Action<TreeButton> BlurCallback;
    public TreeButton Button { get; set; }

    public void OnPointerClick(PointerEventData eventData) => this.Callback?.Invoke(this.Button, eventData.button);

    public void OnPointerEnter(PointerEventData eventData) => this.HoverCallback?.Invoke(this.Button);

    public void OnPointerExit(PointerEventData eventData) => this.BlurCallback?.Invoke(this.Button);
}