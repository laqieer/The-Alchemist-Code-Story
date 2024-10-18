// Decompiled with JetBrains decompiler
// Type: SRPG.UnitAbilityList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "アビリティ詳細画面の表示", FlowNode.PinTypes.Output, 100)]
  public class UnitAbilityList : MonoBehaviour, IGameParameter, IFlowInterface
  {
    [Description("アビリティ詳細を表示するアンカー位置 (0.0 - 1.0)")]
    public Vector2 TooltipAnchorPos = new Vector2(1f, 0.5f);
    private List<UnitAbilityListItemEvents> mItems = new List<UnitAbilityListItemEvents>();
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
    [Description("固定アビリティを表示する")]
    public bool ShowFixedAbilities;
    [Description("マスターアビリティを表示する")]
    public bool ShowMasterAbilities;
    public bool ShowLockedJobAbilities;
    public ScrollRect ScrollParent;
    private float mDecelerationRate;

    private void Start()
    {
      if ((UnityEngine.Object) this.Item_Normal != (UnityEngine.Object) null)
        this.Item_Normal.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.Item_Locked != (UnityEngine.Object) null)
        this.Item_Locked.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.Item_NoSlot != (UnityEngine.Object) null)
        this.Item_NoSlot.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.Item_Empty != (UnityEngine.Object) null)
        this.Item_Empty.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.Item_SlotMismatch != (UnityEngine.Object) null)
        this.Item_SlotMismatch.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.Item_Fixed != (UnityEngine.Object) null)
        this.Item_Fixed.gameObject.SetActive(false);
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

    public void DisplayAll()
    {
      this.DisplaySlotType(~EAbilitySlot.Action, false);
    }

    public void DisplaySlots()
    {
      this.mLastDisplayMode = UnitAbilityList.RefreshTypes.DisplaySlots;
      for (int index = 0; index < this.mItems.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mItems[index].gameObject);
      this.mItems.Clear();
      if (this.Unit == null)
        this.Unit = DataSource.FindDataOfClass<UnitData>(this.gameObject, (UnitData) null);
      if (this.Unit == null)
        return;
      GlobalVars.SelectedUnitUniqueID.Set(this.Unit.UniqueID);
      AbilityData[] equipAbilitys = this.Unit.CreateEquipAbilitys();
      Transform transform = this.transform;
      for (int index = 0; index < this.Unit.CurrentJob.AbilitySlots.Length; ++index)
      {
        AbilityData data1 = equipAbilitys[index];
        bool flag = false;
        UnitAbilityListItemEvents abilityListItemEvents;
        if (data1 != null && data1.Param != null)
        {
          flag = data1.Param.is_fixed;
          UnitAbilityListItemEvents original = !flag ? this.Item_Normal : this.Item_Fixed;
          if ((UnityEngine.Object) original == (UnityEngine.Object) null)
            original = this.Item_Normal;
          abilityListItemEvents = UnityEngine.Object.Instantiate<UnitAbilityListItemEvents>(original);
          DataSource.Bind<AbilityData>(abilityListItemEvents.gameObject, data1);
          string key = this.Unit.SearchAbilityReplacementSkill(data1.Param.iname);
          if (!string.IsNullOrEmpty(key))
            DataSource.Bind<AbilityParam>(abilityListItemEvents.gameObject, MonoSingleton<GameManager>.Instance.GetAbilityParam(key));
          abilityListItemEvents.IsEnableSkillChange = true;
          JobData job;
          int rank;
          if (this.GetAbilitySource(data1.AbilityID, out job, out rank))
            DataSource.Bind<AbilityUnlockInfo>(abilityListItemEvents.gameObject, new AbilityUnlockInfo()
            {
              JobName = job.Name,
              Rank = rank
            });
          abilityListItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
          abilityListItemEvents.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
          abilityListItemEvents.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
          abilityListItemEvents.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
        }
        else
        {
          AbilityParam data2 = new AbilityParam();
          data2.slot = JobData.ABILITY_SLOT_TYPES[index];
          abilityListItemEvents = UnityEngine.Object.Instantiate<UnitAbilityListItemEvents>(this.Item_Empty);
          DataSource.Bind<AbilityParam>(abilityListItemEvents.gameObject, data2);
        }
        abilityListItemEvents.transform.SetParent(transform, false);
        abilityListItemEvents.gameObject.SetActive(true);
        if (!flag)
          abilityListItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this._OnSlotSelect);
        this.mItems.Add(abilityListItemEvents);
      }
    }

    private void _OnSlotSelect(GameObject go)
    {
      for (int slotIndex = 0; slotIndex < this.mItems.Count; ++slotIndex)
      {
        if ((UnityEngine.Object) this.mItems[slotIndex].gameObject == (UnityEngine.Object) go)
        {
          if (this.OnSlotSelect == null)
            break;
          this.OnSlotSelect(slotIndex);
          break;
        }
      }
    }

    private GameObject GetItemRoot(GameObject go)
    {
      return go;
    }

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
      if ((UnityEngine.Object) current != (UnityEngine.Object) null)
      {
        current.enabled = false;
        current.enabled = true;
      }
      GameSettings instance = GameSettings.Instance;
      if (!string.IsNullOrEmpty(instance.Dialog_AbilityDetail))
      {
        GameObject original = AssetManager.Load<GameObject>(instance.Dialog_AbilityDetail);
        if ((UnityEngine.Object) original != (UnityEngine.Object) null)
          UnityEngine.Object.Instantiate<GameObject>(original);
      }
      bool flag = false;
      if ((bool) ((UnityEngine.Object) go))
      {
        ListItemEvents component = go.GetComponent<ListItemEvents>();
        if ((bool) ((UnityEngine.Object) component))
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

    public void DisplaySlotType(EAbilitySlot slotType, bool hideEquipped = false)
    {
      this.mLastDisplayMode = UnitAbilityList.RefreshTypes.DisplayAll;
      this.mLastDisplaySlot = slotType;
      for (int index = 0; index < this.mItems.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mItems[index].gameObject);
      this.mItems.Clear();
      if (this.Unit == null)
        this.Unit = DataSource.FindDataOfClass<UnitData>(this.gameObject, (UnitData) null);
      if (this.Unit == null)
        return;
      List<AbilityData> learnedAbilities = this.Unit.GetAllLearnedAbilities();
      Transform transform1 = this.transform;
      bool flag1 = false;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if ((instance.Player.TutorialFlags & 1L) == 0L && instance.GetNextTutorialStep() == "ShowAbilityTab")
      {
        instance.CompleteTutorialStep();
        if (instance.GetNextTutorialStep() == "ShowAbilityLvUp")
          flag1 = true;
      }
      if ((UnityEngine.Object) this.Item_Normal != (UnityEngine.Object) null)
      {
        for (int index1 = 0; index1 < learnedAbilities.Count; ++index1)
        {
          AbilityData data = learnedAbilities[index1];
          if ((slotType == ~EAbilitySlot.Action || slotType == data.SlotType) && (this.ShowFixedAbilities || !data.Param.is_fixed) && (this.ShowMasterAbilities || !((string) this.Unit.UnitParam.ability == data.AbilityID) && !this.Unit.IsQuestClearUnlocked(data.Param.iname, QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility)) && this.Unit.MapEffectAbility != data)
          {
            if (hideEquipped)
            {
              bool flag2 = false;
              for (int index2 = 0; index2 < this.Unit.CurrentJob.AbilitySlots.Length; ++index2)
              {
                if (this.Unit.CurrentJob.AbilitySlots[index2] == data.UniqueID)
                {
                  flag2 = true;
                  break;
                }
              }
              if (flag2)
                continue;
            }
            UnitAbilityListItemEvents abilityListItemEvents = UnityEngine.Object.Instantiate<UnitAbilityListItemEvents>(this.Item_Normal);
            this.mItems.Add(abilityListItemEvents);
            DataSource.Bind<AbilityData>(abilityListItemEvents.gameObject, data);
            abilityListItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this._OnAbilitySelect);
            abilityListItemEvents.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
            abilityListItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
            abilityListItemEvents.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
            abilityListItemEvents.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
            abilityListItemEvents.transform.SetParent(transform1, false);
            abilityListItemEvents.gameObject.SetActive(true);
            JobData job;
            int rank;
            if (this.GetAbilitySource(data.AbilityID, out job, out rank))
              DataSource.Bind<AbilityUnlockInfo>(abilityListItemEvents.gameObject, new AbilityUnlockInfo()
              {
                JobName = job.Name,
                Rank = rank
              });
            if (flag1 && index1 == 0)
            {
              SGHighlightObject.Instance().highlightedObject = abilityListItemEvents.RankupButton.gameObject;
              SGHighlightObject.Instance().Highlight(string.Empty, "sg_tut_1.023", (SGHighlightObject.OnActivateCallback) null, EventDialogBubble.Anchors.TopLeft, true, false, false);
            }
          }
        }
      }
      if (slotType != ~EAbilitySlot.Action && (UnityEngine.Object) this.Item_SlotMismatch != (UnityEngine.Object) null)
      {
        for (int index = 0; index < learnedAbilities.Count; ++index)
        {
          AbilityData data = learnedAbilities[index];
          if (slotType != data.SlotType && (this.ShowFixedAbilities || !data.Param.is_fixed) && (this.ShowMasterAbilities || !((string) this.Unit.UnitParam.ability == data.AbilityID) && !this.Unit.IsQuestClearUnlocked(data.Param.iname, QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility)) && this.Unit.MapEffectAbility != data)
          {
            UnitAbilityListItemEvents abilityListItemEvents = UnityEngine.Object.Instantiate<UnitAbilityListItemEvents>(this.Item_SlotMismatch);
            this.mItems.Add(abilityListItemEvents);
            DataSource.Bind<AbilityData>(abilityListItemEvents.gameObject, data);
            abilityListItemEvents.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
            abilityListItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
            abilityListItemEvents.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
            abilityListItemEvents.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
            abilityListItemEvents.transform.SetParent(transform1, false);
            abilityListItemEvents.gameObject.SetActive(true);
            JobData job;
            int rank;
            if (this.GetAbilitySource(data.AbilityID, out job, out rank))
              DataSource.Bind<AbilityUnlockInfo>(abilityListItemEvents.gameObject, new AbilityUnlockInfo()
              {
                JobName = job.Name,
                Rank = rank
              });
          }
        }
      }
      if ((UnityEngine.Object) this.Item_Locked != (UnityEngine.Object) null)
      {
        GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
        for (int index1 = 0; index1 < this.Unit.Jobs.Length; ++index1)
        {
          if (this.ShowLockedJobAbilities || this.Unit.Jobs[index1].Rank > 0)
          {
            RarityParam rarityParam = instanceDirect.GetRarityParam((int) this.Unit.UnitParam.raremax);
            for (int lv = this.Unit.Jobs[index1].Rank + 1; lv < JobParam.MAX_JOB_RANK; ++lv)
            {
              OString[] learningAbilitys = this.Unit.Jobs[index1].Param.GetLearningAbilitys(lv);
              if (learningAbilitys != null && (int) rarityParam.UnitJobLvCap >= lv)
              {
                for (int index2 = 0; index2 < learningAbilitys.Length; ++index2)
                {
                  string key = (string) learningAbilitys[index2];
                  if (!string.IsNullOrEmpty(key))
                  {
                    AbilityParam abilityParam = instanceDirect.GetAbilityParam(key);
                    if (this.ShowFixedAbilities || !abilityParam.is_fixed)
                    {
                      UnitAbilityListItemEvents abilityListItemEvents = UnityEngine.Object.Instantiate<UnitAbilityListItemEvents>(this.Item_Locked);
                      this.mItems.Add(abilityListItemEvents);
                      DataSource.Bind<AbilityParam>(abilityListItemEvents.gameObject, abilityParam);
                      abilityListItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this._OnAbilitySelect);
                      abilityListItemEvents.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
                      abilityListItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
                      abilityListItemEvents.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
                      abilityListItemEvents.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
                      abilityListItemEvents.transform.SetParent(transform1, false);
                      abilityListItemEvents.gameObject.SetActive(true);
                      DataSource.Bind<AbilityUnlockInfo>(abilityListItemEvents.gameObject, new AbilityUnlockInfo()
                      {
                        JobName = this.Unit.Jobs[index1].Name,
                        Rank = lv
                      });
                    }
                  }
                }
              }
            }
          }
        }
        if (this.ShowMasterAbilities && !string.IsNullOrEmpty((string) this.Unit.UnitParam.ability) && this.Unit.LearnAbilitys.Find((Predicate<AbilityData>) (p => p.AbilityID == (string) this.Unit.UnitParam.ability)) == null)
        {
          AbilityParam abilityParam = instanceDirect.GetAbilityParam((string) this.Unit.UnitParam.ability);
          UnitAbilityListItemEvents abilityListItemEvents = UnityEngine.Object.Instantiate<UnitAbilityListItemEvents>(this.Item_Locked);
          this.mItems.Add(abilityListItemEvents);
          DataSource.Bind<AbilityParam>(abilityListItemEvents.gameObject, abilityParam);
          abilityListItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this._OnAbilitySelect);
          abilityListItemEvents.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
          abilityListItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
          abilityListItemEvents.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
          abilityListItemEvents.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
          abilityListItemEvents.transform.SetParent(transform1, false);
          abilityListItemEvents.gameObject.SetActive(true);
        }
      }
      if ((UnityEngine.Object) this.ScrollParent != (UnityEngine.Object) null)
      {
        this.mDecelerationRate = this.ScrollParent.decelerationRate;
        this.ScrollParent.decelerationRate = 0.0f;
      }
      RectTransform transform2 = this.transform as RectTransform;
      transform2.anchoredPosition = new Vector2(transform2.anchoredPosition.x, 0.0f);
      this.StartCoroutine(this.RefreshScrollRect());
    }

    [DebuggerHidden]
    private IEnumerator RefreshScrollRect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitAbilityList.\u003CRefreshScrollRect\u003Ec__Iterator12A() { \u003C\u003Ef__this = this };
    }

    public void UpdateItem(AbilityData ability)
    {
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (DataSource.FindDataOfClass<AbilityData>(this.mItems[index].gameObject, (AbilityData) null) == ability)
          GameParameter.UpdateAll(this.mItems[index].gameObject);
      }
    }

    public bool IsEmpty
    {
      get
      {
        return this.mItems.Count == 0;
      }
    }

    public void UpdateValue()
    {
      if (!this.AutoRefresh)
        return;
      if (this.mLastDisplayMode == UnitAbilityList.RefreshTypes.DisplayAll)
      {
        this.DisplaySlotType(this.mLastDisplaySlot, false);
      }
      else
      {
        if (this.mLastDisplayMode != UnitAbilityList.RefreshTypes.DisplaySlots)
          return;
        this.DisplaySlots();
      }
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
