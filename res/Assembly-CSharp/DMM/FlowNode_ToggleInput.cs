// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleInput
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;

#nullable disable
[FlowNode.NodeType("Toggle/Input", 32741)]
[FlowNode.Pin(10, "Enable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(11, "Disable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(12, "Reset", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 0)]
public class FlowNode_ToggleInput : FlowNode
{
  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 10:
        SRPG_TouchInputModule.LockInput();
        break;
      case 11:
        SRPG_TouchInputModule.UnlockInput();
        break;
      case 12:
        SRPG_TouchInputModule.UnlockInput(true);
        break;
    }
    this.ActivateOutputLinks(1);
  }
}
