﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DestroyCanvasAll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("UI/DestroyCanvasAll", 32741)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_DestroyCanvasAll : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      UIUtility.PopCanvasAll();
      this.ActivateOutputLinks(1);
    }
  }
}
