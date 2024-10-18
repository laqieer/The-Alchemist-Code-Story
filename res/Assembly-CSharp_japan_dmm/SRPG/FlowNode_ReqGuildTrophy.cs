// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildTrophy
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
  [FlowNode.NodeType("GuildTrophy/Req/ReqGuildTrophy", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "RequestSpan", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(1000, "Cancel", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "ポート未所属", FlowNode.PinTypes.Output, 1001)]
  public class FlowNode_ReqGuildTrophy : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_IN_REQUEST_SPAN = 2;
    protected const int PIN_OUT_SUCCESS = 101;
    protected const int PIN_OUT_CANCEL = 1000;
    protected const int PIN_OUT_NOT_JOINED = 1001;
    private static FlowNode_ReqGuildTrophy.AfterCertainPeriodTime mAfterCertainPeriodTime = new FlowNode_ReqGuildTrophy.AfterCertainPeriodTime();

    public static void ResetTimer() => FlowNode_ReqGuildTrophy.mAfterCertainPeriodTime.Clear();

    public ReqGuildTrophy.RequestParam CreateReqGetGuildTrophy()
    {
      return new ReqGuildTrophy.RequestParam();
    }

    public override void OnActivate(int pinID)
    {
      ReqGuildTrophy.RequestParam reqGetGuildTrophy = this.CreateReqGetGuildTrophy();
      if (reqGetGuildTrophy == null)
        return;
      if (!MonoSingleton<GameManager>.Instance.Player.IsGuildAssign)
      {
        this.ActivateOutputLinks(1000);
      }
      else
      {
        switch (pinID)
        {
          case 1:
            ((Behaviour) this).enabled = true;
            this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
            this.ExecRequest((WebAPI) new ReqGuildTrophy(reqGetGuildTrophy, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
            break;
          case 2:
            if (!FlowNode_ReqGuildTrophy.mAfterCertainPeriodTime.IsAfterCertainPeriodTime())
            {
              this.ActivateOutputLinks(1000);
              break;
            }
            ((Behaviour) this).enabled = true;
            this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
            this.ExecRequest((WebAPI) new ReqGuildTrophy(reqGetGuildTrophy, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
            break;
        }
      }
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqGuildTrophy.Response response = (ReqGuildTrophy.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqGuildTrophy.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqGuildTrophy.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
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
          UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(1001)), systemModal: true);
        }
        else
          this.OnRetry();
      }
      else
      {
        if (flag)
        {
          WebAPI.JSON_BodyResponse<ReqGuildTrophy.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGuildTrophy.Response>>(www.text);
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
            MonoSingleton<GameManager>.Instance.Player.GuildTrophyData.OverwriteGuildTrophyProgress(response.guild_trophies, false);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnFailed();
            return;
          }
          FlowNode_ReqGuildTrophy.mAfterCertainPeriodTime.TimeUpdate();
          SRPG.Network.RemoveAPI();
          this.Success();
        }
      }
    }

    public class AfterCertainPeriodTime
    {
      private DateTime mDateTime = DateTime.MinValue;
      private const int PERIOD_TIME = 1;

      public void Clear() => this.mDateTime = DateTime.MinValue;

      public void TimeUpdate() => this.mDateTime = TimeManager.ServerTime;

      public bool IsAfterCertainPeriodTime()
      {
        return this.mDateTime.AddHours(1.0) < TimeManager.ServerTime;
      }
    }

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqGuildTrophy.Response body;
    }
  }
}
