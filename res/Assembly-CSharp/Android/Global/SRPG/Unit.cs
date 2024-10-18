﻿// Decompiled with JetBrains decompiler
// Type: SRPG.Unit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SRPG
{
  public class Unit : BaseObject
  {
    private static string[] mStrNameUnitConds = new string[30]{ "quest.BUD_COND_POISON", "quest.BUD_COND_PARALYSED", "quest.BUD_COND_STUN", "quest.BUD_COND_SLEEP", "quest.BUD_COND_CHARM", "quest.BUD_COND_STONE", "quest.BUD_COND_BLINDNESS", "quest.BUD_COND_DISABLESKILL", "quest.BUD_COND_DISABLEMOVE", "quest.BUD_COND_DISABLEATTACK", "quest.BUD_COND_ZOMBIE", "quest.BUD_COND_DEATHSENTENCE", "quest.BUD_COND_BERSERK", "quest.BUD_COND_DISABLEKNOCKBACK", "quest.BUD_COND_DISABLEBUFF", "quest.BUD_COND_DISABLEDEBUFF", "quest.BUD_COND_STOP", "quest.BUD_COND_FAST", "quest.BUD_COND_SLOW", "quest.BUD_COND_AUTOHEAL", "quest.BUD_COND_DONSOKU", "quest.BUD_COND_RAGE", "quest.BUD_COND_GOODSLEEP", "quest.BUD_COND_AUTOJEWEL", "quest.BUD_COND_DISABLEHEAL", "quest.BUD_COND_DISABLESINGLEATTACK", "quest.BUD_COND_DISABLEAREAATTACK", "quest.BUD_COND_DISABLEDECCT", "quest.BUD_COND_DISABLEINCCT", "quest.BUD_COND_SHIELD" };
    private static string[] mStrDescUnitConds = new string[30]{ "quest.BUD_COND_DESC_POISON", "quest.BUD_COND_DESC_PARALYSED", "quest.BUD_COND_DESC_STUN", "quest.BUD_COND_DESC_SLEEP", "quest.BUD_COND_DESC_CHARM", "quest.BUD_COND_DESC_STONE", "quest.BUD_COND_DESC_BLINDNESS", "quest.BUD_COND_DESC_DISABLESKILL", "quest.BUD_COND_DESC_DISABLEMOVE", "quest.BUD_COND_DESC_DISABLEATTACK", "quest.BUD_COND_DESC_ZOMBIE", "quest.BUD_COND_DESC_DEATHSENTENCE", "quest.BUD_COND_DESC_BERSERK", "quest.BUD_COND_DESC_DISABLEKNOCKBACK", "quest.BUD_COND_DESC_DISABLEBUFF", "quest.BUD_COND_DESC_DISABLEDEBUFF", "quest.BUD_COND_DESC_STOP", "quest.BUD_COND_DESC_FAST", "quest.BUD_COND_DESC_SLOW", "quest.BUD_COND_DESC_AUTOHEAL", "quest.BUD_COND_DESC_DONSOKU", "quest.BUD_COND_DESC_RAGE", "quest.BUD_COND_DESC_GOODSLEEP", "quest.BUD_COND_DESC_AUTOJEWEL", "quest.BUD_COND_DESC_DISABLEHEAL", "quest.BUD_COND_DESC_DISABLESINGLEATTACK", "quest.BUD_COND_DESC_DISABLEAREAATTACK", "quest.BUD_COND_DESC_DISABLEDECCT", "quest.BUD_COND_DESC_DISABLEINCCT", "quest.BUD_COND_DESC_SHIELD" };
    public static readonly int MAX_AI = 2;
    public static OInt MAX_UNIT_CONDITION = (OInt) Enum.GetNames(typeof (EUnitCondition)).Length;
    private static OInt UNIT_INDEX = (OInt) 0;
    public static OInt UNIT_CAST_INDEX = (OInt) 0;
    public static readonly int[,] DIRECTION_OFFSETS = new int[4, 2]{ { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };
    public static EUnitDirection[] ReverseDirection = new EUnitDirection[5]{ EUnitDirection.NegativeX, EUnitDirection.NegativeY, EUnitDirection.PositiveX, EUnitDirection.PositiveY, EUnitDirection.NegativeX };
    private static BaseStatus BuffWorkStatus = new BaseStatus();
    private static BaseStatus BuffWorkScaleStatus = new BaseStatus();
    private static BaseStatus DebuffWorkScaleStatus = new BaseStatus();
    private static BaseStatus PassiveWorkScaleStatus = new BaseStatus();
    private static BaseStatus BuffDupliScaleStatus = new BaseStatus();
    private static BaseStatus DebuffDupliScaleStatus = new BaseStatus();
    private OInt mUnitFlag = (OInt) 0;
    private OInt mCommandFlag = (OInt) 0;
    private OIntVector2 mGridPosition = new OIntVector2();
    private OIntVector2 mGridPositionTurnStart = new OIntVector2();
    private OInt mSearched = (OInt) 0;
    private BaseStatus mMaximumBaseStatus = new BaseStatus();
    private BaseStatus mMaximumStatus = new BaseStatus();
    private BaseStatus mCurrentStatus = new BaseStatus();
    private List<AIParam> mAI = new List<AIParam>(Unit.MAX_AI);
    private OInt mAITop = (OInt) 0;
    private AIActionTable mAIActionTable = new AIActionTable();
    private OInt mAIActionIndex = (OInt) 0;
    private OInt mAIActionTurnCount = (OInt) 0;
    private AIPatrolTable mAIPatrolTable = new AIPatrolTable();
    private OInt mAIPatrolIndex = (OInt) 0;
    private List<BuffAttachment> mBuffAttachments = new List<BuffAttachment>(8);
    private List<CondAttachment> mCondAttachments = new List<CondAttachment>(8);
    private Unit.UnitDrop mDrop = new Unit.UnitDrop();
    private Unit.UnitSteal mSteal = new Unit.UnitSteal();
    private List<Unit.UnitShield> mShields = new List<Unit.UnitShield>();
    private OBool mEntryTriggerAndCheck = (OBool) false;
    private OInt mWaitEntryClock = (OInt) 0;
    private OInt mMoveWaitTurn = (OInt) 0;
    private OInt mActionCount = (OInt) 0;
    private OInt mDeathCount = (OInt) 0;
    private OInt mAutoJewel = (OInt) 0;
    private Dictionary<SkillData, OInt> mSkillUseCount = new Dictionary<SkillData, OInt>();
    public const int DIRECTION_MAX = 4;
    private string mUnitName;
    private string mUniqueName;
    private UnitData mUnitData;
    private EUnitSide mSide;
    private OInt mTurnStartDir;
    private NPCSetting mSettingNPC;
    private OInt mUnitIndex;
    private SkillData mAIForceSkill;
    private MapBreakObj mBreakObj;
    private string mCreateBreakObjId;
    private int mCreateBreakObjClock;
    private OInt mCurrentCondition;
    private OInt mDisableCondition;
    public Unit Target;
    public Grid TreasureGainTarget;
    public EUnitDirection Direction;
    public bool IsPartyMember;
    public bool IsSub;
    private EventTrigger mEventTrigger;
    private List<UnitEntryTrigger> mEntryTriggers;
    private OInt mChargeTime;
    private SkillData mCastSkill;
    private OInt mCastTime;
    private OInt mCastIndex;
    private Unit mUnitTarget;
    private Grid mGridTarget;
    private GridMap<bool> mCastSkillGridMap;
    private Unit mRageTarget;
    private Unit mGuardTarget;
    private OInt mGuardTurn;
    private string mParentUniqueName;
    private int mTowerStartHP;
    public List<OString> mNotifyUniqueNames;
    private int mKillCount;
    private bool mDropDirection;
    public int OwnerPlayerIndex;
    private int mTurnCount;
    private bool mEntryUnit;
    private SkillData mPushCastSkill;
    private MySound.Voice mBattleVoice;

    public static string[] StrNameUnitConds
    {
      get
      {
        return Unit.mStrNameUnitConds;
      }
    }

    public static string[] StrDescUnitConds
    {
      get
      {
        return Unit.mStrDescUnitConds;
      }
    }

    public OInt AIActionIndex
    {
      get
      {
        return this.mAIActionIndex;
      }
    }

    public OInt AIActionTurnCount
    {
      get
      {
        return this.mAIActionTurnCount;
      }
    }

    public OInt AIPatrolIndex
    {
      get
      {
        return this.mAIPatrolIndex;
      }
    }

    public bool IsNPC
    {
      get
      {
        return this.mSettingNPC != null;
      }
    }

    public int Gems
    {
      set
      {
        this.CurrentStatus.param.mp = (OShort) Math.Max(Math.Min(value, (int) this.MaximumStatus.param.mp), 0);
      }
      get
      {
        return (int) this.CurrentStatus.param.mp;
      }
    }

    public int WaitClock
    {
      set
      {
        this.mWaitEntryClock = (OInt) value;
      }
      get
      {
        return (int) this.mWaitEntryClock;
      }
    }

    public int WaitMoveTurn
    {
      set
      {
        this.mMoveWaitTurn = (OInt) value;
      }
      get
      {
        return (int) this.mMoveWaitTurn;
      }
    }

    public Dictionary<SkillData, OInt> GetSkillUseCount()
    {
      return this.mSkillUseCount;
    }

    public int TurnCount
    {
      get
      {
        return this.mTurnCount;
      }
      set
      {
        this.mTurnCount = value;
      }
    }

    public bool EntryUnit
    {
      get
      {
        return this.mEntryUnit;
      }
    }

    public string UniqueName
    {
      get
      {
        return this.mUniqueName;
      }
    }

    public string UnitName
    {
      get
      {
        return this.mUnitName;
      }
      set
      {
        this.mUnitName = value;
      }
    }

    public UnitData UnitData
    {
      get
      {
        return this.mUnitData;
      }
    }

    public UnitParam UnitParam
    {
      get
      {
        return this.mUnitData.UnitParam;
      }
    }

    public EUnitType UnitType
    {
      get
      {
        if (this.UnitParam != null)
          return this.UnitParam.type;
        return EUnitType.Unit;
      }
    }

    public int Lv
    {
      get
      {
        return this.mUnitData.Lv;
      }
    }

    public JobData Job
    {
      get
      {
        return this.mUnitData.CurrentJob;
      }
    }

    public SkillData LeaderSkill
    {
      get
      {
        return this.mUnitData.LeaderSkill;
      }
    }

    public List<AbilityData> BattleAbilitys
    {
      get
      {
        return this.mUnitData.BattleAbilitys;
      }
    }

    public List<SkillData> BattleSkills
    {
      get
      {
        return this.mUnitData.BattleSkills;
      }
    }

    public List<BuffAttachment> BuffAttachments
    {
      get
      {
        return this.mBuffAttachments;
      }
    }

    public List<CondAttachment> CondAttachments
    {
      get
      {
        return this.mCondAttachments;
      }
    }

    public EquipData[] CurrentEquips
    {
      get
      {
        return this.mUnitData.CurrentEquips;
      }
    }

    public AIParam AI
    {
      get
      {
        if (this.mAI.Count > 0)
          return this.mAI[(int) this.mAITop];
        return (AIParam) null;
      }
    }

    public SkillData AIForceSkill
    {
      get
      {
        return this.mAIForceSkill;
      }
    }

    public BaseStatus MaximumStatus
    {
      get
      {
        return this.mMaximumStatus;
      }
    }

    public BaseStatus CurrentStatus
    {
      get
      {
        return this.mCurrentStatus;
      }
    }

    public BaseStatus MaximumStatusWithMap
    {
      get
      {
        return this.mMaximumBaseStatus;
      }
    }

    public bool IsDead
    {
      get
      {
        if ((int) this.CurrentStatus.param.hp == 0)
          return (int) this.MaximumStatus.param.hp > 0;
        return false;
      }
    }

    public bool IsEntry
    {
      get
      {
        return this.IsUnitFlag(EUnitFlag.Entried);
      }
    }

    public bool IsControl
    {
      get
      {
        if (!this.IsEnableControlCondition())
          return false;
        if (PunMonoSingleton<MyPhoton>.Instance.IsMultiVersus || MonoSingleton<GameManager>.Instance.AudienceMode)
          return true;
        if (this.mSide == EUnitSide.Player)
          return this.IsPartyMember;
        return false;
      }
    }

    public bool IsGimmick
    {
      get
      {
        return this.UnitType != EUnitType.Unit;
      }
    }

    public bool IsDestructable
    {
      get
      {
        if (this.IsGimmick && (int) this.MaximumStatus.param.hp > 0)
          return !this.IsDead;
        return false;
      }
    }

    public bool IsIntoUnit
    {
      get
      {
        return this.mUnitData.IsIntoUnit;
      }
    }

    public bool IsJump
    {
      get
      {
        if (this.mCastSkill != null)
          return this.mCastSkill.CastType == ECastTypes.Jump;
        return false;
      }
    }

    public EUnitSide Side
    {
      get
      {
        return this.mSide;
      }
      set
      {
        this.mSide = value;
      }
    }

    public int UnitFlag
    {
      get
      {
        return (int) this.mUnitFlag;
      }
      set
      {
        this.mUnitFlag = (OInt) value;
      }
    }

    public Unit.UnitDrop Drop
    {
      get
      {
        return this.mDrop;
      }
    }

    public Unit.UnitSteal Steal
    {
      get
      {
        return this.mSteal;
      }
    }

    public List<Unit.UnitShield> Shields
    {
      get
      {
        return this.mShields;
      }
    }

    public int DisableMoveGridHeight
    {
      get
      {
        return Math.Max(this.GetMoveHeight() + 1, BattleMap.MAP_FLOOR_HEIGHT);
      }
    }

    public EElement Element
    {
      get
      {
        return this.UnitData.Element;
      }
    }

    public JobTypes JobType
    {
      get
      {
        return this.UnitData.JobType;
      }
    }

    public RoleTypes RoleType
    {
      get
      {
        return this.UnitData.RoleType;
      }
    }

    public int x
    {
      get
      {
        return (int) this.mGridPosition.x;
      }
      set
      {
        this.mGridPosition.x = (OInt) value;
      }
    }

    public int y
    {
      get
      {
        return (int) this.mGridPosition.y;
      }
      set
      {
        this.mGridPosition.y = (OInt) value;
      }
    }

    public int startX
    {
      get
      {
        return (int) this.mGridPositionTurnStart.x;
      }
      set
      {
        this.mGridPositionTurnStart.x = (OInt) value;
      }
    }

    public int startY
    {
      get
      {
        return (int) this.mGridPositionTurnStart.y;
      }
      set
      {
        this.mGridPositionTurnStart.y = (OInt) value;
      }
    }

    public EUnitDirection startDir
    {
      get
      {
        return (EUnitDirection) (int) this.mTurnStartDir;
      }
      set
      {
        this.mTurnStartDir = (OInt) ((int) value);
      }
    }

    public NPCSetting SettingNPC
    {
      get
      {
        return this.mSettingNPC;
      }
    }

    public int SizeX
    {
      get
      {
        if (this.UnitParam != null)
          return (int) this.UnitParam.sw;
        return 1;
      }
    }

    public int SizeY
    {
      get
      {
        if (this.UnitParam != null)
          return (int) this.UnitParam.sh;
        return 1;
      }
    }

    public EventTrigger EventTrigger
    {
      get
      {
        return this.mEventTrigger;
      }
    }

    public List<UnitEntryTrigger> EntryTriggers
    {
      get
      {
        return this.mEntryTriggers;
      }
    }

    public OBool IsEntryTriggerAndCheck
    {
      get
      {
        return this.mEntryTriggerAndCheck;
      }
    }

    public int ActionCount
    {
      get
      {
        return (int) this.mActionCount;
      }
    }

    public int DeathCount
    {
      get
      {
        return (int) this.mDeathCount;
      }
    }

    public int AutoJewel
    {
      get
      {
        return (int) this.mAutoJewel;
      }
    }

    public OInt ChargeTime
    {
      get
      {
        return this.mChargeTime;
      }
      set
      {
        this.mChargeTime = value;
      }
    }

    public OInt ChargeTimeMax
    {
      get
      {
        FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
        if (fixParam == null)
          return (OInt) 0;
        return (OInt) SkillParam.CalcSkillEffectValue(SkillParamCalcTypes.Scale, (int) this.CurrentStatus.bonus[BattleBonus.ChargeTimeRate], (int) fixParam.ChargeTimeMax);
      }
    }

    public SkillData CastSkill
    {
      get
      {
        return this.mCastSkill;
      }
    }

    public OInt CastTimeMax
    {
      get
      {
        FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
        if (fixParam == null)
          return (OInt) 0;
        return (OInt) SkillParam.CalcSkillEffectValue(SkillParamCalcTypes.Scale, (int) this.CurrentStatus.bonus[BattleBonus.CastTimeRate], (int) fixParam.ChargeTimeMax);
      }
    }

    public OInt CastTime
    {
      get
      {
        return this.mCastTime;
      }
      set
      {
        this.mCastTime = value;
      }
    }

    public OInt CastIndex
    {
      get
      {
        return this.mCastIndex;
      }
    }

    public Unit UnitTarget
    {
      get
      {
        return this.mUnitTarget;
      }
    }

    public Grid GridTarget
    {
      get
      {
        return this.mGridTarget;
      }
    }

    public GridMap<bool> CastSkillGridMap
    {
      get
      {
        return this.mCastSkillGridMap;
      }
      set
      {
        this.mCastSkillGridMap = value;
      }
    }

    public Unit RageTarget
    {
      get
      {
        return this.mRageTarget;
      }
    }

    public OInt UnitIndex
    {
      get
      {
        return this.mUnitIndex;
      }
    }

    public string ParentUniqueName
    {
      get
      {
        return this.mParentUniqueName;
      }
    }

    public List<OString> NotifyUniqueNames
    {
      get
      {
        return this.mNotifyUniqueNames;
      }
    }

    public int TowerStartHP
    {
      get
      {
        return this.mTowerStartHP;
      }
      set
      {
        this.mTowerStartHP = value;
      }
    }

    public int KillCount
    {
      get
      {
        return this.mKillCount;
      }
      set
      {
        this.mKillCount = value;
      }
    }

    public bool IsBreakObj
    {
      get
      {
        return this.UnitType == EUnitType.BreakObj;
      }
    }

    public eMapBreakClashType BreakObjClashType
    {
      get
      {
        if (this.mBreakObj != null)
          return (eMapBreakClashType) this.mBreakObj.clash_type;
        return eMapBreakClashType.ALL;
      }
    }

    public eMapBreakAIType BreakObjAIType
    {
      get
      {
        if (this.mBreakObj != null)
          return (eMapBreakAIType) this.mBreakObj.ai_type;
        return eMapBreakAIType.NONE;
      }
    }

    public eMapBreakSideType BreakObjSideType
    {
      get
      {
        if (this.mBreakObj != null)
          return (eMapBreakSideType) this.mBreakObj.side_type;
        return eMapBreakSideType.UNKNOWN;
      }
    }

    public eMapBreakRayType BreakObjRayType
    {
      get
      {
        if (this.mBreakObj != null)
          return (eMapBreakRayType) this.mBreakObj.ray_type;
        return eMapBreakRayType.PASS;
      }
    }

    public bool IsBreakDispUI
    {
      get
      {
        if (this.mBreakObj != null)
          return this.mBreakObj.is_ui != 0;
        return false;
      }
    }

    public string CreateBreakObjId
    {
      get
      {
        return this.mCreateBreakObjId;
      }
    }

    public int CreateBreakObjClock
    {
      get
      {
        return this.mCreateBreakObjClock;
      }
    }

    private void SetupStatus()
    {
      BaseStatus dsc = new BaseStatus();
      this.mUnitData.Status.CopyTo(dsc);
      if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null && !this.IsBreakObj)
      {
        QuestParam currentQuest = SceneBattle.Instance.CurrentQuest;
        if (currentQuest != null)
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if ((UnityEngine.Object) instance != (UnityEngine.Object) null && !string.IsNullOrEmpty(currentQuest.MapBuff))
          {
            BaseStatus status1 = new BaseStatus();
            BaseStatus status2 = new BaseStatus();
            BaseStatus status3 = new BaseStatus();
            BaseStatus status4 = new BaseStatus();
            BuffEffect buffEffect = BuffEffect.CreateBuffEffect(instance.MasterParam.GetBuffEffectParam(currentQuest.MapBuff), 0, 0);
            buffEffect.CalcBuffStatus(ref status1, BuffTypes.Buff, SkillParamCalcTypes.Add);
            buffEffect.CalcBuffStatus(ref status2, BuffTypes.Buff, SkillParamCalcTypes.Scale);
            buffEffect.CalcBuffStatus(ref status3, BuffTypes.Debuff, SkillParamCalcTypes.Add);
            buffEffect.CalcBuffStatus(ref status4, BuffTypes.Debuff, SkillParamCalcTypes.Scale);
            status2.Add(status3);
            dsc.AddRate(status2);
            dsc.Add(status1);
            dsc.Add(status3);
          }
        }
      }
      dsc.CopyTo(this.mMaximumBaseStatus);
      dsc.CopyTo(this.mMaximumStatus);
      dsc.CopyTo(this.mCurrentStatus);
    }

    public bool Setup(UnitData unitdata = null, UnitSetting setting = null, Unit.DropItem dropitem = null, Unit.DropItem stealitem = null)
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
        if (!this.mUnitData.Setup(iname1, unitLevelExp, rare, awake, (string) null, 1, elem))
          return false;
        if (npc.abilities != null)
        {
          for (int index1 = 0; index1 < npc.abilities.Length; ++index1)
          {
            string iname2 = (string) npc.abilities[index1].iname;
            int rank = (int) npc.abilities[index1].rank;
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
                    skillData.Setup(ab_param.skills[j].iname, rank, abilityData.GetRankMaxCap(), (MasterParam) null);
                  }
                }
                if (npc.abilities[index1].skills != null)
                {
                  for (int index2 = 0; index2 < npc.abilities[index1].skills.Length; ++index2)
                  {
                    if (npc.abilities[index1].skills[index2] != null)
                    {
                      string sk_iname = (string) npc.abilities[index1].skills[index2].iname;
                      SkillData skillData = this.mUnitData.BattleSkills.Find((Predicate<SkillData>) (p => p.SkillID == sk_iname));
                      if (skillData != null)
                      {
                        skillData.UseRate = npc.abilities[index1].skills[index2].rate;
                        skillData.UseCondition = npc.abilities[index1].skills[index2].cond;
                      }
                    }
                  }
                }
              }
            }
          }
          this.mUnitData.CalcStatus();
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
        this.mSettingNPC = npc;
      }
      else
      {
        if (unitdata == null)
          return false;
        this.mUnitData = unitdata;
        this.mSearched = this.UnitParam.search;
      }
      this.mUnitName = this.UnitParam.name;
      this.mUniqueName = (string) setting.uniqname;
      this.mParentUniqueName = (string) setting.parent;
      this.SetupStatus();
      this.mSide = !this.IsGimmick ? (EUnitSide) (int) setting.side : EUnitSide.Neutral;
      string ai;
      if (!string.IsNullOrEmpty((string) setting.ai))
      {
        ai = (string) setting.ai;
      }
      else
      {
        ai = (string) this.UnitParam.ai;
        JobData currentJob = this.mUnitData.CurrentJob;
        if (currentJob != null && !string.IsNullOrEmpty(currentJob.Param.ai))
          ai = currentJob.Param.ai;
      }
      if (!string.IsNullOrEmpty(ai))
      {
        AIParam aiParam = MonoSingleton<GameManager>.Instance.MasterParam.GetAIParam(ai);
        DebugUtility.Assert(aiParam != null, "ai " + ai + " not found");
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
      this.IsInitialized = true;
      return true;
    }

    public bool SetupDummy(EUnitSide side, string iname, int lv = 1, int rare = 0, int awake = 0, string job_iname = null, int job_rank = 1)
    {
      int unitLevelExp = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitLevelExp(lv);
      this.mUnitData = new UnitData();
      if (!this.mUnitData.Setup(iname, unitLevelExp, rare, awake, job_iname, job_rank, EElement.None))
        return false;
      this.mSide = side;
      this.mUnitName = this.UnitParam.name;
      this.mUnitData.Status.CopyTo(this.mMaximumStatus);
      this.mUnitData.Status.CopyTo(this.mCurrentStatus);
      this.SetUnitFlag(EUnitFlag.Entried, false);
      this.SetUnitFlag(EUnitFlag.Entried, this.CheckEnableEntry());
      this.SetupStatus();
      this.mUnitIndex = Unit.UNIT_INDEX++;
      this.IsInitialized = true;
      return true;
    }

    public bool SetupResume(MultiPlayResumeUnitData data, Unit target, Unit rage, Unit casttarget, List<MultiPlayResumeSkillData> buffskill, List<MultiPlayResumeSkillData> condskill)
    {
      if (this.UnitName != data.name)
        return false;
      SceneBattle instance = SceneBattle.Instance;
      BattleCore battleCore = (BattleCore) null;
      if ((UnityEngine.Object) instance != (UnityEngine.Object) null)
        battleCore = instance.Battle;
      BaseStatus baseStatus1 = new BaseStatus();
      BaseStatus baseStatus2 = new BaseStatus();
      BaseStatus baseStatus3 = new BaseStatus();
      BaseStatus baseStatus4 = new BaseStatus();
      this.CurrentStatus.param.hp = (OInt) data.hp;
      this.Gems = data.gem;
      int x1 = data.x;
      this.x = x1;
      this.startX = x1;
      int y1 = data.y;
      this.y = y1;
      this.startY = y1;
      this.startDir = this.Direction = (EUnitDirection) data.dir;
      if ((int) this.CurrentStatus.param.hp > (int) GlobalVars.MaxHpInBattle && this.mSide == EUnitSide.Player && this.mSide == EUnitSide.Player)
        GlobalVars.MaxHpInBattle = this.CurrentStatus.param.hp;
      this.Target = target;
      this.mRageTarget = rage;
      Dictionary<SkillData, OInt>.KeyCollection keys = this.mSkillUseCount.Keys;
      if (!string.IsNullOrEmpty(data.castskill))
      {
        this.mCastSkill = this.GetSkillData(data.castskill);
        this.mCastTime = (OInt) data.casttime;
        this.mCastIndex = (OInt) data.castindex;
        this.mUnitTarget = casttarget;
        if (data.ctx != -1 && data.cty != -1)
        {
          BattleMap currentMap = instance.Battle.CurrentMap;
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
      if (this.mEntryTriggers != null && data.etr != null)
      {
        for (int index = 0; index < data.etr.Length && index < this.mEntryTriggers.Count; ++index)
          this.mEntryTriggers[index].on = data.etr[index] != 0;
      }
      if (data.skillname != null)
      {
        for (int index1 = 0; index1 < data.skillname.Length; ++index1)
        {
          for (int index2 = 0; index2 < keys.Count; ++index2)
          {
            if (keys.ToArray<SkillData>()[index2].SkillParam.iname == data.skillname[index1])
            {
              this.mSkillUseCount[keys.ToArray<SkillData>()[index2]] = (OInt) data.skillcnt[index1];
              break;
            }
          }
        }
      }
      this.SuspendClearBuffCondEffects();
      if (data.buff != null && battleCore != null)
      {
        for (int index1 = 0; index1 < data.buff.Length; ++index1)
        {
          BuffAttachment dba = (BuffAttachment) null;
          if (buffskill[index1].skill != null)
          {
            SkillData skill = buffskill[index1].skill;
            SkillEffectTargets skilltarget = (SkillEffectTargets) data.buff[index1].skilltarget;
            BuffEffect buffEffect = skill.GetBuffEffect(skilltarget);
            if (buffEffect != null)
            {
              int turn = data.buff[index1].turn;
              ESkillCondition cond = buffEffect.param.cond;
              EffectCheckTargets chkTarget = buffEffect.param.chk_target;
              EffectCheckTimings chkTiming = buffEffect.param.chk_timing;
              int duplicateCount = skill.DuplicateCount;
              baseStatus1.Clear();
              baseStatus2.Clear();
              baseStatus3.Clear();
              baseStatus4.Clear();
              skill.BuffSkill(skill.Timing, baseStatus1, baseStatus2, baseStatus3, baseStatus4, (RandXorshift) null, skilltarget, true);
              if (data.buff[index1].type == 0)
              {
                switch ((SkillParamCalcTypes) data.buff[index1].calc)
                {
                  case SkillParamCalcTypes.Add:
                    dba = battleCore.CreateBuffAttachment(buffskill[index1].user, this, skill, skilltarget, buffEffect.param, (BuffTypes) data.buff[index1].type, (SkillParamCalcTypes) data.buff[index1].calc, baseStatus1, cond, turn, chkTarget, chkTiming, data.buff[index1].passive, duplicateCount);
                    break;
                  case SkillParamCalcTypes.Scale:
                    dba = battleCore.CreateBuffAttachment(buffskill[index1].user, this, skill, skilltarget, buffEffect.param, (BuffTypes) data.buff[index1].type, (SkillParamCalcTypes) data.buff[index1].calc, baseStatus2, cond, turn, chkTarget, chkTiming, data.buff[index1].passive, duplicateCount);
                    break;
                }
              }
              else
              {
                switch ((SkillParamCalcTypes) data.buff[index1].calc)
                {
                  case SkillParamCalcTypes.Add:
                    dba = battleCore.CreateBuffAttachment(buffskill[index1].user, this, skill, skilltarget, buffEffect.param, BuffTypes.Debuff, SkillParamCalcTypes.Add, baseStatus3, cond, turn, chkTarget, chkTiming, data.buff[index1].passive, duplicateCount);
                    break;
                  case SkillParamCalcTypes.Scale:
                    dba = battleCore.CreateBuffAttachment(buffskill[index1].user, this, skill, skilltarget, buffEffect.param, BuffTypes.Debuff, SkillParamCalcTypes.Scale, baseStatus4, cond, turn, chkTarget, chkTiming, data.buff[index1].passive, duplicateCount);
                    break;
                }
              }
              if (dba != null)
              {
                dba.turn = (OInt) turn;
                if (dba.skill != null)
                {
                  int num1 = 1;
                  if (dba.DuplicateCount > 0)
                  {
                    int num2 = 0;
                    for (int index2 = 0; index2 < this.BuffAttachments.Count; ++index2)
                    {
                      if (this.isSameBuffAttachment(this.BuffAttachments[index2], dba))
                        ++num2;
                    }
                    num1 = Math.Max(1 + num2 - dba.DuplicateCount, 0);
                  }
                  if (num1 > 0)
                  {
                    int num2 = 0;
                    for (int index2 = 0; index2 < this.BuffAttachments.Count; ++index2)
                    {
                      if (this.isSameBuffAttachment(this.BuffAttachments[index2], dba))
                      {
                        this.BuffAttachments.RemoveAt(index2--);
                        if (++num2 >= num1)
                          break;
                      }
                    }
                  }
                }
                this.BuffAttachments.Add(dba);
              }
            }
          }
          else if (buffskill[index1].user != null)
          {
            EventTrigger eventTrigger = buffskill[index1].user.EventTrigger;
            if (eventTrigger != null)
            {
              BuffAttachment buffAttachment = eventTrigger.MakeBuff(buffskill[index1].user, this);
              buffAttachment.turn = (OInt) data.buff[index1].turn;
              this.BuffAttachments.Add(buffAttachment);
            }
          }
        }
      }
      if (data.cond != null && battleCore != null)
      {
        for (int index1 = 0; index1 < data.cond.Length; ++index1)
        {
          CondAttachment condAttachment1 = battleCore.CreateCondAttachment(condskill[index1].user, this, condskill[index1].skill, (ConditionEffectTypes) data.cond[index1].type, (ESkillCondition) data.cond[index1].condition, (EUnitCondition) data.cond[index1].calc, EffectCheckTargets.Target, (EffectCheckTimings) data.cond[index1].timing, data.cond[index1].turn, data.cond[index1].passive, data.cond[index1].curse != 0);
          if (condAttachment1 != null)
          {
            condAttachment1.CheckTarget = condskill[index1].check;
            if (condAttachment1.skill != null)
            {
              for (int index2 = 0; index2 < this.CondAttachments.Count; ++index2)
              {
                CondAttachment condAttachment2 = this.CondAttachments[index2];
                if (condAttachment2.skill != null && condAttachment2.skill.SkillID == condAttachment1.skill.SkillID && (condAttachment2.CondType == condAttachment1.CondType && condAttachment2.Condition == condAttachment1.Condition))
                {
                  List<CondAttachment> condAttachments = this.CondAttachments;
                  int index3 = index2;
                  int num = index3 - 1;
                  condAttachments.RemoveAt(index3);
                  break;
                }
              }
            }
            this.CondAttachments.Add(condAttachment1);
          }
        }
      }
      if (data.shields != null)
      {
        foreach (MultiPlayResumeShield shield in data.shields)
        {
          SkillParam skillParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetSkillParam(shield.inm);
          if (skillParam != null)
            this.AddShieldSuspend(skillParam, shield.nhp, shield.mhp, shield.ntu, shield.mtu, shield.drt, shield.dvl);
        }
      }
      this.UpdateBuffEffects();
      this.UpdateCondEffects();
      this.CalcCurrentStatus(false);
      this.SetUnitFlag(EUnitFlag.Searched, data.search != 0);
      this.SetUnitFlag(EUnitFlag.ToDying, data.to_dying != 0);
      this.SetUnitFlag(EUnitFlag.Paralysed, data.paralyse != 0);
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
      using (List<ArtifactData>.Enumerator enumerator = artifactDataList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ArtifactData current = enumerator.Current;
          if (current != null && current.BattleEffectSkill != null && (current.BattleEffectSkill.SkillParam != null && current.BattleEffectSkill.SkillParam.iname == skill_id))
            return current.BattleEffectSkill;
        }
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
        if (itemData != null && itemData.Skill != null && (itemData.Skill.SkillParam != null && itemData.Skill.SkillParam.iname == skill_id))
          return itemData.Skill;
      }
      return (SkillData) null;
    }

    public void SuspendClearBuffCondEffects()
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        if (!(bool) buffAttachment.IsPassive && (buffAttachment.skill == null || !buffAttachment.skill.IsPassiveSkill()) && (buffAttachment.skill != null && buffAttachment.skill.Timing == ESkillTiming.Auto))
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

    public bool SetupSuspend(BattleCore bc, BattleSuspend.Data.UnitInfo unit_info)
    {
      if (bc == null || unit_info == null)
        return false;
      BaseStatus baseStatus1 = new BaseStatus();
      BaseStatus baseStatus2 = new BaseStatus();
      BaseStatus baseStatus3 = new BaseStatus();
      BaseStatus baseStatus4 = new BaseStatus();
      this.CurrentStatus.param.hp = (OInt) unit_info.nhp;
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
      using (List<BattleSuspend.Data.UnitInfo.SkillUse>.Enumerator enumerator = unit_info.sul.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          BattleSuspend.Data.UnitInfo.SkillUse current = enumerator.Current;
          SkillData skillUseCount = this.GetSkillUseCount(current.sid);
          if (skillUseCount != null)
            this.mSkillUseCount[skillUseCount] = (OInt) current.ctr;
        }
      }
      this.SuspendClearBuffCondEffects();
      using (List<BattleSuspend.Data.UnitInfo.Buff>.Enumerator enumerator = unit_info.bfl.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          BattleSuspend.Data.UnitInfo.Buff current = enumerator.Current;
          Unit unitFromAllUnits1 = BattleSuspend.GetUnitFromAllUnits(bc, current.uni);
          Unit unitFromAllUnits2 = BattleSuspend.GetUnitFromAllUnits(bc, current.cui);
          SkillData skill = ((unitFromAllUnits1 == null ? (SkillData) null : unitFromAllUnits1.GetSkillData(current.sid)) ?? (unitFromAllUnits1 == null ? (SkillData) null : unitFromAllUnits1.SearchArtifactSkill(current.sid))) ?? this.SearchItemSkill(bc, current.sid);
          if (unitFromAllUnits2 != null)
          {
            if (skill == null)
            {
              if (!current.ipa)
              {
                EventTrigger eventTrigger = unitFromAllUnits1.EventTrigger;
                if (eventTrigger != null)
                {
                  BuffAttachment buffAttachment = eventTrigger.MakeBuff(unitFromAllUnits1, this);
                  buffAttachment.turn = (OInt) current.tur;
                  this.BuffAttachments.Add(buffAttachment);
                }
              }
            }
            else if (!current.ipa || !skill.IsSubActuate() && !this.ContainsSkillAttachment(skill))
            {
              SkillEffectTargets stg = (SkillEffectTargets) current.stg;
              BuffEffect buffEffect = skill.GetBuffEffect(stg);
              if (buffEffect != null)
              {
                baseStatus1.Clear();
                baseStatus2.Clear();
                baseStatus3.Clear();
                baseStatus4.Clear();
                skill.BuffSkill(skill.Timing, baseStatus1, baseStatus2, baseStatus3, baseStatus4, (RandXorshift) null, stg, true);
                ESkillCondition cond = buffEffect.param.cond;
                EffectCheckTargets chkTarget = buffEffect.param.chk_target;
                EffectCheckTimings chkTiming = buffEffect.param.chk_timing;
                int duplicateCount = skill.DuplicateCount;
                int tur = current.tur;
                BuffAttachment buffAttachment = (BuffAttachment) null;
                if (current.btp == 0)
                {
                  switch ((SkillParamCalcTypes) current.ctp)
                  {
                    case SkillParamCalcTypes.Add:
                      buffAttachment = bc.CreateBuffAttachment(unitFromAllUnits1, this, skill, stg, buffEffect.param, BuffTypes.Buff, SkillParamCalcTypes.Add, baseStatus1, cond, tur, chkTarget, chkTiming, current.ipa, duplicateCount);
                      break;
                    case SkillParamCalcTypes.Scale:
                      buffAttachment = bc.CreateBuffAttachment(unitFromAllUnits1, this, skill, stg, buffEffect.param, BuffTypes.Buff, SkillParamCalcTypes.Scale, baseStatus2, cond, tur, chkTarget, chkTiming, current.ipa, duplicateCount);
                      break;
                  }
                }
                else
                {
                  switch ((SkillParamCalcTypes) current.ctp)
                  {
                    case SkillParamCalcTypes.Add:
                      buffAttachment = bc.CreateBuffAttachment(unitFromAllUnits1, this, skill, stg, buffEffect.param, BuffTypes.Debuff, SkillParamCalcTypes.Add, baseStatus3, cond, tur, chkTarget, chkTiming, current.ipa, duplicateCount);
                      break;
                    case SkillParamCalcTypes.Scale:
                      buffAttachment = bc.CreateBuffAttachment(unitFromAllUnits1, this, skill, stg, buffEffect.param, BuffTypes.Debuff, SkillParamCalcTypes.Scale, baseStatus4, cond, tur, chkTarget, chkTiming, current.ipa, duplicateCount);
                      break;
                  }
                }
                if (buffAttachment != null)
                {
                  buffAttachment.turn = (OInt) tur;
                  this.BuffAttachments.Add(buffAttachment);
                }
              }
            }
          }
        }
      }
      using (List<BattleSuspend.Data.UnitInfo.Cond>.Enumerator enumerator = unit_info.cdl.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          BattleSuspend.Data.UnitInfo.Cond current = enumerator.Current;
          Unit unitFromAllUnits1 = BattleSuspend.GetUnitFromAllUnits(bc, current.uni);
          Unit unitFromAllUnits2 = BattleSuspend.GetUnitFromAllUnits(bc, current.cui);
          SkillData skill = ((unitFromAllUnits1 == null ? (SkillData) null : unitFromAllUnits1.GetSkillData(current.sid)) ?? (unitFromAllUnits1 == null ? (SkillData) null : unitFromAllUnits1.SearchArtifactSkill(current.sid))) ?? this.SearchItemSkill(bc, current.sid);
          if (unitFromAllUnits2 != null && (skill != null || current.ucd == 3 && !current.ipa) && (!current.ipa || !skill.IsSubActuate() && !this.ContainsSkillAttachment(skill)))
          {
            CondAttachment condAttachment = bc.CreateCondAttachment(unitFromAllUnits1, this, skill, (ConditionEffectTypes) current.cdt, (ESkillCondition) current.ucd, (EUnitCondition) current.cnd, EffectCheckTargets.Target, (EffectCheckTimings) current.tim, current.tur, current.ipa, current.icu);
            if (condAttachment != null)
            {
              condAttachment.CheckTarget = unitFromAllUnits2;
              this.CondAttachments.Add(condAttachment);
            }
          }
        }
      }
      using (List<BattleSuspend.Data.UnitInfo.Shield>.Enumerator enumerator = unit_info.shl.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          BattleSuspend.Data.UnitInfo.Shield current = enumerator.Current;
          SkillParam skillParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetSkillParam(current.inm);
          if (skillParam != null)
            this.AddShieldSuspend(skillParam, current.nhp, current.mhp, current.ntu, current.mtu, current.drt, current.dvl);
        }
      }
      this.UpdateBuffEffects();
      this.UpdateCondEffects();
      this.CalcCurrentStatus(false);
      return true;
    }

    public string[] GetTags()
    {
      return this.UnitParam.tags;
    }

    public bool CheckExistMap()
    {
      if (!this.IsDead && this.IsEntry)
        return !this.IsSub;
      return false;
    }

    public bool CheckCollision(Grid grid)
    {
      return this.CheckCollision(grid.x, grid.y, true);
    }

    public bool CheckCollision(int cx, int cy, bool is_check_exist = true)
    {
      if (is_check_exist && !this.CheckExistMap())
        return false;
      for (int index1 = 0; index1 < this.SizeY; ++index1)
      {
        for (int index2 = 0; index2 < this.SizeX; ++index2)
        {
          if (this.x + index2 == cx && this.y + index1 == cy)
            return true;
        }
      }
      return false;
    }

    public bool CheckCollision(int x0, int y0, int x1, int y1)
    {
      return this.CheckExistMap() && x1 >= this.x && (this.x + this.SizeX - 1 >= x0 && y1 >= this.y) && this.y + this.SizeY - 1 >= y0;
    }

    public bool CheckCollisionDirect(int tx, int ty, int cx, int cy, bool is_check_exist = true)
    {
      if (is_check_exist && !this.CheckExistMap())
        return false;
      for (int index1 = 0; index1 < this.SizeY; ++index1)
      {
        for (int index2 = 0; index2 < this.SizeX; ++index2)
        {
          if (tx + index2 == cx && ty + index1 == cy)
            return true;
        }
      }
      return false;
    }

    private bool isSameBuffAttachment(BuffAttachment sba, BuffAttachment dba)
    {
      return sba != null && dba != null && (sba.skill != null && dba.skill != null) && (sba.skill.SkillID == dba.skill.SkillID && sba.BuffType == dba.BuffType && (sba.CalcType == dba.CalcType && sba.CheckTiming == dba.CheckTiming)) && (sba.Param != null && dba.Param != null && sba.Param.iname == dba.Param.iname);
    }

    public int GetBuffAttachmentDuplicateCount(BuffAttachment buff)
    {
      int num = 0;
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        if (this.isSameBuffAttachment(this.BuffAttachments[index], buff))
          ++num;
      }
      return num;
    }

    public void CalcCurrentStatus(bool is_initialized = false)
    {
      int hp = (int) this.CurrentStatus.param.hp;
      int mp = (int) this.CurrentStatus.param.mp;
      Unit.BuffWorkStatus.Clear();
      Unit.BuffWorkScaleStatus.Clear();
      Unit.DebuffWorkScaleStatus.Clear();
      Unit.PassiveWorkScaleStatus.Clear();
      Unit.BuffDupliScaleStatus.Clear();
      Unit.DebuffDupliScaleStatus.Clear();
      this.mAutoJewel = (OInt) 0;
      this.mMaximumBaseStatus.CopyTo(this.MaximumStatus);
      int enemy_dead_count = 0;
      SceneBattle instance = SceneBattle.Instance;
      if ((bool) ((UnityEngine.Object) instance) && instance.Battle != null)
        enemy_dead_count = instance.Battle.GetDeadCountEnemy();
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
        this.BuffAttachments[index].IsCalculated = false;
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        if (buffAttachment.UseCondition != ESkillCondition.Dying)
          this.CalcBuffStatus(buffAttachment, enemy_dead_count);
      }
      BaseStatus src = new BaseStatus();
      src.Add(Unit.BuffWorkScaleStatus);
      src.Add(Unit.DebuffWorkScaleStatus);
      src.Add(Unit.PassiveWorkScaleStatus);
      this.MaximumStatus.Add(Unit.BuffWorkStatus);
      this.MaximumStatus.AddRate(src);
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        if (buffAttachment.UseCondition == ESkillCondition.Dying)
          this.CalcBuffStatus(buffAttachment, enemy_dead_count);
      }
      this.mMaximumBaseStatus.CopyTo(this.MaximumStatus);
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
        this.BuffAttachments[index].IsCalculated = false;
      if (this.IsUnitCondition(EUnitCondition.Blindness))
      {
        FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
        int num1 = 0;
        int num2 = 0;
        bool flag = false;
        for (int index = 0; index < this.CondAttachments.Count; ++index)
        {
          CondAttachment condAttachment = this.CondAttachments[index];
          if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.Blindness))
          {
            int num3 = 0;
            int num4 = 0;
            if (condAttachment.skill != null)
            {
              CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
              if (condEffect1 != null)
              {
                int num5 = (int) ((int) condEffect1.param.v_blink_hit == 0 ? fixParam.BlindnessHitRate : condEffect1.param.v_blink_hit);
                int num6 = (int) ((int) condEffect1.param.v_blink_avo == 0 ? fixParam.BlindnessAvoidRate : condEffect1.param.v_blink_avo);
                if (Math.Abs(num3) + Math.Abs(num4) < Math.Abs(num5) + Math.Abs(num6))
                {
                  num3 = num5;
                  num4 = num6;
                }
                flag = true;
              }
              CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
              if (condEffect2 != null)
              {
                int num5 = (int) ((int) condEffect2.param.v_blink_hit == 0 ? fixParam.BlindnessHitRate : condEffect2.param.v_blink_hit);
                int num6 = (int) ((int) condEffect2.param.v_blink_avo == 0 ? fixParam.BlindnessAvoidRate : condEffect2.param.v_blink_avo);
                if (Math.Abs(num3) + Math.Abs(num4) < Math.Abs(num5) + Math.Abs(num6))
                {
                  num3 = num5;
                  num4 = num6;
                }
                flag = true;
              }
            }
            else
            {
              if (Math.Abs(num3) + Math.Abs(num4) < Math.Abs((int) fixParam.BlindnessHitRate) + Math.Abs((int) fixParam.BlindnessAvoidRate))
              {
                num3 = (int) fixParam.BlindnessHitRate;
                num4 = (int) fixParam.BlindnessAvoidRate;
              }
              flag = true;
            }
            if (Math.Abs(num1) + Math.Abs(num2) < Math.Abs(num3) + Math.Abs(num4))
            {
              num1 = num3;
              num2 = num4;
            }
          }
        }
        if (!flag)
        {
          num1 = (int) fixParam.BlindnessHitRate;
          num2 = (int) fixParam.BlindnessAvoidRate;
        }
        BaseStatus buffWorkStatus1;
        BattleBonus index1;
        (buffWorkStatus1 = Unit.BuffWorkStatus)[index1 = BattleBonus.HitRate] = (OInt) ((int) buffWorkStatus1[index1] + num1);
        BaseStatus buffWorkStatus2;
        BattleBonus index2;
        (buffWorkStatus2 = Unit.BuffWorkStatus)[index2 = BattleBonus.AvoidRate] = (OInt) ((int) buffWorkStatus2[index2] + num2);
      }
      if (this.IsUnitCondition(EUnitCondition.Berserk))
      {
        FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
        int num1 = 0;
        int num2 = 0;
        bool flag = false;
        for (int index = 0; index < this.CondAttachments.Count; ++index)
        {
          CondAttachment condAttachment = this.CondAttachments[index];
          if (condAttachment != null && condAttachment.IsFailCondition() && condAttachment.ContainsCondition(EUnitCondition.Berserk))
          {
            int num3 = 0;
            int num4 = 0;
            if (condAttachment.skill != null)
            {
              CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
              if (condEffect1 != null)
              {
                int num5 = (int) ((int) condEffect1.param.v_berserk_atk == 0 ? fixParam.BerserkAtkRate : condEffect1.param.v_berserk_atk);
                int num6 = (int) ((int) condEffect1.param.v_berserk_def == 0 ? fixParam.BerserkDefRate : condEffect1.param.v_berserk_def);
                if (Math.Abs(num3) + Math.Abs(num4) < Math.Abs(num5) + Math.Abs(num6))
                {
                  num3 = num5;
                  num4 = num6;
                }
              }
              CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
              if (condEffect2 != null)
              {
                int num5 = (int) ((int) condEffect2.param.v_berserk_atk == 0 ? fixParam.BerserkAtkRate : condEffect2.param.v_berserk_atk);
                int num6 = (int) ((int) condEffect2.param.v_berserk_def == 0 ? fixParam.BerserkDefRate : condEffect2.param.v_berserk_def);
                if (Math.Abs(num3) + Math.Abs(num4) < Math.Abs(num5) + Math.Abs(num6))
                {
                  num3 = num5;
                  num4 = num6;
                }
              }
            }
            else if (Math.Abs(num3) + Math.Abs(num4) < Math.Abs((int) fixParam.BerserkAtkRate) + Math.Abs((int) fixParam.BerserkDefRate))
            {
              num3 = (int) fixParam.BerserkAtkRate;
              num4 = (int) fixParam.BerserkDefRate;
            }
            if (Math.Abs(num1) + Math.Abs(num2) < Math.Abs(num3) + Math.Abs(num4))
            {
              num1 = num3;
              num2 = num4;
            }
            flag = true;
          }
        }
        if (!flag)
        {
          num1 = (int) fixParam.BerserkAtkRate;
          num2 = (int) fixParam.BerserkDefRate;
        }
        BaseStatus passiveWorkScaleStatus1;
        StatusTypes index1;
        (passiveWorkScaleStatus1 = Unit.PassiveWorkScaleStatus)[index1 = StatusTypes.Atk] = (OInt) ((int) passiveWorkScaleStatus1[index1] + num1);
        BaseStatus passiveWorkScaleStatus2;
        StatusTypes index2;
        (passiveWorkScaleStatus2 = Unit.PassiveWorkScaleStatus)[index2 = StatusTypes.Mag] = (OInt) ((int) passiveWorkScaleStatus2[index2] + num1);
        BaseStatus passiveWorkScaleStatus3;
        StatusTypes index3;
        (passiveWorkScaleStatus3 = Unit.PassiveWorkScaleStatus)[index3 = StatusTypes.Def] = (OInt) ((int) passiveWorkScaleStatus3[index3] + num2);
        BaseStatus passiveWorkScaleStatus4;
        StatusTypes index4;
        (passiveWorkScaleStatus4 = Unit.PassiveWorkScaleStatus)[index4 = StatusTypes.Mnd] = (OInt) ((int) passiveWorkScaleStatus4[index4] + num2);
      }
      Unit.BuffWorkScaleStatus.Add(Unit.DebuffWorkScaleStatus);
      Unit.BuffWorkScaleStatus.Add(Unit.PassiveWorkScaleStatus);
      this.MaximumStatus.Add(Unit.BuffWorkStatus);
      this.MaximumStatus.AddRate(Unit.BuffWorkScaleStatus);
      this.MaximumStatus.CopyTo(this.CurrentStatus);
      this.mAutoJewel = this.CurrentStatus[BattleBonus.AutoJewel];
      if (is_initialized)
      {
        this.CurrentStatus.param.mp = (OShort) this.GetStartGems();
      }
      else
      {
        if ((int) this.CurrentStatus.param.hp > (int) GlobalVars.MaxHpInBattle && this.mSide == EUnitSide.Player)
          GlobalVars.MaxHpInBattle = this.CurrentStatus.param.hp;
        this.CurrentStatus.param.hp = (OInt) Math.Min(hp, (int) this.MaximumStatus.param.hp);
        this.CurrentStatus.param.mp = (OShort) Math.Min(mp, (int) this.MaximumStatus.param.mp);
        if ((int) this.CurrentStatus.param.hp > (int) GlobalVars.MaxHpInBattle && this.mSide == EUnitSide.Player)
          GlobalVars.MaxHpInBattle = this.CurrentStatus.param.hp;
      }
      for (int index = 0; index < this.mShields.Count; ++index)
      {
        if (!this.mShields[index].skill_param.IsShieldReset() && (int) this.mShields[index].hp == 0)
          this.mShields.RemoveAt(index--);
      }
      if (this.mRageTarget == null || !this.mRageTarget.IsDeadCondition())
        return;
      this.CureCondEffects(EUnitCondition.Rage, true, true);
    }

    private void CalcBuffStatus(BuffAttachment buff, int enemy_dead_count)
    {
      if (buff == null || buff.IsCalculated)
        return;
      SkillData skill = buff.skill;
      BaseStatus baseStatus = new BaseStatus(buff.status);
      if (!(bool) buff.IsPassive && (skill == null || !skill.IsPassiveSkill()) && !this.IsEnableBuffEffect(buff.BuffType) || buff.UseCondition != ESkillCondition.None && buff.UseCondition == ESkillCondition.Dying && !this.IsDying())
        return;
      if (buff.Param != null)
      {
        int num = 0;
        if (buff.Param.mAppType == EAppType.AllKillCount)
        {
          if (enemy_dead_count == 0)
            return;
          num = Math.Min(enemy_dead_count, buff.Param.mAppMct) - 1;
        }
        else if (buff.Param.mAppType == EAppType.SelfKillCount)
        {
          if (this.mKillCount == 0)
            return;
          num = Math.Min(this.mKillCount, buff.Param.mAppMct) - 1;
        }
        if (buff.Param.mEffRange == EEffRange.SelfNearAlly && buff.Param.mAppMct > 0)
        {
          int allyUnitNum = this.GetAllyUnitNum(buff.user);
          if (allyUnitNum == 0)
            return;
          num += Math.Min(allyUnitNum, buff.Param.mAppMct) - 1;
        }
        if (num > 0)
        {
          for (int index = 0; index < num; ++index)
            baseStatus.Add(buff.status);
        }
      }
      int attachmentDuplicateCount = this.GetBuffAttachmentDuplicateCount(buff);
      if (attachmentDuplicateCount > 1)
      {
        switch (buff.CalcType)
        {
          case SkillParamCalcTypes.Add:
          case SkillParamCalcTypes.Fixed:
            for (int index = 0; index < attachmentDuplicateCount; ++index)
              Unit.BuffWorkStatus.Add(baseStatus);
            break;
          case SkillParamCalcTypes.Scale:
            if (buff.skill != null && buff.skill.IsPassiveSkill())
            {
              for (int index = 0; index < attachmentDuplicateCount; ++index)
                Unit.PassiveWorkScaleStatus.Add(baseStatus);
              break;
            }
            if (buff.BuffType == BuffTypes.Buff)
            {
              Unit.BuffDupliScaleStatus.Clear();
              for (int index = 0; index < attachmentDuplicateCount; ++index)
                Unit.BuffDupliScaleStatus.Add(baseStatus);
              Unit.BuffWorkScaleStatus.ReplaceHighest(Unit.BuffDupliScaleStatus);
              break;
            }
            Unit.DebuffDupliScaleStatus.Clear();
            for (int index = 0; index < attachmentDuplicateCount; ++index)
              Unit.DebuffDupliScaleStatus.Add(baseStatus);
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
        switch (buff.CalcType)
        {
          case SkillParamCalcTypes.Add:
            Unit.BuffWorkStatus.Add(baseStatus);
            break;
          case SkillParamCalcTypes.Scale:
            if (buff.skill != null && buff.skill.IsPassiveSkill())
            {
              Unit.PassiveWorkScaleStatus.Add(baseStatus);
              break;
            }
            if (buff.BuffType == BuffTypes.Buff)
            {
              Unit.BuffWorkScaleStatus.ReplaceHighest(baseStatus);
              break;
            }
            Unit.DebuffWorkScaleStatus.ReplaceLowest(baseStatus);
            break;
        }
      }
    }

    public void SetUnitFlag(EUnitFlag tgt, bool sw)
    {
      if (sw)
      {
        Unit unit = this;
        unit.mUnitFlag = (OInt) ((int) ((EUnitFlag) (int) unit.mUnitFlag | tgt));
      }
      else
      {
        Unit unit = this;
        unit.mUnitFlag = (OInt) ((int) ((EUnitFlag) (int) unit.mUnitFlag & ~tgt));
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
        unit.mCommandFlag = (OInt) ((int) ((EUnitCommandFlag) (int) unit.mCommandFlag | tgt));
      }
      else
      {
        Unit unit = this;
        unit.mCommandFlag = (OInt) ((int) ((EUnitCommandFlag) (int) unit.mCommandFlag & ~tgt));
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

    public int GetBaseAvoidRate()
    {
      if (this.Job != null)
        return this.Job.GetJobRankAvoidRate();
      return 0;
    }

    public int GetStartGems()
    {
      int num = 100;
      return (int) this.MaximumStatus.param.mp * (this.Job != null ? num + this.Job.GetJobRankInitJewelRate() : num + (int) this.UnitParam.inimp) / 100 + (int) this.CurrentStatus.param.imp;
    }

    public int GetGoodSleepHpHealValue()
    {
      if (this.IsUnitCondition(EUnitCondition.DisableHeal))
        return 0;
      return Math.Max((int) this.MaximumStatus.param.hp * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GoodSleepHpHealRate / 100, 1);
    }

    public int GetGoodSleepMpHealValue()
    {
      return Math.Max((int) this.MaximumStatus.param.mp * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GoodSleepMpHealRate / 100, 1);
    }

    public void ClearSkillUseCount()
    {
      this.mSkillUseCount.Clear();
      for (int index1 = 0; index1 < this.BattleAbilitys.Count; ++index1)
      {
        AbilityData battleAbility = this.BattleAbilitys[index1];
        for (int index2 = 0; index2 < battleAbility.Skills.Count; ++index2)
        {
          SkillData skill = battleAbility.Skills[index2];
          if ((int) skill.SkillParam.count > 0 && !this.mSkillUseCount.ContainsKey(skill))
            this.mSkillUseCount.Add(skill, this.GetSkillUseCountMax(skill));
        }
      }
    }

    public bool CheckDamageActionStart()
    {
      return this.IsUnitFlag(EUnitFlag.DamagedActionStart) || this.NotifyUniqueNames != null && this.NotifyUniqueNames.Count > 0;
    }

    public bool CheckEnableSkillUseCount(SkillData skill)
    {
      return (skill.SkillType == ESkillType.Skill || skill.SkillType == ESkillType.Reaction) && this.mSkillUseCount.ContainsKey(skill);
    }

    public OInt GetSkillUseCount(SkillData skill)
    {
      if (!this.mSkillUseCount.ContainsKey(skill))
        return (OInt) 0;
      return this.mSkillUseCount[skill];
    }

    public OInt GetSkillUseCountMax(SkillData skill)
    {
      return (OInt) ((int) skill.SkillParam.count + (int) this.MaximumStatus[BattleBonus.SkillUseCount]);
    }

    public void UpdateSkillUseCount(SkillData skill, int count)
    {
      if (!this.CheckEnableSkillUseCount(skill))
        return;
      int skillUseCount = (int) this.GetSkillUseCount(skill);
      this.mSkillUseCount[skill] = (OInt) Math.Min(Math.Max(skillUseCount + count, 0), (int) this.GetSkillUseCountMax(skill));
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

    public bool IsEnableSteal()
    {
      return !this.IsGimmick && this.Steal != null;
    }

    public bool IsEnableControlCondition()
    {
      return this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.ForceAuto) && (!this.IsUnitCondition(EUnitCondition.Charm) && !this.IsUnitCondition(EUnitCondition.Zombie)) && (!this.IsUnitCondition(EUnitCondition.Berserk) && !this.IsUnitCondition(EUnitCondition.Rage));
    }

    public bool IsEnableMoveCondition(bool bCondOnly = false)
    {
      return this.IsUnitFlag(EUnitFlag.ForceMoved) || (bCondOnly || !this.IsUnitFlag(EUnitFlag.Moved)) && (this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.Paralysed)) && (!this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Sleep) && (!this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.DisableMove))) && !this.IsUnitCondition(EUnitCondition.Stop);
    }

    public bool IsEnableActionCondition()
    {
      if (!this.IsEnableAttackCondition(false) && !this.IsEnableSkillCondition(false))
        return this.IsEnableItemCondition(false);
      return true;
    }

    public bool IsEnableAttackCondition(bool bCondOnly = false)
    {
      return (bCondOnly || !this.IsUnitFlag(EUnitFlag.Action)) && (this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.Paralysed)) && (!this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Sleep) && (!this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.DisableAttack))) && (!this.IsUnitCondition(EUnitCondition.Stop) && this.GetAttackSkill() != null);
    }

    public bool IsEnableHelpCondition()
    {
      return this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.Paralysed) && (!this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Sleep)) && (!this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.Charm) && (!this.IsUnitCondition(EUnitCondition.Zombie) && !this.IsUnitCondition(EUnitCondition.DisableAttack))) && (!this.IsUnitCondition(EUnitCondition.Berserk) && !this.IsUnitCondition(EUnitCondition.Stop) && (!this.IsUnitCondition(EUnitCondition.Rage) && this.GetAttackSkill() != null));
    }

    public bool IsEnableSkillCondition(bool bCondOnly = false)
    {
      return (bCondOnly || !this.IsUnitFlag(EUnitFlag.Action)) && (this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.Paralysed)) && (!this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Sleep) && (!this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.Charm))) && (!this.IsUnitCondition(EUnitCondition.Zombie) && !this.IsUnitCondition(EUnitCondition.DisableSkill) && (!this.IsUnitCondition(EUnitCondition.Berserk) && !this.IsUnitCondition(EUnitCondition.Stop)) && !this.IsUnitCondition(EUnitCondition.Rage));
    }

    public bool IsEnableItemCondition(bool bCondOnly = false)
    {
      return (bCondOnly || !this.IsUnitFlag(EUnitFlag.Action)) && (this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.Paralysed)) && (!this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Sleep) && (!this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.Charm))) && (!this.IsUnitCondition(EUnitCondition.Zombie) && !this.IsUnitCondition(EUnitCondition.Berserk) && (!this.IsUnitCondition(EUnitCondition.Stop) && !this.IsUnitCondition(EUnitCondition.Rage)));
    }

    public bool IsEnableReactionCondition()
    {
      return this.CheckExistMap() && !this.IsUnitFlag(EUnitFlag.Paralysed) && (!this.IsUnitCondition(EUnitCondition.Stun) && !this.IsUnitCondition(EUnitCondition.Stop)) && (!this.IsUnitCondition(EUnitCondition.Sleep) && !this.IsUnitCondition(EUnitCondition.Stone) && !this.IsUnitCondition(EUnitCondition.DisableSkill));
    }

    public bool IsEnableReactionSkill(SkillData skill)
    {
      if (!skill.IsReactionSkill() || (int) skill.SkillParam.count > 0 && (int) this.GetSkillUseCount(skill) == 0)
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
      return this.CheckExistMap() && !this.IsUnitCondition(EUnitCondition.Stun) && (!this.IsUnitCondition(EUnitCondition.Stop) && !this.IsUnitCondition(EUnitCondition.Sleep)) && !this.IsUnitCondition(EUnitCondition.Stone);
    }

    public bool IsEnableAutoHealCondition()
    {
      return !this.IsDeadCondition() && (this.mCastSkill == null || this.mCastSkill.CastType != ECastTypes.Jump);
    }

    public bool IsEnableGimmickCondition()
    {
      return true;
    }

    public bool IsDeadCondition()
    {
      return this.IsDead || this.IsUnitCondition(EUnitCondition.Stone);
    }

    public bool IsDisableGimmick()
    {
      if (!this.IsGimmick)
        return true;
      if (this.IsDestructable || this.IsBreakObj)
        return this.IsDead;
      if (this.mEventTrigger != null)
        return this.mEventTrigger.Count == 0;
      return false;
    }

    public bool IsEnableAutoMode()
    {
      return !this.IsUnitFlag(EUnitFlag.ForceAuto) && this.IsControl && (this.IsEntry && !this.IsSub) && !this.IsDeadCondition() && ((!this.IsUnitCondition(EUnitCondition.Charm) || !this.IsUnitCondition(EUnitCondition.Zombie) || !this.IsUnitCondition(EUnitCondition.Berserk)) && this.GetRageTarget() == null);
    }

    public bool CheckCancelSkillFailCondition(EUnitCondition condition)
    {
      return this.CastSkill != null && ((condition & EUnitCondition.Stun) != (EUnitCondition) 0 || (condition & EUnitCondition.Sleep) != (EUnitCondition) 0 || ((condition & EUnitCondition.Stone) != (EUnitCondition) 0 || (condition & EUnitCondition.DisableSkill) != (EUnitCondition) 0) || ((condition & EUnitCondition.Rage) != (EUnitCondition) 0 || (condition & EUnitCondition.Charm) != (EUnitCondition) 0 || (condition & EUnitCondition.Berserk) != (EUnitCondition) 0) || this.CastSkill.IsDamagedSkill() && (condition & EUnitCondition.DisableAttack) != (EUnitCondition) 0);
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
        if (condAttachment != null && condAttachment.IsFailCondition() && (condAttachment.Condition == condition && condAttachment.IsCurse))
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
      if (skill.Condition != ESkillCondition.None && skill.Condition == ESkillCondition.Dying && !this.IsDying())
        return false;
      if (skill.IsSetBreakObjSkill())
      {
        SceneBattle instance = SceneBattle.Instance;
        BattleCore battleCore = (BattleCore) null;
        if ((bool) ((UnityEngine.Object) instance))
          battleCore = instance.Battle;
        if (battleCore != null && !battleCore.IsAllowBreakObjEntryMax())
          return false;
      }
      return true;
    }

    public void SetGuardTarget(Unit target, int turn)
    {
      this.mGuardTarget = target;
      this.mGuardTurn = (OInt) turn;
    }

    public Unit GetGuardTarget()
    {
      if (this.IsDeadCondition())
        return (Unit) null;
      if (!this.CheckExistMap() || this.IsUnitCondition(EUnitCondition.Stun) || (this.IsUnitCondition(EUnitCondition.Paralysed) || this.IsUnitCondition(EUnitCondition.Sleep)))
        return (Unit) null;
      if (this.mGuardTarget == null || this.CheckEnemySide(this.mGuardTarget) || (this.mGuardTarget.IsDeadCondition() || !this.mGuardTarget.CheckExistMap()))
        return (Unit) null;
      return this.mGuardTarget;
    }

    public void CancelGuradTarget()
    {
      this.mGuardTarget = (Unit) null;
      this.mGuardTurn = (OInt) 0;
    }

    private void UpdateGuardTurn()
    {
      if (this.mGuardTarget == null)
        return;
      this.mGuardTurn = (OInt) Math.Max((int) (--this.mGuardTurn), 0);
      if ((int) this.mGuardTurn != 0 && !this.CheckEnemySide(this.mGuardTarget) && (!this.mGuardTarget.IsDeadCondition() && this.mGuardTarget.CheckExistMap()))
        return;
      this.CancelGuradTarget();
    }

    public void AddShield(SkillData skill)
    {
      if (skill == null || skill.EffectType != SkillEffectTypes.Shield || (skill.ShieldType == ShieldTypes.None || this.IsBreakObj))
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

    private void AddShieldSuspend(SkillParam skill_param, int hp, int hp_max, int turn, int turn_max, int damage_rate, int damage_value)
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

    public void CalcShieldDamage(DamageTypes damageType, ref int damage, bool bEnableShieldBreak, AttackDetailTypes attack_detail_type, RandXorshift rand)
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
              break;
            }
            if (mShield.shieldType == ShieldTypes.Limitter && damage <= (int) mShield.hp)
            {
              damage = 0;
              if (!bEnableShieldBreak)
                break;
              mShield.is_efficacy = (OBool) true;
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
        Unit.UnitShield mShield = this.mShields[index];
        if (mShield.skill_param.IsShieldReset())
          mShield.hp = mShield.hpMax;
        if ((int) this.mShields[index].turn > 0)
        {
          --this.mShields[index].turn;
          if ((int) this.mShields[index].turn <= 0)
            this.mShields.RemoveAt(index--);
        }
      }
    }

    public bool ContainsSkillAttachment(SkillData skill)
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        if (buffAttachment.skill != null && buffAttachment.skill.SkillID == skill.SkillID)
          return true;
      }
      using (List<CondAttachment>.Enumerator enumerator = this.CondAttachments.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          CondAttachment current = enumerator.Current;
          if (current.skill != null && current.skill.SkillID == skill.SkillID)
            return true;
        }
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
        if (this.mCastSkill != null && this.mCastSkill != skill && this.mCastSkill.CastType == ECastTypes.Jump)
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
      if (buff.Param != null && buff.Param.IsUpReplenish && (buff.CheckTiming != EffectCheckTimings.Moment && (int) buff.status[ParamTypes.HpMax] != 0))
        num = (int) this.MaximumStatus.param.hp;
      this.CalcCurrentStatus(false);
      if (num != 0)
      {
        StatusParam statusParam = this.CurrentStatus.param;
        statusParam.hp = (OInt) ((int) statusParam.hp + Math.Max((int) this.MaximumStatus.param.hp - num, 0));
      }
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

    public void UpdateBuffEffectTurnCount(EffectCheckTimings timing, Unit current)
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
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

    public void RemoveBuffPrevApply()
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        if (this.BuffAttachments[index].CheckTiming == EffectCheckTimings.PrevApply)
          this.BuffAttachments.RemoveAt(index--);
      }
    }

    public void UpdateBuffEffects()
    {
      for (int index = 0; index < this.BuffAttachments.Count; ++index)
      {
        BuffAttachment buffAttachment = this.BuffAttachments[index];
        SkillData skill = buffAttachment.skill;
        if ((bool) buffAttachment.IsPassive)
        {
          if (skill != null && !skill.IsSubActuate())
          {
            Unit user = buffAttachment.user;
            if (user != null && user.IsDeadCondition())
            {
              SkillData leaderSkill = user.LeaderSkill;
              if (leaderSkill == null || skill.SkillID != leaderSkill.SkillID)
                this.BuffAttachments.RemoveAt(index--);
            }
          }
        }
        else if ((skill == null || !skill.IsPassiveSkill()) && !this.IsEnableBuffEffect(buffAttachment.BuffType))
          this.BuffAttachments.RemoveAt(index--);
        else if (buffAttachment.CheckTarget != null && buffAttachment.CheckTarget.IsDeadCondition())
          this.BuffAttachments.RemoveAt(index--);
        else if (buffAttachment.CheckTiming != EffectCheckTimings.Eternal && (int) buffAttachment.turn <= 0)
          this.BuffAttachments.RemoveAt(index--);
      }
    }

    public bool IsUnitCondition(EUnitCondition condition)
    {
      return ((EUnitCondition) (int) this.mCurrentCondition & condition) != (EUnitCondition) 0;
    }

    public bool IsDisableUnitCondition(EUnitCondition condition)
    {
      return ((EUnitCondition) (int) this.mDisableCondition & condition) != (EUnitCondition) 0;
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
      switch (condition)
      {
        case EUnitCondition.Stone:
        case EUnitCondition.Zombie:
        case EUnitCondition.DeathSentence:
        case EUnitCondition.DisableKnockback:
        case EUnitCondition.GoodSleep:
          return false;
        default:
          return true;
      }
    }

    public bool IsClockCureCondition(EUnitCondition condition)
    {
      switch (condition)
      {
        case EUnitCondition.Stop:
        case EUnitCondition.Fast:
        case EUnitCondition.Slow:
          return true;
        default:
          return false;
      }
    }

    public bool IsUnitConditionDamage()
    {
      return this.IsUnitCondition(EUnitCondition.Poison);
    }

    public bool IsPassiveUnitCondition(EUnitCondition condition)
    {
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (condAttachment != null && (bool) condAttachment.IsPassive && (condAttachment.IsFailCondition() && condAttachment.Condition == condition))
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
      else if (cond.CondType == ConditionEffectTypes.DisableCondition && cond.CheckTiming != EffectCheckTimings.Eternal && (!(bool) cond.IsPassive && (int) cond.turn == 0))
        cond.turn = fixParam.DefaultCondTurns[cond.Condition];
      if (this.IsBreakObj && (!(bool) cond.IsPassive || cond.user != this))
        return false;
      SkillData skill = cond.skill;
      if (skill != null && this.mCastSkill != null && (this.mCastSkill != skill && this.mCastSkill.CastType == ECastTypes.Jump))
        return false;
      EUnitCondition condition = cond.Condition;
      switch (cond.CondType)
      {
        case ConditionEffectTypes.CureCondition:
          this.CureCondEffects(condition, true, false);
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
                  CondEffect condEffect1 = cond.skill.GetCondEffect(SkillEffectTargets.Target);
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
                if (!flag)
                  num = (int) fixParam.DefaultDeathCount;
                if ((int) this.mDeathCount > 0)
                  this.mDeathCount = (OInt) Math.Min((int) this.mDeathCount, num);
                if (this.IsUnitCondition(EUnitCondition.DeathSentence))
                  return false;
                this.mDeathCount = (OInt) num;
              }
              if ((condition & EUnitCondition.Fast) != (EUnitCondition) 0)
                this.CureCondEffects(EUnitCondition.Slow, true, false);
              if ((condition & EUnitCondition.Slow) != (EUnitCondition) 0)
                this.CureCondEffects(EUnitCondition.Fast, true, false);
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

    public void CureCondEffects(EUnitCondition target, bool updated = true, bool forced = false)
    {
      if (this.CheckCancelSkillCureCondition(target))
        this.CancelCastSkill();
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (!(bool) condAttachment.IsPassive && (condAttachment.UseCondition == ESkillCondition.None || condAttachment.UseCondition == ESkillCondition.Weather) && ((!condAttachment.IsCurse || forced) && condAttachment.Condition == target))
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
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      for (int index = 0; index < this.CondAttachments.Count; ++index)
      {
        CondAttachment condAttachment = this.CondAttachments[index];
        if (!(bool) condAttachment.IsPassive && condAttachment.CheckTiming != EffectCheckTimings.Eternal)
        {
          if (condAttachment.IsCurse)
          {
            if (condAttachment.user != null && condAttachment.user.IsDead)
              this.CondAttachments.RemoveAt(index--);
          }
          else if ((int) condAttachment.turn <= 0 && (!condAttachment.IsFailCondition() || this.IsAutoCureCondition(condAttachment.Condition)))
            this.CondAttachments.RemoveAt(index--);
        }
      }
      for (int index1 = 0; index1 < this.CondAttachments.Count; ++index1)
      {
        CondAttachment condAttachment = this.CondAttachments[index1];
        if (condAttachment.UseCondition == ESkillCondition.None || condAttachment.UseCondition != ESkillCondition.Dying || this.IsDying())
        {
          for (int index2 = 0; index2 < (int) Unit.MAX_UNIT_CONDITION; ++index2)
          {
            EUnitCondition eunitCondition = (EUnitCondition) (1 << index2);
            if (condAttachment.Condition == eunitCondition)
            {
              switch (condAttachment.CondType)
              {
                case ConditionEffectTypes.FailCondition:
                case ConditionEffectTypes.ForcedFailCondition:
                case ConditionEffectTypes.RandomFailCondition:
                  if (condAttachment.IsCurse)
                    num3 |= 1 << index2;
                  num1 |= 1 << index2;
                  continue;
                case ConditionEffectTypes.DisableCondition:
                  num2 |= 1 << index2;
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
      this.mCurrentCondition = (OInt) 0;
      this.mDisableCondition = (OInt) 0;
      for (int index = 0; index < (int) Unit.MAX_UNIT_CONDITION; ++index)
      {
        int num4 = 1 << index;
        if ((num2 & num4) != 0)
        {
          this.CureCondEffects((EUnitCondition) num4, false, false);
          if ((num3 & num4) == 0)
            continue;
        }
        if ((num1 & num4) != 0)
        {
          Unit unit = this;
          unit.mCurrentCondition = (OInt) ((int) unit.mCurrentCondition | num4);
        }
      }
      this.mDisableCondition = (OInt) num2;
      if (!this.IsUnitCondition(EUnitCondition.DeathSentence))
        this.mDeathCount = (OInt) -1;
      if (!this.IsUnitCondition(EUnitCondition.Rage))
        this.mRageTarget = (Unit) null;
      if (!this.IsUnitCondition(EUnitCondition.Paralysed))
        this.SetUnitFlag(EUnitFlag.Paralysed, false);
      if (!this.IsUnitCondition(EUnitCondition.Stone))
        return;
      this.mCurrentCondition = (OInt) 32;
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
              if (this.mCastSkill != null && this.mCastSkill.CastType == ECastTypes.Jump && condAttachment.CheckTiming == EffectCheckTimings.ActionStart || (this.IsUnitCondition(EUnitCondition.Stone) && condition != EUnitCondition.Stone || !this.IsAutoCureCondition(condition)))
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
      this.mDeathCount = (OInt) Math.Max((int) (--this.mDeathCount), 0);
    }

    public bool CheckEventTrigger(EEventTrigger trigger)
    {
      if (this.mEventTrigger != null && this.mEventTrigger.Count > 0)
        return this.mEventTrigger.Trigger == trigger;
      return false;
    }

    public void DecrementTriggerCount()
    {
      if (this.mEventTrigger == null)
        return;
      this.mEventTrigger.Count = Math.Min(--this.mEventTrigger.Count, 0);
    }

    public bool CheckWinEventTrigger()
    {
      if (this.mEventTrigger != null && this.mEventTrigger.EventType == EEventType.Win)
      {
        if (this.mEventTrigger.Trigger == EEventTrigger.Dead)
          return this.IsDead;
        if (this.mEventTrigger.Trigger == EEventTrigger.HpDownBorder)
          return (int) this.MaximumStatus.param.hp * this.mEventTrigger.IntValue / 100 >= (int) this.CurrentStatus.param.hp;
      }
      return false;
    }

    public bool CheckLoseEventTrigger()
    {
      if (this.mEventTrigger != null && this.mEventTrigger.EventType == EEventType.Lose)
      {
        if (this.mEventTrigger.Trigger == EEventTrigger.Dead)
          return this.IsDead;
        if (this.mEventTrigger.Trigger == EEventTrigger.HpDownBorder)
          return (int) this.MaximumStatus.param.hp * this.mEventTrigger.IntValue / 100 >= (int) this.CurrentStatus.param.hp;
      }
      return false;
    }

    public bool CheckNeedEscaped()
    {
      return this.AI != null && (int) this.AI.escape_border != 0 && (!this.IsUnitCondition(EUnitCondition.DisableHeal) && (int) this.MaximumStatus.param.hp != 0) && (int) this.MaximumStatus.param.hp * (int) this.AI.escape_border >= (int) this.CurrentStatus.param.hp * 100;
    }

    public bool CheckEnableEntry()
    {
      if (this.IsDead)
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
      if ((bool) this.IsEntryTriggerAndCheck)
        return flag;
      return false;
    }

    public void UpdateClockTime()
    {
      if (!this.IsEntry)
        this.mWaitEntryClock = (OInt) Math.Max((int) (--this.mWaitEntryClock), 0);
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
              CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
              if (condEffect1 != null)
                val1 = Math.Max(val1, Math.Abs((int) condEffect1.param.v_fast));
              CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
              if (condEffect2 != null)
                val1 = Math.Max(val1, Math.Abs((int) condEffect2.param.v_fast));
            }
            else
              val1 = Math.Abs((int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.DefaultClockUpValue);
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
              CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
              if (condEffect1 != null)
                val1 = Math.Max(val1, Math.Abs((int) condEffect1.param.v_slow));
              CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
              if (condEffect2 != null)
                val1 = Math.Max(val1, Math.Abs((int) condEffect2.param.v_slow));
            }
            else
              val1 = Math.Abs((int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.DefaultClockDownValue);
          }
        }
        if (val1 == 0)
          val1 = Math.Abs((int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.DefaultClockDownValue);
        spd -= spd * val1 / 100;
      }
      return (OInt) spd;
    }

    public void UpdateChargeTime()
    {
      if (this.IsUnitCondition(EUnitCondition.Stop))
        return;
      Unit unit = this;
      unit.mChargeTime = (OInt) ((int) unit.mChargeTime + (int) this.GetChargeSpeed());
    }

    public bool CheckChargeTimeFullOver()
    {
      if (this.IsUnitCondition(EUnitCondition.Stop))
        return false;
      return (int) this.mChargeTime >= (int) this.ChargeTimeMax;
    }

    public OInt GetCastSpeed()
    {
      return this.GetCastSpeed(this.mCastSkill);
    }

    public OInt GetCastSpeed(SkillData skill)
    {
      if (this.IsUnitCondition(EUnitCondition.Stop))
        return (OInt) 0;
      int castSpeed = (int) skill.CastSpeed;
      if (this.IsUnitCondition(EUnitCondition.Fast))
        castSpeed += castSpeed * 50 / 100;
      if (this.IsUnitCondition(EUnitCondition.Slow))
        castSpeed -= castSpeed * 50 / 100;
      return (OInt) castSpeed;
    }

    public void UpdateCastTime()
    {
      if (this.mCastSkill == null || this.IsUnitCondition(EUnitCondition.Stop))
        return;
      Unit unit = this;
      unit.mCastTime = (OInt) ((int) unit.mCastTime + (int) this.GetCastSpeed());
    }

    public bool CheckCastTimeFullOver()
    {
      if (this.IsUnitCondition(EUnitCondition.Stop) || this.CastSkill == null)
        return false;
      return (int) this.mCastTime >= (int) this.CastTimeMax;
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

    public void SetCastTime(int time)
    {
      this.mCastTime = (OInt) time;
    }

    public void NotifyContinue()
    {
      if ((int) this.CurrentStatus.param.hp > (int) GlobalVars.MaxHpInBattle && this.mSide == EUnitSide.Player)
        GlobalVars.MaxHpInBattle = this.CurrentStatus.param.hp;
      this.CurrentStatus.param.hp = this.MaximumStatus.param.hp;
      this.CurrentStatus.param.mp = this.MaximumStatus.param.mp;
      if ((int) this.CurrentStatus.param.hp > (int) GlobalVars.MaxHpInBattle && this.mSide == EUnitSide.Player)
        GlobalVars.MaxHpInBattle = this.CurrentStatus.param.hp;
      this.CancelCastSkill();
      this.CancelGuradTarget();
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
      this.SetUnitFlag(EUnitFlag.UnitWithdraw, false);
      this.SetUnitFlag(EUnitFlag.Reinforcement, false);
      this.SetUnitFlag(EUnitFlag.ToDying, false);
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
      this.CalcCurrentStatus(false);
      this.ClearSkillUseCount();
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
          other.setRelatedBuff(other.x, other.y, false);
          TrickData.MomentBuff(other, other.x, other.y, EffectCheckTimings.Moment);
          TrickData.MomentBuff(other, other.x, other.y, EffectCheckTimings.MomentStart);
          other.CalcCurrentStatus(false);
        }
      }
      this.UpdateShieldTurn();
      this.UpdateGuardTurn();
      this.UpdateDeathSentence();
      if (this.mRageTarget != null && this.mRageTarget.IsDeadCondition())
        this.CureCondEffects(EUnitCondition.Rage, true, true);
      this.startX = this.x;
      this.startY = this.y;
      this.startDir = this.Direction;
      if (!this.IsDead && this.IsEntry && !this.IsUnitFlag(EUnitFlag.FirstAction))
      {
        if (this.IsPartyMember && !this.IsUnitFlag(EUnitFlag.DisableFirstVoice))
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
      this.mMoveWaitTurn = (OInt) Math.Max((int) (--this.mMoveWaitTurn), 0);
      ++this.mActionCount;
      if (this.mAIActionTable == null || this.mAIActionTable.actions == null || (this.mAIActionTable.actions.Count <= 0 || this.mAIActionTable.actions.Count <= (int) this.mAIActionIndex))
        return;
      ++this.mAIActionTurnCount;
    }

    public void NotifyActionEnd()
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
        Unit unit2 = this;
        unit2.mChargeTime = (OInt) ((int) unit2.mChargeTime - (int) fixParam.ChargeTimeDecAction);
      }
      this.mChargeTime = (OInt) Math.Max((int) this.mChargeTime, 0);
      this.CalcCurrentStatus(false);
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
        other.setRelatedBuff(other.x, other.y, false);
        TrickData.MomentBuff(other, other.x, other.y, EffectCheckTimings.Moment);
        other.CalcCurrentStatus(false);
      }
    }

    public void NotifyCommandStart()
    {
      if (this.mUnitData.Lv > (int) GlobalVars.MaxLevelInBattle && this.mSide == EUnitSide.Player)
        GlobalVars.MaxLevelInBattle = (OInt) this.mUnitData.Lv;
      this.CalcCurrentStatus(false);
    }

    public void RefleshMomentBuff(List<Unit> others, bool is_direct = false, int grid_x = -1, int grid_y = -1)
    {
      if (others == null)
        return;
      using (List<Unit>.Enumerator enumerator = others.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Unit current = enumerator.Current;
          int grid_x1 = current.x;
          int grid_y1 = current.y;
          if (is_direct && current == this)
          {
            if (grid_x >= 0)
              grid_x1 = grid_x;
            if (grid_y >= 0)
              grid_y1 = grid_y;
          }
          current.UpdateBuffEffectTurnCount(EffectCheckTimings.Moment, this);
          current.UpdateBuffEffects();
          current.setRelatedBuff(grid_x1, grid_y1, is_direct);
          TrickData.MomentBuff(current, grid_x1, grid_y1, EffectCheckTimings.Moment);
          current.CalcCurrentStatus(false);
        }
      }
    }

    public void RefleshMomentBuff(bool is_direct = false, int grid_x = -1, int grid_y = -1)
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
      this.UpdateBuffEffects();
      this.setRelatedBuff(grid_x1, grid_y1, is_direct);
      TrickData.MomentBuff(this, grid_x1, grid_y1, EffectCheckTimings.Moment);
      this.CalcCurrentStatus(false);
    }

    public bool CheckEnemySide(Unit target)
    {
      if (this == target)
        return false;
      if (!this.IsUnitCondition(EUnitCondition.Charm) && !this.IsUnitCondition(EUnitCondition.Zombie))
        return this.Side != target.Side;
      if (target.IsUnitCondition(EUnitCondition.Charm) || target.IsUnitCondition(EUnitCondition.Zombie))
        return false;
      return this.Side == target.Side;
    }

    public Unit GetRageTarget()
    {
      if (this.IsUnitCondition(EUnitCondition.Rage))
        return this.mRageTarget;
      return (Unit) null;
    }

    public SkillData GetAttackSkill()
    {
      return this.UnitData.GetAttackSkill();
    }

    public void Heal(int value)
    {
      if ((int) this.CurrentStatus.param.hp > (int) GlobalVars.MaxHpInBattle && this.mSide == EUnitSide.Player)
        GlobalVars.MaxHpInBattle = this.CurrentStatus.param.hp;
      this.CurrentStatus.param.hp = (OInt) Math.Min((int) this.CurrentStatus.param.hp + value, (int) this.MaximumStatus.param.hp);
      if ((int) this.CurrentStatus.param.hp <= (int) GlobalVars.MaxHpInBattle || this.mSide != EUnitSide.Player)
        return;
      GlobalVars.MaxHpInBattle = this.CurrentStatus.param.hp;
    }

    public void Damage(int value, bool is_check_dying = false)
    {
      if (value > (int) GlobalVars.MaxDamageInBattle && this.mSide == EUnitSide.Player)
        GlobalVars.MaxDamageInBattle = (OInt) value;
      bool flag = this.IsDying();
      this.CurrentStatus.param.hp = (OInt) Math.Max((int) this.CurrentStatus.param.hp - value, 0);
      if (is_check_dying && !flag && this.IsDying())
        this.SetUnitFlag(EUnitFlag.ToDying, true);
      if ((int) this.CurrentStatus.param.hp <= (int) GlobalVars.MaxHpInBattle || this.mSide != EUnitSide.Player)
        return;
      GlobalVars.MaxHpInBattle = this.CurrentStatus.param.hp;
    }

    public void ForceDead()
    {
      this.CurrentStatus.param.hp = (OInt) 0;
      this.mDeathCount = (OInt) 0;
      this.mChargeTime = (OInt) 0;
      this.CancelCastSkill();
      this.CancelGuradTarget();
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

    public void SetDeathCount(int count)
    {
      this.mDeathCount = (OInt) count;
    }

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
            CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
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
            CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
            if (condEffect1 != null && (int) condEffect1.param.v_paralyse_rate != 0)
              num = Math.Max(num, (int) condEffect1.param.v_paralyse_rate);
            CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
            if (condEffect2 != null && (int) condEffect2.param.v_paralyse_rate != 0)
              num = Math.Max(num, (int) condEffect2.param.v_paralyse_rate);
          }
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

    public int GetAttackRangeMin()
    {
      return this.GetAttackRangeMin(this.GetAttackSkill());
    }

    public int GetAttackRangeMin(SkillData skill)
    {
      if (skill == null || skill.SkillParam.select_range == ESelectType.All)
        return 0;
      return this.UnitData.GetAttackRangeMin(skill);
    }

    public int GetAttackRangeMax()
    {
      return this.GetAttackRangeMax(this.GetAttackSkill());
    }

    public int GetAttackRangeMax(SkillData skill)
    {
      if (skill == null)
        return 0;
      int num = this.UnitData.GetAttackRangeMax(skill);
      if (skill.IsEnableChangeRange())
        num += (int) this.mCurrentStatus[BattleBonus.EffectRange];
      if (skill.SkillParam.select_range == ESelectType.All)
        num = 99;
      return num;
    }

    public int GetAttackScope()
    {
      return this.GetAttackScope(this.GetAttackSkill());
    }

    public int GetAttackScope(SkillData skill)
    {
      if (skill == null)
        return 0;
      int attackScope = this.UnitData.GetAttackScope(skill);
      if (skill.IsEnableChangeRange())
        attackScope += (int) this.mCurrentStatus[BattleBonus.EffectScope];
      return attackScope;
    }

    public int GetAttackHeight()
    {
      return this.GetAttackHeight(this.GetAttackSkill(), false);
    }

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
            CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
            if (condEffect1 != null && (int) condEffect1.param.v_donmov > 0)
              num = Math.Min(num, (int) condEffect1.param.v_donmov);
            CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
            if (condEffect2 != null && (int) condEffect2.param.v_donmov > 0)
              num = Math.Min(num, (int) condEffect2.param.v_donmov);
          }
          if (num == (int) byte.MaxValue)
            num = 1;
          val1 = Math.Min(val1, num);
          flag = true;
        }
      }
      if (flag)
        return val1;
      return 1;
    }

    public int GetMoveHeight()
    {
      return Math.Max((int) this.mCurrentStatus.param.jmp, 1);
    }

    public void SetSearchRange(int value)
    {
      this.mSearched = (OInt) value;
    }

    public int GetSearchRange()
    {
      return (int) this.mSearched;
    }

    public int GetCombination()
    {
      return this.UnitData.GetCombination();
    }

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
      return (SkillData) null;
    }

    public int GetSkillUsedCost(SkillData skill)
    {
      int num = skill.Cost;
      if (skill.EffectType != SkillEffectTypes.GemsGift && !skill.IsNormalAttack() && !skill.IsItem())
        num = Math.Max(num + num * (int) this.CurrentStatus[BattleBonus.UsedJewelRate] / 100 + (int) this.CurrentStatus[BattleBonus.UsedJewel], 0);
      return num;
    }

    public EElement GetWeakElement()
    {
      return UnitParam.GetWeakElement(this.Element);
    }

    public int GetAutoHpHealValue()
    {
      if (this.IsUnitCondition(EUnitCondition.DisableHeal))
        return 0;
      int num1 = 0;
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
            int num2 = 0;
            if (condAttachment.skill != null)
            {
              CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
              if (condEffect1 != null)
              {
                if ((int) condEffect1.param.v_auto_hp_heal > 0)
                  num2 = Math.Max(num2, (int) this.MaximumStatus.param.hp * (int) condEffect1.param.v_auto_hp_heal / 100);
                if ((int) condEffect1.param.v_auto_hp_heal_fix > 0)
                  num2 = Math.Max(num2, (int) condEffect1.param.v_auto_hp_heal_fix);
              }
              CondEffect condEffect2 = condAttachment.user != this ? (CondEffect) null : condAttachment.skill.GetCondEffect(SkillEffectTargets.Self);
              if (condEffect2 != null)
              {
                if ((int) condEffect2.param.v_auto_hp_heal > 0)
                  num2 = Math.Max(num2, (int) this.MaximumStatus.param.hp * (int) condEffect2.param.v_auto_hp_heal / 100);
                if ((int) condEffect2.param.v_auto_hp_heal_fix > 0)
                  num2 = Math.Max(num2, (int) condEffect2.param.v_auto_hp_heal_fix);
              }
            }
            if (num2 == 0)
              num2 = (int) this.MaximumStatus.param.hp * (int) fixParam.HpAutoHealRate / 100;
            flag = true;
            val1 = Math.Max(val1, num2);
          }
        }
        if (!flag)
          val1 = (int) this.MaximumStatus.param.hp * (int) fixParam.HpAutoHealRate / 100;
        num1 += val1;
      }
      if (this.IsUnitCondition(EUnitCondition.Sleep) && this.IsUnitCondition(EUnitCondition.GoodSleep))
        num1 += this.GetGoodSleepHpHealValue();
      return num1;
    }

    public bool CheckAutoHpHeal()
    {
      return this.GetAutoHpHealValue() > 0;
    }

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
              CondEffect condEffect1 = condAttachment.skill.GetCondEffect(SkillEffectTargets.Target);
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

    public bool CheckAutoMpHeal()
    {
      return this.GetAutoMpHealValue() > 0;
    }

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
      if (waitDirection)
      {
        if (!this.IsGimmick)
        {
          SceneBattle instance = SceneBattle.Instance;
          if ((UnityEngine.Object) instance != (UnityEngine.Object) null && (UnityEngine.Object) instance.FindUnitController(this) != (UnityEngine.Object) null)
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
      if ((int) this.mAIActionTurnCount < (int) this.mAIActionTable.actions[(int) this.mAIActionIndex].turn)
        return (AIAction) null;
      return this.mAIActionTable.actions[(int) this.mAIActionIndex];
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

    public bool IsAIPatrolTable()
    {
      return this.mAIPatrolTable != null && this.mAIPatrolTable.routes != null && ((int) this.mAIPatrolIndex >= 0 && this.mAIPatrolTable.routes.Length > (int) this.mAIPatrolIndex) && (this.mAIPatrolTable.keeped != 0 || !this.IsUnitFlag(EUnitFlag.Searched));
    }

    public AIPatrolPoint GetCurrentPatrolPoint()
    {
      if (!this.IsAIPatrolTable())
        return (AIPatrolPoint) null;
      return this.mAIPatrolTable.routes[(int) this.mAIPatrolIndex];
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
          val2 = Math.Max(Math.Abs((int) buffAttachment.status[param]), val2);
      }
      return val2;
    }

    public void GetEnableBetterBuffEffect(Unit self, SkillData skill, SkillEffectTargets effect_target, out int buff_count, out int buff_value, bool bRequestOnly = false)
    {
      buff_count = 0;
      buff_value = 0;
      BuffEffect buffEffect = skill.GetBuffEffect(effect_target);
      if (buffEffect != null && buffEffect.param != null && buffEffect.param.buffs != null)
      {
        for (int index1 = 0; index1 < buffEffect.targets.Count; ++index1)
        {
          BuffEffect.BuffTarget target = buffEffect.targets[index1];
          if (target.buffType == BuffTypes.Buff)
          {
            if (this.IsUnitCondition(EUnitCondition.DisableBuff) || Math.Max(100 - (int) this.CurrentStatus.enchant_resist.resist_buff, 0) <= (self == null || self.AI == null ? 0 : (int) self.AI.buff_border))
              continue;
          }
          else if (target.buffType == BuffTypes.Debuff && (this.IsUnitCondition(EUnitCondition.DisableDebuff) || Math.Max(100 - (int) this.CurrentStatus.enchant_resist.resist_debuff, 0) <= (self == null || self.AI == null ? 0 : (int) self.AI.buff_border)))
            continue;
          if (this.BuffAttachments.Find((Predicate<BuffAttachment>) (p => p.skill == skill)) == null)
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
      string skinVoiceSheetName = this.GetUnitSkinVoiceSheetName(-1);
      if (string.IsNullOrEmpty(skinVoiceSheetName))
        return;
      string sheetName = "VO_" + skinVoiceSheetName;
      string cueNamePrefix = this.GetUnitSkinVoiceCueName(-1) + "_";
      this.mBattleVoice = new MySound.Voice(sheetName, skinVoiceSheetName, cueNamePrefix, false);
    }

    public void PlayBattleVoice(string cueID)
    {
      if (this.mBattleVoice == null)
        return;
      this.mBattleVoice.Play(cueID, 0.0f, false);
    }

    public int CalcTowerDamege()
    {
      return this.mTowerStartHP - (int) this.CurrentStatus.param.hp;
    }

    public Unit GetUnitUseCollaboSkill(SkillData skill, bool is_use_tuc = false)
    {
      int x = this.x;
      int y = this.y;
      if (is_use_tuc)
      {
        SceneBattle instance = SceneBattle.Instance;
        if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
          return (Unit) null;
        TacticsUnitController unitController = instance.FindUnitController(this);
        if ((UnityEngine.Object) unitController != (UnityEngine.Object) null && !this.IsUnitFlag(EUnitFlag.Moved))
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
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return (Unit) null;
      BattleCore battle = instance.Battle;
      if (battle == null)
        return (Unit) null;
      if (battle.CurrentMap == null)
        return (Unit) null;
      Grid current1 = battle.CurrentMap[ux, uy];
      for (int index1 = current1.y - 1; index1 <= current1.y + 1; ++index1)
      {
        for (int index2 = current1.x - 1; index2 <= current1.x + 1; ++index2)
        {
          if (index2 != current1.x || index1 != current1.y)
          {
            Grid current2 = battle.CurrentMap[index2, index1];
            if (current2 != null)
            {
              Unit unitAtGrid = battle.FindUnitAtGrid(current2);
              if (unitAtGrid != null && unitAtGrid.mSide == this.mSide && (!(unitAtGrid.UnitParam.iname != partnerIname) && unitAtGrid.GetSkillUseCount(skill.SkillParam.iname) != null) && Math.Abs(current1.height - current2.height) <= (int) skill.SkillParam.CollaboHeight)
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
      if (unitUseCollaboSkill == null)
        return false;
      return unitUseCollaboSkill.Gems - unitUseCollaboSkill.GetSkillUsedCost(skill) >= 0;
    }

    public SkillData GetSkillUseCount(string skill_iname)
    {
      using (Dictionary<SkillData, OInt>.KeyCollection.Enumerator enumerator = this.mSkillUseCount.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SkillData current = enumerator.Current;
          if (current != null && current.SkillParam.iname == skill_iname)
            return current;
        }
      }
      return (SkillData) null;
    }

    public bool IsNormalSize
    {
      get
      {
        return this.SizeX == 1 && this.SizeY == 1;
      }
    }

    public bool IsThrow
    {
      get
      {
        return this.mUnitData.IsThrow;
      }
    }

    private void setRelatedBuff(int grid_x, int grid_y, bool is_direct = false)
    {
      SceneBattle instance = SceneBattle.Instance;
      if (!(bool) ((UnityEngine.Object) instance))
        return;
      BattleCore battle = instance.Battle;
      if (battle == null || battle.CurrentMap == null)
        return;
      BattleMap currentMap = battle.CurrentMap;
      if (currentMap == null)
        return;
      List<Unit> unitList = new List<Unit>(battle.Units.Count);
      for (int index = 0; index < 4; ++index)
      {
        Grid grid = currentMap[grid_x + Unit.DIRECTION_OFFSETS[index, 0], grid_y + Unit.DIRECTION_OFFSETS[index, 1]];
        if (grid != null)
        {
          Unit unit = !is_direct ? battle.FindUnitAtGrid(grid) : battle.DirectFindUnitAtGrid(grid);
          if (unit != null && unit != this)
          {
            using (List<BuffAttachment>.Enumerator enumerator = unit.BuffAttachments.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                BuffAttachment current = enumerator.Current;
                if (current.CheckTiming != EffectCheckTimings.Moment && current.Param != null && (current.Param.mEffRange == EEffRange.SelfNearAlly && unit.mSide == this.mSide))
                {
                  unitList.Add(unit);
                  break;
                }
              }
            }
          }
        }
      }
      if (unitList.Count == 0)
        return;
      using (List<Unit>.Enumerator enumerator1 = unitList.GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          Unit current1 = enumerator1.Current;
          using (List<BuffAttachment>.Enumerator enumerator2 = current1.BuffAttachments.GetEnumerator())
          {
            while (enumerator2.MoveNext())
            {
              BuffAttachment current2 = enumerator2.Current;
              if (current2.CheckTiming != EffectCheckTimings.Moment && current2.Param != null && (current2.Param.mEffRange == EEffRange.SelfNearAlly && current1.mSide == this.mSide))
              {
                BuffAttachment buffAttachment = new BuffAttachment();
                current2.CopyTo(buffAttachment);
                buffAttachment.CheckTiming = EffectCheckTimings.Moment;
                buffAttachment.IsPassive = (OBool) false;
                buffAttachment.turn = (OInt) 1;
                bool flag = false;
                using (List<BuffAttachment>.Enumerator enumerator3 = this.BuffAttachments.GetEnumerator())
                {
                  while (enumerator3.MoveNext())
                  {
                    BuffAttachment current3 = enumerator3.Current;
                    if (this.isSameBuffAttachment(current3, buffAttachment) && current3.user == buffAttachment.user)
                    {
                      flag = true;
                      break;
                    }
                  }
                }
                if (!flag)
                  this.SetBuffAttachment(buffAttachment, false);
              }
            }
          }
        }
      }
    }

    public int GetAllyUnitNum(Unit target_unit)
    {
      if (target_unit == null || target_unit.IsDead)
        return 0;
      SceneBattle instance = SceneBattle.Instance;
      if (!(bool) ((UnityEngine.Object) instance))
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
        if ((bool) ((UnityEngine.Object) unitController))
        {
          IntVector2 intVector2 = instance.CalcCoord(unitController.CenterPosition);
          x = intVector2.x;
          y = intVector2.y;
        }
      }
      int num = 0;
      for (int index = 0; index < 4; ++index)
      {
        Grid grid = currentMap[x + Unit.DIRECTION_OFFSETS[index, 0], y + Unit.DIRECTION_OFFSETS[index, 1]];
        if (grid != null)
        {
          Unit unit = !battle.IsBattleFlag(EBattleFlag.ComputeAI) ? battle.DirectFindUnitAtGrid(grid) : battle.FindUnitAtGrid(grid);
          if (unit != null && unit.mSide == target_unit.mSide)
            ++num;
        }
      }
      return num;
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

    public void BeginDropDirection()
    {
      this.mDropDirection = true;
    }

    public void EndDropDirection()
    {
      this.mDropDirection = false;
    }

    public bool IsDropDirection()
    {
      return this.mDropDirection;
    }

    public bool IsKnockBack
    {
      get
      {
        return this.mUnitData.IsKnockBack;
      }
    }

    public class DropItem
    {
      public ItemParam param;
      public OInt num;
      public OBool is_secret;
    }

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
        if ((int) this.exp <= 0 && (int) this.gems <= 0 && (int) this.gold <= 0)
          return this.items.Count > 0;
        return true;
      }

      public void Drop()
      {
        this.gained = true;
      }

      public void CopyTo(Unit.UnitDrop other)
      {
        other.exp = this.exp;
        other.gems = this.gems;
        other.gold = this.gold;
        other.gained = this.gained;
        other.items.Clear();
        for (int index = 0; index < this.items.Count; ++index)
        {
          if (this.items[index] != null && this.items[index].param != null && (int) this.items[index].num != 0)
            other.items.Add(new Unit.DropItem()
            {
              param = this.items[index].param,
              num = this.items[index].num
            });
        }
      }
    }

    public class UnitSteal
    {
      public List<Unit.DropItem> items = new List<Unit.DropItem>();
      public OInt gold;
      public bool is_item_steeled;
      public bool is_gold_steeled;

      public bool IsEnableItemSteal()
      {
        if (!this.is_item_steeled)
          return this.items.Count > 0;
        return false;
      }

      public bool IsEnableGoldSteal()
      {
        return !this.is_gold_steeled;
      }

      public void CopyTo(Unit.UnitSteal other)
      {
        other.gold = this.gold;
        other.is_item_steeled = this.is_item_steeled;
        other.is_gold_steeled = this.is_gold_steeled;
        other.items.Clear();
        for (int index = 0; index < this.items.Count; ++index)
        {
          if (this.items[index] != null && this.items[index].param != null && (int) this.items[index].num != 0)
            other.items.Add(new Unit.DropItem()
            {
              param = this.items[index].param,
              num = this.items[index].num
            });
        }
      }
    }

    public class UnitShield
    {
      public OBool is_efficacy = (OBool) false;
      public ShieldTypes shieldType;
      public DamageTypes damageType;
      public OInt hp;
      public OInt hpMax;
      public OInt turn;
      public OInt turnMax;
      public OInt damage_rate;
      public OInt damage_value;
      public SkillParam skill_param;

      public void CopyTo(Unit.UnitShield other)
      {
        other.shieldType = this.shieldType;
        other.damageType = this.damageType;
        other.hp = this.hp;
        other.hpMax = this.hpMax;
        other.turn = this.turn;
        other.turnMax = this.turnMax;
        other.damage_rate = this.damage_rate;
        other.damage_value = this.damage_value;
        other.skill_param = this.skill_param;
        other.is_efficacy = this.is_efficacy;
      }
    }
  }
}
