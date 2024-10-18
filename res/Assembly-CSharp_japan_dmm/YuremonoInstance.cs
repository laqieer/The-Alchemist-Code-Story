// Decompiled with JetBrains decompiler
// Type: YuremonoInstance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
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
  public YuremonoParam Param;
  private List<YuremonoInstance.SkirtTraceStatus> mSkirtTraceStatusList;
  public Transform SpineTransform;
  private Quaternion mSpineBaseRotation;
  private Dictionary<string, Quaternion> mSkirtDiffHistory = new Dictionary<string, Quaternion>();
  private List<YuremonoInstance.TargetState> mStates = new List<YuremonoInstance.TargetState>();
  public bool DisplayDebug;

  public void SkirtSetUp()
  {
    if (0 >= this.Param.SkirtTraceTargets.Length)
      return;
    Transform transform1 = ((Component) this).transform;
    this.mSpineBaseRotation = this.SpineTransform.localRotation;
    this.mSkirtTraceStatusList = new List<YuremonoInstance.SkirtTraceStatus>();
    for (int index1 = 0; index1 < this.Param.SkirtTraceTargets.Length; ++index1)
    {
      YuremonoParam.SkirtTraceParam skirtTraceTarget = this.Param.SkirtTraceTargets[index1];
      Transform childRecursively1 = GameUtility.findChildRecursively(transform1, skirtTraceTarget.Name);
      if (!Object.op_Equality((Object) childRecursively1, (Object) null) && childRecursively1.childCount > 0)
      {
        Transform child = childRecursively1.GetChild(0);
        Transform childRecursively2 = GameUtility.findChildRecursively(transform1, skirtTraceTarget.TargetName);
        Transform transform2 = (Transform) null;
        if (Object.op_Inequality((Object) childRecursively2, (Object) null))
          transform2 = childRecursively2.GetChild(0);
        YuremonoInstance.SkirtTraceStatus skirtTraceStatus = new YuremonoInstance.SkirtTraceStatus();
        skirtTraceStatus.SkirtRatio = skirtTraceTarget.SkirtRatio;
        skirtTraceStatus.Transform = childRecursively1;
        skirtTraceStatus.BaseOffset = childRecursively1.localPosition;
        skirtTraceStatus.offsetTailPos = Quaternion.op_Multiply(Quaternion.Inverse(childRecursively1.rotation), Vector3.op_Subtraction(child.position, childRecursively1.position));
        skirtTraceStatus.BaseRotaion = childRecursively1.localRotation;
        skirtTraceStatus.RotWaitAng = skirtTraceTarget.RotWaitAng;
        skirtTraceStatus.TargetTransform = childRecursively2;
        skirtTraceStatus.TargetTailTransform = transform2;
        if (Object.op_Inequality((Object) childRecursively2, (Object) null))
          skirtTraceStatus.BaseTargetWorldRotationDiff = Quaternion.op_Multiply(Quaternion.Inverse(childRecursively2.parent.rotation), childRecursively2.rotation);
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
        if (!this.mSkirtDiffHistory.ContainsKey(((Object) childRecursively1).name))
          this.mSkirtDiffHistory.Add(((Object) childRecursively1).name, Quaternion.identity);
        else
          this.mSkirtDiffHistory[((Object) childRecursively1).name] = Quaternion.identity;
      }
    }
  }

  private Quaternion GetSpineWorldRotation()
  {
    float num = 0.0f;
    Vector3 zero = Vector3.zero;
    Quaternion quaternion = Quaternion.op_Multiply(Quaternion.Inverse(this.mSpineBaseRotation), this.SpineTransform.localRotation);
    ((Quaternion) ref quaternion).ToAngleAxis(ref num, ref zero);
    Vector3 vector3 = Quaternion.op_Multiply(this.SpineTransform.rotation, zero);
    return Quaternion.AngleAxis(num, vector3);
  }

  private void SkirtLateUpdate()
  {
    if (this.mSkirtTraceStatusList == null)
      return;
    for (int index1 = 0; index1 < this.mSkirtTraceStatusList.Count; ++index1)
    {
      YuremonoInstance.SkirtTraceStatus skirtTraceStatus = this.mSkirtTraceStatusList[index1];
      Quaternion spineWorldRotation = this.GetSpineWorldRotation();
      Quaternion quaternion1 = Quaternion.identity;
      if (skirtTraceStatus.EikyoSkirts != null && skirtTraceStatus.EikyoSkirts.Length > 0)
      {
        for (int index2 = 0; index2 < skirtTraceStatus.EikyoSkirts.Length; ++index2)
        {
          Quaternion identity = Quaternion.identity;
          if (this.mSkirtDiffHistory.ContainsKey(skirtTraceStatus.EikyoSkirts[index2].Name))
          {
            Quaternion quaternion2 = this.mSkirtDiffHistory[skirtTraceStatus.EikyoSkirts[index2].Name];
            float num = 0.0f;
            Vector3 zero = Vector3.zero;
            ((Quaternion) ref quaternion2).ToAngleAxis(ref num, ref zero);
            num *= skirtTraceStatus.EikyoSkirts[index2].Ratio;
            Quaternion quaternion3 = Quaternion.AngleAxis(num, zero);
            quaternion1 = Quaternion.op_Multiply(quaternion1, quaternion3);
          }
        }
      }
      if (Object.op_Equality((Object) skirtTraceStatus.TargetTransform, (Object) null))
      {
        skirtTraceStatus.Transform.rotation = Quaternion.op_Multiply(Quaternion.op_Multiply(Quaternion.op_Multiply(quaternion1, spineWorldRotation), skirtTraceStatus.Transform.parent.rotation), skirtTraceStatus.BaseRotaion);
      }
      else
      {
        Quaternion quaternion4 = Quaternion.op_Multiply(skirtTraceStatus.TargetTransform.parent.rotation, skirtTraceStatus.BaseTargetWorldRotationDiff);
        Quaternion rotation = skirtTraceStatus.TargetTransform.rotation;
        Quaternion quaternion5 = Quaternion.op_Multiply(Quaternion.Inverse(quaternion4), rotation);
        float num = 0.0f;
        Vector3 zero = Vector3.zero;
        ((Quaternion) ref quaternion5).ToAngleAxis(ref num, ref zero);
        Vector3 vector3_1 = Quaternion.op_Multiply(skirtTraceStatus.TargetTransform.rotation, zero);
        Quaternion quaternion6 = Quaternion.AngleAxis(num, vector3_1);
        skirtTraceStatus.Transform.position = Vector3.op_Addition(skirtTraceStatus.Transform.parent.position, Quaternion.op_Multiply(Quaternion.op_Multiply(spineWorldRotation, skirtTraceStatus.Transform.parent.rotation), skirtTraceStatus.BaseOffset));
        Vector3 vector3_2 = Vector3.op_Multiply(Vector3.op_Subtraction(Vector3.op_Addition(skirtTraceStatus.Transform.position, Quaternion.op_Multiply(Quaternion.op_Multiply(Quaternion.op_Multiply(spineWorldRotation, skirtTraceStatus.Transform.parent.rotation), skirtTraceStatus.BaseRotaion), skirtTraceStatus.offsetTailPos)), skirtTraceStatus.Transform.position), skirtTraceStatus.SkirtRatio);
        Vector3 vector3_3 = Vector3.op_Subtraction(Vector3.op_Addition(skirtTraceStatus.Transform.position, vector3_2), skirtTraceStatus.TargetTransform.position);
        Vector3 vector3_4 = Vector3.op_Subtraction(skirtTraceStatus.TargetTailTransform.position, skirtTraceStatus.TargetTransform.position);
        if ((double) Mathf.Acos(Vector3.Dot(((Vector3) ref vector3_3).normalized, ((Vector3) ref vector3_4).normalized)) * 180.0 / 3.1415927410125732 < (double) skirtTraceStatus.RotWaitAng)
        {
          skirtTraceStatus.Transform.rotation = Quaternion.op_Multiply(Quaternion.op_Multiply(Quaternion.op_Multiply(Quaternion.op_Multiply(quaternion6, quaternion1), spineWorldRotation), skirtTraceStatus.Transform.parent.rotation), skirtTraceStatus.BaseRotaion);
          this.mSkirtDiffHistory[((Object) skirtTraceStatus.Transform).name] = quaternion6;
        }
        else
        {
          skirtTraceStatus.Transform.rotation = Quaternion.op_Multiply(Quaternion.op_Multiply(Quaternion.op_Multiply(quaternion1, spineWorldRotation), skirtTraceStatus.Transform.parent.rotation), skirtTraceStatus.BaseRotaion);
          this.mSkirtDiffHistory[((Object) skirtTraceStatus.Transform).name] = Quaternion.identity;
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
    if (Object.op_Equality((Object) this.Param, (Object) null))
      return;
    Transform transform = ((Component) this).transform;
    for (int index = this.Param.Targets.Length - 1; index >= 0; --index)
    {
      Transform childRecursively = GameUtility.findChildRecursively(transform, this.Param.Targets[index].TargetName);
      if (Object.op_Equality((Object) childRecursively, (Object) null))
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
    Vector3 vector3 = Vector3.op_Subtraction(pos, origin);
    if ((double) ((Vector3) ref vector3).magnitude >= (double) distance)
      vector3 = Vector3.op_Multiply(((Vector3) ref vector3).normalized, distance);
    return Vector3.op_Addition(vector3, origin);
  }

  private void LateUpdate()
  {
    this.SkirtLateUpdate();
    Vector3 lossyScale = ((Component) this).transform.lossyScale;
    float scale = (float) (((double) Mathf.Abs(lossyScale.x) + (double) Mathf.Abs(lossyScale.y) + (double) Mathf.Abs(lossyScale.z)) / 3.0);
    float deltaTime = Time.deltaTime;
    Vector3 vector3_1 = Vector3.op_Multiply(Physics.gravity, deltaTime);
    for (int index = this.mStates.Count - 1; index >= 0; --index)
    {
      YuremonoInstance.TargetState mState = this.mStates[index];
      if (mState.IsValid)
      {
        Vector3 vector3_2 = Vector3.op_Addition(mState.Transform.position, Vector3.op_Multiply(Vector3.op_Multiply(Vector3.down, mState.Param.Length), scale));
        Vector3 vector3_3 = mState.CalcScaledBaseTailPos(scale);
        mState.DesiredTailPos = Vector3.Lerp(vector3_3, vector3_2, mState.Param.Kinematic);
        YuremonoInstance.TargetState targetState1 = mState;
        targetState1.Velocity = Vector3.op_Multiply(targetState1.Velocity, (float) (1.0 - (double) mState.Param.Damping * (double) deltaTime));
        Vector3 vector3_4 = YuremonoInstance.CalcConstrainedPos(mState.TailPos, mState.Transform.position, mState.Param.Length * scale);
        Vector3 vector3_5 = Vector3.op_Subtraction(mState.DesiredTailPos, vector3_4);
        Vector3 vector3_6 = Vector3.op_Addition(mState.Velocity, Vector3.op_Multiply(Vector3.op_Multiply(vector3_5, deltaTime), mState.Param.Acceleration));
        mState.Velocity = vector3_6;
        YuremonoInstance.TargetState targetState2 = mState;
        targetState2.TailPos = Vector3.op_Addition(targetState2.TailPos, Vector3.op_Addition(Vector3.op_Multiply(mState.Velocity, deltaTime), Vector3.op_Multiply(vector3_1, mState.Param.Gravity)));
        mState.TailPos = YuremonoInstance.CalcConstrainedPos(mState.TailPos, mState.Transform.position, mState.Param.Length * scale);
        if ((double) mState.Param.AngularLimit > 0.0)
        {
          Vector3 vector3_7 = Vector3.op_Subtraction(mState.CalcScaledBaseTailPos(scale), mState.Transform.position);
          Vector3 vector3_8 = Vector3.op_Subtraction(mState.TailPos, mState.Transform.position);
          if ((double) Vector3.Angle(vector3_7, vector3_8) >= (double) mState.Param.AngularLimit)
          {
            Vector3 vector3_9 = Vector3.Cross(vector3_7, vector3_8);
            Vector3 normalized = ((Vector3) ref vector3_9).normalized;
            Vector3 vector3_10 = Vector3.Cross(normalized, vector3_7);
            float num = (double) Vector3.Dot(((Vector3) ref vector3_10).normalized, vector3_8) < 0.0 ? -mState.Param.AngularLimit : mState.Param.AngularLimit;
            mState.TailPos = Vector3.op_Addition(Vector3.op_Multiply(Quaternion.op_Multiply(Quaternion.AngleAxis(num, normalized), ((Vector3) ref vector3_7).normalized), ((Vector3) ref vector3_8).magnitude), mState.Transform.position);
            mState.Velocity = Vector3.op_Subtraction(mState.DesiredTailPos, mState.TailPos);
          }
        }
        Quaternion rotation = Quaternion.FromToRotation(Vector3.op_Subtraction(mState.CurrentTailPos, mState.Transform.position), Vector3.op_Subtraction(mState.TailPos, mState.Transform.position));
        mState.Transform.rotation = Quaternion.op_Multiply(rotation, mState.Transform.rotation);
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

    public Vector3 Origin => this.Transform.position;

    public Vector3 BaseTailPos
    {
      get
      {
        Matrix4x4 localToWorldMatrix = this.Transform.parent.localToWorldMatrix;
        return ((Matrix4x4) ref localToWorldMatrix).MultiplyPoint(Vector3.op_Addition(this.Transform.localPosition, Vector3.op_Multiply(Quaternion.op_Multiply(this.BaseRotation, this.Forward), this.Param.Length)));
      }
    }

    public Vector3 CalcScaledBaseTailPos(float scale)
    {
      Matrix4x4 localToWorldMatrix = this.Transform.parent.localToWorldMatrix;
      return ((Matrix4x4) ref localToWorldMatrix).MultiplyPoint(Vector3.op_Addition(this.Transform.localPosition, Vector3.op_Multiply(Vector3.op_Multiply(Quaternion.op_Multiply(this.BaseRotation, this.Forward), this.Param.Length), scale)));
    }

    public Vector3 CurrentTailPos
    {
      get
      {
        return Vector3.op_Addition(Vector3.op_Multiply(Quaternion.op_Multiply(this.Transform.rotation, this.Forward), this.Param.Length), this.Transform.position);
      }
    }

    public bool IsValid => Object.op_Inequality((Object) this.Transform, (Object) null);
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
