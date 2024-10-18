// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckPremiumUser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Battle/Speed/BattleSpeedEditorOption", 32741)]
  [FlowNode.Pin(1, "Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Premium", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(20, "Not Premium", FlowNode.PinTypes.Output, 3)]
  public class FlowNode_CheckPremiumUser : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID == 1)
        this.ActivateOutputLinks(10);
      else
        this.ActivateOutputLinks(20);
    }
  }
}
