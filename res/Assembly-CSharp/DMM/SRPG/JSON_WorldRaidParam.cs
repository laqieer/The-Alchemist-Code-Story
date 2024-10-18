// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_WorldRaidParam
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
  public class JSON_WorldRaidParam
  {
    public string iname;
    public string event_day;
    public string begin_time;
    public string ch_end_time;
    public string ch_perm_time;
    public string end_time;
    public string desc_url;
    public string desc_title;
    public JSON_WorldRaidParam.BossInfo[] boss;

    [MessagePackObject(true)]
    [Serializable]
    public class BossInfo
    {
      public int is_last_boss;
      public string boss_id;
      public string beat_reward_id;
      public string la_reward_id;
      public string dmg_reward_id;
      public string rank_reward_id;
      public string eb_buff_id;
    }
  }
}
