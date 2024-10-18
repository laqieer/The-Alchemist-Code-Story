// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqBtlComRaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("System/ReqBtlCom/ReqBtlComRaid", 32741)]
  [FlowNode.Pin(100, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "Reset to Title", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_ReqBtlComRaid : FlowNode_Network
  {
    public override void OnSuccess(WWWResult www)
    {
    }
  }
}
