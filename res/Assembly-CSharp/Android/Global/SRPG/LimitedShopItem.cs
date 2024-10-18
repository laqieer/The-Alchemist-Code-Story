﻿// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class LimitedShopItem : ShopItem
  {
    public bool Deserialize(JSON_LimitedShopItemListSet json)
    {
      if (json == null || json.item == null || (string.IsNullOrEmpty(json.item.iname) || json.cost == null) || string.IsNullOrEmpty(json.cost.type))
        return false;
      this.id = json.id;
      this.iname = json.item.iname;
      this.num = json.item.num;
      this.max_num = json.item.maxnum;
      this.bougthnum = json.item.boughtnum;
      this.saleValue = json.cost.value;
      this.saleType = ShopData.String2SaleType(json.cost.type);
      this.is_reset = json.isreset == 1;
      this.is_soldout = json.sold > 0;
      if (json.children != null)
        this.children = json.children;
      return true;
    }

    public void SetShopItem(ShopItem shop_item)
    {
      this.id = shop_item.id;
      this.iname = shop_item.iname;
      this.is_soldout = shop_item.is_soldout;
      this.num = shop_item.num;
      this.saleType = shop_item.saleType;
      this.saleValue = shop_item.saleValue;
    }
  }
}
