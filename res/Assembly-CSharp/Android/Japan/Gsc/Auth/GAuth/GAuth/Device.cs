// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.Device
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using DeviceKit;

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

    public string Platform
    {
      get
      {
        return "none";
      }
    }

    public bool initialized
    {
      get
      {
        return true;
      }
    }

    public bool hasError
    {
      get
      {
        return false;
      }
    }
  }
}
