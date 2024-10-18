// Decompiled with JetBrains decompiler
// Type: FlowNode_TrophyUpdateProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using SRPG;

[FlowNode.NodeType("Trophy/TrophyUpdateProgress", 32741)]
[FlowNode.Pin(0, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
[FlowNode.Pin(10, "Enabled", FlowNode.PinTypes.Input, 10)]
[FlowNode.Pin(11, "Disable", FlowNode.PinTypes.Input, 11)]
public class FlowNode_TrophyUpdateProgress : FlowNode
{
  private bool m_IsEnable = true;
  private const int INPUT_PIN = 0;
  private const int OUTPUT_PIN = 1;
  private const int ENABLED_PIN = 10;
  private const int DISABLE_PIN = 11;

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 0:
        if (this.m_IsEnable)
          MonoSingleton<GameManager>.Instance.Player.TrophyUpdateProgress();
        this.ActivateOutputLinks(1);
        break;
      case 10:
        this.m_IsEnable = true;
        break;
      case 11:
        this.m_IsEnable = false;
        break;
    }
  }
}
