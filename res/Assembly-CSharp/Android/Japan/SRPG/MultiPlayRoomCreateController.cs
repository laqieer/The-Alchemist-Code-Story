// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayRoomCreateController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
      if ((UnityEngine.Object) this.PassToggle != (UnityEngine.Object) null)
        this.PassToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnPassToggle));
      if ((UnityEngine.Object) this.LimitToggle != (UnityEngine.Object) null)
        this.LimitToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnLimitToggle));
      if ((UnityEngine.Object) this.ClearToggle != (UnityEngine.Object) null)
        this.ClearToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnClearToggle));
      this.Refresh();
    }

    private void Refresh()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      string empty = string.Empty;
      OInt[] multiPlayLimitUnitLv = instance.MasterParam.GetMultiPlayLimitUnitLv();
      if ((UnityEngine.Object) this.UnitLvPulldown != (UnityEngine.Object) null && multiPlayLimitUnitLv != null)
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
      if ((UnityEngine.Object) this.ClearEnableButton != (UnityEngine.Object) null)
        this.ClearEnableButton.SetActive(!GlobalVars.MultiPlayClearOnly);
      if ((UnityEngine.Object) this.ClearDisableButton != (UnityEngine.Object) null)
        this.ClearDisableButton.SetActive(GlobalVars.MultiPlayClearOnly);
      if ((UnityEngine.Object) this.DetailFilter != (UnityEngine.Object) null)
        this.DetailFilter.SetActive(!GlobalVars.SelectedMultiPlayLimit);
      if ((UnityEngine.Object) this.LimitEnableButton != (UnityEngine.Object) null)
        this.LimitEnableButton.SetActive(!GlobalVars.SelectedMultiPlayLimit);
      if ((UnityEngine.Object) this.LimitDisableButton != (UnityEngine.Object) null)
        this.LimitDisableButton.SetActive(GlobalVars.SelectedMultiPlayLimit);
      if ((UnityEngine.Object) this.UnitPulldownBG != (UnityEngine.Object) null)
        this.UnitPulldownBG.enabled = true;
      if ((UnityEngine.Object) this.LimitToggle != (UnityEngine.Object) null)
        this.LimitToggle.isOn = GlobalVars.SelectedMultiPlayLimit;
      if (!((UnityEngine.Object) this.ClearToggle != (UnityEngine.Object) null))
        return;
      this.ClearToggle.isOn = GlobalVars.MultiPlayClearOnly;
    }

    public void OnEnablePass()
    {
      if (!((UnityEngine.Object) this.DetailFilter != (UnityEngine.Object) null))
        return;
      this.DetailFilter.SetActive(true);
    }

    public void OnDisablePass()
    {
      if (!((UnityEngine.Object) this.DetailFilter != (UnityEngine.Object) null))
        return;
      this.DetailFilter.SetActive(!GlobalVars.SelectedMultiPlayLimit);
    }

    public void OnEnableLimit()
    {
      GlobalVars.SelectedMultiPlayLimit = true;
      if (!((UnityEngine.Object) this.DetailFilter != (UnityEngine.Object) null))
        return;
      this.DetailFilter.SetActive(false);
    }

    public void OnDisableLimit()
    {
      GlobalVars.SelectedMultiPlayLimit = false;
      if (!((UnityEngine.Object) this.DetailFilter != (UnityEngine.Object) null))
        return;
      this.DetailFilter.SetActive(true);
    }

    public void OnEnableClear()
    {
      GlobalVars.MultiPlayClearOnly = true;
    }

    public void OnDisableClear()
    {
      GlobalVars.MultiPlayClearOnly = false;
    }

    private void OnUnitLvChange(int index)
    {
      OInt[] multiPlayLimitUnitLv = MonoSingleton<GameManager>.Instance.MasterParam.GetMultiPlayLimitUnitLv();
      if (multiPlayLimitUnitLv == null || index >= multiPlayLimitUnitLv.Length)
        return;
      if (this.mMaxLv < (int) multiPlayLimitUnitLv[index])
      {
        this.UnitLvPulldown.Selection = this.mNowSelIdx;
        this.UnitLvPulldown.PrevSelection = -1;
        UIUtility.SystemMessage(LocalizedText.Get("sys.MULTI_LIMIT_ERROR"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
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
        if ((UnityEngine.Object) this.DetailFilter != (UnityEngine.Object) null)
          this.DetailFilter.SetActive(true);
        FlowNode_Variable.Set("MultiPlayPasscode", "1");
      }
      else
      {
        if ((UnityEngine.Object) this.DetailFilter != (UnityEngine.Object) null)
          this.DetailFilter.SetActive(!GlobalVars.SelectedMultiPlayLimit);
        FlowNode_Variable.Set("MultiPlayPasscode", "0");
      }
      if (!((UnityEngine.Object) this.Filter != (UnityEngine.Object) null))
        return;
      this.Filter.SetActive(isOn);
    }

    public void OnLimitToggle(bool isOn)
    {
      GlobalVars.SelectedMultiPlayLimit = isOn;
      if (!((UnityEngine.Object) this.DetailFilter != (UnityEngine.Object) null))
        return;
      this.DetailFilter.SetActive(!isOn);
    }

    public void OnClearToggle(bool isOn)
    {
      GlobalVars.MultiPlayClearOnly = isOn;
    }
  }
}
