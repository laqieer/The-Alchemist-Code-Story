// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_RaidPeriodParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
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
  }
}
