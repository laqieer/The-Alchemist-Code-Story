// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DrawCardCharacterMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("DrawCard/Character/Message", 32741)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Hidden", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_DrawCardCharacterMessage : FlowNode
  {
    private const int PIN_IN_MESS_SHOW = 1;
    private const int PIN_IN_MESS_HIDDEN = 2;
    private const int PIN_OUT_END = 10;
    [FlowNode.ShowInInfo]
    public string Message = "Empty";

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 1:
          DrawCardCharacterMessage.ShowMessage(this.Message);
          break;
        case 2:
          DrawCardCharacterMessage.HiddenMessage();
          break;
      }
      this.ActivateOutputLinks(10);
    }
  }
}
