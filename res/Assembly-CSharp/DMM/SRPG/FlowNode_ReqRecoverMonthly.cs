// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRecoverMonthly
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
  [FlowNode.NodeType("LoginBonus/Req/Recover", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(20, "OutOfPeriod", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(21, "RemainRecoverShort", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(22, "AlreadyReceived", FlowNode.PinTypes.Output, 22)]
  [FlowNode.Pin(23, "CanNotRecoverFuture", FlowNode.PinTypes.Output, 23)]
  public class FlowNode_ReqRecoverMonthly : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 0;
    private const int PIN_OT_SUCCESS = 10;
    private const int PIN_OT_OUT_OF_PERIOD = 20;
    private const int PIN_OT_REMAIN_RECOVER_SHORT = 21;
    private const int PIN_OT_ALREADY_RECEIVED = 22;
    private const int PIN_OT_CAN_NOT_RECOVER_FUTURE = 23;

    public override void OnActivate(int pinID)
    {
      if (string.IsNullOrEmpty(GlobalVars.MonthlyLoginBonus_SelectTableIname))
        DebugUtility.LogError("補填したいログインボーナスのinameが指定されていません.");
      else if (GlobalVars.MonthlyLoginBonus_SelectRecoverDay < 0)
      {
        DebugUtility.LogError("補填したいログインボーナスの日にちが指定されていません.");
      }
      else
      {
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        this.ExecRequest((WebAPI) new ReqMonthlyRecover(GlobalVars.MonthlyLoginBonus_SelectTableIname, GlobalVars.MonthlyLoginBonus_SelectRecoverDay, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
        ((Behaviour) this).enabled = true;
      }
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(10);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqMonthlyRecover.Response response = (ReqMonthlyRecover.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqRecoverMonthly.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqRecoverMonthly.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
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
          case SRPG.Network.EErrCode.MonthlyLoginBonus_OutOfPeriod:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            this.ActivateOutputLinks(20);
            ((Behaviour) this).enabled = false;
            break;
          case SRPG.Network.EErrCode.MonthlyLoginBonus_RemainRecoverShort:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            this.ActivateOutputLinks(21);
            ((Behaviour) this).enabled = false;
            break;
          case SRPG.Network.EErrCode.MonthlyLoginBonus_AlreadyReceived:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            this.ActivateOutputLinks(22);
            ((Behaviour) this).enabled = false;
            break;
          case SRPG.Network.EErrCode.MonthlyLoginBonus_CanNotRecoverFuture:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            this.ActivateOutputLinks(22);
            ((Behaviour) this).enabled = false;
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
          WebAPI.JSON_BodyResponse<ReqMonthlyRecover.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMonthlyRecover.Response>>(www.text);
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
            MonoSingleton<GameManager>.Instance.Deserialize(response.notify);
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
      public ReqMonthlyRecover.Response body;
    }
  }
}
