// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GenesisSelectMode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
