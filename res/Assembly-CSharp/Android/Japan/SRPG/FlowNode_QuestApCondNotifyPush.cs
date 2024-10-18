// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_QuestApCondNotifyPush
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

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
      if ((bool) ((UnityEngine.Object) instance))
      {
        QuestParam quest = instance.FindQuest(GlobalVars.SelectedQuestID);
        if (quest != null && !quest.IsScenario && (int) quest.aplv > instance.Player.Lv)
        {
          NotifyList.Push(LocalizedText.Get("sys.QUEST_AP_CONDITION", new object[1]
          {
            (object) quest.aplv
          }));
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
