// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildRoleBonus
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
  [FlowNode.NodeType("Guild/Req/RoleBonus", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(200, "受け取れるRoleじゃない", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(201, "報酬期間外", FlowNode.PinTypes.Output, 201)]
  public class FlowNode_ReqGuildRoleBonus : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 1;
    private const int PIN_OUT_SUCCESS = 101;
    private const int PIN_OUT_MASTER_ONLY = 200;
    private const int PIN_OUT_OUT_OF_MASTER_REWARD_PERIOD = 201;
    private static ReqGuildRoleBonus.Response mKeepResponse;

    public static ReqGuildRoleBonus.Response GetResponse()
    {
      return FlowNode_ReqGuildRoleBonus.mKeepResponse;
    }

    public static void Clear()
    {
      FlowNode_ReqGuildRoleBonus.mKeepResponse = (ReqGuildRoleBonus.Response) null;
    }

    public override void OnActivate(int pinID)
    {
      ReqGuildRoleBonus.RequestParam request = this.CreateRequest();
      if (request == null || pinID != 1)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      this.ExecRequest((WebAPI) new ReqGuildRoleBonus(request, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
    }

    public ReqGuildRoleBonus.RequestParam CreateRequest()
    {
      if (!MonoSingleton<GameManager>.Instance.Player.IsGuildAssign)
        return (ReqGuildRoleBonus.RequestParam) null;
      return new ReqGuildRoleBonus.RequestParam()
      {
        gid = (long) MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid
      };
    }

    public void Success()
    {
      ((Behaviour) this).enabled = false;
      MonoSingleton<GameManager>.Instance.Player.HasGuildReward = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqGuildRoleBonus.Response response = (ReqGuildRoleBonus.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqGuildRoleBonus.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqGuildRoleBonus.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
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
          case SRPG.Network.EErrCode.Guild_MasterOnly:
            this.ActivateOutputLinks(200);
            ((Behaviour) this).enabled = false;
            SRPG.Network.ResetError();
            SRPG.Network.RemoveAPI();
            break;
          case SRPG.Network.EErrCode.Guild_OutOfMasterRewarPeriod:
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
          WebAPI.JSON_BodyResponse<ReqGuildRoleBonus.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGuildRoleBonus.Response>>(www.text);
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
            FlowNode_ReqGuildRoleBonus.mKeepResponse = response;
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
      public ReqGuildRoleBonus.Response body;
    }
  }
}
