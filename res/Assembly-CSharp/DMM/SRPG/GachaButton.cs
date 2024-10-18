// Decompiled with JetBrains decompiler
// Type: SRPG.GachaButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GachaButton : MonoBehaviour
  {
    [SerializeField]
    private GameObject optionLayout2;
    [SerializeField]
    private GameObject optionLayout;
    [SerializeField]
    private GameObject paidOption;
    [SerializeField]
    private GameObject timerOption;
    [SerializeField]
    private GameObject timerObject;
    [SerializeField]
    private GameObject limitObject;
    [SerializeField]
    private GameObject haveOption;
    [SerializeField]
    private GameObject ticketState;
    [SerializeField]
    private GameObject stepupOption;
    [SerializeField]
    private GameObject stepupState;
    [SerializeField]
    private GameObject gachaBtn;
    [SerializeField]
    private GameObject costBG;
    [SerializeField]
    private Text costText;
    [SerializeField]
    private GameObject costFree;
    private GameObject mCurrentStepup10;
    private GameObject mCurrentStepup1;
    private GameObject mMaxStepup10;
    private GameObject mMaxStepup1;
    public ImageArray Limit10;
    public ImageArray Limit1;
    public ImageArray TimerH10;
    public ImageArray TimerH1;
    public ImageArray TimerM10;
    public ImageArray TimerM1;
    public ImageArray TimerS10;
    public ImageArray TimerS1;
    public Transform PaidCoin;
    private List<ImageArray> mPaidCoinNums;
    [SerializeField]
    private GameObject AppealOption;
    private ImageArray AppealBgs;
    private Text AppealMessageText;
    private bool mStarted;
    private GameManager gm;
    private StateMachine<GachaButton> mState;
    private bool mUpdateTrigger;
    private GachaButton.GachaCategoryType mCategoryType;
    private GachaCostType mCostType;
    private int mCost;
    private int mStepIndex;
    private int mStepMax;
    private int mTicketNum;
    private string mButtonText = string.Empty;
    private string mCategory = string.Empty;
    private Coroutine mUpdateCoroutine;
    private float mNextUpdateTime;
    private GachaButtonParam m_GachaButtonParam;
    [SerializeField]
    private GameObject DiscountObject;
    [SerializeField]
    private Text DiscountValueStrObject;
    [SerializeField]
    private Text ButtonTextObject;

    public GameObject OptionLayout2 => this.optionLayout2;

    public GameObject OptionLayout => this.optionLayout;

    public GameObject PaidOption => this.paidOption;

    public GameObject TimerOption => this.timerOption;

    public GameObject TimerObject => this.timerObject;

    public GameObject LimitObject => this.limitObject;

    public GameObject HaveOption => this.haveOption;

    public GameObject TicketState => this.ticketState;

    public GameObject StepupOption => this.stepupOption;

    public GameObject StepupState => this.stepupState;

    public GameObject GachaBtn => this.gachaBtn;

    public GameObject CostBG => this.costBG;

    public bool UpdateTrigger
    {
      get => this.mUpdateTrigger;
      set => this.mUpdateTrigger = value;
    }

    public bool IsUpdateTrigger => this.mUpdateTrigger;

    public GachaCostType CostType
    {
      get => this.mCostType;
      set => this.mCostType = value;
    }

    public bool IsSetCostType
    {
      get => this.mCostType != GachaCostType.NONE && this.mCostType != GachaCostType.ALL;
    }

    public GachaButton.GachaCategoryType CategoryType
    {
      get => this.mCategoryType;
      set => this.mCategoryType = value;
    }

    public bool IsSetCategoryType
    {
      get
      {
        return this.mCategoryType != GachaButton.GachaCategoryType.NONE && this.mCategoryType != GachaButton.GachaCategoryType.ALL;
      }
    }

    public int Cost
    {
      get => this.mCost;
      set => this.mCost = value;
    }

    public int StepIndex
    {
      get => this.mStepIndex;
      set => this.mStepIndex = value;
    }

    public int StepMax
    {
      get => this.mStepMax;
      set => this.mStepMax = value;
    }

    public int TicketNum
    {
      get => this.mTicketNum;
      set => this.mTicketNum = value;
    }

    public string ButtonText
    {
      get => this.mButtonText;
      set => this.mButtonText = value;
    }

    public string Category
    {
      get => this.mCategory;
      set => this.mCategory = value;
    }

    private void Start()
    {
      this.gm = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.optionLayout, (UnityEngine.Object) null))
        this.optionLayout.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.optionLayout2, (UnityEngine.Object) null))
        this.optionLayout2.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.paidOption, (UnityEngine.Object) null))
        this.paidOption.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.timerOption, (UnityEngine.Object) null))
        this.timerOption.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.haveOption, (UnityEngine.Object) null))
        this.haveOption.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.stepupOption, (UnityEngine.Object) null))
        this.stepupOption.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GachaBtn, (UnityEngine.Object) null))
        this.GachaBtn.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DiscountObject, (UnityEngine.Object) null))
        this.DiscountObject.SetActive(false);
      this.mState = new StateMachine<GachaButton>(this);
      this.mState.GotoState<GachaButton.State_Init>();
    }

    private void Update()
    {
      if (this.mState == null)
        return;
      this.mState.Update();
    }

    private void OnEnable()
    {
      if (this.mUpdateCoroutine != null)
      {
        this.StopCoroutine(this.mUpdateCoroutine);
        this.mUpdateCoroutine = (Coroutine) null;
      }
      if (!this.mStarted)
        return;
      this.RefreshTimerOption();
    }

    private bool IsObjectActivated(GameObject gobj)
    {
      return !UnityEngine.Object.op_Equality((UnityEngine.Object) gobj, (UnityEngine.Object) null) && gobj.activeInHierarchy;
    }

    private bool IsFreeCoin() => this.Category == "coin_1" && this.gm.Player.CheckFreeGachaCoin();

    private bool IsFreeGold()
    {
      return this.Category == "gold_1" && this.gm.Player.CheckFreeGachaGold() && !this.gm.Player.CheckFreeGachaGoldMax();
    }

    public bool SetGachaButtonEvent(UnityAction action)
    {
      if (action != null)
      {
        Button component = this.GachaBtn.GetComponent<Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          ((UnityEventBase) component.onClick).RemoveAllListeners();
          ((UnityEvent) component.onClick).AddListener(action);
          return true;
        }
      }
      return false;
    }

    public bool SetGachaButtonUIParameter(
      GachaCostType cost_type = GachaCostType.NONE,
      GachaButton.GachaCategoryType category_type = GachaButton.GachaCategoryType.NONE)
    {
      if (cost_type == GachaCostType.NONE || cost_type == GachaCostType.ALL || category_type == GachaButton.GachaCategoryType.NONE || category_type == GachaButton.GachaCategoryType.ALL)
        return false;
      this.mCostType = cost_type;
      this.mCategoryType = category_type;
      this.mUpdateTrigger = true;
      return true;
    }

    public bool SetupGachaButtonParam(GachaButtonParam _param)
    {
      if (_param == null)
        return false;
      this.m_GachaButtonParam = _param;
      return true;
    }

    public bool UpdateUI()
    {
      this.UpdateCostBG(this.m_GachaButtonParam.CostType);
      this.UpdateButtonImage(this.m_GachaButtonParam.CostType);
      string buttonText = this.m_GachaButtonParam.ButtonText;
      if (string.IsNullOrEmpty(buttonText))
        buttonText = LocalizedText.Get("sys.BTN_MULTI_GACHA", (object) this.m_GachaButtonParam.ExecNum);
      this.UpdateButtonText(buttonText, !this.m_GachaButtonParam.IsDiscount());
      if (this.m_GachaButtonParam.IsDiscount())
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DiscountObject, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DiscountValueStrObject, (UnityEngine.Object) null))
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(this.m_GachaButtonParam.DiscountItem);
          if (itemParam != null)
            this.DiscountValueStrObject.text = itemParam.value.ToString();
        }
        else
          DebugUtility.LogError("割引表示に必要なオブジェクトが指定されていません.");
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DiscountObject, (UnityEngine.Object) null))
        this.DiscountObject.SetActive(this.m_GachaButtonParam.IsDiscount());
      this.UpdateCostNumber();
      this.UpdateOptionObject();
      this.gachaBtn.SetActive(true);
      ((Component) this).gameObject.SetActive(true);
      this.mStarted = true;
      this.RefreshTimerOption();
      return true;
    }

    private bool UpdateCostBG(GachaCostType _cost_type = GachaCostType.NONE)
    {
      if (_cost_type == GachaCostType.NONE || _cost_type == GachaCostType.ALL)
      {
        DebugUtility.LogError("コストタイプが指定されていません");
        return false;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.costBG, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("コストアイコンを表示するオブジェクトが指定されていません");
        return false;
      }
      if (_cost_type == GachaCostType.TICKET)
      {
        ((Component) this.costBG.transform.parent).gameObject.SetActive(false);
        return true;
      }
      ((Component) this.costBG.transform.parent).gameObject.SetActive(true);
      this.costBG.GetComponent<ImageArray>().ImageIndex = _cost_type == GachaCostType.COIN || _cost_type == GachaCostType.COIN_P ? 0 : 1;
      return true;
    }

    private bool UpdateButtonImage(GachaCostType cost = GachaCostType.NONE)
    {
      if (cost == GachaCostType.NONE || cost == GachaCostType.ALL)
        return false;
      this.gachaBtn.GetComponent<ImageArray>().ImageIndex = cost != GachaCostType.COIN_P ? 0 : 1;
      return true;
    }

    private bool UpdateButtonText(string _button_text = "", bool _show = true)
    {
      if (string.IsNullOrEmpty(_button_text))
      {
        DebugUtility.LogError("ボタンテキストが指定されていません");
        return false;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.gachaBtn, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("表示するボタンの指定がありません");
        return false;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ButtonTextObject, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("ボタンテキストを表示するTextコンポーネントがありません");
        return false;
      }
      this.ButtonTextObject.text = _button_text;
      ((Component) this.ButtonTextObject).gameObject.SetActive(_show);
      return true;
    }

    private bool UpdateCostNumber(
      int _cost,
      int _exec_num,
      GachaCostType _cost_type,
      GachaCategory _gacha_category,
      bool _is_nouse_free,
      bool _is_discount)
    {
      ((Component) this.costText).gameObject.SetActive(false);
      this.costFree.gameObject.SetActive(false);
      bool flag = false;
      switch (_gacha_category)
      {
        case GachaCategory.NONE:
          if (_cost == 0 && !_is_discount)
          {
            flag = true;
            break;
          }
          break;
        case GachaCategory.DEFAULT_RARE:
          flag = _exec_num == 1 && _cost_type == GachaCostType.COIN && !_is_nouse_free && this.gm.Player.CheckFreeGachaCoin();
          break;
        default:
          flag = _exec_num == 1 && this.gm.Player.CheckFreeGachaGold();
          break;
      }
      this.costFree.gameObject.SetActive(flag);
      ((Component) this.costText).gameObject.SetActive(!flag);
      this.RefreshCostNum(_cost_type, _cost);
      return true;
    }

    private bool UpdateCostNumber()
    {
      if (this.m_GachaButtonParam == null)
      {
        DebugUtility.LogError("GachaButtonParamがNullです.");
        return false;
      }
      ((Component) this.costText).gameObject.SetActive(false);
      this.costFree.gameObject.SetActive(false);
      bool flag = false;
      if (this.m_GachaButtonParam.Category != GachaCategory.NONE)
        flag = this.m_GachaButtonParam.Category != GachaCategory.DEFAULT_RARE ? this.m_GachaButtonParam.ExecNum == 1 && this.gm.Player.CheckFreeGachaGold() : this.m_GachaButtonParam.ExecNum == 1 && this.m_GachaButtonParam.CostType == GachaCostType.COIN && !this.m_GachaButtonParam.IsNoUseFree && this.gm.Player.CheckFreeGachaCoin();
      else if (this.m_GachaButtonParam.IsDailyFree)
        flag = this.m_GachaButtonParam.IsDailyFree;
      else if (this.m_GachaButtonParam.FixCost == 0 && !this.m_GachaButtonParam.IsDiscount())
        flag = true;
      ((Component) this.costText).gameObject.SetActive(!flag);
      this.costFree.gameObject.SetActive(flag);
      this.RefreshCostNum(this.m_GachaButtonParam.CostType, this.m_GachaButtonParam.FixCost);
      return true;
    }

    private bool RefreshCostNum(GachaCostType costType, int cost = 0)
    {
      if (cost < 0)
        return false;
      this.costText.text = costType != GachaCostType.GOLD ? cost.ToString() : CurrencyBitmapText.CreateFormatedText(cost.ToString());
      return true;
    }

    private bool UpdateOptionObject()
    {
      GameUtility.SetGameObjectActive(this.optionLayout, false);
      GameUtility.SetGameObjectActive(this.optionLayout2, false);
      GameUtility.SetGameObjectActive(this.paidOption, false);
      GameUtility.SetGameObjectActive(this.timerOption, false);
      GameUtility.SetGameObjectActive(this.stepupOption, false);
      GameUtility.SetGameObjectActive(this.AppealOption, false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.stepupOption, (UnityEngine.Object) null) && this.m_GachaButtonParam.IsStepUp() && this.m_GachaButtonParam.IsShowStepup)
      {
        if (this.UpdateStepup(this.m_GachaButtonParam.StepIndex, this.m_GachaButtonParam.StepMax))
        {
          GameUtility.SetGameObjectActive(this.optionLayout, true);
          GameUtility.SetGameObjectActive(this.stepupOption, true);
        }
      }
      else if (this.m_GachaButtonParam.Category != GachaCategory.NONE && this.m_GachaButtonParam.ExecNum == 1 && this.m_GachaButtonParam.CostType != GachaCostType.COIN_P)
      {
        if (!this.m_GachaButtonParam.IsNoUseFree)
        {
          bool flag = true;
          long sec = 0;
          int num = 0;
          if (this.m_GachaButtonParam.Category == GachaCategory.DEFAULT_RARE)
          {
            sec = this.gm.Player.GetNextFreeGachaCoinCoolDownSec();
            num = 1 - this.gm.Player.FreeGachaCoin.num;
            flag = this.gm.Player.CheckFreeGachaCoin();
          }
          else if (this.m_GachaButtonParam.Category == GachaCategory.DEFAULT_NORMAL)
          {
            sec = this.gm.Player.GetNextFreeGachaGoldCoolDownSec();
            num = (int) this.gm.MasterParam.FixParam.FreeGachaGoldMax - this.gm.Player.FreeGachaGold.num;
            flag = this.gm.Player.CheckFreeGachaGold();
          }
          this.UpdateTimer(sec);
          this.UpdateLimit(num);
          this.TimerObject.SetActive(!flag);
          this.LimitObject.SetActive(flag);
          this.optionLayout.SetActive(true);
          this.timerOption.SetActive(true);
        }
      }
      else if (this.m_GachaButtonParam.CostType == GachaCostType.COIN_P && this.UpdatePaidCost())
      {
        this.optionLayout2.SetActive(true);
        this.paidOption.SetActive(true);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealOption, (UnityEngine.Object) null) && !string.IsNullOrEmpty(this.m_GachaButtonParam.AppealText) && this.UpdateAppeal(this.m_GachaButtonParam.AppealText, this.m_GachaButtonParam.AppealType))
        GameUtility.SetGameObjectActive(this.AppealOption, true);
      return true;
    }

    private bool UpdateStepup(int _index, int _max)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.StepupState, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("ステップアップ表示用のRootオブジェクトの指定がありません");
        return false;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCurrentStepup10, (UnityEngine.Object) null))
      {
        Transform transform = this.StepupState.transform.Find("current_10");
        this.mCurrentStepup10 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null) ? (GameObject) null : ((Component) transform).gameObject;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCurrentStepup1, (UnityEngine.Object) null))
      {
        Transform transform = this.StepupState.transform.Find("current_1");
        this.mCurrentStepup1 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null) ? (GameObject) null : ((Component) transform).gameObject;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mMaxStepup10, (UnityEngine.Object) null))
      {
        Transform transform = this.StepupState.transform.Find("max_10");
        this.mMaxStepup10 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null) ? (GameObject) null : ((Component) transform).gameObject;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mMaxStepup1, (UnityEngine.Object) null))
      {
        Transform transform = this.StepupState.transform.Find("max_1");
        this.mMaxStepup1 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null) ? (GameObject) null : ((Component) transform).gameObject;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCurrentStepup1, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCurrentStepup10, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mMaxStepup1, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mMaxStepup10, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("ステップアップ表示に必要なオブジェクトがありません");
        return false;
      }
      int num1 = (_index + 1) % 10;
      int num2 = (_index + 1) / 10;
      int num3 = _max % 10;
      int num4 = _max / 10;
      this.mCurrentStepup1.GetComponent<ImageArray>().ImageIndex = num1;
      this.mCurrentStepup10.GetComponent<ImageArray>().ImageIndex = num2;
      this.mMaxStepup10.GetComponent<ImageArray>().ImageIndex = num4;
      this.mMaxStepup1.GetComponent<ImageArray>().ImageIndex = num3;
      this.mCurrentStepup10.SetActive(num2 > 0);
      this.mMaxStepup10.SetActive(num4 > 0);
      return true;
    }

    private bool UpdateTimer(long sec = 0)
    {
      int num1 = (int) (sec / 3600L);
      int num2 = (int) (sec % 3600L / 60L);
      int num3 = (int) (sec % 60L);
      int num4 = num1 / 10;
      int num5 = num1 % 10;
      int num6 = num2 / 10;
      int num7 = num2 % 10;
      int num8 = num3 / 10;
      int num9 = num3 % 10;
      this.TimerH10.ImageIndex = num4 >= 10 ? 0 : num4;
      this.TimerH1.ImageIndex = num5 >= 10 ? 0 : num5;
      this.TimerM10.ImageIndex = num6 >= 10 ? 0 : num6;
      this.TimerM1.ImageIndex = num7 >= 10 ? 0 : num7;
      this.TimerS10.ImageIndex = num8 >= 10 ? 0 : num8;
      this.TimerS1.ImageIndex = num9 >= 10 ? 0 : num9;
      return true;
    }

    private bool UpdateLimit(int num)
    {
      if (num <= 0)
      {
        this.Limit10.ImageIndex = 0;
        this.Limit1.ImageIndex = 0;
        return false;
      }
      int num1 = num / 10;
      int num2 = num % 10;
      this.Limit10.ImageIndex = num1 >= 10 ? 0 : num1;
      ((Component) this.Limit10).gameObject.SetActive(num1 > 0);
      this.Limit1.ImageIndex = num2 >= 10 ? 0 : num2;
      return true;
    }

    [DebuggerHidden]
    private IEnumerator UpdateOption()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaButton.\u003CUpdateOption\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void SetUpdateOption(float interval)
    {
      if (!((Component) this).gameObject.activeInHierarchy)
        return;
      if ((double) interval <= 0.0)
      {
        if (this.mUpdateCoroutine == null)
          return;
        this.StopCoroutine(this.mUpdateCoroutine);
      }
      else
      {
        this.mNextUpdateTime = Time.time + interval;
        if (this.mUpdateCoroutine != null)
          return;
        this.mUpdateCoroutine = this.StartCoroutine(this.UpdateOption());
      }
    }

    private bool RefreshTimerOption()
    {
      if (this.m_GachaButtonParam.Category == GachaCategory.NONE || this.m_GachaButtonParam.IsNoUseFree)
      {
        this.TimerObject.SetActive(false);
        this.LimitObject.SetActive(false);
        if (this.mUpdateCoroutine != null)
        {
          this.StopCoroutine(this.mUpdateCoroutine);
          this.mUpdateCoroutine = (Coroutine) null;
        }
        return true;
      }
      bool flag = true;
      long sec = 0;
      int num = 0;
      if (this.m_GachaButtonParam.Category == GachaCategory.DEFAULT_RARE)
      {
        sec = this.gm.Player.GetNextFreeGachaCoinCoolDownSec();
        num = 1 - this.gm.Player.FreeGachaCoin.num;
        flag = this.gm.Player.CheckFreeGachaCoin();
      }
      else if (this.m_GachaButtonParam.Category == GachaCategory.DEFAULT_NORMAL)
      {
        sec = this.gm.Player.GetNextFreeGachaGoldCoolDownSec();
        num = (int) this.gm.MasterParam.FixParam.FreeGachaGoldMax - this.gm.Player.FreeGachaGold.num;
        flag = this.gm.Player.CheckFreeGachaGold();
      }
      this.UpdateCostNumber(this.m_GachaButtonParam.FixCost, this.m_GachaButtonParam.ExecNum, this.m_GachaButtonParam.CostType, this.m_GachaButtonParam.Category, this.m_GachaButtonParam.IsNoUseFree, this.m_GachaButtonParam.IsDiscount());
      this.UpdateTimer(sec);
      this.UpdateLimit(num);
      this.TimerObject.SetActive(!flag);
      this.LimitObject.SetActive(flag);
      this.SetUpdateOption(1f);
      return true;
    }

    private bool UpdatePaidCost()
    {
      this.RefreshPaidCost(this.gm.Player.PaidCoin);
      return true;
    }

    private bool RefreshPaidCost(int cost = 0)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.PaidCoin, (UnityEngine.Object) null))
        return false;
      int childCount = this.PaidCoin.childCount;
      int num1 = (int) Math.Log10(cost <= 0 ? 1.0 : (double) cost) + 1;
      int num2 = cost;
      for (int index = childCount; index > 0; --index)
      {
        Transform transform = this.PaidCoin.Find("coin" + Mathf.Pow(10f, (float) (index - 1)).ToString());
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) transform, (UnityEngine.Object) null))
        {
          if (num1 < index)
          {
            ((Component) transform).gameObject.SetActive(false);
          }
          else
          {
            int num3 = (int) Mathf.Pow(10f, (float) (index - 1));
            int num4 = num2 / num3;
            ((Component) transform).gameObject.SetActive(true);
            ((Component) transform).GetComponent<ImageArray>().ImageIndex = num4;
            num2 %= num3;
          }
        }
      }
      return true;
    }

    private bool UpdateAppeal(string message, int _appeal_type = 0)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.AppealMessageText, (UnityEngine.Object) null))
      {
        Text componentInChildren = this.AppealOption.GetComponentInChildren<Text>();
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("訴求文言を表示するオブジェクトがありません");
          return false;
        }
        this.AppealMessageText = componentInChildren;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.AppealBgs, (UnityEngine.Object) null))
      {
        ImageArray componentInChildren = this.AppealOption.GetComponentInChildren<ImageArray>();
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("訴求吹き出し画像を表示するオブジェクトがありません");
          return false;
        }
        this.AppealBgs = componentInChildren;
      }
      if (_appeal_type < this.AppealBgs.Images.Length)
        this.AppealBgs.ImageIndex = _appeal_type;
      this.AppealMessageText.text = message;
      return true;
    }

    public void OnClick()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.gachaBtn, (UnityEngine.Object) null))
        return;
      Button component = this.GachaBtn.GetComponent<Button>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      ((UnityEvent) component.onClick).Invoke();
    }

    public enum GGachaCostType
    {
      NONE,
      COIN,
      COIN_P,
      GOLD,
      TICKET,
      FREE_COIN,
      FREE_GOLD,
      ALL,
    }

    public enum GachaNumType
    {
      NONE,
      SINGLE,
      MULTI,
      ALL,
    }

    public enum GachaCategoryType
    {
      NONE,
      RARE,
      NORMAL,
      STEPUP,
      ALL,
    }

    private class State_Init : State<GachaButton>
    {
      public override void Begin(GachaButton self)
      {
        base.Begin(self);
        self.mState.GotoState<GachaButton.State_Wait>();
      }
    }

    private class State_Wait : State<GachaButton>
    {
      public override void Begin(GachaButton self) => base.Begin(self);

      public override void Update(GachaButton self)
      {
        base.Update(self);
        if (!self.UpdateTrigger)
          return;
        self.mState.GotoState<GachaButton.State_UpdateUI>();
      }
    }

    private class State_UpdateUI : State<GachaButton>
    {
      public override void Begin(GachaButton self)
      {
        base.Begin(self);
        self.UpdateUI();
        self.mUpdateTrigger = false;
        self.mState.GotoState<GachaButton.State_Wait>();
      }
    }
  }
}
