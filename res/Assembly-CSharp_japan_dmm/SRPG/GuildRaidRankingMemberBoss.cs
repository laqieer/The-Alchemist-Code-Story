// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidRankingMemberBoss
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildRaidRankingMemberBoss
  {
    public int BossId { get; private set; }

    public int Rank { get; private set; }

    public bool Deserialize(JSON_GuildRaidRankingMemberBoss json)
    {
      this.BossId = json.boss_id;
      this.Rank = json.rank;
      return true;
    }
  }
}
