﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RejectFriend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Friend/RejectFriend", 32741)]
  [FlowNode.Pin(10, "ひとり拒否", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "すべて拒否", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(20, "ひとり拒否成功", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(21, "すべて拒否成功", FlowNode.PinTypes.Output, 21)]
  public class FlowNode_RejectFriend : FlowNode_Network
  {
    private bool mIsRemoveAll;
    private string remove_fuid;

    public override void OnActivate(int pinID)
    {
      if (((Behaviour) this).enabled)
        return;
      this.mIsRemoveAll = false;
      this.remove_fuid = (string) null;
      switch (pinID)
      {
        case 10:
          if (Network.Mode == Network.EConnectMode.Offline)
          {
            this.ActivateOutputLinks(20);
            ((Behaviour) this).enabled = false;
            break;
          }
          string fuid = GlobalVars.SelectedFriend.FUID;
          this.ExecRequest((WebAPI) new ReqFriendReject(new string[1]
          {
            fuid
          }, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          this.remove_fuid = fuid;
          ((Behaviour) this).enabled = true;
          break;
        case 11:
          this.mIsRemoveAll = true;
          if (Network.Mode == Network.EConnectMode.Offline)
          {
            this.ActivateOutputLinks(21);
            ((Behaviour) this).enabled = false;
            break;
          }
          List<FriendData> friendsFollower = MonoSingleton<GameManager>.Instance.Player.FriendsFollower;
          List<FriendData> friendDataList = new List<FriendData>();
          for (int index = 0; index < friendsFollower.Count; ++index)
          {
            if (friendsFollower[index] != null)
              friendDataList.Add(friendsFollower[index]);
          }
          if (friendDataList.Count < 1)
            break;
          string[] fuids = new string[friendDataList.Count];
          for (int index = 0; index < friendDataList.Count; ++index)
            fuids[index] = friendDataList[index].FUID;
          this.ExecRequest((WebAPI) new ReqFriendReject(fuids, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          ((Behaviour) this).enabled = true;
          break;
        default:
          ((Behaviour) this).enabled = false;
          break;
      }
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
        WebAPI.JSON_BodyResponse<Json_PlayerData> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerData>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnRetry();
            return;
          }
          MonoSingleton<GameManager>.GetInstanceDirect().RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
          Network.RemoveAPI();
          if (this.mIsRemoveAll)
          {
            MonoSingleton<GameManager>.Instance.Player.RemoveFriendFollowerAll();
            this.ActivateOutputLinks(21);
          }
          else
          {
            MonoSingleton<GameManager>.Instance.Player.RemoveFriendFollower(this.remove_fuid);
            this.ActivateOutputLinks(20);
          }
          GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.PLAYER_FRIENDNUM);
          ((Behaviour) this).enabled = false;
        }
      }
    }
  }
}
