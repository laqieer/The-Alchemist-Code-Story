// Decompiled with JetBrains decompiler
// Type: SRPG.ShopBuyUpdateConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
        string str1;
        string str2;
        switch (MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(GlobalVars.ShopType).UpdateCostType)
        {
          case ESaleType.Gold:
            str1 = LocalizedText.Get("sys.GOLD");
            str2 = LocalizedText.Get("sys.GOLD");
            break;
          case ESaleType.TourCoin:
            str1 = LocalizedText.Get("sys.TOUR_COIN");
            str2 = LocalizedText.Get("sys.ITEM_TANI_1");
            break;
          case ESaleType.ArenaCoin:
            str1 = LocalizedText.Get("sys.ARENA_COIN");
            str2 = LocalizedText.Get("sys.ITEM_TANI_1");
            break;
          case ESaleType.PiecePoint:
            str1 = LocalizedText.Get("sys.PIECE_POINT");
            str2 = LocalizedText.Get("sys.ITEM_TANI_3");
            break;
          case ESaleType.MultiCoin:
            str1 = LocalizedText.Get("sys.MULTI_COIN");
            str2 = LocalizedText.Get("sys.ITEM_TANI_1");
            break;
          case ESaleType.EventCoin:
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.EventShopItem.shop_cost_iname);
            str1 = itemParam == null ? LocalizedText.Get("sys.EVENT_COIN") : itemParam.name;
            str2 = LocalizedText.Get("sys.ITEM_TANI_1");
            break;
          default:
            str1 = LocalizedText.Get("sys.COIN");
            str2 = LocalizedText.Get("sys.ITEM_TANI_1");
            break;
        }
        int shopUpdateCost = MonoSingleton<GameManager>.Instance.Player.GetShopUpdateCost(GlobalVars.ShopType, false);
        this.Message.text = string.Format(LocalizedText.Get("sys.UPDATE_ITEMLIST_MESSAGE"), (object) str1, (object) shopUpdateCost, (object) str2);
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
