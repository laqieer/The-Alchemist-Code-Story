// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidPeriodParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GuildRaidPeriodParam : GuildRaidMasterParam<JSON_GuildRaidPeriodParam>
  {
    public const int DAY_HOUR = 24;

    public int Id { get; private set; }

    public int Bp { get; private set; }

    public int RoundBuffMax { get; private set; }

    public int RoundMax { get; private set; }

    public int HealAp { get; private set; }

    public int DefaultBp { get; private set; }

    public int UnitLevelMin { get; private set; }

    public DateTime BeginAt { get; private set; }

    public DateTime MainBeginAt { get; private set; }

    public DateTime EndAt { get; private set; }

    public int LevelId { get; private set; }

    public DateTime RewardEndAt { get; private set; }

    public DateTime RewardRankingBeginAt { get; private set; }

    public DateTime RewardRankingEndAt { get; private set; }

    public string RewardRankingId { get; private set; }

    public List<GuildRaidPeriodTime> Schedule { get; private set; }

    public GuildRaidManager.GuildRaidApDrawType BpType { get; private set; }

    public override bool Deserialize(JSON_GuildRaidPeriodParam json)
    {
      if (json == null)
        return false;
      this.Id = json.id;
      this.Bp = json.bp;
      this.RoundMax = json.round_max;
      this.RoundBuffMax = json.round_buff_max;
      this.HealAp = json.heal_ap;
      this.UnitLevelMin = json.unit_lv_min;
      this.DefaultBp = json.default_bp;
      this.BpType = (GuildRaidManager.GuildRaidApDrawType) json.bp_type;
      this.BeginAt = DateTime.MinValue;
      DateTime result1;
      if (!string.IsNullOrEmpty(json.begin_at) && DateTime.TryParse(json.begin_at, out result1))
        this.BeginAt = result1;
      this.MainBeginAt = DateTime.MinValue;
      DateTime result2;
      if (!string.IsNullOrEmpty(json.main_begin_at) && DateTime.TryParse(json.main_begin_at, out result2))
        this.MainBeginAt = result2;
      this.EndAt = DateTime.MinValue;
      DateTime result3;
      if (!string.IsNullOrEmpty(json.end_at) && DateTime.TryParse(json.end_at, out result3))
        this.EndAt = result3;
      this.RewardEndAt = DateTime.MinValue;
      DateTime result4;
      if (!string.IsNullOrEmpty(json.reward_end_at) && DateTime.TryParse(json.reward_end_at, out result4))
        this.RewardEndAt = result4;
      this.RewardRankingBeginAt = DateTime.MinValue;
      DateTime result5;
      if (!string.IsNullOrEmpty(json.reward_ranking_begin_at) && DateTime.TryParse(json.reward_ranking_begin_at, out result5))
        this.RewardRankingBeginAt = result5;
      this.RewardRankingEndAt = DateTime.MinValue;
      DateTime result6;
      if (!string.IsNullOrEmpty(json.reward_ranking_end_at) && DateTime.TryParse(json.reward_ranking_end_at, out result6))
        this.RewardRankingEndAt = result6;
      this.Schedule = new List<GuildRaidPeriodTime>();
      if (json.schedule != null)
      {
        for (int index = 0; index < json.schedule.Length; ++index)
        {
          GuildRaidPeriodTime guildRaidPeriodTime = new GuildRaidPeriodTime();
          if (guildRaidPeriodTime.Deserialize(json.schedule[index]))
            this.Schedule.Add(guildRaidPeriodTime);
        }
      }
      this.RewardRankingId = json.reward_ranking_id;
      return true;
    }

    public int GetMaxAp() => this.HealAp * this.Bp;

    public GuildRaidManager.GuildRaidScheduleType ConfirmScheduleType()
    {
      DateTime serverTime = TimeManager.ServerTime;
      if (this.Schedule == null || this.Schedule.Count == 0)
        return this.BeginAt > serverTime || serverTime > this.EndAt ? GuildRaidManager.GuildRaidScheduleType.Close : GuildRaidManager.GuildRaidScheduleType.Open;
      if (this.BeginAt > serverTime || serverTime > this.EndAt)
        return GuildRaidManager.GuildRaidScheduleType.Close;
      for (int index = 0; index < this.Schedule.Count; ++index)
      {
        DateTime dateTime1 = DateTime.Parse(TimeManager.ServerTime.ToShortDateString() + " " + this.Schedule[index].Begin + ":00");
        TimeSpan timeSpan = this.GetTimeSpan(this.Schedule[index].Open);
        DateTime dateTime2 = dateTime1 + timeSpan;
        if (dateTime1 <= serverTime && serverTime < dateTime2)
          return GuildRaidManager.GuildRaidScheduleType.OpenSchedule;
      }
      return GuildRaidManager.GuildRaidScheduleType.CloseSchedule;
    }

    public bool IsRewardRankingTimeBetween()
    {
      DateTime serverTime = TimeManager.ServerTime;
      return this.RewardRankingBeginAt <= serverTime && serverTime < this.RewardRankingEndAt;
    }

    public TimeSpan GetTimeSpan(string hTime)
    {
      string[] strArray = hTime.Split(':');
      int[] numArray = new int[3];
      if (strArray.Length == 2)
        numArray[2] = 0;
      for (int index = 0; index < strArray.Length; ++index)
        numArray[index] = int.Parse(strArray[index]);
      int num1 = numArray[0];
      int num2 = numArray[1];
      int num3 = numArray[2];
      DateTime dateTime1 = new DateTime(0L);
      DateTime dateTime2 = new DateTime(0L);
      dateTime1 = dateTime1.AddHours((double) num1);
      dateTime1 = dateTime1.AddMinutes((double) num2);
      dateTime1 = dateTime1.AddSeconds((double) num3);
      return !(dateTime1 >= dateTime2.AddDays(1.0)) ? new TimeSpan(dateTime1.Hour, dateTime1.Minute, dateTime1.Second) : new TimeSpan(dateTime1.Hour + 24, dateTime1.Minute, dateTime1.Second);
    }
  }
}
