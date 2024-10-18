// Decompiled with JetBrains decompiler
// Type: SRPG.RuneEnhanceWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "強化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "ゲージ使用", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(100, "強化通信開始", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "強化通信完了", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(120, "通常強化/連続強化の切替", FlowNode.PinTypes.Input, 120)]
  [FlowNode.Pin(1000, "自身を閉じる", FlowNode.PinTypes.Output, 1000)]
  public class RuneEnhanceWindow : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_RUNE_ENHANCE = 10;
    private const int INPUT_RUNE_USE_GAUGE = 11;
    private const int OUTPUT_START_ENHANCE = 100;
    private const int INPUT_FINISHED_ENHANCE = 110;
    private const int INPUT_SWITCH_ENHANCE_MODE = 120;
    private const int OUTPUT_CLOSE_WINDOW = 1000;
    private const int NORMAL_ENHANCE_ADD = 1;
    [SerializeField]
    private RuneDrawInfo mRuneDrawInfo;
    [SerializeField]
    private RuneIcon mRuneIcon;
    [SerializeField]
    private RuneDrawBaseState mRuneDrawBaseState;
    [SerializeField]
    private RuneDrawBaseState mRuneDrawAfterBaseState;
    [SerializeField]
    private RuneDrawEnhanceLevel mRuneDrawEnhanceLevel;
    [SerializeField]
    private RuneDrawEvoState mRuneDrawEvoState;
    [SerializeField]
    private RuneDrawCost mRuneDrawCost;
    [SerializeField]
    private RuneDrawEnhancePercentage mRuneDrawEnhancePercentage;
    [SerializeField]
    private RuneContinueEnhance mRuneContinueEnhance;
    [SerializeField]
    private Button mEnhanceButton;
    [SerializeField]
    private GameObject mNormalEnhanceModeUI;
    [SerializeField]
    private GameObject mContinueEnhanceModeUI;
    [SerializeField]
    private ImageArray mSwitchEnhanceModeImage;
    private RuneManager mRuneManager;
    private BindRuneData mRuneData;
    private bool mIsUseEnforceGauge;
    private bool mIsNormalEnhance = true;

    public void Awake()
    {
    }

    private void OnDestroy()
    {
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.ConfirmEnhance();
          break;
        case 11:
          if (this.mIsNormalEnhance)
          {
            if (Object.op_Implicit((Object) this.mRuneDrawEnhancePercentage) && RuneUtility.IsCanUseGauge(this.mRuneData))
            {
              this.mIsUseEnforceGauge = !this.mIsUseEnforceGauge;
              this.mRuneDrawEnhancePercentage.SetDrawParam(this.mRuneData, this.mIsUseEnforceGauge);
              break;
            }
            UIUtility.SystemMessage(LocalizedText.Get("sys.RUNE_IS_NOT_USE_ENFORCEGAUGE"), (UIUtility.DialogResultEvent) (yes_button => { }));
            break;
          }
          this.mIsUseEnforceGauge = !this.mIsUseEnforceGauge;
          this.mRuneDrawEnhancePercentage.SetDrawParam(this.mRuneData, this.mIsUseEnforceGauge);
          break;
        case 110:
          if (Object.op_Implicit((Object) this.mRuneManager))
          {
            this.mRuneManager.EnhanceSettings.Reset(true);
            if (!this.mIsNormalEnhance)
            {
              RuneEnhanceSettings.eEnhanceMode mode = RuneEnhanceSettings.eEnhanceMode.Normal;
              int num = 0;
              this.mRuneContinueEnhance.GetEnhanceSettingParam(ref mode, ref num);
              this.mRuneManager.EnhanceSettings.Setup(mode, num, this.mIsUseEnforceGauge);
            }
            this.mIsUseEnforceGauge = false;
            this.mRuneManager.RefreshRuneEnhanceFinished();
            FlowNode_ReqRuneEnhance.Clear();
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
          break;
        case 120:
          this.SwitchEnhanceMode();
          break;
      }
    }

    public void Setup(RuneManager manager, BindRuneData rune_data)
    {
      if (rune_data == null)
        return;
      this.mRuneManager = manager;
      this.mRuneData = rune_data;
      BindRuneData copyRune = this.mRuneData.CreateCopyRune();
      ++copyRune.Rune.enforce;
      if (Object.op_Implicit((Object) this.mRuneDrawInfo))
        this.mRuneDrawInfo.SetDrawParam(this.mRuneData);
      if (Object.op_Implicit((Object) this.mRuneIcon))
        this.mRuneIcon.Setup(this.mRuneData);
      if (Object.op_Implicit((Object) this.mRuneDrawBaseState))
        this.mRuneDrawBaseState.SetDrawParam(this.mRuneData);
      if (Object.op_Implicit((Object) this.mRuneDrawAfterBaseState))
        this.mRuneDrawAfterBaseState.SetDrawParam(copyRune);
      if (Object.op_Implicit((Object) this.mRuneDrawEvoState))
        this.mRuneDrawEvoState.SetDrawParam(this.mRuneData);
      if (Object.op_Implicit((Object) this.mRuneDrawEnhanceLevel))
        this.mRuneDrawEnhanceLevel.SetDrawParam(this.mRuneData, 1);
      if (Object.op_Implicit((Object) this.mRuneDrawCost))
        this.mRuneDrawCost.SetDrawParam(this.mRuneData.EnhanceCost);
      if (Object.op_Implicit((Object) this.mRuneDrawEnhancePercentage))
        this.mRuneDrawEnhancePercentage.SetDrawParam(this.mRuneData, this.mIsUseEnforceGauge);
      if (Object.op_Implicit((Object) this.mRuneContinueEnhance))
        this.mRuneContinueEnhance.Init(this.mRuneData);
      this.Refresh();
    }

    public void Refresh()
    {
      if (Object.op_Implicit((Object) this.mRuneDrawInfo))
        this.mRuneDrawInfo.Refresh();
      if (Object.op_Implicit((Object) this.mRuneIcon))
        this.mRuneIcon.Refresh();
      if (Object.op_Implicit((Object) this.mRuneDrawBaseState))
        this.mRuneDrawBaseState.Refresh();
      if (Object.op_Implicit((Object) this.mRuneDrawAfterBaseState))
        this.mRuneDrawAfterBaseState.Refresh();
      if (Object.op_Implicit((Object) this.mRuneDrawEvoState))
        this.mRuneDrawEvoState.Refresh();
      if (Object.op_Implicit((Object) this.mRuneDrawEnhanceLevel))
        this.mRuneDrawEnhanceLevel.Refresh();
      if (Object.op_Implicit((Object) this.mRuneDrawCost))
        this.mRuneDrawCost.Refresh();
      if (Object.op_Implicit((Object) this.mRuneDrawEnhancePercentage))
        this.mRuneDrawEnhancePercentage.Refresh();
      this.RefreshButton();
    }

    public void RefreshButton()
    {
      if (this.mRuneData == null)
        return;
      RuneCost enhanceCost = this.mRuneData.EnhanceCost;
      if (enhanceCost == null || !Object.op_Implicit((Object) this.mEnhanceButton))
        return;
      ((Selectable) this.mEnhanceButton).interactable = enhanceCost.IsPlayerAmountEnough();
    }

    public void ConfirmEnhance()
    {
      if (Object.op_Equality((Object) this.mRuneManager, (Object) null))
        return;
      string key = "sys.RUNE_ENHANCE_NOGAUGE_CONFIRM";
      if (this.mIsUseEnforceGauge)
        key = !this.mIsNormalEnhance ? "sys.RUNE_ENHANCE_CONTINUE_GAUGE_CONFIRM" : "sys.RUNE_ENHANCE_GAUGE_CONFIRM";
      bool flag = RuneUtility.CalcPlayerGauge(this.mRuneData) >= RuneUtility.CAN_USE_GAUGE_PERCENTAGE;
      if (!this.mIsNormalEnhance && flag && !this.mIsUseEnforceGauge)
        key = "sys.RUNE_ENHANCE_CONTINUE_DONT_USE_GAUGE_CONFIRM1";
      UIUtility.ConfirmBox(LocalizedText.Get(key), (UIUtility.DialogResultEvent) (yes_button =>
      {
        FlowNode_ReqRuneEnhance.SetTarget(this.mRuneData, this.mIsUseEnforceGauge);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }), (UIUtility.DialogResultEvent) null);
    }

    private void SwitchEnhanceMode()
    {
      this.mIsNormalEnhance = !this.mIsNormalEnhance;
      GameUtility.SetGameObjectActive(this.mNormalEnhanceModeUI, this.mIsNormalEnhance);
      GameUtility.SetGameObjectActive(this.mContinueEnhanceModeUI, !this.mIsNormalEnhance);
      if (Object.op_Inequality((Object) this.mSwitchEnhanceModeImage, (Object) null))
        this.mSwitchEnhanceModeImage.ImageIndex = !this.mIsNormalEnhance ? 1 : 0;
      this.mIsUseEnforceGauge = false;
      this.mRuneDrawEnhancePercentage.SetDrawParam(this.mRuneData, this.mIsUseEnforceGauge);
    }
  }
}
