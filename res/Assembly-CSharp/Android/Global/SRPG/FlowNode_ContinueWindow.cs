﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ContinueWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("UI/ContinueWindow", 32741)]
  [FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(11, "ForceClose", FlowNode.PinTypes.Input, 11)]
  public class FlowNode_ContinueWindow : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 10:
          if (!ContinueWindow.Create(SceneBattle.Instance.continueWindowRes, new ContinueWindow.ResultEvent(this.OnDecide), new ContinueWindow.ResultEvent(this.OnCancel)))
          {
            this.OnCancel((GameObject) null);
            break;
          }
          this.ActivateOutputLinks(100);
          break;
        case 11:
          ContinueWindow.ForceClose();
          this.ActivateOutputLinks(101);
          break;
      }
    }

    private void OnDecide(GameObject dialog)
    {
      this.ActivateOutputLinks(1);
    }

    private void OnCancel(GameObject dialog)
    {
      this.ActivateOutputLinks(2);
    }
  }
}
