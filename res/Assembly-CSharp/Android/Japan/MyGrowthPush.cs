// Decompiled with JetBrains decompiler
// Type: MyGrowthPush
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

public class MyGrowthPush : MonoBehaviour
{
  public string applicationId = string.Empty;
  public string credentialId = string.Empty;
  public string senderId = string.Empty;

  public GrowthPush.Environment getEnvironment()
  {
    return Debug.isDebugBuild ? GrowthPush.Environment.Development : GrowthPush.Environment.Production;
  }

  private void Start()
  {
    Debug.Log((object) "[GrowthBeat] - Start");
  }

  private void OnDestroy()
  {
    Debug.Log((object) "[GrowthBeat] - Destroy");
  }

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
