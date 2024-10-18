// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidCoolDaysParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GuildRaidCoolDaysParam : GuildRaidMasterParam<JSON_GuildRaidCoolDaysParam>
  {
    public int PeriodId { get; private set; }

    public List<DateTime> Schedule { get; private set; }

    public override bool Deserialize(JSON_GuildRaidCoolDaysParam json)
    {
      if (json == null)
        return false;
      this.PeriodId = json.period_id;
      if (json.schedule == null || json.schedule.Length == 0)
        return false;
      this.Schedule = new List<DateTime>();
      for (int index = 0; index < json.schedule.Length; ++index)
      {
        DateTime result = DateTime.MinValue;
        if (!string.IsNullOrEmpty(json.schedule[index]) && DateTime.TryParse(json.schedule[index], out result))
          this.Schedule.Add(result);
      }
      return true;
    }

    public bool GetCoolDays()
    {
      DateTime serverTime = TimeManager.ServerTime;
      for (int index = 0; index < this.Schedule.Count; ++index)
      {
        if (TimeManager.ServerTime.Year == this.Schedule[index].Year && TimeManager.ServerTime.Month == this.Schedule[index].Month && TimeManager.ServerTime.Day == this.Schedule[index].Day)
          return true;
      }
      return false;
    }
  }
}
