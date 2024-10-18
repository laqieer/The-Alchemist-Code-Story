// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RaidPeriodTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Raid/RaidPeriodTime", 32741)]
  [FlowNode.Pin(10, "Start", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1000, "RAID指定なしで開催中", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "RAID期間終了", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(1002, "RAIDスケジュール指定で開催中", FlowNode.PinTypes.Output, 1002)]
  [FlowNode.Pin(1003, "RAIDスケジュール指定で停止中", FlowNode.PinTypes.Output, 1003)]
  public class FlowNode_RaidPeriodTime : FlowNode
  {
    private const int INPUT_START = 10;
    private const int OUTPUT_RAIDTIMEOPEN = 1000;
    private const int OUTPUT_RAIDTIMECLOSE = 1001;
    private const int OUTPUT_RAIDTIMEOPENSCHEDULE = 1002;
    private const int OUTPUT_RAIDTIMECLOSESCHEDULE = 1003;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      switch (MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus())
      {
        case RaidManager.RaidScheduleType.Open:
          this.ActivateOutputLinks(1000);
          break;
        case RaidManager.RaidScheduleType.Close:
          this.ActivateOutputLinks(1001);
          break;
        case RaidManager.RaidScheduleType.OpenSchedule:
          this.ActivateOutputLinks(1002);
          break;
        case RaidManager.RaidScheduleType.CloseSchedule:
          this.ActivateOutputLinks(1003);
          break;
      }
    }
  }
}
