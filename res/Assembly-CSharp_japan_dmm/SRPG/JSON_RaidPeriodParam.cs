// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_RaidPeriodParam
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
  public class JSON_RaidPeriodParam : JSON_RaidMasterParam
  {
    public int id;
    public int max_bp;
    public string add_bp_time;
    public int bp_by_coin;
    public int rescue_member_max;
    public string rescure_send_interval;
    public int complete_reward_id;
    public int round_buff_max;
    public string begin_at;
    public string end_at;
    public string reward_begin_at;
    public string reward_end_at;
  }
}
