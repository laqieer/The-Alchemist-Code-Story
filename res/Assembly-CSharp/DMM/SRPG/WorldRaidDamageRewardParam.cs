// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidDamageRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class WorldRaidDamageRewardParam
  {
    private string mIname;
    private WorldRaidDamageRewardParam.Reward[] mRewards;

    public string Iname => this.mIname;

    public List<WorldRaidDamageRewardParam.Reward> RewardList
    {
      get
      {
        return this.mRewards != null ? new List<WorldRaidDamageRewardParam.Reward>((IEnumerable<WorldRaidDamageRewardParam.Reward>) this.mRewards) : new List<WorldRaidDamageRewardParam.Reward>();
      }
    }

    public void Deserialize(JSON_WorldRaidDamageRewardParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mRewards = (WorldRaidDamageRewardParam.Reward[]) null;
      if (json.rewards == null || json.rewards.Length == 0)
        return;
      this.mRewards = new WorldRaidDamageRewardParam.Reward[json.rewards.Length];
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        this.mRewards[index] = new WorldRaidDamageRewardParam.Reward();
        this.mRewards[index].Deserialize(json.rewards[index]);
      }
    }

    public static void Deserialize(
      ref List<WorldRaidDamageRewardParam> list,
      JSON_WorldRaidDamageRewardParam[] json)
    {
      if (json == null)
        return;
      if (list == null)
        list = new List<WorldRaidDamageRewardParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        WorldRaidDamageRewardParam damageRewardParam = new WorldRaidDamageRewardParam();
        damageRewardParam.Deserialize(json[index]);
        list.Add(damageRewardParam);
      }
    }

    public static WorldRaidDamageRewardParam GetParam(string iname)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      return !UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) || instance.WorldRaidDamageRewardParamList == null ? (WorldRaidDamageRewardParam) null : instance.WorldRaidDamageRewardParamList.Find((Predicate<WorldRaidDamageRewardParam>) (p => p.Iname == iname));
    }

    public class Reward
    {
      private WorldRaidDamageLotteryParam mLotteryParam;

      public int DmgMin { get; private set; }

      public int DmgMax { get; private set; }

      public string LotteryId { get; private set; }

      public int EffIdx { get; private set; }

      public WorldRaidDamageLotteryParam LotteryParam
      {
        get
        {
          if (this.mLotteryParam == null && !string.IsNullOrEmpty(this.LotteryId))
            this.mLotteryParam = WorldRaidDamageLotteryParam.GetParam(this.LotteryId);
          return this.mLotteryParam;
        }
      }

      public void Deserialize(JSON_WorldRaidDamageRewardParam.Reward json)
      {
        if (json == null)
          return;
        this.DmgMin = json.dmg_min;
        this.DmgMax = json.dmg_max;
        this.LotteryId = json.lottery_id;
        this.EffIdx = json.eff_idx;
      }
    }
  }
}
