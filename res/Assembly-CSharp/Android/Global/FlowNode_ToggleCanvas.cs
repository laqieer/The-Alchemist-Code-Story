// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleCanvas
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(1, "Turn On", FlowNode.PinTypes.Input, 5)]
[FlowNode.Pin(0, "Out", FlowNode.PinTypes.Output, 999)]
[FlowNode.NodeType("Toggle/Canvas", 32741)]
[FlowNode.Pin(2, "Turn Off", FlowNode.PinTypes.Input, 6)]
public class FlowNode_ToggleCanvas : FlowNode
{
  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 1:
        Canvas component1 = this.GetComponent<Canvas>();
        if (!((Object) component1 != (Object) null))
          break;
        component1.enabled = true;
        if (!((Object) component1.GetComponent<CanvasStack>() != (Object) null))
          break;
        CanvasStack.SortCanvases();
        break;
      case 2:
        Canvas component2 = this.GetComponent<Canvas>();
        if (!((Object) component2 != (Object) null))
          break;
        component2.enabled = false;
        if (!((Object) component2.GetComponent<CanvasStack>() != (Object) null))
          break;
        CanvasStack.SortCanvases();
        break;
    }
  }
}
