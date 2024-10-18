// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidRewardRankingParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GuildRaidRewardRankingParam : GuildRaidMasterParam<JSON_GuildRaidRewardRankingParam>
  {
    public string Id { get; private set; }

    public List<GuildRaidRewardRankingDataParam> Ranking { get; private set; }

    public override bool Deserialize(JSON_GuildRaidRewardRankingParam json)
    {
      if (json == null)
        return false;
      this.Id = json.id;
      if (json.ranking == null)
        return false;
      this.Ranking = new List<GuildRaidRewardRankingDataParam>();
      for (int index = 0; index < json.ranking.Length; ++index)
      {
        GuildRaidRewardRankingDataParam rankingDataParam = new GuildRaidRewardRankingDataParam();
        if (rankingDataParam.Deserialize(json.ranking[index]))
          this.Ranking.Add(rankingDataParam);
      }
      return true;
    }
  }
}
