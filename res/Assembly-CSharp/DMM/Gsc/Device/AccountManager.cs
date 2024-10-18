// Decompiled with JetBrains decompiler
// Type: Gsc.Device.AccountManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using DeviceKit;

#nullable disable
namespace Gsc.Device
{
  public class AccountManager : IAccountManager
  {
    private string secretKey;
    private string deviceId;

    public static IAccountManager Create(IAccountManager customManager)
    {
      return customManager ?? (IAccountManager) new AccountManager();
    }

    public string GetSecretKey(string name)
    {
      if (this.secretKey == null && this.deviceId == null)
        App.GetAuthKeys(ref this.secretKey, ref this.deviceId, name);
      return this.secretKey;
    }

    public string GetDeviceId(string name)
    {
      if (this.secretKey == null && this.deviceId == null)
        App.GetAuthKeys(ref this.secretKey, ref this.deviceId, name);
      return this.deviceId;
    }

    public void SetKeyPair(string name, string secretKey, string deviceId)
    {
      App.SetAuthKeys(secretKey, deviceId, name);
      App.GetAuthKeys(ref this.secretKey, ref this.deviceId, name);
    }

    public void SetDeviceId(string name, string deviceId)
    {
      App.SetAuthKeys(this.secretKey, deviceId, name);
      App.GetAuthKeys(ref this.secretKey, ref this.deviceId, name);
    }

    public void Remove(string name)
    {
      App.DeleteAuthKeys(name);
      this.secretKey = (string) null;
      this.deviceId = (string) null;
    }

    public void Reset()
    {
      this.secretKey = (string) null;
      this.deviceId = (string) null;
    }
  }
}
