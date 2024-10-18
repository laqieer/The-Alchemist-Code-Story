// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidRankingGuild
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildRaidRankingGuild : ViewGuildData
  {
    public int MemberCount { get; private set; }

    public int MemberMax { get; private set; }

    public bool Deserialize(JSON_GuildRaidRankingGuild json)
    {
      this.Deserialize((JSON_ViewGuild) json);
      this.MemberCount = json.member_count;
      this.MemberMax = json.member_max;
      return true;
    }
  }
}
