// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_WeatherSetParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_WeatherSetParam
  {
    public string iname;
    public string name;
    public string[] st_wth;
    public int[] st_rate;
    public int ch_cl_min;
    public int ch_cl_max;
    public string[] ch_wth;
    public int[] ch_rate;
  }
}
