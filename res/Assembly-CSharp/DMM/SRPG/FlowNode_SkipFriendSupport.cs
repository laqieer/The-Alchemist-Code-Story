// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SkipFriendSupport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("SRPG/SkipFriendSupport", 32741)]
  [FlowNode.Pin(100, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
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
