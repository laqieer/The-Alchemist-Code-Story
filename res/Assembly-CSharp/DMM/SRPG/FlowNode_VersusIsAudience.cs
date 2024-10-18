// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_VersusIsAudience
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/Versus/IsAudience", 32741)]
  [FlowNode.Pin(0, "Check", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(100, "Yes", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(200, "No", FlowNode.PinTypes.Output, 3)]
  public class FlowNode_VersusIsAudience : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (MonoSingleton<GameManager>.Instance.AudienceMode)
        this.ActivateOutputLinks(100);
      else
        this.ActivateOutputLinks(200);
    }
  }
}
