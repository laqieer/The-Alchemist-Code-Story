// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactSlots
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Job Change(True)", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Job Change(False)", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Force Update", FlowNode.PinTypes.Input, 2)]
  public class ArtifactSlots : MonoBehaviour, IFlowInterface
  {
    private static readonly string ArtiSelectPath = "UI/ArtiSelect";
    public GenericSlot ArtifactSlot;
    public GenericSlot ArtifactSlot2;
    public GenericSlot ArtifactSlot3;
    public GameObject RootObject;
    public UnitJobDropdown JobDropDown;
    private UnitData mCurrentUnit;
    private GameObject mArtiSelector;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
        case 1:
          this.Refresh(pinID == 0);
          break;
        case 2:
          this.Refresh(true);
          this.UpdateRootObject();
          break;
      }
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactSlot, (UnityEngine.Object) null))
        this.ArtifactSlot.OnSelect = new GenericSlot.SelectEvent(this.OnClick);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactSlot2, (UnityEngine.Object) null))
        this.ArtifactSlot2.OnSelect = new GenericSlot.SelectEvent(this.OnClick);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactSlot3, (UnityEngine.Object) null))
        return;
      this.ArtifactSlot3.OnSelect = new GenericSlot.SelectEvent(this.OnClick);
    }

    private void OnDestroy() => GameUtility.DestroyGameObject(this.mArtiSelector);

    public void Refresh(bool enable)
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) ((Component) this).transform.parent).gameObject, (UnitData) null);
      if (dataOfClass == null)
      {
        this.RefreshSlots(this.ArtifactSlot, (ArtifactData[]) null, 0, 0);
        this.RefreshSlots(this.ArtifactSlot2, (ArtifactData[]) null, 1, 0);
        this.RefreshSlots(this.ArtifactSlot3, (ArtifactData[]) null, 2, 0);
      }
      else
      {
        this.mCurrentUnit = dataOfClass;
        ArtifactData[] artifactDataArray = new ArtifactData[3];
        ArtifactData[] artifactDatas;
        if (enable)
        {
          int jobIndex = dataOfClass.JobIndex;
          UnitData unitDataByUniqueId = MonoSingleton<GameManager>.GetInstanceDirect().Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
          if (unitDataByUniqueId == null)
            return;
          if (UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
            this.mCurrentUnit = UnitOverWriteUtility.Apply(unitDataByUniqueId, (eOverWritePartyType) GlobalVars.OverWritePartyType);
          artifactDatas = this.mCurrentUnit.Jobs[jobIndex].ArtifactDatas;
        }
        else
          artifactDatas = dataOfClass.CurrentJob.ArtifactDatas;
        List<ArtifactData> artifactDataList = new List<ArtifactData>((IEnumerable<ArtifactData>) artifactDatas);
        Dictionary<int, List<ArtifactData>> dictionary = new Dictionary<int, List<ArtifactData>>();
        for (int index = 0; index < artifactDataList.Count; ++index)
        {
          if (artifactDataList[index] != null)
          {
            int type = (int) artifactDataList[index].ArtifactParam.type;
            if (!dictionary.ContainsKey(type))
              dictionary[type] = new List<ArtifactData>();
            dictionary[type].Add(artifactDataList[index]);
          }
        }
        artifactDataList.Clear();
        List<int> intList = new List<int>((IEnumerable<int>) dictionary.Keys);
        intList.Sort((Comparison<int>) ((x, y) => x - y));
        for (int index = 0; index < intList.Count; ++index)
          artifactDataList.AddRange((IEnumerable<ArtifactData>) dictionary[intList[index]]);
        ArtifactData[] array = artifactDataList.ToArray();
        this.RefreshSlots(this.ArtifactSlot, array, 0, dataOfClass.CurrentJob.Rank, ArtifactSlots.IsLockedArtifactSlot(0, dataOfClass.CurrentJob, dataOfClass), enable);
        this.RefreshSlots(this.ArtifactSlot2, array, 1, dataOfClass.CurrentJob.Rank, ArtifactSlots.IsLockedArtifactSlot(1, dataOfClass.CurrentJob, dataOfClass), enable);
        this.RefreshSlots(this.ArtifactSlot3, array, 2, dataOfClass.CurrentJob.Rank, ArtifactSlots.IsLockedArtifactSlot(2, dataOfClass.CurrentJob, dataOfClass), enable);
      }
    }

    private void RefreshSlots(
      GenericSlot slot,
      ArtifactData[] list,
      int index,
      int job_rank,
      bool is_locked = true,
      bool enable = true)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) slot, (UnityEngine.Object) null))
        return;
      ArtifactData data = (ArtifactData) null;
      if (job_rank > 0 && list.Length > index)
        data = list[index];
      slot.SetLocked(is_locked);
      slot.SetSlotData<ArtifactData>(data);
      SRPG_Button componentInChildren = ((Component) slot).gameObject.GetComponentInChildren<SRPG_Button>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        return;
      bool flag = !is_locked;
      ((Selectable) componentInChildren).interactable = enable && flag;
    }

    public static bool IsLockedArtifactSlot(int slot, JobData job, UnitData unit)
    {
      if (job == null || unit == null)
        return false;
      int awakeLv = unit.AwakeLv;
      FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
      return fixParam != null && fixParam.EquipArtifactSlotUnlock != null && fixParam.EquipArtifactSlotUnlock.Length > 0 && fixParam.EquipArtifactSlotUnlock.Length >= slot && (int) fixParam.EquipArtifactSlotUnlock[slot] > awakeLv;
    }

    private void OnClick(GenericSlot slot, bool interactable)
    {
      if (!interactable)
        return;
      GameObject gameObject = AssetManager.Load<GameObject>(ArtifactSlots.ArtiSelectPath);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      this.mArtiSelector = UnityEngine.Object.Instantiate<GameObject>(gameObject);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mArtiSelector, (UnityEngine.Object) null))
        return;
      ArtifactWindow component = this.mArtiSelector.GetComponent<ArtifactWindow>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) slot, (UnityEngine.Object) null))
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) slot, (UnityEngine.Object) this.ArtifactSlot))
          component.SelectArtifactSlot = ArtifactTypes.Arms;
        else if (UnityEngine.Object.op_Equality((UnityEngine.Object) slot, (UnityEngine.Object) this.ArtifactSlot2))
          component.SelectArtifactSlot = ArtifactTypes.Armor;
        else if (UnityEngine.Object.op_Equality((UnityEngine.Object) slot, (UnityEngine.Object) this.ArtifactSlot3))
          component.SelectArtifactSlot = ArtifactTypes.Accessory;
      }
      component.OnEquip = new ArtifactWindow.EquipEvent(this.OnSelect);
      component.SetOwnerUnit(this.mCurrentUnit, this.mCurrentUnit.JobIndex);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component.ArtifactScrollList, (UnityEngine.Object) null))
      {
        if (component.ArtifactScrollList.CurrentOwner != this.mCurrentUnit)
          component.ArtifactScrollList.CurrentOwner = this.mCurrentUnit;
        ArtifactData dataOfClass = DataSource.FindDataOfClass<ArtifactData>(((Component) slot).gameObject, (ArtifactData) null);
        DataSource.Bind<ArtifactData>(((Component) component).gameObject, dataOfClass);
        long iid = 0;
        if (this.mCurrentUnit.CurrentJob.Artifacts != null)
        {
          long uniqueId = (long) (dataOfClass == null ? (OLong) 0L : dataOfClass.UniqueID);
          for (int index = 0; index < this.mCurrentUnit.CurrentJob.Artifacts.Length; ++index)
          {
            if (this.mCurrentUnit.CurrentJob.Artifacts[index] != 0L & this.mCurrentUnit.CurrentJob.Artifacts[index] == uniqueId)
            {
              iid = this.mCurrentUnit.CurrentJob.Artifacts[index];
              break;
            }
          }
        }
        ArtifactData artifactData = (ArtifactData) null;
        if (iid != 0L)
          artifactData = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(iid);
        if (artifactData != null)
          component.ArtifactScrollList.SetSelection(new ArtifactData[1]
          {
            artifactData
          }, true, true);
        else
          component.ArtifactScrollList.SetSelection(new ArtifactData[0], true, true);
        ArtifactTypes select_slot_artifact_type = dataOfClass == null ? ArtifactTypes.None : dataOfClass.ArtifactParam.type;
        List<string> stringList1 = new List<string>();
        int jobIndex = this.mCurrentUnit.JobIndex;
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.GetInstanceDirect().Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
        component.ArtifactScrollList.FiltersPriority = unitDataByUniqueId != null ? component.SetEquipArtifactFilters(unitDataByUniqueId.Jobs[jobIndex], dataOfClass, select_slot_artifact_type) : stringList1.ToArray();
        ArtifactData[] viewArtifactList = this.GetViewArtifactList();
        ArtifactList.SlotExcludeEquipType excludeEquipType = ArtifactList.SlotExcludeEquipType.Non;
        List<string> stringList2 = new List<string>();
        if (viewArtifactList != null)
        {
          for (int index = 0; index < viewArtifactList.Length; ++index)
          {
            if (component.SelectArtifactSlot != (ArtifactTypes) (index + 1) && viewArtifactList[index] != null)
            {
              if (viewArtifactList[index].ArtifactParam.type == ArtifactTypes.Arms)
                excludeEquipType = excludeEquipType != ArtifactList.SlotExcludeEquipType.Armor ? ArtifactList.SlotExcludeEquipType.Arms : ArtifactList.SlotExcludeEquipType.Mix;
              if (viewArtifactList[index].ArtifactParam.type == ArtifactTypes.Armor)
                excludeEquipType = excludeEquipType != ArtifactList.SlotExcludeEquipType.Arms ? ArtifactList.SlotExcludeEquipType.Armor : ArtifactList.SlotExcludeEquipType.Mix;
              if (viewArtifactList[index].ArtifactParam.type == ArtifactTypes.Accessory)
                stringList2.Add(viewArtifactList[index].ArtifactParam.iname);
            }
          }
        }
        component.ArtifactScrollList.ExcludeEquipType = excludeEquipType;
        component.ArtifactScrollList.ExcludeEquipTypeIname = stringList2;
      }
      else
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component.ArtifactList, (UnityEngine.Object) null))
          return;
        if (component.ArtifactList.TestOwner != this.mCurrentUnit)
          component.ArtifactList.TestOwner = this.mCurrentUnit;
        ArtifactData dataOfClass = DataSource.FindDataOfClass<ArtifactData>(((Component) slot).gameObject, (ArtifactData) null);
        long iid = 0;
        if (this.mCurrentUnit.CurrentJob.Artifacts != null)
        {
          long uniqueId = (long) (dataOfClass == null ? (OLong) 0L : dataOfClass.UniqueID);
          for (int index = 0; index < this.mCurrentUnit.CurrentJob.Artifacts.Length; ++index)
          {
            if (this.mCurrentUnit.CurrentJob.Artifacts[index] != 0L & this.mCurrentUnit.CurrentJob.Artifacts[index] == uniqueId)
            {
              iid = this.mCurrentUnit.CurrentJob.Artifacts[index];
              break;
            }
          }
        }
        ArtifactData artifactData = (ArtifactData) null;
        if (iid != 0L)
          artifactData = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(iid);
        if (artifactData != null)
          component.ArtifactList.SetSelection((object[]) new ArtifactData[1]
          {
            artifactData
          }, true, true);
        else
          component.ArtifactList.SetSelection((object[]) new ArtifactData[0], true, true);
        ArtifactTypes select_slot_artifact_type = dataOfClass == null ? ArtifactTypes.None : dataOfClass.ArtifactParam.type;
        List<string> stringList3 = new List<string>();
        int jobIndex = this.mCurrentUnit.JobIndex;
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.GetInstanceDirect().Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
        if (unitDataByUniqueId == null)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component.ArtifactScrollList, (UnityEngine.Object) null))
            component.ArtifactScrollList.FiltersPriority = stringList3.ToArray();
          else
            component.ArtifactList.FiltersPriority = stringList3.ToArray();
        }
        else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component.ArtifactScrollList, (UnityEngine.Object) null))
          component.ArtifactScrollList.FiltersPriority = component.SetEquipArtifactFilters(unitDataByUniqueId.Jobs[jobIndex], dataOfClass, select_slot_artifact_type);
        else
          component.ArtifactList.FiltersPriority = component.SetEquipArtifactFilters(unitDataByUniqueId.Jobs[jobIndex], dataOfClass, select_slot_artifact_type);
        ArtifactData[] viewArtifactList = this.GetViewArtifactList();
        ArtifactList.SlotExcludeEquipType excludeEquipType = ArtifactList.SlotExcludeEquipType.Non;
        List<string> stringList4 = new List<string>();
        if (viewArtifactList != null)
        {
          for (int index = 0; index < viewArtifactList.Length; ++index)
          {
            if (component.SelectArtifactSlot != (ArtifactTypes) (index + 1) && viewArtifactList[index] != null)
            {
              if (viewArtifactList[index].ArtifactParam.type == ArtifactTypes.Arms)
                excludeEquipType = excludeEquipType != ArtifactList.SlotExcludeEquipType.Armor ? ArtifactList.SlotExcludeEquipType.Arms : ArtifactList.SlotExcludeEquipType.Mix;
              if (viewArtifactList[index].ArtifactParam.type == ArtifactTypes.Armor)
                excludeEquipType = excludeEquipType != ArtifactList.SlotExcludeEquipType.Arms ? ArtifactList.SlotExcludeEquipType.Armor : ArtifactList.SlotExcludeEquipType.Mix;
              if (viewArtifactList[index].ArtifactParam.type == ArtifactTypes.Accessory)
                stringList4.Add(viewArtifactList[index].ArtifactParam.iname);
            }
          }
        }
        component.ArtifactList.ExcludeEquipType = excludeEquipType;
        component.ArtifactList.ExcludeEquipTypeIname = stringList4;
      }
    }

    private ArtifactData[] GetViewArtifactList()
    {
      ArtifactData[] view_artifact_datas = new List<ArtifactData>()
      {
        DataSource.FindDataOfClass<ArtifactData>(((Component) this.ArtifactSlot).gameObject, (ArtifactData) null),
        DataSource.FindDataOfClass<ArtifactData>(((Component) this.ArtifactSlot2).gameObject, (ArtifactData) null),
        DataSource.FindDataOfClass<ArtifactData>(((Component) this.ArtifactSlot3).gameObject, (ArtifactData) null)
      }.ToArray();
      List<ArtifactData> artifactDataList = new List<ArtifactData>((IEnumerable<ArtifactData>) this.mCurrentUnit.CurrentJob.ArtifactDatas);
      if (artifactDataList.Count != view_artifact_datas.Length)
        return (ArtifactData[]) null;
      for (int i = 0; i < view_artifact_datas.Length; ++i)
      {
        if (view_artifact_datas[i] != null && artifactDataList.Find((Predicate<ArtifactData>) (x => x != null && (long) x.UniqueID == (long) view_artifact_datas[i].UniqueID)) == null)
          return (ArtifactData[]) null;
      }
      return view_artifact_datas;
    }

    private void OnSelect(ArtifactData artifact, ArtifactTypes type = ArtifactTypes.None)
    {
      if (artifact != null)
      {
        PlayerData player = MonoSingleton<GameManager>.GetInstanceDirect().Player;
        UnitData unit = (UnitData) null;
        JobData job1 = (JobData) null;
        if (player.FindOwner(artifact, out unit, out job1))
        {
          for (int slot = 0; slot < job1.Artifacts.Length; ++slot)
          {
            if (job1.Artifacts[slot] == (long) artifact.UniqueID)
            {
              job1.SetEquipArtifact(slot, (ArtifactData) null);
              break;
            }
          }
          if (this.mCurrentUnit.UniqueID == unit.UniqueID)
          {
            JobData jobData = (JobData) null;
            foreach (JobData job2 in this.mCurrentUnit.Jobs)
            {
              if (job2.UniqueID == job1.UniqueID)
                jobData = job2;
            }
            if (jobData != null)
            {
              for (int slot = 0; slot < jobData.Artifacts.Length; ++slot)
              {
                if (jobData.Artifacts[slot] == (long) artifact.UniqueID)
                {
                  jobData.SetEquipArtifact(slot, (ArtifactData) null);
                  break;
                }
              }
            }
          }
        }
      }
      int index1 = JobData.GetArtifactSlotIndex(type);
      ArtifactData[] viewArtifactList = this.GetViewArtifactList();
      if (viewArtifactList == null)
        return;
      if (artifact != null && (artifact.ArtifactParam.type == ArtifactTypes.Arms || artifact.ArtifactParam.type == ArtifactTypes.Armor))
      {
        for (int index2 = 0; index2 < viewArtifactList.Length; ++index2)
        {
          if (viewArtifactList[index2] != null && viewArtifactList[index2].ArtifactParam.type == artifact.ArtifactParam.type)
            index1 = index2;
        }
      }
      viewArtifactList[index1] = artifact;
      List<ArtifactData> artifactDataList = new List<ArtifactData>();
      for (int slot = 0; slot < this.mCurrentUnit.CurrentJob.ArtifactDatas.Length; ++slot)
        this.mCurrentUnit.CurrentJob.SetEquipArtifact(slot, (ArtifactData) null);
      for (int index3 = 0; index3 < viewArtifactList.Length; ++index3)
      {
        if (viewArtifactList[index3] != null)
        {
          if (viewArtifactList[index3].ArtifactParam.type == ArtifactTypes.Accessory)
            artifactDataList.Add(viewArtifactList[index3]);
          else
            this.mCurrentUnit.CurrentJob.SetEquipArtifact(JobData.GetArtifactSlotIndex(viewArtifactList[index3].ArtifactParam.type), viewArtifactList[index3]);
        }
      }
      for (int index4 = 0; index4 < artifactDataList.Count; ++index4)
      {
        for (int slot = 0; slot < this.mCurrentUnit.CurrentJob.ArtifactDatas.Length; ++slot)
        {
          if (this.mCurrentUnit.CurrentJob.ArtifactDatas[slot] == null)
          {
            this.mCurrentUnit.CurrentJob.SetEquipArtifact(slot, artifactDataList[index4]);
            break;
          }
        }
      }
      this.mCurrentUnit.UpdateArtifact(this.mCurrentUnit.JobIndex, refreshSkillAbilityDeriveData: true);
      if (!UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
        SRPG.Network.RequestAPI((WebAPI) new ReqArtifactSet(this.mCurrentUnit.UniqueID, this.mCurrentUnit.CurrentJob.UniqueID, this.mCurrentUnit.CurrentJob.Artifacts, new SRPG.Network.ResponseCallback(this.OnArtifactSetResult)));
      else
        SRPG.Network.RequestAPI((WebAPI) new ReqArtifactSet_OverWrite(this.mCurrentUnit.UniqueID, this.mCurrentUnit.CurrentJob.UniqueID, this.mCurrentUnit.CurrentJob.Artifacts, (eOverWritePartyType) GlobalVars.OverWritePartyType, new SRPG.Network.ResponseCallback(this.OnArtifactSetResult), EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
    }

    private void OnArtifactSetResult(WWWResult www)
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
        this.OnArtifactSetResult_Simple(www);
      else
        this.OnArtifactSetResult_OverWrite(www);
    }

    private void OnArtifactSetResult_Simple(WWWResult www)
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
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Retry();
          return;
        }
        SRPG.Network.RemoveAPI();
        this.UpdateRootObject();
        this.Refresh(true);
      }
    }

    private void OnArtifactSetResult_OverWrite(WWWResult www)
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
        ArtifactSlots.MP_ArtifactSet_OverWriteResponse overWriteResponse = SerializerCompressorHelper.Decode<ArtifactSlots.MP_ArtifactSet_OverWriteResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RootObject, (UnityEngine.Object) null))
      {
        UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(this.RootObject, (UnitData) null);
        if (dataOfClass != null)
        {
          UnitData data = UnitOverWriteUtility.Apply(dataOfClass, (eOverWritePartyType) GlobalVars.OverWritePartyType);
          data.CalcStatus();
          DataSource.Bind<UnitData>(this.RootObject, data);
        }
      }
      this.UpdateRootObject();
      this.Refresh(true);
    }

    private void UpdateRootObject()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.RootObject, (UnityEngine.Object) null))
        return;
      GameParameter.UpdateAll(this.RootObject);
    }

    [MessagePackObject(true)]
    public class MP_ArtifactSet_OverWriteResponse : WebAPI.JSON_BaseResponse
    {
      public ReqArtifactSet_OverWrite.Response body;
    }
  }
}
