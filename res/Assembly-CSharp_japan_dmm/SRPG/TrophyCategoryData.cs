// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyCategoryData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class TrophyCategoryData
  {
    private TrophyCategoryParam category_param;
    private List<TrophyState> trophies;
    private List<TrophyState> tmp1_trophies;
    private List<TrophyState> tmp2_trophies;
    private bool is_in_completed_data;

    public TrophyCategoryData(TrophyCategoryParam _tcp)
    {
      this.is_in_completed_data = false;
      this.category_param = _tcp;
      this.trophies = new List<TrophyState>();
      this.tmp1_trophies = new List<TrophyState>();
      this.tmp2_trophies = new List<TrophyState>();
    }

    public TrophyCategoryParam Param => this.category_param;

    public List<TrophyState> Trophies => this.trophies;

    public bool IsInCompletedData => this.is_in_completed_data;

    public void AddTrophy(TrophyState _trophy)
    {
      this.trophies.Add(_trophy);
      if (_trophy.IsCompleted)
      {
        this.tmp1_trophies.Add(_trophy);
        this.is_in_completed_data = true;
      }
      else
        this.tmp2_trophies.Add(_trophy);
    }

    public void RemoveTrophy(TrophyState _trophy)
    {
      if (this.trophies.Contains(_trophy))
        this.trophies.Remove(_trophy);
      if (this.tmp1_trophies.Contains(_trophy))
        this.tmp1_trophies.Remove(_trophy);
      if (!this.tmp2_trophies.Contains(_trophy))
        return;
      this.tmp2_trophies.Remove(_trophy);
    }

    public void Apply()
    {
      this.trophies.Clear();
      this.trophies.AddRange((IEnumerable<TrophyState>) this.tmp1_trophies);
      this.trophies.AddRange((IEnumerable<TrophyState>) this.tmp2_trophies);
      this.tmp1_trophies.Clear();
      this.tmp2_trophies.Clear();
      this.tmp1_trophies = this.tmp2_trophies = (List<TrophyState>) null;
    }
  }
}
