// Decompiled with JetBrains decompiler
// Type: FlowNode_ActionCall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

[FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
[FlowNode.NodeType("UI/ActionCall", 32741)]
public class FlowNode_ActionCall : FlowNode
{
  [FlowNode.DropTarget(typeof (GameObject), true)]
  [FlowNode.ShowInInfo]
  public GameObject Target;
  public ActionCall.EventType EventType;
  public SerializeValueList Value;

  public override void OnActivate(int pinID)
  {
    ActionCall component = (!((UnityEngine.Object) this.Target != (UnityEngine.Object) null) ? this.gameObject : this.Target).GetComponent<ActionCall>();
    if ((UnityEngine.Object) component != (UnityEngine.Object) null)
      component.Invoke(this.EventType, this.Value);
    this.ActivateOutputLinks(1);
  }
}
