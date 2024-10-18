﻿// Decompiled with JetBrains decompiler
// Type: SRPG.GuerrillaShopScheduleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class GuerrillaShopScheduleParam
  {
    public int id;
    public string begin_at;
    public string end_at;
    public int accum_ap;
    public string open_time;
    public string cool_time;
    public GuerrillaShopScheduleAdvent[] advent;

    public bool Deserialize(JSON_GuerrillaShopScheduleParam json)
    {
      this.id = json.id;
      this.begin_at = json.begin_at;
      this.end_at = json.end_at;
      this.accum_ap = json.accum_ap;
      this.open_time = json.open_time;
      this.cool_time = json.cool_time;
      if (json.advent != null)
      {
        GuerrillaShopScheduleAdvent[] shopScheduleAdventArray = new GuerrillaShopScheduleAdvent[json.advent.Length];
        for (int index = 0; index < json.advent.Length; ++index)
        {
          shopScheduleAdventArray[index] = new GuerrillaShopScheduleAdvent();
          shopScheduleAdventArray[index].id = json.advent[index].id;
          shopScheduleAdventArray[index].coef = json.advent[index].coef;
        }
      }
      return true;
    }
  }
}
