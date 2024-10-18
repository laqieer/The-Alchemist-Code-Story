// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildRaidRankingPort
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
  [FlowNode.NodeType("GuildRaid/Req/GuildRaidRankingPort", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqGuildRaidRankingPort : FlowNode_Network
  {
    [SerializeField]
    private bool Overwrite;
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || ((Behaviour) this).enabled)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null)
        this.OnFailed();
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null))
      {
        this.OnFailed();
      }
      else
      {
        if (this.Overwrite)
          GuildRaidManager.Instance.CurrentRankingPortPage = GuildRaidManager.Instance.PreviousRankingPortPage = 1;
        this.ExecRequest((WebAPI) new ReqGuildRaidRankingPort(MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid, GuildRaidManager.Instance.mRankingType != GuildRaidManager.GuildRaidRankingType.Current ? GuildRaidManager.Instance.PreviousRankingPortPage : GuildRaidManager.Instance.CurrentRankingPortPage, GuildRaidManager.Instance.mRankingType, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
      }
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
      ReqGuildRaidRankingPort.Response response = (ReqGuildRaidRankingPort.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqGuildRaidRankingPort.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqGuildRaidRankingPort.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
        string statMsg = mpResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        response = mpResponse.body;
      }
      if (SRPG.Network.IsError)
      {
        if (SRPG.Network.ErrCode == SRPG.Network.EErrCode.Guild_NotJoined)
        {
          SRPG.Network.RemoveAPI();
          SRPG.Network.ResetError();
          ((Behaviour) this).enabled = false;
          UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => GlobalEvent.Invoke("MENU_HOME", (object) null)), systemModal: true);
        }
        else
          this.OnRetry();
      }
      else
      {
        if (flag)
        {
          WebAPI.JSON_BodyResponse<ReqGuildRaidRankingPort.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGuildRaidRankingPort.Response>>(www.text);
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
          if (GuildRaidManager.Instance.mRankingType == GuildRaidManager.GuildRaidRankingType.Current)
          {
            GuildRaidManager.Instance.SetupCurrentRankingPortList(response.ranking_port, this.Overwrite);
            GuildRaidManager.Instance.SetupCurrentRankingPortSelf(response.my_info);
            GuildRaidManager.Instance.CurrentRankingPortPageTotal = response.totalPage;
          }
          else
          {
            GuildRaidManager.Instance.SetupPreviousRankingPortList(response.ranking_port, this.Overwrite);
            GuildRaidManager.Instance.SetupPreviousRankingPortSelf(response.my_info);
            GuildRaidManager.Instance.PreviousRankingPortPageTotal = response.totalPage;
          }
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
      public ReqGuildRaidRankingPort.Response body;
    }
  }
}
