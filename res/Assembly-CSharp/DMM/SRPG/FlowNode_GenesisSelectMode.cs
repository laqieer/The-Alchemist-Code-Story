// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GenesisSelectMode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Genesis/SelectMode", 32741)]
  [FlowNode.Pin(1, "Normal", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Eliete", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "Extra", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_GenesisSelectMode : FlowNode
  {
    [SerializeField]
    private FlowNode_GenesisSelectMode.ModeTarget mModeTarget;

    public override void OnActivate(int pinID)
    {
      GenesisChapterManager instance = GenesisChapterManager.Instance;
      QuestDifficulties difficult = QuestDifficulties.Normal;
      switch (pinID)
      {
        case 1:
          difficult = QuestDifficulties.Normal;
          break;
        case 2:
          difficult = QuestDifficulties.Elite;
          break;
        case 3:
          difficult = QuestDifficulties.Extra;
          break;
      }
      if (this.mModeTarget == FlowNode_GenesisSelectMode.ModeTarget.Stage)
        instance.SetStageDifficulty(difficult);
      else
        instance.SetBossDifficulty(difficult);
      this.ActivateOutputLinks(101);
    }

    public enum ModeTarget
    {
      Stage,
      Boss,
    }
  }
}
