// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GetAccessToken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Auth;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Auth/GetAccessToken", 32741)]
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
      ((Behaviour) this).enabled = false;
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
        Network.SessionID = jsonObject.body.access_token;
        MyMetaps.TrackEvent("device_id", MonoSingleton<GameManager>.Instance.DeviceId);
        Network.RemoveAPI();
        this.ActivateOutputLinks(1);
        ((Behaviour) this).enabled = false;
      }
    }

    private class JSON_AccessToken
    {
      public string access_token;
      public int expires_in;
    }
  }
}
