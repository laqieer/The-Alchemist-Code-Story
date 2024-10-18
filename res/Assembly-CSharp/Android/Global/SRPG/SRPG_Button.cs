// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_Button
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/Button (SRPG)")]
  public class SRPG_Button : Button
  {
    private SRPG_Button.ButtonClickEvent mOnClick = (SRPG_Button.ButtonClickEvent) (b => {});
    [CustomEnum(typeof (SystemSound.ECue), -1)]
    public int ClickSound = -1;
    [BitMask]
    public CriticalSections CSMask;

    public void AddListener(SRPG_Button.ButtonClickEvent listener)
    {
      this.mOnClick += listener;
    }

    public void RemoveListener(SRPG_Button.ButtonClickEvent listener)
    {
      this.mOnClick -= listener;
    }

    private void PlaySound()
    {
      if (!this.IsInteractable() || this.ClickSound < 0)
        return;
      SystemSound.Play((SystemSound.ECue) this.ClickSound);
    }

    private bool IsCriticalSectionActive()
    {
      return (this.CSMask & CriticalSection.GetActive()) != (CriticalSections) 0;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
      if (this.IsCriticalSectionActive())
        return;
      if (this.IsActive() && eventData.button == PointerEventData.InputButton.Left)
        this.mOnClick(this);
      this.PlaySound();
      base.OnPointerClick(eventData);
    }

    public override void OnSubmit(BaseEventData eventData)
    {
      if (this.IsCriticalSectionActive())
        return;
      if (this.IsActive())
        this.mOnClick(this);
      this.PlaySound();
      base.OnSubmit(eventData);
    }

    public virtual void UpdateButtonState()
    {
      Selectable.SelectionState state = this.currentSelectionState;
      if (this.IsActive() && !this.IsInteractable())
        state = Selectable.SelectionState.Disabled;
      this.DoStateTransition(state, true);
    }

    public delegate void ButtonClickEvent(SRPG_Button go);
  }
}
