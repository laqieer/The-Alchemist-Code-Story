﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckFacebookID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(3, "No Device ID", FlowNode.PinTypes.Output, 2)]
  [FlowNode.NodeType("Network/CheckFacebookID", 32741)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_CheckFacebookID : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      string deviceId = MonoSingleton<GameManager>.Instance.DeviceId;
      if (deviceId == string.Empty)
      {
        this.ActivateOutputLinks(3);
      }
      else
      {
        this.ExecRequest((WebAPI) new ReqCheckFacebookID(deviceId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.enabled = true;
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_ReqCheckFacebookIDResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ReqCheckFacebookIDResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          GlobalVars.FacebookID = jsonObject.body.related_id;
          this.ActivateOutputLinks(2);
          this.enabled = false;
        }
      }
    }
  }
}
