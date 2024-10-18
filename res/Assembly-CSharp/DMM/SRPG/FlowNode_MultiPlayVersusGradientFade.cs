// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayVersusGradientFade
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/Versus/GradientFade", 32741)]
  [FlowNode.Pin(1, "Fade In", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Fade Out", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "Finish", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_MultiPlayVersusGradientFade : FlowNode
  {
    private const int PIN_IN_FADE_IN = 1;
    private const int PIN_IN_FADE_OUT = 2;
    private const int PIN_OUT_FINISH = 10;
    private bool mFading;

    public override void OnActivate(int pinID)
    {
      MultiPlayVersusGradientFade instance = MultiPlayVersusGradientFade.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
      {
        DebugUtility.Log("MultiPlayVersus専用です");
      }
      else
      {
        if (pinID == 1)
          instance.FadeIn();
        else
          instance.FadeOut();
        ((Behaviour) this).enabled = true;
        this.mFading = true;
      }
    }

    private void Update()
    {
      if (!this.mFading)
        return;
      MultiPlayVersusGradientFade instance = MultiPlayVersusGradientFade.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
      {
        this.mFading = false;
        this.ActivateOutputLinks(10);
        ((Behaviour) this).enabled = false;
      }
      else
      {
        if (instance.Fading)
          return;
        this.mFading = false;
        this.ActivateOutputLinks(10);
        ((Behaviour) this).enabled = false;
      }
    }
  }
}
