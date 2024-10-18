// Decompiled with JetBrains decompiler
// Type: SRPG.GvGRankingData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GvGRankingData
  {
    private GvGRankingGuildData mSelf;
    private List<GvGRankingGuildData> mGuilds = new List<GvGRankingGuildData>();
    private List<GvGScoreRankingData> mBeats = new List<GvGScoreRankingData>();
    private List<GvGScoreRankingData> mDefenses = new List<GvGScoreRankingData>();

    public GvGRankingGuildData Self => this.mSelf;

    public List<GvGRankingGuildData> Guilds => this.mGuilds;

    public List<GvGScoreRankingData> Beats => this.mBeats;

    public List<GvGScoreRankingData> Defenses => this.mDefenses;

    public void Deserialize(JSON_GvGRankingData[] guilds)
    {
      if (guilds == null)
        return;
      this.mGuilds.Clear();
      for (int index = 0; index < guilds.Length; ++index)
      {
        GvGRankingGuildData grankingGuildData = new GvGRankingGuildData();
        grankingGuildData.Deserialize(guilds[index]);
        this.mGuilds.Add(grankingGuildData);
      }
    }

    public void Deserialize(JSON_GvGScoreRanking[] data, bool IsBeat)
    {
      if (data == null)
        return;
      List<GvGScoreRankingData> gscoreRankingDataList1 = new List<GvGScoreRankingData>();
      List<GvGScoreRankingData> gscoreRankingDataList2 = !IsBeat ? this.mDefenses : this.mBeats;
      gscoreRankingDataList2.Clear();
      for (int index = 0; index < data.Length; ++index)
      {
        GvGScoreRankingData gscoreRankingData = new GvGScoreRankingData();
        gscoreRankingData.Deserialize(data[index]);
        gscoreRankingDataList2.Add(gscoreRankingData);
      }
    }
  }
}
