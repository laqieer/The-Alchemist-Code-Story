// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqArtifactsSet
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
  [FlowNode.NodeType("System/ReqArtifact/ReqArtifactsSet", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "装備武具に変更発生", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_ReqArtifactsSet : FlowNode_Network
  {
    private const int PIN_INPUT_REQUEST = 0;
    private const int PIN_OUTPUT_SUCCESS = 1;
    private const int PIN_OUTPUT_ARTIFACT_DIFF = 2;
    private UnitData unit;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      this.unit = component.list.GetObject<UnitData>(ArtifactSVB_Key.UNIT);
      int index1 = component.list.GetObject<int>(ArtifactSVB_Key.JOB_INDEX);
      List<ArtifactData> artifactDataList = component.list.GetObject<List<ArtifactData>>(ArtifactSVB_Key.ARTIFACTS);
      if (this.unit == null || artifactDataList == null)
        return;
      List<long> artifacts_iid = new List<long>();
      for (int index2 = 0; index2 < artifactDataList.Count; ++index2)
      {
        long uniqueId = (long) (artifactDataList[index2] == null ? (OLong) 0L : artifactDataList[index2].UniqueID);
        artifacts_iid.Add(uniqueId);
      }
      JobData job = this.unit.Jobs[index1];
      if (SRPG.Network.Mode == SRPG.Network.EConnectMode.Online)
      {
        if (this.unit.UniqueID < 1L || job.UniqueID < 1L || artifacts_iid == null || artifacts_iid.Count <= 0)
        {
          ((Behaviour) this).enabled = false;
          this.Success();
        }
        else if (!this.IsDiffEquipArtifacts(job.UniqueID, artifacts_iid))
        {
          ((Behaviour) this).enabled = false;
          this.Success(false);
        }
        else
        {
          ((Behaviour) this).enabled = true;
          if (!UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
            this.ExecRequest((WebAPI) new ReqArtifactSet(this.unit.UniqueID, job.UniqueID, artifacts_iid.ToArray(), new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          else
            this.ExecRequest((WebAPI) new ReqArtifactSet_OverWrite(this.unit.UniqueID, job.UniqueID, artifacts_iid.ToArray(), (eOverWritePartyType) GlobalVars.OverWritePartyType, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
        }
      }
      else
        this.Success();
    }

    private void Success(bool is_need_refresh_event = true)
    {
      ((Behaviour) this).enabled = false;
      if (is_need_refresh_event)
        this.ActivateOutputLinks(2);
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (SRPG.Network.IsError)
      {
        switch (SRPG.Network.ErrCode)
        {
          case SRPG.Network.EErrCode.NoJobSetJob:
          case SRPG.Network.EErrCode.CantSelectJob:
          case SRPG.Network.EErrCode.NoUnitSetJob:
            FlowNode_Network.Failed();
            break;
          default:
            FlowNode_Network.Retry();
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
      if (jsonObject.body == null)
      {
        FlowNode_Network.Retry();
      }
      else
      {
        try
        {
          MonoSingleton<GameManager>.GetInstanceDirect().Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.GetInstanceDirect().Deserialize(jsonObject.body.units);
          if (jsonObject.body.artifacts != null)
            MonoSingleton<GameManager>.GetInstanceDirect().Deserialize(jsonObject.body.artifacts, true);
          MonoSingleton<GameManager>.GetInstanceDirect().Player.UpdateArtifactOwner();
          UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.unit.UniqueID);
          if (unitDataByUniqueId != null)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) ArtifactWindow.Instance, (UnityEngine.Object) null))
            {
              if (ArtifactWindow.Instance.OwnerUnitData != null)
              {
                int jobIndex = ArtifactWindow.Instance.OwnerUnitData.JobIndex;
                ArtifactWindow.Instance.OwnerUnitData.Setup(unitDataByUniqueId);
                ArtifactWindow.Instance.OwnerUnitData.SetJobIndex(jobIndex);
              }
            }
          }
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Retry();
          return;
        }
        SRPG.Network.RemoveAPI();
        this.Success();
      }
    }

    private void Success_OverWrite(WWWResult www)
    {
      ReqArtifactSet_OverWrite.Response body;
      if (EncodingTypes.IsJsonSerializeCompressSelected(!GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK : GlobalVars.SelectedSerializeCompressMethod))
      {
        WebAPI.JSON_BodyResponse<ReqArtifactSet_OverWrite.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqArtifactSet_OverWrite.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "jsonRes == null");
        body = jsonObject.body;
      }
      else
      {
        FlowNode_ReqArtifactsSet.MP_ArtifactSet_OverWriteResponse overWriteResponse = SerializerCompressorHelper.Decode<FlowNode_ReqArtifactsSet.MP_ArtifactSet_OverWriteResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
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
        FlowNode_Network.Failed();
        return;
      }
      SRPG.Network.RemoveAPI();
      this.Success();
    }

    private bool IsDiffEquipArtifacts(long job_iid, List<long> artifacts_iid)
    {
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.unit.UniqueID);
      if (unitDataByUniqueId != null)
      {
        JobData jobData = Array.Find<JobData>(UnitOverWriteUtility.Apply(unitDataByUniqueId, (eOverWritePartyType) GlobalVars.OverWritePartyType).Jobs, (Predicate<JobData>) (job => job.UniqueID == job_iid));
        if (jobData != null)
        {
          for (int index = 0; index < jobData.Artifacts.Length; ++index)
          {
            if (artifacts_iid.Count > index && artifacts_iid[index] != jobData.Artifacts[index])
              return true;
          }
        }
      }
      return false;
    }

    [MessagePackObject(true)]
    public class MP_ArtifactSet_OverWriteResponse : WebAPI.JSON_BaseResponse
    {
      public ReqArtifactSet_OverWrite.Response body;
    }
  }
}
