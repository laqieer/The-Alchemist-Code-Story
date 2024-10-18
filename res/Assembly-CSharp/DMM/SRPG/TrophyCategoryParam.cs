// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyCategoryParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  public class TrophyCategoryParam
  {
    public string iname;
    public int hash_code;
    public TrophyCategorys category;
    public bool is_not_pull;
    public int days;
    public int beginner;
    public TimeParser begin_at = new TimeParser();
    public TimeParser end_at = new TimeParser();
    public string linked_quest;
    public string banner;

    public bool IsNotPull => this.is_not_pull;

    public bool IsBeginner => this.beginner == 1;

    public bool IsDaily => this.days == 1;

    public bool IsLinekedQuest => !string.IsNullOrEmpty(this.linked_quest);

    public bool Deserialize(JSON_TrophyCategoryParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.hash_code = this.iname.GetHashCode();
      this.is_not_pull = json.is_not_pull == 1;
      this.days = json.day_reset;
      this.beginner = json.bgnr;
      this.begin_at.Set(json.begin_at, DateTime.MinValue);
      this.end_at.Set(json.end_at, DateTime.MaxValue);
      this.category = json.category == 0 ? TrophyCategorys.Other : (TrophyCategorys) json.category;
      this.linked_quest = json.linked_quest;
      this.banner = json.banner;
      return true;
    }

    public bool IsAvailablePeriod(DateTime now, bool is_grace)
    {
      DateTime dateTimes = this.begin_at.DateTimes;
      DateTime times = this.end_at.DateTimes;
      if (this.IsBeginner)
      {
        DateTime beginnerEndTime = MonoSingleton<GameManager>.Instance.Player.GetBeginnerEndTime();
        times = !(times <= beginnerEndTime) ? beginnerEndTime : times;
      }
      if (is_grace)
        times = this.AddTimeSpan(times, this.GetGraceRewardSpan());
      return now >= dateTimes && times >= now;
    }

    public bool IsOpenLinekedQuest(DateTime now, bool is_grace)
    {
      if (!this.IsLinekedQuest)
        return true;
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(this.linked_quest);
      if (quest == null || !MonoSingleton<GameManager>.Instance.Player.IsBeginner() && quest.IsBeginner)
        return false;
      bool flag;
      if (quest.IsJigen)
      {
        DateTime dateTime1 = TimeManager.FromUnixTime(quest.start);
        DateTime dateTime2 = this.AddTimeSpan(TimeManager.FromUnixTime(quest.end), !is_grace ? this.GetQuestGrace() : this.GetGraceRewardSpan());
        flag = dateTime1 <= now && now < dateTime2;
      }
      else
        flag = !quest.hidden;
      return flag;
    }

    public DateTime GetQuestTime(DateTime base_time, bool is_quest_grace)
    {
      if (!this.IsLinekedQuest)
        return base_time;
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(this.linked_quest);
      return quest == null ? base_time : (!is_quest_grace ? TimeManager.FromUnixTime(quest.end) : this.AddTimeSpan(TimeManager.FromUnixTime(quest.end), this.GetQuestGrace()));
    }

    private DateTime AddTimeSpan(DateTime times, TimeSpan span)
    {
      if (times.Equals(DateTime.MaxValue))
        return times;
      try
      {
        times = times.Add(span);
      }
      catch (Exception ex)
      {
        times = DateTime.MaxValue;
      }
      return times;
    }

    private TimeSpan GetGraceRewardSpan() => new TimeSpan(14, 0, 0, 0);

    private TimeSpan GetQuestGrace()
    {
      return this.IsDaily ? new TimeSpan(0, 0, 0, 0) : new TimeSpan(1, 0, 0, 0);
    }
  }
}
