// Decompiled with JetBrains decompiler
// Type: GrowthPush
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

public class GrowthPush
{
  private static GrowthPush instance = new GrowthPush();

  private GrowthPush()
  {
  }

  public static GrowthPush GetInstance()
  {
    return GrowthPush.instance;
  }

  public void Initialize(string applicationId, string credentialId, GrowthPush.Environment environment)
  {
    this.Initialize(applicationId, credentialId, environment, true);
  }

  public void Initialize(string applicationId, string credentialId, GrowthPush.Environment environment, bool adInfoEnable)
  {
    this.Initialize(applicationId, credentialId, environment, adInfoEnable, (string) null);
  }

  public void Initialize(string applicationId, string credentialId, GrowthPush.Environment environment, bool adInfoEnable, string channelId)
  {
  }

  public void RequestDeviceToken(string senderId)
  {
  }

  public void RequestDeviceToken()
  {
  }

  public string GetDeviceToken()
  {
    return (string) null;
  }

  public void SetDeviceToken(string deviceToken)
  {
  }

  public void ClearBadge()
  {
  }

  public void SetTag(string name)
  {
    this.SetTag(name, string.Empty);
  }

  public void SetTag(string name, string value)
  {
  }

  public void TrackEvent(string name)
  {
    this.TrackEvent(name, string.Empty);
  }

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
