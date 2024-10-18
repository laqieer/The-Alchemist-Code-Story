// Decompiled with JetBrains decompiler
// Type: SRPG.ShopTimeOutItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ShopTimeOutItem : MonoBehaviour
  {
    public Text Amount;
    public Text Name;
    public GameObject ItemIcon;
    public GameObject ArtifactIcon;
    public ConceptCardIcon m_ConceptCardIcon;

    public void SetShopItemInfo(ShopItem shop_item, string name)
    {
      this.ItemIcon.SetActive(false);
      this.ArtifactIcon.SetActive(false);
      ((Component) this.m_ConceptCardIcon).gameObject.SetActive(false);
      if (shop_item.IsItem)
        this.ItemIcon.SetActive(true);
      else if (shop_item.IsArtifact)
        this.ArtifactIcon.SetActive(true);
      else if (shop_item.IsConceptCard)
        ((Component) this.m_ConceptCardIcon).gameObject.SetActive(true);
      else
        this.ItemIcon.SetActive(true);
      this.Amount.text = shop_item.num.ToString();
      this.Name.text = name;
    }
  }
}
