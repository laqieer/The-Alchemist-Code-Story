﻿// Decompiled with JetBrains decompiler
// Type: FlowNode_ActionCall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

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
    ActionCall component = (!((UnityEngine.Object) this.Target != (UnityEngine.Object) null) ? this.gameObject : this.Target).GetComponent<ActionCall>();
    if ((UnityEngine.Object) component != (UnityEngine.Object) null)
      component.Invoke(this.EventType, this.Value);
    this.ActivateOutputLinks(1);
  }
}
