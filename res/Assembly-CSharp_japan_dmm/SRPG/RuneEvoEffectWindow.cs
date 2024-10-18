// Decompiled with JetBrains decompiler
// Type: SRPG.RuneEvoEffectWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "ゲージ増加アニメ再生", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "自身を閉じる", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(1000, "自身を閉じる", FlowNode.PinTypes.Output, 1000)]
  public class RuneEvoEffectWindow : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_START_GAUGE_ANIM = 10;
    private const int INPUT_CLOSE_WINDOW = 100;
    private const int OUTPUT_CLOSE_WINDOW = 1000;
    [SerializeField]
    private RuneIcon mRuneIconBfore;
    [SerializeField]
    private RuneIcon mRuneIconAfter;
    [SerializeField]
    private RuneDrawEvoStateOneSetting mOneSetting;
    [SerializeField]
    private Button mCloseButton;
    private RuneManager mRuneManager;
    private BindRuneData mCurrRuneData;

    public void Awake()
    {
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          if (!Object.op_Implicit((Object) this.mOneSetting))
            break;
          this.mOneSetting.StartGaugeAnim();
          break;
        case 100:
          if (Object.op_Implicit((Object) this.mRuneManager))
            this.mRuneManager.SelectedRuneAsEquip(this.mCurrRuneData);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
          break;
      }
    }

    public void Setup(RuneManager manager, BindRuneData before_rune, BindRuneData after_rune)
    {
      this.mRuneManager = manager;
      this.mCurrRuneData = after_rune;
      RuneData rune = after_rune.Rune;
      int index = (int) rune.evo - 1;
      BaseStatus addStatus = (BaseStatus) null;
      BaseStatus scaleStatus = (BaseStatus) null;
      rune.CreateBaseStatusFromEvoParam(index, ref addStatus, ref scaleStatus, true);
      if (addStatus == null && scaleStatus == null)
        return;
      float percentage = rune.PowerPercentageFromEvoParam(index);
      if (Object.op_Implicit((Object) this.mRuneIconBfore))
        this.mRuneIconBfore.Setup(before_rune);
      if (Object.op_Implicit((Object) this.mRuneIconAfter))
        this.mRuneIconAfter.Setup(after_rune);
      if (!Object.op_Implicit((Object) this.mOneSetting))
        return;
      if (addStatus == null)
        addStatus = new BaseStatus();
      if (scaleStatus == null)
        scaleStatus = new BaseStatus();
      this.mOneSetting.SetStatus(addStatus, scaleStatus, percentage, true);
    }

    public void Refresh()
    {
      if (Object.op_Implicit((Object) this.mRuneIconBfore))
        this.mRuneIconBfore.Refresh();
      if (Object.op_Implicit((Object) this.mRuneIconAfter))
        this.mRuneIconAfter.Refresh();
      if (!Object.op_Implicit((Object) this.mOneSetting))
        return;
      this.mOneSetting.Refresh();
    }
  }
}
