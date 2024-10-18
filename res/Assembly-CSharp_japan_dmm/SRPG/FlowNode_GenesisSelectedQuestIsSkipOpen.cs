// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GenesisSelectedQuestIsSkipOpen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Genesis/スキチケ開放の可能性があるか？", 32741)]
  [FlowNode.Pin(1, "In", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "スキチケ開放の可能性あり", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "スキチケ開放の可能性なし", FlowNode.PinTypes.Output, 12)]
  public class FlowNode_GenesisSelectedQuestIsSkipOpen : FlowNode
  {
    private const int PIN_IN = 1;
    private const int PIN_OUT_TRUE = 11;
    private const int PIN_OUT_FALSE = 12;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.CheckSkipOpen();
    }

    private void CheckSkipOpen()
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      if (quest != null && quest.IsGenesisBoss && quest.HasMission() && !quest.IsMissionCompleteALL())
        this.ActivateOutputLinks(11);
      else
        this.ActivateOutputLinks(12);
    }
  }
}
