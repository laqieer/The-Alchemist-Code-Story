﻿// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_AdvanceEventModeInfoParam
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
  public class JSON_AdvanceEventModeInfoParam
  {
    public string star_id;
    public int liberation_qno;
    public string boss_unit_id;
    public int boss_hp;
    public string boss_ch_item_id;
    public int boss_ch_item_num;
    public string boss_reward_id;
    public int mode_ui_index;
    public string lap_boss_id;
  }
}
