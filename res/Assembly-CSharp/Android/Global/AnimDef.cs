// Decompiled with JetBrains decompiler
// Type: AnimDef
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimDef : ScriptableObject
{
  public bool UseRootMotion = true;
  [HideInInspector]
  public AnimDef.CustomCurve[] CustomCurves = new AnimDef.CustomCurve[0];
  private static AnimDef mDefaultEmptyAnimation;
  public AnimationClip animation;
  public AnimationCurve rootTranslationX;
  public AnimationCurve rootTranslationY;
  public AnimationCurve rootTranslationZ;
  public string rootBoneName;
  public bool IsParentPosZero;
  public AnimEvent[] events;
  public List<string> CurveNames;

  public static AnimDef DefaultEmptyAnimation
  {
    get
    {
      if ((UnityEngine.Object) AnimDef.mDefaultEmptyAnimation == (UnityEngine.Object) null)
      {
        AnimDef.mDefaultEmptyAnimation = ScriptableObject.CreateInstance<AnimDef>();
        AnimDef.mDefaultEmptyAnimation.animation = new AnimationClip();
        AnimDef.mDefaultEmptyAnimation.animation.hideFlags = HideFlags.DontSave;
        AnimDef.mDefaultEmptyAnimation.animation.legacy = true;
      }
      return AnimDef.mDefaultEmptyAnimation;
    }
  }

  public float Length
  {
    get
    {
      if ((UnityEngine.Object) this.animation != (UnityEngine.Object) null)
        return this.animation.length;
      return 0.0f;
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

  public bool IsValid
  {
    get
    {
      return (UnityEngine.Object) this.animation != (UnityEngine.Object) null;
    }
  }

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
