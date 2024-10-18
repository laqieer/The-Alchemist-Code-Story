// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidBossStageInfo
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
  [FlowNode.Pin(10, "表示更新", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "表示更新完了", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(200, "ボスデータ取得に失敗", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(11, "左ボタンタップ", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "右ボタンタップ", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(101, "PREインデック設定完了", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "NEXTインデックス設定完了", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "PREアニメーション開始", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "NEXTアニメーション開始", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(13, "バックボタンタップ", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(300, "閉じる", FlowNode.PinTypes.Output, 300)]
  [FlowNode.Pin(301, "ワールドトップ画面更新して閉じる", FlowNode.PinTypes.Output, 301)]
  public class WorldRaidBossStageInfo : MonoBehaviour, IFlowInterface, IWebHelp
  {
    public const int PIN_INPUT_REFRESH = 10;
    public const int PIN_INPUT_LEFT_BUTTON = 11;
    public const int PIN_INPUT_RIGHT_BUTTON = 12;
    public const int PIN_INPUT_BACK_BUTTON = 13;
    public const int PIN_OUTPUT_REFRESH = 100;
    public const int PIN_OUTPUT_SET_INDEX_PRE = 101;
    public const int PIN_OUTPUT_SET_INDEX_NEXT = 102;
    public const int PIN_OUTPUT_ANIM_START_PRE = 103;
    public const int PIN_OUTPUT_ANIM_START_NEXT = 104;
    public const int PIN_OUTPUT_ERROR_NOT_FOUND_BOSS = 200;
    public const int PIN_OUTPUT_NORMAL_BACK = 300;
    public const int PIN_OUTPUT_REFRESH_BACK = 301;
    private const string DEFEAT_TEXT_COLOR = "#808080";
    private const string DEFAULT_TEXT_COLOR = "#F91A4A";
    [SerializeField]
    private GameObject mNormalBossBg;
    [SerializeField]
    private GameObject mLastBossBg;
    [SerializeField]
    private RawPolyImage mBossImage;
    [SerializeField]
    private Text mBossNameText;
    [SerializeField]
    private Text mBossHpText;
    [SerializeField]
    private Text mBossHpPercent;
    [SerializeField]
    private Text mBossPercentText;
    [SerializeField]
    private Image mBossElementIcon;
    [SerializeField]
    private GameObject[] mBossLifeObjs;
    [SerializeField]
    private Transform mRaidRewardIconParent;
    [SerializeField]
    private RaidRewardIcon mRaidRewardIcon_Templete;
    [SerializeField]
    private ScrollRect mRaidRewardScroll;
    [SerializeField]
    private Text mRestTimeText;
    [SerializeField]
    private Button mChallengeButton;
    [SerializeField]
    private GameObject mChallengeLight;
    [SerializeField]
    private Button mLeftButton;
    [SerializeField]
    private Button mRightButton;
    [SerializeField]
    private Image mFlickButton;
    [SerializeField]
    private GameObject mBossPageParent;
    [SerializeField]
    private Toggle mTemplateDotIcon;
    [SerializeField]
    private Button mRefreshButton;
    [SerializeField]
    private float mRefreshButtonInterval;
    private WorldRaidParam mWorldRaidParam;
    private WorldRaidBossDetailData mBossData;
    private WorldRaidParam.BossInfo mBossInfo;
    private WorldRaidBossParam mBossParam;
    private List<GameObject> mRewardList = new List<GameObject>();
    private List<Toggle> mDotList = new List<Toggle>();
    private bool mIsAnimInLeft;
    private bool mIsAnimInRight;
    private float mRefreshButtonTime;
    private Color mDefeatTextColor = new Color();
    private Color mDefaultTextColor = new Color();
    private bool mIsTopRefreshFlag;
    private bool mIsFirstFlag = true;

    private void Awake()
    {
      ColorUtility.TryParseHtmlString("#808080", ref this.mDefeatTextColor);
      ColorUtility.TryParseHtmlString("#F91A4A", ref this.mDefaultTextColor);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.Refresh();
          break;
        case 11:
          this.OnClickLeftButton();
          break;
        case 12:
          this.OnClickRightButton();
          break;
        case 13:
          this.OnClickBackButton();
          break;
      }
    }

    private void Update()
    {
      this.Update_RestTime();
      if (!Object.op_Inequality((Object) this.mRefreshButton, (Object) null) || ((Selectable) this.mRefreshButton).interactable || this.mBossData == null || this.mBossData.CurrentHP <= 0L || (double) this.mRefreshButtonInterval <= 0.0)
        return;
      this.mRefreshButtonTime += Time.deltaTime;
      if ((double) this.mRefreshButtonTime < (double) this.mRefreshButtonInterval)
        return;
      ((Selectable) this.mRefreshButton).interactable = true;
    }

    private void Refresh()
    {
      this.mWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
      this.mBossData = WorldRaidBossManager.GetBossData();
      this.mBossInfo = WorldRaidBossManager.Instance.GetCurrentBossInfo();
      this.mBossParam = WorldRaidBossManager.Instance.GetCurrentWorldRaidBossParam();
      if (this.mBossData == null || this.mBossParam == null)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
      }
      else
      {
        GlobalVars.SelectedQuestID = this.mBossParam.QuestId;
        if (this.mBossParam != null)
        {
          DataSource.Bind<QuestParam>(((Component) this).gameObject, MonoSingleton<GameManager>.Instance.FindQuest(this.mBossParam.QuestId));
          DataSource.Bind<UnitParam>(((Component) this).gameObject, MonoSingleton<GameManager>.Instance.GetUnitParam(this.mBossParam.UnitId));
        }
        if (this.mBossInfo.IsLastBoss)
        {
          GameUtility.SetGameObjectActive(this.mNormalBossBg, false);
          GameUtility.SetGameObjectActive(this.mLastBossBg, true);
          this.CreateDotIcon(WorldRaidManager.GetLastBossData());
        }
        else
        {
          GameUtility.SetGameObjectActive(this.mNormalBossBg, true);
          GameUtility.SetGameObjectActive(this.mLastBossBg, false);
          this.CreateDotIcon(WorldRaidManager.GetNormalBossData());
        }
        this.Refresh_BossInfo();
        GameParameter.UpdateAll(((Component) this).gameObject);
        this.CheckLeftRightChange();
        if (Object.op_Inequality((Object) this.mRefreshButton, (Object) null))
        {
          ((Selectable) this.mRefreshButton).interactable = false;
          this.mRefreshButtonTime = 0.0f;
        }
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void Refresh_BossInfo()
    {
      if (Object.op_Equality((Object) WorldRaidBossManager.Instance, (Object) null) || this.mBossData == null || this.mBossParam == null)
        return;
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.mBossParam.UnitId);
      if (unitParam == null)
        return;
      if (Object.op_Inequality((Object) this.mBossImage, (Object) null))
        ((Graphic) this.mBossImage).color = this.mBossData.CurrentHP > 0L ? Color.white : Color.cyan;
      if (Object.op_Inequality((Object) this.mBossNameText, (Object) null))
        this.mBossNameText.text = unitParam.name.ToString();
      if (Object.op_Inequality((Object) this.mBossHpText, (Object) null))
      {
        this.mBossHpText.text = this.mBossData.CurrentHP.ToString();
        ((Graphic) this.mBossHpText).color = this.mBossData.CurrentHP > 0L ? this.mDefaultTextColor : this.mDefeatTextColor;
      }
      double currentHp = (double) this.mBossData.CurrentHP;
      double num1 = (double) this.mBossData.CalcMaxHP();
      if (num1 <= 0.0)
        num1 = 1.0;
      int num2 = (int) (currentHp / num1 * 100.0);
      if (num2 == 100 && currentHp < num1)
        num2 = 99;
      if (num2 == 0 && currentHp > 0.0)
        num2 = 1;
      if (Object.op_Inequality((Object) this.mBossHpPercent, (Object) null) && Object.op_Inequality((Object) this.mBossPercentText, (Object) null))
      {
        this.mBossHpPercent.text = num2.ToString();
        ((Graphic) this.mBossHpPercent).color = num2 > 0 ? this.mDefaultTextColor : this.mDefeatTextColor;
        ((Graphic) this.mBossPercentText).color = num2 > 0 ? this.mDefaultTextColor : this.mDefeatTextColor;
      }
      if (this.mBossLifeObjs.Length > 0)
      {
        float num3 = 100f / (float) this.mBossLifeObjs.Length;
        float num4 = 0.0f;
        bool active = true;
        for (int index = 0; index < this.mBossLifeObjs.Length; ++index)
        {
          if (active && ((double) num2 <= (double) num4 || num2 == 0))
            active = false;
          GameUtility.SetGameObjectActive(this.mBossLifeObjs[index], active);
          num4 += num3;
        }
      }
      if (Object.op_Inequality((Object) this.mChallengeButton, (Object) null))
      {
        if (currentHp <= 0.0)
          ((Selectable) this.mChallengeButton).interactable = false;
        else
          ((Selectable) this.mChallengeButton).interactable = true;
      }
      GameUtility.SetGameObjectActive(this.mChallengeLight, currentHp > 0.0);
      if (Object.op_Inequality((Object) this.mBossElementIcon, (Object) null))
      {
        GameSettings instance = GameSettings.Instance;
        this.mBossElementIcon.sprite = (Sprite) null;
        int element = (int) unitParam.element;
        if (0 <= element && element < instance.Elements_IconSmall.Length)
          this.mBossElementIcon.sprite = instance.Elements_IconSmall[element];
      }
      if (Object.op_Inequality((Object) this.mRaidRewardIcon_Templete, (Object) null) && Object.op_Inequality((Object) this.mRaidRewardIconParent, (Object) null))
      {
        foreach (Object mReward in this.mRewardList)
          Object.Destroy(mReward);
        this.mRewardList.Clear();
        WorldRaidParam.BossInfo currentBossInfo = WorldRaidBossManager.Instance.GetCurrentBossInfo();
        if (currentBossInfo != null)
        {
          WorldRaidRewardParam beatRewardParam = currentBossInfo.BeatRewardParam;
          if (beatRewardParam != null)
          {
            foreach (WorldRaidRewardParam.Reward reward in beatRewardParam.RewardList)
            {
              RaidRewardIcon raidRewardIcon = Object.Instantiate<RaidRewardIcon>(this.mRaidRewardIcon_Templete, this.mRaidRewardIconParent, false);
              if (Object.op_Inequality((Object) raidRewardIcon, (Object) null))
              {
                raidRewardIcon.Initialize(reward.ItemType, reward.ItemIname, reward.ItemNum);
                ((Component) raidRewardIcon).gameObject.SetActive(true);
                this.mRewardList.Add(((Component) raidRewardIcon).gameObject);
              }
            }
          }
        }
      }
      if (Object.op_Inequality((Object) this.mRaidRewardScroll, (Object) null))
        this.mRaidRewardScroll.horizontalNormalizedPosition = 0.0f;
      if (!this.mIsFirstFlag && currentHp <= 0.0)
        this.mIsTopRefreshFlag = true;
      this.mIsFirstFlag = false;
    }

    private void Update_RestTime()
    {
      if (this.mWorldRaidParam == null)
        return;
      if (Object.op_Inequality((Object) this.mRestTimeText, (Object) null))
        this.mRestTimeText.text = WorldRaidManager.GetRemainTimeText();
      if (!(TimeManager.ServerTime >= this.mWorldRaidParam.ChallengeEndAt) || !Object.op_Inequality((Object) this.mChallengeButton, (Object) null))
        return;
      ((Selectable) this.mChallengeButton).interactable = false;
    }

    private void CheckLeftRightChange()
    {
      if (Object.op_Inequality((Object) this.mLeftButton, (Object) null) && Object.op_Inequality((Object) this.mRightButton, (Object) null))
      {
        if (!this.mBossInfo.IsLastBoss)
        {
          if (WorldRaidManager.GetNormalBossData().Count > 1)
          {
            ((Selectable) this.mLeftButton).interactable = true;
            ((Selectable) this.mRightButton).interactable = true;
          }
          else
          {
            ((Selectable) this.mLeftButton).interactable = false;
            ((Selectable) this.mRightButton).interactable = false;
          }
        }
        else if (WorldRaidManager.GetLastBossData().Count > 1)
        {
          ((Selectable) this.mLeftButton).interactable = true;
          ((Selectable) this.mRightButton).interactable = true;
        }
        else
        {
          ((Selectable) this.mLeftButton).interactable = false;
          ((Selectable) this.mRightButton).interactable = false;
        }
      }
      if (Object.op_Inequality((Object) this.mFlickButton, (Object) null))
      {
        if (!this.mBossInfo.IsLastBoss)
        {
          if (WorldRaidManager.GetNormalBossData().Count > 1)
            ((Graphic) this.mFlickButton).raycastTarget = true;
          else
            ((Graphic) this.mFlickButton).raycastTarget = false;
        }
        else if (WorldRaidManager.GetLastBossData().Count > 1)
          ((Graphic) this.mFlickButton).raycastTarget = true;
        else
          ((Graphic) this.mFlickButton).raycastTarget = false;
      }
      if (this.mIsAnimInLeft)
      {
        this.mIsAnimInLeft = false;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
      }
      if (!this.mIsAnimInRight)
        return;
      this.mIsAnimInRight = false;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
    }

    private void OnClickLeftButton()
    {
      if (Object.op_Equality((Object) WorldRaidBossManager.Instance, (Object) null))
        return;
      WorldRaidBossManager.Instance.BossIndexSub(this.mBossInfo.IsLastBoss);
      this.mIsAnimInRight = true;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void OnClickRightButton()
    {
      if (Object.op_Equality((Object) WorldRaidBossManager.Instance, (Object) null))
        return;
      WorldRaidBossManager.Instance.BossIndexAdd(this.mBossInfo.IsLastBoss);
      this.mIsAnimInLeft = true;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }

    private void CreateDotIcon(List<WorldRaidBossChallengedData> _list)
    {
      if (this.mDotList.Count > 0)
      {
        foreach (Component mDot in this.mDotList)
          Object.Destroy((Object) mDot.gameObject);
      }
      if (!Object.op_Inequality((Object) this.mTemplateDotIcon, (Object) null) || !Object.op_Inequality((Object) this.mBossPageParent, (Object) null))
        return;
      this.mDotList.Clear();
      foreach (WorldRaidBossChallengedData bossChallengedData in _list)
      {
        Toggle toggle = Object.Instantiate<Toggle>(this.mTemplateDotIcon, this.mBossPageParent.transform);
        ((Component) toggle).gameObject.SetActive(true);
        this.mDotList.Add(toggle);
        if (bossChallengedData.BossIname == this.mBossData.BossIname)
          toggle.isOn = true;
      }
    }

    private void OnClickBackButton()
    {
      if (this.mIsTopRefreshFlag)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 301);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 300);
    }

    public bool GetHelpURL(out string url, out string title)
    {
      url = (string) null;
      title = (string) null;
      if (this.mBossParam == null || string.IsNullOrEmpty(this.mBossParam.DetailTitle) || string.IsNullOrEmpty(this.mBossParam.DetailUrl))
        return false;
      title = this.mBossParam.DetailTitle;
      url = this.mBossParam.DetailUrl;
      return true;
    }
  }
}
