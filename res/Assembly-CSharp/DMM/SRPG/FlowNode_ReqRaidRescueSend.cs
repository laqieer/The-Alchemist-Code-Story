// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRaidRescueSend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Raid/Req/Rescue/Send", 32741)]
  public class FlowNode_ReqRaidRescueSend : FlowNode_ReqRaidBase
  {
    public override WebAPI GenerateWebAPI()
    {
      RaidBossData currentRaidBossData = RaidManager.Instance.CurrentRaidBossData;
      int is_send_guild = !RaidManager.Instance.RescueReqOptionGuild ? 0 : 1;
      int is_send_friend = !RaidManager.Instance.RescueReqOptionFriend ? 0 : 1;
      return (WebAPI) new ReqRaidRescueSend(currentRaidBossData.AreaId, currentRaidBossData.RaidBossInfo.BossId, currentRaidBossData.RaidBossInfo.Round, is_send_guild, is_send_friend, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback));
    }

    public override bool Success(WWWResult www)
    {
      RaidManager.Instance.CurrentRaidBossData.SOSDone();
      return true;
    }
  }
}
