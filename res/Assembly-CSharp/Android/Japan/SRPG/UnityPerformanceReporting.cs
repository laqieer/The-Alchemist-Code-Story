// Decompiled with JetBrains decompiler
// Type: SRPG.UnityPerformanceReporting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.CrashLog;

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
