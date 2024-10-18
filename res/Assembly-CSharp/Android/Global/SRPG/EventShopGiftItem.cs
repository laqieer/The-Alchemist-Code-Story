// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopGiftItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EventShopGiftItem : MonoBehaviour
  {
    public Text Amount;
    public Text Name;
    public GameObject ItemIcon;
    public GameObject ArtifactIcon;

    public void SetShopItemInfo(Json_ShopItemDesc shop_item_desc, string name)
    {
      this.ItemIcon.SetActive(!shop_item_desc.IsArtifact);
      this.ArtifactIcon.SetActive(shop_item_desc.IsArtifact);
      this.Amount.text = shop_item_desc.num.ToString();
      this.Name.text = name;
    }
  }
}
