// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class ArenaRewardParam
  {
    private List<ArenaRewardParam.RewardItem> mItems = new List<ArenaRewardParam.RewardItem>();
    private DateTime begin_at = DateTime.MinValue;
    private DateTime end_at = DateTime.MaxValue;
    private int mRank;
    private int mCoin;
    private int mGold;
    private int mArenaCoin;
    private int mFrom;

    public int Rank
    {
      get
      {
        return this.mRank;
      }
    }

    public int FromRank
    {
      get
      {
        return this.mFrom;
      }
    }

    public int Coin
    {
      get
      {
        return this.mCoin;
      }
    }

    public int Gold
    {
      get
      {
        return this.mGold;
      }
    }

    public int ArenaCoin
    {
      get
      {
        return this.mArenaCoin;
      }
    }

    public DateTime BeginAt
    {
      get
      {
        return this.begin_at;
      }
    }

    public DateTime EndAt
    {
      get
      {
        return this.end_at;
      }
    }

    public List<ArenaRewardParam.RewardItem> Items
    {
      get
      {
        return this.mItems;
      }
    }

    public static bool Deserialize(ref List<ArenaRewardParam> param_list, JSON_ArenaResult[] json_arena_results)
    {
      param_list = new List<ArenaRewardParam>();
      if (json_arena_results == null || json_arena_results.Length <= 0)
        return false;
      foreach (JSON_ArenaResult jsonArenaResult in json_arena_results)
      {
        ArenaRewardParam arenaRewardParam = new ArenaRewardParam();
        arenaRewardParam.mRank = jsonArenaResult.rank;
        arenaRewardParam.mGold = jsonArenaResult.gold;
        arenaRewardParam.mCoin = jsonArenaResult.coin;
        arenaRewardParam.mArenaCoin = jsonArenaResult.ac;
        if (!string.IsNullOrEmpty(jsonArenaResult.begin_at))
          DateTime.TryParse(jsonArenaResult.begin_at, out arenaRewardParam.begin_at);
        if (!string.IsNullOrEmpty(jsonArenaResult.end_at))
          DateTime.TryParse(jsonArenaResult.end_at, out arenaRewardParam.end_at);
        if (!string.IsNullOrEmpty(jsonArenaResult.item1) && jsonArenaResult.num1 > 0)
          arenaRewardParam.mItems.Add(new ArenaRewardParam.RewardItem(jsonArenaResult.item1, jsonArenaResult.num1));
        if (!string.IsNullOrEmpty(jsonArenaResult.item2) && jsonArenaResult.num2 > 0)
          arenaRewardParam.mItems.Add(new ArenaRewardParam.RewardItem(jsonArenaResult.item2, jsonArenaResult.num2));
        if (!string.IsNullOrEmpty(jsonArenaResult.item3) && jsonArenaResult.num3 > 0)
          arenaRewardParam.mItems.Add(new ArenaRewardParam.RewardItem(jsonArenaResult.item3, jsonArenaResult.num3));
        if (!string.IsNullOrEmpty(jsonArenaResult.item4) && jsonArenaResult.num4 > 0)
          arenaRewardParam.mItems.Add(new ArenaRewardParam.RewardItem(jsonArenaResult.item4, jsonArenaResult.num4));
        if (!string.IsNullOrEmpty(jsonArenaResult.item5) && jsonArenaResult.num5 > 0)
          arenaRewardParam.mItems.Add(new ArenaRewardParam.RewardItem(jsonArenaResult.item5, jsonArenaResult.num5));
        param_list.Add(arenaRewardParam);
      }
      return true;
    }

    public static List<ArenaRewardParam> GetSortedRewardParams(List<ArenaRewardParam> reward_params)
    {
      List<ArenaRewardParam> arenaRewardParamList = new List<ArenaRewardParam>();
      DateTime serverTime = TimeManager.ServerTime;
      foreach (ArenaRewardParam rewardParam in reward_params)
      {
        if (rewardParam.BeginAt <= serverTime && rewardParam.EndAt >= serverTime)
          arenaRewardParamList.Add(rewardParam);
      }
      arenaRewardParamList.Sort((Comparison<ArenaRewardParam>) ((a, b) => a.Rank - b.Rank));
      for (int index1 = 0; index1 < arenaRewardParamList.Count; ++index1)
      {
        int index2 = index1 - 1;
        arenaRewardParamList[index1].mFrom = index2 >= 0 ? arenaRewardParamList[index2].mRank + 1 : arenaRewardParamList[index1].mRank;
      }
      return arenaRewardParamList;
    }

    public class RewardItem
    {
      public string iname = string.Empty;
      public int num;

      public RewardItem(string _iname, int _num)
      {
        this.iname = _iname;
        this.num = _num;
      }
    }
  }
}
