// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBattleRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class RaidBattleRewardParam : RaidMasterParam<JSON_RaidBattleRewardParam>
  {
    private int mId;
    private List<RaidBattleRewardWeightParam> mRewards;

    public int Id
    {
      get
      {
        return this.mId;
      }
    }

    public List<RaidBattleRewardWeightParam> Rewards
    {
      get
      {
        return this.mRewards;
      }
    }

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
