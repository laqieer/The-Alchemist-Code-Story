// Decompiled with JetBrains decompiler
// Type: SRPG.RuneResetStatusWindow
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
  [FlowNode.Pin(100, "基本ステータス変更通信開始", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "基本ステータス変更通信完了", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(110, "覚醒ステータス変更通信開始", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(111, "覚醒ステータス変更通信完了", FlowNode.PinTypes.Input, 111)]
  [FlowNode.Pin(117, "覚醒ステータス強化通信開始", FlowNode.PinTypes.Output, 117)]
  [FlowNode.Pin(118, "覚醒ステータス強化通信完了", FlowNode.PinTypes.Input, 118)]
  [FlowNode.Pin(120, "基本のゲージアニメーション再生", FlowNode.PinTypes.Input, 120)]
  [FlowNode.Pin(121, "覚醒のゲージアニメーション再生", FlowNode.PinTypes.Input, 121)]
  [FlowNode.Pin(200, "効果変更", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(201, "効果強化", FlowNode.PinTypes.Input, 201)]
  [FlowNode.Pin(900, "自身を閉じる", FlowNode.PinTypes.Input, 900)]
  [FlowNode.Pin(1000, "自身を閉じる", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1100, "ルーンリスト更新", FlowNode.PinTypes.Input, 1100)]
  public class RuneResetStatusWindow : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_SELECT_BASE = 10;
    private const int INPUT_SELECT_EVO = 11;
    private const int OUTPUT_START_RESET_PARAM_BASE = 100;
    private const int INPUT_FINISHED_RESET_BASE = 101;
    private const int OUTPUT_START_RESET_STATUS_EVO = 110;
    private const int INPUT_FINISHED_RESET_EVO = 111;
    private const int OUTPUT_START_PARAM_ENH_EVO = 117;
    private const int INPUT_FINISHED_PARAM_ENH_EVO = 118;
    private const int INPUT_START_GAUGE_ANIM_BASE = 120;
    private const int INPUT_START_GAUGE_ANIM_EVO = 121;
    private const int INPUT_RESET_BUTTON = 200;
    private const int INPUT_ENHANCE_BUTTON = 201;
    private const int INPUT_CLOSE_WINDOW = 900;
    private const int OUTPUT_CLOSE_WINDOW = 1000;
    private const int INPUT_REFRESH_RUNE_LIST = 1100;
    private const int ViewCountNum = 1;
    [SerializeField]
    private RuneDrawCost mRuneDrawCostTemp;
    [SerializeField]
    private RuneDrawCost mRuneDrawCostEnhanceTemp;
    [SerializeField]
    private RuneDrawInfo mRuneDrawInfo;
    [SerializeField]
    private RuneIcon mRuneIcon;
    [SerializeField]
    private RuneDrawBaseState mRuneDrawBaseStateView;
    [SerializeField]
    private RuneDrawBaseState mRuneDrawBaseState;
    [SerializeField]
    private RuneDrawEvoState mRuneDrawEvoState;
    [Space(5f)]
    [SerializeField]
    private GameObject mRuneDrawCostBaseParent;
    [SerializeField]
    private GameObject mRuneDrawCostEvoParent;
    [SerializeField]
    private GameObject mRuneDrawCostEvoParamEnhParent;
    [Space(5f)]
    [SerializeField]
    private GameObject mTargetNone;
    [SerializeField]
    private GameObject mTargetBase;
    [SerializeField]
    private GameObject mTargetEvo;
    [SerializeField]
    private GameObject mTargetEvoParamEnh;
    [Space(5f)]
    [SerializeField]
    private ImageArray mButtonReset;
    [SerializeField]
    private ImageArray mButtonEnhance;
    private RuneManager mRuneManager;
    private BindRuneData mRuneData;
    private List<RuneDrawCost> mRuneBaseCostList = new List<RuneDrawCost>();
    private List<RuneDrawCost> mRuneEvoCostList = new List<RuneDrawCost>();
    private List<RuneDrawCost> mRuneParamEnhEvoCostList = new List<RuneDrawCost>();
    private RuneResetStatusWindow.ButtonType mSelectedTarget;
    private int mSelectedEvoIndex;
    private RuneResetStatusWindow.ResetType mResetType;

    public void OnClickCostItem(
      RuneCost cost,
      int index,
      RuneResetStatusWindow.ButtonType type,
      RuneResetStatusWindow.ResetType reset)
    {
      switch (type)
      {
        case RuneResetStatusWindow.ButtonType.Base:
          if (reset == RuneResetStatusWindow.ResetType.Reset)
          {
            this.ConfirmResetParamBase(cost, index);
            break;
          }
          DebugUtility.LogError("基本ステータスの効果強化はありません");
          break;
        case RuneResetStatusWindow.ButtonType.Evo:
          if (reset == RuneResetStatusWindow.ResetType.Reset)
          {
            this.ConfirmResetStatusEvo(cost, index);
            break;
          }
          this.ConfirmParamEnhEvo(cost, index);
          break;
      }
    }

    public void Start()
    {
    }

    private void OnDestroy()
    {
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 118:
          if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneManager))
            break;
          this.mRuneManager.RefreshRuneParamEnhFinished();
          break;
        case 120:
          this.Refresh();
          if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawBaseState))
            break;
          this.mRuneDrawBaseState.StartGaugeAnim();
          break;
        case 121:
          this.Refresh();
          if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawEvoState))
            break;
          this.mRuneDrawEvoState.StartGaugeAnim(this.mSelectedEvoIndex);
          break;
        default:
          if (pinID != 200)
          {
            if (pinID != 201)
            {
              if (pinID != 101)
              {
                if (pinID != 111)
                {
                  if (pinID != 900)
                  {
                    if (pinID != 1100 || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRuneManager, (UnityEngine.Object) null))
                      break;
                    this.mRuneManager.RefreshSelectableList();
                    break;
                  }
                  if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneManager))
                    this.mRuneManager.RefreshEquipWindow();
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
                  break;
                }
                if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawEvoState))
                  this.mRuneDrawEvoState.SetDisableColor(false, this.mSelectedEvoIndex);
                this.Refresh();
                break;
              }
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawBaseState))
                this.mRuneDrawBaseState.SetDisableColor(false);
              this.Refresh();
              break;
            }
            if (this.mSelectedTarget == RuneResetStatusWindow.ButtonType.Base)
              this.mSelectedTarget = RuneResetStatusWindow.ButtonType.None;
            this.mResetType = RuneResetStatusWindow.ResetType.Enhance;
            this.Refresh();
            break;
          }
          this.mResetType = RuneResetStatusWindow.ResetType.Reset;
          this.Refresh();
          break;
      }
    }

    public void OnSelectBaseStatusButton()
    {
      this.mSelectedTarget = RuneResetStatusWindow.ButtonType.Base;
      this.Refresh();
    }

    public void OnSelectEvoStatusButton(int index)
    {
      this.mSelectedTarget = RuneResetStatusWindow.ButtonType.Evo;
      this.mSelectedEvoIndex = index;
      this.Refresh();
    }

    public void Setup(
      RuneManager manager,
      BindRuneData rune_data,
      bool _is_enhance_evo,
      int _evo_slot_num)
    {
      this.mRuneManager = manager;
      this.mRuneData = rune_data;
      RuneData rune = this.mRuneData.Rune;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawInfo))
        this.mRuneDrawInfo.SetDrawParam(this.mRuneData);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneIcon))
        this.mRuneIcon.Setup(this.mRuneData);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawBaseStateView))
        this.mRuneDrawBaseStateView.SetDrawParam(this.mRuneData);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawBaseState))
      {
        this.mRuneDrawBaseState.SetDrawParam(this.mRuneData);
        this.mRuneDrawBaseState.SetSelectedCallBack(new RuneDrawBaseState.OnSelectedEvent(this.OnSelectBaseStatusButton));
      }
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawEvoState))
      {
        this.mRuneDrawEvoState.SetDrawParam(this.mRuneData);
        this.mRuneDrawEvoState.SetSelectedCallBack(new RuneDrawEvoState.OnSelectedEvent(this.OnSelectEvoStatusButton));
      }
      if (rune.ResetParamBaseCost != null)
      {
        this.CreateButton(this.mRuneDrawCostTemp, this.mRuneBaseCostList, this.mRuneDrawCostBaseParent, rune.ResetParamBaseCost.Length);
        this.ButtonSetting(this.mRuneBaseCostList, rune.ResetParamBaseCost, RuneResetStatusWindow.ButtonType.Base, RuneResetStatusWindow.ResetType.Reset);
      }
      if (rune.ResetStatusEvoCost != null)
      {
        this.CreateButton(this.mRuneDrawCostTemp, this.mRuneEvoCostList, this.mRuneDrawCostEvoParent, rune.ResetStatusEvoCost.Length);
        this.ButtonSetting(this.mRuneEvoCostList, rune.ResetStatusEvoCost, RuneResetStatusWindow.ButtonType.Evo, RuneResetStatusWindow.ResetType.Reset);
      }
      if (rune.ParamEnhEvoCost != null)
      {
        this.CreateButton(this.mRuneDrawCostEnhanceTemp, this.mRuneParamEnhEvoCostList, this.mRuneDrawCostEvoParamEnhParent, rune.ParamEnhEvoCost.Length);
        this.ButtonSetting(this.mRuneParamEnhEvoCostList, rune.ParamEnhEvoCost, RuneResetStatusWindow.ButtonType.Evo, RuneResetStatusWindow.ResetType.Enhance);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRuneDrawCostTemp, (UnityEngine.Object) null))
        ((Component) this.mRuneDrawCostTemp).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRuneDrawCostEnhanceTemp, (UnityEngine.Object) null))
        ((Component) this.mRuneDrawCostEnhanceTemp).gameObject.SetActive(false);
      if (_is_enhance_evo && _evo_slot_num >= 0)
      {
        this.mResetType = RuneResetStatusWindow.ResetType.Enhance;
        this.mSelectedTarget = RuneResetStatusWindow.ButtonType.Evo;
        this.mSelectedEvoIndex = _evo_slot_num;
      }
      this.Refresh();
    }

    public void CreateButton(
      RuneDrawCost template,
      List<RuneDrawCost> draw_list,
      GameObject parent,
      int num)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) template, (UnityEngine.Object) null) || draw_list == null || UnityEngine.Object.op_Equality((UnityEngine.Object) parent, (UnityEngine.Object) null))
        return;
      draw_list.ForEach((Action<RuneDrawCost>) (obj => UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) obj).gameObject)));
      draw_list.Clear();
      for (int index = 0; index < num; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(((Component) template).gameObject);
        gameObject.transform.SetParent(parent.transform, false);
        gameObject.SetActive(true);
        RuneDrawCost component = gameObject.GetComponent<RuneDrawCost>();
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
          break;
        draw_list.Add(component);
      }
    }

    public void ButtonSetting(
      List<RuneDrawCost> draw_list,
      RuneCost[] cost_list,
      RuneResetStatusWindow.ButtonType btn_type,
      RuneResetStatusWindow.ResetType reset_type)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      RuneResetStatusWindow.\u003CButtonSetting\u003Ec__AnonStorey2 settingCAnonStorey2 = new RuneResetStatusWindow.\u003CButtonSetting\u003Ec__AnonStorey2();
      // ISSUE: reference to a compiler-generated field
      settingCAnonStorey2.btn_type = btn_type;
      // ISSUE: reference to a compiler-generated field
      settingCAnonStorey2.reset_type = reset_type;
      // ISSUE: reference to a compiler-generated field
      settingCAnonStorey2.\u0024this = this;
      if (draw_list == null || cost_list == null)
        return;
      for (int index = 0; index < draw_list.Count && index < cost_list.Length; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        RuneResetStatusWindow.\u003CButtonSetting\u003Ec__AnonStorey0 settingCAnonStorey0 = new RuneResetStatusWindow.\u003CButtonSetting\u003Ec__AnonStorey0();
        // ISSUE: reference to a compiler-generated field
        settingCAnonStorey0.cost = cost_list[index];
        // ISSUE: reference to a compiler-generated field
        if (settingCAnonStorey0.cost != null)
        {
          RuneDrawCost draw = draw_list[index];
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) draw, (UnityEngine.Object) null))
          {
            Button componentInChildren = ((Component) draw).GetComponentInChildren<Button>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
            {
              // ISSUE: object of a compiler-generated type is created
              // ISSUE: method pointer
              ((UnityEvent) componentInChildren.onClick).AddListener(new UnityAction((object) new RuneResetStatusWindow.\u003CButtonSetting\u003Ec__AnonStorey1()
              {
                \u003C\u003Ef__ref\u00242 = settingCAnonStorey2,
                \u003C\u003Ef__ref\u00240 = settingCAnonStorey0,
                number = index
              }, __methodptr(\u003C\u003Em__0)));
              // ISSUE: reference to a compiler-generated field
              ((Selectable) componentInChildren).interactable = settingCAnonStorey0.cost.IsPlayerAmountEnough();
            }
            // ISSUE: reference to a compiler-generated field
            draw.SetDrawParam(settingCAnonStorey0.cost, 1);
            draw.Refresh();
          }
        }
      }
    }

    public void SetButtonInteractable(
      List<RuneDrawCost> draw_list,
      RuneCost[] cost_list,
      bool interactable)
    {
      if (draw_list == null || cost_list == null)
        return;
      for (int index = 0; index < draw_list.Count && index < cost_list.Length; ++index)
      {
        RuneCost cost = cost_list[index];
        if (cost != null)
        {
          RuneDrawCost draw = draw_list[index];
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) draw, (UnityEngine.Object) null))
          {
            Button componentInChildren = ((Component) draw).GetComponentInChildren<Button>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
              ((Selectable) componentInChildren).interactable = cost.IsPlayerAmountEnough() && interactable;
          }
        }
      }
    }

    public void Refresh()
    {
      switch (this.mSelectedTarget)
      {
        case RuneResetStatusWindow.ButtonType.None:
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawBaseState))
            this.mRuneDrawBaseState.ShowFrame(false);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawEvoState))
            this.mRuneDrawEvoState.ShowFrame(false);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTargetNone))
            this.mTargetNone.SetActive(true);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTargetBase))
            this.mTargetBase.SetActive(false);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTargetEvo))
            this.mTargetEvo.SetActive(false);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTargetEvoParamEnh))
          {
            this.mTargetEvoParamEnh.SetActive(false);
            break;
          }
          break;
        case RuneResetStatusWindow.ButtonType.Base:
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawBaseState))
            this.mRuneDrawBaseState.ShowFrame(true);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawEvoState))
            this.mRuneDrawEvoState.ShowFrame(false);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTargetNone))
            this.mTargetNone.SetActive(this.mResetType == RuneResetStatusWindow.ResetType.Enhance);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTargetBase))
            this.mTargetBase.SetActive(this.mResetType == RuneResetStatusWindow.ResetType.Reset);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTargetEvo))
            this.mTargetEvo.SetActive(false);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTargetEvoParamEnh))
          {
            this.mTargetEvoParamEnh.SetActive(false);
            break;
          }
          break;
        case RuneResetStatusWindow.ButtonType.Evo:
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawBaseState))
            this.mRuneDrawBaseState.ShowFrame(false);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawEvoState))
            this.mRuneDrawEvoState.ShowFrame(true, this.mSelectedEvoIndex);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTargetNone))
            this.mTargetNone.SetActive(false);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTargetBase))
            this.mTargetBase.SetActive(false);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTargetEvo))
            this.mTargetEvo.SetActive(this.mResetType == RuneResetStatusWindow.ResetType.Reset);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTargetEvoParamEnh))
          {
            this.mTargetEvoParamEnh.SetActive(this.mResetType == RuneResetStatusWindow.ResetType.Enhance);
            break;
          }
          break;
      }
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawBaseState))
        this.mRuneDrawBaseState.Refresh();
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawEvoState))
        this.mRuneDrawEvoState.Refresh();
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawInfo))
        this.mRuneDrawInfo.Refresh();
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneIcon))
        this.mRuneIcon.Refresh();
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawBaseStateView))
        this.mRuneDrawBaseStateView.Refresh();
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawCostTemp))
        this.mRuneDrawCostTemp.Refresh();
      if (this.mRuneBaseCostList != null)
      {
        foreach (RuneDrawCost mRuneBaseCost in this.mRuneBaseCostList)
        {
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) mRuneBaseCost, (UnityEngine.Object) null))
            mRuneBaseCost.Refresh();
        }
      }
      if (this.mRuneEvoCostList != null)
      {
        foreach (RuneDrawCost mRuneEvoCost in this.mRuneEvoCostList)
        {
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) mRuneEvoCost, (UnityEngine.Object) null))
            mRuneEvoCost.Refresh();
        }
      }
      if (this.mRuneParamEnhEvoCostList != null && this.mRuneData != null)
      {
        RuneData rune = this.mRuneData.Rune;
        if (rune != null)
        {
          this.SetButtonInteractable(this.mRuneParamEnhEvoCostList, rune.ParamEnhEvoCost, rune.IsCanEnhanceEvoParam(this.mSelectedEvoIndex));
          foreach (RuneDrawCost runeParamEnhEvoCost in this.mRuneParamEnhEvoCostList)
          {
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) runeParamEnhEvoCost, (UnityEngine.Object) null))
              runeParamEnhEvoCost.Refresh();
          }
        }
      }
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawBaseState))
        this.mRuneDrawBaseState.SetButtonInteractable(this.mResetType == RuneResetStatusWindow.ResetType.Reset);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mButtonReset))
        this.mButtonReset.ImageIndex = this.mResetType != RuneResetStatusWindow.ResetType.Reset ? 0 : 1;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mButtonEnhance))
        return;
      this.mButtonEnhance.ImageIndex = this.mResetType != RuneResetStatusWindow.ResetType.Enhance ? 0 : 1;
    }

    public void ConfirmResetParamBase(RuneCost cost, int index)
    {
      int count;
      string txt;
      RuneResetStatusWindow.CreateCostText(cost, out count, out txt);
      UIUtility.ConfirmBox(0 >= count ? txt + LocalizedText.Get("sys.RUNE_RESET_BASE_CONFIRM_NOCOST") : txt + LocalizedText.Get("sys.RUNE_RESET_BASE_CONFIRM"), (UIUtility.DialogResultEvent) (yes_button =>
      {
        this.mRuneDrawBaseState.SetDisableColor(true);
        FlowNode_ReqRuneResetParamBase.SetTarget(this.mRuneData, index);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }), (UIUtility.DialogResultEvent) null);
    }

    public void ConfirmResetStatusEvo(RuneCost cost, int index)
    {
      int count;
      string txt;
      RuneResetStatusWindow.CreateCostText(cost, out count, out txt);
      UIUtility.ConfirmBox(0 >= count ? txt + LocalizedText.Get("sys.RUNE_RESET_EVO_CONFIRM_NOCOST") : txt + LocalizedText.Get("sys.RUNE_RESET_EVO_CONFIRM"), (UIUtility.DialogResultEvent) (yes_button =>
      {
        this.mRuneDrawEvoState.SetDisableColor(true, this.mSelectedEvoIndex);
        FlowNode_ReqRuneResetStatusEvo.SetTarget(this.mRuneData, index, this.mSelectedEvoIndex);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
      }), (UIUtility.DialogResultEvent) null);
    }

    public void ConfirmParamEnhEvo(RuneCost cost, int index)
    {
      int count;
      string txt;
      RuneResetStatusWindow.CreateCostText(cost, out count, out txt);
      UIUtility.ConfirmBox(0 >= count ? txt + LocalizedText.Get("sys.RUNE_ENHANCE_STATUS_EVO_CONFIRM_NOCOST") : txt + LocalizedText.Get("sys.RUNE_ENHANCE_STATUS_EVO_CONFIRM"), (UIUtility.DialogResultEvent) (yes_button =>
      {
        FlowNode_ReqRuneParamEnhEvo.SetTarget(this.mRuneData, index, this.mSelectedEvoIndex);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 117);
      }), (UIUtility.DialogResultEvent) null);
    }

    public static void CreateCostText(RuneCost cost, out int count, out string txt)
    {
      count = 0;
      txt = string.Empty;
      if (0 < cost.use_coin)
      {
        txt += string.Format(LocalizedText.Get("sys.RUNE_RESET_COST_COIN", (object) cost.use_coin));
        ++count;
      }
      if (cost.use_item != null && 0 < cost.use_num)
      {
        if (0 < count)
          txt += " ";
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(cost.use_item);
        if (itemParam != null)
        {
          txt += string.Format(LocalizedText.Get("sys.RUNE_RESET_COST_ITEM", (object) itemParam.name, (object) cost.use_num));
          ++count;
        }
      }
      if (0 >= cost.use_zeny)
        return;
      string formatedText = CurrencyBitmapText.CreateFormatedText(cost.use_zeny.ToString());
      if (0 < count)
        txt += " ";
      txt += string.Format(LocalizedText.Get("sys.RUNE_RESET_COST_ZENY", (object) formatedText));
      ++count;
    }

    public void CloseSelf() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);

    public enum ButtonType
    {
      None,
      Base,
      Evo,
    }

    public enum ResetType
    {
      Reset,
      Enhance,
    }
  }
}
