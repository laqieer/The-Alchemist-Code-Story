// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopGiftItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EventShopGiftItem : MonoBehaviour
  {
    public Text Amount;
    public Text Name;
    public GameObject ItemIcon;
    public GameObject ArtifactIcon;
    public ConceptCardIcon m_ConceptCardIcon;

    public void SetShopItemInfo(Json_ShopItemDesc shop_item_desc, string name, int amount)
    {
      this.ItemIcon.SetActive(false);
      this.ArtifactIcon.SetActive(false);
      ((Component) this.m_ConceptCardIcon).gameObject.SetActive(false);
      if (shop_item_desc.IsItem)
        this.ItemIcon.SetActive(true);
      else if (shop_item_desc.IsArtifact)
        this.ArtifactIcon.SetActive(true);
      else if (shop_item_desc.IsConceptCard)
        ((Component) this.m_ConceptCardIcon).gameObject.SetActive(true);
      this.Amount.text = (shop_item_desc.num * amount).ToString();
      this.Name.text = name;
    }
  }
}
