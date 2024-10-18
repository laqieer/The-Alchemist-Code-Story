// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AllEnhanceEquip
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
  [FlowNode.NodeType("System/Unit/AllEnhanceEquip", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(201, "Faild", FlowNode.PinTypes.Output, 201)]
  public class FlowNode_AllEnhanceEquip : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    protected const int PIN_OUT_FAILD = 201;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || ((Behaviour) this).enabled)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      this.ExecRequest((WebAPI) new ReqAllEquipExpAdd(MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID).Jobs[(int) GlobalVars.SelectedUnitJobIndex].UniqueID, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
    }

    private void Success()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) ((Component) ((Component) this).transform.root).gameObject, (UnityEngine.Object) null))
        MonoSingleton<GameManager>.Instance.Player.OnSoubiPowerUp(DataSource.FindDataOfClass<int>(((Component) ((Component) this).transform.root).gameObject, 0));
      else
        MonoSingleton<GameManager>.Instance.Player.OnSoubiPowerUp();
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    private void Failure()
    {
      SRPG.Network.RemoveAPI();
      SRPG.Network.ResetError();
      ((Behaviour) this).enabled = false;
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqAllEquipExpAdd.Response response = (ReqAllEquipExpAdd.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_AllEnhanceEquip.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_AllEnhanceEquip.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
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
          case SRPG.Network.EErrCode.NoJobEnforceEquip:
          case SRPG.Network.EErrCode.NoEquipEnforce:
          case SRPG.Network.EErrCode.ForceMax:
          case SRPG.Network.EErrCode.MaterialShort:
          case SRPG.Network.EErrCode.EnforcePlayerLvShort:
            this.Failure();
            UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) null, systemModal: true);
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
          WebAPI.JSON_BodyResponse<ReqAllEquipExpAdd.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqAllEquipExpAdd.Response>>(www.text);
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
          MonoSingleton<GameManager>.Instance.Deserialize(response.player);
          MonoSingleton<GameManager>.Instance.Deserialize(response.items);
          MonoSingleton<GameManager>.Instance.Deserialize(response.units);
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
      public ReqAllEquipExpAdd.Response body;
    }
  }
}
