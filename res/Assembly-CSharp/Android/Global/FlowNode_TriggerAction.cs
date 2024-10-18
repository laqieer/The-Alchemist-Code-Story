// Decompiled with JetBrains decompiler
// Type: FlowNode_TriggerAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;

[FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
[FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
[FlowNode.ShowInInspector]
[FlowNode.NodeType("Event/TriggerAction", 32741)]
public class FlowNode_TriggerAction : FlowNode
{
  [SerializeField]
  public UnityEvent Action;

  public override void OnActivate(int pinID)
  {
    if (pinID != 0)
      return;
    this.Action.Invoke();
    this.ActivateOutputLinks(1);
  }
}
