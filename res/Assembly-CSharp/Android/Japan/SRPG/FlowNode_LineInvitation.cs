// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LineInvitation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("LINE/Invitation", 32741)]
  [FlowNode.Pin(100, "Send", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(110, "Read", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(200, "Send Done", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(200, "Read Done", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_LineInvitation : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID == 100)
      {
        string s = LocalizedText.Get("sys.MP_LINE_INVITATION");
        DebugUtility.Log("LINE招待:" + s);
        Application.OpenURL(LocalizedText.Get("sys.MP_LINE_HTTP") + WWW.EscapeURL(s, Encoding.UTF8));
        this.ActivateOutputLinks(200);
      }
      else if (pinID == 110)
        ;
    }
  }
}
