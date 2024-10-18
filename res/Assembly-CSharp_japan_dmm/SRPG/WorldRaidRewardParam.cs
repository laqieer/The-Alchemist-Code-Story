// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class WorldRaidRewardParam
  {
    private string mIname;
    private WorldRaidRewardParam.Reward[] mRewards;

    public string Iname => this.mIname;

    public List<WorldRaidRewardParam.Reward> RewardList
    {
      get
      {
        return this.mRewards != null ? new List<WorldRaidRewardParam.Reward>((IEnumerable<WorldRaidRewardParam.Reward>) this.mRewards) : new List<WorldRaidRewardParam.Reward>();
      }
    }

    public void Deserialize(JSON_WorldRaidRewardParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mRewards = (WorldRaidRewardParam.Reward[]) null;
      if (json.rewards == null || json.rewards.Length == 0)
        return;
      this.mRewards = new WorldRaidRewardParam.Reward[json.rewards.Length];
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        this.mRewards[index] = new WorldRaidRewardParam.Reward();
        this.mRewards[index].Deserialize(json.rewards[index]);
      }
    }

    public static void Deserialize(
      ref List<WorldRaidRewardParam> list,
      JSON_WorldRaidRewardParam[] json)
    {
      if (json == null)
        return;
      if (list == null)
        list = new List<WorldRaidRewardParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        WorldRaidRewardParam worldRaidRewardParam = new WorldRaidRewardParam();
        worldRaidRewardParam.Deserialize(json[index]);
        list.Add(worldRaidRewardParam);
      }
    }

    public static WorldRaidRewardParam GetParam(string iname)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      return !UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) || instance.WorldRaidRewardParamList == null ? (WorldRaidRewardParam) null : instance.WorldRaidRewardParamList.Find((Predicate<WorldRaidRewardParam>) (p => p.Iname == iname));
    }

    public class Reward
    {
      private ItemParam mItemParam;

      public int ItemType { get; private set; }

      public string ItemIname { get; private set; }

      public int ItemNum { get; private set; }

      public ItemParam ItemParam
      {
        get
        {
          if (this.mItemParam == null && !string.IsNullOrEmpty(this.ItemIname))
            this.mItemParam = MonoSingleton<GameManager>.Instance.GetItemParam(this.ItemIname);
          return this.mItemParam;
        }
      }

      public void Deserialize(JSON_WorldRaidRewardParam.Reward json)
      {
        if (json == null)
          return;
        this.ItemType = json.item_type;
        this.ItemIname = json.item_iname;
        this.ItemNum = json.item_num;
      }
    }
  }
}
