// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayRankMatchCheckBP
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(11, "Disable", FlowNode.PinTypes.Output, 11)]
  [FlowNode.ShowInInspector]
  [FlowNode.NodeType("Multi/MultiPlayRankMatchCheckBP", 32741)]
  [FlowNode.Pin(0, "Check", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Enable", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_MultiPlayRankMatchCheckBP : FlowNode
  {
    private const int PIN_IN_CHECK = 0;
    private const int PIN_OUT_ENABLE = 10;
    private const int PIN_OUT_DISABLE = 11;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ActivateOutputLinks(10);
    }
  }
}
