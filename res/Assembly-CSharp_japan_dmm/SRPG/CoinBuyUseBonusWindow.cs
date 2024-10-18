// Decompiled with JetBrains decompiler
// Type: SRPG.CoinBuyUseBonusWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(100, "初期化", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(110, "消費ボーナス表示", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(120, "購入ボーナス表示", FlowNode.PinTypes.Input, 120)]
  [FlowNode.Pin(101, "表示更新", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(130, "HOMEバッジ更新", FlowNode.PinTypes.Input, 130)]
  [FlowNode.Pin(1000, "報酬受取", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1010, "真理念装詳細表示", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(1000, "報酬受取", FlowNode.PinTypes.Output, 1000)]
  public class CoinBuyUseBonusWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 100;
    private const int PIN_INPUT_REFRESH = 101;
    private const int PIN_INPUT_CHANGE_USE_BONUS = 110;
    private const int PIN_INPUT_CHANGE_BUY_BONUS = 120;
    private const int PIN_INPUT_REFRESH_BADGE = 130;
    private const int PIN_OUTPUT_RECEIVE_REWARD = 1000;
    private const int PIN_OUTPUT_OPEN_CONCEPTCARD_DETAIL = 1010;
    private static int PlayerCoin = -1;
    private Dictionary<eCoinBuyUseBonusTrigger, Dictionary<eCoinBuyUseBonusType, CoinBuyUseBonusParam>> mBonusListAll;
    private Dictionary<eCoinBuyUseBonusTrigger, Dictionary<eCoinBuyUseBonusType, bool>> mBonusBadgeStates;
    private List<GameObject> mCreatedObjects = new List<GameObject>();
    private Dictionary<eCoinBuyUseBonusType, Toggle> mTypeToggles;
    private eCoinBuyUseBonusTrigger mSelectedTrigger;
    private Dictionary<eCoinBuyUseBonusTrigger, eCoinBuyUseBonusType> mSelectedType;
    private CoinBuyUseBonusParam mTargetBonusParam;
    private CoinBuyUseBonusContentParam mTargetContentParam;
    [SerializeField]
    private Text mBonusPeriodText;
    [SerializeField]
    private GameObject mBonusPeriodTextRoot;
    [SerializeField]
    private GameObject mRewardContentTemplate;
    [SerializeField]
    private GameObject mRewardContentParent;
    [SerializeField]
    private GameObject mSwitchButton_Use;
    [SerializeField]
    private GameObject mSwitchButton_Buy;
    [SerializeField]
    private Toggle mDaliyToggle;
    [SerializeField]
    private Toggle mPeriodToggle;
    [SerializeField]
    private GameObject mTitleBackground_Use;
    [SerializeField]
    private GameObject mTitleBackground_Buy;
    [SerializeField]
    private Text mCurrentUseCoinCount;
    [SerializeField]
    private Text mCurrentBuyCoinCount;
    [SerializeField]
    private GameObject mUseDayResetExplainObject;
    [SerializeField]
    private GameObject mBuyDayResetExplainObject;
    [SerializeField]
    private GameObject mNoBonusText;
    [SerializeField]
    private GameObject mSwitchButton_Use_Badge;
    [SerializeField]
    private GameObject mSwitchButton_Buy_Badge;
    [SerializeField]
    private GameObject mDaliyToggle_Badge;
    [SerializeField]
    private GameObject mPeriodToggle_Badge;
    [SerializeField]
    private GameObject mItemIcon;
    [SerializeField]
    private GameObject mStaminaIcon;
    [SerializeField]
    private GameObject mPlayerExpIcon;
    [SerializeField]
    private GameObject mArtifactIcon;
    [SerializeField]
    private GameObject mCoinIcon;
    [SerializeField]
    private GameObject mGoldIcon;
    [SerializeField]
    private GameObject mSetItemIcon;
    [SerializeField]
    private GameObject mUnitIcon;
    [SerializeField]
    private GameObject mAwardIcon;
    [SerializeField]
    private GameObject mArenaCoinIcon;
    [SerializeField]
    private GameObject mMultiCoinIcon;
    [SerializeField]
    private GameObject mKakeraCoinIcon;
    [SerializeField]
    private GameObject mConceptCardIcon;
    private static CoinBuyUseBonusWindow mInstance;

    public CoinBuyUseBonusParam TargetBonusParam => this.mTargetBonusParam;

    public CoinBuyUseBonusContentParam TargetContentParam => this.mTargetContentParam;

    public GameObject ItemIcon => this.mItemIcon;

    public GameObject StaminaIcon => this.mStaminaIcon;

    public GameObject PlayerExpIcon => this.mPlayerExpIcon;

    public GameObject ArtifactIcon => this.mArtifactIcon;

    public GameObject CoinIcon => this.mCoinIcon;

    public GameObject GoldIcon => this.mGoldIcon;

    public GameObject SetItemIcon => this.mSetItemIcon;

    public GameObject UnitIcon => this.mUnitIcon;

    public GameObject AwardIcon => this.mAwardIcon;

    public GameObject ArenaCoinIcon => this.mArenaCoinIcon;

    public GameObject MultiCoinIcon => this.mMultiCoinIcon;

    public GameObject KakeraCoinIcon => this.mKakeraCoinIcon;

    public GameObject ConceptCardIcon => this.mConceptCardIcon;

    private eCoinBuyUseBonusType SelectedType
    {
      get => this.mSelectedType[this.mSelectedTrigger];
      set => this.mSelectedType[this.mSelectedTrigger] = value;
    }

    public static CoinBuyUseBonusWindow Instance => CoinBuyUseBonusWindow.mInstance;

    public static bool IsDirtyBonusProgress()
    {
      return !Object.op_Equality((Object) MonoSingleton<GameManager>.Instance, (Object) null) && MonoSingleton<GameManager>.Instance.Player != null && MonoSingleton<GameManager>.Instance.Player.Coin != CoinBuyUseBonusWindow.PlayerCoin;
    }

    public static void SyncCoin()
    {
      if (Object.op_Equality((Object) MonoSingleton<GameManager>.Instance, (Object) null) || MonoSingleton<GameManager>.Instance.Player == null)
        return;
      CoinBuyUseBonusWindow.PlayerCoin = MonoSingleton<GameManager>.Instance.Player.Coin;
    }

    public static void ResetParam() => CoinBuyUseBonusWindow.PlayerCoin = -1;

    private void Awake() => CoinBuyUseBonusWindow.mInstance = this;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 100:
          this.Init();
          break;
        case 101:
          this.ChangeTrigger(this.mSelectedTrigger, this.SelectedType);
          break;
        case 110:
          this.mSelectedTrigger = eCoinBuyUseBonusTrigger.Use;
          this.ChangeTrigger(this.mSelectedTrigger, this.SelectedType);
          break;
        case 120:
          this.mSelectedTrigger = eCoinBuyUseBonusTrigger.Buy;
          this.ChangeTrigger(this.mSelectedTrigger, this.SelectedType);
          break;
        case 130:
          this.RefreshHomeBadge();
          break;
      }
    }

    private void Init_BonusDictionary()
    {
      this.mBonusListAll = new Dictionary<eCoinBuyUseBonusTrigger, Dictionary<eCoinBuyUseBonusType, CoinBuyUseBonusParam>>();
      this.mBonusBadgeStates = new Dictionary<eCoinBuyUseBonusTrigger, Dictionary<eCoinBuyUseBonusType, bool>>();
      for (int key1 = 0; key1 < 3; ++key1)
      {
        Dictionary<eCoinBuyUseBonusType, CoinBuyUseBonusParam> dictionary1 = new Dictionary<eCoinBuyUseBonusType, CoinBuyUseBonusParam>();
        Dictionary<eCoinBuyUseBonusType, bool> dictionary2 = new Dictionary<eCoinBuyUseBonusType, bool>();
        for (int key2 = 0; key2 < 3; ++key2)
        {
          dictionary1.Add((eCoinBuyUseBonusType) key2, (CoinBuyUseBonusParam) null);
          dictionary2.Add((eCoinBuyUseBonusType) key2, false);
        }
        this.mBonusListAll.Add((eCoinBuyUseBonusTrigger) key1, dictionary1);
        this.mBonusBadgeStates.Add((eCoinBuyUseBonusTrigger) key1, dictionary2);
      }
    }

    private void Init()
    {
      this.mTypeToggles = new Dictionary<eCoinBuyUseBonusType, Toggle>();
      this.mTypeToggles.Add(eCoinBuyUseBonusType.Daily, this.mDaliyToggle);
      this.mTypeToggles.Add(eCoinBuyUseBonusType.Period, this.mPeriodToggle);
      this.Init_BonusDictionary();
      this.RegisterBonus(eCoinBuyUseBonusTrigger.Buy, eCoinBuyUseBonusType.Daily);
      this.RegisterBonus(eCoinBuyUseBonusTrigger.Buy, eCoinBuyUseBonusType.Period);
      this.RegisterBonus(eCoinBuyUseBonusTrigger.Use, eCoinBuyUseBonusType.Daily);
      this.RegisterBonus(eCoinBuyUseBonusTrigger.Use, eCoinBuyUseBonusType.Period);
      this.mSelectedTrigger = eCoinBuyUseBonusTrigger.Buy;
      this.SelectDefaultTrigger();
      this.mSelectedType = new Dictionary<eCoinBuyUseBonusTrigger, eCoinBuyUseBonusType>();
      this.mSelectedType[eCoinBuyUseBonusTrigger.Buy] = eCoinBuyUseBonusType.Daily;
      this.mSelectedType[eCoinBuyUseBonusTrigger.Use] = eCoinBuyUseBonusType.Daily;
      this.SelectDefaultToggle(eCoinBuyUseBonusTrigger.Buy);
      this.SelectDefaultToggle(eCoinBuyUseBonusTrigger.Use);
      foreach (eCoinBuyUseBonusTrigger key1 in this.mBonusBadgeStates.Keys)
      {
        foreach (eCoinBuyUseBonusType key2 in this.mBonusBadgeStates[key1].Keys)
        {
          if (this.mBonusBadgeStates[key1][key2])
          {
            this.mSelectedTrigger = key1;
            this.SelectedType = key2;
          }
        }
      }
      this.ChangeTrigger(this.mSelectedTrigger, this.SelectedType);
    }

    private void RegisterBonus(eCoinBuyUseBonusTrigger trigger, eCoinBuyUseBonusType type)
    {
      CoinBuyUseBonusParam buyUseBonusParam = MonoSingleton<GameManager>.Instance.MasterParam.GetActiveCoinBuyUseBonusParam(trigger, type);
      if (buyUseBonusParam == null)
        return;
      this.mBonusListAll[trigger][type] = buyUseBonusParam;
      this.mBonusBadgeStates[trigger][type] = MonoSingleton<GameManager>.Instance.Player.IsExistReceivableCoinBuyUseBonus(trigger, type);
    }

    private void SelectDefaultTrigger()
    {
      if (this.mBonusListAll[this.mSelectedTrigger][eCoinBuyUseBonusType.Daily] != null || this.mBonusListAll[this.mSelectedTrigger][eCoinBuyUseBonusType.Period] != null)
        return;
      foreach (eCoinBuyUseBonusTrigger key1 in this.mBonusListAll.Keys)
      {
        foreach (eCoinBuyUseBonusType key2 in this.mBonusListAll[key1].Keys)
        {
          if (this.mBonusListAll[key1][key2] != null)
          {
            this.mSelectedTrigger = key1;
            return;
          }
        }
      }
    }

    private void SelectDefaultToggle(eCoinBuyUseBonusTrigger target_trigger)
    {
      eCoinBuyUseBonusType key1 = this.mSelectedType[target_trigger];
      if (this.mBonusListAll[target_trigger][key1] != null)
        return;
      foreach (eCoinBuyUseBonusType key2 in this.mBonusListAll[target_trigger].Keys)
      {
        if (this.mBonusListAll[target_trigger][key2] != null)
        {
          this.mSelectedType[target_trigger] = key2;
          break;
        }
      }
    }

    private void ChangeTrigger(eCoinBuyUseBonusTrigger trigger, eCoinBuyUseBonusType type)
    {
      if (MonoSingleton<GameManager>.Instance.MasterParam.GetEnableCoinBuyUseBonusParams() == null)
        this.mNoBonusText.SetActive(true);
      this.RefreshBadge();
      Dictionary<eCoinBuyUseBonusType, CoinBuyUseBonusParam> dictionary = this.mBonusListAll[trigger];
      switch (trigger)
      {
        case eCoinBuyUseBonusTrigger.Buy:
          this.mSwitchButton_Use.SetActive(false);
          this.mSwitchButton_Buy.SetActive(true);
          this.mTitleBackground_Use.SetActive(false);
          this.mTitleBackground_Buy.SetActive(true);
          this.mSwitchButton_Buy_Badge.SetActive(this.IsActiveBonusTriggerBadge(eCoinBuyUseBonusTrigger.Use));
          this.mDaliyToggle_Badge.SetActive(this.mBonusBadgeStates[trigger][eCoinBuyUseBonusType.Daily]);
          this.mPeriodToggle_Badge.SetActive(this.mBonusBadgeStates[trigger][eCoinBuyUseBonusType.Period]);
          break;
        case eCoinBuyUseBonusTrigger.Use:
          this.mSwitchButton_Use.SetActive(true);
          this.mSwitchButton_Buy.SetActive(false);
          this.mTitleBackground_Use.SetActive(true);
          this.mTitleBackground_Buy.SetActive(false);
          this.mSwitchButton_Use_Badge.SetActive(this.IsActiveBonusTriggerBadge(eCoinBuyUseBonusTrigger.Buy));
          this.mDaliyToggle_Badge.SetActive(this.mBonusBadgeStates[trigger][eCoinBuyUseBonusType.Daily]);
          this.mPeriodToggle_Badge.SetActive(this.mBonusBadgeStates[trigger][eCoinBuyUseBonusType.Period]);
          break;
      }
      bool flag1 = this.mBonusListAll[eCoinBuyUseBonusTrigger.Buy][eCoinBuyUseBonusType.Daily] != null || this.mBonusListAll[eCoinBuyUseBonusTrigger.Buy][eCoinBuyUseBonusType.Period] != null;
      bool flag2 = this.mBonusListAll[eCoinBuyUseBonusTrigger.Use][eCoinBuyUseBonusType.Daily] != null || this.mBonusListAll[eCoinBuyUseBonusTrigger.Use][eCoinBuyUseBonusType.Period] != null;
      if (!flag1 || !flag2)
      {
        this.mSwitchButton_Use.SetActive(false);
        this.mSwitchButton_Buy.SetActive(false);
      }
      this.ChangeTab(trigger, type);
    }

    private bool IsActiveBonusTriggerBadge(eCoinBuyUseBonusTrigger trigger)
    {
      for (int key = 0; key < 3; ++key)
      {
        if (this.mBonusBadgeStates[trigger][(eCoinBuyUseBonusType) key])
          return true;
      }
      return false;
    }

    private void RefreshBadge()
    {
      for (int index1 = 0; index1 < 3; ++index1)
      {
        eCoinBuyUseBonusTrigger buyUseBonusTrigger = (eCoinBuyUseBonusTrigger) index1;
        if (this.mBonusBadgeStates.ContainsKey(buyUseBonusTrigger))
        {
          for (int index2 = 0; index2 < 3; ++index2)
          {
            eCoinBuyUseBonusType coinBuyUseBonusType = (eCoinBuyUseBonusType) index2;
            if (this.mBonusBadgeStates[buyUseBonusTrigger].ContainsKey(coinBuyUseBonusType))
              this.mBonusBadgeStates[buyUseBonusTrigger][coinBuyUseBonusType] = MonoSingleton<GameManager>.Instance.Player.IsExistReceivableCoinBuyUseBonus(buyUseBonusTrigger, coinBuyUseBonusType);
          }
        }
      }
    }

    private void ChangeTab(eCoinBuyUseBonusTrigger trigger, eCoinBuyUseBonusType type)
    {
      CoinBuyUseBonusParam bonus_param = this.mBonusListAll[trigger][type];
      foreach (eCoinBuyUseBonusType key in this.mTypeToggles.Keys)
        this.mTypeToggles[key].isOn = false;
      this.mTypeToggles[type].isOn = true;
      ((Component) this.mTypeToggles[eCoinBuyUseBonusType.Daily]).gameObject.SetActive(this.mBonusListAll[trigger][eCoinBuyUseBonusType.Daily] != null);
      ((Component) this.mTypeToggles[eCoinBuyUseBonusType.Period]).gameObject.SetActive(this.mBonusListAll[trigger][eCoinBuyUseBonusType.Period] != null);
      this.mBonusPeriodTextRoot.SetActive(false);
      if (bonus_param != null)
      {
        string format = LocalizedText.Get("sys.BUYUSE_PERIOD_FORMAT1");
        string str1 = bonus_param.BeginAt.ToString(format);
        string str2 = bonus_param.EndAt.ToString(format);
        this.mBonusPeriodTextRoot.SetActive(true);
        this.mBonusPeriodText.text = string.Format(LocalizedText.Get("sys.BUYUSE_PERIOD_FORMAT2"), (object) str1, (object) str2);
      }
      if (bonus_param != null)
      {
        Text text = trigger != eCoinBuyUseBonusTrigger.Use ? this.mCurrentBuyCoinCount : this.mCurrentUseCoinCount;
        if (Object.op_Inequality((Object) text, (Object) null))
          text.text = MonoSingleton<GameManager>.Instance.Player.GetCoinBuyUseBonusProgress(bonus_param.Iname).ToString();
      }
      if (bonus_param != null)
      {
        GameObject gameObject = bonus_param.Trigger != eCoinBuyUseBonusTrigger.Use ? this.mBuyDayResetExplainObject : this.mUseDayResetExplainObject;
        if (Object.op_Inequality((Object) gameObject, (Object) null))
        {
          bool flag = bonus_param.Type == eCoinBuyUseBonusType.Daily;
          gameObject.SetActive(flag);
        }
      }
      this.mRewardContentTemplate.SetActive(false);
      if (bonus_param == null)
        return;
      int num = bonus_param.RewardSet.Contents.Length - this.mCreatedObjects.Count;
      for (int index = 0; index < num; ++index)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.mRewardContentTemplate);
        gameObject.transform.SetParent(this.mRewardContentParent.transform, false);
        this.mCreatedObjects.Add(gameObject);
      }
      for (int index = 0; index < this.mCreatedObjects.Count; ++index)
        this.mCreatedObjects[index].SetActive(false);
      List<CoinBuyUseBonusContentParam> bonusContentParamList = new List<CoinBuyUseBonusContentParam>();
      List<CoinBuyUseBonusContentParam> collection1 = new List<CoinBuyUseBonusContentParam>();
      List<CoinBuyUseBonusContentParam> collection2 = new List<CoinBuyUseBonusContentParam>();
      for (int index = 0; index < bonus_param.RewardSet.Contents.Length; ++index)
      {
        if (MonoSingleton<GameManager>.Instance.Player.IsReceivedCoinBuyUseBonus(bonus_param.Iname, bonus_param.RewardSet.Contents[index].Num))
          collection2.Add(bonus_param.RewardSet.Contents[index]);
        else
          collection1.Add(bonus_param.RewardSet.Contents[index]);
      }
      bonusContentParamList.AddRange((IEnumerable<CoinBuyUseBonusContentParam>) collection1);
      bonusContentParamList.AddRange((IEnumerable<CoinBuyUseBonusContentParam>) collection2);
      for (int index = 0; index < bonusContentParamList.Count; ++index)
      {
        this.mCreatedObjects[index].SetActive(true);
        this.mCreatedObjects[index].GetComponent<CoinBuyUseBonusContent>().Refresh(bonus_param, bonusContentParamList[index]);
      }
    }

    public void OnChangeTypeToggle(Toggle toggle)
    {
      if (!toggle.isOn)
        return;
      foreach (eCoinBuyUseBonusType key in this.mTypeToggles.Keys)
      {
        if (Object.op_Equality((Object) this.mTypeToggles[key], (Object) toggle))
        {
          if (this.SelectedType == key)
            break;
          this.SelectedType = key;
          this.ChangeTab(this.mSelectedTrigger, this.SelectedType);
          break;
        }
      }
    }

    public void ReceiveReward(
      CoinBuyUseBonusParam bonus_param,
      CoinBuyUseBonusContentParam content_param)
    {
      this.mTargetBonusParam = bonus_param;
      this.mTargetContentParam = content_param;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
    }

    public void OnClick_ConceptCardIcon(GameObject obj)
    {
      ConceptCardData dataOfClass = DataSource.FindDataOfClass<ConceptCardData>(obj, (ConceptCardData) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedConceptCardData.Set(dataOfClass);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1010);
    }

    private void RefreshHomeBadge()
    {
      if (Object.op_Equality((Object) CoinBuyUseBonusIcon.Instance, (Object) null))
        return;
      CoinBuyUseBonusIcon.Instance.RefreshBadge();
    }
  }
}
