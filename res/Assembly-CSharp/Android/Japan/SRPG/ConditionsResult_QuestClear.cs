// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_QuestClear
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ConditionsResult_QuestClear : ConditionsResult
  {
    private QuestParam mQuestParam;

    public ConditionsResult_QuestClear(QuestParam questParam)
    {
      this.mQuestParam = questParam;
      this.mIsClear = questParam.state == QuestStates.Cleared;
      this.mTargetValue = 2;
      this.mCurrentValue = (int) questParam.state;
    }

    public override string text
    {
      get
      {
        return LocalizedText.Get("sys.TOBIRA_CONDITIONS_QUEST_CLEAR", new object[1]{ (object) this.mQuestParam.name });
      }
    }

    public override string errorText
    {
      get
      {
        return string.Format("クエスト「{0}」をクリアしていません", (object) this.mQuestParam.name);
      }
    }
  }
}
