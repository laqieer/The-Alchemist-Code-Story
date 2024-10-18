// Decompiled with JetBrains decompiler
// Type: SRPG.UnityPerformanceReporting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.CrashLog;

#nullable disable
namespace SRPG
{
  public class UnityPerformanceReporting : MonoBehaviour
  {
    private void Awake()
    {
      CrashReporting.Init("8d9b4183-a378-4c53-b66a-b5ac3d9a531a", MyApplicationPlugin.version, string.Empty);
    }
  }
}
