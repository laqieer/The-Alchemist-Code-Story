﻿// Decompiled with JetBrains decompiler
// Type: Gsc.Device.AccountManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using DeviceKit;

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
        App.GetAuthKeys(out this.secretKey, out this.deviceId, name);
      return this.secretKey;
    }

    public string GetDeviceId(string name)
    {
      if (this.secretKey == null && this.deviceId == null)
        App.GetAuthKeys(out this.secretKey, out this.deviceId, name);
      return this.deviceId;
    }

    public void SetKeyPair(string name, string secretKey, string deviceId)
    {
      App.SetAuthKeys(secretKey, deviceId, name);
      App.GetAuthKeys(out this.secretKey, out this.deviceId, name);
    }

    public void SetDeviceId(string name, string deviceId)
    {
      App.SetAuthKeys(this.secretKey, deviceId, name);
      App.GetAuthKeys(out this.secretKey, out this.deviceId, name);
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