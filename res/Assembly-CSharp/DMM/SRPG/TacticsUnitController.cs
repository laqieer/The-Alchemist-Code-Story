// Decompiled with JetBrains decompiler
// Type: SRPG.TacticsUnitController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG.AnimEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class TacticsUnitController : UnitController
  {
    public static List<TacticsUnitController> Instances = new List<TacticsUnitController>(16);
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
    public List<string> mCustomRunAnimations;
    private GameObject mRenkeiAuraEffect;
    private GameObject mDrainEffect;
    private GameObject mChargeGrnTargetUnitEffect;
    private GameObject mChargeRedTargetUnitEffect;
    private GameObject mVersusCursor;
    private Transform mVersusCursorRoot;
    private bool mEnableStaticLight = true;
    public TacticsUnitController.PostureTypes Posture;
    public Color ColorMod = Color.white;
    private Texture2D mIconSmall;
    private Texture2D mIconMedium;
    private Texture2D mJobIcon;
    private string mRunAnimation;
    private long mCachedHP;
    private UnitGauge mHPGauge;
    private UnitGaugeMark mAddIconGauge;
    private float mKeepHPGaugeVisible;
    private long mCachedHpMax;
    private ChargeIcon mChargeIcon;
    private DeathSentenceIcon mDeathSentenceIcon;
    private GameObject mOwnerIndexUI;
    private UnitGaugeMark.EMarkType mKeepUnitGaugeMarkType;
    private UnitGaugeMark.EGemIcon mKeepUnitGaugeMarkGemIconType;
    private TacticsUnitController.HideGimmickAnimation mHideGimmickAnim;
    public string UniqueName;
    [NonSerialized]
    public int PlayerMemberIndex = -1;
    private Unit mUnit;
    private Projector mShadow;
    public UnitCursor UnitCursorTemplate;
    private UnitCursor mUnitCursor;
    public bool AutoUpdateRotation = true;
    private StateMachine<TacticsUnitController> mStateMachine;
    private bool mCancelAction;
    private EUnitDirection mFieldActionDir;
    private Vector2 mFieldActionPoint;
    public const float ADJUST_MOVE_DIST = 0.3f;
    private Color mBlendColor;
    private TacticsUnitController.ColorBlendModes mBlendMode;
    private float mBlendColorTime;
    private float mBlendColorTimeMax;
    private bool mEnableColorBlending;
    private PetController mPet;
    public static readonly string ANIM_IDLE_FIELD = "idlefield0";
    public static readonly string ANIM_IDLE_DEMO = "cmn_demo_talk_idle0";
    public static readonly string ANIM_RUN_FIELD = "runfield0";
    public static readonly string ANIM_STEP = "cmn_step0";
    public static readonly string ANIM_FALL_LOOP = "cmn_fallloop0";
    public static readonly string ANIM_CLIMBUP = "cmn_jumploop0";
    public static readonly string ANIM_PICKUP = "cmn_pickup0";
    public static readonly string ANIM_GENKIDAMA = "cmn_freeze0";
    public static readonly string ANIM_DOWN_STAND = "cmn_downstand0";
    public static readonly string ANIM_DODGE = "cmn_dodge0";
    public static readonly string ANIM_DAMAGE = "cmn_damage0";
    public static readonly string ANIM_DOWN = "cmn_down0";
    public static readonly string ANIM_ITEM_USE = "itemuse0";
    public static readonly string ANIM_GROGGY = "cmn_groggy0";
    private Dictionary<string, GameObject> mDefendSkillEffects = new Dictionary<string, GameObject>();
    private GameObject mPickupObject;
    private IntVector2 mCachedXY = new IntVector2(-1, -1);
    private float mFlyingPosToY;
    private float mFlyingPosNowY;
    private bool mIsFlyingUp;
    private Vector3[] mRoute;
    private int mRoutePos;
    private float mRunSpeed = 4f;
    private Vector3 mVelocity = Vector3.zero;
    private GridMap<int> mWalkableField;
    private bool mCollideGround = true;
    public bool IgnoreMove;
    private float mPostMoveAngle;
    private float mIdleInterpTime;
    private bool mLoadedPartially;
    private Vector3 mLookAtTarget;
    private float mSpinCount = 2f;
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
    private bool mIsProhibitedUpdateBadStatus;
    private bool mIsDispOffBadStatus;
    private const string ID_BATTLE_RUN = "B_RUN";
    private const string ID_BATTLE_SKILL = "B_SKL";
    private const string ID_BATTLE_BACKSTEP = "B_BS";
    private const string ID_BATTLE_DAMAGE_NORMAL = "B_DMG0";
    private const string ID_BATTLE_DODGE = "B_DGE";
    private const string ID_BATTLE_DOWN = "B_DOWN";
    private const string ID_BATTLE_CHANT = "B_CHA";
    private const string ID_BATTLE_RUNLOOP = "B_RUNL";
    private const string ID_BATTLE_DEAD = "B_DEAD";
    private const string ID_BATTLE_PRESKILL = "B_PRS";
    private const string ID_BATTLE_TOSS_LIFT = "B_TOSS_LIFT";
    private const string ID_BATTLE_TOSS_THROW = "B_TOSS_THROW";
    private const string ID_BATTLE_TRANSFORM = "B_TRANSFORM";
    private const string ID_BATTLE_PRE_TRANSFORM = "B_PRE_TRANSFORM";
    private const string ID_BATTLE_BIGUNIT_BS_IDLE = "B_BU_BS_IDLE";
    private const string ID_BATTLE_BIGUNIT_BS_DAMAGE = "B_BU_BS_DAMAGE";
    public const string ANIM_BATTLE_TOSS_LIFT = "cmn_toss_lift0";
    public const string ANIM_BATTLE_TOSS_THROW = "cmn_toss_throw0";
    public const string ANIM_BATTLE_BIGUNIT_BS_IDLE = "idleBattleScene0";
    public const string ANIM_BATTLE_BIGUNIT_BS_DAMAGE = "damageBattleScene0";
    public int DrainGemsOnHit;
    public GemParticle[] GemDrainEffects;
    public GameObject GemDrainHitEffect;
    public bool ShowCriticalEffectOnHit;
    public bool ShowBackstabEffectOnHit;
    public TacticsUnitController.EElementEffectTypes ShowElementEffectOnHit = TacticsUnitController.EElementEffectTypes.Normal;
    private int mDamageDispCounter;
    public List<TacticsUnitController.ShieldState> ShieldStates = new List<TacticsUnitController.ShieldState>();
    private TacticsUnitController.ShakeUnit mShaker;
    private float mMapTrajectoryHeight;
    private TacticsUnitController.SkillVars mSkillVars;
    public bool ShouldDodgeHits;
    public bool ShouldPerfectDodge;
    public bool ShouldDefendHits;
    private static string CameraAnimationDir = "Camera/";
    public const string COLLABO_SKILL_NAME_SUB = "_sub";
    private GameObject mLastHitEffect;
    private LogSkill.Target mHitInfo;
    private LogSkill.Target mHitInfoSelf;
    private const float CameraInterpRate = 4f;
    private float mCastJumpOffsetY = 10f;
    private bool mCastJumpStartComplete;
    private bool mCastJumpFallComplete;
    public Vector3 JumpFallPos;
    public IntVector2 JumpMapFallPos;
    private bool mFinishedCastJumpFall;
    private bool mIsPlayDamageMotion;
    private TacticsUnitController.eKnockBackMode mKnockBackMode;
    private Grid mKnockBackGrid;
    private const float KNOCK_BACK_SEC = 0.4f;
    private Vector3 mKbPosStart;
    private Vector3 mKbPosEnd;
    private float mKbPassedSec;
    public const string TRANSFORM_SKILL_NAME_SUB = "_chg";

    public void EnableStaticLight(bool enable) => this.mEnableStaticLight = enable;

    public void ResetColorMod() => this.ColorMod = Color.white;

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
      return (IEnumerator) new TacticsUnitController.\u003CCacheIconsAsync\u003Ec__Iterator0()
      {
        \u0024this = this
      };
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
        long currentHp = this.Unit.GetCurrentHP();
        long num = Math.Max(this.Unit.GetMaximumHP(), 1L);
        return Mathf.Clamp(num == 0L ? 100 : (int) (currentHp * 100L / num), 0, 100);
      }
    }

    public long VisibleHPValue => this.mCachedHP;

    public ChargeIcon ChargeIcon
    {
      get => this.mChargeIcon;
      set
      {
      }
    }

    public DeathSentenceIcon DeathSentenceIcon
    {
      get => this.mDeathSentenceIcon;
      set
      {
      }
    }

    public void InitHPGauge(Canvas canvas, UnitGauge gaugeTemplate)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) canvas, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) gaugeTemplate, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mHPGauge, (UnityEngine.Object) null))
      {
        this.mHPGauge = UnityEngine.Object.Instantiate<UnitGauge>(gaugeTemplate);
        this.mHPGauge.SetOwner(this.Unit);
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mAddIconGauge, (UnityEngine.Object) null))
        this.mAddIconGauge = ((Component) this.mHPGauge).GetComponent<UnitGaugeMark>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mChargeIcon, (UnityEngine.Object) null))
        this.mChargeIcon = ((Component) this.mHPGauge).GetComponent<ChargeIcon>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mDeathSentenceIcon, (UnityEngine.Object) null))
      {
        this.mDeathSentenceIcon = ((Component) this.mHPGauge).GetComponent<DeathSentenceIcon>();
        this.mDeathSentenceIcon.Init(this.Unit);
      }
      this.ResetHPGauge();
    }

    public void SetHPGaugeMode(
      TacticsUnitController.HPGaugeModes mode,
      SkillData skill = null,
      Unit attacker = null)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mHPGauge, (UnityEngine.Object) null))
        return;
      UnitGauge component = ((Component) this.mHPGauge).GetComponent<UnitGauge>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
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
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mChargeIcon, (UnityEngine.Object) null))
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

    public void SetHPChangeYosou(long newHP, int hpmax_damage = 0)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mHPGauge, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mHPGauge.MainGauge, (UnityEngine.Object) null))
        return;
      GameSettings instance = GameSettings.Instance;
      newHP = Math.Min(Math.Max(newHP, 0L), this.Unit.GetMaximumHP());
      if (hpmax_damage > 0 && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mHPGauge.MaxGauge, (UnityEngine.Object) null))
      {
        this.mCachedHpMax = Math.Max(this.Unit.GetMaximumHP() - (long) hpmax_damage, 0L);
        this.mCachedHP = Math.Min(Math.Max(this.mCachedHP, 0L), this.mCachedHpMax);
        long maximumHp = this.Unit.GetMaximumHP();
        this.mHPGauge.MaxGauge.UpdateValue(maximumHp == 0L ? 1f : Mathf.Clamp01((float) this.mCachedHpMax / (float) maximumHp));
        this.mHPGauge.MainGauge.Colors = new Color32[1]
        {
          this.Unit.Side != EUnitSide.Player ? instance.Gauge_EnemyHP_Base : instance.Gauge_PlayerHP_Base
        };
        this.mHPGauge.MainGauge.UpdateValue(Mathf.Clamp01((this.mCachedHpMax == 0L ? 1f : Mathf.Clamp01((float) this.mCachedHP / (float) this.mCachedHpMax)) * this.mHPGauge.MaxGauge.Value));
      }
      else if (newHP < this.mCachedHP)
      {
        Color32[] colors;
        if (newHP <= 0L)
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
          colors[0].a = this.mCachedHP == 0L ? (byte) 0 : (byte) (newHP * (long) byte.MaxValue / this.mCachedHP);
          colors[1].a = (byte) ((uint) byte.MaxValue - (uint) colors[0].a);
        }
        this.mHPGauge.MainGauge.UpdateColors(colors);
      }
      else if (newHP > this.mCachedHP)
      {
        Color32[] colors = new Color32[2]
        {
          this.Unit.Side != EUnitSide.Player ? instance.Gauge_EnemyHP_Base : instance.Gauge_PlayerHP_Base,
          this.Unit.Side != EUnitSide.Player ? instance.Gauge_EnemyHP_Heal : instance.Gauge_PlayerHP_Heal
        };
        colors[0].a = newHP == 0L ? (byte) 0 : (byte) (this.mCachedHP * (long) byte.MaxValue / newHP);
        colors[1].a = (byte) ((uint) byte.MaxValue - (uint) colors[0].a);
        long maximumHp = this.Unit.GetMaximumHP();
        this.mHPGauge.MainGauge.Value = maximumHp == 0L ? 1f : Mathf.Clamp01((float) newHP / (float) maximumHp);
        this.mHPGauge.MainGauge.UpdateColors(colors);
      }
      else
        this.ResetHPGauge();
    }

    public bool IsHPGaugeChanging
    {
      get
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mHPGauge, (UnityEngine.Object) null) || !((Component) this.mHPGauge).gameObject.activeInHierarchy || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mHPGauge.MainGauge, (UnityEngine.Object) null) || !((Component) this.mHPGauge.MainGauge).gameObject.activeInHierarchy)
          return false;
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mHPGauge.MaxGauge, (UnityEngine.Object) null) ? this.mHPGauge.MainGauge.IsAnimating | this.mHPGauge.MaxGauge.IsAnimating : this.mHPGauge.MainGauge.IsAnimating;
      }
    }

    public RectTransform HPGaugeTransform
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mHPGauge, (UnityEngine.Object) null) ? ((Component) this.mHPGauge).GetComponent<RectTransform>() : (RectTransform) null;
      }
    }

    public void ShowHPGauge(bool visible)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mHPGauge, (UnityEngine.Object) null))
        ((Component) this.mHPGauge).gameObject.SetActive(visible);
      this.mKeepHPGaugeVisible = -1f;
    }

    public void ResetHPGauge()
    {
      this.mCachedHP = this.Unit.GetCurrentHP();
      this.mCachedHpMax = this.Unit.GetMaximumHP();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mHPGauge, (UnityEngine.Object) null))
        return;
      GameSettings instance = GameSettings.Instance;
      this.mHPGauge.MainGauge.Colors = new Color32[1]
      {
        this.Unit.Side != EUnitSide.Player ? instance.Gauge_EnemyHP_Base : instance.Gauge_PlayerHP_Base
      };
      this.mHPGauge.MainGauge.AnimateRangedValue(this.mCachedHP, this.Unit.GetMaximumHP(), 0.0f);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mHPGauge.MaxGauge, (UnityEngine.Object) null))
        return;
      this.mHPGauge.MaxGauge.UpdateValue(1f);
    }

    public void UpdateHPRelative(int delta, float duration = 0.5f, bool is_hpmax_damage = false)
    {
      long mCachedHpMax = this.mCachedHpMax;
      if (is_hpmax_damage)
      {
        this.mCachedHpMax = Math.Max(mCachedHpMax + (long) delta, 0L);
        delta = 0;
      }
      this.mCachedHP += (long) delta;
      this.mCachedHP = Math.Min(Math.Max(this.mCachedHP, 0L), this.mCachedHpMax);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mHPGauge, (UnityEngine.Object) null) || !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mHPGauge.MainGauge))
        return;
      if (is_hpmax_damage && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mHPGauge.MaxGauge))
      {
        float newValue = mCachedHpMax == 0L ? 1f : Mathf.Clamp01((float) this.mCachedHpMax / (float) mCachedHpMax);
        this.mHPGauge.MaxGauge.AnimateValue(newValue, duration);
        this.mHPGauge.MainGauge.AnimateValue(Mathf.Clamp01((this.mCachedHpMax == 0L ? 0.0f : Mathf.Clamp01((float) this.mCachedHP / (float) this.mCachedHpMax)) * newValue), duration);
      }
      else
        this.mHPGauge.MainGauge.AnimateRangedValue(this.mCachedHP, this.mCachedHpMax, duration);
      ((Component) this.mHPGauge).gameObject.SetActive(true);
      if ((double) this.mKeepHPGaugeVisible < 0.0)
        return;
      this.mKeepHPGaugeVisible = 1.5f;
    }

    private void UpdateGauges()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mHPGauge, (UnityEngine.Object) null) && (double) this.mKeepHPGaugeVisible >= 0.0 && ((Component) this.mHPGauge).gameObject.activeInHierarchy)
      {
        this.mKeepHPGaugeVisible -= Time.deltaTime;
        if ((double) this.mKeepHPGaugeVisible <= 0.0)
        {
          ((Component) this.mHPGauge).gameObject.SetActive(false);
          this.mKeepHPGaugeVisible = 0.0f;
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mAddIconGauge, (UnityEngine.Object) null))
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
        skill.Setup(this.mSkillVars.Skill.iname, 1);
      }
      this.mHPGauge.ActivateElementIcon(true);
      this.mHPGauge.OnAttack(skill, skill.ElementType, (int) skill.ElementValue, attacker.Element, (int) attacker.CurrentStatus.element_assist[attacker.Element]);
    }

    public void ShowOwnerIndexUI(bool show)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mOwnerIndexUI, (UnityEngine.Object) null))
        return;
      this.mOwnerIndexUI.gameObject.SetActive(show);
    }

    public bool CreateOwnerIndexUI(
      Canvas canvas,
      GameObject templeteUI,
      JSON_MyPhotonPlayerParam param)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mOwnerIndexUI, (UnityEngine.Object) null))
        return true;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) canvas, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) templeteUI, (UnityEngine.Object) null) || param == null)
        return false;
      this.mOwnerIndexUI = UnityEngine.Object.Instantiate<GameObject>(templeteUI);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mOwnerIndexUI, (UnityEngine.Object) null))
        return false;
      DataSource.Bind<JSON_MyPhotonPlayerParam>(this.mOwnerIndexUI, param);
      UIProjector uiProjector = ((Component) this).gameObject.AddComponent<UIProjector>();
      uiProjector.UIObject = this.mOwnerIndexUI.transform as RectTransform;
      uiProjector.AutoDestroyUIObject = true;
      uiProjector.LocalOffset = Vector3.up;
      uiProjector.SetCanvas(canvas);
      return true;
    }

    public void ShowVersusCursor(bool show)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mVersusCursor, (UnityEngine.Object) null))
        return;
      this.mVersusCursor.gameObject.SetActive(show);
    }

    public void PlayVersusCursor(bool play)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mVersusCursor, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mVersusCursorRoot, (UnityEngine.Object) null))
        return;
      Animation component = ((Component) this.mVersusCursorRoot).GetComponent<Animation>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      ((Behaviour) component).enabled = play;
    }

    public bool CreateVersusCursor(GameObject templeteUI)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mVersusCursor, (UnityEngine.Object) null))
        return true;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) templeteUI, (UnityEngine.Object) null))
        return false;
      this.mVersusCursor = UnityEngine.Object.Instantiate<GameObject>(templeteUI);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mVersusCursor, (UnityEngine.Object) null))
        return false;
      this.mVersusCursor.transform.SetParent(((Component) this).gameObject.transform, false);
      this.mVersusCursor.transform.localPosition = Vector3.op_Multiply(Vector3.up, 1.3f);
      this.mVersusCursor.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
      GameUtility.SetLayer(this.mVersusCursor, GameUtility.LayerUI, true);
      this.mVersusCursorRoot = this.mVersusCursor.transform.Find("Root");
      return true;
    }

    public void SetGimmickIcon(Unit TargetUnit)
    {
      if (TargetUnit == null || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mAddIconGauge, (UnityEngine.Object) null))
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
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mAddIconGauge, (UnityEngine.Object) null))
        return;
      this.mAddIconGauge.SetEndAnimationAll();
    }

    public void DeleteGimmickIconAll()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mAddIconGauge, (UnityEngine.Object) null))
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

    public void ResetScale() => this.HideGimmickAnim.ResetScale();

    public bool IsA(string id)
    {
      return this.Unit != null && this.Unit.UnitParam.iname == id || this.UniqueName == id;
    }

    public Unit Unit => this.mUnit;

    public Vector2 FieldActionPoint => this.mFieldActionPoint;

    public bool TriggerFieldAction(Vector3 velocity, bool resetToMove = false)
    {
      Transform transform = ((Component) this).transform;
      this.mOnFieldActionEnd = !resetToMove ? new TacticsUnitController.FieldActionEndEvent(this.PlayIdleSmooth) : new TacticsUnitController.FieldActionEndEvent(this.MoveAgain);
      float num1 = Mathf.Floor(transform.position.x);
      float num2 = Mathf.Floor(transform.position.z);
      float num3 = num1 + 1f;
      float num4 = num2 + 1f;
      float num5 = float.MaxValue;
      EUnitDirection direction = EUnitDirection.NegativeX;
      this.mFieldActionPoint = new Vector2(((Component) this).transform.position.x, ((Component) this).transform.position.z);
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
        float num8 = num3 - transform.position.x;
        if ((double) num8 < (double) num5)
        {
          num5 = num8;
          direction = EUnitDirection.PositiveX;
        }
      }
      if ((double) velocity.z < 0.0)
      {
        float num9 = transform.position.z - num2;
        if ((double) num9 < (double) num5)
        {
          num5 = num9;
          direction = EUnitDirection.NegativeY;
        }
      }
      else if ((double) velocity.z > 0.0)
      {
        float num10 = num4 - transform.position.z;
        if ((double) num10 < (double) num5)
        {
          num5 = num10;
          direction = EUnitDirection.PositiveY;
        }
      }
      if ((double) num5 <= 0.30000001192092896)
      {
        IntVector2 offset = direction.ToOffset();
        int num11 = Mathf.FloorToInt(transform.position.x);
        int num12 = Mathf.FloorToInt(transform.position.z);
        int x = num11 + offset.x;
        int y1 = num12 + offset.y;
        if (this.mWalkableField != null && this.mWalkableField.isValid(x, y1) && this.mWalkableField.get(x, y1) >= 0)
        {
          float y2 = GameUtility.RaycastGround(transform.position).y;
          float num13 = GameUtility.RaycastGround((float) x + 0.5f, (float) y1 + 0.5f) - y2;
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
          Vector3 pos;
          // ISSUE: explicit constructor call
          ((Vector3) ref pos).\u002Ector(this.mFieldActionPoint.x, 0.0f, this.mFieldActionPoint.y);
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
          if ((double) num13 <= (double) instance.Unit_FallAnimationThreshold)
          {
            this.GotoState<TacticsUnitController.State_FieldActionFall>();
            return true;
          }
          if ((double) num13 <= -(double) instance.Unit_StepAnimationThreshold)
          {
            this.GotoState<TacticsUnitController.State_FieldActionJump>();
            return true;
          }
          if ((double) num13 >= (double) instance.Unit_JumpAnimationThreshold)
          {
            this.GotoState<TacticsUnitController.State_FieldActionJumpUp>();
            return true;
          }
          if ((double) num13 >= (double) instance.Unit_StepAnimationThreshold)
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
          if ((double) pos.x % 1.0 < 0.30000001192092896)
          {
            pos.x = Mathf.Floor(pos.x) + 0.3f;
            flag = true;
            break;
          }
          break;
        case EUnitDirection.NegativeY:
          if ((double) pos.z % 1.0 < 0.30000001192092896)
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
      get => this.mStateMachine.IsInKindOfState<TacticsUnitController.State_FieldAction>();
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
      Vector3 position = ((Component) this).transform.position;
      StaticLightVolume volume = StaticLightVolume.FindVolume(position);
      Color directLit;
      Color indirectLit;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) volume, (UnityEngine.Object) null) || !this.mEnableStaticLight)
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
      Color color;
      // ISSUE: explicit constructor call
      ((Color) ref color).\u002Ector(0.0f, 0.0f, 0.0f, 0.0f);
      if (this.mBadStatus != null && (double) this.mBadStatus.BlendColor.a > 0.0)
        color = Color.Lerp(this.mBadStatus.BlendColor, new Color(1f, 1f, 1f, 0.0f), (float) ((1.0 - (double) Mathf.Cos(Time.time * 3.14159274f)) * 0.5) * 0.5f);
      switch (this.mBlendMode)
      {
        case TacticsUnitController.ColorBlendModes.Fade:
          this.mBlendColorTime += Time.deltaTime;
          float num = Mathf.Clamp01(this.mBlendColorTime / this.mBlendColorTimeMax);
          if ((double) num >= 1.0)
          {
            this.mBlendMode = TacticsUnitController.ColorBlendModes.None;
            break;
          }
          color = Color.Lerp(this.mBlendColor, new Color(1f, 1f, 1f, 0.0f), num);
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

    public bool HasCursor => UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mUnitCursor, (UnityEngine.Object) null);

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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mUnitCursor, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) prefab, (UnityEngine.Object) null))
        return;
      this.mUnitCursor = UnityEngine.Object.Instantiate<UnitCursor>(prefab);
      ((Component) this.mUnitCursor).transform.parent = ((Component) this).transform;
      this.mUnitCursor.Color = color;
      ((Component) this.mUnitCursor).transform.localPosition = Vector3.op_Multiply(Vector3.up, 0.3f);
      if (this.mUnit == null || this.mUnit.IsNormalSize)
        return;
      this.mUnitCursor.LoopScale = (float) ((double) (this.mUnit.SizeX + this.mUnit.SizeY) / 2.0 + 1.0);
      this.mUnitCursor.StartScale = this.mUnitCursor.LoopScale + 2f;
      this.mUnitCursor.EndScale = this.mUnitCursor.StartScale;
    }

    public void CancelAction() => this.mCancelAction = true;

    public void HideCursor(bool immediate = false)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitCursor, (UnityEngine.Object) null))
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
      this.mCachedHP = unit.GetCurrentHP();
      this.mCachedHpMax = unit.GetMaximumHP();
      this.UniqueName = unit.UniqueName;
      ((Component) this).transform.rotation = this.mUnit.Direction.ToRotation();
      this.SetupPet();
      this.SetupUnit(unit.UnitData);
    }

    private void SetupPet()
    {
      if (this.mUnit.Job == null || string.IsNullOrEmpty(this.mUnit.Job.Param.pet))
        return;
      this.mPet = new GameObject("Pet", new System.Type[1]
      {
        typeof (PetController)
      }).GetComponent<PetController>();
      this.mPet.Owner = ((Component) this).gameObject;
      this.mPet.PetID = this.mUnit.Job.Param.pet;
    }

    protected new void Start()
    {
      this.OnAnimationUpdate = new AnimationPlayer.AnimationUpdateEvent(this.AnimationUpdated);
      base.Start();
      if (this.mUnit == null || !this.mUnit.IsBreakObj)
      {
        if (this.Posture == TacticsUnitController.PostureTypes.Combat)
          this.LoadUnitAnimationAsync("IDLE", TacticsUnitController.ANIM_IDLE_FIELD, true);
        else
          this.LoadUnitAnimationAsync("IDLE", TacticsUnitController.ANIM_IDLE_DEMO, false);
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
      return (IEnumerator) new TacticsUnitController.\u003CLoadDefendSkillEffectAsync\u003Ec__Iterator1()
      {
        skillEffectName = skillEffectName,
        \u0024this = this
      };
    }

    protected override void PostSetup()
    {
      GameSettings instance = GameSettings.Instance;
      float mapCharacterScale = instance.Quest.MapCharacterScale;
      this.RootMotionScale = mapCharacterScale;
      ((Component) this).transform.localScale = Vector3.op_Multiply(Vector3.one, mapCharacterScale);
      this.VesselStrength = instance.Unit_MaxGlowStrength;
      CharacterSettings component = this.UnitObject.GetComponent<CharacterSettings>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
      {
        Unit unit = this.Unit;
        if (unit != null)
        {
          this.UnitSide = unit.Side;
          if (component.IsUseGlowColor)
          {
            this.VesselColor = component.GlowColor;
            this.VesselStrength = component.GlowStrength;
          }
          else if (unit.Side == EUnitSide.Enemy)
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
        if (this.mUnitObjectLists.Count <= 1 && UnityEngine.Object.op_Inequality((UnityEngine.Object) component.ShadowProjector, (UnityEngine.Object) null))
        {
          GameObject gameObject = ((Component) component.ShadowProjector).gameObject;
          this.mShadow = UnityEngine.Object.Instantiate<GameObject>(gameObject, Vector3.op_Addition(((Component) this).transform.position, gameObject.transform.position), gameObject.transform.rotation).GetComponent<Projector>();
          ((Component) this.mShadow).transform.SetParent(this.GetCharacterRoot(), true);
          this.mShadow.ignoreLayers = ~(1 << LayerMask.NameToLayer("BG"));
          GameUtility.SetLayer((Component) this.mShadow, GameUtility.LayerHidden, true);
          this.mShadow.orthographicSize *= mapCharacterScale;
          if (this.Unit != null && !this.Unit.IsNormalSize)
            this.mShadow.orthographicSize *= (float) (this.Unit.SizeX + this.Unit.SizeY) / 2f;
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPet, (UnityEngine.Object) null))
      {
        Transform transform1 = ((Component) this).transform;
        Transform transform2 = ((Component) this.mPet).transform;
        transform2.position = transform1.position;
        transform2.rotation = transform1.rotation;
      }
      if (this.Unit == null)
        return;
      this.CacheIcons();
    }

    public void SetRenkeiAura(GameObject eff)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRenkeiAuraEffect, (UnityEngine.Object) null))
      {
        GameUtility.StopEmitters(this.mRenkeiAuraEffect);
        GameUtility.RequireComponent<OneShotParticle>(this.mRenkeiAuraEffect);
        this.mRenkeiAuraEffect = (GameObject) null;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) eff, (UnityEngine.Object) null))
        return;
      this.mRenkeiAuraEffect = UnityEngine.Object.Instantiate<GameObject>(eff, Vector3.zero, Quaternion.identity);
      this.mRenkeiAuraEffect.transform.SetParent(((Component) this).transform, false);
    }

    public void StopRenkeiAura() => this.SetRenkeiAura((GameObject) null);

    public void EnableChargeTargetUnit(GameObject eff, bool is_grn)
    {
      if (is_grn)
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mChargeGrnTargetUnitEffect) || !UnityEngine.Object.op_Implicit((UnityEngine.Object) eff))
          return;
        this.mChargeGrnTargetUnitEffect = UnityEngine.Object.Instantiate<GameObject>(eff, Vector3.zero, Quaternion.identity);
        if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mChargeGrnTargetUnitEffect))
          return;
        this.mChargeGrnTargetUnitEffect.transform.SetParent(((Component) this).transform, false);
      }
      else
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mChargeRedTargetUnitEffect) || !UnityEngine.Object.op_Implicit((UnityEngine.Object) eff))
          return;
        this.mChargeRedTargetUnitEffect = UnityEngine.Object.Instantiate<GameObject>(eff, Vector3.zero, Quaternion.identity);
        if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mChargeRedTargetUnitEffect))
          return;
        this.mChargeRedTargetUnitEffect.transform.SetParent(((Component) this).transform, false);
      }
    }

    public void DisableChargeTargetUnit()
    {
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mChargeGrnTargetUnitEffect))
      {
        GameUtility.StopEmitters(this.mChargeGrnTargetUnitEffect);
        GameUtility.RequireComponent<OneShotParticle>(this.mChargeGrnTargetUnitEffect);
        this.mChargeGrnTargetUnitEffect = (GameObject) null;
      }
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mChargeRedTargetUnitEffect))
        return;
      GameUtility.StopEmitters(this.mChargeRedTargetUnitEffect);
      GameUtility.RequireComponent<OneShotParticle>(this.mChargeRedTargetUnitEffect);
      this.mChargeRedTargetUnitEffect = (GameObject) null;
    }

    public void SetDrainEffect(GameObject eff)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) eff, (UnityEngine.Object) null))
        return;
      Transform transform = GameUtility.findChildRecursively(((Component) this).transform, "Bip001");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) transform, (UnityEngine.Object) null))
        transform = ((Component) this).transform;
      this.mDrainEffect = UnityEngine.Object.Instantiate<GameObject>(eff, ((Component) this).transform.position, ((Component) this).transform.rotation);
      this.mDrainEffect.transform.SetParent(transform, false);
      this.mDrainEffect.RequireComponent<OneShotParticle>();
      this.mDrainEffect.SetActive(false);
    }

    public void BeginLoadPickupAnimation()
    {
      this.LoadUnitAnimationAsync("PICK", TacticsUnitController.ANIM_PICKUP, this.mCharacterData.IsJobPickup);
    }

    public void UnloadPickupAnimation() => this.UnloadAnimation("PICK");

    public void LoadGenkidamaAnimation(bool load)
    {
      if (load)
        this.LoadUnitAnimationAsync("GENK", TacticsUnitController.ANIM_GENKIDAMA, this.mCharacterData.IsJobFreeze);
      else
        this.UnloadAnimation("GENK");
    }

    public void PlayGenkidama() => this.PlayAnimation("GENK", true, 0.2f);

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
      float num = (float) ((57.295780181884766 * (double) Mathf.Atan2(y, x) + 360.0) % 360.0);
      if (325.0 <= (double) num || (double) num < 45.0)
        return EUnitDirection.PositiveX;
      if (45.0 <= (double) num && (double) num < 135.0)
        return EUnitDirection.PositiveY;
      return 135.0 <= (double) num && (double) num < 225.0 ? EUnitDirection.NegativeX : EUnitDirection.NegativeY;
    }

    public EUnitDirection CalcUnitDirectionFromRotation()
    {
      Transform transform = ((Component) this).transform;
      return TacticsUnitController.CalcUnitDirection(transform.forward.x, transform.forward.z);
    }

    private bool CheckCollision(int x0, int y0, int x1, int y1)
    {
      if (!this.mWalkableField.isValid(x1, y1))
        return true;
      int num1 = this.mWalkableField.get(x0, y0);
      int num2 = this.mWalkableField.get(x1, y1);
      return num2 < 0 || BattleMap.MAX_GRID_MOVING < Mathf.Abs(num1 - num2);
    }

    private void AnimationUpdated(GameObject go)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCharacterSettings, (UnityEngine.Object) null))
        return;
      this.mCharacterSettings.SetSkeleton("small");
    }

    public bool CollideGround
    {
      get => this.mCollideGround;
      set => this.mCollideGround = value;
    }

    private void ResetFlyingUp()
    {
      this.mIsFlyingUp = false;
      this.mFlyingPosToY = 0.0f;
      this.mFlyingPosNowY = 0.0f;
    }

    private Unit CheckFlyingUp(SceneBattle sb, IntVector2 co)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) sb))
        return (Unit) null;
      BattleCore battle = sb.Battle;
      if (battle == null)
        return (Unit) null;
      BattleMap currentMap = battle.CurrentMap;
      if (currentMap == null)
        return (Unit) null;
      Grid grid1 = currentMap[co.x, co.y];
      if (grid1 == null)
        return (Unit) null;
      for (int minColX = this.Unit.MinColX; minColX < this.Unit.MaxColX; ++minColX)
      {
        for (int minColY = this.Unit.MinColY; minColY < this.Unit.MaxColY; ++minColY)
        {
          Grid grid2 = currentMap[grid1.x + minColX, grid1.y + minColY];
          Unit unitAtGrid = battle.FindUnitAtGrid(grid2);
          if (unitAtGrid != null && this.Unit != unitAtGrid && this.Unit.Side != unitAtGrid.Side)
            return unitAtGrid;
        }
      }
      return (Unit) null;
    }

    private void SnapToGround()
    {
      Transform transform = ((Component) this).transform;
      Vector3 position = transform.position;
      position.y = GameUtility.RaycastGround(position).y;
      if (this.mUnit != null && this.mUnit.IsPassMovType)
      {
        SceneBattle instance = SceneBattle.Instance;
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        {
          IntVector2 co = instance.CalcCoord(position);
          if (co != this.mCachedXY)
          {
            this.mCachedXY = co;
            bool mIsFlyingUp = this.mIsFlyingUp;
            Unit unit = this.CheckFlyingUp(instance, co);
            this.mIsFlyingUp = unit != null;
            if (this.mIsFlyingUp != mIsFlyingUp)
            {
              if (this.mIsFlyingUp)
              {
                float ridingFlyingHeight = GameSettings.Instance.Quest.RidingFlyingHeight;
                this.mFlyingPosToY = unit.SizeZ <= 0 ? ridingFlyingHeight : (float) unit.SizeZ - (1f - ridingFlyingHeight);
              }
              else
                this.mFlyingPosToY = 0.0f;
            }
          }
          if ((double) this.mFlyingPosToY != 0.0 || (double) this.mFlyingPosToY != (double) this.mFlyingPosNowY)
          {
            this.mFlyingPosNowY = Mathf.Lerp(this.mFlyingPosNowY, this.mFlyingPosToY, Time.deltaTime * 75f);
            position.y += this.mFlyingPosNowY;
          }
        }
      }
      transform.position = position;
    }

    protected override void LateUpdate()
    {
      base.LateUpdate();
      if ((double) ((Vector3) ref this.mVelocity).sqrMagnitude > 0.0)
      {
        Transform transform = ((Component) this).transform;
        transform.position = Vector3.op_Addition(transform.position, Vector3.op_Multiply(this.mVelocity, Time.deltaTime));
      }
      if (this.mCollideGround)
        this.SnapToGround();
      else
        this.ResetFlyingUp();
      this.UpdateColorBlending();
    }

    public GridMap<int> WalkableField
    {
      set => this.mWalkableField = value;
    }

    public float StartMove(Vector3[] route, float directionAngle = -1f)
    {
      if (this.IgnoreMove)
      {
        this.IgnoreMove = false;
        return 0.0f;
      }
      ((Component) this).transform.position = route[0];
      this.mPostMoveAngle = directionAngle;
      this.mRoute = route;
      this.mRoutePos = 0;
      this.GotoState<TacticsUnitController.State_Move>();
      float num1 = 0.0f;
      for (int index = 1; index < route.Length; ++index)
      {
        double num2 = (double) num1;
        Vector3 vector3 = Vector3.op_Subtraction(route[index - 1], route[index]);
        double magnitude = (double) ((Vector3) ref vector3).magnitude;
        num1 = (float) (num2 + magnitude);
      }
      return num1 / this.mRunSpeed;
    }

    public void SetRunningSpeed(float speed) => this.mRunSpeed = speed;

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
      this.PlayIdle();
    }

    public void PlayTakenAnimation() => this.GotoState<TacticsUnitController.State_Taken>();

    public bool isIdle
    {
      get
      {
        return this.mStateMachine.IsInState<TacticsUnitController.State_Idle>() || this.mStateMachine.IsInState<TacticsUnitController.State_Down>() || this.mStateMachine.IsInState<TacticsUnitController.State_BigUnitBsIdle>();
      }
    }

    public bool isMoving
    {
      get
      {
        return this.mStateMachine.IsInState<TacticsUnitController.State_Move>() || this.mStateMachine.IsInState<TacticsUnitController.State_RunLoop>();
      }
    }

    public bool IsInitLoading
    {
      get
      {
        return this.IsLoading || this.mStateMachine.IsInState<TacticsUnitController.State_WaitResources>();
      }
    }

    protected override void OnVisibilityChange(bool visible)
    {
      if (!visible || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mShadow, (UnityEngine.Object) null))
        return;
      ((Component) this.mShadow).gameObject.layer = GameUtility.LayerDefault;
    }

    public bool IsLoadedPartially => this.mLoadedPartially;

    public void ResetRotation()
    {
      ((Component) this).transform.rotation = this.mUnit.Direction.ToRotation();
    }

    private void UpdateRotation()
    {
      if (this.mUnit == null)
        return;
      ((Component) this).transform.rotation = Quaternion.Slerp(((Component) this).transform.rotation, this.mUnit.Direction.ToRotation(), Time.deltaTime * 10f);
    }

    public void LookAt(Vector3 position)
    {
      if (this.mUnit != null && this.mUnit.IsBreakObj || this.mCharacterData.IsNoTurn)
        return;
      Transform transform = ((Component) this).transform;
      Vector3 vector3 = Vector3.op_Subtraction(position, transform.position);
      vector3.y = 0.0f;
      transform.rotation = Quaternion.LookRotation(vector3);
    }

    public void LookAt(Component target) => this.LookAt(target.transform.position);

    public void PlayLookAt(Component target, float spin = 2f)
    {
      this.PlayLookAt(target.transform.position, spin);
    }

    public void PlayLookAt(Vector3 target, float spin = 2f)
    {
      this.mSpinCount = spin;
      this.mLookAtTarget = target;
      this.GotoState<TacticsUnitController.State_LookAt>();
    }

    private void PlayIdleSmooth() => this.PlayIdle(0.1f);

    private void MoveAgain() => this.GotoState<TacticsUnitController.State_Move>();

    public void StepTo(Vector3 dest)
    {
      this.mStepStart = ((Component) this).transform.position;
      this.mStepEnd = dest;
      Vector3 vector3 = Vector3.op_Subtraction(this.mStepEnd, this.mStepStart);
      if ((double) ((Vector3) ref vector3).magnitude <= (double) GameSettings.Instance.Quest.AnimateGridSnapRadius)
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
      this.mBadStatusLocks &= ~(int) condition;
    }

    public void ClearBadStatusLocks() => this.mBadStatusLocks = 0;

    public void EnableProhibitedUpdateBadStatus(bool is_prohibited)
    {
      this.mIsProhibitedUpdateBadStatus = is_prohibited;
    }

    public void EnableDispOffBadStatus(bool is_disp_off)
    {
      this.mIsDispOffBadStatus = is_disp_off;
      if (is_disp_off)
        this.mLoadedBadStatusAnimation = (string) null;
      this.UpdateBadStatus();
    }

    public void UpdateBadStatus()
    {
      if (BadStatusEffects.Effects == null || this.Unit == null || this.mIsProhibitedUpdateBadStatus)
        return;
      BadStatusEffects.Desc desc = (BadStatusEffects.Desc) null;
      bool flag1 = false;
      if (!this.mIsDispOffBadStatus)
      {
        for (int index = 0; index < BadStatusEffects.Effects.Count; ++index)
        {
          EUnitCondition key = BadStatusEffects.Effects[index].Key;
          if ((this.mBadStatusLocks & (int) key) == 0 && this.Unit.IsUnitCondition(key))
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
      }
      bool flag3 = this.Unit.IsUnitCondition(EUnitCondition.DeathSentence);
      if (this.mIsDispOffBadStatus)
        flag3 = false;
      if (!flag3)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDeathSentenceIcon, (UnityEngine.Object) null))
          this.mDeathSentenceIcon.Close();
      }
      else
        this.DeathSentenceCountDown(false);
      if (this.IsCursed != flag1 && UnityEngine.Object.op_Inequality((UnityEngine.Object) BadStatusEffects.CurseEffect, (UnityEngine.Object) null))
      {
        if (flag1)
        {
          this.mCurseEffect = UnityEngine.Object.Instantiate<GameObject>(BadStatusEffects.CurseEffect);
          string attach_target = BadStatusEffects.CurseEffectAttachTarget;
          if (!this.mUnit.IsNormalSize)
            attach_target = BadStatusEffects.CurseEffectAttachTargetBigUnit;
          this.attachBadStatusEffect(this.mCurseEffect, attach_target);
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBadStatusEffect, (UnityEngine.Object) null))
      {
        GameUtility.StopEmitters(this.mBadStatusEffect);
        this.mBadStatusEffect.AddComponent<OneShotParticle>();
        this.mBadStatusEffect = (GameObject) null;
      }
      this.mBadStatus = desc;
      if (this.mBadStatus != null)
      {
        if (this.mUnit.IsNormalSize)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBadStatus.Effect, (UnityEngine.Object) null))
          {
            this.mBadStatusEffect = UnityEngine.Object.Instantiate<GameObject>(this.mBadStatus.Effect);
            this.attachBadStatusEffect(this.mBadStatusEffect, this.mBadStatus.AttachTarget);
          }
        }
        else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBadStatus.EffectBigUnit, (UnityEngine.Object) null))
        {
          this.mBadStatusEffect = UnityEngine.Object.Instantiate<GameObject>(this.mBadStatus.EffectBigUnit);
          this.attachBadStatusEffect(this.mBadStatusEffect, this.mBadStatus.AttachTargetBigUnit);
        }
        if (!string.IsNullOrEmpty(this.mBadStatus.AnimationName) && string.IsNullOrEmpty(this.mLoadedBadStatusAnimation))
        {
          bool addJobName = false;
          if (this.mCharacterData != null)
          {
            if (this.mBadStatus.AnimationName == TacticsUnitController.ANIM_GENKIDAMA)
              addJobName = this.mCharacterData.IsJobFreeze;
            else if (this.mBadStatus.AnimationName == TacticsUnitController.ANIM_GROGGY)
              addJobName = this.mCharacterData.IsJobGroggy;
          }
          this.mLoadedBadStatusAnimation = this.mBadStatus.AnimationName;
          this.LoadUnitAnimationAsync("BAD", this.mBadStatus.AnimationName, addJobName);
        }
        if (this.Unit.IsUnitCondition(EUnitCondition.DeathSentence) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDeathSentenceIcon, (UnityEngine.Object) null))
          this.mDeathSentenceIcon.Open();
      }
      this.SetMonochrome(this.mBadStatus != null && this.mBadStatus.Key == EUnitCondition.Stone);
    }

    private void attachBadStatusEffect(GameObject go_effect, string attach_target = null, bool is_use_cs = false)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) go_effect))
        return;
      Transform transform = (Transform) null;
      if (!string.IsNullOrEmpty(attach_target))
        transform = !is_use_cs || !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mCharacterSettings) ? GameUtility.findChildRecursively(((Component) this).transform, attach_target) : GameUtility.findChildRecursively(((Component) this.mCharacterSettings).transform, attach_target);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) transform, (UnityEngine.Object) null))
        transform = this.GetCharacterRoot();
      go_effect.transform.SetParent(transform, false);
      if (this.IsVisible())
        return;
      this.SetVisible(false);
    }

    public bool IsDeathSentenceCountDownPlaying()
    {
      return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDeathSentenceIcon, (UnityEngine.Object) null) && this.mDeathSentenceIcon.IsDeathSentenceCountDownPlaying;
    }

    public void DeathSentenceCountDown(bool isShow, float LifeTime = 0.0f)
    {
      if (isShow)
        this.ShowHPGauge(true);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDeathSentenceIcon, (UnityEngine.Object) null))
        return;
      this.mDeathSentenceIcon.Open();
      this.mDeathSentenceIcon.Countdown(Mathf.Max(this.Unit.DeathCount, 0), LifeTime);
    }

    public void UpdateShields(bool is_update_turn = true, bool is_check_dirty = true)
    {
      if (this.mUnit == null)
        return;
      if (is_check_dirty)
      {
        foreach (TacticsUnitController.ShieldState shieldState in this.ShieldStates)
        {
          if (!shieldState.Target.is_ignore_efficacy)
          {
            if ((int) shieldState.Target.hp != shieldState.LastHP || shieldState.Target.is_efficacy)
              shieldState.Dirty = true;
          }
          else
            shieldState.ClearDirty();
        }
      }
      foreach (Unit.UnitShield shield in this.mUnit.Shields)
      {
        Unit.UnitShield unit_shield = shield;
        if (this.ShieldStates.Find((Predicate<TacticsUnitController.ShieldState>) (sh => sh.Target == unit_shield)) == null)
        {
          TacticsUnitController.ShieldState shieldState = new TacticsUnitController.ShieldState();
          shieldState.Target = unit_shield;
          shieldState.ClearDirty();
          this.ShieldStates.Add(shieldState);
        }
      }
    }

    public void RemoveObsoleteShieldStates()
    {
      if (this.mUnit == null)
        return;
      for (int index = this.ShieldStates.Count - 1; index >= 0; --index)
      {
        if (!this.mUnit.Shields.Contains(this.ShieldStates[index].Target))
          this.ShieldStates.RemoveAt(index);
      }
    }

    private void ShowCriticalEffect(Vector3 position, float yOffset)
    {
      Camera main = Camera.main;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) main, (UnityEngine.Object) null))
      {
        GameSettings instance = GameSettings.Instance;
        FlashEffect flashEffect = ((Component) main).gameObject.AddComponent<FlashEffect>();
        flashEffect.Strength = instance.CriticalHit_FlashStrength;
        flashEffect.Duration = instance.CriticalHit_FlashDuration;
        CameraShakeEffect cameraShakeEffect = ((Component) main).gameObject.AddComponent<CameraShakeEffect>();
        cameraShakeEffect.Duration = instance.CriticalHit_ShakeDuration;
        cameraShakeEffect.AmplitudeX = instance.CriticalHit_ShakeAmplitudeX;
        cameraShakeEffect.AmplitudeY = instance.CriticalHit_ShakeAmplitudeY;
        cameraShakeEffect.FrequencyX = instance.CriticalHit_ShakeFrequencyX;
        cameraShakeEffect.FrequencyY = instance.CriticalHit_ShakeFrequencyY;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
        return;
      SceneBattle.Instance.PopupCritical(position, yOffset);
    }

    private void DrainGems(TacticsUnitController goal)
    {
      if (this.GemDrainEffects == null || this.DrainGemsOnHit <= 0)
        return;
      int num = this.DrainGemsOnHit + Random.Range(0, GameSettings.Instance.Gem_DrainCount_Randomness);
      Transform transform = ((Component) goal).transform;
      Vector3 vector3_1 = Vector3.op_Subtraction(((Component) this).transform.position, transform.position);
      Vector3 centerPosition = this.CenterPosition;
      Vector3 vector3_2 = Vector3.op_Subtraction(goal.CenterPosition, transform.position);
      for (int index1 = 0; index1 < num; ++index1)
      {
        Quaternion quaternion = Quaternion.op_Multiply(Quaternion.AngleAxis((float) Random.Range(135, 225), vector3_1), Quaternion.AngleAxis((float) Random.Range(70, 110), Vector3.right));
        for (int index2 = 0; index2 < this.GemDrainEffects.Length; ++index2)
        {
          if (!((Component) this.GemDrainEffects[index2]).gameObject.activeInHierarchy)
          {
            this.GemDrainEffects[index2].Reset();
            ((Component) this.GemDrainEffects[index2]).gameObject.SetActive(true);
            ((Component) this.GemDrainEffects[index2]).transform.position = centerPosition;
            ((Component) this.GemDrainEffects[index2]).transform.rotation = quaternion;
            this.GemDrainEffects[index2].TargetObject = transform;
            this.GemDrainEffects[index2].TargetOffset = vector3_2;
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GemDrainHitEffect, (UnityEngine.Object) null))
            {
              if (!GemParticleHitEffect.IsEnable)
                GemParticleHitEffect.IsEnable = true;
              ((Component) this.GemDrainEffects[index2]).gameObject.AddComponent<GemParticleHitEffect>();
              ((Component) this.GemDrainEffects[index2]).gameObject.GetComponent<GemParticleHitEffect>().EffectPrefab = this.GemDrainHitEffect;
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
      set => this.mShaker.ShakeStart = true;
      get => this.mShaker.ShakeStart;
    }

    public bool IsShakeEnd() => this.mShaker.ShakeCount <= 0;

    public float GetShakeProgress()
    {
      return (float) (1 - this.mShaker.ShakeCount / this.mShaker.ShakeMaxCount);
    }

    public void AdvanceShake()
    {
      ((Component) this).transform.localPosition = this.mShaker.AdvanceShake();
    }

    public void SkillTurn(LogSkill Act, EUnitDirection AxisDirection)
    {
      if (this.mSkillVars == null || this.mSkillVars.mSkillSequence == null)
        return;
      SceneBattle instance = SceneBattle.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      Vector3 vector3 = instance.CalcGridCenter(Act.self.x, Act.self.y);
      Vector3 zero = Vector3.zero;
      IntVector2 intVector2_1 = new IntVector2();
      Vector3 pos;
      if (Act.CauseOfReaction != null)
      {
        pos = instance.CalcGridCenter(Act.CauseOfReactionPos.x, Act.CauseOfReactionPos.y);
        intVector2_1.x = Act.CauseOfReactionPos.x;
        intVector2_1.y = Act.CauseOfReactionPos.y;
      }
      else
      {
        int x = Act.pos.x;
        int y = Act.pos.y;
        if (Act.skill != null && !Act.skill.IsAreaSkill() && Act.targets != null && Act.targets.Count != 0)
        {
          Unit target = Act.targets[0].target;
          if (!target.IsNormalSize)
          {
            x = target.x;
            y = target.y;
          }
        }
        pos = instance.CalcGridCenter(x, y);
        intVector2_1.x = x;
        intVector2_1.y = y;
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
          IntVector2 intVector2_3 = instance.CalcCoord(((Component) this).transform.position);
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
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSkillVars.HitTimers[index].Target, (UnityEngine.Object) target))
          return true;
      }
      return false;
    }

    public bool IsStartSkill() => this.mSkillVars != null;

    public SkillSequence.MapCameraTypes GetSkillCameraType()
    {
      return this.mSkillVars != null && this.mSkillVars.mSkillSequence != null ? this.mSkillVars.mSkillSequence.MapCameraType : SkillSequence.MapCameraTypes.None;
    }

    public bool IsSkillMirror()
    {
      return this.mSkillVars != null && this.mSkillVars.mSkillSequence != null && !this.mSkillVars.mSkillSequence.NotMirror && this.Unit.Side == EUnitSide.Enemy;
    }

    public void LoadDeathAnimation(TacticsUnitController.DeathAnimationTypes mask)
    {
      if ((mask & TacticsUnitController.DeathAnimationTypes.Normal) == (TacticsUnitController.DeathAnimationTypes) 0 || !UnityEngine.Object.op_Equality((UnityEngine.Object) this.FindAnimation("B_DEAD"), (UnityEngine.Object) null))
        return;
      this.LoadUnitAnimationAsync("B_DEAD", TacticsUnitController.ANIM_DOWN_STAND, this.mCharacterData.IsJobDownstand);
    }

    public void PlayDead(TacticsUnitController.DeathAnimationTypes type)
    {
      this.GotoState<TacticsUnitController.State_Dead>();
    }

    private void CalcCameraPos(
      AnimationClip clip,
      float normalizedTime,
      int cameraIndex,
      out Vector3 pos,
      out Quaternion rotation)
    {
      pos = Vector3.zero;
      rotation = Quaternion.identity;
      GameObject gameObject1 = new GameObject();
      GameObject gameObject2 = new GameObject("Camera001");
      GameObject gameObject3 = new GameObject("Camera002");
      Transform transform1 = gameObject1.transform;
      Transform transform2 = gameObject2.transform;
      Transform transform3 = gameObject3.transform;
      transform1.SetParent(((Component) this).transform, false);
      transform2.SetParent(transform1, false);
      transform3.SetParent(transform1, false);
      clip.SampleAnimation(gameObject1, normalizedTime * clip.length);
      Transform transform4 = cameraIndex == 0 ? transform2 : transform3;
      pos = Vector3.op_Addition(Vector3.op_Subtraction(transform4.position, ((Component) this).transform.position), this.RootMotionInverse);
      rotation = transform4.rotation;
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) gameObject1);
      rotation = Quaternion.op_Multiply(rotation, GameUtility.Yaw180);
    }

    private void SetActiveCameraPosition(Vector3 position, Quaternion rotation)
    {
      Transform transform = ((Component) this.mSkillVars.mActiveCamera).transform;
      transform.position = position;
      transform.rotation = rotation;
    }

    private void AnimateCamera(AnimationClip cameraClip, float normalizedTime, int cameraIndex = 0)
    {
      this.AnimateCameraInterpolated(cameraClip, normalizedTime, 1f, TacticsUnitController.CameraState.Default, cameraIndex);
    }

    private void AnimateCameraInterpolated(
      AnimationClip cameraClip,
      float normalizedTime,
      float interpRate,
      TacticsUnitController.CameraState startState,
      int cameraIndex = 0)
    {
      Vector3 pos;
      Quaternion rotation;
      this.CalcCameraPos(cameraClip, normalizedTime, cameraIndex, out pos, out rotation);
      Vector3 position = Vector3.op_Addition(((Component) this).transform.position, pos);
      Quaternion quaternion = rotation;
      if ((double) interpRate < 1.0)
      {
        position = Vector3.Lerp(startState.Position, position, interpRate);
        quaternion = Quaternion.Slerp(startState.Rotation, quaternion, interpRate);
      }
      this.SetActiveCameraPosition(position, Quaternion.op_Multiply(this.mSkillVars.mCameraShakeOffset, quaternion));
    }

    public void PlayDamageReaction(int damage, HitReactionTypes hitType = HitReactionTypes.Normal)
    {
      if (this.ShouldDefendHits || damage <= 0)
        return;
      this.PlayDamage(hitType);
    }

    [DebuggerHidden]
    private IEnumerator ShowHitPopup(
      TacticsUnitController target,
      bool critical,
      bool backstab,
      bool guard,
      bool weak,
      bool resist,
      bool hpDamage,
      int hpDamageValue,
      bool mpDamage,
      int mpDamageValue,
      bool absorb,
      bool is_guts)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CShowHitPopup\u003Ec__Iterator2()
      {
        guard = guard,
        target = target,
        critical = critical,
        backstab = backstab,
        weak = weak,
        resist = resist,
        absorb = absorb,
        is_guts = is_guts,
        hpDamage = hpDamage,
        hpDamageValue = hpDamageValue,
        mpDamage = mpDamage,
        mpDamageValue = mpDamageValue,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator ShowHealPopup(TacticsUnitController target, int hpHeal, int mpHeal)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CShowHealPopup\u003Ec__Iterator3()
      {
        hpHeal = hpHeal,
        target = target,
        mpHeal = mpHeal
      };
    }

    private void HitTarget(
      TacticsUnitController target,
      HitReactionTypes hitReaction = HitReactionTypes.None,
      bool doReaction = true)
    {
      bool flag1 = target.mHitInfo.IsCombo();
      bool isLast = this.mSkillVars.NumHitsLeft == 0;
      bool flag2 = !this.mSkillVars.UseBattleScene;
      bool flag3 = false;
      bool absorb = target.mHitInfo.shieldDamage > 0;
      bool is_guts = (target.mHitInfo.hitType & LogSkill.EHitTypes.Guts) != (LogSkill.EHitTypes) 0;
      int num1 = 0;
      bool flag4 = target.ShouldDodgeHits;
      int hpDamageValue;
      int mpDamageValue;
      if (flag1)
      {
        int index = target.mHitInfo.hits.Count - 1 - this.mSkillVars.NumHitsLeft;
        if (index < 0 || index >= target.mHitInfo.hits.Count)
          index = 0;
        hpDamageValue = target.mHitInfo.hits[index].hp_damage;
        mpDamageValue = target.mHitInfo.hits[index].mp_damage;
        flag4 = target.mHitInfo.hits[index].is_avoid;
      }
      else
      {
        hpDamageValue = target.mHitInfo.GetTotalHpDamage();
        mpDamageValue = target.mHitInfo.GetTotalMpDamage();
      }
      if (flag2 && hpDamageValue < 1 && num1 < 1)
        flag2 = true;
      int totalHpHeal = target.mHitInfo.GetTotalHpHeal();
      int totalMpHeal = target.mHitInfo.GetTotalMpHeal();
      if (doReaction && (hpDamageValue >= 1 || mpDamageValue >= 1))
        target.PlayDamageReaction(1, hitReaction);
      if (target.mKnockBackGrid != null && isLast)
        target.mKnockBackMode = TacticsUnitController.eKnockBackMode.START;
      if (this.mSkillVars.Skill.effect_type != SkillEffectTypes.Throw && this.mSkillVars.Skill.effect_type != SkillEffectTypes.DynamicTransformUnit && UnityEngine.Object.op_Inequality((UnityEngine.Object) target, (UnityEngine.Object) this) && this.mSkillVars.NumHitsLeft == this.mSkillVars.TotalHits - 1)
      {
        target.AutoUpdateRotation = false;
        if (this.mSkillVars.Skill.cast_type == ECastTypes.Jump && Vector3.op_Equality(((Component) target).transform.position, ((Component) this).transform.position))
        {
          Vector3 position = ((Component) this).transform.position;
          SceneBattle instance = SceneBattle.Instance;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
            position = instance.CalcGridCenter(this.mUnit.x, this.mUnit.y);
          target.LookAt(position);
        }
        else
          target.LookAt(((Component) this).transform.position);
      }
      target.DrainGems(this);
      this.StartCoroutine(this.ShowHitPopup(target, target.ShowCriticalEffectOnHit, target.ShowBackstabEffectOnHit && UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null), target.ShouldDefendHits, target.ShowElementEffectOnHit == TacticsUnitController.EElementEffectTypes.Effective, target.ShowElementEffectOnHit == TacticsUnitController.EElementEffectTypes.NotEffective, false, 0, false, 0, absorb, is_guts));
      if (!target.ShouldDefendHits && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Changing && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Throw)
        this.SpawnHitEffect(target, isLast);
      if (!flag1 && !isLast)
        return;
      bool flag5 = false;
      if (this.mSkillVars.Skill.IsDamagedSkill() && !flag4)
      {
        bool hpDamage = hpDamageValue >= 1;
        bool mpDamage = mpDamageValue >= 1;
        this.StartCoroutine(this.ShowHitPopup(target, false, false, false, false, false, hpDamage, hpDamageValue, mpDamage, mpDamageValue, false, is_guts));
        if (mpDamage && isLast)
          flag3 = true;
        if (flag2 && hpDamage)
        {
          float duration = !isLast ? 0.1f : 0.5f;
          target.UpdateHPRelative(-hpDamageValue, duration, this.mSkillVars.Skill.IsMhmDamage());
          target.OnHitGaugeWeakRegist(this.mUnit);
          target.ChargeIcon.Close();
          if (target.mHitInfo.IsDefend())
          {
            if (this.mSkillVars.TotalHits <= 1 || this.mSkillVars.TotalHits - this.mSkillVars.NumHitsLeft <= 1)
              target.Unit.PlayBattleVoice("battle_0016");
          }
          else
          {
            int num2 = Mathf.Max(hpDamageValue, mpDamageValue);
            if (this.mSkillVars.TotalHits > 1)
              num2 /= this.mSkillVars.TotalHits;
            float num3 = (int) target.Unit.MaximumStatus.param.hp == 0 ? 1f : 1f * (float) num2 / (float) (int) target.Unit.MaximumStatus.param.hp;
            if ((double) num3 >= 0.75)
              target.Unit.PlayBattleVoice("battle_0036");
            else if ((double) num3 > 0.5)
              target.Unit.PlayBattleVoice("battle_0035");
            else
              target.Unit.PlayBattleVoice("battle_0034");
          }
        }
      }
      this.StartCoroutine(this.ShowHealPopup(target, totalHpHeal, totalMpHeal));
      if (totalHpHeal >= 1 && flag2)
      {
        target.UpdateHPRelative(totalHpHeal);
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
        SceneBattle.Instance.PopupParamChange(target.UIDispPosition, target.mHitInfo.buff, target.mHitInfo.debuff, target.mHitInfo.ChangeValueCT);
        if (!flag5 && this.Unit != target.Unit && target.mHitInfo.buff.CheckEffect())
          target.Unit.PlayBattleVoice("battle_0018");
      }
      if (!flag3)
        return;
      UnitQueue.Instance.Refresh(target.Unit);
    }

    public void BuffEffectTarget()
    {
      if (this.mSkillVars == null || this.mSkillVars.Skill == null || this.mSkillVars.Targets == null || !this.mSkillVars.Skill.IsPrevApply())
        return;
      foreach (TacticsUnitController target in this.mSkillVars.Targets)
      {
        if (target.mHitInfo.IsBuffEffect() || target.mHitInfo.ChangeValueCT != 0)
        {
          SceneBattle.Instance.PopupParamChange(target.UIDispPosition, target.mHitInfo.buff, target.mHitInfo.debuff, target.mHitInfo.ChangeValueCT);
          this.mSkillVars.mIsFinishedBuffEffectTarget = true;
        }
      }
    }

    public void BuffEffectSelf()
    {
      if (this.mSkillVars == null || this.mSkillVars.Skill == null || !this.mSkillVars.Skill.IsPrevApply() || this.mHitInfoSelf == null || this.mHitInfoSelf.hits.Count <= 0 && !this.mHitInfoSelf.buff.CheckEffect() && !this.mHitInfoSelf.debuff.CheckEffect() || !this.mHitInfoSelf.IsBuffEffect())
        return;
      SceneBattle.Instance.PopupParamChange(this.UIDispPosition, this.mHitInfoSelf.buff, this.mHitInfoSelf.debuff);
      this.mSkillVars.mIsFinishedBuffEffectSelf = true;
    }

    public void SkillEffectSelf()
    {
      if (this.mHitInfoSelf == null || this.mHitInfoSelf.hits.Count <= 0 && !this.mHitInfoSelf.buff.CheckEffect() && !this.mHitInfoSelf.debuff.CheckEffect() && this.mSkillVars.HpCostDamage == 0)
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
        this.PlayDamageReaction(1);
      if (num > 0)
      {
        SceneBattle.Instance.PopupDamageNumber(this, num);
        MonoSingleton<GameManager>.Instance.Player.OnDamageToEnemy(this.mUnit, this.mUnit, num);
        if (flag1)
        {
          this.UpdateHPRelative(-num);
          this.OnHitGaugeWeakRegist(this.mUnit);
          this.ChargeIcon.Close();
        }
      }
      if (totalHpHeal >= 1 || totalMpHeal >= 1)
      {
        this.StartCoroutine(this.ShowHealPopup(this, totalHpHeal, totalMpHeal));
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDrainEffect, (UnityEngine.Object) null))
        {
          this.mDrainEffect.transform.position = this.CenterPosition;
          this.mDrainEffect.transform.rotation = ((Component) this).transform.rotation;
          this.mDrainEffect.SetActive(true);
        }
        if (flag1)
        {
          this.UpdateHPRelative(totalHpHeal);
          this.ChargeIcon.Close();
        }
      }
      if (!this.mSkillVars.mIsFinishedBuffEffectSelf && this.mHitInfoSelf.IsBuffEffect())
      {
        SceneBattle instance = SceneBattle.Instance;
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        {
          Vector3 position = this.UIDispPosition;
          if (this.mSkillVars.UseBattleScene && instance.Battle != null)
            position = instance.CalcGridCenter(instance.Battle.GetUnitGridPosition(this.mHitInfoSelf.target));
          instance.PopupParamChange(position, this.mHitInfoSelf.buff, this.mHitInfoSelf.debuff);
        }
      }
      this.UpdateBadStatus();
    }

    private bool ReflectTargetTypeToPos(ref Vector3 pos)
    {
      if (this.mSkillVars.Skill == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSkillVars.mSkillEffect, (UnityEngine.Object) null) || !SkillParam.IsTypeLaser(this.mSkillVars.Skill.select_scope))
        return false;
      SceneBattle instance = SceneBattle.Instance;
      BattleCore battleCore = (BattleCore) null;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        battleCore = instance.Battle;
      if (battleCore == null)
        return false;
      EUnitDirection index = instance.UnitDirectionFromPosition(((Component) this).transform.position, pos, this.mSkillVars.Skill);
      switch (this.mSkillVars.mSkillEffect.TargetTypeForLaser)
      {
        case SkillEffect.eTargetTypeForLaser.StepFront:
          int frontTypeForLaser = this.mSkillVars.mSkillEffect.StepFrontTypeForLaser;
          IntVector2 intVector2_1 = instance.CalcCoord(((Component) this).transform.position);
          pos.x = (float) (intVector2_1.x + Unit.DIRECTION_OFFSETS[(int) index, 0] * frontTypeForLaser) + 0.5f;
          pos.z = (float) (intVector2_1.y + Unit.DIRECTION_OFFSETS[(int) index, 1] * frontTypeForLaser) + 0.5f;
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
              IntVector2 intVector2_2 = instance.CalcCoord(((Component) this).transform.position);
              pos.x = (float) ((double) intVector2_2.x + (double) Unit.DIRECTION_OFFSETS[(int) index, 0] * ((double) num + 1.0) / 2.0 + 0.5);
              pos.z = (float) ((double) intVector2_2.y + (double) Unit.DIRECTION_OFFSETS[(int) index, 1] * ((double) num + 1.0) / 2.0 + 0.5);
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

    protected override void OnEventStart(AnimEvent e, float weight)
    {
      if (this.mSkillVars == null)
        return;
      if (e is ToggleCamera)
      {
        this.mSkillVars.mCameraID = (e as ToggleCamera).CameraID;
        if (this.mSkillVars.mCameraID >= 0 && this.mSkillVars.mCameraID <= 1)
          return;
        Debug.LogError((object) ("Invalid CameraID: " + (object) this.mSkillVars.mCameraID));
        this.mSkillVars.mCameraID = 0;
      }
      else if (e is HitFrame && (this.mSkillVars.mSkillSequence.SkillType == SkillSequence.SkillTypes.Melee || UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillVars.mSkillEffect, (UnityEngine.Object) null) && this.mSkillVars.mSkillEffect.IsTeleportMode))
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
            {
              this.mSkillVars.Targets[index1].PlayDodge(perfectAvoid, is_disp_popup);
              if (this.mSkillVars.NumHitsLeft == 0 && target.mKnockBackGrid != null)
                target.mKnockBackMode = TacticsUnitController.eKnockBackMode.START;
            }
            if (this.mSkillVars.mSkillEffect.AlwaysExplode && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Changing && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Throw)
              this.SpawnHitEffect(this.mSkillVars.Targets[index1], this.mSkillVars.NumHitsLeft == 0);
          }
          else
          {
            this.HitTarget(this.mSkillVars.Targets[index1], hitFrame.ReactionType);
            MonoSingleton<GameManager>.Instance.Player.OnDamageToEnemy(this.mUnit, target.Unit, target.mHitInfo.GetTotalHpDamage());
          }
        }
      }
      else if (e is ChantFrame)
      {
        ChantFrame chantFrame = (ChantFrame) e;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillVars.mSkillEffect.ChantEffect, (UnityEngine.Object) null))
          return;
        Vector3 spawnPos;
        Quaternion spawnRot;
        chantFrame.CalcPosition(this.UnitObject, this.mSkillVars.mSkillEffect.ChantEffect, out spawnPos, out spawnRot);
        this.mSkillVars.mChantEffect = UnityEngine.Object.Instantiate<GameObject>(this.mSkillVars.mSkillEffect.ChantEffect, spawnPos, spawnRot);
        GameUtility.SetLayer(this.mSkillVars.mChantEffect, ((Component) this).gameObject.layer, true);
        if (!chantFrame.AttachEffect)
          return;
        this.mSkillVars.mChantEffect.transform.parent = chantFrame.BoneName.Length <= 0 ? this.UnitObject.transform : GameUtility.findChildRecursively(this.UnitObject.transform, chantFrame.BoneName);
      }
      else if (e is ProjectileFrame && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillVars.mSkillEffect, (UnityEngine.Object) null) && this.IsRangedSkillType())
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
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillVars.mSkillEffect.ProjectileEffect, (UnityEngine.Object) null) && !pd.mIsHitOnly)
        {
          Vector3 spawnPos;
          Quaternion spawnRot;
          projectileFrame.CalcPosition(this.UnitObject, this.mSkillVars.mSkillEffect.ProjectileEffect, out spawnPos, out spawnRot);
          pd.mProjectile = UnityEngine.Object.Instantiate<GameObject>(this.mSkillVars.mSkillEffect.ProjectileEffect, spawnPos, spawnRot);
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
          Transform transform = ((Component) this).transform;
          MapProjectile mapProjectile = GameUtility.RequireComponent<MapProjectile>(pd.mProjectile);
          if (this.mSkillVars.mSkillEffect.MapHitEffectType == SkillEffect.MapHitEffectTypes.EachHits)
          {
            if (this.mSkillVars.Skill != null || SkillParam.IsTypeLaser(this.mSkillVars.Skill.select_scope))
            {
              Vector3 b = this.mSkillVars.mTargetPosition;
              float num1 = GameUtility.CalcDistance2D(transform.position, b);
              for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
              {
                Vector3 position = ((Component) this.mSkillVars.Targets[index]).transform.position;
                float num2 = GameUtility.CalcDistance2D(transform.position, position);
                if ((double) num1 < (double) num2)
                {
                  num1 = num2;
                  b = position;
                }
              }
              this.mSkillVars.mTargetPosition = b;
              this.mSkillVars.mTargetPosition.y = GameUtility.RaycastGround(this.mSkillVars.mTargetPosition).y;
            }
            else
            {
              float num3 = 0.0f;
              Vector3 vector3 = Vector3.op_Subtraction(this.mSkillVars.mTargetPosition, transform.position);
              vector3.y = 0.0f;
              ((Vector3) ref vector3).Normalize();
              for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
              {
                float num4 = Vector3.Dot(Vector3.op_Subtraction(((Component) this.mSkillVars.Targets[index]).transform.position, transform.position), vector3);
                if ((double) num4 >= (double) num3)
                  num3 = num4;
              }
              this.mSkillVars.mTargetPosition = Vector3.op_Addition(transform.position, Vector3.op_Multiply(vector3, num3));
              this.mSkillVars.mTargetPosition.y = GameUtility.RaycastGround(this.mSkillVars.mTargetPosition).y;
            }
          }
          this.ReflectTargetTypeToPos(ref this.mSkillVars.mTargetPosition);
          mapProjectile.StartCameraTargetPosition = transform.position;
          mapProjectile.EndCameraTargetPosition = this.mSkillVars.mTargetPosition;
          mapProjectile.GoalPosition = Vector3.op_Addition(this.mSkillVars.mTargetPosition, Vector3.op_Multiply(Vector3.up, 0.5f));
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSkillVars.mTargetController) && this.mSkillVars.mTargetController.Unit != null && !this.mSkillVars.mTargetController.Unit.IsNormalSize)
          {
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSkillVars.mTargetController) && this.mSkillVars.Skill != null && !this.mSkillVars.Skill.IsAreaSkill())
            {
              SceneBattle instance = SceneBattle.Instance;
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
                this.mSkillVars.mTargetPosition = instance.CalcGridCenter(this.mSkillVars.mTargetController.Unit.x, this.mSkillVars.mTargetController.Unit.y);
            }
            Vector3 mTargetPosition = this.mSkillVars.mTargetPosition;
            mTargetPosition.y += this.mSkillVars.mTargetController.Unit.OffsZ;
            mapProjectile.EndCameraTargetPosition = mTargetPosition;
            Vector3 vector3 = Vector3.op_Addition(this.mSkillVars.mTargetPosition, Vector3.op_Multiply(Vector3.up, 0.5f));
            vector3.y += this.mSkillVars.mTargetController.Unit.OffsZ;
            mapProjectile.GoalPosition = vector3;
          }
          mapProjectile.Speed = this.mSkillVars.mSkillEffect.MapProjectileSpeed;
          mapProjectile.HitDelay = this.mSkillVars.mSkillEffect.MapProjectileHitDelay;
          mapProjectile.OnHit = new MapProjectile.HitEvent(this.OnProjectileHit);
          mapProjectile.OnDistanceUpdate = new MapProjectile.DistanceChangeEvent(this.OnProjectileDistanceChange);
          mapProjectile.mProjectileData = pd;
          if (this.mSkillVars.mSkillEffect.IsTeleportMode && (double) e.End > (double) e.Start + 0.10000000149011612)
          {
            float num = e.End - e.Start;
            Vector3 vector3 = Vector3.op_Subtraction(mapProjectile.EndCameraTargetPosition, mapProjectile.StartCameraTargetPosition);
            mapProjectile.Speed = ((Vector3) ref vector3).magnitude / num;
            if (Vector3.op_Equality(vector3, Vector3.zero))
              mapProjectile.TeleportTime = num;
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillVars.mActiveCamera, (UnityEngine.Object) null) && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Changing && this.mSkillVars.mSkillSequence.MapCameraType == SkillSequence.MapCameraTypes.None)
          {
            mapProjectile.CameraTransform = ((Component) this.mSkillVars.mActiveCamera).transform;
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
          float num5 = this.mMapTrajectoryHeight;
          if ((double) GameUtility.CalcDistance2D(Vector3.op_Subtraction(mapProjectile.GoalPosition, ((Component) mapProjectile).transform.position)) <= 1.2000000476837158)
          {
            mapProjectile.IsArrow = false;
            mapProjectile.Speed = this.mSkillVars.mSkillEffect.MapProjectileSpeed;
          }
          else
          {
            mapProjectile.TimeScale = this.mSkillVars.mSkillEffect.MapTrajectoryTimeScale;
            if ((double) this.mSkillVars.mStartPosition.y >= (double) num5)
              num5 = this.mSkillVars.mStartPosition.y;
            mapProjectile.TopHeight = num5 + 0.5f;
          }
        }
      }
      else if (e is CameraShake)
      {
        this.mSkillVars.mCameraShakeSeedX = Random.value;
        this.mSkillVars.mCameraShakeSeedY = Random.value;
      }
      else
      {
        if (e is SRPG.AnimEvents.TargetState && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillVars.mTargetController, (UnityEngine.Object) null))
        {
          switch ((e as SRPG.AnimEvents.TargetState).State)
          {
            case SRPG.AnimEvents.TargetState.StateTypes.Stand:
              this.mSkillVars.mTargetController.PlayIdle();
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
          if (!this.mSkillVars.mAuraEnable || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillVars.mSkillEffect, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillVars.mSkillEffect.AuraEffect, (UnityEngine.Object) null))
            return;
          AuraFrame auraFrame = e as AuraFrame;
          Vector3 spawnPos;
          Quaternion spawnRot;
          auraFrame.CalcPosition(this.UnitObject, this.mSkillVars.mSkillEffect.AuraEffect, out spawnPos, out spawnRot);
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mSkillVars.mSkillEffect.AuraEffect, spawnPos, spawnRot);
          Transform transform1 = gameObject.transform;
          Transform transform2 = (Transform) null;
          if (!string.IsNullOrEmpty(auraFrame.BoneName))
            transform2 = GameUtility.findChildRecursively(this.UnitObject.transform, auraFrame.BoneName);
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) transform2, (UnityEngine.Object) null))
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
          float num6 = 0.0f;
          if (this.mSkillVars.Targets != null)
          {
            for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
            {
              TacticsUnitController target = this.mSkillVars.Targets[index];
              if (!target.ShouldDodgeHits)
              {
                int num7 = this.mSkillVars.TotalHits <= 1 ? target.mHitInfo.GetTotalHpDamage() : target.mHitInfo.GetTotalHpDamage() / this.mSkillVars.TotalHits;
                num6 = Mathf.Max((int) this.Unit.MaximumStatus.param.hp == 0 ? 1f : 1f * (float) num7 / (float) (int) this.Unit.MaximumStatus.param.hp, num6);
              }
            }
          }
          if ((double) num6 >= 0.75)
            this.Unit.PlayBattleVoice("battle_0033");
          else if ((double) num6 > 0.5)
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
    private IEnumerator AnimateProjectile(
      AnimationClip clip,
      float length,
      Vector3 basePosition,
      Quaternion baseRotation,
      TacticsUnitController.ProjectileStopEvent callback,
      TacticsUnitController.ProjectileData pd)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CAnimateProjectile\u003Ec__Iterator4()
      {
        length = length,
        clip = clip,
        pd = pd,
        basePosition = basePosition,
        baseRotation = baseRotation,
        callback = callback
      };
    }

    private void CalcEnemyPos(
      AnimationClip clip,
      float normalizedTime,
      out Vector3 pos,
      out Quaternion rotation)
    {
      GameObject gameObject1 = new GameObject();
      GameObject gameObject2 = new GameObject("Enm_Distance_dummy");
      Transform transform1 = gameObject1.transform;
      Transform transform2 = gameObject2.transform;
      transform2.SetParent(transform1, false);
      transform1.SetParent(((Component) this).transform, false);
      clip.SampleAnimation(gameObject1, normalizedTime * clip.length);
      pos = Vector3.op_Addition(transform2.position, this.RootMotionInverse);
      Quaternion quaternion = Quaternion.op_Multiply(transform2.localRotation, Quaternion.AngleAxis(90f, Vector3.right));
      rotation = Quaternion.op_Multiply(transform1.rotation, quaternion);
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) gameObject1);
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

    public void PlayPreSkillAnimation(Camera cam, Vector3 targetPos)
    {
      if (this.mSkillVars == null)
        return;
      this.mSkillVars.mTargetPosition = targetPos;
      this.mSkillVars.mActiveCamera = cam;
      this.GotoState<TacticsUnitController.State_PreSkill>();
    }

    public void PlayDamage(HitReactionTypes hitType)
    {
      if (this.mSkillVars != null || !this.Unit.IsEnablePlayAnimCondition() || this.mUnit != null && this.mUnit.IsBreakObj)
        return;
      if (this.mStateMachine.IsInState<TacticsUnitController.State_BigUnitBsIdle>() || this.mStateMachine.IsInState<TacticsUnitController.State_BigUnitBsDamage>())
      {
        this.PlayBigUnitBsDamage();
      }
      else
      {
        switch (hitType)
        {
          case HitReactionTypes.Normal:
          case HitReactionTypes.Aerial:
            this.GotoState<TacticsUnitController.State_NormalDamage>();
            break;
          case HitReactionTypes.Kirimomi:
            this.PlayKirimomi();
            break;
        }
      }
    }

    public void PlayDodge(bool perfectAvoid, bool is_disp_popup = true)
    {
      if (is_disp_popup && UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
      {
        if (perfectAvoid)
          SceneBattle.Instance.PopupPefectAvoid(this.UIDispPosition);
        else
          SceneBattle.Instance.PopupMiss(this.UIDispPosition);
      }
      if (this.mSkillVars != null || !this.Unit.IsEnablePlayAnimCondition() || UnityEngine.Object.op_Equality((UnityEngine.Object) this.FindAnimation("B_DGE"), (UnityEngine.Object) null) || this.mStateMachine.IsInState<TacticsUnitController.State_BigUnitBsIdle>() || this.mStateMachine.IsInState<TacticsUnitController.State_BigUnitBsDamage>())
        return;
      this.GotoState<TacticsUnitController.State_Dodge>();
    }

    public void LoadDodgeAnimation()
    {
      if (this.mUnit != null && this.mUnit.IsBreakObj)
        return;
      this.LoadUnitAnimationAsync("B_DGE", TacticsUnitController.ANIM_DODGE, this.mCharacterData.IsJobDodge);
    }

    public void LoadDamageAnimations()
    {
      if (this.mUnit != null && this.mUnit.IsBreakObj)
        return;
      this.LoadUnitAnimationAsync("B_DMG0", TacticsUnitController.ANIM_DAMAGE, this.mCharacterData.IsJobDamage);
      this.LoadUnitAnimationAsync("B_DOWN", TacticsUnitController.ANIM_DOWN, false);
    }

    public void LoadSkillSequence(
      SkillParam skillParam,
      SkillMotion skill_motion,
      bool loadJobAnimation,
      bool useBattleScene,
      bool is_cs = false,
      bool is_cs_sub = false)
    {
      this.mSkillVars = new TacticsUnitController.SkillVars();
      this.mSkillVars.UseBattleScene = useBattleScene;
      if (!useBattleScene)
        ;
      this.mSkillVars.Skill = skillParam;
      this.mSkillVars.mIsCollaboSkill = is_cs;
      this.mSkillVars.mIsCollaboSkillSub = is_cs_sub;
      this.mSkillVars.mSkillMotion = skill_motion;
      this.AddLoadThreadCount();
      this.StartCoroutine(this.LoadSkillSequenceAsync(skillParam, loadJobAnimation));
    }

    public bool HasPreSkillAnimation
    {
      get
      {
        return this.mSkillVars != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillVars.mSkillStartAnimation, (UnityEngine.Object) null);
      }
    }

    public bool IsSkillParentPosZero
    {
      get
      {
        return this.mSkillVars != null && !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSkillVars.mSkillAnimation, (UnityEngine.Object) null) && this.mSkillVars.mSkillAnimation.IsParentPosZero;
      }
    }

    public bool IsPreSkillParentPosZero
    {
      get
      {
        return this.mSkillVars != null && !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSkillVars.mSkillStartAnimation, (UnityEngine.Object) null) && this.mSkillVars.mSkillStartAnimation.IsParentPosZero;
      }
    }

    public bool IsSkillRidingUnitCHM
    {
      get
      {
        return this.mSkillVars != null && !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSkillVars.mSkillAnimation, (UnityEngine.Object) null) && this.mSkillVars.mSkillAnimation.IsRidingUnitCHM;
      }
    }

    [DebuggerHidden]
    private IEnumerator LoadSkillSequenceAsync(SkillParam skillParam, bool loadJobAnimation)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CLoadSkillSequenceAsync\u003Ec__Iterator5()
      {
        skillParam = skillParam,
        loadJobAnimation = loadJobAnimation,
        \u0024this = this
      };
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
      return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillVars.mSkillEffect, (UnityEngine.Object) null);
    }

    [DebuggerHidden]
    private IEnumerator LoadSkillEffectAsync(string skillEffectName)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CLoadSkillEffectAsync\u003Ec__Iterator6()
      {
        skillEffectName = skillEffectName,
        \u0024this = this
      };
    }

    public SkillEffect LoadedSkillEffect
    {
      get => this.mSkillVars != null ? this.mSkillVars.mSkillEffect : (SkillEffect) null;
    }

    public void UnloadBattleAnimations()
    {
      this.UnloadAnimation("B_DMG0");
      this.UnloadAnimation("B_DOWN");
      this.UnloadAnimation("B_DGE");
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
      this.UnloadAnimation("B_BU_BS_IDLE");
      this.UnloadAnimation("B_BU_BS_DAMAGE");
    }

    private void ResetSkill()
    {
      this.mCollideGround = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillVars.mTargetController, (UnityEngine.Object) null))
        this.mSkillVars.mTargetController.mCollideGround = true;
      this.StopAura();
      this.mLastHitEffect = (GameObject) null;
      if (this.mSkillVars.HitTimerThread != null)
        this.StopCoroutine(this.mSkillVars.HitTimerThread);
      foreach (TacticsUnitController.ProjectileData projectileDataList in this.mSkillVars.mProjectileDataLists)
        GameUtility.DestroyGameObject(projectileDataList.mProjectile);
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
        this.PlayIdle();
      this.ActivateRidingModelByJob();
      this.ResetSkill();
    }

    private int CountSkillUnitVoice()
    {
      int num = 0;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillVars.mSkillAnimation, (UnityEngine.Object) null) && this.mSkillVars.mSkillAnimation.events != null)
      {
        for (int index = 0; index < this.mSkillVars.mSkillAnimation.events.Length; ++index)
        {
          if (this.mSkillVars.mSkillAnimation.events[index] is UnitVoiceEvent)
            ++num;
        }
      }
      return num;
    }

    public void SetLastHitEffect(GameObject effect) => this.mLastHitEffect = effect;

    public void SetHitInfo(LogSkill.Target target) => this.mHitInfo = target;

    public void SetHitInfoSelf(LogSkill.Target selfTarget) => this.mHitInfoSelf = selfTarget;

    public int CountSkillHits()
    {
      int num = 0;
      switch (this.mSkillVars.mSkillSequence.SkillType)
      {
        case SkillSequence.SkillTypes.Melee:
        case SkillSequence.SkillTypes.Ranged:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillVars.mSkillAnimation, (UnityEngine.Object) null) && this.mSkillVars.mSkillAnimation.events != null)
          {
            for (int index = 0; index < this.mSkillVars.mSkillAnimation.events.Length; ++index)
            {
              if (this.mSkillVars.mSkillAnimation.events[index] is HitFrame)
                ++num;
            }
          }
          if (this.mSkillVars.mSkillSequence.SkillType == SkillSequence.SkillTypes.Ranged && num == 0)
          {
            num = 1;
            break;
          }
          break;
        case SkillSequence.SkillTypes.RangedRayNoMovCmr:
        case SkillSequence.SkillTypes.RangedRayFirstMovCmr:
        case SkillSequence.SkillTypes.RangedRayLastMovCmr:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillVars.mSkillAnimation, (UnityEngine.Object) null) && this.mSkillVars.mSkillAnimation.events != null)
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

    private void SpawnHitEffect(TacticsUnitController target, bool isLast)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) target))
        return;
      Vector3 position1 = ((Component) target).transform.position;
      if (target.Unit != null && !target.Unit.IsNormalSize)
        position1.y += target.Unit.OffsZ;
      SkillEffect mSkillEffect = this.mSkillVars.mSkillEffect;
      Vector3 position2 = position1;
      Quaternion identity = Quaternion.identity;
      Quaternion rotation = ((Component) this).transform.rotation;
      double y = (double) ((Quaternion) ref rotation).eulerAngles.y;
      mSkillEffect.SpawnExplosionEffect(0, position2, identity, (float) y);
      if (!isLast || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mLastHitEffect, (UnityEngine.Object) null))
        return;
      GameUtility.SpawnParticle(this.mLastHitEffect, position1, Quaternion.identity, (GameObject) null);
      TimeManager.StartHitSlow(0.1f, 0.3f);
    }

    private void SpawnDefendHitEffect(SkillData defSkill, int useCount, int useCountMax)
    {
      if (defSkill == null)
        return;
      string defendEffect = defSkill.SkillParam.defend_effect;
      if (string.IsNullOrEmpty(defendEffect) || !this.mDefendSkillEffects.ContainsKey(defendEffect))
        return;
      GameObject defendSkillEffect = this.mDefendSkillEffects[defendEffect];
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) defendSkillEffect, (UnityEngine.Object) null))
        return;
      Animator component = GameUtility.SpawnParticle(defendSkillEffect, ((Component) this).transform.position, Quaternion.identity, (GameObject) null).GetComponent<Animator>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
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
        target.PlayDodge(perfectAvoid);
        flag1 = this.mSkillVars.mSkillEffect.AlwaysExplode;
      }
      if (!flag1)
        return;
      this.HitTarget(target, this.mSkillVars.mSkillEffect.RangedHitReactionType);
      MonoSingleton<GameManager>.Instance.Player.OnDamageToEnemy(this.mUnit, target.Unit, target.mHitInfo.GetTotalHpDamage());
    }

    [DebuggerHidden]
    private IEnumerator AsyncHitTimer()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CAsyncHitTimer\u003Ec__Iterator7()
      {
        \u0024this = this
      };
    }

    private void InternalStartSkill(
      TacticsUnitController[] targets,
      Vector3 targetPosition,
      Camera activeCamera,
      bool doStateChange = true)
    {
      if (this.mSkillVars.mSkillSequence == null)
      {
        Debug.LogError((object) "SkillSequence not loaded yet");
      }
      else
      {
        this.mCollideGround = !this.mSkillVars.UseBattleScene;
        this.mSkillVars.mActiveCamera = activeCamera;
        this.mSkillVars.Targets = new List<TacticsUnitController>((IEnumerable<TacticsUnitController>) targets);
        if (this.mSkillVars.UseBattleScene)
          this.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
        this.mSkillVars.HitTimerThread = this.StartCoroutine(this.AsyncHitTimer());
        this.mSkillVars.mStartPosition = ((Component) this).transform.position;
        if (targets.Length == 1)
        {
          this.mSkillVars.mTargetController = targets[0];
          this.mSkillVars.mTargetControllerPosition = ((Component) this.mSkillVars.mTargetController).transform.position;
          this.mSkillVars.mTargetController.mCollideGround = this.mCollideGround;
        }
        if (this.mSkillVars.UseBattleScene)
        {
          this.mSkillVars.mTargetPosition = this.mSkillVars.mTargetControllerPosition;
          if (this.mSkillVars.Targets.Count != 0)
          {
            TacticsUnitController target = this.mSkillVars.Targets[0];
            if (!target.Unit.IsNormalSize)
              target.PlayBigUnitBsIdle();
          }
        }
        else
          this.mSkillVars.mTargetPosition = targetPosition;
        this.mSkillVars.MaxPlayVoice = this.mSkillVars.NumPlayVoice = this.CountSkillUnitVoice();
        this.mSkillVars.TotalHits = this.mSkillVars.NumHitsLeft = this.CountSkillHits();
        this.mSkillVars.mAuraEnable = UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillVars.mSkillEffect.AuraEffect, (UnityEngine.Object) null);
        this.mSkillVars.mCameraID = 0;
        this.mSkillVars.mChantCameraID = 0;
        this.mSkillVars.mSkillCameraID = 0;
        this.mSkillVars.mProjectileDataLists.Clear();
        this.mSkillVars.mNumShotCount = 0;
        this.mDamageDispCounter = 0;
        SceneBattle instance = SceneBattle.Instance;
        BattleCore battleCore = (BattleCore) null;
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
          battleCore = instance.Battle;
        if (battleCore != null && battleCore.IsSkillDirection)
        {
          List<GameObject> goRidingModelList = this.GoRidingModelList;
          if (goRidingModelList != null)
          {
            foreach (GameObject gameObject in goRidingModelList)
            {
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
                gameObject.SetActive(this.IsSkillRidingUnitCHM);
            }
          }
        }
        if (doStateChange && this.mSkillVars.Skill.cast_type == ECastTypes.Jump)
        {
          this.GotoState<TacticsUnitController.State_JumpCastComplete>();
        }
        else
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillVars.mSkillEffect, (UnityEngine.Object) null) && this.mSkillVars.mSkillEffect.StartSound != null)
            this.mSkillVars.mSkillEffect.StartSound.Play();
          if (!doStateChange)
            return;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillVars.mSkillChantAnimation, (UnityEngine.Object) null))
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

    public void StartSkill(
      Vector3 targetPosition,
      Camera activeCamera,
      TacticsUnitController[] targets,
      Vector3[] hitGrids,
      SkillParam skill)
    {
      this.mSkillVars.HitGrids = hitGrids;
      this.InternalStartSkill(targets, targetPosition, activeCamera);
    }

    public void StartSkill(TacticsUnitController target, Camera activeCamera, SkillParam skill)
    {
      this.InternalStartSkill(new TacticsUnitController[1]
      {
        target
      }, ((Component) target).transform.position, activeCamera);
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

    public void PlayKirimomi() => this.GotoState<TacticsUnitController.State_Kirimomi>();

    public void PlayDown() => this.GotoState<TacticsUnitController.State_Down>();

    public void SetHpCostSkill(int SkillHpCost) => this.mSkillVars.HpCostDamage = SkillHpCost;

    private void OnProjectileDistanceChange(GameObject go, float distance)
    {
      MapProjectile component = go.GetComponent<MapProjectile>();
      if (this.mSkillVars.mSkillEffect.MapHitEffectType != SkillEffect.MapHitEffectTypes.EachHits)
        return;
      for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
      {
        if (!this.mSkillVars.HitTargets.Contains(this.mSkillVars.Targets[index]) && (double) component.CalcDepth(((Component) this.mSkillVars.Targets[index]).transform.position) <= (double) distance + 0.5)
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
      if (pd != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) pd.mProjectile, (UnityEngine.Object) null))
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
        if (flag)
        {
          if (tacticsUnitController.mSkillVars.mSkillEffect.AreaEffects != null && tacticsUnitController.mSkillVars.mSkillEffect.AreaEffects.Effects != null && tacticsUnitController.mSkillVars.mSkillEffect.AreaEffects.Effects.Length > 0)
          {
            Quaternion quaternion;
            if (tacticsUnitController.mSkillVars.mSkillEffect.AreaEffects.SyncDirection)
            {
              Quaternion rotation = ((Component) tacticsUnitController).transform.rotation;
              quaternion = Quaternion.Euler(0.0f, ((Quaternion) ref rotation).eulerAngles.y, 0.0f);
            }
            else
              quaternion = Quaternion.identity;
            Quaternion rotation1 = quaternion;
            GameUtility.SpawnParticle(tacticsUnitController.mSkillVars.mSkillEffect.AreaEffects.Effects, tacticsUnitController.mSkillVars.mTargetPosition, rotation1, (GameObject) null);
          }
          else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) tacticsUnitController.mSkillVars.mSkillEffect.TargetHitEffect, (UnityEngine.Object) null))
            GameUtility.SpawnParticle(tacticsUnitController.mSkillVars.mSkillEffect.TargetHitEffect, tacticsUnitController.mSkillVars.mTargetPosition, Quaternion.identity, (GameObject) null);
        }
        switch (this.mSkillVars.mSkillEffect.MapHitEffectType)
        {
          case SkillEffect.MapHitEffectTypes.TargetRadial:
            for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
            {
              Vector3 vector3 = Vector3.op_Subtraction(((Component) this.mSkillVars.Targets[index]).transform.position, this.mSkillVars.mTargetPosition);
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
            Vector3 position1 = ((Component) tacticsUnitController).transform.position;
            position1.y = 0.0f;
            Vector3 forward = ((Component) tacticsUnitController).transform.forward;
            forward.y = 0.0f;
            ((Vector3) ref forward).Normalize();
            for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
            {
              Vector3 position2 = ((Component) this.mSkillVars.Targets[index]).transform.position;
              position2.y = 0.0f;
              float num = Mathf.Max(Vector3.Dot(Vector3.op_Subtraction(position2, position1), forward), 0.0f);
              this.mSkillVars.HitTimers.Add(new TacticsUnitController.HitTimer(this.mSkillVars.Targets[index], num * this.mSkillVars.mSkillEffect.MapHitEffectIntervals));
            }
            break;
          case SkillEffect.MapHitEffectTypes.InstigatorRadial:
            for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
            {
              Vector3 vector3 = Vector3.op_Subtraction(((Component) this.mSkillVars.Targets[index]).transform.position, ((Component) this).transform.position);
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
      return this.Unit.CastSkill != null && ECastTypes.Jump == this.Unit.CastSkill.CastType;
    }

    public bool IsCastJumpStartComplete() => this.mCastJumpStartComplete;

    public bool IsCastJumpFallComplete() => this.mCastJumpFallComplete;

    public void CastJump() => this.GotoState<TacticsUnitController.State_JumpCast>();

    public void CastJumpFall(bool is_play_damage_motion = false)
    {
      this.mIsPlayDamageMotion = is_play_damage_motion;
      this.GotoState<TacticsUnitController.State_JumpCastFall>();
    }

    public bool IsFinishedCastJumpFall() => this.mFinishedCastJumpFall;

    public void SetCastJump()
    {
      this.CollideGround = true;
      this.SetVisible(false);
      this.mCastJumpStartComplete = true;
      this.mCastJumpFallComplete = false;
      this.mFinishedCastJumpFall = false;
      Vector3 position = ((Component) this).transform.position;
      position.y += this.mCastJumpOffsetY;
      ((Component) this).transform.position = position;
    }

    public void HideGimmickForTargetGrid(TacticsUnitController target)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) target) || target.Unit != null && target.Unit.IsJump)
        return;
      SceneBattle instance = SceneBattle.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return;
      Grid unitGridPosition = instance.Battle.GetUnitGridPosition(target.Unit);
      Unit gimmickAtGrid = instance.Battle.FindGimmickAtGrid(unitGridPosition, true);
      TacticsUnitController unitController = instance.FindUnitController(gimmickAtGrid);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) || unitController.Unit.IsBreakObj)
        return;
      unitController.ScaleHide();
    }

    private void createEffect(TacticsUnitController target, GameObject go_effect)
    {
      GameObject go = UnityEngine.Object.Instantiate<GameObject>(go_effect, Vector3.zero, Quaternion.identity);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) go))
        return;
      go.transform.SetParent(((Component) target).transform, false);
      GameUtility.RequireComponent<OneShotParticle>(go);
    }

    private void createEffect(
      TacticsUnitController target,
      SkillEffect.EffectElement[] effects,
      bool set_rotation = false,
      float rotationY = 0.0f)
    {
      GameObject go = new GameObject();
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) go))
        return;
      DelayedEffectSpawner delayedEffectSpawner = go.RequireComponent<DelayedEffectSpawner>();
      Quaternion rotation = Quaternion.identity;
      if (set_rotation)
        rotation = Quaternion.Euler(0.0f, rotationY, 0.0f);
      delayedEffectSpawner.Init(effects, Vector3.zero, rotation);
      go.transform.SetParent(((Component) target).transform, false);
    }

    public List<TacticsUnitController> GetTargetTucLists()
    {
      return this.mSkillVars == null || this.mSkillVars.Targets == null ? new List<TacticsUnitController>() : this.mSkillVars.Targets;
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
      get => !this.isIdle || this.mKnockBackMode != TacticsUnitController.eKnockBackMode.IDLE;
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
        if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
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
              if (!this.IsVisible())
                break;
              this.createEffect(this, instance.KnockBackEffect);
              break;
            case TacticsUnitController.eKnockBackMode.EXEC:
              this.mKbPassedSec += Time.deltaTime;
              float num = this.mKbPassedSec / 0.4f;
              if ((double) num >= 1.0)
              {
                ((Component) this).transform.position = this.mKbPosEnd;
                this.HideGimmickForTargetGrid(this);
                this.mKnockBackMode = TacticsUnitController.eKnockBackMode.IDLE;
              }
              else
                ((Component) this).transform.position = Vector3.Lerp(this.mKbPosStart, this.mKbPosEnd, num * (2f - num));
              if (this.IsVisible())
                break;
              GameObject jumpSpotEffect = instance.GetJumpSpotEffect(this.Unit);
              if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) jumpSpotEffect))
                break;
              jumpSpotEffect.transform.position = ((Component) this).transform.position;
              Vector3 position = jumpSpotEffect.transform.position;
              position.y = GameUtility.RaycastGround(position).y;
              jumpSpotEffect.transform.position = position;
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
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
          return instance.CalcGridCenter(this.mSkillVars.mTeleportGrid.x, this.mSkillVars.mTeleportGrid.y);
      }
      return ((Component) this).transform.position;
    }

    public void SetStartPos(Vector3 pos)
    {
      if (this.mSkillVars == null)
        return;
      this.mSkillVars.mStartPosition = pos;
    }

    public void LookAtTarget()
    {
      if (this.mSkillVars == null || this.mSkillVars.Skill == null || !this.mSkillVars.Skill.IsTargetTeleport || this.mSkillVars.Targets == null || this.mSkillVars.Targets.Count == 0)
        return;
      TacticsUnitController target = this.mSkillVars.Targets[0];
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) target))
        return;
      this.LookAt(target.CenterPosition);
    }

    public void ReflectDispModel()
    {
      if (this.mUnit == null || !this.mUnit.IsBreakObj || !this.SetActivateUnitObject(this.Unit.GetMapBreakNowStage((int) this.Unit.CurrentStatus.param.hp)) || !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mBadStatusEffect) || this.mBadStatus == null || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBadStatus.Effect, (UnityEngine.Object) null))
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

    public SkillSequence GetSkillSequence()
    {
      return this.mSkillVars == null ? (SkillSequence) null : this.mSkillVars.mSkillSequence;
    }

    public void LoadTransformAnimation(SkillSequence ss)
    {
      if (ss == null)
        return;
      if (!string.IsNullOrEmpty(ss.SkillAnimation.Name))
      {
        string animationName = ss.SkillAnimation.Name + "_chg";
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.FindAnimation("B_TRANSFORM"), (UnityEngine.Object) null))
          this.LoadUnitAnimationAsync("B_TRANSFORM", animationName, false);
      }
      if (string.IsNullOrEmpty(ss.StartAnimation))
        return;
      string animationName1 = ss.StartAnimation + "_chg";
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.FindAnimation("B_PRE_TRANSFORM"), (UnityEngine.Object) null))
        return;
      this.LoadUnitAnimationAsync("B_PRE_TRANSFORM", animationName1, false);
    }

    private void PlayAfterTransform()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.FindAnimation("B_TRANSFORM"), (UnityEngine.Object) null))
        return;
      this.GotoState<TacticsUnitController.State_AfterTransform>();
    }

    public void PlayPreAfterTransform()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.FindAnimation("B_PRE_TRANSFORM"), (UnityEngine.Object) null))
        return;
      this.GotoState<TacticsUnitController.State_PreAfterTransform>();
    }

    public List<TacticsUnitController> GetSkillTargets()
    {
      return this.mSkillVars == null || this.mSkillVars.Targets == null ? new List<TacticsUnitController>() : this.mSkillVars.Targets;
    }

    public void LoadBigUnitBsAnimation()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.FindAnimation("B_BU_BS_IDLE"), (UnityEngine.Object) null))
        this.LoadUnitAnimationAsync("B_BU_BS_IDLE", "idleBattleScene0", true);
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.FindAnimation("B_BU_BS_DAMAGE"), (UnityEngine.Object) null))
        return;
      this.LoadUnitAnimationAsync("B_BU_BS_DAMAGE", "damageBattleScene0", true);
    }

    private void PlayBigUnitBsIdle()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.FindAnimation("B_BU_BS_IDLE"), (UnityEngine.Object) null))
        return;
      this.GotoState<TacticsUnitController.State_BigUnitBsIdle>();
    }

    private void PlayBigUnitBsDamage()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.FindAnimation("B_BU_BS_DAMAGE"), (UnityEngine.Object) null))
        return;
      this.GotoState<TacticsUnitController.State_BigUnitBsDamage>();
    }

    public void DirectionOff_LoadSkill(
      SkillParam skillParam,
      SkillMotion skill_motion,
      bool is_cs = false,
      bool is_cs_sub = false)
    {
      this.mSkillVars = new TacticsUnitController.SkillVars();
      this.mSkillVars.Skill = skillParam;
      this.mSkillVars.mIsCollaboSkill = is_cs;
      this.mSkillVars.mIsCollaboSkillSub = is_cs_sub;
      this.mSkillVars.mSkillMotion = skill_motion;
      this.AddLoadThreadCount();
      this.StartCoroutine(this.LoadSkillSequenceAsync(skillParam, false));
      if (string.IsNullOrEmpty(skill_motion.EffectId))
        return;
      this.LoadSkillEffect(skill_motion.EffectId);
    }

    public void DirectionOff_EndSkill()
    {
      this.SkillEffectSelf();
      this.FinishSkill();
    }

    public void DirectionOff_StartSkill(
      Vector3 targetPosition,
      Camera activeCamera,
      TacticsUnitController[] targets,
      Vector3[] hitGrids,
      SkillParam skill)
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
            if (this.mSkillVars.NumHitsLeft == 0 && target.mKnockBackGrid != null)
              target.mKnockBackMode = TacticsUnitController.eKnockBackMode.START;
            if (this.mSkillVars.mSkillEffect.AlwaysExplode && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Changing && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Throw)
              this.SpawnHitEffect(this.mSkillVars.Targets[index1], this.mSkillVars.NumHitsLeft == 0);
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
          float num1 = 0.0f;
          if (this.mSkillVars.Targets != null)
          {
            for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
            {
              TacticsUnitController target = this.mSkillVars.Targets[index];
              if (!target.ShouldDodgeHits)
              {
                int num2 = this.mSkillVars.TotalHits <= 1 ? target.mHitInfo.GetTotalHpDamage() : target.mHitInfo.GetTotalHpDamage() / this.mSkillVars.TotalHits;
                num1 = Mathf.Max((int) this.Unit.MaximumStatus.param.hp == 0 ? 1f : 1f * (float) num2 / (float) (int) this.Unit.MaximumStatus.param.hp, num1);
              }
            }
          }
          if ((double) num1 >= 0.75)
            cueID = "battle_0033";
          if ((double) num1 > 0.5)
            cueID = "battle_0032";
        }
        this.Unit.PlayBattleVoice(cueID);
      }
      return this.mSkillVars.NumHitsLeft > 0;
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
      private Vector3 mBaseScale;
      private TacticsUnitController mGimmickController;
      public bool IsHide;
      public bool Enable;
      private float mWait = 1f;
      private float mCurrentWait;
      private float mSpeed = 5f;

      public void Init(TacticsUnitController GimmickController)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) GimmickController, (UnityEngine.Object) null) || GimmickController.Unit == null || !GimmickController.Unit.IsGimmick || GimmickController.mUnit.IsBreakObj)
          return;
        this.mGimmickController = GimmickController;
        this.mBaseScale = ((Component) this.mGimmickController).transform.localScale;
        this.mCurrentWait = this.mWait;
        this.Enable = false;
        this.IsHide = true;
      }

      public void ResetScale()
      {
        this.mCurrentWait = this.mWait;
        this.Enable = false;
        this.IsHide = true;
        ((Component) this.mGimmickController).transform.localScale = this.mBaseScale;
      }

      public void Update()
      {
        if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mGimmickController) || !this.Enable)
          return;
        if (this.IsHide)
        {
          if (0.0 < (double) this.mCurrentWait)
          {
            this.mCurrentWait -= this.mSpeed * Time.deltaTime;
            ((Component) this.mGimmickController).transform.localScale = Vector3.op_Multiply(this.mBaseScale, this.mCurrentWait / this.mWait);
            return;
          }
          ((Component) this.mGimmickController).transform.localScale = Vector3.zero;
          this.mCurrentWait = 0.0f;
        }
        else
        {
          if ((double) this.mWait > (double) this.mCurrentWait)
          {
            this.mCurrentWait += this.mSpeed * Time.deltaTime;
            ((Component) this.mGimmickController).transform.localScale = Vector3.op_Multiply(this.mBaseScale, this.mCurrentWait / this.mWait);
            return;
          }
          ((Component) this.mGimmickController).transform.localScale = this.mBaseScale;
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
          self.LoadUnitAnimationAsync("RUN", TacticsUnitController.ANIM_RUN_FIELD, true);
          self.LoadUnitAnimationAsync("STEP", TacticsUnitController.ANIM_STEP, self.mCharacterData.IsJobStep);
          self.LoadUnitAnimationAsync("FLLP", TacticsUnitController.ANIM_FALL_LOOP, self.mCharacterData.IsJobFallloop);
          self.LoadUnitAnimationAsync("CLMB", TacticsUnitController.ANIM_CLIMBUP, self.mCharacterData.IsJobJumploop);
        }
        self.PlayIdle();
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
        if (self.mBadStatus != null && !string.IsNullOrEmpty(self.mBadStatus.AnimationName) && UnityEngine.Object.op_Inequality((UnityEngine.Object) self.FindAnimation("BAD"), (UnityEngine.Object) null) && self.mBadStatus.AnimationName == self.mLoadedBadStatusAnimation)
          id = "BAD";
        if ((double) self.GetTargetWeight(id) >= 1.0)
          return;
        self.PlayAnimation(id, true, self.mIdleInterpTime);
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
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.FindAnimation("BAD")))
            {
              self.UnloadAnimation("BAD");
              self.StopAnimation("BAD");
              self.mLoadedBadStatusAnimation = (string) null;
            }
          }
          else if (string.IsNullOrEmpty(self.mLoadedBadStatusAnimation))
          {
            bool addJobName = false;
            if (self.mCharacterData != null)
            {
              if (animationName == TacticsUnitController.ANIM_GENKIDAMA)
                addJobName = self.mCharacterData.IsJobFreeze;
              else if (animationName == TacticsUnitController.ANIM_GROGGY)
                addJobName = self.mCharacterData.IsJobGroggy;
            }
            self.LoadUnitAnimationAsync("BAD", animationName, addJobName);
            self.mLoadedBadStatusAnimation = animationName;
          }
          else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.FindAnimation("BAD"), (UnityEngine.Object) null) && (double) self.GetTargetWeight("BAD") < 1.0)
            self.PlayAnimation("BAD", true, 0.25f);
        }
        else if (!string.IsNullOrEmpty(self.mLoadedBadStatusAnimation) && UnityEngine.Object.op_Implicit((UnityEngine.Object) self.FindAnimation("BAD")))
        {
          self.UnloadAnimation("BAD");
          self.mLoadedBadStatusAnimation = (string) null;
          self.PlayAnimation("IDLE", true, 0.25f);
        }
        if (!self.AutoUpdateRotation)
          return;
        self.UpdateRotation();
      }
    }

    private class State_LookAt : TacticsUnitController.State
    {
      private float mTime;
      private float mSpinTime = 0.25f;
      private float mSpinCount = 2f;
      private Transform mTransform;
      private Vector3 mStartPosition;
      private Quaternion mEndRotation;
      private float mJumpHeight = 0.2f;

      public override void Begin(TacticsUnitController self)
      {
        this.mSpinCount = self.mSpinCount;
        this.mSpinTime = this.mSpinCount * 0.125f;
        this.mJumpHeight = this.mSpinCount * 0.1f;
        this.mTransform = ((Component) self).transform;
        Vector3 vector3 = Vector3.op_Subtraction(self.mLookAtTarget, this.mTransform.position);
        vector3.y = 0.0f;
        this.mStartPosition = this.mTransform.position;
        this.mEndRotation = Quaternion.LookRotation(vector3);
      }

      public override void Update(TacticsUnitController self)
      {
        this.mTime += Time.deltaTime;
        float num = Mathf.Clamp01(this.mTime / this.mSpinTime);
        this.mTransform.rotation = Quaternion.op_Multiply(Quaternion.AngleAxis((float) ((1.0 - (double) num) * (double) this.mSpinCount * 360.0), Vector3.up), this.mEndRotation);
        Vector3 mStartPosition = this.mStartPosition;
        mStartPosition.y += Mathf.Sin(num * 3.14159274f) * this.mJumpHeight;
        this.mTransform.position = mStartPosition;
        if ((double) num < 1.0)
          return;
        self.PlayIdle();
      }
    }

    private delegate void FieldActionEndEvent();

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
        this.mStartPosition = ((Component) self).transform.position;
        self.PlayAnimation("CLMB", true, 0.1f);
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
        float num2 = instance.Unit_JumpZCurve.Evaluate(num1);
        float num3 = instance.Unit_JumpYCurve.Evaluate(num1);
        zero.x = Mathf.Lerp(this.mStartPosition.x, this.mEndPosition.x, num2);
        zero.z = Mathf.Lerp(this.mStartPosition.z, this.mEndPosition.z, num2);
        zero.y = Mathf.Lerp(this.mStartPosition.y, this.mEndPosition.y, num1) + num3;
        ((Component) self).transform.position = zero;
        if (!this.mFalling && (double) num3 < (double) this.mLastY)
        {
          self.PlayAnimation("FLLP", true, 0.1f);
          this.mFalling = true;
        }
        this.mLastY = num3;
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
        this.mStartPosition = ((Component) self).transform.position;
        this.mEndPosition = GameUtility.RaycastGround(self.mFieldActionPoint);
        float num = this.mEndPosition.y - this.mStartPosition.y;
        this.mJumpHeight = Mathf.Abs(num);
        if ((double) num < 0.0)
          this.mJumpHeight *= 0.5f;
        this.mAnimRate = (float) (1.0 / ((double) GameUtility.CalcDistance2D(this.mStartPosition, this.mEndPosition) / (double) GameSettings.Instance.Quest.MapRunSpeedMax));
        ((Component) self).transform.rotation = self.mFieldActionDir.ToRotation();
        self.mCollideGround = false;
      }

      public override void Update(TacticsUnitController self)
      {
        this.mTime = Mathf.Clamp01(this.mTime + this.mAnimRate * Time.deltaTime);
        Vector3 vector3 = Vector3.Lerp(this.mStartPosition, this.mEndPosition, this.mTime);
        vector3.y += Mathf.Sin(this.mTime * 3.14159274f) * this.mJumpHeight;
        if ((double) this.mTime >= 1.0)
        {
          ((Component) self).transform.position = this.mEndPosition;
          self.mOnFieldActionEnd();
        }
        else
          ((Component) self).transform.position = vector3;
      }

      public override void End(TacticsUnitController self) => self.mCollideGround = true;
    }

    private class State_FieldActionFall : TacticsUnitController.State_FieldAction
    {
      private Vector3 mStartPosition;
      private Vector3 mEndPosition;
      private float mTime;
      private float mDuration = 0.5f;

      public override void Begin(TacticsUnitController self)
      {
        this.mStartPosition = ((Component) self).transform.position;
        this.mEndPosition = GameUtility.RaycastGround(self.mFieldActionPoint);
        GameSettings instance = GameSettings.Instance;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
          this.mDuration = instance.Unit_FallMinTime + Mathf.Abs(this.mStartPosition.y - this.mEndPosition.y) * instance.Unit_FallTimePerHeight;
        ((Component) self).transform.rotation = self.mFieldActionDir.ToRotation();
        self.mCollideGround = false;
        self.PlayAnimation("FLLP", true, 0.1f);
      }

      public override void Update(TacticsUnitController self)
      {
        this.mTime += Time.deltaTime;
        float num = Mathf.Clamp01(this.mTime / this.mDuration);
        Vector3 vector3 = Vector3.Lerp(this.mStartPosition, this.mEndPosition, num);
        vector3.y = Mathf.Lerp(this.mStartPosition.y, this.mEndPosition.y, 1f - Mathf.Cos((float) ((double) num * 3.1415927410125732 * 0.5)));
        if ((double) num >= 1.0)
        {
          ((Component) self).transform.position = this.mEndPosition;
          self.mOnFieldActionEnd();
        }
        else
          ((Component) self).transform.position = vector3;
      }

      public override void End(TacticsUnitController self) => self.mCollideGround = true;
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
          self.PlayIdle();
        }
        else
        {
          self.mStepStart = Vector3.Lerp(self.mStepStart, self.mStepEnd, Time.deltaTime * 10f);
          Vector3 vector3 = Vector3.op_Subtraction(self.mStepStart, self.mStepEnd);
          if ((double) ((Vector3) ref vector3).magnitude < 0.0099999997764825821)
          {
            ((Component) self).transform.position = self.mStepEnd;
            self.PlayIdle(0.1f);
          }
          else
            ((Component) self).transform.position = self.mStepStart;
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
          if (animation.events[index] is Marker)
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
          self.PlayIdle();
        }
        else
        {
          float remainingTime = self.GetRemainingTime("STEP");
          float num = Mathf.Clamp01((self.GetAnimation("STEP").Length - remainingTime) / this.mLandTime);
          Vector3 vector3 = Vector3.Lerp(self.mStepStart, self.mStepEnd, num);
          ((Component) self).transform.position = vector3;
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
          self.PlayAnimation(self.mRunAnimation, true, self.mMoveAnimInterpTime);
          this.LookToward(self, Vector3.op_Subtraction(self.mRoute[self.mRoutePos], ((Component) self).transform.position));
          this.mStartPos = self.mRoute[self.mRoutePos];
        }
        else
          this.mStartPos = ((Component) self).transform.position;
        if ((double) self.mPostMoveAngle < 0.0)
          return;
        this.mAdjustDirection = true;
        this.mDesiredRotation = Quaternion.AngleAxis(self.mPostMoveAngle, Vector3.up);
        this.mRotationRate = 720f;
      }

      public override void Update(TacticsUnitController self)
      {
        Transform transform = ((Component) self).transform;
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
          self.PlayIdle();
        }
        else
        {
          Vector3 vector3_1 = Vector3.op_Subtraction(self.mRoute[self.mRoutePos], this.mStartPos);
          vector3_1.y = 0.0f;
          float sqrMagnitude = ((Vector3) ref vector3_1).sqrMagnitude;
          if ((double) sqrMagnitude <= 9.9999997473787516E-05)
          {
            transform.position = self.mRoute[self.mRoutePos];
            ++self.mRoutePos;
            if (this.mAdjustDirection || self.mRoute.Length > self.mRoutePos)
              return;
            self.PlayIdle();
          }
          else
          {
            if ((double) sqrMagnitude > 9.9999997473787516E-05)
            {
              Vector3 vector3_2 = vector3_1;
              vector3_2.y = 0.0f;
              ((Vector3) ref vector3_2).Normalize();
              if (self.TriggerFieldAction(vector3_2, true))
              {
                self.mMoveAnimInterpTime = 0.1f;
                return;
              }
              Vector3 a = Vector3.op_Addition(((Component) self).transform.position, Vector3.op_Multiply(Vector3.op_Multiply(vector3_2, self.mRunSpeed), Time.deltaTime));
              ((Component) self).transform.position = a;
              float num1 = GameUtility.CalcDistance2D(a, self.mRoute[self.mRoutePos]);
              if ((double) num1 < 0.5 && self.mRoutePos < self.mRoute.Length - 1)
              {
                Vector3 vector3_3 = Vector3.op_Subtraction(self.mRoute[self.mRoutePos + 1], self.mRoute[self.mRoutePos]);
                vector3_3.y = 0.0f;
                ((Vector3) ref vector3_3).Normalize();
                float num2 = (float) ((1.0 - (double) num1 / 0.5) * 0.5);
                ((Component) self).transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(vector3_2), Quaternion.LookRotation(vector3_3), num2);
              }
              else if (self.mRoutePos > 1)
              {
                float num3 = GameUtility.CalcDistance2D(a, self.mRoute[self.mRoutePos - 1]);
                if ((double) num3 < 0.5)
                {
                  Vector3 vector3_4 = Vector3.op_Subtraction(self.mRoute[self.mRoutePos - 1], self.mRoute[self.mRoutePos - 2]);
                  vector3_4.y = 0.0f;
                  ((Vector3) ref vector3_4).Normalize();
                  float num4 = (float) (0.5 + (double) num3 / 0.5 * 0.5);
                  ((Component) self).transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(vector3_4), Quaternion.LookRotation(vector3_2), num4);
                }
                else
                  this.LookToward(self, vector3_2);
              }
              else
                this.LookToward(self, vector3_2);
            }
            Vector3 vector3_5 = Vector3.op_Subtraction(transform.position, this.mStartPos);
            vector3_5.y = 0.0f;
            if ((double) Vector3.Dot(vector3_1, vector3_5) / (double) sqrMagnitude < 0.99989998340606689)
              return;
            this.mStartPos = self.mRoute[self.mRoutePos];
            ((Component) self).transform.position = self.mRoute[self.mRoutePos];
            ++self.mRoutePos;
            if (this.mAdjustDirection || self.mRoute.Length > self.mRoutePos)
              return;
            self.PlayIdle();
          }
        }
      }

      private void LookToward(TacticsUnitController self, Vector3 v)
      {
        v.y = 0.0f;
        if ((double) ((Vector3) ref v).magnitude <= 9.9999997473787516E-05)
          return;
        ((Component) self).transform.rotation = Quaternion.LookRotation(v);
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
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCharacterSettings, (UnityEngine.Object) null))
          this.mTopPos.y += self.mCharacterSettings.Height * ((Component) self).transform.lossyScale.y;
        MapPickup componentInChildren = self.mPickupObject.GetComponentInChildren<MapPickup>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren.Shadow, (UnityEngine.Object) null))
          return;
        ((Component) componentInChildren.Shadow).gameObject.SetActive(false);
      }

      public override void Update(TacticsUnitController self)
      {
        Vector3 vector3 = Vector3.Lerp(this.mObjectStartPos, this.mTopPos, Mathf.Sin((float) ((double) self.GetNormalizedTime("PICK") * 3.1415927410125732 * 0.5)));
        Transform transform = self.mPickupObject.transform;
        transform.rotation = Quaternion.LookRotation(Vector3.back);
        transform.position = vector3;
        if ((double) self.GetRemainingTime("PICK") <= 0.0)
        {
          if (!this.mPickedUp)
          {
            MapPickup componentInChildren = self.mPickupObject.GetComponentInChildren<MapPickup>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null) && componentInChildren.OnPickup != null)
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
        self.PlayIdle();
      }

      public override void End(TacticsUnitController self)
      {
        self.mPickupObject = (GameObject) null;
        self.UnloadPickupAnimation();
      }
    }

    private class State_Taken : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self) => self.PlayAnimation("PICK", false);
    }

    private struct CameraState
    {
      public Vector3 Position;
      public Quaternion Rotation;
      public static TacticsUnitController.CameraState Default;

      public void CacheCurrent(Camera camera)
      {
        Transform transform = ((Component) camera).transform;
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
      public List<GameObject> mAuras = new List<GameObject>(4);
      public Camera mActiveCamera;
      public StatusParam Param;
      public int mCameraID;
      public int mChantCameraID;
      public int mSkillCameraID;
      public Quaternion mCameraShakeOffset = Quaternion.identity;
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
      public List<TacticsUnitController.ProjectileData> mProjectileDataLists = new List<TacticsUnitController.ProjectileData>();
      public int mNumShotCount;
      public bool mProjectileTriggered;
      public SkillEffect mSkillEffect;
      public List<TacticsUnitController.OutputPoint> mOutputPoints = new List<TacticsUnitController.OutputPoint>(32);
      public bool UseBattleScene;
      public Vector3[] HitGrids;
      public List<TacticsUnitController.HitTimer> HitTimers = new List<TacticsUnitController.HitTimer>();
      public Coroutine HitTimerThread;
      public List<TacticsUnitController> HitTargets = new List<TacticsUnitController>(4);
      public Grid mLandingGrid;
      public Grid mTeleportGrid;
      public bool mIsFinishedBuffEffectTarget;
      public bool mIsFinishedBuffEffectSelf;
      public SkillMotion mSkillMotion;
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
        this.Target.is_efficacy = false;
        this.Target.is_ignore_efficacy = false;
        this.Dirty = false;
      }
    }

    private class ShakeUnit
    {
      private Vector3 mShakeBasePos;
      private Vector3 mShakeOffset;
      private float mShakeSpeed = 0.0125f;
      private int mShakeMaxCount = 8;
      private int mShakeCount = 8;
      private bool mIsShakeStart;

      public bool ShakeStart
      {
        set => this.mIsShakeStart = value;
        get => this.mIsShakeStart;
      }

      public int ShakeMaxCount
      {
        set
        {
        }
        get => this.mShakeMaxCount;
      }

      public int ShakeCount
      {
        set
        {
        }
        get => this.mShakeCount;
      }

      public void Init(Vector3 basePosition, Vector3 direction)
      {
        GameSettings instance = GameSettings.Instance;
        this.mShakeSpeed = instance.ShakeUnit_Offset;
        this.mShakeMaxCount = instance.ShakeUnit_MaxCount;
        this.mShakeBasePos = basePosition;
        Vector3 vector3 = Vector3.Cross(direction, new Vector3(0.0f, 1f, 0.0f));
        this.mShakeOffset = ((Vector3) ref vector3).normalized;
        TacticsUnitController.ShakeUnit shakeUnit = this;
        shakeUnit.mShakeOffset = Vector3.op_Multiply(shakeUnit.mShakeOffset, this.mShakeSpeed);
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
            this.mShakeOffset = Vector3.op_UnaryNegation(this.mShakeOffset);
          --this.mShakeCount;
          return Vector3.op_Addition(this.mShakeBasePos, this.mShakeOffset);
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
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_6050");
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) self.GetRemainingTime("B_DEAD") > 0.0)
          return;
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) self).gameObject);
        if (self.mUnit != null && self.mUnit.IsBreakObj)
          return;
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_6051");
      }
    }

    private delegate void ProjectileStopEvent(TacticsUnitController.ProjectileData pd);

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
      public override void Begin(TacticsUnitController self) => self.PlayAnimation("B_DOWN", true);

      public override void End(TacticsUnitController self)
      {
      }
    }

    private class State_Dodge : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.Unit.PlayBattleVoice("battle_0017");
        self.PlayAnimation("B_DGE", false, 0.1f);
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) self.GetRemainingTime("B_DGE") > 0.10000000149011612)
          return;
        self.PlayIdle(0.1f);
      }
    }

    private class State_NormalDamage : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("B_DMG0", false, 0.1f);
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) self.GetRemainingTime("B_DMG0") > 0.10000000149011612)
          return;
        self.PlayIdle(0.1f);
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
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillVars.mSkillEffect, (UnityEngine.Object) null) || self.mSkillVars.mSkillEffect.ChantSound == null)
          return;
        self.mSkillVars.mSkillEffect.ChantSound.Play();
      }

      public override void Update(TacticsUnitController self)
      {
        self.mSkillVars.mChantCameraID = self.mSkillVars.mCameraID;
        float normalizedTime = self.GetNormalizedTime("B_CHA");
        if (self.mSkillVars.UseBattleScene && !self.mSkillVars.mIsCollaboSkillSub && UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillVars.mSkillChantCameraClip, (UnityEngine.Object) null))
        {
          if (self.mSkillVars.mSkillSequence.InterpChantCamera)
            self.AnimateCameraInterpolated(self.mSkillVars.mSkillChantCameraClip, normalizedTime, 4f * normalizedTime, this.mStartCameraState, self.mSkillVars.mChantCameraID);
          else
            self.AnimateCamera(self.mSkillVars.mSkillChantCameraClip, normalizedTime, self.mSkillVars.mChantCameraID);
        }
        if ((double) normalizedTime < 1.0)
          return;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillVars.mChantEffect, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillVars.mChantEffect.GetComponentInChildren<ParticleSystem>(), (UnityEngine.Object) null))
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
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.FindAnimation("B_SKL")))
          self.PlayAnimation("B_SKL", false);
        if (self.mSkillVars.mSkillEffect.StopAura == SkillEffect.AuraStopTimings.BeforeHit)
          self.StopAura();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillVars.mTargetController, (UnityEngine.Object) null))
          this.mTargetRotation = ((Component) self.mSkillVars.mTargetController).transform.rotation;
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
          if (self.mSkillVars.mSkillAnimation.CurveNames.Contains("Enm_Distance_dummy") && UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillVars.mTargetController, (UnityEngine.Object) null))
          {
            Vector3 pos;
            Quaternion rotation;
            self.CalcEnemyPos(self.mSkillVars.mSkillAnimation.animation, normalizedTime, out pos, out rotation);
            Transform transform = ((Component) self.mSkillVars.mTargetController).transform;
            transform.position = pos;
            transform.rotation = Quaternion.op_Multiply(rotation, this.mTargetRotation);
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillVars.mSkillCameraClip, (UnityEngine.Object) null) && !self.mSkillVars.mIsCollaboSkillSub)
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
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) self.FindAnimation("B_BS"), (UnityEngine.Object) null))
          return;
        self.PlayAnimation("B_BS", false, 0.1f);
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
          self.OnProjectileHit();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) self.FindAnimation("B_BS"), (UnityEngine.Object) null))
          return;
        self.PlayAnimation("B_BS", false, 0.1f);
      }

      public override void End(TacticsUnitController self)
      {
        self.mSkillVars.mTargetController = (TacticsUnitController) null;
      }

      private void OnHit(TacticsUnitController.ProjectileData pd) => this.self.OnProjectileHit(pd);

      public override void Update(TacticsUnitController self)
      {
        this.mStateTime += Time.deltaTime;
        if (self.mSkillVars.UseBattleScene)
        {
          foreach (TacticsUnitController.ProjectileData projectileDataList in self.mSkillVars.mProjectileDataLists)
          {
            if (!projectileDataList.mProjStartAnimEnded && UnityEngine.Object.op_Inequality((UnityEngine.Object) projectileDataList.mProjectile, (UnityEngine.Object) null))
            {
              if (projectileDataList.mProjectileThread != null)
                return;
              projectileDataList.mProjStartAnimEnded = true;
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillVars.mTargetController, (UnityEngine.Object) null))
              {
                projectileDataList.mProjectileThread = self.StartCoroutine(self.AnimateProjectile(self.mSkillVars.mSkillEffect.ProjectileEnd, self.mSkillVars.mSkillEffect.ProjectileEndTime, self.mSkillVars.mTargetControllerPosition, Quaternion.identity, new TacticsUnitController.ProjectileStopEvent(this.OnHit), projectileDataList));
                ParticleSystem component = projectileDataList.mProjectile.GetComponent<ParticleSystem>();
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                  component.Clear(true);
              }
            }
          }
          if (!self.mSkillVars.mIsCollaboSkillSub)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillVars.mSkillEndCameraClip, (UnityEngine.Object) null))
            {
              Vector3 pos;
              Quaternion rotation;
              self.CalcCameraPos(self.mSkillVars.mSkillEndCameraClip, Mathf.Clamp01(this.mStateTime / this.mAnimationLength), 0, out pos, out rotation);
              self.SetActiveCameraPosition(Vector3.op_Addition(self.mSkillVars.mTargetControllerPosition, pos), Quaternion.op_Multiply(self.mSkillVars.mCameraShakeOffset, rotation));
            }
            else
            {
              Transform battleCamera = GameSettings.Instance.Quest.BattleCamera;
              Vector3 position = battleCamera.position;
              Quaternion rotation = battleCamera.rotation;
              self.SetActiveCameraPosition(Vector3.op_Addition(self.mSkillVars.mTargetControllerPosition, position), Quaternion.op_Multiply(self.mSkillVars.mCameraShakeOffset, rotation));
            }
          }
        }
        if (!this.mUnitAnimationEnded && (double) self.GetRemainingTime("B_BS") <= 0.0)
        {
          if (self.mSkillVars.UseBattleScene)
            self.PlayAnimation("IDLE", true, 0.1f);
          this.mUnitAnimationEnded = true;
        }
        foreach (TacticsUnitController.ProjectileData projectileDataList in self.mSkillVars.mProjectileDataLists)
        {
          if (projectileDataList.mProjectileThread != null)
            return;
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
          foreach (TacticsUnitController.ProjectileData projectileDataList in self.mSkillVars.mProjectileDataLists)
          {
            if (!projectileDataList.mProjectileHitsTarget)
            {
              flag = false;
              break;
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
        if (self.mSkillVars.Skill.IsTransformSkill())
          self.SetVisible(false);
        self.PlayIdle();
      }
    }

    private class State_JumpCast : TacticsUnitController.State
    {
      private float mBasePosY;
      private float mCastTime = 0.5f;
      private float mElapsed;

      public override void Begin(TacticsUnitController self)
      {
        this.mBasePosY = ((Component) self).transform.position.y;
        this.mElapsed = 0.0f;
        self.CollideGround = false;
        self.mCastJumpStartComplete = false;
        self.mCastJumpFallComplete = false;
        self.PlayAnimation("CLMB", false, this.mCastTime / 10f);
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_7036");
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) this.mCastTime <= (double) this.mElapsed)
        {
          self.PlayIdle();
        }
        else
        {
          this.mElapsed += Time.deltaTime;
          if ((double) this.mCastTime < (double) this.mElapsed)
            this.mElapsed = this.mCastTime;
          Vector3 position = ((Component) self).transform.position;
          position.y = this.mBasePosY + self.mCastJumpOffsetY * (this.mElapsed / this.mCastTime);
          ((Component) self).transform.position = position;
        }
      }

      public override void End(TacticsUnitController self)
      {
        self.CollideGround = true;
        self.SetVisible(false);
        self.mCastJumpStartComplete = true;
      }
    }

    private class State_JumpCastFall : TacticsUnitController.State
    {
      private float mBasePosY;
      private float mCastTime = 0.5f;
      private float mElapsed;
      private TacticsUnitController.State_JumpCastFall.eMotionMode mMotionMode;

      public override void Begin(TacticsUnitController self)
      {
        this.mBasePosY = ((Component) self).transform.position.y;
        this.mElapsed = 0.0f;
        self.CollideGround = false;
        self.mFinishedCastJumpFall = false;
        self.PlayAnimation("IDLE", false);
        Vector3 position = ((Component) self).transform.position;
        position.y += self.mCastJumpOffsetY;
        ((Component) self).transform.position = position;
        self.SetVisible(true);
        this.mMotionMode = TacticsUnitController.State_JumpCastFall.eMotionMode.Fall;
      }

      public override void Update(TacticsUnitController self)
      {
        switch (this.mMotionMode)
        {
          case TacticsUnitController.State_JumpCastFall.eMotionMode.Fall:
            if ((double) this.mElapsed >= (double) this.mCastTime)
            {
              SceneBattle instance = SceneBattle.Instance;
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
              {
                self.createEffect(self, instance.JumpFallEffect);
                instance.OnGimmickUpdate();
              }
              if (self.mIsPlayDamageMotion)
              {
                self.PlayAnimation("B_DMG0", false, 0.1f);
                this.mMotionMode = TacticsUnitController.State_JumpCastFall.eMotionMode.Damage;
                break;
              }
              self.PlayIdle();
              break;
            }
            this.mElapsed += Time.deltaTime;
            if ((double) this.mElapsed > (double) this.mCastTime)
              this.mElapsed = this.mCastTime;
            Vector3 position = ((Component) self).transform.position;
            position.y = this.mBasePosY + self.mCastJumpOffsetY * ((this.mCastTime - this.mElapsed) / this.mCastTime);
            ((Component) self).transform.position = position;
            break;
          case TacticsUnitController.State_JumpCastFall.eMotionMode.Damage:
            if ((double) self.GetRemainingTime("B_DMG0") > 0.10000000149011612)
              break;
            self.PlayIdle(0.1f);
            break;
        }
      }

      public override void End(TacticsUnitController self)
      {
        self.CollideGround = true;
        self.mFinishedCastJumpFall = true;
      }

      private enum eMotionMode
      {
        Fall,
        Damage,
      }
    }

    private class State_JumpCastComplete : TacticsUnitController.State
    {
      private TacticsUnitController.State_JumpCastComplete.MotionMode Mode;
      private GameObject mFallEffect;
      private GameObject mHitEffect;
      private Vector3 mBasePos;
      private IntVector2 mBaseMapPos;
      private float mCastTime = 0.3f;
      private float mElapsed;
      private TacticsUnitController[] mExcludes;
      private static readonly Color SceneFadeColor = new Color(0.2f, 0.2f, 0.2f, 1f);
      private float mFallStartWait = 0.7f;
      private float TransStartTime;
      private float TransWaitTime = 0.1f;
      private bool beforeVisible = true;
      private float ReturnTime = 0.3f;
      private bool isDirect;

      public override void Begin(TacticsUnitController self)
      {
        this.mBasePos = ((Component) self).transform.position;
        this.mBaseMapPos = new IntVector2(self.Unit.x, self.Unit.y);
        if (self.mSkillVars.mLandingGrid != null)
          this.mBaseMapPos = new IntVector2(self.mSkillVars.mLandingGrid.x, self.mSkillVars.mLandingGrid.y);
        this.mElapsed = 0.0f;
        self.mCastJumpFallComplete = false;
        self.CollideGround = false;
        this.Mode = TacticsUnitController.State_JumpCastComplete.MotionMode.FALL_WAIT;
        self.AnimateVessel(1f, 0.0f);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
        {
          SceneBattle instance = SceneBattle.Instance;
          this.mExcludes = instance.GetActiveUnits();
          Array.Resize<TacticsUnitController>(ref this.mExcludes, this.mExcludes.Length + 1);
          this.mExcludes[this.mExcludes.Length - 1] = self;
          FadeController.Instance.BeginSceneFade(TacticsUnitController.State_JumpCastComplete.SceneFadeColor, 0.5f, this.mExcludes, (TacticsUnitController[]) null);
          this.mBasePos = instance.CalcGridCenter(this.mBaseMapPos.x, this.mBaseMapPos.y);
          self.mSkillVars.mStartPosition = this.mBasePos;
        }
        self.PlayAnimation("FLLP", true, 0.0f);
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
        if (this.self.mSkillVars != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mSkillVars.mSkillEffect.AuraEffect, (UnityEngine.Object) null))
        {
          this.mFallEffect = UnityEngine.Object.Instantiate<GameObject>(this.self.mSkillVars.mSkillEffect.AuraEffect, ((Component) this.self).transform.position, ((Component) this.self).transform.rotation);
          this.mFallEffect.transform.SetParent(((Component) this.self).transform);
        }
        Vector3 position = ((Component) this.self).transform.position;
        position.y = this.self.mCastJumpOffsetY;
        ((Component) this.self).transform.position = position;
        this.Mode = TacticsUnitController.State_JumpCastComplete.MotionMode.FALL;
        this.mElapsed = 0.0f;
      }

      private void FallUpdate(TacticsUnitController self)
      {
        if ((double) this.mElapsed >= (double) this.mCastTime)
        {
          if (self.mSkillVars != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillVars.mSkillEffect, (UnityEngine.Object) null))
          {
            if (self.mSkillVars.mSkillEffect.AreaEffects != null && self.mSkillVars.mSkillEffect.AreaEffects.Effects != null && self.mSkillVars.mSkillEffect.AreaEffects.Effects.Length > 0)
            {
              Quaternion quaternion;
              if (self.mSkillVars.mSkillEffect.AreaEffects.SyncDirection)
              {
                Quaternion rotation = ((Component) self).transform.rotation;
                quaternion = Quaternion.Euler(0.0f, ((Quaternion) ref rotation).eulerAngles.y, 0.0f);
              }
              else
                quaternion = Quaternion.identity;
              Quaternion rotation1 = quaternion;
              this.mHitEffect = GameUtility.SpawnParticle(self.mSkillVars.mSkillEffect.AreaEffects.Effects, self.JumpFallPos, rotation1, (GameObject) null);
              this.mHitEffect.SetActive(false);
            }
            else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillVars.mSkillEffect.TargetHitEffect, (UnityEngine.Object) null))
              this.mHitEffect = UnityEngine.Object.Instantiate<GameObject>(self.mSkillVars.mSkillEffect.TargetHitEffect, self.JumpFallPos, Quaternion.identity);
          }
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
          ((Component) self).transform.position = jumpFallPos;
        }
      }

      private void EnterReturn()
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mFallEffect, (UnityEngine.Object) null))
        {
          GameUtility.RequireComponent<OneShotParticle>(this.mFallEffect);
          GameUtility.StopEmitters(this.mFallEffect);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mHitEffect, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mHitEffect.GetComponent<DelayedEffectSpawner>(), (UnityEngine.Object) null))
            this.mHitEffect.SetActive(true);
          else
            GameUtility.RequireComponent<OneShotParticle>(this.mHitEffect);
        }
        for (int index = 0; index < this.self.mSkillVars.Targets.Count; ++index)
        {
          Vector3 vector3 = Vector3.op_Subtraction(((Component) this.self.mSkillVars.Targets[index]).transform.position, this.self.mSkillVars.mTargetPosition);
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
          SceneBattle instance = SceneBattle.Instance;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
            instance.OnGimmickUpdate();
          self.GotoState<TacticsUnitController.State_SkillEnd>();
        }
        Vector3 vector3 = Vector3.op_Subtraction(this.mBasePos, self.JumpFallPos);
        ((Component) self).transform.position = Vector3.op_Addition(self.JumpFallPos, Vector3.op_Multiply(vector3, this.mElapsed / this.ReturnTime));
        bool visible = this.beforeVisible;
        if (!this.isDirect)
        {
          if ((double) this.ReturnTime * 0.85000002384185791 < (double) this.mElapsed)
            visible = true;
          else if ((double) this.ReturnTime * 0.15000000596046448 < (double) this.mElapsed)
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
        ((Component) self).transform.position = this.mBasePos;
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
        GameObject go = UnityEngine.Object.Instantiate<GameObject>(EffectPrefab, ((Component) Parent).transform.position, ((Component) Parent).transform.rotation);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) go, (UnityEngine.Object) null))
        {
          go.RequireComponent<OneShotParticle>();
          go.transform.SetParent(((Component) Parent).transform);
        }
        return go;
      }

      private DelayedEffectSpawner CreateEffect(
        SkillEffect.EffectElement[] effects,
        TacticsUnitController Parent,
        bool set_rotation = false,
        float rotationY = 0.0f)
      {
        DelayedEffectSpawner effect = new GameObject().RequireComponent<DelayedEffectSpawner>();
        Transform transform = ((Component) Parent).transform;
        Quaternion rotation = transform.rotation;
        if (set_rotation)
          rotation = Quaternion.Euler(((Quaternion) ref rotation).eulerAngles.x, rotationY, ((Quaternion) ref rotation).eulerAngles.z);
        effect.Init(effects, transform.position, rotation);
        return effect;
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
        return (IEnumerator) new TacticsUnitController.State_ChangeGrid.\u003CWait\u003Ec__Iterator0()
        {
          self = self,
          Seconds = Seconds,
          \u0024this = this
        };
      }

      [DebuggerHidden]
      private IEnumerator EffectEndWait(float Seconds, TacticsUnitController self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_ChangeGrid.\u003CEffectEndWait\u003Ec__Iterator1()
        {
          Seconds = Seconds,
          self = self
        };
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
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSelfEffect, (UnityEngine.Object) null) || !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTargetEffect, (UnityEngine.Object) null))
            ;
          if (this.mChanged)
            return;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSelfParticle))
          {
            double time = (double) this.mSelfParticle.time;
            ParticleSystem.MainModule main = this.mSelfParticle.main;
            double duration = (double) ((ParticleSystem.MainModule) ref main).duration;
            if (time / duration >= 1.0)
              goto label_8;
          }
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSelfParticle))
            return;
