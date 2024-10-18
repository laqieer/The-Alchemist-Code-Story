// Decompiled with JetBrains decompiler
// Type: SRPG.InfinitySpawnGroupData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class InfinitySpawnGroupData
  {
    private List<InfinitySpawnData> mInfinitySpawns = new List<InfinitySpawnData>();
    public int tag;
    public int deck;
    public int interval;
    public int spawn_max;
    public int spawn_unit_num;
    public int is_spawn_at_start;
    public int is_skip_empty_at_start;

    public List<InfinitySpawnData> InfinitySpawns
    {
      get
      {
        return this.mInfinitySpawns;
      }
    }

    public void Setup(JSON_InfinitySpawnGroup json_data)
    {
      this.tag = json_data.tag;
      this.deck = json_data.deck;
      this.interval = json_data.interval;
      this.spawn_max = json_data.spawn_max;
      this.spawn_unit_num = json_data.spawn_unit_num;
      this.is_spawn_at_start = json_data.is_spawn_at_start;
      this.is_skip_empty_at_start = json_data.is_skip_empty_at_start;
    }

    public void AddSameGroup(InfinitySpawnData[] spawns)
    {
      InfinitySpawnData[] all = Array.FindAll<InfinitySpawnData>(spawns, (Predicate<InfinitySpawnData>) (spawn => spawn.group == this.tag));
      if (all == null || all.Length <= 0)
        return;
      this.mInfinitySpawns.AddRange((IEnumerable<InfinitySpawnData>) all);
    }

    public int GetSpawnableCount(List<Unit> units, int reserved_count, int act_count, bool easy_check = false)
    {
      if (!easy_check && act_count % this.interval != 0)
        return 0;
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < units.Count; ++index)
      {
        if (units[index].UnitType == EUnitType.Unit && units[index].IsEntry && !units[index].IsDead)
        {
          if (units[index].Side == EUnitSide.Enemy)
            ++num1;
          if (units[index].InfinitySpawnTag == this.tag)
            ++num2;
        }
      }
      return Mathf.Clamp(this.spawn_unit_num, 0, Mathf.Max(0, Mathf.Min(this.spawn_max - num2, 13 - (num1 + reserved_count))));
    }

    public void AddReserve(int deck_index)
    {
      if (this.mInfinitySpawns.Count <= 0)
        return;
      this.mInfinitySpawns[(int) ((long) SceneBattle.Instance.Battle.GetRandom() % (long) this.mInfinitySpawns.Count)].AddReserveUnit(deck_index);
    }

    public void Reset()
    {
      for (int index = 0; index < this.mInfinitySpawns.Count; ++index)
        this.mInfinitySpawns[index].Reset();
    }
  }
}
