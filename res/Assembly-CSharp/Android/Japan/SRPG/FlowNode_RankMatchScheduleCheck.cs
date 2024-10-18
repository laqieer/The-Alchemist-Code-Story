// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RankMatchScheduleCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

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
