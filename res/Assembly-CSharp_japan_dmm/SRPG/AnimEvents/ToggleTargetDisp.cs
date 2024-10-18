// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleTargetDisp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG.AnimEvents
{
  public class ToggleTargetDisp : AnimEvent
  {
    private TacticsUnitController getTarget(TacticsUnitController tuc)
    {
      if (Object.op_Equality((Object) tuc, (Object) null))
        return (TacticsUnitController) null;
      List<TacticsUnitController> targetTucLists = tuc.GetTargetTucLists();
      return targetTucLists == null || targetTucLists.Count == 0 ? (TacticsUnitController) null : targetTucLists[0];
    }

    public override void OnStart(GameObject go)
    {
      TacticsUnitController componentInParent = go.GetComponentInParent<TacticsUnitController>();
      if (!Object.op_Implicit((Object) componentInParent))
        return;
      TacticsUnitController target = this.getTarget(componentInParent);
      if (!Object.op_Implicit((Object) target))
        return;
      target.SetVisible(false);
    }

    public override void OnEnd(GameObject go)
    {
      TacticsUnitController componentInParent = go.GetComponentInParent<TacticsUnitController>();
      if (!Object.op_Implicit((Object) componentInParent))
        return;
      TacticsUnitController target = this.getTarget(componentInParent);
      if (!Object.op_Implicit((Object) target))
        return;
      target.SetVisible(true);
      SceneBattle instance = SceneBattle.Instance;
      if (!Object.op_Implicit((Object) instance))
        return;
      instance.OnGimmickUpdate();
    }
  }
}
