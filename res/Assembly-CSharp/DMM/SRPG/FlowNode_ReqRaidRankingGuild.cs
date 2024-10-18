// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRaidRankingGuild
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Raid/Req/RankingGuild", 32741)]
  public class FlowNode_ReqRaidRankingGuild : FlowNode_ReqRaidBase
  {
    public override WebAPI GenerateWebAPI()
    {
      return (WebAPI) new ReqRaidRankingGuild(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback));
    }

    public override bool Success(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<ReqRaidRankingGuild.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRaidRankingGuild.Response>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      try
      {
        if (jsonObject.body == null)
          throw new Exception("Response is NULL : /raidboss/ranking/guild");
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) RaidManager.Instance, (UnityEngine.Object) null))
          throw new Exception("RaidManager not exists : /raidboss/ranking/guild");
        RaidBeatRankingWindow.Instance.Setup(jsonObject.body);
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
