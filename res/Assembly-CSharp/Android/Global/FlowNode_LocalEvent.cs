// Decompiled with JetBrains decompiler
// Type: FlowNode_LocalEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("")]
[FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 0)]
[FlowNode.NodeType("Event/LocalEvent", 58751)]
public class FlowNode_LocalEvent : FlowNode
{
  [FlowNode.ShowInInfo]
  public string EventName;

  public override void OnActivate(int pinID)
  {
    this.ActivateOutputLinks(1);
  }
}
