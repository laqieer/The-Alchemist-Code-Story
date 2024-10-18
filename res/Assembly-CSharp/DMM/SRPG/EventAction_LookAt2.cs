// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_LookAt2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/アクター/回転", "指定のオブジェクトを回転させます。", 5592405, 4473992)]
  public class EventAction_LookAt2 : EventAction
  {
    [StringIsActorList]
    public string TargetID;
    public EventAction_LookAt2.LookAtTypes LookAt;
    [StringIsActorID]
    [HideInInspector]
    public string LookAtID;
    [HideInInspector]
    public Vector3 LookAtPosition;
    public EventAction_LookAt2.RotationModes RotationMode;
    public ObjectAnimator.CurveType Curve;
    [HideInInspector]
    public float Time = 1f;
    [HideInInspector]
    public float Speed = 90f;
    public bool Async;
    public bool Rotate3D;
    private Quaternion mStartRotation;
    private Quaternion mEndRotation;
    private Transform mTarget;
    private Vector3 mLookAt;
    public bool m_MoveSnap = true;
    public bool Reverse = true;
    [Range(0.0f, 359f)]
    [HideInInspector]
    public float RotateX;
    [Range(0.0f, 359f)]
    [HideInInspector]
    public float RotateY;
    [Range(0.0f, 359f)]
    [HideInInspector]
    public float RotateZ;
    private Vector3 mEulerStartRotate;
    private Vector3 mEulerEndRotate;
    private Vector3 mAddEulerAngle;
    private float mTime;

    public override void OnActivate()
    {
      GameObject actor1 = EventAction.FindActor(this.TargetID);
      if (!Object.op_Equality((Object) actor1, (Object) null))
      {
        this.mTarget = actor1.transform;
        if (this.LookAt == EventAction_LookAt2.LookAtTypes.GameObject)
        {
          GameObject actor2 = EventAction.FindActor(this.LookAtID);
          if (!Object.op_Equality((Object) actor2, (Object) null))
            this.LookAtPosition = actor2.transform.position;
          else
            goto label_18;
        }
        Vector3 vector3 = Vector3.op_Subtraction(this.LookAtPosition, ((Component) this.mTarget).transform.position);
        if (!this.Rotate3D)
          vector3.y = 0.0f;
        this.mStartRotation = this.mTarget.rotation;
        this.mEndRotation = Quaternion.LookRotation(vector3);
        if (this.LookAt == EventAction_LookAt2.LookAtTypes.WorldRotate)
          this.mEndRotation = Quaternion.Euler(this.RotateX, this.RotateY, this.RotateZ);
        this.mEulerStartRotate = ((Quaternion) ref this.mStartRotation).eulerAngles;
        this.mEulerEndRotate = ((Quaternion) ref this.mEndRotation).eulerAngles;
        if (!this.Reverse)
        {
          this.mAddEulerAngle = Vector3.op_Subtraction(this.mEulerEndRotate, this.mEulerStartRotate);
          this.mAddEulerAngle.x = (double) Mathf.Abs(this.mAddEulerAngle.x) > 180.0 || (double) this.mAddEulerAngle.x == 0.0 ? this.mAddEulerAngle.x : (float) (-(double) Mathf.Sign(this.mAddEulerAngle.x) * (360.0 - (double) Mathf.Abs(this.mAddEulerAngle.x)));
          this.mAddEulerAngle.y = (double) Mathf.Abs(this.mAddEulerAngle.y) > 180.0 || (double) this.mAddEulerAngle.y == 0.0 ? this.mAddEulerAngle.y : (float) (-(double) Mathf.Sign(this.mAddEulerAngle.y) * (360.0 - (double) Mathf.Abs(this.mAddEulerAngle.y)));
          this.mAddEulerAngle.z = (double) Mathf.Abs(this.mAddEulerAngle.z) > 180.0 || (double) this.mAddEulerAngle.z == 0.0 ? this.mAddEulerAngle.z : (float) (-(double) Mathf.Sign(this.mAddEulerAngle.z) * (360.0 - (double) Mathf.Abs(this.mAddEulerAngle.z)));
        }
        else
        {
          this.mAddEulerAngle = Vector3.op_Subtraction(this.mEulerEndRotate, this.mEulerStartRotate);
          this.mAddEulerAngle.x = (double) Mathf.Abs(this.mAddEulerAngle.x) > 180.0 ? (float) (-(double) Mathf.Sign(this.mAddEulerAngle.x) * (360.0 - (double) Mathf.Abs(this.mAddEulerAngle.x))) : this.mAddEulerAngle.x;
          this.mAddEulerAngle.y = (double) Mathf.Abs(this.mAddEulerAngle.y) > 180.0 ? (float) (-(double) Mathf.Sign(this.mAddEulerAngle.y) * (360.0 - (double) Mathf.Abs(this.mAddEulerAngle.y))) : this.mAddEulerAngle.y;
          this.mAddEulerAngle.z = (double) Mathf.Abs(this.mAddEulerAngle.z) > 180.0 ? (float) (-(double) Mathf.Sign(this.mAddEulerAngle.z) * (360.0 - (double) Mathf.Abs(this.mAddEulerAngle.z))) : this.mAddEulerAngle.z;
        }
        if (this.RotationMode == EventAction_LookAt2.RotationModes.Speed)
          this.Time = Quaternion.Angle(this.mStartRotation, this.mEndRotation) / this.Speed;
        if ((double) this.Time <= 0.0)
        {
          this.mTarget.rotation = this.mEndRotation;
        }
        else
        {
          if (!this.Async)
            return;
          this.ActivateNext(true);
          return;
        }
      }
label_18:
      this.ActivateNext();
    }

    public override void Update()
    {
      this.mTime += UnityEngine.Time.deltaTime;
      this.mTarget.rotation = Quaternion.Euler(Vector3.op_Addition(this.mEulerStartRotate, Vector3.op_Multiply(this.mAddEulerAngle, this.Curve.Evaluate(Mathf.Clamp01(this.mTime / this.Time)))));
      if ((double) this.mTime < (double) this.Time)
        return;
      this.ActivateNext();
    }

    public enum LookAtTypes
    {
      WorldPosition,
      GameObject,
      WorldRotate,
    }

    public enum RotationModes
    {
      FixedTime,
      Speed,
    }
  }
}
