// Decompiled with JetBrains decompiler
// Type: SRPG.IgnoreDevice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class IgnoreDevice
  {
    public string maker;
    public List<string> type_name_list = new List<string>();
    public string os_version;

    public void SetDevices(string str_maker, string[] device_list, string str_osversion)
    {
      this.maker = str_maker.ToLower();
      foreach (string device in device_list)
        this.type_name_list.Add(device.ToLower());
      this.os_version = str_osversion.ToLower();
    }

    public bool checkIgnoreDevice(string str_maker, string str_device, string str_osversion)
    {
      if (this.os_version != str_osversion.ToLower() || this.maker != str_maker.ToLower())
        return false;
      foreach (string typeName in this.type_name_list)
      {
        if (typeName == str_device.ToLower())
          return true;
      }
      return false;
    }
  }
}
