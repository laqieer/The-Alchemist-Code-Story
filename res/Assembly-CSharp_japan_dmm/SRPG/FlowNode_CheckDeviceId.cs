// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckDeviceId
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("SRPG/CheckDeviceId", 32741)]
  [FlowNode.Pin(101, "Start", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(1001, "Enable", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(1002, "Disable", FlowNode.PinTypes.Output, 1002)]
  public class FlowNode_CheckDeviceId : FlowNode
  {
    private const int PIN_INPUT_START = 101;
    private const int PIN_OUTPUT_ENABLE = 1001;
    private const int PIN_OUTPUT_DISABLE = 1002;

    public override void OnActivate(int pinID)
    {
      if (pinID != 101)
        return;
      this.Check();
    }

    private void Check()
    {
      MonoSingleton<GameManager>.Instance.InitAuth();
      this.ActivateOutputLinks(!MonoSingleton<GameManager>.Instance.IsDeviceId() ? 1002 : 1001);
    }
  }
}
