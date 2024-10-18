// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqFgGAuth
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("System/Auth/ReqFgGAuth", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqFgGAuth : FlowNode_Network
  {
    private const int PIN_ID_REQUEST = 1;
    private const int PIN_ID_SUCCESS = 2;
    private ReqFgGAuth.eAuthStatus mAuthStatusBefore;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.Success(ReqFgGAuth.eAuthStatus.Disable);
    }

    private void Success(ReqFgGAuth.eAuthStatus authStatus)
    {
      MonoSingleton<GameManager>.Instance.AuthStatus = authStatus;
      this.enabled = false;
      this.ActivateOutputLinks(2);
    }

    private bool CheckStatusChanged(ReqFgGAuth.eAuthStatus status)
    {
      if (this.mAuthStatusBefore == ReqFgGAuth.eAuthStatus.None)
        return false;
      return this.mAuthStatusBefore != status;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        this.OnFailed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqFgGAuth.JSON_FgGAuth> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqFgGAuth.JSON_FgGAuth>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        int authStatus = jsonObject.body.auth_status;
        MonoSingleton<GameManager>.Instance.FgGAuthHost = jsonObject.body.auth_url;
        Network.RemoveAPI();
        switch (authStatus)
        {
          case 1:
            this.Success(ReqFgGAuth.eAuthStatus.Disable);
            break;
          case 2:
            this.Success(ReqFgGAuth.eAuthStatus.NotSynchronized);
            break;
          case 3:
            this.Success(ReqFgGAuth.eAuthStatus.Synchronized);
            if (GameUtility.GetCurrentScene() != GameUtility.EScene.HOME || !this.CheckStatusChanged(ReqFgGAuth.eAuthStatus.Synchronized))
              break;
            MonoSingleton<GameManager>.Instance.Player.OnFgGIDLogin();
            break;
          default:
            this.OnFailed();
            break;
        }
      }
    }

    private class JSON_FgGAuth
    {
      public int auth_status;
      public string auth_url;
    }
  }
}
