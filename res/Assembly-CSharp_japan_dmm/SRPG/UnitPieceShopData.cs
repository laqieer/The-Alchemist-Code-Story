// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPieceShopData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class UnitPieceShopData
  {
    public string ShopIName { get; private set; }

    public string CostIName { get; private set; }

    public List<UnitPieceShopItem> ShopItems { get; private set; }

    public bool Deserialize(ReqUnitPieceShopItemList.Response json)
    {
      if (json == null)
        return false;
      this.ShopIName = json.shop_iname;
      this.CostIName = json.cost_iname;
      this.ShopItems = new List<UnitPieceShopItem>();
      for (int index = 0; index < json.shopitems.Length; ++index)
      {
        UnitPieceShopItem unitPieceShopItem = new UnitPieceShopItem();
        if (unitPieceShopItem.Deserialize(json.shopitems[index]))
          this.ShopItems.Add(unitPieceShopItem);
      }
      return true;
    }

    public bool Deserialize(ReqUnitPieceShopBuypaid.Response json)
    {
      if (json == null)
        return false;
      this.ShopItems = new List<UnitPieceShopItem>();
      for (int index = 0; index < json.shopitems.Length; ++index)
      {
        UnitPieceShopItem unitPieceShopItem = new UnitPieceShopItem();
        if (unitPieceShopItem.Deserialize(json.shopitems[index]))
          this.ShopItems.Add(unitPieceShopItem);
      }
      return true;
    }
  }
}
