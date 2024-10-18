// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckPeriodDrawCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("DrawCard/CheckPeriod", 32741)]
  [FlowNode.Pin(1, "Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "True", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "False", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_CheckPeriodDrawCard : FlowNode
  {
    private const int PIN_IN_CHECK = 1;
    private const int PIN_OUT_TRUE = 10;
    private const int PIN_OUT_FALSE = 11;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (DrawCardParam.DrawCardEnabled)
        this.ActivateOutputLinks(10);
      else
        this.ActivateOutputLinks(11);
    }
  }
}
