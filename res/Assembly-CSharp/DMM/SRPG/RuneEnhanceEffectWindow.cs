// Decompiled with JetBrains decompiler
// Type: SRPG.RuneEnhanceEffectWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "強化アニメーション開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "強化成功アニメーション", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "強化失敗アニメーション", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(10, "強化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "ゲージ使用", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "ボタン表示更新", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(100, "強化通信開始", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "強化通信完了", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(120, "強化再演出開始", FlowNode.PinTypes.Output, 120)]
  [FlowNode.Pin(200, "自身を閉じてルーン装備へ", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(210, "強化アニメーション終了", FlowNode.PinTypes.Input, 210)]
  [FlowNode.Pin(220, "強化アニメーション終了", FlowNode.PinTypes.Output, 220)]
  [FlowNode.Pin(230, "連続強化を止める", FlowNode.PinTypes.Input, 230)]
  [FlowNode.Pin(240, "連続強化である", FlowNode.PinTypes.Output, 240)]
  [FlowNode.Pin(250, "連続強化でない", FlowNode.PinTypes.Output, 250)]
  [FlowNode.Pin(1000, "自身を閉じる", FlowNode.PinTypes.Output, 1000)]
  public class RuneEnhanceEffectWindow : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_RUNE_ENHANCE_ANIM = 1;
    private const int OUTPUT_RUNE_ENHANCE_SUCCESS_ANIM = 2;
    private const int OUTPUT_RUNE_ENHANCE_FAILURE_ANIM = 3;
    private const int INPUT_RUNE_ENHANCE = 10;
    private const int INPUT_RUNE_USE_GAUGE = 11;
    private const int INPUT_REFRESH_BUTTON = 12;
    private const int OUTPUT_START_ENHANCE = 100;
    private const int INPUT_FINISHED_ENHANCE = 110;
    private const int OUTPUT_START_RE_ENHANCE = 120;
    private const int INPUT_CLOSE_TO_EQUIP = 200;
    private const int INPUT_END_RUNE_ENHANCE_ANIM = 210;
    private const int OUTPUT_END_RUNE_ENHANCE_ANIM = 220;
    private const int INPUT_STOP_CONTINUE_ENHANCE = 230;
    private const int OUTPUT_CONTINUE_ENHANCE_ON = 240;
    private const int OUTPUT_CONTINUE_ENHANCE_OFF = 250;
    private const int OUTPUT_CLOSE_WINDOW = 1000;
    [SerializeField]
    private float CONTINUE_ENHANCE_INTERVAL = 2f;
    [SerializeField]
    private RuneIcon mRuneIcon;
    [SerializeField]
    private RuneDrawEnhanceLevel mRuneDrawEnhanceLevel;
    [SerializeField]
    private RuneDrawEnhancedBaseState mRuneDrawEnhancedBaseState;
    [SerializeField]
    private RuneDrawCost mRuneDrawCost;
    [SerializeField]
    private RuneDrawEnhanceEffect mRuneDrawEnhanceEffect;
    [SerializeField]
    private RuneDrawEnhancePercentage mRuneDrawEnhancePercentage;
    [SerializeField]
    private Button mEnhanceButton;
    [SerializeField]
    private GameObject mSwitchObj_NextEnhance;
    [SerializeField]
    private GameObject mSwitchObj_NextEvo;
    [SerializeField]
    private Button mCloseButton;
    [SerializeField]
    private Button mContinueEnhanceStopButton;
    [SerializeField]
    private Button mUseGaugeButton;
    private RuneManager mRuneManager;
    private BindRuneData mRuneDataBefore;
    private BindRuneData mRuneDataCurr;
    private bool mIsEnhanceSuccess;
    private bool mIsUseEnforceGauge;
    private bool mIsStopContinueEnhanceTime;
    private bool mIsContinueEnhance;
    private float mRestContinueEnhanceTime;
    private GameObject mConfirmEnhancePopup;

    public void Awake()
    {
    }

    private void OnDestroy()
    {
      this.mRuneManager.EnhanceSettings.Reset(true);
      if (!Object.op_Inequality((Object) this.mConfirmEnhancePopup, (Object) null))
        return;
      Win_Btn_DecideCancel_FL_C component = this.mConfirmEnhancePopup.GetComponent<Win_Btn_DecideCancel_FL_C>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.BeginClose();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.PlayAnimation();
          break;
        case 10:
          this.ConfirmEnhance();
          break;
        case 11:
          if (Object.op_Implicit((Object) this.mRuneDrawEnhancePercentage) && RuneUtility.IsCanUseGauge(this.mRuneDataCurr))
          {
            this.mIsUseEnforceGauge = !this.mIsUseEnforceGauge;
            this.mRuneDrawEnhancePercentage.SetDrawParam(this.mRuneDataCurr, this.mIsUseEnforceGauge);
            break;
          }
          this.mIsStopContinueEnhanceTime = true;
          UIUtility.SystemMessage(LocalizedText.Get("sys.RUNE_IS_NOT_USE_ENFORCEGAUGE"), (UIUtility.DialogResultEvent) (yes_button => this.ResetContinueEnhanceParam()));
          break;
        case 12:
          this.EnhanceButtonRefresh();
          break;
        case 110:
          if (Object.op_Implicit((Object) this.mRuneManager))
          {
            this.mRuneManager.RefreshRuneEnhanceFinished();
            FlowNode_ReqRuneEnhance.Clear();
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 120);
          break;
        case 200:
          if (Object.op_Implicit((Object) this.mRuneManager))
            this.mRuneManager.SelectedRuneAsEquip(this.mRuneDataCurr);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
          break;
        case 210:
          this.OnEnhanceAnimationEnd();
          break;
        case 230:
          this.StopContinueEnhance();
          break;
      }
    }

    private void Update() => this.Update_ContinueEnhance();

    public void Setup(
      RuneManager manager,
      BindRuneData before_rune_data,
      BindRuneData after_rune_data,
      float _before_gauge)
    {
      this.mRuneManager = manager;
      this.mRuneDataBefore = before_rune_data;
      this.mRuneDataCurr = after_rune_data;
      this.mIsEnhanceSuccess = FlowNode_ReqRuneEnhance.IsResultSuccess();
      this.mIsUseEnforceGauge = manager.EnhanceSettings.UseGauge;
      RuneData rune1 = this.mRuneDataBefore.Rune;
      RuneData rune2 = this.mRuneDataCurr.Rune;
      if (rune1 == null || rune2 == null)
        return;
      int add_level = rune2.EnhanceNum - rune1.EnhanceNum;
      bool isEvoNext = rune2.IsEvoNext;
      if (Object.op_Implicit((Object) this.mSwitchObj_NextEnhance))
        this.mSwitchObj_NextEnhance.SetActive(!isEvoNext);
      if (Object.op_Implicit((Object) this.mSwitchObj_NextEvo))
        this.mSwitchObj_NextEvo.SetActive(isEvoNext);
      if (Object.op_Implicit((Object) this.mRuneIcon))
        this.mRuneIcon.Setup(this.mRuneDataCurr);
      if (Object.op_Implicit((Object) this.mRuneDrawEnhanceLevel))
        this.mRuneDrawEnhanceLevel.SetDrawParam(this.mRuneDataBefore, add_level);
      if (Object.op_Implicit((Object) this.mRuneDrawEnhancedBaseState))
        this.mRuneDrawEnhancedBaseState.SetDrawParam(this.mRuneDataBefore, this.mRuneDataCurr);
      if (Object.op_Implicit((Object) this.mRuneDrawCost))
        this.mRuneDrawCost.SetDrawParam(this.mRuneDataCurr.EnhanceCost);
      if (Object.op_Implicit((Object) this.mRuneDrawEnhanceEffect))
        this.mRuneDrawEnhanceEffect.SetDrawParam(this.mIsEnhanceSuccess);
      if (Object.op_Implicit((Object) this.mRuneDrawEnhancePercentage))
      {
        this.mRuneDrawEnhancePercentage.SetDrawParam(this.mRuneDataCurr, this.mIsUseEnforceGauge);
        this.mRuneDrawEnhancePercentage.SetupGaugeAnim(_before_gauge);
      }
      this.Refresh();
      bool flag = false;
      if (Object.op_Inequality((Object) this.mRuneManager, (Object) null))
      {
        this.mRuneManager.EnhanceSettings.Decrement();
        flag = this.IsEnableContinueEnhance();
      }
      if (Object.op_Inequality((Object) this.mRuneManager, (Object) null) && !flag)
        this.mRuneManager.EnhanceSettings.Reset();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, !flag ? 250 : 240);
      this.mIsUseEnforceGauge = false;
      if (Object.op_Inequality((Object) this.mRuneManager, (Object) null) && flag)
        this.mIsUseEnforceGauge = this.mRuneManager.EnhanceSettings.UseGauge;
      if (flag)
      {
        GameUtility.SetGameObjectActive((Component) this.mCloseButton, false);
        GameUtility.SetGameObjectActive((Component) this.mContinueEnhanceStopButton, true);
        if (!Object.op_Inequality((Object) this.mUseGaugeButton, (Object) null))
          return;
        ((Selectable) this.mUseGaugeButton).interactable = false;
      }
      else
      {
        GameUtility.SetGameObjectActive((Component) this.mCloseButton, true);
        GameUtility.SetGameObjectActive((Component) this.mContinueEnhanceStopButton, false);
        if (!Object.op_Inequality((Object) this.mUseGaugeButton, (Object) null))
          return;
        ((Selectable) this.mUseGaugeButton).interactable = true;
      }
    }

    public void Refresh()
    {
      if (Object.op_Implicit((Object) this.mRuneIcon))
        this.mRuneIcon.Refresh();
      if (Object.op_Implicit((Object) this.mRuneDrawEnhanceLevel))
        this.mRuneDrawEnhanceLevel.Refresh();
      if (Object.op_Implicit((Object) this.mRuneDrawEnhancedBaseState))
        this.mRuneDrawEnhancedBaseState.Refresh();
      if (Object.op_Implicit((Object) this.mRuneDrawCost))
        this.mRuneDrawCost.Refresh();
      if (Object.op_Implicit((Object) this.mRuneDrawEnhanceEffect))
        this.mRuneDrawEnhanceEffect.Refresh();
      if (Object.op_Implicit((Object) this.mRuneDrawEnhancePercentage))
        this.mRuneDrawEnhancePercentage.Refresh();
      if (!Object.op_Implicit((Object) this.mEnhanceButton) || this.mRuneDataCurr == null)
        return;
      RuneCost enhanceCost = this.mRuneDataCurr.EnhanceCost;
      if (enhanceCost == null)
        return;
      ((Selectable) this.mEnhanceButton).interactable = enhanceCost.IsPlayerAmountEnough();
    }

    private void EnhanceButtonRefresh()
    {
      if (!Object.op_Implicit((Object) this.mEnhanceButton) || this.mRuneDataCurr == null)
        return;
      RuneCost enhanceCost = this.mRuneDataCurr.EnhanceCost;
      if (enhanceCost == null)
        return;
      ((Selectable) this.mEnhanceButton).interactable = enhanceCost.IsPlayerAmountEnough();
    }

    public void ConfirmEnhance()
    {
      if (Object.op_Equality((Object) this.mRuneManager, (Object) null))
        return;
      string key = "sys.RUNE_ENHANCE_NOGAUGE_CONFIRM";
      if (this.mIsUseEnforceGauge)
        key = this.mIsContinueEnhance ? "sys.RUNE_ENHANCE_CONTINUE_GAUGE_CONFIRM" : "sys.RUNE_ENHANCE_GAUGE_CONFIRM";
      this.mIsStopContinueEnhanceTime = true;
      this.mConfirmEnhancePopup = UIUtility.ConfirmBox(LocalizedText.Get(key), (UIUtility.DialogResultEvent) (yes_button => this.StartEnhance()), (UIUtility.DialogResultEvent) (no_button => this.ResetContinueEnhanceParam()));
    }

    private void PlayAnimation()
    {
      if (Object.op_Equality((Object) this.mRuneDrawEnhanceEffect, (Object) null))
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
      if (this.mRuneDrawEnhanceEffect.IsEnhanceSuccess)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);
    }

    private void OnEnhanceAnimationEnd()
    {
      this.ResetContinueEnhanceParam();
      this.mIsContinueEnhance = false;
      if (Object.op_Inequality((Object) this.mRuneManager, (Object) null))
        this.mIsContinueEnhance = this.IsEnableContinueEnhance();
      if (Object.op_Inequality((Object) this.mRuneDrawEnhancePercentage, (Object) null))
        this.mRuneDrawEnhancePercentage.SetDrawParam(this.mRuneDataCurr, this.mIsUseEnforceGauge);
      if (Object.op_Inequality((Object) this.mRuneManager, (Object) null) && Object.op_Inequality((Object) this.mRuneDrawEnhanceEffect, (Object) null) && this.mRuneManager.EnhanceSettings.StartedMode != RuneEnhanceSettings.eEnhanceMode.Normal && !this.mRuneManager.EnhanceSettings.UseGauge && RuneUtility.CalcPlayerGauge(this.mRuneDataCurr) >= RuneUtility.CAN_USE_GAUGE_PERCENTAGE && !this.mRuneDrawEnhanceEffect.IsEnhanceSuccess)
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.RUNE_CONTINUE_ENHANCE_STOP_REASON"), (UIUtility.DialogResultEvent) (go => { }));
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 220);
    }

    private void StopContinueEnhance()
    {
      this.mIsContinueEnhance = false;
      if (Object.op_Inequality((Object) this.mRuneManager, (Object) null))
        this.mRuneManager.EnhanceSettings.Reset();
      GameUtility.SetGameObjectActive((Component) this.mCloseButton, true);
      GameUtility.SetGameObjectActive((Component) this.mContinueEnhanceStopButton, false);
      if (!Object.op_Inequality((Object) this.mUseGaugeButton, (Object) null))
        return;
      ((Selectable) this.mUseGaugeButton).interactable = true;
    }

    private void Update_ContinueEnhance()
    {
      if (!this.mIsContinueEnhance || this.mIsStopContinueEnhanceTime)
        return;
      this.mRestContinueEnhanceTime -= Time.deltaTime;
      if ((double) this.mRestContinueEnhanceTime > 0.0)
        return;
      this.mIsContinueEnhance = false;
      this.StartEnhance();
    }

    private void StartEnhance()
    {
      FlowNode_ReqRuneEnhance.SetTarget(this.mRuneDataCurr, this.mIsUseEnforceGauge);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private bool IsEnableContinueEnhance()
    {
      return !Object.op_Equality((Object) this.mRuneManager, (Object) null) && this.mRuneDataCurr != null && (this.mRuneManager.EnhanceSettings.UseGauge || this.mRuneDrawEnhanceEffect.IsEnhanceSuccess || RuneUtility.CalcPlayerGauge(this.mRuneDataCurr) < RuneUtility.CAN_USE_GAUGE_PERCENTAGE) && !this.mRuneDataCurr.Rune.IsEvoNext && this.mRuneDataCurr.EnhanceCost.IsPlayerAmountEnough() && this.mRuneManager.EnhanceSettings.IsEnableContinueEnhance(this.mRuneDataCurr.Rune);
    }

    private void ResetContinueEnhanceParam()
    {
      this.mIsStopContinueEnhanceTime = false;
      this.mRestContinueEnhanceTime = this.CONTINUE_ENHANCE_INTERVAL;
    }
  }
}
