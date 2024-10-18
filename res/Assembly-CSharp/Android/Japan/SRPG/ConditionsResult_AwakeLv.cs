// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_AwakeLv
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ConditionsResult_AwakeLv : ConditionsResult_Unit
  {
    public int mCondsAwakeLv;

    public ConditionsResult_AwakeLv(UnitData unitData, UnitParam unitParam, int condsAwakeLv)
      : base(unitData, unitParam)
    {
      this.mCondsAwakeLv = condsAwakeLv;
      this.mTargetValue = condsAwakeLv;
      if (unitData != null)
      {
        this.mIsClear = unitData.AwakeLv >= condsAwakeLv;
        this.mCurrentValue = unitData.AwakeLv;
      }
      else
        this.mIsClear = false;
    }

    public override string text
    {
      get
      {
        return LocalizedText.Get("sys.TOBIRA_CONDITIONS_UNIT_AWAKE", (object) this.unitName, (object) this.mCondsAwakeLv);
      }
    }

    public override string errorText
    {
      get
      {
        return string.Format("ユニット「{0}」を所持していません", (object) this.unitName);
      }
    }
  }
}
