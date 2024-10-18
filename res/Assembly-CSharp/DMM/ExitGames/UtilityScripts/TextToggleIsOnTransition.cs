// Decompiled with JetBrains decompiler
// Type: ExitGames.UtilityScripts.TextToggleIsOnTransition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace ExitGames.UtilityScripts
{
  [RequireComponent(typeof (Text))]
  public class TextToggleIsOnTransition : 
    MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IEventSystemHandler
  {
    public Toggle toggle;
    private Text _text;
    public Color NormalOnColor = Color.white;
    public Color NormalOffColor = Color.black;
    public Color HoverOnColor = Color.black;
    public Color HoverOffColor = Color.black;
    private bool isHover;

    public void OnEnable()
    {
      this._text = ((Component) this).GetComponent<Text>();
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.toggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnValueChanged)));
    }

    public void OnDisable()
    {
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.toggle.onValueChanged).RemoveListener(new UnityAction<bool>((object) this, __methodptr(OnValueChanged)));
    }

    public void OnValueChanged(bool isOn)
    {
      ((Graphic) this._text).color = !isOn ? (!this.isHover ? this.NormalOffColor : this.NormalOnColor) : (!this.isHover ? this.HoverOffColor : this.HoverOnColor);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      this.isHover = true;
      ((Graphic) this._text).color = !this.toggle.isOn ? this.HoverOffColor : this.HoverOnColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      this.isHover = false;
      ((Graphic) this._text).color = !this.toggle.isOn ? this.NormalOffColor : this.NormalOnColor;
    }
  }
}
