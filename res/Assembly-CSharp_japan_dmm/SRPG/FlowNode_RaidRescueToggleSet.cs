// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RaidRescueToggleSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Raid/RaidRescueToggleSet", 32741)]
  [FlowNode.Pin(0, "input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Not Join Guild", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Friend Empty", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_RaidRescueToggleSet : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null || !MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined)
        this.ActivateOutputLinks(10);
      if (MonoSingleton<GameManager>.Instance.Player.FriendNum != 0)
        return;
      this.ActivateOutputLinks(11);
    }
  }
}
