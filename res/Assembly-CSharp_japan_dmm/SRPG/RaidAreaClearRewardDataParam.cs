// Decompiled with JetBrains decompiler
// Type: SRPG.RaidAreaClearRewardDataParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class RaidAreaClearRewardDataParam
  {
    private int mRound;
    private string mRewardId;

    public int Round => this.mRound;

    public string RewardId => this.mRewardId;

    public bool Deserialize(JSON_RaidAreaClearRewardDataParam json)
    {
      if (json == null)
        return false;
      this.mRound = json.round;
      this.mRewardId = json.reward_id;
      return true;
    }
  }
}
