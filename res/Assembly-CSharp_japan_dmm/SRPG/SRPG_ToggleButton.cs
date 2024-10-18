// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_ToggleButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("UI/Toggle Button (SRPG)")]
  public class SRPG_ToggleButton : SRPG_Button
  {
    private bool mIsOn;
    public bool AutoToggle;

    public bool IsOn
    {
      get => this.mIsOn;
      set
      {
        if (this.mIsOn == value)
          return;
        this.mIsOn = value;
        ((Selectable) this).DoStateTransition(!this.mIsOn ? (Selectable.SelectionState) 0 : (Selectable.SelectionState) 2, false);
      }
    }

    protected virtual void DoStateTransition(Selectable.SelectionState state, bool instant)
    {
      if (state == null || state == 1 || state == 2)
        state = !this.mIsOn ? (Selectable.SelectionState) 0 : (Selectable.SelectionState) 2;
      ((Selectable) this).DoStateTransition(state, instant);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
      if (((Selectable) this).IsInteractable() && this.AutoToggle)
        this.IsOn = !this.IsOn;
      base.OnPointerClick(eventData);
    }
  }
}
