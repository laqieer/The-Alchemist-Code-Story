// Decompiled with JetBrains decompiler
// Type: SRPG.Flownode_GuildRaidForcedDeck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("GuildRaid/ForceDeck", 32741)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "ForceDeck", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "Not ForceDeck", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(900, "Error", FlowNode.PinTypes.Output, 900)]
  public class Flownode_GuildRaidForcedDeck : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      GuildRaidManager instance = GuildRaidManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        this.ActivateOutputLinks(900);
      else if (instance.IsForcedDeck)
        this.ActivateOutputLinks(101);
      else
        this.ActivateOutputLinks(102);
    }
  }
}
