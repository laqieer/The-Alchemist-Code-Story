// Decompiled with JetBrains decompiler
// Type: SRPG.RaidSettingsWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Close", FlowNode.PinTypes.Output, 1)]
  public class RaidSettingsWindow : MonoBehaviour, IFlowInterface
  {
    private int mLastTicketCount = -1;
    public RaidSettingsWindow.RaidSettingsEvent OnAccept;
    public string DebugQuestID;
    public SRPG_Button AddButton;
    public SRPG_Button SubButton;
    public Slider Slider;
    public Text CountText;
    public Text APText;
    public Text CostText;
    public SRPG_Button OKButton;
    public GameObject Ticket;
    private QuestParam mQuest;
    private int mCount;
    private int mCountMax;
    private int mLimit;
    private bool mStarted;
    private bool mInsideRefresh;

    public void Activated(int pinID)
    {
    }

    public void Setup(QuestParam quest, int count, int max)
    {
      this.mQuest = quest;
      this.mLimit = max;
      if (count >= 0)
        this.mCount = Mathf.Max(1, count);
      if (!this.mStarted)
        return;
      this.Refresh();
    }

    public int Count
    {
      get
      {
        return this.mCount;
      }
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.AddButton != (UnityEngine.Object) null)
        this.AddButton.onClick.AddListener(new UnityAction(this.OnAddClick));
      if ((UnityEngine.Object) this.SubButton != (UnityEngine.Object) null)
        this.SubButton.onClick.AddListener(new UnityAction(this.OnSubClick));
      if ((UnityEngine.Object) this.Slider != (UnityEngine.Object) null)
      {
        this.Slider.onValueChanged.AddListener(new UnityAction<float>(this.OnSliderChange));
        this.Slider.minValue = 1f;
      }
      if ((UnityEngine.Object) this.OKButton != (UnityEngine.Object) null)
        this.OKButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnOKClick));
      MonoSingleton<GameManager>.Instance.OnStaminaChange += new GameManager.StaminaChangeEvent(this.OnPlayerStaminaChange);
      this.mStarted = true;
      if (this.mQuest == null)
        return;
      this.Refresh();
    }

    private void OnOKClick(SRPG_Button button)
    {
      if (this.OnAccept == null)
        return;
      this.OnAccept(this);
    }

    private void OnPlayerStaminaChange()
    {
      if ((UnityEngine.Object) this == (UnityEngine.Object) null)
        MonoSingleton<GameManager>.Instance.OnStaminaChange -= new GameManager.StaminaChangeEvent(this.OnPlayerStaminaChange);
      else
        this.CountChanged();
    }

    private int GetTicketNum()
    {
      if (this.mQuest != null && !string.IsNullOrEmpty(this.mQuest.ticket))
        return MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mQuest.ticket);
      return -1;
    }

    private void Update()
    {
      int ticketNum = this.GetTicketNum();
      if (this.mLastTicketCount == ticketNum)
        return;
      this.mLastTicketCount = ticketNum;
      this.Refresh();
    }

    private void OnAddClick()
    {
      if (this.mCount >= this.mCountMax)
        return;
      ++this.mCount;
      if (!((UnityEngine.Object) this.Slider != (UnityEngine.Object) null))
        return;
      this.Slider.value = (float) this.mCount;
    }

    private void OnSubClick()
    {
      if (this.mCount <= 1)
        return;
      --this.mCount;
      if (!((UnityEngine.Object) this.Slider != (UnityEngine.Object) null))
        return;
      this.Slider.value = (float) this.mCount;
    }

    private void OnSliderChange(float value)
    {
      if (this.mInsideRefresh)
        return;
      this.mCount = Mathf.Clamp(Mathf.FloorToInt(value), 1, this.mCountMax);
      this.CountChanged();
    }

    private void CountChanged()
    {
      if ((UnityEngine.Object) this.AddButton != (UnityEngine.Object) null)
        this.AddButton.interactable = this.mCount < this.mCountMax;
      if ((UnityEngine.Object) this.SubButton != (UnityEngine.Object) null)
        this.SubButton.interactable = this.mCount > 1;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      bool flag = true;
      if ((UnityEngine.Object) this.APText != (UnityEngine.Object) null)
        this.APText.text = player.Stamina.ToString();
      if (this.mQuest != null)
      {
        int num = this.mQuest.RequiredApWithPlayerLv(player.Lv, true) * this.mCount;
        flag &= player.Stamina >= num;
        if ((UnityEngine.Object) this.CostText != (UnityEngine.Object) null)
        {
          this.CostText.text = num.ToString();
          Selectable component = this.CostText.GetComponent<Selectable>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.interactable = flag;
        }
      }
      if (!((UnityEngine.Object) this.CountText != (UnityEngine.Object) null))
        return;
      this.CountText.text = this.mCount.ToString();
      Selectable component1 = this.CountText.GetComponent<Selectable>();
      if (!((UnityEngine.Object) component1 != (UnityEngine.Object) null))
        return;
      component1.interactable = flag;
    }

    public void Refresh()
    {
      if (this.mQuest == null || string.IsNullOrEmpty(this.mQuest.ticket))
        return;
      this.mInsideRefresh = true;
      ItemParam data = string.IsNullOrEmpty(this.mQuest.ticket) ? (ItemParam) null : MonoSingleton<GameManager>.Instance.GetItemParam(this.mQuest.ticket);
      ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(data);
      int a = itemDataByItemParam == null ? 0 : itemDataByItemParam.Num;
      this.mLastTicketCount = a;
      this.mCountMax = Mathf.Min(a, this.mLimit);
      if (this.mQuest.GetChallangeLimit() > 0)
        this.mCountMax = Mathf.Min(this.mCountMax, this.mQuest.GetChallangeLimit() - this.mQuest.GetChallangeCount());
      this.mCount = Mathf.Min(this.mCount, this.mCountMax);
      if ((UnityEngine.Object) this.Ticket != (UnityEngine.Object) null)
      {
        DataSource.Bind<ItemData>(this.Ticket, itemDataByItemParam);
        DataSource.Bind<ItemParam>(this.Ticket, data);
      }
      if ((UnityEngine.Object) this.Slider != (UnityEngine.Object) null)
      {
        this.Slider.maxValue = (float) this.mCountMax;
        if (Mathf.FloorToInt(this.Slider.value) != this.mCount)
          this.Slider.value = (float) this.mCount;
      }
      this.CountChanged();
      GameParameter.UpdateAll(this.gameObject);
      this.mInsideRefresh = false;
    }

    public void Close()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    public delegate void RaidSettingsEvent(RaidSettingsWindow settings);
  }
}
