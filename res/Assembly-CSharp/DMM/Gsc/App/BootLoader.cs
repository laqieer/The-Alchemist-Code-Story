// Decompiled with JetBrains decompiler
// Type: Gsc.App.BootLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.App.NetworkHelper;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;

#nullable disable
namespace Gsc.App
{
  public static class BootLoader
  {
    private static AccountManager mAccountManager = new AccountManager();

    public static BootLoader.BootState BootStates { get; set; }

    [RuntimeInitializeOnLoadMethod]
    private static void OnBoot()
    {
      BootLoader.BootStates = BootLoader.BootState.AWAKE;
      Directory.SetCurrentDirectory(Path.GetFullPath(Application.dataPath + "/../").TrimEnd('\\', '/'));
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

    public static AccountManager GetAccountManager() => BootLoader.mAccountManager;

    [DebuggerHidden]
    private static IEnumerator InitializeApplication()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      BootLoader.\u003CInitializeApplication\u003Ec__Iterator0 applicationCIterator0 = new BootLoader.\u003CInitializeApplication\u003Ec__Iterator0();
      return (IEnumerator) applicationCIterator0;
    }

    public enum BootState
    {
      AWAKE,
      SUCCESS,
      FAILED,
    }
  }
}
