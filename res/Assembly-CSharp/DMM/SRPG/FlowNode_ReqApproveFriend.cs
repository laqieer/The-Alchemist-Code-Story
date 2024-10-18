// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqApproveFriend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Friend/ApproveFriend", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqApproveFriend : FlowNode_Network
  {
    private string req_fuid;

    public override void OnActivate(int pinID)
    {
      this.req_fuid = (string) null;
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        string fuid = (string) null;
        if (!string.IsNullOrEmpty(GlobalVars.SelectedFriendID))
          fuid = GlobalVars.SelectedFriendID;
        else if (GlobalVars.FoundFriend != null && !string.IsNullOrEmpty(GlobalVars.FoundFriend.FUID))
          fuid = GlobalVars.FoundFriend.FUID;
        if (fuid == null)
        {
          this.Success();
        }
        else
        {
          this.ExecRequest((WebAPI) new ReqFriendApprove(fuid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          this.req_fuid = fuid;
          ((Behaviour) this).enabled = true;
        }
      }
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.PLAYER_FRIENDNUM);
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.ApprRequestMax:
          case Network.EErrCode.ApprFriendIsFull:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_ApproveFriend> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ApproveFriend>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Player.RemoveFriendFollower(this.req_fuid);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.friends, FriendStates.Friend);
            MonoSingleton<GameManager>.Instance.Player.FirstFriendCount = jsonObject.body.first_count;
            MonoSingleton<GameManager>.Instance.Player.UpdateFriendCount();
          }
          catch (Exception ex)
          {
            this.OnRetry(ex);
            return;
          }
          Network.RemoveAPI();
          this.Success();
        }
      }
    }
  }
}
