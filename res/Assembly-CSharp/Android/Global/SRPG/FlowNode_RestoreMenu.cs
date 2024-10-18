// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RestoreMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("System/Set Restore Menu", 32741)]
  [FlowNode.Pin(10, "Out", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "Set", FlowNode.PinTypes.Input, 1)]
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
