// Decompiled with JetBrains decompiler
// Type: FlowNode_Log
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Debug/Log", 32741)]
[FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
public class FlowNode_Log : FlowNode
{
  public string Value;

  public override void OnActivate(int pinID)
  {
    Debug.Log((object) this.Value);
    this.ActivateOutputLinks(1);
  }
}
