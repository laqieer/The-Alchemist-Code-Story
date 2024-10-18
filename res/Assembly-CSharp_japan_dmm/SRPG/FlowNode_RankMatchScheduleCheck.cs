﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RankMatchScheduleCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/RankMatchScheduleCheck", 32741)]
  [FlowNode.Pin(1, "Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Aggregating", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "Rewarding", FlowNode.PinTypes.Output, 12)]
  public class FlowNode_RankMatchScheduleCheck : FlowNode
  {
    private const int PIN_IN_CHECK = 1;
    private const int PIN_OUT_OPEN = 10;
    private const int PIN_OUT_AGGREGATING = 11;
    private const int PIN_OUT_REWARDING = 12;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.RankMatchScheduleId > 0)
        this.ActivateOutputLinks(10);
      else if (instance.RankMatchRankingStatus == ReqRankMatchStatus.RankingStatus.Aggregating)
        this.ActivateOutputLinks(11);
      else
        this.ActivateOutputLinks(12);
    }
  }
}
