﻿// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleTargetDisp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG.AnimEvents
{
  public class ToggleTargetDisp : AnimEvent
  {
    private TacticsUnitController getTarget(TacticsUnitController tuc)
    {
      if ((UnityEngine.Object) tuc == (UnityEngine.Object) null)
        return (TacticsUnitController) null;
      List<TacticsUnitController> targetTucLists = tuc.GetTargetTucLists();
      if (targetTucLists == null || targetTucLists.Count == 0)
        return (TacticsUnitController) null;
      return targetTucLists[0];
    }

    public override void OnStart(GameObject go)
    {
      TacticsUnitController componentInParent = go.GetComponentInParent<TacticsUnitController>();
      if (!(bool) ((UnityEngine.Object) componentInParent))
        return;
      TacticsUnitController target = this.getTarget(componentInParent);
      if (!(bool) ((UnityEngine.Object) target))
        return;
      target.SetVisible(false);
    }

    public override void OnEnd(GameObject go)
    {
      TacticsUnitController componentInParent = go.GetComponentInParent<TacticsUnitController>();
      if (!(bool) ((UnityEngine.Object) componentInParent))
        return;
      TacticsUnitController target = this.getTarget(componentInParent);
      if (!(bool) ((UnityEngine.Object) target))
        return;
      target.SetVisible(true);
      SceneBattle instance = SceneBattle.Instance;
      if (!(bool) ((UnityEngine.Object) instance))
        return;
      instance.OnGimmickUpdate();
    }
  }
}
