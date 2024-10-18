// Decompiled with JetBrains decompiler
// Type: SRPG.SceneStartup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack.Resolvers;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/Scene/Startup")]
  public class SceneStartup : Scene
  {
    private static bool mResolutionChanged;
    private const string Key_ClearCache = "CLEARCACHE";
    public bool AutoStart = true;

    private new void Awake()
    {
      base.Awake();
      MonoSingleton<UrlScheme>.Instance.Ensure();
      MonoSingleton<PaymentManager>.Instance.Ensure();
      MonoSingleton<NetworkError>.Instance.Ensure();
      MonoSingleton<WatchManager>.Instance.Ensure();
      MonoSingleton<PermissionManager>.Instance.Ensure();
      TextAsset textAsset = Resources.Load<TextAsset>("appserveraddr");
      if (Object.op_Inequality((Object) textAsset, (Object) null))
        Network.SetDefaultHostConfigured(textAsset.text);
      SceneStartup.SetupMessagePackResolvers();
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneStartup.\u003CStart\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public static void SetupMessagePackResolvers()
    {
      CompositeResolver.RegisterAndSetAsDefault(GeneratedResolver.Instance, BuiltinResolver.Instance, AttributeFormatterResolver.Instance, PrimitiveObjectResolver.Instance);
    }
  }
}
