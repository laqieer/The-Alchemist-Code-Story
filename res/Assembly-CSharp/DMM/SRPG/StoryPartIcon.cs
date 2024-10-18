// Decompiled with JetBrains decompiler
// Type: SRPG.StoryPartIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class StoryPartIcon : MonoBehaviour
  {
    private const string ANIMATION_RELEASE_NAME = "open";
    private StoryPartIcon.EndUnlockAnimStoryPart OnEndUnlockAnimation;
    [SerializeField]
    private GameObject MainIcon;
    [SerializeField]
    private Animator UnlockAnimator;
    [SerializeField]
    private Image LockCover;
    private bool mLockFlag;
    private bool mIsPlayingUnlockAnim;

    public bool Setup(bool unlock_flag, eStoryPart story_part, bool is_unlock_anim)
    {
      bool flag = !unlock_flag;
      if (is_unlock_anim)
        flag = true;
      if (Object.op_Equality((Object) this.MainIcon, (Object) null))
        return false;
      this.mLockFlag = flag;
      if (!flag)
      {
        this.MainIcon.SetActive(true);
        GameUtility.SetGameObjectActive((Component) this.LockCover, false);
      }
      else
      {
        this.MainIcon.SetActive(false);
        GameUtility.SetGameObjectActive((Component) this.LockCover, true);
      }
      return true;
    }

    public bool PlayUnlockAnim(StoryPartIcon.EndUnlockAnimStoryPart callback)
    {
      if (!this.mLockFlag || Object.op_Equality((Object) this.UnlockAnimator, (Object) null))
        return false;
      this.mIsPlayingUnlockAnim = true;
      this.OnEndUnlockAnimation = callback;
      ((Component) this.UnlockAnimator).gameObject.SetActive(true);
      this.UnlockAnimator.Play("open");
      return true;
    }

    private void Update()
    {
      if (!this.mIsPlayingUnlockAnim || this.IsPlayingReleaseAnim())
        return;
      this.mIsPlayingUnlockAnim = false;
      this.OnEndUnlockAnimation();
      this.ReleaseIcon();
    }

    private bool IsPlayingReleaseAnim()
    {
      if (!this.mLockFlag)
        return false;
      bool flag = false;
      if (Object.op_Inequality((Object) this.UnlockAnimator, (Object) null))
      {
        AnimatorStateInfo animatorStateInfo = this.UnlockAnimator.GetCurrentAnimatorStateInfo(0);
        if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime < 1.0)
          flag = true;
      }
      return flag;
    }

    private void ReleaseIcon()
    {
      this.MainIcon.SetActive(true);
      GameUtility.SetGameObjectActive((Component) this.LockCover, false);
      GameUtility.SetGameObjectActive((Component) this.UnlockAnimator, false);
      this.mLockFlag = false;
    }

    public delegate void EndUnlockAnimStoryPart();
  }
}
