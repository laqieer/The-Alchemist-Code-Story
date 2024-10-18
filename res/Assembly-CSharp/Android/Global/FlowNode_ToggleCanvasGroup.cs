// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleCanvasGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Toggle/CanvasGroup", 32741)]
[FlowNode.Pin(6, "Turn Off interactive", FlowNode.PinTypes.Input, 6)]
[FlowNode.Pin(5, "Turn On interactive", FlowNode.PinTypes.Input, 5)]
[FlowNode.Pin(4, "Hide", FlowNode.PinTypes.Input, 4)]
[FlowNode.Pin(3, "Show", FlowNode.PinTypes.Input, 3)]
[FlowNode.Pin(2, "Turn Off Block Raycasts", FlowNode.PinTypes.Input, 2)]
[FlowNode.Pin(1, "Turn On Block Raycasts", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(0, "Out", FlowNode.PinTypes.Output, 999)]
public class FlowNode_ToggleCanvasGroup : FlowNode
{
  [FlowNode.DropTarget(typeof (CanvasGroup), true)]
  [FlowNode.ShowInInfo]
  public CanvasGroup Target;

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 1:
        if ((Object) this.Target != (Object) null)
          this.Target.blocksRaycasts = true;
        this.ActivateOutputLinks(0);
        break;
      case 2:
        if ((Object) this.Target != (Object) null)
          this.Target.blocksRaycasts = false;
        this.ActivateOutputLinks(0);
        break;
      case 3:
        if ((Object) this.Target != (Object) null)
          this.Target.alpha = 1f;
        this.ActivateOutputLinks(0);
        break;
      case 4:
        if ((Object) this.Target != (Object) null)
          this.Target.alpha = 0.0f;
        this.ActivateOutputLinks(0);
        break;
      case 5:
        if ((Object) this.Target != (Object) null)
          this.Target.interactable = true;
        this.ActivateOutputLinks(0);
        break;
      case 6:
        if ((Object) this.Target != (Object) null)
          this.Target.interactable = false;
        this.ActivateOutputLinks(0);
        break;
    }
  }
}
