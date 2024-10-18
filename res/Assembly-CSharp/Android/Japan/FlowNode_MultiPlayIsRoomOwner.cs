// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlayIsRoomOwner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;

[FlowNode.NodeType("Multi/MultiPlayIsRoomOwner", 32741)]
[FlowNode.Pin(100, "Test", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 2)]
[FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 3)]
public class FlowNode_MultiPlayIsRoomOwner : FlowNode
{
  public override void OnActivate(int pinID)
  {
    if (pinID != 100)
      return;
    if (PunMonoSingleton<MyPhoton>.Instance.IsOldestPlayer())
      this.ActivateOutputLinks(1);
    else
      this.ActivateOutputLinks(2);
  }
}
