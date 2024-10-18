// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRankingGuildData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class RaidRankingGuildData
  {
    private int mRank;
    private int mScore;
    private ViewGuildData mViewGuild;

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

    public ViewGuildData ViewGuild
    {
      get
      {
        return this.mViewGuild;
      }
    }

    public bool Deserialize(Json_RaidRankingGuildData json)
    {
      this.mRank = json.rank;
      this.mScore = json.score;
      if (json.guild != null)
      {
        this.mViewGuild = new ViewGuildData();
        this.mViewGuild.Deserialize(json.guild);
      }
      return true;
    }
  }
}
