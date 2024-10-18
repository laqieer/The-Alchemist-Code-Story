// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_WeatherSetParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
