// Decompiled with JetBrains decompiler
// Type: FlowNode_Startup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

[FlowNode.NodeType("Event/Startup", 58751)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 0)]
public class FlowNode_Startup : FlowNodePersistent
{
  private void Start()
  {
    this.ActivateOutputLinks(1);
    this.enabled = false;
  }
}
