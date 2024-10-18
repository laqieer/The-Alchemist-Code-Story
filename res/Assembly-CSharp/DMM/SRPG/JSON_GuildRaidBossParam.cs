// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GuildRaidBossParam
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
  public class JSON_GuildRaidBossParam : JSON_GuildRaidMasterParam
  {
    public int id;
    public int period_id;
    public int area_no;
    public string name;
    public int hp;
    public int hp_warning;
    public string unit_iname;
    public string quest_iname;
    public int score_id;
    public string buff_id;
    public string mob_buff_id;
    public string beat_reward_id;
    public string damage_ranking_reward_id;
    public string damage_ratio_reward_id;
    public string lastatk_reward_id;
  }
}
