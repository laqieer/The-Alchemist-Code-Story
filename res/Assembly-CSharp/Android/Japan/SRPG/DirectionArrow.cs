// Decompiled with JetBrains decompiler
// Type: SRPG.DirectionArrow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class DirectionArrow : MonoBehaviour
  {
    [HelpBox("方向の選択状態にあわせてAnimatorのStateNameを変更します (0=Normal,1=Press,2=Hilit,3=Close)。矢印はアニメーションが停止したら破棄されるので、PressとClose状態以外はループアニメーションにしてください。")]
    public string StateName = "state";
    public DirectionArrow.ArrowStates State;
    public EUnitDirection Direction;
    private Animator mAnimator;

    private void Start()
    {
      this.mAnimator = this.GetComponent<Animator>();
    }

    private void Update()
    {
      if ((UnityEngine.Object) this.mAnimator == (UnityEngine.Object) null)
        return;
      this.mAnimator.SetInteger(this.StateName, (int) this.State);
      if (this.mAnimator.GetCurrentAnimatorStateInfo(0).loop || ((double) this.mAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0 || this.mAnimator.IsInTransition(0)))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
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
