// Decompiled with JetBrains decompiler
// Type: SRPG.RaidDamageAmountRewardWeightParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class RaidDamageAmountRewardWeightParam
  {
    private int mDamageAmount;
    private string mRewardId;

    public int DamageAmount => this.mDamageAmount;

    public string RewardId => this.mRewardId;

    public bool Deserialize(JSON_RaidDamageAmountRewardAmountParam json)
    {
      if (json == null)
        return false;
      this.mDamageAmount = json.damage_Amount;
      this.mRewardId = json.reward_id;
      return true;
    }
  }
}
