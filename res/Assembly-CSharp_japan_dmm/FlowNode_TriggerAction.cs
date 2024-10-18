// Decompiled with JetBrains decompiler
// Type: FlowNode_TriggerAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;

#nullable disable
[FlowNode.NodeType("Event/TriggerAction", 32741)]
[FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
[FlowNode.ShowInInspector]
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
