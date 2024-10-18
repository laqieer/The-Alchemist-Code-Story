﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RestoreMenuVariable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Set", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Assigned", FlowNode.PinTypes.Output, 9)]
  [FlowNode.Pin(11, "== Variable", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(12, "!= Variable", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(1, "Compare", FlowNode.PinTypes.Input, 2)]
  [FlowNode.NodeType("System/RestoreMenuVariable", 32741)]
  public class FlowNode_RestoreMenuVariable : FlowNode
  {
    private const int PIN_ID_SET = 0;
    private const int PIN_ID_COMPARE = 1;
    private const int PIN_ID_ASSIGNED = 10;
    private const int PIN_ID_EQUAL = 11;
    private const int PIN_ID_UNEQUAL = 12;
    public RestorePoints RestorePoint;
    [HideInInspector]
    public RestorePoints ResetRestorePoint;
    [HideInInspector]
    public bool AutoReset;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          HomeWindow.SetRestorePoint(this.RestorePoint);
          this.ActivateOutputLinks(10);
          break;
        case 1:
          if (HomeWindow.GetRestorePoint() == this.RestorePoint)
            this.ActivateOutputLinks(11);
          else
            this.ActivateOutputLinks(12);
          if (!this.AutoReset)
            break;
          HomeWindow.SetRestorePoint(this.ResetRestorePoint);
          break;
      }
    }
  }
}
