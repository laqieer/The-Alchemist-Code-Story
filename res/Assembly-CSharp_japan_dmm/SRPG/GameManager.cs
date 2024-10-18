// Decompiled with JetBrains decompiler
// Type: SRPG.GameManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using gu3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/Manager/Game")]
  public class GameManager : MonoSingleton<GameManager>
  {
    private const string MasterParamPath = "Data/MasterParam";
    private const string MasterParamSerializedPath = "Data/MasterParamSerialized";
    private const string QuestParamPath = "Data/QuestParam";
    private const string QuestParamSerializedPath = "Data/QuestParamSerialized";
    private bool mRelogin;
    private PlayerData mPlayer = new PlayerData();
    private OptionData mOption = new OptionData();
    private MasterParam mMasterParam = new MasterParam();
    private List<SectionParam> mWorlds = new List<SectionParam>();
    private List<ArchiveParam> mArchives = new List<ArchiveParam>();
    private List<ChapterParam> mAreas = new List<ChapterParam>();
    private List<QuestParam> mQuests = new List<QuestParam>();
    private Dictionary<string, QuestParam> mQuestsDict = new Dictionary<string, QuestParam>();
    private List<ObjectiveParam> mObjectives = new List<ObjectiveParam>();
    private List<ObjectiveParam> mTowerObjectives = new List<ObjectiveParam>();
    private List<MagnificationParam> mMagnifications = new List<MagnificationParam>();
    private List<QuestCondParam> mConditions = new List<QuestCondParam>();
    private List<QuestPartyParam> mParties = new List<QuestPartyParam>();
    private List<QuestCampaignParentParam> mCampaignParents = new List<QuestCampaignParentParam>();
    private List<QuestCampaignChildParam> mCampaignChildren = new List<QuestCampaignChildParam>();
    private List<QuestCampaignTrust> mCampaignTrust = new List<QuestCampaignTrust>();
    private List<QuestCampaignInspSkill> mCampaignInspSkill = new List<QuestCampaignInspSkill>();
    private List<QuestParam> mTowerBaseQuests = new List<QuestParam>();
    private List<TowerFloorParam> mTowerFloors = new List<TowerFloorParam>();
    private Dictionary<string, TowerFloorParam> mTowerFloorsDict = new Dictionary<string, TowerFloorParam>();
    private List<TowerRewardParam> mTowerRewards = new List<TowerRewardParam>();
    private List<TowerRoundRewardParam> mTowerRoundRewards = new List<TowerRoundRewardParam>();
    private List<TowerParam> mTowers = new List<TowerParam>();
    private TowerResuponse mTowerResuponse = new TowerResuponse();
    private List<ArenaPlayer> mArenaPlayers = new List<ArenaPlayer>();
    private List<ArenaPlayer>[] mArenaRanking = new List<ArenaPlayer>[2]
    {
      new List<ArenaPlayer>(),
      new List<ArenaPlayer>()
    };
    private List<ArenaPlayerHistory> mArenaHistory = new List<ArenaPlayerHistory>();
    private List<GachaParam> mGachas = new List<GachaParam>();
    private List<ChatChannelMasterParam> mChatChannelMasters = new List<ChatChannelMasterParam>();
    private List<UnitParam> mBadgeUnitUnlocks;
    private List<AchievementParam> mAchievement = new List<AchievementParam>();
    private GameObject mNotifyList;
    private List<MultiRanking> mMultiUnitRank = new List<MultiRanking>();
    private Dictionary<string, RankingData> mUnitRanking = new Dictionary<string, RankingData>();
    private List<VersusTowerParam> mVersusTowerFloor = new List<VersusTowerParam>();
    private VsTowerMatchEndParam mVersusEndParam = new VsTowerMatchEndParam();
    private List<VersusScheduleParam> mVersusScheduleParam = new List<VersusScheduleParam>();
    private List<VersusCoinParam> mVersusCoinParam = new List<VersusCoinParam>();
    private List<VersusFriendScore> mVersusFriendScore = new List<VersusFriendScore>();
    private List<SRPG.MapEffectParam> mMapEffectParam;
    private List<WeatherSetParam> mWeatherSetParam;
    private List<RankingQuestParam> mRankingQuestParam = new List<RankingQuestParam>();
    private List<RankingQuestRewardParam> mRankingQuestRewardParam = new List<RankingQuestRewardParam>();
    private List<RankingQuestScheduleParam> mRankingQuestScheduleParam = new List<RankingQuestScheduleParam>();
    private List<RankingQuestParam> mAvailableRankingQuesstParams = new List<RankingQuestParam>();
    private List<GenesisStarParam> mGenesisStarParam;
    private List<GenesisChapterParam> mGenesisChapterParam;
    private List<GenesisRewardParam> mGenesisRewardParam;
    private List<GenesisLapBossParam> mGenesisLapBossParam;
    private List<AdvanceStarParam> mAdvanceStarParam;
    private List<AdvanceEventParam> mAdvanceEventParam;
    private List<AdvanceRewardParam> mAdvanceRewardParam;
    private List<AdvanceLapBossParam> mAdvanceLapBossParam;
    private List<GuildRaidBossParam> mGuildRaidBossParam;
    private List<GuildRaidCoolDaysParam> mGuildRaidCoolDaysParam;
    private List<GuildRaidPeriodParam> mGuildRaidPeriodParam;
    private List<GuildRaidScoreParam> mGuildRaidScoreParam;
    private List<GuildRaidRewardParam> mGuildRaidRewardParam;
    private List<GuildRaidRewardDmgRankingParam> mGuildRaidRewardDmgRankingParam;
    private List<GuildRaidRewardDmgRatioParam> mGuildRaidRewardDmgRatioParam;
    private List<GuildRaidRewardRoundParam> mGuildRaidRewardRoundParam;
    private List<GuildRaidRewardRankingParam> mGuildRaidRewardRankingParam;
    public List<GvGPeriodParam> mGvGPeriodParam;
    public List<GvGNodeParam> mGvGNodeParam;
    public List<GvGNPCPartyParam> mGvGNPCPartyParam;
    public List<GvGNPCUnitParam> mGvGNPCUnitParam;
    public List<GvGRewardRankingParam> mGvGRewardRankingParam;
    public List<GvGRewardParam> mGvGRewardParam;
    public List<GvGRuleParam> mGvGRuleParam;
    public List<GvGNodeRewardParam> mGvGNodeRewardParam;
    public List<GvGLeagueParam> mGvGLeagueParam;
    public List<GvGCalcRateSettingParam> mGvGCalcRateSettingParam;
    private List<WorldRaidParam> mWorldRaidParamList;
    private List<WorldRaidBossParam> mWorldRaidBossParamList;
    private List<WorldRaidRewardParam> mWorldRaidRewardParamList;
    private List<WorldRaidDamageRewardParam> mWorldRaidDamageRewardParamList;
    private List<WorldRaidDamageLotteryParam> mWorldRaidDamageLotteryParamList;
    private List<WorldRaidRankingRewardParam> mWorldRaidRankingRewardParamList;
    private VersusAudienceManager mAudienceManager;
    private List<MultiTowerFloorParam> mMultiTowerFloor = new List<MultiTowerFloorParam>();
    private List<MultiTowerRewardParam> mMultiTowerRewards = new List<MultiTowerRewardParam>();
    private MultiTowerRoundParam mMultiTowerRound = new MultiTowerRoundParam();
    public int MaxClearFloor;
    private List<VersusFirstWinBonusParam> mVersusFirstWinBonus = new List<VersusFirstWinBonusParam>();
    private List<VersusStreakWinScheduleParam> mVersusStreakSchedule = new List<VersusStreakWinScheduleParam>();
    private List<VersusStreakWinBonusParam> mVersusStreakWinBonus = new List<VersusStreakWinBonusParam>();
    private List<VersusRuleParam> mVersusRule = new List<VersusRuleParam>();
    private List<VersusCoinCampParam> mVersusCoinCamp = new List<VersusCoinCampParam>();
    private int mVersusNowStreakWinCnt;
    private List<VersusEnableTimeParam> mVersusEnableTime = new List<VersusEnableTimeParam>();
    private List<VersusRankParam> mVersusRank = new List<VersusRankParam>();
    private List<VersusRankClassParam> mVersusRankClass = new List<VersusRankClassParam>();
    private List<VersusRankRankingRewardParam> mVersusRankRankingReward = new List<VersusRankRankingRewardParam>();
    private List<VersusRankRewardParam> mVersusRankReward = new List<VersusRankRewardParam>();
    private List<VersusRankMissionScheduleParam> mVersusRankMissionSchedule = new List<VersusRankMissionScheduleParam>();
    private List<VersusRankMissionParam> mVersusRankMission = new List<VersusRankMissionParam>();
    private List<GuerrillaShopAdventQuestParam> mGuerrillaShopAdventQuest = new List<GuerrillaShopAdventQuestParam>();
    private List<GuerrillaShopScheduleParam> mGuerrillaShopScheduleParam = new List<GuerrillaShopScheduleParam>();
    private List<VersusDraftDeckParam> mVersusDraftDecks = new List<VersusDraftDeckParam>();
    private List<VersusDraftUnitParam> mVersusDraftUnit = new List<VersusDraftUnitParam>();
    private List<string> mTips = new List<string>();
    public List<QuestLobbyNews> mQuestLobbyNews = new List<QuestLobbyNews>();
    private int mVersusBestStreakWinCnt;
    private int mVersusAllPriodStreakWinCount;
    private int mVersusBestAllPriodStreakWinCount;
    private bool mVersusFirstWinRewardRecived;
    private long mVersusFreeExpiredTime;
    private long mVersusFreeNextTime;
    private VersusDraftType mVersusDraftType;
    private bool mSelectedVersusCpuBattle;
    private List<SRPG.VersusCpuData> mVersusCpuData = new List<SRPG.VersusCpuData>();
    private ReqRaidBtlEnd.Response mRaidBtlEndParam = new ReqRaidBtlEnd.Response();
    private static string sBattleVersion;
    public ReqFgGAuth.eAuthStatus AuthStatus;
    public string FgGAuthHost;
    private string mReloadMasterDataError;
    public string MasterDataLoadErrorMessage = string.Empty;
    private MyGUID mMyGuid;
    public GameManager.BadgeTypes IsBusyUpdateBadges;
    public GameManager.BadgeTypes BadgeFlags;
    private DateTime mLastUpdateTime;
    private int mLastStamina;
    private long mLastGold;
    private int mLastAbilityRankUpCount;
    public GameManager.DayChangeEvent OnDayChange = (GameManager.DayChangeEvent) (() => { });
    public GameManager.StaminaChangeEvent OnStaminaChange = (GameManager.StaminaChangeEvent) (() => { });
    public GameManager.RankUpCountChangeEvent OnAbilityRankUpCountChange = (GameManager.RankUpCountChangeEvent) (n => { });
    public GameManager.RankUpCountChangeEvent OnAbilityRankUpCountPreReset = (GameManager.RankUpCountChangeEvent) (n => { });
    public GameManager.PlayerLvChangeEvent OnPlayerLvChange = (GameManager.PlayerLvChangeEvent) (() => { });
    public GameManager.PlayerCurrencyChangeEvent OnPlayerCurrencyChange = (GameManager.PlayerCurrencyChangeEvent) (() => { });
    private const float DesiredFrameTime = 0.026f;
    private const float MaxFrameTime = 0.03f;
    private const float MinAnimationFrameTime = 0.001f;
    private const float MaxAnimationFrameTime = 0.006f;
    public bool EnableAnimationFrameSkipping;
    private bool mHasLoggedIn;
    private static bool mUpscaleMode;
    private GameObject mBuyCoinWindow;
    private GameManager.BuyCoinEvent mOnBuyCoinEnd;
    private GameManager.BuyCoinEvent mOnBuyCoinCancel;
    private List<Coroutine> mImportantJobs = new List<Coroutine>();
    private Coroutine mImportantJobCoroutine;
    public GameManager.SceneChangeEvent OnSceneChange = (GameManager.SceneChangeEvent) (() => true);
    private List<GameManager.TextureRequest> mTextureRequests = new List<GameManager.TextureRequest>();
    private int mTutorialStep;
    private List<AssetList.Item> mTutorialDLAssets = new List<AssetList.Item>(1024);
    private bool mScannedTutorialAssets;
    private Coroutine mWaitDownloadThread;
    private static readonly int SAVE_UPDATE_TROPHY_LIST_ENCODE_KEY = 41213;
    private string mSavedUpdateTrophyListString;
    private List<TrophyState> mUpdateTrophyList;
    private List<TrophyState> mUpdateChallengeList;
    private List<TrophyState> mUpdateTrophyAward;
    public UpdateTrophyLock update_trophy_lock = new UpdateTrophyLock();
    public UpdateTrophyInterval update_trophy_interval = new UpdateTrophyInterval();
    private bool is_start_server_sync_trophy_exec;
    private List<TrophyState> mServerSyncTrophyList;
    private List<TrophyState> mServerSyncChallengeList;
    private List<TrophyState> mServerSyncTrophyAward;
    private List<UnitData> mCharacterQuestUnits;
    private long mLimitedShopEndAt;
    private JSON_ShopListArray.Shops[] mLimitedShopList;
    private bool mIsLimitedShopOpen;
    private GameManager.VersusRange[] mFreeMatchRange;
    private GameManager.QuestTime mQuestPlayTime = new GameManager.QuestTime();

    public bool IsRelogin
    {
      get => this.mRelogin;
      set => this.mRelogin = value;
    }

    private string AgreedVer
    {
      get => (string) MonoSingleton<UserInfoManager>.Instance.GetValue("tou_agreed_ver");
      set => MonoSingleton<UserInfoManager>.Instance.SetValue("tou_agreed_ver", (object) value);
    }

    public bool VersusTowerMatchBegin { get; set; }

    public bool VersusTowerMatchReceipt { get; set; }

    public string VersusTowerMatchName { get; set; }

    public long VersusTowerMatchEndAt { get; set; }

    public int VersusCoinRemainCnt { get; set; }

    public string VersusLastUid { get; set; }

    public bool RankMatchBegin { get; set; }

    public int RankMatchScheduleId { get; set; }

    public ReqRankMatchStatus.RankingStatus RankMatchRankingStatus { get; set; }

    public long RankMatchExpiredTime { get; set; }

    public long RankMatchNextTime { get; set; }

    public string[] RankMatchMatchedEnemies { get; set; }

    public string DigestHash { get; set; }

    public string PrevCheckHash { get; set; }

    public string AlterCheckHash { get; set; }

    public string QuestDigestHash { get; set; }

    public List<SRPG.MapEffectParam> MapEffectParam => this.mMapEffectParam;

    public List<RankingQuestParam> RankingQuestParams => this.mRankingQuestParam;

    public List<RankingQuestRewardParam> RankingQuestRewardParams => this.mRankingQuestRewardParam;

    public List<RankingQuestScheduleParam> RankingQuestScheduleParams
    {
      get => this.mRankingQuestScheduleParam;
    }

    public List<RankingQuestParam> AvailableRankingQuesstParams
    {
      get => this.mAvailableRankingQuesstParams;
    }

    public List<GenesisChapterParam> GenesisChapterParamList => this.mGenesisChapterParam;

    public List<AdvanceEventParam> AdvanceEventParamList => this.mAdvanceEventParam;

    public List<WorldRaidParam> WorldRaidParamList => this.mWorldRaidParamList;

    public List<WorldRaidBossParam> WorldRaidBossParamList => this.mWorldRaidBossParamList;

    public List<WorldRaidRewardParam> WorldRaidRewardParamList => this.mWorldRaidRewardParamList;

    public List<WorldRaidDamageRewardParam> WorldRaidDamageRewardParamList
    {
      get => this.mWorldRaidDamageRewardParamList;
    }

    public List<WorldRaidDamageLotteryParam> WorldRaidDamageLotteryParamList
    {
      get => this.mWorldRaidDamageLotteryParamList;
    }

    public List<WorldRaidRankingRewardParam> WorldRaidRankingRewardParamList
    {
      get => this.mWorldRaidRankingRewardParamList;
    }

    public bool AudienceMode { get; set; }

    public MyPhoton.MyRoom AudienceRoom { get; set; }

    public VersusAudienceManager AudienceManager
    {
      get
      {
        if (this.mAudienceManager == null)
          this.mAudienceManager = new VersusAudienceManager();
        return this.mAudienceManager;
      }
    }

    public List<string> Tips
    {
      get => this.mTips;
      set => this.mTips = value;
    }

    public TipsParam FindTips(string iname)
    {
      TipsParam[] tips = this.MasterParam.Tips;
      if (tips != null)
      {
        for (int index = 0; index < tips.Length; ++index)
        {
          if (tips[index].iname == iname)
            return tips[index];
        }
      }
      return (TipsParam) null;
    }

    public bool UseAppGuardAuthentication { get; set; }

    public string AppGuardUniqueClientID { get; set; }

    public int VS_StreakWinCnt_Now
    {
      get => this.mVersusNowStreakWinCnt;
      set => this.mVersusNowStreakWinCnt = value;
    }

    public int VS_StreakWinCnt_Best
    {
      get => this.mVersusBestStreakWinCnt;
      set => this.mVersusBestStreakWinCnt = value;
    }

    public int VS_StreakWinCnt_NowAllPriod
    {
      get => this.mVersusAllPriodStreakWinCount;
      set => this.mVersusAllPriodStreakWinCount = value;
    }

    public int VS_StreakWinCnt_BestAllPriod
    {
      get => this.mVersusBestAllPriodStreakWinCount;
      set => this.mVersusBestAllPriodStreakWinCount = value;
    }

    public bool IsVSFirstWinRewardRecived
    {
      get => this.mVersusFirstWinRewardRecived;
      set => this.mVersusFirstWinRewardRecived = value;
    }

    public long VSFreeExpiredTime
    {
      get => this.mVersusFreeExpiredTime;
      set => this.mVersusFreeExpiredTime = value;
    }

    public long VSFreeNextTime
    {
      get => this.mVersusFreeNextTime;
      set => this.mVersusFreeNextTime = value;
    }

    public long mVersusEnableId { get; set; }

    public VersusDraftType VSDraftType
    {
      get => this.mVersusDraftType;
      set => this.mVersusDraftType = value;
    }

    public long VSDraftId { get; set; }

    public string VSDraftQuestId { get; set; }

    public bool IsVSCpuBattle
    {
      get => this.mSelectedVersusCpuBattle;
      set => this.mSelectedVersusCpuBattle = value;
    }

    public List<SRPG.VersusCpuData> VersusCpuData => this.mVersusCpuData;

    public VersusEnableTimeParam GetVersusEnableTime(long schedule_id)
    {
      return this.mVersusEnableTime.Find((Predicate<VersusEnableTimeParam>) (VersusET => (long) VersusET.ScheduleId == schedule_id));
    }

    public VersusRankParam GetVersusRankParam(int schedule_id)
    {
      return this.mVersusRank.Find((Predicate<VersusRankParam>) (vr => vr.Id == schedule_id));
    }

    public List<VersusEnableTimeScheduleParam> GetVersusRankMapSchedule(int schedule_id)
    {
      VersusEnableTimeParam versusEnableTimeParam = this.mVersusEnableTime.Find((Predicate<VersusEnableTimeParam>) (VersusET => VersusET.ScheduleId == schedule_id));
      return versusEnableTimeParam == null ? new List<VersusEnableTimeScheduleParam>() : versusEnableTimeParam.Schedule;
    }

    public int GetNextVersusRankClass(int schedule_id, RankMatchClass current_class, int point)
    {
      VersusRankClassParam versusRankClassParam = this.mVersusRankClass.Find((Predicate<VersusRankClassParam>) (vrc => vrc.ScheduleId == schedule_id && vrc.Class == current_class));
      return versusRankClassParam == null ? 0 : versusRankClassParam.UpPoint - point;
    }

    public VersusRankClassParam GetVersusRankClass(int schedule_id, RankMatchClass current_class)
    {
      return this.mVersusRankClass.Find((Predicate<VersusRankClassParam>) (vrc => vrc.ScheduleId == schedule_id && vrc.Class == current_class)) ?? (VersusRankClassParam) null;
    }

    public int GetMaxBattlePoint(int schedule_id)
    {
      VersusRankParam versusRankParam = this.mVersusRank.Find((Predicate<VersusRankParam>) (vr => vr.Id == schedule_id));
      return versusRankParam == null ? 0 : versusRankParam.Limit;
    }

    public List<VersusRankClassParam> GetVersusRankClassList(int schedule_id)
    {
      List<VersusRankClassParam> all = this.mVersusRankClass.FindAll((Predicate<VersusRankClassParam>) (vrc => vrc.ScheduleId == schedule_id));
      all.Sort((Comparison<VersusRankClassParam>) ((a, b) => a.Class - b.Class));
      return all;
    }

    public List<VersusRankRankingRewardParam> GetVersusRankRankingRewardList(int schedule_id)
    {
      List<VersusRankRankingRewardParam> all = this.mVersusRankRankingReward.FindAll((Predicate<VersusRankRankingRewardParam>) (vrrr => vrrr.ScheduleId == schedule_id));
      all.Sort((Comparison<VersusRankRankingRewardParam>) ((a, b) => a.RankBegin - b.RankBegin));
      return all;
    }

    public List<VersusRankReward> GetVersusRankClassRewardList(string reward_id)
    {
      VersusRankRewardParam versusRankRewardParam = this.mVersusRankReward.Find((Predicate<VersusRankRewardParam>) (reward => reward.RewardId == reward_id));
      return versusRankRewardParam == null ? new List<VersusRankReward>() : versusRankRewardParam.RewardList;
    }

    public List<VersusRankMissionParam> GetVersusRankMissionList(int schedule_id)
    {
      List<VersusRankMissionParam> mission_list = new List<VersusRankMissionParam>();
      List<VersusRankMissionScheduleParam> all = this.mVersusRankMissionSchedule.FindAll((Predicate<VersusRankMissionScheduleParam>) (vrms => vrms.ScheduleId == schedule_id));
      if (all == null)
        return mission_list;
      all.ForEach((Action<VersusRankMissionScheduleParam>) (schedule =>
      {
        VersusRankMissionParam rankMissionParam = this.mVersusRankMission.Find((Predicate<VersusRankMissionParam>) (vrm => vrm.IName == schedule.IName));
        if (rankMissionParam == null)
          return;
        mission_list.Add(rankMissionParam);
      }));
      mission_list.Sort((Comparison<VersusRankMissionParam>) ((a, b) => a.IName.CompareTo(b.IName)));
      return mission_list;
    }

    public List<VersusDraftDeckParam> GetVersusDraftDecks() => this.mVersusDraftDecks;

    public List<VersusDraftDeckParam> GetVersusDraftDecksNow(long schedule_id)
    {
      VersusEnableTimeParam vet = this.GetVersusEnableTime(schedule_id);
      return this.mVersusDraftDecks == null || this.mVersusDraftDecks.Count == 0 ? (List<VersusDraftDeckParam>) null : this.mVersusDraftDecks.FindAll((Predicate<VersusDraftDeckParam>) (deck => deck.Id == vet.DraftId || vet.FriendDraftIdList != null && vet.FriendDraftIdList.Count != 0 && vet.FriendDraftIdList.Contains(deck.Id)));
    }

    public VersusDraftDeckParam GetVersusDraftDeck(long draft_id)
    {
      return this.mVersusDraftDecks.Find((Predicate<VersusDraftDeckParam>) (deck => (long) deck.Id == draft_id));
    }

    public List<VersusDraftUnitParam> GetVersusDraftUnits(long draft_id)
    {
      return this.mVersusDraftUnit.FindAll((Predicate<VersusDraftUnitParam>) (vdu => vdu.Id == draft_id));
    }

    public ReqRaidBtlEnd.Response RaidBtlEndParam
    {
      set => this.mRaidBtlEndParam = value;
      get => this.mRaidBtlEndParam;
    }

    public ReqGuildRaidBtlEnd.Response RecentGuildRaidBtlEndParam { get; set; }

    public ReqWorldRaidBtlEnd.Response RecentWorldRaidBtlEndParam { get; set; }

    public string BattleVersion
    {
      get => GameManager.sBattleVersion;
      set => GameManager.sBattleVersion = value;
    }

    protected override void Release()
    {
      GlobalEvent.RemoveListener("TOU_AGREE", new GlobalEvent.Delegate(this.OnAgreeTermsOfUse));
      SceneAwakeObserver.ClearListeners();
      CriticalSection.ForceReset();
      ButtonEvent.Reset();
      CharacterDB.UnloadAll();
      AssetManager.UnloadAll();
      AssetDownloader.Reset();
      SkillSequence.UnloadAll();
      HomeWindow.SetRestorePoint(RestorePoints.Home);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<MySound>.GetInstanceDirect(), (UnityEngine.Object) null))
        MonoSingleton<MySound>.Instance.StopBGM();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mNotifyList, (UnityEngine.Object) null))
      {
        GameUtility.DestroyGameObject(this.mNotifyList);
        this.mNotifyList = (GameObject) null;
      }
      this.mScannedTutorialAssets = false;
    }

    public void ResetData() => this.Release();

    public void InitNotifyList(GameObject notifyListTemplate)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) notifyListTemplate, (UnityEngine.Object) null) || !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mNotifyList, (UnityEngine.Object) null))
        return;
      this.mNotifyList = UnityEngine.Object.Instantiate<GameObject>(notifyListTemplate);
    }

    public SectionParam FindWorld(string iname)
    {
      for (int index = this.mWorlds.Count - 1; index >= 0; --index)
      {
        if (this.mWorlds[index].iname == iname)
          return this.mWorlds[index];
      }
      return (SectionParam) null;
    }

    public List<SectionParam> GetWorldAll() => this.mWorlds;

    public ArchiveParam FindArchive(string iname)
    {
      for (int index = this.mArchives.Count - 1; index >= 0; --index)
      {
        if (this.mArchives[index].iname == iname)
          return this.mArchives[index];
      }
      return (ArchiveParam) null;
    }

    public ArchiveParam FindArchiveByArea(string area_iname)
    {
      if (string.IsNullOrEmpty(area_iname))
        return (ArchiveParam) null;
      for (int index = this.mArchives.Count - 1; index >= 0; --index)
      {
        if (this.mArchives[index].area_iname == area_iname || this.mArchives[index].area_iname_multi == area_iname)
          return this.mArchives[index];
      }
      return (ArchiveParam) null;
    }

    public ChapterParam FindArea(string iname)
    {
      for (int index = this.mAreas.Count - 1; index >= 0; --index)
      {
        if (this.mAreas[index].iname == iname)
          return this.mAreas[index];
      }
      return (ChapterParam) null;
    }

    public QuestParam FindQuest(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (QuestParam) null;
      QuestParam quest;
      if (this.mQuestsDict.TryGetValue(iname, out quest))
        return quest;
      TowerFloorParam towerFloor = this.FindTowerFloor(iname);
      if (towerFloor != null)
        return towerFloor.Clone((QuestParam) null, true);
      return this.FindMultiTowerFloorParam(iname)?.Clone((QuestParam) null, true);
    }

    public QuestParam FindQuest(QuestTypes type)
    {
      for (int index = this.mQuests.Count - 1; index >= 0; --index)
      {
        if (this.mQuests[index].type == type)
          return this.mQuests[index];
      }
      return (QuestParam) null;
    }

    public QuestParam GetOpenUnitQuestParam(string unit_id)
    {
      return string.IsNullOrEmpty(unit_id) ? (QuestParam) null : this.mQuests.Find((Predicate<QuestParam>) (open => open.OpenUnit == unit_id));
    }

    public ObjectiveParam FindObjective(string iname)
    {
      for (int index = this.mObjectives.Count - 1; index >= 0; --index)
      {
        if (this.mObjectives[index].iname == iname)
          return this.mObjectives[index];
      }
      return (ObjectiveParam) null;
    }

    public ObjectiveParam FindTowerObjective(string iname)
    {
      for (int index = this.mTowerObjectives.Count - 1; index >= 0; --index)
      {
        if (this.mTowerObjectives[index].iname == iname)
          return this.mTowerObjectives[index];
      }
      return (ObjectiveParam) null;
    }

    public MagnificationParam FindMagnification(string iname)
    {
      for (int index = this.mMagnifications.Count - 1; index >= 0; --index)
      {
        if (this.mMagnifications[index].iname == iname)
          return this.mMagnifications[index];
      }
      return (MagnificationParam) null;
    }

    public QuestCondParam FindQuestCond(string iname)
    {
      if (this.mConditions != null)
      {
        for (int index = this.mConditions.Count - 1; index >= 0; --index)
        {
          if (this.mConditions[index].iname == iname)
            return this.mConditions[index];
        }
      }
      return (QuestCondParam) null;
    }

    public QuestPartyParam FindQuestParty(string iname)
    {
      if (this.mParties != null)
      {
        for (int index = this.mParties.Count - 1; index >= 0; --index)
        {
          if (this.mParties[index].iname == iname)
            return this.mParties[index];
        }
      }
      return (QuestPartyParam) null;
    }

    public QuestCampaignData[] FindQuestCampaigns(string[] inames)
    {
      List<QuestCampaignData> questCampaignDataList = new List<QuestCampaignData>();
      if (this.mCampaignChildren != null && inames != null && inames.Length > 0)
      {
        foreach (QuestCampaignChildParam mCampaignChild in this.mCampaignChildren)
        {
          foreach (string iname in inames)
          {
            if (!(iname != mCampaignChild.iname))
            {
              foreach (QuestCampaignData questCampaignData1 in mCampaignChild.MakeData())
              {
                QuestCampaignData d = questCampaignData1;
                QuestCampaignData questCampaignData2 = questCampaignDataList.Find((Predicate<QuestCampaignData>) (value => value.type == d.type));
                if (questCampaignData2 == null || questCampaignData2.type == QuestCampaignValueTypes.ExpUnit && !(questCampaignData2.unit == d.unit))
                  questCampaignDataList.Add(d);
              }
              break;
            }
          }
        }
      }
      return questCampaignDataList.ToArray();
    }

    public QuestCampaignData[] FindQuestCampaigns(QuestParam questParam)
    {
      List<QuestCampaignData> questCampaignDataList = new List<QuestCampaignData>();
      DateTime serverTime = TimeManager.ServerTime;
      if (this.mCampaignChildren != null)
      {
        for (int index = this.mCampaignChildren.Count - 1; index >= 0; --index)
        {
          QuestCampaignChildParam mCampaignChild = this.mCampaignChildren[index];
          if (mCampaignChild.IsValidQuest(questParam) && mCampaignChild.IsAvailablePeriod(serverTime))
          {
            foreach (QuestCampaignData questCampaignData1 in mCampaignChild.MakeData())
            {
              QuestCampaignData d = questCampaignData1;
              QuestCampaignData questCampaignData2 = questCampaignDataList.Find((Predicate<QuestCampaignData>) (value => value.type == d.type));
              if (questCampaignData2 == null || questCampaignData2.type == QuestCampaignValueTypes.ExpUnit && !(questCampaignData2.unit == d.unit))
                questCampaignDataList.Add(d);
            }
          }
        }
      }
      return questCampaignDataList.ToArray();
    }

    public QuestParam FindBaseQuest(QuestTypes questType, string iname)
    {
      if (questType == QuestTypes.Tower)
      {
        for (int index = this.mTowerBaseQuests.Count - 1; index >= 0; --index)
        {
          if (this.mTowerBaseQuests[index].iname == iname)
            return this.mTowerBaseQuests[index];
        }
      }
      return (QuestParam) null;
    }

    public TowerFloorParam FindTowerFloor(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (TowerFloorParam) null;
      TowerFloorParam towerFloorParam;
      return this.mTowerFloorsDict.TryGetValue(iname, out towerFloorParam) ? towerFloorParam : (TowerFloorParam) null;
    }

    public TowerFloorParam FindFirstTowerFloor(string towerID)
    {
      if (this.mTowerFloors == null)
        return (TowerFloorParam) null;
      return this.FindTowerFloors(towerID)?.Find((Predicate<TowerFloorParam>) (floorParam => string.IsNullOrEmpty(floorParam.cond_quest)));
    }

    public TowerFloorParam FindLastTowerFloor(string towerID)
    {
      if (this.mTowerFloors == null)
        return (TowerFloorParam) null;
      List<TowerFloorParam> towerFloors = this.FindTowerFloors(towerID);
      if (towerFloors == null)
        return (TowerFloorParam) null;
      return towerFloors.Count < 1 ? (TowerFloorParam) null : towerFloors[towerFloors.Count - 1];
    }

    public TowerFloorParam FindNextTowerFloor(string towerID, string currentFloorID)
    {
      if (this.mTowerFloors == null)
        return (TowerFloorParam) null;
      return this.FindTowerFloors(towerID)?.Find((Predicate<TowerFloorParam>) (floorParam => !string.IsNullOrEmpty(floorParam.cond_quest) && floorParam.cond_quest == currentFloorID));
    }

    public List<TowerFloorParam> FindTowerFloors(string towerID)
    {
      return this.mTowerFloors != null ? this.mTowerFloors.FindAll((Predicate<TowerFloorParam>) (floor => floor.tower_id == towerID)) : (List<TowerFloorParam>) null;
    }

    public TowerRewardParam FindTowerReward(string iname)
    {
      if (this.mTowerResuponse.round > (byte) 0)
      {
        if (this.mTowerRoundRewards != null)
        {
          for (int index = this.mTowerRoundRewards.Count - 1; index >= 0; --index)
          {
            if (this.mTowerRoundRewards[index].iname == iname)
              return (TowerRewardParam) this.mTowerRoundRewards[index];
          }
        }
      }
      else if (this.mTowerRewards != null)
      {
        for (int index = this.mTowerRewards.Count - 1; index >= 0; --index)
        {
          if (this.mTowerRewards[index].iname == iname)
            return this.mTowerRewards[index];
        }
      }
      return (TowerRewardParam) null;
    }

    public TowerParam FindTower(string iname)
    {
      if (this.mTowers != null)
      {
        for (int index = this.mTowers.Count - 1; index >= 0; --index)
        {
          if (this.mTowers[index].iname == iname)
            return this.mTowers[index];
        }
      }
      return (TowerParam) null;
    }

    public int GetQuestTypeCount(QuestTypes type)
    {
      int questTypeCount = 0;
      for (int index = 0; index < this.mQuests.Count; ++index)
      {
        if (this.mQuests[index].type == type)
          ++questTypeCount;
      }
      return questTypeCount;
    }

    public List<QuestParam> GetQuestTypeList(QuestTypes type)
    {
      List<QuestParam> questTypeList = new List<QuestParam>();
      for (int index = 0; index < this.mQuests.Count; ++index)
      {
        if (this.mQuests[index].type == type)
          questTypeList.Add(this.mQuests[index]);
      }
      return questTypeList;
    }

    public int CalcTowerScore(bool isNow = true)
    {
      TowerResuponse towerResuponse = this.TowerResuponse;
      int num1 = 0;
      int num2 = !isNow ? towerResuponse.spd_score : towerResuponse.turn_num;
      int num3 = !isNow ? towerResuponse.tec_score : towerResuponse.died_num;
      int num4 = !isNow ? towerResuponse.ret_score : towerResuponse.retire_num;
      int num5 = !isNow ? towerResuponse.rcv_score : towerResuponse.recover_num;
      int num6 = 0;
      TowerScoreParam[] towerScoreParam = this.mMasterParam.FindTowerScoreParam(this.FindTower(towerResuponse.TowerID).score_iname);
      for (int index = 0; index < towerScoreParam.Length; ++index)
      {
        if (num2 <= (int) towerScoreParam[index].TurnCnt && (num6 & 1) == 0)
        {
          num1 += (int) towerScoreParam[index].Score;
          num6 |= 1;
        }
        if (num3 <= (int) towerScoreParam[index].DiedCnt && (num6 & 2) == 0)
        {
          num1 += (int) towerScoreParam[index].Score;
          num6 |= 2;
        }
        if (num4 <= (int) towerScoreParam[index].RetireCnt && (num6 & 4) == 0)
        {
          num1 += (int) towerScoreParam[index].Score;
          num6 |= 4;
        }
        if (num5 <= (int) towerScoreParam[index].RecoverCnt && (num6 & 8) == 0)
        {
          num1 += (int) towerScoreParam[index].Score;
          num6 |= 8;
        }
      }
      for (int index = 0; index < 4; ++index)
      {
        if ((num6 & 1 << index) == 0)
          num1 += (int) towerScoreParam[towerScoreParam.Length - 1].Score;
      }
      return num1 / 4;
    }

    public TOWER_RANK CalcTowerRank(bool isNow = true)
    {
      int num = this.CalcTowerScore(isNow);
      TOWER_RANK towerRank1 = TOWER_RANK.E_MINUS;
      OInt[] towerRank2 = this.mMasterParam.TowerRank;
      for (int index = 0; index < towerRank2.Length; ++index)
      {
        if (num <= (int) towerRank2[index])
        {
          towerRank1 = (TOWER_RANK) index;
          break;
        }
      }
      return towerRank1;
    }

    public string ConvertTowerScoreToRank(TowerParam towerParam, int score, TOWER_SCORE_TYPE type)
    {
      TowerScoreParam[] towerScoreParam = this.mMasterParam.FindTowerScoreParam(towerParam.score_iname);
      string rank = towerScoreParam[towerScoreParam.Length - 1].Rank;
      for (int index = 0; index < towerScoreParam.Length; ++index)
      {
        switch (type)
        {
          case TOWER_SCORE_TYPE.TURN:
            if (score <= (int) towerScoreParam[index].TurnCnt)
            {
              rank = towerScoreParam[index].Rank;
              goto label_12;
            }
            else
              break;
          case TOWER_SCORE_TYPE.DIED:
            if (score <= (int) towerScoreParam[index].DiedCnt)
            {
              rank = towerScoreParam[index].Rank;
              goto label_12;
            }
            else
              break;
          case TOWER_SCORE_TYPE.RETIRE:
            if (score <= (int) towerScoreParam[index].RetireCnt)
            {
              rank = towerScoreParam[index].Rank;
              goto label_12;
            }
            else
              break;
          case TOWER_SCORE_TYPE.RECOVER:
            if (score <= (int) towerScoreParam[index].RecoverCnt)
            {
              rank = towerScoreParam[index].Rank;
              goto label_12;
            }
            else
              break;
        }
      }
label_12:
      return rank;
    }

    public ArenaPlayer FindArenaPlayer(string fuid)
    {
      return this.mArenaPlayers.Find((Predicate<ArenaPlayer>) (p => p.FUID == fuid));
    }

    public ArenaPlayer[] ArenaPlayers => this.mArenaPlayers.ToArray();

    public ArenaPlayer[] GetArenaRanking(ReqBtlColoRanking.RankingTypes type)
    {
      return this.mArenaRanking[(int) type].ToArray();
    }

    public AchievementParam FindAchievement(int id)
    {
      for (int index = this.mAchievement.Count - 1; index >= 0; --index)
      {
        if (this.mAchievement[index].id == id)
          return this.mAchievement[index];
      }
      return (AchievementParam) null;
    }

    public ArenaPlayerHistory[] GetArenaHistory() => this.mArenaHistory.ToArray();

    public MailData FindMail(long mailID)
    {
      if (this.mPlayer == null)
        return (MailData) null;
      return this.mPlayer.CurrentMails == null ? (MailData) null : this.mPlayer.CurrentMails.Find((Predicate<MailData>) (mail => mail.mid == mailID));
    }

    public PlayerData Player => this.mPlayer;

    public OptionData Option => this.mOption;

    public MasterParam MasterParam
    {
      get => this.mMasterParam;
      set => this.mMasterParam = value;
    }

    public SectionParam[] Sections => this.mWorlds.ToArray();

    public ArchiveParam[] Archives => this.mArchives.ToArray();

    public ChapterParam[] Chapters => this.mAreas.ToArray();

    public QuestParam[] Quests => this.mQuests.ToArray();

    public ObjectiveParam[] Objectives => this.mObjectives.ToArray();

    public TrophyParam[] Trophies => this.mMasterParam.Trophies;

    public TrophyObjective[] GetTrophiesOfType(TrophyConditionTypes type)
    {
      return this.mMasterParam.GetTrophiesOfType(type);
    }

    public ConceptCardParam GetConceptCardParam(string iname)
    {
      return this.mMasterParam.GetConceptCardParam(iname);
    }

    public AchievementParam[] Achievement => this.mAchievement.ToArray();

    public GachaParam[] Gachas => this.mGachas.ToArray();

    public bool IsFgGAuthSync() => this.AuthStatus == ReqFgGAuth.eAuthStatus.Synchronized;

    public TowerParam[] Towers => this.mTowers.ToArray();

    public TowerResuponse TowerResuponse => this.mTowerResuponse;

    public List<TowerResuponse.PlayerUnit> TowerResuponsePlayerUnit => this.TowerResuponse.pdeck;

    public List<TowerResuponse.EnemyUnit> TowerEnemyUnit => this.TowerResuponse.edeck;

    public VersusFriendScore[] VersusFriendInfo => this.mVersusFriendScore.ToArray();

    protected override void Initialize()
    {
      this.OnDayChange += new GameManager.DayChangeEvent(this.DayChanged);
      GlobalEvent.AddListener("TOU_AGREE", new GlobalEvent.Delegate(this.OnAgreeTermsOfUse));
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
    }

    public static void HandleAnyLoadMasterDataErrors(
      GameManager.LoadMasterDataResult result,
      bool flowNodeWillDisplayAnyErrorMessages = false)
    {
      if (result.Result == GameManager.ELoadMasterDataResult.SUCCESS)
        return;
      string str;
      GameManager.MasterDataLoadException dataLoadException;
      switch (result.Result)
      {
        case GameManager.ELoadMasterDataResult.ERROR_MASTER_PARAM_DECRYPT:
          str = "MASTER_PARAM_DECRYPT_ERROR";
          dataLoadException = (GameManager.MasterDataLoadException) new GameManager.MasterParamDecryptException();
          break;
        case GameManager.ELoadMasterDataResult.ERROR_QUEST_PARAM_DECRYPT:
          str = "QUEST_PARAM_DECRYPT_ERROR";
          dataLoadException = (GameManager.MasterDataLoadException) new GameManager.QuestParamDecryptException();
          break;
        case GameManager.ELoadMasterDataResult.ERROR_QUEST_DROP_PARAM_DECRYPT:
          str = "QUEST_DROP_PARAM_DECRYPT_ERROR";
          dataLoadException = (GameManager.MasterDataLoadException) new GameManager.QuestDropParamDecryptException();
          break;
        case GameManager.ELoadMasterDataResult.ERROR_MASTER_PARAM_DESERIALIZE:
          str = "MASTER_PARAM_DESERIALIZE_ERROR";
          dataLoadException = (GameManager.MasterDataLoadException) new GameManager.MasterParamDeserializeException();
          break;
        case GameManager.ELoadMasterDataResult.ERROR_QUEST_PARAM_DESERIALIZE:
          str = "QUEST_PARAM_DESERIALIZE_ERROR";
          dataLoadException = (GameManager.MasterDataLoadException) new GameManager.QuestParamDeserializeException();
          break;
        default:
          str = "MASTER_DATA_OTHER_ERROR";
          dataLoadException = (GameManager.MasterDataLoadException) new GameManager.MasterDataGenericException();
          break;
      }
      string msg = LocalizedText.Get("embed." + str);
      if (!flowNodeWillDisplayAnyErrorMessages)
        EmbedSystemMessage.Create(msg, (EmbedSystemMessage.SystemMessageEvent) (go => FlowNode_LoadScene.LoadBootScene()), true);
      else
        MonoSingleton<GameManager>.Instance.MasterDataLoadErrorMessage = msg;
      dataLoadException.Type = result.Result;
      dataLoadException.ActualException = result.Exception;
      Debug.LogError((object) dataLoadException);
      result.LogData.AddCommon(true, true, true, true);
      if (result.Result == GameManager.ELoadMasterDataResult.ERROR_MASTER_PARAM_DECRYPT || result.Result == GameManager.ELoadMasterDataResult.ERROR_QUEST_PARAM_DECRYPT || result.Result == GameManager.ELoadMasterDataResult.ERROR_QUEST_DROP_PARAM_DECRYPT)
      {
        FlowNode_SendLogMessage.SendLogMessage(result.LogData, "EncryptionErrors");
      }
      else
      {
        if (result.Result != GameManager.ELoadMasterDataResult.ERROR_MASTER_PARAM_DESERIALIZE && result.Result != GameManager.ELoadMasterDataResult.ERROR_QUEST_PARAM_DESERIALIZE)
          return;
        FlowNode_SendLogMessage.SendLogMessage(result.LogData, "MessagePackErrors");
      }
    }

    public GameManager.LoadMasterDataResult ReloadMasterData(
      GameManager.MasterDataInBinary masterDataSerialized)
    {
      return this.ReloadMasterData((string) null, (string) null, masterDataSerialized);
    }

    public GameManager.LoadMasterDataResult ReloadMasterData(
      string masterParam = null,
      string questParam = null,
      GameManager.MasterDataInBinary masterDataSerialized = null)
    {
      GameManager.LoadMasterDataResult masterDataResult = new GameManager.LoadMasterDataResult();
      masterDataResult.Result = GameManager.ELoadMasterDataResult.NOT_YET_MATE;
      if (this.IsRelogin)
      {
        masterDataResult.Result = GameManager.ELoadMasterDataResult.SUCCESS;
        return masterDataResult;
      }
      if (masterDataSerialized != null)
      {
        if (masterDataSerialized.LoadResult.Result != GameManager.ELoadMasterDataResult.SUCCESS)
          return masterDataSerialized.LoadResult;
      }
      try
      {
        string msg = (string) null;
        if (masterDataSerialized == null)
        {
          if (masterParam == null)
            masterParam = AssetManager.LoadTextData("Data/MasterParam");
          if (!string.IsNullOrEmpty(this.DigestHash))
          {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(System.Text.Encoding.UTF8.GetBytes(masterParam));
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < hash.Length; ++index)
              stringBuilder.AppendFormat("{0:x2}", (object) hash[index]);
            string str = stringBuilder.ToString();
            if ((string.IsNullOrEmpty(this.PrevCheckHash) || !string.IsNullOrEmpty(this.PrevCheckHash) && this.PrevCheckHash != str) && str != this.DigestHash)
              this.AlterCheckHash = str;
          }
          this.Deserialize2(JsonUtility.FromJson<JSON_MasterParam>(masterParam) ?? throw new InvalidJSONException());
          if (questParam == null)
            questParam = AssetManager.LoadTextData("Data/QuestParam");
          this.Deserialize(JsonUtility.FromJson<Json_QuestList>(questParam) ?? throw new InvalidJSONException());
        }
        else
        {
          if (masterDataSerialized.MasterBytes == null || masterDataSerialized.MasterBytes.Length == 0)
            throw new InvalidOperationException("Cannot load MessagePack-serialized MasterParam!");
          if (!string.IsNullOrEmpty(this.DigestHash))
          {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(masterDataSerialized.MasterBytes);
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < hash.Length; ++index)
              stringBuilder.AppendFormat("{0:x2}", (object) hash[index]);
            msg = stringBuilder.ToString();
            if ((string.IsNullOrEmpty(this.PrevCheckHash) || !string.IsNullOrEmpty(this.PrevCheckHash) && this.PrevCheckHash != msg) && msg != this.DigestHash)
              this.AlterCheckHash = msg;
          }
          JSON_MasterParam jsonMasterParam = (JSON_MasterParam) null;
          JSON_MasterParam json1;
          try
          {
            json1 = SerializerCompressorHelper.Decode<JSON_MasterParam>(masterDataSerialized.MasterBytes, true, CompressMode.None, printExceptions: false);
          }
          catch (Exception ex)
          {
            FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
            sendLogGenerator.Add(GameManager.ELoadMasterDataResult.ERROR_MASTER_PARAM_DESERIALIZE.ToString(), ex.Message);
            sendLogGenerator.Add("StackTrace", ex.StackTrace);
            sendLogGenerator.Add("NetworkDigest", this.DigestHash);
            sendLogGenerator.Add("LoadedDigest", msg);
            sendLogGenerator.Add("HashMatch", !(this.DigestHash == msg) ? "false" : "true");
            sendLogGenerator.Add("DataLength", masterDataSerialized.MasterBytes.Length.ToString() + string.Empty);
            masterDataResult.Exception = ex;
            masterDataResult.LogData = sendLogGenerator;
            masterDataResult.Result = GameManager.ELoadMasterDataResult.ERROR_MASTER_PARAM_DESERIALIZE;
            return masterDataResult;
          }
          if (json1 == null)
            throw new InvalidJSONException();
          this.Deserialize2(json1);
          jsonMasterParam = (JSON_MasterParam) null;
          if (masterDataSerialized.QuestBytes == null || masterDataSerialized.QuestBytes.Length == 0)
            throw new InvalidOperationException("Cannot load MessagePack-serialized QuestParam!");
          Json_QuestList jsonQuestList = (Json_QuestList) null;
          Json_QuestList json2;
          try
          {
            json2 = SerializerCompressorHelper.Decode<Json_QuestList>(masterDataSerialized.QuestBytes, true, CompressMode.None, printExceptions: false);
          }
          catch (Exception ex)
          {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(masterDataSerialized.QuestBytes);
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < hash.Length; ++index)
              stringBuilder.AppendFormat("{0:x2}", (object) hash[index]);
            string str = stringBuilder.ToString();
            FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
            sendLogGenerator.Add(GameManager.ELoadMasterDataResult.ERROR_QUEST_PARAM_DESERIALIZE.ToString(), ex.Message);
            sendLogGenerator.Add("StackTrace", ex.StackTrace);
            sendLogGenerator.Add("NetworkDigest", this.QuestDigestHash);
            sendLogGenerator.Add("LoadedDigest", msg);
            sendLogGenerator.Add("HashMatch", !(this.QuestDigestHash == str) ? "false" : "true");
            sendLogGenerator.Add("DataLength", masterDataSerialized.QuestBytes.Length.ToString() + string.Empty);
            masterDataResult.Exception = ex;
            masterDataResult.LogData = sendLogGenerator;
            masterDataResult.Result = GameManager.ELoadMasterDataResult.ERROR_QUEST_PARAM_DESERIALIZE;
            return masterDataResult;
          }
          if (json2 == null)
            throw new InvalidJSONException();
          this.Deserialize(json2);
          jsonQuestList = (Json_QuestList) null;
        }
      }
      catch (Exception ex)
      {
        this.mReloadMasterDataError = ex.ToString();
        FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
        sendLogGenerator.Add("LoadMasterDataException", ex.Message);
        sendLogGenerator.Add("StackTrace", ex.StackTrace);
        masterDataResult.Exception = ex;
        masterDataResult.LogData = sendLogGenerator;
        masterDataResult.Result = GameManager.ELoadMasterDataResult.ERROR_OTHER;
        return masterDataResult;
      }
      masterDataResult.Result = GameManager.ELoadMasterDataResult.SUCCESS;
      return masterDataResult;
    }

    private void Start()
    {
      this.UpdateResolution();
      LogMonitor.Start();
      DebugMenu.Start();
      ExceptionMonitor.Start();
    }

    public bool Deserialize(JSON_MasterParam json)
    {
      bool flag = true & this.mMasterParam.Deserialize(json);
      if (flag)
        this.mMasterParam.CacheReferences();
      return flag;
    }

    public bool Deserialize2(JSON_MasterParam json)
    {
      bool flag = true & this.mMasterParam.Deserialize2(json);
      this.mMasterParam.DumpLoadedLog();
      if (flag)
        this.mMasterParam.CacheReferences();
      return flag;
    }

    public void Deserialize(Json_PlayerData json)
    {
      int num = 0;
      if (this.Player != null)
        num = this.Player.Lv;
      this.mPlayer.Deserialize(json);
      if (num == 0 || num == this.Player.Lv)
        return;
      this.OnPlayerLvChange();
    }

    public void Deserialize(Json_TrophyPlayerData json)
    {
      int num = 0;
      if (this.Player != null)
        num = this.Player.Lv;
      this.mPlayer.Deserialize(json);
      if (num == 0 || num == this.Player.Lv)
        return;
      this.OnPlayerLvChange();
    }

    public void Deserialize(Json_Unit[] json) => this.mPlayer.Deserialize(json);

    public void Deserialize(Json_Item[] json) => this.mPlayer.Deserialize(json);

    public void Deserialize(Json_Artifact[] json, bool differenceUpdate = false)
    {
      this.mPlayer.Deserialize(json, differenceUpdate);
    }

    public void Deserialize(JSON_ConceptCard[] json, bool is_data_override = true)
    {
      this.mPlayer.Deserialize(json, is_data_override);
    }

    public void Deserialize(JSON_ConceptCardMaterial[] json, bool is_data_override = true)
    {
      this.mPlayer.Deserialize(json, is_data_override);
    }

    public void Deserialize(string[] skin_card_inames)
    {
      this.mPlayer.Deserialize(skin_card_inames);
    }

    public void Deserialize(Json_Skin[] json) => this.mPlayer.Deserialize(json);

    public void Deserialize(Json_Party[] json) => this.mPlayer.Deserialize(json);

    public bool Deserialize(Json_Mail[] json) => this.mPlayer.Deserialize(json);

    public void Deserialize(Json_Friend[] json) => this.mPlayer.Deserialize(json);

    public void Deserialize(Json_Friend[] json, FriendStates state)
    {
      this.mPlayer.Deserialize(json, state);
    }

    public void Deserialize(Json_Support[] json) => this.mPlayer.Deserialize(json);

    public void Deserialize(Json_MultiFuids[] json) => this.mPlayer.Deserialize(json);

    public void Deserialize(FriendPresentWishList.Json[] json) => this.mPlayer.Deserialize(json);

    public void Deserialize(FriendPresentReceiveList.Json[] json) => this.mPlayer.Deserialize(json);

    public void Deserialize(JSON_PlayerGuild json) => this.mPlayer.Deserialize(json);

    public void Deserialize(JSON_Guild json) => this.mPlayer.Deserialize(json);

    public void Deserialize(Json_Notify json)
    {
      if (json == null)
        return;
      this.mPlayer.Deserialize(json);
    }

    public void Deserialize(Json_Notify_Monthly json)
    {
      if (json == null)
        return;
      this.mPlayer.Deserialize(json);
    }

    public void Deserialize(Json_Versus json)
    {
      if (json == null)
        return;
      this.mPlayer.Deserialize(json);
    }

    public void Deserialize(ReqGetRune.Response json, bool is_data_override = true)
    {
      if (json == null)
        return;
      this.mPlayer.Deserialize(json, is_data_override);
    }

    public void Deserialize(Json_RuneEnforceGaugeData[] json)
    {
      if (json == null)
        return;
      this.mPlayer.Deserialize(json);
    }

    public void Deserialize(Json_RuneData[] json, bool is_data_override = true)
    {
      if (json == null)
        return;
      this.mPlayer.Deserialize(json, is_data_override);
    }

    public void Deserialize(ReqRuneStorageAdd.Response json)
    {
      if (json == null)
        return;
      this.mPlayer.Deserialize(json);
    }

    public void Deserialize(ReqRuneFavorite.Response json, bool is_data_override = true)
    {
      if (json == null)
        return;
      this.mPlayer.Deserialize(json);
    }

    public void Deserialize(Json_QuestList json)
    {
      this.mWorlds.Clear();
      this.mArchives.Clear();
      this.mAreas.Clear();
      this.mObjectives.Clear();
      this.mTowerObjectives.Clear();
      this.mMagnifications.Clear();
      this.mParties.Clear();
      this.mConditions.Clear();
      this.mQuests.Clear();
      this.mQuestsDict.Clear();
      this.mMagnifications.Clear();
      this.mConditions.Clear();
      this.mCampaignParents.Clear();
      this.mCampaignChildren.Clear();
      this.mCampaignTrust.Clear();
      this.mTowerRewards.Clear();
      this.mTowerBaseQuests.Clear();
      this.mVersusTowerFloor.Clear();
      this.mTowerFloors.Clear();
      this.mTowerFloorsDict.Clear();
      this.mVersusScheduleParam.Clear();
      this.mVersusCoinParam.Clear();
      this.mMultiTowerFloor.Clear();
      this.mMultiTowerRewards.Clear();
      this.mRankingQuestParam.Clear();
      this.mRankingQuestRewardParam.Clear();
      this.mRankingQuestScheduleParam.Clear();
      this.mAvailableRankingQuesstParams.Clear();
      this.mVersusFirstWinBonus.Clear();
      this.mVersusStreakWinBonus.Clear();
      this.mVersusRule.Clear();
      this.mVersusCoinCamp.Clear();
      this.mVersusRank.Clear();
      this.mVersusRankClass.Clear();
      this.mVersusRankRankingReward.Clear();
      this.mVersusRankReward.Clear();
      this.mVersusRankMissionSchedule.Clear();
      this.mVersusRankMission.Clear();
      this.mVersusDraftDecks.Clear();
      this.mVersusDraftUnit.Clear();
      this.mQuestLobbyNews.Clear();
      DebugUtility.Verify((object) json, typeof (Json_QuestList));
      if (json.worlds != null)
      {
        for (int index = 0; index < json.worlds.Length; ++index)
        {
          SectionParam sectionParam = new SectionParam();
          sectionParam.Deserialize(json.worlds[index]);
          this.mWorlds.Add(sectionParam);
        }
      }
      if (json.archives != null)
      {
        for (int index = 0; index < json.archives.Length; ++index)
        {
          if (!string.IsNullOrEmpty(json.archives[index].area_iname) || !string.IsNullOrEmpty(json.archives[index].area_iname_multi))
          {
            ArchiveParam archiveParam = new ArchiveParam();
            archiveParam.Deserialize(json.archives[index]);
            this.mArchives.Add(archiveParam);
          }
        }
      }
      if (json.areas != null)
      {
        for (int index = 0; index < json.areas.Length; ++index)
        {
          ChapterParam chapterParam = new ChapterParam();
          chapterParam.Deserialize(json.areas[index]);
          chapterParam.IsArchiveQuest = this.FindArchiveByArea(chapterParam.iname) != null;
          this.mAreas.Add(chapterParam);
          if (!string.IsNullOrEmpty(chapterParam.section))
            chapterParam.sectionParam = this.FindWorld(chapterParam.section);
        }
        for (int index = 0; index < json.areas.Length; ++index)
        {
          if (!string.IsNullOrEmpty(json.areas[index].parent))
          {
            ChapterParam area = this.FindArea(json.areas[index].iname);
            if (area != null)
            {
              area.parent = this.FindArea(json.areas[index].parent);
              if (area.parent != null)
                area.parent.children.Add(area);
            }
          }
        }
      }
      if (json.objectives != null)
      {
        for (int index = 0; index < json.objectives.Length; ++index)
        {
          ObjectiveParam objectiveParam = new ObjectiveParam();
          objectiveParam.Deserialize(json.objectives[index]);
          this.mObjectives.Add(objectiveParam);
        }
      }
      if (json.towerObjectives != null)
      {
        for (int index = 0; index < json.towerObjectives.Length; ++index)
        {
          ObjectiveParam objectiveParam = new ObjectiveParam();
          objectiveParam.Deserialize(json.towerObjectives[index]);
          this.mTowerObjectives.Add(objectiveParam);
        }
      }
      if (json.magnifications != null)
      {
        for (int index = 0; index < json.magnifications.Length; ++index)
        {
          MagnificationParam magnificationParam = new MagnificationParam();
          magnificationParam.Deserialize(json.magnifications[index]);
          this.mMagnifications.Add(magnificationParam);
        }
      }
      if (json.conditions != null)
      {
        for (int index = 0; index < json.conditions.Length; ++index)
        {
          QuestCondParam questCondParam = new QuestCondParam();
          questCondParam.Deserialize(json.conditions[index]);
          this.mConditions.Add(questCondParam);
        }
      }
      if (json.parties != null)
      {
        for (int index = 0; index < json.parties.Length; ++index)
        {
          QuestPartyParam questPartyParam = new QuestPartyParam();
          questPartyParam.Deserialize(json.parties[index]);
          this.mParties.Add(questPartyParam);
        }
      }
      for (int index = 0; index < json.quests.Length; ++index)
      {
        QuestParam questParam = new QuestParam();
        questParam.Deserialize(json.quests[index]);
        this.mQuests.Add(questParam);
        this.mQuestsDict.Add(questParam.iname, questParam);
        if (!string.IsNullOrEmpty(questParam.ChapterID))
        {
          questParam.Chapter = this.FindArea(questParam.ChapterID);
          if (questParam.Chapter != null)
            questParam.Chapter.quests.Add(questParam);
        }
        if (questParam.type == QuestTypes.Tower)
          this.mTowerBaseQuests.Add(questParam);
      }
      if (this.mPlayer.UnitNum >= 1)
      {
        for (int index = 0; index < this.mPlayer.Units.Count; ++index)
          this.mPlayer.Units[index].ResetCharacterQuestParams();
      }
      if (json.CampaignParents != null)
      {
        for (int index = 0; index < json.CampaignParents.Length; ++index)
        {
          QuestCampaignParentParam campaignParentParam = new QuestCampaignParentParam();
          if (campaignParentParam.Deserialize(json.CampaignParents[index]))
            this.mCampaignParents.Add(campaignParentParam);
        }
      }
      if (json.CampaignChildren != null)
      {
        for (int index = 0; index < json.CampaignChildren.Length; ++index)
        {
          QuestCampaignChildParam campaignChildParam = new QuestCampaignChildParam();
          if (campaignChildParam.Deserialize(json.CampaignChildren[index]))
          {
            List<QuestCampaignParentParam> campaignParentParamList = new List<QuestCampaignParentParam>();
            foreach (QuestCampaignParentParam mCampaignParent in this.mCampaignParents)
            {
              if (mCampaignParent.IsChild(campaignChildParam.iname))
                campaignParentParamList.Add(mCampaignParent);
            }
            campaignChildParam.parents = campaignParentParamList.ToArray();
            this.mCampaignChildren.Add(campaignChildParam);
          }
        }
      }
      if (json.CampaignTrust != null)
      {
        for (int index = 0; index < json.CampaignTrust.Length; ++index)
        {
          QuestCampaignTrust param = new QuestCampaignTrust();
          if (param.Deserialize(json.CampaignTrust[index]))
          {
            this.mCampaignTrust.Add(param);
            QuestCampaignChildParam campaignChildParam = this.mCampaignChildren.Find((Predicate<QuestCampaignChildParam>) (value => value.iname == param.iname));
            if (campaignChildParam != null)
              campaignChildParam.campaignTrust = param;
          }
        }
      }
      if (json.CampaignInspSkill != null)
      {
        for (int index = 0; index < json.CampaignInspSkill.Length; ++index)
        {
          QuestCampaignInspSkill param = new QuestCampaignInspSkill();
          if (param.Deserialize(json.CampaignInspSkill[index]))
          {
            this.mCampaignInspSkill.Add(param);
            QuestCampaignChildParam campaignChildParam = this.mCampaignChildren.Find((Predicate<QuestCampaignChildParam>) (value => value.iname == param.Iname));
            if (campaignChildParam != null)
              campaignChildParam.campaignInspSkill = param;
          }
        }
      }
      if (json.towerRewards != null)
      {
        for (int index = 0; index < json.towerRewards.Length; ++index)
        {
          TowerRewardParam towerRewardParam = new TowerRewardParam();
          towerRewardParam.Deserialize(json.towerRewards[index]);
          this.mTowerRewards.Add(towerRewardParam);
        }
      }
      if (json.towerRoundRewards != null)
      {
        for (int index = 0; index < json.towerRoundRewards.Length; ++index)
        {
          TowerRoundRewardParam roundRewardParam = new TowerRoundRewardParam();
          roundRewardParam.Deserialize(json.towerRoundRewards[index]);
          this.mTowerRoundRewards.Add(roundRewardParam);
        }
      }
      if (json.towerFloors != null)
      {
        for (int index = 0; index < json.towerFloors.Length; ++index)
        {
          TowerFloorParam towerFloorParam = new TowerFloorParam();
          towerFloorParam.Deserialize(json.towerFloors[index]);
          this.mTowerFloors.Add(towerFloorParam);
          this.mTowerFloorsDict.Add(towerFloorParam.iname, towerFloorParam);
        }
      }
      if (json.towers != null)
      {
        for (int index = 0; index < json.towers.Length; ++index)
        {
          TowerParam towerParam = new TowerParam();
          towerParam.Deserialize(json.towers[index]);
          this.mTowers.Add(towerParam);
        }
      }
      if (json.versusTowerFloor != null)
      {
        this.mVersusTowerFloor = new List<VersusTowerParam>(json.versusTowerFloor.Length);
        for (int index = 0; index < json.versusTowerFloor.Length; ++index)
        {
          VersusTowerParam versusTowerParam = new VersusTowerParam();
          versusTowerParam.Deserialize(json.versusTowerFloor[index]);
          this.mVersusTowerFloor.Add(versusTowerParam);
        }
      }
      if (json.versusschedule != null)
      {
        this.mVersusScheduleParam = new List<VersusScheduleParam>(json.versusschedule.Length);
        for (int index = 0; index < json.versusschedule.Length; ++index)
        {
          VersusScheduleParam versusScheduleParam = new VersusScheduleParam();
          versusScheduleParam.Deserialize(json.versusschedule[index]);
          this.mVersusScheduleParam.Add(versusScheduleParam);
        }
      }
      if (json.versuscoin != null)
      {
        this.mVersusCoinParam = new List<VersusCoinParam>(json.versuscoin.Length);
        for (int index = 0; index < json.versuscoin.Length; ++index)
        {
          VersusCoinParam versusCoinParam = new VersusCoinParam();
          versusCoinParam.Deserialize(json.versuscoin[index]);
          this.mVersusCoinParam.Add(versusCoinParam);
        }
      }
      if (json.multitowerFloor != null)
      {
        this.mMultiTowerFloor = new List<MultiTowerFloorParam>(json.multitowerFloor.Length);
        for (int index = 0; index < json.multitowerFloor.Length; ++index)
        {
          MultiTowerFloorParam multiTowerFloorParam = new MultiTowerFloorParam();
          multiTowerFloorParam.Deserialize(json.multitowerFloor[index]);
          this.mMultiTowerFloor.Add(multiTowerFloorParam);
        }
      }
      if (json.multitowerRewards != null)
      {
        for (int index = 0; index < json.multitowerRewards.Length; ++index)
        {
          MultiTowerRewardParam towerRewardParam = new MultiTowerRewardParam();
          towerRewardParam.Deserialize(json.multitowerRewards[index]);
          this.mMultiTowerRewards.Add(towerRewardParam);
        }
      }
      if (json.MapEffect != null)
      {
        List<SRPG.MapEffectParam> mapEffectParamList = new List<SRPG.MapEffectParam>(json.MapEffect.Length);
        for (int index = 0; index < json.MapEffect.Length; ++index)
        {
          SRPG.MapEffectParam mapEffectParam = new SRPG.MapEffectParam();
          mapEffectParam.Deserialize(json.MapEffect[index]);
          mapEffectParamList.Add(mapEffectParam);
        }
        this.mMapEffectParam = mapEffectParamList;
        this.mMasterParam.MakeMapEffectHaveJobLists();
      }
      if (json.WeatherSet != null)
      {
        List<WeatherSetParam> weatherSetParamList = new List<WeatherSetParam>(json.WeatherSet.Length);
        for (int index = 0; index < json.WeatherSet.Length; ++index)
        {
          WeatherSetParam weatherSetParam = new WeatherSetParam();
          weatherSetParam.Deserialize(json.WeatherSet[index]);
          weatherSetParamList.Add(weatherSetParam);
        }
        this.mWeatherSetParam = weatherSetParamList;
      }
      if (json.rankingQuestSchedule != null)
      {
        this.mRankingQuestScheduleParam = new List<RankingQuestScheduleParam>(json.rankingQuestSchedule.Length);
        for (int index = 0; index < json.rankingQuestSchedule.Length; ++index)
        {
          RankingQuestScheduleParam questScheduleParam = new RankingQuestScheduleParam();
          questScheduleParam.Deserialize(json.rankingQuestSchedule[index]);
          this.mRankingQuestScheduleParam.Add(questScheduleParam);
        }
      }
      if (json.rankingQuestRewards != null)
      {
        this.mRankingQuestRewardParam = new List<RankingQuestRewardParam>(json.rankingQuestRewards.Length);
        for (int index = 0; index < json.rankingQuestRewards.Length; ++index)
        {
          RankingQuestRewardParam questRewardParam = new RankingQuestRewardParam();
          questRewardParam.Deserialize(json.rankingQuestRewards[index]);
          this.mRankingQuestRewardParam.Add(questRewardParam);
        }
      }
      if (json.rankingQuests != null)
      {
        this.mRankingQuestParam = new List<RankingQuestParam>(json.rankingQuests.Length);
        for (int index = 0; index < json.rankingQuests.Length; ++index)
        {
          RankingQuestParam rankingQuestParam = new RankingQuestParam();
          rankingQuestParam.Deserialize(json.rankingQuests[index]);
          rankingQuestParam.rewardParam = RankingQuestRewardParam.FindByID(rankingQuestParam.reward_id);
          rankingQuestParam.scheduleParam = RankingQuestScheduleParam.FindByID(rankingQuestParam.schedule_id);
          this.mRankingQuestParam.Add(rankingQuestParam);
        }
      }
      if (json.versusfirstwinbonus != null)
      {
        int length = json.versusfirstwinbonus.Length;
        this.mVersusFirstWinBonus = new List<VersusFirstWinBonusParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusFirstWinBonusParam firstWinBonusParam = new VersusFirstWinBonusParam();
          if (firstWinBonusParam != null && firstWinBonusParam.Deserialize(json.versusfirstwinbonus[index]))
            this.mVersusFirstWinBonus.Add(firstWinBonusParam);
        }
      }
      if (json.versusstreakwinschedule != null)
      {
        int length = json.versusstreakwinschedule.Length;
        this.mVersusStreakSchedule = new List<VersusStreakWinScheduleParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusStreakWinScheduleParam winScheduleParam = new VersusStreakWinScheduleParam();
          if (winScheduleParam != null && winScheduleParam.Deserialize(json.versusstreakwinschedule[index]))
            this.mVersusStreakSchedule.Add(winScheduleParam);
        }
      }
      if (json.versusstreakwinbonus != null)
      {
        int length = json.versusstreakwinbonus.Length;
        this.mVersusStreakWinBonus = new List<VersusStreakWinBonusParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusStreakWinBonusParam streakWinBonusParam = new VersusStreakWinBonusParam();
          if (streakWinBonusParam != null && streakWinBonusParam.Deserialize(json.versusstreakwinbonus[index]))
            this.mVersusStreakWinBonus.Add(streakWinBonusParam);
        }
      }
      if (json.versusrule != null)
      {
        int length = json.versusrule.Length;
        this.mVersusRule = new List<VersusRuleParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusRuleParam versusRuleParam = new VersusRuleParam();
          if (versusRuleParam != null && versusRuleParam.Deserialize(json.versusrule[index]))
            this.mVersusRule.Add(versusRuleParam);
        }
      }
      if (json.versuscoincamp != null)
      {
        int length = json.versuscoincamp.Length;
        this.mVersusCoinCamp = new List<VersusCoinCampParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusCoinCampParam versusCoinCampParam = new VersusCoinCampParam();
          if (versusCoinCampParam != null && versusCoinCampParam.Deserialize(json.versuscoincamp[index]))
            this.mVersusCoinCamp.Add(versusCoinCampParam);
        }
      }
      if (json.versusenabletime != null)
      {
        int length = json.versusenabletime.Length;
        this.mVersusEnableTime = new List<VersusEnableTimeParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusEnableTimeParam versusEnableTimeParam = new VersusEnableTimeParam();
          if (versusEnableTimeParam.Deserialize(json.versusenabletime[index]))
            this.mVersusEnableTime.Add(versusEnableTimeParam);
        }
      }
      if (json.VersusRank != null)
      {
        int length = json.VersusRank.Length;
        this.mVersusRank = new List<VersusRankParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusRankParam versusRankParam = new VersusRankParam();
          if (versusRankParam.Deserialize(json.VersusRank[index]))
            this.mVersusRank.Add(versusRankParam);
        }
      }
      if (json.VersusRankClass != null)
      {
        int length = json.VersusRankClass.Length;
        this.mVersusRankClass = new List<VersusRankClassParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusRankClassParam versusRankClassParam = new VersusRankClassParam();
          if (versusRankClassParam.Deserialize(json.VersusRankClass[index]))
            this.mVersusRankClass.Add(versusRankClassParam);
        }
      }
      if (json.VersusRankRankingReward != null)
      {
        int length = json.VersusRankRankingReward.Length;
        this.mVersusRankRankingReward = new List<VersusRankRankingRewardParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusRankRankingRewardParam rankingRewardParam = new VersusRankRankingRewardParam();
          if (rankingRewardParam.Deserialize(json.VersusRankRankingReward[index]))
            this.mVersusRankRankingReward.Add(rankingRewardParam);
        }
      }
      if (json.VersusRankReward != null)
      {
        int length = json.VersusRankReward.Length;
        this.mVersusRankReward = new List<VersusRankRewardParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusRankRewardParam versusRankRewardParam = new VersusRankRewardParam();
          if (versusRankRewardParam.Deserialize(json.VersusRankReward[index]))
            this.mVersusRankReward.Add(versusRankRewardParam);
        }
      }
      if (json.VersusRankMissionSchedule != null)
      {
        int length = json.VersusRankMissionSchedule.Length;
        this.mVersusRankMissionSchedule = new List<VersusRankMissionScheduleParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusRankMissionScheduleParam missionScheduleParam = new VersusRankMissionScheduleParam();
          if (missionScheduleParam.Deserialize(json.VersusRankMissionSchedule[index]))
            this.mVersusRankMissionSchedule.Add(missionScheduleParam);
        }
      }
      if (json.VersusRankMission != null)
      {
        int length = json.VersusRankMission.Length;
        this.mVersusRankMission = new List<VersusRankMissionParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusRankMissionParam rankMissionParam = new VersusRankMissionParam();
          if (rankMissionParam.Deserialize(json.VersusRankMission[index]))
            this.mVersusRankMission.Add(rankMissionParam);
        }
      }
      if (json.GuerrillaShopAdventQuest != null)
      {
        int length = json.GuerrillaShopAdventQuest.Length;
        this.mGuerrillaShopAdventQuest = new List<GuerrillaShopAdventQuestParam>(length);
        for (int index = 0; index < length; ++index)
        {
          GuerrillaShopAdventQuestParam adventQuestParam = new GuerrillaShopAdventQuestParam();
          if (adventQuestParam.Deserialize(json.GuerrillaShopAdventQuest[index]))
            this.mGuerrillaShopAdventQuest.Add(adventQuestParam);
        }
      }
      if (json.GuerrillaShopSchedule != null)
      {
        int length = json.GuerrillaShopSchedule.Length;
        this.mGuerrillaShopScheduleParam = new List<GuerrillaShopScheduleParam>(length);
        for (int index = 0; index < length; ++index)
        {
          GuerrillaShopScheduleParam shopScheduleParam = new GuerrillaShopScheduleParam();
          if (shopScheduleParam.Deserialize(json.GuerrillaShopSchedule[index]))
            this.mGuerrillaShopScheduleParam.Add(shopScheduleParam);
        }
      }
      if (json.VersusDraftDeck != null)
      {
        int length = json.VersusDraftDeck.Length;
        this.mVersusDraftDecks = new List<VersusDraftDeckParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusDraftDeckParam versusDraftDeckParam = new VersusDraftDeckParam();
          if (versusDraftDeckParam.Deserialize(json.VersusDraftDeck[index]))
            this.mVersusDraftDecks.Add(versusDraftDeckParam);
        }
      }
      if (json.VersusDraftUnit != null)
      {
        int length = json.VersusDraftUnit.Length;
        this.mVersusDraftUnit = new List<VersusDraftUnitParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusDraftUnitParam versusDraftUnitParam = new VersusDraftUnitParam();
          if (versusDraftUnitParam.Deserialize((long) index + 1L, json.VersusDraftUnit[index]))
            this.mVersusDraftUnit.Add(versusDraftUnitParam);
        }
      }
      if (json.questLobbyNews != null)
      {
        int length = json.questLobbyNews.Length;
        this.mQuestLobbyNews = new List<QuestLobbyNews>(length);
        for (int index = 0; index < length; ++index)
        {
          QuestLobbyNews questLobbyNews = new QuestLobbyNews();
          if (questLobbyNews.Deserialize(json.questLobbyNews[index]))
            this.mQuestLobbyNews.Add(questLobbyNews);
        }
      }
      GenesisStarParam.Deserialize(ref this.mGenesisStarParam, json.GenesisStar);
      GenesisRewardParam.Deserialize(ref this.mGenesisRewardParam, json.GenesisReward);
      GenesisLapBossParam.Deserialize(ref this.mGenesisLapBossParam, json.GenesisLapBoss);
      GenesisChapterParam.Deserialize(ref this.mGenesisChapterParam, json.GenesisChapter);
      AdvanceStarParam.Deserialize(ref this.mAdvanceStarParam, json.AdvanceStar);
      AdvanceRewardParam.Deserialize(ref this.mAdvanceRewardParam, json.AdvanceReward);
      AdvanceLapBossParam.Deserialize(ref this.mAdvanceLapBossParam, json.AdvanceLapBoss);
      AdvanceEventParam.Deserialize(ref this.mAdvanceEventParam, json.AdvanceEvent);
      GuildRaidMaster.Deserialize<GuildRaidBossParam, JSON_GuildRaidBossParam>(ref this.mGuildRaidBossParam, json.GuildRaidBoss);
      GuildRaidMaster.Deserialize<GuildRaidCoolDaysParam, JSON_GuildRaidCoolDaysParam>(ref this.mGuildRaidCoolDaysParam, json.GuildRaidCoolDays);
      GuildRaidMaster.Deserialize<GuildRaidScoreParam, JSON_GuildRaidScoreParam>(ref this.mGuildRaidScoreParam, json.GuildRaidScore);
      GuildRaidMaster.Deserialize<GuildRaidPeriodParam, JSON_GuildRaidPeriodParam>(ref this.mGuildRaidPeriodParam, json.GuildRaidPeriod);
      GuildRaidMaster.Deserialize<GuildRaidRewardParam, JSON_GuildRaidRewardParam>(ref this.mGuildRaidRewardParam, json.GuildRaidReward);
      GuildRaidMaster.Deserialize<GuildRaidRewardDmgRankingParam, JSON_GuildRaidRewardDmgRankingParam>(ref this.mGuildRaidRewardDmgRankingParam, json.GuildRaidRewardDmgRanking);
      GuildRaidMaster.Deserialize<GuildRaidRewardDmgRatioParam, JSON_GuildRaidRewardDmgRatioParam>(ref this.mGuildRaidRewardDmgRatioParam, json.GuildRaidRewardDmgRatio);
      GuildRaidMaster.Deserialize<GuildRaidRewardRoundParam, JSON_GuildRaidRewardRoundParam>(ref this.mGuildRaidRewardRoundParam, json.GuildRaidRewardRound);
      GuildRaidMaster.Deserialize<GuildRaidRewardRankingParam, JSON_GuildRaidRewardRankingParam>(ref this.mGuildRaidRewardRankingParam, json.GuildRaidRewardRanking);
      GvGMaster.Deserialize<GvGPeriodParam, JSON_GvGPeriodParam>(ref this.mGvGPeriodParam, json.GvGPeriod);
      GvGMaster.Deserialize<GvGNodeParam, JSON_GvGNodeParam>(ref this.mGvGNodeParam, json.GvGNode);
      GvGMaster.Deserialize<GvGNPCPartyParam, JSON_GvGNPCPartyParam>(ref this.mGvGNPCPartyParam, json.GvGNPCParty);
      this.mGvGNPCUnitParam = new List<GvGNPCUnitParam>();
      if (json.GvGNPCUnit != null)
      {
        for (int index = 0; index < json.GvGNPCUnit.Length; ++index)
        {
          GvGNPCUnitParam gvGnpcUnitParam = new GvGNPCUnitParam();
          if (gvGnpcUnitParam.Deserialize(json.GvGNPCUnit[index]))
            this.mGvGNPCUnitParam.Add(gvGnpcUnitParam);
        }
      }
      GvGMaster.Deserialize<GvGRewardRankingParam, JSON_GvGRewardRankingParam>(ref this.mGvGRewardRankingParam, json.GvGRewardRanking);
      GvGMaster.Deserialize<GvGRewardParam, JSON_GvGRewardParam>(ref this.mGvGRewardParam, json.GvGReward);
      GvGMaster.Deserialize<GvGRuleParam, JSON_GvGRuleParam>(ref this.mGvGRuleParam, json.GvGRule);
      GvGMaster.Deserialize<GvGNodeRewardParam, JSON_GvGNodeRewardParam>(ref this.mGvGNodeRewardParam, json.GvGNodeReward);
      GvGMaster.Deserialize<GvGLeagueParam, JSON_GvGLeagueParam>(ref this.mGvGLeagueParam, json.GvGLeague);
      GvGMaster.Deserialize<GvGCalcRateSettingParam, JSON_GvGCalcRateSettingParam>(ref this.mGvGCalcRateSettingParam, json.GvGCalcRateSetting);
      WorldRaidParam.Deserialize(ref this.mWorldRaidParamList, json.WorldRaid);
      WorldRaidBossParam.Deserialize(ref this.mWorldRaidBossParamList, json.WorldRaidBoss);
      WorldRaidRewardParam.Deserialize(ref this.mWorldRaidRewardParamList, json.WorldRaidReward);
      WorldRaidDamageRewardParam.Deserialize(ref this.mWorldRaidDamageRewardParamList, json.WorldRaidDamageReward);
      WorldRaidDamageLotteryParam.Deserialize(ref this.mWorldRaidDamageLotteryParamList, json.WorldRaidDamageLottery);
      WorldRaidRankingRewardParam.Deserialize(ref this.mWorldRaidRankingRewardParamList, json.WorldRaidRankingReward);
    }

    public bool Deserialize(Json_AchievementList json)
    {
      this.mAchievement.Clear();
      int num = 0;
      while (num < json.achievements.Length)
        ++num;
      return true;
    }

    public void ResetJigenQuests(bool is_genesis = false, bool is_advance = false)
    {
      for (int index = 0; index < this.mQuests.Count; ++index)
      {
        if ((!is_genesis || this.mQuests[index].IsGenesis) && (!is_advance || this.mQuests[index].IsAdvance))
        {
          this.mQuests[index].start = 0L;
          this.mQuests[index].end = 0L;
          if (this.mQuests[index].IsKeyQuest)
            this.mQuests[index].Chapter.key_end = 0L;
          this.mQuests[index].gps_enable = false;
        }
      }
    }

    public void ResetGpsQuests()
    {
      for (int index = 0; index < this.mQuests.Count; ++index)
        this.mQuests[index].gps_enable = false;
    }

    public bool DeserializeGps(JSON_QuestProgress[] jsons)
    {
      if (jsons == null)
        return false;
      for (int index = 0; index < jsons.Length; ++index)
      {
        JSON_QuestProgress json = jsons[index];
        if (json != null)
        {
          QuestParam quest = this.FindQuest(json.i);
          if (quest != null && (quest.IsGps || quest.IsMultiAreaQuest))
            quest.gps_enable = true;
        }
      }
      return true;
    }

    public bool Deserialize(JSON_QuestProgress[] json)
    {
      if (json == null)
        return true;
      for (int index = 0; index < json.Length; ++index)
      {
        if (!this.Deserialize(json[index]))
          return false;
      }
      QuestParam.ClearGenesisIsBossLiberation();
      QuestParam.ClearAdvanceIsBossLiberation();
      return true;
    }

    public bool Deserialize(JSON_QuestProgress json)
    {
      if (json == null)
        return true;
      QuestParam quest = this.FindQuest(json.i);
      if (quest == null)
        return true;
      OInt m = (OInt) json.m;
      quest.clear_missions = (int) m;
      quest.state = (QuestStates) json.t;
      quest.start = json.s;
      quest.end = json.e;
      quest.key_end = json.n;
      quest.key_cnt = json.c;
      if (quest.Chapter != null)
      {
        quest.Chapter.start = quest.Chapter.start > 0L ? Math.Min(quest.Chapter.start, json.s) : json.s;
        quest.Chapter.end = Math.Max(quest.Chapter.end, json.e);
        if (quest.Chapter.IsKeyQuest())
          quest.Chapter.key_end = json.e != 0L ? Math.Max(quest.Chapter.key_end, Math.Min(json.e, json.n)) : Math.Max(quest.Chapter.key_end, json.n);
      }
      if (json.d != null)
      {
        quest.dailyCount = CheckCast.to_short(json.d.num);
        quest.dailyReset = CheckCast.to_short(json.d.reset);
      }
      quest.best_clear_time = json.b;
      return true;
    }

    public void Deserialize(JSON_ChapterCount[] json)
    {
      if (json == null || json.Length <= 0)
      {
        for (int index = 0; index < this.mAreas.Count; ++index)
          this.mAreas[index].challengeCount = 0;
      }
      else
      {
        for (int index = 0; index < json.Length; ++index)
          this.Deserialize(json[index]);
      }
    }

    public void Deserialize(JSON_ChapterCount json)
    {
      if (json == null)
        return;
      ChapterParam area = this.FindArea(json.i);
      if (area == null)
        return;
      area.challengeCount = json.num;
    }

    public bool Deserialize(JSON_OpenedQuestArchivesListResponse listResponse)
    {
      if (listResponse == null)
        return false;
      this.Player.QuestArchivesFreeEndTime = (long) listResponse.free_end;
      if (listResponse.archives != null && listResponse.archives.Length > 0)
      {
        this.Player.OpenedQuestArchives.Clear();
        foreach (JSON_OpenedQuestArchive archive in listResponse.archives)
          this.Player.OpenedQuestArchives.Add(new OpenedQuestArchive()
          {
            iname = archive.iname,
            begin_at = TimeManager.FromUnixTime(archive.begin_at),
            end_at = TimeManager.FromUnixTime(archive.end_at)
          });
      }
      return true;
    }

    public bool Deserialize(JSON_QuestArchiveOpenResponse response)
    {
      if (response == null || response.archive == null)
        return false;
      JSON_OpenedQuestArchive archive = response.archive;
      Json_Item[] items = response.items;
      foreach (OpenedQuestArchive openedQuestArchive in this.Player.OpenedQuestArchives)
      {
        if (openedQuestArchive.iname == archive.iname)
        {
          this.Player.OpenedQuestArchives.Remove(openedQuestArchive);
          break;
        }
      }
      this.Player.OpenedQuestArchives.Add(new OpenedQuestArchive()
      {
        iname = archive.iname,
        begin_at = TimeManager.FromUnixTime(archive.begin_at),
        end_at = TimeManager.FromUnixTime(archive.end_at)
      });
      if (items != null)
        this.Deserialize(items);
      this.Player.QuestArchivesFreeEndTime = (long) response.free_end;
      return true;
    }

    public bool Deserialize(Json_GachaList json, bool diff = false)
    {
      if (json == null || json.gachas == null)
        return false;
      if (!diff)
        this.mGachas.Clear();
      for (int i = 0; i < json.gachas.Length; ++i)
      {
        GachaParam gachaParam = this.mGachas.Find((Predicate<GachaParam>) (g => g.iname == json.gachas[i].iname));
        if (gachaParam == null)
        {
          gachaParam = new GachaParam();
          this.mGachas.Add(gachaParam);
        }
        gachaParam.Deserialize(json.gachas[i]);
      }
      return true;
    }

    public void Deserialize(Json_GachaResult json)
    {
      this.mPlayer.Deserialize(json.player);
      this.mPlayer.Deserialize(json.items);
      this.mPlayer.Deserialize(json.units);
      this.mPlayer.Deserialize(json.mails);
      if (json.artifacts != null)
        this.mPlayer.Deserialize(json.artifacts, true);
      this.mPlayer.TrophyData.OverwriteTrophyProgress(json.trophyprogs);
      this.mPlayer.TrophyData.OverwriteTrophyProgress(json.bingoprogs);
    }

    public void Deserialize(Json_GoogleReview json)
    {
      this.mPlayer.Deserialize(json.player);
      this.mPlayer.Deserialize(json.items);
      this.mPlayer.Deserialize(json.units);
      this.mPlayer.Deserialize(json.mails);
    }

    public bool Deserialize(Json_ArenaPlayers json)
    {
      this.mArenaPlayers.Clear();
      if (!this.Player.Deserialize(json))
        return false;
      if (json.coloenemies != null)
      {
        for (int index = 0; index < json.coloenemies.Length; ++index)
        {
          ArenaPlayer arenaPlayer = new ArenaPlayer();
          try
          {
            arenaPlayer.Deserialize(json.coloenemies[index]);
            this.mArenaPlayers.Add(arenaPlayer);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
        }
      }
      return true;
    }

    public bool Deserialize(Json_ArenaEnemies json)
    {
      this.mArenaPlayers.Clear();
      if (!this.Player.Deserialize(json))
        return false;
      if (json.coloenemies != null)
      {
        for (int index = 0; index < json.coloenemies.Length; ++index)
        {
          ArenaPlayer arenaPlayer = new ArenaPlayer();
          try
          {
            arenaPlayer.Deserialize(json.coloenemies[index]);
            this.mArenaPlayers.Add(arenaPlayer);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
        }
      }
      return true;
    }

    public bool Deserialize(JSON_ArenaRanking json, ReqBtlColoRanking.RankingTypes type)
    {
      List<ArenaPlayer> arenaPlayerList = this.mArenaRanking[(int) type];
      arenaPlayerList.Clear();
      if (json.coloenemies != null)
      {
        for (int index = 0; index < json.coloenemies.Length; ++index)
        {
          ArenaPlayer arenaPlayer = new ArenaPlayer();
          try
          {
            arenaPlayer.Deserialize(json.coloenemies[index]);
            arenaPlayerList.Add(arenaPlayer);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
        }
      }
      return true;
    }

    public bool Deserialize(JSON_ArenaHistory json)
    {
      List<ArenaPlayerHistory> mArenaHistory = this.mArenaHistory;
      mArenaHistory.Clear();
      if (json.colohistories != null)
      {
        for (int index = 0; index < json.colohistories.Length; ++index)
        {
          ArenaPlayerHistory arenaPlayerHistory = new ArenaPlayerHistory();
          try
          {
            arenaPlayerHistory.Deserialize(json.colohistories[index]);
            mArenaHistory.Add(arenaPlayerHistory);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
        }
      }
      return true;
    }

    public void Deserialize(JSON_ReqTowerResuponse json)
    {
      TowerResuponse towerResuponse = new TowerResuponse();
      towerResuponse.Deserialize(json);
      this.mTowerResuponse = towerResuponse;
    }

    public bool Deserialize(JSON_ChatChannelMaster json)
    {
      if (json == null || json.channels == null)
        return false;
      this.mChatChannelMasters.Clear();
      for (int index = 0; index < json.channels.Length; ++index)
      {
        ChatChannelMasterParam channelMasterParam = new ChatChannelMasterParam();
        channelMasterParam.Deserialize(json.channels[index]);
        this.mChatChannelMasters.Add(channelMasterParam);
      }
      return true;
    }

    public bool Deserialize(Json_VersusFriendScore[] json)
    {
      if (json == null || this.mVersusFriendScore == null)
        return false;
      this.mVersusFriendScore.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        VersusFriendScore versusFriendScore = new VersusFriendScore();
        versusFriendScore.floor = json[index].floor;
        versusFriendScore.name = json[index].name;
        versusFriendScore.unit = new UnitData();
        versusFriendScore.unit.Deserialize(json[index].unit);
        this.mVersusFriendScore.Add(versusFriendScore);
      }
      return true;
    }

    public bool SetVersusWinCount(int wincnt)
    {
      PlayerData player = this.Player;
      if (player != null)
      {
        player.SetVersusWinCount(GlobalVars.SelectedMultiPlayVersusType, wincnt);
        player.AddVersusTotalCount(GlobalVars.SelectedMultiPlayVersusType, 1);
      }
      return true;
    }

    public void SetVersuTowerEndParam(
      bool rankup,
      bool winbonus,
      int key,
      int floor,
      int arravied)
    {
      if (this.mVersusEndParam == null)
        return;
      this.mVersusEndParam.rankup = rankup;
      this.mVersusEndParam.winbonus = winbonus;
      this.mVersusEndParam.key = key;
      this.mVersusEndParam.floor = floor;
      this.mVersusEndParam.arravied = arravied;
    }

    public VsTowerMatchEndParam GetVsTowerMatchEndParam() => this.mVersusEndParam;

    public void SetAvailableRankingQuestParams(List<RankingQuestParam> value)
    {
      this.mAvailableRankingQuesstParams = value;
    }

    public UnitParam GetUnitParam(string key) => this.mMasterParam.GetUnitParam(key);

    public SkillParam GetSkillParam(string key) => this.mMasterParam.GetSkillParam(key);

    public AbilityParam GetAbilityParam(string key) => this.mMasterParam.GetAbilityParam(key);

    public ItemParam GetItemParam(string key) => this.mMasterParam.GetItemParam(key);

    public AwardParam GetAwardParam(string key) => this.mMasterParam.GetAwardParam(key);

    public GeoParam GetGeoParam(string key) => this.mMasterParam.GetGeoParam(key);

    public WeaponParam GetWeaponParam(string key) => this.mMasterParam.GetWeaponParam(key);

    public RecipeParam GetRecipeParam(string key) => this.mMasterParam.GetRecipeParam(key);

    public JobParam GetJobParam(string key) => this.mMasterParam.GetJobParam(key);

    public JobParam[] GetAllJobs() => this.mMasterParam.GetAllJobs();

    public JobSetParam GetJobSetParam(string key) => this.mMasterParam.GetJobSetParam(key);

    public JobSetParam[] GetClassChangeJobSetParam(string key)
    {
      return this.mMasterParam.GetClassChangeJobSetParam(key);
    }

    public GrowParam GetGrowParam(string key) => this.mMasterParam.GetGrowParam(key);

    public AIParam GetAIParam(string key) => this.mMasterParam.GetAIParam(key);

    public RarityParam GetRarityParam(int rarity) => this.mMasterParam.GetRarityParam(rarity);

    public List<GachaParam> GetGachaList(string category)
    {
      List<GachaParam> gachaList = new List<GachaParam>();
      for (int index = 0; index < this.mGachas.Count; ++index)
      {
        if (this.mGachas[index].category == category)
          gachaList.Add(this.mGachas[index]);
      }
      return gachaList;
    }

    public ChatChannelMasterParam[] GetChatChannelMaster()
    {
      return this.mChatChannelMasters != null ? this.mChatChannelMasters.ToArray() : new ChatChannelMasterParam[0];
    }

    public string DeviceId => this.mMyGuid != null ? this.mMyGuid.device_id : (string) null;

    public string SecretKey => this.mMyGuid != null ? this.mMyGuid.secret_key : (string) null;

    public string UdId => this.mMyGuid != null ? this.mMyGuid.udid : (string) null;

    public bool IsDeviceId() => this.DeviceId != null;

    public bool InitAuth()
    {
      if (this.mMyGuid == null)
        this.mMyGuid = new MyGUID();
      this.mMyGuid.Init(31221512);
      return true;
    }

    public void SaveAuth(string device_id)
    {
      this.mMyGuid.SaveAuth(31221512, this.SecretKey, device_id, this.UdId);
    }

    public void SaveAuthWithKey(string device_id, string secretKey)
    {
      this.mMyGuid.SetSecretKey(secretKey);
      this.mMyGuid.SaveAuth(31221512, this.SecretKey, device_id, this.UdId);
    }

    public void ResetAuth()
    {
      this.mMyGuid.ResetCache();
      SRPG.Network.SessionID = string.Empty;
    }

    private string GenerateSalt()
    {
      byte[] numArray = new byte[24];
      new RNGCryptoServiceProvider().GetBytes(numArray);
      return Convert.ToBase64String(numArray);
    }

    private string GenerateHash(string pass, string salt)
    {
      string empty = string.Empty;
      System.Security.Cryptography.SHA256 shA256 = System.Security.Cryptography.SHA256.Create();
      byte[] bytes = new UTF8Encoding().GetBytes(pass + salt);
      string base64String = Convert.ToBase64String(shA256.ComputeHash(bytes));
      shA256.Clear();
      return base64String;
    }

    public string Encrypt(string key, string iv, string src)
    {
      RijndaelManaged rijndaelManaged1 = new RijndaelManaged();
      rijndaelManaged1.Padding = PaddingMode.Zeros;
      rijndaelManaged1.Mode = CipherMode.CBC;
      RijndaelManaged rijndaelManaged2 = rijndaelManaged1;
      int num1 = 128;
      rijndaelManaged1.BlockSize = num1;
      int num2 = num1;
      rijndaelManaged2.KeySize = num2;
      byte[] bytes1 = System.Text.Encoding.UTF8.GetBytes(key);
      byte[] bytes2 = System.Text.Encoding.UTF8.GetBytes(iv);
      byte[] bytes3 = System.Text.Encoding.UTF8.GetBytes(src);
      ICryptoTransform encryptor = rijndaelManaged1.CreateEncryptor(bytes1, bytes2);
      MemoryStream memoryStream = new MemoryStream();
      CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write);
      cryptoStream.Write(bytes3, 0, bytes3.Length);
      cryptoStream.FlushFinalBlock();
      return Convert.ToBase64String(memoryStream.ToArray());
    }

    public string Decrypt(string key, string iv, string src)
    {
      RijndaelManaged rijndaelManaged1 = new RijndaelManaged();
      rijndaelManaged1.Padding = PaddingMode.Zeros;
      rijndaelManaged1.Mode = CipherMode.CBC;
      RijndaelManaged rijndaelManaged2 = rijndaelManaged1;
      int num1 = 128;
      rijndaelManaged1.BlockSize = num1;
      int num2 = num1;
      rijndaelManaged2.KeySize = num2;
      byte[] bytes1 = System.Text.Encoding.UTF8.GetBytes(key);
      byte[] bytes2 = System.Text.Encoding.UTF8.GetBytes(iv);
      byte[] buffer = Convert.FromBase64String(src);
      byte[] numArray = new byte[buffer.Length];
      ICryptoTransform decryptor = rijndaelManaged1.CreateDecryptor(bytes1, bytes2);
      new CryptoStream((Stream) new MemoryStream(buffer), decryptor, CryptoStreamMode.Read).Read(numArray, 0, numArray.Length);
      return System.Text.Encoding.UTF8.GetString(numArray);
    }

    public bool CheckBadges(GameManager.BadgeTypes type)
    {
      return (this.BadgeFlags & type) != ~GameManager.BadgeTypes.All;
    }

    public bool CheckBusyBadges(GameManager.BadgeTypes type)
    {
      return (this.IsBusyUpdateBadges & type) != ~GameManager.BadgeTypes.All;
    }

    public void RequestUpdateBadges(GameManager.BadgeTypes type)
    {
      if ((type & GameManager.BadgeTypes.DailyMission) != ~GameManager.BadgeTypes.All)
      {
        this.BadgeFlags &= ~GameManager.BadgeTypes.DailyMission;
        this.Player.TrophyData.UpdateTrophyStates();
        foreach (TrophyState trophyStates in this.Player.TrophyData.TrophyStatesList)
        {
          if (trophyStates.Param.IsShowBadge(trophyStates))
          {
            this.BadgeFlags |= GameManager.BadgeTypes.DailyMission;
            break;
          }
        }
      }
      if ((type & GameManager.BadgeTypes.GiftBox) != ~GameManager.BadgeTypes.All)
      {
        this.BadgeFlags &= ~GameManager.BadgeTypes.GiftBox;
        if (this.Player.UnreadMail || this.Player.UnreadMailPeriod)
          this.BadgeFlags |= GameManager.BadgeTypes.GiftBox;
      }
      if ((type & GameManager.BadgeTypes.Friend) != ~GameManager.BadgeTypes.All)
      {
        this.BadgeFlags &= ~GameManager.BadgeTypes.Friend;
        if (0 < this.Player.FollowerNum)
          this.BadgeFlags |= GameManager.BadgeTypes.Friend;
      }
      if ((type & GameManager.BadgeTypes.Unit) != ~GameManager.BadgeTypes.All)
        this.StartCoroutine(this.UpdateUnitsBadges());
      if ((type & GameManager.BadgeTypes.UnitUnlock) != ~GameManager.BadgeTypes.All)
        this.StartCoroutine(this.UpdateUnitUnlockBadges());
      if ((type & GameManager.BadgeTypes.GoldGacha) != ~GameManager.BadgeTypes.All)
      {
        this.BadgeFlags &= ~GameManager.BadgeTypes.GoldGacha;
        if (this.Player.CheckFreeGachaGold())
          this.BadgeFlags |= GameManager.BadgeTypes.GoldGacha;
      }
      if ((type & GameManager.BadgeTypes.RareGacha) != ~GameManager.BadgeTypes.All)
      {
        this.BadgeFlags &= ~GameManager.BadgeTypes.RareGacha;
        if (this.Player.CheckFreeGachaCoin())
          this.BadgeFlags |= GameManager.BadgeTypes.RareGacha;
      }
      if ((type & GameManager.BadgeTypes.Arena) != ~GameManager.BadgeTypes.All)
      {
        this.BadgeFlags &= ~GameManager.BadgeTypes.Arena;
        if (this.Player.CheckUnlock(UnlockTargets.Arena) && this.Player.ChallengeArenaNum == this.Player.ChallengeArenaMax)
          this.BadgeFlags |= GameManager.BadgeTypes.Arena;
      }
      if ((type & GameManager.BadgeTypes.Multiplay) == ~GameManager.BadgeTypes.All)
        return;
      this.BadgeFlags &= ~GameManager.BadgeTypes.Multiplay;
      if (!this.Player.CheckUnlock(UnlockTargets.MultiPlay) || this.Player.ChallengeMultiNum != 0)
        return;
      this.BadgeFlags |= GameManager.BadgeTypes.Multiplay;
    }

    [DebuggerHidden]
    private IEnumerator UpdateUnitsBadges()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CUpdateUnitsBadges\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator UpdateUnitUnlockBadges()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CUpdateUnitUnlockBadges\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    public bool CheckEnableUnitUnlock(UnitParam unit)
    {
      return this.mBadgeUnitUnlocks != null && this.mBadgeUnitUnlocks.Contains(unit);
    }

    public void GetLearningAbilitySource(
      UnitData unit,
      string abilityID,
      out JobParam job,
      out int rank)
    {
      for (int index = 0; index < unit.Jobs.Length; ++index)
      {
        rank = unit.Jobs[index].Param.FindRankOfAbility(abilityID);
        if (rank != -1)
        {
          job = unit.Jobs[index].Param;
          return;
        }
      }
      job = (JobParam) null;
      rank = -1;
    }

    public SectionParam GetCurrentSectionParam()
    {
      SectionParam currentSectionParam = (SectionParam) null;
      if (!string.IsNullOrEmpty((string) GlobalVars.HomeBgSection))
      {
        currentSectionParam = this.FindWorld((string) GlobalVars.HomeBgSection);
        if (currentSectionParam != null && (string.IsNullOrEmpty(currentSectionParam.home) || currentSectionParam.hidden))
          currentSectionParam = (SectionParam) null;
      }
      if (currentSectionParam == null)
      {
        QuestParam lastStoryQuest = this.Player.FindLastStoryQuest();
        if (lastStoryQuest != null)
        {
          ChapterParam area = this.FindArea(lastStoryQuest.ChapterID);
          if (area != null)
            currentSectionParam = this.FindWorld(area.section);
        }
      }
      return currentSectionParam;
    }

    public bool IsLogin => this.mHasLoggedIn;

    public void PostLogin()
    {
      this.mHasLoggedIn = true;
      this.mTutorialStep = 0;
      this.mLastStamina = this.Player.Stamina;
      this.mLastGold = (long) this.Player.Gold;
      this.mLastAbilityRankUpCount = this.Player.AbilityRankUpCountNum;
      this.Player.ClearItemFlags(ItemData.ItemFlags.NewItem | ItemData.ItemFlags.NewSkin);
      this.Player.LoginReset();
      GlobalVars.SelectedUnitUniqueID.Set(0L);
      GlobalVars.SelectedJobUniqueID.Set(0L);
      GlobalVars.SelectedEquipmentSlot.Set(-1);
      GlobalVars.ResetVarsWithAttribute(typeof (GlobalVars.ResetOnLogin));
      this.mLastUpdateTime = TimeManager.ServerTime;
      HomeWindow.EnterHomeCount = 0;
      MySmartBeat.SetupUserInfo();
      FlowNode_ReqGuildTrophy.ResetTimer();
    }

    public void NotifyAbilityRankUpCountChanged()
    {
      int abilityRankUpCountNum = this.Player.AbilityRankUpCountNum;
      this.mLastAbilityRankUpCount = abilityRankUpCountNum;
      this.OnAbilityRankUpCountChange(abilityRankUpCountNum);
    }

    private void UpdateResolution()
    {
      bool flag = false;
      if (GameManager.mUpscaleMode == flag)
        return;
      GameManager.mUpscaleMode = flag;
      int defaultScreenWidth = ScreenUtility.DefaultScreenWidth;
      int h = ScreenUtility.DefaultScreenHeight;
      if (flag)
      {
        float num1 = (float) defaultScreenWidth / (float) h;
        int num2 = Mathf.Min(h, 750);
        defaultScreenWidth = Mathf.FloorToInt(num1 * (float) num2);
        h = num2;
      }
      ScreenUtility.SetResolution(defaultScreenWidth, h);
      DebugUtility.Log(string.Format("Changing Resolution to [{0} x {1}]", (object) defaultScreenWidth, (object) h));
    }

    private void Update()
    {
      AppGuardClient.DoGameGuardCheck();
      if (this.mHasLoggedIn)
      {
        DateTime serverTime = TimeManager.ServerTime;
        long num1 = serverTime.Ticks / 864000000000L;
        long num2 = this.mLastUpdateTime.Ticks / 864000000000L;
        this.mLastUpdateTime = serverTime;
        if (num1 - num2 < 1L)
          ;
        int stamina = this.Player.Stamina;
        if (stamina != this.mLastStamina)
        {
          this.mLastStamina = stamina;
          this.OnStaminaChange();
        }
        this.Player.UpdateAbilityRankUpCount();
        int abilityRankUpCountNum = this.Player.AbilityRankUpCountNum;
        if (abilityRankUpCountNum != this.mLastAbilityRankUpCount)
        {
          this.OnAbilityRankUpCountChange(abilityRankUpCountNum);
          this.mLastAbilityRankUpCount = abilityRankUpCountNum;
        }
        if ((long) this.Player.Gold != this.mLastGold)
        {
          this.mLastGold = (long) this.Player.Gold;
          this.OnPlayerCurrencyChange();
        }
        this.UpdateTrophy();
        if (this.mAudienceManager != null)
          this.mAudienceManager.Update();
      }
      this.EnableAnimationFrameSkipping = false;
      AnimationPlayer.MaxUpdateTime = !this.EnableAnimationFrameSkipping ? long.MaxValue : (long) ((double) Mathf.Lerp(1f / 1000f, 3f / 500f, 1f - Mathf.Clamp01((float) (((double) Time.unscaledDeltaTime - 0.026000000536441803) / 0.0039999987930059433))) * 10000000.0);
      this.UpdateTextureLoadRequests();
    }

    private void DayChanged()
    {
      if (this.Player == null)
        return;
      this.Player.ResetStaminaRecoverCount();
      this.Player.ResetBuyGoldNum();
      this.Player.ResetQuestChallengeResets();
      this.Player.ResetQuestChallenges();
      this.Player.TrophyData.UpdateTrophyStates();
    }

    public void StartBuyStaminaSequence(bool staminaLacking)
    {
      this.StartBuyStaminaSequence(staminaLacking, (PartyWindow2) null);
    }

    public void StartBuyStaminaSequence(bool _staminaLacking, PartyWindow2 _pwindow)
    {
      if (this.Player.IsHaveHealAPItems())
      {
        this.StartCoroutine(this.StartBuyStaminaSequence2(_pwindow));
      }
      else
      {
        FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
        string empty = string.Empty;
        string text;
        if (_staminaLacking)
          text = LocalizedText.Get("sys.STAMINA_NOT_ENOUGH", (object) this.Player.GetStaminaRecoveryCost(), (object) fixParam.StaminaAdd);
        else
          text = LocalizedText.Get("sys.RESET_STAMINA", (object) this.Player.GetStaminaRecoveryCost(), (object) fixParam.StaminaAdd);
        UIUtility.ConfirmBox(text, (string) null, (UIUtility.DialogResultEvent) (go => this.ContinueBuyStamina()), (UIUtility.DialogResultEvent) null);
      }
    }

    [DebuggerHidden]
    private IEnumerator StartBuyStaminaSequence2(PartyWindow2 _pwindow)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CStartBuyStaminaSequence2\u003Ec__Iterator2()
      {
        _pwindow = _pwindow
      };
    }

    private void ContinueBuyStamina()
    {
      if (this.Player.StaminaBuyNum >= this.MasterParam.GetVipBuyStaminaLimit(this.Player.VipRank))
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.STAMINA_BUY_LIMIT"), (UIUtility.DialogResultEvent) (go => { }));
      }
      else
      {
        if (!this.NotRequiredHeal() || !this.CoinShortage() || SRPG.Network.Mode != SRPG.Network.EConnectMode.Online)
          return;
        SRPG.Network.RequestAPI((WebAPI) new ReqItemAddStmPaid(new SRPG.Network.ResponseCallback(this.OnBuyStamina)));
      }
    }

    public bool NotRequiredHeal()
    {
      if (this.Player.StaminaStockCap > this.Player.Stamina)
        return true;
      UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.STAMINAFULL"), (UIUtility.DialogResultEvent) (go => { }));
      return false;
    }

    public bool CoinShortage()
    {
      if (this.Player.Coin >= this.Player.GetStaminaRecoveryCost())
        return true;
      this.ConfirmBuyCoin((GameManager.BuyCoinEvent) null, (GameManager.BuyCoinEvent) null);
      return false;
    }

    private void OnBuyStamina(WWWResult www)
    {
      if (SRPG.Network.IsError)
      {
        if (SRPG.Network.ErrCode == SRPG.Network.EErrCode.StaminaCoinShort)
          SRPG.Network.RemoveAPI();
        else
          FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          int staminaRecoveryCost = this.Player.GetStaminaRecoveryCost();
          if (!this.Player.Deserialize(jsonObject.body.player, PlayerData.EDeserializeFlags.Coin | PlayerData.EDeserializeFlags.Stamina))
          {
            FlowNode_Network.Retry();
          }
          else
          {
            SRPG.Network.RemoveAPI();
            MyMetaps.TrackSpendCoin("BuyStamina", staminaRecoveryCost);
            UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.STAMINARECOVERED", (object) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StaminaAdd), (UIUtility.DialogResultEvent) (go => { }));
            GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_COIN_STATUS.ToString(), (object) this);
          }
        }
      }
    }

    public void ConfirmBuyCoin(GameManager.BuyCoinEvent onEnd, GameManager.BuyCoinEvent onCancel)
    {
      this.mOnBuyCoinEnd = onEnd;
      this.mOnBuyCoinCancel = onCancel;
      this.mBuyCoinWindow = UIUtility.ConfirmBox(LocalizedText.Get("sys.OUT_OF_COIN_CONFIRM_BUY_COIN"), (string) null, (UIUtility.DialogResultEvent) (go => this.StartBuyCoinSequence()), new UIUtility.DialogResultEvent(this.OnBuyCoinConfirmCancel));
    }

    private void OnBuyCoinEnd(GameObject go)
    {
      if (this.mOnBuyCoinEnd == null)
        return;
      GameManager.BuyCoinEvent mOnBuyCoinEnd = this.mOnBuyCoinEnd;
      this.mOnBuyCoinEnd = (GameManager.BuyCoinEvent) null;
      mOnBuyCoinEnd();
    }

    private void OnBuyCoinConfirmCancel(GameObject go)
    {
      if (this.mOnBuyCoinCancel == null)
        return;
      GameManager.BuyCoinEvent mOnBuyCoinCancel = this.mOnBuyCoinCancel;
      this.mOnBuyCoinCancel = (GameManager.BuyCoinEvent) null;
      mOnBuyCoinCancel();
    }

    public void StartBuyCoinSequence()
    {
      GameObject dialogBuyCoin = GameSettings.Instance.Dialog_BuyCoin;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) dialogBuyCoin, (UnityEngine.Object) null))
        return;
      this.mBuyCoinWindow = UnityEngine.Object.Instantiate<GameObject>(dialogBuyCoin);
      this.mBuyCoinWindow.RequireComponent<DestroyEventListener>().Listeners += new DestroyEventListener.DestroyEvent(this.OnBuyCoinEnd);
    }

    public bool IsBuyCoinWindowOpen
    {
      get
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBuyCoinWindow, (UnityEngine.Object) null))
          return true;
        this.mBuyCoinWindow = (GameObject) null;
        return false;
      }
    }

    public void RegisterImportantJob(Coroutine co)
    {
      this.mImportantJobs.Add(co);
      if (this.mImportantJobCoroutine != null)
        return;
      this.mImportantJobCoroutine = this.StartCoroutine(this.AsyncWaitForImportantJobs());
    }

    [DebuggerHidden]
    private IEnumerator AsyncWaitForImportantJobs()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CAsyncWaitForImportantJobs\u003Ec__Iterator3()
      {
        \u0024this = this
      };
    }

    public bool IsImportantJobRunning => this.mImportantJobs.Count > 0;

    public bool PrepareSceneChange()
    {
      foreach (System.Delegate invocation in this.OnSceneChange.GetInvocationList())
      {
        if (!(invocation as GameManager.SceneChangeEvent)())
          return false;
      }
      return true;
    }

    public void ApplyTextureAsync(RawImage target, string path)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) target, (UnityEngine.Object) null))
        return;
      for (int index = 0; index < this.mTextureRequests.Count; ++index)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTextureRequests[index].Target, (UnityEngine.Object) target))
        {
          if (!(this.mTextureRequests[index].Path != path))
            return;
          if (string.IsNullOrEmpty(path))
          {
            this.mTextureRequests[index].Target.texture = (Texture) null;
            this.mTextureRequests.RemoveAt(index);
            return;
          }
          this.mTextureRequests[index].Request = (LoadRequest) null;
          this.mTextureRequests[index].Path = path;
          return;
        }
      }
      if (string.IsNullOrEmpty(path))
      {
        target.texture = (Texture) null;
      }
      else
      {
        target.texture = (Texture) null;
        GameManager.TextureRequest texReq = new GameManager.TextureRequest();
        texReq.Target = target;
        texReq.Path = path;
        this.mTextureRequests.Add(texReq);
        if (AssetManager.IsLoading)
          return;
        this.RequestTexture(texReq);
      }
    }

    private bool RequestTexture(GameManager.TextureRequest texReq)
    {
      texReq.Request = AssetManager.LoadAsync<Texture2D>(texReq.Path);
      if (!texReq.Request.isDone)
        return false;
      texReq.Target.texture = (Texture) (texReq.Request.asset as Texture2D);
      return true;
    }

    public void CancelTextureLoadRequest(RawImage target)
    {
      for (int index = 0; index < this.mTextureRequests.Count; ++index)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTextureRequests[index].Target, (UnityEngine.Object) target))
        {
          this.mTextureRequests.RemoveAt(index);
          break;
        }
      }
    }

    private void UpdateTextureLoadRequests()
    {
      for (int index = 0; index < this.mTextureRequests.Count; ++index)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTextureRequests[index].Target, (UnityEngine.Object) null))
          this.mTextureRequests.RemoveAt(index--);
        else if (this.mTextureRequests[index].Request == null)
        {
          if (!AssetManager.IsLoading)
            this.RequestTexture(this.mTextureRequests[index]);
        }
        else if (this.mTextureRequests[index].Request.isDone)
        {
          this.mTextureRequests[index].Target.texture = (Texture) (this.mTextureRequests[index].Request.asset as Texture2D);
          this.mTextureRequests.RemoveAt(index--);
        }
      }
    }

    public void CompleteTutorialStep() => ++this.mTutorialStep;

    public string GetNextTutorialStep()
    {
      GameSettings instance = GameSettings.Instance;
      return this.mTutorialStep >= instance.Tutorial_Steps.Length ? (string) null : instance.Tutorial_Steps[this.mTutorialStep];
    }

    public void UpdateTutorialFlags(long add)
    {
      if ((this.Player.TutorialFlags & add) == add)
        return;
      this.Player.TutorialFlags |= add;
      if (SRPG.Network.Mode == SRPG.Network.EConnectMode.Offline)
        return;
      SRPG.Network.RequestAPI((WebAPI) new ReqTutUpdate(this.Player.TutorialFlags, new SRPG.Network.ResponseCallback(this.OnTutorialFlagUpdate)));
    }

    public void UpdateTutorialFlags(string flagName)
    {
      long tutorialFlagMask = GameSettings.Instance.CreateTutorialFlagMask(flagName);
      if (tutorialFlagMask == 0L)
        return;
      this.UpdateTutorialFlags(tutorialFlagMask);
    }

    public bool IsTutorialFlagSet(string flagName)
    {
      long tutorialFlagMask = GameSettings.Instance.CreateTutorialFlagMask(flagName);
      return tutorialFlagMask != 0L && (this.Player.TutorialFlags & tutorialFlagMask) != 0L;
    }

    public int TutorialStep => this.mTutorialStep;

    private void OnTutorialFlagUpdate(WWWResult www)
    {
      if (SRPG.Network.IsError)
        FlowNode_Network.Retry();
      else
        SRPG.Network.RemoveAPI();
    }

    public void DownloadAndTransitScene(string sceneName)
    {
      if (AssetManager.IsAssetBundle(sceneName))
      {
        CriticalSection.Enter(CriticalSections.SceneChange);
        this.StartCoroutine(this.DownloadAndTransitSceneAsync(sceneName));
      }
      else
        SceneManager.LoadScene(sceneName);
    }

    [DebuggerHidden]
    private IEnumerator DownloadAndTransitSceneAsync(string sceneName)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CDownloadAndTransitSceneAsync\u003Ec__Iterator4()
      {
        sceneName = sceneName
      };
    }

    public void UpdateTutorialStep()
    {
      GameSettings instance = GameSettings.Instance;
      this.mTutorialStep = instance.Tutorial_Steps.Length;
      for (int index = instance.Tutorial_Steps.Length - 1; index >= 0; --index)
      {
        if (!string.IsNullOrEmpty(instance.Tutorial_Steps[index]))
        {
          QuestParam quest = this.FindQuest(instance.Tutorial_Steps[index]);
          if (quest != null && quest.state == QuestStates.Cleared)
            break;
          this.mTutorialStep = index;
        }
      }
    }

    public void OnAgreeTermsOfUse(object caller) => this.AgreedVer = Application.version;

    public bool IsAgreeTermsOfUse() => this.AgreedVer != null && this.AgreedVer.Length > 0;

    public void RefreshTutorialDLAssets(bool forceRefresh = false)
    {
      if (this.mScannedTutorialAssets && !forceRefresh)
        return;
      this.mScannedTutorialAssets = true;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      AssetList.Item[] assets = AssetManager.AssetList.Assets;
      for (int index = 0; index < assets.Length; ++index)
      {
        if ((assets[index].Flags & AssetBundleFlags.Tutorial) != (AssetBundleFlags) 0 && ((assets[index].Flags & AssetBundleFlags.TutorialMovie) == (AssetBundleFlags) 0 || (!GameUtility.IsDebugBuild || GlobalVars.DebugIsPlayTutorial) && (instance.Player.TutorialFlags & 1L) == 0L) && !AssetManager.IsAssetInCache(assets[index].IDStr))
          this.mTutorialDLAssets.Add(assets[index]);
      }
      this.mTutorialDLAssets.Sort((Comparison<AssetList.Item>) ((x, y) =>
      {
        if (x.Size == y.Size)
          return 0;
        return x.Size > y.Size ? 1 : -1;
      }));
    }

    public bool HasTutorialDLAssets
    {
      get
      {
        if (!this.mScannedTutorialAssets)
          this.RefreshTutorialDLAssets();
        return this.mTutorialDLAssets.Count > 0;
      }
    }

    public void DownloadTutorialAssets()
    {
      for (int index = 0; index < this.mTutorialDLAssets.Count; ++index)
      {
        if (AssetManager.IsAssetInCache(this.mTutorialDLAssets[index].IDStr))
          this.mTutorialDLAssets.RemoveAt(index--);
        else
          AssetDownloader.Add(this.mTutorialDLAssets[index].IDStr);
      }
      this.mTutorialDLAssets.Clear();
    }

    public bool PartialDownloadTutorialAssets()
    {
      if (!AssetDownloader.isDone || this.mWaitDownloadThread != null || this.mTutorialDLAssets.Count <= 0)
        return false;
      List<AssetList.Item> queue = new List<AssetList.Item>();
      int networkBgdlChunkSize = GameSettings.Instance.Network_BGDLChunkSize;
      int num = 0;
      for (int index = 0; index < this.mTutorialDLAssets.Count; ++index)
      {
        if (AssetManager.IsAssetInCache(this.mTutorialDLAssets[index].IDStr))
        {
          this.mTutorialDLAssets.RemoveAt(index--);
        }
        else
        {
          queue.Add(this.mTutorialDLAssets[index]);
          AssetDownloader.Add(this.mTutorialDLAssets[index].IDStr);
          num += this.mTutorialDLAssets[index].Size;
          if (num >= networkBgdlChunkSize)
            break;
        }
      }
      if (num <= 0)
        return false;
      AssetDownloader.DownloadState state = AssetDownloader.StartDownload(false, false, ThreadPriority.Lowest);
      if (state == null)
        return false;
      this.mWaitDownloadThread = this.StartCoroutine(this.WaitDownloadAsync(queue, state));
      return true;
    }

    [DebuggerHidden]
    private IEnumerator WaitDownloadAsync(
      List<AssetList.Item> queue,
      AssetDownloader.DownloadState state)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CWaitDownloadAsync\u003Ec__Iterator5()
      {
        state = state,
        queue = queue,
        \u0024this = this
      };
    }

    private void OnApplicationFocus(bool focus)
    {
      MyLocalNotification.CancelStamina();
      if (FlowNode_GetCurrentScene.IsAfterLogin())
        MyLocalNotification.SetStamina(this.MasterParam.LocalNotificationParam, this.Player);
      List<LocalNotificationInfo> localoNotifications = MyLocalNotification.LocaloNotifications;
      if (focus)
      {
        LocalNotification.CancelNotificationsWithCategory(RegularLocalNotificationParam.CATEGORY_MORNING);
        LocalNotification.CancelNotificationsWithCategory(RegularLocalNotificationParam.CATEGORY_NOON);
        LocalNotification.CancelNotificationsWithCategory(RegularLocalNotificationParam.CATEGORY_AFTERNOON);
        if (localoNotifications != null && localoNotifications.Count > 0)
        {
          foreach (LocalNotificationInfo notificationInfo in localoNotifications)
            LocalNotification.CancelNotificationsWithCategory(notificationInfo.trophy_iname);
        }
        MyLocalNotification.ResetComeBack();
      }
      else
      {
        TrophyParam[] trophies = MonoSingleton<GameManager>.Instance.Trophies;
        if (localoNotifications != null && localoNotifications.Count > 0 && trophies != null)
        {
          foreach (LocalNotificationInfo notificationInfo in localoNotifications)
          {
            LocalNotificationInfo lparam = notificationInfo;
            TrophyParam trophyParam = Array.Find<TrophyParam>(trophies, (Predicate<TrophyParam>) (p => p.iname == lparam.trophy_iname));
            if (trophyParam != null && lparam.push_flg != 0)
            {
              string pushWord = lparam.push_word;
              string trophyIname = lparam.trophy_iname;
              for (int index = trophyParam.Objectives.Length - 1; index >= 0; --index)
              {
                int hour = int.Parse(trophyParam.Objectives[index].sval_base.Substring(0, 2));
                MyLocalNotification.SetRegular(new RegularLocalNotificationParam(pushWord, trophyIname, hour, 0, 0), this.Player);
              }
            }
          }
        }
        MyLocalNotification.SetComeBack(LocalizedText.Get("embed.LOCAL_NOTIFICATION_COMEBACK"), 1209600L);
      }
    }

    public void LoadUpdateTrophyList()
    {
      if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.SAVE_UPDATE_TROPHY_LIST_KEY))
        return;
      string s = PlayerPrefsUtility.GetString(PlayerPrefsUtility.SAVE_UPDATE_TROPHY_LIST_KEY, string.Empty);
      byte[] data = !string.IsNullOrEmpty(s) ? Convert.FromBase64String(s) : (byte[]) null;
      string src;
      try
      {
        src = data != null ? MyEncrypt.Decrypt(GameManager.SAVE_UPDATE_TROPHY_LIST_ENCODE_KEY, data) : (string) null;
      }
      catch (Exception ex)
      {
        src = string.Empty;
      }
      this.mSavedUpdateTrophyListString = src;
      JSON_TrophyResponse jsonTrophyResponse;
      try
      {
        jsonTrophyResponse = !string.IsNullOrEmpty(src) ? JSONParser.parseJSONObject<JSON_TrophyResponse>(src) : (JSON_TrophyResponse) null;
      }
      catch (Exception ex)
      {
        jsonTrophyResponse = (JSON_TrophyResponse) null;
      }
      JSON_TrophyProgress[] trophyprogs = jsonTrophyResponse?.trophyprogs;
      if (trophyprogs == null || trophyprogs.Length <= 0)
        return;
      List<TrophyState> trophyStatesList = this.Player.TrophyData.TrophyStatesList;
      for (int index1 = 0; index1 < trophyprogs.Length; ++index1)
      {
        if (trophyprogs[index1] != null)
        {
          TrophyParam trophy = this.MasterParam.GetTrophy(trophyprogs[index1].iname);
          if (trophy != null)
          {
            TrophyState server = (TrophyState) null;
            for (int index2 = 0; index2 < trophyStatesList.Count; ++index2)
            {
              if (trophyStatesList[index2].iname == trophyprogs[index1].iname)
              {
                server = trophyStatesList[index2];
                break;
              }
            }
            if (server == null || this.IsSavedUpdateTrophyStateNeedToSend(server, trophyprogs[index1].ymd, trophyprogs[index1].pts, trophyprogs[index1].rewarded_at != 0))
            {
              TrophyState trophyCounter = this.Player.TrophyData.GetTrophyCounter(trophy);
              if (trophyCounter != null)
              {
                trophyCounter.StartYMD = trophyprogs[index1].ymd;
                if (trophyprogs[index1].pts != null)
                {
                  for (int index3 = 0; index3 < trophyprogs[index1].pts.Length; ++index3)
                    trophyCounter.Count[index3] = trophyprogs[index1].pts[index3];
                }
                trophyCounter.IsEnded = trophyprogs[index1].rewarded_at != 0;
                trophyCounter.IsDirty = true;
                DebugUtility.LogWarning("LoadSavedTrophy: " + trophyprogs[index1].iname + " / " + trophy.Name);
              }
            }
          }
        }
      }
      this.Player.TrophyData.UpdateTrophyStates();
    }

    public void SaveUpdateTrophyList(List<TrophyState> updateList)
    {
      string msg = (string) null;
      if (updateList != null && updateList.Count > 0)
      {
        string str = "{\"trophyprogs\":[";
        bool flag = true;
        foreach (TrophyState update in updateList)
        {
          if (update != null)
          {
            if (flag)
              flag = false;
            else
              str += ",";
            str += "{";
            str = str + "\"iname\":\"" + update.iname + "\"";
            if (update.Count != null)
            {
              str += ",\"pts\":[";
              for (int index = 0; index < update.Count.Length; ++index)
              {
                if (index > 0)
                  str += ",";
                str += (string) (object) update.Count[index];
              }
              str += "]";
            }
            str = str + ",\"ymd\":" + (object) update.StartYMD;
            str += "}";
          }
        }
        msg = str + "]}";
      }
      if (this.mSavedUpdateTrophyListString == msg)
        return;
      byte[] inArray = !string.IsNullOrEmpty(msg) ? MyEncrypt.Encrypt(GameManager.SAVE_UPDATE_TROPHY_LIST_ENCODE_KEY, msg) : (byte[]) null;
      string str1 = inArray != null ? Convert.ToBase64String(inArray) : string.Empty;
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.SAVE_UPDATE_TROPHY_LIST_KEY, str1, true);
      this.mSavedUpdateTrophyListString = msg;
      DebugUtility.LogWarning("SaveTrophy:" + (msg ?? "null"));
    }

    private bool IsSavedUpdateTrophyStateNeedToSend(
      TrophyState server,
      int ymd,
      int[] count,
      bool isEnded)
    {
      if (server == null || this.MasterParam == null)
        return false;
      TrophyParam trophy = this.MasterParam.GetTrophy(server.iname);
      if (trophy == null)
        return false;
      int length1 = trophy.Objectives != null ? trophy.Objectives.Length : 0;
      int length2 = count != null ? count.Length : 0;
      int length3 = server.Count != null ? server.Count.Length : 0;
      if (length2 != length1 || length2 != length3)
        return false;
      bool flag = true;
      for (int index = 0; index < length2; ++index)
      {
        if (count[index] < trophy.Objectives[index].RequiredCount)
        {
          flag = false;
          break;
        }
      }
      bool send = false;
      if (ymd > server.StartYMD)
        send = true;
      else if (ymd >= server.StartYMD && (!server.IsEnded || !isEnded) && !server.IsEnded)
      {
        if (isEnded)
          send = true;
        else if ((!server.IsCompleted || !flag) && !server.IsCompleted)
        {
          if (flag)
          {
            send = true;
          }
          else
          {
            int num1 = 0;
            int num2 = 0;
            for (int index = 0; index < length2; ++index)
            {
              num1 += server.Count[index];
              num2 += count[index];
            }
            if (num1 < num2)
              send = true;
          }
        }
      }
      return send;
    }

    public void CreateUpdateTrophyList(
      out List<TrophyState> updateTrophyList,
      out List<TrophyState> updateChallengeList,
      out List<TrophyState> updateTrophyAward)
    {
      updateTrophyList = new List<TrophyState>();
      updateChallengeList = new List<TrophyState>();
      updateTrophyAward = new List<TrophyState>();
      List<TrophyState> trophyStatesList = this.Player.TrophyData.TrophyStatesList;
      for (int index = 0; index < trophyStatesList.Count; ++index)
      {
        if ((trophyStatesList[index].IsDirty || trophyStatesList[index].IsSending) && !trophyStatesList[index].IsEnded)
        {
          TrophyParam trophy = this.MasterParam.GetTrophy(trophyStatesList[index].iname);
          if (trophy != null)
          {
            if (trophy.IsChallengeMission)
              updateChallengeList.Add(trophyStatesList[index]);
            else if (trophyStatesList[index].Param.DispType == TrophyDispType.Award && trophyStatesList[index].IsCompleted)
              updateTrophyAward.Add(trophyStatesList[index]);
            else
              updateTrophyList.Add(trophyStatesList[index]);
          }
        }
      }
    }

    public bool IsExternalPermit()
    {
      return string.IsNullOrEmpty(FlowNode_Variable.Get("IS_EXTERNAL_API_PERMIT"));
    }

    private void UpdateTrophy()
    {
      if (SRPG.Network.Mode != SRPG.Network.EConnectMode.Online || !this.update_trophy_interval.PlayCheckUpdate() || this.update_trophy_lock.IsLock || !this.IsExternalPermit() || CriticalSection.IsActive || !NotifyList.hasInstance)
        return;
      this.update_trophy_interval.SetUpdateInterval();
      this.UpdateTrophyAPI();
    }

    public void UpdateTrophyAPI()
    {
      List<TrophyState> updateTrophyList;
      List<TrophyState> updateChallengeList;
      List<TrophyState> updateTrophyAward;
      this.CreateUpdateTrophyList(out updateTrophyList, out updateChallengeList, out updateTrophyAward);
      List<TrophyState> updateList = new List<TrophyState>(updateTrophyList.Count + updateChallengeList.Count + updateTrophyAward.Count);
      updateList.AddRange((IEnumerable<TrophyState>) updateTrophyList);
      updateList.AddRange((IEnumerable<TrophyState>) updateChallengeList);
      updateList.AddRange((IEnumerable<TrophyState>) updateTrophyAward);
      this.SaveUpdateTrophyList(updateList);
      if (updateList.Count <= 0)
        return;
      if (updateTrophyList.Count > 0)
      {
        this.mUpdateTrophyList = updateTrophyList;
        foreach (TrophyState mUpdateTrophy in this.mUpdateTrophyList)
        {
          mUpdateTrophy.IsDirty = false;
          mUpdateTrophy.IsSending = true;
        }
        SRPG.Network.RequestAPI((WebAPI) new ReqUpdateTrophy(this.mUpdateTrophyList, new SRPG.Network.ResponseCallback(this.UpdateTrophyResponseCallback), false, EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
      }
      if (updateChallengeList.Count > 0)
      {
        this.mUpdateChallengeList = updateChallengeList;
        foreach (TrophyState mUpdateChallenge in this.mUpdateChallengeList)
        {
          mUpdateChallenge.IsDirty = false;
          mUpdateChallenge.IsSending = true;
        }
        SRPG.Network.RequestAPI((WebAPI) new ReqUpdateBingo(this.mUpdateChallengeList, new SRPG.Network.ResponseCallback(this.UpdateChallengeResponseCallback), false, EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
      }
      if (updateTrophyAward.Count <= 0)
        return;
      this.mUpdateTrophyAward = updateTrophyAward;
      foreach (TrophyState trophyState in this.mUpdateTrophyAward)
      {
        trophyState.IsDirty = false;
        trophyState.IsSending = true;
      }
      SRPG.Network.RequestAPI((WebAPI) new ReqUpdateTrophy(this.mUpdateTrophyAward, new SRPG.Network.ResponseCallback(this.UpdateTrophyAwardResponseCallback), true, EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
    }

    private void UpdateTrophyResponseCallback(WWWResult www)
    {
      this.update_trophy_interval.SetSyncInterval();
      if (SRPG.Network.IsError)
      {
        if (SRPG.Network.ErrCode == SRPG.Network.EErrCode.TrophyStarMission_Future)
          FlowNode_Network.Failed();
        else
          FlowNode_Network.Retry();
      }
      else
      {
        foreach (TrophyState mUpdateTrophy in this.mUpdateTrophyList)
        {
          mUpdateTrophy.IsSending = false;
          if (mUpdateTrophy.IsCompleted)
          {
            if (mUpdateTrophy.Param.IsDaily)
              NotifyList.PushDailyTrophy(mUpdateTrophy.Param);
            else
              NotifyList.PushTrophy(mUpdateTrophy.Param);
          }
        }
        this.mUpdateTrophyList = (List<TrophyState>) null;
        this.RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
        SRPG.Network.RemoveAPI();
        Json_TrophyPlayerDataAll body;
        if (EncodingTypes.IsJsonSerializeCompressSelected(!GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK : GlobalVars.SelectedSerializeCompressMethod))
        {
          WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll>>(www.text);
          DebugUtility.Assert(jsonObject != null, "jsonRes == null");
          body = jsonObject.body;
        }
        else
        {
          FlowNode_ReqUpdateTrophy.MP_TrophyPlayerDataAllResponse playerDataAllResponse = SerializerCompressorHelper.Decode<FlowNode_ReqUpdateTrophy.MP_TrophyPlayerDataAllResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
          DebugUtility.Assert(playerDataAllResponse != null, "mpRes == null");
          body = playerDataAllResponse.body;
        }
        try
        {
          if (body == null)
            throw new Exception("ReqUpdateTrophy: illegal Server response!");
          if (!TrophyStarMissionParam.EntryTrophyStarMission(body.star_mission))
            throw new Exception("ReqUpdateTrophy: illegal StarMission information!");
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
        }
      }
    }

    private void UpdateChallengeResponseCallback(WWWResult www)
    {
      this.update_trophy_interval.SetSyncInterval();
      if (SRPG.Network.IsError)
      {
        if (SRPG.Network.ErrCode == SRPG.Network.EErrCode.BingoOutofDate || SRPG.Network.ErrCode == SRPG.Network.EErrCode.BingoRemainingChildren || SRPG.Network.ErrCode == SRPG.Network.EErrCode.BingoOutofDateReceive)
        {
          SRPG.Network.ResetError();
        }
        else
        {
          FlowNode_Network.Retry();
          return;
        }
      }
      foreach (TrophyState mUpdateChallenge in this.mUpdateChallengeList)
      {
        mUpdateChallenge.IsSending = false;
        if (mUpdateChallenge.IsCompleted)
        {
          if (mUpdateChallenge.Param.IsDaily)
            NotifyList.PushDailyTrophy(mUpdateChallenge.Param);
          else
            NotifyList.PushTrophy(mUpdateChallenge.Param);
        }
      }
      this.mUpdateChallengeList = (List<TrophyState>) null;
      SRPG.Network.RemoveAPI();
    }

    private void UpdateTrophyAwardResponseCallback(WWWResult www)
    {
      this.update_trophy_interval.SetSyncInterval();
      if (SRPG.Network.IsError)
      {
        if (SRPG.Network.ErrCode == SRPG.Network.EErrCode.TrophyStarMission_Future)
          FlowNode_Network.Failed();
        else
          FlowNode_Network.Retry();
      }
      else
      {
        foreach (TrophyState trophyState in this.mUpdateTrophyAward)
        {
          trophyState.IsSending = false;
          if (trophyState.IsCompleted)
            NotifyList.PushAward(trophyState.Param);
        }
        this.mUpdateTrophyAward = (List<TrophyState>) null;
        SRPG.Network.RemoveAPI();
        Json_TrophyPlayerDataAll body;
        if (EncodingTypes.IsJsonSerializeCompressSelected(!GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK : GlobalVars.SelectedSerializeCompressMethod))
        {
          WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll>>(www.text);
          DebugUtility.Assert(jsonObject != null, "jsonRes == null");
          body = jsonObject.body;
          if (body == null)
            return;
        }
        else
        {
          FlowNode_ReqUpdateTrophy.MP_TrophyPlayerDataAllResponse playerDataAllResponse = SerializerCompressorHelper.Decode<FlowNode_ReqUpdateTrophy.MP_TrophyPlayerDataAllResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
          DebugUtility.Assert(playerDataAllResponse != null, "mpRes == null");
          body = playerDataAllResponse.body;
          if (body != null)
          {
            if (body.concept_cards == null)
            {
              if (body.items == null)
              {
                if (body.player == null)
                {
                  if (body.units == null)
                    return;
                }
              }
            }
          }
        }
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(body.items);
          MonoSingleton<GameManager>.Instance.Deserialize(body.units);
          if (!TrophyStarMissionParam.EntryTrophyStarMission(body.star_mission))
            throw new Exception("ReqUpdateTrophy: illegal StarMission information!");
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
        }
      }
    }

    public void ServerSyncTrophyExecStart(out string trophy_progs, out string bingo_progs)
    {
      trophy_progs = string.Empty;
      bingo_progs = string.Empty;
      if (this.is_start_server_sync_trophy_exec)
      {
        DebugUtility.Log("ServerSyncTrophyExecBegin が連続で呼ばれています。");
      }
      else
      {
        this.is_start_server_sync_trophy_exec = true;
        MonoSingleton<GameManager>.Instance.update_trophy_lock.Lock();
        MonoSingleton<GameManager>.Instance.CreateUpdateTrophyList(out this.mServerSyncTrophyList, out this.mServerSyncChallengeList, out this.mServerSyncTrophyAward);
        if (0 >= this.mServerSyncTrophyList.Count + this.mServerSyncChallengeList.Count + this.mServerSyncTrophyAward.Count)
          return;
        if (0 < this.mServerSyncTrophyList.Count || 0 < this.mServerSyncTrophyAward.Count)
        {
          ReqUpdateTrophy reqUpdateTrophy = new ReqUpdateTrophy();
          reqUpdateTrophy.BeginTrophyReqString();
          reqUpdateTrophy.AddTrophyReqString(this.mServerSyncTrophyList, false);
          reqUpdateTrophy.AddTrophyReqString(this.mServerSyncTrophyAward, true);
          reqUpdateTrophy.EndTrophyReqString();
          trophy_progs = reqUpdateTrophy.GetTrophyReqString();
          foreach (TrophyState serverSyncTrophy in this.mServerSyncTrophyList)
          {
            serverSyncTrophy.IsDirty = false;
            serverSyncTrophy.IsSending = false;
          }
          foreach (TrophyState trophyState in this.mServerSyncTrophyAward)
          {
            trophyState.IsDirty = false;
            trophyState.IsSending = false;
          }
        }
        if (0 >= this.mServerSyncChallengeList.Count)
          return;
        ReqUpdateBingo reqUpdateBingo = new ReqUpdateBingo();
        reqUpdateBingo.BeginBingoReqString();
        reqUpdateBingo.AddBingoReqString(this.mServerSyncChallengeList, false);
        reqUpdateBingo.EndBingoReqString();
        reqUpdateBingo.serializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        if (GlobalVars.SelectedSerializeCompressMethodWasNodeSet)
          reqUpdateBingo.serializeCompressMethod = GlobalVars.SelectedSerializeCompressMethod;
        bingo_progs = reqUpdateBingo.GetBingoReqString();
        foreach (TrophyState serverSyncChallenge in this.mServerSyncChallengeList)
        {
          serverSyncChallenge.IsDirty = false;
          serverSyncChallenge.IsSending = false;
        }
      }
    }

    public void ServerSyncTrophyExecEnd(WWWResult www)
    {
      if (!this.is_start_server_sync_trophy_exec)
      {
        DebugUtility.Log("ServerSyncTrophyExecBegin が呼ばれていません。");
      }
      else
      {
        this.is_start_server_sync_trophy_exec = false;
        if (this.mServerSyncTrophyList == null || this.mServerSyncChallengeList == null || this.mServerSyncTrophyAward == null)
          return;
        MonoSingleton<GameManager>.Instance.update_trophy_interval.SetSyncInterval();
        List<TrophyState> updateList = new List<TrophyState>(this.mServerSyncTrophyList.Count + this.mServerSyncChallengeList.Count + this.mServerSyncTrophyAward.Count);
        updateList.AddRange((IEnumerable<TrophyState>) this.mServerSyncTrophyList);
        updateList.AddRange((IEnumerable<TrophyState>) this.mServerSyncChallengeList);
        updateList.AddRange((IEnumerable<TrophyState>) this.mServerSyncTrophyAward);
        MonoSingleton<GameManager>.Instance.SaveUpdateTrophyList(updateList);
        if (this.mServerSyncTrophyList.Count > 0)
        {
          foreach (TrophyState serverSyncTrophy in this.mServerSyncTrophyList)
          {
            serverSyncTrophy.IsDirty = false;
            serverSyncTrophy.IsSending = false;
            if (serverSyncTrophy.IsCompleted)
            {
              if (serverSyncTrophy.Param.IsDaily)
                NotifyList.PushDailyTrophy(serverSyncTrophy.Param);
              else
                NotifyList.PushTrophy(serverSyncTrophy.Param);
            }
          }
        }
        if (this.mServerSyncChallengeList.Count > 0)
        {
          foreach (TrophyState serverSyncChallenge in this.mServerSyncChallengeList)
          {
            serverSyncChallenge.IsDirty = false;
            serverSyncChallenge.IsSending = false;
            if (serverSyncChallenge.IsCompleted)
              NotifyList.PushTrophy(serverSyncChallenge.Param);
          }
        }
        if (this.mServerSyncTrophyAward.Count > 0)
        {
          foreach (TrophyState trophyState in this.mServerSyncTrophyAward)
          {
            trophyState.IsDirty = false;
            trophyState.IsSending = false;
            if (trophyState.IsCompleted)
              NotifyList.PushAward(trophyState.Param);
          }
          try
          {
            WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll>>(www.text);
            DebugUtility.Assert(jsonObject != null, "res == null");
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
        }
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
        this.mServerSyncTrophyList = (List<TrophyState>) null;
        this.mServerSyncChallengeList = (List<TrophyState>) null;
        this.mServerSyncTrophyAward = (List<TrophyState>) null;
        MonoSingleton<GameManager>.Instance.update_trophy_lock.Unlock();
      }
    }

    public void AddCharacterQuestPopup(UnitData unit)
    {
      if (unit == null)
        return;
      if (this.mCharacterQuestUnits == null)
        this.mCharacterQuestUnits = new List<UnitData>();
      else if (this.mCharacterQuestUnits.Exists((Predicate<UnitData>) (u => u.UnitID == unit.UnitID)))
        return;
      this.mCharacterQuestUnits.Add(unit);
    }

    public void ShowCharacterQuestPopup(string template)
    {
      this.StartCoroutine(this.ShowCharacterQuestPopupAsync(template));
    }

    [DebuggerHidden]
    public IEnumerator ShowCharacterQuestPopupAsync(string template)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CShowCharacterQuestPopupAsync\u003Ec__Iterator6()
      {
        template = template,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    public IEnumerator SkinUnlockPopup(ArtifactParam unlockSkin)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CSkinUnlockPopup\u003Ec__Iterator7()
      {
        unlockSkin = unlockSkin
      };
    }

    [DebuggerHidden]
    public IEnumerator SkinUnlockPopup(ItemParam[] rewardItems)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CSkinUnlockPopup\u003Ec__Iterator8()
      {
        rewardItems = rewardItems
      };
    }

    public long LimitedShopEndAt
    {
      get => this.mLimitedShopEndAt;
      set => this.mLimitedShopEndAt = value;
    }

    public JSON_ShopListArray.Shops[] LimitedShopList
    {
      get => this.mLimitedShopList;
      set => this.mLimitedShopList = value;
    }

    public bool IsLimitedShopOpen
    {
      set => this.mIsLimitedShopOpen = value;
      get => this.mIsLimitedShopOpen;
    }

    public void Deserialize(ReqMultiRank.Json_MultiRank json)
    {
      if (json == null)
        return;
      this.mMultiUnitRank = (List<MultiRanking>) null;
      this.mMultiUnitRank = new List<MultiRanking>();
      if (json.isReady != 1)
        return;
      for (int index = 0; index < json.ranking.Length; ++index)
        this.mMultiUnitRank.Add(new MultiRanking()
        {
          unit = json.ranking[index].unit_iname,
          job = json.ranking[index].job_iname
        });
    }

    public void Deserialize(RankingData[] datas)
    {
      if (this.mUnitRanking == null)
        this.mUnitRanking = new Dictionary<string, RankingData>();
      this.mUnitRanking.Clear();
      for (int index = 0; index < datas.Length; ++index)
      {
        if (datas[index] != null)
          this.mUnitRanking[datas[index].iname] = datas[index];
      }
    }

    public List<MultiRanking> MultiUnitRank => this.mMultiUnitRank;

    public Dictionary<string, RankingData> UnitRanking => this.mUnitRanking;

    public void CreateMatchingRange()
    {
      if (this.mFreeMatchRange != null)
        return;
      this.mFreeMatchRange = new GameManager.VersusRange[6]
      {
        new GameManager.VersusRange(1, 20),
        new GameManager.VersusRange(21, 40),
        new GameManager.VersusRange(41, 60),
        new GameManager.VersusRange(61, 84),
        new GameManager.VersusRange(85, 120),
        new GameManager.VersusRange(121, -1)
      };
    }

    public string GetVersusKey(VERSUS_TYPE type)
    {
      string versusKey = string.Empty;
      VersusMatchingParam[] versusMatchingParam = this.mMasterParam.GetVersusMatchingParam();
      switch (type)
      {
        case VERSUS_TYPE.Free:
          int num = this.Player.CalcLevel();
          this.CreateMatchingRange();
          for (int index = 0; index < this.mFreeMatchRange.Length; ++index)
          {
            if (num >= this.mFreeMatchRange[index].min && (this.mFreeMatchRange[index].max == -1 || num <= this.mFreeMatchRange[index].max))
            {
              versusKey = VersusMatchingParam.CalcHash("key" + string.Format("{0:D2}", (object) index));
              break;
            }
          }
          break;
        case VERSUS_TYPE.Tower:
          for (int index = 0; index < versusMatchingParam.Length; ++index)
          {
            if (versusMatchingParam[index].MatchKey == VersusMatchingParam.TOWER_KEY)
            {
              versusKey = versusMatchingParam[index].MatchKeyHash;
              break;
            }
          }
          break;
        case VERSUS_TYPE.Friend:
          for (int index = 0; index < versusMatchingParam.Length; ++index)
          {
            if (versusMatchingParam[index].MatchKey == VersusMatchingParam.FRIEND_KEY)
            {
              versusKey = versusMatchingParam[index].MatchKeyHash;
              break;
            }
          }
          break;
      }
      return versusKey;
    }

    public void GetTowerMatchItems(int floor, List<string> Items, List<int> Nums, bool bWin)
    {
      VersusTowerParam[] versusTowerParam = this.GetVersusTowerParam();
      if (versusTowerParam == null || floor < 0 || floor >= versusTowerParam.Length)
        return;
      if (bWin && versusTowerParam[floor].WinIteminame != null)
      {
        for (int index = 0; index < versusTowerParam[floor].WinIteminame.Length; ++index)
        {
          Items.Add((string) versusTowerParam[floor].WinIteminame[index]);
          Nums.Add((int) versusTowerParam[floor].WinItemNum[index]);
        }
      }
      else
      {
        if (versusTowerParam[floor].JoinIteminame == null)
          return;
        for (int index = 0; index < versusTowerParam[floor].JoinIteminame.Length; ++index)
        {
          Items.Add((string) versusTowerParam[floor].JoinIteminame[index]);
          Nums.Add((int) versusTowerParam[floor].JoinItemNum[index]);
        }
      }
    }

    public void GetVersusTopFloorItems(int floor, List<string> Items, List<int> Nums)
    {
      VersusTowerParam[] versusTowerParam = this.GetVersusTowerParam();
      if (versusTowerParam == null || floor < 0 || floor >= versusTowerParam.Length || versusTowerParam[floor].SpIteminame == null)
        return;
      for (int index = 0; index < versusTowerParam[floor].SpIteminame.Length; ++index)
      {
        Items.Add((string) versusTowerParam[floor].SpIteminame[index]);
        Nums.Add((int) versusTowerParam[floor].SpItemnum[index]);
      }
    }

    public VersusTowerParam[] GetVersusTowerParam() => this.mVersusTowerFloor.ToArray();

    public VersusTowerParam GetCurrentVersusTowerParam(int idx = -1)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int floor = idx == -1 ? player.VersusTowerFloor : idx;
      string iname = this.VersusTowerMatchName;
      return !string.IsNullOrEmpty(iname) ? this.mVersusTowerFloor.Find((Predicate<VersusTowerParam>) (data => (string) data.VersusTowerID == iname && (int) data.Floor == floor)) : (VersusTowerParam) null;
    }

    public void GetRankMatchCondition(out int lrange, out int frange)
    {
      lrange = -1;
      frange = -1;
      int versusTowerFloor = this.Player.VersusTowerFloor;
      VersusMatchCondParam[] matchingCondition = this.mMasterParam.GetVersusMatchingCondition();
      if (matchingCondition == null || versusTowerFloor < 0 || versusTowerFloor >= matchingCondition.Length)
        return;
      lrange = (int) matchingCondition[versusTowerFloor].LvRange;
      frange = (int) matchingCondition[versusTowerFloor].FloorRange;
    }

    public void InitAlterHash(string digest = null, string quest_digest = null)
    {
      if (string.IsNullOrEmpty(digest))
        return;
      this.DigestHash = digest;
      if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.ALTER_PREV_CHECK_HASH))
        this.PrevCheckHash = PlayerPrefsUtility.GetString(PlayerPrefsUtility.ALTER_PREV_CHECK_HASH, string.Empty);
      if (string.IsNullOrEmpty(quest_digest))
        return;
      this.QuestDigestHash = quest_digest;
    }

    public VersusScheduleParam FindVersusTowerScheduleParam(string towerID)
    {
      return string.IsNullOrEmpty(towerID) ? (VersusScheduleParam) null : this.mVersusScheduleParam.Find((Predicate<VersusScheduleParam>) (data => string.Equals(data.tower_iname, towerID)));
    }

    public bool ExistsOpenVersusTower(string towerID = null)
    {
      List<VersusScheduleParam> all = this.mVersusScheduleParam.FindAll((Predicate<VersusScheduleParam>) (data => data.IsOpen));
      if (all.Count < 1)
        return false;
      return string.IsNullOrEmpty(towerID) || all.Find((Predicate<VersusScheduleParam>) (data => string.Equals(data.tower_iname, towerID))) != null;
    }

    public VersusCoinParam GetVersusCoinParam(string iname)
    {
      return this.mVersusCoinParam != null ? this.mVersusCoinParam.Find((Predicate<VersusCoinParam>) (x => x.iname == iname)) : (VersusCoinParam) null;
    }

    public VersusFriendScore[] GetVersusFriendScore(int floor)
    {
      VersusTowerParam versusTowerParam = this.GetCurrentVersusTowerParam(floor);
      VersusFriendScore[] versusFriendInfo = this.VersusFriendInfo;
      List<VersusFriendScore> versusFriendScoreList = new List<VersusFriendScore>();
      if (versusTowerParam != null && versusFriendInfo != null)
      {
        for (int index = 0; index < versusFriendInfo.Length; ++index)
        {
          if (string.Compare((string) versusTowerParam.FloorName, versusFriendInfo[index].floor) == 0)
            versusFriendScoreList.Add(versusFriendInfo[index]);
        }
      }
      return versusFriendScoreList.ToArray();
    }

    public bool IsVersusMode()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      bool flag = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        flag &= instance.CurrentState == MyPhoton.MyState.ROOM;
      return flag & GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.VERSUS & GlobalVars.SelectedMultiPlayVersusType != VERSUS_TYPE.Friend;
    }

    public List<MultiTowerFloorParam> GetMTAllFloorParam(string type)
    {
      List<MultiTowerFloorParam> mtAllFloorParam = new List<MultiTowerFloorParam>();
      for (int index = 0; index < this.mMultiTowerFloor.Count; ++index)
      {
        if (this.mMultiTowerFloor[index].tower_id == type)
          mtAllFloorParam.Add(this.mMultiTowerFloor[index]);
      }
      return mtAllFloorParam;
    }

    public MultiTowerFloorParam GetMTFloorParam(string type, int floor)
    {
      return this.mMultiTowerFloor.Find((Predicate<MultiTowerFloorParam>) (data => (int) data.floor == floor && data.tower_id == type));
    }

    public MultiTowerFloorParam GetMTFloorParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (MultiTowerFloorParam) null;
      int length = iname.LastIndexOf('_');
      int result = -1;
      return int.TryParse(iname.Substring(length + 1), out result) ? this.GetMTFloorParam(iname.Substring(0, length), result) : (MultiTowerFloorParam) null;
    }

    public MultiTowerFloorParam FindMultiTowerFloorParam(string iname)
    {
      return this.mMultiTowerFloor.Find((Predicate<MultiTowerFloorParam>) (data => data.iname_text == iname));
    }

    public List<MultiTowerRewardItem> GetMTFloorReward(string iname, int round)
    {
      return this.mMultiTowerRewards.Find((Predicate<MultiTowerRewardParam>) (data => data.iname == iname))?.GetReward(round);
    }

    public int GetMTRound(int floor)
    {
      int index = floor - 1;
      return this.mMultiTowerRound == null || this.mMultiTowerRound.Round == null || this.mMultiTowerRound.Round.Count <= index ? 1 : this.mMultiTowerRound.Round[index] + 1;
    }

    public int GetMTClearedMaxFloor()
    {
      return this.mMultiTowerRound == null ? 0 : this.mMultiTowerRound.Now;
    }

    public int GetMTChallengeFloor()
    {
      if (this.mMultiTowerRound == null)
        return 1;
      List<MultiTowerFloorParam> mtAllFloorParam = this.GetMTAllFloorParam(GlobalVars.SelectedMultiTowerID);
      int mtChallengeFloor = this.mMultiTowerRound.Now + 1;
      if (mtAllFloorParam != null && mtAllFloorParam.Count > 0)
        mtChallengeFloor = Mathf.Clamp(mtChallengeFloor, 1, (int) mtAllFloorParam[mtAllFloorParam.Count - 1].floor);
      return mtChallengeFloor;
    }

    public void AddMTQuest(string iname, QuestParam param)
    {
      if (this.mQuests == null || this.mQuestsDict == null || this.mQuestsDict.ContainsKey(iname))
        return;
      this.mQuests.Add(param);
      this.mQuestsDict.Add(iname, param);
    }

    public List<int> GetMTSkipFloorList()
    {
      List<int> mtSkipFloorList = new List<int>();
      int mtClearedMaxFloor = this.GetMTClearedMaxFloor();
      List<MultiTowerFloorParam> mtAllFloorParam = this.GetMTAllFloorParam(GlobalVars.SelectedMultiTowerID);
      for (int index = 0; index < mtAllFloorParam.Count; ++index)
      {
        MultiTowerFloorParam multiTowerFloorParam = mtAllFloorParam[index];
        if (multiTowerFloorParam.is_skip && (int) multiTowerFloorParam.floor > mtClearedMaxFloor && (int) multiTowerFloorParam.floor <= this.MaxClearFloor)
          mtSkipFloorList.Add((int) multiTowerFloorParam.floor);
      }
      return mtSkipFloorList;
    }

    public bool IsMTCanSkip() => this.GetMTSkipFloorList().Count != 0;

    public void Deserialize(ReqMultiTwStatus.FloorParam[] param)
    {
      if (this.mMultiTowerRound == null)
        this.mMultiTowerRound = new MultiTowerRoundParam();
      this.mMultiTowerRound.Now = 0;
      if (this.mMultiTowerRound.Round == null)
        this.mMultiTowerRound.Round = new List<int>();
      this.mMultiTowerRound.Round.Clear();
      if (param == null)
        return;
      for (int index = 0; index < param.Length; ++index)
        this.mMultiTowerRound.Round.Add(param[index].clear_count);
      this.mMultiTowerRound.Now = param[param.Length - 1].floor;
    }

    public void Deserialize(ReqMultiTwStatus.Response param)
    {
      if (param == null)
        return;
      this.Deserialize(param.floors);
      this.MaxClearFloor = param.max_clear_floor;
    }

    public SRPG.MapEffectParam GetMapEffectParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (SRPG.MapEffectParam) null;
      if (this.mMapEffectParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetMapEffectParam no data!</color>"));
        return (SRPG.MapEffectParam) null;
      }
      SRPG.MapEffectParam mapEffectParam = this.mMapEffectParam.Find((Predicate<SRPG.MapEffectParam>) (d => d.Iname == iname));
      if (mapEffectParam == null)
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetMapEffectParam data not found! iname={0}</color>", (object) iname));
      return mapEffectParam;
    }

    public WeatherSetParam GetWeatherSetParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (WeatherSetParam) null;
      if (this.mWeatherSetParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetWeatherSetParam no data!</color>"));
        return (WeatherSetParam) null;
      }
      WeatherSetParam weatherSetParam = this.mWeatherSetParam.Find((Predicate<WeatherSetParam>) (d => d.Iname == iname));
      if (weatherSetParam == null)
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetWeatherSetParam data not found! iname={0}</color>", (object) iname));
      return weatherSetParam;
    }

    public GenesisChapterParam GetGenesisChapterParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (GenesisChapterParam) null;
      if (this.mGenesisChapterParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetGenesisChapterParam no data!</color>"));
        return (GenesisChapterParam) null;
      }
      GenesisChapterParam genesisChapterParam = this.mGenesisChapterParam.Find((Predicate<GenesisChapterParam>) (d => d.Iname == iname));
      if (genesisChapterParam == null)
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetGenesisChapterParam data not found! iname={0}</color>", (object) iname));
      return genesisChapterParam;
    }

    public GenesisChapterParam GetGenesisChapterParamFromAreaId(string area_id)
    {
      if (string.IsNullOrEmpty(area_id))
        return (GenesisChapterParam) null;
      if (this.mGenesisChapterParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetGenesisChapterParam no data!</color>"));
        return (GenesisChapterParam) null;
      }
      GenesisChapterParam chapterParamFromAreaId = this.mGenesisChapterParam.Find((Predicate<GenesisChapterParam>) (d => d.AreaId == area_id));
      if (chapterParamFromAreaId == null)
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetGenesisChapterParam data not found! area_id={0}</color>", (object) area_id));
      return chapterParamFromAreaId;
    }

    public GenesisStarParam GetGenesisStarParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (GenesisStarParam) null;
      if (this.mGenesisStarParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetGenesisStarParam no data!</color>"));
        return (GenesisStarParam) null;
      }
      GenesisStarParam genesisStarParam = this.mGenesisStarParam.Find((Predicate<GenesisStarParam>) (d => d.Iname == iname));
      if (genesisStarParam == null)
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetGenesisStarParam data not found! iname={0}</color>", (object) iname));
      return genesisStarParam;
    }

    public GenesisRewardParam GetGenesisRewardParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (GenesisRewardParam) null;
      if (this.mGenesisRewardParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetGenesisRewardParam no data!</color>"));
        return (GenesisRewardParam) null;
      }
      GenesisRewardParam genesisRewardParam = this.mGenesisRewardParam.Find((Predicate<GenesisRewardParam>) (d => d.Iname == iname));
      if (genesisRewardParam == null)
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetGenesisRewardParam data not found! iname={0}</color>", (object) iname));
      return genesisRewardParam;
    }

    public GenesisLapBossParam GetGenesisLapBossParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (GenesisLapBossParam) null;
      if (this.mGenesisLapBossParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetGenesisLapBossParam no data!</color>"));
        return (GenesisLapBossParam) null;
      }
      GenesisLapBossParam genesisLapBossParam = this.mGenesisLapBossParam.Find((Predicate<GenesisLapBossParam>) (d => d.Iname == iname));
      if (genesisLapBossParam == null)
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetGenesisLapBossParam data not found! iname={0}</color>", (object) iname));
      return genesisLapBossParam;
    }

    public AdvanceEventParam GetAdvanceEventParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (AdvanceEventParam) null;
      if (this.mAdvanceEventParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetAdvanceEventParam no data!</color>"));
        return (AdvanceEventParam) null;
      }
      AdvanceEventParam advanceEventParam = this.mAdvanceEventParam.Find((Predicate<AdvanceEventParam>) (d => d.Iname == iname));
      if (advanceEventParam == null)
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetAdvanceEventParam data not found! iname={0}</color>", (object) iname));
      return advanceEventParam;
    }

    public AdvanceEventParam GetAdvanceEventParamFromAreaId(string area_id)
    {
      if (string.IsNullOrEmpty(area_id))
        return (AdvanceEventParam) null;
      if (this.mAdvanceEventParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetAdvanceEventParam no data!</color>"));
        return (AdvanceEventParam) null;
      }
      AdvanceEventParam eventParamFromAreaId = this.mAdvanceEventParam.Find((Predicate<AdvanceEventParam>) (d => d.AreaId == area_id));
      if (eventParamFromAreaId == null)
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetAdvanceEventParam data not found! area_id={0}</color>", (object) area_id));
      return eventParamFromAreaId;
    }

    public AdvanceStarParam GetAdvanceStarParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (AdvanceStarParam) null;
      if (this.mAdvanceStarParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetAdvanceStarParam no data!</color>"));
        return (AdvanceStarParam) null;
      }
      AdvanceStarParam advanceStarParam = this.mAdvanceStarParam.Find((Predicate<AdvanceStarParam>) (d => d.Iname == iname));
      if (advanceStarParam == null)
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetAdvanceStarParam data not found! iname={0}</color>", (object) iname));
      return advanceStarParam;
    }

    public AdvanceRewardParam GetAdvanceRewardParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (AdvanceRewardParam) null;
      if (this.mAdvanceRewardParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetAdvanceRewardParam no data!</color>"));
        return (AdvanceRewardParam) null;
      }
      AdvanceRewardParam advanceRewardParam = this.mAdvanceRewardParam.Find((Predicate<AdvanceRewardParam>) (d => d.Iname == iname));
      if (advanceRewardParam == null)
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetAdvanceRewardParam data not found! iname={0}</color>", (object) iname));
      return advanceRewardParam;
    }

    public AdvanceLapBossParam GetAdvanceLapBossParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (AdvanceLapBossParam) null;
      if (this.mAdvanceLapBossParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetAdvanceLapBossParam no data!</color>"));
        return (AdvanceLapBossParam) null;
      }
      AdvanceLapBossParam advanceLapBossParam = this.mAdvanceLapBossParam.Find((Predicate<AdvanceLapBossParam>) (d => d.Iname == iname));
      if (advanceLapBossParam == null)
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetAdvanceLapBossParam data not found! iname={0}</color>", (object) iname));
      return advanceLapBossParam;
    }

    public GuildRaidPeriodParam GetNowScheduleGuildRaidPeriodParam()
    {
      if (this.mGuildRaidPeriodParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGuildRaidPeriodParam no data!</color>"));
        return (GuildRaidPeriodParam) null;
      }
      DateTime serverTime = TimeManager.ServerTime;
      for (int index = 0; index < this.mGuildRaidPeriodParam.Count; ++index)
      {
        if (this.mGuildRaidPeriodParam[index].BeginAt <= serverTime && serverTime < this.mGuildRaidPeriodParam[index].EndAt)
          return this.mGuildRaidPeriodParam[index];
      }
      DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetGuildRaidPeriodParam data not Schedule!"));
      return (GuildRaidPeriodParam) null;
    }

    public GuildRaidPeriodParam GetActiveGuildRaidRankingPeriod()
    {
      if (this.mGuildRaidPeriodParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGuildRaidPeriodParam no data!</color>"));
        return (GuildRaidPeriodParam) null;
      }
      DateTime serverTime = TimeManager.ServerTime;
      for (int index = 0; index < this.mGuildRaidPeriodParam.Count; ++index)
      {
        if (this.mGuildRaidPeriodParam[index].RewardRankingBeginAt <= serverTime && serverTime <= this.mGuildRaidPeriodParam[index].RewardRankingEndAt)
          return this.mGuildRaidPeriodParam[index];
      }
      return (GuildRaidPeriodParam) null;
    }

    public GuildRaidPeriodParam GetActiveGuildRaidRewardPeriod()
    {
      if (this.mGuildRaidPeriodParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGuildRaidPeriodParam no data!</color>"));
        return (GuildRaidPeriodParam) null;
      }
      DateTime serverTime = TimeManager.ServerTime;
      for (int index = 0; index < this.mGuildRaidPeriodParam.Count; ++index)
      {
        if (this.mGuildRaidPeriodParam[index].BeginAt <= serverTime && serverTime <= this.mGuildRaidPeriodParam[index].RewardEndAt)
          return this.mGuildRaidPeriodParam[index];
      }
      return (GuildRaidPeriodParam) null;
    }

    public GuildRaidPeriodParam GetGuildRaidPeriodParam(int id)
    {
      if (this.mGuildRaidPeriodParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGuildRaidPeriodParam no data!</color>"));
        return (GuildRaidPeriodParam) null;
      }
      GuildRaidPeriodParam guildRaidPeriodParam = this.mGuildRaidPeriodParam.Find((Predicate<GuildRaidPeriodParam>) (d => d.Id == id));
      if (guildRaidPeriodParam == null)
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetGuildRaidPeriodParam data not found! id={0}</color>", (object) id.ToString()));
      return guildRaidPeriodParam;
    }

    public GuildRaidManager.GuildRaidScheduleType GetGuildRaidPeriodScheduleType()
    {
      GuildRaidPeriodParam guildRaidPeriodParam = this.GetNowScheduleGuildRaidPeriodParam();
      return guildRaidPeriodParam == null ? GuildRaidManager.GuildRaidScheduleType.Close : this.GetGuildRaidPeriodScheduleType(guildRaidPeriodParam.Id);
    }

    public GuildRaidManager.GuildRaidScheduleType GetGuildRaidPeriodScheduleType(int id)
    {
      if (this.mGuildRaidPeriodParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGuildRaidPeriodParam no data!</color>"));
        return GuildRaidManager.GuildRaidScheduleType.Close;
      }
      GuildRaidPeriodParam guildRaidPeriodParam = this.mGuildRaidPeriodParam.Find((Predicate<GuildRaidPeriodParam>) (d => d.Id == id));
      if (guildRaidPeriodParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetGuildRaidPeriodParam data not found! id={0}</color>", (object) id.ToString()));
        return GuildRaidManager.GuildRaidScheduleType.Close;
      }
      GuildRaidManager.GuildRaidScheduleType periodScheduleType = guildRaidPeriodParam.ConfirmScheduleType();
      switch (periodScheduleType)
      {
        case GuildRaidManager.GuildRaidScheduleType.Open:
        case GuildRaidManager.GuildRaidScheduleType.OpenSchedule:
          GuildRaidCoolDaysParam raidCoolDaysParam = this.mGuildRaidCoolDaysParam.Find((Predicate<GuildRaidCoolDaysParam>) (d => d.PeriodId == id));
          if (raidCoolDaysParam != null && raidCoolDaysParam.GetCoolDays())
            return GuildRaidManager.GuildRaidScheduleType.CloseSchedule;
          break;
      }
      return periodScheduleType;
    }

    public GuildRaidRewardParam GetGuildRaidRewardParam(string id)
    {
      if (this.mGuildRaidRewardParam != null)
        return this.mGuildRaidRewardParam.Find((Predicate<GuildRaidRewardParam>) (d => d.Id == id));
      DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGuildRaidRewardParam no data!</color>"));
      return (GuildRaidRewardParam) null;
    }

    public List<GuildRaidReward> GetGuildRaidRewards(string id)
    {
      if (this.mGuildRaidRewardParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGuildRaidRewardParam no data!</color>"));
        return (List<GuildRaidReward>) null;
      }
      return this.mGuildRaidRewardParam.Find((Predicate<GuildRaidRewardParam>) (d => d.Id == id))?.Rewards;
    }

    public GuildRaidRewardParam GetGuildRaidRewardRound(string id, int round)
    {
      if (this.mGuildRaidRewardRoundParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGuildRaidRewardRoundParam no data!</color>"));
        return (GuildRaidRewardParam) null;
      }
      GuildRaidRewardRoundParam rewardRoundParam = this.mGuildRaidRewardRoundParam.Find((Predicate<GuildRaidRewardRoundParam>) (d => d.Id == id));
      if (rewardRoundParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGuildRaidRewardRoundParam not found id:{0} </color>", (object) id));
        return (GuildRaidRewardParam) null;
      }
      GuildRaidRewardParam guildRaidRewardRound = (GuildRaidRewardParam) null;
      for (int index = 0; index < rewardRoundParam.Reward.Count; ++index)
      {
        if (rewardRoundParam.Reward[index] != null)
        {
          if (rewardRoundParam.Reward[index].Round == round)
            return this.GetGuildRaidRewardParam(rewardRoundParam.Reward[index].RewardId);
          guildRaidRewardRound = this.GetGuildRaidRewardParam(rewardRoundParam.Reward[index].RewardId);
        }
      }
      DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGuildRaidRewardRoundParam not found round:{0} </color>", (object) round.ToString()));
      return guildRaidRewardRound;
    }

    public GuildRaidRewardDmgRankingParam GetGuildRaidRewardDmgRanking(string id)
    {
      if (this.mGuildRaidRewardRoundParam != null)
        return this.mGuildRaidRewardDmgRankingParam.Find((Predicate<GuildRaidRewardDmgRankingParam>) (d => d.Id == id));
      DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGuildRaidRewardDmgRankingParam no data!</color>"));
      return (GuildRaidRewardDmgRankingParam) null;
    }

    public GuildRaidRewardParam GetGuildRaidRewardDmgRankingReward(
      string id,
      int ranking,
      int round)
    {
      GuildRaidRewardDmgRankingParam rewardDmgRankingParam = this.GetGuildRaidRewardDmgRanking(id);
      if (rewardDmgRankingParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGuildRaidRewardDmgRankingParam not found id:{0} </color>", (object) id));
        return (GuildRaidRewardParam) null;
      }
      int rank = -1;
      for (int index = 0; index < rewardDmgRankingParam.Ranking.Count; ++index)
      {
        if (rewardDmgRankingParam.Ranking[index].RankStart <= rank && rank <= rewardDmgRankingParam.Ranking[index].RankEnd)
        {
          rank = index;
          break;
        }
      }
      if (rank == -1)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGuildRaidRewardDmgRankingParam rank over </color>"));
        return (GuildRaidRewardParam) null;
      }
      if (rank == rewardDmgRankingParam.Ranking.Count)
        return (GuildRaidRewardParam) null;
      GuildRaidRewardRoundParam rewardRoundParam = this.mGuildRaidRewardRoundParam.Find((Predicate<GuildRaidRewardRoundParam>) (d => d.Id == rewardDmgRankingParam.Ranking[rank].RewardRoundId));
      if (rewardRoundParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGuildRaidRewardRoundParam not found id:{0} </color>", (object) id));
        return (GuildRaidRewardParam) null;
      }
      GuildRaidRewardParam dmgRankingReward = (GuildRaidRewardParam) null;
      for (int index = 0; index < rewardRoundParam.Reward.Count; ++index)
      {
        if (rewardRoundParam.Reward[index] != null)
        {
          if (rewardRoundParam.Reward[index].Round == round)
            return this.GetGuildRaidRewardParam(rewardRoundParam.Reward[index].RewardId);
          dmgRankingReward = this.GetGuildRaidRewardParam(rewardRoundParam.Reward[index].RewardId);
        }
      }
      DebugUtility.Log(string.Format("<color=yellow>QuestParam/GuildRaidRewardRoundParam not found round:{0} </color>", (object) round.ToString()));
      return dmgRankingReward;
    }

    public GuildRaidRewardParam GetGuildRaidRewardDmgRatio(string id, int ratio, int round)
    {
      if (this.mGuildRaidRewardRoundParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGuildRaidRewardRoundParam no data!</color>"));
        return (GuildRaidRewardParam) null;
      }
      GuildRaidRewardDmgRatioParam rewardDmgRatioParam = this.mGuildRaidRewardDmgRatioParam.Find((Predicate<GuildRaidRewardDmgRatioParam>) (d => d.Id == id));
      if (rewardDmgRatioParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGuildRaidRewardRoundParam not found id:{0} </color>", (object) id));
        return (GuildRaidRewardParam) null;
      }
      GuildRaidRewardDmgRatio guildRaidRewardDmgRatio = rewardDmgRatioParam.Reward.Find((Predicate<GuildRaidRewardDmgRatio>) (d => d.DatageRatio == ratio));
      if (guildRaidRewardDmgRatio == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>guildRaidRewardDmgRatio not found id:{0} </color>", (object) ratio));
        return (GuildRaidRewardParam) null;
      }
      GuildRaidRewardRoundParam rewardRoundParam = this.mGuildRaidRewardRoundParam.Find((Predicate<GuildRaidRewardRoundParam>) (d => d.Id == guildRaidRewardDmgRatio.RewardRoundId));
      if (rewardRoundParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGuildRaidRewardRoundParam not found id:{0} </color>", (object) id));
        return (GuildRaidRewardParam) null;
      }
      GuildRaidRewardParam raidRewardDmgRatio = (GuildRaidRewardParam) null;
      for (int index = 0; index < rewardRoundParam.Reward.Count; ++index)
      {
        if (rewardRoundParam.Reward[index] != null)
        {
          if (rewardRoundParam.Reward[index].Round == round)
            return this.GetGuildRaidRewardParam(rewardRoundParam.Reward[index].RewardId);
          raidRewardDmgRatio = this.GetGuildRaidRewardParam(rewardRoundParam.Reward[index].RewardId);
        }
      }
      DebugUtility.Log(string.Format("<color=yellow>QuestParam/GuildRaidRewardRoundParam not found round:{0} </color>", (object) round.ToString()));
      return raidRewardDmgRatio;
    }

    public GuildRaidBossParam GetGuildRaidBossParam(int boss_id)
    {
      return this.mGuildRaidBossParam == null ? (GuildRaidBossParam) null : this.mGuildRaidBossParam.Find((Predicate<GuildRaidBossParam>) (rbg => rbg.Id == boss_id));
    }

    public List<GuildRaidBossParam> GetGuildRaidBossByPeriod(int period_id)
    {
      return this.mGuildRaidBossParam == null ? new List<GuildRaidBossParam>() : this.mGuildRaidBossParam.FindAll((Predicate<GuildRaidBossParam>) (grb => grb.PeriodId == period_id));
    }

    public int GetGuildRaidBossCountByPeriod(int period_id)
    {
      return this.mGuildRaidBossParam == null ? 0 : this.mGuildRaidBossParam.Count<GuildRaidBossParam>((Func<GuildRaidBossParam, bool>) (grb => grb.PeriodId == period_id));
    }

    public int GetGuildRaidBossIndex(int periodId, int bossId)
    {
      if (this.mGuildRaidBossParam == null)
        return 1;
      GuildRaidBossParam guildRaidBossParam = this.mGuildRaidBossParam.Find((Predicate<GuildRaidBossParam>) (grb => grb.Id == bossId));
      return guildRaidBossParam == null ? 0 : guildRaidBossParam.AreaNo;
    }

    public bool IsGuildRaidBossHpWarning(int boss_id, int hp)
    {
      if (this.mGuildRaidBossParam == null)
        return false;
      GuildRaidBossParam guildRaidBossParam = this.mGuildRaidBossParam.Find((Predicate<GuildRaidBossParam>) (rbg => rbg.Id == boss_id));
      return guildRaidBossParam != null && guildRaidBossParam.HPWarning >= hp;
    }

    public GuildRaidBossParam GetGuildRaidBossParam(int periodid, int area_no)
    {
      return this.mGuildRaidBossParam == null ? (GuildRaidBossParam) null : this.mGuildRaidBossParam.Find((Predicate<GuildRaidBossParam>) (rbg => rbg.PeriodId == periodid && rbg.AreaNo == area_no));
    }

    public List<GuildRaidBossParam> GetRaidBossParamAll(int period_id)
    {
      return this.mGuildRaidBossParam == null ? new List<GuildRaidBossParam>() : this.mGuildRaidBossParam.FindAll((Predicate<GuildRaidBossParam>) (rbp => rbp.PeriodId == period_id));
    }

    public List<GuildRaidReward> GetRaidRewardParamList(string reward_id)
    {
      if (this.mGuildRaidRewardParam == null)
        return new List<GuildRaidReward>();
      GuildRaidRewardParam guildRaidRewardParam = this.mGuildRaidRewardParam.Find((Predicate<GuildRaidRewardParam>) (rr => rr.Id == reward_id));
      return guildRaidRewardParam == null ? new List<GuildRaidReward>() : guildRaidRewardParam.Rewards;
    }

    public int GetGuildRaidBossScore(int scoreId, int round, int damage)
    {
      if (this.mGuildRaidScoreParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GuildRaidScoreParam not found"));
        return damage;
      }
      GuildRaidScoreParam guildRaidScoreParam = this.mGuildRaidScoreParam.Find((Predicate<GuildRaidScoreParam>) (rr => rr.Id == scoreId));
      if (guildRaidScoreParam != null)
        return guildRaidScoreParam.GetScore(round, damage);
      DebugUtility.Log(string.Format("<color=yellow>QuestParam/GuildRaidScoreParam not found Id{0}", (object) scoreId));
      return damage;
    }

    public GuildRaidRewardRankingParam GetGuildRaidRewardRankingParamId(string id)
    {
      if (this.mGuildRaidRewardRankingParam != null)
        return this.mGuildRaidRewardRankingParam.Find((Predicate<GuildRaidRewardRankingParam>) (rr => rr.Id == id));
      DebugUtility.Log(string.Format("<color=yellow>QuestParam/GuildRaidRewardRankingParam not found"));
      return (GuildRaidRewardRankingParam) null;
    }

    public string GetGuildRaidRewardRankingParam(string id, int rank)
    {
      if (this.mGuildRaidRewardRankingParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GuildRaidRewardRankingParam not found"));
        return (string) null;
      }
      GuildRaidRewardRankingParam rewardRankingParam = this.mGuildRaidRewardRankingParam.Find((Predicate<GuildRaidRewardRankingParam>) (rr => rr.Id == id));
      if (rewardRankingParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GuildRaidRewardRoundParam not found id:{0} </color>", (object) id));
        return (string) null;
      }
      for (int index = 0; index < rewardRankingParam.Ranking.Count; ++index)
      {
        if (rewardRankingParam.Ranking[index].RankStart <= rank && rank <= rewardRankingParam.Ranking[index].RankEnd)
          return rewardRankingParam.Ranking[index].RewardId;
      }
      return (string) null;
    }

    public bool IsGuildRaidBattleSchedule(int id)
    {
      if (this.mGuildRaidPeriodParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetGuildRaidPeriodParam data not found!</color>"));
        return false;
      }
      GuildRaidPeriodParam guildRaidPeriodParam = this.mGuildRaidPeriodParam.Find((Predicate<GuildRaidPeriodParam>) (d => d.Id == id));
      if (guildRaidPeriodParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetGuildRaidPeriodParam data not found! id={0}</color>", (object) id.ToString()));
        return false;
      }
      DateTime serverTime = TimeManager.ServerTime;
      return guildRaidPeriodParam.MainBeginAt <= serverTime && serverTime <= guildRaidPeriodParam.EndAt;
    }

    public RankingQuestParam FindAvailableRankingQuest(string iname)
    {
      return this.mAvailableRankingQuesstParams.Find((Predicate<RankingQuestParam>) (param => param.iname == iname));
    }

    public List<string> GetMultiAreaQuestList()
    {
      List<string> multiAreaQuestList = new List<string>();
      for (int index = 0; index < this.mQuests.Count; ++index)
      {
        QuestParam mQuest = this.mQuests[index];
        if (mQuest != null && mQuest.gps_enable && mQuest.IsMultiAreaQuest)
          multiAreaQuestList.Add(mQuest.iname);
      }
      return multiAreaQuestList;
    }

    public bool IsValidAreaQuest()
    {
      bool flag = false;
      for (int index = 0; index < this.mQuests.Count; ++index)
      {
        QuestParam mQuest = this.mQuests[index];
        if (mQuest != null && mQuest.IsGps)
          flag |= mQuest.IsDateUnlock();
      }
      return flag;
    }

    public bool IsValidMultiAreaQuest()
    {
      bool flag = false;
      for (int index = 0; index < this.mQuests.Count; ++index)
      {
        QuestParam mQuest = this.mQuests[index];
        if (mQuest != null && mQuest.IsMultiAreaQuest)
          flag |= mQuest.IsDateUnlock();
      }
      return flag;
    }

    public VersusFirstWinBonusParam GetVSFirstWinBonus(long servertime = -1)
    {
      VersusFirstWinBonusParam vsFirstWinBonus = (VersusFirstWinBonusParam) null;
      DateTime dateTime = servertime != -1L ? TimeManager.FromUnixTime(servertime) : TimeManager.ServerTime;
      for (int index = 0; index < this.mVersusFirstWinBonus.Count; ++index)
      {
        VersusFirstWinBonusParam versusFirstWinBonu = this.mVersusFirstWinBonus[index];
        if (dateTime >= versusFirstWinBonu.begin_at && dateTime <= versusFirstWinBonu.end_at)
        {
          vsFirstWinBonus = versusFirstWinBonu;
          break;
        }
      }
      return vsFirstWinBonus;
    }

    public int GetVSStreakSchedule(STREAK_JUDGE judge, DateTime time)
    {
      int vsStreakSchedule = -1;
      for (int index = 0; index < this.mVersusStreakSchedule.Count; ++index)
      {
        VersusStreakWinScheduleParam winScheduleParam = this.mVersusStreakSchedule[index];
        if (winScheduleParam.judge == judge && time >= winScheduleParam.begin_at && time <= winScheduleParam.end_at)
        {
          vsStreakSchedule = winScheduleParam.id;
          break;
        }
      }
      return vsStreakSchedule;
    }

    public VersusStreakWinBonusParam GetVSStreakWinBonus(
      int streakcnt,
      STREAK_JUDGE judge,
      long servertime = -1)
    {
      VersusStreakWinBonusParam vsStreakWinBonus = (VersusStreakWinBonusParam) null;
      DateTime time = servertime != -1L ? TimeManager.FromUnixTime(servertime) : TimeManager.ServerTime;
      int vsStreakSchedule = this.GetVSStreakSchedule(judge, time);
      if (vsStreakSchedule != -1)
      {
        for (int index = 0; index < this.mVersusStreakWinBonus.Count; ++index)
        {
          VersusStreakWinBonusParam versusStreakWinBonu = this.mVersusStreakWinBonus[index];
          if (versusStreakWinBonu.id == vsStreakSchedule && versusStreakWinBonu.wincnt == streakcnt)
          {
            vsStreakWinBonus = versusStreakWinBonu;
            break;
          }
        }
      }
      return vsStreakWinBonus;
    }

    public VS_MODE GetVSMode(long servertime = -1)
    {
      VS_MODE vsMode = VS_MODE.THREE_ON_THREE;
      DateTime dateTime = servertime != -1L ? TimeManager.FromUnixTime(servertime) : TimeManager.ServerTime;
      for (int index = 0; index < this.mVersusRule.Count; ++index)
      {
        VersusRuleParam versusRuleParam = this.mVersusRule[index];
        if (dateTime >= versusRuleParam.begin_at && dateTime <= versusRuleParam.end_at)
        {
          vsMode = versusRuleParam.mode;
          break;
        }
      }
      return vsMode;
    }

    public int GetVSGetCoinRate(long servertime = -1)
    {
      int vsGetCoinRate = 1;
      DateTime dateTime = servertime != -1L ? TimeManager.FromUnixTime(servertime) : TimeManager.ServerTime;
      for (int index = 0; index < this.mVersusCoinCamp.Count; ++index)
      {
        VersusCoinCampParam versusCoinCampParam = this.mVersusCoinCamp[index];
        if (dateTime >= versusCoinCampParam.begin_at && dateTime <= versusCoinCampParam.end_at)
        {
          vsGetCoinRate = versusCoinCampParam.coinrate;
          break;
        }
      }
      return vsGetCoinRate;
    }

    public STREAK_JUDGE SearchVersusJudgeType(int id, long servertime = -1)
    {
      STREAK_JUDGE streakJudge = STREAK_JUDGE.None;
      DateTime dateTime = servertime != -1L ? TimeManager.FromUnixTime(servertime) : TimeManager.ServerTime;
      for (int index = 0; index < this.mVersusStreakSchedule.Count; ++index)
      {
        VersusStreakWinScheduleParam winScheduleParam = this.mVersusStreakSchedule[index];
        if (winScheduleParam.id == id && dateTime >= winScheduleParam.begin_at && dateTime <= winScheduleParam.end_at)
        {
          streakJudge = winScheduleParam.judge;
          break;
        }
      }
      return streakJudge;
    }

    public bool Deserialize(Json_VersusCpu json)
    {
      this.mVersusCpuData.Clear();
      if (json.comenemies != null)
      {
        for (int index = json.comenemies.Length - 1; index >= 0; --index)
        {
          SRPG.VersusCpuData versusCpuData = new SRPG.VersusCpuData();
          try
          {
            versusCpuData.Deserialize(json.comenemies[index], json.comenemies.Length - index);
            this.mVersusCpuData.Add(versusCpuData);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
        }
      }
      return true;
    }

    public eStoryPart GetReleaseStoryPart(string quest_id)
    {
      if (this.mWorlds.Count == 0)
        return eStoryPart.None;
      foreach (SectionParam mWorld in this.mWorlds)
      {
        if (quest_id == mWorld.releaseKeyQuest)
          return (eStoryPart) mWorld.storyPart;
      }
      return eStoryPart.None;
    }

    public Dictionary<eStoryPart, bool> GetUnlockStoryPartStates()
    {
      Dictionary<eStoryPart, bool> unlockStoryPartStates = new Dictionary<eStoryPart, bool>();
      foreach (eStoryPart key in Enum.GetValues(typeof (eStoryPart)))
        unlockStoryPartStates.Add(key, false);
      for (int i = 0; i < this.mWorlds.Count; ++i)
      {
        if (!string.IsNullOrEmpty(this.mWorlds[i].releaseKeyQuest))
        {
          QuestParam questParam = Array.Find<QuestParam>(this.Quests, (Predicate<QuestParam>) (quest => quest.iname == this.mWorlds[i].releaseKeyQuest));
          if (questParam == null || questParam.state != QuestStates.Cleared)
            continue;
        }
        eStoryPart storyPart = (eStoryPart) this.mWorlds[i].storyPart;
        if (unlockStoryPartStates.ContainsKey(storyPart))
          unlockStoryPartStates[storyPart] = true;
      }
      return unlockStoryPartStates;
    }

    public bool CheckReleaseStoryPart()
    {
      return this.GetNeedReleaseAnimationStoryPart() != eStoryPart.None;
    }

    public eStoryPart GetNeedReleaseAnimationStoryPart()
    {
      QuestParam[] quests = this.Quests;
      QuestParam questParam = (QuestParam) null;
      for (int index = 0; index < quests.Length; ++index)
      {
        if (quests[index].IsStory)
        {
          if (quests[index].state == QuestStates.Cleared)
            questParam = quests[index];
          else
            break;
        }
      }
      if (questParam == null)
        return eStoryPart.None;
      eStoryPart releaseStoryPart = this.GetReleaseStoryPart(questParam.iname);
      switch (releaseStoryPart)
      {
        case eStoryPart.None:
          return eStoryPart.None;
        case eStoryPart.Makuai:
          return eStoryPart.None;
        default:
          if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.RELEASE_STORY_PART_KEY + (object) (int) releaseStoryPart))
            return releaseStoryPart;
          goto case eStoryPart.None;
      }
    }

    public string GetUnlockConditionsStoryPartMessage(eStoryPart story_part)
    {
      SectionParam sectionParam = this.mWorlds.Find((Predicate<SectionParam>) (p => (eStoryPart) p.storyPart == story_part));
      return sectionParam != null ? LocalizedText.Get("sys." + sectionParam.message_sys_id) : string.Empty;
    }

    public List<SectionParam> GetAllStorySelctionParams(bool isAvailableOnly = false)
    {
      List<QuestParam> questParamList = !isAvailableOnly ? this.mQuests.Where<QuestParam>((Func<QuestParam, bool>) (quest => !quest.hidden && quest.IsStory && !string.IsNullOrEmpty(quest.world))).ToList<QuestParam>() : ((IEnumerable<QuestParam>) this.mPlayer.AvailableQuests).Where<QuestParam>((Func<QuestParam, bool>) (quest => !quest.hidden && quest.IsStory && !string.IsNullOrEmpty(quest.world))).ToList<QuestParam>();
      SortedDictionary<string, SectionParam> source = new SortedDictionary<string, SectionParam>();
      for (int index = 0; index < questParamList.Count; ++index)
      {
        if (!source.ContainsKey(questParamList[index].world))
        {
          SectionParam world = this.FindWorld(questParamList[index].world);
          if (world != null)
            source.Add(questParamList[index].world, world);
        }
      }
      return source.Select<KeyValuePair<string, SectionParam>, SectionParam>((Func<KeyValuePair<string, SectionParam>, SectionParam>) (key_value => key_value.Value)).ToList<SectionParam>();
    }

    public List<QuestParam> GetAllQuestParamsInChallengeableStorySelctions()
    {
      List<QuestParam> challengeableStorySelctions = new List<QuestParam>();
      Dictionary<string, SectionParam> dictionary = this.GetAllStorySelctionParams(true).ToDictionary<SectionParam, string>((Func<SectionParam, string>) (list_item => list_item.iname));
      foreach (QuestParam quest in this.Quests)
      {
        if (quest.type == QuestTypes.Story && quest.type == QuestTypes.Free && quest.type == QuestTypes.StoryExtra && dictionary.ContainsKey(quest.world))
          challengeableStorySelctions.Add(quest);
      }
      return challengeableStorySelctions;
    }

    public List<QuestParam> GetAllTowerQuestParam()
    {
      List<QuestParam> questParamList = new List<QuestParam>();
      List<TowerFloorParam> source = new List<TowerFloorParam>();
      foreach (TowerParam mTower in this.mTowers)
      {
        List<TowerFloorParam> towerFloors = this.FindTowerFloors(mTower.iname);
        if (towerFloors != null)
          source.AddRange((IEnumerable<TowerFloorParam>) towerFloors);
      }
      return source.Select<TowerFloorParam, QuestParam>((Func<TowerFloorParam, QuestParam>) (floor => floor.GetQuestParam())).ToList<QuestParam>();
    }

    public List<QuestParam> GetAllMultiTowerQuestParam()
    {
      List<QuestParam> questParamList = new List<QuestParam>();
      List<QuestParam> list = this.mQuests.Where<QuestParam>((Func<QuestParam, bool>) (quest => quest.type == QuestTypes.MultiTower)).ToList<QuestParam>();
      List<MultiTowerFloorParam> source = new List<MultiTowerFloorParam>();
      foreach (QuestParam questParam in list)
      {
        foreach (MultiTowerFloorParam multiTowerFloorParam in MonoSingleton<GameManager>.Instance.GetMTAllFloorParam(questParam.iname))
        {
          if (source.Contains(multiTowerFloorParam))
            source.Add(multiTowerFloorParam);
        }
      }
      return source.Select<MultiTowerFloorParam, QuestParam>((Func<MultiTowerFloorParam, QuestParam>) (floor => floor.GetQuestParam())).ToList<QuestParam>();
    }

    public List<UnitData> GetBattleEntryUnits(QuestParam questParam)
    {
      List<UnitData> battleEntryUnits = (List<UnitData>) null;
      if (PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay)
        battleEntryUnits = this.GetMultiPlayUnits(questParam);
      else if (MonoSingleton<GameManager>.Instance.AudienceMode)
        battleEntryUnits = this.GetAudienceModeUnits(questParam);
      else if (MonoSingleton<GameManager>.Instance.IsVSCpuBattle)
        battleEntryUnits = this.GetCPUBattleUnits(questParam);
      return battleEntryUnits;
    }

    private List<UnitData> GetAudienceModeUnits(QuestParam questParam)
    {
      List<UnitData> audienceModeUnits = new List<UnitData>();
      if (PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay || !this.AudienceMode)
        return audienceModeUnits;
      UnitData[] unitDataArray = (UnitData[]) null;
      AudienceStartParam startedParam = this.AudienceManager.GetStartedParam();
      JSON_MyPhotonRoomParam roomParam = this.AudienceManager.GetRoomParam();
      if (startedParam != null)
      {
        List<JSON_MyPhotonPlayerParam> photonPlayerParamList = new List<JSON_MyPhotonPlayerParam>();
        for (int index = 0; index < startedParam.players.Length; ++index)
        {
          startedParam.players[index].SetupUnits();
          photonPlayerParamList.Add(startedParam.players[index]);
        }
        int length = 0;
        int index1 = 0;
        foreach (JSON_MyPhotonPlayerParam photonPlayerParam in photonPlayerParamList)
          length += photonPlayerParam.units.Length;
        unitDataArray = new UnitData[length];
        VS_MODE vsmode = (VS_MODE) roomParam.vsmode;
        foreach (JSON_MyPhotonPlayerParam photonPlayerParam in photonPlayerParamList)
        {
          int num = 0;
          foreach (JSON_MyPhotonPlayerParam.UnitDataElem unit in photonPlayerParam.units)
          {
            unitDataArray[index1] = unit.unit;
            ++index1;
            if (vsmode == VS_MODE.THREE_ON_THREE && ++num >= VersusRuleParam.THREE_ON_THREE)
              break;
          }
        }
      }
      for (int index = 0; index < unitDataArray.Length; ++index)
      {
        if (unitDataArray[index] != null)
          audienceModeUnits.Add(unitDataArray[index]);
      }
      return audienceModeUnits;
    }

    private List<UnitData> GetCPUBattleUnits(QuestParam questParam)
    {
      List<UnitData> cpuBattleUnits = new List<UnitData>();
      if (PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay || this.AudienceMode || !this.IsVSCpuBattle)
        return cpuBattleUnits;
      PartyData partyOfType = this.Player.FindPartyOfType(QuestParam.QuestToPartyIndex(questParam.type));
      SRPG.VersusCpuData versusCpu = (SRPG.VersusCpuData) GlobalVars.VersusCpu;
      int index1 = 0;
      UnitData[] unitDataArray = new UnitData[partyOfType.MAX_UNIT + versusCpu.Units.Length];
      for (int index2 = 0; index2 < partyOfType.MAX_UNIT; ++index2)
      {
        long unitUniqueId = partyOfType.GetUnitUniqueID(index2);
        if (unitUniqueId > 0L)
        {
          unitDataArray[index1] = this.Player.FindUnitDataByUniqueID(unitUniqueId);
          ++index1;
        }
      }
      for (int index3 = 0; index3 < versusCpu.Units.Length; ++index3)
      {
        if (versusCpu.Units[index3] != null)
        {
          unitDataArray[index1] = versusCpu.Units[index3];
          ++index1;
        }
      }
      for (int index4 = 0; index4 < unitDataArray.Length; ++index4)
      {
        if (unitDataArray[index4] != null)
          cpuBattleUnits.Add(unitDataArray[index4]);
      }
      return cpuBattleUnits;
    }

    private List<UnitData> GetMultiPlayUnits(QuestParam questParam)
    {
      List<UnitData> multiPlayUnits = new List<UnitData>();
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (!instance.IsMultiPlay)
        return multiPlayUnits;
      MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
      JSON_MyPhotonRoomParam myPhotonRoomParam = currentRoom != null ? JSON_MyPhotonRoomParam.Parse(currentRoom.json) : (JSON_MyPhotonRoomParam) null;
      List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
      int length = 0;
      UnitData[] unitDataArray;
      if (instance.IsMultiVersus)
      {
        int index = 0;
        foreach (JSON_MyPhotonPlayerParam photonPlayerParam in myPlayersStarted)
          length += photonPlayerParam.units.Length;
        unitDataArray = new UnitData[length];
        VS_MODE vsmode = (VS_MODE) myPhotonRoomParam.vsmode;
        foreach (JSON_MyPhotonPlayerParam photonPlayerParam in myPlayersStarted)
        {
          int num = 0;
          foreach (JSON_MyPhotonPlayerParam.UnitDataElem unit in photonPlayerParam.units)
          {
            unitDataArray[index] = unit.unit;
            ++index;
            if (vsmode == VS_MODE.THREE_ON_THREE && ++num >= VersusRuleParam.THREE_ON_THREE)
              break;
          }
        }
      }
      else
      {
        unitDataArray = new UnitData[JSON_MyPhotonRoomParam.GetTotalUnitNum(questParam)];
        if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.TOWER)
        {
          int num1 = 0;
          foreach (JSON_MyPhotonPlayerParam photonPlayerParam in myPlayersStarted)
          {
            int unitSlotNum = myPhotonRoomParam != null ? myPhotonRoomParam.GetUnitSlotNum(photonPlayerParam.playerIndex) : 0;
            int num2 = num1;
            num1 += unitSlotNum;
            foreach (JSON_MyPhotonPlayerParam.UnitDataElem unit in photonPlayerParam.units)
            {
              if (unit.slotID >= 0 && unit.slotID < unitSlotNum)
              {
                int index = num2 + unit.slotID;
                unitDataArray[index] = unit.unit;
              }
            }
          }
        }
        else
        {
          int num3 = 0;
          foreach (JSON_MyPhotonPlayerParam photonPlayerParam in myPlayersStarted)
          {
            int unitSlotNum = myPhotonRoomParam != null ? myPhotonRoomParam.GetUnitSlotNum(photonPlayerParam.playerIndex) : 0;
            int num4 = num3;
            num3 += unitSlotNum;
            foreach (JSON_MyPhotonPlayerParam.UnitDataElem unit in photonPlayerParam.units)
            {
              if (unit.slotID >= 0 && unit.slotID < unitSlotNum)
              {
                int index = num4 + unit.slotID;
                unitDataArray[index] = unit.unit;
              }
            }
          }
        }
      }
      for (int index = 0; index < unitDataArray.Length; ++index)
      {
        if (unitDataArray[index] != null)
          multiPlayUnits.Add(unitDataArray[index]);
      }
      return multiPlayUnits;
    }

    public void DestroyAudienceManager()
    {
      if (this.mAudienceManager == null)
        return;
      this.mAudienceManager.Reset();
      this.mAudienceManager = (VersusAudienceManager) null;
    }

    public void ResetQuestPlayTime()
    {
      this.mQuestPlayTime.start = 0.0f;
      this.mQuestPlayTime.end = 0.0f;
    }

    public void StartQuestPlayTime()
    {
      this.ResetQuestPlayTime();
      this.mQuestPlayTime.start = Time.time;
    }

    public void EndQuestPlayTime() => this.mQuestPlayTime.end = Time.time;

    public float GetQuestPlayTime()
    {
      return (double) this.mQuestPlayTime.start > 0.0 && (double) this.mQuestPlayTime.end > 0.0 ? this.mQuestPlayTime.end - this.mQuestPlayTime.start : 0.0f;
    }

    public enum ELoadMasterDataResult
    {
      NOT_YET_MATE,
      SUCCESS,
      ERROR_MASTER_PARAM_DECRYPT,
      ERROR_QUEST_PARAM_DECRYPT,
      ERROR_QUEST_DROP_PARAM_DECRYPT,
      ERROR_MASTER_PARAM_DESERIALIZE,
      ERROR_QUEST_PARAM_DESERIALIZE,
      ERROR_OTHER,
    }

    public struct LoadMasterDataResult
    {
      public Exception Exception;
      public GameManager.ELoadMasterDataResult Result;
      public FlowNode_SendLogMessage.SendLogGenerator LogData;
    }

    public class MasterDataLoadException : Exception
    {
      public GameManager.ELoadMasterDataResult Type;
      public Exception ActualException;

      public override string Message
      {
        get
        {
          object[] objArray = new object[4]
          {
            (object) "Failed to load master data [",
            (object) this.Type,
            (object) "]\n",
            null
          };
          string str;
          if (this.ActualException != null)
            str = "(" + (object) this.ActualException.GetType() + ": " + this.ActualException.Message + ")";
          else
            str = string.Empty;
          objArray[3] = (object) str;
          return string.Concat(objArray);
        }
      }

      public override string StackTrace
      {
        get => this.ActualException == null ? (string) null : this.ActualException.StackTrace;
      }

      public override Exception GetBaseException() => this.ActualException;
    }

    public class MasterParamDecryptException : GameManager.MasterDataLoadException
    {
    }

    public class QuestParamDecryptException : GameManager.MasterDataLoadException
    {
    }

    public class QuestDropParamDecryptException : GameManager.MasterDataLoadException
    {
    }

    public class MasterParamDeserializeException : GameManager.MasterDataLoadException
    {
    }

    public class QuestParamDeserializeException : GameManager.MasterDataLoadException
    {
    }

    public class MasterDataGenericException : GameManager.MasterDataLoadException
    {
    }

    public class MasterDataInBinary
    {
      public byte[] MasterBytes;
      public byte[] QuestBytes;
      public GameManager.LoadMasterDataResult LoadResult = new GameManager.LoadMasterDataResult();

      public GameManager.MasterDataInBinary Load(bool serialized, bool encrypted)
      {
        if (serialized && encrypted)
        {
          byte[] input1 = (byte[]) null;
          try
          {
            input1 = AssetManager.LoadBinaryData("Data/MasterParamSerialized");
            this.MasterBytes = EncryptionHelper.Decrypt(EncryptionHelper.KeyType.APP, input1, SRPG.Network.MasterDigest, EncryptionHelper.DecryptOptions.IsFile);
          }
          catch (Exception ex)
          {
            FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
            sendLogGenerator.Add(GameManager.ELoadMasterDataResult.ERROR_MASTER_PARAM_DECRYPT.ToString(), ex.Message);
            sendLogGenerator.Add("StackTrace", ex.StackTrace);
            sendLogGenerator.Add("Digest", SRPG.Network.MasterDigest);
            sendLogGenerator.Add("DataLength", (input1 == null ? 0 : input1.Length).ToString() + string.Empty);
            this.LoadResult.Exception = ex;
            this.LoadResult.LogData = sendLogGenerator;
            this.LoadResult.Result = GameManager.ELoadMasterDataResult.ERROR_MASTER_PARAM_DECRYPT;
            return this;
          }
          byte[] input2 = (byte[]) null;
          try
          {
            input2 = AssetManager.LoadBinaryData("Data/QuestParamSerialized");
            this.QuestBytes = EncryptionHelper.Decrypt(EncryptionHelper.KeyType.APP, input2, SRPG.Network.QuestDigest, EncryptionHelper.DecryptOptions.IsFile);
          }
          catch (Exception ex)
          {
            FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
            sendLogGenerator.Add(GameManager.ELoadMasterDataResult.ERROR_QUEST_PARAM_DECRYPT.ToString(), ex.Message);
            sendLogGenerator.Add("StackTrace", ex.StackTrace);
            sendLogGenerator.Add("Digest", SRPG.Network.QuestDigest);
            sendLogGenerator.Add("DataLength", (input2 == null ? 0 : input2.Length).ToString() + string.Empty);
            this.LoadResult.Exception = ex;
            this.LoadResult.LogData = sendLogGenerator;
            this.LoadResult.Result = GameManager.ELoadMasterDataResult.ERROR_QUEST_PARAM_DECRYPT;
            return this;
          }
        }
        else if (serialized)
        {
          this.MasterBytes = AssetManager.LoadBinaryData("Data/MasterParamSerialized");
          this.QuestBytes = AssetManager.LoadBinaryData("Data/QuestParamSerialized");
        }
        else
        {
          this.LoadResult.Exception = (Exception) new InvalidOperationException("MessagePackのシリアライズされていない暗号化だけされたマスタデータは未対応");
          this.LoadResult.Result = GameManager.ELoadMasterDataResult.ERROR_OTHER;
          return this;
        }
        this.LoadResult.Result = GameManager.ELoadMasterDataResult.SUCCESS;
        return this;
      }
    }

    [Flags]
    public enum BadgeTypes
    {
      Unit = 1,
      UnitUnlock = 2,
      GoldGacha = 4,
      RareGacha = 8,
      DailyMission = 16, // 0x00000010
      Arena = 32, // 0x00000020
      Multiplay = 64, // 0x00000040
      Friend = 128, // 0x00000080
      GiftBox = 256, // 0x00000100
      ItemEquipment = 512, // 0x00000200
      All = -1, // 0xFFFFFFFF
    }

    public delegate void DayChangeEvent();

    public delegate void StaminaChangeEvent();

    public delegate void RankUpCountChangeEvent(int count);

    public delegate void PlayerLvChangeEvent();

    public delegate void PlayerCurrencyChangeEvent();

    public delegate void BuyCoinEvent();

    public delegate bool SceneChangeEvent();

    private class TextureRequest
    {
      public RawImage Target;
      public string Path;
      public LoadRequest Request;
    }

    private class VersusRange
    {
      public int min;
      public int max;

      public VersusRange(int _min, int _max)
      {
        this.min = _min;
        this.max = _max;
      }
    }

    private struct QuestTime
    {
      public float start;
      public float end;
    }
  }
}
