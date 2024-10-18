// Decompiled with JetBrains decompiler
// Type: SRPG.RaidAreaParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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

    public int Id => this.mId;

    public int Order => this.mOrder;

    public int PeriodId => this.mPeriodId;

    public int BossCount => this.mBossCount;

    public int AreaBossId => this.mAreaBossId;

    public int ClearRewardId => this.mClearRewardId;

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
