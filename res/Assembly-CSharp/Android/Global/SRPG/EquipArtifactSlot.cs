// Decompiled with JetBrains decompiler
// Type: SRPG.EquipArtifactSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class EquipArtifactSlot : GenericSlot
  {
    public SRPG_Button Lock;

    private void Awake()
    {
      if ((UnityEngine.Object) this.SelectButton != (UnityEngine.Object) null)
        this.SelectButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnButtonClick));
      if (!((UnityEngine.Object) this.Lock != (UnityEngine.Object) null))
        return;
      this.Lock.AddListener(new SRPG_Button.ButtonClickEvent(this.OnLockClick));
    }

    private void OnButtonClick(SRPG_Button button)
    {
      if (this.OnSelect == null || !button.interactable)
        return;
      this.OnSelect((GenericSlot) this, button.interactable);
    }

    private void OnLockClick(SRPG_Button button)
    {
      if (this.OnSelect == null)
        return;
      this.OnSelect((GenericSlot) this, false);
    }
  }
}
