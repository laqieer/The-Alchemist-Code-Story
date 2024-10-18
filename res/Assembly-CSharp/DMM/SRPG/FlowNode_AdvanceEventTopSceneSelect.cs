// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AdvanceEventTopSceneSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Advance/EventTopSceneSelect", 32741)]
  [FlowNode.Pin(1, "Select", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "Top", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(21, "Stage", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(22, "Boss", FlowNode.PinTypes.Output, 22)]
  [FlowNode.Pin(101, "OutOfPeriod", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(111, "KeyClosed", FlowNode.PinTypes.Output, 111)]
  public class FlowNode_AdvanceEventTopSceneSelect : FlowNode
  {
    private const int PIN_IN = 1;
    private const int PIN_OUT_TOP = 11;
    private const int PIN_OUT_STAGE = 21;
    private const int PIN_OUT_BOSS = 22;
    private const int PIN_OUT_QUEST_OUT_OF_PERIOD = 101;
    private const int PIN_OUT_QUEST_KEY_CLOSED = 111;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      AdvanceEventManager.Instance.SetRestorePointIsStage(false);
      AdvanceEventManager.Instance.SetJumpFromMission(false);
      RestorePoints restorePoint = HomeWindow.GetRestorePoint();
      HomeWindow.SetRestorePoint(RestorePoints.Home);
      if (restorePoint != RestorePoints.AdvanceStage && restorePoint != RestorePoints.AdvanceBoss)
      {
        this.ToQuest(MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID), false);
      }
      else
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.LastPlayedQuest.Get());
        long serverTime = Network.GetServerTime();
        if (quest.Chapter.IsKeyQuest() && !quest.Chapter.IsKeyUnlock(serverTime))
          this.ActivateOutputLinks(111);
        else if (!quest.Chapter.IsDateUnlock(serverTime))
          this.ActivateOutputLinks(101);
        else
          this.ToQuest(quest, true);
      }
    }

    private void ToQuest(QuestParam quest, bool is_restore)
    {
      if (quest == null)
      {
        this.ToTop();
      }
      else
      {
        AdvanceEventParam eventParamFromAreaId = MonoSingleton<GameManager>.Instance.GetAdvanceEventParamFromAreaId(quest.ChapterID);
        if (eventParamFromAreaId == null)
        {
          this.ToTop();
        }
        else
        {
          AdvanceManager.CurrentEventParam = eventParamFromAreaId;
          AdvanceManager.CurrentChapterParam = quest.Chapter;
          AdvanceEventManager.Instance.SetCurrentEventParam(eventParamFromAreaId);
          if (quest.type == QuestTypes.AdvanceStory)
          {
            if (is_restore)
              AdvanceEventManager.Instance.SetRestorePointIsStage(true);
            else
              AdvanceEventManager.Instance.SetJumpFromMission(true);
            AdvanceEventManager.Instance.SetStageDifficulty(quest.difficulty);
            this.ActivateOutputLinks(21);
          }
          else if (quest.type == QuestTypes.AdvanceBoss)
          {
            AdvanceEventManager.Instance.SetBossDifficulty(quest.difficulty);
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
