﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GetAccessToken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using Gsc.Auth;

namespace SRPG
{
  [FlowNode.Pin(2, "Migrate", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "Banned", FlowNode.PinTypes.Output, 3)]
  [FlowNode.NodeType("System/GetAccessToken", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_GetAccessToken : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      MyMetaps.TrackEvent("device_id", MonoSingleton<GameManager>.Instance.DeviceId);
      Network.SessionID = Session.DefaultSession.AccessToken;
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoDevice:
          case Network.EErrCode.Authorize:
            this.OnFailed();
            return;
        }
      }
      WebAPI.JSON_BodyResponse<FlowNode_GetAccessToken.JSON_AccessToken> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_GetAccessToken.JSON_AccessToken>>(www.text);
      if (jsonObject.body == null)
      {
        this.OnFailed();
      }
      else
      {
        if (jsonObject.body.access_token == null && jsonObject.body.status != null)
        {
          GlobalVars.BanStatus = int.Parse(jsonObject.body.status);
          GlobalVars.CustomerID = jsonObject.body.cuid;
          Network.RemoveAPI();
          this.ActivateOutputLinks(3);
        }
        else
        {
          if (jsonObject.body.device_id != null)
            MonoSingleton<GameManager>.Instance.SaveAuth(jsonObject.body.device_id);
          Network.SessionID = jsonObject.body.access_token;
          Network.RemoveAPI();
          if (jsonObject.body.old_device_id != null)
          {
            MonoSingleton<GameManager>.Instance.SaveAuth(jsonObject.body.old_device_id, jsonObject.body.secret_key);
            MonoSingleton<GameManager>.Instance.InitAuth();
            this.ActivateOutputLinks(2);
          }
          else
            this.ActivateOutputLinks(1);
        }
        this.enabled = false;
      }
    }

    private class JSON_AccessToken
    {
      public string access_token;
      public int expires_in;
      public string device_id;
      public string old_device_id;
      public string secret_key;
      public string status;
      public string cuid;
    }
  }
}
