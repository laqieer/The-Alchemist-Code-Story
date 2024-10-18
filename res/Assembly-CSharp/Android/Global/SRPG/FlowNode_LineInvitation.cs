// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LineInvitation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("LINE/Invitation", 32741)]
  [FlowNode.Pin(200, "Read Done", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(200, "Send Done", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(110, "Read", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Send", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_LineInvitation : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID == 100)
      {
        string str = LocalizedText.Get("sys.MP_LINE_INVITATION");
        DebugUtility.Log("LINE招待:" + str);
        Application.OpenURL(LocalizedText.Get("sys.MP_LINE_HTTP") + WWW.EscapeURL(str, Encoding.UTF8));
        this.ActivateOutputLinks(200);
      }
      else if (pinID == 110)
        ;
    }
  }
}
