// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MultiTowerFloorParam
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
  public class JSON_MultiTowerFloorParam
  {
    public int id;
    public string title;
    public string name;
    public string expr;
    public string cond;
    public string tower_id;
    public int cond_floor;
    public string reward_id;
    public JSON_MapParam[] map;
    public short pt;
    public short lv;
    public short joblv;
    public short floor;
    public short unitnum;
    public short notcon;
    public string me_id;
    public int is_wth_no_chg;
    public string wth_set_id;
    public int is_skip;
    public string iname;
    public string rdy_cnd;
  }
}
