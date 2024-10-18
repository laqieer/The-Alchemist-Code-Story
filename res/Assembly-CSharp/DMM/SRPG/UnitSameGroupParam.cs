// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSameGroupParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace SRPG
{
  public class UnitSameGroupParam
  {
    public string name;
    public string[] units;

    public static void Deserialize(
      ref List<UnitSameGroupParam> ref_params,
      JSON_UnitSameGroupParam[] json)
    {
      if (ref_params == null)
        ref_params = new List<UnitSameGroupParam>();
      ref_params.Clear();
      if (json == null)
        return;
      foreach (JSON_UnitSameGroupParam json1 in json)
      {
        UnitSameGroupParam unitSameGroupParam = new UnitSameGroupParam();
        unitSameGroupParam.Deserialize(json1);
        ref_params.Add(unitSameGroupParam);
      }
    }

    public bool Deserialize(JSON_UnitSameGroupParam json)
    {
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
      List<string> stringList = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      if (this.units == null)
        return string.Empty;
      for (int index = 0; index < this.units.Length; ++index)
      {
        if (!string.IsNullOrEmpty(this.units[index]))
        {
          UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.units[index]);
          if (unitParam != null && !stringList.Contains(unitParam.name))
            stringList.Add(unitParam.name);
        }
      }
      for (int index = 0; index < stringList.Count; ++index)
      {
        if (index != 0)
          stringBuilder.Append(LocalizedText.Get("sys.PARTYEDITOR_SAMEUNIT_PLUS"));
        stringBuilder.Append(stringList[index]);
      }
      return stringBuilder.ToString();
    }

    public string GetGroupUnitOtherNameText(string unit_name)
    {
      List<string> stringList = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      if (this.units == null)
        return string.Empty;
      for (int index = 0; index < this.units.Length; ++index)
      {
        if (!string.IsNullOrEmpty(this.units[index]) && !(unit_name == this.units[index]))
        {
          UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.units[index]);
          if (unitParam != null && !stringList.Contains(unitParam.name))
            stringList.Add(unitParam.name);
        }
      }
      for (int index = 0; index < stringList.Count; index = index + 1 + 1)
      {
        if (index != 0)
          stringBuilder.Append(LocalizedText.Get("sys.PARTYEDITOR_SAMEUNIT_PLUS"));
        stringBuilder.Append(stringList[index]);
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

    public static List<UnitSameGroupParam> IsSameUnitInParty(UnitData[] units)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null) || instance.MasterParam == null || units == null)
        return (List<UnitSameGroupParam>) null;
      List<UnitSameGroupParam> unitSameGroupParamList1 = new List<UnitSameGroupParam>();
      List<UnitSameGroupParam> unitSameGroupParamList2 = new List<UnitSameGroupParam>();
      for (int index1 = 0; index1 < units.Length; ++index1)
      {
        if (units[index1] != null)
        {
          for (int index2 = 0; index2 < unitSameGroupParamList1.Count; ++index2)
          {
            if (unitSameGroupParamList1[index2] != null && unitSameGroupParamList1[index2].IsInGroup(units[index1].UnitID) && !unitSameGroupParamList2.Contains(unitSameGroupParamList1[index2]))
              unitSameGroupParamList2.Add(unitSameGroupParamList1[index2]);
          }
          UnitSameGroupParam unitSameGroup = instance.MasterParam.GetUnitSameGroup(units[index1].UnitID);
          if (unitSameGroup != null && !unitSameGroupParamList1.Contains(unitSameGroup))
            unitSameGroupParamList1.Add(unitSameGroup);
        }
      }
      return unitSameGroupParamList2;
    }

    public static bool IsSameUnitInParty(UnitData[] units, string unitID)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null) || instance.MasterParam == null || units == null)
        return false;
      UnitSameGroupParam unitSameGroup = instance.MasterParam.GetUnitSameGroup(unitID);
      if (unitSameGroup == null)
        return false;
      for (int index = 0; index < units.Length; ++index)
      {
        if (units[index] != null && !(units[index].UnitID == unitID) && unitSameGroup != null && unitSameGroup.IsInGroup(units[index].UnitID))
          return true;
      }
      return false;
    }

    public static UnitSameGroupParam IsSameUnitInParty(
      UnitData[] units,
      UnitData changed,
      UnitData selected)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null) || instance.MasterParam == null || units == null)
        return (UnitSameGroupParam) null;
      List<UnitSameGroupParam> unitSameGroupParamList = new List<UnitSameGroupParam>();
      UnitSameGroupParam unitSameGroup = instance.MasterParam.GetUnitSameGroup(selected.UnitID);
      if (unitSameGroup != null)
        unitSameGroupParamList.Add(unitSameGroup);
      for (int index1 = 0; index1 < units.Length; ++index1)
      {
        if (units[index1] != null && units[index1] != changed && units[index1] != selected)
        {
          for (int index2 = 0; index2 < unitSameGroupParamList.Count; ++index2)
          {
            if (unitSameGroupParamList[index2] != null && unitSameGroupParamList[index2].IsInGroup(units[index1].UnitID))
              return unitSameGroupParamList[index2];
          }
        }
      }
      return (UnitSameGroupParam) null;
    }
  }
}
