// Decompiled with JetBrains decompiler
// Type: SRPG.Flownode_RaidCheckRescueWaitSeconds
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Raid/CheckRescueWaitSeconds", 32741)]
  [FlowNode.Pin(1, "Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "CanReload", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(102, "UseCache", FlowNode.PinTypes.Output, 3)]
  public class Flownode_RaidCheckRescueWaitSeconds : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      RaidManager instance = RaidManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        this.ActivateOutputLinks(101);
      else if (instance.RaidRescueMemberList == null)
        this.ActivateOutputLinks(101);
      else
        this.ActivateOutputLinks(102);
    }
  }
}
