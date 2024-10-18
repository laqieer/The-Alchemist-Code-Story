// Decompiled with JetBrains decompiler
// Type: SRPG.RaidAreaClearRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class RaidAreaClearRewardParam : RaidMasterParam<JSON_RaidAreaClearRewardParam>
  {
    private int mId;
    private List<RaidAreaClearRewardDataParam> mRewards;

    public int Id => this.mId;

    public List<RaidAreaClearRewardDataParam> Rewards => this.mRewards;

    public override bool Deserialize(JSON_RaidAreaClearRewardParam json)
    {
      if (json == null)
        return false;
      this.mId = json.id;
      this.mRewards = new List<RaidAreaClearRewardDataParam>();
      if (json.rewards != null)
      {
        for (int index = 0; index < json.rewards.Length; ++index)
        {
          RaidAreaClearRewardDataParam clearRewardDataParam = new RaidAreaClearRewardDataParam();
          if (clearRewardDataParam.Deserialize(json.rewards[index]))
            this.mRewards.Add(clearRewardDataParam);
        }
      }
      return true;
    }
  }
}
