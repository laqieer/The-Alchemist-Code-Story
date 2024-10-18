// Decompiled with JetBrains decompiler
// Type: SRPG.UnitJobDropdown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Apply Job", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Job Change(TmpUnit)", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Job Change&Close", FlowNode.PinTypes.Output, 11)]
  public class UnitJobDropdown : Pulldown, IFlowInterface
  {
    public static UnitJobDropdown.JobChangeEvent OnJobChange = (UnitJobDropdown.JobChangeEvent) (unitUniqueID => { });
    public RawImage JobIcon;
    public RawImage ItemJobIcon;
    public bool RefreshOnStart = true;
    public GameObject GameParamterRoot;
    public UnitJobDropdown.ParentObjectEvent UpdateValue;
    private bool mRequestSent;
    private UnitData mTargetUnit;
    private string mOriginalJobID;
    private bool mIsCloseWhenJobChangeRequest;

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      if (this.mTargetUnit != null && this.mTargetUnit.CurrentJob.JobID != this.mOriginalJobID)
        this.RequestJobChange(false);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }

    protected override void Start()
    {
      base.Start();
      UnitJobDropdown unitJobDropdown = this;
      // ISSUE: method pointer
      unitJobDropdown.OnSelectionChange = (UnityAction<int>) System.Delegate.Combine((System.Delegate) unitJobDropdown.OnSelectionChange, (System.Delegate) new UnityAction<int>((object) this, __methodptr(JobChanged)));
      if (!this.RefreshOnStart)
        return;
      this.Refresh();
    }

    private void RequestJobChange(bool immediate, bool is_close_when_jobchange_request = true)
    {
      if (this.mRequestSent)
        return;
      this.mIsCloseWhenJobChangeRequest = is_close_when_jobchange_request;
      PlayerPartyTypes dataOfClass = DataSource.FindDataOfClass<PlayerPartyTypes>(((Component) this).gameObject, PlayerPartyTypes.Max);
      this.mRequestSent = true;
      if ((this.mTargetUnit.TempFlags & UnitData.TemporaryFlags.TemporaryUnitData) != (UnitData.TemporaryFlags) 0)
        MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mTargetUnit.UniqueID).SetJobFor(dataOfClass, this.mTargetUnit.CurrentJob);
      UnitJobDropdown.OnJobChange(this.mTargetUnit.UniqueID);
      if (UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
      {
        ReqUnitJob_OverWrite api = new ReqUnitJob_OverWrite(this.mTargetUnit.UniqueID, this.mTargetUnit.CurrentJob.UniqueID, (eOverWritePartyType) GlobalVars.OverWritePartyType, new SRPG.Network.ResponseCallback(this.JobChangeResult), EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK);
        if (immediate)
          SRPG.Network.RequestAPIImmediate((WebAPI) api, true);
        else
          SRPG.Network.RequestAPI((WebAPI) api);
      }
      else if ((this.mTargetUnit.TempFlags & UnitData.TemporaryFlags.TemporaryUnitData) == (UnitData.TemporaryFlags) 0)
      {
        ReqUnitJob api = new ReqUnitJob(this.mTargetUnit.UniqueID, this.mTargetUnit.CurrentJob.UniqueID, new SRPG.Network.ResponseCallback(this.JobChangeResult));
        if (immediate)
          SRPG.Network.RequestAPIImmediate((WebAPI) api, true);
        else
          SRPG.Network.RequestAPI((WebAPI) api);
      }
      else
      {
        ReqUnitJob api = new ReqUnitJob(this.mTargetUnit.UniqueID, this.mTargetUnit.CurrentJob.UniqueID, PartyData.GetStringFromPartyType(dataOfClass), new SRPG.Network.ResponseCallback(this.JobChangeResult));
        if (immediate)
          SRPG.Network.RequestAPIImmediate((WebAPI) api, true);
        else
          SRPG.Network.RequestAPI((WebAPI) api);
      }
    }

    private void JobChangeResult(WWWResult www)
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
      try
      {
        MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
        MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
        MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
        SRPG.Network.RemoveAPI();
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        FlowNode_Network.Failed();
        return;
      }
      this.mRequestSent = false;
      this.PostJobChange(this.mIsCloseWhenJobChangeRequest);
    }

    private void Success_OverWrite(WWWResult www)
    {
      ReqUnitJob_OverWrite.Response body;
      if (EncodingTypes.IsJsonSerializeCompressSelected(!GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK : GlobalVars.SelectedSerializeCompressMethod))
      {
        WebAPI.JSON_BodyResponse<ReqUnitJob_OverWrite.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqUnitJob_OverWrite.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "jsonRes == null");
        body = jsonObject.body;
      }
      else
      {
        UnitJobDropdown.MP_UnitJob_OverWriteResponse overWriteResponse = SerializerCompressorHelper.Decode<UnitJobDropdown.MP_UnitJob_OverWriteResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
        DebugUtility.Assert(overWriteResponse != null, "mpRes == null");
        body = overWriteResponse.body;
      }
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(body.party_decks);
        SRPG.Network.RemoveAPI();
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        FlowNode_Network.Failed();
        return;
      }
      this.mRequestSent = false;
      this.PostJobChange(this.mIsCloseWhenJobChangeRequest);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GameParamterRoot, (UnityEngine.Object) null))
        return;
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(this.GameParamterRoot, (UnitData) null);
      if (dataOfClass != null)
      {
        UnitData data = UnitOverWriteUtility.Apply(dataOfClass, (eOverWritePartyType) GlobalVars.OverWritePartyType);
        data.CalcStatus();
        DataSource.Bind<UnitData>(this.GameParamterRoot, data);
      }
      GameParameter.UpdateAll(this.GameParamterRoot);
    }

    private void PostJobChange(bool is_close)
    {
      this.mRequestSent = false;
      if (this.mTargetUnit != null)
      {
        MonoSingleton<GameManager>.Instance.Player.OnJobChange(this.mTargetUnit.UnitID);
        this.mOriginalJobID = this.mTargetUnit.CurrentJob.JobID;
        if (this.UpdateValue != null)
          this.UpdateValue();
        if (DataSource.FindDataOfClass<PlayerPartyTypes>(((Component) this).gameObject, PlayerPartyTypes.Max) == PlayerPartyTypes.MultiTower)
        {
          int lastSelectionIndex = 0;
          List<PartyEditData> teams = PartyUtility.LoadTeamPresets(PartyWindow2.EditPartyTypes.MultiTower, out lastSelectionIndex);
          if (teams != null && lastSelectionIndex >= 0)
          {
            for (int index1 = 0; index1 < teams.Count; ++index1)
            {
              if (teams[index1] != null)
              {
                for (int index2 = 0; index2 < teams[index1].Units.Length; ++index2)
                {
                  if (teams[index1].Units[index2] != null && teams[index1].Units[index2].UnitParam.iname == this.mTargetUnit.UnitParam.iname)
                  {
                    teams[index1].Units[index2] = this.mTargetUnit;
                    break;
                  }
                }
              }
            }
            PartyUtility.SaveTeamPresets(PartyWindow2.EditPartyTypes.MultiTower, lastSelectionIndex, teams);
            GlobalEvent.Invoke("SELECT_PARTY_END", (object) null);
          }
        }
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, !is_close ? 10 : 11);
    }

    private void OnApplicationPause(bool pausing)
    {
      if (!pausing || this.mTargetUnit == null || !(this.mTargetUnit.CurrentJob.JobID != this.mOriginalJobID))
        return;
      this.RequestJobChange(true);
    }

    private void OnApplicationFocus(bool focus)
    {
      if (focus)
        return;
      this.OnApplicationPause(true);
    }

    private void JobChanged(int value)
    {
      if (this.mTargetUnit == null || value == this.mTargetUnit.JobIndex)
        return;
      this.mTargetUnit.SetJobIndex(value);
      if (UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
      {
        this.RequestJobChange(false, false);
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GameParamterRoot, (UnityEngine.Object) null))
          GameParameter.UpdateAll(this.GameParamterRoot);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      }
    }

    public void Refresh()
    {
      this.ClearItems();
      if (this.mTargetUnit == null)
        this.mTargetUnit = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null);
      if (this.mTargetUnit == null)
        return;
      this.mOriginalJobID = this.mTargetUnit.CurrentJob.JobID;
      for (int index = 0; index < this.mTargetUnit.Jobs.Length; ++index)
      {
        if (this.mTargetUnit.Jobs[index].IsActivated)
        {
          UnitJobDropdown.JobPulldownItem jobPulldownItem = this.AddItem(this.mTargetUnit.Jobs[index].Name, index) as UnitJobDropdown.JobPulldownItem;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) jobPulldownItem.JobIcon, (UnityEngine.Object) null))
          {
            string name = AssetPath.JobIconSmall(this.mTargetUnit.Jobs[index].Param);
            if (!string.IsNullOrEmpty(name))
              jobPulldownItem.JobIcon.texture = (Texture) AssetManager.Load<Texture2D>(name);
          }
          if (index == this.mTargetUnit.JobIndex)
            this.Selection = this.ItemCount - 1;
        }
      }
    }

    protected override PulldownItem SetupPulldownItem(GameObject itemObject)
    {
      UnitJobDropdown.JobPulldownItem jobPulldownItem = itemObject.AddComponent<UnitJobDropdown.JobPulldownItem>();
      jobPulldownItem.JobIcon = this.ItemJobIcon;
      return (PulldownItem) jobPulldownItem;
    }

    protected override void UpdateSelection()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.JobIcon, (UnityEngine.Object) null))
        return;
      UnitJobDropdown.JobPulldownItem itemAt = this.GetItemAt(this.Selection) as UnitJobDropdown.JobPulldownItem;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) itemAt, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) itemAt.JobIcon, (UnityEngine.Object) null))
        return;
      this.JobIcon.texture = itemAt.JobIcon.texture;
    }

    public delegate void JobChangeEvent(long unitUniqueID);

    public class JobPulldownItem : PulldownItem
    {
      public string JobID;
      public RawImage JobIcon;
    }

    public delegate void ParentObjectEvent();

    [MessagePackObject(true)]
    public class MP_UnitJob_OverWriteResponse : WebAPI.JSON_BaseResponse
    {
      public ReqUnitJob_OverWrite.Response body;
    }
  }
}
