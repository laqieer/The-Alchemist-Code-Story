// Decompiled with JetBrains decompiler
// Type: FlowNode_Delay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[FlowNode.NodeType("Common/Delay", 32741)]
[FlowNode.Pin(10, "Start", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(11, "Cancel", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(1, "Finished", FlowNode.PinTypes.Output, 2)]
[FlowNode.Pin(2, "Cancelled", FlowNode.PinTypes.Output, 3)]
public class FlowNode_Delay : FlowNode
{
  public float Timer = 1f;
  private float mTimer;
  public bool UnscaledTime;

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 10:
        if ((double) this.Timer <= 0.0)
        {
          this.ActivateOutputLinks(1);
          break;
        }
        this.mTimer = 0.0f;
        ((Behaviour) this).enabled = true;
        break;
      case 11:
        ((Behaviour) this).enabled = false;
        this.ActivateOutputLinks(2);
        break;
    }
  }

  private void Update()
  {
    if (this.UnscaledTime)
      this.mTimer += Time.unscaledDeltaTime;
    else
      this.mTimer += Time.deltaTime;
    if ((double) this.mTimer < (double) this.Timer)
      return;
    ((Behaviour) this).enabled = false;
    this.ActivateOutputLinks(1);
  }
}
