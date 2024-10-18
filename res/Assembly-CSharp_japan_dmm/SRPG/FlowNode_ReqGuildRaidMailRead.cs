// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildRaidMailRead
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("GuildRaid/Req/GuildRaidMailRead", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(300, "Not Joined", FlowNode.PinTypes.Output, 300)]
  [FlowNode.Pin(301, "Out of Period", FlowNode.PinTypes.Output, 301)]
  [FlowNode.Pin(331, "Already Mail Received", FlowNode.PinTypes.Output, 331)]
  [FlowNode.Pin(332, "Mail Not Received", FlowNode.PinTypes.Output, 332)]
  [FlowNode.Pin(401, "Mail ID Dupulicate", FlowNode.PinTypes.Output, 401)]
  public class FlowNode_ReqGuildRaidMailRead : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    protected const int PIN_OUT_NOT_JOINED = 300;
    protected const int PIN_OUT_OUT_OF_PERIOD = 301;
    protected const int PIN_OUT_ALREADY_MAIL_RECEIVED = 331;
    protected const int PIN_OUT_MAIL_NOT_RECEIVED = 332;
    protected const int PIN_OUT_MAIL_ID_DUPULICATE = 401;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || ((Behaviour) this).enabled)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null)
        this.OnFailed();
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null) || GuildRaidManager.Instance.MailReceivingIdList == null || GuildRaidManager.Instance.MailReceivingIdList.Count <= 0)
        this.OnFailed();
      else
        this.ExecRequest((WebAPI) new ReqGuildRaidMailRead(GuildRaidManager.Instance.MailReceivingIdList.ToArray(), GuildRaidManager.Instance.MailCurrentPage, MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
    }

    private void Success()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
        return;
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqGuildRaidMailRead.Response response = (ReqGuildRaidMailRead.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqGuildRaidMailRead.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqGuildRaidMailRead.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
        string statMsg = mpResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        response = mpResponse.body;
      }
      if (SRPG.Network.IsError)
      {
        switch (SRPG.Network.ErrCode)
        {
          case SRPG.Network.EErrCode.MailIDDupulicate:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(401)), systemModal: true);
            break;
          case SRPG.Network.EErrCode.Guild_NotJoined:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(300)), systemModal: true);
            break;
          case SRPG.Network.EErrCode.GuildRaid_OutOfPeriod:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(301)), systemModal: true);
            break;
          case SRPG.Network.EErrCode.GuildRaid_AlreadyMailReceived:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(331)), systemModal: true);
            break;
          case SRPG.Network.EErrCode.GuildRaid_MailNotReceived:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(332)), systemModal: true);
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        if (flag)
        {
          WebAPI.JSON_BodyResponse<ReqGuildRaidMailRead.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGuildRaidMailRead.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnRetry();
            return;
          }
          response = jsonObject.body;
        }
        SRPG.Network.RemoveAPI();
        try
        {
          GuildRaidManager.Instance.SetupMail(response.mails);
          GuildRaidManager.Instance.SetupMailReceived(response.processed);
          GuildRaidManager.Instance.SetupMailToGiftList(response.gift_mailids);
          MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(response.trophyprogs);
          MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(response.bingoprogs);
          MonoSingleton<GameManager>.Instance.Player.Deserialize(response.player);
          MonoSingleton<GameManager>.Instance.Player.Deserialize(response.items);
          MonoSingleton<GameManager>.Instance.Player.Deserialize(response.units);
          if (response.artifacts != null)
            MonoSingleton<GameManager>.Instance.Deserialize(response.artifacts);
          if (response.cards != null)
            MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          this.OnFailed();
          return;
        }
        this.Success();
      }
    }

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqGuildRaidMailRead.Response body;
    }
  }
}
