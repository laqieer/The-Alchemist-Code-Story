// Decompiled with JetBrains decompiler
// Type: SRPG.UnitAbilityPicker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "リスト更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "アビリティが選択された", FlowNode.PinTypes.Output, 100)]
  public class UnitAbilityPicker : MonoBehaviour, IFlowInterface
  {
    public UnitData UnitData;
    [NonSerialized]
    public int AbilitySlot = -1;
    [StringIsGameObjectID]
    public string InventoryWindowID;
    public UnitAbilityList ListBody;
    public Button ClearButton;
    public bool AlwaysShowClearButton;
    public GameObject NoAbilityMessage;
    public Slider YajirushiSlider;
    public UnitAbilityPicker.AbilityPickerEvent OnAbilitySelect;
    public UnitAbilityPicker.AbilityPickerEvent OnAbilityRankUp;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ClearButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ClearButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnClearSlot)));
      }
      this.Refresh();
    }

    private void OnClearSlot()
    {
      if (this.OnAbilitySelect != null)
      {
        this.OnAbilitySelect((AbilityData) null, (GameObject) null);
      }
      else
      {
        Debug.Log((object) nameof (OnClearSlot));
        UnitData unitData = UnitOverWriteUtility.Apply(this.UnitData == null ? MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID) : this.UnitData, (eOverWritePartyType) GlobalVars.OverWritePartyType);
        if (unitData == null)
          return;
        int selectedAbilitySlot = (int) GlobalVars.SelectedAbilitySlot;
        unitData.CurrentJob.SetAbilitySlot(selectedAbilitySlot, (AbilityData) null);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void _OnAbilitySelect(AbilityData ability, GameObject itemGO)
    {
      if (this.OnAbilitySelect != null)
      {
        this.OnAbilitySelect(ability, itemGO);
      }
      else
      {
        Debug.Log((object) "OnAbilitySelect");
        int slot = this.AbilitySlot < 0 ? (int) GlobalVars.SelectedAbilitySlot : this.AbilitySlot;
        UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null);
        if (dataOfClass != null && ability != null)
          dataOfClass.CurrentJob.SetAbilitySlot(slot, ability);
        UnitData unitData = UnitOverWriteUtility.Apply(this.UnitData == null ? MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID) : this.UnitData, (eOverWritePartyType) GlobalVars.OverWritePartyType);
        if (unitData == null || ability == null)
          return;
        unitData.CurrentJob.SetAbilitySlot(slot, ability);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void _OnAbilityRankUp(AbilityData ability, GameObject itemGO)
    {
      if (this.OnAbilityRankUp == null)
        return;
      this.OnAbilityRankUp(ability, itemGO);
    }

    public void Refresh()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListBody, (UnityEngine.Object) null))
        return;
      UnitData unitData = UnitOverWriteUtility.Apply(this.UnitData == null ? MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID) : this.UnitData, (eOverWritePartyType) GlobalVars.OverWritePartyType);
      if (unitData == null)
        return;
      this.ListBody.Unit = unitData;
      this.ListBody.OnAbilitySelect = new UnitAbilityList.AbilityEvent(this._OnAbilitySelect);
      this.ListBody.OnAbilityRankUp = new UnitAbilityList.AbilityEvent(this._OnAbilityRankUp);
      int index = this.AbilitySlot < 0 ? (int) GlobalVars.SelectedAbilitySlot : this.AbilitySlot;
      EAbilitySlot slotType = JobData.ABILITY_SLOT_TYPES[index];
      this.ListBody.ShowFixedAbilities = index == 0;
      this.ListBody.DisplaySlotType(slotType, true, true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ClearButton, (UnityEngine.Object) null))
      {
        if (!this.AlwaysShowClearButton)
          ((Component) this.ClearButton).gameObject.SetActive(unitData.CurrentJob.AbilitySlots[index] != 0L);
        else
          ((Component) this.ClearButton).gameObject.SetActive(true);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NoAbilityMessage, (UnityEngine.Object) null))
        this.NoAbilityMessage.SetActive(this.ListBody.IsEmpty);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.YajirushiSlider, (UnityEngine.Object) null))
        return;
      this.YajirushiSlider.value = (float) index / 5f * this.YajirushiSlider.maxValue + this.YajirushiSlider.minValue;
    }

    public delegate void AbilityPickerEvent(AbilityData ability, GameObject itemGO);
  }
}
