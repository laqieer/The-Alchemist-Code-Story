// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckFBLoginStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(2, "Complete", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/CheckFBLoginStatus", 32741)]
  public class FlowNode_CheckFBLoginStatus : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      MonoSingleton<GameManager>.Instance.Player.OnFacebookLogin();
      this.ActivateOutputLinks(2);
    }
  }
}
