// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.BuffEffectEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG.AnimEvents
{
  public class BuffEffectEvent : AnimEvent
  {
    public bool IsDispTarget = true;
    public bool IsDispSelf;

    public override void OnStart(GameObject go)
    {
      TacticsUnitController componentInParent = go.GetComponentInParent<TacticsUnitController>();
      if (!Object.op_Implicit((Object) componentInParent))
        return;
      if (this.IsDispTarget)
        componentInParent.BuffEffectTarget();
      if (!this.IsDispSelf)
        return;
      componentInParent.BuffEffectSelf();
    }
  }
}
