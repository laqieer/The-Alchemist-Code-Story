// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleUnitDisp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
      if (Object.op_Inequality((Object) this.mGeneratedCharacter, (Object) null))
      {
        this.mGeneratedCharacter.SetVisible(false);
      }
      else
      {
        this.mUnitController = go.GetComponentInParent<UnitController>();
        if (!Object.op_Inequality((Object) this.mUnitController, (Object) null))
          return;
        this.mUnitController.SetVisible(false);
      }
    }

    public override void OnEnd(GameObject go)
    {
      base.OnEnd(go);
      if (Object.op_Inequality((Object) this.mGeneratedCharacter, (Object) null))
      {
        this.mGeneratedCharacter.SetVisible(true);
      }
      else
      {
        if (!Object.op_Inequality((Object) this.mUnitController, (Object) null))
          return;
        this.mUnitController.SetVisible(true);
      }
    }
  }
}
