// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_UnitGetEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;

namespace SRPG
{
  [FlowNode.Pin(10, "終了", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("UI/UnitGetEffect", 32741)]
  [FlowNode.Pin(1, "開始", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_UnitGetEffect : FlowNode
  {
    private UnitGetWindowController mWindow;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.mWindow = this.gameObject.AddComponent<UnitGetWindowController>();
      this.mWindow.Init((UnitGetParam) null);
      this.StartCoroutine(this.ShowEffect());
    }

    [DebuggerHidden]
    private IEnumerator ShowEffect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_UnitGetEffect.\u003CShowEffect\u003Ec__IteratorDF() { \u003C\u003Ef__this = this };
    }
  }
}
