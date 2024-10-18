﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_FgGStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(3, "未連携", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(4, "連携済み", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(1, "Input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("FgGID/FgGStatus", 32741)]
  [FlowNode.Pin(2, "非表示", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_FgGStatus : FlowNode
  {
    private const int PIN_ID_INPUT = 1;
    private const int PIN_ID_DISABLE = 2;
    private const int PIN_ID_NOT_SYNCHRONIZED = 3;
    private const int PIN_ID_SYNCHRONIZED = 4;

    public override void OnActivate(int pinID)
    {
      switch (MonoSingleton<GameManager>.Instance.AuthStatus)
      {
        case ReqFgGAuth.eAuthStatus.Disable:
          this.ActivateOutputLinks(2);
          break;
        case ReqFgGAuth.eAuthStatus.NotSynchronized:
          this.ActivateOutputLinks(3);
          break;
        case ReqFgGAuth.eAuthStatus.Synchronized:
          this.ActivateOutputLinks(4);
          break;
        default:
          this.ActivateOutputLinks(2);
          break;
      }
    }
  }
}
