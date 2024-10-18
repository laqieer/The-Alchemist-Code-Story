// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGachaSetPickup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Gacha/Pickup/Set", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "NoGacha", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "NotPickupSelect", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "IncorrectPickups", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(14, "CanNotChangePickups", FlowNode.PinTypes.Output, 14)]
  public class FlowNode_ReqGachaSetPickup : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 1;
    private const int PIN_OUT_SUCCESS = 10;
    private const int PIN_OUT_NOGACHA = 11;
    private const int PIN_OUT_NOT_PICKUP_SELECT = 12;
    private const int PIN_OUT_INCORRECT_PICKUPS = 13;
    private const int PIN_OUT_CANNOT_CHANGE_PICKUPS = 14;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      ReqGachaSetPickup.RequestParam request = this.CreateRequest();
      if (request == null)
        return;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      this.ExecRequest((WebAPI) new ReqGachaSetPickup(request, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
      ((Behaviour) this).enabled = true;
    }

    public ReqGachaSetPickup.RequestParam CreateRequest()
    {
      if (Object.op_Equality((Object) GachaWindow.Instance, (Object) null) || GachaWindow.Instance.RequestPickupSelectDatas == null)
        return (ReqGachaSetPickup.RequestParam) null;
      GachaPickupSelectParam pickupSelectParam = GachaWindow.Instance.GetCurrentGachaPickupSelectParam();
      if (pickupSelectParam == null)
        return (ReqGachaSetPickup.RequestParam) null;
      GachaTopParamNew[] currentGacha = GachaWindow.Instance.GetCurrentGacha();
      if (currentGacha == null || currentGacha.Length <= 0)
        return (ReqGachaSetPickup.RequestParam) null;
      List<GachaDropData> pickupSelectDatas = GachaWindow.Instance.RequestPickupSelectDatas;
      if (pickupSelectParam.select_pickup_num != pickupSelectDatas.Count)
        return (ReqGachaSetPickup.RequestParam) null;
      ReqGachaSetPickup.RequestParam request = new ReqGachaSetPickup.RequestParam();
      request.gachaid = currentGacha[0].iname;
      request.pickups = new ReqGachaSetPickup.PickupData[pickupSelectDatas.Count];
      for (int index = 0; index < pickupSelectDatas.Count; ++index)
        request.pickups[index] = new ReqGachaSetPickup.PickupData()
        {
          itype = pickupSelectDatas[index].Itype,
          iname = pickupSelectDatas[index].Iname
        };
      return request;
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(10);
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
        SRPG.Network.EErrCode errCode = SRPG.Network.ErrCode;
        switch (errCode)
        {
          case SRPG.Network.EErrCode.NotPickupSelect:
            this.ActivateOutputLinks(12);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.IncorrectPickups:
            this.ActivateOutputLinks(13);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.CanNotChangePickupItems:
            this.ActivateOutputLinks(14);
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          default:
            if (errCode == SRPG.Network.EErrCode.NoGacha)
            {
              this.ActivateOutputLinks(11);
              ((Behaviour) this).enabled = false;
              SRPG.Network.RemoveAPI();
              SRPG.Network.ResetError();
              break;
            }
            this.OnRetry();
            break;
        }
      }
      else
      {
        SRPG.Network.RemoveAPI();
        this.Success();
      }
    }
  }
}
