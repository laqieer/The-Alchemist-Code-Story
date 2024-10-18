// Decompiled with JetBrains decompiler
// Type: SRPG.DirectionArrow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class DirectionArrow : MonoBehaviour
  {
    public DirectionArrow.ArrowStates State;
    public EUnitDirection Direction;
    private Animator mAnimator;
    [HelpBox("方向の選択状態にあわせてAnimatorのStateNameを変更します (0=Normal,1=Press,2=Hilit,3=Close)。矢印はアニメーションが停止したら破棄されるので、PressとClose状態以外はループアニメーションにしてください。")]
    public string StateName = "state";

    private void Start() => this.mAnimator = ((Component) this).GetComponent<Animator>();

    private void Update()
    {
      if (Object.op_Equality((Object) this.mAnimator, (Object) null))
        return;
      this.mAnimator.SetInteger(this.StateName, (int) this.State);
      AnimatorStateInfo animatorStateInfo1 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
      if (((AnimatorStateInfo) ref animatorStateInfo1).loop)
        return;
      AnimatorStateInfo animatorStateInfo2 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
      if ((double) ((AnimatorStateInfo) ref animatorStateInfo2).normalizedTime < 1.0 || this.mAnimator.IsInTransition(0))
        return;
      Object.Destroy((Object) ((Component) this).gameObject);
    }

    public enum ArrowStates
    {
      Normal,
      Press,
      Hilit,
      Close,
    }
  }
}
