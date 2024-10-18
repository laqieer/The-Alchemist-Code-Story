﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class EventShopItem : ShopItem
  {
    public string cost_iname;
    public bool update_type;
    private JSON_EventShopItemListSet.Cost cost;

    public bool Deserialize(JSON_EventShopItemListSet json)
    {
      if (json == null || json.item == null || string.IsNullOrEmpty(json.item.iname) || json.cost == null || string.IsNullOrEmpty(json.cost.type))
        return false;
      this.id = json.id;
      this.iname = json.item.iname;
      this.num = json.item.num;
      this.max_num = json.item.maxnum;
      this.bougthnum = json.item.boughtnum;
      this.saleValue = json.cost.value;
      this.saleType = ShopData.String2SaleType(json.cost.type);
      this.cost_iname = json.cost.iname == null ? GlobalVars.EventShopItem.shop_cost_iname : json.cost.iname;
      if (this.saleType == ESaleType.EventCoin && this.cost_iname == null)
        return false;
      this.is_reset = json.isreset == 1;
      this.start = json.start;
      this.end = json.end;
      this.is_soldout = json.sold > 0;
      this.step = json.item.step;
      this.children = (Json_ShopItemDesc[]) null;
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
