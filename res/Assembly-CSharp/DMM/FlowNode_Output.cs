// Decompiled with JetBrains decompiler
// Type: FlowNode_Output
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
[FlowNode.NodeType("Event/Output", 32741)]
[FlowNode.Pin(1, "", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(10, "", FlowNode.PinTypes.Output, 10)]
public class FlowNode_Output : FlowNode
{
  public string PinName;
  [NonSerialized]
  public FlowNode_ExternalLink TargetNode;
  [NonSerialized]
  public int TargetPinID;

  public override string GetCaption() => base.GetCaption() + ":" + this.PinName;

  public override void OnActivate(int pinID)
  {
    if (Object.op_Inequality((Object) this.TargetNode, (Object) null))
      this.TargetNode.ActivateOutputLinks(this.TargetPinID);
    this.ActivateOutputLinks(10);
  }
}
