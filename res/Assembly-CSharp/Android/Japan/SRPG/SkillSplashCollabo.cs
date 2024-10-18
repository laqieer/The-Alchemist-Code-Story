// Decompiled with JetBrains decompiler
// Type: SRPG.SkillSplashCollabo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class SkillSplashCollabo : MonoBehaviour
  {
    [Description("スプラッシュ表示を閉じるのに使用するトリガーの名前")]
    public string EndTrigger = "end";
    [Description("スプラッシュ表示が閉じられた状態のステートの名前、この状態になると先へ進みます")]
    public string EndStateName = "finished";
    private bool mClose = true;
    public RawImage[] UnitImages2_main_Images;
    public RawImage[] UnitImages2_sub_Images;
    public RawImage[] UnitEyeImages_main_Images;
    public RawImage[] UnitEyeImages_sub_Images;
    [SerializeField]
    private Animator mAnimator;

    public void SetCharaImages(Texture2D u2_main_tex, Texture2D u2_sub_tex, Texture2D ue_main_tex, Texture2D ue_sub_tex)
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

    public void Close()
    {
      this.mClose = true;
    }

    private void Start()
    {
      if (!((UnityEngine.Object) this.mAnimator == (UnityEngine.Object) null))
        return;
      this.mAnimator = this.GetComponent<Animator>();
    }

    private void Update()
    {
      if (this.mClose)
        this.mAnimator.SetTrigger(this.EndTrigger);
      if (!this.mAnimator.GetCurrentAnimatorStateInfo(0).IsName(this.EndStateName) || this.mAnimator.IsInTransition(0) || (double) this.mAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0)
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    }
  }
}
