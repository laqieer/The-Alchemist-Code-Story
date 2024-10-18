// Decompiled with JetBrains decompiler
// Type: FlowNode_Log
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
[FlowNode.NodeType("Debug/Log", 32741)]
public class FlowNode_Log : FlowNode
{
  public string Value;

  public override void OnActivate(int pinID)
  {
    Debug.Log((object) this.Value);
    this.ActivateOutputLinks(1);
  }
}
