// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayVersusGradientFade
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
      {
        DebugUtility.Log("MultiPlayVersus専用です");
      }
      else
      {
        if (pinID == 1)
          instance.FadeIn();
        else
          instance.FadeOut();
        this.enabled = true;
        this.mFading = true;
      }
    }

    private void Update()
    {
      if (!this.mFading)
        return;
      MultiPlayVersusGradientFade instance = MultiPlayVersusGradientFade.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
      {
        this.mFading = false;
        this.ActivateOutputLinks(10);
        this.enabled = false;
      }
      else
      {
        if (instance.Fading)
          return;
        this.mFading = false;
        this.ActivateOutputLinks(10);
        this.enabled = false;
      }
    }
  }
}
