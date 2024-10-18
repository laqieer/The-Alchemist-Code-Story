// Decompiled with JetBrains decompiler
// Type: Gsc.Device.IAccountManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace Gsc.Device
{
  public interface IAccountManager
  {
    string GetSecretKey(string name);

    string GetDeviceId(string name);

    void SetKeyPair(string name, string secretKey, string deviceId);

    void SetDeviceId(string name, string deviceId);

    void Remove(string name);

    void Reset();
  }
}
