// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayRankMatchCheckBP
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayRankMatchCheckBP", 32741)]
  [FlowNode.Pin(0, "Check", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Enable", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Disable", FlowNode.PinTypes.Output, 11)]
  [FlowNode.ShowInInspector]
  public class FlowNode_MultiPlayRankMatchCheckBP : FlowNode
  {
    private const int PIN_IN_CHECK = 0;
    private const int PIN_OUT_ENABLE = 10;
    private const int PIN_OUT_DISABLE = 11;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (MonoSingleton<GameManager>.Instance.Player.RankMatchBattlePoint > 0)
        this.ActivateOutputLinks(10);
      else
        this.ActivateOutputLinks(11);
    }
  }
}
