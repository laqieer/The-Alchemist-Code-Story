// Decompiled with JetBrains decompiler
// Type: FlowNode_LocalEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("")]
[FlowNode.NodeType("Event/LocalEvent", 58751)]
[FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 0)]
public class FlowNode_LocalEvent : FlowNode
{
  [FlowNode.ShowInInfo]
  public string EventName;

  public override void OnActivate(int pinID)
  {
    this.ActivateOutputLinks(1);
  }
}
