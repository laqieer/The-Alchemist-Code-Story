// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidRewardDmgRankingParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GuildRaidRewardDmgRankingParam : 
    GuildRaidMasterParam<JSON_GuildRaidRewardDmgRankingParam>
  {
    public string Id { get; private set; }

    public List<GuildRaidRewardDmgRankingRankParam> Ranking { get; private set; }

    public override bool Deserialize(JSON_GuildRaidRewardDmgRankingParam json)
    {
      if (json == null)
        return false;
      this.Id = json.id;
      if (json.ranking == null)
        return false;
      this.Ranking = new List<GuildRaidRewardDmgRankingRankParam>();
      for (int index = 0; index < json.ranking.Length; ++index)
      {
        GuildRaidRewardDmgRankingRankParam rankingRankParam = new GuildRaidRewardDmgRankingRankParam();
        if (rankingRankParam.Deserialize(json.ranking[index]))
          this.Ranking.Add(rankingRankParam);
      }
      return true;
    }
  }
}
