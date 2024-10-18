// Decompiled with JetBrains decompiler
// Type: ExitGames.UtilityScripts.TextToggleIsOnTransition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExitGames.UtilityScripts
{
  [RequireComponent(typeof (Text))]
  public class TextToggleIsOnTransition : MonoBehaviour, IPointerExitHandler, IEventSystemHandler, IPointerEnterHandler
  {
    public Color NormalOnColor = Color.white;
    public Color NormalOffColor = Color.black;
    public Color HoverOnColor = Color.black;
    public Color HoverOffColor = Color.black;
    public Toggle toggle;
    private Text _text;
    private bool isHover;

    public void OnEnable()
    {
      this._text = this.GetComponent<Text>();
      this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
    }

    public void OnDisable()
    {
      this.toggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged));
    }

    public void OnValueChanged(bool isOn)
    {
      this._text.color = !isOn ? (!this.isHover ? this.NormalOffColor : this.NormalOnColor) : (!this.isHover ? this.HoverOffColor : this.HoverOnColor);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      this.isHover = true;
      this._text.color = !this.toggle.isOn ? this.HoverOffColor : this.HoverOnColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      this.isHover = false;
      this._text.color = !this.toggle.isOn ? this.NormalOffColor : this.NormalOnColor;
    }
  }
}
