﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetLimitedShopList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "IsLimitedShop", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("System/SetLimitedShopList", 32741)]
  public class FlowNode_SetLimitedShopList : FlowNode_Network
  {
    private int inputPin = 1;
    public LimitedShopList limited_shop_list;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 && pinID != 2 || this.enabled)
        return;
      this.inputPin = pinID;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqLimitedShopList(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.enabled = true;
      }
      else
        this.Success();
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_ShopListArray> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ShopListArray>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
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
        this.Success();
      }
    }

    public void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(10);
    }

    public long GetLimitedShopPeriodEndAt(JSON_ShopListArray list)
    {
      long num = 0;
      if (list != null && list.shops.Length > 0)
      {
        for (int index = 0; index < list.shops.Length; ++index)
        {
          long end = list.shops[index].end;
          num = num >= end ? num : end;
        }
      }
      return num;
    }
  }
}
