// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_UnitGetEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;

namespace SRPG
{
  [FlowNode.NodeType("UI/UnitGetEffect", 32741)]
  [FlowNode.Pin(1, "開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "終了", FlowNode.PinTypes.Output, 10)]
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
      return (IEnumerator) new FlowNode_UnitGetEffect.\u003CShowEffect\u003Ec__Iterator0() { \u0024this = this };
    }
  }
}
