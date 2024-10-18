// Decompiled with JetBrains decompiler
// Type: SRPG.RaidDamageAmountRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class RaidDamageAmountRewardParam : RaidMasterParam<JSON_RaidDamageAmountRewardParam>
  {
    private int mId;
    private List<RaidDamageAmountRewardWeightParam> mRewards;

    public int Id => this.mId;

    public List<RaidDamageAmountRewardWeightParam> Rewards => this.mRewards;

    public override bool Deserialize(JSON_RaidDamageAmountRewardParam json)
    {
      if (json == null)
        return false;
      this.mId = json.id;
      this.mRewards = new List<RaidDamageAmountRewardWeightParam>();
      if (json.rewards != null)
      {
        for (int index = 0; index < json.rewards.Length; ++index)
        {
          RaidDamageAmountRewardWeightParam rewardWeightParam = new RaidDamageAmountRewardWeightParam();
          if (rewardWeightParam.Deserialize(json.rewards[index]))
            this.mRewards.Add(rewardWeightParam);
        }
      }
      return true;
    }
  }
}
