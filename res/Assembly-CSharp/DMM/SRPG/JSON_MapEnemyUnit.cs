// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MapEnemyUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class JSON_MapEnemyUnit : JSON_MapPartyUnit
  {
    public string iname;
    public int side;
    public int lv;
    public int rare;
    public int awake;
    public int elem;
    public int exp;
    public int gems;
    public int gold;
    public int search;
    public int ctrl;
    public int no_st_drop;
    public int no_disp_drop;
    public string drop;
    public int notice_damage;
    public string[] notice_members;
    public JSON_MapEquipAbility[] abils;
    public JSON_AIActionTable acttbl;
    public AIPatrolTable patrol;
    public string fskl;
    public short weight;
    public byte tag;
    public int spawn_max;
    public MapBreakObj break_obj;
    public int need_dead;
    public int is_raid_boss;
    public int withdraw_drop;

    public bool IsRandSymbol => this.iname.StartsWith("enemy_");

    public int RandTagIndex
    {
      get
      {
        if (!this.IsRandSymbol)
          return -1;
        string[] strArray = this.iname.Split('_');
        return int.Parse(strArray[strArray.Length - 1]);
      }
    }

    public bool IsEmptySymbol => this.iname == "EMPTY";
  }
}
