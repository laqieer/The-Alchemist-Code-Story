// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGachaPickup
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
  [FlowNode.NodeType("System/Gacha/Pickup", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(20, "NotSetGachaId", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(21, "NoGacha", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(22, "NotPickupSelect", FlowNode.PinTypes.Output, 22)]
  [FlowNode.Pin(23, "NotPickupItems", FlowNode.PinTypes.Output, 23)]
  public class FlowNode_ReqGachaPickup : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 1;
    private const int PIN_OUT_SUCCESS = 10;
    private const int PIN_OUT_NOT_SET_GACHAID = 20;
    private const int PIN_OUT_NOGACHA = 21;
    private const int PIN_OUT_NOT_PICKUP_SELECT = 22;
    private const int PIN_OUT_NOT_PICKUP_ITEMS = 23;
    private string select_gachaid = string.Empty;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || ((Behaviour) this).enabled)
        return;
      this.select_gachaid = !UnityEngine.Object.op_Inequality((UnityEngine.Object) GachaWindow.Instance, (UnityEngine.Object) null) ? string.Empty : GachaWindow.Instance.GetCurrentGachaId();
      if (string.IsNullOrEmpty(this.select_gachaid))
        this.ActivateOutputLinks(20);
      else if (GachaWindow.Instance.IsAlreadyPickupSelectList(this.select_gachaid))
      {
        this.ActivateOutputLinks(10);
      }
      else
      {
        ReqGachaPickup.RequestParam rp = new ReqGachaPickup.RequestParam();
        rp.gachaid = this.select_gachaid;
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        this.ExecRequest((WebAPI) new ReqGachaPickup(rp, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
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
      ReqGachaPickup.Response response = (ReqGachaPickup.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqGachaPickup.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqGachaPickup.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
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
          case SRPG.Network.EErrCode.NoGacha:
            this.ActivateOutputLinks(21);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.NotPickupSelect:
            this.ActivateOutputLinks(22);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.NotPickupItems:
            this.ActivateOutputLinks(23);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
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
          WebAPI.JSON_BodyResponse<ReqGachaPickup.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGachaPickup.Response>>(www.text);
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
            GachaWindow.Instance.SetupGachaSelectList(this.select_gachaid, response.pickups, response.pickup_select_num);
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
      public ReqGachaPickup.Response body;
    }
  }
}
