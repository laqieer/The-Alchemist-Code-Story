﻿// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_ToggleButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/Toggle Button (SRPG)")]
  public class SRPG_ToggleButton : SRPG_Button
  {
    private bool mIsOn;
    public bool AutoToggle;

    public bool IsOn
    {
      get
      {
        return this.mIsOn;
      }
      set
      {
        if (this.mIsOn == value)
          return;
        this.mIsOn = value;
        this.DoStateTransition(!this.mIsOn ? Selectable.SelectionState.Normal : Selectable.SelectionState.Pressed, false);
      }
    }

    protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
    {
      if (state == Selectable.SelectionState.Normal || state == Selectable.SelectionState.Highlighted || state == Selectable.SelectionState.Pressed)
        state = !this.mIsOn ? Selectable.SelectionState.Normal : Selectable.SelectionState.Pressed;
      base.DoStateTransition(state, instant);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
      if (this.IsInteractable() && this.AutoToggle)
        this.IsOn = !this.IsOn;
      base.OnPointerClick(eventData);
    }
  }
}
