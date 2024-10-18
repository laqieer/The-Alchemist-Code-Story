// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_UnitLv
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ConditionsResult_UnitLv : ConditionsResult_Unit
  {
    public int mCondsLv;

    public ConditionsResult_UnitLv(UnitData unitData, UnitParam unitParam, int condsLv)
      : base(unitData, unitParam)
    {
      this.mCondsLv = condsLv;
      this.mTargetValue = condsLv;
      if (unitData != null)
      {
        this.mIsClear = unitData.Lv >= condsLv;
        this.mCurrentValue = unitData.Lv;
      }
      else
        this.mIsClear = false;
    }

    public override string text
    {
      get
      {
        return LocalizedText.Get("sys.TOBIRA_CONDITIONS_UNIT_LEVEL", (object) this.unitName, (object) this.mCondsLv);
      }
    }

    public override string errorText
    {
      get
      {
        return this.unitData != null ? string.Format("ユニット「{0}」のレベルが条件を満たしていない", (object) this.unitName) : string.Format("ユニット「{0}」を所持していません", (object) this.unitName);
      }
    }
  }
}
