// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleGameObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[FlowNode.NodeType("Toggle/GameObject", 32741)]
[FlowNode.Pin(10, "Enable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(11, "Disable", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 2)]
public class FlowNode_ToggleGameObject : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject Target;

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 10:
        if (Object.op_Inequality((Object) this.Target, (Object) null))
        {
          this.Target.SetActive(true);
          break;
        }
        break;
      case 11:
        if (Object.op_Inequality((Object) this.Target, (Object) null))
        {
          this.Target.SetActive(false);
          break;
        }
        break;
    }
    this.ActivateOutputLinks(1);
  }
}
