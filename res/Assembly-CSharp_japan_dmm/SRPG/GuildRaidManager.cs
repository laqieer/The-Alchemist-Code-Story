// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "Start Challenge", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "Selected GuildRaid", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(13, "NextArea", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(14, "BackArea", FlowNode.PinTypes.Input, 14)]
  [FlowNode.Pin(101, "Finish Initialize", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "Beat Effect Start", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(202, "GuildRaid Stage Info", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(203, "GuildRaid Stage Info View", FlowNode.PinTypes.Output, 203)]
  [FlowNode.Pin(901, "Error", FlowNode.PinTypes.Output, 901)]
  public class GuildRaidManager : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_INIT = 1;
    public const int PIN_INPUT_CHALLENGE = 11;
    public const int PIN_INPUT_SELECTED = 12;
    public const int PIN_INPUT_NEXTAREA = 13;
    public const int PIN_INPUT_BACKAREA = 14;
    public const int PIN_OUTPUT_FINISH_INIT = 101;
    public const int PIN_OUTPUT_BEAT_EFFECT = 102;
    public const int PIN_OUTPUT_TO_DETAIL = 202;
    public const int PIN_OUTPUT_TO_VIEW = 203;
    public const int PIN_OUTPUT_ERROR = 901;
    public const int DEFAULT_BOSSCOUNT = 5;
    public const int START_ROUND = 1;
    private static GuildRaidManager mInstance;
    public int CurrentRankingPage = 1;
    public int CurrentRankingPageTotal = 1;
    public int PreviousRankingPage = 1;
    public int PreviousRankingPageTotal = 1;
    public GuildRaidManager.GuildRaidRankingType mRankingType;
    public int CurrentRankingPortPage = 1;
    public int CurrentRankingPortPageTotal = 1;
    public int PreviousRankingPortPage = 1;
    public int PreviousRankingPortPageTotal = 1;
    public int RankingPortBossPage = 1;
    public int RankingPortBossId;
    public int RankingPortBossPageTotal = 1;
    public int RankingDamageSummaryPage = 1;
    public int RankingDamageSummaryPageTotal = 1;
    public int RankingDamageRoundPage = 1;
    public int RankingDamageRoundBossId;
    public int RankingDamageRoundRound;
    public int RankingDamageRoundPageTotal = 1;
    [SerializeField]
    private GuildRaidArea mAreaPrefab;
    [SerializeField]
    private Transform mAreaTransform;
    [SerializeField]
    private GameObject mPeriodTextParent;
    [SerializeField]
    private Text mPeriodText;
    [SerializeField]
    private Text mLapText;
    [SerializeField]
    private Button mRefreshButton;
    [SerializeField]
    private Slider mBpSlider;
    [SerializeField]
    private GameObject mBpSliderLine;
    [SerializeField]
    private GameObject mApSliderType;
    [SerializeField]
    private GameObject mApLoopAp;
    [SerializeField]
    private GameObject mApLoopPlus;
    [SerializeField]
    private GameObject mApLoopEnd;
    [SerializeField]
    private GameObject mApLoopBpRemain;
    [SerializeField]
    private GameObject mApLoopBpEternal;
    private bool isFirstUpdate;

    public static GuildRaidManager Instance => GuildRaidManager.mInstance;

    public int PeriodId { get; private set; }

    public bool CurrentGuildRaidFinish { get; private set; }

    public int CurrentRound { get; private set; }

    public int TrialRound { get; set; }

    public int CurrentAreaNo { get; private set; }

    public int CurrentBossId { get; private set; }

    public int ViewBossId { get; private set; }

    public int AreaBossCount { get; private set; }

    public int CurrentBp { get; private set; }

    public int MaxBp { get; private set; }

    public int ChallengedBp { get; private set; }

    public int CurrentAp { get; private set; }

    public int DefaultBp { get; private set; }

    public int MaxAp { get; private set; }

    public int RefreshWaitSec { get; private set; }

    public GuildRaidArea CurrentArea { get; private set; }

    public GuildRaidData CurrentData { get; private set; }

    public GuildRaidBossInfo CurrentBossInfo { get; private set; }

    public bool IsForcedDeck => this.ForcedDeck != null;

    public bool IsBeatEffect { get; private set; }

    public GuildRaidManager.GuildRaidBpHealType BpHealType { get; private set; }

    public int HealBp { get; private set; }

    public int PortRankingRank { get; private set; }

    public List<GuildRaidChallengingPlayer> CurrentBossChallengingPlayerList { get; private set; }

    public GuildRaidBattleType BattleType { get; set; }

    public List<UnitData> ForcedDeck { get; private set; }

    public List<string> UsedUnitInameList { get; private set; }

    public DateTime RefreshWaitStarted { get; private set; }

    public bool IsReceiveMail { get; private set; }

    public List<GuildRaidMailListItem> MailCurrentPageItemList { get; private set; }

    public GuildRaidMailOption MailCurrentOption { get; private set; }

    public int MailCurrentPage { get; private set; }

    public List<int> MailReceivingIdList { get; private set; }

    public List<GuildRaidMailListItem> MailReceiveItemList { get; private set; }

    public List<int> MailToGiftItemIdList { get; private set; }

    public int BtlTrun { get; private set; }

    public List<GuildRaidRanking> CurrentRankingList { get; private set; }

    public GuildRaidRanking CurrentRankingSelf { get; private set; }

    public List<GuildRaidRanking> PreviousRankingList { get; private set; }

    public GuildRaidRanking PreviousRankingSelf { get; private set; }

    public List<GuildRaidRankingMember> CurrentRankingPortList { get; private set; }

    public GuildRaidRankingMember CurrentRankingPortSelf { get; private set; }

    public List<GuildRaidRankingMember> PreviousRankingPortList { get; private set; }

    public GuildRaidRankingMember PreviousRankingPortSelf { get; private set; }

    public List<GuildRaidRankingMember> RankingPortBossList { get; private set; }

    public List<GuildRaidRankingDamage> RankingDamageSummaryList { get; private set; }

    public List<GuildRaidRankingDamage> RankingDamageRoundList { get; private set; }

    public DateTime PeriodBeginAt { get; private set; }

    public DateTime PeriodEndAt { get; private set; }

    public DateTime RewardEndAt { get; private set; }

    private void Awake()
    {
      GuildRaidManager.mInstance = this;
      this.isFirstUpdate = false;
      this.RefreshWaitStarted = DateTime.MinValue;
    }

    private void OnDestroy() => GuildRaidManager.mInstance = (GuildRaidManager) null;

    private void Update()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRefreshButton, (UnityEngine.Object) null))
        return;
      if (this.IsRefreshConfirm())
        ((Selectable) this.mRefreshButton).interactable = true;
      else
        ((Selectable) this.mRefreshButton).interactable = false;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Refresh();
          break;
        case 11:
          GuildRaidBossParam guildRaidBossParam = MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(this.CurrentBossId);
          if (guildRaidBossParam != null)
            this.ViewBossId = guildRaidBossParam.Id;
          this.StartChallenge();
          break;
      }
    }

    private void Refresh()
    {
      GuildRaidPeriodParam guildRaidPeriodParam = MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(this.PeriodId);
      if (guildRaidPeriodParam == null)
      {
        DebugUtility.LogError("periodParam is null.");
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 901);
      }
      else
      {
        int bossCountByPeriod = MonoSingleton<GameManager>.Instance.GetGuildRaidBossCountByPeriod(this.PeriodId);
        this.AreaBossCount = bossCountByPeriod <= 0 ? 5 : bossCountByPeriod;
        this.SwitchArea(this.CurrentRound - 1);
        this.PeriodBeginAt = guildRaidPeriodParam.BeginAt;
        this.PeriodEndAt = guildRaidPeriodParam.EndAt;
        this.RewardEndAt = guildRaidPeriodParam.RewardEndAt;
        GameUtility.SetGameObjectActive(this.mApSliderType, guildRaidPeriodParam.BpType == GuildRaidManager.GuildRaidApDrawType.Slider);
        GameUtility.SetGameObjectActive(this.mApLoopPlus, guildRaidPeriodParam.BpType == GuildRaidManager.GuildRaidApDrawType.Loop);
        GameUtility.SetGameObjectActive(this.mApLoopAp, guildRaidPeriodParam.BpType == GuildRaidManager.GuildRaidApDrawType.Loop);
        GameUtility.SetGameObjectActive(this.mApLoopEnd, guildRaidPeriodParam.BpType == GuildRaidManager.GuildRaidApDrawType.Loop && guildRaidPeriodParam.Bp <= this.HealBp);
        this.BpHealType = GuildRaidManager.GuildRaidBpHealType.Normal;
        if (guildRaidPeriodParam.Bp == 0 && guildRaidPeriodParam.HealAp == 0)
        {
          this.BpHealType = GuildRaidManager.GuildRaidBpHealType.Nothing;
          GameUtility.SetGameObjectActive(this.mApSliderType, false);
          GameUtility.SetGameObjectActive(this.mApLoopPlus, false);
          GameUtility.SetGameObjectActive(this.mApLoopAp, false);
          GameUtility.SetGameObjectActive(this.mApLoopEnd, false);
        }
        else if (guildRaidPeriodParam.Bp == 0 && guildRaidPeriodParam.BpType == GuildRaidManager.GuildRaidApDrawType.Loop)
        {
          this.BpHealType = GuildRaidManager.GuildRaidBpHealType.Eternal;
          GameUtility.SetGameObjectActive(this.mApLoopEnd, false);
        }
        GameUtility.SetGameObjectActive(this.mApLoopBpRemain, this.BpHealType == GuildRaidManager.GuildRaidBpHealType.Normal);
        GameUtility.SetGameObjectActive(this.mApLoopBpEternal, this.BpHealType == GuildRaidManager.GuildRaidBpHealType.Eternal);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPeriodTextParent, (UnityEngine.Object) null))
          this.mPeriodTextParent.SetActive(true);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPeriodText, (UnityEngine.Object) null))
        {
          if (this.IsPeriodEndAt())
            this.mPeriodText.text = string.Format(LocalizedText.Get("sys.GUILDRAID_REWARD_DATETIME"), (object) this.RewardEndAt.Month, (object) this.RewardEndAt.Day, (object) this.RewardEndAt.Hour, (object) this.RewardEndAt.Minute);
          else
            this.mPeriodText.text = string.Format(LocalizedText.Get("sys.GUILDRAID_PERIOD_DATETIME"), (object) this.PeriodEndAt.Month, (object) this.PeriodEndAt.Day, (object) this.PeriodEndAt.Hour, (object) this.PeriodEndAt.Minute);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mLapText, (UnityEngine.Object) null))
          this.mLapText.text = (this.CurrentRound - 1).ToString();
        this.MailCurrentPage = 1;
        int currentBossId = this.CurrentBossId;
        this.ViewBossId = currentBossId;
        this.RankingDamageRoundBossId = currentBossId;
        this.RankingDamageRoundRound = this.CurrentRound;
        if (!this.isFirstUpdate)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBpSlider, (UnityEngine.Object) null))
          {
            float num = 1f / (float) guildRaidPeriodParam.Bp;
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBpSliderLine, (UnityEngine.Object) null))
            {
              this.mBpSliderLine.SetActive(false);
              for (int index = 1; index < guildRaidPeriodParam.Bp; ++index)
              {
                GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mBpSliderLine, this.mBpSliderLine.transform.parent);
                if (!UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
                {
                  gameObject.GetComponent<Slider>().value = num * (float) index;
                  gameObject.SetActive(true);
                }
              }
            }
            this.mBpSlider.value = (float) this.CurrentAp / (float) this.MaxAp;
          }
          this.isFirstUpdate = true;
        }
        if (HomeWindow.GetRestorePoint() == RestorePoints.GuildRaid)
          HomeWindow.SetRestorePoint(RestorePoints.Home);
        GameParameter.UpdateAll(((Component) this).gameObject);
        if (this.IsBeatEffect)
        {
          this.IsBeatEffect = false;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
        }
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
    }

    public bool IsPeriodEndAt() => !(TimeManager.ServerTime < this.PeriodEndAt);

    public void SetupCurrentData(JSON_GuildRaidCurrent json)
    {
      if (json == null)
      {
        DebugUtility.LogError("SetupCurrentData: json is null");
      }
      else
      {
        this.PeriodId = json.period_id;
        this.CurrentRound = json.round;
        this.CurrentGuildRaidFinish = json.is_finish != 0;
        GuildRaidPeriodParam guildRaidPeriodParam = MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(this.PeriodId);
        if (guildRaidPeriodParam == null)
        {
          DebugUtility.LogError("periodParam is null.");
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 901);
        }
        else if (guildRaidPeriodParam.BpType == GuildRaidManager.GuildRaidApDrawType.Slider)
          this.MaxAp = guildRaidPeriodParam.GetMaxAp();
        else
          this.MaxAp = guildRaidPeriodParam.HealAp;
      }
    }

    public void SetupPrevData(JSON_GuildRaidPrev json, int round, int boss_id)
    {
      this.IsBeatEffect = false;
      if (json == null)
        return;
      this.IsBeatEffect = json.round != round || json.boss_id != boss_id;
      if (json.round != 0 || json.boss_id != 0)
        return;
      this.IsBeatEffect = false;
    }

    public void SetupBossInfo(JSON_GuildRaidBossInfo json, bool isCurrent = false)
    {
      if (json == null)
      {
        DebugUtility.LogError("SetupBossInfo: json is null");
      }
      else
      {
        this.CurrentBossInfo = new GuildRaidBossInfo();
        this.CurrentBossInfo.Deserialize(json);
        if (!isCurrent)
          return;
        this.CurrentBossId = this.CurrentBossInfo.BossId;
        this.CurrentAreaNo = MonoSingleton<GameManager>.Instance.GetGuildRaidBossIndex(this.PeriodId, this.CurrentBossId);
      }
    }

    public void SetupRankingRank(int rank) => this.PortRankingRank = rank;

    public void SetupChallengingPlayers(JSON_GuildRaidChallengingPlayer[] json)
    {
      this.CurrentBossChallengingPlayerList = new List<GuildRaidChallengingPlayer>();
      if (json == null || json == null)
        return;
      for (int index = 0; index < json.Length; ++index)
      {
        if (json[index] != null)
        {
          GuildRaidChallengingPlayer challengingPlayer = new GuildRaidChallengingPlayer();
          if (challengingPlayer.Deserialize(json[index]))
            this.CurrentBossChallengingPlayerList.Add(challengingPlayer);
        }
      }
    }

    public void SetupBattlePoint(JSON_GuildRaidBattlePoint json)
    {
      if (json == null)
      {
        DebugUtility.LogError("SetupBattlePoint: json is null");
      }
      else
      {
        this.CurrentBp = json.pt + json.defbp;
        this.CurrentAp = json.ap;
        this.MaxBp = json.max;
        this.DefaultBp = json.defbp;
        GuildRaidPeriodParam guildRaidPeriodParam = MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(this.PeriodId);
        if (guildRaidPeriodParam == null)
          return;
        if (this.CurrentAp > 0)
        {
          if (guildRaidPeriodParam.BpType == GuildRaidManager.GuildRaidApDrawType.Slider && this.CurrentAp > this.MaxAp)
            this.CurrentAp = this.MaxAp;
          this.HealBp = this.CurrentAp / guildRaidPeriodParam.HealAp;
          if (this.HealBp > guildRaidPeriodParam.Bp)
            this.HealBp = guildRaidPeriodParam.Bp;
        }
        int num = guildRaidPeriodParam.DefaultBp - json.defbp;
        this.ChallengedBp = this.HealBp - json.pt + num;
        if (this.ChallengedBp >= 0)
          return;
        this.ChallengedBp = 0;
      }
    }

    public void SetupForcedDeck(Json_Unit[] json)
    {
      if (json == null || json.Length <= 0)
      {
        this.ForcedDeck = (List<UnitData>) null;
      }
      else
      {
        this.ForcedDeck = new List<UnitData>();
        for (int index = 0; index < json.Length; ++index)
        {
          if (json[index] == null || string.IsNullOrEmpty(json[index].iname))
          {
            this.ForcedDeck.Add((UnitData) null);
          }
          else
          {
            UnitData unitData = new UnitData();
            unitData.Deserialize(json[index]);
            this.ForcedDeck.Add(unitData);
          }
        }
      }
    }

    public void SetupSelectedUnits(string[] json)
    {
      this.UsedUnitInameList = new List<string>();
      if (json == null || json.Length <= 0)
        return;
      this.UsedUnitInameList.AddRange((IEnumerable<string>) json);
    }

    public void SetupRefreshWaitSec(int refreshWaitSec)
    {
      this.RefreshWaitSec = refreshWaitSec;
      this.RefreshWaitStarted = TimeManager.ServerTime;
    }

    public void SetupIsReceiveMail(int receiveMailCount)
    {
      this.IsReceiveMail = receiveMailCount > 0;
    }

    public void SetupMail(JSON_GuildRaidMail json)
    {
      if (json == null)
      {
        DebugUtility.LogError("SetupMail: json is null");
      }
      else
      {
        this.MailCurrentPageItemList = new List<GuildRaidMailListItem>();
        if (json.list != null)
        {
          for (int index = 0; index < json.list.Length; ++index)
          {
            if (json.list[index] != null)
            {
              GuildRaidMailListItem raidMailListItem = new GuildRaidMailListItem();
              if (raidMailListItem.Deserialize(json.list[index]))
                this.MailCurrentPageItemList.Add(raidMailListItem);
            }
          }
        }
        this.MailCurrentOption = (GuildRaidMailOption) null;
        if (json.option != null)
        {
          this.MailCurrentOption = new GuildRaidMailOption();
          this.MailCurrentOption.Deserialize(json.option);
        }
        this.IsReceiveMail = json.list != null && json.list.Length > 0;
        GameParameter.UpdateAll(((Component) this).gameObject);
      }
    }

    public void SetupMailReceived(JSON_GuildRaidMailListItem[] json)
    {
      this.MailReceiveItemList = new List<GuildRaidMailListItem>();
      if (json != null)
      {
        for (int index = 0; index < json.Length; ++index)
        {
          if (json[index] != null)
          {
            GuildRaidMailListItem raidMailListItem = new GuildRaidMailListItem();
            if (raidMailListItem.Deserialize(json[index]))
              this.MailReceiveItemList.Add(raidMailListItem);
          }
        }
      }
      RewardData rewardData = new RewardData();
      GameManager gm = MonoSingleton<GameManager>.Instance;
      this.MailReceiveItemList.ForEach((Action<GuildRaidMailListItem>) (item => gm.GetGuildRaidRewardParam(item.RewardId)?.Rewards.ForEach((Action<GuildRaidReward>) (reward =>
      {
        switch (reward.Type)
        {
          case RaidRewardType.Item:
            ItemParam ip = gm.GetItemParam(reward.IName);
            if (ip == null)
              break;
            ItemData itemData1 = rewardData.Items.Find((Predicate<ItemData>) (ri => ri.Param.iname == ip.iname));
            if (itemData1 != null)
            {
              itemData1.Gain(reward.Num);
              break;
            }
            ItemData itemData2 = new ItemData();
            if (!itemData2.Setup(0L, ip.iname, reward.Num))
              break;
            rewardData.Items.Add(itemData2);
            break;
          case RaidRewardType.Gold:
            rewardData.Gold += reward.Num;
            break;
          case RaidRewardType.Coin:
            rewardData.Coin += reward.Num;
            break;
          case RaidRewardType.Award:
            if (gm.MasterParam.GetAwardParam(reward.IName) == null)
              break;
            if (!rewardData.GiftRecieveItemDataDic.ContainsKey(reward.IName))
            {
              rewardData.GiftRecieveItemDataDic.Add(reward.IName, new GiftRecieveItemData()
              {
                iname = reward.IName,
                num = 1,
                type = GiftTypes.Award
              });
              break;
            }
            ++rewardData.GiftRecieveItemDataDic[reward.IName].num;
            break;
          case RaidRewardType.Unit:
            if (gm.GetUnitParam(reward.IName) == null)
              break;
            if (!rewardData.GiftRecieveItemDataDic.ContainsKey(reward.IName))
            {
              rewardData.GiftRecieveItemDataDic.Add(reward.IName, new GiftRecieveItemData()
              {
                iname = reward.IName,
                num = 1,
                type = GiftTypes.Unit
              });
              break;
            }
            ++rewardData.GiftRecieveItemDataDic[reward.IName].num;
            break;
          case RaidRewardType.ConceptCard:
            if (ConceptCardData.CreateConceptCardDataForDisplay(reward.IName) == null)
              break;
            if (!rewardData.GiftRecieveItemDataDic.ContainsKey(reward.IName))
            {
              rewardData.GiftRecieveItemDataDic.Add(reward.IName, new GiftRecieveItemData()
              {
                iname = reward.IName,
                num = reward.Num,
                type = GiftTypes.ConceptCard
              });
              break;
            }
            rewardData.GiftRecieveItemDataDic[reward.IName].num += reward.Num;
            break;
          case RaidRewardType.Artifact:
            if (gm.MasterParam.GetArtifactParam(reward.IName) == null)
              break;
            if (!rewardData.GiftRecieveItemDataDic.ContainsKey(reward.IName))
            {
              rewardData.GiftRecieveItemDataDic.Add(reward.IName, new GiftRecieveItemData()
              {
                iname = reward.IName,
                num = reward.Num,
                type = GiftTypes.Artifact
              });
              break;
            }
            rewardData.GiftRecieveItemDataDic[reward.IName].num += reward.Num;
            break;
        }
      }))));
      GlobalVars.LastReward.Set(rewardData);
    }

    public void SetupMailToGiftList(int[] ids)
    {
      if (ids == null)
        this.MailToGiftItemIdList = new List<int>();
      else
        this.MailToGiftItemIdList = new List<int>((IEnumerable<int>) ids);
    }

    public void SetupCurrentRankingList(JSON_GuildRaidRanking[] json, bool isOverwrite = false)
    {
      if (this.CurrentRankingList == null || isOverwrite)
        this.CurrentRankingList = new List<GuildRaidRanking>();
      if (json == null)
        return;
      for (int index = 0; index < json.Length; ++index)
      {
        if (json[index] != null)
        {
          GuildRaidRanking guildRaidRanking = new GuildRaidRanking();
          if (guildRaidRanking.Deserialize(json[index]))
            this.CurrentRankingList.Add(guildRaidRanking);
        }
      }
    }

    public void SetupCurrentRankingSelf(JSON_GuildRaidRanking json)
    {
      if (json == null)
      {
        DebugUtility.LogError("SetupCurrentRankingSelf: json is null");
      }
      else
      {
        this.CurrentRankingSelf = new GuildRaidRanking();
        this.CurrentRankingSelf.Deserialize(json);
      }
    }

    public void SetupPreviousRankingList(JSON_GuildRaidRanking[] json, bool isOverwrite = false)
    {
      if (this.PreviousRankingList == null || isOverwrite)
        this.PreviousRankingList = new List<GuildRaidRanking>();
      if (json == null)
        return;
      for (int index = 0; index < json.Length; ++index)
      {
        if (json[index] != null)
        {
          GuildRaidRanking guildRaidRanking = new GuildRaidRanking();
          if (guildRaidRanking.Deserialize(json[index]))
            this.PreviousRankingList.Add(guildRaidRanking);
        }
      }
    }

    public void SetupPreviousRankingSelf(JSON_GuildRaidRanking json)
    {
      if (json == null)
      {
        DebugUtility.LogError("SetupPreviousRankingSelf: json is null");
      }
      else
      {
        this.PreviousRankingSelf = new GuildRaidRanking();
        this.PreviousRankingSelf.Deserialize(json);
      }
    }

    public void SetupCurrentRankingPortList(JSON_GuildRaidRankingMember[] json, bool isOverwrite = false)
    {
      if (this.CurrentRankingPortList == null || isOverwrite)
        this.CurrentRankingPortList = new List<GuildRaidRankingMember>();
      if (json == null)
        return;
      for (int index = 0; index < json.Length; ++index)
      {
        if (json[index] != null)
        {
          GuildRaidRankingMember raidRankingMember = new GuildRaidRankingMember();
          if (raidRankingMember.Deserialize(json[index]))
            this.CurrentRankingPortList.Add(raidRankingMember);
        }
      }
    }

    public void SetupCurrentRankingPortSelf(JSON_GuildRaidRankingMember json)
    {
      if (json == null)
      {
        DebugUtility.LogError("SetupCurrentRankingPortSelf: json is null");
      }
      else
      {
        this.CurrentRankingPortSelf = new GuildRaidRankingMember();
        this.CurrentRankingPortSelf.Deserialize(json);
      }
    }

    public void SetupPreviousRankingPortList(JSON_GuildRaidRankingMember[] json, bool isOverwrite = false)
    {
      if (this.PreviousRankingPortList == null || isOverwrite)
        this.PreviousRankingPortList = new List<GuildRaidRankingMember>();
      if (json == null)
        return;
      for (int index = 0; index < json.Length; ++index)
      {
        if (json[index] != null)
        {
          GuildRaidRankingMember raidRankingMember = new GuildRaidRankingMember();
          if (raidRankingMember.Deserialize(json[index]))
            this.PreviousRankingPortList.Add(raidRankingMember);
        }
      }
    }

    public void SetupPreviousRankingPortSelf(JSON_GuildRaidRankingMember json)
    {
      if (json == null)
      {
        DebugUtility.LogError("SetupPreviousRankingPortSelf: json is null");
      }
      else
      {
        this.PreviousRankingPortSelf = new GuildRaidRankingMember();
        this.PreviousRankingPortSelf.Deserialize(json);
      }
    }

    public void SetupPreviousRankingPortBoss(JSON_GuildRaidRankingMember[] json, bool isOverwrite = false)
    {
      if (this.RankingPortBossList == null || isOverwrite)
        this.RankingPortBossList = new List<GuildRaidRankingMember>();
      if (json == null)
        return;
      for (int index = 0; index < json.Length; ++index)
      {
        if (json[index] != null)
        {
          GuildRaidRankingMember raidRankingMember = new GuildRaidRankingMember();
          if (raidRankingMember.Deserialize(json[index]))
            this.RankingPortBossList.Add(raidRankingMember);
        }
      }
    }

    public void SetupRankingDamageSummary(JSON_GuildRaidRankingDamage[] json, bool isOverwrite = false)
    {
      if (this.RankingDamageSummaryList == null || isOverwrite)
        this.RankingDamageSummaryList = new List<GuildRaidRankingDamage>();
      if (json == null)
        return;
      for (int index = 0; index < json.Length; ++index)
      {
        if (json[index] != null)
        {
          GuildRaidRankingDamage raidRankingDamage = new GuildRaidRankingDamage();
          if (raidRankingDamage.Deserialize(json[index]))
            this.RankingDamageSummaryList.Add(raidRankingDamage);
        }
      }
    }

    public void SetupRankingDamageRound(JSON_GuildRaidRankingDamage[] json, bool isOverwrite = false)
    {
      if (this.RankingDamageRoundList == null || isOverwrite)
        this.RankingDamageRoundList = new List<GuildRaidRankingDamage>();
      if (json == null)
        return;
      for (int index = 0; index < json.Length; ++index)
      {
        if (json[index] != null)
        {
          GuildRaidRankingDamage raidRankingDamage = new GuildRaidRankingDamage();
          if (raidRankingDamage.Deserialize(json[index]))
            this.RankingDamageRoundList.Add(raidRankingDamage);
        }
      }
    }

    public void AddMailReceivingIdList(int giftId)
    {
      if (this.MailReceivingIdList == null)
        this.MailReceivingIdList = new List<int>();
      this.MailReceivingIdList.Add(giftId);
    }

    public void ResetMailReceivingIdList()
    {
      if (this.MailReceivingIdList == null)
        this.MailReceivingIdList = new List<int>();
      this.MailReceivingIdList.Clear();
    }

    private void SwitchArea(int round_index)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CurrentArea, (UnityEngine.Object) null))
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.CurrentArea).gameObject);
        this.CurrentArea = (GuildRaidArea) null;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mAreaPrefab, (UnityEngine.Object) null))
        return;
      this.CurrentArea = UnityEngine.Object.Instantiate<GuildRaidArea>(this.mAreaPrefab, this.mAreaTransform);
      this.CurrentArea.Initialize(round_index);
    }

    public void StartChallenge() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 202);

    public GuildRaidBossParam BeatedGuildRaidBossList(int round, int area_no)
    {
      return round < this.CurrentRound || area_no <= this.CurrentAreaNo ? MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(this.PeriodId, area_no) : (GuildRaidBossParam) null;
    }

    public bool IsFinishGuildRaid() => this.CurrentGuildRaidFinish;

    public bool IsLastArea()
    {
      return MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(this.PeriodId, this.CurrentAreaNo) == null;
    }

    public void ShowDetail(GuildRaidBossParam bossParam)
    {
      if (bossParam == null)
        return;
      int id = bossParam.Id;
      this.ViewBossId = id;
      this.RankingDamageRoundBossId = id;
      this.RankingDamageRoundRound = this.CurrentRound;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 203);
    }

    public int GetBossNo(GuildRaidBossParam bossParam)
    {
      return bossParam == null ? 0 : this.CurrentRound * this.AreaBossCount + bossParam.AreaNo;
    }

    public void ReSetRefreshWaitSec() => this.RefreshWaitStarted = TimeManager.ServerTime;

    public void SetRefreshButton()
    {
      this.RefreshWaitStarted = this.RefreshWaitStarted.AddSeconds((double) (-1 * this.RefreshWaitSec));
    }

    public bool IsRefreshConfirm()
    {
      return (TimeManager.ServerTime - this.RefreshWaitStarted).TotalSeconds > (double) this.RefreshWaitSec;
    }

    public bool IsCloseSchedule()
    {
      return MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodScheduleType() == GuildRaidManager.GuildRaidScheduleType.CloseSchedule || MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodScheduleType() == GuildRaidManager.GuildRaidScheduleType.Close;
    }

    public bool IsBossChallenge(GuildRaidBossInfo info)
    {
      return this.IsBossTrial(info) && (this.CurrentBp != 0 || this.IsForcedDeck);
    }

    public bool IsBossTrial(GuildRaidBossInfo info)
    {
      return !this.IsFinishGuildRaid() && !this.IsCloseSchedule() && info != null && info.CurrentHP != 0;
    }

    public void SetAreaBossOverflow(int cnt)
    {
      if (cnt <= 0 || this.AreaBossCount <= cnt)
        return;
      this.AreaBossCount = cnt;
    }

    public static void SetNotifyPush(int cnt = 0)
    {
      if (cnt == 0)
        NotifyList.Push(LocalizedText.Get("sys.GUILDRAID_BPHEAL_MESSAGE"));
      else
        NotifyList.Push(string.Format(LocalizedText.Get("sys.GUILDRAID_BPHEAL_SET_MESSAGE"), (object) cnt));
    }

    public enum GuildRaidScheduleType
    {
      Open,
      Close,
      OpenSchedule,
      CloseSchedule,
    }

    public enum GuildRaidRankingType
    {
      Current,
      Previous,
    }

    public enum GuildRaidApDrawType
    {
      Slider,
      Loop,
    }

    public enum GuildRaidBpHealType
    {
      Normal,
      Nothing,
      Eternal,
    }
  }
}
