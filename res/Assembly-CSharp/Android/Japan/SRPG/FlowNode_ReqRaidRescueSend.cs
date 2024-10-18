// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRaidRescueSend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
