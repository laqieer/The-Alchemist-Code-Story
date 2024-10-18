// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayVersusGradientFade
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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

    public static MultiPlayVersusGradientFade Instance
    {
      get
      {
        return MultiPlayVersusGradientFade.sInstance;
      }
    }

    public bool Fading
    {
      get
      {
        return this.mFading;
      }
    }

    private void Awake()
    {
      if ((UnityEngine.Object) MultiPlayVersusGradientFade.sInstance != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) this);
      MultiPlayVersusGradientFade.sInstance = this;
      this.mAnimator = this.GetComponent<Animator>();
      this.mCanvasGroup = this.GetComponent<CanvasGroup>();
    }

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) MultiPlayVersusGradientFade.sInstance == (UnityEngine.Object) this))
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
      if (!this.mFading || !this.mAnimator.GetCurrentAnimatorStateInfo(0).IsName(this.mStateName))
        return;
      if (this.mStateName == "FadeInFinish")
        this.mCanvasGroup.blocksRaycasts = false;
      this.mFading = false;
    }
  }
}
