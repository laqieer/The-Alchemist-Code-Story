// Decompiled with JetBrains decompiler
// Type: SRPG.ShopSellSelectNumWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "決定", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(101, "キャンセル", FlowNode.PinTypes.Output, 11)]
  public class ShopSellSelectNumWindow : MonoBehaviour, IFlowInterface
  {
    public Text TxtTitle;
    public Text TxtSellItemPriceStr;
    public Text TxtSellNumStr;
    public Text TxtSellTotalPriceStr;
    public Text TxtDecide;
    public Slider SellNumSlider;
    public Button BtnDecide;
    public Button BtnCancel;
    public Button BtnPlus;
    public Button BtnMinus;
    private int mSaveSellNum;

    private void Awake()
    {
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.TxtTitle, (Object) null))
        this.TxtTitle.text = LocalizedText.Get("sys.SHOP_SELL_SELECTNUM_TITLE");
      if (Object.op_Inequality((Object) this.TxtSellItemPriceStr, (Object) null))
        this.TxtSellItemPriceStr.text = LocalizedText.Get("sys.SELL_PRICE");
      if (Object.op_Inequality((Object) this.TxtSellNumStr, (Object) null))
        this.TxtSellNumStr.text = LocalizedText.Get("sys.SELL_NUM");
      if (Object.op_Inequality((Object) this.TxtSellTotalPriceStr, (Object) null))
        this.TxtSellTotalPriceStr.text = LocalizedText.Get("sys.SELL_PRICE");
      if (Object.op_Inequality((Object) this.TxtDecide, (Object) null))
        this.TxtDecide.text = LocalizedText.Get("sys.CMD_DECIDE");
      if (Object.op_Inequality((Object) this.BtnDecide, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnDecide.onClick).AddListener(new UnityAction((object) this, __methodptr(OnDecide)));
      }
      if (Object.op_Inequality((Object) this.BtnCancel, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnCancel.onClick).AddListener(new UnityAction((object) this, __methodptr(OnCancel)));
      }
      if (Object.op_Inequality((Object) this.BtnPlus, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnPlus.onClick).AddListener(new UnityAction((object) this, __methodptr(OnAddNum)));
      }
      if (Object.op_Inequality((Object) this.BtnMinus, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnMinus.onClick).AddListener(new UnityAction((object) this, __methodptr(OnRemoveNum)));
      }
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void OnAddNum()
    {
      SellItem selectSellItem = GlobalVars.SelectSellItem;
      if (selectSellItem == null)
        return;
      if (selectSellItem.num < selectSellItem.item.Num)
      {
        if (Object.op_Inequality((Object) this.SellNumSlider, (Object) null) && selectSellItem.num >= (int) this.SellNumSlider.maxValue)
          return;
        ++selectSellItem.num;
      }
      if (!Object.op_Inequality((Object) this.SellNumSlider, (Object) null))
        return;
      this.SellNumSlider.value = (float) selectSellItem.num;
    }

    private void OnRemoveNum()
    {
      SellItem selectSellItem = GlobalVars.SelectSellItem;
      if (selectSellItem == null)
        return;
      if (selectSellItem.num > 0)
        --selectSellItem.num;
      if (!Object.op_Inequality((Object) this.SellNumSlider, (Object) null))
        return;
      this.SellNumSlider.value = (float) selectSellItem.num;
    }

    private void Refresh()
    {
      SellItem selectSellItem = GlobalVars.SelectSellItem;
      if (selectSellItem == null)
        return;
      if (Object.op_Inequality((Object) this.SellNumSlider, (Object) null))
      {
        ((UnityEventBase) this.SellNumSlider.onValueChanged).RemoveAllListeners();
        if (GlobalVars.ShopType != EShopType.AwakePiece)
        {
          int num1 = 0;
          foreach (SellItem sellItem in GlobalVars.SellItemList)
          {
            if (sellItem != selectSellItem)
              num1 += sellItem.item.Sell * sellItem.num;
          }
          int num2 = (int.MaxValue - MonoSingleton<GameManager>.Instance.Player.Gold - num1) / selectSellItem.item.Sell;
          this.SellNumSlider.maxValue = num2 >= selectSellItem.item.Num ? (float) selectSellItem.item.Num : (float) num2;
        }
        else
          this.SellNumSlider.maxValue = (float) selectSellItem.item.Num;
        // ISSUE: method pointer
        ((UnityEvent<float>) this.SellNumSlider.onValueChanged).AddListener(new UnityAction<float>((object) this, __methodptr(OnSellNumChanged)));
        this.SellNumSlider.value = (float) selectSellItem.num;
      }
      this.mSaveSellNum = selectSellItem.num;
      DataSource.Bind<SellItem>(((Component) this).gameObject, selectSellItem);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void OnSellNumChanged(float value)
    {
      SellItem selectSellItem = GlobalVars.SelectSellItem;
      if (selectSellItem == null)
        return;
      selectSellItem.num = (int) value;
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void OnDecide()
    {
      SellItem selectSellItem = GlobalVars.SelectSellItem;
      if (selectSellItem.num > 0)
      {
        if (!GlobalVars.SellItemList.Contains(selectSellItem))
          GlobalVars.SellItemList.Add(selectSellItem);
      }
      else
      {
        selectSellItem.index = -1;
        selectSellItem.num = 0;
        GlobalVars.SellItemList.Remove(selectSellItem);
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void OnCancel()
    {
      GlobalVars.SelectSellItem.num = this.mSaveSellNum;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }
  }
}
