// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RaidStampRally
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Raid/RaidStampRally", 32741)]
  [FlowNode.Pin(2, "スタンプラリーBOSS数検索", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(100, "3体以下", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "4体", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "5体", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "6体", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "7体", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(200, "エラー", FlowNode.PinTypes.Output, 200)]
  public class FlowNode_RaidStampRally : FlowNode
  {
    private const int PIN_INPUT_BOSSCOUNT = 2;
    private const int PIN_OUTPUT_BOSSCOUNTBASE = 100;
    private const int PIN_OUTPUT_BOSSCOUNT04 = 101;
    private const int PIN_OUTPUT_BOSSCOUNT05 = 102;
    private const int PIN_OUTPUT_BOSSCOUNT06 = 103;
    private const int PIN_OUTPUT_BOSSCOUNT07 = 104;
    private const int PIN_OUTPUT_BOSSCOUNTERROR = 200;

    public override void OnActivate(int pinID)
    {
      if (pinID != 2)
        return;
      List<RaidBossParam> raidBossAll = RaidStampRallyWindow.GetRaidBossAll(RaidManager.Instance.RaidPeriodId);
      if (raidBossAll == null)
      {
        this.ActivateOutputLinks(200);
      }
      else
      {
        switch (raidBossAll.Count)
        {
          case 4:
            this.ActivateOutputLinks(101);
            break;
          case 5:
            this.ActivateOutputLinks(102);
            break;
          case 6:
            this.ActivateOutputLinks(103);
            break;
          case 7:
            this.ActivateOutputLinks(104);
            break;
          default:
            this.ActivateOutputLinks(200);
            break;
        }
      }
    }
  }
}
