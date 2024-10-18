// Decompiled with JetBrains decompiler
// Type: SRPG.RaidGuildInfoData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class RaidGuildInfoData
  {
    private int mRank;
    private int mScore;

    public int Rank => this.mRank;

    public int Score => this.mScore;

    public bool Deserialize(Json_RaidGuildInfoData json)
    {
      this.mRank = json.rank;
      this.mScore = json.score;
      return true;
    }
  }
}
