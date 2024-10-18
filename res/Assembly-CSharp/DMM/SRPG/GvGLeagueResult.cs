// Decompiled with JetBrains decompiler
// Type: SRPG.GvGLeagueResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GvGLeagueResult
  {
    public GvGLeagueData From { get; private set; }

    public GvGLeagueData To { get; private set; }

    public bool Deserialize(JSON_GvGLeagueResult json)
    {
      if (json == null || json.from == null || json.to == null)
        return false;
      this.From = new GvGLeagueData();
      this.From.Deserialize(json.from);
      this.To = new GvGLeagueData();
      this.To.Deserialize(json.to);
      return true;
    }
  }
}
