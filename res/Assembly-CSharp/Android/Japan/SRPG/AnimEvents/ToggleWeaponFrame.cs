// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleWeaponFrame
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
      if ((UnityEngine.Object) this.mGeneratedCharacter != (UnityEngine.Object) null)
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
        if ((UnityEngine.Object) this.mUnitController == (UnityEngine.Object) null)
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
      if ((UnityEngine.Object) this.mGeneratedCharacter != (UnityEngine.Object) null)
      {
        if (this.Primary != ToggleWeaponFrame.SHOW_TYPE.KEEP)
          this.mGeneratedCharacter.SetPrimaryEquipmentsVisible(this.Primary == ToggleWeaponFrame.SHOW_TYPE.HIDDEN);
        if (this.Secondary == ToggleWeaponFrame.SHOW_TYPE.KEEP)
          return;
        this.mGeneratedCharacter.SetSecondaryEquipmentsVisible(this.Secondary == ToggleWeaponFrame.SHOW_TYPE.HIDDEN);
      }
      else
      {
        if ((UnityEngine.Object) this.mUnitController == (UnityEngine.Object) null)
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
