// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_QuestClear
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
      get => LocalizedText.Get("sys.TOBIRA_CONDITIONS_QUEST_CLEAR", (object) this.mQuestParam.name);
    }

    public override string errorText
    {
      get => string.Format("クエスト「{0}」をクリアしていません", (object) this.mQuestParam.name);
    }
  }
}
