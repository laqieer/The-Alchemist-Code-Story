// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDisassemblyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "分解リストすべてクリア", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "キャンセル", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "分解実行", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(100, "分解通信開始", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "分解通信完了", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(200, "ルーンを選択", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(201, "ルーンを外す", FlowNode.PinTypes.Input, 201)]
  [FlowNode.Pin(210, "全て選択", FlowNode.PinTypes.Input, 210)]
  [FlowNode.Pin(1000, "自身を閉じる", FlowNode.PinTypes.Output, 1000)]
  public class RuneDisassemblyWindow : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_RUNE_CLEAR = 10;
    private const int INPUT_RUNE_CANCEL = 11;
    private const int INPUT_RUNE_EXEC = 12;
    private const int OUTPUT_NETWORK_START_EXEC = 100;
    private const int INPUT_NETWORK_FINISH_EXEC = 101;
    private const int INPUT_RUNE_SELECTED = 200;
    private const int INPUT_RUNE_UNSELECTED = 201;
    private const int INPUT_RUNE_SELECT_ALL = 210;
    private const int OUTPUT_CLOSE_WINDOW = 1000;
    [SerializeField]
    private GameObject mRuneSelectedInfo;
    [SerializeField]
    private GameObject mRuneUnselectedPanel;
    [SerializeField]
    private RuneDrawInfo mRuneDrawInfo;
    [SerializeField]
    private RuneIcon mRuneIcon;
    [SerializeField]
    private RuneDrawBaseState mRuneDrawBaseState;
    [SerializeField]
    private RuneDrawEnhanceLevel mRuneDrawEnhanceLevel;
    [SerializeField]
    private RuneDrawEvoState mRuneDrawEvoState;
    [SerializeField]
    private RuneDrawDisassemblyInfo mRuneDrawDisassemblyInfo;
    [SerializeField]
    private Button mExecButton;
    [SerializeField]
    private RuneSelectableListWindow mRuneSelectableListWindow;
    [SerializeField]
    private RuneScrollList mRuneDisassemblyScrollList;
    [SerializeField]
    private int mWarningRarity = 5;
    [SerializeField]
    private int mMaxDisassemblySelected = 99;
    [SerializeField]
    private Button mResetButton;
    [SerializeField]
    private Button mClearButton;
    [SerializeField]
    private Button mSelectAllButton;
    private BindRuneData mSelectedBindRuneData;
    private RuneManager mRuneManager;
    private List<BindRuneData> mRuneDatas = new List<BindRuneData>();
    private List<BindRuneData> mPlayerRuneList = new List<BindRuneData>();
    private static RuneDisassemblyWindow mInstance;
    private Vector2 mVec = new Vector2();

    public static RuneDisassemblyWindow Instance => RuneDisassemblyWindow.mInstance;

    public void Awake()
    {
      RuneDisassemblyWindow.mInstance = this;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneSelectableListWindow))
        DebugUtility.LogError("mRuneSelectableListWindow が設定されていません");
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDisassemblyScrollList))
        return;
      DebugUtility.LogError("mRuneDisassemblyScrollList が設定されていません");
    }

    private void OnDestroy() => RuneDisassemblyWindow.mInstance = (RuneDisassemblyWindow) null;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.ClearAllSelectedRune();
          break;
        case 11:
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneManager))
          {
            this.mRuneManager.CloseDisassembly();
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
          break;
        case 12:
          this.ConfirmDisassembly();
          break;
        case 101:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRuneManager, (UnityEngine.Object) null))
          {
            this.mRuneManager.RefreshRuneDisassemblyFinished(FlowNode_ReqRuneDisassembly.GetResponse());
            FlowNode_ReqRuneDisassembly.Clear();
          }
          else
          {
            List<BindRuneData> targetRunes = FlowNode_ReqRuneDisassembly.GetTargetRunes();
            ReqRuneDisassembly.Response response = FlowNode_ReqRuneDisassembly.GetResponse();
            RuneDisassemblyEffectWindow popupWindow = RuneManager.CreatePopupWindow<RuneDisassemblyEffectWindow>("UI/Rune/RuneDisassemblyEffect");
            if (response == null || targetRunes == null || UnityEngine.Object.op_Equality((UnityEngine.Object) popupWindow, (UnityEngine.Object) null))
            {
              DebugUtility.LogError("RuneDisasseblyWindowでエラー response=" + (object) (response != null) + ", runes=" + (object) (targetRunes != null) + "window=" + (object) UnityEngine.Object.op_Inequality((UnityEngine.Object) popupWindow, (UnityEngine.Object) null));
              break;
            }
            popupWindow.Setup((RuneManager) null, response);
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
          break;
        case 200:
          this.SelecteedRuneFromFlowNode();
          break;
        case 201:
          this.UnselectRune();
          break;
        case 210:
          this.SelectAll();
          break;
      }
    }

    public void Setup(RuneManager manager, List<BindRuneData> rune_list)
    {
      this.mRuneManager = manager;
      this.SetupBindRuneList(rune_list);
      this.mRuneSelectableListWindow.Initialize(manager, this.mPlayerRuneList, true);
      this.mRuneSelectableListWindow.SetOnSelectCallback(new RuneSelectableListWindow.OnSelectedEvent(this.OnSelectedRune));
      this.mRuneSelectableListWindow.SetLockDisable(true);
      this.mRuneDisassemblyScrollList.Init(this.mRuneDatas, (RuneSlotIndex) byte.MaxValue);
      this.Refresh();
    }

    public void SetupBindRuneList(List<BindRuneData> rune_list)
    {
      this.mPlayerRuneList.Clear();
      for (int index = 0; index < rune_list.Count; ++index)
      {
        if (!rune_list[index].IsFavorite)
          this.mPlayerRuneList.Add(new BindRuneData(rune_list[index].iid));
      }
    }

    public void Refresh()
    {
      this.mRuneSelectableListWindow.Refresh();
      this.mRuneDisassemblyScrollList.Refresh();
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneSelectedInfo))
        this.mRuneSelectedInfo.SetActive(this.mSelectedBindRuneData != null);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneUnselectedPanel))
        this.mRuneUnselectedPanel.SetActive(this.mSelectedBindRuneData == null);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawDisassemblyInfo))
        this.mRuneDrawDisassemblyInfo.SetParam(this.TotalZeny(), this.mRuneDatas.Count, this.mMaxDisassemblySelected, this.IsWarning(), this.DisassemblyPredictedValue());
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mExecButton))
        ((Selectable) this.mExecButton).interactable = 1 <= this.mRuneDatas.Count;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mResetButton))
        ((Selectable) this.mResetButton).interactable = 1 <= this.mRuneDatas.Count;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mClearButton))
        ((Selectable) this.mClearButton).interactable = 1 <= this.mRuneDatas.Count;
      List<BindRuneData> runes = this.mRuneSelectableListWindow.GetRunes();
      bool flag1 = this.mMaxDisassemblySelected <= this.mRuneDatas.Count;
      bool flag2 = runes.FindIndex((Predicate<BindRuneData>) (rune => rune != null && !rune.is_check)) >= 0;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSelectAllButton))
        ((Selectable) this.mSelectAllButton).interactable = !flag1 && flag2;
      this.RefreshTarget(this.mSelectedBindRuneData);
    }

    private void RefreshTarget(BindRuneData target_rune)
    {
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawInfo))
        this.mRuneDrawInfo.SetDrawParam(target_rune);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneIcon))
        this.mRuneIcon.Setup(target_rune);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawBaseState))
        this.mRuneDrawBaseState.SetDrawParam(target_rune);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawEvoState))
        return;
      this.mRuneDrawEvoState.SetDrawParam(target_rune);
    }

    private void SelecteedRuneFromFlowNode()
    {
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      RuneIcon component = currentValue.GetComponent<RuneIcon>("_self");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      this.SelectedDetailRune(component.Rune);
    }

    public void SelectedDetailRune(BindRuneData rune)
    {
      if (this.mSelectedBindRuneData != null)
        this.mSelectedBindRuneData.is_selected = false;
      this.RefreshTarget(this.mSelectedBindRuneData);
      this.mSelectedBindRuneData = rune;
      if (this.mSelectedBindRuneData != null)
        this.mSelectedBindRuneData.is_selected = true;
      this.Refresh();
    }

    public void UnselectRune()
    {
      if (this.mSelectedBindRuneData == null)
        return;
      int index = this.mRuneDatas.IndexOf(this.mSelectedBindRuneData);
      this.OnSelectedRune(this.mSelectedBindRuneData);
      if (this.mRuneDatas.Count > 0 && index >= 0)
        this.SelectedDetailRune(this.mRuneDatas.Count <= index ? this.mRuneDatas[this.mRuneDatas.Count - 1] : this.mRuneDatas[index]);
      this.mRuneSelectableListWindow.Refresh();
    }

    private void SelectAll()
    {
      List<BindRuneData> runes = this.mRuneSelectableListWindow.GetRunes();
      if (runes.Count <= 0)
        return;
      for (int index = 0; index < runes.Count && this.mRuneDatas.Count < this.mMaxDisassemblySelected; ++index)
      {
        if (runes[index] != null && !runes[index].is_check && !runes[index].IsFavorite && runes[index].GetOwner() == null)
        {
          runes[index].is_check = true;
          this.mRuneDatas.Add(runes[index]);
        }
      }
      BindRuneData rune = (BindRuneData) null;
      for (int index = 0; index < this.mRuneDatas.Count; ++index)
      {
        if (this.mRuneDatas[index] != null)
        {
          rune = this.mRuneDatas[index];
          break;
        }
      }
      this.mRuneDisassemblyScrollList.Init(this.mRuneDatas, (RuneSlotIndex) byte.MaxValue, is_not_sort: true);
      this.SelectedDetailRune(rune);
      this.Refresh();
    }

    public void OnSelectedRune(BindRuneData rune)
    {
      if (rune == null || rune.Rune == null)
        return;
      if (this.mRuneDatas.Contains(rune))
      {
        this.ClearSelectedRune(rune);
      }
      else
      {
        this.AddSelectedRune(rune);
        ContentScroller scroller = this.mRuneDisassemblyScrollList.GetScroller();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) scroller, (UnityEngine.Object) null))
          return;
        scroller.ResetVerticalPosition();
      }
    }

    public void AddSelectedRune(BindRuneData rune)
    {
      if (rune == null || this.mRuneDatas.Count >= this.mMaxDisassemblySelected)
        return;
      rune.is_check = true;
      this.mRuneDatas.Add(rune);
      this.mRuneDisassemblyScrollList.Init(this.mRuneDatas, (RuneSlotIndex) byte.MaxValue, is_not_sort: true);
      this.SelectedDetailRune(rune);
      this.Refresh();
    }

    private void ScrollKeepInit()
    {
      ContentScroller scroller = this.mRuneDisassemblyScrollList.GetScroller();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) scroller, (UnityEngine.Object) null))
        return;
      this.mVec = scroller.content.anchoredPosition;
    }

    private void ScrollKeepApply()
    {
      ContentScroller scroller = this.mRuneDisassemblyScrollList.GetScroller();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) scroller, (UnityEngine.Object) null))
        return;
      Vector2 vector2 = new Vector2();
      ((Vector2) ref vector2).Set(scroller.content.anchoredPosition.x, this.mVec.y);
      scroller.content.anchoredPosition = vector2;
    }

    public void ClearSelectedRune(BindRuneData rune)
    {
      if (rune == null)
        return;
      this.ScrollKeepInit();
      rune.is_check = false;
      if (this.mRuneDatas.Contains(rune))
        this.mRuneDatas.Remove(rune);
      this.mRuneDisassemblyScrollList.Init(this.mRuneDatas, (RuneSlotIndex) byte.MaxValue, is_not_sort: true);
      this.SelectedDetailRune((BindRuneData) null);
      this.ScrollKeepApply();
      this.Refresh();
    }

    public void ClearAllSelectedRune()
    {
      for (int index = 0; index < this.mRuneDatas.Count; ++index)
        this.mRuneDatas[index].ResetOptionParam();
      this.mRuneDatas.Clear();
      this.mRuneDisassemblyScrollList.Init(this.mRuneDatas, (RuneSlotIndex) byte.MaxValue);
      this.SelectedDetailRune((BindRuneData) null);
      this.Refresh();
    }

    private int TotalZeny()
    {
      int num = 0;
      for (int index = 0; index < this.mRuneDatas.Count; ++index)
      {
        RuneData rune = this.mRuneDatas[index].Rune;
        if (rune == null)
          DebugUtility.LogError("TotalZenyで計算ミス発生");
        else
          num += rune.DisassemblyZeny;
      }
      return num;
    }

    private bool IsWarning()
    {
      for (int index = 0; index < this.mRuneDatas.Count; ++index)
      {
        RuneData rune = this.mRuneDatas[index].Rune;
        if (rune == null)
          DebugUtility.LogError("IsWarningで計算ミス発生");
        else if (rune.Item.rare > this.mWarningRarity)
          return true;
      }
      return false;
    }

    private List<RuneDisassembly.Materials> DisassemblyPredictedValue()
    {
      List<RuneDisassembly.Materials> materialsList = new List<RuneDisassembly.Materials>();
      if (this.mRuneDatas == null || this.mRuneDatas.Count <= 0)
        return materialsList;
      Dictionary<string, RuneDisassembly.Materials> dict;
      RuneDisassembly.ResultCalc(this.mRuneDatas, ReqRuneDisassembly.Response.Result.success, out dict);
      if (dict == null || dict.Count <= 0)
        return materialsList;
      foreach (KeyValuePair<string, RuneDisassembly.Materials> keyValuePair in dict)
        materialsList.Add(keyValuePair.Value);
      return materialsList;
    }

    public void CloseSelf() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);

    public void ConfirmDisassembly()
    {
      UIUtility.ConfirmBox(LocalizedText.Get("sys.RUNE_DISASSEMBLY_MESSAGE"), (UIUtility.DialogResultEvent) (yes_button =>
      {
        FlowNode_ReqRuneDisassembly.SetTarget(this.mRuneDatas);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }), (UIUtility.DialogResultEvent) null);
    }
  }
}
