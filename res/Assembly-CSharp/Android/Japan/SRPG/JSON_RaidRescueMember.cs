// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_RaidRescueMember
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_RaidRescueMember
  {
    public string uid;
    public string name;
    public int lv;
    public int member_type;
    public Json_Unit unit;
    public string selected_award;
    public int lastlogin;
    public int area_id;
    public int boss_id;
    public int round;
    public int current_hp;
    public long start_time;
  }
}
