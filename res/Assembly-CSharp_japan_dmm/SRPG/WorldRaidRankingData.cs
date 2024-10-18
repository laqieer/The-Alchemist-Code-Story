// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidRankingData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class WorldRaidRankingData
  {
    public int Rank { get; private set; }

    public long Score { get; private set; }

    public string Name { get; private set; }

    public int Lv { get; private set; }

    public UnitData Unit { get; private set; }

    public int GuildId { get; private set; }

    public string GuildName { get; private set; }

    public string AwardId { get; private set; }

    public bool Deserialize(JSON_WorldRaidRankingData json)
    {
      if (json == null)
        return false;
      this.Rank = json.rank;
      this.Score = json.score;
      this.Name = json.name;
      this.Lv = json.lv;
      if (json.unit != null && !string.IsNullOrEmpty(json.unit.iname))
      {
        this.Unit = UnitData.CreateUnitDataForDisplay(json.unit.iname);
        if (!string.IsNullOrEmpty(json.unit.cur_skin))
          this.Unit.SetJobSkinAll(json.unit.cur_skin, false);
      }
      this.GuildId = 0;
      this.GuildName = string.Empty;
      if (json.guild != null)
      {
        this.GuildId = json.guild.id;
        this.GuildName = json.guild.name;
      }
      this.AwardId = json.selected_award;
      return true;
    }
  }
}
