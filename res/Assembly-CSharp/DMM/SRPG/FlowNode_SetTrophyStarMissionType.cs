// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetTrophyStarMissionType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Trophy/StarMission/Set/Type", 32741)]
  [FlowNode.Pin(1, "Set Daily", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Set Weekly", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(101, "Done", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_SetTrophyStarMissionType : FlowNode
  {
    private const int PIN_IN_SET_DAILY = 1;
    private const int PIN_IN_SET_WEEKLY = 2;
    private const int PIN_OUT_DONE = 101;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 1:
          TrophyStarMissionParam.SelectStarMissionType = TrophyStarMissionParam.eStarMissionType.DAILY;
          break;
        case 2:
          TrophyStarMissionParam.SelectStarMissionType = TrophyStarMissionParam.eStarMissionType.WEEKLY;
          break;
      }
      this.ActivateOutputLinks(101);
    }
  }
}
