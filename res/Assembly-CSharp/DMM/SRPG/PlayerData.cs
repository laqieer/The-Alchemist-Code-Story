// Decompiled with JetBrains decompiler
// Type: SRPG.PlayerData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Purchase;
using SRPG.JsonUtlity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class PlayerData
  {
    public static readonly int INI_UNIT_CAPACITY = 20;
    public static readonly int MAX_UNIT_CAPACITY = 50;
    public const string SAIGONI_HOME_HIRAITA_LV = "lastplv";
    public const string SAIGONI_HOME_HIRAITA_VIPLV = "lastviplv";
    public const int ITEM_CAP = 99999;
    public const int INVENTORY_SIZE = 5;
    private static readonly string PLAYRE_DATA_VERSION = "38.0";
    public static readonly string TEAM_ID_KEY = "TeamID";
    public static readonly string MULTI_PLAY_TEAM_ID_KEY = "MultiPlayTeamID";
    public static readonly string ARENA_TEAM_ID_KEY = "ArenaTeamID";
    public static readonly string ROOM_COMMENT_KEY = "MultiPlayRoomComment";
    public static readonly string VERSUS_ID_KEY = "VERSUS_PLACEMENT_";
    private string mName;
    private string mCuid;
    private string mFuid;
    private string mTuid;
    private long mTuidExpiredAt;
    private int mLoginCount;
    private OInt mNewGameAt = (OInt) 0;
    private OInt mLv = (OInt) 0;
    private OInt mExp = (OInt) 0;
    private OInt mGold = (OInt) 0;
    private OInt mFreeCoin = (OInt) 0;
    private OInt mPaidCoin = (OInt) 0;
    private OInt mComCoin = (OInt) 0;
    private OInt mTourCoin = (OInt) 0;
    private OInt mArenaCoin = (OInt) 0;
    private OInt mMultiCoin = (OInt) 0;
    private OInt mAbilityPoint = (OInt) 0;
    private OInt mPiecePoint = (OInt) 0;
    private OInt mVipRank = (OInt) 0;
    private OInt mVipPoint = (OInt) 0;
    private OLong mVipExpiredAt = (OLong) 0L;
    private OLong mPremiumExpiredAt = (OLong) 0L;
    private List<EventCoinData> mEventCoinList = new List<EventCoinData>();
    private TimeRecoveryValue mStamina = new TimeRecoveryValue();
    private TimeRecoveryValue mCaveStamina = new TimeRecoveryValue();
    private TimeRecoveryValue mAbilityRankUpCount = new TimeRecoveryValue();
    public int mArenaResetCount;
    public DateTime LoginDate;
    public long TutorialFlags;
    private Json_LoginBonus[] mLoginBonus;
    private int mLoginBonusCount;
    private bool mFirstLogin;
    private Dictionary<string, Json_LoginBonusTable> mLoginBonusTables = new Dictionary<string, Json_LoginBonusTable>();
    private Json_Notify mServerNotify;
    private Json_LoginBonusTable mLoginBonus30days;
    private Json_LoginBonusTable mLoginBonus28days;
    private Json_LoginBonusTable mPremiumLoginBonus;
    private OInt mChallengeMultiNum = (OInt) 0;
    private OInt mStaminaBuyNum = (OInt) 0;
    private OInt mGoldBuyNum = (OInt) 0;
    private OInt mChallengeArenaNum = (OInt) 0;
    private TimeRecoveryValue mChallengeArenaTimer = new TimeRecoveryValue();
    private OInt mTourNum = (OInt) 0;
    private OInt mUnitCap = (OInt) PlayerData.INI_UNIT_CAPACITY;
    private int mArenaRank;
    private int mBestArenaRank;
    private DateTime mArenaLastAt = GameUtility.UnixtimeToLocalTime(0L);
    private int mArenaSeed;
    private int mArenaMaxActionNum;
    private DateTime mArenaEndAt = GameUtility.UnixtimeToLocalTime(0L);
    private List<UnitData> mUnits;
    private List<ItemData> mItems;
    private List<PartyData> mPartys;
    private List<ArtifactData> mArtifacts = new List<ArtifactData>();
    private Dictionary<string, Dictionary<int, int>> mArtifactsNumByRarity = new Dictionary<string, Dictionary<int, int>>();
    private List<ConceptCardData> mConceptCards = new List<ConceptCardData>();
    private Dictionary<string, int> mConceptCardNum = new Dictionary<string, int>();
    private List<SkinConceptCardData> mSkinConceptCards = new List<SkinConceptCardData>();
    private List<ConceptCardMaterialData> mConceptCardExpMaterials = new List<ConceptCardMaterialData>();
    private List<ConceptCardMaterialData> mConceptCardTrustMaterials = new List<ConceptCardMaterialData>();
    private Dictionary<long, RuneData> mRunes = new Dictionary<long, RuneData>();
    private short mRuneStorage;
    private short mRuneStorageUsed;
    private List<RuneEnforceGaugeData> mRuneEnforceGauge = new List<RuneEnforceGaugeData>();
    private PlayerGuildData mPlayerGuild;
    private GuildData mGuild;
    private StoryExChallengeCountData mStoryExChallengeCountData = new StoryExChallengeCountData();
    private int mGvGMaxActionNum;
    private List<string> mSkins = new List<string>();
    private Dictionary<long, UnitData> mUniqueID2UnitData = new Dictionary<long, UnitData>();
    private Dictionary<string, ItemData> mID2ItemData = new Dictionary<string, ItemData>();
    private TrophyData mTrophyData = new TrophyData();
    private GuildTrophyData mGuildTrophyData = new GuildTrophyData();
    private ShopData[] mShops = new ShopData[Enum.GetNames(typeof (EShopType)).Length];
    private LimitedShopData mLimitedShops = new LimitedShopData();
    private EventShopData mEventShops = new EventShopData();
    private UnitPieceShopData mUnitPieceShopData = new UnitPieceShopData();
    private List<SRPG.RankMatchMissionState> mRankMatchMissionState = new List<SRPG.RankMatchMissionState>();
    public RankMatchSeasonResult mRankMatchSeasonResult = new RankMatchSeasonResult();
    public RaidRankRewardResult mRaidRankRewardResult = new RaidRankRewardResult();
    public GuildRaidSeasonResult mGuildRaidSeasonResult = new GuildRaidSeasonResult();
    private List<PlayerCoinBuyUseBonusState> mCoinBuyUseBonusStateList = new List<PlayerCoinBuyUseBonusState>();
    private List<PlayerCoinBuyUseBonusRewardState> mCoinBuyUseBonusRewardStateList = new List<PlayerCoinBuyUseBonusRewardState>();
    private AutoRepeatQuestData mAutoRepeatQuestProgress = new AutoRepeatQuestData();
    private AutoRepeatQuestBoxData mAutoRepeatQuestBox = new AutoRepeatQuestBoxData();
    private List<string> mAutoRepeatQuestApItemPriority = new List<string>();
    private Dictionary<eOverWritePartyType, List<PartyOverWrite>> mPartyOverWriteDatas;
    private List<QuestParam> mAvailableQuests = new List<QuestParam>();
    private bool mQuestListDirty = true;
    private List<OpenedQuestArchive> mQuestArchives = new List<OpenedQuestArchive>();
    public long QuestArchivesFreeEndTime;
    public List<FriendData> Friends = new List<FriendData>();
    public List<FriendData> FriendsFollower = new List<FriendData>();
    public List<FriendData> FriendsFollow = new List<FriendData>();
    public int mFriendNum;
    public int mFollowerNum;
    public List<string> mFollowerUID = new List<string>();
    public List<MultiFuid> MultiFuids = new List<MultiFuid>();
    public List<SupportData> Supports = new List<SupportData>();
    public FriendPresentWishList FriendPresentWishList = new FriendPresentWishList();
    public FriendPresentReceiveList FriendPresentReceiveList = new FriendPresentReceiveList();
    public List<MailData> Mails = new List<MailData>();
    public List<MailData> CurrentMails = new List<MailData>();
    public MailPageData MailPage;
    public OLong mUnlocks = (OLong) 0L;
    public FreeGacha FreeGachaGold = new FreeGacha();
    public FreeGacha FreeGachaCoin = new FreeGacha();
    public PaidGacha PaidGacha = new PaidGacha();
    public Dictionary<string, PaymentInfo> PaymentInfos = new Dictionary<string, PaymentInfo>();
    private bool mUnreadMailPeriod;
    private bool mUnreadMail;
    private bool mValidGpsGift;
    private bool mValidFriendPresent;
    private string mSelectedAward;
    private List<string> mHaveAward = new List<string>();
    private int mVersusPoint;
    private int[] mVersusWinCount = new int[4];
    private int[] mVersusTotalCount = new int[4];
    private int mVersusFreeWin;
    private int mVersusRankWin;
    private int mVersusFriendWin;
    private int mVersusTwFloor;
    private int mVersusTwKey;
    private int mVersusTwWinCnt;
    private bool mVersusSeasonGift;
    private int mRankMatchRank;
    private int mRankMatchScore;
    private RankMatchClass mRankMatchClass;
    private int mRankMatchBattlePoint;
    private int mRankMatchStreakWin;
    private int mRankMatchOldRank;
    private int mRankMatchOldScore;
    private RankMatchClass mRankMatchOldClass;
    private bool mMultiInvitaionFlag;
    private string mMultiInvitaionComment;
    private int mFirstFriendCount;
    private int mFirstChargeStatus = -1;
    private long mGuerrillaShopStart;
    private long mGuerrillaShopEnd;
    private bool mIsGuerrillaShopStarted;
    private const string CHANGE_SCRIPT_COMPLETE_QUEST_KEY = "COMPLETE_QUEST_MISSION";
    private List<ExpansionPurchaseData> mExpansions = new List<ExpansionPurchaseData>();
    public CombatPowerData combatPowerData = new CombatPowerData();
    public int SupportCount;
    public int SupportGold;
    private ItemData[] mInventory = new ItemData[5];
    private Dictionary<string, int> mConsumeMaterials = new Dictionary<string, int>(16);
    private int mCreateItemCost;
    private const int MAX_JOB = 6;
    private int mPrevCheckHour = -1;
    private UpdateTrophyInterval mUpdateInterval = new UpdateTrophyInterval();
    private Queue<string> mLoginBonusQueue = new Queue<string>();
    private PlayerData.TrophyStarMission mTrophyStarMissionInfo;

    public PlayerData()
    {
      this.LoginDate = DateTime.Now;
      this.mPartys = new List<PartyData>(17);
      for (int type = 0; type < 17; ++type)
        this.mPartys.Add(new PartyData((PlayerPartyTypes) type)
        {
          Name = "パーティ" + (object) (type + 1),
          PartyType = (PlayerPartyTypes) type
        });
    }

    public TrophyData TrophyData => this.mTrophyData;

    public GuildTrophyData GetGuildTrophyData() => this.mGuildTrophyData;

    public GuildTrophyData GuildTrophyData => this.mGuildTrophyData;

    public UnitPieceShopData UnitPieceShopData => this.mUnitPieceShopData;

    public List<SRPG.RankMatchMissionState> RankMatchMissionState
    {
      get => this.mRankMatchMissionState;
      set => this.mRankMatchMissionState = value;
    }

    public List<PlayerCoinBuyUseBonusState> CoinBuyUseBonusStateList
    {
      get => this.mCoinBuyUseBonusStateList;
    }

    public List<PlayerCoinBuyUseBonusRewardState> CoinBuyUseBonusRewardStateList
    {
      get => this.mCoinBuyUseBonusRewardStateList;
    }

    public AutoRepeatQuestData AutoRepeatQuestProgress => this.mAutoRepeatQuestProgress;

    public bool IsAutoRepeatQuestMeasuring
    {
      get => this.mAutoRepeatQuestProgress != null && this.mAutoRepeatQuestProgress.IsMeasuring;
    }

    public AutoRepeatQuestBoxData AutoRepeatQuestBox => this.mAutoRepeatQuestBox;

    public List<string> AutoRepeatQuestApItemPriority => this.mAutoRepeatQuestApItemPriority;

    public Dictionary<eOverWritePartyType, List<PartyOverWrite>> PartyOverWriteDatas
    {
      get => this.mPartyOverWriteDatas;
    }

    public void IncrementRankMatchMission(RankMatchMissionType type)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      instance.GetVersusRankMissionList(instance.RankMatchScheduleId).ForEach((Action<VersusRankMissionParam>) (mission =>
      {
        if (mission.Type != type)
          return;
        SRPG.RankMatchMissionState matchMissionState = this.RankMatchMissionState.Find((Predicate<SRPG.RankMatchMissionState>) (state => state.IName == mission.IName));
        if (matchMissionState == null)
        {
          matchMissionState = new SRPG.RankMatchMissionState();
          matchMissionState.Deserialize(mission.IName, 0, (string) null);
          this.RankMatchMissionState.Add(matchMissionState);
        }
        matchMissionState.Increment();
      }));
    }

    public void SetMaxProgRankMatchMission(RankMatchMissionType type, int prog)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      instance.GetVersusRankMissionList(instance.RankMatchScheduleId).ForEach((Action<VersusRankMissionParam>) (mission =>
      {
        if (mission.Type != type)
          return;
        SRPG.RankMatchMissionState matchMissionState = this.RankMatchMissionState.Find((Predicate<SRPG.RankMatchMissionState>) (state => state.IName == mission.IName));
        if (matchMissionState == null)
        {
          matchMissionState = new SRPG.RankMatchMissionState();
          matchMissionState.Deserialize(mission.IName, 0, (string) null);
          this.RankMatchMissionState.Add(matchMissionState);
        }
        if (matchMissionState.Progress >= prog)
          return;
        matchMissionState.SetProgress(prog);
      }));
    }

    public void RewardedRankMatchMission(string iname)
    {
      this.RankMatchMissionState.Find((Predicate<SRPG.RankMatchMissionState>) (state => state.IName == iname)).Rewarded();
    }

    public string GetMissionProgressString()
    {
      string str = "\"missionprogs\":[";
      for (int index = 0; index < this.RankMatchMissionState.Count; ++index)
      {
        if (index > 0)
          str += ",";
        str = str + "{" + "\"iname\":\"" + JsonEscape.Escape(this.RankMatchMissionState[index].IName) + "\"" + ",\"prog\":" + (object) this.RankMatchMissionState[index].Progress + "}";
      }
      return str + "]";
    }

    public bool IsQuestAvailable(string questID)
    {
      QuestParam questparam = MonoSingleton<GameManager>.Instance.FindQuest(questID);
      if (questparam == null)
        return false;
      bool flag1 = questparam.IsDateUnlock();
      bool flag2 = Array.Find<QuestParam>(this.AvailableQuests, (Predicate<QuestParam>) (p => p == questparam)) != null;
      return flag1 && flag2;
    }

    public bool IsQuestAvailable(QuestParam questparam)
    {
      if (questparam == null)
        return false;
      bool flag1 = questparam.IsDateUnlock();
      bool flag2 = Array.FindIndex<QuestParam>(this.AvailableQuests, (Predicate<QuestParam>) (p => p == questparam)) >= 0;
      return flag1 && flag2;
    }

    public bool IsQuestCleared(string questID)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(questID);
      return quest != null && quest.state == QuestStates.Cleared;
    }

    public QuestParam[] AvailableQuests
    {
      get
      {
        if (this.mQuestListDirty)
        {
          this.mQuestListDirty = false;
          this.mAvailableQuests.Clear();
          GameManager instance = MonoSingleton<GameManager>.Instance;
          for (int index = 0; index < instance.Quests.Length; ++index)
          {
            QuestParam quest = instance.Quests[index];
            if (quest != null && quest.IsQuestCondition())
              this.mAvailableQuests.Add(quest);
          }
        }
        return this.mAvailableQuests.ToArray();
      }
    }

    public QuestParam[] GetClearedQuests()
    {
      List<QuestParam> questParamList = new List<QuestParam>();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      for (int index = 0; index < instance.Quests.Length; ++index)
      {
        QuestParam quest = instance.Quests[index];
        if (quest != null && quest.state == QuestStates.Cleared)
          questParamList.Add(quest);
      }
      return questParamList.ToArray();
    }

    public List<OpenedQuestArchive> OpenedQuestArchives => this.mQuestArchives;

    public bool CanOpenArchiveForFree => Network.GetServerTime() > this.QuestArchivesFreeEndTime;

    public string OkyakusamaCode => this.mCuid;

    public BtlResultTypes RankMatchResult { get; set; }

    public int RankMatchTotalCount => this.RankMatchWinCount + this.RankMatchLoseCount;

    public int RankMatchWinCount { get; set; }

    public int RankMatchLoseCount { get; set; }

    public string Name
    {
      get => this.mName;
      set => this.mName = value;
    }

    public string CUID => this.mCuid;

    public string FUID => this.mFuid;

    public string TUID => this.mTuid;

    public long TuidExpiredAt => this.mTuidExpiredAt;

    public int LoginCount => this.mLoginCount;

    public int Lv => (int) this.mLv;

    public int Exp => (int) this.mExp;

    public long NewGameAt => (long) (int) this.mNewGameAt;

    public DateTime NewGameAtDateTime => TimeManager.FromUnixTime((long) (int) this.mNewGameAt);

    public int Gold => (int) this.mGold;

    public int Coin => (int) this.mFreeCoin + (int) this.mPaidCoin + (int) this.mComCoin;

    public int FreeCoin => (int) this.mFreeCoin;

    public int PaidCoin => (int) this.mPaidCoin;

    public int ComCoin => (int) this.mComCoin;

    public int TourCoin => (int) this.mTourCoin;

    public int ArenaCoin => (int) this.mArenaCoin;

    public int MultiCoin => (int) this.mMultiCoin;

    public int AbilityPoint => (int) this.mAbilityPoint;

    public int PiecePoint => (int) this.mPiecePoint;

    public int VipRank => (int) this.mVipRank;

    public int VipPoint => (int) this.mVipPoint;

    public List<EventCoinData> EventCoinList => this.mEventCoinList;

    public int Stamina => (int) this.mStamina.val;

    public int StaminaMax => (int) this.mStamina.valMax;

    public long StaminaRecverySec => (long) this.mStamina.interval;

    public long StaminaAt => (long) this.mStamina.at;

    public int StaminaStockCap
    {
      get => (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StaminaStockCap;
    }

    public int CaveStamina => (int) this.mCaveStamina.val;

    public int CaveStaminaMax => (int) this.mCaveStamina.valMax;

    public long CaveStaminaRecverySec => (long) this.mCaveStamina.interval;

    public long CaveStaminaAt => (long) this.mCaveStamina.at;

    public int CaveStaminaStockCap
    {
      get => (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CaveStaminaStockCap;
    }

    public int AbilityRankUpCountNum => (int) this.mAbilityRankUpCount.val;

    public int AbilityRankUpCountMax => (int) this.mAbilityRankUpCount.valMax;

    public long AbilityRankUpCountRecverySec => (long) this.mAbilityRankUpCount.interval;

    public long AbilityRankUpCountAt => (long) this.mAbilityRankUpCount.at;

    public int ChallengeArenaNum => (int) this.mChallengeArenaNum;

    public int ChallengeArenaMax
    {
      get => (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeArenaMax;
    }

    public long ChallengeArenaCoolDownSec
    {
      get
      {
        return (long) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeArenaCoolDownSec;
      }
    }

    public long ChallengeArenaAt => (long) this.mChallengeArenaTimer.at;

    public int ChallengeTourNum => (int) this.mTourNum;

    public int ChallengeTourMax
    {
      get => (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeTourMax;
    }

    public int ArenaRank => this.mArenaRank;

    public int ArenaRankBest => this.mBestArenaRank;

    public DateTime ArenaLastAt => this.mArenaLastAt;

    public int ArenaSeed => this.mArenaSeed;

    public int ArenaMaxActionNum => this.mArenaMaxActionNum;

    public DateTime ArenaEndAt => this.mArenaEndAt;

    public int ChallengeMultiNum => (int) this.mChallengeMultiNum;

    public void IncrementChallengeMultiNum() => ++this.mChallengeMultiNum;

    public int ChallengeMultiMax
    {
      get => (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeMultiMax;
    }

    public int StaminaBuyNum => (int) this.mStaminaBuyNum;

    public int GoldBuyNum => (int) this.mGoldBuyNum;

    public int UnitCap => (int) this.mUnitCap;

    public List<UnitData> Units => this.mUnits;

    public int UnitNum => this.mUnits != null ? this.mUnits.Count : 0;

    public UnitData GetRentalUnit() => this.mUnits.Find((Predicate<UnitData>) (u => u.IsRental));

    public bool RemoveRentalUnit(string iname)
    {
      UnitData unitData = this.mUnits.Find((Predicate<UnitData>) (u => u.IsRental && u.UnitID == iname));
      if (unitData == null)
      {
        DebugUtility.LogError("RemoveRentalUnit/該当ユニットがいない？ iname=" + iname);
        return false;
      }
      if (this.mUnits.Remove(unitData) && this.mUniqueID2UnitData.Remove(unitData.UniqueID))
        return true;
      DebugUtility.LogError("RemoveRentalUnit/抹消エラー？ iname=" + iname);
      return false;
    }

    public List<ItemData> Items
    {
      get => this.mItems.FindAll((Predicate<ItemData>) (item => !item.Param.IsExpire));
    }

    public List<ArtifactData> Artifacts => this.mArtifacts;

    public int ArtifactNum => this.mArtifacts.Count;

    public int ArtifactCap
    {
      get => (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ArtifactBoxCap;
    }

    public int GetArtifactNumByRarity(string iname, int rarity)
    {
      Dictionary<int, int> dictionary;
      int num;
      return !string.IsNullOrEmpty(iname) && this.mArtifactsNumByRarity.TryGetValue(iname, out dictionary) && dictionary.TryGetValue(rarity, out num) ? num : 0;
    }

    private void ResetArtifacts()
    {
      this.mArtifacts.Clear();
      this.mArtifactsNumByRarity.Clear();
    }

    private void AddArtifact(ArtifactData item)
    {
      this.mArtifacts.Add(item);
      this.AddArtifaceNumByRarity(item);
    }

    private void RemoveArtifact(ArtifactData item)
    {
      this.mArtifacts.Remove(item);
      this.RemoveArtifactNumByRarity(item);
    }

    private void AddArtifaceNumByRarity(ArtifactData item)
    {
      Dictionary<int, int> dictionary;
      if (this.mArtifactsNumByRarity.TryGetValue(item.ArtifactParam.iname, out dictionary))
      {
        int num;
        if (dictionary.TryGetValue((int) item.Rarity, out num))
          dictionary[(int) item.Rarity] = num + 1;
        else
          dictionary.Add((int) item.Rarity, 1);
      }
      else
        this.mArtifactsNumByRarity.Add(item.ArtifactParam.iname, new Dictionary<int, int>()
        {
          {
            (int) item.Rarity,
            1
          }
        });
    }

    private void RemoveArtifactNumByRarity(ArtifactData item)
    {
      Dictionary<int, int> dictionary;
      int num1;
      if (!this.mArtifactsNumByRarity.TryGetValue(item.ArtifactParam.iname, out dictionary) || !dictionary.TryGetValue((int) item.Rarity, out num1))
        return;
      int num2 = num1 - 1;
      if (num2 <= 0)
        dictionary.Remove((int) item.Rarity);
      else
        dictionary[(int) item.Rarity] = num2;
    }

    public List<string> Skins => this.mSkins;

    public int FriendCap
    {
      get => (int) MonoSingleton<GameManager>.Instance.MasterParam.GetPlayerParam(this.Lv).fcap;
    }

    public int FriendNum
    {
      get => this.mFriendNum;
      set => this.mFriendNum = value;
    }

    public int FollowerNum
    {
      get => this.mFollowerNum;
      set => this.mFollowerNum = value;
    }

    public List<string> FollowerUID => this.mFollowerUID;

    public bool IsRequestFriend() => this.FriendNum < this.FriendCap;

    public List<PartyData> Partys => this.mPartys;

    public ItemData[] Inventory => this.mInventory.Clone() as ItemData[];

    public int ConceptCardNum => this.mConceptCards.Count;

    public int ConceptCardCap
    {
      get => (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardMax;
    }

    public bool UnreadMailPeriod
    {
      get => this.mUnreadMailPeriod;
      set => this.mUnreadMailPeriod = value;
    }

    public bool UnreadMail => this.mUnreadMail;

    public bool ValidGpsGift
    {
      set => this.mValidGpsGift = value;
      get => this.mValidGpsGift;
    }

    public bool ValidFriendPresent
    {
      set => this.mValidFriendPresent = value;
      get => this.mValidFriendPresent;
    }

    public string SelectedAward
    {
      get => this.mSelectedAward;
      set => this.mSelectedAward = value;
    }

    public int VERSUS_POINT => this.mVersusPoint;

    public int VersusFreeWinCnt => this.mVersusWinCount[0];

    public int VersusTowerWinCnt => this.mVersusWinCount[1];

    public int VersusFriendWinCnt => this.mVersusWinCount[2];

    public int VersusFreeCnt => this.mVersusTotalCount[0];

    public int VersusTowerCnt => this.mVersusTotalCount[1];

    public int VersusFriendCnt => this.mVersusTotalCount[2];

    public int VersusTowerFloor => this.mVersusTwFloor;

    public int VersusTowerKey => this.mVersusTwKey;

    public int VersusTowerWinBonus => this.mVersusTwWinCnt;

    public bool VersusSeazonGiftReceipt
    {
      get => this.mVersusSeasonGift;
      set => this.mVersusSeasonGift = value;
    }

    public int RankMatchRank => this.mRankMatchRank;

    public int RankMatchScore => this.mRankMatchScore;

    public RankMatchClass RankMatchClass => this.mRankMatchClass;

    public int RankMatchBattlePoint => this.mRankMatchBattlePoint;

    public int RankMatchStreakWin => this.mRankMatchStreakWin;

    public int RankMatchOldRank => this.mRankMatchOldRank;

    public int RankMatchOldScore => this.mRankMatchOldScore;

    public RankMatchClass RankMatchOldClass => this.mRankMatchOldClass;

    public bool MultiInvitaionFlag => this.mMultiInvitaionFlag;

    public string MultiInvitaionComment => this.mMultiInvitaionComment;

    public int FirstFriendCount
    {
      get => this.mFirstFriendCount;
      set => this.mFirstFriendCount = value;
    }

    public int FirstChargeStatus
    {
      get => this.mFirstChargeStatus;
      set => this.mFirstChargeStatus = value;
    }

    public long GuerrillaShopStart => this.mGuerrillaShopStart;

    public long GuerrillaShopEnd => this.mGuerrillaShopEnd;

    public bool IsGuerrillaShopStarted
    {
      get => this.mIsGuerrillaShopStarted;
      set => this.mIsGuerrillaShopStarted = value;
    }

    public PlayerGuildData PlayerGuild => this.mPlayerGuild;

    public GuildData Guild => this.mGuild;

    public bool IsGuildAssign => this.mPlayerGuild != null && this.mPlayerGuild.IsJoined;

    public bool HasArenaReward { get; set; }

    public bool HasGvGReward { get; set; }

    public int GvGMaxActionNum => this.mGvGMaxActionNum;

    public bool HasGuildReward { get; set; }

    public int GuildLeagueRate { get; set; }

    public bool IsGuildGvGJoin { get; set; }

    public StoryExChallengeCountData StoryExChallengeCount => this.mStoryExChallengeCountData;

    public int CurrentRuneStorageSize => (int) this.mRuneStorage;

    public int CurrentRuneStorageUsed => (int) this.mRuneStorageUsed;

    public bool IsRuneStorageFull => this.CurrentRuneStorageUsed > this.CurrentRuneStorageSize;

    public List<ExpansionPurchaseData> Expansions => this.mExpansions;

    public bool IsNeedToCombatPowerRequest => this.combatPowerData.IsChanged;

    public bool ConsumeStamina(int stamina)
    {
      if (this.Stamina < stamina)
        return false;
      if ((int) this.mStamina.val >= (int) this.mStamina.valMax)
        this.mStamina.at = (OLong) Network.GetServerTime();
      this.mStamina.val = (OInt) Mathf.Max((int) this.mStamina.val - stamina, 0);
      return true;
    }

    public bool DEBUG_CONSUME_COIN(int coin)
    {
      if (this.Coin < coin)
        return false;
      while ((int) this.mFreeCoin > 0 && coin > 0)
      {
        --coin;
        --this.mFreeCoin;
      }
      while ((int) this.mComCoin > 0 && coin > 0)
      {
        --coin;
        --this.mComCoin;
      }
      while ((int) this.mPaidCoin > 0 && coin > 0)
      {
        --coin;
        --this.mPaidCoin;
      }
      return true;
    }

    public bool DEBUG_CONSUME_PAID_COIN(int coin)
    {
      if (this.PaidCoin < coin)
        return false;
      PlayerData playerData = this;
      playerData.mPaidCoin = (OInt) ((int) playerData.mPaidCoin - coin);
      return true;
    }

    public void DEBUG_ADD_COIN(int free, int paid, int com)
    {
      PlayerData playerData1 = this;
      playerData1.mFreeCoin = (OInt) ((int) playerData1.mFreeCoin + free);
      PlayerData playerData2 = this;
      playerData2.mPaidCoin = (OInt) ((int) playerData2.mPaidCoin + paid);
      PlayerData playerData3 = this;
      playerData3.mComCoin = (OInt) ((int) playerData3.mComCoin + com);
      this.GainVipPoint(paid);
    }

    public void SetCoinPurchaseResult(PaymentManager.CoinRecord record)
    {
      if (record == null)
        return;
      this.mFreeCoin = (OInt) record.currentFreeCoin;
      this.mPaidCoin = (OInt) record.currentPaidCoin;
      foreach (string productId in record.productIds)
      {
        FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
        if (productId.Contains((string) fixParam.VipCardProduct))
        {
          long serverTime = Network.GetServerTime();
          if (serverTime > (long) this.mVipExpiredAt)
          {
            this.mPremiumExpiredAt = (OLong) TimeManager.FromDateTime(TimeManager.FromUnixTime(serverTime + (long) ((int) fixParam.VipCardDate * 24 * 60 * 60)).Date);
          }
          else
          {
            PlayerData playerData = this;
            playerData.mVipExpiredAt = (OLong) ((long) playerData.mVipExpiredAt + (long) ((int) fixParam.VipCardDate * 24 * 60 * 60));
          }
        }
        if (productId.Contains((string) fixParam.PremiumProduct))
        {
          int premiumDateSpan = this.GetPremiumDateSpan();
          this.mPremiumExpiredAt = (OLong) TimeManager.FromDateTime(TimeManager.FromUnixTime(Network.GetServerTime() + (long) (premiumDateSpan * 24 * 60 * 60)).Date);
          MonoSingleton<GameManager>.Instance.Player.ResetPremiumLoginBonus();
        }
      }
    }

    public void SetCoinPurchaseResult(FulfillmentResult result)
    {
      if (result == null)
        return;
      this.mFreeCoin = (OInt) result.CurrentFreeCoin;
      this.mPaidCoin = (OInt) result.CurrentPaidCoin;
      this.mComCoin = (OInt) result.CurrentCommonCoin;
    }

    public void SetOrderResult(FulfillmentResult.OrderInfo order)
    {
      if (order == null)
        return;
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      if (order.ProductId.Contains((string) fixParam.VipCardProduct))
      {
        long serverTime = Network.GetServerTime();
        if (serverTime > (long) this.mVipExpiredAt)
        {
          this.mVipExpiredAt = (OLong) TimeManager.FromDateTime(TimeManager.FromUnixTime(serverTime + (long) ((int) fixParam.VipCardDate * 24 * 60 * 60)).Date + new TimeSpan(23, 59, 59));
        }
        else
        {
          PlayerData playerData = this;
          playerData.mVipExpiredAt = (OLong) ((long) playerData.mVipExpiredAt + (long) ((int) fixParam.VipCardDate * 24 * 60 * 60));
        }
      }
      if (!order.ProductId.Contains((string) fixParam.PremiumProduct))
        return;
      int premiumDateSpan = this.GetPremiumDateSpan();
      long serverTime1 = Network.GetServerTime();
      MonoSingleton<GameManager>.Instance.Player.ResetPremiumLoginBonus();
      DateTime dateTime = TimeManager.FromUnixTime(serverTime1);
      dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
      TimeSpan timeSpan = new TimeSpan(premiumDateSpan, 0, 0, 0, 0);
      this.mPremiumExpiredAt = (OLong) TimeManager.FromDateTime(dateTime + timeSpan);
    }

    public int GetPremiumDateSpan()
    {
      if (MonoSingleton<GameManager>.Instance.MasterParam.Premium != null)
      {
        long serverTime = Network.GetServerTime();
        for (int index = 0; index < MonoSingleton<GameManager>.Instance.MasterParam.Premium.Length; ++index)
        {
          PremiumParam premiumParam = MonoSingleton<GameManager>.Instance.MasterParam.Premium[index];
          if (premiumParam.m_BeginAt < serverTime && serverTime <= premiumParam.m_EndAt)
            return premiumParam.m_Span;
        }
        for (int index = 0; index < MonoSingleton<GameManager>.Instance.MasterParam.Premium.Length; ++index)
        {
          PremiumParam premiumParam = MonoSingleton<GameManager>.Instance.MasterParam.Premium[index];
          if (premiumParam.m_BeginAt == 0L && premiumParam.m_EndAt == 0L)
            return premiumParam.m_Span;
        }
      }
      return 0;
    }

    public bool IsGuerrillaShopOpen()
    {
      bool flag = false;
      if (MonoSingleton<GameManager>.Instance.Player.GuerrillaShopEnd != 0L && TimeManager.ServerTime < TimeManager.FromUnixTime(MonoSingleton<GameManager>.Instance.Player.GuerrillaShopEnd))
        flag = true;
      return flag;
    }

    public void DEBUG_REPAIR_STAMINA()
    {
      this.mStamina.val = this.mStamina.valMax;
      this.mStamina.Update();
    }

    public void DEBUG_ADD_GOLD(int gold)
    {
      PlayerData playerData = this;
      playerData.mGold = (OInt) ((int) playerData.mGold + gold);
    }

    public bool Deserialize(Json_PlayerData json, PlayerData.EDeserializeFlags flag)
    {
      if (json == null)
        return false;
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      if ((flag & PlayerData.EDeserializeFlags.Gold) == PlayerData.EDeserializeFlags.Gold)
      {
        this.mGold = (OInt) json.gold;
        this.mGoldBuyNum = (OInt) json.cnt_buygold;
      }
      if ((flag & PlayerData.EDeserializeFlags.Coin) == PlayerData.EDeserializeFlags.Coin && json.coin != null)
      {
        this.mFreeCoin = (OInt) json.coin.free;
        this.mPaidCoin = (OInt) json.coin.paid;
        this.mComCoin = (OInt) json.coin.com;
      }
      if ((flag & PlayerData.EDeserializeFlags.Stamina) == PlayerData.EDeserializeFlags.Stamina)
      {
        if (json.stamina != null)
        {
          this.mStamina.val = (OInt) json.stamina.pt;
          this.mStamina.valMax = (OInt) json.stamina.max;
          this.mStamina.valRecover = fixParam.StaminaRecoveryVal;
          this.mStamina.interval = fixParam.StaminaRecoverySec;
          this.mStamina.at = (OLong) json.stamina.at;
        }
        this.mStaminaBuyNum = (OInt) json.cnt_stmrecover;
      }
      if ((flag & PlayerData.EDeserializeFlags.Cave) == PlayerData.EDeserializeFlags.Cave && json.cave != null)
      {
        this.mCaveStamina.val = (OInt) json.cave.pt;
        this.mCaveStamina.valMax = fixParam.CaveStaminaMax;
        this.mCaveStamina.valRecover = fixParam.CaveStaminaRecoveryVal;
        this.mCaveStamina.interval = fixParam.CaveStaminaRecoverySec;
        this.mCaveStamina.at = (OLong) json.cave.at;
      }
      if ((flag & PlayerData.EDeserializeFlags.AbilityUp) == PlayerData.EDeserializeFlags.AbilityUp && json.abilup != null)
      {
        this.mAbilityRankUpCount.val = (OInt) json.abilup.num;
        this.mAbilityRankUpCount.valMax = fixParam.AbilityRankUpCountMax;
        this.mAbilityRankUpCount.valRecover = fixParam.AbilityRankUpCountRecoveryVal;
        this.mAbilityRankUpCount.interval = fixParam.AbilityRankUpCountRecoverySec;
        this.mAbilityRankUpCount.at = (OLong) json.abilup.at;
      }
      if ((flag & PlayerData.EDeserializeFlags.Arena) == PlayerData.EDeserializeFlags.Arena && json.arena != null)
      {
        this.mChallengeArenaNum = (OInt) json.arena.num;
        this.mChallengeArenaTimer.valMax = (OInt) 1;
        this.mChallengeArenaTimer.valRecover = (OInt) 1;
        this.mChallengeArenaTimer.interval = fixParam.ChallengeArenaCoolDownSec;
        this.mChallengeArenaTimer.at = (OLong) json.arena.at;
        this.mArenaResetCount = json.arena.cnt_resetcost;
      }
      if ((flag & PlayerData.EDeserializeFlags.Tour) == PlayerData.EDeserializeFlags.Tour && json.tour != null)
        this.mTourNum = (OInt) json.tour.num;
      return true;
    }

    public bool Deserialize(Json_ArenaPlayers json)
    {
      if (json == null)
        return false;
      this.mArenaRank = json.rank_myself;
      this.mBestArenaRank = json.best_myself;
      this.mArenaLastAt = GameUtility.UnixtimeToLocalTime(json.btl_at);
      GlobalVars.SelectedQuestID = !string.IsNullOrEmpty(json.quest_iname) ? json.quest_iname : string.Empty;
      this.mArenaSeed = json.seed;
      this.mArenaMaxActionNum = json.maxActionNum;
      this.mArenaEndAt = GameUtility.UnixtimeToLocalTime(json.end_at);
      if (json.end_at != 0L && this.mArenaEndAt < TimeManager.ServerTime)
        this.mArenaEndAt = GameUtility.UnixtimeToLocalTime(0L);
      return true;
    }

    public void SetGvGMaxActionNum(int maxActionNum) => this.mGvGMaxActionNum = maxActionNum;

    public bool Deserialize(Json_ArenaEnemies json)
    {
      if (json == null)
        return false;
      GlobalVars.SelectedQuestID = !string.IsNullOrEmpty(json.quest_iname) ? json.quest_iname : string.Empty;
      this.mArenaEndAt = GameUtility.UnixtimeToLocalTime(json.end_at);
      if (json.end_at != 0L && this.mArenaEndAt < TimeManager.ServerTime)
        this.mArenaEndAt = GameUtility.UnixtimeToLocalTime(0L);
      return true;
    }

    public void Deserialize(Json_PlayerData json)
    {
      this.mName = json != null ? json.name : throw new InvalidJSONException();
      this.mCuid = json.cuid;
      this.mFuid = json.fuid;
      this.mTuid = (string) null;
      this.mTuidExpiredAt = 0L;
      this.mExp = (OInt) json.exp;
      this.mGold = (OInt) json.gold;
      this.mLv = (OInt) this.CalcLevel();
      this.mUnitCap = (OInt) json.unitbox_max;
      this.mAbilityPoint = (OInt) json.abilpt;
      this.mFreeCoin = (OInt) 0;
      this.mPaidCoin = (OInt) 0;
      this.mComCoin = (OInt) 0;
      this.mTourCoin = (OInt) json.enseicoin;
      this.mArenaCoin = (OInt) json.arenacoin;
      this.mMultiCoin = (OInt) json.multicoin;
      this.mPiecePoint = (OInt) json.kakeracoin;
      this.mVipPoint = (OInt) 0;
      this.mVipRank = (OInt) 0;
      this.mNewGameAt = (OInt) json.newgame_at;
      this.mLoginCount = json.logincont;
      if (json.multi != null)
      {
        this.mMultiInvitaionFlag = json.multi.is_multi_push != 0;
        this.mMultiInvitaionComment = json.multi.multi_comment;
      }
      if (json.vip != null)
        this.mVipExpiredAt = (OLong) json.vip.expired_at;
      if (json.premium != null)
        this.mPremiumExpiredAt = (OLong) json.premium.expired_at;
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      if (json.tuid != null)
      {
        this.mTuid = json.tuid.id;
        this.mTuidExpiredAt = json.tuid.expired_at;
      }
      if (json.coin != null)
      {
        this.mFreeCoin = (OInt) json.coin.free;
        this.mPaidCoin = (OInt) json.coin.paid;
        this.mComCoin = (OInt) json.coin.com;
      }
      if (json.stamina != null)
      {
        this.mStamina.val = (OInt) json.stamina.pt;
        this.mStamina.valMax = (OInt) json.stamina.max;
        this.mStamina.valRecover = fixParam.StaminaRecoveryVal;
        this.mStamina.interval = fixParam.StaminaRecoverySec;
        this.mStamina.at = (OLong) json.stamina.at;
      }
      if (json.cave != null)
      {
        this.mCaveStamina.val = (OInt) json.cave.pt;
        this.mCaveStamina.valMax = fixParam.CaveStaminaMax;
        this.mCaveStamina.valRecover = fixParam.CaveStaminaRecoveryVal;
        this.mCaveStamina.interval = fixParam.CaveStaminaRecoverySec;
        this.mCaveStamina.at = (OLong) json.cave.at;
      }
      if (json.abilup != null)
      {
        this.mAbilityRankUpCount.val = (OInt) json.abilup.num;
        this.mAbilityRankUpCount.valMax = fixParam.AbilityRankUpCountMax;
        this.mAbilityRankUpCount.valRecover = fixParam.AbilityRankUpCountRecoveryVal;
        this.mAbilityRankUpCount.interval = fixParam.AbilityRankUpCountRecoverySec;
        this.mAbilityRankUpCount.at = (OLong) json.abilup.at;
      }
      if (json.arena != null)
      {
        this.mChallengeArenaNum = (OInt) json.arena.num;
        this.mChallengeArenaTimer.val = (OInt) 0;
        this.mChallengeArenaTimer.valMax = (OInt) 1;
        this.mChallengeArenaTimer.valRecover = (OInt) 1;
        this.mChallengeArenaTimer.interval = fixParam.ChallengeArenaCoolDownSec;
        this.mChallengeArenaTimer.at = (OLong) json.arena.at;
        this.mArenaResetCount = json.arena.cnt_resetcost;
      }
      if (json.tour != null)
        this.mTourNum = (OInt) json.tour.num;
      if (json.gachag != null)
      {
        this.FreeGachaGold.num = json.gachag.num;
        this.FreeGachaGold.at = json.gachag.at;
      }
      if (json.gachac != null)
      {
        this.FreeGachaCoin.num = json.gachac.num;
        this.FreeGachaCoin.at = json.gachac.at;
      }
      if (json.gachap != null)
      {
        this.PaidGacha.num = json.gachap.num;
        this.PaidGacha.at = json.gachap.at;
      }
      if (json.friends != null)
      {
        this.mFriendNum = json.friends.friendnum;
        if (json.friends.follower != null)
        {
          this.mFollowerNum = json.friends.follower.Length;
          this.mFollowerUID.Clear();
          for (int index = 0; index < this.mFollowerNum; ++index)
            this.mFollowerUID.Add(json.friends.follower[index]);
        }
        else
          this.mFollowerNum = 0;
      }
      this.mUnreadMail = json.mail_f_unread == 0;
      this.mUnreadMailPeriod = json.mail_unread == 0;
      this.mChallengeMultiNum = (OInt) json.cnt_multi;
      this.mStaminaBuyNum = (OInt) json.cnt_stmrecover;
      this.mGoldBuyNum = (OInt) json.cnt_buygold;
      this.mSelectedAward = json.selected_award;
      if (json.g_shop != null)
      {
        long timeStart = json.g_shop.time_start;
        long timeEnd = json.g_shop.time_end;
        if (this.mGuerrillaShopStart != timeStart || this.mGuerrillaShopEnd != timeEnd)
        {
          DateTime serverTime = TimeManager.ServerTime;
          DateTime dateTime1 = TimeManager.FromUnixTime(json.g_shop.time_start);
          DateTime dateTime2 = TimeManager.FromUnixTime(json.g_shop.time_end);
          if (serverTime >= dateTime1 && serverTime < dateTime2)
            this.mIsGuerrillaShopStarted = true;
          this.mGuerrillaShopStart = timeStart;
          this.mGuerrillaShopEnd = timeEnd;
        }
      }
      this.UpdateUnlocks();
    }

    public void Deserialize(Json_TrophyPlayerData json)
    {
      this.mExp = json != null ? (OInt) json.exp : throw new InvalidJSONException();
      this.mGold = (OInt) json.gold;
      this.mLv = (OInt) this.CalcLevel();
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      if (json.coin != null)
      {
        this.mFreeCoin = (OInt) json.coin.free;
        this.mPaidCoin = (OInt) json.coin.paid;
        this.mComCoin = (OInt) json.coin.com;
      }
      if (json.stamina != null)
      {
        this.mStamina.val = (OInt) json.stamina.pt;
        this.mStamina.valMax = (OInt) json.stamina.max;
        this.mStamina.valRecover = fixParam.StaminaRecoveryVal;
        this.mStamina.interval = fixParam.StaminaRecoverySec;
        this.mStamina.at = (OLong) json.stamina.at;
      }
      this.UpdateUnlocks();
    }

    public void Deserialize(Json_Currencies json)
    {
      this.mGold = (OInt) json.gold;
      if (json.coin != null)
      {
        this.mFreeCoin = (OInt) json.coin.free;
        this.mPaidCoin = (OInt) json.coin.paid;
        this.mComCoin = (OInt) json.coin.com;
      }
      this.mArenaCoin = (OInt) json.arenacoin;
      this.mMultiCoin = (OInt) json.multicoin;
      this.mTourCoin = (OInt) json.enseicoin;
      this.mPiecePoint = (OInt) json.kakeracoin;
    }

    public void Deserialize(Json_MailInfo json)
    {
      this.mUnreadMail = json.mail_f_unread == 0;
      this.mUnreadMailPeriod = json.mail_unread == 0;
    }

    public void Deserialize(Json_Unit[] units)
    {
      if (units == null)
        return;
      if (this.mUnits == null)
        this.mUnits = new List<UnitData>((int) this.mUnitCap);
      for (int index = 0; index < units.Length; ++index)
      {
        UnitData unitData = this.FindUnitDataByUniqueID(units[index].iid);
        if (unitData == null)
        {
          unitData = new UnitData();
          this.mUnits.Add(unitData);
          this.mUniqueID2UnitData[units[index].iid] = unitData;
        }
        try
        {
          unitData.Deserialize(units[index]);
        }
        catch (Exception ex)
        {
          this.mUnits.Remove(unitData);
          this.mUniqueID2UnitData.Remove(units[index].iid);
          DebugUtility.LogException(ex);
        }
      }
      this.UpdateTotalCombatPower();
    }

    public void Deserialize(Json_Item[] items)
    {
      if (items == null)
        return;
      if (this.mItems == null)
        this.mItems = new List<ItemData>(items.Length);
      for (int index = 0; index < items.Length; ++index)
      {
        ItemData itemData = this.FindByItemID(items[index].iname);
        bool flag = false;
        if (itemData == null)
        {
          itemData = new ItemData();
          itemData.IsNew = true;
          this.mItems.Add(itemData);
          this.mID2ItemData[items[index].iname] = itemData;
          flag = true;
        }
        try
        {
          itemData.Deserialize(items[index]);
          if (flag)
            itemData.IsNewSkin = itemData.Param != null && itemData.Param.type == EItemType.UnitSkin;
        }
        catch (Exception ex)
        {
          this.mItems.Remove(itemData);
          this.mID2ItemData.Remove(items[index].iname);
          DebugUtility.LogException(ex);
        }
      }
      this.UpdateInventory();
    }

    public void Deserialize(Json_Artifact[] items, bool differenceUpdate = false)
    {
      if (items == null)
      {
        this.mArtifacts.Clear();
        this.mArtifactsNumByRarity.Clear();
      }
      else
      {
        for (int index = 0; index < items.Length; ++index)
        {
          bool flag = false;
          ArtifactData artifactData = this.FindArtifactByUniqueID(items[index].iid);
          if (artifactData == null)
          {
            artifactData = new ArtifactData();
            flag = true;
          }
          else
            this.RemoveArtifactNumByRarity(artifactData);
          try
          {
            artifactData.Deserialize(items[index]);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            continue;
          }
          if (flag)
            this.mArtifacts.Add(artifactData);
          this.AddArtifaceNumByRarity(artifactData);
        }
        if (differenceUpdate)
          return;
        int i = 0;
        while (i < this.mArtifacts.Count)
        {
          if (Array.Find<Json_Artifact>(items, (Predicate<Json_Artifact>) (p => p.iid == (long) this.mArtifacts[i].UniqueID)) != null)
          {
            ++i;
          }
          else
          {
            this.RemoveArtifactNumByRarity(this.mArtifacts[i]);
            this.mArtifacts.RemoveAt(i);
          }
        }
      }
    }

    public void Deserialize(JSON_ConceptCard concept_cards)
    {
      if (concept_cards == null)
        return;
      ConceptCardData conceptCardByUniqueId = this.FindConceptCardByUniqueID(concept_cards.iid);
      if (conceptCardByUniqueId != null)
      {
        try
        {
          conceptCardByUniqueId.Deserialize(concept_cards);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
        }
      }
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        int indexOfConceptCard = this.mUnits[index].FindIndexOfConceptCard(concept_cards.iid);
        if (indexOfConceptCard != -1)
        {
          this.mUnits[index].SetConceptCardByIndex(indexOfConceptCard, conceptCardByUniqueId);
          this.mUnits[index].UpdateConceptCardChanged();
        }
      }
    }

    public void Deserialize(JSON_ConceptCard[] concept_cards, bool is_data_override = true)
    {
      if (is_data_override && (concept_cards == null || concept_cards.Length <= 0))
      {
        this.mConceptCards.Clear();
        this.mConceptCardNum.Clear();
      }
      else
      {
        if (concept_cards == null)
          return;
        for (int index1 = 0; index1 < concept_cards.Length; ++index1)
        {
          ConceptCardData conceptCard = this.FindConceptCardByUniqueID(concept_cards[index1].iid);
          if (conceptCard == null)
          {
            try
            {
              conceptCard = new ConceptCardData();
              conceptCard.Deserialize(concept_cards[index1]);
              this.mConceptCards.Add(conceptCard);
            }
            catch (Exception ex)
            {
              DebugUtility.LogException(ex);
              continue;
            }
          }
          for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
          {
            int indexOfConceptCard = this.mUnits[index2].FindIndexOfConceptCard(concept_cards[index1].iid);
            if (indexOfConceptCard != -1)
              this.mUnits[index2].SetConceptCardByIndex(indexOfConceptCard, conceptCard);
          }
        }
        if (is_data_override)
        {
          int i = 0;
          while (i < this.mConceptCards.Count)
          {
            if (Array.Find<JSON_ConceptCard>(concept_cards, (Predicate<JSON_ConceptCard>) (p => p.iid == (long) this.mConceptCards[i].UniqueID)) != null)
            {
              ++i;
            }
            else
            {
              for (int index = 0; index < this.mUnits.Count; ++index)
              {
                int indexOfConceptCard = this.mUnits[index].FindIndexOfConceptCard((long) this.mConceptCards[i].UniqueID);
                if (indexOfConceptCard != -1)
                  this.mUnits[index].SetConceptCardByIndex(indexOfConceptCard, (ConceptCardData) null);
              }
              this.mConceptCards.RemoveAt(i);
            }
          }
        }
        this.UpdateConceptCardNum();
      }
    }

    public void Deserialize(
      JSON_ConceptCardMaterial[] concept_card_materials,
      bool is_data_override = true)
    {
      if (is_data_override && (concept_card_materials == null || concept_card_materials.Length <= 0))
      {
        this.mConceptCardExpMaterials.Clear();
        this.mConceptCardTrustMaterials.Clear();
      }
      else
      {
        if (concept_card_materials == null)
          return;
        this.mConceptCardExpMaterials.Clear();
        this.mConceptCardTrustMaterials.Clear();
        for (int index = 0; index < concept_card_materials.Length; ++index)
        {
          ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(concept_card_materials[index].iname);
          if (conceptCardParam != null)
          {
            try
            {
              ConceptCardMaterialData cardMaterialData = new ConceptCardMaterialData();
              cardMaterialData.Deserialize(concept_card_materials[index]);
              if (conceptCardParam.type == eCardType.Enhance_exp)
                this.mConceptCardExpMaterials.Add(cardMaterialData);
              else if (conceptCardParam.type == eCardType.Enhance_trust)
                this.mConceptCardTrustMaterials.Add(cardMaterialData);
            }
            catch (Exception ex)
            {
              DebugUtility.LogException(ex);
            }
          }
        }
      }
    }

    public void Deserialize(string[] inames)
    {
      this.mSkinConceptCards.Clear();
      if (inames == null)
        return;
      for (int index = 0; index < inames.Length; ++index)
      {
        SkinConceptCardData skinConceptCardData = new SkinConceptCardData();
        skinConceptCardData.Deserialize(inames[index]);
        this.mSkinConceptCards.Add(skinConceptCardData);
      }
    }

    public void Deserialize(Json_Skin[] skins)
    {
      if (this.mSkins == null)
        this.mSkins = new List<string>();
      this.mSkins.Clear();
      if (skins == null || skins.Length < 1)
        return;
      for (int index = 0; index < skins.Length; ++index)
      {
        if (skins[index] != null && !string.IsNullOrEmpty(skins[index].iname))
          this.mSkins.Add(skins[index].iname);
      }
    }

    public void Deserialize(Json_Party[] parties)
    {
      for (int index = 0; index < this.mPartys.Count; ++index)
        this.mPartys[index].Reset();
      if (parties == null)
        throw new InvalidJSONException();
      for (int index1 = 0; index1 < parties.Length; ++index1)
      {
        if (parties[index1] != null)
        {
          int num = index1;
          if (!string.IsNullOrEmpty(parties[index1].ptype))
            num = (int) PartyData.GetPartyTypeFromString(parties[index1].ptype);
          this.mPartys[num].Deserialize(parties[index1]);
          int lastSelectionIndex = 0;
          PartyWindow2.EditPartyTypes editPartyType = ((PlayerPartyTypes) num).ToEditPartyType();
          if (PartyUtility.LoadTeamPresets(editPartyType, out lastSelectionIndex) == null)
          {
            int maxTeamCount = editPartyType.GetMaxTeamCount();
            List<PartyEditData> teams = new List<PartyEditData>();
            for (int index2 = 0; index2 < maxTeamCount; ++index2)
            {
              PartyEditData partyEditData = new PartyEditData(PartyUtility.CreateDefaultPartyNameFromIndex(index2), this.mPartys[num]);
              teams.Add(partyEditData);
            }
            PartyUtility.SaveTeamPresets(editPartyType, 0, teams);
          }
        }
      }
    }

    public bool Deserialize(Json_Mail[] mails)
    {
      this.Mails.Clear();
      if (mails == null)
        return true;
      for (int index = 0; index < mails.Length; ++index)
      {
        MailData mailData = new MailData();
        if (!mailData.Deserialize(mails[index]))
        {
          DebugUtility.Assert("Failed Mail Deserialize.");
          return false;
        }
        this.Mails.Add(mailData);
      }
      return true;
    }

    public bool Deserialize(Json_Mails mails)
    {
      this.MailPage = new MailPageData();
      if (mails == null)
        return false;
      this.MailPage.Deserialize(mails.list);
      this.MailPage.Deserialize(mails.option);
      return true;
    }

    public void Deserialize(Json_Friend[] friends)
    {
      this.Deserialize(friends, FriendStates.Friend);
      this.Deserialize(friends, FriendStates.Follwer);
      this.Deserialize(friends, FriendStates.Follow);
    }

    public void Deserialize(Json_Friend[] friends, FriendStates state)
    {
      switch (state)
      {
        case FriendStates.Friend:
          this.Friends.Clear();
          break;
        case FriendStates.Follow:
          this.FriendsFollow.Clear();
          break;
        case FriendStates.Follwer:
          this.FriendsFollower.Clear();
          break;
        default:
          return;
      }
      if (friends == null)
        return;
      for (int index = 0; index < friends.Length; ++index)
      {
        FriendData friendData = new FriendData();
        try
        {
          friendData.Deserialize(friends[index]);
          if (friendData.State == state)
          {
            switch (friendData.State)
            {
              case FriendStates.Friend:
                this.Friends.Add(friendData);
                continue;
              case FriendStates.Follow:
                this.FriendsFollow.Add(friendData);
                continue;
              case FriendStates.Follwer:
                this.FriendsFollower.Add(friendData);
                continue;
              default:
                continue;
            }
          }
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
        }
      }
      if (state != FriendStates.Friend)
      {
        if (state != FriendStates.Follwer)
          return;
        this.FollowerNum = this.FriendsFollower.Count;
      }
      else
        this.FriendNum = this.Friends.Count;
    }

    public void Deserialize(Json_Support[] supports)
    {
      this.Supports.Clear();
      if (supports == null)
        return;
      for (int index = 0; index < supports.Length; ++index)
      {
        SupportData supportData = new SupportData();
        try
        {
          supportData.Deserialize(supports[index]);
          this.Supports.Add(supportData);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
        }
      }
    }

    public void Deserialize(Json_MultiFuids[] fuids)
    {
      this.MultiFuids.Clear();
      if (fuids == null)
        return;
      for (int index = 0; index < fuids.Length; ++index)
      {
        MultiFuid multiFuid = new MultiFuid();
        try
        {
          multiFuid.Deserialize(fuids[index]);
          this.MultiFuids.Add(multiFuid);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
        }
      }
    }

    public void Deserialize(FriendPresentWishList.Json[] jsons)
    {
      try
      {
        this.FriendPresentWishList.Clear();
        if (jsons == null)
          return;
        this.FriendPresentWishList.Deserialize(jsons);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
      }
    }

    public void Deserialize(FriendPresentReceiveList.Json[] jsons)
    {
      try
      {
        this.FriendPresentReceiveList.Clear();
        if (jsons == null)
          return;
        this.FriendPresentReceiveList.Deserialize(jsons);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
      }
    }

    public bool Deserialize(Json_Notify notify)
    {
      if (notify == null)
        return true;
      if (this.mServerNotify == null)
        this.mServerNotify = notify;
      if (AssetManager.UseDLC)
        this.InitLoginBonusTable();
      return true;
    }

    public bool Deserialize(Json_Notify_Monthly notify)
    {
      if (notify == null)
      {
        DebugUtility.LogError("更新されたログインボーナスがありません.");
        return false;
      }
      if (AssetManager.UseDLC)
        this.UpdateLoginBonusTable(notify);
      return true;
    }

    public void InitLoginBonusTable()
    {
      Json_Notify mServerNotify = this.mServerNotify;
      if (mServerNotify == null)
        return;
      this.mFirstLogin = (mServerNotify.bonus >> 5 & 1) != 0;
      this.mLoginBonusCount = mServerNotify.bonus & 31;
      this.mLoginBonus = mServerNotify.logbonus;
      this.mLoginBonus28days = (Json_LoginBonusTable) null;
      this.mPremiumLoginBonus = (Json_LoginBonusTable) null;
      Json_LoginBonusTable[] loginBonusData = this.CreateLoginBonusData(mServerNotify.logbotables);
      this.mServerNotify = (Json_Notify) null;
      for (int index = 0; index < loginBonusData.Length; ++index)
      {
        Json_LoginBonusTable jsonLoginBonusTable = loginBonusData[index];
        if (jsonLoginBonusTable != null && !string.IsNullOrEmpty(jsonLoginBonusTable.type))
        {
          this.mLoginBonusTables[jsonLoginBonusTable.type] = jsonLoginBonusTable;
          if (this.mFirstLogin)
            this.mLoginBonusQueue.Enqueue(jsonLoginBonusTable.type);
          if (jsonLoginBonusTable.bonus_units != null && jsonLoginBonusTable.premium_bonuses == null)
            this.mLoginBonus28days = jsonLoginBonusTable;
          else if (jsonLoginBonusTable.login_days != null && jsonLoginBonusTable.login_days.Length > 0)
            this.mLoginBonus30days = jsonLoginBonusTable;
          else if (jsonLoginBonusTable.premium_bonuses != null)
            this.mPremiumLoginBonus = jsonLoginBonusTable;
        }
      }
      this.SupportCount = 1;
      this.SupportGold = mServerNotify.supgold;
    }

    public void UpdateLoginBonusTable(Json_Notify_Monthly notify)
    {
      foreach (Json_LoginBonusTable jsonLoginBonusTable in this.CreateLoginBonusData(notify.tables))
      {
        if (jsonLoginBonusTable != null && !string.IsNullOrEmpty(jsonLoginBonusTable.type) && this.mLoginBonusTables.ContainsKey(jsonLoginBonusTable.type))
        {
          this.mLoginBonusTables[jsonLoginBonusTable.type] = jsonLoginBonusTable;
          this.mLoginBonus30days = jsonLoginBonusTable;
        }
      }
    }

    public Json_LoginBonusTable[] CreateLoginBonusData(Json_LoginBonusTable[] originals)
    {
      if (originals == null)
        return (Json_LoginBonusTable[]) null;
      List<Dictionary<string, object>> bonusTableFromJson = this.CreateLoginBonusTableFromJson();
      if (bonusTableFromJson == null || bonusTableFromJson.Count < 1)
        return (Json_LoginBonusTable[]) null;
      List<Json_LoginBonusTable> jsonLoginBonusTableList = new List<Json_LoginBonusTable>();
      foreach (Json_LoginBonusTable original in originals)
      {
        Json_LoginBonusTable loginBonusTable = this.CreateLoginBonusTable(bonusTableFromJson, original);
        if (loginBonusTable != null)
          jsonLoginBonusTableList.Add(loginBonusTable);
      }
      return jsonLoginBonusTableList.ToArray();
    }

    private Json_LoginBonusTable CreateLoginBonusTable(
      List<Dictionary<string, object>> tables,
      Json_LoginBonusTable orig)
    {
      DebugUtility.Log("[" + orig.type + "] のログインボーナステーブル生成開始");
      Dictionary<string, object> root = (Dictionary<string, object>) null;
      if (tables.FirstOrDefault<Dictionary<string, object>>((Func<Dictionary<string, object>, bool>) (table => table.TryGetValueAndCast<Dictionary<string, object>>(orig.type, out root))) == null)
      {
        DebugUtility.LogError("ログインボーナスの[" + orig.type + "] が見つかりません。");
        return (Json_LoginBonusTable) null;
      }
      Json_LoginBonusTable loginBonusTable = new Json_LoginBonusTable();
      loginBonusTable.count = orig.count;
      loginBonusTable.type = orig.type;
      loginBonusTable.lastday = orig.lastday;
      loginBonusTable.login_days = orig.login_days;
      loginBonusTable.remain_recover = orig.remain_recover;
      root.TryGetValueAndCast<string>("prefab", out loginBonusTable.prefab);
      Dictionary<string, object> val1;
      if (root.TryGetValueAndCast<Dictionary<string, object>>("units", out val1))
      {
        string[] orderedJsonObject = this.SortToIntegerOrderedJsonObject<string>(val1);
        if (orderedJsonObject == null)
        {
          DebugUtility.LogError("[" + orig.type + "] のフォーマットエラー: unitsのキーは整数値、値は文字列を設定してください。");
          return (Json_LoginBonusTable) null;
        }
        loginBonusTable.bonus_units = orderedJsonObject;
      }
      root.TryGetValueAndCast<int>("recover", out loginBonusTable.max_recover);
      string val2 = string.Empty;
      if (root.TryGetValueAndCast<string>("begin", out val2))
      {
        DateTime dateTime = DateTime.Parse(val2);
        loginBonusTable.current_month = dateTime.Month;
      }
      Dictionary<string, object> val3;
      if (!root.TryGetValueAndCast<Dictionary<string, object>>("table", out val3, true))
      {
        DebugUtility.LogError("[" + orig.type + "] のフォーマットエラー: tableが見つかりません。");
        return (Json_LoginBonusTable) null;
      }
      bool flag = false;
      string val4;
      if (root.TryGetValueAndCast<string>("type", out val4) && (val4 == "Premium" || val4 == "Monthly"))
        flag = true;
      if (flag)
        loginBonusTable.premium_bonuses = this.CreatePremiumLoginBonusItemTable(val3);
      else
        loginBonusTable.bonuses = this.CreateLoginBonusItemTable(val3);
      DebugUtility.Log("[" + orig.type + "] のログインボーナス生成完了");
      return loginBonusTable;
    }

    public bool Deserialize(JSON_PlayerCoinBuyUseBonusState[] bonus_stats)
    {
      this.mCoinBuyUseBonusStateList.Clear();
      if (bonus_stats != null)
      {
        for (int index = 0; index < bonus_stats.Length; ++index)
        {
          PlayerCoinBuyUseBonusState buyUseBonusState = new PlayerCoinBuyUseBonusState();
          buyUseBonusState.Deserialize(bonus_stats[index]);
          this.mCoinBuyUseBonusStateList.Add(buyUseBonusState);
        }
      }
      return true;
    }

    public bool Deserialize(
      JSON_PlayerCoinBuyUseBonusRewardState[] reward_stats)
    {
      this.mCoinBuyUseBonusRewardStateList.Clear();
      if (reward_stats != null)
      {
        for (int index = 0; index < reward_stats.Length; ++index)
        {
          PlayerCoinBuyUseBonusRewardState bonusRewardState = new PlayerCoinBuyUseBonusRewardState();
          bonusRewardState.Deserialize(reward_stats[index]);
          this.mCoinBuyUseBonusRewardStateList.Add(bonusRewardState);
        }
      }
      return true;
    }

    public bool Deserialize(Json_AutoRepeatQuestData progress, bool override_drop = true)
    {
      this.mAutoRepeatQuestProgress.Deserialize(progress, override_drop);
      return true;
    }

    public bool Deserialize(JSON_PartyOverWrite[] ow_party)
    {
      if (ow_party == null)
        return false;
      if (this.mPartyOverWriteDatas == null)
        this.mPartyOverWriteDatas = new Dictionary<eOverWritePartyType, List<PartyOverWrite>>();
      for (int index = 0; index < ow_party.Length; ++index)
      {
        eOverWritePartyType key = UnitOverWriteUtility.String2OverWritePartyType(ow_party[index].ptype);
        if (key != eOverWritePartyType.None)
        {
          if (!this.mPartyOverWriteDatas.ContainsKey(key))
            this.mPartyOverWriteDatas.Add(key, new List<PartyOverWrite>());
          this.mPartyOverWriteDatas[key].Clear();
          PartyOverWrite partyOverWrite = new PartyOverWrite();
          partyOverWrite.Deserialize(ow_party[index]);
          this.mPartyOverWriteDatas[key].Add(partyOverWrite);
        }
      }
      return true;
    }

    private T[] SortToIntegerOrderedJsonObject<T>(Dictionary<string, object> json) where T : class
    {
      List<KeyValuePair<int, T>> source = new List<KeyValuePair<int, T>>();
      foreach (KeyValuePair<string, object> keyValuePair in json)
      {
        int result = 0;
        if (!int.TryParse(keyValuePair.Key, out result))
        {
          DebugUtility.LogError("キー [" + keyValuePair.Key + "] を整数にキャストできません。");
          return (T[]) null;
        }
        if (!(keyValuePair.Value is T obj))
        {
          DebugUtility.LogError("[" + keyValuePair.Value + "] は" + keyValuePair.Value.GetType().Name + "です。" + typeof (T).Name + "にはキャストできません。");
          return (T[]) null;
        }
        source.Add(new KeyValuePair<int, T>(result, obj));
      }
      return source.OrderBy<KeyValuePair<int, T>, int>((Func<KeyValuePair<int, T>, int>) (kv => kv.Key)).Select<KeyValuePair<int, T>, T>((Func<KeyValuePair<int, T>, T>) (kv => kv.Value)).ToArray<T>();
    }

    private List<Dictionary<string, object>> CreateLoginBonusTableFromJson()
    {
      List<Dictionary<string, object>> bonusTableFromJson = new List<Dictionary<string, object>>();
      string[] strArray = new string[3]
      {
        AssetManager.LoadTextData("Data/CountupBonusTable"),
        AssetManager.LoadTextData("Data/PremiumLoginBonus"),
        AssetManager.LoadTextData("Data/MonthlyBonusTable")
      };
      foreach (string json in strArray)
      {
        Dictionary<string, object> dictionary = LoginBonusJsonParser.Deserialize(json);
        if (dictionary != null)
          bonusTableFromJson.Add(dictionary);
      }
      return bonusTableFromJson;
    }

    private Json_LoginBonus[] CreateLoginBonusItemTable(Dictionary<string, object> json)
    {
      Dictionary<string, object>[] orderedJsonObject = this.SortToIntegerOrderedJsonObject<Dictionary<string, object>>(json);
      List<Json_LoginBonus> jsonLoginBonusList = new List<Json_LoginBonus>();
      foreach (Dictionary<string, object> json1 in orderedJsonObject)
      {
        Json_LoginBonus jsonLoginBonus = new Json_LoginBonus();
        Dictionary<string, object> val1;
        if (json1.TryGetValueAndCast<Dictionary<string, object>>("item", out val1))
        {
          val1.TryGetValueAndCast<string>("iname", out jsonLoginBonus.iname);
          val1.TryGetValueAndCast<int>("num", out jsonLoginBonus.num);
        }
        Dictionary<string, object> val2;
        if (json1.TryGetValueAndCast<Dictionary<string, object>>("coin", out val2))
          val2.TryGetValueAndCast<int>("num", out jsonLoginBonus.coin);
        Dictionary<string, object> val3;
        if (json1.TryGetValueAndCast<Dictionary<string, object>>("vip", out val3))
        {
          Json_LoginBonusVip jsonLoginBonusVip = new Json_LoginBonusVip();
          val3.TryGetValueAndCast<int>("lv", out jsonLoginBonusVip.lv);
          jsonLoginBonus.vip = jsonLoginBonusVip;
        }
        jsonLoginBonusList.Add(jsonLoginBonus);
      }
      return jsonLoginBonusList.ToArray();
    }

    private Json_PremiumLoginBonus[] CreatePremiumLoginBonusItemTable(
      Dictionary<string, object> json)
    {
      Dictionary<string, object>[] orderedJsonObject1 = this.SortToIntegerOrderedJsonObject<Dictionary<string, object>>(json);
      List<Json_PremiumLoginBonus> premiumLoginBonusList = new List<Json_PremiumLoginBonus>();
      foreach (Dictionary<string, object> json1 in orderedJsonObject1)
      {
        Json_PremiumLoginBonus premiumLoginBonus = new Json_PremiumLoginBonus();
        Dictionary<string, object> val1;
        if (json1.TryGetValueAndCast<Dictionary<string, object>>("item", out val1))
        {
          List<Json_PremiumLoginBonusItem> premiumLoginBonusItemList = new List<Json_PremiumLoginBonusItem>();
          Dictionary<string, object>[] orderedJsonObject2 = this.SortToIntegerOrderedJsonObject<Dictionary<string, object>>(val1);
          if (orderedJsonObject2 != null)
          {
            foreach (Dictionary<string, object> json2 in orderedJsonObject2)
            {
              Json_PremiumLoginBonusItem premiumLoginBonusItem = new Json_PremiumLoginBonusItem();
              string val2;
              if (json2.TryGetValueAndCast<string>("iname", out val2))
                premiumLoginBonusItem.iname = val2;
              int val3;
              if (json2.TryGetValueAndCast<int>("num", out val3))
                premiumLoginBonusItem.num = val3;
              premiumLoginBonusItemList.Add(premiumLoginBonusItem);
            }
          }
          premiumLoginBonus.item = premiumLoginBonusItemList.ToArray();
        }
        json1.TryGetValueAndCast<string>("icon", out premiumLoginBonus.icon);
        json1.TryGetValueAndCast<int>("coin", out premiumLoginBonus.coin);
        json1.TryGetValueAndCast<int>("gold", out premiumLoginBonus.gold);
        premiumLoginBonusList.Add(premiumLoginBonus);
      }
      return premiumLoginBonusList.ToArray();
    }

    public void Deserialize(Json_Versus json)
    {
      VERSUS_TYPE type = VERSUS_TYPE.Free;
      this.mVersusPoint = json.point;
      if (json.counts == null)
        return;
      for (int index = 0; index < json.counts.Length; ++index)
      {
        if (string.Compare(json.counts[index].type, VERSUS_TYPE.Free.ToString().ToLower()) == 0)
          type = VERSUS_TYPE.Free;
        else if (string.Compare(json.counts[index].type, VERSUS_TYPE.Tower.ToString().ToLower()) == 0)
          type = VERSUS_TYPE.Tower;
        else if (string.Compare(json.counts[index].type, VERSUS_TYPE.Friend.ToString().ToLower()) == 0)
          type = VERSUS_TYPE.Friend;
        this.SetVersusWinCount(type, json.counts[index].win);
        this.SetVersusTotalCount(type, json.counts[index].win + json.counts[index].lose);
      }
    }

    public void SetVersusWinCount(VERSUS_TYPE type, int wincnt)
    {
      if (type != VERSUS_TYPE.Free && type != VERSUS_TYPE.Tower && type != VERSUS_TYPE.Friend)
        return;
      this.mVersusWinCount[(int) type] = wincnt;
    }

    public void AddVersusTotalCount(VERSUS_TYPE type, int addcnt)
    {
      if (type != VERSUS_TYPE.Free && type != VERSUS_TYPE.Tower && type != VERSUS_TYPE.Friend)
        return;
      this.mVersusTotalCount[(int) type] = addcnt + this.mVersusTotalCount[(int) type];
    }

    public void SetVersusTotalCount(VERSUS_TYPE type, int cnt)
    {
      if (type != VERSUS_TYPE.Free && type != VERSUS_TYPE.Tower && type != VERSUS_TYPE.Friend)
        return;
      this.mVersusTotalCount[(int) type] = cnt;
    }

    public void SetVersusRankpoint(int point) => this.mVersusPoint = point;

    public void SetHaveAward(string[] awards)
    {
      if (awards == null || awards.Length <= 0)
        return;
      this.mHaveAward.Clear();
      for (int index = 0; index < awards.Length; ++index)
      {
        if (!string.IsNullOrEmpty(awards[index]))
          this.mHaveAward.Add(awards[index]);
      }
    }

    public void Deserialize(JSON_PlayerGuild player_guild)
    {
      this.mPlayerGuild = (PlayerGuildData) null;
      if (player_guild == null)
        return;
      try
      {
        this.mPlayerGuild = new PlayerGuildData();
        this.mPlayerGuild.Deserialize(player_guild);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
      }
    }

    public void Deserialize(JSON_Guild guild)
    {
      if (guild != null)
      {
        try
        {
          if (this.mGuild == null)
            this.mGuild = new GuildData();
          this.mGuild.Deserialize(guild);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
        }
      }
      else
        this.mGuild = (GuildData) null;
    }

    public void Deserialize(
      JSON_StoryExChallengeCount story_ex_challenge_count)
    {
      if (story_ex_challenge_count == null)
        return;
      this.mStoryExChallengeCountData.Deserialize(story_ex_challenge_count);
    }

    public void Deserialize(Json_ExpansionPurchase[] expansions)
    {
      this.mExpansions.Clear();
      if (expansions == null || expansions.Length <= 0)
        return;
      for (int index = 0; index < expansions.Length; ++index)
      {
        ExpansionPurchaseData expansionPurchaseData = new ExpansionPurchaseData();
        expansionPurchaseData.Deserialize(expansions[index]);
        this.mExpansions.Add(expansionPurchaseData);
      }
    }

    public bool IsFirstLogin => this.mFirstLogin;

    public void ForceFirstLogin() => this.mFirstLogin = true;

    public int LoginCountWithType(string type)
    {
      return string.IsNullOrEmpty(type) || !this.mLoginBonusTables.ContainsKey(type) ? 0 : this.mLoginBonusTables[type].count;
    }

    public Json_LoginBonus RecentLoginBonus
    {
      get
      {
        return this.LoginBonus != null && 0 < this.mLoginBonusCount && this.mLoginBonusCount <= this.LoginBonus.Length ? this.LoginBonus[this.mLoginBonusCount - 1] : (Json_LoginBonus) null;
      }
    }

    public Json_LoginBonus FindRecentLoginBonus(string type)
    {
      Json_LoginBonus[] loginBonuses = this.FindLoginBonuses(type);
      if (loginBonuses == null)
        return (Json_LoginBonus) null;
      int num = this.LoginCountWithType(type);
      return num < 1 || loginBonuses.Length < num ? (Json_LoginBonus) null : loginBonuses[num - 1];
    }

    public Json_LoginBonusTable LoginBonus28days => this.mLoginBonus28days;

    public Json_LoginBonus[] LoginBonus => this.mLoginBonus;

    public Json_LoginBonusTable LoginBonus30days => this.mLoginBonus30days;

    public Json_LoginBonus[] FindLoginBonuses(string type)
    {
      if (string.IsNullOrEmpty(type))
        return this.mLoginBonus;
      return !this.mLoginBonusTables.ContainsKey(type) ? (Json_LoginBonus[]) null : this.mLoginBonusTables[type].bonuses;
    }

    public Json_PremiumLoginBonus[] FindPremiumLoginBonuses(string type)
    {
      if (string.IsNullOrEmpty(type))
        return (Json_PremiumLoginBonus[]) null;
      return !this.mLoginBonusTables.ContainsKey(type) ? (Json_PremiumLoginBonus[]) null : this.mLoginBonusTables[type].premium_bonuses;
    }

    public Json_LoginBonusTable PremiumLoginBonus => this.mPremiumLoginBonus;

    public void ResetPremiumLoginBonus() => this.mPremiumLoginBonus = (Json_LoginBonusTable) null;

    public int LoginBonusCount => this.mLoginBonusCount;

    public string GetLoginBonusePrefabName(string type)
    {
      if (string.IsNullOrEmpty(type))
        return (string) null;
      return !this.mLoginBonusTables.ContainsKey(type) ? (string) null : this.mLoginBonusTables[type].prefab;
    }

    public string[] GetLoginBonuseUnitIDs(string type)
    {
      if (string.IsNullOrEmpty(type))
        return (string[]) null;
      return !this.mLoginBonusTables.ContainsKey(type) ? (string[]) null : this.mLoginBonusTables[type].bonus_units;
    }

    public bool IsLastLoginBonus(string type)
    {
      return !string.IsNullOrEmpty(type) && this.mLoginBonusTables.ContainsKey(type) && this.mLoginBonusTables[type].lastday > 0;
    }

    public int[] GetLoginBonusLoginDays(string type)
    {
      if (string.IsNullOrEmpty(type))
        return (int[]) null;
      return !this.mLoginBonusTables.ContainsKey(type) ? (int[]) null : this.mLoginBonusTables[type].login_days;
    }

    public int GetLoginBonusRemainRecover(string type)
    {
      return string.IsNullOrEmpty(type) || !this.mLoginBonusTables.ContainsKey(type) ? -1 : this.mLoginBonusTables[type].remain_recover;
    }

    public int GetLoginBonusMaxRecover(string type)
    {
      return string.IsNullOrEmpty(type) || !this.mLoginBonusTables.ContainsKey(type) ? -1 : this.mLoginBonusTables[type].max_recover;
    }

    public int GetLoginBonusCurrentMonth(string type)
    {
      return string.IsNullOrEmpty(type) || !this.mLoginBonusTables.ContainsKey(type) ? -1 : this.mLoginBonusTables[type].current_month;
    }

    public bool IsLoginBonusTable(string type)
    {
      return !string.IsNullOrEmpty(type) && this.mLoginBonusTables.ContainsKey(type);
    }

    public void SetRuneStorageNum(int value)
    {
      if (value >= (int) short.MaxValue)
        DebugUtility.LogError("mRuneStorage の value のサイズがオーバーフローするよ");
      else
        this.mRuneStorage = (short) value;
    }

    public void SetRuneStorageUsedNum(int value)
    {
      if (value >= (int) short.MaxValue)
        DebugUtility.LogError("mRuneNum value のサイズがオーバーフローするよ");
      else
        this.mRuneStorageUsed = (short) value;
    }

    public void Deserialize(ReqGetRune.Response json, bool is_data_override)
    {
      if (json == null)
        return;
      this.Deserialize(json.runes, is_data_override);
      this.Deserialize(json.rune_enforce_gauge);
      this.mRuneStorage = (short) json.rune_storage;
    }

    public void Deserialize(Json_RuneEnforceGaugeData[] rune_enforce_gauge)
    {
      if (rune_enforce_gauge == null || rune_enforce_gauge == null)
        return;
      for (int index = 0; index < rune_enforce_gauge.Length; ++index)
      {
        RuneEnforceGaugeData gauge = new RuneEnforceGaugeData();
        gauge.Deserialize(rune_enforce_gauge[index]);
        if (this.mRuneEnforceGauge != null && this.mRuneEnforceGauge.Count > 0)
        {
          RuneEnforceGaugeData enforceGaugeData = this.mRuneEnforceGauge.Find((Predicate<RuneEnforceGaugeData>) (r => (int) r.rare == (int) gauge.rare));
          if (enforceGaugeData != null)
            enforceGaugeData.val = gauge.val;
          else
            this.mRuneEnforceGauge.Add(gauge);
        }
        else
          this.mRuneEnforceGauge.Add(gauge);
      }
    }

    public void Deserialize(Json_RuneData[] runes, bool is_data_override)
    {
      if (is_data_override && (runes == null || runes.Length <= 0))
      {
        this.mRunes.Clear();
      }
      else
      {
        if (runes == null || this.mRunes == null)
          return;
        for (int index = 0; index < runes.Length; ++index)
          this.Deserialize(runes[index]);
      }
    }

    public void Deserialize(Json_RuneData rune)
    {
      if (rune == null || this.mRunes == null)
        return;
      RuneData runeData = this.FindRuneByUniqueID(rune.iid);
      bool flag = runeData == null;
      if (flag)
        runeData = new RuneData();
      try
      {
        runeData.Deserialize(rune);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return;
      }
      if (flag)
      {
        this.mRunes.Add((long) runeData.UniqueID, runeData);
      }
      else
      {
        UnitData unitData = this.Units.Find((Predicate<UnitData>) (ud => ud.UniqueID == (long) rune.unit_id));
        if (unitData == null)
          return;
        for (int index = 0; index < unitData.EquipRunes.Length; ++index)
        {
          RuneData equipRune = unitData.EquipRunes[index];
          if (equipRune != null && (long) equipRune.UniqueID == rune.iid)
          {
            unitData.EquipRunes[index] = runeData;
            break;
          }
        }
        unitData.UpdateRuneChanged();
      }
    }

    public void Deserialize(ReqRuneStorageAdd.Response json)
    {
      if (json == null)
        return;
      this.mRuneStorage = (short) json.rune_storage;
    }

    public void Deserialize(ReqRuneFavorite.Response json)
    {
      if (json == null || json.rune == null)
        return;
      RuneData runeByUniqueId;
      if ((runeByUniqueId = this.FindRuneByUniqueID(json.rune.iid)) == null)
        return;
      runeByUniqueId.IsFavorite = json.rune.IsFavorite;
    }

    public bool CheckUnlock(UnlockTargets target)
    {
      return ((UnlockTargets) (long) this.mUnlocks & target) != (UnlockTargets) 0;
    }

    public void SetParty(int index, PartyData party)
    {
      if (index < 0 || index > this.mPartys.Count - 1)
        return;
      this.mPartys[index].SetParty(party);
    }

    public UnitData FindUnitDataByUnitID(string iname)
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (iname == this.mUnits[index].UnitParam.iname)
          return this.mUnits[index];
      }
      return (UnitData) null;
    }

    public UnitData FindUnitDataByUniqueID(long iid)
    {
      try
      {
        return this.mUniqueID2UnitData[iid];
      }
      catch (Exception ex)
      {
        return (UnitData) null;
      }
    }

    public UnitData FindUnitDataByUniqueParam(UnitParam unit)
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (unit == this.mUnits[index].UnitParam)
          return this.mUnits[index];
      }
      return (UnitData) null;
    }

    public PartyData FindPartyOfType(PlayerPartyTypes type) => this.mPartys[(int) type];

    public void SetQuestState(string name, QuestStates st)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(name);
      if (quest == null)
        return;
      quest.state = st;
      this.mQuestListDirty = true;
    }

    public void ResetQuestStates()
    {
      QuestParam[] quests = MonoSingleton<GameManager>.Instance.Quests;
      for (int index = quests.Length - 1; index >= 0; --index)
        quests[index].state = QuestStates.New;
      this.mQuestListDirty = true;
    }

    public void ResetQuestChallengeResets()
    {
      QuestParam[] quests = MonoSingleton<GameManager>.Instance.Quests;
      for (int index = quests.Length - 1; index >= 0; --index)
      {
        if (quests[index].dayReset > 0)
        {
          DateTime dateTime = TimeManager.FromUnixTime(quests[index].end - quests[index].start);
          if (quests[index].dayReset >= dateTime.Day)
            quests[index].dailyReset = (short) 0;
        }
      }
      this.mQuestListDirty = true;
    }

    public void ResetQuestChallenges()
    {
      QuestParam[] quests = MonoSingleton<GameManager>.Instance.Quests;
      for (int index = quests.Length - 1; index >= 0; --index)
      {
        if (quests[index].dayReset > 0)
        {
          DateTime dateTime = TimeManager.FromUnixTime(quests[index].end - quests[index].start);
          if (quests[index].dayReset >= dateTime.Day)
            quests[index].dailyCount = (short) 0;
        }
      }
      this.mQuestListDirty = true;
    }

    public void MarkQuestChallenged(string name)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(name);
      if (quest == null || quest.state != QuestStates.New)
        return;
      this.SetQuestState(name, QuestStates.Challenged);
    }

    public void MarkQuestCleared(string name) => this.SetQuestState(name, QuestStates.Cleared);

    public QuestParam FindLastStoryQuest()
    {
      QuestParam[] availableQuests = this.AvailableQuests;
      int num = 0;
      string iname = PlayerPrefsUtility.GetString(PlayerPrefsUtility.LAST_SELECTED_STORY_QUEST_ID, string.Empty);
      if (!string.IsNullOrEmpty(iname))
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(iname);
        if (quest != null && quest.Chapter != null && quest.Chapter.sectionParam != null && quest.Chapter.sectionParam.storyPart > 0)
          num = quest.Chapter.sectionParam.storyPart;
      }
      for (int index1 = 0; index1 < availableQuests.Length; ++index1)
      {
        if (availableQuests[index1].IsStory && !string.IsNullOrEmpty(availableQuests[index1].ChapterID) && (num <= 0 || availableQuests[index1].Chapter == null || availableQuests[index1].Chapter.sectionParam == null || num == availableQuests[index1].Chapter.sectionParam.storyPart))
        {
          QuestParam lastStoryQuest = availableQuests[index1];
          for (int index2 = index1 + 1; index2 < availableQuests.Length; ++index2)
          {
            if (availableQuests[index2].IsStory && (num <= 0 || availableQuests[index2].Chapter == null || availableQuests[index2].Chapter.sectionParam == null || num == availableQuests[index2].Chapter.sectionParam.storyPart))
            {
              lastStoryQuest = availableQuests[index2];
              if (availableQuests[index2].state != QuestStates.Cleared)
                return availableQuests[index2];
            }
          }
          return lastStoryQuest;
        }
      }
      return (QuestParam) null;
    }

    public void SetQuestMissionFlags(string name, bool[] missions)
    {
      int missions1 = 0;
      if (missions != null)
      {
        for (int index = 0; index < missions.Length; ++index)
        {
          if (missions[index])
            missions1 |= 1 << index;
        }
      }
      this.SetQuestMissionFlags(name, missions1);
    }

    public void SetQuestMissionFlags(string name, int missions)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(name);
      if (quest == null)
        return;
      quest.clear_missions |= missions;
    }

    public bool IsQuestArchiveOpenByArea(string chapterIname)
    {
      ArchiveParam archiveByArea = MonoSingleton<GameManager>.Instance.FindArchiveByArea(chapterIname);
      return archiveByArea != null && this.IsQuestArchiveOpen(archiveByArea.iname);
    }

    public bool IsQuestArchiveOpen(string iname)
    {
      return !string.IsNullOrEmpty(iname) && this.OpenedQuestArchives != null && this.OpenedQuestArchives.Any<OpenedQuestArchive>((Func<OpenedQuestArchive, bool>) (t => t.iname.Equals(iname))) && this.OpenedQuestArchives.Find((Predicate<OpenedQuestArchive>) (t => t.iname.Equals(iname))).end_at > TimeManager.ServerTime;
    }

    public OpenedQuestArchive GetOpenedQuestArchive(string iname)
    {
      OpenedQuestArchive openedQuestArchive = this.OpenedQuestArchives.Find((Predicate<OpenedQuestArchive>) (t => t.iname.Equals(iname)));
      return openedQuestArchive != null && openedQuestArchive.end_at > TimeManager.ServerTime ? openedQuestArchive : (OpenedQuestArchive) null;
    }

    public void SetQuestChallengeNumDaily(string name, int num)
    {
      MonoSingleton<GameManager>.Instance.FindQuest(name)?.SetChallangeCount(num);
    }

    public void IncrementQuestChallangeNumDaily(string name)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(name);
      if (quest == null)
        return;
      int num = quest.GetChallangeCount() + 1;
      this.SetQuestChallengeNumDaily(name, num);
      if (quest.Chapter == null)
        return;
      quest.Chapter.IncrementChallangeCount();
    }

    public bool HasItem(string iname)
    {
      ItemData itemDataByItemId = this.FindItemDataByItemID(iname);
      return itemDataByItemId != null && itemDataByItemId.Num > 0;
    }

    public int GetItemAmount(string iname)
    {
      ItemData itemDataByItemId = this.FindItemDataByItemID(iname);
      return itemDataByItemId != null ? itemDataByItemId.Num : 0;
    }

    public ItemData FindItemDataByItemID(string iname, bool is_all = false)
    {
      if (string.IsNullOrEmpty(iname))
        return (ItemData) null;
      return is_all ? this.mItems.Find((Predicate<ItemData>) (p => p.ItemID == iname)) : this.mItems.Find((Predicate<ItemData>) (p => p.ItemID == iname && !p.Param.IsExpire));
    }

    public ItemData FindItemDataByItemParam(ItemParam param)
    {
      return this.mItems.Find((Predicate<ItemData>) (p => p.Param == param && !p.Param.IsExpire));
    }

    public ItemData FindInventoryByItemID(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (ItemData) null;
      for (int index = 0; index < this.mInventory.Length; ++index)
      {
        if (this.mInventory[index] != null && iname == this.mInventory[index].ItemID)
          return this.mInventory[index];
      }
      return (ItemData) null;
    }

    public bool IsExistExpireItem_Inventory()
    {
      for (int index = 0; index < this.Inventory.Length; ++index)
      {
        if (this.Inventory[index] != null && this.Inventory[index].Param.IsExpire)
          return true;
      }
      return false;
    }

    public ArtifactData FindArtifactByUniqueID(long iid)
    {
      return this.mArtifacts.Find((Predicate<ArtifactData>) (p => (long) p.UniqueID == iid));
    }

    public List<ArtifactData> FindArtifactsByIDs(HashSet<string> ids)
    {
      return this.mArtifacts.FindAll((Predicate<ArtifactData>) (artifact => ids.Contains(artifact.ArtifactParam.iname)));
    }

    public List<ArtifactData> FindArtifactsByArtifactID(string iname)
    {
      return this.mArtifacts.FindAll((Predicate<ArtifactData>) (p => p.ArtifactParam.iname == iname));
    }

    public List<ArtifactData> GetInspirationSkillLvUpArtifacts(
      ArtifactData source_artifact,
      InspirationSkillData source_insp_skill)
    {
      if (source_artifact == null || this.mArtifacts == null || this.mArtifacts.Count <= 0)
        return new List<ArtifactData>();
      ArtifactParam source_arti_param = source_artifact.ArtifactParam;
      AbilityParam source_ability_param = source_insp_skill.AbilityData.Param;
      List<ArtifactData> all = this.mArtifacts.FindAll((Predicate<ArtifactData>) (artifact => (long) artifact.UniqueID != (long) source_artifact.UniqueID && artifact.UsableInspirationSkillLvUp(source_arti_param, source_ability_param)));
      if (all == null || all.Count <= 0)
        return new List<ArtifactData>();
      InspSkillParam inspSkillParam = source_insp_skill.InspSkillParam;
      return source_artifact.InspMaterialListSort(inspSkillParam, all);
    }

    public void RemoveArtifactByUniqueID(long iid)
    {
      int index = this.mArtifacts.FindIndex((Predicate<ArtifactData>) (p => (long) p.UniqueID == iid));
      if (index < 0)
        return;
      this.mArtifacts.RemoveAt(index);
    }

    public bool FindOwner(
      ArtifactData arti,
      out UnitData unit,
      out JobData job,
      bool is_include_over_write = false)
    {
      unit = (UnitData) null;
      job = (JobData) null;
      if (is_include_over_write)
      {
        List<UnitData> mUnits = this.mUnits;
        if (arti.GetOwner(mUnits, out unit, out job))
          return true;
        unit = UnitOverWriteUtility.GetOwner(arti);
        if (unit == null)
          return false;
        job = unit.CurrentJob;
        return true;
      }
      List<UnitData> mUnits1 = this.mUnits;
      if (!UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
        return arti.GetOwner(mUnits1, out unit, out job);
      unit = UnitOverWriteUtility.GetOwner(arti, (eOverWritePartyType) GlobalVars.OverWritePartyType);
      if (unit == null)
        return false;
      job = unit.CurrentJob;
      return true;
    }

    public void SetPartyCurrentIndex(int index)
    {
      for (int index1 = 0; index1 < this.mPartys.Count; ++index1)
        this.mPartys[index1].Selected = index == index1;
    }

    public int GetDefensePartyIndex()
    {
      for (int index = 1; index < this.mPartys.Count; ++index)
      {
        if (this.mPartys[index].IsDefense)
          return index;
      }
      return 0;
    }

    public void SetDefenseParty(int index)
    {
      for (int index1 = 0; index1 < this.mPartys.Count; ++index1)
        this.mPartys[index1].IsDefense = index == index1;
    }

    public int GetPartyCurrentIndex()
    {
      for (int index = 0; index < this.mPartys.Count; ++index)
      {
        if (this.mPartys[index].Selected)
          return index;
      }
      return 0;
    }

    public PartyData GetPartyCurrent() => this.Partys[this.GetPartyCurrentIndex()];

    public void AutoSetLeaderUnit()
    {
      List<UnitData> units = MonoSingleton<GameManager>.Instance.Player.Units;
      if (units.Count <= 0)
        return;
      for (int index1 = 0; index1 < this.mPartys.Count; ++index1)
      {
        PartyData mParty = this.mPartys[index1];
        if (mParty.GetUnitUniqueID(0) == 0L)
        {
          for (int index2 = 0; index2 < units.Count; ++index2)
          {
            UnitData unitData = units[index2];
            if (unitData != null)
            {
              bool flag = false;
              for (int index3 = 0; index3 < mParty.MAX_UNIT; ++index3)
              {
                if (mParty.GetUnitUniqueID(index3) == unitData.UniqueID)
                {
                  flag = true;
                  break;
                }
              }
              if (!flag)
              {
                mParty.SetUnitUniqueID(0, unitData.UniqueID);
                break;
              }
            }
          }
        }
      }
    }

    public static int CalcLevelFromExp(int current)
    {
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      int playerLevelCap = masterParam.GetPlayerLevelCap();
      int num = 0;
      int val1 = 0;
      for (int index = 0; index < playerLevelCap; ++index)
      {
        num += masterParam.GetPlayerNextExp(index + 1);
        if (num > current)
          return val1;
        ++val1;
      }
      return Math.Min(Math.Max(val1, 1), playerLevelCap);
    }

    public int CalcLevel() => PlayerData.CalcLevelFromExp((int) this.mExp);

    public int GetLevelExp()
    {
      return MonoSingleton<GameManager>.Instance.MasterParam.GetPlayerNextExp((int) this.mLv);
    }

    public int GetExp()
    {
      return (int) this.mExp - MonoSingleton<GameManager>.Instance.MasterParam.GetPlayerLevelExp((int) this.mLv);
    }

    public int GetNextExp()
    {
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      int playerLevelCap = masterParam.GetPlayerLevelCap();
      int num = 0;
      for (int index = 0; index < playerLevelCap; ++index)
      {
        num += masterParam.GetPlayerNextExp(index + 1);
        if (num > (int) this.mExp)
          return num - (int) this.mExp;
      }
      return 0;
    }

    public void GainExp(int exp)
    {
      int mLv = (int) this.mLv;
      PlayerData playerData = this;
      playerData.mExp = (OInt) ((int) playerData.mExp + exp);
      this.mLv = (OInt) this.CalcLevel();
      if (mLv == (int) this.mLv)
        return;
      this.PlayerLevelUp((int) this.mLv - mLv);
    }

    private void PlayerLevelUp(int delta)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      this.mStamina.valMax = instance.MasterParam.GetPlayerParam((int) this.mLv).pt;
      this.mStamina.val = (OInt) Math.Min((int) this.mStamina.val + (int) instance.MasterParam.FixParam.StaminaAdd2 * delta, this.StaminaStockCap);
      this.UpdateUnlocks();
      if (Network.Mode != Network.EConnectMode.Offline)
        return;
      this.SavePlayerPrefs();
    }

    public static int CalcVipRankFromPoint(int current)
    {
      if (current == 0)
        return 0;
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      int num1 = 0;
      int num2 = 0;
      int vipRankCap = masterParam.GetVipRankCap();
      for (int index = 0; index < vipRankCap; ++index)
      {
        num1 += masterParam.GetVipRankNextPoint(index + 1);
        if (num1 <= current)
          ++num2;
      }
      return num2;
    }

    public void GainVipPoint(int point)
    {
    }

    public void AddPaymentInfo(string productId, int num = 1)
    {
      if (this.PaymentInfos.ContainsKey(productId))
        this.PaymentInfos[productId].AddNum(num);
      else
        this.PaymentInfos.Add(productId, new PaymentInfo(productId, num));
    }

    public void UpdateUnlocks()
    {
      UnlockTargets unlockTargets = (UnlockTargets) 0;
      this.mUnlocks = (OLong) 0L;
      foreach (UnlockParam unlock in MonoSingleton<GameManager>.Instance.MasterParam.Unlocks)
      {
        if (unlock != null)
        {
          unlockTargets |= unlock.UnlockTarget;
          if (unlock.PlayerLevel <= this.Lv && unlock.VipRank <= this.VipRank && this.IsClearUnclockConditions_Quest(unlock.ClearQuests))
          {
            PlayerData playerData = this;
            playerData.mUnlocks = (OLong) (long) ((UnlockTargets) (long) playerData.mUnlocks | unlock.UnlockTarget);
          }
        }
      }
      PlayerData playerData1 = this;
      playerData1.mUnlocks = (OLong) (long) ((UnlockTargets) (long) playerData1.mUnlocks | ~unlockTargets);
    }

    public bool IsClearUnclockConditions_Quest(string[] conds_quests)
    {
      if (conds_quests == null || conds_quests.Length <= 0)
        return true;
      for (int index = 0; index < conds_quests.Length; ++index)
      {
        if (!string.IsNullOrEmpty(conds_quests[index]) && !this.IsQuestCleared(conds_quests[index]))
          return false;
      }
      return true;
    }

    public void GainGold(int gold) => this.mGold = (OInt) Math.Max((int) this.mGold + gold, 0);

    private ItemData FindByItemID(string itemID)
    {
      try
      {
        return this.mID2ItemData[itemID];
      }
      catch (Exception ex)
      {
        return (ItemData) null;
      }
    }

    public bool CheckFreeGachaGold()
    {
      DateTime dateTime1 = TimeManager.FromUnixTime(Network.GetServerTime());
      DateTime dateTime2 = TimeManager.FromUnixTime(this.FreeGachaGold.at);
      if (dateTime1.Year < dateTime2.Year || dateTime1.Month < dateTime2.Month || dateTime1.Day < dateTime2.Day || this.FreeGachaGold.num == 0)
        return true;
      return this.FreeGachaGold.num != (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.FreeGachaGoldMax && this.GetNextFreeGachaGoldCoolDownSec() == 0L;
    }

    public bool CheckFreeGachaGoldMax()
    {
      DateTime dateTime1 = TimeManager.FromUnixTime(Network.GetServerTime());
      DateTime dateTime2 = TimeManager.FromUnixTime(this.FreeGachaGold.at);
      return dateTime1.Year >= dateTime2.Year && dateTime1.Month >= dateTime2.Month && dateTime1.Day >= dateTime2.Day && this.FreeGachaGold.num == (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.FreeGachaGoldMax;
    }

    public long GetNextFreeGachaGoldCoolDownSec()
    {
      long serverTime = Network.GetServerTime();
      DateTime dateTime1 = TimeManager.FromUnixTime(serverTime);
      DateTime dateTime2 = TimeManager.FromUnixTime(this.FreeGachaGold.at);
      return dateTime1.Year < dateTime2.Year || dateTime1.Month < dateTime2.Month || dateTime1.Day < dateTime2.Day ? 0L : Math.Max((long) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.FreeGachaGoldCoolDownSec - (serverTime - this.FreeGachaGold.at), 0L);
    }

    public bool CheckFreeGachaCoin() => this.GetNextFreeGachaCoinCoolDownSec() == 0L;

    public long GetNextFreeGachaCoinCoolDownSec()
    {
      return Math.Max((long) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.FreeGachaCoinCoolDownSec - (Network.GetServerTime() - this.FreeGachaCoin.at), 0L);
    }

    public bool CheckPaidGacha() => this.PaidGacha.num == 0;

    public void SetInventory(int index, ItemData item)
    {
      if (0 > index || index >= this.mInventory.Length)
        return;
      this.mInventory[index] = item;
    }

    public void SaveInventory()
    {
      for (int index = 0; index < this.mInventory.Length; ++index)
      {
        if (this.mInventory[index] != null)
          PlayerPrefsUtility.SetString(PlayerPrefsUtility.PLAYERDATA_INVENTORY + (object) index, this.mInventory[index].ItemID);
        else
          PlayerPrefsUtility.DeleteKey(PlayerPrefsUtility.PLAYERDATA_INVENTORY + (object) index);
      }
    }

    public void UpdateInventory()
    {
      for (int index = 0; index < this.mInventory.Length; ++index)
      {
        this.mInventory[index] = (ItemData) null;
        if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.PLAYERDATA_INVENTORY + (object) index))
        {
          string iname = PlayerPrefsUtility.GetString(PlayerPrefsUtility.PLAYERDATA_INVENTORY + (object) index, string.Empty);
          if (!string.IsNullOrEmpty(iname))
          {
            ItemData itemDataByItemId = this.FindItemDataByItemID(iname, true);
            if (itemDataByItemId != null)
              this.mInventory[index] = itemDataByItemId;
          }
        }
      }
    }

    public bool UseExpPotion(UnitData unit, ItemData item)
    {
      if (item == null || item.Param == null || item.Num <= 0 || item.ItemType != EItemType.ExpUpUnit)
        return false;
      unit.GainExp(item.Param.value, MonoSingleton<GameManager>.Instance.Player.Lv);
      item.Used(1);
      return true;
    }

    public bool CheckFriend(string fuid)
    {
      if (string.IsNullOrEmpty(fuid))
        return false;
      FriendData friendData = this.Friends.Find((Predicate<FriendData>) (p => p.FUID == fuid));
      return friendData != null && friendData.IsFriend();
    }

    public void RemoveFriendFollowerAll()
    {
      MonoSingleton<GameManager>.Instance.Player.FriendsFollower.Clear();
      this.FollowerNum = 0;
    }

    public void RemoveFriendFollower(string fuid)
    {
      if (string.IsNullOrEmpty(fuid))
        return;
      FriendData friendData = this.FriendsFollower.Find((Predicate<FriendData>) (p => p.FUID == fuid));
      if (friendData == null)
        return;
      this.FriendsFollower.Remove(friendData);
      --this.FollowerNum;
    }

    public bool CheckEnableEquipUnit(ItemParam item)
    {
      if (item == null || item.type != EItemType.Equip)
        return false;
      for (int index = 0; index < this.Units.Count; ++index)
      {
        if (this.Units[index].CheckEnableEquipment(item))
          return true;
      }
      return false;
    }

    public bool CheckEnableCreateItem(
      ItemParam param,
      bool root = true,
      int needNum = 1,
      NeedEquipItemList item_list = null)
    {
      bool is_ikkatsu = false;
      return this.CheckEnableCreateItem(param, ref is_ikkatsu, root, needNum, item_list);
    }

    public bool CheckEnableCreateItem(
      ItemParam param,
      ref bool is_ikkatsu,
      bool root = true,
      int needNum = 1,
      NeedEquipItemList item_list = null)
    {
      if (root)
      {
        this.mConsumeMaterials.Clear();
        this.mCreateItemCost = 0;
        is_ikkatsu = false;
      }
      if (param == null || string.IsNullOrEmpty(param.recipe))
      {
        if (item_list != null && param.IsCommon && (int) param.cmn_type - 1 == 2)
          item_list.Add(param, 1, true);
        return false;
      }
      RecipeParam recipe = param.Recipe;
      if (recipe == null || recipe.items == null)
        return false;
      this.mCreateItemCost += recipe.cost * needNum;
      bool flag = true;
      for (int index = 0; index < recipe.items.Length; ++index)
      {
        RecipeItem recipeItem = recipe.items[index];
        ItemData itemDataByItemId = this.FindItemDataByItemID(recipeItem.iname);
        int num1 = itemDataByItemId == null ? 0 : itemDataByItemId.Num;
        int num2 = recipeItem.num * needNum;
        if (this.mConsumeMaterials.ContainsKey(recipeItem.iname))
        {
          int num3 = Math.Min(Math.Max(num1 - this.mConsumeMaterials[recipeItem.iname], 0), num2);
          if (num3 > 0)
          {
            Dictionary<string, int> consumeMaterials;
            string iname;
            (consumeMaterials = this.mConsumeMaterials)[iname = recipeItem.iname] = consumeMaterials[iname] + num3;
            num2 -= num3;
          }
        }
        else
        {
          int num4 = Math.Min(num1, num2);
          if (num4 > 0)
          {
            this.mConsumeMaterials.Add(recipeItem.iname, num4);
            num2 -= num4;
          }
        }
        if (num2 > 0)
        {
          ItemParam itemParam = MonoSingleton<GameManager>.GetInstanceDirect().GetItemParam(recipeItem.iname);
          if (item_list != null)
          {
            bool is_common = itemParam.IsCommon && index == 0;
            if (is_common)
              item_list.Add(itemParam, num2);
            else if (!itemParam.IsCommon && string.IsNullOrEmpty(itemParam.recipe))
              item_list.IsNotEnough = true;
            item_list.SetRecipeTree(new RecipeTree(itemParam), is_common);
          }
          if (!this.CheckEnableCreateItem(itemParam, ref is_ikkatsu, false, num2, item_list))
            flag = false;
          item_list?.UpRecipeTree();
          if (itemParam.recipe != null)
            is_ikkatsu = true;
        }
      }
      return flag;
    }

    public bool CheckEnableCreateItem(
      ItemParam param,
      ref bool is_ikkatsu,
      ref int cost,
      ref Dictionary<string, int> consumes,
      NeedEquipItemList item_list = null)
    {
      return this.CheckEnableCreateItem(param, 1, ref is_ikkatsu, ref cost, ref consumes, item_list);
    }

    public bool CheckEnableCreateItem(
      ItemParam param,
      int count,
      ref bool is_ikkatsu,
      ref int cost,
      ref Dictionary<string, int> consumes,
      NeedEquipItemList item_list = null)
    {
      bool flag = this.CheckEnableCreateItem(param, ref is_ikkatsu, needNum: count, item_list: item_list);
      cost = this.mCreateItemCost;
      consumes = this.mConsumeMaterials;
      return flag;
    }

    public int GetCreateItemCost(ItemParam param)
    {
      bool is_ikkatsu = false;
      this.CheckEnableCreateItem(param, ref is_ikkatsu);
      return this.mCreateItemCost;
    }

    public bool GetEnableCreateEquipItem(
      UnitData self,
      EquipData equip,
      ref int cost,
      NeedEquipItemList item_list = null)
    {
      if (equip == null || equip.ItemParam.equipLv > self.Lv)
      {
        if (item_list != null)
          item_list.IsNotEnough = true;
        return false;
      }
      if (equip.IsEquiped())
        return false;
      ItemData itemDataByItemParam = this.FindItemDataByItemParam(equip.ItemParam);
      int num1 = itemDataByItemParam == null ? 0 : itemDataByItemParam.Num;
      int num2 = 1;
      if (this.mConsumeMaterials.ContainsKey(equip.ItemID))
      {
        int num3 = Math.Min(Math.Max(num1 - this.mConsumeMaterials[equip.ItemID], 0), num2);
        if (num3 > 0)
        {
          Dictionary<string, int> consumeMaterials;
          string itemId;
          (consumeMaterials = this.mConsumeMaterials)[itemId = equip.ItemID] = consumeMaterials[itemId] + num3;
          num2 -= num3;
        }
      }
      else
      {
        int num4 = Math.Min(num1, num2);
        if (num4 > 0)
        {
          this.mConsumeMaterials.Add(equip.ItemID, num4);
          num2 -= num4;
        }
      }
      return num2 == 0 || this.CheckEnableCreateItem(equip.ItemParam, false, num2) && this.Gold >= cost;
    }

    public void GetEnableCreateEquipItemAll(
      UnitData self,
      EquipData[] equips,
      ref bool[] equipFlags,
      ref Dictionary<string, int> consume,
      ref int cost,
      NeedEquipItemList item_list = null)
    {
      if (self == null || equips == null || equipFlags == null)
        return;
      int num = 0;
      this.mConsumeMaterials.Clear();
      this.mCreateItemCost = 0;
      Dictionary<string, int> dictionary = new Dictionary<string, int>((IDictionary<string, int>) this.mConsumeMaterials);
      for (int index = 0; index < equips.Length; ++index)
      {
        equipFlags[index] = this.GetEnableCreateEquipItem(self, equips[index], ref cost, item_list);
        if (equipFlags[index])
        {
          dictionary = new Dictionary<string, int>((IDictionary<string, int>) this.mConsumeMaterials);
          num = this.mCreateItemCost;
        }
        else
        {
          this.mConsumeMaterials = new Dictionary<string, int>((IDictionary<string, int>) dictionary);
          this.mCreateItemCost = num;
        }
      }
      consume = this.mConsumeMaterials;
      cost = this.mCreateItemCost;
    }

    public void GetEnableCreateEquipItemAll(
      UnitData self,
      EquipData[] equips,
      ref bool[] equipFlags,
      NeedEquipItemList item_list = null)
    {
      this.GetEnableCreateEquipItemAll(self, equips, ref equipFlags, ref this.mConsumeMaterials, ref this.mCreateItemCost, item_list);
    }

    public bool CheckEnableCreateEquipItemAll(
      UnitData self,
      EquipData[] equips,
      ref Dictionary<string, int> consume,
      ref int cost,
      NeedEquipItemList item_list = null)
    {
      if (self == null || equips == null)
        return false;
      this.mConsumeMaterials.Clear();
      this.mCreateItemCost = 0;
      for (int index = 0; index < equips.Length; ++index)
      {
        EquipData equip = equips[index];
        if (equip == null || equip.ItemParam.equipLv > self.Lv)
        {
          if (item_list != null)
            item_list.IsNotEnough = true;
          return false;
        }
        if (!equip.IsEquiped())
        {
          ItemData itemDataByItemParam = this.FindItemDataByItemParam(equip.ItemParam);
          int num1 = itemDataByItemParam == null ? 0 : itemDataByItemParam.Num;
          int num2 = 1;
          if (this.mConsumeMaterials.ContainsKey(equip.ItemID))
          {
            int num3 = Math.Min(Math.Max(num1 - this.mConsumeMaterials[equip.ItemID], 0), num2);
            if (num3 > 0)
            {
              Dictionary<string, int> consumeMaterials;
              string itemId;
              (consumeMaterials = this.mConsumeMaterials)[itemId = equip.ItemID] = consumeMaterials[itemId] + num3;
              num2 -= num3;
            }
          }
          else
          {
            int num4 = Math.Min(num1, num2);
            if (num4 > 0)
            {
              this.mConsumeMaterials.Add(equip.ItemID, num4);
              num2 -= num4;
            }
          }
          if (num2 != 0 && !this.CheckEnableCreateItem(equips[index].ItemParam, false, num2, item_list))
          {
            if (equips[index].ItemParam.Recipe == null && (int) equips[index].ItemParam.cmn_type - 1 != 2)
            {
              if (item_list != null)
                item_list.IsNotEnough = true;
              return false;
            }
            if (item_list == null || !item_list.IsEnoughCommon())
              return false;
          }
        }
      }
      consume = this.mConsumeMaterials;
      cost = this.mCreateItemCost;
      if (this.Gold >= cost)
        return true;
      if (item_list != null)
        item_list.IsNotEnough = true;
      return false;
    }

    public bool CheckEnableCreateEquipItemAll(
      UnitData self,
      EquipData[] equips,
      NeedEquipItemList item_list = null)
    {
      return this.CheckEnableCreateEquipItemAll(self, equips, ref this.mConsumeMaterials, ref this.mCreateItemCost, item_list);
    }

    public bool CheckEnable2(
      UnitData self,
      EquipData[] equips_base,
      ref Dictionary<string, int> consume,
      ref int cost,
      ref int target_rank,
      ref bool can_jobmaster,
      ref bool can_jobmax,
      NeedEquipItemList item_list = null)
    {
      JobParam jobParam = MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(self.CurrentJob.JobID);
      int rank = self.CurrentJob.Rank;
      int jobRankCap = self.CurrentJob.GetJobRankCap(self);
      this.mConsumeMaterials.Clear();
      this.mCreateItemCost = 0;
      EquipData[] equips = new EquipData[6];
      for (int lv = rank; lv <= jobRankCap; ++lv)
      {
        bool equipItemAll2;
        if (lv == rank)
        {
          equipItemAll2 = this.CheckEnableCreateEquipItemAll2(self, equips_base, item_list);
        }
        else
        {
          for (int index = 0; index < equips.Length; ++index)
          {
            equips[index] = new EquipData();
            equips[index].Setup(jobParam.GetRankupItemID(lv, index));
          }
          equipItemAll2 = this.CheckEnableCreateEquipItemAll2(self, equips, item_list);
        }
        if (equipItemAll2)
        {
          if (jobRankCap == JobParam.MAX_JOB_RANK && lv == jobRankCap && equipItemAll2)
            can_jobmaster = true;
          if (lv == jobRankCap)
            can_jobmax = true;
          consume = new Dictionary<string, int>((IDictionary<string, int>) this.mConsumeMaterials);
          cost = this.mCreateItemCost;
          target_rank = Mathf.Min(lv + 1, jobRankCap);
        }
        else
          break;
      }
      return true;
    }

    public bool CheckEnableCreateEquipItemAll2(
      UnitData self,
      EquipData[] equips,
      NeedEquipItemList item_list = null)
    {
      if (self == null || equips == null)
        return false;
      for (int index = 0; index < equips.Length; ++index)
      {
        EquipData equip = equips[index];
        if (equip == null || string.IsNullOrEmpty(equip.ItemID) || equip.ItemParam.equipLv > self.Lv)
          return false;
        if (!equip.IsEquiped())
        {
          ItemData itemDataByItemParam = this.FindItemDataByItemParam(equip.ItemParam);
          int num1 = itemDataByItemParam == null ? 0 : itemDataByItemParam.Num;
          int num2 = 1;
          if (this.mConsumeMaterials.ContainsKey(equip.ItemID))
          {
            int num3 = Math.Min(Math.Max(num1 - this.mConsumeMaterials[equip.ItemID], 0), num2);
            if (num3 > 0)
            {
              Dictionary<string, int> consumeMaterials;
              string itemId;
              (consumeMaterials = this.mConsumeMaterials)[itemId = equip.ItemID] = consumeMaterials[itemId] + num3;
              num2 -= num3;
            }
          }
          else
          {
            int num4 = Math.Min(num1, num2);
            if (num4 > 0)
            {
              this.mConsumeMaterials.Add(equip.ItemID, num4);
              num2 -= num4;
            }
          }
          if (num2 != 0 && !this.CheckEnableCreateItem(equip.ItemParam, false, num2, item_list) && (item_list == null || !item_list.IsEnoughCommon()))
          {
            item_list?.Remove();
            return false;
          }
        }
      }
      if (this.Gold >= this.mCreateItemCost)
        return true;
      if (item_list != null)
        item_list.IsNotEnough = true;
      return false;
    }

    public bool SetUnitEquipment(UnitData unit, int slotIndex)
    {
      if (!unit.CurrentJob.CheckEnableEquipSlot(slotIndex))
      {
        Debug.LogError((object) "指定スロットに装備を装着する事はできません。");
        return false;
      }
      ItemData itemDataByItemId = this.FindItemDataByItemID(unit.GetRankupEquipData(unit.JobIndex, slotIndex).ItemID);
      if (itemDataByItemId == null || itemDataByItemId.Num <= 0)
      {
        Debug.LogError((object) "装備アイテムを所持していません。");
        return false;
      }
      unit.CurrentJob.Equip(slotIndex);
      unit.CalcStatus();
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.Unit);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.ItemEquipment);
      return true;
    }

    public bool RarityUpUnit(UnitData unit)
    {
      if (!unit.CheckUnitRarityUp())
        return false;
      RecipeParam rarityUpRecipe = unit.GetRarityUpRecipe();
      if (rarityUpRecipe.cost > (int) this.mGold || !unit.UnitRarityUp())
        return false;
      PlayerData playerData = this;
      playerData.mGold = (OInt) ((int) playerData.mGold - rarityUpRecipe.cost);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.Unit);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.UnitUnlock);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.ItemEquipment);
      return true;
    }

    private void ConsumeAwakePieces(UnitData unit, int num)
    {
      ItemData itemDataByItemId1 = this.FindItemDataByItemID(unit.UnitParam.piece);
      ItemData itemDataByItemId2 = this.FindItemDataByItemID(unit.UnitParam.piece);
      ItemData itemDataByItemId3 = this.FindItemDataByItemID(unit.UnitParam.piece);
      if (itemDataByItemId1 != null && itemDataByItemId1.Num > 0)
      {
        int num1 = itemDataByItemId1.Num < num ? itemDataByItemId1.Num : num;
        itemDataByItemId1.Used(num1);
        num -= num1;
      }
      if (num < 1)
        return;
      if (itemDataByItemId2 != null && itemDataByItemId2.Num > 0)
      {
        int num2 = itemDataByItemId2.Num < num ? itemDataByItemId2.Num : num;
        itemDataByItemId2.Used(num2);
        num -= num2;
      }
      if (num < 1)
        return;
      if (itemDataByItemId3 != null && itemDataByItemId3.Num > 0)
      {
        int num3 = itemDataByItemId3.Num < num ? itemDataByItemId3.Num : num;
        itemDataByItemId3.Used(num3);
        num -= num3;
      }
      if (num < 1)
        return;
      Debug.LogError((object) ("減算できていない欠片個数: " + (object) num));
    }

    public bool AwakingUnit(UnitData unit)
    {
      if (!unit.CheckUnitAwaking())
        return false;
      int awakeNeedPieces = unit.GetAwakeNeedPieces();
      if (!unit.UnitAwaking())
        return false;
      this.ConsumeAwakePieces(unit, awakeNeedPieces);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.Unit);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.UnitUnlock);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.ItemEquipment);
      return true;
    }

    public List<ItemData> GetJobRankUpReturnItemData(UnitData self, int jobNo, bool ignoreEquiped = false)
    {
      return self.GetJobRankUpReturnItemData(jobNo, ignoreEquiped);
    }

    public bool JobRankUpUnit(UnitData unit, int jobIndex)
    {
      if (!unit.CheckJobRankUpAllEquip(jobIndex))
        return false;
      unit.JobRankUp(jobIndex);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.Unit);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.UnitUnlock);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.ItemEquipment);
      return true;
    }

    public bool ClassChangeUnit(UnitData unit, int index)
    {
      if (!unit.CheckJobClassChange(index))
        return false;
      unit.JobClassChange(index);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.Unit);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.UnitUnlock);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.ItemEquipment);
      return true;
    }

    public bool CheckRankUpAbility(AbilityData ability)
    {
      return ability.Rank < ability.GetRankCap() && this.AbilityRankUpCountNum != 0 && this.Gold >= MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityNextGold(ability.Rank);
    }

    public bool RankUpAbility(AbilityData ability, bool is_update_badges = true)
    {
      if (!this.CheckRankUpAbility(ability))
        return false;
      this.GainGold(-ability.GetNextGold());
      ability.GainExp();
      this.mAbilityRankUpCount.val = (OInt) Math.Max((int) --this.mAbilityRankUpCount.val, 0);
      if (is_update_badges)
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
      return true;
    }

    public void GainItem(string itemID, int num)
    {
      ItemData byItemId = this.FindByItemID(itemID);
      if (byItemId == null)
      {
        ItemData itemData = new ItemData();
        itemData.Setup(0L, itemID, num);
        itemData.IsNew = true;
        itemData.IsNewSkin = itemData.Param != null && itemData.Param.type == EItemType.UnitSkin;
        this.mItems.Add(itemData);
      }
      else
        byItemId.Gain(num);
    }

    public void GainUnit(string unitID)
    {
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(unitID);
      UnitData unitData = new UnitData();
      List<long> longList = new List<long>();
      foreach (UnitData mUnit in this.mUnits)
        longList.Add(mUnit.UniqueID);
      long uniqueID = 1;
      for (long index = 1; index < 1000L; ++index)
      {
        bool flag = false;
        foreach (long num in longList)
        {
          if (index == num)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          uniqueID = index;
          break;
        }
      }
      Json_Unit json = new Json_Unit()
      {
        iid = uniqueID,
        iname = unitParam.iname,
        exp = 0,
        lv = 1,
        plus = 0,
        rare = 0,
        select = new Json_UnitSelectable()
      };
      json.select.job = 0L;
      json.jobs = (Json_Job[]) null;
      json.abil = (Json_MasterAbility) null;
      if (unitParam.jobsets != null && unitParam.jobsets.Length > 0)
      {
        List<Json_Job> jsonJobList = new List<Json_Job>(unitParam.jobsets.Length);
        int num = 1;
        for (int index = 0; index < unitParam.jobsets.Length; ++index)
        {
          JobSetParam jobSetParam = MonoSingleton<GameManager>.Instance.GetJobSetParam(unitParam.jobsets[index]);
          if (jobSetParam != null)
            jsonJobList.Add(new Json_Job()
            {
              iid = (long) num++,
              iname = jobSetParam.job,
              rank = 0,
              equips = (Json_Equip[]) null,
              abils = (Json_Ability[]) null,
              artis = (Json_Artifact[]) null
            });
        }
        for (int index = 0; index < unitParam.jobsets.Length; ++index)
        {
          JobSetParam jobSetParam = MonoSingleton<GameManager>.Instance.GetJobSetParam(unitParam.jobsets[index]);
          while (!string.IsNullOrEmpty(jobSetParam.jobchange))
          {
            jobSetParam = MonoSingleton<GameManager>.Instance.GetJobSetParam(jobSetParam.jobchange);
            if (jobSetParam != null)
              jsonJobList.Add(new Json_Job()
              {
                iid = (long) num++,
                iname = jobSetParam.job,
                rank = 0,
                equips = (Json_Equip[]) null,
                abils = (Json_Ability[]) null,
                artis = (Json_Artifact[]) null
              });
            else
              break;
          }
        }
        json.jobs = jsonJobList.ToArray();
      }
      unitData.Deserialize(json);
      unitData.SetUniqueID(uniqueID);
      unitData.JobRankUp(0);
      this.mUnits.Add(unitData);
      this.mUniqueID2UnitData[unitData.UniqueID] = unitData;
    }

    public void GainUnit(UnitData unit)
    {
      this.mUnits.Add(unit);
      this.mUniqueID2UnitData[unit.UniqueID] = unit;
    }

    public List<UnitData> GetSortedUnits(string menuID, bool includeShujinko = true)
    {
      GameUtility.UnitSortModes sortMode = GameUtility.UnitSortModes.Time;
      bool ascending = false;
      if (!string.IsNullOrEmpty(menuID) && PlayerPrefsUtility.HasKey(menuID))
      {
        string str = PlayerPrefsUtility.GetString(menuID, string.Empty);
        ascending = PlayerPrefsUtility.GetInt(menuID + "#") != 0;
        try
        {
          sortMode = (GameUtility.UnitSortModes) Enum.Parse(typeof (GameUtility.UnitSortModes), str, true);
        }
        catch (Exception ex)
        {
        }
      }
      return this.GetSortedUnits(sortMode, ascending, includeShujinko);
    }

    public List<UnitData> GetSortedUnits(
      GameUtility.UnitSortModes sortMode,
      bool ascending = false,
      bool includeShujinko = true)
    {
      List<UnitData> units1 = MonoSingleton<GameManager>.Instance.Player.Units;
      List<UnitData> units2 = new List<UnitData>();
      for (int index = 0; index < units1.Count; ++index)
      {
        UnitData unitData = units1[index];
        if (includeShujinko || !unitData.UnitParam.IsHero())
          units2.Add(unitData);
      }
      int[] sortValues = (int[]) null;
      if (sortMode != GameUtility.UnitSortModes.Time)
        GameUtility.SortUnits(units2, sortMode, false, out sortValues, true);
      else
        ascending = !ascending;
      if (ascending)
        units2.Reverse();
      return units2;
    }

    public UnitData GetUnitData(long iid)
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (this.mUnits[index].UniqueID == iid)
          return this.mUnits[index];
      }
      return (UnitData) null;
    }

    public int GetItemSlotAmount()
    {
      int itemSlotAmount = 0;
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (this.mItems[index].Num != 0)
          ++itemSlotAmount;
      }
      return itemSlotAmount;
    }

    public bool CheckConceptCardCapacity(int adddValue)
    {
      return (int) GlobalVars.ConceptCardNum + adddValue <= this.ConceptCardCap;
    }

    public bool CheckItemCapacity(ItemParam item, int num)
    {
      ItemData itemDataByItemId = this.FindItemDataByItemID(item.iname);
      return (itemDataByItemId == null ? 0 : itemDataByItemId.NumNonCap) <= 99999 - num;
    }

    public CreateItemResult CheckCreateItem(ItemParam item)
    {
      if (item == null || string.IsNullOrEmpty(item.recipe))
        return CreateItemResult.NotEnough;
      RecipeParam recipeParam = MonoSingleton<GameManager>.Instance.GetRecipeParam(item.recipe);
      if (recipeParam == null)
        return CreateItemResult.NotEnough;
      bool flag = false;
      for (int index = 0; index < recipeParam.items.Length; ++index)
      {
        RecipeItem recipeItem = recipeParam.items[index];
        int num = recipeItem.num;
        int itemAmount1 = this.GetItemAmount(recipeItem.iname);
        if (itemAmount1 < num)
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(recipeItem.iname);
          if (itemParam == null || !itemParam.IsCommon)
            return CreateItemResult.NotEnough;
          int itemAmount2 = this.GetItemAmount(MonoSingleton<GameManager>.Instance.MasterParam.GetCommonEquip(itemParam, false).iname);
          if (itemAmount1 + itemAmount2 < num)
            return CreateItemResult.NotEnough;
          flag = true;
        }
      }
      return flag ? CreateItemResult.CanCreateCommon : CreateItemResult.CanCreate;
    }

    public bool CreateItem(ItemParam item)
    {
      RecipeParam recipeParam = MonoSingleton<GameManager>.Instance.GetRecipeParam(item.recipe);
      if (!this.CheckItemCapacity(item, 1) || recipeParam.cost > this.Gold || this.CheckCreateItem(item) == CreateItemResult.NotEnough)
        return false;
      this.GainGold(-recipeParam.cost);
      for (int index = 0; index < recipeParam.items.Length; ++index)
      {
        RecipeItem recipeItem = recipeParam.items[index];
        this.GainItem(recipeItem.iname, -recipeItem.num);
      }
      this.GainItem(item.iname, 1);
      return true;
    }

    public bool CreateItemAll(ItemParam item)
    {
      if (!this.CheckItemCapacity(item, 1))
        return false;
      Dictionary<string, int> consumes = (Dictionary<string, int>) null;
      bool is_ikkatsu = false;
      int cost = 0;
      if (!this.CheckEnableCreateItem(item, ref is_ikkatsu, ref cost, ref consumes) || cost > this.Gold)
        return false;
      this.GainGold(-cost);
      if (consumes != null)
      {
        foreach (string key in consumes.Keys)
          this.GainItem(key, -consumes[key]);
      }
      this.GainItem(item.iname, 1);
      return true;
    }

    public bool CheckEnableConvertGold()
    {
      return this.Items.Find((Predicate<ItemData>) (item => item.ItemType == EItemType.GoldConvert && item.Num > 0)) != null;
    }

    public ShopData GetShopData(EShopType type)
    {
      switch (type)
      {
        case EShopType.Event:
          return this.GetEventShopData().GetShopData();
        case EShopType.Limited:
        case EShopType.Port:
          return this.GetLimitedShopData().GetShopData();
        default:
          return this.mShops[(int) type];
      }
    }

    public void SetShopData(EShopType type, ShopData shop)
    {
      switch (type)
      {
        case EShopType.Event:
          this.mEventShops.SetShopData(shop);
          break;
        case EShopType.Limited:
        case EShopType.Port:
          this.mLimitedShops.SetShopData(shop);
          break;
      }
      this.mShops[(int) type] = shop;
    }

    public LimitedShopData GetLimitedShopData() => this.mLimitedShops;

    public void SetLimitedShopData(LimitedShopData shop) => this.mLimitedShops = shop;

    public EventShopData GetEventShopData() => this.mEventShops;

    public void SetEventShopData(EventShopData shop) => this.mEventShops = shop;

    public bool CheckUnlockShopType(EShopType type)
    {
      UnlockTargets unlockTargets = type.ToUnlockTargets();
      return unlockTargets != (UnlockTargets) 0 && this.CheckUnlock(unlockTargets);
    }

    public string GetShopName(EShopType type)
    {
      string key = string.Empty;
      switch (type)
      {
        case EShopType.Normal:
          key = "sys.SHOPNAME_NORMAL";
          break;
        case EShopType.Tabi:
          key = "sys.SHOPNAME_TABI";
          break;
        case EShopType.Kimagure:
          key = "sys.SHOPNAME_KIMAGURE";
          break;
        case EShopType.Monozuki:
          key = "sys.SHOPNAME_MONOZUKI";
          break;
        case EShopType.Tour:
          key = "sys.SHOPNAME_TOUR";
          break;
        case EShopType.Arena:
          key = "sys.SHOPNAME_ARENA";
          break;
        case EShopType.Multi:
          key = "sys.SHOPNAME_MULTI";
          break;
        case EShopType.AwakePiece:
          key = "sys.SHOPNAME_KAKERA";
          break;
        case EShopType.Artifact:
          key = "sys.SHOPNAME_ARTIFACT";
          break;
        case EShopType.Event:
          key = "sys.SHOPNAME_EVENT";
          break;
        case EShopType.Limited:
          key = "sys.SHOPNAME_LIMITED";
          break;
        case EShopType.Port:
          key = "sys.SHOPNAME_PORT";
          break;
      }
      return key == string.Empty ? key : LocalizedText.Get(key);
    }

    public int GetShopUpdateCost(EShopType type, bool getOldCost = false)
    {
      ShopData shopData = this.GetShopData(type);
      if (shopData == null)
        return 0;
      ShopParam shopParam = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(type);
      if (shopParam == null || shopParam.UpdateCosts == null || shopParam.UpdateCosts.Length <= 0)
        return 0;
      int updateCount = shopData.UpdateCount;
      if (getOldCost)
        --updateCount;
      int index = Mathf.Clamp(updateCount, 0, shopParam.UpdateCosts.Length - 1);
      return shopParam.UpdateCosts[index];
    }

    public int GetShopTypeCostAmount(ESaleType type)
    {
      switch (type)
      {
        case ESaleType.Gold:
          return this.Gold;
        case ESaleType.Coin:
          return this.Coin;
        case ESaleType.TourCoin:
          return this.TourCoin;
        case ESaleType.ArenaCoin:
          return this.ArenaCoin;
        case ESaleType.PiecePoint:
          return this.PiecePoint;
        case ESaleType.MultiCoin:
          return this.MultiCoin;
        case ESaleType.EventCoin:
          DebugUtility.Assert("There is no common price in the event coin.");
          return 0;
        case ESaleType.Coin_P:
          return this.PaidCoin;
        default:
          return 0;
      }
    }

    public bool CheckShopUpdateCost(EShopType type)
    {
      if (this.GetShopData(type) == null)
        return false;
      ShopParam shopParam = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(type);
      return shopParam != null && this.GetShopUpdateCost(type) <= this.GetShopTypeCostAmount(shopParam.UpdateCostType);
    }

    public void DEBUG_BUY_ITEM_UPDATED(EShopType shoptype)
    {
      ShopData shopData = this.GetShopData(shoptype);
      ShopParam shopParam = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(shoptype);
      if (shopData == null || shopParam == null || !this.CheckShopUpdateCost(shoptype))
        return;
      for (int index = 0; index < shopData.items.Count; ++index)
        shopData.items[index].is_soldout = false;
      int shopUpdateCost = this.GetShopUpdateCost(shoptype);
      switch (shopParam.UpdateCostType)
      {
        case ESaleType.Gold:
          this.mGold = (OInt) Math.Max((int) this.mGold - shopUpdateCost, 0);
          break;
        case ESaleType.Coin:
          this.DEBUG_CONSUME_COIN(shopUpdateCost);
          break;
        case ESaleType.TourCoin:
          this.mTourCoin = (OInt) Math.Max((int) this.mTourCoin - shopUpdateCost, 0);
          break;
        case ESaleType.ArenaCoin:
          this.mArenaCoin = (OInt) Math.Max((int) this.mArenaCoin - shopUpdateCost, 0);
          break;
        case ESaleType.PiecePoint:
          this.mPiecePoint = (OInt) Math.Max((int) this.mPiecePoint - shopUpdateCost, 0);
          break;
        case ESaleType.MultiCoin:
          this.mMultiCoin = (OInt) Math.Max((int) this.mMultiCoin - shopUpdateCost, 0);
          break;
        case ESaleType.EventCoin:
          string shopCostIname = GlobalVars.EventShopItem.shop_cost_iname;
          this.SetEventCoinNum(shopCostIname, Math.Max(this.EventCoinNum(shopCostIname) - shopUpdateCost, 0));
          break;
        case ESaleType.Coin_P:
          this.DEBUG_CONSUME_PAID_COIN(shopUpdateCost);
          break;
      }
    }

    public void DEBUG_BUY_ITEM(EShopType shoptype, int index)
    {
      ShopData shopData = this.GetShopData(shoptype);
      if (shopData == null)
        return;
      ShopItem shopItem = shopData.items[index];
      if (shopItem.is_soldout)
        return;
      ItemData itemDataByItemId = this.FindItemDataByItemID(shopItem.iname);
      if (itemDataByItemId != null && itemDataByItemId.Num == itemDataByItemId.HaveCap)
        return;
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(shopItem.iname);
      switch (shopItem.saleType)
      {
        case ESaleType.Gold:
          this.mGold = (OInt) Math.Max((int) this.mGold - itemParam.buy * shopItem.num, 0);
          break;
        case ESaleType.Coin:
          this.DEBUG_CONSUME_COIN(itemParam.coin * shopItem.num);
          break;
        case ESaleType.TourCoin:
          this.mTourCoin = (OInt) Math.Max((int) this.mTourCoin - itemParam.tour_coin * shopItem.num, 0);
          break;
        case ESaleType.ArenaCoin:
          this.mArenaCoin = (OInt) Math.Max((int) this.mArenaCoin - itemParam.arena_coin * shopItem.num, 0);
          break;
        case ESaleType.PiecePoint:
          this.mPiecePoint = (OInt) Math.Max((int) this.mPiecePoint - itemParam.piece_point * shopItem.num, 0);
          break;
        case ESaleType.MultiCoin:
          this.mMultiCoin = (OInt) Math.Max((int) this.mMultiCoin - itemParam.multi_coin * shopItem.num, 0);
          break;
        case ESaleType.EventCoin:
          DebugUtility.Assert("There is no common price in the event coin.");
          break;
        case ESaleType.Coin_P:
          this.DEBUG_CONSUME_PAID_COIN(itemParam.coin * shopItem.num);
          break;
      }
      this.GainItem(shopItem.iname, shopItem.num);
      shopItem.is_soldout = true;
    }

    public DateTime VipExpiredAt => TimeManager.FromUnixTime((long) this.mVipExpiredAt);

    public bool CheckEnableVipCard() => Network.GetServerTime() < (long) this.mVipExpiredAt;

    public DateTime PremiumExpiredAt => TimeManager.FromUnixTime((long) this.mPremiumExpiredAt);

    public bool CheckEnablePremiumMember()
    {
      return Network.GetServerTime() < (long) this.mPremiumExpiredAt;
    }

    public void SubAbilityRankUpCount(int value) => this.mAbilityRankUpCount.SubValue(value);

    public void RestoreAbilityRankUpCount()
    {
      this.mAbilityRankUpCount.val = this.mAbilityRankUpCount.valMax;
      this.mAbilityRankUpCount.at = (OLong) Network.GetServerTime();
    }

    public void SubStamina(int value) => this.mStamina.SubValue(value);

    public long GetNextStaminaRecoverySec() => this.mStamina.GetNextRecoverySec();

    public void UpdateStamina() => this.mStamina.Update();

    public int GetStaminaRecoveryCost(bool getOldCost = false)
    {
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      int mStaminaBuyNum = (int) this.mStaminaBuyNum;
      if (getOldCost)
        --mStaminaBuyNum;
      int index = Math.Max(Math.Min(mStaminaBuyNum, fixParam.StaminaAddCost.Length - 1), 0);
      return (int) fixParam.StaminaAddCost[index];
    }

    public void ResetStaminaRecoverCount() => this.mStaminaBuyNum = (OInt) 0;

    public void ResetBuyGoldNum() => this.mGoldBuyNum = (OInt) 0;

    public void SubCaveStamina(int value) => this.mCaveStamina.SubValue(value);

    public long GetNextCaveStaminaRecoverySec() => this.mCaveStamina.GetNextRecoverySec();

    public void UpdateCaveStamina() => this.mCaveStamina.Update();

    public int GetCaveStaminaRecoveryCost()
    {
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      int index = Math.Max(Math.Min(0, fixParam.CaveStaminaAddCost.Length), 0);
      return (int) fixParam.CaveStaminaAddCost[index];
    }

    public long GetNextAbilityRankUpCountRecoverySec()
    {
      return this.mAbilityRankUpCount.GetNextRecoverySec();
    }

    public void UpdateAbilityRankUpCount() => this.mAbilityRankUpCount.Update();

    public int ArenaResetCount => this.mArenaResetCount;

    public bool ChallengeArena()
    {
      if (this.ChallengeArenaNum >= this.ChallengeArenaMax)
        return false;
      --this.mChallengeArenaNum;
      this.mChallengeArenaTimer.val = (OInt) 0;
      this.mChallengeArenaTimer.at = (OLong) Network.GetServerTime();
      return true;
    }

    public bool CheckChangeArena()
    {
      return this.ChallengeArenaNum < this.ChallengeArenaMax && (int) this.mChallengeArenaTimer.val == (int) this.mChallengeArenaTimer.valMax;
    }

    public long GetNextChallengeArenaCoolDownSec()
    {
      return this.mChallengeArenaTimer.GetNextRecoverySec();
    }

    public void UpdateChallengeArenaTimer() => this.mChallengeArenaTimer.Update();

    public int GetChallengeArenaCost()
    {
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      if (fixParam.ArenaResetTicketCost == null)
        return 0;
      int index = Math.Max(Math.Min(this.mArenaResetCount, fixParam.ArenaResetTicketCost.Length - 1), 0);
      return (int) fixParam.ArenaResetTicketCost[index];
    }

    public void InitPlayerPrefs()
    {
    }

    public void DEBUG_GAIN_ALL_ITEMS()
    {
      List<ItemParam> items = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.Items;
      for (int index = 0; index < items.Count; ++index)
      {
        if (items[index].type == EItemType.Used && !string.IsNullOrEmpty(items[index].skill))
          this.GainItem(items[index].iname, 10);
      }
    }

    public void DEBUG_TRASH_ALL_ITEMS() => this.Items.Clear();

    public void DEBUG_ADD_ARTIFACTS(ArtifactData artifact)
    {
      if (this.mArtifacts == null)
        this.mArtifacts = new List<ArtifactData>();
      if (artifact == null || this.mArtifacts.Contains(artifact))
        return;
      this.AddArtifact(artifact);
    }

    public void DEBUG_GAIN_ALL_ARTIFACT()
    {
      List<ArtifactParam> artifacts = MonoSingleton<GameManager>.Instance.MasterParam.Artifacts;
      long num = 1;
      for (int index = 0; index < artifacts.Count; ++index)
      {
        ArtifactParam artifactParam = artifacts[index];
        if (artifactParam.is_create)
        {
          Json_Artifact json = new Json_Artifact();
          json.iid = num++;
          json.exp = 0;
          json.iname = artifactParam.iname;
          json.rare = artifactParam.rareini;
          json.fav = 0;
          ArtifactData artifactData = new ArtifactData();
          artifactData.Deserialize(json);
          this.AddArtifact(artifactData);
        }
      }
    }

    public void DEBUG_TRASH_ALL_ARTIFACT()
    {
      this.mArtifacts.Clear();
      this.mArtifactsNumByRarity.Clear();
    }

    public void LoadPlayerPrefs()
    {
      bool flag1 = true;
      if (EditorPlayerPrefs.HasKey("Version"))
        flag1 = PlayerData.PLAYRE_DATA_VERSION != EditorPlayerPrefs.GetString("Version");
      if (flag1)
        this.InitPlayerPrefs();
      if (EditorPlayerPrefs.HasKey("Gold"))
        this.mGold = (OInt) EditorPlayerPrefs.GetInt("Gold");
      if (EditorPlayerPrefs.HasKey("PaidCoin"))
        this.mPaidCoin = (OInt) EditorPlayerPrefs.GetInt("PaidCoin");
      if (EditorPlayerPrefs.HasKey("FreeCoin"))
        this.mFreeCoin = (OInt) EditorPlayerPrefs.GetInt("FreeCoin");
      if (EditorPlayerPrefs.HasKey("ComCoin"))
        this.mComCoin = (OInt) EditorPlayerPrefs.GetInt("ComCoin");
      if (EditorPlayerPrefs.HasKey("TourCoin"))
        this.mTourCoin = (OInt) EditorPlayerPrefs.GetInt("TourCoin");
      if (EditorPlayerPrefs.HasKey("ArenaCoin"))
        this.mArenaCoin = (OInt) EditorPlayerPrefs.GetInt("ArenaCoin");
      if (EditorPlayerPrefs.HasKey("MultiCoin"))
        this.mMultiCoin = (OInt) EditorPlayerPrefs.GetInt("MultiCoin");
      if (EditorPlayerPrefs.HasKey("PiecePoint"))
        this.mPiecePoint = (OInt) EditorPlayerPrefs.GetInt("PiecePoint");
      if (EditorPlayerPrefs.HasKey("PlayerExp"))
        this.mExp = (OInt) EditorPlayerPrefs.GetInt("PlayerExp");
      if (string.IsNullOrEmpty(this.mCuid))
      {
        this.mCuid = "1";
        this.mName = "GUMI";
        this.mLv = (OInt) this.CalcLevel();
        this.UpdateUnlocks();
      }
      if (EditorPlayerPrefs.HasKey("Stamina"))
        this.mStamina.val = (OInt) EditorPlayerPrefs.GetInt("Stamina");
      if (EditorPlayerPrefs.HasKey("StaminaAt"))
        this.mStamina.at = (OLong) Convert.ToInt64(EditorPlayerPrefs.GetString("StaminaAt"));
      if (EditorPlayerPrefs.HasKey("CaveStamina"))
        this.mCaveStamina.val = (OInt) EditorPlayerPrefs.GetInt("CaveStamina");
      if (EditorPlayerPrefs.HasKey("CaveStaminaAt"))
        this.mCaveStamina.at = (OLong) Convert.ToInt64(EditorPlayerPrefs.GetString("CaveStaminaAt"));
      if (EditorPlayerPrefs.HasKey("AbilRankUpCount"))
        this.mAbilityRankUpCount.val = (OInt) EditorPlayerPrefs.GetInt("AbilRankUpCount");
      if (EditorPlayerPrefs.HasKey("AbilRankUpCountAt"))
        this.mAbilityRankUpCount.at = (OLong) Convert.ToInt64(EditorPlayerPrefs.GetString("AbilRankUpCountAt"));
      PlayerParam playerParam = MonoSingleton<GameManager>.Instance.MasterParam.GetPlayerParam((int) this.mLv);
      if (playerParam != null)
      {
        this.mUnitCap = playerParam.ucap;
        this.mStamina.valMax = playerParam.pt;
      }
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      this.mStamina.valRecover = fixParam.StaminaRecoveryVal;
      this.mStamina.interval = fixParam.StaminaRecoverySec;
      this.mCaveStamina.valMax = fixParam.CaveStaminaMax;
      this.mCaveStamina.valRecover = fixParam.CaveStaminaRecoveryVal;
      this.mCaveStamina.interval = fixParam.CaveStaminaRecoverySec;
      this.mAbilityRankUpCount.valMax = fixParam.AbilityRankUpCountMax;
      this.mAbilityRankUpCount.valRecover = fixParam.AbilityRankUpCountRecoveryVal;
      this.mAbilityRankUpCount.interval = fixParam.AbilityRankUpCountRecoverySec;
      if (EditorPlayerPrefs.HasKey("ARTI_NUM"))
      {
        int num = EditorPlayerPrefs.GetInt("ARTI_NUM");
        for (int index = 0; index < num; ++index)
        {
          string src = EditorPlayerPrefs.GetString("ARTI_" + (object) index);
          if (!string.IsNullOrEmpty(src))
          {
            Json_Artifact jsonObject = JSONParser.parseJSONObject<Json_Artifact>(src);
            if (jsonObject != null)
            {
              ArtifactData artifactData = new ArtifactData();
              artifactData.Deserialize(jsonObject);
              this.AddArtifact(artifactData);
            }
          }
        }
      }
      if (this.mUnits == null)
        this.mUnits = new List<UnitData>((int) this.mUnitCap);
      this.mUnits.Clear();
      this.mUniqueID2UnitData.Clear();
      List<Json_Ability> jsonAbilityList1 = new List<Json_Ability>(5);
      int num1 = EditorPlayerPrefs.GetInt("UnitNum");
      for (int index1 = 0; index1 < num1; ++index1)
      {
        UnitData unitData = new UnitData();
        if (unitData != null)
        {
          jsonAbilityList1.Clear();
          string str1 = "Unit" + (object) index1 + "_";
          Json_Unit json1 = new Json_Unit();
          json1.iname = EditorPlayerPrefs.GetString(str1 + "Iname");
          json1.iid = (long) EditorPlayerPrefs.GetInt(str1 + "Iid");
          json1.exp = EditorPlayerPrefs.GetInt(str1 + "Exp");
          json1.plus = EditorPlayerPrefs.GetInt(str1 + "Plus");
          json1.rare = EditorPlayerPrefs.GetInt(str1 + "Rarity");
          List<Json_Job> jsonJobList = new List<Json_Job>(6);
          for (int index2 = 0; index2 < 6; ++index2)
          {
            string str2 = str1 + "Job" + (object) index2 + "_";
            if (EditorPlayerPrefs.HasKey(str2 + "Iname") && !string.IsNullOrEmpty(EditorPlayerPrefs.GetString(str2 + "Iname")))
            {
              Json_Job jsonJob = new Json_Job();
              jsonJob.iname = EditorPlayerPrefs.GetString(str2 + "Iname");
              jsonJob.iid = (long) EditorPlayerPrefs.GetInt(str2 + "Iid");
              jsonJob.rank = EditorPlayerPrefs.GetInt(str2 + "Rank");
              jsonJob.equips = new Json_Equip[6];
              for (int index3 = 0; index3 < jsonJob.equips.Length; ++index3)
              {
                string str3 = str2 + "Equip" + (object) index3 + "_";
                if (EditorPlayerPrefs.HasKey(str3 + "Iname"))
                {
                  jsonJob.equips[index3] = new Json_Equip();
                  jsonJob.equips[index3].iname = EditorPlayerPrefs.GetString(str3 + "Iname");
                  jsonJob.equips[index3].iid = (long) EditorPlayerPrefs.GetInt(str3 + "Iid");
                  jsonJob.equips[index3].exp = EditorPlayerPrefs.GetInt(str3 + "Exp");
                }
                else
                  jsonJob.equips[index3] = (Json_Equip) null;
              }
              List<Json_Ability> jsonAbilityList2 = new List<Json_Ability>(8);
              for (int index4 = 0; index4 < 8; ++index4)
              {
                string str4 = str2 + "Ability" + (object) index4 + "_";
                if (EditorPlayerPrefs.HasKey(str4 + "Iname") && !string.IsNullOrEmpty(EditorPlayerPrefs.GetString(str4 + "Iname")))
                {
                  Json_Ability jsonAbility = new Json_Ability();
                  jsonAbility.iname = EditorPlayerPrefs.GetString(str4 + "Iname");
                  jsonAbility.iid = (long) EditorPlayerPrefs.GetInt(str4 + "Iid");
                  jsonAbility.exp = EditorPlayerPrefs.GetInt(str4 + "Exp");
                  bool flag2 = false;
                  for (int index5 = 0; index5 < jsonAbilityList2.Count; ++index5)
                  {
                    if (jsonAbilityList2[index5].iname == jsonAbility.iname)
                      flag2 = true;
                  }
                  if (!flag2)
                    jsonAbilityList2.Add(jsonAbility);
                }
              }
              jsonJob.abils = jsonAbilityList2.Count <= 0 ? (Json_Ability[]) null : jsonAbilityList2.ToArray();
              jsonJob.select = new Json_JobSelectable();
              jsonJob.select.abils = new long[5];
              Array.Clear((Array) jsonJob.select.abils, 0, jsonJob.select.abils.Length);
              for (int index6 = 0; index6 < jsonJob.select.abils.Length; ++index6)
              {
                string key = str2 + "Select_Ability" + (object) index6;
                if (EditorPlayerPrefs.HasKey(key))
                  jsonJob.select.abils[index6] = (long) EditorPlayerPrefs.GetInt(key);
              }
              jsonJobList.Add(jsonJob);
            }
          }
          json1.jobs = jsonJobList.ToArray();
          json1.select = new Json_UnitSelectable();
          json1.select.job = (long) EditorPlayerPrefs.GetInt(str1 + "Select_Job");
          try
          {
            unitData.Deserialize(json1);
            for (int job_index = 0; job_index < unitData.Jobs.Length; ++job_index)
            {
              for (int slot = 0; slot < unitData.Jobs[job_index].Artifacts.Length; ++slot)
              {
                string key = str1 + "Job" + (object) job_index + "_" + "Artifact" + (object) slot + "_Iid";
                if (EditorPlayerPrefs.HasKey(key))
                {
                  long iid = (long) EditorPlayerPrefs.GetInt(key);
                  ArtifactData artifactData = MonoSingleton<GameManager>.Instance.Player.Artifacts.Find((Predicate<ArtifactData>) (adl => (long) adl.UniqueID == iid));
                  if (artifactData != null)
                  {
                    Json_Artifact json2 = new Json_Artifact();
                    json2.iid = (long) artifactData.UniqueID;
                    json2.iname = artifactData.ArtifactParam.iname;
                    json2.rare = artifactData.ArtifactParam.raremax;
                    RarityParam rarityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam(artifactData.ArtifactParam.raremax);
                    json2.exp = ArtifactData.StaticCalcExpFromLevel((int) rarityParam.ArtifactLvCap);
                    ArtifactData artifact = new ArtifactData();
                    artifact.Reset();
                    artifact.Deserialize(json2);
                    unitData.SetEquipArtifactData(job_index, slot, artifact);
                  }
                }
              }
            }
            this.mUnits.Add(unitData);
            this.mUniqueID2UnitData[unitData.UniqueID] = unitData;
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
        }
      }
      for (int index7 = 0; index7 < 17; ++index7)
      {
        Json_Party json = new Json_Party();
        PartyData partyData = new PartyData((PlayerPartyTypes) index7);
        json.units = new long[partyData.MAX_UNIT];
        for (int index8 = 0; index8 < json.units.Length; ++index8)
          json.units[index8] = (long) EditorPlayerPrefs.GetInt("Hensei" + (object) index7 + "_UNIT" + (object) index8 + "_ID");
        this.mPartys[index7].Deserialize(json);
      }
      Debug.Log((object) "LoadPlayerPrefs Items");
      int capacity = EditorPlayerPrefs.GetInt("ItemNum");
      if (this.mItems == null)
        this.mItems = new List<ItemData>(capacity);
      this.mItems.Clear();
      this.mID2ItemData.Clear();
      for (int index = 0; index < capacity; ++index)
      {
        string str = "Item" + (object) index + "_";
        Json_Item json = new Json_Item();
        json.iname = EditorPlayerPrefs.GetString(str + "Iname");
        json.iid = (long) EditorPlayerPrefs.GetInt(str + "Iid");
        json.num = EditorPlayerPrefs.GetInt(str + "Num");
        if (MonoSingleton<GameManager>.Instance.GetItemParam(json.iname) == null)
        {
          DebugUtility.Log("存在しないアイテム[" + json.iname + "]が指定された");
        }
        else
        {
          ItemData itemData = new ItemData();
          itemData.Deserialize(json);
          this.mItems.Add(itemData);
          this.mID2ItemData[json.iname] = itemData;
        }
      }
    }

    [DebuggerHidden]
    public IEnumerator SavePlayerPrefsAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PlayerData.\u003CSavePlayerPrefsAsync\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void InternalSavePlayerPrefsParty()
    {
      for (int index1 = 0; index1 < this.mPartys.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.mPartys[index1].MAX_UNIT; ++index2)
        {
          EditorPlayerPrefs.SetInt("Hensei" + (object) index1 + "_UNIT" + (object) index2 + "_ID", (int) this.mPartys[index1].GetUnitUniqueID(index2));
          EditorPlayerPrefs.SetInt("Hensei" + (object) index1 + "_UNIT" + (object) index2 + "_LEADER", this.mPartys[index1].LeaderIndex != index2 ? 0 : 1);
        }
      }
    }

    public void SavePlayerPrefsParty()
    {
      this.InternalSavePlayerPrefsParty();
      EditorPlayerPrefs.Flush();
    }

    public void SavePlayerPrefs()
    {
      IEnumerator enumerator = this.SavePlayerPrefsAsync();
      do
        ;
      while (enumerator.MoveNext());
    }

    public long GenerateUnitUniqueID()
    {
      long num = 0;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (this.mUnits[index].UniqueID > num)
          num = this.mUnits[index].UniqueID;
      }
      return num + 1L;
    }

    public void OnQuestStart(string questID)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(questID);
      if (quest == null || quest.type == QuestTypes.Tutorial)
        return;
      if (quest.type == QuestTypes.Arena)
      {
        TrophyParam[] trophies = MonoSingleton<GameManager>.Instance.Trophies;
        for (int index = trophies.Length - 1; index >= 0; --index)
        {
          TrophyParam trophyParam = trophies[index];
          for (int countIndex = trophyParam.Objectives.Length - 1; countIndex >= 0; --countIndex)
          {
            if (trophyParam.Objectives[countIndex].type == TrophyConditionTypes.arena)
              this.mTrophyData.AddTrophyCounter(trophyParam, countIndex, 1);
          }
        }
      }
      if (quest.IsMulti && GlobalVars.ResumeMultiplayPlayerID == 0)
      {
        TrophyParam[] trophies = MonoSingleton<GameManager>.Instance.Trophies;
        for (int index = trophies.Length - 1; index >= 0; --index)
        {
          TrophyParam trophyParam = trophies[index];
          for (int countIndex = trophyParam.Objectives.Length - 1; countIndex >= 0; --countIndex)
          {
            if (trophyParam.Objectives[countIndex].type == TrophyConditionTypes.multiplay)
              this.mTrophyData.AddTrophyCounter(trophyParam, countIndex, 1);
          }
        }
      }
      if (!quest.IsMultiTower || GlobalVars.ResumeMultiplayPlayerID != 0)
        return;
      TrophyParam[] trophies1 = MonoSingleton<GameManager>.Instance.Trophies;
      for (int index = trophies1.Length - 1; index >= 0; --index)
      {
        TrophyParam trophyParam = trophies1[index];
        for (int countIndex = trophyParam.Objectives.Length - 1; countIndex >= 0; --countIndex)
        {
          if (trophyParam.Objectives[countIndex].type == TrophyConditionTypes.multitower)
            this.mTrophyData.AddTrophyCounter(trophyParam, countIndex, 1);
        }
      }
    }

    public void OnQuestWin(
      string questID,
      BattleCore.Record battleRecord = null,
      UnitData[] currentUnits = null,
      int addCnt = 1)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(questID);
      if (quest == null || quest.type == QuestTypes.Tutorial)
        return;
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winquest);
      for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType1[index];
        if (!string.IsNullOrEmpty(trophyObjective.sval_base))
        {
          if (!trophyObjective.sval.Contains(questID))
            continue;
        }
        else if (quest.type == QuestTypes.Event || quest.type == QuestTypes.Beginner || quest.type == QuestTypes.Arena || quest.IsMulti || quest.type == QuestTypes.Character || quest.difficulty != QuestDifficulties.Normal || quest.type == QuestTypes.Tower || quest.IsVersus || quest.type == QuestTypes.Ordeal || quest.type == QuestTypes.RankMatch || quest.type == QuestTypes.Raid || quest.type == QuestTypes.GuildRaid || quest.type == QuestTypes.WorldRaid || quest.type == QuestTypes.GenesisStory || quest.type == QuestTypes.GenesisBoss || quest.type == QuestTypes.AdvanceStory || quest.type == QuestTypes.AdvanceBoss || quest.type == QuestTypes.UnitRental || quest.type == QuestTypes.GvG)
          continue;
        this.mTrophyData.AddTrophyCounter(trophyObjective, addCnt);
      }
      if (battleRecord != null && quest.bonusObjective != null)
      {
        TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.complete_all_quest_mission);
        for (int index1 = trophiesOfType2.Length - 1; index1 >= 0; --index1)
        {
          if (!(trophiesOfType2[index1].sval_base != questID))
          {
            int num = 0;
            for (int index2 = 0; index2 < quest.bonusObjective.Length; ++index2)
            {
              if ((battleRecord.allBonusFlags & 1 << index2) != 0)
                ++num;
            }
            if (num >= quest.bonusObjective.Length)
              this.mTrophyData.AddTrophyCounter(trophiesOfType2[index1], addCnt);
          }
        }
      }
      if (battleRecord != null && quest.bonusObjective != null)
      {
        int num = 0;
        for (int index = 0; index < quest.bonusObjective.Length; ++index)
        {
          if ((battleRecord.allBonusFlags & 1 << index) != 0 || quest.IsMissionClear(index))
            ++num;
        }
        if (num >= quest.bonusObjective.Length)
        {
          TrophyObjective[] trophiesOfType3 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.complete_all_quest_mission_total);
          for (int index = trophiesOfType3.Length - 1; index >= 0; --index)
          {
            if (!(trophiesOfType3[index].sval_base != questID))
              this.mTrophyData.AddTrophyCounter(trophiesOfType3[index], addCnt);
          }
          if (!quest.IsMissionCompleteALL())
          {
            if (!string.IsNullOrEmpty(FlowNode_Variable.Get("COMPLETE_QUEST_MISSION")))
              MonoSingleton<GameManager>.Instance.Player.UpdateCompleteAllQuestCountTrophy2(quest);
            else
              MonoSingleton<GameManager>.Instance.Player.UpdateCompleteAllQuestCountTrophy(quest);
          }
        }
      }
      if (quest.type != QuestTypes.GenesisStory && quest.type != QuestTypes.GenesisBoss && quest.type != QuestTypes.AdvanceStory && quest.type != QuestTypes.AdvanceBoss)
      {
        if (quest.difficulty == QuestDifficulties.Extra)
        {
          TrophyObjective[] trophiesOfType4 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winstory_extra);
          for (int index = trophiesOfType4.Length - 1; index >= 0; --index)
            this.mTrophyData.AddTrophyCounter(trophiesOfType4[index], addCnt);
        }
        if (quest.difficulty == QuestDifficulties.Elite)
        {
          TrophyObjective[] trophiesOfType5 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winelite);
          for (int index = trophiesOfType5.Length - 1; index >= 0; --index)
            this.mTrophyData.AddTrophyCounter(trophiesOfType5[index], addCnt);
        }
      }
      if (quest.type == QuestTypes.Arena)
      {
        TrophyObjective[] trophiesOfType6 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winarena);
        for (int index = trophiesOfType6.Length - 1; index >= 0; --index)
          this.mTrophyData.AddTrophyCounter(trophiesOfType6[index], addCnt);
      }
      if (quest.type == QuestTypes.Event || quest.type == QuestTypes.Tower || quest.type == QuestTypes.GenesisStory || quest.type == QuestTypes.AdvanceStory)
      {
        TrophyObjective[] trophiesOfType7 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winevent);
        for (int index = trophiesOfType7.Length - 1; index >= 0; --index)
          this.mTrophyData.AddTrophyCounter(trophiesOfType7[index], addCnt);
      }
      SupportData support = (SupportData) GlobalVars.SelectedSupport;
      if (quest.type == QuestTypes.Ordeal)
      {
        support = (SupportData) null;
        if (GlobalVars.OrdealSupports != null)
        {
          foreach (SupportData ordealSupport in GlobalVars.OrdealSupports)
          {
            if (ordealSupport != null)
            {
              support = ordealSupport;
              break;
            }
          }
        }
      }
      if (support != null)
      {
        TrophyObjective[] trophiesOfType8 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winquestsoldier);
        for (int index = trophiesOfType8.Length - 1; index >= 0; --index)
          this.mTrophyData.AddTrophyCounter(trophiesOfType8[index], addCnt);
      }
      if (quest.IsMulti)
      {
        TrophyObjective[] trophiesOfType9 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winmulti);
        for (int index = trophiesOfType9.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType9[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.ContainsSval(questID))
            this.mTrophyData.AddTrophyCounter(trophyObjective, addCnt);
        }
        TrophyObjective[] trophiesOfType10 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winmultimore);
        for (int index = trophiesOfType10.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType10[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.ContainsSval(questID))
          {
            List<JSON_MyPhotonPlayerParam> myPlayersStarted = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted();
            if (myPlayersStarted != null && myPlayersStarted.Count >= trophyObjective.ival)
              this.mTrophyData.AddTrophyCounter(trophyObjective, addCnt);
          }
        }
        TrophyObjective[] trophiesOfType11 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winmultiless);
        for (int index = trophiesOfType11.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType11[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.ContainsSval(questID))
          {
            List<JSON_MyPhotonPlayerParam> myPlayersStarted = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted();
            if (myPlayersStarted != null && myPlayersStarted.Count <= trophyObjective.ival)
              this.mTrophyData.AddTrophyCounter(trophyObjective, addCnt);
          }
        }
      }
      if (quest.type == QuestTypes.Tower)
      {
        TrophyObjective[] trophiesOfType12 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.wintower);
        for (int index = trophiesOfType12.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType12[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.ContainsSval(questID))
            this.mTrophyData.AddTrophyCounter(trophyObjective, addCnt);
        }
        TowerFloorParam towerFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(questID);
        if (towerFloor != null)
        {
          TrophyObjective[] trophiesOfType13 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.tower);
          for (int index = trophiesOfType13.Length - 1; index >= 0; --index)
          {
            TrophyObjective trophyObjective = trophiesOfType13[index];
            if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.ContainsSval(towerFloor.tower_id))
              this.mTrophyData.AddTrophyCounter(trophyObjective, addCnt);
          }
        }
      }
      if (quest.IsVersus)
      {
        TrophyObjective[] trophiesOfType14 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.vswin);
        for (int index = trophiesOfType14.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType14[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == questID)
            this.mTrophyData.AddTrophyCounter(trophyObjective, addCnt);
        }
        TrophyObjective[] trophiesOfType15 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.vs);
        for (int index = trophiesOfType15.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType15[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == questID)
            this.mTrophyData.AddTrophyCounter(trophyObjective, addCnt);
        }
      }
      if (quest.type == QuestTypes.Ordeal)
      {
        TrophyObjective[] trophiesOfType16 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.clear_ordeal);
        for (int index = trophiesOfType16.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType16[index];
          if (!string.IsNullOrEmpty(trophyObjective.sval_base))
          {
            if (trophyObjective.sval_base == quest.iname)
              this.mTrophyData.AddTrophyCounter(trophyObjective, addCnt);
          }
          else
            DebugUtility.LogError("レコードミッション「" + trophyObjective.Param.Name + "」はクエストが指定されていません。");
        }
      }
      if (quest.type == QuestTypes.Raid)
      {
        switch (RaidManager.SelectedLastRaidOwnerType)
        {
          case RaidManager.RaidOwnerType.Self:
            TrophyObjective[] trophiesOfType17 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.raidboss);
            for (int index = trophiesOfType17.Length - 1; index >= 0; --index)
              this.mTrophyData.AddTrophyCounter(trophiesOfType17[index], addCnt);
            break;
          case RaidManager.RaidOwnerType.Rescue:
          case RaidManager.RaidOwnerType.Rescue_Temp:
            if (RaidManager.SelectedLastRaidRescueMemberType == RaidRescueMemberType.Guild)
            {
              TrophyObjective[] trophiesOfType18 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.raidboss_rescue_for_guildmember);
              for (int index = trophiesOfType18.Length - 1; index >= 0; --index)
                this.mTrophyData.AddTrophyCounter(trophiesOfType18[index], addCnt);
              break;
            }
            break;
        }
      }
      if (quest.type == QuestTypes.GuildRaid && GlobalVars.CurrentBattleType.Get() == GuildRaidBattleType.Main)
      {
        TrophyObjective[] trophiesOfType19 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.guildraid);
        for (int index = trophiesOfType19.Length - 1; index >= 0; --index)
          this.mTrophyData.AddTrophyCounter(trophiesOfType19[index], addCnt);
      }
      if (quest.type == QuestTypes.GvG)
      {
        TrophyObjective[] trophiesOfType20 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.gvg);
        for (int index = trophiesOfType20.Length - 1; index >= 0; --index)
          this.mTrophyData.AddTrophyCounter(trophiesOfType20[index], addCnt);
      }
      MonoSingleton<GameManager>.Instance.Player.UpdateQuestMissionCount(quest, battleRecord);
      List<UnitData> units = new List<UnitData>();
      if (currentUnits != null)
      {
        for (int index = 0; index < currentUnits.Length; ++index)
        {
          UnitData currentUnit = currentUnits[index];
          if (currentUnit != null && currentUnit.IsMyUnit)
            units.Add(currentUnit);
        }
      }
      else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) && SceneBattle.Instance.Battle != null && SceneBattle.Instance.Battle.Player != null)
      {
        for (int index = 0; index < SceneBattle.Instance.Battle.Player.Count; ++index)
        {
          Unit unit = SceneBattle.Instance.Battle.Player[index];
          if (unit != null && unit.UnitData != null && !unit.IsUnitFlag(EUnitFlag.IsHelp) && !unit.IsNPC && unit.UnitData.IsMyUnit)
            units.Add(unit.UnitData);
        }
      }
      if (units == null || units.Count <= 0)
        return;
      TrophyObjective[] trophiesOfType21 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winquestunit);
      for (int index = trophiesOfType21.Length - 1; index >= 0; --index)
        this.AddCountWinQuestUnit(trophiesOfType21[index], units, (SupportData) null, addCnt);
      TrophyObjective[] trophiesOfType22 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winquestunit_by_quest);
      for (int index = trophiesOfType22.Length - 1; index >= 0; --index)
        this.AddCountWinQuestUnitByQuest(trophiesOfType22[index], units, (SupportData) null, quest, addCnt);
      TrophyObjective[] trophiesOfType23 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winquestunit_by_area);
      if (quest.Chapter != null)
      {
        for (int index = trophiesOfType23.Length - 1; index >= 0; --index)
          this.AddCountWinQuestUnitByArea(trophiesOfType23[index], units, (SupportData) null, quest, addCnt);
      }
      if (quest.IsStoryAll)
      {
        TrophyObjective[] trophiesOfType24 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winquestunit_by_mode);
        for (int index = trophiesOfType24.Length - 1; index >= 0; --index)
          this.AddCountWinQuestUnitByMode(trophiesOfType24[index], units, (SupportData) null, quest, addCnt);
      }
      TrophyObjective[] trophiesOfType25 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winquestunit_support);
      for (int index = trophiesOfType25.Length - 1; index >= 0; --index)
        this.AddCountWinQuestUnit(trophiesOfType25[index], units, support, addCnt);
      TrophyObjective[] trophiesOfType26 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winquestunit_support_by_quest);
      for (int index = trophiesOfType26.Length - 1; index >= 0; --index)
        this.AddCountWinQuestUnitByQuest(trophiesOfType26[index], units, support, quest, addCnt);
      TrophyObjective[] trophiesOfType27 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winquestunit_support_by_area);
      if (quest.Chapter != null)
      {
        for (int index = trophiesOfType27.Length - 1; index >= 0; --index)
          this.AddCountWinQuestUnitByArea(trophiesOfType27[index], units, support, quest, addCnt);
      }
      if (!quest.IsStoryAll)
        return;
      TrophyObjective[] trophiesOfType28 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winquestunit_support_by_mode);
      for (int index = trophiesOfType28.Length - 1; index >= 0; --index)
        this.AddCountWinQuestUnitByMode(trophiesOfType28[index], units, support, quest, addCnt);
    }

    private bool IsSelectUnitInParty(UnitData[] units, SupportData support, UnitParam param)
    {
      if (units == null || param == null)
      {
        DebugUtility.LogError("units,もしくはparamがnullです.");
        return false;
      }
      return support != null && support.Unit.UnitParam.iname == param.iname || Array.Find<UnitData>(units, (Predicate<UnitData>) (unit => unit.UnitParam.iname == param.iname)) != null;
    }

    private void AddCountWinQuestUnit(
      TrophyObjective trophy,
      List<UnitData> units,
      SupportData support,
      int addCnt)
    {
      if (trophy == null)
        return;
      if (trophy.sval == null)
      {
        DebugUtility.LogError("トロフィー:" + trophy.Param.iname + "]でsvalが設定されていません:");
      }
      else
      {
        UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(trophy.sval[0]);
        if (unitParam == null)
        {
          DebugUtility.LogError("トロフィー:" + trophy.Param.iname + "]で指定されたユニットは存在しません:" + trophy.sval[0]);
        }
        else
        {
          if (!this.IsSelectUnitInParty(units.ToArray(), support, unitParam))
            return;
          this.mTrophyData.AddTrophyCounter(trophy, addCnt);
        }
      }
    }

    private void AddCountWinQuestUnitByQuest(
      TrophyObjective trophy,
      List<UnitData> units,
      SupportData support,
      QuestParam quest,
      int addCnt)
    {
      if (trophy == null)
        return;
      if (trophy.sval == null)
      {
        DebugUtility.LogError("トロフィー:" + trophy.Param.iname + "]でsvalが設定されていません:");
      }
      else
      {
        UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(trophy.sval[0]);
        if (unitParam == null)
        {
          DebugUtility.LogError("トロフィー:" + trophy.Param.iname + "]で指定されたユニットは存在しません:" + trophy.sval[0]);
        }
        else
        {
          if (!this.IsSelectUnitInParty(units.ToArray(), support, unitParam))
            return;
          for (int index = 1; index < trophy.sval.Count; ++index)
          {
            QuestParam quest1 = MonoSingleton<GameManager>.Instance.FindQuest(trophy.sval[index]);
            if (quest1 != null && quest1.iname == quest.iname)
            {
              this.mTrophyData.AddTrophyCounter(trophy, addCnt);
              break;
            }
          }
        }
      }
    }

    private void AddCountWinQuestUnitByArea(
      TrophyObjective trophy,
      List<UnitData> units,
      SupportData support,
      QuestParam quest,
      int addCnt)
    {
      if (trophy == null)
        return;
      if (trophy.sval == null)
      {
        DebugUtility.LogError("トロフィー:" + trophy.Param.iname + "]でsvalが設定されていません:");
      }
      else
      {
        UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(trophy.sval[0]);
        if (unitParam == null)
        {
          DebugUtility.LogError("トロフィー:" + trophy.Param.iname + "]で指定されたユニットは存在しません:" + trophy.sval[0]);
        }
        else
        {
          if (!this.IsSelectUnitInParty(units.ToArray(), support, unitParam))
            return;
          for (int index = 1; index < trophy.sval.Count; ++index)
          {
            ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea(trophy.sval[index]);
            if (area != null && quest.Chapter.iname == area.iname)
            {
              this.mTrophyData.AddTrophyCounter(trophy, addCnt);
              break;
            }
          }
        }
      }
    }

    private void AddCountWinQuestUnitByMode(
      TrophyObjective trophy,
      List<UnitData> units,
      SupportData support,
      QuestParam quest,
      int addCnt)
    {
      if (trophy == null)
        return;
      if (trophy.sval == null)
      {
        DebugUtility.LogError("トロフィー:" + trophy.Param.iname + "]でsvalが設定されていません:");
      }
      else
      {
        UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(trophy.sval[0]);
        if (unitParam == null)
        {
          DebugUtility.LogError("トロフィー:" + trophy.Param.iname + "]で指定されたユニットは存在しません:" + trophy.sval[0]);
        }
        else
        {
          if (!this.IsSelectUnitInParty(units.ToArray(), support, unitParam))
            return;
          if (trophy.sval.Count == 1)
          {
            DebugUtility.LogError("トロフィー:" + trophy.Param.iname + "]のsvalに難易度指定がありません.");
          }
          else
          {
            for (int index = 1; index < trophy.sval.Count; ++index)
            {
              QuestDifficulties questDifficulties = QuestParam.GetQuestDifficulties(trophy.sval[index]);
              if (quest.difficulty == questDifficulties)
              {
                this.mTrophyData.AddTrophyCounter(trophy, addCnt);
                break;
              }
            }
          }
        }
      }
    }

    public void OnQuestLose(string questID)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(questID);
      if (questID == null || quest.type == QuestTypes.Tutorial)
        return;
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.losequest);
      for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType1[index];
        if (!string.IsNullOrEmpty(trophyObjective.sval_base))
        {
          if (!trophyObjective.sval.Contains(questID))
            continue;
        }
        else if (quest.type == QuestTypes.Event || quest.type == QuestTypes.Beginner || quest.type == QuestTypes.Arena || quest.IsMulti || quest.type == QuestTypes.Character || quest.difficulty != QuestDifficulties.Normal || quest.type == QuestTypes.Tower || quest.IsVersus || quest.type == QuestTypes.Ordeal || quest.type == QuestTypes.RankMatch || quest.type == QuestTypes.Raid || quest.type == QuestTypes.GuildRaid || quest.type == QuestTypes.GenesisStory || quest.type == QuestTypes.GenesisBoss || quest.type == QuestTypes.AdvanceStory || quest.type == QuestTypes.AdvanceBoss || quest.type == QuestTypes.UnitRental || quest.type == QuestTypes.GvG)
          continue;
        this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
      }
      if (quest.type != QuestTypes.GenesisStory && quest.type != QuestTypes.GenesisBoss && quest.type != QuestTypes.AdvanceStory && quest.type != QuestTypes.AdvanceBoss && quest.difficulty == QuestDifficulties.Elite)
      {
        TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.loseelite);
        for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
          this.mTrophyData.AddTrophyCounter(trophiesOfType2[index], 1);
      }
      if (quest.type == QuestTypes.Arena)
      {
        TrophyObjective[] trophiesOfType3 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.losearena);
        for (int index = trophiesOfType3.Length - 1; index >= 0; --index)
          this.mTrophyData.AddTrophyCounter(trophiesOfType3[index], 1);
      }
      if (quest.type == QuestTypes.Event || quest.type == QuestTypes.Tower || quest.type == QuestTypes.GenesisStory || quest.type == QuestTypes.AdvanceStory)
      {
        TrophyObjective[] trophiesOfType4 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.loseevent);
        for (int index = trophiesOfType4.Length - 1; index >= 0; --index)
          this.mTrophyData.AddTrophyCounter(trophiesOfType4[index], 1);
      }
      if (quest.type == QuestTypes.Tower)
      {
        TrophyObjective[] trophiesOfType5 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.losetower);
        for (int index = trophiesOfType5.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType5[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.ContainsSval(questID))
            this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
        }
        TowerFloorParam towerFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(questID);
        if (towerFloor != null)
        {
          TrophyObjective[] trophiesOfType6 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.tower);
          for (int index = trophiesOfType6.Length - 1; index >= 0; --index)
          {
            TrophyObjective trophyObjective = trophiesOfType6[index];
            if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.ContainsSval(towerFloor.tower_id))
              this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
          }
        }
      }
      if (quest.IsVersus)
      {
        TrophyObjective[] trophiesOfType7 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.vslose);
        for (int index = trophiesOfType7.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType7[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == questID)
            this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
        }
        TrophyObjective[] trophiesOfType8 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.vs);
        for (int index = trophiesOfType8.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType8[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == questID)
            this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
        }
      }
      if (quest.type == QuestTypes.Raid)
      {
        switch (RaidManager.SelectedLastRaidOwnerType)
        {
          case RaidManager.RaidOwnerType.Self:
            TrophyObjective[] trophiesOfType9 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.raidboss);
            for (int index = trophiesOfType9.Length - 1; index >= 0; --index)
              this.mTrophyData.AddTrophyCounter(trophiesOfType9[index], 1);
            break;
          case RaidManager.RaidOwnerType.Rescue:
          case RaidManager.RaidOwnerType.Rescue_Temp:
            if (RaidManager.SelectedLastRaidRescueMemberType == RaidRescueMemberType.Guild)
            {
              TrophyObjective[] trophiesOfType10 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.raidboss_rescue_for_guildmember);
              for (int index = trophiesOfType10.Length - 1; index >= 0; --index)
                this.mTrophyData.AddTrophyCounter(trophiesOfType10[index], 1);
              break;
            }
            break;
        }
      }
      if (quest.type == QuestTypes.GuildRaid && GlobalVars.CurrentBattleType.Get() == GuildRaidBattleType.Main)
      {
        TrophyObjective[] trophiesOfType11 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.guildraid);
        for (int index = trophiesOfType11.Length - 1; index >= 0; --index)
          this.mTrophyData.AddTrophyCounter(trophiesOfType11[index], 1);
      }
      if (quest.type != QuestTypes.GvG)
        return;
      TrophyObjective[] trophiesOfType12 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.gvg);
      for (int index = trophiesOfType12.Length - 1; index >= 0; --index)
        this.mTrophyData.AddTrophyCounter(trophiesOfType12[index], 1);
    }

    public void OnGoldChange(int delta)
    {
      if (delta == 0)
        return;
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.has_gold_over);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        if (this.Gold >= trophiesOfType[index].ival)
          this.mTrophyData.AddTrophyCounter(trophiesOfType[index], 1);
      }
    }

    public void OnCoinChange(int delta)
    {
    }

    public void OnItemQuantityChange(string itemID, int delta)
    {
      if (delta <= 0)
        return;
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.getitem);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (trophyObjective.ContainsSval(itemID))
          this.mTrophyData.AddTrophyCounter(trophyObjective, delta);
      }
    }

    public void OnPlayerLevelChange(int delta)
    {
      if (delta <= 0)
        return;
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.playerlv);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (trophyObjective.ival <= this.Lv)
          this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
      }
    }

    public void OnEnemyKill(string enemyID, int count)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.killenemy);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (trophyObjective.ContainsSval(enemyID))
          this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
      }
    }

    public void OnDamageToEnemy(Unit unit, Unit target, int damage)
    {
      if (unit == null || unit.Side != EUnitSide.Player || !unit.IsPartyMember || target == null || target.Side != EUnitSide.Enemy || UnityEngine.Object.op_Equality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) || SceneBattle.Instance.IsPlayingPreCalcResultQuest || SceneBattle.Instance.Battle != null && SceneBattle.Instance.Battle.IsMultiPlay && (PunMonoSingleton<MyPhoton>.Instance.MyPlayerIndex <= 0 || PunMonoSingleton<MyPhoton>.Instance.MyPlayerIndex != unit.OwnerPlayerIndex))
        return;
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.damage_over);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        if (trophiesOfType[index].ival <= damage)
          this.mTrophyData.AddTrophyCounter(trophiesOfType[index], 1);
      }
    }

    public void OnAbilityPowerUp(string unitID, string abilityID, int level, bool verify = false)
    {
      if (!verify)
      {
        TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.ability);
        for (int index = trophiesOfType.Length - 1; index >= 0; --index)
          this.mTrophyData.AddTrophyCounter(trophiesOfType[index], 1);
      }
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.makeabilitylevel);
      for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType1[index];
        if (trophyObjective.ival <= level)
        {
          if (string.IsNullOrEmpty(trophyObjective.sval_base))
          {
            this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
          }
          else
          {
            char[] chArray = new char[1]{ ',' };
            string[] strArray = trophyObjective.sval_base.Split(chArray);
            if ((string.IsNullOrEmpty(strArray[1]) || abilityID == strArray[1]) && (string.IsNullOrEmpty(strArray[0]) || unitID == strArray[0]))
              this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
          }
        }
      }
    }

    public void OnSoubiPowerUp(int value = 1)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.soubi);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        this.mTrophyData.AddTrophyCounter(trophiesOfType[index], value);
    }

    public void OnBuyGold()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.buygold);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        this.mTrophyData.AddTrophyCounter(trophiesOfType[index], 1);
    }

    public void OnFgGIDLogin()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.fggid);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        this.mTrophyData.AddTrophyCounter(trophiesOfType[index], 1);
    }

    public void OnGacha(GachaTypes type, int count)
    {
      if (count <= 0)
        return;
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.gacha);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (trophyObjective.sval_base == "normal")
        {
          if (type != GachaTypes.Normal)
            continue;
        }
        else if (trophyObjective.sval_base == "rare")
        {
          if (type != GachaTypes.Rare)
            continue;
        }
        else if (trophyObjective.sval_base == "vip" && type != GachaTypes.Vip)
          continue;
        this.mTrophyData.AddTrophyCounter(trophyObjective, count);
      }
    }

    public void OnUnitLevelChange(string unitID, int delta, int level, bool verify = false)
    {
      if (delta <= 0 && !verify)
        return;
      if (!verify)
      {
        TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.unitlevel);
        for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType[index];
          if (trophyObjective.ContainsSval(unitID) && trophyObjective.ival <= level)
            this.mTrophyData.AddTrophyCounter(trophyObjective, delta);
        }
      }
      if (!verify)
      {
        TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.upunitlevel);
        for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == unitID)
            this.mTrophyData.AddTrophyCounter(trophyObjective, delta);
        }
      }
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.makeunitlevel);
      for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType1[index];
        if (trophyObjective.ival <= level && (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.ContainsSval(unitID)))
          this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
      }
    }

    public void OnUnitLevelAndJobLevelChange(string unitID, int level, params JobData[] jobs)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.makeunitandjoblevel);
      char[] chArray = new char[1]{ ',' };
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trohy = trophiesOfType[index];
        string[] strArray = trohy.sval_base.Split(chArray);
        if (strArray.Length < 4)
        {
          this.PrintUnitAndJobLevelUsage(trohy);
        }
        else
        {
          string str1 = strArray[0];
          int result1;
          if (!int.TryParse(strArray[1], out result1))
            this.PrintUnitAndJobLevelUsage(trohy);
          else if (level >= result1 && !string.IsNullOrEmpty(str1) && str1 == unitID)
          {
            string str2 = strArray[2];
            int result2;
            if (!int.TryParse(strArray[3], out result2))
            {
              this.PrintUnitAndJobLevelUsage(trohy);
            }
            else
            {
              foreach (JobData job in jobs)
              {
                if (job.Param.iname == str2 && result2 <= job.Rank)
                  this.mTrophyData.AddTrophyCounter(trohy, 1);
              }
            }
          }
        }
      }
    }

    private void PrintUnitAndJobLevelUsage(TrophyObjective trohy)
    {
      DebugUtility.Log(trohy.Param.iname + ": [" + trohy.sval_base + "]は不正な文字列です。カンマ区切りで「ユニット名・ユニットレベル・ジョブ名・ジョブレベル」の順に設定してください。");
    }

    public void OnEvolutionChange(string unitID, int rarity)
    {
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.evolutionnum);
      for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType1[index];
        if (trophyObjective.ContainsSval(unitID) && trophyObjective.ival <= rarity)
          this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
      }
      TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.evoltiontimes);
      for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType2[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.ContainsSval(unitID))
          this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
      }
    }

    public void OnJobLevelChange(
      string unitID,
      string jobID,
      int rank,
      bool verify = false,
      int rankDelta = 1)
    {
      char[] chArray = new char[1]{ ',' };
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.joblevel);
      if (!verify)
      {
        for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType1[index];
          string[] strArray = trophyObjective.sval_base.Split(chArray);
          if (strArray[0] == unitID && strArray[1] == jobID && trophyObjective.ival <= rank)
            this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
        }
      }
      TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.upjoblevel);
      if (!verify)
      {
        for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType2[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base))
          {
            this.mTrophyData.AddTrophyCounter(trophyObjective, rankDelta);
          }
          else
          {
            string[] strArray = trophyObjective.sval_base.Split(chArray);
            if (strArray[0] == unitID && strArray[1] == jobID)
              this.mTrophyData.AddTrophyCounter(trophyObjective, rankDelta);
          }
        }
      }
      TrophyObjective[] trophiesOfType3 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.makejoblevel);
      for (int index = trophiesOfType3.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType3[index];
        if (trophyObjective.ival <= rank)
        {
          if (string.IsNullOrEmpty(trophyObjective.sval_base))
          {
            this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
          }
          else
          {
            string[] strArray = trophyObjective.sval_base.Split(chArray);
            if (strArray[0] == unitID && strArray[1] == jobID)
              this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
          }
        }
      }
    }

    public void OnMultiTowerHelp()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.multitower_help);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        this.mTrophyData.AddTrophyCounter(trophiesOfType[index], 1);
    }

    public void OnLogin()
    {
      this.TrophyUpdateProgress();
      this.ResetPrevCheckHour();
    }

    public void TrophyUpdateProgress()
    {
      MonoSingleton<GameManager>.Instance.Player.UpdateUnitTrophyStates(true);
      MonoSingleton<GameManager>.Instance.Player.UpdatePlayerTrophyStates();
      MonoSingleton<GameManager>.Instance.Player.UpdateArenaRankTrophyStates();
      MonoSingleton<GameManager>.Instance.Player.UpdateArtifactTrophyStates();
      MonoSingleton<GameManager>.Instance.Player.UpdateTobiraTrophyStates();
      if (!string.IsNullOrEmpty(FlowNode_Variable.Get("COMPLETE_QUEST_MISSION")))
        MonoSingleton<GameManager>.Instance.Player.UpdateCompleteAllQuestCountTrophy2();
      else
        MonoSingleton<GameManager>.Instance.Player.UpdateCompleteAllQuestCountTrophy();
      MonoSingleton<GameManager>.Instance.Player.CheckAllCompleteMissionTrophy();
      MonoSingleton<GameManager>.Instance.Player.UpdateQuestMissionCount();
    }

    public void OnSoubiSet(string unitID, int countUp = 1)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.unitequip);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.ContainsSval(unitID))
          this.mTrophyData.AddTrophyCounter(trophyObjective, countUp);
      }
    }

    public void OnLimitBreak(string unitID, int delta = 1)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.limitbreak);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.ContainsSval(unitID))
          this.mTrophyData.AddTrophyCounter(trophyObjective, delta);
      }
    }

    public void OnJobChange(string unitID)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.changejob);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.ContainsSval(unitID))
          this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
      }
    }

    public void OnChangeAbilitySet(string unitID)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.changeability);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.ContainsSval(unitID))
          this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
      }
    }

    public void OnBuyAtShop(string shopID, string itemID, int num)
    {
      if (num <= 0)
        return;
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.buyatshop);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base))
        {
          this.mTrophyData.AddTrophyCounter(trophyObjective, num);
        }
        else
        {
          char[] chArray = new char[1]{ ',' };
          string[] strArray = trophyObjective.sval_base.Split(chArray);
          if ((string.IsNullOrEmpty(strArray[1]) || itemID == strArray[1]) && (string.IsNullOrEmpty(strArray[0]) || shopID == strArray[0]))
            this.mTrophyData.AddTrophyCounter(trophyObjective, num);
        }
      }
    }

    public void OnArtifactTransmute(string artifactID)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.artifacttransmute);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.ContainsSval(artifactID))
          this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
      }
    }

    public void OnArtifactStrength(
      string artifactID,
      int useItemNum,
      int beforeLevel,
      int currentLevel)
    {
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.artifactstrength);
      for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType1[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.ContainsSval(artifactID))
          this.mTrophyData.AddTrophyCounter(trophyObjective, useItemNum);
      }
      int num = currentLevel - beforeLevel;
      if (num >= 1)
      {
        TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.upartifactlevel);
        for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType2[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || string.Equals(trophyObjective.sval_base, artifactID))
            this.mTrophyData.AddTrophyCounter(trophyObjective, num);
        }
      }
      TrophyObjective[] trophiesOfType3 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.makeartifactlevel);
      for (int index = trophiesOfType3.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType3[index];
        if (currentLevel >= trophyObjective.ival && (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.ContainsSval(artifactID)))
          this.mTrophyData.SetTrophyCounter(trophyObjective, currentLevel);
      }
    }

    public void OnArtifactEvolution(string artifactID)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.artifactevolution);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.ContainsSval(artifactID))
          this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
      }
    }

    public void OnUnlockTobiraTrophy(long unitUniqueID)
    {
      UnitData unitDataByUniqueId = this.FindUnitDataByUniqueID(unitUniqueID);
      this.UpdateUnlockTobiraUnitCountTrophy();
      this.UpdateUnlockTobiraUnitTrophy(unitDataByUniqueId);
    }

    public void OnOpenTobiraTrophy(long unitUniqueID)
    {
      this.UpdateSinsTobiraTrophy(this.FindUnitDataByUniqueID(unitUniqueID));
      this.CheckAllSinsTobiraNonTargetTrophy();
    }

    public void UpdateTobiraTrophyStates()
    {
      this.UpdateUnlockTobiraUnitCountTrophy();
      for (int index = 0; index < this.Units.Count; ++index)
      {
        this.UpdateUnlockTobiraUnitTrophy(this.Units[index]);
        this.UpdateSinsTobiraTrophy(this.Units[index]);
      }
      this.CheckAllSinsTobiraNonTargetTrophy();
    }

    private void UpdateSinsTobiraTrophy(UnitData unitData)
    {
      if (!unitData.IsUnlockTobira)
        return;
      List<TobiraParam.Category> unlockTobiraCategorys = this.GetUnlockTobiraCategorys(unitData);
      for (int index1 = 0; index1 < unlockTobiraCategorys.Count; ++index1)
      {
        TrophyConditionTypes type;
        switch (unlockTobiraCategorys[index1])
        {
          case TobiraParam.Category.Envy:
            type = TrophyConditionTypes.envy_unlock_unit;
            break;
          case TobiraParam.Category.Wrath:
            type = TrophyConditionTypes.sloth_unlock_unit;
            break;
          case TobiraParam.Category.Sloth:
            type = TrophyConditionTypes.lust_unlock_unit;
            break;
          case TobiraParam.Category.Lust:
            type = TrophyConditionTypes.gluttonny_unlock_unit;
            break;
          case TobiraParam.Category.Gluttony:
            type = TrophyConditionTypes.wrath_unlock_unit;
            break;
          case TobiraParam.Category.Greed:
            type = TrophyConditionTypes.greed_unlock_unit;
            break;
          case TobiraParam.Category.Pride:
            type = TrophyConditionTypes.pride_unlock_unit;
            break;
          default:
            continue;
        }
        TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(type);
        for (int index2 = trophiesOfType.Length - 1; index2 >= 0; --index2)
        {
          TrophyObjective trophyObjective = trophiesOfType[index2];
          if (trophyObjective.ContainsSval(unitData.UnitParam.iname))
            this.mTrophyData.SetTrophyCounter(trophyObjective, 1);
        }
      }
    }

    private void CheckAllSinsTobiraNonTargetTrophy()
    {
      this.SetSinsTobiraTrophyByAllUnit(TobiraParam.Category.Envy, TrophyConditionTypes.envy_unlock_unit);
      this.SetSinsTobiraTrophyByAllUnit(TobiraParam.Category.Wrath, TrophyConditionTypes.sloth_unlock_unit);
      this.SetSinsTobiraTrophyByAllUnit(TobiraParam.Category.Sloth, TrophyConditionTypes.lust_unlock_unit);
      this.SetSinsTobiraTrophyByAllUnit(TobiraParam.Category.Lust, TrophyConditionTypes.gluttonny_unlock_unit);
      this.SetSinsTobiraTrophyByAllUnit(TobiraParam.Category.Gluttony, TrophyConditionTypes.wrath_unlock_unit);
      this.SetSinsTobiraTrophyByAllUnit(TobiraParam.Category.Greed, TrophyConditionTypes.greed_unlock_unit);
      this.SetSinsTobiraTrophyByAllUnit(TobiraParam.Category.Pride, TrophyConditionTypes.pride_unlock_unit);
    }

    private void SetSinsTobiraTrophyByAllUnit(
      TobiraParam.Category category,
      TrophyConditionTypes trophyType)
    {
      int num = 0;
      for (int index = 0; index < this.Units.Count; ++index)
      {
        if (this.Units[index].CheckTobiraIsUnlocked(category))
          ++num;
      }
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(trophyType);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base))
          this.mTrophyData.SetTrophyCounter(trophyObjective, num);
      }
    }

    public List<TobiraParam.Category> GetUnlockTobiraCategorys(UnitData unitData)
    {
      List<TobiraParam.Category> unlockTobiraCategorys = new List<TobiraParam.Category>();
      if (unitData.CheckTobiraIsUnlocked(TobiraParam.Category.Envy))
        unlockTobiraCategorys.Add(TobiraParam.Category.Envy);
      if (unitData.CheckTobiraIsUnlocked(TobiraParam.Category.Sloth))
        unlockTobiraCategorys.Add(TobiraParam.Category.Sloth);
      if (unitData.CheckTobiraIsUnlocked(TobiraParam.Category.Lust))
        unlockTobiraCategorys.Add(TobiraParam.Category.Lust);
      if (unitData.CheckTobiraIsUnlocked(TobiraParam.Category.Wrath))
        unlockTobiraCategorys.Add(TobiraParam.Category.Wrath);
      if (unitData.CheckTobiraIsUnlocked(TobiraParam.Category.Greed))
        unlockTobiraCategorys.Add(TobiraParam.Category.Greed);
      if (unitData.CheckTobiraIsUnlocked(TobiraParam.Category.Gluttony))
        unlockTobiraCategorys.Add(TobiraParam.Category.Gluttony);
      if (unitData.CheckTobiraIsUnlocked(TobiraParam.Category.Pride))
        unlockTobiraCategorys.Add(TobiraParam.Category.Pride);
      return unlockTobiraCategorys;
    }

    private void UpdateUnlockTobiraUnitCountTrophy()
    {
      if (this.Units == null)
        return;
      int num = 0;
      for (int index = 0; index < this.Units.Count; ++index)
      {
        if (this.Units[index].IsUnlockTobira)
          ++num;
      }
      if (num <= 0)
        return;
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.unlock_tobira_total);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        this.mTrophyData.SetTrophyCounter(trophiesOfType[index], num);
    }

    private void UpdateUnlockTobiraUnitTrophy(UnitData unitData)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.unlock_tobira_unit);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        if (unitData.IsUnlockTobira)
        {
          TrophyObjective trophyObjective = trophiesOfType[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base))
            DebugUtility.LogError("トロフィー[" + trophyObjective.Param.Name + "]にはユニットが指定されていません。");
          else if (trophyObjective.ContainsSval(unitData.UnitParam.iname))
            this.mTrophyData.SetTrophyCounter(trophyObjective, 1);
        }
      }
    }

    public void OnMixedConceptCard(
      string conceptCardID,
      int beforeLevel,
      int currentLevel,
      int beforeAwakeCount,
      int currentAwakeCount,
      int beforeTrust,
      int currentTrust)
    {
      MonoSingleton<GameManager>.Instance.Player.UpdateConceptCardLevelupTrophy(conceptCardID, beforeLevel, currentLevel);
      MonoSingleton<GameManager>.Instance.Player.UpdateConceptCardLimitBreakTrophy(conceptCardID, beforeAwakeCount, currentAwakeCount);
      MonoSingleton<GameManager>.Instance.Player.UpdateConceptCardTrustUpTrophy(conceptCardID, beforeTrust, currentTrust);
      MonoSingleton<GameManager>.Instance.Player.UpdateConceptCardTrustMaxTrophy(conceptCardID, currentTrust);
    }

    public void UpdateConceptCardTrophyAll()
    {
      if (this.ConceptCards == null)
        return;
      MonoSingleton<GameManager>.Instance.Player.CheckAllConceptCardLevelupTrophy();
      MonoSingleton<GameManager>.Instance.Player.CheckAllConceptCardLimitBreakTrophy();
      MonoSingleton<GameManager>.Instance.Player.CheckAllConceptCardTrustUpTrophy();
      MonoSingleton<GameManager>.Instance.Player.CheckAllConceptCardTrustMaxTrophy();
    }

    public void UpdateConceptCardLevelupTrophy(
      string conceptCardID,
      int beforeLevel,
      int currentLevel)
    {
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.up_conceptcard_level);
      int num = currentLevel - beforeLevel;
      if (num >= 1)
      {
        for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
          this.mTrophyData.AddTrophyCounter(trophiesOfType1[index], num);
      }
      TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.up_conceptcard_level_target);
      for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType2[index];
        if (trophyObjective.ContainsSval(conceptCardID))
        {
          TrophyState trophyCounter = this.mTrophyData.GetTrophyCounter(trophyObjective.Param);
          if (trophyCounter != null && trophyCounter.Count.Length > 0 && trophyCounter.Count[0] <= currentLevel)
            this.mTrophyData.SetTrophyCounter(trophyObjective, currentLevel);
        }
      }
    }

    private void CheckAllConceptCardLevelupTrophy()
    {
      MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.up_conceptcard_level);
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.up_conceptcard_level_target);
      for (int index1 = trophiesOfType.Length - 1; index1 >= 0; --index1)
      {
        TrophyObjective trophyObjective = trophiesOfType[index1];
        for (int index2 = 0; index2 < this.ConceptCards.Count; ++index2)
        {
          if (trophyObjective.ContainsSval(this.ConceptCards[index2].Param.iname))
          {
            TrophyState trophyCounter = this.mTrophyData.GetTrophyCounter(trophyObjective.Param);
            if (trophyCounter != null && trophyCounter.Count.Length > 0 && trophyCounter.Count[0] <= (int) this.ConceptCards[index2].Lv)
              this.mTrophyData.SetTrophyCounter(trophyObjective, (int) this.ConceptCards[index2].Lv);
          }
        }
      }
    }

    public void UpdateConceptCardLimitBreakTrophy(
      string conceptCardID,
      int beforeLimitBreak,
      int currentLimitBreak)
    {
      if (currentLimitBreak <= 0)
        return;
      int num = currentLimitBreak - beforeLimitBreak;
      if (num >= 1)
      {
        TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.limitbreak_conceptcard);
        for (int index = trophiesOfType.Length - 1; index >= 0; --index)
          this.mTrophyData.AddTrophyCounter(trophiesOfType[index], num);
      }
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.limitbreak_conceptcard_target);
      for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType1[index];
        if (!string.IsNullOrEmpty(trophyObjective.sval_base) && trophyObjective.ContainsSval(conceptCardID))
        {
          TrophyState trophyCounter = this.mTrophyData.GetTrophyCounter(trophyObjective.Param);
          if (trophyCounter != null && trophyCounter.Count.Length > 0 && trophyCounter.Count[0] <= currentLimitBreak)
            this.mTrophyData.SetTrophyCounter(trophyObjective, currentLimitBreak);
        }
      }
    }

    public void CheckAllConceptCardLimitBreakTrophy()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.limitbreak_conceptcard_target);
      for (int index1 = trophiesOfType.Length - 1; index1 >= 0; --index1)
      {
        TrophyObjective trophyObjective = trophiesOfType[index1];
        for (int index2 = 0; index2 < this.ConceptCards.Count; ++index2)
        {
          if (trophyObjective.ContainsSval(this.ConceptCards[index2].Param.iname))
          {
            TrophyState trophyCounter = this.mTrophyData.GetTrophyCounter(trophyObjective.Param);
            if (trophyCounter != null && trophyCounter.Count.Length > 0 && trophyCounter.Count[0] <= (int) this.ConceptCards[index2].AwakeCount)
              this.mTrophyData.SetTrophyCounter(trophyObjective, (int) this.ConceptCards[index2].AwakeCount);
          }
        }
      }
    }

    public void UpdateConceptCardTrustUpTrophy(
      string conceptCardID,
      int beforeTrust,
      int currentTrust)
    {
      if (currentTrust == 0)
        return;
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.up_conceptcard_trust);
      int num = currentTrust - beforeTrust;
      if (num >= 1)
      {
        for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
          this.mTrophyData.AddTrophyCounter(trophiesOfType1[index], num);
      }
      TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.up_conceptcard_trust_target);
      for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType2[index];
        if (!string.IsNullOrEmpty(trophyObjective.sval_base) && trophyObjective.ContainsSval(conceptCardID))
        {
          TrophyState trophyCounter = this.mTrophyData.GetTrophyCounter(trophyObjective.Param);
          if (trophyCounter != null && trophyCounter.Count.Length > 0 && trophyCounter.Count[0] <= currentTrust)
            this.mTrophyData.SetTrophyCounter(trophyObjective, currentTrust);
        }
      }
    }

    public void CheckAllConceptCardTrustUpTrophy()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.up_conceptcard_trust_target);
      for (int index1 = trophiesOfType.Length - 1; index1 >= 0; --index1)
      {
        TrophyObjective trophyObjective = trophiesOfType[index1];
        for (int index2 = 0; index2 < this.ConceptCards.Count; ++index2)
        {
          if (trophyObjective.ContainsSval(this.ConceptCards[index2].Param.iname))
          {
            TrophyState trophyCounter = this.mTrophyData.GetTrophyCounter(trophyObjective.Param);
            if (trophyCounter != null && trophyCounter.Count.Length > 0 && trophyCounter.Count[0] <= (int) this.ConceptCards[index2].Trust)
              this.mTrophyData.SetTrophyCounter(trophyObjective, (int) this.ConceptCards[index2].Trust);
          }
        }
      }
    }

    public void UpdateConceptCardTrustMaxTrophy(string conceptCardID, int currentTrust)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.max_conceptcard_trust);
      int cardTrustMax = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
      if (currentTrust < cardTrustMax)
        return;
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base))
          this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
        if (!string.IsNullOrEmpty(trophyObjective.sval_base) && trophyObjective.ContainsSval(conceptCardID))
          this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
      }
    }

    private void CheckAllConceptCardTrustMaxTrophy()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.max_conceptcard_trust);
      int cardTrustMax = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
      for (int index1 = trophiesOfType.Length - 1; index1 >= 0; --index1)
      {
        TrophyObjective trophyObjective = trophiesOfType[index1];
        if (string.IsNullOrEmpty(trophyObjective.sval_base))
        {
          int num = 0;
          for (int index2 = 0; index2 < this.ConceptCards.Count; ++index2)
          {
            if ((int) this.ConceptCards[index2].Trust >= cardTrustMax)
              ++num;
          }
          TrophyState trophyCounter = this.mTrophyData.GetTrophyCounter(trophyObjective.Param);
          if (trophyCounter != null && trophyCounter.Count.Length > 0 && trophyCounter.Count[0] <= num)
            this.mTrophyData.SetTrophyCounter(trophyObjective, num);
        }
        else
        {
          int num = 0;
          for (int index3 = 0; index3 < this.ConceptCards.Count; ++index3)
          {
            if (trophyObjective.ContainsSval(this.ConceptCards[index3].Param.iname) && (int) this.ConceptCards[index3].Trust >= cardTrustMax)
              ++num;
          }
          TrophyState trophyCounter = this.mTrophyData.GetTrophyCounter(trophyObjective.Param);
          if (trophyCounter != null && trophyCounter.Count.Length > 0 && trophyCounter.Count[0] <= num)
            this.mTrophyData.SetTrophyCounter(trophyObjective, num);
        }
      }
    }

    public void UpdateSendFriendPresentTrophy()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.send_present);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        this.mTrophyData.AddTrophyCounter(trophiesOfType[index], 1);
    }

    public void UpdateClearOrdealTrophy(
      BattleCore.Record record,
      QuestTypes questType,
      string questIname)
    {
      if (record.result != BattleCore.QuestResult.Win && questType != QuestTypes.Ordeal)
        return;
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.clear_ordeal);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (!string.IsNullOrEmpty(trophyObjective.sval_base))
        {
          if (trophyObjective.sval_base == questIname)
            this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
        }
        else
          DebugUtility.LogError("レコードミッション「" + trophyObjective.Param.Name + "」はクエストが指定されていません。");
      }
    }

    public void UpdateCompleteAllQuestCountTrophy(QuestParam questParam = null)
    {
      this.UpdateCompleteMissionCount(TrophyConditionTypes.complete_story_mission_count, questParam);
      this.UpdateCompleteMissionCount(TrophyConditionTypes.complete_event_mission_count, questParam);
      this.UpdateCompleteMissionCount(TrophyConditionTypes.complete_ordeal_mission_count, questParam);
      this.UpdateCompleteMissionCount(TrophyConditionTypes.complete_all_mission_count, questParam);
    }

    private void TrophyAllQuestTypeCompleteCount(QuestParam quest = null)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TrophyObjective[] trophiesOfType = instance.GetTrophiesOfType(TrophyConditionTypes.complete_all_mission_count);
      if (quest == null)
      {
        for (int index1 = trophiesOfType.Length - 1; index1 >= 0; --index1)
        {
          TrophyObjective trophyObjective = trophiesOfType[index1];
          int num = 0;
          for (int index2 = 0; index2 < instance.Quests.Length; ++index2)
          {
            if (instance.Quests[index2].IsMissionCompleteALL())
              ++num;
          }
          this.mTrophyData.SetTrophyCounter(trophyObjective, num);
        }
      }
      else
      {
        for (int index = trophiesOfType.Length - 1; index >= 0; --index)
          this.mTrophyData.AddTrophyCounter(trophiesOfType[index], 1);
      }
    }

    private void CheckAllCompleteMissionTrophy()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TrophyObjective[] trophiesOfType = instance.GetTrophiesOfType(TrophyConditionTypes.complete_all_quest_mission_total);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (!string.IsNullOrEmpty(trophyObjective.sval_base))
        {
          QuestParam quest = instance.FindQuest(trophyObjective.sval_base);
          if (quest != null && quest.IsMissionCompleteALL())
            this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
        }
      }
    }

    private void UpdateCompleteMissionCount(TrophyConditionTypes type, QuestParam quest = null)
    {
      QuestTypes questTypes;
      switch (type)
      {
        case TrophyConditionTypes.complete_all_mission_count:
          this.TrophyAllQuestTypeCompleteCount(quest);
          return;
        case TrophyConditionTypes.complete_story_mission_count:
          questTypes = QuestTypes.Story;
          break;
        case TrophyConditionTypes.complete_event_mission_count:
          questTypes = QuestTypes.Event;
          break;
        case TrophyConditionTypes.complete_ordeal_mission_count:
          questTypes = QuestTypes.Ordeal;
          break;
        default:
          DebugUtility.LogError("指定できないミッションが設定されています。");
          return;
      }
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (quest != null)
      {
        TrophyObjective[] trophiesOfType = instance.GetTrophiesOfType(type);
        for (int index1 = trophiesOfType.Length - 1; index1 >= 0; --index1)
        {
          TrophyObjective trophyObjective = trophiesOfType[index1];
          if (questTypes == quest.type)
          {
            if (trophyObjective.sval != null && trophyObjective.sval.Count > 0)
            {
              for (int index2 = 0; index2 < trophyObjective.sval.Count; ++index2)
              {
                if (quest.Chapter != null && trophyObjective.sval[index2] == quest.Chapter.iname)
                {
                  this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
                  break;
                }
              }
            }
            else
              this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
          }
        }
      }
      else
      {
        TrophyObjective[] trophiesOfType = instance.GetTrophiesOfType(type);
        for (int index3 = trophiesOfType.Length - 1; index3 >= 0; --index3)
        {
          int num = 0;
          TrophyObjective trophyObjective = trophiesOfType[index3];
          if (trophyObjective.sval != null && trophyObjective.sval.Count > 0)
          {
            for (int index4 = 0; index4 < instance.Quests.Length; ++index4)
            {
              if (questTypes == instance.Quests[index4].type && instance.Quests[index4].IsMissionCompleteALL() && instance.Quests[index4].Chapter != null)
              {
                for (int index5 = 0; index5 < trophyObjective.sval.Count; ++index5)
                {
                  if (trophyObjective.sval[index5] == instance.Quests[index4].Chapter.iname)
                  {
                    ++num;
                    break;
                  }
                }
              }
            }
          }
          else
          {
            for (int index6 = 0; index6 < instance.Quests.Length; ++index6)
            {
              if (instance.Quests[index6].type == questTypes && instance.Quests[index6].IsMissionCompleteALL())
                ++num;
            }
          }
          this.mTrophyData.SetTrophyCounter(trophyObjective, num);
        }
      }
    }

    public void UpdateViewNewsTrophy(string url)
    {
      if (!url.Contains(Network.NewsHost))
        return;
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.view_news);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        this.mTrophyData.AddTrophyCounter(trophiesOfType[index], 1);
    }

    public void RecordAllCompleteCheck(TrophyCategoryParam category)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.view_news);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        this.mTrophyData.AddTrophyCounter(trophiesOfType[index], 1);
    }

    public void UpdateCompleteAllQuestCountTrophy2(QuestParam questParam = null)
    {
      CompleteQuestMap completeQuestMap = (CompleteQuestMap) null;
      if (questParam == null)
      {
        completeQuestMap = new CompleteQuestMap();
        completeQuestMap.LoadData();
      }
      this.UpdateCompleteMissionCount2(TrophyConditionTypes.complete_story_mission_count, questParam, completeQuestMap);
      this.UpdateCompleteMissionCount2(TrophyConditionTypes.complete_event_mission_count, questParam, completeQuestMap);
      this.UpdateCompleteMissionCount2(TrophyConditionTypes.complete_ordeal_mission_count, questParam, completeQuestMap);
      this.UpdateCompleteMissionCount2(TrophyConditionTypes.complete_all_mission_count, questParam, completeQuestMap);
    }

    private void UpdateCompleteMissionCount2(
      TrophyConditionTypes type,
      QuestParam quest = null,
      CompleteQuestMap completeQuestMap = null)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      QuestTypes key = QuestTypes.None;
      bool flag = false;
      switch (type)
      {
        case TrophyConditionTypes.complete_all_mission_count:
          if (quest == null)
          {
            flag = true;
            break;
          }
          this.TrophyAllQuestTypeCompleteCount(quest);
          return;
        case TrophyConditionTypes.complete_story_mission_count:
          key = QuestTypes.Story;
          break;
        case TrophyConditionTypes.complete_event_mission_count:
          key = QuestTypes.Event;
          break;
        case TrophyConditionTypes.complete_ordeal_mission_count:
          key = QuestTypes.Ordeal;
          break;
        default:
          DebugUtility.LogError("指定できないミッションが設定されています。");
          return;
      }
      if (quest != null)
      {
        TrophyObjective[] trophiesOfType = instance.GetTrophiesOfType(type);
        for (int index1 = trophiesOfType.Length - 1; index1 >= 0; --index1)
        {
          TrophyObjective trophyObjective = trophiesOfType[index1];
          if (key == quest.type)
          {
            if (trophyObjective.sval != null && trophyObjective.sval.Count > 0)
            {
              for (int index2 = 0; index2 < trophyObjective.sval.Count; ++index2)
              {
                if (quest.Chapter != null && trophyObjective.sval[index2] == quest.Chapter.iname)
                {
                  this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
                  break;
                }
              }
            }
            else
              this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
          }
        }
      }
      else
      {
        if (completeQuestMap == null)
        {
          completeQuestMap = new CompleteQuestMap();
          completeQuestMap.LoadData();
        }
        TrophyObjective[] trophiesOfType = instance.GetTrophiesOfType(type);
        if (flag)
        {
          int allCount = completeQuestMap.GetAllCount();
          for (int index = trophiesOfType.Length - 1; index >= 0; --index)
            this.mTrophyData.SetTrophyCounter(trophiesOfType[index], allCount);
        }
        else
        {
          for (int index3 = trophiesOfType.Length - 1; index3 >= 0; --index3)
          {
            TrophyObjective trophyObjective = trophiesOfType[index3];
            int num1 = 0;
            if (!string.IsNullOrEmpty(trophyObjective.sval_base))
            {
              for (int index4 = 0; index4 < trophyObjective.sval.Count; ++index4)
              {
                CompleteQuestMap.CompleteQuestData completeQuestData;
                completeQuestMap.mChapterMap.TryGetValue(trophyObjective.sval[index4], out completeQuestData);
                if (completeQuestData != null)
                {
                  if (key != completeQuestData.mQuestType)
                    DebugUtility.LogError("「" + trophyObjective.Param.iname + "」に指定されたチャプター「" + trophyObjective.sval[index4] + "」は指定のクエストタイプに存在しません。");
                  num1 += completeQuestData.mCount;
                }
              }
              this.mTrophyData.SetTrophyCounter(trophyObjective, num1);
            }
            else
            {
              int num2 = 0;
              completeQuestMap.mQuestTypeMap.TryGetValue(key, out num2);
              int num3 = num1 + num2;
              this.mTrophyData.SetTrophyCounter(trophyObjective, num3);
            }
          }
        }
      }
    }

    public void OnChallengeMissionComplete(string trophyID)
    {
      foreach (TrophyParam trophy in MonoSingleton<GameManager>.Instance.Trophies)
      {
        if (trophy.IsChallengeMissionRoot && trophy.iname == trophyID)
          this.mTrophyData.AddTrophyCounter(trophy, 0, 1);
      }
    }

    public void OnTowerScore(bool isNow = true)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TowerResuponse towerResuponse = instance.TowerResuponse;
      if (towerResuponse == null || string.IsNullOrEmpty(towerResuponse.TowerID) || towerResuponse.speedRank == 0 && towerResuponse.techRank == 0)
        return;
      int num = instance.CalcTowerScore(isNow);
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.overtowerscore);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (num <= trophyObjective.ival && (string.IsNullOrEmpty(trophyObjective.sval_base) || string.Equals(trophyObjective.sval_base, towerResuponse.TowerID)))
          this.mTrophyData.SetTrophyCounter(trophyObjective, num);
      }
    }

    public void OnReadTips(string trophyIname)
    {
      foreach (TrophyObjective trophyObjective in MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.read_tips))
      {
        if (trophyObjective.ContainsSval(trophyIname))
          this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
      }
      foreach (TrophyObjective trophyObjective in MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.read_tips_count))
        this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
    }

    public void UpdateVipDailyMission(int vipLv)
    {
    }

    public void UpdateCardDailyMission()
    {
      if ((long) this.mVipExpiredAt == 0L || TimeManager.FromUnixTime((long) this.mVipExpiredAt) < TimeManager.ServerTime)
        return;
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.card);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        TrophyState trophyCounter = this.mTrophyData.GetTrophyCounter(trophyObjective.Param);
        if (trophyCounter != null && !trophyCounter.IsCompleted)
          this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
      }
    }

    private void ResetPrevCheckHour() => this.mPrevCheckHour = -1;

    public void UpdateStaminaDailyMission()
    {
      if (!this.mUpdateInterval.PlayCheckUpdate())
        return;
      int hour = TimeManager.ServerTime.Hour;
      if (hour == this.mPrevCheckHour)
        return;
      this.mUpdateInterval.SetUpdateInterval();
      this.mPrevCheckHour = hour;
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.stamina);
      List<int> mealHours = MonoSingleton<WatchManager>.Instance.GetMealHours();
      for (int index1 = trophiesOfType.Length - 1; index1 >= 0; --index1)
      {
        TrophyObjective trophyObjective = trophiesOfType[index1];
        TrophyState trophyCounter = this.mTrophyData.GetTrophyCounter(trophyObjective.Param);
        if (trophyCounter != null && !trophyCounter.IsCompleted)
        {
          int num1 = int.Parse(trophyObjective.sval_base.Substring(0, 2));
          int num2 = int.Parse(trophyObjective.sval_base.Substring(3, 2));
          if (num1 <= hour && hour < num2)
            this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
          if (mealHours != null)
          {
            for (int index2 = 0; index2 < mealHours.Count; ++index2)
            {
              if (num1 <= mealHours[index2] && mealHours[index2] < num2)
                this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
            }
          }
        }
      }
    }

    public void UpdateArtifactTrophyStates()
    {
      if (this.mArtifacts.Count < 1)
        return;
      int num = 1;
      Dictionary<string, ArtifactData> dictionary = new Dictionary<string, ArtifactData>();
      for (int index = 0; index < this.mArtifacts.Count; ++index)
      {
        ArtifactData mArtifact = this.mArtifacts[index];
        if (mArtifact != null)
        {
          num = Mathf.Max(num, (int) mArtifact.Lv);
          if (mArtifact.ArtifactParam != null)
          {
            if (!dictionary.ContainsKey(mArtifact.ArtifactParam.iname))
              dictionary.Add(mArtifact.ArtifactParam.iname, mArtifact);
            else if ((int) dictionary[mArtifact.ArtifactParam.iname].Lv < (int) mArtifact.Lv)
              dictionary[mArtifact.ArtifactParam.iname] = mArtifact;
          }
        }
      }
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.makeartifactlevel);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        if (string.IsNullOrEmpty(trophiesOfType[index].sval_base))
        {
          this.mTrophyData.SetTrophyCounter(trophiesOfType[index], num);
        }
        else
        {
          foreach (string key in dictionary.Keys)
          {
            if (trophiesOfType[index].ContainsSval(key))
              this.mTrophyData.SetTrophyCounter(trophiesOfType[index], (int) dictionary[key].Lv);
          }
        }
      }
    }

    public void UpdatePlayerTrophyStates()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.playerlv);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (this.Lv >= trophyObjective.ival)
          this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
      }
    }

    public void UpdateArenaRankTrophyStates(int currentRank = -1, int bestRank = -1)
    {
      if (currentRank == -1)
        currentRank = this.ArenaRank;
      if (bestRank == -1)
        bestRank = this.ArenaRankBest;
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.becomearenarank);
      for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType1[index];
        if (currentRank == trophyObjective.ival || bestRank == trophyObjective.ival)
          this.mTrophyData.SetTrophyCounter(trophyObjective, trophyObjective.ival);
      }
      TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.overarenarank);
      for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType2[index];
        if (bestRank <= trophyObjective.ival)
          this.mTrophyData.SetTrophyCounter(trophyObjective, bestRank);
      }
    }

    public void UpdateTowerTrophyStates() => this.OnTowerScore(false);

    public void UpdateVersusTowerTrophyStates(string towerName, int currentFloor)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.overvsrankfloor);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if ((string.IsNullOrEmpty(trophyObjective.sval_base) || string.Equals(trophyObjective.sval_base, towerName)) && currentFloor >= trophyObjective.ival)
          this.mTrophyData.SetTrophyCounter(trophyObjective, currentFloor);
      }
    }

    public void UpdateJoinGuild()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.join_guild);
      if (trophiesOfType == null || trophiesOfType.Length <= 0)
        return;
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        if (this.IsGuildAssign)
          this.mTrophyData.AddTrophyCounter(trophiesOfType[index], 1);
      }
    }

    public void UpdateFriendCount() => this.UpdateFriendCount(this.FriendNum);

    public void UpdateFriendCount(int friend_count)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.friend_count);
      if (trophiesOfType == null || trophiesOfType.Length <= 0)
        return;
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyState trophyCounter = this.mTrophyData.GetTrophyCounter(trophiesOfType[index].Param);
        if (trophyCounter == null)
          this.mTrophyData.SetTrophyCounter(trophiesOfType[index], friend_count);
        else if (trophyCounter.Count[0] <= friend_count)
          this.mTrophyData.SetTrophyCounter(trophiesOfType[index], friend_count);
      }
    }

    public void UpdateQuestMissionCount(QuestParam questParam = null, BattleCore.Record recode = null)
    {
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      QuestParam[] clearedQuests = this.GetClearedQuests();
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.clear_questmission_count);
      int num1 = 0;
      for (int index = 0; index < clearedQuests.Length; ++index)
      {
        QuestParam questParam1 = clearedQuests[index];
        if (clearedQuests != null)
        {
          if (questParam != null && recode != null)
          {
            if (questParam.iname == questParam1.iname)
              num1 += GameUtility.GetBitCount((long) recode.allBonusFlags);
            else
              num1 += questParam1.GetClearMissionNum();
          }
          else
            num1 += questParam1.GetClearMissionNum();
        }
      }
      for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType1[index];
        if (trophiesOfType1 != null)
          this.mTrophyData.SetTrophyCounter(trophyObjective, num1);
      }
      dictionary.Clear();
      TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.clear_questmission_count_by_quest);
      for (int index1 = trophiesOfType2.Length - 1; index1 >= 0; --index1)
      {
        TrophyObjective trophyObjective = trophiesOfType2[index1];
        if (trophyObjective != null && trophyObjective.sval_base != null && trophyObjective.SvalCount > 0)
        {
          int num2 = 0;
          for (int index2 = 0; index2 < trophyObjective.sval.Count; ++index2)
          {
            string str = trophyObjective.sval[index2];
            if (!dictionary.ContainsKey(str))
            {
              QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(str);
              if (quest != null && quest.state == QuestStates.Cleared && quest.HasMission())
              {
                if (questParam != null && recode != null)
                {
                  if (quest.iname == questParam.iname)
                  {
                    int bitCount = GameUtility.GetBitCount((long) recode.allBonusFlags);
                    dictionary.Add(str, bitCount);
                  }
                  else
                    dictionary.Add(str, quest.GetClearMissionNum());
                }
                else
                  dictionary.Add(str, quest.GetClearMissionNum());
              }
              else
                continue;
            }
            num2 += dictionary[str];
            this.mTrophyData.SetTrophyCounter(trophyObjective, num2);
            if (this.mTrophyData.GetTrophyCounter(trophyObjective.Param).IsCompleted)
              break;
          }
        }
      }
      dictionary.Clear();
      TrophyObjective[] trophiesOfType3 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.clear_questmission_count_by_area);
      for (int index3 = trophiesOfType3.Length - 1; index3 >= 0; --index3)
      {
        TrophyObjective trophyObjective = trophiesOfType3[index3];
        if (trophyObjective != null && trophyObjective.sval_base != null && trophyObjective.SvalCount > 0)
        {
          int num3 = 0;
          for (int index4 = 0; index4 < trophyObjective.sval.Count; ++index4)
          {
            string str = trophyObjective.sval[index4];
            if (!string.IsNullOrEmpty(str))
            {
              if (!dictionary.ContainsKey(str))
              {
                ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea(str);
                if (area != null && area.quests != null && area.quests.Count > 0)
                {
                  int num4 = 0;
                  for (int index5 = 0; index5 < area.quests.Count; ++index5)
                  {
                    QuestParam quest = area.quests[index5];
                    if (quest != null && quest.state == QuestStates.Cleared && quest.HasMission())
                    {
                      if (questParam != null && recode != null)
                      {
                        if (quest.iname == questParam.iname)
                          num4 += GameUtility.GetBitCount((long) recode.allBonusFlags);
                        else
                          num4 += quest.GetClearMissionNum();
                      }
                      else
                        num4 += quest.GetClearMissionNum();
                    }
                  }
                  dictionary.Add(str, num4);
                }
                else
                  continue;
              }
              num3 += dictionary[str];
              this.mTrophyData.SetTrophyCounter(trophyObjective, num3);
              if (MonoSingleton<GameManager>.Instance.Player.TrophyData.GetTrophyCounter(trophyObjective.Param).IsCompleted)
                break;
            }
          }
        }
      }
      dictionary.Clear();
      TrophyObjective[] trophiesOfType4 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.clear_questmission_count_by_mode);
      for (int index6 = trophiesOfType4.Length - 1; index6 >= 0; --index6)
      {
        TrophyObjective trophyObjective = trophiesOfType4[index6];
        if (trophyObjective != null)
        {
          int num5 = 0;
          if (string.IsNullOrEmpty(trophyObjective.sval_base))
          {
            DebugUtility.LogError("トロフィー[" + trophyObjective.Param.iname + "]にsvalが指定されていません.");
          }
          else
          {
            for (int index7 = 0; index7 < trophyObjective.SvalCount; ++index7)
            {
              string str = trophyObjective.sval[index7];
              if (!string.IsNullOrEmpty(str))
              {
                QuestDifficulties questDifficulties = QuestParam.GetQuestDifficulties(str);
                if (!dictionary.ContainsKey(questDifficulties.ToString()))
                {
                  int num6 = 0;
                  for (int index8 = 0; index8 < clearedQuests.Length; ++index8)
                  {
                    QuestParam questParam2 = clearedQuests[index8];
                    if (questParam2 != null && questParam2.IsStoryAll && questParam2.state == QuestStates.Cleared && questParam2.HasMission() && questParam2.difficulty == questDifficulties)
                    {
                      if (questParam != null && recode != null)
                      {
                        if (questParam.iname == questParam2.iname)
                          num6 += GameUtility.GetBitCount((long) recode.allBonusFlags);
                        else
                          num6 += questParam2.GetClearMissionNum();
                      }
                      else
                        num6 += questParam2.GetClearMissionNum();
                    }
                  }
                  dictionary.Add(questDifficulties.ToString(), num6);
                }
                num5 += dictionary[questDifficulties.ToString()];
                this.mTrophyData.SetTrophyCounter(trophyObjective, num5);
              }
            }
          }
        }
      }
    }

    public void ClearNewItemFlags()
    {
      for (int index = this.mItems.Count - 1; index >= 0; --index)
      {
        if (this.mItems[index] != null)
          this.mItems[index].IsNew = false;
      }
    }

    public void ClearItemFlags(ItemData.ItemFlags flags)
    {
      if (flags == (ItemData.ItemFlags) 0)
        return;
      for (int index = this.mItems.Count - 1; index >= 0; --index)
      {
        if (this.mItems[index] != null)
          this.mItems[index].ResetFlag(flags);
      }
    }

    public bool ItemEntryExists(string iname) => this.mID2ItemData.ContainsKey(iname);

    public void ClearUnits()
    {
      if (this.mUnits != null)
        this.mUnits.Clear();
      if (this.mUniqueID2UnitData == null)
        return;
      this.mUniqueID2UnitData.Clear();
    }

    public void ClearItems()
    {
      if (this.mItems != null)
        this.mItems.Clear();
      if (this.mID2ItemData == null)
        return;
      this.mID2ItemData.Clear();
    }

    public void ClearArtifacts()
    {
      this.mArtifacts.Clear();
      this.mArtifactsNumByRarity.Clear();
    }

    public void OfflineSellArtifacts(ArtifactData[] artifacts)
    {
      if (artifacts == null)
        return;
      for (int index = 0; index < artifacts.Length; ++index)
      {
        this.RemoveArtifact(artifacts[index]);
        this.GainGold(artifacts[index].ArtifactParam.sell);
      }
    }

    public void UpdateArtifactOwner()
    {
      for (int index1 = 0; index1 < this.mUnits.Count; ++index1)
      {
        if (this.mUnits[index1] != null && this.mUnits[index1].Jobs != null)
        {
          for (int index2 = 0; index2 < this.mUnits[index1].Jobs.Length; ++index2)
          {
            if (this.mUnits[index1].Jobs[index2] != null && this.mUnits[index1].Jobs[index2].ArtifactDatas != null)
            {
              for (int index3 = 0; index3 < this.mUnits[index1].Jobs[index2].Artifacts.Length; ++index3)
                this.mUnits[index1].Jobs[index2].ArtifactDatas[index3] = (ArtifactData) null;
            }
          }
        }
      }
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (this.mUnits[index] != null && this.mUnits[index].Jobs != null)
        {
          for (int job_index = 0; job_index < this.mUnits[index].Jobs.Length; ++job_index)
          {
            if (this.mUnits[index].Jobs[job_index] != null && this.mUnits[index].Jobs[job_index].ArtifactDatas != null)
            {
              for (int slot = 0; slot < this.mUnits[index].Jobs[job_index].Artifacts.Length; ++slot)
              {
                if (this.mUnits[index].Jobs[job_index].Artifacts[slot] == 0L)
                {
                  this.mUnits[index].Jobs[job_index].ArtifactDatas[slot] = (ArtifactData) null;
                }
                else
                {
                  ArtifactData artifactByUniqueId = this.FindArtifactByUniqueID(this.mUnits[index].Jobs[job_index].Artifacts[slot]);
                  this.mUnits[index].SetEquipArtifactData(job_index, slot, artifactByUniqueId, this.mUnits[index].JobIndex == job_index);
                }
              }
            }
          }
          this.mUnits[index].UpdateArtifact(this.mUnits[index].JobIndex);
        }
      }
    }

    public bool IsBeginner()
    {
      double beginnerDays = (double) (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.BeginnerDays;
      double totalDays = new TimeSpan(TimeManager.FromUnixTime((long) (int) this.mNewGameAt).Ticks).TotalDays;
      double num = new TimeSpan(TimeManager.FromUnixTime(Network.GetServerTime()).Ticks).TotalDays - totalDays;
      return beginnerDays > num;
    }

    public DateTime GetBeginnerEndTime()
    {
      double beginnerDays = (double) (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.BeginnerDays;
      return TimeManager.FromUnixTime((long) (int) this.mNewGameAt).AddDays(beginnerDays);
    }

    public void SetBeginnerNotified()
    {
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.LAST_BEGINNER_NOTIFIED_DATE, TimeManager.ServerTime.ToString("yyyy/MM/dd"));
    }

    public bool NeedsShowBeginnerNotify()
    {
      if (!this.IsBeginner() || PlayerPrefsUtility.GetInt(PlayerPrefsUtility.BEGINNER_TOP_HAS_VISITED) != 0)
        return false;
      string s = PlayerPrefsUtility.GetString(PlayerPrefsUtility.LAST_BEGINNER_NOTIFIED_DATE, string.Empty);
      if (string.IsNullOrEmpty(s))
        return true;
      DateTime result;
      if (DateTime.TryParse(s, out result))
      {
        long num = TimeManager.FromDateTime(result.AddDays(1.0));
        return TimeManager.FromDateTime(TimeManager.ServerTime) > num;
      }
      this.SetBeginnerNotified();
      return false;
    }

    public Dictionary<ItemParam, int> CreateItemSnapshot()
    {
      Dictionary<ItemParam, int> itemSnapshot = new Dictionary<ItemParam, int>();
      for (int index = 0; index < this.mItems.Count; ++index)
        itemSnapshot[this.mItems[index].Param] = this.mItems[index].NumNonCap;
      return itemSnapshot;
    }

    public void GainPiecePoint(int point)
    {
      this.mPiecePoint = (OInt) Math.Max((int) this.mPiecePoint + point, 0);
    }

    public string DequeueNextLoginBonusTableID()
    {
      return this.mLoginBonusQueue.Count < 1 ? (string) null : this.mLoginBonusQueue.Dequeue();
    }

    public bool HasQueuedLoginBonus => this.mLoginBonusQueue.Count > 0;

    public void UpdateUnitTrophyStates(bool verbose)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      for (int index1 = 0; index1 < this.mUnits.Count; ++index1)
      {
        UnitData mUnit = this.mUnits[index1];
        if (mUnit != null)
        {
          if (!mUnit.IsRental)
            num1 += mUnit.Lv;
          ++num2;
          JobData[] jobs = mUnit.Jobs;
          if (jobs != null)
          {
            for (int index2 = 0; index2 < jobs.Length; ++index2)
            {
              if (jobs[index2] != null)
              {
                if (jobs[index2].Rank >= 11)
                {
                  ++num3;
                  break;
                }
                if (jobs[index2].Rank > 0)
                {
                  JobSetParam jobSetFast = mUnit.UnitParam.GetJobSetFast(index2);
                  if (jobSetFast != null && !string.IsNullOrEmpty(jobSetFast.jobchange))
                  {
                    JobSetParam jobSetParam = masterParam.GetJobSetParam(jobSetFast.jobchange);
                    if (jobSetParam != null && jobSetParam.job == jobs[index2].JobID)
                    {
                      ++num3;
                      break;
                    }
                  }
                }
              }
            }
          }
          if (mUnit.UnitParam != null && mUnit.Rarity - (int) mUnit.UnitParam.rare > 0)
            ++num4;
          num5 += mUnit.AwakeLv;
        }
      }
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.collectunits);
      for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
      {
        if (trophiesOfType1[index].ival <= num2)
          this.mTrophyData.AddTrophyCounter(trophiesOfType1[index], 1);
      }
      TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.totaljoblv11);
      for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
      {
        if (trophiesOfType2[index].ival <= num3)
          this.mTrophyData.AddTrophyCounter(trophiesOfType2[index], 1);
      }
      TrophyObjective[] trophiesOfType3 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.totalunitlvs);
      for (int index = trophiesOfType3.Length - 1; index >= 0; --index)
        this.mTrophyData.SetTrophyCounter(trophiesOfType3[index], num1);
      TrophyObjective[] trophiesOfType4 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.evoltiontimes);
      for (int index = trophiesOfType4.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType4[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base))
          this.mTrophyData.SetTrophyCounter(trophyObjective, num4);
      }
      TrophyObjective[] trophiesOfType5 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.limitbreak);
      for (int index = trophiesOfType5.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType5[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base))
          this.mTrophyData.SetTrophyCounter(trophyObjective, num5);
      }
      if (!verbose)
        return;
      for (int index3 = 0; index3 < this.mUnits.Count; ++index3)
      {
        UnitData mUnit = this.mUnits[index3];
        if (mUnit == null || mUnit.UnitParam == null)
          break;
        string iname = mUnit.UnitParam.iname;
        this.OnUnitLevelChange(iname, 0, mUnit.Lv, true);
        JobData[] jobs = mUnit.Jobs;
        if (jobs != null)
        {
          for (int index4 = 0; index4 < jobs.Length; ++index4)
            this.OnJobLevelChange(iname, jobs[index4].JobID, jobs[index4].Rank, true);
        }
        this.OnUnitLevelAndJobLevelChange(iname, mUnit.Lv, mUnit.Jobs);
        List<AbilityData> learnAbilitys = mUnit.LearnAbilitys;
        for (int index5 = 0; index5 < learnAbilitys.Count; ++index5)
          this.OnAbilityPowerUp(iname, learnAbilitys[index5].AbilityID, learnAbilitys[index5].Rank, true);
        if (mUnit.Rarity > (int) mUnit.UnitParam.rare)
          this.OnEvolutionCheck(iname, mUnit.Rarity, (int) mUnit.UnitParam.rare);
        this.OnLimitBreakCheck(iname, mUnit.AwakeLv);
      }
    }

    public void OnUnitGet()
    {
      int num = 0;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        UnitData mUnit = this.mUnits[index];
        if (mUnit != null && !mUnit.IsRental)
          num += mUnit.Lv;
      }
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.totalunitlvs);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        this.mTrophyData.SetTrophyCounter(trophiesOfType[index], num);
    }

    public void OnEvolutionCheck(string unitID, int rarity, int initialRarity)
    {
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.evolutionnum);
      for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType1[index];
        if (trophyObjective.ContainsSval(unitID) && trophyObjective.ival <= rarity)
          this.mTrophyData.AddTrophyCounter(trophyObjective, 1);
      }
      int num = rarity - initialRarity;
      if (num < 1)
        return;
      TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.evoltiontimes);
      for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType2[index];
        if (!string.IsNullOrEmpty(trophyObjective.sval_base) && trophyObjective.ContainsSval(unitID))
          this.mTrophyData.SetTrophyCounter(trophyObjective, num);
      }
    }

    public void OnLimitBreakCheck(string unitID, int awake)
    {
      if (awake <= 0)
        return;
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.limitbreak);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (!string.IsNullOrEmpty(trophyObjective.sval_base) && trophyObjective.sval_base == unitID)
          this.mTrophyData.SetTrophyCounter(trophyObjective, awake);
      }
    }

    public void SetupEventCoin()
    {
      if (this.mEventCoinList.Count != 0 || MonoSingleton<GameManager>.Instance.MasterParam.Items == null)
        return;
      List<ItemParam> items = MonoSingleton<GameManager>.Instance.MasterParam.Items;
      for (int index = 0; index < items.Count; ++index)
      {
        if (items[index].type == EItemType.EventCoin)
          this.mEventCoinList.Add(new EventCoinData()
          {
            iname = items[index].iname,
            param = items[index]
          });
      }
    }

    public void UpdateEventCoin()
    {
      this.SetupEventCoin();
      for (int i = 0; i < this.mEventCoinList.Count; ++i)
      {
        ItemData itemData = MonoSingleton<GameManager>.Instance.Player.Items.Find((Predicate<ItemData>) (f => f.Param.iname.Equals(this.mEventCoinList[i].iname)));
        if (itemData != null)
          this.mEventCoinList[i].have = itemData;
      }
    }

    public int EventCoinNum(string cost_iname)
    {
      if (cost_iname == null)
        return 0;
      EventCoinData eventCoinData = this.mEventCoinList.Find((Predicate<EventCoinData>) (f => f.iname.Equals(cost_iname)));
      return eventCoinData != null && eventCoinData.have != null ? eventCoinData.have.Num : 0;
    }

    public void SetEventCoinNum(string cost_iname, int num)
    {
      if (cost_iname == null)
        return;
      MonoSingleton<GameManager>.Instance.Player.Items.Find((Predicate<ItemData>) (f => f.Param.iname.Equals(cost_iname)))?.SetNum(num);
    }

    public void SetVersusPlacement(string key, int idx) => PlayerPrefsUtility.SetInt(key, idx);

    public int GetVersusPlacement(string key) => PlayerPrefsUtility.GetInt(key);

    public void SetTowerMatchInfo(int floor, int key, int wincnt, bool gift)
    {
      this.mVersusTwFloor = floor;
      this.mVersusTwKey = key;
      this.mVersusTwWinCnt = wincnt;
      this.mVersusSeasonGift = gift;
    }

    public void SetRankMatchInfo(
      int _rank,
      int _score,
      RankMatchClass _class,
      int _battle_point,
      int _streak_win,
      int _wincnt,
      int _losecnt)
    {
      this.mRankMatchOldClass = this.mRankMatchClass;
      this.mRankMatchOldRank = this.mRankMatchRank;
      this.mRankMatchOldScore = this.mRankMatchScore;
      this.mRankMatchRank = _rank;
      this.mRankMatchScore = _score;
      this.mRankMatchClass = _class;
      this.mRankMatchBattlePoint = _battle_point;
      this.mRankMatchStreakWin = _streak_win;
      this.RankMatchWinCount = _wincnt;
      this.RankMatchLoseCount = _losecnt;
    }

    public bool IsHaveAward(string award)
    {
      return this.mHaveAward != null && this.mHaveAward.Contains(award);
    }

    public void UpdateAchievementTrophyStates()
    {
      List<AchievementParam> achievementData = GameCenterManager.GetAchievementData();
      if (achievementData == null || achievementData.Count < 1)
        return;
      for (int index = 0; index < achievementData.Count; ++index)
      {
        AchievementParam achievementParam = achievementData[index];
        List<TrophyState> tState;
        if (this.mTrophyData.DictTryGetValue(achievementParam.iname, out tState) && tState[0].IsCompleted)
          GameCenterManager.SendAchievementProgress(achievementParam);
      }
    }

    public void SetWishList(string iname, int priority)
    {
      this.FriendPresentWishList.Set(iname, priority);
    }

    public void SetQuestListDirty() => this.mQuestListDirty = true;

    public List<ConceptCardData> ConceptCards => this.mConceptCards;

    public List<ConceptCardMaterialData> ConceptCardExpMaterials => this.mConceptCardExpMaterials;

    public List<ConceptCardMaterialData> ConceptCardTrustMaterials
    {
      get => this.mConceptCardTrustMaterials;
    }

    public List<SkinConceptCardData> SkinConceptCards => this.mSkinConceptCards;

    public ConceptCardData FindConceptCardByUniqueID(long iid)
    {
      return this.mConceptCards.Find((Predicate<ConceptCardData>) (card => (long) card.UniqueID == iid));
    }

    public void RemoveConceptCardData(long[] iids)
    {
      this.mConceptCards.RemoveAll((Predicate<ConceptCardData>) (card =>
      {
        for (int index = 0; index < iids.Length; ++index)
        {
          if ((long) card.UniqueID == iids[index])
            return true;
        }
        return false;
      }));
      for (int index1 = 0; index1 < iids.Length; ++index1)
      {
        for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
        {
          int indexOfConceptCard = this.mUnits[index2].FindIndexOfConceptCard(iids[index1]);
          if (indexOfConceptCard != -1)
            this.mUnits[index2].SetConceptCardByIndex(indexOfConceptCard, (ConceptCardData) null);
        }
      }
      this.UpdateConceptCardNum();
    }

    public void UpdateConceptCardNum()
    {
      this.mConceptCardNum.Clear();
      for (int index = 0; index < this.mConceptCards.Count; ++index)
      {
        string iname = this.mConceptCards[index].Param.iname;
        if (this.mConceptCardNum.ContainsKey(iname))
        {
          Dictionary<string, int> mConceptCardNum;
          string key;
          (mConceptCardNum = this.mConceptCardNum)[key = iname] = mConceptCardNum[key] + 1;
        }
        else
          this.mConceptCardNum.Add(iname, 1);
      }
    }

    public void UpdateConceptCardNum(string[] inames)
    {
      this.mConceptCardNum.Clear();
      for (int index = 0; index < inames.Length; ++index)
      {
        string iname = inames[index];
        if (this.mConceptCardNum.ContainsKey(iname))
        {
          Dictionary<string, int> mConceptCardNum;
          string key;
          (mConceptCardNum = this.mConceptCardNum)[key = iname] = mConceptCardNum[key] + 1;
        }
        else
          this.mConceptCardNum.Add(iname, 1);
      }
    }

    public int GetConceptCardNum(string iname)
    {
      int conceptCardNum = 0;
      this.mConceptCardNum.TryGetValue(iname, out conceptCardNum);
      return conceptCardNum;
    }

    public int GetConceptCardMaterialNum(string iname)
    {
      int conceptCardMaterialNum = 0;
      ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(iname);
      if (conceptCardParam == null)
        return conceptCardMaterialNum;
      ConceptCardMaterialData cardMaterialData = (ConceptCardMaterialData) null;
      if (conceptCardParam.type == eCardType.Enhance_exp)
        cardMaterialData = this.mConceptCardExpMaterials.Find((Predicate<ConceptCardMaterialData>) (p => (string) p.IName == iname));
      else if (conceptCardParam.type == eCardType.Enhance_trust)
        cardMaterialData = this.mConceptCardTrustMaterials.Find((Predicate<ConceptCardMaterialData>) (p => (string) p.IName == iname));
      if (cardMaterialData != null)
        conceptCardMaterialNum = (int) cardMaterialData.Num;
      return conceptCardMaterialNum;
    }

    public OLong GetConceptCardMaterialUniqueID(string iname)
    {
      OLong materialUniqueId = (OLong) -1L;
      ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(iname);
      if (conceptCardParam == null)
        return materialUniqueId;
      ConceptCardMaterialData cardMaterialData = (ConceptCardMaterialData) null;
      if (conceptCardParam.type == eCardType.Enhance_exp)
        cardMaterialData = this.mConceptCardExpMaterials.Find((Predicate<ConceptCardMaterialData>) (p => (string) p.IName == iname));
      else if (conceptCardParam.type == eCardType.Enhance_trust)
        cardMaterialData = this.mConceptCardTrustMaterials.Find((Predicate<ConceptCardMaterialData>) (p => (string) p.IName == iname));
      if (cardMaterialData != null)
        materialUniqueId = cardMaterialData.UniqueID;
      return materialUniqueId;
    }

    public int GetEnhanceConceptCardMaterial()
    {
      int conceptCardMaterial = 0;
      if (this.mConceptCardExpMaterials != null)
        conceptCardMaterial += this.mConceptCardExpMaterials.Count;
      if (this.mConceptCardTrustMaterials != null)
        conceptCardMaterial += this.mConceptCardTrustMaterials.Count;
      return conceptCardMaterial;
    }

    public void OverWriteConceptCardMaterials(JSON_ConceptCardMaterial[] concept_card_materials)
    {
      if (concept_card_materials == null)
        return;
      ConceptCardMaterialData cardMaterialData = (ConceptCardMaterialData) null;
      for (int index = 0; index < concept_card_materials.Length; ++index)
      {
        ConceptCardParam param = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(concept_card_materials[index].iname);
        if (param != null)
        {
          if (param.type == eCardType.Enhance_exp)
          {
            cardMaterialData = this.mConceptCardExpMaterials.Find((Predicate<ConceptCardMaterialData>) (p => (string) p.IName == param.iname));
            if (cardMaterialData != null)
              cardMaterialData.Num = (OInt) concept_card_materials[index].num;
          }
          else if (param.type == eCardType.Enhance_trust)
          {
            cardMaterialData = this.mConceptCardTrustMaterials.Find((Predicate<ConceptCardMaterialData>) (p => (string) p.IName == param.iname));
            if (cardMaterialData != null)
              cardMaterialData.Num = (OInt) concept_card_materials[index].num;
          }
          if ((int) cardMaterialData.Num == 0)
          {
            if (param.type == eCardType.Enhance_exp)
              this.mConceptCardExpMaterials.Remove(cardMaterialData);
            else if (param.type == eCardType.Enhance_trust)
              this.mConceptCardTrustMaterials.Remove(cardMaterialData);
          }
        }
      }
    }

    public void SetConceptCardNum(string iname, int value)
    {
      if (this.mConceptCardNum.ContainsKey(iname))
        this.mConceptCardNum[iname] = value;
      else
        this.mConceptCardNum.Add(iname, value);
    }

    public void OnDirtyConceptCardData()
    {
      GlobalVars.IsDirtyConceptCardData.Set(true);
      GlobalVars.IsDirtySkinConceptCardData.Set(true);
    }

    public void OnDirtyRuneData() => GlobalVars.IsDirtyRuneData.Set(true);

    public bool IsHaveHealAPItems()
    {
      bool flag = false;
      List<ItemData> list = this.Items.Where<ItemData>((Func<ItemData, bool>) (x => x.ItemType == EItemType.ApHeal)).ToList<ItemData>();
      if (list != null)
      {
        for (int index = 0; index < list.Count; ++index)
        {
          if (list[index].Num > 0)
          {
            flag = true;
            break;
          }
        }
      }
      return flag;
    }

    public bool IsHaveConceptCardExpMaterial()
    {
      if (this.mConceptCardExpMaterials == null || this.mConceptCardExpMaterials.Count == 0)
        return false;
      bool flag = false;
      List<ConceptCardMaterialData> all = this.mConceptCardExpMaterials.FindAll((Predicate<ConceptCardMaterialData>) (p => (int) p.Num > 0));
      if (all != null && all.Count > 0)
        flag = true;
      return flag;
    }

    public bool IsHaveConceptCardTrustMaterial()
    {
      if (this.mConceptCardTrustMaterials == null || this.mConceptCardTrustMaterials.Count == 0)
        return false;
      bool flag = false;
      List<ConceptCardMaterialData> all = this.mConceptCardTrustMaterials.FindAll((Predicate<ConceptCardMaterialData>) (p => (int) p.Num > 0));
      if (all != null && all.Count > 0)
        flag = true;
      return flag;
    }

    public void UpdateConceptCardEquipedSlots(Json_Unit[] units)
    {
      if (units == null)
        return;
      for (int index = 0; index < units.Length; ++index)
      {
        if (units[index] != null && units[index].iid != 0L)
        {
          UnitData unitDataByUniqueId = this.FindUnitDataByUniqueID(units[index].iid);
          if (unitDataByUniqueId != null)
          {
            for (int slotIndex = 0; slotIndex < unitDataByUniqueId.ConceptCards.Length; ++slotIndex)
            {
              if (unitDataByUniqueId.ConceptCards[slotIndex] != null && (long) unitDataByUniqueId.ConceptCards[slotIndex].UniqueID != 0L)
                this.FindConceptCardByUniqueID((long) unitDataByUniqueId.ConceptCards[slotIndex].UniqueID)?.SetSlotIndex(slotIndex);
            }
          }
        }
      }
    }

    public void UpdateConceptCardEquipedSlotsAllUnit()
    {
      if (this.mUnits == null)
        return;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        UnitData mUnit = this.mUnits[index];
        if (mUnit != null)
        {
          for (int slotIndex = 0; slotIndex < mUnit.ConceptCards.Length; ++slotIndex)
          {
            if (mUnit.ConceptCards[slotIndex] != null && (long) mUnit.ConceptCards[slotIndex].UniqueID != 0L)
              this.FindConceptCardByUniqueID((long) mUnit.ConceptCards[slotIndex].UniqueID)?.SetSlotIndex(slotIndex);
          }
        }
      }
    }

    public void SetTowerFloorResetCoin(ReqTowerFloorReset.Json_Response result)
    {
      if (result == null)
        return;
      this.mFreeCoin = (OInt) result.coin.free;
      this.mPaidCoin = (OInt) result.coin.paid;
      this.mComCoin = (OInt) result.coin.com;
    }

    public bool SetPremiumLoginBonus(Json_LoginBonusTable loginbonus)
    {
      if (loginbonus == null || string.IsNullOrEmpty(loginbonus.type))
        return true;
      List<Dictionary<string, object>> bonusTableFromJson = this.CreateLoginBonusTableFromJson();
      if (bonusTableFromJson == null || bonusTableFromJson.Count < 1)
        return true;
      Json_LoginBonusTable loginBonusTable = this.CreateLoginBonusTable(bonusTableFromJson, loginbonus);
      if (loginBonusTable != null)
      {
        this.mLoginBonusTables[loginBonusTable.type] = loginBonusTable;
        this.mPremiumLoginBonus = loginBonusTable;
      }
      return true;
    }

    public void SetAutoRepeatQuestBox(int box_add_count)
    {
      this.mAutoRepeatQuestBox.Setup(box_add_count);
    }

    public bool IsAutoRepeatQuestBoxSizeLimit()
    {
      return MonoSingleton<GameManager>.Instance.MasterParam.AutoRepeatQuestBoxParams == null || this.AutoRepeatQuestBox.AddCount + 1 >= MonoSingleton<GameManager>.Instance.MasterParam.AutoRepeatQuestBoxParams.Length;
    }

    public void SetAutoRepeatQuestApItemPriority(string[] ap_items)
    {
      if (ap_items == null || ap_items.Length <= 0)
        return;
      this.mAutoRepeatQuestApItemPriority.Clear();
      for (int index = 0; index < ap_items.Length; ++index)
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(ap_items[index]);
        if (itemParam != null && itemParam.type == EItemType.ApHeal)
          this.mAutoRepeatQuestApItemPriority.Add(itemParam.iname);
      }
    }

    public void SetAutoRepeatQuestBoxExpansion(int box_add_count)
    {
      this.mAutoRepeatQuestBox.SetupExpansion(box_add_count);
    }

    public Dictionary<long, RuneData> Runes => this.mRunes;

    public RuneData FindRuneByUniqueID(long iid)
    {
      RuneData runeByUniqueId;
      this.mRunes.TryGetValue(iid, out runeByUniqueId);
      return runeByUniqueId;
    }

    public List<RuneEnforceGaugeData> GetRuneEnforceGauge() => this.mRuneEnforceGauge;

    public short GetRuneStrage() => this.mRuneStorage;

    public bool FindRuneOwner(RuneData rune, out UnitData unit)
    {
      unit = (UnitData) null;
      if (rune == null)
        return false;
      for (int index1 = 0; index1 < this.mUnits.Count; ++index1)
      {
        if (this.mUnits[index1].EquipRunes != null)
        {
          for (int index2 = 0; index2 < this.mUnits[index1].EquipRunes.Length; ++index2)
          {
            if (this.mUnits[index1].EquipRunes[index2] != null && (long) this.mUnits[index1].EquipRunes[index2].UniqueID == (long) rune.UniqueID)
            {
              unit = this.mUnits[index1];
              return true;
            }
          }
        }
      }
      return false;
    }

    public bool RemoveRunes(long[] unique_id)
    {
      bool flag = true;
      foreach (long key in unique_id)
      {
        if (this.mRunes.ContainsKey(key))
          this.mRunes.Remove(key);
        else
          flag = false;
      }
      return flag;
    }

    public bool IsExistReceivableCoinBuyUseBonus(
      eCoinBuyUseBonusTrigger trigger,
      eCoinBuyUseBonusType type)
    {
      CoinBuyUseBonusParam buyUseBonusParam = MonoSingleton<GameManager>.Instance.MasterParam.GetActiveCoinBuyUseBonusParam(trigger, type);
      if (buyUseBonusParam != null)
      {
        for (int index = 0; index < buyUseBonusParam.RewardSet.Contents.Length; ++index)
        {
          if (this.IsReceivableCoinBuyUseBonus(buyUseBonusParam.Iname, buyUseBonusParam.RewardSet.Contents[index].Num))
            return true;
        }
      }
      return false;
    }

    public int GetCoinBuyUseBonusProgress(string bonus_iname)
    {
      PlayerCoinBuyUseBonusState buyUseBonusState = this.mCoinBuyUseBonusStateList.Find((Predicate<PlayerCoinBuyUseBonusState>) (s => s.iname == bonus_iname));
      return buyUseBonusState == null ? 0 : buyUseBonusState.total;
    }

    public bool IsAchievedCoinBuyUseBonus(string bonus_iname, int coin_num)
    {
      PlayerCoinBuyUseBonusState buyUseBonusState = this.mCoinBuyUseBonusStateList.Find((Predicate<PlayerCoinBuyUseBonusState>) (state => state.iname == bonus_iname));
      return buyUseBonusState != null && buyUseBonusState.total >= coin_num;
    }

    public bool IsReceivableCoinBuyUseBonus(string bonus_iname, int coin_num)
    {
      if (!this.IsAchievedCoinBuyUseBonus(bonus_iname, coin_num))
        return false;
      PlayerCoinBuyUseBonusRewardState bonusRewardState = MonoSingleton<GameManager>.Instance.Player.CoinBuyUseBonusRewardStateList.Find((Predicate<PlayerCoinBuyUseBonusRewardState>) (state => state.iname == bonus_iname && state.num == coin_num));
      return bonusRewardState == null || !bonusRewardState.IsReceived;
    }

    public bool IsReceivedCoinBuyUseBonus(string bonus_iname, int coin_num)
    {
      if (!this.IsAchievedCoinBuyUseBonus(bonus_iname, coin_num))
        return false;
      PlayerCoinBuyUseBonusRewardState bonusRewardState = MonoSingleton<GameManager>.Instance.Player.CoinBuyUseBonusRewardStateList.Find((Predicate<PlayerCoinBuyUseBonusRewardState>) (state => state.iname == bonus_iname && state.num == coin_num));
      return bonusRewardState != null && bonusRewardState.IsReceived;
    }

    public PlayerCoinBuyUseBonusRewardState GetCoinBuyUseBonusRewardState(
      string bonus_iname,
      int coin_num)
    {
      return MonoSingleton<GameManager>.Instance.Player.CoinBuyUseBonusRewardStateList.Find((Predicate<PlayerCoinBuyUseBonusRewardState>) (state => state.iname == bonus_iname && state.num == coin_num)) ?? (PlayerCoinBuyUseBonusRewardState) null;
    }

    public PlayerData.TrophyStarMission TrophyStarMissionInfo
    {
      get => this.mTrophyStarMissionInfo;
      set => this.mTrophyStarMissionInfo = value;
    }

    public bool IsCanGetRewardTrophyStarMission()
    {
      if (this.mTrophyStarMissionInfo == null)
        return false;
      bool trophyStarMission = false;
      if (this.mTrophyStarMissionInfo.Daily != null && this.mTrophyStarMissionInfo.Daily.TsmParam != null)
      {
        PlayerData.TrophyStarMission.StarMission daily = this.mTrophyStarMissionInfo.Daily;
        int starNum = daily.StarNum;
        for (int index = 0; index < daily.TsmParam.StarSetList.Count; ++index)
        {
          int requireStar = (int) daily.TsmParam.StarSetList[index].RequireStar;
          if ((daily.Rewards == null || index >= daily.Rewards.Length || daily.Rewards[index] == 0) && starNum >= requireStar)
          {
            trophyStarMission = true;
            break;
          }
        }
      }
      if (!trophyStarMission && this.mTrophyStarMissionInfo.Weekly != null && this.mTrophyStarMissionInfo.Weekly.TsmParam != null)
      {
        PlayerData.TrophyStarMission.StarMission weekly = this.mTrophyStarMissionInfo.Weekly;
        int starNum = weekly.StarNum;
        for (int index = 0; index < weekly.TsmParam.StarSetList.Count; ++index)
        {
          int requireStar = (int) weekly.TsmParam.StarSetList[index].RequireStar;
          if ((weekly.Rewards == null || index >= weekly.Rewards.Length || weekly.Rewards[index] == 0) && starNum >= requireStar)
          {
            trophyStarMission = true;
            break;
          }
        }
      }
      return trophyStarMission;
    }

    public ExpansionPurchaseData[] GetExpansionDatas(ExpansionPurchaseParam.eExpansionType type)
    {
      return this.GetEnableExpansionDatas(type, false);
    }

    public ExpansionPurchaseData[] GetEnableExpansionDatas(
      ExpansionPurchaseParam.eExpansionType type,
      bool is_enable_check = true)
    {
      List<ExpansionPurchaseData> expansionPurchaseDataList = new List<ExpansionPurchaseData>();
      if (this.mExpansions == null)
        return expansionPurchaseDataList.ToArray();
      for (int index = 0; index < this.mExpansions.Count; ++index)
      {
        ExpansionPurchaseData mExpansion = this.mExpansions[index];
        if (mExpansion != null && mExpansion.GetExpansionType() == type && (!is_enable_check || mExpansion.expired_at > Network.GetServerTime()))
          expansionPurchaseDataList.Add(mExpansion);
      }
      return expansionPurchaseDataList.ToArray();
    }

    public ExpansionPurchaseData[] GetEffectiveData(ExpansionPurchaseParam.eExpansionType _type)
    {
      List<ExpansionPurchaseData> expansionPurchaseDataList = new List<ExpansionPurchaseData>();
      foreach (ExpansionPurchaseData enableExpansionData in this.GetEnableExpansionDatas(_type))
      {
        ExpansionPurchaseData data = enableExpansionData;
        if (data.param.ExpansionType == _type)
        {
          ExpansionPurchaseData expansionPurchaseData = expansionPurchaseDataList.Find((Predicate<ExpansionPurchaseData>) (x => x.param.Group == data.param.Group));
          if (expansionPurchaseData == null)
            expansionPurchaseDataList.Add(data);
          else if (expansionPurchaseData != null && data.expired_at < expansionPurchaseData.expired_at)
          {
            expansionPurchaseDataList.Remove(expansionPurchaseData);
            expansionPurchaseDataList.Add(data);
          }
        }
      }
      return expansionPurchaseDataList.ToArray();
    }

    public int GetChallengeLimitCount(
      ExpansionPurchaseParam.eExpansionType _type,
      string _iname,
      int _challenge_limit_count)
    {
      if (_challenge_limit_count <= 0)
        return 0;
      int num = 0;
      foreach (ExpansionPurchaseData expansionPurchaseData in this.GetEffectiveData(_type))
      {
        switch (_type)
        {
          case ExpansionPurchaseParam.eExpansionType.ExtraCount:
            num += expansionPurchaseData.param.Value;
            break;
          case ExpansionPurchaseParam.eExpansionType.ChallengeCount:
            if (!string.IsNullOrEmpty(_iname) && MonoSingleton<GameManager>.Instance.MasterParam.ExpansionPurchaseQuestParams[expansionPurchaseData.iname].Contains(_iname))
            {
              num += expansionPurchaseData.param.Value;
              break;
            }
            break;
        }
      }
      return _challenge_limit_count + num;
    }

    public long GetExpansionGroupExpiredAt(BuyCoinProductParam _param)
    {
      List<ExpansionPurchaseParam> all1 = MonoSingleton<GameManager>.Instance.MasterParam.ExpansionPurchaseParams.FindAll((Predicate<ExpansionPurchaseParam>) (x => x.BuyCoinProduct == _param.Iname));
      if (all1 == null)
        return 0;
      long expansionGroupExpiredAt = 0;
      foreach (ExpansionPurchaseParam expansionPurchaseParam in all1)
      {
        ExpansionPurchaseParam param = expansionPurchaseParam;
        List<ExpansionPurchaseData> all2 = this.mExpansions.FindAll((Predicate<ExpansionPurchaseData>) (x => x.param != null && x.param.Group == param.Group));
        if (all2 != null && all2.Count != 0)
        {
          foreach (ExpansionPurchaseData expansionPurchaseData in all2)
          {
            if (expansionGroupExpiredAt == 0L || expansionGroupExpiredAt < expansionPurchaseData.expired_at)
              expansionGroupExpiredAt = expansionPurchaseData.expired_at;
          }
        }
      }
      return expansionGroupExpiredAt;
    }

    public bool IsEnableExpansionPurchaseForValue(
      int value,
      ExpansionPurchaseParam.eExpansionType type)
    {
      List<ExpansionPurchaseData> expansionPurchaseDataList = new List<ExpansionPurchaseData>((IEnumerable<ExpansionPurchaseData>) this.GetEnableExpansionDatas(type));
      return expansionPurchaseDataList != null && expansionPurchaseDataList.Count > 0 && expansionPurchaseDataList.FindIndex((Predicate<ExpansionPurchaseData>) (p => p.param.Value == value)) != -1;
    }

    public void LoginReset() => this.mTrophyStarMissionInfo = (PlayerData.TrophyStarMission) null;

    public int GetAllUnitsTotalStrength()
    {
      int unitsTotalStrength = 0;
      if (this.mUnits == null)
        return 0;
      for (int index = 0; index < this.mUnits.Count; ++index)
        unitsTotalStrength += this.mUnits[index].CalcTotalParameter();
      return unitsTotalStrength;
    }

    public void UpdateTotalCombatPower()
    {
      int totalCombatPower = this.combatPowerData.TotalCombatPower;
      this.combatPowerData.UpdateCombatPower(this.mUnits.Where<UnitData>((Func<UnitData, bool>) (unit => unit != null && !unit.IsRental)));
    }

    public void ClearTotalCombatPowerRequestFlag() => this.combatPowerData.ClearChangeFlag();

    public void UpdateCombatPowerTrophy()
    {
      Action<TrophyObjective[], int> action = (Action<TrophyObjective[], int>) ((trophyList, combatPower) =>
      {
        for (int index = trophyList.Length - 1; index >= 0; --index)
        {
          if (trophyList[index].ival <= combatPower)
            this.mTrophyData.AddTrophyCounter(trophyList[index], 1);
        }
      });
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.over_total_combat_power);
      action(trophiesOfType1, this.combatPowerData.TotalCombatPower);
      TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.over_fire_combat_power);
      action(trophiesOfType2, this.combatPowerData.TotalFireCombatPower);
      TrophyObjective[] trophiesOfType3 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.over_water_combat_power);
      action(trophiesOfType3, this.combatPowerData.TotalWaterCombatPower);
      TrophyObjective[] trophiesOfType4 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.over_thunder_combat_power);
      action(trophiesOfType4, this.combatPowerData.TotalThunderCombatPower);
      TrophyObjective[] trophiesOfType5 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.over_wind_combat_power);
      action(trophiesOfType5, this.combatPowerData.TotalWindCombatPower);
      TrophyObjective[] trophiesOfType6 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.over_shine_combat_power);
      action(trophiesOfType6, this.combatPowerData.TotalShineCombatPower);
      TrophyObjective[] trophiesOfType7 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.over_dark_combat_power);
      action(trophiesOfType7, this.combatPowerData.TotalDarkCombatPower);
      TrophyObjective[] trophiesOfType8 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.over_unit_combat_power);
      action(trophiesOfType8, this.combatPowerData.HighestCombatPower);
    }

    [Flags]
    public enum EDeserializeFlags
    {
      None = 0,
      Gold = 1,
      Coin = 2,
      Stamina = 4,
      Cave = 8,
      AbilityUp = 16, // 0x00000010
      Arena = 32, // 0x00000020
      Tour = 64, // 0x00000040
    }

    public class Json_InitData
    {
      public PlayerData.Json_InitUnits[] units;
      public PlayerData.Json_InitItems[] items;
      public PlayerData.Json_InitParty[] party;
      public PlayerData.Json_InitUnits[] friends;
    }

    public class Json_FriendData
    {
      public Json_Unit[] friends;
    }

    public class Json_InitUnits
    {
      public string iname;
      public int exp;
      public string[] skills;
    }

    public class Json_InitItems
    {
      public string iname;
      public int num;
    }

    public class Json_InitParty
    {
      public PlayerData.Json_InitPartyUnit[] units;
    }

    public class Json_InitPartyUnit
    {
      public int iid;
      public int leader;
    }

    public class TrophyStarMission
    {
      public PlayerData.TrophyStarMission.StarMission Daily;
      public PlayerData.TrophyStarMission.StarMission Weekly;

      public void Update(ReqTrophyStarMission.StarMission star_mission)
      {
        if (star_mission == null)
          return;
        if (star_mission.daily != null)
        {
          if (this.Daily == null)
            this.Daily = new PlayerData.TrophyStarMission.StarMission();
          this.Daily.Update(star_mission.daily);
        }
        if (star_mission.weekly == null)
          return;
        if (this.Weekly == null)
          this.Weekly = new PlayerData.TrophyStarMission.StarMission();
        this.Weekly.Update(star_mission.weekly);
      }

      public class StarMission
      {
        public TrophyStarMissionParam TsmParam;
        public int YyMmDd;
        public int StarNum;
        public int[] Rewards;

        public void SetRewards(int[] rewards)
        {
          if (rewards == null)
            return;
          this.Rewards = new int[rewards.Length];
          if (rewards.Length == 0)
            return;
          rewards.CopyTo((Array) this.Rewards, 0);
        }

        public void Update(ReqTrophyStarMission.StarMission.Info info)
        {
          if (info == null)
            return;
          TrophyStarMissionParam starMissionParam = TrophyStarMissionParam.GetParam(info.iname);
          if (starMissionParam != null)
            this.TsmParam = starMissionParam;
          this.YyMmDd = info.ymd;
          this.StarNum = info.star_num;
          this.SetRewards(info.rewards);
        }
      }
    }
  }
}
