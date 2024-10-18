// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Friend_RemoveFavorite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Friend/Favorite/RemoveFavorite", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_Friend_RemoveFavorite : FlowNode_Network
  {
    private string mTargetFuid;

    public override void OnActivate(int pinID)
    {
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
          FriendData friendData = MonoSingleton<GameManager>.Instance.Player.Friends.Find((Predicate<FriendData>) (f => f.FUID == fuid));
          if (friendData != null)
          {
            string empty = string.Empty;
            if (!friendData.IsFavorite)
            {
              UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.FRIEND_NO_FAVORITE"), (UIUtility.DialogResultEvent) (go => { }));
              return;
            }
          }
          this.mTargetFuid = fuid;
          this.ExecRequest((WebAPI) new ReqFriendFavoriteRemove(fuid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          ((Behaviour) this).enabled = true;
        }
      }
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<Json_FriendArray> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_FriendArray>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      if (Network.IsError)
        this.OnRetry();
      else if (jsonObject.body == null)
      {
        this.OnRetry();
      }
      else
      {
        if (!string.IsNullOrEmpty(this.mTargetFuid))
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.friends, FriendStates.Friend);
            FriendData friendData = MonoSingleton<GameManager>.Instance.Player.Friends.Find((Predicate<FriendData>) (f => f.FUID == this.mTargetFuid));
            if (friendData != null)
              GlobalVars.SelectedFriend = friendData;
          }
          catch (Exception ex)
          {
            this.OnRetry(ex);
            return;
          }
        }
        Network.RemoveAPI();
        this.Success();
      }
    }
  }
}
