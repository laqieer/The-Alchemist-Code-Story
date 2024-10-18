// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildAttend
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
  [FlowNode.NodeType("Guild/Req/Attend", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "報酬受け取り成功", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "報酬受け取り済み", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(110, "出席不可", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(200, "加入日は出席不可", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(201, "出席期間外", FlowNode.PinTypes.Output, 201)]
  public class FlowNode_ReqGuildAttend : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 1;
    private const int PIN_OUT_SUCCESS = 101;
    private const int PIN_OUT_SUCCESSED = 102;
    private const int PIN_OUT_CAN_NOT_ATTENDED = 110;
    private const int PIN_OUT_NOT_ATTEND_JOIN_DAY = 200;
    private const int PIN_OUT_OUT_OF_ATTEND_PERIOD = 201;
    private static ReqGuildAttend.Response mKeepResponse;

    public static ReqGuildAttend.Response GetResponse() => FlowNode_ReqGuildAttend.mKeepResponse;

    public static void Clear()
    {
      FlowNode_ReqGuildAttend.mKeepResponse = (ReqGuildAttend.Response) null;
    }

    public override void OnActivate(int pinID)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GuildManager.Instance, (UnityEngine.Object) null) && GuildManager.Instance.AttendStatus == GuildManager.GuildAttendStatus.CAN_NOT_ATTENDED)
      {
        this.ActivateOutputLinks(110);
      }
      else
      {
        ReqGuildAttend.RequestParam request = this.CreateRequest();
        if (request == null || pinID != 1)
          return;
        ((Behaviour) this).enabled = true;
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        this.ExecRequest((WebAPI) new ReqGuildAttend(request, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
      }
    }

    protected override void OnDestroy() => FlowNode_ReqGuildAttend.Clear();

    public ReqGuildAttend.RequestParam CreateRequest()
    {
      if (!MonoSingleton<GameManager>.Instance.Player.IsGuildAssign)
        return (ReqGuildAttend.RequestParam) null;
      return new ReqGuildAttend.RequestParam()
      {
        gid = (long) MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid
      };
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(FlowNode_ReqGuildAttend.mKeepResponse == null || FlowNode_ReqGuildAttend.mKeepResponse.rewards == null || FlowNode_ReqGuildAttend.mKeepResponse.rewards.Length <= 0 ? 102 : 101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqGuildAttend.Response response = (ReqGuildAttend.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqGuildAttend.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqGuildAttend.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
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
          case SRPG.Network.EErrCode.Guild_NotAttendJoinDay:
            this.ActivateOutputLinks(200);
            ((Behaviour) this).enabled = false;
            SRPG.Network.ResetError();
            SRPG.Network.RemoveAPI();
            break;
          case SRPG.Network.EErrCode.Guild_OutOfAttendPeriod:
            this.ActivateOutputLinks(201);
            ((Behaviour) this).enabled = false;
            SRPG.Network.ResetError();
            SRPG.Network.RemoveAPI();
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
          WebAPI.JSON_BodyResponse<ReqGuildAttend.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGuildAttend.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          response = jsonObject.body;
        }
        if (response == null)
        {
          this.OnFailed();
        }
        else
        {
          try
          {
            if (response.player != null)
              MonoSingleton<GameManager>.Instance.Deserialize(response.player);
            if (response.items != null)
              MonoSingleton<GameManager>.Instance.Deserialize(response.items);
            if (response.cards != null && response.cards.Length > 0)
              MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
            if (response.runes != null && response.runes.Length > 0)
            {
              MonoSingleton<GameManager>.Instance.Player.OnDirtyRuneData();
              MonoSingleton<GameManager>.Instance.Player.SetRuneStorageUsedNum(response.rune_storage_used);
            }
            if (response.guild_trophies != null)
              MonoSingleton<GameManager>.Instance.Player.GuildTrophyData.OverwriteGuildTrophyProgress(response.guild_trophies);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GuildManager.Instance, (UnityEngine.Object) null))
              GuildManager.Instance.SetGuildAttendStatus(GuildManager.GuildAttendStatus.ATTENDED);
            FlowNode_ReqGuildAttend.mKeepResponse = response;
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnFailed();
            return;
          }
          SRPG.Network.RemoveAPI();
          this.Success();
        }
      }
    }

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqGuildAttend.Response body;
    }
  }
}
