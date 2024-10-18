// Decompiled with JetBrains decompiler
// Type: FlowNode_LocalizedText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

[FlowNode.NodeType("Common/LocalizedText", 32741)]
[FlowNode.Pin(0, "Load", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(10, "Unload", FlowNode.PinTypes.Input, 0)]
public class FlowNode_LocalizedText : FlowNode
{
  public string tableID;

  public override void OnActivate(int pinID)
  {
    if (string.IsNullOrEmpty(this.tableID))
      return;
    if (pinID == 0)
    {
      LocalizedText.LoadTable(this.tableID, false);
    }
    else
    {
      if (pinID != 1)
        return;
      LocalizedText.UnloadTable(this.tableID);
    }
  }
}
