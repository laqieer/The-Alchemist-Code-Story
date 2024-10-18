// Decompiled with JetBrains decompiler
// Type: SRPG.GuildAttendParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class GuildAttendParam
  {
    private int mID;
    private long mStartAt;
    private long mEndAt;
    private GuildAttendRewardDetail[] mRewards;

    public int id => this.mID;

    public long start_at => this.mStartAt;

    public long end_at => this.mEndAt;

    public GuildAttendRewardDetail[] rewards => this.mRewards;

    public static bool Deserialize(ref GuildAttendParam[] param, JSON_GuildAttendParam[] json)
    {
      if (json == null)
        return false;
      param = new GuildAttendParam[json.Length];
      for (int index = 0; index < json.Length; ++index)
      {
        GuildAttendParam guildAttendParam = new GuildAttendParam();
        guildAttendParam.Deserialize(json[index]);
        param[index] = guildAttendParam;
      }
      return true;
    }

    public void Deserialize(JSON_GuildAttendParam json)
    {
      if (json == null || json.rewards == null)
        return;
      this.mID = json.id;
      this.mStartAt = 0L;
      DateTime result;
      if (!string.IsNullOrEmpty(json.start_at))
      {
        DateTime.TryParse(json.start_at, out result);
        this.mStartAt = TimeManager.GetUnixSec(result);
      }
      this.mEndAt = 0L;
      if (!string.IsNullOrEmpty(json.end_at))
      {
        DateTime.TryParse(json.end_at, out result);
        this.mEndAt = TimeManager.GetUnixSec(result);
      }
      this.mRewards = new GuildAttendRewardDetail[json.rewards.Length];
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        GuildAttendRewardDetail attendRewardDetail = new GuildAttendRewardDetail();
        attendRewardDetail.Deserialize(json.rewards[index]);
        this.mRewards[index] = attendRewardDetail;
      }
    }
  }
}
