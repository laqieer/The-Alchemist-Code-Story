// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeCategoryParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  public class ChallengeCategoryParam
  {
    public string iname;
    public TimeParser begin_at = new TimeParser();
    public TimeParser end_at = new TimeParser();
    public int prio;
    public TimeParser beginner_period = new TimeParser();

    public int Priority => this.prio;

    public bool Deserialize(JSON_ChallengeCategoryParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.begin_at.Set(json.begin_at, DateTime.MinValue);
      this.end_at.Set(json.end_at, DateTime.MaxValue);
      this.prio = json.prio;
      this.beginner_period.Set(json.beginner_period, DateTime.MinValue);
      return true;
    }

    public bool IsAvailablePeriod(DateTime now)
    {
      DateTime dateTimes1 = this.begin_at.DateTimes;
      DateTime dateTimes2 = this.end_at.DateTimes;
      return now >= dateTimes1 && dateTimes2 >= now && MonoSingleton<GameManager>.Instance.Player.NewGameAtDateTime >= this.beginner_period.DateTimes;
    }
  }
}
