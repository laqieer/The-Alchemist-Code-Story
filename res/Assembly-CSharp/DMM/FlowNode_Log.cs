// Decompiled with JetBrains decompiler
// Type: FlowNode_Log
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
