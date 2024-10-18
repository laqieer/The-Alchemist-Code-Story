// Decompiled with JetBrains decompiler
// Type: FlowNode_OnMailSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("")]
[FlowNode.NodeType("Event/OnMailSelect", 58751)]
[FlowNode.Pin(1, "Selected", FlowNode.PinTypes.Output, 0)]
public class FlowNode_OnMailSelect : FlowNodePersistent
{
  public void Selected() => this.ActivateOutputLinks(1);
}
