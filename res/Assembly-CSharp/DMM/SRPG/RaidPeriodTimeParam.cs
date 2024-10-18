// Decompiled with JetBrains decompiler
// Type: SRPG.RaidPeriodTimeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class RaidPeriodTimeParam : RaidMasterParam<JSON_RaidPeriodTimeParam>
  {
    private int mId;
    private int mPeriodId;
    private DateTime mBeginAt;
    private DateTime mEndAt;
    private List<RaidPeriodTimeScheduleParam> mSchedule;

    public int Id => this.mId;

    public int PeriodId => this.mPeriodId;

    public DateTime BeginAt => this.mBeginAt;

    public DateTime EndAt => this.mEndAt;

    public List<RaidPeriodTimeScheduleParam> Schedule => this.mSchedule;

    public override bool Deserialize(JSON_RaidPeriodTimeParam json)
    {
      if (json == null)
        return false;
      this.mId = json.id;
      this.mPeriodId = json.period_id;
      this.mBeginAt = DateTime.MinValue;
      if (!string.IsNullOrEmpty(json.begin_at))
        DateTime.TryParse(json.begin_at, out this.mBeginAt);
      this.mEndAt = DateTime.MaxValue;
      if (!string.IsNullOrEmpty(json.end_at))
        DateTime.TryParse(json.end_at, out this.mEndAt);
      this.mSchedule = new List<RaidPeriodTimeScheduleParam>();
      if (json.schedule != null)
      {
        for (int index = 0; index < json.schedule.Length; ++index)
        {
          RaidPeriodTimeScheduleParam timeScheduleParam = new RaidPeriodTimeScheduleParam();
          if (timeScheduleParam.Deserialize(json.schedule[index]))
            this.mSchedule.Add(timeScheduleParam);
        }
      }
      return true;
    }
  }
}
