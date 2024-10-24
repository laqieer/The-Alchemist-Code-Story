﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_MoveObject2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/オブジェクト/移動", "シーン上のオブジェクトを移動させます。", 4478293, 4491400)]
  public class EventAction_MoveObject2 : EventAction
  {
    public float Time = 1f;
    [StringIsObjectList]
    public string TargetID;
    public ObjectAnimator.CurveType Curve;
    public Vector3 Position;
    public Vector3 Rotation;
    public bool Async;
    private ObjectAnimator mAnimator;

    public override void OnActivate()
    {
      GameObject actor = EventAction.FindActor(this.TargetID);
      if ((UnityEngine.Object) actor == (UnityEngine.Object) null)
      {
        this.ActivateNext();
      }
      else
      {
        Quaternion rotation = Quaternion.Euler(this.Rotation);
        this.mAnimator = ObjectAnimator.Get(actor);
        this.mAnimator.AnimateTo(this.Position, rotation, this.Time, this.Curve);
        if (!this.Async && (double) this.Time > 0.0)
          return;
        this.ActivateNext();
      }
    }

    public override void Update()
    {
      if ((UnityEngine.Object) this.mAnimator != (UnityEngine.Object) null && this.mAnimator.isMoving)
        return;
      this.ActivateNext();
    }
  }
}
