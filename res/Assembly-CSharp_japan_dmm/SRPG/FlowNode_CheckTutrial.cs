// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckTutrial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Check/CheckTutrial")]
  [FlowNode.Pin(0, "CheckTutrial", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "Skip", FlowNode.PinTypes.Output, 3)]
  public class FlowNode_CheckTutrial : FlowNode
  {
    private void Start()
    {
    }

    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
        EmbedWindowYesNo.Create(LocalizedText.Get("embed.PLAYTUTORIAL"), new EmbedWindowYesNo.YesNoWindowEvent(this.OnSelect));
      else
        this.ActivateOutputLinks(3);
    }

    private void OnSelect(bool yes)
    {
      if (yes)
        this.OnPlayTutorial();
      else
        this.OnSkipTutorial();
    }

    private void OnPlayTutorial()
    {
      GlobalVars.DebugIsPlayTutorial = true;
      this.ActivateOutputLinks(1);
    }

    private void OnSkipTutorial()
    {
      GlobalVars.DebugIsPlayTutorial = false;
      this.ActivateOutputLinks(2);
    }
  }
}
