// Decompiled with JetBrains decompiler
// Type: SRPG.EquipArtifactSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EquipArtifactSlot : GenericSlot
  {
    public SRPG_Button Lock;

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.SelectButton, (Object) null))
        this.SelectButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnButtonClick));
      if (!Object.op_Inequality((Object) this.Lock, (Object) null))
        return;
      this.Lock.AddListener(new SRPG_Button.ButtonClickEvent(this.OnLockClick));
    }

    private void OnButtonClick(SRPG_Button button)
    {
      if (this.OnSelect == null || !((Selectable) button).interactable)
        return;
      this.OnSelect((GenericSlot) this, ((Selectable) button).interactable);
    }

    private void OnLockClick(SRPG_Button button)
    {
      if (this.OnSelect == null)
        return;
      this.OnSelect((GenericSlot) this, false);
    }
  }
}
