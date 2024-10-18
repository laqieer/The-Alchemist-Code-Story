// Decompiled with JetBrains decompiler
// Type: SRPG.SkillSplashCollabo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class SkillSplashCollabo : MonoBehaviour
  {
    [Description("スプラッシュ表示を閉じるのに使用するトリガーの名前")]
    public string EndTrigger = "end";
    [Description("スプラッシュ表示が閉じられた状態のステートの名前、この状態になると先へ進みます")]
    public string EndStateName = "finished";
    public RawImage[] UnitImages2_main_Images;
    public RawImage[] UnitImages2_sub_Images;
    public RawImage[] UnitEyeImages_main_Images;
    public RawImage[] UnitEyeImages_sub_Images;
    [SerializeField]
    private Animator mAnimator;
    private bool mClose = true;

    public void SetCharaImages(
      Texture2D u2_main_tex,
      Texture2D u2_sub_tex,
      Texture2D ue_main_tex,
      Texture2D ue_sub_tex)
    {
      for (int index = 0; index < this.UnitImages2_main_Images.Length; ++index)
        this.UnitImages2_main_Images[index].texture = (Texture) u2_main_tex;
      for (int index = 0; index < this.UnitImages2_sub_Images.Length; ++index)
        this.UnitImages2_sub_Images[index].texture = (Texture) u2_sub_tex;
      for (int index = 0; index < this.UnitEyeImages_main_Images.Length; ++index)
        this.UnitEyeImages_main_Images[index].texture = (Texture) ue_main_tex;
      for (int index = 0; index < this.UnitEyeImages_sub_Images.Length; ++index)
        this.UnitEyeImages_sub_Images[index].texture = (Texture) ue_sub_tex;
    }

    public void Close() => this.mClose = true;

    private void Start()
    {
      if (!Object.op_Equality((Object) this.mAnimator, (Object) null))
        return;
      this.mAnimator = ((Component) this).GetComponent<Animator>();
    }

    private void Update()
    {
      if (this.mClose)
        this.mAnimator.SetTrigger(this.EndTrigger);
      AnimatorStateInfo animatorStateInfo1 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
      if (!((AnimatorStateInfo) ref animatorStateInfo1).IsName(this.EndStateName) || this.mAnimator.IsInTransition(0))
        return;
      AnimatorStateInfo animatorStateInfo2 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
      if ((double) ((AnimatorStateInfo) ref animatorStateInfo2).normalizedTime < 1.0)
        return;
      Object.Destroy((Object) ((Component) this).gameObject);
    }
  }
}
