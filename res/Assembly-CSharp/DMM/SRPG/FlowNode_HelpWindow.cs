// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_HelpWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/Helpwindow", 32741)]
  [FlowNode.Pin(0, "Setup", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Finish", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_HelpWindow : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Network.SiteHost);
      stringBuilder.Append("notice/detail/help/index");
      FlowNode_Variable.Set("SHARED_WEBWINDOW_TITLE", LocalizedText.Get("sys.CONFIG_BTN_HELP"));
      FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", stringBuilder.ToString());
      this.ActivateOutputLinks(100);
    }
  }
}
