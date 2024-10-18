// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_RaidBossInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  [Serializable]
  public class JSON_RaidBossInfo
  {
    public int no;
    public int boss_id;
    public int round;
    public int current_hp;
    public long start_time;
    public int is_reward;
    public int is_timeover;
    public int is_rescue_damage_zero;
    public int is_beat_resucue;
  }
}
