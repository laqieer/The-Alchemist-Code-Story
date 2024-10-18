// Decompiled with JetBrains decompiler
// Type: YuremonoParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class YuremonoParam : ScriptableObject
{
  public YuremonoParam.TargetParam[] Targets = new YuremonoParam.TargetParam[0];
  public YuremonoParam.SkirtTraceParam[] SkirtTraceTargets = new YuremonoParam.SkirtTraceParam[0];

  public enum Axes
  {
    XNegative,
    XPositive,
    YNegative,
    YPositive,
    ZNegative,
    ZPositive,
  }

  [Serializable]
  public class TargetParam
  {
    [Range(0.0f, 1f)]
    public float Kinematic = 1f;
    [Range(0.0f, 1f)]
    public float Gravity = 1f;
    public float Length = 1f;
    public float Damping = 0.1f;
    public float Acceleration = 10f;
    public string TargetName;
    public YuremonoParam.Axes ForwardAxis;
    [Range(0.0f, 90f)]
    public float AngularLimit;
  }

  [Serializable]
  public class SkirtTraceParam
  {
    public YuremonoParam.SkirtTraceParam.EikyoSkirt[] EikyoSkirts = new YuremonoParam.SkirtTraceParam.EikyoSkirt[0];
    public string Name;
    [Range(0.0f, 1f)]
    public float SkirtRatio;
    [Range(0.0f, 360f)]
    public float RotWaitAng;
    public string TargetName;

    [Serializable]
    public class EikyoSkirt
    {
      public string Name;
      [Range(0.0f, 1f)]
      public float Ratio;
    }
  }
}
