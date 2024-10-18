// Decompiled with JetBrains decompiler
// Type: SRPG.GvGRuleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GvGRuleParam : GvGMasterParam<JSON_GvGRuleParam>
  {
    private string mIname;
    private string mName;
    private int mUnitCount;
    private int mDefUnitCount;
    private int mUnitCoolTime;
    private List<EBirth> mCndsBirth;

    public string Iname => this.mIname;

    public string Name => this.mName;

    public int UnitCount => this.mUnitCount;

    public int DefUnitCount => this.mDefUnitCount;

    public int UnitCoolTime => this.mUnitCoolTime;

    public List<EBirth> CndsBirth => this.mCndsBirth;

    public override bool Deserialize(JSON_GvGRuleParam json)
    {
      if (json == null)
        return false;
      this.mIname = json.iname;
      this.mName = json.name;
      this.mUnitCount = json.unit_cnt;
      this.mDefUnitCount = json.def_unit_cnt;
      this.mUnitCoolTime = json.unit_cool_time;
      this.mCndsBirth = new List<EBirth>();
      if (json.cnds_birth != null)
      {
        for (int index = 0; index < json.cnds_birth.Length; ++index)
          this.mCndsBirth.Add((EBirth) json.cnds_birth[index]);
      }
      return true;
    }

    public bool IsExistConditions() => this.mCndsBirth != null && this.mCndsBirth.Count > 0;

    public List<UnitData> GetDisableUnits(List<UnitData> units)
    {
      List<UnitData> disableUnits = new List<UnitData>((IEnumerable<UnitData>) units);
      for (int index = disableUnits.Count - 1; index >= 0; --index)
      {
        if (this.IsEnableUnit(disableUnits[index]))
          disableUnits.Remove(disableUnits[index]);
      }
      return disableUnits;
    }

    public List<UnitData> GetEnableUnits(List<UnitData> units)
    {
      List<UnitData> enableUnits = new List<UnitData>((IEnumerable<UnitData>) units);
      for (int index = enableUnits.Count - 1; index >= 0; --index)
      {
        if (!this.IsEnableUnit(enableUnits[index]))
          enableUnits.Remove(enableUnits[index]);
      }
      return enableUnits;
    }

    private bool IsEnableUnit(UnitData unit)
    {
      return this.mCndsBirth.Count <= 0 || this.mCndsBirth.FindIndex((Predicate<EBirth>) (birth => birth == (EBirth) unit.UnitParam.birthID)) > -1;
    }
  }
}
