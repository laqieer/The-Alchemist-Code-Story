﻿// Decompiled with JetBrains decompiler
// Type: SRPG.UnitAbilityList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(100, "アビリティ詳細画面の表示", FlowNode.PinTypes.Output, 100)]
  public class UnitAbilityList : MonoBehaviour, IFlowInterface, IGameParameter
  {
    public UnitAbilityList.RefreshTypes RefreshOnStart;
    private UnitAbilityList.RefreshTypes mLastDisplayMode;
    private EAbilitySlot mLastDisplaySlot;
    public bool AutoRefresh;
    public UnitAbilityList.AbilityEvent OnAbilitySelect;
    public UnitAbilityList.AbilityEvent OnAbilityRankUp;
    public UnitAbilityList.AbilityEvent OnRankUpBtnPress;
    public UnitAbilityList.AbilityEvent OnAbilityRankUpExec;
    public UnitAbilityList.AbilitySlotEvent OnSlotSelect;
    public UnitData Unit;
    [Description("アビリティ詳細を表示するアンカー位置 (0.0 - 1.0)")]
    public Vector2 TooltipAnchorPos = new Vector2(1f, 0.5f);
    public GameObject AbilityTooltip;
    [Description("通常状態のアイテムとして使用する雛形")]
    public UnitAbilityListItemEvents Item_Normal;
    [Description("ロック状態のアイテムとして使用する雛形")]
    public UnitAbilityListItemEvents Item_Locked;
    [Description("使用不可スロットの雛形")]
    public UnitAbilityListItemEvents Item_NoSlot;
    [Description("アビリティが割り当てられていないスロットの雛形")]
    public UnitAbilityListItemEvents Item_Empty;
    [Description("スロットが違うアイテムの雛形")]
    public UnitAbilityListItemEvents Item_SlotMismatch;
    [Description("変更できないアビリティのスロットの雛形")]
    public UnitAbilityListItemEvents Item_Fixed;
    private List<UnitAbilityListItemEvents> mItems = new List<UnitAbilityListItemEvents>();
    [Description("固定アビリティを表示する")]
    public bool ShowFixedAbilities;
    [Description("マスターアビリティを表示する")]
    public bool ShowMasterAbilities;
    public bool ShowLockedJobAbilities;
    public ScrollRect ScrollParent;
    private float mDecelerationRate;

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Normal, (UnityEngine.Object) null))
        ((Component) this.Item_Normal).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Locked, (UnityEngine.Object) null))
        ((Component) this.Item_Locked).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_NoSlot, (UnityEngine.Object) null))
        ((Component) this.Item_NoSlot).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Empty, (UnityEngine.Object) null))
        ((Component) this.Item_Empty).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_SlotMismatch, (UnityEngine.Object) null))
        ((Component) this.Item_SlotMismatch).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Fixed, (UnityEngine.Object) null))
        ((Component) this.Item_Fixed).gameObject.SetActive(false);
      if (this.RefreshOnStart == UnitAbilityList.RefreshTypes.DisplayAll)
      {
        this.DisplayAll();
      }
      else
      {
        if (this.RefreshOnStart != UnitAbilityList.RefreshTypes.DisplaySlots)
          return;
        this.DisplaySlots();
      }
    }

    public void Activated(int pinID)
    {
    }

    public void DisplayAll() => this.DisplaySlotType(~EAbilitySlot.Action);

    public void DisplaySlots()
    {
      this.mLastDisplayMode = UnitAbilityList.RefreshTypes.DisplaySlots;
      for (int index = 0; index < this.mItems.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mItems[index]).gameObject);
      this.mItems.Clear();
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null);
      if (dataOfClass != null)
        this.Unit = dataOfClass;
      if (this.Unit == null)
        return;
      GlobalVars.SelectedUnitUniqueID.Set(this.Unit.UniqueID);
      List<AbilityData> abilityDataList = new List<AbilityData>((IEnumerable<AbilityData>) this.Unit.CreateEquipAbilitys());
      if (this.Unit.MasterAbility != null)
        abilityDataList.Add(this.Unit.MasterAbility);
      Transform transform = ((Component) this).transform;
      for (int index = 0; index < abilityDataList.Count; ++index)
      {
        AbilityData derivedAbility = abilityDataList[index];
        bool flag = false;
        if (derivedAbility != null && derivedAbility.Param != null && derivedAbility.IsDeriveBaseAbility)
          derivedAbility = derivedAbility.DerivedAbility;
        UnitAbilityListItemEvents abilityListItemEvents1;
        if (derivedAbility != null && derivedAbility.Param != null)
        {
          flag = derivedAbility.Param.is_fixed;
          UnitAbilityListItemEvents abilityListItemEvents2 = this.Unit.MasterAbility == null || this.Unit.MasterAbility != derivedAbility ? (!flag ? this.Item_Normal : this.Item_Fixed) : this.Item_Fixed;
          if (derivedAbility.IsDerivedAbility && derivedAbility.DeriveBaseAbility == this.Unit.MasterAbility)
            abilityListItemEvents2 = this.Item_Fixed;
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) abilityListItemEvents2, (UnityEngine.Object) null))
            abilityListItemEvents2 = this.Item_Normal;
          abilityListItemEvents1 = UnityEngine.Object.Instantiate<UnitAbilityListItemEvents>(abilityListItemEvents2);
          AbilityDeriveList component = ((Component) abilityListItemEvents1).gameObject.GetComponent<AbilityDeriveList>();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            DataSource.Bind<AbilityData>(((Component) abilityListItemEvents1).gameObject, derivedAbility);
            string key = this.Unit.SearchAbilityReplacementSkill(derivedAbility.Param.iname);
            if (!string.IsNullOrEmpty(key))
              DataSource.Bind<AbilityParam>(((Component) abilityListItemEvents1).gameObject, MonoSingleton<GameManager>.Instance.GetAbilityParam(key));
            abilityListItemEvents1.IsEnableSkillChange = true;
            JobData job;
            int rank;
            if (this.GetAbilitySource(derivedAbility.AbilityID, out job, out rank))
              DataSource.Bind<AbilityUnlockInfo>(((Component) abilityListItemEvents1).gameObject, new AbilityUnlockInfo()
              {
                JobName = job.Name,
                Rank = rank
              });
            abilityListItemEvents1.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
            abilityListItemEvents1.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
            abilityListItemEvents1.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
            abilityListItemEvents1.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
          }
          else if (derivedAbility.IsDerivedAbility)
          {
            component.SetupWithAbilityParam(derivedAbility.Param, new List<AbilityDeriveParam>()
            {
              derivedAbility.DeriveParam
            });
            // ISSUE: method pointer
            component.AddDetailOpenEventListener(new UnityAction<GameObject>((object) this, __methodptr(_OnAbilityDetail)));
          }
          else
          {
            component.SetupWithAbilityParam(derivedAbility.Param, (List<AbilityDeriveParam>) null);
            // ISSUE: method pointer
            component.AddDetailOpenEventListener(new UnityAction<GameObject>((object) this, __methodptr(_OnAbilityDetail)));
          }
        }
        else
        {
          AbilityParam data = new AbilityParam();
          data.slot = JobData.ABILITY_SLOT_TYPES[index];
          abilityListItemEvents1 = UnityEngine.Object.Instantiate<UnitAbilityListItemEvents>(this.Item_Empty);
          DataSource.Bind<AbilityParam>(((Component) abilityListItemEvents1).gameObject, data);
        }
        ((Component) abilityListItemEvents1).transform.SetParent(transform, false);
        ((Component) abilityListItemEvents1).gameObject.SetActive(true);
        GameParameter.UpdateAll(((Component) abilityListItemEvents1).gameObject);
        if (!flag)
          abilityListItemEvents1.OnSelect = new ListItemEvents.ListItemEvent(this._OnSlotSelect);
        this.mItems.Add(abilityListItemEvents1);
      }
    }

    private void _OnSlotSelect(GameObject go)
    {
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) ((Component) this.mItems[index]).gameObject, (UnityEngine.Object) go))
        {
          if (this.OnSlotSelect == null)
            break;
          this.OnSlotSelect(index);
          break;
        }
      }
    }

    private GameObject GetItemRoot(GameObject go) => go;

    private void _OnAbilitySelect(GameObject go)
    {
      AbilityData dataOfClass = DataSource.FindDataOfClass<AbilityData>(go, (AbilityData) null);
      go = this.GetItemRoot(go);
      if (this.OnAbilitySelect == null)
        return;
      this.OnAbilitySelect(dataOfClass, go);
    }

    private void _OnAbilityRankUp(GameObject go)
    {
      AbilityData dataOfClass = DataSource.FindDataOfClass<AbilityData>(go, (AbilityData) null);
      go = this.GetItemRoot(go);
      if (this.OnAbilityRankUp != null)
        this.OnAbilityRankUp(dataOfClass, go);
      GameParameter.UpdateAll(go);
    }

    private void _OnAbilityDetail(GameObject go)
    {
      GlobalVars.SelectedAbilityID.Set(string.Empty);
      GlobalVars.SelectedAbilityUniqueID.Set(0L);
      AbilityData dataOfClass1 = DataSource.FindDataOfClass<AbilityData>(go, (AbilityData) null);
      if (dataOfClass1 != null)
      {
        GlobalVars.SelectedAbilityID.Set(dataOfClass1.Param.iname);
        GlobalVars.SelectedAbilityUniqueID.Set(dataOfClass1.UniqueID);
      }
      else
      {
        AbilityParam dataOfClass2 = DataSource.FindDataOfClass<AbilityParam>(go, (AbilityParam) null);
        GlobalVars.SelectedAbilityID.Set(dataOfClass2 == null ? (string) null : dataOfClass2.iname);
      }
      if (string.IsNullOrEmpty((string) GlobalVars.SelectedAbilityID))
        return;
      EventSystem current = EventSystem.current;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) current, (UnityEngine.Object) null))
      {
        ((Behaviour) current).enabled = false;
        ((Behaviour) current).enabled = true;
      }
      GameSettings instance = GameSettings.Instance;
      if (!string.IsNullOrEmpty(instance.Dialog_AbilityDetail))
      {
        GameObject gameObject = AssetManager.Load<GameObject>(instance.Dialog_AbilityDetail);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          UnityEngine.Object.Instantiate<GameObject>(gameObject);
      }
      bool flag = false;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) go))
      {
        ListItemEvents component = go.GetComponent<ListItemEvents>();
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) component))
          flag = component.IsEnableSkillChange;
      }
      AbilityDetailWindow.IsEnableSkillChange = flag;
      AbilityDetailWindow.SetBindObject(this.Unit);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void _OnRankUpBtnPress(GameObject go)
    {
      AbilityData dataOfClass = DataSource.FindDataOfClass<AbilityData>(go, (AbilityData) null);
      go = this.GetItemRoot(go);
      if (this.OnRankUpBtnPress != null)
        this.OnRankUpBtnPress(dataOfClass, go);
      GameParameter.UpdateAll(go);
    }

    private void _OnRankUpBtnUp(GameObject go)
    {
      AbilityData dataOfClass = DataSource.FindDataOfClass<AbilityData>(go, (AbilityData) null);
      go = this.GetItemRoot(go);
      if (this.OnAbilityRankUpExec != null)
        this.OnAbilityRankUpExec(dataOfClass, go);
      GameParameter.UpdateAll(go);
    }

    private bool GetAbilitySource(string abilityID, out JobData job, out int rank)
    {
      for (int index = 0; index < this.Unit.Jobs.Length; ++index)
      {
        rank = this.Unit.Jobs[index].Param.FindRankOfAbility(abilityID);
        if (rank != -1)
        {
          job = this.Unit.Jobs[index];
          return true;
        }
      }
      job = (JobData) null;
      rank = -1;
      return false;
    }

    public void DisplaySlotType(EAbilitySlot slotType, bool hideEquipped = false, bool showDerivedAbility = false)
    {
      this.mLastDisplayMode = UnitAbilityList.RefreshTypes.DisplayAll;
      this.mLastDisplaySlot = slotType;
      for (int index = 0; index < this.mItems.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mItems[index]).gameObject);
      this.mItems.Clear();
      if (this.Unit == null)
        this.Unit = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null);
      if (this.Unit == null)
        return;
      List<AbilityData> abilityDataList = !showDerivedAbility ? this.Unit.GetAllLearnedAbilities() : this.Unit.GetAllLearnedAbilities(true);
      Transform transform = ((Component) this).transform;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Normal, (UnityEngine.Object) null))
      {
        for (int index1 = 0; index1 < abilityDataList.Count; ++index1)
        {
          AbilityData abilityData = abilityDataList[index1];
          if ((slotType == ~EAbilitySlot.Action || slotType == abilityData.SlotType) && (this.ShowFixedAbilities || !abilityData.Param.is_fixed) && (this.ShowMasterAbilities || !(this.Unit.UnitParam.ability == abilityData.AbilityID) && !this.Unit.IsQuestClearUnlocked(abilityData.Param.iname, QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility)) && this.Unit.MapEffectAbility != abilityData && (this.ShowMasterAbilities || !this.Unit.TobiraMasterAbilitys.Contains(abilityData)) && (!abilityData.IsDerivedAbility || (this.ShowFixedAbilities || !abilityData.DeriveBaseAbility.Param.is_fixed) && (this.ShowMasterAbilities || !(this.Unit.UnitParam.ability == abilityData.DeriveBaseAbility.AbilityID) && !this.Unit.IsQuestClearUnlocked(abilityData.DeriveBaseAbility.Param.iname, QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility)) && this.Unit.MapEffectAbility != abilityData.DeriveBaseAbility && (this.ShowMasterAbilities || !this.Unit.TobiraMasterAbilitys.Contains(abilityData.DeriveBaseAbility))))
          {
            if (hideEquipped)
            {
              bool flag = false;
              for (int index2 = 0; index2 < this.Unit.CurrentJob.AbilitySlots.Length; ++index2)
              {
                if (this.Unit.CurrentJob.AbilitySlots[index2] == abilityData.UniqueID)
                {
                  flag = true;
                  break;
                }
              }
              if (flag)
                continue;
            }
            UnitAbilityListItemEvents abilityListItemEvents = UnityEngine.Object.Instantiate<UnitAbilityListItemEvents>(this.Item_Normal);
            this.mItems.Add(abilityListItemEvents);
            ((Component) abilityListItemEvents).transform.SetParent(transform, false);
            ((Component) abilityListItemEvents).gameObject.SetActive(true);
            AbilityDeriveList component = ((Component) abilityListItemEvents).gameObject.GetComponent<AbilityDeriveList>();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
            {
              DataSource.Bind<AbilityData>(((Component) abilityListItemEvents).gameObject, abilityData);
              abilityListItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this._OnAbilitySelect);
              abilityListItemEvents.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
              abilityListItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
              abilityListItemEvents.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
              abilityListItemEvents.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
              JobData job;
              int rank;
              if (this.GetAbilitySource(abilityData.AbilityID, out job, out rank))
                DataSource.Bind<AbilityUnlockInfo>(((Component) abilityListItemEvents).gameObject, new AbilityUnlockInfo()
                {
                  JobName = job.Name,
                  Rank = rank
                });
            }
            else if (abilityData.IsDerivedAbility)
            {
              component.SetupWithAbilityData(abilityData.DeriveBaseAbility, new List<AbilityData>()
              {
                abilityData
              });
              // ISSUE: method pointer
              component.AddDetailOpenEventListener(new UnityAction<GameObject>((object) this, __methodptr(_OnAbilityDetail)));
              // ISSUE: method pointer
              component.AddSelectEventListener(new UnityAction<GameObject>((object) this, __methodptr(_OnAbilitySelect)));
              // ISSUE: method pointer
              component.AddRankUpEventListener(new UnityAction<GameObject>((object) this, __methodptr(_OnAbilityRankUp)));
              // ISSUE: method pointer
              component.AddRankUpBtnPressEventListener(new UnityAction<GameObject>((object) this, __methodptr(_OnRankUpBtnPress)));
              // ISSUE: method pointer
              component.AddRankUpBtnUpEventListener(new UnityAction<GameObject>((object) this, __methodptr(_OnRankUpBtnUp)));
            }
            else
            {
              component.SetupWithAbilityData(abilityData, (List<AbilityData>) null, this.Unit);
              // ISSUE: method pointer
              component.AddDetailOpenEventListener(new UnityAction<GameObject>((object) this, __methodptr(_OnAbilityDetail)));
              // ISSUE: method pointer
              component.AddSelectEventListener(new UnityAction<GameObject>((object) this, __methodptr(_OnAbilitySelect)));
              // ISSUE: method pointer
              component.AddRankUpEventListener(new UnityAction<GameObject>((object) this, __methodptr(_OnAbilityRankUp)));
              // ISSUE: method pointer
              component.AddRankUpBtnPressEventListener(new UnityAction<GameObject>((object) this, __methodptr(_OnRankUpBtnPress)));
              // ISSUE: method pointer
              component.AddRankUpBtnUpEventListener(new UnityAction<GameObject>((object) this, __methodptr(_OnRankUpBtnUp)));
            }
          }
        }
      }
      if (slotType != ~EAbilitySlot.Action && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_SlotMismatch, (UnityEngine.Object) null))
      {
        for (int index = 0; index < abilityDataList.Count; ++index)
        {
          AbilityData data = abilityDataList[index];
          if (slotType != data.SlotType && (this.ShowFixedAbilities || !data.Param.is_fixed) && (this.ShowMasterAbilities || !(this.Unit.UnitParam.ability == data.AbilityID) && !this.Unit.IsQuestClearUnlocked(data.Param.iname, QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility)) && this.Unit.MapEffectAbility != data && (this.ShowMasterAbilities || !this.Unit.TobiraMasterAbilitys.Contains(data)))
          {
            UnitAbilityListItemEvents abilityListItemEvents = UnityEngine.Object.Instantiate<UnitAbilityListItemEvents>(this.Item_SlotMismatch);
            this.mItems.Add(abilityListItemEvents);
            DataSource.Bind<AbilityData>(((Component) abilityListItemEvents).gameObject, data);
            abilityListItemEvents.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
            abilityListItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
            abilityListItemEvents.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
            abilityListItemEvents.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
            ((Component) abilityListItemEvents).transform.SetParent(transform, false);
            ((Component) abilityListItemEvents).gameObject.SetActive(true);
            JobData job;
            int rank;
            if (this.GetAbilitySource(data.AbilityID, out job, out rank))
              DataSource.Bind<AbilityUnlockInfo>(((Component) abilityListItemEvents).gameObject, new AbilityUnlockInfo()
              {
                JobName = job.Name,
                Rank = rank
              });
          }
        }
      }
      List<string> overwrittenAbilitys = TobiraUtility.GetOverwrittenAbilitys(this.Unit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Locked, (UnityEngine.Object) null))
      {
        GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
        for (int index3 = 0; index3 < this.Unit.Jobs.Length; ++index3)
        {
          if (this.ShowLockedJobAbilities || this.Unit.Jobs[index3].Rank > 0)
          {
            RarityParam rarityParam = instanceDirect.GetRarityParam((int) this.Unit.UnitParam.raremax);
            for (int lv = this.Unit.Jobs[index3].Rank + 1; lv <= JobParam.MAX_JOB_RANK; ++lv)
            {
              OString[] learningAbilitys = this.Unit.Jobs[index3].Param.GetLearningAbilitys(lv);
              if (learningAbilitys != null && (int) rarityParam.UnitJobLvCap >= lv)
              {
                for (int index4 = 0; index4 < learningAbilitys.Length; ++index4)
                {
                  string key = (string) learningAbilitys[index4];
                  if (!string.IsNullOrEmpty(key))
                  {
                    AbilityParam abilityParam = instanceDirect.GetAbilityParam(key);
                    if ((this.ShowFixedAbilities || !abilityParam.is_fixed) && !overwrittenAbilitys.Contains(abilityParam.iname))
                    {
                      UnitAbilityListItemEvents abilityListItemEvents = UnityEngine.Object.Instantiate<UnitAbilityListItemEvents>(this.Item_Locked);
                      this.mItems.Add(abilityListItemEvents);
                      DataSource.Bind<AbilityParam>(((Component) abilityListItemEvents).gameObject, abilityParam);
                      abilityListItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this._OnAbilitySelect);
                      abilityListItemEvents.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
                      abilityListItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
                      abilityListItemEvents.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
                      abilityListItemEvents.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
                      ((Component) abilityListItemEvents).transform.SetParent(transform, false);
                      ((Component) abilityListItemEvents).gameObject.SetActive(true);
                      DataSource.Bind<AbilityUnlockInfo>(((Component) abilityListItemEvents).gameObject, new AbilityUnlockInfo()
                      {
                        JobName = this.Unit.Jobs[index3].Name,
                        Rank = lv
                      });
                    }
                  }
                }
              }
            }
          }
        }
        if (this.ShowMasterAbilities && !string.IsNullOrEmpty(this.Unit.UnitParam.ability) && this.Unit.LearnAbilitys.Find((Predicate<AbilityData>) (p => p.AbilityID == this.Unit.UnitParam.ability)) == null && !overwrittenAbilitys.Exists((Predicate<string>) (abil => abil == this.Unit.UnitParam.ability)))
        {
          AbilityParam abilityParam = instanceDirect.GetAbilityParam(this.Unit.UnitParam.ability);
          UnitAbilityListItemEvents abilityListItemEvents = UnityEngine.Object.Instantiate<UnitAbilityListItemEvents>(this.Item_Locked);
          this.mItems.Add(abilityListItemEvents);
          DataSource.Bind<AbilityParam>(((Component) abilityListItemEvents).gameObject, abilityParam);
          abilityListItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this._OnAbilitySelect);
          abilityListItemEvents.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
          abilityListItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
          abilityListItemEvents.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
          abilityListItemEvents.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
          ((Component) abilityListItemEvents).transform.SetParent(transform, false);
          ((Component) abilityListItemEvents).gameObject.SetActive(true);
        }
      }
      this.ResetScrollPos();
    }

    [DebuggerHidden]
    private IEnumerator RefreshScrollRect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitAbilityList.\u003CRefreshScrollRect\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public void UpdateItem(AbilityData ability)
    {
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (DataSource.FindDataOfClass<AbilityData>(((Component) this.mItems[index]).gameObject, (AbilityData) null) == ability)
          GameParameter.UpdateAll(((Component) this.mItems[index]).gameObject);
      }
    }

    public bool IsEmpty => this.mItems.Count == 0;

    public void UpdateValue()
    {
      if (!this.AutoRefresh)
        return;
      if (this.mLastDisplayMode == UnitAbilityList.RefreshTypes.DisplayAll)
      {
        this.DisplaySlotType(this.mLastDisplaySlot);
      }
      else
      {
        if (this.mLastDisplayMode != UnitAbilityList.RefreshTypes.DisplaySlots)
          return;
        this.DisplaySlots();
      }
    }

    public void ResetScrollPos()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollParent, (UnityEngine.Object) null))
      {
        this.mDecelerationRate = this.ScrollParent.decelerationRate;
        this.ScrollParent.decelerationRate = 0.0f;
      }
      RectTransform transform = ((Component) this).transform as RectTransform;
      transform.anchoredPosition = new Vector2(transform.anchoredPosition.x, 0.0f);
      this.StartCoroutine(this.RefreshScrollRect());
    }

    public enum RefreshTypes
    {
      None,
      DisplayAll,
      DisplaySlots,
    }

    public delegate void AbilityEvent(AbilityData ability, GameObject itemGO);

    public delegate void AbilitySlotEvent(int slotIndex);
  }
}
