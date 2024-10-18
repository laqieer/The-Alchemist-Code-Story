// Decompiled with JetBrains decompiler
// Type: SRPG.Unit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class Unit : BaseObject
  {
    private static string[] mStrNameUnitConds = new string[46]
    {
      "quest.BUD_COND_POISON",
      "quest.BUD_COND_PARALYSED",
      "quest.BUD_COND_STUN",
      "quest.BUD_COND_SLEEP",
      "quest.BUD_COND_CHARM",
      "quest.BUD_COND_STONE",
      "quest.BUD_COND_BLINDNESS",
      "quest.BUD_COND_DISABLESKILL",
      "quest.BUD_COND_DISABLEMOVE",
      "quest.BUD_COND_DISABLEATTACK",
      "quest.BUD_COND_ZOMBIE",
      "quest.BUD_COND_DEATHSENTENCE",
      "quest.BUD_COND_BERSERK",
      "quest.BUD_COND_DISABLEKNOCKBACK",
      "quest.BUD_COND_DISABLEBUFF",
      "quest.BUD_COND_DISABLEDEBUFF",
      "quest.BUD_COND_STOP",
      "quest.BUD_COND_FAST",
      "quest.BUD_COND_SLOW",
      "quest.BUD_COND_AUTOHEAL",
      "quest.BUD_COND_DONSOKU",
      "quest.BUD_COND_RAGE",
      "quest.BUD_COND_GOODSLEEP",
      "quest.BUD_COND_AUTOJEWEL",
      "quest.BUD_COND_DISABLEHEAL",
      "quest.BUD_COND_DISABLESINGLEATTACK",
      "quest.BUD_COND_DISABLEAREAATTACK",
      "quest.BUD_COND_DISABLEDECCT",
      "quest.BUD_COND_DISABLEINCCT",
      "quest.BUD_COND_DISABLEESAFIRE",
      "quest.BUD_COND_DISABLEESAWATER",
      "quest.BUD_COND_DISABLEESAWIND",
      "quest.BUD_COND_DISABLEESATHUNDER",
      "quest.BUD_COND_DISABLEESASHINE",
      "quest.BUD_COND_DISABLEESADARK",
      "quest.BUD_COND_DISABLEMAXDAMAGEHP",
      "quest.BUD_COND_DISABLEMAXDAMAGEMP",
      "quest.BUD_COND_DISABLESIDEATTACK",
      "quest.BUD_COND_DISABLEBACKATTACK",
      "quest.BUD_COND_DISABLEOBSTREACTION",
      "quest.BUD_COND_DISABLEFORCEDTARGETING",
      "quest.BUD_COND_SHIELD",
      "quest.BUD_COND_FORCED_TARGETING",
      "quest.BUD_COND_BE_FORCED_TARGETED",
      "quest.BUD_COND_PROTECT",
      "quest.BUD_COND_GUARD"
    };
    private static string[] mStrDescUnitConds = new string[46]
    {
      "quest.BUD_COND_DESC_POISON",
      "quest.BUD_COND_DESC_PARALYSED",
      "quest.BUD_COND_DESC_STUN",
      "quest.BUD_COND_DESC_SLEEP",
      "quest.BUD_COND_DESC_CHARM",
      "quest.BUD_COND_DESC_STONE",
      "quest.BUD_COND_DESC_BLINDNESS",
      "quest.BUD_COND_DESC_DISABLESKILL",
      "quest.BUD_COND_DESC_DISABLEMOVE",
      "quest.BUD_COND_DESC_DISABLEATTACK",
      "quest.BUD_COND_DESC_ZOMBIE",
      "quest.BUD_COND_DESC_DEATHSENTENCE",
      "quest.BUD_COND_DESC_BERSERK",
      "quest.BUD_COND_DESC_DISABLEKNOCKBACK",
      "quest.BUD_COND_DESC_DISABLEBUFF",
      "quest.BUD_COND_DESC_DISABLEDEBUFF",
      "quest.BUD_COND_DESC_STOP",
      "quest.BUD_COND_DESC_FAST",
      "quest.BUD_COND_DESC_SLOW",
      "quest.BUD_COND_DESC_AUTOHEAL",
      "quest.BUD_COND_DESC_DONSOKU",
      "quest.BUD_COND_DESC_RAGE",
      "quest.BUD_COND_DESC_GOODSLEEP",
      "quest.BUD_COND_DESC_AUTOJEWEL",
      "quest.BUD_COND_DESC_DISABLEHEAL",
      "quest.BUD_COND_DESC_DISABLESINGLEATTACK",
      "quest.BUD_COND_DESC_DISABLEAREAATTACK",
      "quest.BUD_COND_DESC_DISABLEDECCT",
      "quest.BUD_COND_DESC_DISABLEINCCT",
      "quest.BUD_COND_DESC_DISABLEESAFIRE",
      "quest.BUD_COND_DESC_DISABLEESAWATER",
      "quest.BUD_COND_DESC_DISABLEESAWIND",
      "quest.BUD_COND_DESC_DISABLEESATHUNDER",
      "quest.BUD_COND_DESC_DISABLEESASHINE",
      "quest.BUD_COND_DESC_DISABLEESADARK",
      "quest.BUD_COND_DESC_DISABLEMAXDAMAGEHP",
      "quest.BUD_COND_DESC_DISABLEMAXDAMAGEMP",
      "quest.BUD_COND_DESC_DISABLESIDEATTACK",
      "quest.BUD_COND_DESC_DISABLEBACKATTACK",
      "quest.BUD_COND_DESC_DISABLEOBSTREACTION",
      "quest.BUD_COND_DESC_DISABLEFORCEDTARGETING",
      "quest.BUD_COND_DESC_SHIELD",
      "quest.BUD_COND_DESC_FORCED_TARGETING",
      "quest.BUD_COND_DESC_BE_FORCED_TARGETED",
      "quest.BUD_COND_DESC_PROTECT",
      "quest.BUD_COND_DESC_GUARD"
    };
    public static readonly int MAX_AI = 2;
    public static OInt MAX_UNIT_CONDITION = (OInt) Enum.GetNames(typeof (EUnitCondition)).Length;
    private static OInt UNIT_INDEX = (OInt) 0;
    public static OInt UNIT_CAST_INDEX = (OInt) 0;
    public const int DIRECTION_MAX = 4;
    public static readonly int[,] DIRECTION_OFFSETS = new int[4, 2]
    {
      {
        1,
        0
      },
      {
        0,
        1
      },
      {
        -1,
        0
      },
      {
        0,
        -1
      }
    };
    public static EUnitDirection[] ReverseDirection = new EUnitDirection[5]
    {
      EUnitDirection.NegativeX,
      EUnitDirection.NegativeY,
      EUnitDirection.PositiveX,
      EUnitDirection.PositiveY,
      EUnitDirection.NegativeX
    };
    public static EUnitDirection[] LeftDirection = new EUnitDirection[5]
    {
      EUnitDirection.PositiveY,
      EUnitDirection.NegativeX,
      EUnitDirection.NegativeY,
      EUnitDirection.PositiveX,
      EUnitDirection.PositiveY
    };
    public static EUnitDirection[] RightDirection = new EUnitDirection[5]
    {
      EUnitDirection.NegativeY,
      EUnitDirection.PositiveX,
      EUnitDirection.PositiveY,
      EUnitDirection.NegativeX,
      EUnitDirection.NegativeY
    };
    public static EUnitDirection[] ForwardDirection = new EUnitDirection[5]
    {
      EUnitDirection.PositiveX,
      EUnitDirection.PositiveY,
      EUnitDirection.NegativeX,
      EUnitDirection.NegativeY,
      EUnitDirection.PositiveX
    };
    private string mUnitName;
    private string mUniqueName;
    private UnitData mUnitData;
    private EUnitSide mSide;
    private OInt mUnitFlag = (OInt) 0;
    private OInt mCommandFlag = (OInt) 0;
    private OIntVector2 mGridPosition = new OIntVector2();
    private OIntVector2 mGridPositionTurnStart = new OIntVector2();
    private OInt mTurnStartDir;
    private NPCSetting mSettingNPC;
    private OInt mUnitIndex;
    private OInt mSearched = (OInt) 0;
    private BaseStatus mMaximumStatus = new BaseStatus();
    private int mMaximumStatusHp;
    private BaseStatus mCurrentStatus = new BaseStatus();
    private int mUnitChangedHp;
    private List<AIParam> mAI = new List<AIParam>(Unit.MAX_AI);
    private OInt mAITop = (OInt) 0;
    private AIActionTable mAIActionTable = new AIActionTable();
    private OInt mAIActionIndex = (OInt) 0;
    private OInt mAIActionTurnCount = (OInt) 0;
    private AIPatrolTable mAIPatrolTable = new AIPatrolTable();
    private OInt mAIPatrolIndex = (OInt) 0;
    private SkillData mAIForceSkill;
    private MapBreakObj mBreakObj;
    private string mCreateBreakObjId;
    private int mCreateBreakObjClock;
    private int mTeamId;
    private FriendStates mFriendStates;
    private OInt mKeepHp;
    private int mInfinitySpawnTag = -1;
    private int mInfinitySpawnDeck;
    private BaseStatus mMaximumStatusWithMap = new BaseStatus();
    private OBool mNeedDead = (OBool) false;
    private DynamicTransformUnitParam mDtuParam;
    private Unit mDtuFromUnit;
    private int mDtuRemainTurn;
    private int mOverKillDamage;
    private List<BuffAttachment> mBuffAttachments = new List<BuffAttachment>(8);
    private List<CondAttachment> mCondAttachments = new List<CondAttachment>(8);
    private OLong mCurrentCondition;
    private OLong mDisableCondition;
    public Unit Target;
    public Grid TreasureGainTarget;
    public EUnitDirection Direction;
    public bool IsPartyMember;
    public bool IsSub;
    private bool mIsRaidBoss;
    private Unit.UnitDrop mDrop = new Unit.UnitDrop();
    private bool mIsWithdrawDrop;
    private Unit.UnitSteal mSteal = new Unit.UnitSteal();
    private List<Unit.UnitShield> mShields = new List<Unit.UnitShield>();
    private List<Unit.UnitMhmDamage> mMhmDamageLists = new List<Unit.UnitMhmDamage>();
    private List<Unit.UnitInsp> mInspInsList = new List<Unit.UnitInsp>();
    private List<Unit.UnitInsp> mInspUseList = new List<Unit.UnitInsp>();
    private List<Unit.UnitForcedTargeting> mFtgtTargetList = new List<Unit.UnitForcedTargeting>();
    private List<Unit.UnitForcedTargeting> mFtgtFromList = new List<Unit.UnitForcedTargeting>();
    private EventTrigger mEventTrigger;
    private List<UnitEntryTrigger> mEntryTriggers;
    private OBool mEntryTriggerAndCheck = (OBool) false;
    private OInt mWaitEntryClock = (OInt) 0;
    private OInt mMoveWaitTurn = (OInt) 0;
    private OInt mActionCount = (OInt) 0;
    private OInt mDeathCount = (OInt) 0;
    private OInt mAutoJewel = (OInt) 0;
    private OInt mChargeTime;
    private SkillData mCastSkill;
    private OInt mCastTime;
    private OInt mCastIndex;
    private Unit mUnitTarget;
    private Grid mGridTarget;
    private GridMap<bool> mCastSkillGridMap;
    private Unit mRageTarget;
    private List<Unit.UnitProtect> mProtects = new List<Unit.UnitProtect>();
    private List<Unit.UnitProtect> mGuards = new List<Unit.UnitProtect>();
    private int mProtectDamageCount;
    private Dictionary<SkillData, OInt> mSkillUseCount = new Dictionary<SkillData, OInt>();
    private List<SkillData> mJudgeHpLists = new List<SkillData>();
    private string mParentUniqueName;
    private int mTowerStartHP;
    public List<OString> mNotifyUniqueNames;
    private int mKillCount;
    private bool mDropDirection;
    private string[] mTags;
    private List<Unit.AbilityChange> mAbilityChangeLists = new List<Unit.AbilityChange>();
    private List<AbilityData> mBattleAbilitys;
    private List<SkillData> mBattleSkills;
    private List<AbilityData> mAddedAbilitys = new List<AbilityData>();
    private List<SkillData> mAddedSkills = new List<SkillData>();
    private List<SkillData> mAdditionalSkills = new List<SkillData>();
    public int OwnerPlayerIndex;
    private int mTurnCount;
    private bool mEntryUnit;
    private static BaseStatus LapBossBuffAdd = new BaseStatus();
    private static BaseStatus LapBossBuffNad = new BaseStatus();
    private static BaseStatus LapBossBuffScl = new BaseStatus();
    private static BaseStatus LapBossDebuffAdd = new BaseStatus();
    private static BaseStatus LapBossDebuffNad = new BaseStatus();
    private static BaseStatus LapBossDebuffScl = new BaseStatus();
    private static uint mCondLinkageID = 0;
    public BuffBit CondLinkageBuff = new BuffBit();
    public BuffBit CondLinkageDebuff = new BuffBit();
    private SkillData mPushCastSkill;
    private static readonly Dictionary<ParamTypes, BattleBonus> ResistStatusBuffDic = new Dictionary<ParamTypes, BattleBonus>()
    {
      {
        ParamTypes.Hp,
        BattleBonus.Resist_BuffHpMax
      },
      {
        ParamTypes.HpMax,
        BattleBonus.Resist_BuffHpMax
      },
      {
        ParamTypes.Atk,
        BattleBonus.Resist_BuffAtk
      },
      {
        ParamTypes.Def,
        BattleBonus.Resist_BuffDef
      },
      {
        ParamTypes.Mag,
        BattleBonus.Resist_BuffMag
      },
      {
        ParamTypes.Mnd,
        BattleBonus.Resist_BuffMnd
      },
      {
        ParamTypes.Rec,
        BattleBonus.Resist_BuffRec
      },
      {
        ParamTypes.Dex,
        BattleBonus.Resist_BuffDex
      },
      {
        ParamTypes.Spd,
        BattleBonus.Resist_BuffSpd
      },
      {
        ParamTypes.Cri,
        BattleBonus.Resist_BuffCri
      },
      {
        ParamTypes.Luk,
        BattleBonus.Resist_BuffLuk
      },
      {
        ParamTypes.Mov,
        BattleBonus.Resist_BuffMov
      },
      {
        ParamTypes.Jmp,
        BattleBonus.Resist_BuffJmp
      }
    };
    private static readonly Dictionary<ParamTypes, BattleBonus> ResistStatusDebuffDic = new Dictionary<ParamTypes, BattleBonus>()
    {
      {
        ParamTypes.Hp,
        BattleBonus.Resist_DebuffHpMax
      },
      {
        ParamTypes.HpMax,
        BattleBonus.Resist_DebuffHpMax
      },
      {
        ParamTypes.Atk,
        BattleBonus.Resist_DebuffAtk
      },
      {
        ParamTypes.Def,
        BattleBonus.Resist_DebuffDef
      },
      {
        ParamTypes.Mag,
        BattleBonus.Resist_DebuffMag
      },
      {
        ParamTypes.Mnd,
        BattleBonus.Resist_DebuffMnd
      },
      {
        ParamTypes.Rec,
        BattleBonus.Resist_DebuffRec
      },
      {
        ParamTypes.Dex,
        BattleBonus.Resist_DebuffDex
      },
      {
        ParamTypes.Spd,
        BattleBonus.Resist_DebuffSpd
      },
      {
        ParamTypes.Cri,
        BattleBonus.Resist_DebuffCri
      },
      {
        ParamTypes.Luk,
        BattleBonus.Resist_DebuffLuk
      },
      {
        ParamTypes.Mov,
        BattleBonus.Resist_DebuffMov
      },
      {
        ParamTypes.Jmp,
        BattleBonus.Resist_DebuffJmp
      }
    };
    private MySound.Voice mBattleVoice;
    public const int BOSS_PROVISIONAL_HP = 1000000000;
    private long mBossLongBaseMaxHP;
    private long mBossLongMaxHP;
    private long mBossLongCurHP;
    private static BaseStatus BuffWorkStatus = new BaseStatus();
    private static BaseStatus BuffWorkScaleStatus = new BaseStatus();
    private static BaseStatus DebuffWorkScaleStatus = new BaseStatus();
    private static BaseStatus PassiveWorkScaleStatus = new BaseStatus();
    private static BaseStatus BuffDupliScaleStatus = new BaseStatus();
    private static BaseStatus DebuffDupliScaleStatus = new BaseStatus();
    private static BaseStatus BuffConceptCardStatus = new BaseStatus();
    private static BaseStatus DebuffConceptCardStatus = new BaseStatus();
    private static BaseStatus BuffConceptCardScaleStatus = new BaseStatus();
    private static BaseStatus DebuffConceptCardScaleStatus = new BaseStatus();
    private static BaseStatus DupliConceptCardStatus = new BaseStatus();

    public static string[] StrNameUnitConds => Unit.mStrNameUnitConds;

    public static string[] StrDescUnitConds => Unit.mStrDescUnitConds;

    public OInt AIActionIndex => this.mAIActionIndex;

    public OInt AIActionTurnCount => this.mAIActionTurnCount;

    public OInt AIPatrolIndex => this.mAIPatrolIndex;

    public bool IsNPC => this.mSettingNPC != null;

    public bool IsRaidBoss => this.mIsRaidBoss;

    public bool IsWithdrawDrop => this.mIsWithdrawDrop;

    private string[] Tags
    {
      get
      {
        if (this.mTags != null)
          return this.mTags;
        List<string> stringList = new List<string>();
        if (this.UnitParam.tags != null)
        {
          foreach (string tag in this.UnitParam.tags)
          {
            if (!stringList.Contains(tag))
              stringList.Add(tag);
          }
        }
        if (this.Job != null && this.Job.Param != null && this.Job.Param.tags != null)
        {
          foreach (string tag in this.Job.Param.tags)
          {
            if (!stringList.Contains(tag))
              stringList.Add(tag);
          }
        }
        this.mTags = stringList.Count != 0 ? stringList.ToArray() : new string[0];
        return this.mTags;
      }
    }

    public int Gems
    {
      set
      {
        this.CurrentStatus.param.mp = (OShort) Math.Max(Math.Min(value, (int) this.MaximumStatus.param.mp), 0);
      }
      get => (int) this.CurrentStatus.param.mp;
    }

    public int WaitClock
    {
      set => this.mWaitEntryClock = (OInt) value;
      get => (int) this.mWaitEntryClock;
    }

    public int WaitMoveTurn
    {
      set => this.mMoveWaitTurn = (OInt) value;
      get => (int) this.mMoveWaitTurn;
    }

    public Dictionary<SkillData, OInt> GetSkillUseCount() => this.mSkillUseCount;

    public List<SkillData> JudgeHpLists => this.mJudgeHpLists;

    public int TurnCount
    {
      get => this.mTurnCount;
      set => this.mTurnCount = value;
    }

    public bool EntryUnit => this.mEntryUnit;

    public string UniqueName => this.mUniqueName;

    public string UnitName
    {
      get => this.mUnitName;
      set => this.mUnitName = value;
    }

    public UnitData UnitData => this.mUnitData;

    public UnitParam UnitParam => this.mUnitData.UnitParam;

    public EUnitType UnitType => this.UnitParam != null ? this.UnitParam.type : EUnitType.Unit;

    public int Lv => this.mUnitData.Lv;

    public JobData Job => this.mUnitData.CurrentJob;

    public SkillData LeaderSkill => this.mUnitData.CurrentLeaderSkill;

    public List<AbilityData> BattleAbilitys
    {
      get
      {
        if (this.mBattleAbilitys == null)
          this.RefreshBattleAbilitysAndSkills();
        return this.mBattleAbilitys;
      }
    }

    public List<SkillData> BattleSkills
    {
      get
      {
        if (this.mBattleAbilitys == null)
          this.RefreshBattleAbilitysAndSkills();
        return this.mBattleSkills;
      }
    }

    public List<BuffAttachment> BuffAttachments => this.mBuffAttachments;

    public List<CondAttachment> CondAttachments => this.mCondAttachments;

    public EquipData[] CurrentEquips => this.mUnitData.CurrentEquips;

    public AIParam AI => this.mAI.Count > 0 ? this.mAI[(int) this.mAITop] : (AIParam) null;

    public SkillData AIForceSkill => this.mAIForceSkill;

    public BaseStatus MaximumStatus => this.mMaximumStatus;

    public int MaximumStatusHp => this.mMaximumStatusHp;

    public BaseStatus CurrentStatus => this.mCurrentStatus;

    public int UnitChangedHp
    {
      get => this.mUnitChangedHp;
      set => this.mUnitChangedHp = value;
    }

    public bool IsDead
    {
      get => (int) this.CurrentStatus.param.hp == 0 && (int) this.MaximumStatus.param.hp > 0;
    }

    public bool IsEntry => this.IsUnitFlag(EUnitFlag.Entried);

    public bool NeedDead => (bool) this.mNeedDead;

    public bool IsControl
    {
      get
      {
        if (!this.IsEnableControlCondition())
          return false;
        if (PunMonoSingleton<MyPhoton>.Instance.IsMultiVersus || MonoSingleton<GameManager>.Instance.AudienceMode)
          return true;
        return this.mSide == EUnitSide.Player && this.IsPartyMember;
      }
    }

    public bool IsGimmick => this.UnitType != EUnitType.Unit;

    public bool IsIntoUnit => this.mUnitData.IsIntoUnit;

    public bool IsJump => this.mCastSkill != null && this.mCastSkill.CastType == ECastTypes.Jump;

    public EUnitSide Side
    {
      get => this.mSide;
      set => this.mSide = value;
    }

    public int UnitFlag
    {
      get => (int) this.mUnitFlag;
      set => this.mUnitFlag = (OInt) value;
    }

    public Unit.UnitDrop Drop => this.mDrop;

    public Unit.UnitSteal Steal => this.mSteal;

    public List<Unit.UnitShield> Shields => this.mShields;

    public List<Unit.UnitProtect> Protects => this.mProtects;

    public List<Unit.UnitProtect> Guards => this.mGuards;

    public int ProtectDamageCount
    {
      get => this.mProtectDamageCount;
      set => this.mProtectDamageCount = value;
    }

    public List<Unit.UnitMhmDamage> MhmDamageLists => this.mMhmDamageLists;

    public List<Unit.UnitInsp> InspInsList => this.mInspInsList;

    public List<Unit.UnitInsp> InspUseList => this.mInspUseList;

    public List<Unit.UnitForcedTargeting> FtgtTargetList => this.mFtgtTargetList;

    public List<Unit.UnitForcedTargeting> FtgtFromList => this.mFtgtFromList;

    private Unit.UnitForcedTargeting FtgtFromActive
    {
      get
      {
        return this.mFtgtFromList.Count != 0 ? this.mFtgtFromList[0] : (Unit.UnitForcedTargeting) null;
      }
    }

    public Unit FtgtFromActiveUnit
    {
      get => this.FtgtFromActive != null ? this.FtgtFromActive.mUnit : (Unit) null;
    }

    public int DisableMoveGridHeight
    {
      get => Math.Max(this.GetMoveHeight() + 1, BattleMap.MAP_FLOOR_HEIGHT);
    }

    public EElement Element => this.UnitData.Element;

    public JobTypes JobType => this.UnitData.JobType;

    public RoleTypes RoleType => this.UnitData.RoleType;

    public int x
    {
      get => (int) this.mGridPosition.x;
      set => this.mGridPosition.x = (OInt) value;
    }

    public int y
    {
      get => (int) this.mGridPosition.y;
      set => this.mGridPosition.y = (OInt) value;
    }

    public int startX
    {
      get => (int) this.mGridPositionTurnStart.x;
      set => this.mGridPositionTurnStart.x = (OInt) value;
    }

    public int startY
    {
      get => (int) this.mGridPositionTurnStart.y;
      set => this.mGridPositionTurnStart.y = (OInt) value;
    }

    public EUnitDirection startDir
    {
      get => (EUnitDirection) (int) this.mTurnStartDir;
      set => this.mTurnStartDir = (OInt) (int) value;
    }

    public NPCSetting SettingNPC => this.mSettingNPC;

    public int SizeX => this.UnitData != null ? (int) this.UnitData.UnitParam.sw : 1;

    public int SizeY => this.UnitData != null ? (int) this.UnitData.UnitParam.sh : 1;

    public int SizeZ => this.UnitData != null ? (int) this.UnitData.UnitParam.sd : 1;

    public float OffsZ => (float) this.SizeZ / 2f;

    public int MinColX => -((this.SizeX - 1) / 2);

    public int MaxColX => this.SizeX + this.MinColX;

    public int MinColY => -((this.SizeY - 1) / 2);

    public int MaxColY => this.SizeY + this.MinColY;

    public EventTrigger EventTrigger => this.mEventTrigger;

    public List<UnitEntryTrigger> EntryTriggers => this.mEntryTriggers;

    public OBool IsEntryTriggerAndCheck => this.mEntryTriggerAndCheck;

    public int ActionCount => (int) this.mActionCount;

    public int DeathCount => (int) this.mDeathCount;

    public int AutoJewel => (int) this.mAutoJewel;

    public OInt ChargeTime
    {
      get => this.mChargeTime;
      set => this.mChargeTime = value;
    }

    public OInt ChargeTimeMax
    {
      get
      {
        FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
        return fixParam == null ? (OInt) 1 : (OInt) Math.Max(SkillParam.CalcSkillEffectValue(SkillParamCalcTypes.Scale, (int) this.CurrentStatus.bonus[BattleBonus.ChargeTimeRate], (int) fixParam.ChargeTimeMax), 1);
      }
    }

    public SkillData CastSkill => this.mCastSkill;

    public OInt CastTimeMax
    {
      get
      {
        FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
        return fixParam == null ? (OInt) 1 : (OInt) Math.Max(SkillParam.CalcSkillEffectValue(SkillParamCalcTypes.Scale, (int) this.CurrentStatus.bonus[BattleBonus.CastTimeRate], (int) fixParam.ChargeTimeMax), 1);
      }
    }

    public OInt CastTime
    {
      get => this.mCastTime;
      set => this.mCastTime = value;
    }

    public OInt CastIndex => this.mCastIndex;

    public Unit UnitTarget
    {
      get => this.mUnitTarget;
      set => this.mUnitTarget = value;
    }

    public Grid GridTarget => this.mGridTarget;

    public GridMap<bool> CastSkillGridMap
    {
      get => this.mCastSkillGridMap;
      set => this.mCastSkillGridMap = value;
    }

    public Unit RageTarget => this.mRageTarget;

    public OInt UnitIndex => this.mUnitIndex;

    public string ParentUniqueName => this.mParentUniqueName;

    public List<OString> NotifyUniqueNames => this.mNotifyUniqueNames;

    public int TowerStartHP
    {
      get => this.mTowerStartHP;
      set => this.mTowerStartHP = value;
    }

    public int KillCount
    {
      get => this.mKillCount;
      set => this.mKillCount = value;
    }

    public bool IsBreakObj => this.UnitType == EUnitType.BreakObj;

    public eMapBreakClashType BreakObjClashType
    {
      get
      {
        return this.mBreakObj != null ? (eMapBreakClashType) this.mBreakObj.clash_type : eMapBreakClashType.ALL;
      }
    }

    public eMapBreakAIType BreakObjAIType
    {
      get
      {
        return this.mBreakObj != null ? (eMapBreakAIType) this.mBreakObj.ai_type : eMapBreakAIType.NONE;
      }
    }

    public eMapBreakSideType BreakObjSideType
    {
      get
      {
        return this.mBreakObj != null ? (eMapBreakSideType) this.mBreakObj.side_type : eMapBreakSideType.UNKNOWN;
      }
    }

    public eMapBreakRayType BreakObjRayType
    {
      get
      {
        return this.mBreakObj != null ? (eMapBreakRayType) this.mBreakObj.ray_type : eMapBreakRayType.PASS;
      }
    }

    public bool IsBreakDispUI => this.mBreakObj != null && this.mBreakObj.is_ui != 0;

    public string CreateBreakObjId => this.mCreateBreakObjId;

    public int CreateBreakObjClock => this.mCreateBreakObjClock;

    public int TeamId
    {
      get => this.mTeamId;
      set => this.mTeamId = value;
    }

    public FriendStates FriendStates
    {
      get => this.mFriendStates;
      set => this.mFriendStates = value;
    }

    public OInt KeepHp
    {
      get => this.mKeepHp;
      set => this.mKeepHp = value;
    }

    public int InfinitySpawnTag
    {
      get => this.mInfinitySpawnTag;
      set => this.mInfinitySpawnTag = value;
    }

    public int InfinitySpawnDeck
    {
      get => this.mInfinitySpawnDeck;
      set => this.mInfinitySpawnDeck = value;
    }

    public List<Unit.AbilityChange> AbilityChangeLists => this.mAbilityChangeLists;

    public List<AbilityData> AddedAbilitys => this.mAddedAbilitys;

    public DynamicTransformUnitParam DtuParam
    {
      get => this.mDtuParam;
      set => this.mDtuParam = value;
    }

    public Unit DtuFromUnit
    {
      get => this.mDtuFromUnit;
      set => this.mDtuFromUnit = value;
    }

    public int DtuRemainTurn
    {
      get => this.mDtuRemainTurn;
      set => this.mDtuRemainTurn = value;
    }

    public int OverKillDamage => this.mOverKillDamage;

    public eMovType MovType
    {
      get => this.Job != null && this.Job.Param != null ? this.Job.Param.MovType : eMovType.Normal;
    }

    public bool IsPassMovType => this.MovType == eMovType.Flying;

    public bool IsRiding => this.Job != null && this.Job.Param != null && this.Job.Param.IsRiding;

    private bool IsFlyingPassJob
    {
      get => this.Job == null || this.Job.Param == null || this.Job.Param.IsFlyingPass;
    }

    private bool IsFlyingPassUnit => this.UnitParam == null || this.UnitParam.IsFlyingPass;

    public bool IsFlyingPass => this.IsFlyingPassJob && this.IsFlyingPassUnit;

    private void SetupStatus()
    {
      BaseStatus dsc = new BaseStatus();
      this.mUnitData.Status.CopyTo(dsc);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) && !this.IsBreakObj)
      {
        QuestParam currentQuest = SceneBattle.Instance.CurrentQuest;
        if (currentQuest != null)
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && !string.IsNullOrEmpty(currentQuest.MapBuff))
          {
            BaseStatus status1 = new BaseStatus();
            BaseStatus status2 = new BaseStatus();
            BaseStatus status3 = new BaseStatus();
            BaseStatus status4 = new BaseStatus();
            BuffEffect buffEffect = BuffEffect.CreateBuffEffect(instance.MasterParam.GetBuffEffectParam(currentQuest.MapBuff), 0, 0);
            buffEffect.CalcBuffStatus(ref status1, this.Element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Add);
            buffEffect.CalcBuffStatus(ref status2, this.Element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Scale);
            buffEffect.CalcBuffStatus(ref status3, this.Element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Add);
            buffEffect.CalcBuffStatus(ref status4, this.Element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Scale);
            dsc.Add(status1);
            dsc.Add(status3);
            status2.Add(status4);
            dsc.AddRate(status2);
            dsc.param.ApplyMinVal();
          }
        }
      }
      dsc.CopyTo(this.mMaximumStatusWithMap);
      dsc.CopyTo(this.mMaximumStatus);
      dsc.CopyTo(this.mCurrentStatus);
      this.mMaximumStatusHp = (int) this.mMaximumStatus.param.hp;
    }

    private void SetupRaidBuffStatus(NPCSetting npc)
    {
      BaseStatus dsc = new BaseStatus();
      this.mUnitData.Status.CopyTo(dsc);
      int rankcap = 1;
      string key = string.Empty;
      if (SceneBattle.Instance.CurrentQuest.IsRaid)
      {
        RaidBossParam raidBoss = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(GlobalVars.CurrentRaidBossId.Get());
        if (raidBoss == null)
          return;
        key = raidBoss.MobBuffId;
        if ((bool) npc.is_raid_boss)
        {
          this.mIsRaidBoss = true;
          dsc.param.hp = (OInt) raidBoss.HP;
          key = raidBoss.BuffId;
        }
        RaidPeriodParam raidPeriod = MonoSingleton<GameManager>.Instance.MasterParam.GetRaidPeriod(raidBoss.PeriodId);
        if (raidPeriod == null)
          return;
        rankcap = raidPeriod.RoundBuffMax;
      }
      else if (SceneBattle.Instance.CurrentQuest.IsGuildRaid)
      {
        GuildRaidBossParam guildRaidBossParam = MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(GlobalVars.CurrentRaidBossId.Get());
        if (guildRaidBossParam == null)
          return;
        key = guildRaidBossParam.MobBuffId;
        if ((bool) npc.is_raid_boss)
        {
          this.mIsRaidBoss = true;
          dsc.param.hp = (OInt) guildRaidBossParam.HP;
          key = guildRaidBossParam.BuffId;
        }
        GuildRaidPeriodParam guildRaidPeriodParam = MonoSingleton<GameManager>.Instance.GetGuildRaidPeriodParam(guildRaidBossParam.PeriodId);
        if (guildRaidPeriodParam == null)
          return;
        rankcap = guildRaidPeriodParam.RoundBuffMax;
      }
      if ((int) GlobalVars.CurrentRaidRound > 1)
      {
        BaseStatus status1 = new BaseStatus();
        BaseStatus status2 = new BaseStatus();
        BaseStatus status3 = new BaseStatus();
        BaseStatus status4 = new BaseStatus();
        BuffEffectParam buffEffectParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBuffEffectParam(key);
        if (buffEffectParam != null)
        {
          BuffEffect buffEffect = BuffEffect.CreateBuffEffect(buffEffectParam, (int) GlobalVars.CurrentRaidRound, rankcap);
          buffEffect.CalcBuffStatus(ref status1, this.Element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Add);
          buffEffect.CalcBuffStatus(ref status2, this.Element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Scale);
          buffEffect.CalcBuffStatus(ref status3, this.Element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Add);
          buffEffect.CalcBuffStatus(ref status4, this.Element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Scale);
          dsc.Add(status1);
          dsc.Add(status3);
          status2.Add(status4);
          dsc.AddRate(status2);
          dsc.param.ApplyMinVal();
        }
      }
      dsc.CopyTo(this.mUnitData.Status);
    }

    private void SetupGvGBuffStatus(int round, int nodeID)
    {
      if (round < 1)
        return;
      BaseStatus dsc = new BaseStatus();
      this.mUnitData.Status.CopyTo(dsc);
      int num = 1;
      string key = string.Empty;
      GvGNodeParam node = GvGNodeParam.GetNode((int) GlobalVars.GvGNodeId);
      if (node != null)
      {
        key = node.ConsecutiveDebuffId;
        num = node.ConsecutiveDebuffMax;
      }
      BaseStatus status1 = new BaseStatus();
      BaseStatus status2 = new BaseStatus();
      BaseStatus status3 = new BaseStatus();
      BaseStatus status4 = new BaseStatus();
      BuffEffectParam buffEffectParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBuffEffectParam(key);
      if (buffEffectParam != null)
      {
        BuffEffect buffEffect = BuffEffect.CreateBuffEffect(buffEffectParam, round + 1, num + 1);
        buffEffect.CalcBuffStatus(ref status1, this.Element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Add);
        buffEffect.CalcBuffStatus(ref status2, this.Element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Scale);
        buffEffect.CalcBuffStatus(ref status3, this.Element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Add);
        buffEffect.CalcBuffStatus(ref status4, this.Element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Scale);
        dsc.Add(status1);
        dsc.Add(status3);
        status2.Add(status4);
        dsc.AddRate(status2);
        dsc.param.ApplyMinVal();
      }
      dsc.CopyTo(this.mUnitData.Status);
    }

    private void SetupGenAdvLapBossBuff(QuestParam quest_param, NPCSetting npc)
    {
      BuffEffectParam buffEffectParam = (BuffEffectParam) null;
      int rank = 0;
      int rankcap = 0;
      if (quest_param.IsGenesisBoss)
      {
        GenesisBossInfo.LapBossBattleInfo lapBossBattleData = GenesisBossInfo.LapBossBattleData;
        if (lapBossBattleData == null)
          return;
        rank = lapBossBattleData.Round;
        rankcap = lapBossBattleData.LapBossParam.RoundBuffMax;
        buffEffectParam = !(bool) npc.is_raid_boss ? lapBossBattleData.LapBossParam.OtherBuffParam : lapBossBattleData.LapBossParam.BossBuffParam;
      }
      else if (quest_param.IsAdvanceBoss)
      {
        AdvanceBossInfo.LapBossBattleInfo lapBossBattleData = AdvanceBossInfo.LapBossBattleData;
        if (lapBossBattleData == null)
          return;
        rank = lapBossBattleData.Round;
        rankcap = lapBossBattleData.LapBossParam.RoundBuffMax;
        buffEffectParam = !(bool) npc.is_raid_boss ? lapBossBattleData.LapBossParam.OtherBuffParam : lapBossBattleData.LapBossParam.BossBuffParam;
      }
      if (buffEffectParam == null || rank <= 1)
        return;
      BaseStatus dsc = new BaseStatus();
      this.mUnitData.Status.CopyTo(dsc);
      Unit.LapBossBuffAdd.Clear();
      Unit.LapBossBuffNad.Clear();
      Unit.LapBossBuffScl.Clear();
      Unit.LapBossDebuffAdd.Clear();
      Unit.LapBossDebuffNad.Clear();
      Unit.LapBossDebuffScl.Clear();
      BuffEffect buffEffect = BuffEffect.CreateBuffEffect(buffEffectParam, rank, rankcap);
      buffEffect.CalcBuffStatus(ref Unit.LapBossBuffAdd, this.Element, BuffTypes.Buff, true, false, SkillParamCalcTypes.Add);
      buffEffect.CalcBuffStatus(ref Unit.LapBossBuffNad, this.Element, BuffTypes.Buff, true, true, SkillParamCalcTypes.Add);
      buffEffect.CalcBuffStatus(ref Unit.LapBossBuffScl, this.Element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Scale);
      buffEffect.CalcBuffStatus(ref Unit.LapBossDebuffAdd, this.Element, BuffTypes.Debuff, true, false, SkillParamCalcTypes.Add);
      buffEffect.CalcBuffStatus(ref Unit.LapBossDebuffNad, this.Element, BuffTypes.Debuff, true, true, SkillParamCalcTypes.Add);
      buffEffect.CalcBuffStatus(ref Unit.LapBossDebuffScl, this.Element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Scale);
      dsc.Add(Unit.LapBossBuffAdd);
      dsc.Add(Unit.LapBossBuffNad);
      dsc.Add(Unit.LapBossDebuffAdd);
      dsc.Add(Unit.LapBossDebuffNad);
      Unit.LapBossBuffScl.Add(Unit.LapBossDebuffScl);
      dsc.AddRate(Unit.LapBossBuffScl);
      dsc.param.ApplyMinVal();
      dsc.CopyTo(this.mUnitData.Status);
    }

    private void SetupWorldRaidBossBuff(QuestParam quest_param, NPCSetting npc)
    {
      if (quest_param == null || npc == null || !quest_param.IsWorldRaid || !(bool) npc.is_raid_boss)
        return;
      string wr_iname = GlobalVars.CurrentWorldRaidIname.Get();
      string wrb_iname = GlobalVars.CurrentRaidBossIname.Get();
      if (string.IsNullOrEmpty(wr_iname) || string.IsNullOrEmpty(wrb_iname))
        return;
      BaseStatus dsc = new BaseStatus();
      this.mUnitData.Status.CopyTo(dsc);
      BuffEffectParam bossBuff = WorldRaidBossManager.GetBossBuff(wr_iname, wrb_iname);
      if (bossBuff != null)
      {
        Unit.LapBossBuffAdd.Clear();
        Unit.LapBossBuffNad.Clear();
        Unit.LapBossBuffScl.Clear();
        Unit.LapBossDebuffAdd.Clear();
        Unit.LapBossDebuffNad.Clear();
        Unit.LapBossDebuffScl.Clear();
        BuffEffect buffEffect = BuffEffect.CreateBuffEffect(bossBuff, 1, 1);
        buffEffect.CalcBuffStatus(ref Unit.LapBossBuffAdd, this.Element, BuffTypes.Buff, true, false, SkillParamCalcTypes.Add);
        buffEffect.CalcBuffStatus(ref Unit.LapBossBuffNad, this.Element, BuffTypes.Buff, true, true, SkillParamCalcTypes.Add);
        buffEffect.CalcBuffStatus(ref Unit.LapBossBuffScl, this.Element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Scale);
        buffEffect.CalcBuffStatus(ref Unit.LapBossDebuffAdd, this.Element, BuffTypes.Debuff, true, false, SkillParamCalcTypes.Add);
        buffEffect.CalcBuffStatus(ref Unit.LapBossDebuffNad, this.Element, BuffTypes.Debuff, true, true, SkillParamCalcTypes.Add);
        buffEffect.CalcBuffStatus(ref Unit.LapBossDebuffScl, this.Element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Scale);
        dsc.Add(Unit.LapBossBuffAdd);
        dsc.Add(Unit.LapBossBuffNad);
        dsc.Add(Unit.LapBossDebuffAdd);
        dsc.Add(Unit.LapBossDebuffNad);
        Unit.LapBossBuffScl.Add(Unit.LapBossDebuffScl);
        dsc.AddRate(Unit.LapBossBuffScl);
      }
      long max_hp = GlobalVars.CurrentWorldRaidBossHP.Get();
      if (max_hp > 0L)
      {
        WorldRaidBossData worldRaidBossData = new WorldRaidBossData();
        worldRaidBossData.Deserialize(new JSON_WorldRaidBossData()
        {
          boss_iname = GlobalVars.CurrentRaidBossIname.Get(),
          current_hp = max_hp
        });
        long num = worldRaidBossData.CalcMaxHP();
        if (num > 0L)
          max_hp = num;
        this.SetBossMaxHP(max_hp);
        dsc.param.hp = max_hp <= 1000000000L ? (OInt) (int) max_hp : (OInt) 1000000000;
      }
      dsc.param.ApplyMinVal();
      dsc.CopyTo(this.mUnitData.Status);
    }

    public bool Setup(
      UnitData unitdata = null,
      UnitSetting setting = null,
      Unit.DropItem dropitem = null,
      Unit.DropItem stealitem = null)
    {
      if (setting == null)
        return false;
      if (setting is NPCSetting)
      {
        NPCSetting npc = setting as NPCSetting;
        string iname1 = (string) npc.iname;
        int unitLevelExp = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitLevelExp((int) npc.lv);
        int rare = (int) npc.rare;
        int awake = (int) npc.awake;
        EElement elem = (EElement) (int) npc.elem;
        this.mUnitData = new UnitData();
        if (!this.mUnitData.Setup(iname1, unitLevelExp, rare, awake, elem: elem, unlockTobiraNum: TobiraParam.MAX_TOBIRA_COUNT - 1))
          return false;
        if (npc.abilities != null)
        {
          for (int i = 0; i < npc.abilities.Length; ++i)
          {
            string iname2 = (string) npc.abilities[i].iname;
            int rank = (int) npc.abilities[i].rank;
            int val1 = rank - 1;
            if (!string.IsNullOrEmpty(iname2))
            {
              AbilityParam ab_param = MonoSingleton<GameManager>.Instance.GetAbilityParam(iname2);
              if (ab_param != null)
              {
                if (ab_param.skills != null)
                {
                  AbilityData abilityData = this.mUnitData.BattleAbilitys.Find((Predicate<AbilityData>) (p => p.Param == ab_param));
                  if (abilityData != null)
                  {
                    if (abilityData.Rank < rank)
                      abilityData.Setup(this.mUnitData, abilityData.UniqueID, ab_param.iname, Math.Max(val1, 0));
                    else
                      continue;
                  }
                  else
                  {
                    abilityData = new AbilityData();
                    abilityData.Setup(this.mUnitData, (long) this.mUnitData.BattleAbilitys.Count, ab_param.iname, Math.Max(val1, 0));
                    this.mUnitData.BattleAbilitys.Add(abilityData);
                  }
                  abilityData.UpdateLearningsSkill(false);
                  for (int j = 0; j < ab_param.skills.Length; ++j)
                  {
                    SkillData skillData = this.mUnitData.BattleSkills.Find((Predicate<SkillData>) (p => p.SkillID == ab_param.skills[j].iname));
                    if (skillData == null)
                    {
                      skillData = new SkillData();
                      this.mUnitData.BattleSkills.Add(skillData);
                    }
                    skillData.Setup(ab_param.skills[j].iname, rank, abilityData.GetRankMaxCap());
                  }
                }
                if (npc.abilities[i].skills != null)
                {
                  AbilityData abilityData = this.mUnitData.BattleAbilitys.Find((Predicate<AbilityData>) (abil => abil.AbilityID == (string) npc.abilities[i].iname));
                  for (int index = 0; index < npc.abilities[i].skills.Length; ++index)
                  {
                    if (npc.abilities[i].skills[index] != null)
                    {
                      string sk_iname = (string) npc.abilities[i].skills[index].iname;
                      SkillData skillData1 = this.mUnitData.BattleSkills.Find((Predicate<SkillData>) (p => p.SkillID == sk_iname));
                      if (skillData1 != null)
                      {
                        skillData1.UseRate = npc.abilities[i].skills[index].rate;
                        skillData1.UseCondition = npc.abilities[i].skills[index].cond;
                        skillData1.CheckCount = (int) npc.abilities[i].skills[index].check_cnt > 0 | (bool) npc.control;
                        if (abilityData != null && abilityData.Skills != null)
                        {
                          SkillData skillData2 = abilityData.Skills.Find((Predicate<SkillData>) (sk => sk.SkillID == sk_iname));
                          if (skillData2 != null)
                          {
                            skillData2.UseRate = npc.abilities[i].skills[index].rate;
                            skillData2.UseCondition = npc.abilities[i].skills[index].cond;
                            skillData2.CheckCount = (int) npc.abilities[i].skills[index].check_cnt > 0 | (bool) npc.control;
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
          this.mUnitData.CalcStatus();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) && SceneBattle.Instance.CurrentQuest != null)
          {
            QuestParam currentQuest = SceneBattle.Instance.CurrentQuest;
            if (currentQuest.IsRaid || currentQuest.IsGuildRaid)
              this.SetupRaidBuffStatus(npc);
            else if (currentQuest.IsGenAdvBoss)
            {
              if ((bool) npc.is_raid_boss)
                this.mIsRaidBoss = true;
              this.SetupGenAdvLapBossBuff(currentQuest, npc);
            }
            else if (currentQuest.IsGvG)
            {
              GvGParty gvGparty = GlobalVars.GvGDefenseParty.Get();
              this.SetupGvGBuffStatus(gvGparty == null ? 0 : gvGparty.WinNum, (int) GlobalVars.GvGNodeId);
            }
            else if (currentQuest.IsWorldRaid && (bool) npc.is_raid_boss)
            {
              this.mIsRaidBoss = true;
              this.SetupWorldRaidBossBuff(currentQuest, npc);
            }
          }
          this.mAIActionTable.Clear();
          if (npc.acttbl != null && npc.acttbl.actions != null)
            npc.acttbl.CopyTo(this.mAIActionTable);
          this.mAIPatrolTable.Clear();
          if (npc.patrol != null && npc.patrol.routes != null && npc.patrol.routes.Length > 0)
            npc.patrol.CopyTo(this.mAIPatrolTable);
          if (!string.IsNullOrEmpty((string) npc.fskl))
            this.mAIForceSkill = this.mUnitData.BattleSkills.Find((Predicate<SkillData>) (p => p.SkillID == (string) npc.fskl));
        }
        if (dropitem != null)
        {
          this.mDrop.items.Clear();
          this.mDrop.items.Add(dropitem);
        }
        this.mDrop.exp = npc.exp;
        this.mDrop.gems = npc.gems;
        this.mDrop.gold = npc.gold;
        this.mDrop.gained = false;
        if (this.UnitType == EUnitType.Gem)
          this.mDrop.gems = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GemsGainValue;
        if (stealitem != null)
        {
          this.mSteal.items.Clear();
          this.mSteal.items.Add(stealitem);
        }
        this.mSteal.is_item_steeled = false;
        this.mSteal.is_gold_steeled = false;
        this.mSearched = npc.search;
        this.SetUnitFlag(EUnitFlag.DamagedActionStart, (int) npc.notice_damage != 0);
        if (npc.notice_members != null)
          this.mNotifyUniqueNames = new List<OString>((IEnumerable<OString>) npc.notice_members);
        if ((int) setting.side == 0)
          this.IsPartyMember = (bool) npc.control;
        this.mBreakObj = new MapBreakObj();
        if (npc.break_obj != null)
          npc.break_obj.CopyTo(this.mBreakObj);
        this.mNeedDead = npc.need_dead;
        this.mIsWithdrawDrop = npc.withdraw_drop;
        this.mSettingNPC = npc;
      }
      else
      {
        if (unitdata == null)
          return false;
        this.mUnitData = unitdata;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) && SceneBattle.Instance.CurrentQuest != null && SceneBattle.Instance.CurrentQuest.IsGvG)
        {
          GvGParty gvGparty = (GvGParty) null;
          if ((int) setting.side == 0)
            gvGparty = GlobalVars.GvGOffenseParty.Get();
          else if ((int) setting.side == 1)
            gvGparty = GlobalVars.GvGDefenseParty.Get();
          this.SetupGvGBuffStatus(gvGparty == null ? 0 : gvGparty.WinNum, (int) GlobalVars.GvGNodeId);
        }
        this.mSearched = (OInt) (short) this.UnitParam.search;
      }
      this.mUnitName = this.UnitParam.name;
      this.mUniqueName = (string) setting.uniqname;
      this.mParentUniqueName = (string) setting.parent;
      this.SetupStatus();
      this.mSide = !this.IsGimmick ? (EUnitSide) (int) setting.side : EUnitSide.Neutral;
      string key;
      if (!string.IsNullOrEmpty((string) setting.ai))
      {
        key = (string) setting.ai;
      }
      else
      {
        key = !this.IsGimmick ? UnitParam.AI_TYPE_DEFAULT : (string) null;
        JobData currentJob = this.mUnitData.CurrentJob;
        if (currentJob != null && !string.IsNullOrEmpty(currentJob.Param.ai))
          key = currentJob.Param.ai;
      }
      if (!string.IsNullOrEmpty(key))
      {
        AIParam aiParam = MonoSingleton<GameManager>.Instance.MasterParam.GetAIParam(key);
        DebugUtility.Assert(aiParam != null, "ai " + key + " not found");
        this.mAI.Add(aiParam);
      }
      if (setting.trigger != null)
      {
        this.mEventTrigger = new EventTrigger();
        this.mEventTrigger.Setup(setting.trigger);
      }
      if (setting.entries != null && setting.entries.Count > 0)
      {
        this.mEntryTriggers = new List<UnitEntryTrigger>((IEnumerable<UnitEntryTrigger>) setting.entries);
        this.mEntryTriggerAndCheck = (OBool) ((int) setting.entries_and != 0);
      }
      this.x = (int) setting.pos.x;
      this.y = (int) setting.pos.y;
      this.Direction = (EUnitDirection) (int) setting.dir;
      this.mWaitEntryClock = setting.waitEntryClock;
      this.mMoveWaitTurn = setting.waitMoveTurn;
      if ((int) setting.startCtVal != 0)
      {
        switch (setting.startCtCalc)
        {
          case eMapUnitCtCalcType.FIXED:
            this.mChargeTime = setting.startCtVal;
            break;
          case eMapUnitCtCalcType.SCALE:
            this.mChargeTime = (OInt) ((int) this.ChargeTimeMax * (int) setting.startCtVal / 100);
            break;
        }
        this.mChargeTime = (OInt) Math.Max((int) this.mChargeTime, 0);
      }
      if (setting.DisableFirceVoice)
        this.SetUnitFlag(EUnitFlag.DisableFirstVoice, true);
      this.mUnitIndex = Unit.UNIT_INDEX++;
      this.SetUnitFlag(EUnitFlag.Entried, this.CheckEnableEntry());
      this.mAdditionalSkills.Clear();
      this.UpdateAdditionalSkills();
      this.IsInitialized = true;
      return true;
    }

    public bool SetupDummy(
      EUnitSide side,
      string iname,
      int lv = 1,
      int rare = 0,
      int awake = 0,
      string job_iname = null,
      int job_rank = 1)
    {
      int unitLevelExp = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitLevelExp(lv);
      this.mUnitData = new UnitData();
      if (!this.mUnitData.Setup(iname, unitLevelExp, rare, awake, job_iname, job_rank))
        return false;
      this.mSide = side;
      this.mUnitName = this.UnitParam.name;
      this.mUnitData.Status.CopyTo(this.mMaximumStatus);
      this.mUnitData.Status.CopyTo(this.mCurrentStatus);
      this.mMaximumStatusHp = (int) this.mMaximumStatus.param.hp;
      this.SetUnitFlag(EUnitFlag.Entried, false);
      this.SetUnitFlag(EUnitFlag.Entried, this.CheckEnableEntry());
      this.SetupStatus();
      this.mUnitIndex = Unit.UNIT_INDEX++;
      this.mAdditionalSkills.Clear();
      this.UpdateAdditionalSkills();
      this.IsInitialized = true;
      return true;
    }

    public bool SetupDynamicTransform(
      Unit org_unit,
      DynamicTransformUnitParam dtu_param,
      bool is_no_art_cc = false)
    {
      if (org_unit == null || dtu_param == null)
        return false;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return false;
      UnitParam unitParam = instance.MasterParam.GetUnitParam(dtu_param.TrUnitId);
      if (unitParam == null)
        return false;
      string trUnitId = dtu_param.TrUnitId;
      int exp = org_unit.UnitData.Exp;
      int rarity = org_unit.UnitData.Rarity;
      int awakeLv = org_unit.UnitData.AwakeLv;
      EElement element = unitParam.element;
      string job_iname = (string) null;
      int rank = org_unit.UnitData.CurrentJob.Rank;
      if (unitParam.jobsets != null && unitParam.jobsets.Length > 0 && !string.IsNullOrEmpty(unitParam.jobsets[0]))
      {
        JobSetParam jobSetParam = instance.GetJobSetParam(unitParam.jobsets[0]);
        if (jobSetParam != null && !string.IsNullOrEmpty(jobSetParam.job))
          job_iname = jobSetParam.job;
      }
      UnitData unitData = new UnitData();
      if (!unitData.Setup(trUnitId, exp, rarity, awakeLv, job_iname, rank, element))
        return false;
      EquipData[] equips1 = org_unit.UnitData.CurrentJob.Equips;
      EquipData[] equips2 = unitData.CurrentJob.Equips;
      if (equips1 != null && equips1.Length != 0 && equips2 != null && equips2.Length != 0 && equips1.Length == equips2.Length)
      {
        for (int index = 0; index < equips1.Length; ++index)
        {
          if (equips1[index].IsEquiped())
            equips2[index].Equip(new Json_Equip()
            {
              iid = (long) (index + 1),
              iname = equips2[index].ItemParam.iname,
              exp = equips2[index].GetNeedExp(equips1[index].Rank)
            });
        }
      }
      this.mUnitData = unitData;
      if (!is_no_art_cc)
      {
        ArtifactData[] artifactDatas = org_unit.UnitData.CurrentJob.ArtifactDatas;
        for (int slot = 0; slot < artifactDatas.Length; ++slot)
          this.mUnitData.CurrentJob.SetEquipArtifact(slot, artifactDatas[slot], true);
        this.mUnitData.ConceptCards = org_unit.UnitData.ConceptCards;
        this.mUnitData.DtuTobiraCopyTo(org_unit.UnitData);
        this.mUnitData.DtuSetupSkillAbilityDerive();
        this.mUnitData.EquipRunes = org_unit.UnitData.EquipRunes;
      }
      this.mUnitData.BattleAbilitys.Clear();
      this.mUnitData.BattleSkills.Clear();
      if (!string.IsNullOrEmpty(dtu_param.UpperToAbId))
      {
        int ab_rank = 1;
        AbilityData abilityData = org_unit.BattleAbilitys.Find((Predicate<AbilityData>) (ba => ba.SlotType == EAbilitySlot.Action && ba.Param.is_fixed));
        if (abilityData != null)
          ab_rank = abilityData.Rank;
        this.SetupDynamicTransformEntryAbilityAndSkill(dtu_param.UpperToAbId, ab_rank);
      }
      if (!string.IsNullOrEmpty(dtu_param.LowerToAbId))
      {
        int ab_rank = 1;
        AbilityData abilityData = org_unit.BattleAbilitys.Find((Predicate<AbilityData>) (ba => ba.SlotType == EAbilitySlot.Action && !ba.Param.is_fixed));
        if (abilityData != null)
          ab_rank = abilityData.Rank;
        this.SetupDynamicTransformEntryAbilityAndSkill(dtu_param.LowerToAbId, ab_rank);
      }
      if (!string.IsNullOrEmpty(dtu_param.ReactToAbId))
      {
        int ab_rank = 1;
        AbilityData abilityData = org_unit.BattleAbilitys.Find((Predicate<AbilityData>) (ba => ba.SlotType == EAbilitySlot.Reaction));
        if (abilityData != null)
          ab_rank = abilityData.Rank;
        this.SetupDynamicTransformEntryAbilityAndSkill(dtu_param.ReactToAbId, ab_rank);
      }
      List<AbilityData> all = org_unit.BattleAbilitys.FindAll((Predicate<AbilityData>) (ba => ba.SlotType == EAbilitySlot.Support));
      if (all != null)
      {
        for (int index = 0; index < all.Count; ++index)
        {
          AbilityData abilityData = all[index];
          this.SetupDynamicTransformEntryAbilityAndSkill(abilityData.Param.iname, abilityData.Rank);
        }
      }
      this.mUnitData.AddUnitBattleAbilityTransform();
      this.mUnitData.CalcStatus();
      this.mSearched = (OInt) (short) this.UnitParam.search;
      this.mUnitName = this.UnitParam.name;
      this.mUniqueName = org_unit.mUniqueName;
      this.mParentUniqueName = org_unit.mParentUniqueName;
      this.SetupStatus();
      this.mSide = org_unit.mSide;
      string key = UnitParam.AI_TYPE_DEFAULT;
      JobData currentJob = this.mUnitData.CurrentJob;
      if (currentJob != null && !string.IsNullOrEmpty(currentJob.Param.ai))
        key = currentJob.Param.ai;
      AIParam aiParam = MonoSingleton<GameManager>.Instance.MasterParam.GetAIParam(key);
      DebugUtility.Assert(aiParam != null, "Unit/SetupDynamicTransform ai:" + key + " not found");
      this.mAI.Add(aiParam);
      this.x = org_unit.x;
      this.y = org_unit.y;
      this.Direction = org_unit.Direction;
      this.mUnitIndex = Unit.UNIT_INDEX++;
      this.mAdditionalSkills.Clear();
      this.UpdateAdditionalSkills();
      this.IsInitialized = true;
      return true;
    }

    private void SetupDynamicTransformEntryAbilityAndSkill(string ab_id, int ab_rank)
    {
      AbilityParam ab_param = MonoSingleton<GameManager>.Instance.GetAbilityParam(ab_id);
      if (ab_param == null)
        return;
      int val1 = ab_rank - 1;
      if (ab_param.skills == null)
        return;
      AbilityData abilityData = this.mUnitData.BattleAbilitys.Find((Predicate<AbilityData>) (p => p.Param == ab_param));
      if (abilityData != null)
      {
        if (abilityData.Rank >= ab_rank)
          return;
        abilityData.Setup(this.mUnitData, abilityData.UniqueID, ab_param.iname, Math.Max(val1, 0));
      }
      else
      {
        abilityData = new AbilityData();
        abilityData.Setup(this.mUnitData, (long) this.mUnitData.BattleAbilitys.Count, ab_param.iname, Math.Max(val1, 0));
        this.mUnitData.BattleAbilitys.Add(abilityData);
      }
      abilityData.UpdateLearningsSkill(true);
      for (int index = 0; index < abilityData.Skills.Count; ++index)
      {
        SkillData skill = abilityData.Skills[index];
        if (skill != null && !this.mUnitData.BattleSkills.Contains(skill))
          this.mUnitData.BattleSkills.Add(skill);
      }
    }

    public bool SetupResume(
      MultiPlayResumeUnitData data,
      Unit target,
      Unit rage,
      Unit casttarget,
      List<MultiPlayResumeSkillData> buffskill,
      List<MultiPlayResumeSkillData> condskill)
    {
      if (this.UnitName != data.name)
        return false;
      SceneBattle instance1 = SceneBattle.Instance;
      BattleCore battleCore = (BattleCore) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance1, (UnityEngine.Object) null))
        battleCore = instance1.Battle;
      BaseStatus status1 = new BaseStatus();
      BaseStatus status2 = new BaseStatus();
      BaseStatus status3 = new BaseStatus();
      BaseStatus status4 = new BaseStatus();
      BaseStatus status5 = new BaseStatus();
      BaseStatus status6 = new BaseStatus();
      this.CurrentStatus.param.hp = (OInt) data.hp;
      this.mUnitChangedHp = data.chp;
      this.Gems = data.gem;
      int x1 = data.x;
      this.x = x1;
      this.startX = x1;
      int y1 = data.y;
      this.y = y1;
      this.startY = y1;
      this.startDir = this.Direction = (EUnitDirection) data.dir;
      this.Target = target;
      this.mRageTarget = rage;
      if (this.IsSub && data.entry != 0)
        this.IsSub = false;
      if (!string.IsNullOrEmpty(data.castskill))
      {
        this.mCastSkill = this.GetSkillData(data.castskill);
        this.mCastTime = (OInt) data.casttime;
        this.mCastIndex = (OInt) data.castindex;
        this.mUnitTarget = casttarget;
        if (data.ctx != -1 && data.cty != -1)
        {
          BattleMap currentMap = instance1.Battle.CurrentMap;
          if (currentMap != null)
            this.mGridTarget = currentMap[data.ctx, data.cty];
        }
        if (data.castgrid != null)
        {
          GridMap<bool> gridMap = new GridMap<bool>(data.grid_w, data.grid_h);
          for (int x2 = 0; x2 < data.grid_w; ++x2)
          {
            for (int y2 = 0; y2 < data.grid_h; ++y2)
              gridMap.set(x2, y2, data.castgrid[x2 + y2 * data.grid_w] != 0);
          }
          this.CastSkillGridMap = gridMap;
        }
      }
      this.mChargeTime = (OInt) data.chargetime;
      this.mDeathCount = (OInt) data.deathcnt;
      this.mAutoJewel = (OInt) data.autojewel;
      this.mWaitEntryClock = (OInt) data.waitturn;
      this.mMoveWaitTurn = (OInt) data.moveturn;
      this.mActionCount = (OInt) data.actcnt;
      this.mAIActionIndex = (OInt) data.aiindex;
      this.mAIActionTurnCount = (OInt) data.aiturn;
      this.mAIPatrolIndex = (OInt) data.aipatrol;
      this.mTurnCount = data.turncnt;
      if (this.mEventTrigger != null)
        this.mEventTrigger.Count = data.trgcnt;
      this.mKillCount = data.killcnt;
      this.mCreateBreakObjId = data.boi;
      this.mCreateBreakObjClock = data.boc;
      this.OwnerPlayerIndex = data.own;
      this.mInfinitySpawnTag = data.ist;
      this.mInfinitySpawnDeck = data.isd;
      if (this.mEntryTriggers != null && data.etr != null)
      {
        for (int index = 0; index < data.etr.Length && index < this.mEntryTriggers.Count; ++index)
          this.mEntryTriggers[index].on = data.etr[index] != 0;
      }
      GameManager instance2 = MonoSingleton<GameManager>.Instance;
      this.mDtuParam = instance2.MasterParam.GetDynamicTransformUnitParam(data.did);
      this.mDtuFromUnit = BattleSuspend.GetUnitFromAllUnits(battleCore, data.dfu);
      this.mDtuRemainTurn = data.drt;
      this.mOverKillDamage = data.okd;
      this.mAbilityChangeLists.Clear();
      if (data.abilchgs != null)
      {
        for (int index1 = 0; index1 < data.abilchgs.Length; ++index1)
        {
          if (data.abilchgs[index1] != null && data.abilchgs[index1].acd != null)
          {
            Unit.AbilityChange abilityChange = new Unit.AbilityChange();
            for (int index2 = 0; index2 < data.abilchgs[index1].acd.Length; ++index2)
            {
              MultiPlayResumeAbilChg.Data data1 = data.abilchgs[index1].acd[index2];
              AbilityParam abilityParam1 = instance2.MasterParam.GetAbilityParam(data1.fid);
              AbilityParam abilityParam2 = instance2.MasterParam.GetAbilityParam(data1.tid);
              if (abilityParam1 == null || abilityParam2 == null)
              {
                abilityChange = (Unit.AbilityChange) null;
                break;
              }
              abilityChange.Add(abilityParam1, abilityParam2, data1.tur, data1.irs != 0, data1.exp, data1.iif != 0);
            }
            if (abilityChange != null && abilityChange.mDataLists.Count != 0)
              this.mAbilityChangeLists.Add(abilityChange);
          }
        }
      }
      this.mAddedAbilitys.Clear();
      this.mAddedSkills.Clear();
      if (data.addedabils != null)
      {
        for (int index = 0; index < data.addedabils.Length; ++index)
        {
          MultiPlayResumeAddedAbil addedabil = data.addedabils[index];
          if (addedabil != null)
          {
            AbilityParam abilityParam = instance2.MasterParam.GetAbilityParam(addedabil.aid);
            if (abilityParam != null)
              this.CreateAddedAbilityAndSkills(abilityParam, addedabil.exp, addedabil.nct);
          }
        }
        this.RefreshBattleAbilitysAndSkills();
      }
      if (data.skillname != null)
      {
        for (int index3 = 0; index3 < data.skillname.Length; ++index3)
        {
          int offs = 0;
          for (int index4 = 0; index4 < index3; ++index4)
          {
            if (data.skillname[index3] == data.skillname[index4])
              ++offs;
          }
          SkillData skillForUseCount = this.GetSkillForUseCount(data.skillname[index3], offs);
          if (skillForUseCount != null)
            this.mSkillUseCount[skillForUseCount] = (OInt) data.skillcnt[index3];
        }
      }
      this.SuspendClearBuffCondEffects(true);
      if (data.buff != null && battleCore != null)
      {
        for (int index5 = 0; index5 < data.buff.Length; ++index5)
        {
          BuffAttachment buffAttachment1 = (BuffAttachment) null;
          if (buffskill[index5].skill != null)
          {
            SkillData skill = buffskill[index5].skill;
            SkillEffectTargets skilltarget = (SkillEffectTargets) data.buff[index5].skilltarget;
            BuffEffect buffEffect = skill.GetBuffEffect(skilltarget);
            if (buffEffect != null)
            {
              int turn = data.buff[index5].turn;
              ESkillCondition cond = buffEffect.param.cond;
              EffectCheckTargets chkTarget = buffEffect.param.chk_target;
              EffectCheckTimings chkTiming = buffEffect.param.chk_timing;
              int duplicateCount = skill.DuplicateCount;
              status1.Clear();
              status3.Clear();
              status5.Clear();
              status2.Clear();
              status4.Clear();
              status6.Clear();
              skill.BuffSkill(skill.Timing, this.UnitData.Element, status1, status3, status5, status2, status4, status6, buff_target: skilltarget, is_resume: true);
              if (buffskill[index5].user != null && buffskill[index5].user.UnitData.ConceptCards != null)
              {
                for (int index6 = 0; index6 < buffskill[index5].user.UnitData.ConceptCards.Length; ++index6)
                {
                  ConceptCardData conceptCard = buffskill[index5].user.UnitData.ConceptCards[index6];
                  if (conceptCard != null)
                  {
                    List<BuffEffect> cardSkillAddBuffs = conceptCard.GetEnableCardSkillAddBuffs(buffskill[index5].user.UnitData, skill.SkillParam);
                    BaseStatus status7 = new BaseStatus();
                    BaseStatus status8 = new BaseStatus();
                    BaseStatus status9 = new BaseStatus();
                    BaseStatus status10 = new BaseStatus();
                    BaseStatus status11 = new BaseStatus();
                    BaseStatus status12 = new BaseStatus();
                    for (int index7 = 0; index7 < cardSkillAddBuffs.Count; ++index7)
                    {
                      status7.Clear();
                      status8.Clear();
                      status9.Clear();
                      status10.Clear();
                      status11.Clear();
                      status12.Clear();
                      cardSkillAddBuffs[index7].CalcBuffStatus(ref status7, this.Element, BuffTypes.Buff, true, false, SkillParamCalcTypes.Add);
                      cardSkillAddBuffs[index7].CalcBuffStatus(ref status8, this.Element, BuffTypes.Buff, true, true, SkillParamCalcTypes.Add);
                      cardSkillAddBuffs[index7].CalcBuffStatus(ref status9, this.Element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Scale);
                      cardSkillAddBuffs[index7].CalcBuffStatus(ref status10, this.Element, BuffTypes.Debuff, true, false, SkillParamCalcTypes.Add);
                      cardSkillAddBuffs[index7].CalcBuffStatus(ref status11, this.Element, BuffTypes.Debuff, true, true, SkillParamCalcTypes.Add);
                      cardSkillAddBuffs[index7].CalcBuffStatus(ref status12, this.Element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Scale);
                      status1.Add(status7);
                      status3.Add(status8);
                      status5.Add(status9);
                      status2.Add(status10);
                      status4.Add(status11);
                      status6.Add(status12);
                    }
                  }
                }
              }
              if (skill.IsNeedDecreaseEffect())
              {
                int decreaseEffectRate = skill.DecreaseEffectRate;
                status1.SubRateRoundDown((long) decreaseEffectRate);
                status3.SubRateRoundDown((long) decreaseEffectRate);
                status5.SubRateRoundDown((long) decreaseEffectRate);
                status2.SubRateRoundDown((long) decreaseEffectRate);
                status4.SubRateRoundDown((long) decreaseEffectRate);
                status6.SubRateRoundDown((long) decreaseEffectRate);
              }
              List<Unit> aag_unit_lists = new List<Unit>();
              if (skill.SkillParam.AbsorbAndGive != eAbsorbAndGive.None)
              {
                if (data.buff[index5].atl.Count != 0)
                {
                  for (int index8 = 0; index8 < data.buff[index5].atl.Count; ++index8)
                  {
                    Unit unitFromAllUnits = BattleSuspend.GetUnitFromAllUnits((BattleCore) null, data.buff[index5].atl[index8]);
                    if (unitFromAllUnits != null)
                      aag_unit_lists.Add(unitFromAllUnits);
                  }
                }
                this.AbsorbAndGiveExchangeBuff(buffskill[index5].user, this, skill, buffEffect, aag_unit_lists, status1, status5, status2, status6);
              }
              List<BuffAttachment.ResistStatusBuff> rsb_list = (List<BuffAttachment.ResistStatusBuff>) null;
              if (data.buff[index5].rsl.Count != 0)
              {
                rsb_list = new List<BuffAttachment.ResistStatusBuff>(data.buff[index5].rsl.Count);
                for (int index9 = 0; index9 < data.buff[index5].rsl.Count; ++index9)
                  rsb_list.Add(new BuffAttachment.ResistStatusBuff((StatusTypes) data.buff[index5].rsl[index9].rst, data.buff[index5].rsl[index9].rsv));
                int[] resistStatusBuffList = BuffAttachment.GetResistStatusBuffList(rsb_list);
                if ((byte) data.buff[index5].type == (byte) 0)
                {
                  this.ApplyStatusResistBuff(resistStatusBuffList, ref status1);
                  this.ApplyStatusResistBuff(resistStatusBuffList, ref status3);
                  this.ApplyStatusResistBuff(resistStatusBuffList, ref status5);
                }
                else
                {
                  this.ApplyStatusResistBuff(resistStatusBuffList, ref status2);
                  this.ApplyStatusResistBuff(resistStatusBuffList, ref status4);
                  this.ApplyStatusResistBuff(resistStatusBuffList, ref status6);
                }
              }
              if ((byte) data.buff[index5].type == (byte) 0)
              {
                switch ((SkillParamCalcTypes) data.buff[index5].calc)
                {
                  case SkillParamCalcTypes.Add:
                    buffAttachment1 = data.buff[index5].vtp != 0 ? battleCore.CreateBuffAttachment(buffskill[index5].user, this, skill, skilltarget, buffEffect, BuffTypes.Buff, true, SkillParamCalcTypes.Add, status3, cond, turn, chkTarget, chkTiming, data.buff[index5].passive, duplicateCount) : battleCore.CreateBuffAttachment(buffskill[index5].user, this, skill, skilltarget, buffEffect, BuffTypes.Buff, false, SkillParamCalcTypes.Add, status1, cond, turn, chkTarget, chkTiming, data.buff[index5].passive, duplicateCount);
                    break;
                  case SkillParamCalcTypes.Scale:
                    buffAttachment1 = battleCore.CreateBuffAttachment(buffskill[index5].user, this, skill, skilltarget, buffEffect, BuffTypes.Buff, data.buff[index5].vtp != 0, SkillParamCalcTypes.Scale, status5, cond, turn, chkTarget, chkTiming, data.buff[index5].passive, duplicateCount);
                    break;
                }
              }
              else
              {
                switch ((SkillParamCalcTypes) data.buff[index5].calc)
                {
                  case SkillParamCalcTypes.Add:
                    buffAttachment1 = data.buff[index5].vtp != 0 ? battleCore.CreateBuffAttachment(buffskill[index5].user, this, skill, skilltarget, buffEffect, BuffTypes.Debuff, true, SkillParamCalcTypes.Add, status4, cond, turn, chkTarget, chkTiming, data.buff[index5].passive, duplicateCount) : battleCore.CreateBuffAttachment(buffskill[index5].user, this, skill, skilltarget, buffEffect, BuffTypes.Debuff, false, SkillParamCalcTypes.Add, status2, cond, turn, chkTarget, chkTiming, data.buff[index5].passive, duplicateCount);
                    break;
                  case SkillParamCalcTypes.Scale:
                    buffAttachment1 = battleCore.CreateBuffAttachment(buffskill[index5].user, this, skill, skilltarget, buffEffect, BuffTypes.Debuff, data.buff[index5].vtp != 0, SkillParamCalcTypes.Scale, status6, cond, turn, chkTarget, chkTiming, data.buff[index5].passive, duplicateCount);
                    break;
                }
              }
              if (buffAttachment1 != null)
              {
                buffAttachment1.turn = (OInt) turn;
                buffAttachment1.LinkageID = data.buff[index5].lid;
                buffAttachment1.UpBuffCount = (OInt) data.buff[index5].ubc;
                if (buffAttachment1.Param != null && (bool) buffAttachment1.Param.mIsUpBuff)
                  this.UpdateUpBuffEffect(buffAttachment1);
                buffAttachment1.AagTargetLists = aag_unit_lists;
                buffAttachment1.ResistStatusBuffList = rsb_list;
                if (buffAttachment1.skill != null && (buffAttachment1.skill.Condition != ESkillCondition.CardSkill || !buffAttachment1.skill.IsPassiveSkill()))
                {
                  int num1 = 1;
                  if (buffAttachment1.DuplicateCount > 0)
                  {
                    int num2 = 0;
                    for (int index10 = 0; index10 < this.BuffAttachments.Count; ++index10)
                    {
                      if (this.isSameBuffAttachment(this.BuffAttachments[index10], buffAttachment1))
                        ++num2;
                    }
                    num1 = Math.Max(1 + num2 - buffAttachment1.DuplicateCount, 0);
                  }
                  if (num1 > 0)
                  {
                    int num3 = 0;
                    for (int index11 = 0; index11 < this.BuffAttachments.Count; ++index11)
                    {
                      if (this.isSameBuffAttachment(this.BuffAttachments[index11], buffAttachment1))
                      {
                        this.BuffAttachments.RemoveAt(index11--);
                        if (++num3 >= num1)
                          break;
                      }
                    }
                  }
                }
                this.BuffAttachments.Add(buffAttachment1);
              }
            }
          }
          else if (buffskill[index5].user != null)
          {
            EventTrigger eventTrigger = buffskill[index5].user.EventTrigger;
            if (eventTrigger != null)
            {
              BuffAttachment buffAttachment2 = eventTrigger.MakeBuff(buffskill[index5].user, this);
              buffAttachment2.turn = (OInt) data.buff[index5].turn;
              this.BuffAttachments.Add(buffAttachment2);
            }
          }
        }
      }
      if (data.cond != null && battleCore != null)
      {
        for (int index12 = 0; index12 < data.cond.Length; ++index12)
        {
          CondEffectParam condEffectParam = (CondEffectParam) null;
          SkillEffectTargets skilltarget = (SkillEffectTargets) data.cond[index12].skilltarget;
          if (condskill[index12].skill != null)
          {
            CondEffect condEffect = condskill[index12].skill.GetCondEffect(skilltarget);
            if (condEffect != null)
              condEffectParam = condEffect.param;
          }
          if (condEffectParam == null && !string.IsNullOrEmpty(data.cond[index12].bc_id))
            condEffectParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetCondEffectParam(data.cond[index12].bc_id);
          CondAttachment condAttachment1 = battleCore.CreateCondAttachment(condskill[index12].user, this, condskill[index12].skill, skilltarget, condEffectParam, (ConditionEffectTypes) data.cond[index12].type, (ESkillCondition) data.cond[index12].condition, (EUnitCondition) data.cond[index12].calc, EffectCheckTargets.Target, (EffectCheckTimings) data.cond[index12].timing, data.cond[index12].turn, data.cond[index12].passive, data.cond[index12].curse != 0);
          if (condAttachment1 != null)
          {
            condAttachment1.CheckTarget = condskill[index12].check;
            condAttachment1.LinkageID = data.cond[index12].lid;
            condAttachment1.CondId = data.cond[index12].bc_id;
            if (condAttachment1.skill != null)
            {
              for (int index13 = 0; index13 < this.CondAttachments.Count; ++index13)
              {
                CondAttachment condAttachment2 = this.CondAttachments[index13];
                if (condAttachment2.skill != null && condAttachment2.skill.SkillID == condAttachment1.skill.SkillID && condAttachment2.CondType == condAttachment1.CondType && condAttachment2.Condition == condAttachment1.Condition)
                {
                  List<CondAttachment> condAttachments = this.CondAttachments;
                  int index14 = index13;
                  int num = index14 - 1;
                  condAttachments.RemoveAt(index14);
                  break;
                }
              }
            }
            this.CondAttachments.Add(condAttachment1);
            if (condAttachment1.LinkageID != 0U)
            {
              bool flag = false;
              if (data.buff != null)
              {
                foreach (MultiPlayResumeBuff multiPlayResumeBuff in data.buff)
                {
                  if ((int) multiPlayResumeBuff.lid == (int) condAttachment1.LinkageID)
                  {
                    this.CondLinkageBuffAttach(condAttachment1, multiPlayResumeBuff.turn);
                    flag = true;
                    break;
                  }
                }
              }
              if (!flag)
                this.CondLinkageBuffAttach(condAttachment1, (int) condAttachment1.turn);
            }
          }
        }
      }
      this.mShields.Clear();
      if (data.shields != null)
      {
        foreach (MultiPlayResumeShield shield in data.shields)
        {
          SkillParam skillParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetSkillParam(shield.inm);
          if (skillParam != null)
            this.AddShieldSuspend(skillParam, shield.nhp, shield.mhp, shield.ntu, shield.mtu, shield.drt, shield.dvl);
        }
      }
      this.ClearJudgeHpLists();
      if (data.hpis != null)
      {
        foreach (string hpi in data.hpis)
        {
          SkillData skillData = this.GetSkillData(hpi);
          if (skillData != null)
            this.AddJudgeHpLists(skillData);
        }
      }
      this.ClearMhmDamage();
      if (data.mhm_dmgs != null)
      {
        foreach (MultiPlayResumeMhmDmg mhmDmg in data.mhm_dmgs)
          this.AddMhmDamage((Unit.eTypeMhmDamage) mhmDmg.typ, mhmDmg.dmg);
      }
      this.mFtgtTargetList.Clear();
      if (data.tfl != null)
      {
        foreach (MultiPlayResumeFtgt multiPlayResumeFtgt in data.tfl)
        {
          Unit unitFromAllUnits = BattleSuspend.GetUnitFromAllUnits(battleCore, multiPlayResumeFtgt.uni);
          if (unitFromAllUnits != null)
            this.mFtgtTargetList.Add(new Unit.UnitForcedTargeting(unitFromAllUnits, multiPlayResumeFtgt.tur));
        }
      }
      this.mFtgtFromList.Clear();
      if (data.ffl != null)
      {
        foreach (MultiPlayResumeFtgt multiPlayResumeFtgt in data.ffl)
        {
          Unit unitFromAllUnits = BattleSuspend.GetUnitFromAllUnits(battleCore, multiPlayResumeFtgt.uni);
          if (unitFromAllUnits != null)
            this.mFtgtFromList.Add(new Unit.UnitForcedTargeting(unitFromAllUnits, multiPlayResumeFtgt.tur));
        }
      }
      this.ClearProtect();
      if (data.protects != null)
      {
        foreach (MultiPlayResumeProtect protect in data.protects)
          this.ResumeProtect(new BattleSuspend.Data.UnitInfo.Protect()
          {
            target = protect.target,
            skillIname = protect.skillIname,
            value = protect.value
          }, battleCore);
      }
      this.ClearGuard();
      if (data.guards != null)
      {
        foreach (MultiPlayResumeProtect guard in data.guards)
          this.ResumeProtect(new BattleSuspend.Data.UnitInfo.Protect()
          {
            target = guard.target,
            skillIname = guard.skillIname,
            value = guard.value
          }, battleCore, true);
      }
      this.CurrentStatus.param.hp = (OInt) data.hp;
      this.mUnitFlag = (OInt) data.flag;
      this.UpdateBuffEffects();
      this.UpdateCondEffects();
      this.CalcCurrentStatus();
      this.mEntryUnit = data.entry != 0;
      return true;
    }

    public SkillData SearchArtifactSkill(string skill_id)
    {
      if (string.IsNullOrEmpty(skill_id))
        return (SkillData) null;
      JobData job = this.Job;
      if (job == null)
        return (SkillData) null;
      List<ArtifactData> artifactDataList = new List<ArtifactData>();
      if (job.ArtifactDatas != null && job.ArtifactDatas.Length != 0)
        artifactDataList.AddRange((IEnumerable<ArtifactData>) job.ArtifactDatas);
      if (!string.IsNullOrEmpty(job.SelectedSkin))
      {
        ArtifactData selectedSkinData = job.GetSelectedSkinData();
        if (selectedSkinData != null)
          artifactDataList.Add(selectedSkinData);
      }
      foreach (ArtifactData artifactData in artifactDataList)
      {
        if (artifactData != null && artifactData.BattleEffectSkill != null && artifactData.BattleEffectSkill.SkillParam != null && artifactData.BattleEffectSkill.SkillParam.iname == skill_id)
          return artifactData.BattleEffectSkill;
      }
      return (SkillData) null;
    }

    public SkillData SearchItemSkill(BattleCore bc, string skill_id)
    {
      if (string.IsNullOrEmpty(skill_id))
        return (SkillData) null;
      ItemData[] mInventory = bc.mInventory;
      if (mInventory == null)
        return (SkillData) null;
      foreach (ItemData itemData in mInventory)
      {
        if (itemData != null && itemData.Skill != null && itemData.Skill.SkillParam != null && itemData.Skill.SkillParam.iname == skill_id)
          return itemData.Skill;
      }
      return (SkillData) null;
    }

    public void SuspendClearBuffCondEffects(bool is_multi = false)
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        if (is_multi && buffAttachment.skill != null)
        {
          if (buffAttachment.skill.Condition == ESkillCondition.CardSkill)
          {
            this.BuffAttachments.RemoveAt(index--);
            continue;
          }
          if ((bool) buffAttachment.IsPassive && buffAttachment.skill.DuplicateCount > 0)
          {
            this.BuffAttachments.RemoveAt(index--);
            continue;
          }
        }
        if (!(bool) buffAttachment.IsPassive && (buffAttachment.skill == null || !buffAttachment.skill.IsPassiveSkill()) && buffAttachment.skill != null && buffAttachment.skill.Timing == ESkillTiming.Auto)
          this.BuffAttachments.RemoveAt(index--);
      }
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (!(bool) condAttachment.IsPassive && (condAttachment.skill == null || !condAttachment.skill.IsPassiveSkill()))
        {
          if (condAttachment.skill != null && condAttachment.skill.Timing == ESkillTiming.Auto)
            this.CondAttachments.RemoveAt(index--);
          else if (condAttachment.UseCondition == ESkillCondition.Weather && !(bool) condAttachment.IsPassive)
            this.CondAttachments.RemoveAt(index--);
        }
      }
    }

    private void AbsorbAndGiveExchangeBuff(
      Unit self,
      Unit target,
      SkillData skill,
      BuffEffect buff_effect,
      List<Unit> aag_unit_lists,
      BaseStatus buff_add,
      BaseStatus buff_scl,
      BaseStatus debuff_add,
      BaseStatus debuff_scl)
    {
      if (self == null || target == null || skill == null || buff_effect == null || aag_unit_lists == null)
        return;
      eAbsorbAndGive absorbAndGive = skill.SkillParam.AbsorbAndGive;
      if (absorbAndGive == eAbsorbAndGive.None)
        return;
      bool flag1 = buff_effect.CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Scale);
      bool flag2 = buff_effect.CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Scale);
      if (SkillParam.IsAagTypeGive(absorbAndGive))
      {
        BaseStatus status = self.UnitData.Status;
        if (self != target && !SkillParam.IsAagTypeSame(absorbAndGive))
          buff_add.Swap(debuff_add, true);
        if (flag1)
        {
          if (self == target || SkillParam.IsAagTypeSame(absorbAndGive))
            buff_add.AddConvRate(buff_scl, status);
          else
            debuff_add.SubConvRate(buff_scl, status);
        }
        if (flag2)
        {
          if (self == target || SkillParam.IsAagTypeSame(absorbAndGive))
            debuff_add.AddConvRate(debuff_scl, status);
          else
            buff_add.SubConvRate(debuff_scl, status);
        }
        if (!SkillParam.IsAagTypeDiv(absorbAndGive) || aag_unit_lists.Count <= 1 || self == target)
          return;
        buff_add.Div(aag_unit_lists.Count);
        debuff_add.Div(aag_unit_lists.Count);
      }
      else if (self == target)
      {
        buff_add.Swap(debuff_add, true);
        if (aag_unit_lists.Count > 1)
        {
          buff_add.Mul(aag_unit_lists.Count);
          debuff_add.Mul(aag_unit_lists.Count);
        }
        for (int index = 0; index < aag_unit_lists.Count; ++index)
        {
          BaseStatus status = aag_unit_lists[index].UnitData.Status;
          if (flag1)
            debuff_add.SubConvRate(buff_scl, status);
          if (flag2)
            buff_add.SubConvRate(debuff_scl, status);
        }
      }
      else
      {
        BaseStatus status = target.UnitData.Status;
        if (flag1)
          buff_add.AddConvRate(buff_scl, status);
        if (!flag2)
          return;
        debuff_add.AddConvRate(debuff_scl, status);
      }
    }

    public bool SetupSuspend(BattleCore bc, BattleSuspend.Data.UnitInfo unit_info)
    {
      if (bc == null || unit_info == null)
        return false;
      BaseStatus status1 = new BaseStatus();
      BaseStatus status2 = new BaseStatus();
      BaseStatus status3 = new BaseStatus();
      BaseStatus status4 = new BaseStatus();
      BaseStatus status5 = new BaseStatus();
      BaseStatus status6 = new BaseStatus();
      this.CurrentStatus.param.hp = (OInt) unit_info.nhp;
      this.mUnitChangedHp = unit_info.chp;
      this.Gems = unit_info.gem;
      this.x = unit_info.ugx;
      this.y = unit_info.ugy;
      this.Direction = (EUnitDirection) unit_info.dir;
      this.UnitFlag = unit_info.ufg;
      this.IsSub = unit_info.isb;
      this.ChargeTime = (OInt) unit_info.crt;
      this.Target = BattleSuspend.GetUnitFromAllUnits(bc, unit_info.tgi);
      this.mRageTarget = BattleSuspend.GetUnitFromAllUnits(bc, unit_info.rti);
      if (!string.IsNullOrEmpty(unit_info.csi))
      {
        this.mCastSkill = this.GetSkillData(unit_info.csi);
        this.mCastTime = (OInt) unit_info.ctm;
        this.mCastIndex = (OInt) unit_info.cid;
        if (unit_info.cgm != null)
        {
          GridMap<bool> gridMap = new GridMap<bool>(unit_info.cgw, unit_info.cgh);
          if (gridMap != null)
          {
            for (int idx = 0; idx < unit_info.cgm.Length; ++idx)
              gridMap.set(idx, unit_info.cgm[idx] != 0);
            this.CastSkillGridMap = gridMap;
          }
        }
        if (bc.CurrentMap != null)
          this.mGridTarget = bc.CurrentMap[unit_info.ctx, unit_info.cty];
        this.mUnitTarget = BattleSuspend.GetUnitFromAllUnits(bc, unit_info.cti);
      }
      this.mDeathCount = (OInt) unit_info.dct;
      this.mAutoJewel = (OInt) unit_info.ajw;
      this.WaitClock = unit_info.wtt;
      this.WaitMoveTurn = unit_info.mvt;
      this.mActionCount = (OInt) unit_info.acc;
      this.mTurnCount = unit_info.tuc;
      if (this.mEventTrigger != null)
        this.mEventTrigger.Count = unit_info.trc;
      this.KillCount = unit_info.klc;
      if (this.mEntryTriggers != null && unit_info.etr != null)
      {
        for (int index = 0; index < unit_info.etr.Length && index < this.mEntryTriggers.Count; ++index)
          this.mEntryTriggers[index].on = unit_info.etr[index] != 0;
      }
      this.mAIActionIndex = (OInt) unit_info.aid;
      this.mAIActionTurnCount = (OInt) unit_info.atu;
      this.mAIPatrolIndex = (OInt) unit_info.apt;
      this.mCreateBreakObjId = unit_info.boi;
      this.mCreateBreakObjClock = unit_info.boc;
      this.mTeamId = unit_info.tid;
      this.mFriendStates = (FriendStates) unit_info.fst;
      this.mInfinitySpawnTag = unit_info.ist;
      this.mInfinitySpawnDeck = unit_info.isd;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      this.mDtuParam = instance.MasterParam.GetDynamicTransformUnitParam(unit_info.did);
      this.mDtuFromUnit = BattleSuspend.GetUnitFromAllUnits(bc, unit_info.dfu);
      this.mDtuRemainTurn = unit_info.drt;
      this.mOverKillDamage = unit_info.okd;
      this.mInspInsList.Clear();
      for (int index = 0; index < unit_info.iil.Count; ++index)
      {
        BattleSuspend.Data.UnitInfo.Insp insp = unit_info.iil[index];
        this.EntryInspIns((long) insp.aii, insp.sno, insp.val != 0, insp.cct);
      }
      this.mInspUseList.Clear();
      for (int index = 0; index < unit_info.iul.Count; ++index)
        this.EntryInspUse((long) unit_info.iul[index]);
      this.mAbilityChangeLists.Clear();
      if (unit_info.acl.Count != 0)
      {
        for (int index1 = 0; index1 < unit_info.acl.Count; ++index1)
        {
          BattleSuspend.Data.UnitInfo.AbilChg abilChg = unit_info.acl[index1];
          if (abilChg.acd.Count != 0)
          {
            Unit.AbilityChange abilityChange = new Unit.AbilityChange();
            for (int index2 = 0; index2 < abilChg.acd.Count; ++index2)
            {
              BattleSuspend.Data.UnitInfo.AbilChg.Data data = abilChg.acd[index2];
              AbilityParam abilityParam1 = instance.MasterParam.GetAbilityParam(data.fid);
              AbilityParam abilityParam2 = instance.MasterParam.GetAbilityParam(data.tid);
              if (abilityParam1 == null || abilityParam2 == null)
              {
                abilityChange = (Unit.AbilityChange) null;
                break;
              }
              abilityChange.Add(abilityParam1, abilityParam2, data.tur, data.irs != 0, data.exp, data.iif != 0);
            }
            if (abilityChange != null && abilityChange.mDataLists.Count != 0)
              this.mAbilityChangeLists.Add(abilityChange);
          }
        }
      }
      this.mAddedAbilitys.Clear();
      this.mAddedSkills.Clear();
      if (unit_info.aal.Count != 0)
      {
        for (int index = 0; index < unit_info.aal.Count; ++index)
        {
          BattleSuspend.Data.UnitInfo.AddedAbil addedAbil = unit_info.aal[index];
          AbilityParam abilityParam = instance.MasterParam.GetAbilityParam(addedAbil.aid);
          if (abilityParam != null)
            this.CreateAddedAbilityAndSkills(abilityParam, addedAbil.exp, addedAbil.nct);
        }
        this.RefreshBattleAbilitysAndSkills();
      }
      for (int index3 = 0; index3 < unit_info.sul.Count; ++index3)
      {
        BattleSuspend.Data.UnitInfo.SkillUse skillUse = unit_info.sul[index3];
        int offs = 0;
        for (int index4 = 0; index4 < index3; ++index4)
        {
          if (skillUse.sid == unit_info.sul[index4].sid)
            ++offs;
        }
        SkillData skillForUseCount = this.GetSkillForUseCount(skillUse.sid, offs);
        if (skillForUseCount != null)
          this.mSkillUseCount[skillForUseCount] = (OInt) skillUse.ctr;
      }
      this.mProtects.Clear();
      for (int index = 0; index < unit_info.protects.Count; ++index)
        this.ResumeProtect(unit_info.protects[index], bc);
      this.mGuards.Clear();
      for (int index = 0; index < unit_info.guards.Count; ++index)
        this.ResumeProtect(unit_info.guards[index], bc, true);
      this.SuspendClearBuffCondEffects();
      foreach (BattleSuspend.Data.UnitInfo.Buff buff1 in unit_info.bfl)
      {
        Unit unitFromAllUnits1 = BattleSuspend.GetUnitFromAllUnits(bc, buff1.uni);
        Unit unitFromAllUnits2 = BattleSuspend.GetUnitFromAllUnits(bc, buff1.cui);
        SkillData skill = unitFromAllUnits1 == null ? (SkillData) null : unitFromAllUnits1.GetSkillData(buff1.sid);
        if (skill == null && unitFromAllUnits1 != null && unitFromAllUnits1.IsUnitFlag(EUnitFlag.IsDynamicTransform))
        {
          Unit unit = unitFromAllUnits1;
          while (unit.IsUnitFlag(EUnitFlag.IsDynamicTransform))
          {
            unit = unit.mDtuFromUnit;
            if (unit != null)
            {
              skill = unit == null ? (SkillData) null : unit.GetSkillData(buff1.sid);
              if (skill != null)
                break;
            }
            else
              break;
          }
        }
        if (skill == null)
          skill = unitFromAllUnits1 == null ? (SkillData) null : unitFromAllUnits1.SearchArtifactSkill(buff1.sid);
        if (skill == null)
          skill = this.SearchItemSkill(bc, buff1.sid);
        if (unitFromAllUnits2 != null)
        {
          if (skill == null)
          {
            if (!buff1.ipa)
            {
              EventTrigger eventTrigger = unitFromAllUnits1.EventTrigger;
              if (eventTrigger != null)
              {
                BuffAttachment buffAttachment = eventTrigger.MakeBuff(unitFromAllUnits1, this);
                buffAttachment.turn = (OInt) buff1.tur;
                this.BuffAttachments.Add(buffAttachment);
              }
            }
          }
          else
          {
            SkillEffectTargets stg = (SkillEffectTargets) buff1.stg;
            BuffEffect buffEffect = skill.GetBuffEffect(stg);
            if (buffEffect != null && buffEffect.param != null)
            {
              if (buff1.ipa && skill != null && (skill.IsSubActuate() || this.ContainsSkillBuffAttachment(skill, unitFromAllUnits1)))
              {
                if ((bool) buffEffect.param.mIsUpBuff)
                {
                  foreach (BuffAttachment buffAttachment in this.BuffAttachments)
                  {
                    if (buffAttachment.skill != null && buffAttachment.skill.SkillID == skill.SkillID && buffAttachment.Param != null && buffAttachment.Param.iname == buffEffect.param.iname && buffAttachment.skilltarget == (SkillEffectTargets) buff1.stg)
                    {
                      buffAttachment.UpBuffCount = (OInt) buff1.ubc;
                      this.UpdateUpBuffEffect(buffAttachment);
                    }
                  }
                }
              }
              else
              {
                status1.Clear();
                status3.Clear();
                status5.Clear();
                status2.Clear();
                status4.Clear();
                status6.Clear();
                skill.BuffSkill(skill.Timing, this.UnitData.Element, status1, status3, status5, status2, status4, status6, buff_target: stg, is_resume: true);
                if (unitFromAllUnits1 != null && unitFromAllUnits1.UnitData.ConceptCards != null)
                {
                  for (int index5 = 0; index5 < unitFromAllUnits1.UnitData.ConceptCards.Length; ++index5)
                  {
                    ConceptCardData conceptCard = unitFromAllUnits1.UnitData.ConceptCards[index5];
                    if (conceptCard != null)
                    {
                      List<BuffEffect> cardSkillAddBuffs = conceptCard.GetEnableCardSkillAddBuffs(unitFromAllUnits1.UnitData, skill.SkillParam);
                      BaseStatus status7 = new BaseStatus();
                      BaseStatus status8 = new BaseStatus();
                      BaseStatus status9 = new BaseStatus();
                      BaseStatus status10 = new BaseStatus();
                      BaseStatus status11 = new BaseStatus();
                      BaseStatus status12 = new BaseStatus();
                      for (int index6 = 0; index6 < cardSkillAddBuffs.Count; ++index6)
                      {
                        status7.Clear();
                        status8.Clear();
                        status9.Clear();
                        status10.Clear();
                        status11.Clear();
                        status12.Clear();
                        cardSkillAddBuffs[index6].CalcBuffStatus(ref status7, this.Element, BuffTypes.Buff, true, false, SkillParamCalcTypes.Add);
                        cardSkillAddBuffs[index6].CalcBuffStatus(ref status8, this.Element, BuffTypes.Buff, true, true, SkillParamCalcTypes.Add);
                        cardSkillAddBuffs[index6].CalcBuffStatus(ref status9, this.Element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Scale);
                        cardSkillAddBuffs[index6].CalcBuffStatus(ref status10, this.Element, BuffTypes.Debuff, true, false, SkillParamCalcTypes.Add);
                        cardSkillAddBuffs[index6].CalcBuffStatus(ref status11, this.Element, BuffTypes.Debuff, true, true, SkillParamCalcTypes.Add);
                        cardSkillAddBuffs[index6].CalcBuffStatus(ref status12, this.Element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Scale);
                        status1.Add(status7);
                        status3.Add(status8);
                        status5.Add(status9);
                        status2.Add(status10);
                        status4.Add(status11);
                        status6.Add(status12);
                      }
                    }
                  }
                }
                if (skill.IsNeedDecreaseEffect())
                {
                  int decreaseEffectRate = skill.DecreaseEffectRate;
                  status1.SubRateRoundDown((long) decreaseEffectRate);
                  status3.SubRateRoundDown((long) decreaseEffectRate);
                  status5.SubRateRoundDown((long) decreaseEffectRate);
                  status2.SubRateRoundDown((long) decreaseEffectRate);
                  status4.SubRateRoundDown((long) decreaseEffectRate);
                  status6.SubRateRoundDown((long) decreaseEffectRate);
                }
                List<Unit> aag_unit_lists = new List<Unit>();
                if (skill.SkillParam.AbsorbAndGive != eAbsorbAndGive.None)
                {
                  if (buff1.atl.Count != 0)
                  {
                    for (int index = 0; index < buff1.atl.Count; ++index)
                    {
                      Unit unitFromAllUnits3 = BattleSuspend.GetUnitFromAllUnits(bc, buff1.atl[index]);
                      if (unitFromAllUnits3 != null)
                        aag_unit_lists.Add(unitFromAllUnits3);
                    }
                  }
                  this.AbsorbAndGiveExchangeBuff(unitFromAllUnits1, this, skill, buffEffect, aag_unit_lists, status1, status5, status2, status6);
                }
                List<BuffAttachment.ResistStatusBuff> rsb_list = (List<BuffAttachment.ResistStatusBuff>) null;
                if (buff1.rsl.Count != 0)
                {
                  rsb_list = new List<BuffAttachment.ResistStatusBuff>(buff1.rsl.Count);
                  for (int index = 0; index < buff1.rsl.Count; ++index)
                    rsb_list.Add(new BuffAttachment.ResistStatusBuff((StatusTypes) buff1.rsl[index].rst, buff1.rsl[index].rsv));
                  int[] resistStatusBuffList = BuffAttachment.GetResistStatusBuffList(rsb_list);
                  if ((byte) buff1.btp == (byte) 0)
                  {
                    this.ApplyStatusResistBuff(resistStatusBuffList, ref status1);
                    this.ApplyStatusResistBuff(resistStatusBuffList, ref status3);
                    this.ApplyStatusResistBuff(resistStatusBuffList, ref status5);
                  }
                  else
                  {
                    this.ApplyStatusResistBuff(resistStatusBuffList, ref status2);
                    this.ApplyStatusResistBuff(resistStatusBuffList, ref status4);
                    this.ApplyStatusResistBuff(resistStatusBuffList, ref status6);
                  }
                }
                ESkillCondition cond = buffEffect.param.cond;
                EffectCheckTargets chkTarget = buffEffect.param.chk_target;
                EffectCheckTimings chkTiming = buffEffect.param.chk_timing;
                int duplicateCount = skill.DuplicateCount;
                int tur = buff1.tur;
                BuffAttachment buff2 = (BuffAttachment) null;
                if ((byte) buff1.btp == (byte) 0)
                {
                  switch ((SkillParamCalcTypes) buff1.ctp)
                  {
                    case SkillParamCalcTypes.Add:
                      buff2 = buff1.vtp != 0 ? bc.CreateBuffAttachment(unitFromAllUnits1, this, skill, stg, buffEffect, BuffTypes.Buff, true, SkillParamCalcTypes.Add, status3, cond, tur, chkTarget, chkTiming, buff1.ipa, duplicateCount) : bc.CreateBuffAttachment(unitFromAllUnits1, this, skill, stg, buffEffect, BuffTypes.Buff, false, SkillParamCalcTypes.Add, status1, cond, tur, chkTarget, chkTiming, buff1.ipa, duplicateCount);
                      break;
                    case SkillParamCalcTypes.Scale:
                      buff2 = bc.CreateBuffAttachment(unitFromAllUnits1, this, skill, stg, buffEffect, BuffTypes.Buff, buff1.vtp != 0, SkillParamCalcTypes.Scale, status5, cond, tur, chkTarget, chkTiming, buff1.ipa, duplicateCount);
                      break;
                  }
                }
                else
                {
                  switch ((SkillParamCalcTypes) buff1.ctp)
                  {
                    case SkillParamCalcTypes.Add:
                      buff2 = buff1.vtp != 0 ? bc.CreateBuffAttachment(unitFromAllUnits1, this, skill, stg, buffEffect, BuffTypes.Debuff, true, SkillParamCalcTypes.Add, status4, cond, tur, chkTarget, chkTiming, buff1.ipa, duplicateCount) : bc.CreateBuffAttachment(unitFromAllUnits1, this, skill, stg, buffEffect, BuffTypes.Debuff, false, SkillParamCalcTypes.Add, status2, cond, tur, chkTarget, chkTiming, buff1.ipa, duplicateCount);
                      break;
                    case SkillParamCalcTypes.Scale:
                      buff2 = bc.CreateBuffAttachment(unitFromAllUnits1, this, skill, stg, buffEffect, BuffTypes.Debuff, buff1.vtp != 0, SkillParamCalcTypes.Scale, status6, cond, tur, chkTarget, chkTiming, buff1.ipa, duplicateCount);
                      break;
                  }
                }
                if (buff2 != null)
                {
                  buff2.turn = (OInt) tur;
                  buff2.LinkageID = buff1.lid;
                  buff2.UpBuffCount = (OInt) buff1.ubc;
                  if (buff2.Param != null && (bool) buff2.Param.mIsUpBuff)
                    this.UpdateUpBuffEffect(buff2);
                  buff2.AagTargetLists = aag_unit_lists;
                  buff2.ResistStatusBuffList = rsb_list;
                  this.BuffAttachments.Add(buff2);
                }
              }
            }
          }
        }
      }
      foreach (BattleSuspend.Data.UnitInfo.Cond cond in unit_info.cdl)
      {
        Unit unitFromAllUnits4 = BattleSuspend.GetUnitFromAllUnits(bc, cond.uni);
        Unit unitFromAllUnits5 = BattleSuspend.GetUnitFromAllUnits(bc, cond.cui);
        SkillData skill = unitFromAllUnits4 == null ? (SkillData) null : unitFromAllUnits4.GetSkillData(cond.sid);
        if (skill == null && unitFromAllUnits4 != null && unitFromAllUnits4.IsUnitFlag(EUnitFlag.IsDynamicTransform))
        {
          Unit unit = unitFromAllUnits4;
          while (unit.IsUnitFlag(EUnitFlag.IsDynamicTransform))
          {
            unit = unit.mDtuFromUnit;
            if (unit != null)
            {
              skill = unit == null ? (SkillData) null : unit.GetSkillData(cond.sid);
              if (skill != null)
                break;
            }
            else
              break;
          }
        }
        if (skill == null)
          skill = unitFromAllUnits4 == null ? (SkillData) null : unitFromAllUnits4.SearchArtifactSkill(cond.sid);
        if (skill == null)
          skill = this.SearchItemSkill(bc, cond.sid);
        if (unitFromAllUnits5 != null)
        {
          if (skill == null)
          {
            if (cond.ipa)
              continue;
          }
          else if (cond.ipa && (skill.IsSubActuate() || this.ContainsSkillCondAttachment(skill)))
            continue;
          SkillEffectTargets stg = (SkillEffectTargets) cond.stg;
          CondEffectParam condEffectParam = (CondEffectParam) null;
          if (skill != null)
          {
            CondEffect condEffect = skill.GetCondEffect(stg);
            if (condEffect != null)
              condEffectParam = condEffect.param;
          }
          if (condEffectParam == null && !string.IsNullOrEmpty(cond.cid))
            condEffectParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetCondEffectParam(cond.cid);
          CondAttachment condAttachment = bc.CreateCondAttachment(unitFromAllUnits4, this, skill, stg, condEffectParam, (ConditionEffectTypes) cond.cdt, (ESkillCondition) cond.ucd, (EUnitCondition) cond.cnd, EffectCheckTargets.Target, (EffectCheckTimings) cond.tim, cond.tur, cond.ipa, cond.icu);
          if (condAttachment != null)
          {
            condAttachment.CheckTarget = unitFromAllUnits5;
            condAttachment.LinkageID = cond.lid;
            condAttachment.CondId = cond.cid;
            this.CondAttachments.Add(condAttachment);
            if (condAttachment.LinkageID != 0U)
            {
              bool flag = false;
              foreach (BattleSuspend.Data.UnitInfo.Buff buff in unit_info.bfl)
              {
                if ((int) buff.lid == (int) condAttachment.LinkageID)
                {
                  this.CondLinkageBuffAttach(condAttachment, buff.tur);
                  flag = true;
                  break;
                }
              }
              if (!flag)
                this.CondLinkageBuffAttach(condAttachment, (int) condAttachment.turn);
            }
          }
        }
      }
      this.mShields.Clear();
      foreach (BattleSuspend.Data.UnitInfo.Shield shield in unit_info.shl)
      {
        SkillParam skillParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetSkillParam(shield.inm);
        if (skillParam != null)
          this.AddShieldSuspend(skillParam, shield.nhp, shield.mhp, shield.ntu, shield.mtu, shield.drt, shield.dvl);
      }
      this.ClearJudgeHpLists();
      foreach (string iname in unit_info.hpi)
      {
        SkillData skillData = this.GetSkillData(iname);
        if (skillData != null)
          this.AddJudgeHpLists(skillData);
      }
      this.ClearMhmDamage();
      foreach (BattleSuspend.Data.UnitInfo.MhmDmg mhmDmg in unit_info.mhl)
        this.AddMhmDamage((Unit.eTypeMhmDamage) mhmDmg.typ, mhmDmg.dmg);
      this.mFtgtTargetList.Clear();
      foreach (BattleSuspend.Data.UnitInfo.Ftgt ftgt in unit_info.tfl)
      {
        Unit unitFromAllUnits = BattleSuspend.GetUnitFromAllUnits(bc, ftgt.uni);
        if (unitFromAllUnits != null)
          this.mFtgtTargetList.Add(new Unit.UnitForcedTargeting(unitFromAllUnits, ftgt.tur));
      }
      this.mFtgtFromList.Clear();
      foreach (BattleSuspend.Data.UnitInfo.Ftgt ftgt in unit_info.ffl)
      {
        Unit unitFromAllUnits = BattleSuspend.GetUnitFromAllUnits(bc, ftgt.uni);
        if (unitFromAllUnits != null)
          this.mFtgtFromList.Add(new Unit.UnitForcedTargeting(unitFromAllUnits, ftgt.tur));
      }
      this.CurrentStatus.param.hp = (OInt) unit_info.nhp;
      this.SetBossCurrentHP(unit_info.bhp);
      this.UpdateBuffEffects();
      this.UpdateCondEffects();
      this.CalcCurrentStatus();
      return true;
    }

    public void ApplyStatusResistBuff(int[] statuses, ref BaseStatus status)
    {
      if (statuses == null || statuses.Length != StatusParam.MAX_STATUS || status == null)
        return;
      for (int index = 0; index < statuses.Length; ++index)
      {
        StatusTypes type1 = (StatusTypes) index;
        switch (type1)
        {
          case StatusTypes.Mp:
          case StatusTypes.MpIni:
            continue;
          default:
            if ((int) status.param[type1] != 0 && statuses[index] != 0)
            {
              int num = statuses[index];
              if (num > 100)
                num = 100;
              else if (num < -100)
                num = -100;
              StatusParam statusParam;
              StatusTypes type2;
              (statusParam = status.param)[type2 = type1] = (OInt) ((int) statusParam[type2] - (int) status.param[type1] * num / 100);
              continue;
            }
            continue;
        }
      }
    }

    public string[] GetTags() => this.Tags;

    public bool CheckExistMap() => !this.IsDead && this.IsEntry && !this.IsSub;

    public bool CheckCollision(Grid grid) => this.CheckCollision(grid.x, grid.y);

    public bool CheckCollision(int cx, int cy, bool is_check_exist = true)
    {
      if (is_check_exist && !this.CheckExistMap())
        return false;
      for (int minColY = this.MinColY; minColY < this.MaxColY; ++minColY)
      {
        for (int minColX = this.MinColX; minColX < this.MaxColX; ++minColX)
        {
          if (this.x + minColX == cx && this.y + minColY == cy)
            return true;
        }
      }
      return false;
    }

    public bool CheckCollision(int x0, int y0, int x1, int y1)
    {
      return this.CheckExistMap() && x1 >= this.x + this.MinColX && this.x + this.MaxColX - 1 >= x0 && y1 >= this.y + this.MinColY && this.y + this.MaxColY - 1 >= y0;
    }

    public bool CheckCollisionDirect(int tx, int ty, int cx, int cy, bool is_check_exist = true)
    {
      if (is_check_exist && !this.CheckExistMap())
        return false;
      for (int minColY = this.MinColY; minColY < this.MaxColY; ++minColY)
      {
        for (int minColX = this.MinColX; minColX < this.MaxColX; ++minColX)
        {
          if (tx + minColX == cx && ty + minColY == cy)
            return true;
        }
      }
      return false;
    }

    private bool isSameBuffAttachment(BuffAttachment sba, BuffAttachment dba)
    {
      return sba != null && dba != null && sba.skill != null && dba.skill != null && sba.skill.SkillID == dba.skill.SkillID && sba.BuffType == dba.BuffType && sba.CalcType == dba.CalcType && sba.CheckTiming == dba.CheckTiming && sba.Param != null && dba.Param != null && sba.Param.iname == dba.Param.iname && sba.IsNegativeValueIsBuff == dba.IsNegativeValueIsBuff;
    }

    public int GetBuffAttachmentDuplicateCount(BuffAttachment buff)
    {
      int attachmentDuplicateCount = 0;
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        if (this.isSameBuffAttachment(this.BuffAttachments[index], buff))
          ++attachmentDuplicateCount;
      }
      return attachmentDuplicateCount;
    }

    public List<BaseStatus> GetBuffAttachmentStatuses(BuffAttachment buff)
    {
      List<BaseStatus> attachmentStatuses = new List<BaseStatus>();
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        if (this.isSameBuffAttachment(buffAttachment, buff))
          attachmentStatuses.Add(buffAttachment.status);
      }
      return attachmentStatuses;
    }

    public bool IsIncludeBuffAttachmentResistStatusBuff(BuffAttachment buff)
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        if (this.isSameBuffAttachment(buffAttachment, buff) && buffAttachment.ResistStatusBuffList != null && buffAttachment.ResistStatusBuffList.Count != 0)
          return true;
      }
      return false;
    }

    public void SetUnitFlag(EUnitFlag tgt, bool sw)
    {
      if (sw)
      {
        Unit unit = this;
        unit.mUnitFlag = (OInt) (int) ((EUnitFlag) (int) unit.mUnitFlag | tgt);
      }
      else
      {
        Unit unit = this;
        unit.mUnitFlag = (OInt) (int) ((EUnitFlag) (int) unit.mUnitFlag & ~tgt);
      }
    }

    public bool IsUnitFlag(EUnitFlag tgt)
    {
      return ((EUnitFlag) (int) this.mUnitFlag & tgt) != (EUnitFlag) 0;
    }

    public void SetCommandFlag(EUnitCommandFlag tgt, bool sw)
    {
      if (sw)
      {
        Unit unit = this;
        unit.mCommandFlag = (OInt) (int) ((EUnitCommandFlag) (int) unit.mCommandFlag | tgt);
      }
      else
      {
        Unit unit = this;
        unit.mCommandFlag = (OInt) (int) ((EUnitCommandFlag) (int) unit.mCommandFlag & ~tgt);
      }
    }

    public bool IsCommandFlag(EUnitCommandFlag tgt)
    {
      return ((EUnitCommandFlag) (int) this.mCommandFlag & tgt) != (EUnitCommandFlag) 0;
    }

    public bool IsDying()
    {
      return (int) this.MaximumStatus.param.hp * (int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.HpDyingRate / 100 >= (int) this.CurrentStatus.param.hp;
    }

    public bool IsJudgeHP(SkillData skill) => skill != null && skill.IsJudgeHp(this);

    public int GetBaseAvoidRate()
    {
      if (this.Job == null)
        return 0;
      UnitJobOverwriteParam jobOverwriteParam = this.UnitData.GetUnitJobOverwriteParam(this.Job.JobID);
      return jobOverwriteParam != null ? jobOverwriteParam.mAvoid : this.Job.GetJobRankAvoidRate();
    }

    public int GetStartGems()
    {
      int num1 = 100;
      int num2;
      if (this.Job == null)
      {
        num2 = num1 + (this.UnitParam.no_job_status == null ? 0 : this.UnitParam.no_job_status.inimp);
      }
      else
      {
        UnitJobOverwriteParam jobOverwriteParam = this.UnitData.GetUnitJobOverwriteParam(this.Job.JobID);
        num2 = jobOverwriteParam == null ? num1 + this.Job.GetJobRankInitJewelRate() : num1 + jobOverwriteParam.mInimp;
      }
      return Math.Max((int) this.MaximumStatus.param.mp * num2 / 100 + (int) this.CurrentStatus.param.imp, 0);
    }

    public int GetGoodSleepHpHealValue()
    {
      return this.IsUnitCondition(EUnitCondition.DisableHeal) || this.IsUseBossLongHP() ? 0 : Math.Max((int) this.MaximumStatus.param.hp * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GoodSleepHpHealRate / 100, 1);
    }

    public int GetGoodSleepMpHealValue()
    {
      return Math.Max((int) this.MaximumStatus.param.mp * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GoodSleepMpHealRate / 100, 1);
    }

    public void InitializeSkillUseCount()
    {
      this.mSkillUseCount.Clear();
      for (int index1 = 0; index1 < this.BattleAbilitys.Count; ++index1)
      {
        AbilityData battleAbility = this.BattleAbilitys[index1];
        for (int index2 = 0; index2 < battleAbility.Skills.Count; ++index2)
        {
          SkillData skill = battleAbility.Skills[index2];
          if (skill.SkillParam.count > 0 && !this.mSkillUseCount.ContainsKey(skill))
            this.mSkillUseCount.Add(skill, this.GetSkillUseCountMax(skill));
        }
      }
      for (int index = 0; index < this.mAddedSkills.Count; ++index)
      {
        SkillData mAddedSkill = this.mAddedSkills[index];
        if (mAddedSkill.SkillParam.count > 0 && !this.mSkillUseCount.ContainsKey(mAddedSkill))
          this.mSkillUseCount.Add(mAddedSkill, this.GetSkillUseCountMax(mAddedSkill));
      }
    }

    public void AddSkillUseCount(AbilityData ad)
    {
      if (ad == null || ad.Skills == null)
        return;
      for (int index = 0; index < ad.Skills.Count; ++index)
      {
        SkillData skill = ad.Skills[index];
        if (skill.SkillParam.count > 0 && !this.mSkillUseCount.ContainsKey(skill))
          this.mSkillUseCount.Add(skill, this.GetSkillUseCountMax(skill));
      }
    }

    public bool CheckDamageActionStart()
    {
      return this.IsUnitFlag(EUnitFlag.DamagedActionStart) || this.NotifyUniqueNames != null && this.NotifyUniqueNames.Count > 0;
    }

    public bool CheckEnableSkillUseCount(SkillData skill)
    {
      return (!this.IsNPC || skill.CheckCount) && (skill.SkillType == ESkillType.Skill || skill.SkillType == ESkillType.Reaction) && this.mSkillUseCount.ContainsKey(skill);
    }

    public OInt GetSkillUseCount(SkillData skill)
    {
      return !this.mSkillUseCount.ContainsKey(skill) ? (OInt) 0 : this.mSkillUseCount[skill];
    }

    public OInt GetSkillUseCountMax(SkillData skill)
    {
      return (OInt) (skill.SkillParam.count + (int) this.MaximumStatus[BattleBonus.SkillUseCount]);
    }

    public void UpdateSkillUseCount(SkillData skill, int count)
    {
      if (!this.CheckEnableSkillUseCount(skill))
        return;
      int skillUseCount = (int) this.GetSkillUseCount(skill);
      this.mSkillUseCount[skill] = (OInt) Math.Min(Math.Max(skillUseCount + count, 0), (int) this.GetSkillUseCountMax(skill));
    }

    public void UpdateAllSkillUseCount(int count)
    {
      if (this.mSkillUseCount.Count == 0 || count == 0 && (this.IsUnitFlag(EUnitFlag.IsDtuExec) || (this.IsUnitFlag(EUnitFlag.UnitTransformed) || this.IsUnitFlag(EUnitFlag.IsDynamicTransform)) && this.IsDead))
        return;
      foreach (SkillData skill in this.mSkillUseCount.Keys.ToArray<SkillData>())
        this.UpdateSkillUseCount(skill, count);
    }

    public void ClearJudgeHpLists() => this.mJudgeHpLists.Clear();

    public bool IsContainsJudgeHpLists(SkillData skill)
    {
      return skill != null && this.mJudgeHpLists.Contains(skill);
    }

    public void AddJudgeHpLists(SkillData skill)
    {
      if (skill == null)
        return;
      this.mJudgeHpLists.Add(skill);
    }

    public bool RemoveJudgeHpLists(SkillData skill)
    {
      if (skill == null || !this.mJudgeHpLists.Contains(skill))
        return false;
      this.mJudgeHpLists.Remove(skill);
      return true;
    }

    public bool IsEnableBuffEffect(BuffTypes type)
    {
      switch (type)
      {
        case BuffTypes.Buff:
          return (this.IsUnitCondition(EUnitCondition.DisableBuff) ? 1 : (this.IsDisableUnitCondition(EUnitCondition.DisableBuff) ? 1 : 0)) == 0;
        case BuffTypes.Debuff:
          return (this.IsUnitCondition(EUnitCondition.DisableDebuff) ? 1 : (this.IsDisableUnitCondition(EUnitCondition.DisableDebuff) ? 1 : 0)) == 0;
        default:
          return false;
      }
    }

    public bool IsEnableSteal() => !this.IsGimmick && this.Steal != null;

    public bool IsJumpBreakCondition()
    {
      return this.IsUnitCondition(EUnitCondition.Stun | EUnitCondition.Sleep | EUnitCondition.Charm | EUnitCondition.Stone | EUnitCondition.DisableSkill | EUnitCondition.DisableAttack | EUnitCondition.Berserk | EUnitCondition.Stop | EUnitCondition.Rage | EUnitCondition.GoodSleep);
    }

    public bool IsJumpBreakNoMotionCondition()
    {
      return this.IsUnitCondition(EUnitCondition.Sleep | EUnitCondition.Stone | EUnitCondition.Stop | EUnitCondition.GoodSleep);
    }

    public bool IsEnableControlCondition()
    {
      return this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.ForceAuto) && !this.IsUnitCondition(EUnitCondition.Charm) && !this.IsUnitCondition(EUnitCondition.Zombie) && !this.IsUnitCondition(EUnitCondition.Berserk) && !this.IsUnitCondition(EUnitCondition.Rage);
    }

    public bool IsEnableMoveCondition(bool bCondOnly = false)
    {
      return this.IsUnitFlag(EUnitFlag.ForceMoved) || (bCondOnly || !this.IsUnitFlag(EUnitFlag.Moved)) && this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.Paralysed) && !this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Sleep) && !this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.DisableMove) && !this.IsUnitCondition(EUnitCondition.Stop);
    }

    public bool IsEnableActionCondition()
    {
      return this.IsEnableAttackCondition() || this.IsEnableSkillCondition() || this.IsEnableItemCondition();
    }

    public bool IsEnableAttackCondition(bool bCondOnly = false)
    {
      return (bCondOnly || !this.IsUnitFlag(EUnitFlag.Action)) && this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.Paralysed) && !this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Sleep) && !this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.DisableAttack) && !this.IsUnitCondition(EUnitCondition.Stop) && this.GetAttackSkill() != null;
    }

    public bool IsEnableHelpCondition()
    {
      return this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.Paralysed) && !this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Sleep) && !this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.Charm) && !this.IsUnitCondition(EUnitCondition.Zombie) && !this.IsUnitCondition(EUnitCondition.DisableAttack) && !this.IsUnitCondition(EUnitCondition.Berserk) && !this.IsUnitCondition(EUnitCondition.Stop) && !this.IsUnitCondition(EUnitCondition.Rage) && this.GetAttackSkill() != null;
    }

    public bool IsEnableSkillCondition(bool bCondOnly = false)
    {
      return (bCondOnly || !this.IsUnitFlag(EUnitFlag.Action)) && this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.Paralysed) && !this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Sleep) && !this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.Charm) && !this.IsUnitCondition(EUnitCondition.Zombie) && !this.IsUnitCondition(EUnitCondition.DisableSkill) && !this.IsUnitCondition(EUnitCondition.Berserk) && !this.IsUnitCondition(EUnitCondition.Stop) && !this.IsUnitCondition(EUnitCondition.Rage);
    }

    public bool IsEnableItemCondition(bool bCondOnly = false)
    {
      return (bCondOnly || !this.IsUnitFlag(EUnitFlag.Action)) && this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.Paralysed) && !this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Sleep) && !this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.Charm) && !this.IsUnitCondition(EUnitCondition.Zombie) && !this.IsUnitCondition(EUnitCondition.Berserk) && !this.IsUnitCondition(EUnitCondition.Stop) && !this.IsUnitCondition(EUnitCondition.Rage);
    }

    public bool IsEnableReactionCondition()
    {
      return this.CheckExistMap() && !this.IsJump && !this.IsUnitFlag(EUnitFlag.Paralysed) && !this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Stop) && !this.IsUnitCondition(EUnitCondition.Sleep) && !this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.DisableSkill);
    }

    public bool IsEnableReactionSkill(SkillData skill)
    {
      if (!skill.IsReactionSkill() || skill.SkillParam.count > 0 && (int) this.GetSkillUseCount(skill) == 0)
        return false;
      switch (skill.Timing)
      {
        case ESkillTiming.DamageCalculate:
        case ESkillTiming.DamageControl:
        case ESkillTiming.Reaction:
        case ESkillTiming.FirstReaction:
          return this.IsEnableReactionCondition() && (!skill.IsDamagedSkill() || !this.IsUnitCondition(EUnitCondition.DisableAttack));
        default:
          return false;
      }
    }

    public bool IsEnableSelectDirectionCondition()
    {
      return this.CheckExistMap() && !this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Stop) && !this.IsUnitCondition(EUnitCondition.Sleep) && !this.IsUnitCondition(EUnitCondition.Stone);
    }

    public bool IsEnableAutoHealCondition()
    {
      return !this.IsDeadCondition() && (this.mCastSkill == null || this.mCastSkill.CastType != ECastTypes.Jump);
    }

    public bool IsEnableGimmickCondition() => true;

    public bool IsDeadCondition()
    {
      return this.IsDead || this.IsUnitCondition(EUnitCondition.Stone) || this.IsUnitFlag(EUnitFlag.UnitChanged);
    }

    public bool IsDisableGimmick()
    {
      if (!this.IsGimmick)
        return true;
      if (this.IsBreakObj)
        return this.IsDead;
      return this.mEventTrigger != null && this.mEventTrigger.Count == 0;
    }

    public bool IsEnableAutoMode()
    {
      return !this.IsUnitFlag(EUnitFlag.ForceAuto) && this.IsControl && this.IsEntry && !this.IsSub && !this.IsDeadCondition() && (!this.IsUnitCondition(EUnitCondition.Charm) || !this.IsUnitCondition(EUnitCondition.Zombie) || !this.IsUnitCondition(EUnitCondition.Berserk)) && this.GetRageTarget() == null;
    }

    public bool IsEnablePlayAnimCondition()
    {
      return !this.IsUnitCondition(EUnitCondition.Stop) && !this.IsUnitCondition(EUnitCondition.Stone);
    }

    public bool CheckCancelSkillFailCondition(EUnitCondition condition)
    {
      return this.CastSkill != null && ((condition & EUnitCondition.Stun) != (EUnitCondition) 0 || (condition & EUnitCondition.Sleep) != (EUnitCondition) 0 || (condition & EUnitCondition.Stone) != (EUnitCondition) 0 || (condition & EUnitCondition.DisableSkill) != (EUnitCondition) 0 || (condition & EUnitCondition.Rage) != (EUnitCondition) 0 || (condition & EUnitCondition.Charm) != (EUnitCondition) 0 || (condition & EUnitCondition.Berserk) != (EUnitCondition) 0 || this.CastSkill.IsDamagedSkill() && (condition & EUnitCondition.DisableAttack) != (EUnitCondition) 0);
    }

    public bool CheckCancelSkillCureCondition(EUnitCondition condition)
    {
      return this.CastSkill != null && ((condition & EUnitCondition.Rage) != (EUnitCondition) 0 && this.IsUnitCondition(EUnitCondition.Rage) || (condition & EUnitCondition.Charm) != (EUnitCondition) 0 && this.IsUnitCondition(EUnitCondition.Charm) || (condition & EUnitCondition.Berserk) != (EUnitCondition) 0 && this.IsUnitCondition(EUnitCondition.Berserk));
    }

    public bool CheckEnableCureCondition(EUnitCondition condition)
    {
      if (!this.IsUnitCondition(condition) || this.IsPassiveUnitCondition(condition))
        return false;
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.Condition == condition && condAttachment.IsCurse)
          return false;
      }
      return true;
    }

    public bool CheckEnableFailCondition(EUnitCondition condition)
    {
      return !this.IsUnitCondition(condition) && !this.IsDisableUnitCondition(condition);
    }

    public bool CheckEnableUseSkill(SkillData skill, bool bCheckCondOnly = false)
    {
      if (skill == null)
        return false;
      switch (skill.SkillType)
      {
        case ESkillType.Attack:
          if (!this.IsEnableAttackCondition(bCheckCondOnly))
            return false;
          break;
        case ESkillType.Skill:
          if (!this.IsEnableSkillCondition(bCheckCondOnly))
            return false;
          break;
        case ESkillType.Item:
          if (!this.IsEnableItemCondition(bCheckCondOnly))
            return false;
          break;
      }
      if (this.Gems - this.GetSkillUsedCost(skill) < 0 || skill.HpCostRate == 100 && skill.GetHpCost(this) >= (int) this.CurrentStatus.param.hp || this.CheckEnableSkillUseCount(skill) && (int) this.GetSkillUseCount(skill) == 0)
        return false;
      if (skill.IsDamagedSkill())
      {
        if (this.IsUnitCondition(EUnitCondition.DisableAttack))
          return false;
      }
      else if (this.GetRageTarget() != null)
        return false;
      if (skill.Condition != ESkillCondition.None)
      {
        switch (skill.Condition)
        {
          case ESkillCondition.Dying:
            if (!this.IsDying())
              return false;
            break;
          case ESkillCondition.JudgeHP:
            if (!skill.IsJudgeHp(this))
              return false;
            break;
        }
      }
      if (skill.IsSetBreakObjSkill())
      {
        SceneBattle instance = SceneBattle.Instance;
        BattleCore battleCore = (BattleCore) null;
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
          battleCore = instance.Battle;
        if (battleCore != null && !battleCore.IsAllowBreakObjEntryMax())
          return false;
      }
      if (this.IsUnitFlag(EUnitFlag.IsDynamicTransform) && this.mDtuParam != null)
      {
        if (this.mDtuParam.IsNoWeaponAbility)
        {
          ArtifactData[] artifactDatas = this.UnitData.CurrentJob.ArtifactDatas;
          if (artifactDatas != null)
          {
            for (int index1 = 0; index1 < artifactDatas.Length; ++index1)
            {
              ArtifactData artifactData = artifactDatas[index1];
              if (artifactData != null && artifactData.LearningAbilities != null)
              {
                for (int index2 = 0; index2 < artifactData.LearningAbilities.Count; ++index2)
                {
                  AbilityData learningAbility = artifactData.LearningAbilities[index2];
                  if (learningAbility != null && learningAbility.Skills.Contains(skill))
                    return false;
                }
              }
            }
          }
        }
        if (this.mDtuParam.IsNoVisionAbility && this.UnitData.ConceptCards != null)
        {
          for (int index3 = 0; index3 < this.UnitData.ConceptCards.Length; ++index3)
          {
            ConceptCardData conceptCard = this.UnitData.ConceptCards[index3];
            if (conceptCard != null)
            {
              List<ConceptCardEquipEffect> enableEquipEffects = conceptCard.GetEnableEquipEffects(this.UnitData, this.UnitData.CurrentJob, true);
              for (int index4 = 0; index4 < enableEquipEffects.Count; ++index4)
              {
                AbilityData ability = enableEquipEffects[index4].Ability;
                if (ability != null && ability.Skills != null && ability.Skills.Contains(skill))
                  return false;
              }
            }
          }
        }
      }
      return !skill.IsProtectSkill() || this.Protects.Count <= 0;
    }

    public void ClearProtect() => this.mProtects.Clear();

    public void ClearGuard() => this.mGuards.Clear();

    public void ResumeProtect(
      BattleSuspend.Data.UnitInfo.Protect protect,
      BattleCore btl,
      bool isGuard = false)
    {
      if (string.IsNullOrEmpty(protect.skillIname))
        return;
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      if (masterParam == null)
        return;
      SkillParam skillParam = masterParam.GetSkillParam(protect.skillIname);
      if (skillParam == null || skillParam.ProtectSkill == null)
        return;
      Unit.UnitProtect unitProtect = new Unit.UnitProtect()
      {
        target = BattleSuspend.GetUnitFromAllUnits(btl, protect.target),
        skillIname = protect.skillIname,
        value = (OInt) protect.value,
        type = skillParam.ProtectSkill.Type,
        damageType = skillParam.ProtectSkill.DamageType,
        range = (OInt) skillParam.ProtectSkill.Range,
        height = (OInt) skillParam.ProtectSkill.Height
      };
      unitProtect.eternal = skillParam.ProtectSkill.IsProtectEternal((int) unitProtect.value);
      unitProtect.passive = skillParam.timing == ESkillTiming.Passive || skillParam.timing == ESkillTiming.Auto;
      if (isGuard)
        this.mGuards.Add(unitProtect);
      else
        this.mProtects.Add(unitProtect);
    }

    public bool IsProtectCondition()
    {
      return !this.IsUnitCondition(EUnitCondition.Paralysed | EUnitCondition.Stun | EUnitCondition.Sleep | EUnitCondition.Charm | EUnitCondition.Stone | EUnitCondition.DisableSkill | EUnitCondition.Berserk | EUnitCondition.Stop | EUnitCondition.Rage | EUnitCondition.GoodSleep) && !this.IsJump && this.IsEntry && !this.IsSub;
    }

    public bool IsProtectTargetCondition(Unit unit)
    {
      return unit.Side == this.Side && !this.IsDead && !this.IsUnitFlag(EUnitFlag.UnitChanged) && this.CheckExistMap() && !this.IsJump;
    }

    public bool IsProtectTargetRelease(Unit unit)
    {
      return unit.Side != this.Side || this.IsDead || this.IsUnitFlag(EUnitFlag.UnitChanged) || !this.CheckExistMap();
    }

    public Unit.UnitProtect GetSameProtect(Unit.UnitProtect protect)
    {
      return protect == null ? (Unit.UnitProtect) null : this.mProtects.Find((Predicate<Unit.UnitProtect>) (p => p.target == protect.target && p.skillIname == protect.skillIname));
    }

    public Unit.UnitProtect GetSameGuard(Unit.UnitProtect protect)
    {
      return protect == null ? (Unit.UnitProtect) null : this.mGuards.Find((Predicate<Unit.UnitProtect>) (p => p.target == protect.target && p.skillIname == protect.skillIname));
    }

    private Unit.UnitProtect CopyUnitProtect(Unit target, Unit.UnitProtect protect)
    {
      if (protect == null || target == null)
        return (Unit.UnitProtect) null;
      return new Unit.UnitProtect()
      {
        target = target,
        type = protect.type,
        value = protect.value,
        damageType = protect.damageType,
        range = protect.range,
        height = protect.height,
        eternal = protect.eternal,
        skillIname = protect.skillIname,
        passive = protect.passive
      };
    }

    private ProtectTypes GetProtectType(SkillData skill)
    {
      return skill == null || skill.ProtectValue <= 0 ? ProtectTypes.None : skill.ProtectType;
    }

    public void AddProtect(Unit target, SkillData skill)
    {
      ProtectTypes protectType = this.GetProtectType(skill);
      if (protectType == ProtectTypes.None || target == this)
        return;
      Unit.UnitProtect protect = new Unit.UnitProtect();
      protect.target = target;
      protect.type = protectType;
      protect.value = (OInt) skill.ProtectValue;
      protect.damageType = skill.ProtectDamageType;
      protect.range = skill.ProtectRange;
      protect.height = skill.ProtectHeight;
      protect.skillIname = skill.SkillID;
      protect.passive = skill.Timing == ESkillTiming.Passive || skill.Timing == ESkillTiming.Auto;
      ProtectSkillParam protectSkillParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetProtectSkillParam(protect.skillIname);
      if (protectSkillParam != null)
        protect.eternal = protectSkillParam.IsProtectEternal((int) protect.value);
      Unit.UnitProtect sameProtect = this.GetSameProtect(protect);
      if (sameProtect != null)
      {
        sameProtect.value = protect.value;
      }
      else
      {
        int index = this.mProtects.FindIndex((Predicate<Unit.UnitProtect>) (p => p.passive));
        if (index != -1 && !protect.passive)
          this.mProtects.Insert(index, protect);
        else
          this.mProtects.Add(protect);
      }
      target.AddGuard(this, protect);
    }

    public void AddGuard(Unit target, Unit.UnitProtect protect)
    {
      Unit.UnitProtect protect1 = this.CopyUnitProtect(target, protect);
      Unit.UnitProtect sameGuard = this.GetSameGuard(protect1);
      if (sameGuard != null)
      {
        sameGuard.value = protect.value;
      }
      else
      {
        int index = this.mGuards.FindIndex((Predicate<Unit.UnitProtect>) (p => p.passive));
        if (index != -1 && !protect1.passive)
          this.mGuards.Insert(index, protect1);
        else
          this.mGuards.Add(protect1);
      }
    }

    private void UpdateProtectTurn()
    {
      if (this.mProtects == null)
        return;
      for (int index = 0; index < this.mProtects.Count; ++index)
      {
        if (this.mProtects[index] != null)
        {
          if (this.mProtects[index].target.IsProtectTargetRelease(this))
            this.CancelProtect(this.mProtects[index--]);
          else if (this.mProtects[index].type == ProtectTypes.Turn)
          {
            if (!this.mProtects[index].eternal)
              this.mProtects[index].value = (OInt) Math.Max((int) --this.mProtects[index].value, 0);
            if ((int) this.mProtects[index].value <= 0)
              this.CancelProtect(this.mProtects[index--]);
          }
        }
      }
    }

    public void CancelProtect(Unit.UnitProtect protect = null)
    {
      if (protect == null)
      {
        if (this.mProtects == null)
          return;
        for (int index = 0; index < this.mProtects.Count; ++index)
        {
          if (this.mProtects[index].target != null)
            this.mProtects[index].target.CancelGuard(this, this.mProtects[index]);
        }
        this.mProtects.Clear();
        for (int index = 0; index < this.mGuards.Count; ++index)
        {
          if (this.mGuards[index].target != null)
          {
            Unit.UnitProtect protect1 = this.CopyUnitProtect(this, this.mGuards[index]);
            Unit.UnitProtect sameProtect = this.mGuards[index].target.GetSameProtect(protect1);
            if (sameProtect != null)
              this.mGuards[index].target.CancelProtect(sameProtect);
          }
        }
        this.mGuards.Clear();
      }
      else
      {
        if (protect.target != null)
          protect.target.CancelGuard(this, protect);
        this.mProtects.Remove(protect);
      }
    }

    public void CancelGuard(Unit target, Unit.UnitProtect protect)
    {
      if (protect == null)
        return;
      Unit.UnitProtect sameGuard = this.GetSameGuard(this.CopyUnitProtect(target, protect));
      if (sameGuard == null)
        return;
      this.mGuards.Remove(sameGuard);
    }

    public void CalcProtectDamage(DamageTypes damageType, int damage, Unit guard)
    {
      if (guard == null)
        return;
      for (int index = this.mProtects.Count - 1; index >= 0; --index)
      {
        if (this.mProtects[index] != null)
        {
          Unit.UnitProtect mProtect = this.mProtects[index];
          if (guard == mProtect.target && mProtect.type != ProtectTypes.None && mProtect.type != ProtectTypes.Turn && (int) mProtect.value > 0 && mProtect.damageType == damageType)
          {
            if (mProtect.type == ProtectTypes.UseCount)
            {
              --mProtect.value;
              break;
            }
            if (mProtect.type == ProtectTypes.Hp)
            {
              int num = Math.Max((int) mProtect.value - damage, 0);
              mProtect.value = (OInt) num;
              break;
            }
          }
        }
      }
    }

    public void ProtectDtuCopyTo(List<Unit> units, Unit from_unit)
    {
      this.mProtects = new List<Unit.UnitProtect>((IEnumerable<Unit.UnitProtect>) from_unit.Protects);
      this.mGuards = new List<Unit.UnitProtect>((IEnumerable<Unit.UnitProtect>) from_unit.Guards);
      for (int index = 0; index < units.Count; ++index)
      {
        Unit unit = units[index];
        if (unit.Protects != null)
          unit.ProtectChangeTarget(this, from_unit);
      }
    }

    public void ProtectChangeTarget(Unit newTarget, Unit oldTarget)
    {
      if (this.Protects == null)
        return;
      List<Unit.UnitProtect> all = this.Protects.FindAll((Predicate<Unit.UnitProtect>) (p => p.target == oldTarget));
      if (all == null)
        return;
      for (int index = 0; index < all.Count; ++index)
        all[index].target = newTarget;
    }

    public void AddShield(SkillData skill)
    {
      if (skill == null || skill.EffectType != SkillEffectTypes.Shield || skill.ShieldType == ShieldTypes.None || this.IsBreakObj)
        return;
      int shieldValue = (int) skill.ShieldValue;
      if (shieldValue == 0)
        return;
      int num = (int) skill.ShieldTurn;
      if (num <= 0)
        num = -1;
      int controlDamageRate = (int) skill.ControlDamageRate;
      int controlDamageValue = (int) skill.ControlDamageValue;
      this.AddShieldSuspend(skill.SkillParam, shieldValue, shieldValue, num, num, controlDamageRate, controlDamageValue);
    }

    private void AddShieldSuspend(
      SkillParam skill_param,
      int hp,
      int hp_max,
      int turn,
      int turn_max,
      int damage_rate,
      int damage_value)
    {
      if (skill_param == null)
        return;
      Unit.UnitShield unitShield = new Unit.UnitShield();
      unitShield.shieldType = skill_param.shield_type;
      unitShield.damageType = skill_param.shield_damage_type;
      unitShield.hp = (OInt) hp;
      unitShield.hpMax = (OInt) hp_max;
      unitShield.turn = (OInt) turn;
      unitShield.turnMax = (OInt) turn_max;
      unitShield.damage_rate = (OInt) damage_rate;
      unitShield.damage_value = (OInt) damage_value;
      unitShield.skill_param = skill_param;
      for (int index = 0; index < this.mShields.Count; ++index)
      {
        if (this.mShields[index].shieldType == unitShield.shieldType && this.mShields[index].damageType == unitShield.damageType)
          this.mShields.RemoveAt(index--);
      }
      this.mShields.Add(unitShield);
      MySort<Unit.UnitShield>.Sort(this.mShields, (Comparison<Unit.UnitShield>) ((src, dsc) =>
      {
        if (src.shieldType != dsc.shieldType)
          return src.shieldType == ShieldTypes.Limitter || dsc.shieldType != ShieldTypes.Limitter && src.shieldType == ShieldTypes.UseCount ? -1 : 1;
        if (src.damageType != dsc.damageType)
        {
          if (src.damageType == DamageTypes.TotalDamage)
            return -1;
          if (dsc.damageType == DamageTypes.TotalDamage)
            return 1;
        }
        return 0;
      }));
    }

    public bool CheckEnableShieldType(DamageTypes type)
    {
      for (int index = 0; index < this.mShields.Count; ++index)
      {
        if ((int) this.mShields[index].hp > 0 && this.mShields[index].damageType == type)
          return true;
      }
      return false;
    }

    public void CalcShieldDamage(
      DamageTypes damageType,
      ref int damage,
      bool bEnableShieldBreak,
      AttackDetailTypes attack_detail_type,
      RandXorshift rand,
      LogSkill.Target log_target = null)
    {
      if (damage <= 0)
        return;
      for (int index = 0; index < this.mShields.Count; ++index)
      {
        Unit.UnitShield mShield = this.mShields[index];
        if ((int) mShield.hp > 0 && mShield.damageType == damageType && mShield.skill_param.IsReactionDet(attack_detail_type))
        {
          int damageRate = (int) mShield.damage_rate;
          if (0 >= damageRate || damageRate >= 100 || (int) (rand.Get() % 100U) <= damageRate)
          {
            if (mShield.shieldType == ShieldTypes.UseCount)
            {
              int num = damage;
              if ((int) mShield.damage_value != 0)
                num = Math.Min(Math.Max(damage - this.CalcShieldEffectValue(mShield.skill_param.control_damage_calc, (int) mShield.damage_value, damage), 0), damage);
              damage = Math.Max(damage - num, 0);
              if (!bEnableShieldBreak)
                break;
              --mShield.hp;
              if (log_target == null)
                break;
              log_target.isProcShield = true;
              break;
            }
            if (mShield.shieldType == ShieldTypes.Hp)
            {
              int num1 = 0;
              if ((int) mShield.damage_value != 0)
                num1 = Math.Min(Math.Max(damage - this.CalcShieldEffectValue(mShield.skill_param.control_damage_calc, (int) mShield.damage_value, damage), 0), damage);
              int num2 = damage - num1;
              int hp = (int) mShield.hp;
              int num3 = Math.Max((int) mShield.hp - num1, 0);
              damage = Math.Max(num1 - (int) mShield.hp, 0);
              damage += num2;
              if (!bEnableShieldBreak)
                break;
              mShield.hp = (OInt) num3;
              if (log_target == null)
                break;
              log_target.isProcShield = true;
              break;
            }
            if (mShield.shieldType == ShieldTypes.Limitter && damage <= (int) mShield.hp)
            {
              damage = 0;
              if (!bEnableShieldBreak)
                break;
              mShield.is_efficacy = true;
              if (log_target == null)
                break;
              log_target.isProcShield = true;
              break;
            }
          }
        }
      }
    }

    private int CalcShieldEffectValue(SkillParamCalcTypes calctype, int skillval, int target)
    {
      switch (calctype)
      {
        case SkillParamCalcTypes.Add:
          return target + skillval;
        case SkillParamCalcTypes.Scale:
          int num1 = skillval;
          switch (num1)
          {
            case -50:
              --num1;
              break;
            case 50:
              ++num1;
              break;
          }
          int num2 = Mathf.RoundToInt((float) ((double) target * (double) num1 / 100.0));
          return target + num2;
        default:
          return skillval;
      }
    }

    private void UpdateShieldTurn()
    {
      for (int index = 0; index < this.mShields.Count; ++index)
      {
        if ((int) this.mShields[index].turn > 0)
        {
          --this.mShields[index].turn;
          if ((int) this.mShields[index].turn <= 0)
          {
            this.mShields.RemoveAt(index--);
            continue;
          }
        }
        Unit.UnitShield mShield = this.mShields[index];
        if (mShield.skill_param.IsShieldReset())
          mShield.hp = mShield.hpMax;
      }
    }

    public void DestroyShield(LogSkill.Target log_target)
    {
      bool flag = false;
      foreach (Unit.UnitShield mShield in this.mShields)
      {
        if ((int) mShield.hp > 0)
        {
          mShield.hp = (OInt) 0;
          if (!flag)
          {
            if (log_target != null)
              log_target.isProcShield = true;
            flag = true;
          }
          else
            mShield.is_ignore_efficacy = true;
        }
      }
    }

    public Unit.UnitForcedTargeting GetFtgtTarget(Unit unit)
    {
      return unit == null ? (Unit.UnitForcedTargeting) null : this.mFtgtTargetList.Find((Predicate<Unit.UnitForcedTargeting>) (t => t.mUnit == unit));
    }

    public bool RemoveFtgtTarget(Unit.UnitForcedTargeting ftgt_target)
    {
      if (ftgt_target == null)
        return false;
      this.mFtgtTargetList.Remove(ftgt_target);
      return true;
    }

    public bool RemoveFtgtTarget(Unit unit) => this.RemoveFtgtTarget(this.GetFtgtTarget(unit));

    public bool InsertFtgtTarget(Unit target, int turn)
    {
      if (target == null || turn < 0)
        return false;
      this.RemoveFtgtTarget(this.GetFtgtTarget(target));
      this.mFtgtTargetList.Insert(0, new Unit.UnitForcedTargeting(target, turn));
      return true;
    }

    public Unit.UnitForcedTargeting GetFtgtFrom(Unit unit)
    {
      return unit == null ? (Unit.UnitForcedTargeting) null : this.mFtgtFromList.Find((Predicate<Unit.UnitForcedTargeting>) (t => t.mUnit == unit));
    }

    public bool RemoveFtgtFrom(Unit.UnitForcedTargeting ftgt_from)
    {
      if (ftgt_from == null)
        return false;
      this.mFtgtFromList.Remove(ftgt_from);
      return true;
    }

    public bool RemoveFtgtFrom(Unit unit) => this.RemoveFtgtFrom(this.GetFtgtFrom(unit));

    public bool InsertFtgtFrom(Unit from, int turn)
    {
      if (from == null || turn < 0)
        return false;
      this.RemoveFtgtFrom(this.GetFtgtFrom(from));
      this.mFtgtFromList.Insert(0, new Unit.UnitForcedTargeting(from, turn));
      return true;
    }

    private void CancelForcedTargeting()
    {
      foreach (Unit.UnitForcedTargeting mFtgtTarget in this.mFtgtTargetList)
      {
        if (mFtgtTarget != null && mFtgtTarget.mUnit != null)
          mFtgtTarget.mUnit.RemoveFtgtFrom(this);
      }
      this.mFtgtTargetList.Clear();
      foreach (Unit.UnitForcedTargeting mFtgtFrom in this.mFtgtFromList)
      {
        if (mFtgtFrom != null && mFtgtFrom.mUnit != null)
          mFtgtFrom.mUnit.RemoveFtgtTarget(this);
      }
      this.mFtgtFromList.Clear();
    }

    private void UpdateForcedTargetingTurn()
    {
      for (int index = this.mFtgtFromList.Count - 1; index >= 0; --index)
      {
        Unit.UnitForcedTargeting mFtgtFrom = this.mFtgtFromList[index];
        if (mFtgtFrom != null && mFtgtFrom.mTurn > 0 && mFtgtFrom.mTurn < 99)
        {
          --mFtgtFrom.mTurn;
          if (mFtgtFrom.mUnit != null)
          {
            Unit.UnitForcedTargeting ftgtTarget = mFtgtFrom.mUnit.GetFtgtTarget(this);
            if (ftgtTarget != null)
              --ftgtTarget.mTurn;
          }
          if (mFtgtFrom.mTurn <= 0)
          {
            this.mFtgtFromList.RemoveAt(index);
            if (mFtgtFrom.mUnit != null)
              mFtgtFrom.mUnit.RemoveFtgtTarget(this);
          }
        }
      }
    }

    public void ClearMhmDamage() => this.mMhmDamageLists.Clear();

    public void AddMhmDamage(Unit.eTypeMhmDamage type, int damage)
    {
      this.mMhmDamageLists.Add(new Unit.UnitMhmDamage(type, damage));
    }

    private void ReflectMhmDamage()
    {
      foreach (Unit.UnitMhmDamage mMhmDamageList in this.mMhmDamageLists)
        this.ReflectMhmDamage(mMhmDamageList.mType, (int) mMhmDamageList.mDamage);
    }

    public void ReflectMhmDamage(Unit.eTypeMhmDamage type, int damage, bool is_allow_to_dead = false)
    {
      switch (type)
      {
        case Unit.eTypeMhmDamage.HP:
          if (this.IsUseBossLongHP())
          {
            if (is_allow_to_dead && (long) damage > this.mBossLongMaxHP)
              this.mOverKillDamage = damage - (int) this.mBossLongMaxHP;
            this.mBossLongMaxHP = Math.Max(this.mBossLongMaxHP - (long) damage, 0L);
            bool flag = false;
            if (this.mBossLongMaxHP <= 0L)
            {
              flag = true;
              this.mBossLongMaxHP = 1L;
            }
            this.mBossLongCurHP = Math.Min(this.mBossLongCurHP, this.mBossLongMaxHP);
            if (flag && is_allow_to_dead)
              this.mBossLongCurHP = 0L;
            this.InternalReflectBossCurrentHP();
            break;
          }
          if (is_allow_to_dead && damage > (int) this.MaximumStatus.param.hp)
            this.mOverKillDamage = damage - (int) this.MaximumStatus.param.hp;
          this.MaximumStatus.param.hp = (OInt) Math.Max((int) this.MaximumStatus.param.hp - damage, 0);
          bool flag1 = false;
          if ((int) this.MaximumStatus.param.hp <= 0)
          {
            flag1 = true;
            this.MaximumStatus.param.hp = (OInt) 1;
          }
          this.CurrentStatus.param.hp = (OInt) Math.Min((int) this.CurrentStatus.param.hp, (int) this.MaximumStatus.param.hp);
          if (!flag1 || !is_allow_to_dead)
            break;
          this.CurrentStatus.param.hp = (OInt) 0;
          break;
        case Unit.eTypeMhmDamage.MP:
          this.MaximumStatus.param.mp = (OShort) Math.Max((int) this.MaximumStatus.param.mp - damage, 0);
          this.CurrentStatus.param.mp = (OShort) Math.Min((short) this.CurrentStatus.param.mp, (short) this.MaximumStatus.param.mp);
          break;
      }
    }

    public bool ContainsSkillBuffAttachment(SkillData skill, Unit user = null)
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        if (buffAttachment.skill != null && buffAttachment.skill.SkillID == skill.SkillID && (user == null || user == buffAttachment.user))
          return true;
      }
      return false;
    }

    public bool SetBuffAttachment(BuffAttachment buff, bool is_duplicate = false)
    {
      if (buff == null || this.IsBreakObj && (!(bool) buff.IsPassive || buff.user != this))
        return false;
      SkillData skill = buff.skill;
      if (skill != null)
      {
        if (this.mCastSkill != null && this.mCastSkill != skill && this.mCastSkill.CastType == ECastTypes.Jump && skill.SkillParam.TargetEx == eSkillTargetEx.None)
          return false;
        if (!is_duplicate)
        {
          int num1 = 1;
          if (buff.DuplicateCount > 0)
            num1 = Math.Max(1 + this.GetBuffAttachmentDuplicateCount(buff) - buff.DuplicateCount, 0);
          if (num1 > 0)
          {
            int index = 0;
            int num2 = 0;
            for (; index < this.BuffAttachments.Count; ++index)
            {
              if (this.isSameBuffAttachment(this.BuffAttachments[index], buff))
              {
                if (buff.IsPrevApply)
                  return false;
                this.BuffAttachments.RemoveAt(index--);
                if (++num2 >= num1)
                  break;
              }
            }
          }
        }
      }
      this.BuffAttachments.Add(buff);
      int num = 0;
      int count = 0;
      if (buff.Param != null && buff.CheckTiming != EffectCheckTimings.Moment && !buff.IsPrevApply)
      {
        if (buff.Param.IsUpReplenish)
        {
          if (buff.status[ParamTypes.HpMax] != 0)
            num = (int) this.MaximumStatus.param.hp;
        }
        else if (buff.Param.IsUpReplenishUseSkillCtr)
          count = buff.status[ParamTypes.SkillUseCount];
      }
      bool is_predict = false;
      BattleCore battle = !UnityEngine.Object.op_Implicit((UnityEngine.Object) SceneBattle.Instance) ? (BattleCore) null : SceneBattle.Instance.Battle;
      if (battle != null)
        is_predict = battle.IsBattleFlag(EBattleFlag.PredictResult);
      this.CalcCurrentStatus(is_predict: is_predict);
      if (num != 0)
      {
        StatusParam statusParam = this.CurrentStatus.param;
        statusParam.hp = (OInt) ((int) statusParam.hp + Math.Max((int) this.MaximumStatus.param.hp - num, 0));
      }
      if (!is_predict && count != 0)
        this.UpdateAllSkillUseCount(count);
      this.UpdateBuffEffects();
      return true;
    }

    public void ClearPassiveBuffEffects()
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        if ((bool) this.BuffAttachments[index].IsPassive && (this.BuffAttachments[index].skill == null || this.BuffAttachments[index].skill.IsPassiveSkill()))
          this.BuffAttachments.RemoveAt(index--);
      }
    }

    public void ClearBuffEffects()
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        if (!(bool) this.BuffAttachments[index].IsPassive && (this.BuffAttachments[index].skill == null || !this.BuffAttachments[index].skill.IsPassiveSkill()))
          this.BuffAttachments.RemoveAt(index--);
      }
    }

    public void UpdateBuffEffectTurnCount(
      EffectCheckTimings timing,
      Unit current,
      AvoidType avoidType = AvoidType.None)
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        if (current != null && current == this)
        {
          if (timing == EffectCheckTimings.AvoidEnd)
          {
            if (buffAttachment.Param != null && (avoidType != AvoidType.None && buffAttachment.Param.IsAvoidAllType || avoidType == AvoidType.Avoid && buffAttachment.Param.IsAvoidMissType || avoidType == AvoidType.PerfectAvoid && buffAttachment.Param.IsAvoidPerfectType))
              this.UpdateUpBuffEffect(buffAttachment, timing, true);
          }
          else
            this.UpdateUpBuffEffect(buffAttachment, timing, true);
        }
        if (!(bool) buffAttachment.IsPassive)
        {
          EffectCheckTimings checkTiming = buffAttachment.CheckTiming;
          switch (checkTiming)
          {
            case EffectCheckTimings.ClockCountUp:
            case EffectCheckTimings.Moment:
            case EffectCheckTimings.MomentStart:
              if (checkTiming == timing && checkTiming != EffectCheckTimings.Eternal)
              {
                --buffAttachment.turn;
                continue;
              }
              continue;
            default:
              if (buffAttachment.CheckTarget != null)
              {
                if (buffAttachment.CheckTarget == current)
                  goto case EffectCheckTimings.ClockCountUp;
                else
                  continue;
              }
              else if (current == null || current == this)
                goto case EffectCheckTimings.ClockCountUp;
              else
                continue;
          }
        }
      }
    }

    private void UpdateUpBuffEffect(
      BuffAttachment buff,
      EffectCheckTimings timing = EffectCheckTimings.ActionStart,
      bool is_count_up = false)
    {
      if (buff == null || buff.skill == null || buff.Param == null || !(bool) buff.Param.mIsUpBuff)
        return;
      if (is_count_up)
      {
        if (buff.Param.mUpTiming != timing)
          return;
        ++buff.UpBuffCount;
      }
      BuffEffect buffEffect = BuffEffect.CreateBuffEffect(buff.Param, buff.skill.Rank, buff.skill.GetRankCap());
      if (buffEffect == null)
        return;
      buff.status.Clear();
      int decreaseEffectRate = !buff.skill.IsNeedDecreaseEffect() ? 0 : buff.skill.DecreaseEffectRate;
      buffEffect.CalcBuffStatus(ref buff.status, this.Element, buff.BuffType, true, buff.IsNegativeValueIsBuff, buff.CalcType, (int) buff.UpBuffCount, coefDecreaseRate: decreaseEffectRate);
    }

    private bool IsBuffConceptCardLS(BuffAttachment ba, Unit leader_unit, SkillData leader_skill)
    {
      return ba != null && leader_unit != null && leader_skill != null && ba.Param != null && ba.user == leader_unit && ba.skill == leader_skill && (bool) ba.IsPassive && ba.Param.chk_timing == EffectCheckTimings.Eternal && !(bool) ba.Param.mIsUpBuff;
    }

    public bool IsAttachConceptCardLS(Unit leader_unit, SkillData leader_skill)
    {
      if (leader_skill == null)
        return false;
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        if (this.IsBuffConceptCardLS(this.BuffAttachments[index], leader_unit, leader_skill))
          return true;
      }
      return false;
    }

    public void ApplyConceptCardLS(Unit leader_unit, SkillData leader_skill, int coef)
    {
      if (leader_skill == null || leader_skill.Condition != ESkillCondition.CardLsSkill)
        return;
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        if (this.IsBuffConceptCardLS(buffAttachment, leader_unit, leader_skill))
        {
          BuffEffect buffEffect = BuffEffect.CreateBuffEffect(buffAttachment.Param, buffAttachment.skill.Rank, buffAttachment.skill.GetRankCap());
          if (buffEffect != null)
          {
            buffAttachment.status.Clear();
            buffEffect.CalcBuffStatus(ref buffAttachment.status, this.Element, buffAttachment.BuffType, true, buffAttachment.IsNegativeValueIsBuff, buffAttachment.CalcType, coef: coef);
          }
        }
      }
    }

    public bool RemoveBuffPrevApply()
    {
      bool flag = false;
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        if (this.BuffAttachments[index].IsPrevApply)
        {
          this.BuffAttachments.RemoveAt(index--);
          flag = true;
        }
      }
      return flag;
    }

    public bool UpdateBuffEffects()
    {
      int count = this.BuffAttachments.Count;
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        SkillData skill = buffAttachment.skill;
        if (buffAttachment.LinkageID != 0U)
        {
          bool flag = false;
          foreach (CondAttachment condAttachment in this.CondAttachments)
          {
            if ((int) condAttachment.LinkageID == (int) buffAttachment.LinkageID)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            this.BuffAttachments.RemoveAt(index--);
            continue;
          }
        }
        if ((bool) buffAttachment.IsPassive)
        {
          if (skill != null && !skill.IsSubActuate())
          {
            Unit user = buffAttachment.user;
            if (user != null && user.IsDead)
            {
              SkillData leaderSkill = user.LeaderSkill;
              if (leaderSkill == null || skill.SkillID != leaderSkill.SkillID)
                this.BuffAttachments.RemoveAt(index--);
            }
          }
        }
        else if ((skill == null || !skill.IsPassiveSkill()) && (buffAttachment.Param == null || !buffAttachment.Param.IsNoDisabled) && !this.IsEnableBuffEffect(buffAttachment.BuffType))
          this.BuffAttachments.RemoveAt(index--);
        else if (buffAttachment.CheckTarget != null && buffAttachment.CheckTarget.IsDeadCondition() && !buffAttachment.IsPrevApply)
          this.BuffAttachments.RemoveAt(index--);
        else if (buffAttachment.CheckTiming != EffectCheckTimings.Eternal && !buffAttachment.IsPrevApply && (int) buffAttachment.turn <= 0)
          this.BuffAttachments.RemoveAt(index--);
      }
      return count != this.BuffAttachments.Count;
    }

    public bool ContainsSkillCondAttachment(SkillData skill)
    {
      foreach (CondAttachment condAttachment in this.CondAttachments)
      {
        if (condAttachment.skill != null && condAttachment.skill.SkillID == skill.SkillID)
          return true;
      }
      return false;
    }

    public bool IsUnitCondition(EUnitCondition condition)
    {
      return ((EUnitCondition) (long) this.mCurrentCondition & condition) != (EUnitCondition) 0;
    }

    public bool IsDisableUnitCondition(EUnitCondition condition)
    {
      return ((EUnitCondition) (long) this.mDisableCondition & condition) != (EUnitCondition) 0;
    }

    public bool IsCurseUnitCondition(EUnitCondition condition)
    {
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (condAttachment.IsFailCondition() && condAttachment.Condition == condition && condAttachment.IsCurse)
          return true;
      }
      return false;
    }

    public bool IsAutoCureCondition(EUnitCondition condition)
    {
      return condition != EUnitCondition.Stone && condition != EUnitCondition.Zombie && condition != EUnitCondition.DeathSentence && condition != EUnitCondition.DisableKnockback && condition != EUnitCondition.GoodSleep;
    }

    public bool IsClockCureCondition(EUnitCondition condition)
    {
      return condition == EUnitCondition.Stop || condition == EUnitCondition.Fast || condition == EUnitCondition.Slow;
    }

    public bool IsUnitConditionDamage() => this.IsUnitCondition(EUnitCondition.Poison);

    public bool IsPassiveUnitCondition(EUnitCondition condition)
    {
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (condAttachment != null && (bool) condAttachment.IsPassive && condAttachment.IsFailCondition() && condAttachment.Condition == condition)
          return true;
      }
      return false;
    }

    public bool SetCondAttachment(CondAttachment cond)
    {
      if (cond == null)
        return false;
      FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
      if (cond.IsFailCondition() && cond.CheckTiming != EffectCheckTimings.Eternal && !(bool) cond.IsPassive)
      {
        if (this.IsClockCureCondition(cond.Condition))
        {
          cond.CheckTarget = this;
          cond.CheckTiming = EffectCheckTimings.ClockCountUp;
          cond.turn = fixParam.DefaultCondTurns[cond.Condition];
        }
        if ((int) cond.turn == 0)
          cond.turn = fixParam.DefaultCondTurns[cond.Condition];
        if (cond.user != null && cond.Condition == EUnitCondition.Poison)
        {
          CondAttachment condAttachment = cond;
          condAttachment.turn = (OInt) ((int) condAttachment.turn + (int) cond.user.CurrentStatus[BattleBonus.PoisonTurn]);
        }
      }
      else if (cond.CondType == ConditionEffectTypes.DisableCondition && cond.CheckTiming != EffectCheckTimings.Eternal && !(bool) cond.IsPassive && (int) cond.turn == 0)
        cond.turn = fixParam.DefaultCondTurns[cond.Condition];
      if (this.IsBreakObj && (!(bool) cond.IsPassive || cond.user != this))
        return false;
      SkillData skill = cond.skill;
      if (skill != null && this.mCastSkill != null && this.mCastSkill != skill && this.mCastSkill.CastType == ECastTypes.Jump && skill.SkillParam.TargetEx == eSkillTargetEx.None)
        return false;
      EUnitCondition condition = cond.Condition;
      switch (cond.CondType)
      {
        case ConditionEffectTypes.CureCondition:
          this.CureCondEffects(condition);
          break;
        case ConditionEffectTypes.FailCondition:
        case ConditionEffectTypes.ForcedFailCondition:
        case ConditionEffectTypes.RandomFailCondition:
          if (!this.IsDisableUnitCondition(condition) || cond.CondType == ConditionEffectTypes.ForcedFailCondition)
          {
            if (!(bool) cond.IsPassive)
            {
              if (this.IsUnitCondition(EUnitCondition.Stone))
                return false;
              if ((condition & EUnitCondition.DeathSentence) != (EUnitCondition) 0)
              {
                bool flag = false;
                int num = 999;
                if (cond.skill != null)
                {
                  CondEffect condEffect1 = cond.skill.GetCondEffect();
                  if (condEffect1 != null && (int) condEffect1.param.v_death_count > 0)
                  {
                    num = Math.Min(num, (int) condEffect1.param.v_death_count);
                    flag = true;
                  }
                  CondEffect condEffect2 = cond.user != this ? (CondEffect) null : cond.skill.GetCondEffect(SkillEffectTargets.Self);
                  if (condEffect2 != null && (int) condEffect2.param.v_death_count > 0)
                  {
                    num = Math.Min(num, (int) condEffect2.param.v_death_count);
                    flag = true;
                  }
                }
                else if (cond.Param != null && (int) cond.Param.v_death_count > 0)
                {
                  num = Math.Min(num, (int) cond.Param.v_death_count);
                  flag = true;
                }
                if (!flag)
                  num = (int) fixParam.DefaultDeathCount;
                if ((int) this.mDeathCount > 0)
                  this.mDeathCount = (OInt) Math.Min((int) this.mDeathCount, num);
                if (this.IsUnitCondition(EUnitCondition.DeathSentence))
                  return false;
                this.mDeathCount = (OInt) num;
              }
              if ((condition & EUnitCondition.Fast) != (EUnitCondition) 0)
                this.CureCondEffects(EUnitCondition.Slow);
              if ((condition & EUnitCondition.Slow) != (EUnitCondition) 0)
                this.CureCondEffects(EUnitCondition.Fast);
              if ((condition & EUnitCondition.Rage) != (EUnitCondition) 0)
              {
                if (cond.user == null || cond.user == this)
                  return false;
                this.mRageTarget = cond.user;
              }
            }
            if (this.CheckCancelSkillFailCondition(condition))
              this.CancelCastSkill();
            this.CondAttachments.Add(cond);
            this.CondLinkageBuffAttach(cond, (int) cond.turn, true);
            break;
          }
          break;
        case ConditionEffectTypes.DisableCondition:
          this.CondAttachments.Add(cond);
          break;
        default:
          return false;
      }
      this.UpdateCondEffects();
      return true;
    }

    private uint GenerateCondLinkageID()
    {
      ++Unit.mCondLinkageID;
      if (Unit.mCondLinkageID == 0U)
        ++Unit.mCondLinkageID;
      return Unit.mCondLinkageID;
    }

    public void ClearCondLinkageBuffBits()
    {
      this.CondLinkageBuff.Clear();
      this.CondLinkageDebuff.Clear();
    }

    private bool CondLinkageBuffAttach(
      CondAttachment cond_attachment,
      int turn = 0,
      bool is_check_resist = false)
    {
      if (cond_attachment == null)
        return false;
      BuffEffect linkageBuff = cond_attachment.LinkageBuff;
      if (linkageBuff == null || !linkageBuff.CheckEnableBuffTarget(this))
        return false;
      BaseStatus status1 = new BaseStatus();
      BaseStatus status2 = new BaseStatus();
      BaseStatus status3 = new BaseStatus();
      BaseStatus status4 = new BaseStatus();
      BaseStatus status5 = new BaseStatus();
      BaseStatus status6 = new BaseStatus();
      linkageBuff.CalcBuffStatus(ref status1, this.Element, BuffTypes.Buff, true, false, SkillParamCalcTypes.Add);
      linkageBuff.CalcBuffStatus(ref status2, this.Element, BuffTypes.Buff, true, true, SkillParamCalcTypes.Add);
      linkageBuff.CalcBuffStatus(ref status3, this.Element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Scale);
      linkageBuff.CalcBuffStatus(ref status4, this.Element, BuffTypes.Debuff, true, false, SkillParamCalcTypes.Add);
      linkageBuff.CalcBuffStatus(ref status5, this.Element, BuffTypes.Debuff, true, true, SkillParamCalcTypes.Add);
      linkageBuff.CalcBuffStatus(ref status6, this.Element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Scale);
      bool flag1 = true;
      bool flag2 = true;
      int[] status_buffs = (int[]) null;
      int[] status_debuffs = (int[]) null;
      if (is_check_resist && cond_attachment.Param != null && cond_attachment.Param.IsLinkBuffResist && UnityEngine.Object.op_Implicit((UnityEngine.Object) SceneBattle.Instance) && SceneBattle.Instance.Battle != null)
      {
        BattleCore battle = SceneBattle.Instance.Battle;
        flag1 = battle.IsBuffDisableAndResist(this, linkageBuff);
        flag2 = battle.IsDebuffDisableAndResist(this, linkageBuff);
        if (flag1)
        {
          status_buffs = new int[StatusParam.MAX_STATUS];
          for (int index = 0; index < status_buffs.Length; ++index)
            status_buffs[index] = 0;
          battle.GetResistStatusBuff(this, ref status_buffs);
          this.ApplyStatusResistBuff(status_buffs, ref status1);
          this.ApplyStatusResistBuff(status_buffs, ref status2);
          this.ApplyStatusResistBuff(status_buffs, ref status3);
        }
        if (flag2)
        {
          status_debuffs = new int[StatusParam.MAX_STATUS];
          for (int index = 0; index < status_debuffs.Length; ++index)
            status_debuffs[index] = 0;
          battle.GetResistStatusDebuff(this, ref status_debuffs);
          this.ApplyStatusResistBuff(status_debuffs, ref status4);
          this.ApplyStatusResistBuff(status_debuffs, ref status5);
          this.ApplyStatusResistBuff(status_debuffs, ref status6);
        }
      }
      cond_attachment.LinkageID = this.GenerateCondLinkageID();
      bool flag3 = false;
      if (flag1)
      {
        if (linkageBuff.CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Add, false))
        {
          BuffAttachment linkageBuffAttachment = this.CreateCondLinkageBuffAttachment(this, cond_attachment, BuffTypes.Buff, false, SkillParamCalcTypes.Add, status1, turn);
          if (this.SetBuffAttachment(linkageBuffAttachment))
          {
            flag3 = true;
            linkageBuffAttachment.EntryResistStatusBuffList(status_buffs);
          }
        }
        if (linkageBuff.CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Add, true))
        {
          BuffAttachment linkageBuffAttachment = this.CreateCondLinkageBuffAttachment(this, cond_attachment, BuffTypes.Buff, true, SkillParamCalcTypes.Add, status2, turn);
          if (this.SetBuffAttachment(linkageBuffAttachment))
          {
            flag3 = true;
            linkageBuffAttachment.EntryResistStatusBuffList(status_buffs);
          }
        }
        if (linkageBuff.CheckBuffCalcType(BuffTypes.Buff, SkillParamCalcTypes.Scale))
        {
          BuffAttachment linkageBuffAttachment = this.CreateCondLinkageBuffAttachment(this, cond_attachment, BuffTypes.Buff, false, SkillParamCalcTypes.Scale, status3, turn);
          if (this.SetBuffAttachment(linkageBuffAttachment))
          {
            flag3 = true;
            linkageBuffAttachment.EntryResistStatusBuffList(status_buffs);
          }
        }
      }
      if (flag2)
      {
        if (linkageBuff.CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Add, false))
        {
          BuffAttachment linkageBuffAttachment = this.CreateCondLinkageBuffAttachment(this, cond_attachment, BuffTypes.Debuff, false, SkillParamCalcTypes.Add, status4, turn);
          if (this.SetBuffAttachment(linkageBuffAttachment))
          {
            flag3 = true;
            linkageBuffAttachment.EntryResistStatusBuffList(status_debuffs);
          }
        }
        if (linkageBuff.CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Add, true))
        {
          BuffAttachment linkageBuffAttachment = this.CreateCondLinkageBuffAttachment(this, cond_attachment, BuffTypes.Debuff, true, SkillParamCalcTypes.Add, status5, turn);
          if (this.SetBuffAttachment(linkageBuffAttachment))
          {
            flag3 = true;
            linkageBuffAttachment.EntryResistStatusBuffList(status_debuffs);
          }
        }
        if (linkageBuff.CheckBuffCalcType(BuffTypes.Debuff, SkillParamCalcTypes.Scale))
        {
          BuffAttachment linkageBuffAttachment = this.CreateCondLinkageBuffAttachment(this, cond_attachment, BuffTypes.Debuff, false, SkillParamCalcTypes.Scale, status6, turn);
          if (this.SetBuffAttachment(linkageBuffAttachment))
          {
            flag3 = true;
            linkageBuffAttachment.EntryResistStatusBuffList(status_debuffs);
          }
        }
      }
      if (flag3)
      {
        status1.Add(status2);
        status1.Add(status4);
        status1.Add(status5);
        status3.Add(status6);
        BattleCore.SetBuffBits(status1, ref this.CondLinkageBuff, ref this.CondLinkageDebuff);
        BattleCore.SetBuffBits(status3, ref this.CondLinkageBuff, ref this.CondLinkageDebuff);
      }
      this.UpdateBuffEffects();
      return true;
    }

    private BuffAttachment CreateCondLinkageBuffAttachment(
      Unit target,
      CondAttachment cond_attachment,
      BuffTypes buff_type,
      bool is_negative_value_is_buff,
      SkillParamCalcTypes calc_type,
      BaseStatus status,
      int turn)
    {
      if (cond_attachment == null)
        return (BuffAttachment) null;
      BuffEffect linkageBuff = cond_attachment.LinkageBuff;
      if (linkageBuff == null)
        return (BuffAttachment) null;
      BuffAttachment linkageBuffAttachment = new BuffAttachment(linkageBuff);
      linkageBuffAttachment.user = (Unit) null;
      linkageBuffAttachment.skill = (SkillData) null;
      linkageBuffAttachment.skilltarget = SkillEffectTargets.Self;
      linkageBuffAttachment.IsPassive = (OBool) (linkageBuff.param.chk_timing == EffectCheckTimings.Eternal);
      linkageBuffAttachment.CheckTarget = (Unit) null;
      linkageBuffAttachment.DuplicateCount = 0;
      if (cond_attachment.Param != null && cond_attachment.Param.IsLinkBuffDupli)
      {
        linkageBuffAttachment.user = cond_attachment.user;
        linkageBuffAttachment.skill = cond_attachment.skill;
        linkageBuffAttachment.skilltarget = cond_attachment.skilltarget;
        linkageBuffAttachment.CheckTarget = cond_attachment.CheckTarget;
        linkageBuffAttachment.DuplicateCount = cond_attachment.skill == null ? 0 : cond_attachment.skill.DuplicateCount;
      }
      linkageBuffAttachment.CheckTiming = linkageBuff.param.chk_timing;
      if (turn == 0)
        turn = (int) linkageBuff.param.turn;
      linkageBuffAttachment.turn = (OInt) turn;
      linkageBuffAttachment.BuffType = buff_type;
      linkageBuffAttachment.IsNegativeValueIsBuff = is_negative_value_is_buff;
      linkageBuffAttachment.CalcType = calc_type;
      linkageBuffAttachment.UseCondition = linkageBuff.param.cond;
      linkageBuffAttachment.LinkageID = cond_attachment.LinkageID;
      status.CopyTo(linkageBuffAttachment.status);
      return linkageBuffAttachment;
    }

    public void CureCondEffects(EUnitCondition target, bool updated = true, bool forced = false)
    {
      if (this.CheckCancelSkillCureCondition(target))
        this.CancelCastSkill();
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (!(bool) condAttachment.IsPassive && (condAttachment.UseCondition == ESkillCondition.None || condAttachment.UseCondition == ESkillCondition.Weather) && (!condAttachment.IsCurse || forced) && condAttachment.Condition == target)
        {
          switch (condAttachment.CondType)
          {
            case ConditionEffectTypes.FailCondition:
            case ConditionEffectTypes.ForcedFailCondition:
            case ConditionEffectTypes.RandomFailCondition:
              this.CondAttachments.RemoveAt(index--);
              continue;
            default:
              continue;
          }
        }
      }
      if (!updated)
        return;
      this.UpdateCondEffects();
    }

    public void UpdateCondEffects()
    {
      long num1 = 0;
      long num2 = 0;
      long num3 = 0;
      bool flag = false;
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (!(bool) condAttachment.IsPassive && condAttachment.CheckTiming != EffectCheckTimings.Eternal)
        {
          if (condAttachment.IsCurse)
          {
            if (condAttachment.user != null && condAttachment.user.IsDead)
            {
              if (condAttachment.LinkageBuff != null)
                flag = true;
              this.CondAttachments.RemoveAt(index--);
            }
          }
          else if ((int) condAttachment.turn <= 0 && (!condAttachment.IsFailCondition() || this.IsAutoCureCondition(condAttachment.Condition)))
          {
            if (condAttachment.LinkageBuff != null)
              flag = true;
            this.CondAttachments.RemoveAt(index--);
          }
        }
      }
      if (flag)
        this.UpdateBuffEffects();
      for (int index1 = 0; index1 < this.CondAttachments.Count; ++index1)
      {
        CondAttachment condAttachment = this.CondAttachments[index1];
        if (condAttachment.UseCondition != ESkillCondition.None)
        {
          switch (condAttachment.UseCondition)
          {
            case ESkillCondition.Dying:
              if (this.IsDying())
                break;
              continue;
            case ESkillCondition.JudgeHP:
              if (condAttachment.skill == null || !condAttachment.skill.IsJudgeHp(this))
                continue;
              break;
          }
        }
        for (int index2 = 0; index2 < (int) Unit.MAX_UNIT_CONDITION; ++index2)
        {
          EUnitCondition eunitCondition = (EUnitCondition) (1L << index2);
          if (condAttachment.Condition == eunitCondition)
          {
            switch (condAttachment.CondType)
            {
              case ConditionEffectTypes.FailCondition:
              case ConditionEffectTypes.ForcedFailCondition:
              case ConditionEffectTypes.RandomFailCondition:
                if (condAttachment.IsCurse)
                  num3 |= 1L << index2;
                num1 |= 1L << index2;
                continue;
              case ConditionEffectTypes.DisableCondition:
                num2 |= 1L << index2;
                continue;
              default:
                continue;
            }
          }
        }
      }
      this.mCurrentCondition = (OLong) 0L;
      this.mDisableCondition = (OLong) 0L;
      for (int index = 0; index < (int) Unit.MAX_UNIT_CONDITION; ++index)
      {
        long target = 1L << index;
        if ((num2 & target) != 0L)
        {
          this.CureCondEffects((EUnitCondition) target, false);
          if ((num3 & target) == 0L)
            continue;
        }
        if ((num1 & target) != 0L)
        {
          Unit unit = this;
          unit.mCurrentCondition = (OLong) ((long) unit.mCurrentCondition | target);
        }
      }
      this.mDisableCondition = (OLong) num2;
      if (!this.IsUnitCondition(EUnitCondition.DeathSentence))
        this.mDeathCount = (OInt) -1;
      if (!this.IsUnitCondition(EUnitCondition.Rage))
        this.mRageTarget = (Unit) null;
      if (!this.IsUnitCondition(EUnitCondition.Paralysed))
        this.SetUnitFlag(EUnitFlag.Paralysed, false);
      if (!this.IsUnitCondition(EUnitCondition.Stone))
        return;
      this.mCurrentCondition = (OLong) 32L;
    }

    public void UpdateCondEffectTurnCount(EffectCheckTimings timing, Unit current)
    {
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (!(bool) condAttachment.IsPassive)
        {
          if (condAttachment.CheckTiming != EffectCheckTimings.ClockCountUp && condAttachment.CheckTiming != EffectCheckTimings.Moment)
          {
            if (condAttachment.CheckTarget != null)
            {
              if (condAttachment.CheckTarget != current)
                continue;
            }
            else if (current != null && current != this)
              continue;
          }
          EffectCheckTimings checkTiming = condAttachment.CheckTiming;
          if (checkTiming == timing && checkTiming != EffectCheckTimings.Eternal)
          {
            if (condAttachment.IsFailCondition())
            {
              EUnitCondition condition = condAttachment.Condition;
              if (this.mCastSkill != null && this.mCastSkill.CastType == ECastTypes.Jump && condAttachment.CheckTiming == EffectCheckTimings.ActionStart || this.IsUnitCondition(EUnitCondition.Stone) && condition != EUnitCondition.Stone || !this.IsAutoCureCondition(condition))
                continue;
            }
            --condAttachment.turn;
          }
        }
      }
    }

    private void UpdateDeathSentence()
    {
      if (!this.IsUnitCondition(EUnitCondition.DeathSentence) || this.IsUnitCondition(EUnitCondition.Stone) || this.mCastSkill != null && this.mCastSkill.CastType == ECastTypes.Jump)
        return;
      this.mDeathCount = (OInt) Math.Max((int) --this.mDeathCount, 0);
    }

    public bool CheckEventTrigger(EEventTrigger trigger)
    {
      return this.mEventTrigger != null && this.mEventTrigger.Count > 0 && this.mEventTrigger.Trigger == trigger;
    }

    public void DecrementTriggerCount()
    {
      if (this.mEventTrigger == null)
        return;
      this.mEventTrigger.Count = Math.Min(--this.mEventTrigger.Count, 0);
    }

    public bool CheckWinEventTrigger()
    {
      if (this.mEventTrigger != null && this.mEventTrigger.EventType == EEventType.Win && !this.IsUnitFlag(EUnitFlag.UnitTransformed))
      {
        if (this.mEventTrigger.Trigger == EEventTrigger.Dead)
          return this.IsDead;
        if (this.mEventTrigger.Trigger == EEventTrigger.HpDownBorder)
          return this.mMaximumStatusHp * this.mEventTrigger.IntValue / 100 >= (int) this.CurrentStatus.param.hp;
      }
      return false;
    }

    public bool CheckLoseEventTrigger()
    {
      if (this.mEventTrigger != null && this.mEventTrigger.EventType == EEventType.Lose && !this.IsUnitFlag(EUnitFlag.UnitTransformed))
      {
        if (this.mEventTrigger.Trigger == EEventTrigger.Dead)
          return this.IsDead;
        if (this.mEventTrigger.Trigger == EEventTrigger.HpDownBorder)
          return this.mMaximumStatusHp * this.mEventTrigger.IntValue / 100 >= (int) this.CurrentStatus.param.hp;
      }
      return false;
    }

    public bool CheckNeedEscaped()
    {
      return this.AI != null && (int) this.AI.escape_border != 0 && !this.IsUnitCondition(EUnitCondition.DisableHeal) && (int) this.MaximumStatus.param.hp != 0 && (int) this.MaximumStatus.param.hp * (int) this.AI.escape_border >= (int) this.CurrentStatus.param.hp * 100;
    }

    public bool CheckEnableEntry()
    {
      if (this.IsUnitFlag(EUnitFlag.OtherTeam) || this.IsDead)
        return false;
      if (this.EntryUnit && !this.IsUnitFlag(EUnitFlag.Reinforcement) && this.IsSub)
        return true;
      if (this.IsSub || this.IsEntry)
        return false;
      if (this.EntryUnit)
        return true;
      if (this.mEntryTriggers == null)
        return (int) this.mWaitEntryClock == 0;
      bool flag = true;
      for (int index = 0; index < this.mEntryTriggers.Count; ++index)
      {
        UnitEntryTrigger mEntryTrigger = this.mEntryTriggers[index];
        if (!(bool) this.IsEntryTriggerAndCheck && mEntryTrigger.on)
          return true;
        flag &= mEntryTrigger.on;
      }
      return (bool) this.IsEntryTriggerAndCheck && flag;
    }

    public void UpdateClockTime()
    {
      if (!this.IsEntry)
        this.mWaitEntryClock = (OInt) Math.Max((int) --this.mWaitEntryClock, 0);
      this.UpdateBuffEffectTurnCount(EffectCheckTimings.ClockCountUp, this);
      this.UpdateCondEffectTurnCount(EffectCheckTimings.ClockCountUp, this);
      this.UpdateBuffEffects();
      this.UpdateCondEffects();
    }

    public OInt GetChargeSpeed()
    {
      if (this.IsUnitCondition(EUnitCondition.Stop))
        return (OInt) 0;
      int spd = (int) this.CurrentStatus.param.spd;
      if (this.IsUnitCondition(EUnitCondition.Fast))
      {
        int val1 = 0;
        for (int index = 0; index < this.CondAttachments.Count; ++index)
        {
          CondAttachment condAttachment = this.CondAttachments[index];
          if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.Fast))
          {
            if (condAttachment.skill != null)
            {
              CondEffect condEffect1 = condAttachment.skill.GetCondEffect();
              if (condEffect1 != null)
                val1 = Math.Max(val1, Math.Abs((int) condEffect1.param.v_fast));
              CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
              if (condEffect2 != null)
                val1 = Math.Max(val1, Math.Abs((int) condEffect2.param.v_fast));
            }
            else
              val1 = condAttachment.Param == null ? Math.Abs((int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.DefaultClockUpValue) : Math.Max(val1, Math.Abs((int) condAttachment.Param.v_fast));
          }
        }
        if (val1 == 0)
          val1 = Math.Abs((int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.DefaultClockUpValue);
        spd += spd * val1 / 100;
      }
      if (this.IsUnitCondition(EUnitCondition.Slow))
      {
        int val1 = 0;
        for (int index = 0; index < this.CondAttachments.Count; ++index)
        {
          CondAttachment condAttachment = this.CondAttachments[index];
          if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.Slow))
          {
            if (condAttachment.skill != null)
            {
              CondEffect condEffect3 = condAttachment.skill.GetCondEffect();
              if (condEffect3 != null)
                val1 = Math.Max(val1, Math.Abs((int) condEffect3.param.v_slow));
              CondEffect condEffect4 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
              if (condEffect4 != null)
                val1 = Math.Max(val1, Math.Abs((int) condEffect4.param.v_slow));
            }
            else
              val1 = condAttachment.Param == null ? Math.Abs((int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.DefaultClockDownValue) : Math.Max(val1, Math.Abs((int) condAttachment.Param.v_slow));
          }
        }
        if (val1 == 0)
          val1 = Math.Abs((int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.DefaultClockDownValue);
        spd -= spd * val1 / 100;
      }
      return (OInt) spd;
    }

    public bool UpdateChargeTime()
    {
      if (this.IsUnitCondition(EUnitCondition.Stop) || this.mCastSkill != null && this.mCastSkill.SkillParam.IsNoChargeCalcCT())
        return false;
      Unit unit = this;
      unit.mChargeTime = (OInt) ((int) unit.mChargeTime + (int) this.GetChargeSpeed());
      return true;
    }

    public bool CheckChargeTimeFullOver()
    {
      return !this.IsUnitCondition(EUnitCondition.Stop) && (int) this.mChargeTime >= (int) this.ChargeTimeMax;
    }

    public OInt GetCastSpeed() => this.GetCastSpeed(this.mCastSkill);

    public OInt GetCastSpeed(SkillData skill)
    {
      if (this.IsUnitCondition(EUnitCondition.Stop))
        return (OInt) 0;
      if (skill == null)
        return (OInt) 0;
      int castSpeed = (int) skill.CastSpeed;
      if (this.IsUnitCondition(EUnitCondition.Fast))
        castSpeed += castSpeed * 50 / 100;
      if (this.IsUnitCondition(EUnitCondition.Slow))
        castSpeed -= castSpeed * 50 / 100;
      return (OInt) castSpeed;
    }

    public bool UpdateCastTime()
    {
      if (this.mCastSkill == null || this.IsUnitCondition(EUnitCondition.Stop))
        return false;
      Unit unit = this;
      unit.mCastTime = (OInt) ((int) unit.mCastTime + (int) this.GetCastSpeed());
      return true;
    }

    public bool CheckCastTimeFullOver()
    {
      return !this.IsUnitCondition(EUnitCondition.Stop) && this.CastSkill != null && (int) this.mCastTime >= (int) this.CastTimeMax;
    }

    public void SetCastSkill(SkillData skill, Unit unit, Grid grid)
    {
      this.mCastSkill = skill;
      this.mCastTime = (OInt) 0;
      this.mCastIndex = Unit.UNIT_CAST_INDEX++;
      this.mUnitTarget = unit;
      this.mGridTarget = grid;
      this.mCastSkillGridMap = (GridMap<bool>) null;
    }

    public void CancelCastSkill()
    {
      this.mCastSkill = (SkillData) null;
      this.mCastTime = (OInt) 0;
      this.mCastIndex = (OInt) 0;
      this.mUnitTarget = (Unit) null;
      this.mGridTarget = (Grid) null;
      this.mCastSkillGridMap = (GridMap<bool>) null;
    }

    public void PushCastSkill()
    {
      this.mPushCastSkill = this.mCastSkill;
      this.mCastSkill = (SkillData) null;
    }

    public void PopCastSkill()
    {
      if (this.mPushCastSkill == null)
        return;
      this.mCastSkill = this.mPushCastSkill;
      this.mPushCastSkill = (SkillData) null;
    }

    public void SetCastTime(int time) => this.mCastTime = (OInt) time;

    public void NotifyContinue()
    {
      this.CurrentStatus.param.hp = this.MaximumStatus.param.hp;
      this.CurrentStatus.param.mp = this.MaximumStatus.param.mp;
      this.CancelCastSkill();
      this.CancelProtect();
      this.NotifyMapStart();
      this.mChargeTime = this.ChargeTimeMax;
    }

    public void NotifyMapStart()
    {
      this.SetUnitFlag(EUnitFlag.Moved, false);
      this.SetUnitFlag(EUnitFlag.Action, false);
      this.SetUnitFlag(EUnitFlag.Sneaking, false);
      this.SetUnitFlag(EUnitFlag.Paralysed, false);
      this.SetUnitFlag(EUnitFlag.ForceMoved, false);
      this.SetUnitFlag(EUnitFlag.EntryDead, false);
      this.SetUnitFlag(EUnitFlag.TriggeredAutoSkills, false);
      this.SetUnitFlag(EUnitFlag.UnitChanged, false);
      this.SetUnitFlag(EUnitFlag.UnitWithdraw, false);
      this.SetUnitFlag(EUnitFlag.ToDying, false);
      if (!this.IsUnitFlag(EUnitFlag.InfinitySpawn))
        this.SetUnitFlag(EUnitFlag.Reinforcement, false);
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        if (!(bool) this.BuffAttachments[index].IsPassive && (this.BuffAttachments[index].skill == null || !this.BuffAttachments[index].skill.IsPassiveSkill()))
          this.BuffAttachments.RemoveAt(index--);
      }
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        if (!(bool) this.CondAttachments[index].IsPassive && (this.CondAttachments[index].skill == null || !this.CondAttachments[index].skill.IsPassiveSkill()))
          this.CondAttachments.RemoveAt(index--);
      }
      this.ClearMhmDamage();
      this.mAbilityChangeLists.Clear();
      this.RefreshBattleAbilitysAndSkills();
      this.UpdateBuffEffects();
      this.UpdateCondEffects();
      this.CalcCurrentStatus();
      this.InitializeSkillUseCount();
      this.ClearJudgeHpLists();
    }

    public void NotifyActionStart(List<Unit> others)
    {
      if (others != null)
      {
        for (int index = 0; index < others.Count; ++index)
        {
          Unit other = others[index];
          other.UpdateCondEffectTurnCount(EffectCheckTimings.ActionStart, this);
          other.UpdateCondEffects();
          other.UpdateBuffEffectTurnCount(EffectCheckTimings.ActionStart, this);
          other.UpdateBuffEffectTurnCount(EffectCheckTimings.Moment, this);
          other.UpdateBuffEffectTurnCount(EffectCheckTimings.MomentStart, this);
          other.UpdateBuffEffects();
          other.setRelatedBuff(other.x, other.y);
          TrickData.MomentBuff(other, other.x, other.y);
          TrickData.MomentBuff(other, other.x, other.y, EffectCheckTimings.MomentStart);
          other.CalcCurrentStatus();
        }
      }
      this.UpdateShieldTurn();
      this.UpdateProtectTurn();
      this.UpdateForcedTargetingTurn();
      this.UpdateDeathSentence();
      if (this.mRageTarget != null && this.mRageTarget.IsDeadCondition())
        this.CureCondEffects(EUnitCondition.Rage, forced: true);
      this.startX = this.x;
      this.startY = this.y;
      this.startDir = this.Direction;
      if (!this.IsDead && this.IsEntry && !this.IsUnitFlag(EUnitFlag.FirstAction))
      {
        if (this.IsPartyMember && !this.IsUnitFlag(EUnitFlag.DisableFirstVoice) && (!SceneBattle.Instance.Battle.IsMultiPlay || SceneBattle.Instance.Battle.MultiFinishLoad))
          this.PlayBattleVoice("battle_0030");
        this.SetUnitFlag(EUnitFlag.FirstAction, true);
      }
      this.SetUnitFlag(EUnitFlag.Moved, (int) this.mMoveWaitTurn > 0);
      this.SetUnitFlag(EUnitFlag.Action, false);
      this.SetUnitFlag(EUnitFlag.Escaped, false);
      this.SetUnitFlag(EUnitFlag.Sneaking, false);
      this.SetUnitFlag(EUnitFlag.Paralysed, false);
      this.SetUnitFlag(EUnitFlag.ForceMoved, false);
      this.SetCommandFlag(EUnitCommandFlag.Move, false);
      this.SetCommandFlag(EUnitCommandFlag.Action, false);
      this.mMoveWaitTurn = (OInt) Math.Max((int) --this.mMoveWaitTurn, 0);
      ++this.mActionCount;
      if (this.mAIActionTable == null || this.mAIActionTable.actions == null || this.mAIActionTable.actions.Count <= 0 || this.mAIActionTable.actions.Count <= (int) this.mAIActionIndex)
        return;
      ++this.mAIActionTurnCount;
    }

    public void NotifyActionEnd()
    {
      this.UpdateAbilityChange();
      this.ChargeTimeDec();
      this.CalcCurrentStatus();
    }

    public void ChargeTimeDec()
    {
      FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
      Unit unit1 = this;
      unit1.mChargeTime = (OInt) ((int) unit1.mChargeTime - (int) fixParam.ChargeTimeDecWait);
      if (this.IsCommandFlag(EUnitCommandFlag.Move))
      {
        Unit unit2 = this;
        unit2.mChargeTime = (OInt) ((int) unit2.mChargeTime - (int) fixParam.ChargeTimeDecMove);
      }
      if (this.IsCommandFlag(EUnitCommandFlag.Action))
      {
        Unit unit3 = this;
        unit3.mChargeTime = (OInt) ((int) unit3.mChargeTime - (int) fixParam.ChargeTimeDecAction);
      }
      this.mChargeTime = (OInt) Math.Max((int) this.mChargeTime, 0);
    }

    public void NotifyActionEndAfter(List<Unit> others)
    {
      if (others == null)
        return;
      for (int index = 0; index < others.Count; ++index)
      {
        Unit other = others[index];
        other.UpdateCondEffectTurnCount(EffectCheckTimings.ActionEnd, this);
        other.UpdateCondEffects();
        other.UpdateBuffEffectTurnCount(EffectCheckTimings.ActionEnd, this);
        other.UpdateBuffEffectTurnCount(EffectCheckTimings.Moment, this);
        other.UpdateBuffEffects();
        other.setRelatedBuff(other.x, other.y);
        TrickData.MomentBuff(other, other.x, other.y);
        other.CalcCurrentStatus();
      }
    }

    public void NotifyCommandStart() => this.CalcCurrentStatus();

    public void RefreshMomentBuff(List<Unit> others, bool is_direct = false, int grid_x = -1, int grid_y = -1)
    {
      if (others == null)
        return;
      foreach (Unit other in others)
      {
        int grid_x1 = other.x;
        int grid_y1 = other.y;
        if (is_direct && other == this)
        {
          if (grid_x >= 0)
            grid_x1 = grid_x;
          if (grid_y >= 0)
            grid_y1 = grid_y;
        }
        other.UpdateBuffEffectTurnCount(EffectCheckTimings.Moment, this);
        if (false | other.UpdateBuffEffects() | other.setRelatedBuff(grid_x1, grid_y1, is_direct) | TrickData.MomentBuff(other, grid_x1, grid_y1))
          other.CalcCurrentStatus();
      }
    }

    public void RefreshMomentBuff(bool is_direct = false, int grid_x = -1, int grid_y = -1)
    {
      int grid_x1 = this.x;
      int grid_y1 = this.y;
      if (is_direct)
      {
        if (grid_x >= 0)
          grid_x1 = grid_x;
        if (grid_y >= 0)
          grid_y1 = grid_y;
      }
      this.UpdateBuffEffectTurnCount(EffectCheckTimings.Moment, this);
      if (!(false | this.UpdateBuffEffects() | this.setRelatedBuff(grid_x1, grid_y1, is_direct) | TrickData.MomentBuff(this, grid_x1, grid_y1)))
        return;
      this.CalcCurrentStatus();
    }

    public bool CheckEnemySide(Unit target)
    {
      if (this == target)
        return false;
      if (!this.IsUnitCondition(EUnitCondition.Charm) && !this.IsUnitCondition(EUnitCondition.Zombie))
        return this.Side != target.Side;
      return !target.IsUnitCondition(EUnitCondition.Charm) && !target.IsUnitCondition(EUnitCondition.Zombie) && this.Side == target.Side;
    }

    public Unit GetRageTarget()
    {
      return this.IsUnitCondition(EUnitCondition.Rage) ? this.mRageTarget : (Unit) null;
    }

    public SkillData GetAttackSkill() => this.UnitData.GetAttackSkill();

    public void Heal(int value)
    {
      this.CurrentStatus.param.hp = (OInt) Mathf.Clamp((int) this.CurrentStatus.param.hp + value, 0, (int) this.MaximumStatus.param.hp);
      this.AddBossCurrentHP(value);
    }

    public void Damage(int value, bool is_check_dying = false)
    {
      bool flag = this.IsDying();
      if (value > (int) this.CurrentStatus.param.hp)
        this.mOverKillDamage = value - (int) this.CurrentStatus.param.hp;
      this.CurrentStatus.param.hp = (OInt) Math.Max((int) this.CurrentStatus.param.hp - value, 0);
      this.AddBossCurrentHP(-value);
      if (!is_check_dying || flag || !this.IsDying())
        return;
      this.SetUnitFlag(EUnitFlag.ToDying, true);
    }

    public void ForceDead()
    {
      this.CurrentStatus.param.hp = (OInt) 0;
      this.mDeathCount = (OInt) 0;
      this.mChargeTime = (OInt) 0;
      this.CancelCastSkill();
      this.CancelProtect();
      this.CancelForcedTargeting();
      this.SetUnitFlag(EUnitFlag.EntryDead, true);
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        if (!(bool) this.BuffAttachments[index].IsPassive && (this.BuffAttachments[index].skill == null || !this.BuffAttachments[index].skill.IsPassiveSkill()))
          this.BuffAttachments.RemoveAt(index--);
      }
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        if (!(bool) this.CondAttachments[index].IsPassive && (this.CondAttachments[index].skill == null || !this.CondAttachments[index].skill.IsPassiveSkill()))
          this.CondAttachments.RemoveAt(index--);
      }
      this.UpdateBuffEffects();
      this.UpdateCondEffects();
    }

    public void SetDeathCount(int count) => this.mDeathCount = (OInt) count;

    public int GetPoisonDamage()
    {
      FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
      bool flag = false;
      int val1_1 = 0;
      int val1_2 = 0;
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.Poison))
        {
          int num1 = 0;
          int num2 = 0;
          if (condAttachment.skill != null)
          {
            CondEffect condEffect1 = condAttachment.skill.GetCondEffect();
            if (condEffect1 != null)
            {
              if ((int) condEffect1.param.v_poison_rate != 0)
                num1 = Math.Max(num1, (int) condEffect1.param.v_poison_rate);
              if ((int) condEffect1.param.v_poison_fix != 0)
                num2 = Math.Max(num2, (int) condEffect1.param.v_poison_fix);
            }
            CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
            if (condEffect2 != null)
            {
              if ((int) condEffect2.param.v_poison_rate != 0)
                num1 = Math.Max(num1, (int) condEffect2.param.v_poison_rate);
              if ((int) condEffect2.param.v_poison_fix != 0)
                num2 = Math.Max(num2, (int) condEffect2.param.v_poison_fix);
            }
          }
          else if (condAttachment.Param != null)
          {
            if ((int) condAttachment.Param.v_poison_rate != 0)
              num1 = Math.Max(num1, (int) condAttachment.Param.v_poison_rate);
            if ((int) condAttachment.Param.v_poison_fix != 0)
              num2 = Math.Max(num2, (int) condAttachment.Param.v_poison_fix);
          }
          if (num1 == 0 && num2 == 0)
            num1 = (int) fixParam.PoisonDamageRate;
          if (condAttachment.user != null)
          {
            if (num1 != 0)
              num1 += (int) condAttachment.user.CurrentStatus[BattleBonus.PoisonDamage];
            if (num2 != 0)
              num2 += num2 * (int) condAttachment.user.CurrentStatus[BattleBonus.PoisonDamage] / 100;
          }
          flag = true;
          val1_1 = Math.Max(val1_1, num1);
          val1_2 = Math.Max(val1_2, num2);
        }
      }
      if (!flag)
        val1_1 = (int) fixParam.PoisonDamageRate;
      int val2 = Math.Max(val1_1 == 0 ? 0 : (int) this.MaximumStatus.param.hp * val1_1 / 100, 1);
      return Math.Max(val1_2, val2);
    }

    public int GetParalyseRate()
    {
      if (!this.IsUnitCondition(EUnitCondition.Paralysed))
        return 0;
      int val1 = 0;
      bool flag = false;
      FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.Paralysed))
        {
          int num = 0;
          if (condAttachment.skill != null)
          {
            CondEffect condEffect1 = condAttachment.skill.GetCondEffect();
            if (condEffect1 != null && (int) condEffect1.param.v_paralyse_rate != 0)
              num = Math.Max(num, (int) condEffect1.param.v_paralyse_rate);
            CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
            if (condEffect2 != null && (int) condEffect2.param.v_paralyse_rate != 0)
              num = Math.Max(num, (int) condEffect2.param.v_paralyse_rate);
          }
          else if (condAttachment.Param != null && (int) condAttachment.Param.v_paralyse_rate != 0)
            num = Math.Max(num, (int) condAttachment.Param.v_paralyse_rate);
          if (num == 0)
            num = (int) fixParam.ParalysedRate;
          flag = true;
          val1 = Math.Max(val1, num);
        }
      }
      if (!flag)
        val1 = (int) fixParam.ParalysedRate;
      return val1;
    }

    public int GetAttackRangeMin() => this.GetAttackRangeMin(this.GetAttackSkill());

    public int GetAttackRangeMin(SkillData skill)
    {
      return skill == null || skill.SkillParam.select_range == ESelectType.All ? 0 : this.UnitData.GetAttackRangeMin(skill);
    }

    public int GetAttackRangeMax() => this.GetAttackRangeMax(this.GetAttackSkill());

    public int GetAttackRangeMax(SkillData skill)
    {
      if (skill == null)
        return 0;
      int val1 = this.UnitData.GetAttackRangeMax(skill);
      if (skill.IsEnableChangeRange())
        val1 += (int) this.mCurrentStatus[BattleBonus.EffectRange];
      if (skill.SkillParam.select_range == ESelectType.All)
        val1 = 99;
      return Math.Max(val1, 0);
    }

    public int GetAttackScope() => this.GetAttackScope(this.GetAttackSkill());

    public int GetAttackScope(SkillData skill)
    {
      if (skill == null)
        return 0;
      int attackScope = this.UnitData.GetAttackScope(skill);
      if (skill.IsEnableChangeRange())
        attackScope += (int) this.mCurrentStatus[BattleBonus.EffectScope];
      return Math.Max(attackScope, 0);
    }

    public int GetAttackHeight() => this.GetAttackHeight(this.GetAttackSkill());

    public int GetAttackHeight(SkillData skill, bool is_range = false)
    {
      if (skill == null)
        return 0;
      int attackHeight = this.UnitData.GetAttackHeight(skill, is_range);
      if (skill.IsEnableChangeRange())
        attackHeight += (int) this.mCurrentStatus[BattleBonus.EffectHeight];
      return Math.Max(attackHeight, 1);
    }

    public int GetMoveCount(bool bCondOnly = false)
    {
      if (!this.IsEnableMoveCondition(bCondOnly))
        return 0;
      if (!this.IsUnitCondition(EUnitCondition.Donsoku))
        return (int) this.mCurrentStatus.param.mov;
      int val1 = (int) byte.MaxValue;
      bool flag = false;
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.Donsoku))
        {
          int num = (int) byte.MaxValue;
          if (condAttachment.skill != null)
          {
            CondEffect condEffect1 = condAttachment.skill.GetCondEffect();
            if (condEffect1 != null && (int) condEffect1.param.v_donmov > 0)
              num = Math.Min(num, (int) condEffect1.param.v_donmov);
            CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
            if (condEffect2 != null && (int) condEffect2.param.v_donmov > 0)
              num = Math.Min(num, (int) condEffect2.param.v_donmov);
          }
          else if (condAttachment.Param != null && (int) condAttachment.Param.v_donmov > 0)
            num = Math.Min(num, (int) condAttachment.Param.v_donmov);
          if (num == (int) byte.MaxValue)
            num = 1;
          val1 = Math.Min(val1, num);
          flag = true;
        }
      }
      return flag ? val1 : 1;
    }

    public int GetMoveHeight() => Math.Max((int) this.mCurrentStatus.param.jmp, 1);

    public void SetSearchRange(int value) => this.mSearched = (OInt) value;

    public int GetSearchRange() => (int) this.mSearched;

    public int GetCombination() => this.UnitData.GetCombination();

    public int GetCombinationRange()
    {
      return this.UnitData.GetCombinationRange() + (int) this.CurrentStatus[BattleBonus.CombinationRange];
    }

    public AbilityData GetAbilityData(long iid)
    {
      if (iid <= 0L)
        return (AbilityData) null;
      for (int index = 0; index < this.BattleAbilitys.Count; ++index)
      {
        if (iid == this.BattleAbilitys[index].UniqueID)
          return this.BattleAbilitys[index];
      }
      return (AbilityData) null;
    }

    public SkillData GetSkillData(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (SkillData) null;
      SkillData attackSkill = this.GetAttackSkill();
      if (attackSkill != null && iname == attackSkill.SkillID)
        return attackSkill;
      for (int index = 0; index < this.BattleSkills.Count; ++index)
      {
        if (iname == this.BattleSkills[index].SkillID)
          return this.BattleSkills[index];
      }
      if (this.UnitData.ConceptCards != null)
      {
        for (int index = 0; index < this.UnitData.ConceptCards.Length; ++index)
        {
          ConceptCardData conceptCard = this.UnitData.ConceptCards[index];
          if (conceptCard != null)
          {
            foreach (SkillData enableCardSkill in conceptCard.GetEnableCardSkills(this.UnitData))
            {
              if (iname == enableCardSkill.SkillID)
                return enableCardSkill;
            }
          }
        }
      }
      List<AbilityData> abilityDataList1 = new List<AbilityData>();
      abilityDataList1.AddRange((IEnumerable<AbilityData>) this.mUnitData.BattleAbilitys);
      abilityDataList1.AddRange((IEnumerable<AbilityData>) this.mAddedAbilitys);
      List<AbilityData> abilityDataList2 = new List<AbilityData>();
      foreach (AbilityData abilityData in abilityDataList1)
      {
        AbilityData ability = abilityData;
        if (this.BattleAbilitys.Find((Predicate<AbilityData>) (t => t.AbilityID == ability.AbilityID)) == null && !abilityDataList2.Contains(ability))
          abilityDataList2.Add(ability);
      }
      foreach (AbilityData abilityData in abilityDataList2)
      {
        if (abilityData.Skills != null)
        {
          foreach (SkillData skill in abilityData.Skills)
          {
            if (skill != null && iname == skill.SkillID)
              return skill;
          }
        }
      }
      return this.FindAdditionalSkills(iname) ?? (SkillData) null;
    }

    public int GetSkillUsedCost(SkillData skill)
    {
      int skillUsedCost = skill.Cost;
      if (skill.EffectType != SkillEffectTypes.GemsGift && !skill.IsNormalAttack() && !skill.IsItem() && !skill.SkillParam.IsNoUsedJewelBuff())
      {
        int num = Math.Max(skillUsedCost + (int) this.CurrentStatus[BattleBonus.UsedJewel], 0);
        skillUsedCost = Math.Max(num + num * (int) this.CurrentStatus[BattleBonus.UsedJewelRate] / 100, 0);
      }
      return skillUsedCost;
    }

    public EElement GetWeakElement() => UnitParam.GetWeakElement(this.Element);

    public int CalcParamRecover(int val)
    {
      return (int) this.CurrentStatus.param.rec <= 0 ? 0 : val * (int) this.CurrentStatus.param.rec / 100;
    }

    public int GetAutoHpHealValue()
    {
      if (this.IsUnitCondition(EUnitCondition.DisableHeal) || this.IsUseBossLongHP())
        return 0;
      int autoHpHealValue = 0;
      if (this.IsUnitCondition(EUnitCondition.AutoHeal))
      {
        int val1 = 0;
        bool flag = false;
        FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
        for (int index = 0; index < this.CondAttachments.Count; ++index)
        {
          CondAttachment condAttachment = this.CondAttachments[index];
          if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.AutoHeal))
          {
            int num = 0;
            if (condAttachment.skill != null)
            {
              CondEffect condEffect1 = condAttachment.skill.GetCondEffect();
              if (condEffect1 != null)
              {
                if ((int) condEffect1.param.v_auto_hp_heal > 0)
                  num = Math.Max(num, (int) this.MaximumStatus.param.hp * (int) condEffect1.param.v_auto_hp_heal / 100);
                if ((int) condEffect1.param.v_auto_hp_heal_fix > 0)
                  num = Math.Max(num, (int) condEffect1.param.v_auto_hp_heal_fix);
              }
              CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
              if (condEffect2 != null)
              {
                if ((int) condEffect2.param.v_auto_hp_heal > 0)
                  num = Math.Max(num, (int) this.MaximumStatus.param.hp * (int) condEffect2.param.v_auto_hp_heal / 100);
                if ((int) condEffect2.param.v_auto_hp_heal_fix > 0)
                  num = Math.Max(num, (int) condEffect2.param.v_auto_hp_heal_fix);
              }
            }
            else if (condAttachment.Param != null)
            {
              if ((int) condAttachment.Param.v_auto_hp_heal > 0)
                num = Math.Max(num, (int) this.MaximumStatus.param.hp * (int) condAttachment.Param.v_auto_hp_heal / 100);
              if ((int) condAttachment.Param.v_auto_hp_heal_fix > 0)
                num = Math.Max(num, (int) condAttachment.Param.v_auto_hp_heal_fix);
            }
            if (num == 0)
              num = (int) this.MaximumStatus.param.hp * (int) fixParam.HpAutoHealRate / 100;
            flag = true;
            val1 = Math.Max(val1, num);
          }
        }
        if (!flag)
          val1 = (int) this.MaximumStatus.param.hp * (int) fixParam.HpAutoHealRate / 100;
        autoHpHealValue += val1;
      }
      if (this.IsUnitCondition(EUnitCondition.Sleep) && this.IsUnitCondition(EUnitCondition.GoodSleep))
        autoHpHealValue += this.GetGoodSleepHpHealValue();
      return autoHpHealValue;
    }

    public bool CheckAutoHpHeal() => this.GetAutoHpHealValue() > 0;

    public int GetAutoMpHealValue()
    {
      int autoJewel = this.AutoJewel;
      if (this.IsUnitCondition(EUnitCondition.AutoJewel))
      {
        bool flag = false;
        int val1 = 0;
        FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
        for (int index = 0; index < this.CondAttachments.Count; ++index)
        {
          CondAttachment condAttachment = this.CondAttachments[index];
          if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.AutoJewel))
          {
            int num = 0;
            if (condAttachment.skill != null)
            {
              CondEffect condEffect1 = condAttachment.skill.GetCondEffect();
              if (condEffect1 != null)
              {
                if ((int) condEffect1.param.v_auto_mp_heal > 0)
                  num = Math.Max(num, (int) this.MaximumStatus.param.mp * (int) condEffect1.param.v_auto_mp_heal / 100);
                if ((int) condEffect1.param.v_auto_mp_heal_fix > 0)
                  num = Math.Max(num, (int) condEffect1.param.v_auto_mp_heal_fix);
              }
              CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
              if (condEffect2 != null)
              {
                if ((int) condEffect2.param.v_auto_mp_heal > 0)
                  num = Math.Max(num, (int) this.MaximumStatus.param.mp * (int) condEffect2.param.v_auto_mp_heal / 100);
                if ((int) condEffect2.param.v_auto_mp_heal_fix > 0)
                  num = Math.Max(num, (int) condEffect2.param.v_auto_mp_heal_fix);
              }
            }
            else if (condAttachment.Param != null)
            {
              if ((int) condAttachment.Param.v_auto_mp_heal > 0)
                num = Math.Max(num, (int) this.MaximumStatus.param.mp * (int) condAttachment.Param.v_auto_mp_heal / 100);
              if ((int) condAttachment.Param.v_auto_mp_heal_fix > 0)
                num = Math.Max(num, (int) condAttachment.Param.v_auto_mp_heal_fix);
            }
            if (num == 0)
              num = (int) this.MaximumStatus.param.mp * (int) fixParam.MpAutoHealRate / 100;
            flag = true;
            val1 = Math.Max(val1, num);
          }
        }
        if (!flag)
          val1 = (int) this.MaximumStatus.param.mp * (int) fixParam.MpAutoHealRate / 100;
        autoJewel += val1;
      }
      if (this.IsUnitCondition(EUnitCondition.Sleep) && this.IsUnitCondition(EUnitCondition.GoodSleep))
        autoJewel += this.GetGoodSleepMpHealValue();
      return autoJewel;
    }

    public bool CheckAutoMpHeal() => this.GetAutoMpHealValue() > 0;

    public bool CheckItemDrop(bool waitDirection = false)
    {
      if (this.Side == EUnitSide.Player)
        return false;
      if (this.IsGimmick)
      {
        if (this.IsBreakObj)
          return this.IsDead;
        if (!this.IsDisableGimmick() || this.EventTrigger == null || this.EventTrigger.EventType != EEventType.Treasure)
          return false;
      }
      else if (!this.IsDead)
        return false;
      if (this.IsUnitFlag(EUnitFlag.UnitWithdraw) && !this.IsWithdrawDrop)
        return false;
      if (waitDirection)
      {
        if (!this.IsGimmick)
        {
          SceneBattle instance = SceneBattle.Instance;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) instance.FindUnitController(this), (UnityEngine.Object) null))
            return false;
        }
        if (this.IsDropDirection())
          return false;
      }
      return true;
    }

    public bool CheckActionSkillBuffAttachments(BuffTypes type)
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        if (!(bool) this.BuffAttachments[index].IsPassive && this.BuffAttachments[index].BuffType == type)
          return true;
      }
      return false;
    }

    public bool IsAIActionTable()
    {
      return this.mAIActionTable.actions.Count != 0 && (int) this.mAIActionIndex >= 0 && this.mAIActionTable.actions.Count > (int) this.mAIActionIndex;
    }

    public AIAction GetCurrentAIAction()
    {
      if (!this.IsAIActionTable())
        return (AIAction) null;
      return (int) this.mAIActionTurnCount < (int) this.mAIActionTable.actions[(int) this.mAIActionIndex].turn ? (AIAction) null : this.mAIActionTable.actions[(int) this.mAIActionIndex];
    }

    public void NextAIAction()
    {
      if (!this.IsAIActionTable())
        return;
      ++this.mAIActionIndex;
      this.mAIActionTurnCount = (OInt) 0;
      if (this.mAIActionTable.looped == 0)
        return;
      this.mAIActionIndex = (OInt) ((int) this.mAIActionIndex % this.mAIActionTable.actions.Count);
    }

    public AIAction SetAIAction(int index)
    {
      if (!this.IsAIActionTable())
        return (AIAction) null;
      if (index < 0 || index >= this.mAIActionTable.actions.Count)
        return (AIAction) null;
      if ((int) this.mAIActionIndex == index || (int) this.mAIActionTable.actions[index].turn != 0)
        return (AIAction) null;
      this.mAIActionIndex = (OInt) index;
      this.mAIActionTurnCount = (OInt) 0;
      return this.mAIActionTable.actions[(int) this.mAIActionIndex];
    }

    public bool IsAIPatrolTable()
    {
      return this.mAIPatrolTable != null && this.mAIPatrolTable.routes != null && (int) this.mAIPatrolIndex >= 0 && this.mAIPatrolTable.routes.Length > (int) this.mAIPatrolIndex && (this.mAIPatrolTable.keeped != 0 || !this.IsUnitFlag(EUnitFlag.Searched));
    }

    public AIPatrolPoint GetCurrentPatrolPoint()
    {
      return !this.IsAIPatrolTable() ? (AIPatrolPoint) null : this.mAIPatrolTable.routes[(int) this.mAIPatrolIndex];
    }

    public void NextPatrolPoint()
    {
      if (!this.IsAIPatrolTable())
        return;
      ++this.mAIPatrolIndex;
      if (this.mAIPatrolTable.looped == 0)
        return;
      this.mAIPatrolIndex = (OInt) ((int) this.mAIPatrolIndex % this.mAIPatrolTable.routes.Length);
    }

    public int GetConditionPriority(SkillData skill, SkillEffectTargets target)
    {
      int val1 = (int) byte.MaxValue;
      if (this.AI == null || this.AI.ConditionPriorities == null)
        return val1;
      CondEffect condEffect = skill.GetCondEffect(target);
      if (condEffect != null && condEffect.param != null && condEffect.param.conditions != null)
      {
        for (int index = 0; index < condEffect.param.conditions.Length; ++index)
        {
          int val2 = Array.IndexOf<EUnitCondition>(this.AI.ConditionPriorities, condEffect.param.conditions[index]);
          if (val2 != -1)
            val1 = Math.Max(Math.Min(val1, val2), 0);
        }
      }
      return val1;
    }

    public int GetBuffPriority(SkillData skill, SkillEffectTargets target)
    {
      int val1 = (int) byte.MaxValue;
      if (this.AI == null || this.AI.BuffPriorities == null)
        return val1;
      BuffEffect buffEffect = skill.GetBuffEffect(target);
      if (buffEffect != null && buffEffect.targets != null)
      {
        for (int index = 0; index < buffEffect.targets.Count; ++index)
        {
          int val2 = Array.IndexOf<ParamTypes>(this.AI.BuffPriorities, buffEffect.targets[index].paramType);
          if (val2 != -1)
            val1 = Math.Max(Math.Min(val1, val2), 0);
        }
      }
      if ((int) skill.ControlChargeTimeValue != 0)
      {
        int val2 = Array.IndexOf<ParamTypes>(this.AI.BuffPriorities, ParamTypes.ChargeTimeRate);
        if (val2 != -1)
          val1 = Math.Max(Math.Min(val1, val2), 0);
      }
      return val1;
    }

    public int GetActionSkillBuffValue(BuffTypes type, SkillParamCalcTypes calc, ParamTypes param)
    {
      int val2 = 0;
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        if (!(bool) buffAttachment.IsPassive && buffAttachment.BuffType == type && buffAttachment.CalcType == calc)
          val2 = Math.Max(Math.Abs(buffAttachment.status[param]), val2);
      }
      return val2;
    }

    public void GetEnableBetterBuffEffect(
      Unit self,
      SkillData skill,
      SkillEffectTargets effect_target,
      out int buff_count,
      out int buff_value,
      bool bRequestOnly = false)
    {
      buff_count = 0;
      buff_value = 0;
      BuffEffect buffEffect = skill.GetBuffEffect(effect_target);
      if (buffEffect != null && buffEffect.param != null && buffEffect.param.buffs != null)
      {
        for (int index1 = 0; index1 < buffEffect.targets.Count; ++index1)
        {
          BuffEffect.BuffTarget target = buffEffect.targets[index1];
          if (!this.IsResistBuff(self, target.buffType, buffEffect, target) && this.BuffAttachments.Find((Predicate<BuffAttachment>) (p => p.skill == skill)) == null)
          {
            int actionSkillBuffValue = this.GetActionSkillBuffValue(target.buffType, target.calcType, target.paramType);
            int num = Math.Max(Math.Abs((int) target.value) - actionSkillBuffValue, 0);
            if (num != 0)
            {
              if (bRequestOnly && this.AI != null && this.AI.BuffPriorities != null)
              {
                for (int index2 = 0; index2 < this.AI.BuffPriorities.Length; ++index2)
                {
                  if (target.paramType == this.AI.BuffPriorities[index2])
                  {
                    buff_value += num;
                    break;
                  }
                }
              }
              else
                buff_value += num;
              ++buff_count;
            }
          }
        }
      }
      if ((int) skill.ControlChargeTimeValue == 0)
        return;
      buff_value += Math.Abs((int) skill.ControlChargeTimeValue);
      ++buff_count;
    }

    public bool IsResistBuff(
      Unit self,
      BuffTypes buff_type,
      BuffEffect effect,
      BuffEffect.BuffTarget buff_target)
    {
      int buffBorder = self == null || self.AI == null ? 0 : (int) self.AI.buff_border;
      switch (buff_type)
      {
        case BuffTypes.Buff:
          if (!this.IsEnableBuffEffect(BuffTypes.Buff) && !effect.param.IsNoDisabled || Math.Max(100 - (int) this.CurrentStatus.enchant_resist.resist_buff, 0) <= buffBorder)
            return true;
          break;
        case BuffTypes.Debuff:
          if (!this.IsEnableBuffEffect(BuffTypes.Debuff) && !effect.param.IsNoDisabled || Math.Max(100 - (int) this.CurrentStatus.enchant_resist.resist_debuff, 0) <= buffBorder)
            return true;
          break;
      }
      return this.IsResistStatusBuff(buff_type, buff_target, buffBorder);
    }

    private bool IsResistStatusBuff(
      BuffTypes buff_type,
      BuffEffect.BuffTarget buff_target,
      int border)
    {
      if (buff_target == null)
        return false;
      Dictionary<ParamTypes, BattleBonus> dictionary = Unit.ResistStatusBuffDic;
      if (buff_type == BuffTypes.Debuff)
        dictionary = Unit.ResistStatusDebuffDic;
      ParamTypes paramType = buff_target.paramType;
      return dictionary.ContainsKey(paramType) && Math.Max(100 - (int) this.CurrentStatus.bonus[dictionary[paramType]], 0) <= border;
    }

    public bool ReqRevive { get; set; }

    public string GetUnitSkinVoiceSheetName(int jobIndex = -1)
    {
      return this.UnitData.GetUnitSkinVoiceSheetName(jobIndex);
    }

    public string GetUnitSkinVoiceCueName(int jobIndex = -1)
    {
      return this.UnitData.GetUnitSkinVoiceCueName(jobIndex);
    }

    public void LoadBattleVoice()
    {
      if (this.mBattleVoice != null)
        return;
      string skinVoiceSheetName = this.GetUnitSkinVoiceSheetName();
      if (string.IsNullOrEmpty(skinVoiceSheetName))
        return;
      string sheetName = "VO_" + skinVoiceSheetName;
      string cueNamePrefix = this.GetUnitSkinVoiceCueName() + "_";
      this.mBattleVoice = new MySound.Voice(sheetName, skinVoiceSheetName, cueNamePrefix);
    }

    public void PlayBattleVoice(string cueID)
    {
      if (this.mBattleVoice == null)
        return;
      this.mBattleVoice.Play(cueID);
    }

    public int CalcTowerDamege()
    {
      return this.IsUnitFlag(EUnitFlag.UnitChanged) ? this.mTowerStartHP - this.mUnitChangedHp : this.mTowerStartHP - (int) this.CurrentStatus.param.hp;
    }

    public Unit GetUnitUseCollaboSkill(SkillData skill, bool is_use_tuc = false)
    {
      int x = this.x;
      int y = this.y;
      if (is_use_tuc)
      {
        SceneBattle instance = SceneBattle.Instance;
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
          return (Unit) null;
        TacticsUnitController unitController = instance.FindUnitController(this);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) && !this.IsUnitFlag(EUnitFlag.Moved))
        {
          IntVector2 intVector2 = instance.CalcCoord(unitController.CenterPosition);
          x = intVector2.x;
          y = intVector2.y;
        }
      }
      return this.GetUnitUseCollaboSkill(skill, x, y);
    }

    public Unit GetUnitUseCollaboSkill(SkillData skill, int ux, int uy)
    {
      string partnerIname = CollaboSkillParam.GetPartnerIname(this.mUnitData.UnitParam.iname, skill.SkillParam.iname);
      if (string.IsNullOrEmpty(partnerIname))
        return (Unit) null;
      SceneBattle instance = SceneBattle.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return (Unit) null;
      BattleCore battle = instance.Battle;
      if (battle == null)
        return (Unit) null;
      if (battle.CurrentMap == null)
        return (Unit) null;
      Grid current1 = battle.CurrentMap[ux, uy];
      for (int y = current1.y - 1; y <= current1.y + 1; ++y)
      {
        for (int x = current1.x - 1; x <= current1.x + 1; ++x)
        {
          if (x != current1.x || y != current1.y)
          {
            Grid current2 = battle.CurrentMap[x, y];
            if (current2 != null)
            {
              Unit unitAtGrid = battle.FindUnitAtGrid(current2);
              if (unitAtGrid != null && unitAtGrid.mSide == this.mSide && !(unitAtGrid.UnitParam.iname != partnerIname) && unitAtGrid.GetSkillForUseCount(skill.SkillParam.iname) != null && Math.Abs(current1.height - current2.height) <= skill.SkillParam.CollaboHeight)
                return unitAtGrid;
            }
          }
        }
      }
      return (Unit) null;
    }

    public bool IsUseSkillCollabo(SkillData skill, bool is_use_tuc = false)
    {
      if (!(bool) skill.IsCollabo)
        return true;
      Unit unitUseCollaboSkill = this.GetUnitUseCollaboSkill(skill, is_use_tuc);
      return unitUseCollaboSkill != null && unitUseCollaboSkill.Gems - unitUseCollaboSkill.GetSkillUsedCost(skill) >= 0;
    }

    public SkillData GetSkillForUseCount(string skill_iname, int offs = 0)
    {
      SkillData[] array = this.mSkillUseCount.Keys.ToArray<SkillData>();
      SkillData skillForUseCount = (SkillData) null;
      for (int index = 0; index < array.Length; ++index)
      {
        SkillData skillData = array[index];
        if (skillData != null && skillData.SkillParam.iname == skill_iname)
        {
          skillForUseCount = skillData;
          if (offs > 0)
            --offs;
          else
            break;
        }
      }
      return skillForUseCount;
    }

    public bool IsNormalSize => this.SizeX == 1 && this.SizeY == 1;

    public bool IsThrow => this.mUnitData.IsThrow;

    private bool setRelatedBuff(int grid_x, int grid_y, bool is_direct = false)
    {
      SceneBattle instance = SceneBattle.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return false;
      BattleCore battle = instance.Battle;
      if (battle == null || battle.CurrentMap == null)
        return false;
      BattleMap currentMap = battle.CurrentMap;
      if (currentMap == null)
        return false;
      bool flag1 = false;
      foreach (BuffAttachment buffAttachment in this.BuffAttachments)
      {
        if (buffAttachment.CheckTiming != EffectCheckTimings.Moment && buffAttachment.Param != null && buffAttachment.Param.mEffRange == EEffRange.SelfNearAlly)
        {
          flag1 = true;
          break;
        }
      }
      List<Unit> unitList = new List<Unit>(battle.Units.Count);
      for (int index = 0; index < 4; ++index)
      {
        Grid grid = currentMap[grid_x + Unit.DIRECTION_OFFSETS[index, 0], grid_y + Unit.DIRECTION_OFFSETS[index, 1]];
        if (grid != null)
        {
          Unit unit = !is_direct ? battle.FindUnitAtGrid(grid) : battle.DirectFindUnitAtGrid(grid);
          if (unit != null && unit != this)
          {
            foreach (BuffAttachment buffAttachment in unit.BuffAttachments)
            {
              if (buffAttachment.CheckTiming != EffectCheckTimings.Moment && buffAttachment.Param != null && (buffAttachment.Param.mEffRange == EEffRange.SelfNearAlly || buffAttachment.Param.mEffRange == EEffRange.NearAllyOnly) && unit.mSide == this.mSide)
              {
                unitList.Add(unit);
                break;
              }
            }
          }
        }
      }
      if (unitList.Count == 0)
        return flag1;
      foreach (Unit unit in unitList)
      {
        foreach (BuffAttachment buffAttachment1 in unit.BuffAttachments)
        {
          if (buffAttachment1.CheckTiming != EffectCheckTimings.Moment && buffAttachment1.Param != null && buffAttachment1.Param.mEffRange != EEffRange.None && unit.mSide == this.mSide)
          {
            if (buffAttachment1.skill != null)
            {
              BuffEffect buffEffect = buffAttachment1.skill.GetBuffEffect(buffAttachment1.skilltarget);
              if (buffEffect != null && !buffEffect.CheckEnableBuffTarget(this))
                continue;
            }
            BuffAttachment buffAttachment2 = new BuffAttachment();
            buffAttachment1.CopyTo(buffAttachment2);
            buffAttachment2.CheckTiming = EffectCheckTimings.Moment;
            buffAttachment2.IsPassive = (OBool) false;
            buffAttachment2.turn = (OInt) 1;
            bool flag2 = false;
            foreach (BuffAttachment buffAttachment3 in this.BuffAttachments)
            {
              if (this.isSameBuffAttachment(buffAttachment3, buffAttachment2) && buffAttachment3.user == buffAttachment2.user)
              {
                flag2 = true;
                break;
              }
            }
            if (!flag2)
            {
              this.SetBuffAttachment(buffAttachment2);
              flag1 = true;
            }
          }
        }
      }
      return flag1;
    }

    public int GetAllyUnitNum(Unit target_unit)
    {
      if (target_unit == null || target_unit.IsDead)
        return 0;
      SceneBattle instance = SceneBattle.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return 0;
      BattleCore battle = instance.Battle;
      if (battle == null || battle.CurrentMap == null)
        return 0;
      BattleMap currentMap = battle.CurrentMap;
      int x = target_unit.x;
      int y = target_unit.y;
      if (!battle.IsBattleFlag(EBattleFlag.ComputeAI) && target_unit == battle.CurrentUnit)
      {
        TacticsUnitController unitController = instance.FindUnitController(target_unit);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController))
        {
          IntVector2 intVector2 = instance.CalcCoord(unitController.CenterPosition);
          x = intVector2.x;
          y = intVector2.y;
        }
      }
      int allyUnitNum = 0;
      for (int index = 0; index < 4; ++index)
      {
        Grid grid = currentMap[x + Unit.DIRECTION_OFFSETS[index, 0], y + Unit.DIRECTION_OFFSETS[index, 1]];
        if (grid != null)
        {
          Unit unit = !battle.IsBattleFlag(EBattleFlag.ComputeAI) ? battle.DirectFindUnitAtGrid(grid) : battle.FindUnitAtGrid(grid);
          if (unit != null && unit.mSide == target_unit.mSide)
            ++allyUnitNum;
        }
      }
      return allyUnitNum;
    }

    public int GetMapBreakNowStage(int hp)
    {
      if (this.mBreakObj == null || this.mBreakObj.rest_hps == null || this.mBreakObj.rest_hps.Length == 0)
        return 0;
      for (int index = this.mBreakObj.rest_hps.Length - 1; index >= 0; --index)
      {
        if (hp <= this.mBreakObj.rest_hps[index])
          return index + 1;
      }
      return 0;
    }

    public void SetCreateBreakObj(string break_obj_id, int create_clock)
    {
      this.mCreateBreakObjId = break_obj_id;
      this.mCreateBreakObjClock = create_clock;
    }

    public void BeginDropDirection() => this.mDropDirection = true;

    public void EndDropDirection() => this.mDropDirection = false;

    public bool IsDropDirection() => this.mDropDirection;

    public bool IsKnockBack => this.mUnitData.IsKnockBack;

    public bool IsChanging => this.mUnitData.IsChanging;

    public bool ExecuteAbilityChange(
      AbilityParam fr_ap,
      AbilityParam to_ap,
      int turn,
      bool is_reset)
    {
      if (fr_ap == null || to_ap == null)
        return false;
      if (fr_ap.slot == EAbilitySlot.Support || to_ap.slot == EAbilitySlot.Support)
      {
        DebugUtility.LogError(string.Format("アビリティ切替：サポートスキルは対象外！ from={0} to={1}", (object) fr_ap.iname, (object) to_ap.iname));
        return false;
      }
      if (fr_ap.slot != to_ap.slot)
      {
        DebugUtility.LogError(string.Format("アビリティ切替：スロットは同系統のみ！ from={0} to={1}", (object) fr_ap.iname, (object) to_ap.iname));
        return false;
      }
      Unit.eAcType eAcType = Unit.eAcType.UNKNOWN;
      Unit.AbilityChange abilityChange = (Unit.AbilityChange) null;
      for (int index = 0; index < this.mAbilityChangeLists.Count; ++index)
      {
        Unit.AbilityChange abilityChangeList = this.mAbilityChangeLists[index];
        if (abilityChangeList.IsInclude(fr_ap))
        {
          abilityChange = abilityChangeList;
          break;
        }
      }
      if (abilityChange == null)
      {
        for (int index = 0; index < this.mUnitData.BattleAbilitys.Count; ++index)
        {
          AbilityData battleAbility = this.mUnitData.BattleAbilitys[index];
          if (battleAbility.AbilityID == fr_ap.iname)
          {
            eAcType = Unit.eAcType.NEW;
            abilityChange = new Unit.AbilityChange(fr_ap, to_ap, turn == 0 ? 0 : turn + 1, is_reset, battleAbility.Exp, turn == 0);
            if (abilityChange != null)
            {
              this.mAbilityChangeLists.Add(abilityChange);
              break;
            }
            break;
          }
        }
      }
      else if (abilityChange.IsCancel(fr_ap, to_ap))
        eAcType = Unit.eAcType.CANCEL;
      else if (abilityChange.IsBack(fr_ap, to_ap))
        eAcType = Unit.eAcType.BACK;
      else if (abilityChange.GetToAp() == fr_ap)
      {
        eAcType = Unit.eAcType.ADD;
        abilityChange.Add(fr_ap, to_ap, turn == 0 ? 0 : turn + 1, is_reset, abilityChange.GetLastExp(), turn == 0);
      }
      if (eAcType == Unit.eAcType.UNKNOWN || abilityChange == null)
      {
        DebugUtility.LogWarning(string.Format("アビリティ切替スキル設定不整合！ from={0} to={1}", (object) fr_ap.iname, (object) to_ap.iname));
        return false;
      }
      switch (eAcType)
      {
        case Unit.eAcType.NEW:
        case Unit.eAcType.ADD:
          bool is_none_category = false;
          for (int index = 0; index < this.mUnitData.BattleAbilitys.Count; ++index)
          {
            AbilityData battleAbility = this.mUnitData.BattleAbilitys[index];
            if (battleAbility.AbilityID == fr_ap.iname)
            {
              is_none_category = battleAbility.IsNoneCategory;
              break;
            }
          }
          this.CreateAddedAbilityAndSkills(to_ap, abilityChange.GetLastExp(), is_none_category);
          break;
        case Unit.eAcType.CANCEL:
          abilityChange.Clear();
          this.mAbilityChangeLists.Remove(abilityChange);
          break;
        case Unit.eAcType.BACK:
          abilityChange.RemoveLast();
          break;
      }
      this.RefreshBattleAbilitysAndSkills();
      return true;
    }

    public void RefreshBattleAbilitysAndSkills()
    {
      if (this.mBattleAbilitys == null)
        this.mBattleAbilitys = new List<AbilityData>();
      this.mBattleAbilitys.Clear();
      this.mBattleAbilitys.AddRange((IEnumerable<AbilityData>) this.mUnitData.BattleAbilitys);
      for (int index1 = 0; index1 < this.mAbilityChangeLists.Count; ++index1)
      {
        Unit.AbilityChange abilityChangeList = this.mAbilityChangeLists[index1];
        if (abilityChangeList != null)
        {
          AbilityParam fromAp = abilityChangeList.GetFromAp();
          AbilityParam toAp = abilityChangeList.GetToAp();
          if (fromAp != null && toAp != null)
          {
            AbilityData abilityData = (AbilityData) null;
            for (int index2 = this.mAddedAbilitys.Count - 1; index2 >= 0; --index2)
            {
              if (this.mAddedAbilitys[index2].AbilityID == toAp.iname)
              {
                abilityData = this.mAddedAbilitys[index2];
                break;
              }
            }
            if (abilityData != null)
            {
              for (int index3 = this.mBattleAbilitys.Count - 1; index3 >= 0; --index3)
              {
                if (this.mBattleAbilitys[index3].AbilityID == fromAp.iname)
                {
                  this.mBattleAbilitys[index3] = abilityData;
                  break;
                }
              }
            }
          }
        }
      }
      if (this.mBattleSkills == null)
        this.mBattleSkills = new List<SkillData>();
      this.mBattleSkills.Clear();
      for (int index4 = 0; index4 < this.mBattleAbilitys.Count; ++index4)
      {
        if (this.mBattleAbilitys[index4].Skills != null)
        {
          for (int index5 = 0; index5 < this.mBattleAbilitys[index4].Skills.Count; ++index5)
          {
            SkillData skill = this.mBattleAbilitys[index4].Skills[index5];
            if (skill != null && !this.mBattleSkills.Contains(skill))
              this.mBattleSkills.Add(skill);
          }
        }
      }
    }

    public bool CreateAddedAbilityAndSkills(AbilityParam ap, int ab_exp, bool is_none_category)
    {
      if (ap == null)
        return false;
      for (int index = 0; index < this.mUnitData.BattleAbilitys.Count; ++index)
      {
        if (this.mUnitData.BattleAbilitys[index].Param == ap)
          return false;
      }
      for (int index = 0; index < this.mAddedAbilitys.Count; ++index)
      {
        if (this.mAddedAbilitys[index].Param == ap)
          return false;
      }
      AbilityData ad = new AbilityData();
      ad.Setup(this.mUnitData, this.GetFreeAbilityIID(), ap.iname, Math.Max(ab_exp, 0));
      this.mAddedAbilitys.Add(ad);
      List<SkillData> skillDataList = new List<SkillData>((IEnumerable<SkillData>) this.mUnitData.BattleSkills);
      skillDataList.AddRange((IEnumerable<SkillData>) this.mAddedSkills);
      ad.UpdateLearningsSkill(true, skillDataList);
      ad.IsNoneCategory = is_none_category;
      List<SkillData> skill_list = new List<SkillData>();
      skill_list.AddRange((IEnumerable<SkillData>) ad.Skills);
      this.UnitData.RefrectionSkillAndAbility(new List<AbilityData>(), skill_list);
      ad.ReplaceSkills(skillDataList);
      bool flag = false;
      for (int index = 0; index < ad.Skills.Count; ++index)
      {
        SkillData skill = ad.Skills[index];
        if (skill != null && !this.mAddedSkills.Contains(skill))
        {
          this.mAddedSkills.Add(skill);
          flag = true;
        }
      }
      if (flag)
        this.UpdateAdditionalSkills();
      this.AddSkillUseCount(ad);
      return true;
    }

    private long GetFreeAbilityIID()
    {
      long freeAbilityIid = 1;
      List<AbilityData> abilityDataList = new List<AbilityData>();
      abilityDataList.AddRange((IEnumerable<AbilityData>) this.mUnitData.BattleAbilitys);
      abilityDataList.AddRange((IEnumerable<AbilityData>) this.mAddedAbilitys);
      while (freeAbilityIid < long.MaxValue)
      {
        bool flag = false;
        for (int index = 0; index < abilityDataList.Count; ++index)
        {
          if (abilityDataList[index].UniqueID == freeAbilityIid)
          {
            ++freeAbilityIid;
            flag = true;
            break;
          }
        }
        if (!flag)
          break;
      }
      return freeAbilityIid;
    }

    public void UpdateAbilityChange()
    {
      bool flag1 = false;
      for (int index1 = this.mAbilityChangeLists.Count - 1; index1 >= 0; --index1)
      {
        Unit.AbilityChange abilityChangeList = this.mAbilityChangeLists[index1];
        for (int index2 = abilityChangeList.mDataLists.Count - 1; index2 >= 0; --index2)
        {
          Unit.AbilityChange.Data mDataList = abilityChangeList.mDataLists[index2];
          if (!mDataList.mIsInfinite && mDataList.mTurn != 0)
            --mDataList.mTurn;
        }
        bool flag2 = false;
        for (int index3 = abilityChangeList.mDataLists.Count - 1; index3 >= 0; --index3)
        {
          Unit.AbilityChange.Data mDataList = abilityChangeList.mDataLists[index3];
          if (!mDataList.mIsInfinite && mDataList.mTurn == 0)
          {
            flag2 |= mDataList.mIsReset;
            abilityChangeList.mDataLists.RemoveAt(index3);
            flag1 = true;
          }
          else
            break;
        }
        if (abilityChangeList.mDataLists.Count == 0 || flag2)
        {
          abilityChangeList.Clear();
          this.mAbilityChangeLists.RemoveAt(index1);
        }
      }
      if (!flag1)
        return;
      this.RefreshBattleAbilitysAndSkills();
    }

    public void DtuCopyTo(Unit from_unit, bool is_cancel)
    {
      if (from_unit == null)
        return;
      this.mActionCount = (OInt) from_unit.ActionCount;
      this.mDeathCount = (OInt) from_unit.DeathCount;
      this.mAutoJewel = (OInt) from_unit.AutoJewel;
      this.mRageTarget = from_unit.RageTarget;
      this.mKillCount = from_unit.KillCount;
      if (!is_cancel && this.mDtuParam != null && this.mDtuParam.IsInheritSkin && this.mUnitData.CurrentJob != null && from_unit.mUnitData.CurrentJob != null)
      {
        this.mUnitData.CurrentJob.SelectedSkin = from_unit.mUnitData.CurrentJob.SelectedSkin;
        this.mUnitData.CurrentJob.SelectSkinData = from_unit.mUnitData.CurrentJob.SelectSkinData;
      }
      this.mShields = from_unit.mShields;
      this.mMhmDamageLists = from_unit.mMhmDamageLists;
      this.mFtgtTargetList = new List<Unit.UnitForcedTargeting>((IEnumerable<Unit.UnitForcedTargeting>) from_unit.mFtgtTargetList);
      this.mFtgtFromList = new List<Unit.UnitForcedTargeting>((IEnumerable<Unit.UnitForcedTargeting>) from_unit.mFtgtFromList);
      foreach (Unit.UnitForcedTargeting mFtgtTarget in this.mFtgtTargetList)
      {
        if (mFtgtTarget.mUnit != null)
        {
          foreach (Unit.UnitForcedTargeting mFtgtFrom in mFtgtTarget.mUnit.mFtgtFromList)
          {
            if (mFtgtFrom.mUnit == from_unit)
              mFtgtFrom.mUnit = this;
          }
        }
      }
      foreach (Unit.UnitForcedTargeting mFtgtFrom in this.mFtgtFromList)
      {
        if (mFtgtFrom.mUnit != null)
        {
          foreach (Unit.UnitForcedTargeting mFtgtTarget in mFtgtFrom.mUnit.mFtgtTargetList)
          {
            if (mFtgtTarget.mUnit == from_unit)
              mFtgtTarget.mUnit = this;
          }
        }
      }
    }

    public void DtuSkillUseCountCopyTo(Unit from_unit)
    {
      if (from_unit == null)
        return;
      foreach (SkillData key in new List<SkillData>((IEnumerable<SkillData>) this.mSkillUseCount.Keys))
      {
        foreach (KeyValuePair<SkillData, OInt> keyValuePair in from_unit.mSkillUseCount)
        {
          if (key.SkillID == keyValuePair.Key.SkillID)
            this.mSkillUseCount[key] = keyValuePair.Value;
        }
      }
    }

    public void EntryInspIns(long aiid, int slot_no, bool valid = false, int check_ctr = -1)
    {
      Unit.UnitInsp insp = this.CreateInsp(aiid, slot_no, valid, check_ctr);
      if (insp == null)
        return;
      if ((int) insp.mCheckCtr < 0)
      {
        insp.mCheckCtr = (OInt) 1;
        InspSkillParam inspSkillParam = (InspSkillParam) null;
        InspirationSkillData inspirationSkillDataBySlot = insp.mArtifact.GetInspirationSkillDataBySlot(slot_no);
        if (inspirationSkillDataBySlot != null)
          inspSkillParam = inspirationSkillDataBySlot.InspSkillParam;
        if (inspSkillParam == null && insp.mArtifact.ArtifactParam != null)
          inspSkillParam = MonoSingleton<GameManager>.Instance.MasterParam.GetInspirationSkillParam(insp.mArtifact.ArtifactParam.insp_skill);
        if (inspSkillParam != null)
          insp.mCheckCtr = (OInt) inspSkillParam.GetCheckNum();
      }
      this.mInspInsList.Add(insp);
    }

    public Unit.UnitInsp CreateInsp(long aiid, int slot_no = 0, bool valid = false, int check_ctr = 1)
    {
      if (this.UnitData == null || this.UnitData.CurrentJob == null || this.UnitData.CurrentJob.ArtifactDatas == null)
        return (Unit.UnitInsp) null;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return (Unit.UnitInsp) null;
      ArtifactData artifactByUniqueId = instance.Player.FindArtifactByUniqueID(aiid);
      return artifactByUniqueId == null ? (Unit.UnitInsp) null : new Unit.UnitInsp(artifactByUniqueId, slot_no, valid, check_ctr);
    }

    public bool GetInspSlotNo(ArtifactData artifact, out int slot_no)
    {
      slot_no = 0;
      if (artifact != null)
      {
        for (int index = 0; index < this.mInspInsList.Count; ++index)
        {
          Unit.UnitInsp mInspIns = this.mInspInsList[index];
          if ((long) mInspIns.mArtifact.UniqueID == (long) artifact.UniqueID)
          {
            slot_no = (int) mInspIns.mSlotNo;
            return true;
          }
        }
      }
      return false;
    }

    public bool ApplyInspIns(ArtifactData artifact)
    {
      if (artifact != null)
      {
        for (int index = 0; index < this.mInspInsList.Count; ++index)
        {
          Unit.UnitInsp mInspIns = this.mInspInsList[index];
          if ((long) mInspIns.mArtifact.UniqueID == (long) artifact.UniqueID && !(bool) mInspIns.mValid)
          {
            if ((int) mInspIns.mCheckCtr != 0)
              --mInspIns.mCheckCtr;
            if ((int) mInspIns.mCheckCtr > 0)
              return false;
            mInspIns.mValid = (OBool) true;
            return true;
          }
        }
      }
      return false;
    }

    public bool EntryInspUse(long aiid)
    {
      if (!this.IsPartyMember || this.IsUnitFlag(EUnitFlag.IsHelp) || this.mInspUseList.Find((Predicate<Unit.UnitInsp>) (d => (long) d.mArtifact.UniqueID == aiid)) != null)
        return false;
      Unit.UnitInsp insp = this.CreateInsp(aiid);
      if (insp == null)
        return false;
      this.mInspUseList.Add(insp);
      return true;
    }

    public bool IsFtgtTargetValid()
    {
      bool flag = false;
      foreach (Unit.UnitForcedTargeting mFtgtTarget in this.mFtgtTargetList)
      {
        if (mFtgtTarget.mUnit != null && mFtgtTarget.mUnit.FtgtFromActiveUnit == this)
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    public bool IsFtgtFromValid() => this.FtgtFromActive != null;

    private bool UpdateAdditionalSkills()
    {
      if (this.mUnitData == null)
        return false;
      bool flag = false;
      List<SkillData> skillDataList = new List<SkillData>((IEnumerable<SkillData>) this.mUnitData.BattleSkills);
      skillDataList.AddRange((IEnumerable<SkillData>) this.mAddedSkills);
      foreach (SkillData skillData1 in skillDataList)
      {
        SkillData base_skill = skillData1;
        if (base_skill.SkillAdditional != null && this.mAdditionalSkills.Find((Predicate<SkillData>) (p => p.SkillID == base_skill.SkillAdditional.SkillId)) == null)
        {
          SkillData skillData2 = new SkillData();
          skillData2.Setup(base_skill.SkillAdditional.SkillId, base_skill.Rank, base_skill.GetRankCap());
          this.mAdditionalSkills.Add(skillData2);
          flag = true;
        }
      }
      return flag;
    }

    public SkillData FindAdditionalSkills(string skill_iname)
    {
      return this.mAdditionalSkills.Find((Predicate<SkillData>) (p => p.SkillID == skill_iname));
    }

    public long BossLongBaseMaxHP => this.mBossLongBaseMaxHP;

    public long BossLongMaxHP => this.mBossLongMaxHP;

    public long BossLongCurHP => this.mBossLongCurHP;

    private void InternalReflectBossCurrentHP()
    {
      this.CurrentStatus.param.hp = this.mBossLongCurHP <= 1000000000L ? (OInt) (int) this.mBossLongCurHP : (OInt) 1000000000;
      if (this.mBossLongMaxHP > 1000000000L)
        this.MaximumStatus.param.hp = (OInt) 1000000000;
      else
        this.MaximumStatus.param.hp = (OInt) (int) this.mBossLongMaxHP;
    }

    public bool IsUseBossLongHP() => this.IsRaidBoss && this.mBossLongBaseMaxHP != 0L;

    public void AddBossCurrentHP(int val)
    {
      if (val == 0 || !this.IsUseBossLongHP())
        return;
      this.mBossLongCurHP = Math.Min(Math.Max(this.mBossLongCurHP + (long) val, 0L), this.mBossLongMaxHP);
      this.InternalReflectBossCurrentHP();
    }

    public void ReflectBossCurrentHP(bool is_reset = false)
    {
      if (!this.IsUseBossLongHP())
        return;
      this.InternalReflectBossCurrentHP();
      if (!is_reset)
        return;
      this.mBossLongMaxHP = this.mBossLongBaseMaxHP;
    }

    public void SetBossMaxHP(long max_hp)
    {
      this.mBossLongCurHP = this.mBossLongMaxHP = this.mBossLongBaseMaxHP = max_hp;
    }

    public void SetBossCurrentHP(long hp)
    {
      this.mBossLongCurHP = Math.Min(Math.Max(hp, 0L), this.mBossLongBaseMaxHP);
    }

    public long GetCurrentHP()
    {
      return this.IsUseBossLongHP() ? this.mBossLongCurHP : (long) (int) this.CurrentStatus.param.hp;
    }

    public long GetMaximumHP()
    {
      return this.IsUseBossLongHP() ? this.mBossLongMaxHP : (long) (int) this.MaximumStatus.param.hp;
    }

    public long GetBaseHP()
    {
      return this.IsUseBossLongHP() ? this.mBossLongBaseMaxHP : (long) (int) this.UnitData.Status.param.hp;
    }

    public void CalcCurrentStatus(bool is_initialized = false, bool is_predict = false)
    {
      int hp = (int) this.CurrentStatus.param.hp;
      int mp = (int) this.CurrentStatus.param.mp;
      Unit.BuffWorkStatus.Clear();
      Unit.BuffWorkScaleStatus.Clear();
      Unit.DebuffWorkScaleStatus.Clear();
      Unit.PassiveWorkScaleStatus.Clear();
      Unit.BuffDupliScaleStatus.Clear();
      Unit.DebuffDupliScaleStatus.Clear();
      Unit.BuffConceptCardStatus.Clear();
      Unit.DebuffConceptCardStatus.Clear();
      Unit.BuffConceptCardScaleStatus.Clear();
      Unit.DebuffConceptCardScaleStatus.Clear();
      Unit.DupliConceptCardStatus.Clear();
      this.mAutoJewel = (OInt) 0;
      this.mMaximumStatusWithMap.CopyTo(this.MaximumStatus);
      int enemy_dead_count = 0;
      SceneBattle instance = SceneBattle.Instance;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) && instance.Battle != null)
        enemy_dead_count = instance.Battle.GetDeadCountEnemy();
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
        this.BuffAttachments[index].IsCalculated = false;
      bool flag1 = false;
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        if (!buffAttachment.IsCalcLaterCondition)
          flag1 |= this.CalcBuffStatus(buffAttachment, enemy_dead_count);
      }
      BaseStatus src = new BaseStatus();
      src.Add(Unit.BuffWorkScaleStatus);
      src.Add(Unit.DebuffWorkScaleStatus);
      src.Add(Unit.PassiveWorkScaleStatus);
      if (flag1)
      {
        BaseStatus baseStatus1 = new BaseStatus();
        BaseStatus baseStatus2 = new BaseStatus();
        BaseStatus baseStatus3 = new BaseStatus();
        BaseStatus baseStatus4 = new BaseStatus();
        Unit.BuffConceptCardStatus.CopyTo(baseStatus1);
        Unit.DebuffConceptCardStatus.CopyTo(baseStatus2);
        Unit.BuffConceptCardScaleStatus.CopyTo(baseStatus3);
        Unit.DebuffConceptCardScaleStatus.CopyTo(baseStatus4);
        baseStatus1.ChoiceHighest(baseStatus3, this.MaximumStatus);
        baseStatus2.ChoiceLowest(baseStatus4, this.MaximumStatus);
        src.Add(baseStatus3);
        src.Add(baseStatus4);
        this.MaximumStatus.Add(baseStatus1);
        this.MaximumStatus.Add(baseStatus2);
      }
      this.MaximumStatus.Add(Unit.BuffWorkStatus);
      this.MaximumStatus.AddRate(src);
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        if (buffAttachment.IsCalcLaterCondition)
          flag1 |= this.CalcBuffStatus(buffAttachment, enemy_dead_count);
      }
      this.mMaximumStatusWithMap.CopyTo(this.MaximumStatus);
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
        this.BuffAttachments[index].IsCalculated = false;
      if (this.IsUnitCondition(EUnitCondition.Blindness))
      {
        FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
        int num1 = 0;
        int num2 = 0;
        bool flag2 = false;
        for (int index = 0; index < this.CondAttachments.Count; ++index)
        {
          CondAttachment condAttachment = this.CondAttachments[index];
          if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.Blindness))
          {
            int num3 = 0;
            int num4 = 0;
            if (condAttachment.skill != null)
            {
              CondEffect condEffect1 = condAttachment.skill.GetCondEffect();
              if (condEffect1 != null)
              {
                int num5 = (int) ((int) condEffect1.param.v_blink_hit == 0 ? fixParam.BlindnessHitRate : condEffect1.param.v_blink_hit);
                int num6 = (int) ((int) condEffect1.param.v_blink_avo == 0 ? fixParam.BlindnessAvoidRate : condEffect1.param.v_blink_avo);
                if (Math.Abs(num3) + Math.Abs(num4) < Math.Abs(num5) + Math.Abs(num6))
                {
                  num3 = num5;
                  num4 = num6;
                }
                flag2 = true;
              }
              CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
              if (condEffect2 != null)
              {
                int num7 = (int) ((int) condEffect2.param.v_blink_hit == 0 ? fixParam.BlindnessHitRate : condEffect2.param.v_blink_hit);
                int num8 = (int) ((int) condEffect2.param.v_blink_avo == 0 ? fixParam.BlindnessAvoidRate : condEffect2.param.v_blink_avo);
                if (Math.Abs(num3) + Math.Abs(num4) < Math.Abs(num7) + Math.Abs(num8))
                {
                  num3 = num7;
                  num4 = num8;
                }
                flag2 = true;
              }
            }
            else if (condAttachment.Param != null)
            {
              int num9 = (int) ((int) condAttachment.Param.v_blink_hit == 0 ? fixParam.BlindnessHitRate : condAttachment.Param.v_blink_hit);
              int num10 = (int) ((int) condAttachment.Param.v_blink_avo == 0 ? fixParam.BlindnessAvoidRate : condAttachment.Param.v_blink_avo);
              if (Math.Abs(num3) + Math.Abs(num4) < Math.Abs(num9) + Math.Abs(num10))
              {
                num3 = num9;
                num4 = num10;
              }
              flag2 = true;
            }
            else
            {
              if (Math.Abs(num3) + Math.Abs(num4) < Math.Abs((int) fixParam.BlindnessHitRate) + Math.Abs((int) fixParam.BlindnessAvoidRate))
              {
                num3 = (int) fixParam.BlindnessHitRate;
                num4 = (int) fixParam.BlindnessAvoidRate;
              }
              flag2 = true;
            }
            if (Math.Abs(num1) + Math.Abs(num2) < Math.Abs(num3) + Math.Abs(num4))
            {
              num1 = num3;
              num2 = num4;
            }
          }
        }
        if (!flag2)
        {
          num1 = (int) fixParam.BlindnessHitRate;
          num2 = (int) fixParam.BlindnessAvoidRate;
        }
        BaseStatus buffWorkStatus1;
        (buffWorkStatus1 = Unit.BuffWorkStatus)[BattleBonus.HitRate] = (OInt) ((int) buffWorkStatus1[BattleBonus.HitRate] + num1);
        BaseStatus buffWorkStatus2;
        (buffWorkStatus2 = Unit.BuffWorkStatus)[BattleBonus.AvoidRate] = (OInt) ((int) buffWorkStatus2[BattleBonus.AvoidRate] + num2);
      }
      if (this.IsUnitCondition(EUnitCondition.Berserk))
      {
        FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
        int num11 = 0;
        int num12 = 0;
        bool flag3 = false;
        for (int index = 0; index < this.CondAttachments.Count; ++index)
        {
          CondAttachment condAttachment = this.CondAttachments[index];
          if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.Berserk))
          {
            int num13 = 0;
            int num14 = 0;
            if (condAttachment.skill != null)
            {
              CondEffect condEffect3 = condAttachment.skill.GetCondEffect();
              if (condEffect3 != null)
              {
                int num15 = (int) ((int) condEffect3.param.v_berserk_atk == 0 ? fixParam.BerserkAtkRate : condEffect3.param.v_berserk_atk);
                int num16 = (int) ((int) condEffect3.param.v_berserk_def == 0 ? fixParam.BerserkDefRate : condEffect3.param.v_berserk_def);
                if (Math.Abs(num13) + Math.Abs(num14) < Math.Abs(num15) + Math.Abs(num16))
                {
                  num13 = num15;
                  num14 = num16;
                }
              }
              CondEffect condEffect4 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
              if (condEffect4 != null)
              {
                int num17 = (int) ((int) condEffect4.param.v_berserk_atk == 0 ? fixParam.BerserkAtkRate : condEffect4.param.v_berserk_atk);
                int num18 = (int) ((int) condEffect4.param.v_berserk_def == 0 ? fixParam.BerserkDefRate : condEffect4.param.v_berserk_def);
                if (Math.Abs(num13) + Math.Abs(num14) < Math.Abs(num17) + Math.Abs(num18))
                {
                  num13 = num17;
                  num14 = num18;
                }
              }
            }
            else if (condAttachment.Param != null)
            {
              int num19 = (int) ((int) condAttachment.Param.v_berserk_atk == 0 ? fixParam.BerserkAtkRate : condAttachment.Param.v_berserk_atk);
              int num20 = (int) ((int) condAttachment.Param.v_berserk_def == 0 ? fixParam.BerserkDefRate : condAttachment.Param.v_berserk_def);
              if (Math.Abs(num13) + Math.Abs(num14) < Math.Abs(num19) + Math.Abs(num20))
              {
                num13 = num19;
                num14 = num20;
              }
            }
            else if (Math.Abs(num13) + Math.Abs(num14) < Math.Abs((int) fixParam.BerserkAtkRate) + Math.Abs((int) fixParam.BerserkDefRate))
            {
              num13 = (int) fixParam.BerserkAtkRate;
              num14 = (int) fixParam.BerserkDefRate;
            }
            if (Math.Abs(num11) + Math.Abs(num12) < Math.Abs(num13) + Math.Abs(num14))
            {
              num11 = num13;
              num12 = num14;
            }
            flag3 = true;
          }
        }
        if (!flag3)
        {
          num11 = (int) fixParam.BerserkAtkRate;
          num12 = (int) fixParam.BerserkDefRate;
        }
        BaseStatus passiveWorkScaleStatus1;
        (passiveWorkScaleStatus1 = Unit.PassiveWorkScaleStatus)[StatusTypes.Atk] = (OInt) ((int) passiveWorkScaleStatus1[StatusTypes.Atk] + num11);
        BaseStatus passiveWorkScaleStatus2;
        (passiveWorkScaleStatus2 = Unit.PassiveWorkScaleStatus)[StatusTypes.Mag] = (OInt) ((int) passiveWorkScaleStatus2[StatusTypes.Mag] + num11);
        BaseStatus passiveWorkScaleStatus3;
        (passiveWorkScaleStatus3 = Unit.PassiveWorkScaleStatus)[StatusTypes.Def] = (OInt) ((int) passiveWorkScaleStatus3[StatusTypes.Def] + num12);
        BaseStatus passiveWorkScaleStatus4;
        (passiveWorkScaleStatus4 = Unit.PassiveWorkScaleStatus)[StatusTypes.Mnd] = (OInt) ((int) passiveWorkScaleStatus4[StatusTypes.Mnd] + num12);
      }
      Unit.BuffWorkScaleStatus.Add(Unit.DebuffWorkScaleStatus);
      Unit.BuffWorkScaleStatus.Add(Unit.PassiveWorkScaleStatus);
      if (flag1)
      {
        Unit.BuffConceptCardStatus.ChoiceHighest(Unit.BuffConceptCardScaleStatus, this.MaximumStatus);
        Unit.DebuffConceptCardStatus.ChoiceLowest(Unit.DebuffConceptCardScaleStatus, this.MaximumStatus);
        Unit.BuffWorkScaleStatus.Add(Unit.BuffConceptCardScaleStatus);
        Unit.BuffWorkScaleStatus.Add(Unit.DebuffConceptCardScaleStatus);
        this.MaximumStatus.Add(Unit.BuffConceptCardStatus);
        this.MaximumStatus.Add(Unit.DebuffConceptCardStatus);
      }
      this.MaximumStatus.Add(Unit.BuffWorkStatus);
      this.MaximumStatus.AddRate(Unit.BuffWorkScaleStatus);
      this.MaximumStatus.param.ApplyMinVal();
      this.MaximumStatus.CopyTo(this.CurrentStatus);
      this.mAutoJewel = this.CurrentStatus[BattleBonus.AutoJewel];
      if (is_initialized)
        this.CurrentStatus.param.mp = (OShort) this.GetStartGems();
      else if (is_predict)
      {
        this.CurrentStatus.param.hp = (OInt) hp;
        this.CurrentStatus.param.mp = (OShort) mp;
      }
      else
      {
        this.CurrentStatus.param.hp = (OInt) Math.Min(hp, (int) this.MaximumStatus.param.hp);
        this.CurrentStatus.param.mp = (OShort) Math.Min(mp, (int) this.MaximumStatus.param.mp);
      }
      this.ReflectBossCurrentHP(true);
      this.mMaximumStatusHp = (int) this.mMaximumStatus.param.hp;
      this.ReflectMhmDamage();
      for (int index = 0; index < this.mShields.Count; ++index)
      {
        if (!this.mShields[index].skill_param.IsShieldReset() && (int) this.mShields[index].hp == 0)
          this.mShields.RemoveAt(index--);
      }
      if (this.mRageTarget != null && this.mRageTarget.IsDeadCondition())
        this.CureCondEffects(EUnitCondition.Rage, forced: true);
      for (int index = 0; index < this.mProtects.Count; ++index)
      {
        if ((int) this.mProtects[index].value <= 0)
          this.CancelProtect(this.mProtects[index--]);
      }
      this.UpdateAllSkillUseCount(0);
    }

    private bool CalcBuffStatus(BuffAttachment buff, int enemy_dead_count)
    {
      if (buff == null || buff.IsCalculated || buff.CheckTiming != EffectCheckTimings.Moment && buff.Param != null && buff.Param.mEffRange == EEffRange.NearAllyOnly && this == buff.user)
        return false;
      SkillData skill = buff.skill;
      List<BaseStatus> attachmentStatuses = this.GetBuffAttachmentStatuses(buff);
      if (attachmentStatuses.Count == 0)
        attachmentStatuses.Add(buff.status);
      BaseStatus[] baseStatusArray = new BaseStatus[attachmentStatuses.Count];
      if (this.IsIncludeBuffAttachmentResistStatusBuff(buff))
      {
        for (int index = 0; index < baseStatusArray.Length; ++index)
          baseStatusArray[index] = new BaseStatus(attachmentStatuses[index]);
      }
      else
      {
        for (int index = 0; index < baseStatusArray.Length; ++index)
          baseStatusArray[index] = (BaseStatus) null;
        baseStatusArray[0] = new BaseStatus(buff.status);
      }
      if (!(bool) buff.IsPassive && (skill == null || !skill.IsPassiveSkill()) && (buff.Param == null || !buff.Param.IsNoDisabled) && !this.IsEnableBuffEffect(buff.BuffType))
        return false;
      if (buff.UseCondition != ESkillCondition.None)
      {
        switch (buff.UseCondition)
        {
          case ESkillCondition.Dying:
            if (!this.IsDying())
              return false;
            break;
          case ESkillCondition.JudgeHP:
            if (buff.skill == null || !buff.skill.IsJudgeHp(this))
              return false;
            break;
        }
      }
      if (buff.Param != null)
      {
        int num = 0;
        if (buff.Param.mAppType == EAppType.AllKillCount)
        {
          if (enemy_dead_count == 0)
            return false;
          num = Math.Min(enemy_dead_count, buff.Param.mAppMct) - 1;
        }
        else if (buff.Param.mAppType == EAppType.SelfKillCount)
        {
          if (this.mKillCount == 0)
            return false;
          num = Math.Min(this.mKillCount, buff.Param.mAppMct) - 1;
        }
        if ((buff.Param.mEffRange == EEffRange.SelfNearAlly || buff.Param.mEffRange == EEffRange.NearAllyOnly) && buff.Param.mAppMct > 0)
        {
          int allyUnitNum = this.GetAllyUnitNum(buff.user);
          if (allyUnitNum == 0)
            return false;
          num += Math.Min(allyUnitNum, buff.Param.mAppMct) - 1;
        }
        if (num > 0)
        {
          for (int index1 = 0; index1 < baseStatusArray.Length; ++index1)
          {
            if (baseStatusArray[index1] != null)
            {
              for (int index2 = 0; index2 < num; ++index2)
                baseStatusArray[index1].Add(attachmentStatuses[index1]);
            }
          }
        }
      }
      for (int index = 1; index < baseStatusArray.Length; ++index)
      {
        if (baseStatusArray[index] == null)
          baseStatusArray[index] = baseStatusArray[0];
      }
      bool flag1 = skill != null && skill.Condition == ESkillCondition.CardSkill;
      bool flag2 = buff.BuffType == BuffTypes.Buff;
      if (flag1 && buff.IsNegativeValueIsBuff)
        flag2 = !flag2;
      int num1 = baseStatusArray.Length;
      if (flag1 && buff.skill != null && buff.skill.IsPassiveSkill())
        num1 = 1;
      if (num1 > 1)
      {
        switch (buff.CalcType)
        {
          case SkillParamCalcTypes.Add:
          case SkillParamCalcTypes.Fixed:
            if (flag1)
            {
              Unit.DupliConceptCardStatus.Clear();
              for (int index = 0; index < num1; ++index)
                Unit.DupliConceptCardStatus.Add(baseStatusArray[index]);
              if (flag2)
              {
                Unit.BuffConceptCardStatus.ReplaceHighest(Unit.DupliConceptCardStatus);
                break;
              }
              Unit.DebuffConceptCardStatus.ReplaceLowest(Unit.DupliConceptCardStatus);
              break;
            }
            for (int index = 0; index < num1; ++index)
              Unit.BuffWorkStatus.Add(baseStatusArray[index]);
            break;
          case SkillParamCalcTypes.Scale:
            if (buff.skill != null && buff.skill.IsPassiveSkill())
            {
              if (flag1)
              {
                Unit.DupliConceptCardStatus.Clear();
                for (int index = 0; index < num1; ++index)
                  Unit.DupliConceptCardStatus.Add(baseStatusArray[index]);
                if (flag2)
                {
                  Unit.BuffConceptCardScaleStatus.ReplaceHighest(Unit.DupliConceptCardStatus);
                  break;
                }
                Unit.DebuffConceptCardScaleStatus.ReplaceLowest(Unit.DupliConceptCardStatus);
                break;
              }
              for (int index = 0; index < num1; ++index)
                Unit.PassiveWorkScaleStatus.Add(baseStatusArray[index]);
              break;
            }
            if (flag2)
            {
              Unit.BuffDupliScaleStatus.Clear();
              for (int index = 0; index < num1; ++index)
                Unit.BuffDupliScaleStatus.Add(baseStatusArray[index]);
              if (flag1)
              {
                Unit.BuffConceptCardScaleStatus.ReplaceHighest(Unit.BuffDupliScaleStatus);
                break;
              }
              Unit.BuffWorkScaleStatus.ReplaceHighest(Unit.BuffDupliScaleStatus);
              break;
            }
            Unit.DebuffDupliScaleStatus.Clear();
            for (int index = 0; index < num1; ++index)
              Unit.DebuffDupliScaleStatus.Add(baseStatusArray[index]);
            if (flag1)
            {
              Unit.DebuffConceptCardScaleStatus.ReplaceLowest(Unit.DebuffDupliScaleStatus);
              break;
            }
            Unit.DebuffWorkScaleStatus.ReplaceLowest(Unit.DebuffDupliScaleStatus);
            break;
        }
        for (int index = 0; index < this.BuffAttachments.Count; ++index)
        {
          BuffAttachment buffAttachment = this.BuffAttachments[index];
          if (this.isSameBuffAttachment(buffAttachment, buff))
            buffAttachment.IsCalculated = true;
        }
      }
      else
      {
        BaseStatus baseStatus = baseStatusArray[0];
        switch (buff.CalcType)
        {
          case SkillParamCalcTypes.Add:
            if (flag1)
            {
              if (flag2)
              {
                Unit.BuffConceptCardStatus.ReplaceHighest(baseStatus);
                break;
              }
              Unit.DebuffConceptCardStatus.ReplaceLowest(baseStatus);
              break;
            }
            Unit.BuffWorkStatus.Add(baseStatus);
            break;
          case SkillParamCalcTypes.Scale:
            if (buff.skill != null && buff.skill.IsPassiveSkill())
            {
              if (flag1)
              {
                if (flag2)
                {
                  Unit.BuffConceptCardScaleStatus.ReplaceHighest(baseStatus);
                  break;
                }
                Unit.DebuffConceptCardScaleStatus.ReplaceLowest(baseStatus);
                break;
              }
              Unit.PassiveWorkScaleStatus.Add(baseStatus);
              break;
            }
            if (flag2)
            {
              if (flag1)
              {
                Unit.BuffConceptCardScaleStatus.ReplaceHighest(baseStatus);
                break;
              }
              Unit.BuffWorkScaleStatus.ReplaceHighest(baseStatus);
              break;
            }
            if (flag1)
            {
              Unit.DebuffConceptCardScaleStatus.ReplaceLowest(baseStatus);
              break;
            }
            Unit.DebuffWorkScaleStatus.ReplaceLowest(baseStatus);
            break;
        }
      }
      return flag1;
    }

    [MessagePackObject(true)]
    public class DropItem
    {
      public ItemParam itemParam;
      public ConceptCardParam conceptCardParam;
      public OInt num;
      public OBool is_secret;

      public bool isItem => this.itemParam != null;

      public bool isConceptCard => this.conceptCardParam != null;

      public string Iname
      {
        get
        {
          if (this.isItem)
            return this.itemParam.iname;
          return this.isConceptCard ? this.conceptCardParam.iname : string.Empty;
        }
      }

      public EBattleRewardType BattleRewardType
      {
        get
        {
          if (this.isItem)
            return EBattleRewardType.Item;
          return this.isConceptCard ? EBattleRewardType.ConceptCard : EBattleRewardType.None;
        }
      }

      public static bool IsNullOrEmpty(Unit.DropItem value)
      {
        return value == null || (int) value.num == 0 || value.itemParam == null && value.conceptCardParam == null;
      }
    }

    [MessagePackObject(true)]
    public class UnitDrop
    {
      public List<Unit.DropItem> items = new List<Unit.DropItem>();
      public OInt exp;
      public OInt gems;
      public OInt gold;
      public bool gained;

      public bool IsEnableDrop()
      {
        if (this.gained)
          return false;
        return (int) this.exp > 0 || (int) this.gems > 0 || (int) this.gold > 0 || this.items.Count > 0;
      }

      public void Drop() => this.gained = true;

      public void CopyTo(Unit.UnitDrop other)
      {
        other.exp = this.exp;
        other.gems = this.gems;
        other.gold = this.gold;
        other.gained = this.gained;
        other.items.Clear();
        for (int index = 0; index < this.items.Count; ++index)
        {
          if (!Unit.DropItem.IsNullOrEmpty(this.items[index]))
            other.items.Add(new Unit.DropItem()
            {
              itemParam = this.items[index].itemParam,
              conceptCardParam = this.items[index].conceptCardParam,
              num = this.items[index].num
            });
        }
      }
    }

    [MessagePackObject(true)]
    public class UnitSteal
    {
      public List<Unit.DropItem> items = new List<Unit.DropItem>();
      public OInt gold;
      public bool is_item_steeled;
      public bool is_gold_steeled;

      public bool IsEnableItemSteal() => !this.is_item_steeled && this.items.Count > 0;

      public bool IsEnableGoldSteal() => !this.is_gold_steeled;

      public void CopyTo(Unit.UnitSteal other)
      {
        other.gold = this.gold;
        other.is_item_steeled = this.is_item_steeled;
        other.is_gold_steeled = this.is_gold_steeled;
        other.items.Clear();
        for (int index = 0; index < this.items.Count; ++index)
        {
          if (!Unit.DropItem.IsNullOrEmpty(this.items[index]))
            other.items.Add(new Unit.DropItem()
            {
              itemParam = this.items[index].itemParam,
              conceptCardParam = this.items[index].conceptCardParam,
              num = this.items[index].num
            });
        }
      }
    }

    [MessagePackObject(true)]
    public class UnitShield
    {
      public ShieldTypes shieldType;
      public DamageTypes damageType;
      public OInt hp;
      public OInt hpMax;
      public OInt turn;
      public OInt turnMax;
      public OInt damage_rate;
      public OInt damage_value;
      public SkillParam skill_param;
      public bool is_efficacy;
      public bool is_ignore_efficacy;
    }

    public enum eTypeMhmDamage
    {
      HP,
      MP,
    }

    [MessagePackObject(true)]
    public class UnitMhmDamage
    {
      public Unit.eTypeMhmDamage mType;
      public OInt mDamage;

      public UnitMhmDamage()
      {
      }

      public UnitMhmDamage(Unit.eTypeMhmDamage type, int damage)
      {
        this.mType = type;
        this.mDamage = (OInt) damage;
      }

      public void CopyTo(Unit.UnitMhmDamage other)
      {
        other.mType = this.mType;
        other.mDamage = this.mDamage;
      }
    }

    [MessagePackObject(false)]
    public class UnitInsp
    {
      [Key(0)]
      public ArtifactData mArtifact;
      [Key(1)]
      public OInt mSlotNo;
      [Key(2)]
      public OBool mValid;
      [Key(3)]
      public OInt mCheckCtr;

      [SerializationConstructor]
      public UnitInsp(ArtifactData artifact, int slot_no, bool valid = false, int check_ctr = 1)
      {
        this.mArtifact = artifact;
        this.mSlotNo = (OInt) slot_no;
        this.mValid = (OBool) valid;
        this.mCheckCtr = (OInt) check_ctr;
      }
    }

    [MessagePackObject(true)]
    public class UnitForcedTargeting
    {
      public Unit mUnit;
      public int mTurn;

      public UnitForcedTargeting()
      {
      }

      public UnitForcedTargeting(Unit unit, int turn)
      {
        this.mUnit = unit;
        this.mTurn = turn;
      }
    }

    [MessagePackObject(true)]
    public class UnitProtect
    {
      public Unit target;
      public OInt value;
      public DamageTypes damageType;
      public ProtectTypes type;
      public OInt range;
      public OInt height;
      public bool eternal;
      public bool passive;
      public string skillIname;
    }

    [MessagePackObject(true)]
    public class AbilityChange
    {
      public List<Unit.AbilityChange.Data> mDataLists = new List<Unit.AbilityChange.Data>();

      public AbilityChange()
      {
      }

      public AbilityChange(
        AbilityParam fr_ap,
        AbilityParam to_ap,
        int turn,
        bool is_reset,
        int exp,
        bool is_infinite)
      {
        this.mDataLists.Clear();
        this.mDataLists.Add(new Unit.AbilityChange.Data(fr_ap, to_ap, turn, is_reset, exp, is_infinite));
      }

      public void Clear()
      {
        if (this.mDataLists.Count == 0)
          return;
        this.mDataLists.Clear();
      }

      public void Add(
        AbilityParam fr_ap,
        AbilityParam to_ap,
        int turn,
        bool is_reset,
        int exp,
        bool is_infinite)
      {
        this.mDataLists.Add(new Unit.AbilityChange.Data(fr_ap, to_ap, turn, is_reset, exp, is_infinite));
      }

      public void RemoveLast()
      {
        if (this.mDataLists.Count == 0)
          return;
        this.mDataLists.RemoveAt(this.mDataLists.Count - 1);
      }

      public AbilityParam GetFromAp()
      {
        return this.mDataLists.Count == 0 ? (AbilityParam) null : this.mDataLists[0].mFromAp;
      }

      public AbilityParam GetToAp()
      {
        return this.mDataLists.Count == 0 ? (AbilityParam) null : this.mDataLists[this.mDataLists.Count - 1].mToAp;
      }

      public int GetLastExp()
      {
        return this.mDataLists.Count == 0 ? 0 : this.mDataLists[this.mDataLists.Count - 1].mExp;
      }

      public bool IsInclude(AbilityParam ap)
      {
        bool flag = false;
        for (int index = 0; index < this.mDataLists.Count; ++index)
        {
          Unit.AbilityChange.Data mDataList = this.mDataLists[index];
          if (mDataList.mFromAp == ap || mDataList.mToAp == ap)
          {
            flag = true;
            break;
          }
        }
        return flag;
      }

      public bool IsCancel(AbilityParam fr_ap, AbilityParam to_ap)
      {
        return this.mDataLists.Count != 0 && this.GetFromAp() == to_ap && this.GetToAp() == fr_ap;
      }

      public bool IsBack(AbilityParam fr_ap, AbilityParam to_ap)
      {
        if (this.mDataLists.Count < 2)
          return false;
        Unit.AbilityChange.Data mDataList = this.mDataLists[this.mDataLists.Count - 1];
        return mDataList.mFromAp == to_ap && mDataList.mToAp == fr_ap;
      }

      [MessagePackObject(true)]
      public class Data
      {
        public AbilityParam mFromAp;
        public AbilityParam mToAp;
        public int mTurn;
        public bool mIsReset;
        public int mExp;
        public bool mIsInfinite;

        public Data()
        {
        }

        public Data(
          AbilityParam fr_ap,
          AbilityParam to_ap,
          int turn,
          bool is_reset,
          int exp,
          bool is_infinite)
        {
          this.mFromAp = fr_ap;
          this.mToAp = to_ap;
          this.mTurn = turn;
          this.mIsReset = is_reset;
          this.mExp = exp;
          this.mIsInfinite = is_infinite;
        }
      }
    }

    private enum eAcType
    {
      UNKNOWN,
      NEW,
      CANCEL,
      BACK,
      ADD,
    }
  }
}
