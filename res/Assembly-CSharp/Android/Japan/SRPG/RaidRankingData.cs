// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRankingData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class RaidRankingData
  {
    private string mUID;
    private string mName;
    private int mLv;
    private int mRank;
    private int mScore;
    private UnitData mUnit;
    private string mSelectedAward;
    private ViewGuildData mViewGuild;

    public string UID
    {
      get
      {
        return this.mUID;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
    }

    public int Lv
    {
      get
      {
        return this.mLv;
      }
    }

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

    public UnitData Unit
    {
      get
      {
        return this.mUnit;
      }
    }

    public string SelectedAward
    {
      get
      {
        return this.mSelectedAward;
      }
    }

    public ViewGuildData ViewGuild
    {
      get
      {
        return this.mViewGuild;
      }
    }

    public bool Deserialize(Json_RaidRankingData json)
    {
      this.mUID = json.uid;
      this.mName = json.name;
      this.mLv = json.lv;
      this.mRank = json.rank;
      this.mScore = json.score;
      this.mSelectedAward = json.selected_award;
      if (json.unit != null)
      {
        this.mUnit = new UnitData();
        this.mUnit.Deserialize(json.unit);
      }
      if (json.guild != null)
      {
        this.mViewGuild = new ViewGuildData();
        this.mViewGuild.Deserialize(json.guild);
      }
      return true;
    }
  }
}
