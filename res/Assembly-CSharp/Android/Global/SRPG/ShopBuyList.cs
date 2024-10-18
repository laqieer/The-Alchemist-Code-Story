﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ShopBuyList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ShopBuyList : MonoBehaviour
  {
    private ShopItem mShopItem;
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

    public ShopItem ShopItem
    {
      set
      {
        this.mShopItem = value;
        this.SetActive(this.amount, !this.mShopItem.IsSet);
        this.SetActive(this.day_reset, this.mShopItem.is_reset);
        this.SetActive(this.limit, !this.mShopItem.is_reset);
        this.SetActive(this.icon_set, this.mShopItem.saleType == ESaleType.Coin_P);
        this.SetActive(this.sold_count, !this.ShopItem.IsNotLimited);
        this.SetActive(this.no_limited_price, this.ShopItem.IsNotLimited);
        this.SetActive(this.item_name, !this.ShopItem.IsArtifact);
        this.SetActive(this.artifact_name, this.ShopItem.IsArtifact);
        this.SetActive(this.item_icon, !this.ShopItem.IsArtifact);
        this.SetActive(this.artifact_icon, this.ShopItem.IsArtifact);
        if (!((UnityEngine.Object) this.amount_text != (UnityEngine.Object) null))
          return;
        this.amount_text.text = this.mShopItem.remaining_num.ToString();
      }
      get
      {
        return this.mShopItem;
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
