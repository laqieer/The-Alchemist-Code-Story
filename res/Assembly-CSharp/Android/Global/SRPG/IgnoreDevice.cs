﻿// Decompiled with JetBrains decompiler
// Type: SRPG.IgnoreDevice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class IgnoreDevice
  {
    public List<string> type_name_list = new List<string>();
    public string maker;
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
      using (List<string>.Enumerator enumerator = this.type_name_list.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          if (enumerator.Current == str_device.ToLower())
            return true;
        }
      }
      return false;
    }
  }
}
