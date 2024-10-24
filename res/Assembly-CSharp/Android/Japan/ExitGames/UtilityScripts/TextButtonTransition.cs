﻿// Decompiled with JetBrains decompiler
// Type: ExitGames.UtilityScripts.TextButtonTransition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExitGames.UtilityScripts
{
  [RequireComponent(typeof (Text))]
  public class TextButtonTransition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
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
