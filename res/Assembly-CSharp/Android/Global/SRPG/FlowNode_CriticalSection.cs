// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CriticalSection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(10, "Out", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "Wait", FlowNode.PinTypes.Input, 2)]
  [FlowNode.NodeType("System/クリティカルセクション", 16711680)]
  [FlowNode.Pin(11, "Finished", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(0, "Enter", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Leave", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_CriticalSection : FlowNode
  {
    [BitMask]
    public CriticalSections Mask = CriticalSections.Default;
    private const int PINID_ENTER = 0;
    private const int PINID_LEAVE = 1;
    private const int PINID_WAIT = 2;
    private const int PINID_OUT = 10;
    private const int PINID_FINISHED = 11;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          CriticalSection.Enter(this.Mask);
          this.ActivateOutputLinks(10);
          break;
        case 1:
          CriticalSection.Leave(this.Mask);
          this.ActivateOutputLinks(10);
          break;
        case 2:
          this.enabled = true;
          break;
      }
    }

    private void Update()
    {
      if (CriticalSection.IsActive)
        return;
      this.enabled = false;
      this.ActivateOutputLinks(11);
    }
  }
}
