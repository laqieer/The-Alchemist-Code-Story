// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleChange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
[AddComponentMenu("")]
[FlowNode.NodeType("Event/ToggleChange", 58751)]
[FlowNode.Pin(1, "enable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(2, "disable", FlowNode.PinTypes.Input, 0)]
public class FlowNode_ToggleChange : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (Toggle), true)]
  public Toggle Target;

  public override void OnActivate(int pinID)
  {
    if (!Object.op_Inequality((Object) this.Target, (Object) null))
      return;
    if (pinID == 1)
    {
      this.Target.isOn = true;
    }
    else
    {
      if (pinID != 2)
        return;
      this.Target.isOn = false;
    }
  }
}
