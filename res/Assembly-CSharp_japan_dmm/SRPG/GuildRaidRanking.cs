// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildRaidRanking
  {
    public int Rank { get; private set; }

    public long Score { get; private set; }

    public GuildRaidRankingGuild Guild { get; private set; }

    public bool Deserialize(JSON_GuildRaidRanking json)
    {
      this.Rank = json.rank;
      this.Score = json.score;
      if (json.guild == null)
        return false;
      this.Guild = new GuildRaidRankingGuild();
      this.Guild.Deserialize(json.guild);
      return true;
    }
  }
}
