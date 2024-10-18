// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_HelpWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  [FlowNode.Pin(100, "Finish", FlowNode.PinTypes.Output, 100)]
  [FlowNode.NodeType("UI/Helpwindow", 32741)]
  [FlowNode.Pin(0, "Setup", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_HelpWindow : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Network.NewsHost);
      stringBuilder.Append("notice/detail/help/index");
      FlowNode_Variable.Set("SHARED_WEBWINDOW_TITLE", LocalizedText.Get("sys.CONFIG_BTN_HELP"));
      FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", stringBuilder.ToString());
      this.ActivateOutputLinks(100);
    }
  }
}
