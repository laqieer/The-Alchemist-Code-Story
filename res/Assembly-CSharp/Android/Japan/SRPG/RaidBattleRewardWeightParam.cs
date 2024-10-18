// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBattleRewardWeightParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class RaidBattleRewardWeightParam
  {
    private int mWeight;
    private string mRewardId;

    public int Weight
    {
      get
      {
        return this.mWeight;
      }
    }

    public string RewardId
    {
      get
      {
        return this.mRewardId;
      }
    }

    public bool Deserialize(JSON_RaidBattleRewardWeightParam json)
    {
      if (json == null)
        return false;
      this.mWeight = json.weight;
      this.mRewardId = json.reward_id;
      return true;
    }
  }
}
