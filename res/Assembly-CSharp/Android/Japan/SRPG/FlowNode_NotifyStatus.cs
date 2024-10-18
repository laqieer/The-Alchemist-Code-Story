// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NotifyStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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

    public void NotifyEnable()
    {
      NotifyList.mNotifyEnable = true;
    }

    public void NotifyDisable()
    {
      NotifyList.mNotifyEnable = false;
    }
  }
}
