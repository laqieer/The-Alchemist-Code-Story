// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.BuffEffectEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class BuffEffectEvent : AnimEvent
  {
    public bool IsDispTarget = true;
    public bool IsDispSelf;

    public override void OnStart(GameObject go)
    {
      TacticsUnitController componentInParent = go.GetComponentInParent<TacticsUnitController>();
      if (!(bool) ((UnityEngine.Object) componentInParent))
        return;
      if (this.IsDispTarget)
        componentInParent.BuffEffectTarget();
      if (!this.IsDispSelf)
        return;
      componentInParent.BuffEffectSelf();
    }
  }
}
