// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_RaidBossInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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
