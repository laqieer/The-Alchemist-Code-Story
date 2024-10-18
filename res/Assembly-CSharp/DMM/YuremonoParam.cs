// Decompiled with JetBrains decompiler
// Type: YuremonoParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
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
    public string TargetName;
    [Range(0.0f, 1f)]
    public float Kinematic = 1f;
    [Range(0.0f, 1f)]
    public float Gravity = 1f;
    public float Length = 1f;
    public YuremonoParam.Axes ForwardAxis;
    public float Damping = 0.1f;
    public float Acceleration = 10f;
    [Range(0.0f, 90f)]
    public float AngularLimit;
  }

  [Serializable]
  public class SkirtTraceParam
  {
    public string Name;
    [Range(0.0f, 1f)]
    public float SkirtRatio;
    [Range(0.0f, 360f)]
    public float RotWaitAng;
    public string TargetName;
    public YuremonoParam.SkirtTraceParam.EikyoSkirt[] EikyoSkirts = new YuremonoParam.SkirtTraceParam.EikyoSkirt[0];

    [Serializable]
    public class EikyoSkirt
    {
      public string Name;
      [Range(0.0f, 1f)]
      public float Ratio;
    }
  }
}
