// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_UpdateBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("UI/UpdateBadge", 32741)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_UpdateBadge : FlowNode
  {
    public GameManager.BadgeTypes type;

    public override void OnActivate(int pinID)
    {
      if (pinID == 1)
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(this.type);
      this.ActivateOutputLinks(10);
    }
  }
}
