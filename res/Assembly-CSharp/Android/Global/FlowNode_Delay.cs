// Decompiled with JetBrains decompiler
// Type: FlowNode_Delay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Delay", 32741)]
[FlowNode.Pin(1, "Finished", FlowNode.PinTypes.Output, 2)]
[FlowNode.Pin(2, "Cancelled", FlowNode.PinTypes.Output, 3)]
[FlowNode.Pin(10, "Start", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(11, "Cancel", FlowNode.PinTypes.Input, 1)]
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
        this.enabled = true;
        break;
      case 11:
        this.enabled = false;
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
    this.enabled = false;
    this.ActivateOutputLinks(1);
  }
}
