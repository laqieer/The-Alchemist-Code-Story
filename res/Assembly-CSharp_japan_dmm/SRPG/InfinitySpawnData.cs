// Decompiled with JetBrains decompiler
// Type: SRPG.InfinitySpawnData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class InfinitySpawnData
  {
    public int x;
    public int y;
    public int group;
    public int dir;
    private List<int> mReserveUnitIndexList = new List<int>();

    public List<int> ReserveUnitIndexList => this.mReserveUnitIndexList;

    public void Setup(JSON_InfinitySpawn json_data)
    {
      this.x = json_data.x;
      this.y = json_data.y;
      this.group = json_data.group;
      this.dir = json_data.dir;
    }

    public void AddReserveUnit(int deck_index)
    {
      if (deck_index <= -1)
        return;
      this.mReserveUnitIndexList.Add(deck_index);
    }

    public void Reset() => this.mReserveUnitIndexList.Clear();
  }
}
