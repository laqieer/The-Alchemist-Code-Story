﻿// Decompiled with JetBrains decompiler
// Type: SRPG.TacticsUnitController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using SRPG.AnimEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class TacticsUnitController : UnitController
  {
    public static List<TacticsUnitController> Instances = new List<TacticsUnitController>(16);
    public static readonly string ANIM_IDLE_FIELD = "idlefield0";
    public static readonly string ANIM_IDLE_DEMO = "cmn_demo_talk_idle0";
    public static readonly string ANIM_RUN_FIELD = "runfield0";
    public static readonly string ANIM_STEP = "cmn_step0";
    public static readonly string ANIM_FALL_LOOP = "cmn_fallloop0";
    public static readonly string ANIM_FALL_END = "cmn_fallland0";
    public static readonly string ANIM_CLIMBUP = "cmn_jumploop0";
    public static readonly string ANIM_PICKUP = "cmn_pickup0";
    public static readonly string ANIM_GENKIDAMA = "cmn_freeze0";
    private static string CameraAnimationDir = "Camera/";
    public Color ColorMod = Color.white;
    [NonSerialized]
    public int PlayerMemberIndex = -1;
    public bool AutoUpdateRotation = true;
    private Dictionary<string, GameObject> mDefendSkillEffects = new Dictionary<string, GameObject>();
    private float mRunSpeed = 4f;
    private Vector3 mVelocity = Vector3.zero;
    private bool mCollideGround = true;
    private float mSpinCount = 2f;
    public TacticsUnitController.EElementEffectTypes ShowElementEffectOnHit = TacticsUnitController.EElementEffectTypes.Normal;
    public List<TacticsUnitController.ShieldState> Shields = new List<TacticsUnitController.ShieldState>();
    private float mCastJumpOffsetY = 10f;
    private const string ID_IDLE = "IDLE";
    private const string ID_RUN = "RUN";
    private const string ID_STEP = "STEP";
    private const string ID_PICKUP = "PICK";
    private const string ID_BADSTATUS = "BAD";
    private const string ID_CUSTOMRUN = "RUN_";
    private const string ID_FALL_LOOP = "FLLP";
    private const string ID_FALL_END = "FLEN";
    private const string ID_CLIMBUP = "CLMB";
    private const string ID_GENKIDAMA = "GENK";
    public const float ADJUST_MOVE_DIST = 0.3f;
    private const string ID_BATTLE_RUN = "B_RUN";
    private const string ID_BATTLE_SKILL = "B_SKL";
    private const string ID_BATTLE_BACKSTEP = "B_BS";
    private const string ID_BATTLE_DAMAGE_NORMAL = "B_DMG0";
    private const string ID_BATTLE_DAMAGE_AIR = "B_DMG1";
    private const string ID_BATTLE_DEFEND = "B_DEF";
    private const string ID_BATTLE_DODGE = "B_DGE";
    private const string ID_BATTLE_DOWN = "B_DOWN";
    private const string ID_BATTLE_DYING = "B_DIE";
    private const string ID_BATTLE_CHANT = "B_CHA";
    private const string ID_BATTLE_RUNLOOP = "B_RUNL";
    private const string ID_BATTLE_DEAD = "B_DEAD";
    private const string ID_BATTLE_PRESKILL = "B_PRS";
    private const string ID_BATTLE_TOSS_LIFT = "B_TOSS_LIFT";
    private const string ID_BATTLE_TOSS_THROW = "B_TOSS_THROW";
    private const string ID_BATTLE_TRANSFORM = "B_TRANSFORM";
    public const string ANIM_BATTLE_TOSS_LIFT = "cmn_toss_lift0";
    public const string ANIM_BATTLE_TOSS_THROW = "cmn_toss_throw0";
    public const string COLLABO_SKILL_NAME_SUB = "_sub";
    private const float CameraInterpRate = 4f;
    private const float KNOCK_BACK_SEC = 0.4f;
    public const string TRANSFORM_SKILL_NAME_SUB = "_chg";
    public List<string> mCustomRunAnimations;
    private GameObject mRenkeiAuraEffect;
    private GameObject mDrainEffect;
    private GameObject mChargeGrnTargetUnitEffect;
    private GameObject mChargeRedTargetUnitEffect;
    private GameObject mVersusCursor;
    private Transform mVersusCursorRoot;
    public TacticsUnitController.PostureTypes Posture;
    private Texture2D mIconSmall;
    private Texture2D mIconMedium;
    private Texture2D mJobIcon;
    private string mRunAnimation;
    private int mCachedHP;
    private UnitGauge mHPGauge;
    private UnitGaugeMark mAddIconGauge;
    private float mKeepHPGaugeVisible;
    private ChargeIcon mChargeIcon;
    private DeathSentenceIcon mDeathSentenceIcon;
    private GameObject mOwnerIndexUI;
    private UnitGaugeMark.EMarkType mKeepUnitGaugeMarkType;
    private UnitGaugeMark.EGemIcon mKeepUnitGaugeMarkGemIconType;
    private TacticsUnitController.HideGimmickAnimation mHideGimmickAnim;
    public string UniqueName;
    private Unit mUnit;
    private Projector mShadow;
    public UnitCursor UnitCursorTemplate;
    private UnitCursor mUnitCursor;
    private StateMachine<TacticsUnitController> mStateMachine;
    private bool mCancelAction;
    private EUnitDirection mFieldActionDir;
    private Vector2 mFieldActionPoint;
    private Color mBlendColor;
    private TacticsUnitController.ColorBlendModes mBlendMode;
    private float mBlendColorTime;
    private float mBlendColorTimeMax;
    private bool mEnableColorBlending;
    private PetController mPet;
    private GameObject mPickupObject;
    private Vector3[] mRoute;
    private int mRoutePos;
    private GridMap<int> mWalkableField;
    public bool IgnoreMove;
    private float mPostMoveAngle;
    private float mIdleInterpTime;
    private bool mLoadedPartially;
    private Vector3 mLookAtTarget;
    private TacticsUnitController.FieldActionEndEvent mOnFieldActionEnd;
    private Vector3 mStepStart;
    private Vector3 mStepEnd;
    private float mMoveAnimInterpTime;
    private BadStatusEffects.Desc mBadStatus;
    private GameObject mBadStatusEffect;
    private string mLoadedBadStatusAnimation;
    private GameObject mCurseEffect;
    private string mLoadedCurseAnimation;
    private bool IsCursed;
    private int mBadStatusLocks;
    public int DrainGemsOnHit;
    public GemParticle[] GemDrainEffects;
    public GameObject GemDrainHitEffect;
    public bool ShowCriticalEffectOnHit;
    public bool ShowBackstabEffectOnHit;
    private TacticsUnitController.ShakeUnit mShaker;
    private float mMapTrajectoryHeight;
    private TacticsUnitController.SkillVars mSkillVars;
    public bool ShouldDodgeHits;
    public bool ShouldPerfectDodge;
    public bool ShouldDefendHits;
    private GameObject mLastHitEffect;
    private LogSkill.Target mHitInfo;
    private LogSkill.Target mHitInfoSelf;
    private bool mCastJumpStartComplete;
    private bool mCastJumpFallComplete;
    public Vector3 JumpFallPos;
    public IntVector2 JumpMapFallPos;
    private TacticsUnitController.eKnockBackMode mKnockBackMode;
    private Grid mKnockBackGrid;
    private Vector3 mKbPosStart;
    private Vector3 mKbPosEnd;
    private float mKbPassedSec;

    public void StartSkillWithAnimationOption(Vector3 targetPosition, UnityEngine.Camera activeCamera, TacticsUnitController[] targets, Vector3[] hitGrids, SkillParam skill, bool withAnimation = true)
    {
      this.mSkillVars.HitGrids = hitGrids;
      if (withAnimation)
        this.InternalStartSkill(targets, targetPosition, activeCamera, true);
      else
        this.InternalStartSkillWithoutAnimation(targets, targetPosition, activeCamera);
    }

    public void StartSkillWithAnimationOption(TacticsUnitController target, UnityEngine.Camera activeCamera, SkillParam skill, bool withAnimation = true)
    {
      if (withAnimation)
        this.InternalStartSkill(new TacticsUnitController[1]
        {
          target
        }, target.transform.position, activeCamera, true);
      else
        this.InternalStartSkillWithoutAnimation(new TacticsUnitController[1]
        {
          target
        }, target.transform.position, activeCamera);
    }

    private void InternalStartSkillWithoutAnimation(TacticsUnitController[] targets, Vector3 targetPosition, UnityEngine.Camera activeCamera)
    {
      if (this.mSkillVars.mSkillSequence == null)
      {
        UnityEngine.Debug.LogError((object) "SkillSequence not loaded yet");
      }
      else
      {
        this.mCollideGround = !this.mSkillVars.UseBattleScene;
        this.mSkillVars.mActiveCamera = activeCamera;
        this.mSkillVars.Targets = new List<TacticsUnitController>((IEnumerable<TacticsUnitController>) targets);
        if (this.mSkillVars.UseBattleScene)
          this.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
        this.mSkillVars.HitTimerThread = this.StartCoroutine(this.AsyncHitTimer());
        this.mSkillVars.mStartPosition = this.transform.position;
        if (targets.Length == 1)
        {
          this.mSkillVars.mTargetController = targets[0];
          this.mSkillVars.mTargetControllerPosition = this.mSkillVars.mTargetController.transform.position;
          this.mSkillVars.mTargetController.mCollideGround = this.mCollideGround;
        }
        this.mSkillVars.mTargetPosition = !this.mSkillVars.UseBattleScene ? targetPosition : this.mSkillVars.mTargetControllerPosition;
        this.mSkillVars.MaxPlayVoice = this.mSkillVars.NumPlayVoice = this.CountSkillUnitVoice();
        this.mSkillVars.TotalHits = this.mSkillVars.NumHitsLeft = this.CountSkillHits();
        this.mSkillVars.mAuraEnable = (UnityEngine.Object) this.mSkillVars.mSkillEffect.AuraEffect != (UnityEngine.Object) null;
        if (this.UseSubEquipment)
          this.SwitchEquipments();
        this.mSkillVars.mCameraID = 0;
        this.mSkillVars.mChantCameraID = 0;
        this.mSkillVars.mSkillCameraID = 0;
        this.GotoState<TacticsUnitController.State_Skill_Without_Animation>();
      }
    }

    public void ResetColorMod()
    {
      this.ColorMod = Color.white;
    }

    public void CacheIcons()
    {
      if (this.Unit == null)
        return;
      this.StartCoroutine(this.CacheIconsAsync());
    }

    [DebuggerHidden]
    private IEnumerator CacheIconsAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CCacheIconsAsync\u003Ec__Iterator2E() { \u003C\u003Ef__this = this };
    }

    public static TacticsUnitController FindByUnitID(string unitID)
    {
      for (int index = TacticsUnitController.Instances.Count - 1; index >= 0; --index)
      {
        if (TacticsUnitController.Instances[index].Unit != null && TacticsUnitController.Instances[index].Unit.UnitParam.iname == unitID)
          return TacticsUnitController.Instances[index];
      }
      return (TacticsUnitController) null;
    }

    public static TacticsUnitController FindByUniqueName(string uniqueName)
    {
      for (int index = TacticsUnitController.Instances.Count - 1; index >= 0; --index)
      {
        if (TacticsUnitController.Instances[index].UniqueName == uniqueName)
          return TacticsUnitController.Instances[index];
      }
      return (TacticsUnitController) null;
    }

    public void LoadRunAnimation(string animationName)
    {
      if (string.IsNullOrEmpty(animationName))
        return;
      this.LoadAnimationAsync("RUN_" + animationName, "CHM/" + animationName);
    }

    public void LoadRunAnimation(string animationName, string path)
    {
      if (string.IsNullOrEmpty(animationName))
        return;
      this.LoadAnimationAsync("RUN_" + animationName, path);
    }

    public void UnloadRunAnimation(string animationName)
    {
      this.UnloadAnimation("RUN_" + animationName);
    }

    public void SetRunAnimation(string animationName)
    {
      bool flag = false;
      string str = string.IsNullOrEmpty(animationName) ? "RUN" : "RUN_" + animationName;
      if (str == this.mRunAnimation)
        return;
      if (this.IsAnimationPlaying(this.mRunAnimation))
      {
        this.StopAnimation(this.mRunAnimation);
        flag = true;
      }
      this.mRunAnimation = str;
      if (!flag)
        return;
      this.PlayAnimation(this.mRunAnimation, true);
    }

    public int HPPercentage
    {
      get
      {
        return Mathf.Clamp((int) this.Unit.MaximumStatus.param.hp == 0 ? 100 : (int) this.Unit.CurrentStatus.param.hp * 100 / (int) this.Unit.MaximumStatus.param.hp, 0, 100);
      }
    }

    public int VisibleHPValue
    {
      get
      {
        return this.mCachedHP;
      }
    }

    public ChargeIcon ChargeIcon
    {
      get
      {
        return this.mChargeIcon;
      }
      set
      {
      }
    }

    public DeathSentenceIcon DeathSentenceIcon
    {
      get
      {
        return this.mDeathSentenceIcon;
      }
      set
      {
      }
    }

    public void InitHPGauge(Canvas canvas, UnitGauge gaugeTemplate)
    {
      if ((UnityEngine.Object) canvas == (UnityEngine.Object) null || (UnityEngine.Object) gaugeTemplate == (UnityEngine.Object) null)
        return;
      if ((UnityEngine.Object) this.mHPGauge == (UnityEngine.Object) null)
      {
        this.mHPGauge = UnityEngine.Object.Instantiate<UnitGauge>(gaugeTemplate);
        this.mHPGauge.SetOwner(this.Unit);
      }
      if ((UnityEngine.Object) this.mAddIconGauge == (UnityEngine.Object) null)
        this.mAddIconGauge = this.mHPGauge.GetComponent<UnitGaugeMark>();
      if ((UnityEngine.Object) this.mChargeIcon == (UnityEngine.Object) null)
        this.mChargeIcon = this.mHPGauge.GetComponent<ChargeIcon>();
      if ((UnityEngine.Object) this.mDeathSentenceIcon == (UnityEngine.Object) null)
      {
        this.mDeathSentenceIcon = this.mHPGauge.GetComponent<DeathSentenceIcon>();
        this.mDeathSentenceIcon.Init(this.Unit);
      }
      this.ResetHPGauge();
    }

    public void SetHPGaugeMode(TacticsUnitController.HPGaugeModes mode, SkillData skill = null, Unit attacker = null)
    {
      if (!((UnityEngine.Object) this.mHPGauge != (UnityEngine.Object) null))
        return;
      UnitGauge component = this.mHPGauge.GetComponent<UnitGauge>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
      {
        component.Mode = (int) mode;
        this.OnUnitGaugeModeChange(mode);
      }
      if (mode == TacticsUnitController.HPGaugeModes.Attack)
      {
        if (skill == null || attacker == null)
          return;
        this.mHPGauge.Focus(skill, skill.ElementType, (int) skill.ElementValue, attacker.Element, (int) attacker.CurrentStatus.element_assist[attacker.Element]);
      }
      else
        this.mHPGauge.DeactivateElementIcon();
    }

    public void OnUnitGaugeModeChange(TacticsUnitController.HPGaugeModes Mode)
    {
      if (!((UnityEngine.Object) this.mChargeIcon != (UnityEngine.Object) null))
        return;
      if (this.Unit.CastSkill == null)
      {
        this.mChargeIcon.Close();
      }
      else
      {
        switch (Mode)
        {
          case TacticsUnitController.HPGaugeModes.Normal:
            this.mChargeIcon.Open();
            break;
          case TacticsUnitController.HPGaugeModes.Attack:
            this.mChargeIcon.Close();
            break;
          case TacticsUnitController.HPGaugeModes.Target:
            this.mChargeIcon.Open();
            break;
        }
      }
    }

    public void SetHPChangeYosou(int newHP)
    {
      if (!((UnityEngine.Object) this.mHPGauge != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mHPGauge.MainGauge != (UnityEngine.Object) null))
        return;
      GameSettings instance = GameSettings.Instance;
      if (newHP < this.mCachedHP)
      {
        Color32[] colors;
        if (newHP <= 0)
        {
          colors = new Color32[1]
          {
            this.Unit.Side != EUnitSide.Player ? instance.Gauge_EnemyHP_Damage : instance.Gauge_PlayerHP_Damage
          };
          colors[0].a = byte.MaxValue;
        }
        else
        {
          colors = new Color32[2]
          {
            this.Unit.Side != EUnitSide.Player ? instance.Gauge_EnemyHP_Base : instance.Gauge_PlayerHP_Base,
            this.Unit.Side != EUnitSide.Player ? instance.Gauge_EnemyHP_Damage : instance.Gauge_PlayerHP_Damage
          };
          colors[0].a = (byte) (newHP * (int) byte.MaxValue / this.mCachedHP);
          colors[1].a = (byte) ((uint) byte.MaxValue - (uint) colors[0].a);
        }
        this.mHPGauge.MainGauge.UpdateColors(colors);
      }
      else if (newHP > this.mCachedHP)
      {
        Color32[] colors = new Color32[2]{ this.Unit.Side != EUnitSide.Player ? instance.Gauge_EnemyHP_Base : instance.Gauge_PlayerHP_Base, this.Unit.Side != EUnitSide.Player ? instance.Gauge_EnemyHP_Heal : instance.Gauge_PlayerHP_Heal };
        colors[0].a = (int) this.Unit.MaximumStatus.param.hp == 0 ? (byte) 0 : (byte) (this.mCachedHP * (int) byte.MaxValue / (int) this.Unit.MaximumStatus.param.hp);
        colors[1].a = (byte) ((uint) byte.MaxValue - (uint) colors[0].a);
        this.mHPGauge.MainGauge.Value = (int) this.Unit.MaximumStatus.param.hp == 0 ? 1f : Mathf.Clamp01((float) newHP / (float) (int) this.Unit.MaximumStatus.param.hp);
        this.mHPGauge.MainGauge.UpdateColors(colors);
      }
      else
        this.ResetHPGauge();
    }

    public bool IsHPGaugeChanging
    {
      get
      {
        if ((UnityEngine.Object) this.mHPGauge != (UnityEngine.Object) null && this.mHPGauge.gameObject.activeInHierarchy && ((UnityEngine.Object) this.mHPGauge.MainGauge != (UnityEngine.Object) null && this.mHPGauge.MainGauge.gameObject.activeInHierarchy))
          return this.mHPGauge.MainGauge.IsAnimating;
        return false;
      }
    }

    public RectTransform HPGaugeTransform
    {
      get
      {
        if ((UnityEngine.Object) this.mHPGauge != (UnityEngine.Object) null)
          return this.mHPGauge.GetComponent<RectTransform>();
        return (RectTransform) null;
      }
    }

    public void ShowHPGauge(bool visible)
    {
      if ((UnityEngine.Object) this.mHPGauge != (UnityEngine.Object) null)
        this.mHPGauge.gameObject.SetActive(visible);
      this.mKeepHPGaugeVisible = -1f;
    }

    public void ResetHPGauge()
    {
      this.mCachedHP = (int) this.Unit.CurrentStatus.param.hp;
      if (!((UnityEngine.Object) this.mHPGauge != (UnityEngine.Object) null))
        return;
      GameSettings instance = GameSettings.Instance;
      this.mHPGauge.MainGauge.Colors = new Color32[1]
      {
        this.Unit.Side != EUnitSide.Player ? instance.Gauge_EnemyHP_Base : instance.Gauge_PlayerHP_Base
      };
      this.mHPGauge.MainGauge.AnimateRangedValue(this.mCachedHP, (int) this.Unit.MaximumStatus.param.hp, 0.0f);
    }

    public void UpdateHPRelative(int delta, float duration = 0.5f)
    {
      this.mCachedHP += delta;
      this.mCachedHP = Mathf.Clamp(this.mCachedHP, 0, (int) this.Unit.MaximumStatus.param.hp);
      if ((UnityEngine.Object) this.mHPGauge == (UnityEngine.Object) null)
        return;
      this.mHPGauge.MainGauge.AnimateRangedValue(this.mCachedHP, (int) this.Unit.MaximumStatus.param.hp, duration);
      this.mHPGauge.gameObject.SetActive(true);
      if ((double) this.mKeepHPGaugeVisible < 0.0)
        return;
      this.mKeepHPGaugeVisible = 1.5f;
    }

    private void UpdateGauges()
    {
      if ((UnityEngine.Object) this.mHPGauge != (UnityEngine.Object) null && (double) this.mKeepHPGaugeVisible >= 0.0 && this.mHPGauge.gameObject.activeInHierarchy)
      {
        this.mKeepHPGaugeVisible -= Time.deltaTime;
        if ((double) this.mKeepHPGaugeVisible <= 0.0)
        {
          this.mHPGauge.gameObject.SetActive(false);
          this.mKeepHPGaugeVisible = 0.0f;
        }
      }
      if (!((UnityEngine.Object) this.mAddIconGauge != (UnityEngine.Object) null))
        return;
      this.mAddIconGauge.IsGaugeUpdate = this.IsHPGaugeChanging;
      this.mAddIconGauge.IsUnitDead = this.Unit.IsDead;
    }

    private void OnHitGaugeWeakRegist(Unit attacker)
    {
      if (this.mSkillVars == null || this.mSkillVars.Skill == null)
        return;
      SkillData skill = this.mUnit.GetSkillData(this.mSkillVars.Skill.iname);
      if (skill == null)
      {
        skill = new SkillData();
        skill.Setup(this.mSkillVars.Skill.iname, 1, 1, (MasterParam) null);
      }
      this.mHPGauge.ActivateElementIcon(true);
      this.mHPGauge.OnAttack(skill, skill.ElementType, (int) skill.ElementValue, attacker.Element, (int) attacker.CurrentStatus.element_assist[attacker.Element]);
    }

    public void ShowOwnerIndexUI(bool show)
    {
      if ((UnityEngine.Object) this.mOwnerIndexUI == (UnityEngine.Object) null)
        return;
      this.mOwnerIndexUI.gameObject.SetActive(show);
    }

    public bool CreateOwnerIndexUI(Canvas canvas, GameObject templeteUI, JSON_MyPhotonPlayerParam param)
    {
      if ((UnityEngine.Object) this.mOwnerIndexUI != (UnityEngine.Object) null)
        return true;
      if ((UnityEngine.Object) canvas == (UnityEngine.Object) null || (UnityEngine.Object) templeteUI == (UnityEngine.Object) null || param == null)
        return false;
      this.mOwnerIndexUI = UnityEngine.Object.Instantiate<GameObject>(templeteUI);
      if ((UnityEngine.Object) this.mOwnerIndexUI == (UnityEngine.Object) null)
        return false;
      DataSource.Bind<JSON_MyPhotonPlayerParam>(this.mOwnerIndexUI, param);
      UIProjector uiProjector = this.gameObject.AddComponent<UIProjector>();
      uiProjector.UIObject = this.mOwnerIndexUI.transform as RectTransform;
      uiProjector.AutoDestroyUIObject = true;
      uiProjector.LocalOffset = Vector3.up;
      uiProjector.SetCanvas(canvas);
      return true;
    }

    public void ShowVersusCursor(bool show)
    {
      if ((UnityEngine.Object) this.mVersusCursor == (UnityEngine.Object) null)
        return;
      this.mVersusCursor.gameObject.SetActive(show);
    }

    public void PlayVersusCursor(bool play)
    {
      if ((UnityEngine.Object) this.mVersusCursor == (UnityEngine.Object) null || (UnityEngine.Object) this.mVersusCursorRoot == (UnityEngine.Object) null)
        return;
      Animation component = this.mVersusCursorRoot.GetComponent<Animation>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.enabled = play;
    }

    public bool CreateVersusCursor(GameObject templeteUI)
    {
      if ((UnityEngine.Object) this.mVersusCursor != (UnityEngine.Object) null)
        return true;
      if ((UnityEngine.Object) templeteUI == (UnityEngine.Object) null)
        return false;
      this.mVersusCursor = UnityEngine.Object.Instantiate<GameObject>(templeteUI);
      if ((UnityEngine.Object) this.mVersusCursor == (UnityEngine.Object) null)
        return false;
      this.mVersusCursor.transform.SetParent(this.gameObject.transform, false);
      this.mVersusCursor.transform.localPosition = Vector3.up * 1.3f;
      this.mVersusCursor.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
      GameUtility.SetLayer(this.mVersusCursor, GameUtility.LayerUI, true);
      this.mVersusCursorRoot = this.mVersusCursor.transform.FindChild("Root");
      return true;
    }

    public void SetGimmickIcon(Unit TargetUnit)
    {
      if (TargetUnit == null || !((UnityEngine.Object) this.mAddIconGauge != (UnityEngine.Object) null))
        return;
      if (TargetUnit.UnitType == EUnitType.Treasure)
        this.mAddIconGauge.ChangeAnimationByUnitType(TargetUnit.UnitType);
      if (TargetUnit.UnitType == EUnitType.Gem && TargetUnit.EventTrigger != null)
        this.mAddIconGauge.SetGemIcon(TargetUnit.EventTrigger.GimmickType);
      if (this.mKeepUnitGaugeMarkType != UnitGaugeMark.EMarkType.None && (this.mKeepUnitGaugeMarkType != this.mAddIconGauge.MarkType || this.mKeepUnitGaugeMarkGemIconType != this.mAddIconGauge.GemIconType))
        this.mAddIconGauge.SetEndAnimation(this.mKeepUnitGaugeMarkType);
      this.mKeepUnitGaugeMarkType = this.mAddIconGauge.MarkType;
      this.mKeepUnitGaugeMarkGemIconType = this.mAddIconGauge.GemIconType;
    }

    public void HideGimmickIcon(EUnitType Type)
    {
      if (!((UnityEngine.Object) this.mAddIconGauge != (UnityEngine.Object) null))
        return;
      this.mAddIconGauge.SetEndAnimationAll();
    }

    public void DeleteGimmickIconAll()
    {
      if (!((UnityEngine.Object) this.mAddIconGauge != (UnityEngine.Object) null))
        return;
      this.mAddIconGauge.DeleteIconAll();
    }

    private TacticsUnitController.HideGimmickAnimation HideGimmickAnim
    {
      get
      {
        if (this.mHideGimmickAnim == null)
        {
          this.mHideGimmickAnim = new TacticsUnitController.HideGimmickAnimation();
          this.mHideGimmickAnim.Init(this);
        }
        return this.mHideGimmickAnim;
      }
      set
      {
      }
    }

    public void ScaleHide()
    {
      this.HideGimmickAnim.Enable = true;
      this.HideGimmickAnim.IsHide = true;
    }

    public void ScaleShow()
    {
      this.HideGimmickAnim.Enable = true;
      this.HideGimmickAnim.IsHide = false;
    }

    public void ResetScale()
    {
      this.HideGimmickAnim.ResetScale();
    }

    public bool IsA(string id)
    {
      if (this.Unit != null && this.Unit.UnitParam.iname == id)
        return true;
      return this.UniqueName == id;
    }

    public Unit Unit
    {
      get
      {
        return this.mUnit;
      }
    }

    public Vector2 FieldActionPoint
    {
      get
      {
        return this.mFieldActionPoint;
      }
    }

    public bool TriggerFieldAction(Vector3 velocity, bool resetToMove = false)
    {
      Transform transform = this.transform;
      this.mOnFieldActionEnd = !resetToMove ? new TacticsUnitController.FieldActionEndEvent(this.PlayIdleSmooth) : new TacticsUnitController.FieldActionEndEvent(this.MoveAgain);
      float num1 = Mathf.Floor(transform.position.x);
      float num2 = Mathf.Floor(transform.position.z);
      float num3 = num1 + 1f;
      float num4 = num2 + 1f;
      float num5 = float.MaxValue;
      EUnitDirection direction = EUnitDirection.NegativeX;
      this.mFieldActionPoint = new Vector2(this.transform.position.x, this.transform.position.z);
      float num6 = 0.3f;
      if ((double) velocity.x < 0.0)
      {
        float num7 = transform.position.x - num1;
        if ((double) num7 < (double) num5)
        {
          num5 = num7;
          direction = EUnitDirection.NegativeX;
        }
      }
      else if ((double) velocity.x > 0.0)
      {
        float num7 = num3 - transform.position.x;
        if ((double) num7 < (double) num5)
        {
          num5 = num7;
          direction = EUnitDirection.PositiveX;
        }
      }
      if ((double) velocity.z < 0.0)
      {
        float num7 = transform.position.z - num2;
        if ((double) num7 < (double) num5)
        {
          num5 = num7;
          direction = EUnitDirection.NegativeY;
        }
      }
      else if ((double) velocity.z > 0.0)
      {
        float num7 = num4 - transform.position.z;
        if ((double) num7 < (double) num5)
        {
          num5 = num7;
          direction = EUnitDirection.PositiveY;
        }
      }
      if ((double) num5 <= 0.300000011920929)
      {
        IntVector2 offset = direction.ToOffset();
        int num7 = Mathf.FloorToInt(transform.position.x);
        int num8 = Mathf.FloorToInt(transform.position.z);
        int x = num7 + offset.x;
        int y1 = num8 + offset.y;
        if (this.mWalkableField != null && this.mWalkableField.isValid(x, y1) && this.mWalkableField.get(x, y1) >= 0)
        {
          float y2 = GameUtility.RaycastGround(transform.position).y;
          float num9 = GameUtility.RaycastGround((float) x + 0.5f, (float) y1 + 0.5f) - y2;
          switch (direction)
          {
            case EUnitDirection.PositiveX:
              this.mFieldActionPoint.x = num3 + num6;
              break;
            case EUnitDirection.PositiveY:
              this.mFieldActionPoint.y = num4 + num6;
              break;
            case EUnitDirection.NegativeX:
              this.mFieldActionPoint.x = num1 - num6;
              break;
            case EUnitDirection.NegativeY:
              this.mFieldActionPoint.y = num2 - num6;
              break;
          }
          Vector3 pos = new Vector3(this.mFieldActionPoint.x, 0.0f, this.mFieldActionPoint.y);
          switch (direction)
          {
            case EUnitDirection.PositiveX:
            case EUnitDirection.NegativeX:
              if (this.AdjustMovePos(EUnitDirection.NegativeY, ref pos))
                this.mFieldActionPoint = new Vector2(pos.x, pos.z);
              if (this.AdjustMovePos(EUnitDirection.PositiveY, ref pos))
              {
                this.mFieldActionPoint = new Vector2(pos.x, pos.z);
                break;
              }
              break;
            case EUnitDirection.PositiveY:
            case EUnitDirection.NegativeY:
              if (this.AdjustMovePos(EUnitDirection.NegativeX, ref pos))
                this.mFieldActionPoint = new Vector2(pos.x, pos.z);
              if (this.AdjustMovePos(EUnitDirection.PositiveX, ref pos))
              {
                this.mFieldActionPoint = new Vector2(pos.x, pos.z);
                break;
              }
              break;
          }
          GameSettings instance = GameSettings.Instance;
          this.mFieldActionDir = direction;
          if ((double) num9 <= (double) instance.Unit_FallAnimationThreshold)
          {
            this.GotoState<TacticsUnitController.State_FieldActionFall>();
            return true;
          }
          if ((double) num9 <= -(double) instance.Unit_StepAnimationThreshold)
          {
            this.GotoState<TacticsUnitController.State_FieldActionJump>();
            return true;
          }
          if ((double) num9 >= (double) instance.Unit_JumpAnimationThreshold)
          {
            this.GotoState<TacticsUnitController.State_FieldActionJumpUp>();
            return true;
          }
          if ((double) num9 >= (double) instance.Unit_StepAnimationThreshold)
          {
            this.GotoState<TacticsUnitController.State_FieldActionJump>();
            return true;
          }
        }
      }
      return false;
    }

    public bool AdjustMovePos(EUnitDirection edir, ref Vector3 pos)
    {
      bool flag = false;
      switch (edir)
      {
        case EUnitDirection.PositiveX:
          if ((double) pos.x % 1.0 > 0.699999988079071)
          {
            pos.x = Mathf.Floor(pos.x) + 0.7f;
            flag = true;
            break;
          }
          break;
        case EUnitDirection.PositiveY:
          if ((double) pos.z % 1.0 > 0.699999988079071)
          {
            pos.z = Mathf.Floor(pos.z) + 0.7f;
            flag = true;
            break;
          }
          break;
        case EUnitDirection.NegativeX:
          if ((double) pos.x % 1.0 < 0.300000011920929)
          {
            pos.x = Mathf.Floor(pos.x) + 0.3f;
            flag = true;
            break;
          }
          break;
        case EUnitDirection.NegativeY:
          if ((double) pos.z % 1.0 < 0.300000011920929)
          {
            pos.z = Mathf.Floor(pos.z) + 0.3f;
            flag = true;
            break;
          }
          break;
      }
      return flag;
    }

    public bool IsPlayingFieldAction
    {
      get
      {
        return this.mStateMachine.IsInKindOfState<TacticsUnitController.State_FieldAction>();
      }
    }

    private void Awake()
    {
      this.mRunAnimation = "RUN";
      TacticsUnitController.Instances.Add(this);
      this.LoadEquipments = true;
      this.KeepUnitHidden = true;
      this.mStateMachine = new StateMachine<TacticsUnitController>(this);
    }

    private void SetMonochrome(bool enable)
    {
      if (enable)
      {
        for (int index = 0; index < this.CharacterMaterials.Count; ++index)
        {
          this.CharacterMaterials[index].EnableKeyword("MONOCHROME_ON");
          this.CharacterMaterials[index].DisableKeyword("MONOCHROME_OFF");
        }
      }
      else
      {
        for (int index = 0; index < this.CharacterMaterials.Count; ++index)
        {
          this.CharacterMaterials[index].EnableKeyword("MONOCHROME_OFF");
          this.CharacterMaterials[index].DisableKeyword("MONOCHROME_ON");
        }
      }
    }

    private void DisableColorBlending()
    {
      for (int index = 0; index < this.CharacterMaterials.Count; ++index)
      {
        this.CharacterMaterials[index].EnableKeyword("COLORBLEND_OFF");
        this.CharacterMaterials[index].DisableKeyword("COLORBLEND_ON");
      }
    }

    private void ApplyColorBlending(Color color)
    {
      for (int index = 0; index < this.CharacterMaterials.Count; ++index)
      {
        this.CharacterMaterials[index].EnableKeyword("COLORBLEND_ON");
        this.CharacterMaterials[index].DisableKeyword("COLORBLEND_OFF");
        this.CharacterMaterials[index].SetColor("_blendColor", color);
      }
    }

    public void FadeBlendColor(Color color, float time)
    {
      this.mBlendMode = TacticsUnitController.ColorBlendModes.Fade;
      this.mBlendColor = color;
      this.mBlendColorTime = 0.0f;
      this.mBlendColorTimeMax = time;
    }

    private void UpdateColorBlending()
    {
      Vector3 position = this.transform.position;
      StaticLightVolume volume = StaticLightVolume.FindVolume(position);
      Color directLit;
      Color indirectLit;
      if ((UnityEngine.Object) volume == (UnityEngine.Object) null)
      {
        GameSettings instance = GameSettings.Instance;
        directLit = instance.Character_DefaultDirectLitColor;
        indirectLit = instance.Character_DefaultIndirectLitColor;
      }
      else
        volume.CalcLightColor(position, out directLit, out indirectLit);
      for (int index = 0; index < this.CharacterMaterials.Count; ++index)
      {
        this.CharacterMaterials[index].SetColor("_directLitColor", directLit);
        this.CharacterMaterials[index].SetColor("_indirectLitColor", indirectLit);
      }
      Color color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
      if (this.mBadStatus != null && (double) this.mBadStatus.BlendColor.a > 0.0)
        color = Color.Lerp(this.mBadStatus.BlendColor, new Color(1f, 1f, 1f, 0.0f), (float) ((1.0 - (double) Mathf.Cos(Time.time * 3.141593f)) * 0.5) * 0.5f);
      switch (this.mBlendMode)
      {
        case TacticsUnitController.ColorBlendModes.Fade:
          this.mBlendColorTime += Time.deltaTime;
          float t = Mathf.Clamp01(this.mBlendColorTime / this.mBlendColorTimeMax);
          if ((double) t >= 1.0)
          {
            this.mBlendMode = TacticsUnitController.ColorBlendModes.None;
            break;
          }
          color = Color.Lerp(this.mBlendColor, new Color(1f, 1f, 1f, 0.0f), t);
          break;
      }
      if ((double) color.a > 0.0)
      {
        this.ApplyColorBlending(color);
        this.mEnableColorBlending = true;
      }
      else if (this.mEnableColorBlending)
      {
        this.mEnableColorBlending = false;
        this.DisableColorBlending();
      }
      if ((double) this.ColorMod.r >= 1.0 && (double) this.ColorMod.g >= 1.0 && (double) this.ColorMod.b >= 1.0)
      {
        for (int index = 0; index < this.CharacterMaterials.Count; ++index)
          this.CharacterMaterials[index].DisableKeyword("USE_COLORMOD");
      }
      else
      {
        for (int index = 0; index < this.CharacterMaterials.Count; ++index)
        {
          this.CharacterMaterials[index].EnableKeyword("USE_COLORMOD");
          this.CharacterMaterials[index].SetColor("_colorMod", this.ColorMod);
        }
      }
    }

    public bool HasCursor
    {
      get
      {
        return (UnityEngine.Object) this.mUnitCursor != (UnityEngine.Object) null;
      }
    }

    private void GotoState<T>() where T : TacticsUnitController.State, new()
    {
      this.mStateMachine.GotoState<T>();
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      GameUtility.DestroyGameObject((Component) this.mPet);
      GameUtility.DestroyGameObject((Component) this.mHPGauge);
      TacticsUnitController.Instances.Remove(this);
    }

    public void ShowCursor(UnitCursor prefab, Color color)
    {
      if ((UnityEngine.Object) this.mUnitCursor != (UnityEngine.Object) null || (UnityEngine.Object) prefab == (UnityEngine.Object) null)
        return;
      this.mUnitCursor = UnityEngine.Object.Instantiate<UnitCursor>(prefab);
      this.mUnitCursor.transform.parent = this.transform;
      this.mUnitCursor.Color = color;
      this.mUnitCursor.transform.localPosition = Vector3.up * 0.3f;
    }

    public void CancelAction()
    {
      this.mCancelAction = true;
    }

    public void HideCursor(bool immediate = false)
    {
      if ((UnityEngine.Object) this.mUnitCursor == (UnityEngine.Object) null)
        return;
      if (immediate)
        GameUtility.DestroyGameObject((Component) this.mUnitCursor);
      else
        this.mUnitCursor.FadeOut();
      this.mUnitCursor = (UnitCursor) null;
    }

    public void SetupUnit(Unit unit)
    {
      this.mUnit = unit;
      this.mCachedHP = (int) unit.CurrentStatus.param.hp;
      this.UniqueName = unit.UniqueName;
      this.transform.rotation = this.mUnit.Direction.ToRotation();
      this.SetupPet();
      this.SetupUnit(unit.UnitData, -1);
    }

    private void SetupPet()
    {
      if (this.mUnit.Job == null || string.IsNullOrEmpty(this.mUnit.Job.Param.pet))
        return;
      this.mPet = new GameObject("Pet", new System.Type[1]
      {
        typeof (PetController)
      }).GetComponent<PetController>();
      this.mPet.Owner = this.gameObject;
      this.mPet.PetID = this.mUnit.Job.Param.pet;
    }

    protected new void Start()
    {
      this.OnAnimationUpdate = new AnimationPlayer.AnimationUpdateEvent(this.AnimationUpdated);
      base.Start();
      if (this.mUnit == null || !this.mUnit.IsBreakObj)
      {
        if (this.Posture == TacticsUnitController.PostureTypes.Combat)
          this.LoadUnitAnimationAsync("IDLE", TacticsUnitController.ANIM_IDLE_FIELD, true, false);
        else
          this.LoadUnitAnimationAsync("IDLE", TacticsUnitController.ANIM_IDLE_DEMO, false, false);
      }
      this.GotoState<TacticsUnitController.State_WaitResources>();
    }

    public void LoadDefendSkillEffect(string skillEffectName)
    {
      DebugUtility.Log("LoadDefendSkillEffect: " + skillEffectName);
      if (string.IsNullOrEmpty(skillEffectName))
        return;
      this.AddLoadThreadCount();
      this.StartCoroutine(this.LoadDefendSkillEffectAsync(skillEffectName));
    }

    [DebuggerHidden]
    private IEnumerator LoadDefendSkillEffectAsync(string skillEffectName)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CLoadDefendSkillEffectAsync\u003Ec__Iterator2F() { skillEffectName = skillEffectName, \u003C\u0024\u003EskillEffectName = skillEffectName, \u003C\u003Ef__this = this };
    }

    protected override void PostSetup()
    {
      GameSettings instance = GameSettings.Instance;
      float mapCharacterScale = instance.Quest.MapCharacterScale;
      this.RootMotionScale = mapCharacterScale;
      this.transform.localScale = Vector3.one * mapCharacterScale;
      CharacterSettings component = this.UnitObject.GetComponent<CharacterSettings>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
      {
        Unit unit = this.Unit;
        if (unit != null)
        {
          if (unit.Side == EUnitSide.Enemy)
            this.VesselColor = instance.Character_EnemyGlowColor;
          else
            this.VesselColor = instance.Character_PlayerGlowColor;
          for (int index = 0; index < unit.BattleSkills.Count; ++index)
          {
            SkillParam skillParam = unit.BattleSkills[index].SkillParam;
            if (skillParam.type == ESkillType.Reaction && (skillParam.effect_type == SkillEffectTypes.Defend || skillParam.effect_type == SkillEffectTypes.DamageControl))
              this.LoadDefendSkillEffect(skillParam.defend_effect);
          }
        }
        component.SetSkeleton("small");
        if (this.mUnitObjectLists.Count <= 1 && (UnityEngine.Object) component.ShadowProjector != (UnityEngine.Object) null)
        {
          GameObject gameObject = component.ShadowProjector.gameObject;
          this.mShadow = ((GameObject) UnityEngine.Object.Instantiate((UnityEngine.Object) gameObject, this.transform.position + gameObject.transform.position, gameObject.transform.rotation)).GetComponent<Projector>();
          this.mShadow.transform.SetParent(this.GetCharacterRoot(), true);
          this.mShadow.ignoreLayers = ~(1 << LayerMask.NameToLayer("BG"));
          GameUtility.SetLayer((Component) this.mShadow, GameUtility.LayerHidden, true);
          this.mShadow.orthographicSize *= mapCharacterScale;
        }
      }
      if ((UnityEngine.Object) this.mPet != (UnityEngine.Object) null)
      {
        Transform transform1 = this.transform;
        Transform transform2 = this.mPet.transform;
        transform2.position = transform1.position;
        transform2.rotation = transform1.rotation;
      }
      if (this.Unit == null)
        return;
      this.CacheIcons();
    }

    public void SetRenkeiAura(GameObject eff)
    {
      if ((UnityEngine.Object) this.mRenkeiAuraEffect != (UnityEngine.Object) null)
      {
        GameUtility.StopEmitters(this.mRenkeiAuraEffect);
        GameUtility.RequireComponent<OneShotParticle>(this.mRenkeiAuraEffect);
        this.mRenkeiAuraEffect = (GameObject) null;
      }
      if (!((UnityEngine.Object) eff != (UnityEngine.Object) null))
        return;
      this.mRenkeiAuraEffect = UnityEngine.Object.Instantiate((UnityEngine.Object) eff, Vector3.zero, Quaternion.identity) as GameObject;
      this.mRenkeiAuraEffect.transform.SetParent(this.transform, false);
    }

    public void StopRenkeiAura()
    {
      this.SetRenkeiAura((GameObject) null);
    }

    public void EnableChargeTargetUnit(GameObject eff, bool is_grn)
    {
      if (is_grn)
      {
        if ((bool) ((UnityEngine.Object) this.mChargeGrnTargetUnitEffect) || !(bool) ((UnityEngine.Object) eff))
          return;
        this.mChargeGrnTargetUnitEffect = UnityEngine.Object.Instantiate((UnityEngine.Object) eff, Vector3.zero, Quaternion.identity) as GameObject;
        if (!(bool) ((UnityEngine.Object) this.mChargeGrnTargetUnitEffect))
          return;
        this.mChargeGrnTargetUnitEffect.transform.SetParent(this.transform, false);
      }
      else
      {
        if ((bool) ((UnityEngine.Object) this.mChargeRedTargetUnitEffect) || !(bool) ((UnityEngine.Object) eff))
          return;
        this.mChargeRedTargetUnitEffect = UnityEngine.Object.Instantiate((UnityEngine.Object) eff, Vector3.zero, Quaternion.identity) as GameObject;
        if (!(bool) ((UnityEngine.Object) this.mChargeRedTargetUnitEffect))
          return;
        this.mChargeRedTargetUnitEffect.transform.SetParent(this.transform, false);
      }
    }

    public void DisableChargeTargetUnit()
    {
      if ((bool) ((UnityEngine.Object) this.mChargeGrnTargetUnitEffect))
      {
        GameUtility.StopEmitters(this.mChargeGrnTargetUnitEffect);
        GameUtility.RequireComponent<OneShotParticle>(this.mChargeGrnTargetUnitEffect);
        this.mChargeGrnTargetUnitEffect = (GameObject) null;
      }
      if (!(bool) ((UnityEngine.Object) this.mChargeRedTargetUnitEffect))
        return;
      GameUtility.StopEmitters(this.mChargeRedTargetUnitEffect);
      GameUtility.RequireComponent<OneShotParticle>(this.mChargeRedTargetUnitEffect);
      this.mChargeRedTargetUnitEffect = (GameObject) null;
    }

    public void SetDrainEffect(GameObject eff)
    {
      if (!((UnityEngine.Object) eff != (UnityEngine.Object) null))
        return;
      Transform parent = GameUtility.findChildRecursively(this.transform, "Bip001");
      if ((UnityEngine.Object) parent == (UnityEngine.Object) null)
        parent = this.transform;
      this.mDrainEffect = UnityEngine.Object.Instantiate((UnityEngine.Object) eff, this.transform.position, this.transform.rotation) as GameObject;
      this.mDrainEffect.transform.SetParent(parent, false);
      this.mDrainEffect.RequireComponent<OneShotParticle>();
      this.mDrainEffect.SetActive(false);
    }

    public void BeginLoadPickupAnimation()
    {
      this.LoadUnitAnimationAsync("PICK", TacticsUnitController.ANIM_PICKUP, false, false);
    }

    public void UnloadPickupAnimation()
    {
      this.UnloadAnimation("PICK");
    }

    public void LoadGenkidamaAnimation(bool load)
    {
      if (load)
        this.LoadUnitAnimationAsync("GENK", TacticsUnitController.ANIM_GENKIDAMA, false, false);
      else
        this.UnloadAnimation("GENK");
    }

    public void PlayGenkidama()
    {
      this.PlayAnimation("GENK", true, 0.2f, 0.0f);
    }

    public void PlayPickup(GameObject pickupObject)
    {
      this.mPickupObject = pickupObject;
      this.GotoState<TacticsUnitController.State_Pickup>();
    }

    protected override void Update()
    {
      base.Update();
      if (this.mStateMachine != null)
        this.mStateMachine.Update();
      this.UpdateGauges();
      this.HideGimmickAnim.Update();
      this.execKnockBack();
    }

    public static EUnitDirection CalcUnitDirection(float x, float y)
    {
      float num = (float) ((57.2957801818848 * (double) Mathf.Atan2(y, x) + 360.0) % 360.0);
      if (325.0 <= (double) num || (double) num < 45.0)
        return EUnitDirection.PositiveX;
      if (45.0 <= (double) num && (double) num < 135.0)
        return EUnitDirection.PositiveY;
      return 135.0 <= (double) num && (double) num < 225.0 ? EUnitDirection.NegativeX : EUnitDirection.NegativeY;
    }

    public EUnitDirection CalcUnitDirectionFromRotation()
    {
      Transform transform = this.transform;
      return TacticsUnitController.CalcUnitDirection(transform.forward.x, transform.forward.z);
    }

    private bool CheckCollision(int x0, int y0, int x1, int y1)
    {
      if (!this.mWalkableField.isValid(x1, y1))
        return true;
      int num1 = this.mWalkableField.get(x0, y0);
      int num2 = this.mWalkableField.get(x1, y1);
      if (num2 >= 0)
        return BattleMap.MAX_GRID_MOVING < Mathf.Abs(num1 - num2);
      return true;
    }

    private void AnimationUpdated(GameObject go)
    {
      if (!((UnityEngine.Object) this.mCharacterSettings != (UnityEngine.Object) null))
        return;
      this.mCharacterSettings.SetSkeleton("small");
    }

    public bool CollideGround
    {
      get
      {
        return this.mCollideGround;
      }
      set
      {
        this.mCollideGround = value;
      }
    }

    public void SnapToGround()
    {
      Transform transform = this.transform;
      Vector3 position = transform.position;
      position.y = GameUtility.RaycastGround(position).y;
      transform.position = position;
    }

    protected override void LateUpdate()
    {
      base.LateUpdate();
      if ((double) this.mVelocity.sqrMagnitude > 0.0)
        this.transform.position += this.mVelocity * Time.deltaTime;
      if (this.mCollideGround)
        this.SnapToGround();
      this.UpdateColorBlending();
    }

    public GridMap<int> WalkableField
    {
      set
      {
        this.mWalkableField = value;
      }
    }

    public float StartMove(Vector3[] route, float directionAngle = -1)
    {
      if (this.IgnoreMove)
      {
        this.IgnoreMove = false;
        return 0.0f;
      }
      this.transform.position = route[0];
      this.mPostMoveAngle = directionAngle;
      this.mRoute = route;
      this.mRoutePos = 0;
      this.GotoState<TacticsUnitController.State_Move>();
      float num = 0.0f;
      for (int index = 1; index < route.Length; ++index)
        num += (route[index - 1] - route[index]).magnitude;
      return num / this.mRunSpeed;
    }

    public void SetRunningSpeed(float speed)
    {
      this.mRunSpeed = speed;
    }

    public void StartRunning()
    {
      if (this.mStateMachine.IsInState<TacticsUnitController.State_RunLoop>())
        return;
      this.GotoState<TacticsUnitController.State_RunLoop>();
    }

    public void PlayIdle(float interpTime = 0.0f)
    {
      this.mIdleInterpTime = interpTime;
      this.GotoState<TacticsUnitController.State_Idle>();
    }

    public void StopRunning()
    {
      if (this.mStateMachine.IsInState<TacticsUnitController.State_Idle>())
        return;
      this.PlayIdle(0.0f);
    }

    public void PlayTakenAnimation()
    {
      this.GotoState<TacticsUnitController.State_Taken>();
    }

    public bool isIdle
    {
      get
      {
        if (!this.mStateMachine.IsInState<TacticsUnitController.State_Idle>())
          return this.mStateMachine.IsInState<TacticsUnitController.State_Down>();
        return true;
      }
    }

    public bool isMoving
    {
      get
      {
        if (!this.mStateMachine.IsInState<TacticsUnitController.State_Move>())
          return this.mStateMachine.IsInState<TacticsUnitController.State_RunLoop>();
        return true;
      }
    }

    protected override void OnVisibilityChange(bool visible)
    {
      if (!visible || !((UnityEngine.Object) this.mShadow != (UnityEngine.Object) null))
        return;
      this.mShadow.gameObject.layer = GameUtility.LayerDefault;
    }

    public bool IsLoadedPartially
    {
      get
      {
        return this.mLoadedPartially;
      }
    }

    public void ResetRotation()
    {
      this.transform.rotation = this.mUnit.Direction.ToRotation();
    }

    private void UpdateRotation()
    {
      if (this.mUnit == null)
        return;
      this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.mUnit.Direction.ToRotation(), Time.deltaTime * 10f);
    }

    public void LookAt(Vector3 position)
    {
      if (this.mUnit != null && this.mUnit.IsBreakObj)
        return;
      Transform transform = this.transform;
      Vector3 forward = position - transform.position;
      forward.y = 0.0f;
      transform.rotation = Quaternion.LookRotation(forward);
    }

    public void LookAt(Component target)
    {
      this.LookAt(target.transform.position);
    }

    public void PlayLookAt(Component target, float spin = 2)
    {
      this.PlayLookAt(target.transform.position, spin);
    }

    public void PlayLookAt(Vector3 target, float spin = 2)
    {
      this.mSpinCount = spin;
      this.mLookAtTarget = target;
      this.GotoState<TacticsUnitController.State_LookAt>();
    }

    private void PlayIdleSmooth()
    {
      this.PlayIdle(0.1f);
    }

    private void MoveAgain()
    {
      this.GotoState<TacticsUnitController.State_Move>();
    }

    public void StepTo(Vector3 dest)
    {
      this.mStepStart = this.transform.position;
      this.mStepEnd = dest;
      if ((double) (this.mStepEnd - this.mStepStart).magnitude <= (double) GameSettings.Instance.Quest.AnimateGridSnapRadius)
        this.GotoState<TacticsUnitController.State_StepNoAnimation>();
      else
        this.GotoState<TacticsUnitController.State_Step>();
    }

    public void LockUpdateBadStatus(EUnitCondition condition, bool is_force = false)
    {
      if (!is_force && this.Unit.IsUnitCondition(condition))
        return;
      this.mBadStatusLocks |= (int) condition;
    }

    public void UnlockUpdateBadStatus(EUnitCondition condition)
    {
      this.mBadStatusLocks &= (int) ~condition;
    }

    public void ClearBadStatusLocks()
    {
      this.mBadStatusLocks = 0;
    }

    public void UpdateBadStatus()
    {
      if (BadStatusEffects.Effects == null || this.Unit == null)
        return;
      BadStatusEffects.Desc desc = (BadStatusEffects.Desc) null;
      bool flag1 = false;
      for (int index = 0; index < BadStatusEffects.Effects.Count; ++index)
      {
        EUnitCondition key = BadStatusEffects.Effects[index].Key;
        if (((EUnitCondition) this.mBadStatusLocks & key) == (EUnitCondition) 0 && this.Unit.IsUnitCondition(key))
        {
          if (desc == null)
          {
            bool flag2 = true;
            if (key == EUnitCondition.AutoHeal && this.Unit.IsUnitCondition(EUnitCondition.DisableHeal))
              flag2 = false;
            if (flag2)
              desc = BadStatusEffects.Effects[index];
          }
          if (this.Unit.IsCurseUnitCondition(key))
            flag1 = true;
        }
      }
      if (!this.Unit.IsUnitCondition(EUnitCondition.DeathSentence))
      {
        if ((UnityEngine.Object) this.mDeathSentenceIcon != (UnityEngine.Object) null)
          this.mDeathSentenceIcon.Close();
      }
      else
        this.DeathSentenceCountDown(false, 0.0f);
      if (this.IsCursed != flag1 && (UnityEngine.Object) BadStatusEffects.CurseEffect != (UnityEngine.Object) null)
      {
        if (flag1)
        {
          this.mCurseEffect = UnityEngine.Object.Instantiate<GameObject>(BadStatusEffects.CurseEffect);
          this.attachBadStatusEffect(this.mCurseEffect, BadStatusEffects.CurseEffectAttachTarget, false);
        }
        else
        {
          GameUtility.StopEmitters(this.mCurseEffect);
          this.mCurseEffect.AddComponent<OneShotParticle>();
          this.mCurseEffect = (GameObject) null;
        }
      }
      this.IsCursed = flag1;
      if (desc == this.mBadStatus)
        return;
      if ((UnityEngine.Object) this.mBadStatusEffect != (UnityEngine.Object) null)
      {
        GameUtility.StopEmitters(this.mBadStatusEffect);
        this.mBadStatusEffect.AddComponent<OneShotParticle>();
        this.mBadStatusEffect = (GameObject) null;
      }
      this.mBadStatus = desc;
      if (this.mBadStatus != null)
      {
        if ((UnityEngine.Object) this.mBadStatus.Effect != (UnityEngine.Object) null)
        {
          this.mBadStatusEffect = UnityEngine.Object.Instantiate<GameObject>(this.mBadStatus.Effect);
          this.attachBadStatusEffect(this.mBadStatusEffect, this.mBadStatus.AttachTarget, false);
        }
        if (!string.IsNullOrEmpty(this.mBadStatus.AnimationName) && string.IsNullOrEmpty(this.mLoadedBadStatusAnimation))
        {
          this.mLoadedBadStatusAnimation = this.mBadStatus.AnimationName;
          this.LoadUnitAnimationAsync("BAD", this.mBadStatus.AnimationName, false, false);
        }
        if (this.Unit.IsUnitCondition(EUnitCondition.DeathSentence))
          this.mDeathSentenceIcon.Open();
      }
      this.SetMonochrome(this.mBadStatus != null && this.mBadStatus.Key == EUnitCondition.Stone);
    }

    private void attachBadStatusEffect(GameObject go_effect, string attach_target = null, bool is_use_cs = false)
    {
      if (!(bool) ((UnityEngine.Object) go_effect))
        return;
      Transform parent = (Transform) null;
      if (!string.IsNullOrEmpty(attach_target))
        parent = !is_use_cs || !(bool) ((UnityEngine.Object) this.mCharacterSettings) ? GameUtility.findChildRecursively(this.transform, attach_target) : GameUtility.findChildRecursively(this.mCharacterSettings.transform, attach_target);
      if ((UnityEngine.Object) parent == (UnityEngine.Object) null)
        parent = this.GetCharacterRoot();
      go_effect.transform.SetParent(parent, false);
    }

    public bool IsDeathSentenceCountDownPlaying()
    {
      if ((UnityEngine.Object) this.mDeathSentenceIcon != (UnityEngine.Object) null)
        return this.mDeathSentenceIcon.IsDeathSentenceCountDownPlaying;
      return false;
    }

    public void DeathSentenceCountDown(bool isShow, float LifeTime = 0.0f)
    {
      if (isShow)
        this.ShowHPGauge(true);
      if (!((UnityEngine.Object) this.mDeathSentenceIcon != (UnityEngine.Object) null))
        return;
      this.mDeathSentenceIcon.Open();
      this.mDeathSentenceIcon.Countdown(Mathf.Max(this.Unit.DeathCount, 0), LifeTime);
    }

    public void UpdateShields(bool is_update_turn = true)
    {
      if (this.mUnit == null)
        return;
      using (List<TacticsUnitController.ShieldState>.Enumerator enumerator = this.Shields.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          TacticsUnitController.ShieldState current = enumerator.Current;
          if ((int) current.Target.hp != current.LastHP || (bool) current.Target.is_efficacy)
            current.Dirty = true;
        }
      }
      using (List<Unit.UnitShield>.Enumerator enumerator = this.mUnit.Shields.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Unit.UnitShield unit_shield = enumerator.Current;
          if (this.Shields.Find((Predicate<TacticsUnitController.ShieldState>) (sh => sh.Target == unit_shield)) == null)
            this.Shields.Add(new TacticsUnitController.ShieldState()
            {
              Target = unit_shield,
              LastHP = (int) unit_shield.hp,
              LastTurn = (int) unit_shield.turn
            });
        }
      }
    }

    public void RemoveObsoleteShieldStates()
    {
      if (this.mUnit == null)
        return;
      for (int index = this.Shields.Count - 1; index >= 0; --index)
      {
        if (!this.mUnit.Shields.Contains(this.Shields[index].Target))
          this.Shields.RemoveAt(index);
      }
    }

    private void ShowCriticalEffect(Vector3 position, float yOffset)
    {
      UnityEngine.Camera main = UnityEngine.Camera.main;
      if ((UnityEngine.Object) main != (UnityEngine.Object) null)
      {
        GameSettings instance = GameSettings.Instance;
        FlashEffect flashEffect = main.gameObject.AddComponent<FlashEffect>();
        flashEffect.Strength = instance.CriticalHit_FlashStrength;
        flashEffect.Duration = instance.CriticalHit_FlashDuration;
        CameraShakeEffect cameraShakeEffect = main.gameObject.AddComponent<CameraShakeEffect>();
        cameraShakeEffect.Duration = instance.CriticalHit_ShakeDuration;
        cameraShakeEffect.AmplitudeX = instance.CriticalHit_ShakeAmplitudeX;
        cameraShakeEffect.AmplitudeY = instance.CriticalHit_ShakeAmplitudeY;
        cameraShakeEffect.FrequencyX = instance.CriticalHit_ShakeFrequencyX;
        cameraShakeEffect.FrequencyY = instance.CriticalHit_ShakeFrequencyY;
      }
      if (!((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null))
        return;
      SceneBattle.Instance.PopupCritical(position, yOffset);
    }

    private void DrainGems(TacticsUnitController goal)
    {
      if (this.GemDrainEffects == null || this.DrainGemsOnHit <= 0)
        return;
      int num = this.DrainGemsOnHit + UnityEngine.Random.Range(0, GameSettings.Instance.Gem_DrainCount_Randomness);
      Transform transform = goal.transform;
      Vector3 axis = this.transform.position - transform.position;
      Vector3 centerPosition = this.CenterPosition;
      Vector3 vector3 = goal.CenterPosition - transform.position;
      for (int index1 = 0; index1 < num; ++index1)
      {
        Quaternion quaternion = Quaternion.AngleAxis((float) UnityEngine.Random.Range(135, 225), axis) * Quaternion.AngleAxis((float) UnityEngine.Random.Range(70, 110), Vector3.right);
        for (int index2 = 0; index2 < this.GemDrainEffects.Length; ++index2)
        {
          if (!this.GemDrainEffects[index2].gameObject.activeInHierarchy)
          {
            this.GemDrainEffects[index2].Reset();
            this.GemDrainEffects[index2].gameObject.SetActive(true);
            this.GemDrainEffects[index2].transform.position = centerPosition;
            this.GemDrainEffects[index2].transform.rotation = quaternion;
            this.GemDrainEffects[index2].TargetObject = transform;
            this.GemDrainEffects[index2].TargetOffset = vector3;
            if ((UnityEngine.Object) this.GemDrainHitEffect != (UnityEngine.Object) null)
            {
              if (!GemParticleHitEffect.IsEnable)
                GemParticleHitEffect.IsEnable = true;
              this.GemDrainEffects[index2].gameObject.AddComponent<GemParticleHitEffect>();
              this.GemDrainEffects[index2].gameObject.GetComponent<GemParticleHitEffect>().EffectPrefab = this.GemDrainHitEffect;
              break;
            }
            break;
          }
        }
      }
    }

    public void InitShake(Vector3 basePosition, Vector3 direction)
    {
      if (this.mShaker == null)
        this.mShaker = new TacticsUnitController.ShakeUnit();
      this.mShaker.Init(basePosition, direction);
    }

    public bool ShakeStart
    {
      set
      {
        this.mShaker.ShakeStart = true;
      }
      get
      {
        return this.mShaker.ShakeStart;
      }
    }

    public bool IsShakeEnd()
    {
      return this.mShaker.ShakeCount <= 0;
    }

    public float GetShakeProgress()
    {
      return (float) (1 - this.mShaker.ShakeCount / this.mShaker.ShakeMaxCount);
    }

    public void AdvanceShake()
    {
      this.transform.localPosition = this.mShaker.AdvanceShake();
    }

    public void SkillTurn(LogSkill Act, EUnitDirection AxisDirection)
    {
      if (this.mSkillVars == null || this.mSkillVars.mSkillSequence == null)
        return;
      SceneBattle instance = SceneBattle.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return;
      Vector3 vector3 = instance.CalcGridCenter(Act.self.x, Act.self.y);
      Vector3 zero = Vector3.zero;
      IntVector2 intVector2_1 = new IntVector2();
      Vector3 pos;
      if (Act.CauseOfReaction != null)
      {
        pos = instance.CalcGridCenter(Act.CauseOfReaction.x, Act.CauseOfReaction.y);
        intVector2_1.x = Act.CauseOfReaction.x;
        intVector2_1.y = Act.CauseOfReaction.y;
      }
      else
      {
        pos = instance.CalcGridCenter(Act.pos.x, Act.pos.y);
        intVector2_1.x = Act.pos.x;
        intVector2_1.y = Act.pos.y;
        if (this.ReflectTargetTypeToPos(ref pos))
        {
          IntVector2 intVector2_2 = instance.CalcCoord(pos);
          intVector2_1.x = intVector2_2.x;
          intVector2_1.y = intVector2_2.y;
        }
      }
      switch (this.mSkillVars.mSkillSequence.SkillTurnType)
      {
        case SkillSequence.SkillTurnTypes.Target:
          IntVector2 intVector2_3 = instance.CalcCoord(this.transform.position);
          if (intVector2_3.x == intVector2_1.x && intVector2_3.y == intVector2_1.y)
            break;
          this.LookAt(pos);
          break;
        case SkillSequence.SkillTurnTypes.Axis:
          pos.x = vector3.x + (float) Unit.DIRECTION_OFFSETS[(int) AxisDirection, 0];
          pos.z = vector3.z + (float) Unit.DIRECTION_OFFSETS[(int) AxisDirection, 1];
          this.LookAt(pos);
          break;
      }
    }

    public void SetArrowTrajectoryHeight(float Height)
    {
      this.mMapTrajectoryHeight = GameUtility.InternalToMapHeight(Height);
    }

    private bool HitTimerExists(TacticsUnitController target)
    {
      for (int index = 0; index < this.mSkillVars.HitTimers.Count; ++index)
      {
        if ((UnityEngine.Object) this.mSkillVars.HitTimers[index].Target == (UnityEngine.Object) target)
          return true;
      }
      return false;
    }

    public bool IsStartSkill()
    {
      return this.mSkillVars != null;
    }

    public SkillSequence.MapCameraTypes GetSkillCameraType()
    {
      if (this.mSkillVars != null && this.mSkillVars.mSkillSequence != null)
        return this.mSkillVars.mSkillSequence.MapCameraType;
      return SkillSequence.MapCameraTypes.None;
    }

    public bool IsSkillMirror()
    {
      if (this.mSkillVars != null && this.mSkillVars.mSkillSequence != null && !this.mSkillVars.mSkillSequence.NotMirror)
        return this.Unit.Side == EUnitSide.Enemy;
      return false;
    }

    public void LoadDeathAnimation(TacticsUnitController.DeathAnimationTypes mask)
    {
      if ((mask & TacticsUnitController.DeathAnimationTypes.Normal) == (TacticsUnitController.DeathAnimationTypes) 0 || !((UnityEngine.Object) this.FindAnimation("B_DEAD") == (UnityEngine.Object) null))
        return;
      this.LoadUnitAnimationAsync("B_DEAD", "cmn_downstand0", false, false);
    }

    public void PlayDead(TacticsUnitController.DeathAnimationTypes type)
    {
      this.GotoState<TacticsUnitController.State_Dead>();
    }

    private void CalcCameraPos(AnimationClip clip, float normalizedTime, int cameraIndex, out Vector3 pos, out Quaternion rotation)
    {
      pos = Vector3.zero;
      rotation = Quaternion.identity;
      GameObject go = new GameObject();
      GameObject gameObject1 = new GameObject("Camera001");
      GameObject gameObject2 = new GameObject("Camera002");
      Transform transform1 = go.transform;
      Transform transform2 = gameObject1.transform;
      Transform transform3 = gameObject2.transform;
      transform1.SetParent(this.transform, false);
      transform2.SetParent(transform1, false);
      transform3.SetParent(transform1, false);
      clip.SampleAnimation(go, normalizedTime * clip.length);
      Transform transform4 = cameraIndex == 0 ? transform2 : transform3;
      pos = transform4.position - this.transform.position + this.RootMotionInverse;
      rotation = transform4.rotation;
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) go);
      rotation *= GameUtility.Yaw180;
    }

    private void SetActiveCameraPosition(Vector3 position, Quaternion rotation)
    {
      Transform transform = this.mSkillVars.mActiveCamera.transform;
      transform.position = position;
      transform.rotation = rotation;
    }

    private void AnimateCamera(AnimationClip cameraClip, float normalizedTime, int cameraIndex = 0)
    {
      this.AnimateCameraInterpolated(cameraClip, normalizedTime, 1f, TacticsUnitController.CameraState.Default, cameraIndex);
    }

    private void AnimateCameraInterpolated(AnimationClip cameraClip, float normalizedTime, float interpRate, TacticsUnitController.CameraState startState, int cameraIndex = 0)
    {
      Vector3 pos;
      Quaternion rotation;
      this.CalcCameraPos(cameraClip, normalizedTime, cameraIndex, out pos, out rotation);
      Vector3 vector3 = this.transform.position + pos;
      Quaternion b = rotation;
      if ((double) interpRate < 1.0)
      {
        vector3 = Vector3.Lerp(startState.Position, vector3, interpRate);
        b = Quaternion.Slerp(startState.Rotation, b, interpRate);
      }
      this.SetActiveCameraPosition(vector3, this.mSkillVars.mCameraShakeOffset * b);
    }

    public void PlayDamageReaction(int damage, HitReactionTypes hitType = HitReactionTypes.Normal)
    {
      if (this.ShouldDefendHits || damage <= 0)
        return;
      this.PlayDamage(hitType);
    }

    [DebuggerHidden]
    private IEnumerator ShowHitPopup(TacticsUnitController target, bool critical, bool backstab, bool guard, bool weak, bool resist, bool hpDamage, int hpDamageValue, bool mpDamage, int mpDamageValue, bool absorb, bool is_guts)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CShowHitPopup\u003Ec__Iterator30() { guard = guard, target = target, critical = critical, backstab = backstab, weak = weak, resist = resist, absorb = absorb, is_guts = is_guts, hpDamage = hpDamage, hpDamageValue = hpDamageValue, mpDamage = mpDamage, mpDamageValue = mpDamageValue, \u003C\u0024\u003Eguard = guard, \u003C\u0024\u003Etarget = target, \u003C\u0024\u003Ecritical = critical, \u003C\u0024\u003Ebackstab = backstab, \u003C\u0024\u003Eweak = weak, \u003C\u0024\u003Eresist = resist, \u003C\u0024\u003Eabsorb = absorb, \u003C\u0024\u003Eis_guts = is_guts, \u003C\u0024\u003EhpDamage = hpDamage, \u003C\u0024\u003EhpDamageValue = hpDamageValue, \u003C\u0024\u003EmpDamage = mpDamage, \u003C\u0024\u003EmpDamageValue = mpDamageValue, \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator ShowHealPopup(TacticsUnitController target, int hpHeal, int mpHeal)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CShowHealPopup\u003Ec__Iterator31() { hpHeal = hpHeal, target = target, mpHeal = mpHeal, \u003C\u0024\u003EhpHeal = hpHeal, \u003C\u0024\u003Etarget = target, \u003C\u0024\u003EmpHeal = mpHeal, \u003C\u003Ef__this = this };
    }

    private void HitTarget(TacticsUnitController target, HitReactionTypes hitReaction = HitReactionTypes.None, bool doReaction = true)
    {
      bool flag1 = target.mHitInfo.IsCombo();
      bool isLast = this.mSkillVars.NumHitsLeft == 0;
      bool flag2 = !this.mSkillVars.UseBattleScene;
      bool flag3 = false;
      bool absorb = target.mHitInfo.shieldDamage > 0;
      bool is_guts = (target.mHitInfo.hitType & LogSkill.EHitTypes.Guts) != (LogSkill.EHitTypes) 0;
      int num1 = 0;
      bool flag4 = target.ShouldDodgeHits;
      int num2;
      int num3;
      if (flag1)
      {
        int index = target.mHitInfo.hits.Count - 1 - this.mSkillVars.NumHitsLeft;
        if (index < 0 || index >= target.mHitInfo.hits.Count)
          index = 0;
        num2 = target.mHitInfo.hits[index].hp_damage;
        num3 = target.mHitInfo.hits[index].mp_damage;
        flag4 = target.mHitInfo.hits[index].is_avoid;
      }
      else
      {
        num2 = target.mHitInfo.GetTotalHpDamage();
        num3 = target.mHitInfo.GetTotalMpDamage();
      }
      if (flag2 && num2 < 1 && num1 < 1)
        flag2 = true;
      int totalHpHeal = target.mHitInfo.GetTotalHpHeal();
      int totalMpHeal = target.mHitInfo.GetTotalMpHeal();
      if (doReaction && (num2 >= 1 || num3 >= 1))
        target.PlayDamageReaction(1, hitReaction);
      if (target.mKnockBackGrid != null && isLast)
        target.mKnockBackMode = TacticsUnitController.eKnockBackMode.START;
      if (this.mSkillVars.Skill.effect_type != SkillEffectTypes.Throw && (UnityEngine.Object) target != (UnityEngine.Object) this && this.mSkillVars.NumHitsLeft == this.mSkillVars.TotalHits - 1)
      {
        target.AutoUpdateRotation = false;
        target.LookAt(this.transform.position);
      }
      target.DrainGems(this);
      this.StartCoroutine(this.ShowHitPopup(target, target.ShowCriticalEffectOnHit, target.ShowBackstabEffectOnHit && (UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null, target.ShouldDefendHits, target.ShowElementEffectOnHit == TacticsUnitController.EElementEffectTypes.Effective, target.ShowElementEffectOnHit == TacticsUnitController.EElementEffectTypes.NotEffective, false, 0, false, 0, absorb, is_guts));
      if (!target.ShouldDefendHits && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Changing && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Throw)
        this.SpawnHitEffect(target.transform.position, isLast);
      if (!flag1 && !isLast)
        return;
      bool flag5 = false;
      if (this.mSkillVars.Skill.IsDamagedSkill() && !flag4)
      {
        bool hpDamage = num2 >= 1;
        bool mpDamage = num3 >= 1;
        this.StartCoroutine(this.ShowHitPopup(target, false, false, false, false, false, hpDamage, num2, mpDamage, num3, false, is_guts));
        if (mpDamage && isLast)
          flag3 = true;
        if (flag2 && hpDamage)
        {
          float duration = !isLast ? 0.1f : 0.5f;
          target.UpdateHPRelative(-num2, duration);
          target.OnHitGaugeWeakRegist(this.mUnit);
          target.ChargeIcon.Close();
          if (target.mHitInfo.IsDefend())
          {
            if (this.mSkillVars.TotalHits <= 1 || this.mSkillVars.TotalHits - this.mSkillVars.NumHitsLeft <= 1)
              target.Unit.PlayBattleVoice("battle_0016");
          }
          else
          {
            int num4 = Mathf.Max(num2, num3);
            if (this.mSkillVars.TotalHits > 1)
              num4 /= this.mSkillVars.TotalHits;
            float num5 = (int) target.Unit.MaximumStatus.param.hp == 0 ? 1f : 1f * (float) num4 / (float) (int) target.Unit.MaximumStatus.param.hp;
            if ((double) num5 >= 0.75)
              target.Unit.PlayBattleVoice("battle_0036");
            else if ((double) num5 > 0.5)
              target.Unit.PlayBattleVoice("battle_0035");
            else
              target.Unit.PlayBattleVoice("battle_0034");
          }
        }
      }
      this.StartCoroutine(this.ShowHealPopup(target, totalHpHeal, totalMpHeal));
      if (totalHpHeal >= 1 && flag2)
      {
        target.UpdateHPRelative(totalHpHeal, 0.5f);
        if (!flag5 && this.Unit != target.Unit && totalHpHeal > 0)
        {
          target.Unit.PlayBattleVoice("battle_0018");
          flag5 = true;
        }
      }
      if (totalMpHeal >= 1)
      {
        flag3 = true;
        if (!flag5 && this.Unit != target.Unit)
        {
          target.Unit.PlayBattleVoice("battle_0018");
          flag5 = true;
        }
      }
      if (!this.mSkillVars.mIsFinishedBuffEffectTarget && (target.mHitInfo.IsBuffEffect() || target.mHitInfo.ChangeValueCT != 0))
      {
        SceneBattle.Instance.PopupParamChange(target.CenterPosition, target.mHitInfo.buff, target.mHitInfo.debuff, target.mHitInfo.ChangeValueCT);
        if (!flag5 && this.Unit != target.Unit && target.mHitInfo.buff.CheckEffect())
          target.Unit.PlayBattleVoice("battle_0018");
      }
      if (!flag3)
        return;
      UnitQueue.Instance.Refresh(target.Unit);
    }

    public void BuffEffectTarget()
    {
      if (this.mSkillVars == null || this.mSkillVars.Skill == null || (this.mSkillVars.Targets == null || !this.mSkillVars.Skill.IsPrevApply()))
        return;
      using (List<TacticsUnitController>.Enumerator enumerator = this.mSkillVars.Targets.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          TacticsUnitController current = enumerator.Current;
          if (current.mHitInfo.IsBuffEffect() || current.mHitInfo.ChangeValueCT != 0)
          {
            SceneBattle.Instance.PopupParamChange(current.CenterPosition, current.mHitInfo.buff, current.mHitInfo.debuff, current.mHitInfo.ChangeValueCT);
            this.mSkillVars.mIsFinishedBuffEffectTarget = true;
          }
        }
      }
    }

    public void BuffEffectSelf()
    {
      if (this.mSkillVars == null || this.mSkillVars.Skill == null || (!this.mSkillVars.Skill.IsPrevApply() || this.mHitInfoSelf == null) || this.mHitInfoSelf.hits.Count <= 0 && !this.mHitInfoSelf.buff.CheckEffect() && !this.mHitInfoSelf.debuff.CheckEffect() || !this.mHitInfoSelf.IsBuffEffect())
        return;
      SceneBattle.Instance.PopupParamChange(this.CenterPosition, this.mHitInfoSelf.buff, this.mHitInfoSelf.debuff, 0);
      this.mSkillVars.mIsFinishedBuffEffectSelf = true;
    }

    public void SkillEffectSelf()
    {
      if (this.mHitInfoSelf == null || this.mHitInfoSelf.hits.Count <= 0 && !this.mHitInfoSelf.buff.CheckEffect() && !this.mHitInfoSelf.debuff.CheckEffect())
        return;
      bool flag1 = !this.mSkillVars.UseBattleScene;
      int totalHpDamage = this.mHitInfoSelf.GetTotalHpDamage();
      int totalMpDamage = this.mHitInfoSelf.GetTotalMpDamage();
      int totalHpHeal = this.mHitInfoSelf.GetTotalHpHeal();
      int totalMpHeal = this.mHitInfoSelf.GetTotalMpHeal();
      int num = totalHpDamage + this.mSkillVars.HpCostDamage;
      if (flag1 && totalHpDamage < 1 && totalHpHeal < 1)
        flag1 = false;
      bool flag2 = false;
      for (int index = 0; index < this.mHitInfoSelf.debuff.bits.Length; ++index)
      {
        if (this.mHitInfoSelf.debuff.bits[index] != 0)
        {
          flag2 = true;
          break;
        }
      }
      if (num >= 1 || totalMpDamage >= 1 || flag2)
        this.PlayDamageReaction(1, HitReactionTypes.Normal);
      if (num > 0)
      {
        SceneBattle.Instance.PopupDamageNumber(this.CenterPosition, num);
        MonoSingleton<GameManager>.Instance.Player.OnDamageToEnemy(this.mUnit, this.mUnit, num);
        if (flag1)
        {
          this.UpdateHPRelative(-num, 0.5f);
          this.OnHitGaugeWeakRegist(this.mUnit);
          this.ChargeIcon.Close();
        }
      }
      if (totalHpHeal >= 1 || totalMpHeal >= 1)
      {
        SceneBattle.Instance.StartCoroutine(this.ShowHealPopup(this, totalHpHeal, totalMpHeal));
        if ((UnityEngine.Object) this.mDrainEffect != (UnityEngine.Object) null)
        {
          this.mDrainEffect.transform.position = this.CenterPosition;
          this.mDrainEffect.transform.rotation = this.transform.rotation;
          this.mDrainEffect.SetActive(true);
        }
        if (flag1)
        {
          this.UpdateHPRelative(totalHpHeal, 0.5f);
          this.ChargeIcon.Close();
        }
      }
      if (!this.mSkillVars.mIsFinishedBuffEffectSelf && this.mHitInfoSelf.IsBuffEffect())
        SceneBattle.Instance.PopupParamChange(this.CenterPosition, this.mHitInfoSelf.buff, this.mHitInfoSelf.debuff, 0);
      this.UpdateBadStatus();
    }

    private bool ReflectTargetTypeToPos(ref Vector3 pos)
    {
      if (this.mSkillVars.Skill == null || (UnityEngine.Object) this.mSkillVars.mSkillEffect == (UnityEngine.Object) null || !SkillParam.IsTypeLaser(this.mSkillVars.Skill.select_scope))
        return false;
      SceneBattle instance = SceneBattle.Instance;
      BattleCore battleCore = (BattleCore) null;
      if ((bool) ((UnityEngine.Object) instance))
        battleCore = instance.Battle;
      if (battleCore == null)
        return false;
      EUnitDirection eunitDirection = instance.UnitDirectionFromPosition(this.transform.position, pos);
      switch (this.mSkillVars.mSkillEffect.TargetTypeForLaser)
      {
        case SkillEffect.eTargetTypeForLaser.StepFront:
          int frontTypeForLaser = this.mSkillVars.mSkillEffect.StepFrontTypeForLaser;
          IntVector2 intVector2_1 = instance.CalcCoord(this.transform.position);
          pos.x = (float) (intVector2_1.x + Unit.DIRECTION_OFFSETS[(int) eunitDirection, 0] * frontTypeForLaser) + 0.5f;
          pos.z = (float) (intVector2_1.y + Unit.DIRECTION_OFFSETS[(int) eunitDirection, 1] * frontTypeForLaser) + 0.5f;
          pos.y = GameUtility.RaycastGround(pos).y;
          break;
        case SkillEffect.eTargetTypeForLaser.FrontCenter:
          SkillData skillData = this.mUnit.GetSkillData(this.mSkillVars.Skill.iname);
          if (skillData != null)
          {
            int attackRangeMax = this.mUnit.GetAttackRangeMax(skillData);
            if (attackRangeMax > 0)
            {
              int num = attackRangeMax - this.mUnit.GetAttackRangeMin(skillData);
              switch (this.mSkillVars.Skill.select_scope)
              {
                case ESelectType.LaserSpread:
                  ++num;
                  break;
                case ESelectType.LaserWide:
                case ESelectType.LaserTriple:
                  num += 2;
                  break;
              }
              IntVector2 intVector2_2 = instance.CalcCoord(this.transform.position);
              pos.x = (float) ((double) intVector2_2.x + (double) Unit.DIRECTION_OFFSETS[(int) eunitDirection, 0] * ((double) num + 1.0) / 2.0 + 0.5);
              pos.z = (float) ((double) intVector2_2.y + (double) Unit.DIRECTION_OFFSETS[(int) eunitDirection, 1] * ((double) num + 1.0) / 2.0 + 0.5);
              pos.y = GameUtility.RaycastGround(pos).y;
              break;
            }
            break;
          }
          break;
        default:
          return false;
      }
      return true;
    }

    [DebuggerHidden]
    private IEnumerator DelayedPopupParamChange(BuffBit buff, BuffBit debuff)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CDelayedPopupParamChange\u003Ec__Iterator32() { buff = buff, debuff = debuff, \u003C\u0024\u003Ebuff = buff, \u003C\u0024\u003Edebuff = debuff, \u003C\u003Ef__this = this };
    }

    protected override void OnEventStart(AnimEvent e, float weight)
    {
      if (this.mSkillVars == null)
        return;
      if (e is ToggleCamera)
      {
        this.mSkillVars.mCameraID = (e as ToggleCamera).CameraID;
        if (this.mSkillVars.mCameraID >= 0 && this.mSkillVars.mCameraID <= 1)
          return;
        UnityEngine.Debug.LogError((object) ("Invalid CameraID: " + (object) this.mSkillVars.mCameraID));
        this.mSkillVars.mCameraID = 0;
      }
      else if (e is HitFrame && (this.mSkillVars.mSkillSequence.SkillType == SkillSequence.SkillTypes.Melee || (UnityEngine.Object) this.mSkillVars.mSkillEffect != (UnityEngine.Object) null && this.mSkillVars.mSkillEffect.IsTeleportMode))
      {
        HitFrame hitFrame = e as HitFrame;
        --this.mSkillVars.NumHitsLeft;
        for (int index1 = 0; index1 < this.mSkillVars.Targets.Count; ++index1)
        {
          TacticsUnitController target = this.mSkillVars.Targets[index1];
          bool flag = target.ShouldDodgeHits;
          bool perfectAvoid = target.ShouldPerfectDodge;
          if (target.mHitInfo.IsCombo())
          {
            int index2 = target.mHitInfo.hits.Count - 1 - this.mSkillVars.NumHitsLeft;
            if (0 <= index2 && index2 < target.mHitInfo.hits.Count)
            {
              flag = target.mHitInfo.hits[index2].is_avoid;
              perfectAvoid = target.mHitInfo.hits[index2].is_pf_avoid;
            }
          }
          if (flag)
          {
            bool is_disp_popup = true;
            if (this.mSkillVars.UseBattleScene)
              is_disp_popup = this.mSkillVars.NumHitsLeft == this.mSkillVars.TotalHits - 1;
            if (target.mHitInfo.IsCombo() || this.mSkillVars.NumHitsLeft == 0)
              this.mSkillVars.Targets[index1].PlayDodge(perfectAvoid, is_disp_popup);
            if (this.mSkillVars.mSkillEffect.AlwaysExplode && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Changing && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Throw)
              this.SpawnHitEffect(this.mSkillVars.Targets[index1].transform.position, this.mSkillVars.NumHitsLeft == 0);
          }
          else
          {
            this.HitTarget(this.mSkillVars.Targets[index1], hitFrame.ReactionType, true);
            MonoSingleton<GameManager>.Instance.Player.OnDamageToEnemy(this.mUnit, target.Unit, target.mHitInfo.GetTotalHpDamage());
          }
        }
      }
      else if (e is ChantFrame)
      {
        ChantFrame chantFrame = (ChantFrame) e;
        if (!((UnityEngine.Object) this.mSkillVars.mSkillEffect.ChantEffect != (UnityEngine.Object) null))
          return;
        Vector3 spawnPos;
        Quaternion spawnRot;
        chantFrame.CalcPosition(this.UnitObject, this.mSkillVars.mSkillEffect.ChantEffect, out spawnPos, out spawnRot);
        this.mSkillVars.mChantEffect = (GameObject) UnityEngine.Object.Instantiate((UnityEngine.Object) this.mSkillVars.mSkillEffect.ChantEffect, spawnPos, spawnRot);
        GameUtility.SetLayer(this.mSkillVars.mChantEffect, this.gameObject.layer, true);
        if (!chantFrame.AttachEffect)
          return;
        this.mSkillVars.mChantEffect.transform.parent = chantFrame.BoneName.Length <= 0 ? this.UnitObject.transform : GameUtility.findChildRecursively(this.UnitObject.transform, chantFrame.BoneName);
      }
      else if (e is ProjectileFrame && (UnityEngine.Object) this.mSkillVars.mSkillEffect != (UnityEngine.Object) null && this.IsRangedSkillType())
      {
        this.mSkillVars.mProjectileTriggered = true;
        ProjectileFrame projectileFrame = (ProjectileFrame) e;
        TacticsUnitController.ProjectileData pd = new TacticsUnitController.ProjectileData();
        if (projectileFrame is ProjectileFrameHitOnly)
        {
          pd.mIsHitOnly = true;
          ProjectileFrameHitOnly projectileFrameHitOnly = (ProjectileFrameHitOnly) projectileFrame;
          pd.mIsNotSpawnLandingEffect = !projectileFrameHitOnly.IsSpawnLandingEffect;
        }
        if ((UnityEngine.Object) this.mSkillVars.mSkillEffect.ProjectileEffect != (UnityEngine.Object) null && !pd.mIsHitOnly)
        {
          Vector3 spawnPos;
          Quaternion spawnRot;
          projectileFrame.CalcPosition(this.UnitObject, this.mSkillVars.mSkillEffect.ProjectileEffect, out spawnPos, out spawnRot);
          pd.mProjectile = UnityEngine.Object.Instantiate((UnityEngine.Object) this.mSkillVars.mSkillEffect.ProjectileEffect, spawnPos, spawnRot) as GameObject;
        }
        else
        {
          Vector3 spawnPos;
          Quaternion spawnRot;
          projectileFrame.CalcPosition(this.UnitObject, Vector3.zero, Quaternion.identity, out spawnPos, out spawnRot);
          pd.mProjectile = new GameObject("PROJECTILE");
          Transform transform = pd.mProjectile.transform;
          transform.position = spawnPos;
          transform.rotation = spawnRot;
        }
        this.mSkillVars.mProjectileDataLists.Add(pd);
        ++this.mSkillVars.mNumShotCount;
        this.mSkillVars.HitTargets.Clear();
        if (this.mSkillVars.mSkillEffect.ProjectileSound != null)
          this.mSkillVars.mSkillEffect.ProjectileSound.Play();
        if (this.mSkillVars.UseBattleScene)
        {
          if (pd.mProjectileThread != null)
            return;
          pd.mProjectileThread = this.StartCoroutine(this.AnimateProjectile(this.mSkillVars.mSkillEffect.ProjectileStart, this.mSkillVars.mSkillEffect.ProjectileStartTime, pd.mProjectile.transform.position, Quaternion.identity, (TacticsUnitController.ProjectileStopEvent) null, pd));
        }
        else
        {
          Transform transform = this.transform;
          MapProjectile mapProjectile = GameUtility.RequireComponent<MapProjectile>(pd.mProjectile);
          if (this.mSkillVars.mSkillEffect.MapHitEffectType == SkillEffect.MapHitEffectTypes.EachHits)
          {
            float num1 = 0.0f;
            Vector3 rhs = this.mSkillVars.mTargetPosition - transform.position;
            rhs.y = 0.0f;
            rhs.Normalize();
            for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
            {
              float num2 = Vector3.Dot(this.mSkillVars.Targets[index].transform.position - transform.position, rhs);
              if ((double) num2 >= (double) num1)
                num1 = num2;
            }
            this.mSkillVars.mTargetPosition = transform.position + rhs * num1;
            this.mSkillVars.mTargetPosition.y = GameUtility.RaycastGround(this.mSkillVars.mTargetPosition).y;
          }
          this.ReflectTargetTypeToPos(ref this.mSkillVars.mTargetPosition);
          mapProjectile.StartCameraTargetPosition = transform.position;
          mapProjectile.EndCameraTargetPosition = this.mSkillVars.mTargetPosition;
          mapProjectile.GoalPosition = this.mSkillVars.mTargetPosition + Vector3.up * 0.5f;
          mapProjectile.Speed = this.mSkillVars.mSkillEffect.MapProjectileSpeed;
          mapProjectile.HitDelay = this.mSkillVars.mSkillEffect.MapProjectileHitDelay;
          mapProjectile.OnHit = new MapProjectile.HitEvent(this.OnProjectileHit);
          mapProjectile.OnDistanceUpdate = new MapProjectile.DistanceChangeEvent(this.OnProjectileDistanceChange);
          mapProjectile.mProjectileData = pd;
          if (this.mSkillVars.mSkillEffect.IsTeleportMode && (double) e.End > (double) e.Start + 0.100000001490116)
          {
            float num = e.End - e.Start;
            Vector3 vector3 = mapProjectile.EndCameraTargetPosition - mapProjectile.StartCameraTargetPosition;
            mapProjectile.Speed = vector3.magnitude / num;
          }
          if ((UnityEngine.Object) this.mSkillVars.mActiveCamera != (UnityEngine.Object) null && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Changing && this.mSkillVars.mSkillSequence.MapCameraType == SkillSequence.MapCameraTypes.None)
          {
            mapProjectile.CameraTransform = this.mSkillVars.mActiveCamera.transform;
            if (this.IsRangedRaySkillType() && this.mSkillVars.TotalHits > 1)
            {
              switch (this.mSkillVars.mSkillSequence.SkillType)
              {
                case SkillSequence.SkillTypes.RangedRayNoMovCmr:
                  mapProjectile.CameraTransform = (Transform) null;
                  break;
                case SkillSequence.SkillTypes.RangedRayFirstMovCmr:
                  if (this.mSkillVars.mNumShotCount > 1)
                  {
                    mapProjectile.CameraTransform = (Transform) null;
                    break;
                  }
                  break;
                case SkillSequence.SkillTypes.RangedRayLastMovCmr:
                  if (this.mSkillVars.mNumShotCount < this.mSkillVars.TotalHits)
                  {
                    mapProjectile.CameraTransform = (Transform) null;
                    break;
                  }
                  break;
              }
            }
          }
          if (this.mSkillVars.mSkillEffect.MapTrajectoryType != SkillEffect.TrajectoryTypes.Arrow)
            return;
          mapProjectile.IsArrow = true;
          float num3 = this.mMapTrajectoryHeight;
          if ((double) GameUtility.CalcDistance2D(mapProjectile.GoalPosition - mapProjectile.transform.position) <= 1.20000004768372)
          {
            mapProjectile.IsArrow = false;
            mapProjectile.Speed = this.mSkillVars.mSkillEffect.MapProjectileSpeed;
          }
          else
          {
            mapProjectile.TimeScale = this.mSkillVars.mSkillEffect.MapTrajectoryTimeScale;
            if ((double) this.mSkillVars.mStartPosition.y >= (double) num3)
              num3 = this.mSkillVars.mStartPosition.y;
            mapProjectile.TopHeight = num3 + 0.5f;
          }
        }
      }
      else if (e is CameraShake)
      {
        this.mSkillVars.mCameraShakeSeedX = UnityEngine.Random.value;
        this.mSkillVars.mCameraShakeSeedY = UnityEngine.Random.value;
      }
      else
      {
        if (e is SRPG.AnimEvents.TargetState && (UnityEngine.Object) this.mSkillVars.mTargetController != (UnityEngine.Object) null)
        {
          switch ((e as SRPG.AnimEvents.TargetState).State)
          {
            case SRPG.AnimEvents.TargetState.StateTypes.Stand:
              this.mSkillVars.mTargetController.PlayIdle(0.0f);
              break;
            case SRPG.AnimEvents.TargetState.StateTypes.Down:
              this.mSkillVars.mTargetController.PlayDown();
              break;
            case SRPG.AnimEvents.TargetState.StateTypes.Kirimomi:
              this.mSkillVars.mTargetController.PlayKirimomi();
              break;
          }
        }
        if (e is AuraFrame)
        {
          if (!this.mSkillVars.mAuraEnable || !((UnityEngine.Object) this.mSkillVars.mSkillEffect != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mSkillVars.mSkillEffect.AuraEffect != (UnityEngine.Object) null))
            return;
          AuraFrame auraFrame = e as AuraFrame;
          Vector3 spawnPos;
          Quaternion spawnRot;
          auraFrame.CalcPosition(this.UnitObject, this.mSkillVars.mSkillEffect.AuraEffect, out spawnPos, out spawnRot);
          GameObject gameObject = UnityEngine.Object.Instantiate((UnityEngine.Object) this.mSkillVars.mSkillEffect.AuraEffect, spawnPos, spawnRot) as GameObject;
          Transform transform1 = gameObject.transform;
          Transform transform2 = (Transform) null;
          if (!string.IsNullOrEmpty(auraFrame.BoneName))
            transform2 = GameUtility.findChildRecursively(this.UnitObject.transform, auraFrame.BoneName);
          if ((UnityEngine.Object) transform2 == (UnityEngine.Object) null)
            transform2 = this.GetCharacterRoot();
          transform1.parent = transform2;
          this.mSkillVars.mAuras.Add(gameObject);
        }
        else
        {
          if (!(e is UnitVoiceEvent))
            return;
          bool flag = this.mSkillVars.MaxPlayVoice == this.mSkillVars.NumPlayVoice;
          --this.mSkillVars.NumPlayVoice;
          if (flag)
          {
            if (this.mSkillVars.Skill.IsReactionSkill() && this.mSkillVars.Skill.IsDamagedSkill())
            {
              this.Unit.PlayBattleVoice("battle_0013");
              return;
            }
            if (this.ShowCriticalEffectOnHit)
            {
              this.Unit.PlayBattleVoice("battle_0012");
              return;
            }
          }
          float b = 0.0f;
          if (this.mSkillVars.Targets != null)
          {
            for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
            {
              TacticsUnitController target = this.mSkillVars.Targets[index];
              if (!target.ShouldDodgeHits)
                b = Mathf.Max((int) this.Unit.MaximumStatus.param.hp == 0 ? 1f : (float) (1.0 * (this.mSkillVars.TotalHits <= 1 ? (double) target.mHitInfo.GetTotalHpDamage() : (double) (target.mHitInfo.GetTotalHpDamage() / this.mSkillVars.TotalHits))) / (float) (int) this.Unit.MaximumStatus.param.hp, b);
            }
          }
          if ((double) b >= 0.75)
            this.Unit.PlayBattleVoice("battle_0033");
          else if ((double) b > 0.5)
            this.Unit.PlayBattleVoice("battle_0032");
          else
            this.Unit.PlayBattleVoice("battle_0031");
        }
      }
    }

    private bool IsRangedSkillType()
    {
      if (this.mSkillVars != null && this.mSkillVars.mSkillSequence != null)
      {
        switch (this.mSkillVars.mSkillSequence.SkillType)
        {
          case SkillSequence.SkillTypes.Ranged:
          case SkillSequence.SkillTypes.RangedRayNoMovCmr:
          case SkillSequence.SkillTypes.RangedRayFirstMovCmr:
          case SkillSequence.SkillTypes.RangedRayLastMovCmr:
            return true;
        }
      }
      return false;
    }

    private bool IsRangedRaySkillType()
    {
      if (this.mSkillVars != null && this.mSkillVars.mSkillSequence != null)
      {
        switch (this.mSkillVars.mSkillSequence.SkillType)
        {
          case SkillSequence.SkillTypes.RangedRayNoMovCmr:
          case SkillSequence.SkillTypes.RangedRayFirstMovCmr:
          case SkillSequence.SkillTypes.RangedRayLastMovCmr:
            return true;
        }
      }
      return false;
    }

    protected override void OnEvent(AnimEvent e, float time, float weight)
    {
      if (this.mSkillVars == null || !(e is CameraShake))
        return;
      this.mSkillVars.mCameraShakeOffset = (e as CameraShake).CalcOffset(time, this.mSkillVars.mCameraShakeSeedX, this.mSkillVars.mCameraShakeSeedY);
    }

    [DebuggerHidden]
    private IEnumerator AnimateProjectile(AnimationClip clip, float length, Vector3 basePosition, Quaternion baseRotation, TacticsUnitController.ProjectileStopEvent callback, TacticsUnitController.ProjectileData pd)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CAnimateProjectile\u003Ec__Iterator33() { length = length, clip = clip, pd = pd, basePosition = basePosition, baseRotation = baseRotation, callback = callback, \u003C\u0024\u003Elength = length, \u003C\u0024\u003Eclip = clip, \u003C\u0024\u003Epd = pd, \u003C\u0024\u003EbasePosition = basePosition, \u003C\u0024\u003EbaseRotation = baseRotation, \u003C\u0024\u003Ecallback = callback };
    }

    private void CalcEnemyPos(AnimationClip clip, float normalizedTime, out Vector3 pos, out Quaternion rotation)
    {
      GameObject go = new GameObject();
      GameObject gameObject = new GameObject("Enm_Distance_dummy");
      Transform transform1 = go.transform;
      Transform transform2 = gameObject.transform;
      transform2.SetParent(transform1, false);
      transform1.SetParent(this.transform, false);
      clip.SampleAnimation(go, normalizedTime * clip.length);
      pos = transform2.position + this.RootMotionInverse;
      Quaternion quaternion = transform2.localRotation * Quaternion.AngleAxis(90f, Vector3.right);
      rotation = transform1.rotation * quaternion;
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) go);
    }

    private void StopAura()
    {
      if (this.mSkillVars.mAuras.Count > 0)
      {
        for (int index = this.mSkillVars.mAuras.Count - 1; index >= 0; --index)
        {
          GameObject mAura = this.mSkillVars.mAuras[index];
          GameUtility.RequireComponent<OneShotParticle>(mAura);
          GameUtility.StopEmitters(mAura);
        }
        this.mSkillVars.mAuras.Clear();
      }
      this.mSkillVars.mAuraEnable = false;
    }

    public void PlayPreSkillAnimation(UnityEngine.Camera cam, Vector3 targetPos)
    {
      if (this.mSkillVars == null)
        return;
      this.mSkillVars.mTargetPosition = targetPos;
      this.mSkillVars.mActiveCamera = cam;
      this.GotoState<TacticsUnitController.State_PreSkill>();
    }

    public void PlayDamage(HitReactionTypes hitType)
    {
      if (this.mSkillVars != null || this.Unit.IsUnitCondition(EUnitCondition.Stone) || this.mUnit != null && this.mUnit.IsBreakObj)
        return;
      switch (hitType)
      {
        case HitReactionTypes.Normal:
          this.GotoState<TacticsUnitController.State_NormalDamage>();
          break;
        case HitReactionTypes.Aerial:
          this.GotoState<TacticsUnitController.State_AerialDamage>();
          break;
        case HitReactionTypes.Kirimomi:
          this.PlayKirimomi();
          break;
      }
    }

    public void PlayDodge(bool perfectAvoid, bool is_disp_popup = true)
    {
      if (is_disp_popup && (UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
      {
        if (perfectAvoid)
          SceneBattle.Instance.PopupPefectAvoid(this.CenterPosition, 0.0f);
        else
          SceneBattle.Instance.PopupMiss(this.CenterPosition, 0.0f);
      }
      if (this.mSkillVars != null || (UnityEngine.Object) this.FindAnimation("B_DGE") == (UnityEngine.Object) null)
        return;
      this.GotoState<TacticsUnitController.State_Dodge>();
    }

    public void LoadDyingAnimation()
    {
      this.LoadUnitAnimationAsync("B_DIE", "cmn_downstand0", false, false);
    }

    public void LoadDodgeAnimation()
    {
      if (this.mUnit != null && this.mUnit.IsBreakObj)
        return;
      this.LoadUnitAnimationAsync("B_DGE", "cmn_dodge0", false, false);
    }

    public void LoadDefendAnimations()
    {
      if (this.mUnit != null && this.mUnit.IsBreakObj)
        return;
      this.LoadUnitAnimationAsync("B_DEF", "cmn_guard0", false, false);
    }

    public void LoadDamageAnimations()
    {
      if (this.mUnit != null && this.mUnit.IsBreakObj)
        return;
      this.LoadUnitAnimationAsync("B_DMG0", "cmn_damage0", false, false);
      this.LoadUnitAnimationAsync("B_DMG1", "cmn_damageair0", false, false);
      this.LoadUnitAnimationAsync("B_DOWN", "cmn_down0", false, false);
    }

    public void LoadSkillSequence(SkillParam skillParam, bool loadJobAnimation, bool useBattleScene, bool is_cs = false, bool is_cs_sub = false)
    {
      this.mSkillVars = new TacticsUnitController.SkillVars();
      this.mSkillVars.UseBattleScene = useBattleScene;
      if (!useBattleScene)
        ;
      this.mSkillVars.Skill = skillParam;
      this.mSkillVars.mIsCollaboSkill = is_cs;
      this.mSkillVars.mIsCollaboSkillSub = is_cs_sub;
      this.AddLoadThreadCount();
      this.StartCoroutine(this.LoadSkillSequenceAsync(skillParam, loadJobAnimation));
    }

    public bool HasPreSkillAnimation
    {
      get
      {
        if (this.mSkillVars != null)
          return (UnityEngine.Object) this.mSkillVars.mSkillStartAnimation != (UnityEngine.Object) null;
        return false;
      }
    }

    public bool IsSkillParentPosZero
    {
      get
      {
        if (this.mSkillVars == null || (UnityEngine.Object) this.mSkillVars.mSkillAnimation == (UnityEngine.Object) null)
          return false;
        return this.mSkillVars.mSkillAnimation.IsParentPosZero;
      }
    }

    public bool IsPreSkillParentPosZero
    {
      get
      {
        if (this.mSkillVars == null || (UnityEngine.Object) this.mSkillVars.mSkillStartAnimation == (UnityEngine.Object) null)
          return false;
        return this.mSkillVars.mSkillStartAnimation.IsParentPosZero;
      }
    }

    [DebuggerHidden]
    private IEnumerator LoadSkillSequenceAsync(SkillParam skillParam, bool loadJobAnimation)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CLoadSkillSequenceAsync\u003Ec__Iterator34() { skillParam = skillParam, loadJobAnimation = loadJobAnimation, \u003C\u0024\u003EskillParam = skillParam, \u003C\u0024\u003EloadJobAnimation = loadJobAnimation, \u003C\u003Ef__this = this };
    }

    public void LoadSkillEffect(string skillEffectName, bool is_cs_sub = false)
    {
      this.mSkillVars.mSkillEffect = (SkillEffect) null;
      if (is_cs_sub)
        skillEffectName += "_sub";
      DebugUtility.Log("LoadSkillEffect: " + skillEffectName);
      if (string.IsNullOrEmpty(skillEffectName))
        return;
      this.AddLoadThreadCount();
      this.StartCoroutine(this.LoadSkillEffectAsync(skillEffectName));
    }

    public bool IsFinishedLoadSkillEffect()
    {
      return (UnityEngine.Object) this.mSkillVars.mSkillEffect != (UnityEngine.Object) null;
    }

    [DebuggerHidden]
    private IEnumerator LoadSkillEffectAsync(string skillEffectName)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CLoadSkillEffectAsync\u003Ec__Iterator35() { skillEffectName = skillEffectName, \u003C\u0024\u003EskillEffectName = skillEffectName, \u003C\u003Ef__this = this };
    }

    public SkillEffect LoadedSkillEffect
    {
      get
      {
        if (this.mSkillVars != null)
          return this.mSkillVars.mSkillEffect;
        return (SkillEffect) null;
      }
    }

    public void UnloadBattleAnimations()
    {
      this.UnloadAnimation("B_DMG0");
      this.UnloadAnimation("B_DMG1");
      this.UnloadAnimation("B_DOWN");
      this.UnloadAnimation("B_DGE");
      this.UnloadAnimation("B_DEF");
      this.UnloadAnimation("B_SKL");
      this.UnloadAnimation("B_CHA");
      this.UnloadAnimation("B_PRS");
      this.UnloadAnimation("B_BS");
      this.UnloadAnimation("B_RUN");
      this.UnloadAnimation("B_RUNL");
      this.LoadGenkidamaAnimation(false);
      this.UnloadAnimation("B_TOSS_LIFT");
      this.UnloadAnimation("B_TOSS_THROW");
      this.UnloadAnimation("B_TRANSFORM");
    }

    private void ResetSkill()
    {
      this.mCollideGround = true;
      if ((UnityEngine.Object) this.mSkillVars.mTargetController != (UnityEngine.Object) null)
        this.mSkillVars.mTargetController.mCollideGround = true;
      this.StopAura();
      this.mLastHitEffect = (GameObject) null;
      if (this.mSkillVars.HitTimerThread != null)
        this.StopCoroutine(this.mSkillVars.HitTimerThread);
      using (List<TacticsUnitController.ProjectileData>.Enumerator enumerator = this.mSkillVars.mProjectileDataLists.GetEnumerator())
      {
        while (enumerator.MoveNext())
          GameUtility.DestroyGameObject(enumerator.Current.mProjectile);
      }
      this.mSkillVars.mProjectileDataLists.Clear();
      this.mSkillVars.mNumShotCount = 0;
      this.UnloadBattleAnimations();
      if (this.UseSubEquipment)
        this.ResetSubEquipments();
      this.mSkillVars = (TacticsUnitController.SkillVars) null;
      this.mHitInfoSelf = (LogSkill.Target) null;
    }

    private void FinishSkill()
    {
      for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
      {
        LogSkill.Target mHitInfo = this.mSkillVars.Targets[index].mHitInfo;
        if ((mHitInfo.IsFailCondition() || mHitInfo.IsCureCondition() || mHitInfo.IsBuffEffect()) && !this.mSkillVars.Targets[index].Unit.IsDead)
          this.mSkillVars.Targets[index].UpdateBadStatus();
      }
      if (!this.mSkillVars.UseBattleScene)
        this.StepTo(this.mSkillVars.mStartPosition);
      else
        this.PlayIdle(0.0f);
      this.ResetSkill();
    }

    private int CountSkillUnitVoice()
    {
      int num = 0;
      if ((UnityEngine.Object) this.mSkillVars.mSkillAnimation != (UnityEngine.Object) null && this.mSkillVars.mSkillAnimation.events != null)
      {
        for (int index = 0; index < this.mSkillVars.mSkillAnimation.events.Length; ++index)
        {
          if (this.mSkillVars.mSkillAnimation.events[index] is UnitVoiceEvent)
            ++num;
        }
      }
      return num;
    }

    public void SetLastHitEffect(GameObject effect)
    {
      this.mLastHitEffect = effect;
    }

    public void SetHitInfo(LogSkill.Target target)
    {
      this.mHitInfo = target;
    }

    public void SetHitInfoSelf(LogSkill.Target selfTarget)
    {
      this.mHitInfoSelf = selfTarget;
    }

    public int CountSkillHits()
    {
      int num = 0;
      switch (this.mSkillVars.mSkillSequence.SkillType)
      {
        case SkillSequence.SkillTypes.Melee:
          if ((UnityEngine.Object) this.mSkillVars.mSkillAnimation != (UnityEngine.Object) null && this.mSkillVars.mSkillAnimation.events != null)
          {
            for (int index = 0; index < this.mSkillVars.mSkillAnimation.events.Length; ++index)
            {
              if (this.mSkillVars.mSkillAnimation.events[index] is HitFrame)
                ++num;
            }
            break;
          }
          break;
        case SkillSequence.SkillTypes.Ranged:
          num = 1;
          break;
        case SkillSequence.SkillTypes.RangedRayNoMovCmr:
        case SkillSequence.SkillTypes.RangedRayFirstMovCmr:
        case SkillSequence.SkillTypes.RangedRayLastMovCmr:
          if ((UnityEngine.Object) this.mSkillVars.mSkillAnimation != (UnityEngine.Object) null && this.mSkillVars.mSkillAnimation.events != null)
          {
            for (int index = 0; index < this.mSkillVars.mSkillAnimation.events.Length; ++index)
            {
              if (this.mSkillVars.mSkillAnimation.events[index] is ProjectileFrame)
                ++num;
            }
            break;
          }
          break;
      }
      return num;
    }

    private void SpawnHitEffect(Vector3 pos, bool isLast)
    {
      this.mSkillVars.mSkillEffect.SpawnExplosionEffect(0, pos, Quaternion.identity);
      if (!isLast || !((UnityEngine.Object) this.mLastHitEffect != (UnityEngine.Object) null))
        return;
      GameUtility.SpawnParticle(this.mLastHitEffect, pos, Quaternion.identity, (GameObject) null);
      TimeManager.StartHitSlow(0.1f, 0.3f);
    }

    private void SpawnDefendHitEffect(SkillData defSkill, int useCount, int useCountMax)
    {
      if (defSkill == null)
        return;
      string defendEffect = defSkill.SkillParam.defend_effect;
      if (string.IsNullOrEmpty(defendEffect) || (UnityEngine.Object) this.mDefendSkillEffects[defendEffect] == (UnityEngine.Object) null)
        return;
      GameObject defendSkillEffect = this.mDefendSkillEffects[defendEffect];
      if (!((UnityEngine.Object) defendSkillEffect != (UnityEngine.Object) null))
        return;
      Animator component = GameUtility.SpawnParticle(defendSkillEffect, this.transform.position, Quaternion.identity, (GameObject) null).GetComponent<Animator>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.SetInteger("skill_count", useCount);
      if (useCountMax <= 0)
        return;
      component.SetFloat("skill_count_norm", (float) useCount / (float) useCountMax);
    }

    private void HitDelayed(TacticsUnitController target)
    {
      if (!this.IsRangedRaySkillType())
      {
        this.mSkillVars.TotalHits = 1;
        this.mSkillVars.NumHitsLeft = 0;
      }
      if ((double) this.mSkillVars.mSkillEffect.HitColorBlendTime > 0.0)
        target.FadeBlendColor(this.mSkillVars.mSkillEffect.HitColor, this.mSkillVars.mSkillEffect.HitColorBlendTime);
      bool flag1 = true;
      HitReactionTypes rangedHitReactionType = this.mSkillVars.mSkillEffect.RangedHitReactionType;
      bool flag2 = target.ShouldDodgeHits;
      bool perfectAvoid = target.ShouldPerfectDodge;
      if (this.IsRangedRaySkillType() && target.mHitInfo.IsCombo())
      {
        int index = target.mHitInfo.hits.Count - 1 - this.mSkillVars.NumHitsLeft;
        if (0 <= index && index < target.mHitInfo.hits.Count)
        {
          flag2 = target.mHitInfo.hits[index].is_avoid;
          perfectAvoid = target.mHitInfo.hits[index].is_pf_avoid;
        }
      }
      if (rangedHitReactionType != HitReactionTypes.None && flag2)
      {
        target.PlayDodge(perfectAvoid, true);
        flag1 = this.mSkillVars.mSkillEffect.AlwaysExplode;
      }
      if (!flag1)
        return;
      this.HitTarget(target, this.mSkillVars.mSkillEffect.RangedHitReactionType, true);
      MonoSingleton<GameManager>.Instance.Player.OnDamageToEnemy(this.mUnit, target.Unit, target.mHitInfo.GetTotalHpDamage());
    }

    [DebuggerHidden]
    private IEnumerator AsyncHitTimer()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CAsyncHitTimer\u003Ec__Iterator36() { \u003C\u003Ef__this = this };
    }

    private void InternalStartSkill(TacticsUnitController[] targets, Vector3 targetPosition, UnityEngine.Camera activeCamera, bool doStateChange = true)
    {
      if (this.mSkillVars.mSkillSequence == null)
      {
        UnityEngine.Debug.LogError((object) "SkillSequence not loaded yet");
      }
      else
      {
        this.mCollideGround = !this.mSkillVars.UseBattleScene;
        this.mSkillVars.mActiveCamera = activeCamera;
        this.mSkillVars.Targets = new List<TacticsUnitController>((IEnumerable<TacticsUnitController>) targets);
        if (this.mSkillVars.UseBattleScene)
          this.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
        this.mSkillVars.HitTimerThread = this.StartCoroutine(this.AsyncHitTimer());
        this.mSkillVars.mStartPosition = this.transform.position;
        if (targets.Length == 1)
        {
          this.mSkillVars.mTargetController = targets[0];
          this.mSkillVars.mTargetControllerPosition = this.mSkillVars.mTargetController.transform.position;
          this.mSkillVars.mTargetController.mCollideGround = this.mCollideGround;
        }
        this.mSkillVars.mTargetPosition = !this.mSkillVars.UseBattleScene ? targetPosition : this.mSkillVars.mTargetControllerPosition;
        this.mSkillVars.MaxPlayVoice = this.mSkillVars.NumPlayVoice = this.CountSkillUnitVoice();
        this.mSkillVars.TotalHits = this.mSkillVars.NumHitsLeft = this.CountSkillHits();
        this.mSkillVars.mAuraEnable = (UnityEngine.Object) this.mSkillVars.mSkillEffect.AuraEffect != (UnityEngine.Object) null;
        this.mSkillVars.mCameraID = 0;
        this.mSkillVars.mChantCameraID = 0;
        this.mSkillVars.mSkillCameraID = 0;
        this.mSkillVars.mProjectileDataLists.Clear();
        this.mSkillVars.mNumShotCount = 0;
        if (doStateChange && this.mSkillVars.Skill.cast_type == ECastTypes.Jump)
        {
          this.GotoState<TacticsUnitController.State_JumpCastComplete>();
        }
        else
        {
          if ((UnityEngine.Object) this.mSkillVars.mSkillEffect != (UnityEngine.Object) null && this.mSkillVars.mSkillEffect.StartSound != null)
            this.mSkillVars.mSkillEffect.StartSound.Play();
          if (!doStateChange)
            return;
          if ((UnityEngine.Object) this.mSkillVars.mSkillChantAnimation != (UnityEngine.Object) null)
            this.GotoState<TacticsUnitController.State_SkillChant>();
          else if (this.mSkillVars.Skill.effect_type == SkillEffectTypes.Changing)
            this.GotoState<TacticsUnitController.State_ChangeGrid>();
          else if (this.mSkillVars.Skill.effect_type == SkillEffectTypes.Throw)
          {
            this.GotoState<TacticsUnitController.State_Throw>();
          }
          else
          {
            this.GotoState<TacticsUnitController.State_Skill>();
            if (!this.mSkillVars.Skill.IsTransformSkill() || this.mSkillVars.Targets.Count == 0)
              return;
            this.mSkillVars.Targets[0].PlayAfterTransform();
          }
        }
      }
    }

    public void StartSkill(Vector3 targetPosition, UnityEngine.Camera activeCamera, TacticsUnitController[] targets, Vector3[] hitGrids, SkillParam skill)
    {
      this.mSkillVars.HitGrids = hitGrids;
      this.InternalStartSkill(targets, targetPosition, activeCamera, true);
    }

    public void StartSkill(TacticsUnitController target, UnityEngine.Camera activeCamera, SkillParam skill)
    {
      this.InternalStartSkill(new TacticsUnitController[1]
      {
        target
      }, target.transform.position, activeCamera, true);
    }

    public void SetLandingGrid(Grid landing)
    {
      if (this.mSkillVars == null)
        return;
      this.mSkillVars.mLandingGrid = landing;
    }

    public void SetTeleportGrid(Grid teleport)
    {
      if (this.mSkillVars == null)
        return;
      this.mSkillVars.mTeleportGrid = teleport;
    }

    public void PlayKirimomi()
    {
      this.GotoState<TacticsUnitController.State_Kirimomi>();
    }

    public void PlayDown()
    {
      this.GotoState<TacticsUnitController.State_Down>();
    }

    public void SetHpCostSkill(int SkillHpCost)
    {
      this.mSkillVars.HpCostDamage = SkillHpCost;
    }

    private void OnProjectileDistanceChange(GameObject go, float distance)
    {
      MapProjectile component = go.GetComponent<MapProjectile>();
      if (this.mSkillVars.mSkillEffect.MapHitEffectType != SkillEffect.MapHitEffectTypes.EachHits)
        return;
      for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
      {
        if (!this.mSkillVars.HitTargets.Contains(this.mSkillVars.Targets[index]) && (double) component.CalcDepth(this.mSkillVars.Targets[index].transform.position) <= (double) distance + 0.5)
        {
          this.mSkillVars.HitTimers.Add(new TacticsUnitController.HitTimer(this.mSkillVars.Targets[index], Time.time + this.mSkillVars.mSkillEffect.MapProjectileHitDelay));
          this.mSkillVars.HitTargets.Add(this.mSkillVars.Targets[index]);
        }
      }
    }

    private void OnProjectileHit(TacticsUnitController.ProjectileData pd = null)
    {
      TacticsUnitController tacticsUnitController = this;
      if (!this.mSkillVars.mSkillEffect.IsTeleportMode)
        --tacticsUnitController.mSkillVars.NumHitsLeft;
      if (pd != null && (UnityEngine.Object) pd.mProjectile != (UnityEngine.Object) null)
      {
        GameUtility.StopEmitters(pd.mProjectile);
        GameUtility.RequireComponent<OneShotParticle>(pd.mProjectile);
        pd.mProjectile = (GameObject) null;
        tacticsUnitController.mSkillVars.mProjectileDataLists.Remove(pd);
      }
      if (!this.mSkillVars.mSkillEffect.IsTeleportMode)
      {
        bool flag = true;
        if (pd != null)
          flag = !pd.mIsNotSpawnLandingEffect;
        if ((UnityEngine.Object) tacticsUnitController.mSkillVars.mSkillEffect.TargetHitEffect != (UnityEngine.Object) null && flag)
          GameUtility.SpawnParticle(tacticsUnitController.mSkillVars.mSkillEffect.TargetHitEffect, tacticsUnitController.mSkillVars.mTargetPosition, Quaternion.identity, (GameObject) null);
        switch (this.mSkillVars.mSkillEffect.MapHitEffectType)
        {
          case SkillEffect.MapHitEffectTypes.TargetRadial:
            for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
            {
              Vector3 vector3 = this.mSkillVars.Targets[index].transform.position - this.mSkillVars.mTargetPosition;
              vector3.y = 0.0f;
              float num = Mathf.Abs(vector3.x) + Mathf.Abs(vector3.z);
              this.mSkillVars.HitTimers.Add(new TacticsUnitController.HitTimer(this.mSkillVars.Targets[index], Time.time + num * this.mSkillVars.mSkillEffect.MapHitEffectIntervals));
            }
            break;
          case SkillEffect.MapHitEffectTypes.EachTargets:
            for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
              this.mSkillVars.HitTimers.Add(new TacticsUnitController.HitTimer(this.mSkillVars.Targets[index], Time.time + (float) index * this.mSkillVars.mSkillEffect.MapHitEffectIntervals));
            break;
          case SkillEffect.MapHitEffectTypes.Directional:
            Vector3 position1 = tacticsUnitController.transform.position;
            position1.y = 0.0f;
            Vector3 forward = tacticsUnitController.transform.forward;
            forward.y = 0.0f;
            forward.Normalize();
            for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
            {
              Vector3 position2 = this.mSkillVars.Targets[index].transform.position;
              position2.y = 0.0f;
              float num = Mathf.Max(Vector3.Dot(position2 - position1, forward), 0.0f);
              this.mSkillVars.HitTimers.Add(new TacticsUnitController.HitTimer(this.mSkillVars.Targets[index], num * this.mSkillVars.mSkillEffect.MapHitEffectIntervals));
            }
            break;
          case SkillEffect.MapHitEffectTypes.InstigatorRadial:
            for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
            {
              Vector3 vector3 = this.mSkillVars.Targets[index].transform.position - this.transform.position;
              vector3.y = 0.0f;
              float num = Mathf.Abs(vector3.x) + Mathf.Abs(vector3.z);
              this.mSkillVars.HitTimers.Add(new TacticsUnitController.HitTimer(this.mSkillVars.Targets[index], Time.time + num * this.mSkillVars.mSkillEffect.MapHitEffectIntervals));
            }
            break;
        }
      }
      if (pd == null)
        return;
      pd.mProjectileHitsTarget = true;
    }

    public bool IsJumpCant()
    {
      if (this.Unit.CastSkill != null)
        return ECastTypes.Jump == this.Unit.CastSkill.CastType;
      return false;
    }

    public bool IsCastJumpStartComplete()
    {
      return this.mCastJumpStartComplete;
    }

    public bool IsCastJumpFallComplete()
    {
      return this.mCastJumpFallComplete;
    }

    public void CastJump()
    {
      this.GotoState<TacticsUnitController.State_JumpCast>();
    }

    public void CastJumpFall()
    {
    }

    public void SetCastJump()
    {
      this.CollideGround = true;
      this.SetVisible(false);
      this.mCastJumpStartComplete = true;
      this.mCastJumpFallComplete = false;
      Vector3 position = this.transform.position;
      position.y += this.mCastJumpOffsetY;
      this.transform.position = position;
    }

    public void HideGimmickForTargetGrid(TacticsUnitController target)
    {
      if (!(bool) ((UnityEngine.Object) target))
        return;
      SceneBattle instance = SceneBattle.Instance;
      if (!(bool) ((UnityEngine.Object) instance))
        return;
      Grid unitGridPosition = instance.Battle.GetUnitGridPosition(target.Unit);
      Unit gimmickAtGrid = instance.Battle.FindGimmickAtGrid(unitGridPosition, true, (Unit) null);
      TacticsUnitController unitController = instance.FindUnitController(gimmickAtGrid);
      if (!((UnityEngine.Object) unitController != (UnityEngine.Object) null) || unitController.Unit.IsBreakObj)
        return;
      unitController.ScaleHide();
    }

    private void createEffect(TacticsUnitController target, GameObject go_effect)
    {
      GameObject go = UnityEngine.Object.Instantiate((UnityEngine.Object) go_effect, Vector3.zero, Quaternion.identity) as GameObject;
      if (!(bool) ((UnityEngine.Object) go))
        return;
      go.transform.SetParent(target.transform, false);
      GameUtility.RequireComponent<OneShotParticle>(go);
    }

    public List<TacticsUnitController> GetTargetTucLists()
    {
      if (this.mSkillVars == null || this.mSkillVars.Targets == null)
        return new List<TacticsUnitController>();
      return this.mSkillVars.Targets;
    }

    public Grid KnockBackGrid
    {
      set
      {
        this.mKnockBackMode = TacticsUnitController.eKnockBackMode.IDLE;
        this.mKnockBackGrid = value;
      }
    }

    public bool IsBusy
    {
      get
      {
        if (this.isIdle)
          return this.mKnockBackMode != TacticsUnitController.eKnockBackMode.IDLE;
        return true;
      }
    }

    private void execKnockBack()
    {
      if (this.mKnockBackMode == TacticsUnitController.eKnockBackMode.IDLE)
        return;
      if (this.mKnockBackGrid == null)
      {
        this.mKnockBackMode = TacticsUnitController.eKnockBackMode.IDLE;
      }
      else
      {
        SceneBattle instance = SceneBattle.Instance;
        if (!(bool) ((UnityEngine.Object) instance))
        {
          this.mKnockBackMode = TacticsUnitController.eKnockBackMode.IDLE;
        }
        else
        {
          switch (this.mKnockBackMode)
          {
            case TacticsUnitController.eKnockBackMode.START:
              this.mKbPosStart = this.CenterPosition;
              this.mKbPosEnd = instance.CalcGridCenter(this.mKnockBackGrid);
              this.mKbPassedSec = 0.0f;
              this.mKnockBackMode = TacticsUnitController.eKnockBackMode.EXEC;
              this.createEffect(this, instance.KnockBackEffect);
              break;
            case TacticsUnitController.eKnockBackMode.EXEC:
              this.mKbPassedSec += Time.deltaTime;
              float num = this.mKbPassedSec / 0.4f;
              if ((double) num >= 1.0)
              {
                this.transform.position = this.mKbPosEnd;
                this.HideGimmickForTargetGrid(this);
                this.mKnockBackMode = TacticsUnitController.eKnockBackMode.IDLE;
                break;
              }
              this.transform.position = Vector3.Lerp(this.mKbPosStart, this.mKbPosEnd, num * (2f - num));
              break;
          }
        }
      }
    }

    public Vector3 GetTargetPos()
    {
      if (this.mSkillVars == null)
        return Vector3.zero;
      if (this.mSkillVars.mTeleportGrid != null)
      {
        SceneBattle instance = SceneBattle.Instance;
        if ((bool) ((UnityEngine.Object) instance))
          return instance.CalcGridCenter(this.mSkillVars.mTeleportGrid.x, this.mSkillVars.mTeleportGrid.y);
      }
      return this.transform.position;
    }

    public void SetStartPos(Vector3 pos)
    {
      if (this.mSkillVars == null)
        return;
      this.mSkillVars.mStartPosition = pos;
    }

    public void LookAtTarget()
    {
      if (this.mSkillVars == null || this.mSkillVars.Skill == null || (!this.mSkillVars.Skill.IsTargetTeleport || this.mSkillVars.Targets == null) || this.mSkillVars.Targets.Count == 0)
        return;
      TacticsUnitController target = this.mSkillVars.Targets[0];
      if (!(bool) ((UnityEngine.Object) target))
        return;
      this.LookAt(target.CenterPosition);
    }

    public void ReflectDispModel()
    {
      if (this.mUnit == null || !this.mUnit.IsBreakObj || (!this.SetActivateUnitObject(this.Unit.GetMapBreakNowStage((int) this.Unit.CurrentStatus.param.hp)) || !(bool) ((UnityEngine.Object) this.mBadStatusEffect)) || (this.mBadStatus == null || !((UnityEngine.Object) this.mBadStatus.Effect != (UnityEngine.Object) null)))
        return;
      this.attachBadStatusEffect(this.mBadStatusEffect, this.mBadStatus.AttachTarget, true);
    }

    public void PlayTrickKnockBack(bool is_dmg_anm = false)
    {
      if (this.mKnockBackGrid == null)
        return;
      if (is_dmg_anm)
        this.PlayDamage(HitReactionTypes.Normal);
      this.mKnockBackMode = TacticsUnitController.eKnockBackMode.START;
    }

    public string GetAnmNameTransformSkill()
    {
      if (this.mSkillVars == null || this.mSkillVars.mSkillSequence == null)
        return (string) null;
      if (string.IsNullOrEmpty(this.mSkillVars.mSkillSequence.SkillAnimation.Name))
        return (string) null;
      return this.mSkillVars.mSkillSequence.SkillAnimation.Name + "_chg";
    }

    public void LoadTransformAnimation(string name)
    {
      if (!((UnityEngine.Object) this.FindAnimation("B_TRANSFORM") == (UnityEngine.Object) null))
        return;
      this.LoadUnitAnimationAsync("B_TRANSFORM", name, false, false);
    }

    private void PlayAfterTransform()
    {
      if ((UnityEngine.Object) this.FindAnimation("B_TRANSFORM") == (UnityEngine.Object) null)
        return;
      this.GotoState<TacticsUnitController.State_AfterTransform>();
    }

    public void DirectionOff_LoadSkill(SkillParam skillParam, bool is_cs = false, bool is_cs_sub = false)
    {
      this.mSkillVars = new TacticsUnitController.SkillVars();
      this.mSkillVars.Skill = skillParam;
      this.mSkillVars.mIsCollaboSkill = is_cs;
      this.mSkillVars.mIsCollaboSkillSub = is_cs_sub;
      this.AddLoadThreadCount();
      this.StartCoroutine(this.LoadSkillSequenceAsync(skillParam, false));
      if (string.IsNullOrEmpty(skillParam.effect))
        return;
      this.LoadSkillEffect(skillParam.effect, false);
    }

    public void DirectionOff_EndSkill()
    {
      this.SkillEffectSelf();
      this.FinishSkill();
    }

    public void DirectionOff_StartSkill(Vector3 targetPosition, UnityEngine.Camera activeCamera, TacticsUnitController[] targets, Vector3[] hitGrids, SkillParam skill)
    {
      this.mSkillVars.HitGrids = hitGrids;
      this.InternalStartSkill(targets, targetPosition, activeCamera, false);
    }

    public bool DirectionOff_OnEventStart()
    {
      if (this.mSkillVars == null)
        return false;
      --this.mSkillVars.NumHitsLeft;
      if (this.mSkillVars.NumHitsLeft < 0)
        this.mSkillVars.NumHitsLeft = 0;
      bool flag1 = false;
      for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
      {
        if (this.mSkillVars.Targets[index].mHitInfo.IsCombo())
          flag1 = true;
      }
      if (!flag1)
        this.mSkillVars.NumHitsLeft = 0;
      for (int index1 = 0; index1 < this.mSkillVars.Targets.Count; ++index1)
      {
        TacticsUnitController target = this.mSkillVars.Targets[index1];
        bool flag2 = target.ShouldDodgeHits;
        bool perfectAvoid = target.ShouldPerfectDodge;
        if (target.mHitInfo.IsCombo())
        {
          int index2 = target.mHitInfo.hits.Count - 1 - this.mSkillVars.NumHitsLeft;
          if (0 <= index2 && index2 < target.mHitInfo.hits.Count)
          {
            flag2 = target.mHitInfo.hits[index2].is_avoid;
            perfectAvoid = target.mHitInfo.hits[index2].is_pf_avoid;
          }
        }
        if (flag2)
        {
          bool is_disp_popup = true;
          if (this.mSkillVars.UseBattleScene)
            is_disp_popup = this.mSkillVars.NumHitsLeft == this.mSkillVars.TotalHits - 1;
          if (target.mHitInfo.IsCombo() || this.mSkillVars.NumHitsLeft == 0)
          {
            this.mSkillVars.Targets[index1].PlayDodge(perfectAvoid, is_disp_popup);
            if (this.mSkillVars.mSkillEffect.AlwaysExplode && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Changing && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Throw)
              this.SpawnHitEffect(this.mSkillVars.Targets[index1].transform.position, this.mSkillVars.NumHitsLeft == 0);
          }
        }
        else if (target.mHitInfo.IsCombo() || this.mSkillVars.NumHitsLeft == 0)
        {
          this.HitTarget(this.mSkillVars.Targets[index1], this.mSkillVars.mSkillEffect.RangedHitReactionType, false);
          MonoSingleton<GameManager>.Instance.Player.OnDamageToEnemy(this.mUnit, target.Unit, target.mHitInfo.GetTotalHpDamage());
        }
      }
      bool flag3 = this.mSkillVars.MaxPlayVoice == this.mSkillVars.NumPlayVoice;
      string cueID = "battle_0031";
      --this.mSkillVars.NumPlayVoice;
      if (flag3)
      {
        if (this.mSkillVars.Skill.IsReactionSkill() && this.mSkillVars.Skill.IsDamagedSkill())
          cueID = "battle_0013";
        if (this.ShowCriticalEffectOnHit)
          cueID = "battle_0012";
        if (!string.IsNullOrEmpty(cueID))
        {
          float b = 0.0f;
          if (this.mSkillVars.Targets != null)
          {
            for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
            {
              TacticsUnitController target = this.mSkillVars.Targets[index];
              if (!target.ShouldDodgeHits)
                b = Mathf.Max((int) this.Unit.MaximumStatus.param.hp == 0 ? 1f : (float) (1.0 * (this.mSkillVars.TotalHits <= 1 ? (double) target.mHitInfo.GetTotalHpDamage() : (double) (target.mHitInfo.GetTotalHpDamage() / this.mSkillVars.TotalHits))) / (float) (int) this.Unit.MaximumStatus.param.hp, b);
            }
          }
          if ((double) b >= 0.75)
            cueID = "battle_0033";
          if ((double) b > 0.5)
            cueID = "battle_0032";
        }
        this.Unit.PlayBattleVoice(cueID);
      }
      return this.mSkillVars.NumHitsLeft > 0;
    }

    private class State_Skill_Without_Animation : TacticsUnitController.State
    {
      private TacticsUnitController.CameraState mStartCameraState;
      private Quaternion mTargetRotation;
      private float mWaitTime;

      public override void Begin(TacticsUnitController self)
      {
        if (self.mSkillVars.UseBattleScene)
          this.mStartCameraState.CacheCurrent(self.mSkillVars.mActiveCamera);
        if (self.mSkillVars.mSkillEffect.StopAura == SkillEffect.AuraStopTimings.BeforeHit)
          self.StopAura();
        if ((UnityEngine.Object) self.mSkillVars.mTargetController != (UnityEngine.Object) null)
          this.mTargetRotation = self.mSkillVars.mTargetController.transform.rotation;
        this.mWaitTime = self.mSkillVars.mSkillEffect.ProjectileStartTime;
        --self.mSkillVars.NumHitsLeft;
        for (int index = 0; index < self.mSkillVars.Targets.Count; ++index)
        {
          if (self.mSkillVars.Targets[index].ShouldDodgeHits)
          {
            self.mSkillVars.Targets[index].PlayDodge(self.mSkillVars.Targets[index].ShouldPerfectDodge, true);
            if (self.mSkillVars.mSkillEffect.AlwaysExplode && self.mSkillVars.Skill.effect_type != SkillEffectTypes.Changing)
              self.SpawnHitEffect(self.mSkillVars.Targets[index].transform.position, self.mSkillVars.NumHitsLeft == 0);
          }
          else
            self.HitTarget(self.mSkillVars.Targets[index], HitReactionTypes.None, true);
        }
      }

      public override void End(TacticsUnitController self)
      {
        if (self.mSkillVars.mSkillEffect.StopAura != SkillEffect.AuraStopTimings.AfterHit)
          return;
        self.StopAura();
      }

      public override void Update(TacticsUnitController self)
      {
        if (self.mSkillVars.mSkillSequence.SkillType == SkillSequence.SkillTypes.Melee)
          self.GotoState<TacticsUnitController.State_SkillEnd>();
        else
          self.GotoState<TacticsUnitController.State_RangedSkillEnd>();
      }
    }

    public enum PostureTypes
    {
      Combat,
      NonCombat,
    }

    public enum HPGaugeModes
    {
      Normal,
      Attack,
      Target,
      Change,
    }

    private class HideGimmickAnimation
    {
      private float mWait = 1f;
      private float mSpeed = 5f;
      private Vector3 mBaseScale;
      private TacticsUnitController mGimmickController;
      public bool IsHide;
      public bool Enable;
      private float mCurrentWait;

      public void Init(TacticsUnitController GimmickController)
      {
        if ((UnityEngine.Object) GimmickController == (UnityEngine.Object) null || GimmickController.Unit == null || (!GimmickController.Unit.IsGimmick || GimmickController.mUnit.IsBreakObj))
          return;
        this.mGimmickController = GimmickController;
        this.mBaseScale = this.mGimmickController.transform.localScale;
        this.mCurrentWait = this.mWait;
        this.Enable = false;
        this.IsHide = true;
      }

      public void ResetScale()
      {
        this.mCurrentWait = this.mWait;
        this.Enable = false;
        this.IsHide = true;
        this.mGimmickController.transform.localScale = this.mBaseScale;
      }

      public void Update()
      {
        if (!(bool) ((UnityEngine.Object) this.mGimmickController) || !this.Enable)
          return;
        if (this.IsHide)
        {
          if (0.0 < (double) this.mCurrentWait)
          {
            this.mCurrentWait -= this.mSpeed * Time.deltaTime;
            this.mGimmickController.transform.localScale = this.mBaseScale * (this.mCurrentWait / this.mWait);
            return;
          }
          this.mGimmickController.transform.localScale = Vector3.zero;
          this.mCurrentWait = 0.0f;
        }
        else
        {
          if ((double) this.mWait > (double) this.mCurrentWait)
          {
            this.mCurrentWait += this.mSpeed * Time.deltaTime;
            this.mGimmickController.transform.localScale = this.mBaseScale * (this.mCurrentWait / this.mWait);
            return;
          }
          this.mGimmickController.transform.localScale = this.mBaseScale;
          this.mCurrentWait = this.mWait;
        }
        this.Enable = false;
      }
    }

    private class State : SRPG.State<TacticsUnitController>
    {
    }

    private enum ColorBlendModes
    {
      None,
      Fade,
      Blink,
    }

    private class State_WaitResources : TacticsUnitController.State
    {
      public override void Update(TacticsUnitController self)
      {
        if (self.IsLoading)
          return;
        self.mLoadedPartially = true;
        if (self.mCharacterData.Movable)
        {
          self.LoadUnitAnimationAsync("RUN", TacticsUnitController.ANIM_RUN_FIELD, true, false);
          self.LoadUnitAnimationAsync("STEP", TacticsUnitController.ANIM_STEP, false, false);
          self.LoadUnitAnimationAsync("FLLP", TacticsUnitController.ANIM_FALL_LOOP, false, false);
          self.LoadUnitAnimationAsync("FLEN", TacticsUnitController.ANIM_FALL_END, false, false);
          self.LoadUnitAnimationAsync("CLMB", TacticsUnitController.ANIM_CLIMBUP, false, false);
        }
        self.PlayIdle(0.0f);
      }

      public override void End(TacticsUnitController self)
      {
        if (self.mUnit != null && self.mUnit.IsDead || self.KeepUnitHidden)
          return;
        self.SetVisible(true);
      }
    }

    private class State_Idle : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        if (self.mUnit != null && self.mUnit.IsBreakObj)
          return;
        string id = "IDLE";
        self.RootMotionMode = AnimationPlayer.RootMotionModes.Translate;
        self.UpdateBadStatus();
        if (self.mBadStatus != null && !string.IsNullOrEmpty(self.mBadStatus.AnimationName) && ((UnityEngine.Object) self.FindAnimation("BAD") != (UnityEngine.Object) null && self.mBadStatus.AnimationName == self.mLoadedBadStatusAnimation))
          id = "BAD";
        if ((double) self.GetTargetWeight(id) >= 1.0)
          return;
        self.PlayAnimation(id, true, self.mIdleInterpTime, 0.0f);
      }

      public override void Update(TacticsUnitController self)
      {
        if (self.mUnit != null && self.mUnit.IsBreakObj)
          return;
        string animationName = self.mBadStatus == null ? (string) null : self.mBadStatus.AnimationName;
        if (!string.IsNullOrEmpty(animationName))
        {
          if (!string.IsNullOrEmpty(self.mLoadedBadStatusAnimation) && self.mLoadedBadStatusAnimation != animationName)
          {
            if ((bool) ((UnityEngine.Object) self.FindAnimation("BAD")))
            {
              self.UnloadAnimation("BAD");
              self.StopAnimation("BAD");
              self.mLoadedBadStatusAnimation = (string) null;
            }
          }
          else if (string.IsNullOrEmpty(self.mLoadedBadStatusAnimation))
          {
            self.LoadUnitAnimationAsync("BAD", animationName, false, false);
            self.mLoadedBadStatusAnimation = animationName;
          }
          else if ((UnityEngine.Object) self.FindAnimation("BAD") != (UnityEngine.Object) null && (double) self.GetTargetWeight("BAD") < 1.0)
            self.PlayAnimation("BAD", true, 0.25f, 0.0f);
        }
        else if (!string.IsNullOrEmpty(self.mLoadedBadStatusAnimation) && (bool) ((UnityEngine.Object) self.FindAnimation("BAD")))
        {
          self.UnloadAnimation("BAD");
          self.mLoadedBadStatusAnimation = (string) null;
          self.PlayAnimation("IDLE", true, 0.25f, 0.0f);
        }
        if (!self.AutoUpdateRotation)
          return;
        self.UpdateRotation();
      }
    }

    private class State_LookAt : TacticsUnitController.State
    {
      private float mSpinTime = 0.25f;
      private float mSpinCount = 2f;
      private float mJumpHeight = 0.2f;
      private float mTime;
      private Transform mTransform;
      private Vector3 mStartPosition;
      private Quaternion mEndRotation;

      public override void Begin(TacticsUnitController self)
      {
        this.mSpinCount = self.mSpinCount;
        this.mSpinTime = this.mSpinCount * 0.125f;
        this.mJumpHeight = this.mSpinCount * 0.1f;
        this.mTransform = self.transform;
        Vector3 forward = self.mLookAtTarget - this.mTransform.position;
        forward.y = 0.0f;
        this.mStartPosition = this.mTransform.position;
        this.mEndRotation = Quaternion.LookRotation(forward);
      }

      public override void Update(TacticsUnitController self)
      {
        this.mTime += Time.deltaTime;
        float num = Mathf.Clamp01(this.mTime / this.mSpinTime);
        this.mTransform.rotation = Quaternion.AngleAxis((float) ((1.0 - (double) num) * (double) this.mSpinCount * 360.0), Vector3.up) * this.mEndRotation;
        Vector3 mStartPosition = this.mStartPosition;
        mStartPosition.y += Mathf.Sin(num * 3.141593f) * this.mJumpHeight;
        this.mTransform.position = mStartPosition;
        if ((double) num < 1.0)
          return;
        self.PlayIdle(0.0f);
      }
    }

    private class State_FieldAction : TacticsUnitController.State
    {
    }

    private class State_FieldActionClimpUp : TacticsUnitController.State_FieldAction
    {
    }

    private class State_FieldActionJumpUp : TacticsUnitController.State_FieldAction
    {
      private Vector3 mStartPosition;
      private Vector3 mEndPosition;
      private float mTime;
      private float mDuration;
      private bool mFalling;
      private float mLastY;

      public override void Begin(TacticsUnitController self)
      {
        this.mEndPosition = GameUtility.RaycastGround(self.mFieldActionPoint);
        this.mStartPosition = self.transform.position;
        self.PlayAnimation("CLMB", true, 0.1f, 0.0f);
        self.RootMotionMode = AnimationPlayer.RootMotionModes.Discard;
        self.mCollideGround = false;
        GameSettings instance = GameSettings.Instance;
        this.mDuration = instance.Unit_JumpMinTime + instance.Unit_JumpTimePerHeight * (this.mEndPosition.y - this.mStartPosition.y);
      }

      public override void End(TacticsUnitController self)
      {
        self.RootMotionMode = AnimationPlayer.RootMotionModes.Translate;
        self.mCollideGround = true;
      }

      public override void Update(TacticsUnitController self)
      {
        GameSettings instance = GameSettings.Instance;
        this.mTime += Time.deltaTime;
        float num1 = Mathf.Clamp01(this.mTime / this.mDuration);
        Vector3 zero = Vector3.zero;
        float t = instance.Unit_JumpZCurve.Evaluate(num1);
        float num2 = instance.Unit_JumpYCurve.Evaluate(num1);
        zero.x = Mathf.Lerp(this.mStartPosition.x, this.mEndPosition.x, t);
        zero.z = Mathf.Lerp(this.mStartPosition.z, this.mEndPosition.z, t);
        zero.y = Mathf.Lerp(this.mStartPosition.y, this.mEndPosition.y, num1) + num2;
        self.transform.position = zero;
        if (!this.mFalling && (double) num2 < (double) this.mLastY)
        {
          self.PlayAnimation("FLLP", true, 0.1f, 0.0f);
          this.mFalling = true;
        }
        this.mLastY = num2;
        if ((double) num1 < 1.0)
          return;
        self.mOnFieldActionEnd();
      }
    }

    private class State_FieldActionJump : TacticsUnitController.State_FieldAction
    {
      private Vector3 mStartPosition;
      private Vector3 mEndPosition;
      private float mTime;
      private float mAnimRate;
      private float mJumpHeight;

      public override void Begin(TacticsUnitController self)
      {
        this.mStartPosition = self.transform.position;
        this.mEndPosition = GameUtility.RaycastGround(self.mFieldActionPoint);
        float f = this.mEndPosition.y - this.mStartPosition.y;
        this.mJumpHeight = Mathf.Abs(f);
        if ((double) f < 0.0)
          this.mJumpHeight *= 0.5f;
        this.mAnimRate = (float) (1.0 / ((double) GameUtility.CalcDistance2D(this.mStartPosition, this.mEndPosition) / (double) GameSettings.Instance.Quest.MapRunSpeedMax));
        self.transform.rotation = self.mFieldActionDir.ToRotation();
        self.mCollideGround = false;
      }

      public override void Update(TacticsUnitController self)
      {
        this.mTime = Mathf.Clamp01(this.mTime + this.mAnimRate * Time.deltaTime);
        Vector3 vector3 = Vector3.Lerp(this.mStartPosition, this.mEndPosition, this.mTime);
        vector3.y += Mathf.Sin(this.mTime * 3.141593f) * this.mJumpHeight;
        if ((double) this.mTime >= 1.0)
        {
          self.transform.position = this.mEndPosition;
          self.mOnFieldActionEnd();
        }
        else
          self.transform.position = vector3;
      }

      public override void End(TacticsUnitController self)
      {
        self.mCollideGround = true;
      }
    }

    private class State_FieldActionFall : TacticsUnitController.State_FieldAction
    {
      private float mDuration = 0.5f;
      private Vector3 mStartPosition;
      private Vector3 mEndPosition;
      private float mTime;

      public override void Begin(TacticsUnitController self)
      {
        this.mStartPosition = self.transform.position;
        this.mEndPosition = GameUtility.RaycastGround(self.mFieldActionPoint);
        GameSettings instance = GameSettings.Instance;
        if ((UnityEngine.Object) instance != (UnityEngine.Object) null)
          this.mDuration = instance.Unit_FallMinTime + Mathf.Abs(this.mStartPosition.y - this.mEndPosition.y) * instance.Unit_FallTimePerHeight;
        self.transform.rotation = self.mFieldActionDir.ToRotation();
        self.mCollideGround = false;
        self.PlayAnimation("FLLP", true, 0.1f, 0.0f);
      }

      public override void Update(TacticsUnitController self)
      {
        this.mTime += Time.deltaTime;
        float t = Mathf.Clamp01(this.mTime / this.mDuration);
        Vector3 vector3 = Vector3.Lerp(this.mStartPosition, this.mEndPosition, t);
        vector3.y = Mathf.Lerp(this.mStartPosition.y, this.mEndPosition.y, 1f - Mathf.Cos((float) ((double) t * 3.14159274101257 * 0.5)));
        if ((double) t >= 1.0)
        {
          self.transform.position = this.mEndPosition;
          self.mOnFieldActionEnd();
        }
        else
          self.transform.position = vector3;
      }

      public override void End(TacticsUnitController self)
      {
        self.mCollideGround = true;
      }
    }

    private class State_RunLoop : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation(self.mRunAnimation, true);
      }

      public override void Update(TacticsUnitController self)
      {
      }

      public override void End(TacticsUnitController self)
      {
      }
    }

    private class State_StepNoAnimation : TacticsUnitController.State
    {
      public override void Update(TacticsUnitController self)
      {
        if (self.mCancelAction)
        {
          self.mCancelAction = false;
          self.PlayIdle(0.0f);
        }
        else
        {
          self.mStepStart = Vector3.Lerp(self.mStepStart, self.mStepEnd, Time.deltaTime * 10f);
          if ((double) (self.mStepStart - self.mStepEnd).magnitude < 0.00999999977648258)
          {
            self.transform.position = self.mStepEnd;
            self.PlayIdle(0.1f);
          }
          else
            self.transform.position = self.mStepStart;
        }
      }
    }

    private class State_Step : TacticsUnitController.State
    {
      private float mLandTime;

      public override void Begin(TacticsUnitController self)
      {
        AnimDef animation = self.GetAnimation("STEP");
        this.mLandTime = animation.Length;
        for (int index = 0; index < animation.events.Length; ++index)
        {
          if (animation.events[index] is SRPG.AnimEvents.Marker)
          {
            this.mLandTime = animation.events[index].End;
            break;
          }
        }
        self.PlayAnimation("STEP", false);
        self.SetSpeed("STEP", GameSettings.Instance.Quest.GridSnapSpeed);
      }

      public override void Update(TacticsUnitController self)
      {
        if (self.mCancelAction)
        {
          self.mCancelAction = false;
          self.PlayIdle(0.0f);
        }
        else
        {
          float remainingTime = self.GetRemainingTime("STEP");
          float t = Mathf.Clamp01((self.GetAnimation("STEP").Length - remainingTime) / this.mLandTime);
          Vector3 vector3 = Vector3.Lerp(self.mStepStart, self.mStepEnd, t);
          self.transform.position = vector3;
          if ((double) remainingTime > 0.0)
            return;
          self.PlayIdle(0.2f);
        }
      }
    }

    private class State_Move : TacticsUnitController.State
    {
      private Vector3 mStartPos;
      private bool mAdjustDirection;
      private Quaternion mDesiredRotation;
      private float mRotationRate;

      public override void Begin(TacticsUnitController self)
      {
        if (self.mRoutePos < self.mRoute.Length)
        {
          self.PlayAnimation(self.mRunAnimation, true, self.mMoveAnimInterpTime, 0.0f);
          this.LookToward(self, self.mRoute[self.mRoutePos] - self.transform.position);
          this.mStartPos = self.mRoute[self.mRoutePos];
        }
        else
          this.mStartPos = self.transform.position;
        if ((double) self.mPostMoveAngle < 0.0)
          return;
        this.mAdjustDirection = true;
        this.mDesiredRotation = Quaternion.AngleAxis(self.mPostMoveAngle, Vector3.up);
        this.mRotationRate = 720f;
      }

      public override void Update(TacticsUnitController self)
      {
        Transform transform = self.transform;
        if (self.mRoute.Length <= self.mRoutePos)
        {
          if (this.mAdjustDirection)
          {
            Quaternion rotation = transform.rotation;
            if ((double) Quaternion.Angle(rotation, this.mDesiredRotation) > 1.0)
            {
              transform.rotation = Quaternion.RotateTowards(rotation, this.mDesiredRotation, this.mRotationRate * Time.deltaTime);
              return;
            }
          }
          transform.rotation = this.mDesiredRotation;
          self.PlayIdle(0.0f);
        }
        else
        {
          Vector3 lhs = self.mRoute[self.mRoutePos] - this.mStartPos;
          lhs.y = 0.0f;
          float sqrMagnitude = lhs.sqrMagnitude;
          if ((double) sqrMagnitude <= 9.99999974737875E-05)
          {
            transform.position = self.mRoute[self.mRoutePos];
            ++self.mRoutePos;
            if (this.mAdjustDirection || self.mRoute.Length > self.mRoutePos)
              return;
            self.PlayIdle(0.0f);
          }
          else
          {
            if ((double) sqrMagnitude > 9.99999974737875E-05)
            {
              Vector3 vector3 = lhs;
              vector3.y = 0.0f;
              vector3.Normalize();
              if (self.TriggerFieldAction(vector3, true))
              {
                self.mMoveAnimInterpTime = 0.1f;
                return;
              }
              Vector3 a = self.transform.position + vector3 * self.mRunSpeed * Time.deltaTime;
              self.transform.position = a;
              float num1 = GameUtility.CalcDistance2D(a, self.mRoute[self.mRoutePos]);
              if ((double) num1 < 0.5 && self.mRoutePos < self.mRoute.Length - 1)
              {
                Vector3 forward = self.mRoute[self.mRoutePos + 1] - self.mRoute[self.mRoutePos];
                forward.y = 0.0f;
                forward.Normalize();
                float t = (float) ((1.0 - (double) num1 / 0.5) * 0.5);
                self.transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(vector3), Quaternion.LookRotation(forward), t);
              }
              else if (self.mRoutePos > 1)
              {
                float num2 = GameUtility.CalcDistance2D(a, self.mRoute[self.mRoutePos - 1]);
                if ((double) num2 < 0.5)
                {
                  Vector3 forward = self.mRoute[self.mRoutePos - 1] - self.mRoute[self.mRoutePos - 2];
                  forward.y = 0.0f;
                  forward.Normalize();
                  float t = (float) (0.5 + (double) num2 / 0.5 * 0.5);
                  self.transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(forward), Quaternion.LookRotation(vector3), t);
                }
                else
                  this.LookToward(self, vector3);
              }
              else
                this.LookToward(self, vector3);
            }
            Vector3 rhs = transform.position - this.mStartPos;
            rhs.y = 0.0f;
            if ((double) Vector3.Dot(lhs, rhs) / (double) sqrMagnitude < 0.999899983406067)
              return;
            this.mStartPos = self.mRoute[self.mRoutePos];
            self.transform.position = self.mRoute[self.mRoutePos];
            ++self.mRoutePos;
            if (this.mAdjustDirection || self.mRoute.Length > self.mRoutePos)
              return;
            self.PlayIdle(0.0f);
          }
        }
      }

      private void LookToward(TacticsUnitController self, Vector3 v)
      {
        v.y = 0.0f;
        if ((double) v.magnitude <= 9.99999974737875E-05)
          return;
        self.transform.rotation = Quaternion.LookRotation(v);
      }
    }

    private class State_Pickup : TacticsUnitController.State
    {
      private Vector3 mObjectStartPos;
      private Vector3 mTopPos;
      private bool mPickedUp;
      private float mPostPickupDelay;

      public override void Begin(TacticsUnitController self)
      {
        self.AutoUpdateRotation = false;
        this.mPostPickupDelay = GameSettings.Instance.Quest.WaitAfterUnitPickupGimmick;
        self.PlayAnimation("PICK", false);
        self.SetEquipmentsVisible(false);
        this.mObjectStartPos = self.mPickupObject.transform.position;
        this.mTopPos = this.mObjectStartPos;
        if ((UnityEngine.Object) self.mCharacterSettings != (UnityEngine.Object) null)
          this.mTopPos.y += self.mCharacterSettings.Height * self.transform.lossyScale.y;
        MapPickup componentInChildren = self.mPickupObject.GetComponentInChildren<MapPickup>();
        if (!((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null) || !((UnityEngine.Object) componentInChildren.Shadow != (UnityEngine.Object) null))
          return;
        componentInChildren.Shadow.gameObject.SetActive(false);
      }

      public override void Update(TacticsUnitController self)
      {
        Vector3 vector3 = Vector3.Lerp(this.mObjectStartPos, this.mTopPos, Mathf.Sin((float) ((double) self.GetNormalizedTime("PICK") * 3.14159274101257 * 0.5)));
        Transform transform = self.mPickupObject.transform;
        transform.rotation = Quaternion.LookRotation(Vector3.back);
        transform.position = vector3;
        if ((double) self.GetRemainingTime("PICK") <= 0.0)
        {
          if (!this.mPickedUp)
          {
            MapPickup componentInChildren = self.mPickupObject.GetComponentInChildren<MapPickup>();
            if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null && componentInChildren.OnPickup != null)
              componentInChildren.OnPickup();
            this.mPickedUp = true;
          }
          this.mPostPickupDelay -= Time.deltaTime;
        }
        if ((double) this.mPostPickupDelay > 0.0)
          return;
        self.GotoState<TacticsUnitController.State_PostPickup>();
      }
    }

    private class State_PostPickup : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        ObjectAnimator.Get(self.mPickupObject).ScaleTo(Vector3.zero, 0.2f, ObjectAnimator.CurveType.Linear);
        GameUtility.StopEmitters(self.mPickupObject);
        self.SetEquipmentsVisible(true);
      }

      public override void Update(TacticsUnitController self)
      {
        if (ObjectAnimator.Get(self.mPickupObject).isMoving)
          return;
        self.PlayIdle(0.0f);
      }

      public override void End(TacticsUnitController self)
      {
        self.mPickupObject = (GameObject) null;
        self.UnloadPickupAnimation();
      }
    }

    private class State_Taken : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("PICK", false);
      }
    }

    private struct CameraState
    {
      public Vector3 Position;
      public Quaternion Rotation;
      public static TacticsUnitController.CameraState Default;

      public void CacheCurrent(UnityEngine.Camera camera)
      {
        Transform transform = camera.transform;
        this.Position = transform.position;
        this.Rotation = transform.rotation;
      }
    }

    public enum PointType
    {
      Damage,
      Heal,
    }

    public struct OutputPoint
    {
      public int Value;
      public TacticsUnitController.PointType PointType;
      public bool Critical;
    }

    public class ProjectileData
    {
      public GameObject mProjectile;
      public Coroutine mProjectileThread;
      public bool mProjectileHitsTarget;
      public bool mProjStartAnimEnded;
      public bool mIsHitOnly;
      public bool mIsNotSpawnLandingEffect;
    }

    private class SkillVars
    {
      public List<GameObject> mAuras = new List<GameObject>(4);
      public Quaternion mCameraShakeOffset = Quaternion.identity;
      public List<TacticsUnitController.ProjectileData> mProjectileDataLists = new List<TacticsUnitController.ProjectileData>();
      public List<TacticsUnitController.OutputPoint> mOutputPoints = new List<TacticsUnitController.OutputPoint>(32);
      public List<TacticsUnitController.HitTimer> HitTimers = new List<TacticsUnitController.HitTimer>();
      public List<TacticsUnitController> HitTargets = new List<TacticsUnitController>(4);
      private int mDefaultLayer;
      public bool SuppressDamageOutput;
      public int HpCostDamage;
      public SkillParam Skill;
      public bool mIsCollaboSkill;
      public bool mIsCollaboSkillSub;
      public List<TacticsUnitController> Targets;
      public Vector3 mStartPosition;
      public Vector3 mTargetPosition;
      public Vector3 mTargetControllerPosition;
      public TacticsUnitController mTargetController;
      public bool mAuraEnable;
      public UnityEngine.Camera mActiveCamera;
      public StatusParam Param;
      public int mCameraID;
      public int mChantCameraID;
      public int mSkillCameraID;
      public float mCameraShakeSeedX;
      public float mCameraShakeSeedY;
      public int MaxPlayVoice;
      public int NumPlayVoice;
      public int TotalHits;
      public int NumHitsLeft;
      public SkillSequence mSkillSequence;
      public AnimDef mSkillChantAnimation;
      public AnimDef mSkillAnimation;
      public AnimDef mSkillEndAnimation;
      public AnimDef mSkillStartAnimation;
      public AnimationClip mSkillChantCameraClip;
      public AnimationClip mSkillCameraClip;
      public AnimationClip mSkillEndCameraClip;
      public GameObject mChantEffect;
      public int mNumShotCount;
      public bool mProjectileTriggered;
      public SkillEffect mSkillEffect;
      public bool UseBattleScene;
      public Vector3[] HitGrids;
      public Coroutine HitTimerThread;
      public Grid mLandingGrid;
      public Grid mTeleportGrid;
      public bool mIsFinishedBuffEffectTarget;
      public bool mIsFinishedBuffEffectSelf;
    }

    public enum EElementEffectTypes
    {
      NotEffective,
      Normal,
      Effective,
    }

    public class ShieldState
    {
      public Unit.UnitShield Target;
      public int LastHP;
      public int LastTurn;
      public bool Dirty;

      public void ClearDirty()
      {
        this.LastHP = (int) this.Target.hp;
        this.LastTurn = (int) this.Target.turn;
        this.Target.is_efficacy = (OBool) false;
        this.Dirty = false;
      }
    }

    private class ShakeUnit
    {
      private float mShakeSpeed = 0.0125f;
      private int mShakeMaxCount = 8;
      private int mShakeCount = 8;
      private Vector3 mShakeBasePos;
      private Vector3 mShakeOffset;
      private bool mIsShakeStart;

      public bool ShakeStart
      {
        set
        {
          this.mIsShakeStart = value;
        }
        get
        {
          return this.mIsShakeStart;
        }
      }

      public int ShakeMaxCount
      {
        set
        {
        }
        get
        {
          return this.mShakeMaxCount;
        }
      }

      public int ShakeCount
      {
        set
        {
        }
        get
        {
          return this.mShakeCount;
        }
      }

      public void Init(Vector3 basePosition, Vector3 direction)
      {
        GameSettings instance = GameSettings.Instance;
        this.mShakeSpeed = instance.ShakeUnit_Offset;
        this.mShakeMaxCount = instance.ShakeUnit_MaxCount;
        this.mShakeBasePos = basePosition;
        this.mShakeOffset = Vector3.Cross(direction, new Vector3(0.0f, 1f, 0.0f)).normalized;
        this.mShakeOffset *= this.mShakeSpeed;
        this.mShakeCount = this.mShakeMaxCount;
        this.mIsShakeStart = false;
      }

      public Vector3 AdvanceShake()
      {
        if (!this.mIsShakeStart)
          return this.mShakeBasePos;
        if (0 < this.mShakeCount)
        {
          if (this.mShakeCount % 2 == 0)
            this.mShakeOffset = -this.mShakeOffset;
          --this.mShakeCount;
          return this.mShakeBasePos + this.mShakeOffset;
        }
        this.mIsShakeStart = false;
        return this.mShakeBasePos;
      }
    }

    private struct HitTimer
    {
      public TacticsUnitController Target;
      public float HitTime;

      public HitTimer(TacticsUnitController target, float hitTime)
      {
        this.Target = target;
        this.HitTime = hitTime;
      }
    }

    [Flags]
    public enum DeathAnimationTypes
    {
      Normal = 1,
    }

    private class State_Dead : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.RootMotionMode = AnimationPlayer.RootMotionModes.Discard;
        self.PlayAnimation("B_DEAD", false);
        if (self.mUnit != null && self.mUnit.IsBreakObj)
          return;
        self.Unit.PlayBattleVoice("battle_0028");
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_6050", 0.0f);
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) self.GetRemainingTime("B_DEAD") > 0.0)
          return;
        UnityEngine.Object.Destroy((UnityEngine.Object) self.gameObject);
        if (self.mUnit != null && self.mUnit.IsBreakObj)
          return;
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_6051", 0.0f);
      }
    }

    private class State_Kirimomi : TacticsUnitController.State
    {
      private Quaternion mRotationOld;

      public override void Begin(TacticsUnitController self)
      {
        this.mRotationOld = self.UnitObject.transform.localRotation;
      }

      public override void Update(TacticsUnitController self)
      {
        self.UnitObject.transform.localRotation = Quaternion.AngleAxis(Time.time * GameSettings.Instance.Quest.KirimomiRotationRate, Vector3.up);
      }

      public override void End(TacticsUnitController self)
      {
        self.UnitObject.transform.localRotation = this.mRotationOld;
      }
    }

    private class State_Down : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("B_DOWN", true);
      }

      public override void End(TacticsUnitController self)
      {
      }
    }

    private class State_Dodge : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.Unit.PlayBattleVoice("battle_0017");
        self.PlayAnimation("B_DGE", false, 0.1f, 0.0f);
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) self.GetRemainingTime("B_DGE") > 0.100000001490116)
          return;
        self.PlayIdle(0.1f);
      }
    }

    private class State_Defend : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("B_DEF", false, 0.1f, 0.0f);
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) self.GetRemainingTime("B_DEF") > 0.100000001490116)
          return;
        self.PlayIdle(0.1f);
      }
    }

    private class State_NormalDamage : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("B_DMG0", false, 0.1f, 0.0f);
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) self.GetRemainingTime("B_DMG0") > 0.100000001490116)
          return;
        self.PlayIdle(0.1f);
      }
    }

    private class State_AerialDamage : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("B_DMG1", false, 0.1f, 0.0f);
      }
    }

    private class State_SkillChant : TacticsUnitController.State
    {
      private TacticsUnitController.CameraState mStartCameraState = new TacticsUnitController.CameraState();

      public override void Begin(TacticsUnitController self)
      {
        if (self.mSkillVars.UseBattleScene)
          this.mStartCameraState.CacheCurrent(self.mSkillVars.mActiveCamera);
        self.PlayAnimation("B_CHA", false);
        if (!((UnityEngine.Object) self.mSkillVars.mSkillEffect != (UnityEngine.Object) null) || self.mSkillVars.mSkillEffect.ChantSound == null)
          return;
        self.mSkillVars.mSkillEffect.ChantSound.Play();
      }

      public override void Update(TacticsUnitController self)
      {
        self.mSkillVars.mChantCameraID = self.mSkillVars.mCameraID;
        float normalizedTime = self.GetNormalizedTime("B_CHA");
        if (self.mSkillVars.UseBattleScene && !self.mSkillVars.mIsCollaboSkillSub && (UnityEngine.Object) self.mSkillVars.mSkillChantCameraClip != (UnityEngine.Object) null)
        {
          if (self.mSkillVars.mSkillSequence.InterpChantCamera)
            self.AnimateCameraInterpolated(self.mSkillVars.mSkillChantCameraClip, normalizedTime, 4f * normalizedTime, this.mStartCameraState, self.mSkillVars.mChantCameraID);
          else
            self.AnimateCamera(self.mSkillVars.mSkillChantCameraClip, normalizedTime, self.mSkillVars.mChantCameraID);
        }
        if ((double) normalizedTime < 1.0)
          return;
        if ((UnityEngine.Object) self.mSkillVars.mChantEffect != (UnityEngine.Object) null)
        {
          if ((UnityEngine.Object) self.mSkillVars.mChantEffect.GetComponentInChildren<ParticleSystem>() != (UnityEngine.Object) null)
          {
            GameUtility.StopEmitters(self.mSkillVars.mChantEffect);
            self.mSkillVars.mChantEffect.AddComponent<OneShotParticle>();
          }
          else
            UnityEngine.Object.DestroyImmediate((UnityEngine.Object) self.mSkillVars.mChantEffect);
          self.mSkillVars.mChantEffect = (GameObject) null;
        }
        if (self.mSkillVars.Skill.effect_type == SkillEffectTypes.Changing)
          self.GotoState<TacticsUnitController.State_ChangeGrid>();
        else if (self.mSkillVars.Skill.effect_type == SkillEffectTypes.Throw)
        {
          self.GotoState<TacticsUnitController.State_Throw>();
        }
        else
        {
          self.GotoState<TacticsUnitController.State_Skill>();
          if (!self.mSkillVars.Skill.IsTransformSkill() || self.mSkillVars.Targets.Count == 0)
            return;
          self.mSkillVars.Targets[0].PlayAfterTransform();
        }
      }

      public override void End(TacticsUnitController self)
      {
        if (self.mSkillVars.mSkillEffect.StopAura != SkillEffect.AuraStopTimings.AfterChant)
          return;
        self.StopAura();
      }
    }

    private class State_Skill : TacticsUnitController.State
    {
      private TacticsUnitController.CameraState mStartCameraState;
      private Quaternion mTargetRotation;
      private float mWaitTime;
      private bool mIsProcessed;

      public override void Begin(TacticsUnitController self)
      {
        if (self.mSkillVars.UseBattleScene)
          this.mStartCameraState.CacheCurrent(self.mSkillVars.mActiveCamera);
        self.PlayAnimation("B_SKL", false);
        if (self.mSkillVars.mSkillEffect.StopAura == SkillEffect.AuraStopTimings.BeforeHit)
          self.StopAura();
        if ((UnityEngine.Object) self.mSkillVars.mTargetController != (UnityEngine.Object) null)
          this.mTargetRotation = self.mSkillVars.mTargetController.transform.rotation;
        this.mWaitTime = self.mSkillVars.mSkillEffect.ProjectileStartTime;
      }

      public override void End(TacticsUnitController self)
      {
        if (self.mSkillVars.mSkillEffect.StopAura != SkillEffect.AuraStopTimings.AfterHit)
          return;
        self.StopAura();
      }

      public override void Update(TacticsUnitController self)
      {
        self.mSkillVars.mSkillCameraID = self.mSkillVars.mCameraID;
        this.mWaitTime = Mathf.Max(this.mWaitTime - Time.deltaTime, 0.0f);
        float normalizedTime = self.GetNormalizedTime("B_SKL");
        if (self.mSkillVars.UseBattleScene)
        {
          if (self.mSkillVars.mSkillAnimation.CurveNames.Contains("Enm_Distance_dummy") && (UnityEngine.Object) self.mSkillVars.mTargetController != (UnityEngine.Object) null)
          {
            Vector3 pos;
            Quaternion rotation;
            self.CalcEnemyPos(self.mSkillVars.mSkillAnimation.animation, normalizedTime, out pos, out rotation);
            Transform transform = self.mSkillVars.mTargetController.transform;
            transform.position = pos;
            transform.rotation = rotation * this.mTargetRotation;
          }
          if ((UnityEngine.Object) self.mSkillVars.mSkillCameraClip != (UnityEngine.Object) null && !self.mSkillVars.mIsCollaboSkillSub)
          {
            if (self.mSkillVars.mSkillSequence.InterpSkillCamera)
              self.AnimateCameraInterpolated(self.mSkillVars.mSkillCameraClip, normalizedTime, 4f * normalizedTime, this.mStartCameraState, self.mSkillVars.mSkillCameraID);
            else
              self.AnimateCamera(self.mSkillVars.mSkillCameraClip, normalizedTime, self.mSkillVars.mSkillCameraID);
          }
        }
        if ((double) self.GetRemainingTime("B_SKL") > 0.0)
          return;
        if (!this.mIsProcessed && self.mSkillVars.Skill.IsTransformSkill())
        {
          self.SetVisible(false);
          this.mIsProcessed = true;
        }
        if ((double) this.mWaitTime > 0.0)
          return;
        if (self.mSkillVars.mSkillSequence.SkillType == SkillSequence.SkillTypes.Melee)
          self.GotoState<TacticsUnitController.State_SkillEnd>();
        else
          self.GotoState<TacticsUnitController.State_RangedSkillEnd>();
      }
    }

    private class State_SkillEnd : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        if (!((UnityEngine.Object) self.FindAnimation("B_BS") != (UnityEngine.Object) null))
          return;
        self.PlayAnimation("B_BS", false, 0.1f, 0.0f);
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) self.GetRemainingTime("B_BS") > 0.0)
          return;
        self.SkillEffectSelf();
        self.FinishSkill();
      }
    }

    private class State_RangedSkillEnd : TacticsUnitController.State
    {
      private float mAnimationLength;
      private float mStateTime;
      private bool mUnitAnimationEnded;
      private bool mHit;
      private bool mProjEndAnimEnded;

      public override void Begin(TacticsUnitController self)
      {
        this.mAnimationLength = self.mSkillVars.mSkillSequence.EndLength;
        if (!self.mSkillVars.mProjectileTriggered)
          self.OnProjectileHit((TacticsUnitController.ProjectileData) null);
        if (!((UnityEngine.Object) self.FindAnimation("B_BS") != (UnityEngine.Object) null))
          return;
        self.PlayAnimation("B_BS", false, 0.1f, 0.0f);
      }

      public override void End(TacticsUnitController self)
      {
        self.mSkillVars.mTargetController = (TacticsUnitController) null;
      }

      private void OnHit(TacticsUnitController.ProjectileData pd)
      {
        this.self.OnProjectileHit(pd);
      }

      public override void Update(TacticsUnitController self)
      {
        this.mStateTime += Time.deltaTime;
        if (self.mSkillVars.UseBattleScene)
        {
          using (List<TacticsUnitController.ProjectileData>.Enumerator enumerator = self.mSkillVars.mProjectileDataLists.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              TacticsUnitController.ProjectileData current = enumerator.Current;
              if (!current.mProjStartAnimEnded && (UnityEngine.Object) current.mProjectile != (UnityEngine.Object) null)
              {
                if (current.mProjectileThread != null)
                  return;
                current.mProjStartAnimEnded = true;
                if ((UnityEngine.Object) self.mSkillVars.mTargetController != (UnityEngine.Object) null)
                {
                  current.mProjectileThread = self.StartCoroutine(self.AnimateProjectile(self.mSkillVars.mSkillEffect.ProjectileEnd, self.mSkillVars.mSkillEffect.ProjectileEndTime, self.mSkillVars.mTargetControllerPosition, Quaternion.identity, new TacticsUnitController.ProjectileStopEvent(this.OnHit), current));
                  ParticleSystem component = current.mProjectile.GetComponent<ParticleSystem>();
                  if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                    component.Clear(true);
                }
              }
            }
          }
          if (!self.mSkillVars.mIsCollaboSkillSub)
          {
            if ((UnityEngine.Object) self.mSkillVars.mSkillEndCameraClip != (UnityEngine.Object) null)
            {
              Vector3 pos;
              Quaternion rotation;
              self.CalcCameraPos(self.mSkillVars.mSkillEndCameraClip, Mathf.Clamp01(this.mStateTime / this.mAnimationLength), 0, out pos, out rotation);
              self.SetActiveCameraPosition(self.mSkillVars.mTargetControllerPosition + pos, self.mSkillVars.mCameraShakeOffset * rotation);
            }
            else
            {
              Transform battleCamera = GameSettings.Instance.Quest.BattleCamera;
              Vector3 position = battleCamera.position;
              Quaternion rotation = battleCamera.rotation;
              self.SetActiveCameraPosition(self.mSkillVars.mTargetControllerPosition + position, self.mSkillVars.mCameraShakeOffset * rotation);
            }
          }
        }
        if (!this.mUnitAnimationEnded && (double) self.GetRemainingTime("B_BS") <= 0.0)
        {
          if (self.mSkillVars.UseBattleScene)
            self.PlayAnimation("IDLE", true, 0.1f, 0.0f);
          this.mUnitAnimationEnded = true;
        }
        using (List<TacticsUnitController.ProjectileData>.Enumerator enumerator = self.mSkillVars.mProjectileDataLists.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            if (enumerator.Current.mProjectileThread != null)
              return;
          }
        }
        if ((double) this.mStateTime >= (double) this.mAnimationLength && this.mHit)
        {
          self.SkillEffectSelf();
          self.FinishSkill();
        }
        else
        {
          if (this.mHit || self.mSkillVars.HitTimers.Count > 0)
            return;
          bool flag = true;
          using (List<TacticsUnitController.ProjectileData>.Enumerator enumerator = self.mSkillVars.mProjectileDataLists.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              if (!enumerator.Current.mProjectileHitsTarget)
              {
                flag = false;
                break;
              }
            }
          }
          this.mHit = flag;
        }
      }
    }

    private class State_PreSkill : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("B_PRS", false);
        self.mSkillVars.mAuraEnable = false;
        self.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
      }

      public override void End(TacticsUnitController self)
      {
        self.RootMotionMode = AnimationPlayer.RootMotionModes.Translate;
      }

      public override void Update(TacticsUnitController self)
      {
        float normalizedTime = self.GetNormalizedTime("B_PRS");
        self.AnimateCamera(self.mSkillVars.mSkillStartAnimation.animation, normalizedTime, self.mSkillVars.mCameraID);
        if ((double) normalizedTime < 1.0)
          return;
        self.PlayIdle(0.0f);
      }
    }

    private class State_JumpCast : TacticsUnitController.State
    {
      private float mCastTime = 0.5f;
      private float mBasePosY;
      private float mElapsed;

      public override void Begin(TacticsUnitController self)
      {
        this.mBasePosY = self.transform.position.y;
        this.mElapsed = 0.0f;
        self.CollideGround = false;
        self.mCastJumpStartComplete = false;
        self.mCastJumpFallComplete = false;
        self.PlayAnimation("CLMB", false, this.mCastTime / 10f, 0.0f);
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_7036", 0.0f);
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) this.mCastTime <= (double) this.mElapsed)
        {
          self.PlayIdle(0.0f);
        }
        else
        {
          this.mElapsed += Time.deltaTime;
          if ((double) this.mCastTime < (double) this.mElapsed)
            this.mElapsed = this.mCastTime;
          Vector3 position = self.transform.position;
          position.y = this.mBasePosY + self.mCastJumpOffsetY * (this.mElapsed / this.mCastTime);
          self.transform.position = position;
        }
      }

      public override void End(TacticsUnitController self)
      {
        self.CollideGround = true;
        self.SetVisible(false);
        self.mCastJumpStartComplete = true;
      }
    }

    private class State_JumpCastComplete : TacticsUnitController.State
    {
      private static readonly Color SceneFadeColor = new Color(0.2f, 0.2f, 0.2f, 1f);
      private float mCastTime = 0.3f;
      private float mFallStartWait = 0.7f;
      private float TransWaitTime = 0.1f;
      private bool beforeVisible = true;
      private float ReturnTime = 0.3f;
      private TacticsUnitController.State_JumpCastComplete.MotionMode Mode;
      private GameObject mFallEffect;
      private GameObject mHitEffect;
      private Vector3 mBasePos;
      private IntVector2 mBaseMapPos;
      private float mElapsed;
      private TacticsUnitController[] mExcludes;
      private float TransStartTime;
      private bool isDirect;

      public override void Begin(TacticsUnitController self)
      {
        this.mBasePos = self.transform.position;
        this.mBaseMapPos = new IntVector2(self.Unit.x, self.Unit.y);
        if (self.mSkillVars.mLandingGrid != null)
          this.mBaseMapPos = new IntVector2(self.mSkillVars.mLandingGrid.x, self.mSkillVars.mLandingGrid.y);
        this.mElapsed = 0.0f;
        self.mCastJumpFallComplete = false;
        self.CollideGround = false;
        this.Mode = TacticsUnitController.State_JumpCastComplete.MotionMode.FALL_WAIT;
        self.AnimateVessel(1f, 0.0f);
        if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
        {
          SceneBattle instance = SceneBattle.Instance;
          this.mExcludes = instance.GetActiveUnits();
          Array.Resize<TacticsUnitController>(ref this.mExcludes, this.mExcludes.Length + 1);
          this.mExcludes[this.mExcludes.Length - 1] = self;
          FadeController.Instance.BeginSceneFade(TacticsUnitController.State_JumpCastComplete.SceneFadeColor, 0.5f, this.mExcludes, (TacticsUnitController[]) null);
          this.mBasePos = instance.CalcGridCenter(this.mBaseMapPos.x, this.mBaseMapPos.y);
          self.mSkillVars.mStartPosition = this.mBasePos;
        }
        self.PlayAnimation("FLLP", true, 0.0f, 0.0f);
      }

      private void FallWaitUpdate(TacticsUnitController self)
      {
        if ((double) this.mElapsed >= (double) this.mFallStartWait)
        {
          this.EnterFall();
        }
        else
        {
          this.mElapsed += Time.deltaTime;
          if ((double) this.mElapsed < (double) this.mFallStartWait)
            return;
          this.mElapsed = this.mFallStartWait;
        }
      }

      private void EnterFall()
      {
        this.self.SetVisible(true);
        this.self.SetEquipmentsVisible(true);
        if (this.self.mSkillVars != null && (UnityEngine.Object) this.self.mSkillVars.mSkillEffect.AuraEffect != (UnityEngine.Object) null)
        {
          this.mFallEffect = UnityEngine.Object.Instantiate((UnityEngine.Object) this.self.mSkillVars.mSkillEffect.AuraEffect, this.self.transform.position, this.self.transform.rotation) as GameObject;
          this.mFallEffect.transform.SetParent(this.self.transform);
        }
        Vector3 position = this.self.transform.position;
        position.y = this.self.mCastJumpOffsetY;
        this.self.transform.position = position;
        this.Mode = TacticsUnitController.State_JumpCastComplete.MotionMode.FALL;
        this.mElapsed = 0.0f;
      }

      private void FallUpdate(TacticsUnitController self)
      {
        if ((double) this.mElapsed >= (double) this.mCastTime)
        {
          if (self.mSkillVars != null && (UnityEngine.Object) self.mSkillVars.mSkillEffect.TargetHitEffect != (UnityEngine.Object) null)
            this.mHitEffect = UnityEngine.Object.Instantiate((UnityEngine.Object) self.mSkillVars.mSkillEffect.TargetHitEffect, self.JumpFallPos, Quaternion.identity) as GameObject;
          this.Mode = TacticsUnitController.State_JumpCastComplete.MotionMode.RETURN;
          this.EnterReturn();
        }
        else
        {
          this.mElapsed += Time.deltaTime;
          if ((double) this.mElapsed >= (double) this.mCastTime)
            this.mElapsed = this.mCastTime;
          Vector3 jumpFallPos = self.JumpFallPos;
          jumpFallPos.y = self.JumpFallPos.y + self.mCastJumpOffsetY * (float) (1.0 - (double) this.mElapsed / (double) this.mCastTime);
          self.transform.position = jumpFallPos;
        }
      }

      private void EnterReturn()
      {
        if ((UnityEngine.Object) this.mFallEffect != (UnityEngine.Object) null)
        {
          GameUtility.RequireComponent<OneShotParticle>(this.mFallEffect);
          GameUtility.StopEmitters(this.mFallEffect);
        }
        if ((UnityEngine.Object) this.mHitEffect != (UnityEngine.Object) null)
          GameUtility.RequireComponent<OneShotParticle>(this.mHitEffect);
        for (int index = 0; index < this.self.mSkillVars.Targets.Count; ++index)
        {
          Vector3 vector3 = this.self.mSkillVars.Targets[index].transform.position - this.self.mSkillVars.mTargetPosition;
          vector3.y = 0.0f;
          float num = Mathf.Abs(vector3.x) + Mathf.Abs(vector3.z);
          this.self.mSkillVars.HitTimers.Add(new TacticsUnitController.HitTimer(this.self.mSkillVars.Targets[index], Time.time + num * this.self.mSkillVars.mSkillEffect.MapHitEffectIntervals));
        }
        this.self.PlayAnimation("STEP", false);
        float num1 = (float) (this.mBaseMapPos.x - this.self.JumpMapFallPos.x);
        float num2 = (float) (this.mBaseMapPos.y - this.self.JumpMapFallPos.y);
        if ((double) num1 * (double) num1 <= 1.0 && (double) num2 * (double) num2 <= 1.0)
          this.isDirect = true;
        this.mElapsed = 0.0f;
        this.TransStartTime = Time.time;
      }

      private void ReturnUpdate(TacticsUnitController self)
      {
        if ((double) this.TransWaitTime > (double) Time.time - (double) this.TransStartTime)
          return;
        this.mElapsed += Time.deltaTime;
        if ((double) this.mElapsed >= (double) this.ReturnTime)
        {
          this.mElapsed = this.ReturnTime;
          self.GotoState<TacticsUnitController.State_SkillEnd>();
        }
        Vector3 vector3 = this.mBasePos - self.JumpFallPos;
        self.transform.position = self.JumpFallPos + vector3 * (this.mElapsed / this.ReturnTime);
        bool visible = this.beforeVisible;
        if (!this.isDirect)
        {
          if ((double) this.ReturnTime * 0.850000023841858 < (double) this.mElapsed)
            visible = true;
          else if ((double) this.ReturnTime * 0.150000005960464 < (double) this.mElapsed)
            visible = false;
        }
        if (this.beforeVisible != visible)
          self.SetVisible(visible);
        this.beforeVisible = visible;
      }

      public override void Update(TacticsUnitController self)
      {
        if (this.Mode == TacticsUnitController.State_JumpCastComplete.MotionMode.FALL_WAIT)
          this.FallWaitUpdate(self);
        else if (this.Mode == TacticsUnitController.State_JumpCastComplete.MotionMode.FALL)
        {
          this.FallUpdate(self);
        }
        else
        {
          if (this.Mode != TacticsUnitController.State_JumpCastComplete.MotionMode.RETURN)
            return;
          this.ReturnUpdate(self);
        }
      }

      public override void End(TacticsUnitController self)
      {
        self.CollideGround = true;
        self.mCastJumpFallComplete = true;
        self.transform.position = this.mBasePos;
      }

      private enum MotionMode
      {
        FALL_WAIT,
        FALL,
        RETURN,
      }
    }

    private class State_ChangeGrid : TacticsUnitController.State
    {
      private bool mChanged;
      private bool mStartEffect;
      private GameObject mSelfEffect;
      private GameObject mTargetEffect;
      private ParticleSystem mSelfParticle;

      private GameObject CreateEffect(GameObject EffectPrefab, TacticsUnitController Parent)
      {
        GameObject go = UnityEngine.Object.Instantiate((UnityEngine.Object) EffectPrefab, Parent.transform.position, Parent.transform.rotation) as GameObject;
        if ((UnityEngine.Object) go != (UnityEngine.Object) null)
        {
          go.RequireComponent<OneShotParticle>();
          go.transform.SetParent(Parent.transform);
        }
        return go;
      }

      private void ChangePosition()
      {
      }

      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("B_SKL", false);
        TacticsUnitController target = self.mSkillVars.Targets[0];
        target.AutoUpdateRotation = false;
        target.LookAt(self.CenterPosition);
      }

      [DebuggerHidden]
      private IEnumerator Wait(float Seconds, TacticsUnitController self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_ChangeGrid.\u003CWait\u003Ec__Iterator37() { self = self, Seconds = Seconds, \u003C\u0024\u003Eself = self, \u003C\u0024\u003ESeconds = Seconds, \u003C\u003Ef__this = this };
      }

      [DebuggerHidden]
      private IEnumerator EffectEndWait(float Seconds, TacticsUnitController self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_ChangeGrid.\u003CEffectEndWait\u003Ec__Iterator38() { Seconds = Seconds, self = self, \u003C\u0024\u003ESeconds = Seconds, \u003C\u0024\u003Eself = self };
      }

      public override void Update(TacticsUnitController self)
      {
        if (!this.mStartEffect)
        {
          if ((double) self.GetNormalizedTime("B_SKL") < 1.0)
            return;
          self.StartCoroutine(this.Wait(0.2f, self));
        }
        else
        {
          if (!((UnityEngine.Object) this.mSelfEffect == (UnityEngine.Object) null) || !((UnityEngine.Object) this.mTargetEffect == (UnityEngine.Object) null))
            ;
          if (!((UnityEngine.Object) this.mSelfParticle != (UnityEngine.Object) null) || (double) this.mSelfParticle.time / (double) this.mSelfParticle.duration < 1.0 || this.mChanged)
            return;
          this.mChanged = true;
          TacticsUnitController target = self.mSkillVars.Targets[0];
          Vector3 position = self.transform.position;
          self.transform.position = target.transform.position;
          target.transform.position = position;
          self.mSkillVars.mStartPosition = self.transform.position;
          self.StartCoroutine(this.EffectEndWait(0.2f, self));
        }
      }

      public override void End(TacticsUnitController self)
      {
      }
    }

    private enum eKnockBackMode
    {
      IDLE,
      START,
      EXEC,
    }

    private class State_Throw : TacticsUnitController.State
    {
      private const float START_WAIT_TIME = 0.1f;
      private const float TURN_WAIT_TIME = 0.1f;
      private const float ACC_WAIT_TIME = 0.3f;
      private const float FINISH_WAIT_TIME = 0.2f;
      private SceneBattle mSceneBattle;
      private TacticsUnitController mTargetTuc;

      public override void Begin(TacticsUnitController self)
      {
        this.mSceneBattle = SceneBattle.Instance;
        if (self.mSkillVars.Targets.Count > 0)
          this.mTargetTuc = self.mSkillVars.Targets[0];
        if (!(bool) ((UnityEngine.Object) this.mSceneBattle) || !(bool) ((UnityEngine.Object) this.mTargetTuc))
        {
          self.GotoState<TacticsUnitController.State_SkillEnd>();
        }
        else
        {
          self.LoadUnitAnimationAsync("B_TOSS_LIFT", "cmn_toss_lift0", false, false);
          self.LoadUnitAnimationAsync("B_TOSS_THROW", "cmn_toss_throw0", false, false);
          self.StartCoroutine(this.execPerformance(self));
        }
      }

      [DebuggerHidden]
      private IEnumerator execPerformance(TacticsUnitController self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_Throw.\u003CexecPerformance\u003Ec__Iterator39() { self = self, \u003C\u0024\u003Eself = self, \u003C\u003Ef__this = this };
      }

      [DebuggerHidden]
      private IEnumerator lerpTurn(TacticsUnitController target, Vector3 target_pos)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_Throw.\u003ClerpTurn\u003Ec__Iterator3A() { target = target, target_pos = target_pos, \u003C\u0024\u003Etarget = target, \u003C\u0024\u003Etarget_pos = target_pos };
      }

      [DebuggerHidden]
      private IEnumerator lerpPickUp(TacticsUnitController self, TacticsUnitController target)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_Throw.\u003ClerpPickUp\u003Ec__Iterator3B() { target = target, self = self, \u003C\u0024\u003Etarget = target, \u003C\u0024\u003Eself = self };
      }

      [DebuggerHidden]
      private IEnumerator lerpThrow(TacticsUnitController target, Vector3 target_pos)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_Throw.\u003ClerpThrow\u003Ec__Iterator3C() { target = target, target_pos = target_pos, \u003C\u0024\u003Etarget = target, \u003C\u0024\u003Etarget_pos = target_pos };
      }

      [DebuggerHidden]
      private IEnumerator lerpBound(TacticsUnitController target)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_Throw.\u003ClerpBound\u003Ec__Iterator3D() { target = target, \u003C\u0024\u003Etarget = target };
      }
    }

    private class State_AfterTransform : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("B_TRANSFORM", false);
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) self.GetRemainingTime("B_TRANSFORM") > 0.100000001490116)
          return;
        self.PlayIdle(0.1f);
      }
    }

    private delegate void FieldActionEndEvent();

    private delegate void ProjectileStopEvent(TacticsUnitController.ProjectileData pd);
  }
}
