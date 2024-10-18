// Decompiled with JetBrains decompiler
// Type: SRPG.Json_Unit
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
    public JSON_ConceptCard[] concept_cards;
    public Json_Tobira[] doors;
    public Json_Ability[] door_abils;
    public int rental;
    public int favpoint;
    public string rental_iname;
    public Json_RuneData[] runes;
  }
}
