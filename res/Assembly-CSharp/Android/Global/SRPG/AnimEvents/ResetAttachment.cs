// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ResetAttachment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class ResetAttachment : AnimEvent
  {
    public bool IsNoResetPrimaryHand;
    public bool IsNoResetSecondaryHand;

    public override void OnStart(GameObject go)
    {
      UnitController componentInParent = go.GetComponentInParent<UnitController>();
      if (!(bool) ((UnityEngine.Object) componentInParent))
        return;
      if (!this.IsNoResetPrimaryHand)
        componentInParent.ResetAttachmentLists(UnitController.EquipmentType.PRIMARY);
      if (this.IsNoResetSecondaryHand)
        return;
      componentInParent.ResetAttachmentLists(UnitController.EquipmentType.SECONDARY);
    }
  }
}
