// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopBuyList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class LimitedShopBuyList : MonoBehaviour
  {
    private LimitedShopItem mLimitedShopItem;
    public GameObject amount;
    public GameObject day_reset;
    public GameObject limit;
    public GameObject icon_set;
    public GameObject item_name;
    public GameObject artifact_name;
    public GameObject item_icon;
    public GameObject artifact_icon;
    public GameObject sold_count;
    public GameObject no_limited_price;
    public Text amount_text;

    public LimitedShopItem limitedShopItem
    {
      set
      {
        this.mLimitedShopItem = value;
        this.SetActive(this.day_reset, this.mLimitedShopItem.is_reset);
        this.SetActive(this.limit, !this.mLimitedShopItem.is_reset);
        this.SetActive(this.icon_set, this.mLimitedShopItem.saleType == ESaleType.Coin_P);
        this.SetActive(this.sold_count, !this.limitedShopItem.IsNotLimited);
        this.SetActive(this.no_limited_price, this.limitedShopItem.IsNotLimited);
        this.SetActive(this.item_name, !this.limitedShopItem.IsArtifact);
        this.SetActive(this.artifact_name, this.limitedShopItem.IsArtifact);
        this.SetActive(this.item_icon, !this.limitedShopItem.IsArtifact);
        this.SetActive(this.artifact_icon, this.limitedShopItem.IsArtifact);
        if (!((UnityEngine.Object) this.amount_text != (UnityEngine.Object) null))
          return;
        this.amount_text.text = this.limitedShopItem.remaining_num.ToString();
      }
      get
      {
        return this.mLimitedShopItem;
      }
    }

    public void SetActive(GameObject obj, bool is_active)
    {
      if (!((UnityEngine.Object) obj != (UnityEngine.Object) null))
        return;
      obj.SetActive(is_active);
    }
  }
}
