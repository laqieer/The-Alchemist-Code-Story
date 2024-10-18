// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRaidGuildStats
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Raid/Req/GuildStats", 32741)]
  public class FlowNode_ReqRaidGuildStats : FlowNode_ReqRaidBase
  {
    public override WebAPI GenerateWebAPI()
    {
      return (WebAPI) new ReqRaidGuildStats(MonoSingleton<GameManager>.Instance.MasterParam.GetActiveRaidPeriod(end_offset_hour: 24).Id, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback));
    }

    public override bool Success(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<ReqRaidGuildStats.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRaidGuildStats.Response>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      try
      {
        if (jsonObject.body == null)
          throw new Exception("Response is NULL : /raidboss/guild/stats");
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) RaidGuildStatsWindow.Instance, (UnityEngine.Object) null))
          throw new Exception("RaidManager not exists : /raidboss/guild/stats");
        RaidGuildStatsWindow.Instance.Setup(jsonObject.body);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      return true;
    }
  }
}
