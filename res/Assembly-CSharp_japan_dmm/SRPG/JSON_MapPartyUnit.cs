// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MapPartyUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class JSON_MapPartyUnit
  {
    public string name;
    public string ai;
    public int x;
    public int y;
    public int dir;
    public int wait_e;
    public int wait_m;
    public int wait_exit;
    public int ct_calc;
    public int ct_val;
    public int fvoff;
    public int ai_type;
    public int ai_x;
    public int ai_y;
    public int ai_len;
    public string parent;
    public JSON_EventTrigger trg;
    public UnitEntryTrigger[] entries;
    public int entries_and;
  }
}
