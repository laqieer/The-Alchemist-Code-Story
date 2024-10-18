// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_Unit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public abstract class ConditionsResult_Unit : ConditionsResult
  {
    private UnitData mUnitData;
    private UnitParam mUnitParam;

    public ConditionsResult_Unit(UnitData unitData, UnitParam unitParam)
    {
      this.mUnitData = unitData;
      this.mUnitParam = unitParam;
    }

    public UnitData unitData => this.mUnitData;

    public bool hasUnitData => this.mUnitData != null;

    public string unitName
    {
      get
      {
        if (this.mUnitData != null)
          return this.mUnitData.UnitParam.name;
        return this.mUnitParam != null ? this.mUnitParam.name : string.Empty;
      }
    }
  }
}
