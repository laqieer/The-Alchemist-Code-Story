// Decompiled with JetBrains decompiler
// Type: FlowNode_IsFgGBuy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
[FlowNode.NodeType("BuyCoin/IsFgGBuy", 32741)]
[FlowNode.Pin(0, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(10, "OK", FlowNode.PinTypes.Output, 10)]
[FlowNode.Pin(11, "NG", FlowNode.PinTypes.Output, 11)]
public class FlowNode_IsFgGBuy : FlowNode
{
  private const int PIN_INPUT = 0;
  private const int PIN_OUTPUT_OK = 10;
  private const int PIN_OUTPUT_NG = 11;

  public override void OnActivate(int pinID)
  {
    if (pinID != 0)
      return;
    if (FlowNode_IsFgGBuy.IsFgGBuy())
      this.ActivateOutputLinks(10);
    else
      this.ActivateOutputLinks(11);
  }

  public static bool IsFgGBuy() => false;
}
