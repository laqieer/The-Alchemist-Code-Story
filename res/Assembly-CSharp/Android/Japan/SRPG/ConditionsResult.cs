// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public abstract class ConditionsResult
  {
    protected bool mIsClear;
    protected int mTargetValue;
    protected int mCurrentValue;

    public bool isClear
    {
      get
      {
        return this.mIsClear;
      }
    }

    public int targetValue
    {
      get
      {
        return this.mTargetValue;
      }
    }

    public int currentValue
    {
      get
      {
        return this.mCurrentValue;
      }
    }

    public abstract string text { get; }

    public abstract string errorText { get; }

    public bool isConditionsUnitLv
    {
      get
      {
        return this.GetType() == typeof (ConditionsResult_UnitLv);
      }
    }

    public bool isConditionsAwake
    {
      get
      {
        return this.GetType() == typeof (ConditionsResult_AwakeLv);
      }
    }

    public bool isConditionsJobLv
    {
      get
      {
        return this.GetType() == typeof (ConditionsResult_JobLv);
      }
    }

    public bool isConditionsTobiraLv
    {
      get
      {
        return this.GetType() == typeof (ConditionsResult_TobiraLv);
      }
    }

    public bool isConditionsQuestClear
    {
      get
      {
        return this.GetType() == typeof (ConditionsResult_QuestClear);
      }
    }

    public bool isConditionsTobiraNoConditions
    {
      get
      {
        return this.GetType() == typeof (ConditionsResult_TobiraNoConditions);
      }
    }
  }
}
