// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidRankingRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class WorldRaidRankingRewardParam
  {
    private string mIname;
    private WorldRaidRankingRewardParam.Reward[] mRewards;

    public string Iname => this.mIname;

    public List<WorldRaidRankingRewardParam.Reward> RewardList
    {
      get
      {
        return this.mRewards != null ? new List<WorldRaidRankingRewardParam.Reward>((IEnumerable<WorldRaidRankingRewardParam.Reward>) this.mRewards) : new List<WorldRaidRankingRewardParam.Reward>();
      }
    }

    public void Deserialize(JSON_WorldRaidRankingRewardParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mRewards = (WorldRaidRankingRewardParam.Reward[]) null;
      if (json.rewards == null || json.rewards.Length == 0)
        return;
      this.mRewards = new WorldRaidRankingRewardParam.Reward[json.rewards.Length];
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        this.mRewards[index] = new WorldRaidRankingRewardParam.Reward();
        this.mRewards[index].Deserialize(json.rewards[index]);
      }
    }

    public static void Deserialize(
      ref List<WorldRaidRankingRewardParam> list,
      JSON_WorldRaidRankingRewardParam[] json)
    {
      if (json == null)
        return;
      if (list == null)
        list = new List<WorldRaidRankingRewardParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        WorldRaidRankingRewardParam rankingRewardParam = new WorldRaidRankingRewardParam();
        rankingRewardParam.Deserialize(json[index]);
        list.Add(rankingRewardParam);
      }
    }

    public static WorldRaidRankingRewardParam GetParam(string iname)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      return !UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) || instance.WorldRaidRankingRewardParamList == null ? (WorldRaidRankingRewardParam) null : instance.WorldRaidRankingRewardParamList.Find((Predicate<WorldRaidRankingRewardParam>) (p => p.Iname == iname));
    }

    public class Reward
    {
      private WorldRaidRewardParam mRewardParam;

      public int RankBegin { get; private set; }

      public int RankEnd { get; private set; }

      public string RewardId { get; private set; }

      public WorldRaidRewardParam RewardParam
      {
        get
        {
          if (this.mRewardParam == null && !string.IsNullOrEmpty(this.RewardId))
            this.mRewardParam = WorldRaidRewardParam.GetParam(this.RewardId);
          return this.mRewardParam;
        }
      }

      public void Deserialize(JSON_WorldRaidRankingRewardParam.Reward json)
      {
        if (json == null)
          return;
        this.RankBegin = json.rank_begin;
        this.RankEnd = json.rank_end;
        this.RewardId = json.reward_id;
      }
    }
  }
}
