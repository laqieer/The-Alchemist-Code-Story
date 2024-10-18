// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGroupParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Text;

#nullable disable
namespace SRPG
{
  public class UnitGroupParam
  {
    public string iname;
    public string name;
    public string[] units;

    public bool Deserialize(JSON_UnitGroupParam json)
    {
      this.iname = json.iname;
      this.name = json.name;
      this.units = json.units;
      return true;
    }

    public bool IsInGroup(string unit_iname)
    {
      return Array.FindIndex<string>(this.units, (Predicate<string>) (u => u == unit_iname)) >= 0;
    }

    public string GetName()
    {
      return string.IsNullOrEmpty(this.name) ? this.GetGroupUnitAllNameText() : this.name;
    }

    public string GetGroupUnitAllNameText()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.units == null)
        return string.Empty;
      for (int index = 0; index < this.units.Length; ++index)
      {
        UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.units[index]);
        if (unitParam != null)
        {
          stringBuilder.Append(unitParam.name);
          if (index < this.units.Length - 1)
            stringBuilder.Append("CONCEPT_CARD_SKILL_DESCRIPTION_COMMA");
        }
      }
      return stringBuilder.ToString();
    }

    public static bool IsInGroup(UnitGroupParam[] group_param, string unit_iname)
    {
      foreach (UnitGroupParam unitGroupParam in group_param)
      {
        if (unitGroupParam.IsInGroup(unit_iname))
          return true;
      }
      return false;
    }
  }
}
