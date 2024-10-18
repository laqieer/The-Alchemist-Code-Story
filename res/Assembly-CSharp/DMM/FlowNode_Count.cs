// Decompiled with JetBrains decompiler
// Type: FlowNode_Count
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
[FlowNode.NodeType("Common/Count", 32741)]
[FlowNode.Pin(1, "Count Up", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(2, "Reset", FlowNode.PinTypes.Input, 2)]
[FlowNode.Pin(100, "Finished", FlowNode.PinTypes.Output, 100)]
public class FlowNode_Count : FlowNode
{
  public int Count = 1;
  private int mCount;
  public bool AutoReset;

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 1:
        ++this.mCount;
        if (this.mCount != this.Count)
          break;
        if (this.AutoReset)
          this.mCount = 0;
        this.ActivateOutputLinks(100);
        break;
      case 2:
        this.mCount = 0;
        break;
    }
  }
}
