// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidBossStageInfo
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
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Select Lap", FlowNode.PinTypes.Input, 100)]
  public class GuildRaidBossStageInfo : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_INIT = 1;
    public const int PIN_INPUT_SELECTLAP = 100;
    [SerializeField]
    private RawImage_Transparent mGuildRaidImage;
    [SerializeField]
    private GameObject mClearIcon;
    [SerializeField]
    private GameObject mBeatReward;
    [SerializeField]
    private GameObject mBeatRewardItem;
    [SerializeField]
    private GameObject mLastAtkReward;
    [SerializeField]
    private GameObject mLastAtkRewardItem;
    [SerializeField]
    private Button mBeatReceiptButton;
    [SerializeField]
    private GameObject[] mRatioRewardPart;
    [SerializeField]
    private Button mRefreshButton;
    [SerializeField]
    private GameObject mChallengeList;
    [SerializeField]
    private GameObject mApLoopBpRemain;
    [SerializeField]
    private GameObject mApLoopBpEternal;
    [SerializeField]
    private GameObject mLapList;
    [SerializeField]
    private Button mLapButtonTemp;
    [SerializeField]
    private Text mLapText;
    [SerializeField]
    private SRPG_ScrollRect mLapScroll;
    [SerializeField]
    private Text mScheduleCloseText;
    private ColorBlock mKeepButtonColor = new ColorBlock();
    private ColorBlock mChangeButtonColor = new ColorBlock();
    private const int RATIO_MAX = 5;
    private const int RATIO_COUNT = 20;
    private const int SCROLL_INITLAP = 1;
    private bool isListDraw;
    private List<GameObject> mListReward = new List<GameObject>();
    private List<GameObject> mListChallenge = new List<GameObject>();
    private List<Button> mListButton = new List<Button>();

    private void Awake()
    {
      GuildRaidManager.Instance.TrialRound = GuildRaidManager.Instance.CurrentRound;
      if (!Object.op_Inequality((Object) this.mLapScroll, (Object) null))
        return;
      this.mLapScroll.verticalNormalizedPosition = 1f;
    }

    private void Update()
    {
      if (GuildRaidManager.Instance.IsRefreshConfirm() && GuildRaidManager.Instance.CurrentBossInfo.CurrentHP > 0)
        ((Selectable) this.mRefreshButton).interactable = true;
      else
        ((Selectable) this.mRefreshButton).interactable = false;
    }

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) GuildRaidManager.Instance, (Object) null))
        return;
      GuildRaidManager.Instance.SetRefreshButton();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
      {
        if (pinID != 100)
          return;
        this.SetSelectLapButtonColor();
        this.isListDraw = !this.isListDraw;
        GameUtility.SetGameObjectActive(this.mLapList, this.isListDraw);
      }
      else
        this.Init();
    }

    private void Init()
    {
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      GuildRaidManager instance = GuildRaidManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      GuildRaidBossParam guildRaidBossParam = MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(instance.CurrentBossInfo.BossId);
      if (guildRaidBossParam == null)
        return;
      UnitParam unitParam = masterParam.GetUnitParam(guildRaidBossParam.UnitIName);
      if (unitParam == null)
        return;
      DataSource.Bind<GuildRaidBossInfo>(((Component) this).gameObject, instance.CurrentBossInfo);
      DataSource.Bind<GuildRaidBossParam>(((Component) this).gameObject, guildRaidBossParam);
      DataSource.Bind<UnitParam>(((Component) this).gameObject, unitParam);
      GameUtility.SetGameObjectActive(this.mApLoopBpRemain, instance.BpHealType != GuildRaidManager.GuildRaidBpHealType.Eternal);
      GameUtility.SetGameObjectActive(this.mApLoopBpEternal, instance.BpHealType == GuildRaidManager.GuildRaidBpHealType.Eternal);
      GlobalVars.SelectedQuestID = guildRaidBossParam.QuestIName;
      GuildRaidRewardParam guildRaidRewardRound1 = MonoSingleton<GameManager>.Instance.GetGuildRaidRewardRound(guildRaidBossParam.BeatRewardId, instance.CurrentRound);
      if (this.mListReward != null && this.mListReward.Count > 0)
      {
        for (int index = 0; index < this.mListReward.Count; ++index)
          Object.Destroy((Object) this.mListReward[index]);
      }
      this.mListReward.Clear();
      if (guildRaidRewardRound1 != null)
      {
        GameUtility.SetGameObjectActive(this.mBeatReward, true);
        if (Object.op_Inequality((Object) this.mBeatRewardItem, (Object) null) && guildRaidRewardRound1 != null && guildRaidRewardRound1.Rewards != null)
        {
          for (int index = 0; index < guildRaidRewardRound1.Rewards.Count; ++index)
          {
            GameObject gameObject = Object.Instantiate<GameObject>(this.mBeatRewardItem, this.mBeatRewardItem.transform.parent);
            RewardListItem component = gameObject.GetComponent<RewardListItem>();
            if (!Object.op_Equality((Object) component, (Object) null))
            {
              component.AllNotActive();
              this.SetRewardIcon(guildRaidRewardRound1.Rewards[index], component);
              gameObject.SetActive(true);
              this.mListReward.Add(gameObject);
            }
          }
        }
      }
      else
        GameUtility.SetGameObjectActive(this.mBeatReward, false);
      GuildRaidRewardParam guildRaidRewardRound2 = MonoSingleton<GameManager>.Instance.GetGuildRaidRewardRound(guildRaidBossParam.LastAttackRewardId, instance.CurrentRound);
      if (guildRaidRewardRound2 != null)
      {
        GameUtility.SetGameObjectActive(this.mLastAtkReward, true);
        if (Object.op_Inequality((Object) this.mLastAtkRewardItem, (Object) null) && guildRaidRewardRound2 != null && guildRaidRewardRound2.Rewards != null)
        {
          for (int index = 0; index < guildRaidRewardRound2.Rewards.Count; ++index)
          {
            GameObject gameObject = Object.Instantiate<GameObject>(this.mLastAtkRewardItem, this.mLastAtkRewardItem.transform.parent);
            RewardListItem component = gameObject.GetComponent<RewardListItem>();
            if (!Object.op_Equality((Object) component, (Object) null))
            {
              component.AllNotActive();
              this.SetRewardIcon(guildRaidRewardRound2.Rewards[index], component);
              gameObject.SetActive(true);
              this.mListReward.Add(gameObject);
            }
          }
        }
      }
      else
        GameUtility.SetGameObjectActive(this.mLastAtkReward, false);
      if (this.mListChallenge != null && this.mListChallenge.Count > 0)
      {
        for (int index = 0; index < this.mListChallenge.Count; ++index)
          Object.Destroy((Object) this.mListChallenge[index]);
      }
      this.mListChallenge.Clear();
      if (instance.CurrentBossChallengingPlayerList != null && Object.op_Inequality((Object) this.mChallengeList, (Object) null))
      {
        this.mChallengeList.SetActive(false);
        int count = instance.CurrentBossChallengingPlayerList.Count;
        for (int index = 0; index < count; ++index)
        {
          if (instance.CurrentBossChallengingPlayerList[index] != null)
          {
            GameObject gameObject = Object.Instantiate<GameObject>(this.mChallengeList, this.mChallengeList.transform.parent);
            this.mListChallenge.Add(gameObject);
            DataSource.Bind<GuildRaidChallengingPlayer>(gameObject, instance.CurrentBossChallengingPlayerList[index]);
            gameObject.SetActive(true);
          }
        }
      }
      if (instance.CurrentBossInfo.CurrentHP <= 0)
      {
        if (Object.op_Inequality((Object) this.mGuildRaidImage, (Object) null))
          ((Graphic) this.mGuildRaidImage).color = Color.cyan;
        if (Object.op_Inequality((Object) this.mClearIcon, (Object) null))
          this.mClearIcon.SetActive(true);
      }
      GuildRaidBossInfo currentBossInfo = instance.CurrentBossInfo;
      if (currentBossInfo != null)
      {
        if (currentBossInfo.CurrentHP > 0 && MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodScheduleType() == GuildRaidManager.GuildRaidScheduleType.CloseSchedule && Object.op_Inequality((Object) this.mScheduleCloseText, (Object) null))
          this.mScheduleCloseText.text = LocalizedText.Get("sys.GUILDRAID_SCHEDULE_CLOSE");
        if (Object.op_Inequality((Object) instance, (Object) null) && instance.ChallengedBp == instance.MaxBp && instance.BpHealType != GuildRaidManager.GuildRaidBpHealType.Eternal && Object.op_Inequality((Object) this.mScheduleCloseText, (Object) null))
          this.mScheduleCloseText.text = LocalizedText.Get("sys.GUILDRAID_BP_EMPTY");
      }
      if (Object.op_Inequality((Object) this.mLapText, (Object) null) && Object.op_Inequality((Object) this.mLapText, (Object) null))
        this.mLapText.text = string.Format(LocalizedText.Get("sys.GUILDRAID_LAP"), (object) instance.TrialRound);
      GameUtility.SetGameObjectActive(((Component) this.mLapButtonTemp).gameObject, false);
      if (Object.op_Inequality((Object) this.mLapButtonTemp, (Object) null) && Object.op_Inequality((Object) this.mLapButtonTemp, (Object) null))
      {
        this.mKeepButtonColor = ((Selectable) this.mLapButtonTemp).colors;
        this.mChangeButtonColor = ((Selectable) this.mLapButtonTemp).colors;
        ((ColorBlock) ref this.mChangeButtonColor).normalColor = ((ColorBlock) ref this.mChangeButtonColor).disabledColor;
        ((ColorBlock) ref this.mChangeButtonColor).highlightedColor = ((ColorBlock) ref this.mChangeButtonColor).disabledColor;
      }
      GuildRaidPeriodParam guildRaidPeriodParam = MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(instance.PeriodId);
      if (guildRaidPeriodParam != null && Object.op_Inequality((Object) this.mLapButtonTemp, (Object) null))
      {
        for (int index = 0; index < this.mListButton.Count; ++index)
          Object.Destroy((Object) ((Component) this.mListButton[index]).gameObject);
        this.mListButton.Clear();
        for (int index = 0; index < guildRaidPeriodParam.RoundBuffMax; ++index)
        {
          Button button = Object.Instantiate<Button>(this.mLapButtonTemp, ((Component) this.mLapButtonTemp).transform.parent);
          if (!Object.op_Equality((Object) button, (Object) null))
          {
            GuildRaidBossStageInfoLapItem component = ((Component) button).GetComponent<GuildRaidBossStageInfoLapItem>();
            if (Object.op_Inequality((Object) component, (Object) null) && Object.op_Inequality((Object) component.mText, (Object) null))
              component.mText.text = string.Format(LocalizedText.Get("sys.GUILDRAID_LAP"), (object) (index + 1));
            ((Component) button).gameObject.SetActive(true);
            this.mListButton.Add(button);
          }
        }
        this.SetSelectLapButtonColor();
      }
      GameUtility.SetGameObjectActive(this.mLapList, this.isListDraw);
      GameParameter.UpdateAll(((Component) this).gameObject);
      GuildRaidManager.Instance.ReSetRefreshWaitSec();
    }

    private void SetSelectLapButtonColor()
    {
      for (int index = 0; index < this.mListButton.Count; ++index)
      {
        if (!Object.op_Equality((Object) this.mListButton[index], (Object) null))
        {
          Button button = this.mListButton[index];
          if (Object.op_Equality((Object) button, (Object) null))
            return;
          if (GuildRaidManager.Instance.TrialRound == index + 1)
            ((Selectable) button).colors = this.mKeepButtonColor;
          else
            ((Selectable) button).colors = this.mChangeButtonColor;
        }
      }
      if (!Object.op_Inequality((Object) this.mLapScroll, (Object) null) || this.mListButton.Count <= 0 || GuildRaidManager.Instance.TrialRound == 0)
        return;
      if (GuildRaidManager.Instance.TrialRound == 1)
      {
        this.mLapScroll.verticalNormalizedPosition = 1f;
      }
      else
      {
        float y1 = (((Component) this.mLapButtonTemp).gameObject.transform as RectTransform).sizeDelta.y;
        float y2 = (((Component) this.mLapScroll).gameObject.transform.parent as RectTransform).sizeDelta.y;
        if ((double) y2 <= (double) y1 || (double) y2 == 0.0)
          return;
        this.mLapScroll.verticalNormalizedPosition = Mathf.Clamp(1f - (((float) GuildRaidManager.Instance.TrialRound + 0.5f) / (float) this.mListButton.Count - 0.5f * y1 / y2) / (float) (1.0 - (double) y1 / (double) y2), 0.0f, 1f);
      }
    }

    public void OnNoChallengeButton()
    {
      GuildRaidPeriodParam guildRaidPeriodParam = MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(GuildRaidManager.Instance.PeriodId);
      if (guildRaidPeriodParam == null)
        return;
      string msg = string.Format(LocalizedText.Get("sys.GUILDRAID_NOCHALLENGE_TEXT"), (object) guildRaidPeriodParam.HealAp, (object) guildRaidPeriodParam.Bp);
      if (GuildRaidManager.Instance.BpHealType == GuildRaidManager.GuildRaidBpHealType.Eternal)
        msg = string.Format(LocalizedText.Get("sys.GUILDRAID_NOCHALLENGE_ETERNAL_TEXT"), (object) guildRaidPeriodParam.HealAp, (object) guildRaidPeriodParam.Bp);
      UIUtility.SystemMessage(msg, (UIUtility.DialogResultEvent) null);
    }

    public void OnLapSelectButon(Button obj)
    {
      int num = this.mListButton.IndexOf(obj);
      if (num != -1)
        GuildRaidManager.Instance.TrialRound = num + 1;
      if (Object.op_Inequality((Object) this.mLapText, (Object) null))
        this.mLapText.text = string.Format(LocalizedText.Get("sys.GUILDRAID_LAP"), (object) GuildRaidManager.Instance.TrialRound);
      this.SetSelectLapButtonColor();
      this.isListDraw = false;
      GameUtility.SetGameObjectActive(this.mLapList, this.isListDraw);
    }

    private void SetRewardIcon(GuildRaidReward reward, RewardListItem listItem)
    {
      if (reward == null || Object.op_Equality((Object) listItem, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      GameObject gameObject = (GameObject) null;
      bool flag = false;
      switch (reward.Type)
      {
        case RaidRewardType.Item:
          ItemParam itemParam = instance.GetItemParam(reward.IName);
          if (itemParam != null)
          {
            gameObject = listItem.RewardItem;
            DataSource.Bind<ItemParam>(gameObject, itemParam);
            flag = true;
            break;
          }
          break;
        case RaidRewardType.Gold:
          gameObject = listItem.RewardGold;
          flag = true;
          break;
        case RaidRewardType.Coin:
          gameObject = listItem.RewardCoin;
          flag = true;
          break;
        case RaidRewardType.Award:
          AwardParam awardParam = instance.GetAwardParam(reward.IName);
          if (awardParam == null)
            return;
          gameObject = listItem.RewardAward;
          DataSource.Bind<AwardParam>(gameObject, awardParam);
          break;
        case RaidRewardType.Unit:
          UnitParam unitParam = instance.GetUnitParam(reward.IName);
          if (unitParam != null)
          {
            gameObject = listItem.RewardUnit;
            DataSource.Bind<UnitParam>(gameObject, unitParam);
            break;
          }
          break;
        case RaidRewardType.ConceptCard:
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(reward.IName);
          if (cardDataForDisplay != null)
          {
            gameObject = listItem.RewardCard;
            ConceptCardIcon component = gameObject.GetComponent<ConceptCardIcon>();
            if (Object.op_Inequality((Object) component, (Object) null))
            {
              component.Setup(cardDataForDisplay);
              break;
            }
            break;
          }
          break;
        case RaidRewardType.Artifact:
          ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(reward.IName);
          if (artifactParam != null)
          {
            gameObject = listItem.RewardArtifact;
            DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
            break;
          }
          break;
      }
      if (flag)
      {
        Transform transform = gameObject.transform.Find("amount/Text_amount");
        if (Object.op_Inequality((Object) transform, (Object) null))
        {
          Text component = ((Component) transform).GetComponent<Text>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.text = reward.Num.ToString();
        }
      }
      if (!Object.op_Inequality((Object) gameObject, (Object) null))
        return;
      gameObject.transform.SetParent(listItem.RewardList, false);
      gameObject.SetActive(true);
    }
  }
}
