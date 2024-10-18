// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public abstract class ConditionsResult
  {
    protected bool mIsClear;
    protected int mTargetValue;
    protected int mCurrentValue;

    public bool isClear => this.mIsClear;

    public int targetValue => this.mTargetValue;

    public int currentValue => this.mCurrentValue;

    public abstract string text { get; }

    public abstract string errorText { get; }

    public bool isConditionsUnitLv
    {
      get => (object) this.GetType() == (object) typeof (ConditionsResult_UnitLv);
    }

    public bool isConditionsAwake
    {
      get => (object) this.GetType() == (object) typeof (ConditionsResult_AwakeLv);
    }

    public bool isConditionsJobLv
    {
      get => (object) this.GetType() == (object) typeof (ConditionsResult_JobLv);
    }

    public bool isConditionsTobiraLv
    {
      get => (object) this.GetType() == (object) typeof (ConditionsResult_TobiraLv);
    }

    public bool isConditionsQuestClear
    {
      get => (object) this.GetType() == (object) typeof (ConditionsResult_QuestClear);
    }

    public bool isConditionsTobiraNoConditions
    {
      get => (object) this.GetType() == (object) typeof (ConditionsResult_TobiraNoConditions);
    }
  }
}
