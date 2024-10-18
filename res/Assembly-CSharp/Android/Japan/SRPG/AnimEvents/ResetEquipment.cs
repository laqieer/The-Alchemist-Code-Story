// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ResetEquipment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class ResetEquipment : AnimEvent
  {
    public bool IsNoResetPrimaryHand;
    public bool IsNoResetSecondaryHand;

    public override void OnStart(GameObject go)
    {
      GeneratedCharacter component = go.GetComponent<GeneratedCharacter>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
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
        if ((UnityEngine.Object) componentInParent == (UnityEngine.Object) null)
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
