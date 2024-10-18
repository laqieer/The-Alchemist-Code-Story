// Decompiled with JetBrains decompiler
// Type: GrowthPush
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
public class GrowthPush
{
  private static GrowthPush instance = new GrowthPush();

  private GrowthPush()
  {
  }

  public static GrowthPush GetInstance() => GrowthPush.instance;

  public void Initialize(
    string applicationId,
    string credentialId,
    GrowthPush.Environment environment)
  {
    this.Initialize(applicationId, credentialId, environment, true);
  }

  public void Initialize(
    string applicationId,
    string credentialId,
    GrowthPush.Environment environment,
    bool adInfoEnable)
  {
    this.Initialize(applicationId, credentialId, environment, adInfoEnable, (string) null);
  }

  public void Initialize(
    string applicationId,
    string credentialId,
    GrowthPush.Environment environment,
    bool adInfoEnable,
    string channelId)
  {
  }

  public void RequestDeviceToken(string senderId) => this.RequestDeviceToken();

  public void RequestDeviceToken()
  {
  }

  public string GetDeviceToken() => (string) null;

  public void SetDeviceToken(string deviceToken)
  {
  }

  public void ClearBadge()
  {
  }

  public void SetTag(string name) => this.SetTag(name, string.Empty);

  public void SetTag(string name, string value)
  {
  }

  public void TrackEvent(string name) => this.TrackEvent(name, string.Empty);

  public void TrackEvent(string name, string value)
  {
  }

  public void TrackEvent(string name, string value, string gameObject, string methodName)
  {
  }

  public void RenderMessage(string uuid)
  {
  }

  public void SetChannelId(string channelId)
  {
  }

  public void DeleteDefaultNotificationChannel()
  {
  }

  public void SetBaseUrl(string baseUrl)
  {
  }

  public enum Environment
  {
    Unknown,
    Development,
    Production,
  }
}
