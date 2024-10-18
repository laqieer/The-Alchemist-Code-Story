// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_RotateHead
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/頭を回転", "指定のオブジェクトの頭を回転させます。", 5592405, 4473992)]
  public class EventAction_RotateHead : EventAction
  {
    private static readonly string HeadBoneName = "Bip001 Head";
    private static readonly string NeckBoneName = "Bip001 Neck";
    private static readonly string Spine1BoneName = "Bip001 Spine1";
    private static readonly float NeckRotateInHead = 0.25f;
    private static readonly float Spine1RotateInHead = 0.25f;
    [HideInInspector]
    public float Time = 1f;
    [HideInInspector]
    public float Speed = 90f;
    [StringIsActorList]
    public string TargetID;
    [Range(-40f, 40f)]
    public float RotateY;
    public EventAction_RotateHead.RotationModes RotationMode;
    public ObjectAnimator.CurveType Curve;
    public bool Async;
    private Transform mTargetHead;
    private Transform mTargetNeck;
    private Transform mTargetSpine1;
    private Quaternion mStartRotationHead;
    private Quaternion mEndRotationHead;
    private Quaternion mStartRotationNeck;
    private Quaternion mEndRotationNeck;
    private Quaternion mStartRotationSpine1;
    private Quaternion mEndRotationSpine1;
    private float mTime;

    public override void OnActivate()
    {
      GameObject actor = EventAction.FindActor(this.TargetID);
      if ((UnityEngine.Object) actor != (UnityEngine.Object) null)
      {
        this.mTargetHead = GameUtility.findChildRecursively(actor.transform, EventAction_RotateHead.HeadBoneName);
        this.mTargetNeck = GameUtility.findChildRecursively(actor.transform, EventAction_RotateHead.NeckBoneName);
        this.mTargetSpine1 = GameUtility.findChildRecursively(actor.transform, EventAction_RotateHead.Spine1BoneName);
        if ((UnityEngine.Object) this.mTargetHead != (UnityEngine.Object) null)
          this.mStartRotationHead = this.mTargetHead.localRotation;
        if ((UnityEngine.Object) this.mTargetNeck != (UnityEngine.Object) null)
          this.mStartRotationNeck = this.mTargetNeck.localRotation;
        if ((UnityEngine.Object) this.mTargetSpine1 != (UnityEngine.Object) null)
          this.mStartRotationSpine1 = this.mTargetSpine1.localRotation;
        this.mEndRotationHead = Quaternion.Euler(-this.RotateY, 0.0f, 0.0f);
        this.mEndRotationNeck = Quaternion.Euler(-this.RotateY * EventAction_RotateHead.NeckRotateInHead, 0.0f, 0.0f);
        this.mEndRotationSpine1 = Quaternion.Euler(-this.RotateY * EventAction_RotateHead.Spine1RotateInHead, 0.0f, 0.0f);
      }
      if (this.RotationMode == EventAction_RotateHead.RotationModes.Speed)
        this.Time = Quaternion.Angle(this.mStartRotationHead, this.mEndRotationHead) / this.Speed;
      if (!this.Async)
        return;
      this.ActivateNext(true);
    }

    public override void Update()
    {
      this.mTime += UnityEngine.Time.deltaTime;
      float t = this.Curve.Evaluate(Mathf.Clamp01(this.mTime / this.Time));
      if ((UnityEngine.Object) this.mTargetHead != (UnityEngine.Object) null)
        this.mTargetHead.localRotation = Quaternion.Slerp(this.mStartRotationHead, this.mEndRotationHead, t);
      if ((UnityEngine.Object) this.mTargetNeck != (UnityEngine.Object) null)
        this.mTargetNeck.localRotation = Quaternion.Slerp(this.mStartRotationNeck, this.mEndRotationNeck, t);
      if ((UnityEngine.Object) this.mTargetSpine1 != (UnityEngine.Object) null)
        this.mTargetSpine1.localRotation = Quaternion.Slerp(this.mStartRotationSpine1, this.mEndRotationSpine1, t);
      if ((double) this.mTime < (double) this.Time)
        return;
      if (this.Async)
        this.enabled = false;
      else
        this.ActivateNext();
    }

    public enum RotationModes
    {
      FixedTime,
      Speed,
    }
  }
}
