// Decompiled with JetBrains decompiler
// Type: SRPG.LoginInfoParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  [Serializable]
  public class LoginInfoParam
  {
    public string iname;
    public string path;
    public LoginInfoParam.SelectScene scene;
    public long begin_at;
    public long end_at;
    public LoginInfoParam.DispConditions conditions;
    public int conditions_value;
    public int priority;
    public int draw_count;
    public bool movie_compel;
    public string movie;
    public string url;

    public bool Deserialize(JSON_LoginInfoParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.path = json.path;
      this.scene = (LoginInfoParam.SelectScene) json.scene;
      DateTime result1 = DateTime.MinValue;
      DateTime result2 = DateTime.MaxValue;
      if (!string.IsNullOrEmpty(json.begin_at))
        DateTime.TryParse(json.begin_at, out result1);
      if (!string.IsNullOrEmpty(json.end_at))
        DateTime.TryParse(json.end_at, out result2);
      this.begin_at = TimeManager.FromDateTime(result1);
      this.end_at = TimeManager.FromDateTime(result2);
      this.conditions = (LoginInfoParam.DispConditions) json.conditions;
      this.conditions_value = json.conditions_value;
      this.priority = json.priority;
      this.draw_count = json.draw_count;
      this.movie_compel = json.movie_compel == 1;
      this.movie = json.movie;
      this.url = json.url;
      return true;
    }

    public bool IsDisplayable(DateTime server_time, int player_level, bool is_beginner)
    {
      long num = TimeManager.FromDateTime(server_time);
      if (this.begin_at > num || num >= this.end_at)
      {
        string key = this.iname + (object) this.begin_at;
        if (PlayerPrefsUtility.HasKey(key))
          PlayerPrefsUtility.DeleteKey(key);
        return false;
      }
      return this.CheckConditions(player_level, is_beginner);
    }

    private bool CheckConditions(int player_level, bool is_beginner)
    {
      switch (this.conditions)
      {
        case LoginInfoParam.DispConditions.PlayerLvMore:
          if (player_level >= this.conditions_value)
            return true;
          break;
        case LoginInfoParam.DispConditions.PlayerLvLess:
          if (player_level <= this.conditions_value)
            return true;
          break;
        case LoginInfoParam.DispConditions.Beginner:
          if (is_beginner)
            return true;
          break;
        case LoginInfoParam.DispConditions.NotBeginner:
          if (!is_beginner)
            return true;
          break;
        default:
          return true;
      }
      return false;
    }

    public enum SelectScene : byte
    {
      None,
      Gacha,
      LimitedShop,
      EventQuest,
      TowerQuest,
      BuyShop,
    }

    public enum DispConditions
    {
      None,
      PlayerLvMore,
      PlayerLvLess,
      Beginner,
      NotBeginner,
    }
  }
}
