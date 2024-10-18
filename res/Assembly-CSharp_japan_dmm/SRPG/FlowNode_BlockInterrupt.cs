// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BlockInterrupt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("BlockInterrupt", 32741)]
  [FlowNode.Pin(0, "Destroy", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(5, "Create", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "OnDestroy", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(20, "OnCreate", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_BlockInterrupt : FlowNode
  {
    public BlockInterrupt.EType Type;
    private BlockInterrupt mBlock;

    protected override void OnDestroy()
    {
      if (this.mBlock != null)
      {
        this.mBlock.Destroy();
        this.mBlock = (BlockInterrupt) null;
      }
      base.OnDestroy();
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if (this.mBlock != null)
          {
            this.mBlock.Destroy();
            this.mBlock = (BlockInterrupt) null;
          }
          this.ActivateOutputLinks(10);
          break;
        case 5:
          if (this.mBlock == null)
            this.mBlock = BlockInterrupt.Create(this.Type);
          this.ActivateOutputLinks(20);
          break;
      }
    }
  }
}
