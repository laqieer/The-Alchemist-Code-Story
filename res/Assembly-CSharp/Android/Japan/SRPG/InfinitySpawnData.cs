// Decompiled with JetBrains decompiler
// Type: SRPG.InfinitySpawnData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class InfinitySpawnData
  {
    private List<int> mReserveUnitIndexList = new List<int>();
    public int x;
    public int y;
    public int group;
    public int dir;

    public List<int> ReserveUnitIndexList
    {
      get
      {
        return this.mReserveUnitIndexList;
      }
    }

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

    public void Reset()
    {
      this.mReserveUnitIndexList.Clear();
    }
  }
}
