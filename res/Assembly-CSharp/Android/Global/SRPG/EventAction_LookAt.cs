// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_LookAt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("アクター/回転", "指定のオブジェクトを回転させます。", 5592405, 4473992)]
  public class EventAction_LookAt : EventAction
  {
    [HideInInspector]
    public float Time = 1f;
    [HideInInspector]
    public float Speed = 90f;
    [StringIsActorID]
    public string TargetID;
    public EventAction_LookAt.LookAtTypes LookAt;
    [StringIsActorID]
    [HideInInspector]
    public string LookAtID;
    [HideInInspector]
    public Vector3 LookAtPosition;
    public EventAction_LookAt.RotationModes RotationMode;
    public ObjectAnimator.CurveType Curve;
    public bool Rotate3D;
    private Quaternion mStartRotation;
    private Quaternion mEndRotation;
    private Transform mTarget;
    private Vector3 mLookAt;
    private float mTime;

    public override void OnActivate()
    {
      GameObject actor1 = EventAction.FindActor(this.TargetID);
      if (!((UnityEngine.Object) actor1 == (UnityEngine.Object) null))
      {
        this.mTarget = actor1.transform;
        if (this.LookAt == EventAction_LookAt.LookAtTypes.GameObject)
        {
          GameObject actor2 = EventAction.FindActor(this.LookAtID);
          if (!((UnityEngine.Object) actor2 == (UnityEngine.Object) null))
            this.LookAtPosition = actor2.transform.position;
          else
            goto label_11;
        }
        Vector3 forward = this.LookAtPosition - this.mTarget.transform.position;
        if (!this.Rotate3D)
          forward.y = 0.0f;
        this.mStartRotation = this.mTarget.rotation;
        this.mEndRotation = Quaternion.LookRotation(forward);
        if (this.RotationMode == EventAction_LookAt.RotationModes.Speed)
          this.Time = Quaternion.Angle(this.mStartRotation, this.mEndRotation) / this.Speed;
        if ((double) this.Time > 0.0)
          return;
        this.mTarget.rotation = this.mEndRotation;
      }
label_11:
      this.ActivateNext();
    }

    public override void Update()
    {
      this.mTime += UnityEngine.Time.deltaTime;
      this.mTarget.rotation = Quaternion.Slerp(this.mStartRotation, this.mEndRotation, this.Curve.Evaluate(Mathf.Clamp01(this.mTime / this.Time)));
      if ((double) this.mTime < (double) this.Time)
        return;
      this.ActivateNext();
    }

    public enum LookAtTypes
    {
      WorldPosition,
      GameObject,
    }

    public enum RotationModes
    {
      FixedTime,
      Speed,
    }
  }
}
