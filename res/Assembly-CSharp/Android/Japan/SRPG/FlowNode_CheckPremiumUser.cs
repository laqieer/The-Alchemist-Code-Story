// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckPremiumUser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
