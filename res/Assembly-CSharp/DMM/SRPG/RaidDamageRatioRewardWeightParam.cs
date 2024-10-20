﻿// Decompiled with JetBrains decompiler
// Type: SRPG.RaidDamageRatioRewardWeightParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class RaidDamageRatioRewardWeightParam
  {
    private int mDamageRatio;
    private string mRewardId;

    public int DamageRatio => this.mDamageRatio;

    public string RewardId => this.mRewardId;

    public bool Deserialize(JSON_RaidDamageRatioRewardRatioParam json)
    {
      if (json == null)
        return false;
      this.mDamageRatio = json.damage_ratio;
      this.mRewardId = json.reward_id;
      return true;
    }
  }
}