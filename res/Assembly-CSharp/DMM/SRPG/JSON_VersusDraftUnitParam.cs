// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_VersusDraftUnitParam
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
  public class JSON_VersusDraftUnitParam
  {
    public long id;
    public long draft_unit_id;
    public int weight;
    public string unit_iname;
    public int rare;
    public int awake;
    public int lv;
    public int select_job_idx;
    public string job1_iname;
    public int job1_lv;
    public int job1_equip;
    public string job2_iname;
    public int job2_lv;
    public int job2_equip;
    public string job3_iname;
    public int job3_lv;
    public int job3_equip;
    public string abil1_iname;
    public int abil1_lv;
    public string abil2_iname;
    public int abil2_lv;
    public string abil3_iname;
    public int abil3_lv;
    public string abil4_iname;
    public int abil4_lv;
    public string abil5_iname;
    public int abil5_lv;
    public string arti1_iname;
    public int arti1_rare;
    public int arti1_lv;
    public string arti2_iname;
    public int arti2_rare;
    public int arti2_lv;
    public string arti3_iname;
    public int arti3_rare;
    public int arti3_lv;
    public string card_iname;
    public int card_lv;
    public int card_leaderskill;
    public string card2_iname;
    public int card2_lv;
    public int door1_lv;
    public int door2_lv;
    public int door3_lv;
    public int door4_lv;
    public int door5_lv;
    public int door6_lv;
    public int door7_lv;
    public string master_abil;
    public string skin;
    public string[] clear_quest_iname;
  }
}
