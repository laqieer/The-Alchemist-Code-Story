// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_FindFriend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "FindByID", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "FindByUsername", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/FindFriend", 32741)]
  [FlowNode.Pin(200, "みつからなかった", FlowNode.PinTypes.Output, 200)]
  public class FlowNode_FindFriend : FlowNode_Network
  {
    public InputField InputFieldFriendID;

    private void Start()
    {
      if (!((UnityEngine.Object) this.InputFieldFriendID != (UnityEngine.Object) null))
        return;
      this.InputFieldFriendID.onEndEdit.AddListener((UnityAction<string>) (_param1 => this.OnEndEdit(this.InputFieldFriendID)));
      this.enabled = true;
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      GUtility.SetImmersiveMove();
      if (!((UnityEngine.Object) this.InputFieldFriendID != (UnityEngine.Object) null) || this.InputFieldFriendID.onEndEdit == null)
        return;
      this.InputFieldFriendID.onEndEdit.RemoveAllListeners();
    }

    private void OnEndEdit(InputField field)
    {
      GUtility.SetImmersiveMove();
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 && pinID != 1 || this.enabled)
        return;
      if (Network.Mode == Network.EConnectMode.Offline)
        this.Failure();
      else if ((UnityEngine.Object) this.InputFieldFriendID == (UnityEngine.Object) null)
      {
        this.enabled = false;
      }
      else
      {
        string text = this.InputFieldFriendID.text;
        if (string.IsNullOrEmpty(text))
        {
          this.enabled = false;
        }
        else
        {
          GlobalVars.SelectedFriendID = string.Empty;
          switch (pinID)
          {
            case 0:
              this.ExecRequest((WebAPI) new ReqFriendFind(text, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
              break;
            case 1:
              this.ExecRequest((WebAPI) new ReqFriendFindByUsername(text, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
              break;
          }
          this.enabled = true;
        }
      }
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(10);
    }

    private void Failure()
    {
      this.enabled = false;
      this.ActivateOutputLinks(200);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.FindNoFriend:
            Network.RemoveAPI();
            Network.ResetError();
            this.Failure();
            break;
          case Network.EErrCode.FindIsMine:
            this.enabled = false;
            this.OnBack();
            break;
          case Network.EErrCode.StringTooShort:
            this.enabled = false;
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        DebugMenu.Log("API", "find/friend:" + www.text);
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          try
          {
            if (jsonObject.body.friends == null || jsonObject.body.friends.Length < 1)
              throw new InvalidJSONException();
            FriendData friendData = new FriendData();
            friendData.Deserialize(jsonObject.body.friends[0]);
            GlobalVars.FoundFriend = friendData;
            this.Success();
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.Failure();
          }
        }
      }
    }
  }
}
