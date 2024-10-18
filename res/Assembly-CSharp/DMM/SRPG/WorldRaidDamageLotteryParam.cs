// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidDamageLotteryParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class WorldRaidDamageLotteryParam
  {
    private string mIname;
    private WorldRaidDamageLotteryParam.Reward[] mRewards;

    public string Iname => this.mIname;

    public List<WorldRaidDamageLotteryParam.Reward> RewardList
    {
      get
      {
        return this.mRewards != null ? new List<WorldRaidDamageLotteryParam.Reward>((IEnumerable<WorldRaidDamageLotteryParam.Reward>) this.mRewards) : new List<WorldRaidDamageLotteryParam.Reward>();
      }
    }

    public void Deserialize(JSON_WorldRaidDamageLotteryParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mRewards = (WorldRaidDamageLotteryParam.Reward[]) null;
      if (json.rewards == null || json.rewards.Length == 0)
        return;
      this.mRewards = new WorldRaidDamageLotteryParam.Reward[json.rewards.Length];
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        this.mRewards[index] = new WorldRaidDamageLotteryParam.Reward();
        this.mRewards[index].Deserialize(json.rewards[index]);
      }
    }

    public static void Deserialize(
      ref List<WorldRaidDamageLotteryParam> list,
      JSON_WorldRaidDamageLotteryParam[] json)
    {
      if (json == null)
        return;
      if (list == null)
        list = new List<WorldRaidDamageLotteryParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        WorldRaidDamageLotteryParam damageLotteryParam = new WorldRaidDamageLotteryParam();
        damageLotteryParam.Deserialize(json[index]);
        list.Add(damageLotteryParam);
      }
    }

    public static WorldRaidDamageLotteryParam GetParam(string iname)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      return !UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) || instance.WorldRaidDamageLotteryParamList == null ? (WorldRaidDamageLotteryParam) null : instance.WorldRaidDamageLotteryParamList.Find((Predicate<WorldRaidDamageLotteryParam>) (p => p.Iname == iname));
    }

    public class Reward
    {
      private ItemParam mItemParam;

      public int Weight { get; private set; }

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

      public void Deserialize(JSON_WorldRaidDamageLotteryParam.Reward json)
      {
        if (json == null)
          return;
        this.Weight = json.weight;
        this.ItemType = json.item_type;
        this.ItemIname = json.item_iname;
        this.ItemNum = json.item_num;
      }
    }
  }
}
