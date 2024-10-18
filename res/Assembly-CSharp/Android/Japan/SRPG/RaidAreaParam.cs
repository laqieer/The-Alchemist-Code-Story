// Decompiled with JetBrains decompiler
// Type: SRPG.RaidAreaParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class RaidAreaParam : RaidMasterParam<JSON_RaidAreaParam>
  {
    private int mId;
    private int mOrder;
    private int mPeriodId;
    private int mBossCount;
    private int mAreaBossId;
    private int mClearRewardId;

    public int Id
    {
      get
      {
        return this.mId;
      }
    }

    public int Order
    {
      get
      {
        return this.mOrder;
      }
    }

    public int PeriodId
    {
      get
      {
        return this.mPeriodId;
      }
    }

    public int BossCount
    {
      get
      {
        return this.mBossCount;
      }
    }

    public int AreaBossId
    {
      get
      {
        return this.mAreaBossId;
      }
    }

    public int ClearRewardId
    {
      get
      {
        return this.mClearRewardId;
      }
    }

    public override bool Deserialize(JSON_RaidAreaParam json)
    {
      if (json == null)
        return false;
      this.mId = json.id;
      this.mOrder = json.order;
      this.mPeriodId = json.period_id;
      this.mBossCount = json.boss_count;
      this.mAreaBossId = json.area_boss_id;
      this.mClearRewardId = json.clear_reward_id;
      return true;
    }
  }
}
