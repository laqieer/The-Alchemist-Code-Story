// Decompiled with JetBrains decompiler
// Type: SRPG.RuneScrollList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneScrollList : MonoBehaviour
  {
    [SerializeField]
    private Text mEmptyMessageText;
    [SerializeField]
    private ContentController mContentController;
    private List<BindRuneData> mRuneDatas = new List<BindRuneData>();
    private List<BindRuneData> mRuneDatasDefault = new List<BindRuneData>();
    private List<RuneIconParam> mIconParams = new List<RuneIconParam>();
    private bool mIsEquipHide;
    private bool mIsHideButtonFlag;
    private RuneSlotIndex mCurrentSlotFilter;

    public bool IsEquipHide => this.mIsEquipHide;

    public bool HideButtonFlag => this.mIsHideButtonFlag;

    public List<BindRuneData> RuneDatas => this.mRuneDatas;

    public void Init(
      List<BindRuneData> rune_datas,
      RuneSlotIndex slotFilter,
      bool is_equip_hide = false,
      bool is_not_sort = false,
      bool _is_use_equip_button = false)
    {
      this.mRuneDatasDefault.Clear();
      this.mRuneDatasDefault.AddRange((IEnumerable<BindRuneData>) rune_datas);
      this.mRuneDatas.Clear();
      this.mRuneDatas.AddRange((IEnumerable<BindRuneData>) rune_datas);
      this.mCurrentSlotFilter = slotFilter;
      if (!is_not_sort)
      {
        this.Filter(this.mCurrentSlotFilter);
        if (_is_use_equip_button)
          this.mIsHideButtonFlag = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.RUNE_EXCLUDE_EQUIPED) != 0;
        this.EquipHide(true, _is_use_equip_button, is_equip_hide);
        this.Sort();
      }
      this.CreateContens();
      this.Refresh();
    }

    public void CreateContens()
    {
      this.mContentController.Release();
      ContentSource source = new ContentSource();
      this.mIconParams.Clear();
      if (this.mRuneDatas != null)
      {
        for (int index = 0; index < this.mRuneDatas.Count; ++index)
        {
          BindRuneData mRuneData = this.mRuneDatas[index];
          RuneIconParam runeIconParam = new RuneIconParam();
          runeIconParam.mRune = mRuneData;
          runeIconParam.Initialize(source);
          this.mIconParams.Add(runeIconParam);
        }
      }
      source.SetTable((ContentSource.Param[]) this.mIconParams.ToArray());
      this.mContentController.Initialize(source, Vector2.zero);
    }

    public void Refresh()
    {
      bool flag = true;
      if (this.mIconParams != null)
      {
        for (int index = 0; index < this.mIconParams.Count; ++index)
        {
          flag = false;
          this.mIconParams[index].Refresh();
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEmptyMessageText, (UnityEngine.Object) null))
        return;
      ((Component) this.mEmptyMessageText).gameObject.SetActive(flag);
    }

    public void Sort()
    {
      SortUtility.SortRunePrefs sortRunePrefs = SortUtility.Load_RuneSortFromCache();
      SortUtility.SortPrefsData firstOn = sortRunePrefs.FindFirstOn();
      if (firstOn == null)
        return;
      SortRuneConditionParam runeConditionParam = MonoSingleton<GameManager>.Instance.MasterParam.FindSortRuneConditionParam(firstOn);
      if (runeConditionParam == null)
        return;
      SortUtility.SortRune(runeConditionParam.sort_type, sortRunePrefs.IsAscending, sortRunePrefs.IsBaseParamSort, sortRunePrefs.IsEvoParamSort, sortRunePrefs.IsSetParamSort, this.mRuneDatas);
    }

    public void Filter(RuneSlotIndex slotFilter)
    {
      this.mRuneDatas.Clear();
      this.mRuneDatas.AddRange((IEnumerable<BindRuneData>) this.mRuneDatasDefault);
      if ((byte) slotFilter != byte.MaxValue)
        this.mRuneDatas.RemoveAll((Predicate<BindRuneData>) (rune => (int) (byte) rune.Rune.RuneParam.slot_index != (int) (byte) slotFilter));
      FilterUtility.FilterRune(ref this.mRuneDatas, FilterRunePrefs.GetFilterPrefs());
    }

    public void EquipHide(bool _change_flag, bool _is_use_button_flag = false, bool _is_equip_hide = false)
    {
      List<BindRuneData> collection = this.EquipHideList(this.mRuneDatas, !_change_flag ? this.mIsEquipHide : (!_is_use_button_flag ? _is_equip_hide : this.mIsHideButtonFlag));
      this.mRuneDatas.Clear();
      this.mRuneDatas.AddRange((IEnumerable<BindRuneData>) collection);
    }

    private List<BindRuneData> EquipHideList(List<BindRuneData> _rune_datas, bool _is_equip_hide)
    {
      this.mIsEquipHide = _is_equip_hide;
      List<BindRuneData> bindRuneDataList = new List<BindRuneData>();
      for (int index = 0; index < _rune_datas.Count; ++index)
      {
        RuneData rune = _rune_datas[index].Rune;
        if (rune != null && (!this.mIsEquipHide || rune.GetOwner() == null))
          bindRuneDataList.Add(_rune_datas[index]);
      }
      return bindRuneDataList;
    }

    public void ReverseEquipHideFlag()
    {
      this.mIsHideButtonFlag = !this.mIsHideButtonFlag;
      PlayerPrefsUtility.SetInt(PlayerPrefsUtility.RUNE_EXCLUDE_EQUIPED, this.mIsHideButtonFlag ? 1 : 0);
    }

    public ContentScroller GetScroller()
    {
      return UnityEngine.Object.op_Equality((UnityEngine.Object) this.mContentController, (UnityEngine.Object) null) ? (ContentScroller) null : this.mContentController.scroller;
    }

    public enum FilterType
    {
      None,
      Disable,
      Hide,
    }
  }
}
