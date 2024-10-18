// Decompiled with JetBrains decompiler
// Type: YuremonoInstance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class YuremonoInstance : MonoBehaviour
{
  private static readonly Vector3[] mAxisToVector = new Vector3[6]
  {
    new Vector3(-1f, 0.0f, 0.0f),
    new Vector3(1f, 0.0f, 0.0f),
    new Vector3(0.0f, -1f, 0.0f),
    new Vector3(0.0f, 1f, 0.0f),
    new Vector3(0.0f, 0.0f, -1f),
    new Vector3(0.0f, 0.0f, 1f)
  };
  private Dictionary<string, Quaternion> mSkirtDiffHistory = new Dictionary<string, Quaternion>();
  private List<YuremonoInstance.TargetState> mStates = new List<YuremonoInstance.TargetState>();
  public YuremonoParam Param;
  private List<YuremonoInstance.SkirtTraceStatus> mSkirtTraceStatusList;
  public Transform SpineTransform;
  private Quaternion mSpineBaseRotation;
  public bool DisplayDebug;

  public void SkirtSetUp()
  {
    if (0 >= this.Param.SkirtTraceTargets.Length)
      return;
    Transform transform1 = this.transform;
    this.mSpineBaseRotation = this.SpineTransform.localRotation;
    this.mSkirtTraceStatusList = new List<YuremonoInstance.SkirtTraceStatus>();
    for (int index1 = 0; index1 < this.Param.SkirtTraceTargets.Length; ++index1)
    {
      YuremonoParam.SkirtTraceParam skirtTraceTarget = this.Param.SkirtTraceTargets[index1];
      Transform childRecursively1 = GameUtility.findChildRecursively(transform1, skirtTraceTarget.Name);
      if (!((Object) childRecursively1 == (Object) null) && childRecursively1.childCount > 0)
      {
        Transform child = childRecursively1.GetChild(0);
        Transform childRecursively2 = GameUtility.findChildRecursively(transform1, skirtTraceTarget.TargetName);
        Transform transform2 = (Transform) null;
        if ((Object) childRecursively2 != (Object) null)
          transform2 = childRecursively2.GetChild(0);
        YuremonoInstance.SkirtTraceStatus skirtTraceStatus = new YuremonoInstance.SkirtTraceStatus();
        skirtTraceStatus.SkirtRatio = skirtTraceTarget.SkirtRatio;
        skirtTraceStatus.Transform = childRecursively1;
        skirtTraceStatus.BaseOffset = childRecursively1.localPosition;
        skirtTraceStatus.offsetTailPos = Quaternion.Inverse(childRecursively1.rotation) * (child.position - childRecursively1.position);
        skirtTraceStatus.BaseRotaion = childRecursively1.localRotation;
        skirtTraceStatus.RotWaitAng = skirtTraceTarget.RotWaitAng;
        skirtTraceStatus.TargetTransform = childRecursively2;
        skirtTraceStatus.TargetTailTransform = transform2;
        if ((Object) childRecursively2 != (Object) null)
          skirtTraceStatus.BaseTargetWorldRotationDiff = Quaternion.Inverse(childRecursively2.parent.rotation) * childRecursively2.rotation;
        if (skirtTraceTarget.EikyoSkirts.Length > 0)
        {
          skirtTraceStatus.EikyoSkirts = new YuremonoParam.SkirtTraceParam.EikyoSkirt[skirtTraceTarget.EikyoSkirts.Length];
          for (int index2 = 0; index2 < skirtTraceTarget.EikyoSkirts.Length; ++index2)
          {
            skirtTraceStatus.EikyoSkirts[index2] = new YuremonoParam.SkirtTraceParam.EikyoSkirt();
            skirtTraceStatus.EikyoSkirts[index2].Name = skirtTraceTarget.EikyoSkirts[index2].Name;
            skirtTraceStatus.EikyoSkirts[index2].Ratio = skirtTraceTarget.EikyoSkirts[index2].Ratio;
          }
        }
        this.mSkirtTraceStatusList.Add(skirtTraceStatus);
        if (!this.mSkirtDiffHistory.ContainsKey(childRecursively1.name))
          this.mSkirtDiffHistory.Add(childRecursively1.name, Quaternion.identity);
        else
          this.mSkirtDiffHistory[childRecursively1.name] = Quaternion.identity;
      }
    }
  }

  private Quaternion GetSpineWorldRotation()
  {
    float angle = 0.0f;
    Vector3 axis1 = Vector3.zero;
    (Quaternion.Inverse(this.mSpineBaseRotation) * this.SpineTransform.localRotation).ToAngleAxis(out angle, out axis1);
    Vector3 axis2 = this.SpineTransform.rotation * axis1;
    return Quaternion.AngleAxis(angle, axis2);
  }

  private void SkirtLateUpdate()
  {
    if (this.mSkirtTraceStatusList == null)
      return;
    for (int index1 = 0; index1 < this.mSkirtTraceStatusList.Count; ++index1)
    {
      YuremonoInstance.SkirtTraceStatus skirtTraceStatus = this.mSkirtTraceStatusList[index1];
      Quaternion spineWorldRotation = this.GetSpineWorldRotation();
      Quaternion identity1 = Quaternion.identity;
      if (skirtTraceStatus.EikyoSkirts != null && skirtTraceStatus.EikyoSkirts.Length > 0)
      {
        for (int index2 = 0; index2 < skirtTraceStatus.EikyoSkirts.Length; ++index2)
        {
          Quaternion identity2 = Quaternion.identity;
          if (this.mSkirtDiffHistory.ContainsKey(skirtTraceStatus.EikyoSkirts[index2].Name))
          {
            Quaternion quaternion1 = this.mSkirtDiffHistory[skirtTraceStatus.EikyoSkirts[index2].Name];
            float angle = 0.0f;
            Vector3 axis = Vector3.zero;
            quaternion1.ToAngleAxis(out angle, out axis);
            angle *= skirtTraceStatus.EikyoSkirts[index2].Ratio;
            Quaternion quaternion2 = Quaternion.AngleAxis(angle, axis);
            identity1 *= quaternion2;
          }
        }
      }
      if ((Object) skirtTraceStatus.TargetTransform == (Object) null)
      {
        skirtTraceStatus.Transform.rotation = identity1 * spineWorldRotation * skirtTraceStatus.Transform.parent.rotation * skirtTraceStatus.BaseRotaion;
      }
      else
      {
        Quaternion quaternion1 = Quaternion.Inverse(skirtTraceStatus.TargetTransform.parent.rotation * skirtTraceStatus.BaseTargetWorldRotationDiff) * skirtTraceStatus.TargetTransform.rotation;
        float angle = 0.0f;
        Vector3 axis1 = Vector3.zero;
        quaternion1.ToAngleAxis(out angle, out axis1);
        Vector3 axis2 = skirtTraceStatus.TargetTransform.rotation * axis1;
        Quaternion quaternion2 = Quaternion.AngleAxis(angle, axis2);
        skirtTraceStatus.Transform.position = skirtTraceStatus.Transform.parent.position + spineWorldRotation * skirtTraceStatus.Transform.parent.rotation * skirtTraceStatus.BaseOffset;
        Vector3 vector3 = (skirtTraceStatus.Transform.position + spineWorldRotation * skirtTraceStatus.Transform.parent.rotation * skirtTraceStatus.BaseRotaion * skirtTraceStatus.offsetTailPos - skirtTraceStatus.Transform.position) * skirtTraceStatus.SkirtRatio;
        if ((double) Mathf.Acos(Vector3.Dot((skirtTraceStatus.Transform.position + vector3 - skirtTraceStatus.TargetTransform.position).normalized, (skirtTraceStatus.TargetTailTransform.position - skirtTraceStatus.TargetTransform.position).normalized)) * 180.0 / 3.14159274101257 < (double) skirtTraceStatus.RotWaitAng)
        {
          skirtTraceStatus.Transform.rotation = quaternion2 * identity1 * spineWorldRotation * skirtTraceStatus.Transform.parent.rotation * skirtTraceStatus.BaseRotaion;
          this.mSkirtDiffHistory[skirtTraceStatus.Transform.name] = quaternion2;
        }
        else
        {
          skirtTraceStatus.Transform.rotation = identity1 * spineWorldRotation * skirtTraceStatus.Transform.parent.rotation * skirtTraceStatus.BaseRotaion;
          this.mSkirtDiffHistory[skirtTraceStatus.Transform.name] = Quaternion.identity;
        }
      }
    }
  }

  private void Start()
  {
    this.Setup();
    this.SkirtSetUp();
  }

  private void Reset()
  {
    for (int index = this.mStates.Count - 1; index >= 0; --index)
      this.mStates[index].Transform.localRotation = this.mStates[index].BaseRotation;
  }

  public void Setup()
  {
    this.Reset();
    this.mStates.Clear();
    if ((Object) this.Param == (Object) null)
      return;
    Transform transform = this.transform;
    for (int index = this.Param.Targets.Length - 1; index >= 0; --index)
    {
      Transform childRecursively = GameUtility.findChildRecursively(transform, this.Param.Targets[index].TargetName);
      if ((Object) childRecursively == (Object) null)
      {
        DebugUtility.LogError("Target '" + this.Param.Targets[index].TargetName + "' not found.");
      }
      else
      {
        YuremonoInstance.TargetState targetState = new YuremonoInstance.TargetState()
        {
          Transform = childRecursively,
          Param = this.Param.Targets[index]
        };
        targetState.BaseRotation = targetState.Transform.localRotation;
        targetState.Forward = YuremonoInstance.mAxisToVector[(int) targetState.Param.ForwardAxis];
        targetState.TailPos = targetState.CurrentTailPos;
        targetState.DesiredTailPos = targetState.TailPos;
        this.mStates.Add(targetState);
      }
    }
    for (int index1 = 0; index1 < this.mStates.Count; ++index1)
    {
      for (int index2 = 0; index2 < this.mStates.Count; ++index2)
      {
        if (index1 != index2 && this.mStates[index1].Transform.IsChildOf(this.mStates[index2].Transform))
        {
          YuremonoInstance.TargetState mState = this.mStates[index2];
          this.mStates[index2] = this.mStates[index1];
          this.mStates[index1] = mState;
        }
      }
    }
  }

  private void Update()
  {
    for (int index = this.mStates.Count - 1; index >= 0; --index)
    {
      YuremonoInstance.TargetState mState = this.mStates[index];
      if (mState.IsValid)
        mState.Transform.localRotation = mState.BaseRotation;
    }
  }

  private static Vector3 CalcConstrainedPos(Vector3 pos, Vector3 origin, float distance)
  {
    Vector3 vector3 = pos - origin;
    if ((double) vector3.magnitude >= (double) distance)
      vector3 = vector3.normalized * distance;
    return vector3 + origin;
  }

  private void LateUpdate()
  {
    this.SkirtLateUpdate();
    Vector3 lossyScale = this.transform.lossyScale;
    float scale = (float) (((double) Mathf.Abs(lossyScale.x) + (double) Mathf.Abs(lossyScale.y) + (double) Mathf.Abs(lossyScale.z)) / 3.0);
    float deltaTime = Time.deltaTime;
    Vector3 vector3_1 = Physics.gravity * deltaTime;
    for (int index = this.mStates.Count - 1; index >= 0; --index)
    {
      YuremonoInstance.TargetState mState = this.mStates[index];
      if (mState.IsValid)
      {
        Vector3 b = mState.Transform.position + Vector3.down * mState.Param.Length * scale;
        Vector3 a = mState.CalcScaledBaseTailPos(scale);
        mState.DesiredTailPos = Vector3.Lerp(a, b, mState.Param.Kinematic);
        mState.Velocity *= (float) (1.0 - (double) mState.Param.Damping * (double) deltaTime);
        Vector3 vector3_2 = YuremonoInstance.CalcConstrainedPos(mState.TailPos, mState.Transform.position, mState.Param.Length * scale);
        Vector3 vector3_3 = mState.DesiredTailPos - vector3_2;
        Vector3 vector3_4 = mState.Velocity + vector3_3 * deltaTime * mState.Param.Acceleration;
        mState.Velocity = vector3_4;
        mState.TailPos += mState.Velocity * deltaTime + vector3_1 * mState.Param.Gravity;
        mState.TailPos = YuremonoInstance.CalcConstrainedPos(mState.TailPos, mState.Transform.position, mState.Param.Length * scale);
        if ((double) mState.Param.AngularLimit > 0.0)
        {
          Vector3 vector3_5 = mState.CalcScaledBaseTailPos(scale) - mState.Transform.position;
          Vector3 vector3_6 = mState.TailPos - mState.Transform.position;
          if ((double) Vector3.Angle(vector3_5, vector3_6) >= (double) mState.Param.AngularLimit)
          {
            Vector3 normalized = Vector3.Cross(vector3_5, vector3_6).normalized;
            float angle = (double) Vector3.Dot(Vector3.Cross(normalized, vector3_5).normalized, vector3_6) < 0.0 ? -mState.Param.AngularLimit : mState.Param.AngularLimit;
            mState.TailPos = Quaternion.AngleAxis(angle, normalized) * vector3_5.normalized * vector3_6.magnitude + mState.Transform.position;
            mState.Velocity = mState.DesiredTailPos - mState.TailPos;
          }
        }
        Quaternion rotation = Quaternion.FromToRotation(mState.CurrentTailPos - mState.Transform.position, mState.TailPos - mState.Transform.position);
        mState.Transform.rotation = rotation * mState.Transform.rotation;
      }
    }
  }

  public class TargetState
  {
    public YuremonoParam.TargetParam Param;
    public Transform Transform;
    public Quaternion BaseRotation;
    public Vector3 TailPos;
    public Vector3 DesiredTailPos;
    public Vector3 Forward;
    public Vector3 Velocity;

    public Vector3 Origin
    {
      get
      {
        return this.Transform.position;
      }
    }

    public Vector3 BaseTailPos
    {
      get
      {
        return this.Transform.parent.localToWorldMatrix.MultiplyPoint(this.Transform.localPosition + this.BaseRotation * this.Forward * this.Param.Length);
      }
    }

    public Vector3 CalcScaledBaseTailPos(float scale)
    {
      return this.Transform.parent.localToWorldMatrix.MultiplyPoint(this.Transform.localPosition + this.BaseRotation * this.Forward * this.Param.Length * scale);
    }

    public Vector3 CurrentTailPos
    {
      get
      {
        return this.Transform.rotation * this.Forward * this.Param.Length + this.Transform.position;
      }
    }

    public bool IsValid
    {
      get
      {
        return (Object) this.Transform != (Object) null;
      }
    }
  }

  public class SkirtTraceStatus
  {
    public Transform Transform;
    public Vector3 BaseOffset;
    public Vector3 offsetTailPos;
    public Quaternion BaseRotaion;
    public float SkirtRatio;
    public float RotWait;
    public float RotWaitAng;
    public Transform TargetTransform;
    public Transform TargetTailTransform;
    public Quaternion BaseTargetWorldRotationDiff;
    public YuremonoParam.SkirtTraceParam.EikyoSkirt[] EikyoSkirts;
  }
}
