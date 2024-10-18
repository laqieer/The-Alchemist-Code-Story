// Decompiled with JetBrains decompiler
// Type: FlowNode_EventCall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;

#nullable disable
[FlowNode.NodeType("Event/Call", 32741)]
[FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
public class FlowNode_EventCall : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject Target;
  [FlowNode.ShowInInfo]
  public string Key = string.Empty;
  public string Value = string.Empty;

  public override void OnActivate(int pinID)
  {
    EventCall component = (!Object.op_Inequality((Object) this.Target, (Object) null) ? ((Component) this).gameObject : this.Target).GetComponent<EventCall>();
    if (Object.op_Inequality((Object) component, (Object) null))
      component.Invoke(this.Key, this.Value);
    this.ActivateOutputLinks(1);
  }
}
