// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBattleRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class RaidBattleRewardParam : RaidMasterParam<JSON_RaidBattleRewardParam>
  {
    private int mId;
    private List<RaidBattleRewardWeightParam> mRewards;

    public int Id => this.mId;

    public List<RaidBattleRewardWeightParam> Rewards => this.mRewards;

    public override bool Deserialize(JSON_RaidBattleRewardParam json)
    {
      if (json == null)
        return false;
      this.mId = json.id;
      this.mRewards = new List<RaidBattleRewardWeightParam>();
      if (json.rewards != null)
      {
        for (int index = 0; index < json.rewards.Length; ++index)
        {
          RaidBattleRewardWeightParam rewardWeightParam = new RaidBattleRewardWeightParam();
          if (rewardWeightParam.Deserialize(json.rewards[index]))
            this.mRewards.Add(rewardWeightParam);
        }
      }
      return true;
    }
  }
}
