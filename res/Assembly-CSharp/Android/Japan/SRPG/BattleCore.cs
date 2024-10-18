﻿// Decompiled with JetBrains decompiler
// Type: SRPG.BattleCore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SRPG
{
  public class BattleCore
  {
    public static readonly int MAX_MAP = 3;
    public static readonly int MAX_PARTY = 7;
    public static readonly int MAX_ENEMY = 16;
    public static readonly int MAX_ORDER = BattleCore.MAX_PARTY + BattleCore.MAX_ENEMY;
    public static readonly int MAX_UNITS = BattleCore.MAX_PARTY + BattleCore.MAX_ENEMY;
    public static readonly int MAX_GEMS = 99;
    private static BaseStatus BuffWorkStatus = new BaseStatus();
    private static BaseStatus DebuffWorkStatus = new BaseStatus();
    private static BaseStatus BuffNegativeWorkStatus = new BaseStatus();
    private static BaseStatus DebuffNegativeWorkStatus = new BaseStatus();
    private static BaseStatus BuffWorkScaleStatus = new BaseStatus();
    private static BaseStatus DebuffWorkScaleStatus = new BaseStatus();
    private static IntVector2[] mBigUnitOfsPos5x5 = new IntVector2[25]{ new IntVector2(0, 0), new IntVector2(0, 1), new IntVector2(1, 1), new IntVector2(1, 0), new IntVector2(1, -1), new IntVector2(0, -1), new IntVector2(-1, -1), new IntVector2(-1, 0), new IntVector2(-1, 1), new IntVector2(-1, 2), new IntVector2(0, 2), new IntVector2(1, 2), new IntVector2(2, 2), new IntVector2(2, 1), new IntVector2(2, 0), new IntVector2(2, -1), new IntVector2(2, -2), new IntVector2(1, -2), new IntVector2(0, -2), new IntVector2(-1, -2), new IntVector2(-2, -2), new IntVector2(-2, -1), new IntVector2(-2, 0), new IntVector2(-2, 1), new IntVector2(-2, 2) };
    private static BaseStatus BuffAagWorkStatus = new BaseStatus();
    private static BaseStatus DebuffAagWorkStatus = new BaseStatus();
    private static BaseStatus BuffAagNegativeWorkStatus = new BaseStatus();
    private static BaseStatus DebuffAagNegativeWorkStatus = new BaseStatus();
    private static BaseStatus AagWorkStatus = new BaseStatus();
    private static List<BattleCore.SkillResult> mSkillResults = new List<BattleCore.SkillResult>();
    private QuestParam mQuestParam = new QuestParam();
    public int[] mActionCounts = new int[2];
    private Dictionary<string, int> mTargetKillstreakDict = new Dictionary<string, int>();
    private Dictionary<string, int> mMaxTargetKillstreakDict = new Dictionary<string, int>();
    private List<Unit> mAllUnits = new List<Unit>(BattleCore.MAX_UNITS);
    private List<Unit> mUnits = new List<Unit>(BattleCore.MAX_UNITS);
    private List<Unit> mPlayer = new List<Unit>(BattleCore.MAX_PARTY);
    private OInt mLeaderIndex = (OInt) -1;
    private OInt mEnemyLeaderIndex = (OInt) -1;
    private OInt mFriendIndex = (OInt) -1;
    private List<int> mMtLeaderIndexList = new List<int>();
    private List<Unit> mStartingMembers = new List<Unit>();
    private List<Unit> mHelperUnits = new List<Unit>(BattleCore.MAX_ENEMY);
    private List<BattleMap> mMap = new List<BattleMap>(BattleCore.MAX_MAP);
    private OInt mClockTime = (OInt) 0;
    private OInt mClockTimeTotal = (OInt) 0;
    public ItemData[] mInventory = new ItemData[5];
    private List<BattleCore.OrderData> mOrder = new List<BattleCore.OrderData>(BattleCore.MAX_ORDER);
    private BattleLogServer mLogs = new BattleLogServer();
    private string mUniqueKey = string.Empty;
    private RandXorshift mRand = new RandXorshift(nameof (mRand));
    private RandXorshift mRandDamage = new RandXorshift(nameof (mRandDamage));
    private Dictionary<string, BattleCore.SkillExecLog> mSkillExecLogs = new Dictionary<string, BattleCore.SkillExecLog>();
    private BattleCore.Record mRecord = new BattleCore.Record();
    private List<Unit> mTreasures = new List<Unit>();
    private List<Unit> mGems = new List<Unit>();
    private List<GimmickEvent> mGimmickEvents = new List<GimmickEvent>();
    private uint mArenaActionCountMax = 25;
    private List<Unit> sameJudgeUnitLists = new List<Unit>();
    private List<BuffAttachment> AagBuffAttachmentLists = new List<BuffAttachment>();
    private List<Unit> AagTargetLists = new List<Unit>();
    private SkillMap mSkillMap = new SkillMap();
    private TrickMap mTrickMap = new TrickMap();
    public const int DTU_NEST_MAX = 256;
    private long mBtlID;
    private int mBtlFlags;
    private int mWinTriggerCount;
    private int mLoseTriggerCount;
    private int mKillstreak;
    private int mMaxKillstreak;
    private bool mPlayByManually;
    private bool mIsUseAutoPlayMode;
    private int mTotalHeal;
    private int mTotalDamagesTaken;
    private int mTotalDamages;
    private int mTotalDamagesTakenCount;
    private bool mIsDamageTakenCurrentSkill;
    private int mMaxDamage;
    private int mNumUsedItems;
    private int mNumUsedSkills;
    private int mNpcStartIndex;
    private int mEntryUnitMax;
    private List<Unit>[] mEnemys;
    private FriendStates mFriendStates;
    private int mMapIndex;
    private int mContinueCount;
    private int mCurrentTeamId;
    private int mMaxTeamId;
    private string mFinisherIname;
    private uint mSeed;
    private uint mSeedDamage;
    private RandXorshift CurrentRand;
    private List<Grid> mGridLines;
    private string[] mQuestCampaignIds;
    private RankingQuestParam mRankingQuestParam;
    private int mMyPlayerIndex;
    private bool mMultiFinishLoad;
    private BattleCore.RESUME_STATE mResumeState;
    public BattleCore.LogCallback LogHandler;
    public BattleCore.LogCallback WarningHandler;
    public BattleCore.LogCallback ErrorHandler;
    public bool IsUnitRestart;
    public bool[] EventTriggers;
    private bool mIsArenaSkip;
    private uint mArenaActionCount;
    private string mArenaQuestID;
    private BattleCore.Json_Battle mArenaQuestJsonBtl;
    private bool mIsArenaCalc;
    private BattleCore.QuestResult mArenaCalcResult;
    private BattleCore.eArenaCalcType mArenaCalcTypeNext;
    private bool IsEnableAagBuff;
    private bool IsEnableAagDebuff;
    private bool IsEnableAagBuffNegative;
    private bool IsEnableAagDebuffNegative;
    private List<Unit> mEnemyPriorities;
    private List<Unit> mGimmickPriorities;
    private GridMap<int> mMoveMap;
    private GridMap<bool> mRangeMap;
    private GridMap<bool> mScopeMap;
    private GridMap<bool> mSearchMap;
    private GridMap<int> mSafeMap;
    private GridMap<int> mSafeMapEx;

    public string UniqueKey
    {
      get
      {
        return this.mUniqueKey;
      }
    }

    public RandXorshift CloneRand()
    {
      return this.mRand.Clone();
    }

    public RandXorshift CloneRandDamage()
    {
      RandXorshift randXorshift = this.mRandDamage.Clone();
      randXorshift?.Seed(this.mSeedDamage);
      return randXorshift;
    }

    public uint Seed
    {
      get
      {
        return this.mSeed;
      }
      set
      {
        this.mSeed = value;
      }
    }

    public RandXorshift Rand
    {
      get
      {
        return this.mRand;
      }
    }

    public uint DamageSeed
    {
      get
      {
        return this.mSeedDamage;
      }
      set
      {
        this.mSeedDamage = value;
      }
    }

    public void SetRandSeed(int index, uint seed)
    {
      this.mRand.SetSeed(index, seed);
    }

    public void SetRandDamageSeed(int index, uint seed)
    {
      this.mRandDamage.SetSeed(index, seed);
    }

    public Dictionary<string, BattleCore.SkillExecLog> SkillExecLogs
    {
      get
      {
        return this.mSkillExecLogs;
      }
    }

    public bool SyncStart { get; set; }

    public int MyPlayerIndex
    {
      get
      {
        return this.mMyPlayerIndex;
      }
    }

    public bool IsMultiPlay { get; private set; }

    public bool IsMultiVersus { get; private set; }

    public bool IsRankMatch { get; private set; }

    public bool IsMultiTower { get; private set; }

    public bool IsTower
    {
      get
      {
        if (this.mQuestParam != null)
          return this.mQuestParam.type == QuestTypes.Tower;
        return false;
      }
    }

    public bool IsOrdeal
    {
      get
      {
        if (this.mQuestParam != null)
          return this.mQuestParam.type == QuestTypes.Ordeal;
        return false;
      }
    }

    public bool IsRaidQuest
    {
      get
      {
        if (this.mQuestParam != null)
          return this.mQuestParam.type == QuestTypes.Raid;
        return false;
      }
    }

    public bool IsGenesisStory
    {
      get
      {
        if (this.mQuestParam != null)
          return this.mQuestParam.type == QuestTypes.GenesisStory;
        return false;
      }
    }

    public bool IsGenesisBoss
    {
      get
      {
        if (this.mQuestParam != null)
          return this.mQuestParam.type == QuestTypes.GenesisBoss;
        return false;
      }
    }

    public bool IsVSForceWin { get; set; }

    public bool IsVSForceWinComfirm { get; set; }

    public bool IsRankingQuest
    {
      get
      {
        return this.mRankingQuestParam != null;
      }
    }

    public bool MultiFinishLoad
    {
      get
      {
        return this.mMultiFinishLoad;
      }
      set
      {
        this.mMultiFinishLoad = value;
      }
    }

    public BattleCore.RESUME_STATE ResumeState
    {
      get
      {
        return this.mResumeState;
      }
      set
      {
        this.mResumeState = value;
      }
    }

    public bool IsResume
    {
      get
      {
        return this.mResumeState == BattleCore.RESUME_STATE.WAIT;
      }
    }

    public void SetResumeWait()
    {
      this.mResumeState = BattleCore.RESUME_STATE.WAIT;
    }

    private void DebugAssert(bool condition, string msg)
    {
      if (!condition)
      {
        if (this.ErrorHandler != null)
          this.ErrorHandler(msg);
        throw new Exception("[Assertion Failed] " + msg);
      }
    }

    private void DebugLog(string s)
    {
      if (this.LogHandler == null)
        return;
      this.LogHandler(s);
    }

    private void DebugWarn(string s)
    {
      if (this.WarningHandler == null)
        return;
      this.WarningHandler(s);
    }

    private void DebugErr(string s)
    {
      if (this.ErrorHandler == null)
        return;
      this.ErrorHandler(s);
    }

    public string QuestID
    {
      get
      {
        if (this.mQuestParam != null)
          return this.mQuestParam.iname;
        return (string) null;
      }
    }

    public string QuestName
    {
      get
      {
        if (this.mQuestParam != null)
          return this.mQuestParam.name;
        return (string) null;
      }
    }

    public string QuestTerms
    {
      get
      {
        if (this.mQuestParam != null)
          return this.mQuestParam.cond;
        return (string) null;
      }
    }

    public QuestTypes QuestType
    {
      get
      {
        if (this.mQuestParam != null)
          return this.mQuestParam.type;
        return QuestTypes.Story;
      }
    }

    public string QuestClearEventName
    {
      get
      {
        if (this.mQuestParam != null)
          return this.mQuestParam.event_clear;
        return (string) null;
      }
    }

    public bool IsUnitChange
    {
      get
      {
        if (this.mQuestParam != null)
          return this.mQuestParam.IsUnitChange;
        return false;
      }
    }

    public bool IsMultiLeaderSkill
    {
      get
      {
        if (this.mQuestParam != null)
          return this.mQuestParam.IsMultiLeaderSkill;
        return false;
      }
    }

    public List<BattleMap> Map
    {
      get
      {
        return this.mMap;
      }
    }

    public List<Unit> AllUnits
    {
      get
      {
        return this.mAllUnits;
      }
    }

    public int NpcStartIndex
    {
      get
      {
        return this.mNpcStartIndex;
      }
    }

    public List<Unit> Units
    {
      get
      {
        return this.mUnits;
      }
    }

    public List<BattleCore.OrderData> Order
    {
      get
      {
        return this.mOrder;
      }
    }

    public List<Unit> StartingMembers
    {
      get
      {
        return this.mStartingMembers;
      }
    }

    public List<Unit> HelperUnits
    {
      get
      {
        return this.mHelperUnits;
      }
    }

    public int MapIndex
    {
      get
      {
        return this.mMapIndex;
      }
    }

    public BattleMap CurrentMap
    {
      get
      {
        if (this.mMap != null && 0 <= this.mMapIndex && this.mMapIndex < this.mMap.Count)
          return this.mMap[this.mMapIndex];
        return (BattleMap) null;
      }
    }

    public Unit CurrentUnit
    {
      get
      {
        if (this.mOrder.Count > 0)
          return this.mOrder[0].Unit;
        return (Unit) null;
      }
    }

    public BattleCore.OrderData CurrentOrderData
    {
      get
      {
        if (this.mOrder.Count > 0)
          return this.mOrder[0];
        return (BattleCore.OrderData) null;
      }
    }

    public BattleLogServer Logs
    {
      get
      {
        return this.mLogs;
      }
    }

    public List<Unit> Player
    {
      get
      {
        return this.mPlayer;
      }
    }

    public List<Unit> Enemys
    {
      get
      {
        if (this.MapIndex >= 0 && this.MapIndex < this.mEnemys.Length)
          return this.mEnemys[this.MapIndex];
        return (List<Unit>) null;
      }
    }

    public Unit Leader
    {
      get
      {
        if ((this.IsMultiPlay || (int) this.mLeaderIndex == -1) && (!this.IsMultiPlay || (int) this.mLeaderIndex == -1 || !this.mQuestParam.IsMultiLeaderSkill) && (!this.IsMultiVersus || (int) this.mLeaderIndex == -1))
          return (Unit) null;
        if (this.mPlayer != null && (int) this.mLeaderIndex >= 0 && (int) this.mLeaderIndex < this.mPlayer.Count)
          return this.mPlayer[(int) this.mLeaderIndex];
        return (Unit) null;
      }
    }

    public Unit Friend
    {
      get
      {
        if (this.IsMultiPlay || (int) this.mFriendIndex == -1)
          return (Unit) null;
        if (this.mPlayer != null && (int) this.mFriendIndex >= 0 && (int) this.mFriendIndex < this.mPlayer.Count)
          return this.mPlayer[(int) this.mFriendIndex];
        return (Unit) null;
      }
    }

    public Unit EnemyLeader
    {
      get
      {
        if ((this.IsMultiPlay || (int) this.mEnemyLeaderIndex == -1) && (!this.IsMultiVersus || (int) this.mEnemyLeaderIndex == -1))
          return (Unit) null;
        if (this.Enemys != null && (int) this.mEnemyLeaderIndex >= 0 && (int) this.mEnemyLeaderIndex < this.Enemys.Count)
          return this.Enemys[(int) this.mEnemyLeaderIndex];
        return (Unit) null;
      }
    }

    public OInt LeaderIndex
    {
      get
      {
        return this.mLeaderIndex;
      }
    }

    public OInt EnemyLeaderIndex
    {
      get
      {
        return this.mEnemyLeaderIndex;
      }
    }

    public OInt FriendIndex
    {
      get
      {
        return this.mFriendIndex;
      }
    }

    public List<int> MtLeaderIndexList
    {
      get
      {
        return this.mMtLeaderIndexList;
      }
    }

    public bool IsFriendStatus
    {
      get
      {
        if (this.Friend != null)
          return this.mFriendStates == FriendStates.Friend;
        return false;
      }
    }

    public long BtlID
    {
      get
      {
        return this.mBtlID;
      }
    }

    public int WinTriggerCount
    {
      get
      {
        return this.mWinTriggerCount;
      }
      set
      {
        this.mWinTriggerCount = value;
      }
    }

    public int LoseTriggerCount
    {
      get
      {
        return this.mLoseTriggerCount;
      }
      set
      {
        this.mLoseTriggerCount = value;
      }
    }

    public int ActionCountTotal
    {
      get
      {
        return ((IEnumerable<int>) this.mActionCounts).Sum();
      }
    }

    public int Killstreak
    {
      get
      {
        return this.mKillstreak;
      }
      set
      {
        this.mKillstreak = value;
      }
    }

    public int MaxKillstreak
    {
      get
      {
        return this.mMaxKillstreak;
      }
      set
      {
        this.mMaxKillstreak = value;
      }
    }

    public Dictionary<string, int> TargetKillstreak
    {
      get
      {
        return this.mTargetKillstreakDict;
      }
      set
      {
        this.mTargetKillstreakDict = value;
      }
    }

    public Dictionary<string, int> MaxTargetKillstreak
    {
      get
      {
        return this.mMaxTargetKillstreakDict;
      }
      set
      {
        this.mMaxTargetKillstreakDict = value;
      }
    }

    public bool PlayByManually
    {
      get
      {
        return this.mPlayByManually;
      }
      set
      {
        this.mPlayByManually = value;
      }
    }

    public bool IsUseAutoPlayMode
    {
      get
      {
        return this.mIsUseAutoPlayMode;
      }
      set
      {
        this.mIsUseAutoPlayMode = value;
      }
    }

    public int TotalHeal
    {
      get
      {
        return this.mTotalHeal;
      }
      set
      {
        this.mTotalHeal = value;
      }
    }

    public int TotalDamagesTaken
    {
      get
      {
        return this.mTotalDamagesTaken;
      }
      set
      {
        this.mTotalDamagesTaken = value;
      }
    }

    public int TotalDamages
    {
      get
      {
        return this.mTotalDamages;
      }
      set
      {
        this.mTotalDamages = value;
      }
    }

    public int TotalDamagesTakenCount
    {
      get
      {
        return this.mTotalDamagesTakenCount;
      }
      set
      {
        this.mTotalDamagesTakenCount = value;
      }
    }

    public int MaxDamage
    {
      get
      {
        return this.mMaxDamage;
      }
      set
      {
        this.mMaxDamage = value;
      }
    }

    public int NumUsedItems
    {
      get
      {
        return this.mNumUsedItems;
      }
      set
      {
        this.mNumUsedItems = value;
      }
    }

    public int NumUsedSkills
    {
      get
      {
        return this.mNumUsedSkills;
      }
      set
      {
        this.mNumUsedSkills = value;
      }
    }

    public int ClockTime
    {
      get
      {
        return (int) this.mClockTime;
      }
      set
      {
        this.mClockTime = (OInt) value;
      }
    }

    public int ClockTimeTotal
    {
      get
      {
        return (int) this.mClockTimeTotal;
      }
      set
      {
        this.mClockTimeTotal = (OInt) value;
      }
    }

    public int ContinueCount
    {
      get
      {
        return this.mContinueCount;
      }
      set
      {
        this.mContinueCount = value;
      }
    }

    public int CurrentTeamId
    {
      get
      {
        return this.mCurrentTeamId;
      }
      set
      {
        this.mCurrentTeamId = value;
      }
    }

    public int MaxTeamId
    {
      get
      {
        return this.mMaxTeamId;
      }
      set
      {
        this.mMaxTeamId = value;
      }
    }

    public string FinisherIname
    {
      get
      {
        return this.mFinisherIname;
      }
      set
      {
        this.mFinisherIname = value;
      }
    }

    public bool RequestAutoBattle { get; set; }

    public bool IsAutoBattle { get; set; }

    public void SetManualPlayFlag()
    {
      if (this.IsAutoBattle || this.CurrentUnit.Side != EUnitSide.Player)
        return;
      this.PlayByManually = true;
    }

    public bool IsSkillDirection
    {
      get
      {
        if (this.IsMultiPlay || this.IsMultiVersus || this.mQuestParam != null && this.mQuestParam.type == QuestTypes.Arena)
          return true;
        return GameUtility.Config_DirectionCut.Value;
      }
    }

    public string[] QuestCampaignIds
    {
      get
      {
        return this.mQuestCampaignIds;
      }
    }

    public List<GimmickEvent> GimmickEventList
    {
      get
      {
        return this.mGimmickEvents;
      }
    }

    ~BattleCore()
    {
      this.Release();
    }

    public void Release()
    {
      if (this.mLogs != null)
      {
        this.mLogs.Release();
        this.mLogs = (BattleLogServer) null;
      }
      if (this.mOrder != null)
      {
        this.mOrder.Clear();
        this.mOrder = (List<BattleCore.OrderData>) null;
      }
      this.mRecord = (BattleCore.Record) null;
      this.mRand = (RandXorshift) null;
      if (this.mAllUnits != null)
      {
        this.mAllUnits.Clear();
        this.mAllUnits = (List<Unit>) null;
      }
      if (this.mMap != null)
      {
        for (int index = 0; index < this.mMap.Count; ++index)
        {
          if (this.mMap[index] != null)
            this.mMap[index].Release();
        }
        this.mMap.Clear();
        this.mMap = (List<BattleMap>) null;
      }
      this.mMapIndex = 0;
      this.mEnemys = (List<Unit>[]) null;
      this.mPlayer = (List<Unit>) null;
      this.mQuestParam = (QuestParam) null;
      this.ReleaseAi();
    }

    public bool SetupMultiPlayUnit(UnitData[] units, int[] ownerPlayerIndex, int[] placementIndex, bool[] sub)
    {
      MyPhoton instance1 = PunMonoSingleton<MyPhoton>.Instance;
      GameManager instance2 = MonoSingleton<GameManager>.Instance;
      List<UnitSetting> partyUnitSettings = this.mMap[0].PartyUnitSettings;
      List<UnitSetting> arenaUnitSettings = this.mMap[0].ArenaUnitSettings;
      PlayerPartyTypes playerPartyType;
      PlayerPartyTypes enemyPartyType;
      this.mQuestParam.GetPartyTypes(out playerPartyType, out enemyPartyType);
      if (instance2.AudienceMode || instance2.IsVSCpuBattle)
      {
        this.IsMultiPlay = true;
        this.IsMultiVersus = true;
        this.IsRankMatch = false;
        this.mLeaderIndex = (OInt) 0;
        this.mEnemyLeaderIndex = (OInt) 0;
        this.VersusTurnMax = (uint) this.mQuestParam.VersusMoveCount;
        this.RemainVersusTurnCount = 0U;
        for (int index = 0; index < units.Length; ++index)
        {
          if (units[index] != null)
          {
            Unit unit = new Unit();
            if (!unit.Setup(units[index], ownerPlayerIndex[index] != 1 ? arenaUnitSettings[placementIndex[index]] : partyUnitSettings[placementIndex[index]], (Unit.DropItem) null, (Unit.DropItem) null))
            {
              this.DebugErr("failed unit Setup");
              return false;
            }
            unit.IsPartyMember = true;
            unit.SetUnitFlag(EUnitFlag.Searched, true);
            unit.OwnerPlayerIndex = ownerPlayerIndex[index];
            unit.Side = ownerPlayerIndex[index] != 1 ? EUnitSide.Enemy : EUnitSide.Player;
            if (unit.Side == EUnitSide.Player)
              this.mPlayer.Add(unit);
            this.mAllUnits.Add(unit);
            this.mStartingMembers.Add(unit);
          }
        }
      }
      else if (instance1.IsMultiVersus)
      {
        this.IsMultiVersus = instance1.IsMultiVersus;
        this.IsRankMatch = instance1.IsRankMatch;
        this.mLeaderIndex = (OInt) 0;
        this.mEnemyLeaderIndex = (OInt) 0;
        this.VersusTurnMax = (uint) this.mQuestParam.VersusMoveCount;
        this.RemainVersusTurnCount = 0U;
        for (int index = 0; index < units.Length; ++index)
        {
          if (units[index] != null)
          {
            Unit unit = new Unit();
            if (!unit.Setup(units[index], ownerPlayerIndex[index] != 1 ? arenaUnitSettings[placementIndex[index]] : partyUnitSettings[placementIndex[index]], (Unit.DropItem) null, (Unit.DropItem) null))
            {
              this.DebugErr("failed unit Setup");
              return false;
            }
            unit.IsPartyMember = true;
            unit.SetUnitFlag(EUnitFlag.Searched, true);
            unit.OwnerPlayerIndex = ownerPlayerIndex[index];
            unit.Side = ownerPlayerIndex[index] == instance1.MyPlayerIndex ? EUnitSide.Player : EUnitSide.Enemy;
            if (unit.Side == EUnitSide.Player)
              this.mPlayer.Add(unit);
            this.mAllUnits.Add(unit);
            this.mStartingMembers.Add(unit);
          }
        }
      }
      else
      {
        this.IsMultiTower = GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.TOWER;
        for (int index = 0; index < units.Length; ++index)
        {
          if (units[index] != null)
          {
            UnitData unitData = new UnitData();
            unitData.Setup(units[index]);
            unitData.SetJob(playerPartyType);
            if (this.mQuestParam.IsUnitAllowed(units[index]))
            {
              Unit unit = new Unit();
              if (this.IsMultiTower)
              {
                if (!unit.Setup(units[index], partyUnitSettings[placementIndex[index]], (Unit.DropItem) null, (Unit.DropItem) null))
                {
                  this.DebugErr("failed unit Setup");
                  return false;
                }
              }
              else if (!unit.Setup(units[index], partyUnitSettings[index], (Unit.DropItem) null, (Unit.DropItem) null))
              {
                this.DebugErr("failed unit Setup");
                return false;
              }
              unit.IsPartyMember = true;
              unit.SetUnitFlag(EUnitFlag.Searched, true);
              unit.OwnerPlayerIndex = ownerPlayerIndex[index];
              unit.IsSub = sub[index];
              this.mPlayer.Add(unit);
              this.mAllUnits.Add(unit);
              if (!unit.IsSub)
                this.mStartingMembers.Add(unit);
            }
          }
        }
      }
      return true;
    }

    public bool Deserialize(string questID, BattleCore.Json_Battle jsonBtl, int myPlayerIndex, UnitData[] units, int[] ownerPlayerIndex, bool is_restart = false, int[] placementIndex = null, bool[] sub = null)
    {
      if (jsonBtl == null | string.IsNullOrEmpty(questID))
        return false;
      this.mMyPlayerIndex = myPlayerIndex;
      if (this.mMyPlayerIndex <= 0)
      {
        this.DebugLog("[PUN]this is singleplay");
      }
      else
      {
        this.IsMultiPlay = true;
        this.DebugLog("[PUN]this is multiplay");
      }
      GameManager instance = MonoSingleton<GameManager>.Instance;
      this.mBtlID = jsonBtl.btlid;
      this.mMapIndex = 0;
      this.mLeaderIndex = (OInt) -1;
      this.mFriendIndex = (OInt) -1;
      this.mFriendStates = FriendStates.None;
      this.mWinTriggerCount = 0;
      this.mLoseTriggerCount = 0;
      if (jsonBtl != null && jsonBtl.btlinfo != null && jsonBtl.btlinfo.quest_ranking != null)
        this.mRankingQuestParam = RankingQuestParam.FindRankingQuestParam(questID, jsonBtl.btlinfo.quest_ranking.schedule_id, (RankingQuestType) jsonBtl.btlinfo.quest_ranking.type);
      this.mQuestParam = MonoSingleton<GameManager>.Instance.FindQuest(questID);
      DebugUtility.Assert(this.mQuestParam != null, "mQuestParam == null");
      PlayerPartyTypes playerPartyType;
      PlayerPartyTypes enemyPartyType;
      this.mQuestParam.GetPartyTypes(out playerPartyType, out enemyPartyType);
      this.mSeed = (uint) jsonBtl.btlinfo.seed;
      this.mRand.Seed(this.mSeed);
      this.CurrentRand = this.mRand;
      this.mUniqueKey = jsonBtl.btlinfo.key;
      for (int index = 0; index < this.mQuestParam.map.Count; ++index)
      {
        BattleMap battleMap = new BattleMap();
        battleMap.mRandDeckResult = jsonBtl.btlinfo.lot_enemies;
        RandDeckResult[] deck = jsonBtl.btlinfo.GetDeck();
        if (deck != null)
          battleMap.mRandDeckResult = deck;
        if (!battleMap.Initialize(this, this.mQuestParam.map[index]))
          return false;
        this.mMap.Add(battleMap);
      }
      List<UnitSetting> partyUnitSettings = this.mMap[0].PartyUnitSettings;
      List<UnitSubSetting> partyUnitSubSettings = this.mMap[0].PartyUnitSubSettings;
      if (partyUnitSettings != null && partyUnitSettings.Count > 0)
      {
        if (this.IsMultiPlay || instance.AudienceMode || instance.IsVSCpuBattle)
        {
          if (!this.SetupMultiPlayUnit(units, ownerPlayerIndex, placementIndex, sub))
            return false;
        }
        else
        {
          int index1 = 0;
          int index2 = 0;
          string[] strArray = this.mQuestParam.questParty == null ? this.mQuestParam.units.GetList() : ((IEnumerable<PartySlotTypeUnitPair>) this.mQuestParam.questParty.GetMainSubSlots()).Where<PartySlotTypeUnitPair>((Func<PartySlotTypeUnitPair, bool>) (slot => slot.Type == PartySlotType.ForcedHero)).Select<PartySlotTypeUnitPair, string>((Func<PartySlotTypeUnitPair, string>) (slot => slot.Unit)).ToArray<string>();
          if (strArray != null && strArray.Length > 0)
          {
            for (int index3 = 0; index3 < strArray.Length; ++index3)
            {
              string iname = strArray[index3];
              if (!string.IsNullOrEmpty(iname))
              {
                UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(iname);
                if (unitDataByUnitId == null)
                {
                  this.DebugErr("player uniqueid not equal");
                  return false;
                }
                UnitData unitdata = new UnitData();
                unitdata.Setup(unitDataByUnitId);
                unitdata.SetJob(playerPartyType);
                Unit unit = new Unit();
                if (!unit.Setup(unitdata, partyUnitSettings[index1], (Unit.DropItem) null, (Unit.DropItem) null))
                {
                  this.DebugErr("failed unit Setup");
                  return false;
                }
                unit.IsPartyMember = true;
                unit.SetUnitFlag(EUnitFlag.Searched, true);
                unit.SetUnitFlag(EUnitFlag.ForceEntried, true);
                unit.SetUnitFlag(EUnitFlag.DisableUnitChange, true);
                this.mPlayer.Add(unit);
                this.mAllUnits.Add(unit);
                this.mStartingMembers.Add(unit);
                ++index1;
              }
            }
          }
          if (jsonBtl.btlinfo.units != null)
          {
            for (int index3 = 0; index3 < jsonBtl.btlinfo.units.Length; ++index3)
            {
              long iid = (long) jsonBtl.btlinfo.units[index3].iid;
              if (iid > 0L)
              {
                UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(iid);
                if (unitDataByUniqueId == null)
                {
                  this.DebugErr("player uniqueid not equal");
                  return false;
                }
                UnitData unitData = new UnitData();
                unitData.Setup(unitDataByUniqueId);
                PartyData partyCurrent = MonoSingleton<GameManager>.Instance.Player.GetPartyCurrent();
                if (this.mQuestParam.type == QuestTypes.Tower)
                {
                  int num = !MonoSingleton<GameManager>.Instance.FindTowerFloor(this.mQuestParam.iname).can_help ? -1 : 0;
                  if (partyUnitSettings.Count <= index1 + num)
                    continue;
                }
                else if (this.mQuestParam.type == QuestTypes.Raid)
                {
                  if (partyUnitSettings.Count <= index1 - 1)
                    continue;
                }
                else if (partyUnitSettings.Count <= index1)
                  continue;
                if (this.mQuestParam.IsUnitAllowed(unitData))
                {
                  bool flag = index3 < partyCurrent.MAX_MAINMEMBER;
                  if (!flag || index3 < this.mQuestParam.GetSelectMainMemberNum())
                  {
                    UnitSetting setting = this.mQuestParam.type == QuestTypes.Tower || this.mQuestParam.type == QuestTypes.Raid ? (flag ? partyUnitSettings[index1] : partyUnitSettings[partyUnitSettings.Count - 1]) : partyUnitSettings[index1];
                    if (!flag)
                    {
                      setting = new UnitSetting();
                      setting.side = (OInt) 0;
                      if (index2 < partyUnitSubSettings.Count)
                      {
                        setting.startCtCalc = partyUnitSubSettings[index2].startCtCalc;
                        setting.startCtVal = partyUnitSubSettings[index2].startCtVal;
                        ++index2;
                      }
                    }
                    Unit unit = new Unit();
                    if (!unit.Setup(unitData, setting, (Unit.DropItem) null, (Unit.DropItem) null))
                    {
                      this.DebugErr("failed unit Setup");
                      return false;
                    }
                    if ((int) this.mLeaderIndex == -1)
                      this.mLeaderIndex = (OInt) index1;
                    if (flag)
                    {
                      this.mStartingMembers.Add(unit);
                      ++index1;
                    }
                    unit.SetUnitFlag(EUnitFlag.Searched, true);
                    unit.IsPartyMember = true;
                    unit.IsSub = !flag;
                    this.mPlayer.Add(unit);
                    this.mAllUnits.Add(unit);
                  }
                }
              }
            }
          }
          if (jsonBtl.btlinfo.help != null && index1 < partyUnitSettings.Count)
          {
            UnitData unitdata = new UnitData();
            try
            {
              unitdata.Deserialize(jsonBtl.btlinfo.help.unit);
            }
            catch (Exception ex)
            {
              this.DebugErr("<EXCEPTION> " + ex.Message + "\n-----------------------\n" + ex.StackTrace);
            }
            Unit unit = new Unit();
            if (!unit.Setup(unitdata, partyUnitSettings[index1], (Unit.DropItem) null, (Unit.DropItem) null))
            {
              this.DebugErr("failed unit Setup");
              return false;
            }
            unit.IsPartyMember = true;
            unit.IsSub = false;
            unit.SetUnitFlag(EUnitFlag.Searched, true);
            unit.SetUnitFlag(EUnitFlag.DisableUnitChange, true);
            unit.SetUnitFlag(EUnitFlag.IsHelp, true);
            this.mFriendIndex = (OInt) this.mPlayer.Count;
            this.mPlayer.Add(unit);
            this.mAllUnits.Add(unit);
            this.mStartingMembers.Add(unit);
            this.mFriendStates = (FriendStates) jsonBtl.btlinfo.help.isFriend;
            int num = index1 + 1;
          }
          this.mCurrentTeamId = 0;
          this.mMaxTeamId = 1;
          if (jsonBtl.btlinfo.ordeals != null && jsonBtl.btlinfo.ordeals.Length > 1)
          {
            for (int index3 = 1; index3 < jsonBtl.btlinfo.ordeals.Length; ++index3)
            {
              BattleCore.Json_BtlOrdeal ordeal = jsonBtl.btlinfo.ordeals[index3];
              if (ordeal.units != null)
              {
                for (int index4 = 0; index4 < ordeal.units.Length; ++index4)
                {
                  long iid = (long) ordeal.units[index4].iid;
                  if (iid > 0L)
                  {
                    UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(iid);
                    if (unitDataByUniqueId == null)
                    {
                      this.DebugErr("ordeals/player uniqueid not equal");
                      return false;
                    }
                    UnitData unitData = new UnitData();
                    unitData.Setup(unitDataByUniqueId);
                    if (this.mQuestParam.IsUnitAllowed(unitData))
                    {
                      UnitSetting setting = partyUnitSettings[index4];
                      Unit unit = new Unit();
                      if (!unit.Setup(unitData, setting, (Unit.DropItem) null, (Unit.DropItem) null))
                      {
                        this.DebugErr("ordeals/failed unit Setup");
                        return false;
                      }
                      unit.SetUnitFlag(EUnitFlag.Searched, true);
                      unit.SetUnitFlag(EUnitFlag.Entried, false);
                      unit.SetUnitFlag(EUnitFlag.OtherTeam, true);
                      unit.TeamId = index3;
                      unit.IsPartyMember = true;
                      this.mPlayer.Add(unit);
                      this.mAllUnits.Add(unit);
                    }
                  }
                }
                if (ordeal.help != null)
                {
                  UnitData unitdata = new UnitData();
                  try
                  {
                    unitdata.Deserialize(ordeal.help.unit);
                  }
                  catch (Exception ex)
                  {
                    this.DebugErr("ordeals/friend <EXCEPTION> " + ex.Message + "\n-----------------------\n" + ex.StackTrace);
                  }
                  Unit unit = new Unit();
                  if (!unit.Setup(unitdata, partyUnitSettings[ordeal.units.Length], (Unit.DropItem) null, (Unit.DropItem) null))
                  {
                    this.DebugErr("ordeals/failed unit Setup");
                    return false;
                  }
                  unit.IsPartyMember = true;
                  unit.IsSub = false;
                  unit.SetUnitFlag(EUnitFlag.Searched, true);
                  unit.SetUnitFlag(EUnitFlag.DisableUnitChange, true);
                  unit.SetUnitFlag(EUnitFlag.Entried, false);
                  unit.SetUnitFlag(EUnitFlag.OtherTeam, true);
                  unit.SetUnitFlag(EUnitFlag.IsHelp, true);
                  unit.TeamId = index3;
                  unit.FriendStates = (FriendStates) ordeal.help.isFriend;
                  this.mPlayer.Add(unit);
                  this.mAllUnits.Add(unit);
                }
                ++this.mMaxTeamId;
              }
            }
          }
        }
        this.mNpcStartIndex = this.mAllUnits.Count;
      }
      if ((int) this.mLeaderIndex == (int) (OInt) -1 && this.mPlayer.Count >= 1)
        this.mLeaderIndex = (OInt) 0;
      this.mEnemys = new List<Unit>[this.mMap.Count];
      switch (this.mQuestParam.type)
      {
        case QuestTypes.Story:
        case QuestTypes.Multi:
        case QuestTypes.Tutorial:
        case QuestTypes.Free:
        case QuestTypes.Event:
        case QuestTypes.Character:
        case QuestTypes.Tower:
        case QuestTypes.Gps:
        case QuestTypes.Extra:
        case QuestTypes.MultiTower:
        case QuestTypes.Beginner:
        case QuestTypes.MultiGps:
        case QuestTypes.Ordeal:
        case QuestTypes.Raid:
        case QuestTypes.GenesisStory:
        case QuestTypes.GenesisBoss:
          string str = (string) null;
          int num1 = 0;
          if (this.mQuestParam.questParty != null)
          {
            str = this.mQuestParam.questParty.GetNpcLeaderUnitIname();
            num1 = this.mQuestParam.questParty.l_npc_rare;
          }
          int count = this.mPlayer.Count;
          int index5 = 0;
          for (int index1 = 0; index1 < this.mMap.Count; ++index1)
          {
            this.mEnemys[index1] = new List<Unit>(BattleCore.MAX_ENEMY);
            List<NPCSetting> npcUnitSettings = this.mMap[index1].NPCUnitSettings;
            if (npcUnitSettings != null)
            {
              for (int index2 = 0; index2 < npcUnitSettings.Count; ++index2)
              {
                Unit unit = new Unit();
                Unit.DropItem dropitem = (Unit.DropItem) null;
                BattleCore.Json_BtlDrop[] drops = jsonBtl.btlinfo.drops;
                if (drops != null && index5 < drops.Length && (!string.IsNullOrEmpty(drops[index5].iname) && drops[index5].num > 0))
                {
                  ItemParam itemParam = (ItemParam) null;
                  ConceptCardParam conceptCardParam = (ConceptCardParam) null;
                  if (drops[index5].dropItemType == EBattleRewardType.Item)
                    itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(drops[index5].iname);
                  else if (drops[index5].dropItemType == EBattleRewardType.ConceptCard)
                    conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(drops[index5].iname);
                  else
                    DebugUtility.LogError(string.Format("不明なドロップ品が登録されています。iname:{0} (itype:{1})", (object) drops[index5].iname, (object) drops[index5].itype));
                  if (itemParam != null || conceptCardParam != null)
                  {
                    dropitem = new Unit.DropItem();
                    dropitem.itemParam = itemParam;
                    dropitem.conceptCardParam = conceptCardParam;
                    dropitem.num = (OInt) drops[index5].num;
                    dropitem.is_secret = (OBool) (drops[index5].secret != 0);
                  }
                }
                Unit.DropItem stealitem = (Unit.DropItem) null;
                BattleCore.Json_BtlSteal[] steals = jsonBtl.btlinfo.steals;
                if (steals != null && index5 < steals.Length && (!string.IsNullOrEmpty(steals[index5].iname) && steals[index5].num > 0))
                {
                  ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(steals[index5].iname);
                  if (itemParam != null)
                  {
                    stealitem = new Unit.DropItem();
                    stealitem.itemParam = itemParam;
                    stealitem.num = (OInt) steals[index5].num;
                  }
                }
                bool flag = false;
                if (!string.IsNullOrEmpty(str) && (string) npcUnitSettings[index2].iname == str)
                {
                  flag = true;
                  npcUnitSettings[index2].rare = (OInt) num1;
                  str = (string) null;
                }
                if (!unit.Setup((UnitData) null, (UnitSetting) npcUnitSettings[index2], dropitem, stealitem))
                {
                  this.DebugErr("enemy unit setup failed");
                  return false;
                }
                unit.SetUnitFlag(EUnitFlag.DisableUnitChange, true);
                switch (unit.Side)
                {
                  case EUnitSide.Player:
                    if (flag)
                      this.mLeaderIndex = (OInt) count;
                    this.mPlayer.Add(unit);
                    this.mStartingMembers.Add(unit);
                    ++count;
                    break;
                  case EUnitSide.Enemy:
                  case EUnitSide.Neutral:
                    this.mEnemys[index1].Add(unit);
                    break;
                }
                this.mAllUnits.Add(unit);
                ++index5;
              }
            }
          }
          for (int index1 = 0; index1 < this.mMap.Count; ++index1)
          {
            if (this.mMap[index1].InfinitySpawnMgr != null)
            {
              this.mMap[index1].InfinitySpawnMgr.RefreshSpawnEnemyUnitsForBattleStart(this.mUnits);
              for (int index2 = 0; index2 < this.mMap[index1].InfinitySpawnMgr.InfinitySpawnGroups.Length; ++index2)
              {
                InfinitySpawnGroupData infinitySpawnGroup = this.mMap[index1].InfinitySpawnMgr.InfinitySpawnGroups[index2];
                for (int index3 = 0; index3 < infinitySpawnGroup.InfinitySpawns.Count; ++index3)
                {
                  List<int> reserveUnitIndexList = infinitySpawnGroup.InfinitySpawns[index3].ReserveUnitIndexList;
                  if (reserveUnitIndexList.Count > 0)
                  {
                    for (int index4 = 0; index4 < reserveUnitIndexList.Count; ++index4)
                      this.CreateInfinitySpawnUnit(string.Empty, infinitySpawnGroup.InfinitySpawns[index3].x, infinitySpawnGroup.InfinitySpawns[index3].y, infinitySpawnGroup.InfinitySpawns[index3].dir, infinitySpawnGroup.InfinitySpawns[index3].group, reserveUnitIndexList[index4], false);
                  }
                }
              }
            }
          }
          break;
        case QuestTypes.Arena:
          ArenaPlayer selectedArenaPlayer = (ArenaPlayer) GlobalVars.SelectedArenaPlayer;
          if (selectedArenaPlayer != null)
          {
            List<UnitSetting> arenaUnitSettings = this.mMap[0].ArenaUnitSettings;
            this.mEnemys[0] = new List<Unit>(selectedArenaPlayer.Unit.Length);
            for (int index1 = 0; index1 < selectedArenaPlayer.Unit.Length; ++index1)
            {
              if (selectedArenaPlayer.Unit[index1] != null)
              {
                UnitData unitdata = new UnitData();
                unitdata.Setup(selectedArenaPlayer.Unit[index1]);
                unitdata.SetJob(enemyPartyType);
                Unit unit = new Unit();
                if (!unit.Setup(unitdata, arenaUnitSettings[index1], (Unit.DropItem) null, (Unit.DropItem) null))
                {
                  this.DebugErr("failed unit Setup");
                  return false;
                }
                unit.SetUnitFlag(EUnitFlag.Searched, true);
                this.mEnemys[0].Add(unit);
                this.mAllUnits.Add(unit);
              }
            }
            this.mEnemyLeaderIndex = (OInt) 0;
          }
          for (int index1 = 0; index1 < this.mAllUnits.Count; ++index1)
            this.mAllUnits[index1].SetUnitFlag(EUnitFlag.ForceAuto, true);
          List<NPCSetting> npcUnitSettings1 = this.mMap[0].NPCUnitSettings;
          if (npcUnitSettings1 != null)
          {
            for (int index1 = 0; index1 < npcUnitSettings1.Count; ++index1)
            {
              Unit unit = new Unit();
              if (!unit.Setup((UnitData) null, (UnitSetting) npcUnitSettings1[index1], (Unit.DropItem) null, (Unit.DropItem) null))
                this.DebugErr("Arena: enemy unit setup failed");
              else if (unit.IsBreakObj && unit.Side == EUnitSide.Neutral)
              {
                unit.SetUnitFlag(EUnitFlag.DisableUnitChange, true);
                this.mEnemys[0].Add(unit);
                this.mAllUnits.Add(unit);
              }
            }
            break;
          }
          break;
        case QuestTypes.VersusFree:
        case QuestTypes.VersusRank:
          this.mEnemyLeaderIndex = (OInt) 0;
          this.mEnemys[0] = new List<Unit>(BattleCore.MAX_ENEMY);
          foreach (Unit allUnit in this.AllUnits)
          {
            if (allUnit.Side == EUnitSide.Enemy)
            {
              this.mEnemys[0].Add(allUnit);
              if (instance.IsVSCpuBattle)
                allUnit.SetUnitFlag(EUnitFlag.ForceAuto, true);
            }
          }
          List<NPCSetting> npcUnitSettings2 = this.mMap[0].NPCUnitSettings;
          if (npcUnitSettings2 != null)
          {
            for (int index1 = 0; index1 < npcUnitSettings2.Count; ++index1)
            {
              Unit unit = new Unit();
              if (!unit.Setup((UnitData) null, (UnitSetting) npcUnitSettings2[index1], (Unit.DropItem) null, (Unit.DropItem) null))
                this.DebugErr("Versus: enemy unit setup failed");
              else if (unit.IsBreakObj && unit.Side == EUnitSide.Neutral)
              {
                unit.SetUnitFlag(EUnitFlag.DisableUnitChange, true);
                this.mEnemys[0].Add(unit);
                this.mAllUnits.Add(unit);
              }
            }
            break;
          }
          break;
        case QuestTypes.RankMatch:
          this.mEnemyLeaderIndex = (OInt) 0;
          this.mEnemys[0] = new List<Unit>(BattleCore.MAX_ENEMY);
          foreach (Unit allUnit in this.AllUnits)
          {
            if (allUnit.Side == EUnitSide.Enemy)
              this.mEnemys[0].Add(allUnit);
          }
          List<NPCSetting> npcUnitSettings3 = this.mMap[0].NPCUnitSettings;
          if (npcUnitSettings3 != null)
          {
            for (int index1 = 0; index1 < npcUnitSettings3.Count; ++index1)
            {
              Unit unit = new Unit();
              if (!unit.Setup((UnitData) null, (UnitSetting) npcUnitSettings3[index1], (Unit.DropItem) null, (Unit.DropItem) null))
                this.DebugErr("Versus: enemy unit setup failed");
              else if (unit.IsBreakObj && unit.Side == EUnitSide.Neutral)
              {
                unit.SetUnitFlag(EUnitFlag.DisableUnitChange, true);
                this.mEnemys[0].Add(unit);
                this.mAllUnits.Add(unit);
              }
            }
            break;
          }
          break;
      }
      this.mEntryUnitMax = 0;
      for (int index1 = 0; index1 < this.mAllUnits.Count; ++index1)
      {
        if (!this.mAllUnits[index1].IsUnitFlag(EUnitFlag.InfinitySpawn))
          ++this.mEntryUnitMax;
      }
      if (jsonBtl.btlinfo.atkmags != null)
        this.mQuestParam.SetAtkTypeMag(jsonBtl.btlinfo.atkmags);
      if (jsonBtl.btlinfo.campaigns != null)
        this.mQuestCampaignIds = jsonBtl.btlinfo.campaigns;
      if (jsonBtl.btlinfo.sins != null)
      {
        for (int index1 = 0; index1 < jsonBtl.btlinfo.sins.Length; ++index1)
        {
          BattleCore.Json_BtlInspSlot sin = jsonBtl.btlinfo.sins[index1];
          Unit playerUnitByUniqueId = this.FindPlayerUnitByUniqueID(sin.uiid);
          if (playerUnitByUniqueId != null)
          {
            for (int index2 = 0; index2 < sin.artifact.Length; ++index2)
            {
              BattleCore.Json_BtlInspArtifactSlot inspArtifactSlot = sin.artifact[index2];
              playerUnitByUniqueId.EntryInspIns((long) inspArtifactSlot.iid, inspArtifactSlot.slot, false, -1);
            }
          }
        }
      }
      if (!is_restart)
      {
        int length = this.mEntryUnitMax - this.mNpcStartIndex;
        if (length != 0)
        {
          this.mRecord.drops = new OInt[length];
          Array.Clear((Array) this.mRecord.drops, 0, length);
          this.mRecord.item_steals = new OInt[length];
          Array.Clear((Array) this.mRecord.item_steals, 0, length);
          this.mRecord.gold_steals = new OInt[length];
          Array.Clear((Array) this.mRecord.gold_steals, 0, length);
        }
      }
      this.UpdateUnitName();
      if (this.CurrentMap != null && this.mQuestParam.IsNoStartVoice)
      {
        for (int index1 = 0; index1 < this.mAllUnits.Count; ++index1)
          this.mAllUnits[index1].SetUnitFlag(EUnitFlag.DisableFirstVoice, true);
      }
      this.BeginBattlePassiveSkill();
      if (this.mQuestParam.type == QuestTypes.Tower)
      {
        MonoSingleton<GameManager>.Instance.TowerResuponse.CalcDamage(this.Player);
        MonoSingleton<GameManager>.Instance.TowerResuponse.CalcEnemyDamage(this.Enemys, false);
      }
      else if (this.mQuestParam.type == QuestTypes.Raid)
      {
        for (int index1 = 0; index1 < this.Enemys.Count; ++index1)
        {
          if (this.Enemys[index1].IsRaidBoss)
            this.Enemys[index1].CurrentStatus.param.hp = (OInt) GlobalVars.CurrentRaidBossInfo.HP;
        }
      }
      else if (this.mQuestParam.type == QuestTypes.GenesisBoss && jsonBtl.btlinfo.enemies != null)
      {
        for (int index1 = 0; index1 < jsonBtl.btlinfo.enemies.Length; ++index1)
        {
          BattleCore.Json_GenesisEnemy enemy = jsonBtl.btlinfo.enemies[index1];
          for (int mNpcStartIndex = this.mNpcStartIndex; mNpcStartIndex < this.mAllUnits.Count; ++mNpcStartIndex)
          {
            if (enemy.eid == mNpcStartIndex - this.mNpcStartIndex && this.mAllUnits[mNpcStartIndex].IsRaidBoss == (enemy.boss_flg != 0))
            {
              this.mAllUnits[mNpcStartIndex].CurrentStatus.param.hp = (OInt) Math.Max(Math.Min(enemy.hp, (int) this.mAllUnits[mNpcStartIndex].MaximumStatus.param.hp), 0);
              break;
            }
          }
        }
      }
      for (int index1 = 0; index1 < 5; ++index1)
      {
        this.mInventory[index1] = (ItemData) null;
        ItemData itemData1 = MonoSingleton<GameManager>.GetInstanceDirect().Player.Inventory[index1];
        if (itemData1 != null)
        {
          ItemData itemData2 = new ItemData();
          itemData2.Setup(itemData1.UniqueID, itemData1.Param, Math.Min(itemData1.Num, itemData1.Param.invcap));
          this.mInventory[index1] = itemData2;
        }
      }
      this.mRand.Seed(this.mSeed);
      this.SetBattleFlag(EBattleFlag.Initialized, true);
      return true;
    }

    public uint GetRandom()
    {
      return this.CurrentRand.Get();
    }

    private void UpdateUnitName()
    {
      if (this.mPlayer != null)
      {
        for (int index1 = 0; index1 < this.mPlayer.Count; ++index1)
        {
          char ch = 'A';
          for (int index2 = 0; index2 < this.mPlayer.Count && this.mPlayer[index1] != this.mPlayer[index2]; ++index2)
          {
            if (this.mPlayer[index1].UnitParam.iname == this.mPlayer[index2].UnitParam.iname)
              ++ch;
          }
          this.mPlayer[index1].UnitName = this.mPlayer[index1].UnitParam.name + (object) ch;
        }
      }
      if (this.mEnemys == null)
        return;
      for (int index1 = 0; index1 < this.mMap.Count; ++index1)
      {
        List<Unit> mEnemy = this.mEnemys[index1];
        for (int index2 = 0; index2 < mEnemy.Count; ++index2)
        {
          char ch = 'A';
          for (int index3 = 0; index3 < mEnemy.Count && mEnemy[index2] != mEnemy[index3]; ++index3)
          {
            if (mEnemy[index2].UnitParam.iname == mEnemy[index3].UnitParam.iname)
              ++ch;
          }
          mEnemy[index2].UnitName = mEnemy[index2].UnitParam.name + (object) ch;
        }
      }
    }

    public QuestParam GetQuest()
    {
      return MonoSingleton<GameManager>.Instance.FindQuest(this.QuestID);
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

    private void BeginBattlePassiveSkill()
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
        this.mUnits[index].ClearPassiveBuffEffects();
      if (this.IsMultiTower)
      {
        this.mMtLeaderIndexList.Clear();
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if ((UnityEngine.Object) instance != (UnityEngine.Object) null)
        {
          List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
          if (myPlayersStarted != null)
          {
            for (int i = 0; i < myPlayersStarted.Count; ++i)
            {
              int index = this.mPlayer.FindIndex((Predicate<Unit>) (data => data.OwnerPlayerIndex == i + 1));
              if (index != -1)
                this.InternalBattlePassiveSkill(this.mPlayer[index], this.mPlayer[index].LeaderSkill, true, (BuffEffect[]) null);
              this.mMtLeaderIndexList.Add(index);
            }
          }
        }
      }
      else if (this.Leader != null)
        this.InternalBattlePassiveSkill(this.Leader, this.Leader.LeaderSkill, true, (BuffEffect[]) null);
      if (this.Friend != null && this.mFriendStates == FriendStates.Friend)
        this.InternalBattlePassiveSkill(this.Friend, this.Friend.LeaderSkill, true, (BuffEffect[]) null);
      if (this.EnemyLeader != null)
        this.InternalBattlePassiveSkill(this.EnemyLeader, this.EnemyLeader.LeaderSkill, true, (BuffEffect[]) null);
      for (int index1 = 0; index1 < this.mAllUnits.Count; ++index1)
      {
        Unit mAllUnit = this.mAllUnits[index1];
        if (mAllUnit != null && !mAllUnit.IsDead && !mAllUnit.IsUnitFlag(EUnitFlag.OtherTeam))
        {
          EquipData[] currentEquips = mAllUnit.CurrentEquips;
          if (currentEquips != null)
          {
            for (int index2 = 0; index2 < currentEquips.Length; ++index2)
            {
              EquipData equipData = currentEquips[index2];
              if (equipData != null && equipData.IsValid() && equipData.IsEquiped())
                this.InternalBattlePassiveSkill(mAllUnit, equipData.Skill, false, (BuffEffect[]) null);
            }
          }
          for (int index2 = 0; index2 < mAllUnit.BattleSkills.Count; ++index2)
            this.InternalBattlePassiveSkill(mAllUnit, mAllUnit.BattleSkills[index2], false, (BuffEffect[]) null);
          ConceptCardData conceptCard = mAllUnit.UnitData.ConceptCard;
          if (conceptCard != null)
          {
            foreach (SkillData enableCardSkill in conceptCard.GetEnableCardSkills(mAllUnit.UnitData))
            {
              List<BuffEffect> cardSkillAddBuffs = conceptCard.GetEnableCardSkillAddBuffs(mAllUnit.UnitData, enableCardSkill.SkillParam);
              this.InternalBattlePassiveSkill(mAllUnit, enableCardSkill, true, cardSkillAddBuffs.ToArray());
            }
          }
        }
      }
      for (int index = 0; index < this.Player.Count; ++index)
        this.Player[index].CalcCurrentStatus(this.mMapIndex == 0, false);
      for (int index = 0; index < this.Enemys.Count; ++index)
        this.Enemys[index].CalcCurrentStatus(true, false);
    }

    private void BeginBattlePassiveSkill(Unit unit)
    {
      if (unit == null || unit.IsDead)
        return;
      EquipData[] currentEquips = unit.CurrentEquips;
      if (currentEquips != null)
      {
        for (int index = 0; index < currentEquips.Length; ++index)
        {
          EquipData equipData = currentEquips[index];
          if (equipData != null && equipData.IsValid() && equipData.IsEquiped())
            this.InternalBattlePassiveSkill(unit, equipData.Skill, false, (BuffEffect[]) null);
        }
      }
      for (int index = 0; index < unit.BattleSkills.Count; ++index)
        this.InternalBattlePassiveSkill(unit, unit.BattleSkills[index], false, (BuffEffect[]) null);
      ConceptCardData conceptCard = unit.UnitData.ConceptCard;
      if (conceptCard != null)
      {
        foreach (SkillData enableCardSkill in conceptCard.GetEnableCardSkills(unit.UnitData))
        {
          if (!enableCardSkill.IsSubActuate())
          {
            List<BuffEffect> cardSkillAddBuffs = conceptCard.GetEnableCardSkillAddBuffs(unit.UnitData, enableCardSkill.SkillParam);
            this.InternalBattlePassiveSkill(unit, enableCardSkill, true, cardSkillAddBuffs.ToArray());
          }
        }
      }
      for (int index = 0; index < this.Player.Count; ++index)
      {
        Unit unit1 = this.Player[index];
        if (unit1 != null && !unit1.IsDead)
          unit1.CalcCurrentStatus(false, false);
      }
      for (int index = 0; index < this.Enemys.Count; ++index)
      {
        Unit enemy = this.Enemys[index];
        if (enemy != null && !enemy.IsDead)
          enemy.CalcCurrentStatus(false, false);
      }
    }

    private void UpdateBattlePassiveSkill()
    {
      for (int index1 = 0; index1 < this.mAllUnits.Count; ++index1)
      {
        Unit mAllUnit = this.mAllUnits[index1];
        if (mAllUnit != null && !mAllUnit.IsDead && !mAllUnit.IsUnitFlag(EUnitFlag.OtherTeam))
        {
          EquipData[] currentEquips = mAllUnit.CurrentEquips;
          if (currentEquips != null)
          {
            for (int index2 = 0; index2 < currentEquips.Length; ++index2)
            {
              EquipData equipData = currentEquips[index2];
              if (equipData != null && equipData.IsValid() && equipData.IsEquiped())
              {
                SkillData skill = equipData.Skill;
                if (skill != null && skill.Target != ESkillTarget.Self && (skill.IsPassiveSkill() && skill.Timing == ESkillTiming.Passive))
                  this.InternalBattlePassiveSkill(mAllUnit, skill, false, (BuffEffect[]) null);
              }
            }
          }
          for (int index2 = 0; index2 < mAllUnit.BattleSkills.Count; ++index2)
          {
            SkillData battleSkill = mAllUnit.BattleSkills[index2];
            if (battleSkill != null && battleSkill.Target != ESkillTarget.Self && (battleSkill.IsPassiveSkill() && battleSkill.Timing == ESkillTiming.Passive))
              this.InternalBattlePassiveSkill(mAllUnit, mAllUnit.BattleSkills[index2], false, (BuffEffect[]) null);
          }
        }
      }
      for (int index = 0; index < this.Player.Count; ++index)
        this.Player[index].CalcCurrentStatus(false, false);
      for (int index = 0; index < this.Enemys.Count; ++index)
        this.Enemys[index].CalcCurrentStatus(false, false);
    }

    private void InternalBattlePassiveSkill(Unit self, SkillData skill, bool is_duplicate = false, BuffEffect[] buff_effects = null)
    {
      if (skill == null || !skill.IsPassiveSkill())
        return;
      if (skill.Condition == ESkillCondition.MapEffect)
      {
        MapEffectParam mapEffectParam = MonoSingleton<GameManager>.Instance.GetMapEffectParam(this.mQuestParam.MapEffectId);
        if (mapEffectParam == null || !mapEffectParam.IsValidSkill(skill.SkillID))
          return;
      }
      for (int index = 0; index < this.Player.Count; ++index)
      {
        if (skill.Condition != ESkillCondition.CardSkill || ConceptCardUtility.IsEnableCardSkillForUnit(this.Player[index], skill))
          this.InternalBattlePassiveSkill(self, this.Player[index], skill, is_duplicate, buff_effects);
      }
      for (int index = 0; index < this.Enemys.Count; ++index)
      {
        if (skill.Condition != ESkillCondition.CardSkill || ConceptCardUtility.IsEnableCardSkillForUnit(this.Enemys[index], skill))
          this.InternalBattlePassiveSkill(self, this.Enemys[index], skill, is_duplicate, buff_effects);
      }
    }

    private void InternalBattlePassiveSkill(Unit self, Unit target, SkillData skill, bool is_duplicate = false, BuffEffect[] buff_effects = null)
    {
      if (self == null || skill == null || (!skill.IsPassiveSkill() || target == null) || (target.IsGimmick && !target.IsBreakObj || target.IsUnitFlag(EUnitFlag.OtherTeam)) || (self.IsSub && !skill.IsSubActuate() || !this.CheckSkillTarget(self, target, skill) || !is_duplicate && target.ContainsSkillAttachment(skill)))
        return;
      SkillEffectTypes effectType = skill.EffectType;
      switch (effectType)
      {
        case SkillEffectTypes.Buff:
        case SkillEffectTypes.Debuff:
        case SkillEffectTypes.FailCondition:
        case SkillEffectTypes.DisableCondition:
          bool flag = !string.IsNullOrEmpty(skill.SkillParam.tokkou);
          BuffEffect buffEffect = skill.GetBuffEffect(SkillEffectTargets.Target);
          if (buffEffect != null && buffEffect.param != null && (buffEffect.param.mAppType != EAppType.Standard || buffEffect.param.mEffRange != EEffRange.Self || (bool) buffEffect.param.mIsUpBuff))
            flag = true;
          if (skill.Target != ESkillTarget.Self || skill.Condition != ESkillCondition.None || flag)
            this.BuffSkill(ESkillTiming.Passive, self, target, skill, true, (LogSkill) null, SkillEffectTargets.Target, is_duplicate, buff_effects, (List<Unit>) null);
          this.CondSkill(ESkillTiming.Passive, self, target, skill, true, (LogSkill) null, SkillEffectTargets.Target, false, (List<Unit>) null);
          List<UnitData> ud_targets = new List<UnitData>((IEnumerable<UnitData>) new UnitData[1]{ target.UnitData });
          this.JudgeInspSkill(self, ud_targets, skill);
          break;
        default:
          if (effectType != SkillEffectTypes.Equipment)
            break;
          goto case SkillEffectTypes.Buff;
      }
    }

    private void SetBattleFlag(EBattleFlag tgt, bool sw)
    {
      if (sw)
        this.mBtlFlags |= 1 << (int) (tgt & (EBattleFlag) 31);
      else
        this.mBtlFlags &= ~(1 << (int) (tgt & (EBattleFlag) 31));
    }

    public bool IsBattleFlag(EBattleFlag tgt)
    {
      return (this.mBtlFlags & 1 << (int) (tgt & (EBattleFlag) 31)) != 0;
    }

    public bool IsInitialized
    {
      get
      {
        return this.IsBattleFlag(EBattleFlag.Initialized);
      }
    }

    public bool IsMapCommand
    {
      get
      {
        return this.IsBattleFlag(EBattleFlag.MapCommand);
      }
    }

    public bool IsBattle
    {
      get
      {
        return this.IsBattleFlag(EBattleFlag.Battle);
      }
    }

    public bool IsUnitAuto(Unit unit)
    {
      if (!unit.IsControl)
        return true;
      return this.IsAutoBattle;
    }

    public void RemoveLog()
    {
      this.mLogs.RemoveLog();
    }

    public LogType Log<LogType>() where LogType : BattleLog, new()
    {
      if (this.mIsArenaCalc || !MonoSingleton<GameManager>.Instance.AudienceManager.IsSkipEnd)
        return Activator.CreateInstance<LogType>();
      return this.mLogs.Log<LogType>();
    }

    private void CalcOrder()
    {
      while (true)
      {
        this.mOrder.Clear();
        for (int index = 0; index < this.mUnits.Count; ++index)
        {
          Unit mUnit = this.mUnits[index];
          if (!mUnit.IsDeadCondition() && mUnit.IsEntry && (mUnit.Side != EUnitSide.Player || !mUnit.IsSub) && (!mUnit.IsUnitCondition(EUnitCondition.Stop) && (!mUnit.IsGimmick || mUnit.AI != null)) && !mUnit.IsBreakObj)
          {
            this.mOrder.Add(new BattleCore.OrderData()
            {
              Unit = mUnit,
              IsCharged = mUnit.CheckChargeTimeFullOver()
            });
            if (mUnit.CastSkill != null)
              this.mOrder.Add(new BattleCore.OrderData()
              {
                Unit = mUnit,
                IsCastSkill = true,
                IsCharged = mUnit.CheckCastTimeFullOver()
              });
          }
        }
        for (int index = 0; index < this.mUnits.Count; ++index)
        {
          Unit mUnit = this.mUnits[index];
          if (mUnit.CheckEnableEntry())
          {
            this.EntryUnit(mUnit);
            if (!mUnit.IsBreakObj)
              this.mOrder.Add(new BattleCore.OrderData()
              {
                Unit = mUnit
              });
          }
        }
        if (this.mOrder.Count == 0 || this.CheckEnableNextClockTime())
          this.NextClockTime();
        else
          break;
      }
      MySort<BattleCore.OrderData>.Sort(this.mOrder, (Comparison<BattleCore.OrderData>) ((src, dsc) =>
      {
        int chargeTime1 = (int) src.Unit.ChargeTime;
        int castTime1 = (int) src.Unit.CastTime;
        int chargeTime2 = (int) dsc.Unit.ChargeTime;
        int castTime2 = (int) dsc.Unit.CastTime;
        int num = this.judgeSortOrder(src, dsc);
        src.Unit.ChargeTime = (OInt) chargeTime1;
        src.Unit.CastTime = (OInt) castTime1;
        dsc.Unit.ChargeTime = (OInt) chargeTime2;
        dsc.Unit.CastTime = (OInt) castTime2;
        return num;
      }));
    }

    private int judgeSortOrder(BattleCore.OrderData src, BattleCore.OrderData dst)
    {
      if (src.CheckChargeTimeFullOver() && dst.CheckChargeTimeFullOver())
      {
        int num1 = (int) src.GetChargeTime() * 1000 / (int) src.GetChargeTimeMax();
        int num2 = (int) dst.GetChargeTime() * 1000 / (int) dst.GetChargeTimeMax();
        if (num2 != num1)
          return num2 - num1;
        if (src.IsCastSkill && dst.IsCastSkill)
        {
          if ((int) src.Unit.CastIndex < (int) dst.Unit.CastIndex)
            return -1;
          if ((int) src.Unit.CastIndex > (int) dst.Unit.CastIndex)
            return 1;
        }
        if (src.IsCastSkill == dst.IsCastSkill)
          return (int) src.Unit.UnitIndex - (int) dst.Unit.UnitIndex;
        return src.IsCastSkill ? -1 : 1;
      }
      if (src.CheckChargeTimeFullOver())
        return -1;
      if (dst.CheckChargeTimeFullOver())
        return 1;
      if (!src.UpdateChargeTime() && !dst.UpdateChargeTime())
        return (int) src.Unit.UnitIndex - (int) dst.Unit.UnitIndex;
      return this.judgeSortOrder(src, dst);
    }

    public void StartOrder(bool sync = true, bool force = true, bool judge = true)
    {
      this.Logs.Reset();
      this.ResumeState = BattleCore.RESUME_STATE.NONE;
      WeatherData.IsExecuteUpdate = false;
      WeatherData.IsEntryConditionLog = false;
      this.NextOrder(true, sync, force, judge);
      WeatherData.IsExecuteUpdate = true;
      WeatherData.IsEntryConditionLog = true;
      this.ClearAI();
    }

    private void NextOrder(bool is_skip_update_et = false, bool sync = true, bool forceSync = false, bool judge = true)
    {
      bool flag = this.CurrentUnit != null && this.CurrentUnit.OwnerPlayerIndex > 0;
      if (!is_skip_update_et)
      {
        for (int index = 0; index < this.mUnits.Count; ++index)
        {
          this.UpdateEntryTriggers(UnitEntryTypes.DecrementHp, this.mUnits[index], (SkillParam) null);
          this.UpdateEntryTriggers(UnitEntryTypes.OnGridEnemy, this.mUnits[index], (SkillParam) null);
          this.CheckWithDrawUnit(this.mUnits[index]);
        }
        this.UpdateEntryTriggers(UnitEntryTypes.DecrementMember, (Unit) null, (SkillParam) null);
        this.UpdateEntryTriggers(UnitEntryTypes.DecrementUnit, (Unit) null, (SkillParam) null);
        this.UpdateCancelCastSkill();
        this.UpdateGimmickEventStart();
        this.UpdateGimmickEventTrick();
        TrickData.UpdateMarker();
      }
      if (judge && this.CheckJudgeBattle())
      {
        this.CalcQuestRecord();
        this.MapEnd();
      }
      else
      {
        this.CalcOrder();
        if (!is_skip_update_et && this.UpdateEntryByInfinitySpawn(this.mUnits))
          this.CalcOrder();
        this.UpdateWeather();
        if (this.UseAutoSkills())
          this.CalcOrder();
        this.CheckBreakObjKill();
        if (this.CheckJudgeBattle())
        {
          this.CalcQuestRecord();
          this.MapEnd();
        }
        else
        {
          BattleCore.OrderData currentOrderData = this.CurrentOrderData;
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (this.IsMultiPlay && !instance.AudienceMode && !instance.IsVSCpuBattle && (sync && flag || forceSync))
            this.Log<LogSync>();
          if (currentOrderData.IsCastSkill)
            this.Log<LogCastSkillStart>();
          else
            this.Log<LogUnitStart>();
          this.mKillstreak = 0;
          this.mTargetKillstreakDict.Clear();
        }
      }
    }

    private bool UseAutoSkills()
    {
      bool flag = false;
      for (int index = 0; index < this.mAllUnits.Count; ++index)
      {
        if (this.UseAutoSkills(this.mAllUnits[index]))
          flag = true;
      }
      return flag;
    }

    private bool UseAutoSkills(Unit unit)
    {
      bool flag = false;
      if (unit.IsUnitFlag(EUnitFlag.Entried) && !unit.IsSub && !unit.IsUnitFlag(EUnitFlag.TriggeredAutoSkills))
      {
        unit.SetUnitFlag(EUnitFlag.TriggeredAutoSkills, true);
        for (int index = 0; index < unit.BattleSkills.Count; ++index)
        {
          SkillData battleSkill = unit.BattleSkills[index];
          if (battleSkill.Timing == ESkillTiming.Auto)
          {
            if (battleSkill.Condition == ESkillCondition.MapEffect)
            {
              MapEffectParam mapEffectParam = MonoSingleton<GameManager>.Instance.GetMapEffectParam(this.mQuestParam.MapEffectId);
              if (mapEffectParam == null || !mapEffectParam.IsValidSkill(battleSkill.SkillID))
                continue;
            }
            this.UseSkill(unit, unit.x, unit.y, battleSkill, false, 0, 0, true);
            flag = true;
          }
        }
      }
      return flag;
    }

    private bool CheckEnableNextClockTime()
    {
      for (int index = 0; index < this.mOrder.Count; ++index)
      {
        if (this.mOrder[index].CheckChargeTimeFullOver())
          return false;
      }
      return true;
    }

    private void NextClockTime()
    {
      ++this.mClockTime;
      ++this.mClockTimeTotal;
      if (TrickData.CheckClock((int) this.mClockTimeTotal))
        TrickData.UpdateMarker();
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        mUnit.UpdateClockTime();
        if (mUnit.CheckEnableEntry())
        {
          this.EntryUnit(mUnit);
          if (!mUnit.IsBreakObj)
            this.mOrder.Add(new BattleCore.OrderData()
            {
              Unit = mUnit
            });
        }
      }
      for (int index = 0; index < this.mOrder.Count; ++index)
        this.mOrder[index].UpdateChargeTime();
    }

    private void CreateGimmickEvents()
    {
      BattleMap currentMap = this.CurrentMap;
      if (currentMap.GimmickEvents == null)
        return;
      for (int index1 = 0; index1 < currentMap.GimmickEvents.Count; ++index1)
      {
        JSON_GimmickEvent gimmickEvent1 = currentMap.GimmickEvents[index1];
        if ((!string.IsNullOrEmpty(gimmickEvent1.skill) || gimmickEvent1.ev_type != 0) && gimmickEvent1.type != 0)
        {
          GimmickEvent gimmickEvent2 = new GimmickEvent();
          gimmickEvent2.ev_type = (eGimmickEventType) gimmickEvent1.ev_type;
          bool is_starter1 = false;
          if (gimmickEvent2.ev_type != eGimmickEventType.UNIT_KILL)
          {
            this.GetConditionUnitByUnitID(gimmickEvent2.users, gimmickEvent1.su_iname);
            this.GetConditionUnitByUniqueName(gimmickEvent2.users, gimmickEvent1.su_tag, out is_starter1);
            if ((!string.IsNullOrEmpty(gimmickEvent1.su_iname) || !string.IsNullOrEmpty(gimmickEvent1.su_tag)) && gimmickEvent2.users.Count == 0)
              continue;
          }
          this.GetConditionUnitByUnitID(gimmickEvent2.targets, gimmickEvent1.st_iname);
          bool is_starter2 = false;
          this.GetConditionUnitByUniqueName(gimmickEvent2.targets, gimmickEvent1.st_tag, out is_starter2);
          gimmickEvent2.IsStarter = is_starter2;
          this.GetConditionTrickByTrickID(gimmickEvent2.td_targets, gimmickEvent1.st_iname);
          this.GetConditionTrickByTag(gimmickEvent2.td_targets, gimmickEvent1.st_tag);
          gimmickEvent2.td_iname = gimmickEvent1.st_iname;
          gimmickEvent2.td_tag = gimmickEvent1.st_tag;
          if (gimmickEvent2.ev_type == eGimmickEventType.USE_SKILL)
          {
            string[] strArray = gimmickEvent1.skill.Split(',');
            if (strArray != null && strArray.Length > 0)
            {
              for (int index2 = 0; index2 < strArray.Length; ++index2)
                gimmickEvent2.skills.Add(strArray[index2]);
            }
          }
          gimmickEvent2.condition.type = (GimmickEventTriggerType) gimmickEvent1.type;
          gimmickEvent2.condition.count = gimmickEvent1.count;
          gimmickEvent2.condition.grids = new List<Grid>();
          for (int index2 = 0; index2 < gimmickEvent1.x.Length && index2 < gimmickEvent1.y.Length; ++index2)
          {
            Grid grid = currentMap[gimmickEvent1.x[index2], gimmickEvent1.y[index2]];
            if (grid != null)
              gimmickEvent2.condition.grids.Add(grid);
          }
          this.GetConditionUnitByUnitID(gimmickEvent2.condition.units, gimmickEvent1.cu_iname);
          this.GetConditionUnitByUniqueName(gimmickEvent2.condition.units, gimmickEvent1.cu_tag, out is_starter1);
          if (string.IsNullOrEmpty(gimmickEvent1.cu_iname) && string.IsNullOrEmpty(gimmickEvent1.cu_tag) || gimmickEvent2.condition.units.Count != 0)
          {
            this.GetConditionUnitByUnitID(gimmickEvent2.condition.targets, gimmickEvent1.ct_iname);
            this.GetConditionUnitByUniqueName(gimmickEvent2.condition.targets, gimmickEvent1.ct_tag, out is_starter1);
            this.GetConditionTrickByTrickID(gimmickEvent2.condition.td_targets, gimmickEvent1.ct_iname);
            this.GetConditionTrickByTag(gimmickEvent2.condition.td_targets, gimmickEvent1.ct_tag);
            gimmickEvent2.condition.td_iname = gimmickEvent1.ct_iname;
            gimmickEvent2.condition.td_tag = gimmickEvent1.ct_tag;
            this.mGimmickEvents.Add(gimmickEvent2);
          }
        }
      }
    }

    public void RelinkTrickGimmickEvents()
    {
      foreach (GimmickEvent mGimmickEvent in this.mGimmickEvents)
      {
        if (mGimmickEvent.td_targets.Count != 0)
        {
          mGimmickEvent.td_targets.Clear();
          this.GetConditionTrickByTrickID(mGimmickEvent.td_targets, mGimmickEvent.td_iname);
          this.GetConditionTrickByTag(mGimmickEvent.td_targets, mGimmickEvent.td_tag);
        }
        if (mGimmickEvent.condition.td_targets.Count != 0)
        {
          mGimmickEvent.condition.td_targets.Clear();
          this.GetConditionTrickByTrickID(mGimmickEvent.condition.td_targets, mGimmickEvent.condition.td_iname);
          this.GetConditionTrickByTag(mGimmickEvent.condition.td_targets, mGimmickEvent.condition.td_tag);
        }
      }
    }

    public void GetConditionUnitByUnitID(List<Unit> results, string inames)
    {
      if (results == null || string.IsNullOrEmpty(inames))
        return;
      string[] strArray = inames.Split(',');
      if (strArray == null && strArray.Length == 0)
        return;
      for (int index1 = 0; index1 < strArray.Length; ++index1)
      {
        for (int index2 = 0; index2 < this.Units.Count; ++index2)
        {
          if (strArray[index1] == this.Units[index2].UnitParam.iname && !results.Contains(this.Units[index2]))
            results.Add(this.Units[index2]);
        }
      }
    }

    public void GetConditionUnitByUniqueName(List<Unit> results, string tags, out bool is_starter)
    {
      is_starter = false;
      if (results == null || string.IsNullOrEmpty(tags))
        return;
      string[] strArray = tags.Split(',');
      if (strArray == null && strArray.Length == 0)
        return;
      for (int index1 = 0; index1 < strArray.Length; ++index1)
      {
        for (int index2 = 0; index2 < this.Units.Count; ++index2)
        {
          if (this.CheckMatchUniqueName(this.Units[index2], strArray[index1]) && !results.Contains(this.Units[index2]))
            results.Add(this.Units[index2]);
          if (strArray[index1] == "starter")
            is_starter = true;
        }
      }
    }

    public void GetConditionTrickByTrickID(List<TrickData> results, string inames)
    {
      if (results == null || string.IsNullOrEmpty(inames))
        return;
      string[] strArray = inames.Split(',');
      if (strArray == null && strArray.Length == 0)
        return;
      List<TrickData> effectAll = TrickData.GetEffectAll();
      for (int index = 0; index < strArray.Length; ++index)
      {
        foreach (TrickData trickData in effectAll)
        {
          if (strArray[index] == trickData.TrickParam.Name && !results.Contains(trickData))
            results.Add(trickData);
        }
      }
    }

    public void GetConditionTrickByTag(List<TrickData> results, string tags)
    {
      if (results == null || string.IsNullOrEmpty(tags))
        return;
      string[] strArray = tags.Split(',');
      if (strArray == null && strArray.Length == 0)
        return;
      List<TrickData> effectAll = TrickData.GetEffectAll();
      for (int index = 0; index < strArray.Length; ++index)
      {
        foreach (TrickData td in effectAll)
        {
          if (this.IsMatchTrickTag(td, strArray[index]) && !results.Contains(td))
            results.Add(td);
        }
      }
    }

    private void GimmickEventDamageCount(Unit attacker, Unit defender)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      for (int index = 0; index < this.mGimmickEvents.Count; ++index)
      {
        GimmickEvent mGimmickEvent = this.mGimmickEvents[index];
        if (!mGimmickEvent.IsCompleted && mGimmickEvent.condition.type == GimmickEventTriggerType.DamageCount)
        {
          GimmickEventCondition condition = mGimmickEvent.condition;
          if ((condition.units.Count <= 0 || !attacker.IsUnitFlag(EUnitFlag.UnitTransformed) && condition.units.Contains(this.DtuFindOrgUnit(attacker))) && (!defender.IsUnitFlag(EUnitFlag.UnitTransformed) && condition.targets.Contains(this.DtuFindOrgUnit(defender))))
          {
            ++mGimmickEvent.count;
            if (mGimmickEvent.IsStarter && condition.count <= mGimmickEvent.count)
              mGimmickEvent.starter = attacker;
          }
        }
      }
    }

    private void GimmickEventDeadCount(Unit self, Unit target)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult) || target == null || !target.IsDead)
        return;
      for (int index = 0; index < this.mGimmickEvents.Count; ++index)
      {
        GimmickEvent mGimmickEvent = this.mGimmickEvents[index];
        if (!mGimmickEvent.IsCompleted && mGimmickEvent.condition.type == GimmickEventTriggerType.DeadUnit)
        {
          GimmickEventCondition condition = mGimmickEvent.condition;
          if ((condition.units.Count <= 0 || !self.IsUnitFlag(EUnitFlag.UnitTransformed) && condition.units.Contains(this.DtuFindOrgUnit(self))) && (!target.IsUnitFlag(EUnitFlag.UnitTransformed) && condition.targets.Contains(this.DtuFindOrgUnit(target))))
          {
            ++mGimmickEvent.count;
            if (mGimmickEvent.IsStarter && condition.count <= mGimmickEvent.count)
              mGimmickEvent.starter = self;
          }
        }
      }
    }

    public void GimmickEventHpLower(Unit defender)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      for (int index1 = 0; index1 < this.mGimmickEvents.Count; ++index1)
      {
        GimmickEvent mGimmickEvent = this.mGimmickEvents[index1];
        if (!mGimmickEvent.IsCompleted && (mGimmickEvent.condition.type == GimmickEventTriggerType.HpLower || mGimmickEvent.condition.type == GimmickEventTriggerType.HpLowerRatio))
        {
          GimmickEventCondition condition = mGimmickEvent.condition;
          if (!defender.IsUnitFlag(EUnitFlag.UnitTransformed) && condition.targets.Contains(this.DtuFindOrgUnit(defender)))
          {
            int num1 = 0;
            int num2 = 0;
            for (int index2 = 0; index2 < condition.targets.Count; ++index2)
            {
              Unit actUnit = this.DtuFindActUnit(condition.targets[index2]);
              if (actUnit != null)
              {
                switch (mGimmickEvent.condition.type)
                {
                  case GimmickEventTriggerType.HpLower:
                    num1 += (int) actUnit.CurrentStatus.param.hp;
                    break;
                  case GimmickEventTriggerType.HpLowerRatio:
                    num1 += (int) actUnit.CurrentStatus.param.hp * 100 / actUnit.MaximumStatusHp;
                    break;
                }
                ++num2;
              }
            }
            if (num2 != 0 && num1 / num2 <= condition.count)
              ++mGimmickEvent.count;
          }
        }
      }
    }

    public void GimmickEventTrickKillCount(TrickData trick_data)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult) || trick_data == null)
        return;
      foreach (GimmickEvent mGimmickEvent in this.mGimmickEvents)
      {
        if (!mGimmickEvent.IsCompleted && mGimmickEvent.condition.type == GimmickEventTriggerType.DeadUnit && mGimmickEvent.condition.td_targets.Contains(trick_data))
          ++mGimmickEvent.count;
      }
    }

    private void GimmickEventOnGrid()
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      for (int index1 = 0; index1 < this.mGimmickEvents.Count; ++index1)
      {
        GimmickEvent mGimmickEvent = this.mGimmickEvents[index1];
        if (!mGimmickEvent.IsCompleted && mGimmickEvent.IsStarter && (mGimmickEvent.starter == null && mGimmickEvent.condition.type == GimmickEventTriggerType.OnGrid))
        {
          for (int index2 = 0; index2 < mGimmickEvent.condition.grids.Count; ++index2)
          {
            int x = mGimmickEvent.condition.grids[index2].x;
            int y = mGimmickEvent.condition.grids[index2].y;
            for (int index3 = 0; index3 < this.Units.Count; ++index3)
            {
              Unit unit = this.Units[index3];
              if (!unit.IsGimmick && unit.CheckExistMap() && (mGimmickEvent.condition.units.Count <= 0 || !unit.IsUnitFlag(EUnitFlag.UnitTransformed) && mGimmickEvent.condition.units.Contains(this.DtuFindOrgUnit(unit))) && (unit.x == x && unit.y == y))
                mGimmickEvent.starter = unit;
            }
          }
        }
      }
    }

    private bool CheckEnableGimmickEvent(GimmickEvent gimmick)
    {
      if (gimmick.IsCompleted)
        return false;
      if (gimmick.condition.count != 0)
      {
        switch (gimmick.condition.type)
        {
          case GimmickEventTriggerType.HpLower:
          case GimmickEventTriggerType.HpLowerRatio:
            return gimmick.count > 0;
          default:
            return gimmick.condition.count <= gimmick.count;
        }
      }
      else
      {
        if (gimmick.condition.type != GimmickEventTriggerType.OnGrid)
          return false;
        for (int index1 = 0; index1 < gimmick.condition.grids.Count; ++index1)
        {
          int x = gimmick.condition.grids[index1].x;
          int y = gimmick.condition.grids[index1].y;
          for (int index2 = 0; index2 < this.Units.Count; ++index2)
          {
            Unit unit = this.Units[index2];
            if (!unit.IsGimmick && unit.CheckExistMap() && (gimmick.condition.units.Count <= 0 || !unit.IsUnitFlag(EUnitFlag.UnitTransformed) && gimmick.condition.units.Contains(this.DtuFindOrgUnit(unit))) && ((!gimmick.IsStarter || unit == gimmick.starter) && (unit.x == x && unit.y == y)))
              return true;
          }
        }
        return false;
      }
    }

    private void UpdateGimmickEventStart()
    {
      this.GimmickEventOnGrid();
      bool flag = true;
      while (flag)
      {
        flag = false;
        for (int index1 = 0; index1 < this.mGimmickEvents.Count; ++index1)
        {
          GimmickEvent mGimmickEvent = this.mGimmickEvents[index1];
          if (this.CheckEnableGimmickEvent(mGimmickEvent))
          {
            if (mGimmickEvent.IsStarter && mGimmickEvent.starter != null && !mGimmickEvent.targets.Contains(mGimmickEvent.starter))
              mGimmickEvent.targets.Add(mGimmickEvent.starter);
            int num = 0;
            for (int index2 = 0; index2 < mGimmickEvent.targets.Count; ++index2)
            {
              if (this.DtuFindActUnit(mGimmickEvent.targets[index2]).CheckExistMap())
                ++num;
            }
            if (num != 0)
            {
              switch (mGimmickEvent.ev_type)
              {
                case eGimmickEventType.USE_SKILL:
                  if (mGimmickEvent.users.Count > 0)
                  {
                    for (int index2 = 0; index2 < mGimmickEvent.users.Count; ++index2)
                    {
                      Unit actUnit1 = this.DtuFindActUnit(mGimmickEvent.users[index2]);
                      if (actUnit1.CheckExistMap())
                      {
                        for (int index3 = 0; index3 < mGimmickEvent.skills.Count; ++index3)
                        {
                          SkillData skill = actUnit1.GetSkillData(mGimmickEvent.skills[index3]);
                          if (skill == null)
                          {
                            skill = new SkillData();
                            skill.Setup(mGimmickEvent.skills[index3], 1, 1, (MasterParam) null);
                          }
                          LogSkill log = this.Log<LogSkill>();
                          log.self = actUnit1;
                          log.skill = skill;
                          if (mGimmickEvent.targets.Count == 1 && !skill.IsAllEffect())
                          {
                            log.pos.x = mGimmickEvent.targets[0].x;
                            log.pos.y = mGimmickEvent.targets[0].y;
                          }
                          else
                          {
                            log.pos.x = log.self.x;
                            log.pos.y = log.self.y;
                          }
                          log.is_append = !skill.IsCutin();
                          log.is_gimmick = true;
                          for (int index4 = 0; index4 < mGimmickEvent.targets.Count; ++index4)
                          {
                            Unit actUnit2 = this.DtuFindActUnit(mGimmickEvent.targets[index4]);
                            if (actUnit2.CheckExistMap())
                              log.SetSkillTarget(log.self, actUnit2);
                          }
                          this.ExecuteSkill(ESkillTiming.Used, log, skill);
                        }
                      }
                    }
                    break;
                  }
                  for (int index2 = 0; index2 < mGimmickEvent.targets.Count; ++index2)
                  {
                    Unit actUnit = this.DtuFindActUnit(mGimmickEvent.targets[index2]);
                    if (actUnit.CheckExistMap())
                    {
                      for (int index3 = 0; index3 < mGimmickEvent.skills.Count; ++index3)
                      {
                        Unit unit = actUnit;
                        SkillData skill = unit.GetSkillData(mGimmickEvent.skills[index3]);
                        if (skill == null)
                        {
                          skill = new SkillData();
                          skill.Setup(mGimmickEvent.skills[index3], 1, 1, (MasterParam) null);
                        }
                        LogSkill log = this.Log<LogSkill>();
                        log.self = unit;
                        log.skill = skill;
                        log.pos.x = log.self.x;
                        log.pos.y = log.self.y;
                        log.is_append = !skill.IsCutin();
                        log.is_gimmick = true;
                        log.SetSkillTarget(log.self, log.self);
                        this.ExecuteSkill(ESkillTiming.Used, log, skill);
                      }
                    }
                  }
                  break;
                case eGimmickEventType.UNIT_KILL:
                  for (int index2 = 0; index2 < mGimmickEvent.targets.Count; ++index2)
                  {
                    Unit actUnit = this.DtuFindActUnit(mGimmickEvent.targets[index2]);
                    if (actUnit.CheckExistMap())
                    {
                      actUnit.KeepHp = actUnit.CurrentStatus.param.hp;
                      actUnit.CurrentStatus.param.hp = (OInt) 0;
                      this.Dead((Unit) null, actUnit, DeadTypes.Damage, false);
                      flag = true;
                    }
                  }
                  break;
              }
              mGimmickEvent.IsCompleted = true;
            }
          }
        }
      }
    }

    private void UpdateGimmickEventTrick()
    {
      bool flag;
      do
      {
        flag = false;
        foreach (GimmickEvent mGimmickEvent in this.mGimmickEvents)
        {
          if (this.CheckEnableGimmickEvent(mGimmickEvent) && mGimmickEvent.td_targets.Count != 0 && mGimmickEvent.ev_type == eGimmickEventType.UNIT_KILL)
          {
            foreach (TrickData tdTarget in mGimmickEvent.td_targets)
              TrickData.RemoveEffect(tdTarget);
            mGimmickEvent.IsCompleted = true;
            flag = true;
          }
        }
      }
      while (flag);
    }

    private bool CheckMatchUniqueName(Unit self, string tag)
    {
      if (!string.IsNullOrEmpty(tag) && self != null && !self.IsUnitFlag(EUnitFlag.UnitTransformed))
      {
        self = this.DtuFindOrgUnit(self);
        if (tag == self.UniqueName)
          return true;
        if (tag == "pall")
          return self.Side == EUnitSide.Player;
        if (tag == "eall")
          return self.Side == EUnitSide.Enemy;
        if (tag == "nall")
          return self.Side == EUnitSide.Neutral;
      }
      return false;
    }

    private bool IsMatchTrickTag(TrickData td, string tag)
    {
      return !string.IsNullOrEmpty(tag) && tag == td.Tag;
    }

    private void UpdateEntryTriggers(UnitEntryTypes type, Unit target, SkillParam skill = null)
    {
      int num1 = (int) type;
      int num2 = 0;
      int num3 = 0;
      if (type == UnitEntryTypes.DecrementMember)
      {
        for (int index = 0; index < this.mUnits.Count; ++index)
        {
          Unit mUnit = this.mUnits[index];
          if (!mUnit.IsUnitFlag(EUnitFlag.UnitTransformed) && !mUnit.IsUnitFlag(EUnitFlag.IsDynamicTransform) && (!mUnit.IsGimmick && mUnit.IsDeadCondition()))
          {
            if (mUnit.Side == EUnitSide.Player)
              ++num2;
            else if (mUnit.Side == EUnitSide.Enemy)
              ++num3;
          }
        }
      }
      for (int index1 = 0; index1 < this.mUnits.Count; ++index1)
      {
        Unit mUnit1 = this.mUnits[index1];
        if ((!mUnit1.IsGimmick || mUnit1.IsBreakObj || mUnit1.EventTrigger != null && (mUnit1.EventTrigger.EventType == EEventType.Treasure || mUnit1.EventTrigger.EventType == EEventType.Gem) && mUnit1.CheckEventTrigger(mUnit1.EventTrigger.Trigger)) && (!mUnit1.IsEntry && !mUnit1.IsDead && (!mUnit1.IsSub && mUnit1.EntryTriggers != null)))
        {
          for (int index2 = 0; index2 < mUnit1.EntryTriggers.Count; ++index2)
          {
            UnitEntryTrigger entryTrigger = mUnit1.EntryTriggers[index2];
            if (!entryTrigger.on && entryTrigger.type == num1)
            {
              switch (type)
              {
                case UnitEntryTypes.DecrementMember:
                  if (mUnit1.Side == EUnitSide.Player)
                  {
                    entryTrigger.on = num2 >= entryTrigger.value;
                    continue;
                  }
                  if (mUnit1.Side == EUnitSide.Enemy)
                  {
                    entryTrigger.on = num3 >= entryTrigger.value;
                    continue;
                  }
                  continue;
                case UnitEntryTypes.DecrementHp:
                  if (this.CheckMatchUniqueName(target, entryTrigger.unit) && entryTrigger.value >= (int) target.CurrentStatus.param.hp)
                  {
                    entryTrigger.on = true;
                    continue;
                  }
                  continue;
                case UnitEntryTypes.DeadEnemy:
                  if (this.CheckMatchUniqueName(target, entryTrigger.unit))
                  {
                    if (target.IsGimmick && !target.IsBreakObj)
                    {
                      if (target.EventTrigger == null || target.EventTrigger.EventType != EEventType.Treasure && target.EventTrigger.EventType != EEventType.Gem || target.EventTrigger.Count != 0)
                        continue;
                    }
                    else if (target.IsUnitFlag(EUnitFlag.UnitTransformed) || !target.IsDead)
                      continue;
                    entryTrigger.on = true;
                    continue;
                  }
                  continue;
                case UnitEntryTypes.UsedSkill:
                  if (this.CheckMatchUniqueName(target, entryTrigger.unit) && skill != null && !(skill.iname != entryTrigger.skill))
                  {
                    entryTrigger.on = true;
                    continue;
                  }
                  continue;
                case UnitEntryTypes.OnGridEnemy:
                  if (this.CheckMatchUniqueName(target, entryTrigger.unit) && target.x == entryTrigger.x && target.y == entryTrigger.y)
                  {
                    entryTrigger.on = true;
                    continue;
                  }
                  continue;
                case UnitEntryTypes.WithdrawEnemy:
                  if (this.CheckMatchUniqueName(target, entryTrigger.unit) && !target.IsGimmick && (target.IsDead && target.IsUnitFlag(EUnitFlag.UnitWithdraw)))
                  {
                    entryTrigger.on = true;
                    continue;
                  }
                  continue;
                case UnitEntryTypes.DecrementUnit:
                  int num4 = 0;
                  for (int index3 = 0; index3 < this.mUnits.Count; ++index3)
                  {
                    Unit mUnit2 = this.mUnits[index3];
                    if (!mUnit2.IsUnitFlag(EUnitFlag.UnitTransformed) && !mUnit2.IsGimmick && (mUnit2.IsDead && this.CheckMatchUniqueName(mUnit2, entryTrigger.unit)))
                      ++num4;
                  }
                  entryTrigger.on = num4 >= entryTrigger.value;
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
    }

    private bool IsAllDead(EUnitSide side)
    {
      bool flag = true;
      switch (side)
      {
        case EUnitSide.Player:
          for (int index = 0; index < this.mPlayer.Count; ++index)
          {
            Unit unit = this.mPlayer[index];
            if (unit.IsEntry && !unit.IsGimmick)
              flag &= unit.IsDeadCondition();
          }
          break;
        case EUnitSide.Enemy:
          for (int index = 0; index < this.mEnemys[this.MapIndex].Count; ++index)
          {
            Unit unit = this.mEnemys[this.MapIndex][index];
            if (unit.IsEntry && !unit.IsGimmick)
              flag &= unit.IsDeadCondition();
          }
          break;
      }
      return flag;
    }

    public BattleCore.QuestResult GetQuestResult()
    {
      BattleMap currentMap = this.CurrentMap;
      if (this.CheckMonitorGoalUnit(currentMap.WinMonitorCondition))
        return BattleCore.QuestResult.Win;
      if (this.CheckMonitorGoalUnit(currentMap.LoseMonitorCondition))
        return BattleCore.QuestResult.Lose;
      if (this.CheckMonitorActionCount(currentMap.WinMonitorCondition))
        return BattleCore.QuestResult.Win;
      if (this.CheckMonitorActionCount(currentMap.LoseMonitorCondition))
        return BattleCore.QuestResult.Lose;
      if (this.CheckMonitorWithdrawUnit(currentMap.WinMonitorCondition))
        return BattleCore.QuestResult.Win;
      if (this.CheckMonitorWithdrawUnit(currentMap.LoseMonitorCondition))
        return BattleCore.QuestResult.Lose;
      int mWinTriggerCount = this.mWinTriggerCount;
      int loseTriggerCount = this.mLoseTriggerCount;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (this.mUnits[index].CheckWinEventTrigger())
          ++mWinTriggerCount;
        if (this.mUnits[index].CheckLoseEventTrigger())
          ++loseTriggerCount;
      }
      if ((int) this.mQuestParam.win != 0 && (int) this.mQuestParam.win <= mWinTriggerCount)
        return BattleCore.QuestResult.Win;
      if (this.IsMultiVersus)
      {
        if (this.IsVSForceWin)
          return this.IsVSForceWinComfirm ? BattleCore.QuestResult.Win : BattleCore.QuestResult.Pending;
        if (this.IsAllDead(EUnitSide.Player) && this.IsAllDead(EUnitSide.Enemy))
          return BattleCore.QuestResult.Draw;
        if (this.IsAllDead(EUnitSide.Player))
          return BattleCore.QuestResult.Lose;
        if (this.IsAllDead(EUnitSide.Enemy))
          return BattleCore.QuestResult.Win;
      }
      if (this.mEnemys != null)
      {
        bool flag = true;
        for (int index = 0; index < this.mEnemys[this.MapIndex].Count; ++index)
        {
          Unit unit = this.mEnemys[this.MapIndex][index];
          if ((unit.IsEntry || unit.NeedDead) && !unit.IsGimmick)
            flag &= unit.IsDead;
        }
        if (flag)
          return BattleCore.QuestResult.Win;
      }
      if (this.mQuestParam.OverClockTimeWin > 0 && (int) this.mClockTimeTotal > this.mQuestParam.OverClockTimeWin)
        return BattleCore.QuestResult.Win;
      if ((int) this.mQuestParam.lose != 0 && (int) this.mQuestParam.lose <= loseTriggerCount)
        return BattleCore.QuestResult.Lose;
      if (this.mPlayer != null)
      {
        bool flag = true;
        for (int index = 0; index < this.mPlayer.Count; ++index)
        {
          Unit unit = this.mPlayer[index];
          if (unit.IsEntry && !unit.IsGimmick)
            flag &= unit.IsDeadCondition();
        }
        if (flag)
          return BattleCore.QuestResult.Lose;
      }
      if (this.mQuestParam.OverClockTimeLose > 0 && (int) this.mClockTimeTotal > this.mQuestParam.OverClockTimeLose)
        return BattleCore.QuestResult.Lose;
      if (this.mQuestParam.type == QuestTypes.Arena)
      {
        if (this.mArenaActionCount == 0U)
          return BattleCore.QuestResult.Lose;
        if (this.mIsArenaSkip)
          return this.mArenaCalcResult;
      }
      if (!this.IsMultiVersus || this.RemainVersusTurnCount != 0U)
        return BattleCore.QuestResult.Pending;
      int num1 = this.mPlayer.Count<Unit>((Func<Unit, bool>) (unit => !unit.IsDead));
      int num2 = this.mEnemys[this.MapIndex].Count<Unit>((Func<Unit, bool>) (unit => !unit.IsDead));
      if (num1 > num2)
        return BattleCore.QuestResult.Win;
      if (num2 > num1)
        return BattleCore.QuestResult.Lose;
      int hpRate1 = this.GetHpRate(EUnitSide.Player);
      int hpRate2 = this.GetHpRate(EUnitSide.Enemy);
      if (hpRate1 > hpRate2)
        return BattleCore.QuestResult.Win;
      return hpRate2 > hpRate1 ? BattleCore.QuestResult.Lose : BattleCore.QuestResult.Draw;
    }

    private int GetHpRate(EUnitSide side)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      switch (side)
      {
        case EUnitSide.Player:
          for (int index = 0; index < this.mPlayer.Count; ++index)
          {
            num1 += (int) this.mPlayer[index].MaximumStatus.param.hp;
            num2 += (int) this.mPlayer[index].CurrentStatus.param.hp;
          }
          break;
        case EUnitSide.Enemy:
          List<Unit> mEnemy = this.mEnemys[this.MapIndex];
          for (int index = 0; index < mEnemy.Count; ++index)
          {
            if (mEnemy[index].Side == EUnitSide.Enemy)
            {
              num1 += (int) mEnemy[index].MaximumStatus.param.hp;
              num2 += (int) mEnemy[index].CurrentStatus.param.hp;
            }
          }
          break;
      }
      if (num1 > 0)
        num3 = num2 * 100 / num1;
      return num3;
    }

    public List<int> GetFinishHp(EUnitSide side)
    {
      List<int> intList = new List<int>();
      switch (side)
      {
        case EUnitSide.Player:
          for (int index = 0; index < this.mPlayer.Count; ++index)
            intList.Add((int) this.mPlayer[index].CurrentStatus.param.hp);
          break;
        case EUnitSide.Enemy:
          List<Unit> mEnemy = this.mEnemys[this.MapIndex];
          for (int index = 0; index < mEnemy.Count; ++index)
          {
            if (mEnemy[index].Side == EUnitSide.Enemy)
              intList.Add((int) mEnemy[index].CurrentStatus.param.hp);
          }
          break;
      }
      return intList;
    }

    public int GetDeadCount(EUnitSide side)
    {
      int num = 0;
      switch (side)
      {
        case EUnitSide.Player:
          for (int index = 0; index < this.mPlayer.Count; ++index)
          {
            if (this.mPlayer[index].IsDeadCondition())
              ++num;
          }
          break;
        case EUnitSide.Enemy:
          List<Unit> mEnemy = this.mEnemys[this.MapIndex];
          for (int index = 0; index < mEnemy.Count; ++index)
          {
            if (mEnemy[index].Side == EUnitSide.Enemy && mEnemy[index].IsDeadCondition())
              ++num;
          }
          break;
      }
      return num;
    }

    public List<string> GetPlayerName()
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.mPlayer.Count; ++index)
        stringList.Add(this.mPlayer[index].UnitName + "_" + (object) this.mPlayer[index].OwnerPlayerIndex);
      return stringList;
    }

    public bool CheckMonitorActionCount(QuestMonitorCondition cond)
    {
      if (cond.actions.Count > 0)
      {
        for (int index1 = 0; index1 < cond.actions.Count; ++index1)
        {
          UnitMonitorCondition action = cond.actions[index1];
          if (!string.IsNullOrEmpty(action.tag))
          {
            if (action.tag == "pall")
            {
              for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
              {
                if (this.mUnits[index2].Side == EUnitSide.Player && this.CheckMonitorActionCountCondition(this.mUnits[index2], action))
                  return true;
              }
              continue;
            }
            if (action.tag == "eall")
            {
              for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
              {
                if (this.mUnits[index2].Side == EUnitSide.Enemy && this.CheckMonitorActionCountCondition(this.mUnits[index2], action))
                  return true;
              }
              continue;
            }
            if (action.tag == "nall")
            {
              for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
              {
                if (this.mUnits[index2].Side == EUnitSide.Neutral && this.CheckMonitorActionCountCondition(this.mUnits[index2], action))
                  return true;
              }
              continue;
            }
            if (this.CheckMonitorActionCountCondition(this.FindUnitByUniqueName(action.tag), action))
              return true;
          }
          if (!string.IsNullOrEmpty(action.iname))
          {
            for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
            {
              Unit mUnit = this.mUnits[index2];
              if (!(mUnit.UnitParam.iname != action.iname) && this.CheckMonitorActionCountCondition(mUnit, action))
                return true;
            }
          }
        }
      }
      return false;
    }

    private bool CheckMonitorActionCountCondition(Unit unit, UnitMonitorCondition monitor)
    {
      return unit != null && !unit.IsGimmick && (unit.CheckExistMap() && unit.ActionCount >= monitor.turn);
    }

    public bool CheckEnableRemainingActionCount(QuestMonitorCondition cond)
    {
      if (cond == null || cond.actions.Count == 0)
        return false;
      for (int index1 = 0; index1 < cond.actions.Count; ++index1)
      {
        UnitMonitorCondition action = cond.actions[index1];
        if (action.turn > 0)
        {
          if (!string.IsNullOrEmpty(action.tag) && (action.tag == "pall" || action.tag == "eall" || (action.tag == "nall" || this.FindUnitByUniqueName(action.tag) != null)))
            return true;
          if (!string.IsNullOrEmpty(action.iname))
          {
            for (int index2 = 0; index2 < this.Units.Count; ++index2)
            {
              if (this.Units[index2].UnitParam.iname == action.iname)
                return true;
            }
          }
        }
      }
      return false;
    }

    public int GetRemainingActionCount(QuestMonitorCondition cond)
    {
      if (cond == null || cond.actions.Count == 0)
        return -1;
      int val1 = (int) byte.MaxValue;
      for (int index1 = 0; index1 < cond.actions.Count; ++index1)
      {
        UnitMonitorCondition action = cond.actions[index1];
        if (!string.IsNullOrEmpty(action.tag))
        {
          if (action.tag == "pall")
          {
            for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
            {
              if (this.mUnits[index2].Side == EUnitSide.Player)
                val1 = Math.Min(val1, action.turn - this.mUnits[index2].ActionCount);
            }
          }
          if (action.tag == "eall")
          {
            for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
            {
              if (this.mUnits[index2].Side == EUnitSide.Enemy)
                val1 = Math.Min(val1, action.turn - this.mUnits[index2].ActionCount);
            }
          }
          if (action.tag == "nall")
          {
            for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
            {
              if (this.mUnits[index2].Side == EUnitSide.Neutral)
                val1 = Math.Min(val1, action.turn - this.mUnits[index2].ActionCount);
            }
          }
          Unit unitByUniqueName = this.FindUnitByUniqueName(action.tag);
          if (unitByUniqueName != null)
            val1 = Math.Min(val1, action.turn - unitByUniqueName.ActionCount);
        }
        else if (!string.IsNullOrEmpty(action.iname))
        {
          for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
          {
            if (!(this.mUnits[index2].UnitParam.iname != action.iname))
              val1 = Math.Min(val1, action.turn - this.mUnits[index2].ActionCount);
          }
        }
      }
      if (val1 != (int) byte.MaxValue)
        return Math.Max(val1, 0);
      return -1;
    }

    private bool CheckMonitorGoalUnit(QuestMonitorCondition cond)
    {
      if (cond.goals.Count > 0)
      {
        for (int index1 = 0; index1 < cond.goals.Count; ++index1)
        {
          UnitMonitorCondition goal = cond.goals[index1];
          if (!string.IsNullOrEmpty(goal.tag))
          {
            if (goal.tag == "pall")
            {
              for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
              {
                if (this.mUnits[index2].Side == EUnitSide.Player && this.CheckMonitorGoalCondition(this.mUnits[index2], goal))
                  return true;
              }
              continue;
            }
            if (goal.tag == "eall")
            {
              for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
              {
                if (this.mUnits[index2].Side == EUnitSide.Enemy && this.CheckMonitorGoalCondition(this.mUnits[index2], goal))
                  return true;
              }
              continue;
            }
            if (goal.tag == "nall")
            {
              for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
              {
                if (this.mUnits[index2].Side == EUnitSide.Neutral && this.CheckMonitorGoalCondition(this.mUnits[index2], goal))
                  return true;
              }
              continue;
            }
            if (this.CheckMonitorGoalCondition(this.FindUnitByUniqueName(goal.tag), goal))
              return true;
          }
          if (!string.IsNullOrEmpty(goal.iname))
          {
            for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
            {
              Unit mUnit = this.mUnits[index2];
              if (!(mUnit.UnitParam.iname != goal.iname) && this.CheckMonitorGoalCondition(mUnit, goal))
                return true;
            }
          }
        }
      }
      return false;
    }

    private bool CheckMonitorGoalCondition(Unit unit, UnitMonitorCondition monitor)
    {
      return unit != null && !unit.IsGimmick && (unit.CheckExistMap() && unit.x == monitor.x) && (unit.y == monitor.y && (monitor.turn <= 0 || monitor.turn >= unit.ActionCount));
    }

    private bool CheckMonitorWithdrawUnit(QuestMonitorCondition cond)
    {
      if (cond.withdraw.Count != 0)
      {
        for (int index = 0; index < cond.withdraw.Count; ++index)
        {
          UnitMonitorCondition monitorCondition = cond.withdraw[index];
          if (!string.IsNullOrEmpty(monitorCondition.tag))
          {
            if (monitorCondition.tag == "pall")
            {
              using (List<Unit>.Enumerator enumerator = this.mUnits.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  Unit current = enumerator.Current;
                  if (current.Side == EUnitSide.Player && this.CheckMonitorWithdrawCondition(current))
                    return true;
                }
                continue;
              }
            }
            else if (monitorCondition.tag == "eall")
            {
              using (List<Unit>.Enumerator enumerator = this.mUnits.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  Unit current = enumerator.Current;
                  if (current.Side == EUnitSide.Enemy && this.CheckMonitorWithdrawCondition(current))
                    return true;
                }
                continue;
              }
            }
            else if (monitorCondition.tag == "nall")
            {
              using (List<Unit>.Enumerator enumerator = this.mUnits.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  Unit current = enumerator.Current;
                  if (current.Side == EUnitSide.Neutral && this.CheckMonitorWithdrawCondition(current))
                    return true;
                }
                continue;
              }
            }
            else if (this.CheckMonitorWithdrawCondition(this.FindUnitByUniqueName(monitorCondition.tag)))
              return true;
          }
          if (!string.IsNullOrEmpty(monitorCondition.iname))
          {
            foreach (Unit mUnit in this.mUnits)
            {
              if (!(mUnit.UnitParam.iname != monitorCondition.iname) && this.CheckMonitorWithdrawCondition(mUnit))
                return true;
            }
          }
        }
      }
      return false;
    }

    private bool CheckMonitorWithdrawCondition(Unit unit)
    {
      return unit != null && unit.IsDead && unit.IsUnitFlag(EUnitFlag.UnitWithdraw);
    }

    public bool IsBonusObjectiveComplete(QuestBonusObjective bonus, ref int takeoverProgress)
    {
      switch (bonus.Type)
      {
        case EMissionType.KillAllEnemy:
          for (int index = 0; index < this.Units.Count; ++index)
          {
            if (this.Units[index].Side == EUnitSide.Enemy && (!this.Units[index].IsDead || this.Units[index].IsUnitFlag(EUnitFlag.UnitWithdraw)))
              return false;
          }
          return true;
        case EMissionType.NoDeath:
          if (this.IsAllAlive())
            return true;
          takeoverProgress = 1;
          return false;
        case EMissionType.LimitedTurn:
          takeoverProgress = this.ActionCountTotal;
          int result1;
          if (int.TryParse(bonus.TypeParam, out result1))
            return this.ActionCountTotal <= result1;
          return false;
        case EMissionType.MaxSkillCount:
          takeoverProgress = this.mNumUsedSkills;
          int result2;
          if (int.TryParse(bonus.TypeParam, out result2))
            return this.mNumUsedSkills <= result2;
          return false;
        case EMissionType.MaxItemCount:
          takeoverProgress = this.mNumUsedItems;
          int result3;
          if (int.TryParse(bonus.TypeParam, out result3))
            return this.mNumUsedItems <= result3;
          return false;
        case EMissionType.MaxPartySize:
          int num1 = 0;
          for (int index = 0; index < this.Units.Count; ++index)
          {
            if (!this.Units[index].IsUnitFlag(EUnitFlag.IsDynamicTransform) && this.Units[index].Side == EUnitSide.Player && this.Units[index].IsPartyMember)
              ++num1;
          }
          int result4;
          if (int.TryParse(bonus.TypeParam, out result4) && num1 <= result4)
            return true;
          takeoverProgress = 1;
          return false;
        case EMissionType.LimitedUnitElement:
          List<EElement> eelementList = new List<EElement>();
          string[] strArray1 = bonus.TypeParam.Split(',');
          if (strArray1 != null)
          {
            for (int index = 0; index < strArray1.Length; ++index)
            {
              try
              {
                EElement eelement = (EElement) Enum.Parse(typeof (EElement), strArray1[index], true);
                eelementList.Add(eelement);
              }
              catch (Exception ex)
              {
                return false;
              }
            }
          }
          for (int index1 = 0; index1 < this.mPlayer.Count; ++index1)
          {
            if (this.mPlayer[index1].IsPartyMember)
            {
              bool flag = false;
              for (int index2 = 0; index2 < eelementList.Count; ++index2)
              {
                if (this.mPlayer[index1].Element == eelementList[index2])
                {
                  flag = true;
                  break;
                }
              }
              if (!flag)
              {
                takeoverProgress = 1;
                return false;
              }
            }
          }
          return true;
        case EMissionType.LimitedUnitID:
          for (int index = 0; index < this.Units.Count; ++index)
          {
            if (this.Units[index].Side == EUnitSide.Player && this.Units[index].IsPartyMember && this.Units[index].UnitParam.iname == bonus.TypeParam)
              return true;
          }
          takeoverProgress = 1;
          return false;
        case EMissionType.NoMercenary:
          bool flag1 = this.Friend == null;
          if (flag1 && this.IsOrdeal)
          {
            foreach (Unit unit in this.Units)
            {
              if (unit.IsUnitFlag(EUnitFlag.IsHelp))
              {
                flag1 = false;
                break;
              }
            }
          }
          return flag1;
        case EMissionType.Killstreak:
          takeoverProgress = this.mMaxKillstreak;
          int result5;
          if (int.TryParse(bonus.TypeParam, out result5))
            return this.mMaxKillstreak >= result5;
          return false;
        case EMissionType.TotalHealHPMax:
          takeoverProgress = this.mTotalHeal;
          int result6;
          if (int.TryParse(bonus.TypeParam, out result6))
            return this.mTotalHeal <= result6;
          return false;
        case EMissionType.TotalHealHPMin:
          takeoverProgress = this.mTotalHeal;
          int result7;
          if (int.TryParse(bonus.TypeParam, out result7))
            return this.mTotalHeal >= result7;
          return false;
        case EMissionType.TotalDamagesTakenMax:
          takeoverProgress = this.mTotalDamagesTaken;
          int result8;
          if (int.TryParse(bonus.TypeParam, out result8))
            return this.mTotalDamagesTaken <= result8;
          return false;
        case EMissionType.TotalDamagesTakenMin:
          takeoverProgress = this.mTotalDamagesTaken;
          int result9;
          if (int.TryParse(bonus.TypeParam, out result9))
            return this.mTotalDamagesTaken >= result9;
          return false;
        case EMissionType.TotalDamagesMax:
          takeoverProgress = this.mTotalDamages;
          int result10;
          if (int.TryParse(bonus.TypeParam, out result10))
            return this.mTotalDamages <= result10;
          return false;
        case EMissionType.TotalDamagesMin:
          takeoverProgress = this.mTotalDamages;
          int result11;
          if (int.TryParse(bonus.TypeParam, out result11))
            return this.mTotalDamages >= result11;
          return false;
        case EMissionType.LimitedCT:
          takeoverProgress = (int) this.mClockTimeTotal;
          int result12;
          if (int.TryParse(bonus.TypeParam, out result12))
            return (int) this.mClockTimeTotal <= result12;
          return false;
        case EMissionType.LimitedContinue:
          takeoverProgress = this.mContinueCount;
          int result13;
          if (int.TryParse(bonus.TypeParam, out result13))
            return this.mContinueCount <= result13;
          return false;
        case EMissionType.NoNpcDeath:
          return this.IsNPCAllAlive();
        case EMissionType.TargetKillstreak:
          string[] strArray2 = bonus.TypeParam.Split(',');
          int num2;
          if (strArray2.Length >= 2 && this.mMaxTargetKillstreakDict.TryGetValue(strArray2[0].Trim(), out num2))
          {
            takeoverProgress = num2;
            int result14;
            if (int.TryParse(strArray2[1], out result14))
              return num2 >= result14;
            return false;
          }
          break;
        case EMissionType.NoTargetDeath:
          string str = bonus.TypeParam.Trim();
          bool flag2 = false;
          foreach (Unit unit in this.Units)
          {
            if (unit.Side == EUnitSide.Enemy && unit.UnitParam.iname == str)
              flag2 |= unit.IsDead;
          }
          return !flag2;
        case EMissionType.BreakObjClashMax:
          UnitParam unitParam1 = (UnitParam) null;
          int result15 = 1;
          string[] strArray3 = bonus.TypeParam.Split(',');
          if (strArray3 != null)
          {
            if (strArray3.Length >= 1)
              unitParam1 = MonoSingleton<GameManager>.Instance.GetUnitParam(strArray3[0].Trim());
            if (strArray3.Length >= 2)
              int.TryParse(strArray3[1].Trim(), out result15);
          }
          if (unitParam1 != null)
          {
            int unitDeadCount = this.getUnitDeadCount(unitParam1.iname);
            takeoverProgress = unitDeadCount;
            if (unitDeadCount <= result15)
              return true;
          }
          return false;
        case EMissionType.BreakObjClashMin:
          UnitParam unitParam2 = (UnitParam) null;
          int result16 = 1;
          string[] strArray4 = bonus.TypeParam.Split(',');
          if (strArray4 != null)
          {
            if (strArray4.Length >= 1)
              unitParam2 = MonoSingleton<GameManager>.Instance.GetUnitParam(strArray4[0].Trim());
            if (strArray4.Length >= 2)
              int.TryParse(strArray4[1].Trim(), out result16);
          }
          if (unitParam2 != null)
          {
            int unitDeadCount = this.getUnitDeadCount(unitParam2.iname);
            takeoverProgress = unitDeadCount;
            if (unitDeadCount >= result16)
              return true;
          }
          return false;
        case EMissionType.WithdrawUnit:
          int num3 = 0;
          foreach (Unit unit in this.Units)
          {
            if (unit.IsUnitFlag(EUnitFlag.UnitWithdraw) && !(unit.UnitParam.iname != bonus.TypeParam))
              ++num3;
          }
          return num3 != 0;
        case EMissionType.UseMercenary:
          bool flag3 = this.Friend != null;
          if (!flag3 && this.IsOrdeal)
          {
            foreach (Unit unit in this.Units)
            {
              if (unit.IsUnitFlag(EUnitFlag.IsHelp))
              {
                flag3 = true;
                break;
              }
            }
          }
          return flag3;
        case EMissionType.LimitedUnitID_MainOnly:
          for (int index = 0; index < this.Units.Count; ++index)
          {
            if (this.mStartingMembers.Contains(this.Units[index]) && this.Units[index] != this.Friend && (this.Units[index].Side == EUnitSide.Player && this.Units[index].IsPartyMember) && this.Units[index].UnitParam.iname == bonus.TypeParam)
              return true;
          }
          takeoverProgress = 1;
          return false;
        case EMissionType.OnlyTargetArtifactType:
          if (this.IsMissionClearArtifactTypeConditions(bonus, false))
            return true;
          takeoverProgress = 1;
          return false;
        case EMissionType.OnlyTargetArtifactType_MainOnly:
          if (this.IsMissionClearArtifactTypeConditions(bonus, true))
            return true;
          takeoverProgress = 1;
          return false;
        case EMissionType.OnlyTargetJobs:
          if (this.IsMissionClearPartyMemberJobConditions(bonus, false))
            return true;
          takeoverProgress = 1;
          return false;
        case EMissionType.OnlyTargetJobs_MainOnly:
          if (this.IsMissionClearPartyMemberJobConditions(bonus, true))
            return true;
          takeoverProgress = 1;
          return false;
        case EMissionType.OnlyTargetUnitBirthplace:
          if (this.IsMissionClearUnitBirthplaceConditions(bonus, false))
            return true;
          takeoverProgress = 1;
          return false;
        case EMissionType.OnlyTargetUnitBirthplace_MainOnly:
          if (this.IsMissionClearUnitBirthplaceConditions(bonus, true))
            return true;
          takeoverProgress = 1;
          return false;
        case EMissionType.OnlyTargetSex:
          if (this.IsMissionClearUnitSexConditions(bonus, false))
            return true;
          takeoverProgress = 1;
          return false;
        case EMissionType.OnlyTargetSex_MainOnly:
          if (this.IsMissionClearUnitSexConditions(bonus, true))
            return true;
          takeoverProgress = 1;
          return false;
        case EMissionType.OnlyHeroUnit:
          if (this.IsMissionClearOnlyHeroConditions(false))
            return true;
          takeoverProgress = 1;
          return false;
        case EMissionType.OnlyHeroUnit_MainOnly:
          if (this.IsMissionClearOnlyHeroConditions(true))
            return true;
          takeoverProgress = 1;
          return false;
        case EMissionType.Finisher:
          if (this.mFinisherIname == bonus.TypeParam)
            return true;
          takeoverProgress = 1;
          return false;
        case EMissionType.TotalGetTreasureCount:
          int num4 = 0;
          for (int index = 0; index < this.mTreasures.Count; ++index)
          {
            if (this.mTreasures[index].EventTrigger.Count <= 0)
              ++num4;
          }
          takeoverProgress = num4;
          int result17;
          if (int.TryParse(bonus.TypeParam, out result17))
            return num4 >= result17;
          return false;
        case EMissionType.KillstreakByUsingTargetItem:
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(bonus.TypeParam);
          if (!this.mSkillExecLogs.ContainsKey(itemParam.skill))
            return false;
          takeoverProgress = this.mSkillExecLogs[itemParam.skill].kill_count;
          return this.mSkillExecLogs[itemParam.skill].kill_count > 0;
        case EMissionType.KillstreakByUsingTargetSkill:
          if (!this.mSkillExecLogs.ContainsKey(bonus.TypeParam))
            return false;
          takeoverProgress = this.mSkillExecLogs[bonus.TypeParam].kill_count;
          return this.mSkillExecLogs[bonus.TypeParam].kill_count > 0;
        case EMissionType.MaxPartySize_IgnoreFriend:
          int num5 = 0;
          for (int index = 0; index < this.Units.Count; ++index)
          {
            if (!this.Units[index].IsUnitFlag(EUnitFlag.IsDynamicTransform) && this.Units[index].Side == EUnitSide.Player && (this.Units[index].IsPartyMember && this.Units[index] != this.Friend))
              ++num5;
          }
          int result18;
          if (int.TryParse(bonus.TypeParam, out result18) && num5 <= result18)
            return true;
          takeoverProgress = 1;
          return false;
        case EMissionType.NoAutoMode:
          if (!this.mIsUseAutoPlayMode)
            return true;
          takeoverProgress = 1;
          return false;
        case EMissionType.NoDeath_NoContinue:
          if (this.IsAllAlive())
            return this.ContinueCount <= 0;
          return false;
        case EMissionType.OnlyTargetUnits:
          return this.IsMissionClearOnlyTargetUnitsConditions(bonus, false);
        case EMissionType.OnlyTargetUnits_MainOnly:
          return this.IsMissionClearOnlyTargetUnitsConditions(bonus, true);
        case EMissionType.LimitedTurn_Leader:
          takeoverProgress = this.Leader.ActionCount;
          int result19;
          if (int.TryParse(bonus.TypeParam, out result19))
            return this.Leader.ActionCount <= result19;
          return false;
        case EMissionType.NoDeathTargetNpcUnits:
          return this.IsNPCAlive(bonus.TypeParam.Split(','));
        case EMissionType.UseTargetSkill:
          if (!this.mSkillExecLogs.ContainsKey(bonus.TypeParam))
            return false;
          takeoverProgress = this.mSkillExecLogs[bonus.TypeParam].use_count;
          return this.mSkillExecLogs[bonus.TypeParam].use_count > 0;
        case EMissionType.TotalKillstreakCount:
          int totalKillCount = this.GetTotalKillCount();
          takeoverProgress = totalKillCount;
          int result20;
          if (int.TryParse(bonus.TypeParam, out result20))
            return totalKillCount >= result20;
          return false;
        case EMissionType.TotalGetGemCount_Over:
          int num6 = 0;
          for (int index = 0; index < this.mGems.Count; ++index)
          {
            if (this.mGems[index].EventTrigger.Count <= 0)
              ++num6;
          }
          takeoverProgress = num6;
          int result21;
          if (int.TryParse(bonus.TypeParam, out result21))
            return num6 >= result21;
          return false;
        case EMissionType.TotalGetGemCount_Less:
          int num7 = 0;
          for (int index = 0; index < this.mGems.Count; ++index)
          {
            if (this.mGems[index].EventTrigger.Count <= 0)
              ++num7;
          }
          takeoverProgress = num7;
          int result22;
          if (int.TryParse(bonus.TypeParam, out result22))
            return num7 <= result22;
          return false;
        case EMissionType.TeamPartySizeMax_IncMercenary:
          int result23 = 0;
          int.TryParse(bonus.TypeParam, out result23);
          return this.IsTeamPartySizeMax(result23, true);
        case EMissionType.TeamPartySizeMax_NoMercenary:
          int result24 = 0;
          int.TryParse(bonus.TypeParam, out result24);
          return this.IsTeamPartySizeMax(result24, false);
        case EMissionType.ChallengeCountMax:
          int result25 = 0;
          int.TryParse(bonus.TypeParam, out result25);
          takeoverProgress = 1;
          return this.IsTeamPartySizeMax(result25, false);
        case EMissionType.DeathCountMax:
          int playerUnitDeadCount = this.GetPlayerUnitDeadCount();
          int result26 = 0;
          int.TryParse(bonus.TypeParam, out result26);
          takeoverProgress = playerUnitDeadCount;
          return playerUnitDeadCount <= result26;
        case EMissionType.DamageOver:
          int result27 = 0;
          int.TryParse(bonus.TypeParam, out result27);
          takeoverProgress = this.mMaxDamage;
          return this.mMaxDamage >= result27;
        case EMissionType.SurviveUnit:
          bool flag4 = true;
          string[] strArray5 = bonus.TypeParam.Split(',');
          if (strArray5 != null)
          {
            for (int index1 = 0; index1 < strArray5.Length; ++index1)
            {
              bool flag5 = false;
              for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
              {
                if (this.mUnits[index2].UnitParam.iname == strArray5[index1].Trim())
                {
                  flag5 = true;
                  if (this.mUnits[index2].IsDead)
                  {
                    flag4 = false;
                    break;
                  }
                  break;
                }
              }
              if (!flag5)
              {
                flag4 = false;
                break;
              }
              if (!flag4)
                break;
            }
          }
          if (!flag4)
            takeoverProgress = 1;
          return flag4;
        case EMissionType.KillTargetEnemy:
          int num8 = 0;
          string[] strArray6 = bonus.TypeParam.Split(',');
          if (strArray6 == null)
          {
            DebugUtility.LogError("[" + bonus.Type.ToString() + "]には不正な条件値が入力されています");
            return false;
          }
          if (strArray6.Length != 2)
          {
            DebugUtility.LogError("[" + bonus.Type.ToString() + "]には不正な条件値が入力されています");
            return false;
          }
          for (int index1 = 0; index1 < strArray6.Length - 1; ++index1)
          {
            int num9 = 0;
            for (int index2 = 0; index2 < this.mEnemys.Length; ++index2)
            {
              for (int index3 = 0; index3 < this.mEnemys[index2].Count; ++index3)
              {
                if (this.mEnemys[index2][index3].UnitParam.iname == strArray6[index1].Trim() && this.mEnemys[index2][index3].IsDead)
                  ++num9;
              }
            }
            num8 += num9;
          }
          takeoverProgress = num8;
          int result28;
          if (int.TryParse(strArray6[strArray6.Length - 1], out result28))
          {
            if (result28 <= num8)
              return true;
          }
          else
            DebugUtility.LogError("[" + bonus.Type.ToString() + "]条件値の最後に整数値を入力してください。");
          return false;
        case EMissionType.TakenDamageLessEqual:
          takeoverProgress = this.mTotalDamagesTakenCount;
          int result29;
          if (int.TryParse(bonus.TypeParam, out result29))
            return this.mTotalDamagesTakenCount <= result29;
          return false;
        case EMissionType.TakenDamageGreatorEqual:
          takeoverProgress = this.mTotalDamagesTakenCount;
          int result30;
          if (int.TryParse(bonus.TypeParam, out result30))
            return this.mTotalDamagesTakenCount >= result30;
          return false;
        case EMissionType.LimitedTurnLessEqualPartyOnly:
          takeoverProgress = this.mActionCounts[0];
          int result31;
          if (int.TryParse(bonus.TypeParam, out result31))
            return this.mActionCounts[0] <= result31;
          return false;
        case EMissionType.LimitedTurnGreatorEqualPartyOnly:
          takeoverProgress = this.mActionCounts[0];
          int result32;
          if (int.TryParse(bonus.TypeParam, out result32))
            return this.mActionCounts[0] >= result32;
          return false;
        case EMissionType.KillEnemy:
          int num10 = 0;
          for (int index1 = 0; index1 < this.mEnemys.Length; ++index1)
          {
            for (int index2 = 0; index2 < this.mEnemys[index1].Count; ++index2)
            {
              if (this.IsUnit(this.mEnemys[index1][index2]) && this.mEnemys[index1][index2].IsDead)
                ++num10;
            }
          }
          takeoverProgress = num10;
          int result33;
          if (int.TryParse(bonus.TypeParam, out result33))
          {
            if (result33 <= num10)
              return true;
          }
          else
            DebugUtility.LogError("[" + bonus.Type.ToString() + "]条件値には整数値を文字列として入力してください。");
          return false;
        case EMissionType.BreakObj:
          int num11 = 0;
          for (int index1 = 0; index1 < this.mEnemys.Length; ++index1)
          {
            for (int index2 = 0; index2 < this.mEnemys[index1].Count; ++index2)
            {
              if (this.mEnemys[index1][index2].IsBreakObj && this.mEnemys[index1][index2].IsDead)
                ++num11;
            }
          }
          takeoverProgress = num11;
          int result34;
          if (int.TryParse(bonus.TypeParam, out result34))
          {
            if (result34 <= num11)
              return true;
          }
          else
            DebugUtility.LogError("[" + bonus.Type.ToString() + "]条件値には整数値を文字列として入力してください。");
          return false;
        case EMissionType.LimitedTurnLessEqualPartyAndNPC:
          int num12 = 0 + this.mActionCounts[0] + this.mActionCounts[1];
          takeoverProgress = num12;
          int result35;
          if (int.TryParse(bonus.TypeParam, out result35))
            return num12 <= result35;
          return false;
        case EMissionType.LimitedTurnGreatorEqualPartyAndNPC:
          int num13 = 0 + this.mActionCounts[0] + this.mActionCounts[1];
          takeoverProgress = num13;
          int result36;
          if (int.TryParse(bonus.TypeParam, out result36))
            return num13 >= result36;
          return false;
      }
      return false;
    }

    private bool IsUnit(Unit unit)
    {
      if (unit.UnitType != EUnitType.Unit)
        return unit.UnitType == EUnitType.EventUnit;
      return true;
    }

    private bool IsTeamPartySizeMax(int max_num, bool is_inc_mercenary)
    {
      bool flag = true;
      for (int index = 0; index < this.mMaxTeamId; ++index)
      {
        int num = 0;
        foreach (Unit unit in this.Units)
        {
          if (!unit.IsUnitFlag(EUnitFlag.IsDynamicTransform) && unit.Side == EUnitSide.Player && (unit.IsPartyMember && unit.TeamId == index) && (!unit.IsUnitFlag(EUnitFlag.IsHelp) || is_inc_mercenary))
            ++num;
        }
        if (num > max_num)
        {
          flag = false;
          break;
        }
      }
      return flag;
    }

    public bool IsAllAlive()
    {
      for (int index = 0; index < this.Units.Count; ++index)
      {
        Unit unit = this.Units[index];
        if (unit.Side == EUnitSide.Player && unit.IsPartyMember && (unit.IsDead && !unit.IsUnitFlag(EUnitFlag.UnitChanged)) && (!unit.IsUnitFlag(EUnitFlag.UnitTransformed) && !unit.IsUnitFlag(EUnitFlag.IsDynamicTransform)))
          return false;
      }
      return true;
    }

    public bool IsNPCAllAlive()
    {
      foreach (Unit unit in this.Units)
      {
        if (unit.Side == EUnitSide.Player && !unit.IsPartyMember && (unit.IsDead && !unit.IsUnitFlag(EUnitFlag.UnitChanged)) && (!unit.IsUnitFlag(EUnitFlag.UnitTransformed) && !unit.IsUnitFlag(EUnitFlag.IsDynamicTransform)))
          return false;
      }
      return true;
    }

    public bool IsNPCAlive(string[] target_unit_inames)
    {
      if (target_unit_inames.Length <= 0)
        return this.IsNPCAllAlive();
      foreach (Unit unit in this.Units)
      {
        if (((IEnumerable<string>) target_unit_inames).Contains<string>(unit.UnitParam.iname) && unit.Side == EUnitSide.Player && (!unit.IsPartyMember && unit.IsDead) && (!unit.IsUnitFlag(EUnitFlag.UnitChanged) && !unit.IsUnitFlag(EUnitFlag.UnitTransformed) && !unit.IsUnitFlag(EUnitFlag.IsDynamicTransform)))
          return false;
      }
      return true;
    }

    private bool IsMissionClearOnlyTargetUnitsConditions(QuestBonusObjective bonus, bool check_main_member_only = false)
    {
      string[] strArray = bonus.TypeParam.Split(',');
      for (int index = 0; index < this.Units.Count; ++index)
      {
        if (!this.Units[index].IsUnitFlag(EUnitFlag.IsDynamicTransform) && this.Units[index].Side == EUnitSide.Player && this.Units[index].IsPartyMember && (!((IEnumerable<string>) strArray).Contains<string>(this.Units[index].UnitParam.iname) || check_main_member_only && (this.Units[index] == this.Friend || !this.mStartingMembers.Contains(this.Units[index]))))
          return false;
      }
      return true;
    }

    private bool IsMissionClearArtifactTypeConditions(QuestBonusObjective bonus, bool check_main_member_only = false)
    {
      string[] strArray = bonus.TypeParam.Split(',');
      if (strArray.Length <= 0)
        return false;
      for (int index = 0; index < this.Units.Count; ++index)
      {
        if (!this.Units[index].IsUnitFlag(EUnitFlag.IsDynamicTransform) && this.Units[index].Side == EUnitSide.Player && this.Units[index].IsPartyMember)
        {
          ArtifactParam artifactParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(this.Units[index].Job.Param.artifact);
          if (artifactParam == null || !((IEnumerable<string>) strArray).Contains<string>(artifactParam.tag) || check_main_member_only && (this.Units[index] == this.Friend || !this.mStartingMembers.Contains(this.Units[index])))
            return false;
        }
      }
      return true;
    }

    private bool IsMissionClearPartyMemberJobConditions(QuestBonusObjective bonus, bool check_main_member_only = false)
    {
      string[] strArray = bonus.TypeParam.Split(',');
      if (strArray.Length <= 0)
        return false;
      for (int index = 0; index < this.Units.Count; ++index)
      {
        if (!this.Units[index].IsUnitFlag(EUnitFlag.IsDynamicTransform) && this.Units[index].Side == EUnitSide.Player && this.Units[index].IsPartyMember && (!((IEnumerable<string>) strArray).Contains<string>(this.Units[index].Job.JobID) || check_main_member_only && (this.Units[index] == this.Friend || !this.mStartingMembers.Contains(this.Units[index]))))
          return false;
      }
      return true;
    }

    private bool IsMissionClearOnlyHeroConditions(bool check_main_member_only = false)
    {
      for (int index = 0; index < this.Units.Count; ++index)
      {
        if (!this.Units[index].IsUnitFlag(EUnitFlag.IsDynamicTransform) && this.Units[index].Side == EUnitSide.Player && this.Units[index].IsPartyMember && (!this.Units[index].UnitParam.IsHero() || check_main_member_only && (this.Units[index] == this.Friend || !this.mStartingMembers.Contains(this.Units[index]))))
          return false;
      }
      return true;
    }

    private bool IsMissionClearUnitBirthplaceConditions(QuestBonusObjective bonus, bool check_main_member_only = false)
    {
      for (int index = 0; index < this.Units.Count; ++index)
      {
        if (!this.Units[index].IsUnitFlag(EUnitFlag.IsDynamicTransform) && this.Units[index].Side == EUnitSide.Player && this.Units[index].IsPartyMember && ((string) this.Units[index].UnitParam.birth != bonus.TypeParam || check_main_member_only && (this.Units[index] == this.Friend || !this.mStartingMembers.Contains(this.Units[index]))))
          return false;
      }
      return true;
    }

    private bool IsMissionClearUnitSexConditions(QuestBonusObjective bonus, bool check_main_member_only = false)
    {
      int result;
      int.TryParse(bonus.TypeParam, out result);
      for (int index = 0; index < this.Units.Count; ++index)
      {
        if (!this.Units[index].IsUnitFlag(EUnitFlag.IsDynamicTransform) && this.Units[index].Side == EUnitSide.Player && this.Units[index].IsPartyMember && (this.Units[index].UnitParam.sex != (ESex) result || check_main_member_only && (this.Units[index] == this.Friend || !this.mStartingMembers.Contains(this.Units[index]))))
          return false;
      }
      return true;
    }

    private bool IsKillAllEnemyOnceBattle()
    {
      int totalKillCount = this.GetTotalKillCount();
      int num = 0;
      for (int index = 0; index < this.Units.Count; ++index)
      {
        if (!this.Units[index].IsUnitFlag(EUnitFlag.IsDynamicTransform))
        {
          if (this.Units[index].Side == EUnitSide.Enemy && (!this.Units[index].IsDead || this.Units[index].IsUnitFlag(EUnitFlag.UnitWithdraw)))
            return false;
          ++num;
        }
      }
      return totalKillCount == num;
    }

    private int getUnitDeadCount(string unit_iname)
    {
      if (string.IsNullOrEmpty(unit_iname))
        return 0;
      int num = 0;
      foreach (Unit unit in this.Units)
      {
        if (!unit.IsUnitFlag(EUnitFlag.IsDynamicTransform) && unit.IsDead && !(unit.UnitParam.iname != unit_iname))
          ++num;
      }
      return num;
    }

    private int GetTotalKillCount()
    {
      int num = 0;
      for (int index = 0; index < this.Units.Count; ++index)
      {
        if (!this.Units[index].IsUnitFlag(EUnitFlag.IsDynamicTransform) && this.Units[index].Side == EUnitSide.Player)
          num += this.Units[index].KillCount;
      }
      return num;
    }

    private int GetPlayerUnitDeadCount()
    {
      int num = 0;
      for (int index = 0; index < this.Units.Count; ++index)
      {
        Unit unit = this.Units[index];
        if (unit.Side == EUnitSide.Player && unit.IsPartyMember && (unit.IsDead && !unit.IsUnitFlag(EUnitFlag.UnitChanged)) && (!unit.IsUnitFlag(EUnitFlag.UnitTransformed) && !unit.IsUnitFlag(EUnitFlag.IsDynamicTransform)))
          ++num;
      }
      return num;
    }

    private void CalcQuestRecord()
    {
      this.DtuReturn();
      this.UnitChangeReturn();
      this.mRecord.result = this.GetQuestResult();
      this.mRecord.playerexp = (OInt) this.mQuestParam.pexp;
      this.mRecord.gold = (OInt) this.mQuestParam.gold;
      this.mRecord.unitexp = (OInt) this.mQuestParam.uexp;
      this.mRecord.multicoin = (OInt) this.mQuestParam.mcoin;
      this.mRecord.items.Clear();
      this.mRecord.bonusFlags = 0;
      this.mRecord.allBonusFlags = 0;
      this.mRecord.bonusCount = 0;
      if (this.mRecord.takeoverProgressList != null)
        this.mRecord.takeoverProgressList.Clear();
      int index1 = -1;
      int num1 = 0;
      bool flag1 = false;
      if (this.mQuestParam.type == QuestTypes.Tower)
        flag1 = true;
      if ((this.mRecord.result == BattleCore.QuestResult.Win || flag1) && this.mQuestParam.bonusObjective != null)
      {
        if (this.mQuestParam.type == QuestTypes.Tower)
        {
          for (int index2 = 0; index2 < this.mQuestParam.bonusObjective.Length; ++index2)
            this.mRecord.takeoverProgressList.Add(0);
        }
        for (int index2 = 0; index2 < this.mQuestParam.bonusObjective.Length; ++index2)
        {
          if (this.mQuestParam.bonusObjective[index2].Type == EMissionType.MissionAllCompleteAtOnce)
            index1 = index2;
          if (this.mRecord.result != BattleCore.QuestResult.Lose || this.mQuestParam.bonusObjective[index2].IsMissionTypeAllowLose())
          {
            int takeoverProgress = 0;
            bool flag2 = this.IsBonusObjectiveComplete(this.mQuestParam.bonusObjective[index2], ref takeoverProgress);
            if (index2 < this.mRecord.takeoverProgressList.Count)
            {
              if (this.mQuestParam.CheckMissionValueIsDefault(index2))
              {
                this.mRecord.takeoverProgressList[index2] = takeoverProgress;
              }
              else
              {
                this.mRecord.takeoverProgressList[index2] = this.mQuestParam.GetMissionValue(index2);
                List<int> takeoverProgressList;
                int index3;
                (takeoverProgressList = this.mRecord.takeoverProgressList)[index3 = index2] = takeoverProgressList[index3] + takeoverProgress;
              }
            }
            if (this.mQuestParam.bonusObjective[index2].IsProgressMission())
              flag2 = this.mRecord.result != BattleCore.QuestResult.Lose && this.mQuestParam.bonusObjective[index2].CheckMissionValueAchievable(this.mRecord.takeoverProgressList[index2]);
            if (flag2)
            {
              this.mRecord.allBonusFlags |= 1 << index2;
              ++num1;
            }
            if ((this.mQuestParam.clear_missions & 1 << index2) == 0 && flag2)
            {
              this.SetReward(this.mQuestParam.bonusObjective[index2]);
              this.mRecord.bonusFlags |= 1 << index2;
            }
          }
        }
        this.mRecord.bonusCount = this.mQuestParam.bonusObjective.Length;
        if (index1 >= 0 && this.mQuestParam.bonusObjective.Length - num1 <= 1)
        {
          this.mRecord.allBonusFlags |= 1 << index1;
          int num2 = num1 + 1;
          if ((this.mQuestParam.clear_missions & 1 << index1) == 0)
          {
            this.SetReward(this.mQuestParam.bonusObjective[index1]);
            this.mRecord.bonusFlags |= 1 << index1;
          }
        }
      }
      this.GainUnitSteal(this.mRecord);
      this.GainUnitDrop(this.mRecord, false);
      this.mRecord.units.Clear();
      this.GainRankMatchItem();
      this.GainFreeVersusItem();
      this.GainInspSkill();
    }

    public void AddReward(RewardType rewardType, string iname)
    {
      switch (rewardType)
      {
        case RewardType.Artifact:
          ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(iname);
          if (artifactParam == null)
            break;
          this.mRecord.artifacts.Add(artifactParam);
          break;
        case RewardType.ConceptCard:
          ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(iname);
          if (conceptCardParam == null)
            break;
          this.mRecord.items.Add(new BattleCore.DropItemParam(conceptCardParam));
          break;
        default:
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(iname);
          if (itemParam == null)
            break;
          this.mRecord.items.Add(new BattleCore.DropItemParam(itemParam));
          break;
      }
    }

    private void SetReward(QuestBonusObjective bonus)
    {
      if (bonus.itemType == RewardType.Nothing)
        return;
      if (bonus.itemType == RewardType.Artifact)
      {
        ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.FirstOrDefault<ArtifactParam>((Func<ArtifactParam, bool>) (arti => arti.iname == bonus.item));
        if (artifactParam == null)
          return;
        for (int index = 0; index < bonus.itemNum; ++index)
          this.mRecord.artifacts.Add(artifactParam);
      }
      else if (bonus.itemType == RewardType.ConceptCard)
      {
        ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(bonus.item);
        if (conceptCardParam == null)
          return;
        for (int index = 0; index < bonus.itemNum; ++index)
          this.mRecord.items.Add(new BattleCore.DropItemParam(conceptCardParam));
      }
      else
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(bonus.item);
        if (itemParam == null)
          return;
        for (int index = 0; index < bonus.itemNum; ++index)
          this.mRecord.items.Add(new BattleCore.DropItemParam(itemParam));
      }
    }

    public void GainUnitDrop(BattleCore.Record record, bool waitDirection = false)
    {
      int length = this.mEntryUnitMax - this.mNpcStartIndex;
      if (length == 0)
        return;
      if (record.drops == null)
        record.drops = new OInt[length];
      Array.Clear((Array) record.drops, 0, length);
      int mNpcStartIndex = this.mNpcStartIndex;
      int index1 = 0;
      while (mNpcStartIndex < this.mEntryUnitMax)
      {
        Unit mAllUnit = this.mAllUnits[mNpcStartIndex];
        if (mAllUnit.CheckItemDrop(waitDirection))
        {
          Unit.UnitDrop drop = mAllUnit.Drop;
          for (int index2 = 0; index2 < drop.items.Count; ++index2)
          {
            for (int index3 = 0; index3 < (int) drop.items[index2].num; ++index3)
            {
              if (drop.items[index2].isItem)
                record.items.Add(new BattleCore.DropItemParam(drop.items[index2].itemParam)
                {
                  mIsSecret = (bool) drop.items[index2].is_secret
                });
              else if (drop.items[index2].isConceptCard)
                record.items.Add(new BattleCore.DropItemParam(drop.items[index2].conceptCardParam)
                {
                  mIsSecret = (bool) drop.items[index2].is_secret
                });
              else
                DebugUtility.LogError("不明なドロップ品");
            }
          }
          BattleCore.Record record1 = record;
          record1.gold = (OInt) ((int) record1.gold + (int) drop.gold);
          record.drops[index1] = (OInt) 1;
        }
        ++mNpcStartIndex;
        ++index1;
      }
    }

    public void GainUnitSteal(BattleCore.Record record)
    {
      int length = this.mEntryUnitMax - this.mNpcStartIndex;
      if (length == 0)
        return;
      if (record.item_steals == null)
        record.item_steals = new OInt[length];
      Array.Clear((Array) record.item_steals, 0, length);
      if (record.gold_steals == null)
        record.gold_steals = new OInt[length];
      Array.Clear((Array) record.gold_steals, 0, length);
      int mNpcStartIndex = this.mNpcStartIndex;
      int index1 = 0;
      while (mNpcStartIndex < this.mEntryUnitMax)
      {
        Unit mAllUnit = this.mAllUnits[mNpcStartIndex];
        if (mAllUnit.IsGimmick)
          break;
        Unit.UnitSteal steal = mAllUnit.Steal;
        if (steal.is_item_steeled)
        {
          for (int index2 = 0; index2 < steal.items.Count; ++index2)
            record.items.Add(new BattleCore.DropItemParam(steal.items[index2].itemParam));
          record.item_steals[index1] = (OInt) 1;
        }
        if (steal.is_gold_steeled)
        {
          BattleCore.Record record1 = record;
          record1.gold = (OInt) ((int) record1.gold + (int) steal.gold);
          record.gold_steals[index1] = (OInt) 1;
        }
        ++mNpcStartIndex;
        ++index1;
      }
    }

    private void GainConvertedGold()
    {
      if (this.mRecord.items == null)
        return;
      Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
      for (int index1 = 0; index1 < this.mRecord.items.Count; ++index1)
      {
        if (this.mRecord.items[index1].itemParam != null)
        {
          string iname = this.mRecord.items[index1].itemParam.iname;
          if (dictionary1.ContainsKey(iname))
          {
            Dictionary<string, int> dictionary2;
            string index2;
            (dictionary2 = dictionary1)[index2 = iname] = dictionary2[index2] + 1;
          }
          else
            dictionary1[iname] = 1;
        }
      }
      foreach (KeyValuePair<string, int> keyValuePair in dictionary1)
      {
        int num1 = keyValuePair.Value;
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(keyValuePair.Key);
        if (itemParam != null)
        {
          ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(keyValuePair.Key);
          if (itemDataByItemId != null)
            num1 += itemDataByItemId.Num;
          int num2 = num1 - itemParam.cap;
          if (num2 > 0)
          {
            BattleCore.Record mRecord = this.mRecord;
            mRecord.gold = (OInt) ((int) mRecord.gold + num2 * itemParam.sell);
          }
        }
      }
    }

    private void GainRankMatchItem()
    {
      if (!this.IsMultiVersus || GlobalVars.SelectedMultiPlayVersusType != VERSUS_TYPE.Tower)
        return;
      BattleCore.QuestResult questResult = this.GetQuestResult();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      List<string> Items = new List<string>();
      List<int> Nums = new List<int>();
      instance.GetTowerMatchItems(player.VersusTowerFloor - 1, Items, Nums, questResult == BattleCore.QuestResult.Win);
      instance.GetVersusTopFloorItems(player.VersusTowerFloor - 1, Items, Nums);
      for (int index1 = 0; index1 < Items.Count; ++index1)
      {
        ItemParam itemParam = instance.GetItemParam(Items[index1]);
        if (itemParam != null)
        {
          for (int index2 = 0; index2 < Nums[index1]; ++index2)
            this.mRecord.items.Add(new BattleCore.DropItemParam(itemParam));
        }
      }
    }

    private void GainFreeVersusItem()
    {
      if (!this.IsMultiVersus || GlobalVars.SelectedMultiPlayVersusType != VERSUS_TYPE.Free || this.GetQuestResult() != BattleCore.QuestResult.Win)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!instance.IsVSFirstWinRewardRecived)
      {
        VersusFirstWinBonusParam vsFirstWinBonus = instance.GetVSFirstWinBonus(GlobalVars.VersusFreeMatchTime);
        if (vsFirstWinBonus != null)
          this.SetVersusReward(vsFirstWinBonus.rewards);
      }
      int streakcnt1 = instance.VS_StreakWinCnt_Now + 1;
      VersusStreakWinBonusParam vsStreakWinBonus1 = instance.GetVSStreakWinBonus(streakcnt1, STREAK_JUDGE.Now, GlobalVars.VersusFreeMatchTime);
      if (vsStreakWinBonus1 != null)
        this.SetVersusReward(vsStreakWinBonus1.rewards);
      int streakcnt2 = instance.VS_StreakWinCnt_NowAllPriod + 1;
      if (instance.VS_StreakWinCnt_BestAllPriod >= streakcnt2)
        return;
      VersusStreakWinBonusParam vsStreakWinBonus2 = instance.GetVSStreakWinBonus(streakcnt2, STREAK_JUDGE.AllPriod, GlobalVars.VersusFreeMatchTime);
      if (vsStreakWinBonus2 == null)
        return;
      this.SetVersusReward(vsStreakWinBonus2.rewards);
    }

    private void SetVersusReward(VersusWinBonusRewardParam[] rewards)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      for (int index1 = 0; index1 < rewards.Length; ++index1)
      {
        VersusWinBonusRewardParam reward = rewards[index1];
        switch (reward.type)
        {
          case VERSUS_REWARD_TYPE.Item:
          case VERSUS_REWARD_TYPE.Coin:
            ItemParam itemParam = instance.GetItemParam(reward.iname);
            if (itemParam != null)
            {
              for (int index2 = 0; index2 < reward.num; ++index2)
                this.mRecord.items.Add(new BattleCore.DropItemParam(itemParam));
              break;
            }
            break;
          case VERSUS_REWARD_TYPE.Gold:
            BattleCore.Record mRecord = this.mRecord;
            mRecord.gold = (OInt) ((int) mRecord.gold + reward.num);
            break;
        }
      }
    }

    public void SetGiftReward(GiftData gift)
    {
      if (gift == null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!string.IsNullOrEmpty(gift.iname))
      {
        if (gift.CheckGiftTypeIncluded(GiftTypes.Item))
        {
          ItemParam itemParam = instance.GetItemParam(gift.iname);
          if (itemParam == null)
            return;
          for (int index = 0; index < gift.num; ++index)
            this.mRecord.items.Add(new BattleCore.DropItemParam(itemParam));
        }
        else
        {
          if (!gift.CheckGiftTypeIncluded(GiftTypes.ConceptCard))
            return;
          ConceptCardParam conceptCardParam = instance.GetConceptCardParam(gift.iname);
          if (conceptCardParam == null)
            return;
          for (int index = 0; index < gift.num; ++index)
            this.mRecord.items.Add(new BattleCore.DropItemParam(conceptCardParam));
        }
      }
      else if (gift.gold > 0)
      {
        BattleCore.Record mRecord = this.mRecord;
        mRecord.gold = (OInt) ((int) mRecord.gold + gift.gold);
      }
      else
      {
        if (gift.coin <= 0)
          return;
        ItemParam itemParam = instance.GetItemParam("$COIN");
        if (itemParam == null)
          return;
        for (int index = 0; index < gift.coin; ++index)
          this.mRecord.items.Add(new BattleCore.DropItemParam(itemParam));
      }
    }

    private void GainInspSkill()
    {
      BattleCore.Record mRecord = this.mRecord;
      mRecord.mInspSkillInsList.Clear();
      mRecord.mInspSkillUseList.Clear();
      for (int index1 = 0; index1 < this.mPlayer.Count; ++index1)
      {
        Unit unit = this.mPlayer[index1];
        if (unit.IsPartyMember && !unit.IsUnitFlag(EUnitFlag.IsHelp))
        {
          for (int index2 = 0; index2 < unit.InspInsList.Count; ++index2)
          {
            Unit.UnitInsp inspIns = unit.InspInsList[index2];
            if ((bool) inspIns.mValid)
            {
              BattleCore.Record.InspSkill inspSkill1 = mRecord.mInspSkillInsList.Find((Predicate<BattleCore.Record.InspSkill>) (d => d.mUnitData == unit.UnitData));
              if (inspSkill1 != null)
              {
                inspSkill1.mUnitInspList.Add(inspIns);
              }
              else
              {
                BattleCore.Record.InspSkill inspSkill2 = new BattleCore.Record.InspSkill(unit.UnitData, inspIns);
                mRecord.mInspSkillInsList.Add(inspSkill2);
              }
            }
          }
          for (int index2 = 0; index2 < unit.InspUseList.Count; ++index2)
          {
            Unit.UnitInsp inspUse = unit.InspUseList[index2];
            BattleCore.Record.InspSkill inspSkill1 = mRecord.mInspSkillUseList.Find((Predicate<BattleCore.Record.InspSkill>) (d => d.mUnitData == unit.UnitData));
            if (inspSkill1 != null)
            {
              inspSkill1.mUnitInspList.Add(inspUse);
            }
            else
            {
              BattleCore.Record.InspSkill inspSkill2 = new BattleCore.Record.InspSkill(unit.UnitData, inspUse);
              mRecord.mInspSkillUseList.Add(inspSkill2);
            }
          }
        }
      }
    }

    public void SetInspSkillIns(BattleCore.Json_BtlInspSlot[] sins)
    {
      if (sins == null || sins.Length == 0)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!(bool) ((UnityEngine.Object) instance))
        return;
      BattleCore.Record mRecord = this.mRecord;
      mRecord.mInspResultInspList.Clear();
      for (int index1 = 0; index1 < sins.Length; ++index1)
      {
        BattleCore.Json_BtlInspSlot sin = sins[index1];
        UnitData unitDataByUniqueId = instance.Player.FindUnitDataByUniqueID((long) sin.uiid);
        if (unitDataByUniqueId != null)
        {
          for (int index2 = 0; index2 < sin.artifact.Length; ++index2)
          {
            BattleCore.Json_BtlInspArtifactSlot bias = sin.artifact[index2];
            ArtifactData artifact = Array.Find<ArtifactData>(unitDataByUniqueId.CurrentJob.ArtifactDatas, (Predicate<ArtifactData>) (d => (long) d.UniqueID == (long) bias.iid));
            if (artifact != null)
            {
              InspirationSkillData inspirationSkillDataBySlot = artifact.GetInspirationSkillDataBySlot(bias.slot);
              if (inspirationSkillDataBySlot != null && inspirationSkillDataBySlot.AbilityData != null)
                mRecord.mInspResultInspList.Add(new BattleCore.Record.InspResult(unitDataByUniqueId, artifact, inspirationSkillDataBySlot.AbilityData));
            }
          }
        }
      }
    }

    public void SetInspSkillLvUp(BattleCore.Json_BtlInsp[] levelup_sins)
    {
      if (levelup_sins == null || levelup_sins.Length == 0)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!(bool) ((UnityEngine.Object) instance))
        return;
      BattleCore.Record mRecord = this.mRecord;
      mRecord.mInspResultLvUpList.Clear();
      for (int index1 = 0; index1 < levelup_sins.Length; ++index1)
      {
        BattleCore.Json_BtlInsp levelupSin = levelup_sins[index1];
        UnitData unitDataByUniqueId = instance.Player.FindUnitDataByUniqueID((long) levelupSin.uiid);
        if (unitDataByUniqueId != null)
        {
          for (int index2 = 0; index2 < levelupSin.artifact.Length; ++index2)
          {
            BattleCore.Json_BtlInspArtifact bia = levelupSin.artifact[index2];
            ArtifactData artifact = Array.Find<ArtifactData>(unitDataByUniqueId.CurrentJob.ArtifactDatas, (Predicate<ArtifactData>) (d => (long) d.UniqueID == (long) bia.iid));
            if (artifact != null)
            {
              InspirationSkillData inspirationSkillData = artifact.GetCurrentInspirationSkillData();
              if (inspirationSkillData != null && inspirationSkillData.AbilityData != null)
                mRecord.mInspResultLvUpList.Add(new BattleCore.Record.InspResult(unitDataByUniqueId, artifact, inspirationSkillData.AbilityData));
            }
          }
        }
      }
    }

    public Unit FindPlayerUnitByUniqueID(int uiid)
    {
      for (int index = 0; index < this.mPlayer.Count; ++index)
      {
        Unit unit = this.mPlayer[index];
        if (unit.UnitData.UniqueID == (long) uiid)
          return unit;
      }
      return (Unit) null;
    }

    public void SetGenesisBossResult(Json_Gift[] gifts)
    {
      BattleCore.Record mRecord = this.mRecord;
      mRecord.mGenesisBossResultReward = (Json_Gift) null;
      if (gifts == null || gifts.Length == 0)
        return;
      mRecord.mGenesisBossResultReward = gifts[0];
    }

    public BattleCore.Record GetQuestRecord()
    {
      return this.mRecord;
    }

    private bool CheckJudgeBattle()
    {
      switch (this.GetQuestResult())
      {
        case BattleCore.QuestResult.Win:
        case BattleCore.QuestResult.Lose:
        case BattleCore.QuestResult.Draw:
          return true;
        default:
          return false;
      }
    }

    public bool CheckNextMap()
    {
      return this.mMapIndex < this.mMap.Count - 1;
    }

    public int GetGems(Unit unit)
    {
      return unit.Gems;
    }

    public void SetGems(Unit unit, int gems)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      unit.Gems = Math.Max(Math.Min(gems, (int) unit.MaximumStatus.param.mp), 0);
    }

    private int AddGems(Unit unit, int gems)
    {
      if (!this.IsBattleFlag(EBattleFlag.PredictResult))
        unit.Gems = Math.Max(Math.Min(unit.Gems + gems, (int) unit.MaximumStatus.param.mp), 0);
      return unit.Gems;
    }

    private int SubGems(Unit unit, int gems)
    {
      if (!this.IsBattleFlag(EBattleFlag.PredictResult))
        unit.Gems = Math.Max(Math.Min(unit.Gems - gems, (int) unit.MaximumStatus.param.mp), 0);
      return unit.Gems;
    }

    public Grid GetUnitGridPosition(Unit unit)
    {
      if (unit == null)
        return (Grid) null;
      return this.CurrentMap?[unit.x, unit.y];
    }

    public Grid GetUnitGridPosition(int x, int y)
    {
      return this.CurrentMap?[x, y];
    }

    public Unit FindUnitByUniqueName(string uniqname)
    {
      if (string.IsNullOrEmpty(uniqname))
        return (Unit) null;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (!(uniqname != this.mUnits[index].UniqueName))
          return this.DtuFindActUnit(this.mUnits[index]);
      }
      return (Unit) null;
    }

    public Unit FindUnitAtGrid(int x, int y)
    {
      return this.FindUnitAtGrid(this.CurrentMap[x, y]);
    }

    public Unit FindUnitAtGrid(Grid grid)
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (!this.mUnits[index].IsGimmick && this.mUnits[index].CheckCollision(grid))
          return this.mUnits[index];
      }
      return (Unit) null;
    }

    public Unit DirectFindUnitAtGrid(Grid grid)
    {
      foreach (Unit mUnit in this.mUnits)
      {
        if (!mUnit.IsGimmick)
        {
          bool flag = false;
          if (mUnit == this.CurrentUnit)
          {
            SceneBattle instance = SceneBattle.Instance;
            if ((UnityEngine.Object) instance != (UnityEngine.Object) null)
            {
              TacticsUnitController unitController = instance.FindUnitController(mUnit);
              if ((bool) ((UnityEngine.Object) unitController))
              {
                IntVector2 intVector2 = instance.CalcCoord(unitController.CenterPosition);
                if (mUnit.CheckCollisionDirect(intVector2.x, intVector2.y, grid.x, grid.y, true))
                  return mUnit;
                flag = true;
              }
            }
          }
          if (!flag && mUnit.CheckCollision(grid.x, grid.y, true))
            return mUnit;
        }
      }
      return (Unit) null;
    }

    public bool IsTargetUnitForGridMap(Unit unit, GridMap<bool> grid_map)
    {
      if (unit == null || grid_map == null)
        return false;
      if (unit.IsNormalSize)
        return grid_map.get(unit.x, unit.y);
      for (int minColX = unit.MinColX; minColX < unit.MaxColX; ++minColX)
      {
        for (int minColY = unit.MinColY; minColY < unit.MaxColY; ++minColY)
        {
          int x = unit.x + minColX;
          int y = unit.y + minColY;
          if (grid_map.isValid(x, y) && grid_map.get(x, y))
            return true;
        }
      }
      return false;
    }

    public Unit FindUnitAtGridIgnoreSide(Grid grid, EUnitSide ignoreSide)
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (!this.mUnits[index].IsGimmick && this.mUnits[index].Side == ignoreSide && this.mUnits[index].CheckCollision(grid))
          return this.mUnits[index];
      }
      return (Unit) null;
    }

    public Unit FindGimmickAtGrid(int x, int y, bool is_valid_disable = false)
    {
      return this.FindGimmickAtGrid(this.CurrentMap[x, y], is_valid_disable, (Unit) null);
    }

    public Unit FindGimmickAtGrid(Grid grid, bool is_valid_disable = false, Unit ignore_unit = null)
    {
      if (grid == null)
        return (Unit) null;
      Unit unit = (Unit) null;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (this.mUnits[index].IsGimmick && (ignore_unit == null || this.mUnits[index] != ignore_unit) && ((is_valid_disable || !this.mUnits[index].IsDisableGimmick()) && this.mUnits[index].CheckCollision(grid)))
        {
          if (this.mUnits[index].IsBreakObj)
            return this.mUnits[index];
          unit = this.mUnits[index];
        }
      }
      return unit;
    }

    public bool CommandWait(EUnitDirection dir)
    {
      if (!this.CurrentUnit.IsDead)
        this.CurrentUnit.Direction = dir;
      return this.CommandWait(false);
    }

    public bool CommandWait(bool is_skip_event = false)
    {
      this.DebugAssert(this.IsMapCommand, "マップコマンド中のみコール可");
      this.Log<LogMapWait>().self = this.CurrentUnit;
      if (!is_skip_event)
      {
        this.TrickActionEndEffect(this.CurrentUnit, true);
        this.ExecuteEventTriggerOnGrid(this.CurrentUnit, EEventTrigger.Stop);
      }
      this.InternalLogUnitEnd();
      return true;
    }

    private void InternalLogUnitEnd()
    {
      this.Log<LogUnitEnd>().self = this.CurrentUnit;
    }

    public static EUnitDirection UnitDirectionFromVector(Unit self, int x, int y)
    {
      int num1 = Math.Abs(x);
      int num2 = Math.Abs(y);
      if (num1 > num2)
      {
        if (x < 0)
          return EUnitDirection.NegativeX;
        if (x > 0)
          return EUnitDirection.PositiveX;
      }
      if (num1 < num2)
      {
        if (y < 0)
          return EUnitDirection.NegativeY;
        if (y > 0)
          return EUnitDirection.PositiveY;
      }
      if (x > 0)
        return EUnitDirection.PositiveX;
      if (x < 0)
        return EUnitDirection.NegativeX;
      if (y > 0)
        return EUnitDirection.PositiveY;
      if (y < 0)
        return EUnitDirection.NegativeY;
      if (self != null)
        return self.Direction;
      return EUnitDirection.PositiveY;
    }

    public bool Move(Unit self, Grid goal, bool forceAI = false)
    {
      return this.Move(self, goal, EUnitDirection.Auto, false, forceAI);
    }

    public bool CheckMove(Unit self, Grid goal)
    {
      this.DebugAssert(self != null, "self == null");
      if (goal == null)
        return false;
      BattleMap currentMap = this.CurrentMap;
      this.DebugAssert(currentMap != null, "map == null");
      for (int minColX = self.MinColX; minColX < self.MaxColX; ++minColX)
      {
        for (int minColY = self.MinColY; minColY < self.MaxColY; ++minColY)
        {
          Unit unitAtGrid = this.FindUnitAtGrid(currentMap[goal.x + minColX, goal.y + minColY]);
          if (unitAtGrid != null && self != unitAtGrid)
            return false;
        }
      }
      return true;
    }

    public bool MoveMultiPlayer(Unit self, int x, int y, EUnitDirection direction)
    {
      if (!this.CheckMove(self, this.CurrentMap[x, y]))
        return false;
      int step = (self.x >= x ? self.x - x : x - self.x) + (self.y >= y ? self.y - y : y - self.y);
      self.x = x;
      self.y = y;
      self.Direction = direction;
      this.DebugLog("[PUN]MoveMultiPlayer unitID:" + (object) this.mPlayer.FindIndex((Predicate<Unit>) (p => p == self)) + " x:" + (object) x + " y:" + (object) y + " d:" + (object) direction);
      this.MoveEnd(self, step, true);
      return true;
    }

    public bool Move(Unit self, Grid goal, EUnitDirection direction, bool isStickControl = false, bool forceAI = false)
    {
      this.DebugAssert(this.IsMapCommand, "マップコマンド中のみコール可");
      this.DebugAssert(self != null, "self == null");
      if (goal == null)
        return false;
      BattleMap currentMap = this.CurrentMap;
      this.DebugAssert(currentMap != null, "map == null");
      bool flag1 = forceAI || this.IsUnitAuto(self);
      if (!flag1 && !self.IsUnitFlag(EUnitFlag.ForceMoved) && !this.CheckMove(self, goal))
        return false;
      if (isStickControl)
      {
        int step = (self.x >= goal.x ? self.x - goal.x : goal.x - self.x) + (self.y >= goal.y ? self.y - goal.y : goal.y - self.y);
        self.x = goal.x;
        self.y = goal.y;
        self.Direction = direction;
        LogMapMove logMapMove = this.Log<LogMapMove>();
        logMapMove.self = self;
        logMapMove.ex = self.x;
        logMapMove.ey = self.y;
        logMapMove.dir = self.Direction;
        logMapMove.auto = flag1;
        this.MoveEnd(self, step, false);
        return true;
      }
      currentMap.ResetMoveRoutes();
      currentMap.CalcMoveSteps(self, goal, false);
      currentMap.CalcMoveRoutes(self);
      int moveRoutesCount = currentMap.GetMoveRoutesCount();
      if (currentMap.GetNextMoveRoutes() == null)
        self.Direction = direction == EUnitDirection.Auto ? BattleCore.UnitDirectionFromVector(self, goal.x - self.x, goal.y - self.y) : direction;
      LogMapMove logMapMove1 = this.Log<LogMapMove>();
      logMapMove1.self = self;
      logMapMove1.ex = self.x;
      logMapMove1.ey = self.y;
      logMapMove1.dir = self.Direction;
      logMapMove1.auto = flag1;
      this.DebugLog("[" + self.UnitName + "] x:" + (object) self.x + ", y:" + (object) self.y + ", h:" + (object) currentMap[self.x, self.y].height + " から移動開始");
      while (true)
      {
        int x1 = self.x;
        int y1 = self.y;
        Grid nextMoveRoutes = currentMap.GetNextMoveRoutes();
        if (nextMoveRoutes != null)
        {
          bool flag2 = currentMap.IsLastMoveGrid(nextMoveRoutes);
          self.x = nextMoveRoutes.x;
          self.y = nextMoveRoutes.y;
          this.DebugLog("[" + self.UnitName + "] x:" + (object) self.x + ", y:" + (object) self.y + ", h:" + (object) nextMoveRoutes.height + " へ移動");
          if (flag2 && direction != EUnitDirection.Auto)
          {
            self.Direction = direction;
          }
          else
          {
            int x2 = nextMoveRoutes.x - x1;
            int y2 = nextMoveRoutes.y - y1;
            if (flag2 && flag1)
            {
              if (self.IsUnitFlag(EUnitFlag.Escaped))
              {
                x2 = x1 - nextMoveRoutes.x;
                y2 = y1 - nextMoveRoutes.y;
              }
              int num1 = this.mSafeMap.get(x1, y1);
              for (int index = 0; index < 4; ++index)
              {
                int num2 = Unit.DIRECTION_OFFSETS[index, 0];
                int num3 = Unit.DIRECTION_OFFSETS[index, 1];
                if (this.mSafeMap.isValid(nextMoveRoutes.x + num2, nextMoveRoutes.y + num3))
                {
                  int num4 = this.mSafeMap.get(nextMoveRoutes.x + num2, nextMoveRoutes.y + num3);
                  if (num4 < num1)
                  {
                    num1 = num4;
                    x2 = num2;
                    y2 = num3;
                  }
                }
              }
            }
            self.Direction = BattleCore.UnitDirectionFromVector(self, x2, y2);
          }
          LogMapMove logMapMove2 = this.Log<LogMapMove>();
          logMapMove2.self = self;
          logMapMove2.ex = nextMoveRoutes.x;
          logMapMove2.ey = nextMoveRoutes.y;
          logMapMove2.dir = self.Direction;
          logMapMove2.auto = flag1;
          currentMap.IncrementMoveStep();
        }
        else
          break;
      }
      this.MoveEnd(self, moveRoutesCount, false);
      this.DebugLog("[" + self.UnitName + "] x:" + (object) self.x + ", y:" + (object) self.y + ", h:" + (object) currentMap[self.x, self.y].height + " で停止");
      self.SetUnitFlag(EUnitFlag.Escaped, false);
      self.SetUnitFlag(EUnitFlag.Moved, true);
      self.SetCommandFlag(EUnitCommandFlag.Move, true);
      return true;
    }

    private void MoveEnd(Unit self, int step, bool isMultiPlayer = false)
    {
      self.SetUnitFlag(EUnitFlag.Escaped, false);
      self.SetUnitFlag(EUnitFlag.Moved, true);
      self.SetCommandFlag(EUnitCommandFlag.Move, true);
      this.EndMoveSkill(self, step);
      for (int index = 0; index < this.Logs.Num; ++index)
      {
        if (this.Logs[index] is LogMapEnd || this.Logs[index] is LogUnitEnd)
          return;
      }
      int index1 = Math.Max(this.Logs.Num - 1, 0);
      if (self.CastSkill != null && self.CastSkill.LineType != ELineType.None)
        self.CancelCastSkill();
      if (!(this.Logs[index1] is LogMapMove))
      {
        if (self.IsDead)
          this.InternalLogUnitEnd();
        else if (!isMultiPlayer)
          this.Log<LogMapCommand>();
      }
      self.RefleshMomentBuff(this.Units, false, -1, -1);
    }

    public void MapCommandEnd(Unit self)
    {
      self.SetUnitFlag(EUnitFlag.Moved, true);
      self.SetUnitFlag(EUnitFlag.Action, true);
    }

    public static int Sqrt(int v)
    {
      if (v <= 0)
        return 0;
      int num1 = v <= 1 ? 1 : v;
      int num2;
      do
      {
        num2 = num1;
        num1 = (v / num1 + num1) / 2;
      }
      while (num1 < num2);
      return num2;
    }

    public static int Sin(int v)
    {
      int num1 = v - v / 628 * 628;
      int num2 = num1;
      int num3 = num1;
      int num4 = 10;
      for (int index = 1; index <= num4; ++index)
      {
        num2 *= -(num1 * num1) / ((2 * num4 + 1) * (2 * num4));
        num3 += num2;
      }
      return num3;
    }

    public static int Atan(int x)
    {
      int num1 = 0;
      for (int index = 0; index < 100; ++index)
      {
        int num2 = -1 ^ index * (1 / x ^ 2 * index + 1) / (2 * index + 1);
        num1 += num2;
      }
      return (9000 - 180 * num1 * 100) / 314;
    }

    public static int Pow(int val, int n)
    {
      int num = 1;
      for (int index = 1; index <= n; ++index)
        num *= val;
      return num;
    }

    public void GetSkillGridLines(Unit self, int ex, int ey, SkillData skill, ref List<Grid> results)
    {
      DebugUtility.Assert(self != null, "self == null");
      DebugUtility.Assert(skill != null, "skill == null");
      int attackRangeMax = self.GetAttackRangeMax(skill);
      if (attackRangeMax <= 0)
        return;
      int attackRangeMin = self.GetAttackRangeMin(skill);
      int attackHeight = self.GetAttackHeight(skill, true);
      ELineType lineType = skill.LineType;
      bool bHeightBonus = skill.IsEnableHeightRangeBonus();
      this.GetSkillGridLines(self.x, self.y, ex, ey, attackRangeMin, attackRangeMax, attackHeight, lineType, bHeightBonus, ref results);
    }

    public void GetSkillGridLines(int sx, int sy, int ex, int ey, int range_min, int range_max, int attack_height, ELineType line_type, bool bHeightBonus, ref List<Grid> results)
    {
      results.Clear();
      BattleMap currentMap = this.CurrentMap;
      Grid start = currentMap[sx, sy];
      Grid grid = currentMap[ex, ey];
      if (range_max == 0)
        return;
      int num1 = range_max;
      if (bHeightBonus)
        num1 += this.GetAttackRangeBonus(start.height, 0);
      int num2 = 100;
      int _x_ = (ex - sx) * num2;
      int _y_ = (ey - sy) * num2;
      BattleCore.SVector2 svector2 = new BattleCore.SVector2(_x_, _y_);
      int num3 = svector2.Length();
      if (range_min > 0 && range_max > 0 && num3 <= range_min)
        return;
      switch (line_type)
      {
        case ELineType.Direct:
          int tx1 = sx;
          int ty1 = sy;
          if (num3 != 0)
          {
            svector2.x = svector2.x * num2 / num3;
            svector2.y = svector2.y * num2 / num3;
            svector2 *= num1;
            tx1 += svector2.x / num2;
            ty1 += svector2.y / num2;
          }
          this.GetGridOnLine(start, tx1, ty1, ref results);
          for (int index = 0; index < results.Count; ++index)
          {
            int num4 = this.CalcGridDistance(start, results[index]);
            int num5 = range_max - num4;
            int attackRangeBonus = this.GetAttackRangeBonus(start.height, results[index].height);
            if (bHeightBonus)
              num5 += attackRangeBonus;
            if (num5 < 0)
              results.RemoveRange(index, results.Count - index);
          }
          break;
        case ELineType.Curved:
          this.GetGridOnLine(start, ex, ey, ref results);
          for (int index1 = 0; index1 < results.Count; ++index1)
          {
            int num4 = this.CalcGridDistance(start, results[index1]);
            int num5 = num1 - num4;
            int val1 = 0;
            for (int index2 = 0; index2 <= index1; ++index2)
              val1 = Math.Min(val1, this.GetAttackRangeBonus(start.height, results[index2].height));
            if (num5 + val1 < 0)
              results.RemoveRange(index1, results.Count - index1);
          }
          break;
        case ELineType.Stab:
          int num6 = (grid.height - start.height) * num2;
          if (num1 * num2 + 50 < BattleCore.Sqrt(_x_ * _x_ + _y_ * _y_ + num6 * num6))
            return;
          int tx2 = sx;
          int ty2 = sy;
          if (num3 != 0)
          {
            svector2.x = svector2.x * num2 / num3;
            svector2.y = svector2.y * num2 / num3;
            svector2 *= num1;
            tx2 += svector2.x / num2;
            ty2 += svector2.y / num2;
          }
          this.GetGridOnLine(start, tx2, ty2, ref results);
          break;
        default:
          return;
      }
      switch (line_type)
      {
        case ELineType.Direct:
        case ELineType.Stab:
          List<Grid> gridList = new List<Grid>();
          gridList.AddRange((IEnumerable<Grid>) results);
          int num7 = 1;
          Unit unitAtGrid1 = this.FindUnitAtGrid(grid);
          if (unitAtGrid1 != null && !unitAtGrid1.IsNormalSize)
            num7 = (int) unitAtGrid1.UnitParam.sd;
          for (int index1 = num7 - 1; index1 >= 0; --index1)
          {
            results.Clear();
            results.AddRange((IEnumerable<Grid>) gridList);
            int num4 = (grid.height + index1 * 2 - start.height) * num2;
            bool flag = false;
            for (int index2 = 0; index2 < results.Count; ++index2)
            {
              int num5 = new BattleCore.SVector2((results[index2].x - sx) * num2, (results[index2].y - sy) * num2).Length();
              int num8 = 0;
              if (num4 != 0 && num3 != 0)
              {
                int num9 = num5 * num2 / num3;
                num8 = num4 * num9 / num2;
              }
              int num10 = start.height * num2 + BattleMap.MAP_FLOOR_HEIGHT * num2 / 2 + num8;
              int height = results[index2].height;
              if (grid == results[index2])
                height += index1 * 2;
              int num11 = height * num2;
              if (num10 < num11)
              {
                results.RemoveRange(index2, results.Count - index2);
                flag = true;
                break;
              }
              int num12 = BattleMap.MAP_FLOOR_HEIGHT * num2 / 2;
              Unit unitAtGrid2 = this.FindUnitAtGrid(results[index2]);
              if (unitAtGrid2 != null && !unitAtGrid2.IsNormalSize)
                num12 += BattleMap.MAP_FLOOR_HEIGHT * num2 * ((int) unitAtGrid2.UnitParam.sd - 1);
              int num13 = num11 + num12;
              if (num10 > num13)
              {
                results.RemoveAt(index2--);
              }
              else
              {
                Unit gimmickAtGrid = this.FindGimmickAtGrid(results[index2], false, (Unit) null);
                if (gimmickAtGrid != null && gimmickAtGrid.IsBreakObj && (gimmickAtGrid.BreakObjClashType == eMapBreakClashType.INVINCIBLE && gimmickAtGrid.BreakObjRayType == eMapBreakRayType.TERRAIN))
                {
                  results.RemoveRange(index2, results.Count - index2);
                  flag = true;
                  break;
                }
              }
            }
            if (!flag)
              break;
          }
          break;
        case ELineType.Curved:
          if (!results.Contains(grid))
          {
            results.Clear();
            break;
          }
          double num14 = 0.0;
          if (num14 < (double) start.height)
            num14 = (double) start.height;
          for (int index = 0; index < results.Count; ++index)
          {
            if (num14 < (double) results[index].height)
              num14 = (double) results[index].height;
          }
          if (num14 <= (double) start.height)
            ++num14;
          double num15 = num14 + 1.0;
          bool flag1 = true;
          double val2 = (double) num3 / 100.0;
          double num16 = (double) (start.height - grid.height);
          double num17 = 9.8;
          for (int index1 = 0; index1 < attack_height; ++index1)
          {
            double num4 = num15 + (double) index1;
            double d1 = 2.0 * num17 * (num4 - num16);
            double d2 = 2.0 * num17 * num4;
            double num5 = d1 <= 0.0 ? 0.0 : Math.Sqrt(d1);
            double num8 = d2 <= 0.0 ? 0.0 : Math.Sqrt(d2);
            double num9 = (num5 + num8) / num17;
            double d3 = Math.Pow(val2 / num9, 2.0) + 2.0 * num17 * (num4 - num16);
            double num10 = d3 <= 0.0 ? 0.0 : Math.Sqrt(d3);
            double num11 = num9 / val2;
            double a = Math.Atan(num9 * num5 / val2);
            flag1 = true;
            for (int index2 = 0; index2 < results.Count; ++index2)
            {
              int num12 = results[index2].x - start.x;
              int num13 = results[index2].y - start.y;
              double num18 = Math.Min((double) BattleCore.Sqrt(num12 * num12 + num13 * num13), val2);
              double x = num11 * num18;
              double num19 = Math.Sin(a);
              double num20 = Math.Pow(x, 2.0);
              double num21 = num17 * num20 * 0.5;
              double num22 = num10 * x * num19 - num21;
              double num23 = (double) (results[index2].height - start.height) - 0.01;
              if (num22 < num23)
              {
                flag1 = false;
                break;
              }
              double num24 = num23 + (double) (BattleMap.MAP_FLOOR_HEIGHT / 2);
              if (num22 < num24)
              {
                Unit gimmickAtGrid = this.FindGimmickAtGrid(results[index2], false, (Unit) null);
                if (gimmickAtGrid != null && gimmickAtGrid.IsBreakObj && (gimmickAtGrid.BreakObjClashType == eMapBreakClashType.INVINCIBLE && gimmickAtGrid.BreakObjRayType == eMapBreakRayType.TERRAIN))
                {
                  flag1 = false;
                  break;
                }
              }
            }
            if (flag1)
              break;
          }
          if (flag1)
            break;
          results.Clear();
          break;
        default:
          if (grid.height - start.height <= attack_height)
            break;
          results.Remove(grid);
          break;
      }
    }

    private int GetCriticalRate(Unit self, Unit target, SkillData skill)
    {
      if (skill == null || !skill.IsNormalAttack() && !(bool) skill.SkillParam.IsCritical)
        return 0;
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      int num1 = 1000;
      int criticalRateCriMultiply = (int) fixParam.CriticalRate_Cri_Multiply;
      int criticalRateCriDivision = (int) fixParam.CriticalRate_Cri_Division;
      int num2 = BattleCore.Sqrt((int) self.CurrentStatus.param.cri * num1 * num1);
      if (num2 != 0)
      {
        if (criticalRateCriMultiply != 0)
          num2 *= criticalRateCriMultiply;
        if (criticalRateCriDivision != 0)
          num2 /= criticalRateCriDivision;
      }
      int criticalRateLukMultiply = (int) fixParam.CriticalRate_Luk_Multiply;
      int criticalRateLukDivision = (int) fixParam.CriticalRate_Luk_Division;
      int num3 = BattleCore.Sqrt((int) target.CurrentStatus.param.luk * num1 * num1);
      if (num3 != 0)
      {
        if (criticalRateLukMultiply != 0)
          num3 *= criticalRateLukMultiply;
        if (criticalRateLukDivision != 0)
          num3 /= criticalRateLukDivision;
      }
      int num4 = num2 - num3;
      if (num4 != 0)
        num4 /= num1;
      Grid unitGridPosition1 = this.GetUnitGridPosition(self);
      Grid unitGridPosition2 = this.GetUnitGridPosition(target);
      if (unitGridPosition1 != null && unitGridPosition2 != null)
      {
        int num5 = unitGridPosition1.height - unitGridPosition2.height;
        if (num5 > 0)
          num4 += (int) fixParam.HighGridCriRate;
        else if (num5 < 0)
          num4 += (int) fixParam.DownGridCriRate;
      }
      return Math.Min(Math.Max(num4 + (int) self.CurrentStatus[BattleBonus.CriticalRate], 0), 100);
    }

    private bool CheckCritical(Unit self, Unit target, SkillData skill)
    {
      this.DebugAssert(self != null, "self == null");
      return (int) (this.GetRandom() % 100U) <= this.GetCriticalRate(self, target, skill);
    }

    public int GetCriticalDamage(Unit self, int damage, uint rand)
    {
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      int criticalDamageRate1 = (int) fixParam.MinCriticalDamageRate;
      int criticalDamageRate2 = (int) fixParam.MaxCriticalDamageRate;
      int num = criticalDamageRate1 + (int) ((long) rand % (long) (criticalDamageRate2 - criticalDamageRate1));
      damage += 100 * damage * num / 10000;
      return damage;
    }

    private int GetAvoidRate(Unit self, Unit target, SkillData skill)
    {
      if (target.IsUnitCondition(EUnitCondition.Sleep) || target.IsUnitCondition(EUnitCondition.Stun) || (target.IsUnitCondition(EUnitCondition.Stone) || target.IsUnitCondition(EUnitCondition.Stop)) || skill.IsForceHit())
        return 0;
      if (target.IsBreakObj)
        return target.BreakObjClashType == eMapBreakClashType.INVINCIBLE ? 100 : 0;
      if (target.AI != null && target.AI.CheckFlag(AIFlags.DisableAvoid) || this.IsCombinationAttack(skill))
        return 0;
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      int avoidBaseRate = (int) fixParam.AvoidBaseRate;
      int num1 = (int) target.CurrentStatus.param.spd - (int) self.CurrentStatus.param.dex / 2;
      if (num1 != 0)
        avoidBaseRate += num1 * (int) fixParam.AvoidParamScale / 100;
      int num2 = avoidBaseRate + BattleCore.Sqrt((target.Lv - self.Lv) / 2) + target.GetBaseAvoidRate() + (int) target.CurrentStatus[BattleBonus.AvoidRate] - (int) self.CurrentStatus[BattleBonus.HitRate];
      if (self.IsUnitFlag(EUnitFlag.SideAttack))
        num2 -= (int) fixParam.AddHitRateSide;
      if (self.IsUnitFlag(EUnitFlag.BackAttack))
        num2 -= (int) fixParam.AddHitRateBack;
      int num3 = 0;
      if (skill.IsReactionSkill())
        num3 += skill.CalcBuffEffectValue(ParamTypes.Avoid_Reaction, (int) target.CurrentStatus[BattleBonus.Avoid_Reaction], SkillEffectTargets.Target);
      switch (skill.AttackDetailType)
      {
        case AttackDetailTypes.Slash:
          num3 += (int) target.CurrentStatus[BattleBonus.Avoid_Slash];
          break;
        case AttackDetailTypes.Stab:
          num3 += (int) target.CurrentStatus[BattleBonus.Avoid_Pierce];
          break;
        case AttackDetailTypes.Blow:
          num3 += (int) target.CurrentStatus[BattleBonus.Avoid_Blow];
          break;
        case AttackDetailTypes.Shot:
          num3 += (int) target.CurrentStatus[BattleBonus.Avoid_Shot];
          break;
        case AttackDetailTypes.Magic:
          num3 += (int) target.CurrentStatus[BattleBonus.Avoid_Magic];
          break;
        case AttackDetailTypes.Jump:
          num3 += (int) target.CurrentStatus[BattleBonus.Avoid_Jump];
          break;
      }
      int val1 = num2 + num3;
      if (!skill.IsAreaSkill())
      {
        if (target.IsDisableUnitCondition(EUnitCondition.DisableSingleAttack))
          val1 = 100;
      }
      else if (target.IsDisableUnitCondition(EUnitCondition.DisableAreaAttack))
        val1 = 100;
      if (skill.IsMhmDamage() && val1 < 100)
      {
        EUnitCondition condition = EUnitCondition.DisableMaxDamageHp;
        if (skill.IsJewelAttack())
          condition = EUnitCondition.DisableMaxDamageMp;
        if (target.IsDisableUnitCondition(condition))
          val1 = 100;
      }
      return Math.Max(Math.Min(val1, (int) fixParam.MaxAvoidRate), 0);
    }

    private bool CheckAvoid(Unit self, Unit target, SkillData skill)
    {
      int randomHitRate = (int) skill.SkillParam.random_hit_rate;
      if (randomHitRate > 0)
      {
        int num = (int) (this.GetRandom() % 100U);
        if (randomHitRate > num)
          return true;
      }
      return this.GetAvoidRate(self, target, skill) > (int) (this.GetRandom() % 100U);
    }

    private bool CheckGuts(Unit self)
    {
      if (self == null || !self.IsDead)
        return false;
      int currentStatu = (int) self.CurrentStatus[BattleBonus.GutsRate];
      return currentStatu > 0 && (int) (this.CurrentRand.Get() % 100U) < currentStatu || this.mQuestParam.type == QuestTypes.Tutorial && self.Side == EUnitSide.Player;
    }

    private int GetSkillEffectValue(Unit self, Unit target, SkillData skill, LogSkill log = null)
    {
      int num1 = (int) skill.EffectValue;
      if (skill.IsSuicide())
      {
        int num2 = (int) self.MaximumStatus.param.hp == 0 ? 100 : 100 * (int) self.CurrentStatus.param.hp / (int) self.MaximumStatus.param.hp;
        num1 = num1 * num2 / 100;
      }
      int num3 = (int) skill.EffectRange;
      if (num3 != 0)
        num3 = (int) ((long) this.GetRandom() % (long) Math.Abs(num3)) * ((int) skill.EffectRange <= 0 ? -1 : 1);
      int num4 = ((int) self.MaximumStatus.param.hp == 0 ? 100 : 100 * (int) self.CurrentStatus.param.hp / (int) self.MaximumStatus.param.hp) * (int) skill.EffectHpMaxRate / 100;
      int num5 = ((int) self.MaximumStatus.param.mp == 0 ? 0 : 100 * (int) self.CurrentStatus.param.mp / (int) self.MaximumStatus.param.mp) * (int) skill.EffectGemsMaxRate / 100;
      int num6 = 0;
      int effectDeadRate = (int) skill.SkillParam.effect_dead_rate;
      if (effectDeadRate != 0)
      {
        for (int index = 0; index < this.mUnits.Count; ++index)
        {
          if (!this.mUnits[index].IsUnitFlag(EUnitFlag.OtherTeam) && !this.mUnits[index].IsUnitFlag(EUnitFlag.UnitTransformed) && (!this.mUnits[index].IsUnitFlag(EUnitFlag.IsDynamicTransform) && this.mUnits[index].IsDead) && (!this.mUnits[index].IsGimmick && !this.CheckEnemySide(self, this.mUnits[index])))
            num6 += effectDeadRate;
        }
      }
      int num7 = 0;
      if ((int) skill.SkillParam.effect_lvrate != 0 && target != null)
      {
        int num2 = target.Lv - self.Lv;
        if (num2 > 0)
          num7 = num2 * (int) skill.SkillParam.effect_lvrate;
      }
      int num8 = 0;
      int hitTargetNumRate = (int) skill.SkillParam.EffectHitTargetNumRate;
      if (hitTargetNumRate != 0 && log != null && (log.targets != null && log.targets.Count > 1))
        num8 += hitTargetNumRate * (log.targets.Count - 1);
      return num1 + num3 + num4 + num5 + num6 + num7 + num8;
    }

    private bool CheckWeakPoint(Unit self, Unit target, SkillData skill)
    {
      if (skill.ElementType == EElement.None)
        return false;
      return skill.ElementType == target.GetWeakElement();
    }

    private int CalcHeal(Unit self, Unit target, SkillData skill, LogSkill log)
    {
      if (skill.EffectType == SkillEffectTypes.Heal)
      {
        int skillEffectValue = this.GetSkillEffectValue(self, target, skill, log);
        return SkillParam.CalcSkillEffectValue(skill.EffectCalcType, skillEffectValue, (int) self.CurrentStatus.param.mag);
      }
      if (skill.EffectType != SkillEffectTypes.RateHeal)
        return 0;
      int skillEffectValue1 = this.GetSkillEffectValue(self, target, skill, log);
      return (int) target.MaximumStatus.param.hp * skillEffectValue1 / 100;
    }

    private void Heal(Unit target, int value)
    {
      target.Heal(value);
    }

    private int CalcGainedGems(Unit self, Unit target, SkillData skill, int damage, bool bCritical, bool bWeakPoint)
    {
      if (skill == null || !skill.IsNormalAttack() || target.IsBreakObj)
        return 0;
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      int gainNormalAttack = (int) fixParam.GemsGainNormalAttack;
      if (bCritical)
        gainNormalAttack += (int) fixParam.GemsGainCriticalAttack;
      if (bWeakPoint)
        gainNormalAttack += (int) fixParam.GemsGainWeakAttack;
      if (self.IsUnitFlag(EUnitFlag.SideAttack))
        gainNormalAttack += (int) fixParam.GemsGainSideAttack;
      if (self.IsUnitFlag(EUnitFlag.BackAttack))
        gainNormalAttack += (int) fixParam.GemsGainBackAttack;
      if (target.IsDead)
        gainNormalAttack += (int) fixParam.GemsGainKillBonus;
      if ((int) fixParam.GemsGainDiffFloorCount > 0)
      {
        int num = this.CurrentMap[self.x, self.y].height - this.CurrentMap[target.x, target.y].height;
        if (num > 0)
          gainNormalAttack += Math.Min(num / (int) fixParam.GemsGainDiffFloorCount, (int) fixParam.GemsGainDiffFloorMax);
      }
      int num1 = gainNormalAttack + (int) self.CurrentStatus[BattleBonus.GainJewel];
      return num1 + num1 * (int) self.CurrentStatus[BattleBonus.GainJewelRate] / 100;
    }

    private void DamageCureCondition(Unit target, LogSkill log = null)
    {
      if (target.IsUnitCondition(EUnitCondition.Sleep))
        this.CureCondition(target, EUnitCondition.Sleep, log);
      if (!target.IsUnitCondition(EUnitCondition.Charm))
        return;
      this.CureCondition(target, EUnitCondition.Charm, log);
    }

    private void HealCureCondition(Unit target, LogSkill log = null)
    {
    }

    private Unit GetSubMemberFirst(int owner = -1)
    {
      for (int index = 0; index < this.Player.Count; ++index)
      {
        if (this.Player[index].IsSub && !this.Player[index].IsDead && this.Player[index] != this.Friend && (owner == -1 || this.Player[index].OwnerPlayerIndex == owner))
          return this.Player[index];
      }
      return (Unit) null;
    }

    public void ResumeDead(Unit target)
    {
      this.Dead((Unit) null, target, DeadTypes.Damage, true);
    }

    private void Dead(Unit self, Unit target, DeadTypes type, bool is_resume = false)
    {
      if (target.IsUnitFlag(EUnitFlag.EntryDead))
        return;
      target.SetUnitFlag(EUnitFlag.EntryDead, true);
      if (!is_resume && this.DeadSkill(target, self))
        return;
      if (target.Side == EUnitSide.Enemy)
      {
        ++this.mKillstreak;
        this.mMaxKillstreak = Math.Max(this.mMaxKillstreak, this.mKillstreak);
        string iname = target.UnitParam.iname;
        int val1;
        if (this.mTargetKillstreakDict.TryGetValue(iname, out val1))
          ++val1;
        else
          val1 = 1;
        this.mTargetKillstreakDict[iname] = val1;
        int val2;
        this.mMaxTargetKillstreakDict[iname] = !this.mMaxTargetKillstreakDict.TryGetValue(iname, out val2) ? val1 : Math.Max(val1, val2);
      }
      if (self != null && self != target)
        ++self.KillCount;
      if (!target.IsDead)
        return;
      (this.Logs.Last as LogDead ?? this.Log<LogDead>()).Add(target, type);
      target.ForceDead();
      if (target.IsUnitFlag(EUnitFlag.IsDynamicTransform))
        this.DtuDead(target);
      this.GridEventStart(self, target, EEventTrigger.Dead, (SkillParam) null);
      if (target.IsPartyMember && this.Friend != target && (this.GetQuestResult() == BattleCore.QuestResult.Pending && !target.IsNPC))
      {
        Unit unit1 = !this.IsMultiPlay ? this.GetSubMemberFirst(-1) : this.GetSubMemberFirst(target.OwnerPlayerIndex);
        if (unit1 != null)
        {
          Grid duplicatePosition = this.GetCorrectDuplicatePosition(target);
          unit1.x = duplicatePosition.x;
          unit1.y = duplicatePosition.y;
          unit1.Direction = target.Direction;
          if (!is_resume)
          {
            unit1.IsSub = false;
            unit1.SetUnitFlag(EUnitFlag.Reinforcement, true);
          }
          this.Log<LogUnitEntry>().self = unit1;
          this.BeginBattlePassiveSkill(unit1);
          unit1.UpdateBuffEffects();
          unit1.CalcCurrentStatus(false, false);
          unit1.CurrentStatus.param.hp = unit1.MaximumStatus.param.hp;
          if (this.IsTower)
          {
            int unitDamage = MonoSingleton<GameManager>.Instance.TowerResuponse.GetUnitDamage(unit1.UnitData);
            unit1.CurrentStatus.param.hp = (OInt) Math.Max((int) unit1.CurrentStatus.param.hp - unitDamage, 1);
          }
          unit1.CurrentStatus.param.mp = (OShort) unit1.GetStartGems();
          unit1.InitializeSkillUseCount();
          for (int index = 0; index < this.Player.Count; ++index)
          {
            Unit unit2 = this.Player[index];
            if (unit2.IsSub && !unit2.IsDead && unit2 != this.Friend)
            {
              unit2.UpdateBuffEffects();
              unit2.CalcCurrentStatus(false, false);
              unit2.CurrentStatus.param.hp = unit2.MaximumStatus.param.hp;
              if (this.IsTower)
              {
                int unitDamage = MonoSingleton<GameManager>.Instance.TowerResuponse.GetUnitDamage(unit2.UnitData);
                unit2.CurrentStatus.param.hp = (OInt) Math.Max((int) unit2.CurrentStatus.param.hp - unitDamage, 1);
              }
              unit2.CurrentStatus.param.mp = (OShort) unit2.GetStartGems();
            }
          }
          this.UseAutoSkills(unit1);
        }
      }
      this.GimmickEventDeadCount(self, target);
      this.UpdateGimmickEventTrick();
      this.UpdateEntryTriggers(UnitEntryTypes.DeadEnemy, target, (SkillParam) null);
      if (target.IsUnitFlag(EUnitFlag.CreatedBreakObj))
      {
        if (this.Enemys.Contains(target))
          this.Enemys.Remove(target);
        if (this.mAllUnits.Contains(target))
          this.mAllUnits.Remove(target);
        if (this.mUnits.Contains(target))
          this.mUnits.Remove(target);
      }
      if (!target.IsUnitFlag(EUnitFlag.InfinitySpawn))
        return;
      if (this.Enemys.Contains(target))
        this.Enemys.Remove(target);
      if (this.mAllUnits.Contains(target))
        this.mAllUnits.Remove(target);
      if (!this.mUnits.Contains(target))
        return;
      this.mUnits.Remove(target);
    }

    private void Revive(Unit target, int hp)
    {
      LogRevive logRevive = this.Log<LogRevive>();
      logRevive.self = target;
      logRevive.hp = hp;
    }

    private void EntryUnit(Unit self)
    {
      Grid duplicatePosition = this.GetCorrectDuplicatePosition(self);
      self.x = duplicatePosition.x;
      self.y = duplicatePosition.y;
      self.SetUnitFlag(EUnitFlag.Entried, true);
      self.SetUnitFlag(EUnitFlag.Reinforcement, true);
      self.IsSub = false;
      this.Log<LogUnitEntry>().self = self;
    }

    private void CureCondition(Unit target, EUnitCondition condition, LogSkill logskl)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      LogCureCondition logCureCondition = this.Log<LogCureCondition>();
      logCureCondition.self = target;
      logCureCondition.condition = condition;
      bool flag = target.IsUnitCondition(condition);
      target.CureCondEffects(condition, true, false);
      if (logskl == null || target == null || (!flag || target.IsUnitCondition(condition)))
        return;
      LogSkill.Target target1 = logskl.FindTarget(target);
      if (target1 == null)
        return;
      target1.cureCondition |= condition;
    }

    private void FailCondition(Unit self, Unit target, SkillData skill, SkillEffectTargets skilltarget, CondEffectParam param, ConditionEffectTypes type, ESkillCondition cond, EUnitCondition condition, EffectCheckTargets chk_target, EffectCheckTimings chk_timing, int turn, bool is_passive, bool is_curse, LogSkill logskl, bool is_same_ow)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      LogFailCondition logFailCondition = this.Log<LogFailCondition>();
      logFailCondition.self = target;
      logFailCondition.source = self;
      logFailCondition.condition = condition;
      SceneBattle instance = SceneBattle.Instance;
      if ((UnityEngine.Object) instance != (UnityEngine.Object) null)
      {
        TacticsUnitController unitController = instance.FindUnitController(target);
        if ((UnityEngine.Object) unitController != (UnityEngine.Object) null)
          unitController.LockUpdateBadStatus(logFailCondition.condition, false);
      }
      CondAttachment condAttachment1 = this.CreateCondAttachment(self, target, skill, skilltarget, param, type, cond, condition, chk_target, chk_timing, turn, is_passive, is_curse);
      if (is_same_ow)
      {
        for (int index = 0; index < target.CondAttachments.Count; ++index)
        {
          CondAttachment condAttachment2 = target.CondAttachments[index];
          if (condAttachment1.IsSame(condAttachment2, true))
            target.CondAttachments.RemoveAt(index--);
        }
      }
      target.ClearCondLinkageBuffBits();
      target.SetCondAttachment(condAttachment1);
      if (logskl == null || !target.IsUnitCondition(condition))
        return;
      LogSkill.Target target1 = logskl.FindTarget(target);
      if (target1 == null)
        return;
      target1.failCondition |= condition;
      if (condAttachment1.LinkageBuff == null)
        return;
      target.CondLinkageBuff.CopyTo(target1.buff);
      target.CondLinkageDebuff.CopyTo(target1.debuff);
    }

    private void BuffSkill(ESkillTiming timing, Unit self, Unit target, SkillData skill, bool is_passive = false, LogSkill log = null, SkillEffectTargets buff_target = SkillEffectTargets.Target, bool is_duplicate = false, BuffEffect[] add_buff_effects = null, List<Unit> dsse_target_lists = null)
    {
      if (this.IsBattleFlag(EBattleFlag.ComputeAI) || timing != skill.Timing)
        return;
      BattleCore.BuffWorkStatus.Clear();
      BattleCore.DebuffWorkStatus.Clear();
      BattleCore.BuffNegativeWorkStatus.Clear();
      BattleCore.DebuffNegativeWorkStatus.Clear();
      BattleCore.BuffWorkScaleStatus.Clear();
      BattleCore.DebuffWorkScaleStatus.Clear();
      BuffEffect buffEffect = skill.GetBuffEffect(buff_target);
      if (buffEffect == null || buffEffect.targets.Count == 0 || !buffEffect.CheckEnableBuffTarget(target))
        return;
      DependStateSpcEffParam stateSpcEffParam = skill.GetDependStateSpcEffParam();
      if (stateSpcEffParam != null)
      {
        if (dsse_target_lists != null)
        {
          bool flag = false;
          for (int index = 0; index < dsse_target_lists.Count; ++index)
          {
            Unit dsseTargetList = dsse_target_lists[index];
            if (stateSpcEffParam.IsApplyBuff(dsseTargetList, buff_target))
            {
              flag = true;
              break;
            }
          }
          if (!flag)
            return;
        }
        else if (!stateSpcEffParam.IsApplyBuff(target, buff_target))
          return;
      }
      Unit target1 = target;
      eAbsorbAndGive aag = skill.SkillParam.AbsorbAndGive;
      if (aag != eAbsorbAndGive.None)
      {
        if (buff_target == SkillEffectTargets.Target && self != target)
        {
          if (SkillParam.IsAagTypeGive(aag))
          {
            if (this.IsEnableAag)
            {
              this.AagTargetLists.Add(target);
              return;
            }
            target1 = self;
          }
        }
        else
          aag = eAbsorbAndGive.None;
      }
      bool flag1 = true;
      bool flag2 = true;
      if (!skill.IsPassiveSkill())
      {
        if (!target1.IsEnableBuffEffect(BuffTypes.Buff) && !buffEffect.param.IsNoDisabled)
          flag1 = false;
        else if ((int) target1.CurrentStatus.enchant_resist.resist_buff > 0)
        {
          if ((int) target1.CurrentStatus.enchant_resist.resist_buff < 100)
          {
            if ((int) (this.GetRandom() % 100U) < (int) target1.CurrentStatus.enchant_resist.resist_buff)
              flag1 = false;
          }
          else
            flag1 = false;
        }
        if (!target1.IsEnableBuffEffect(BuffTypes.Debuff) && !buffEffect.param.IsNoDisabled)
          flag2 = false;
        else if ((int) target1.CurrentStatus.enchant_resist.resist_debuff > 0)
        {
          if ((int) target1.CurrentStatus.enchant_resist.resist_debuff < 100)
          {
            if ((int) (this.GetRandom() % 100U) < (int) target1.CurrentStatus.enchant_resist.resist_debuff)
              flag2 = false;
          }
          else
            flag2 = false;
        }
      }
      if (!skill.BuffSkill(timing, target1.Element, BattleCore.BuffWorkStatus, BattleCore.BuffNegativeWorkStatus, BattleCore.BuffWorkScaleStatus, BattleCore.DebuffWorkStatus, BattleCore.DebuffNegativeWorkStatus, BattleCore.DebuffWorkScaleStatus, this.CurrentRand, buff_target, false, (List<BuffEffect.BuffValues>) null))
        return;
      int turn = (int) buffEffect.param.turn;
      ESkillCondition cond = buffEffect.param.cond;
      EffectCheckTargets chkTarget = buffEffect.param.chk_target;
      EffectCheckTimings chkTiming = buffEffect.param.chk_timing;
      int duplicateCount = skill.DuplicateCount;
      bool flag3 = buffEffect.CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Add, false);
      bool flag4 = buffEffect.CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Add, true);
      bool flag5 = buffEffect.CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Scale);
      bool flag6 = buffEffect.CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Add, false);
      bool flag7 = buffEffect.CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Add, true);
      bool flag8 = buffEffect.CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Scale);
      if (aag != eAbsorbAndGive.None)
      {
        BaseStatus status = target1.UnitData.Status;
        if (flag5)
        {
          BattleCore.BuffWorkStatus.AddConvRate(BattleCore.BuffWorkScaleStatus, status);
          flag3 = true;
          flag5 = false;
        }
        if (flag8)
        {
          BattleCore.DebuffWorkStatus.AddConvRate(BattleCore.DebuffWorkScaleStatus, status);
          flag6 = true;
          flag8 = false;
        }
      }
      if (skill.Condition == ESkillCondition.CardSkill && add_buff_effects != null)
      {
        BaseStatus status1 = new BaseStatus();
        BaseStatus status2 = new BaseStatus();
        BaseStatus status3 = new BaseStatus();
        BaseStatus status4 = new BaseStatus();
        BaseStatus status5 = new BaseStatus();
        BaseStatus status6 = new BaseStatus();
        for (int index = 0; index < add_buff_effects.Length; ++index)
        {
          if (add_buff_effects[index] != null)
          {
            status1.Clear();
            status2.Clear();
            status3.Clear();
            status4.Clear();
            status5.Clear();
            status6.Clear();
            add_buff_effects[index].CalcBuffStatus(ref status1, target.Element, BuffTypes.Buff, true, false, SkillParamCalcTypes.Add, 0);
            add_buff_effects[index].CalcBuffStatus(ref status2, target.Element, BuffTypes.Buff, true, true, SkillParamCalcTypes.Add, 0);
            add_buff_effects[index].CalcBuffStatus(ref status3, target.Element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Scale, 0);
            add_buff_effects[index].CalcBuffStatus(ref status4, target.Element, BuffTypes.Debuff, true, false, SkillParamCalcTypes.Add, 0);
            add_buff_effects[index].CalcBuffStatus(ref status5, target.Element, BuffTypes.Debuff, true, true, SkillParamCalcTypes.Add, 0);
            add_buff_effects[index].CalcBuffStatus(ref status6, target.Element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Scale, 0);
            flag3 |= add_buff_effects[index].CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Add, false);
            flag4 |= add_buff_effects[index].CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Add, true);
            flag5 |= add_buff_effects[index].CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Scale);
            flag6 |= add_buff_effects[index].CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Add, false);
            flag7 |= add_buff_effects[index].CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Add, true);
            flag8 |= add_buff_effects[index].CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Scale);
            BattleCore.BuffWorkStatus.Add(status1);
            BattleCore.BuffNegativeWorkStatus.Add(status2);
            BattleCore.BuffWorkScaleStatus.Add(status3);
            BattleCore.DebuffWorkStatus.Add(status4);
            BattleCore.DebuffNegativeWorkStatus.Add(status5);
            BattleCore.DebuffWorkScaleStatus.Add(status6);
          }
        }
      }
      bool flag9 = false;
      if (flag1)
      {
        if (flag3)
        {
          BuffAttachment buffAttachment = this.CreateBuffAttachment(self, target1, skill, buff_target, buffEffect.param, BuffTypes.Buff, false, SkillParamCalcTypes.Add, BattleCore.BuffWorkStatus, cond, turn, chkTarget, chkTiming, is_passive, duplicateCount);
          if (target1.SetBuffAttachment(buffAttachment, is_duplicate))
          {
            flag9 = true;
            if (aag != eAbsorbAndGive.None)
            {
              this.AagBuffAttachmentLists.Add(buffAttachment);
              if (SkillParam.IsAagTypeSame(aag))
              {
                BattleCore.BuffAagWorkStatus.Add(BattleCore.BuffWorkStatus);
                this.IsEnableAagBuff = true;
              }
              else
              {
                BattleCore.DebuffAagWorkStatus.Sub(BattleCore.BuffWorkStatus);
                this.IsEnableAagDebuff = true;
              }
            }
          }
        }
        if (flag4)
        {
          BuffAttachment buffAttachment = this.CreateBuffAttachment(self, target1, skill, buff_target, buffEffect.param, BuffTypes.Buff, true, SkillParamCalcTypes.Add, BattleCore.BuffNegativeWorkStatus, cond, turn, chkTarget, chkTiming, is_passive, duplicateCount);
          if (target1.SetBuffAttachment(buffAttachment, is_duplicate))
          {
            flag9 = true;
            if (aag != eAbsorbAndGive.None)
            {
              this.AagBuffAttachmentLists.Add(buffAttachment);
              if (SkillParam.IsAagTypeSame(aag))
              {
                BattleCore.BuffAagNegativeWorkStatus.Add(BattleCore.BuffNegativeWorkStatus);
                this.IsEnableAagBuffNegative = true;
              }
              else
              {
                BattleCore.DebuffAagNegativeWorkStatus.Sub(BattleCore.BuffNegativeWorkStatus);
                this.IsEnableAagDebuffNegative = true;
              }
            }
          }
        }
        if (flag5)
        {
          BuffAttachment buffAttachment = this.CreateBuffAttachment(self, target1, skill, buff_target, buffEffect.param, BuffTypes.Buff, false, SkillParamCalcTypes.Scale, BattleCore.BuffWorkScaleStatus, cond, turn, chkTarget, chkTiming, is_passive, duplicateCount);
          if (target1.SetBuffAttachment(buffAttachment, is_duplicate))
            flag9 = true;
        }
      }
      if (flag2)
      {
        if (flag6)
        {
          BuffAttachment buffAttachment = this.CreateBuffAttachment(self, target1, skill, buff_target, buffEffect.param, BuffTypes.Debuff, false, SkillParamCalcTypes.Add, BattleCore.DebuffWorkStatus, cond, turn, chkTarget, chkTiming, is_passive, duplicateCount);
          if (target1.SetBuffAttachment(buffAttachment, is_duplicate))
          {
            flag9 = true;
            if (aag != eAbsorbAndGive.None)
            {
              this.AagBuffAttachmentLists.Add(buffAttachment);
              if (SkillParam.IsAagTypeSame(aag))
              {
                BattleCore.DebuffAagWorkStatus.Add(BattleCore.DebuffWorkStatus);
                this.IsEnableAagDebuff = true;
              }
              else
              {
                BattleCore.BuffAagWorkStatus.Sub(BattleCore.DebuffWorkStatus);
                this.IsEnableAagBuff = true;
              }
            }
          }
        }
        if (flag7)
        {
          BuffAttachment buffAttachment = this.CreateBuffAttachment(self, target1, skill, buff_target, buffEffect.param, BuffTypes.Debuff, true, SkillParamCalcTypes.Add, BattleCore.DebuffNegativeWorkStatus, cond, turn, chkTarget, chkTiming, is_passive, duplicateCount);
          if (target1.SetBuffAttachment(buffAttachment, is_duplicate))
          {
            flag9 = true;
            if (aag != eAbsorbAndGive.None)
            {
              this.AagBuffAttachmentLists.Add(buffAttachment);
              if (SkillParam.IsAagTypeSame(aag))
              {
                BattleCore.DebuffAagNegativeWorkStatus.Add(BattleCore.DebuffNegativeWorkStatus);
                this.IsEnableAagDebuffNegative = true;
              }
              else
              {
                BattleCore.BuffAagNegativeWorkStatus.Sub(BattleCore.DebuffNegativeWorkStatus);
                this.IsEnableAagBuffNegative = true;
              }
            }
          }
        }
        if (flag8)
        {
          BuffAttachment buffAttachment = this.CreateBuffAttachment(self, target1, skill, buff_target, buffEffect.param, BuffTypes.Debuff, false, SkillParamCalcTypes.Scale, BattleCore.DebuffWorkScaleStatus, cond, turn, chkTarget, chkTiming, is_passive, duplicateCount);
          if (target1.SetBuffAttachment(buffAttachment, is_duplicate))
            flag9 = true;
        }
      }
      if (!flag9)
        return;
      if (aag != eAbsorbAndGive.None)
        this.AagTargetLists.Add(target);
      if (log == null)
        return;
      BattleCore.BuffWorkStatus.Add(BattleCore.BuffNegativeWorkStatus);
      BattleCore.BuffWorkStatus.Add(BattleCore.DebuffWorkStatus);
      BattleCore.BuffWorkStatus.Add(BattleCore.DebuffNegativeWorkStatus);
      BattleCore.BuffWorkScaleStatus.Add(BattleCore.DebuffWorkScaleStatus);
      BuffBit buff = new BuffBit();
      BuffBit debuff = new BuffBit();
      BattleCore.SetBuffBits(BattleCore.BuffWorkStatus, ref buff, ref debuff);
      BattleCore.SetBuffBits(BattleCore.BuffWorkScaleStatus, ref buff, ref debuff);
      LogSkill.Target target2 = (LogSkill.Target) null;
      switch (buff_target)
      {
        case SkillEffectTargets.Target:
          if (aag != eAbsorbAndGive.None && self == target1)
          {
            target2 = log.self_effect;
            target2.target = target1;
            break;
          }
          target2 = log.FindTarget(target1);
          break;
        case SkillEffectTargets.Self:
          if (self == target1)
          {
            target2 = log.self_effect;
            target2.target = target1;
            break;
          }
          break;
      }
      if (target2 == null)
        return;
      buff.CopyTo(target2.buff);
      debuff.CopyTo(target2.debuff);
    }

    public static void SetBuffBits(BaseStatus status, ref BuffBit buff, ref BuffBit debuff)
    {
      for (int index = 0; index < status.param.Length; ++index)
      {
        if ((int) status.param[(StatusTypes) index] != 0)
        {
          ParamTypes paramTypes = status.param.GetParamTypes(index);
          if ((int) status.param[(StatusTypes) index] > 0)
            buff.SetBit(paramTypes);
          else
            debuff.SetBit(paramTypes);
        }
      }
      for (int index = 0; index < ElementParam.MAX_ELEMENT; ++index)
      {
        if ((int) status.element_assist.values[index] != 0)
        {
          ParamTypes assistParamTypes = status.element_assist.GetAssistParamTypes(index);
          if ((int) status.element_assist.values[index] > 0)
            buff.SetBit(assistParamTypes);
          else
            debuff.SetBit(assistParamTypes);
        }
        if ((int) status.element_resist.values[index] != 0)
        {
          ParamTypes resistParamTypes = status.element_resist.GetResistParamTypes(index);
          if ((int) status.element_resist.values[index] > 0)
            buff.SetBit(resistParamTypes);
          else
            debuff.SetBit(resistParamTypes);
        }
      }
      for (int index = 0; index < EnchantParam.MAX_ENCHANGT; ++index)
      {
        if ((int) status.enchant_assist.values[index] != 0)
        {
          ParamTypes assistParamTypes = status.enchant_assist.GetAssistParamTypes(index);
          if ((int) status.enchant_assist.values[index] > 0)
            buff.SetBit(assistParamTypes);
          else
            debuff.SetBit(assistParamTypes);
        }
        if ((int) status.enchant_resist.values[index] != 0)
        {
          ParamTypes resistParamTypes = status.enchant_resist.GetResistParamTypes(index);
          if ((int) status.enchant_resist.values[index] > 0)
            buff.SetBit(resistParamTypes);
          else
            debuff.SetBit(resistParamTypes);
        }
      }
      for (int index = 0; index < status.bonus.values.Length; ++index)
      {
        if ((int) status.bonus.values[index] != 0)
        {
          ParamTypes paramTypes = status.bonus.GetParamTypes(index);
          if ((int) status.bonus.values[index] > 0)
            buff.SetBit(paramTypes);
          else
            debuff.SetBit(paramTypes);
        }
      }
    }

    public BuffAttachment CreateBuffAttachment(Unit user, Unit target, SkillData skill, SkillEffectTargets skilltarget, BuffEffectParam param, BuffTypes buffType, bool is_negative_value_is_buff, SkillParamCalcTypes calcType, BaseStatus status, ESkillCondition cond, int turn, EffectCheckTargets chktgt, EffectCheckTimings timing, bool is_passive, int dupli)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
      {
        if (param != null && 0 < (int) param.rate && (int) param.rate < 100)
          return (BuffAttachment) null;
        timing = EffectCheckTimings.PrevApply;
      }
      BuffAttachment buffAttachment1 = new BuffAttachment(param);
      buffAttachment1.user = user;
      buffAttachment1.turn = (OInt) turn;
      buffAttachment1.skill = skill;
      buffAttachment1.skilltarget = skilltarget;
      buffAttachment1.IsPassive = (OBool) is_passive;
      buffAttachment1.BuffType = buffType;
      buffAttachment1.IsNegativeValueIsBuff = is_negative_value_is_buff;
      buffAttachment1.CalcType = calcType;
      buffAttachment1.CheckTiming = timing;
      buffAttachment1.CheckTarget = (Unit) null;
      buffAttachment1.UseCondition = cond;
      buffAttachment1.DuplicateCount = dupli;
      switch (chktgt)
      {
        case EffectCheckTargets.Target:
          buffAttachment1.CheckTarget = target;
          break;
        case EffectCheckTargets.User:
          buffAttachment1.CheckTarget = user;
          break;
      }
      if (user != null && timing != EffectCheckTimings.Eternal && !is_passive && (param == null || !param.IsNoBuffTurn))
      {
        if (buffType == BuffTypes.Buff)
        {
          BuffAttachment buffAttachment2 = buffAttachment1;
          buffAttachment2.turn = (OInt) ((int) buffAttachment2.turn + (int) user.CurrentStatus[BattleBonus.BuffTurn]);
        }
        if (buffType == BuffTypes.Debuff)
        {
          BuffAttachment buffAttachment2 = buffAttachment1;
          buffAttachment2.turn = (OInt) ((int) buffAttachment2.turn + (int) user.CurrentStatus[BattleBonus.DebuffTurn]);
        }
      }
      status.CopyTo(buffAttachment1.status);
      return buffAttachment1;
    }

    private void CondSkill(ESkillTiming timing, Unit self, Unit target, SkillData skill, bool is_passive = false, LogSkill log = null, SkillEffectTargets cond_target = SkillEffectTargets.Target, bool is_same_ow = false, List<Unit> dsse_target_lists = null)
    {
      if (timing != skill.Timing)
        return;
      CondEffect condEffect = skill.GetCondEffect(cond_target);
      ConditionEffectTypes type = ConditionEffectTypes.None;
      if (condEffect != null && condEffect.param != null)
      {
        if (!condEffect.CheckEnableCondTarget(target))
          return;
        DependStateSpcEffParam stateSpcEffParam = skill.GetDependStateSpcEffParam();
        if (stateSpcEffParam != null)
        {
          if (dsse_target_lists != null)
          {
            bool flag = false;
            for (int index = 0; index < dsse_target_lists.Count; ++index)
            {
              Unit dsseTargetList = dsse_target_lists[index];
              if (stateSpcEffParam.IsApplyCond(dsseTargetList, cond_target))
              {
                flag = true;
                break;
              }
            }
            if (!flag)
              return;
          }
          else if (!stateSpcEffParam.IsApplyCond(target, cond_target))
            return;
        }
        if (condEffect.param.type != ConditionEffectTypes.None && condEffect.param.conditions != null)
        {
          int rate = (int) condEffect.rate;
          if (rate > 0 && rate < 100 && (int) (this.GetRandom() % 100U) > rate)
            return;
          type = condEffect.param.type;
        }
      }
      LogSkill.Target target1 = (LogSkill.Target) null;
      if (log != null)
      {
        switch (cond_target)
        {
          case SkillEffectTargets.Target:
            target1 = log.FindTarget(target);
            break;
          case SkillEffectTargets.Self:
            if (condEffect == null || condEffect.param == null)
              return;
            if (self == target)
            {
              log.self_effect.target = self;
              break;
            }
            break;
          default:
            return;
        }
      }
      switch (type)
      {
        case ConditionEffectTypes.None:
          if (!skill.IsDamagedSkill())
            break;
          EnchantParam enchantAssist1 = self.CurrentStatus.enchant_assist;
          EnchantParam enchantResist1 = target.CurrentStatus.enchant_resist;
          for (int index = 0; index < (int) Unit.MAX_UNIT_CONDITION; ++index)
          {
            EUnitCondition condition = (EUnitCondition) (1L << index);
            if (!target.IsDisableUnitCondition(condition) && this.CheckFailCondition(target, (int) enchantAssist1[condition], (int) enchantResist1[condition], condition))
              this.FailCondition(self, target, skill, cond_target, (CondEffectParam) null, ConditionEffectTypes.FailCondition, ESkillCondition.None, condition, EffectCheckTargets.Target, EffectCheckTimings.ActionStart, 0, is_passive, false, log, is_same_ow);
          }
          break;
        case ConditionEffectTypes.CureCondition:
          if (condEffect == null || condEffect.param == null || condEffect.param.conditions == null)
            break;
          for (int index = 0; index < condEffect.param.conditions.Length; ++index)
          {
            EUnitCondition condition = condEffect.param.conditions[index];
            this.CureCondition(target, condition, log);
          }
          break;
        case ConditionEffectTypes.FailCondition:
          if (condEffect == null || condEffect.param == null || (condEffect.param.conditions == null || (int) condEffect.value == 0))
            break;
          EnchantParam enchantAssist2 = self.CurrentStatus.enchant_assist;
          EnchantParam enchantResist2 = target.CurrentStatus.enchant_resist;
          self.CurrentStatus.enchant_assist.CopyTo(enchantAssist2);
          for (int index = 0; index < condEffect.param.conditions.Length; ++index)
          {
            EUnitCondition condition = condEffect.param.conditions[index];
            if (!target.IsDisableUnitCondition(condition) && this.CheckFailCondition(target, (int) enchantAssist2[condition] + (int) condEffect.value, (int) enchantResist2[condition], condition))
              this.FailCondition(self, target, skill, cond_target, condEffect.param, condEffect.param.type, condEffect.param.cond, condition, condEffect.param.chk_target, condEffect.param.chk_timing, (int) condEffect.turn, is_passive, condEffect.IsCurse, log, is_same_ow);
          }
          break;
        case ConditionEffectTypes.ForcedFailCondition:
          if (condEffect == null || condEffect.param == null || condEffect.param.conditions == null)
            break;
          for (int index = 0; index < condEffect.param.conditions.Length; ++index)
          {
            EUnitCondition condition = condEffect.param.conditions[index];
            this.FailCondition(self, target, skill, cond_target, condEffect.param, condEffect.param.type, condEffect.param.cond, condition, condEffect.param.chk_target, condEffect.param.chk_timing, (int) condEffect.turn, is_passive, condEffect.IsCurse, log, is_same_ow);
          }
          break;
        case ConditionEffectTypes.RandomFailCondition:
          if (condEffect == null || condEffect.param == null || (condEffect.param.conditions == null || (int) condEffect.value == 0))
            break;
          EnchantParam enchantAssist3 = self.CurrentStatus.enchant_assist;
          EnchantParam enchantResist3 = target.CurrentStatus.enchant_resist;
          int index1 = (int) ((long) this.GetRandom() % (long) condEffect.param.conditions.Length);
          EUnitCondition condition1 = condEffect.param.conditions[index1];
          if (target.IsDisableUnitCondition(condition1) || !this.CheckFailCondition(target, (int) enchantAssist3[condition1] + (int) condEffect.value, (int) enchantResist3[condition1], condition1))
            break;
          this.FailCondition(self, target, skill, cond_target, condEffect.param, condEffect.param.type, condEffect.param.cond, condition1, condEffect.param.chk_target, condEffect.param.chk_timing, (int) condEffect.turn, is_passive, condEffect.IsCurse, log, is_same_ow);
          break;
        case ConditionEffectTypes.DisableCondition:
          if (condEffect == null || condEffect.param == null || condEffect.param.conditions == null)
            break;
          for (int index2 = 0; index2 < condEffect.param.conditions.Length; ++index2)
          {
            CondAttachment condAttachment = this.CreateCondAttachment(self, target, skill, cond_target, condEffect.param, type, condEffect.param.cond, condEffect.param.conditions[index2], condEffect.param.chk_target, condEffect.param.chk_timing, (int) condEffect.turn, is_passive, false);
            target.SetCondAttachment(condAttachment);
          }
          break;
      }
    }

    private void CondSkillSetRateLog(ESkillTiming timing, Unit self, Unit target, SkillData skill, LogSkill log)
    {
      if (self == null || target == null || (skill == null || log == null) || timing != skill.Timing)
        return;
      CondEffect condEffect = skill.GetCondEffect(SkillEffectTargets.Target);
      if (condEffect == null || condEffect.param == null || (!condEffect.CheckEnableCondTarget(target) || condEffect.param.type == ConditionEffectTypes.None) || (condEffect.param.conditions == null || (int) condEffect.value == 0))
        return;
      DependStateSpcEffParam stateSpcEffParam = skill.GetDependStateSpcEffParam();
      if (stateSpcEffParam != null && !stateSpcEffParam.IsApplyCond(target, SkillEffectTargets.Target))
        return;
      LogSkill.Target target1 = log.FindTarget(target);
      if (target1 == null)
        return;
      target1.CondHitLists.Clear();
      switch (condEffect.param.type)
      {
        case ConditionEffectTypes.FailCondition:
        case ConditionEffectTypes.ForcedFailCondition:
        case ConditionEffectTypes.RandomFailCondition:
          EnchantParam enchantAssist = self.CurrentStatus.enchant_assist;
          EnchantParam enchantResist = target.CurrentStatus.enchant_resist;
          foreach (EUnitCondition condition in condEffect.param.conditions)
          {
            LogSkill.Target.CondHit condHit = new LogSkill.Target.CondHit(condition, 0);
            if (!target.IsDisableUnitCondition(condition) || condEffect.param.type == ConditionEffectTypes.ForcedFailCondition)
            {
              int num1 = (int) condEffect.rate;
              if (num1 <= 0 || num1 > 100)
                num1 = 100;
              if (condEffect.param.type != ConditionEffectTypes.ForcedFailCondition)
              {
                int num2 = Math.Max(0, Math.Min((int) enchantAssist[condition] + (int) condEffect.value - (int) enchantResist[condition], 100));
                num1 = num1 * num2 / 100;
              }
              condHit.Per = num1;
            }
            target1.CondHitLists.Add(condHit);
          }
          break;
      }
    }

    public CondAttachment CreateCondAttachment(Unit user, Unit target, SkillData skill, SkillEffectTargets skilltarget, CondEffectParam param, ConditionEffectTypes type, ESkillCondition cond, EUnitCondition condition, EffectCheckTargets chktgt, EffectCheckTimings timing, int turn, bool is_passive, bool is_curse)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return (CondAttachment) null;
      if (type == ConditionEffectTypes.None && skill != null && !skill.IsDamagedSkill())
        return (CondAttachment) null;
      CondAttachment condAttachment = new CondAttachment(param);
      condAttachment.user = user;
      condAttachment.turn = (OInt) turn;
      condAttachment.skill = skill;
      condAttachment.IsPassive = (OBool) is_passive;
      condAttachment.CondType = type;
      condAttachment.Condition = condition;
      condAttachment.CheckTarget = (Unit) null;
      condAttachment.CheckTiming = timing;
      condAttachment.UseCondition = cond;
      condAttachment.skilltarget = skilltarget;
      condAttachment.SetupLinkageBuff();
      switch (chktgt)
      {
        case EffectCheckTargets.Target:
          condAttachment.CheckTarget = target;
          break;
        case EffectCheckTargets.User:
          condAttachment.CheckTarget = user;
          break;
      }
      if (condAttachment.IsFailCondition())
        condAttachment.IsCurse = is_curse;
      return condAttachment;
    }

    private bool CheckFailCondition(Unit target, int val, int resist, EUnitCondition condition)
    {
      if (val <= 0)
        return false;
      int num1 = val - resist;
      if (num1 <= 0)
        return false;
      int num2 = (int) (this.GetRandom() % 100U);
      return num1 > num2;
    }

    private void EndMoveSkill(Unit self, int step)
    {
    }

    private bool CheckEnableUseSkill(Unit self, SkillData skill, bool bCheckCondOnly = false)
    {
      return self.CheckEnableUseSkill(skill, bCheckCondOnly);
    }

    public bool CheckEnemySide(Unit self, Unit target)
    {
      if (self == target)
        return false;
      if (!self.IsUnitCondition(EUnitCondition.Charm) && !self.IsUnitCondition(EUnitCondition.Zombie))
        return self.Side != target.Side;
      if (target.IsUnitCondition(EUnitCondition.Charm) || target.IsUnitCondition(EUnitCondition.Zombie))
        return false;
      return self.Side == target.Side;
    }

    public bool CheckGimmickEnemySide(Unit self, Unit target)
    {
      if (self == null || target == null || (!target.IsGimmick || self == target))
        return false;
      EUnitSide eunitSide = EUnitSide.Neutral;
      if (this.IsMultiVersus)
      {
        if (target.BreakObjSideType == eMapBreakSideType.PLAYER)
        {
          eunitSide = EUnitSide.Player;
          if (this.mMyPlayerIndex > 0 && target.OwnerPlayerIndex > 0 && this.mMyPlayerIndex != target.OwnerPlayerIndex)
            eunitSide = EUnitSide.Enemy;
        }
        else if (target.BreakObjSideType == eMapBreakSideType.ENEMY)
        {
          eunitSide = EUnitSide.Enemy;
          if (this.mMyPlayerIndex > 0 && target.OwnerPlayerIndex > 0 && this.mMyPlayerIndex != target.OwnerPlayerIndex)
            eunitSide = EUnitSide.Player;
        }
      }
      else if (target.BreakObjSideType == eMapBreakSideType.PLAYER)
        eunitSide = EUnitSide.Player;
      else if (target.BreakObjSideType == eMapBreakSideType.ENEMY)
        eunitSide = EUnitSide.Enemy;
      if (self.IsUnitCondition(EUnitCondition.Charm) || self.IsUnitCondition(EUnitCondition.Zombie))
        return self.Side == eunitSide;
      return self.Side != eunitSide;
    }

    public bool CheckSkillTarget(Unit self, Unit target, SkillData skill)
    {
      this.DebugAssert(self != null, "self == null");
      this.DebugAssert(skill != null, "failed. skill != null");
      if (target == null)
        return false;
      if (target.IsGimmick)
        return this.IsTargetBreakUnit(self, target, skill);
      eSkillTargetEx targetEx = skill.SkillParam.TargetEx;
      if (target.CastSkill != null && target.CastSkill.CastType == ECastTypes.Jump)
      {
        switch (skill.SkillParam.TargetEx)
        {
          case eSkillTargetEx.JumpInc:
          case eSkillTargetEx.JumpOnly:
            break;
          default:
            return false;
        }
      }
      else if (targetEx == eSkillTargetEx.JumpOnly)
        return false;
      if (skill.EffectType == SkillEffectTypes.Changing && !target.IsChanging)
        return false;
      bool flag = false;
      ESkillTarget target1 = skill.Target;
      switch (target1)
      {
        case ESkillTarget.Self:
          flag = self == target;
          break;
        case ESkillTarget.SelfSide:
        case ESkillTarget.SelfSideNotSelf:
          flag = !this.CheckEnemySide(self, target);
          if (target1 == ESkillTarget.SelfSideNotSelf && flag)
          {
            flag = self != target;
            break;
          }
          break;
        case ESkillTarget.EnemySide:
          flag = this.CheckEnemySide(self, target);
          break;
        case ESkillTarget.UnitAll:
          flag = true;
          break;
        case ESkillTarget.NotSelf:
          flag = self != target;
          break;
        case ESkillTarget.GridNoUnit:
          if (skill.TeleportType != eTeleportType.None)
          {
            switch (skill.TeleportTarget)
            {
              case ESkillTarget.Self:
                flag = self == target;
                break;
              case ESkillTarget.SelfSide:
              case ESkillTarget.SelfSideNotSelf:
                flag = !this.CheckEnemySide(self, target);
                if (skill.TeleportTarget == ESkillTarget.SelfSideNotSelf && flag)
                {
                  flag = self != target;
                  break;
                }
                break;
              case ESkillTarget.EnemySide:
                flag = this.CheckEnemySide(self, target);
                break;
              case ESkillTarget.UnitAll:
                flag = true;
                break;
              case ESkillTarget.NotSelf:
                flag = self != target;
                break;
            }
          }
          else
          {
            flag = false;
            break;
          }
      }
      if (!flag)
        return false;
      if (skill.EffectType == SkillEffectTypes.Revive)
        return target.IsDead;
      return !target.IsDead;
    }

    public int GetAttackRangeBonus(int selfHeight, int targetHeight)
    {
      int num = selfHeight - targetHeight;
      if (Math.Abs(num) < BattleMap.MAP_FLOOR_HEIGHT)
        return 0;
      return num / BattleMap.MAP_FLOOR_HEIGHT;
    }

    public GridMap<bool> CreateSelectGridMap(Unit self, int targetX, int targetY, SkillData skill)
    {
      BattleMap currentMap = this.CurrentMap;
      GridMap<bool> result = new GridMap<bool>(currentMap.Width, currentMap.Height);
      this.CreateSelectGridMap(self, targetX, targetY, skill, ref result, false);
      return result;
    }

    public GridMap<bool> CreateSelectGridMap(Unit self, int targetX, int targetY, SkillData skill, ref GridMap<bool> result, bool keeped = false)
    {
      SkillParam skillParam = skill.SkillParam;
      int attackRangeMin = self.GetAttackRangeMin(skill);
      int attackRangeMax = self.GetAttackRangeMax(skill);
      ELineType lineType = skillParam.line_type;
      ESelectType selectRange = skillParam.select_range;
      int attackScope = self.GetAttackScope(skill);
      int attackHeight = self.GetAttackHeight(skill, true);
      bool bCheckGridLine = skill.CheckGridLineSkill();
      bool bHeightBonus = skill.IsEnableHeightRangeBonus();
      bool bSelfEffect = skill.IsSelfTargetSelect();
      return this.CreateSelectGridMap(self, targetX, targetY, attackRangeMin, attackRangeMax, selectRange, lineType, attackScope, bCheckGridLine, bHeightBonus, attackHeight, bSelfEffect, ref result, keeped);
    }

    private GridMap<bool> CreateSelectGridMap(Unit self, int targetX, int targetY, int range_min, int range_max, ESelectType rangetype, ELineType linetype, int scope, bool bCheckGridLine, bool bHeightBonus, int attack_height, bool bSelfEffect, ref GridMap<bool> result, bool keeped = false)
    {
      BattleMap currentMap = this.CurrentMap;
      GridMap<bool> result1 = keeped ? new GridMap<bool>(currentMap.Width, currentMap.Height) : result;
      result1.fill(false);
      Grid grid = currentMap[targetX, targetY];
      if (range_max > 0 || rangetype == ESelectType.All)
      {
        int range_max1 = range_max;
        if (bHeightBonus)
          range_max1 += this.GetAttackRangeBonus(grid.height, 0);
        switch (rangetype)
        {
          case ESelectType.Diamond:
            this.CreateGridMapDiamond(grid, range_min, range_max1, ref result1);
            break;
          case ESelectType.Square:
            this.CreateGridMapSquare(grid, range_min, range_max1, ref result1);
            break;
          case ESelectType.Laser:
            for (int index1 = 0; index1 < 4; ++index1)
            {
              int num1 = Unit.DIRECTION_OFFSETS[index1, 0];
              int num2 = Unit.DIRECTION_OFFSETS[index1, 1];
              int index2 = grid.x + num1;
              int index3 = grid.y + num2;
              this.CreateGridMapLaser(grid, currentMap[index2, index3], range_min, range_max, scope, ref result1);
            }
            break;
          case ESelectType.All:
            result1.fill(true);
            break;
          case ESelectType.Bishop:
            this.CreateGridMapBishop(grid, range_min, range_max1, ref result1);
            break;
          case ESelectType.LaserSpread:
            for (int index = 0; index < 4; ++index)
            {
              Grid target = currentMap[grid.x + Unit.DIRECTION_OFFSETS[index, 0], grid.y + Unit.DIRECTION_OFFSETS[index, 1]];
              this.CreateGridMapLaserSpread(grid, target, range_min, range_max, ref result1, false);
            }
            break;
          case ESelectType.LaserWide:
            for (int index = 0; index < 4; ++index)
            {
              Grid target = currentMap[grid.x + Unit.DIRECTION_OFFSETS[index, 0], grid.y + Unit.DIRECTION_OFFSETS[index, 1]];
              this.CreateGridMapLaserWide(grid, target, range_min, range_max, ref result1, false);
            }
            break;
          case ESelectType.Horse:
            this.CreateGridMapHorse(grid, range_min, range_max1, ref result1);
            break;
          case ESelectType.LaserTwin:
            for (int index = 0; index < 4; ++index)
            {
              Grid target = currentMap[grid.x + Unit.DIRECTION_OFFSETS[index, 0], grid.y + Unit.DIRECTION_OFFSETS[index, 1]];
              this.CreateGridMapLaserTwin(grid, target, range_min, range_max, ref result1, false);
            }
            break;
          case ESelectType.LaserTriple:
            for (int index = 0; index < 4; ++index)
            {
              Grid target = currentMap[grid.x + Unit.DIRECTION_OFFSETS[index, 0], grid.y + Unit.DIRECTION_OFFSETS[index, 1]];
              this.CreateGridMapLaserTriple(grid, target, range_min, range_max, ref result1, false);
            }
            break;
          default:
            this.CreateGridMapCross(grid, range_min, range_max1, ref result1);
            break;
        }
        if (SkillParam.IsTypeLaser(rangetype))
        {
          for (int x = 0; x < result1.w; ++x)
          {
            for (int y = 0; y < result1.h; ++y)
            {
              if (result1.get(x, y) && !this.CheckEnableAttackHeight(grid, currentMap[x, y], attack_height))
                result1.set(x, y, false);
            }
          }
        }
        else
        {
          for (int index1 = -range_max1; index1 <= range_max1; ++index1)
          {
            for (int index2 = -range_max1; index2 <= range_max1; ++index2)
            {
              int num1 = rangetype != ESelectType.Square ? Math.Abs(index2) + Math.Abs(index1) : Math.Max(Math.Abs(index2), Math.Abs(index1));
              if (num1 <= range_max1)
              {
                int index3 = index2 + grid.x;
                int index4 = index1 + grid.y;
                Grid goal = currentMap[index3, index4];
                if (goal != null && goal != grid && result1.get(index3, index4))
                {
                  int num2 = 0;
                  if (bHeightBonus)
                    num2 = this.GetAttackRangeBonus(grid.height, goal.height);
                  if (num1 > range_max + num2)
                    result1.set(index3, index4, false);
                  else if (goal.geo != null && (bool) goal.geo.DisableStopped)
                    result1.set(goal.x, goal.y, false);
                  else if (linetype == ELineType.None)
                  {
                    if (!this.CheckEnableAttackHeight(grid, goal, attack_height))
                      result1.set(goal.x, goal.y, false);
                  }
                  else
                  {
                    this.GetSkillGridLines(grid.x, grid.y, index3, index4, range_min, range_max, attack_height, linetype, bHeightBonus, ref this.mGridLines);
                    result1.set(goal.x, goal.y, this.mGridLines.Contains(goal));
                  }
                }
              }
            }
          }
        }
      }
      result1.set(grid.x, grid.y, bSelfEffect);
      bool flag = self != null && !self.IsNormalSize;
      for (int index1 = 0; index1 < result1.w; ++index1)
      {
        for (int index2 = 0; index2 < result1.h; ++index2)
        {
          if (result1.get(index1, index2))
          {
            if (flag && (!bSelfEffect || index1 != grid.x || index2 != grid.y) && self.CheckCollisionDirect(targetX, targetY, index1, index2, true))
              result.set(index1, index2, false);
            else
              result.set(index1, index2, true);
          }
        }
      }
      return result;
    }

    private bool CheckGridOnLine(int x1, int y1, int x2, int y2, int sx, int sy, int tx, int ty)
    {
      long num1 = (long) (tx - sx);
      long num2 = (long) (ty - sy);
      long num3 = (long) (x1 - sx);
      long num4 = (long) (y1 - sy);
      long num5 = (long) (x2 - sx);
      long num6 = (long) (y2 - sy);
      if ((num1 * num4 - num2 * num3) * (num1 * num6 - num2 * num5) > 0L)
        return false;
      long num7 = (long) (x2 - x1);
      long num8 = (long) (y2 - y1);
      long num9 = (long) (sx - x1);
      long num10 = (long) (sy - y1);
      long num11 = (long) (tx - x1);
      long num12 = (long) (ty - y1);
      return (num7 * num10 - num8 * num9) * (num7 * num12 - num8 * num11) <= 0L;
    }

    private void GetGridOnLine(Grid start, Grid target, ref List<Grid> results)
    {
      this.GetGridOnLine(start, target.x, target.y, ref results);
    }

    private void GetGridOnLine(Grid start, int tx, int ty, ref List<Grid> results)
    {
      BattleMap currentMap = this.CurrentMap;
      results.Clear();
      int num1 = 100;
      int num2 = start.x * num1;
      int num3 = start.y * num1;
      int num4 = tx * num1;
      int num5 = ty * num1;
      for (int index1 = 0; index1 < currentMap.Width; ++index1)
      {
        for (int index2 = 0; index2 < currentMap.Height; ++index2)
        {
          Grid grid = currentMap[index1, index2];
          if (grid != start)
          {
            int num6 = grid.y * num1 + 45;
            int num7 = grid.y * num1 - 45;
            int num8 = grid.x * num1 - 45;
            int num9 = grid.x * num1 + 45;
            if (Math.Min(num8, num9) <= Math.Max(num2, num4) && Math.Min(num6, num7) <= Math.Max(num3, num5) && (Math.Min(num2, num4) <= Math.Max(num8, num9) && Math.Min(num3, num5) <= Math.Max(num6, num7)) && ((this.CheckGridOnLine(num8, num6, num9, num7, num2, num3, num4, num5) || this.CheckGridOnLine(num9, num6, num8, num7, num2, num3, num4, num5)) && !this.mGridLines.Contains(grid)))
              this.mGridLines.Add(grid);
          }
        }
      }
      MySort<Grid>.Sort(results, (Comparison<Grid>) ((src, dsc) =>
      {
        if (src == dsc)
          return 0;
        return this.CalcGridDistance(start, src) - this.CalcGridDistance(start, dsc);
      }));
    }

    public GridMap<bool> CreateScopeGridMap(Unit self, int selfX, int selfY, int targetX, int targetY, SkillData skill)
    {
      BattleMap currentMap = this.CurrentMap;
      GridMap<bool> result = new GridMap<bool>(currentMap.Width, currentMap.Height);
      this.CreateScopeGridMap(self, selfX, selfY, targetX, targetY, skill, ref result, false);
      return result;
    }

    public GridMap<bool> CreateScopeGridMap(Unit self, int selfX, int selfY, int targetX, int targetY, SkillData skill, ref GridMap<bool> result, bool keeped = false)
    {
      SkillParam skillParam = skill.SkillParam;
      int attackRangeMin = self.GetAttackRangeMin(skill);
      int attackRangeMax = self.GetAttackRangeMax(skill);
      int attackScope = self.GetAttackScope(skill);
      int attackHeight = self.GetAttackHeight(skill, false);
      ESelectType selectScope = skillParam.select_scope;
      this.CreateScopeGridMap(self, selfX, selfY, targetX, targetY, attackRangeMin, attackRangeMax, attackScope, attackHeight, selectScope, ref result, keeped, skill.TeleportType);
      return result;
    }

    public GridMap<bool> CreateScopeGridMap(int gx, int gy, ESelectType shape, int scope, int height)
    {
      if (this.CurrentMap == null)
        return (GridMap<bool>) null;
      GridMap<bool> result = new GridMap<bool>(this.CurrentMap.Width, this.CurrentMap.Height);
      this.CreateScopeGridMap((Unit) null, 0, 0, gx, gy, 0, 0, scope, height, shape, ref result, false, eTeleportType.None);
      return result;
    }

    public GridMap<bool> CreateScopeGridMap(Unit self, int selfX, int selfY, int targetX, int targetY, int range_min, int range_max, int scope, int enable_height, ESelectType scopetype, ref GridMap<bool> result, bool keeped, eTeleportType teleport_type)
    {
      GridMap<bool> gridMap = keeped ? new GridMap<bool>(result.w, result.h) : result;
      gridMap.fill(false);
      bool flag1 = false;
      if (scope < 1)
      {
        if (SkillParam.IsTypeLaser(scopetype))
          return result;
        if (scopetype != ESelectType.All)
        {
          gridMap.set(targetX, targetY, true);
          flag1 = true;
        }
      }
      int x1 = targetX;
      int y1 = targetY;
      if (teleport_type == eTeleportType.AfterSkill)
      {
        targetX = selfX;
        targetY = selfY;
      }
      BattleMap currentMap = this.CurrentMap;
      Grid grid = currentMap[targetX, targetY];
      Grid start = grid;
      if (!flag1)
      {
        Grid self1 = currentMap[selfX, selfY];
        switch (scopetype)
        {
          case ESelectType.Diamond:
            this.SetGridMap(ref gridMap, grid, grid);
            this.CreateGridMapDiamond(grid, 0, scope, ref gridMap);
            break;
          case ESelectType.Square:
            this.SetGridMap(ref gridMap, grid, grid);
            this.CreateGridMapSquare(grid, 0, scope, ref gridMap);
            break;
          case ESelectType.Laser:
            this.CreateGridMapLaser(self1, grid, range_min, range_max, scope, ref gridMap);
            start = self1;
            break;
          case ESelectType.All:
            gridMap.fill(true);
            break;
          case ESelectType.Wall:
            this.SetGridMap(ref gridMap, grid, grid);
            this.CreateGridMapWall(self1, grid, 0, scope, ref gridMap);
            break;
          case ESelectType.WallPlus:
            this.SetGridMap(ref gridMap, grid, grid);
            this.CreateGridMapWallPlus(self1, grid, 0, scope, ref gridMap);
            break;
          case ESelectType.Bishop:
            this.SetGridMap(ref gridMap, grid, grid);
            this.CreateGridMapBishop(grid, 0, scope, ref gridMap);
            break;
          case ESelectType.Victory:
            this.SetGridMap(ref gridMap, grid, grid);
            this.CreateGridMapVictory(self1, grid, 0, scope, ref gridMap);
            break;
          case ESelectType.LaserSpread:
            this.CreateGridMapLaserSpread(self1, grid, range_min, range_max, ref gridMap, true);
            start = self1;
            break;
          case ESelectType.LaserWide:
            this.CreateGridMapLaserWide(self1, grid, range_min, range_max, ref gridMap, true);
            start = self1;
            break;
          case ESelectType.Horse:
            this.SetGridMap(ref gridMap, grid, grid);
            this.CreateGridMapHorse(grid, 0, scope, ref gridMap);
            break;
          case ESelectType.LaserTwin:
            this.CreateGridMapLaserTwin(self1, grid, range_min, range_max, ref gridMap, true);
            start = self1;
            break;
          case ESelectType.LaserTriple:
            this.CreateGridMapLaserTriple(self1, grid, range_min, range_max, ref gridMap, true);
            start = self1;
            break;
          case ESelectType.SquareOutline:
            this.CreateGridMapSquare(grid, 0, scope, ref gridMap);
            gridMap.set(grid.x, grid.y, false);
            break;
          default:
            this.SetGridMap(ref gridMap, grid, grid);
            this.CreateGridMapCross(grid, 0, scope, ref gridMap);
            break;
        }
      }
      bool flag2 = self != null && !self.IsNormalSize;
      for (int index1 = gridMap.h - 1; index1 >= 0; --index1)
      {
        for (int index2 = 0; index2 < gridMap.w; ++index2)
        {
          if (gridMap.get(index2, index1))
          {
            Grid goal = currentMap[index2, index1];
            if (!this.CheckEnableAttackHeight(start, goal, enable_height))
              gridMap.set(index2, index1, false);
            if (goal.geo != null && (bool) goal.geo.DisableStopped)
              gridMap.set(index2, index1, false);
            if (flag2 && self.CheckCollisionDirect(selfX, selfY, index2, index1, true))
              gridMap.set(index2, index1, false);
          }
        }
      }
      if (teleport_type == eTeleportType.AfterSkill)
        gridMap.set(x1, y1, true);
      if (keeped)
      {
        for (int x2 = 0; x2 < gridMap.w; ++x2)
        {
          for (int y2 = 0; y2 < gridMap.h; ++y2)
          {
            if (gridMap.get(x2, y2))
              result.set(x2, y2, true);
          }
        }
      }
      return result;
    }

    public void CreateGridMapCross(Grid target, int range_min, int range_max, ref GridMap<bool> result)
    {
      if (target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      for (int index1 = range_min + 1; index1 <= range_max; ++index1)
      {
        for (int index2 = 0; index2 < 4; ++index2)
        {
          int num1 = Unit.DIRECTION_OFFSETS[index2, 0] * index1;
          int num2 = Unit.DIRECTION_OFFSETS[index2, 1] * index1;
          int index3 = target.x + num1;
          int index4 = target.y + num2;
          Grid goal = currentMap[index3, index4];
          this.SetGridMap(ref result, target, goal);
        }
      }
    }

    private void CreateGridMapDiamond(Grid target, int range_min, int range_max, ref GridMap<bool> result)
    {
      if (target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      for (int index1 = -range_max; index1 <= range_max; ++index1)
      {
        for (int index2 = -range_max; index2 <= range_max; ++index2)
        {
          if (Math.Abs(index2) + Math.Abs(index1) <= range_max)
          {
            int index3 = target.x + index2;
            int index4 = target.y + index1;
            Grid goal = currentMap[index3, index4];
            if (range_min <= 0 || range_max <= 0 || this.CalcGridDistance(target, goal) > range_min)
              this.SetGridMap(ref result, target, goal);
          }
        }
      }
    }

    private void CreateGridMapSquare(Grid target, int range_min, int range_max, ref GridMap<bool> result)
    {
      if (target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      for (int index1 = -range_max; index1 <= range_max; ++index1)
      {
        if (range_min <= 0 || range_max <= 0 || range_min < Math.Abs(index1))
        {
          for (int index2 = -range_max; index2 <= range_max; ++index2)
          {
            if (range_min <= 0 || range_max <= 0 || range_min < Math.Abs(index2))
            {
              int index3 = target.x + index2;
              int index4 = target.y + index1;
              Grid goal = currentMap[index3, index4];
              this.SetGridMap(ref result, target, goal);
            }
          }
        }
      }
    }

    private void CreateGridMapLaser(Grid self, Grid target, int range_min, int range_max, int scope, ref GridMap<bool> result)
    {
      if (self == target || target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      int index1 = (int) this.UnitDirectionFromGrid(self, target);
      int num1 = Math.Max(scope - 1, 0);
      for (int index2 = range_min + 1; index2 <= range_max; ++index2)
      {
        for (int index3 = -num1; index3 <= num1; ++index3)
        {
          int num2 = Unit.DIRECTION_OFFSETS[index1, 0] * index2;
          int num3 = Unit.DIRECTION_OFFSETS[index1, 1] * index2;
          int index4 = self.x + num2 + Unit.DIRECTION_OFFSETS[index1, 1] * index3;
          int index5 = self.y + num3 + Unit.DIRECTION_OFFSETS[index1, 0] * index3;
          Grid goal = currentMap[index4, index5];
          this.SetGridMap(ref result, target, goal);
        }
      }
      if (result.get(target.x, target.y))
        return;
      result.fill(false);
    }

    private EUnitDirection unitDirectionFromPos(int src_gx, int src_gy, int dst_gx, int dst_gy)
    {
      return this.UnitDirectionFromGrid(this.GetUnitGridPosition(src_gx, src_gy), this.GetUnitGridPosition(dst_gx, dst_gy));
    }

    public EUnitDirection UnitDirectionFromGrid(Grid self, Grid target)
    {
      if (self == null || target == null)
        return EUnitDirection.PositiveY;
      int num1 = target.x - self.x;
      int num2 = target.y - self.y;
      int num3 = Math.Abs(num1);
      int num4 = Math.Abs(num2);
      if (num3 > num4)
      {
        if (num1 < 0)
          return EUnitDirection.NegativeX;
        if (num1 > 0)
          return EUnitDirection.PositiveX;
      }
      if (num3 < num4)
      {
        if (num2 < 0)
          return EUnitDirection.NegativeY;
        if (num2 > 0)
          return EUnitDirection.PositiveY;
      }
      if (num1 > 0)
        return EUnitDirection.PositiveX;
      if (num1 < 0)
        return EUnitDirection.NegativeX;
      return num2 > 0 || num2 >= 0 ? EUnitDirection.PositiveY : EUnitDirection.NegativeY;
    }

    public EUnitDirection UnitDirectionFromGridLaserTwin(Grid self, Grid target)
    {
      int num1 = target.x - self.x;
      int num2 = target.y - self.y;
      int num3 = Math.Abs(num1);
      int num4 = Math.Abs(num2);
      if (num3 > num4)
      {
        if (num1 < 0)
          return EUnitDirection.NegativeX;
        if (num1 > 0)
          return EUnitDirection.PositiveX;
      }
      if (num3 < num4)
      {
        if (num2 < 0)
          return EUnitDirection.NegativeY;
        if (num2 > 0)
          return EUnitDirection.PositiveY;
      }
      if (num1 > 0)
        return num2 < 0 ? EUnitDirection.NegativeY : EUnitDirection.PositiveX;
      if (num1 < 0)
        return num2 > 0 ? EUnitDirection.PositiveY : EUnitDirection.NegativeX;
      return num2 > 0 || num2 >= 0 ? EUnitDirection.PositiveY : EUnitDirection.NegativeY;
    }

    private void CreateGridMapWall(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result)
    {
      if (self == target || target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      int index1 = (int) this.UnitDirectionFromGrid(self, target);
      for (int index2 = -range_max; index2 <= range_max; ++index2)
      {
        if (Math.Abs(index2) >= range_min)
        {
          int num1 = Unit.DIRECTION_OFFSETS[index1, 1] * index2;
          int num2 = Unit.DIRECTION_OFFSETS[index1, 0] * index2;
          this.SetGridMap(ref result, target, currentMap[target.x + num1, target.y + num2]);
        }
      }
    }

    private void CreateGridMapWallPlus(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result)
    {
      if (self == target || target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      int index1 = (int) this.UnitDirectionFromGrid(self, target);
      for (int index2 = -range_max; index2 <= range_max; ++index2)
      {
        if (Math.Abs(index2) >= range_min)
        {
          int num1 = Unit.DIRECTION_OFFSETS[index1, 1] * index2;
          int num2 = Unit.DIRECTION_OFFSETS[index1, 0] * index2;
          this.SetGridMap(ref result, target, currentMap[target.x + num1, target.y + num2]);
          int num3 = num1 + Unit.DIRECTION_OFFSETS[index1, 0];
          int num4 = num2 + Unit.DIRECTION_OFFSETS[index1, 1];
          this.SetGridMap(ref result, target, currentMap[target.x + num3, target.y + num4]);
        }
      }
    }

    private void CreateGridMapBishop(Grid target, int range_min, int range_max, ref GridMap<bool> result)
    {
      if (target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      for (int index1 = -range_max; index1 <= range_max; ++index1)
      {
        if (range_min <= 0 || range_max <= 0 || range_min < Math.Abs(index1))
        {
          for (int index2 = -range_max; index2 <= range_max; ++index2)
          {
            if ((range_min <= 0 || range_max <= 0 || range_min < Math.Abs(index2)) && Math.Abs(index2) == Math.Abs(index1))
              this.SetGridMap(ref result, target, currentMap[target.x + index2, target.y + index1]);
          }
        }
      }
    }

    private void CreateGridMapVictory(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result)
    {
      if (self == target || target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      int index1 = (int) this.UnitDirectionFromGrid(self, target);
      for (int index2 = -range_max; index2 <= range_max; ++index2)
      {
        if (Math.Abs(index2) >= range_min)
        {
          int num1 = Unit.DIRECTION_OFFSETS[index1, 1] * index2;
          int num2 = Unit.DIRECTION_OFFSETS[index1, 0] * index2;
          int num3 = num1 + Unit.DIRECTION_OFFSETS[index1, 0] * Math.Abs(index2);
          int num4 = num2 + Unit.DIRECTION_OFFSETS[index1, 1] * Math.Abs(index2);
          this.SetGridMap(ref result, target, currentMap[target.x + num3, target.y + num4]);
        }
      }
    }

    private void CreateGridMapLaserSpread(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result, bool is_chk_clear = true)
    {
      if (self == target || target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      int index1 = (int) this.UnitDirectionFromGrid(self, target);
      for (int index2 = range_min; index2 <= range_max; ++index2)
      {
        int num1 = Unit.DIRECTION_OFFSETS[index1, 0] * (index2 + 1);
        int num2 = Unit.DIRECTION_OFFSETS[index1, 1] * (index2 + 1);
        for (int index3 = -index2; index3 <= index2; ++index3)
        {
          int num3 = Unit.DIRECTION_OFFSETS[index1, 1] * index3;
          int num4 = Unit.DIRECTION_OFFSETS[index1, 0] * index3;
          this.SetGridMap(ref result, target, currentMap[self.x + num1 + num3, self.y + num2 + num4]);
        }
      }
      if (!is_chk_clear || result.get(target.x, target.y))
        return;
      result.fill(false);
    }

    private void CreateGridMapLaserWide(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result, bool is_chk_clear = true)
    {
      if (self == target || target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      int index1 = (int) this.UnitDirectionFromGrid(self, target);
      for (int index2 = range_min; index2 <= range_max; ++index2)
      {
        if (index2 != 0)
        {
          int num1 = Unit.DIRECTION_OFFSETS[index1, 0] * (index2 + 1);
          int num2 = Unit.DIRECTION_OFFSETS[index1, 1] * (index2 + 1);
          this.SetGridMap(ref result, target, currentMap[self.x + num1, self.y + num2]);
          for (int index3 = 0; index3 < 4; ++index3)
          {
            int num3 = Unit.DIRECTION_OFFSETS[index3, 0];
            int num4 = Unit.DIRECTION_OFFSETS[index3, 1];
            this.SetGridMap(ref result, target, currentMap[self.x + num1 + num3, self.y + num2 + num4]);
          }
        }
      }
      if (!is_chk_clear || result.get(target.x, target.y))
        return;
      result.fill(false);
    }

    private void CreateGridMapHorse(Grid target, int range_min, int range_max, ref GridMap<bool> result)
    {
      if (target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      ++range_max;
      for (int index1 = -range_max; index1 <= range_max; ++index1)
      {
        if (range_min <= 0 || range_max <= 0 || range_min < Math.Abs(index1))
        {
          for (int index2 = -range_max; index2 <= range_max; ++index2)
          {
            if ((range_min <= 0 || range_max <= 0 || range_min < Math.Abs(index2)) && (Math.Abs(index2) == Math.Abs(index1) || Math.Abs(index2) <= 1 && Math.Abs(index1) <= 1))
              this.SetGridMap(ref result, target, currentMap[target.x + index2, target.y + index1]);
          }
        }
      }
    }

    private void CreateGridMapLaserTwin(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result, bool is_chk_clear = true)
    {
      if (self == target || target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      int index1 = (int) this.UnitDirectionFromGridLaserTwin(self, target);
      for (int index2 = range_min; index2 <= range_max; ++index2)
      {
        if (index2 != 0)
        {
          int num1 = Unit.DIRECTION_OFFSETS[index1, 0] * index2;
          int num2 = Unit.DIRECTION_OFFSETS[index1, 1] * index2;
          for (int index3 = -1; index3 <= 1; ++index3)
          {
            if (index3 != 0)
            {
              int num3 = Unit.DIRECTION_OFFSETS[index1, 1] * index3;
              int num4 = Unit.DIRECTION_OFFSETS[index1, 0] * index3;
              this.SetGridMap(ref result, target, currentMap[self.x + num1 + num3, self.y + num2 + num4]);
            }
          }
        }
      }
      if (!is_chk_clear || result.get(target.x, target.y))
        return;
      result.fill(false);
    }

    private void CreateGridMapLaserTriple(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result, bool is_chk_clear = true)
    {
      if (self == target || target == null)
        return;
      BattleMap currentMap = this.CurrentMap;
      int index1 = (int) this.UnitDirectionFromGrid(self, target);
      for (int index2 = range_min; index2 <= range_max; ++index2)
      {
        int num1 = Unit.DIRECTION_OFFSETS[index1, 0] * (index2 + 2);
        int num2 = Unit.DIRECTION_OFFSETS[index1, 1] * (index2 + 2);
        if (index2 == 0)
        {
          int num3 = Unit.DIRECTION_OFFSETS[index1, 0];
          int num4 = Unit.DIRECTION_OFFSETS[index1, 1];
          this.SetGridMap(ref result, target, currentMap[self.x + num3, self.y + num4]);
          for (int index3 = -1; index3 <= 1; ++index3)
          {
            int num5 = Unit.DIRECTION_OFFSETS[index1, 1] * index3;
            int num6 = Unit.DIRECTION_OFFSETS[index1, 0] * index3;
            this.SetGridMap(ref result, target, currentMap[self.x + num1 + num5, self.y + num2 + num6]);
          }
        }
        else
        {
          for (int index3 = -2; index3 <= 2; ++index3)
          {
            if (Math.Abs(index3) != 1)
            {
              int num3 = Unit.DIRECTION_OFFSETS[index1, 1] * index3;
              int num4 = Unit.DIRECTION_OFFSETS[index1, 0] * index3;
              this.SetGridMap(ref result, target, currentMap[self.x + num1 + num3, self.y + num2 + num4]);
            }
          }
        }
      }
      if (!is_chk_clear || result.get(target.x, target.y))
        return;
      result.fill(false);
    }

    private bool CheckEnableAttackHeight(Grid start, Grid goal, int diff_ok)
    {
      return Math.Abs(goal.height - start.height) <= diff_ok;
    }

    private void SetGridMap(ref GridMap<bool> gridmap, Grid start, Grid goal)
    {
      if (goal == null || gridmap.get(goal.x, goal.y))
        return;
      gridmap.set(goal.x, goal.y, true);
    }

    private GridMap<bool> CreateSkillRangeMapAll(Unit self, SkillData skill, bool is_move)
    {
      GridMap<bool> result = new GridMap<bool>(this.CurrentMap.Width, this.CurrentMap.Height);
      result.fill(false);
      if (is_move && !self.IsUnitFlag(EUnitFlag.Moved) && self.IsEnableMoveCondition(false))
      {
        GridMap<int> moveMap = this.CreateMoveMap(self, 0);
        for (int index1 = 0; index1 < moveMap.w; ++index1)
        {
          for (int index2 = 0; index2 < moveMap.h; ++index2)
          {
            if (moveMap.get(index1, index2) >= 0)
              this.CreateSelectGridMap(self, index1, index2, skill, ref result, true);
          }
        }
      }
      else
        this.CreateSelectGridMap(self, self.x, self.y, skill, ref result, true);
      return result;
    }

    private GridMap<bool> CreateSkillScopeMapAll(Unit self, SkillData skill, bool is_move)
    {
      GridMap<bool> skillRangeMapAll = this.CreateSkillRangeMapAll(self, skill, is_move);
      if (SkillParam.IsTypeLaser(skill.SkillParam.select_scope))
        return skillRangeMapAll;
      GridMap<bool> result = new GridMap<bool>(this.CurrentMap.Width, this.CurrentMap.Height);
      result.fill(false);
      for (int index1 = 0; index1 < skillRangeMapAll.w; ++index1)
      {
        for (int index2 = 0; index2 < skillRangeMapAll.h; ++index2)
        {
          if (skillRangeMapAll.get(index1, index2))
          {
            result.set(index1, index2, true);
            this.CreateScopeGridMap(self, index1, index2, index1, index2, skill, ref result, true);
          }
        }
      }
      return result;
    }

    public List<Unit> SearchTargetsInGridMap(Unit self, SkillData skill, GridMap<bool> areamap)
    {
      List<Unit> targets = new List<Unit>(this.mOrder.Count);
      this.SearchTargetsInGridMap(self, skill, areamap, targets);
      return targets;
    }

    public List<Unit> SearchTargetsInGridMap(Unit self, SkillData skill, GridMap<bool> areamap, List<Unit> targets)
    {
      BattleMap currentMap = this.CurrentMap;
      targets.Clear();
      if (areamap == null)
      {
        Grid start = currentMap[self.x, self.y];
        int unitMaxAttackHeight = this.GetUnitMaxAttackHeight(self, skill);
        for (int index = 0; index < this.mUnits.Count; ++index)
        {
          Unit mUnit = this.mUnits[index];
          if ((skill == null || this.CheckSkillTarget(self, mUnit, skill)) && this.CheckEnableAttackHeight(start, currentMap[mUnit.x, mUnit.y], unitMaxAttackHeight))
            targets.Add(mUnit);
        }
        return targets;
      }
      for (int x = 0; x < areamap.w; ++x)
      {
        for (int y = 0; y < areamap.h; ++y)
        {
          if (areamap.get(x, y))
          {
            Unit target = this.FindUnitAtGrid(currentMap[x, y]);
            if (target == null)
            {
              target = this.FindGimmickAtGrid(currentMap[x, y], false, (Unit) null);
              if (!this.IsTargetBreakUnit(self, target, skill))
                target = (Unit) null;
            }
            if (target != null && !targets.Contains(target) && (skill == null || this.CheckSkillTarget(self, target, skill)))
              targets.Add(target);
          }
        }
      }
      return targets;
    }

    private void GetExecuteSkillLineTarget(Unit self, int target_x, int target_y, SkillData skill, ref List<Unit> targets, ref BattleCore.ShotTarget shot)
    {
      if (targets == null)
        targets = new List<Unit>(this.Enemys.Count);
      shot = (BattleCore.ShotTarget) null;
      this.mGridLines.Clear();
      BattleMap currentMap = this.CurrentMap;
      if ((self.x != target_x || self.y != target_y) && skill.CheckGridLineSkill())
      {
        if (skill.CheckUnitSkillTarget() && self.CastSkill != skill)
        {
          if (!skill.IsAreaSkill())
          {
            Unit target = this.FindUnitAtGrid(currentMap[target_x, target_y]);
            if (self == target)
              target = (Unit) null;
            if (target == null)
            {
              target = this.FindGimmickAtGrid(currentMap[target_x, target_y], false, (Unit) null);
              if (!this.IsTargetBreakUnit(self, target, skill))
                target = (Unit) null;
            }
            if (!this.CheckSkillTarget(self, target, skill))
              return;
          }
          else
          {
            this.CreateScopeGridMap(self, self.x, self.y, target_x, target_y, skill, ref this.mScopeMap, false);
            List<Unit> unitList = this.SearchTargetsInGridMap(self, skill, this.mScopeMap);
            if (unitList == null || unitList.Count == 0)
              return;
          }
        }
        this.GetSkillGridLines(self, target_x, target_y, skill, ref this.mGridLines);
        switch (skill.LineType)
        {
          case ELineType.Direct:
          case ELineType.Stab:
            if (shot == null)
              shot = new BattleCore.ShotTarget();
            shot.end = currentMap[target_x, target_y];
            for (int index = 0; index < this.mGridLines.Count; ++index)
            {
              Unit target = this.FindUnitAtGrid(this.mGridLines[index]);
              if (self == target)
                target = (Unit) null;
              if (target == null)
              {
                target = this.FindGimmickAtGrid(this.mGridLines[index], false, (Unit) null);
                if (target != null && target.IsBreakObj && (target.BreakObjClashType == eMapBreakClashType.INVINCIBLE && target.BreakObjRayType == eMapBreakRayType.TERRAIN))
                {
                  shot.end = this.mGridLines[index];
                  break;
                }
                if (!this.IsTargetBreakUnit(self, target, skill))
                  target = (Unit) null;
              }
              if (target != null && (!target.IsJump || skill.SkillParam.TargetEx != eSkillTargetEx.None))
              {
                shot.piercers.Add(target);
                targets.Add(target);
                if (!skill.IsPierce())
                {
                  shot.end = this.mGridLines[index];
                  break;
                }
              }
            }
            break;
          case ELineType.Curved:
            double num1 = (double) (self.GetAttackHeight() + 2);
            List<BattleCore.ShotTarget> l = new List<BattleCore.ShotTarget>();
            Grid grid = currentMap[self.x, self.y];
            Grid end = currentMap[target_x, target_y];
            double num2 = 0.0;
            if (num2 < (double) grid.height)
              num2 = (double) grid.height;
            for (int index = 0; index < this.mGridLines.Count; ++index)
            {
              if (num2 < (double) this.mGridLines[index].height)
                num2 = (double) this.mGridLines[index].height;
            }
            if (num2 <= (double) grid.height)
              ++num2;
            double num3 = num2 + 1.0;
            int num4 = end.x - grid.x;
            int num5 = end.y - grid.y;
            double val2 = Math.Sqrt((double) (num4 * num4 + num5 * num5));
            double num6 = 9.8;
            int num7 = 1;
            Unit unitAtGrid = this.FindUnitAtGrid(end);
            if (unitAtGrid != null && !unitAtGrid.IsNormalSize)
              num7 = (int) unitAtGrid.UnitParam.sd;
            for (int index1 = 0; index1 < num7; ++index1)
            {
              double num8 = (double) (grid.height - (end.height + index1 * 2));
              for (int index2 = 0; (double) index2 <= num1; ++index2)
              {
                double num9 = num3 + (double) index2;
                double d1 = 2.0 * num6 * (num9 - num8);
                double d2 = 2.0 * num6 * num9;
                double num10 = d1 <= 0.0 ? 0.0 : Math.Sqrt(d1);
                double num11 = d2 <= 0.0 ? 0.0 : Math.Sqrt(d2);
                double num12 = (num10 + num11) / num6;
                double d3 = Math.Pow(val2 / num12, 2.0) + 2.0 * num6 * (num9 - num8);
                double num13 = d3 <= 0.0 ? 0.0 : Math.Sqrt(d3);
                double a = Math.Atan(num12 * num10 / val2);
                double num14 = a * 180.0 / Math.PI;
                double num15 = num12 / val2;
                BattleCore.ShotTarget shotTarget = new BattleCore.ShotTarget();
                shotTarget.rad = num14;
                shotTarget.height = num9 + (double) index1;
                shotTarget.end = end;
                for (int index3 = 0; index3 < this.mGridLines.Count; ++index3)
                {
                  int num16 = this.mGridLines[index3].x - grid.x;
                  int num17 = this.mGridLines[index3].y - grid.y;
                  double num18 = Math.Min(Math.Sqrt((double) (num16 * num16 + num17 * num17)), val2);
                  double x = num15 * num18;
                  double num19 = Math.Sin(a);
                  double num20 = Math.Pow(x, 2.0);
                  double num21 = num6 * num20 * 0.5;
                  double num22 = num13 * x * num19 - num21;
                  double num23 = (double) (this.mGridLines[index3].height - grid.height) - 0.01;
                  if (num22 >= num23)
                  {
                    int num24 = BattleMap.MAP_FLOOR_HEIGHT / 2;
                    Unit target = this.FindUnitAtGrid(this.mGridLines[index3]);
                    if (target != null && !target.IsNormalSize)
                      num24 += BattleMap.MAP_FLOOR_HEIGHT * ((int) target.UnitParam.sd - 1);
                    double num25 = num23 + (double) num24;
                    if (num22 < num25)
                    {
                      if (self == target)
                        target = (Unit) null;
                      if (target == null)
                      {
                        target = this.FindGimmickAtGrid(this.mGridLines[index3], false, (Unit) null);
                        if (target != null && target.IsBreakObj && (target.BreakObjClashType == eMapBreakClashType.INVINCIBLE && target.BreakObjRayType == eMapBreakRayType.TERRAIN))
                        {
                          shotTarget.end = this.mGridLines[index3];
                          break;
                        }
                        if (!this.IsTargetBreakUnit(self, target, skill))
                          target = (Unit) null;
                      }
                      if (target != null && (!target.IsJump || skill.SkillParam.TargetEx != eSkillTargetEx.None))
                      {
                        shotTarget.piercers.Add(target);
                        if (!skill.IsPierce())
                        {
                          shotTarget.end = this.mGridLines[index3];
                          break;
                        }
                      }
                    }
                  }
                  else
                    break;
                }
                l.Add(shotTarget);
                if (shotTarget.end == end)
                  break;
              }
            }
            Unit target1 = this.FindUnitAtGrid(end);
            if (self == target1)
              target1 = (Unit) null;
            MySort<BattleCore.ShotTarget>.Sort(l, (Comparison<BattleCore.ShotTarget>) ((src, dst) => this.CalcGridDistance(src.end, end) - this.CalcGridDistance(dst.end, end)));
            if (target1 == null)
            {
              target1 = this.FindGimmickAtGrid(end, false, (Unit) null);
              if (!this.IsTargetBreakUnit(self, target1, skill))
                target1 = (Unit) null;
            }
            if (target1 != null && (!target1.IsJump || skill.SkillParam.TargetEx != eSkillTargetEx.None))
            {
              for (int index1 = 0; index1 < l.Count; ++index1)
              {
                if (l[index1].piercers.Contains(target1))
                {
                  for (int index2 = 0; index2 < l[index1].piercers.Count; ++index2)
                    targets.Add(l[index1].piercers[index2]);
                  shot = l[index1];
                  break;
                }
              }
            }
            if (shot == null && l.Count != 0)
            {
              shot = l[0];
              break;
            }
            break;
        }
        for (int index = 0; index < targets.Count; ++index)
        {
          Unit target2 = targets[index];
          if (target2.x == target_x && target2.y == target_y && !this.CheckSkillTarget(self, target2, skill))
          {
            targets.RemoveAt(index);
            --index;
          }
        }
      }
      else
      {
        Unit target = this.FindUnitAtGrid(currentMap[target_x, target_y]);
        if (target == null)
        {
          target = this.FindGimmickAtGrid(currentMap[target_x, target_y], false, (Unit) null);
          if (!this.IsTargetBreakUnit(self, target, skill))
            target = (Unit) null;
        }
        if (target != null && this.CheckSkillTarget(self, target, skill) && skill.SkillParam.select_scope != ESelectType.SquareOutline)
          targets.Add(target);
      }
      if (!skill.IsAreaSkill())
        return;
      Grid grid1 = shot != null ? shot.end : currentMap[target_x, target_y];
      this.CreateScopeGridMap(self, self.x, self.y, grid1.x, grid1.y, skill, ref this.mScopeMap, false);
      List<Unit> unitList1 = this.SearchTargetsInGridMap(self, skill, this.mScopeMap);
      for (int index = 0; index < unitList1.Count; ++index)
      {
        if (!targets.Contains(unitList1[index]))
          targets.Add(unitList1[index]);
      }
    }

    public IntVector2 GetBigUnitOffsetPos(Unit unit, int idx)
    {
      IntVector2 intVector2 = new IntVector2(0, 0);
      if (unit == null || unit.IsNormalSize || (idx < 0 || idx >= unit.SizeX * unit.SizeY) || (unit.SizeX != unit.SizeY || unit.SizeX > 5))
        return intVector2;
      return BattleCore.mBigUnitOfsPos5x5[idx];
    }

    public IntVector2 GetValidGridForSkillRange(Unit unit, int sx, int sy, SkillData skill, int gx, int gy)
    {
      IntVector2 intVector2 = new IntVector2(gx, gy);
      Unit unitAtGrid = this.FindUnitAtGrid(gx, gy);
      if (unit == null || skill == null || (unitAtGrid == null || unitAtGrid.IsNormalSize))
        return intVector2;
      if (skill.IsAreaSkill())
      {
        this.CreateScopeGridMap(unit, sx, sy, gx, gy, skill, ref this.mScopeMap, false);
        if (this.SearchTargetsInGridMap(unit, skill, this.mScopeMap).Contains(unitAtGrid))
          return intVector2;
      }
      GridMap<bool> selectGridMap = this.CreateSelectGridMap(unit, sx, sy, skill);
      if (selectGridMap.isValid(intVector2.x, intVector2.y) && selectGridMap.get(intVector2.x, intVector2.y))
        return intVector2;
      for (int idx = 0; idx < unitAtGrid.SizeX * unitAtGrid.SizeY; ++idx)
      {
        IntVector2 bigUnitOffsetPos = this.GetBigUnitOffsetPos(unitAtGrid, idx);
        int x = unitAtGrid.x + bigUnitOffsetPos.x;
        int y = unitAtGrid.y + bigUnitOffsetPos.y;
        if (selectGridMap.isValid(x, y) && selectGridMap.get(x, y))
        {
          intVector2.x = x;
          intVector2.y = y;
          break;
        }
      }
      return intVector2;
    }

    public BattleCore.CommandResult GetCommandResult(Unit self, int x, int y, int tx, int ty, SkillData skill)
    {
      this.SetBattleFlag(EBattleFlag.PredictResult, true);
      BattleCore.CommandResult commandResult = new BattleCore.CommandResult();
      commandResult.self = self;
      commandResult.skill = skill;
      this.mRandDamage.Seed(this.mSeedDamage);
      this.CurrentRand = this.mRandDamage;
      int x1 = self.x;
      int y1 = self.y;
      EUnitDirection direction = self.Direction;
      self.x = x;
      self.y = y;
      if (tx != x || ty != y)
        self.Direction = BattleCore.UnitDirectionFromVector(self, tx - x, ty - y);
      int gx = tx;
      int gy = ty;
      switch (skill.TeleportType)
      {
        case eTeleportType.BeforeSkill:
          if (skill.IsTargetGridNoUnit)
          {
            self.x = tx;
            self.y = ty;
            break;
          }
          break;
        case eTeleportType.AfterSkill:
          gx = self.x;
          gy = self.y;
          break;
      }
      if (!this.CheckEnableUseSkill(self, skill, false) || !this.IsUseSkillCollabo(self, skill))
      {
        this.DebugErr("スキル使用条件を満たせなかった");
      }
      else
      {
        List<Unit> targets = (List<Unit>) null;
        BattleCore.ShotTarget shot = (BattleCore.ShotTarget) null;
        IntVector2 gridForSkillRange = this.GetValidGridForSkillRange(self, self.x, self.y, skill, gx, gy);
        this.GetExecuteSkillLineTarget(self, gridForSkillRange.x, gridForSkillRange.y, skill, ref targets, ref shot);
        if (targets != null && targets.Count > 0)
        {
          LogSkill log = new LogSkill();
          log.self = self;
          log.skill = skill;
          log.pos.x = tx;
          log.pos.y = ty;
          log.reflect = (LogSkill.Reflection) null;
          log.is_append = !skill.IsCutin();
          for (int index = 0; index < targets.Count; ++index)
            log.SetSkillTarget(self, targets[index]);
          if (shot != null)
          {
            log.pos.x = shot.end.x;
            log.pos.y = shot.end.y;
            log.rad = (int) (shot.rad * 100.0);
            log.height = (int) (shot.height * 100.0);
          }
          List<LogSkill> results = new List<LogSkill>();
          commandResult.targets = new List<BattleCore.UnitResult>(log.targets.Count);
          commandResult.reactions = new List<BattleCore.UnitResult>(log.targets.Count);
          this.SetBattleFlag(EBattleFlag.IsNotClearPrevBuff, true);
          this.ExecuteFirstReactionSkill(self, targets, skill, tx, ty, results);
          log.CheckAliveTarget();
          this.ExecuteSkill(ESkillTiming.Used, log, skill);
          this.ExecuteReactionSkill(log, results);
          this.SetBattleFlag(EBattleFlag.IsNotClearPrevBuff, false);
          for (int index = 0; index < this.AllUnits.Count; ++index)
          {
            Unit allUnit = this.AllUnits[index];
            if (allUnit.RemoveBuffPrevApply())
              allUnit.CalcCurrentStatus(false, false);
          }
          for (int index = 0; index < log.targets.Count; ++index)
          {
            Unit target = log.targets[index].target;
            BattleCore.UnitResult unitResult = new BattleCore.UnitResult();
            unitResult.unit = target;
            unitResult.hp_damage += log.targets[index].GetTotalHpDamage();
            unitResult.hp_heal += log.targets[index].GetTotalHpHeal();
            unitResult.mp_damage += log.targets[index].GetTotalMpDamage();
            unitResult.mp_heal += log.targets[index].GetTotalMpHeal();
            unitResult.critical = log.targets[index].GetTotalCriticalRate();
            unitResult.avoid = log.targets[index].GetTotalAvoidRate();
            unitResult.cond_hit_lists.Clear();
            foreach (LogSkill.Target.CondHit condHitList in log.targets[index].CondHitLists)
              unitResult.cond_hit_lists.Add(new LogSkill.Target.CondHit(condHitList.Cond, condHitList.Per));
            commandResult.targets.Add(unitResult);
          }
          if (log.self_effect != null)
          {
            commandResult.self_effect = new BattleCore.UnitResult();
            commandResult.self_effect.unit = log.self_effect.target;
            commandResult.self_effect.hp_damage += log.self_effect.GetTotalHpDamage();
            commandResult.self_effect.hp_heal += log.self_effect.GetTotalHpHeal();
            commandResult.self_effect.mp_damage += log.self_effect.GetTotalMpDamage();
            commandResult.self_effect.mp_heal += log.self_effect.GetTotalMpHeal();
          }
          for (int index1 = 0; index1 < results.Count; ++index1)
          {
            for (int index2 = 0; index2 < results[index1].targets.Count; ++index2)
            {
              LogSkill.Target target1 = results[index1].targets[index2];
              Unit target2 = target1.target;
              BattleCore.UnitResult unitResult = new BattleCore.UnitResult();
              unitResult.react_unit = results[index1].self;
              unitResult.unit = target2;
              unitResult.hp_damage += target1.GetTotalHpDamage();
              unitResult.hp_heal += target1.GetTotalHpHeal();
              unitResult.mp_damage += target1.GetTotalMpDamage();
              unitResult.mp_heal += target1.GetTotalMpHeal();
              unitResult.critical = target1.GetTotalCriticalRate();
              unitResult.avoid = target1.GetTotalAvoidRate();
              unitResult.reaction = (int) results[index1].skill.EffectRate <= 0 || (int) results[index1].skill.EffectRate >= 100 ? 100 : (int) results[index1].skill.EffectRate;
              unitResult.cond_hit_lists.Clear();
              foreach (LogSkill.Target.CondHit condHitList in target1.CondHitLists)
                unitResult.cond_hit_lists.Add(new LogSkill.Target.CondHit(condHitList.Cond, condHitList.Per));
              commandResult.reactions.Add(unitResult);
            }
          }
        }
      }
      self.x = x1;
      self.y = y1;
      self.Direction = direction;
      this.CurrentRand = this.mRand;
      self.SetUnitFlag(EUnitFlag.SideAttack, false);
      self.SetUnitFlag(EUnitFlag.BackAttack, false);
      this.SetBattleFlag(EBattleFlag.PredictResult, false);
      return commandResult;
    }

    public bool UseSkill(Unit self, int gx, int gy, SkillData skill, bool bUnitLockTarget = false, int ux = 0, int uy = 0, bool is_call_auto_skill = false)
    {
      DebugUtility.Assert(self != null, "self == null");
      DebugUtility.Assert(skill != null, "skill == null");
      Unit unit1 = (Unit) null;
      if (skill.EffectType == SkillEffectTypes.Throw)
      {
        unit1 = this.FindUnitAtGrid(ux, uy);
        if (unit1 == null)
        {
          unit1 = this.FindGimmickAtGrid(ux, uy, false);
          if (unit1 == null || !unit1.IsBreakObj)
            return false;
        }
      }
      if (skill.IsCastSkill() && self.CastSkill != skill)
      {
        if (!this.CheckEnableUseSkill(self, skill, false) || !this.IsUseSkillCollabo(self, skill))
        {
          this.DebugErr("スキル使用条件を満たせなかった");
          return false;
        }
        this.CastStart(self, gx, gy, skill, bUnitLockTarget);
        return true;
      }
      if (self.Side == EUnitSide.Player)
      {
        if (skill.SkillType == ESkillType.Item)
          ++this.mNumUsedItems;
        else if (skill.SkillType == ESkillType.Skill && skill.Timing != ESkillTiming.Auto)
          ++this.mNumUsedSkills;
      }
      this.mRandDamage.Seed(this.mSeedDamage);
      this.CurrentRand = this.mRandDamage;
      if ((gx != self.x || gy != self.y) && !skill.IsCastSkill())
      {
        if (skill.SkillParam.select_scope == ESelectType.LaserTwin)
        {
          Grid current1 = this.CurrentMap[self.x, self.y];
          Grid current2 = this.CurrentMap[gx, gy];
          self.Direction = this.UnitDirectionFromGridLaserTwin(current1, current2);
        }
        else
          self.Direction = BattleCore.UnitDirectionFromVector(self, gx - self.x, gy - self.y);
      }
      int effectRate = (int) skill.EffectRate;
      if (effectRate > 0 && effectRate < 100 && (int) (this.GetRandom() % 100U) > effectRate)
      {
        self.SetUnitFlag(EUnitFlag.Action, true);
        self.SetCommandFlag(EUnitCommandFlag.Action, true);
        if (skill.Timing != ESkillTiming.Auto)
          this.Log<LogMapCommand>();
        return true;
      }
      int skillUsedCost = self.GetSkillUsedCost(skill);
      int gx1 = gx;
      int gy1 = gy;
      List<Unit> targets = (List<Unit>) null;
      BattleCore.ShotTarget shot = (BattleCore.ShotTarget) null;
      int x1 = self.x;
      int y1 = self.y;
      switch (skill.TeleportType)
      {
        case eTeleportType.BeforeSkill:
          if (skill.IsTargetGridNoUnit)
          {
            self.x = gx1;
            self.y = gy1;
            Grid duplicatePosition = this.GetCorrectDuplicatePosition(self);
            if (duplicatePosition != null)
            {
              int num1 = gx1 = duplicatePosition.x;
              self.x = num1;
              gx = num1;
              int num2 = gy1 = duplicatePosition.y;
              self.y = num2;
              gy = num2;
              break;
            }
            break;
          }
          break;
        case eTeleportType.AfterSkill:
          gx1 = self.x;
          gy1 = self.y;
          break;
      }
      if (skill.EffectType == SkillEffectTypes.Throw)
      {
        targets = new List<Unit>(1);
        targets.Add(unit1);
      }
      else if (skill.IsSetBreakObjSkill())
      {
        targets = new List<Unit>(1);
      }
      else
      {
        IntVector2 gridForSkillRange = this.GetValidGridForSkillRange(self, self.x, self.y, skill, gx1, gy1);
        this.GetExecuteSkillLineTarget(self, gridForSkillRange.x, gridForSkillRange.y, skill, ref targets, ref shot);
      }
      self.x = x1;
      self.y = y1;
      int x2 = self.x;
      int y2 = self.y;
      this.ExecuteFirstReactionSkill(self, targets, skill, gx, gy, (List<LogSkill>) null);
      int count = targets.Count;
      if (self.x != x2 || self.y != y2)
      {
        this.CreateSelectGridMap(self, self.x, self.y, skill, ref this.mRangeMap, false);
        if (skill.IsAreaSkill())
        {
          if (!this.mRangeMap.get(gx, gy))
            targets.Clear();
        }
        else
        {
          List<Unit> unitList = new List<Unit>(targets.Count);
          foreach (Unit unit2 in targets)
          {
            if (this.mRangeMap.get(unit2.x, unit2.y))
              unitList.Add(unit2);
          }
          if (targets.Count != unitList.Count)
            targets = unitList;
        }
      }
      bool flag1 = count == 0 || targets.Count != 0;
      for (int index1 = 0; index1 < targets.Count; ++index1)
      {
        Unit unit2 = targets[index1];
        if (unit2.IsDead && unit2.IsUnitFlag(EUnitFlag.UnitTransformed))
        {
          for (int index2 = 0; index2 < this.mUnits.Count; ++index2)
          {
            Unit mUnit = this.mUnits[index2];
            if (!mUnit.IsDead && mUnit.IsUnitFlag(EUnitFlag.IsDynamicTransform) && mUnit.DtuFromUnit == unit2)
            {
              targets[index1] = mUnit;
              break;
            }
          }
        }
      }
      bool flag2 = !self.IsDead;
      if (skill.IsCastSkill() && self.CastSkill != skill)
        flag2 = false;
      if (flag2)
      {
        LogSkill log = (LogSkill) null;
        if (flag1)
        {
          log = this.Log<LogSkill>();
          log.self = self;
          log.skill = skill;
          log.pos.x = gx;
          log.pos.y = gy;
          log.is_append = !skill.IsCutin();
          if (shot != null)
          {
            log.pos.x = shot.end.x;
            log.pos.y = shot.end.y;
            log.rad = (int) (shot.rad * 100.0);
            log.height = (int) (shot.height * 100.0);
          }
          for (int index = 0; index < targets.Count; ++index)
            log.SetSkillTarget(self, targets[index]);
          if (skill.IsDamagedSkill())
          {
            for (int index = 0; index < targets.Count; ++index)
            {
              Unit guardMan = this.GetGuardMan(targets[index]);
              if (guardMan != null && guardMan != self && !targets.Contains(guardMan))
              {
                LogSkill.Target target = log.FindTarget(targets[index]);
                target.guard = targets[index];
                target.target = guardMan;
                targets[index] = guardMan;
              }
            }
          }
        }
        if (!this.IsBattleFlag(EBattleFlag.PredictResult))
        {
          Unit unit2 = (Unit) null;
          if ((bool) skill.IsCollabo)
            unit2 = self.GetUnitUseCollaboSkill(skill, false);
          if (skill.EffectType != SkillEffectTypes.GemsGift)
          {
            this.SubGems(self, skillUsedCost);
            if (unit2 != null)
              this.SubGems(unit2, unit2.GetSkillUsedCost(skill));
          }
          self.UpdateSkillUseCount(skill, -1);
          if (unit2 != null)
          {
            SkillData skillForUseCount = unit2.GetSkillForUseCount(skill.SkillParam.iname, 0);
            if (skillForUseCount != null)
              unit2.UpdateSkillUseCount(skillForUseCount, -1);
          }
        }
        if (flag1)
        {
          log?.CheckAliveTarget();
          this.ExecuteSkill(skill.Timing != ESkillTiming.Auto ? ESkillTiming.Used : ESkillTiming.Auto, log, skill);
          if (skill.Timing == ESkillTiming.Auto && (string.IsNullOrEmpty(skill.SkillParam.motion) || string.IsNullOrEmpty(skill.SkillParam.effect)))
          {
            BattleLog last;
            do
            {
              last = this.mLogs.Last;
              if (last != null)
                this.mLogs.RemoveLogLast();
              else
                break;
            }
            while (!(last is LogSkill));
            log = (LogSkill) null;
          }
        }
        self.CancelCastSkill();
        if (flag1)
        {
          this.ExecuteReactionSkill(log, (List<LogSkill>) null);
          if (!this.IsBattleFlag(EBattleFlag.PredictResult) && log != null)
          {
            this.AddSkillExecLogForQuestMission(log);
            for (int index = 0; index < log.targets.Count; ++index)
            {
              if (log.targets[index].target.Side == EUnitSide.Enemy && log.targets[index].target.IsDead)
              {
                this.TrySetBattleFinisher(self);
                break;
              }
            }
          }
        }
      }
      self.SetUnitFlag(EUnitFlag.Action, true);
      self.SetCommandFlag(EUnitCommandFlag.Action, true);
      if (skill.TeleportType != eTeleportType.None && !skill.TeleportIsMove)
      {
        self.SetUnitFlag(EUnitFlag.Escaped, false);
        self.SetUnitFlag(EUnitFlag.Moved, true);
        self.SetCommandFlag(EUnitCommandFlag.Move, true);
      }
      this.CurrentRand = this.mRand;
      if (skill.Timing != ESkillTiming.Auto && this.GetQuestResult() != BattleCore.QuestResult.Pending)
      {
        this.ExecuteEventTriggerOnGrid(self, EEventTrigger.Stop);
        this.CalcQuestRecord();
        this.MapEnd();
      }
      else
      {
        bool flag3 = skill.IsCastSkill();
        if (!is_call_auto_skill)
        {
          BattleCore.OrderData currentOrderData = this.CurrentOrderData;
          if (currentOrderData != null)
            flag3 = currentOrderData.IsCastSkill;
        }
        if (flag3)
        {
          this.Log<LogCastSkillEnd>();
          if (skill.TeleportType != eTeleportType.None)
          {
            this.TrickActionEndEffect(self, true);
            this.ExecuteEventTriggerOnGrid(self, EEventTrigger.Stop);
          }
        }
        else
        {
          bool flag4 = false;
          if (self == this.CurrentUnit)
            flag4 = self.IsDead || (!self.IsUnitFlag(EUnitFlag.Moved) ? !self.IsEnableMoveCondition(false) && !self.IsEnableSelectDirectionCondition() : !self.IsEnableSelectDirectionCondition());
          if (flag4)
            this.InternalLogUnitEnd();
          else if (skill.Timing != ESkillTiming.Auto)
            this.Log<LogMapCommand>();
        }
      }
      return true;
    }

    private void ExecuteSkill(ESkillTiming timing, LogSkill log, SkillData skill)
    {
      if (timing != skill.Timing)
        return;
      Unit self1 = log.self;
      if (!this.CheckSkillCondition(self1, skill))
        return;
      bool flag1 = self1.IsDying();
      if (log.targets != null)
      {
        foreach (LogSkill.Target target in log.targets)
          target.IsOldDying = target.target.IsDying();
      }
      this.AbsorbAndGiveClear();
      if (!this.IsBattleFlag(EBattleFlag.ComputeAI) && skill.IsPrevApply())
      {
        for (int index = 0; index < log.targets.Count; ++index)
        {
          Unit target = log.targets[index].target;
          this.BuffSkill(timing, self1, target, skill, false, log, SkillEffectTargets.Target, false, (BuffEffect[]) null, (List<Unit>) null);
          target.CalcCurrentStatus(false, this.IsBattleFlag(EBattleFlag.PredictResult));
        }
        this.BuffSkill(timing, self1, self1, skill, false, log, SkillEffectTargets.Self, false, (BuffEffect[]) null, (List<Unit>) null);
        self1.CalcCurrentStatus(false, this.IsBattleFlag(EBattleFlag.PredictResult));
        this.AbsorbAndGiveApply(self1, skill, log);
      }
      List<Unit> unitList = new List<Unit>(log.targets.Count);
      List<bool> boolList = new List<bool>();
      for (int index = 0; index < log.targets.Count; ++index)
        boolList.Add(log.targets[index].target.IsJump);
      if (skill.IsChangeWeatherSkill())
      {
        if (this.IsBattleFlag(EBattleFlag.PredictResult) || skill.WeatherRate <= 0 || string.IsNullOrEmpty(skill.WeatherId))
          return;
        BattleLog last;
        do
        {
          last = this.mLogs.Last;
          if (last != null)
            this.mLogs.RemoveLogLast();
          else
            break;
        }
        while (!(last is LogSkill));
        this.ChangeWeatherForSkill(self1, skill);
      }
      else
      {
        if (skill.IsTransformSkill())
        {
          if (!this.IsBattleFlag(EBattleFlag.PredictResult) && log.targets != null && log.targets.Count > 0)
          {
            if (skill.IsDynamicTransformSkill())
            {
              Unit self2 = this.DtuTransformForSkill(self1, skill);
              if (self2 != null)
              {
                log.targets[0].target = self2;
                this.TrickActionEndEffect(self2, true);
                this.ExecuteEventTriggerOnGrid(self2, EEventTrigger.Stop);
              }
            }
            else
            {
              Unit target = log.targets[0].target;
              target.x = self1.x;
              target.y = self1.y;
              target.Direction = self1.Direction;
              if ((int) self1.CurrentStatus.param.hp != 0)
                self1.KeepHp = self1.CurrentStatus.param.hp;
              if (skill.EffectType == SkillEffectTypes.TransformUnitTakeOverHP && (int) self1.KeepHp != 0)
                target.CurrentStatus.param.hp = self1.KeepHp;
              self1.ForceDead();
              target.SetUnitFlag(EUnitFlag.Entried, true);
              target.SetUnitFlag(EUnitFlag.Reinforcement, true);
            }
          }
        }
        else if (skill.IsTrickSkill())
        {
          if (!this.IsBattleFlag(EBattleFlag.PredictResult))
          {
            int x = log.pos.x;
            int y = log.pos.y;
            this.TrickCreateForSkill(self1, x, y, skill);
          }
        }
        else if (skill.IsSetBreakObjSkill())
        {
          if (!this.IsBattleFlag(EBattleFlag.PredictResult))
          {
            int x = log.pos.x;
            int y = log.pos.y;
            this.BreakObjCreate(skill.SkillParam.BreakObjId, x, y, self1, log, self1.OwnerPlayerIndex);
          }
        }
        else if (skill.IsTargetGridNoUnit && (skill.TeleportType == eTeleportType.None || skill.TeleportType == eTeleportType.Only))
        {
          if (!this.IsBattleFlag(EBattleFlag.PredictResult))
          {
            switch (skill.EffectType)
            {
              case SkillEffectTypes.Teleport:
                self1.x = log.pos.x;
                self1.y = log.pos.y;
                break;
              case SkillEffectTypes.Throw:
                if (log.targets != null && log.targets.Count > 0)
                {
                  Unit target = log.targets[0].target;
                  target.x = log.pos.x;
                  target.y = log.pos.y;
                  unitList.Add(target);
                  break;
                }
                break;
            }
            if (skill.TeleportType == eTeleportType.Only)
            {
              self1.x = log.pos.x;
              self1.y = log.pos.y;
              Grid duplicatePosition = this.GetCorrectDuplicatePosition(self1);
              if (duplicatePosition != null)
              {
                self1.x = duplicatePosition.x;
                self1.y = duplicatePosition.y;
              }
              self1.startX = self1.x;
              self1.startY = self1.y;
              log.TeleportGrid = this.GetUnitGridPosition(self1.x, self1.y);
            }
          }
        }
        else
        {
          if (skill.TeleportType == eTeleportType.BeforeSkill || skill.TeleportType == eTeleportType.AfterSkill)
          {
            if (skill.IsTargetTeleport)
            {
              if (log.targets != null && log.targets.Count > 0)
              {
                Unit target = log.targets[0].target;
                bool is_teleport = false;
                Grid teleportGrid = this.GetTeleportGrid(self1, self1.x, self1.y, target, skill, ref is_teleport);
                if (teleportGrid != null)
                {
                  if (is_teleport)
                  {
                    self1.x = teleportGrid.x;
                    self1.y = teleportGrid.y;
                    Grid duplicatePosition = this.GetCorrectDuplicatePosition(self1);
                    if (duplicatePosition != null)
                    {
                      self1.x = duplicatePosition.x;
                      self1.y = duplicatePosition.y;
                    }
                    if (!this.IsBattleFlag(EBattleFlag.PredictResult))
                    {
                      self1.startX = self1.x;
                      self1.startY = self1.y;
                    }
                    log.TeleportGrid = this.GetUnitGridPosition(self1.x, self1.y);
                  }
                  else if (this.IsBattleFlag(EBattleFlag.PredictResult))
                  {
                    self1.x = teleportGrid.x;
                    self1.y = teleportGrid.y;
                  }
                }
              }
            }
            else
            {
              self1.x = log.pos.x;
              self1.y = log.pos.y;
              if (skill.TeleportType != eTeleportType.BeforeSkill)
              {
                Grid duplicatePosition = this.GetCorrectDuplicatePosition(self1);
                if (duplicatePosition != null)
                {
                  self1.x = duplicatePosition.x;
                  self1.y = duplicatePosition.y;
                }
              }
              if (!this.IsBattleFlag(EBattleFlag.PredictResult))
              {
                self1.startX = self1.x;
                self1.startY = self1.y;
              }
              log.TeleportGrid = this.GetUnitGridPosition(self1.x, self1.y);
            }
          }
          int num1 = Math.Max((int) skill.SkillParam.ComboNum, 1);
          for (int index1 = 0; index1 < log.targets.Count; ++index1)
          {
            Unit target = log.targets[index1].target;
            bool flag2 = true;
            SkillEffectTypes effectType = skill.EffectType;
            switch (effectType)
            {
              case SkillEffectTypes.GemsGift:
                int skillEffectValue = this.GetSkillEffectValue(self1, target, skill, log);
                int num2 = Math.Min(SkillParam.CalcSkillEffectValue(skill.EffectCalcType, skillEffectValue, self1.Gems) + skill.Cost, self1.Gems);
                log.Hit(self1, target, 0, 0, 0, 0, 0, num2, 0, 0, 0, false, false, false, false, 0, false, 0, 0);
                log.ToSelfSkillEffect(0, num2, 0, 0, 0, 0, 0, 0, 0, false, false, false, false);
                this.SubGems(self1, num2);
                this.AddGems(target, num2);
                break;
              case SkillEffectTypes.GemsIncDec:
                int num3 = this.GetSkillEffectValue(self1, target, skill, log);
                if (skill.EffectCalcType == SkillParamCalcTypes.Scale)
                  num3 = (int) target.MaximumStatus.param.mp * num3 / 100;
                if (num3 < 0)
                  log.Hit(self1, target, 0, Math.Abs(num3), 0, 0, 0, 0, 0, 0, 0, false, false, false, false, 0, false, 0, 0);
                else
                  log.Hit(self1, target, 0, 0, 0, 0, 0, num3, 0, 0, 0, false, false, false, false, 0, false, 0, 0);
                this.AddGems(target, num3);
                break;
              case SkillEffectTypes.Guard:
                if (!this.IsBattleFlag(EBattleFlag.PredictResult))
                {
                  self1.SetGuardTarget(target, 3);
                  break;
                }
                break;
              case SkillEffectTypes.Changing:
                if (!this.IsBattleFlag(EBattleFlag.PredictResult))
                {
                  int x = target.x;
                  int y = target.y;
                  target.x = self1.x;
                  target.y = self1.y;
                  self1.x = x;
                  self1.y = y;
                  self1.startX = x;
                  self1.startY = y;
                  target.startX = target.x;
                  target.startY = target.y;
                  if (!target.IsJump)
                  {
                    unitList.Add(target);
                    break;
                  }
                  break;
                }
                break;
              case SkillEffectTypes.RateHeal:
label_85:
                this.HealSkill(self1, target, skill, log);
                break;
              case SkillEffectTypes.RateDamage:
label_72:
                this.mIsDamageTakenCurrentSkill = false;
                for (int index2 = 0; index2 < num1; ++index2)
                {
                  if (!this.IsBattleFlag(EBattleFlag.ComputeAI))
                    target.CalcCurrentStatus(false, false);
                  this.DamageSkill(self1, target, skill, log);
                }
                if (!this.IsBattleFlag(EBattleFlag.PredictResult))
                {
                  int totalHpDamage = log.targets[index1].GetTotalHpDamage();
                  if (totalHpDamage > 0)
                  {
                    if ((self1.IsPartyMember || self1.Side == EUnitSide.Enemy) && target.Side == EUnitSide.Enemy)
                    {
                      this.mTotalDamages += totalHpDamage;
                      this.mMaxDamage = Math.Max(this.mMaxDamage, totalHpDamage);
                    }
                    if (target.IsPartyMember)
                      this.mTotalDamagesTaken += totalHpDamage;
                  }
                }
                if (this.mIsDamageTakenCurrentSkill)
                {
                  ++this.mTotalDamagesTakenCount;
                  break;
                }
                break;
              default:
                switch (effectType - 2)
                {
                  case SkillEffectTypes.None:
                    goto label_72;
                  case SkillEffectTypes.Attack:
                    goto label_85;
                  default:
                    if (effectType == SkillEffectTypes.ReflectDamage || effectType == SkillEffectTypes.RateDamageCurrent)
                      goto label_72;
                    else
                      break;
                }
            }
            if (flag2 && !log.targets[index1].IsAvoid() && target.CheckDamageActionStart())
              this.NotifyDamagedActionStart(self1, target);
          }
          if (this.isKnockBack(skill) && skill.IsDamagedSkill())
          {
            List<LogSkill.Target> ls_target_lists = new List<LogSkill.Target>(log.targets.Count);
            foreach (LogSkill.Target target in log.targets)
            {
              if ((target.GetTotalHpDamage() > 0 || target.GetTotalMpDamage() > 0) && this.checkKnockBack(self1, target.target, skill))
                ls_target_lists.Add(target);
            }
            this.procKnockBack(self1, skill, log.pos.x, log.pos.y, ls_target_lists);
            if (!this.IsBattleFlag(EBattleFlag.PredictResult))
            {
              foreach (LogSkill.Target target in ls_target_lists)
              {
                if (target.KnockBackGrid != null)
                  unitList.Add(target.target);
              }
            }
          }
          int gainJewel = log.GetGainJewel();
          if (gainJewel > 0)
            this.AddGems(self1, gainJewel);
          for (int index1 = 0; index1 < log.targets.Count; ++index1)
          {
            Unit target = log.targets[index1].target;
            if (!target.IsDead)
            {
              this.CondSkillSetRateLog(timing, self1, target, skill, log);
              if (!skill.IsDamagedSkill() || !log.targets[index1].IsAvoid())
              {
                SkillEffectTypes effectType = skill.EffectType;
                switch (effectType)
                {
                  case SkillEffectTypes.Attack:
                    if (log.targets[index1].GetTotalHpDamage() > 0 || log.targets[index1].shieldDamage > 0)
                    {
                      this.DamageCureCondition(target, log);
                      break;
                    }
                    break;
                  case SkillEffectTypes.Heal:
                    if (log.targets[index1].GetTotalHpHeal() > 0)
                    {
                      this.HealCureCondition(target, log);
                      break;
                    }
                    break;
                  default:
                    if (effectType != SkillEffectTypes.RateHeal)
                    {
                      if (effectType == SkillEffectTypes.RateDamage || effectType == SkillEffectTypes.RateDamageCurrent)
                        goto case SkillEffectTypes.Attack;
                      else
                        break;
                    }
                    else
                      goto case SkillEffectTypes.Heal;
                }
                if (!skill.IsPrevApply())
                  this.BuffSkill(timing, self1, target, skill, false, log, SkillEffectTargets.Target, false, (BuffEffect[]) null, (List<Unit>) null);
                this.CondSkill(timing, self1, target, skill, false, log, SkillEffectTargets.Target, false, (List<Unit>) null);
                if (skill.IsNormalAttack())
                {
                  JobData job = self1.Job;
                  if (job != null && (job.ArtifactDatas != null || !string.IsNullOrEmpty(job.SelectedSkin)))
                  {
                    List<ArtifactData> artifactDataList = new List<ArtifactData>();
                    if (job.ArtifactDatas != null && job.ArtifactDatas.Length >= 1)
                      artifactDataList.AddRange((IEnumerable<ArtifactData>) job.ArtifactDatas);
                    if (!string.IsNullOrEmpty(job.SelectedSkin))
                    {
                      ArtifactData selectedSkinData = job.GetSelectedSkinData();
                      if (selectedSkinData != null)
                        artifactDataList.Add(selectedSkinData);
                    }
                    for (int index2 = 0; index2 < artifactDataList.Count; ++index2)
                    {
                      ArtifactData artifactData = artifactDataList[index2];
                      if (artifactData != null && artifactData.ArtifactParam != null && (artifactData.ArtifactParam.type == ArtifactTypes.Arms && artifactData.BattleEffectSkill != null) && artifactData.BattleEffectSkill.SkillParam != null)
                      {
                        this.BuffSkill(timing, self1, target, artifactData.BattleEffectSkill, false, log, SkillEffectTargets.Target, false, (BuffEffect[]) null, (List<Unit>) null);
                        this.CondSkill(timing, self1, target, artifactData.BattleEffectSkill, false, log, SkillEffectTargets.Target, false, (List<Unit>) null);
                      }
                    }
                  }
                }
                this.StealSkill(self1, target, skill, log);
                this.ShieldSkill(target, skill);
                if (!this.IsBattleFlag(EBattleFlag.PredictResult))
                {
                  bool flag2 = false;
                  if (index1 < boolList.Count)
                    flag2 = boolList[index1];
                  if (flag2)
                  {
                    if (skill.SkillParam.IsJumpBreak() || target.IsJumpBreakCondition())
                    {
                      target.CancelCastSkill();
                      log.targets[index1].hitType |= LogSkill.EHitTypes.CastBreak;
                    }
                  }
                  else if (skill.IsCastBreak() && target.CastSkill != null)
                  {
                    target.CancelCastSkill();
                    log.targets[index1].hitType |= LogSkill.EHitTypes.CastBreak;
                  }
                  if ((int) skill.ControlChargeTimeRate != 0)
                  {
                    int num2 = 100;
                    EnchantParam enchantAssist = self1.CurrentStatus.enchant_assist;
                    EnchantParam enchantResist = target.CurrentStatus.enchant_resist;
                    if ((int) skill.ControlChargeTimeValue < 0)
                    {
                      if (target.IsDisableUnitCondition(EUnitCondition.DisableDecCT))
                        num2 = 0;
                      else
                        num2 += (int) enchantAssist[EnchantTypes.DecCT] - (int) enchantResist[EnchantTypes.DecCT];
                    }
                    else if ((int) skill.ControlChargeTimeValue > 0)
                    {
                      if (target.IsDisableUnitCondition(EUnitCondition.DisableIncCT))
                        num2 = 0;
                      else
                        num2 += (int) enchantAssist[EnchantTypes.IncCT] - (int) enchantResist[EnchantTypes.IncCT];
                    }
                    int num3 = (int) skill.ControlChargeTimeRate * num2 / 100;
                    if (num3 > 0)
                    {
                      bool flag3 = true;
                      if (num3 < 100 && (int) (this.GetRandom() % 100U) > num3)
                        flag3 = false;
                      if (flag3)
                      {
                        int chargeTime = (int) target.ChargeTime;
                        target.ChargeTime = (OInt) SkillParam.CalcSkillEffectValue(skill.ControlChargeTimeCalcType, (int) skill.ControlChargeTimeValue, (int) target.ChargeTime);
                        log.targets[index1].ChangeValueCT = (int) target.ChargeTime - chargeTime;
                      }
                    }
                  }
                }
              }
            }
          }
        }
        int hpCost = this.GetHpCost(self1, skill);
        if (hpCost > 0)
        {
          if (!this.IsBattleFlag(EBattleFlag.PredictResult))
          {
            self1.Damage(hpCost, false);
            this.GimmickEventHpLower(self1);
          }
          log.hp_cost = hpCost;
        }
        if (!self1.IsDead)
        {
          List<Unit> dsse_target_lists = (List<Unit>) null;
          if (log.targets.Count != 0)
          {
            dsse_target_lists = new List<Unit>(log.targets.Count);
            for (int index = 0; index < log.targets.Count; ++index)
            {
              Unit target = log.targets[index].target;
              if (target != null)
                dsse_target_lists.Add(target);
            }
          }
          if (!skill.IsPrevApply())
            this.BuffSkill(timing, self1, self1, skill, false, log, SkillEffectTargets.Self, false, (BuffEffect[]) null, dsse_target_lists);
          this.CondSkill(timing, self1, self1, skill, false, log, SkillEffectTargets.Self, false, dsse_target_lists);
          if (skill.IsNormalAttack())
          {
            JobData job = self1.Job;
            if (job != null && (job.ArtifactDatas != null || !string.IsNullOrEmpty(job.SelectedSkin)))
            {
              List<ArtifactData> artifactDataList = new List<ArtifactData>();
              if (job.ArtifactDatas != null && job.ArtifactDatas.Length >= 1)
                artifactDataList.AddRange((IEnumerable<ArtifactData>) job.ArtifactDatas);
              if (!string.IsNullOrEmpty(job.SelectedSkin))
              {
                ArtifactData selectedSkinData = job.GetSelectedSkinData();
                if (selectedSkinData != null)
                  artifactDataList.Add(selectedSkinData);
              }
              for (int index = 0; index < artifactDataList.Count; ++index)
              {
                ArtifactData artifactData = artifactDataList[index];
                if (artifactData != null && artifactData.ArtifactParam != null && (artifactData.ArtifactParam.type == ArtifactTypes.Arms && artifactData.BattleEffectSkill != null) && artifactData.BattleEffectSkill.SkillParam != null)
                {
                  this.BuffSkill(timing, self1, self1, artifactData.BattleEffectSkill, false, log, SkillEffectTargets.Self, false, (BuffEffect[]) null, (List<Unit>) null);
                  this.CondSkill(timing, self1, self1, artifactData.BattleEffectSkill, false, log, SkillEffectTargets.Self, false, (List<Unit>) null);
                }
              }
            }
          }
        }
        if (!this.IsBattleFlag(EBattleFlag.ComputeAI))
        {
          if (!this.IsBattleFlag(EBattleFlag.IsNotClearPrevBuff))
          {
            for (int index = 0; index < log.targets.Count; ++index)
            {
              Unit target = log.targets[index].target;
              if (target.RemoveBuffPrevApply())
                target.CalcCurrentStatus(false, false);
            }
            if (self1.RemoveBuffPrevApply())
              self1.CalcCurrentStatus(false, false);
          }
          if (!skill.IsPrevApply())
            this.AbsorbAndGiveApply(self1, skill, log);
        }
        if (!this.IsBattleFlag(EBattleFlag.PredictResult))
        {
          for (int index = 0; index < log.targets.Count; ++index)
            this.AbilityChange(self1, log.targets[index].target, skill);
          for (int index = 0; index < log.targets.Count; ++index)
            this.GridEventStart(self1, log.targets[index].target, EEventTrigger.HpDownBorder, (SkillParam) null);
          for (int index = 0; index < log.targets.Count; ++index)
          {
            Unit target = log.targets[index].target;
            bool flag2 = false;
            if (index < boolList.Count)
              flag2 = boolList[index];
            if (target.IsDead)
            {
              if (flag2)
                this.Log<LogFall>().Add(target, (Grid) null);
              this.Dead(self1, target, DeadTypes.Damage, false);
            }
            else if (flag2 && !target.IsJump)
            {
              LogFall logFall = this.Logs.Last as LogFall;
              if (logFall == null || !logFall.mIsPlayDamageMotion)
                logFall = this.Log<LogFall>();
              Grid duplicatePosition = this.GetCorrectDuplicatePosition(target);
              logFall.Add(target, duplicatePosition);
              if (!target.IsJumpBreakNoMotionCondition())
                logFall.mIsPlayDamageMotion = true;
              if (duplicatePosition != null)
              {
                target.x = duplicatePosition.x;
                target.y = duplicatePosition.y;
                unitList.Add(target);
              }
            }
          }
          if (self1.IsDead)
            this.Dead(self1, self1, DeadTypes.Damage, false);
          if (!self1.IsDead)
          {
            List<UnitData> ud_targets = new List<UnitData>();
            for (int index = 0; index < log.targets.Count; ++index)
              ud_targets.Add(log.targets[index].target.UnitData);
            this.JudgeInspSkill(self1, ud_targets, skill);
          }
          if (self1.CastSkill != null && self1.CastSkill == skill && self1.CastSkill.CastType == ECastTypes.Jump)
          {
            log.landing = this.GetCorrectDuplicatePosition(self1);
            if (log.landing != null)
            {
              self1.x = log.landing.x;
              self1.y = log.landing.y;
              unitList.Add(self1);
            }
          }
          if (skill.IsDamagedSkill())
          {
            self1.UpdateBuffEffectTurnCount(EffectCheckTimings.AttackEnd, self1);
            self1.UpdateCondEffectTurnCount(EffectCheckTimings.AttackEnd, self1);
            for (int index = 0; index < log.targets.Count; ++index)
            {
              log.targets[index].target.UpdateBuffEffectTurnCount(EffectCheckTimings.AttackEnd, self1);
              log.targets[index].target.UpdateCondEffectTurnCount(EffectCheckTimings.AttackEnd, self1);
              if (!log.targets[index].IsAvoid())
              {
                log.targets[index].target.UpdateBuffEffectTurnCount(EffectCheckTimings.DamageEnd, log.targets[index].target);
                log.targets[index].target.UpdateCondEffectTurnCount(EffectCheckTimings.DamageEnd, log.targets[index].target);
                if ((log.targets[index].hitType & LogSkill.EHitTypes.Guts) != (LogSkill.EHitTypes) 0)
                {
                  log.targets[index].target.UpdateBuffEffectTurnCount(EffectCheckTimings.GutsEnd, log.targets[index].target);
                  log.targets[index].target.UpdateCondEffectTurnCount(EffectCheckTimings.GutsEnd, log.targets[index].target);
                }
              }
            }
          }
          for (int index = 0; index < log.targets.Count; ++index)
          {
            log.targets[index].target.UpdateBuffEffectTurnCount(EffectCheckTimings.SkillEnd, self1);
            log.targets[index].target.UpdateCondEffectTurnCount(EffectCheckTimings.SkillEnd, self1);
          }
          self1.UpdateBuffEffectTurnCount(EffectCheckTimings.SkillEnd, self1);
          self1.UpdateCondEffectTurnCount(EffectCheckTimings.SkillEnd, self1);
          for (int index = 0; index < log.targets.Count; ++index)
          {
            LogSkill.Target target = log.targets[index];
            target.target.UpdateBuffEffects();
            target.target.UpdateCondEffects();
            target.target.CalcCurrentStatus(false, false);
            if (!target.IsOldDying && target.target.IsDying())
              target.target.SetUnitFlag(EUnitFlag.ToDying, true);
          }
          self1.UpdateBuffEffects();
          self1.UpdateCondEffects();
          self1.CalcCurrentStatus(false, false);
          if (!flag1 && self1.IsDying())
            self1.SetUnitFlag(EUnitFlag.ToDying, true);
          this.UpdateEntryTriggers(UnitEntryTypes.UsedSkill, self1, skill.SkillParam);
          foreach (Unit mUnit in this.mUnits)
            this.GridEventStart(self1, mUnit, EEventTrigger.WdUsedSkill, skill.SkillParam);
        }
        bool flag4 = false;
        foreach (Unit self2 in unitList)
        {
          if (!self2.IsDead)
          {
            if (self2.IsJump)
            {
              if (self2 == self1)
              {
                self2.PushCastSkill();
                this.TrickActionEndEffect(self2, true);
                self2.PopCastSkill();
              }
            }
            else
            {
              this.TrickActionEndEffect(self2, false);
              this.ExecuteEventTriggerOnGrid(self2, EEventTrigger.Stop);
              flag4 = true;
            }
          }
        }
        if (flag4)
          self1.RefleshMomentBuff(this.Units, false, -1, -1);
        if (skill.WeatherRate <= 0 || string.IsNullOrEmpty(skill.WeatherId) || this.IsBattleFlag(EBattleFlag.PredictResult))
          return;
        this.ChangeWeatherForSkill(self1, skill);
      }
    }

    private void JudgeInspSkill(Unit self, List<UnitData> ud_targets, SkillData skill)
    {
      ArtifactData artifact = (ArtifactData) null;
      JobData job = self.Job;
      if (job != null && job.ArtifactDatas != null)
      {
        for (int index = 0; index < job.ArtifactDatas.Length; ++index)
        {
          ArtifactData artifactData = job.ArtifactDatas[index];
          if (artifactData != null && artifactData.ArtifactParam != null && artifactData.ArtifactParam.type == ArtifactTypes.Arms)
          {
            artifact = artifactData;
            break;
          }
        }
      }
      if (artifact == null)
        return;
      int slot_no;
      if (self.GetInspSlotNo(artifact, out slot_no) && InspSkillParam.Check(self.UnitData, ud_targets, artifact, slot_no, skill) && self.ApplyInspIns(artifact))
        this.Log<LogInspiration>().self = self;
      if (skill.IsNormalAttack() || !InspSkillParam.IsCanLevelUp(self.UnitData, artifact, skill))
        return;
      self.EntryInspUse((long) artifact.UniqueID);
    }

    private void CastStart(Unit self, int gx, int gy, SkillData skill, bool bUnitLockTarget)
    {
      Unit unit = (Unit) null;
      Grid grid = this.CurrentMap[gx, gy];
      if (bUnitLockTarget)
      {
        unit = this.FindUnitAtGrid(grid);
        if (unit != null)
          grid = (Grid) null;
      }
      LogCast logCast = this.Log<LogCast>();
      logCast.self = self;
      logCast.type = skill.CastType;
      logCast.dx = gx;
      logCast.dy = gy;
      self.SetCastSkill(skill, unit, grid);
      self.SetUnitFlag(EUnitFlag.Action, true);
      self.SetCommandFlag(EUnitCommandFlag.Action, true);
      BattleMap currentMap = this.CurrentMap;
      GridMap<bool> result = new GridMap<bool>(currentMap.Width, currentMap.Height);
      if (skill.IsAreaSkill())
        this.CreateScopeGridMap(self, self.x, self.y, gx, gy, skill, ref result, false);
      else
        result.set(gx, gy, true);
      if (skill.TeleportType == eTeleportType.AfterSkill)
        result.set(gx, gy, false);
      self.CastSkillGridMap = result;
      this.Log<LogMapCommand>();
    }

    private bool IsCombinationAttack(SkillData skill)
    {
      if (skill != null && skill.IsNormalAttack())
        return this.mHelperUnits.Count > 0;
      return false;
    }

    private void GetYuragiDamage(ref int damage)
    {
      if (damage < 2)
        return;
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      if ((int) fixParam.RandomEffectMax == 0)
        return;
      int num = (int) (this.GetRandom() % 100U) % ((int) fixParam.RandomEffectMax * 2 + 1) - (int) fixParam.RandomEffectMax;
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      damage = Math.Max(damage + num, 0);
    }

    private Unit GetGuardMan(Unit self)
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (this.mUnits[index].GetGuardTarget() == self)
          return this.mUnits[index];
      }
      return (Unit) null;
    }

    private bool CheckSkillCondition(Unit self, SkillData skill)
    {
      if (self == null || skill == null)
        return false;
      switch (skill.Condition)
      {
        case ESkillCondition.Dying:
          return self.IsDying();
        case ESkillCondition.JudgeHP:
          return skill.IsJudgeHp(self);
        default:
          return true;
      }
    }

    private void DamageSkill(Unit self, Unit target, SkillData skill, LogSkill log)
    {
      bool is_guts = false;
      bool flag = false;
      bool bWeakPoint = false;
      bool is_pf_avoid = false;
      int damage = 0;
      int absorbed = 0;
      switch (skill.EffectType)
      {
        case SkillEffectTypes.Attack:
          if (!target.IsBreakObj)
          {
            if (this.CheckBackAttack(self, target, skill) && !target.IsDisableUnitCondition(EUnitCondition.DisableBackAttack))
              self.SetUnitFlag(EUnitFlag.BackAttack, true);
            else if (this.CheckSideAttack(self, target, skill) && !target.IsDisableUnitCondition(EUnitCondition.DisableSideAttack))
              self.SetUnitFlag(EUnitFlag.SideAttack, true);
          }
          damage = this.GetDamageSkill(self, target, skill, log);
          if (!skill.IsJewelAttack())
          {
            if (!this.IsCombinationAttack(skill) && (skill.IsNormalAttack() || (bool) skill.SkillParam.IsCritical))
            {
              flag = this.CheckCritical(self, target, skill);
              if (flag)
              {
                uint rand = this.mRandDamage.Get();
                if (!this.IsBattleFlag(EBattleFlag.PredictResult))
                  damage = this.GetCriticalDamage(self, damage, rand);
              }
            }
            this.GetYuragiDamage(ref damage);
            bWeakPoint = this.CheckWeakPoint(self, target, skill);
            break;
          }
          break;
        case SkillEffectTypes.ReflectDamage:
          if (log.reflect != null)
          {
            int skillEffectValue = this.GetSkillEffectValue(self, target, skill, log);
            damage = SkillParam.CalcSkillEffectValue(skill.EffectCalcType, skillEffectValue, log.reflect.damage);
            break;
          }
          break;
        case SkillEffectTypes.RateDamage:
          int skillEffectValue1 = this.GetSkillEffectValue(self, target, skill, log);
          damage = !skill.IsJewelAttack() ? (int) ((long) (int) target.MaximumStatus.param.hp * (long) skillEffectValue1 * 100L / 10000L) : (int) target.MaximumStatus.param.mp * skillEffectValue1 * 100 / 10000;
          break;
        case SkillEffectTypes.RateDamageCurrent:
          int skillEffectValue2 = this.GetSkillEffectValue(self, target, skill, log);
          damage = !skill.IsJewelAttack() ? (int) ((long) (int) target.CurrentStatus.param.hp * (long) skillEffectValue2 * 100L / 10000L) : (int) target.CurrentStatus.param.mp * skillEffectValue2 * 100 / 10000;
          break;
        default:
          return;
      }
      damage = damage * (int) skill.SkillParam.ComboDamageRate / 100;
      this.DamageControlSkill(self, target, skill, ref damage, log);
      int num1 = this.mQuestParam.DamageRatePl;
      int val2 = this.mQuestParam.DamageUpprPl;
      if (target.Side == EUnitSide.Enemy)
      {
        num1 = this.mQuestParam.DamageRateEn;
        val2 = this.mQuestParam.DamageUpprEn;
      }
      damage += damage * num1 / 100;
      if (val2 != 0)
        damage = Math.Min(damage, val2);
      if (skill.IsFixedDamage())
      {
        damage = (int) skill.EffectValue;
        if (log != null && log.targets != null && log.targets.Count > 1)
        {
          int hitTargetNumRate = (int) skill.SkillParam.EffectHitTargetNumRate;
          if (hitTargetNumRate != 0)
          {
            int num2 = hitTargetNumRate * (log.targets.Count - 1);
            damage += 100 * damage * num2 / 10000;
          }
        }
      }
      if (skill.SkillParam.IsHitTargetNumDiv() && log != null && (log.targets != null && log.targets.Count > 1))
        damage /= log.targets.Count;
      if (skill.MaxDamageValue != 0 && damage > skill.MaxDamageValue)
        damage = skill.MaxDamageValue;
      damage = Math.Max(damage, 1);
      bool is_avoid;
      if (this.CheckPerfectAvoidSkill(self, target, skill, log))
      {
        log?.FindTarget(target)?.SetPerfectAvoid(true);
        is_avoid = true;
        is_pf_avoid = true;
      }
      else
        is_avoid = this.CheckAvoid(self, target, skill);
      damage = this.GetResistDamageForMhmDamage(self, target, skill, damage);
      if ((!is_avoid || this.IsBattleFlag(EBattleFlag.PredictResult)) && (damage > 0 && skill.DamageAbsorbRate <= 0) && !skill.IsJewelAttack())
      {
        int num2 = damage;
        LogSkill.Target log_target = (LogSkill.Target) null;
        if (log != null)
          log_target = log.FindTarget(target);
        if (skill.IsPhysicalAttack())
        {
          target.CalcShieldDamage(DamageTypes.PhyDamage, ref damage, !this.IsBattleFlag(EBattleFlag.PredictResult), skill.AttackDetailType, this.CurrentRand, log_target);
          damage = Math.Max(damage * this.mQuestParam.PhysBonus / 100, 0);
        }
        if (skill.IsMagicalAttack())
        {
          target.CalcShieldDamage(DamageTypes.MagDamage, ref damage, !this.IsBattleFlag(EBattleFlag.PredictResult), skill.AttackDetailType, this.CurrentRand, log_target);
          damage = Math.Max(damage * this.mQuestParam.MagBonus / 100, 0);
        }
        target.CalcShieldDamage(DamageTypes.TotalDamage, ref damage, !this.IsBattleFlag(EBattleFlag.PredictResult), skill.AttackDetailType, this.CurrentRand, log_target);
        absorbed = num2 - damage;
      }
      int num3 = 0;
      int num4 = 0;
      int hp_heal = 0;
      int num5 = 0;
      int dropgems = 0;
      if (!skill.IsJewelAttack())
      {
        num3 = damage;
        if (skill.DamageAbsorbRate > 0 && !self.IsUnitCondition(EUnitCondition.DisableHeal))
        {
          int val = damage * skill.DamageAbsorbRate * 100 / 10000;
          hp_heal = self.CalcParamRecover(val);
        }
        if (!is_avoid)
        {
          switch (skill.SkillParam.JewelDamageType)
          {
            case JewelDamageTypes.Calc:
              num4 += BattleCore.Sqrt(damage) * 2;
              break;
            case JewelDamageTypes.Scale:
              num4 += (int) target.MaximumStatus.param.mp * (int) skill.SkillParam.JewelDamageValue / 100;
              break;
            case JewelDamageTypes.Fixed:
              num4 += (int) skill.SkillParam.JewelDamageValue;
              break;
          }
          if ((bool) skill.SkillParam.IsJewelAbsorb)
            num5 = num4;
        }
      }
      else
      {
        num4 = damage;
        if (skill.DamageAbsorbRate > 0)
          num5 = damage * skill.DamageAbsorbRate * 100 / 10000;
      }
      if (!this.IsBattleFlag(EBattleFlag.PredictResult))
      {
        if (!is_avoid)
        {
          if (self.IsPartyMember && hp_heal > 0 && target.Side == EUnitSide.Enemy)
            this.mTotalHeal += Math.Min((int) self.CurrentStatus.param.hp + hp_heal, (int) self.MaximumStatus.param.hp) - (int) self.CurrentStatus.param.hp;
          if (skill.IsMhmDamage())
          {
            if (num3 > 0)
            {
              target.AddMhmDamage(Unit.eTypeMhmDamage.HP, num3);
              target.ReflectMhmDamage(Unit.eTypeMhmDamage.HP, num3, true);
              target.Damage(0, false);
            }
            if (num4 > 0)
            {
              target.AddMhmDamage(Unit.eTypeMhmDamage.MP, num4);
              target.ReflectMhmDamage(Unit.eTypeMhmDamage.MP, num4, false);
            }
          }
          else
          {
            target.Damage(num3, false);
            this.SubGems(target, num4);
          }
          self.Heal(hp_heal);
          this.AddGems(self, num5);
          if (this.CheckGuts(target))
          {
            is_guts = true;
            target.Heal(1);
          }
          dropgems = this.CalcGainedGems(self, target, skill, damage, flag, bWeakPoint);
          if (target.IsPartyMember && !self.IsPartyMember)
            this.mIsDamageTakenCurrentSkill = true;
          this.GimmickEventDamageCount(self, target);
          this.GimmickEventHpLower(target);
        }
        else
        {
          num3 = 0;
          num4 = 0;
          hp_heal = 0;
          num5 = 0;
          dropgems = 0;
          flag = false;
        }
      }
      bool is_combination = this.IsCombinationAttack(skill);
      log.Hit(self, target, num3, num4, 0, 0, 0, 0, 0, 0, dropgems, flag, is_avoid, is_combination, is_guts, absorbed, is_pf_avoid, this.GetCriticalRate(self, target, skill), this.GetAvoidRate(self, target, skill));
      if (hp_heal != 0 || num5 != 0)
        log.ToSelfSkillEffect(0, 0, 0, 0, hp_heal, num5, 0, 0, 0, false, false, false, false);
      self.SetUnitFlag(EUnitFlag.BackAttack, false);
      self.SetUnitFlag(EUnitFlag.SideAttack, false);
    }

    private void NotifyDamagedActionStart(Unit attacker, Unit defender)
    {
      if (this.IsBattleFlag(EBattleFlag.PredictResult) || !this.CheckEnemySide(attacker, defender))
        return;
      if (defender.IsUnitFlag(EUnitFlag.DamagedActionStart))
      {
        defender.SetUnitFlag(EUnitFlag.DamagedActionStart, false);
        defender.SetUnitFlag(EUnitFlag.Searched, true);
      }
      if (defender.NotifyUniqueNames == null)
        return;
      for (int index1 = 0; index1 < defender.NotifyUniqueNames.Count; ++index1)
      {
        if ((string) defender.NotifyUniqueNames[index1] == "pall")
        {
          for (int index2 = 0; index2 < this.mPlayer.Count; ++index2)
          {
            this.mPlayer[index2].SetUnitFlag(EUnitFlag.DamagedActionStart, false);
            this.mPlayer[index2].SetUnitFlag(EUnitFlag.Searched, true);
          }
        }
        else if ((string) defender.NotifyUniqueNames[index1] == "eall")
        {
          for (int index2 = 0; index2 < this.Enemys.Count; ++index2)
          {
            this.Enemys[index2].SetUnitFlag(EUnitFlag.DamagedActionStart, false);
            this.Enemys[index2].SetUnitFlag(EUnitFlag.Searched, true);
          }
        }
        else if ((string) defender.NotifyUniqueNames[index1] == "nall")
        {
          foreach (Unit mUnit in this.mUnits)
          {
            if (mUnit.Side == EUnitSide.Neutral)
            {
              mUnit.SetUnitFlag(EUnitFlag.DamagedActionStart, false);
              mUnit.SetUnitFlag(EUnitFlag.Searched, true);
            }
          }
        }
        else
        {
          Unit unitByUniqueName = this.FindUnitByUniqueName((string) defender.NotifyUniqueNames[index1]);
          if (unitByUniqueName != null)
          {
            unitByUniqueName.SetUnitFlag(EUnitFlag.DamagedActionStart, false);
            unitByUniqueName.SetUnitFlag(EUnitFlag.Searched, true);
          }
        }
      }
    }

    private int GetHpCost(Unit self, SkillData skill)
    {
      if (skill.IsSuicide())
        return (int) self.MaximumStatus.param.hp;
      int hpCost = skill.GetHpCost(self);
      int hpCostRate = skill.HpCostRate;
      if (hpCostRate == 0)
        return 0;
      if (hpCostRate >= 100)
        return hpCost;
      if ((int) (this.GetRandom() % 100U) > hpCostRate || (int) self.CurrentStatus.param.hp <= 1)
        return 0;
      if ((int) self.CurrentStatus.param.hp > hpCost)
        return hpCost;
      return (int) self.CurrentStatus.param.hp - 1;
    }

    private void DefendSkill(Unit attacker, Unit defender, SkillData atkskl, LogSkill log)
    {
      if (!atkskl.IsDamagedSkill() && !defender.IsEnableReactionCondition() || this.IsObstReaction(attacker, defender, atkskl))
        return;
      StatusParam statusParam = defender.CurrentStatus.param;
      for (int index = 0; index < defender.BattleSkills.Count; ++index)
      {
        SkillData battleSkill = defender.BattleSkills[index];
        if (battleSkill != null && battleSkill.IsReactionSkill() && battleSkill.EffectType == SkillEffectTypes.Defend && ((battleSkill.Timing == ESkillTiming.Reaction || battleSkill.Timing == ESkillTiming.DamageCalculate) && (defender.IsEnableReactionSkill(battleSkill) && this.CheckSkillCondition(defender, battleSkill))))
        {
          int effectRate = (int) battleSkill.EffectRate;
          if ((effectRate <= 0 || effectRate >= 100 || (int) (this.GetRandom() % 100U) <= effectRate) && !this.IsBattleFlag(EBattleFlag.PredictResult))
          {
            int skillEffectValue = this.GetSkillEffectValue(defender, attacker, battleSkill, (LogSkill) null);
            if (skillEffectValue != 0)
            {
              bool flag = false;
              switch (battleSkill.ReactionDamageType)
              {
                case DamageTypes.TotalDamage:
                  if (atkskl.IsDamagedSkill() && battleSkill.IsReactionDet(atkskl.AttackDetailType))
                  {
                    flag = true;
                    statusParam.def = (OShort) SkillParam.CalcSkillEffectValue(battleSkill.EffectCalcType, skillEffectValue, (int) statusParam.def);
                    statusParam.mnd = (OShort) SkillParam.CalcSkillEffectValue(battleSkill.EffectCalcType, skillEffectValue, (int) statusParam.mnd);
                    break;
                  }
                  break;
                case DamageTypes.PhyDamage:
                  if (atkskl.IsPhysicalAttack() && battleSkill.IsReactionDet(atkskl.AttackDetailType))
                  {
                    flag = true;
                    statusParam.def = (OShort) SkillParam.CalcSkillEffectValue(battleSkill.EffectCalcType, skillEffectValue, (int) statusParam.def);
                    break;
                  }
                  break;
                case DamageTypes.MagDamage:
                  if (atkskl.IsMagicalAttack() && battleSkill.IsReactionDet(atkskl.AttackDetailType))
                  {
                    flag = true;
                    statusParam.mnd = (OShort) SkillParam.CalcSkillEffectValue(battleSkill.EffectCalcType, skillEffectValue, (int) statusParam.mnd);
                    break;
                  }
                  break;
              }
              if (log != null && flag)
              {
                LogSkill.Target target = log.FindTarget(defender);
                if (target != null)
                {
                  target.SetDefend(true);
                  target.defSkill = battleSkill;
                  target.defSkillUseCount = 0;
                  if ((int) battleSkill.SkillParam.count > 0)
                  {
                    defender.UpdateSkillUseCount(battleSkill, -1);
                    target.defSkillUseCount = (int) defender.GetSkillUseCount(battleSkill);
                  }
                }
              }
            }
          }
        }
      }
    }

    private bool CheckPerfectAvoidSkill(Unit attacker, Unit defender, SkillData atkskl, LogSkill log)
    {
      if (!atkskl.IsDamagedSkill() && !defender.IsEnableReactionCondition() || this.IsObstReaction(attacker, defender, atkskl))
        return false;
      for (int index = 0; index < defender.BattleSkills.Count; ++index)
      {
        SkillData battleSkill = defender.BattleSkills[index];
        if (battleSkill != null && battleSkill.IsReactionSkill() && battleSkill.EffectType == SkillEffectTypes.PerfectAvoid && ((battleSkill.Timing == ESkillTiming.Reaction || battleSkill.Timing == ESkillTiming.DamageCalculate) && (defender.IsEnableReactionSkill(battleSkill) && this.CheckSkillCondition(defender, battleSkill))))
        {
          int effectRate = (int) battleSkill.EffectRate;
          if ((effectRate <= 0 || effectRate >= 100 || (int) (this.GetRandom() % 100U) <= effectRate) && !this.IsBattleFlag(EBattleFlag.PredictResult))
          {
            bool flag = false;
            switch (battleSkill.ReactionDamageType)
            {
              case DamageTypes.TotalDamage:
                flag = atkskl.IsDamagedSkill() && battleSkill.IsReactionDet(atkskl.AttackDetailType);
                break;
              case DamageTypes.PhyDamage:
                flag = atkskl.IsPhysicalAttack() && battleSkill.IsReactionDet(atkskl.AttackDetailType);
                break;
              case DamageTypes.MagDamage:
                flag = atkskl.IsMagicalAttack() && battleSkill.IsReactionDet(atkskl.AttackDetailType);
                break;
            }
            if (flag)
            {
              if ((int) battleSkill.SkillParam.count > 0)
                defender.UpdateSkillUseCount(battleSkill, -1);
              return true;
            }
          }
        }
      }
      return false;
    }

    private void DamageControlSkill(Unit attacker, Unit defender, SkillData atkskl, ref int damage, LogSkill log)
    {
      if (!atkskl.IsDamagedSkill() && !defender.IsEnableReactionCondition() || this.IsObstReaction(attacker, defender, atkskl))
        return;
      for (int index = 0; index < defender.BattleSkills.Count; ++index)
      {
        SkillData battleSkill = defender.BattleSkills[index];
        if (battleSkill != null && battleSkill.IsReactionSkill() && (battleSkill.Timing == ESkillTiming.Reaction || battleSkill.Timing == ESkillTiming.DamageControl) && (defender.IsEnableReactionSkill(battleSkill) && this.CheckSkillCondition(defender, battleSkill)))
        {
          int effectRate = (int) battleSkill.EffectRate;
          if ((effectRate <= 0 || effectRate >= 100 || (int) (this.GetRandom() % 100U) <= effectRate) && (!this.IsBattleFlag(EBattleFlag.PredictResult) && (int) battleSkill.ControlDamageValue != 0))
          {
            bool flag = false;
            switch (battleSkill.ReactionDamageType)
            {
              case DamageTypes.TotalDamage:
                if (battleSkill.IsReactionDet(atkskl.AttackDetailType))
                {
                  damage = SkillParam.CalcSkillEffectValue(battleSkill.ControlDamageCalcType, (int) battleSkill.ControlDamageValue, damage);
                  flag = true;
                  break;
                }
                break;
              case DamageTypes.PhyDamage:
                if (atkskl.IsPhysicalAttack() && battleSkill.IsReactionDet(atkskl.AttackDetailType))
                {
                  damage = SkillParam.CalcSkillEffectValue(battleSkill.ControlDamageCalcType, (int) battleSkill.ControlDamageValue, damage);
                  flag = true;
                  break;
                }
                break;
              case DamageTypes.MagDamage:
                if (atkskl.IsMagicalAttack() && battleSkill.IsReactionDet(atkskl.AttackDetailType))
                {
                  damage = SkillParam.CalcSkillEffectValue(battleSkill.ControlDamageCalcType, (int) battleSkill.ControlDamageValue, damage);
                  flag = true;
                  break;
                }
                break;
            }
            if (log != null && flag)
            {
              LogSkill.Target target = log.FindTarget(defender);
              if (target != null)
              {
                target.SetDefend(true);
                target.defSkill = battleSkill;
                target.defSkillUseCount = 0;
                if ((int) battleSkill.SkillParam.count > 0)
                {
                  defender.UpdateSkillUseCount(battleSkill, -1);
                  target.defSkillUseCount = (int) defender.GetSkillUseCount(battleSkill);
                }
                if (battleSkill.Timing == ESkillTiming.Reaction)
                  target.SetForceReaction(true);
              }
            }
          }
        }
      }
    }

    private int GetResistDamageForMhmDamage(Unit attacker, Unit defender, SkillData skill, int damage)
    {
      int val1 = damage;
      if (defender == null || skill == null || !skill.IsMhmDamage())
        return val1;
      EnchantTypes index = EnchantTypes.MaxDamageHp;
      if (skill.IsJewelAttack())
        index = EnchantTypes.MaxDamageMp;
      EnchantParam enchantAssist = attacker.CurrentStatus.enchant_assist;
      EnchantParam enchantResist = defender.CurrentStatus.enchant_resist;
      int num = (int) enchantAssist[index] - (int) enchantResist[index];
      if (num != 0)
        val1 += damage * num / 100;
      return Math.Max(val1, 0);
    }

    private void HealSkill(Unit self, Unit target, SkillData skill, LogSkill log)
    {
      int hp_heal = 0;
      if (!target.IsUnitCondition(EUnitCondition.DisableHeal))
      {
        int val = this.CalcHeal(self, target, skill, log);
        hp_heal = self.CalcParamRecover(val);
      }
      log.Hit(self, target, 0, 0, 0, 0, hp_heal, 0, 0, 0, 0, false, false, false, false, 0, false, 0, 0);
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      if (target.IsPartyMember)
        this.mTotalHeal += Math.Min((int) target.CurrentStatus.param.hp + hp_heal, (int) target.MaximumStatus.param.hp) - (int) target.CurrentStatus.param.hp;
      this.Heal(target, hp_heal);
    }

    private void AutoHealSkill(Unit self)
    {
      if (!self.IsEnableAutoHealCondition())
        return;
      int autoHpHealValue = self.GetAutoHpHealValue();
      int num = self.CalcParamRecover(autoHpHealValue);
      if (num > 0)
      {
        LogAutoHeal logAutoHeal = this.Log<LogAutoHeal>();
        logAutoHeal.self = self;
        logAutoHeal.type = LogAutoHeal.HealType.Hp;
        logAutoHeal.value = num;
        logAutoHeal.beforeHp = (int) self.CurrentStatus.param.hp;
        self.Heal(num);
        if (self.IsPartyMember)
          this.mTotalHeal += Math.Max((int) self.CurrentStatus.param.hp - logAutoHeal.beforeHp, 0);
      }
      int autoMpHealValue = self.GetAutoMpHealValue();
      if (autoMpHealValue <= 0)
        return;
      LogAutoHeal logAutoHeal1 = this.Log<LogAutoHeal>();
      logAutoHeal1.self = self;
      logAutoHeal1.type = LogAutoHeal.HealType.Jewel;
      logAutoHeal1.value = autoMpHealValue;
      logAutoHeal1.beforeMp = (int) self.CurrentStatus.param.mp;
      this.AddGems(self, autoMpHealValue);
    }

    private void StealSkill(Unit self, Unit target, SkillData skill, LogSkill log)
    {
    }

    private void ShieldSkill(Unit target, SkillData skill)
    {
      if (target == null || skill == null || (skill.EffectType != SkillEffectTypes.Shield || skill.ShieldType == ShieldTypes.None))
        return;
      int effectRate = (int) skill.EffectRate;
      if (0 < effectRate && effectRate < 100 && (int) (this.GetRandom() % 100U) > effectRate || this.IsBattleFlag(EBattleFlag.PredictResult))
        return;
      target.AddShield(skill);
    }

    private bool IsObstReaction(Unit attacker, Unit defender, SkillData skill)
    {
      if (attacker == null || defender == null || skill == null)
        return true;
      if (defender.IsDisableUnitCondition(EUnitCondition.DisableObstReaction))
        return false;
      if (skill.SkillParam.IsObstReaction())
        return true;
      int num = (int) attacker.CurrentStatus.enchant_assist[EnchantTypes.ObstReaction] - (int) defender.CurrentStatus.enchant_resist[EnchantTypes.ObstReaction];
      return num >= 100 || num > 0 && (int) (this.GetRandom() % 100U) < num;
    }

    private void ExecuteFirstReactionSkill(Unit attacker, List<Unit> targets, SkillData skill, int tx, int ty, List<LogSkill> results = null)
    {
      if (skill == null || !skill.IsDamagedSkill() || (attacker == null || attacker.IsDeadCondition()) || (targets == null || targets.Count == 0))
        return;
      Grid current = this.CurrentMap[tx, ty];
      if (current == null)
        return;
      Unit main_target = this.FindUnitAtGrid(current);
      if (main_target == null)
      {
        main_target = this.FindGimmickAtGrid(current, false, (Unit) null);
        if (main_target != null && !main_target.IsBreakObj)
          main_target = (Unit) null;
      }
      for (int index = 0; index < targets.Count; ++index)
      {
        Unit target = targets[index];
        if (attacker != target && target.IsEnableReactionCondition())
          this.InternalReactionSkill(ESkillTiming.FirstReaction, attacker, target, main_target, skill, 0, false, results, target == main_target);
      }
    }

    private void ExecuteReactionSkill(LogSkill log, List<LogSkill> results = null)
    {
      if (log == null || log.targets.Count == 0)
        return;
      SkillData skill = log.skill;
      if (!skill.IsDamagedSkill())
        return;
      Unit self = log.self;
      Grid current = this.CurrentMap[log.pos.x, log.pos.y];
      if (current == null)
        return;
      Unit main_target = this.FindUnitAtGrid(current);
      if (main_target == null)
      {
        main_target = this.FindGimmickAtGrid(current, false, (Unit) null);
        if (main_target != null && !main_target.IsBreakObj)
          main_target = (Unit) null;
      }
      if (self.IsDead)
        return;
      for (int index = 0; index < log.targets.Count; ++index)
      {
        if (log.targets[index].guard == null && !log.targets[index].IsAvoid())
        {
          Unit target = log.targets[index].target;
          if (self != target && target.IsEnableReactionCondition())
          {
            int totalHpDamage = log.targets[index].GetTotalHpDamage();
            this.InternalReactionSkill(ESkillTiming.Reaction, self, target, main_target, skill, totalHpDamage, log.targets[index].is_force_reaction, results, target == main_target);
          }
        }
      }
    }

    private void InternalReactionSkill(ESkillTiming timing, Unit attacker, Unit defender, Unit main_target, SkillData received_skill, int damage, bool is_forced, List<LogSkill> results, bool is_main_target)
    {
      if (!is_forced && this.IsObstReaction(attacker, defender, received_skill))
        return;
      for (int index1 = 0; index1 < defender.BattleSkills.Count; ++index1)
      {
        SkillData battleSkill = defender.BattleSkills[index1];
        if (timing == battleSkill.Timing && defender.IsEnableReactionSkill(battleSkill))
        {
          SkillEffectTypes effectType = battleSkill.EffectType;
          switch (effectType)
          {
            case SkillEffectTypes.Guard:
            case SkillEffectTypes.Teleport:
            case SkillEffectTypes.PerfectAvoid:
            case SkillEffectTypes.Throw:
              continue;
            default:
              switch (effectType - 1)
              {
                case SkillEffectTypes.None:
                case SkillEffectTypes.Attack:
                  continue;
                default:
                  switch (effectType - 7)
                  {
                    case SkillEffectTypes.None:
                    case SkillEffectTypes.Defend:
                      continue;
                    default:
                      if (battleSkill.TeleportType == eTeleportType.None && (battleSkill.IsAllDamageReaction() || received_skill.IsNormalAttack() && is_main_target))
                      {
                        switch (battleSkill.ReactionDamageType)
                        {
                          case DamageTypes.TotalDamage:
                            if (!received_skill.IsDamagedSkill() || !battleSkill.IsReactionDet(received_skill.AttackDetailType))
                              continue;
                            break;
                          case DamageTypes.PhyDamage:
                            if (!received_skill.IsPhysicalAttack() || !battleSkill.IsReactionDet(received_skill.AttackDetailType))
                              continue;
                            break;
                          case DamageTypes.MagDamage:
                            if (!received_skill.IsMagicalAttack() || !battleSkill.IsReactionDet(received_skill.AttackDetailType))
                              continue;
                            break;
                          default:
                            continue;
                        }
                        if (battleSkill.UseCondition == null || battleSkill.UseCondition.type == 0 || battleSkill.UseCondition.unlock)
                        {
                          Unit unit1 = defender;
                          Unit unit2;
                          switch (battleSkill.Target)
                          {
                            case ESkillTarget.Self:
                            case ESkillTarget.SelfSide:
                            case ESkillTarget.SelfSideNotSelf:
                              unit2 = defender;
                              break;
                            case ESkillTarget.EnemySide:
                            case ESkillTarget.UnitAll:
                            case ESkillTarget.NotSelf:
                              unit2 = attacker;
                              break;
                            case ESkillTarget.GridNoUnit:
                              return;
                            default:
                              DebugUtility.LogError("リアクションスキル\"" + battleSkill.Name + "\"に不相応なスキル効果対象が設定されている");
                              return;
                          }
                          if (this.CheckSkillCondition(unit1, battleSkill))
                          {
                            int effectRate = (int) battleSkill.EffectRate;
                            if (effectRate <= 0 || effectRate >= 100 || ((int) (this.GetRandom() % 100U) <= effectRate || is_forced))
                            {
                              LogSkill.Reflection reflection = (LogSkill.Reflection) null;
                              if (battleSkill.EffectType == SkillEffectTypes.ReflectDamage)
                              {
                                reflection = new LogSkill.Reflection();
                                reflection.damage = damage;
                              }
                              List<Unit> targets = (List<Unit>) null;
                              BattleCore.ShotTarget shot = (BattleCore.ShotTarget) null;
                              int x = unit2.x;
                              int y = unit2.y;
                              if (unit1.GetAttackRangeMax(battleSkill) > 0)
                              {
                                this.CreateSelectGridMap(unit1, unit1.x, unit1.y, battleSkill, ref this.mRangeMap, false);
                                targets = this.SearchTargetsInGridMap(unit1, battleSkill, this.mRangeMap);
                                if (targets.Contains(unit2))
                                {
                                  targets.Clear();
                                  IntVector2 gridForSkillRange = this.GetValidGridForSkillRange(unit1, unit1.x, unit1.y, battleSkill, x, y);
                                  this.GetExecuteSkillLineTarget(unit1, gridForSkillRange.x, gridForSkillRange.y, battleSkill, ref targets, ref shot);
                                }
                                else
                                  continue;
                              }
                              else
                              {
                                x = unit1.x;
                                y = unit1.y;
                                this.CreateScopeGridMap(unit1, unit1.x, unit1.y, x, y, battleSkill, ref this.mScopeMap, false);
                                targets = this.SearchTargetsInGridMap(unit1, battleSkill, this.mScopeMap);
                                if (!targets.Contains(unit2))
                                  continue;
                              }
                              if (targets != null && targets.Count != 0)
                              {
                                LogSkill log = !this.IsBattleFlag(EBattleFlag.PredictResult) ? this.Log<LogSkill>() : new LogSkill();
                                log.self = unit1;
                                log.skill = battleSkill;
                                log.pos.x = x;
                                log.pos.y = y;
                                log.reflect = reflection;
                                log.is_append = !battleSkill.IsCutin();
                                log.CauseOfReaction = attacker;
                                log.CauseOfReactionPos = new IntVector2(attacker.x, attacker.y);
                                if (shot != null)
                                {
                                  log.pos.x = shot.end.x;
                                  log.pos.y = shot.end.y;
                                  log.rad = (int) (shot.rad * 100.0);
                                  log.height = (int) (shot.height * 100.0);
                                }
                                for (int index2 = 0; index2 < targets.Count; ++index2)
                                  log.SetSkillTarget(defender, targets[index2]);
                                this.ExecuteSkill(timing, log, battleSkill);
                                results?.Add(log);
                                if ((int) battleSkill.SkillParam.count > 0)
                                {
                                  defender.UpdateSkillUseCount(battleSkill, -1);
                                  continue;
                                }
                                continue;
                              }
                              continue;
                            }
                            continue;
                          }
                          continue;
                        }
                        continue;
                      }
                      continue;
                  }
              }
          }
        }
      }
    }

    private bool DeadSkill(Unit self, Unit target)
    {
      if (self == null || !self.IsDead)
        return false;
      bool flag = false;
      for (int index = 0; index < self.BattleSkills.Count; ++index)
      {
        SkillData battleSkill = self.BattleSkills[index];
        if (battleSkill.Timing == ESkillTiming.Dead && battleSkill.IsTransformSkill())
        {
          Unit other = this.SearchTransformUnit(self);
          if (other != null)
          {
            LogSkill log = !this.IsBattleFlag(EBattleFlag.PredictResult) ? this.Log<LogSkill>() : new LogSkill();
            log.self = self;
            log.skill = battleSkill;
            log.pos.x = self.x;
            log.pos.y = self.y;
            log.is_append = !battleSkill.IsCutin();
            log.SetSkillTarget(self, other);
            this.ExecuteSkill(ESkillTiming.Dead, log, battleSkill);
            flag = true;
          }
        }
      }
      return flag;
    }

    public bool UseItem(Unit self, int gx, int gy, ItemData item)
    {
      this.DebugAssert(item != null, "item == null");
      if (!this.UseSkill(self, gx, gy, item.Skill, false, 0, 0, false))
        return false;
      if (!this.IsMultiPlay || this.mMyPlayerIndex == self.OwnerPlayerIndex)
      {
        item.Used(1);
        if (!this.mRecord.used_items.ContainsKey((OString) item.ItemID))
          this.mRecord.used_items[(OString) item.ItemID] = (OInt) 1;
        else
          ++this.mRecord.used_items[(OString) item.ItemID];
      }
      return true;
    }

    public List<Unit> ContinueStart(long btlid, int seed)
    {
      List<Unit> unitList1 = new List<Unit>(BattleCore.MAX_UNITS);
      this.mBtlID = btlid;
      ++this.mContinueCount;
      this.mClockTime = (OInt) 0;
      for (int index = 0; index < this.Units.Count; ++index)
        this.Units[index].ChargeTime = (OInt) 0;
      foreach (Unit unit in this.mPlayer)
      {
        if (!unit.IsDead && (!unit.IsNPC || unit.IsEntry))
        {
          unit.SetUnitFlag(EUnitFlag.EntryDead, true);
          unit.ForceDead();
          unitList1.Add(unit);
        }
      }
      List<Unit> unitList2 = new List<Unit>();
      for (int index = 0; index < this.mPlayer.Count; ++index)
      {
        Unit unit = this.mPlayer[index];
        if ((!unit.IsNPC || unit.IsEntry) && !unit.IsUnitFlag(EUnitFlag.IsDynamicTransform))
        {
          bool isDead = unit.IsDead;
          this.EventTriggerWithdrawContinue(unit);
          unit.NotifyContinue();
          Grid duplicatePosition = this.GetCorrectDuplicatePosition(unit);
          unit.x = duplicatePosition.x;
          unit.y = duplicatePosition.y;
          if (isDead)
          {
            unit.SetUnitFlag(EUnitFlag.Entried, true);
            if (!this.mStartingMembers.Contains(unit))
            {
              unit.ChargeTime = (OInt) 0;
              unit.IsSub = true;
            }
            else
            {
              this.Log<LogUnitEntry>().self = unit;
              unitList2.Add(unit);
            }
          }
        }
      }
      for (int index = 0; index < unitList2.Count; ++index)
        this.BeginBattlePassiveSkill(unitList2[index]);
      for (int index = 0; index < unitList2.Count; ++index)
      {
        Unit unit = unitList2[index];
        unit.UpdateBuffEffects();
        unit.CalcCurrentStatus(false, false);
        unit.CurrentStatus.param.hp = unit.MaximumStatus.param.hp;
        unit.CurrentStatus.param.mp = unit.MaximumStatus.param.mp;
      }
      for (int index = 0; index < unitList2.Count; ++index)
        this.UseAutoSkills(unitList2[index]);
      for (int index = 0; index < this.Units.Count; ++index)
      {
        Unit unit = this.Units[index];
        if (unit.IsBreakObj && unit.CheckLoseEventTrigger())
        {
          bool isDead = unit.IsDead;
          unit.NotifyContinue();
          Grid duplicatePosition = this.GetCorrectDuplicatePosition(unit);
          unit.x = duplicatePosition.x;
          unit.y = duplicatePosition.y;
          if (isDead)
          {
            unit.SetUnitFlag(EUnitFlag.Entried, true);
            this.Log<LogUnitEntry>().self = unit;
          }
        }
      }
      if (this.CheckMonitorWithdrawUnit(this.CurrentMap.LoseMonitorCondition))
      {
        foreach (Unit enemy in this.Enemys)
        {
          if (this.CheckMonitorWithdrawCondition(enemy) && this.EventTriggerWithdrawContinue(enemy))
          {
            enemy.NotifyContinue();
            Grid duplicatePosition = this.GetCorrectDuplicatePosition(enemy);
            enemy.x = duplicatePosition.x;
            enemy.y = duplicatePosition.y;
            enemy.SetUnitFlag(EUnitFlag.Entried, true);
            this.Log<LogUnitEntry>().self = enemy;
          }
        }
      }
      this.mRecord.result = BattleCore.QuestResult.Pending;
      this.SetBattleFlag(EBattleFlag.MapStart, true);
      this.SetBattleFlag(EBattleFlag.UnitStart, false);
      if (this.CheckEnableSuspendSave())
        this.SaveSuspendData();
      this.NextOrder(true, true, false, true);
      return unitList1;
    }

    private bool EventTriggerWithdrawContinue(Unit unit)
    {
      if (unit.SettingNPC == null || unit.EventTrigger == null || !unit.EventTrigger.IsTriggerWithdraw)
        return false;
      unit.EventTrigger.Count = 1;
      if (unit.SettingNPC.trigger != null)
        unit.EventTrigger.Count = unit.SettingNPC.trigger.Count;
      if (unit.EventTrigger.Trigger == EEventTrigger.WdStandbyGrid)
      {
        unit.x = (int) unit.SettingNPC.pos.x;
        unit.y = (int) unit.SettingNPC.pos.y;
        unit.Direction = (EUnitDirection) (int) unit.SettingNPC.dir;
      }
      return true;
    }

    public void MapStart()
    {
      this.DebugAssert(this.IsInitialized, "初期化済みのみコール可");
      this.DebugAssert(!this.IsBattleFlag(EBattleFlag.MapStart), "マップ未開始のみコール可");
      this.CurrentRand = this.mRand;
      this.mUnits.Clear();
      if (this.IsMultiVersus)
      {
        for (int index = 0; index < this.mAllUnits.Count; ++index)
          this.mUnits.Add(this.mAllUnits[index]);
      }
      else
      {
        for (int index = 0; index < this.mPlayer.Count; ++index)
          this.mUnits.Add(this.mPlayer[index]);
        for (int index = 0; index < this.mEnemys[this.MapIndex].Count; ++index)
          this.mUnits.Add(this.mEnemys[this.MapIndex][index]);
      }
      this.mTreasures.Clear();
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (this.mUnits[index].UnitType == EUnitType.Treasure && this.mUnits[index].EventTrigger != null && this.mUnits[index].EventTrigger.EventType == EEventType.Treasure)
          this.mTreasures.Add(this.mUnits[index]);
      }
      this.mGems.Clear();
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (this.mUnits[index].UnitType == EUnitType.Gem && this.mUnits[index].EventTrigger != null && this.mUnits[index].EventTrigger.EventType == EEventType.Gem)
          this.mGems.Add(this.mUnits[index]);
      }
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        this.mUnits[index].NotifyMapStart();
        this.mUnits[index].TowerStartHP = (int) this.mUnits[index].MaximumStatus.param.hp;
      }
      this.SetBattleFlag(EBattleFlag.MapStart, true);
      this.SetBattleFlag(EBattleFlag.UnitStart, false);
      this.mGridLines = new List<Grid>(this.CurrentMap.Width * this.CurrentMap.Height);
      this.mGridLines.Clear();
      this.InitWeather();
      WeatherData.IsEntryConditionLog = false;
      this.UpdateWeather();
      TrickData.ClearEffect();
      foreach (TrickSetting trickSetting in this.CurrentMap.TrickSettings)
        TrickData.EntryEffect(trickSetting.mId, (int) trickSetting.mGx, (int) trickSetting.mGy, trickSetting.mTag, (Unit) null, 0, 1, 1);
      this.CreateGimmickEvents();
      this.UseAutoSkills();
      this.NextOrder(true, true, false, true);
      WeatherData.IsEntryConditionLog = true;
      DebugUtility.Log("rnd:" + (object) this.CloneRand().Get() + "rndDmg:" + (object) this.CloneRandDamage().Get());
    }

    private void UpdateCancelCastSkill()
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (mUnit.CastSkill != null && mUnit.GetSkillUsedCost(mUnit.CastSkill) > mUnit.Gems)
          mUnit.CancelCastSkill();
      }
    }

    private void UpdateUnitCondition(Unit self)
    {
      if (self == null)
        return;
      self.SetUnitFlag(EUnitFlag.Paralysed, false);
      if ((int) (this.GetRandom() % 100U) >= self.GetParalyseRate())
        return;
      self.SetUnitFlag(EUnitFlag.Paralysed, true);
    }

    private void InvokeSkillBuffCond(Unit unit, SkillData skill, ESkillTiming timing)
    {
      BuffEffect buffEffect1 = skill.GetBuffEffect(SkillEffectTargets.Target);
      BuffEffect buffEffect2 = skill.GetBuffEffect(SkillEffectTargets.Self);
      CondEffect condEffect1 = skill.GetCondEffect(SkillEffectTargets.Target);
      CondEffect condEffect2 = skill.GetCondEffect(SkillEffectTargets.Self);
      if (buffEffect1 != null && buffEffect1.param != null && buffEffect1.param.chk_timing == EffectCheckTimings.Eternal)
        buffEffect1 = (BuffEffect) null;
      if (buffEffect2 != null && buffEffect2.param != null && buffEffect2.param.chk_timing == EffectCheckTimings.Eternal)
        buffEffect2 = (BuffEffect) null;
      if (condEffect1 != null && condEffect1.param != null && condEffect1.param.chk_timing == EffectCheckTimings.Eternal)
        condEffect1 = (CondEffect) null;
      if (condEffect2 != null && condEffect2.param != null && condEffect2.param.chk_timing == EffectCheckTimings.Eternal)
        condEffect2 = (CondEffect) null;
      if (buffEffect1 == null && buffEffect2 == null && (condEffect1 == null && condEffect2 == null))
        return;
      int effectRate = (int) skill.EffectRate;
      if (effectRate > 0 && effectRate < 100 && (int) (this.GetRandom() % 100U) > effectRate)
        return;
      if (buffEffect1 != null)
        this.BuffSkill(timing, unit, unit, skill, false, (LogSkill) null, SkillEffectTargets.Target, false, (BuffEffect[]) null, (List<Unit>) null);
      if (buffEffect2 != null)
        this.BuffSkill(timing, unit, unit, skill, false, (LogSkill) null, SkillEffectTargets.Self, false, (BuffEffect[]) null, (List<Unit>) null);
      if (condEffect1 != null)
        this.CondSkill(timing, unit, unit, skill, false, (LogSkill) null, SkillEffectTargets.Target, true, (List<Unit>) null);
      if (condEffect2 == null)
        return;
      this.CondSkill(timing, unit, unit, skill, false, (LogSkill) null, SkillEffectTargets.Self, true, (List<Unit>) null);
    }

    private void UpdateUnitDyingTurn()
    {
      foreach (Unit unit in this.Units)
      {
        if (unit.IsUnitFlag(EUnitFlag.ToDying))
        {
          unit.SetUnitFlag(EUnitFlag.ToDying, false);
          if (!unit.IsDead && unit.IsDying())
          {
            foreach (SkillData battleSkill in unit.BattleSkills)
            {
              if (battleSkill.Timing == ESkillTiming.Dying && battleSkill.Condition == ESkillCondition.Dying && (!battleSkill.IsPassiveSkill() && this.CheckSkillCondition(unit, battleSkill)))
                this.InvokeSkillBuffCond(unit, battleSkill, ESkillTiming.Dying);
            }
          }
        }
      }
    }

    private void UpdateUnitJudgeHPTurn()
    {
      foreach (Unit unit in this.Units)
      {
        if (!unit.IsDead)
        {
          foreach (SkillData battleSkill in unit.BattleSkills)
          {
            if (battleSkill.Timing == ESkillTiming.JudgeHP && battleSkill.Condition == ESkillCondition.JudgeHP && !battleSkill.IsPassiveSkill())
            {
              if (!this.CheckSkillCondition(unit, battleSkill))
                unit.RemoveJudgeHpLists(battleSkill);
              else if (!unit.IsContainsJudgeHpLists(battleSkill))
              {
                this.InvokeSkillBuffCond(unit, battleSkill, ESkillTiming.JudgeHP);
                unit.AddJudgeHpLists(battleSkill);
              }
            }
          }
        }
      }
    }

    public bool UnitStart()
    {
      IEnumerator enumerator = this.UnitStartAsync();
      do
        ;
      while (enumerator.MoveNext());
      return this.IsUnitRestart;
    }

    [DebuggerHidden]
    public IEnumerator UnitStartAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new BattleCore.\u003CUnitStartAsync\u003Ec__Iterator0() { \u0024this = this };
    }

    private void UpdateSkillUseCondition()
    {
      int p_count = 0;
      int e_count = 0;
      for (int index = 0; index < this.Units.Count; ++index)
      {
        if (!this.Units[index].IsGimmick && this.Units[index].CheckExistMap())
        {
          if (this.Units[index].Side == EUnitSide.Player)
            ++p_count;
          else if (this.Units[index].Side == EUnitSide.Enemy)
            ++e_count;
        }
      }
      for (int index1 = 0; index1 < this.Units.Count; ++index1)
      {
        Unit unit = this.Units[index1];
        if (!unit.IsGimmick && unit.CheckExistMap())
        {
          for (int index2 = 0; index2 < unit.BattleSkills.Count; ++index2)
          {
            this.UpdateSkillUseCondition(unit, unit.BattleSkills[index2].UseCondition, p_count, e_count);
            SkillData skillForUseCount = unit.GetSkillForUseCount(unit.BattleSkills[index2].SkillID, 0);
            if (skillForUseCount != null)
              this.UpdateSkillUseCondition(unit, skillForUseCount.UseCondition, p_count, e_count);
          }
          AIAction currentAiAction = unit.GetCurrentAIAction();
          if (currentAiAction != null)
            this.UpdateSkillUseCondition(unit, currentAiAction.cond, p_count, e_count);
        }
      }
    }

    private void UpdateAIActionUseCondition(Unit self)
    {
      int p_count = 0;
      int e_count = 0;
      for (int index = 0; index < this.Units.Count; ++index)
      {
        Unit unit = this.Units[index];
        if (!unit.IsGimmick && unit.CheckExistMap())
        {
          if (unit.Side == EUnitSide.Player)
            ++p_count;
          else if (unit.Side == EUnitSide.Enemy)
            ++e_count;
        }
      }
      if (self.IsGimmick || !self.CheckExistMap())
        return;
      AIAction currentAiAction = self.GetCurrentAIAction();
      if (currentAiAction == null)
        return;
      this.UpdateSkillUseCondition(self, currentAiAction.cond, p_count, e_count);
    }

    private void UpdateSkillUseCondition(Unit unit, SkillLockCondition condition, int p_count, int e_count)
    {
      if (condition == null)
        return;
      SkillLockTypes type = (SkillLockTypes) condition.type;
      switch (type)
      {
        case SkillLockTypes.PartyMemberUpper:
        case SkillLockTypes.PartyMemberLower:
          condition.unlock = type != SkillLockTypes.PartyMemberUpper ? condition.value >= p_count : condition.value <= p_count;
          break;
        case SkillLockTypes.EnemyMemberUpper:
        case SkillLockTypes.EnemyMemberLower:
          condition.unlock = type != SkillLockTypes.EnemyMemberUpper ? condition.value >= e_count : condition.value <= e_count;
          break;
        case SkillLockTypes.HpUpper:
          condition.unlock = (int) unit.CurrentStatus.param.hp >= condition.value;
          break;
        case SkillLockTypes.HpLower:
          condition.unlock = (int) unit.CurrentStatus.param.hp <= condition.value;
          break;
        case SkillLockTypes.OnGrid:
          if (condition.unlock || condition.x == null)
            break;
          bool flag = false;
          for (int index = 0; index < condition.x.Count; ++index)
          {
            if (condition.x[index] == unit.x && condition.y[index] == unit.y)
            {
              flag = true;
              break;
            }
          }
          condition.unlock = flag;
          break;
      }
    }

    private void ActuatedSneaking(Unit unit)
    {
      if (unit.AI == null || !unit.AI.CheckFlag(AIFlags.Sneaking))
        return;
      unit.SetUnitFlag(EUnitFlag.Sneaking, true);
      this.UpdateSearchMap(unit);
      if (!this.CheckEnemyIntercept(unit))
        return;
      unit.SetUnitFlag(EUnitFlag.Sneaking, false);
    }

    public void NotifyMapCommand()
    {
      Unit currentUnit = this.CurrentUnit;
      currentUnit.NotifyCommandStart();
      if (currentUnit.IsUnitFlag(EUnitFlag.Moved))
        return;
      this.UpdateMoveMap(currentUnit);
    }

    public bool ConditionalUnitEnd(bool ignoreMoveAndAction)
    {
      this.DebugAssert(this.IsInitialized, "初期化済みのみコール可");
      this.DebugAssert(this.IsBattleFlag(EBattleFlag.MapStart), "マップ開始済みのみコール可");
      this.DebugAssert(this.IsBattleFlag(EBattleFlag.UnitStart), "ユニット開始済みのみコール可");
      if (this.CheckJudgeBattle())
      {
        this.CalcQuestRecord();
        this.MapEnd();
        return true;
      }
      Unit currentUnit = this.CurrentUnit;
      this.DebugAssert(currentUnit != null, "unit == null");
      if (currentUnit.IsDead)
      {
        this.InternalLogUnitEnd();
        return true;
      }
      bool flag1 = currentUnit.IsEnableMoveCondition(false);
      bool flag2 = currentUnit.IsEnableActionCondition();
      if (ignoreMoveAndAction || flag2 || flag1)
        return false;
      this.CommandWait(false);
      return true;
    }

    public void UnitEnd()
    {
      this.DebugAssert(this.IsInitialized, "初期化済みのみコール可");
      this.DebugAssert(this.IsBattleFlag(EBattleFlag.MapStart), "マップ開始済みのみコール可");
      this.DebugAssert(this.IsBattleFlag(EBattleFlag.UnitStart), "ユニット開始済みのみコール可");
      this.DebugAssert(this.CurrentOrderData != null, "order == null");
      Unit currentUnit = this.CurrentUnit;
      this.DebugAssert(currentUnit != null, "self == null");
      this.SetBattleFlag(EBattleFlag.UnitStart, false);
      this.SetBattleFlag(EBattleFlag.MapCommand, false);
      currentUnit.NotifyActionEnd();
      for (int index = 0; index < this.Units.Count; ++index)
      {
        Unit unit = this.Units[index];
        if (unit.CastSkill != null && unit.UnitTarget != null && (!unit.IsDeadCondition() && currentUnit.CastSkillGridMap != null))
        {
          Unit unit1 = this.Units.Find((Predicate<Unit>) (p => p == unit.UnitTarget));
          if (unit1 != null)
          {
            GridMap<bool> castSkillGridMap = currentUnit.CastSkillGridMap;
            castSkillGridMap.fill(false);
            if (unit.CastSkill.IsAreaSkill())
              this.CreateScopeGridMap(currentUnit, currentUnit.x, currentUnit.y, unit1.x, unit1.y, unit.CastSkill, ref castSkillGridMap, false);
            else
              castSkillGridMap.set(unit1.x, unit1.y, true);
            currentUnit.CastSkillGridMap = castSkillGridMap;
          }
        }
      }
      if (!currentUnit.IsDeadCondition() && currentUnit.IsUnitCondition(EUnitCondition.Poison))
      {
        int poisonDamage = currentUnit.GetPoisonDamage();
        currentUnit.Damage(poisonDamage, true);
        this.GimmickEventHpLower(currentUnit);
        if (currentUnit.Side == EUnitSide.Enemy && poisonDamage > 0)
        {
          this.mTotalDamages += poisonDamage;
          this.mMaxDamage = Math.Max(this.mMaxDamage, poisonDamage);
        }
        if (currentUnit.Side == EUnitSide.Player && currentUnit.IsPartyMember && poisonDamage > 0)
        {
          this.mTotalDamagesTaken += poisonDamage;
          ++this.mTotalDamagesTakenCount;
        }
        LogDamage logDamage = this.Log<LogDamage>();
        logDamage.self = currentUnit;
        logDamage.damage = poisonDamage;
        if (currentUnit.IsDead)
        {
          if (this.CheckGuts(currentUnit))
          {
            currentUnit.Heal(1);
            currentUnit.UpdateBuffEffectTurnCount(EffectCheckTimings.GutsEnd, currentUnit);
            currentUnit.UpdateCondEffectTurnCount(EffectCheckTimings.GutsEnd, currentUnit);
          }
          else
          {
            Unit self = currentUnit;
            for (int index = 0; index < currentUnit.CondAttachments.Count; ++index)
            {
              if (currentUnit.CondAttachments[index].ContainsCondition(EUnitCondition.Poison))
              {
                self = currentUnit.CondAttachments[index].user;
                if (this.TrySetBattleFinisher(currentUnit.CondAttachments[index].user))
                  break;
              }
            }
            this.Dead(self, currentUnit, DeadTypes.Poison, false);
          }
        }
      }
      if (this.mQuestParam.type == QuestTypes.Arena && currentUnit.Side == EUnitSide.Player)
      {
        int num = (int) this.ArenaSubActionCount(1U);
      }
      currentUnit.NotifyActionEndAfter(this.Units);
      this.UpdateBattlePassiveSkill();
      this.NextOrder(false, true, false, true);
    }

    public void CastSkillStart()
    {
      Unit currentUnit = this.CurrentUnit;
      SkillData castSkill = currentUnit.CastSkill;
      if (castSkill != null)
      {
        if (currentUnit.UnitTarget != null)
        {
          this.UseSkill(currentUnit, currentUnit.UnitTarget.x, currentUnit.UnitTarget.y, castSkill, false, 0, 0, false);
          return;
        }
        if (currentUnit.GridTarget != null)
        {
          this.UseSkill(currentUnit, currentUnit.GridTarget.x, currentUnit.GridTarget.y, castSkill, false, 0, 0, false);
          return;
        }
      }
      currentUnit.CancelCastSkill();
      this.Log<LogCastSkillEnd>();
    }

    public void CastSkillEnd()
    {
      this.ExecuteEventTriggerOnGrid(this.CurrentUnit, EEventTrigger.Stop);
      this.NextOrder(false, true, false, true);
    }

    private void MapEnd()
    {
      if (this.IsOrdeal && this.GetQuestResult() == BattleCore.QuestResult.Lose && this.IsOrdealValidNext())
      {
        this.SetBattleFlag(EBattleFlag.UnitStart, false);
        this.Log<LogOrdealChangeNext>();
      }
      else
      {
        this.Log<LogMapEnd>();
        this.SetBattleFlag(EBattleFlag.UnitStart, false);
        this.SetBattleFlag(EBattleFlag.MapStart, false);
      }
    }

    public void IncrementMap()
    {
      this.DebugAssert(!this.IsBattleFlag(EBattleFlag.MapStart), "マップ未開始のみコール可");
      ++this.mMapIndex;
    }

    public int CalcGridDistance(Grid start, Grid goal)
    {
      if (start == null || goal == null)
        return (int) byte.MaxValue;
      return Math.Abs(start.x - goal.x) + Math.Abs(start.y - goal.y);
    }

    public int CalcGridDistance(Unit self, Unit target)
    {
      this.DebugAssert(self != null, "self == null");
      this.DebugAssert(target != null, "target == null");
      return Math.Abs(self.x - target.x) + Math.Abs(self.y - target.y);
    }

    private int FindNearGridAndDistance(Unit self, Unit target, out Grid self_grid, out Grid target_grid)
    {
      this.DebugAssert(self != null, "self == null");
      this.DebugAssert(target != null, "target == null");
      BattleMap currentMap = this.CurrentMap;
      this.DebugAssert(currentMap != null, "map == null");
      int num1 = (int) byte.MaxValue;
      self_grid = (Grid) null;
      target_grid = (Grid) null;
      for (int minColX1 = self.MinColX; minColX1 < self.MaxColX; ++minColX1)
      {
        for (int minColY1 = self.MinColY; minColY1 < self.MaxColY; ++minColY1)
        {
          Grid start = currentMap[self.x + minColX1, self.y + minColY1];
          this.DebugAssert(start != null, "start == null");
          for (int minColX2 = target.MinColX; minColX2 < target.MaxColX; ++minColX2)
          {
            for (int minColY2 = target.MinColY; minColY2 < target.MaxColY; ++minColY2)
            {
              Grid goal = currentMap[target.x + minColX2, target.y + minColY2];
              this.DebugAssert(goal != null, "goal == null");
              int num2 = this.CalcGridDistance(start, goal);
              if (num2 < num1)
              {
                num1 = num2;
                self_grid = start;
                target_grid = goal;
              }
            }
          }
        }
      }
      return num1;
    }

    public int CalcNearGridDistance(Unit self, Unit target)
    {
      Grid self_grid;
      Grid target_grid;
      return this.FindNearGridAndDistance(self, target, out self_grid, out target_grid);
    }

    public int CalcNearGridDistance(Unit self, Grid target)
    {
      this.DebugAssert(self != null, "self == null");
      this.DebugAssert(target != null, "target == null");
      BattleMap currentMap = this.CurrentMap;
      this.DebugAssert(currentMap != null, "map == null");
      int num1 = (int) byte.MaxValue;
      for (int minColX = self.MinColX; minColX < self.MaxColX; ++minColX)
      {
        for (int minColY = self.MinColY; minColY < self.MaxColY; ++minColY)
        {
          Grid start = currentMap[self.x + minColX, self.y + minColY];
          this.DebugAssert(start != null, "start == null");
          Grid goal = currentMap[target.x, target.y];
          this.DebugAssert(goal != null, "goal == null");
          int num2 = this.CalcGridDistance(start, goal);
          if (num2 < num1)
            num1 = num2;
        }
      }
      return num1;
    }

    public int GetUnitMaxAttackHeight(Unit self, SkillData skill)
    {
      return self.GetAttackHeight(skill, false);
    }

    private void UpdateHelperUnits(Unit self)
    {
      this.DebugAssert(self != null, "self == null");
      this.DebugAssert(this.CurrentMap != null, "map == null");
      this.mHelperUnits.Clear();
      if (!self.IsEnableAttackCondition(false) || !self.IsEnableHelpCondition() || this.IsMultiVersus)
        return;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (self != mUnit && self.Side == mUnit.Side && (!mUnit.IsDeadCondition() && mUnit.IsEnableHelpCondition()))
        {
          int combinationRange = self.GetCombinationRange();
          if (this.CalcGridDistance(self, mUnit) <= combinationRange && this.CheckCombination(self, mUnit))
            this.mHelperUnits.Add(mUnit);
        }
      }
    }

    private bool CheckCombination(Unit self, Unit other)
    {
      return (self.GetCombination() + other.GetCombination()) * 100 / 128 >= (int) (this.GetRandom() % 100U);
    }

    public bool CheckGridEventTrigger(Unit self, Grid grid, EEventTrigger trigger)
    {
      if (self == null || self.IsDead || grid == null)
        return false;
      Unit gimmickAtGrid = this.FindGimmickAtGrid(grid, false, (Unit) null);
      if (gimmickAtGrid == null || self == gimmickAtGrid || !gimmickAtGrid.CheckEventTrigger(trigger))
        return false;
      switch (gimmickAtGrid.EventTrigger.EventType)
      {
        case EEventType.Treasure:
        case EEventType.Gem:
          if (self.Side != EUnitSide.Player)
            return false;
          break;
      }
      return true;
    }

    public bool CheckGridEventTrigger(Unit self, EEventTrigger trigger)
    {
      if (self == null || self.IsDead)
        return false;
      Grid unitGridPosition = this.GetUnitGridPosition(self);
      return this.CheckGridEventTrigger(self, unitGridPosition, trigger);
    }

    public bool ExecuteEventTriggerOnGrid(Unit self, EEventTrigger trigger)
    {
      if (!this.CheckGridEventTrigger(self, trigger))
        return false;
      Unit gimmickAtGrid = this.FindGimmickAtGrid(this.GetUnitGridPosition(self), false, (Unit) null);
      return this.GridEventStart(self, gimmickAtGrid, trigger, (SkillParam) null);
    }

    private bool GridEventStart(Unit self, Unit target, EEventTrigger type, SkillParam skill_param = null)
    {
      if (self == null || target == null || (!target.CheckEventTrigger(type) || this.IsBattleFlag(EBattleFlag.PredictResult)))
        return false;
      EventTrigger eventTrigger = target.EventTrigger;
      if (eventTrigger == null)
        return false;
      bool isDead = self.IsDead;
      switch (eventTrigger.Trigger)
      {
        case EEventTrigger.Dead:
          if (eventTrigger.EventType == EEventType.Win || eventTrigger.EventType == EEventType.Lose || !target.IsDead)
            return false;
          break;
        case EEventTrigger.HpDownBorder:
          if (eventTrigger.EventType == EEventType.Win || eventTrigger.EventType == EEventType.Lose || target.MaximumStatusHp * eventTrigger.IntValue / 100 < (int) target.CurrentStatus.param.hp)
            return false;
          break;
        default:
          if (eventTrigger.IsTriggerWithdraw)
          {
            if (eventTrigger.EventType != EEventType.Withdraw || target.IsDead)
              return false;
            List<Unit> unitList = new List<Unit>();
            if (!string.IsNullOrEmpty(eventTrigger.Tag))
            {
              foreach (Unit mUnit in this.mUnits)
              {
                if (!mUnit.IsGimmick || mUnit.IsBreakObj)
                {
                  if (eventTrigger.Trigger == EEventTrigger.WdDeadUnit)
                  {
                    if (!mUnit.IsDead || !mUnit.IsEntry || (mUnit.IsSub || mUnit.IsUnitFlag(EUnitFlag.UnitWithdraw)))
                      continue;
                  }
                  else if (mUnit.IsDead || !mUnit.IsEntry || mUnit.IsSub)
                    continue;
                  if (this.CheckMatchUniqueName(mUnit, eventTrigger.Tag))
                    unitList.Add(mUnit);
                }
              }
            }
            if (eventTrigger.Trigger != EEventTrigger.WdDeadUnit && unitList.Count == 0)
              unitList.Add(target);
            bool flag = false;
            switch (eventTrigger.Trigger)
            {
              case EEventTrigger.WdHpDownRate:
                using (List<Unit>.Enumerator enumerator = unitList.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    Unit current = enumerator.Current;
                    if (current.MaximumStatusHp * eventTrigger.IntValue / 100 >= (int) current.CurrentStatus.param.hp)
                    {
                      flag = true;
                      break;
                    }
                  }
                  break;
                }
              case EEventTrigger.WdHpDownValue:
                using (List<Unit>.Enumerator enumerator = unitList.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    Unit current = enumerator.Current;
                    if (eventTrigger.IntValue >= (int) current.CurrentStatus.param.hp)
                    {
                      flag = true;
                      break;
                    }
                  }
                  break;
                }
              case EEventTrigger.WdElapsedTurn:
                using (List<Unit>.Enumerator enumerator = unitList.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    Unit current = enumerator.Current;
                    if (eventTrigger.IntValue * (1 + this.mContinueCount) <= current.ActionCount)
                    {
                      flag = true;
                      break;
                    }
                  }
                  break;
                }
              case EEventTrigger.WdStandbyGrid:
                if (string.IsNullOrEmpty(eventTrigger.StrValue))
                  return false;
                string[] strArray = eventTrigger.StrValue.Split(',');
                int result1;
                int result2;
                if (strArray == null || strArray.Length < 2 || (!int.TryParse(strArray[0], out result1) || !int.TryParse(strArray[1], out result2)))
                  return false;
                using (List<Unit>.Enumerator enumerator = unitList.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    Unit current = enumerator.Current;
                    if (current.x == result1 && current.y == result2)
                    {
                      flag = true;
                      break;
                    }
                  }
                  break;
                }
              case EEventTrigger.WdDeadUnit:
                if (unitList.Count != 0 && unitList.Count >= eventTrigger.IntValue)
                {
                  flag = true;
                  break;
                }
                break;
              case EEventTrigger.WdUsedSkill:
                using (List<Unit>.Enumerator enumerator = unitList.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    if (enumerator.Current == self && !(eventTrigger.StrValue != skill_param.iname))
                    {
                      flag = true;
                      break;
                    }
                  }
                  break;
                }
            }
            if (!flag)
              return false;
            break;
          }
          break;
      }
      if (eventTrigger.EventType != EEventType.Withdraw)
      {
        LogMapEvent log = this.Log<LogMapEvent>();
        log.self = self;
        log.target = target;
        log.type = eventTrigger.EventType;
        log.gimmick = eventTrigger.GimmickType;
        switch (eventTrigger.EventType)
        {
          case EEventType.Win:
            ++this.mWinTriggerCount;
            break;
          case EEventType.Lose:
            ++this.mLoseTriggerCount;
            break;
          case EEventType.Gem:
            this.AddGems(self, (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GemsGainValue);
            eventTrigger.ExecuteGimmickEffect(target, self, log);
            break;
        }
      }
      else
        this.UnitWithdraw(target);
      target.DecrementTriggerCount();
      switch (eventTrigger.EventType)
      {
        case EEventType.Treasure:
        case EEventType.Gem:
          if (eventTrigger.Count == 0)
          {
            this.UpdateEntryTriggers(UnitEntryTypes.DeadEnemy, target, (SkillParam) null);
            break;
          }
          break;
      }
      if (eventTrigger.Trigger == EEventTrigger.ExecuteOnGrid)
      {
        self.SetUnitFlag(EUnitFlag.Action, true);
        self.SetCommandFlag(EUnitCommandFlag.Action, true);
      }
      if (!isDead && self.IsDead)
        this.Dead((Unit) null, self, DeadTypes.Damage, false);
      return true;
    }

    private void CheckWithDrawUnit(Unit target)
    {
      this.GridEventStart(target, target, EEventTrigger.WdHpDownRate, (SkillParam) null);
      this.GridEventStart(target, target, EEventTrigger.WdHpDownValue, (SkillParam) null);
      this.GridEventStart(target, target, EEventTrigger.WdElapsedTurn, (SkillParam) null);
      this.GridEventStart(target, target, EEventTrigger.WdStandbyGrid, (SkillParam) null);
      this.GridEventStart(target, target, EEventTrigger.WdDeadUnit, (SkillParam) null);
    }

    private EUnitDirection GetAttackDirection(EUnitDirection sdir, int sx, int sy, int tx, int ty)
    {
      bool flag = false;
      if (sdir == EUnitDirection.PositiveX || sdir == EUnitDirection.NegativeX)
        flag = true;
      int num1 = tx - sx;
      int num2 = ty - sy;
      int num3 = Math.Abs(num1);
      int num4 = Math.Abs(num2);
      if (num3 > num4)
      {
        if (num1 < 0)
          return EUnitDirection.NegativeX;
        if (num1 > 0)
          return EUnitDirection.PositiveX;
      }
      if (num3 < num4)
      {
        if (num2 < 0)
          return EUnitDirection.NegativeY;
        if (num2 > 0)
          return EUnitDirection.PositiveY;
      }
      if (flag)
      {
        if (num1 > 0)
          return EUnitDirection.PositiveX;
        if (num1 < 0)
          return EUnitDirection.NegativeX;
        return num2 > 0 || num2 >= 0 ? EUnitDirection.PositiveY : EUnitDirection.NegativeY;
      }
      if (num2 > 0)
        return EUnitDirection.PositiveY;
      if (num2 < 0)
        return EUnitDirection.NegativeY;
      if (num1 > 0)
        return EUnitDirection.PositiveX;
      return num1 < 0 ? EUnitDirection.NegativeX : EUnitDirection.PositiveY;
    }

    private bool CheckBackAttack(Unit self, Unit target, SkillData skill)
    {
      return this.CheckBackAttack(self.x, self.y, target, skill);
    }

    private bool CheckBackAttack(int sx, int sy, Unit target, SkillData skill)
    {
      int attackDirection = (int) this.GetAttackDirection(target.Direction, sx, sy, target.x, target.y);
      int direction = (int) target.Direction;
      return Math.Abs(Unit.DIRECTION_OFFSETS[attackDirection, 0] + Unit.DIRECTION_OFFSETS[direction, 0]) == 2 || Math.Abs(Unit.DIRECTION_OFFSETS[attackDirection, 1] + Unit.DIRECTION_OFFSETS[direction, 1]) == 2;
    }

    private bool CheckSideAttack(Unit self, Unit target, SkillData skill)
    {
      return this.CheckSideAttack(self.x, self.y, target, skill);
    }

    private bool CheckSideAttack(int sx, int sy, Unit target, SkillData skill)
    {
      int attackDirection = (int) this.GetAttackDirection(target.Direction, sx, sy, target.x, target.y);
      int direction = (int) target.Direction;
      return Math.Abs(Unit.DIRECTION_OFFSETS[attackDirection, 0] + Unit.DIRECTION_OFFSETS[direction, 0]) == 1 && Math.Abs(Unit.DIRECTION_OFFSETS[attackDirection, 1] + Unit.DIRECTION_OFFSETS[direction, 1]) == 1;
    }

    public bool CheckDisableAbilities(Unit self)
    {
      if (self.Side != EUnitSide.Player)
        return false;
      return this.mQuestParam.CheckDisableAbilities();
    }

    public bool CheckDisableItems(Unit self)
    {
      if (self.Side != EUnitSide.Player)
        return false;
      return this.mQuestParam.CheckDisableItems();
    }

    public Grid GetCorrectDuplicatePosition(Unit self)
    {
      BattleMap currentMap = this.CurrentMap;
      Grid start = currentMap[self.x, self.y];
      for (int index1 = 0; index1 < this.Units.Count; ++index1)
      {
        Unit unit = this.Units[index1];
        if (unit != self && unit.CheckCollision(start.x, start.y, true))
        {
          int num1 = Math.Max(currentMap.Width, currentMap.Height);
          int num2 = 999;
          Grid grid1 = (Grid) null;
          for (int index2 = -num1; index2 <= num1; ++index2)
          {
            for (int index3 = -num1; index3 <= num1; ++index3)
            {
              if (Math.Abs(index3) + Math.Abs(index2) <= num1)
              {
                int index4 = self.x + index3;
                int index5 = self.y + index2;
                Grid grid2 = currentMap[index4, index5];
                if (currentMap.CheckEnableMove(self, grid2, false, false))
                {
                  bool flag = true;
                  for (int index6 = 0; index6 < this.mUnits.Count; ++index6)
                  {
                    if (!this.mUnits[index6].IsGimmick && this.mUnits[index6] != self && (!this.mUnits[index6].IsSub && !this.mUnits[index6].IsDead) && (this.mUnits[index6].IsEntry && this.mUnits[index6].CheckCollision(grid2)))
                    {
                      flag = false;
                      break;
                    }
                  }
                  if (flag)
                  {
                    int num3 = this.CalcGridDistance(start, grid2);
                    if (num3 < num2)
                    {
                      num2 = num3;
                      grid1 = grid2;
                    }
                  }
                }
              }
            }
          }
          DebugUtility.Assert(grid1 != null, "空きグリッドが見つからなかった");
          return grid1;
        }
      }
      return start;
    }

    public bool EntryBattleMultiPlayTimeUp { get; set; }

    public bool MultiPlayDisconnectAutoBattle { get; set; }

    public bool EntryBattleMultiPlay(EBattleCommand type, Unit current, Unit enemy, SkillData skill, ItemData item, int gx, int gy, bool unitLockTarget)
    {
      return this.ExecMultiPlayerCommandCore(type, current, enemy, skill, item, gx, gy, unitLockTarget);
    }

    public bool CheckSkillScopeMultiPlay(Unit self, Unit target, int gx, int gy, SkillData skill)
    {
      return true;
    }

    private bool ExecMultiPlayerCommandCore(EBattleCommand type, Unit current, Unit enemy, SkillData skill, ItemData item, int gx, int gy, bool unitLockTarget)
    {
      switch (type)
      {
        case EBattleCommand.Attack:
          if (this.CheckSkillScopeMultiPlay(current, enemy, gx, gy, current.GetAttackSkill()) && this.UseSkill(current, gx, gy, current.GetAttackSkill(), unitLockTarget, 0, 0, false))
            return true;
          break;
        case EBattleCommand.Skill:
          if (this.CheckSkillScopeMultiPlay(current, enemy, gx, gy, skill))
          {
            if (skill.EffectType != SkillEffectTypes.Throw ? this.UseSkill(current, gx, gy, skill, unitLockTarget, 0, 0, false) : this.UseSkill(current, gx, gy, skill, unitLockTarget, enemy.x, enemy.y, false))
              return true;
            break;
          }
          break;
      }
      return false;
    }

    public bool CheckEnableSuspendSave()
    {
      return this.mQuestParam.CheckEnableSuspendStart() && !this.IsMultiPlay;
    }

    public bool CheckEnableSuspendStart()
    {
      if ((bool) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.IsDisableSuspend || !this.mQuestParam.CheckEnableSuspendStart() || this.IsMultiPlay)
        return false;
      return BattleSuspend.IsExistData();
    }

    public static void RemoveSuspendData()
    {
      BattleSuspend.RemoveData();
    }

    public bool SaveSuspendData()
    {
      return BattleSuspend.SaveData();
    }

    public bool LoadSuspendData()
    {
      if (!(bool) GlobalVars.BtlIDStatus || !BattleSuspend.LoadData())
        return false;
      this.Logs.Reset();
      GlobalVars.BtlIDStatus.Set(false);
      WeatherData.IsExecuteUpdate = false;
      WeatherData.IsEntryConditionLog = false;
      this.NextOrder(true, true, false, true);
      WeatherData.IsExecuteUpdate = true;
      WeatherData.IsEntryConditionLog = true;
      return true;
    }

    public void SetBattleID(long btlid)
    {
      this.mBtlID = btlid;
    }

    public bool IsArenaSkip
    {
      get
      {
        return this.mIsArenaSkip;
      }
      set
      {
        this.mIsArenaSkip = value;
      }
    }

    public uint ArenaActionCount
    {
      get
      {
        return this.mArenaActionCount;
      }
    }

    private uint ArenaSubActionCount(uint count = 1)
    {
      if (this.mArenaActionCount >= count)
        this.mArenaActionCount -= count;
      else
        this.mArenaActionCount = 0U;
      return this.mArenaActionCount;
    }

    public void ArenaKeepQuestData(string quest_id, BattleCore.Json_Battle json_btl, int max_action_num)
    {
      this.mArenaQuestID = quest_id;
      this.mArenaQuestJsonBtl = json_btl;
      if (max_action_num <= 0)
        return;
      this.mArenaActionCountMax = (uint) max_action_num;
    }

    public bool ArenaResetQuestData()
    {
      this.mArenaActionCount = this.mArenaActionCountMax;
      if (string.IsNullOrEmpty(this.mArenaQuestID) || this.mArenaQuestJsonBtl == null)
        return false;
      this.mMap.Clear();
      this.mPlayer.Clear();
      this.mAllUnits.Clear();
      this.mStartingMembers.Clear();
      this.Deserialize(this.mArenaQuestID, this.mArenaQuestJsonBtl, 0, (UnitData[]) null, (int[]) null, true, (int[]) null, (bool[]) null);
      return true;
    }

    public bool IsArenaCalc
    {
      get
      {
        return this.mIsArenaCalc;
      }
    }

    public BattleCore.QuestResult ArenaCalcResult
    {
      get
      {
        return this.mArenaCalcResult;
      }
    }

    public void ArenaCalcStart()
    {
      this.mArenaActionCount = this.mArenaActionCountMax;
      this.mIsArenaCalc = true;
      this.mArenaCalcResult = BattleCore.QuestResult.Pending;
      this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.MAP_START;
    }

    public void ArenaCalcFinish()
    {
      this.mIsArenaCalc = false;
    }

    private void judgeStartTypeArenaCalc()
    {
      if (this.CurrentOrderData.IsCastSkill)
        this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.CAST_SKILL_START;
      else
        this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.UNIT_START;
    }

    public bool ArenaCalcStep()
    {
      int num1 = 0;
      do
      {
        switch (this.mArenaCalcTypeNext)
        {
          case BattleCore.eArenaCalcType.MAP_START:
            this.MapStart();
            this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.UNIT_START;
            break;
          case BattleCore.eArenaCalcType.UNIT_START:
            int num2 = 0;
            while (num2 < 256 && this.UnitStart())
              ++num2;
            Unit currentUnit = this.CurrentUnit;
            if (currentUnit != null && currentUnit.CastSkill != null && currentUnit.CastSkill.CastType == ECastTypes.Jump)
            {
              this.CommandWait(false);
              this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.UNIT_END;
              break;
            }
            this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.AI;
            break;
          case BattleCore.eArenaCalcType.AI:
            this.IsAutoBattle = true;
            bool flag = this.UpdateMapAI(false);
            this.IsAutoBattle = false;
            if (!flag)
            {
              this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.UNIT_END;
              break;
            }
            break;
          case BattleCore.eArenaCalcType.UNIT_END:
            this.UnitEnd();
            this.judgeStartTypeArenaCalc();
            break;
          case BattleCore.eArenaCalcType.CAST_SKILL_START:
            this.CastSkillStart();
            this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.CAST_SKILL_END;
            break;
          case BattleCore.eArenaCalcType.CAST_SKILL_END:
            this.CastSkillEnd();
            this.judgeStartTypeArenaCalc();
            break;
          case BattleCore.eArenaCalcType.MAP_END:
            if (this.IsBattleFlag(EBattleFlag.UnitStart))
              this.UnitEnd();
            if (this.IsBattleFlag(EBattleFlag.MapStart))
              this.MapEnd();
            this.mArenaCalcResult = this.GetQuestResult();
            return true;
          default:
            DebugUtility.Log(string.Format("BattleCore/ArenaCalcStep Type unknown! type=", (object) this.mArenaCalcTypeNext.ToString()));
            this.mArenaCalcTypeNext = !this.IsBattleFlag(EBattleFlag.MapStart) ? BattleCore.eArenaCalcType.MAP_START : BattleCore.eArenaCalcType.MAP_END;
            break;
        }
        if (!this.IsBattleFlag(EBattleFlag.MapStart) || this.GetQuestResult() != BattleCore.QuestResult.Pending)
          this.mArenaCalcTypeNext = BattleCore.eArenaCalcType.MAP_END;
      }
      while (++num1 < 64);
      return false;
    }

    public uint VersusTurnMax { get; set; }

    public uint VersusTurnCount { get; set; }

    public uint RemainVersusTurnCount
    {
      get
      {
        return this.VersusTurnMax - this.VersusTurnCount;
      }
      set
      {
        this.VersusTurnCount = value;
      }
    }

    public Unit GetUnitUseCollaboSkill(Unit unit, SkillData skill)
    {
      return unit?.GetUnitUseCollaboSkill(skill, false);
    }

    public bool IsUseSkillCollabo(Unit unit, SkillData skill)
    {
      if (unit == null)
        return false;
      return unit.IsUseSkillCollabo(skill, false);
    }

    public int GetDeadCountEnemy()
    {
      int num = 0;
      for (int index = 0; index < this.Units.Count; ++index)
      {
        if (this.Units[index].Side == EUnitSide.Enemy && this.Units[index].IsDead && (!this.Units[index].IsUnitFlag(EUnitFlag.UnitWithdraw) && !this.Units[index].IsUnitFlag(EUnitFlag.UnitTransformed)) && !this.Units[index].IsUnitFlag(EUnitFlag.IsDynamicTransform))
          ++num;
      }
      return num;
    }

    public void RemoveUnitsByUnitFlag(EUnitFlag unit_flag)
    {
      for (int index = this.AllUnits.Count - 1; index >= 0; --index)
      {
        if (this.AllUnits[index].IsUnitFlag(unit_flag))
          this.AllUnits.RemoveAt(index);
      }
      for (int index = this.Units.Count - 1; index >= 0; --index)
      {
        if (this.Units[index].IsUnitFlag(unit_flag))
          this.Units.RemoveAt(index);
      }
      for (int index = this.Enemys.Count - 1; index >= 0; --index)
      {
        if (this.Enemys[index].IsUnitFlag(unit_flag))
          this.Enemys.RemoveAt(index);
      }
      for (int index = this.Player.Count - 1; index >= 0; --index)
      {
        if (this.Player[index].IsUnitFlag(unit_flag))
          this.Player.RemoveAt(index);
      }
    }

    private bool isKnockBack(SkillData skill)
    {
      if (skill == null || (int) skill.KnockBackVal == 0)
        return false;
      return (int) skill.KnockBackRate != 0;
    }

    private bool checkKnockBack(Unit self, Unit target, SkillData skill)
    {
      if (self == null || target == null || (skill == null || !this.isKnockBack(skill)) || (!target.IsKnockBack || target.IsDisableUnitCondition(EUnitCondition.DisableKnockback)))
        return false;
      EnchantParam enchantAssist = self.CurrentStatus.enchant_assist;
      EnchantParam enchantResist = target.CurrentStatus.enchant_resist;
      int num = (int) skill.KnockBackRate + (int) enchantAssist[EnchantTypes.Knockback] - (int) enchantResist[EnchantTypes.Knockback];
      return num > 0 && (num >= 100 || (int) (this.GetRandom() % 100U) < num);
    }

    private Grid getGridKnockBack(Unit unit_att, Unit unit_def, SkillData skill, int gx, int gy, int kb_val = 0, int kb_dir = -1)
    {
      SceneBattle instance = SceneBattle.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return (Grid) null;
      if (!this.isKnockBack(skill))
        return (Grid) null;
      if (!unit_def.IsKnockBack)
        return (Grid) null;
      int index = (int) this.unitDirectionFromPos(unit_att.x, unit_att.y, unit_def.x, unit_def.y);
      switch (skill.KnockBackDs)
      {
        case eKnockBackDs.Self:
          TacticsUnitController unitController = instance.FindUnitController(unit_att);
          if ((UnityEngine.Object) unitController != (UnityEngine.Object) null)
          {
            index = (int) unitController.CalcUnitDirectionFromRotation();
            break;
          }
          break;
        case eKnockBackDs.Grid:
          index = (int) this.unitDirectionFromPos(gx, gy, unit_def.x, unit_def.y);
          break;
      }
      switch (skill.KnockBackDir)
      {
        case eKnockBackDir.Forward:
          index = (int) Unit.ReverseDirection[index];
          break;
        case eKnockBackDir.Left:
          index = (int) Unit.LeftDirection[index];
          break;
        case eKnockBackDir.Right:
          index = (int) Unit.RightDirection[index];
          break;
      }
      if (kb_val <= 0)
        kb_val = (int) skill.KnockBackVal;
      if (kb_dir >= 0)
        index = kb_dir;
      if (skill.KnockBackDs == eKnockBackDs.Target)
      {
        gx = unit_att.x;
        gy = unit_att.y;
      }
      return this.GetGridKnockBack(unit_def, (EUnitDirection) index, kb_val, skill, gx, gy);
    }

    public Grid GetGridKnockBack(Unit target, EUnitDirection dir, int kb_val, SkillData skill = null, int gx = 0, int gy = 0)
    {
      if (!target.IsKnockBack)
        return (Grid) null;
      List<Unit> unitList = new List<Unit>(this.sameJudgeUnitLists.Count);
      foreach (Unit sameJudgeUnitList in this.sameJudgeUnitLists)
      {
        if (sameJudgeUnitList.IsDead)
          unitList.Add(sameJudgeUnitList);
      }
      int knockBackHeight = (int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.KnockBackHeight;
      int index1 = (int) dir;
      int ux = target.x;
      int uy = target.y;
      Grid grid = (Grid) null;
      Grid current1 = this.CurrentMap[ux, uy];
      if (current1 != null)
      {
        for (int index2 = 0; index2 < kb_val; ++index2)
        {
          ux += Unit.DIRECTION_OFFSETS[index1, 0];
          uy += Unit.DIRECTION_OFFSETS[index1, 1];
          Grid current2 = this.CurrentMap[ux, uy];
          if (current2 != null && Math.Abs(current1.height - current2.height) <= knockBackHeight && (this.CurrentMap.CheckEnableMove(target, current2, false, false) && unitList.Find((Predicate<Unit>) (du => du.CheckCollision(ux, uy, false))) == null))
          {
            grid = current2;
            if (skill != null && skill.KnockBackDir == eKnockBackDir.Forward && (skill.KnockBackDs == eKnockBackDs.Target || skill.KnockBackDs == eKnockBackDs.Grid))
              index1 = (int) Unit.ReverseDirection[(int) this.unitDirectionFromPos(gx, gy, ux, uy)];
          }
          else
            break;
        }
      }
      return grid;
    }

    private void procKnockBack(Unit self, SkillData skill, int gx, int gy, List<LogSkill.Target> ls_target_lists)
    {
      if (self == null || skill == null || (ls_target_lists == null || ls_target_lists.Count == 0))
        return;
      this.sameJudgeUnitLists.Clear();
      List<BattleCore.KnockBackTarget> knockBackTargetList = new List<BattleCore.KnockBackTarget>(ls_target_lists.Count);
      foreach (LogSkill.Target lsTargetList in ls_target_lists)
      {
        this.sameJudgeUnitLists.Add(lsTargetList.target);
        if (lsTargetList.target.IsKnockBack)
        {
          lsTargetList.KnockBackGrid = (Grid) null;
          knockBackTargetList.Add(new BattleCore.KnockBackTarget(lsTargetList, lsTargetList.target.x, lsTargetList.target.y));
        }
      }
      for (int index = 0; index < knockBackTargetList.Count; ++index)
      {
        bool flag = false;
        foreach (BattleCore.KnockBackTarget knockBackTarget in knockBackTargetList)
        {
          if (knockBackTarget.mLsTarget != null && knockBackTarget.mMoveLen < (int) skill.KnockBackVal)
          {
            Grid gridKnockBack = this.getGridKnockBack(self, knockBackTarget.mLsTarget.target, skill, gx, gy, (int) skill.KnockBackVal - knockBackTarget.mMoveLen, knockBackTarget.mMoveDir);
            if (gridKnockBack != null)
            {
              knockBackTarget.mLsTarget.KnockBackGrid = gridKnockBack;
              knockBackTarget.mLsTarget.target.x = gridKnockBack.x;
              knockBackTarget.mLsTarget.target.y = gridKnockBack.y;
              knockBackTarget.mMoveLen = Math.Abs(gridKnockBack.x - knockBackTarget.mUnitGx) + Math.Abs(gridKnockBack.y - knockBackTarget.mUnitGy);
              knockBackTarget.mMoveDir = (int) this.unitDirectionFromPos(knockBackTarget.mUnitGx, knockBackTarget.mUnitGy, gridKnockBack.x, gridKnockBack.y);
              flag = true;
            }
          }
        }
        if (!flag)
          break;
      }
      if (this.IsBattleFlag(EBattleFlag.PredictResult))
      {
        foreach (BattleCore.KnockBackTarget knockBackTarget in knockBackTargetList)
        {
          if (knockBackTarget.mLsTarget.KnockBackGrid != null)
          {
            knockBackTarget.mLsTarget.target.x = knockBackTarget.mUnitGx;
            knockBackTarget.mLsTarget.target.y = knockBackTarget.mUnitGy;
          }
        }
      }
      this.sameJudgeUnitLists.Clear();
    }

    public void SetUnitListKnockBack(List<Unit> unit_lists = null)
    {
      this.sameJudgeUnitLists.Clear();
      if (unit_lists == null)
        return;
      this.sameJudgeUnitLists = unit_lists;
    }

    public Grid GetTeleportGrid(Unit self, int gx, int gy, Unit target, SkillData skill, ref bool is_teleport)
    {
      is_teleport = false;
      if (self == null || target == null || (skill == null || this.CurrentMap == null))
        return (Grid) null;
      Grid current1 = this.CurrentMap[gx, gy];
      Grid current2 = this.CurrentMap[target.x, target.y];
      if (current1 == null || current2 == null)
        return (Grid) null;
      Grid grid = (Grid) null;
      int num = target.SizeX <= target.SizeY ? target.SizeY : target.SizeX;
      for (int index1 = 1; index1 <= num; ++index1)
      {
        if (skill.TeleportSkillPos == eTeleportSkillPos.None)
        {
          int index2 = (int) this.UnitDirectionFromGrid(current2, current1);
          grid = this.CurrentMap[current2.x + Unit.DIRECTION_OFFSETS[index2, 0] * index1, current2.y + Unit.DIRECTION_OFFSETS[index2, 1] * index1];
        }
        else
        {
          EUnitDirection eunitDirection = EUnitDirection.Auto;
          switch (skill.TeleportSkillPos)
          {
            case eTeleportSkillPos.Back:
              eunitDirection = Unit.ReverseDirection[(int) target.Direction];
              break;
            case eTeleportSkillPos.Forward:
              eunitDirection = Unit.ForwardDirection[(int) target.Direction];
              break;
            case eTeleportSkillPos.Left:
              eunitDirection = Unit.LeftDirection[(int) target.Direction];
              break;
            case eTeleportSkillPos.Right:
              eunitDirection = Unit.RightDirection[(int) target.Direction];
              break;
          }
          grid = this.CurrentMap[target.x + Unit.DIRECTION_OFFSETS[(int) eunitDirection, 0] * index1, target.y + Unit.DIRECTION_OFFSETS[(int) eunitDirection, 1] * index1];
        }
        if (grid == null)
          return (Grid) null;
        if (!target.CheckCollision(grid))
          break;
      }
      if (skill.IsTargetTeleport && Math.Abs(current2.height - grid.height) <= (int) skill.SkillParam.effect_height && this.CurrentMap.CheckEnableMove(self, grid, false, false))
        is_teleport = true;
      return grid;
    }

    public bool IsTargetBreakUnit(Unit self, Unit target, SkillData skill = null)
    {
      if (self == null || target == null || !target.IsBreakObj || skill != null && skill.EffectType == SkillEffectTypes.Changing)
        return false;
      bool flag = false;
      switch (target.BreakObjClashType)
      {
        case eMapBreakClashType.ALL:
          flag = true;
          break;
        case eMapBreakClashType.PALL:
          flag = !this.IsMultiVersus ? self.Side == EUnitSide.Player : self.OwnerPlayerIndex == target.OwnerPlayerIndex;
          break;
        case eMapBreakClashType.EALL:
          flag = !this.IsMultiVersus ? self.Side == EUnitSide.Enemy : self.OwnerPlayerIndex != target.OwnerPlayerIndex;
          break;
        case eMapBreakClashType.INVINCIBLE:
          flag = false;
          break;
      }
      if (flag && skill != null && target.BreakObjSideType != eMapBreakSideType.UNKNOWN)
      {
        switch (skill.Target)
        {
          case ESkillTarget.Self:
            flag = self == target;
            break;
          case ESkillTarget.SelfSide:
          case ESkillTarget.SelfSideNotSelf:
            flag = !this.CheckGimmickEnemySide(self, target);
            if (skill.Target == ESkillTarget.SelfSideNotSelf && flag)
            {
              flag = self != target;
              break;
            }
            break;
          case ESkillTarget.EnemySide:
            flag = this.CheckGimmickEnemySide(self, target);
            break;
          case ESkillTarget.UnitAll:
            flag = true;
            break;
          case ESkillTarget.NotSelf:
            flag = self != target;
            break;
          case ESkillTarget.GridNoUnit:
            if (skill.TeleportType != eTeleportType.None)
            {
              switch (skill.TeleportTarget)
              {
                case ESkillTarget.Self:
                  flag = self == target;
                  break;
                case ESkillTarget.SelfSide:
                case ESkillTarget.SelfSideNotSelf:
                  flag = !this.CheckGimmickEnemySide(self, target);
                  if (skill.TeleportTarget == ESkillTarget.SelfSideNotSelf && flag)
                  {
                    flag = self != target;
                    break;
                  }
                  break;
                case ESkillTarget.EnemySide:
                  flag = this.CheckGimmickEnemySide(self, target);
                  break;
                case ESkillTarget.UnitAll:
                  flag = true;
                  break;
                case ESkillTarget.NotSelf:
                  flag = self != target;
                  break;
              }
            }
            else
            {
              flag = false;
              break;
            }
        }
      }
      return flag;
    }

    public Unit BreakObjCreate(string bo_id, int gx, int gy, Unit self = null, LogSkill log = null, int owner_index = 0)
    {
      BreakObjParam breakObjParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBreakObjParam(bo_id);
      NPCSetting breakObjNpc = DownloadUtility.CreateBreakObjNPC(breakObjParam, gx, gy);
      if (breakObjNpc == null)
        return (Unit) null;
      Unit unit = new Unit();
      if (unit.Setup((UnitData) null, (UnitSetting) breakObjNpc, (Unit.DropItem) null, (Unit.DropItem) null))
      {
        unit.SetUnitFlag(EUnitFlag.DisableUnitChange, true);
        unit.SetUnitFlag(EUnitFlag.CreatedBreakObj, true);
        unit.SetCreateBreakObj(bo_id, (int) this.mClockTimeTotal);
        this.Enemys.Add(unit);
        this.mAllUnits.Add(unit);
        this.mUnits.Add(unit);
        unit.Direction = breakObjParam.AppearDir != EUnitDirection.Auto || self == null ? breakObjParam.AppearDir : self.Direction;
        Grid duplicatePosition = this.GetCorrectDuplicatePosition(unit);
        unit.x = duplicatePosition.x;
        unit.y = duplicatePosition.y;
        unit.OwnerPlayerIndex = owner_index;
        unit.SetUnitFlag(EUnitFlag.Entried, true);
        if (self != null && log != null)
          log.SetSkillTarget(self, unit);
      }
      else
        this.DebugErr("BreakObjCreateForSkill: enemy unit setup failed");
      return unit;
    }

    private void CheckBreakObjKill()
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (!mUnit.IsDead && mUnit.IsBreakObj && !string.IsNullOrEmpty(mUnit.CreateBreakObjId))
        {
          BreakObjParam breakObjParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBreakObjParam(mUnit.CreateBreakObjId);
          if (breakObjParam != null && breakObjParam.AliveClock != 0 && mUnit.CreateBreakObjClock + breakObjParam.AliveClock <= (int) this.mClockTimeTotal)
          {
            mUnit.CurrentStatus.param.hp = (OInt) 0;
            this.Dead((Unit) null, mUnit, DeadTypes.Damage, false);
          }
        }
      }
    }

    public bool IsAllowBreakObjEntryMax()
    {
      return this.mUnits.FindAll((Predicate<Unit>) (unit =>
      {
        if (unit.IsBreakObj)
          return !unit.IsDead;
        return false;
      })).Count < GameSettings.Instance.Quest.BreakObjAllowEntryMax;
    }

    public void TrickCreateForSkill(Unit self, int gx, int gy, SkillData skill)
    {
      if (self == null || skill == null)
        return;
      string trickId = skill.SkillParam.TrickId;
      if (string.IsNullOrEmpty(trickId))
        return;
      TrickParam trickParam = MonoSingleton<GameManager>.Instance.MasterParam.GetTrickParam(trickId);
      if (trickParam == null)
        return;
      GridMap<bool> result = new GridMap<bool>(this.CurrentMap.Width, this.CurrentMap.Height);
      if (skill.IsAreaSkill())
      {
        this.CreateScopeGridMap(self, self.x, self.y, gx, gy, skill, ref result, false);
        if (skill.SkillParam.TrickSetType == eTrickSetType.GridNoUnit)
        {
          for (int index1 = 0; index1 < result.w; ++index1)
          {
            for (int index2 = 0; index2 < result.h; ++index2)
            {
              if (result.get(index1, index2) && this.IsTrickExistUnit(index1, index2))
                result.set(index1, index2, false);
            }
          }
        }
      }
      else
      {
        result.set(gx, gy, true);
        if (skill.SkillParam.TrickSetType == eTrickSetType.GridNoUnit && this.IsTrickExistUnit(gx, gy))
          result.set(gx, gy, false);
      }
      for (int index1 = 0; index1 < result.w; ++index1)
      {
        for (int index2 = 0; index2 < result.h; ++index2)
        {
          if (result.get(index1, index2))
            TrickData.EntryEffect(trickParam.Iname, index1, index2, (string) null, self, (int) this.mClockTimeTotal, skill.Rank, skill.GetRankCap());
        }
      }
    }

    private bool IsTrickExistUnit(int gx, int gy)
    {
      Unit unit = this.FindUnitAtGrid(gx, gy);
      if (unit == null)
      {
        unit = this.FindGimmickAtGrid(gx, gy, false);
        if (unit != null && !unit.IsBreakObj)
          unit = (Unit) null;
      }
      return unit != null;
    }

    public void TrickActionEndEffect(Unit self, bool is_update_buff = true)
    {
      if (self == null || !TrickData.IsOnEffect(self))
        return;
      bool flag = false;
      List<Unit> unitList = new List<Unit>();
      unitList.Add(self);
      while (unitList.Count != 0)
      {
        List<BattleCore.TdTarget> tdTargetList = new List<BattleCore.TdTarget>();
        for (int index = 0; index < unitList.Count; ++index)
        {
          Unit unit = unitList[index];
          for (int idx = 0; idx < unit.SizeX * unit.SizeY; ++idx)
          {
            IntVector2 bigUnitOffsetPos = this.GetBigUnitOffsetPos(unit, idx);
            int num1 = unit.x + bigUnitOffsetPos.x;
            int num2 = unit.y + bigUnitOffsetPos.y;
            if (TrickData.SearchEffect(num1, num2) != null)
              tdTargetList.Add(new BattleCore.TdTarget(unit, num1, num2));
          }
        }
        unitList.Clear();
        foreach (BattleCore.TdTarget tdTarget in tdTargetList)
        {
          LogMapTrick log_mt = this.Log<LogMapTrick>();
          TrickData.ActionEffect(eTrickActionTiming.TURN_END, tdTarget.mUnit, tdTarget.mX, tdTarget.mY, this.CurrentRand, log_mt);
          foreach (LogMapTrick.TargetInfo targetInfoList in log_mt.TargetInfoLists)
          {
            Unit target = targetInfoList.Target;
            if (targetInfoList.Damage > 0)
            {
              int damage = targetInfoList.Damage;
              if (target.Side == EUnitSide.Enemy)
              {
                this.mTotalDamages += damage;
                this.mMaxDamage = Math.Max(this.mMaxDamage, damage);
              }
              if (target.Side == EUnitSide.Player && target.IsPartyMember)
              {
                this.mTotalDamagesTaken += damage;
                ++this.mTotalDamagesTakenCount;
              }
            }
            if (target.IsDead)
            {
              if (this.CheckGuts(target))
              {
                target.Heal(1);
                target.UpdateBuffEffectTurnCount(EffectCheckTimings.GutsEnd, target);
                target.UpdateCondEffectTurnCount(EffectCheckTimings.GutsEnd, target);
              }
              else
              {
                this.Dead((Unit) null, target, DeadTypes.Damage, false);
                this.TrySetBattleFinisher(log_mt.TrickData.CreateUnit);
              }
            }
            else if (targetInfoList.KnockBackGrid != null)
            {
              if (!unitList.Contains(target))
                unitList.Add(target);
              flag = true;
            }
          }
        }
      }
      if (!flag || !is_update_buff)
        return;
      self.RefleshMomentBuff(this.Units, false, -1, -1);
    }

    public void UnitChange(Unit self, int gx, int gy, EUnitDirection dir, Unit target)
    {
      this.DebugAssert(this.IsMapCommand, "マップコマンド中のみコール可");
      if (self == null || target == null || (self.IsDead || !target.IsSub))
        return;
      self.Direction = dir;
      int chargeTime = (int) self.ChargeTime;
      self.SetUnitFlag(EUnitFlag.EntryDead, true);
      self.SetUnitFlag(EUnitFlag.UnitChanged, true);
      self.UnitChangedHp = (int) self.CurrentStatus.param.hp;
      self.ForceDead();
      if (self.IsUnitFlag(EUnitFlag.IsDynamicTransform))
      {
        Unit unit = this.DtuDead(self);
        if (unit != null)
        {
          unit.SetUnitFlag(EUnitFlag.UnitChanged, true);
          float num = (float) self.UnitChangedHp * 100f / (float) (int) self.MaximumStatus.param.hp;
          unit.UnitChangedHp = Math.Max((int) ((double) (int) unit.MaximumStatus.param.hp * (double) num / 100.0), 1);
          self.SetUnitFlag(EUnitFlag.UnitChanged, false);
          self.UnitChangedHp = 0;
        }
      }
      target.x = self.x;
      target.y = self.y;
      target.Direction = self.Direction;
      target.IsSub = false;
      float num1 = (int) self.ChargeTimeMax == 0 ? 100f : (float) chargeTime * 100f / (float) (int) self.ChargeTimeMax;
      target.ChargeTime = (OInt) (Mathf.CeilToInt((float) ((double) (int) target.ChargeTimeMax * (double) num1 / 100.0)) + 1);
      LogUnitEntry logUnitEntry = this.Log<LogUnitEntry>();
      logUnitEntry.self = target;
      logUnitEntry.kill_unit = self;
      this.BeginBattlePassiveSkill(target);
      target.UpdateBuffEffects();
      target.CalcCurrentStatus(false, false);
      target.CurrentStatus.param.hp = target.MaximumStatus.param.hp;
      if (this.IsTower)
      {
        int unitDamage = MonoSingleton<GameManager>.Instance.TowerResuponse.GetUnitDamage(target.UnitData);
        target.CurrentStatus.param.hp = (OInt) Math.Max((int) target.CurrentStatus.param.hp - unitDamage, 1);
      }
      target.CurrentStatus.param.mp = (OShort) target.GetStartGems();
      target.InitializeSkillUseCount();
      for (int index = 0; index < this.Player.Count; ++index)
      {
        Unit unit = this.Player[index];
        if (unit.IsSub && !unit.IsDead && unit != this.Friend)
        {
          unit.UpdateBuffEffects();
          unit.CalcCurrentStatus(false, false);
          unit.CurrentStatus.param.hp = unit.MaximumStatus.param.hp;
          if (this.IsTower)
          {
            int unitDamage = MonoSingleton<GameManager>.Instance.TowerResuponse.GetUnitDamage(unit.UnitData);
            unit.CurrentStatus.param.hp = (OInt) Math.Max((int) unit.CurrentStatus.param.hp - unitDamage, 1);
          }
          unit.CurrentStatus.param.mp = (OShort) unit.GetStartGems();
        }
      }
      this.UseAutoSkills(target);
    }

    public void UnitChangeReturn()
    {
      for (int index = 0; index < this.mAllUnits.Count; ++index)
      {
        Unit mAllUnit = this.mAllUnits[index];
        if (mAllUnit != null && mAllUnit.IsUnitFlag(EUnitFlag.UnitChanged))
          mAllUnit.CurrentStatus.param.hp = (OInt) mAllUnit.UnitChangedHp;
      }
    }

    public Unit SearchTransformUnit(Unit self)
    {
      return BattleCore.SearchTransformUnit(this.mUnits, self);
    }

    public static Unit SearchTransformUnit(List<Unit> units, Unit targetUnit)
    {
      Unit unit1 = (Unit) null;
      foreach (Unit unit2 in units)
      {
        if ((!unit2.IsGimmick || unit2.IsBreakObj) && (!unit2.IsEntry && !unit2.IsDead) && (!unit2.IsSub && unit2.EntryTriggers != null))
        {
          foreach (UnitEntryTrigger entryTrigger in unit2.EntryTriggers)
          {
            if (entryTrigger.type == 6 && entryTrigger.unit == targetUnit.UniqueName)
            {
              unit1 = unit2;
              break;
            }
          }
          if (unit1 != null)
            break;
        }
      }
      return unit1;
    }

    private Unit DtuTransformForSkill(Unit from_unit, SkillData skill)
    {
      if (from_unit == null || skill == null || skill.SkillParam == null)
        return (Unit) null;
      string dynamicTransformUnitId = skill.SkillParam.DynamicTransformUnitId;
      if (string.IsNullOrEmpty(dynamicTransformUnitId))
        return (Unit) null;
      Unit unit = this.DtuCreateUnit(from_unit, dynamicTransformUnitId);
      if (unit == null)
        return (Unit) null;
      this.DtuActivateUnit(from_unit, unit, false);
      from_unit.SetUnitFlag(EUnitFlag.UnitTransformed, true);
      return unit;
    }

    public Unit DtuCreateUnit(Unit from_unit, string dtu_id)
    {
      if (from_unit == null)
        return (Unit) null;
      DynamicTransformUnitParam transformUnitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetDynamicTransformUnitParam(dtu_id);
      if (transformUnitParam == null)
        return (Unit) null;
      Unit unit = this.DtuFindCreatedUnit(from_unit, transformUnitParam);
      if (unit == null)
      {
        Unit org_unit = from_unit;
        while (org_unit.DtuFromUnit != null)
          org_unit = org_unit.DtuFromUnit;
        unit = new Unit();
        if (!unit.SetupDynamicTransform(org_unit, transformUnitParam, false))
        {
          this.DebugErr("BattleCore/DynamicTransformUnit failed unit Setup!");
          return (Unit) null;
        }
        unit.DtuParam = transformUnitParam;
        unit.DtuFromUnit = from_unit;
        unit.IsSub = false;
        unit.SetUnitFlag(EUnitFlag.Searched, true);
        unit.SetUnitFlag(EUnitFlag.Entried, true);
        unit.SetUnitFlag(EUnitFlag.IsDynamicTransform, true);
        unit.SetUnitFlag(EUnitFlag.ForceAuto, from_unit.IsUnitFlag(EUnitFlag.ForceAuto));
        unit.SetUnitFlag(EUnitFlag.DisableUnitChange, from_unit.IsUnitFlag(EUnitFlag.DisableUnitChange));
        unit.IsPartyMember = from_unit.IsPartyMember;
        unit.OwnerPlayerIndex = from_unit.OwnerPlayerIndex;
        unit.TeamId = from_unit.TeamId;
        switch (unit.Side)
        {
          case EUnitSide.Player:
            this.mPlayer.Add(unit);
            break;
          case EUnitSide.Enemy:
          case EUnitSide.Neutral:
            this.Enemys.Add(unit);
            break;
        }
        this.mAllUnits.Add(unit);
        this.mUnits.Add(unit);
        unit.NotifyMapStart();
      }
      else
      {
        unit.SetUnitFlag(EUnitFlag.EntryDead, false);
        unit.CurrentStatus.param.hp = (OInt) 1;
      }
      unit.DtuRemainTurn = transformUnitParam.Turn;
      this.DtuAttachLeaderFriendSkill(unit);
      return unit;
    }

    private Unit DtuFindCreatedUnit(Unit from_unit, DynamicTransformUnitParam dtu_param)
    {
      if (from_unit == null || dtu_param == null)
        return (Unit) null;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (mUnit.IsUnitFlag(EUnitFlag.IsDynamicTransform) && mUnit.IsDead && (!(mUnit.DtuParam.Iname != dtu_param.Iname) && mUnit.DtuFromUnit == from_unit) && !(mUnit.UnitParam.iname != dtu_param.TrUnitId))
          return mUnit;
      }
      return (Unit) null;
    }

    private void DtuAttachLeaderFriendSkill(Unit unit)
    {
      unit.BuffAttachments.Clear();
      bool flag = false;
      if (this.IsMultiTower)
      {
        for (int index = 0; index < this.mMtLeaderIndexList.Count; ++index)
        {
          int mMtLeaderIndex = this.mMtLeaderIndexList[index];
          if (mMtLeaderIndex >= 0 && mMtLeaderIndex < this.mPlayer.Count)
          {
            Unit self = this.mPlayer[mMtLeaderIndex];
            if (self != null)
            {
              this.InternalBattlePassiveSkill(self, unit, self.LeaderSkill, true, (BuffEffect[]) null);
              flag = true;
            }
          }
        }
      }
      else
      {
        switch (unit.Side)
        {
          case EUnitSide.Player:
            if (this.Leader != null)
            {
              this.InternalBattlePassiveSkill(this.Leader, unit, this.Leader.LeaderSkill, true, (BuffEffect[]) null);
              flag = true;
            }
            if (this.Friend != null)
            {
              this.InternalBattlePassiveSkill(this.Friend, unit, this.Friend.LeaderSkill, true, (BuffEffect[]) null);
              flag = true;
              break;
            }
            break;
          case EUnitSide.Enemy:
            if (this.EnemyLeader != null)
            {
              this.InternalBattlePassiveSkill(this.EnemyLeader, unit, this.EnemyLeader.LeaderSkill, true, (BuffEffect[]) null);
              flag = true;
              break;
            }
            break;
        }
      }
      if (!flag)
        return;
      unit.CalcCurrentStatus(false, false);
    }

    public void DtuActivateUnit(Unit from_unit, Unit new_unit, bool is_cancel = false)
    {
      if (from_unit == null || new_unit == null)
        return;
      new_unit.SetUnitFlag(EUnitFlag.EntryDead, false);
      new_unit.CurrentStatus.param.hp = (OInt) 1;
      for (int index = 0; index < from_unit.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = from_unit.BuffAttachments[index];
        if (!this.IsDtuCheckLeaderFriendSkill(buffAttachment.skill))
        {
          if (buffAttachment.user == from_unit)
            buffAttachment.user = new_unit;
          if (buffAttachment.CheckTarget == from_unit)
            buffAttachment.CheckTarget = new_unit;
          new_unit.BuffAttachments.Add(buffAttachment);
        }
      }
      for (int index = 0; index < from_unit.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = from_unit.CondAttachments[index];
        if (condAttachment.user == from_unit)
          condAttachment.user = new_unit;
        if (condAttachment.CheckTarget == from_unit)
          condAttachment.CheckTarget = new_unit;
        new_unit.CondAttachments.Add(condAttachment);
      }
      for (int index1 = 0; index1 < this.mUnits.Count; ++index1)
      {
        Unit mUnit = this.mUnits[index1];
        for (int index2 = 0; index2 < mUnit.BuffAttachments.Count; ++index2)
        {
          BuffAttachment buffAttachment = mUnit.BuffAttachments[index2];
          if (!this.IsDtuCheckLeaderFriendSkill(buffAttachment.skill))
          {
            if (buffAttachment.user == from_unit)
              buffAttachment.user = new_unit;
            if (buffAttachment.CheckTarget == from_unit)
              buffAttachment.CheckTarget = new_unit;
            for (int index3 = 0; index3 < buffAttachment.AagTargetLists.Count; ++index3)
            {
              if (buffAttachment.AagTargetLists[index3] == from_unit)
                buffAttachment.AagTargetLists[index3] = new_unit;
            }
          }
        }
        for (int index2 = 0; index2 < mUnit.CondAttachments.Count; ++index2)
        {
          CondAttachment condAttachment = mUnit.CondAttachments[index2];
          if (condAttachment.user == from_unit)
            condAttachment.user = new_unit;
          if (condAttachment.CheckTarget == from_unit)
            condAttachment.CheckTarget = new_unit;
        }
      }
      for (int index = 0; index < from_unit.BuffAttachments.Count; ++index)
      {
        if (!this.IsDtuCheckLeaderFriendSkill(from_unit.BuffAttachments[index].skill))
          from_unit.BuffAttachments.RemoveAt(index--);
      }
      from_unit.CondAttachments.Clear();
      new_unit.x = from_unit.x;
      new_unit.y = from_unit.y;
      new_unit.Direction = from_unit.Direction;
      new_unit.DtuCopyTo(from_unit);
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (mUnit.CastSkill != null && mUnit.UnitTarget == from_unit)
          mUnit.UnitTarget = new_unit;
      }
      float num1 = (int) from_unit.ChargeTimeMax == 0 ? 100f : (float) (int) from_unit.ChargeTime * 100f / (float) (int) from_unit.ChargeTimeMax;
      new_unit.ChargeTime = (OInt) (Mathf.CeilToInt((float) ((double) (int) new_unit.ChargeTimeMax * (double) num1 / 100.0)) + 1);
      if (!is_cancel)
      {
        new_unit.SetCommandFlag(EUnitCommandFlag.Move, from_unit.IsCommandFlag(EUnitCommandFlag.Move));
        new_unit.SetCommandFlag(EUnitCommandFlag.Action, true);
        new_unit.ChargeTimeDec();
      }
      float num2 = (float) (int) from_unit.CurrentStatus.param.hp * 100f / (float) (int) from_unit.MaximumStatus.param.hp;
      float num3 = (float) (int) from_unit.CurrentStatus.param.mp * 100f / (float) (int) from_unit.MaximumStatus.param.mp;
      from_unit.ForceDead();
      new_unit.UpdateBuffEffects();
      new_unit.CalcCurrentStatus(false, false);
      new_unit.CurrentStatus.param.hp = (OInt) Math.Max((int) ((double) (int) new_unit.MaximumStatus.param.hp * (double) num2 / 100.0), 1);
      new_unit.CurrentStatus.param.mp = (OShort) (OInt) ((int) ((double) (int) new_unit.MaximumStatus.param.mp * (double) num3 / 100.0));
      bool flag = false;
      if (!is_cancel)
      {
        if (new_unit.DtuParam != null)
          flag = new_unit.DtuParam.IsTransHpFull;
      }
      else if (from_unit.DtuParam != null)
        flag = from_unit.DtuParam.IsCancelHpFull;
      if (flag)
        new_unit.CurrentStatus.param.hp = new_unit.MaximumStatus.param.hp;
      new_unit.SetUnitFlag(EUnitFlag.TriggeredAutoSkills, true);
      SceneBattle instance = SceneBattle.Instance;
      if (!(bool) ((UnityEngine.Object) instance) || !this.IsMultiPlay)
        return;
      instance.ChangeMultiPlayerUnit(from_unit, new_unit);
    }

    private bool IsDtuCheckLeaderFriendSkill(SkillData skill)
    {
      if (skill == null)
        return false;
      if (this.IsMultiTower)
      {
        for (int index = 0; index < this.mMtLeaderIndexList.Count; ++index)
        {
          int mMtLeaderIndex = this.mMtLeaderIndexList[index];
          if (mMtLeaderIndex >= 0 && mMtLeaderIndex < this.mPlayer.Count)
          {
            Unit unit = this.mPlayer[mMtLeaderIndex];
            if (unit != null && unit.LeaderSkill == skill)
              return true;
          }
        }
      }
      else if (this.Leader != null && this.Leader.LeaderSkill == skill || this.Friend != null && this.Friend.LeaderSkill == skill || this.EnemyLeader != null && this.EnemyLeader.LeaderSkill == skill)
        return true;
      return false;
    }

    private bool DtuUpdateTurn(Unit self)
    {
      if (self == null || !self.IsUnitFlag(EUnitFlag.IsDynamicTransform) || (self.DtuFromUnit == null || self.DtuRemainTurn <= 0))
        return false;
      --self.DtuRemainTurn;
      if (self.DtuRemainTurn <= 0)
      {
        Unit unit = this.DtuCancelTransform(self);
        if (unit != null)
        {
          LogCancelTransform logCancelTransform = this.Log<LogCancelTransform>();
          logCancelTransform.self = self;
          logCancelTransform.next_unit = unit;
          logCancelTransform.dtu = self.DtuParam;
          SceneBattle instance = SceneBattle.Instance;
          if ((bool) ((UnityEngine.Object) instance))
          {
            TacticsUnitController unitController = instance.FindUnitController(self);
            if ((bool) ((UnityEngine.Object) unitController))
              unitController.EnableProhibitedUpdateBadStatus(true);
            instance.IsNoCountUpUnitStartCount = true;
          }
          return true;
        }
      }
      return false;
    }

    private Unit DtuCancelTransform(Unit from_unit)
    {
      if (from_unit == null)
        return (Unit) null;
      if (!from_unit.IsUnitFlag(EUnitFlag.IsDynamicTransform) || from_unit.DtuFromUnit == null)
        return (Unit) null;
      Unit dtuFromUnit = from_unit.DtuFromUnit;
      this.DtuActivateUnit(from_unit, dtuFromUnit, true);
      dtuFromUnit.SetUnitFlag(EUnitFlag.UnitTransformed, false);
      return dtuFromUnit;
    }

    public Unit DtuDead(Unit unit)
    {
      if (unit == null)
        return (Unit) null;
      if (!unit.IsDead || !unit.IsUnitFlag(EUnitFlag.IsDynamicTransform))
        return (Unit) null;
      unit.SetUnitFlag(EUnitFlag.EntryDead, false);
      unit.CurrentStatus.param.hp = (OInt) 1;
      Unit unit1 = unit;
      do
      {
        unit = this.DtuCancelTransform(unit);
      }
      while (unit != null && unit.DtuFromUnit != null);
      if (unit == null)
        unit = unit1;
      unit.ForceDead();
      return unit;
    }

    public void DtuReturn()
    {
      for (int index = 0; index < this.mAllUnits.Count; ++index)
      {
        Unit from_unit = this.mAllUnits[index];
        if (from_unit != null && !from_unit.IsDead && from_unit.IsUnitFlag(EUnitFlag.IsDynamicTransform))
        {
          do
          {
            from_unit = this.DtuCancelTransform(from_unit);
          }
          while (from_unit != null && from_unit.DtuFromUnit != null);
        }
      }
    }

    public Unit DtuFindOrgUnit(Unit unit)
    {
      if (unit == null)
        return (Unit) null;
      while (unit.IsUnitFlag(EUnitFlag.IsDynamicTransform) && unit.DtuFromUnit != null)
        unit = unit.DtuFromUnit;
      return unit;
    }

    public Unit DtuFindActUnit(Unit unit)
    {
      if (unit == null)
        return (Unit) null;
      Unit unit1;
      for (; unit.IsUnitFlag(EUnitFlag.UnitTransformed); unit = unit1)
      {
        unit1 = this.mAllUnits.Find((Predicate<Unit>) (un =>
        {
          if (!un.IsDead || un.IsUnitFlag(EUnitFlag.UnitTransformed))
            return un.DtuFromUnit == unit;
          return false;
        }));
        if (unit1 == null)
          break;
      }
      return unit;
    }

    public bool DtuGetHpMpRate(Unit unit, ref int hp, ref int mp)
    {
      Unit actUnit = this.DtuFindActUnit(unit);
      if (actUnit == unit)
        return false;
      float num1 = (float) (int) actUnit.CurrentStatus.param.hp * 100f / (float) (int) actUnit.MaximumStatus.param.hp;
      hp = Math.Max((int) ((double) (int) unit.MaximumStatus.param.hp * (double) num1 / 100.0), 1);
      float num2 = (float) (int) actUnit.CurrentStatus.param.mp * 100f / (float) (int) actUnit.MaximumStatus.param.mp;
      mp = (int) (OInt) ((int) ((double) (int) unit.MaximumStatus.param.mp * (double) num2 / 100.0));
      return true;
    }

    public void UnitWithdraw(Unit self)
    {
      if (self == null || self.IsDead)
        return;
      bool flag = false;
      for (int index = 0; index < (int) Unit.MAX_UNIT_CONDITION; ++index)
      {
        EUnitCondition eunitCondition = (EUnitCondition) (1L << index);
        if (self.IsUnitCondition(eunitCondition))
        {
          self.CureCondEffects(eunitCondition, false, true);
          flag = true;
        }
      }
      if (flag)
        self.UpdateCondEffects();
      self.SetUnitFlag(EUnitFlag.EntryDead, true);
      self.SetUnitFlag(EUnitFlag.UnitWithdraw, true);
      self.ForceDead();
      if (self.IsUnitFlag(EUnitFlag.IsDynamicTransform))
      {
        Unit unit = this.DtuDead(self);
        if (unit != null)
        {
          unit.SetUnitFlag(EUnitFlag.UnitWithdraw, true);
          self.SetUnitFlag(EUnitFlag.UnitWithdraw, false);
        }
      }
      this.UpdateEntryTriggers(UnitEntryTypes.WithdrawEnemy, self, (SkillParam) null);
      this.Log<LogUnitWithdraw>().self = self;
    }

    private void InitWeather()
    {
      WeatherSetParam weather_set = (WeatherSetParam) null;
      if (this.mQuestParam != null)
        weather_set = MonoSingleton<GameManager>.Instance.GetWeatherSetParam(this.mQuestParam.WeatherSetId);
      WeatherData.Initialize(weather_set, !this.mQuestParam.IsWeatherNoChange);
    }

    private bool UpdateWeather()
    {
      return WeatherData.UpdateWeather(this.Units, this.ClockTimeTotal, this.CurrentRand);
    }

    private bool ChangeWeatherForSkill(Unit self, SkillData skill)
    {
      if (self == null || skill == null || !WeatherData.IsAllowWeatherChange)
        return false;
      int weatherRate = skill.WeatherRate;
      string weatherId = skill.WeatherId;
      if (weatherRate <= 0 || string.IsNullOrEmpty(weatherId) || MonoSingleton<GameManager>.Instance.MasterParam.GetWeatherParam(weatherId) == null)
        return false;
      WeatherData currentWeatherData = WeatherData.CurrentWeatherData;
      if (currentWeatherData != null)
        weatherRate -= currentWeatherData.GetResistRate(this.ClockTimeTotal);
      return (weatherRate >= 100 || (int) (this.GetRandom() % 100U) < weatherRate) && WeatherData.ChangeWeather(weatherId, this.Units, this.ClockTimeTotal, this.CurrentRand, self, skill.Rank, skill.GetRankCap()) != null;
    }

    private void AddSkillExecLogForQuestMission(LogSkill log)
    {
      if (log.skill == null || this.mQuestParam == null || this.mQuestParam.bonusObjective == null)
        return;
      bool flag = false;
      for (int index = 0; index < this.mQuestParam.bonusObjective.Length; ++index)
      {
        if (!this.mQuestParam.bonusObjective[index].IsMissionTypeExecSkill())
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return;
      int num = 0;
      for (int index = 0; index < log.targets.Count; ++index)
      {
        if (log.targets[index].target.Side == EUnitSide.Enemy && log.targets[index].target.IsDead)
          ++num;
      }
      if (!this.mSkillExecLogs.ContainsKey(log.skill.SkillID))
      {
        this.mSkillExecLogs.Add(log.skill.SkillID, new BattleCore.SkillExecLog()
        {
          skill_iname = log.skill.SkillID,
          use_count = 1,
          kill_count = num
        });
      }
      else
      {
        ++this.mSkillExecLogs[log.skill.SkillID].use_count;
        this.mSkillExecLogs[log.skill.SkillID].kill_count += num;
      }
    }

    private bool TrySetBattleFinisher(Unit _unit)
    {
      if (_unit == null || !this.CheckJudgeBattle() || _unit.Side != EUnitSide.Player)
        return false;
      this.mFinisherIname = _unit.UnitParam.iname;
      return true;
    }

    public RankingQuestParam GetRankingQuestParam()
    {
      return this.mRankingQuestParam;
    }

    public int CalcPlayerUnitsTotalParameter()
    {
      int num = 0;
      StringBuilder stringBuilder = new StringBuilder(128);
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        if (this.mUnits[index] != null && this.mUnits[index].IsPartyMember && (!this.mUnits[index].IsNPC && this.mUnits[index].UnitData != null))
        {
          stringBuilder.Append(this.mUnits[index].UnitData.UnitParam.name);
          stringBuilder.Append("\n    ");
          stringBuilder.Append("total : ");
          stringBuilder.Append(this.mUnits[index].UnitData.CalcTotalParameter());
          stringBuilder.Append("\n");
          num += this.mUnits[index].UnitData.CalcTotalParameter();
        }
      }
      stringBuilder.Append("総合値 : ");
      stringBuilder.Append(num);
      DebugUtility.Log(stringBuilder.ToString());
      return num;
    }

    public bool IsOrdealValidNext()
    {
      int num = this.mCurrentTeamId + 1;
      if (num >= this.mMaxTeamId)
        return false;
      foreach (Unit unit in this.mPlayer)
      {
        if (unit.TeamId == num && !unit.IsDead)
          return true;
      }
      return false;
    }

    public void OrdealChangeNext()
    {
      if (!this.IsOrdealValidNext() || this.CurrentMap == null)
        return;
      foreach (Unit unit in this.Units)
        unit.ChargeTime = (OInt) 0;
      foreach (Unit unit in this.mPlayer)
      {
        if (!unit.IsUnitFlag(EUnitFlag.OtherTeam) && !unit.IsDead)
        {
          unit.SetUnitFlag(EUnitFlag.EntryDead, true);
          unit.ForceDead();
          if (unit.IsUnitFlag(EUnitFlag.IsDynamicTransform))
            this.DtuDead(unit);
        }
      }
      ++this.mCurrentTeamId;
      this.mFriendIndex = (OInt) -1;
      List<Unit> unitList = new List<Unit>();
      int num = 0;
      for (int index = 0; index < this.mPlayer.Count; ++index)
      {
        Unit self = this.mPlayer[index];
        self.SetUnitFlag(EUnitFlag.OtherTeam, self.TeamId != this.mCurrentTeamId);
        if (!self.IsUnitFlag(EUnitFlag.OtherTeam))
        {
          if (num == 0)
            this.mLeaderIndex = (OInt) index;
          if (self.IsUnitFlag(EUnitFlag.IsHelp))
          {
            this.mFriendIndex = (OInt) index;
            this.mFriendStates = self.FriendStates;
          }
          Grid duplicatePosition = this.GetCorrectDuplicatePosition(self);
          self.x = duplicatePosition.x;
          self.y = duplicatePosition.y;
          self.SetUnitFlag(EUnitFlag.Entried, true);
          self.ChargeTime = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.OrdealCT;
          unitList.Add(self);
          ++num;
          this.Log<LogUnitEntry>().self = self;
        }
      }
      if (this.Leader != null)
        this.InternalBattlePassiveSkill(this.Leader, this.Leader.LeaderSkill, true, (BuffEffect[]) null);
      if (this.Friend != null && this.mFriendStates == FriendStates.Friend)
        this.InternalBattlePassiveSkill(this.Friend, this.Friend.LeaderSkill, true, (BuffEffect[]) null);
      foreach (Unit unit in unitList)
      {
        unit.CalcCurrentStatus(true, false);
        this.BeginBattlePassiveSkill(unit);
      }
      for (int index = 0; index < unitList.Count; ++index)
      {
        Unit unit = unitList[index];
        unit.UpdateBuffEffects();
        unit.CalcCurrentStatus(false, false);
        unit.CurrentStatus.param.hp = unit.MaximumStatus.param.hp;
        unit.CurrentStatus.param.mp = (OShort) unit.GetStartGems();
      }
      for (int index = 0; index < unitList.Count; ++index)
        this.UseAutoSkills(unitList[index]);
      this.mRecord.result = BattleCore.QuestResult.Pending;
      this.NextOrder(true, true, false, true);
    }

    public void OrdealRestore(int team_id)
    {
      if (!this.IsOrdeal || team_id == 0)
        return;
      this.mFriendIndex = (OInt) -1;
      List<Unit> unitList = new List<Unit>();
      int num = 0;
      for (int index = 0; index < this.mPlayer.Count; ++index)
      {
        Unit unit = this.mPlayer[index];
        unit.SetUnitFlag(EUnitFlag.OtherTeam, unit.TeamId != team_id);
        if (!unit.IsUnitFlag(EUnitFlag.OtherTeam))
        {
          if (num == 0)
            this.mLeaderIndex = (OInt) index;
          if (unit.IsUnitFlag(EUnitFlag.IsHelp))
          {
            this.mFriendIndex = (OInt) index;
            this.mFriendStates = unit.FriendStates;
          }
          unitList.Add(unit);
          ++num;
        }
      }
      if (this.Leader != null)
        this.InternalBattlePassiveSkill(this.Leader, this.Leader.LeaderSkill, true, (BuffEffect[]) null);
      if (this.Friend != null && this.mFriendStates == FriendStates.Friend)
        this.InternalBattlePassiveSkill(this.Friend, this.Friend.LeaderSkill, true, (BuffEffect[]) null);
      foreach (Unit unit in unitList)
        this.BeginBattlePassiveSkill(unit);
    }

    private bool AbilityChange(Unit self, Unit target, SkillData skill)
    {
      if (self == null || target == null || (skill == null || skill.SkillParam == null))
        return false;
      string acFromAbilId = skill.SkillParam.AcFromAbilId;
      string acToAbilId = skill.SkillParam.AcToAbilId;
      if (string.IsNullOrEmpty(acFromAbilId) || string.IsNullOrEmpty(acToAbilId))
        return false;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      AbilityParam abilityParam1 = instance.MasterParam.GetAbilityParam(acFromAbilId);
      AbilityParam abilityParam2 = instance.MasterParam.GetAbilityParam(acToAbilId);
      if (abilityParam1 == null || abilityParam2 == null)
        return false;
      Unit unit = target;
      if (skill.SkillParam.IsAcSelf())
        unit = self;
      return unit.ExecuteAbilityChange(abilityParam1, abilityParam2, skill.SkillParam.AcTurn, skill.SkillParam.IsAcReset());
    }

    public bool IsEnableAag
    {
      get
      {
        if (!this.IsEnableAagBuff && !this.IsEnableAagDebuff && !this.IsEnableAagBuffNegative)
          return this.IsEnableAagDebuffNegative;
        return true;
      }
    }

    private void AbsorbAndGiveClear()
    {
      BattleCore.BuffAagWorkStatus.Clear();
      BattleCore.DebuffAagWorkStatus.Clear();
      BattleCore.BuffAagNegativeWorkStatus.Clear();
      BattleCore.DebuffAagNegativeWorkStatus.Clear();
      this.IsEnableAagBuff = false;
      this.IsEnableAagDebuff = false;
      this.IsEnableAagBuffNegative = false;
      this.IsEnableAagDebuffNegative = false;
      this.AagBuffAttachmentLists.Clear();
      this.AagTargetLists.Clear();
    }

    private void AbsorbAndGiveApply(Unit self, SkillData skill, LogSkill log)
    {
      if (!this.IsEnableAag || self == null || (skill == null || skill.SkillParam == null) || log == null)
        return;
      eAbsorbAndGive absorbAndGive = skill.SkillParam.AbsorbAndGive;
      if (absorbAndGive == eAbsorbAndGive.None)
        return;
      List<Unit> at_lists = new List<Unit>((IEnumerable<Unit>) this.AagTargetLists);
      if (SkillParam.IsAagTypeGive(absorbAndGive))
      {
        if (SkillParam.IsAagTypeDiv(absorbAndGive) && this.AagTargetLists.Count > 1)
        {
          int count = this.AagTargetLists.Count;
          BattleCore.BuffAagWorkStatus.Div(count);
          BattleCore.BuffAagNegativeWorkStatus.Div(count);
          BattleCore.DebuffAagWorkStatus.Div(count);
          BattleCore.DebuffAagNegativeWorkStatus.Div(count);
        }
        for (int index = 0; index < this.AagTargetLists.Count; ++index)
        {
          Unit aagTargetList = this.AagTargetLists[index];
          this.AbsorbAndGiveApplySetBuff(self, aagTargetList, skill, at_lists, log);
        }
      }
      else
        this.AbsorbAndGiveApplySetBuff(self, self, skill, at_lists, log);
      for (int index = 0; index < this.AagBuffAttachmentLists.Count; ++index)
        this.AagBuffAttachmentLists[index].AagTargetLists = at_lists;
      this.AbsorbAndGiveClear();
    }

    private void AbsorbAndGiveApplySetBuff(Unit self, Unit target, SkillData skill, List<Unit> at_lists, LogSkill log = null)
    {
      if (self == null || target == null || skill == null)
        return;
      SkillEffectTargets skillEffectTargets = SkillEffectTargets.Target;
      BuffEffect buffEffect = skill.GetBuffEffect(skillEffectTargets);
      if (buffEffect == null)
        return;
      ESkillCondition cond = buffEffect.param.cond;
      int turn = (int) buffEffect.param.turn;
      EffectCheckTargets chkTarget = buffEffect.param.chk_target;
      EffectCheckTimings chkTiming = buffEffect.param.chk_timing;
      int duplicateCount = skill.DuplicateCount;
      if (at_lists == null)
        at_lists = new List<Unit>();
      bool flag = false;
      if (this.IsEnableAagBuff)
      {
        BuffAttachment buffAttachment = this.CreateBuffAttachment(self, target, skill, skillEffectTargets, buffEffect.param, BuffTypes.Buff, false, SkillParamCalcTypes.Add, BattleCore.BuffAagWorkStatus, cond, turn, chkTarget, chkTiming, false, duplicateCount);
        if (buffAttachment != null)
          buffAttachment.AagTargetLists = at_lists;
        if (target.SetBuffAttachment(buffAttachment, false))
          flag = true;
      }
      if (this.IsEnableAagBuffNegative)
      {
        BuffAttachment buffAttachment = this.CreateBuffAttachment(self, target, skill, skillEffectTargets, buffEffect.param, BuffTypes.Buff, true, SkillParamCalcTypes.Add, BattleCore.BuffAagNegativeWorkStatus, cond, turn, chkTarget, chkTiming, false, duplicateCount);
        if (buffAttachment != null)
          buffAttachment.AagTargetLists = at_lists;
        if (target.SetBuffAttachment(buffAttachment, false))
          flag = true;
      }
      if (this.IsEnableAagDebuff)
      {
        BuffAttachment buffAttachment = this.CreateBuffAttachment(self, target, skill, skillEffectTargets, buffEffect.param, BuffTypes.Debuff, false, SkillParamCalcTypes.Add, BattleCore.DebuffAagWorkStatus, cond, turn, chkTarget, chkTiming, false, duplicateCount);
        if (buffAttachment != null)
          buffAttachment.AagTargetLists = at_lists;
        if (target.SetBuffAttachment(buffAttachment, false))
          flag = true;
      }
      if (this.IsEnableAagDebuffNegative)
      {
        BuffAttachment buffAttachment = this.CreateBuffAttachment(self, target, skill, skillEffectTargets, buffEffect.param, BuffTypes.Debuff, true, SkillParamCalcTypes.Add, BattleCore.DebuffAagNegativeWorkStatus, cond, turn, chkTarget, chkTiming, false, duplicateCount);
        if (buffAttachment != null)
          buffAttachment.AagTargetLists = at_lists;
        if (target.SetBuffAttachment(buffAttachment, false))
          flag = true;
      }
      if (log == null || !flag)
        return;
      BattleCore.AagWorkStatus.Clear();
      BattleCore.AagWorkStatus.Add(BattleCore.BuffAagWorkStatus);
      BattleCore.AagWorkStatus.Add(BattleCore.BuffAagNegativeWorkStatus);
      BattleCore.AagWorkStatus.Add(BattleCore.DebuffAagWorkStatus);
      BattleCore.AagWorkStatus.Add(BattleCore.DebuffAagNegativeWorkStatus);
      BuffBit buff = new BuffBit();
      BuffBit debuff = new BuffBit();
      BattleCore.SetBuffBits(BattleCore.AagWorkStatus, ref buff, ref debuff);
      LogSkill.Target target1 = log.FindTarget(target);
      if (target1 == null)
      {
        target1 = log.self_effect;
        target1.target = target;
      }
      if (target1 == null)
        return;
      buff.CopyTo(target1.buff);
      debuff.CopyTo(target1.debuff);
    }

    private bool UpdateEntryByInfinitySpawn(List<Unit> units)
    {
      bool flag = false;
      if (this.CurrentMap.InfinitySpawnMgr == null)
        return flag;
      InfinitySpawnManager infinitySpawnMgr = this.CurrentMap.InfinitySpawnMgr;
      infinitySpawnMgr.RefreshSpawnEnemyUnits(units);
      for (int index1 = 0; index1 < infinitySpawnMgr.InfinitySpawnGroups.Length; ++index1)
      {
        for (int index2 = 0; index2 < infinitySpawnMgr.InfinitySpawnGroups[index1].InfinitySpawns.Count; ++index2)
        {
          InfinitySpawnData infinitySpawn = infinitySpawnMgr.InfinitySpawnGroups[index1].InfinitySpawns[index2];
          if (infinitySpawn.ReserveUnitIndexList.Count > 0)
          {
            for (int index3 = 0; index3 < infinitySpawn.ReserveUnitIndexList.Count; ++index3)
            {
              int reserveUnitIndex = infinitySpawn.ReserveUnitIndexList[index3];
              Unit infinitySpawnUnit = this.CreateInfinitySpawnUnit(string.Empty, infinitySpawn.x, infinitySpawn.y, infinitySpawn.dir, infinitySpawn.group, reserveUnitIndex, false);
              if (infinitySpawnUnit != null)
              {
                this.Log<LogUnitEntry>().self = infinitySpawnUnit;
                flag = true;
              }
            }
          }
        }
      }
      return flag;
    }

    public Unit CreateInfinitySpawnUnit(string unit_name, int x, int y, int dir, int group, int deck_index, bool dont_add = false)
    {
      if (this.CurrentMap.InfinitySpawnMgr == null)
        return (Unit) null;
      JSON_MapEnemyUnit deckUnit = this.CurrentMap.InfinitySpawnMgr.GetDeckUnit(deck_index);
      if (deckUnit == null)
        return (Unit) null;
      if (deckUnit.iname == "EMPTY")
        return (Unit) null;
      NPCSetting npcSetting = new NPCSetting(deckUnit);
      npcSetting.pos.x = (OInt) x;
      npcSetting.pos.y = (OInt) y;
      npcSetting.dir = (OInt) dir;
      Unit self = new Unit();
      if (self.Setup((UnitData) null, (UnitSetting) npcSetting, (Unit.DropItem) null, (Unit.DropItem) null))
      {
        if (!dont_add)
        {
          self.SetUnitFlag(EUnitFlag.InfinitySpawn, true);
          self.SetUnitFlag(EUnitFlag.Reinforcement, true);
          self.SetUnitFlag(EUnitFlag.Entried, true);
          this.Enemys.Add(self);
          this.mAllUnits.Add(self);
          this.mUnits.Add(self);
        }
        Grid duplicatePosition = this.GetCorrectDuplicatePosition(self);
        self.x = duplicatePosition.x;
        self.y = duplicatePosition.y;
        self.InfinitySpawnTag = group;
        self.InfinitySpawnDeck = deck_index;
        if (!string.IsNullOrEmpty(unit_name))
          self.UnitName = unit_name;
      }
      return self;
    }

    public GridMap<int> MoveMap
    {
      get
      {
        return this.mMoveMap;
      }
    }

    public GridMap<bool> RangeMap
    {
      get
      {
        return this.mRangeMap;
      }
    }

    public GridMap<bool> ScopeMap
    {
      get
      {
        return this.mScopeMap;
      }
    }

    public GridMap<bool> SearchMap
    {
      get
      {
        return this.mSearchMap;
      }
    }

    public SkillMap SkillMap
    {
      get
      {
        return this.mSkillMap;
      }
    }

    public TrickMap TrickMap
    {
      get
      {
        return this.mTrickMap;
      }
    }

    private void ClearAI()
    {
      BattleMap currentMap = this.CurrentMap;
      if (this.mEnemyPriorities == null)
        this.mEnemyPriorities = new List<Unit>(this.mUnits.Count);
      this.mEnemyPriorities.Clear();
      if (this.mGimmickPriorities == null)
        this.mGimmickPriorities = new List<Unit>(this.mUnits.Count);
      this.mGimmickPriorities.Clear();
      int width = currentMap.Width;
      int height = currentMap.Height;
      if (this.mMoveMap == null)
        this.mMoveMap = new GridMap<int>(width, height);
      if (this.mMoveMap.w != width || this.mMoveMap.h != height)
        this.mMoveMap.resize(width, height);
      this.mMoveMap.fill(-1);
      if (this.mRangeMap == null)
        this.mRangeMap = new GridMap<bool>(width, height);
      if (this.mRangeMap.w != width || this.mRangeMap.h != height)
        this.mRangeMap.resize(width, height);
      this.mRangeMap.fill(false);
      if (this.mScopeMap == null)
        this.mScopeMap = new GridMap<bool>(width, height);
      if (this.mScopeMap.w != width || this.mScopeMap.h != height)
        this.mScopeMap.resize(width, height);
      this.mScopeMap.fill(false);
      if (this.mSearchMap == null)
        this.mSearchMap = new GridMap<bool>(width, height);
      if (this.mSearchMap.w != width || this.mSearchMap.h != height)
        this.mSearchMap.resize(width, height);
      this.mSearchMap.fill(false);
      if (this.mSafeMap == null)
        this.mSafeMap = new GridMap<int>(width, height);
      if (this.mSafeMap.w != width || this.mSafeMap.h != height)
        this.mSafeMap.resize(width, height);
      this.mSafeMap.fill(-1);
      if (this.mSafeMapEx == null)
        this.mSafeMapEx = new GridMap<int>(width, height);
      if (this.mSafeMapEx.w != width || this.mSafeMapEx.h != height)
        this.mSafeMapEx.resize(width, height);
      this.mSafeMapEx.fill(-1);
      this.mTrickMap.Initialize(width, height);
      this.mTrickMap.Clear();
      this.RefreshTreasureTargetAI();
    }

    private void ReleaseAi()
    {
    }

    public bool UpdateMapAI(bool forceAI)
    {
      return this.UpdateMapAI_Impl(forceAI);
    }

    private bool UpdateMapAI_Impl(bool forceAI)
    {
      DebugUtility.Assert(this.CurrentUnit != null, "CurrentUnit == null");
      Unit self = this.CurrentUnit;
      if (this.IsAutoBattle && self.Side == EUnitSide.Player)
        this.IsUseAutoPlayMode = true;
      if (self.AI != null)
      {
        if (self.AI.CheckFlag(AIFlags.DisableMove))
          self.SetUnitFlag(EUnitFlag.Moved, true);
        if (self.AI.CheckFlag(AIFlags.DisableAction))
          self.SetUnitFlag(EUnitFlag.Action, true);
      }
      if (self.IsUnitFlag(EUnitFlag.DamagedActionStart))
      {
        this.CommandWait(false);
        return false;
      }
      if (self.IsAIPatrolTable() && this.CalcMoveTargetAI(self, forceAI))
        return true;
      AIAction aiAction = this.mSkillMap.GetAction();
      if (aiAction != null && self.IsEnableActionCondition() && (string.IsNullOrEmpty((string) aiAction.skill) && (int) aiAction.type == 0))
      {
        this.CommandWait(false);
        this.mSkillMap.NextAction(false);
        return false;
      }
      if (!this.CalcSearchingAI(self))
        return false;
      this.GetEnemyPriorities(self, this.mEnemyPriorities, this.mGimmickPriorities);
      if (this.CheckEscapeAI(self))
        self.SetUnitFlag(EUnitFlag.Escaped, true);
      if (!self.IsUnitFlag(EUnitFlag.Action))
      {
        BattleCore.SkillResult result = this.mSkillMap.GetUseSkill();
        if (result != null)
        {
          if (this.UseSkillAI(self, result, forceAI))
          {
            this.mSkillMap.NextAction(result);
            return true;
          }
          self.SetUnitFlag(EUnitFlag.Action, true);
          this.mSkillMap.SetUseSkill((BattleCore.SkillResult) null);
        }
        else
        {
          this.mSkillMap.isNoExecActionSkill = false;
          Func<List<BattleCore.SkillResult>, bool> useskill = (Func<List<BattleCore.SkillResult>, bool>) (results =>
          {
            this.SortSkillResult(self, results);
            result = results[0];
            if (!this.UseSkillAI(self, result, forceAI))
              return false;
            this.mSkillMap.SetUseSkill(result);
            this.mSkillMap.NextAction(result);
            return true;
          });
          if (self.CastSkill == null)
          {
            if (aiAction != null)
            {
              AIAction action = aiAction;
              while (!this.CalcUseActionAI(self, action, useskill))
              {
                if (action.noExecAct == eAIActionNoExecAct.CHECK_NEXT && action.nextActIdx > 0)
                {
                  action = this.mSkillMap.SetAction(action.nextActIdx - 1);
                  if (action != null)
                  {
                    aiAction = action;
                    this.UpdateAIActionUseCondition(self);
                  }
                }
                else
                  action = (AIAction) null;
                if (action == null)
                {
                  this.mSkillMap.isNoExecActionSkill = true;
                  switch (aiAction.noExecAct)
                  {
                    case eAIActionNoExecAct.AI:
                    case eAIActionNoExecAct.USE_PROB_AND_AI:
                      this.RefreshUseSkillMap(self, true);
                      goto label_36;
                    default:
                      goto label_36;
                  }
                }
              }
              return !string.IsNullOrEmpty((string) action.skill) || (int) action.type != 0;
            }
label_36:
            List<SkillData> skillList = this.mSkillMap.GetSkillList();
            if (skillList != null)
            {
              if (this.CalcUseSkillsAI(self, skillList, useskill))
                return true;
              this.mSkillMap.NextSkillList();
              this.Log<LogMapCommand>();
              return true;
            }
          }
        }
      }
      else
        this.mSkillMap.SetUseSkill((BattleCore.SkillResult) null);
      if (this.CalcMoveTargetAI(self, forceAI))
      {
        this.mSkillMap.useSkillNum = 0;
        this.mSkillMap.NextAction(true);
        return true;
      }
      this.CommandWait(false);
      return false;
    }

    public GridMap<int> CreateMoveMap(Unit self, int movcount = 0)
    {
      BattleMap currentMap = this.CurrentMap;
      GridMap<int> movmap = new GridMap<int>(currentMap.Width, currentMap.Height);
      if (0 < movcount)
        this.UpdateMoveMap(self, movmap, movcount);
      else
        this.UpdateMoveMap(self, movmap);
      return movmap;
    }

    private void UpdateMoveMap(Unit self, GridMap<int> movmap, int movcount)
    {
      movmap.fill(-1);
      BattleMap currentMap = this.CurrentMap;
      Grid target = currentMap[self.x, self.y];
      currentMap.CalcMoveSteps(self, target, false);
      int num = movcount;
      for (int index1 = -num; index1 <= num; ++index1)
      {
        for (int index2 = -num; index2 <= num; ++index2)
        {
          if (Math.Abs(index2) + Math.Abs(index1) <= num)
          {
            Grid grid = currentMap[self.x + index2, self.y + index1];
            if (grid != null && grid.step != byte.MaxValue)
            {
              int src = Math.Max((int) grid.step - (int) target.step, 0);
              if (src <= num)
                movmap.set(grid.x, grid.y, src);
            }
          }
        }
      }
    }

    private void UpdateMoveMap(Unit self)
    {
      this.UpdateMoveMap(self, this.mMoveMap);
    }

    private void UpdateMoveMap(Unit self, GridMap<int> movmap)
    {
      int movcount = self.IsUnitFlag(EUnitFlag.Moved) ? 0 : self.GetMoveCount(false);
      this.UpdateMoveMap(self, movmap, movcount);
    }

    private void UpdateSafeMap(Unit self)
    {
      BattleMap currentMap = this.CurrentMap;
      int num = self.IsUnitFlag(EUnitFlag.Moved) ? 0 : self.GetMoveCount(false);
      this.mSafeMap.fill(-1);
      for (int index1 = 0; index1 < this.mUnits.Count; ++index1)
      {
        Unit mUnit = this.mUnits[index1];
        if (!mUnit.IsGimmick && !mUnit.IsDeadCondition() && (!mUnit.IsSub && mUnit.IsEntry) && this.CheckEnemySide(self, mUnit))
        {
          Grid self_grid;
          Grid target_grid;
          this.FindNearGridAndDistance(self, mUnit, out self_grid, out target_grid);
          if (currentMap.CalcMoveSteps(self, target_grid, false))
          {
            for (int index2 = -num; index2 <= num; ++index2)
            {
              for (int index3 = -num; index3 <= num; ++index3)
              {
                int x = self.x + index3;
                int y = self.y + index2;
                Grid grid = currentMap[x, y];
                if (grid != null && grid.step != byte.MaxValue)
                {
                  int src = this.mSafeMap.get(x, y) + (int) grid.step;
                  this.mSafeMap.set(x, y, src);
                }
              }
            }
          }
        }
      }
      this.mSafeMapEx.fill((int) byte.MaxValue);
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (!mUnit.IsGimmick && !mUnit.IsDeadCondition() && (!mUnit.IsSub && mUnit.IsEntry) && this.CheckEnemySide(self, mUnit))
        {
          Grid target = currentMap[mUnit.x, mUnit.y];
          currentMap.CalcMoveSteps(mUnit, target, false);
          for (int y = 0; y < currentMap.Height; ++y)
          {
            for (int x = 0; x < currentMap.Width; ++x)
            {
              Grid grid = currentMap[x, y];
              if (grid != null && grid.step != (byte) 127 && (int) grid.step < this.mSafeMapEx.get(x, y))
                this.mSafeMapEx.set(x, y, (int) grid.step);
            }
          }
          if (mUnit.CastSkill != null && mUnit.UnitTarget != self)
          {
            SkillData castSkill = mUnit.CastSkill;
            if (!castSkill.IsAllEffect() && !castSkill.IsHealSkill())
            {
              GridMap<bool> castSkillGridMap = mUnit.CastSkillGridMap;
              if (castSkillGridMap != null)
              {
                for (int y = 0; y < castSkillGridMap.h; ++y)
                {
                  for (int x = 0; x < castSkillGridMap.w; ++x)
                  {
                    if (castSkillGridMap.get(x, y))
                      this.mSafeMapEx.set(x, y, -1);
                  }
                }
              }
            }
          }
        }
      }
      List<IntVector2> intVector2List = new List<IntVector2>();
      for (int index1 = 0; index1 < this.mSafeMapEx.h; ++index1)
      {
        for (int index2 = 0; index2 < this.mSafeMapEx.w; ++index2)
        {
          if (this.mSafeMapEx.get(index2, index1) == (int) byte.MaxValue)
            intVector2List.Add(new IntVector2(index2, index1));
        }
      }
      for (int index1 = 0; index1 < this.mUnits.Count; ++index1)
      {
        Unit mUnit = this.mUnits[index1];
        if (!mUnit.IsGimmick && !mUnit.IsDeadCondition() && (!mUnit.IsSub && mUnit.IsEntry) && this.CheckEnemySide(self, mUnit))
        {
          Grid target = currentMap[mUnit.x, mUnit.y];
          currentMap.CalcMoveSteps(mUnit, target, true);
          for (int index2 = 0; index2 < intVector2List.Count; ++index2)
          {
            IntVector2 intVector2 = intVector2List[index2];
            Grid grid = currentMap[intVector2.x, intVector2.y];
            if (grid != null && grid.step != (byte) 127 && this.mSafeMapEx.get(intVector2.x, intVector2.y) > (int) grid.step)
              this.mSafeMapEx.set(intVector2.x, intVector2.y, (int) grid.step);
          }
        }
      }
    }

    private Grid GetSafePositionAI(Unit self)
    {
      int currentEnemyNum = this.GetCurrentEnemyNum(self);
      if (currentEnemyNum == 0)
        return (Grid) null;
      BattleMap currentMap = this.CurrentMap;
      if ((self.IsUnitFlag(EUnitFlag.Moved) ? 0 : self.GetMoveCount(false)) == 0)
        return (Grid) null;
      bool flag = self.AI != null && self.AI.CheckFlag(AIFlags.CastSkillFriendlyFire);
      Grid start = currentMap[self.x, self.y];
      Grid goal = currentMap[self.x, self.y];
      int num1 = Math.Max(this.mSafeMap.get(start.x, start.y) + (self.AI == null ? 0 : (int) self.AI.safe_border * currentEnemyNum), 0);
      for (int x = 0; x < this.mSafeMap.w; ++x)
      {
        for (int y = 0; y < this.mSafeMap.h; ++y)
        {
          if (this.mMoveMap.get(x, y) >= 0 && this.mSafeMap.get(x, y) >= num1)
          {
            Grid grid = currentMap[x, y];
            if ((!flag || !this.CheckFriendlyFireOnGridMap(self, grid)) && this.CheckMove(self, grid))
            {
              if (goal != null)
              {
                int num2 = this.CalcGridDistance(start, goal);
                if (this.CalcGridDistance(start, grid) < num2)
                  goal = grid;
              }
              else
                goal = grid;
            }
          }
        }
      }
      return goal;
    }

    private bool GetSafePositionEx(Unit self, GridMap<bool> rangeMap, ref BattleCore.SVector2 result)
    {
      int num1 = int.MinValue;
      bool flag = false;
      for (int x = 0; x < rangeMap.w; ++x)
      {
        for (int y = 0; y < rangeMap.h; ++y)
        {
          if (rangeMap.get(x, y))
          {
            int num2 = this.mSafeMapEx.get(x, y);
            if (num2 != (int) byte.MaxValue && num2 > num1)
            {
              num1 = num2;
              result.x = x;
              result.y = y;
              flag = true;
            }
          }
        }
      }
      return flag;
    }

    private bool GetSafePositionEx(Unit self, List<Grid> move_targets, ref Grid result)
    {
      int num1 = int.MinValue;
      bool flag = false;
      BattleMap currentMap = this.CurrentMap;
      for (int index = 0; index < move_targets.Count; ++index)
      {
        Grid moveTarget = move_targets[index];
        if (currentMap.CheckEnableMove(self, moveTarget, false, false))
        {
          int num2 = this.mSafeMapEx.get(moveTarget.x, moveTarget.y);
          if (num2 != (int) byte.MaxValue && num2 > num1)
          {
            num1 = num2;
            result = moveTarget;
            flag = true;
          }
        }
      }
      return flag;
    }

    private int GetSafeValue(Unit self, Grid target)
    {
      int num1 = this.mSafeMapEx.get(target.x, target.y);
      int num2 = 1;
      BattleMap currentMap = this.CurrentMap;
      Grid[] gridArray = new Grid[4]{ currentMap[target.x - 1, target.y], currentMap[target.x + 1, target.y], currentMap[target.x, target.y + 1], currentMap[target.x, target.y - 1] };
      foreach (Grid grid in gridArray)
      {
        if (grid != null && currentMap.CheckEnableMove(self, grid, false, false))
        {
          ++num2;
          num1 += this.mSafeMapEx.get(grid.x, grid.y);
        }
      }
      return num1 * 10 / num2;
    }

    public void InitSkillMap(Unit self)
    {
      this.mSkillMap.Clear();
      this.mSkillMap.owner = self;
      this.mSkillMap.skillSeed = this.mRand.GetSeed();
      this.mSkillMap.SetAction(self.GetCurrentAIAction());
      List<AbilityData> battleAbilitys = self.BattleAbilitys;
      for (int index1 = 0; index1 < battleAbilitys.Count; ++index1)
      {
        AbilityData abilityData = battleAbilitys[index1];
        if (abilityData != null)
        {
          for (int index2 = 0; index2 < abilityData.Skills.Count; ++index2)
          {
            SkillData skill = abilityData.Skills[index2];
            if (skill != null && (abilityData.AbilityType == EAbilityType.Active || skill.Timing != ESkillTiming.Used))
              this.mSkillMap.allSkills.Add(skill);
          }
        }
      }
      if (self.GetAttackSkill() != null)
        this.mSkillMap.allSkills.Add(self.GetAttackSkill());
      if (self.AIForceSkill != null)
        this.mSkillMap.allSkills.Add(self.AIForceSkill);
      this.RefreshUseSkillMap(self, false);
    }

    private void UpdateSkillMap(Unit self, List<SkillData> skills)
    {
      this.mSkillMap.Reset();
      BattleMap currentMap = this.CurrentMap;
      IntVector2 intVector2 = new IntVector2(self.x, self.y);
      for (int index1 = 0; index1 < skills.Count; ++index1)
      {
        SkillData skillData = (SkillData) null;
        for (int index2 = 0; index2 < this.mSkillMap.allSkills.Count; ++index2)
        {
          if (this.mSkillMap.allSkills[index2].SkillID == skills[index1].SkillID)
          {
            skillData = this.mSkillMap.allSkills[index2];
            break;
          }
        }
        if (skillData != null)
        {
          if (self.Gems >= self.GetSkillUsedCost(skillData) && this.mSkillMap.attackHeight < skillData.EnableAttackGridHeight)
            this.mSkillMap.attackHeight = skillData.EnableAttackGridHeight;
          this.mSkillMap.Add(new SkillMap.Data(skillData));
        }
      }
      this.SetBattleFlag(EBattleFlag.ComputeAI, true);
      List<SkillMap.Data> list = this.mSkillMap.list;
      bool is_friendlyfire = self.AI != null && self.AI.CheckFlag(AIFlags.CastSkillFriendlyFire);
      List<Grid> enableMoveGridList = this.GetEnableMoveGridList(self, true, is_friendlyfire, false, true, false);
      for (int index1 = 0; index1 < enableMoveGridList.Count; ++index1)
      {
        Grid grid = enableMoveGridList[index1];
        if (currentMap.CheckEnableMove(self, grid, false, false))
        {
          self.x = grid.x;
          self.y = grid.y;
          self.RefleshMomentBuff(false, -1, -1);
          for (int index2 = 0; index2 < list.Count; ++index2)
          {
            SkillMap.Data data = list[index2];
            SkillData skill = data.skill;
            SkillMap.Target range = new SkillMap.Target();
            range.pos = new IntVector2(self.x, self.y);
            range.scores = new Dictionary<int, SkillMap.Score>();
            GridMap<bool> gridMap = (GridMap<bool>) null;
            if (skill.TeleportType != eTeleportType.AfterSkill)
            {
              if (skill.SkillParam.select_scope == ESelectType.All || skill.SkillParam.target == ESkillTarget.Self)
              {
                if (intVector2.x == self.x && intVector2.y == self.y || this.mTrickMap.IsGoodData(self.x, self.y))
                  gridMap = (GridMap<bool>) null;
                else
                  continue;
              }
              else
                gridMap = this.CreateSelectGridMapAI(self, self.x, self.y, skill);
            }
            if (gridMap == null)
            {
              SkillMap.Score score = new SkillMap.Score(self.x, self.y, currentMap.Width, currentMap.Height);
              if (this.SetupSkillMapScore(self, grid, skill, score))
                range.Add(score);
            }
            else
            {
              for (int index3 = 0; index3 < gridMap.w; ++index3)
              {
                for (int index4 = 0; index4 < gridMap.h; ++index4)
                {
                  if (gridMap.get(index3, index4))
                  {
                    SkillMap.Score score = new SkillMap.Score(index3, index4, currentMap.Width, currentMap.Height);
                    if (this.SetupSkillMapScore(self, grid, skill, score))
                      range.Add(score);
                  }
                }
              }
            }
            data.Add(range);
          }
        }
      }
      self.x = intVector2.x;
      self.y = intVector2.y;
      self.RefleshMomentBuff(false, -1, -1);
      this.SetBattleFlag(EBattleFlag.ComputeAI, false);
    }

    private bool SetupSkillMapScore(Unit self, Grid goal, SkillData skill, SkillMap.Score score)
    {
      BattleMap currentMap = this.CurrentMap;
      int x1 = score.pos.x;
      int y1 = score.pos.y;
      BattleCore.ShotTarget shot = (BattleCore.ShotTarget) null;
      List<Unit> targets = new List<Unit>();
      IntVector2 gridForSkillRange = this.GetValidGridForSkillRange(self, self.x, self.y, skill, x1, y1);
      this.GetExecuteSkillLineTarget(self, gridForSkillRange.x, gridForSkillRange.y, skill, ref targets, ref shot);
      if (skill.IsAreaSkill())
      {
        for (int y2 = 0; y2 < this.mScopeMap.h; ++y2)
        {
          for (int x2 = 0; x2 < this.mScopeMap.w; ++x2)
          {
            if (this.mScopeMap.get(x2, y2))
              score.range.Set(x2, y2);
          }
        }
      }
      else
        score.range.Set(x1, y1);
      if (!skill.IsTrickSkill())
      {
        for (int index = 0; index < targets.Count; ++index)
        {
          Unit target = targets[index];
          if (target != null && target.IsBreakObj && !this.IsTargetBreakUnitAI(self, target))
          {
            targets.RemoveAt(index);
            --index;
          }
        }
        if (targets.Count > 0)
        {
          this.SetBattleFlag(EBattleFlag.PredictResult, true);
          this.mRandDamage.Seed(this.mSeedDamage);
          this.CurrentRand = this.mRandDamage;
          LogSkill log = new LogSkill();
          log.self = self;
          log.skill = skill;
          log.pos.x = x1;
          log.pos.y = y1;
          log.reflect = (LogSkill.Reflection) null;
          for (int index = 0; index < targets.Count; ++index)
            log.SetSkillTarget(self, targets[index]);
          if (shot != null)
          {
            log.pos.x = shot.end.x;
            log.pos.y = shot.end.y;
            log.rad = (int) (shot.rad * 100.0);
            log.height = (int) (shot.height * 100.0);
          }
          this.ExecuteSkill(ESkillTiming.Used, log, skill);
          self.x = goal.x;
          self.y = goal.y;
          this.CurrentRand = this.mRand;
          self.SetUnitFlag(EUnitFlag.SideAttack, false);
          self.SetUnitFlag(EUnitFlag.BackAttack, false);
          this.SetBattleFlag(EBattleFlag.PredictResult, false);
          if (skill.TeleportType != eTeleportType.None && (log.TeleportGrid == null || !currentMap.CheckEnableMove(self, log.TeleportGrid, false, false)))
            return false;
          score.log = log;
        }
      }
      return true;
    }

    public BattleCore.SkillResult GetSkillResult(BattleCore.AiCache cache, Unit self, SkillData skill, SkillMap.Score score)
    {
      BattleCore.SkillResult skillResult = (BattleCore.SkillResult) null;
      if (this.IsEnableUseSkillEffect(self, skill, score.log))
      {
        LogSkill log = score.log;
        skillResult = new BattleCore.SkillResult()
        {
          skill = skill,
          movpos = cache.map[self.x, self.y],
          usepos = cache.map[score.pos.x, score.pos.y]
        };
        skillResult.locked = this.FindUnitAtGrid(skillResult.usepos) != null;
        skillResult.cond_prio = cache.cond_prio;
        skillResult.cost_jewel = cache.cost_jewel;
        skillResult.buff_prio = (int) byte.MaxValue;
        skillResult.buff_dup = (int) byte.MaxValue;
        if (log != null)
        {
          skillResult.log = log;
          skillResult.heal = log.GetTruthTotalHpHeal();
          skillResult.heal_num = log.GetTruthTotalHpHealCount();
          skillResult.cure_num = log.GetTotalCureConditionCount();
          skillResult.fail_num = log.GetTotalFailConditionCount();
          skillResult.disable_num = log.GetTotalDisableConditionCount();
          skillResult.gain_jewel = log.GetGainJewel();
          if (this.isKnockBack(skill))
          {
            for (int index = 0; index < log.targets.Count; ++index)
            {
              LogSkill.Target target1 = log.targets[index];
              Unit target2 = target1.target;
              if (!this.IsFailTrickData(target2, target2.x, target2.y))
              {
                Grid knockBackGrid = target1.KnockBackGrid;
                if (knockBackGrid != null)
                {
                  if (this.IsGoodTrickData(target2, target2.x, target2.y) && !this.IsGoodTrickData(target2, knockBackGrid.x, knockBackGrid.y))
                    ++skillResult.nockback_prio;
                  if (this.IsFailTrickData(target2, knockBackGrid.x, knockBackGrid.y))
                    ++skillResult.nockback_prio;
                }
              }
            }
          }
          for (int index = 0; index < log.targets.Count; ++index)
          {
            LogSkill.Target target1 = log.targets[index];
            Unit target2 = target1.target;
            int totalHpDamage = target1.GetTotalHpDamage();
            if (target2.IsBreakObj)
            {
              skillResult.ext_damage += Math.Max(Math.Min(totalHpDamage, (int) target2.CurrentStatus.param.hp), 0);
              skillResult.ext_dead_num += totalHpDamage <= (int) target2.CurrentStatus.param.hp ? 0 : 1;
            }
            else
            {
              skillResult.unit_damage_t += totalHpDamage;
              skillResult.unit_damage += Math.Max(Math.Min(totalHpDamage, (int) target2.CurrentStatus.param.hp), 0);
              skillResult.unit_dead_num += totalHpDamage <= (int) target2.CurrentStatus.param.hp ? 0 : 1;
            }
            int buffPriority = target2.GetBuffPriority(skill, SkillEffectTargets.Target);
            skillResult.buff_prio = Math.Max(Math.Min(buffPriority, skillResult.buff_prio), 0);
          }
          log.GetTotalBuffEffect(out skillResult.buff_num, out skillResult.buff);
          if (skill.DuplicateCount > 1)
          {
            for (int index = 0; index < log.targets.Count; ++index)
            {
              int val2 = 0;
              if (log.targets[index].target.BuffAttachments.Count > 0)
              {
                foreach (BuffAttachment buffAttachment in log.targets[index].target.BuffAttachments)
                {
                  if (buffAttachment.skill != null && buffAttachment.skill.SkillID == skill.SkillID)
                    ++val2;
                }
              }
              skillResult.buff_dup = Math.Min(skillResult.buff_dup, val2);
            }
          }
        }
        if (skillResult.buff_prio == (int) byte.MaxValue)
          skillResult.buff_prio = self.GetBuffPriority(skill, SkillEffectTargets.Self);
        if (skill.TeleportType == eTeleportType.BeforeSkill && !skill.IsTargetTeleport && (cache.baseRangeMap != null && cache.baseRangeMap.get(skillResult.usepos.x, skillResult.usepos.y)))
          skillResult.movpos = cache.map[cache.pos.x, cache.pos.y];
        skillResult.distance = this.CalcGridDistance(skillResult.usepos, skillResult.movpos);
        skillResult.score_prio = score.priority;
        skillResult.fail_trick = this.GetFailTrickPriority(self, skillResult.movpos);
        skillResult.good_trick = this.GetGoodTrickPriority(self, skillResult.movpos, ref skillResult.heal_trick);
        skillResult.unit_prio = this.GetSkillTargetsHighestPriority(self, skill, log);
        skillResult.ct = 0;
        if (self.x != cache.pos.x || self.y != cache.pos.y)
          skillResult.ct -= (int) cache.fixparam.ChargeTimeDecMove;
        skillResult.ct -= (int) cache.fixparam.ChargeTimeDecWait;
        skillResult.ct -= (int) cache.fixparam.ChargeTimeDecAction;
        if (skill.TeleportType == eTeleportType.AfterSkill)
        {
          GridMap<bool> selectGridMapAi = this.CreateSelectGridMapAI(self, self.x, self.y, skill);
          BattleCore.SVector2 result = new BattleCore.SVector2(skillResult.movpos.x, skillResult.movpos.y);
          this.GetSafePositionEx(self, selectGridMapAi, ref result);
          skillResult.usepos = cache.map[result.x, result.y];
          skillResult.locked = false;
          skillResult.distance = this.CalcGridDistance(skillResult.usepos, skillResult.movpos);
        }
        if (skill.IsTargetTeleport)
        {
          Grid teleportGrid = log.TeleportGrid;
          if (teleportGrid != null)
            skillResult.teleport = this.GetSafeValue(self, teleportGrid);
        }
      }
      return skillResult;
    }

    public void SortSkillResult(Unit self, List<BattleCore.SkillResult> results)
    {
      bool bPositioning = self.AI != null && self.AI.CheckFlag(AIFlags.Positioning);
      MySort<BattleCore.SkillResult>.Sort(results, (Comparison<BattleCore.SkillResult>) ((src, dsc) =>
      {
        if (src == dsc)
          return 0;
        if (dsc.score_prio != src.score_prio)
          return dsc.score_prio - src.score_prio;
        if (dsc.fail_trick != src.fail_trick)
          return src.fail_trick - dsc.fail_trick;
        if (dsc.heal_trick != src.heal_trick)
          return dsc.heal_trick - src.heal_trick;
        if (dsc.unit_prio != src.unit_prio)
          return src.unit_prio - dsc.unit_prio;
        if (src.skill.IsDamagedSkill())
        {
          if (dsc.unit_dead_num != src.unit_dead_num)
            return dsc.unit_dead_num - src.unit_dead_num;
          if (dsc.nockback_prio != src.nockback_prio)
            return dsc.nockback_prio - src.nockback_prio;
          if (dsc.unit_damage != src.unit_damage && self.AI != null && Math.Abs(dsc.unit_damage - src.unit_damage) > (int) self.AI.gosa_border)
            return dsc.unit_damage - src.unit_damage;
          if (dsc.unit_dead_num == 0)
          {
            if (dsc.cond_prio != src.cond_prio)
              return src.cond_prio - dsc.cond_prio;
            if (dsc.fail_num != src.fail_num)
              return dsc.fail_num - src.fail_num;
            if (dsc.buff_prio != src.buff_prio)
              return src.buff_prio - dsc.buff_prio;
            if (dsc.buff != src.buff)
              return dsc.buff - src.buff;
            if (dsc.buff_num != src.buff_num)
              return dsc.buff_num - src.buff_num;
          }
          if (dsc.ext_dead_num != src.ext_dead_num)
            return dsc.ext_dead_num - src.ext_dead_num;
          if (dsc.ext_damage != src.ext_damage && self.AI != null && Math.Abs(dsc.ext_damage - src.ext_damage) > (int) self.AI.gosa_border)
            return dsc.ext_damage - src.ext_damage;
        }
        else if (src.skill.IsHealSkill())
        {
          if (dsc.heal != src.heal)
            return dsc.heal - src.heal;
          if (dsc.heal_num != src.heal_num)
            return dsc.heal_num - src.heal_num;
        }
        else if (src.skill.IsSupportSkill())
        {
          if (dsc.buff_prio != src.buff_prio)
            return src.buff_prio - dsc.buff_prio;
          if (dsc.buff != src.buff)
            return dsc.buff - src.buff;
          if (dsc.buff_num != src.buff_num)
            return dsc.buff_num - src.buff_num;
          if (dsc.buff_dup != src.buff_dup)
            return src.buff_dup - dsc.buff_dup;
        }
        else if (src.skill.EffectType == SkillEffectTypes.CureCondition)
        {
          if (dsc.cond_prio != src.cond_prio)
            return src.cond_prio - dsc.cond_prio;
          if (dsc.cure_num != src.cure_num)
            return dsc.cure_num - src.cure_num;
        }
        else if (src.skill.EffectType == SkillEffectTypes.FailCondition)
        {
          if (dsc.cond_prio != src.cond_prio)
            return src.cond_prio - dsc.cond_prio;
          if (dsc.fail_num != src.fail_num)
            return dsc.fail_num - src.fail_num;
        }
        else if (src.skill.EffectType == SkillEffectTypes.DisableCondition)
        {
          if (dsc.cond_prio != src.cond_prio)
            return src.cond_prio - dsc.cond_prio;
          if (dsc.disable_num != src.disable_num)
            return dsc.disable_num - src.disable_num;
        }
        if (src.teleport != dsc.teleport)
          return dsc.teleport - src.teleport;
        if (dsc.good_trick != src.good_trick)
          return dsc.good_trick - src.good_trick;
        if (dsc.cost_jewel != src.cost_jewel)
          return src.cost_jewel - dsc.cost_jewel;
        if (src.skill == dsc.skill && src.skill.IsCastSkill() && (src.skill.IsAreaSkill() && src.skill.IsEnableUnitLockTarget()) && (src.skill.Target != ESkillTarget.UnitAll && src.skill.Target != ESkillTarget.NotSelf))
        {
          if (dsc.locked && !src.locked)
            return 1;
          if (src.locked && !dsc.locked)
            return -1;
        }
        if (dsc.ct != src.ct)
          return dsc.ct - src.ct;
        if (dsc.distance == src.distance)
        {
          if (bPositioning && dsc.movpos.height != src.movpos.height)
            return dsc.movpos.height - src.movpos.height;
          if (src.skill.IsNormalAttack() && dsc.skill.IsNormalAttack() && dsc.gain_jewel != src.gain_jewel)
            return dsc.gain_jewel - src.gain_jewel;
        }
        return dsc.distance - src.distance;
      }));
    }

    private void RefreshUseSkillMap(Unit self, bool is_add_act = false)
    {
      AIParam ai = self.AI;
      bool flag1 = true;
      if (!self.IsEnableSkillCondition(false) || this.CheckDisableAbilities(self))
        flag1 = false;
      if (this.mQuestParam.CheckAllowedAutoBattle() && self.IsEnableAutoMode() && GameUtility.Config_AutoMode_DisableSkill.Value)
        flag1 = false;
      if (ai != null && ai.CheckFlag(AIFlags.DisableSkill))
        flag1 = false;
      if (is_add_act)
      {
        if (this.mSkillMap.useSkillLists != null)
          this.mSkillMap.useSkillLists.Clear();
        if (this.mSkillMap.forceSkillList != null)
          this.mSkillMap.forceSkillList.Clear();
        if (this.mSkillMap.healSkills != null)
          this.mSkillMap.healSkills.Clear();
        if (this.mSkillMap.damageSkills != null)
          this.mSkillMap.damageSkills.Clear();
        if (this.mSkillMap.supportSkills != null)
          this.mSkillMap.supportSkills.Clear();
        if (this.mSkillMap.cureConditionSkills != null)
          this.mSkillMap.cureConditionSkills.Clear();
        if (this.mSkillMap.failConditionSkills != null)
          this.mSkillMap.failConditionSkills.Clear();
        if (this.mSkillMap.disableConditionSkills != null)
          this.mSkillMap.disableConditionSkills.Clear();
        if (this.mSkillMap.transformSkills != null)
          this.mSkillMap.transformSkills.Clear();
        if (this.mSkillMap.exeSkills != null)
          this.mSkillMap.exeSkills.Clear();
      }
      bool is_no_add_rate = false;
      if (flag1)
      {
        int gems = this.GetGems(self);
        bool flag2 = true;
        if (self.IsAIActionTable())
        {
          AIAction action = this.mSkillMap.GetAction();
          if (action != null)
          {
            if (is_add_act && action.noExecAct != eAIActionNoExecAct.NONE)
            {
              switch (action.noExecAct)
              {
                case eAIActionNoExecAct.AI:
                case eAIActionNoExecAct.USE_PROB_AND_AI:
                  flag2 = true;
                  if (action.noExecAct == eAIActionNoExecAct.AI)
                  {
                    is_no_add_rate = true;
                    break;
                  }
                  break;
                default:
                  flag2 = (int) action.type == 2;
                  break;
              }
            }
            else
              flag2 = (int) action.type == 2;
          }
          else
            flag2 = false;
        }
        if (flag2)
        {
          if (gems >= (int) ai.gems_border)
          {
            for (int index = 0; index < self.BattleSkills.Count; ++index)
            {
              SkillData skill = self.GetSkillForUseCount(self.BattleSkills[index].SkillID, 0) ?? self.BattleSkills[index];
              this.EntryUseSkill(self, skill, false, is_no_add_rate);
            }
          }
          this.EntryUseSkill(self, self.AIForceSkill, true, is_no_add_rate);
        }
      }
      this.EntryUseSkill(self, self.GetAttackSkill(), this.mSkillMap.forceSkillList.Count > 0, is_no_add_rate);
      List<List<SkillData>> useSkillLists = this.mSkillMap.useSkillLists;
      if (ai != null && ai.SkillCategoryPriorities != null)
      {
        for (int index1 = 0; index1 < ai.SkillCategoryPriorities.Length; ++index1)
        {
          List<SkillData> skillDataList = (List<SkillData>) null;
          switch (ai.SkillCategoryPriorities[index1])
          {
            case SkillCategory.Damage:
              skillDataList = this.mSkillMap.damageSkills;
              goto default;
            case SkillCategory.Heal:
              if (this.GetHealUnitCount(self) != 0)
              {
                skillDataList = this.mSkillMap.healSkills;
                goto default;
              }
              else
                goto default;
            case SkillCategory.Support:
              if (ai.CheckFlag(AIFlags.SelfBuffOnly))
              {
                bool flag2 = false;
                for (int index2 = 0; index2 < self.BuffAttachments.Count; ++index2)
                {
                  if (!(bool) self.BuffAttachments[index2].IsPassive && self.BuffAttachments[index2].BuffType == BuffTypes.Buff && self.BuffAttachments[index2].user == self)
                  {
                    flag2 = true;
                    break;
                  }
                }
                if (flag2)
                  break;
              }
              skillDataList = this.mSkillMap.supportSkills;
              goto default;
            case SkillCategory.CureCondition:
              skillDataList = this.mSkillMap.cureConditionSkills;
              goto default;
            case SkillCategory.FailCondition:
              skillDataList = this.mSkillMap.failConditionSkills;
              goto default;
            case SkillCategory.DisableCondition:
              skillDataList = this.mSkillMap.disableConditionSkills;
              goto default;
            case SkillCategory.Transform:
              skillDataList = this.mSkillMap.transformSkills;
              goto default;
            default:
              if (skillDataList == null || !useSkillLists.Contains(skillDataList))
              {
                if (skillDataList == null)
                {
                  useSkillLists.Add(new List<SkillData>());
                  break;
                }
                useSkillLists.Add(skillDataList);
                break;
              }
              break;
          }
        }
        if (this.mSkillMap.exeSkills.Count <= 0)
          return;
        useSkillLists.Add(this.mSkillMap.exeSkills);
      }
      else
        useSkillLists.Add(this.mSkillMap.damageSkills);
    }

    private bool EntryUseSkill(Unit self, SkillData skill, bool forced, bool is_no_add_rate)
    {
      if (skill == null || skill.SkillType != ESkillType.Attack && skill.SkillType != ESkillType.Skill && skill.SkillType != ESkillType.Item || (skill.Timing != ESkillTiming.Used || skill.EffectType == SkillEffectTypes.Throw || (skill.IsSetBreakObjSkill() || !this.CheckEnableUseSkill(self, skill, false))))
        return false;
      if (forced)
      {
        this.mSkillMap.forceSkillList.Add(skill);
        return true;
      }
      if (!this.CheckUseSkill(self, skill, is_no_add_rate))
        return false;
      List<SkillData> skillDataList = (List<SkillData>) null;
      if (skill.IsDamagedSkill())
        skillDataList = this.mSkillMap.damageSkills;
      else if (skill.IsHealSkill())
        skillDataList = this.mSkillMap.healSkills;
      else if (skill.IsSupportSkill())
        skillDataList = this.mSkillMap.supportSkills;
      else if (skill.EffectType == SkillEffectTypes.CureCondition)
        skillDataList = this.mSkillMap.cureConditionSkills;
      else if (skill.EffectType == SkillEffectTypes.FailCondition)
        skillDataList = this.mSkillMap.failConditionSkills;
      else if (skill.EffectType == SkillEffectTypes.DisableCondition)
        skillDataList = this.mSkillMap.disableConditionSkills;
      else if (skill.EffectType == SkillEffectTypes.DynamicTransformUnit)
        skillDataList = this.mSkillMap.transformSkills;
      else if (skill.TeleportType != eTeleportType.None)
        skillDataList = this.mSkillMap.exeSkills;
      if (skillDataList == null)
        return false;
      skillDataList.Add(skill);
      return true;
    }

    private bool CheckUseSkill(Unit self, SkillData skill, bool is_no_add_rate)
    {
      if (this.QuestType == QuestTypes.Arena)
        return (int) (this.GetRandom() % 100U) >= (int) skill.SkillParam.rate;
      if (skill.IsNormalAttack())
        return true;
      if (skill.IsDamagedSkill())
      {
        if (skill.IsJewelAttack() && self.AI != null && self.AI.CheckFlag(AIFlags.DisableJewelAttack))
          return false;
      }
      else
      {
        if (self.GetRageTarget() != null)
          return false;
        bool flag1 = false;
        if (!skill.IsHealSkill())
        {
          if (skill.IsSupportSkill())
            flag1 = true;
          else if (!skill.IsTrickSkill())
          {
            if (skill.EffectType == SkillEffectTypes.CureCondition)
            {
              CondEffect condEffect = skill.GetCondEffect(SkillEffectTargets.Target);
              if (condEffect == null || condEffect.param == null || (condEffect.param.conditions == null || condEffect.param.type != ConditionEffectTypes.CureCondition))
                return false;
              bool flag2 = false;
              for (int index = 0; index < condEffect.param.conditions.Length; ++index)
              {
                if (this.GetFailCondSelfSideUnitCount(self, condEffect.param.conditions[index]) != 0)
                {
                  flag2 = true;
                  break;
                }
              }
              if (!flag2)
                return false;
            }
            else if (skill.EffectType != SkillEffectTypes.FailCondition && skill.EffectType == SkillEffectTypes.DisableCondition)
            {
              CondEffect condEffect = skill.GetCondEffect(SkillEffectTargets.Target);
              if (condEffect == null || condEffect.param == null || (condEffect.param.conditions == null || condEffect.param.type != ConditionEffectTypes.DisableCondition))
                return false;
              bool flag2 = false;
              for (int index = 0; index < condEffect.param.conditions.Length; ++index)
              {
                EUnitCondition condition = condEffect.param.conditions[index];
                if (this.IsFailCondSkillUseEnemies(self, condition))
                {
                  flag2 = true;
                  break;
                }
              }
              if (!flag2)
                return false;
            }
          }
        }
        if (flag1)
        {
          bool flag2 = false;
          bool flag3 = true;
          if ((int) self.AI.DisableSupportActionHpBorder != 0)
          {
            int num = (int) self.MaximumStatus.param.hp == 0 ? 100 : 100 * (int) self.CurrentStatus.param.hp / (int) self.MaximumStatus.param.hp;
            flag2 = true;
            flag3 &= (int) self.AI.DisableSupportActionHpBorder >= num;
          }
          if ((int) self.AI.DisableSupportActionMemberBorder != 0)
          {
            int aliveUnitCount = this.GetAliveUnitCount(self);
            flag2 = true;
            flag3 &= (int) self.AI.DisableSupportActionMemberBorder >= aliveUnitCount;
          }
          if (flag2 && flag3)
            return false;
        }
      }
      if (self.IsPartyMember)
        return (int) (this.GetRandom() % 100U) >= (int) skill.SkillParam.rate;
      return (skill.UseCondition == null || skill.UseCondition.type == 0 || skill.UseCondition.unlock) && (is_no_add_rate || (int) (this.GetRandom() % 100U) < (int) skill.UseRate);
    }

    public bool CalcUseActionAI(Unit self, AIAction action, Func<List<BattleCore.SkillResult>, bool> useskill)
    {
      if (action == null)
        return false;
      List<SkillData> skills = (List<SkillData>) null;
      if (string.IsNullOrEmpty((string) action.skill))
      {
        switch ((int) action.type)
        {
          case 1:
            SkillData attackSkill = self.GetAttackSkill();
            if (attackSkill != null && this.CheckEnableUseSkill(self, attackSkill, false))
            {
              skills = new List<SkillData>();
              skills.Add(attackSkill);
              break;
            }
            break;
          case 2:
            break;
          default:
            this.CommandWait(false);
            this.mSkillMap.NextAction(false);
            return true;
        }
      }
      else
      {
        bool flag = true;
        if (action.cond != null)
          flag = action.cond.unlock;
        if (flag)
        {
          SkillData skillData = self.BattleSkills.Find((Predicate<SkillData>) (p => p.SkillID == (string) action.skill));
          SkillData skill = self.GetSkillForUseCount((string) action.skill, 0) ?? skillData;
          if (this.CheckEnableUseSkill(self, skill, false))
          {
            skills = new List<SkillData>();
            skills.Add(skill);
          }
        }
      }
      return skills != null && this.CalcUseSkillsAI(self, skills, useskill);
    }

    public bool CalcUseSkillsAI(Unit self, List<SkillData> skills, Func<List<BattleCore.SkillResult>, bool> useskill)
    {
      if (skills.Count == 0)
        return false;
      this.UpdateSkillMap(self, skills);
      BattleCore.mSkillResults.Clear();
      for (int index = 0; index < skills.Count; ++index)
      {
        if (skills[index] != null)
          this.GetUsedSkillResultAllEx(self, skills[index], BattleCore.mSkillResults);
      }
      return BattleCore.mSkillResults.Count > 0 && useskill(BattleCore.mSkillResults);
    }

    private bool UseSkillAI(Unit self, BattleCore.SkillResult result, bool forceAI)
    {
      if (self == null || result == null)
        return false;
      if ((self.x != result.movpos.x || self.y != result.movpos.y) && this.Move(self, result.movpos, forceAI))
        return true;
      bool bUnitLockTarget = false;
      if (result.skill.IsCastSkill() && result.skill.IsEnableUnitLockTarget())
      {
        if (result.skill.IsAreaSkill())
        {
          if (result.skill.Target != ESkillTarget.UnitAll && result.skill.Target != ESkillTarget.NotSelf)
            bUnitLockTarget = result.locked;
        }
        else
          bUnitLockTarget = result.locked;
      }
      return this.UseSkill(self, result.usepos.x, result.usepos.y, result.skill, bUnitLockTarget, 0, 0, false);
    }

    public bool CheckFriendlyFireOnGridMap(Unit self, Grid grid)
    {
      for (int index = 0; index < this.Units.Count; ++index)
      {
        Unit unit = this.Units[index];
        if (unit.CastSkill != null && !unit.CastSkill.IsAllEffect() && (unit.UnitTarget != self && unit.CastSkill.IsDamagedSkill()) && (this.CheckSkillTarget(unit, self, unit.CastSkill) && unit.CastSkillGridMap != null && unit.CastSkillGridMap.get(grid.x, grid.y)))
          return true;
      }
      return false;
    }

    private bool CheckSkillTargetAI(Unit self, Unit target, SkillData skill)
    {
      if (!this.CheckSkillTarget(self, target, skill))
        return false;
      bool flag = false;
      switch (skill.Target)
      {
        case ESkillTarget.UnitAll:
        case ESkillTarget.NotSelf:
          flag = true;
          break;
        case ESkillTarget.GridNoUnit:
          if (skill.TeleportType != eTeleportType.None)
          {
            switch (skill.TeleportTarget)
            {
              case ESkillTarget.UnitAll:
              case ESkillTarget.NotSelf:
                flag = true;
                break;
            }
          }
          else
            break;
      }
      if (flag)
      {
        switch (skill.EffectType)
        {
          case SkillEffectTypes.Heal:
          case SkillEffectTypes.Buff:
          case SkillEffectTypes.Revive:
          case SkillEffectTypes.Shield:
          case SkillEffectTypes.CureCondition:
          case SkillEffectTypes.GemsGift:
          case SkillEffectTypes.Guard:
          case SkillEffectTypes.RateHeal:
            return !this.CheckEnemySide(self, target);
          case SkillEffectTypes.GemsIncDec:
            if ((int) skill.EffectValue > 0)
              return !this.CheckEnemySide(self, target);
            return this.CheckEnemySide(self, target);
          case SkillEffectTypes.Teleport:
          case SkillEffectTypes.Changing:
          case SkillEffectTypes.Throw:
            break;
          default:
            return this.CheckEnemySide(self, target);
        }
      }
      return true;
    }

    private void GetUsedSkillResultAllEx(Unit self, SkillData skill, List<BattleCore.SkillResult> results)
    {
      if (skill == null || results == null)
        return;
      bool flag = !self.IsUnitFlag(EUnitFlag.Moved);
      if (skill.TeleportType == eTeleportType.Only)
      {
        if (flag)
          return;
        this.GetUsedTeleportSkillResult(self, skill, results);
      }
      else
      {
        BattleCore.AiCache cache = new BattleCore.AiCache();
        cache.map = this.CurrentMap;
        cache.fixparam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
        cache.cost_jewel = self.GetSkillUsedCost(skill);
        cache.cond_prio = self.GetConditionPriority(skill, SkillEffectTargets.Target);
        cache.pos = new BattleCore.SVector2(self.x, self.y);
        cache.baseRangeMap = (GridMap<bool>) null;
        if (skill.TeleportType != eTeleportType.None)
          cache.baseRangeMap = this.CreateSelectGridMapAI(self, self.x, self.y, skill);
        if (self.IsUnitFlag(EUnitFlag.Escaped) && !skill.IsHealSkill())
          flag = false;
        SkillMap.Data data = this.mSkillMap.Get(skill);
        if (data == null)
          return;
        foreach (SkillMap.Target target in data.targets.Values)
        {
          if (flag || cache.pos.x == target.pos.x && cache.pos.y == target.pos.y)
          {
            self.x = target.pos.x;
            self.y = target.pos.y;
            if (this.IsUseSkillCollabo(self, skill))
            {
              foreach (SkillMap.Score score in target.scores.Values)
              {
                if (score.log != null)
                {
                  BattleCore.SkillResult skillResult = this.GetSkillResult(cache, self, skill, score);
                  if (skillResult != null)
                    results.Add(skillResult);
                }
              }
            }
          }
        }
        self.x = cache.pos.x;
        self.y = cache.pos.y;
      }
    }

    private void GetUsedTeleportSkillResult(Unit self, SkillData skill, List<BattleCore.SkillResult> results)
    {
      BattleMap currentMap = this.CurrentMap;
      GridMap<bool> selectGridMapAi = this.CreateSelectGridMapAI(self, self.x, self.y, skill);
      List<BattleCore.SVector2> svector2List = new List<BattleCore.SVector2>();
      for (int index1 = 0; index1 < selectGridMapAi.w; ++index1)
      {
        for (int index2 = 0; index2 < selectGridMapAi.h; ++index2)
        {
          if (selectGridMapAi.get(index1, index2))
            svector2List.Add(new BattleCore.SVector2(index1, index2));
        }
      }
      int moveCount = self.GetMoveCount(true);
      Unit unit1 = (Unit) null;
      int num1 = int.MaxValue;
      BattleCore.SVector2 svector2_1 = new BattleCore.SVector2(self.x, self.y);
      if (this.mEnemyPriorities.Count == 0)
        this.GetEnemyPriorities(self, this.mEnemyPriorities, this.mGimmickPriorities);
      List<Unit> mEnemyPriorities = this.mEnemyPriorities;
      for (int index1 = 0; index1 < mEnemyPriorities.Count; ++index1)
      {
        Unit unit2 = mEnemyPriorities[index1];
        Grid unitGridPosition = this.GetUnitGridPosition(unit2);
        if (currentMap.CalcMoveSteps(self, unitGridPosition, false))
        {
          int step = (int) currentMap[self.x, self.y].step;
          for (int index2 = 0; index2 < svector2List.Count; ++index2)
          {
            BattleCore.SVector2 svector2_2 = svector2List[index2];
            if ((int) currentMap[svector2_2.x, svector2_2.y].step < step && step > moveCount)
            {
              int num2 = Math.Abs(svector2_2.y - unit2.y) + Math.Abs(svector2_2.x - unit2.x);
              if (num1 > num2 && this.mSafeMapEx.get(svector2_2.x, svector2_2.y) > 1)
              {
                unit1 = unit2;
                num1 = num2;
                svector2_1 = svector2_2;
              }
            }
          }
        }
      }
      if (unit1 == null || svector2_1.x == self.x && svector2_1.y == self.y)
        return;
      results.Add(new BattleCore.SkillResult()
      {
        skill = skill,
        movpos = this.GetUnitGridPosition(self),
        usepos = currentMap[svector2_1.x, svector2_1.y],
        teleport = -1
      });
    }

    private GridMap<bool> CreateSelectGridMapAI(Unit self, int targetX, int targetY, SkillData skill)
    {
      GridMap<bool> rangeMap = this.CreateSelectGridMap(self, self.x, self.y, skill);
      if (skill.TeleportType != eTeleportType.None)
        rangeMap = this.RemoveCantMove(self, rangeMap, skill);
      return rangeMap;
    }

    private GridMap<bool> RemoveCantMove(Unit self, GridMap<bool> rangeMap, SkillData skill)
    {
      BattleMap currentMap = this.CurrentMap;
      for (int x = 0; x < rangeMap.w; ++x)
      {
        for (int y = 0; y < rangeMap.h; ++y)
        {
          if (rangeMap.get(x, y))
            rangeMap.set(x, y, currentMap.CheckEnableMoveTeleport(self, currentMap[x, y], skill));
        }
      }
      return rangeMap;
    }

    private int GetSkillTargetsHighestPriority(Unit self, SkillData skill, LogSkill log)
    {
      int val1 = (int) byte.MaxValue;
      if (self == null || skill == null || log == null)
        return val1;
      AIParam ai = self.AI;
      if (skill.IsDamagedSkill() && ai != null && !ai.CheckFlag(AIFlags.DisableTargetPriority))
      {
        for (int k = 0; k < log.targets.Count; ++k)
        {
          int index = this.mEnemyPriorities.FindIndex((Predicate<Unit>) (p => p == log.targets[k].target));
          if (index != -1)
            val1 = Math.Max(Math.Min(val1, index), 0);
        }
      }
      return val1;
    }

    private bool IsEnableUseSkillEffect(Unit self, SkillData skill, LogSkill log)
    {
      if (self == null || skill == null || log == null)
        return false;
      int num1 = 0;
      Unit rage = self.GetRageTarget();
      if (rage != null && (!skill.IsDamagedSkill() || log.targets.Find((Predicate<LogSkill.Target>) (p => p.target == rage)) == null))
        return false;
      for (int index1 = 0; index1 < log.targets.Count; ++index1)
      {
        Unit target = log.targets[index1].target;
        if (!this.CheckSkillTargetAI(self, target, skill))
          return false;
        if (!target.IsBreakObj || (skill.IsDamagedSkill() || target.BreakObjSideType != eMapBreakSideType.UNKNOWN) && (skill.IsDamagedSkill() || skill.IsHealSkill()))
        {
          if (skill.IsDamagedSkill())
          {
            if (target.IsBreakObj && target.BreakObjClashType == eMapBreakClashType.INVINCIBLE)
              continue;
          }
          else if (skill.IsHealSkill())
          {
            if (Math.Max(Math.Min(log.targets[index1].GetTotalHpHeal(), (int) target.MaximumStatus.param.hp - (int) target.CurrentStatus.param.hp), 0) == 0)
              continue;
          }
          else if (skill.IsSupportSkill())
          {
            if (self.AI == null || !self.AI.CheckFlag(AIFlags.SelfBuffOnly) || log.targets.Find((Predicate<LogSkill.Target>) (p => p.target == self)) != null)
            {
              if ((int) skill.ControlChargeTimeValue == 0)
              {
                bool flag = false;
                if (target.BuffAttachments.Count > 0)
                {
                  int num2 = 0;
                  foreach (BuffAttachment buffAttachment in target.BuffAttachments)
                  {
                    if (buffAttachment.skill != null && buffAttachment.skill.SkillID == skill.SkillID)
                      ++num2;
                  }
                  if (skill.DuplicateCount <= 1)
                  {
                    if (num2 > 0)
                      continue;
                  }
                  else if (num2 < skill.DuplicateCount)
                    flag = true;
                  else
                    continue;
                }
                BuffEffect buffEffect = skill.GetBuffEffect(SkillEffectTargets.Target);
                if (buffEffect != null && buffEffect.CheckEnableBuffTarget(target))
                {
                  for (int index2 = 0; index2 < buffEffect.targets.Count; ++index2)
                  {
                    switch (buffEffect.targets[index2].buffType)
                    {
                      case BuffTypes.Buff:
                        if (target.IsEnableBuffEffect(BuffTypes.Buff) || buffEffect.param.IsNoDisabled)
                        {
                          int num2 = self.AI == null ? 0 : (int) self.AI.buff_border;
                          if (Math.Max(100 - (int) target.CurrentStatus.enchant_resist.resist_buff, 0) > num2)
                            goto default;
                          else
                            break;
                        }
                        else
                          break;
                      case BuffTypes.Debuff:
                        if (target.IsEnableBuffEffect(BuffTypes.Debuff) || buffEffect.param.IsNoDisabled)
                        {
                          int num2 = self.AI == null ? 0 : (int) self.AI.buff_border;
                          if (Math.Max(100 - (int) target.CurrentStatus.enchant_resist.resist_debuff, 0) > num2)
                            goto default;
                          else
                            break;
                        }
                        else
                          break;
                      default:
                        if (target.GetActionSkillBuffValue(buffEffect.targets[index2].buffType, buffEffect.targets[index2].calcType, buffEffect.targets[index2].paramType) < Math.Abs((int) buffEffect.targets[index2].value))
                        {
                          flag = true;
                          goto label_37;
                        }
                        else
                          break;
                    }
                  }
label_37:
                  if (!flag)
                    continue;
                }
                else
                  continue;
              }
            }
            else
              continue;
          }
          else if (skill.IsConditionSkill())
          {
            CondEffect condEffect = skill.GetCondEffect(SkillEffectTargets.Target);
            if (condEffect != null && condEffect.param.conditions != null && condEffect.CheckEnableCondTarget(target))
            {
              bool flag = false;
              if (skill.EffectType == SkillEffectTypes.CureCondition)
              {
                for (int index2 = 0; index2 < condEffect.param.conditions.Length; ++index2)
                {
                  if (target.CheckEnableCureCondition(condEffect.param.conditions[index2]))
                  {
                    flag = true;
                    break;
                  }
                }
              }
              else if (skill.EffectType == SkillEffectTypes.FailCondition)
              {
                int num2 = self.AI == null ? 0 : (int) self.AI.cond_border;
                if (num2 <= 0 || (int) condEffect.rate <= 0 || (int) condEffect.rate >= num2)
                {
                  for (int index2 = 0; index2 < condEffect.param.conditions.Length; ++index2)
                  {
                    EUnitCondition condition = condEffect.param.conditions[index2];
                    if (!AIUtility.IsFailCondition(self, target, condition))
                      return false;
                    switch (condition)
                    {
                      case EUnitCondition.DisableBuff:
                        if (this.IsBuffDebuffEffectiveEnemies(self, BuffTypes.Buff))
                          goto default;
                        else
                          break;
                      case EUnitCondition.DisableDebuff:
                        if (this.IsBuffDebuffEffectiveEnemies(self, BuffTypes.Debuff))
                          goto default;
                        else
                          break;
                      default:
                        if (target.CheckEnableFailCondition(condition) && (num2 <= 0 || Math.Max((int) condEffect.value - (int) target.CurrentStatus.enchant_resist[condition], 0) >= num2))
                        {
                          flag = true;
                          goto label_64;
                        }
                        else
                          break;
                    }
                  }
                }
                else
                  continue;
              }
              else if (skill.EffectType == SkillEffectTypes.DisableCondition)
              {
                for (int index2 = 0; index2 < condEffect.param.conditions.Length; ++index2)
                {
                  if (!target.IsDisableUnitCondition(condEffect.param.conditions[index2]))
                  {
                    flag = true;
                    break;
                  }
                }
              }
label_64:
              if (!flag)
                continue;
            }
            else
              continue;
          }
          ++num1;
        }
      }
      return num1 != 0;
    }

    private void UpdateTrickMap(Unit self)
    {
      this.mTrickMap.owner = self;
      this.mTrickMap.Clear();
      List<TrickData> effectAll = TrickData.GetEffectAll();
      for (int index = 0; index < effectAll.Count; ++index)
        this.mTrickMap.SetData(new TrickMap.Data(effectAll[index]));
    }

    private bool IsFailTrickData(Unit unit, int x, int y)
    {
      TrickMap.Data data = this.mTrickMap.GetData(x, y);
      if (data != null && data.IsVisual(unit) && data.IsVaild(unit))
        return data.IsFail(unit);
      return false;
    }

    private bool IsGoodTrickData(Unit unit, int x, int y)
    {
      TrickMap.Data data = this.mTrickMap.GetData(x, y);
      if (data != null && data.IsVisual(unit) && data.IsVaild(unit))
        return !data.IsFail(unit);
      return false;
    }

    private int GetFailTrickPriority(Unit self, Grid movpos)
    {
      int num1 = 0;
      if (self == null || movpos == null)
        return num1;
      TrickMap.Data data = this.mTrickMap.GetData(movpos.x, movpos.y);
      if (data == null || !data.IsVisual(self) || (!data.IsVaild(self) || !data.IsFail(self)))
        return num1;
      if (data.IsDamage())
      {
        float num2 = (float) data.CalcDamage(self) / (float) (int) self.MaximumStatus.param.hp;
        if ((double) num2 >= 1.0)
          num2 = 1f;
        num1 += (int) ((double) num2 * 100.0);
      }
      if (data.IsCondEffect())
        ++num1;
      if (data.IsBuffEffect())
        ++num1;
      if (num1 == 0)
        num1 = 1;
      return num1;
    }

    private int GetGoodTrickPriority(Unit self, Grid movpos, ref int heal_trick)
    {
      int num1 = 0;
      if (self == null || movpos == null)
        return num1;
      TrickMap.Data data = this.mTrickMap.GetData(movpos.x, movpos.y);
      if (data == null || !data.IsVisual(self) || (!data.IsVaild(self) || data.IsFail(self)))
        return num1;
      num1 = 1;
      if (data.IsHeal() && (double) ((float) (int) self.CurrentStatus.param.hp / (float) (int) self.MaximumStatus.param.hp) < 0.600000023841858)
      {
        float num2 = (float) data.CalcHeal(self) / (float) (int) self.MaximumStatus.param.hp;
        heal_trick += (int) ((double) num2 * 100.0);
      }
      if (data.IsBuffEffect())
        num1 += data.GetBuffPriority(self);
      return num1;
    }

    private bool CalcMoveTargetAI(Unit self, bool forceAI)
    {
      if (!self.IsEnableMoveCondition(false) || self.GetMoveCount(false) == 0)
        return false;
      BattleMap map = this.CurrentMap;
      Grid start = map[self.x, self.y];
      if (self.IsUnitFlag(EUnitFlag.Escaped))
      {
        Grid escapePositionAi = this.GetEscapePositionAI(self);
        if (escapePositionAi != null)
        {
          if (start == escapePositionAi)
            return false;
          if (this.Move(self, escapePositionAi, forceAI))
            return true;
        }
      }
      AIPatrolPoint currentPatrolPoint = self.GetCurrentPatrolPoint();
      if (currentPatrolPoint != null)
      {
        Grid goal = map[currentPatrolPoint.x, currentPatrolPoint.y];
        if (goal != null)
        {
          if (start == goal)
            return false;
          if (this.Move(self, goal, forceAI))
          {
            if ((int) map[self.x, self.y].step <= currentPatrolPoint.length)
              self.NextPatrolPoint();
            return true;
          }
        }
      }
      bool flag1 = false;
      if (self.IsUnitFlag(EUnitFlag.Action) && (self.AI == null || !self.AI.CheckFlag(AIFlags.DisableAction)))
        flag1 = true;
      if (flag1)
      {
        Grid safePositionAi = this.GetSafePositionAI(self);
        if (safePositionAi != null)
        {
          if (start == safePositionAi)
            return false;
          if (this.Move(self, safePositionAi, forceAI))
            return true;
        }
      }
      bool is_friendlyfire = self.AI != null && self.AI.CheckFlag(AIFlags.CastSkillFriendlyFire);
      bool is_sneaked = self.IsUnitFlag(EUnitFlag.Sneaking);
      if (self.TreasureGainTarget != null)
      {
        List<Grid> enableMoveGridList = this.GetEnableMoveGridList(self, true, is_friendlyfire, is_sneaked, true, true);
        Grid grid = !enableMoveGridList.Contains(self.TreasureGainTarget) ? (enableMoveGridList.Count <= 0 ? (Grid) null : enableMoveGridList[0]) : self.TreasureGainTarget;
        if (grid != null && map.CalcMoveSteps(self, grid, false) && this.Move(self, grid, forceAI))
          return true;
      }
      List<Unit> mEnemyPriorities = this.mEnemyPriorities;
      mEnemyPriorities.AddRange((IEnumerable<Unit>) this.mGimmickPriorities);
      List<BattleCore.MoveGoalTarget> list = BattleCore.MoveGoalTarget.Create(mEnemyPriorities);
      map.CalcMoveSteps(self, this.GetUnitGridPosition(self), false);
      List<Grid> enableMoveGridList1 = this.GetEnableMoveGridList(self, true, is_friendlyfire, is_sneaked, false, false);
      for (int index = 0; index < enableMoveGridList1.Count; ++index)
        enableMoveGridList1[index].step = (byte) 127;
      for (int index1 = 0; index1 < list.Count; ++index1)
      {
        BattleCore.MoveGoalTarget moveGoalTarget = list[index1];
        Grid unitGridPosition = this.GetUnitGridPosition(moveGoalTarget.unit);
        Grid[] aroundGrids = this.GetAroundGrids(moveGoalTarget.unit, unitGridPosition.x, unitGridPosition.y);
        int index2 = -1;
        for (int index3 = 0; index3 < aroundGrids.Length; ++index3)
        {
          if (aroundGrids[index3] != null && aroundGrids[index3].step != (byte) 127 && Math.Abs(unitGridPosition.height - aroundGrids[index3].height) <= this.mSkillMap.attackHeight && (index2 == -1 || (int) aroundGrids[index3].step < (int) aroundGrids[index2].step))
            index2 = index3;
        }
        if (index2 != -1)
        {
          moveGoalTarget.goal.x = (float) aroundGrids[index2].x;
          moveGoalTarget.goal.y = (float) aroundGrids[index2].y;
          moveGoalTarget.step = !moveGoalTarget.unit.IsGimmick ? -1f : (float) aroundGrids[index2].step;
        }
        else
        {
          moveGoalTarget.goal.x = (float) unitGridPosition.x;
          moveGoalTarget.goal.y = (float) unitGridPosition.y;
          moveGoalTarget.step = (float) byte.MaxValue;
        }
      }
      if (this.mGimmickPriorities.Count > 0)
      {
        Comparison<BattleCore.MoveGoalTarget> comparison = (Comparison<BattleCore.MoveGoalTarget>) ((p1, p2) => p1.step.CompareTo(p2.step));
        SortUtility.StableSort<BattleCore.MoveGoalTarget>(list, comparison);
      }
      for (int index1 = 0; index1 < list.Count; ++index1)
      {
        Vector2 goal = list[index1].goal;
        Grid unitGridPosition = this.GetUnitGridPosition((int) goal.x, (int) goal.y);
        if (map.CalcMoveSteps(self, unitGridPosition, false))
        {
          List<Grid> enableMoveGridList2 = this.GetEnableMoveGridList(self, true, is_friendlyfire, is_sneaked, false, true);
          if (is_sneaked)
          {
            bool flag2 = false;
            for (int index2 = 0; index2 < enableMoveGridList2.Count; ++index2)
            {
              if (enableMoveGridList2[index2].step >= (byte) 0 && (int) enableMoveGridList2[index2].step < (int) start.step)
              {
                flag2 = true;
                break;
              }
            }
            if (!flag2)
              enableMoveGridList2 = this.GetEnableMoveGridList(self, true, is_friendlyfire, false, false, true);
          }
          MySort<Grid>.Sort(enableMoveGridList2, (Comparison<Grid>) ((src, dsc) =>
          {
            if ((int) src.step == (int) dsc.step && (int) src.step <= (int) start.step)
            {
              int num1 = 0;
              int num2 = 0;
              int num3 = 0;
              int num4 = 0;
              Grid grid1 = map[src.x - 1, src.y];
              Grid grid2 = map[src.x + 1, src.y];
              Grid grid3 = map[src.x, src.y - 1];
              Grid grid4 = map[src.x, src.y + 1];
              if (grid1 != null && Math.Abs((int) src.step - (int) grid1.step) == 1)
              {
                num1 += (int) grid1.step;
                ++num2;
              }
              if (grid2 != null && Math.Abs((int) src.step - (int) grid2.step) == 1)
              {
                num1 += (int) grid2.step;
                ++num2;
              }
              if (grid3 != null && Math.Abs((int) src.step - (int) grid3.step) == 1)
              {
                num1 += (int) grid3.step;
                ++num2;
              }
              if (grid4 != null && Math.Abs((int) src.step - (int) grid4.step) == 1)
              {
                num1 += (int) grid4.step;
                ++num2;
              }
              Grid grid5 = map[dsc.x - 1, dsc.y];
              Grid grid6 = map[dsc.x + 1, dsc.y];
              Grid grid7 = map[dsc.x, dsc.y - 1];
              Grid grid8 = map[dsc.x, dsc.y + 1];
              if (grid5 != null && Math.Abs((int) dsc.step - (int) grid5.step) == 1)
              {
                num3 += (int) grid5.step;
                ++num4;
              }
              if (grid6 != null && Math.Abs((int) dsc.step - (int) grid6.step) == 1)
              {
                num3 += (int) grid6.step;
                ++num4;
              }
              if (grid7 != null && Math.Abs((int) dsc.step - (int) grid7.step) == 1)
              {
                num3 += (int) grid7.step;
                ++num4;
              }
              if (grid8 != null && Math.Abs((int) dsc.step - (int) grid8.step) == 1)
              {
                num3 += (int) grid8.step;
                ++num4;
              }
              if (num1 != 0 && num3 != 0)
                return num1 * 100 / num2 < num3 * 100 / num4 ? -1 : 1;
            }
            return (int) src.step - (int) dsc.step;
          }));
          for (int index2 = 0; index2 < enableMoveGridList2.Count && start != enableMoveGridList2[index2]; ++index2)
          {
            if (this.Move(self, enableMoveGridList2[index2], forceAI))
              return true;
          }
          if (start == unitGridPosition)
            return false;
          if (this.Move(self, unitGridPosition, forceAI))
            return true;
        }
      }
      return false;
    }

    private Grid[] GetAroundGrids(Unit unit, int gx, int gy)
    {
      BattleMap currentMap = this.CurrentMap;
      Grid[] gridArray = new Grid[4]{ currentMap[gx - 1, gy], currentMap[gx + 1, gy], currentMap[gx, gy + 1], currentMap[gx, gy - 1] };
      if (unit == null || unit.IsNormalSize)
        return gridArray;
      IntVector2[] intVector2Array = new IntVector2[4]{ new IntVector2(-1, 0), new IntVector2(1, 0), new IntVector2(0, 1), new IntVector2(0, -1) };
      for (int index1 = 0; index1 < 4; ++index1)
      {
        if (gridArray[index1] != null)
        {
          while (unit.CheckCollision(gridArray[index1]))
          {
            int index2 = gridArray[index1].x + intVector2Array[index1].x;
            int index3 = gridArray[index1].y + intVector2Array[index1].y;
            gridArray[index1] = currentMap[index2, index3];
            if (gridArray[index1] == null)
              break;
          }
        }
      }
      return gridArray;
    }

    private List<Grid> GetEnableMoveGridList(Unit self, bool is_move = true, bool is_friendlyfire = true, bool is_sneaked = false, bool is_treasure = false, bool is_trickpanel = false)
    {
      BattleMap currentMap = this.CurrentMap;
      Grid grid1 = currentMap[self.x, self.y];
      List<Grid> gridList = new List<Grid>(this.mMoveMap.w * this.mMoveMap.h);
      int num1 = self.GetMoveCount(false);
      if (self.IsUnitFlag(EUnitFlag.Moved) || !self.IsEnableMoveCondition(false))
        num1 = 0;
      if (is_move && num1 > 0)
      {
        int num2 = (int) byte.MaxValue;
        if (self.TreasureGainTarget != null && is_treasure)
        {
          Grid grid2 = currentMap[self.x, self.y];
          currentMap.CalcMoveSteps(self, self.TreasureGainTarget, true);
          num2 = (int) grid2.step / num1 + ((int) grid2.step % num1 == 0 ? 0 : 1);
        }
        for (int x = 0; x < this.mMoveMap.w; ++x)
        {
          for (int y = 0; y < this.mMoveMap.h; ++y)
          {
            if (this.mMoveMap.get(x, y) >= 0)
            {
              Grid grid2 = currentMap[x, y];
              if ((!is_friendlyfire || !this.CheckFriendlyFireOnGridMap(self, grid2)) && (!is_trickpanel || !this.IsFailTrickData(self, x, y)))
              {
                if (self.TreasureGainTarget != null && is_treasure)
                {
                  if ((int) grid2.step / num1 + ((int) grid2.step % num1 == 0 ? 0 : 1) > num2)
                    continue;
                }
                else if (is_sneaked && this.mSearchMap.get(x, y))
                  continue;
                if (this.CheckMove(self, grid2) && !gridList.Contains(grid2))
                  gridList.Add(grid2);
              }
            }
          }
        }
        if (self.TreasureGainTarget != null && is_treasure)
        {
          if (gridList.Contains(self.TreasureGainTarget))
          {
            gridList.Clear();
            gridList.Add(self.TreasureGainTarget);
          }
          else
          {
            int num3 = (int) byte.MaxValue;
            for (int index = 0; index < gridList.Count; ++index)
            {
              if (num3 > (int) gridList[index].step)
                num3 = (int) gridList[index].step;
            }
            for (int index = 0; index < gridList.Count; ++index)
            {
              if (num3 < (int) gridList[index].step)
                gridList.RemoveAt(index--);
            }
          }
          if (self.IsEnableActionCondition())
          {
            Grid grid2 = currentMap[self.x, self.y];
            if (!gridList.Contains(grid2))
              gridList.Add(grid2);
          }
        }
      }
      else
        gridList.Add(grid1);
      return gridList;
    }

    private bool CheckEscapeAI(Unit self)
    {
      if (this.QuestType == QuestTypes.Arena || !self.IsEnableMoveCondition(false) || (self.GetMoveCount(false) == 0 || !self.CheckNeedEscaped()))
        return false;
      return this.GetHealer(self).Count > 0;
    }

    private Grid GetEscapePositionAI(Unit self)
    {
      BattleMap currentMap = this.CurrentMap;
      if ((self.IsUnitFlag(EUnitFlag.Moved) ? 0 : self.GetMoveCount(false)) == 0)
        return (Grid) null;
      bool flag1 = self.AI != null && self.AI.CheckFlag(AIFlags.CastSkillFriendlyFire);
      Grid grid1 = currentMap[self.x, self.y];
      List<Unit> healer = this.GetHealer(self);
      for (int index = 0; index < healer.Count; ++index)
      {
        Grid target = currentMap[healer[index].x, healer[index].y];
        if (currentMap.CalcMoveSteps(self, target, false))
        {
          bool flag2 = false;
          for (int x = 0; x < this.mMoveMap.w; ++x)
          {
            for (int y = 0; y < this.mMoveMap.h; ++y)
            {
              if (this.mMoveMap.get(x, y) >= 0)
              {
                Grid grid2 = currentMap[x, y];
                if (this.CheckMove(self, grid2) && (!flag1 || !this.CheckFriendlyFireOnGridMap(self, grid2)) && (int) grid1.step > (int) grid2.step)
                {
                  grid1 = grid2;
                  flag2 = true;
                }
              }
            }
          }
          if (flag2)
            break;
        }
      }
      return grid1;
    }

    private bool CalcSearchingAI(Unit self)
    {
      if (self.IsUnitFlag(EUnitFlag.Searched))
        return true;
      List<Unit> unitList = new List<Unit>(1);
      unitList.Add(self);
      if (!string.IsNullOrEmpty(self.UniqueName))
      {
        for (int index = 0; index < this.mUnits.Count; ++index)
        {
          Unit mUnit = this.mUnits[index];
          if (mUnit != self && mUnit.Side == self.Side && mUnit.ParentUniqueName == self.UniqueName)
            unitList.Add(this.mUnits[index]);
        }
      }
      if (!string.IsNullOrEmpty(self.ParentUniqueName))
      {
        for (int index = 0; index < this.mUnits.Count; ++index)
        {
          Unit mUnit = this.mUnits[index];
          if (mUnit != self && mUnit.Side == self.Side && (mUnit.UniqueName == self.ParentUniqueName || mUnit.ParentUniqueName == self.ParentUniqueName))
            unitList.Add(this.mUnits[index]);
        }
      }
      bool flag = false;
      for (int index = 0; index < unitList.Count; ++index)
      {
        if (unitList[index].IsUnitFlag(EUnitFlag.Searched) || this.Searching(unitList[index]))
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        for (int index = 0; index < unitList.Count; ++index)
          unitList[index].SetUnitFlag(EUnitFlag.Searched, true);
        return true;
      }
      this.CommandWait(false);
      return false;
    }

    private bool Searching(Unit self)
    {
      DebugUtility.Assert(self != null, "self == null");
      if (self.IsUnitFlag(EUnitFlag.Searched))
        return true;
      BattleMap currentMap = this.CurrentMap;
      DebugUtility.Assert(currentMap != null, "map == null");
      int searchRange = self.GetSearchRange();
      if (searchRange <= 0)
        return false;
      GridMap<bool> result = new GridMap<bool>(currentMap.Width, currentMap.Height);
      this.CreateSelectGridMap(self, self.x, self.y, 0, searchRange, ESelectType.Diamond, ELineType.None, 0, false, false, 99, false, ref result, false);
      for (int x = 0; x < result.w; ++x)
      {
        for (int y = 0; y < result.h; ++y)
        {
          if (result.get(x, y))
          {
            Unit unitAtGrid = this.FindUnitAtGrid(currentMap[x, y]);
            if (unitAtGrid != null && unitAtGrid.Side != self.Side)
              return true;
          }
        }
      }
      return false;
    }

    private void UpdateSearchMap(Unit self)
    {
      BattleMap currentMap = this.CurrentMap;
      this.mSearchMap.fill(false);
      GridMap<bool> result = new GridMap<bool>(currentMap.Width, currentMap.Height);
      result.fill(false);
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (mUnit != self && mUnit.Side != self.Side && (!mUnit.IsDead && !mUnit.IsGimmick) && (!mUnit.IsSub && mUnit.IsEntry))
        {
          int range_max = mUnit.GetSearchRange() + 1;
          if (range_max != 0)
          {
            int x1 = mUnit.x;
            int y1 = mUnit.y;
            this.CreateSelectGridMap(mUnit, x1, y1, 0, range_max, ESelectType.Diamond, ELineType.None, 0, false, false, 99, false, ref result, false);
            for (int x2 = 0; x2 < result.w; ++x2)
            {
              for (int y2 = 0; y2 < result.h; ++y2)
              {
                if (result.get(x2, y2))
                  this.mSearchMap.set(x2, y2, true);
              }
            }
          }
        }
      }
    }

    private bool CheckSearchMap(Unit self)
    {
      DebugUtility.Assert(this.mSearchMap != null, "mSearchMap == null");
      BattleMap currentMap = this.CurrentMap;
      DebugUtility.Assert(currentMap != null, "map == null");
      DebugUtility.Assert(currentMap[self.x, self.y] != null, "grid == null");
      for (int x = 0; x < this.mSearchMap.w; ++x)
      {
        for (int y = 0; y < this.mSearchMap.h; ++y)
        {
          if (this.mSearchMap.get(x, y))
          {
            Grid grid = currentMap[x, y];
            if (self.CheckCollision(grid))
              return true;
          }
        }
      }
      return false;
    }

    private bool CheckEnemyIntercept(Unit self)
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (mUnit != self && mUnit.Side != self.Side && (!mUnit.IsDeadCondition() && !mUnit.IsSub) && (mUnit.IsEntry && !mUnit.IsGimmick && (mUnit.IsUnitFlag(EUnitFlag.Searched) || this.CheckSearchMap(self))))
          return true;
      }
      return false;
    }

    private int GetCurrentEnemyNum(Unit self)
    {
      int num = 0;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (!mUnit.IsDead && mUnit.IsEntry && (!mUnit.IsGimmick && !mUnit.IsSub) && (mUnit != self && this.CheckEnemySide(self, mUnit)))
          ++num;
      }
      return num;
    }

    private void GetEnemyPriorities(Unit self, List<Unit> enemyTargets, List<Unit> gimmickTargets)
    {
      if (self == null)
        return;
      enemyTargets.Clear();
      gimmickTargets.Clear();
      Unit rageTarget = self.GetRageTarget();
      if (rageTarget != null)
      {
        enemyTargets.Add(rageTarget);
      }
      else
      {
        for (int index = 0; index < this.mUnits.Count; ++index)
        {
          Unit mUnit = this.mUnits[index];
          if (mUnit != self && !mUnit.IsDead && (!mUnit.IsSub && mUnit.IsEntry))
          {
            if (mUnit.IsGimmick)
            {
              if (mUnit.IsBreakObj && this.IsTargetBreakUnit(self, mUnit, (SkillData) null) && (this.IsTargetBreakUnitAI(self, mUnit) && this.CheckGimmickEnemySide(self, mUnit)))
                gimmickTargets.Add(mUnit);
            }
            else if (this.CheckEnemySide(self, mUnit))
              enemyTargets.Add(mUnit);
          }
        }
        this.SortAttackTargets(self, enemyTargets);
      }
    }

    private void SortAttackTargets(Unit unit, List<Unit> targets)
    {
      DebugUtility.Assert(unit != null, "unit == null");
      if (targets.Count <= 0)
        return;
      Unit rage = unit.GetRageTarget();
      MySort<Unit>.Sort(targets, (Comparison<Unit>) ((src, dsc) =>
      {
        if (src == dsc)
          return 0;
        if (src.UnitType != EUnitType.Unit && dsc.UnitType == EUnitType.Unit)
          return 1;
        if (dsc.UnitType != EUnitType.Unit && src.UnitType == EUnitType.Unit)
          return -1;
        if (rage != null)
        {
          if (src == rage)
            return -1;
          if (dsc == rage)
            return 1;
        }
        AIParam ai = unit.AI;
        if (ai != null)
        {
          RoleTypes role = ai.role;
          RoleTypes roleType1 = src.RoleType;
          RoleTypes roleType2 = dsc.RoleType;
          if (role != RoleTypes.None && roleType1 != roleType2)
          {
            if (role == roleType1)
              return -1;
            if (role == roleType2)
              return 1;
          }
          if (ai.param_prio != ParamPriorities.None)
          {
            int num1 = 0;
            switch (ai.param)
            {
              case ParamTypes.Hp:
                num1 = (int) src.CurrentStatus.param.hp - (int) dsc.CurrentStatus.param.hp;
                break;
              case ParamTypes.HpMax:
                num1 = (int) src.MaximumStatus.param.hp - (int) dsc.MaximumStatus.param.hp;
                break;
              case ParamTypes.Atk:
                num1 = (int) src.CurrentStatus.param.atk - (int) dsc.CurrentStatus.param.atk;
                break;
              case ParamTypes.Def:
                num1 = (int) src.CurrentStatus.param.def - (int) dsc.CurrentStatus.param.def;
                break;
              case ParamTypes.Mag:
                num1 = (int) src.CurrentStatus.param.mag - (int) dsc.CurrentStatus.param.mag;
                break;
              case ParamTypes.Mnd:
                num1 = (int) src.CurrentStatus.param.mnd - (int) dsc.CurrentStatus.param.mnd;
                break;
            }
            int num2 = num1 * (ai.param_prio != ParamPriorities.High ? 1 : -1);
            if (num2 != 0)
            {
              if (!ai.CheckFlag(AIFlags.UseOldSort))
                return num2;
              switch (ai.param_prio)
              {
                case ParamPriorities.High:
                  return Math.Min(Math.Abs(num2), 1) * -1;
                case ParamPriorities.Low:
                  return Math.Min(Math.Abs(num2), 1);
              }
            }
          }
        }
        return this.CalcNearGridDistance(unit, src) - this.CalcNearGridDistance(unit, dsc);
      }));
    }

    private int GetAliveUnitCount(Unit self)
    {
      int num = 0;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (!mUnit.IsDead && !mUnit.IsGimmick && mUnit.Side == self.Side)
          ++num;
      }
      return num;
    }

    private int GetDeadUnitCount(Unit self)
    {
      int num = 0;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (mUnit.IsDead && !mUnit.IsGimmick && mUnit.Side == self.Side)
          ++num;
      }
      return num;
    }

    private int GetFailCondSelfSideUnitCount(Unit self, EUnitCondition condition)
    {
      int num = 0;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (!this.mUnits[index].IsSub && this.mUnits[index].IsEntry && (!this.mUnits[index].IsDead && !this.mUnits[index].IsGimmick) && (!this.CheckEnemySide(self, mUnit) && mUnit.IsUnitCondition(condition)))
          ++num;
      }
      return num;
    }

    private int GetHealUnitCount(Unit self)
    {
      int num = 0;
      AIParam ai = self.AI;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (!mUnit.IsDead && mUnit.IsEntry && !mUnit.IsSub && (!this.CheckEnemySide(self, mUnit) || mUnit.IsGimmick && this.IsTargetBreakUnit(self, mUnit, (SkillData) null)))
        {
          int hp1 = (int) mUnit.CurrentStatus.param.hp;
          int hp2 = (int) mUnit.MaximumStatus.param.hp;
          if (ai != null)
          {
            if (hp2 * (int) ai.heal_border < hp1 * 100)
              continue;
          }
          else if (hp1 == hp2)
            continue;
          ++num;
        }
      }
      return num;
    }

    private List<Unit> GetHealer(Unit self)
    {
      List<Unit> l = new List<Unit>();
      int hp1 = (int) self.CurrentStatus.param.hp;
      int hp2 = (int) self.MaximumStatus.param.hp;
      for (int index1 = 0; index1 < this.mUnits.Count; ++index1)
      {
        Unit mUnit = this.mUnits[index1];
        if (!mUnit.IsDead && mUnit.IsEntry && (!mUnit.IsGimmick && !mUnit.IsSub) && (mUnit != self && mUnit.IsEnableSkillCondition(true) && !this.CheckEnemySide(self, mUnit)))
        {
          for (int index2 = 0; index2 < mUnit.BattleSkills.Count; ++index2)
          {
            SkillData battleSkill = mUnit.BattleSkills[index2];
            if (battleSkill != null)
            {
              SkillData skill = self.GetSkillForUseCount(battleSkill.SkillID, 0) ?? battleSkill;
              if (skill.IsHealSkill() && this.CheckEnableUseSkill(mUnit, skill, true) && (this.IsUseSkillCollabo(mUnit, skill) && this.CheckSkillTargetAI(mUnit, self, skill)) && hp2 * (int) mUnit.AI.heal_border >= hp1 * 100)
              {
                l.Add(mUnit);
                break;
              }
            }
          }
        }
      }
      if (l.Count > 0)
        MySort<Unit>.Sort(l, (Comparison<Unit>) ((src, dsc) =>
        {
          if (src == dsc)
            return 0;
          int chargeTime1 = (int) src.ChargeTime;
          int chargeTimeMax1 = (int) src.ChargeTimeMax;
          int chargeTime2 = (int) dsc.ChargeTime;
          int chargeTimeMax2 = (int) dsc.ChargeTimeMax;
          if (chargeTime1 != chargeTime2)
          {
            if (chargeTime1 >= chargeTimeMax1 && chargeTime2 >= chargeTimeMax2)
              return chargeTime2 - chargeTimeMax2 - (chargeTime1 - chargeTimeMax1);
            if (chargeTime1 >= chargeTimeMax1)
              return -1;
            if (chargeTime2 >= chargeTimeMax2)
              return 1;
            int chargeSpeed1 = (int) src.GetChargeSpeed();
            int chargeSpeed2 = (int) dsc.GetChargeSpeed();
            int num1 = chargeTimeMax1 - chargeTime1 == 0 ? 0 : (chargeTimeMax1 - chargeTime1) * 100 / chargeSpeed1;
            int num2 = chargeTimeMax2 - chargeTime2 == 0 ? 0 : (chargeTimeMax2 - chargeTime2) * 100 / chargeSpeed2;
            if (num1 != num2)
              return num1 - num2;
          }
          return this.CalcNearGridDistance(self, src) - this.CalcNearGridDistance(self, dsc);
        }));
      return l;
    }

    public List<Unit> CreateAttackTargetsAI(Unit self, SkillData skill, bool is_move)
    {
      GridMap<bool> skillScopeMapAll = this.CreateSkillScopeMapAll(self, skill, is_move);
      List<Unit> targets = new List<Unit>(this.mUnits.Count);
      this.SearchTargetsInGridMap(self, skill, skillScopeMapAll, targets);
      for (int index = 0; index < targets.Count; ++index)
      {
        if (!this.CheckSkillTargetAI(self, targets[index], skill))
          targets.Remove(targets[index--]);
      }
      return targets;
    }

    private void RefreshTreasureTargetAI()
    {
      if (!this.mQuestParam.CheckAllowedAutoBattle() || !GameUtility.Config_AutoMode_Treasure.Value || this.mTreasures.Count == 0)
        return;
      for (int index = 0; index < this.mUnits.Count; ++index)
        this.mUnits[index].TreasureGainTarget = (Grid) null;
      BattleMap currentMap = this.CurrentMap;
      for (int index1 = 0; index1 < this.mTreasures.Count; ++index1)
      {
        if (this.mTreasures[index1].EventTrigger != null && this.mTreasures[index1].EventTrigger.EventType == EEventType.Treasure && this.mTreasures[index1].EventTrigger.Count != 0)
        {
          Unit suited = (Unit) null;
          Grid grid = currentMap[this.mTreasures[index1].x, this.mTreasures[index1].y];
          if (this.FindUnitAtGrid(grid) == null)
          {
            int num1 = (int) byte.MaxValue;
            for (int index2 = 0; index2 < this.mPlayer.Count; ++index2)
            {
              Unit unit = this.mPlayer[index2];
              if (unit.UnitType == EUnitType.Unit && unit.TreasureGainTarget == null && (unit.IsEntry && !unit.IsSub) && (unit.IsEnableAutoMode() && unit.IsEnableMoveCondition(true)))
              {
                int moveCount = unit.GetMoveCount(true);
                if (moveCount != 0)
                {
                  currentMap.CalcMoveSteps(unit, currentMap[unit.x, unit.y], false);
                  int num2 = (int) grid.step / moveCount + ((int) grid.step % moveCount <= 0 ? 0 : 1);
                  if (num2 <= num1 && (num2 != num1 || suited == null || this.mOrder.FindIndex((Predicate<BattleCore.OrderData>) (p => p.Unit == suited)) >= this.mOrder.FindIndex((Predicate<BattleCore.OrderData>) (p => p.Unit == unit))))
                  {
                    suited = unit;
                    num1 = num2;
                  }
                }
              }
            }
            if (suited != null)
              suited.TreasureGainTarget = grid;
          }
        }
      }
    }

    private bool IsBuffDebuffEffectiveEnemies(Unit self, BuffTypes type)
    {
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        Unit mUnit = this.mUnits[index];
        if (!mUnit.IsSub && mUnit.IsEntry && (!mUnit.IsDead && !mUnit.IsGimmick) && (this.CheckEnemySide(self, mUnit) && mUnit.CheckActionSkillBuffAttachments(type)))
          return true;
      }
      return false;
    }

    private bool IsFailCondSkillUseEnemies(Unit self, EUnitCondition condition)
    {
      if (condition == EUnitCondition.AutoHeal || condition == EUnitCondition.GoodSleep || (condition == EUnitCondition.AutoJewel || condition == EUnitCondition.DisableBuff) || (condition == EUnitCondition.DisableDebuff || condition == EUnitCondition.DisableKnockback))
        return false;
      for (int index1 = 0; index1 < this.mUnits.Count; ++index1)
      {
        Unit mUnit = this.mUnits[index1];
        if (!mUnit.IsSub && mUnit.IsEntry && (!mUnit.IsDead && !mUnit.IsGimmick) && (mUnit.IsEnableSkillCondition(true) && this.CheckEnemySide(self, mUnit) && mUnit.BattleSkills != null))
        {
          for (int index2 = 0; index2 < mUnit.BattleSkills.Count; ++index2)
          {
            SkillData battleSkill = mUnit.BattleSkills[index2];
            if (mUnit.IsPartyMember || (int) battleSkill.UseRate != 0 && (battleSkill.UseCondition == null || battleSkill.UseCondition.type == 0 || battleSkill.UseCondition.unlock))
            {
              CondEffect condEffect = battleSkill.GetCondEffect(SkillEffectTargets.Target);
              if (condEffect != null && condEffect.param != null && condEffect.param.conditions != null && (condEffect.param.type == ConditionEffectTypes.FailCondition || condEffect.param.type == ConditionEffectTypes.ForcedFailCondition || condEffect.param.type == ConditionEffectTypes.RandomFailCondition))
                return Array.IndexOf<EUnitCondition>(condEffect.param.conditions, condition) != -1;
            }
          }
        }
      }
      return false;
    }

    private bool IsTargetBreakUnitAI(Unit self, Unit target)
    {
      EUnitSide eunitSide = self.Side;
      if (self.IsUnitCondition(EUnitCondition.Charm) || self.IsUnitCondition(EUnitCondition.Zombie))
      {
        if (self.Side == EUnitSide.Player)
          eunitSide = EUnitSide.Enemy;
        else if (self.Side == EUnitSide.Enemy)
          eunitSide = EUnitSide.Player;
      }
      bool flag = true;
      if (target.BreakObjAIType == eMapBreakAIType.PALL)
      {
        if (eunitSide != EUnitSide.Player)
          flag = false;
      }
      else if (target.BreakObjAIType == eMapBreakAIType.EALL)
      {
        if (eunitSide != EUnitSide.Enemy)
          flag = false;
      }
      else if (target.BreakObjAIType == eMapBreakAIType.NONE)
        flag = false;
      return flag;
    }

    private int GetAtkBonusForAttackDetailType(Unit self, SkillData skill)
    {
      int num = 0;
      if (skill.IsReactionSkill())
        num += (int) self.CurrentStatus[BattleBonus.ReactionAttack];
      switch (skill.AttackDetailType)
      {
        case AttackDetailTypes.Slash:
          num = num + (int) self.CurrentStatus[BattleBonus.SlashAttack] + this.mQuestParam.GetAtkTypeMag(AttackDetailTypes.Slash);
          break;
        case AttackDetailTypes.Stab:
          num = num + (int) self.CurrentStatus[BattleBonus.PierceAttack] + this.mQuestParam.GetAtkTypeMag(AttackDetailTypes.Stab);
          break;
        case AttackDetailTypes.Blow:
          num = num + (int) self.CurrentStatus[BattleBonus.BlowAttack] + this.mQuestParam.GetAtkTypeMag(AttackDetailTypes.Blow);
          break;
        case AttackDetailTypes.Shot:
          num = num + (int) self.CurrentStatus[BattleBonus.ShotAttack] + this.mQuestParam.GetAtkTypeMag(AttackDetailTypes.Shot);
          break;
        case AttackDetailTypes.Magic:
          num = num + (int) self.CurrentStatus[BattleBonus.MagicAttack] + this.mQuestParam.GetAtkTypeMag(AttackDetailTypes.Magic);
          break;
        case AttackDetailTypes.Jump:
          num = num + (int) self.CurrentStatus[BattleBonus.JumpAttack] + this.mQuestParam.GetAtkTypeMag(AttackDetailTypes.Jump);
          break;
      }
      return num;
    }

    private int GetResistDamageForAttackDetailType(Unit defender, SkillData skill, int damage)
    {
      int num1 = damage;
      int num2 = 0;
      if (skill.IsReactionSkill())
        num2 += (int) defender.CurrentStatus[BattleBonus.Resist_Reaction];
      switch (skill.AttackDetailType)
      {
        case AttackDetailTypes.Slash:
          num2 += (int) defender.CurrentStatus[BattleBonus.Resist_Slash];
          break;
        case AttackDetailTypes.Stab:
          num2 += (int) defender.CurrentStatus[BattleBonus.Resist_Pierce];
          break;
        case AttackDetailTypes.Blow:
          num2 += (int) defender.CurrentStatus[BattleBonus.Resist_Blow];
          break;
        case AttackDetailTypes.Shot:
          num2 += (int) defender.CurrentStatus[BattleBonus.Resist_Shot];
          break;
        case AttackDetailTypes.Magic:
          num2 += (int) defender.CurrentStatus[BattleBonus.Resist_Magic];
          break;
        case AttackDetailTypes.Jump:
          num2 += (int) defender.CurrentStatus[BattleBonus.Resist_Jump];
          break;
      }
      if (num2 != 0)
        num1 = damage - damage * num2 / 100;
      return num1;
    }

    private int GetResistDamageForUnitDefense(Unit defender, SkillData skill, int damage)
    {
      int num1 = damage;
      if (defender == null || skill == null)
        return num1;
      int num2 = 0;
      switch (defender.Element)
      {
        case EElement.Fire:
          num2 += (int) defender.CurrentStatus[BattleBonus.UnitDefenseFire];
          break;
        case EElement.Water:
          num2 += (int) defender.CurrentStatus[BattleBonus.UnitDefenseWater];
          break;
        case EElement.Wind:
          num2 += (int) defender.CurrentStatus[BattleBonus.UnitDefenseWind];
          break;
        case EElement.Thunder:
          num2 += (int) defender.CurrentStatus[BattleBonus.UnitDefenseThunder];
          break;
        case EElement.Shine:
          num2 += (int) defender.CurrentStatus[BattleBonus.UnitDefenseShine];
          break;
        case EElement.Dark:
          num2 += (int) defender.CurrentStatus[BattleBonus.UnitDefenseDark];
          break;
      }
      if (num2 != 0)
        num1 = damage - damage * num2 / 100;
      return num1;
    }

    private int CalcAtkPointSkillBase(Unit attacker, Unit defender, SkillData skill)
    {
      int num1 = 1;
      int num2 = 1;
      int num3 = 1;
      StatusParam statusParam1 = attacker.CurrentStatus.param;
      StatusParam statusParam2 = defender == null ? (StatusParam) null : defender.CurrentStatus.param;
      int num4;
      if (!string.IsNullOrEmpty(skill.SkillParam.weapon))
      {
        WeaponParam weaponParam = MonoSingleton<GameManager>.Instance.GetWeaponParam(skill.SkillParam.weapon);
        switch (weaponParam.FormulaType)
        {
          case WeaponFormulaTypes.Atk:
            num4 = (int) weaponParam.atk * (100 * (int) statusParam1.atk / 10) / 100;
            break;
          case WeaponFormulaTypes.Mag:
            num4 = (int) weaponParam.atk * (100 * (int) statusParam1.mag / 10) / 100;
            break;
          case WeaponFormulaTypes.AtkSpd:
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.atk + (int) statusParam1.spd)) / 15 / 100;
            break;
          case WeaponFormulaTypes.MagSpd:
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.mag + (int) statusParam1.spd)) / 15 / 100;
            break;
          case WeaponFormulaTypes.AtkDex:
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.atk + (int) statusParam1.dex)) / 20 / 100;
            break;
          case WeaponFormulaTypes.MagDex:
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.mag + (int) statusParam1.dex)) / 20 / 100;
            break;
          case WeaponFormulaTypes.AtkLuk:
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.atk + (int) statusParam1.luk)) / 20 / 100;
            break;
          case WeaponFormulaTypes.MagLuk:
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.mag + (int) statusParam1.luk)) / 20 / 100;
            break;
          case WeaponFormulaTypes.AtkMag:
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.atk + (int) statusParam1.mag)) / 20 / 100;
            break;
          case WeaponFormulaTypes.SpAtk:
            if ((int) statusParam1.atk > 0)
              num1 += (int) ((long) this.GetRandom() % (long) (int) statusParam1.atk);
            num4 = (int) weaponParam.atk * (100 * (int) statusParam1.atk / 10) * (50 + 100 * num1 / (int) statusParam1.atk) / 10000;
            break;
          case WeaponFormulaTypes.SpMag:
            int num5 = 0;
            if (statusParam2 != null)
              num5 = (int) statusParam2.mnd;
            num4 = (int) weaponParam.atk * (100 * (int) statusParam1.mag / 10) * (20 + 100 / ((int) statusParam1.mag + num5) * (int) statusParam1.mag) / 10000;
            break;
          case WeaponFormulaTypes.AtkSpdDex:
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.atk + (int) statusParam1.spd / 2 + (int) statusParam1.spd * attacker.Lv / 100 + (int) statusParam1.dex / 4)) / 20 / 100;
            break;
          case WeaponFormulaTypes.MagSpdDex:
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.mag + (int) statusParam1.spd / 2 + (int) statusParam1.spd * attacker.Lv / 100 + (int) statusParam1.dex / 4)) / 20 / 100;
            break;
          case WeaponFormulaTypes.AtkDexLuk:
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.atk + (int) statusParam1.dex / 2 + (int) statusParam1.luk / 2)) / 20 / 100;
            break;
          case WeaponFormulaTypes.MagDexLuk:
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.mag + (int) statusParam1.dex / 2 + (int) statusParam1.luk / 2)) / 20 / 100;
            break;
          case WeaponFormulaTypes.Luk:
            num4 = (int) weaponParam.atk * (100 * (int) statusParam1.luk / 10) / 100;
            break;
          case WeaponFormulaTypes.Dex:
            num4 = (int) weaponParam.atk * (100 * (int) statusParam1.dex / 10) / 100;
            break;
          case WeaponFormulaTypes.Spd:
            num4 = (int) weaponParam.atk * (100 * (int) statusParam1.spd / 10) / 100;
            break;
          case WeaponFormulaTypes.Cri:
            num4 = (int) weaponParam.atk * (100 * (int) statusParam1.cri / 10) / 100;
            break;
          case WeaponFormulaTypes.Def:
            num4 = (int) weaponParam.atk * (100 * (int) statusParam1.def / 10) / 100;
            break;
          case WeaponFormulaTypes.Mnd:
            num4 = (int) weaponParam.atk * (100 * (int) statusParam1.mnd / 10) / 100;
            break;
          case WeaponFormulaTypes.AtkRndLuk:
            int num6 = Mathf.CeilToInt((float) attacker.Lv / 10f);
            if (num6 <= 0)
              num6 = 1;
            int num7 = num2 + (int) ((long) this.GetRandom() % (long) num6);
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.atk + num7 * ((int) statusParam1.luk / 3)) / 20) / 100;
            break;
          case WeaponFormulaTypes.MagRndLuk:
            int num8 = Mathf.CeilToInt((float) attacker.Lv / 10f);
            if (num8 <= 0)
              num8 = 1;
            int num9 = num3 + (int) ((long) this.GetRandom() % (long) num8);
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.mag + num9 * ((int) statusParam1.luk / 3)) / 20) / 100;
            break;
          case WeaponFormulaTypes.AtkEAt:
            int num10 = 0;
            if (statusParam2 != null)
              num10 = (int) statusParam2.atk;
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.atk * 2 - num10) / 10) / 100;
            break;
          case WeaponFormulaTypes.MagEMg:
            int num11 = 0;
            if (statusParam2 != null)
              num11 = (int) statusParam2.mag;
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.mag * 2 - num11) / 10) / 100;
            break;
          case WeaponFormulaTypes.AtkDefEDf:
            int num12 = 0;
            if (statusParam2 != null)
              num12 = (int) statusParam2.def;
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.atk + (int) statusParam1.def - num12) / 10) / 100;
            break;
          case WeaponFormulaTypes.MagMndEMd:
            int num13 = 0;
            if (statusParam2 != null)
              num13 = (int) statusParam2.mnd;
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.mag + (int) statusParam1.mnd - num13) / 10) / 100;
            break;
          case WeaponFormulaTypes.LukELk:
            int num14 = 0;
            if (statusParam2 != null)
              num14 = (int) statusParam2.luk;
            num4 = (int) weaponParam.atk * (100 * ((int) statusParam1.luk * 2 - num14) / 10) / 100;
            break;
          case WeaponFormulaTypes.MHp:
            int hp = (int) attacker.MaximumStatus.param.hp;
            num4 = (int) weaponParam.atk * (100 * (hp - (int) statusParam1.hp) / 10) / 100;
            break;
          case WeaponFormulaTypes.EAt:
            int num15 = 0;
            if (statusParam2 != null)
              num15 = (int) statusParam2.atk;
            num4 = (int) weaponParam.atk * (100 * num15) / 10 / 100;
            break;
          case WeaponFormulaTypes.EMg:
            int num16 = 0;
            if (statusParam2 != null)
              num16 = (int) statusParam2.mag;
            num4 = (int) weaponParam.atk * (100 * num16) / 10 / 100;
            break;
          case WeaponFormulaTypes.EDx:
            int num17 = 0;
            if (statusParam2 != null)
              num17 = (int) statusParam2.dex;
            num4 = (int) weaponParam.atk * (100 * num17) / 10 / 100;
            break;
          case WeaponFormulaTypes.ESp2:
            int num18 = 0;
            if (statusParam2 != null)
              num18 = (int) statusParam2.spd;
            num4 = (int) weaponParam.atk * (num18 * num18) / 100;
            break;
          default:
            num4 = (int) statusParam1.atk;
            break;
        }
      }
      else
        num4 = !skill.IsPhysicalAttack() ? (int) statusParam1.mag : (int) statusParam1.atk;
      if (num4 < 0)
        num4 = 0;
      return num4;
    }

    private int CalcAtkPointSkill(Unit attacker, Unit defender, SkillData skill, LogSkill log)
    {
      int target1 = this.CalcAtkPointSkillBase(attacker, defender, skill);
      if ((bool) skill.IsCollabo)
      {
        Unit unitUseCollaboSkill = attacker.GetUnitUseCollaboSkill(skill, false);
        if (unitUseCollaboSkill != null)
        {
          int num = this.CalcAtkPointSkillBase(unitUseCollaboSkill, defender, skill);
          target1 = (target1 + num) / 2;
        }
        else
          DebugUtility.LogWarning(string.Format("BattleCore/CalcAtkPointSkill collabo unit not found! unit_iname={0}, skill_iname={1}", (object) attacker.UnitParam.iname, (object) skill.SkillParam.iname));
      }
      int skillEffectValue = this.GetSkillEffectValue(attacker, defender, skill, log);
      int num1 = SkillParam.CalcSkillEffectValue(skill.EffectCalcType, skillEffectValue, target1);
      if (skill.IsSuicide())
        num1 += (int) attacker.CurrentStatus.param.hp / 2;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
      if (num1 != 0)
      {
        int attackDetailType = this.GetAtkBonusForAttackDetailType(attacker, skill);
        if (defender != null)
        {
          EnchantParam enchantAssist1 = attacker.CurrentStatus.enchant_assist;
          EnchantParam enchantResist1 = defender.CurrentStatus.enchant_resist;
          if (!skill.IsAreaSkill())
            num2 += (int) enchantAssist1[EnchantTypes.SingleAttack] - (int) enchantResist1[EnchantTypes.SingleAttack];
          else
            num2 += (int) enchantAssist1[EnchantTypes.AreaAttack] - (int) enchantResist1[EnchantTypes.AreaAttack];
          if (!skill.IsIgnoreElement())
          {
            if (attacker.Element != EElement.None)
            {
              ElementParam elementAssist = attacker.CurrentStatus.element_assist;
              num3 += (int) elementAssist[attacker.Element];
              if (attacker.Element == UnitParam.GetWeakElement(defender.Element))
                num3 += (int) fixParam.WeakUpRate;
              else if (attacker.Element == UnitParam.GetResistElement(defender.Element))
                num3 += (int) fixParam.ResistDownRate;
              num8 += UnitParam.GetWeakElement(defender.Element) != attacker.Element ? (UnitParam.GetResistElement(defender.Element) != attacker.Element ? 0 : 1) : -1;
            }
            if (skill.ElementType != EElement.None)
            {
              num3 += (int) skill.ElementValue;
              if (skill.ElementType == UnitParam.GetWeakElement(defender.Element))
                num3 += (int) fixParam.WeakUpRate;
              else if (skill.ElementType == UnitParam.GetResistElement(defender.Element))
                num3 += (int) fixParam.ResistDownRate;
              num8 += UnitParam.GetWeakElement(defender.Element) != skill.ElementType ? (UnitParam.GetResistElement(defender.Element) != skill.ElementType ? 0 : 1) : -1;
              if (skill.ElementType == UnitParam.GetWeakElement(defender.Element))
              {
                num4 = skill.ElementSpcAtkRate;
                EnchantParam enchantAssist2 = attacker.CurrentStatus.enchant_assist;
                EnchantParam enchantResist2 = defender.CurrentStatus.enchant_resist;
                switch (defender.Element)
                {
                  case EElement.Fire:
                    if (!defender.IsDisableUnitCondition(EUnitCondition.DisableEsaFire))
                    {
                      num4 += (int) enchantAssist2.esa_fire - (int) enchantResist2.esa_fire;
                      break;
                    }
                    break;
                  case EElement.Water:
                    if (!defender.IsDisableUnitCondition(EUnitCondition.DisableEsaWater))
                    {
                      num4 += (int) enchantAssist2.esa_water - (int) enchantResist2.esa_water;
                      break;
                    }
                    break;
                  case EElement.Wind:
                    if (!defender.IsDisableUnitCondition(EUnitCondition.DisableEsaWind))
                    {
                      num4 += (int) enchantAssist2.esa_wind - (int) enchantResist2.esa_wind;
                      break;
                    }
                    break;
                  case EElement.Thunder:
                    if (!defender.IsDisableUnitCondition(EUnitCondition.DisableEsaThunder))
                    {
                      num4 += (int) enchantAssist2.esa_thunder - (int) enchantResist2.esa_thunder;
                      break;
                    }
                    break;
                  case EElement.Shine:
                    if (!defender.IsDisableUnitCondition(EUnitCondition.DisableEsaShine))
                    {
                      num4 += (int) enchantAssist2.esa_shine - (int) enchantResist2.esa_shine;
                      break;
                    }
                    break;
                  case EElement.Dark:
                    if (!defender.IsDisableUnitCondition(EUnitCondition.DisableEsaDark))
                    {
                      num4 += (int) enchantAssist2.esa_dark - (int) enchantResist2.esa_dark;
                      break;
                    }
                    break;
                }
              }
            }
            EElement index = skill.ElementType;
            if (index == EElement.None)
              index = attacker.Element;
            if (index != EElement.None)
            {
              ElementParam elementResist = defender.CurrentStatus.element_resist;
              num3 -= (int) elementResist[index];
            }
          }
          if (log != null && log.targets != null)
          {
            LogSkill.Target target2 = log.targets.Find((Predicate<LogSkill.Target>) (p => p.target == defender));
            if (target2 != null)
            {
              target2.element_effect_rate = num3;
              target2.element_effect_resist = num8;
            }
          }
          string[] tags = defender.GetTags();
          string tokkou1 = skill.SkillParam.tokkou;
          if (!string.IsNullOrEmpty(tokkou1) && tags != null)
          {
            for (int index = 0; index < tags.Length; ++index)
            {
              if (!string.IsNullOrEmpty(tags[index]) && !(tokkou1 != tags[index]))
              {
                if (skill.SkillParam.tk_rate != 0)
                {
                  num5 += skill.SkillParam.tk_rate;
                  break;
                }
                num5 += (int) fixParam.TokkouDamageRate;
                break;
              }
            }
          }
          foreach (BuffAttachment buffAttachment in attacker.BuffAttachments)
          {
            if (buffAttachment.skill != null && !string.IsNullOrEmpty(buffAttachment.skill.SkillParam.tokkou) && (tags != null && new List<string>((IEnumerable<string>) tags).Contains(buffAttachment.skill.SkillParam.tokkou)))
            {
              if (buffAttachment.skill.SkillParam.tk_rate != 0)
                num5 += buffAttachment.skill.SkillParam.tk_rate;
              else
                num5 += (int) fixParam.TokkouDamageRate;
            }
          }
          BaseStatus currentStatus = attacker.CurrentStatus;
          if (currentStatus.tokkou.Count != 0)
          {
            for (int index = 0; index < tags.Length; ++index)
            {
              TokkouValue tokkouValue = currentStatus.tokkou.SearchTagMax(tags[index]);
              if (tokkouValue != null)
                num5 += (int) tokkouValue.value;
            }
          }
          if (skill.IsEnableHeightParamAdjust())
          {
            Grid unitGridPosition1 = this.GetUnitGridPosition(attacker);
            Grid unitGridPosition2 = this.GetUnitGridPosition(defender);
            if (unitGridPosition1 != null && unitGridPosition2 != null)
            {
              int num11 = unitGridPosition1.height - unitGridPosition2.height;
              if (num11 > 0)
                num6 = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.HighGridAtkRate;
              if (num11 < 0)
                num6 = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.DownGridAtkRate;
            }
          }
          if (skill.IsNormalAttack())
          {
            JobData job = attacker.Job;
            if (job != null && (job.ArtifactDatas != null || !string.IsNullOrEmpty(job.SelectedSkin)))
            {
              List<ArtifactData> artifactDataList = new List<ArtifactData>();
              if (job.ArtifactDatas != null && job.ArtifactDatas.Length >= 1)
                artifactDataList.AddRange((IEnumerable<ArtifactData>) job.ArtifactDatas);
              if (!string.IsNullOrEmpty(job.SelectedSkin))
              {
                ArtifactData selectedSkinData = job.GetSelectedSkinData();
                if (selectedSkinData != null)
                  artifactDataList.Add(selectedSkinData);
              }
              for (int index1 = 0; index1 < artifactDataList.Count; ++index1)
              {
                ArtifactData artifactData = artifactDataList[index1];
                if (artifactData != null && artifactData.ArtifactParam != null && (artifactData.ArtifactParam.type == ArtifactTypes.Arms && artifactData.BattleEffectSkill != null) && artifactData.BattleEffectSkill.SkillParam != null)
                {
                  string tokkou2 = artifactData.BattleEffectSkill.SkillParam.tokkou;
                  if (!string.IsNullOrEmpty(tokkou2) && tags != null)
                  {
                    for (int index2 = 0; index2 < tags.Length; ++index2)
                    {
                      if (!string.IsNullOrEmpty(tags[index2]) && !(tokkou2 != tags[index2]))
                      {
                        if (artifactData.BattleEffectSkill.SkillParam.tk_rate != 0)
                        {
                          num5 += artifactData.BattleEffectSkill.SkillParam.tk_rate;
                          break;
                        }
                        num5 += (int) fixParam.TokkouDamageRate;
                        break;
                      }
                    }
                  }
                }
              }
            }
          }
          if (skill.JumpSpcAtkRate != 0 && defender.IsJump)
            num9 = skill.JumpSpcAtkRate;
          EnchantParam enchantAssist3 = attacker.CurrentStatus.enchant_assist;
          EnchantParam enchantResist3 = defender.CurrentStatus.enchant_resist;
          if (attacker.IsUnitFlag(EUnitFlag.SideAttack))
            num10 += (int) enchantAssist3[EnchantTypes.SideAttack] - (int) enchantResist3[EnchantTypes.SideAttack];
          else if (attacker.IsUnitFlag(EUnitFlag.BackAttack))
            num10 += (int) enchantAssist3[EnchantTypes.BackAttack] - (int) enchantResist3[EnchantTypes.BackAttack];
          DependStateSpcEffParam stateSpcEffParam = skill.GetDependStateSpcEffParam();
          if (stateSpcEffParam != null && stateSpcEffParam.IsSatisfyCondition(defender))
            num5 += stateSpcEffParam.InvSaRate;
        }
        if (this.IsCombinationAttack(skill))
          num7 = this.mHelperUnits.Count * (int) fixParam.CombinationRate;
        int num12 = attackDetailType + num3 + num4 + num5 + num6 + num7 + num2 + num9 + num10;
        num1 += 100 * num1 * num12 / 10000;
      }
      return num1;
    }

    private int CalcDefPointSkill(Unit attacker, Unit defender, SkillData skill, LogSkill log)
    {
      int num1 = 0;
      this.DefendSkill(attacker, defender, skill, log);
      if (skill.IsPhysicalAttack())
        num1 = (int) defender.CurrentStatus.param.def;
      if (skill.IsMagicalAttack())
        num1 = (int) defender.CurrentStatus.param.mnd;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      if (num1 > 0)
      {
        int ignoreDefenseRate = (int) skill.SkillParam.ignore_defense_rate;
        if (attacker.IsUnitFlag(EUnitFlag.BackAttack) && skill.BackAttackDefenseDownRate != 0)
          num2 = skill.BackAttackDefenseDownRate;
        if (attacker.IsUnitFlag(EUnitFlag.SideAttack) && skill.SideAttackDefenseDownRate != 0)
          num3 = skill.SideAttackDefenseDownRate;
        if (skill.IsEnableHeightParamAdjust())
        {
          Grid unitGridPosition1 = this.GetUnitGridPosition(attacker);
          Grid unitGridPosition2 = this.GetUnitGridPosition(defender);
          if (unitGridPosition1 != null && unitGridPosition2 != null)
          {
            int num5 = unitGridPosition1.height - unitGridPosition2.height;
            if (num5 > 0)
              num4 = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.HighGridDefRate;
            if (num5 < 0)
              num4 = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.DownGridDefRate;
          }
        }
        int num6 = ignoreDefenseRate + num2 + num3 + num4;
        num1 += 100 * num1 * num6 / 10000;
      }
      return num1;
    }

    private int GetDamageSkill(Unit attacker, Unit defender, SkillData skill, LogSkill log)
    {
      int num1 = Math.Max(this.CalcAtkPointSkill(attacker, defender, skill, log) - this.CalcDefPointSkill(attacker, defender, skill, log), 0);
      int num2;
      if (skill.IsJewelAttack())
      {
        num2 = BattleCore.Sqrt(num1) * 2;
      }
      else
      {
        int attackDetailType = this.GetResistDamageForAttackDetailType(defender, skill, num1);
        num2 = this.GetResistDamageForUnitDefense(defender, skill, attackDetailType);
      }
      return num2;
    }

    public class OrderData
    {
      public Unit Unit;
      public bool IsCastSkill;
      public bool IsCharged;

      public OInt GetChargeTime()
      {
        if (this.IsCastSkill)
          return this.Unit.CastTime;
        return this.Unit.ChargeTime;
      }

      public OInt GetChargeTimeMax()
      {
        if (this.IsCastSkill)
          return this.Unit.CastTimeMax;
        return this.Unit.ChargeTimeMax;
      }

      public OInt GetChargeSpeed()
      {
        if (this.IsCastSkill)
          return this.Unit.GetCastSpeed();
        return this.Unit.GetChargeSpeed();
      }

      public bool CheckChargeTimeFullOver()
      {
        if (this.IsCastSkill)
          return this.Unit.CheckCastTimeFullOver();
        return this.Unit.CheckChargeTimeFullOver();
      }

      public bool UpdateChargeTime()
      {
        if (this.IsCastSkill)
          return this.Unit.UpdateCastTime();
        return this.Unit.UpdateChargeTime();
      }
    }

    public class DropItemParam
    {
      private ItemParam mItemParam;
      private ConceptCardParam mConceptCardParam;
      public bool mIsSecret;

      public DropItemParam(ItemParam ip)
      {
        this.mItemParam = ip;
      }

      public DropItemParam(ConceptCardParam ccp)
      {
        this.mConceptCardParam = ccp;
      }

      public string Name
      {
        get
        {
          if (this.IsItem)
            return this.mItemParam.name;
          if (this.IsConceptCard)
            return this.mConceptCardParam.name;
          return string.Empty;
        }
      }

      public string Iname
      {
        get
        {
          if (this.IsItem)
            return this.mItemParam.iname;
          if (this.IsConceptCard)
            return this.mConceptCardParam.iname;
          return string.Empty;
        }
      }

      public bool IsItem
      {
        get
        {
          return this.mItemParam != null;
        }
      }

      public bool IsConceptCard
      {
        get
        {
          return this.mConceptCardParam != null;
        }
      }

      public ItemParam itemParam
      {
        get
        {
          return this.mItemParam;
        }
      }

      public ConceptCardParam conceptCardParam
      {
        get
        {
          return this.mConceptCardParam;
        }
      }
    }

    public class SkillExecLog
    {
      public string skill_iname;
      public int use_count;
      public int kill_count;

      public void Restore(BattleSuspend.Data.SkillExecLogInfo _log_info)
      {
        this.skill_iname = _log_info.inm;
        this.use_count = _log_info.ucnt;
        this.kill_count = _log_info.kcnt;
      }
    }

    public class Record
    {
      public OInt playerexp = (OInt) 0;
      public OInt unitexp = (OInt) 0;
      public OInt gold = (OInt) 0;
      public OInt chain = (OInt) 0;
      public OInt multicoin = (OInt) 0;
      public OInt pvpcoin = (OInt) 0;
      public List<UnitParam> units = new List<UnitParam>(4);
      public List<BattleCore.DropItemParam> items = new List<BattleCore.DropItemParam>(4);
      public List<ArtifactParam> artifacts = new List<ArtifactParam>(4);
      public List<int> takeoverProgressList = new List<int>();
      public Dictionary<OString, OInt> used_items = new Dictionary<OString, OInt>();
      public List<BattleCore.Record.InspSkill> mInspSkillInsList = new List<BattleCore.Record.InspSkill>();
      public List<BattleCore.Record.InspSkill> mInspSkillUseList = new List<BattleCore.Record.InspSkill>();
      public List<BattleCore.Record.InspResult> mInspResultInspList = new List<BattleCore.Record.InspResult>();
      public List<BattleCore.Record.InspResult> mInspResultLvUpList = new List<BattleCore.Record.InspResult>();
      public BattleCore.QuestResult result;
      public int bonusFlags;
      public int allBonusFlags;
      public int bonusCount;
      public OInt[] drops;
      public OInt[] item_steals;
      public OInt[] gold_steals;
      public Json_Gift mGenesisBossResultReward;

      public bool IsZero
      {
        get
        {
          return (int) this.gold == 0 && (int) this.playerexp == 0 && ((int) this.unitexp == 0 && this.items.Count <= 0) && (int) this.multicoin == 0;
        }
      }

      public bool IsMissionClearAll(bool onlyCurrentBattle)
      {
        int num = !onlyCurrentBattle ? this.allBonusFlags : this.bonusFlags;
        bool flag = true;
        for (int index = 0; index < this.bonusCount; ++index)
        {
          if ((num & 1 << index) != 0)
            flag = false;
        }
        return flag;
      }

      public class InspSkill
      {
        public List<Unit.UnitInsp> mUnitInspList = new List<Unit.UnitInsp>();
        public UnitData mUnitData;

        public InspSkill(UnitData unit_data, Unit.UnitInsp unit_insp)
        {
          this.mUnitData = unit_data;
          this.mUnitInspList.Add(unit_insp);
        }
      }

      public class InspResult
      {
        public UnitData mUnitData;
        public ArtifactData mArtifactData;
        public AbilityData mAbilityData;

        public InspResult(UnitData unit_data, ArtifactData artifact, AbilityData ability)
        {
          this.mUnitData = unit_data;
          this.mArtifactData = artifact;
          this.mAbilityData = ability;
        }
      }
    }

    public enum RESUME_STATE
    {
      NONE,
      REQUEST,
      WAIT,
    }

    public delegate void LogCallback(string s);

    public class Json_BattleCont
    {
      public long btlid;
      public BattleCore.Json_BtlInfo btlinfo;
      public Json_PlayerData player;
    }

    public class Json_Battle
    {
      public long btlid;
      public BattleCore.Json_BtlInfo btlinfo;
      public Json_Unit[] coloenemyunits;
      public int is_rehash;
      public int is_timeover;
    }

    public class Json_BtlReward
    {
      public int gold;
    }

    public class Json_BtlInfo
    {
      public string qid;
      public BattleCore.Json_BtlUnit[] units;
      public Json_Support help;
      public BattleCore.Json_BtlDrop[] drops;
      public BattleCore.Json_BtlSteal[] steals;
      public BattleCore.Json_BtlReward[] rewards;
      public string key;
      public int seed;
      public int[] atkmags;
      public string[] campaigns;
      public long start_at;
      public int multi_floor;
      public int roomid;
      public BattleCore.Json_BtlInfoRankingQuest quest_ranking;
      public BattleCore.Json_BtlOrdeal[] ordeals;
      public BattleCore.JSON_RankMatchRank myRank;
      public BattleCore.JSON_RankMatchRank enemyRank;
      public RandDeckResult[] lot_enemies;
      public int area_id;
      public int boss_id;
      public int round;
      public JSON_RaidBossInfo boss_info;
      public BattleCore.Json_BtlInspSlot[] sins;
      public BattleCore.Json_GenesisEnemy[] enemies;
      public Json_Item[] items;

      public virtual RandDeckResult[] GetDeck()
      {
        return (RandDeckResult[]) null;
      }

      public bool IsMultiTowerQuest
      {
        get
        {
          return this.multi_floor != 0;
        }
      }

      public bool IsRaidQuest
      {
        get
        {
          return this.boss_id != 0;
        }
      }

      public bool IsOrdeal
      {
        get
        {
          if (this.ordeals != null)
            return this.ordeals.Length > 0;
          return false;
        }
      }

      public virtual QuestParam GetQuestParam()
      {
        if (this.IsMultiTowerQuest)
          return MonoSingleton<GameManager>.Instance.GetMTFloorParam(this.qid, this.multi_floor).GetQuestParam();
        if (!this.IsRaidQuest)
          return MonoSingleton<GameManager>.Instance.FindQuest(this.qid);
        RaidBossParam raidBoss = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(this.boss_id);
        if (raidBoss != null)
          return MonoSingleton<GameManager>.Instance.FindQuest(raidBoss.QuestIName);
        return (QuestParam) null;
      }

      public List<UnitData> GetPlayerSideUnits()
      {
        List<UnitData> unitDataList;
        if (PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay)
          unitDataList = this.GetMultiPlayUnits();
        else if (MonoSingleton<GameManager>.Instance.AudienceMode)
          unitDataList = this.GetAudienceModeUnits();
        else if (MonoSingleton<GameManager>.Instance.IsVSCpuBattle)
          unitDataList = BattleCore.Json_BtlInfo.GetCPUBattleUnits();
        else if (this.IsOrdeal)
        {
          unitDataList = this.GetOdealOwnUnits();
          List<UnitData> ordealSupportUnits = this.GetOrdealSupportUnits();
          if (ordealSupportUnits.Count > 0)
            unitDataList.AddRange((IEnumerable<UnitData>) ordealSupportUnits);
        }
        else
        {
          unitDataList = this.GetOwnUnits();
          UnitData supportUnit = this.GetSupportUnit();
          if (supportUnit != null)
            unitDataList.Add(supportUnit);
        }
        return unitDataList;
      }

      public List<UnitData> GetOwnUnits()
      {
        List<UnitData> unitDataList = new List<UnitData>();
        if (this.units == null)
          return unitDataList;
        QuestParam questParam = this.GetQuestParam();
        string[] strArray = questParam.questParty == null ? questParam.units.GetList() : ((IEnumerable<PartySlotTypeUnitPair>) questParam.questParty.GetMainSubSlots()).Where<PartySlotTypeUnitPair>((Func<PartySlotTypeUnitPair, bool>) (slot => slot.Type == PartySlotType.ForcedHero)).Select<PartySlotTypeUnitPair, string>((Func<PartySlotTypeUnitPair, string>) (slot => slot.Unit)).ToArray<string>();
        if (strArray != null && strArray.Length > 0)
        {
          for (int index = 0; index < strArray.Length; ++index)
          {
            string iname = strArray[index];
            if (!string.IsNullOrEmpty(iname))
            {
              UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(iname);
              if (unitDataByUnitId == null)
              {
                DebugUtility.LogError("player uniqueid not equal");
                return unitDataList;
              }
              unitDataList.Add(unitDataByUnitId);
            }
          }
        }
        for (int index = 0; index < this.units.Length; ++index)
        {
          BattleCore.Json_BtlUnit unit = this.units[index];
          if (unit.iid > 0)
          {
            UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) unit.iid);
            if (unitDataByUniqueId != null)
              unitDataList.Add(unitDataByUniqueId);
            else
              break;
          }
        }
        return unitDataList;
      }

      public UnitData GetSupportUnit()
      {
        if (this.help == null || this.help.unit == null)
          return (UnitData) null;
        UnitData unitData = new UnitData();
        unitData.Deserialize(this.help.unit);
        return unitData;
      }

      public List<UnitData> GetOdealOwnUnits()
      {
        List<UnitData> unitDataList = new List<UnitData>();
        if (this.ordeals == null)
          return unitDataList;
        for (int index1 = 0; index1 < this.ordeals.Length; ++index1)
        {
          BattleCore.Json_BtlOrdeal ordeal = this.ordeals[index1];
          if (ordeal != null && ordeal.units != null)
          {
            for (int index2 = 0; index2 < ordeal.units.Length; ++index2)
            {
              BattleCore.Json_BtlUnit unit = ordeal.units[index2];
              if (unit.iid > 0)
              {
                UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) unit.iid);
                if (unitDataByUniqueId != null)
                  unitDataList.Add(unitDataByUniqueId);
                else
                  break;
              }
            }
          }
        }
        return unitDataList;
      }

      public List<UnitData> GetOrdealSupportUnits()
      {
        List<UnitData> unitDataList = new List<UnitData>();
        if (this.ordeals == null)
          return unitDataList;
        for (int index1 = 0; index1 < this.ordeals.Length; ++index1)
        {
          BattleCore.Json_BtlOrdeal ordeal = this.ordeals[index1];
          if (ordeal != null && ordeal.units != null)
          {
            for (int index2 = 0; index2 < ordeal.units.Length; ++index2)
            {
              Json_Support help = ordeal.help;
              if (help != null)
              {
                UnitData unitData = new UnitData();
                unitData.Deserialize(help.unit);
                unitDataList.Add(unitData);
              }
            }
          }
        }
        return unitDataList;
      }

      public List<UnitData> GetAudienceModeUnits()
      {
        List<UnitData> unitDataList = new List<UnitData>();
        if (PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay || !MonoSingleton<GameManager>.Instance.AudienceMode)
          return unitDataList;
        UnitData[] unitDataArray = (UnitData[]) null;
        AudienceStartParam startedParam = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam();
        JSON_MyPhotonRoomParam roomParam = MonoSingleton<GameManager>.Instance.AudienceManager.GetRoomParam();
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
          int[] numArray1 = new int[length];
          int[] numArray2 = new int[length];
          VS_MODE vsmode = (VS_MODE) roomParam.vsmode;
          foreach (JSON_MyPhotonPlayerParam photonPlayerParam in photonPlayerParamList)
          {
            int num = 0;
            foreach (JSON_MyPhotonPlayerParam.UnitDataElem unit in photonPlayerParam.units)
            {
              unitDataArray[index1] = unit.unit;
              numArray1[index1] = photonPlayerParam.playerIndex;
              numArray2[index1] = unit.place;
              ++index1;
              if (vsmode == VS_MODE.THREE_ON_THREE && ++num >= VersusRuleParam.THREE_ON_THREE)
                break;
            }
          }
        }
        for (int index = 0; index < unitDataArray.Length; ++index)
        {
          if (unitDataArray[index] != null)
            unitDataList.Add(unitDataArray[index]);
        }
        return unitDataList;
      }

      public static List<UnitData> GetCPUBattleUnits()
      {
        List<UnitData> unitDataList = new List<UnitData>();
        if (PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay || MonoSingleton<GameManager>.Instance.AudienceMode || !MonoSingleton<GameManager>.Instance.IsVSCpuBattle)
          return unitDataList;
        PartyData partyCurrent = MonoSingleton<GameManager>.Instance.Player.GetPartyCurrent();
        VersusCpuData versusCpu = (VersusCpuData) GlobalVars.VersusCpu;
        int index1 = 0;
        int length = partyCurrent.MAX_UNIT + versusCpu.Units.Length;
        UnitData[] unitDataArray = new UnitData[length];
        int[] numArray1 = new int[length];
        int[] numArray2 = new int[length];
        for (int index2 = 0; index2 < partyCurrent.MAX_UNIT; ++index2)
        {
          long unitUniqueId = partyCurrent.GetUnitUniqueID(index2);
          if (unitUniqueId > 0L)
          {
            unitDataArray[index1] = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(unitUniqueId);
            numArray1[index1] = 1;
            numArray2[index1] = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.VERSUS_ID_KEY + (object) index2, 0);
            ++index1;
          }
        }
        for (int index2 = 0; index2 < versusCpu.Units.Length; ++index2)
        {
          if (versusCpu.Units[index2] != null)
          {
            unitDataArray[index1] = versusCpu.Units[index2];
            numArray1[index1] = 2;
            numArray2[index1] = versusCpu.Place[index2];
            ++index1;
          }
        }
        for (int index2 = 0; index2 < unitDataArray.Length; ++index2)
        {
          if (unitDataArray[index2] != null)
            unitDataList.Add(unitDataArray[index2]);
        }
        return unitDataList;
      }

      public List<UnitData> GetMultiPlayUnits()
      {
        List<UnitData> unitDataList = new List<UnitData>();
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (!instance.IsMultiPlay)
          return unitDataList;
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
          int[] numArray1 = new int[length];
          int[] numArray2 = new int[length];
          VS_MODE vsmode = (VS_MODE) myPhotonRoomParam.vsmode;
          foreach (JSON_MyPhotonPlayerParam photonPlayerParam in myPlayersStarted)
          {
            int num = 0;
            foreach (JSON_MyPhotonPlayerParam.UnitDataElem unit in photonPlayerParam.units)
            {
              unitDataArray[index] = unit.unit;
              numArray1[index] = photonPlayerParam.playerIndex;
              numArray2[index] = unit.place;
              ++index;
              if (vsmode == VS_MODE.THREE_ON_THREE && ++num >= VersusRuleParam.THREE_ON_THREE)
                break;
            }
          }
        }
        else
        {
          int totalUnitNum = JSON_MyPhotonRoomParam.GetTotalUnitNum(MonoSingleton<GameManager>.Instance.FindQuest(this.qid));
          unitDataArray = new UnitData[totalUnitNum];
          int[] numArray1 = new int[totalUnitNum];
          int[] numArray2 = new int[totalUnitNum];
          if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.TOWER)
          {
            int num1 = 0;
            foreach (JSON_MyPhotonPlayerParam photonPlayerParam in myPlayersStarted)
            {
              int num2 = myPhotonRoomParam != null ? myPhotonRoomParam.GetUnitSlotNum(photonPlayerParam.playerIndex) : 0;
              int num3 = num1;
              num1 += num2;
              foreach (JSON_MyPhotonPlayerParam.UnitDataElem unit in photonPlayerParam.units)
              {
                if (unit.slotID >= 0 && unit.slotID < num2)
                {
                  int index = num3 + unit.slotID;
                  unitDataArray[index] = unit.unit;
                  numArray1[index] = photonPlayerParam.playerIndex;
                  numArray2[index] = unit.place;
                }
              }
            }
          }
          else
          {
            int num1 = 0;
            foreach (JSON_MyPhotonPlayerParam photonPlayerParam in myPlayersStarted)
            {
              int num2 = myPhotonRoomParam != null ? myPhotonRoomParam.GetUnitSlotNum(photonPlayerParam.playerIndex) : 0;
              int num3 = num1;
              num1 += num2;
              foreach (JSON_MyPhotonPlayerParam.UnitDataElem unit in photonPlayerParam.units)
              {
                if (unit.slotID >= 0 && unit.slotID < num2)
                {
                  int index = num3 + unit.slotID;
                  unitDataArray[index] = unit.unit;
                  numArray1[index] = photonPlayerParam.playerIndex;
                }
              }
            }
          }
        }
        for (int index = 0; index < unitDataArray.Length; ++index)
        {
          if (unitDataArray[index] != null)
            unitDataList.Add(unitDataArray[index]);
        }
        return unitDataList;
      }
    }

    public class Json_BtlUnit
    {
      public int iid;
    }

    public class Json_BtlDrop
    {
      public string iname;
      public string itype;
      public int gold;
      public int num;
      public int secret;

      public EBattleRewardType dropItemType
      {
        get
        {
          if (string.IsNullOrEmpty(this.iname))
            return EBattleRewardType.None;
          switch (this.itype)
          {
            case "item":
              return EBattleRewardType.Item;
            case "card":
              return EBattleRewardType.ConceptCard;
            default:
              return EBattleRewardType.Unknown;
          }
        }
      }
    }

    public class Json_BtlSteal
    {
      public string iname;
      public int gold;
      public int num;
    }

    public class Json_BtlInfoRankingQuest
    {
      public int type;
      public int schedule_id;
    }

    public class Json_BtlOrdeal
    {
      public BattleCore.Json_BtlUnit[] units;
      public Json_Support help;
    }

    public class JSON_RankMatchRank
    {
      public int score;
      public int type;
    }

    public class Json_BtlInspSlot
    {
      public int uiid;
      public BattleCore.Json_BtlInspArtifactSlot[] artifact;
    }

    public class Json_BtlInspArtifactSlot
    {
      public int iid;
      public int slot;
    }

    public class Json_BtlInsp
    {
      public int uiid;
      public BattleCore.Json_BtlInspArtifact[] artifact;
    }

    public class Json_BtlInspArtifact
    {
      public int iid;
    }

    [Serializable]
    public class Json_GenesisEnemy
    {
      public int eid;
      public int boss_flg;
      public int hp;
    }

    public enum QuestResult
    {
      Pending,
      Win,
      Lose,
      Retreat,
      Draw,
    }

    public struct SVector2
    {
      public int x;
      public int y;

      public SVector2(int _x_, int _y_)
      {
        this.x = _x_;
        this.y = _y_;
      }

      public SVector2(BattleCore.SVector2 v)
      {
        this.x = v.x;
        this.y = v.y;
      }

      public static BattleCore.SVector2 operator +(BattleCore.SVector2 a, BattleCore.SVector2 b)
      {
        return new BattleCore.SVector2(a.x + b.x, a.y + b.y);
      }

      public static BattleCore.SVector2 operator -(BattleCore.SVector2 a, BattleCore.SVector2 b)
      {
        return new BattleCore.SVector2(a.x - b.x, a.y - b.y);
      }

      public static BattleCore.SVector2 operator *(BattleCore.SVector2 a, BattleCore.SVector2 b)
      {
        return new BattleCore.SVector2(a.x * b.x, a.y * b.y);
      }

      public static BattleCore.SVector2 operator *(BattleCore.SVector2 a, int mul)
      {
        return new BattleCore.SVector2(a.x * mul, a.y * mul);
      }

      public static bool operator ==(BattleCore.SVector2 a, BattleCore.SVector2 b)
      {
        if (a.x == b.x)
          return a.y == b.y;
        return false;
      }

      public static bool operator !=(BattleCore.SVector2 a, BattleCore.SVector2 b)
      {
        if (a.x == b.x)
          return a.y != b.y;
        return true;
      }

      public int Length()
      {
        return BattleCore.Sqrt(this.x * this.x + this.y * this.y);
      }

      public static int DotProduct(ref BattleCore.SVector2 s, ref BattleCore.SVector2 t)
      {
        return s.x * t.x + s.y * t.y;
      }

      public override bool Equals(object obj)
      {
        if (obj is BattleCore.SVector2)
          return this == (BattleCore.SVector2) obj;
        return false;
      }

      public override int GetHashCode()
      {
        return 0;
      }
    }

    public class HitData
    {
      public int hp_damage;
      public int mp_damage;
      public int ch_damage;
      public int ca_damage;
      public int hp_heal;
      public int mp_heal;
      public int ch_heal;
      public int ca_heal;
      public bool is_critical;
      public bool is_avoid;
      public bool is_pf_avoid;
      public int critical_rate;
      public int avoid_rate;

      public HitData(int _hp_damage_, int _mp_damage_, int _ch_damage_, int _ca_damage_, int _hp_heal_, int _mp_heal_, int _ch_heal_, int _ca_heal_, bool _critical_, bool _avoid_, bool _pf_avoid_, int _critical_rate_, int _avoid_rate_)
      {
        this.hp_damage = _hp_damage_;
        this.mp_damage = _mp_damage_;
        this.ch_damage = _ch_damage_;
        this.ca_damage = _ca_damage_;
        this.hp_heal = _hp_heal_;
        this.mp_heal = _mp_heal_;
        this.ch_heal = _ch_heal_;
        this.ca_heal = _ca_heal_;
        this.is_critical = _critical_;
        this.is_avoid = _avoid_;
        this.is_pf_avoid = _pf_avoid_;
        this.critical_rate = _critical_rate_;
        this.avoid_rate = _avoid_rate_;
      }
    }

    public class ChainUnit
    {
      public List<BattleCore.HitData> hits = new List<BattleCore.HitData>(4);
      public Unit self;
    }

    public class UnitResult
    {
      public List<LogSkill.Target.CondHit> cond_hit_lists = new List<LogSkill.Target.CondHit>();
      public Unit react_unit;
      public Unit unit;
      public int hp_damage;
      public int mp_damage;
      public int hp_heal;
      public int mp_heal;
      public int avoid;
      public int critical;
      public int reaction;
    }

    public class CommandResult
    {
      public Unit self;
      public SkillData skill;
      public BattleCore.UnitResult self_effect;
      public List<BattleCore.UnitResult> targets;
      public List<BattleCore.UnitResult> reactions;
    }

    private class ShotTarget
    {
      public List<Unit> piercers = new List<Unit>();
      public Grid end;
      public double rad;
      public double height;
    }

    public enum eArenaCalcType
    {
      UNKNOWN,
      MAP_START,
      UNIT_START,
      AI,
      UNIT_END,
      CAST_SKILL_START,
      CAST_SKILL_END,
      MAP_END,
    }

    private class KnockBackTarget
    {
      public int mMoveDir = -1;
      public LogSkill.Target mLsTarget;
      public int mUnitGx;
      public int mUnitGy;
      public int mMoveLen;

      public KnockBackTarget(LogSkill.Target ls_target, int gx, int gy)
      {
        this.mLsTarget = ls_target;
        this.mUnitGx = gx;
        this.mUnitGy = gy;
        this.mMoveLen = 0;
        this.mMoveDir = -1;
      }
    }

    private class TdTarget
    {
      public Unit mUnit;
      public int mX;
      public int mY;

      public TdTarget(Unit unit, int x, int y)
      {
        this.mUnit = unit;
        this.mX = x;
        this.mY = y;
      }
    }

    public class AiCache
    {
      public BattleMap map;
      public FixParam fixparam;
      public BattleCore.SVector2 pos;
      public int cond_prio;
      public int cost_jewel;
      public Grid goal;
      public GridMap<bool> baseRangeMap;
    }

    public class SkillResult
    {
      public SkillData skill;
      public Grid movpos;
      public Grid usepos;
      public LogSkill log;
      public bool locked;
      public int score_prio;
      public int unit_prio;
      public int cost_jewel;
      public int gain_jewel;
      public int heal;
      public int heal_num;
      public int cure_num;
      public int fail_num;
      public int disable_num;
      public int cond_prio;
      public int buff;
      public int buff_num;
      public int buff_prio;
      public int buff_dup;
      public int unit_damage_t;
      public int unit_damage;
      public int unit_dead_num;
      public int ext_damage;
      public int ext_dead_num;
      public int nockback_prio;
      public int distance;
      public int teleport;
      public int ct;
      public int fail_trick;
      public int good_trick;
      public int heal_trick;
    }

    public class MoveGoalTarget
    {
      public Unit unit;
      public Vector2 goal;
      public float step;

      public static List<BattleCore.MoveGoalTarget> Create(List<Unit> targets)
      {
        List<BattleCore.MoveGoalTarget> moveGoalTargetList = new List<BattleCore.MoveGoalTarget>();
        for (int index = 0; index < targets.Count; ++index)
          moveGoalTargetList.Add(new BattleCore.MoveGoalTarget()
          {
            unit = targets[index],
            goal = Vector2.zero
          });
        return moveGoalTargetList;
      }

      public override string ToString()
      {
        return "[" + (object) this.goal.x + "," + (object) this.goal.y + "](" + (object) this.step + "):" + this.unit.UnitName;
      }
    }
  }
}
