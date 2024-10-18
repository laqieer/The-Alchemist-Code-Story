// Decompiled with JetBrains decompiler
// Type: SRPG.RuneParamEnhEvoEffectWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(100, "ゲージアニメ開始", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(110, "覚醒ステータス強化通信完了", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(900, "閉じるボタン押下", FlowNode.PinTypes.Input, 900)]
  [FlowNode.Pin(910, "覚醒ステータス強化通信開始", FlowNode.PinTypes.Output, 910)]
  [FlowNode.Pin(920, "強化アニメ開始", FlowNode.PinTypes.Output, 920)]
  [FlowNode.Pin(1000, "自身を閉じる", FlowNode.PinTypes.Output, 1000)]
  public class RuneParamEnhEvoEffectWindow : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_START_GAUGE_ANIM = 100;
    private const int INPUT_ENHANCE_EVO_REQUEST_END = 110;
    private const int INPUT_CLOSE = 900;
    private const int OUTPUT_ENHANCE_EVO_REQUEST_START = 910;
    private const int OUTPUT_ENHANCE_EVO_ANIM_START = 920;
    private const int OUTPUT_CLOSE_WINDOW = 1000;
    [SerializeField]
    private RuneIcon mRuneIcon;
    [SerializeField]
    private RuneDrawEnhanceEffect mRuneDrawEnhanceEffect;
    [SerializeField]
    private RuneDrawEvoStateOneSetting mRuneDrawEvoStateOneSetting;
    [SerializeField]
    private RuneDrawCost mRuneDrawCostEnhanceEvoTemp;
    private BindRuneData mRuneDataBefore;
    private BindRuneData mRuneDataCurr;
    private RuneManager mRuneManager;
    private bool mIsEnhanceSuccess;
    private int mEvoSlot;
    private int mEvoIndex;
    private List<Button> mCreatedEvoEnhanceButtons = new List<Button>();
    private List<RuneDrawCost> mCreatedDrawCost = new List<RuneDrawCost>();
    private GameObject mConfirmEvoEnhancePopup;

    public void Awake()
    {
    }

    private void OnDestroy()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mConfirmEvoEnhancePopup, (UnityEngine.Object) null))
        return;
      Win_Btn_DecideCancel_FL_C component = this.mConfirmEvoEnhancePopup.GetComponent<Win_Btn_DecideCancel_FL_C>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.BeginClose();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 100:
          this.StartCoroutine(this.StartGaugeAnimation());
          break;
        case 110:
          this.Setup(this.mRuneManager, FlowNode_ReqRuneParamEnhEvo.BackupRune, FlowNode_ReqRuneParamEnhEvo.TargetRune);
          FlowNode_ReqRuneParamEnhEvo.Clear();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 920);
          break;
        case 900:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRuneManager, (UnityEngine.Object) null) && this.mRuneDataCurr != null && this.mRuneDataCurr.Rune != null)
            this.mRuneManager.SelectedResetStatus(this.mRuneDataCurr, true, this.mRuneDataCurr.Rune.GetEvoIndex(this.mEvoSlot));
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
          break;
      }
    }

    [DebuggerHidden]
    private IEnumerator StartGaugeAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RuneParamEnhEvoEffectWindow.\u003CStartGaugeAnimation\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public void Setup(
      RuneManager manager,
      BindRuneData before_rune_data,
      BindRuneData after_rune_data)
    {
      this.mRuneManager = manager;
      this.mRuneDataBefore = before_rune_data;
      this.mRuneDataCurr = after_rune_data;
      this.mIsEnhanceSuccess = FlowNode_ReqRuneParamEnhEvo.IsResultSuccess;
      this.mEvoSlot = FlowNode_ReqRuneParamEnhEvo.SelectedEvoSlot;
      this.mEvoIndex = FlowNode_ReqRuneParamEnhEvo.SelectedEvoIndex;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneIcon))
        this.mRuneIcon.Setup(this.mRuneDataCurr);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawEnhanceEffect))
        this.mRuneDrawEnhanceEffect.SetDrawParam(this.mIsEnhanceSuccess);
      this.SetGaugeStatus();
      this.CreateEnhanceEvoButtons();
      this.Refresh();
    }

    private void CreateEnhanceEvoButtons()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneDrawCostEnhanceEvoTemp, (UnityEngine.Object) null) || this.mRuneDataCurr == null || this.mCreatedEvoEnhanceButtons.Count > 0)
        return;
      GameUtility.SetGameObjectActive((Component) this.mRuneDrawCostEnhanceEvoTemp, false);
      for (int index = 0; index < this.mRuneDataCurr.ParamEnhEvoCost.Length; ++index)
      {
        RuneDrawCost runeDrawCost = UnityEngine.Object.Instantiate<RuneDrawCost>(this.mRuneDrawCostEnhanceEvoTemp, ((Component) this.mRuneDrawCostEnhanceEvoTemp).transform.parent, false);
        Button componentInChildren = ((Component) runeDrawCost).GetComponentInChildren<Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          RuneParamEnhEvoEffectWindow.\u003CCreateEnhanceEvoButtons\u003Ec__AnonStorey1 buttonsCAnonStorey1 = new RuneParamEnhEvoEffectWindow.\u003CCreateEnhanceEvoButtons\u003Ec__AnonStorey1();
          // ISSUE: reference to a compiler-generated field
          buttonsCAnonStorey1.\u0024this = this;
          ((Component) runeDrawCost).gameObject.SetActive(true);
          runeDrawCost.SetDrawParam(this.mRuneDataCurr.ParamEnhEvoCost[index]);
          this.mCreatedEvoEnhanceButtons.Add(componentInChildren);
          this.mCreatedDrawCost.Add(runeDrawCost);
          // ISSUE: reference to a compiler-generated field
          buttonsCAnonStorey1.rune_cost = this.mRuneDataCurr.ParamEnhEvoCost[index];
          // ISSUE: method pointer
          ((UnityEvent) componentInChildren.onClick).AddListener(new UnityAction((object) buttonsCAnonStorey1, __methodptr(\u003C\u003Em__0)));
        }
      }
    }

    private void OnClickEnhanceEvo(RuneCost cost)
    {
      if (this.mRuneDataCurr == null || cost == null)
        return;
      int count = 0;
      string txt = string.Empty;
      RuneResetStatusWindow.CreateCostText(cost, out count, out txt);
      txt = 0 >= count ? txt + LocalizedText.Get("sys.RUNE_ENHANCE_STATUS_EVO_CONFIRM_NOCOST") : txt + LocalizedText.Get("sys.RUNE_ENHANCE_STATUS_EVO_CONFIRM");
      this.mConfirmEvoEnhancePopup = UIUtility.ConfirmBox(txt, (UIUtility.DialogResultEvent) (yes_button =>
      {
        FlowNode_ReqRuneParamEnhEvo.SetTarget(this.mRuneDataCurr, Array.FindIndex<RuneCost>(this.mRuneDataCurr.Rune.ParamEnhEvoCost, (Predicate<RuneCost>) (c => c == cost)), this.mEvoIndex);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 910);
      }), (UIUtility.DialogResultEvent) null);
    }

    public void Refresh()
    {
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneIcon))
        this.mRuneIcon.Refresh();
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawEnhanceEffect))
        this.mRuneDrawEnhanceEffect.Refresh();
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mRuneDrawEvoStateOneSetting))
        this.mRuneDrawEvoStateOneSetting.Refresh();
      if (this.mRuneDataCurr == null)
        return;
      for (int index = 0; index < this.mCreatedDrawCost.Count; ++index)
        this.mCreatedDrawCost[index].Refresh();
      if (this.mRuneDataCurr.Rune.IsCanEnhanceEvoParam(this.mEvoIndex))
        return;
      for (int index = 0; index < this.mCreatedDrawCost.Count; ++index)
        GameUtility.SetGameObjectActive((Component) this.mCreatedDrawCost[index], false);
    }

    private void SetGaugeStatus()
    {
      if (this.mRuneDataCurr == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneDrawEvoStateOneSetting, (UnityEngine.Object) null))
        return;
      RuneData rune1 = this.mRuneDataCurr.Rune;
      if (rune1 == null)
        return;
      RuneData rune2 = this.mRuneDataBefore.Rune;
      if (rune2 == null)
        return;
      int index = this.mEvoSlot - 1;
      if (0 > index || index >= rune1.state.evo_state.Count)
        return;
      BaseStatus addStatus = (BaseStatus) null;
      BaseStatus scaleStatus = (BaseStatus) null;
      if (index < rune1.GetLengthFromEvoParam())
      {
        rune1.CreateBaseStatusFromEvoParam(index, ref addStatus, ref scaleStatus, true);
        if (addStatus == null)
          addStatus = new BaseStatus();
        if (scaleStatus == null)
          scaleStatus = new BaseStatus();
        float percentage = rune1.PowerPercentageFromEvoParam(index);
        float start_percentage = rune2.PowerPercentageFromEvoParam(index);
        this.mRuneDrawEvoStateOneSetting.SetStatus(addStatus, scaleStatus, percentage, true, start_percentage);
      }
      else
        this.mRuneDrawEvoStateOneSetting.SetStatus((BaseStatus) null, (BaseStatus) null, 0.0f);
    }
  }
}
