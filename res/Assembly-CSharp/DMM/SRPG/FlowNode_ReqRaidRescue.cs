// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRaidRescue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Raid/Req/Rescue/Index", 32741)]
  [FlowNode.Pin(204, "Rescue Player Level Short", FlowNode.PinTypes.Output, 204)]
  public class FlowNode_ReqRaidRescue : FlowNode_ReqRaidBase
  {
    public override WebAPI GenerateWebAPI()
    {
      return (WebAPI) new ReqRaidRescue(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback));
    }

    public override bool Success(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.ResetError();
        UIUtility.SystemMessage(LocalizedText.Get("sys.RAID_RESCUE_CAN_NOT_RESCUE_PLAYER_LV_SHORT"), (UIUtility.DialogResultEvent) (gameObject => this.ActivateOutputLinks(204)));
        return false;
      }
      WebAPI.JSON_BodyResponse<ReqRaidRescue.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRaidRescue.Response>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      try
      {
        if (jsonObject.body == null)
          throw new Exception("Response is NULL : /raidboss/rescue");
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) RaidManager.Instance, (UnityEngine.Object) null))
          throw new Exception("RaidManager not exists : /raidboss/rescue");
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
