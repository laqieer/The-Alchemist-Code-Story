// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetLimitedShopList
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
  [FlowNode.NodeType("System/Shop/SetLimitedShopList", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "IsLimitedShop", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(131, "ポートショップ：ギルド未所属", FlowNode.PinTypes.Output, 131)]
  public class FlowNode_SetLimitedShopList : FlowNode_Network
  {
    private const int PIN_OUT_PORTSHOP_GUILD_NOTJOINED = 131;
    public LimitedShopList limited_shop_list;
    private int inputPin = 1;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 && pinID != 2 || ((Behaviour) this).enabled)
        return;
      this.inputPin = pinID;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        ReqLimitedShopList.eShopType shop_type = ReqLimitedShopList.eShopType.Limited;
        if (GlobalVars.ShopType == EShopType.Port)
          shop_type = ReqLimitedShopList.eShopType.Port;
        this.ExecRequest((WebAPI) new ReqLimitedShopList(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), shop_type));
        ((Behaviour) this).enabled = true;
      }
      else
        this.Success();
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.Guild_NotJoined)
        {
          Network.RemoveAPI();
          Network.ResetError();
          ((Behaviour) this).enabled = false;
          this.ActivateOutputLinks(131);
        }
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_ShopListArray> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ShopListArray>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (GlobalVars.ShopType == EShopType.Port)
        {
          try
          {
            if (jsonObject.body == null)
              throw new Exception("SetLimitedShopList: illegal response data!");
            if (jsonObject.body.shops == null)
              jsonObject.body.shops = new JSON_ShopListArray.Shops[0];
            for (int index = 0; index < jsonObject.body.shops.Length; ++index)
            {
              if (jsonObject.body.shops[index] == null)
                throw new Exception("SetLimitedShopList: illegal response shops data!");
            }
            if (!AlchemyShopManager.EntryShopList(jsonObject.body.shops))
              throw new Exception("SetLimitedShopList: illegal shops data!");
            AlchemyShopManager.ClearGlobalLimitedShopItemInfo();
            if (jsonObject.body.first_shop_info != null)
            {
              if (!AlchemyShopManager.SetCurrentShopData(jsonObject.body.first_shop_info))
                throw new Exception("SetLimitedShopList: illegal shopitems data!");
            }
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnFailed();
            return;
          }
        }
        else
        {
          if (jsonObject.body.shops == null || jsonObject.body.shops.Length <= 0)
          {
            MonoSingleton<GameManager>.Instance.LimitedShopEndAt = 0L;
            MonoSingleton<GameManager>.Instance.LimitedShopList = (JSON_ShopListArray.Shops[]) null;
          }
          else
          {
            for (int index = 0; index < jsonObject.body.shops.Length; ++index)
            {
              if (jsonObject.body.shops[index] == null)
              {
                this.OnRetry();
                return;
              }
            }
            MonoSingleton<GameManager>.Instance.LimitedShopEndAt = this.GetLimitedShopPeriodEndAt(jsonObject.body);
            if (this.inputPin != 2)
              this.limited_shop_list.SetLimitedShopList(jsonObject.body.shops);
            MonoSingleton<GameManager>.Instance.LimitedShopList = jsonObject.body.shops;
          }
          string warned_key = PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT;
          if (GlobalVars.ShopType == EShopType.Port)
            warned_key = PlayerPrefsUtility.WARNED_PORTSHOP_ITEM_TIMEOUT;
          this.EraseTimeOutPopupHistory(warned_key, jsonObject.body.shops);
        }
        this.Success();
      }
    }

    public void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(10);
    }

    public long GetLimitedShopPeriodEndAt(JSON_ShopListArray list)
    {
      long limitedShopPeriodEndAt = 0;
      if (list != null && list.shops.Length > 0)
      {
        for (int index = 0; index < list.shops.Length; ++index)
        {
          long end = list.shops[index].end;
          limitedShopPeriodEndAt = limitedShopPeriodEndAt >= end ? limitedShopPeriodEndAt : end;
        }
      }
      return limitedShopPeriodEndAt;
    }

    private void EraseTimeOutPopupHistory(string warned_key, JSON_ShopListArray.Shops[] shops)
    {
      if (shops == null || shops.Length <= 0)
      {
        string json = JsonUtility.ToJson((object) new ShopTimeOutItemInfoArray());
        PlayerPrefsUtility.SetString(warned_key, json);
      }
      else
      {
        if (!PlayerPrefsUtility.HasKey(warned_key))
          return;
        ShopTimeOutItemInfoArray outItemInfoArray = JsonUtility.FromJson<ShopTimeOutItemInfoArray>(PlayerPrefsUtility.GetString(warned_key, string.Empty));
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
        PlayerPrefsUtility.SetString(warned_key, json);
      }
    }
  }
}
