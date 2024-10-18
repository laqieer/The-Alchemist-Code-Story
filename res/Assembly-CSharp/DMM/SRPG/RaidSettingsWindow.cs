// Decompiled with JetBrains decompiler
// Type: SRPG.RaidSettingsWindow
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
  [FlowNode.Pin(1, "Close", FlowNode.PinTypes.Output, 1)]
  public class RaidSettingsWindow : MonoBehaviour, IFlowInterface
  {
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
    [SerializeField]
    private Text TitleText;
    [SerializeField]
    private GameObject GoCostAp;
    [SerializeField]
    private GameObject GoCostItem;
    [SerializeField]
    private Text GoCostItemCost;
    [SerializeField]
    private GameObject StoryExChallengeCount;
    private QuestParam mQuest;
    private int mCount;
    private int mCountMax;
    private int mLimit;
    private bool mStarted;
    private int mLastTicketCount = -1;
    private bool mInsideRefresh;
    private ItemParam GenAdvBossChItemParam;
    private int GenAdvBossChItemNeedNum;

    public void Activated(int pinID)
    {
    }

    public void Setup(QuestParam quest, int count, int max)
    {
      this.mQuest = quest;
      this.mLimit = max;
      if (count >= 0)
        this.mCount = Mathf.Max(1, count);
      if (this.mQuest != null)
      {
        bool isGenAdvBoss = this.mQuest.IsGenAdvBoss;
        if (Object.op_Implicit((Object) this.GoCostAp))
          this.GoCostAp.SetActive(!isGenAdvBoss);
        if (Object.op_Implicit((Object) this.GoCostItem))
        {
          this.GoCostItem.SetActive(isGenAdvBoss);
          if (isGenAdvBoss)
          {
            if (Object.op_Implicit((Object) this.TitleText))
              this.TitleText.text = LocalizedText.Get("sys.SKIPBATTLE_MESSAGE_GEN_ADV_BOSS");
            if (this.mQuest.IsGenesisBoss)
              GenesisBossInfo.GetBossChallengeItemInfo(ref this.GenAdvBossChItemParam, ref this.GenAdvBossChItemNeedNum);
            if (this.mQuest.IsAdvanceBoss)
              AdvanceBossInfo.GetBossChallengeItemInfo(ref this.GenAdvBossChItemParam, ref this.GenAdvBossChItemNeedNum);
            if (this.GenAdvBossChItemParam != null)
            {
              ItemData data = new ItemData();
              data.Setup(0L, this.GenAdvBossChItemParam, this.GenAdvBossChItemNeedNum * this.mCount);
              DataSource.Bind<ItemData>(this.GoCostItem, data);
              ItemIcon component = this.GoCostItem.GetComponent<ItemIcon>();
              if (Object.op_Implicit((Object) component))
                component.UpdateValue();
            }
            this.UpdateCostItem();
          }
        }
      }
      bool active = false;
      if (this.mQuest != null)
        active = this.mQuest.type == QuestTypes.StoryExtra;
      GameUtility.SetGameObjectActive(this.StoryExChallengeCount, active);
      if (!this.mStarted)
        return;
      this.Refresh();
    }

    public int Count => this.mCount;

    private void Start()
    {
      if (Object.op_Inequality((Object) this.AddButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.AddButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnAddClick)));
      }
      if (Object.op_Inequality((Object) this.SubButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.SubButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnSubClick)));
      }
      if (Object.op_Inequality((Object) this.Slider, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<float>) this.Slider.onValueChanged).AddListener(new UnityAction<float>((object) this, __methodptr(OnSliderChange)));
        this.Slider.minValue = 1f;
      }
      if (Object.op_Inequality((Object) this.OKButton, (Object) null))
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
      if (Object.op_Equality((Object) this, (Object) null))
        MonoSingleton<GameManager>.Instance.OnStaminaChange -= new GameManager.StaminaChangeEvent(this.OnPlayerStaminaChange);
      else
        this.CountChanged(true);
    }

    private int GetTicketNum()
    {
      return this.mQuest != null && !string.IsNullOrEmpty(this.mQuest.ticket) ? MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mQuest.ticket) : -1;
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
      if (!Object.op_Inequality((Object) this.Slider, (Object) null))
        return;
      this.Slider.value = (float) this.mCount;
    }

    private void OnSubClick()
    {
      if (this.mCount <= 1)
        return;
      --this.mCount;
      if (!Object.op_Inequality((Object) this.Slider, (Object) null))
        return;
      this.Slider.value = (float) this.mCount;
    }

    private void OnSliderChange(float value)
    {
      if (this.mInsideRefresh)
        return;
      this.mCount = Mathf.Clamp(Mathf.FloorToInt(value), 1, this.mCountMax);
      this.mCount = Mathf.Min(this.mCount, this.mCountMax);
      this.CountChanged();
    }

    private void CountChanged(bool is_stamina_change = false)
    {
      if (Object.op_Inequality((Object) this.AddButton, (Object) null))
        ((Selectable) this.AddButton).interactable = this.mCount < this.mCountMax;
      if (Object.op_Inequality((Object) this.SubButton, (Object) null))
        ((Selectable) this.SubButton).interactable = this.mCount > 1;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      bool flag = true;
      if (this.mQuest != null)
      {
        if (!this.mQuest.IsGenAdvBoss)
        {
          if (Object.op_Inequality((Object) this.APText, (Object) null))
            this.APText.text = player.Stamina.ToString();
          int num = this.mQuest.RequiredApWithPlayerLv(player.Lv) * this.mCount;
          flag &= player.Stamina >= num;
          if (Object.op_Inequality((Object) this.CostText, (Object) null))
          {
            this.CostText.text = num.ToString();
            Selectable component = ((Component) this.CostText).GetComponent<Selectable>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.interactable = flag;
          }
        }
        else
          this.UpdateCostItem();
      }
      if (!Object.op_Inequality((Object) this.CountText, (Object) null))
        return;
      this.CountText.text = this.mCount.ToString();
      Selectable component1 = ((Component) this.CountText).GetComponent<Selectable>();
      if (!Object.op_Inequality((Object) component1, (Object) null))
        return;
      component1.interactable = flag;
    }

    private void UpdateCostItem()
    {
      if (!Object.op_Implicit((Object) this.GoCostItemCost))
        return;
      this.GoCostItemCost.text = (this.GenAdvBossChItemNeedNum * this.mCount).ToString();
    }

    public void Refresh()
    {
      if (this.mQuest == null || string.IsNullOrEmpty(this.mQuest.ticket))
        return;
      this.mInsideRefresh = true;
      ItemParam itemParam = string.IsNullOrEmpty(this.mQuest.ticket) ? (ItemParam) null : MonoSingleton<GameManager>.Instance.GetItemParam(this.mQuest.ticket);
      ItemData itemDataByItemParam1 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(itemParam);
      int num1 = itemDataByItemParam1 == null ? 0 : itemDataByItemParam1.Num;
      this.mLastTicketCount = num1;
      this.mCountMax = Mathf.Min(num1, this.mLimit);
      this.mCountMax = QuestParam.GetRaidTicketCount_LimitMax(this.mQuest, this.mCountMax);
      if (this.mQuest.IsGenAdvBoss && this.GenAdvBossChItemParam != null)
      {
        ItemData itemDataByItemParam2 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(this.GenAdvBossChItemParam);
        int num2 = itemDataByItemParam2 == null ? 0 : itemDataByItemParam2.Num;
        if (this.GenAdvBossChItemNeedNum != 0)
          this.mCountMax = Mathf.Min(this.mCountMax, num2 / this.GenAdvBossChItemNeedNum);
      }
      this.mCount = Mathf.Min(this.mCount, this.mCountMax);
      if (Object.op_Inequality((Object) this.Ticket, (Object) null))
      {
        DataSource.Bind<ItemData>(this.Ticket, itemDataByItemParam1);
        DataSource.Bind<ItemParam>(this.Ticket, itemParam);
      }
      if (Object.op_Inequality((Object) this.Slider, (Object) null))
      {
        this.Slider.maxValue = (float) this.mCountMax;
        if (Mathf.FloorToInt(this.Slider.value) != this.mCount)
          this.Slider.value = (float) this.mCount;
      }
      this.CountChanged();
      GameParameter.UpdateAll(((Component) this).gameObject);
      this.mInsideRefresh = false;
    }

    public void Close() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);

    public delegate void RaidSettingsEvent(RaidSettingsWindow settings);
  }
}
