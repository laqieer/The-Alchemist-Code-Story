// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class RaidRewardParam : RaidMasterParam<JSON_RaidRewardParam>
  {
    private string mId;
    private List<RaidReward> mRewards;

    public string Id => this.mId;

    public List<RaidReward> Rewards => this.mRewards;

    public override bool Deserialize(JSON_RaidRewardParam json)
    {
      if (json == null)
        return false;
      this.mId = json.id;
      this.mRewards = new List<RaidReward>();
      if (json.rewards != null)
      {
        for (int index = 0; index < json.rewards.Length; ++index)
        {
          RaidReward raidReward = new RaidReward();
          if (raidReward.Deserialize(json.rewards[index]))
            this.mRewards.Add(raidReward);
        }
      }
      return true;
    }
  }
}
