// Decompiled with JetBrains decompiler
// Type: SRPG.ShopSellSelectNumWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
      if ((UnityEngine.Object) this.TxtTitle != (UnityEngine.Object) null)
        this.TxtTitle.text = LocalizedText.Get("sys.SHOP_SELL_SELECTNUM_TITLE");
      if ((UnityEngine.Object) this.TxtSellItemPriceStr != (UnityEngine.Object) null)
        this.TxtSellItemPriceStr.text = LocalizedText.Get("sys.SELL_PRICE");
      if ((UnityEngine.Object) this.TxtSellNumStr != (UnityEngine.Object) null)
        this.TxtSellNumStr.text = LocalizedText.Get("sys.SELL_NUM");
      if ((UnityEngine.Object) this.TxtSellTotalPriceStr != (UnityEngine.Object) null)
        this.TxtSellTotalPriceStr.text = LocalizedText.Get("sys.SELL_PRICE");
      if ((UnityEngine.Object) this.TxtDecide != (UnityEngine.Object) null)
        this.TxtDecide.text = LocalizedText.Get("sys.CMD_DECIDE");
      if ((UnityEngine.Object) this.BtnDecide != (UnityEngine.Object) null)
        this.BtnDecide.onClick.AddListener(new UnityAction(this.OnDecide));
      if ((UnityEngine.Object) this.BtnCancel != (UnityEngine.Object) null)
        this.BtnCancel.onClick.AddListener(new UnityAction(this.OnCancel));
      if ((UnityEngine.Object) this.BtnPlus != (UnityEngine.Object) null)
        this.BtnPlus.onClick.AddListener(new UnityAction(this.OnAddNum));
      if ((UnityEngine.Object) this.BtnMinus != (UnityEngine.Object) null)
        this.BtnMinus.onClick.AddListener(new UnityAction(this.OnRemoveNum));
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
        ++selectSellItem.num;
      if (!((UnityEngine.Object) this.SellNumSlider != (UnityEngine.Object) null))
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
      if (!((UnityEngine.Object) this.SellNumSlider != (UnityEngine.Object) null))
        return;
      this.SellNumSlider.value = (float) selectSellItem.num;
    }

    private void Refresh()
    {
      SellItem selectSellItem = GlobalVars.SelectSellItem;
      if (selectSellItem == null)
        return;
      if ((UnityEngine.Object) this.SellNumSlider != (UnityEngine.Object) null)
      {
        this.SellNumSlider.onValueChanged.RemoveAllListeners();
        this.SellNumSlider.maxValue = (float) selectSellItem.item.Num;
        this.SellNumSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnSellNumChanged));
        this.SellNumSlider.value = (float) selectSellItem.num;
      }
      this.mSaveSellNum = selectSellItem.num;
      DataSource.Bind<SellItem>(this.gameObject, selectSellItem);
      GameParameter.UpdateAll(this.gameObject);
    }

    private void OnSellNumChanged(float value)
    {
      SellItem selectSellItem = GlobalVars.SelectSellItem;
      if (selectSellItem == null)
        return;
      selectSellItem.num = (int) value;
      GameParameter.UpdateAll(this.gameObject);
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
