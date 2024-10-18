﻿// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRankRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class VersusRankRewardParam
  {
    private string mRewardId;
    private List<VersusRankReward> mRewardList;

    public string RewardId
    {
      get
      {
        return this.mRewardId;
      }
    }

    public List<VersusRankReward> RewardList
    {
      get
      {
        return this.mRewardList;
      }
    }

    public bool Deserialize(JSON_VersusRankRewardParam json)
    {
      if (json == null)
        return false;
      this.mRewardId = json.reward_id;
      this.mRewardList = new List<VersusRankReward>();
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        VersusRankReward versusRankReward = new VersusRankReward();
        if (versusRankReward.Deserialize(json.rewards[index]))
          this.mRewardList.Add(versusRankReward);
      }
      return true;
    }
  }
}
