// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckAutoRepeatQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("AutoRepeatQuest/Check", 32741)]
  [FlowNode.Pin(10, "自動周回中かチェック", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "自動周回中です", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "自動周回中ではない", FlowNode.PinTypes.Output, 110)]
  public class FlowNode_CheckAutoRepeatQuest : FlowNode
  {
    private const int PIN_INPUT_AUTOREPEAT_NOW = 10;
    private const int PIN_OUTPUT_AUTOREPEAT_NOW = 100;
    private const int PIN_OUTPUT_AUTOREPEAT_NOT_NOW = 110;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      this.StartCoroutine(this.CheckAutoRepeatQuestNow());
    }

    [DebuggerHidden]
    private IEnumerator CheckAutoRepeatQuestNow()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_CheckAutoRepeatQuest.\u003CCheckAutoRepeatQuestNow\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
