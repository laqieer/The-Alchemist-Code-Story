﻿// Decompiled with JetBrains decompiler
// Type: Gsc.App.AccountManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using Gsc.Device;
using SRPG;

namespace Gsc.App
{
  public class AccountManager : IAccountManager
  {
    private string secretKey;
    private string deviceId;

    private void LoadKeys()
    {
      if (!SDK.Initialized || this.secretKey != null || this.deviceId != null)
        return;
      MonoSingleton<GameManager>.Instance.InitAuth();
      this.deviceId = MonoSingleton<GameManager>.Instance.DeviceId;
      this.secretKey = MonoSingleton<GameManager>.Instance.SecretKey;
      if (!string.IsNullOrEmpty(this.deviceId))
        return;
      this.deviceId = (string) null;
    }

    public string GetSecretKey(string name)
    {
      this.LoadKeys();
      return this.secretKey;
    }

    public string GetDeviceId(string name)
    {
      this.LoadKeys();
      return this.deviceId;
    }

    public void SetKeyPair(string name, string secretKey, string deviceId)
    {
      MonoSingleton<GameManager>.Instance.SaveAuthWithKey(deviceId, secretKey);
      this.secretKey = secretKey;
      this.deviceId = deviceId;
    }

    public void SetDeviceId(string name, string deviceId)
    {
      MonoSingleton<GameManager>.Instance.SaveAuth(deviceId);
      this.deviceId = deviceId;
    }

    public void Remove(string name)
    {
      MonoSingleton<GameManager>.Instance.ResetAuth();
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
