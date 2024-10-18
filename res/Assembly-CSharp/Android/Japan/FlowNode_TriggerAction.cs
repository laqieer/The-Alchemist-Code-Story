// Decompiled with JetBrains decompiler
// Type: FlowNode_TriggerAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;

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
