// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_IsSelectedQuestId
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Story/IsSelectedQuestId", 32741)]
  [FlowNode.Pin(1, "Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Selected", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "Not Selected", FlowNode.PinTypes.Output, 110)]
  public class FlowNode_IsSelectedQuestId : FlowNode
  {
    private const int PIN_IN_INPUT = 1;
    private const int PIN_OUT_SELECTED = 100;
    private const int PIN_OUT_NOT_SELECTED = 110;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.ActivateOutputLinks(!this.IsSelected() ? 110 : 100);
    }

    private bool IsSelected()
    {
      return GlobalVars.SelectedStoryPart.Get() != 0 || !string.IsNullOrEmpty(GlobalVars.SelectedSection.Get()) || !string.IsNullOrEmpty(GlobalVars.SelectedChapter.Get());
    }
  }
}
