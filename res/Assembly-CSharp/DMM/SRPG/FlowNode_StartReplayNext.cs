// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_StartReplayNext
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Replay/StartReplayNext", 32741)]
  [FlowNode.Pin(1, "Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "OK", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "Cancel", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_StartReplayNext : FlowNode
  {
    private const int PIN_IN_CHECK = 1;
    private const int PIN_OUT_OK = 101;
    private const int PIN_OUT_CANCEL = 102;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Implicit((Object) instance) && instance.Player != null)
      {
        QuestParam quest = instance.FindQuest((string) GlobalVars.ReplaySelectedNextQuestID);
        if (quest != null)
        {
          this.StartCoroutine(this.PrepareAssets(quest));
          return;
        }
      }
      this.ActivateOutputLinks(102);
    }

    [DebuggerHidden]
    private IEnumerator PrepareAssets(QuestParam quest_param)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_StartReplayNext.\u003CPrepareAssets\u003Ec__Iterator0()
      {
        quest_param = quest_param,
        \u0024this = this
      };
    }
  }
}
