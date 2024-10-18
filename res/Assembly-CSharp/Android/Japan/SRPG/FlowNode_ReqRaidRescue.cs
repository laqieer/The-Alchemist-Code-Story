// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRaidRescue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

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
        UIUtility.SystemMessage(LocalizedText.Get("sys.RAID_RESCUE_CAN_NOT_RESCUE_PLAYER_LV_SHORT"), (UIUtility.DialogResultEvent) (gameObject => this.ActivateOutputLinks(204)), (GameObject) null, false, -1);
        return false;
      }
      WebAPI.JSON_BodyResponse<ReqRaidRescue.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRaidRescue.Response>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      try
      {
        if (jsonObject.body == null)
          throw new Exception("Response is NULL : /raidboss/rescue");
        if ((UnityEngine.Object) RaidManager.Instance == (UnityEngine.Object) null)
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
