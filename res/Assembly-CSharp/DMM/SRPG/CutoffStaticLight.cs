// Decompiled with JetBrains decompiler
// Type: SRPG.CutoffStaticLight
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class CutoffStaticLight : AnimEvent
  {
    public override void OnStart(GameObject go)
    {
      GeneratedCharacter component = go.GetComponent<GeneratedCharacter>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        component.EnableStaticLight(false);
        CameraHook.SetDisableFog(true);
      }
      else
      {
        TacticsUnitController componentInParent = go.GetComponentInParent<TacticsUnitController>();
        if (!Object.op_Inequality((Object) componentInParent, (Object) null))
          return;
        componentInParent.EnableStaticLight(false);
        CameraHook.SetDisableFog(true);
      }
    }

    public override void OnEnd(GameObject go)
    {
      GeneratedCharacter component = go.GetComponent<GeneratedCharacter>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        component.EnableStaticLight(true);
        CameraHook.SetDisableFog(false);
      }
      else
      {
        TacticsUnitController componentInParent = go.GetComponentInParent<TacticsUnitController>();
        if (!Object.op_Inequality((Object) componentInParent, (Object) null))
          return;
        componentInParent.EnableStaticLight(true);
        CameraHook.SetDisableFog(false);
      }
    }
  }
}
