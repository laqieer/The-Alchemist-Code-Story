// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildRaidReportSelfShare
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("GuildRaid/Req/GuildRaidReportSelfShare", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqGuildRaidReportSelfShare : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || ((Behaviour) this).enabled)
        return;
      ((Behaviour) this).enabled = true;
      if (GuildRaidReportInfo.CurrentReportId == 0)
      {
        this.OnFailed();
      }
      else
      {
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null)
          this.OnFailed();
        else
          this.ExecRequest((WebAPI) new ReqGuildRaidReportSelfShare(MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid, GuildRaidReportInfo.CurrentReportId, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
      }
    }

    private void Success()
    {
      if (Object.op_Equality((Object) this, (Object) null))
        return;
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (!EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
      {
        WebAPI.JSON_BaseResponse jsonBaseResponse = SerializerCompressorHelper.Decode<WebAPI.JSON_BaseResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(jsonBaseResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) jsonBaseResponse.stat;
        string statMsg = jsonBaseResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
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
        SRPG.Network.RemoveAPI();
        this.Success();
      }
    }
  }
}
