// Decompiled with JetBrains decompiler
// Type: SRPG.GvGLeagueData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GvGLeagueData
  {
    public string Id { get; set; }

    public int Rate { get; set; }

    public int Rank { get; set; }

    public bool Deserialize(JSON_GvGLeagueData json)
    {
      if (json == null)
        return false;
      this.Id = json.id;
      this.Rate = json.rate;
      this.Rank = json.rank;
      return true;
    }
  }
}
