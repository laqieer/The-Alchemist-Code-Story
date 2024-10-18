// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqBtlComRaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("System/ReqBtlComRaid", 32741)]
  [FlowNode.Pin(100, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Reset to Title", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_ReqBtlComRaid : FlowNode_Network
  {
    public override void OnSuccess(WWWResult www)
    {
    }

    public override void OnActivate(int pinID)
    {
    }
  }
}
