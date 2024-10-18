// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.BuffEffectEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
