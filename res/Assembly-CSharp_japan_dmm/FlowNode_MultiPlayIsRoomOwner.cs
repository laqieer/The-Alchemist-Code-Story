// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlayIsRoomOwner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;

#nullable disable
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
