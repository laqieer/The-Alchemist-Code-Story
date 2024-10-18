// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardIconBattleResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
      if (!Object.op_Inequality((Object) this.ConceptCardBody, (Object) null))
        return;
      this.ConceptCardBody.SetActive(flag);
      if (!flag)
        return;
      Animator component = this.ConceptCardBody.GetComponent<Animator>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      if (isTrustUp)
        component.SetTrigger("up");
      else
        component.SetTrigger("open");
    }

    public void ShowAnimationAfter()
    {
      bool flag = this.ConceptCard != null;
      if (Object.op_Inequality((Object) this.UnitBody, (Object) null))
        this.UnitBody.SetActive(!flag);
      if (!Object.op_Inequality((Object) this.BlackCover, (Object) null))
        return;
      this.BlackCover.SetActive(!flag);
    }

    public void StartTrustUpAnimation()
    {
      if (Object.op_Equality((Object) this.TrustUpAnimator, (Object) null))
        return;
      this.TrustUpAnimator.Play("up");
    }
  }
}
