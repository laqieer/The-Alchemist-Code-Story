// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_UnitLv
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
        if (this.unitData != null)
          return string.Format("ユニット「{0}」のレベルが条件を満たしていない", (object) this.unitName);
        return string.Format("ユニット「{0}」を所持していません", (object) this.unitName);
      }
    }
  }
}
