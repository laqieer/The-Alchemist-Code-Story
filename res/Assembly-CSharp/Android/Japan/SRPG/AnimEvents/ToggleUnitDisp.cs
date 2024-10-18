// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleUnitDisp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class ToggleUnitDisp : AnimEvent
  {
    private UnitController mUnitController;
    private GeneratedCharacter mGeneratedCharacter;

    public override void OnStart(GameObject go)
    {
      base.OnStart(go);
      this.mGeneratedCharacter = go.GetComponent<GeneratedCharacter>();
      if ((UnityEngine.Object) this.mGeneratedCharacter != (UnityEngine.Object) null)
      {
        this.mGeneratedCharacter.SetVisible(false);
      }
      else
      {
        this.mUnitController = go.GetComponentInParent<UnitController>();
        if (!((UnityEngine.Object) this.mUnitController != (UnityEngine.Object) null))
          return;
        this.mUnitController.SetVisible(false);
      }
    }

    public override void OnEnd(GameObject go)
    {
      base.OnEnd(go);
      if ((UnityEngine.Object) this.mGeneratedCharacter != (UnityEngine.Object) null)
      {
        this.mGeneratedCharacter.SetVisible(true);
      }
      else
      {
        if (!((UnityEngine.Object) this.mUnitController != (UnityEngine.Object) null))
          return;
        this.mUnitController.SetVisible(true);
      }
    }
  }
}
