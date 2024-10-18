// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AdvanceCacheClear
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Advance/CacheClear", 32741)]
  [FlowNode.Pin(1, "Input", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Output", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_AdvanceCacheClear : FlowNode
  {
    public const int PIN_IN = 1;
    public const int PIN_OUT = 101;
    [SerializeField]
    private bool GlobalVarsClear;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      AdvanceManager.CurrentChapterParam = (ChapterParam) null;
      AdvanceManager.CurrentEventParam = (AdvanceEventParam) null;
      if (this.GlobalVarsClear)
      {
        GlobalVars.SelectedChapter.Reset();
        GlobalVars.SelectedQuestID = string.Empty;
      }
      this.ActivateOutputLinks(101);
    }
  }
}
