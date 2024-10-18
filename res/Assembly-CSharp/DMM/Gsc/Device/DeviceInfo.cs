// Decompiled with JetBrains decompiler
// Type: Gsc.Device.DeviceInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

#nullable disable
namespace Gsc.Device
{
  public static class DeviceInfo
  {
    public const string OSNAME = "windows";
    private static readonly string deviceModel;
    private static readonly string deviceVendor;

    static DeviceInfo()
    {
      Match match = new Regex("(?<model>.*)\\((?<vendor>.*)\\)").Match(SystemInfo.deviceModel);
      if (match.Success)
      {
        DeviceInfo.deviceModel = match.Groups["model"].Captures[0].Value.Trim();
        DeviceInfo.deviceVendor = match.Groups["vendor"].Captures[0].Value.Trim();
      }
      else
      {
        DeviceInfo.deviceModel = SystemInfo.deviceModel;
        DeviceInfo.deviceVendor = "<unknown>";
      }
    }

    public static string DeviceModel => DeviceInfo.deviceModel;

    public static string DeviceVendor => DeviceInfo.deviceVendor;

    public static string OperatingSystem => SystemInfo.operatingSystem;

    public static string ProcessorType => SystemInfo.processorType;

    public static int SystemMemorySize => SystemInfo.systemMemorySize << 10;

    public static void SetGraphicsInfo(Dictionary<string, object> data)
    {
      data.Add("graphics_device_name", (object) SystemInfo.graphicsDeviceName);
      data.Add("graphics_device_type", (object) SystemInfo.graphicsDeviceType);
      data.Add("graphics_device_vendor", (object) SystemInfo.graphicsDeviceVendor);
      data.Add("graphics_device_version", (object) SystemInfo.graphicsDeviceVersion);
      data.Add("graphics_memory_size", (object) SystemInfo.graphicsMemorySize);
      data.Add("graphics_multi_threaded", (object) SystemInfo.graphicsMultiThreaded);
      data.Add("graphics_shader_level", (object) SystemInfo.graphicsShaderLevel);
    }
  }
}
