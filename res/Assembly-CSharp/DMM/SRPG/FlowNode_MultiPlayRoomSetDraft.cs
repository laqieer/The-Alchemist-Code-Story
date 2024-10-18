// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayRoomSetDraft
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayRoomSetDraft", 32741)]
  [FlowNode.Pin(0, "Set Normal", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Set Draft", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Output", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_MultiPlayRoomSetDraft : FlowNode
  {
    private const int PIN_INPUT_SET_NORMAL = 0;
    private const int PIN_INPUT_SET_DRAFT = 1;
    private const int PIN_OUTPUT_FINISH = 2;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          GlobalVars.IsVersusDraftMode = false;
          break;
        case 1:
          GlobalVars.IsVersusDraftMode = true;
          break;
      }
      this.ActivateOutputLinks(2);
    }
  }
}
