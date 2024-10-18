// Decompiled with JetBrains decompiler
// Type: SRPG.SkillSplash
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class SkillSplash : MonoBehaviour
  {
    [SerializeField]
    private Animator mAnimator;
    public bool NoLoop;
    [Description("スプラッシュ表示を閉じるのに使用するトリガーの名前")]
    public string EndTrigger = "end";
    [Description("スプラッシュ表示が閉じられた状態のステートの名前、この状態になると先へ進みます")]
    public string EndStateName = "finished";
    public RawImage[] Chara_01_Images;
    public RawImage[] Chara_02_Images;
    private bool mClose;

    public void SetCharaImages(Texture2D newTexture1, Texture2D newTexture2)
    {
      if (Object.op_Inequality((Object) newTexture1, (Object) null))
      {
        for (int index = 0; index < this.Chara_01_Images.Length; ++index)
          this.Chara_01_Images[index].texture = (Texture) newTexture1;
      }
      if (!Object.op_Inequality((Object) newTexture2, (Object) null))
        return;
      for (int index = 0; index < this.Chara_02_Images.Length; ++index)
        this.Chara_02_Images[index].texture = (Texture) newTexture2;
    }

    public void Close() => this.mClose = true;

    private void Start()
    {
      if (Object.op_Equality((Object) this.mAnimator, (Object) null))
        this.mAnimator = ((Component) this).GetComponent<Animator>();
      if (!this.NoLoop)
        return;
      this.Close();
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
