// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayFirstContact
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(0, "exist", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "no", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1, "yes", FlowNode.PinTypes.Output, 0)]
  [FlowNode.NodeType("Multi/MultiPlayFirstContact", 32741)]
  public class FlowNode_MultiPlayFirstContact : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ActivateOutputLinks((UnityEngine.Object) SceneBattle.Instance == (UnityEngine.Object) null || SceneBattle.Instance.FirstContact <= 0 ? 2 : 1);
    }
  }
}
