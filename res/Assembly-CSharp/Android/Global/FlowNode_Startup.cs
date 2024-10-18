// Decompiled with JetBrains decompiler
// Type: FlowNode_Startup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

[FlowNode.NodeType("Event/Startup", 58751)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 0)]
public class FlowNode_Startup : FlowNodePersistent
{
  private void Start()
  {
    this.ActivateOutputLinks(1);
    this.enabled = false;
  }

  public override void OnActivate(int pinID)
  {
  }
}
