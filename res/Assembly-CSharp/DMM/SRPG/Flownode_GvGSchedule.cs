// Decompiled with JetBrains decompiler
// Type: SRPG.Flownode_GvGSchedule
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("GvG/GvGOpen", 32741)]
  [FlowNode.Pin(1, "Start Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "GvG Open", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "GvG Close", FlowNode.PinTypes.Output, 102)]
  public class Flownode_GvGSchedule : FlowNode
  {
    private const int PIN_INPUT_CHECK = 1;
    private const int PIN_OUTPUT_OPEN = 101;
    private const int PIN_OUTPUT_CLOSE = 102;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (GvGPeriodParam.GetGvGPeriod() != null)
        this.ActivateOutputLinks(101);
      else
        this.ActivateOutputLinks(102);
    }
  }
}
