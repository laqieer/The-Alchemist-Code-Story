// Decompiled with JetBrains decompiler
// Type: FlowNode_Output
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[FlowNode.NodeType("Event/Output", 32741)]
[FlowNode.Pin(1, "", FlowNode.PinTypes.Input, 0)]
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
    if (!((Object) this.TargetNode != (Object) null))
      return;
    this.TargetNode.ActivateOutputLinks(this.TargetPinID);
  }
}
