// Decompiled with JetBrains decompiler
// Type: SRPG.ShopData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ShopData
  {
    public List<ShopItem> items = new List<ShopItem>();
    public int UpdateCount;
    public bool btn_update;

    public static ESaleType String2SaleType(string type)
    {
      if (type != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (ShopData.\u003C\u003Ef__switch\u0024map1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ShopData.\u003C\u003Ef__switch\u0024map1 = new Dictionary<string, int>(8)
          {
            {
              "gold",
              0
            },
            {
              "coin",
              1
            },
            {
              "coin_p",
              2
            },
            {
              "tc",
              3
            },
            {
              "ac",
              4
            },
            {
              "ec",
              5
            },
            {
              "pp",
              6
            },
            {
              "mc",
              7
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (ShopData.\u003C\u003Ef__switch\u0024map1.TryGetValue(type, out num))
        {
          switch (num)
          {
            case 0:
              return ESaleType.Gold;
            case 1:
              return ESaleType.Coin;
            case 2:
              return ESaleType.Coin_P;
            case 3:
              return ESaleType.TourCoin;
            case 4:
              return ESaleType.ArenaCoin;
            case 5:
              return ESaleType.EventCoin;
            case 6:
              return ESaleType.PiecePoint;
            case 7:
              return ESaleType.MultiCoin;
          }
        }
      }
      return ESaleType.Coin;
    }

    public static string ShopItemType2String(EShopItemType itype)
    {
      switch (itype)
      {
        case EShopItemType.Item:
        case EShopItemType.Set:
          return "item";
        case EShopItemType.Artifact:
          return "artifact";
        case EShopItemType.ConceptCard:
          return "concept_card";
        default:
          DebugUtility.LogError(string.Format("不明な商品タイプです (itype => {0})", (object) itype));
          return string.Empty;
      }
    }

    public static EShopItemType String2ShopItemType(string itype)
    {
      if (string.IsNullOrEmpty(itype))
      {
        DebugUtility.LogError("商品タイプが空文字です");
      }
      else
      {
        switch (itype)
        {
          case "item":
            return EShopItemType.Item;
          case "concept_card":
            return EShopItemType.ConceptCard;
          case "artifact":
            return EShopItemType.Artifact;
          default:
            DebugUtility.LogError(string.Format("不明な商品タイプです (itype => {0})", (object) itype));
            break;
        }
      }
      return EShopItemType.Unknown;
    }

    public static EShopItemType Iname2ShopItemType(string iname)
    {
      if (string.IsNullOrEmpty(iname))
      {
        DebugUtility.LogError("inameが空文字です");
      }
      else
      {
        if (MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(iname) != null)
          return EShopItemType.Item;
        if (MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(iname) != null)
          return EShopItemType.ConceptCard;
        if (MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(iname) != null)
          return EShopItemType.Artifact;
        DebugUtility.LogError(string.Format("不明な識別子です (iname => {0})", (object) iname));
      }
      return EShopItemType.Unknown;
    }

    public static int GetRemainingCurrency(ShopItem shopitem)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      switch (shopitem.saleType)
      {
        case ESaleType.Gold:
          return player.Gold;
        case ESaleType.Coin:
          return player.Coin;
        case ESaleType.TourCoin:
          return player.TourCoin;
        case ESaleType.ArenaCoin:
          return player.ArenaCoin;
        case ESaleType.PiecePoint:
          return player.PiecePoint;
        case ESaleType.MultiCoin:
          return player.MultiCoin;
        case ESaleType.EventCoin:
          switch (shopitem)
          {
            case EventShopItem _:
              EventShopItem eventShopItem = (EventShopItem) shopitem;
              return player.EventCoinNum(eventShopItem.cost_iname);
            case LimitedShopItem _:
              LimitedShopItem limitedShopItem = shopitem as LimitedShopItem;
              return player.EventCoinNum(limitedShopItem.cost_iname);
          }
          break;
        case ESaleType.Coin_P:
          return player.PaidCoin;
      }
      return 0;
    }

    public static int GetBuyPrice(ShopItem shopItem)
    {
      if (shopItem.isSetSaleValue)
        return shopItem.saleValue;
      int buyPrice = 0;
      if (shopItem != null)
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(shopItem.iname);
        if (itemParam != null)
        {
          switch (shopItem.saleType)
          {
            case ESaleType.Gold:
              buyPrice = shopItem.num * itemParam.buy;
              break;
            case ESaleType.Coin:
            case ESaleType.Coin_P:
              buyPrice = shopItem.num * itemParam.coin;
              break;
            case ESaleType.TourCoin:
              buyPrice = shopItem.num * itemParam.tour_coin;
              break;
            case ESaleType.ArenaCoin:
              buyPrice = shopItem.num * itemParam.arena_coin;
              break;
            case ESaleType.PiecePoint:
              buyPrice = shopItem.num * itemParam.piece_point;
              break;
            case ESaleType.MultiCoin:
              buyPrice = shopItem.num * itemParam.multi_coin;
              break;
            case ESaleType.EventCoin:
              DebugUtility.Assert("There is no common price in the event coin.");
              break;
          }
        }
      }
      return buyPrice;
    }

    public bool Deserialize(Json_ShopResponse response)
    {
      if (response.shopitems == null || !this.Deserialize(response.shopitems))
        return false;
      this.UpdateCount = response.relcnt;
      this.btn_update = true;
      if (!string.IsNullOrEmpty(response.msg) && response.msg.StartsWith("{"))
        this.btn_update = JSONParser.parseJSONObject<Json_ShopMsgResponse>(response.msg).update.Equals("on");
      GlobalVars.ConceptCardNum.Set(response.concept_count);
      return true;
    }

    public bool Deserialize(Json_ShopUpdateResponse response)
    {
      if (response.currencies == null)
        return false;
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(response.currencies);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      if (response.shopitems == null || !this.Deserialize(response.shopitems))
        return false;
      this.UpdateCount = response.relcnt;
      GlobalVars.ConceptCardNum.Set(response.concept_count);
      return true;
    }

    public bool Deserialize(Json_ShopBuyResponse response)
    {
      if (response.currencies == null)
        return false;
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(response.currencies);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      if (response.items != null)
      {
        try
        {
          MonoSingleton<GameManager>.Instance.Player.Deserialize(response.items);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return false;
        }
      }
      if (response.shopitems == null)
        return false;
      Json_ShopItem[] shopitems = response.shopitems;
      for (int i = 0; i < shopitems.Length; ++i)
      {
        ShopItem shopItem1 = this.items[i];
        if (shopItem1 == null)
        {
          shopItem1 = new ShopItem();
          this.items.Add(shopItem1);
        }
        else
        {
          ShopItem shopItem2 = this.items.Find((Predicate<ShopItem>) (it => it.id == shopitems[i].id));
          if (shopItem2 != null && shopitems[i] != null && shopitems[i].sold == 0 && shopitems[i].item != null && shopitems[i].item.step > shopItem2.step && shopitems[i].cost != null)
            ShopBuyStepupInfoData.Set(shopItem2.iname, shopitems[i].item.itype, shopItem2.max_num, shopItem2.saleValue, shopitems[i].cost.value, shopItem2.saleType);
        }
        if (!shopItem1.Deserialize(shopitems[i]))
          return false;
      }
      try
      {
        MonoSingleton<GameManager>.Instance.Player.Deserialize(response.units);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      GlobalVars.ConceptCardNum.Set(response.concept_count);
      try
      {
        MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(response.trophyprogs);
        MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(response.bingoprogs);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      return true;
    }

    public bool Deserialize(Json_ShopItem[] shopitems)
    {
      if (shopitems == null)
        return true;
      this.items.Clear();
      for (int index = 0; index < shopitems.Length; ++index)
      {
        ShopItem shopItem = new ShopItem();
        if (!shopItem.Deserialize(shopitems[index]))
          return false;
        this.items.Add(shopItem);
      }
      return true;
    }
  }
}
