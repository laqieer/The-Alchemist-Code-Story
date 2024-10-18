// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GetAccessToken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using Gsc.Auth;

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
        Network.SessionID = jsonObject.body.access_token;
        MyMetaps.TrackEvent("device_id", MonoSingleton<GameManager>.Instance.DeviceId);
        Network.RemoveAPI();
        this.ActivateOutputLinks(1);
        this.enabled = false;
      }
    }

    private class JSON_AccessToken
    {
      public string access_token;
      public int expires_in;
    }
  }
}
