// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_RaidRescueMember
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
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
