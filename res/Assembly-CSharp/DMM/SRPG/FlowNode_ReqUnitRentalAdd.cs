// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqUnitRentalAdd
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
  [FlowNode.NodeType("UnitRental/Req/Add", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqUnitRentalAdd : FlowNode_Network
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
        this.ExecRequest((WebAPI) new ReqUnitRentalAdd(unitRentalParam.Iname, unitRentalParam.UnitId, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
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
      ReqUnitRentalAdd.Response response = (ReqUnitRentalAdd.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqUnitRentalAdd.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqUnitRentalAdd.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
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
          case SRPG.Network.EErrCode.UnitRental_AlreadyJoin:
          case SRPG.Network.EErrCode.UnitRental_FavpointShort:
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
          WebAPI.JSON_BodyResponse<ReqUnitRentalAdd.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqUnitRentalAdd.Response>>(www.text);
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
            if (response.units != null && response.units.Length != 0)
            {
              instance.Deserialize(response.units);
              UnitData unitDataByUnitId = instance.Player.FindUnitDataByUnitID(response.units[0].iname);
              if (unitDataByUnitId != null)
                UnitRentalParam.GetUnitData = unitDataByUnitId;
            }
            else
            {
              this.OnFailed();
              return;
            }
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnFailed();
            return;
          }
          SRPG.Network.RemoveAPI();
          MonoSingleton<GameManager>.Instance.Player.OnUnitGet();
          this.Success();
        }
      }
    }

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqUnitRentalAdd.Response body;
    }
  }
}
