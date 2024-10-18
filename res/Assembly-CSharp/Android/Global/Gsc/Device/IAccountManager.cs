// Decompiled with JetBrains decompiler
// Type: Gsc.Device.IAccountManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
