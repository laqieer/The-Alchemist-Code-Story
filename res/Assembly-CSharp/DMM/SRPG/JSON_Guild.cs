// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_Guild
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  [Serializable]
  public class JSON_Guild
  {
    public long id;
    public string created_uid;
    public string name;
    public string award_id;
    public string board;
    public int count;
    public int max_count;
    public int submaster_count;
    public JSON_GuildEntryCondition guild_subscription_condition;
    public JSON_GuildMember[] guild_member;
    public string[] have_awards;
    public JSON_GuildFacilityData[] facilities;
    public long created_at;
    public int gvg_join_status;
    public int gvg_rate;
  }
}
