﻿// Decompiled with JetBrains decompiler
// Type: FlowNode_Count
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

[FlowNode.NodeType("Count", 32741)]
[FlowNode.Pin(100, "Finished", FlowNode.PinTypes.Output, 100)]
[FlowNode.Pin(2, "Reset", FlowNode.PinTypes.Input, 2)]
[FlowNode.Pin(1, "Count Up", FlowNode.PinTypes.Input, 1)]
public class FlowNode_Count : FlowNode
{
  public int Count = 1;
  private int mCount;
  public bool AutoReset;

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 1:
        ++this.mCount;
        if (this.mCount != this.Count)
          break;
        if (this.AutoReset)
          this.mCount = 0;
        this.ActivateOutputLinks(100);
        break;
      case 2:
        this.mCount = 0;
        break;
    }
  }
}
