// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqUnitRentalLeave
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
  [FlowNode.NodeType("UnitRental/Req/Leave", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqUnitRentalLeave : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      UnitRentalParam unitRentalParam = (UnitRentalParam) null;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
      {
        UnitData rentalUnit = instance.Player.GetRentalUnit();
        if (rentalUnit != null)
          unitRentalParam = UnitRentalParam.GetParam(rentalUnit.RentalIname);
      }
      if (unitRentalParam != null)
        this.ExecRequest((WebAPI) new ReqUnitRentalLeave(unitRentalParam.Iname, unitRentalParam.UnitId, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
      else
        this.OnFailed();
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqUnitRentalLeave.Response response = (ReqUnitRentalLeave.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqUnitRentalLeave.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqUnitRentalLeave.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
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
          case SRPG.Network.EErrCode.UnitRental_FavpointMax:
          case SRPG.Network.EErrCode.UnitRental_NotFoundUnit:
            this.OnFailed();
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
          WebAPI.JSON_BodyResponse<ReqUnitRentalLeave.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqUnitRentalLeave.Response>>(www.text);
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
            GameManager instance = MonoSingleton<GameManager>.Instance;
            if (!string.IsNullOrEmpty(response.leave_unit_iname))
              instance.Player.RemoveRentalUnit(response.leave_unit_iname);
            if (response.return_items != null)
              UnitRentalParam.EntryLeaveReturnItems(response.return_items);
            if (response.items != null)
              instance.Deserialize(response.items);
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
      public ReqUnitRentalLeave.Response body;
    }
  }
}
