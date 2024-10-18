// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaPlayerHistory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ArenaPlayerHistory
  {
    public string type;
    public string result;
    public JSON_ArenaRankInfo ranking;
    public DateTime battle_at = DateTime.MinValue;
    public ArenaPlayer my;
    public ArenaPlayer enemy;

    public bool IsAttack() => this.type == "challenge";

    public bool IsWin() => this.result == "win";

    public bool IsNew() => (DateTime.Now - this.battle_at).TotalHours <= 2.0;

    public void Deserialize(Json_ArenaPlayerHistory json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.ranking = new JSON_ArenaRankInfo();
      this.type = json.type;
      this.result = json.result;
      this.battle_at = GameUtility.UnixtimeToLocalTime(json.at);
      if (json.ranking != null)
      {
        this.ranking.rank = json.ranking.rank;
        this.ranking.up = json.ranking.up;
        this.ranking.is_best = json.ranking.is_best;
      }
      if (json.my != null)
      {
        try
        {
          this.my = new ArenaPlayer();
          this.my.Deserialize(json.my);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
        }
      }
      if (json.enemy == null)
        return;
      try
      {
        this.enemy = new ArenaPlayer();
        this.enemy.Deserialize(json.enemy);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
      }
    }
  }
}
