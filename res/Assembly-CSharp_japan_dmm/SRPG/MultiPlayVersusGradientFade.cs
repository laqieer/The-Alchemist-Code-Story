// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayVersusGradientFade
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class MultiPlayVersusGradientFade : MonoBehaviour
  {
    private static MultiPlayVersusGradientFade sInstance;
    private const string STATE_FADE_IN = "FadeIn";
    private const string STATE_FADE_OUT = "FadeOut";
    private const string STATE_FADE_IN_FINISH = "FadeInFinish";
    private const string STATE_FADE_OUT_FINISH = "FadeOutFinish";
    private bool mFading;
    private Animator mAnimator;
    private CanvasGroup mCanvasGroup;
    private string mStateName;

    public static MultiPlayVersusGradientFade Instance => MultiPlayVersusGradientFade.sInstance;

    public bool Fading => this.mFading;

    private void Awake()
    {
      if (Object.op_Inequality((Object) MultiPlayVersusGradientFade.sInstance, (Object) null))
        Object.Destroy((Object) this);
      MultiPlayVersusGradientFade.sInstance = this;
      this.mAnimator = ((Component) this).GetComponent<Animator>();
      this.mCanvasGroup = ((Component) this).GetComponent<CanvasGroup>();
    }

    private void OnDestroy()
    {
      if (!Object.op_Equality((Object) MultiPlayVersusGradientFade.sInstance, (Object) this))
        return;
      MultiPlayVersusGradientFade.sInstance = (MultiPlayVersusGradientFade) null;
    }

    public void FadeIn()
    {
      if (this.mFading)
        return;
      this.mAnimator.Play(nameof (FadeIn));
      this.mStateName = "FadeInFinish";
      this.mFading = true;
    }

    public void FadeOut()
    {
      if (this.mFading)
        return;
      this.mCanvasGroup.blocksRaycasts = true;
      this.mAnimator.Play(nameof (FadeOut));
      this.mStateName = "FadeOutFinish";
      this.mFading = true;
    }

    private void Update()
    {
      if (!this.mFading)
        return;
      AnimatorStateInfo animatorStateInfo = this.mAnimator.GetCurrentAnimatorStateInfo(0);
      if (!((AnimatorStateInfo) ref animatorStateInfo).IsName(this.mStateName))
        return;
      if (this.mStateName == "FadeInFinish")
        this.mCanvasGroup.blocksRaycasts = false;
      this.mFading = false;
    }
  }
}
