﻿// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_TowerParam
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
  public class JSON_TowerParam
  {
    public string iname;
    public string name;
    public string expr;
    public string banr;
    public string item;
    public string bg;
    public string floor_bg_open;
    public string floor_bg_close;
    public string eventURL;
    public short unit_recover_minute;
    public short unit_recover_coin;
    public byte can_unit_recover;
    public byte is_down;
    public byte is_view_ranking;
    public short unlock_level;
    public string unlock_quest;
    public string url;
    public short floor_reset_coin;
    public string score_iname;
  }
}
