// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleTeleport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
      if (!(bool) ((UnityEngine.Object) componentInParent))
        return;
      SceneBattle instance = SceneBattle.Instance;
      if (!(bool) ((UnityEngine.Object) instance))
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
      if (!(bool) ((UnityEngine.Object) componentInParent))
        return;
      componentInParent.transform.position = Vector3.Lerp(this.mPosStart, this.mPosEnd, ratio);
    }

    public override void OnEnd(GameObject go)
    {
      if (!this.mIsValid)
        return;
      TacticsUnitController componentInParent = go.GetComponentInParent<TacticsUnitController>();
      if (!(bool) ((UnityEngine.Object) componentInParent))
        return;
      componentInParent.CollideGround = true;
      componentInParent.transform.position = this.mPosEnd;
      componentInParent.SetStartPos(componentInParent.transform.position);
      componentInParent.LookAtTarget();
      SceneBattle instance = SceneBattle.Instance;
      if ((bool) ((UnityEngine.Object) instance))
        instance.OnGimmickUpdate();
      this.mIsValid = false;
    }
  }
}
