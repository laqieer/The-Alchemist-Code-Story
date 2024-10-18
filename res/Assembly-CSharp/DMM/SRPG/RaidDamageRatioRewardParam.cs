// Decompiled with JetBrains decompiler
// Type: SRPG.RaidDamageRatioRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class RaidDamageRatioRewardParam : RaidMasterParam<JSON_RaidDamageRatioRewardParam>
  {
    private int mId;
    private List<RaidDamageRatioRewardWeightParam> mRewards;

    public int Id => this.mId;

    public List<RaidDamageRatioRewardWeightParam> Rewards => this.mRewards;

    public override bool Deserialize(JSON_RaidDamageRatioRewardParam json)
    {
      if (json == null)
        return false;
      this.mId = json.id;
      this.mRewards = new List<RaidDamageRatioRewardWeightParam>();
      if (json.rewards != null)
      {
        for (int index = 0; index < json.rewards.Length; ++index)
        {
          RaidDamageRatioRewardWeightParam rewardWeightParam = new RaidDamageRatioRewardWeightParam();
          if (rewardWeightParam.Deserialize(json.rewards[index]))
            this.mRewards.Add(rewardWeightParam);
        }
      }
      return true;
    }
  }
}
