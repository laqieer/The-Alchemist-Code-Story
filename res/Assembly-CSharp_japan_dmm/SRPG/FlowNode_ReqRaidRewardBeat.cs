// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRaidRewardBeat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Raid/Req/Reward/Beat", 32741)]
  public class FlowNode_ReqRaidRewardBeat : FlowNode_ReqRaidBase
  {
    public override WebAPI GenerateWebAPI()
    {
      int area_id = 0;
      int boss_id = 0;
      int round = 0;
      string uid = string.Empty;
      if (RaidManager.Instance.SelectedRaidOwnerType == RaidManager.RaidOwnerType.Self)
      {
        area_id = RaidManager.Instance.CurrentRaidAreaId;
        boss_id = RaidManager.Instance.CurrentRaidBossData.RaidBossInfo.BossId;
        round = RaidManager.Instance.CurrentRaidBossData.RaidBossInfo.Round;
      }
      else if (RaidManager.Instance.SelectedRaidOwnerType == RaidManager.RaidOwnerType.Rescue)
      {
        area_id = RaidManager.Instance.RescueRaidBossData.AreaId;
        boss_id = RaidManager.Instance.RescueRaidBossData.RaidBossInfo.BossId;
        round = RaidManager.Instance.RescueRaidBossData.RaidBossInfo.Round;
        uid = RaidManager.Instance.RescueRaidBossData.OwnerUID;
      }
      return (WebAPI) new ReqRaidRewardBeat(area_id, boss_id, round, uid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback));
    }

    public override bool Success(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<ReqRaidRewardBeat.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRaidRewardBeat.Response>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      try
      {
        if (jsonObject.body == null)
          throw new Exception("Response is NULL : /raidboss/reward/beat");
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) RaidManager.Instance, (UnityEngine.Object) null))
          throw new Exception("RaidManager not exists : /raidboss/reward/beat");
        RaidManager.Instance.Setup(jsonObject.body);
      }
      catch (Exception ex)
      {
        Network.RemoveAPI();
        DebugUtility.LogException(ex);
        return false;
      }
      return true;
    }
  }
}
