// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GvGPeriodParam
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
  public class JSON_GvGPeriodParam : JSON_GvGMasterParam
  {
    public int id;
    public string prepare_start_at;
    public string begin_at;
    public string end_at;
    public string exit_at;
    public string declaration_start_time;
    public string declaration_end_time;
    public int declaration_cool_minites;
    public string battle_start_time;
    public string battle_end_time;
    public string battle_cool_start_time;
    public string battle_cool_end_time;
    public int declared_cool_minutes;
    public int battle_cool_seconds;
    public int declare_num;
    public int map_idx;
    public int matching_count_min;
    public int matching_count_max;
    public int first_occupy_node_num;
    public int defense_unit_min;
    public string url;
    public string[] rule_cycle;
    public string url_title;
  }
}
