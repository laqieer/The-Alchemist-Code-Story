// Decompiled with JetBrains decompiler
// Type: SRPG.Flownode_GuildRaidBattlePoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("GuildRaid/BattlePoint", 32741)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Today End", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "No Challenge", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "Challenge", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "NoBattleSchedule", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(900, "Error", FlowNode.PinTypes.Output, 900)]
  public class Flownode_GuildRaidBattlePoint : FlowNode
  {
    private const int PIN_IN_START = 1;
    private const int PIN_OUT_TODAYEND = 101;
    private const int PIN_OUT_NO_CHALLENGE = 102;
    private const int PIN_OUT_CHALLENGE = 103;
    private const int PIN_OUT_NOBATTLESCHEDULE = 104;
    private const int PIN_OUT_ERROR = 900;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      GuildRaidManager instance2 = GuildRaidManager.Instance;
      if (Object.op_Equality((Object) instance2, (Object) null) || Object.op_Equality((Object) instance1, (Object) null))
        this.ActivateOutputLinks(900);
      else if (!instance1.IsGuildRaidBattleSchedule(instance2.PeriodId))
        this.ActivateOutputLinks(104);
      else if (instance2.ChallengedBp == instance2.MaxBp)
        this.ActivateOutputLinks(101);
      else if (instance2.ChallengedBp == 0)
        this.ActivateOutputLinks(102);
      else
        this.ActivateOutputLinks(103);
    }
  }
}
