// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.SwitchEquipment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG.AnimEvents
{
  public class SwitchEquipment : AnimEvent
  {
    public SwitchEquipment.eSwitchTarget SwitchPrimaryHand;
    public SwitchEquipment.eSwitchTarget SwitchSecondaryHand;

    public override void OnStart(GameObject go)
    {
      GeneratedCharacter component = go.GetComponent<GeneratedCharacter>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        if (this.SwitchPrimaryHand != SwitchEquipment.eSwitchTarget.NO_CHANGE)
          component.SwitchEquipmentLists(GeneratedCharacter.EquipmentType.PRIMARY, (int) this.SwitchPrimaryHand);
        if (this.SwitchSecondaryHand == SwitchEquipment.eSwitchTarget.NO_CHANGE)
          return;
        component.SwitchEquipmentLists(GeneratedCharacter.EquipmentType.SECONDARY, (int) this.SwitchSecondaryHand);
      }
      else
      {
        UnitController componentInParent = go.GetComponentInParent<UnitController>();
        if (Object.op_Equality((Object) componentInParent, (Object) null))
          return;
        if (this.SwitchPrimaryHand != SwitchEquipment.eSwitchTarget.NO_CHANGE)
          componentInParent.SwitchEquipmentLists(UnitController.EquipmentType.PRIMARY, (int) this.SwitchPrimaryHand);
        if (this.SwitchSecondaryHand == SwitchEquipment.eSwitchTarget.NO_CHANGE)
          return;
        componentInParent.SwitchEquipmentLists(UnitController.EquipmentType.SECONDARY, (int) this.SwitchSecondaryHand);
      }
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
