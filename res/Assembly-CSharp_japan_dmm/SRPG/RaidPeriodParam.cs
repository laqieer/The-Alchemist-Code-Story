// Decompiled with JetBrains decompiler
// Type: SRPG.RaidPeriodParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class RaidPeriodParam : RaidMasterParam<JSON_RaidPeriodParam>
  {
    private int mId;
    private int mMaxBp;
    private string mAddBpTime;
    private int mBpByCoin;
    private int mRescueMemberMax;
    private string mRescureSendInterval;
    private int mCompleteRewardId;
    private int mRoundBuffMax;
    private DateTime mBeginAt;
    private DateTime mEndAt;
    private DateTime mRewardBeginAt;
    private DateTime mRewardEndAt;

    public int Id => this.mId;

    public int MaxBp => this.mMaxBp;

    public string AddBpTime => this.mAddBpTime;

    public int BpByCoin => this.mBpByCoin;

    public int RescueMemberMax => this.mRescueMemberMax;

    public string RescureSendInterval => this.mRescureSendInterval;

    public int CompleteRewardId => this.mCompleteRewardId;

    public int RoundBuffMax => this.mRoundBuffMax;

    public DateTime BeginAt => this.mBeginAt;

    public DateTime EndAt => this.mEndAt;

    public DateTime RewardBeginAt => this.mRewardBeginAt;

    public DateTime RewardEndAt => this.mRewardEndAt;

    public override bool Deserialize(JSON_RaidPeriodParam json)
    {
      if (json == null)
        return false;
      this.mId = json.id;
      this.mMaxBp = json.max_bp;
      this.mAddBpTime = json.add_bp_time;
      this.mBpByCoin = json.bp_by_coin;
      this.mRescueMemberMax = json.rescue_member_max;
      this.mRescureSendInterval = json.rescure_send_interval;
      this.mCompleteRewardId = json.complete_reward_id;
      this.mRoundBuffMax = json.round_buff_max;
      this.mBeginAt = DateTime.MinValue;
      if (!string.IsNullOrEmpty(json.begin_at))
        DateTime.TryParse(json.begin_at, out this.mBeginAt);
      this.mEndAt = DateTime.MaxValue;
      if (!string.IsNullOrEmpty(json.end_at))
        DateTime.TryParse(json.end_at, out this.mEndAt);
      this.mRewardBeginAt = DateTime.MinValue;
      if (!string.IsNullOrEmpty(json.reward_begin_at))
        DateTime.TryParse(json.reward_begin_at, out this.mRewardBeginAt);
      this.mRewardEndAt = DateTime.MinValue;
      if (!string.IsNullOrEmpty(json.reward_end_at))
        DateTime.TryParse(json.reward_end_at, out this.mRewardEndAt);
      return true;
    }
  }
}
