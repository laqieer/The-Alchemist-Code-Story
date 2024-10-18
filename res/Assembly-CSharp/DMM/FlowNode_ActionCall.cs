// Decompiled with JetBrains decompiler
// Type: FlowNode_ActionCall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;

#nullable disable
[FlowNode.NodeType("UI/ActionCall", 32741)]
[FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
public class FlowNode_ActionCall : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject Target;
  public ActionCall.EventType EventType;
  public SerializeValueList Value;

  public override void OnActivate(int pinID)
  {
    ActionCall component = (!Object.op_Inequality((Object) this.Target, (Object) null) ? ((Component) this).gameObject : this.Target).GetComponent<ActionCall>();
    if (Object.op_Inequality((Object) component, (Object) null))
      component.Invoke(this.EventType, this.Value);
    this.ActivateOutputLinks(1);
  }
}
