// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleInput
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;

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
        SRPG_TouchInputModule.UnlockInput(false);
        break;
      case 12:
        SRPG_TouchInputModule.UnlockInput(true);
        break;
    }
    this.ActivateOutputLinks(1);
  }
}
