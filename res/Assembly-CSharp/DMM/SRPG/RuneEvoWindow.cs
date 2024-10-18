// Decompiled with JetBrains decompiler
// Type: SRPG.RuneEvoWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "覚醒", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "覚醒通信開始", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "覚醒通信完了", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(1000, "自身を閉じる", FlowNode.PinTypes.Output, 1000)]
  public class RuneEvoWindow : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_RUNE_EVO = 10;
    private const int OUTPUT_START_EVO = 100;
    private const int INPUT_FINISHED_EVO = 110;
    private const int OUTPUT_CLOSE_WINDOW = 1000;
    [SerializeField]
    private RuneDrawInfo mRuneDrawInfo;
    [SerializeField]
    private RuneIcon mRuneIconBfore;
    [SerializeField]
    private RuneIcon mRuneIconAfter;
    [SerializeField]
    private RuneDrawBaseState mRuneDrawBaseState;
    [SerializeField]
    private RuneDrawEvoState mRuneDrawEvoState;
    [SerializeField]
    private RuneDrawCost mRuneDrawCost;
    [SerializeField]
    private Button mEvoButton;
    private RuneManager mRuneManager;
    private BindRuneData mRuneData;
    private BindRuneData mBeforeRune;

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
          this.ConfirmEvolution();
          break;
        case 110:
          if (Object.op_Implicit((Object) this.mRuneManager))
            this.mRuneManager.RefreshRuneEvoFinished(this.mBeforeRune, this.mRuneData);
          FlowNode_ReqRuneEvo.Clear();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
          break;
      }
    }

    public void Setup(RuneManager manager, BindRuneData rune_data)
    {
      if (rune_data == null)
        return;
      this.mRuneManager = manager;
      this.mRuneData = rune_data;
      BindRuneData rune1 = (BindRuneData) null;
      if (this.mRuneData != null)
      {
        rune1 = this.mRuneData.CreateCopyRune();
        RuneData rune2 = rune1.Rune;
        if (rune2 != null && rune2.IsCanEvo)
          ++rune2.evo;
        this.mBeforeRune = this.mRuneData.CreateCopyRune();
      }
      if (Object.op_Implicit((Object) this.mRuneDrawInfo))
        this.mRuneDrawInfo.SetDrawParam(this.mRuneData);
      if (Object.op_Implicit((Object) this.mRuneIconBfore))
        this.mRuneIconBfore.Setup(this.mRuneData);
      if (Object.op_Implicit((Object) this.mRuneIconAfter))
        this.mRuneIconAfter.Setup(rune1);
      if (Object.op_Implicit((Object) this.mRuneDrawBaseState))
        this.mRuneDrawBaseState.SetDrawParam(this.mRuneData);
      if (Object.op_Implicit((Object) this.mRuneDrawEvoState))
        this.mRuneDrawEvoState.SetDrawParam(this.mRuneData);
      if (Object.op_Implicit((Object) this.mRuneDrawCost))
        this.mRuneDrawCost.SetDrawParam(this.mRuneData.EvoCost);
      this.Refresh();
    }

    public void Refresh()
    {
      if (Object.op_Implicit((Object) this.mRuneDrawInfo))
        this.mRuneDrawInfo.Refresh();
      if (Object.op_Implicit((Object) this.mRuneIconBfore))
        this.mRuneIconBfore.Refresh();
      if (Object.op_Implicit((Object) this.mRuneIconAfter))
        this.mRuneIconAfter.Refresh();
      if (Object.op_Implicit((Object) this.mRuneDrawBaseState))
        this.mRuneDrawBaseState.Refresh();
      if (Object.op_Implicit((Object) this.mRuneDrawEvoState))
        this.mRuneDrawEvoState.Refresh();
      if (Object.op_Implicit((Object) this.mRuneDrawCost))
        this.mRuneDrawCost.Refresh();
      this.RefreshButton();
    }

    public void RefreshButton()
    {
      if (!Object.op_Implicit((Object) this.mEvoButton))
        return;
      RuneCost evoCost = this.mRuneData.EvoCost;
      if (evoCost == null)
        return;
      ((Selectable) this.mEvoButton).interactable = this.mRuneData.EvoNum + 1 <= MonoSingleton<GameManager>.Instance.MasterParam.FixParam.RuneMaxEvoNum && evoCost.IsPlayerAmountEnough();
    }

    public void ConfirmEvolution()
    {
      UIUtility.ConfirmBox(LocalizedText.Get("sys.RUNE_EVO_CONFIRM"), (UIUtility.DialogResultEvent) (yes_button =>
      {
        FlowNode_ReqRuneEvo.SetTarget(this.mRuneData);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }), (UIUtility.DialogResultEvent) null);
    }
  }
}
