// Decompiled with JetBrains decompiler
// Type: SRPG.AdvanceStarRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class AdvanceStarRewardParam
  {
    public int NeedStarNum;
    public string RewardId;
    public bool IsReward;

    public void Deserialize(JSON_AdvanceStarRewardParam json)
    {
      if (json == null)
        return;
      this.NeedStarNum = json.need;
      this.RewardId = json.reward_id;
    }
  }
}
