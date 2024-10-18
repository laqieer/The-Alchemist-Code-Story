// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleWeaponFrame
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG.AnimEvents
{
  public class ToggleWeaponFrame : AnimEvent
  {
    public ToggleWeaponFrame.SHOW_TYPE Primary;
    public ToggleWeaponFrame.SHOW_TYPE Secondary;
    private UnitController mUnitController;
    private GeneratedCharacter mGeneratedCharacter;

    public override void OnStart(GameObject go)
    {
      this.mGeneratedCharacter = go.GetComponent<GeneratedCharacter>();
      if (Object.op_Inequality((Object) this.mGeneratedCharacter, (Object) null))
      {
        if (this.Primary != ToggleWeaponFrame.SHOW_TYPE.KEEP)
          this.mGeneratedCharacter.SetPrimaryEquipmentsVisible(this.Primary != ToggleWeaponFrame.SHOW_TYPE.HIDDEN);
        if (this.Secondary == ToggleWeaponFrame.SHOW_TYPE.KEEP)
          return;
        this.mGeneratedCharacter.SetSecondaryEquipmentsVisible(this.Secondary != ToggleWeaponFrame.SHOW_TYPE.HIDDEN);
      }
      else
      {
        this.mUnitController = go.GetComponentInParent<UnitController>();
        if (Object.op_Equality((Object) this.mUnitController, (Object) null))
          return;
        if (this.Primary != ToggleWeaponFrame.SHOW_TYPE.KEEP)
          this.mUnitController.SetPrimaryEquipmentsVisible(this.Primary != ToggleWeaponFrame.SHOW_TYPE.HIDDEN);
        if (this.Secondary == ToggleWeaponFrame.SHOW_TYPE.KEEP)
          return;
        this.mUnitController.SetSecondaryEquipmentsVisible(this.Secondary != ToggleWeaponFrame.SHOW_TYPE.HIDDEN);
      }
    }

    public override void OnEnd(GameObject go)
    {
      if (Object.op_Inequality((Object) this.mGeneratedCharacter, (Object) null))
      {
        if (this.Primary != ToggleWeaponFrame.SHOW_TYPE.KEEP)
          this.mGeneratedCharacter.SetPrimaryEquipmentsVisible(this.Primary == ToggleWeaponFrame.SHOW_TYPE.HIDDEN);
        if (this.Secondary == ToggleWeaponFrame.SHOW_TYPE.KEEP)
          return;
        this.mGeneratedCharacter.SetSecondaryEquipmentsVisible(this.Secondary == ToggleWeaponFrame.SHOW_TYPE.HIDDEN);
      }
      else
      {
        if (Object.op_Equality((Object) this.mUnitController, (Object) null))
          return;
        if (this.Primary != ToggleWeaponFrame.SHOW_TYPE.KEEP)
          this.mUnitController.SetPrimaryEquipmentsVisible(this.Primary == ToggleWeaponFrame.SHOW_TYPE.HIDDEN);
        if (this.Secondary == ToggleWeaponFrame.SHOW_TYPE.KEEP)
          return;
        this.mUnitController.SetSecondaryEquipmentsVisible(this.Secondary == ToggleWeaponFrame.SHOW_TYPE.HIDDEN);
      }
    }

    public enum SHOW_TYPE
    {
      KEEP,
      HIDDEN,
      VISIBLE,
    }
  }
}
