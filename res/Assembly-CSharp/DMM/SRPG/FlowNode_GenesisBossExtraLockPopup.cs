// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GenesisBossExtraLockPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Genesis/ボスロックポップ", 32741)]
  [FlowNode.Pin(1, "In", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "Out", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_GenesisBossExtraLockPopup : FlowNode
  {
    private const int PIN_IN = 1;
    private const int PIN_OUT = 11;
    [SerializeField]
    private QuestDifficulties QuestDifficulty;
    [SerializeField]
    private bool IsUnlockCondDisp;
    private bool mIsRunning;

    protected override void Awake()
    {
      base.Awake();
      this.mIsRunning = false;
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || this.mIsRunning)
        return;
      this.StartCoroutine(this.PopupMessageOpen());
    }

    [DebuggerHidden]
    private IEnumerator PopupMessageOpen()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_GenesisBossExtraLockPopup.\u003CPopupMessageOpen\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
