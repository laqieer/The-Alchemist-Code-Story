// Decompiled with JetBrains decompiler
// Type: SRPG.GachaTicketSelectNumWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(102, "キャンセル", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(101, "チケット枚数を決定", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(103, "チケットのinameが指定されていない", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "所持数が0orアイテムデータが存在しない", FlowNode.PinTypes.Output, 104)]
  public class GachaTicketSelectNumWindow : MonoBehaviour, IFlowInterface
  {
    private const int DefaultMaxNum = 10;
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
    private int mSaveUseNum;
    private int mMaxNum;
    private GachaManager gacham;

    public void Activated(int pinID)
    {
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.BtnPlus != (UnityEngine.Object) null)
        this.BtnPlus.onClick.AddListener(new UnityAction(this.OnAddNum));
      if ((UnityEngine.Object) this.BtnMinus != (UnityEngine.Object) null)
        this.BtnMinus.onClick.AddListener(new UnityAction(this.OnRemoveNum));
      if ((UnityEngine.Object) this.BtnMax != (UnityEngine.Object) null)
        this.BtnMax.onClick.AddListener(new UnityAction(this.OnMaxNum));
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
          if ((UnityEngine.Object) this.gacham == (UnityEngine.Object) null)
            this.gacham = MonoSingleton<GachaManager>.Instance;
          this.Refresh(itemDataByItemId);
        }
      }
    }

    public void Refresh(ItemData data)
    {
      this.mMaxNum = Mathf.Min(data.Num, 10);
      if ((UnityEngine.Object) this.WindowTitle != (UnityEngine.Object) null)
        this.WindowTitle.text = LocalizedText.Get("sys.GACHA_TICKET_SELECT_TITLE", new object[1]
        {
          (object) data.Param.name
        });
      if ((UnityEngine.Object) this.AmountTicket != (UnityEngine.Object) null)
      {
        DataSource.Bind<ItemData>(this.AmountTicket, data);
        GameParameter.UpdateAll(this.AmountTicket);
      }
      if ((UnityEngine.Object) this.TicketNumSlider != (UnityEngine.Object) null)
      {
        this.TicketNumSlider.onValueChanged.RemoveAllListeners();
        this.TicketNumSlider.minValue = 1f;
        this.TicketNumSlider.maxValue = (float) this.mMaxNum;
        this.TicketNumSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnUseNumChanged));
        this.TicketNumSlider.value = this.TicketNumSlider.minValue;
      }
      if ((UnityEngine.Object) this.BtnPlus != (UnityEngine.Object) null)
        this.BtnPlus.interactable = (double) this.TicketNumSlider.value + 1.0 <= (double) this.TicketNumSlider.maxValue;
      if ((UnityEngine.Object) this.BtnMinus != (UnityEngine.Object) null)
        this.BtnMinus.interactable = (double) this.TicketNumSlider.value - 1.0 >= (double) this.TicketNumSlider.minValue;
      this.UsedNum.text = this.TicketNumSlider.value.ToString();
      this.gacham.UseTicketNum = (int) this.TicketNumSlider.value;
    }

    private void OnAddNum()
    {
      if (!((UnityEngine.Object) this.TicketNumSlider != (UnityEngine.Object) null) || (double) this.TicketNumSlider.maxValue <= (double) this.TicketNumSlider.value)
        return;
      ++this.TicketNumSlider.value;
    }

    private void OnRemoveNum()
    {
      if (!((UnityEngine.Object) this.TicketNumSlider != (UnityEngine.Object) null) || (double) this.TicketNumSlider.minValue >= (double) this.TicketNumSlider.value)
        return;
      --this.TicketNumSlider.value;
    }

    private void OnUseNumChanged(float value)
    {
      this.UsedNum.text = ((int) value).ToString();
      this.gacham.UseTicketNum = (int) value;
      if ((UnityEngine.Object) this.BtnPlus != (UnityEngine.Object) null)
        this.BtnPlus.interactable = (double) this.TicketNumSlider.value + 1.0 <= (double) this.TicketNumSlider.maxValue;
      if (!((UnityEngine.Object) this.BtnMinus != (UnityEngine.Object) null))
        return;
      this.BtnMinus.interactable = (double) this.TicketNumSlider.value - 1.0 >= (double) this.TicketNumSlider.minValue;
    }

    private void OnMaxNum()
    {
      if (!((UnityEngine.Object) this.TicketNumSlider != (UnityEngine.Object) null))
        return;
      this.TicketNumSlider.value = this.TicketNumSlider.maxValue;
    }
  }
}
