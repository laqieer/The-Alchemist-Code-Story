// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRaidRescueCancel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Raid/Req/Rescue/Cancel", 32741)]
  public class FlowNode_ReqRaidRescueCancel : FlowNode_ReqRaidBase
  {
    public override WebAPI GenerateWebAPI()
    {
      RaidBossData rescueRaidBossData = RaidManager.Instance.RescueRaidBossData;
      return (WebAPI) new ReqRaidRescueCancel(rescueRaidBossData.OwnerUID, rescueRaidBossData.AreaId, rescueRaidBossData.RaidBossInfo.BossId, rescueRaidBossData.RaidBossInfo.Round, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback));
    }

    public override bool Success(WWWResult www)
    {
      return true;
    }
  }
}
