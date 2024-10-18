// Decompiled with JetBrains decompiler
// Type: SRPG.GachaTicketSelectNumWindow
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
  [FlowNode.Pin(101, "チケット枚数を決定", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "キャンセル", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "チケットのinameが指定されていない", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "所持数が0orアイテムデータが存在しない", FlowNode.PinTypes.Output, 104)]
  public class GachaTicketSelectNumWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private Text WindowTitle;
    [SerializeField]
    private BitmapText UsedNum;
    [SerializeField]
    private Slider TicketNumSlider;
    [SerializeField]
    private GameObject AmountTicket;
    [SerializeField]
    private Button BtnDecide;
    [SerializeField]
    private Button BtnCancel;
    [SerializeField]
    private Button BtnPlus;
    [SerializeField]
    private Button BtnMinus;
    [SerializeField]
    private Button BtnMax;
    [SerializeField]
    [Header("単発召喚チケットの最大消費可能枚数(基本変更しない)")]
    private int MaxSelectNumSingleGacha = 10;
    [SerializeField]
    [Header("N連召喚チケットの最大消費可能枚数(基本変更しない)")]
    private int MaxSelectNumMultiGacha = 10;
    [SerializeField]
    [Header("最大選択枚数表記テキスト")]
    private Text SelectNumWarningText;
    private int mMaxNum;
    private int mFixMaxNum;
    private const int BUTTON_SINGLE_VALUE = 1;
    public static int LastSelectNum;
    public static string LastSelectGachaIname = string.Empty;
    private int mInitSelectNum = 1;
    private GachaManager gacham;

    public void Activated(int pinID)
    {
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.BtnPlus, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnPlus.onClick).AddListener(new UnityAction((object) this, __methodptr(OnAddNumSingle)));
      }
      if (Object.op_Inequality((Object) this.BtnMinus, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnMinus.onClick).AddListener(new UnityAction((object) this, __methodptr(OnSubNumSingle)));
      }
      if (Object.op_Inequality((Object) this.BtnMax, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnMax.onClick).AddListener(new UnityAction((object) this, __methodptr(OnMaxNum)));
      }
      string iname = FlowNode_Variable.Get("USE_TICKET_INAME");
      FlowNode_Variable.Set("USE_TICKET_INAME", string.Empty);
      if (string.IsNullOrEmpty(iname))
      {
        DebugUtility.LogError("不正なアイテムが指定されました");
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      }
      else
      {
        ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(iname);
        if (itemDataByItemId == null || itemDataByItemId.Num < 0)
        {
          DebugUtility.LogError("所持していないアイテムが指定されました");
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
        }
        else
        {
          if (Object.op_Equality((Object) this.gacham, (Object) null))
            this.gacham = MonoSingleton<GachaManager>.Instance;
          if (Object.op_Inequality((Object) GachaWindow.Instance, (Object) null))
          {
            GachaTopParamNew selectTicketGacha = GachaWindow.Instance.LastSelectTicketGacha;
            if (selectTicketGacha == null)
            {
              DebugUtility.LogError("召喚スケジュールがありません.");
              return;
            }
            if (selectTicketGacha.CostType != GachaCostType.TICKET)
            {
              DebugUtility.LogError("gname:" + selectTicketGacha.iname + "はチケット召喚ではありません.");
              return;
            }
            if (iname != selectTicketGacha.ticket_iname)
            {
              DebugUtility.LogError("選択されたチケット:" + iname + "とgname:" + selectTicketGacha.iname + "で指定されているチケット:" + selectTicketGacha.ticket_iname + "が一致しません.");
              return;
            }
            this.mMaxNum = selectTicketGacha.num != 1 ? this.MaxSelectNumMultiGacha : this.MaxSelectNumSingleGacha;
            this.mFixMaxNum = !selectTicketGacha.redraw ? Mathf.Min(itemDataByItemId.Num, this.mMaxNum) : 1;
            if (!string.IsNullOrEmpty(GachaTicketSelectNumWindow.LastSelectGachaIname))
            {
              if (selectTicketGacha.iname == GachaTicketSelectNumWindow.LastSelectGachaIname)
                this.mInitSelectNum = itemDataByItemId.Num <= GachaTicketSelectNumWindow.LastSelectNum ? itemDataByItemId.Num : GachaTicketSelectNumWindow.LastSelectNum;
              else
                GachaTicketSelectNumWindow.LastSelectNum = 1;
            }
            else
              GachaTicketSelectNumWindow.LastSelectNum = 1;
            GachaTicketSelectNumWindow.LastSelectGachaIname = selectTicketGacha.iname;
          }
          this.Refresh(itemDataByItemId);
        }
      }
    }

    public void Refresh(ItemData data)
    {
      if (Object.op_Inequality((Object) this.WindowTitle, (Object) null))
        this.WindowTitle.text = LocalizedText.Get("sys.GACHA_TICKET_SELECT_TITLE", (object) data.Param.name);
      if (Object.op_Inequality((Object) this.AmountTicket, (Object) null))
      {
        DataSource.Bind<ItemData>(this.AmountTicket, data);
        GameParameter.UpdateAll(this.AmountTicket);
      }
      if (Object.op_Inequality((Object) this.SelectNumWarningText, (Object) null))
        this.SelectNumWarningText.text = LocalizedText.Get("sys.GACHA_TICKET_SELECT_CONFIRM", (object) this.mMaxNum);
      if (Object.op_Inequality((Object) this.TicketNumSlider, (Object) null))
      {
        ((UnityEventBase) this.TicketNumSlider.onValueChanged).RemoveAllListeners();
        this.TicketNumSlider.minValue = 1f;
        this.TicketNumSlider.maxValue = (float) this.mFixMaxNum;
        // ISSUE: method pointer
        ((UnityEvent<float>) this.TicketNumSlider.onValueChanged).AddListener(new UnityAction<float>((object) this, __methodptr(OnUseNumChanged)));
        this.TicketNumSlider.value = (float) this.mInitSelectNum;
      }
      this.UpdateButtonInteractable();
      ((Text) this.UsedNum).text = this.TicketNumSlider.value.ToString();
      this.gacham.UseTicketNum = (int) this.TicketNumSlider.value;
    }

    private void OnAddNum(int value)
    {
      if (!Object.op_Inequality((Object) this.TicketNumSlider, (Object) null) || (double) this.TicketNumSlider.maxValue <= (double) this.TicketNumSlider.value)
        return;
      this.TicketNumSlider.value += (float) value;
    }

    private void OnAddNumSingle() => this.OnAddNum(1);

    private void OnSubNum(int value)
    {
      if (!Object.op_Inequality((Object) this.TicketNumSlider, (Object) null) || (double) this.TicketNumSlider.minValue >= (double) this.TicketNumSlider.value)
        return;
      this.TicketNumSlider.value -= (float) value;
    }

    private void OnSubNumSingle() => this.OnSubNum(1);

    private void OnUseNumChanged(float value)
    {
      ((Text) this.UsedNum).text = ((int) value).ToString();
      this.gacham.UseTicketNum = (int) value;
      GachaTicketSelectNumWindow.LastSelectNum = (int) value;
      this.UpdateButtonInteractable();
    }

    private void UpdateButtonInteractable()
    {
      if (Object.op_Inequality((Object) this.BtnPlus, (Object) null))
        ((Selectable) this.BtnPlus).interactable = (double) this.TicketNumSlider.value + 1.0 <= (double) this.TicketNumSlider.maxValue;
      if (!Object.op_Inequality((Object) this.BtnMinus, (Object) null))
        return;
      ((Selectable) this.BtnMinus).interactable = (double) this.TicketNumSlider.value - 1.0 >= (double) this.TicketNumSlider.minValue;
    }

    private void OnMaxNum()
    {
      if (!Object.op_Inequality((Object) this.TicketNumSlider, (Object) null))
        return;
      this.TicketNumSlider.value = this.TicketNumSlider.maxValue;
    }
  }
}
