// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ResetQuestIdGlobalVars
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Story/ResetGlobalVars", 32741)]
  [FlowNode.Pin(1, "In", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ResetQuestIdGlobalVars : FlowNode
  {
    private const int PIN_IN_INPUT = 1;
    private const int PIN_OUT_OUTPUT = 100;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.ResetGlobalVars();
      this.ActivateOutputLinks(100);
    }

    private void ResetGlobalVars()
    {
      GlobalVars.SelectedStoryPart.Set(0);
      GlobalVars.SelectedSection.Reset();
      GlobalVars.SelectedChapter.Reset();
      GlobalVars.SelectedQuestID = string.Empty;
    }
  }
}
