// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRankingData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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

    public string UID => this.mUID;

    public string Name => this.mName;

    public int Lv => this.mLv;

    public int Rank => this.mRank;

    public int Score => this.mScore;

    public UnitData Unit => this.mUnit;

    public string SelectedAward => this.mSelectedAward;

    public ViewGuildData ViewGuild => this.mViewGuild;

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
