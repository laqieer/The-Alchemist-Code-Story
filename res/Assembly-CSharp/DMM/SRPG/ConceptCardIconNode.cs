// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardIconNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardIconNode : ContentNode
  {
    [SerializeField]
    public ConceptCardIcon Icon;
    [SerializeField]
    public GameObject EmptyObject;
    [SerializeField]
    public GameObject DecreaseEffect;
    [SerializeField]
    public Text DecreaseEffectText;

    public void Setup(ConceptCardData cc_data)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null) || cc_data == null)
        return;
      this.Icon.Setup(cc_data);
      ((Component) this.Icon).gameObject.SetActive(true);
    }

    public void SetNotSellFlag(bool not_sale)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null))
        return;
      this.Icon.SetNotSellFlag(not_sale);
    }

    public void Empty(bool is_enmpty)
    {
      if (Object.op_Equality((Object) this.EmptyObject, (Object) null) || Object.op_Equality((Object) this.Icon, (Object) null))
        return;
      ((Component) this.Icon).gameObject.SetActive(!is_enmpty);
      this.EmptyObject.SetActive(is_enmpty);
    }

    public void Enable(bool enable)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null) || !((Component) this.Icon).gameObject.activeSelf)
        return;
      this.Icon.RefreshEnableParam(enable);
    }

    public void Select(bool select)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null) || !((Component) this.Icon).gameObject.activeSelf)
        return;
      this.Icon.RefreshSelectParam(select);
    }

    public void Recommend(bool is_recommend)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null) || !((Component) this.Icon).gameObject.activeSelf)
        return;
      this.Icon.SetRecommendFlag(is_recommend);
    }

    public void SetDecreaseEffectActive(bool isEnableDecreaseEffect, int decreaseEffectRate)
    {
      if (Object.op_Equality((Object) this.DecreaseEffect, (Object) null))
        return;
      if (isEnableDecreaseEffect)
      {
        if (!this.DecreaseEffect.activeSelf)
          this.DecreaseEffect.SetActive(true);
        ConceptCardUtility.SetDecreaseEffectRateText(this.DecreaseEffectText, decreaseEffectRate);
      }
      else
      {
        if (!this.DecreaseEffect.activeSelf)
          return;
        this.DecreaseEffect.SetActive(false);
      }
    }
  }
}
