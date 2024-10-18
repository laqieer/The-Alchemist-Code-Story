// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleWeaponFrame
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class ToggleWeaponFrame : AnimEvent
  {
    public ToggleWeaponFrame.SHOW_TYPE Primary;
    public ToggleWeaponFrame.SHOW_TYPE Secondary;

    public override void OnStart(GameObject go)
    {
      UnitController componentInParent = go.GetComponentInParent<UnitController>();
      if ((UnityEngine.Object) componentInParent == (UnityEngine.Object) null)
        return;
      if (this.Primary != ToggleWeaponFrame.SHOW_TYPE.KEEP)
      {
        bool visible = this.Primary != ToggleWeaponFrame.SHOW_TYPE.HIDDEN;
        componentInParent.SetPrimaryEquipmentsVisible(visible);
      }
      if (this.Secondary == ToggleWeaponFrame.SHOW_TYPE.KEEP)
        return;
      bool visible1 = this.Secondary != ToggleWeaponFrame.SHOW_TYPE.HIDDEN;
      componentInParent.SetSecondaryEquipmentsVisible(visible1);
    }

    public override void OnEnd(GameObject go)
    {
      UnitController componentInParent = go.GetComponentInParent<UnitController>();
      if ((UnityEngine.Object) componentInParent == (UnityEngine.Object) null)
        return;
      if (this.Primary != ToggleWeaponFrame.SHOW_TYPE.KEEP)
      {
        bool visible = this.Primary == ToggleWeaponFrame.SHOW_TYPE.HIDDEN;
        componentInParent.SetPrimaryEquipmentsVisible(visible);
      }
      if (this.Secondary == ToggleWeaponFrame.SHOW_TYPE.KEEP)
        return;
      bool visible1 = this.Secondary == ToggleWeaponFrame.SHOW_TYPE.HIDDEN;
      componentInParent.SetSecondaryEquipmentsVisible(visible1);
    }

    public enum SHOW_TYPE
    {
      KEEP,
      HIDDEN,
      VISIBLE,
    }
  }
}
