﻿// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace SRPG
{
  public class MultiTowerRewardParam
  {
    public string iname;
    public MultiTowerRewardItem[] mReward;

    public void Deserialize(JSON_MultiTowerRewardParam json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.iname = json.iname;
      if (json.rewards == null)
        return;
      this.mReward = new MultiTowerRewardItem[json.rewards.Length];
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        this.mReward[index] = new MultiTowerRewardItem();
        this.mReward[index].Deserialize(json.rewards[index]);
      }
    }

    public List<MultiTowerRewardItem> GetReward(int round)
    {
      List<MultiTowerRewardItem> multiTowerRewardItemList = new List<MultiTowerRewardItem>();
      if (this.mReward != null)
      {
        int num1 = Enumerable.Max(((IEnumerable<MultiTowerRewardItem>) this.mReward).Select<MultiTowerRewardItem, int>((Func<MultiTowerRewardItem, int>) (data => data.round_ed)));
        int num2 = round < num1 ? round : num1;
        for (int index = 0; index < this.mReward.Length; ++index)
        {
          if (this.mReward[index].round_st <= num2 && this.mReward[index].round_ed >= num2)
            multiTowerRewardItemList.Add(this.mReward[index]);
        }
      }
      return multiTowerRewardItemList;
    }
  }
}
