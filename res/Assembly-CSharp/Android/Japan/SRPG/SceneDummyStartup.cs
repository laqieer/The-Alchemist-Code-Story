// Decompiled with JetBrains decompiler
// Type: SRPG.SceneDummyStartup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
      Debug.Log((object) string.Format("Changing Resolution to [{0} x {1}] from [{2} x {3}]", (object) w, (object) h, (object) Screen.width, (object) Screen.height));
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      SceneDummyStartup.\u003CStart\u003Ec__Iterator0 startCIterator0 = new SceneDummyStartup.\u003CStart\u003Ec__Iterator0();
      return (IEnumerator) startCIterator0;
    }
  }
}
