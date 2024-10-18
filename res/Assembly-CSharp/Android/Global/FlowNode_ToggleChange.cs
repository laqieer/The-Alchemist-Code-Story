// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleChange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[FlowNode.Pin(2, "disable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "enable", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Event/ToggleChange", 58751)]
[AddComponentMenu("")]
public class FlowNode_ToggleChange : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (Toggle), true)]
  public Toggle Target;

  public override void OnActivate(int pinID)
  {
    if (!((Object) this.Target != (Object) null))
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
