// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisLapBossParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GenesisLapBossParam
  {
    private string mIname;
    private int mRoundBuffMax;
    private string mBossBuffId;
    private string mOtherBuffId;
    private GenesisLapBossParam.LapInfo[] mLapInfo;
    private BuffEffectParam mBossBuffParam;
    private BuffEffectParam mOtherBuffParam;

    public string Iname => this.mIname;

    public int RoundBuffMax => this.mRoundBuffMax;

    public string BossBuffId => this.mBossBuffId;

    public BuffEffectParam BossBuffParam
    {
      get
      {
        if (this.mBossBuffParam == null)
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (Object.op_Implicit((Object) instance) && instance.MasterParam != null)
            this.mBossBuffParam = instance.MasterParam.GetBuffEffectParam(this.mBossBuffId);
        }
        return this.mBossBuffParam;
      }
    }

    public string OtherBuffId => this.mOtherBuffId;

    public BuffEffectParam OtherBuffParam
    {
      get
      {
        if (this.mOtherBuffParam == null)
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (Object.op_Implicit((Object) instance) && instance.MasterParam != null)
            this.mOtherBuffParam = instance.MasterParam.GetBuffEffectParam(this.mOtherBuffId);
        }
        return this.mOtherBuffParam;
      }
    }

    public List<GenesisLapBossParam.LapInfo> LapInfoList
    {
      get
      {
        return this.mLapInfo != null ? new List<GenesisLapBossParam.LapInfo>((IEnumerable<GenesisLapBossParam.LapInfo>) this.mLapInfo) : new List<GenesisLapBossParam.LapInfo>();
      }
    }

    public void Deserialize(JSON_GenesisLapBossParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mRoundBuffMax = json.round_buff_max;
      this.mBossBuffId = json.boss_bid;
      this.mOtherBuffId = json.other_bid;
      this.mLapInfo = (GenesisLapBossParam.LapInfo[]) null;
      if (json.lap_info == null || json.lap_info.Length == 0)
        return;
      this.mLapInfo = new GenesisLapBossParam.LapInfo[json.lap_info.Length];
      for (int index = 0; index < json.lap_info.Length; ++index)
      {
        this.mLapInfo[index] = new GenesisLapBossParam.LapInfo();
        this.mLapInfo[index].Deserialize(json.lap_info[index]);
      }
    }

    public GenesisRewardParam GetRoundReward(int round)
    {
      List<GenesisLapBossParam.LapInfo> lapInfoList = this.LapInfoList;
      if (lapInfoList.Count == 0)
        return (GenesisRewardParam) null;
      for (int index = lapInfoList.Count - 1; index >= 0; --index)
      {
        GenesisLapBossParam.LapInfo lapInfo = lapInfoList[index];
        if (lapInfo != null && round >= lapInfo.Round)
          return lapInfo.Reward;
      }
      return lapInfoList[0].Reward;
    }

    public static void Deserialize(
      ref List<GenesisLapBossParam> list,
      JSON_GenesisLapBossParam[] json)
    {
      if (json == null)
        return;
      if (list == null)
        list = new List<GenesisLapBossParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        GenesisLapBossParam genesisLapBossParam = new GenesisLapBossParam();
        genesisLapBossParam.Deserialize(json[index]);
        list.Add(genesisLapBossParam);
      }
    }

    public class LapInfo
    {
      private int mRound;
      private string mRewardId;
      private GenesisRewardParam mReward;

      public int Round => this.mRound;

      public string RewardId => this.mRewardId;

      public GenesisRewardParam Reward
      {
        get
        {
          if (this.mReward == null)
          {
            GameManager instance = MonoSingleton<GameManager>.Instance;
            if (Object.op_Implicit((Object) instance))
              this.mReward = instance.GetGenesisRewardParam(this.mRewardId);
          }
          return this.mReward;
        }
      }

      public void Deserialize(JSON_GenesisLapBossParam.LapInfo json)
      {
        if (json == null)
          return;
        this.mRound = json.round;
        this.mRewardId = json.reward_id;
      }
    }
  }
}
