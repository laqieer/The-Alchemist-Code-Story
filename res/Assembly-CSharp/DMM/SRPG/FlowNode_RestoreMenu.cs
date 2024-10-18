// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RestoreMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
