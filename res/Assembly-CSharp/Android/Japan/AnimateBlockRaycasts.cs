// Decompiled with JetBrains decompiler
// Type: AnimateBlockRaycasts
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AnimateBlockRaycasts : MonoBehaviour
{
  public string[] BlockStates = new string[0];
  private Animator mAnimator;
  private CanvasGroup mCanvasGroup;
  private int mBlockCount;

  private void Start()
  {
    this.mAnimator = this.GetComponent<Animator>();
    this.mCanvasGroup = this.GetComponent<CanvasGroup>();
    if (!((Object) this.mAnimator == (Object) null) && !((Object) this.mCanvasGroup == (Object) null))
      return;
    Object.Destroy((Object) this.gameObject);
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
        if (this.mAnimator.GetCurrentAnimatorStateInfo(0).IsName(this.BlockStates[index]))
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
