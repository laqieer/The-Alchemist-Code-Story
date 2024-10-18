// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildRaidBtlLog
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
  [FlowNode.NodeType("GuildRaid/Req/GuildRaidBtlLog", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqGuildRaidBtlLog : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || ((Behaviour) this).enabled)
        return;
      ((Behaviour) this).enabled = true;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildRaidBattleLogWindow.Instance, (UnityEngine.Object) null))
      {
        this.OnFailed();
      }
      else
      {
        GuildRaidBattleLogWindow instance = GuildRaidBattleLogWindow.Instance;
        if (instance.NextPage <= instance.CurrentPage)
        {
          this.Success();
        }
        else
        {
          this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
          if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null)
            this.OnFailed();
          else
            this.ExecRequest((WebAPI) new ReqGuildRaidBtlLog(MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid, instance.NextPage, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
        }
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
      ReqGuildRaidBtlLog.Response response = (ReqGuildRaidBtlLog.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqGuildRaidBtlLog.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqGuildRaidBtlLog.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
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
          WebAPI.JSON_BodyResponse<ReqGuildRaidBtlLog.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGuildRaidBtlLog.Response>>(www.text);
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
          GuildRaidBattleLogWindow instance = GuildRaidBattleLogWindow.Instance;
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
            throw new Exception("GuildRaidBattleLogWindow: instance is null");
          bool isOverwrite = instance.NextPage == 1;
          GuildRaidBattleLogWindow.Instance.SetupBattleLog(response.battle_logs, isOverwrite);
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
      public ReqGuildRaidBtlLog.Response body;
    }
  }
}
