// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidRewardDmgRatioParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GuildRaidRewardDmgRatioParam : GuildRaidMasterParam<JSON_GuildRaidRewardDmgRatioParam>
  {
    public string Id { get; private set; }

    public List<GuildRaidRewardDmgRatio> Reward { get; private set; }

    public override bool Deserialize(JSON_GuildRaidRewardDmgRatioParam json)
    {
      if (json == null)
        return false;
      this.Id = json.id;
      if (json.rewards == null || json.rewards.Length == 0)
        return false;
      this.Reward = new List<GuildRaidRewardDmgRatio>();
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        GuildRaidRewardDmgRatio raidRewardDmgRatio = new GuildRaidRewardDmgRatio();
        if (raidRewardDmgRatio.Deserialize(json.rewards[index]))
          this.Reward.Add(raidRewardDmgRatio);
      }
      return true;
    }
  }
}
