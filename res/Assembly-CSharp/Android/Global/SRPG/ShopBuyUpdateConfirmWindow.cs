﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ShopBuyUpdateConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class ShopBuyUpdateConfirmWindow : MonoBehaviour, IFlowInterface
  {
    public Text Title;
    public Text Message;
    public Text DecideText;
    public Text CancelText;

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.Title != (UnityEngine.Object) null)
        this.Title.text = LocalizedText.Get("sys.UPDATE_ITEMLIST_TITLE");
      if ((UnityEngine.Object) this.Message != (UnityEngine.Object) null)
      {
        ShopParam shopParam = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(GlobalVars.ShopType);
        string empty = string.Empty;
        string str;
        switch (shopParam.UpdateCostType)
        {
          case ESaleType.Gold:
            str = LocalizedText.Get("sys.GOLD");
            empty = LocalizedText.Get("sys.GOLD");
            break;
          case ESaleType.TourCoin:
            str = LocalizedText.Get("sys.TOUR_COIN");
            break;
          case ESaleType.ArenaCoin:
            str = LocalizedText.Get("sys.ARENA_COIN");
            break;
          case ESaleType.PiecePoint:
            str = LocalizedText.Get("sys.PIECE_POINT");
            break;
          case ESaleType.MultiCoin:
            str = LocalizedText.Get("sys.MULTI_COIN");
            break;
          case ESaleType.EventCoin:
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.EventShopItem.shop_cost_iname);
            str = itemParam == null ? LocalizedText.Get("sys.EVENT_COIN") : itemParam.name;
            break;
          default:
            str = LocalizedText.Get("sys.COIN");
            break;
        }
        int shopUpdateCost = MonoSingleton<GameManager>.Instance.Player.GetShopUpdateCost(GlobalVars.ShopType, false);
        this.Message.text = string.Format(LocalizedText.Get("sys.UPDATE_ITEMLIST_MESSAGE"), (object) str, (object) shopUpdateCost, (object) empty);
      }
      if ((UnityEngine.Object) this.DecideText != (UnityEngine.Object) null)
        this.DecideText.text = LocalizedText.Get("sys.CMD_YES");
      if ((UnityEngine.Object) this.CancelText != (UnityEngine.Object) null)
        this.CancelText.text = LocalizedText.Get("sys.CMD_NO");
      this.Refresh();
    }

    private void Refresh()
    {
      this.enabled = true;
    }
  }
}
