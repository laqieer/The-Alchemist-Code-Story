// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleCanvas
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
        Canvas component1 = ((Component) this).GetComponent<Canvas>();
        if (!Object.op_Inequality((Object) component1, (Object) null))
          break;
        ((Behaviour) component1).enabled = true;
        if (!Object.op_Inequality((Object) ((Component) component1).GetComponent<CanvasStack>(), (Object) null))
          break;
        CanvasStack.SortCanvases();
        break;
      case 2:
        Canvas component2 = ((Component) this).GetComponent<Canvas>();
        if (!Object.op_Inequality((Object) component2, (Object) null))
          break;
        ((Behaviour) component2).enabled = false;
        if (!Object.op_Inequality((Object) ((Component) component2).GetComponent<CanvasStack>(), (Object) null))
          break;
        CanvasStack.SortCanvases();
        break;
    }
  }
}
