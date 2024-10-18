// Decompiled with JetBrains decompiler
// Type: SRPG.SceneStartup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/Scene/Startup")]
  public class SceneStartup : Scene
  {
    public bool AutoStart = true;
    private const string Key_ClearCache = "CLEARCACHE";
    private static bool mResolutionChanged;

    private new void Awake()
    {
      Application.AdvertisingIdentifierCallback identifierCallback = (Application.AdvertisingIdentifierCallback) ((advertisingId, trackingEnabled, error) =>
      {
        DebugUtility.Log("advertisingId " + advertisingId + " " + (object) trackingEnabled + " " + error);
        GameManager.SetDeviceID(advertisingId);
      });
      // ISSUE: reference to a compiler-generated field
      if (!Application.RequestAdvertisingIdentifierAsync(SceneStartup.\u003C\u003Ef__am\u0024cache2))
      {
        string device_id = Guid.NewGuid().ToString();
        DebugUtility.Log("advertisingId not supported: " + device_id);
        GameManager.SetDeviceID(device_id);
      }
      base.Awake();
      MonoSingleton<UrlScheme>.Instance.Ensure();
      MonoSingleton<PaymentManager>.Instance.Ensure();
      MonoSingleton<NetworkError>.Instance.Ensure();
      UnityEngine.Object.DontDestroyOnLoad(UnityEngine.Object.Instantiate(Resources.Load("fillalpha"), Vector3.zero, Quaternion.identity));
      TextAsset textAsset = Resources.Load<TextAsset>("appserveraddr");
      if (!((UnityEngine.Object) textAsset != (UnityEngine.Object) null))
        return;
      Network.SetDefaultHostConfigured(textAsset.text);
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneStartup.\u003CStart\u003Ec__Iterator9F() { \u003C\u003Ef__this = this };
    }
  }
}
