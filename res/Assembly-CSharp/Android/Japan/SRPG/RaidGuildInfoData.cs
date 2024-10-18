// Decompiled with JetBrains decompiler
// Type: SRPG.RaidGuildInfoData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class RaidGuildInfoData
  {
    private int mRank;
    private int mScore;

    public int Rank
    {
      get
      {
        return this.mRank;
      }
    }

    public int Score
    {
      get
      {
        return this.mScore;
      }
    }

    public bool Deserialize(Json_RaidGuildInfoData json)
    {
      this.mRank = json.rank;
      this.mScore = json.score;
      return true;
    }
  }
}
