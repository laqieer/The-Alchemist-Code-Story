// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ClearMySound
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Sound/ClearMySound", 65535)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1000, "End", FlowNode.PinTypes.Output, 1000)]
  public class FlowNode_ClearMySound : FlowNode
  {
    private const int PIN_INPUT_START = 0;
    private const int PIN_OUTPUT_END = 1000;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.StartCoroutine(this.Clear());
    }

    [DebuggerHidden]
    private IEnumerator Clear()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ClearMySound.\u003CClear\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
