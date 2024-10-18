// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleUnitDisp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class ToggleUnitDisp : AnimEvent
  {
    public override void OnStart(GameObject go)
    {
      UnitController componentInParent = go.GetComponentInParent<UnitController>();
      if (!(bool) ((UnityEngine.Object) componentInParent))
        return;
      componentInParent.SetVisible(false);
    }

    public override void OnEnd(GameObject go)
    {
      UnitController componentInParent = go.GetComponentInParent<UnitController>();
      if (!(bool) ((UnityEngine.Object) componentInParent))
        return;
      componentInParent.SetVisible(true);
    }
  }
}
