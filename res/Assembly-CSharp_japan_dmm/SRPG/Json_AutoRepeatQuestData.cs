// Decompiled with JetBrains decompiler
// Type: SRPG.Json_AutoRepeatQuestData
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
  public class Json_AutoRepeatQuestData
  {
    public string qid;
    public int[] units;
    public string start_time;
    public int time_per_lap;
    public int lap_num;
    public int lap_num_not_box;
    public int is_full_box;
    public int stop_reason;
    public int current_lap_num;
    public int gold;
    public BattleCore.Json_BtlDrop[][] drops;
    public int auto_repeat_check;
  }
}
