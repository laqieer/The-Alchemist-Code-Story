// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EmbedSystemMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/EmbedSystemMessage", 32741)]
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Done", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(100, "Opened", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_EmbedSystemMessage : FlowNode
  {
    public string m_Msg;
    public bool dontDestroyOnLoad;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      bool success = false;
      string msg = LocalizedText.Get(this.m_Msg, ref success);
      if (success)
        EmbedSystemMessage.Create(msg, new EmbedSystemMessage.SystemMessageEvent(this.OnSystemMessageEvent), this.dontDestroyOnLoad);
      else
        EmbedSystemMessage.Create(this.m_Msg, new EmbedSystemMessage.SystemMessageEvent(this.OnSystemMessageEvent), this.dontDestroyOnLoad);
      this.ActivateOutputLinks(100);
    }

    private void OnSystemMessageEvent(bool yes)
    {
      if (yes)
        this.ActivateOutputLinks(1);
      else
        this.ActivateOutputLinks(2);
    }
  }
}
