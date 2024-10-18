// Decompiled with JetBrains decompiler
// Type: FlowNode_ResetGuerrillaShopStartedFlag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;

#nullable disable
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
