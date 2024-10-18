// Decompiled with JetBrains decompiler
// Type: AnimateBlockRaycasts
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class AnimateBlockRaycasts : MonoBehaviour
{
  private Animator mAnimator;
  private CanvasGroup mCanvasGroup;
  private int mBlockCount;
  public string[] BlockStates = new string[0];

  private void Start()
  {
    this.mAnimator = ((Component) this).GetComponent<Animator>();
    this.mCanvasGroup = ((Component) this).GetComponent<CanvasGroup>();
    if (!Object.op_Equality((Object) this.mAnimator, (Object) null) && !Object.op_Equality((Object) this.mCanvasGroup, (Object) null))
      return;
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  private void LateUpdate()
  {
    if (this.mAnimator.IsInTransition(0))
    {
      this.mBlockCount = 0;
      this.mCanvasGroup.blocksRaycasts = false;
    }
    else
    {
      for (int index = 0; index < this.BlockStates.Length; ++index)
      {
        AnimatorStateInfo animatorStateInfo = this.mAnimator.GetCurrentAnimatorStateInfo(0);
        if (((AnimatorStateInfo) ref animatorStateInfo).IsName(this.BlockStates[index]))
        {
          this.mCanvasGroup.blocksRaycasts = this.mBlockCount > 0;
          ++this.mBlockCount;
          return;
        }
      }
      this.mBlockCount = 0;
      this.mCanvasGroup.blocksRaycasts = false;
    }
  }
}
