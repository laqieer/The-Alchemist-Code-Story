// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AdvanceSelectedQuestIsCond
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Advance/難易度開放の可能性があるクエストか？", 32741)]
  [FlowNode.Pin(1, "In", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "難易度開放の可能性あり", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(21, "難易度開放開放の可能性なし", FlowNode.PinTypes.Output, 21)]
  public class FlowNode_AdvanceSelectedQuestIsCond : FlowNode
  {
    private const int PIN_IN = 1;
    private const int PIN_OUT_TRUE = 11;
    private const int PIN_OUT_FALSE = 21;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      string selected_quest_id = GlobalVars.SelectedQuestID;
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(selected_quest_id);
      if (quest == null || !quest.IsAdvanceStory && !quest.IsAdvanceBoss)
      {
        this.ActivateOutputLinks(21);
      }
      else
      {
        QuestParam[] quests = MonoSingleton<GameManager>.Instance.Quests;
        for (int index = 0; index < quests.Length; ++index)
        {
          if (quests[index] != quest && quests[index].cond_quests != null && (quests[index].IsAdvanceStory || quests[index].IsAdvanceBoss) && !(quests[index].ChapterID != quest.ChapterID) && (quest.IsAdvanceBoss || quests[index].IsAdvanceBoss || quests[index].difficulty != quest.difficulty) && !MonoSingleton<GameManager>.Instance.Player.IsQuestAvailable(quests[index].iname) && Array.FindIndex<string>(quests[index].cond_quests, (Predicate<string>) (cond_quest => cond_quest == selected_quest_id)) >= 0)
          {
            this.ActivateOutputLinks(11);
            return;
          }
        }
        this.ActivateOutputLinks(21);
      }
    }
  }
}
