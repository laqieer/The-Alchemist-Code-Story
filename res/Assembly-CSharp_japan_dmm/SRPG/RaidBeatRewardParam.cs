// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBeatRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class RaidBeatRewardParam : RaidMasterParam<JSON_RaidBeatRewardParam>
  {
    private int mId;
    private List<RaidBeatRewardDataParam> mRewards;

    public int Id => this.mId;

    public List<RaidBeatRewardDataParam> Rewards => this.mRewards;

    public override bool Deserialize(JSON_RaidBeatRewardParam json)
    {
      if (json == null)
        return false;
      this.mId = json.id;
      this.mRewards = new List<RaidBeatRewardDataParam>();
      if (json.rewards != null)
      {
        for (int index = 0; index < json.rewards.Length; ++index)
        {
          RaidBeatRewardDataParam beatRewardDataParam = new RaidBeatRewardDataParam();
          if (beatRewardDataParam.Deserialize(json.rewards[index]))
            this.mRewards.Add(beatRewardDataParam);
        }
      }
      return true;
    }
  }
}
