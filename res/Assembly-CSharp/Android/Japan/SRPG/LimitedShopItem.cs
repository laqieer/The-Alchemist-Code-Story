﻿// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

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
      this.start = json.start;
      this.end = json.end;
      this.is_soldout = json.sold > 0;
      if (json.children != null)
      {
        this.children = json.children;
        foreach (Json_ShopItemDesc child in this.children)
        {
          if (child.IsConceptCard)
            MonoSingleton<GameManager>.Instance.Player.SetConceptCardNum(child.iname, child.has_count);
        }
      }
      if (json.children != null)
      {
        this.shopItemType = EShopItemType.Set;
      }
      else
      {
        this.shopItemType = ShopData.String2ShopItemType(json.item.itype);
        if (this.shopItemType == EShopItemType.Unknown)
          this.shopItemType = ShopData.Iname2ShopItemType(json.item.iname);
      }
      if (this.IsConceptCard)
        MonoSingleton<GameManager>.Instance.Player.SetConceptCardNum(this.iname, json.item.has_count);
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
