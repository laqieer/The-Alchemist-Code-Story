// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_FadeColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
        FadeController.Instance.FadeTo(new Color(this.Color.r, this.Color.g, this.Color.b, 1f - this.Color.a), 0.0f, 0);
      FadeController.Instance.FadeTo(this.Color, this.Time, 0);
      this.enabled = true;
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
