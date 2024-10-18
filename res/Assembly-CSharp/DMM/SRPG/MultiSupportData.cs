// Decompiled with JetBrains decompiler
// Type: SRPG.MultiSupportData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class MultiSupportData
  {
    public string UID { get; private set; }

    public string FUID { get; private set; }

    public string Name { get; private set; }

    public int Lv { get; private set; }

    public List<UnitData> Units { get; private set; }

    public ViewGuildData Guild { get; private set; }

    public bool IsFriend { get; private set; }

    public int Cost { get; private set; }

    public bool Deserialize(JSON_MultiSupport json)
    {
      if (json == null)
        return false;
      this.UID = json.uid;
      this.FUID = json.fuid;
      this.Name = json.name;
      this.Lv = json.lv;
      this.IsFriend = json.isFriend != 0;
      this.Cost = json.cost;
      if (json.units == null)
        return false;
      this.Units = new List<UnitData>();
      for (int index = 0; index < json.units.Length; ++index)
      {
        if (json.units[index] != null)
        {
          UnitData unitData = new UnitData();
          unitData.Deserialize(json.units[index]);
          this.Units.Add(unitData);
        }
      }
      if (this.Units.Count <= 0)
        return false;
      this.Guild = new ViewGuildData();
      this.Guild.Deserialize(json.guild);
      return true;
    }
  }
}
