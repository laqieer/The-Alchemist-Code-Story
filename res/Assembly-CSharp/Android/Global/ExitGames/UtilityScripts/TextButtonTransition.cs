// Decompiled with JetBrains decompiler
// Type: ExitGames.UtilityScripts.TextButtonTransition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExitGames.UtilityScripts
{
  [RequireComponent(typeof (Text))]
  public class TextButtonTransition : MonoBehaviour, IPointerExitHandler, IEventSystemHandler, IPointerEnterHandler
  {
    public Color NormalColor = Color.white;
    public Color HoverColor = Color.black;
    private Text _text;

    public void Awake()
    {
      this._text = this.GetComponent<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      this._text.color = this.HoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      this._text.color = this.NormalColor;
    }
  }
}
