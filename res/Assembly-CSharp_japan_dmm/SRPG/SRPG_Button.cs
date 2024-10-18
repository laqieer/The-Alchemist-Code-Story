// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_Button
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("UI/Button (SRPG)")]
  public class SRPG_Button : Button
  {
    private SRPG_Button.ButtonClickEvent mOnClick = (SRPG_Button.ButtonClickEvent) (b => { });
    [CustomEnum(typeof (SystemSound.ECue), -1)]
    public int ClickSound = -1;
    [BitMask]
    public CriticalSections CSMask;

    public void AddListener(SRPG_Button.ButtonClickEvent listener) => this.mOnClick += listener;

    public void RemoveListener(SRPG_Button.ButtonClickEvent listener) => this.mOnClick -= listener;

    private void PlaySound()
    {
      if (!((Selectable) this).IsInteractable() || this.ClickSound < 0)
        return;
      SystemSound.Play((SystemSound.ECue) this.ClickSound);
    }

    private bool IsCriticalSectionActive()
    {
      return (this.CSMask & CriticalSection.GetActive()) != (CriticalSections) 0;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
      if (this.IsCriticalSectionActive() || SRPG_InputField.IsFocus)
        return;
      this.PlaySound();
      if (((UIBehaviour) this).IsActive() && eventData.button == null)
        this.mOnClick(this);
      base.OnPointerClick(eventData);
    }

    public virtual void OnSubmit(BaseEventData eventData)
    {
      if (this.IsCriticalSectionActive() || SRPG_InputField.IsFocus)
        return;
      if (((UIBehaviour) this).IsActive())
        this.mOnClick(this);
      this.PlaySound();
      base.OnSubmit(eventData);
    }

    public virtual void UpdateButtonState()
    {
      Selectable.SelectionState selectionState = ((Selectable) this).currentSelectionState;
      if (((UIBehaviour) this).IsActive() && !((Selectable) this).IsInteractable())
        selectionState = (Selectable.SelectionState) 3;
      ((Selectable) this).DoStateTransition(selectionState, true);
    }

    public delegate void ButtonClickEvent(SRPG_Button go);
  }
}
