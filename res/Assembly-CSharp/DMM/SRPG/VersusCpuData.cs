// Decompiled with JetBrains decompiler
// Type: SRPG.VersusCpuData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class VersusCpuData : ICombatPowerContent
  {
    public string Name;
    public int Lv;
    public UnitData[] Units;
    public int[] Place;
    public int Score;
    public int Deck;

    public long CombatPower
    {
      get => (long) PartyUtility.CalcTotalCombatPower((IEnumerable<UnitData>) this.Units);
    }

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
