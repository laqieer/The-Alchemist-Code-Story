// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class RaidRewardParam : RaidMasterParam<JSON_RaidRewardParam>
  {
    private string mId;
    private List<RaidReward> mRewards;

    public string Id
    {
      get
      {
        return this.mId;
      }
    }

    public List<RaidReward> Rewards
    {
      get
      {
        return this.mRewards;
      }
    }

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
