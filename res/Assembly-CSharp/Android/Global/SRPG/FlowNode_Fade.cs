// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Fade
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(101, "Fade In", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1, "Finished", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("UI/Fade", 32741)]
  [FlowNode.Pin(100, "Fade Out", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_Fade : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 100:
          if (!FadeController.InstanceExists)
            FadeController.Instance.FadeTo(Color.clear, 0.0f, 0);
          FadeController.Instance.FadeTo(Color.black, 1f, 0);
          this.enabled = true;
          break;
        case 101:
          if (!FadeController.InstanceExists)
            FadeController.Instance.FadeTo(Color.black, 0.0f, 0);
          FadeController.Instance.FadeTo(Color.clear, 1f, 0);
          this.enabled = true;
          break;
      }
    }

    private void Update()
    {
      if (FadeController.Instance.IsFading(0))
        return;
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }
  }
}
