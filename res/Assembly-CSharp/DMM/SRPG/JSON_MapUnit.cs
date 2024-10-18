// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MapUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class JSON_MapUnit
  {
    public JSON_MapPartyUnit[] party;
    public JSON_MapEnemyUnit[] enemy;
    public JSON_MapPartyUnit[] arena;
    public JSON_QuestMonitorCondition w_cond;
    public JSON_QuestMonitorCondition l_cond;
    public JSON_GimmickEvent[] gs;
    public JSON_MapEnemyUnit[] deck;
    public JSON_RandUnitTag[] rand_tag;
    public JSON_MapPartySubCT[] party_subs;
    public JSON_MapTrick[] tricks;
    public JSON_InfinitySpawnGroup[] inf_spawn_group;
    public JSON_InfinitySpawn[] inf_spawn;
    public JSON_MapEnemyUnit[] inf_deck;

    public List<JSON_MapEnemyUnit> GetRandFixedUnit()
    {
      List<JSON_MapEnemyUnit> randFixedUnit = new List<JSON_MapEnemyUnit>();
      for (int index = 0; index < this.enemy.Length; ++index)
      {
        if (MonoSingleton<GameManager>.Instance.MasterParam.ContainsUnitID(this.enemy[index].iname))
        {
          UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(this.enemy[index].iname);
          if (unitParam.type == EUnitType.Gem || unitParam.type == EUnitType.BreakObj || unitParam.type == EUnitType.Treasure)
            randFixedUnit.Add(this.enemy[index]);
        }
      }
      return randFixedUnit;
    }

    public bool HasRandomEnemies
    {
      get
      {
        return this.deck != null && this.deck.Length > 0 && this.rand_tag != null && this.rand_tag.Length > 0;
      }
    }

    public JSON_MapEnemyUnit[] ReplacedRandEnemy(
      RandDeckResult[] rand_lot_result,
      bool delete_rand_symbol = true)
    {
      if (rand_lot_result == null || rand_lot_result.Length <= 0)
      {
        if (delete_rand_symbol)
          this.DeleteRandSymbolInEnemies();
        return this.enemy;
      }
      for (int index = 0; index < rand_lot_result.Length; ++index)
      {
        if (this.enemy.Length <= rand_lot_result[index].set_id)
        {
          DebugUtility.LogError("ランダム抽選結果と敵配置データに不整合が発生");
          return (JSON_MapEnemyUnit[]) null;
        }
        JSON_MapEnemyUnit jsonMapEnemyUnit1 = this.enemy[rand_lot_result[index].set_id];
        if (!jsonMapEnemyUnit1.IsRandSymbol)
        {
          DebugUtility.LogError("ランダム抽選結果と敵配置データに不整合が発生");
          return (JSON_MapEnemyUnit[]) null;
        }
        JSON_MapEnemyUnit jsonMapEnemyUnit2 = JsonUtility.FromJson<JSON_MapEnemyUnit>(JsonUtility.ToJson((object) this.deck[rand_lot_result[index].id]));
        jsonMapEnemyUnit2.x = jsonMapEnemyUnit1.x;
        jsonMapEnemyUnit2.y = jsonMapEnemyUnit1.y;
        jsonMapEnemyUnit2.dir = jsonMapEnemyUnit1.dir;
        jsonMapEnemyUnit2.name = jsonMapEnemyUnit1.name;
        jsonMapEnemyUnit2.entries = jsonMapEnemyUnit1.entries;
        jsonMapEnemyUnit2.entries_and = jsonMapEnemyUnit1.entries_and;
        this.enemy[rand_lot_result[index].set_id] = jsonMapEnemyUnit2;
      }
      if (delete_rand_symbol)
        this.DeleteRandSymbolInEnemies();
      return this.enemy;
    }

    private void DeleteRandSymbolInEnemies()
    {
      List<JSON_MapEnemyUnit> jsonMapEnemyUnitList = new List<JSON_MapEnemyUnit>();
      for (int index = 0; index < this.enemy.Length; ++index)
      {
        if (!this.enemy[index].IsRandSymbol)
          jsonMapEnemyUnitList.Add(this.enemy[index]);
      }
      this.enemy = jsonMapEnemyUnitList.ToArray();
    }

    public JSON_MapEnemyUnit[] GetMapEnemyUnits_AvailableRandom()
    {
      List<JSON_MapEnemyUnit> jsonMapEnemyUnitList = new List<JSON_MapEnemyUnit>();
      List<int> intList = new List<int>();
      for (int index = 0; index < this.rand_tag.Length; ++index)
      {
        if (this.rand_tag[index] == null || this.rand_tag[index].spawn < 1)
          intList.Add(index);
      }
      for (int index = 0; index < this.deck.Length; ++index)
      {
        if (!intList.Contains(this.deck[index].RandTagIndex) && this.deck[index].weight >= (short) 1)
          jsonMapEnemyUnitList.Add(this.deck[index]);
      }
      return jsonMapEnemyUnitList.ToArray();
    }

    public JSON_MapEnemyUnit[] GetMapEnemyUnits_AvailableInfinitySpawn()
    {
      List<JSON_MapEnemyUnit> jsonMapEnemyUnitList = new List<JSON_MapEnemyUnit>();
      for (int index = 0; index < this.inf_deck.Length; ++index)
      {
        JSON_MapEnemyUnit jsonMapEnemyUnit = this.inf_deck[index];
        if (jsonMapEnemyUnit != null && !jsonMapEnemyUnit.IsEmptySymbol)
          jsonMapEnemyUnitList.Add(jsonMapEnemyUnit);
      }
      return jsonMapEnemyUnitList.ToArray();
    }
  }
}
