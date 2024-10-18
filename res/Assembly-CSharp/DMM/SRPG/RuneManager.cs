// Decompiled with JetBrains decompiler
// Type: SRPG.RuneManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RuneManager : MonoBehaviour
  {
    [SerializeField]
    private RuneInventory mRuneInventory;
    [SerializeField]
    private RuneSlotSelector mRuneSlotSelector;
    [SerializeField]
    private RuneDrawEbaleSetEff mRuneDrawEbaleSetEff;
    [SerializeField]
    private bool mIsReplaseRune;
    public const string PREFAB_PATH_RUNE_SELECTABLE_LIST_WINDOW = "UI/Rune/RuneSelectableListWindow";
    public const string PREFAB_PATH_RUNE_EQUIP_WINDOW = "UI/Rune/RuneEquipWindow";
    public const string PREFAB_PATH_RUNE_ENHANCE_WINDOW = "UI/Rune/RuneEnhanceWindow";
    public const string PREFAB_PATH_RUNE_ENHANCE_EFFECT = "UI/Rune/RuneEnhanceEffect";
    public const string PREFAB_PATH_RUNE_EVO_WINDOW = "UI/Rune/RuneEvoWindow";
    public const string PREFAB_PATH_RUNE_DISASSEMBLY_WINDOW = "UI/Rune/RuneDisassemblyWindow";
    public const string PREFAB_PATH_RUNE_DISASSEMBLY_EFF_WINDOW = "UI/Rune/RuneDisassemblyEffect";
    public const string PREFAB_PATH_RUNE_RESET_STATUS_WINDOW = "UI/Rune/RuneResetStatusWindow";
    public const string PREFAB_PATH_RUNE_EVO_EFFECT = "UI/Rune/RuneEvoEffect";
    public const string PREFAB_PATH_RUNE_PARAM_ENH_EFFECT = "UI/Rune/RuneEnhanceStatusEvoEffect";
    private UnitData mCurrentUnit;
    private List<BindRuneData> mPlayerRuneList = new List<BindRuneData>();
    private RuneSelectableListWindow mRuneSelectableListWindow;
    private BindRuneData[] mEquipSelectedRune = new BindRuneData[2];
    private RuneEquipWindow[] mRuneEquipWindow = new RuneEquipWindow[2];
    private RuneEnhanceWindow mRuneEnhanceWindow;
    private RuneEnhanceEffectWindow mRuneEnhanceEffectWindow;
    private RuneEvoWindow mRuneEvoWindow;
    private RuneEvoEffectWindow mRuneEvoEffectWindow;
    private RuneDisassemblyWindow mRuneDisassemblyWindow;
    private RuneDisassemblyEffectWindow mRuneDisassemblyEffectWindow;
    private RuneResetStatusWindow mRuneResetStatusWindow;
    private RuneParamEnhEvoEffectWindow mRuneParamEnhEvoEffectWindow;
    private RuneManager.Mode mMode;
    private RuneEnhanceSettings mEnhanceSettings = new RuneEnhanceSettings();
    private static RuneManager mInstance;

    public RuneSelectableListWindow RuneSelectableListWindow => this.mRuneSelectableListWindow;

    public RuneEnhanceSettings EnhanceSettings => this.mEnhanceSettings;

    public static RuneManager Instance => RuneManager.mInstance;

    private void Awake()
    {
      RuneManager.mInstance = this;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneInventory))
        DebugUtility.LogError("mRuneInventory が設定されていません");
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneSlotSelector))
        return;
      DebugUtility.LogError("mRuneSlotSelector が設定されていません");
    }

    private void OnDestroy()
    {
      this.DestroyRuneEquipWindow();
      FlowNode_ReqRuneEquip.Clear();
      FlowNode_ReqRuneEvo.Clear();
      FlowNode_ReqRuneDisassembly.Clear();
      FlowNode_ReqRuneEnhance.Clear();
      FlowNode_ReqRuneResetParamBase.Clear();
      FlowNode_ReqRuneResetStatusEvo.Clear();
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneEnhanceEffectWindow))
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mRuneEnhanceEffectWindow).gameObject);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneEvoEffectWindow))
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mRuneEvoEffectWindow).gameObject);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneParamEnhEvoEffectWindow))
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mRuneParamEnhEvoEffectWindow).gameObject);
      RuneManager.mInstance = (RuneManager) null;
    }

    public BindRuneData FindRune(RuneData rune)
    {
      return rune == null ? (BindRuneData) null : this.mPlayerRuneList.Find((Predicate<BindRuneData>) (p => p.iid == (long) rune.UniqueID));
    }

    public void SetDoEquipment(BindRuneData rune_data)
    {
      FlowNode_ReqRuneEquip.SetEquip(rune_data, this.mPlayerRuneList, this.mCurrentUnit);
    }

    public void SetDoUnEquipment(BindRuneData rune_data)
    {
      FlowNode_ReqRuneEquip.SetUnequip(rune_data, this.mPlayerRuneList, this.mCurrentUnit);
    }

    public void SetDoUnEquipmentAll()
    {
      FlowNode_ReqRuneEquip.SetUnequipAll(this.mPlayerRuneList, this.mCurrentUnit);
    }

    public void RefreshRuneEquipFinished()
    {
      this.CloseOnlyRuneEquipWindow(RuneManager.RuneEquip.Equip);
      this.CloseOnlyRuneEquipWindow(RuneManager.RuneEquip.UnEquip);
      this.RefreshUnit();
      this.mRuneSlotSelector.Refresh();
      this.mRuneSelectableListWindow.Refresh(true);
      FlowNode_ReqRuneEquip.Clear();
    }

    public void RefreshSelectableList()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRuneSelectableListWindow, (UnityEngine.Object) null))
        return;
      this.SetupBindRuneList();
      this.mRuneSelectableListWindow.Initialize(this, this.mPlayerRuneList, _is_use_equip_button: true);
    }

    private bool IsRuneOwnerIsCurrentUnit(BindRuneData runeData)
    {
      if (runeData == null || this.mCurrentUnit == null)
        return false;
      UnitData owner = runeData.GetOwner();
      return owner != null && owner.UniqueID == this.mCurrentUnit.UniqueID;
    }

    public void RefreshRuneEnhanceFinished()
    {
      this.OpenRuneEnhanceResultWindow(FlowNode_ReqRuneEnhance.GetBackupRune(), FlowNode_ReqRuneEnhance.GetTargetRune(), FlowNode_ReqRuneEnhance.GetBackupGauge());
      if (this.IsRuneOwnerIsCurrentUnit(FlowNode_ReqRuneEnhance.GetTargetRune()))
      {
        this.RefreshUnit();
        this.StartCoroutine(UnitEnhanceV3.Instance.PlayRuneEquipmentEffect(false));
      }
      else
        this.mRuneSlotSelector.Refresh();
      this.mRuneSelectableListWindow.SelectedSlot();
      FlowNode_ReqRuneEnhance.Clear();
    }

    public void RefreshRuneEvoFinished(BindRuneData before_rune, BindRuneData after_rune)
    {
      this.OpenRuneEvoResultWindow(before_rune, after_rune);
      if (this.IsRuneOwnerIsCurrentUnit(after_rune))
      {
        this.RefreshUnit();
        this.StartCoroutine(UnitEnhanceV3.Instance.PlayRuneEquipmentEffect(false));
      }
      else
        this.mRuneSlotSelector.Refresh();
      this.mRuneSelectableListWindow.SelectedSlot();
      FlowNode_ReqRuneEvo.Clear();
    }

    public void RefreshRuneDisassemblyFinished(ReqRuneDisassembly.Response res)
    {
      this.OpenRuneDisassemblyResultWindow();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRuneDisassemblyEffectWindow, (UnityEngine.Object) null))
        this.mRuneDisassemblyEffectWindow.Setup(this, res);
      List<long> longList = new List<long>();
      foreach (BindRuneData targetRune in FlowNode_ReqRuneDisassembly.GetTargetRunes())
        longList.Add(targetRune.iid);
      MonoSingleton<GameManager>.Instance.Player.RemoveRunes(longList.ToArray());
      MonoSingleton<GameManager>.Instance.Player.SetRuneStorageUsedNum(res.rune_storage_used);
      this.SetupBindRuneList();
      FlowNode_ReqRuneDisassembly.Clear();
    }

    public void RefreshRuneParamEnhFinished()
    {
      this.OpenRuneParamEnhEvoEffectWindow(FlowNode_ReqRuneParamEnhEvo.BackupRune, FlowNode_ReqRuneParamEnhEvo.TargetRune);
      if (this.IsRuneOwnerIsCurrentUnit(FlowNode_ReqRuneParamEnhEvo.TargetRune))
      {
        this.RefreshUnit();
        this.StartCoroutine(UnitEnhanceV3.Instance.PlayRuneEquipmentEffect(false));
      }
      else
        this.mRuneSlotSelector.Refresh();
      this.mRuneSelectableListWindow.SelectedSlot();
      FlowNode_ReqRuneParamEnhEvo.Clear();
      this.CloseRuneResetStatusWindow();
    }

    public void Initialize(UnitData unit)
    {
      this.mCurrentUnit = unit;
      this.mMode = RuneManager.Mode.Equip;
      this.mEnhanceSettings.Reset(true);
      this.SetupBindRuneList();
      this.mRuneSlotSelector.Initialize(this, unit);
      this.mRuneDrawEbaleSetEff.SetData(unit);
      this.OpenRuneSelectableListWindow(new RuneSelectableListWindow.OnSelectedEvent(this.SelectedRuneAsEquip));
    }

    public void SetupBindRuneList()
    {
      this.mPlayerRuneList.Clear();
      foreach (KeyValuePair<long, RuneData> rune in MonoSingleton<GameManager>.Instance.Player.Runes)
        this.mPlayerRuneList.Add(new BindRuneData((long) rune.Value.UniqueID)
        {
          is_owner_disable = true
        });
    }

    public void RefreshUnit()
    {
      this.mCurrentUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
      this.mRuneSlotSelector.SetUnit(this.mCurrentUnit);
      this.mRuneSlotSelector.Refresh();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRuneDrawEbaleSetEff, (UnityEngine.Object) null))
        return;
      this.mRuneDrawEbaleSetEff.SetData(this.mCurrentUnit);
    }

    public void SelectedRuneSlot(RuneSlotIndex slot_index)
    {
      BindRuneData rune = this.FindRune(this.mCurrentUnit.EquipRune((int) (byte) slot_index));
      this.CloseRuneEquipWindow();
      if (rune != null)
        this.OpenRuneUnEquipWindow(rune);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneSelectableListWindow))
        this.mRuneSelectableListWindow.SelectedSlot(slot_index);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneSlotSelector))
        return;
      this.mRuneSlotSelector.SelectedSlot(slot_index);
    }

    public void SelectedFilterSlot(RuneSlotIndex slot_index)
    {
      if (this.mMode == RuneManager.Mode.Disassembly || !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneSlotSelector))
        return;
      this.mRuneSlotSelector.SelectedSlot(slot_index);
    }

    public void OpenDisassembly()
    {
      this.mMode = RuneManager.Mode.Disassembly;
      this.CloseRuneEquipWindow();
      this.CloseRuneUnEquipWindow();
      this.CloseRuneSelectableListWindow();
      this.OpenRuneDisassemblyWindow();
    }

    public void CloseDisassembly()
    {
      this.mMode = RuneManager.Mode.Equip;
      this.CloseRuneDisassemblyWindow();
      this.OpenRuneSelectableListWindow(new RuneSelectableListWindow.OnSelectedEvent(this.SelectedRuneAsEquip));
    }

    public void SelectedRuneAsEquip(BindRuneData rune)
    {
      if (rune == null)
        return;
      this.mMode = RuneManager.Mode.Equip;
      this.OpenRuneEquipWindow(rune);
      for (int index = 0; index < this.mPlayerRuneList.Count; ++index)
        this.mPlayerRuneList[index].is_selected = false;
      rune.is_selected = true;
    }

    public void SelectedEnhance(BindRuneData rune_data)
    {
      this.mMode = RuneManager.Mode.Enhance;
      this.OpenOnlyRuneEnhanceWindow(rune_data);
    }

    public void SelectedEvolution(BindRuneData rune_data)
    {
      this.mMode = RuneManager.Mode.Evolution;
      this.OpenOnlyRuneEvoWindow(rune_data);
    }

    public void SelectedResetStatus(
      BindRuneData rune_data,
      bool _is_enhance_evo = false,
      int _evo_slot_num = 0)
    {
      this.mMode = RuneManager.Mode.ResetStatus;
      this.OpenRuneResetStatusWindow(rune_data, _is_enhance_evo, _evo_slot_num);
    }

    public void CloseRuneWindow()
    {
      this.CloseRuneEquipWindow();
      this.CloseRuneUnEquipWindow();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRuneSelectableListWindow, (UnityEngine.Object) null))
        this.mRuneSelectableListWindow.SelectedSlot((RuneSlotIndex) byte.MaxValue);
      this.SelectedFilterSlot((RuneSlotIndex) byte.MaxValue);
    }

    public void RefreshEquipWindow()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRuneEquipWindow[1], (UnityEngine.Object) null))
        this.mRuneEquipWindow[1].Refresh();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRuneEquipWindow[0], (UnityEngine.Object) null))
        return;
      this.mRuneEquipWindow[0].Refresh();
    }

    public static T CreatePopupWindow<T>(string path)
    {
      GameObject gameObject1 = AssetManager.Load<GameObject>(path);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        return default (T);
      GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
      gameObject1.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
      gameObject1.transform.localScale = new Vector3(1f, 1f, 1f);
      return gameObject2.GetComponent<T>();
    }

    private void OpenRuneEquipWindow(BindRuneData selected_rune)
    {
      if (selected_rune == null)
        return;
      UnitData owner = selected_rune.GetOwner();
      if (this.mIsReplaseRune && owner != null && owner.UnitID == this.mCurrentUnit.UnitID)
      {
        this.OpenRuneUnEquipWindow(selected_rune);
        if (this.mEquipSelectedRune[1] == null || (int) (byte) this.mEquipSelectedRune[1].RuneParam.slot_index == (int) (byte) selected_rune.RuneParam.slot_index)
          return;
        this.CloseRuneEquipWindow();
      }
      else
      {
        this.mEquipSelectedRune[1] = selected_rune;
        RuneData rune1 = this.mCurrentUnit.EquipRune((int) (byte) selected_rune.RuneParam.slot_index);
        if (rune1 != null)
        {
          BindRuneData rune2 = this.FindRune(rune1);
          if (rune2 != null)
            this.OpenRuneUnEquipWindow(rune2);
        }
        else
          this.CloseRuneUnEquipWindow();
        if (rune1 != null && selected_rune.iid == (long) rune1.UniqueID)
          return;
        this.OpenOnlyRuneEquipWindow(selected_rune, RuneManager.RuneEquip.Equip);
      }
    }

    private void CloseRuneEquipWindow()
    {
      this.mEquipSelectedRune[1] = (BindRuneData) null;
      this.CloseOnlyRuneEquipWindow(RuneManager.RuneEquip.Equip);
    }

    private void OpenRuneUnEquipWindow(BindRuneData selected_rune)
    {
      this.mEquipSelectedRune[0] = selected_rune;
      this.OpenOnlyRuneEquipWindow(selected_rune, RuneManager.RuneEquip.UnEquip);
    }

    private void CloseRuneUnEquipWindow()
    {
      this.mEquipSelectedRune[0] = (BindRuneData) null;
      this.CloseOnlyRuneEquipWindow(RuneManager.RuneEquip.UnEquip);
    }

    private void OpenOnlyRuneEquipWindow(BindRuneData selected_rune, RuneManager.RuneEquip equip)
    {
      int index = (int) equip;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneEquipWindow[index], (UnityEngine.Object) null))
      {
        this.mRuneEquipWindow[index] = RuneManager.CreatePopupWindow<RuneEquipWindow>("UI/Rune/RuneEquipWindow");
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneEquipWindow[index], (UnityEngine.Object) null))
          return;
        this.mRuneInventory.SetParentPopupObject(((Component) this.mRuneEquipWindow[index]).gameObject, equip != RuneManager.RuneEquip.Equip ? RuneInventory.WindowParent.UnEquipPopup : RuneInventory.WindowParent.EquipPopup);
      }
      this.mRuneEquipWindow[index].Setup(this, this.mCurrentUnit, selected_rune, equip);
    }

    private void CloseOnlyRuneEquipWindow(RuneManager.RuneEquip equip)
    {
      int index = (int) equip;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneEquipWindow[index], (UnityEngine.Object) null))
        return;
      this.mRuneEquipWindow[index].CloseSelf();
      this.mRuneEquipWindow[index] = (RuneEquipWindow) null;
    }

    private void DestroyRuneEquipWindow()
    {
      for (int index = 0; index < this.mRuneEquipWindow.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRuneEquipWindow[index], (UnityEngine.Object) null))
        {
          UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mRuneEquipWindow[index]).gameObject);
          this.mRuneEquipWindow[index] = (RuneEquipWindow) null;
        }
      }
    }

    private void OpenOnlyRuneEnhanceWindow(BindRuneData selected_rune)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneEnhanceWindow, (UnityEngine.Object) null))
      {
        this.mRuneEnhanceWindow = RuneManager.CreatePopupWindow<RuneEnhanceWindow>("UI/Rune/RuneEnhanceWindow");
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneEnhanceWindow, (UnityEngine.Object) null))
          return;
      }
      this.mRuneEnhanceWindow.Setup(this, selected_rune);
    }

    private void OpenRuneEnhanceResultWindow(
      BindRuneData before_rune,
      BindRuneData after_rune,
      float _before_gauge)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneEnhanceEffectWindow, (UnityEngine.Object) null))
      {
        this.mRuneEnhanceEffectWindow = RuneManager.CreatePopupWindow<RuneEnhanceEffectWindow>("UI/Rune/RuneEnhanceEffect");
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneEnhanceEffectWindow, (UnityEngine.Object) null))
          return;
      }
      this.mRuneEnhanceEffectWindow.Setup(this, before_rune, after_rune, _before_gauge);
    }

    private void OpenOnlyRuneEvoWindow(BindRuneData selected_rune)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneEvoWindow, (UnityEngine.Object) null))
      {
        this.mRuneEvoWindow = RuneManager.CreatePopupWindow<RuneEvoWindow>("UI/Rune/RuneEvoWindow");
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneEvoWindow, (UnityEngine.Object) null))
          return;
      }
      this.mRuneEvoWindow.Setup(this, selected_rune);
    }

    private void OpenRuneEvoResultWindow(BindRuneData before_rune, BindRuneData after_rune)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneEvoEffectWindow, (UnityEngine.Object) null))
      {
        this.mRuneEvoEffectWindow = RuneManager.CreatePopupWindow<RuneEvoEffectWindow>("UI/Rune/RuneEvoEffect");
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneEvoEffectWindow, (UnityEngine.Object) null))
          return;
      }
      this.mRuneEvoEffectWindow.Setup(this, before_rune, after_rune);
    }

    private void OpenRuneSelectableListWindow(
      RuneSelectableListWindow.OnSelectedEvent selected_callback)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneSelectableListWindow, (UnityEngine.Object) null))
      {
        this.mRuneSelectableListWindow = RuneManager.CreatePopupWindow<RuneSelectableListWindow>("UI/Rune/RuneSelectableListWindow");
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneSelectableListWindow, (UnityEngine.Object) null))
          return;
        this.mRuneInventory.SetParentPopupObject(((Component) this.mRuneSelectableListWindow).gameObject, RuneInventory.WindowParent.SelectableList);
      }
      this.mRuneSelectableListWindow.Initialize(this, this.mPlayerRuneList, _is_use_equip_button: true);
      this.mRuneSelectableListWindow.SetOnSelectCallback(selected_callback);
    }

    private void CloseRuneSelectableListWindow()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneSelectableListWindow, (UnityEngine.Object) null))
        return;
      this.mRuneSelectableListWindow.CloseSelf();
      this.mRuneSelectableListWindow = (RuneSelectableListWindow) null;
    }

    private void OpenRuneDisassemblyWindow()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneDisassemblyWindow, (UnityEngine.Object) null))
      {
        this.mRuneDisassemblyWindow = RuneManager.CreatePopupWindow<RuneDisassemblyWindow>("UI/Rune/RuneDisassemblyWindow");
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneDisassemblyWindow, (UnityEngine.Object) null))
          return;
      }
      this.mRuneDisassemblyWindow.Setup(this, this.mPlayerRuneList);
    }

    private void CloseRuneDisassemblyWindow()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneDisassemblyWindow, (UnityEngine.Object) null))
        return;
      this.mRuneDisassemblyWindow.CloseSelf();
      this.mRuneDisassemblyWindow = (RuneDisassemblyWindow) null;
    }

    private void OpenRuneDisassemblyResultWindow()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneDisassemblyEffectWindow, (UnityEngine.Object) null))
        return;
      this.mRuneDisassemblyEffectWindow = RuneManager.CreatePopupWindow<RuneDisassemblyEffectWindow>("UI/Rune/RuneDisassemblyEffect");
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneDisassemblyEffectWindow, (UnityEngine.Object) null))
        ;
    }

    private void OpenRuneResetStatusWindow(
      BindRuneData rune_data,
      bool _is_enhance_evo = false,
      int _evo_slot_num = 0)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneResetStatusWindow, (UnityEngine.Object) null))
      {
        this.mRuneResetStatusWindow = RuneManager.CreatePopupWindow<RuneResetStatusWindow>("UI/Rune/RuneResetStatusWindow");
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneResetStatusWindow, (UnityEngine.Object) null))
          return;
      }
      this.mRuneResetStatusWindow.Setup(this, rune_data, _is_enhance_evo, _evo_slot_num);
    }

    private void CloseRuneResetStatusWindow()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneResetStatusWindow, (UnityEngine.Object) null))
        return;
      this.mRuneResetStatusWindow.CloseSelf();
      this.mRuneResetStatusWindow = (RuneResetStatusWindow) null;
    }

    private void OpenRuneParamEnhEvoEffectWindow(BindRuneData before_rune, BindRuneData after_rune)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneParamEnhEvoEffectWindow, (UnityEngine.Object) null))
      {
        this.mRuneParamEnhEvoEffectWindow = RuneManager.CreatePopupWindow<RuneParamEnhEvoEffectWindow>("UI/Rune/RuneEnhanceStatusEvoEffect");
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneParamEnhEvoEffectWindow, (UnityEngine.Object) null))
          return;
      }
      this.mRuneParamEnhEvoEffectWindow.Setup(this, before_rune, after_rune);
    }

    public enum RuneEquip
    {
      UnEquip,
      Equip,
      Max,
    }

    public enum Mode
    {
      Equip,
      Disassembly,
      Enhance,
      Evolution,
      ResetStatus,
    }
  }
}
