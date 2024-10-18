// Decompiled with JetBrains decompiler
// Type: SRPG.SkillSplash
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (Animator))]
  public class SkillSplash : MonoBehaviour
  {
    [Description("スプラッシュ表示を閉じるのに使用するトリガーの名前")]
    public string EndTrigger = "end";
    [Description("スプラッシュ表示が閉じられた状態のステートの名前、この状態になると先へ進みます")]
    public string EndStateName = "finished";
    public bool NoLoop;
    public RawImage[] Chara_01_Images;
    public RawImage[] Chara_02_Images;
    private Animator mAnimator;
    private bool mClose;

    public void SetCharaImages(Texture2D newTexture1, Texture2D newTexture2)
    {
      for (int index = 0; index < this.Chara_01_Images.Length; ++index)
        this.Chara_01_Images[index].texture = (Texture) newTexture1;
      for (int index = 0; index < this.Chara_02_Images.Length; ++index)
        this.Chara_02_Images[index].texture = (Texture) newTexture2;
    }

    public void Close()
    {
      this.mClose = true;
    }

    private void Start()
    {
      this.mAnimator = this.GetComponent<Animator>();
      if (!this.NoLoop)
        return;
      this.Close();
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
