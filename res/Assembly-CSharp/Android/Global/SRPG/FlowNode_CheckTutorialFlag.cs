﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckTutorialFlag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(2, "True", FlowNode.PinTypes.Output, 0)]
  [FlowNode.NodeType("Tutorial/CheckTutorialFlag", 32741)]
  [FlowNode.Pin(1, "CheckFlag", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(3, "False", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_CheckTutorialFlag : FlowNode
  {
    private const int PIN_ID_IN = 1;
    private const int PIN_ID_TRUE = 2;
    private const int PIN_ID_FALSE = 3;
    public TutorialFlags mTutorialFlags;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (this.CheckFlag(this.mTutorialFlags))
        this.ActivateOutputLinks(2);
      else
        this.ActivateOutputLinks(3);
    }

    private bool CheckFlag(TutorialFlags flag)
    {
      return (MonoSingleton<GameManager>.Instance.Player.TutorialFlags & (long) flag) != 0L;
    }
  }
}
