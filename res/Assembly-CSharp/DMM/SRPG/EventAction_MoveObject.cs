﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_MoveObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("オブジェクト/移動", "シーン上のオブジェクトを移動させます。", 4478293, 4491400)]
  public class EventAction_MoveObject : EventAction
  {
    [StringIsActorID]
    public string TargetID;
    public ObjectAnimator.CurveType Curve;
    public Vector3 Position;
    public Vector3 Rotation;
    public float Time = 1f;
    public bool Async;
    private ObjectAnimator mAnimator;

    public override void OnActivate()
    {
      GameObject actor = EventAction.FindActor(this.TargetID);
      if (Object.op_Equality((Object) actor, (Object) null))
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
      if (Object.op_Inequality((Object) this.mAnimator, (Object) null) && this.mAnimator.isMoving)
        return;
      this.ActivateNext();
    }

    public override void GoToEndState()
    {
      GameObject actor = EventAction.FindActor(this.TargetID);
      if (Object.op_Equality((Object) actor, (Object) null))
        return;
      Quaternion rotation = Quaternion.Euler(this.Rotation);
      this.mAnimator = ObjectAnimator.Get(actor);
      this.mAnimator.AnimateTo(this.Position, rotation, -1f, this.Curve);
    }
  }
}
