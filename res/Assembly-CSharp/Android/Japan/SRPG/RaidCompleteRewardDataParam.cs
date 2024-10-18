// Decompiled with JetBrains decompiler
// Type: SRPG.RaidCompleteRewardDataParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class RaidCompleteRewardDataParam
  {
    private int mRound;
    private string mRewardId;

    public int Round
    {
      get
      {
        return this.mRound;
      }
    }

    public string RewardId
    {
      get
      {
        return this.mRewardId;
      }
    }

    public bool Deserialize(JSON_RaidCompleteRewardDataParam json)
    {
      if (json == null)
        return false;
      this.mRound = json.round;
      this.mRewardId = json.reward_id;
      return true;
    }
  }
}
