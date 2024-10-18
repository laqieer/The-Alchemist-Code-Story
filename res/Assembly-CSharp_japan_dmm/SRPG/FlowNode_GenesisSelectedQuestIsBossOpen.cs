// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GenesisSelectedQuestIsBossOpen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Genesis/ボス開放の可能性があるか？", 32741)]
  [FlowNode.Pin(1, "In", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "ボス開放の可能性あり", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(21, "ボス開放の可能性なし", FlowNode.PinTypes.Output, 21)]
  public class FlowNode_GenesisSelectedQuestIsBossOpen : FlowNode
  {
    private const int PIN_IN = 1;
    private const int PIN_OUT_TRUE = 11;
    private const int PIN_OUT_FALSE = 21;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      if (quest == null || !quest.IsGenesisStory)
      {
        this.ActivateOutputLinks(21);
      }
      else
      {
        GenesisChapterManager instance = GenesisChapterManager.Instance;
        if (Object.op_Equality((Object) instance, (Object) null))
        {
          this.ActivateOutputLinks(21);
        }
        else
        {
          GenesisChapterParam currentChapterParam = instance.CurrentChapterParam;
          if (currentChapterParam.IsBossCondQuests(instance.StageDifficulty))
            this.ActivateOutputLinks(21);
          else if (currentChapterParam.IsBossLiberation(instance.StageDifficulty))
            this.ActivateOutputLinks(21);
          else
            this.ActivateOutputLinks(11);
        }
      }
    }
  }
}
