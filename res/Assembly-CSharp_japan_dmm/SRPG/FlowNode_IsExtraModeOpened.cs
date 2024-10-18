// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_IsExtraModeOpened
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Quest/IsExtraModeOpened", 32741)]
  [FlowNode.Pin(0, "StoryExtraがOpenしたか？", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Yes", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "No", FlowNode.PinTypes.Output, 110)]
  public class FlowNode_IsExtraModeOpened : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (this.IsOpened())
        this.ActivateOutputLinks(100);
      else
        this.ActivateOutputLinks(110);
    }

    private bool IsOpened()
    {
      SceneBattle instance = SceneBattle.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return false;
      QuestResultData resultData = instance.ResultData;
      QuestParam current_quest = instance.CurrentQuest;
      if (resultData == null || current_quest == null || !resultData.IsFirstWin)
        return false;
      QuestParam[] all1 = Array.FindAll<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, (Predicate<QuestParam>) (quest => quest.type == QuestTypes.StoryExtra));
      if (all1 == null || all1.Length <= 0)
        return false;
      QuestParam[] all2 = Array.FindAll<QuestParam>(all1, (Predicate<QuestParam>) (quest => !this.IsCond(quest, current_quest)));
      return all2 == null || all2.Length != all1.Length;
    }

    private bool IsCond(QuestParam target_quest, QuestParam cond_quest)
    {
      return target_quest != null && cond_quest != null && target_quest.cond_quests != null && target_quest.cond_quests.Length > 0 && Array.FindIndex<string>(target_quest.cond_quests, (Predicate<string>) (quest_id => quest_id == cond_quest.iname)) >= 0;
    }
  }
}
