// Decompiled with JetBrains decompiler
// Type: SRPG.GvGLeagueViewGuild
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GvGLeagueViewGuild : ViewGuildData, IRankingContent
  {
    public GvGLeagueData league;

    public int Rank => this.league != null ? this.league.Rank : 0;

    public void Deserialize(JSON_GvGLeagueViewGuild json)
    {
      this.id = json.id;
      this.name = json.name;
      this.award_id = json.award_id;
      this.level = json.level;
      this.count = json.count;
      this.max_count = json.max_count;
      this.guild_master = json.guild_master;
      this.create_at = TimeManager.FromUnixTime(json.created_at);
      this.league = new GvGLeagueData();
      if (json.league == null)
        return;
      this.league.Id = json.league.id;
      this.league.Rank = json.league.rank;
      this.league.Rate = json.league.rate;
    }
  }
}
