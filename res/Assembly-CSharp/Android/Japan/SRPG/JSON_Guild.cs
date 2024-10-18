// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_Guild
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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
  }
}
