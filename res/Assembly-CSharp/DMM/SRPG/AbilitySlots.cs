// Decompiled with JetBrains decompiler
// Type: SRPG.AbilitySlots
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Job Change(True)", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Job Change(False)", FlowNode.PinTypes.Input, 1)]
  public class AbilitySlots : MonoBehaviour, IFlowInterface
  {
    public UnitAbilityPicker Prefab_AbilityPicker;
    public UnitAbilityList unitAbilityList;
    public GameObject abilityExplanationText;
    public GameObject refreshRoot;
    private bool button_enable;
    private UnitAbilityPicker mAbilityPicker;
    private bool mIsConnecting;

    private UnitData CurrentUnit
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.unitAbilityList, (UnityEngine.Object) null) ? this.unitAbilityList.Unit : (UnitData) null;
      }
    }

    public void Activated(int pinID)
    {
      if (pinID != 0 && pinID != 1)
        return;
      this.Refresh(pinID == 0);
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_AbilityPicker, (UnityEngine.Object) null))
      {
        this.mAbilityPicker = UnityEngine.Object.Instantiate<UnitAbilityPicker>(this.Prefab_AbilityPicker);
        this.mAbilityPicker.OnAbilitySelect = new UnitAbilityPicker.AbilityPickerEvent(this.OnSlotAbilitySelect);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.unitAbilityList, (UnityEngine.Object) null))
        this.unitAbilityList.OnSlotSelect = new UnitAbilityList.AbilitySlotEvent(this.OnAbilitySlotSelect);
      this.SetButtonEnabled();
      this.SetAbilityExplanationText();
    }

    private void OnDestroy()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mAbilityPicker, (UnityEngine.Object) null))
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mAbilityPicker).gameObject);
      this.mAbilityPicker = (UnitAbilityPicker) null;
    }

    private void OnAbilitySlotSelect(int slotIndex)
    {
      if (this.mIsConnecting || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mAbilityPicker, (UnityEngine.Object) null) || this.CurrentUnit == null)
        return;
      this.mAbilityPicker.UnitData = this.CurrentUnit;
      this.mAbilityPicker.AbilitySlot = slotIndex;
      this.mAbilityPicker.Refresh();
      ((Component) this.mAbilityPicker).GetComponent<WindowController>().Open();
    }

    private void OnSlotAbilitySelect(AbilityData ability, GameObject itemGO)
    {
      if (this.CurrentUnit == null)
        return;
      int abilitySlot = this.mAbilityPicker.AbilitySlot;
      ((Component) this.mAbilityPicker).GetComponent<WindowController>().Close();
      this.CurrentUnit.SetEquipAbility(this.CurrentUnit.JobIndex, abilitySlot, ability == null ? 0L : ability.UniqueID);
      MonoSingleton<GameManager>.Instance.Player.OnChangeAbilitySet(this.CurrentUnit.UnitID);
      this.unitAbilityList.DisplaySlots();
      this.mIsConnecting = true;
      if (!UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
        SRPG.Network.RequestAPI((WebAPI) new ReqJobAbility(this.CurrentUnit.CurrentJob.UniqueID, this.CurrentUnit.CurrentJob.AbilitySlots, new SRPG.Network.ResponseCallback(this.Res_UpdateEquippedAbility)));
      else
        SRPG.Network.RequestAPI((WebAPI) new ReqJobAbility_OverWrite(this.CurrentUnit.CurrentJob.UniqueID, this.CurrentUnit.CurrentJob.AbilitySlots, (eOverWritePartyType) GlobalVars.OverWritePartyType, new SRPG.Network.ResponseCallback(this.Res_UpdateEquippedAbility), EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
    }

    private void Res_UpdateEquippedAbility(WWWResult www)
    {
      if (SRPG.Network.IsError)
      {
        switch (SRPG.Network.ErrCode)
        {
          case SRPG.Network.EErrCode.NoAbilitySetAbility:
          case SRPG.Network.EErrCode.NoJobSetAbility:
          case SRPG.Network.EErrCode.UnsetAbility:
            FlowNode_Network.Failed();
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else if (!UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
        this.Res_UpdateEquippedAbility_Simple(www);
      else
        this.Res_UpdateEquippedAbility_OverWrite(www);
    }

    private void Res_UpdateEquippedAbility_Simple(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
      try
      {
        MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
        FlowNode_Network.Failed();
        return;
      }
      SRPG.Network.RemoveAPI();
      this.mIsConnecting = false;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.refreshRoot, (UnityEngine.Object) null))
        return;
      GameParameter.UpdateAll(this.refreshRoot);
    }

    private void Res_UpdateEquippedAbility_OverWrite(WWWResult www)
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
        AbilitySlots.MP_JobAbilityt_OverWriteResponse overWriteResponse = SerializerCompressorHelper.Decode<AbilitySlots.MP_JobAbilityt_OverWriteResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
        DebugUtility.Assert(overWriteResponse != null, "mpRes == null");
        body = overWriteResponse.body;
      }
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(body.party_decks);
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
        FlowNode_Network.Failed();
        return;
      }
      SRPG.Network.RemoveAPI();
      this.mIsConnecting = false;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.refreshRoot, (UnityEngine.Object) null))
        return;
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(this.refreshRoot, (UnitData) null);
      if (dataOfClass != null)
      {
        UnitData data = UnitOverWriteUtility.Apply(dataOfClass, (eOverWritePartyType) GlobalVars.OverWritePartyType);
        data.CalcStatus();
        DataSource.Bind<UnitData>(this.refreshRoot, data);
      }
      GameParameter.UpdateAll(this.refreshRoot);
    }

    public void Refresh(bool enable)
    {
      this.button_enable = enable;
      this.SetButtonEnabled();
      this.SetAbilityExplanationText();
    }

    public void SetButtonEnabled()
    {
      for (int index = 0; index < ((Component) this).transform.childCount; ++index)
      {
        Button componentInChildren = ((Component) ((Component) this).transform.GetChild(index)).GetComponentInChildren<Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
          ((Behaviour) componentInChildren).enabled = this.button_enable;
      }
    }

    public void SetAbilityExplanationText()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.abilityExplanationText, (UnityEngine.Object) null))
        return;
      this.abilityExplanationText.SetActive(this.button_enable);
    }

    [MessagePackObject(true)]
    public class MP_JobAbilityt_OverWriteResponse : WebAPI.JSON_BaseResponse
    {
      public ReqJobAbility_OverWrite.Response body;
    }
  }
}