label_8:
          this.mChanged = true;
          TacticsUnitController target = self.mSkillVars.Targets[0];
          Vector3 position1 = ((Component) self).transform.position;
          ((Component) self).transform.position = ((Component) target).transform.position;
          ((Component) target).transform.position = position1;
          if (!target.IsVisible())
          {
            SceneBattle instance = SceneBattle.Instance;
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
            {
              GameObject jumpSpotEffect = instance.GetJumpSpotEffect(target.Unit);
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) jumpSpotEffect))
              {
                jumpSpotEffect.transform.position = ((Component) target).transform.position;
                Vector3 position2 = jumpSpotEffect.transform.position;
                position2.y = GameUtility.RaycastGround(position2).y;
                jumpSpotEffect.transform.position = position2;
              }
              instance.OnGimmickUpdate();
            }
          }
          self.mSkillVars.mStartPosition = ((Component) self).transform.position;
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
        if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSceneBattle) || !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTargetTuc))
        {
          self.GotoState<TacticsUnitController.State_SkillEnd>();
        }
        else
        {
          self.LoadUnitAnimationAsync("B_TOSS_LIFT", "cmn_toss_lift0", false);
          self.LoadUnitAnimationAsync("B_TOSS_THROW", "cmn_toss_throw0", false);
          self.StartCoroutine(this.execPerformance(self));
        }
      }

      [DebuggerHidden]
      private IEnumerator execPerformance(TacticsUnitController self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_Throw.\u003CexecPerformance\u003Ec__Iterator0()
        {
          self = self,
          \u0024this = this
        };
      }

      [DebuggerHidden]
      private IEnumerator lerpTurn(TacticsUnitController target, Vector3 target_pos)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_Throw.\u003ClerpTurn\u003Ec__Iterator1()
        {
          target = target,
          target_pos = target_pos
        };
      }

      [DebuggerHidden]
      private IEnumerator lerpPickUp(TacticsUnitController self, TacticsUnitController target)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_Throw.\u003ClerpPickUp\u003Ec__Iterator2()
        {
          target = target,
          self = self
        };
      }

      [DebuggerHidden]
      private IEnumerator lerpThrow(TacticsUnitController target, Vector3 target_pos)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_Throw.\u003ClerpThrow\u003Ec__Iterator3()
        {
          target = target,
          target_pos = target_pos
        };
      }

      [DebuggerHidden]
      private IEnumerator lerpBound(TacticsUnitController target)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_Throw.\u003ClerpBound\u003Ec__Iterator4()
        {
          target = target
        };
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
        if ((double) self.GetRemainingTime("B_TRANSFORM") > 0.0)
          return;
        self.PlayIdle(0.1f);
      }
    }

    private class State_PreAfterTransform : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("B_PRE_TRANSFORM", false);
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) self.GetRemainingTime("B_PRE_TRANSFORM") > 0.0)
          return;
        self.PlayIdle(0.1f);
      }
    }

    private class State_BigUnitBsIdle : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("B_BU_BS_IDLE", true);
      }

      public override void Update(TacticsUnitController self)
      {
      }
    }

    private class State_BigUnitBsDamage : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("B_BU_BS_DAMAGE", false);
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) self.GetRemainingTime("B_BU_BS_DAMAGE") > 0.10000000149011612)
          return;
        self.PlayBigUnitBsIdle();
      }
    }
  }
}
