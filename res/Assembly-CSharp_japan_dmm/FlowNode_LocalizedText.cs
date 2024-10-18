// Decompiled with JetBrains decompiler
// Type: FlowNode_LocalizedText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
[FlowNode.NodeType("Common/LocalizedText", 32741)]
[FlowNode.Pin(0, "Load", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Reload", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(10, "Unload", FlowNode.PinTypes.Input, 2)]
[FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 10)]
public class FlowNode_LocalizedText : FlowNode
{
  public string tableID;

  public override void OnActivate(int pinID)
  {
    if (string.IsNullOrEmpty(this.tableID))
      return;
    switch (pinID)
    {
      case 0:
        LocalizedText.LoadTable(this.tableID);
        break;
      case 1:
        LocalizedText.LoadTable(this.tableID, true);
        break;
      default:
        LocalizedText.UnloadTable(this.tableID);
        break;
    }
    this.ActivateOutputLinks(100);
  }
}
