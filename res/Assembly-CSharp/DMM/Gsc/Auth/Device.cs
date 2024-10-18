// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.Device
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Device;
using System.Collections.Generic;

#nullable disable
namespace Gsc.Auth
{
  public static class Device
  {
    private static IDevice device;

    public static string Platform => Gsc.Auth.Device.device.Platform;

    public static bool initialized => Gsc.Auth.Device.device.initialized;

    public static bool hasError => Gsc.Auth.Device.device.hasError;

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
