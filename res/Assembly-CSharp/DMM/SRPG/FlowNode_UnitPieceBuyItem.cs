// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_UnitPieceBuyItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Shop/UnitPieceBuyItem", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(104, "Not Exists Shop", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(105, "Purchased", FlowNode.PinTypes.Output, 105)]
  [FlowNode.Pin(106, "Item Limit", FlowNode.PinTypes.Output, 106)]
  [FlowNode.Pin(120, "Out of Period", FlowNode.PinTypes.Output, 120)]
  [FlowNode.Pin(121, "Refresh List", FlowNode.PinTypes.Output, 121)]
  [FlowNode.Pin(122, "Shop Out of Period", FlowNode.PinTypes.Output, 122)]
  [FlowNode.Pin(123, "Limit Over", FlowNode.PinTypes.Output, 123)]
  [FlowNode.Pin(124, "No Unit Party", FlowNode.PinTypes.Output, 124)]
  public class FlowNode_UnitPieceBuyItem : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 1;
    private const int PIN_OT_SUCCESS = 100;
    private const int PIN_OT_NOT_EXISTS_SHOP = 104;
    private const int PIN_OT_PURCHASED = 105;
    private const int PIN_OT_ITEM_LIMIT = 106;
    private const int PIN_OT_OUTOF_PERIOD = 120;
    private const int PIN_OT_REFRESH_LIST = 121;
    private const int PIN_OT_SHOP_OUTOF_PERIOD = 122;
    private const int PIN_OT_LIMIT_OVER = 123;
    private const int PIN_OT_NO_UNIT_PARTY = 124;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      DebugUtility.Assert(player.UnitPieceShopData != null, "ショップ情報が存在しない");
      ((Behaviour) this).enabled = false;
      UnitPieceShopItem unitPieceShopItem = GlobalVars.BuyUnitPieceShopItem;
      if (unitPieceShopItem == null)
        this.ActivateOutputLinks(104);
      else if (unitPieceShopItem.IsSoldOut)
      {
        this.ActivateOutputLinks(105);
      }
      else
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(unitPieceShopItem.IName);
        if (itemParam == null)
          this.ActivateOutputLinks(104);
        else if (!player.CheckItemCapacity(itemParam, GlobalVars.ShopBuyAmount))
        {
          this.ActivateOutputLinks(106);
        }
        else
        {
          ((Behaviour) this).enabled = true;
          this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
          this.ExecRequest((WebAPI) new ReqUnitPieceShopBuypaid(player.UnitPieceShopData.ShopIName, unitPieceShopItem.IName, GlobalVars.ShopBuyAmount, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
        }
      }
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(100);
    }

    public override void OnSuccess(WWWResult www)
    {
      ReqUnitPieceShopBuypaid.Response json = (ReqUnitPieceShopBuypaid.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_UnitPieceBuyItem.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_UnitPieceBuyItem.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(mpResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
        string statMsg = mpResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        json = mpResponse.body;
      }
      if (SRPG.Network.IsError)
      {
        SRPG.Network.EErrCode errCode = SRPG.Network.ErrCode;
        switch (errCode)
        {
          case SRPG.Network.EErrCode.ShopSoldOut:
          case SRPG.Network.EErrCode.ShopBuyCostShort:
          case SRPG.Network.EErrCode.ShopBuyLvShort:
          case SRPG.Network.EErrCode.ShopBuyNotFound:
          case SRPG.Network.EErrCode.ShopBuyItemNotFound:
            this.OnBack();
            break;
          case SRPG.Network.EErrCode.ShopRefreshItemList:
            UIUtility.SystemMessage((string) null, SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(121)));
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.ShopBuyOutofItemPeriod:
            UIUtility.SystemMessage((string) null, SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(120)));
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.ShopBuyOutofPeriod:
            UIUtility.SystemMessage((string) null, SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(122)));
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.ShopLimitOver:
            UIUtility.SystemMessage((string) null, SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(123)));
            ((Behaviour) this).enabled = false;
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          default:
            if (errCode == SRPG.Network.EErrCode.NoUnitParty)
            {
              UIUtility.SystemMessage((string) null, SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(124)));
              ((Behaviour) this).enabled = false;
              SRPG.Network.RemoveAPI();
              SRPG.Network.ResetError();
              break;
            }
            this.OnRetry();
            break;
        }
      }
      else
      {
        if (flag)
        {
          WebAPI.JSON_BodyResponse<ReqUnitPieceShopBuypaid.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqUnitPieceShopBuypaid.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnRetry();
            return;
          }
          json = jsonObject.body;
        }
        SRPG.Network.RemoveAPI();
        if (json.items != null)
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Player.Deserialize(json.items);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnFailed();
            return;
          }
        }
        if (!MonoSingleton<GameManager>.Instance.Player.UnitPieceShopData.Deserialize(json))
        {
          this.OnFailed();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(json.trophyprogs);
            MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(json.bingoprogs);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnFailed();
            return;
          }
          this.Success();
        }
      }
    }

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqUnitPieceShopBuypaid.Response body;
    }
  }
}
