// Decompiled with JetBrains decompiler
// Type: FlowNode_ResetGuerrillaShopStartedFlag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using SRPG;

[FlowNode.NodeType("System/Shop/ResetGuerrillaShopStartedFlag", 32741)]
[FlowNode.Pin(0, "Reset", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1000, "Exit", FlowNode.PinTypes.Output, 1000)]
public class FlowNode_ResetGuerrillaShopStartedFlag : FlowNode
{
  public override void OnActivate(int pinID)
  {
    if (pinID != 0)
      return;
    MonoSingleton<GameManager>.Instance.Player.IsGuerrillaShopStarted = false;
    this.ActivateOutputLinks(1000);
  }
}
