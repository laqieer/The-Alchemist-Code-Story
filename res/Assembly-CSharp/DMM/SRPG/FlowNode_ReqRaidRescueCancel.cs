// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRaidRescueCancel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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

    public override bool Success(WWWResult www) => true;
  }
}
