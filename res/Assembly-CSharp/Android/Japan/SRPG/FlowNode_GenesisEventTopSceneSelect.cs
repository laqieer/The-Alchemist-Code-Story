// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GenesisEventTopSceneSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

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
      RestorePoints restorePoint = HomeWindow.GetRestorePoint();
      switch (restorePoint)
      {
        case RestorePoints.GenesisStage:
        case RestorePoints.GenesisBoss:
          QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.LastPlayedQuest.Get());
          if (quest == null)
          {
            this.ToTop();
            break;
          }
          GenesisChapterParam chapterParamFromAreaId = MonoSingleton<GameManager>.Instance.GetGenesisChapterParamFromAreaId(quest.ChapterID);
          if (chapterParamFromAreaId == null)
          {
            this.ToTop();
            break;
          }
          GenesisManager.CurrentChapterParam = chapterParamFromAreaId;
          GenesisChapterManager.Instance.SetCurrentChapterParam(chapterParamFromAreaId);
          HomeWindow.SetRestorePoint(RestorePoints.Home);
          if (restorePoint == RestorePoints.GenesisStage && quest.type == QuestTypes.GenesisStory)
          {
            GenesisChapterManager.Instance.SetStageDifficulty(quest.difficulty);
            GenesisChapterManager.Instance.SetRestorePointIsStage(true);
            this.ActivateOutputLinks(21);
            break;
          }
          if (restorePoint != RestorePoints.GenesisBoss || quest.type != QuestTypes.GenesisBoss)
            break;
          GenesisChapterManager.Instance.SetBossDifficulty(quest.difficulty);
          this.ActivateOutputLinks(22);
          break;
        default:
          this.ToTop();
          break;
      }
    }

    private void ToTop()
    {
      this.ActivateOutputLinks(11);
    }
  }
}
