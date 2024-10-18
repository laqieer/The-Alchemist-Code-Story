﻿// Decompiled with JetBrains decompiler
// Type: FlowNode_ExecuteScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
[FlowNode.NodeType("Script", 32741)]
[FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
public class FlowNode_ExecuteScript : FlowNode
{
  public string ScriptName = string.Empty;

  public override string GetCaption()
  {
    return base.GetCaption() + ":" + this.ScriptName;
  }

  public override void OnActivate(int pinID)
  {
    this.ActivateOutputLinks(1);
  }
}
