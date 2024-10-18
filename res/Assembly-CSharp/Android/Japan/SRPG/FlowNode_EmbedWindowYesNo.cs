// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EmbedWindowYesNo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("UI/EmbedWindowYesNo", 32741)]
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(100, "Opened", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_EmbedWindowYesNo : FlowNode
  {
    public string m_Msg;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      bool success = false;
      string msg = LocalizedText.Get(this.m_Msg, ref success);
      if (success)
        EmbedWindowYesNo.Create(msg, new EmbedWindowYesNo.YesNoWindowEvent(this.OnYesNoWindowEvent));
      else
        EmbedWindowYesNo.Create(this.m_Msg, new EmbedWindowYesNo.YesNoWindowEvent(this.OnYesNoWindowEvent));
      this.ActivateOutputLinks(100);
    }

    private void OnYesNoWindowEvent(bool yes)
    {
      if (yes)
        this.ActivateOutputLinks(1);
      else
        this.ActivateOutputLinks(2);
    }
  }
}
