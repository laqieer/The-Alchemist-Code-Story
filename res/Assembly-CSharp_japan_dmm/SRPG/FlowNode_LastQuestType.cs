// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LastQuestType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Battle/LastQuestType", 32741)]
  [FlowNode.Pin(100, "Input", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "SinglePlay", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "MultiPlay", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(200, "Input", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(201, "Story", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "Multi", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(203, "Arena", FlowNode.PinTypes.Output, 203)]
  [FlowNode.Pin(204, "Tutorial", FlowNode.PinTypes.Output, 204)]
  [FlowNode.Pin(205, "Free", FlowNode.PinTypes.Output, 205)]
  [FlowNode.Pin(206, "Event", FlowNode.PinTypes.Output, 206)]
  [FlowNode.Pin(207, "Character", FlowNode.PinTypes.Output, 207)]
  [FlowNode.Pin(208, "Tower", FlowNode.PinTypes.Output, 208)]
  [FlowNode.Pin(209, "VersusFree", FlowNode.PinTypes.Output, 209)]
  [FlowNode.Pin(210, "VersusRank", FlowNode.PinTypes.Output, 210)]
  [FlowNode.Pin(211, "Gps", FlowNode.PinTypes.Output, 211)]
  [FlowNode.Pin(212, "Extra", FlowNode.PinTypes.Output, 212)]
  [FlowNode.Pin(213, "MultiTower", FlowNode.PinTypes.Output, 213)]
  [FlowNode.Pin(214, "Beginner", FlowNode.PinTypes.Output, 214)]
  [FlowNode.Pin(215, "MultiGps", FlowNode.PinTypes.Output, 215)]
  [FlowNode.Pin(216, "Ordeal", FlowNode.PinTypes.Output, 216)]
  [FlowNode.Pin(217, "RankMatch", FlowNode.PinTypes.Output, 217)]
  [FlowNode.Pin(218, "Raid", FlowNode.PinTypes.Output, 218)]
  [FlowNode.Pin(219, "GenesisStory", FlowNode.PinTypes.Output, 219)]
  [FlowNode.Pin(220, "GenesisBoss", FlowNode.PinTypes.Output, 220)]
  [FlowNode.Pin(221, "AdvanceStory", FlowNode.PinTypes.Output, 221)]
  [FlowNode.Pin(222, "AdvanceBoss", FlowNode.PinTypes.Output, 222)]
  [FlowNode.Pin(223, "UnitRental", FlowNode.PinTypes.Output, 223)]
  [FlowNode.Pin(225, "WorldRaid", FlowNode.PinTypes.Output, 225)]
  public class FlowNode_LastQuestType : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      ((Behaviour) this).enabled = false;
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      if (quest == null)
      {
        this.ActivateOutputLinks(101);
        this.ActivateOutputLinks(201);
      }
      switch (pinID)
      {
        case 100:
          switch (quest.type)
          {
            case QuestTypes.Story:
            case QuestTypes.Arena:
            case QuestTypes.Tutorial:
            case QuestTypes.Free:
            case QuestTypes.Event:
            case QuestTypes.Character:
            case QuestTypes.Tower:
            case QuestTypes.Gps:
            case QuestTypes.StoryExtra:
            case QuestTypes.Beginner:
            case QuestTypes.Ordeal:
            case QuestTypes.Raid:
            case QuestTypes.GenesisStory:
            case QuestTypes.GenesisBoss:
            case QuestTypes.AdvanceStory:
            case QuestTypes.AdvanceBoss:
            case QuestTypes.UnitRental:
            case QuestTypes.GuildRaid:
            case QuestTypes.GvG:
            case QuestTypes.WorldRaid:
              this.ActivateOutputLinks(101);
              return;
            case QuestTypes.Multi:
            case QuestTypes.VersusFree:
            case QuestTypes.VersusRank:
            case QuestTypes.MultiTower:
            case QuestTypes.MultiGps:
            case QuestTypes.RankMatch:
              this.ActivateOutputLinks(102);
              return;
            default:
              DebugUtility.LogError("QuestTypesにTypeを追加したらここも見てください。");
              this.ActivateOutputLinks(101);
              return;
          }
        case 200:
          this.ActivateOutputLinks((int) ((byte) 201 + quest.type));
          break;
      }
    }
  }
}
