// Decompiled with JetBrains decompiler
// Type: SRPG.InfinitySpawnManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class InfinitySpawnManager
  {
    private JSON_MapUnit mMapSet;
    private InfinitySpawnGroupData[] mInfinitySpawnGroups;
    private Dictionary<int, List<JSON_MapEnemyUnit>> mInfinitySpawnDeck;

    public InfinitySpawnGroupData[] InfinitySpawnGroups => this.mInfinitySpawnGroups;

    public JSON_MapEnemyUnit[] Deck => this.mMapSet.inf_deck;

    public void Init(JSON_MapUnit mapset)
    {
      this.mMapSet = mapset;
      InfinitySpawnData[] spawns = new InfinitySpawnData[mapset.inf_spawn.Length];
      for (int index = 0; index < mapset.inf_spawn.Length; ++index)
      {
        InfinitySpawnData infinitySpawnData = new InfinitySpawnData();
        infinitySpawnData.Setup(mapset.inf_spawn[index]);
        spawns[index] = infinitySpawnData;
      }
      this.mInfinitySpawnGroups = new InfinitySpawnGroupData[mapset.inf_spawn_group.Length];
      for (int index = 0; index < mapset.inf_spawn_group.Length; ++index)
      {
        InfinitySpawnGroupData infinitySpawnGroupData = new InfinitySpawnGroupData();
        infinitySpawnGroupData.Setup(mapset.inf_spawn_group[index]);
        infinitySpawnGroupData.AddSameGroup(spawns);
        this.mInfinitySpawnGroups[index] = infinitySpawnGroupData;
      }
      this.mInfinitySpawnDeck = new Dictionary<int, List<JSON_MapEnemyUnit>>();
      for (int index = 0; index < mapset.inf_deck.Length; ++index)
      {
        if (mapset.inf_deck[index] != null && mapset.inf_deck[index].tag >= (byte) 0 && mapset.inf_deck[index].weight > (short) 0)
        {
          int tag = (int) mapset.inf_deck[index].tag;
          if (!this.mInfinitySpawnDeck.ContainsKey(tag))
          {
            List<JSON_MapEnemyUnit> jsonMapEnemyUnitList = new List<JSON_MapEnemyUnit>();
            this.mInfinitySpawnDeck.Add((int) mapset.inf_deck[index].tag, jsonMapEnemyUnitList);
          }
          this.mInfinitySpawnDeck[tag].Add(mapset.inf_deck[index]);
        }
      }
    }

    public void RefreshSpawnEnemyUnits(List<Unit> units)
    {
      int act_count = 0;
      for (int index = 0; index < units.Count; ++index)
        act_count += units[index].ActionCount;
      int reserved_count = 0;
      for (int index1 = 0; index1 < this.mInfinitySpawnGroups.Length; ++index1)
      {
        this.mInfinitySpawnGroups[index1].Reset();
        int spawnableCount = this.mInfinitySpawnGroups[index1].GetSpawnableCount(units, reserved_count, act_count);
        reserved_count += spawnableCount;
        for (int index2 = 0; index2 < spawnableCount; ++index2)
        {
          int deck_index = this.Lot(this.mInfinitySpawnDeck[this.mInfinitySpawnGroups[index1].deck]);
          this.mInfinitySpawnGroups[index1].AddReserve(deck_index);
        }
      }
    }

    public void RefreshSpawnEnemyUnitsForBattleStart(List<Unit> units)
    {
      int reserved_count = 0;
      for (int index1 = 0; index1 < this.mInfinitySpawnGroups.Length; ++index1)
      {
        this.mInfinitySpawnGroups[index1].Reset();
        if (this.mInfinitySpawnGroups[index1].is_spawn_at_start != 0)
        {
          int spawnableCount = this.mInfinitySpawnGroups[index1].GetSpawnableCount(units, reserved_count, 0, true);
          reserved_count += spawnableCount;
          for (int index2 = 0; index2 < spawnableCount; ++index2)
          {
            int deck_index = this.Lot(this.mInfinitySpawnDeck[this.mInfinitySpawnGroups[index1].deck], this.mInfinitySpawnGroups[index1].is_skip_empty_at_start != 0);
            this.mInfinitySpawnGroups[index1].AddReserve(deck_index);
          }
        }
      }
    }

    private int Lot(List<JSON_MapEnemyUnit> deck, bool is_ignore_empty = false)
    {
      int num1 = 0;
      for (int index = 0; index < deck.Count; ++index)
      {
        if (!is_ignore_empty || !deck[index].IsEmptySymbol)
          num1 += (int) deck[index].weight;
      }
      int num2 = (int) ((long) SceneBattle.Instance.Battle.GetRandom() % (long) num1);
      JSON_MapEnemyUnit unit = (JSON_MapEnemyUnit) null;
      int num3 = 0;
      for (int index = 0; index < deck.Count; ++index)
      {
        if (!is_ignore_empty || !deck[index].IsEmptySymbol)
        {
          num3 += (int) deck[index].weight;
          if (num3 > num2)
          {
            unit = deck[index];
            break;
          }
        }
      }
      return unit.IsEmptySymbol ? -1 : this.GetDeckIndex(unit);
    }

    public int GetDeckIndex(JSON_MapEnemyUnit unit)
    {
      return Array.FindIndex<JSON_MapEnemyUnit>(this.mMapSet.inf_deck, (Predicate<JSON_MapEnemyUnit>) (u => u == unit));
    }

    public JSON_MapEnemyUnit GetDeckUnit(int deck_index)
    {
      return deck_index <= -1 ? (JSON_MapEnemyUnit) null : this.mMapSet.inf_deck[deck_index];
    }
  }
}
