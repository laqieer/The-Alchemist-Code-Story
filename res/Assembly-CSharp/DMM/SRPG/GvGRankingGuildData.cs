// Decompiled with JetBrains decompiler
// Type: SRPG.GvGRankingGuildData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GvGRankingGuildData : ViewGuildData
  {
    public int mRank;
    public int mPoint;

    public int Rank => this.mRank;

    public int Point => this.mPoint;

    public void Deserialize(JSON_GvGRankingData json)
    {
      if (json == null)
        return;
      this.Deserialize((JSON_ViewGuild) json);
      this.mRank = json.rank;
      this.mPoint = json.point;
    }
  }
}
