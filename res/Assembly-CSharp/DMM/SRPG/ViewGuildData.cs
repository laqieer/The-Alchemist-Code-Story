// Decompiled with JetBrains decompiler
// Type: SRPG.ViewGuildData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ViewGuildData
  {
    public int id;
    public string name;
    public string award_id;
    public int level;
    public int count;
    public int max_count;
    public string guild_master;
    public DateTime create_at;

    public void Deserialize(JSON_ViewGuild json)
    {
      this.id = json.id;
      this.name = json.name;
      this.award_id = json.award_id;
      this.level = json.level;
      this.count = json.count;
      this.max_count = json.max_count;
      this.guild_master = json.guild_master;
      this.create_at = TimeManager.FromUnixTime(json.created_at);
    }
  }
}
