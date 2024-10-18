// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopGiftItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class LimitedShopGiftItem : MonoBehaviour
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
      this.m_ConceptCardIcon.gameObject.SetActive(false);
      if (shop_item_desc.IsItem)
        this.ItemIcon.SetActive(true);
      else if (shop_item_desc.IsArtifact)
        this.ArtifactIcon.SetActive(true);
      else if (shop_item_desc.IsConceptCard)
        this.m_ConceptCardIcon.gameObject.SetActive(true);
      this.Amount.text = (shop_item_desc.num * amount).ToString();
      this.Name.text = name;
    }
  }
}
