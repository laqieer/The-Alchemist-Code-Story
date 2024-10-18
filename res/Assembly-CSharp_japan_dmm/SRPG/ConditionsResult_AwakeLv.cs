// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_AwakeLv
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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

    public override string errorText => string.Format("ユニット「{0}」を所持していません", (object) this.unitName);
  }
}
