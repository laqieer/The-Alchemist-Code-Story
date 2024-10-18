// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GuildRaidRewardParam : GuildRaidMasterParam<JSON_GuildRaidRewardParam>
  {
    public string Id { get; private set; }

    public List<GuildRaidReward> Rewards { get; private set; }

    public override bool Deserialize(JSON_GuildRaidRewardParam json)
    {
      if (json == null)
        return false;
      this.Id = json.id;
      this.Rewards = new List<GuildRaidReward>();
      if (json.rewards != null)
      {
        for (int index = 0; index < json.rewards.Length; ++index)
        {
          GuildRaidReward guildRaidReward = new GuildRaidReward();
          if (guildRaidReward.Deserialize(json.rewards[index]))
            this.Rewards.Add(guildRaidReward);
        }
      }
      return true;
    }
  }
}
