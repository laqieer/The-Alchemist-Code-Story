﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RestoreMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Scene/SetRestoreMenu", 32741)]
  [FlowNode.Pin(1, "Set", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Out", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_RestoreMenu : FlowNode
  {
    [FlowNode.ShowInInfo]
    public RestorePoints RestorePoint;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      HomeWindow.SetRestorePoint(this.RestorePoint);
      this.ActivateOutputLinks(10);
    }
  }
}
