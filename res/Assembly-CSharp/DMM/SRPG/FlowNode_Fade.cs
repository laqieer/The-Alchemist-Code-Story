// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Fade
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/Fade", 32741)]
  [FlowNode.Pin(100, "Fade Out", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "Fade In", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1, "Finished", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_Fade : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 100:
          if (!FadeController.InstanceExists)
            FadeController.Instance.FadeTo(Color.clear, 0.0f);
          FadeController.Instance.FadeTo(Color.black, 1f);
          ((Behaviour) this).enabled = true;
          break;
        case 101:
          if (!FadeController.InstanceExists)
            FadeController.Instance.FadeTo(Color.black, 0.0f);
          FadeController.Instance.FadeTo(Color.clear, 1f);
          ((Behaviour) this).enabled = true;
          break;
      }
    }

    private void Update()
    {
      if (FadeController.Instance.IsFading())
        return;
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }
  }
}
