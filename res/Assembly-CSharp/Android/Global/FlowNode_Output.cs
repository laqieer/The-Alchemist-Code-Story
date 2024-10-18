// Decompiled with JetBrains decompiler
// Type: FlowNode_Output
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

[FlowNode.Pin(1, "", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Event/Output", 32741)]
public class FlowNode_Output : FlowNode
{
  public string PinName;
  [NonSerialized]
  public FlowNode_ExternalLink TargetNode;
  [NonSerialized]
  public int TargetPinID;

  public override string GetCaption()
  {
    return base.GetCaption() + ":" + this.PinName;
  }

  public override void OnActivate(int pinID)
  {
    if (!((UnityEngine.Object) this.TargetNode != (UnityEngine.Object) null))
      return;
    this.TargetNode.ActivateOutputLinks(this.TargetPinID);
  }
}
