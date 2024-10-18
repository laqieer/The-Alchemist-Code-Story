// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace SRPG
{
  public class MultiTowerRewardParam
  {
    public string iname;
    public MultiTowerRewardItem[] mReward;

    public void Deserialize(JSON_MultiTowerRewardParam json)
    {
      this.iname = json != null ? json.iname : throw new InvalidJSONException();
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
      List<MultiTowerRewardItem> reward = new List<MultiTowerRewardItem>();
      if (this.mReward != null)
      {
        int num1 = ((IEnumerable<MultiTowerRewardItem>) this.mReward).Select<MultiTowerRewardItem, int>((Func<MultiTowerRewardItem, int>) (data => data.round_ed)).Max();
        int num2 = round < num1 ? round : num1;
        for (int index = 0; index < this.mReward.Length; ++index)
        {
          if (this.mReward[index].round_st <= num2 && this.mReward[index].round_ed >= num2)
            reward.Add(this.mReward[index]);
        }
      }
      return reward;
    }
  }
}
