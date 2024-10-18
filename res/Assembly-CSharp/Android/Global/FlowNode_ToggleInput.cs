// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleInput
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using SRPG;

[FlowNode.Pin(10, "Enable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(11, "Disable", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Toggle/Input", 32741)]
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
    }
    this.ActivateOutputLinks(1);
  }
}
