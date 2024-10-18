﻿// Decompiled with JetBrains decompiler
// Type: SRPG.SceneDummyStartup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class SceneDummyStartup : Scene
  {
    private static bool mResolutionChanged;

    private new void Awake()
    {
      base.Awake();
      MonoSingleton<UrlScheme>.Instance.Ensure();
      MonoSingleton<PaymentManager>.Instance.Ensure();
      MonoSingleton<NetworkError>.Instance.Ensure();
      if (SceneDummyStartup.mResolutionChanged)
        return;
      int defaultScreenWidth = ScreenUtility.DefaultScreenWidth;
      int defaultScreenHeight = ScreenUtility.DefaultScreenHeight;
      float num1 = (float) defaultScreenWidth / (float) defaultScreenHeight;
      int num2 = Mathf.Min(defaultScreenHeight, 750);
      int w = Mathf.FloorToInt(num1 * (float) num2);
      int h = num2;
      ScreenUtility.SetResolution(w, h);
      SceneDummyStartup.mResolutionChanged = true;
      UnityEngine.Debug.Log((object) string.Format("Changing Resolution to [{0} x {1}] from [{2} x {3}]", new object[4]
      {
        (object) w,
        (object) h,
        (object) Screen.width,
        (object) Screen.height
      }));
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      SceneDummyStartup.\u003CStart\u003Ec__Iterator9B startCIterator9B = new SceneDummyStartup.\u003CStart\u003Ec__Iterator9B();
      return (IEnumerator) startCIterator9B;
    }
  }
}
