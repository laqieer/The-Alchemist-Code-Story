// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_RaidBossParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_RaidBossParam : JSON_RaidMasterParam
  {
    public int id;
    public int stamp_index;
    public int period_id;
    public int weight;
    public string name;
    public int hp;
    public string unit_iname;
    public string quest_iname;
    public string time_limit;
    public int battle_reward_id;
    public int beat_reward_id;
    public int damage_ratio_reward_id;
    public string buff_id;
    public string mob_buff_id;
    public int is_boss;
  }
}
