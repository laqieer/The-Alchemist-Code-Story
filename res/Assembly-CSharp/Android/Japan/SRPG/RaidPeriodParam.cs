// Decompiled with JetBrains decompiler
// Type: SRPG.RaidPeriodParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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

    public int Id
    {
      get
      {
        return this.mId;
      }
    }

    public int MaxBp
    {
      get
      {
        return this.mMaxBp;
      }
    }

    public string AddBpTime
    {
      get
      {
        return this.mAddBpTime;
      }
    }

    public int BpByCoin
    {
      get
      {
        return this.mBpByCoin;
      }
    }

    public int RescueMemberMax
    {
      get
      {
        return this.mRescueMemberMax;
      }
    }

    public string RescureSendInterval
    {
      get
      {
        return this.mRescureSendInterval;
      }
    }

    public int CompleteRewardId
    {
      get
      {
        return this.mCompleteRewardId;
      }
    }

    public int RoundBuffMax
    {
      get
      {
        return this.mRoundBuffMax;
      }
    }

    public DateTime BeginAt
    {
      get
      {
        return this.mBeginAt;
      }
    }

    public DateTime EndAt
    {
      get
      {
        return this.mEndAt;
      }
    }

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
      return true;
    }
  }
}
