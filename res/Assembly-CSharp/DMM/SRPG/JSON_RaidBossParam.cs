// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_RaidBossParam
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
    public int damage_amount_reward_id;
    public string buff_id;
    public string mob_buff_id;
    public int is_boss;
  }
}
