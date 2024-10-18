// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.Device
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Device;
using System.Collections.Generic;

namespace Gsc.Auth
{
  public static class Device
  {
    private static IDevice device;

    public static string Platform
    {
      get
      {
        return Gsc.Auth.Device.device.Platform;
      }
    }

    public static bool initialized
    {
      get
      {
        return Gsc.Auth.Device.device.initialized;
      }
    }

    public static bool hasError
    {
      get
      {
        return Gsc.Auth.Device.device.hasError;
      }
    }

    public static void Initialize()
    {
      if (Gsc.Auth.Device.device != null)
        return;
      Gsc.Auth.Device.device = (IDevice) new Gsc.Auth.GAuth.DMMGamesStore.Device();
    }

    public static void SetAuthDeviceData(Dictionary<string, object> data)
    {
      data.Add("operating_system", (object) DeviceInfo.OperatingSystem);
      data.Add("processor_type", (object) DeviceInfo.ProcessorType);
      data.Add("device_model", (object) DeviceInfo.DeviceModel);
      data.Add("device_vendor", (object) DeviceInfo.DeviceVendor);
    }
  }
}
