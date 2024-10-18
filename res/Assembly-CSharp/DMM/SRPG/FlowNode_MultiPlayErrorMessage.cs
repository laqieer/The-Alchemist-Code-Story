// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayErrorMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayErrorMessage", 32741)]
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Closed", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_MultiPlayErrorMessage : FlowNode
  {
    private GameObject winGO;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      this.winGO = UIUtility.SystemMessage((string) null, Network.ErrMsg, (UIUtility.DialogResultEvent) (go =>
      {
        if (!Object.op_Inequality((Object) this.winGO, (Object) null))
          return;
        this.winGO = (GameObject) null;
        this.ActivateOutputLinks(100);
      }));
      Network.ResetError();
    }
  }
}
