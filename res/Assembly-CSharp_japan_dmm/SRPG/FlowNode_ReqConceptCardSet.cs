// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqConceptCardSet
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
  [FlowNode.NodeType("ConceptCard/Req/ReqConceptCardSet", 32741)]
  [FlowNode.Pin(0, "装備", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "装備した", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ReqConceptCardSet : FlowNode_Network
  {
    private const int INPUT_CONCEPT_CARD_SET = 0;
    private const int OUTPUT_CONCEPT_CARD_SET = 100;
    private long[] mEquipCardIds;
    private long mTargetUnitId;

    public void SetEquipParam(long[] equip_card_iids, long unit_iid)
    {
      this.ResetParam();
      this.mEquipCardIds = equip_card_iids;
      this.mTargetUnitId = unit_iid;
    }

    public void SetReleaseParam(long[] equip_card_iids)
    {
      this.ResetParam();
      this.mEquipCardIds = equip_card_iids;
    }

    private void ResetParam()
    {
      this.mEquipCardIds = new long[2];
      for (int index = 0; index < this.mEquipCardIds.Length; ++index)
        this.mEquipCardIds[index] = 0L;
      this.mTargetUnitId = 0L;
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      ((Behaviour) this).enabled = true;
      if (!UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
        this.ExecRequest((WebAPI) ReqSetConceptCard.CreateSet(this.mEquipCardIds, this.mTargetUnitId, new SRPG.Network.ResponseCallback(this.ConceptCardSetResponseCallback)));
      else
        this.ExecRequest((WebAPI) new ReqSetConceptCard_OverWrite(this.mEquipCardIds, this.mTargetUnitId, (eOverWritePartyType) GlobalVars.OverWritePartyType, new SRPG.Network.ResponseCallback(this.ConceptCardSetResponseCallback), EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (SRPG.Network.IsError)
      {
        int errCode = (int) SRPG.Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        if (!UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
          this.Success_Simple(www);
        else
          this.Success_OverWrite(www);
        ((Behaviour) this).enabled = false;
      }
    }

    private void Success_Simple(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      SRPG.Network.RemoveAPI();
      try
      {
        MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
        MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
        MonoSingleton<GameManager>.Instance.Player.UpdateConceptCardEquipedSlots(jsonObject.body.units);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) UnitEnhanceV3.Instance, (UnityEngine.Object) null))
          return;
        UnitEnhanceV3.Instance.OnEquipConceptCardSelect();
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        this.OnFailed();
      }
    }

    private void Success_OverWrite(WWWResult www)
    {
      ReqSetConceptCard_OverWrite.Response body;
      if (EncodingTypes.IsJsonSerializeCompressSelected(!GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK : GlobalVars.SelectedSerializeCompressMethod))
      {
        WebAPI.JSON_BodyResponse<ReqSetConceptCard_OverWrite.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqSetConceptCard_OverWrite.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "jsonRes == null");
        body = jsonObject.body;
      }
      else
      {
        FlowNode_ReqConceptCardSet.MP_SetConceptCard_OverWriteResponse overWriteResponse = SerializerCompressorHelper.Decode<FlowNode_ReqConceptCardSet.MP_SetConceptCard_OverWriteResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
        DebugUtility.Assert(overWriteResponse != null, "mpRes == null");
        body = overWriteResponse.body;
      }
      SRPG.Network.RemoveAPI();
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(body.party_decks);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        this.OnFailed();
      }
    }

    private void ConceptCardSetResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      this.OnSuccess(www);
      this.ActivateOutputLinks(100);
    }

    [MessagePackObject(true)]
    public class MP_SetConceptCard_OverWriteResponse : WebAPI.JSON_BaseResponse
    {
      public ReqSetConceptCard_OverWrite.Response body;
    }
  }
}
