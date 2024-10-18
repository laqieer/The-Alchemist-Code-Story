// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRankRankingRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class VersusRankRankingRewardParam
  {
    private int mScheduleId;
    private int mRankBegin;
    private int mRankEnd;
    private string mRewardId;

    public int ScheduleId => this.mScheduleId;

    public int RankBegin => this.mRankBegin;

    public int RankEnd => this.mRankEnd;

    public string RewardId => this.mRewardId;

    public bool Deserialize(JSON_VersusRankRankingRewardParam json)
    {
      if (json == null)
        return false;
      this.mScheduleId = json.schedule_id;
      this.mRankBegin = json.rank_begin;
      this.mRankEnd = json.rank_end;
      this.mRewardId = json.reward_id;
      return true;
    }
  }
}
