﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RemoveFriend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("System/RemoveFriend", 32741)]
  [FlowNode.Pin(10, "ひとりフレンド解除", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(20, "ひとりフレンド解除成功", FlowNode.PinTypes.Output, 20)]
  public class FlowNode_RemoveFriend : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (this.enabled)
        return;
      if (pinID == 10)
      {
        if (Network.Mode == Network.EConnectMode.Offline)
        {
          this.ActivateOutputLinks(20);
          this.enabled = false;
        }
        else
        {
          this.ExecRequest((WebAPI) new ReqFriendRemove(new string[1]
          {
            GlobalVars.SelectedFriend.FUID
          }, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          this.enabled = true;
        }
      }
      else
        this.enabled = false;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.RmNoFriend)
          this.OnBack();
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.friends, FriendStates.Friend);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnRetry();
            return;
          }
          MonoSingleton<GameManager>.GetInstanceDirect().RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
          Network.RemoveAPI();
          this.ActivateOutputLinks(20);
          GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.PLAYER_FRIENDNUM);
          this.enabled = false;
        }
      }
    }
  }
}
