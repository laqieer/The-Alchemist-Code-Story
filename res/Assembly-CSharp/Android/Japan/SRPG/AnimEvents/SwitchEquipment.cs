// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.SwitchEquipment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class SwitchEquipment : AnimEvent
  {
    public SwitchEquipment.eSwitchTarget SwitchPrimaryHand;
    public SwitchEquipment.eSwitchTarget SwitchSecondaryHand;

    public override void OnStart(GameObject go)
    {
      GeneratedCharacter component = go.GetComponent<GeneratedCharacter>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
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
        if ((UnityEngine.Object) componentInParent == (UnityEngine.Object) null)
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
