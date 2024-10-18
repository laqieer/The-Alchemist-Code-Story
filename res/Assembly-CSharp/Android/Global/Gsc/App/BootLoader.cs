// Decompiled with JetBrains decompiler
// Type: Gsc.App.BootLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.App.NetworkHelper;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace Gsc.App
{
  public static class BootLoader
  {
    private static AccountManager mAccountManager = new AccountManager();

    public static BootLoader.BootState BootStates { get; set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnBoot()
    {
      BootLoader.BootStates = BootLoader.BootState.AWAKE;
    }

    public static void GscInit()
    {
      if (BootLoader.BootStates != BootLoader.BootState.AWAKE)
        return;
      SDK.BootLoader.Run(BootLoader.InitializeApplication());
    }

    public static void Reboot()
    {
      SDK.Reset();
      GsccBridge.Reset();
    }

    public static AccountManager GetAccountManager()
    {
      return BootLoader.mAccountManager;
    }

    [DebuggerHidden]
    private static IEnumerator InitializeApplication()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      BootLoader.\u003CInitializeApplication\u003Ec__IteratorE1 applicationCIteratorE1 = new BootLoader.\u003CInitializeApplication\u003Ec__IteratorE1();
      return (IEnumerator) applicationCIteratorE1;
    }

    public enum BootState
    {
      AWAKE,
      SUCCESS,
      FAILED,
    }
  }
}
