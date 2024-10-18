// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GenesisEventTopSceneSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Genesis/EventTopSceneSelect", 32741)]
  [FlowNode.Pin(1, "Select", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "Top", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(21, "Stage", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(22, "Boss", FlowNode.PinTypes.Output, 22)]
  public class FlowNode_GenesisEventTopSceneSelect : FlowNode
  {
    private const int PIN_IN = 1;
    private const int PIN_OUT_TOP = 11;
    private const int PIN_OUT_STAGE = 21;
    private const int PIN_OUT_BOSS = 22;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      GenesisChapterManager.Instance.SetRestorePointIsStage(false);
      GenesisChapterManager.Instance.SetJumpFromMission(false);
      RestorePoints restorePoint = HomeWindow.GetRestorePoint();
      HomeWindow.SetRestorePoint(RestorePoints.Home);
      if (restorePoint != RestorePoints.GenesisStage && restorePoint != RestorePoints.GenesisBoss)
        this.ToQuest(MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID), false);
      else
        this.ToQuest(MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.LastPlayedQuest.Get()), true);
    }

    private void ToQuest(QuestParam quest, bool is_restore)
    {
      if (quest == null)
      {
        this.ToTop();
      }
      else
      {
        GenesisChapterParam chapterParamFromAreaId = MonoSingleton<GameManager>.Instance.GetGenesisChapterParamFromAreaId(quest.ChapterID);
        if (chapterParamFromAreaId == null)
        {
          this.ToTop();
        }
        else
        {
          GenesisManager.CurrentChapterParam = chapterParamFromAreaId;
          GenesisChapterManager.Instance.SetCurrentChapterParam(chapterParamFromAreaId);
          if (quest.type == QuestTypes.GenesisStory)
          {
            if (is_restore)
              GenesisChapterManager.Instance.SetRestorePointIsStage(true);
            else
              GenesisChapterManager.Instance.SetJumpFromMission(true);
            GenesisChapterManager.Instance.SetStageDifficulty(quest.difficulty);
            this.ActivateOutputLinks(21);
          }
          else if (quest.type == QuestTypes.GenesisBoss)
          {
            GenesisChapterManager.Instance.SetBossDifficulty(quest.difficulty);
            this.ActivateOutputLinks(22);
          }
          else
            this.ToTop();
        }
      }
    }

    private void ToTop() => this.ActivateOutputLinks(11);
  }
}
