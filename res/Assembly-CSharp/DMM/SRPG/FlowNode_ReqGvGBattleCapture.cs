// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGvGBattleCapture
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
  [FlowNode.NodeType("GvG/Req/GvGBattleCapture", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqGvGBattleCapture : FlowNode_Network
  {
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
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null))
        this.OnFailed();
      else
        this.ExecRequest((WebAPI) new ReqGvGBattleCapture(GvGManager.Instance.SelectNodeId, MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid, GvGManager.GvGGroupId, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
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
      ReqGvGBattleCapture.Response response = (ReqGvGBattleCapture.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqGvGBattleCapture.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqGvGBattleCapture.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
        string statMsg = mpResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        response = mpResponse.body;
      }
      if (SRPG.Network.IsError)
      {
        SRPG.Network.EErrCode errCode = SRPG.Network.ErrCode;
        switch (errCode)
        {
          case SRPG.Network.EErrCode.GvG_HasCapture:
          case SRPG.Network.EErrCode.GvG_CanNotCapture:
          case SRPG.Network.EErrCode.GvG_BattleHalfTime:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            ((Behaviour) this).enabled = false;
            UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => GlobalEvent.Invoke("RELOAD_GVG", (object) null)), systemModal: true);
            break;
          default:
            if (errCode != SRPG.Network.EErrCode.GvG_OutOfPeriod && errCode != SRPG.Network.EErrCode.GvG_NotGvGEntry)
            {
              if (errCode != SRPG.Network.EErrCode.Guild_NotJoined)
              {
                if (errCode != SRPG.Network.EErrCode.GvG_BtlOutOfPeriod)
                {
                  this.OnRetry();
                  break;
                }
                goto case SRPG.Network.EErrCode.GvG_HasCapture;
              }
              else
              {
                SRPG.Network.RemoveAPI();
                SRPG.Network.ResetError();
                ((Behaviour) this).enabled = false;
                UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => GlobalEvent.Invoke("MENU_HOME", (object) null)), systemModal: true);
                break;
              }
            }
            else
            {
              SRPG.Network.RemoveAPI();
              SRPG.Network.ResetError();
              ((Behaviour) this).enabled = false;
              UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => GlobalEvent.Invoke("BACK_GUILD", (object) null)), systemModal: true);
              break;
            }
        }
      }
      else
      {
        if (flag)
        {
          WebAPI.JSON_BodyResponse<ReqGvGBattleCapture.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGvGBattleCapture.Response>>(www.text);
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
          GvGManager instance = GvGManager.Instance;
          instance.SetupNodeDataList(response.nodes);
          instance.SetupRefreshWait(response.refresh_wait_sec);
          instance.SetupAutoRefreshWait(response.auto_refresh_wait_sec);
          instance.SetupUsedUnitList(response.used_units);
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
      public ReqGvGBattleCapture.Response body;
    }
  }
}
