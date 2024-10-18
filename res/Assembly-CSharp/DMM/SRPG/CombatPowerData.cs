// Decompiled with JetBrains decompiler
// Type: SRPG.CombatPowerData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class CombatPowerData
  {
    private int m_TotalCombatPower;
    private int m_TotalFireCombatPower;
    private int m_TotalWaterCombatPower;
    private int m_TotalThunderCombatPower;
    private int m_TotalWindCombatPower;
    private int m_TotalShineCombatPower;
    private int m_TotalDarkCombatPower;
    private int m_HighestCombatPower;
    private bool m_IsChanged;

    public int TotalCombatPower => this.m_TotalCombatPower;

    public int TotalFireCombatPower => this.m_TotalFireCombatPower;

    public int TotalWaterCombatPower => this.m_TotalWaterCombatPower;

    public int TotalThunderCombatPower => this.m_TotalThunderCombatPower;

    public int TotalWindCombatPower => this.m_TotalWindCombatPower;

    public int TotalShineCombatPower => this.m_TotalShineCombatPower;

    public int TotalDarkCombatPower => this.m_TotalDarkCombatPower;

    public int HighestCombatPower => this.m_HighestCombatPower;

    public bool IsChanged => this.m_IsChanged;

    private CombatPowerData Clone()
    {
      return new CombatPowerData()
      {
        m_TotalCombatPower = this.m_TotalCombatPower,
        m_TotalFireCombatPower = this.m_TotalFireCombatPower,
        m_TotalWaterCombatPower = this.m_TotalWaterCombatPower,
        m_TotalThunderCombatPower = this.m_TotalThunderCombatPower,
        m_TotalWindCombatPower = this.m_TotalWindCombatPower,
        m_TotalShineCombatPower = this.m_TotalShineCombatPower,
        m_TotalDarkCombatPower = this.m_TotalDarkCombatPower,
        m_HighestCombatPower = this.m_HighestCombatPower
      };
    }

    private bool IsDifferentValues(CombatPowerData compareTarget)
    {
      return this.m_TotalFireCombatPower != compareTarget.m_TotalFireCombatPower || this.m_TotalWaterCombatPower != compareTarget.m_TotalWaterCombatPower || this.m_TotalThunderCombatPower != compareTarget.m_TotalThunderCombatPower || this.m_TotalWindCombatPower != compareTarget.m_TotalWindCombatPower || this.m_TotalShineCombatPower != compareTarget.m_TotalShineCombatPower || this.m_TotalDarkCombatPower != compareTarget.m_TotalDarkCombatPower || this.m_HighestCombatPower != compareTarget.m_HighestCombatPower;
    }

    private void ClearValues()
    {
      this.m_TotalCombatPower = 0;
      this.m_TotalFireCombatPower = 0;
      this.m_TotalWaterCombatPower = 0;
      this.m_TotalThunderCombatPower = 0;
      this.m_TotalWindCombatPower = 0;
      this.m_TotalShineCombatPower = 0;
      this.m_TotalDarkCombatPower = 0;
      this.m_HighestCombatPower = 0;
    }

    public void ClearChangeFlag() => this.m_IsChanged = false;

    public void UpdateCombatPower(IEnumerable<UnitData> units)
    {
      if (units == null)
        return;
      CombatPowerData combatPowerData = (CombatPowerData) null;
      if (!this.IsChanged)
        combatPowerData = this.Clone();
      this.ClearValues();
      foreach (UnitData unit in units)
      {
        if (unit != null)
        {
          int val2 = unit.CalcTotalParameter();
          this.m_HighestCombatPower = Math.Max(this.m_HighestCombatPower, val2);
          this.m_TotalCombatPower += val2;
          switch (unit.Element)
          {
            case EElement.Fire:
              this.m_TotalFireCombatPower += val2;
              continue;
            case EElement.Water:
              this.m_TotalWaterCombatPower += val2;
              continue;
            case EElement.Wind:
              this.m_TotalWindCombatPower += val2;
              continue;
            case EElement.Thunder:
              this.m_TotalThunderCombatPower += val2;
              continue;
            case EElement.Shine:
              this.m_TotalShineCombatPower += val2;
              continue;
            case EElement.Dark:
              this.m_TotalDarkCombatPower += val2;
              continue;
            default:
              continue;
          }
        }
      }
      if (this.IsChanged)
        return;
      this.m_IsChanged = combatPowerData.IsDifferentValues(this);
    }
  }
}
