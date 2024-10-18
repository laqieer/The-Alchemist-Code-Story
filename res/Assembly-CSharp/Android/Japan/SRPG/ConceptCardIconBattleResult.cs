// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardIconBattleResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ConceptCardIconBattleResult : ConceptCardIcon
  {
    [SerializeField]
    private GameObject UnitBody;
    [SerializeField]
    private GameObject BlackCover;
    [SerializeField]
    private GameObject ConceptCardBody;
    [SerializeField]
    private Animator TrustUpAnimator;

    public void ShowStartAnimation(bool isTrustUp)
    {
      bool flag = this.ConceptCard != null;
      if (!((UnityEngine.Object) this.ConceptCardBody != (UnityEngine.Object) null))
        return;
      this.ConceptCardBody.SetActive(flag);
      if (!flag)
        return;
      Animator component = this.ConceptCardBody.GetComponent<Animator>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      if (isTrustUp)
        component.SetTrigger("up");
      else
        component.SetTrigger("open");
    }

    public void ShowAnimationAfter()
    {
      bool flag = this.ConceptCard != null;
      if ((UnityEngine.Object) this.UnitBody != (UnityEngine.Object) null)
        this.UnitBody.SetActive(!flag);
      if (!((UnityEngine.Object) this.BlackCover != (UnityEngine.Object) null))
        return;
      this.BlackCover.SetActive(!flag);
    }

    public void StartTrustUpAnimation()
    {
      if ((UnityEngine.Object) this.TrustUpAnimator == (UnityEngine.Object) null)
        return;
      this.TrustUpAnimator.Play("up");
    }
  }
}
