// Decompiled with JetBrains decompiler
// Type: SRPG.GachaConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Close", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(11, "BuyCoin", FlowNode.PinTypes.Output, 11)]
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
    [SerializeField]
    private GameObject CautionRedrawText;
    private int mCost;
    private GachaCostType mGachaCostTtype;
    public GachaConfirmWindow.DecideEvent OnDecide;
    public GachaConfirmWindow.CancelEvent OnCancel;
    private GameManager gm;
    private GachaRequestParam m_request;
    private Text RedrawText;
    private GameObject m_Default;
    private GameObject m_Redraw;

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

    public GachaCostType GachaCostType
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
      if ((UnityEngine.Object) this.CautionRedrawText != (UnityEngine.Object) null)
      {
        SerializeValueBehaviour component = this.CautionRedrawText.GetComponent<SerializeValueBehaviour>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          this.RedrawText = component.list.GetUILabel("text");
        this.CautionRedrawText.SetActive(false);
      }
      SerializeValueBehaviour component1 = this.GetComponent<SerializeValueBehaviour>();
      if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
      {
        GameObject gameObject1 = component1.list.GetGameObject("default");
        GameObject gameObject2 = component1.list.GetGameObject("redraw");
        this.m_Default = gameObject1;
        this.m_Redraw = gameObject2;
      }
      this.Refresh();
    }

    private void Refresh()
    {
      if ((UnityEngine.Object) this.gm == (UnityEngine.Object) null)
        return;
      int num1 = this.gm.Player.FreeCoin + this.gm.Player.ComCoin;
      int paidCoin = this.gm.Player.PaidCoin;
      int coin = this.gm.Player.Coin;
      int num2 = coin - this.m_request.Cost;
      if (this.m_request.CostType == GachaCostType.COIN_P)
        num2 = paidCoin - this.m_request.Cost;
      int itemAmount = this.gm.Player.GetItemAmount(this.m_request.Ticket);
      bool flag = num2 >= 0;
      if (this.m_request.CostType == GachaCostType.TICKET)
        flag = true;
      if (this.m_request.IsRedrawGacha)
        flag = true;
      this.DecideButton.gameObject.SetActive(flag);
      this.BuyCoinButton.gameObject.SetActive(!flag);
      string str = this.m_request.ConfirmText;
      if (num2 < 0 && this.m_request.CostType != GachaCostType.TICKET)
        str = this.m_request.CostType != GachaCostType.COIN_P ? LocalizedText.Get("sys.GACHA_TEXT_COIN_NOT_ENOUGH") : LocalizedText.Get("sys.GACHA_TEXT_PAIDCOIN_NOT_ENOUGH");
      if ((UnityEngine.Object) this.AmountBox != (UnityEngine.Object) null)
        this.AmountBox.SetActive(this.m_request.CostType != GachaCostType.TICKET);
      if ((UnityEngine.Object) this.AmountTicketBox != (UnityEngine.Object) null)
        this.AmountTicketBox.SetActive(this.m_request.CostType == GachaCostType.TICKET);
      if ((UnityEngine.Object) this.Confirm != (UnityEngine.Object) null)
      {
        this.Confirm.text = str;
        this.Confirm.gameObject.SetActive(this.m_request.CostType != GachaCostType.TICKET);
      }
      if ((UnityEngine.Object) this.ConfirmTicket != (UnityEngine.Object) null)
      {
        this.Confirm.text = str;
        this.ConfirmTicket.gameObject.SetActive(this.m_request.CostType == GachaCostType.TICKET);
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
      if ((UnityEngine.Object) this.CautionBox != (UnityEngine.Object) null)
        this.CautionBox.SetActive(this.m_request.CostType == GachaCostType.COIN && this.m_request.Cost > 0);
      if ((UnityEngine.Object) this.CautionRedrawText != (UnityEngine.Object) null && this.m_request.IsRedrawGacha && (UnityEngine.Object) this.RedrawText != (UnityEngine.Object) null)
      {
        this.RedrawText.text = LocalizedText.Get("sys.GACHA_REDRAW_CAUTION", new object[1]
        {
          (object) this.m_request.RedrawRest
        });
        this.CautionRedrawText.SetActive(true);
      }
      if ((UnityEngine.Object) this.m_Default != (UnityEngine.Object) null)
        this.m_Default.SetActive(!this.m_request.IsRedrawConfirm);
      if (!((UnityEngine.Object) this.m_Redraw != (UnityEngine.Object) null))
        return;
      this.m_Redraw.SetActive(this.m_request.IsRedrawConfirm);
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

    public void Set(GachaRequestParam _param)
    {
      this.m_request = _param;
    }

    public delegate void DecideEvent();

    public delegate void CancelEvent();
  }
}
