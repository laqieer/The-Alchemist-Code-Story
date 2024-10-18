﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CreateItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Unit/CreateEquipItem", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "所持限界に達している", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "費用が足りない", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(4, "素材が足りない", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(100, "ワンタップ合成", FlowNode.PinTypes.Input, 100)]
  public class FlowNode_CreateItem : FlowNode_Network
  {
    private ItemParam mItemParam;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 && pinID != 100)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      this.mItemParam = MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.SelectedCreateItemID);
      if (!player.CheckItemCapacity(this.mItemParam, 1))
      {
        ((Behaviour) this).enabled = false;
        this.ActivateOutputLinks(2);
      }
      else if (pinID == 0)
      {
        if (MonoSingleton<GameManager>.Instance.GetRecipeParam(this.mItemParam.recipe).cost > player.Gold)
        {
          ((Behaviour) this).enabled = false;
          this.ActivateOutputLinks(3);
        }
        else
        {
          CreateItemResult result_type = player.CheckCreateItem(this.mItemParam);
          if (result_type == CreateItemResult.NotEnough)
          {
            ((Behaviour) this).enabled = false;
            this.ActivateOutputLinks(4);
          }
          else if (result_type == CreateItemResult.CanCreateCommon)
          {
            int cost = 0;
            Dictionary<string, int> consumes = (Dictionary<string, int>) null;
            bool is_ikkatsu = false;
            NeedEquipItemList item_list = new NeedEquipItemList();
            MonoSingleton<GameManager>.GetInstanceDirect().Player.CheckEnableCreateItem(this.mItemParam, ref is_ikkatsu, ref cost, ref consumes, item_list);
            UIUtility.ConfirmBox(LocalizedText.Get("sys.COMMON_EQUIP_CHECK_MADE", (object) item_list.GetCommonItemListString()), (UIUtility.DialogResultEvent) (go => this.CallApiNormal(player, result_type)), (UIUtility.DialogResultEvent) null);
          }
          else
            this.CallApiNormal(player, result_type);
        }
      }
      else
      {
        int cost = 0;
        Dictionary<string, int> consumes = (Dictionary<string, int>) null;
        bool is_ikkatsu = false;
        NeedEquipItemList need_euip_item = new NeedEquipItemList();
        bool flag = player.CheckEnableCreateItem(this.mItemParam, ref is_ikkatsu, ref cost, ref consumes, need_euip_item);
        if (cost > player.Gold)
        {
          ((Behaviour) this).enabled = false;
          this.ActivateOutputLinks(3);
        }
        else if (!flag && !need_euip_item.IsEnoughCommon())
        {
          ((Behaviour) this).enabled = false;
          this.ActivateOutputLinks(4);
        }
        else if (need_euip_item.IsEnoughCommon())
          UIUtility.ConfirmBox(LocalizedText.Get("sys.COMMON_EQUIP_CHECK_ONETAP", (object) need_euip_item.GetCommonItemListString()), (UIUtility.DialogResultEvent) (go => this.CallApi(need_euip_item, player)), (UIUtility.DialogResultEvent) null);
        else
          this.CallApi(need_euip_item, player);
      }
    }

    public void CallApi(NeedEquipItemList need_euip_item, PlayerData player)
    {
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqItemCompositAll(this.mItemParam.iname, need_euip_item.IsEnoughCommon(), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).enabled = true;
      }
      else
        player.CreateItemAll(this.mItemParam);
    }

    public void CallApiNormal(PlayerData player, CreateItemResult result_type)
    {
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqItemComposit(this.mItemParam.iname, result_type == CreateItemResult.CanCreateCommon, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).enabled = true;
      }
      else
        player.CreateItem(this.mItemParam);
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.GouseiMaterialShort:
            this.OnFailed();
            break;
          case Network.EErrCode.GouseiCostShort:
            this.OnFailed();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnRetry();
            return;
          }
          if (this.mItemParam != null)
            UIUtility.SystemMessage((string) null, string.Format(LocalizedText.Get("sys.UNIT_EQUIPMENT_CREATE_MESSAGE"), (object) this.mItemParam.name), (UIUtility.DialogResultEvent) null);
          Network.RemoveAPI();
          this.Success();
        }
      }
    }
  }
}
