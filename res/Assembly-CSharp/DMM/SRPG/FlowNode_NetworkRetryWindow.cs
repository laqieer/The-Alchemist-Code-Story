// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NetworkRetryWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Common/NetworkRetryWindow", 32741)]
  [FlowNode.Pin(0, "Create", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_NetworkRetryWindow : FlowNode
  {
    public GameObject Window;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || !Object.op_Inequality((Object) this.Window, (Object) null))
        return;
      Object.Instantiate<GameObject>(this.Window);
    }
  }
}
