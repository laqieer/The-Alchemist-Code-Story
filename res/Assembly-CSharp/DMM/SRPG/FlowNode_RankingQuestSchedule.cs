// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RankingQuestSchedule
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Quest/RankingQuestSchedule")]
  [FlowNode.Pin(100, "開催期間中のランキングクエストはあるか？", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "開催期間中のランキングクエスト（有）", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "開催期間中のランキングクエスト（無）", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_RankingQuestSchedule : FlowNode
  {
    public const int INPUT_EXIST_OPEN_RANKING_SCHEDULE = 100;
    public const int OUTPUT_EXIST_OPEN_RANKING_SCHEDULE_TRUE = 101;
    public const int OUTPUT_EXIST_OPEN_RANKING_SCHEDULE_FALSE = 102;

    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      List<RankingQuestParam> rankingQuestParamList = RankingQuestScheduleParam.FilterDuplicateRankingQuestIDs(RankingQuestScheduleParam.FindRankingQuestParamBySchedule(RankingQuestScheduleParam.RakingQuestScheduleGetFlags.Open | RankingQuestScheduleParam.RakingQuestScheduleGetFlags.Visible));
      MonoSingleton<GameManager>.Instance.SetAvailableRankingQuestParams(rankingQuestParamList);
      if (rankingQuestParamList.Count > 0)
        this.ActivateOutputLinks(101);
      else
        this.ActivateOutputLinks(102);
    }
  }
}
