// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidBossDetailData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class WorldRaidBossDetailData : WorldRaidBossData
  {
    public WorldRaidBossDetailData() => this.SelectedUnitInameList = new List<string>();

    public List<string> SelectedUnitInameList { get; private set; }

    public bool Deserialize(JSON_WorldRaidBossDetailData json)
    {
      if (json == null || !this.Deserialize((JSON_WorldRaidBossData) json))
        return false;
      this.SelectedUnitInameList.Clear();
      if (json.selected_units != null)
      {
        foreach (string selectedUnit in json.selected_units)
          this.SelectedUnitInameList.Add(selectedUnit);
      }
      return true;
    }
  }
}
