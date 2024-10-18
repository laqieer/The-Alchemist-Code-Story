// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.Device
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using DeviceKit;

#nullable disable
namespace Gsc.Auth.GAuth.GAuth
{
  public class Device : IDevice
  {
    public readonly string IDFA;
    public readonly string ID;

    public Device()
    {
      Device.Instance = this;
      this.IDFA = App.GetIdfa();
      this.ID = App.GetClientId();
    }

    public static Device Instance { get; private set; }

    public string Platform => "none";

    public bool initialized => true;

    public bool hasError => false;
  }
}
