// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.SwitchEquipment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class SwitchEquipment : AnimEvent
  {
    public SwitchEquipment.eSwitchTarget SwitchPrimaryHand;
    public SwitchEquipment.eSwitchTarget SwitchSecondaryHand;

    public override void OnStart(GameObject go)
    {
      UnitController componentInParent = go.GetComponentInParent<UnitController>();
      if (!(bool) ((UnityEngine.Object) componentInParent))
        return;
      if (this.SwitchPrimaryHand != SwitchEquipment.eSwitchTarget.NO_CHANGE)
        componentInParent.SwitchEquipmentLists(UnitController.EquipmentType.PRIMARY, (int) this.SwitchPrimaryHand);
      if (this.SwitchSecondaryHand == SwitchEquipment.eSwitchTarget.NO_CHANGE)
        return;
      componentInParent.SwitchEquipmentLists(UnitController.EquipmentType.SECONDARY, (int) this.SwitchSecondaryHand);
    }

    public enum eSwitchTarget
    {
      NO_CHANGE,
      Element_0,
      Element_1,
      Element_2,
      Element_3,
      Element_4,
      Element_5,
      Element_6,
      Element_7,
      Element_8,
      Element_9,
    }
  }
}
