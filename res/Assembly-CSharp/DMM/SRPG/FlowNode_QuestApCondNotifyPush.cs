// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_QuestApCondNotifyPush
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/QuestApCondNotifyPush", 32741)]
  [FlowNode.Pin(1, "Input", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "NotifyPush", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "NotifyNoPush", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(201, "Output", FlowNode.PinTypes.Output, 201)]
  public class FlowNode_QuestApCondNotifyPush : FlowNode
  {
    private const int PIN_IN_INPUT = 1;
    private const int PIN_OUT_PUSH = 101;
    private const int PIN_OUT_NO_PUSH = 102;
    private const int PIN_OUT_OUTPUT = 201;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      bool flag = false;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Implicit((Object) instance))
      {
        QuestParam quest = instance.FindQuest(GlobalVars.SelectedQuestID);
        if (quest != null && !quest.IsScenario && (int) quest.aplv > instance.Player.Lv)
        {
          NotifyList.Push(LocalizedText.Get("sys.QUEST_AP_CONDITION", (object) quest.aplv));
          flag = true;
        }
      }
      if (flag)
        this.ActivateOutputLinks(101);
      else
        this.ActivateOutputLinks(102);
      this.ActivateOutputLinks(201);
    }
  }
}
