﻿// Decompiled with JetBrains decompiler
// Type: SRPG.VersusCpuData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class VersusCpuData
  {
    public string Name;
    public int Lv;
    public UnitData[] Units;
    public int[] Place;
    public int Score;
    public int Deck;

    public bool Deserialize(Json_VersusCpuData json, int idx)
    {
      if (json == null)
        return false;
      this.Name = json.name;
      this.Lv = json.lv;
      this.Deck = idx;
      if (json.units != null)
      {
        int length = json.units.Length;
        this.Units = new UnitData[length];
        for (int index = 0; index < length; ++index)
        {
          this.Units[index] = new UnitData();
          this.Units[index].Deserialize(json.units[index]);
        }
      }
      if (json.place != null)
      {
        int length = json.place.Length;
        this.Place = new int[length];
        for (int index = 0; index < length; ++index)
          this.Place[index] = json.place[index];
      }
      this.Score = json.score;
      return true;
    }
  }
}
