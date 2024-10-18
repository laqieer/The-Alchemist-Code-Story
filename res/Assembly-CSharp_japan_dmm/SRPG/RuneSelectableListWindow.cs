// Decompiled with JetBrains decompiler
// Type: SRPG.RuneSelectableListWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "フィルタ１選択", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "フィルタ２選択", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "フィルタ３選択", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(13, "フィルタ４選択", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(14, "フィルタ５選択", FlowNode.PinTypes.Input, 14)]
  [FlowNode.Pin(15, "フィルタ６選択", FlowNode.PinTypes.Input, 15)]
  [FlowNode.Pin(20, "フィルタ「すべて」選択", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(30, "フィルタウィンドウ OK", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(31, "ソートウィンドウ OK", FlowNode.PinTypes.Input, 31)]
  [FlowNode.Pin(32, "装備中を除くボタン", FlowNode.PinTypes.Input, 31)]
  [FlowNode.Pin(100, "ルーンを選択", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(1000, "自身を閉じる", FlowNode.PinTypes.Output, 1000)]
  public class RuneSelectableListWindow : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_RUNE_SELECT_FILTER_1 = 10;
    private const int INPUT_RUNE_SELECT_FILTER_2 = 11;
    private const int INPUT_RUNE_SELECT_FILTER_3 = 12;
    private const int INPUT_RUNE_SELECT_FILTER_4 = 13;
    private const int INPUT_RUNE_SELECT_FILTER_5 = 14;
    private const int INPUT_RUNE_SELECT_FILTER_6 = 15;
    private const int INPUT_RUNE_SELECT_FILTER_ALL = 20;
    private const int INPUT_RUNE_SELECT_FILTER_WND_APPLY = 30;
    private const int INPUT_RUNE_SELECT_SORT_WND_APPLY = 31;
    private const int INPUT_RUNE_SELECT_EQUIP_HIDE_APPLY = 32;
    private const int INPUT_RUNE_SELECTED = 100;
    private const int OUTPUT_CLOSE_WINDOW = 1000;
    [SerializeField]
    private RuneScrollList mRuneScrollList;
    [SerializeField]
    private RuneSlotNumberFilter mRuneSlotNumberFilter;
    [SerializeField]
    private ImageArray mRuneFilterMode;
    [SerializeField]
    private Text mRuneSortMode;
    [SerializeField]
    private GameObject mRuneEquipHideObj;
    [SerializeField]
    private Toggle mRuneEquipHideToggle;
    [SerializeField]
    private Text mRuneEquipHideText;
    private RuneManager mRuneManager;
    private RuneSlotIndex mSelectedRuneSlotIndex = (RuneSlotIndex) byte.MaxValue;
    private eRuneSortType mSortType;
    private static RuneSelectableListWindow mInstance;

    private event RuneSelectableListWindow.OnSelectedEvent mOnSelectedEvent = _param0 => { };

    public static RuneSelectableListWindow Instance => RuneSelectableListWindow.mInstance;

    public RuneSlotIndex SelectedRuneSlotIndex => this.mSelectedRuneSlotIndex;

    public void SetOnSelectCallback(
      RuneSelectableListWindow.OnSelectedEvent selected_callback)
    {
      this.mOnSelectedEvent = selected_callback;
    }

    public void Awake()
    {
      if (!Object.op_Implicit((Object) this.mRuneScrollList))
        DebugUtility.LogError("mRuneScrollList が設定されていません");
      RuneSelectableListWindow.mInstance = this;
    }

    private void OnDestroy()
    {
      RuneSelectableListWindow.mInstance = (RuneSelectableListWindow) null;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
        case 11:
        case 12:
        case 13:
        case 14:
        case 15:
          RuneSlotIndex runeSlotIndex = (RuneSlotIndex) (byte) (pinID - 10);
          this.SelectedSlot(runeSlotIndex);
          if (Object.op_Implicit((Object) this.mRuneManager))
            this.mRuneManager.SelectedFilterSlot(runeSlotIndex);
          if (!Object.op_Implicit((Object) RuneDisassemblyWindow.Instance))
            break;
          RuneDisassemblyWindow.Instance.Refresh();
          break;
        case 20:
          this.SelectedSlot((RuneSlotIndex) byte.MaxValue);
          if (Object.op_Implicit((Object) this.mRuneManager))
            this.mRuneManager.SelectedFilterSlot((RuneSlotIndex) byte.MaxValue);
          if (!Object.op_Implicit((Object) RuneDisassemblyWindow.Instance))
            break;
          RuneDisassemblyWindow.Instance.Refresh();
          break;
        default:
          switch (pinID - 30)
          {
            case 0:
              this.mRuneScrollList.Filter(this.mSelectedRuneSlotIndex);
              this.mRuneScrollList.EquipHide(false);
              this.mRuneScrollList.Sort();
              this.mRuneScrollList.CreateContens();
              this.RefreshFilterButton();
              if (!Object.op_Implicit((Object) RuneDisassemblyWindow.Instance))
                return;
              RuneDisassemblyWindow.Instance.Refresh();
              return;
            case 1:
              this.mRuneScrollList.Sort();
              this.mRuneScrollList.CreateContens();
              this.RefreshSortButton();
              return;
            case 2:
              this.mRuneScrollList.ReverseEquipHideFlag();
              this.mRuneScrollList.Filter(this.mSelectedRuneSlotIndex);
              this.mRuneScrollList.EquipHide(true, true);
              this.mRuneScrollList.Sort();
              this.mRuneScrollList.CreateContens();
              this.RefreshEquipHideButton(true);
              if (!Object.op_Implicit((Object) RuneDisassemblyWindow.Instance))
                return;
              RuneDisassemblyWindow.Instance.Refresh();
              return;
            default:
              if (pinID != 100)
                return;
              this.SelectRune();
              return;
          }
      }
    }

    public void Initialize(
      RuneManager manager,
      List<BindRuneData> rune_datas,
      bool is_equip_hide = false,
      bool _is_use_equip_button = false)
    {
      this.mRuneManager = manager;
      if (Object.op_Implicit((Object) this.mRuneSlotNumberFilter))
        this.mRuneSlotNumberFilter.Initialize();
      this.mRuneScrollList.Init(rune_datas, this.mSelectedRuneSlotIndex, is_equip_hide, _is_use_equip_button: _is_use_equip_button);
      this.SelectedSlot(this.mSelectedRuneSlotIndex);
      this.RefreshSortButton();
      this.RefreshFilterButton();
      this.RefreshEquipHideButton(_is_use_equip_button);
    }

    public void SetLockDisable(bool is_lock_disable)
    {
    }

    public void Refresh(bool _is_equip = false)
    {
      if (_is_equip && this.mRuneScrollList.IsEquipHide)
      {
        this.mRuneScrollList.Filter(this.mSelectedRuneSlotIndex);
        this.mRuneScrollList.EquipHide(false);
        this.mRuneScrollList.Sort();
        this.mRuneScrollList.CreateContens();
      }
      else
        this.mRuneScrollList.Refresh();
    }

    private void RefreshSortButton()
    {
      if (Object.op_Equality((Object) this.mRuneSortMode, (Object) null))
        return;
      SortUtility.SortPrefsData firstOn = SortUtility.Load_RuneSortFromCache().FindFirstOn();
      if (firstOn == null)
        return;
      SortRuneConditionParam runeConditionParam = MonoSingleton<GameManager>.Instance.MasterParam.FindSortRuneConditionParam(firstOn);
      if (runeConditionParam == null)
        return;
      this.mRuneSortMode.text = runeConditionParam.name;
    }

    private void RefreshFilterButton()
    {
      if (Object.op_Equality((Object) this.mRuneFilterMode, (Object) null))
        return;
      this.mRuneFilterMode.ImageIndex = !FilterRunePrefs.GetFilterPrefs().IsDisableFilterAll() ? 1 : 0;
    }

    private void RefreshEquipHideButton(bool _is_use_button)
    {
      if (!Object.op_Inequality((Object) this.mRuneEquipHideObj, (Object) null) || !Object.op_Inequality((Object) this.mRuneEquipHideToggle, (Object) null) || !Object.op_Inequality((Object) this.mRuneEquipHideText, (Object) null))
        return;
      if (_is_use_button)
      {
        this.mRuneEquipHideObj.SetActive(true);
        this.mRuneEquipHideToggle.isOn = this.mRuneScrollList.HideButtonFlag;
        if (this.mRuneScrollList.HideButtonFlag)
          this.mRuneEquipHideText.text = LocalizedText.Get("sys.RUNE_STORAGE_EQUIPENT_BTN_TEXT_ON");
        else
          this.mRuneEquipHideText.text = LocalizedText.Get("sys.RUNE_STORAGE_EQUIPENT_BTN_TEXT_OFF");
      }
      else
        this.mRuneEquipHideObj.SetActive(false);
    }

    public void SelectedSlot() => this.SelectedSlot(this.mSelectedRuneSlotIndex);

    public void SelectedSlot(RuneSlotIndex slot)
    {
      this.mSelectedRuneSlotIndex = slot;
      if (Object.op_Implicit((Object) this.mRuneSlotNumberFilter))
        this.mRuneSlotNumberFilter.SelectedSlot(slot);
      this.mRuneScrollList.Filter(this.mSelectedRuneSlotIndex);
      this.mRuneScrollList.EquipHide(false);
      this.mRuneScrollList.Sort();
      this.mRuneScrollList.CreateContens();
      ContentScroller scroller = this.mRuneScrollList.GetScroller();
      if (!Object.op_Implicit((Object) scroller))
        return;
      scroller.StopMovement();
    }

    public void CloseSelf() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);

    private void SelectRune()
    {
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      RuneIcon component = currentValue.GetComponent<RuneIcon>("_self");
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      this.mOnSelectedEvent(component.Rune);
      this.mRuneScrollList.Refresh();
    }

    public List<BindRuneData> GetRunes()
    {
      return Object.op_Inequality((Object) this.mRuneScrollList, (Object) null) ? this.mRuneScrollList.RuneDatas : new List<BindRuneData>();
    }

    public enum SelectableType
    {
      Equip,
      Disassembly,
    }

    public delegate void OnSelectedEvent(BindRuneData rune);
  }
}
