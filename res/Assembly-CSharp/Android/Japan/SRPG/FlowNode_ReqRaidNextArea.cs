// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRaidNextArea
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("Raid/Req/NextArea", 32741)]
  public class FlowNode_ReqRaidNextArea : FlowNode_ReqRaidBase
  {
    public override WebAPI GenerateWebAPI()
    {
      return (WebAPI) new ReqRaidNextArea(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback));
    }

    public override bool Success(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<ReqRaidNextArea.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRaidNextArea.Response>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      try
      {
        if (jsonObject.body == null)
          throw new Exception("Response is NULL : /raidboss/next_area");
        if ((UnityEngine.Object) RaidManager.Instance == (UnityEngine.Object) null)
          throw new Exception("RaidManager not exists : /raidboss/next_area");
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
