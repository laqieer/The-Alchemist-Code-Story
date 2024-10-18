// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Login
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Login/Login", 32741)]
  [FlowNode.Pin(10, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success To PlayNew", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "Success To SetName", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(3, "Success To ReqBtlCom", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(4, "Reset to Title", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(5, "無期限ログイン不可", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(6, "指定の日時までログイン不可", FlowNode.PinTypes.Output, 16)]
  public class FlowNode_Login : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      if (SRPG.Network.Mode == SRPG.Network.EConnectMode.Online)
      {
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        if (MonoSingleton<GameManager>.Instance.IsRelogin)
        {
          this.ExecRequest((WebAPI) new ReqReLogin(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
        }
        else
        {
          MonoSingleton<GameManager>.Instance.Player.ClearUnits();
          MonoSingleton<GameManager>.Instance.Player.ClearItems();
          this.ExecRequest((WebAPI) new ReqLogin(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
        }
        ((Behaviour) this).enabled = true;
      }
      else
        this.Success();
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(3);
    }

    private void Failure()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(4);
    }

    public override void OnSuccess(WWWResult www)
    {
      Json_PlayerDataAll jsonPlayerDataAll;
      if (EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
      {
        if (SRPG.Network.IsError)
        {
          int errCode = (int) SRPG.Network.ErrCode;
          this.OnFailed();
          return;
        }
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "jsonRes == null");
        jsonPlayerDataAll = jsonObject.body;
      }
      else
      {
        FlowNode_Login.MP_PlayerDataAll mpPlayerDataAll = SerializerCompressorHelper.Decode<FlowNode_Login.MP_PlayerDataAll>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpPlayerDataAll.stat;
        string statMsg = mpPlayerDataAll.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        if (SRPG.Network.IsError)
        {
          int errCode = (int) SRPG.Network.ErrCode;
          this.OnFailed();
          return;
        }
        DebugUtility.Assert(mpPlayerDataAll != null, "mpRes == null");
        jsonPlayerDataAll = mpPlayerDataAll.body;
        if (jsonPlayerDataAll.player == null)
          jsonPlayerDataAll = (Json_PlayerDataAll) null;
      }
      SRPG.Network.RemoveAPI();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      AppGuardClient.SetUserId(instance.DeviceId);
      if (jsonPlayerDataAll == null)
      {
        ((Behaviour) this).enabled = false;
        PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV, 0);
        PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV, 0);
        MonoSingleton<GameManager>.Instance.Player.TrophyData.ClearTrophies();
        this.SetUseDLC_EvalOnly(instance.Player, false);
        this.ActivateOutputLinks(1);
      }
      else
      {
        GlobalVars.CustomerID = jsonPlayerDataAll.cuid;
        int status = jsonPlayerDataAll.status;
        if (status != 0)
        {
          GlobalVars.BanStatus = jsonPlayerDataAll.status;
          if (status == 1)
            this.ActivateOutputLinks(5);
          else if (jsonPlayerDataAll.status > 1)
            this.ActivateOutputLinks(6);
          ((Behaviour) this).enabled = false;
        }
        else
        {
          long lastConnectionTime = SRPG.Network.LastConnectionTime;
          instance.Player.LoginDate = TimeManager.FromUnixTime(lastConnectionTime);
          instance.Player.TutorialFlags = jsonPlayerDataAll.tut;
          this.SetUseDLC_EvalOnly(instance.Player, false);
          if (instance.IsRelogin)
          {
            try
            {
              if (jsonPlayerDataAll.player != null)
                instance.Deserialize(jsonPlayerDataAll.player);
              if (jsonPlayerDataAll.items != null)
                instance.Deserialize(jsonPlayerDataAll.items);
              if (jsonPlayerDataAll.units != null)
                instance.Deserialize(jsonPlayerDataAll.units);
              if (jsonPlayerDataAll.parties != null)
                instance.Deserialize(jsonPlayerDataAll.parties);
              if (jsonPlayerDataAll.notify != null)
                instance.Deserialize(jsonPlayerDataAll.notify);
              if (jsonPlayerDataAll.artifacts != null)
                instance.Deserialize(jsonPlayerDataAll.artifacts);
              if (jsonPlayerDataAll.skins != null)
                instance.Deserialize(jsonPlayerDataAll.skins);
              if (jsonPlayerDataAll.vs != null)
                instance.Deserialize(jsonPlayerDataAll.vs);
              if (jsonPlayerDataAll.tips != null)
                instance.Tips = ((IEnumerable<string>) jsonPlayerDataAll.tips).ToList<string>();
              if (jsonPlayerDataAll.player_guild != null)
                instance.Deserialize(jsonPlayerDataAll.player_guild);
              if (jsonPlayerDataAll.expansions != null)
              {
                if (instance.Player != null)
                  instance.Player.Deserialize(jsonPlayerDataAll.expansions);
              }
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
              instance.Deserialize(jsonPlayerDataAll.player);
              instance.Deserialize(jsonPlayerDataAll.items);
              instance.Deserialize(jsonPlayerDataAll.units);
              instance.Deserialize(jsonPlayerDataAll.parties);
              instance.Deserialize(jsonPlayerDataAll.notify);
              instance.Deserialize(jsonPlayerDataAll.artifacts);
              instance.Deserialize(jsonPlayerDataAll.skins);
              instance.Deserialize(jsonPlayerDataAll.vs);
              instance.Deserialize(jsonPlayerDataAll.player_guild);
              if (jsonPlayerDataAll.tips != null)
                instance.Tips = ((IEnumerable<string>) jsonPlayerDataAll.tips).ToList<string>();
              if (instance.Player != null)
              {
                instance.Player.SetRuneStorageNum(jsonPlayerDataAll.rune_storage);
                instance.Player.SetRuneStorageUsedNum(jsonPlayerDataAll.rune_storage_used);
                instance.Player.Deserialize(jsonPlayerDataAll.story_ex_challenge);
                instance.Player.Deserialize(jsonPlayerDataAll.expansions);
              }
            }
            catch (Exception ex)
            {
              DebugUtility.LogException(ex);
              this.Failure();
              return;
            }
          }
          ((Behaviour) this).enabled = false;
          GlobalVars.BtlID.Set(jsonPlayerDataAll.player.btlid);
          if (!string.IsNullOrEmpty(jsonPlayerDataAll.player.btltype))
            GlobalVars.QuestType = QuestParam.ToQuestType(jsonPlayerDataAll.player.btltype);
          FlowNode_ExpireItemNotify.ResetParam();
          if (jsonPlayerDataAll.expire_items != null)
            FlowNode_ExpireItemNotify.Deserialize(jsonPlayerDataAll.expire_items);
          GameUtility.Config_OkyakusamaCode = instance.Player.OkyakusamaCode;
          if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV))
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV, MonoSingleton<GameManager>.Instance.Player.Lv);
          if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV))
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV, MonoSingleton<GameManager>.Instance.Player.VipRank);
          instance.PostLogin();
          PlayerData player = MonoSingleton<GameManager>.Instance.Player;
          if (player != null)
            MyGrowthPush.registCustomerId(player.OkyakusamaCode);
          CoinBuyUseBonusWindow.ResetParam();
          this.ActivateOutputLinks(!string.IsNullOrEmpty(jsonPlayerDataAll.player.name) ? 3 : 2);
        }
      }
    }

    private void SetUseDLC_EvalOnly(PlayerData player, bool _value)
    {
    }

    [MessagePackObject(true)]
    public class MP_PlayerDataAll : WebAPI.JSON_BaseResponse
    {
      public Json_PlayerDataAll body;
    }
  }
}
