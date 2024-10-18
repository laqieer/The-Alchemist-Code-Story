﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EmbedSystemMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(100, "Opened", FlowNode.PinTypes.Output, 100)]
  [FlowNode.NodeType("UI/EmbedSystemMessage", 32741)]
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Done", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_EmbedSystemMessage : FlowNode
  {
    public string m_Msg;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      bool success = false;
      string msg = LocalizedText.Get(this.m_Msg, ref success);
      if (success)
        EmbedSystemMessage.Create(msg, new EmbedSystemMessage.SystemMessageEvent(this.OnSystemMessageEvent));
      else
        EmbedSystemMessage.Create(this.m_Msg, new EmbedSystemMessage.SystemMessageEvent(this.OnSystemMessageEvent));
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
