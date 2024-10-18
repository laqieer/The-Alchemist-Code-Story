﻿// Decompiled with JetBrains decompiler
// Type: SRPG.PlayerData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using Gsc.Purchase;
using SRPG.JsonUtlity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace SRPG
{
  public class PlayerData
  {
    public static readonly int INI_UNIT_CAPACITY = 20;
    public static readonly int MAX_UNIT_CAPACITY = 50;
    private static readonly string PLAYRE_DATA_VERSION = "38.0";
    public static readonly string TEAM_ID_KEY = "TeamID";
    public static readonly string MULTI_PLAY_TEAM_ID_KEY = "MultiPlayTeamID";
    public static readonly string ARENA_TEAM_ID_KEY = "ArenaTeamID";
    public static readonly string ROOM_COMMENT_KEY = "MultiPlayRoomComment";
    public static readonly string VERSUS_ID_KEY = "VERSUS_PLACEMENT_";
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
    private Dictionary<string, Json_LoginBonusTable> mLoginBonusTables = new Dictionary<string, Json_LoginBonusTable>();
    private OInt mChallengeMultiNum = (OInt) 0;
    private OInt mStaminaBuyNum = (OInt) 0;
    private OInt mGoldBuyNum = (OInt) 0;
    private OInt mChallengeArenaNum = (OInt) 0;
    private TimeRecoveryValue mChallengeArenaTimer = new TimeRecoveryValue();
    private OInt mTourNum = (OInt) 0;
    private OInt mUnitCap = (OInt) PlayerData.INI_UNIT_CAPACITY;
    private DateTime mArenaLastAt = GameUtility.UnixtimeToLocalTime(0L);
    private DateTime mArenaEndAt = GameUtility.UnixtimeToLocalTime(0L);
    private List<ArtifactData> mArtifacts = new List<ArtifactData>();
    private Dictionary<string, Dictionary<int, int>> mArtifactsNumByRarity = new Dictionary<string, Dictionary<int, int>>();
    private List<ConceptCardData> mConceptCards = new List<ConceptCardData>();
    private Dictionary<string, int> mConceptCardNum = new Dictionary<string, int>();
    private List<SkinConceptCardData> mSkinConceptCards = new List<SkinConceptCardData>();
    private List<ConceptCardMaterialData> mConceptCardExpMaterials = new List<ConceptCardMaterialData>();
    private List<ConceptCardMaterialData> mConceptCardTrustMaterials = new List<ConceptCardMaterialData>();
    private List<string> mSkins = new List<string>();
    private Dictionary<long, UnitData> mUniqueID2UnitData = new Dictionary<long, UnitData>();
    private Dictionary<string, ItemData> mID2ItemData = new Dictionary<string, ItemData>();
    private List<TrophyState> mTrophyStates = new List<TrophyState>(32);
    private Dictionary<string, List<TrophyState>> mTrophyStatesInameDict = new Dictionary<string, List<TrophyState>>();
    private ShopData[] mShops = new ShopData[Enum.GetNames(typeof (EShopType)).Length];
    private LimitedShopData mLimitedShops = new LimitedShopData();
    private EventShopData mEventShops = new EventShopData();
    private List<SRPG.RankMatchMissionState> mRankMatchMissionState = new List<SRPG.RankMatchMissionState>();
    public RankMatchSeasonResult mRankMatchSeasonResult = new RankMatchSeasonResult();
    private List<QuestParam> mAvailableQuests = new List<QuestParam>();
    private bool mQuestListDirty = true;
    private List<OpenedQuestArchive> mQuestArchives = new List<OpenedQuestArchive>();
    public List<FriendData> Friends = new List<FriendData>();
    public List<FriendData> FriendsFollower = new List<FriendData>();
    public List<FriendData> FriendsFollow = new List<FriendData>();
    public List<string> mFollowerUID = new List<string>();
    public List<MultiFuid> MultiFuids = new List<MultiFuid>();
    public List<SupportData> Supports = new List<SupportData>();
    public FriendPresentWishList FriendPresentWishList = new FriendPresentWishList();
    public FriendPresentReceiveList FriendPresentReceiveList = new FriendPresentReceiveList();
    public List<MailData> Mails = new List<MailData>();
    public List<MailData> CurrentMails = new List<MailData>();
    public OInt mUnlocks = (OInt) 0;
    public FreeGacha FreeGachaGold = new FreeGacha();
    public FreeGacha FreeGachaCoin = new FreeGacha();
    public PaidGacha PaidGacha = new PaidGacha();
    public Dictionary<string, PaymentInfo> PaymentInfos = new Dictionary<string, PaymentInfo>();
    private List<string> mHaveAward = new List<string>();
    private int[] mVersusWinCount = new int[4];
    private int[] mVersusTotalCount = new int[4];
    private int mFirstChargeStatus = -1;
    private ItemData[] mInventory = new ItemData[5];
    private long mMissionClearAt = -1;
    private Dictionary<string, int> mConsumeMaterials = new Dictionary<string, int>(16);
    private int mPrevCheckHour = -1;
    private UpdateTrophyInterval mUpdateInterval = new UpdateTrophyInterval();
    private Queue<string> mLoginBonusQueue = new Queue<string>();
    public const string SAIGONI_HOME_HIRAITA_LV = "lastplv";
    public const string SAIGONI_HOME_HIRAITA_VIPLV = "lastviplv";
    public const int INVENTORY_SIZE = 5;
    private string mName;
    private string mCuid;
    private string mFuid;
    private string mTuid;
    private long mTuidExpiredAt;
    private int mLoginCount;
    public int mArenaResetCount;
    public DateTime LoginDate;
    public long TutorialFlags;
    private Json_LoginBonus[] mLoginBonus;
    private int mLoginBonusCount;
    private bool mFirstLogin;
    private Json_LoginBonusTable mLoginBonus28days;
    private Json_LoginBonusTable mPremiumLoginBonus;
    private int mArenaRank;
    private int mBestArenaRank;
    private int mArenaSeed;
    private int mArenaMaxActionNum;
    private List<UnitData> mUnits;
    private List<ItemData> mItems;
    private List<PartyData> mPartys;
    private PlayerGuildData mPlayerGuild;
    private GuildData mGuild;
    public bool IsTodaysNextArchiveOpenFree;
    public int mFriendNum;
    public int mFollowerNum;
    public MailPageData MailPage;
    private bool mUnreadMailPeriod;
    private bool mUnreadMail;
    private bool mValidGpsGift;
    private bool mValidFriendPresent;
    private string mSelectedAward;
    private int mVersusPoint;
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
    private long mGuerrillaShopStart;
    private long mGuerrillaShopEnd;
    private bool mIsGuerrillaShopStarted;
    private const string CHANGE_SCRIPT_COMPLETE_QUEST_KEY = "COMPLETE_QUEST_MISSION";
    public int SupportCount;
    public int SupportGold;
    private int mCreateItemCost;
    private const int MAX_JOB = 6;

    public PlayerData()
    {
      this.LoginDate = DateTime.Now;
      this.mPartys = new List<PartyData>(12);
      for (int index = 0; index < 12; ++index)
        this.mPartys.Add(new PartyData((PlayerPartyTypes) index)
        {
          Name = "パーティ" + (object) (index + 1),
          PartyType = (PlayerPartyTypes) index
        });
    }

    public List<SRPG.RankMatchMissionState> RankMatchMissionState
    {
      get
      {
        return this.mRankMatchMissionState;
      }
      set
      {
        this.mRankMatchMissionState = value;
      }
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
      bool flag1 = questparam.IsDateUnlock(-1L);
      bool flag2 = Array.Find<QuestParam>(this.AvailableQuests, (Predicate<QuestParam>) (p => p == questparam)) != null;
      if (flag1)
        return flag2;
      return false;
    }

    public bool IsQuestCleared(string questID)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(questID);
      if (quest != null)
        return quest.state == QuestStates.Cleared;
      return false;
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

    public List<OpenedQuestArchive> OpenedQuestArchives
    {
      get
      {
        return this.mQuestArchives;
      }
    }

    public string OkyakusamaCode
    {
      get
      {
        return this.mCuid;
      }
    }

    public BtlResultTypes RankMatchResult { get; set; }

    public int RankMatchTotalCount
    {
      get
      {
        return this.RankMatchWinCount + this.RankMatchLoseCount;
      }
    }

    public int RankMatchWinCount { get; set; }

    public int RankMatchLoseCount { get; set; }

    public string Name
    {
      get
      {
        return this.mName;
      }
      set
      {
        this.mName = value;
      }
    }

    public string CUID
    {
      get
      {
        return this.mCuid;
      }
    }

    public string FUID
    {
      get
      {
        return this.mFuid;
      }
    }

    public string TUID
    {
      get
      {
        return this.mTuid;
      }
    }

    public long TuidExpiredAt
    {
      get
      {
        return this.mTuidExpiredAt;
      }
    }

    public int LoginCount
    {
      get
      {
        return this.mLoginCount;
      }
    }

    public int Lv
    {
      get
      {
        return (int) this.mLv;
      }
    }

    public int Exp
    {
      get
      {
        return (int) this.mExp;
      }
    }

    public int Gold
    {
      get
      {
        return (int) this.mGold;
      }
    }

    public int Coin
    {
      get
      {
        return (int) this.mFreeCoin + (int) this.mPaidCoin + (int) this.mComCoin;
      }
    }

    public int FreeCoin
    {
      get
      {
        return (int) this.mFreeCoin;
      }
    }

    public int PaidCoin
    {
      get
      {
        return (int) this.mPaidCoin;
      }
    }

    public int ComCoin
    {
      get
      {
        return (int) this.mComCoin;
      }
    }

    public int TourCoin
    {
      get
      {
        return (int) this.mTourCoin;
      }
    }

    public int ArenaCoin
    {
      get
      {
        return (int) this.mArenaCoin;
      }
    }

    public int MultiCoin
    {
      get
      {
        return (int) this.mMultiCoin;
      }
    }

    public int AbilityPoint
    {
      get
      {
        return (int) this.mAbilityPoint;
      }
    }

    public int PiecePoint
    {
      get
      {
        return (int) this.mPiecePoint;
      }
    }

    public int VipRank
    {
      get
      {
        return (int) this.mVipRank;
      }
    }

    public int VipPoint
    {
      get
      {
        return (int) this.mVipPoint;
      }
    }

    public List<EventCoinData> EventCoinList
    {
      get
      {
        return this.mEventCoinList;
      }
    }

    public int Stamina
    {
      get
      {
        return (int) this.mStamina.val;
      }
    }

    public int StaminaMax
    {
      get
      {
        return (int) this.mStamina.valMax;
      }
    }

    public long StaminaRecverySec
    {
      get
      {
        return (long) this.mStamina.interval;
      }
    }

    public long StaminaAt
    {
      get
      {
        return (long) this.mStamina.at;
      }
    }

    public int StaminaStockCap
    {
      get
      {
        return (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StaminaStockCap;
      }
    }

    public int CaveStamina
    {
      get
      {
        return (int) this.mCaveStamina.val;
      }
    }

    public int CaveStaminaMax
    {
      get
      {
        return (int) this.mCaveStamina.valMax;
      }
    }

    public long CaveStaminaRecverySec
    {
      get
      {
        return (long) this.mCaveStamina.interval;
      }
    }

    public long CaveStaminaAt
    {
      get
      {
        return (long) this.mCaveStamina.at;
      }
    }

    public int CaveStaminaStockCap
    {
      get
      {
        return (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CaveStaminaStockCap;
      }
    }

    public int AbilityRankUpCountNum
    {
      get
      {
        return (int) this.mAbilityRankUpCount.val;
      }
    }

    public int AbilityRankUpCountMax
    {
      get
      {
        return (int) this.mAbilityRankUpCount.valMax;
      }
    }

    public long AbilityRankUpCountRecverySec
    {
      get
      {
        return (long) this.mAbilityRankUpCount.interval;
      }
    }

    public long AbilityRankUpCountAt
    {
      get
      {
        return (long) this.mAbilityRankUpCount.at;
      }
    }

    public int ChallengeArenaNum
    {
      get
      {
        return (int) this.mChallengeArenaNum;
      }
    }

    public int ChallengeArenaMax
    {
      get
      {
        return (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeArenaMax;
      }
    }

    public long ChallengeArenaCoolDownSec
    {
      get
      {
        return (long) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeArenaCoolDownSec;
      }
    }

    public long ChallengeArenaAt
    {
      get
      {
        return (long) this.mChallengeArenaTimer.at;
      }
    }

    public int ChallengeTourNum
    {
      get
      {
        return (int) this.mTourNum;
      }
    }

    public int ChallengeTourMax
    {
      get
      {
        return (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeTourMax;
      }
    }

    public int ArenaRank
    {
      get
      {
        return this.mArenaRank;
      }
    }

    public int ArenaRankBest
    {
      get
      {
        return this.mBestArenaRank;
      }
    }

    public DateTime ArenaLastAt
    {
      get
      {
        return this.mArenaLastAt;
      }
    }

    public int ArenaSeed
    {
      get
      {
        return this.mArenaSeed;
      }
    }

    public int ArenaMaxActionNum
    {
      get
      {
        return this.mArenaMaxActionNum;
      }
    }

    public DateTime ArenaEndAt
    {
      get
      {
        return this.mArenaEndAt;
      }
    }

    public int ChallengeMultiNum
    {
      get
      {
        return (int) this.mChallengeMultiNum;
      }
    }

    public void IncrementChallengeMultiNum()
    {
      ++this.mChallengeMultiNum;
    }

    public int ChallengeMultiMax
    {
      get
      {
        return (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeMultiMax;
      }
    }

    public int StaminaBuyNum
    {
      get
      {
        return (int) this.mStaminaBuyNum;
      }
    }

    public int GoldBuyNum
    {
      get
      {
        return (int) this.mGoldBuyNum;
      }
    }

    public int UnitCap
    {
      get
      {
        return (int) this.mUnitCap;
      }
    }

    public List<UnitData> Units
    {
      get
      {
        return this.mUnits;
      }
    }

    public int UnitNum
    {
      get
      {
        if (this.mUnits != null)
          return this.mUnits.Count;
        return 0;
      }
    }

    public List<ItemData> Items
    {
      get
      {
        return this.mItems;
      }
    }

    public List<ArtifactData> Artifacts
    {
      get
      {
        return this.mArtifacts;
      }
    }

    public int ArtifactNum
    {
      get
      {
        return this.mArtifacts.Count;
      }
    }

    public int ArtifactCap
    {
      get
      {
        return (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ArtifactBoxCap;
      }
    }

    public int GetArtifactNumByRarity(string iname, int rarity)
    {
      Dictionary<int, int> dictionary;
      int num;
      if (!string.IsNullOrEmpty(iname) && this.mArtifactsNumByRarity.TryGetValue(iname, out dictionary) && dictionary.TryGetValue(rarity, out num))
        return num;
      return 0;
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

    public List<string> Skins
    {
      get
      {
        return this.mSkins;
      }
    }

    public int FriendCap
    {
      get
      {
        return (int) MonoSingleton<GameManager>.Instance.MasterParam.GetPlayerParam(this.Lv).fcap;
      }
    }

    public int FriendNum
    {
      get
      {
        return this.mFriendNum;
      }
      set
      {
        this.mFriendNum = value;
      }
    }

    public int FollowerNum
    {
      get
      {
        return this.mFollowerNum;
      }
      set
      {
        this.mFollowerNum = value;
      }
    }

    public List<string> FollowerUID
    {
      get
      {
        return this.mFollowerUID;
      }
    }

    public bool IsRequestFriend()
    {
      return this.FriendNum < this.FriendCap;
    }

    public List<PartyData> Partys
    {
      get
      {
        return this.mPartys;
      }
    }

    public ItemData[] Inventory
    {
      get
      {
        return this.mInventory.Clone() as ItemData[];
      }
    }

    public void SetMissionClearAt(long unixTimeStamp)
    {
      this.mMissionClearAt = unixTimeStamp;
    }

    public void ResetMissionClearAt()
    {
      this.mMissionClearAt = -1L;
    }

    public DateTime GetMissionClearAt()
    {
      if (this.mMissionClearAt < 0L)
        return TimeManager.ServerTime;
      return TimeManager.FromUnixTime(this.mMissionClearAt);
    }

    public int ConceptCardNum
    {
      get
      {
        return this.mConceptCards.Count;
      }
    }

    public int ConceptCardCap
    {
      get
      {
        return (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardMax;
      }
    }

    public bool UnreadMailPeriod
    {
      get
      {
        return this.mUnreadMailPeriod;
      }
      set
      {
        this.mUnreadMailPeriod = value;
      }
    }

    public bool UnreadMail
    {
      get
      {
        return this.mUnreadMail;
      }
    }

    public bool ValidGpsGift
    {
      set
      {
        this.mValidGpsGift = value;
      }
      get
      {
        return this.mValidGpsGift;
      }
    }

    public bool ValidFriendPresent
    {
      set
      {
        this.mValidFriendPresent = value;
      }
      get
      {
        return this.mValidFriendPresent;
      }
    }

    public string SelectedAward
    {
      get
      {
        return this.mSelectedAward;
      }
      set
      {
        this.mSelectedAward = value;
      }
    }

    public int VERSUS_POINT
    {
      get
      {
        return this.mVersusPoint;
      }
    }

    public int VersusFreeWinCnt
    {
      get
      {
        return this.mVersusWinCount[0];
      }
    }

    public int VersusTowerWinCnt
    {
      get
      {
        return this.mVersusWinCount[1];
      }
    }

    public int VersusFriendWinCnt
    {
      get
      {
        return this.mVersusWinCount[2];
      }
    }

    public int VersusFreeCnt
    {
      get
      {
        return this.mVersusTotalCount[0];
      }
    }

    public int VersusTowerCnt
    {
      get
      {
        return this.mVersusTotalCount[1];
      }
    }

    public int VersusFriendCnt
    {
      get
      {
        return this.mVersusTotalCount[2];
      }
    }

    public int VersusTowerFloor
    {
      get
      {
        return this.mVersusTwFloor;
      }
    }

    public int VersusTowerKey
    {
      get
      {
        return this.mVersusTwKey;
      }
    }

    public int VersusTowerWinBonus
    {
      get
      {
        return this.mVersusTwWinCnt;
      }
    }

    public bool VersusSeazonGiftReceipt
    {
      get
      {
        return this.mVersusSeasonGift;
      }
      set
      {
        this.mVersusSeasonGift = value;
      }
    }

    public int RankMatchRank
    {
      get
      {
        return this.mRankMatchRank;
      }
    }

    public int RankMatchScore
    {
      get
      {
        return this.mRankMatchScore;
      }
    }

    public RankMatchClass RankMatchClass
    {
      get
      {
        return this.mRankMatchClass;
      }
    }

    public int RankMatchBattlePoint
    {
      get
      {
        return this.mRankMatchBattlePoint;
      }
    }

    public int RankMatchStreakWin
    {
      get
      {
        return this.mRankMatchStreakWin;
      }
    }

    public int RankMatchOldRank
    {
      get
      {
        return this.mRankMatchOldRank;
      }
    }

    public int RankMatchOldScore
    {
      get
      {
        return this.mRankMatchOldScore;
      }
    }

    public RankMatchClass RankMatchOldClass
    {
      get
      {
        return this.mRankMatchOldClass;
      }
    }

    public bool MultiInvitaionFlag
    {
      get
      {
        return this.mMultiInvitaionFlag;
      }
    }

    public string MultiInvitaionComment
    {
      get
      {
        return this.mMultiInvitaionComment;
      }
    }

    public int FirstFriendCount
    {
      get
      {
        return this.mFirstFriendCount;
      }
      set
      {
        this.mFirstFriendCount = value;
      }
    }

    public int FirstChargeStatus
    {
      get
      {
        return this.mFirstChargeStatus;
      }
      set
      {
        this.mFirstChargeStatus = value;
      }
    }

    public long GuerrillaShopStart
    {
      get
      {
        return this.mGuerrillaShopStart;
      }
    }

    public long GuerrillaShopEnd
    {
      get
      {
        return this.mGuerrillaShopEnd;
      }
    }

    public bool IsGuerrillaShopStarted
    {
      get
      {
        return this.mIsGuerrillaShopStarted;
      }
      set
      {
        this.mIsGuerrillaShopStarted = value;
      }
    }

    public PlayerGuildData PlayerGuild
    {
      get
      {
        return this.mPlayerGuild;
      }
    }

    public GuildData Guild
    {
      get
      {
        return this.mGuild;
      }
    }

    public bool HasArenaReward { get; set; }

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
            this.mVipExpiredAt = (OLong) TimeManager.FromDateTime(TimeManager.FromUnixTime(serverTime + (long) ((int) fixParam.VipCardDate * 24 * 60 * 60)).Date + new TimeSpan(23, 59, 59));
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
          long serverTime = Network.GetServerTime();
          if (serverTime > (long) this.mPremiumExpiredAt)
          {
            this.mPremiumExpiredAt = (OLong) TimeManager.FromDateTime(TimeManager.FromUnixTime(serverTime + (long) (premiumDateSpan * 24 * 60 * 60)).Date + new TimeSpan(23, 59, 59));
          }
          else
          {
            PlayerData playerData = this;
            playerData.mPremiumExpiredAt = (OLong) ((long) playerData.mPremiumExpiredAt + (long) (premiumDateSpan * 24 * 60 * 60));
          }
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
      if (serverTime1 > (long) this.mPremiumExpiredAt)
      {
        this.mPremiumExpiredAt = (OLong) TimeManager.FromDateTime(TimeManager.FromUnixTime(serverTime1 + (long) (premiumDateSpan * 24 * 60 * 60)).Date + new TimeSpan(23, 59, 59));
      }
      else
      {
        PlayerData playerData = this;
        playerData.mPremiumExpiredAt = (OLong) ((long) playerData.mPremiumExpiredAt + (long) (premiumDateSpan * 24 * 60 * 60));
      }
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
      if (json == null)
        throw new InvalidJSONException();
      this.mName = json.name;
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
      if (json == null)
        throw new InvalidJSONException();
      this.mExp = (OInt) json.exp;
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
      UnitData unitData = this.Units.Find((Predicate<UnitData>) (ud =>
      {
        if (ud.ConceptCard != null)
          return (long) ud.ConceptCard.UniqueID == concept_cards.iid;
        return false;
      }));
      if (unitData == null)
        return;
      unitData.ConceptCard = conceptCardByUniqueId;
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
        for (int i = 0; i < concept_cards.Length; ++i)
        {
          ConceptCardData conceptCardData = this.FindConceptCardByUniqueID(concept_cards[i].iid);
          if (conceptCardData == null)
          {
            try
            {
              conceptCardData = new ConceptCardData();
              conceptCardData.Deserialize(concept_cards[i]);
              this.mConceptCards.Add(conceptCardData);
            }
            catch (Exception ex)
            {
              DebugUtility.LogException(ex);
              continue;
            }
          }
          UnitData unitData = this.Units.Find((Predicate<UnitData>) (ud =>
          {
            if (ud.ConceptCard != null)
              return (long) ud.ConceptCard.UniqueID == concept_cards[i].iid;
            return false;
          }));
          if (unitData != null)
            unitData.ConceptCard = conceptCardData;
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
              UnitData unitData = this.Units.Find((Predicate<UnitData>) (ud =>
              {
                if (ud.ConceptCard != null)
                  return (long) ud.ConceptCard.UniqueID == (long) this.mConceptCards[i].UniqueID;
                return false;
              }));
              if (unitData != null)
                unitData.ConceptCard = (ConceptCardData) null;
              this.mConceptCards.RemoveAt(i);
            }
          }
        }
        this.UpdateConceptCardNum();
      }
    }

    public void Deserialize(JSON_ConceptCardMaterial[] concept_card_materials, bool is_data_override = true)
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
        int index2 = index1;
        if (!string.IsNullOrEmpty(parties[index1].ptype))
          index2 = (int) PartyData.GetPartyTypeFromString(parties[index1].ptype);
        this.mPartys[index2].Deserialize(parties[index1]);
        int lastSelectionIndex = 0;
        PartyWindow2.EditPartyTypes editPartyType = ((PlayerPartyTypes) index2).ToEditPartyType();
        if (PartyUtility.LoadTeamPresets(editPartyType, out lastSelectionIndex, false) == null)
        {
          int maxTeamCount = editPartyType.GetMaxTeamCount();
          List<PartyEditData> teams = new List<PartyEditData>();
          for (int index3 = 0; index3 < maxTeamCount; ++index3)
          {
            PartyEditData partyEditData = new PartyEditData(PartyUtility.CreateDefaultPartyNameFromIndex(index3), this.mPartys[index2]);
            teams.Add(partyEditData);
          }
          PartyUtility.SaveTeamPresets(editPartyType, 0, teams, false);
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
      Json_LoginBonusTable[] loginBonusData = this.CreateLoginBonusData(notify.logbotables);
      this.mFirstLogin = (notify.bonus >> 5 & 1) != 0;
      this.mLoginBonusCount = notify.bonus & 31;
      this.mLoginBonus = notify.logbonus;
      this.mLoginBonus28days = (Json_LoginBonusTable) null;
      this.mPremiumLoginBonus = (Json_LoginBonusTable) null;
      if (loginBonusData != null)
      {
        for (int index = 0; index < loginBonusData.Length; ++index)
        {
          if (loginBonusData[index] != null && !string.IsNullOrEmpty(loginBonusData[index].type))
          {
            this.mLoginBonusTables[loginBonusData[index].type] = loginBonusData[index];
            if (this.mFirstLogin)
              this.mLoginBonusQueue.Enqueue(loginBonusData[index].type);
            if (loginBonusData[index].bonus_units != null && loginBonusData[index].premium_bonuses == null)
              this.mLoginBonus28days = loginBonusData[index];
            else if (loginBonusData[index].premium_bonuses != null)
              this.mPremiumLoginBonus = loginBonusData[index];
          }
        }
      }
      this.SupportCount = 1;
      this.SupportGold = notify.supgold;
      return true;
    }

    private Json_LoginBonusTable[] CreateLoginBonusData(Json_LoginBonusTable[] originals)
    {
      if (originals == null)
        return (Json_LoginBonusTable[]) null;
      List<Dictionary<string, object>> bonusTableFromJson = this.CreateLoginBonusTableFromJson();
      if (bonusTableFromJson == null || bonusTableFromJson.Count < 1)
        return (Json_LoginBonusTable[]) null;
      List<Json_LoginBonusTable> jsonLoginBonusTableList = new List<Json_LoginBonusTable>();
      foreach (Json_LoginBonusTable original in originals)
      {
        Json_LoginBonusTable orig = original;
        DebugUtility.Log("[" + orig.type + "] のログインボーナステーブル生成開始");
        Dictionary<string, object> root = (Dictionary<string, object>) null;
        if (bonusTableFromJson.FirstOrDefault<Dictionary<string, object>>((Func<Dictionary<string, object>, bool>) (table => table.TryGetValueAndCast<Dictionary<string, object>>(orig.type, out root, false))) == null)
        {
          DebugUtility.LogError("[" + orig.type + "] が見つかりません。");
          throw new InvalidJSONException();
        }
        Json_LoginBonusTable jsonLoginBonusTable = new Json_LoginBonusTable();
        jsonLoginBonusTable.count = orig.count;
        jsonLoginBonusTable.type = orig.type;
        jsonLoginBonusTable.lastday = orig.lastday;
        root.TryGetValueAndCast<string>("prefab", out jsonLoginBonusTable.prefab, false);
        Dictionary<string, object> val1;
        if (root.TryGetValueAndCast<Dictionary<string, object>>("units", out val1, false))
        {
          string[] orderedJsonObject = this.SortToIntegerOrderedJsonObject<string>(val1);
          if (orderedJsonObject == null)
          {
            DebugUtility.LogError("[" + orig.type + "] のフォーマットエラー: unitsのキーは整数値、値は文字列を設定してください。");
            throw new InvalidJSONException();
          }
          jsonLoginBonusTable.bonus_units = orderedJsonObject;
        }
        Dictionary<string, object> val2;
        if (!root.TryGetValueAndCast<Dictionary<string, object>>("table", out val2, true))
        {
          DebugUtility.LogError("[" + orig.type + "] のフォーマットエラー: tableが見つかりません。");
          throw new InvalidJSONException();
        }
        bool flag = false;
        string val3;
        if (root.TryGetValueAndCast<string>("type", out val3, false) && val3 == "Premium")
          flag = true;
        if (flag)
          jsonLoginBonusTable.premium_bonuses = this.CreatePremiumLoginBonusItemTable(val2);
        else
          jsonLoginBonusTable.bonuses = this.CreateLoginBonusItemTable(val2);
        jsonLoginBonusTableList.Add(jsonLoginBonusTable);
        DebugUtility.Log("[" + orig.type + "] のログインボーナス生成完了");
      }
      return jsonLoginBonusTableList.ToArray();
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
        T obj = keyValuePair.Value as T;
        if ((object) obj == null)
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
      List<Dictionary<string, object>> dictionaryList = new List<Dictionary<string, object>>();
      string[] strArray = new string[2]{ AssetManager.LoadTextData("Data/CountupBonusTable"), AssetManager.LoadTextData("Data/PremiumLoginBonus") };
      foreach (string json in strArray)
      {
        Dictionary<string, object> dictionary = LoginBonusJsonParser.Deserialize(json);
        if (dictionary != null)
          dictionaryList.Add(dictionary);
      }
      return dictionaryList;
    }

    private Json_LoginBonus[] CreateLoginBonusItemTable(Dictionary<string, object> json)
    {
      Dictionary<string, object>[] orderedJsonObject = this.SortToIntegerOrderedJsonObject<Dictionary<string, object>>(json);
      List<Json_LoginBonus> jsonLoginBonusList = new List<Json_LoginBonus>();
      foreach (Dictionary<string, object> json1 in orderedJsonObject)
      {
        Json_LoginBonus jsonLoginBonus = new Json_LoginBonus();
        Dictionary<string, object> val1;
        if (json1.TryGetValueAndCast<Dictionary<string, object>>("item", out val1, false))
        {
          val1.TryGetValueAndCast<string>("iname", out jsonLoginBonus.iname, false);
          val1.TryGetValueAndCast<int>("num", out jsonLoginBonus.num, false);
        }
        Dictionary<string, object> val2;
        if (json1.TryGetValueAndCast<Dictionary<string, object>>("coin", out val2, false))
          val2.TryGetValueAndCast<int>("num", out jsonLoginBonus.coin, false);
        Dictionary<string, object> val3;
        if (json1.TryGetValueAndCast<Dictionary<string, object>>("vip", out val3, false))
        {
          Json_LoginBonusVip jsonLoginBonusVip = new Json_LoginBonusVip();
          val3.TryGetValueAndCast<int>("lv", out jsonLoginBonusVip.lv, false);
          jsonLoginBonus.vip = jsonLoginBonusVip;
        }
        jsonLoginBonusList.Add(jsonLoginBonus);
      }
      return jsonLoginBonusList.ToArray();
    }

    private Json_PremiumLoginBonus[] CreatePremiumLoginBonusItemTable(Dictionary<string, object> json)
    {
      Dictionary<string, object>[] orderedJsonObject1 = this.SortToIntegerOrderedJsonObject<Dictionary<string, object>>(json);
      List<Json_PremiumLoginBonus> premiumLoginBonusList = new List<Json_PremiumLoginBonus>();
      foreach (Dictionary<string, object> json1 in orderedJsonObject1)
      {
        Json_PremiumLoginBonus premiumLoginBonus = new Json_PremiumLoginBonus();
        Dictionary<string, object> val1;
        if (json.TryGetValueAndCast<Dictionary<string, object>>("item", out val1, false))
        {
          List<Json_PremiumLoginBonusItem> premiumLoginBonusItemList = new List<Json_PremiumLoginBonusItem>();
          Dictionary<string, object>[] orderedJsonObject2 = this.SortToIntegerOrderedJsonObject<Dictionary<string, object>>(json);
          if (orderedJsonObject2 != null)
          {
            foreach (Dictionary<string, object> dictionary in orderedJsonObject2)
            {
              Json_PremiumLoginBonusItem premiumLoginBonusItem = new Json_PremiumLoginBonusItem();
              string val2;
              if (val1.TryGetValueAndCast<string>("iname", out val2, false))
                premiumLoginBonusItem.iname = val2;
              int val3;
              if (val1.TryGetValueAndCast<int>("num", out val3, false))
                premiumLoginBonusItem.num = val3;
              premiumLoginBonusItemList.Add(premiumLoginBonusItem);
            }
          }
          premiumLoginBonus.item = premiumLoginBonusItemList.ToArray();
        }
        json1.TryGetValueAndCast<string>("icon", out premiumLoginBonus.icon, false);
        json1.TryGetValueAndCast<int>("coin", out premiumLoginBonus.coin, false);
        json1.TryGetValueAndCast<int>("gold", out premiumLoginBonus.gold, false);
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

    public void SetVersusRankpoint(int point)
    {
      this.mVersusPoint = point;
    }

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

    public bool IsFirstLogin
    {
      get
      {
        return this.mFirstLogin;
      }
    }

    public void ForceFirstLogin()
    {
      this.mFirstLogin = true;
    }

    public int LoginCountWithType(string type)
    {
      if (string.IsNullOrEmpty(type) || !this.mLoginBonusTables.ContainsKey(type))
        return 0;
      return this.mLoginBonusTables[type].count;
    }

    public Json_LoginBonus RecentLoginBonus
    {
      get
      {
        if (this.LoginBonus != null && 0 < this.mLoginBonusCount && this.mLoginBonusCount <= this.LoginBonus.Length)
          return this.LoginBonus[this.mLoginBonusCount - 1];
        return (Json_LoginBonus) null;
      }
    }

    public Json_LoginBonus FindRecentLoginBonus(string type)
    {
      Json_LoginBonus[] loginBonuses = this.FindLoginBonuses(type);
      if (loginBonuses == null)
        return (Json_LoginBonus) null;
      int num = this.LoginCountWithType(type);
      if (num < 1 || loginBonuses.Length < num)
        return (Json_LoginBonus) null;
      return loginBonuses[num - 1];
    }

    public Json_LoginBonusTable LoginBonus28days
    {
      get
      {
        return this.mLoginBonus28days;
      }
    }

    public Json_LoginBonus[] LoginBonus
    {
      get
      {
        return this.mLoginBonus;
      }
    }

    public Json_LoginBonus[] FindLoginBonuses(string type)
    {
      if (string.IsNullOrEmpty(type))
        return this.mLoginBonus;
      if (!this.mLoginBonusTables.ContainsKey(type))
        return (Json_LoginBonus[]) null;
      return this.mLoginBonusTables[type].bonuses;
    }

    public Json_LoginBonusTable PremiumLoginBonus
    {
      get
      {
        return this.mPremiumLoginBonus;
      }
    }

    public int LoginBonusCount
    {
      get
      {
        return this.mLoginBonusCount;
      }
    }

    public string GetLoginBonusePrefabName(string type)
    {
      if (string.IsNullOrEmpty(type))
        return (string) null;
      if (!this.mLoginBonusTables.ContainsKey(type))
        return (string) null;
      return this.mLoginBonusTables[type].prefab;
    }

    public string[] GetLoginBonuseUnitIDs(string type)
    {
      if (string.IsNullOrEmpty(type))
        return (string[]) null;
      if (!this.mLoginBonusTables.ContainsKey(type))
        return (string[]) null;
      return this.mLoginBonusTables[type].bonus_units;
    }

    public bool IsLastLoginBonus(string type)
    {
      if (string.IsNullOrEmpty(type) || !this.mLoginBonusTables.ContainsKey(type))
        return false;
      return this.mLoginBonusTables[type].lastday > 0;
    }

    public bool CheckUnlock(UnlockTargets target)
    {
      return ((UnlockTargets) (int) this.mUnlocks & target) != (UnlockTargets) 0;
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

    public PartyData FindPartyOfType(PlayerPartyTypes type)
    {
      return this.mPartys[(int) type];
    }

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

    public void MarkQuestCleared(string name)
    {
      this.SetQuestState(name, QuestStates.Cleared);
    }

    public QuestParam FindLastStoryQuest()
    {
      QuestParam[] availableQuests = this.AvailableQuests;
      int num = 0;
      string iname = PlayerPrefsUtility.GetString(PlayerPrefsUtility.LAST_SELECTED_STORY_QUEST_ID, string.Empty);
      if (!string.IsNullOrEmpty(iname))
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(iname);
        if (quest != null && quest.Chapter != null && (quest.Chapter.sectionParam != null && quest.Chapter.sectionParam.storyPart > 0))
          num = quest.Chapter.sectionParam.storyPart;
      }
      for (int index1 = 0; index1 < availableQuests.Length; ++index1)
      {
        if (availableQuests[index1].IsStory && !string.IsNullOrEmpty(availableQuests[index1].ChapterID) && (num <= 0 || availableQuests[index1].Chapter == null || (availableQuests[index1].Chapter.sectionParam == null || num == availableQuests[index1].Chapter.sectionParam.storyPart)))
        {
          QuestParam questParam = availableQuests[index1];
          for (int index2 = index1 + 1; index2 < availableQuests.Length; ++index2)
          {
            if (availableQuests[index2].IsStory && (num <= 0 || availableQuests[index2].Chapter == null || (availableQuests[index2].Chapter.sectionParam == null || num == availableQuests[index2].Chapter.sectionParam.storyPart)))
            {
              questParam = availableQuests[index2];
              if (availableQuests[index2].state != QuestStates.Cleared)
                return availableQuests[index2];
            }
          }
          return questParam;
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
      if (archiveByArea != null)
        return this.IsQuestArchiveOpen(archiveByArea.iname);
      return false;
    }

    public bool IsQuestArchiveOpen(string iname)
    {
      return !string.IsNullOrEmpty(iname) && this.OpenedQuestArchives != null && (this.OpenedQuestArchives.Any<OpenedQuestArchive>((Func<OpenedQuestArchive, bool>) (t => t.iname.Equals(iname))) && this.OpenedQuestArchives.Find((Predicate<OpenedQuestArchive>) (t => t.iname.Equals(iname))).end_at > TimeManager.ServerTime);
    }

    public OpenedQuestArchive GetOpenedQuestArchive(string iname)
    {
      OpenedQuestArchive openedQuestArchive = this.OpenedQuestArchives.Find((Predicate<OpenedQuestArchive>) (t => t.iname.Equals(iname)));
      if (openedQuestArchive != null && openedQuestArchive.end_at > TimeManager.ServerTime)
        return openedQuestArchive;
      return (OpenedQuestArchive) null;
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
    }

    public bool HasItem(string iname)
    {
      ItemData itemDataByItemId = this.FindItemDataByItemID(iname);
      if (itemDataByItemId != null)
        return itemDataByItemId.Num > 0;
      return false;
    }

    public int GetItemAmount(string iname)
    {
      ItemData itemDataByItemId = this.FindItemDataByItemID(iname);
      if (itemDataByItemId != null)
        return itemDataByItemId.Num;
      return 0;
    }

    public ItemData FindItemDataByItemID(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (ItemData) null;
      return this.mItems.Find((Predicate<ItemData>) (p => p.ItemID == iname));
    }

    public ItemData FindItemDataByItemParam(ItemParam param)
    {
      return this.mItems.Find((Predicate<ItemData>) (p => p.Param == param));
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

    public List<ArtifactData> GetInspirationSkillLvUpArtifacts(ArtifactData self, string artifact_iname, string inspiration_skill_iname)
    {
      if (self == null || this.mArtifacts == null || this.mArtifacts.Count <= 0)
        return new List<ArtifactData>();
      List<ArtifactData> all = this.mArtifacts.FindAll((Predicate<ArtifactData>) (artifact =>
      {
        if ((long) artifact.UniqueID != (long) self.UniqueID)
          return artifact.UsableInspirationSkillLvUp(artifact_iname, inspiration_skill_iname);
        return false;
      }));
      if (all == null || all.Count <= 0)
        return new List<ArtifactData>();
      InspSkillParam inspirationSkillParam = MonoSingleton<GameManager>.Instance.MasterParam.GetInspirationSkillParam(inspiration_skill_iname);
      return self.InspMaterialListSort(inspirationSkillParam, all);
    }

    public void RemoveArtifactByUniqueID(long iid)
    {
      int index = this.mArtifacts.FindIndex((Predicate<ArtifactData>) (p => (long) p.UniqueID == iid));
      if (index < 0)
        return;
      this.mArtifacts.RemoveAt(index);
    }

    public bool FindOwner(ArtifactData arti, out UnitData unit, out JobData job)
    {
      unit = (UnitData) null;
      job = (JobData) null;
      for (int index1 = 0; index1 < this.mUnits.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.mUnits[index1].Jobs.Length; ++index2)
        {
          for (int index3 = 0; index3 < this.mUnits[index1].Jobs[index2].Artifacts.Length; ++index3)
          {
            if (this.mUnits[index1].Jobs[index2].Artifacts[index3] == (long) arti.UniqueID)
            {
              unit = this.mUnits[index1];
              job = this.mUnits[index1].Jobs[index2];
              return true;
            }
          }
        }
      }
      return false;
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

    public PartyData GetPartyCurrent()
    {
      return this.Partys[this.GetPartyCurrentIndex()];
    }

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

    public int CalcLevel()
    {
      return PlayerData.CalcLevelFromExp((int) this.mExp);
    }

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
      this.mUnlocks = (OInt) 0;
      foreach (UnlockParam unlock in MonoSingleton<GameManager>.Instance.MasterParam.Unlocks)
      {
        if (unlock != null)
        {
          unlockTargets |= unlock.UnlockTarget;
          if (unlock.PlayerLevel <= this.Lv && unlock.VipRank <= this.VipRank)
          {
            PlayerData playerData = this;
            playerData.mUnlocks = (OInt) ((int) ((UnlockTargets) (int) playerData.mUnlocks | unlock.UnlockTarget));
          }
        }
      }
      PlayerData playerData1 = this;
      playerData1.mUnlocks = (OInt) ((int) ((UnlockTargets) (int) playerData1.mUnlocks | ~unlockTargets));
    }

    public void GainGold(int gold)
    {
      this.mGold = (OInt) Math.Max((int) this.mGold + gold, 0);
    }

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
      if (dateTime1.Year < dateTime2.Year || dateTime1.Month < dateTime2.Month || (dateTime1.Day < dateTime2.Day || this.FreeGachaGold.num == 0))
        return true;
      if (this.FreeGachaGold.num == (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.FreeGachaGoldMax)
        return false;
      return this.GetNextFreeGachaGoldCoolDownSec() == 0L;
    }

    public bool CheckFreeGachaGoldMax()
    {
      DateTime dateTime1 = TimeManager.FromUnixTime(Network.GetServerTime());
      DateTime dateTime2 = TimeManager.FromUnixTime(this.FreeGachaGold.at);
      if (dateTime1.Year < dateTime2.Year || dateTime1.Month < dateTime2.Month || dateTime1.Day < dateTime2.Day)
        return false;
      return this.FreeGachaGold.num == (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.FreeGachaGoldMax;
    }

    public long GetNextFreeGachaGoldCoolDownSec()
    {
      long serverTime = Network.GetServerTime();
      DateTime dateTime1 = TimeManager.FromUnixTime(serverTime);
      DateTime dateTime2 = TimeManager.FromUnixTime(this.FreeGachaGold.at);
      if (dateTime1.Year < dateTime2.Year || dateTime1.Month < dateTime2.Month || dateTime1.Day < dateTime2.Day)
        return 0;
      return Math.Max((long) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.FreeGachaGoldCoolDownSec - (serverTime - this.FreeGachaGold.at), 0L);
    }

    public bool CheckFreeGachaCoin()
    {
      return this.GetNextFreeGachaCoinCoolDownSec() == 0L;
    }

    public long GetNextFreeGachaCoinCoolDownSec()
    {
      return Math.Max((long) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.FreeGachaCoinCoolDownSec - (Network.GetServerTime() - this.FreeGachaCoin.at), 0L);
    }

    public bool CheckPaidGacha()
    {
      return this.PaidGacha.num == 0;
    }

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
          PlayerPrefsUtility.SetString(PlayerPrefsUtility.PLAYERDATA_INVENTORY + (object) index, this.mInventory[index].ItemID, false);
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
            ItemData itemDataByItemId = this.FindItemDataByItemID(iname);
            if (itemDataByItemId != null)
              this.mInventory[index] = itemDataByItemId;
          }
        }
      }
    }

    public bool UseExpPotion(UnitData unit, ItemData item)
    {
      if (item == null || item.Param == null || (item.Num <= 0 || item.ItemType != EItemType.ExpUpUnit))
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
      if (friendData != null)
        return friendData.IsFriend();
      return false;
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

    public bool CheckEnableCreateItem(ItemParam param, bool root = true, int needNum = 1, NeedEquipItemList item_list = null)
    {
      bool is_ikkatsu = false;
      return this.CheckEnableCreateItem(param, ref is_ikkatsu, root, needNum, item_list);
    }

    public bool CheckEnableCreateItem(ItemParam param, ref bool is_ikkatsu, bool root = true, int needNum = 1, NeedEquipItemList item_list = null)
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
        int val1 = itemDataByItemId == null ? 0 : itemDataByItemId.Num;
        int num1 = recipeItem.num * needNum;
        if (this.mConsumeMaterials.ContainsKey(recipeItem.iname))
        {
          int num2 = Math.Min(Math.Max(val1 - this.mConsumeMaterials[recipeItem.iname], 0), num1);
          if (num2 > 0)
          {
            Dictionary<string, int> consumeMaterials;
            string iname;
            (consumeMaterials = this.mConsumeMaterials)[iname = recipeItem.iname] = consumeMaterials[iname] + num2;
            num1 -= num2;
          }
        }
        else
        {
          int num2 = Math.Min(val1, num1);
          if (num2 > 0)
          {
            this.mConsumeMaterials.Add(recipeItem.iname, num2);
            num1 -= num2;
          }
        }
        if (num1 > 0)
        {
          ItemParam itemParam = MonoSingleton<GameManager>.GetInstanceDirect().GetItemParam(recipeItem.iname);
          if (item_list != null)
          {
            bool is_common = itemParam.IsCommon && index == 0;
            if (is_common)
              item_list.Add(itemParam, num1, false);
            else if (!itemParam.IsCommon && string.IsNullOrEmpty(itemParam.recipe))
              item_list.IsNotEnough = true;
            item_list.SetRecipeTree(new RecipeTree(itemParam), is_common);
          }
          if (!this.CheckEnableCreateItem(itemParam, ref is_ikkatsu, false, num1, item_list))
            flag = false;
          item_list?.UpRecipeTree();
          if (itemParam.recipe != null)
            is_ikkatsu = true;
        }
      }
      return flag;
    }

    public bool CheckEnableCreateItem(ItemParam param, ref bool is_ikkatsu, ref int cost, ref Dictionary<string, int> consumes, NeedEquipItemList item_list = null)
    {
      return this.CheckEnableCreateItem(param, 1, ref is_ikkatsu, ref cost, ref consumes, item_list);
    }

    public bool CheckEnableCreateItem(ItemParam param, int count, ref bool is_ikkatsu, ref int cost, ref Dictionary<string, int> consumes, NeedEquipItemList item_list = null)
    {
      bool flag = this.CheckEnableCreateItem(param, ref is_ikkatsu, true, count, item_list);
      cost = this.mCreateItemCost;
      consumes = this.mConsumeMaterials;
      return flag;
    }

    public int GetCreateItemCost(ItemParam param)
    {
      bool is_ikkatsu = false;
      this.CheckEnableCreateItem(param, ref is_ikkatsu, true, 1, (NeedEquipItemList) null);
      return this.mCreateItemCost;
    }

    public bool CheckEnableCreateEquipItemAll(UnitData self, EquipData[] equips, ref Dictionary<string, int> consume, ref int cost, NeedEquipItemList item_list = null)
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
          int val1 = itemDataByItemParam == null ? 0 : itemDataByItemParam.Num;
          int num1 = 1;
          if (this.mConsumeMaterials.ContainsKey(equip.ItemID))
          {
            int num2 = Math.Min(Math.Max(val1 - this.mConsumeMaterials[equip.ItemID], 0), num1);
            if (num2 > 0)
            {
              Dictionary<string, int> consumeMaterials;
              string itemId;
              (consumeMaterials = this.mConsumeMaterials)[itemId = equip.ItemID] = consumeMaterials[itemId] + num2;
              num1 -= num2;
            }
          }
          else
          {
            int num2 = Math.Min(val1, num1);
            if (num2 > 0)
            {
              this.mConsumeMaterials.Add(equip.ItemID, num2);
              num1 -= num2;
            }
          }
          if (num1 != 0 && !this.CheckEnableCreateItem(equips[index].ItemParam, false, num1, item_list))
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

    public bool CheckEnableCreateEquipItemAll(UnitData self, EquipData[] equips, NeedEquipItemList item_list = null)
    {
      return this.CheckEnableCreateEquipItemAll(self, equips, ref this.mConsumeMaterials, ref this.mCreateItemCost, item_list);
    }

    public bool CheckEnable2(UnitData self, EquipData[] equips_base, ref Dictionary<string, int> consume, ref int cost, ref int target_rank, ref bool can_jobmaster, ref bool can_jobmax, NeedEquipItemList item_list = null)
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

    public bool CheckEnableCreateEquipItemAll2(UnitData self, EquipData[] equips, NeedEquipItemList item_list = null)
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
          int val1 = itemDataByItemParam == null ? 0 : itemDataByItemParam.Num;
          int num1 = 1;
          if (this.mConsumeMaterials.ContainsKey(equip.ItemID))
          {
            int num2 = Math.Min(Math.Max(val1 - this.mConsumeMaterials[equip.ItemID], 0), num1);
            if (num2 > 0)
            {
              Dictionary<string, int> consumeMaterials;
              string itemId;
              (consumeMaterials = this.mConsumeMaterials)[itemId = equip.ItemID] = consumeMaterials[itemId] + num2;
              num1 -= num2;
            }
          }
          else
          {
            int num2 = Math.Min(val1, num1);
            if (num2 > 0)
            {
              this.mConsumeMaterials.Add(equip.ItemID, num2);
              num1 -= num2;
            }
          }
          if (num1 != 0 && !this.CheckEnableCreateItem(equip.ItemParam, false, num1, item_list) && (item_list == null || !item_list.IsEnoughCommon()))
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
        int num1 = itemDataByItemId2.Num < num ? itemDataByItemId2.Num : num;
        itemDataByItemId2.Used(num1);
        num -= num1;
      }
      if (num < 1)
        return;
      if (itemDataByItemId3 != null && itemDataByItemId3.Num > 0)
      {
        int num1 = itemDataByItemId3.Num < num ? itemDataByItemId3.Num : num;
        itemDataByItemId3.Used(num1);
        num -= num1;
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
      if (!unit.CheckJobRankUpAllEquip(jobIndex, true))
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
      ability.GainExp(1);
      this.mAbilityRankUpCount.val = (OInt) Math.Max((int) (--this.mAbilityRankUpCount.val), 0);
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
        this.Items.Add(itemData);
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
      Json_Unit json = new Json_Unit() { iid = uniqueID, iname = unitParam.iname, exp = 0, lv = 1, plus = 0, rare = 0, select = new Json_UnitSelectable() };
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
        ascending = PlayerPrefsUtility.GetInt(menuID + "#", 0) != 0;
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

    public List<UnitData> GetSortedUnits(GameUtility.UnitSortModes sortMode, bool ascending = false, bool includeShujinko = true)
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
      int num = 0;
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (this.mItems[index].Num != 0)
          ++num;
      }
      return num;
    }

    public bool CheckConceptCardCapacity(int adddValue)
    {
      return (int) GlobalVars.ConceptCardNum + adddValue <= this.ConceptCardCap;
    }

    public bool CheckItemCapacity(ItemParam item, int num)
    {
      return this.GetItemAmount(item.iname) + num <= item.cap;
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
      if (!this.CheckEnableCreateItem(item, ref is_ikkatsu, ref cost, ref consumes, (NeedEquipItemList) null) || cost > this.Gold)
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
      return this.Items.Find((Predicate<ItemData>) (item =>
      {
        if (item.ItemType == EItemType.GoldConvert)
          return item.Num > 0;
        return false;
      })) != null;
    }

    public ShopData GetShopData(EShopType type)
    {
      if (type == EShopType.Limited)
        return this.GetLimitedShopData().GetShopData();
      if (type == EShopType.Event)
        return this.GetEventShopData().GetShopData();
      return this.mShops[(int) type];
    }

    public void SetShopData(EShopType type, ShopData shop)
    {
      switch (type)
      {
        case EShopType.Event:
          this.mEventShops.SetShopData(shop);
          break;
        case EShopType.Limited:
          this.mLimitedShops.SetShopData(shop);
          break;
      }
      this.mShops[(int) type] = shop;
    }

    public LimitedShopData GetLimitedShopData()
    {
      return this.mLimitedShops;
    }

    public void SetLimitedShopData(LimitedShopData shop)
    {
      this.mLimitedShops = shop;
    }

    public EventShopData GetEventShopData()
    {
      return this.mEventShops;
    }

    public void SetEventShopData(EventShopData shop)
    {
      this.mEventShops = shop;
    }

    public bool CheckUnlockShopType(EShopType type)
    {
      UnlockTargets unlockTargets = type.ToUnlockTargets();
      if (unlockTargets != (UnlockTargets) 0)
        return this.CheckUnlock(unlockTargets);
      return false;
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
      }
      if (key == string.Empty)
        return key;
      return LocalizedText.Get(key);
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
      if (shopParam == null)
        return false;
      return this.GetShopUpdateCost(type, false) <= this.GetShopTypeCostAmount(shopParam.UpdateCostType);
    }

    public void DEBUG_BUY_ITEM_UPDATED(EShopType shoptype)
    {
      ShopData shopData = this.GetShopData(shoptype);
      ShopParam shopParam = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(shoptype);
      if (shopData == null || shopParam == null || !this.CheckShopUpdateCost(shoptype))
        return;
      for (int index = 0; index < shopData.items.Count; ++index)
        shopData.items[index].is_soldout = false;
      int shopUpdateCost = this.GetShopUpdateCost(shoptype, false);
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

    public DateTime VipExpiredAt
    {
      get
      {
        return TimeManager.FromUnixTime((long) this.mVipExpiredAt);
      }
    }

    public bool CheckEnableVipCard()
    {
      return Network.GetServerTime() < (long) this.mVipExpiredAt;
    }

    public DateTime PremiumExpiredAt
    {
      get
      {
        return TimeManager.FromUnixTime((long) this.mPremiumExpiredAt);
      }
    }

    public bool CheckEnablePremiumMember()
    {
      return Network.GetServerTime() < (long) this.mPremiumExpiredAt;
    }

    public void SubAbilityRankUpCount(int value)
    {
      this.mAbilityRankUpCount.SubValue(value);
    }

    public void RestoreAbilityRankUpCount()
    {
      this.mAbilityRankUpCount.val = this.mAbilityRankUpCount.valMax;
      this.mAbilityRankUpCount.at = (OLong) Network.GetServerTime();
    }

    public void SubStamina(int value)
    {
      this.mStamina.SubValue(value);
    }

    public long GetNextStaminaRecoverySec()
    {
      return this.mStamina.GetNextRecoverySec();
    }

    public void UpdateStamina()
    {
      this.mStamina.Update();
    }

    public int GetStaminaRecoveryCost(bool getOldCost = false)
    {
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      int mStaminaBuyNum = (int) this.mStaminaBuyNum;
      if (getOldCost)
        --mStaminaBuyNum;
      int index = Math.Max(Math.Min(mStaminaBuyNum, fixParam.StaminaAddCost.Length - 1), 0);
      return (int) fixParam.StaminaAddCost[index];
    }

    public void ResetStaminaRecoverCount()
    {
      this.mStaminaBuyNum = (OInt) 0;
    }

    public void ResetBuyGoldNum()
    {
      this.mGoldBuyNum = (OInt) 0;
    }

    public void SubCaveStamina(int value)
    {
      this.mCaveStamina.SubValue(value);
    }

    public long GetNextCaveStaminaRecoverySec()
    {
      return this.mCaveStamina.GetNextRecoverySec();
    }

    public void UpdateCaveStamina()
    {
      this.mCaveStamina.Update();
    }

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

    public void UpdateAbilityRankUpCount()
    {
      this.mAbilityRankUpCount.Update();
    }

    public int ArenaResetCount
    {
      get
      {
        return this.mArenaResetCount;
      }
    }

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
      if (this.ChallengeArenaNum >= this.ChallengeArenaMax)
        return false;
      return (int) this.mChallengeArenaTimer.val == (int) this.mChallengeArenaTimer.valMax;
    }

    public long GetNextChallengeArenaCoolDownSec()
    {
      return this.mChallengeArenaTimer.GetNextRecoverySec();
    }

    public void UpdateChallengeArenaTimer()
    {
      this.mChallengeArenaTimer.Update();
    }

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
        this.GainItem(items[index].iname, 10);
    }

    public void DEBUG_TRASH_ALL_ITEMS()
    {
      this.Items.Clear();
    }

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
              for (int index3 = 0; index3 < 8; ++index3)
              {
                string str3 = str2 + "Ability" + (object) index3 + "_";
                if (EditorPlayerPrefs.HasKey(str3 + "Iname") && !string.IsNullOrEmpty(EditorPlayerPrefs.GetString(str3 + "Iname")))
                {
                  Json_Ability jsonAbility = new Json_Ability();
                  jsonAbility.iname = EditorPlayerPrefs.GetString(str3 + "Iname");
                  jsonAbility.iid = (long) EditorPlayerPrefs.GetInt(str3 + "Iid");
                  jsonAbility.exp = EditorPlayerPrefs.GetInt(str3 + "Exp");
                  bool flag2 = false;
                  for (int index4 = 0; index4 < jsonAbilityList2.Count; ++index4)
                  {
                    if (jsonAbilityList2[index4].iname == jsonAbility.iname)
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
              for (int index3 = 0; index3 < jsonJob.select.abils.Length; ++index3)
              {
                string key = str2 + "Select_Ability" + (object) index3;
                if (EditorPlayerPrefs.HasKey(key))
                  jsonJob.select.abils[index3] = (long) EditorPlayerPrefs.GetInt(key);
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
                    unitData.SetEquipArtifactData(job_index, slot, artifact, true);
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
      for (int index1 = 0; index1 < 12; ++index1)
      {
        Json_Party json = new Json_Party();
        PartyData partyData = new PartyData((PlayerPartyTypes) index1);
        json.units = new long[partyData.MAX_UNIT];
        for (int index2 = 0; index2 < json.units.Length; ++index2)
          json.units[index2] = (long) EditorPlayerPrefs.GetInt("Hensei" + (object) index1 + "_UNIT" + (object) index2 + "_ID");
        this.mPartys[index1].Deserialize(json);
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
      return (IEnumerator) new PlayerData.\u003CSavePlayerPrefsAsync\u003Ec__Iterator0() { \u0024this = this };
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

    public void ClearTrophies()
    {
      this.mTrophyStates = new List<TrophyState>();
      this.mTrophyStatesInameDict = new Dictionary<string, List<TrophyState>>();
    }

    public void DeleteTrophies(JSON_TrophyProgress[] trophies)
    {
      if (trophies == null)
        return;
      if (this.mTrophyStates != null)
      {
        for (int i = 0; i < trophies.Length; ++i)
          this.mTrophyStates.RemoveAll((Predicate<TrophyState>) (state => state.iname == trophies[i].iname));
      }
      if (this.mTrophyStatesInameDict == null)
        return;
      for (int index = 0; index < trophies.Length; ++index)
      {
        if (this.mTrophyStatesInameDict.ContainsKey(trophies[index].iname))
          this.mTrophyStatesInameDict.Remove(trophies[index].iname);
      }
    }

    public bool IsTrophyDirty()
    {
      for (int index = this.mTrophyStates.Count - 1; index >= 0; --index)
      {
        if (this.mTrophyStates[index].IsDirty)
          return true;
      }
      return false;
    }

    private void UpdateTrophyState(TrophyState st, int currentYMD)
    {
      if (!st.Param.IsDaily)
        return;
      int startYmd = st.StartYMD;
      int num = Math.Abs(currentYMD.FromYMD().Subtract(startYmd.FromYMD()).Days);
      if (st.IsEnded)
      {
        if (num < 1)
          return;
        this.ClearTrophyCounter(st);
      }
      else if (!st.IsCompleted)
      {
        if (num < 1)
          return;
        this.ClearTrophyCounter(st);
      }
      else if (num >= 2)
      {
        this.ClearTrophyCounter(st);
      }
      else
      {
        if (num < 1)
          return;
        for (int index = 0; index < st.Param.Objectives.Length; ++index)
        {
          if (st.Param.Objectives[index].type == TrophyConditionTypes.stamina)
          {
            this.ClearTrophyCounter(st);
            break;
          }
        }
      }
    }

    public TrophyParam[] GetCompletedTrophies()
    {
      List<TrophyParam> trophyParamList = new List<TrophyParam>(this.mTrophyStates.Count);
      for (int index = this.mTrophyStates.Count - 1; index >= 0; --index)
      {
        TrophyState mTrophyState = this.mTrophyStates[index];
        if (!mTrophyState.IsEnded && mTrophyState.IsCompleted)
          trophyParamList.Add(mTrophyState.Param);
      }
      return trophyParamList.ToArray();
    }

    public void MarkTrophiesEnded(TrophyParam[] trophies)
    {
      for (int index = 0; index < trophies.Length; ++index)
      {
        TrophyState trophyCounter = this.GetTrophyCounter(trophies[index], true);
        trophyCounter.IsEnded = true;
        trophyCounter.IsDirty = true;
        trophyCounter.RewardedAt = TimeManager.ServerTime;
      }
    }

    private void ClearTrophyCounter(TrophyState _st)
    {
      if (this.mTrophyStates.Contains(_st))
        this.mTrophyStates.Remove(_st);
      if (!this.mTrophyStatesInameDict.ContainsKey(_st.iname))
        return;
      this.mTrophyStatesInameDict[_st.iname].Remove(_st);
      if (this.mTrophyStatesInameDict[_st.iname].Count > 0)
        return;
      this.mTrophyStatesInameDict.Remove(_st.iname);
    }

    public TrophyState[] TrophyStates
    {
      get
      {
        return this.mTrophyStates.ToArray();
      }
    }

    public IList<TrophyState> TrophyStatesList
    {
      get
      {
        return (IList<TrophyState>) this.mTrophyStates;
      }
    }

    public void UpdateTrophyStates()
    {
      int ymd = TimeManager.ServerTime.ToYMD();
      TrophyState[] array = this.mTrophyStates.ToArray();
      for (int index = 0; index < array.Length; ++index)
      {
        if (array[index] != null)
          this.UpdateTrophyState(array[index], ymd);
      }
    }

    public void OverwiteTrophyProgress(JSON_TrophyProgress[] trophyProgressList)
    {
      if (trophyProgressList == null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      for (int index = 0; index < trophyProgressList.Length; ++index)
      {
        JSON_TrophyProgress trophyProgress = trophyProgressList[index];
        if (trophyProgress != null)
        {
          TrophyParam trophy = instance.MasterParam.GetTrophy(trophyProgress.iname);
          if (trophy == null)
          {
            DebugUtility.LogWarning("存在しないミッション:" + trophyProgress.iname);
          }
          else
          {
            TrophyState trophyCounter = this.GetTrophyCounter(trophy, false);
            bool flag = trophyCounter.IsEnded || trophyCounter.IsCompleted;
            trophyCounter.Setup(trophy, trophyProgress);
            if (!flag && trophyCounter.IsCompleted)
              NotifyList.PushTrophy(trophy);
          }
        }
      }
    }

    public TrophyState RegistTrophyStateDictByProg(TrophyParam _trophy, JSON_TrophyProgress _prog)
    {
      TrophyState trophyState = this.CreateTrophyState(_trophy);
      trophyState.Setup(_trophy, _prog);
      this.AddTrophyStateDict(trophyState);
      return trophyState;
    }

    public void RegistTrophyStateDictByProgExtra(JSON_TrophyProgress[] _prog)
    {
      if (_prog == null || _prog.Length <= 0)
        return;
      for (int index = 0; index < _prog.Length; ++index)
        MonoSingleton<GameManager>.Instance.Player.RegistTrophyStateDictByProgExtra(MonoSingleton<GameManager>.Instance.MasterParam.GetTrophy(_prog[index].iname), _prog[index]);
    }

    public void RegistTrophyStateDictByProgExtra(TrophyParam _trophy, JSON_TrophyProgress _prog)
    {
      if (!this.mTrophyStatesInameDict.ContainsKey(_trophy.iname))
        this.AddTrophyStateDict(this.CreateTrophyState(_trophy));
      TrophyState trophyState = this.mTrophyStatesInameDict[_trophy.iname].Find((Predicate<TrophyState>) (x => x.iname == _trophy.iname));
      if (trophyState == null || trophyState.IsCompleted)
        return;
      for (int index = 0; index < _trophy.Objectives.Length && index < _prog.pts.Length && index < trophyState.Count.Length; ++index)
        trophyState.Count[index] = Math.Min(_prog.pts[index], _trophy.Objectives[index].ival);
      if (trophyState.IsCompleted && trophyState.Param.DispType == TrophyDispType.Award)
        NotifyList.PushAward(trophyState.Param);
      trophyState.StartYMD = _prog.ymd;
      trophyState.IsEnded = _prog.rewarded_at != 0;
      trophyState.IsDirty = true;
    }

    public void CreateInheritingExtraTrophy(Dictionary<int, List<JSON_TrophyProgress>> progs)
    {
      TrophyParam[] trophies = MonoSingleton<GameManager>.Instance.MasterParam.Trophies;
      if (trophies == null)
        return;
      for (int index1 = 0; index1 < trophies.Length; ++index1)
      {
        TrophyParam param = trophies[index1];
        if (trophies[index1].Objectives[0].type.IsExtraClear())
        {
          int type = (int) trophies[index1].Objectives[0].type;
          if (progs.ContainsKey(type))
          {
            List<JSON_TrophyProgress> prog = progs[type];
            if (prog.Find((Predicate<JSON_TrophyProgress>) (x => x.iname == param.iname)) == null)
            {
              int num = 0;
              for (int index2 = 0; index2 < prog.Count; ++index2)
              {
                if (num < prog[index2].pts[0])
                  num = prog[index2].pts[0];
              }
              TrophyState trophyState = this.CreateTrophyState(param);
              this.SetTrophyCounter(trophyState.Param, 0, num);
              this.AddTrophyStateDict(trophyState);
            }
          }
        }
      }
    }

    public TrophyState GetTrophyCounter(TrophyParam trophy, bool daily_old_data = false)
    {
      List<TrophyState> trophyStateList;
      if (this.mTrophyStatesInameDict.TryGetValue(trophy.iname, out trophyStateList))
      {
        if (!trophy.IsDaily || daily_old_data)
          return trophyStateList[0];
        for (int index = 0; index < trophyStateList.Count; ++index)
        {
          if (trophyStateList[index].StartYMD == TimeManager.ServerTime.ToYMD())
            return trophyStateList[index];
        }
      }
      TrophyState trophyState = this.CreateTrophyState(trophy);
      this.AddTrophyStateDict(trophyState);
      return trophyState;
    }

    private TrophyState CreateTrophyState(TrophyParam _trophy)
    {
      return new TrophyState() { iname = _trophy.iname, StartYMD = TimeManager.ServerTime.ToYMD(), Count = new int[_trophy.Objectives.Length], IsDirty = false, Param = _trophy };
    }

    private void AddTrophyStateDict(TrophyState _state)
    {
      this.mTrophyStates.Add(_state);
      if (!this.mTrophyStatesInameDict.ContainsKey(_state.iname))
        this.mTrophyStatesInameDict.Add(_state.iname, new List<TrophyState>());
      this.mTrophyStatesInameDict[_state.iname].Add(_state);
      this.mTrophyStatesInameDict[_state.iname].Sort((Comparison<TrophyState>) ((a, b) => a.StartYMD - b.StartYMD));
    }

    private bool IsMakeTrophyPlate(TrophyParam trophy, TrophyState st, bool is_achievement)
    {
      return !trophy.IsInvisibleVip() && !trophy.IsInvisibleCard() && !trophy.IsInvisibleStamina() && ((trophy.RequiredTrophies == null || TrophyParam.CheckRequiredTrophies(MonoSingleton<GameManager>.Instance, trophy, true, true)) && trophy.IsAvailablePeriod(TimeManager.ServerTime, is_achievement));
    }

    public void DailyAllCompleteCheck()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.dailyall);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (this.IsDailyAllComplete())
          this.AddTrophyCounter(trophyObjective, 1);
      }
    }

    public bool IsDailyAllComplete()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TrophyParam[] trophies = instance.Trophies;
      PlayerData player = instance.Player;
      if (trophies == null || trophies.Length <= 0)
        return true;
      TrophyState[] trophyStateArray = new TrophyState[trophies.Length];
      for (int index = 0; index < trophies.Length; ++index)
        trophyStateArray[index] = !trophies[index].IsChallengeMission ? player.GetTrophyCounter(trophies[index], false) : (TrophyState) null;
      for (int index1 = 0; index1 < trophies.Length; ++index1)
      {
        TrophyState st = trophyStateArray[index1];
        if (st != null && !st.IsCompleted)
        {
          TrophyParam trophy = trophies[index1];
          bool flag = false;
          for (int index2 = 0; index2 < trophy.Objectives.Length; ++index2)
          {
            if (trophy.Objectives[index2].type == TrophyConditionTypes.dailyall)
            {
              flag = true;
              break;
            }
          }
          if (!flag && trophy.DispType != TrophyDispType.Award && (trophy.DispType != TrophyDispType.Hide && trophy.IsDaily) && this.IsMakeTrophyPlate(trophy, st, false))
            return false;
        }
      }
      return true;
    }

    private bool CheckTrophyCount(TrophyParam trophyParam, int countIndex, int value, ref TrophyState state)
    {
      if (countIndex < 0 || value <= 0 || (trophyParam == null || !trophyParam.IsAvailablePeriod(this.GetMissionClearAt(), false)) || trophyParam.RequiredTrophies != null && !TrophyParam.CheckRequiredTrophies(MonoSingleton<GameManager>.Instance, trophyParam, trophyParam.IsChallengeMission, true))
        return false;
      state = this.GetTrophyCounter(trophyParam, false);
      if (state.IsEnded)
        return false;
      if (state.Count.Length <= countIndex)
        Array.Resize<int>(ref state.Count, countIndex + 1);
      return !state.IsCompleted;
    }

    private bool CheckDailyMissionDayChange(TrophyState state, int countIndex)
    {
      int ymd = this.GetMissionClearAt().ToYMD();
      return !state.Param.IsDaily || ymd <= state.StartYMD || state.IsCompleted;
    }

    public void AddTrophyCounter(TrophyObjective obj, int value)
    {
      this.AddTrophyCounter(obj.Param, obj.index, value);
    }

    public void AddTrophyCounter(TrophyParam trophyParam, int countIndex, int value)
    {
      if (!this.AddTrophyCounterExec(trophyParam, countIndex, value))
        return;
      this.DailyAllCompleteCheck();
    }

    private bool AddTrophyCounterExec(TrophyParam trophyParam, int countIndex, int value)
    {
      TrophyState state = (TrophyState) null;
      if (!this.CheckTrophyCount(trophyParam, countIndex, value, ref state))
        return false;
      int num = state.Count[countIndex];
      state.Count[countIndex] += value;
      if (!this.CheckDailyMissionDayChange(state, countIndex))
      {
        state.Count[countIndex] = num;
        return false;
      }
      state.IsDirty = true;
      MonoSingleton<GameManager>.Instance.update_trophy_interval.SetSyncNow();
      return state.IsCompleted;
    }

    public void SetTrophyCounter(TrophyObjective obj, int value)
    {
      this.SetTrophyCounter(obj.Param, obj.index, value);
    }

    private void SetTrophyCounter(TrophyParam trophyParam, int countIndex, int value)
    {
      if (!this.SetTrophyCounterExec(trophyParam, countIndex, value))
        return;
      this.DailyAllCompleteCheck();
    }

    private bool SetTrophyCounterExec(TrophyParam trophyParam, int countIndex, int value)
    {
      TrophyState state = (TrophyState) null;
      if (!this.CheckTrophyCount(trophyParam, countIndex, value, ref state) || state.Count[countIndex] == value)
        return false;
      int num = state.Count[countIndex];
      state.Count[countIndex] = value;
      if (!this.CheckDailyMissionDayChange(state, countIndex))
      {
        state.Count[countIndex] = num;
        return false;
      }
      state.IsDirty = true;
      MonoSingleton<GameManager>.Instance.update_trophy_interval.SetSyncNow();
      return state.IsCompleted;
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
              this.AddTrophyCounter(trophyParam, countIndex, 1);
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
              this.AddTrophyCounter(trophyParam, countIndex, 1);
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
            this.AddTrophyCounter(trophyParam, countIndex, 1);
        }
      }
    }

    public void OnQuestWin(string questID, BattleCore.Record battleRecord = null)
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
        else if (quest.type == QuestTypes.Event || quest.type == QuestTypes.Beginner || (quest.type == QuestTypes.Arena || quest.IsMulti) || (quest.type == QuestTypes.Character || quest.difficulty != QuestDifficulties.Normal || (quest.type == QuestTypes.Tower || quest.IsVersus)) || (quest.type == QuestTypes.Ordeal || quest.type == QuestTypes.RankMatch || (quest.type == QuestTypes.Raid || quest.type == QuestTypes.GenesisStory) || quest.type == QuestTypes.GenesisBoss))
          continue;
        this.AddTrophyCounter(trophyObjective, 1);
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
              this.AddTrophyCounter(trophiesOfType2[index1], 1);
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
          TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.complete_all_quest_mission_total);
          for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
          {
            if (!(trophiesOfType2[index].sval_base != questID))
              this.AddTrophyCounter(trophiesOfType2[index], 1);
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
      if (quest.type != QuestTypes.GenesisStory && quest.type != QuestTypes.GenesisBoss)
      {
        if (quest.difficulty == QuestDifficulties.Extra)
        {
          TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winstory_extra);
          for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
            this.AddTrophyCounter(trophiesOfType2[index], 1);
        }
        if (quest.difficulty == QuestDifficulties.Elite)
        {
          TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winelite);
          for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
            this.AddTrophyCounter(trophiesOfType2[index], 1);
        }
      }
      if (quest.type == QuestTypes.Arena)
      {
        TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winarena);
        for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
          this.AddTrophyCounter(trophiesOfType2[index], 1);
      }
      if (quest.type == QuestTypes.Event || quest.type == QuestTypes.Tower || quest.type == QuestTypes.GenesisStory)
      {
        TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winevent);
        for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
          this.AddTrophyCounter(trophiesOfType2[index], 1);
      }
      SupportData supportData = (SupportData) GlobalVars.SelectedSupport;
      if (quest.type == QuestTypes.Ordeal)
      {
        supportData = (SupportData) null;
        if (GlobalVars.OrdealSupports != null)
        {
          foreach (SupportData ordealSupport in GlobalVars.OrdealSupports)
          {
            if (ordealSupport != null)
            {
              supportData = ordealSupport;
              break;
            }
          }
        }
      }
      if (supportData != null)
      {
        TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winquestsoldier);
        for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
          this.AddTrophyCounter(trophiesOfType2[index], 1);
      }
      if (quest.IsMulti)
      {
        TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winmulti);
        for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType2[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == questID)
            this.AddTrophyCounter(trophyObjective, 1);
        }
        TrophyObjective[] trophiesOfType3 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winmultimore);
        for (int index = trophiesOfType3.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType3[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == questID)
          {
            List<JSON_MyPhotonPlayerParam> myPlayersStarted = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted();
            if (myPlayersStarted != null && myPlayersStarted.Count >= trophyObjective.ival)
              this.AddTrophyCounter(trophyObjective, 1);
          }
        }
        TrophyObjective[] trophiesOfType4 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.winmultiless);
        for (int index = trophiesOfType4.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType4[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == questID)
          {
            List<JSON_MyPhotonPlayerParam> myPlayersStarted = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted();
            if (myPlayersStarted != null && myPlayersStarted.Count <= trophyObjective.ival)
              this.AddTrophyCounter(trophyObjective, 1);
          }
        }
      }
      if (quest.type == QuestTypes.Tower)
      {
        TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.wintower);
        for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType2[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == questID)
            this.AddTrophyCounter(trophyObjective, 1);
        }
        TowerFloorParam towerFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(questID);
        if (towerFloor != null)
        {
          TrophyObjective[] trophiesOfType3 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.tower);
          for (int index = trophiesOfType3.Length - 1; index >= 0; --index)
          {
            TrophyObjective trophyObjective = trophiesOfType3[index];
            if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == towerFloor.tower_id)
              this.AddTrophyCounter(trophyObjective, 1);
          }
        }
      }
      if (quest.IsVersus)
      {
        TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.vswin);
        for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType2[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == questID)
            this.AddTrophyCounter(trophyObjective, 1);
        }
        TrophyObjective[] trophiesOfType3 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.vs);
        for (int index = trophiesOfType3.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType3[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == questID)
            this.AddTrophyCounter(trophyObjective, 1);
        }
      }
      if (quest.type != QuestTypes.Ordeal)
        return;
      TrophyObjective[] trophiesOfType5 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.clear_ordeal);
      for (int index = trophiesOfType5.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType5[index];
        if (!string.IsNullOrEmpty(trophyObjective.sval_base))
        {
          if (trophyObjective.sval_base == quest.iname)
            this.AddTrophyCounter(trophyObjective, 1);
        }
        else
          DebugUtility.LogError("レコードミッション「" + trophyObjective.Param.Name + "」はクエストが指定されていません。");
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
        else if (quest.type == QuestTypes.Event || quest.type == QuestTypes.Beginner || (quest.type == QuestTypes.Arena || quest.IsMulti) || (quest.type == QuestTypes.Character || quest.difficulty != QuestDifficulties.Normal || (quest.type == QuestTypes.Tower || quest.IsVersus)) || (quest.type == QuestTypes.Ordeal || quest.type == QuestTypes.RankMatch || (quest.type == QuestTypes.Raid || quest.type == QuestTypes.GenesisStory) || quest.type == QuestTypes.GenesisBoss))
          continue;
        this.AddTrophyCounter(trophyObjective, 1);
      }
      if (quest.type != QuestTypes.GenesisStory && quest.type != QuestTypes.GenesisBoss && quest.difficulty == QuestDifficulties.Elite)
      {
        TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.loseelite);
        for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
          this.AddTrophyCounter(trophiesOfType2[index], 1);
      }
      if (quest.type == QuestTypes.Arena)
      {
        TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.losearena);
        for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
          this.AddTrophyCounter(trophiesOfType2[index], 1);
      }
      if (quest.type == QuestTypes.Event || quest.type == QuestTypes.Tower || quest.type == QuestTypes.GenesisStory)
      {
        TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.loseevent);
        for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
          this.AddTrophyCounter(trophiesOfType2[index], 1);
      }
      if (quest.type == QuestTypes.Tower)
      {
        TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.losetower);
        for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType2[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == questID)
            this.AddTrophyCounter(trophyObjective, 1);
        }
        TowerFloorParam towerFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(questID);
        if (towerFloor != null)
        {
          TrophyObjective[] trophiesOfType3 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.tower);
          for (int index = trophiesOfType3.Length - 1; index >= 0; --index)
          {
            TrophyObjective trophyObjective = trophiesOfType3[index];
            if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == towerFloor.tower_id)
              this.AddTrophyCounter(trophyObjective, 1);
          }
        }
      }
      if (!quest.IsVersus)
        return;
      TrophyObjective[] trophiesOfType4 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.vslose);
      for (int index = trophiesOfType4.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType4[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == questID)
          this.AddTrophyCounter(trophyObjective, 1);
      }
      TrophyObjective[] trophiesOfType5 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.vs);
      for (int index = trophiesOfType5.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType5[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == questID)
          this.AddTrophyCounter(trophyObjective, 1);
      }
    }

    public void OnGoldChange(int delta)
    {
      if (delta == 0)
        return;
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.has_gold_over);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        if (this.Gold >= trophiesOfType[index].ival)
          this.AddTrophyCounter(trophiesOfType[index], 1);
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
        if (trophyObjective.sval_base == itemID)
          this.AddTrophyCounter(trophyObjective, delta);
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
          this.AddTrophyCounter(trophyObjective, 1);
      }
    }

    public void OnEnemyKill(string enemyID, int count)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.killenemy);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (trophyObjective.sval_base == enemyID)
          this.AddTrophyCounter(trophyObjective, 1);
      }
    }

    public void OnDamageToEnemy(Unit unit, Unit target, int damage)
    {
      if (unit == null || unit.Side != EUnitSide.Player || (!unit.IsPartyMember || target == null) || (target.Side != EUnitSide.Enemy || (UnityEngine.Object) SceneBattle.Instance == (UnityEngine.Object) null || SceneBattle.Instance.IsPlayingArenaQuest) || SceneBattle.Instance.Battle != null && SceneBattle.Instance.Battle.IsMultiPlay && (PunMonoSingleton<MyPhoton>.Instance.MyPlayerIndex <= 0 || PunMonoSingleton<MyPhoton>.Instance.MyPlayerIndex != unit.OwnerPlayerIndex))
        return;
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.damage_over);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        if (trophiesOfType[index].ival <= damage)
          this.AddTrophyCounter(trophiesOfType[index], 1);
      }
    }

    public void OnAbilityPowerUp(string unitID, string abilityID, int level, bool verify = false)
    {
      if (!verify)
      {
        TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.ability);
        for (int index = trophiesOfType.Length - 1; index >= 0; --index)
          this.AddTrophyCounter(trophiesOfType[index], 1);
      }
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.makeabilitylevel);
      for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType1[index];
        if (trophyObjective.ival <= level)
        {
          if (string.IsNullOrEmpty(trophyObjective.sval_base))
          {
            this.AddTrophyCounter(trophyObjective, 1);
          }
          else
          {
            char[] chArray = new char[1]{ ',' };
            string[] strArray = trophyObjective.sval_base.Split(chArray);
            if ((string.IsNullOrEmpty(strArray[1]) || abilityID == strArray[1]) && (string.IsNullOrEmpty(strArray[0]) || unitID == strArray[0]))
              this.AddTrophyCounter(trophyObjective, 1);
          }
        }
      }
    }

    public void OnSoubiPowerUp()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.soubi);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        this.AddTrophyCounter(trophiesOfType[index], 1);
    }

    public void OnBuyGold()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.buygold);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        this.AddTrophyCounter(trophiesOfType[index], 1);
    }

    public void OnFgGIDLogin()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.fggid);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        this.AddTrophyCounter(trophiesOfType[index], 1);
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
        this.AddTrophyCounter(trophyObjective, count);
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
          if (trophyObjective.sval_base == unitID && trophyObjective.ival <= level)
            this.AddTrophyCounter(trophyObjective, delta);
        }
      }
      if (!verify)
      {
        TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.upunitlevel);
        for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == unitID)
            this.AddTrophyCounter(trophyObjective, delta);
        }
      }
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.makeunitlevel);
      for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType1[index];
        if (trophyObjective.ival <= level && (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == unitID))
          this.AddTrophyCounter(trophyObjective, 1);
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
                  this.AddTrophyCounter(trohy, 1);
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
        if (trophyObjective.sval_base == unitID && trophyObjective.ival <= rarity)
          this.AddTrophyCounter(trophyObjective, 1);
      }
      TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.evoltiontimes);
      for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType2[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == unitID)
          this.AddTrophyCounter(trophyObjective, 1);
      }
    }

    public void OnJobLevelChange(string unitID, string jobID, int rank, bool verify = false, int rankDelta = 1)
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
            this.AddTrophyCounter(trophyObjective, 1);
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
            this.AddTrophyCounter(trophyObjective, rankDelta);
          }
          else
          {
            string[] strArray = trophyObjective.sval_base.Split(chArray);
            if (strArray[0] == unitID && strArray[1] == jobID)
              this.AddTrophyCounter(trophyObjective, rankDelta);
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
            this.AddTrophyCounter(trophyObjective, 1);
          }
          else
          {
            string[] strArray = trophyObjective.sval_base.Split(chArray);
            if (strArray[0] == unitID && strArray[1] == jobID)
              this.AddTrophyCounter(trophyObjective, 1);
          }
        }
      }
    }

    public void OnMultiTowerHelp()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.multitower_help);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        this.AddTrophyCounter(trophiesOfType[index], 1);
    }

    public void OnLoginCount()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.logincount);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (trophyObjective.ival <= this.LoginBonusCount)
          this.AddTrophyCounter(trophyObjective, 1);
      }
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
      MonoSingleton<GameManager>.Instance.Player.UpdateArenaRankTrophyStates(-1, -1);
      MonoSingleton<GameManager>.Instance.Player.UpdateArtifactTrophyStates();
      MonoSingleton<GameManager>.Instance.Player.UpdateTobiraTrophyStates();
      if (!string.IsNullOrEmpty(FlowNode_Variable.Get("COMPLETE_QUEST_MISSION")))
        MonoSingleton<GameManager>.Instance.Player.UpdateCompleteAllQuestCountTrophy2((QuestParam) null);
      else
        MonoSingleton<GameManager>.Instance.Player.UpdateCompleteAllQuestCountTrophy((QuestParam) null);
      MonoSingleton<GameManager>.Instance.Player.CheckAllCompleteMissionTrophy();
    }

    public void OnSoubiSet(string unitID, int countUp = 1)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.unitequip);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == unitID)
          this.AddTrophyCounter(trophyObjective, countUp);
      }
    }

    public void OnLimitBreak(string unitID, int delta = 1)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.limitbreak);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == unitID)
          this.AddTrophyCounter(trophyObjective, delta);
      }
    }

    public void OnJobChange(string unitID)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.changejob);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == unitID)
          this.AddTrophyCounter(trophyObjective, 1);
      }
    }

    public void OnChangeAbilitySet(string unitID)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.changeability);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == unitID)
          this.AddTrophyCounter(trophyObjective, 1);
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
          this.AddTrophyCounter(trophyObjective, num);
        }
        else
        {
          char[] chArray = new char[1]{ ',' };
          string[] strArray = trophyObjective.sval_base.Split(chArray);
          if ((string.IsNullOrEmpty(strArray[1]) || itemID == strArray[1]) && (string.IsNullOrEmpty(strArray[0]) || shopID == strArray[0]))
            this.AddTrophyCounter(trophyObjective, num);
        }
      }
    }

    public void OnArtifactTransmute(string artifactID)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.artifacttransmute);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == artifactID)
          this.AddTrophyCounter(trophyObjective, 1);
      }
    }

    public void OnArtifactStrength(string artifactID, int useItemNum, int beforeLevel, int currentLevel)
    {
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.artifactstrength);
      for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType1[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == artifactID)
          this.AddTrophyCounter(trophyObjective, useItemNum);
      }
      int num = currentLevel - beforeLevel;
      if (num >= 1)
      {
        TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.upartifactlevel);
        for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
        {
          TrophyObjective trophyObjective = trophiesOfType2[index];
          if (string.IsNullOrEmpty(trophyObjective.sval_base) || string.Equals(trophyObjective.sval_base, artifactID))
            this.AddTrophyCounter(trophyObjective, num);
        }
      }
      TrophyObjective[] trophiesOfType3 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.makeartifactlevel);
      for (int index = trophiesOfType3.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType3[index];
        if (currentLevel >= trophyObjective.ival && (string.IsNullOrEmpty(trophyObjective.sval_base) || string.Equals(trophyObjective.sval_base, artifactID)))
          this.SetTrophyCounter(trophyObjective, currentLevel);
      }
    }

    public void OnArtifactEvolution(string artifactID)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.artifactevolution);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base) || trophyObjective.sval_base == artifactID)
          this.AddTrophyCounter(trophyObjective, 1);
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
          if (string.Equals(trophyObjective.sval_base, unitData.UnitParam.iname))
            this.SetTrophyCounter(trophyObjective, 1);
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

    private void SetSinsTobiraTrophyByAllUnit(TobiraParam.Category category, TrophyConditionTypes trophyType)
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
          this.SetTrophyCounter(trophyObjective, num);
      }
    }

    public List<TobiraParam.Category> GetUnlockTobiraCategorys(UnitData unitData)
    {
      List<TobiraParam.Category> categoryList = new List<TobiraParam.Category>();
      if (unitData.CheckTobiraIsUnlocked(TobiraParam.Category.Envy))
        categoryList.Add(TobiraParam.Category.Envy);
      if (unitData.CheckTobiraIsUnlocked(TobiraParam.Category.Sloth))
        categoryList.Add(TobiraParam.Category.Sloth);
      if (unitData.CheckTobiraIsUnlocked(TobiraParam.Category.Lust))
        categoryList.Add(TobiraParam.Category.Lust);
      if (unitData.CheckTobiraIsUnlocked(TobiraParam.Category.Wrath))
        categoryList.Add(TobiraParam.Category.Wrath);
      if (unitData.CheckTobiraIsUnlocked(TobiraParam.Category.Greed))
        categoryList.Add(TobiraParam.Category.Greed);
      if (unitData.CheckTobiraIsUnlocked(TobiraParam.Category.Gluttony))
        categoryList.Add(TobiraParam.Category.Gluttony);
      if (unitData.CheckTobiraIsUnlocked(TobiraParam.Category.Pride))
        categoryList.Add(TobiraParam.Category.Pride);
      return categoryList;
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
        this.SetTrophyCounter(trophiesOfType[index], num);
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
          else if (string.Equals(trophyObjective.sval_base, unitData.UnitParam.iname))
            this.SetTrophyCounter(trophyObjective, 1);
        }
      }
    }

    public void OnMixedConceptCard(string conceptCardID, int beforeLevel, int currentLevel, int beforeAwakeCount, int currentAwakeCount, int beforeTrust, int currentTrust)
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

    public void UpdateConceptCardLevelupTrophy(string conceptCardID, int beforeLevel, int currentLevel)
    {
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.up_conceptcard_level);
      int num = currentLevel - beforeLevel;
      if (num >= 1)
      {
        for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
          this.AddTrophyCounter(trophiesOfType1[index], num);
      }
      TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.up_conceptcard_level_target);
      for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType2[index];
        if (string.Equals(trophyObjective.sval_base, conceptCardID))
        {
          TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(trophyObjective.Param, false);
          if (trophyCounter != null && trophyCounter.Count.Length > 0 && trophyCounter.Count[0] <= currentLevel)
            this.SetTrophyCounter(trophyObjective, currentLevel);
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
          if (string.Equals(trophyObjective.sval_base, this.ConceptCards[index2].Param.iname))
          {
            TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(trophyObjective.Param, false);
            if (trophyCounter != null && trophyCounter.Count.Length > 0 && trophyCounter.Count[0] <= (int) this.ConceptCards[index2].Lv)
              this.SetTrophyCounter(trophyObjective, (int) this.ConceptCards[index2].Lv);
          }
        }
      }
    }

    public void UpdateConceptCardLimitBreakTrophy(string conceptCardID, int beforeLimitBreak, int currentLimitBreak)
    {
      if (currentLimitBreak <= 0)
        return;
      int num = currentLimitBreak - beforeLimitBreak;
      if (num >= 1)
      {
        TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.limitbreak_conceptcard);
        for (int index = trophiesOfType.Length - 1; index >= 0; --index)
          this.AddTrophyCounter(trophiesOfType[index], num);
      }
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.limitbreak_conceptcard_target);
      for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType1[index];
        if (!string.IsNullOrEmpty(trophyObjective.sval_base) && string.Equals(trophyObjective.sval_base, conceptCardID))
        {
          TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(trophyObjective.Param, false);
          if (trophyCounter != null && trophyCounter.Count.Length > 0 && trophyCounter.Count[0] <= currentLimitBreak)
            this.SetTrophyCounter(trophyObjective, currentLimitBreak);
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
          if (string.Equals(trophyObjective.sval_base, this.ConceptCards[index2].Param.iname))
          {
            TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(trophyObjective.Param, false);
            if (trophyCounter != null && trophyCounter.Count.Length > 0 && trophyCounter.Count[0] <= (int) this.ConceptCards[index2].AwakeCount)
              this.SetTrophyCounter(trophyObjective, (int) this.ConceptCards[index2].AwakeCount);
          }
        }
      }
    }

    public void UpdateConceptCardTrustUpTrophy(string conceptCardID, int beforeTrust, int currentTrust)
    {
      if (currentTrust == 0)
        return;
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.up_conceptcard_trust);
      int num = currentTrust - beforeTrust;
      if (num >= 1)
      {
        for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
          this.AddTrophyCounter(trophiesOfType1[index], num);
      }
      TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.up_conceptcard_trust_target);
      for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType2[index];
        if (!string.IsNullOrEmpty(trophyObjective.sval_base) && string.Equals(trophyObjective.sval_base, conceptCardID))
        {
          TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(trophyObjective.Param, false);
          if (trophyCounter != null && trophyCounter.Count.Length > 0 && trophyCounter.Count[0] <= currentTrust)
            this.SetTrophyCounter(trophyObjective, currentTrust);
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
          if (string.Equals(trophyObjective.sval_base, this.ConceptCards[index2].Param.iname))
          {
            TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(trophyObjective.Param, false);
            if (trophyCounter != null && trophyCounter.Count.Length > 0 && trophyCounter.Count[0] <= (int) this.ConceptCards[index2].Trust)
              this.SetTrophyCounter(trophyObjective, (int) this.ConceptCards[index2].Trust);
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
          this.AddTrophyCounter(trophyObjective, 1);
        if (!string.IsNullOrEmpty(trophyObjective.sval_base) && string.Equals(trophyObjective.sval_base, conceptCardID))
          this.AddTrophyCounter(trophyObjective, 1);
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
          TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(trophyObjective.Param, false);
          if (trophyCounter != null && trophyCounter.Count.Length > 0 && trophyCounter.Count[0] <= num)
            this.SetTrophyCounter(trophyObjective, num);
        }
        else
        {
          int num = 0;
          for (int index2 = 0; index2 < this.ConceptCards.Count; ++index2)
          {
            if (trophyObjective.sval_base == this.ConceptCards[index2].Param.iname && (int) this.ConceptCards[index2].Trust >= cardTrustMax)
              ++num;
          }
          TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(trophyObjective.Param, false);
          if (trophyCounter != null && trophyCounter.Count.Length > 0 && trophyCounter.Count[0] <= num)
            this.SetTrophyCounter(trophyObjective, num);
        }
      }
    }

    public void UpdateSendFriendPresentTrophy()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.send_present);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        this.AddTrophyCounter(trophiesOfType[index], 1);
    }

    public void UpdateClearOrdealTrophy(BattleCore.Record record, QuestTypes questType, string questIname)
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
            this.AddTrophyCounter(trophyObjective, 1);
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
          this.SetTrophyCounter(trophyObjective, num);
        }
      }
      else
      {
        for (int index = trophiesOfType.Length - 1; index >= 0; --index)
          this.AddTrophyCounter(trophiesOfType[index], 1);
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
            this.AddTrophyCounter(trophyObjective, 1);
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
                  this.AddTrophyCounter(trophyObjective, 1);
                  break;
                }
              }
            }
            else
              this.AddTrophyCounter(trophyObjective, 1);
          }
        }
      }
      else
      {
        TrophyObjective[] trophiesOfType = instance.GetTrophiesOfType(type);
        for (int index1 = trophiesOfType.Length - 1; index1 >= 0; --index1)
        {
          int num = 0;
          TrophyObjective trophyObjective = trophiesOfType[index1];
          if (trophyObjective.sval != null && trophyObjective.sval.Count > 0)
          {
            for (int index2 = 0; index2 < instance.Quests.Length; ++index2)
            {
              if (questTypes == instance.Quests[index2].type && instance.Quests[index2].IsMissionCompleteALL() && instance.Quests[index2].Chapter != null)
              {
                for (int index3 = 0; index3 < trophyObjective.sval.Count; ++index3)
                {
                  if (trophyObjective.sval[index3] == instance.Quests[index2].Chapter.iname)
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
            for (int index2 = 0; index2 < instance.Quests.Length; ++index2)
            {
              if (instance.Quests[index2].type == questTypes && instance.Quests[index2].IsMissionCompleteALL())
                ++num;
            }
          }
          this.SetTrophyCounter(trophyObjective, num);
        }
      }
    }

    public void UpdateViewNewsTrophy(string url)
    {
      if (!url.Contains(Network.NewsHost))
        return;
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.view_news);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        this.AddTrophyCounter(trophiesOfType[index], 1);
    }

    public void RecordAllCompleteCheck(TrophyCategoryParam category)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.view_news);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        this.AddTrophyCounter(trophiesOfType[index], 1);
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

    private void UpdateCompleteMissionCount2(TrophyConditionTypes type, QuestParam quest = null, CompleteQuestMap completeQuestMap = null)
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
                  this.AddTrophyCounter(trophyObjective, 1);
                  break;
                }
              }
            }
            else
              this.AddTrophyCounter(trophyObjective, 1);
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
            this.SetTrophyCounter(trophiesOfType[index], allCount);
        }
        else
        {
          for (int index1 = trophiesOfType.Length - 1; index1 >= 0; --index1)
          {
            TrophyObjective trophyObjective = trophiesOfType[index1];
            int num1 = 0;
            if (!string.IsNullOrEmpty(trophyObjective.sval_base))
            {
              for (int index2 = 0; index2 < trophyObjective.sval.Count; ++index2)
              {
                CompleteQuestMap.CompleteQuestData completeQuestData;
                completeQuestMap.mChapterMap.TryGetValue(trophyObjective.sval[index2], out completeQuestData);
                if (completeQuestData != null)
                {
                  if (key != completeQuestData.mQuestType)
                    DebugUtility.LogError("「" + trophyObjective.Param.iname + "」に指定されたチャプター「" + trophyObjective.sval[index2] + "」は指定のクエストタイプに存在しません。");
                  num1 += completeQuestData.mCount;
                }
              }
              this.SetTrophyCounter(trophyObjective, num1);
            }
            else
            {
              int num2 = 0;
              completeQuestMap.mQuestTypeMap.TryGetValue(key, out num2);
              int num3 = num1 + num2;
              this.SetTrophyCounter(trophyObjective, num3);
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
          this.AddTrophyCounter(trophy, 0, 1);
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
          this.SetTrophyCounter(trophyObjective, num);
      }
    }

    public void OnReadTips(string trophyIname)
    {
      foreach (TrophyObjective trophyObjective in MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.read_tips))
      {
        if (!(trophyObjective.sval_base != trophyIname))
          this.AddTrophyCounter(trophyObjective, 1);
      }
      foreach (TrophyObjective trophyObjective in MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.read_tips_count))
        this.AddTrophyCounter(trophyObjective, 1);
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
        TrophyState trophyCounter = this.GetTrophyCounter(trophyObjective.Param, false);
        if (trophyCounter != null && !trophyCounter.IsCompleted)
          this.AddTrophyCounter(trophyObjective, 1);
      }
    }

    private void ResetPrevCheckHour()
    {
      this.mPrevCheckHour = -1;
    }

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
        TrophyState trophyCounter = this.GetTrophyCounter(trophyObjective.Param, false);
        if (trophyCounter != null && !trophyCounter.IsCompleted)
        {
          int num1 = int.Parse(trophyObjective.sval_base.Substring(0, 2));
          int num2 = int.Parse(trophyObjective.sval_base.Substring(3, 2));
          if (num1 <= hour && hour < num2)
            this.AddTrophyCounter(trophyObjective, 1);
          if (mealHours != null)
          {
            for (int index2 = 0; index2 < mealHours.Count; ++index2)
            {
              if (num1 <= mealHours[index2] && mealHours[index2] < num2)
                this.AddTrophyCounter(trophyObjective, 1);
            }
          }
        }
      }
    }

    public void UpdateArtifactTrophyStates()
    {
      if (this.mArtifacts.Count < 1)
        return;
      int a = 1;
      Dictionary<string, ArtifactData> dictionary = new Dictionary<string, ArtifactData>();
      for (int index = 0; index < this.mArtifacts.Count; ++index)
      {
        ArtifactData mArtifact = this.mArtifacts[index];
        if (mArtifact != null)
        {
          a = Mathf.Max(a, (int) mArtifact.Lv);
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
          this.SetTrophyCounter(trophiesOfType[index], a);
        else if (dictionary.ContainsKey(trophiesOfType[index].sval_base))
          this.SetTrophyCounter(trophiesOfType[index], (int) dictionary[trophiesOfType[index].sval_base].Lv);
      }
    }

    public void UpdatePlayerTrophyStates()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.playerlv);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (this.Lv >= trophyObjective.ival)
          this.AddTrophyCounter(trophyObjective, 1);
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
          this.SetTrophyCounter(trophyObjective, trophyObjective.ival);
      }
      TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.overarenarank);
      for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType2[index];
        if (bestRank <= trophyObjective.ival)
          this.SetTrophyCounter(trophyObjective, bestRank);
      }
    }

    public void UpdateTowerTrophyStates()
    {
      this.OnTowerScore(false);
    }

    public void UpdateVersusTowerTrophyStates(string towerName, int currentFloor)
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.overvsrankfloor);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if ((string.IsNullOrEmpty(trophyObjective.sval_base) || string.Equals(trophyObjective.sval_base, towerName)) && currentFloor >= trophyObjective.ival)
          this.SetTrophyCounter(trophyObjective, currentFloor);
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

    public bool ItemEntryExists(string iname)
    {
      return this.mID2ItemData.ContainsKey(iname);
    }

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
        if (this.mUnits[index1].Jobs != null)
        {
          for (int index2 = 0; index2 < this.mUnits[index1].Jobs.Length; ++index2)
          {
            if (this.mUnits[index1].Jobs[index2] != null && this.mUnits[index1].Jobs[index2].ArtifactDatas != null)
            {
              for (int index3 = 0; index3 < this.mUnits[index1].Jobs[index2].ArtifactDatas.Length; ++index3)
                this.mUnits[index1].Jobs[index2].ArtifactDatas[index3] = (ArtifactData) null;
            }
          }
        }
      }
      for (int index = 0; index < this.mArtifacts.Count; ++index)
      {
        ArtifactData mArtifact = this.mArtifacts[index];
        if (mArtifact != null && (long) mArtifact.UniqueID != 0L)
        {
          UnitData unit = (UnitData) null;
          JobData job = (JobData) null;
          if (this.FindOwner(mArtifact, out unit, out job))
          {
            int job_index = Array.IndexOf<JobData>(unit.Jobs, job);
            if (job_index != -1)
            {
              for (int slot = 0; slot < job.Artifacts.Length; ++slot)
              {
                if ((long) mArtifact.UniqueID == job.Artifacts[slot])
                {
                  unit.SetEquipArtifactData(job_index, slot, mArtifact, unit.JobIndex == job_index);
                  if (unit.JobIndex != job_index)
                  {
                    unit.UpdateArtifact(unit.JobIndex, true, false);
                    break;
                  }
                  break;
                }
              }
            }
          }
        }
      }
    }

    public bool IsBeginner()
    {
      return (double) (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.BeginnerDays > new TimeSpan(TimeManager.FromUnixTime(Network.GetServerTime()).Ticks).TotalDays - new TimeSpan(TimeManager.FromUnixTime((long) (int) this.mNewGameAt).Ticks).TotalDays;
    }

    public DateTime GetBeginnerEndTime()
    {
      return TimeManager.FromUnixTime((long) (int) this.mNewGameAt).AddDays((double) (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.BeginnerDays);
    }

    public Dictionary<ItemParam, int> CreateItemSnapshot()
    {
      Dictionary<ItemParam, int> dictionary = new Dictionary<ItemParam, int>();
      for (int index = 0; index < this.mItems.Count; ++index)
        dictionary[this.mItems[index].Param] = this.mItems[index].NumNonCap;
      return dictionary;
    }

    public void GainPiecePoint(int point)
    {
      this.mPiecePoint = (OInt) Math.Max((int) this.mPiecePoint + point, 0);
    }

    public string DequeueNextLoginBonusTableID()
    {
      if (this.mLoginBonusQueue.Count < 1)
        return (string) null;
      return this.mLoginBonusQueue.Dequeue();
    }

    public bool HasQueuedLoginBonus
    {
      get
      {
        return this.mLoginBonusQueue.Count > 0;
      }
    }

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
          this.AddTrophyCounter(trophiesOfType1[index], 1);
      }
      TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.totaljoblv11);
      for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
      {
        if (trophiesOfType2[index].ival <= num3)
          this.AddTrophyCounter(trophiesOfType2[index], 1);
      }
      TrophyObjective[] trophiesOfType3 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.totalunitlvs);
      for (int index = trophiesOfType3.Length - 1; index >= 0; --index)
      {
        if (trophiesOfType3[index].ival <= num1)
          this.AddTrophyCounter(trophiesOfType3[index], 1);
      }
      TrophyObjective[] trophiesOfType4 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.evoltiontimes);
      for (int index = trophiesOfType4.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType4[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base))
          this.SetTrophyCounter(trophyObjective, num4);
      }
      TrophyObjective[] trophiesOfType5 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.limitbreak);
      for (int index = trophiesOfType5.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType5[index];
        if (string.IsNullOrEmpty(trophyObjective.sval_base))
          this.SetTrophyCounter(trophyObjective, num5);
      }
      if (!verbose)
        return;
      for (int index1 = 0; index1 < this.mUnits.Count; ++index1)
      {
        UnitData mUnit = this.mUnits[index1];
        if (mUnit == null || mUnit.UnitParam == null)
          break;
        string iname = mUnit.UnitParam.iname;
        this.OnUnitLevelChange(iname, 0, mUnit.Lv, true);
        JobData[] jobs = mUnit.Jobs;
        if (jobs != null)
        {
          for (int index2 = 0; index2 < jobs.Length; ++index2)
            this.OnJobLevelChange(iname, jobs[index2].JobID, jobs[index2].Rank, true, 1);
        }
        this.OnUnitLevelAndJobLevelChange(iname, mUnit.Lv, mUnit.Jobs);
        List<AbilityData> learnAbilitys = mUnit.LearnAbilitys;
        for (int index2 = 0; index2 < learnAbilitys.Count; ++index2)
          this.OnAbilityPowerUp(iname, learnAbilitys[index2].AbilityID, learnAbilitys[index2].Rank, true);
        if (mUnit.Rarity > (int) mUnit.UnitParam.rare)
          this.OnEvolutionCheck(iname, mUnit.Rarity, (int) mUnit.UnitParam.rare);
        this.OnLimitBreakCheck(iname, mUnit.AwakeLv);
      }
    }

    public void OnEvolutionCheck(string unitID, int rarity, int initialRarity)
    {
      TrophyObjective[] trophiesOfType1 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.evolutionnum);
      for (int index = trophiesOfType1.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType1[index];
        if (trophyObjective.sval_base == unitID && trophyObjective.ival <= rarity)
          this.AddTrophyCounter(trophyObjective, 1);
      }
      int num = rarity - initialRarity;
      if (num < 1)
        return;
      TrophyObjective[] trophiesOfType2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.evoltiontimes);
      for (int index = trophiesOfType2.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType2[index];
        if (!string.IsNullOrEmpty(trophyObjective.sval_base) && trophyObjective.sval_base == unitID)
          this.SetTrophyCounter(trophyObjective, num);
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
          this.SetTrophyCounter(trophyObjective, awake);
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
      if (eventCoinData != null && eventCoinData.have != null)
        return eventCoinData.have.Num;
      return 0;
    }

    public void SetEventCoinNum(string cost_iname, int num)
    {
      if (cost_iname == null)
        return;
      MonoSingleton<GameManager>.Instance.Player.Items.Find((Predicate<ItemData>) (f => f.Param.iname.Equals(cost_iname)))?.SetNum(num);
    }

    public void SetVersusPlacement(string key, int idx)
    {
      PlayerPrefsUtility.SetInt(key, idx, false);
    }

    public int GetVersusPlacement(string key)
    {
      return PlayerPrefsUtility.GetInt(key, 0);
    }

    public void SetTowerMatchInfo(int floor, int key, int wincnt, bool gift)
    {
      this.mVersusTwFloor = floor;
      this.mVersusTwKey = key;
      this.mVersusTwWinCnt = wincnt;
      this.mVersusSeasonGift = gift;
    }

    public void SetRankMatchInfo(int _rank, int _score, RankMatchClass _class, int _battle_point, int _streak_win, int _wincnt, int _losecnt)
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
      if (this.mHaveAward == null)
        return false;
      return this.mHaveAward.Contains(award);
    }

    public void UpdateAchievementTrophyStates()
    {
      if (this.mTrophyStatesInameDict == null)
        return;
      List<AchievementParam> achievementData = GameCenterManager.GetAchievementData();
      if (achievementData == null || achievementData.Count < 1)
        return;
      for (int index = 0; index < achievementData.Count; ++index)
      {
        AchievementParam achievementParam = achievementData[index];
        List<TrophyState> trophyStateList;
        if (this.mTrophyStatesInameDict.TryGetValue(achievementParam.iname, out trophyStateList) && trophyStateList[0].IsCompleted)
          GameCenterManager.SendAchievementProgress(achievementParam);
      }
    }

    public void SetWishList(string iname, int priority)
    {
      this.FriendPresentWishList.Set(iname, priority);
    }

    public void SetQuestListDirty()
    {
      this.mQuestListDirty = true;
    }

    public List<ConceptCardData> ConceptCards
    {
      get
      {
        return this.mConceptCards;
      }
    }

    public List<ConceptCardMaterialData> ConceptCardExpMaterials
    {
      get
      {
        return this.mConceptCardExpMaterials;
      }
    }

    public List<ConceptCardMaterialData> ConceptCardTrustMaterials
    {
      get
      {
        return this.mConceptCardTrustMaterials;
      }
    }

    public List<SkinConceptCardData> SkinConceptCards
    {
      get
      {
        return this.mSkinConceptCards;
      }
    }

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
      for (int i = 0; i < iids.Length; ++i)
      {
        UnitData unitData = this.Units.Find((Predicate<UnitData>) (ud =>
        {
          if (ud.ConceptCard != null)
            return (long) ud.ConceptCard.UniqueID == iids[i];
          return false;
        }));
        if (unitData != null)
          unitData.ConceptCard = (ConceptCardData) null;
      }
      this.UpdateConceptCardNum();
    }

    public void UpdateConceptCardNum()
    {
      this.mConceptCardNum.Clear();
      for (int index1 = 0; index1 < this.mConceptCards.Count; ++index1)
      {
        string iname = this.mConceptCards[index1].Param.iname;
        if (this.mConceptCardNum.ContainsKey(iname))
        {
          Dictionary<string, int> mConceptCardNum;
          string index2;
          (mConceptCardNum = this.mConceptCardNum)[index2 = iname] = mConceptCardNum[index2] + 1;
        }
        else
          this.mConceptCardNum.Add(iname, 1);
      }
    }

    public void UpdateConceptCardNum(string[] inames)
    {
      this.mConceptCardNum.Clear();
      for (int index1 = 0; index1 < inames.Length; ++index1)
      {
        string iname = inames[index1];
        if (this.mConceptCardNum.ContainsKey(iname))
        {
          Dictionary<string, int> mConceptCardNum;
          string index2;
          (mConceptCardNum = this.mConceptCardNum)[index2 = iname] = mConceptCardNum[index2] + 1;
        }
        else
          this.mConceptCardNum.Add(iname, 1);
      }
    }

    public int GetConceptCardNum(string iname)
    {
      int num = 0;
      this.mConceptCardNum.TryGetValue(iname, out num);
      return num;
    }

    public int GetConceptCardMaterialNum(string iname)
    {
      int num = 0;
      ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(iname);
      if (conceptCardParam == null)
        return num;
      ConceptCardMaterialData cardMaterialData = (ConceptCardMaterialData) null;
      if (conceptCardParam.type == eCardType.Enhance_exp)
        cardMaterialData = this.mConceptCardExpMaterials.Find((Predicate<ConceptCardMaterialData>) (p => (string) p.IName == iname));
      else if (conceptCardParam.type == eCardType.Enhance_trust)
        cardMaterialData = this.mConceptCardTrustMaterials.Find((Predicate<ConceptCardMaterialData>) (p => (string) p.IName == iname));
      if (cardMaterialData != null)
        num = (int) cardMaterialData.Num;
      return num;
    }

    public OLong GetConceptCardMaterialUniqueID(string iname)
    {
      OLong olong = (OLong) -1L;
      ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(iname);
      if (conceptCardParam == null)
        return olong;
      ConceptCardMaterialData cardMaterialData = (ConceptCardMaterialData) null;
      if (conceptCardParam.type == eCardType.Enhance_exp)
        cardMaterialData = this.mConceptCardExpMaterials.Find((Predicate<ConceptCardMaterialData>) (p => (string) p.IName == iname));
      else if (conceptCardParam.type == eCardType.Enhance_trust)
        cardMaterialData = this.mConceptCardTrustMaterials.Find((Predicate<ConceptCardMaterialData>) (p => (string) p.IName == iname));
      if (cardMaterialData != null)
        olong = cardMaterialData.UniqueID;
      return olong;
    }

    public int GetEnhanceConceptCardMaterial()
    {
      int num = 0;
      if (this.mConceptCardExpMaterials != null)
        num += this.mConceptCardExpMaterials.Count;
      if (this.mConceptCardTrustMaterials != null)
        num += this.mConceptCardTrustMaterials.Count;
      return num;
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
      this.mLoginBonusTables[loginbonus.type] = loginbonus;
      this.mPremiumLoginBonus = loginbonus;
      return true;
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

    private class JSON_TrophyState
    {
      public string id = string.Empty;
      public int[] cnt;
      public long st;
      public int fin;
    }
  }
}
