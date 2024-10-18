// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_VersusIsAudience
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

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
