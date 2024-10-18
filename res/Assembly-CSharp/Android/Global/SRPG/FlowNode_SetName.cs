﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetName
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "Failure", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/SetName", 32741)]
  [FlowNode.Pin(3, "Rename", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_SetName : FlowNode_Network
  {
    private bool isSettingName;

    public override void OnActivate(int pinID)
    {
      if (pinID != 100 || this.isSettingName)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.isSettingName = true;
        if (string.IsNullOrEmpty(GlobalVars.EditPlayerName))
          GlobalVars.EditPlayerName = MonoSingleton<GameManager>.Instance.Player.Name;
        if (string.IsNullOrEmpty(GlobalVars.EditPlayerName))
        {
          UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.RENAME_PLAYER_NAME"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
          this.ActivateOutputLinks(3);
          this.enabled = false;
        }
        else if (GlobalVars.EditPlayerName.Length < 4 || GlobalVars.EditPlayerName.Length > 12)
        {
          UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.PLAYER_NAME_INCORRECT_LENGTH"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
          this.ActivateOutputLinks(3);
          this.isSettingName = false;
        }
        else if (!MyMsgInput.isLegal(GlobalVars.EditPlayerName))
        {
          UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.RENAME_PLAYER_NAME"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
          this.ActivateOutputLinks(3);
          this.isSettingName = false;
        }
        else
        {
          this.ExecRequest((WebAPI) new ReqSetName(GlobalVars.EditPlayerName, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          this.enabled = true;
        }
      }
      else
        this.Success();
    }

    private void Success()
    {
      if (Network.Mode == Network.EConnectMode.Online)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        PlayerPrefs.SetString("PlayerName", MonoSingleton<GameManager>.Instance.Player.Name);
      }
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      this.isSettingName = false;
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.IllegalName:
          case Network.EErrCode.DuplicateName:
          case Network.EErrCode.SameName:
            this.isSettingName = false;
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
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
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.mails);
          }
          catch (Exception ex)
          {
            Debug.LogException(ex);
            this.Failure();
            return;
          }
          GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.GLOBAL_PLAYER_NAME);
          Network.RemoveAPI();
          this.Success();
        }
      }
    }
  }
}
