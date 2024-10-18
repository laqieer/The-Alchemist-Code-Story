// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ResetEquipment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG.AnimEvents
{
  public class ResetEquipment : AnimEvent
  {
    public bool IsNoResetPrimaryHand;
    public bool IsNoResetSecondaryHand;

    public override void OnStart(GameObject go)
    {
      GeneratedCharacter component = go.GetComponent<GeneratedCharacter>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        if (!this.IsNoResetPrimaryHand)
          component.ResetEquipmentLists(GeneratedCharacter.EquipmentType.PRIMARY);
        if (this.IsNoResetSecondaryHand)
          return;
        component.ResetEquipmentLists(GeneratedCharacter.EquipmentType.SECONDARY);
      }
      else
      {
        UnitController componentInParent = go.GetComponentInParent<UnitController>();
        if (Object.op_Equality((Object) componentInParent, (Object) null))
          return;
        if (!this.IsNoResetPrimaryHand)
          componentInParent.ResetEquipmentLists(UnitController.EquipmentType.PRIMARY);
        if (this.IsNoResetSecondaryHand)
          return;
        componentInParent.ResetEquipmentLists(UnitController.EquipmentType.SECONDARY);
      }
    }
  }
}
