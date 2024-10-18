// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RaidFriendRescueCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Raid/FriendCheck", 32741)]
  [FlowNode.Pin(0, "check", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Friend OK", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Friend ERROR", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_RaidFriendRescueCheck : FlowNode
  {
    [SerializeField]
    private int m_friendNum;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      RaidBossData currentRaidBossData = RaidManager.Instance.CurrentRaidBossData;
      if ((!RaidManager.Instance.RescueReqOptionFriend ? 0 : 1) == 0)
        this.ActivateOutputLinks(10);
      else if (MonoSingleton<GameManager>.Instance.Player.FriendNum >= this.m_friendNum)
        this.ActivateOutputLinks(10);
      else
        this.ActivateOutputLinks(11);
    }
  }
}
