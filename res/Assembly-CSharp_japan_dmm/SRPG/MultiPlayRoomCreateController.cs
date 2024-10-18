// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayRoomCreateController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class MultiPlayRoomCreateController : MonoBehaviour
  {
    public Toggle PassToggle;
    public Toggle LimitToggle;
    public Toggle ClearToggle;
    public GameObject Filter;
    public GameObject ClearEnableButton;
    public GameObject ClearDisableButton;
    public GameObject LimitEnableButton;
    public GameObject LimitDisableButton;
    public GameObject DetailFilter;
    public ScrollablePulldown UnitLvPulldown;
    public GraphicRaycaster UnitPulldownBG;
    public ScrollablePulldown BattleSpeedPulldown;
    public GraphicRaycaster BattleSpeedPulldownBG;
    private int mMaxLv;
    private int mNowSelIdx;

    private void Start()
    {
      List<UnitData> units = MonoSingleton<GameManager>.Instance.Player.Units;
      for (int index = 0; index < units.Count; ++index)
      {
        if (this.mMaxLv < units[index].Lv)
          this.mMaxLv = units[index].Lv;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PassToggle, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.PassToggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnPassToggle)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LimitToggle, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.LimitToggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnLimitToggle)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ClearToggle, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ClearToggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnClearToggle)));
      }
      this.Refresh();
    }

    private void Refresh()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      string empty = string.Empty;
      OInt[] multiPlayLimitUnitLv = instance.MasterParam.GetMultiPlayLimitUnitLv();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitLvPulldown, (UnityEngine.Object) null) && multiPlayLimitUnitLv != null)
      {
        int num = 0;
        this.UnitLvPulldown.ClearItems();
        for (int index = 0; index < multiPlayLimitUnitLv.Length; ++index)
        {
          this.UnitLvPulldown.AddItem((int) multiPlayLimitUnitLv[index] != 0 ? multiPlayLimitUnitLv[index].ToString() + LocalizedText.Get("sys.MULTI_JOINLIMIT_OVER") : LocalizedText.Get("sys.MULTI_JOINLIMIT_NONE"), index);
          if ((int) multiPlayLimitUnitLv[index] == GlobalVars.MultiPlayJoinUnitLv)
            num = index;
        }
        this.mNowSelIdx = num;
        this.UnitLvPulldown.Selection = num;
        this.UnitLvPulldown.OnSelectionChangeDelegate = new ScrollablePulldownBase.SelectItemEvent(this.OnUnitLvChange);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BattleSpeedPulldown, (UnityEngine.Object) null))
      {
        this.BattleSpeedPulldown.ClearItems();
        this.BattleSpeedPulldown.Selection = -1;
        float[] lists = BattleSpeedController.CreateBattleSpeedList();
        List<ExpansionPurchaseData> expansionPurchaseDataList = new List<ExpansionPurchaseData>((IEnumerable<ExpansionPurchaseData>) MonoSingleton<GameManager>.Instance.Player.GetEnableExpansionDatas(ExpansionPurchaseParam.eExpansionType.TripleSpeed));
        int num1 = 0;
        float num2 = PlayerPrefsUtility.GetFloat(PlayerPrefsUtility.MULTI_BATTLE_SPEED, 1f);
        if (lists != null)
        {
          for (int i = 0; i < lists.Length; ++i)
          {
            string label = LocalizedText.Get("sys.PARTYEDITOR_BATTLESET_SELECT_DEFAULT_SPEED");
            if ((double) lists[i] > 1.0)
              label = LocalizedText.Get("sys.PARTYEDITOR_BATTLESET_SELECT_SPEED_SUFFIX", (object) (int) lists[i]);
            if ((double) lists[i] == (double) num2)
              num1 = i;
            bool is_lock = false;
            if ((double) lists[i] > 2.0)
            {
              is_lock = expansionPurchaseDataList == null || expansionPurchaseDataList.Count < 0 || expansionPurchaseDataList.FindIndex((Predicate<ExpansionPurchaseData>) (p => (double) p.param.Value == (double) lists[i])) == -1;
              if (is_lock && num1 == i)
                num1 = BattleSpeedController.SPEED_LIST.Length - 1;
            }
            this.BattleSpeedPulldown.AddItem(label, i, is_lock);
          }
          this.BattleSpeedPulldown.OnSelectionChangeDelegate = new ScrollablePulldownBase.SelectItemEvent(this.SetBattleSpeed);
          this.BattleSpeedPulldown.Selection = num1;
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ClearEnableButton, (UnityEngine.Object) null))
        this.ClearEnableButton.SetActive(!GlobalVars.MultiPlayClearOnly);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ClearDisableButton, (UnityEngine.Object) null))
        this.ClearDisableButton.SetActive(GlobalVars.MultiPlayClearOnly);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailFilter, (UnityEngine.Object) null))
        this.DetailFilter.SetActive(!GlobalVars.SelectedMultiPlayLimit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LimitEnableButton, (UnityEngine.Object) null))
        this.LimitEnableButton.SetActive(!GlobalVars.SelectedMultiPlayLimit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LimitDisableButton, (UnityEngine.Object) null))
        this.LimitDisableButton.SetActive(GlobalVars.SelectedMultiPlayLimit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitPulldownBG, (UnityEngine.Object) null))
        ((Behaviour) this.UnitPulldownBG).enabled = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BattleSpeedPulldownBG, (UnityEngine.Object) null))
        ((Behaviour) this.BattleSpeedPulldownBG).enabled = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LimitToggle, (UnityEngine.Object) null))
        this.LimitToggle.isOn = GlobalVars.SelectedMultiPlayLimit;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ClearToggle, (UnityEngine.Object) null))
        return;
      this.ClearToggle.isOn = GlobalVars.MultiPlayClearOnly;
    }

    public void OnEnablePass()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailFilter, (UnityEngine.Object) null))
        return;
      this.DetailFilter.SetActive(true);
    }

    public void OnDisablePass()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailFilter, (UnityEngine.Object) null))
        return;
      this.DetailFilter.SetActive(!GlobalVars.SelectedMultiPlayLimit);
    }

    public void OnEnableLimit()
    {
      GlobalVars.SelectedMultiPlayLimit = true;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailFilter, (UnityEngine.Object) null))
        return;
      this.DetailFilter.SetActive(false);
    }

    public void OnDisableLimit()
    {
      GlobalVars.SelectedMultiPlayLimit = false;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailFilter, (UnityEngine.Object) null))
        return;
      this.DetailFilter.SetActive(true);
    }

    public void OnEnableClear() => GlobalVars.MultiPlayClearOnly = true;

    public void OnDisableClear() => GlobalVars.MultiPlayClearOnly = false;

    private void OnUnitLvChange(int index)
    {
      OInt[] multiPlayLimitUnitLv = MonoSingleton<GameManager>.Instance.MasterParam.GetMultiPlayLimitUnitLv();
      if (multiPlayLimitUnitLv == null || index >= multiPlayLimitUnitLv.Length)
        return;
      if (this.mMaxLv < (int) multiPlayLimitUnitLv[index])
      {
        this.UnitLvPulldown.Selection = this.mNowSelIdx;
        this.UnitLvPulldown.PrevSelection = -1;
        UIUtility.SystemMessage(LocalizedText.Get("sys.MULTI_LIMIT_ERROR"), (UIUtility.DialogResultEvent) null);
      }
      else
      {
        this.mNowSelIdx = index;
        GlobalVars.MultiPlayJoinUnitLv = (int) multiPlayLimitUnitLv[index];
      }
    }

    public void OnPassToggle(bool isOn)
    {
      if (isOn)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailFilter, (UnityEngine.Object) null))
          this.DetailFilter.SetActive(true);
        FlowNode_Variable.Set("MultiPlayPasscode", "1");
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailFilter, (UnityEngine.Object) null))
          this.DetailFilter.SetActive(!GlobalVars.SelectedMultiPlayLimit);
        FlowNode_Variable.Set("MultiPlayPasscode", "0");
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filter, (UnityEngine.Object) null))
        return;
      this.Filter.SetActive(isOn);
    }

    public void OnLimitToggle(bool isOn)
    {
      GlobalVars.SelectedMultiPlayLimit = isOn;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailFilter, (UnityEngine.Object) null))
        return;
      this.DetailFilter.SetActive(!isOn);
    }

    public void OnClearToggle(bool isOn) => GlobalVars.MultiPlayClearOnly = isOn;

    private void SetBattleSpeed(int index)
    {
      float num = BattleSpeedController.SaveLimitSpeed(BattleSpeedController.CreateBattleSpeedList()[index]);
      PlayerPrefsUtility.SetFloat(PlayerPrefsUtility.MULTI_BATTLE_SPEED, num);
    }
  }
}
