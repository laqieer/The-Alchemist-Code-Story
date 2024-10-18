﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_QuestTutorial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Tutorial/Quest Tutorial", 32741)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "Confirm", FlowNode.PinTypes.Output, 3)]
  public class FlowNode_QuestTutorial : FlowNode
  {
    public string QuestID;
    public FlowNode_QuestTutorial.TriggerConditions Condition;
    public string ConfirmText;
    public string LocalFlag;
    public bool CheckLastPlayed;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      string str1 = !this.CheckLastPlayed ? GlobalVars.SelectedQuestID : GlobalVars.LastPlayedQuest.Get();
      if (!string.IsNullOrEmpty(this.QuestID) && str1 != this.QuestID)
      {
        this.OnNo((GameObject) null);
      }
      else
      {
        string str2 = (string) null;
        if (!string.IsNullOrEmpty(this.LocalFlag))
          str2 = FlowNode_Variable.Get(this.LocalFlag);
        if (this.CheckCondition() && string.IsNullOrEmpty(str2))
        {
          if (!string.IsNullOrEmpty(this.LocalFlag))
            FlowNode_Variable.Set(this.LocalFlag, "1");
          if (!string.IsNullOrEmpty(this.ConfirmText))
          {
            this.ActivateOutputLinks(3);
            UIUtility.ConfirmBox(LocalizedText.Get(this.ConfirmText), new UIUtility.DialogResultEvent(this.OnYes), new UIUtility.DialogResultEvent(this.OnNo), systemModal: true);
          }
          else
            this.OnYes((GameObject) null);
        }
        else
          this.OnNo((GameObject) null);
      }
    }

    private bool CheckCondition()
    {
      switch (this.Condition)
      {
        case FlowNode_QuestTutorial.TriggerConditions.FirstTry:
          return GlobalVars.LastQuestState.Get() == QuestStates.New;
        case FlowNode_QuestTutorial.TriggerConditions.FirstWin:
          return GlobalVars.LastQuestResult.Get() == BattleCore.QuestResult.Win && GlobalVars.LastQuestState.Get() != QuestStates.Cleared;
        case FlowNode_QuestTutorial.TriggerConditions.FirstLose:
          return GlobalVars.LastQuestResult.Get() != BattleCore.QuestResult.Win && GlobalVars.LastQuestState.Get() == QuestStates.New;
        case FlowNode_QuestTutorial.TriggerConditions.Win:
          return GlobalVars.LastQuestResult.Get() == BattleCore.QuestResult.Win;
        case FlowNode_QuestTutorial.TriggerConditions.Lose:
          return GlobalVars.LastQuestResult.Get() != BattleCore.QuestResult.Win;
        default:
          return true;
      }
    }

    private void OnYes(GameObject go) => this.ActivateOutputLinks(1);

    private void OnNo(GameObject go) => this.ActivateOutputLinks(2);

    public enum TriggerConditions
    {
      None,
      FirstTry,
      FirstWin,
      FirstLose,
      Win,
      Lose,
    }
  }
}
