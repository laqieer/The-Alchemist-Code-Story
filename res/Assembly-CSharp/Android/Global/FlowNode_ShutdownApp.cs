// Decompiled with JetBrains decompiler
// Type: FlowNode_ShutdownApp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(1, "Shutdown", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("System/Shutdown Application")]
public class FlowNode_ShutdownApp : FlowNode
{
  public override void OnActivate(int pinID)
  {
    if (pinID != 1)
      return;
    Application.Quit();
  }
}
