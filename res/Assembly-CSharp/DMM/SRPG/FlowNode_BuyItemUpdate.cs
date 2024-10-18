// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BuyItemUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Buy/BuyItemUpdate", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(101, "ショップ情報が存在しない", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(103, "ゴールド不足", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(104, "課金コイン不足", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(105, "遠征コイン不足", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(106, "アリーナコイン不足", FlowNode.PinTypes.Output, 16)]
  [FlowNode.Pin(107, "欠片ポイント不足", FlowNode.PinTypes.Output, 17)]
  [FlowNode.Pin(108, "マルチコイン不足", FlowNode.PinTypes.Output, 18)]
  [FlowNode.Pin(109, "イベントコイン不足", FlowNode.PinTypes.Output, 19)]
  [FlowNode.Pin(131, "ポートショップ：ギルド未所属", FlowNode.PinTypes.Output, 131)]
  public class FlowNode_BuyItemUpdate : FlowNode_Network
  {
    private const int PIN_OUT_PORTSHOP_GUILD_NOTJOINED = 131;
    private EShopType mShopType;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || ((Behaviour) this).enabled)
        return;
      this.mShopType = GlobalVars.ShopType;
      ShopData shopData = MonoSingleton<GameManager>.Instance.Player.GetShopData(this.mShopType);
      ShopParam shopParam = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(this.mShopType);
      ((Behaviour) this).enabled = false;
      if (shopData == null || shopParam == null)
      {
        this.ActivateOutputLinks(101);
      }
      else
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        int shopUpdateCost = player.GetShopUpdateCost(this.mShopType);
        switch (shopParam.UpdateCostType)
        {
          case ESaleType.Gold:
            if (player.Gold < shopUpdateCost)
            {
              this.ActivateOutputLinks(103);
              return;
            }
            break;
          case ESaleType.Coin:
            if (player.Coin < shopUpdateCost)
            {
              this.ActivateOutputLinks(104);
              return;
            }
            break;
          case ESaleType.TourCoin:
            if (player.TourCoin < shopUpdateCost)
            {
              this.ActivateOutputLinks(105);
              return;
            }
            break;
          case ESaleType.ArenaCoin:
            if (player.ArenaCoin < shopUpdateCost)
            {
              this.ActivateOutputLinks(106);
              return;
            }
            break;
          case ESaleType.PiecePoint:
            if (player.PiecePoint < shopUpdateCost)
            {
              this.ActivateOutputLinks(107);
              return;
            }
            break;
          case ESaleType.MultiCoin:
            if (player.MultiCoin < shopUpdateCost)
            {
              this.ActivateOutputLinks(108);
              return;
            }
            break;
          case ESaleType.EventCoin:
            string shopCostIname;
            switch (GlobalVars.ShopType)
            {
              case EShopType.Limited:
              case EShopType.Port:
                shopCostIname = GlobalVars.LimitedShopItem.shop_cost_iname;
                break;
              default:
                shopCostIname = GlobalVars.EventShopItem.shop_cost_iname;
                break;
            }
            if (string.IsNullOrEmpty(shopCostIname) || player.EventCoinNum(shopCostIname) < shopUpdateCost)
            {
              this.ActivateOutputLinks(109);
              return;
            }
            break;
        }
        if (Network.Mode == Network.EConnectMode.Online)
        {
          if (this.mShopType == EShopType.Event)
            this.ExecRequest((WebAPI) new ReqItemEventShopUpdate(GlobalVars.EventShopItem.shops.gname, GlobalVars.EventShopItem.shop_cost_iname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          else if (this.mShopType == EShopType.Limited || this.mShopType == EShopType.Port)
            this.ExecRequest((WebAPI) new ReqItemEventShopUpdate(GlobalVars.LimitedShopItem.shops.gname, GlobalVars.LimitedShopItem.shop_cost_iname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          else if (this.mShopType == EShopType.Guerrilla)
            this.ExecRequest((WebAPI) new ReqItemGuerrillaShopUpdate(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          else
            this.ExecRequest((WebAPI) new ReqItemShopUpdate(this.mShopType.ToString(), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          ((Behaviour) this).enabled = true;
        }
        else if (!player.CheckShopUpdateCost(GlobalVars.ShopType))
          this.ActivateOutputLinks(104);
        else
          this.Success();
      }
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(100);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.ShopRefreshCostShort:
          case Network.EErrCode.ShopRefreshLvSort:
            this.OnBack();
            break;
          case Network.EErrCode.Guild_NotJoined:
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).enabled = false;
            this.ActivateOutputLinks(131);
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        switch (GlobalVars.ShopType)
        {
          case EShopType.Event:
            WebAPI.JSON_BodyResponse<Json_EventShopUpdateResponse> jsonObject1 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_EventShopUpdateResponse>>(www.text);
            DebugUtility.Assert(jsonObject1 != null, "res == null");
            if (jsonObject1.body == null)
            {
              this.OnRetry();
              return;
            }
            List<JSON_EventShopItemListSet> eventShopItemListSetList = new List<JSON_EventShopItemListSet>((IEnumerable<JSON_EventShopItemListSet>) jsonObject1.body.shopitems);
            jsonObject1.body.shopitems = eventShopItemListSetList.ToArray();
            Network.RemoveAPI();
            EventShopData shop1 = MonoSingleton<GameManager>.Instance.Player.GetEventShopData() ?? new EventShopData();
            if (!shop1.Deserialize(jsonObject1.body))
            {
              this.OnFailed();
              return;
            }
            MonoSingleton<GameManager>.Instance.Player.SetEventShopData(shop1);
            break;
          case EShopType.Limited:
          case EShopType.Port:
            WebAPI.JSON_BodyResponse<Json_LimitedShopUpdateResponse> jsonObject2 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_LimitedShopUpdateResponse>>(www.text);
            DebugUtility.Assert(jsonObject2 != null, "res == null");
            if (jsonObject2.body == null)
            {
              this.OnRetry();
              return;
            }
            List<JSON_LimitedShopItemListSet> limitedShopItemListSetList = new List<JSON_LimitedShopItemListSet>((IEnumerable<JSON_LimitedShopItemListSet>) jsonObject2.body.shopitems);
            jsonObject2.body.shopitems = limitedShopItemListSetList.ToArray();
            Network.RemoveAPI();
            LimitedShopData shop2 = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData() ?? new LimitedShopData();
            if (!shop2.Deserialize(jsonObject2.body))
            {
              this.OnFailed();
              return;
            }
            MonoSingleton<GameManager>.Instance.Player.SetLimitedShopData(shop2);
            break;
          default:
            WebAPI.JSON_BodyResponse<Json_ShopUpdateResponse> jsonObject3 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ShopUpdateResponse>>(www.text);
            DebugUtility.Assert(jsonObject3 != null, "res == null");
            if (jsonObject3.body == null)
            {
              this.OnRetry();
              return;
            }
            Network.RemoveAPI();
            ShopData shop3 = MonoSingleton<GameManager>.Instance.Player.GetShopData(this.mShopType) ?? new ShopData();
            if (!shop3.Deserialize(jsonObject3.body))
            {
              this.OnFailed();
              return;
            }
            MonoSingleton<GameManager>.Instance.Player.SetShopData(this.mShopType, shop3);
            break;
        }
        ShopParam shopParam = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(this.mShopType);
        if (shopParam != null)
        {
          PlayerData player = MonoSingleton<GameManager>.Instance.Player;
          MyMetaps.TrackSpendShopUpdate(shopParam.UpdateCostType, this.mShopType, player.GetShopUpdateCost(this.mShopType, true));
        }
        this.Success();
      }
    }
  }
}
