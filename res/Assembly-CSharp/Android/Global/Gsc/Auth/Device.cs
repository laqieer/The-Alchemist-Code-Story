// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.Device
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
      Gsc.Auth.Device.device = (IDevice) new Gsc.Auth.GAuth.GAuth.Device();
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
