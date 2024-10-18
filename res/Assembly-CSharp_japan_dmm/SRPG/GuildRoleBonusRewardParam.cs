// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRoleBonusRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GuildRoleBonusRewardParam
  {
    private string mID;
    private GuildRoleBonusReward[] mRewards;

    public string id => this.mID;

    public GuildRoleBonusReward[] rewards => this.mRewards;

    public static bool Deserialize(
      ref List<GuildRoleBonusRewardParam> param,
      JSON_GuildRoleBonusRewardParam[] json)
    {
      if (json == null)
        return false;
      param = new List<GuildRoleBonusRewardParam>(json.Length);
      for (int index = 0; index < json.Length; ++index)
      {
        GuildRoleBonusRewardParam bonusRewardParam = new GuildRoleBonusRewardParam();
        bonusRewardParam.Deserialize(json[index]);
        param.Add(bonusRewardParam);
      }
      return true;
    }

    public void Deserialize(JSON_GuildRoleBonusRewardParam json)
    {
      if (json == null || json.rewards == null)
        return;
      this.mID = json.id;
      this.mRewards = new GuildRoleBonusReward[json.rewards.Length];
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        GuildRoleBonusReward guildRoleBonusReward = new GuildRoleBonusReward();
        guildRoleBonusReward.Deserialize(json.rewards[index]);
        this.mRewards[index] = guildRoleBonusReward;
      }
    }
  }
}
