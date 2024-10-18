// Decompiled with JetBrains decompiler
// Type: SRPG.Flownode_RaidCheckRescueCount
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Raid/CheckRescueCount", 32741)]
  [FlowNode.Pin(1, "Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Has Rescue Member", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(102, "Nothing", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(900, "Error", FlowNode.PinTypes.Output, 9)]
  public class Flownode_RaidCheckRescueCount : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) RaidManager.Instance, (UnityEngine.Object) null))
      {
        this.ActivateOutputLinks(900);
      }
      else
      {
        List<RaidSOSMember> members = new List<RaidSOSMember>();
        int num = 0;
        switch (RaidManager.Instance.SelectedRaidOwnerType)
        {
          case RaidManager.RaidOwnerType.Self:
            members = RaidManager.Instance.CurrentRaidBossData.SOSMember;
            break;
          case RaidManager.RaidOwnerType.Rescue:
            members = RaidManager.Instance.RescueRaidBossData.SOSMember;
            break;
        }
        for (int i = 0; i < members.Count; ++i)
        {
          if (!(members[i].FUID == MonoSingleton<GameManager>.Instance.Player.FUID) && MonoSingleton<GameManager>.Instance.Player.Friends.Find((Predicate<FriendData>) (friend => friend.FUID == members[i].FUID)) == null)
            ++num;
        }
        if (num > 0)
          this.ActivateOutputLinks(101);
        else
          this.ActivateOutputLinks(102);
      }
    }
  }
}
