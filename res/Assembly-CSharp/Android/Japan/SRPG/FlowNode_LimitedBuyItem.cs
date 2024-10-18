﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LimitedBuyItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Linq;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/Shop/LimitedBuyItem", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(104, "ショップ情報がない", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(105, "購入済み", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(106, "アイテム所持上限", FlowNode.PinTypes.Output, 16)]
  [FlowNode.Pin(107, "ゴールド不足", FlowNode.PinTypes.Output, 17)]
  [FlowNode.Pin(108, "課金コイン不足", FlowNode.PinTypes.Output, 18)]
  [FlowNode.Pin(109, "遠征コイン不足", FlowNode.PinTypes.Output, 19)]
  [FlowNode.Pin(110, "アリーナコイン不足", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(111, "カケラポイント不足", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(112, "マルチコイン不足", FlowNode.PinTypes.Output, 22)]
  [FlowNode.Pin(113, "有償石不足", FlowNode.PinTypes.Output, 23)]
  [FlowNode.Pin(114, "イベントコイン不足", FlowNode.PinTypes.Output, 24)]
  [FlowNode.Pin(120, "購入期間外", FlowNode.PinTypes.Output, 25)]
  [FlowNode.Pin(121, "ショップ期間外", FlowNode.PinTypes.Output, 26)]
  [FlowNode.Pin(122, "ショップラインナップが更新された", FlowNode.PinTypes.Output, 27)]
  public class FlowNode_LimitedBuyItem : FlowNode_Network
  {
    private EShopType mShopType;
    private const int PIN_OT_SHOP_BUY_OUTOF_ITEM_PERIOD = 120;
    private const int PIN_OT_SHOP_REFRESH_ITEM_LIST = 121;
    private const int PIN_OT_SHOP_BUY_OUTOF_PERIOD = 122;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      this.enabled = false;
      LimitedShopData limitedShopData = player.GetLimitedShopData();
      if (limitedShopData == null)
      {
        this.ActivateOutputLinks(104);
      }
      else
      {
        LimitedShopItem shopitem = limitedShopData.items.FirstOrDefault<LimitedShopItem>((Func<LimitedShopItem, bool>) (item => item.id == GlobalVars.ShopBuyIndex));
        if (shopitem.is_soldout)
        {
          this.ActivateOutputLinks(105);
        }
        else
        {
          int buy = 0;
          if (shopitem.IsArtifact)
            buy = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(shopitem.iname).GetBuyNum(shopitem.saleType);
          else if (shopitem.IsConceptCard)
          {
            if (!MonoSingleton<GameManager>.Instance.Player.CheckConceptCardCapacity(shopitem.num * GlobalVars.ShopBuyAmount))
            {
              ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(shopitem.iname);
              if (conceptCardParam != null && conceptCardParam.type == eCardType.Equipment)
              {
                this.ActivateOutputLinks(106);
                return;
              }
            }
          }
          else
          {
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(shopitem.iname);
            if (!shopitem.IsSet && !player.CheckItemCapacity(itemParam, shopitem.num * GlobalVars.ShopBuyAmount))
            {
              this.ActivateOutputLinks(106);
              return;
            }
            buy = itemParam.GetBuyNum(shopitem.saleType);
          }
          switch (shopitem.saleType)
          {
            case ESaleType.Gold:
              if (!this.CheckCanBuy(shopitem, buy, player.Gold, 107))
                return;
              break;
            case ESaleType.Coin:
              if (!this.CheckCanBuy(shopitem, buy, player.Coin, 108))
                return;
              break;
            case ESaleType.TourCoin:
              if (!this.CheckCanBuy(shopitem, buy, player.TourCoin, 109))
                return;
              break;
            case ESaleType.ArenaCoin:
              if (!this.CheckCanBuy(shopitem, buy, player.ArenaCoin, 110))
                return;
              break;
            case ESaleType.PiecePoint:
              if (!this.CheckCanBuy(shopitem, buy, player.PiecePoint, 111))
                return;
              break;
            case ESaleType.MultiCoin:
              if (!this.CheckCanBuy(shopitem, buy, player.MultiCoin, 112))
                return;
              break;
            case ESaleType.EventCoin:
              this.ActivateOutputLinks(113);
              return;
            case ESaleType.Coin_P:
              if (!this.CheckCanBuy(shopitem, buy, player.PaidCoin, 113))
                return;
              break;
          }
          if (Network.Mode == Network.EConnectMode.Offline)
          {
            player.DEBUG_BUY_ITEM(GlobalVars.ShopType, GlobalVars.ShopBuyIndex);
            this.Success();
          }
          else
          {
            this.mShopType = GlobalVars.ShopType;
            this.ExecRequest((WebAPI) new ReqItemLimitedShopBuypaid(GlobalVars.LimitedShopItem.shops.gname, GlobalVars.ShopBuyIndex, GlobalVars.ShopBuyAmount, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
            this.enabled = true;
          }
        }
      }
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(100);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.ShopSoldOut:
          case Network.EErrCode.ShopBuyCostShort:
          case Network.EErrCode.ShopBuyLvShort:
          case Network.EErrCode.ShopBuyNotFound:
          case Network.EErrCode.ShopBuyItemNotFound:
            this.OnBack();
            break;
          case Network.EErrCode.ShopRefreshItemList:
            UIUtility.SystemMessage((string) null, Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(121)), (GameObject) null, false, -1);
            this.enabled = false;
            Network.RemoveAPI();
            Network.ResetError();
            break;
          case Network.EErrCode.ShopBuyOutofItemPeriod:
            UIUtility.SystemMessage((string) null, Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(120)), (GameObject) null, false, -1);
            this.enabled = false;
            Network.RemoveAPI();
            Network.ResetError();
            break;
          case Network.EErrCode.ShopBuyOutofPeriod:
            UIUtility.SystemMessage((string) null, Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(122)), (GameObject) null, false, -1);
            this.enabled = false;
            Network.RemoveAPI();
            Network.ResetError();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_LimitedShopBuyResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_LimitedShopBuyResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          LimitedShopData shop = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData() ?? new LimitedShopData();
          if (!shop.Deserialize(jsonObject.body))
          {
            this.OnFailed();
          }
          else
          {
            MonoSingleton<GameManager>.Instance.Player.SetLimitedShopData(shop);
            LimitedShopItem limitedShopItem = shop.items.FirstOrDefault<LimitedShopItem>((Func<LimitedShopItem, bool>) (item => item.id == GlobalVars.ShopBuyIndex));
            if (jsonObject.body.cards != null && jsonObject.body.cards.Length > 0)
            {
              MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
              Json_ShopBuyConceptCard[] cards = jsonObject.body.cards;
              for (int index = 0; index < cards.Length; ++index)
              {
                if (cards[index] != null && cards[index].IsGetConceptCardUnit)
                  FlowNode_ConceptCardGetUnit.AddConceptCardData(ConceptCardData.CreateConceptCardDataForDisplay(cards[index].iname));
              }
            }
            if (limitedShopItem.isSetSaleValue)
            {
              MyMetaps.TrackSpendShop(limitedShopItem.saleType, this.mShopType, limitedShopItem.saleValue);
            }
            else
            {
              int num = !limitedShopItem.IsArtifact ? (!limitedShopItem.IsConceptCard ? MonoSingleton<GameManager>.Instance.GetItemParam(limitedShopItem.iname).GetBuyNum(limitedShopItem.saleType) * limitedShopItem.num : limitedShopItem.saleValue) : MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(limitedShopItem.iname).GetBuyNum(limitedShopItem.saleType) * limitedShopItem.num;
              if (num > 0)
                MyMetaps.TrackSpendShop(limitedShopItem.saleType, this.mShopType, num);
            }
            this.Success();
          }
        }
      }
    }

    public bool CheckCanBuy(LimitedShopItem shopitem, int buy, int check, int pin)
    {
      int num = !shopitem.isSetSaleValue ? buy * shopitem.num : shopitem.saleValue;
      if (check >= num)
        return true;
      this.ActivateOutputLinks(pin);
      return false;
    }
  }
}
