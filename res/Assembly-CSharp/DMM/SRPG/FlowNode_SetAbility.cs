// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetAbility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Unit/SetAbility", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_SetAbility : FlowNode_Network
  {
    private long mUnitUniqueID;
    private Queue<long> mJobs = new Queue<long>();
    private Queue<long> mAbilities = new Queue<long>();

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (SRPG.Network.Mode == SRPG.Network.EConnectMode.Online)
      {
        this.mUnitUniqueID = (long) GlobalVars.SelectedUnitUniqueID;
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mUnitUniqueID);
        for (int index1 = 0; index1 < unitDataByUniqueId.Jobs.Length; ++index1)
        {
          this.mJobs.Enqueue(unitDataByUniqueId.Jobs[index1].UniqueID);
          for (int index2 = 0; index2 < unitDataByUniqueId.Jobs[index1].AbilitySlots.Length; ++index2)
            this.mAbilities.Enqueue(unitDataByUniqueId.Jobs[index1].AbilitySlots[index2]);
        }
        this.UpdateAbilities();
        ((Behaviour) this).enabled = true;
      }
      else
        this.Success();
    }

    private void UpdateAbilities()
    {
      long iid_job = this.mJobs.Dequeue();
      long[] iid_abils = new long[5];
      for (int index = 0; index < 5; ++index)
        iid_abils[index] = this.mAbilities.Dequeue();
      if (!UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
        this.ExecRequest((WebAPI) new ReqJobAbility(iid_job, iid_abils, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      else
        this.ExecRequest((WebAPI) new ReqJobAbility_OverWrite(iid_job, iid_abils, (eOverWritePartyType) GlobalVars.OverWritePartyType, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (SRPG.Network.IsError)
      {
        switch (SRPG.Network.ErrCode)
        {
          case SRPG.Network.EErrCode.NoAbilitySetAbility:
          case SRPG.Network.EErrCode.NoJobSetAbility:
          case SRPG.Network.EErrCode.UnsetAbility:
            this.OnFailed();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else if (!UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
        this.Success_Simple(www);
      else
        this.Success_OverWrite(www);
    }

    private void Success_Simple(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      try
      {
        if (jsonObject.body == null)
          throw new InvalidJSONException();
        MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
        MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        this.OnFailed();
        return;
      }
      SRPG.Network.RemoveAPI();
      if (this.mJobs.Count < 1)
        this.Success();
      else
        this.UpdateAbilities();
    }

    private void Success_OverWrite(WWWResult www)
    {
      ReqJobAbility_OverWrite.Response body;
      if (EncodingTypes.IsJsonSerializeCompressSelected(!GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK : GlobalVars.SelectedSerializeCompressMethod))
      {
        WebAPI.JSON_BodyResponse<ReqJobAbility_OverWrite.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqJobAbility_OverWrite.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "jsonRes == null");
        body = jsonObject.body;
      }
      else
      {
        FlowNode_SetAbility.MP_JobAbilityt_OverWriteResponse overWriteResponse = SerializerCompressorHelper.Decode<FlowNode_SetAbility.MP_JobAbilityt_OverWriteResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
        DebugUtility.Assert(overWriteResponse != null, "mpRes == null");
        body = overWriteResponse.body;
      }
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(body.party_decks);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        this.OnFailed();
        return;
      }
      SRPG.Network.RemoveAPI();
      if (this.mJobs.Count < 1)
        this.Success();
      else
        this.UpdateAbilities();
    }

    [MessagePackObject(true)]
    public class MP_JobAbilityt_OverWriteResponse : WebAPI.JSON_BaseResponse
    {
      public ReqJobAbility_OverWrite.Response body;
    }
  }
}
