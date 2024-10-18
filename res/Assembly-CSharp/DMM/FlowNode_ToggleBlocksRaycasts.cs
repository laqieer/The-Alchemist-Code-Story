// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleBlocksRaycasts
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[FlowNode.NodeType("Toggle/BlocksRaycasts", 32741)]
[FlowNode.Pin(10, "Disable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(11, "Enable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 0)]
public class FlowNode_ToggleBlocksRaycasts : FlowNodePersistent
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject Target;

  public override void OnActivate(int pinID)
  {
    CanvasGroup component = !Object.op_Inequality((Object) this.Target, (Object) null) ? (CanvasGroup) null : this.Target.GetComponent<CanvasGroup>();
    switch (pinID)
    {
      case 10:
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.blocksRaycasts = false;
          break;
        }
        break;
      case 11:
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.blocksRaycasts = true;
          break;
        }
        break;
    }
    this.ActivateOutputLinks(1);
  }
}
