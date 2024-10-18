// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MultiTowerFloorParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
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
  }
}
