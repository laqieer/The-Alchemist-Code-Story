// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqConceptLeaderSkillSet
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
  [FlowNode.NodeType("ConceptCard/Req/ReqConceptLeaderSkillSet", 32741)]
  [FlowNode.Pin(0, "リーダースキルを切り替え", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "リーダースキルを切り替えた", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ReqConceptLeaderSkillSet : FlowNode_Network
  {
    private const int INPUT_CONCEPT_LEADER_SKILL_SWITCH = 0;
    private const int OUTPUT_CONCEPT_LEADER_SKILL_SWITCH = 100;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      ((Behaviour) this).enabled = true;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      PlayerData player = instance.Player;
      if (player == null)
        return;
      UnitData unit = player.GetUnitData((long) GlobalVars.SelectedLSChangeUnitUniqueID);
      if (UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
        unit = UnitOverWriteUtility.Apply(unit, (eOverWritePartyType) GlobalVars.OverWritePartyType);
      if (unit == null || unit.MainConceptCard == null || !unit.MainConceptCard.LeaderSkillIsAvailable())
        DebugUtility.LogError("ユニットIDが不正、もしくは真理念装のデータが不正です。");
      else if (!UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
        this.ExecRequest((WebAPI) new ReqSetConceptLeaderSkill(unit.UniqueID, !unit.IsEquipConceptLeaderSkill(), new SRPG.Network.ResponseCallback(this.ConceptLeaderSkillSetResponseCallback)));
      else
        this.ExecRequest((WebAPI) new ReqSetConceptLeaderSkill_OverWrite(unit.UniqueID, !unit.IsEquipConceptLeaderSkill(), (eOverWritePartyType) GlobalVars.OverWritePartyType, new SRPG.Network.ResponseCallback(this.ConceptLeaderSkillSetResponseCallback), EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
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

    private void ConceptLeaderSkillSetResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      this.OnSuccess(www);
      this.ActivateOutputLinks(100);
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
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        this.OnFailed();
      }
    }

    private void Success_OverWrite(WWWResult www)
    {
      ReqSetConceptLeaderSkill_OverWrite.Response body;
      if (EncodingTypes.IsJsonSerializeCompressSelected(!GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK : GlobalVars.SelectedSerializeCompressMethod))
      {
        WebAPI.JSON_BodyResponse<ReqSetConceptLeaderSkill_OverWrite.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqSetConceptLeaderSkill_OverWrite.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "jsonRes == null");
        body = jsonObject.body;
      }
      else
      {
        FlowNode_ReqConceptLeaderSkillSet.MP_SetConceptLeaderSkill_OverWriteResponse overWriteResponse = SerializerCompressorHelper.Decode<FlowNode_ReqConceptLeaderSkillSet.MP_SetConceptLeaderSkill_OverWriteResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
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

    [MessagePackObject(true)]
    public class MP_SetConceptLeaderSkill_OverWriteResponse : WebAPI.JSON_BaseResponse
    {
      public ReqSetConceptLeaderSkill_OverWrite.Response body;
    }
  }
}
