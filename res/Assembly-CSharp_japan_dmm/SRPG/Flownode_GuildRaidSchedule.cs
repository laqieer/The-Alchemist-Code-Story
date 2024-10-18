// Decompiled with JetBrains decompiler
// Type: SRPG.Flownode_GuildRaidSchedule
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("GuildRaid/GuildRaidSchedule", 32741)]
  [FlowNode.Pin(1, "Set", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "GuildRaid Open", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(102, "GuildRaid OpenSchedule", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(103, "GuildRaid Close", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(104, "GuildRaid CloseShedule", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(900, "Error", FlowNode.PinTypes.Output, 100)]
  public class Flownode_GuildRaidSchedule : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      switch (MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodScheduleType())
      {
        case GuildRaidManager.GuildRaidScheduleType.Open:
          this.ActivateOutputLinks(101);
          break;
        case GuildRaidManager.GuildRaidScheduleType.Close:
          this.ActivateOutputLinks(103);
          break;
        case GuildRaidManager.GuildRaidScheduleType.OpenSchedule:
          this.ActivateOutputLinks(102);
          break;
        case GuildRaidManager.GuildRaidScheduleType.CloseSchedule:
          this.ActivateOutputLinks(104);
          break;
        default:
          this.ActivateOutputLinks(900);
          break;
      }
    }
  }
}
