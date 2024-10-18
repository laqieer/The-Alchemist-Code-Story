// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_FadeColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/Fade (Color)", 32741)]
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Finished", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_FadeColor : FlowNode
  {
    public Color Color = Color.clear;
    public float Time = 1f;
    public bool ForceReset;

    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (!FadeController.InstanceExists || this.ForceReset)
        FadeController.Instance.FadeTo(new Color(this.Color.r, this.Color.g, this.Color.b, 1f - this.Color.a), 0.0f);
      FadeController.Instance.FadeTo(this.Color, this.Time);
      ((Behaviour) this).enabled = true;
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
