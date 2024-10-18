// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SkipFriendSupport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(100, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("SRPG/SkipFriendSupport", 32741)]
  public class FlowNode_SkipFriendSupport : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      GlobalVars.SelectedSupport.Set((SupportData) null);
      this.ActivateOutputLinks(1);
    }
  }
}
