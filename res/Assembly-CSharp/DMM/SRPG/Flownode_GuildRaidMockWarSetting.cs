// Decompiled with JetBrains decompiler
// Type: SRPG.Flownode_GuildRaidMockWarSetting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("GuildRaid/MockWarSetting", 32741)]
  [FlowNode.Pin(1, "Set Main War", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Set Mock War", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(101, "Set", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(900, "Error", FlowNode.PinTypes.Output, 9)]
  public class Flownode_GuildRaidMockWarSetting : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      GuildRaidManager instance = GuildRaidManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
      {
        this.ActivateOutputLinks(900);
      }
      else
      {
        switch (pinID)
        {
          case 1:
            instance.BattleType = GuildRaidBattleType.Main;
            break;
          case 2:
            instance.BattleType = GuildRaidBattleType.Mock;
            break;
        }
        this.ActivateOutputLinks(101);
      }
    }
  }
}
