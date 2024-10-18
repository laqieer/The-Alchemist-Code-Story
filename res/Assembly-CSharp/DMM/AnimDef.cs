﻿// Decompiled with JetBrains decompiler
// Type: AnimDef
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AnimDef : ScriptableObject
{
  private static AnimDef mDefaultEmptyAnimation;
  public AnimationClip animation;
  public AnimationCurve rootTranslationX;
  public AnimationCurve rootTranslationY;
  public AnimationCurve rootTranslationZ;
  public bool UseRootMotion = true;
  public string rootBoneName;
  public bool IsParentPosZero;
  public bool IsRidingUnitCHM;
  public AnimEvent[] events;
  public List<string> CurveNames;
  [HideInInspector]
  public AnimDef.CustomCurve[] CustomCurves = new AnimDef.CustomCurve[0];

  public static AnimDef DefaultEmptyAnimation
  {
    get
    {
      if (Object.op_Equality((Object) AnimDef.mDefaultEmptyAnimation, (Object) null))
      {
        AnimDef.mDefaultEmptyAnimation = ScriptableObject.CreateInstance<AnimDef>();
        AnimDef.mDefaultEmptyAnimation.animation = new AnimationClip();
        ((Object) AnimDef.mDefaultEmptyAnimation.animation).hideFlags = (HideFlags) 52;
        AnimDef.mDefaultEmptyAnimation.animation.legacy = true;
      }
      return AnimDef.mDefaultEmptyAnimation;
    }
  }

  public float Length
  {
    get
    {
      return Object.op_Inequality((Object) this.animation, (Object) null) ? this.animation.length : 0.0f;
    }
  }

  public AnimationCurve FindCustomCurve(string curveName)
  {
    for (int index = 0; index < this.CustomCurves.Length; ++index)
    {
      if (this.CustomCurves[index].Name == curveName)
        return this.CustomCurves[index].CurveData;
    }
    return (AnimationCurve) null;
  }

  public bool IsValid => Object.op_Inequality((Object) this.animation, (Object) null);

  public T[] GetAnimationEvents<T>() where T : AnimEvent
  {
    List<AnimEvent> animEventList = new List<AnimEvent>(this.events.Length);
    for (int index = 0; index < this.events.Length; ++index)
    {
      if (this.events[index] is T)
        animEventList.Add(this.events[index]);
    }
    return (T[]) animEventList.ToArray();
  }

  [Serializable]
  public struct CustomCurve
  {
    public string Name;
    public AnimationCurve CurveData;
    public string Source;
  }
}
