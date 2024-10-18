// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NotifyStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Notify/Status", 32741)]
  [FlowNode.Pin(0, "Enable", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Disable", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "output", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_NotifyStatus : FlowNode
  {
    private const int PIN_ENABLE = 0;
    private const int PIN_DISABLE = 1;
    private const int PIN_OUTPUT = 10;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.NotifyEnable();
          break;
        case 1:
          this.NotifyDisable();
          break;
      }
      this.ActivateOutputLinks(10);
    }

    public void NotifyEnable() => NotifyList.mNotifyEnable = true;

    public void NotifyDisable() => NotifyList.mNotifyEnable = false;
  }
}
