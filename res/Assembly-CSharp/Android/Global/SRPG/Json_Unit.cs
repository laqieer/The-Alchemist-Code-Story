// Decompiled with JetBrains decompiler
// Type: SRPG.Json_Unit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class Json_Unit
  {
    public long iid;
    public string iname;
    public int rare;
    public int plus;
    public int lv;
    public int exp;
    public int fav;
    public Json_MasterAbility abil;
    public Json_CollaboAbility c_abil;
    public Json_Job[] jobs;
    public Json_UnitSelectable select;
    public string[] quest_clear_unlocks;
    public int elem;
  }
}
