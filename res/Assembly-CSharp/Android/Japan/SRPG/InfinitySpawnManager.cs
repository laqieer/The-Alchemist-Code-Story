// Decompiled with JetBrains decompiler
// Type: SRPG.InfinitySpawnManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class InfinitySpawnManager
  {
    private JSON_MapUnit mMapSet;
    private InfinitySpawnGroupData[] mInfinitySpawnGroups;
    private Dictionary<int, List<JSON_MapEnemyUnit>> mInfinitySpawnDeck;

    public InfinitySpawnGroupData[] InfinitySpawnGroups
    {
      get
      {
        return this.mInfinitySpawnGroups;
      }
    }

    public JSON_MapEnemyUnit[] Deck
    {
      get
      {
        return this.mMapSet.inf_deck;
      }
    }

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
        int spawnableCount = this.mInfinitySpawnGroups[index1].GetSpawnableCount(units, reserved_count, act_count, false);
        reserved_count += spawnableCount;
        for (int index2 = 0; index2 < spawnableCount; ++index2)
        {
          int deck_index = this.Lot(this.mInfinitySpawnDeck[this.mInfinitySpawnGroups[index1].deck], false);
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
      if (unit.IsEmptySymbol)
        return -1;
      return this.GetDeckIndex(unit);
    }

    public int GetDeckIndex(JSON_MapEnemyUnit unit)
    {
      return Array.FindIndex<JSON_MapEnemyUnit>(this.mMapSet.inf_deck, (Predicate<JSON_MapEnemyUnit>) (u => u == unit));
    }

    public JSON_MapEnemyUnit GetDeckUnit(int deck_index)
    {
      if (deck_index <= -1)
        return (JSON_MapEnemyUnit) null;
      return this.mMapSet.inf_deck[deck_index];
    }
  }
}
