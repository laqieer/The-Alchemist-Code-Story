// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetEventShopList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Shop/SetEventShopList", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "IsEventShop", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_SetEventShopList : FlowNode_Network
  {
    private FlowNode_SetEventShopList.Mode mode;
    public EventShopList event_shop_list;
    private int inputPin = 1;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 && pinID != 2 || ((Behaviour) this).enabled)
        return;
      this.inputPin = pinID;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.mode = FlowNode_SetEventShopList.Mode.GetShopList;
        this.ExecRequest((WebAPI) new ReqEventShopList(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).enabled = true;
      }
      else
        this.Success();
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        this.OnRetry();
      }
      else if (this.mode == FlowNode_SetEventShopList.Mode.GetShopList)
      {
        WebAPI.JSON_BodyResponse<JSON_ShopListArray> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ShopListArray>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        this.event_shop_list.DestroyItems();
        if (jsonObject.body.shops != null && jsonObject.body.shops.Length > 0)
        {
          for (int index = 0; index < jsonObject.body.shops.Length; ++index)
          {
            if (jsonObject.body.shops[index] == null)
            {
              this.OnRetry();
              return;
            }
          }
          MonoSingleton<GameManager>.Instance.Player.UpdateEventCoin();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) EventShopWindow.Instance, (UnityEngine.Object) null))
            EventShopWindow.Instance.SetEnableEventShopList(jsonObject.body.shops);
          if (this.inputPin != 2)
            this.event_shop_list.AddEventShopList(jsonObject.body.shops);
        }
        this.event_shop_list.AddArenaShopList();
        this.event_shop_list.AddMultiShopList();
        this.EraseTimeOutPopupHistory(jsonObject.body.shops);
        this.mode = FlowNode_SetEventShopList.Mode.GetCoinNum;
        this.ExecRequest((WebAPI) new ReqGetCoinNum(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
      else
      {
        if (this.mode != FlowNode_SetEventShopList.Mode.GetCoinNum)
          return;
        WebAPI.JSON_BodyResponse<FlowNode_SetEventShopList.JSON_CoinNum> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_SetEventShopList.JSON_CoinNum>>(www.text);
        Network.RemoveAPI();
        if (jsonObject.body != null && jsonObject.body.item != null && jsonObject.body.item.Length > 0)
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.item);
        if (jsonObject.body != null && jsonObject.body.newcoin != null)
          GlobalVars.NewSummonCoinInfo = new GlobalVars.SummonCoinInfo()
          {
            Period = jsonObject.body.newcoin.period
          };
        this.Success();
      }
    }

    public void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(10);
    }

    public long GetEventShopPeriodEndAt(JSON_ShopListArray list)
    {
      long eventShopPeriodEndAt = 0;
      if (list != null && list.shops.Length > 0)
      {
        for (int index = 0; index < list.shops.Length; ++index)
        {
          long end = list.shops[index].end;
          eventShopPeriodEndAt = eventShopPeriodEndAt >= end ? eventShopPeriodEndAt : end;
        }
      }
      return eventShopPeriodEndAt;
    }

    private void EraseTimeOutPopupHistory(JSON_ShopListArray.Shops[] shops)
    {
      if (shops == null || shops.Length <= 0)
      {
        string json = JsonUtility.ToJson((object) new ShopTimeOutItemInfoArray());
        PlayerPrefsUtility.SetString(PlayerPrefsUtility.WARNED_EVENTSHOP_ITEM_TIMEOUT, json);
      }
      else
      {
        if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.WARNED_EVENTSHOP_ITEM_TIMEOUT))
          return;
        ShopTimeOutItemInfoArray outItemInfoArray = JsonUtility.FromJson<ShopTimeOutItemInfoArray>(PlayerPrefsUtility.GetString(PlayerPrefsUtility.WARNED_EVENTSHOP_ITEM_TIMEOUT, string.Empty));
        List<ShopTimeOutItemInfo> shopTimeOutItemInfoList = new List<ShopTimeOutItemInfo>();
        if (outItemInfoArray.Infos != null)
        {
          foreach (ShopTimeOutItemInfo info1 in outItemInfoArray.Infos)
          {
            ShopTimeOutItemInfo info = info1;
            if (((IEnumerable<JSON_ShopListArray.Shops>) shops).Any<JSON_ShopListArray.Shops>((Func<JSON_ShopListArray.Shops, bool>) (sh => sh.gname == info.ShopId)))
              shopTimeOutItemInfoList.Add(info);
          }
        }
        string json = JsonUtility.ToJson((object) new ShopTimeOutItemInfoArray(shopTimeOutItemInfoList.ToArray()));
        PlayerPrefsUtility.SetString(PlayerPrefsUtility.WARNED_EVENTSHOP_ITEM_TIMEOUT, json);
      }
    }

    private enum Mode
    {
      GetShopList,
      GetCoinNum,
    }

    private class JSON_CoinNum
    {
      public Json_Item[] item;
      public FlowNode_SetEventShopList.JSON_NewCoin newcoin;
    }

    private class JSON_NewCoin
    {
      public long period;
    }
  }
}
