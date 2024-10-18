// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleCanvas
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Toggle/Canvas", 32741)]
[FlowNode.Pin(0, "Out", FlowNode.PinTypes.Output, 999)]
[FlowNode.Pin(1, "Turn On", FlowNode.PinTypes.Input, 5)]
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
