﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Login
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.Pin(4, "Reset to Title", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(6, "No Login until Specified Date", FlowNode.PinTypes.Output, 16)]
  [FlowNode.NodeType("System/Login", 32741)]
  [FlowNode.Pin(10, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success To PlayNew", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "Success To SetName", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(3, "Success To ReqBtlCom", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(5, "No Login Indefinitely", FlowNode.PinTypes.Output, 15)]
  public class FlowNode_Login : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        if (MonoSingleton<GameManager>.Instance.IsRelogin)
        {
          this.ExecRequest((WebAPI) new ReqReLogin(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        }
        else
        {
          MonoSingleton<GameManager>.Instance.Player.ClearUnits();
          MonoSingleton<GameManager>.Instance.Player.ClearItems();
          this.ExecRequest((WebAPI) new ReqLogin(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        }
        this.enabled = true;
      }
      else
        this.Success();
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(3);
    }

    private void Failure()
    {
      this.enabled = false;
      this.ActivateOutputLinks(4);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this.OnFailed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (jsonObject.body == null)
        {
          this.enabled = false;
          PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV, 0, false);
          PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV, 0, false);
          MonoSingleton<GameManager>.Instance.Player.ClearTrophies();
          this.ActivateOutputLinks(1);
        }
        else
        {
          GlobalVars.CustomerID = jsonObject.body.cuid;
          int status = jsonObject.body.status;
          if (status != 0)
          {
            GlobalVars.BanStatus = jsonObject.body.status;
            if (status == 1)
              this.ActivateOutputLinks(5);
            else if (jsonObject.body.status > 1)
              this.ActivateOutputLinks(6);
            this.enabled = false;
          }
          else
          {
            GameManager instance = MonoSingleton<GameManager>.Instance;
            long lastConnectionTime = Network.LastConnectionTime;
            instance.Player.LoginDate = TimeManager.FromUnixTime(lastConnectionTime);
            instance.Player.TutorialFlags = jsonObject.body.tut;
            if (instance.IsRelogin)
            {
              try
              {
                if (jsonObject.body.player != null)
                  instance.Deserialize(jsonObject.body.player);
                if (jsonObject.body.items != null)
                  instance.Deserialize(jsonObject.body.items);
                if (jsonObject.body.units != null)
                  instance.Deserialize(jsonObject.body.units);
                if (jsonObject.body.parties != null)
                  instance.Deserialize(jsonObject.body.parties);
                if (jsonObject.body.notify != null)
                  instance.Deserialize(jsonObject.body.notify);
                if (jsonObject.body.artifacts != null)
                  instance.Deserialize(jsonObject.body.artifacts, false);
                if (jsonObject.body.skins != null)
                  instance.Deserialize(jsonObject.body.skins);
                if (jsonObject.body.vs != null)
                  instance.Deserialize(jsonObject.body.vs);
              }
              catch (Exception ex)
              {
                DebugUtility.LogException(ex);
                this.Failure();
                return;
              }
            }
            else
            {
              try
              {
                instance.Deserialize(jsonObject.body.player);
                instance.Deserialize(jsonObject.body.items);
                instance.Deserialize(jsonObject.body.units);
                instance.Deserialize(jsonObject.body.parties);
                instance.Deserialize(jsonObject.body.notify);
                instance.Deserialize(jsonObject.body.artifacts, false);
                instance.Deserialize(jsonObject.body.skins);
                instance.Deserialize(jsonObject.body.vs);
              }
              catch (Exception ex)
              {
                DebugUtility.LogException(ex);
                this.Failure();
                return;
              }
            }
            if ((UnityEngine.Object) this != (UnityEngine.Object) null)
              this.enabled = false;
            GlobalVars.BtlID.Set(jsonObject.body.player.btlid);
            if (!string.IsNullOrEmpty(jsonObject.body.player.btltype))
              GlobalVars.QuestType = QuestParam.ToQuestType(jsonObject.body.player.btltype);
            GameUtility.Config_OkyakusamaCode = instance.Player.OkyakusamaCode;
            if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV))
              PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV, MonoSingleton<GameManager>.Instance.Player.Lv, false);
            if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV))
              PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV, MonoSingleton<GameManager>.Instance.Player.VipRank, false);
            instance.PostLogin();
            if (MonoSingleton<GameManager>.Instance.Player == null)
              ;
            this.ActivateOutputLinks(!string.IsNullOrEmpty(jsonObject.body.player.name) ? 3 : 2);
          }
        }
      }
    }
  }
}
