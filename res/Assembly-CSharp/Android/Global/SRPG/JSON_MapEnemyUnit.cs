﻿// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MapEnemyUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
    public string drop;
    public int notice_damage;
    public string[] notice_members;
    public JSON_MapEquipAbility[] abils;
    public JSON_AIActionTable acttbl;
    public AIPatrolTable patrol;
    public string fskl;
    public short weight;
    public byte tag;
    public MapBreakObj break_obj;
  }
}
