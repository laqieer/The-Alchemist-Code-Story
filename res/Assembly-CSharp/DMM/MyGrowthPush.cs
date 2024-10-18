// Decompiled with JetBrains decompiler
// Type: MyGrowthPush
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;

#nullable disable
public class MyGrowthPush : MonoBehaviour
{
  public string applicationId = string.Empty;
  public string credentialId = string.Empty;
  public string senderId = string.Empty;

  public GrowthPush.Environment getEnvironment()
  {
    return Debug.isDebugBuild ? GrowthPush.Environment.Development : GrowthPush.Environment.Production;
  }

  private void Start() => Debug.Log((object) "[GrowthBeat] - Start");

  private void OnDestroy() => Debug.Log((object) "[GrowthBeat] - Destroy");

  private void Update()
  {
  }

  private void Awake()
  {
    GrowthPush.GetInstance().Initialize(this.applicationId, this.credentialId, this.getEnvironment());
    GrowthPush.GetInstance().RequestDeviceToken(this.senderId);
    GrowthPush.GetInstance().ClearBadge();
    GrowthPush.GetInstance().TrackEvent("Launch");
    GrowthPush.GetInstance().GetDeviceToken();
  }

  public static void registCustomerId(string cuid)
  {
    DebugMenu.Log("PUSH", "GrowthPush Set Tag > customid = " + cuid);
    GrowthPush.GetInstance().SetTag("customid", cuid);
  }
}
