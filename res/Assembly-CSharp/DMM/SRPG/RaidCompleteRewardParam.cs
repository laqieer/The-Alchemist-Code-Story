// Decompiled with JetBrains decompiler
// Type: SRPG.RaidCompleteRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class RaidCompleteRewardParam : RaidMasterParam<JSON_RaidCompleteRewardParam>
  {
    private int mId;
    private List<RaidCompleteRewardDataParam> mRewards;

    public int Id => this.mId;

    public List<RaidCompleteRewardDataParam> Rewards => this.mRewards;

    public override bool Deserialize(JSON_RaidCompleteRewardParam json)
    {
      if (json == null)
        return false;
      this.mId = json.id;
      this.mRewards = new List<RaidCompleteRewardDataParam>();
      if (json.rewards != null)
      {
        for (int index = 0; index < json.rewards.Length; ++index)
        {
          RaidCompleteRewardDataParam completeRewardDataParam = new RaidCompleteRewardDataParam();
          if (completeRewardDataParam.Deserialize(json.rewards[index]))
            this.mRewards.Add(completeRewardDataParam);
        }
      }
      return true;
    }
  }
}
