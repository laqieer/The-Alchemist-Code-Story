// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRankRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class VersusRankRewardParam
  {
    private string mRewardId;
    private List<VersusRankReward> mRewardList;

    public string RewardId => this.mRewardId;

    public List<VersusRankReward> RewardList => this.mRewardList;

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
