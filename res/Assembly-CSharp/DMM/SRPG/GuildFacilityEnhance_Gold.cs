// Decompiled with JetBrains decompiler
// Type: SRPG.GuildFacilityEnhance_Gold
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
  [FlowNode.Pin(30, "素材選択クリア", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(40, "施設強化開始", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(70, "データ、表示を更新", FlowNode.PinTypes.Input, 70)]
  [FlowNode.Pin(80, "施設強化演出", FlowNode.PinTypes.Input, 80)]
  [FlowNode.Pin(1010, "施設強化リクエスト", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(1020, "施設強化演出完了", FlowNode.PinTypes.Output, 1020)]
  public class GuildFacilityEnhance_Gold : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_CLEAR_ENHANCE_GOLD = 30;
    private const int PIN_INPUT_START_FACILITY_ENHANCE = 40;
    private const int PIN_INPUT_RESET_ALL = 70;
    private const int PIN_INPUT_START_ENHANCE_ANIMATION = 80;
    private const int PIN_OUTPUT_REQUEST_FACILITY_ENHANCE = 1010;
    private const int PIN_OUTPUT_START_ENHANCE_ANIMATION = 1020;
    [SerializeField]
    private float ENHANCE_EFFECT_TIME = 1f;
    [SerializeField]
    private Slider mFacilityEnhanceSlider;
    [SerializeField]
    private Slider mFacilityExpGauge;
    [SerializeField]
    private Slider mFacilityInvestLimitGauge;
    [SerializeField]
    private Text mFacilityNextExpText;
    [SerializeField]
    private Text mFacilityEnhanceAfterLevelText;
    [SerializeField]
    private GameObject mFacilityEffectTemplate;
    [SerializeField]
    private Text mCurrentInvestPoint;
    [SerializeField]
    private Button mEnhanceSubmitButton;
    [SerializeField]
    private Button mEnhanceClearButton;
    [SerializeField]
    private GameObject mEnhanceLevelupEffect;
    [SerializeField]
    private Button mEnhanceGoldPlusButton;
    [SerializeField]
    private Button mEnhanceGoldMinusButton;
    [SerializeField]
    private Button mEnhanceGoldMaxButton;
    [SerializeField]
    private Text mSelectedGoldText;
    [SerializeField]
    private Text mIncrementPlusText;
    [SerializeField]
    private Text mIncrementMinusText;
    [SerializeField]
    private GameObject mFacilityLevelMaxMask;
    [SerializeField]
    private GameObject mNormalLevelObject;
    [SerializeField]
    private GameObject mMaxLevelObject;
    private GuildFacilityData mTargetFacility;
    private long mSelectedGold;
    private List<GameObject> mCreatedFacilityEffectTexts = new List<GameObject>();
    private long mBeforeEnhanceInvestPoint;
    private GameObject mErrorWindow;
    private GuildFacilityEnhance_Gold.EffectCallBack mCallback;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 30:
          this.ClearSelectEnhanceGold();
          break;
        case 40:
          this.StartGuildFacilityEnhance();
          break;
        case 70:
          this.ResetAll();
          break;
        case 80:
          this.StartCoroutine(this.LevelupAnimation(this.mTargetFacility, new GuildFacilityEnhance_Gold.EffectCallBack(this.EndLevelupAnimation)));
          break;
      }
    }

    private void Start()
    {
      this.Init();
      this.ResetAll();
    }

    private void Update()
    {
      ((Selectable) this.mEnhanceSubmitButton).interactable = this.mSelectedGold > 0L;
      ((Selectable) this.mEnhanceClearButton).interactable = this.mSelectedGold > 0L;
      ((Selectable) this.mEnhanceGoldMaxButton).interactable = (this.ErrorCheck_LvMax() || this.ErrorCheck_InvestLimit() ? 1 : (this.ErrorCheck_HaveGold() ? 1 : 0)) == 0;
      ((Selectable) this.mEnhanceGoldPlusButton).interactable = (this.ErrorCheck_LvMax() || this.ErrorCheck_InvestLimit() ? 1 : (this.ErrorCheck_HaveGold() ? 1 : 0)) == 0;
      ((Selectable) this.mEnhanceGoldMinusButton).interactable = this.mSelectedGold > 0L;
    }

    private void Init()
    {
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        this.mTargetFacility = component.list.GetObject<GuildFacilityData>(GuildSVB_Key.FACILITY, (GuildFacilityData) null);
      this.mIncrementPlusText.text = string.Format(LocalizedText.Get("sys.GUILDFACILITY_ENHANCE_INCREMENT_PLUS_FORMAT"), (object) this.mTargetFacility.Param.Increment);
      this.mIncrementMinusText.text = string.Format(LocalizedText.Get("sys.GUILDFACILITY_ENHANCE_INCREMENT_MINUS_FORMAT"), (object) this.mTargetFacility.Param.Increment);
    }

    private void ResetAll()
    {
      this.mTargetFacility = Array.Find<GuildFacilityData>(MonoSingleton<GameManager>.Instance.Player.Guild.Facilities, (Predicate<GuildFacilityData>) (facility => facility.Iname == this.mTargetFacility.Iname));
      DataSource.Bind<GuildFacilityData>(((Component) this).gameObject, this.mTargetFacility);
      this.Refresh_FacilityEnhance();
      this.ClearSelectEnhanceGold();
    }

    private long GetSelectedTotalGold(bool is_enhance_animation = false, bool is_simple_total = false)
    {
      long mSelectedGold = this.mSelectedGold;
      if (is_simple_total)
        return mSelectedGold;
      long val2 = this.mTargetFacility.Param.DayLimitInvest - (!is_enhance_animation ? (long) this.mTargetFacility.InvestPoint : this.mBeforeEnhanceInvestPoint);
      return Math.Min(mSelectedGold, val2);
    }

    private long GetRestInvestPoint()
    {
      return Math.Max(0L, this.mTargetFacility.Param.DayLimitInvest - (long) this.mTargetFacility.InvestPoint);
    }

    private void StartGuildFacilityEnhance()
    {
      if (this.mSelectedGold <= 0L)
        return;
      this.RequestGuildFacilityEnhance(((Component) this).gameObject);
    }

    private void RequestGuildFacilityEnhance(GameObject obj)
    {
      GuildEnhanceMaterial guildEnhanceMaterial = new GuildEnhanceMaterial();
      guildEnhanceMaterial.facility_iname = this.mTargetFacility.Iname;
      guildEnhanceMaterial.materials = new List<EnhanceMaterial>();
      guildEnhanceMaterial.gold = this.mSelectedGold;
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        component.list.SetObject(GuildSVB_Key.ENHANCE_MATERIAL, (object) guildEnhanceMaterial);
      this.mBeforeEnhanceInvestPoint = (long) this.mTargetFacility.InvestPoint;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1010);
    }

    private void SetActive_FacilityLevelMaxUI(bool is_level_max)
    {
      this.mFacilityLevelMaxMask.SetActive(is_level_max);
      this.mNormalLevelObject.SetActive(!is_level_max);
      this.mMaxLevelObject.SetActive(is_level_max);
    }

    private void Refresh_FacilityEnhance()
    {
      this.SetActive_FacilityLevelMaxUI(this.mTargetFacility.Level >= GuildFacilityData.GetLevelMax(this.mTargetFacility.Param.Type));
    }

    private void Refresh_FacilityExp(long total_add_gold = -1)
    {
      bool flag = total_add_gold >= 0L;
      if (!flag)
        total_add_gold = this.GetSelectedTotalGold();
      int new_level = 0;
      long rest_next_exp = 0;
      GuildFacilityData.SimlateEnhance(this.mTargetFacility, this.Gold2Exp(total_add_gold), out new_level, out rest_next_exp);
      this.mFacilityEnhanceAfterLevelText.text = new_level.ToString();
      this.mFacilityNextExpText.text = this.Exp2Gold(rest_next_exp).ToString();
      int levelMax = GuildFacilityData.GetLevelMax(this.mTargetFacility.Param.Type);
      float num1 = (float) this.Exp2Gold(GuildFacilityData.GetNeedExp(new_level, new_level + 1, this.mTargetFacility.Param.Type));
      float num2 = new_level >= levelMax ? 1f : num1;
      float num3 = this.mFacilityExpGauge.maxValue;
      if (new_level < levelMax)
        num3 = (float) (this.Exp2Gold(this.mTargetFacility.Exp) + total_add_gold - this.Exp2Gold(GuildFacilityData.GetNeedExp(1, new_level, this.mTargetFacility.Param.Type)));
      this.mFacilityExpGauge.maxValue = num2;
      this.mFacilityExpGauge.minValue = 0.0f;
      this.mFacilityExpGauge.value = num3;
      this.Refresh_FacilityEffect(new_level);
      long num4 = total_add_gold;
      if (new_level >= levelMax)
        num4 = this.Exp2Gold(GuildFacilityData.GetNeedExp(1, levelMax, this.mTargetFacility.Param.Type) - this.mTargetFacility.Exp);
      long num5 = Math.Min(this.mTargetFacility.Param.DayLimitInvest, (!flag ? (long) this.mTargetFacility.InvestPoint : this.mBeforeEnhanceInvestPoint) + num4);
      this.mCurrentInvestPoint.text = num5.ToString();
      this.mFacilityInvestLimitGauge.maxValue = (float) this.mTargetFacility.Param.DayLimitInvest;
      this.mFacilityInvestLimitGauge.minValue = 0.0f;
      this.mFacilityInvestLimitGauge.value = (float) num5;
      this.mSelectedGoldText.text = this.mSelectedGold.ToString();
    }

    private void Refresh_FacilityEffect(int after_level)
    {
      if (this.mTargetFacility == null)
        return;
      this.mFacilityEffectTemplate.SetActive(false);
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < this.mTargetFacility.Param.Effects.Length; ++index)
      {
        if (this.mTargetFacility.Param.Effects[index].lv > this.mTargetFacility.Level && this.mTargetFacility.Param.Effects[index].shop_count >= 1)
        {
          num2 = this.mTargetFacility.Param.Effects[index].lv;
          ++num1;
          break;
        }
      }
      GuildFacilityEffectParam effect1 = this.mTargetFacility.GetEffect();
      GuildFacilityEffectParam effect2 = this.mTargetFacility.Param.GetEffect(after_level);
      for (int index = 0; index < this.mCreatedFacilityEffectTexts.Count; ++index)
        this.mCreatedFacilityEffectTexts[index].SetActive(false);
      int num3 = effect2.GetEffectCount() + num1 - this.mCreatedFacilityEffectTexts.Count;
      for (int index = 0; index < num3; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mFacilityEffectTemplate);
        gameObject.transform.SetParent(this.mFacilityEffectTemplate.transform.parent, false);
        this.mCreatedFacilityEffectTexts.Add(gameObject);
      }
      int index1 = 0;
      if (effect1.member_count > 0)
      {
        this.mCreatedFacilityEffectTexts[index1].SetActive(true);
        SerializeValueBehaviour component = this.mCreatedFacilityEffectTexts[index1].GetComponent<SerializeValueBehaviour>();
        component.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_NAME).text = LocalizedText.Get("sys.GUILD_FACILITY_EFFECT_MEMBER_COUNT");
        component.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_VALUE).text = effect1.member_count.ToString();
        this.SetGuildFacilityEffectPlus(component, effect2.member_count - effect1.member_count);
        ++index1;
      }
      if (effect1.sub_master > 0)
      {
        this.mCreatedFacilityEffectTexts[index1].SetActive(true);
        SerializeValueBehaviour component = this.mCreatedFacilityEffectTexts[index1].GetComponent<SerializeValueBehaviour>();
        component.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_NAME).text = LocalizedText.Get("sys.GUILD_FACILITY_EFFECT_SUBMASTER_COUNT");
        component.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_VALUE).text = effect1.sub_master.ToString();
        this.SetGuildFacilityEffectPlus(component, effect2.sub_master - effect1.sub_master);
        ++index1;
      }
      if (effect2.shop_count > 0)
      {
        int num4 = effect1.shop_count;
        int dif_value = effect2.shop_count - effect1.shop_count;
        if (effect1.shop_count <= 0)
        {
          num4 = 1;
          dif_value = effect2.shop_count - 1;
        }
        this.mCreatedFacilityEffectTexts[index1].SetActive(true);
        SerializeValueBehaviour component = this.mCreatedFacilityEffectTexts[index1].GetComponent<SerializeValueBehaviour>();
        component.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_NAME).text = string.Format(LocalizedText.Get("sys.GUILD_FACILITY_EFFECT_RELEASE_SHOP"), (object) num4);
        component.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_VALUE).text = string.Empty;
        this.SetGuildFacilityEffectPlus(component, dif_value);
        ++index1;
      }
      if (num2 <= 0)
        return;
      this.mCreatedFacilityEffectTexts[index1].SetActive(true);
      SerializeValueBehaviour component1 = this.mCreatedFacilityEffectTexts[index1].GetComponent<SerializeValueBehaviour>();
      component1.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_NAME).text = string.Format(LocalizedText.Get("sys.GUILD_FACILITY_EFFECT_RELEASE_SHOP_NEXT"), (object) num2);
      component1.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_VALUE).text = string.Empty;
      this.SetGuildFacilityEffectPlus(component1, 0);
      int num5 = index1 + 1;
    }

    private void SetGuildFacilityEffectPlus(SerializeValueBehaviour svb, int dif_value)
    {
      ((Component) svb.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_PLUS_ICON)).gameObject.SetActive(dif_value != 0);
      ((Component) svb.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_PLUS)).gameObject.SetActive(dif_value != 0);
      svb.list.GetComponent<Text>(GuildFacilitySVB_Key.EFFECT_PLUS).text = dif_value.ToString();
    }

    private void SetupInvestGuildFacilityUI()
    {
      int levelMax = GuildFacilityData.GetLevelMax(this.mTargetFacility.Param.Type);
      long num1 = this.mTargetFacility.Param.DayLimitInvest - (long) this.mTargetFacility.InvestPoint;
      long num2 = this.Exp2Gold(GuildFacilityData.GetNeedExp(1, levelMax, this.mTargetFacility.Param.Type) - this.mTargetFacility.Exp);
      long gold1 = (long) MonoSingleton<GameManager>.Instance.Player.Gold;
      long gold2 = num1;
      if (gold2 > num2)
        gold2 = num2;
      if (gold2 > gold1)
        gold2 = gold1;
      ((UnityEventBase) this.mFacilityEnhanceSlider.onValueChanged).RemoveAllListeners();
      this.mFacilityEnhanceSlider.maxValue = (float) this.Gold2Exp(gold2);
      this.mFacilityEnhanceSlider.minValue = 0.0f;
      this.mFacilityEnhanceSlider.value = (float) this.Gold2Exp(this.mSelectedGold);
      // ISSUE: method pointer
      ((UnityEvent<float>) this.mFacilityEnhanceSlider.onValueChanged).AddListener(new UnityAction<float>((object) this, __methodptr(OnChange_GuildFacilityEnhanceGoldSlider)));
    }

    private void ClearSelectEnhanceGold()
    {
      this.mSelectedGold = 0L;
      this.Refresh_FacilityExp();
      this.SetupInvestGuildFacilityUI();
      this.RefreshFacilityEnhanceSlider();
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private bool ErrorCheck_LvMax()
    {
      return this.mTargetFacility != null && this.GetSelectedTotalGold(is_simple_total: true) >= this.Exp2Gold(GuildFacilityData.GetNeedExp(1, GuildFacilityData.GetLevelMax(this.mTargetFacility.Param.Type), this.mTargetFacility.Param.Type) - this.mTargetFacility.Exp);
    }

    private bool ErrorCheck_InvestLimit()
    {
      return this.mTargetFacility != null && this.GetSelectedTotalGold(is_simple_total: true) >= this.GetRestInvestPoint();
    }

    private bool ErrorCheck_HaveGold()
    {
      return this.mTargetFacility != null && this.mSelectedGold >= (long) MonoSingleton<GameManager>.Instance.Player.Gold;
    }

    private long GetNeedNum_LvMax()
    {
      if (this.mTargetFacility == null)
        return 0;
      long num = this.Exp2Gold(GuildFacilityData.GetNeedExp(1, GuildFacilityData.GetLevelMax(this.mTargetFacility.Param.Type), this.mTargetFacility.Param.Type) - this.mTargetFacility.Exp);
      return num <= 0L ? 0L : num;
    }

    private long GetNeedNum_InvestLimit()
    {
      return this.mTargetFacility == null ? 0L : this.GetRestInvestPoint();
    }

    private void RefreshFacilityEnhanceSlider()
    {
      bool flag1 = (this.ErrorCheck_LvMax() ? 1 : (this.ErrorCheck_InvestLimit() ? 1 : 0)) == 0;
      bool flag2 = this.mSelectedGold > 0L;
      ((Selectable) this.mFacilityEnhanceSlider).interactable = flag1 || flag2;
    }

    private long Exp2Gold(long exp) => exp * (long) this.mTargetFacility.Param.Increment;

    private long Gold2Exp(long gold)
    {
      return (long) Math.Floor((double) gold / (double) this.mTargetFacility.Param.Increment);
    }

    private string GetEnhancedNoticeText()
    {
      string enhancedNoticeText = string.Empty;
      if (this.mTargetFacility.Param.Type == GuildFacilityParam.eFacilityType.GUILD_SHOP)
      {
        long selectedTotalGold = this.GetSelectedTotalGold();
        int level = this.mTargetFacility.Level;
        int new_level = 0;
        long rest_next_exp = 0;
        GuildFacilityData.SimlateEnhance(this.mTargetFacility, this.Gold2Exp(selectedTotalGold), out new_level, out rest_next_exp);
        if (level >= new_level)
          return string.Empty;
        GuildFacilityEffectParam effect = this.mTargetFacility.Param.GetEffect(level);
        int num = this.mTargetFacility.Param.GetEffect(new_level).shop_count - effect.shop_count;
        if (num <= 0)
          return string.Empty;
        int shopCount = effect.shop_count;
        for (int index = 0; index < num; ++index)
        {
          ++shopCount;
          enhancedNoticeText = enhancedNoticeText + string.Format(LocalizedText.Get("sys.GUILDFACILITY_ENHANCE_ENHANCED_NOTICE1"), (object) shopCount) + "\n";
        }
        enhancedNoticeText += LocalizedText.Get("sys.GUILDFACILITY_ENHANCE_ENHANCED_NOTICE2");
      }
      return enhancedNoticeText;
    }

    private void OnChange_GuildFacilityEnhanceGoldSlider(float value)
    {
      bool flag = (double) this.mSelectedGold <= (double) value;
      this.mSelectedGold = (long) Math.Ceiling((double) value * (double) this.mTargetFacility.Param.Increment);
      if (flag)
      {
        if (this.ErrorCheck_LvMax())
        {
          if (this.mSelectedGold != this.GetNeedNum_LvMax() && UnityEngine.Object.op_Equality((UnityEngine.Object) this.mErrorWindow, (UnityEngine.Object) null))
          {
            this.mErrorWindow = UIUtility.SystemMessage(LocalizedText.Get("sys.GUILDFACILITY_ERROR_MSG_ENHANCE_LEVEL_MAX"), new UIUtility.DialogResultEvent(this.OnClose_ErrorWindow));
            ((Selectable) this.mFacilityEnhanceSlider).interactable = false;
          }
          this.mSelectedGold = this.GetNeedNum_LvMax();
        }
        if (this.ErrorCheck_InvestLimit())
        {
          long needNumInvestLimit = this.GetNeedNum_InvestLimit();
          if (this.mSelectedGold != needNumInvestLimit && UnityEngine.Object.op_Equality((UnityEngine.Object) this.mErrorWindow, (UnityEngine.Object) null))
          {
            this.mErrorWindow = UIUtility.SystemMessage(LocalizedText.Get("sys.GUILDFACILITY_ERROR_MSG_ENHANCE_INVEST_LIMIT_GOLD"), new UIUtility.DialogResultEvent(this.OnClose_ErrorWindow));
            ((Selectable) this.mFacilityEnhanceSlider).interactable = false;
          }
          this.mSelectedGold = needNumInvestLimit;
        }
      }
      this.SetupInvestGuildFacilityUI();
      this.Refresh_FacilityExp();
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void OnClose_ErrorWindow(GameObject go)
    {
      ((Selectable) this.mFacilityEnhanceSlider).interactable = true;
    }

    public void OnClick_EnhanceGoldCountUp() => ++this.mFacilityEnhanceSlider.value;

    public void OnClick_EnhanceGoldCountDown() => --this.mFacilityEnhanceSlider.value;

    public void OnClick_EnhanceGoldCountMax()
    {
      long selectedTotalGold = this.GetSelectedTotalGold();
      long num1 = Math.Max(0L, this.Exp2Gold(GuildFacilityData.GetNeedExp(1, GuildFacilityData.GetLevelMax(this.mTargetFacility.Param.Type), this.mTargetFacility.Param.Type) - this.mTargetFacility.Exp) - selectedTotalGold);
      long num2 = Math.Max(0L, this.GetRestInvestPoint() - selectedTotalGold);
      long num3 = (long) MonoSingleton<GameManager>.Instance.Player.Gold - this.mSelectedGold;
      long gold = num1;
      if (gold > num2)
        gold = num2;
      if (gold > num3)
        gold = num3;
      if (gold <= 0L)
        return;
      this.mFacilityEnhanceSlider.value += (float) this.Gold2Exp(gold);
    }

    [DebuggerHidden]
    private IEnumerator LevelupAnimation(
      GuildFacilityData facility,
      GuildFacilityEnhance_Gold.EffectCallBack callback)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GuildFacilityEnhance_Gold.\u003CLevelupAnimation\u003Ec__Iterator0()
      {
        callback = callback,
        \u0024this = this
      };
    }

    private void EndLevelupAnimation()
    {
      string enhancedNoticeText = this.GetEnhancedNoticeText();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GuildLobby.Instance, (UnityEngine.Object) null))
        GuildLobby.Instance.Refresh();
      if (!string.IsNullOrEmpty(enhancedNoticeText))
        UIUtility.SystemMessage(enhancedNoticeText, (UIUtility.DialogResultEvent) (go => FlowNode_GameObject.ActivateOutputLinks((Component) this, 1020)));
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1020);
    }

    public delegate void EffectCallBack();
  }
}
