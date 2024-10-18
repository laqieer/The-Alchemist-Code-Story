// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckUseDLC
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("SRPG/CheckUseDLC", 32741)]
  [FlowNode.Pin(101, "Check", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(1001, "True", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(1002, "False", FlowNode.PinTypes.Output, 1002)]
  public class FlowNode_CheckUseDLC : FlowNode
  {
    private const int PIN_INPUT_START_CHECK = 101;
    private const int PIN_OUTPUT_TRUE = 1001;
    private const int PIN_OUTPUT_FALSE = 1002;

    public override void OnActivate(int pinID)
    {
      if (pinID != 101)
        return;
      this.Check();
    }

    private void Check()
    {
      if (AssetManager.UseDLC)
        this.ActivateOutputLinks(1001);
      else
        this.ActivateOutputLinks(1002);
    }
  }
}
