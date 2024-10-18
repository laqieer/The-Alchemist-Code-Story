// Decompiled with JetBrains decompiler
// Type: SRPG.GachaConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(11, "BuyCoin", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(1, "Close", FlowNode.PinTypes.Output, 1)]
  public class GachaConfirmWindow : MonoBehaviour, IFlowInterface
  {
    private string mConfirmText = string.Empty;
    private bool mIsShowCoinStatus = true;
    private string mUseTicket = string.Empty;
    [SerializeField]
    private Text Confirm;
    [SerializeField]
    private Text FreeCoin;
    [SerializeField]
    private Text PaidCoin;
    [SerializeField]
    private Text CurrentAmountCoin;
    [SerializeField]
    private Button CancelButton;
    [SerializeField]
    private Button DecideButton;
    [SerializeField]
    private Button BuyCoinButton;
    [SerializeField]
    private GameObject CautionBox;
    [SerializeField]
    private GameObject AmountBox;
    [SerializeField]
    private GameObject AmountTicketBox;
    [SerializeField]
    private Text CurrentAmountTicket;
    [SerializeField]
    private Text ConfirmTicket;
    private int mCost;
    private GachaButton.GachaCostType mGachaCostTtype;
    public GachaConfirmWindow.DecideEvent OnDecide;
    public GachaConfirmWindow.CancelEvent OnCancel;
    private GameManager gm;

    public string ConfirmText
    {
      get
      {
        return this.mConfirmText;
      }
      set
      {
        this.mConfirmText = value;
      }
    }

    public int Cost
    {
      get
      {
        return this.mCost;
      }
      set
      {
        this.mCost = value;
      }
    }

    public bool IsShowCoinStatus
    {
      get
      {
        return this.mIsShowCoinStatus;
      }
      set
      {
        this.mIsShowCoinStatus = value;
      }
    }

    public GachaButton.GachaCostType GachaCostType
    {
      get
      {
        return this.mGachaCostTtype;
      }
      set
      {
        this.mGachaCostTtype = value;
      }
    }

    public string UseTicket
    {
      get
      {
        return this.mUseTicket;
      }
      set
      {
        this.mUseTicket = value;
      }
    }

    public void Activated(int pinID)
    {
    }

    private void Start()
    {
      if ((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect() != (UnityEngine.Object) null)
        this.gm = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) this.Confirm != (UnityEngine.Object) null)
        this.Confirm.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.FreeCoin != (UnityEngine.Object) null)
        this.FreeCoin.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.PaidCoin != (UnityEngine.Object) null)
        this.PaidCoin.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.CurrentAmountCoin != (UnityEngine.Object) null)
        this.CurrentAmountCoin.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.CurrentAmountTicket != (UnityEngine.Object) null)
        this.CurrentAmountTicket.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.DecideButton != (UnityEngine.Object) null)
      {
        this.DecideButton.gameObject.SetActive(false);
        this.DecideButton.onClick.AddListener(new UnityAction(this.OnClickDecide));
      }
      if ((UnityEngine.Object) this.BuyCoinButton != (UnityEngine.Object) null)
      {
        this.BuyCoinButton.gameObject.SetActive(false);
        this.BuyCoinButton.onClick.AddListener(new UnityAction(this.OnClickBuyCoin));
      }
      if ((UnityEngine.Object) this.CancelButton != (UnityEngine.Object) null)
        this.CancelButton.onClick.AddListener(new UnityAction(this.OnClickCancel));
      if ((UnityEngine.Object) this.ConfirmTicket != (UnityEngine.Object) null)
        this.ConfirmTicket.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.AmountBox != (UnityEngine.Object) null)
        this.AmountBox.SetActive(false);
      if ((UnityEngine.Object) this.AmountTicketBox != (UnityEngine.Object) null)
        this.AmountTicketBox.SetActive(false);
      this.Refresh();
    }

    private void Refresh()
    {
      if ((UnityEngine.Object) this.gm == (UnityEngine.Object) null)
        return;
      int num1 = this.gm.Player.FreeCoin + this.gm.Player.ComCoin;
      int paidCoin = this.gm.Player.PaidCoin;
      int coin = this.gm.Player.Coin;
      int num2 = coin - this.Cost;
      if (this.GachaCostType == GachaButton.GachaCostType.COIN_P)
        num2 = paidCoin - this.Cost;
      int itemAmount = this.gm.Player.GetItemAmount(this.UseTicket);
      bool flag = num2 >= 0;
      if (this.GachaCostType == GachaButton.GachaCostType.TICKET)
        flag = true;
      this.DecideButton.gameObject.SetActive(flag);
      this.BuyCoinButton.gameObject.SetActive(!flag);
      if (num2 < 0 && this.GachaCostType != GachaButton.GachaCostType.TICKET)
        this.mConfirmText = this.GachaCostType != GachaButton.GachaCostType.COIN_P ? LocalizedText.Get("sys.GACHA_TEXT_COIN_NOT_ENOUGH") : LocalizedText.Get("sys.GACHA_TEXT_PAIDCOIN_NOT_ENOUGH");
      if ((UnityEngine.Object) this.AmountBox != (UnityEngine.Object) null)
        this.AmountBox.SetActive(this.GachaCostType != GachaButton.GachaCostType.TICKET);
      if ((UnityEngine.Object) this.AmountTicketBox != (UnityEngine.Object) null)
        this.AmountTicketBox.SetActive(this.GachaCostType == GachaButton.GachaCostType.TICKET);
      if ((UnityEngine.Object) this.Confirm != (UnityEngine.Object) null)
      {
        this.Confirm.text = this.mConfirmText;
        this.Confirm.gameObject.SetActive(this.GachaCostType != GachaButton.GachaCostType.TICKET);
      }
      if ((UnityEngine.Object) this.ConfirmTicket != (UnityEngine.Object) null)
      {
        this.ConfirmTicket.text = this.mConfirmText;
        this.ConfirmTicket.gameObject.SetActive(this.GachaCostType == GachaButton.GachaCostType.TICKET);
      }
      if ((UnityEngine.Object) this.FreeCoin != (UnityEngine.Object) null)
      {
        this.FreeCoin.text = num1.ToString();
        this.FreeCoin.gameObject.SetActive(true);
      }
      if ((UnityEngine.Object) this.PaidCoin != (UnityEngine.Object) null)
      {
        this.PaidCoin.text = paidCoin.ToString();
        this.PaidCoin.gameObject.SetActive(true);
      }
      if ((UnityEngine.Object) this.CurrentAmountCoin != (UnityEngine.Object) null)
      {
        this.CurrentAmountCoin.text = coin.ToString();
        this.CurrentAmountCoin.gameObject.SetActive(true);
      }
      if ((UnityEngine.Object) this.CurrentAmountTicket != (UnityEngine.Object) null)
      {
        this.CurrentAmountTicket.text = itemAmount.ToString();
        this.CurrentAmountTicket.gameObject.SetActive(true);
      }
      if (!((UnityEngine.Object) this.CautionBox != (UnityEngine.Object) null))
        return;
      this.CautionBox.SetActive(this.GachaCostType == GachaButton.GachaCostType.COIN);
    }

    private void OnClickDecide()
    {
      if (this.OnDecide != null)
        this.OnDecide();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    private void OnClickCancel()
    {
      if (this.OnCancel != null)
        this.OnCancel();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    private void OnClickBuyCoin()
    {
      if (this.OnCancel != null)
        this.OnCancel();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }

    public delegate void DecideEvent();

    public delegate void CancelEvent();
  }
}
