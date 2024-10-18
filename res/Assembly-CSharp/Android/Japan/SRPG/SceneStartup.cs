// Decompiled with JetBrains decompiler
// Type: SRPG.SceneStartup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/Scene/Startup")]
  public class SceneStartup : Scene
  {
    public bool AutoStart = true;
    private static bool mResolutionChanged;
    private const string Key_ClearCache = "CLEARCACHE";

    private new void Awake()
    {
      base.Awake();
      MonoSingleton<UrlScheme>.Instance.Ensure();
      MonoSingleton<PaymentManager>.Instance.Ensure();
      MonoSingleton<NetworkError>.Instance.Ensure();
      MonoSingleton<WatchManager>.Instance.Ensure();
      MonoSingleton<PermissionManager>.Instance.Ensure();
      TextAsset textAsset = Resources.Load<TextAsset>("appserveraddr");
      if (!((UnityEngine.Object) textAsset != (UnityEngine.Object) null))
        return;
      Network.SetDefaultHostConfigured(textAsset.text);
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneStartup.\u003CStart\u003Ec__Iterator0() { \u0024this = this };
    }
  }
}
