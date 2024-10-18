// Decompiled with JetBrains decompiler
// Type: SRPG.Flownode_GuildRaidCheckFinish
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("GuildRaid/CheckFinish", 32741)]
  [FlowNode.Pin(1, "Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "End GuildRaid", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(102, "Not End GuildRaid", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(900, "Error", FlowNode.PinTypes.Output, 9)]
  public class Flownode_GuildRaidCheckFinish : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (Object.op_Equality((Object) GuildRaidManager.Instance, (Object) null))
        this.ActivateOutputLinks(900);
      else if (GuildRaidManager.Instance.IsFinishGuildRaid())
        this.ActivateOutputLinks(101);
      else
        this.ActivateOutputLinks(102);
    }
  }
}
