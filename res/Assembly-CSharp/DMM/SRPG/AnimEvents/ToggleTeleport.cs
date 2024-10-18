// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleTeleport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG.AnimEvents
{
  public class ToggleTeleport : AnimEvent
  {
    private bool mIsValid;
    private Vector3 mPosStart;
    private Vector3 mPosEnd;

    public override void OnStart(GameObject go)
    {
      TacticsUnitController componentInParent = go.GetComponentInParent<TacticsUnitController>();
      if (!Object.op_Implicit((Object) componentInParent))
        return;
      SceneBattle instance = SceneBattle.Instance;
      if (!Object.op_Implicit((Object) instance))
        return;
      this.mPosStart = componentInParent.CenterPosition;
      this.mPosEnd = componentInParent.GetTargetPos();
      instance.OnGimmickUpdate();
      componentInParent.CollideGround = false;
      this.mIsValid = true;
    }

    public override void OnTick(GameObject go, float ratio)
    {
      if (!this.mIsValid)
        return;
      TacticsUnitController componentInParent = go.GetComponentInParent<TacticsUnitController>();
      if (!Object.op_Implicit((Object) componentInParent))
        return;
      ((Component) componentInParent).transform.position = Vector3.Lerp(this.mPosStart, this.mPosEnd, ratio);
    }

    public override void OnEnd(GameObject go)
    {
      if (!this.mIsValid)
        return;
      TacticsUnitController componentInParent = go.GetComponentInParent<TacticsUnitController>();
      if (!Object.op_Implicit((Object) componentInParent))
        return;
      componentInParent.CollideGround = true;
      ((Component) componentInParent).transform.position = this.mPosEnd;
      componentInParent.SetStartPos(((Component) componentInParent).transform.position);
      componentInParent.LookAtTarget();
      SceneBattle instance = SceneBattle.Instance;
      if (Object.op_Implicit((Object) instance))
        instance.OnGimmickUpdate();
      this.mIsValid = false;
    }
  }
}
