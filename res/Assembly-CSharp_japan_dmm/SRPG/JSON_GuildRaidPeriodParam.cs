// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GuildRaidPeriodParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class JSON_GuildRaidPeriodParam : JSON_GuildRaidMasterParam
  {
    public int id;
    public int bp;
    public int round_max;
    public int round_buff_max;
    public int heal_ap;
    public int unit_lv_min;
    public string begin_at;
    public string main_begin_at;
    public string end_at;
    public string reward_end_at;
    public string reward_ranking_begin_at;
    public string reward_ranking_end_at;
    public string reward_ranking_id;
    public JSON_GuildRaidPeriodTime[] schedule;
    public int default_bp;
    public int bp_type;
  }
}
