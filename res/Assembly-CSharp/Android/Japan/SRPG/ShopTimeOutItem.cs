// Decompiled with JetBrains decompiler
// Type: SRPG.ShopTimeOutItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
      this.m_ConceptCardIcon.gameObject.SetActive(false);
      if (shop_item.IsItem)
        this.ItemIcon.SetActive(true);
      else if (shop_item.IsArtifact)
        this.ArtifactIcon.SetActive(true);
      else if (shop_item.IsConceptCard)
        this.m_ConceptCardIcon.gameObject.SetActive(true);
      else
        this.ItemIcon.SetActive(true);
      this.Amount.text = shop_item.num.ToString();
      this.Name.text = name;
    }
  }
}
