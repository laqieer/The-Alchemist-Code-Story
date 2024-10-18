// Decompiled with JetBrains decompiler
// Type: ExitGames.UtilityScripts.TextButtonTransition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace ExitGames.UtilityScripts
{
  [RequireComponent(typeof (Text))]
  public class TextButtonTransition : 
    MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IEventSystemHandler
  {
    private Text _text;
    public Color NormalColor = Color.white;
    public Color HoverColor = Color.black;

    public void Awake() => this._text = ((Component) this).GetComponent<Text>();

    public void OnPointerEnter(PointerEventData eventData)
    {
      ((Graphic) this._text).color = this.HoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      ((Graphic) this._text).color = this.NormalColor;
    }
  }
}
