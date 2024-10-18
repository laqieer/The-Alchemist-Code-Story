// Decompiled with JetBrains decompiler
// Type: SRPG.SceneBattle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/Scene/Battle")]
  public class SceneBattle : Scene
  {
    public static readonly string QUEST_TEXTTABLE = "quest";
    private const float RenkeiSceneFadeTime = 0.5f;
    private const float RenkeiSceneBrightness = 0.25f;
    private static readonly Color RenkeiChargeColor = new Color(0.0f, 0.5f, 1.5f, 0.5f);
    private const float RenkeiChargeTime = 1f;
    private const float RenkeiAssistInterval = 0.5f;
    public InGameMenu mInGameMenu;
    public static readonly string DefaultExitPoint = "Home";
    public static SceneBattle Instance;
    private int mStartPlayerLevel;
    private const float MinLoadSkillTime = 0.75f;
    private BattleCore mBattle;
    private StateMachine<SceneBattle> mState;
    private Unit mSelectedBattleCommandTarget;
    private int mSelectedBattleCommandGridX;
    private int mSelectedBattleCommandGridY;
    private SkillData mSelectedBattleCommandSkill;
    private ItemData mSelectedBattleCommandItem;
    private GameObject mNavigation;
    private FlowNode_TutorialTrigger[] mTutorialTriggers;
    private bool mStartCalled;
    private bool mStartQuestCalled;
    private int mUnitStartCount;
    private int mUnitStartCountTotal;
    public bool IsNoCountUpUnitStartCount;
    private FlowNode_BattleUI mBattleUI;
    private FlowNode_BattleUI_MultiPlay mBattleUI_MultiPlay;
    private bool mUISignal;
    private UnitCursor UnitCursor;
    private TacticsSceneSettings mTacticsSceneRoot;
    private BattleSceneSettings mBattleSceneRoot;
    private BattleSceneSettings mDefaultBattleScene;
    private List<BattleSceneSettings> mBattleScenes = new List<BattleSceneSettings>();
    private DropGoldEffect mTreasureGoldTemplate;
    private TreasureBox mTreasureBoxTemplate;
    private DropItemEffect mTreasureDropTemplate;
    private GameObject mMapAddGemEffectTemplate;
    private Vector3 mCameraCenter;
    private List<TacticsUnitController> mTacticsUnits = new List<TacticsUnitController>(20);
    private QuestParam mCurrentQuest;
    private QuestStates mStartQuestState;
    private bool mDownloadTutorialAssets;
    private string mEventName;
    private int mNumHotTargets;
    private List<TacticsUnitController> mHotTargets = new List<TacticsUnitController>();
    private bool mIsRankingQuestNewScore;
    private bool mIsRankingQuestJoinReward;
    private int mRankingQuestNewRank;
    public string FirstClearItemId;
    public string EventPlayBgmID;
    private bool m_IsCardSendMail;
    private SceneBattle.eArenaSubmitMode mArenaSubmitMode;
    private int mCurrentUnitStartX;
    private int mCurrentUnitStartY;
    private List<JSON_MyPhotonPlayerParam> mAudiencePlayers;
    private bool mQuestStart;
    private SceneBattle.StateTransitionRequest mOnRequestStateChange;
    private Json_Unit mEditorSupportUnit;
    private bool mIsFirstPlay;
    private bool mIsFirstWin;
    private GridMap<float> mHeightMap;
    private float mWorldPosZ;
    private bool mMapReady;
    private bool mInterpAmbientLight;
    public const int VALID_ENEMY_UNIT_MAX = 13;
    private bool mFirstTurn;
    private SceneBattle.CloseBattleUIWindow mCloseBattleUIWindow = new SceneBattle.CloseBattleUIWindow();
    private const uint CHARGE_TARGET_ATTR_GRN = 1;
    private const uint CHARGE_TARGET_ATTR_RED = 2;
    private bool mIsShowCastSkill;
    private bool mAutoActivateGimmick;
    [NonSerialized]
    public int GoldCount;
    [NonSerialized]
    public int TreasureCount;
    public int DispTreasureCount;
    [NonSerialized]
    public SceneBattle.ExitRequests ExitRequest;
    private FlowNode_Network mReqSubmit;
    private bool mQuestResultSending;
    private bool mQuestResultSent;
    private int mFirstContact;
    private bool mRevertQuestNewIfRetire;
    private bool mIsForceEndQuest;
    private QuestResultData mSavedResult;
    public SceneBattle.QuestEndEvent OnQuestEnd = (SceneBattle.QuestEndEvent) (() => { });
    private GameObject GoResultBg;
    private bool mSceneExiting;
    private int mPauseReqCount;
    private Unit mLastPlayerSideUseSkillUnit;
    private bool mWaitSkillSplashClose;
    private SkillSplash mSkillSplash;
    private Unit mCollaboMainUnit;
    private TacticsUnitController mCollaboTargetTuc;
    private bool mIsInstigatorSubUnit;
    private SkillSplashCollabo mSkillSplashCollabo;
    private SkillMotion mSkillMotion = new SkillMotion();
    private bool isScreenMirroring;
    private List<TacticsUnitController> mUnitsInBattle = new List<TacticsUnitController>();
    private List<TacticsUnitController> mIgnoreShieldEffect = new List<TacticsUnitController>();
    private List<SceneBattle.EventRecvSkillUnit> mEventRecvSkillUnitLists = new List<SceneBattle.EventRecvSkillUnit>();
    private GameObject GoWeatherEffect;
    private bool IsSetWeatherEffect;
    private bool IsStoppedWeatherEffect = true;
    private const float UPVIEW_CAMERADISTANCE_BASE = 5f;
    private SceneBattle.CameraMode m_CameraMode;
    private float m_CameraYaw;
    private bool m_NewCamera;
    private bool m_FullRotationCamera;
    private TargetCamera m_TargetCamera;
    private float m_CameraYawMin;
    private float m_CameraYawMax;
    private float m_DefaultCameraYawMin;
    private float m_DefaultCameraYawMax;
    private bool m_UpdateCamera;
    private bool m_BattleCamera;
    private bool m_AllowCameraRotation;
    private bool m_AllowCameraTranslation;
    private float m_CameraAngle;
    private Vector3 m_CameraPosition;
    private bool m_TargetCameraDistanceInterp;
    private float m_TargetCameraDistance;
    private bool m_TargetCameraPositionInterp;
    private Vector3 m_TargetCameraPosition;
    private bool m_TargetCameraAngleInterp;
    private float m_TargetCameraAngle;
    private float m_TargetCameraAngleStart;
    private float m_TargetCameraAngleTime;
    private float m_TargetCameraAngleTimeMax;
    private readonly float MULTI_PLAY_INPUT_TIME_LIMIT_SEC = 10f;
    private readonly float MULTI_PLAY_INPUT_EXT_MOVE = 10f;
    private readonly float MULTI_PLAY_INPUT_EXT_SELECT = 10f;
    private readonly float SEND_TURN_SEC = 1f;
    private readonly int CPUBATTLE_PLAYER_NUM = 2;
    public readonly float SYNC_INTERVAL = 0.1f;
    public readonly float RESUME_REQUEST_INTERVAL = 0.1f;
    public readonly float RESUME_SUCCESS_INTERVAL = 0.1f;
    public float mRestSyncInterval;
    public float mRestResumeRequestInterval;
    public float mRestResumeSuccessInterval;
    private List<SceneBattle.MultiPlayInput> mSendList;
    private float mSendTime;
    private MultiSendLogBuffer mMultiSendLogBuffer;
    private bool mBeginMultiPlay;
    private bool mAudiencePause;
    private bool mAudienceSkip;
    private int mPrevGridX = -1;
    private int mPrevGridY = -1;
    private EUnitDirection mPrevDir = EUnitDirection.Auto;
    private List<SceneBattle.MultiPlayRecvData> mRecvBattle = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvCheck = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvGoodJob = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvContinue = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvIgnoreMyDisconnect = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvResume = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvResumeRequest = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mAudienceDisconnect = new List<SceneBattle.MultiPlayRecvData>();
    private SceneBattle.MultiPlayRecvData mAudienceRetire;
    private Dictionary<int, SceneBattle.MultiPlayRecvData> mRecvCheckData = new Dictionary<int, SceneBattle.MultiPlayRecvData>();
    private Dictionary<int, SceneBattle.MultiPlayRecvData> mRecvCheckMyData = new Dictionary<int, SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayCheck> mMultiPlayCheckList = new List<SceneBattle.MultiPlayCheck>();
    private SceneBattle.EDisconnectType mDisconnectType;
    private List<SceneBattle.MultiPlayer> mMultiPlayer;
    private List<SceneBattle.MultiPlayerUnit> mMultiPlayerUnit;
    private bool mResumeMultiPlay;
    private bool mResumeSend;
    private bool mResumeOnlyPlayer;
    private bool mResumeSuccess;
    private List<int> mResumeAlreadyStartPlayer = new List<int>();
    private bool mMapViewMode;
    private bool mAlreadyEndBattle;
    private bool mCheater;
    private bool mAudienceForceEnd;
    private int mCurrentSendInputUnitID;
    private int mMultiPlaySendID;
    private bool mExecDisconnected;
    private bool mShowInputTimeLimit;
    private int mThinkingPlayerIndex;
    private string mPhotonErrString = string.Empty;
    public List<SceneBattle.ReqCreateBreakObjUc> ReqCreateBreakObjUcLists = new List<SceneBattle.ReqCreateBreakObjUc>();
    public List<SceneBattle.ReqCreateDtuUc> ReqCreateDtuUcLists = new List<SceneBattle.ReqCreateDtuUc>();
    private bool mSetupGoodJob;
    private float mGoodJobWait;
    private const int GRIDLAYER_MOVABLE = 0;
    private const int GRIDLAYER_SHATEI = 1;
    private const int GRIDLAYER_KOUKA = 2;
    private const int GRIDLAYER_CHARGE_GRN = 3;
    private const int GRIDLAYER_CHARGE_RED = 4;
    private const string GRIDLAYER_MATPATH_CHARGE_GRN = "BG/GridMaterialGrn";
    private const string GRIDLAYER_MATPATH_CHARGE_RED = "BG/GridMaterialRed";
    private DirectionArrow DirectionArrowTemplate;
    private GameObject TargetMarkerTemplate;
    private SceneBattle.MoveInput mMoveInput;
    private EventScript mEventScript;
    private EventScript.Sequence mEventSequence;
    private UnitGauge GaugeOverlayTemplate;
    private UnitGauge EnemyGaugeOverlayTemplate;
    private UnitGauge EnemyBossGaugeOverlayTemplate;
    private GameObject mContinueWindowRes;
    [NonSerialized]
    private Unit mSelectedTarget;
    [NonSerialized]
    public bool UIParam_TargetValid;
    [NonSerialized]
    public AbilityData UIParam_CurrentAbility;
    private List<TacticsUnitController> mUnitList = new List<TacticsUnitController>();
    private List<TacticsUnitController> mGimmickList = new List<TacticsUnitController>();
    private bool mIsWaitingForBattleSignal;
    private bool mIsUnitChgActive;
    private SceneBattle.TargetSelectorParam mTargetSelectorParam;
    private EUnitDirection mSkillDirectionByKouka;
    private bool mIsBackSelectSkill;
    private static int MAX_MASK_BATTLE_UI = Enum.GetNames(typeof (SceneBattle.eMaskBattleUI)).Length;
    private uint mControlDisableMask;
    private TutorialButtonImage[] mTutorialButtonImages;
    private Quest_MoveUnit mUIMoveUnit;
    private GameObject mRenkeiAuraEffect;
    private GameObject mRenkeiAssistEffect;
    private GameObject mRenkeiChargeEffect;
    private GameObject mRenkeiHitEffect;
    private GameObject mSummonUnitEffect;
    private GameObject mUnitChangeEffect;
    private GameObject mWithdrawUnitEffect;
    private DamagePopup mDamageTemplate;
    private DamageDsgPopup mDamageDsgTemplate;
    private DamagePopup mHpHealTemplate;
    private DamagePopup mMpHealTemplate;
    private GameObject mAutoHealEffectTemplate;
    private GameObject mDrainHpEffectTemplate;
    private GameObject mDrainMpEffectTemplate;
    private GameObject mParamChangeEffectTemplate;
    private GameObject mConditionChangeEffectTemplate;
    private GameObject mChargeGrnTargetUnitEffect;
    private GameObject mChargeRedTargetUnitEffect;
    private GameObject mKnockBackEffect;
    private GameObject mTrickMarker;
    private Dictionary<string, GameObject> mTrickMarkerDics = new Dictionary<string, GameObject>();
    private GameObject mJumpFallEffect;
    private GameObject mInspirationEffect;
    private GameObject mProtectTargetEffect;
    private GameObject mProtectSelfEffect;
    private SkillNamePlate mSkillNamePlate;
    private SkillNamePlate mInspirationTelopPlate;
    private SkillTargetWindow mSkillTargetWindow;
    private GameObject mJumpSpotEffectTemplate;
    private GameObject mGuardPopup;
    private GameObject mAbsorbPopup;
    private GameObject mCriticalPopup;
    private GameObject mBackstabPopup;
    private GameObject mMissPopup;
    private GameObject mPerfectAvoidPopup;
    private GameObject mWeakPopup;
    private GameObject mResistPopup;
    private GameObject mGutsPopup;
    private GameObject mUnitOwnerIndex;
    private GemParticle[] mGemDrainEffect_Front = new GemParticle[12];
    private GemParticle[] mGemDrainEffect_Side = new GemParticle[12];
    private GemParticle[] mGemDrainEffect_Back = new GemParticle[12];
    private GameObject mGemDrainHitEffect;
    private List<KeyValuePair<Unit, GameObject>> mJumpSpotEffects = new List<KeyValuePair<Unit, GameObject>>();
    private GameObject mTargetMarkerTemplate;
    private GameObject mAssistMarkerTemplate;
    private GameObject mBlockedGridMarker;
    private bool mDisplayBlockedGridMarker;
    private Grid mGridDisplayBlockedGridMarker;
    private GameObject[] mUnitMarkerTemplates;
    private List<GameObject>[] mUnitMarkers;
    private bool mLoadedAllUI;
    private TouchController mTouchController;
    private GameObject mVersusPlayerTarget;
    private GameObject mVersusEnemyTarget;
    private GameObject mGoWeatherAttach;
    private SceneBattle.GridClickEvent mOnGridClick;
    private SceneBattle.UnitClickEvent mOnUnitClick;
    private SceneBattle.UnitFocusEvent mOnUnitFocus;
    private TacticsUnitController mFocusedUnit;
    private TacticsUnitController mMapModeFocusedUnit;
    private SceneBattle.ScreenClickEvent mOnScreenClick;
    private float mLoadProgress_UI;
    private float mLoadProgress_Scene;
    private float mLoadProgress_Animation;
    private SceneBattle.FocusTargetEvent mOnFocusTarget;
    private SceneBattle.SelectTargetEvent mOnSelectTarget;
    private SceneBattle.CancelTargetSelectEvent mOnCancelTargetSelect;
    private List<KeyValuePair<SceneBattle.PupupData, GameObject>> mPopups = new List<KeyValuePair<SceneBattle.PupupData, GameObject>>();
    private Dictionary<RectTransform, Vector3> mPopupPositionCache = new Dictionary<RectTransform, Vector3>();
    private List<RectTransform> mLayoutGauges = new List<RectTransform>(16);
    private Dictionary<RectTransform, Vector2> mGaugePositionCache = new Dictionary<RectTransform, Vector2>();
    private float mGaugeCollisionRadius = 50f;
    private float mGaugeYAspectRatio = 1f;
    private Dictionary<SkillParam, GameObject> mShieldEffects = new Dictionary<SkillParam, GameObject>();
    private bool mLoadingShieldEffects;

    public int UnitStartCount => this.mUnitStartCount;

    public int UnitStartCountTotal => this.mUnitStartCountTotal;

    public FlowNode_BattleUI BattleUI => this.mBattleUI;

    public GameObject CurrentScene
    {
      get
      {
        return UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTacticsSceneRoot) ? ((Component) this.mTacticsSceneRoot).gameObject : (GameObject) null;
      }
    }

    public QuestParam CurrentQuest => this.mCurrentQuest;

    public bool IsRankingQuestNewScore => this.mIsRankingQuestNewScore;

    public bool IsRankingQuestJoinReward => this.mIsRankingQuestJoinReward;

    public int RankingQuestNewRank => this.mRankingQuestNewRank;

    public bool ValidateRankingQuestRank => this.mRankingQuestNewRank > 0;

    public bool IsOrdealQuest
    {
      get => this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.Ordeal;
    }

    public bool IsGetFirstClearItem => !string.IsNullOrEmpty(this.FirstClearItemId);

    public bool IsCardSendMail => this.m_IsCardSendMail;

    public BattleCore Battle => this.mBattle;

    public List<JSON_MyPhotonPlayerParam> AudiencePlayer => this.mAudiencePlayers;

    public bool QuestStart
    {
      get => this.mQuestStart;
      set => this.mQuestStart = value;
    }

    private void ToggleBattleScene(bool visible, string sceneName = null)
    {
      if (visible)
      {
        BattleSceneSettings battleSceneSettings = (BattleSceneSettings) null;
        if (!string.IsNullOrEmpty(sceneName))
        {
          for (int index = 0; index < this.mBattleScenes.Count; ++index)
          {
            if (((UnityEngine.Object) this.mBattleScenes[index]).name == sceneName)
            {
              battleSceneSettings = this.mBattleScenes[index];
              break;
            }
          }
        }
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) battleSceneSettings, (UnityEngine.Object) null))
          battleSceneSettings = this.mDefaultBattleScene;
        this.mBattleSceneRoot = battleSceneSettings;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleSceneRoot, (UnityEngine.Object) null))
        ((Component) this.mBattleSceneRoot).gameObject.SetActive(visible);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTacticsSceneRoot, (UnityEngine.Object) null))
        ((Component) this.mTacticsSceneRoot).gameObject.SetActive(!visible);
      RenderPipeline component = ((Component) Camera.main).GetComponent<RenderPipeline>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.EnableVignette = !visible;
    }

    private void ToggleUserInterface(bool isEnabled)
    {
      string[] strArray = new string[5]
      {
        this.mBattleUI.QueueObjectID,
        this.mBattleUI.QuestStatusID,
        this.mBattleUI.MapHeightID,
        this.mBattleUI.ElementDiagram,
        this.mBattleUI.FukanCameraID
      };
      foreach (string name in strArray)
      {
        Canvas gameObject = GameObjectID.FindGameObject<Canvas>(name);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          ((Behaviour) gameObject).enabled = isEnabled;
      }
      BattleCameraControl gameObject1 = GameObjectID.FindGameObject<BattleCameraControl>(this.mBattleUI.CameraControllerID);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        gameObject1.SetDisp(isEnabled);
      if (!this.Battle.IsMultiVersus)
        return;
      List<Unit> units = this.Battle.Units;
      if (units == null)
        return;
      for (int index = 0; index < units.Count; ++index)
      {
        TacticsUnitController unitController = this.FindUnitController(units[index]);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          unitController.ShowVersusCursor(isEnabled);
      }
    }

    public void EnableUserInterface() => this.ToggleUserInterface(true);

    public void DisableUserInterface() => this.ToggleUserInterface(false);

    private void Start()
    {
      MonoSingleton<GameManager>.Instance.Player.ClearItemFlags(ItemData.ItemFlags.NewItem | ItemData.ItemFlags.NewSkin);
      LocalizedText.LoadTable(SceneBattle.QUEST_TEXTTABLE);
      FadeController.Instance.ResetSceneFade(0.0f);
      this.InitCamera();
      int length = Enum.GetValues(typeof (SceneBattle.UnitMarkerTypes)).Length;
      this.mUnitMarkerTemplates = new GameObject[length];
      this.mUnitMarkers = new List<GameObject>[length];
      for (int index = 0; index < length; ++index)
        this.mUnitMarkers[index] = new List<GameObject>(12);
      this.mBattle = new BattleCore();
      this.mState = new StateMachine<SceneBattle>(this);
      SceneBattle.SimpleEvent.Clear();
      SceneBattle.SimpleEvent.Add(SceneBattle.TreasureEvent.GROUP, (SceneBattle.SimpleEvent.Interface) new SceneBattle.TreasureEvent());
      if (!this.mStartQuestCalled)
        this.GotoState<SceneBattle.State_ReqBtlComReq>();
      this.mStartCalled = true;
    }

    public bool IsPlayingArenaQuest => this.mCurrentQuest.type == QuestTypes.Arena;

    public bool IsPlayingGvGQuest => this.mCurrentQuest.type == QuestTypes.GvG;

    public bool IsPlayingPreCalcResultQuest => this.IsPlayingArenaQuest || this.IsPlayingGvGQuest;

    public bool IsArenaRankupInfo()
    {
      return GlobalVars.ResultArenaBattleResponse != null && GlobalVars.ResultArenaBattleResponse.reward_info.IsReward();
    }

    public int CalcArenaRankDelta()
    {
      return GlobalVars.ResultArenaBattleResponse != null ? Mathf.Max(MonoSingleton<GameManager>.Instance.Player.ArenaRankBest - GlobalVars.ResultArenaBattleResponse.new_rank, 0) : 0;
    }

    public bool IsPlayingMultiQuest => this.mCurrentQuest.IsMulti;

    public bool IsPlayingTower => this.mCurrentQuest.type == QuestTypes.Tower;

    private void OnEnable()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
        return;
      SceneBattle.Instance = this;
    }

    private void OnDisable()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) this))
        return;
      SceneBattle.Instance = (SceneBattle) null;
    }

    private void OnDestroy()
    {
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoResultBg))
        UnityEngine.Object.Destroy((UnityEngine.Object) this.GoResultBg.gameObject);
      this.GoResultBg = (GameObject) null;
      LocalizedText.UnloadTable(SceneBattle.QUEST_TEXTTABLE);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mNavigation, (UnityEngine.Object) null))
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mNavigation.gameObject);
        this.mNavigation = (GameObject) null;
        this.mTutorialTriggers = (FlowNode_TutorialTrigger[]) null;
      }
      this.CleanupGoodJob();
      this.DestroyCamera();
      this.DestroyUI();
      if (this.mBattle != null)
      {
        this.mBattle.Release();
        this.mBattle = (BattleCore) null;
      }
      FadeController.Instance.ReleaseFadeUnits();
    }

    private void OnLoadTacticsScene(GameObject root)
    {
      TacticsSceneSettings component1 = root.GetComponent<TacticsSceneSettings>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        return;
      SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnLoadTacticsScene));
      CriticalSection.Leave(CriticalSections.SceneChange);
      GameUtility.DeactivateActiveChildComponents<Camera>((Component) component1);
      this.mTacticsSceneRoot = component1;
      for (int index1 = 0; index1 < root.transform.childCount; ++index1)
      {
        Transform child1 = root.transform.GetChild(index1);
        if (((UnityEngine.Object) child1).name == "light")
        {
          for (int index2 = 0; index2 < child1.childCount; ++index2)
          {
            Transform child2 = child1.GetChild(index2);
            if (((UnityEngine.Object) child2).name != "Ambient Light")
              UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) child2).gameObject);
          }
          break;
        }
      }
      RenderPipeline component2 = ((Component) Camera.main).GetComponent<RenderPipeline>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
      {
        component2.BackgroundImage = (Texture) component1.BackgroundImage;
        component2.ScreenFilter = (Texture) component1.ScreenFilter;
      }
      if (!component1.OverrideCameraSettings)
        return;
      this.SetCameraYawRange(component1.CameraYawMin, component1.CameraYawMax);
    }

    public void UpdateUnitHP(TacticsUnitController controller)
    {
    }

    public void UpdateUnitMP(TacticsUnitController controller)
    {
    }

    private void FocusUnit(Unit unit)
    {
      if (unit == null)
        return;
      TacticsUnitController unitController = this.FindUnitController(unit);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        return;
      GameSettings instance = GameSettings.Instance;
      ObjectAnimator.Get((Component) Camera.main).AnimateTo(Vector3.op_Addition(((Component) unitController).transform.position, ((Component) instance.Quest.UnitCamera).transform.position), ((Component) instance.Quest.UnitCamera).transform.rotation, 0.3f, ObjectAnimator.CurveType.EaseOut);
    }

    public void GotoNextState()
    {
      if (this.mOnRequestStateChange == null)
        return;
      this.mOnRequestStateChange(SceneBattle.StateTransitionTypes.Forward);
    }

    public void GotoPreviousState()
    {
      if (this.mOnRequestStateChange == null)
        return;
      this.mOnRequestStateChange(SceneBattle.StateTransitionTypes.Back);
    }

    public static Vector3 RaycastGround(Vector3 start)
    {
      RaycastHit raycastHit;
      return Physics.Raycast(Vector3.op_Addition(start, Vector3.op_Multiply(Vector3.up, 10f)), Vector3.op_UnaryNegation(Vector3.up), ref raycastHit) ? ((RaycastHit) ref raycastHit).point : start;
    }

    private TacticsUnitController FindClosestUnitController(Vector3 position, float maxDistance)
    {
      TacticsUnitController closestUnitController = (TacticsUnitController) null;
      float num1 = maxDistance;
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
      {
        TacticsUnitController mTacticsUnit = this.mTacticsUnits[index];
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) mTacticsUnit, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) closestUnitController, (UnityEngine.Object) null) && this.CalcCoord(closestUnitController.CenterPosition) == this.CalcCoord(mTacticsUnit.CenterPosition) && !mTacticsUnit.Unit.IsGimmick)
          {
            closestUnitController = mTacticsUnit;
          }
          else
          {
            float num2 = GameUtility.CalcDistance2D(((Component) mTacticsUnit).transform.position, position);
            if ((double) num2 < (double) num1)
            {
              closestUnitController = mTacticsUnit;
              num1 = num2;
            }
          }
        }
      }
      return closestUnitController;
    }

    public int GetDisplayHeight(Unit unit)
    {
      TacticsUnitController unitController = this.FindUnitController(unit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
      {
        IntVector2 intVector2 = this.CalcCoord(unitController.CenterPosition);
        Grid current = this.mBattle.CurrentMap[intVector2.x, intVector2.y];
        if (current != null)
          return current.height;
      }
      return 0;
    }

    public IntVector2 CalcCoord(Vector3 position)
    {
      return new IntVector2(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.z - ((Component) this.mTacticsSceneRoot).transform.position.z));
    }

    public Vector3 CalcGridCenter(Grid grid)
    {
      return grid == null ? Vector3.zero : new Vector3((float) grid.x + 0.5f, this.mHeightMap.get(grid.x, grid.y), (float) grid.y + 0.5f);
    }

    public Vector3 CalcGridCenter(int x, int y)
    {
      return this.CalcGridCenter(this.mBattle.CurrentMap[x, y]);
    }

    public EUnitDirection UnitDirectionFromPosition(
      Vector3 self,
      Vector3 target,
      SkillParam skill_param)
    {
      IntVector2 intVector2_1 = this.CalcCoord(self);
      IntVector2 intVector2_2 = this.CalcCoord(target);
      Grid current1 = this.mBattle.CurrentMap[intVector2_1.x, intVector2_1.y];
      Grid current2 = this.mBattle.CurrentMap[intVector2_2.x, intVector2_2.y];
      return skill_param != null && skill_param.select_scope == ESelectType.LaserTwin ? this.mBattle.UnitDirectionFromGridLaserTwin(current1, current2) : this.mBattle.UnitDirectionFromGrid(current1, current2);
    }

    public void RemoveLog()
    {
      if (this.mBattle.Logs.Peek != null)
        DebugUtility.Log("RemoveLog(): " + (object) this.mBattle.Logs.Peek.GetType());
      this.Battle.Logs.RemoveLog();
    }

    private void Update()
    {
      this.UpdateMultiPlayer();
      this.UpdateAudiencePlayer();
      if (this.mState != null && this.Battle.ResumeState != BattleCore.RESUME_STATE.WAIT)
        this.mState.Update();
      this.UpdateCameraControl();
      MyEncrypt.EncryptCount = 0;
      MyEncrypt.DecryptCount = 0;
      if (this.mDownloadTutorialAssets && AssetManager.UseDLC && GameUtility.Config_UseAssetBundles.Value && MonoSingleton<GameManager>.Instance.HasTutorialDLAssets && AssetDownloader.isDone)
        MonoSingleton<GameManager>.Instance.PartialDownloadTutorialAssets();
      if (this.CurrentQuest == null || this.CurrentQuest.type == QuestTypes.Tutorial)
        return;
      GlobalVars.IsTutorialEnd = true;
    }

    private void StartDownloadNextQuestAsync()
    {
      this.StartCoroutine(this.DownloadNextQuestAsync());
    }

    [DebuggerHidden]
    private IEnumerator DownloadNextQuestAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CDownloadNextQuestAsync\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public void GotoState<StateType>() where StateType : State<SceneBattle>, new()
    {
      this.mState.GotoState<StateType>();
    }

    public bool IsInState<StateType>() where StateType : State<SceneBattle>
    {
      return this.mState.IsInState<StateType>();
    }

    [DebuggerHidden]
    private IEnumerator DownloadQuestAsync(QuestParam quest)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CDownloadQuestAsync\u003Ec__Iterator1()
      {
        quest = quest,
        \u0024this = this
      };
    }

    public void StartQuest(string questID, BattleCore.Json_Battle json)
    {
      this.mStartQuestCalled = true;
      this.StartCoroutine(this.StartQuestAsync(questID, json));
    }

    public bool IsFirstPlay => this.mIsFirstPlay;

    public bool IsFirstWin => this.mIsFirstWin;

    public bool IsPlayLastDemo
    {
      get
      {
        return this.mBattle != null && this.mCurrentQuest != null && this.mBattle.GetQuestResult() == BattleCore.QuestResult.Win && !string.IsNullOrEmpty(this.mCurrentQuest.event_clear) && this.mIsFirstWin;
      }
    }

    [DebuggerHidden]
    private IEnumerator StartQuestAsync(string questID, BattleCore.Json_Battle jsonBtl)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CStartQuestAsync\u003Ec__Iterator2()
      {
        questID = questID,
        jsonBtl = jsonBtl,
        \u0024this = this
      };
    }

    public void PlayBGM()
    {
      if (!string.IsNullOrEmpty(this.EventPlayBgmID))
        MonoSingleton<MySound>.Instance.PlayBGM(this.EventPlayBgmID, IsUnManaged: EventAction.IsUnManagedAssets(this.EventPlayBgmID, true));
      else
        MonoSingleton<MySound>.Instance.PlayBGM(this.mBattle.CurrentMap.BGMName);
    }

    public void StopBGM() => MonoSingleton<MySound>.Instance.PlayBGM((string) null);

    private void UpdateBGM()
    {
      if (string.IsNullOrEmpty(this.mBattle.CurrentMap.BGMName))
        this.StopBGM();
      else
        this.PlayBGM();
    }

    private void GotoQuestStart()
    {
      this.mRevertQuestNewIfRetire = false;
      this.GotoState_WaitSignal<SceneBattle.State_QuestStartV2>();
    }

    private void CalcGridHeights()
    {
      this.mHeightMap = new GridMap<float>(this.mBattle.CurrentMap.Width, this.mBattle.CurrentMap.Height);
      for (int y = 0; y < this.mBattle.CurrentMap.Height; ++y)
      {
        for (int x = 0; x < this.mBattle.CurrentMap.Width; ++x)
          this.mHeightMap.set(x, y, this.CalcHeight((float) x + 0.5f, (float) y + 0.5f));
      }
    }

    private void BeginLoadMapAsync()
    {
      this.mMapReady = false;
      this.StartCoroutine(this.LoadMapAsync());
    }

    [DebuggerHidden]
    private IEnumerator LoadMapAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CLoadMapAsync\u003Ec__Iterator3()
      {
        \u0024this = this
      };
    }

    private bool IsUnitLoading
    {
      get
      {
        for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        {
          if (this.mTacticsUnits[index].IsLoading)
            return true;
        }
        return false;
      }
    }

    private void ResetUnitPosition(TacticsUnitController controller)
    {
      Vector3 vector3 = this.CalcGridCenter(this.mBattle.GetUnitGridPosition(controller.Unit));
      ((Component) controller).transform.position = vector3;
      controller.ResetRotation();
    }

    private void GotoMapStart()
    {
      this.ArenaActionCountEnable(this.IsPlayingPreCalcResultQuest || this.Battle.IsMultiVersus);
      this.RankingQuestActionCountEnable(this.Battle.IsRankingQuest);
      if (this.IsPlayingArenaQuest)
        this.GotoState<SceneBattle.State_ArenaCalc>();
      else if (this.IsPlayingGvGQuest)
      {
        this.GotoState<SceneBattle.State_GvGCalc>();
      }
      else
      {
        if (this.Battle.IsMultiVersus)
          this.ArenaActionCountSet(this.Battle.RemainVersusTurnCount);
        if (this.Battle.IsRankingQuest)
          this.RankingQuestActionCountSet((uint) this.Battle.ActionCountTotal);
        this.Battle.MapStart();
        if (this.Battle.CheckEnableSuspendStart() && this.Battle.LoadSuspendData())
          this.Battle.RelinkTrickGimmickEvents();
        bool pc_enable = false;
        bool ec_enable = false;
        int pc_count = 0;
        int ec_count = 0;
        BattleMap currentMap = this.Battle.CurrentMap;
        if (currentMap != null)
        {
          pc_enable = this.Battle.CheckEnableRemainingActionCount(currentMap.WinMonitorCondition);
          pc_count = this.Battle.GetRemainingActionCount(currentMap.WinMonitorCondition);
          ec_enable = this.Battle.CheckEnableRemainingActionCount(currentMap.LoseMonitorCondition);
          ec_count = this.Battle.GetRemainingActionCount(currentMap.LoseMonitorCondition);
          if (pc_count == -1)
            pc_enable = false;
          if (ec_count == -1)
            ec_enable = false;
        }
        this.RemainingActionCountEnable(pc_enable, ec_enable);
        this.RemainingActionCountSet((uint) pc_count, (uint) ec_count);
        this.GotoState<SceneBattle.State_LoadMapV2>();
      }
    }

    private Unit RayPickUnit(Vector2 pos)
    {
      Camera main = Camera.main;
      Vector3 position = ((Component) main).transform.position;
      float num1 = (float) (Screen.width / this.mBattle.CurrentMap.Width) * 0.5f;
      Unit unit1 = (Unit) null;
      float num2 = float.MaxValue;
      for (int index = 0; index < this.mBattle.Units.Count; ++index)
      {
        Unit unit2 = this.mBattle.Units[index];
        TacticsUnitController unitController = this.FindUnitController(unit2);
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          Vector3 vector3 = GameUtility.DeformPosition(((Component) unitController).transform.position, position.z);
          Vector3 screenPoint = main.WorldToScreenPoint(vector3);
          Vector2 vector2 = pos;
          vector2.x -= screenPoint.x;
          vector2.y -= screenPoint.y;
          if ((double) ((Vector2) ref vector2).magnitude <= (double) num1 && (double) screenPoint.z < (double) num2)
          {
            unit1 = unit2;
            num2 = screenPoint.z;
          }
        }
      }
      return unit1;
    }

    private void ShowUnitCursor(Unit unit)
    {
      if (unit == null)
        return;
      GameSettings instance = GameSettings.Instance;
      this.ShowUnitCursor(unit, unit.Side != EUnitSide.Player ? instance.Colors.Enemy : instance.Colors.Player);
      for (int index = 0; index < this.mBattle.Units.Count; ++index)
      {
        Unit unit1 = this.mBattle.Units[index];
        if (unit1 != unit && !unit1.IsNormalSize)
          this.ShowUnitCursor(unit1, unit1.Side != EUnitSide.Player ? instance.Colors.Enemy : instance.Colors.Player);
      }
    }

    private void ShowUnitCursor(Unit unit, Color color)
    {
      if (unit == null)
        return;
      TacticsUnitController unitController = this.FindUnitController(unit);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        return;
      unitController.ShowCursor(this.UnitCursor, color);
    }

    private void HideUnitCursor(Unit unit)
    {
      if (unit == null)
        return;
      TacticsUnitController unitController = this.FindUnitController(unit);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        return;
      this.HideUnitCursor(unitController);
    }

    public void HideUnitCursor(TacticsUnitController controller)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) controller, (UnityEngine.Object) null))
        controller.HideCursor();
      for (int index = 0; index < this.mBattle.Units.Count; ++index)
      {
        Unit unit = this.mBattle.Units[index];
        if ((!UnityEngine.Object.op_Inequality((UnityEngine.Object) controller, (UnityEngine.Object) null) || unit != controller.Unit) && !unit.IsNormalSize)
        {
          TacticsUnitController unitController = this.FindUnitController(unit);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController))
            unitController.HideCursor();
        }
      }
    }

    private void CloseBattleUI()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI, (UnityEngine.Object) null))
      {
        this.mBattleUI.OnInputTimeLimit();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.TargetMain, (UnityEngine.Object) null))
          this.mBattleUI.TargetMain.ForceClose();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.TargetSub, (UnityEngine.Object) null))
          this.mBattleUI.TargetSub.ForceClose();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.TargetObjectSub, (UnityEngine.Object) null))
          this.mBattleUI.TargetObjectSub.ForceClose();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.TargetTrickSub, (UnityEngine.Object) null))
          this.mBattleUI.TargetTrickSub.ForceClose();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.CommandWindow, (UnityEngine.Object) null))
          this.mBattleUI.CommandWindow.OnCommandSelect = (UnitCommands.CommandEvent) null;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.ItemWindow, (UnityEngine.Object) null))
          this.mBattleUI.ItemWindow.OnSelectItem = (BattleInventory.SelectEvent) null;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.SkillWindow, (UnityEngine.Object) null))
          this.mBattleUI.SkillWindow.OnSelectSkill = (UnitAbilitySkillList.SelectSkillEvent) null;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillTargetWindow, (UnityEngine.Object) null))
          this.mSkillTargetWindow.ForceHide();
        if (this.mTacticsUnits != null)
        {
          for (int index = 0; index < this.mTacticsUnits.Count; ++index)
          {
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTacticsUnits[index], (UnityEngine.Object) null))
              this.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal);
          }
        }
      }
      this.mOnUnitFocus = (SceneBattle.UnitFocusEvent) null;
      this.mOnUnitClick = (SceneBattle.UnitClickEvent) null;
      this.mOnRequestStateChange = (SceneBattle.StateTransitionRequest) null;
      this.mOnGridClick = (SceneBattle.GridClickEvent) null;
      this.mOnScreenClick = (SceneBattle.ScreenClickEvent) null;
      this.HideUnitMarkers();
      this.HideGrid();
      if (this.mCloseBattleUIWindow == null)
        return;
      this.mCloseBattleUIWindow.CloseAll();
    }

    private void HighlightTargetGrid(Unit current, int range, int difffloor)
    {
      if (range <= 0)
        return;
      BattleMap currentMap = this.mBattle.CurrentMap;
      Grid grid1 = currentMap[current.x, current.y];
      GridMap<bool> grid2 = new GridMap<bool>(currentMap.Width, currentMap.Height);
      for (int y = 0; y < grid2.h; ++y)
      {
        for (int x = 0; x < grid2.w; ++x)
        {
          if (x == current.x || y == current.y)
          {
            int num = Mathf.Max(Mathf.Abs(x - current.x), Mathf.Abs(y - current.y));
            if (num > 0 && num <= range)
            {
              if (difffloor != 0)
              {
                Grid grid3 = currentMap[x, y];
                if (grid3 == null || Mathf.Abs(grid1.height - grid3.height) > difffloor)
                  continue;
              }
              grid2.set(x, y, true);
            }
          }
        }
      }
      this.mTacticsSceneRoot.ShowGridLayer(0, grid2, Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea));
    }

    private void InternalShowCastSkill()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode)
        return;
      List<Unit> unitList = new List<Unit>();
      foreach (BattleCore.OrderData orderData in this.mBattle.Order)
      {
        if (orderData.Unit.Side == EUnitSide.Player && orderData.IsCastSkill && orderData.Unit != null && orderData.Unit.CastSkill != null)
          unitList.Add(orderData.Unit);
      }
      if (this.mBattle.CurrentUnit != null && this.mBattle.CurrentUnit.CastSkill != null && !unitList.Contains(this.mBattle.CurrentUnit) && this.mBattle.CurrentUnit.Side == EUnitSide.Player)
        unitList.Add(this.mBattle.CurrentUnit);
      if (unitList.Count <= 0)
        return;
      List<SceneBattle.ChargeTarget> chargeTargetList = new List<SceneBattle.ChargeTarget>();
      Color32 src1 = Color32.op_Implicit(GameSettings.Instance.Colors.ChargeAreaGrn);
      Color32 src2 = Color32.op_Implicit(GameSettings.Instance.Colors.ChargeAreaRed);
      GridMap<Color32> grid1 = new GridMap<Color32>(this.mBattle.CurrentMap.Width, this.mBattle.CurrentMap.Height);
      GridMap<Color32> grid2 = new GridMap<Color32>(this.mBattle.CurrentMap.Width, this.mBattle.CurrentMap.Height);
      foreach (Unit unit1 in unitList)
      {
        Unit unit = unit1;
        bool flag = false;
        if (unit.CastSkill.IsAdvantage())
          flag = true;
        int num1 = 0;
        int num2 = 0;
        if (unit.UnitTarget != null)
        {
          num1 = unit.UnitTarget.x;
          num2 = unit.UnitTarget.y;
          if (unit.UnitTarget == this.mBattle.CurrentUnit)
          {
            IntVector2 intVector2 = this.CalcCoord(this.FindUnitController(unit.UnitTarget).CenterPosition);
            num1 = intVector2.x;
            num2 = intVector2.y;
          }
        }
        else if (unit.GridTarget != null)
        {
          num1 = unit.GridTarget.x;
          num2 = unit.GridTarget.y;
        }
        GridMap<bool> scopeGridMap = this.mBattle.CreateScopeGridMap(unit, unit.x, unit.y, num1, num2, unit.CastSkill);
        if (SkillParam.IsTypeLaser(unit.CastSkill.SkillParam.select_scope) && unit.UnitTarget != null)
          scopeGridMap.set(num1, num2, true);
        if (unit.CastSkill.TeleportType == eTeleportType.Only || unit.CastSkill.TeleportType == eTeleportType.AfterSkill)
          scopeGridMap.set(num1, num2, false);
        for (int x = 0; x < scopeGridMap.w; ++x)
        {
          if (x < grid1.w)
          {
            for (int y = 0; y < scopeGridMap.h; ++y)
            {
              if (y < grid1.h && scopeGridMap.get(x, y))
              {
                if (unit.UnitTarget != null && x == num1 && y == num2)
                {
                  SceneBattle.ChargeTarget chargeTarget = chargeTargetList.Find((Predicate<SceneBattle.ChargeTarget>) (ctl => ctl.mUnit == unit.UnitTarget));
                  if (chargeTarget != null)
                    chargeTarget.AddAttr(!flag ? 2U : 1U);
                  else
                    chargeTargetList.Add(new SceneBattle.ChargeTarget(unit.UnitTarget, !flag ? 2U : 1U));
                }
                if (flag)
                  grid1.set(x, y, src1);
                else
                  grid2.set(x, y, src2);
              }
            }
          }
        }
      }
      this.mTacticsSceneRoot.ShowGridLayer(3, grid1, "BG/GridMaterialGrn");
      this.mTacticsSceneRoot.ShowGridLayer(4, grid2, "BG/GridMaterialRed");
      foreach (SceneBattle.ChargeTarget chargeTarget in chargeTargetList)
      {
        TacticsUnitController unitController = this.FindUnitController(chargeTarget.mUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          if (((int) chargeTarget.mAttr & 1) != 0)
            unitController.EnableChargeTargetUnit(this.mChargeGrnTargetUnitEffect, true);
          if (((int) chargeTarget.mAttr & 2) != 0)
            unitController.EnableChargeTargetUnit(this.mChargeRedTargetUnitEffect, false);
        }
      }
    }

    private void InternalHideCastSkill(bool is_only_target_unit = false)
    {
      if (!is_only_target_unit)
      {
        this.mTacticsSceneRoot.HideGridLayer(3);
        this.mTacticsSceneRoot.HideGridLayer(4);
      }
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        this.mTacticsUnits[index].DisableChargeTargetUnit();
    }

    private void ShowCastSkill()
    {
      this.mIsShowCastSkill = true;
      if (!GameUtility.Config_ChargeDisp.Value)
        return;
      this.InternalShowCastSkill();
    }

    private void HideCastSkill(bool is_only_target_unit = false)
    {
      this.mIsShowCastSkill = false;
      if (!GameUtility.Config_ChargeDisp.Value)
        return;
      this.InternalHideCastSkill(is_only_target_unit);
    }

    public void ReflectCastSkill(bool is_disp)
    {
      if (is_disp)
      {
        if (!this.mIsShowCastSkill)
          return;
        this.InternalShowCastSkill();
      }
      else
      {
        if (!this.mIsShowCastSkill)
          return;
        this.InternalHideCastSkill();
      }
    }

    private void GotoInputMovement()
    {
      this.mBattleUI.OnInputMoveStart();
      this.GotoState_WaitSignal<SceneBattle.State_MapMoveSelect_Stick>();
    }

    private Unit FindTarget(
      Unit current,
      SkillData skill,
      GridMap<bool> map,
      EUnitSide targetSide,
      bool is_not_self = false)
    {
      TacticsUnitController unitController = this.FindUnitController(current);
      IntVector2 intVector2 = this.CalcCoord(unitController.CenterPosition);
      Vector3 forward = ((Component) unitController).transform.forward;
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector(forward.x, forward.z);
      float num1 = float.MinValue;
      Unit unit1 = (Unit) null;
      for (int index = this.mBattle.Units.Count - 1; index >= 0; --index)
      {
        Unit unit2 = this.mBattle.Units[index];
        if ((!unit2.IsGimmick || unit2.IsBreakObj) && !unit2.IsDead && unit2.IsEntry && !unit2.IsSub && (!is_not_self || unit2 != current) && (unit2.Side == targetSide || this.mBattle.IsTargetBreakUnit(current, unit2, skill)) && map.isValid(unit2.x, unit2.y) && this.mBattle.IsTargetUnitForGridMap(unit2, map))
        {
          int x = unit2.x;
          int y = unit2.y;
          Vector2 vector2_2;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_2).\u002Ector((float) (x - current.x), (float) (y - current.y));
          float magnitude = ((Vector2) ref vector2_2).magnitude;
          ((Vector2) ref vector2_2).Normalize();
          float num2 = Vector2.Dot(vector2_1, vector2_2) - 1f - magnitude;
          if ((double) num1 < (double) num2)
          {
            BattleCore.CommandResult commandResult = this.mBattle.GetCommandResult(current, intVector2.x, intVector2.y, unit2.x, unit2.y, skill);
            if (commandResult != null && commandResult.targets != null && commandResult.targets.Count > 0)
            {
              unit1 = unit2;
              num1 = num2;
            }
          }
        }
      }
      return unit1 ?? (Unit) null;
    }

    private void GotoSelectAttackTarget()
    {
      Unit currentUnit = this.mBattle.CurrentUnit;
      TacticsUnitController unitController = this.FindUnitController(currentUnit);
      IntVector2 intVector2 = this.CalcCoord(unitController.CenterPosition);
      SkillData attackSkill = this.mBattle.CurrentUnit.GetAttackSkill();
      int x1 = currentUnit.x;
      int y1 = currentUnit.y;
      currentUnit.x = intVector2.x;
      currentUnit.y = intVector2.y;
      List<Unit> attackTargetsAi = this.mBattle.CreateAttackTargetsAI(currentUnit, attackSkill, false);
      Vector3 forward = ((Component) unitController).transform.forward;
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector(forward.x, forward.z);
      float num1 = float.MinValue;
      Unit defaultTarget = (Unit) null;
      for (int index = 0; index < attackTargetsAi.Count; ++index)
      {
        if (currentUnit != attackTargetsAi[index])
        {
          int x2 = attackTargetsAi[index].x;
          int y2 = attackTargetsAi[index].y;
          Vector2 vector2_2;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_2).\u002Ector((float) (x2 - currentUnit.x), (float) (y2 - currentUnit.y));
          float magnitude = ((Vector2) ref vector2_2).magnitude;
          ((Vector2) ref vector2_2).Normalize();
          float num2 = Vector2.Dot(vector2_1, vector2_2) - 1f - magnitude;
          if ((double) num1 < (double) num2)
          {
            defaultTarget = attackTargetsAi[index];
            num1 = num2;
          }
        }
      }
      currentUnit.x = x1;
      currentUnit.y = y1;
      this.GotoSelectTarget(attackSkill, new SceneBattle.SelectTargetCallback(this.GotoMapCommand), new SceneBattle.SelectTargetPositionWithSkill(this.OnSelectAttackTarget), defaultTarget);
    }

    private bool ApplyUnitMovement(bool test = false)
    {
      Unit currentUnit = this.mBattle.CurrentUnit;
      TacticsUnitController unitController = this.FindUnitController(currentUnit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) && !currentUnit.IsUnitFlag(EUnitFlag.Moved))
      {
        IntVector2 intVector2 = this.CalcCoord(unitController.CenterPosition);
        EUnitDirection direction = unitController.CalcUnitDirectionFromRotation();
        if (currentUnit.x != intVector2.x || currentUnit.y != intVector2.y)
        {
          Grid current = this.mBattle.CurrentMap[intVector2.x, intVector2.y];
          if (test)
            return this.mBattle.CheckMove(currentUnit, current);
          if (!this.Battle.Move(this.Battle.CurrentUnit, current, direction, true))
          {
            DebugUtility.LogError("移動に失敗");
            return false;
          }
          this.SendInputGridXY(this.Battle.CurrentUnit, intVector2.x, intVector2.y, currentUnit.Direction);
          this.SendInputMoveEnd(this.Battle.CurrentUnit, false);
        }
        else if (currentUnit.Direction != direction)
          this.Battle.CurrentUnit.Direction = direction;
      }
      return true;
    }

    private void OnSelectAttackTarget(int x, int y, SkillData skill, bool bUnitLockTarget)
    {
      this.HideAllHPGauges();
      this.HideAllUnitOwnerIndex();
      Unit currentUnit = this.mBattle.CurrentUnit;
      if (!this.ApplyUnitMovement())
        return;
      if (this.Battle.IsMultiPlay)
        this.SendInputEntryBattle(EBattleCommand.Attack, this.Battle.CurrentUnit, this.mSelectedTarget, (SkillData) null, (ItemData) null, x, y, bUnitLockTarget);
      if (!this.Battle.UseSkill(currentUnit, x, y, skill, bUnitLockTarget))
        DebugUtility.LogError("failed use skill. Unit:" + currentUnit.UnitName + ", x:" + (object) x + ", y:" + (object) y + ", skill:" + (object) currentUnit.GetAttackSkill());
      else
        this.GotoState<SceneBattle.State_WaitForLog>();
    }

    private int CountAccessibleGrids(GridMap<int> grids)
    {
      int num = 0;
      for (int y = 0; y < grids.h; ++y)
      {
        for (int x = 0; x < grids.w; ++x)
        {
          if (grids.get(x, y) >= 0)
            ++num;
        }
      }
      return num;
    }

    private void ResetUnitPosition()
    {
      Unit currentUnit = this.mBattle.CurrentUnit;
      TacticsUnitController unitController = this.FindUnitController(currentUnit);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        return;
      Grid unitGridPosition = this.mBattle.GetUnitGridPosition(currentUnit);
      ((Component) unitController).transform.position = this.CalcGridCenter(unitGridPosition);
    }

    private Vector3[] FindPath(
      Unit unit,
      int startX,
      int startY,
      int goalX,
      int goalY,
      int disableHeight,
      GridMap<int> walkableField)
    {
      Grid[] path1 = this.mBattle.CurrentMap.FindPath(unit, startX, startY, goalX, goalY, disableHeight, walkableField);
      if (path1 == null)
        return (Vector3[]) null;
      Vector3[] path2 = new Vector3[path1.Length];
      for (int index = 0; index < path2.Length; ++index)
        path2[index] = this.CalcGridCenter(path1[index]);
      return path2;
    }

    private void HideGrid()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTacticsSceneRoot, (UnityEngine.Object) null))
        return;
      this.mTacticsSceneRoot.HideGridLayers();
      this.HideCastSkill(true);
    }

    private Vector3 AdjustPositionToCurrentScene(Vector3 pos)
    {
      pos.z += ((Component) this.mTacticsSceneRoot).transform.position.z;
      return pos;
    }

    private void ShowUnitCursorOnCurrent()
    {
      Unit currentUnit = this.mBattle.CurrentUnit;
      if (this.FindUnitController(currentUnit).HasCursor)
        return;
      this.ShowUnitCursor(currentUnit);
    }

    private GridMap<int> CreateCurrentAccessMap()
    {
      Unit currentUnit = this.mBattle.CurrentUnit;
      int x = currentUnit.x;
      int y = currentUnit.y;
      currentUnit.x = currentUnit.startX;
      currentUnit.y = currentUnit.startY;
      GridMap<int> moveMap = this.mBattle.MoveMap;
      currentUnit.x = x;
      currentUnit.y = y;
      return moveMap.clone();
    }

    private void ShowWalkableGrids(GridMap<int> accessMap, int layerIndex)
    {
      GridMap<Color32> grid = new GridMap<Color32>(accessMap.w, accessMap.h);
      GameSettings instance = GameSettings.Instance;
      for (int x = 0; x < accessMap.w; ++x)
      {
        for (int y = 0; y < accessMap.h; ++y)
        {
          if (accessMap.get(x, y) >= 0)
          {
            accessMap.set(x, y, 0);
            if (this.mCurrentUnitStartX == x && this.mCurrentUnitStartY == y)
              grid.set(x, y, Color32.op_Implicit(instance.Colors.StartGrid));
            else
              grid.set(x, y, Color32.op_Implicit(instance.Colors.WalkableArea));
          }
        }
      }
      this.mTacticsSceneRoot.ShowGridLayer(layerIndex, grid, false);
    }

    private void OnUnitDeath(Unit unit)
    {
      if (unit.Side != EUnitSide.Enemy || this.mCurrentQuest.type == QuestTypes.Tutorial)
        return;
      MonoSingleton<GameManager>.Instance.Player.OnEnemyKill(unit.UnitParam.iname, 1);
    }

    private bool ConditionalGotoGimmickState()
    {
      switch (((LogMapEvent) this.mBattle.Logs.Peek).target.EventTrigger.EventType)
      {
        case EEventType.Treasure:
        case EEventType.Gem:
          this.GotoState<SceneBattle.State_PickupGimmick>();
          return true;
        default:
          this.RemoveLog();
          return false;
      }
    }

    private void FinishGimmickState()
    {
      if (this.mBattle.Logs.Num > 0)
        this.GotoState<SceneBattle.State_WaitForLog>();
      else
        this.GotoMapCommand();
    }

    private void AddGoldCount(int num) => this.GoldCount += num;

    private void AddTreasureCount(int num) => this.TreasureCount += num;

    private void AddDispTreasureCount(int num) => this.DispTreasureCount += num;

    public void RestoreTreasureCount(int num)
    {
      this.TreasureCount = num;
      this.DispTreasureCount = num;
    }

    public TacticsUnitController[] GetActiveUnits() => this.mUnitsInBattle.ToArray();

    public TacticsUnitController FindUnitController(Unit unit)
    {
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
      {
        if (this.mTacticsUnits[index].Unit == unit)
          return this.mTacticsUnits[index];
      }
      return (TacticsUnitController) null;
    }

    private void GotoMapCommand()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mInGameMenu, (UnityEngine.Object) null))
        this.mInGameMenu.MultiAutoDisplay(true);
      Unit currentUnit = this.mBattle.CurrentUnit;
      TacticsUnitController unitController = this.FindUnitController(currentUnit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) && unitController.IsJumpCant())
      {
        this.mBattle.CommandWait();
        this.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
      }
      else
      {
        this.Battle.NotifyMapCommand();
        if (this.Battle.IsMultiPlay && !this.Battle.IsUnitAuto(currentUnit) && this.Battle.EntryBattleMultiPlayTimeUp)
        {
          if (this.Battle.MultiPlayDisconnectAutoBattle && !this.Battle.IsVSForceWin)
          {
            this.GotoState_WaitSignal<SceneBattle.State_MapCommandAI>();
          }
          else
          {
            this.mBattle.CommandWait();
            this.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
          }
        }
        else
        {
          for (int index = 0; index < this.mTacticsUnits.Count; ++index)
          {
            this.mTacticsUnits[index].AutoUpdateRotation = true;
            this.mTacticsUnits[index].ResetHPGauge();
          }
          if (currentUnit.IsControl && this.mAutoActivateGimmick && !currentUnit.IsUnitFlag(EUnitFlag.Action) && this.mBattle.CheckGridEventTrigger(currentUnit, EEventTrigger.ExecuteOnGrid))
          {
            this.mAutoActivateGimmick = false;
            this.GotoState_WaitSignal<SceneBattle.State_SelectGridEventV2>();
          }
          else if (!this.Battle.IsUnitAuto(currentUnit))
          {
            bool flag1 = currentUnit.IsEnableMoveCondition();
            bool flag2 = currentUnit.IsEnableActionCondition();
            if (flag1 || flag2)
            {
              if (this.Battle.IsMultiPlay && currentUnit.OwnerPlayerIndex != this.Battle.MyPlayerIndex)
              {
                this.RefreshOnlyMapCommand();
                this.mBattleUI.OnCommandSelectStart();
                this.GotoState_WaitSignal<SceneBattle.State_MapCommandVersus>();
              }
              else
              {
                this.RefreshMapCommands();
                this.mBattleUI.OnCommandSelectStart();
                this.GotoState_WaitSignal<SceneBattle.State_MapCommandV2>();
              }
            }
            else if (!currentUnit.IsEnableSelectDirectionCondition())
            {
              this.mBattle.CommandWait();
              this.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
            }
            else if (this.Battle.IsMultiPlay && currentUnit.OwnerPlayerIndex != this.Battle.MyPlayerIndex)
            {
              this.GotoState_WaitSignal<SceneBattle.State_MapCommandMultiPlay>();
            }
            else
            {
              this.mBattleUI.OnInputDirectionStart();
              this.mBattleUI.CommandWindow.CancelButton.SetActive(false);
              this.GotoState_WaitSignal<SceneBattle.State_InputDirection>();
            }
          }
          else
            this.GotoState_WaitSignal<SceneBattle.State_MapCommandAI>();
        }
      }
    }

    private void GotoMapEnd()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI, (UnityEngine.Object) null))
      {
        this.mBattleUI.OnQuestEnd();
        this.mBattleUI.OnMapEnd();
      }
      this.mBattleUI.OnMapEnd();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null))
        this.mBattleUI_MultiPlay.OnMapEnd();
      this.EndMultiPlayer();
      if (MonoSingleton<GameManager>.Instance.AudienceMode)
        this.GotoState_WaitSignal<SceneBattle.State_AudienceEnd>();
      else
        this.GotoState_WaitSignal<SceneBattle.State_MapEndV2>();
    }

    private void TriggerWinEvent()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEventScript, (UnityEngine.Object) null))
      {
        switch (this.Battle.GetQuestResult())
        {
          case BattleCore.QuestResult.Win:
            this.mEventSequence = this.mEventScript.OnQuestWin();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEventSequence, (UnityEngine.Object) null))
            {
              this.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_PreQuestResult>>();
              return;
            }
            break;
          case BattleCore.QuestResult.Lose:
            this.mEventSequence = this.mEventScript.OnQuestLose();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEventSequence, (UnityEngine.Object) null))
            {
              this.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_PreQuestResult>>();
              return;
            }
            break;
        }
      }
      this.GotoState<SceneBattle.State_PreQuestResult>();
    }

    public int FirstContact => this.mFirstContact;

    public void SubmitResult()
    {
      if (this.mQuestResultSending)
        return;
      this.mQuestResultSending = true;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        long btlId = this.mBattle.BtlID;
        if (this.mCurrentQuest.IsScenario)
          this.SubmitScenarioResult(btlId);
        else
          this.SubmitBattleResult(btlId, this.mCurrentQuest);
      }
      else
      {
        if (this.mBattle.GetQuestResult() == BattleCore.QuestResult.Win)
        {
          MonoSingleton<GameManager>.Instance.Player.MarkQuestCleared(this.mCurrentQuest.iname);
          if (this.IsOrdealQuest && this.mIsFirstPlay)
          {
            this.FirstClearItemId = (string) null;
            if (this.mCurrentQuest.FirstClearItems != null && this.mCurrentQuest.FirstClearItems.Length != 0)
            {
              string firstClearItem = this.mCurrentQuest.FirstClearItems[0];
              if (!string.IsNullOrEmpty(firstClearItem))
              {
                string[] strArray = firstClearItem.Split(',');
                if (strArray != null && strArray.Length != 0)
                {
                  this.FirstClearItemId = strArray[0];
                  ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(this.FirstClearItemId);
                  if (itemParam == null || itemParam.type != EItemType.Unit)
                    this.FirstClearItemId = (string) null;
                }
              }
            }
          }
        }
        BattleCore.RemoveSuspendData();
        this.mQuestResultSent = true;
      }
    }

    public void OnColoRankModify()
    {
      string errMsg = Network.ErrMsg;
      Network.RemoveAPI();
      Network.ResetError();
      UIUtility.SystemMessage((string) null, errMsg, (UIUtility.DialogResultEvent) (go =>
      {
        this.mQuestResultSent = true;
        this.mArenaSubmitMode = SceneBattle.eArenaSubmitMode.FAILED;
      }), systemModal: true);
    }

    private void SubmitResultCallbackImpl(WWWResult www, bool isArenaType = false)
    {
      if (FlowNode_Network.HasCommonError(www) || this.Battle.QuestType == QuestTypes.Tower && TowerErrorHandle.Error())
        return;
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        switch (errCode)
        {
          case Network.EErrCode.GuildRaid_OutOfPeriod:
          case Network.EErrCode.GuildRaid_EnableTimeOutOfPeriod:
          case Network.EErrCode.GuildRaid_AlreadyBeat:
          case Network.EErrCode.GuildRaid_CanNotChallengeByThereIsNoBoss:
            Network.ResetError();
            Network.RemoveAPI();
            BattleCore.RemoveSuspendData();
            UIUtility.SystemMessage(Network.ErrMsg, (UIUtility.DialogResultEvent) (go =>
            {
              this.ExitRequest = SceneBattle.ExitRequests.End;
              this.mQuestResultSent = true;
              this.GotoState_WaitSignal<SceneBattle.State_ExitQuest>();
            }), systemModal: true, systemModalPriority: 10);
            break;
          default:
            switch (errCode - 3900)
            {
              case Network.EErrCode.Success:
                FlowNode_Network.Failed();
                return;
              case Network.EErrCode.Unknown:
              case Network.EErrCode.Version:
                if (isArenaType)
                {
                  this.OnColoRankModify();
                  return;
                }
                FlowNode_Network.Failed();
                return;
              default:
                if (errCode != Network.EErrCode.QuestEnd)
                {
                  if (errCode != Network.EErrCode.ColoRankLower)
                  {
                    if (errCode == Network.EErrCode.WorldRaid_OutOfPeriod)
                    {
                      Network.ResetError();
                      Network.RemoveAPI();
                      BattleCore.RemoveSuspendData();
                      UIUtility.SystemMessage(LocalizedText.Get("sys.WORLDRAID_OUTOFPERIOD_WHEN_BATTLE_END"), (UIUtility.DialogResultEvent) (go =>
                      {
                        this.ExitRequest = SceneBattle.ExitRequests.End;
                        this.mQuestResultSent = true;
                        this.GotoState_WaitSignal<SceneBattle.State_ExitQuest>();
                      }), systemModal: true, systemModalPriority: 10);
                      return;
                    }
                    FlowNode_Network.Retry();
                    return;
                  }
                  goto case Network.EErrCode.Unknown;
                }
                else
                  goto case Network.EErrCode.Success;
            }
        }
      }
      else
      {
        JSON_QuestProgress[] json1 = (JSON_QuestProgress[]) null;
        BattleCore.Json_BtlInspSlot[] sins = (BattleCore.Json_BtlInspSlot[]) null;
        BattleCore.Json_BtlInsp[] levelup_sins = (BattleCore.Json_BtlInsp[]) null;
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonBodyResponse;
        if (this.Battle.QuestType == QuestTypes.Tower)
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          TowerResuponse towerResuponse = instance.TowerResuponse;
          WebAPI.JSON_BodyResponse<Json_TowerBtlResult> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_TowerBtlResult>>(www.text);
          towerResuponse.Deserialize(jsonObject.body.pdeck);
          towerResuponse.arrived_num = jsonObject.body.arrived_num;
          towerResuponse.clear = jsonObject.body.clear;
          if (jsonObject.body.ranking != null)
          {
            towerResuponse.speedRank = jsonObject.body.ranking.spd_rank;
            towerResuponse.techRank = jsonObject.body.ranking.tec_rank;
            towerResuponse.turn_num = jsonObject.body.ranking.turn_num;
            towerResuponse.died_num = jsonObject.body.ranking.died_num;
            towerResuponse.retire_num = jsonObject.body.ranking.retire_num;
            towerResuponse.recover_num = jsonObject.body.ranking.recovery_num;
            towerResuponse.spd_score = jsonObject.body.ranking.spd_score;
            towerResuponse.tec_score = jsonObject.body.ranking.tec_score;
            towerResuponse.ret_score = jsonObject.body.ranking.ret_score;
            towerResuponse.rcv_score = jsonObject.body.ranking.rcv_score;
          }
          QuestParam quest = instance.FindQuest(this.Battle.QuestID);
          if (quest != null && quest.HasMission())
          {
            BattleCore.Record questRecord = this.Battle.GetQuestRecord();
            if (questRecord.takeoverProgressList != null)
            {
              for (int index = 0; index < questRecord.takeoverProgressList.Count; ++index)
                quest.SetMissionValue(index, questRecord.takeoverProgressList[index]);
            }
          }
          if (jsonObject.body.artis != null)
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.artis, true);
          jsonBodyResponse = new WebAPI.JSON_BodyResponse<Json_PlayerDataAll>();
          jsonBodyResponse.body = (Json_PlayerDataAll) jsonObject.body;
        }
        else if (GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.Tower && this.Battle.IsMultiVersus)
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          WebAPI.JSON_BodyResponse<Json_VersusEndEnd> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_VersusEndEnd>>(www.text);
          instance.SetVersusWinCount(jsonObject.body.wincnt);
          instance.SetVersuTowerEndParam(jsonObject.body.rankup == 1, jsonObject.body.win_bonus == 1, jsonObject.body.key, jsonObject.body.floor, jsonObject.body.arravied);
          jsonBodyResponse = new WebAPI.JSON_BodyResponse<Json_PlayerDataAll>();
          jsonBodyResponse.body = (Json_PlayerDataAll) jsonObject.body;
        }
        else if (GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.RankMatch && this.Battle.IsMultiVersus)
        {
          WebAPI.JSON_BodyResponse<Json_VersusEndEnd> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_VersusEndEnd>>(www.text);
          jsonBodyResponse = new WebAPI.JSON_BodyResponse<Json_PlayerDataAll>();
          jsonBodyResponse.body = (Json_PlayerDataAll) jsonObject.body;
        }
        else if (GlobalVars.SelectedMultiPlayVersusType != VERSUS_TYPE.Tower && this.Battle.IsMultiVersus)
        {
          WebAPI.JSON_BodyResponse<Json_VersusEndEnd> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_VersusEndEnd>>(www.text);
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (!instance.IsVSCpuBattle)
            instance.SetVersusWinCount(jsonObject.body.wincnt);
          jsonBodyResponse = new WebAPI.JSON_BodyResponse<Json_PlayerDataAll>();
          jsonBodyResponse.body = (Json_PlayerDataAll) jsonObject.body;
        }
        else if (this.Battle.QuestType == QuestTypes.Raid)
        {
          WebAPI.JSON_BodyResponse<ReqRaidBtlEnd.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRaidBtlEnd.Response>>(www.text);
          jsonBodyResponse = new WebAPI.JSON_BodyResponse<Json_PlayerDataAll>();
          jsonBodyResponse.body = (Json_PlayerDataAll) jsonObject.body;
        }
        else if (this.Battle.QuestType == QuestTypes.GuildRaid)
        {
          WebAPI.JSON_BodyResponse<ReqGuildRaidBtlEnd.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGuildRaidBtlEnd.Response>>(www.text);
          jsonBodyResponse = new WebAPI.JSON_BodyResponse<Json_PlayerDataAll>();
          jsonBodyResponse.body = (Json_PlayerDataAll) jsonObject.body;
        }
        else if (this.Battle.QuestType == QuestTypes.WorldRaid)
        {
          WebAPI.JSON_BodyResponse<ReqWorldRaidBtlEnd.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqWorldRaidBtlEnd.Response>>(www.text);
          if (jsonObject.body.is_mail_cards == 0 && jsonObject.body.cards != null && jsonObject.body.cards.Length > 0)
            MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
          if (jsonObject.body.runes_detail != null && jsonObject.body.runes_detail.Length > 0)
          {
            GameManager instance = MonoSingleton<GameManager>.Instance;
            instance.Player.OnDirtyRuneData();
            instance.Player.SetRuneStorageUsedNum(jsonObject.body.rune_storage_used);
          }
          MonoSingleton<GameManager>.Instance.Player.TrophyData.RegistTrophyStateDictByProgExtra(jsonObject.body.trophyprogs);
          MonoSingleton<GameManager>.Instance.Player.TrophyData.RegistTrophyStateDictByProgExtra(jsonObject.body.bingoprogs);
          jsonBodyResponse = new WebAPI.JSON_BodyResponse<Json_PlayerDataAll>();
          jsonBodyResponse.body = (Json_PlayerDataAll) jsonObject.body;
        }
        else if (this.Battle.QuestType == QuestTypes.GenesisBoss)
        {
          WebAPI.JSON_BodyResponse<ReqGenesisBossBtlEnd.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGenesisBossBtlEnd.Response>>(www.text);
          jsonBodyResponse = new WebAPI.JSON_BodyResponse<Json_PlayerDataAll>();
          jsonBodyResponse.body = (Json_PlayerDataAll) jsonObject.body;
        }
        else if (this.Battle.QuestType == QuestTypes.AdvanceBoss)
        {
          WebAPI.JSON_BodyResponse<ReqAdvanceBossBtlEnd.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqAdvanceBossBtlEnd.Response>>(www.text);
          jsonBodyResponse = new WebAPI.JSON_BodyResponse<Json_PlayerDataAll>();
          jsonBodyResponse.body = (Json_PlayerDataAll) jsonObject.body;
        }
        else
        {
          WebAPI.JSON_BodyResponse<Json_BtlComEnd> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_BtlComEnd>>(www.text);
          MonoSingleton<GameManager>.Instance.Player.TrophyData.RegistTrophyStateDictByProgExtra(jsonObject.body.trophyprogs);
          MonoSingleton<GameManager>.Instance.Player.TrophyData.RegistTrophyStateDictByProgExtra(jsonObject.body.bingoprogs);
          json1 = jsonObject.body.quests;
          if (jsonObject.body.quest_ranking != null)
          {
            this.mIsRankingQuestNewScore = jsonObject.body.quest_ranking.IsNewScore;
            this.mRankingQuestNewRank = jsonObject.body.quest_ranking.rank;
            this.mIsRankingQuestJoinReward = jsonObject.body.quest_ranking.IsJoinReward;
          }
          this.FirstClearItemId = (string) null;
          if (jsonObject.body.fclr_items != null && jsonObject.body.fclr_items.Length != 0)
          {
            this.FirstClearItemId = jsonObject.body.fclr_items[0].iname;
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(this.FirstClearItemId);
            if (itemParam == null || itemParam.type != EItemType.Unit)
              this.FirstClearItemId = (string) null;
          }
          if (jsonObject.body.cards != null)
          {
            for (int index = 0; index < jsonObject.body.cards.Length; ++index)
            {
              MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
              if (jsonObject.body.cards[index].IsGetUnit)
              {
                FlowNode_ConceptCardGetUnit.AddConceptCardData(ConceptCardData.CreateConceptCardDataForDisplay(jsonObject.body.cards[index].iname));
                this.mBattle.AddReward(RewardType.Unit, jsonObject.body.cards[index].get_unit);
              }
            }
          }
          this.m_IsCardSendMail = jsonObject.body.is_mail_cards == 1;
          if (this.mCurrentQuest != null && (this.mCurrentQuest.type == QuestTypes.Multi || this.mCurrentQuest.type == QuestTypes.MultiGps) && jsonObject.body.is_quest_out_of_period == 1)
          {
            MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.IsConnectedInRoom())
              instance.Disconnect();
          }
          sins = jsonObject.body.sins;
          levelup_sins = jsonObject.body.levelup_sins;
          if (jsonObject.body.guildraid_bp_charge > 0)
            GuildRaidManager.SetNotifyPush();
          if (jsonObject.body.runes_detail != null && jsonObject.body.runes_detail.Length > 0)
          {
            List<RuneData> _datas = new List<RuneData>();
            foreach (Json_RuneData json2 in jsonObject.body.runes_detail)
            {
              RuneData runeData = new RuneData();
              runeData.Deserialize(json2);
              _datas.Add(runeData);
            }
            this.mBattle.SetRuneReward(_datas);
          }
          jsonBodyResponse = new WebAPI.JSON_BodyResponse<Json_PlayerDataAll>();
          jsonBodyResponse.body = (Json_PlayerDataAll) jsonObject.body;
        }
        if (jsonBodyResponse.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          List<ConceptCardData> conceptCardDataList = new List<ConceptCardData>();
          for (int index = 0; index < this.Battle.Units.Count; ++index)
          {
            if (this.Battle.Units[index] != null && this.Battle.Units[index].UnitData.MainConceptCard != null && this.Battle.Units[index].IsPartyMember)
              conceptCardDataList.Add(this.Battle.Units[index].UnitData.MainConceptCard);
          }
          try
          {
            GameManager instance = MonoSingleton<GameManager>.Instance;
            VersusCoinParam versusCoinParam = instance.GetVersusCoinParam(this.CurrentQuest.iname);
            int multiCoin1 = instance.Player.MultiCoin;
            int num = 0;
            if (versusCoinParam != null)
              num = instance.Player.GetItemAmount(versusCoinParam.coin_iname);
            string str = (string) null;
            int old_point = 0;
            UnitData rentalUnit1 = instance.Player.GetRentalUnit();
            if (rentalUnit1 != null)
            {
              str = rentalUnit1.UnitID;
              old_point = rentalUnit1.RentalFavoritePoint;
            }
            instance.Deserialize(jsonBodyResponse.body.player);
            instance.Deserialize(jsonBodyResponse.body.units);
            instance.Deserialize(jsonBodyResponse.body.items);
            MonoSingleton<GameManager>.Instance.Player.GuildTrophyData.OverwriteGuildTrophyProgress(jsonBodyResponse.body.guild_trophies);
            instance.Player.Deserialize(jsonBodyResponse.body.story_ex_challenge);
            instance.Player.Deserialize(jsonBodyResponse.body.party_decks);
            if (!string.IsNullOrEmpty(str))
            {
              UnitData rentalUnit2 = instance.Player.GetRentalUnit();
              if (rentalUnit2 != null && rentalUnit2.UnitID == str)
                UnitRentalParam.SendNotificationIsNeed(rentalUnit2.UnitID, old_point, rentalUnit2.RentalFavoritePoint);
            }
            if (jsonBodyResponse.body.artifacts != null)
            {
              instance.Deserialize(jsonBodyResponse.body.artifacts, true);
              instance.Player.UpdateArtifactOwner();
            }
            if (json1 != null)
              instance.Deserialize(json1);
            if (jsonBodyResponse.body.mails != null)
              instance.Deserialize(jsonBodyResponse.body.mails);
            if (jsonBodyResponse.body.fuids != null && (this.mCurrentQuest.type == QuestTypes.Multi || this.mCurrentQuest.type == QuestTypes.MultiGps || this.mCurrentQuest.IsMultiTower))
              instance.Deserialize(jsonBodyResponse.body.fuids);
            if (jsonBodyResponse.body != null && this.mCurrentQuest.type == QuestTypes.Raid)
            {
              instance.RaidBtlEndParam = (ReqRaidBtlEnd.Response) jsonBodyResponse.body;
              if (instance.RaidBtlEndParam != null && instance.RaidBtlEndParam.raid_battle_reward != null)
              {
                RaidRewardData raidRewardData = new RaidRewardData();
                raidRewardData.Deserialize(RaidRewardKind.Beat, instance.RaidBtlEndParam.raid_battle_reward);
                for (int index = 0; index < raidRewardData.Rewards.Length; ++index)
                  this.mBattle.SetGiftReward(raidRewardData.Rewards[index]);
              }
            }
            if (jsonBodyResponse.body != null && this.mCurrentQuest.IsGuildRaid)
              instance.RecentGuildRaidBtlEndParam = (ReqGuildRaidBtlEnd.Response) jsonBodyResponse.body;
            if (jsonBodyResponse.body != null && this.mCurrentQuest.IsWorldRaid)
              instance.RecentWorldRaidBtlEndParam = (ReqWorldRaidBtlEnd.Response) jsonBodyResponse.body;
            if (this.mCurrentQuest.type == QuestTypes.GenesisBoss || this.mCurrentQuest.type == QuestTypes.AdvanceBoss)
            {
              if (this.mCurrentQuest.type == QuestTypes.AdvanceBoss)
                this.mBattle.SetAdvanceBossResult(((ReqAdvanceBossBtlEnd.Response) jsonBodyResponse.body).advance_boss_reward);
              else
                this.mBattle.SetGenesisBossResult(((ReqGenesisBossBtlEnd.Response) jsonBodyResponse.body).genesis_boss_reward);
            }
            if (this.mCurrentQuest.IsVersus && !string.IsNullOrEmpty(jsonBodyResponse.body.fu_status))
            {
              MyPhoton pt = PunMonoSingleton<MyPhoton>.Instance;
              JSON_MyPhotonPlayerParam photonPlayerParam = pt.GetMyPlayersStarted().Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerIndex != pt.MyPlayerIndex));
              if (photonPlayerParam != null)
                instance.Deserialize(new Json_MultiFuids[1]
                {
                  new Json_MultiFuids()
                  {
                    fuid = photonPlayerParam.FUID,
                    status = jsonBodyResponse.body.fu_status
                  }
                });
            }
            int multiCoin2 = instance.Player.MultiCoin;
            if (this.Battle.IsMultiPlay)
            {
              BattleCore.Record questRecord = this.Battle.GetQuestRecord();
              questRecord.multicoin = (OInt) (multiCoin2 - multiCoin1);
              if (versusCoinParam != null)
              {
                int itemAmount = instance.Player.GetItemAmount(versusCoinParam.coin_iname);
                questRecord.pvpcoin = (OInt) (itemAmount - num);
              }
            }
            this.mBattle.SetInspSkillIns(sins);
            this.mBattle.SetInspSkillLvUp(levelup_sins);
            JukeBoxWindow.UnlockMusic(jsonBodyResponse.body.bgms);
            if (RuneUtility.CountRuneNum(this.mBattle.GetQuestRecord().items) > 0)
            {
              instance.Player.OnDirtyRuneData();
              instance.Player.SetRuneStorageUsedNum(jsonBodyResponse.body.rune_storage_used);
            }
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Failed();
            return;
          }
          if (isArenaType)
          {
            WebAPI.JSON_BodyResponse<Json_ArenaPlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ArenaPlayerDataAll>>(www.text);
            if (jsonObject.body == null)
            {
              FlowNode_Network.Retry();
              return;
            }
            ArenaBattleResponse arenaBattleResponse = new ArenaBattleResponse();
            arenaBattleResponse.Deserialize(jsonObject.body.btlres);
            GlobalVars.ResultArenaBattleResponse = arenaBattleResponse;
            this.mArenaSubmitMode = SceneBattle.eArenaSubmitMode.SUCCESS;
          }
          if (this.mBattle.IsAutoBattle)
            GameUtility.SetDefaultSleepSetting();
          if (this.IsPlayingMultiQuest)
            this.mFirstContact = jsonBodyResponse.body.first_contact;
          MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
          this.mQuestResultSent = true;
          Network.RemoveAPI();
          this.TrophyLvupCheck();
          for (int index1 = 0; index1 < conceptCardDataList.Count; ++index1)
          {
            if (jsonBodyResponse.body.units != null)
            {
              for (int index2 = 0; index2 < jsonBodyResponse.body.units.Length; ++index2)
              {
                if (jsonBodyResponse.body.units[index2].concept_cards != null)
                {
                  for (int index3 = 0; index3 < jsonBodyResponse.body.units[index2].concept_cards.Length; ++index3)
                  {
                    if (!jsonBodyResponse.body.units[index2].concept_cards[index3].IsEmptyDummyData && (long) conceptCardDataList[index1].UniqueID == jsonBodyResponse.body.units[index2].concept_cards[index3].iid)
                      MonoSingleton<GameManager>.Instance.Player.UpdateConceptCardTrustMaxTrophy(jsonBodyResponse.body.units[index2].concept_cards[index3].iname, jsonBodyResponse.body.units[index2].concept_cards[index3].trust);
                  }
                }
              }
            }
          }
          if (this.mIsForceEndQuest)
            MonoSingleton<GameManager>.Instance.Player.SetQuestMissionFlags(this.mCurrentQuest.iname, this.mBattle.GetQuestRecord().bonusFlags);
          BattleCore.RemoveSuspendData();
        }
      }
    }

    private void SubmitGvGResultCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        switch (errCode)
        {
          case Network.EErrCode.GvG_BtlOutOfPeriod:
          case Network.EErrCode.GvG_AlreadyBeat:
          case Network.EErrCode.GvG_OtherUserAttacking:
          case Network.EErrCode.GvG_IsCoolTime:
          case Network.EErrCode.GvG_BattleHalfTime:
label_7:
            Network.ResetError();
            Network.RemoveAPI();
            BattleCore.RemoveSuspendData();
            UIUtility.SystemMessage(Network.ErrMsg, (UIUtility.DialogResultEvent) (go =>
            {
              ProgressWindow.Close();
              this.ExitRequest = SceneBattle.ExitRequests.End;
              this.mQuestResultSent = true;
              this.GotoState_WaitSignal<SceneBattle.State_ExitQuest>();
            }), systemModal: true, systemModalPriority: 10);
            break;
          case Network.EErrCode.GvG_HasCapture:
            GlobalVars.ResultGvGCapture.Set(true);
            Network.ResetError();
            Network.RemoveAPI();
            BattleCore.RemoveSuspendData();
            UIUtility.SystemMessage(Network.ErrMsg, (UIUtility.DialogResultEvent) (go =>
            {
              ProgressWindow.Close();
              this.ExitRequest = SceneBattle.ExitRequests.End;
              this.mQuestResultSent = true;
              this.GotoState_WaitSignal<SceneBattle.State_ExitQuest>();
            }), systemModal: true, systemModalPriority: 10);
            break;
          default:
            switch (errCode - 28001)
            {
              case Network.EErrCode.Success:
              case Network.EErrCode.Unknown:
              case Network.EErrCode.AssetVersion:
                goto label_7;
              default:
                if (errCode != Network.EErrCode.Guild_NotJoined)
                {
                  FlowNode_Network.Retry();
                  return;
                }
                goto label_7;
            }
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqGvGBattleExec.Response> jsonBodyResponse = new WebAPI.JSON_BodyResponse<ReqGvGBattleExec.Response>();
        WebAPI.JSON_BodyResponse<ReqGvGBattleExec.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGvGBattleExec.Response>>(www.text);
        jsonBodyResponse.body = jsonObject.body;
        if (jsonBodyResponse.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          ReqGvGBattleExec.Response body = jsonBodyResponse.body;
          GlobalVars.ResultGvGCapture.Set(body.is_capture != 0);
          MonoSingleton<GameManager>.Instance.Player.GuildTrophyData.OverwriteGuildTrophyProgress(body.guild_trophies);
          this.mArenaSubmitMode = SceneBattle.eArenaSubmitMode.SUCCESS;
          if (this.mBattle.IsAutoBattle)
            GameUtility.SetDefaultSleepSetting();
          this.mQuestResultSent = true;
          Network.RemoveAPI();
          BattleCore.RemoveSuspendData();
        }
      }
    }

    private void SubmitResultCallback(WWWResult www) => this.SubmitResultCallbackImpl(www);

    private void SubmitArenaResultCallback(WWWResult www)
    {
      this.SubmitResultCallbackImpl(www, true);
    }

    private void SubmitBattleResult(long btlid, QuestParam quest)
    {
      int time = 0;
      bool isMultiPlay = PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay;
      BattleCore.Record questRecord = this.mBattle.GetQuestRecord();
      BtlResultTypes result = BtlResultTypes.Win;
      if (questRecord.result == BattleCore.QuestResult.Pending)
      {
        result = BtlResultTypes.Retire;
        if (this.mRevertQuestNewIfRetire)
        {
          result = BtlResultTypes.Cancel;
          MonoSingleton<GameManager>.Instance.Player.SetQuestState(quest.name, QuestStates.New);
        }
      }
      else if (questRecord.result == BattleCore.QuestResult.Draw)
        result = BtlResultTypes.Draw;
      else if (questRecord.result != BattleCore.QuestResult.Win)
        result = BtlResultTypes.Lose;
      if (questRecord.result == BattleCore.QuestResult.Win)
        MonoSingleton<GameManager>.Instance.Player.MarkQuestCleared(this.mCurrentQuest.iname);
      if (questRecord.result == BattleCore.QuestResult.Win)
        MonoSingleton<GameManager>.Instance.Player.IncrementQuestChallangeNumDaily(this.mCurrentQuest.iname);
      if (questRecord.drops == null)
        questRecord.drops = new OInt[0];
      if (questRecord.item_steals == null)
        questRecord.item_steals = new OInt[0];
      if (questRecord.gold_steals == null)
        questRecord.gold_steals = new OInt[0];
      int[] beats = new int[questRecord.drops.Length];
      for (int index = 0; index < questRecord.drops.Length; ++index)
        beats[index] = (int) questRecord.drops[index];
      int[] itemSteals = new int[questRecord.item_steals.Length];
      for (int index = 0; index < questRecord.item_steals.Length; ++index)
        itemSteals[index] = (int) questRecord.item_steals[index];
      int[] goldSteals = new int[questRecord.gold_steals.Length];
      for (int index = 0; index < questRecord.gold_steals.Length; ++index)
        goldSteals[index] = (int) questRecord.gold_steals[index];
      int[] missions = new int[questRecord.bonusCount];
      for (int index = 0; index < missions.Length; ++index)
        missions[index] = (questRecord.bonusFlags & 1 << index) == 0 ? 0 : 1;
      int[] missions_log = new int[questRecord.bonusCount];
      for (int index = 0; index < missions_log.Length; ++index)
        missions_log[index] = (questRecord.allBonusFlags & 1 << index) == 0 ? 0 : 1;
      this.UpdateTrophy();
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      string trophy_progs;
      string bingo_progs;
      instance1.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
      if (quest.type == QuestTypes.Arena)
      {
        ArenaPlayer selectedArenaPlayer = (ArenaPlayer) GlobalVars.SelectedArenaPlayer;
        Network.RequestAPI((WebAPI) new ReqBtlComEnd(selectedArenaPlayer.FUID, selectedArenaPlayer.ArenaRank, MonoSingleton<GameManager>.Instance.Player.ArenaRank, result, beats, itemSteals, goldSteals, missions, missions_log, (string[]) null, questRecord.used_items, new Network.ResponseCallback(this.SubmitArenaResultCallback), BtlEndTypes.colo, trophy_progs, bingo_progs));
      }
      else if (quest.IsGvG)
      {
        JSON_GvGBattleEndParam gvGbattleEndParam = this.CreateGvGbattleEndParam(result);
        Network.RequestAPI((WebAPI) new ReqGvGBattleExec((int) GlobalVars.GvGNodeId, MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid, (int) GlobalVars.GvGGroupId, gvGbattleEndParam, new Network.ResponseCallback(this.SubmitGvGResultCallback)));
      }
      else if (quest.type == QuestTypes.Tower)
      {
        int[] missions_value = new int[questRecord.bonusCount];
        for (int index = 0; index < missions_value.Length && index < questRecord.takeoverProgressList.Count; ++index)
          missions_value[index] = questRecord.takeoverProgressList[index];
        int round = (int) instance1.TowerResuponse.round;
        TowerFloorParam towerFloorParam = instance1.TowerResuponse.GetCurrentFloor() ?? instance1.FindTowerFloor(quest.iname);
        byte floor = towerFloorParam == null ? (byte) 0 : towerFloorParam.floor;
        Network.RequestAPI((WebAPI) new ReqTowerBtlComEnd(btlid, this.Battle.Player.ToArray(), this.Battle.Enemys.ToArray(), this.Battle.ActionCountTotal, round, floor, result, this.Battle.Map[0].mRandDeckResult, new Network.ResponseCallback(this.SubmitResultCallback), trophy_progs, bingo_progs, missions, missions_log, missions_value, questRecord));
      }
      else if (quest.type == QuestTypes.VersusFree || quest.type == QuestTypes.VersusRank)
      {
        MyPhoton pt = PunMonoSingleton<MyPhoton>.Instance;
        List<int> finishHp1 = this.Battle.GetFinishHp(EUnitSide.Player);
        List<int> finishHp2 = this.Battle.GetFinishHp(EUnitSide.Enemy);
        int deadCount = this.Battle.GetDeadCount(EUnitSide.Enemy);
        List<int> intList = new List<int>();
        PartyData party = MonoSingleton<GameManager>.Instance.Player.Partys[7];
        for (int index = 0; index < party.MAX_UNIT; ++index)
          intList.Add(PlayerPrefsUtility.GetInt(PlayerPrefsUtility.VERSUS_ID_KEY + (object) index));
        if (instance1.IsVSCpuBattle)
        {
          Network.RequestAPI((WebAPI) new ReqVersusCpuEnd(btlid, result, this.Battle.VersusTurnCount, finishHp1.ToArray(), finishHp2.ToArray(), this.Battle.TotalDamages, this.Battle.TotalDamagesTaken, this.Battle.TotalHeal, deadCount, intList.ToArray(), new Network.ResponseCallback(this.SubmitResultCallback), trophy_progs, bingo_progs));
        }
        else
        {
          JSON_MyPhotonPlayerParam photonPlayerParam = pt.GetMyPlayersStarted().Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerIndex != pt.MyPlayerIndex));
          Network.RequestAPI((WebAPI) new ReqVersusEnd(btlid, result, photonPlayerParam.UID, photonPlayerParam.FUID, this.Battle.VersusTurnCount, finishHp1.ToArray(), finishHp2.ToArray(), this.Battle.TotalDamages, this.Battle.TotalDamagesTaken, this.Battle.TotalHeal, deadCount, intList.ToArray(), new Network.ResponseCallback(this.SubmitResultCallback), GlobalVars.SelectedMultiPlayVersusType, trophy_progs, bingo_progs));
        }
      }
      else if (quest.type == QuestTypes.RankMatch)
      {
        MyPhoton pt = PunMonoSingleton<MyPhoton>.Instance;
        List<int> finishHp3 = this.Battle.GetFinishHp(EUnitSide.Player);
        List<int> finishHp4 = this.Battle.GetFinishHp(EUnitSide.Enemy);
        int deadCount = this.Battle.GetDeadCount(EUnitSide.Enemy);
        List<int> intList = new List<int>();
        PartyData party = MonoSingleton<GameManager>.Instance.Player.Partys[10];
        for (int index = 0; index < party.MAX_UNIT; ++index)
          intList.Add(PlayerPrefsUtility.GetInt(PlayerPrefsUtility.RANKMATCH_ID_KEY + (object) index));
        JSON_MyPhotonPlayerParam photonPlayerParam = pt.GetMyPlayersStarted().Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerIndex != pt.MyPlayerIndex));
        instance1.Player.RankMatchResult = result;
        if (instance1.Player.RankMatchBattlePoint > 0)
        {
          if (result == BtlResultTypes.Win || result == BtlResultTypes.Lose || result == BtlResultTypes.Draw)
            instance1.Player.IncrementRankMatchMission(RankMatchMissionType.Battle);
          if (result == BtlResultTypes.Win)
          {
            instance1.Player.IncrementRankMatchMission(RankMatchMissionType.Win);
            instance1.Player.SetMaxProgRankMatchMission(RankMatchMissionType.StreakWin, instance1.Player.RankMatchStreakWin + 1);
            MyPhoton.MyRoom currentRoom = pt.GetCurrentRoom();
            if (currentRoom != null)
            {
              JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
              int num1 = 0;
              if (myPhotonRoomParam != null && myPhotonRoomParam.players != null && myPhotonRoomParam.players.Length > 1)
              {
                for (int index = 0; index < myPhotonRoomParam.players.Length; ++index)
                {
                  if (myPhotonRoomParam.players[index].playerID != pt.GetMyPlayer().playerID)
                  {
                    num1 = myPhotonRoomParam.players[index].rankmatch_score;
                    break;
                  }
                }
              }
              VersusRankParam versusRankParam = instance1.GetVersusRankParam(instance1.RankMatchScheduleId);
              VersusRankClassParam versusRankClass = instance1.GetVersusRankClass(instance1.RankMatchScheduleId, instance1.Player.RankMatchClass);
              if (versusRankParam != null && versusRankClass != null)
              {
                int num2 = Mathf.Clamp((int) Math.Truncate((double) versusRankParam.WinPointBase * ((double) (num1 - instance1.Player.RankMatchScore) * 0.001 + 1.0)), versusRankClass.WinPointMin, versusRankClass.WinPointMax);
                instance1.Player.SetMaxProgRankMatchMission(RankMatchMissionType.GetPoint, instance1.Player.RankMatchScore + num2);
              }
            }
          }
        }
        string missionProgressString = instance1.Player.GetMissionProgressString();
        Network.RequestAPI((WebAPI) new ReqRankMatchEnd(btlid, result, photonPlayerParam.UID, photonPlayerParam.FUID, this.Battle.VersusTurnCount, finishHp3.ToArray(), finishHp4.ToArray(), this.Battle.TotalDamages, this.Battle.TotalDamagesTaken, this.Battle.TotalHeal, deadCount, intList.ToArray(), new Network.ResponseCallback(this.SubmitResultCallback), trophy_progs, bingo_progs, missionProgressString));
      }
      else
      {
        MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
        List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance2.GetMyPlayersStarted();
        string[] fuid = (string[]) null;
        if (GlobalVars.LastQuestResult.Get() == BattleCore.QuestResult.Win && myPlayersStarted != null)
        {
          fuid = new string[myPlayersStarted.Count];
          int num = 0;
          for (int index = 0; index < myPlayersStarted.Count; ++index)
          {
            JSON_MyPhotonPlayerParam photonPlayerParam = myPlayersStarted[index];
            if (photonPlayerParam != null && photonPlayerParam.playerIndex != instance2.MyPlayerIndex && !string.IsNullOrEmpty(photonPlayerParam.FUID))
              fuid[num++] = photonPlayerParam.FUID;
          }
        }
        SupportData supportData = MonoSingleton<GameManager>.Instance.Player.Supports.Find((Predicate<SupportData>) (f => f.FUID == GlobalVars.SelectedFriendID));
        if (this.Battle.IsMultiTower)
        {
          List<int> finishHp = this.Battle.GetFinishHp(EUnitSide.Player);
          List<string> playerName = this.Battle.GetPlayerName();
          Network.RequestAPI((WebAPI) new ReqBtlMultiTwEnd(btlid, time, result, finishHp.ToArray(), playerName.ToArray(), fuid, new Network.ResponseCallback(this.SubmitResultCallback), trophy_progs, bingo_progs));
        }
        else if (this.Battle.IsRankingQuest && questRecord.result == BattleCore.QuestResult.Win)
        {
          RankingQuestParam rankingQuestParam = this.Battle.GetRankingQuestParam();
          int main_score = 0;
          if (rankingQuestParam.type == RankingQuestType.ActionCount)
            main_score = this.Battle.ActionCountTotal;
          int sub_score = this.Battle.CalcPlayerUnitsTotalParameter();
          string rankingQuestEndParam = ReqBtlComEnd.CreateRankingQuestEndParam(main_score, sub_score);
          Network.RequestAPI((WebAPI) new ReqBtlComEnd(btlid, time, result, beats, itemSteals, goldSteals, missions, missions_log, fuid, questRecord.used_items, new Network.ResponseCallback(this.SubmitResultCallback), !isMultiPlay ? BtlEndTypes.com : BtlEndTypes.multi, trophy_progs, bingo_progs, supportData == null ? 0 : (int) supportData.UnitElement, rankingQuestEndParam, record: questRecord));
        }
        else if (quest.type == QuestTypes.Raid)
          Network.RequestAPI((WebAPI) new ReqRaidBtlEnd(btlid, result, GlobalVars.CurrentRaidBossId.Get(), this.Battle.Enemys, questRecord.used_items, new Network.ResponseCallback(this.SubmitResultCallback), questRecord, trophy_progs, bingo_progs));
        else if (quest.type == QuestTypes.GuildRaid)
        {
          int gid = 0;
          if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild != null && MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined)
            gid = MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid;
          Dictionary<OString, OInt> usedItems = (Dictionary<OString, OInt>) null;
          if (GlobalVars.CurrentBattleType.Get() == GuildRaidBattleType.Main)
            usedItems = questRecord.used_items;
          int remainingActionCount = this.Battle.GetRemainingActionCount(this.Battle.CurrentMap.LoseMonitorCondition);
          Network.RequestAPI((WebAPI) new ReqGuildRaidBtlEnd(gid, btlid, result, GlobalVars.CurrentRaidBossId.Get(), remainingActionCount, this.Battle.Enemys, usedItems, new Network.ResponseCallback(this.SubmitResultCallback), questRecord, trophy_progs, bingo_progs));
        }
        else if (quest.IsWorldRaid)
          Network.RequestAPI((WebAPI) new ReqWorldRaidBtlEnd(btlid, result, GlobalVars.CurrentRaidBossIname.Get(), this.Battle.GetDamageList().ToArray(), this.Battle.Enemys, questRecord.used_items, new Network.ResponseCallback(this.SubmitResultCallback), questRecord, trophy_progs, bingo_progs));
        else if (quest.type == QuestTypes.GenesisBoss)
          Network.RequestAPI((WebAPI) new ReqGenesisBossBtlEnd(btlid, result, this.Battle.AllUnits, this.Battle.NpcStartIndex, missions, missions_log, questRecord.used_items, new Network.ResponseCallback(this.SubmitResultCallback), questRecord, trophy_progs, bingo_progs));
        else if (quest.type == QuestTypes.AdvanceBoss)
        {
          Network.RequestAPI((WebAPI) new ReqAdvanceBossBtlEnd(btlid, result, this.Battle.AllUnits, this.Battle.NpcStartIndex, missions, missions_log, questRecord.used_items, new Network.ResponseCallback(this.SubmitResultCallback), questRecord, trophy_progs, bingo_progs));
        }
        else
        {
          BtlEndTypes apiType = !isMultiPlay ? BtlEndTypes.com : BtlEndTypes.multi;
          if (quest.type == QuestTypes.Ordeal)
            apiType = BtlEndTypes.ordeal;
          bool? is_skip = new bool?();
          if (quest.type == QuestTypes.Tutorial)
            is_skip = new bool?(GlobalVars.IsSkipQuestDemo);
          int num = 0;
          if (result == BtlResultTypes.Win && this.Battle.ContinueCount == 0)
          {
            num = (int) MonoSingleton<GameManager>.Instance.GetQuestPlayTime();
            if (num > 0)
              this.mCurrentQuest.best_clear_time = this.mCurrentQuest.best_clear_time <= 0 ? num : Math.Min(this.mCurrentQuest.best_clear_time, num);
          }
          MonoSingleton<GameManager>.Instance.ResetQuestPlayTime();
          Network.RequestAPI((WebAPI) new ReqBtlComEnd(btlid, time, result, beats, itemSteals, goldSteals, missions, missions_log, fuid, questRecord.used_items, new Network.ResponseCallback(this.SubmitResultCallback), apiType, trophy_progs, bingo_progs, supportData == null ? 0 : (int) supportData.UnitElement, is_skip: is_skip, record: questRecord, time_per_lap: num));
        }
      }
      MonoSingleton<GameManager>.Instance.Player.TrophyData.ResetMissionClearAt();
    }

    private void SubmitScenarioResult(long btlid)
    {
      Network.RequestAPI((WebAPI) new ReqBtlComEnd(btlid, 0, BtlResultTypes.Win, (int[]) null, (int[]) null, (int[]) null, (int[]) null, (int[]) null, (string[]) null, (Dictionary<OString, OInt>) null, new Network.ResponseCallback(this.SubmitResultCallback), BtlEndTypes.com));
      MonoSingleton<GameManager>.Instance.Player.MarkQuestCleared(this.mCurrentQuest.iname);
    }

    public void ForceEndQuest()
    {
      this.mIsForceEndQuest = true;
      this.mExecDisconnected = true;
      if (this.Battle.IsMultiTower || this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.Multi)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
          instance.Disconnect();
      }
      if (this.IsInState<SceneBattle.State_ExitQuest>())
        return;
      this.GotoState<SceneBattle.State_ExitQuest>();
    }

    private void UpdateTrophy()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      BattleCore.QuestResult questResult = this.mBattle.GetQuestResult();
      if (this.mCurrentQuest.type == QuestTypes.Arena)
      {
        if (this.mBattle.GetQuestRecord().result == BattleCore.QuestResult.Win)
          player.OnQuestWin(this.mCurrentQuest.iname);
        else
          player.OnQuestLose(this.mCurrentQuest.iname);
      }
      else
      {
        switch (questResult)
        {
          case BattleCore.QuestResult.Win:
            BattleCore.Record questRecord = this.Battle.GetQuestRecord();
            player.OnQuestWin(this.mCurrentQuest.iname, questRecord);
            for (int index = 0; index < questRecord.items.Count; ++index)
            {
              if (questRecord.items[index].itemParam != null)
                player.OnItemQuantityChange(questRecord.items[index].itemParam.iname, 1);
            }
            break;
          case BattleCore.QuestResult.Lose:
            player.OnQuestLose(this.mCurrentQuest.iname);
            break;
        }
        if (!this.mCurrentQuest.IsMultiTower)
          return;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        int mtClearedMaxFloor = MonoSingleton<GameManager>.Instance.GetMTClearedMaxFloor();
        int selectedMultiTowerFloor = GlobalVars.SelectedMultiTowerFloor;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
          return;
        List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
        if (roomPlayerList == null)
          return;
        for (int index = 0; index < roomPlayerList.Count; ++index)
        {
          JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index].json);
          if (photonPlayerParam != null && photonPlayerParam.playerIndex != instance.MyPlayerIndex && selectedMultiTowerFloor <= mtClearedMaxFloor && selectedMultiTowerFloor > photonPlayerParam.mtClearedFloor)
            player.OnMultiTowerHelp();
        }
      }
    }

    private void TrophyLvupCheck()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (this.mStartPlayerLevel >= player.Lv)
        return;
      player.OnPlayerLevelChange(player.Lv - this.mStartPlayerLevel);
    }

    public void ForceEndQuestInArena()
    {
      if (this.Battle.IsArenaSkip)
        return;
      this.Pause(false);
      GlobalEvent.Invoke("CLOSE_QUESTMENU", (object) this);
      if (this.Battle.GetQuestResult() != BattleCore.QuestResult.Pending)
        return;
      this.Battle.IsArenaSkip = true;
      SRPG_TouchInputModule.LockInput();
      this.GotoState_WaitSignal<SceneBattle.State_ArenaSkipWait>();
    }

    public bool ForceEndQuestInGvG()
    {
      if (this.Battle.IsArenaSkip)
        return false;
      this.Pause(false);
      GlobalEvent.Invoke("CLOSE_QUESTMENU", (object) this);
      if (this.Battle.ArenaCalcResult == BattleCore.QuestResult.Pending)
        return false;
      this.Battle.IsArenaSkip = true;
      SRPG_TouchInputModule.LockInput();
      this.GotoState<SceneBattle.State_ArenaSkipWait>();
      return true;
    }

    public QuestResultData ResultData => this.mSavedResult;

    private void SaveResult()
    {
      this.mSavedResult = new QuestResultData(MonoSingleton<GameManager>.Instance.Player, this.mCurrentQuest.clear_missions, this.mBattle.GetQuestRecord(), this.Battle.AllUnits, this.mIsFirstWin);
    }

    private void OFFLINE_APPLY_QUEST_RESULT()
    {
      if (this.mCurrentQuest.IsScenario)
      {
        MonoSingleton<GameManager>.Instance.Player.MarkQuestCleared(this.mCurrentQuest.iname);
      }
      else
      {
        BattleCore.Record questRecord = this.Battle.GetQuestRecord();
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (questRecord.result != BattleCore.QuestResult.Win)
          return;
        player.GainGold((int) questRecord.gold);
        for (int index = 0; index < this.Battle.Player.Count; ++index)
          this.Battle.Player[index].UnitData.GainExp((int) questRecord.unitexp, player.Lv);
        if (questRecord.items != null)
        {
          for (int index = 0; index < questRecord.items.Count; ++index)
          {
            if (questRecord.items[index].itemParam != null)
              player.GainItem(questRecord.items[index].itemParam.iname, 1);
          }
        }
        MonoSingleton<GameManager>.Instance.Player.MarkQuestCleared(this.mCurrentQuest.iname);
        MonoSingleton<GameManager>.Instance.Player.SetQuestMissionFlags(this.mCurrentQuest.iname, questRecord.bonusFlags);
      }
    }

    private void ExitScene()
    {
      if (this.mSceneExiting)
        return;
      this.mSceneExiting = true;
      MonoSingleton<GameManager>.Instance.Player.UpdateUnitTrophyStates(false);
      if (this.mCurrentQuest.type == QuestTypes.Tutorial)
      {
        MyMetaps.TrackTutorialPoint(this.mCurrentQuest.iname);
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "TUTORIAL_EXIT");
      }
      else
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "EXIT");
    }

    public void Pause(bool flag)
    {
      if (flag)
        ++this.mPauseReqCount;
      else
        this.mPauseReqCount = Math.Max(0, this.mPauseReqCount - 1);
      TimeManager.SetTimeScale(TimeManager.TimeScaleGroups.Game, this.mPauseReqCount <= 0 ? 1f : 0.0f);
    }

    public bool IsPause() => this.mPauseReqCount != 0;

    public void CleanUpMultiPlay()
    {
      this.CleanupGoodJob();
      bool flag = true;
      if (this.Battle.IsMultiTower && this.Battle.GetQuestResult() != BattleCore.QuestResult.Lose)
        flag = false;
      if (this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.Multi && HomeWindow.GetRestorePoint() == RestorePoints.MP)
        flag = false;
      if (flag)
        PunMonoSingleton<MyPhoton>.Instance.Reset();
      DebugUtility.Log("CleanUpMultiPlay done.");
    }

    private SceneBattle.PosRot GetCameraOffset_Unit()
    {
      Transform transform = ((Component) GameSettings.Instance.Quest.UnitCamera).transform;
      return new SceneBattle.PosRot()
      {
        Position = transform.localPosition,
        Rotation = transform.localRotation
      };
    }

    private void GotoPrepareSkill()
    {
      LogSkill peek = this.mBattle.Logs.Peek as LogSkill;
      SkillData skill = peek.skill;
      if (peek.skill != null && peek.skill.IsCastSkill() && peek.skill.CastType == ECastTypes.Jump)
      {
        TacticsUnitController unitController = this.FindUnitController(peek.self);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          IntVector2 pos = peek.pos;
          Vector3 vector3 = this.CalcGridCenter(peek.pos.x, peek.pos.y);
          unitController.JumpMapFallPos = pos;
          unitController.JumpFallPos = vector3;
        }
      }
      if (peek != null && peek.self != null && peek.self.Side == EUnitSide.Player)
        this.mLastPlayerSideUseSkillUnit = peek.self;
      this.mEventRecvSkillUnitLists.Clear();
      if (peek != null && peek.skill != null && peek.targets != null)
      {
        TacticsUnitController unitController1 = this.FindUnitController(peek.self);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController1))
        {
          EElement elem = peek.skill.ElementType;
          if (elem == EElement.None)
            elem = unitController1.Unit.Element;
          foreach (LogSkill.Target target in peek.targets)
          {
            TacticsUnitController unitController2 = this.FindUnitController(target.target);
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController2))
            {
              if (peek.skill.IsDamagedSkill() && !target.IsAvoid())
                this.mEventRecvSkillUnitLists.Add(new SceneBattle.EventRecvSkillUnit(unitController2, elem));
              if (target.IsFailCondition())
                this.mEventRecvSkillUnitLists.Add(new SceneBattle.EventRecvSkillUnit(unitController2, target.failCondition));
            }
          }
        }
      }
      if (this.mBattle.IsUnitAuto(this.mBattle.CurrentUnit) || this.mBattle.EntryBattleMultiPlayTimeUp)
        this.HideGrid();
      this.CancelMapViewMode();
      BattleCameraFukan gameObject = GameObjectID.FindGameObject<BattleCameraFukan>(this.mBattleUI.FukanCameraID);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
      {
        gameObject.SetCameraMode(SceneBattle.CameraMode.DEFAULT);
        gameObject.SetDisp(false);
      }
      this.mSkillMotion.MotionId = skill.SkillParam.motion;
      this.mSkillMotion.EffectId = skill.SkillParam.effect;
      this.mSkillMotion.IsBattleScene = !skill.IsMapSkill();
      if (!string.IsNullOrEmpty(skill.SkillParam.SkillMotionId) && peek.self != null)
      {
        SkillMotionParam skillMotionParam = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillMotionParam(skill.SkillParam.SkillMotionId);
        if (skillMotionParam != null)
        {
          SkillMotion skillMotion = skillMotionParam.GetSkillMotion(peek.self.UnitData.UnitID);
          if (skillMotion != null)
          {
            this.mSkillMotion.MotionId = skillMotion.MotionId;
            this.mSkillMotion.IsBattleScene = skillMotion.IsBattleScene;
            if (!string.IsNullOrEmpty(skillMotion.EffectId))
              this.mSkillMotion.EffectId = skillMotion.EffectId;
          }
        }
      }
      if (!this.mBattle.IsSkillDirection)
      {
        if (peek != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEventScript, (UnityEngine.Object) null))
        {
          TacticsUnitController unitController3 = this.FindUnitController(peek.self);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController3, (UnityEngine.Object) null) && peek.targets != null && peek.targets.Count != 0)
          {
            List<TacticsUnitController> TargetLists = new List<TacticsUnitController>();
            foreach (LogSkill.Target target in peek.targets)
            {
              TacticsUnitController unitController4 = this.FindUnitController(target.target);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController4, (UnityEngine.Object) null))
                TargetLists.Add(unitController4);
            }
            if (TargetLists.Count != 0)
            {
              this.mEventSequence = this.mEventScript.OnUseSkill(EventScript.SkillTiming.BEFORE, unitController3, peek.skill, TargetLists, this.mIsFirstPlay);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEventSequence, (UnityEngine.Object) null))
              {
                this.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_WaitGC<SceneBattle.State_DirectionOffSkill>>>();
                return;
              }
            }
          }
        }
        this.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_DirectionOffSkill>>();
      }
      else
      {
        if (peek != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEventScript, (UnityEngine.Object) null))
        {
          TacticsUnitController unitController5 = this.FindUnitController(peek.self);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController5, (UnityEngine.Object) null) && peek.targets != null && peek.targets.Count != 0)
          {
            List<TacticsUnitController> TargetLists = new List<TacticsUnitController>();
            foreach (LogSkill.Target target in peek.targets)
            {
              TacticsUnitController unitController6 = this.FindUnitController(target.target);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController6, (UnityEngine.Object) null))
                TargetLists.Add(unitController6);
            }
            if (TargetLists.Count != 0)
            {
              this.mEventSequence = this.mEventScript.OnUseSkill(EventScript.SkillTiming.BEFORE, unitController5, peek.skill, TargetLists, this.mIsFirstPlay);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEventSequence, (UnityEngine.Object) null))
              {
                this.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_WaitGC<SceneBattle.State_PrepareSkill>>>();
                return;
              }
            }
          }
        }
        this.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_PrepareSkill>>();
      }
    }

    private void CancelMapViewMode()
    {
      if (!this.Battle.IsMultiPlay || !this.VersusMapView)
        return;
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
      {
        this.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal);
        this.mTacticsUnits[index].SetHPChangeYosou(this.mTacticsUnits[index].VisibleHPValue);
      }
      this.mBattleUI.OnCommandSelect();
      GlobalEvent.Invoke("BATTLE_UNIT_DETAIL_CLOSE", (object) this);
      this.mBattleUI.HideTargetWindows();
      this.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
      this.mBattleUI.OnMapViewEnd();
      this.VersusMapView = false;
    }

    private Unit mCollaboTargetUnit
    {
      get
      {
        return UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mCollaboTargetTuc) ? this.mCollaboTargetTuc.Unit : (Unit) null;
      }
    }

    public Unit CollaboMainUnit
    {
      get => !this.mIsInstigatorSubUnit ? this.mCollaboMainUnit : this.mCollaboTargetUnit;
    }

    public Unit CollaboSubUnit
    {
      get => this.mIsInstigatorSubUnit ? this.mCollaboMainUnit : this.mCollaboTargetUnit;
    }

    private void SetScreenMirroring(bool mirror)
    {
      RenderPipeline component = ((Component) Camera.main).GetComponent<RenderPipeline>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.FlipHorizontally = mirror;
      this.isScreenMirroring = mirror;
    }

    private bool FindChangedShield(
      out TacticsUnitController tuc,
      out TacticsUnitController.ShieldState shield)
    {
      List<SceneBattle.FindShield> l = new List<SceneBattle.FindShield>();
      for (int index1 = 0; index1 < this.mTacticsUnits.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.mTacticsUnits[index1].ShieldStates.Count; ++index2)
        {
          if (this.mTacticsUnits[index1].ShieldStates[index2].Dirty)
            l.Add(new SceneBattle.FindShield(this.mTacticsUnits[index1], this.mTacticsUnits[index1].ShieldStates[index2]));
        }
        if (l.Count != 0)
          break;
      }
      tuc = (TacticsUnitController) null;
      shield = (TacticsUnitController.ShieldState) null;
      if (l.Count == 0)
        return false;
      if (l.Count > 1)
        MySort<SceneBattle.FindShield>.Sort(l, (Comparison<SceneBattle.FindShield>) ((src, dst) =>
        {
          if ((int) src.mShield.Target.hp != (int) dst.mShield.Target.hp)
          {
            if ((int) src.mShield.Target.hp < (int) dst.mShield.Target.hp)
              return -1;
            if ((int) src.mShield.Target.hp > (int) dst.mShield.Target.hp)
              return 1;
          }
          return 0;
        }));
      SceneBattle.FindShield findShield = l[0];
      tuc = findShield.mTuc;
      shield = findShield.mShield;
      return true;
    }

    [DebuggerHidden]
    private IEnumerator SetWeatherEffect(WeatherData wd)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CSetWeatherEffect\u003Ec__Iterator4()
      {
        wd = wd,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator StopWeatherEffect(bool is_immidiate = false)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CStopWeatherEffect\u003Ec__Iterator5()
      {
        is_immidiate = is_immidiate,
        \u0024this = this
      };
    }

    private JSON_GvGBattleEndParam CreateGvGbattleEndParam(BtlResultTypes result)
    {
      JSON_GvGBattleEndParam gvGbattleEndParam = new JSON_GvGBattleEndParam();
      switch (result)
      {
        case BtlResultTypes.Win:
          gvGbattleEndParam.result = "win";
          break;
        case BtlResultTypes.Lose:
          gvGbattleEndParam.result = "lose";
          break;
      }
      List<int> finishHp1 = this.Battle.GetFinishHp(EUnitSide.Player);
      List<int> finishHp2 = this.Battle.GetFinishHp(EUnitSide.Enemy);
      gvGbattleEndParam.units = new JSON_GvGPartyUnit[finishHp1.Count];
      GvGParty gvGparty1 = GlobalVars.GvGOffenseParty.Get();
      if (gvGparty1 != null && gvGparty1.Units != null && gvGparty1.Units.Count > 0 && gvGparty1.Units.Count == finishHp1.Count)
      {
        for (int index = 0; index < finishHp1.Count; ++index)
        {
          gvGbattleEndParam.units[index] = new JSON_GvGPartyUnit();
          gvGbattleEndParam.units[index].iid = (long) (int) gvGparty1.Units[index].UniqueID;
          gvGbattleEndParam.units[index].hp = finishHp1[index];
        }
      }
      GvGParty gvGparty2 = GlobalVars.GvGDefenseParty.Get();
      gvGbattleEndParam.defense = new JSON_GvGParty();
      gvGbattleEndParam.defense.id = gvGparty2.Id;
      gvGbattleEndParam.defense.win_num = gvGparty2.WinNum;
      if (gvGparty2 != null && gvGparty2.Units != null && gvGparty2.Units.Count > 0 && gvGparty2.Units.Count == finishHp2.Count)
      {
        gvGbattleEndParam.defense.units = new JSON_GvGPartyUnit[finishHp2.Count];
        gvGbattleEndParam.defense.npc_units = new JSON_GvGPartyNPC[finishHp2.Count];
        for (int index = 0; index < finishHp2.Count; ++index)
        {
          if (gvGbattleEndParam.defense.is_npc == 0)
          {
            gvGbattleEndParam.defense.units[index] = new JSON_GvGPartyUnit();
            gvGbattleEndParam.defense.units[index].iid = (long) (int) gvGparty2.Units[index].UniqueID;
            gvGbattleEndParam.defense.units[index].hp = finishHp2[index];
          }
          else
          {
            gvGbattleEndParam.defense.npc_units[index] = new JSON_GvGPartyNPC();
            gvGbattleEndParam.defense.npc_units[index].iid = (int) gvGparty2.Units[index].UniqueID;
            gvGbattleEndParam.defense.npc_units[index].hp = finishHp2[index];
          }
        }
      }
      return gvGbattleEndParam;
    }

    public bool isUpView => this.m_CameraMode == SceneBattle.CameraMode.UPVIEW;

    public bool isNewCamera => this.m_NewCamera;

    public bool isFullRotationCamera => this.m_FullRotationCamera;

    public bool mUpdateCameraPosition
    {
      set => this.m_UpdateCamera = value;
      get => this.m_UpdateCamera;
    }

    public bool isBattleCamera => this.m_BattleCamera;

    public TargetCamera targetCamera => this.m_TargetCamera;

    private bool IsCameraMoving
    {
      get
      {
        return ObjectAnimator.Get((Component) Camera.main).isMoving || this.mUpdateCameraPosition && (this.m_TargetCameraPositionInterp || this.m_TargetCameraDistanceInterp);
      }
    }

    public bool isCameraLeftMove
    {
      get
      {
        return this.isFullRotationCamera || (double) this.m_CameraAngle - (double) this.m_CameraYawMin > 0.0099999997764825821;
      }
    }

    public bool isCameraRightMove
    {
      get
      {
        return this.isFullRotationCamera || (double) this.m_CameraYawMax - (double) this.m_CameraAngle > 0.0099999997764825821;
      }
    }

    private Vector3 mCameraTarget
    {
      set => this.m_CameraPosition = value;
      get => this.m_CameraPosition;
    }

    private bool mAllowCameraRotation
    {
      set => this.m_AllowCameraRotation = value;
      get => this.m_AllowCameraRotation;
    }

    private bool mAllowCameraTranslation
    {
      set => this.m_AllowCameraTranslation = value;
      get => this.m_AllowCameraTranslation;
    }

    private bool mDesiredCameraTargetSet
    {
      set => this.m_TargetCameraPositionInterp = value;
      get => this.m_TargetCameraPositionInterp;
    }

    public float CameraYawRatio
    {
      get
      {
        return Mathf.Clamp01((float) (((double) this.m_CameraAngle - (double) this.m_CameraYawMin) / ((double) this.m_CameraYawMax - (double) this.m_CameraYawMin)));
      }
    }

    public void SetMoveCamera() => this.ResetMoveCamera();

    private void SetCameraOffset(Transform transform)
    {
    }

    private void InterpCameraOffset(Transform transform)
    {
    }

    private void InitCamera()
    {
      GameSettings instance = GameSettings.Instance;
      CameraHook.AddPreCullEventListener(new CameraHook.PreCullEvent(this.OnCameraPreCull));
      RenderPipeline.Setup(Camera.main);
      this.m_TargetCamera = GameUtility.RequireComponent<TargetCamera>(((Component) Camera.main).gameObject);
      this.m_TargetCamera.Pitch = instance.GameCamera_AngleX;
      this.m_DefaultCameraYawMin = instance.GameCamera_YawMin;
      this.m_DefaultCameraYawMax = instance.GameCamera_YawMax;
      this.m_CameraYawMin = this.m_DefaultCameraYawMin;
      this.m_CameraYawMax = this.m_DefaultCameraYawMax;
      this.m_CameraAngle = this.m_DefaultCameraYawMin;
      this.m_TargetCameraDistance = instance.GameCamera_DefaultDistance;
      this.m_TargetCameraDistanceInterp = true;
      this.m_NewCamera = false;
      this.SetFullRotationCamera(true);
    }

    private void DestroyCamera()
    {
      CameraHook.RemovePreCullEventListener(new CameraHook.PreCullEvent(this.OnCameraPreCull));
    }

    public void ResetCameraTarget()
    {
      this.m_TargetCamera.Reset();
      if (!this.isUpView)
      {
        this.m_CameraAngle = this.m_TargetCamera.Yaw;
        this.m_TargetCameraDistance = this.m_TargetCamera.CameraDistance;
        this.m_TargetCameraDistanceInterp = false;
      }
      this.m_CameraPosition = this.m_TargetCamera.TargetPosition;
      this.m_CameraPosition.y -= GameSettings.Instance.GameCamera_UnitHeightOffset;
    }

    public void ResetMoveCamera()
    {
      this.InterpCameraTarget((Component) this.FindUnitController(this.mBattle.CurrentUnit));
      this.m_TargetCamera.Pitch = GameSettings.Instance.GameCamera_AngleX;
    }

    private void UpdateCameraControl(bool immediate = false)
    {
      if (this.isBattleCamera)
        return;
      if (this.isUpView)
      {
        this.UpdateCameraControlUpView(immediate);
      }
      else
      {
        Camera main = Camera.main;
        Transform transform = !UnityEngine.Object.op_Inequality((UnityEngine.Object) main, (UnityEngine.Object) null) ? (Transform) null : ((Component) main).transform;
        GameSettings instance = GameSettings.Instance;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTacticsSceneRoot, (UnityEngine.Object) null) && ((Component) this.mTacticsSceneRoot).gameObject.activeInHierarchy)
          main.fieldOfView = GameSettings.Instance.GameCamera_TacticsSceneFOV;
        else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleSceneRoot, (UnityEngine.Object) null) && ((Component) this.mBattleSceneRoot).gameObject.activeInHierarchy)
          main.fieldOfView = GameSettings.Instance.GameCamera_BattleSceneFOV;
        if (!ObjectAnimator.Get((Component) main).isMoving && this.mUpdateCameraPosition)
        {
          bool flag = this.UpdateCameraRotationInterp(immediate);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTouchController, (UnityEngine.Object) null))
          {
            this.UpdateCameraRotationTouchMove(!flag, immediate);
            this.UpdateCameraPositionTouchMove(transform.right, transform.forward);
          }
          this.UpdateCameraPositionInterp(immediate);
          if (this.m_TargetCameraDistanceInterp)
          {
            float num = !immediate ? Time.deltaTime * 8f : 1f;
            float targetCameraDistance = this.m_TargetCameraDistance;
            this.m_TargetCamera.CameraDistance = Mathf.Lerp(this.m_TargetCamera.CameraDistance, targetCameraDistance, num);
            if ((double) Mathf.Abs(targetCameraDistance - this.m_TargetCamera.CameraDistance) <= 0.0099999997764825821)
            {
              this.m_TargetCamera.CameraDistance = targetCameraDistance;
              this.m_TargetCameraDistanceInterp = false;
            }
          }
          this.m_TargetCamera.SetPositionYaw(Vector3.op_Addition(this.m_CameraPosition, Vector3.op_Multiply(Vector3.up, instance.GameCamera_UnitHeightOffset)), this.m_CameraAngle);
        }
        this.OnCameraForcus();
      }
    }

    private bool UpdateCameraRotationTouchMove(bool isEnable, bool immediate)
    {
      bool flag = false;
      if (isEnable && this.m_AllowCameraRotation && this.IsControlBattleUI(SceneBattle.eMaskBattleUI.CAMERA))
      {
        float num1 = -this.mTouchController.AngularVelocity.x * (float) (1.0 / (double) Screen.width * 180.0);
        if (!this.isNewCamera)
        {
          float cameraYawSoftLimit = GameSettings.Instance.GameCamera_YawSoftLimit;
          float num2 = 2f;
          if ((double) this.m_CameraAngle < (double) this.m_CameraYawMin && (double) num1 < 0.0)
          {
            float num3 = Mathf.Pow(1f - Mathf.Clamp01((float) -((double) this.m_CameraAngle - (double) this.m_CameraYawMin) / cameraYawSoftLimit), num2);
            num1 *= num3;
          }
          else if ((double) this.m_CameraAngle > (double) this.m_CameraYawMax && (double) num1 > 0.0)
          {
            float num4 = Mathf.Pow(1f - Mathf.Clamp01((this.m_CameraAngle - this.m_CameraYawMax) / cameraYawSoftLimit), num2);
            num1 *= num4;
          }
          this.m_CameraAngle += num1;
          if (!SRPG_TouchInputModule.IsMultiTouching)
          {
            float num5 = !immediate ? Time.deltaTime * 10f : 1f;
            if ((double) this.m_CameraAngle < (double) this.m_CameraYawMin)
              this.m_CameraAngle = (double) Mathf.Abs(this.m_CameraAngle - this.m_CameraYawMin) >= 0.0099999997764825821 ? Mathf.Lerp(this.m_CameraAngle, this.m_CameraYawMin, num5) : this.m_CameraYawMin;
            else if ((double) this.m_CameraAngle > (double) this.m_CameraYawMax)
              this.m_CameraAngle = (double) Mathf.Abs(this.m_CameraAngle - this.m_CameraYawMax) >= 0.0099999997764825821 ? Mathf.Lerp(this.m_CameraAngle, this.m_CameraYawMax, num5) : this.m_CameraYawMax;
          }
        }
        else
        {
          this.m_CameraAngle += num1;
          if (!this.isFullRotationCamera)
          {
            if ((double) this.m_CameraAngle < (double) this.m_CameraYawMin)
              this.m_CameraAngle = this.m_CameraYawMin;
            else if ((double) this.m_CameraAngle > (double) this.m_CameraYawMax)
              this.m_CameraAngle = this.m_CameraYawMax;
          }
        }
        flag = true;
      }
      this.mTouchController.AngularVelocity = Vector2.zero;
      return flag;
    }

    private bool UpdateCameraPositionTouchMove(Vector3 xAxis, Vector3 yAxis)
    {
      Camera main = Camera.main;
      bool flag = false;
      if (this.m_AllowCameraTranslation && (double) ((Vector2) ref this.mTouchController.Velocity).magnitude > 0.0 && this.IsControlBattleUI(SceneBattle.eMaskBattleUI.CAMERA))
      {
        Vector2 velocity = this.mTouchController.Velocity;
        yAxis.y = 0.0f;
        ((Vector3) ref yAxis).Normalize();
        xAxis.y = 0.0f;
        ((Vector3) ref xAxis).Normalize();
        Vector3 screenPoint = main.WorldToScreenPoint(this.m_CameraPosition);
        Vector2 vector2 = Vector2.op_Implicit(Vector3.op_Subtraction(main.WorldToScreenPoint(Vector3.op_Addition(Vector3.op_Addition(this.m_CameraPosition, xAxis), yAxis)), screenPoint));
        velocity.x /= Mathf.Abs(vector2.x);
        velocity.y /= Mathf.Abs(vector2.y);
        // ISSUE: explicit constructor call
        ((Vector2) ref velocity).\u002Ector((float) ((double) xAxis.x * (double) velocity.x + (double) yAxis.x * (double) velocity.y), (float) ((double) xAxis.z * (double) velocity.x + (double) yAxis.z * (double) velocity.y));
        this.m_CameraPosition.x -= velocity.x;
        this.m_CameraPosition.z -= velocity.y;
        this.m_CameraPosition.x = Mathf.Clamp(this.m_CameraPosition.x, 0.1f, (float) this.mBattle.CurrentMap.Width - 0.1f);
        this.m_CameraPosition.z = Mathf.Clamp(this.m_CameraPosition.z, 0.1f, (float) this.mBattle.CurrentMap.Height - 0.1f);
        this.m_CameraPosition.y = this.CalcHeight(this.m_CameraPosition.x, this.m_CameraPosition.z);
        flag = true;
      }
      this.mTouchController.Velocity = Vector2.zero;
      return flag;
    }

    private bool UpdateCameraRotationInterp(bool immediate)
    {
      if (!this.m_TargetCameraAngleInterp)
        return false;
      this.m_TargetCameraAngleTime = !immediate ? Mathf.Min(this.m_TargetCameraAngleTime + Time.deltaTime, this.m_TargetCameraAngleTimeMax) : this.m_TargetCameraAngleTimeMax;
      this.m_CameraAngle = Mathf.Lerp(this.m_TargetCameraAngleStart, this.m_TargetCameraAngle, Mathf.Sin((float) (1.5707963705062866 * ((double) this.m_TargetCameraAngleTime / (double) this.m_TargetCameraAngleTimeMax))));
      if ((double) this.m_TargetCameraAngleTime >= (double) this.m_TargetCameraAngleTimeMax)
        this.m_TargetCameraAngleInterp = false;
      return true;
    }

    private bool UpdateCameraPositionInterp(bool immediate)
    {
      float num = !immediate ? Time.deltaTime * 8f : 1f;
      if (!this.m_TargetCameraPositionInterp)
        return false;
      this.m_CameraPosition = Vector3.Lerp(this.m_CameraPosition, this.m_TargetCameraPosition, num);
      Vector3 vector3 = Vector3.op_Subtraction(this.m_TargetCameraPosition, this.m_CameraPosition);
      if ((double) ((Vector3) ref vector3).magnitude <= 0.0099999997764825821)
      {
        this.m_CameraPosition = this.m_TargetCameraPosition;
        this.m_TargetCameraPositionInterp = false;
      }
      return true;
    }

    public void SetBattleCamera(bool value) => this.m_BattleCamera = value;

    public void SetNewCamera(TacticsSceneCamera camera)
    {
      this.m_NewCamera = true;
      this.m_FullRotationCamera = camera.allRange.enable;
      if (!this.m_FullRotationCamera && camera.moveRange.isOverride)
      {
        this.m_CameraYawMin = 360f - camera.moveRange.min;
        this.m_CameraYawMax = 360f - camera.moveRange.max;
        this.m_CameraAngle = 360f - camera.moveRange.start;
        if ((double) this.m_CameraYawMin <= (double) this.m_CameraYawMax)
          return;
        float cameraYawMin = this.m_CameraYawMin;
        this.m_CameraYawMin = this.m_CameraYawMax;
        this.m_CameraYawMax = cameraYawMin;
      }
      else
      {
        this.m_CameraYawMin = this.m_DefaultCameraYawMin;
        this.m_CameraYawMax = this.m_DefaultCameraYawMax;
        this.m_CameraAngle = this.m_DefaultCameraYawMin;
      }
    }

    public void SetFullRotationCamera(bool value)
    {
      if (value)
      {
        this.m_NewCamera = true;
      }
      else
      {
        this.m_CameraYawMin = this.m_DefaultCameraYawMin;
        this.m_CameraYawMax = this.m_DefaultCameraYawMax;
        this.m_CameraAngle = this.m_DefaultCameraYawMin;
        this.m_NewCamera = false;
      }
      this.m_FullRotationCamera = value;
    }

    public float GetCameraDistance() => this.m_TargetCameraDistance;

    private void SetCameraTarget(Vector3 position)
    {
      this.m_CameraPosition = position;
      this.m_TargetCameraPositionInterp = false;
    }

    private void SetCameraTarget(Component component, bool is_cal_height = false)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      Vector3 position1 = component.transform.position;
      Vector3 position2;
      // ISSUE: explicit constructor call
      ((Vector3) ref position2).\u002Ector(position1.x, !is_cal_height ? position1.y : this.CalcHeight(position1.x, position1.z), position1.z);
      if (component is TacticsUnitController)
      {
        TacticsUnitController tacticsUnitController = component as TacticsUnitController;
        if (tacticsUnitController.Unit != null && !tacticsUnitController.Unit.IsNormalSize)
          position2.y += tacticsUnitController.Unit.OffsZ;
      }
      this.SetCameraTarget(position2);
    }

    private void SetCameraTarget(float x, float y)
    {
      this.m_CameraPosition.x = x;
      this.m_CameraPosition.y = this.CalcHeight(x, y);
      this.m_CameraPosition.z = y;
    }

    public void InterpCameraTarget(Vector3 position)
    {
      this.m_TargetCameraPosition = position;
      this.m_TargetCameraPositionInterp = true;
      this.mUpdateCameraPosition = true;
    }

    private void InterpCameraTarget(Component component)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      Vector3 position1 = component.transform.position;
      Vector3 position2;
      // ISSUE: explicit constructor call
      ((Vector3) ref position2).\u002Ector(position1.x, position1.y, position1.z);
      if (component is TacticsUnitController)
      {
        TacticsUnitController tacticsUnitController = component as TacticsUnitController;
        if (tacticsUnitController.Unit != null && !tacticsUnitController.Unit.IsNormalSize)
          position2.y += tacticsUnitController.Unit.OffsZ;
      }
      this.InterpCameraTarget(position2);
    }

    private void InterpCameraTargetToCurrent()
    {
      TacticsUnitController unitController = this.FindUnitController(this.mBattle.CurrentUnit);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        return;
      this.InterpCameraTarget((Component) unitController);
    }

    public void InterpCameraDistance(float distance)
    {
      this.m_TargetCameraDistance = distance;
      this.m_TargetCameraDistanceInterp = true;
    }

    public void SetCameraYawRange(float min, float max)
    {
      this.m_DefaultCameraYawMin = min;
      this.m_DefaultCameraYawMax = max;
    }

    public void RotateCamera(float delta, float duration)
    {
      if (!this.IsControlBattleUI(SceneBattle.eMaskBattleUI.CAMERA) || !this.mUpdateCameraPosition || this.isBattleCamera || this.isUpView || ObjectAnimator.Get((Component) Camera.main).isMoving)
        return;
      float num1 = this.isNewCamera ? ((double) delta >= 0.0 ? 45f : -45f) : delta * (this.m_CameraYawMax - this.m_CameraYawMin);
      if (this.m_TargetCameraAngleInterp)
      {
        this.m_TargetCameraAngleStart = this.m_CameraAngle;
        this.m_TargetCameraAngle += num1;
      }
      else
      {
        this.m_TargetCameraAngleStart = this.m_CameraAngle;
        this.m_TargetCameraAngleInterp = true;
        this.m_TargetCameraAngle = this.m_CameraAngle + num1;
      }
      if (this.isNewCamera)
      {
        if ((double) num1 > 0.0)
          this.m_TargetCameraAngle = (float) Mathf.FloorToInt((float) (((double) this.m_TargetCameraAngle + 1.0) / 45.0)) * 45f;
        else if ((double) num1 < 0.0)
          this.m_TargetCameraAngle = (float) Mathf.CeilToInt((float) (((double) this.m_TargetCameraAngle - 1.0) / 45.0)) * 45f;
      }
      float num2 = duration;
      float num3 = duration / 45f;
      duration = Mathf.Abs(this.m_TargetCameraAngle - this.m_TargetCameraAngleStart) * num3;
      if ((double) duration > (double) num2)
        duration = num2;
      else if ((double) duration < 0.10000000149011612)
        duration = 0.1f;
      if (!this.isNewCamera)
        this.m_TargetCameraAngle = Mathf.Clamp(this.m_TargetCameraAngle, this.m_CameraYawMin, this.m_CameraYawMax);
      else if (!this.isFullRotationCamera)
        this.m_TargetCameraAngle = Mathf.Clamp(this.m_TargetCameraAngle, this.m_CameraYawMin, this.m_CameraYawMax);
      else if ((double) this.m_TargetCameraAngle >= 360.0)
      {
        this.m_CameraAngle -= 360f;
        this.m_TargetCameraAngleStart -= 360f;
        this.m_TargetCameraAngle -= 360f;
      }
      else if ((double) this.m_TargetCameraAngle < 0.0)
      {
        this.m_CameraAngle += 360f;
        this.m_TargetCameraAngleStart += 360f;
        this.m_TargetCameraAngle += 360f;
      }
      this.m_TargetCameraAngleTime = 0.0f;
      this.m_TargetCameraAngleTimeMax = duration;
    }

    public float GetCameraAngle() => this.m_CameraAngle;

    public void GetCameraTargetView(out Vector3 center, out float distance, Vector3[] targets)
    {
      Camera main = Camera.main;
      float cameraDistance = this.m_TargetCamera.CameraDistance;
      Vector3 targetPosition = this.m_TargetCamera.TargetPosition;
      distance = cameraDistance;
      if (targets.Length == 1)
      {
        center = targets[0];
      }
      else
      {
        // ISSUE: explicit constructor call
        ((Vector3) ref center).\u002Ector(0.0f, float.MaxValue, 0.0f);
        for (int index = 0; index < targets.Length; ++index)
        {
          center.x += targets[index].x;
          center.z += targets[index].z;
          center.y = Mathf.Min(targets[index].y, center.y);
        }
        center.x /= (float) targets.Length;
        center.y -= 0.5f;
        center.z /= (float) targets.Length;
        bool flag;
        do
        {
          flag = false;
          this.m_TargetCamera.CameraDistance = distance;
          this.m_TargetCamera.SetPositionYaw(center, this.m_CameraAngle);
          for (int index = 0; index < targets.Length; ++index)
          {
            Vector3 viewportPoint = main.WorldToViewportPoint(targets[index]);
            if ((double) viewportPoint.x < 0.0 || (double) viewportPoint.x > 1.0 || (double) viewportPoint.y < 0.0 || (double) viewportPoint.y > 0.89999997615814209)
            {
              ++distance;
              flag = true;
              break;
            }
          }
        }
        while (flag && (double) distance < (double) GameSettings.Instance.GameCamera_MaxDistance);
        center = Vector3.op_Subtraction(center, Vector3.op_Multiply(Vector3.up, GameSettings.Instance.GameCamera_UnitHeightOffset));
        this.m_TargetCamera.CameraDistance = cameraDistance;
        this.m_TargetCamera.TargetPosition = targetPosition;
      }
    }

    private void OnCameraPreCull(Camera cam)
    {
      if (!(((Component) cam).tag == "MainCamera") || ((Component) cam).gameObject.layer != GameUtility.LayerDefault)
        return;
      this.LayoutPopups(cam);
      this.LayoutGauges(cam);
    }

    private void OnCameraForcus()
    {
      if (this.mOnUnitFocus != null)
      {
        if (this.m_TargetCameraPositionInterp)
          return;
        TacticsUnitController closestUnitController = this.FindClosestUnitController(this.m_CameraPosition, 1f);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mFocusedUnit, (UnityEngine.Object) closestUnitController))
          return;
        DebugUtility.Log("Focus:" + (object) closestUnitController);
        this.mFocusedUnit = closestUnitController;
        this.mOnUnitFocus(this.mFocusedUnit);
      }
      else
        this.mFocusedUnit = (TacticsUnitController) null;
    }

    private void UpdateCameraControlUpView(bool immediate = false)
    {
      Camera main = Camera.main;
      Transform transform = !UnityEngine.Object.op_Inequality((UnityEngine.Object) main, (UnityEngine.Object) null) ? (Transform) null : ((Component) main).transform;
      GameSettings instance = GameSettings.Instance;
      main.fieldOfView = GameSettings.Instance.GameCamera_TacticsSceneFOV;
      if (this.mUpdateCameraPosition)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTouchController, (UnityEngine.Object) null) && !ObjectAnimator.Get((Component) main).isMoving)
          this.UpdateCameraPositionTouchMove(transform.right, transform.up);
        this.UpdateCameraPositionInterp(immediate);
        if (this.m_TargetCameraDistanceInterp)
        {
          float num1 = !immediate ? Time.deltaTime * 8f : 1f;
          float num2 = 5f + this.m_TargetCameraDistance;
          this.m_TargetCamera.CameraDistance = Mathf.Lerp(this.m_TargetCamera.CameraDistance, num2, num1);
          if ((double) Mathf.Abs(num2 - this.m_TargetCamera.CameraDistance) <= 0.0099999997764825821)
          {
            this.m_TargetCamera.CameraDistance = num2;
            this.m_TargetCameraDistanceInterp = false;
          }
        }
        this.m_TargetCamera.SetPositionYawPitch(Vector3.op_Addition(this.m_CameraPosition, Vector3.op_Multiply(Vector3.up, instance.GameCamera_UnitHeightOffset)), 90f, 270f);
      }
      this.OnCameraForcus();
    }

    public void OnCameraModeChange(SceneBattle.CameraMode nextMode)
    {
      if (this.m_CameraMode == nextMode)
        return;
      GameSettings instance = GameSettings.Instance;
      this.m_CameraMode = nextMode;
      if (this.m_TargetCameraPositionInterp)
      {
        this.m_CameraPosition = this.m_TargetCameraPosition;
        this.m_TargetCameraPositionInterp = false;
      }
      if (this.m_TargetCameraAngleInterp)
      {
        this.m_CameraAngle = this.m_TargetCameraAngle;
        this.m_TargetCameraAngleInterp = false;
      }
      if (nextMode == SceneBattle.CameraMode.DEFAULT)
      {
        this.m_CameraAngle = this.m_CameraYaw;
        this.m_TargetCameraDistanceInterp = false;
        this.m_TargetCamera.CameraDistance = this.m_TargetCameraDistance;
        this.m_TargetCamera.SetPositionYawPitch(Vector3.op_Addition(this.m_CameraPosition, Vector3.op_Multiply(Vector3.up, instance.GameCamera_UnitHeightOffset)), this.m_CameraAngle, instance.GameCamera_AngleX);
      }
      else
      {
        if (nextMode != SceneBattle.CameraMode.UPVIEW)
          return;
        this.m_CameraYaw = this.m_CameraAngle;
        this.m_TargetCameraDistanceInterp = false;
        this.m_TargetCamera.CameraDistance = 5f + this.m_TargetCameraDistance;
        this.m_TargetCamera.SetPositionYawPitch(Vector3.op_Addition(this.m_CameraPosition, Vector3.op_Multiply(Vector3.up, instance.GameCamera_UnitHeightOffset)), 90f, 270f);
      }
    }

    public bool AudiencePause
    {
      get => this.mAudiencePause;
      set => this.mAudiencePause = value;
    }

    public bool AudienceSkip
    {
      get => this.mAudienceSkip;
      set => this.mAudienceSkip = value;
    }

    private void MultiPlayLog(string str)
    {
    }

    public int MultiPlayerCount => this.mMultiPlayer.Count;

    private float MultiPlayInputTimeLimit { get; set; }

    private bool MultiPlayExtMoveInputTime { get; set; }

    private bool MultiPlayExtSelectInputTime { get; set; }

    public float MultiPlayAddInputTime { get; set; }

    public bool ResumeSuccess
    {
      get => this.mResumeSuccess;
      set => this.mResumeSuccess = value;
    }

    public bool ResumeOnly => this.mResumeOnlyPlayer;

    public bool VersusMapView
    {
      get => this.mMapViewMode;
      set => this.mMapViewMode = value;
    }

    public bool AlreadyEndBattle
    {
      get => this.mAlreadyEndBattle;
      set => this.mAlreadyEndBattle = value;
    }

    public bool IsExistResume
    {
      get => this.mRecvResumeRequest != null && this.mRecvResumeRequest.Count > 0;
    }

    public bool AudienceForceEnd
    {
      get => this.mAudienceForceEnd;
      set => this.mAudienceForceEnd = value;
    }

    public bool IsSend
    {
      get
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        return (instance.AudienceMode ? 1 : (instance.IsVSCpuBattle ? 1 : 0)) == 0;
      }
    }

    public bool IsAutoForDisplay { get; private set; }

    public void ExtentionMultiInputTime(bool bMove)
    {
      if ((double) this.MultiPlayInputTimeLimit <= 0.0)
        return;
      if (bMove && !this.MultiPlayExtMoveInputTime)
      {
        this.MultiPlayInputTimeLimit += this.MULTI_PLAY_INPUT_EXT_MOVE;
        this.MultiPlayAddInputTime = this.MULTI_PLAY_INPUT_EXT_MOVE;
        this.MultiPlayExtMoveInputTime = true;
        this.mBattleUI_MultiPlay.OnExtInput();
      }
      if (bMove || this.MultiPlayExtSelectInputTime)
        return;
      this.MultiPlayInputTimeLimit += this.MULTI_PLAY_INPUT_EXT_SELECT;
      this.MultiPlayAddInputTime = this.MULTI_PLAY_INPUT_EXT_SELECT;
      this.MultiPlayExtSelectInputTime = true;
      this.mBattleUI_MultiPlay.OnExtInput();
    }

    private void CreateMultiPlayer()
    {
      if (!this.Battle.IsMultiPlay)
        return;
      this.mMultiPlayer = new List<SceneBattle.MultiPlayer>();
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
      for (int index = 0; index < myPlayersStarted.Count; ++index)
      {
        JSON_MyPhotonPlayerParam photonPlayerParam = myPlayersStarted[index];
        if (photonPlayerParam.playerIndex == instance.MyPlayerIndex)
          this.IsAutoForDisplay = photonPlayerParam.isAuto != 0;
        else
          this.mMultiPlayer.Add(new SceneBattle.MultiPlayer(this, photonPlayerParam.playerIndex, photonPlayerParam.playerID, photonPlayerParam.isSupportAI != 0, photonPlayerParam.isAutoTreasure != 0, photonPlayerParam.isAutoNoSkill != 0, photonPlayerParam.isAuto));
      }
      this.mMultiPlayerUnit = new List<SceneBattle.MultiPlayerUnit>();
      for (int index = 0; index < this.Battle.AllUnits.Count; ++index)
      {
        Unit allUnit = this.Battle.AllUnits[index];
        if (allUnit != null)
        {
          int playerIndex = allUnit.OwnerPlayerIndex;
          if (playerIndex != instance.MyPlayerIndex && playerIndex > 0)
          {
            SceneBattle.MultiPlayer owner = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerIndex == playerIndex));
            if (owner != null)
              this.mMultiPlayerUnit.Add(new SceneBattle.MultiPlayerUnit(this, index, allUnit, owner));
          }
        }
      }
      this.mSendList = new List<SceneBattle.MultiPlayInput>();
      this.mMultiSendLogBuffer = new MultiSendLogBuffer();
      this.mResumeMultiPlay = instance.IsResume();
      this.mResumeAlreadyStartPlayer.Clear();
    }

    private void CreateAudiencePlayer()
    {
      if (!MonoSingleton<GameManager>.Instance.AudienceMode)
        return;
      this.mMultiPlayer = new List<SceneBattle.MultiPlayer>();
      List<JSON_MyPhotonPlayerParam> mAudiencePlayers = this.mAudiencePlayers;
      for (int index = 0; index < mAudiencePlayers.Count; ++index)
      {
        JSON_MyPhotonPlayerParam photonPlayerParam = mAudiencePlayers[index];
        this.mMultiPlayer.Add(new SceneBattle.MultiPlayer(this, photonPlayerParam.playerIndex, photonPlayerParam.playerID));
      }
      this.mMultiPlayerUnit = new List<SceneBattle.MultiPlayerUnit>();
      for (int index = 0; index < this.Battle.AllUnits.Count; ++index)
      {
        Unit allUnit = this.Battle.AllUnits[index];
        if (allUnit != null)
        {
          int playerIndex = allUnit.OwnerPlayerIndex;
          if (playerIndex > 0)
          {
            SceneBattle.MultiPlayer owner = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerIndex == playerIndex));
            if (owner != null)
              this.mMultiPlayerUnit.Add(new SceneBattle.MultiPlayerUnit(this, index, allUnit, owner));
          }
        }
      }
      this.mSendList = new List<SceneBattle.MultiPlayInput>();
      this.mMultiSendLogBuffer = new MultiSendLogBuffer();
    }

    private void CreateCpuBattlePlayer()
    {
      if (!MonoSingleton<GameManager>.Instance.IsVSCpuBattle)
        return;
      this.mMultiPlayer = new List<SceneBattle.MultiPlayer>();
      for (int index = 0; index < this.CPUBATTLE_PLAYER_NUM; ++index)
        this.mMultiPlayer.Add(new SceneBattle.MultiPlayer(this, index + 1, index + 1));
      this.mMultiPlayerUnit = new List<SceneBattle.MultiPlayerUnit>();
      for (int index = 0; index < this.Battle.AllUnits.Count; ++index)
      {
        Unit allUnit = this.Battle.AllUnits[index];
        if (allUnit != null)
        {
          int playerIndex = allUnit.OwnerPlayerIndex;
          if (playerIndex > 0)
          {
            SceneBattle.MultiPlayer owner = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerIndex == playerIndex));
            if (owner != null)
              this.mMultiPlayerUnit.Add(new SceneBattle.MultiPlayerUnit(this, index, allUnit, owner));
          }
        }
      }
      this.mSendList = new List<SceneBattle.MultiPlayInput>();
      this.mMultiSendLogBuffer = new MultiSendLogBuffer();
    }

    public void ChangeMultiPlayerUnit(Unit fr_unit, Unit to_unit)
    {
      if (fr_unit == null || to_unit == null)
        return;
      int index = this.Battle.AllUnits.FindIndex((Predicate<Unit>) (u => u == to_unit));
      if (index < 0)
        return;
      this.mMultiPlayerUnit.Find((Predicate<SceneBattle.MultiPlayerUnit>) (p => p.Unit == fr_unit))?.SetUnit(index, to_unit);
    }

    public void ResumeMultiPlayerUnit()
    {
      for (int index = 0; index < this.Battle.AllUnits.Count; ++index)
      {
        Unit allUnit = this.Battle.AllUnits[index];
        if (allUnit != null && allUnit.IsUnitFlag(EUnitFlag.IsDynamicTransform) && !allUnit.IsDead)
        {
          Unit dtuFromUnit = allUnit.DtuFromUnit;
          while (dtuFromUnit.DtuFromUnit != null)
            dtuFromUnit = dtuFromUnit.DtuFromUnit;
          this.ChangeMultiPlayerUnit(dtuFromUnit, allUnit);
        }
      }
    }

    public bool IsMultiPlayerAuto(Unit unit, bool showMode = false)
    {
      SceneBattle.MultiPlayerUnit multiPlayerUnit = this.mMultiPlayerUnit.Find((Predicate<SceneBattle.MultiPlayerUnit>) (p => p.Unit == unit));
      if (multiPlayerUnit == null)
        return showMode ? this.IsAutoForDisplay && this.CurrentQuest.CheckAllowedAutoBattle() : this.Battle.IsAutoBattle || this.Battle.RequestAutoBattle;
      if (!showMode)
        return multiPlayerUnit.Owner.IsAutoPlayer;
      return multiPlayerUnit.Owner.IsAutoForDisplay && this.CurrentQuest.CheckAllowedAutoBattle();
    }

    public bool IsMultiTreasurePriority(Unit unit)
    {
      SceneBattle.MultiPlayerUnit multiPlayerUnit = this.mMultiPlayerUnit.Find((Predicate<SceneBattle.MultiPlayerUnit>) (p => p.Unit == unit));
      if (multiPlayerUnit == null)
        return GameUtility.Config_AutoMode_TreasureMulti.Value;
      return !multiPlayerUnit.Owner.IsSupportAI && multiPlayerUnit.Owner.IsAutoTreasure;
    }

    public bool IsMultiDisableSkill(Unit unit)
    {
      SceneBattle.MultiPlayerUnit multiPlayerUnit = this.mMultiPlayerUnit.Find((Predicate<SceneBattle.MultiPlayerUnit>) (p => p.Unit == unit));
      if (multiPlayerUnit == null)
        return GameUtility.Config_AutoMode_DisableSkillMulti.Value;
      return !multiPlayerUnit.Owner.IsSupportAI && multiPlayerUnit.Owner.IsAutoNoSkill;
    }

    private void BeginMultiPlayer()
    {
      if (!this.Battle.IsMultiPlay)
        return;
      if (this.mBeginMultiPlay)
      {
        this.MultiPlayLog("[PUN]already BeginMultiPlayer");
      }
      else
      {
        this.mBeginMultiPlay = true;
        this.mSendList.Clear();
        this.mSendTime = 0.0f;
        this.MultiPlayInputTimeLimit = 0.0f;
        this.MultiPlayExtMoveInputTime = false;
        this.MultiPlayExtSelectInputTime = false;
        this.Battle.EntryBattleMultiPlayTimeUp = false;
        this.Battle.MultiPlayDisconnectAutoBattle = false;
        this.mPrevGridX = -1;
        this.mPrevGridY = -1;
        this.mPrevDir = EUnitDirection.Auto;
        Unit unit = this.Battle.CurrentUnit;
        this.mCurrentSendInputUnitID = this.Battle.AllUnits.FindIndex((Predicate<Unit>) (u => u == unit));
        this.MultiPlayLog("[PUN]BeginMultiPlayer********** turn:" + (object) this.UnitStartCountTotal + " unitID:" + (object) this.mCurrentSendInputUnitID + " sqID:" + (object) this.mMultiPlaySendID);
        SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerIndex == unit.OwnerPlayerIndex));
        multiPlayer?.Begin(this);
        this.mMultiPlayerUnit.Find((Predicate<SceneBattle.MultiPlayerUnit>) (u => u.Unit == unit))?.Begin(this);
        if (unit.OwnerPlayerIndex == this.Battle.MyPlayerIndex)
        {
          this.SendAuto();
          this.MultiPlayInputTimeLimit = this.MULTI_PLAY_INPUT_TIME_LIMIT_SEC;
        }
        if (this.Battle.IsMultiPlay)
          this.CloseBattleUI();
        MyPhoton instance1 = PunMonoSingleton<MyPhoton>.Instance;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance1, (UnityEngine.Object) null) && multiPlayer != null && instance1.IsOldestPlayer() && multiPlayer.Disconnected)
          this.SendOtherPlayerDisconnect(multiPlayer.PlayerIndex);
        GameManager instance2 = MonoSingleton<GameManager>.Instance;
        if (!instance2.AudienceMode)
          return;
        if (multiPlayer != null)
          multiPlayer.Disconnected = false;
        if (!instance2.AudienceManager.IsSkipEnd)
          return;
        this.mBattleUI_MultiPlay.OnAudienceMode();
      }
    }

    private void EndMultiPlayer()
    {
      if (!this.Battle.IsMultiPlay)
        return;
      if (!this.mBeginMultiPlay)
      {
        this.MultiPlayLog("[PUN]not begin EndMultiPlayer");
      }
      else
      {
        this.mBeginMultiPlay = false;
        this.MultiPlayLog("[PUN]EndMultiPlayer*******");
        for (int index = 0; index < this.mMultiPlayerUnit.Count; ++index)
          this.mMultiPlayerUnit[index].End(this);
        for (int index = 0; index < this.mMultiPlayer.Count; ++index)
          this.mMultiPlayer[index].End(this);
        this.MultiPlayInputTimeLimit = 0.0f;
      }
    }

    private SceneBattle.MultiPlayRecvData ConvertInputToReceiveData(
      SceneBattle.EMultiPlayRecvDataHeader header,
      int unitID,
      List<SceneBattle.MultiPlayInput> sendList)
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      MyPhoton.MyPlayer myPlayer = instance.GetMyPlayer();
      int myPlayerIndex = instance.MyPlayerIndex;
      int playerId = myPlayer != null ? myPlayer.playerID : 0;
      if (sendList == null)
        sendList = new List<SceneBattle.MultiPlayInput>();
      if (sendList.Count <= 0)
        sendList.Add(new SceneBattle.MultiPlayInput()
        {
          c = 0
        });
      List<SceneBattle.MultiPlayInput> multiPlayInputList1 = new List<SceneBattle.MultiPlayInput>();
      if (header == SceneBattle.EMultiPlayRecvDataHeader.INPUT)
      {
        List<SceneBattle.MultiPlayInput> multiPlayInputList2 = new List<SceneBattle.MultiPlayInput>();
        SceneBattle.MultiPlayInput multiPlayInput = (SceneBattle.MultiPlayInput) null;
        foreach (SceneBattle.MultiPlayInput send in sendList)
        {
          if (send.c == 2)
          {
            multiPlayInput = send;
          }
          else
          {
            if (multiPlayInput != null)
            {
              multiPlayInputList2.Add(multiPlayInput);
              multiPlayInput = (SceneBattle.MultiPlayInput) null;
            }
            multiPlayInputList2.Add(send);
          }
        }
        foreach (SceneBattle.MultiPlayInput send in sendList)
        {
          SceneBattle.MultiPlayInput input = send;
          if (input.c == 6)
          {
            multiPlayInputList1.Add(input);
          }
          else
          {
            multiPlayInputList1.RemoveAll((Predicate<SceneBattle.MultiPlayInput>) (d => d.c == input.c));
            multiPlayInputList1.Add(input);
          }
        }
        if (multiPlayInput != null)
        {
          multiPlayInputList2.Add(multiPlayInput);
          this.MultiPlayLog("send lastMove x:" + (object) multiPlayInput.x + " z:" + (object) multiPlayInput.z);
        }
        sendList = multiPlayInputList1;
      }
      SceneBattle.MultiPlayRecvData receiveData = new SceneBattle.MultiPlayRecvData();
      SceneBattle.MultiPlayInput def = new SceneBattle.MultiPlayInput();
      receiveData.h = (int) header;
      receiveData.b = this.UnitStartCountTotal;
      receiveData.sq = this.mMultiPlaySendID;
      receiveData.pidx = myPlayerIndex;
      receiveData.pid = playerId;
      receiveData.uid = unitID;
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.c != def.c)) != null)
      {
        receiveData.c = new int[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          receiveData.c[index] = sendList[index].c;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.u != def.u)) != null)
      {
        receiveData.u = new int[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          receiveData.u[index] = sendList[index].u;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => !s.s.Equals(def.s))) != null)
      {
        receiveData.s = new string[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          receiveData.s[index] = sendList[index].s;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => !s.i.Equals(def.i))) != null)
      {
        receiveData.i = new string[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          receiveData.i[index] = sendList[index].i;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.gx != def.gx)) != null)
      {
        receiveData.gx = new int[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          receiveData.gx[index] = sendList[index].gx;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.gy != def.gy)) != null)
      {
        receiveData.gy = new int[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          receiveData.gy[index] = sendList[index].gy;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.ul != def.ul)) != null)
      {
        receiveData.ul = new int[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          receiveData.ul[index] = sendList[index].ul;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.d != def.d)) != null)
      {
        receiveData.d = new int[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          receiveData.d[index] = sendList[index].d;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => (double) s.x != (double) def.x)) != null)
      {
        receiveData.x = new float[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          receiveData.x[index] = sendList[index].x;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => (double) s.z != (double) def.z)) != null)
      {
        receiveData.z = new float[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          receiveData.z[index] = sendList[index].z;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => (double) s.r != (double) def.r)) != null)
      {
        receiveData.r = new float[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          receiveData.r[index] = sendList[index].r;
      }
      return receiveData;
    }

    private byte[] CreateSendBinary(SceneBattle.MultiPlayRecvData recv)
    {
      return GameUtility.Object2Binary<SceneBattle.MultiPlayRecvData>(recv);
    }

    private byte[] CreateSendBinary(
      SceneBattle.EMultiPlayRecvDataHeader header,
      int unitID,
      List<SceneBattle.MultiPlayInput> sendList)
    {
      return this.CreateSendBinary(this.ConvertInputToReceiveData(header, unitID, sendList));
    }

    private string CreateMultiPlayInputList(
      SceneBattle.EMultiPlayRecvDataHeader header,
      int unitID,
      List<SceneBattle.MultiPlayInput> sendList)
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      int myPlayerIndex = instance.MyPlayerIndex;
      MyPhoton.MyPlayer myPlayer = instance.GetMyPlayer();
      int playerId = myPlayer != null ? myPlayer.playerID : 0;
      if (sendList == null)
        sendList = new List<SceneBattle.MultiPlayInput>();
      if (sendList.Count <= 0)
        sendList.Add(new SceneBattle.MultiPlayInput()
        {
          c = 0
        });
      List<SceneBattle.MultiPlayInput> multiPlayInputList1 = new List<SceneBattle.MultiPlayInput>();
      if (header == SceneBattle.EMultiPlayRecvDataHeader.INPUT)
      {
        List<SceneBattle.MultiPlayInput> multiPlayInputList2 = new List<SceneBattle.MultiPlayInput>();
        SceneBattle.MultiPlayInput multiPlayInput = (SceneBattle.MultiPlayInput) null;
        foreach (SceneBattle.MultiPlayInput send in sendList)
        {
          if (send.c == 2)
          {
            multiPlayInput = send;
          }
          else
          {
            if (multiPlayInput != null)
            {
              multiPlayInputList2.Add(multiPlayInput);
              multiPlayInput = (SceneBattle.MultiPlayInput) null;
            }
            multiPlayInputList2.Add(send);
          }
        }
        foreach (SceneBattle.MultiPlayInput send in sendList)
        {
          SceneBattle.MultiPlayInput input = send;
          if (input.c == 6)
          {
            multiPlayInputList1.Add(input);
          }
          else
          {
            multiPlayInputList1.RemoveAll((Predicate<SceneBattle.MultiPlayInput>) (d => d.c == input.c));
            multiPlayInputList1.Add(input);
          }
        }
        if (multiPlayInput != null)
        {
          multiPlayInputList2.Add(multiPlayInput);
          this.MultiPlayLog("send lastMove x:" + (object) multiPlayInput.x + " z:" + (object) multiPlayInput.z);
        }
        sendList = multiPlayInputList1;
      }
      SceneBattle.MultiPlayInput def = new SceneBattle.MultiPlayInput();
      ++this.mMultiPlaySendID;
      string str1 = "{\"h\":" + (object) (int) header + ",\"b\":" + (object) this.UnitStartCountTotal + ",\"sq\":" + (object) this.mMultiPlaySendID + ",\"pidx\":" + (object) myPlayerIndex + ",\"pid\":" + (object) playerId + ",\"uid\":" + (object) unitID;
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.c != def.c)) != null)
      {
        string str2 = str1 + ",\"c\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].c;
        str1 = str2 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.u != def.u)) != null)
      {
        string str3 = str1 + ",\"u\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str3 = str3 + (index > 0 ? "," : string.Empty) + (object) sendList[index].u;
        str1 = str3 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => !s.s.Equals(def.s))) != null)
      {
        string str4 = str1 + ",\"s\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str4 = str4 + (index > 0 ? ",\"" : "\"") + JsonEscape.Escape(sendList[index].s) + "\"";
        str1 = str4 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => !s.i.Equals(def.i))) != null)
      {
        string str5 = str1 + ",\"i\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str5 = str5 + (index > 0 ? ",\"" : "\"") + JsonEscape.Escape(sendList[index].i) + "\"";
        str1 = str5 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.gx != def.gx)) != null)
      {
        string str6 = str1 + ",\"gx\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str6 = str6 + (index > 0 ? "," : string.Empty) + (object) sendList[index].gx;
        str1 = str6 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.gy != def.gy)) != null)
      {
        string str7 = str1 + ",\"gy\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str7 = str7 + (index > 0 ? "," : string.Empty) + (object) sendList[index].gy;
        str1 = str7 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.ul != def.ul)) != null)
      {
        string str8 = str1 + ",\"ul\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str8 = str8 + (index > 0 ? "," : string.Empty) + (object) sendList[index].ul;
        str1 = str8 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.d != def.d)) != null)
      {
        string str9 = str1 + ",\"d\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str9 = str9 + (index > 0 ? "," : string.Empty) + (object) sendList[index].d;
        str1 = str9 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => (double) s.x != (double) def.x)) != null)
      {
        string str10 = str1 + ",\"x\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str10 = str10 + (index > 0 ? "," : string.Empty) + (object) sendList[index].x;
        str1 = str10 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => (double) s.z != (double) def.z)) != null)
      {
        string str11 = str1 + ",\"z\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str11 = str11 + (index > 0 ? "," : string.Empty) + (object) sendList[index].z;
        str1 = str11 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => (double) s.r != (double) def.r)) != null)
      {
        string str12 = str1 + ",\"r\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str12 = str12 + (index > 0 ? "," : string.Empty) + (object) sendList[index].r;
        str1 = str12 + "]";
      }
      string multiPlayInputList3 = str1 + "}";
      this.MultiPlayLog("[PUN] send packet sq:" + (object) this.mMultiPlaySendID + " pid:" + (object) playerId + " pidx:" + (object) myPlayerIndex + " h:" + (object) header + " b:" + (object) this.UnitStartCountTotal);
      if (header == SceneBattle.EMultiPlayRecvDataHeader.INPUT)
      {
        int num = 0;
        while (num < sendList.Count)
          ++num;
      }
      Debug.LogWarning((object) ("SendJson:" + (object) multiPlayInputList3.Length));
      return multiPlayInputList3;
    }

    private bool GainMultiPlayInputTimeLimit()
    {
      if (!this.IsInState<SceneBattle.State_MapMoveSelect_Stick>() && !this.IsInState<SceneBattle.State_SelectItemV2>() && !this.IsInState<SceneBattle.State_SelectSkillV2>() && !this.IsInState<SceneBattle.State_SelectGridEventV2>() && !this.IsInState<SceneBattle.State_SelectTargetV2>() && !this.IsInState<SceneBattle.State_InputDirection>() && !this.IsInState<SceneBattle.State_MapWait>() && !this.IsInState<SceneBattle.State_MapCommandV2>() && !this.IsInState<SceneBattle.State_ThrowTargetSelect>())
        return false;
      Unit currentUnit = this.Battle.CurrentUnit;
      if (currentUnit.CastSkill != null && currentUnit.CastSkill.CastType == ECastTypes.Jump)
        return false;
      TacticsUnitController unitController = this.FindUnitController(currentUnit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) && unitController.IsJumpCant())
        return false;
      string s = FlowNode_Variable.Get("DisableTimeLimit");
      return (string.IsNullOrEmpty(s) || long.Parse(s) == 0L) && currentUnit.IsControl;
    }

    private TacticsUnitController ResetMultiPlayerTransform(Unit unit)
    {
      TacticsUnitController unitController = unit != null ? this.FindUnitController(unit) : (TacticsUnitController) null;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        return (TacticsUnitController) null;
      ((Component) unitController).transform.position = this.CalcGridCenter(unit.startX, unit.startY);
      unitController.CancelAction();
      ((Component) unitController).transform.rotation = unit.startDir.ToRotation();
      return unitController;
    }

    public int DisplayMultiPlayInputTimeLimit { get; set; }

    public JSON_MyPhotonPlayerParam CurrentNotifyDisconnectedPlayer { get; set; }

    public JSON_MyPhotonPlayerParam CurrentResumePlayer { get; set; }

    public SceneBattle.ENotifyDisconnectedPlayerType CurrentNotifyDisconnectedPlayerType { get; set; }

    private int GetMultiPlayInputTimeLimit()
    {
      int playInputTimeLimit = (int) this.MultiPlayInputTimeLimit;
      if ((double) this.MultiPlayInputTimeLimit > (double) playInputTimeLimit)
        ++playInputTimeLimit;
      return playInputTimeLimit;
    }

    public int GetNextMyTurn()
    {
      int nextMyTurn = 0;
      for (int index = 0; index < this.Battle.Order.Count; ++index)
      {
        Unit unit = this.Battle.Order[index % this.Battle.Order.Count].Unit;
        if (!unit.IsDead && unit.IsEntry && !unit.IsSub)
        {
          if (unit.OwnerPlayerIndex == this.Battle.MyPlayerIndex)
            return nextMyTurn;
          ++nextMyTurn;
        }
      }
      return -1;
    }

    public string PhotonErrorString => this.mPhotonErrString;

    private void OnSuccessCheat(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        FlowNode_Network.Failed();
      }
      Network.RemoveAPI();
    }

    private void RecvResumeSuccess(SceneBattle.MultiPlayer mp, SceneBattle.MultiPlayRecvData data)
    {
      this.mResumeSend = false;
      if (this.mRecvResumeRequest.Count > 0)
        this.mRecvResumeRequest.RemoveAll((Predicate<SceneBattle.MultiPlayRecvData>) (p => p.pidx == data.pidx));
      this.Battle.ResumeState = this.mRecvResumeRequest.Count > 0 ? BattleCore.RESUME_STATE.WAIT : BattleCore.RESUME_STATE.NONE;
      if (mp != null)
      {
        if (mp.NotifyDisconnected && this.mRecvResume.Find((Predicate<SceneBattle.MultiPlayRecvData>) (d => d.pidx == data.pidx)) == null)
          this.mRecvResume.Add(data);
        mp.Disconnected = false;
        mp.NotifyDisconnected = false;
      }
      this.mRecvCheck.Clear();
      this.mMultiPlayCheckList.Clear();
      this.mRecvCheckData.Clear();
      this.mRecvCheckMyData.Clear();
      if (!this.Battle.IsResume)
        return;
      this.SendResumeInfo();
    }

    private void RecvEvent()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      List<MyPhoton.MyEvent> events = instance.GetEvents();
      if (events == null)
        return;
      while (events.Count > 0)
      {
        Debug.LogWarning((object) events[0].code);
        if (this.mResumeMultiPlay)
        {
          if (events[0].code == MyPhoton.SEND_TYPE.Resume)
          {
            this.StartCoroutine(this.RecvResume(events[0].binary));
          }
          else
          {
            SceneBattle.MultiPlayRecvData data = (SceneBattle.MultiPlayRecvData) null;
            if (GameUtility.Binary2Object<SceneBattle.MultiPlayRecvData>(out data, events[0].binary))
            {
              SceneBattle.MultiPlayer mp = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerID == data.pid)) ?? this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerIndex == data.pidx));
              if (data.h == 4)
                this.mRecvContinue.Add(data);
              else if (data.h == 6)
              {
                if (mp != null)
                  mp.FinishLoad = true;
              }
              else if (data.h == 9)
                this.RecvResumeSuccess(mp, data);
            }
          }
        }
        else if (events[0].code == MyPhoton.SEND_TYPE.Normal)
        {
          if (events[0].binary == null)
          {
            events.RemoveAt(0);
            continue;
          }
          SceneBattle.MultiPlayRecvData data = (SceneBattle.MultiPlayRecvData) null;
          SceneBattle.MultiPlayer mp = (SceneBattle.MultiPlayer) null;
          if (GameUtility.Binary2Object<SceneBattle.MultiPlayRecvData>(out data, events[0].binary))
          {
            mp = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerID == data.pid)) ?? this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerIndex == data.pidx));
            this.MultiPlayLog("[PUN] recv packet sq:" + (object) data.sq + " pid:" + (object) data.pid + " pidx:" + (object) data.pidx + " h:" + (object) (SceneBattle.EMultiPlayRecvDataHeader) data.h + " b:" + (object) data.b + "/" + (object) this.UnitStartCountTotal);
          }
          else
            data = (SceneBattle.MultiPlayRecvData) null;
          if (data != null)
          {
            if (data.h == 4)
              this.mRecvContinue.Add(data);
            else if (data.h == 3)
              this.mRecvGoodJob.Add(data);
            else if (data.h == 2)
            {
              this.mRecvCheck.RemoveAll((Predicate<SceneBattle.MultiPlayRecvData>) (p => p.pidx == data.pidx));
              this.mRecvCheck.Add(data);
              if (!this.mRecvCheckData.ContainsKey(data.b))
                this.mRecvCheckData.Add(data.b, data);
            }
            else if (data.h == 1)
              this.mRecvBattle.Add(data);
            else if (data.h == 5)
              this.mRecvIgnoreMyDisconnect.Add(data);
            else if (data.h == 6)
            {
              if (mp != null)
                mp.FinishLoad = true;
            }
            else if (data.h == 7)
            {
              if (this.mRecvResumeRequest.Find((Predicate<SceneBattle.MultiPlayRecvData>) (d => d.pidx == data.pidx)) == null)
              {
                this.Battle.ResumeState = BattleCore.RESUME_STATE.REQUEST;
                this.mResumeSend = false;
                Debug.Log((object) "*********************");
                Debug.Log((object) "ResumeRequest!!");
                Debug.Log((object) "*********************");
                this.mRecvResumeRequest.Add(data);
              }
            }
            else if (data.h == 9)
              this.RecvResumeSuccess(mp, data);
            else if (data.h == 10)
            {
              if (mp != null)
                mp.SyncWait = true;
            }
            else if (data.h == 11)
            {
              if (mp != null)
                mp.SyncResumeWait = true;
            }
            else if (data.h == 14 || data.h == 15 || data.h == 16)
            {
              if (data.uid == instance.MyPlayerIndex && !this.mCheater)
              {
                int type = 0;
                Unit unit = (Unit) null;
                for (int index = 0; index < this.Battle.AllUnits.Count; ++index)
                {
                  if (this.Battle.AllUnits[index].OwnerPlayerIndex == instance.MyPlayerIndex)
                  {
                    unit = this.Battle.AllUnits[index];
                    break;
                  }
                }
                string val = string.Empty;
                if (unit != null)
                {
                  val = "unit = " + unit.UnitParam.iname;
                  if (data.h == 15)
                  {
                    List<SkillData> battleSkills = unit.BattleSkills;
                    if (battleSkills != null)
                    {
                      for (int index = 0; index < battleSkills.Count; ++index)
                      {
                        int cost = battleSkills[index].Cost;
                        int hpCostRate = battleSkills[index].HpCostRate;
                        val = val + "[" + (object) index + "]:" + battleSkills[index].SkillParam.iname + "cost(mp):" + (object) cost + "cost(hp):" + (object) hpCostRate + ",";
                      }
                    }
                  }
                }
                Network.RequestAPI((WebAPI) new ReqMultiCheat(type, val, new Network.ResponseCallback(this.OnSuccessCheat)));
                this.mCheater = true;
              }
            }
            else if (data.h == 18)
            {
              SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerIndex == data.pidx));
              if (multiPlayer != null)
              {
                if (data.c[0] == 1)
                  multiPlayer.SetAutoTurn(data.b);
                else
                  multiPlayer.ToggleAutoPlay(data.u[0] != 1);
              }
            }
          }
        }
        events.RemoveAt(0);
      }
    }

    private bool DisconnetEvent()
    {
      MyPhoton instance1 = PunMonoSingleton<MyPhoton>.Instance;
      GameManager instance2 = MonoSingleton<GameManager>.Instance;
      if (this.mExecDisconnected && this.Battle.IsMultiTower || instance2.IsVSCpuBattle || instance1.CurrentState == MyPhoton.MyState.ROOM || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null))
        return false;
      if (!this.mExecDisconnected && !this.mIsWaitingForBattleSignal && !BlockInterrupt.IsBlocked(BlockInterrupt.EType.PHOTON_DISCONNECTED))
      {
        this.mExecDisconnected = true;
        this.mBattleUI_MultiPlay.OnMyDisconnected();
        if (this.mDisconnectType == SceneBattle.EDisconnectType.BAN)
        {
          this.MultiPlayLog("OnBan");
          this.mBattleUI.OnBan();
        }
        else if (this.mDisconnectType == SceneBattle.EDisconnectType.DISCONNECTED)
        {
          if (instance1.LastError == MyPhoton.MyError.TIMEOUT2)
          {
            this.mPhotonErrString = "2:TimeOut:" + (object) this.mMultiPlaySendID;
            foreach (SceneBattle.MultiPlayer multiPlayer in this.mMultiPlayer)
            {
              SceneBattle sceneBattle = this;
              sceneBattle.mPhotonErrString = sceneBattle.mPhotonErrString + "-" + multiPlayer.RecvInputNum.ToString();
            }
          }
          else
            this.mPhotonErrString = instance1.LastError != MyPhoton.MyError.TIMEOUT ? (instance1.LastError != MyPhoton.MyError.RAISE_EVENT_FAILED ? "0" : "3:SendFailed") : "1:TimeOut";
          this.MultiPlayLog("OnDisconnected");
          this.mBattleUI.OnDisconnected();
        }
        else
        {
          this.MultiPlayLog("OnSequenceError");
          this.mBattleUI.OnSequenceError();
        }
        this.GotoState<SceneBattle.State_Disconnected>();
      }
      return true;
    }

    private void ShowTimeLimit()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null))
        return;
      bool flag = false;
      if (this.mBeginMultiPlay && this.Battle.CurrentUnit != null && this.Battle.CurrentUnit.OwnerPlayerIndex == this.Battle.MyPlayerIndex && this.GainMultiPlayInputTimeLimit())
        flag = true;
      int playInputTimeLimit = this.GetMultiPlayInputTimeLimit();
      if (flag && this.DisplayMultiPlayInputTimeLimit != playInputTimeLimit)
      {
        this.DisplayMultiPlayInputTimeLimit = playInputTimeLimit;
        GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.MULTI_INPUT_TIME_LIMIT);
      }
      if (playInputTimeLimit <= 0)
        flag = false;
      if (this.mShowInputTimeLimit != flag)
      {
        if (flag)
          this.mBattleUI_MultiPlay.ShowInputTimeLimit();
        else
          this.mBattleUI_MultiPlay.HideInputTimeLimit();
      }
      this.mShowInputTimeLimit = flag;
    }

    private void ShowThinking()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null))
        return;
      int num = 0;
      if ((this.IsInState<SceneBattle.State_MapCommandMultiPlay>() || this.IsInState<SceneBattle.State_MapCommandVersus>()) && this.Battle.CurrentUnit != null && this.Battle.CurrentUnit.OwnerPlayerIndex > 0)
      {
        string s = FlowNode_Variable.Get("DisableThinkingUI");
        if (string.IsNullOrEmpty(s) || long.Parse(s) == 0L)
          num = this.Battle.CurrentUnit.OwnerPlayerIndex;
      }
      if (MonoSingleton<GameManager>.Instance.AudienceMode && this.Battle.CurrentUnit != null && this.Battle.CurrentUnit.OwnerPlayerIndex > 0)
        num = this.Battle.CurrentUnit.OwnerPlayerIndex;
      if (num != this.mThinkingPlayerIndex)
      {
        if (num <= 0 || this.Battle.CurrentUnit.OwnerPlayerIndex == this.Battle.MyPlayerIndex)
          this.mBattleUI_MultiPlay.HideThinking();
        else
          this.mBattleUI_MultiPlay.ShowThinking();
      }
      this.mThinkingPlayerIndex = num;
    }

    private void OtherPlayerDisconnect()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (this.Battle.GetQuestResult() != BattleCore.QuestResult.Pending || this.mExecDisconnected && this.Battle.IsMultiTower)
        return;
      List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList(true);
      if (roomPlayerList != null)
      {
        foreach (SceneBattle.MultiPlayer multiPlayer in this.mMultiPlayer)
        {
          if (roomPlayerList != null && instance.FindPlayer(roomPlayerList, multiPlayer.PlayerID, multiPlayer.PlayerIndex) == null)
          {
            if (instance.IsOldestPlayer() && !multiPlayer.Disconnected)
              this.SendOtherPlayerDisconnect(multiPlayer.PlayerIndex);
            multiPlayer.Disconnected = true;
          }
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null) && !this.mIsWaitingForBattleSignal && !BlockInterrupt.IsBlocked(BlockInterrupt.EType.PHOTON_DISCONNECTED) && this.CurrentNotifyDisconnectedPlayer == null && !this.mExecDisconnected)
      {
        List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
        MyPhoton.MyPlayer myPlayer = instance.GetMyPlayer();
        foreach (SceneBattle.MultiPlayer multiPlayer1 in this.mMultiPlayer)
        {
          SceneBattle.MultiPlayer mp = multiPlayer1;
          if (myPlayersStarted != null && roomPlayerList != null && myPlayer != null && !mp.NotifyDisconnected && instance.FindPlayer(roomPlayerList, mp.PlayerID, mp.PlayerIndex) == null && this.mRecvIgnoreMyDisconnect.Find((Predicate<SceneBattle.MultiPlayRecvData>) (r => r.pid == mp.PlayerID)) == null)
          {
            this.CurrentNotifyDisconnectedPlayer = myPlayersStarted?.Find((Predicate<JSON_MyPhotonPlayerParam>) (sp => sp.playerIndex == mp.PlayerIndex));
            int num1 = 0;
            int num2 = 0;
            foreach (JSON_MyPhotonPlayerParam photonPlayerParam in myPlayersStarted)
            {
              JSON_MyPhotonPlayerParam sp = photonPlayerParam;
              SceneBattle.MultiPlayer multiPlayer2 = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (t => t.PlayerIndex == sp.playerIndex));
              if (multiPlayer2 == null || !multiPlayer2.NotifyDisconnected)
              {
                if (sp.playerIndex < num1 || num1 == 0)
                  num1 = sp.playerIndex;
                if (sp.playerIndex != mp.PlayerIndex && (sp.playerIndex < num2 || num2 == 0))
                  num2 = sp.playerIndex;
              }
            }
            this.CurrentNotifyDisconnectedPlayerType = num1 == mp.PlayerIndex ? (num2 != instance.MyPlayerIndex ? SceneBattle.ENotifyDisconnectedPlayerType.OWNER : SceneBattle.ENotifyDisconnectedPlayerType.OWNER_AND_I_AM_OWNER) : SceneBattle.ENotifyDisconnectedPlayerType.NORMAL;
            if (this.CurrentResumePlayer != null)
              this.mBattleUI_MultiPlay.OnOtherPlayerResumeClose();
            this.mBattleUI_MultiPlay.OnMyPlayerResumeClose();
            mp.NotifyDisconnected = true;
            mp.RecvInputNum = 0;
            this.mBattleUI_MultiPlay.OnOtherDisconnected();
            break;
          }
        }
      }
      if (this.Battle.ResumeState == BattleCore.RESUME_STATE.NONE)
        return;
      for (int index = this.mRecvResumeRequest.Count - 1; index >= 0; --index)
      {
        SceneBattle.MultiPlayRecvData data = this.mRecvResumeRequest[index];
        if (roomPlayerList.Find((Predicate<MyPhoton.MyPlayer>) (p => p.playerID == data.pid)) == null)
          this.mRecvResumeRequest.RemoveAt(index);
      }
      if (this.mRecvResumeRequest.Count != 0)
        return;
      this.Battle.ResumeState = BattleCore.RESUME_STATE.NONE;
      this.mRecvCheck.Clear();
      this.mMultiPlayCheckList.Clear();
    }

    private void OtherPlayerResume()
    {
      if (this.mRecvResume.Count <= 0)
        return;
      List<JSON_MyPhotonPlayerParam> myPlayersStarted = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null) || this.mIsWaitingForBattleSignal || BlockInterrupt.IsBlocked(BlockInterrupt.EType.PHOTON_DISCONNECTED) || this.CurrentResumePlayer != null)
        return;
      SceneBattle.MultiPlayRecvData data = this.mRecvResume[0];
      this.CurrentResumePlayer = myPlayersStarted?.Find((Predicate<JSON_MyPhotonPlayerParam>) (sp => sp.playerIndex == data.pidx));
      if (this.CurrentNotifyDisconnectedPlayer != null)
        this.mBattleUI_MultiPlay.OnOtherDisconnectedForce();
      this.mBattleUI_MultiPlay.OnMyPlayerResumeClose();
      this.mBattleUI_MultiPlay.OnOtherPlayerResume();
      this.mRecvResume.RemoveAt(0);
    }

    private void CheckStart()
    {
      if (this.Battle.SyncStart)
        return;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      this.Battle.SyncStart = true;
      foreach (SceneBattle.MultiPlayer multiPlayer in this.mMultiPlayer)
      {
        if (!multiPlayer.NotifyDisconnected && !multiPlayer.IsSupportAI)
          this.Battle.SyncStart &= multiPlayer.FinishLoad;
      }
      if (this.mResumeMultiPlay)
      {
        List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList(true);
        if (roomPlayerList.Count == 1)
        {
          this.mResumeMultiPlay = false;
          this.mResumeOnlyPlayer = true;
          foreach (SceneBattle.MultiPlayer multiPlayer in this.mMultiPlayer)
            multiPlayer.NotifyDisconnected = true;
          if (this.Battle.IsMultiVersus)
            this.GotoState<SceneBattle.State_ComfirmFinishbattle>();
          else
            this.mBattleUI_MultiPlay.OnMyPlayerResume();
        }
        else
        {
          bool flag = false;
          foreach (int num in this.mResumeAlreadyStartPlayer)
          {
            int playerID = num;
            flag |= roomPlayerList.Find((Predicate<MyPhoton.MyPlayer>) (p => p.playerID == playerID)) != null;
          }
          if (this.Battle.IsMultiTower)
          {
            MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
            if (currentRoom != null && !currentRoom.battle)
            {
              flag = false;
              this.mExecDisconnected = true;
              instance.Disconnect();
              foreach (SceneBattle.MultiPlayerUnit multiPlayerUnit in this.mMultiPlayerUnit)
              {
                if (multiPlayerUnit != null && multiPlayerUnit.Owner.PlayerIndex != instance.MyPlayerIndex)
                  multiPlayerUnit.Owner.Disconnected = true;
              }
            }
          }
          this.mResumeMultiPlay = flag;
          this.Battle.SyncStart = !flag;
        }
      }
      this.Battle.SyncStart &= !this.mResumeMultiPlay;
      if (!this.Battle.SyncStart)
        return;
      instance.AddMyPlayerParam("BattleStart", (object) true);
    }

    private void UpdateMultiBattleInfo()
    {
      while (this.mRecvBattle.Count > 0)
      {
        SceneBattle.MultiPlayRecvData data = this.mRecvBattle[0];
        if (data.b > this.UnitStartCountTotal)
        {
          DebugUtility.LogWarning("[PUN] new turn data. sq:" + (object) data.sq + " h:" + (object) (SceneBattle.EMultiPlayRecvDataHeader) data.h + " b:" + (object) data.b + "/" + (object) this.UnitStartCountTotal + " test:" + (object) this.mRecvBattle.FindIndex((Predicate<SceneBattle.MultiPlayRecvData>) (r => r.b < data.b)));
          break;
        }
        if (data.b < this.UnitStartCountTotal)
          DebugUtility.LogWarning("[PUN] old turn data. sq:" + (object) data.sq + " h:" + (object) (SceneBattle.EMultiPlayRecvDataHeader) data.h + " b:" + (object) data.b + "/" + (object) this.UnitStartCountTotal);
        else if (data.h == 1)
        {
          this.mMultiPlayerUnit.Find((Predicate<SceneBattle.MultiPlayerUnit>) (p => p.UnitID == data.uid))?.RecvInput(this, data);
          if (this.Battle.IsMultiPlay && !PunMonoSingleton<MyPhoton>.Instance.IsOldestPlayer())
            this.mMultiSendLogBuffer.Add(data);
        }
        this.mRecvBattle.RemoveAt(0);
      }
      if (!MonoSingleton<GameManager>.Instance.AudienceMode)
        return;
      for (int i = 0; i < this.mAudienceDisconnect.Count; ++i)
      {
        if (this.mAudienceDisconnect[i].b == this.UnitStartCountTotal)
        {
          SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerIndex == this.mAudienceDisconnect[i].uid));
          this.mAudienceRetire = this.mAudienceDisconnect[i];
          multiPlayer.Disconnected = true;
          List<SceneBattle.MultiPlayRecvData> audienceDisconnect = this.mAudienceDisconnect;
          int num;
          i = (num = i) - 1;
          int index = num;
          audienceDisconnect.RemoveAt(index);
        }
      }
      if (this.mAudienceRetire == null || this.mAudienceRetire.b != this.UnitStartCountTotal || this.mRecvBattle.Count > 0)
        return;
      SceneBattle.MultiPlayer multiPlayer1 = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerID != this.mAudienceRetire.pid));
      if (multiPlayer1 != null)
        multiPlayer1.NotifyDisconnected = true;
      this.mAudienceRetire = (SceneBattle.MultiPlayRecvData) null;
      this.Battle.IsVSForceWin = true;
      this.GotoState_WaitSignal<SceneBattle.State_AudienceRetire>();
    }

    private bool CheckInputTimeLimit()
    {
      if (this.Battle.IsResume || (double) this.MultiPlayInputTimeLimit == 0.0)
        return true;
      if ((double) this.MultiPlayInputTimeLimit > 0.0)
      {
        if (!this.GainMultiPlayInputTimeLimit())
          return true;
        this.MultiPlayInputTimeLimit -= Time.unscaledDeltaTime;
        if ((double) this.MultiPlayInputTimeLimit > 0.0)
          return true;
        this.MultiPlayInputTimeLimit = 0.0f;
        this.MultiPlayLog("[PUN]TimeUp!");
      }
      return false;
    }

    private void SendTimeLimit()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode)
        return;
      if (this.mIsWaitingForBattleSignal)
      {
        this.MultiPlayLog("[PUN]TimeUp but waiting for battle signal...");
        this.MultiPlayInputTimeLimit = -1f;
      }
      else
      {
        this.MultiPlayInputTimeLimit = 0.0f;
        if (this.IsInState<SceneBattle.State_MapMoveSelect_Stick>())
        {
          this.Battle.EntryBattleMultiPlayTimeUp = true;
        }
        else
        {
          if (!this.Battle.CurrentUnit.IsUnitFlag(EUnitFlag.Moved))
          {
            TacticsUnitController tacticsUnitController = this.ResetMultiPlayerTransform(this.Battle.CurrentUnit);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) tacticsUnitController, (UnityEngine.Object) null))
              this.SetCameraTarget((Component) tacticsUnitController);
            this.SendInputMoveEnd(this.Battle.CurrentUnit, true);
          }
          this.SendInputUnitTimeLimit(this.Battle.CurrentUnit);
          this.SendInputFlush();
          this.CloseBattleUI();
          this.Battle.EntryBattleMultiPlayTimeUp = true;
          this.GotoMapCommand();
        }
      }
    }

    public bool CheckSync()
    {
      List<MyPhoton.MyPlayer> roomPlayerList = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
      bool flag = true;
      foreach (MyPhoton.MyPlayer myPlayer in roomPlayerList)
      {
        MyPhoton.MyPlayer player = myPlayer;
        SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerID == player.playerID));
        if (player.start && multiPlayer != null && !multiPlayer.NotifyDisconnected)
        {
          flag &= multiPlayer.SyncWait;
          Debug.Log((object) (multiPlayer.SyncWait.ToString() + "/" + (object) multiPlayer.PlayerIndex));
        }
      }
      return flag;
    }

    public void ResetSync()
    {
      foreach (SceneBattle.MultiPlayer multiPlayer in this.mMultiPlayer)
      {
        multiPlayer.SyncWait = false;
        multiPlayer.SyncResumeWait = false;
      }
    }

    public bool CheckResumeSync()
    {
      List<MyPhoton.MyPlayer> roomPlayerList = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
      bool flag = true;
      foreach (MyPhoton.MyPlayer myPlayer in roomPlayerList)
      {
        MyPhoton.MyPlayer player = myPlayer;
        SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerID == player.playerID));
        if (player.start && multiPlayer != null && !multiPlayer.NotifyDisconnected)
          flag &= multiPlayer.SyncResumeWait;
      }
      return flag;
    }

    private void UpdateMultiPlayer()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || !this.Battle.IsMultiPlay || !this.Battle.MultiFinishLoad)
        return;
      this.UpdateInterval();
      this.RecvEvent();
      if (this.DisconnetEvent())
        return;
      this.SendRequestResume();
      this.SendResumeInfo();
      this.CheckStart();
      this.ShowTimeLimit();
      this.ShowThinking();
      this.OtherPlayerDisconnect();
      this.OtherPlayerResume();
      if (!this.mBeginMultiPlay)
        return;
      this.CheckMultiPlay();
      this.UpdateGoodJob();
      this.UpdateMultiBattleInfo();
      for (int index = 0; index < this.mMultiPlayer.Count; ++index)
        this.mMultiPlayer[index].Update(this);
      for (int index = 0; index < this.mMultiPlayerUnit.Count; ++index)
        this.mMultiPlayerUnit[index].Update(this);
      this.mSendTime += Time.deltaTime;
      if ((double) this.mSendTime >= (double) this.SEND_TURN_SEC)
        this.SendInputFlush();
      if (this.CheckInputTimeLimit())
        return;
      this.SendTimeLimit();
    }

    private bool RecvEventAudience(bool isSkip = false)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int num1 = -1;
      int num2 = -1;
      int cnt = 0;
      bool flag = true;
      bool check = false;
      while (true)
      {
        SceneBattle.MultiPlayRecvData data;
        do
        {
          do
          {
            if (isSkip && instance.AudienceManager.IsSkipEnd)
            {
              flag = false;
              goto label_20;
            }
            else
            {
              data = (SceneBattle.MultiPlayRecvData) null;
              data = instance.AudienceManager.GetData();
              ++cnt;
              if (data == null)
                goto label_20;
            }
          }
          while (data.b == 0 || data.h == 10 || data.h == 11 || data.h == 9);
          if (isSkip && num2 >= 0 && data.b != num2)
          {
            instance.AudienceManager.Restore();
            goto label_20;
          }
          else
          {
            num2 = data.b;
            SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerID == data.pid));
            if (multiPlayer != null && !isSkip)
            {
              if (num1 == -1)
                num1 = multiPlayer.PlayerID;
              else if (num1 != multiPlayer.PlayerID)
              {
                instance.AudienceManager.Restore();
                goto label_20;
              }
            }
            if (data.h == 1)
              this.mRecvBattle.Add(data);
            else if (data.h == 12)
              this.mAudienceRetire = data;
            else if (data.h == 17)
              this.mAudienceDisconnect.Add(data);
          }
        }
        while (data == null || data.h != 2);
        check = true;
      }
label_20:
      return flag & this.CheckSkipLogEnd(isSkip, cnt, check);
    }

    private bool CheckSkipLogEnd(bool isSkip, int cnt, bool check)
    {
      if (isSkip)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if (!check)
        {
          for (int index = 0; index < cnt; ++index)
            instance.AudienceManager.Restore();
          this.mRecvBattle.Clear();
        }
      }
      return check;
    }

    private void UpdateAudiencePlayer()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!instance.AudienceMode || !this.Battle.MultiFinishLoad || !this.QuestStart)
        return;
      this.RecvEventAudience();
      this.ShowThinking();
      this.UpdateMultiBattleInfo();
      for (int index = 0; index < this.mMultiPlayerUnit.Count; ++index)
        this.mMultiPlayerUnit[index].Update(this);
      if (!instance.AudienceManager.IsEnd || this.Battle.RemainVersusTurnCount == 0U || this.IsInState<SceneBattle.State_AudienceForceEnd>() || this.IsInState<SceneBattle.State_AudienceEnd>() || this.IsInState<SceneBattle.State_AudienceRetire>() || this.IsInState<SceneBattle.State_ExitQuest>() || this.CheckAudienceResult() != BattleCore.QuestResult.Pending)
        return;
      this.GotoState<SceneBattle.State_AudienceForceEnd>();
    }

    public void SkipLog()
    {
      if (this.ReqCreateBreakObjUcLists.Count != 0 || this.ReqCreateDtuUcLists.Count != 0)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!this.RecvEventAudience(true))
      {
        this.Battle.MultiFinishLoad = true;
        instance.AudienceManager.SkipMode = false;
        this.Battle.StartOrder(false, false);
        this.Battle.RemainVersusTurnCount = (uint) this.UnitStartCountTotal;
        this.ArenaActionCountSet(this.Battle.RemainVersusTurnCount);
      }
      else
      {
        ++this.mUnitStartCount;
        ++this.mUnitStartCountTotal;
        int num = 0;
        while (num < 256 && this.Battle.UnitStart())
          ++num;
        this.IsNoCountUpUnitStartCount = false;
        this.BeginMultiPlayer();
        for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTacticsUnits[index], (UnityEngine.Object) null))
            this.mTacticsUnits[index].UpdateBadStatus();
        }
        this.UpdateMultiBattleInfo();
        for (int index = 0; index < this.mMultiPlayerUnit.Count; ++index)
          this.mMultiPlayerUnit[index].UpdateSkip(this);
        this.Battle.CommandWait();
        if (this.Battle.GetQuestResult() == BattleCore.QuestResult.Pending)
          this.Battle.UnitEnd();
        for (BattleCore.OrderData currentOrderData = this.Battle.CurrentOrderData; currentOrderData != null && currentOrderData.IsCastSkill; currentOrderData = this.Battle.CurrentOrderData)
        {
          Unit currentUnit = this.Battle.CurrentUnit;
          SkillData castSkill = currentUnit.CastSkill;
          int x = 0;
          int y = 0;
          if (castSkill != null)
          {
            if (currentUnit.UnitTarget != null)
            {
              x = currentUnit.UnitTarget.x;
              y = currentUnit.UnitTarget.y;
            }
            if (currentUnit.GridTarget != null)
            {
              x = currentUnit.GridTarget.x;
              y = currentUnit.GridTarget.y;
            }
          }
          this.Battle.CastSkillStart();
          this.Battle.CastSkillEnd();
          if (castSkill != null)
          {
            if (castSkill.IsSetBreakObjSkill())
            {
              Unit gimmickAtGrid = this.Battle.FindGimmickAtGrid(x, y);
              if (gimmickAtGrid != null && gimmickAtGrid.IsBreakObj)
                this.ReqCreateBreakObjUcLists.Add(new SceneBattle.ReqCreateBreakObjUc(castSkill, gimmickAtGrid));
            }
            else if (castSkill.IsDynamicTransformSkill())
            {
              Unit unitAtGrid = this.Battle.FindUnitAtGrid(x, y);
              if (unitAtGrid != null && unitAtGrid.IsUnitFlag(EUnitFlag.IsDynamicTransform))
                this.ReqCreateDtuUcLists.Add(new SceneBattle.ReqCreateDtuUc(castSkill, unitAtGrid));
            }
          }
        }
        for (int index = 0; index < this.Battle.AllUnits.Count; ++index)
        {
          TacticsUnitController unitController = this.FindUnitController(this.Battle.AllUnits[index]);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          {
            if (this.Battle.AllUnits[index].IsGimmick && !this.Battle.AllUnits[index].IsBreakObj)
            {
              if (this.Battle.AllUnits[index].IsDisableGimmick())
                ((Component) unitController).gameObject.SetActive(false);
            }
            else
            {
              if (this.Battle.AllUnits[index].IsBreakObj)
                unitController.ReflectDispModel();
              if (unitController.Unit.IsDead)
              {
                unitController.SetVisible(false);
                unitController.ShowHPGauge(false);
                unitController.ShowVersusCursor(false);
                if (!unitController.Unit.IsUnitFlag(EUnitFlag.UnitTransformed) && !unitController.Unit.IsUnitFlag(EUnitFlag.IsDynamicTransform))
                {
                  this.mTacticsUnits.Remove(unitController);
                  UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) unitController).gameObject);
                }
              }
              else
              {
                unitController.SetVisible(true);
                unitController.ShowHPGauge(true);
                unitController.ShowVersusCursor(true);
              }
            }
          }
        }
        for (int index = this.mTacticsUnits.Count - 1; index >= 0; --index)
        {
          TacticsUnitController tuc = this.mTacticsUnits[index];
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) tuc))
          {
            if (this.Battle.AllUnits.Find((Predicate<Unit>) (unit => unit == tuc.Unit)) == null)
            {
              tuc.SetVisible(false);
              tuc.ShowHPGauge(false);
              tuc.ShowVersusCursor(false);
              UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) tuc).gameObject);
            }
            else
              continue;
          }
          this.mTacticsUnits.RemoveAt(index);
        }
        if (this.ReqCreateBreakObjUcLists.Count != 0)
        {
          for (int index = 0; index < this.ReqCreateBreakObjUcLists.Count; ++index)
          {
            SceneBattle.ReqCreateBreakObjUc rcb = this.ReqCreateBreakObjUcLists[index];
            this.StartCoroutine(SceneBattle.State_PrepareSkill.loadBreakObjUnit(this, rcb.mTargetUnit, (Action) (() =>
            {
              TacticsUnitController unitController = this.FindUnitController(rcb.mTargetUnit);
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController))
                unitController.SetVisible(true);
              this.ReqCreateBreakObjUcLists.Remove(rcb);
            })));
          }
        }
        if (this.ReqCreateDtuUcLists.Count != 0)
        {
          for (int index = 0; index < this.ReqCreateDtuUcLists.Count; ++index)
          {
            SceneBattle.ReqCreateDtuUc rcd = this.ReqCreateDtuUcLists[index];
            TacticsUnitController tuc = this.FindUnitController(rcd.mTargetUnit);
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) tuc))
            {
              tuc.SetVisible(true);
              this.ReqCreateDtuUcLists.Remove(rcd);
            }
            else
              this.StartCoroutine(SceneBattle.State_PrepareSkill.loadTransformUnit(this, rcd.mTargetUnit, callback: (Action) (() =>
              {
                tuc = this.FindUnitController(rcd.mTargetUnit);
                if (UnityEngine.Object.op_Implicit((UnityEngine.Object) tuc))
                  tuc.SetVisible(true);
                this.ReqCreateDtuUcLists.Remove(rcd);
              })));
          }
        }
        this.Battle.Logs.Reset();
        this.EndMultiPlayer();
      }
    }

    private void SendAuto()
    {
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      if (this.CurrentQuest.type != QuestTypes.Multi && this.CurrentQuest.type != QuestTypes.MultiGps && this.CurrentQuest.type != QuestTypes.MultiTower && this.CurrentQuest.type != QuestTypes.VersusFree && this.CurrentQuest.type != QuestTypes.VersusRank && this.CurrentQuest.type != QuestTypes.RankMatch)
        return;
      MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
      if (instance1.AudienceMode || instance1.IsVSCpuBattle || !this.Battle.IsAutoBattle)
        return;
      byte[] sendBinary = this.CreateSendBinary(this.ConvertInputToReceiveData(SceneBattle.EMultiPlayRecvDataHeader.START_AUTO, this.mCurrentSendInputUnitID, new List<SceneBattle.MultiPlayInput>()
      {
        new SceneBattle.MultiPlayInput() { c = 1 }
      }));
      instance2.SendRoomMessageBinary(true, sendBinary);
      instance2.SendFlush();
    }

    public void ToggleAutoPlay(bool enable)
    {
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      if (this.CurrentQuest.type != QuestTypes.Multi && this.CurrentQuest.type != QuestTypes.MultiGps && this.CurrentQuest.type != QuestTypes.MultiTower && this.CurrentQuest.type != QuestTypes.VersusFree && this.CurrentQuest.type != QuestTypes.VersusRank && this.CurrentQuest.type != QuestTypes.RankMatch)
        return;
      MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
      if (instance1.AudienceMode || instance1.IsVSCpuBattle)
        return;
      this.IsAutoForDisplay = enable;
      byte[] sendBinary = this.CreateSendBinary(this.ConvertInputToReceiveData(SceneBattle.EMultiPlayRecvDataHeader.START_AUTO, this.mCurrentSendInputUnitID, new List<SceneBattle.MultiPlayInput>()
      {
        new SceneBattle.MultiPlayInput()
        {
          c = 2,
          u = !enable ? 1 : 2
        }
      }));
      instance2.SendRoomMessageBinary(true, sendBinary);
      instance2.SendFlush();
    }

    private void SendInputFlush(bool force = false)
    {
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      if (instance1.AudienceMode || instance1.IsVSCpuBattle)
        return;
      MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
      if (this.mSendList.Count > 0)
      {
        SceneBattle.MultiPlayRecvData receiveData = this.ConvertInputToReceiveData(SceneBattle.EMultiPlayRecvDataHeader.INPUT, this.mCurrentSendInputUnitID, this.mSendList);
        byte[] sendBinary = this.CreateSendBinary(receiveData);
        instance2.SendRoomMessageBinary(true, sendBinary);
        if (force)
          instance2.SendFlush();
        if (this.Battle.IsMultiPlay && !instance2.IsOldestPlayer())
          this.mMultiSendLogBuffer.Add(receiveData);
      }
      this.mSendList.Clear();
      this.mSendTime = 0.0f;
    }

    private void SendInputMove(Unit unit, TacticsUnitController controller)
    {
    }

    private void SendInputGridXY(Unit unit, int gridX, int gridY, EUnitDirection dir, bool send = true)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.AudienceMode || instance.IsVSCpuBattle || !this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp || this.mPrevGridX == gridX && this.mPrevGridY == gridY && this.mPrevDir == dir)
        return;
      this.mSendList.Add(new SceneBattle.MultiPlayInput()
      {
        c = 3,
        gx = gridX,
        gy = gridY,
        d = (int) dir
      });
      this.MultiPlayLog("[PUN] SendInputGridXY x:" + (object) gridX + " y:" + (object) gridY);
      if (send)
        this.SendInputFlush(true);
      this.mPrevGridX = gridX;
      this.mPrevGridY = gridY;
      this.mPrevDir = dir;
    }

    private void SendInputMoveStart(Unit unit)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.AudienceMode || instance.IsVSCpuBattle || !this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
        return;
      this.mSendList.Add(new SceneBattle.MultiPlayInput()
      {
        c = 1,
        gx = unit.x,
        gy = unit.y
      });
    }

    private void SendInputMoveEnd(Unit unit, bool cancel)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.AudienceMode || instance.IsVSCpuBattle || !this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
        return;
      this.mSendList.Add(new SceneBattle.MultiPlayInput()
      {
        c = !cancel ? 4 : 5,
        gx = unit.x,
        gy = unit.y,
        d = (int) unit.Direction
      });
      this.MultiPlayLog("[PUN] SendInputMoveEnd cancel:" + (object) cancel);
    }

    private void SendInputEntryBattle(
      EBattleCommand type,
      Unit unit,
      Unit enemy,
      SkillData skill,
      ItemData item,
      int gx,
      int gy,
      bool bUnitLockTarget)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.AudienceMode || instance.IsVSCpuBattle || !this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
        return;
      this.SendInputUnitXYDir(unit, unit.x, unit.y, unit.Direction);
      SceneBattle.MultiPlayInput multiPlayInput = new SceneBattle.MultiPlayInput();
      multiPlayInput.c = 6;
      if (enemy != null)
        multiPlayInput.u = this.Battle.AllUnits.FindIndex((Predicate<Unit>) (u => u == enemy));
      if (skill != null)
      {
        multiPlayInput.s = skill.SkillID;
        if (skill.CastType == ECastTypes.Jump)
          this.MultiPlayInputTimeLimit = 0.0f;
      }
      if (item != null)
        multiPlayInput.i = item.ItemID;
      multiPlayInput.gx = gx;
      multiPlayInput.gy = gy;
      multiPlayInput.d = (int) type;
      multiPlayInput.ul = !bUnitLockTarget ? 0 : 1;
      this.mSendList.Add(multiPlayInput);
      this.MultiPlayLog("[PUN] SendInputEntryBattle (" + (object) unit.x + "," + (object) unit.y + ")" + (object) unit.Direction + "(" + (enemy != null ? (object) enemy.UnitName : (object) "null") + "[" + (object) multiPlayInput.u + "]," + (skill != null ? (object) skill.SkillID : (object) "null") + "," + (item != null ? (object) item.ItemID : (object) "null") + ")");
      this.SendInputFlush();
    }

    private void SendInputGridEvent(Unit unit)
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || !this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
        return;
      if (!this.Battle.CurrentUnit.IsUnitFlag(EUnitFlag.Moved))
        DebugUtility.LogWarning("SendInputGridEvent not moved");
      this.mSendList.Add(new SceneBattle.MultiPlayInput()
      {
        c = 7,
        gx = unit.x,
        gy = unit.y,
        d = (int) unit.Direction
      });
      this.MultiPlayLog("[PUN] SendInputUnitTimeLimit");
    }

    private void SendInputUnitEnd(Unit unit, EUnitDirection dir)
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || !this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
        return;
      if (!this.Battle.CurrentUnit.IsUnitFlag(EUnitFlag.Moved))
        DebugUtility.LogWarning("SendInputUnitEnd not moved");
      this.mSendList.Add(new SceneBattle.MultiPlayInput()
      {
        c = 8,
        gx = unit.x,
        gy = unit.y,
        d = (int) dir
      });
      this.MultiPlayLog("[PUN] SendInputUnitEnd");
      this.SendInputFlush();
      this.MultiPlayInputTimeLimit = 0.0f;
    }

    private void SendInputUnitTimeLimit(Unit unit)
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || !this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
        return;
      this.mSendList.Add(new SceneBattle.MultiPlayInput()
      {
        c = 9,
        gx = unit.x,
        gy = unit.y,
        d = (int) unit.Direction
      });
      this.MultiPlayLog("[PUN] SendInputUnitTimeLimit");
    }

    private void SendInputUnitXYDir(Unit unit, int gridX, int gridY, EUnitDirection dir)
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || !this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
        return;
      this.mSendList.Add(new SceneBattle.MultiPlayInput()
      {
        c = 10,
        gx = gridX,
        gy = gridY,
        d = (int) dir
      });
      this.MultiPlayLog("[PUN] SendInputUnitXYDir x:" + (object) gridX + " y:" + (object) gridY + " dir:" + (object) dir);
    }

    private bool SendIgnoreMyDisconnect()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || MonoSingleton<GameManager>.Instance.IsVSCpuBattle || !this.Battle.IsMultiPlay)
        return true;
      byte[] sendBinary = this.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.IGNORE_MY_DISCONNECT, 0, (List<SceneBattle.MultiPlayInput>) null);
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      instance.SendRoomMessageBinary(true, sendBinary);
      instance.SendFlush();
      this.MultiPlayLog("[PUN]SendIgnoreMyDisconnect");
      return true;
    }

    public void SendOtherPlayerDisconnect(int uid)
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || !this.Battle.IsMultiPlay)
        return;
      byte[] sendBinary = this.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.OTHERPLAYER_DISCONNECT, uid, (List<SceneBattle.MultiPlayInput>) null);
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      instance.SendRoomMessageBinary(true, sendBinary);
      instance.SendFlush();
      this.MultiPlayLog("[PUN]SendIgnoreMyDisconnect");
    }

    public void ResetCheckData()
    {
      this.mRecvCheck.Clear();
      this.mMultiPlayCheckList.Clear();
    }

    private bool SendCheckMultiPlay()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || !this.Battle.IsMultiPlay)
        return true;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      bool flag = !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null) && this.mBattleUI_MultiPlay.CheckRandCheat;
      List<SceneBattle.MultiPlayInput> sendList = new List<SceneBattle.MultiPlayInput>();
      for (int index = 0; index < this.Battle.AllUnits.Count; ++index)
      {
        Unit allUnit = this.Battle.AllUnits[index];
        SceneBattle.MultiPlayInput multiPlayInput = new SceneBattle.MultiPlayInput();
        multiPlayInput.c = (int) allUnit.CurrentStatus.param.hp;
        multiPlayInput.gx = allUnit.x;
        multiPlayInput.gy = allUnit.y;
        multiPlayInput.d = (int) allUnit.Direction;
        RandXorshift randXorshift1 = !flag ? (RandXorshift) null : this.Battle.CloneRand();
        RandXorshift randXorshift2 = !flag ? (RandXorshift) null : this.Battle.CloneRandDamage();
        uint num1 = randXorshift1 != null ? randXorshift1.Get() : 0U;
        uint num2 = randXorshift2 != null ? randXorshift2.Get() : 0U;
        multiPlayInput.s = num1.ToString() + " / " + num2.ToString();
        sendList.Add(multiPlayInput);
      }
      byte[] sendBinary = this.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.CHECK, 0, sendList);
      if (instance.IsOldestPlayer())
      {
        instance.SendRoomMessageBinary(true, sendBinary);
        instance.SendFlush();
      }
      SceneBattle.MultiPlayRecvData data = (SceneBattle.MultiPlayRecvData) null;
      if (GameUtility.Binary2Object<SceneBattle.MultiPlayRecvData>(out data, sendBinary))
      {
        this.mRecvCheck.RemoveAll((Predicate<SceneBattle.MultiPlayRecvData>) (p => p.pidx == data.pidx));
        this.mRecvCheck.Add(data);
        if (!this.mRecvCheckMyData.ContainsKey(data.b))
          this.mRecvCheckMyData.Add(data.b, data);
      }
      Debug.Log((object) ("*****SendCheckData******::" + (object) data.b));
      return true;
    }

    private bool CheckMultiPlay()
    {
      if (!this.Battle.IsMultiPlay || this.mRecvCheckData.Count <= 0 || this.mRecvCheckMyData.Count <= 0)
        return true;
      bool flag1 = true;
      SceneBattle.MultiPlayRecvData multiPlayRecvData1 = (SceneBattle.MultiPlayRecvData) null;
      List<KeyValuePair<int, SceneBattle.MultiPlayRecvData>> list = this.mRecvCheckMyData.ToList<KeyValuePair<int, SceneBattle.MultiPlayRecvData>>();
      SequenceErrorSendLogMessage errorSendLogMessage = new SequenceErrorSendLogMessage();
      foreach (KeyValuePair<int, SceneBattle.MultiPlayRecvData> keyValuePair in list)
      {
        SceneBattle.MultiPlayCheck multiPlayCheck = new SceneBattle.MultiPlayCheck();
        SceneBattle.MultiPlayRecvData multiPlayRecvData2 = keyValuePair.Value;
        multiPlayCheck.playerID = multiPlayRecvData2.pid;
        multiPlayCheck.playerIndex = multiPlayRecvData2.pidx;
        multiPlayCheck.battleTurn = multiPlayRecvData2.b;
        multiPlayCheck.hp = multiPlayRecvData2.c;
        multiPlayCheck.gx = multiPlayRecvData2.gx;
        multiPlayCheck.gy = multiPlayRecvData2.gy;
        multiPlayCheck.dir = multiPlayRecvData2.d;
        multiPlayCheck.rnd = multiPlayRecvData2.s == null || multiPlayRecvData2.s.Length <= 0 ? (string) null : multiPlayRecvData2.s[0];
        if (this.mRecvCheckData.TryGetValue(keyValuePair.Key, out multiPlayRecvData1))
        {
          SceneBattle.MultiPlayCheck host = new SceneBattle.MultiPlayCheck();
          host.playerID = multiPlayRecvData1.pid;
          host.playerIndex = multiPlayRecvData1.pidx;
          host.battleTurn = multiPlayRecvData1.b;
          host.hp = multiPlayRecvData1.c;
          host.gx = multiPlayRecvData1.gx;
          host.gy = multiPlayRecvData1.gy;
          host.dir = multiPlayRecvData1.d;
          host.rnd = multiPlayRecvData1.s == null || multiPlayRecvData1.s.Length <= 0 ? (string) null : multiPlayRecvData1.s[0];
          bool flag2 = host.IsEqual(multiPlayCheck);
          if (!flag2)
            errorSendLogMessage.Add(multiPlayCheck, host);
          flag1 &= flag2;
          if (!flag1)
          {
            string msg = string.Empty;
            for (int index = 0; index < this.Battle.AllUnits.Count; ++index)
              msg = msg + this.Battle.AllUnits[index].UnitName + "-pos:" + (object) this.Battle.AllUnits[index].x + "," + (object) this.Battle.AllUnits[index].y;
            DebugUtility.LogWarning(msg);
          }
          this.mRecvCheckMyData.Remove(keyValuePair.Key);
          this.mRecvCheckData.Remove(keyValuePair.Key);
          DebugUtility.LogWarning(multiPlayCheck.ToString());
          DebugUtility.LogWarning(host.ToString());
        }
      }
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (flag1)
      {
        Debug.Log((object) "Check OK!");
        return true;
      }
      errorSendLogMessage.AddLog(this.mMultiSendLogBuffer);
      errorSendLogMessage.Send();
      this.mDisconnectType = SceneBattle.EDisconnectType.BAN;
      instance.Disconnect();
      return false;
    }

    private SRPG_TouchInputModule GetTouchInputModule()
    {
      GameObject gameObject = GameObject.Find("EVENTSYSTEM");
      return UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null) ? (SRPG_TouchInputModule) null : gameObject.GetComponent<SRPG_TouchInputModule>();
    }

    public void SetupGoodJob()
    {
      if (this.mSetupGoodJob)
        return;
      SRPG_TouchInputModule touchInputModule = this.GetTouchInputModule();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) touchInputModule, (UnityEngine.Object) null))
        return;
      touchInputModule.OnDoubleTap += new SRPG_TouchInputModule.OnDoubleTapDelegate(this.OnDoubleTap);
      this.mSetupGoodJob = true;
    }

    public void CleanupGoodJob()
    {
      if (!this.mSetupGoodJob)
        return;
      SRPG_TouchInputModule touchInputModule = this.GetTouchInputModule();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) touchInputModule, (UnityEngine.Object) null))
        return;
      touchInputModule.OnDoubleTap -= new SRPG_TouchInputModule.OnDoubleTapDelegate(this.OnDoubleTap);
      this.mSetupGoodJob = false;
    }

    private void AddGoodJob(int gx, int gy, int unitID = -1)
    {
      if (!this.Battle.IsMultiPlay || (double) this.mGoodJobWait > 0.0)
        return;
      this.mGoodJobWait = 1f;
      if (gx < 0 || gx >= this.Battle.CurrentMap.Width || gy < 0 || gy >= this.Battle.CurrentMap.Height)
        return;
      BattleStamp stampWindow = !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null) ? this.mBattleUI_MultiPlay.StampWindow : (BattleStamp) null;
      byte[] sendBinary = this.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.GOODJOB, unitID, new List<SceneBattle.MultiPlayInput>()
      {
        new SceneBattle.MultiPlayInput()
        {
          gx = gx,
          gy = gy,
          c = !UnityEngine.Object.op_Equality((UnityEngine.Object) stampWindow, (UnityEngine.Object) null) ? stampWindow.SelectStampID : -1
        }
      });
      PunMonoSingleton<MyPhoton>.Instance.SendRoomMessageBinary(true, sendBinary);
      SceneBattle.MultiPlayRecvData buffer = (SceneBattle.MultiPlayRecvData) null;
      if (!GameUtility.Binary2Object<SceneBattle.MultiPlayRecvData>(out buffer, sendBinary))
        return;
      this.mRecvGoodJob.Add(buffer);
    }

    public void OnDoubleTap(Vector2 pos)
    {
    }

    private void OnGoodJobClickGrid(Grid grid) => this.AddGoodJob(grid.x, grid.y);

    private void OnGoodJobClickUnit(TacticsUnitController controller)
    {
      Unit unit = controller.Unit;
      int index = this.Battle.AllUnits.FindIndex((Predicate<Unit>) (u => u == unit));
      if (index < 0)
        return;
      this.AddGoodJob(0, 0, index);
    }

    private void UpdateGoodJob()
    {
      if (!this.mSetupGoodJob)
        return;
      if ((double) this.mGoodJobWait > 0.0)
        this.mGoodJobWait -= Time.deltaTime;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null) && this.mBattleUI_MultiPlay.StampWindowIsOpened)
      {
        if (this.mOnGridClick == null)
          this.mOnGridClick = new SceneBattle.GridClickEvent(this.OnGoodJobClickGrid);
        if (this.mOnUnitClick == null)
          this.mOnUnitClick = new SceneBattle.UnitClickEvent(this.OnGoodJobClickUnit);
      }
      else
      {
        if (this.mOnGridClick == new SceneBattle.GridClickEvent(this.OnGoodJobClickGrid))
          this.mOnGridClick = (SceneBattle.GridClickEvent) null;
        if (this.mOnUnitClick == new SceneBattle.UnitClickEvent(this.OnGoodJobClickUnit))
          this.mOnUnitClick = (SceneBattle.UnitClickEvent) null;
      }
      SceneBattle.MultiPlayInput multiPlayInput = new SceneBattle.MultiPlayInput();
      foreach (SceneBattle.MultiPlayRecvData multiPlayRecvData in this.mRecvGoodJob)
      {
        int index = multiPlayRecvData.c == null || multiPlayRecvData.c.Length <= 0 ? multiPlayInput.c : multiPlayRecvData.c[0];
        int x = multiPlayRecvData.gx == null || multiPlayRecvData.gx.Length <= 0 ? multiPlayInput.gx : multiPlayRecvData.gx[0];
        int y = multiPlayRecvData.gy == null || multiPlayRecvData.gy.Length <= 0 ? multiPlayInput.gy : multiPlayRecvData.gy[0];
        int uid = multiPlayRecvData.uid;
        if (0 <= uid && uid < this.Battle.AllUnits.Count)
        {
          x = this.Battle.AllUnits[uid].x;
          y = this.Battle.AllUnits[uid].y;
        }
        if (this.Battle.CurrentMap != null && this.mHeightMap != null && 0 <= x && x < this.Battle.CurrentMap.Width && 0 <= y && y < this.Battle.CurrentMap.Height)
        {
          Vector3 position = this.CalcGridCenter(this.Battle.CurrentMap[x, y]);
          BattleStamp stampWindow = !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null) ? this.mBattleUI_MultiPlay.StampWindow : (BattleStamp) null;
          Sprite sprite = UnityEngine.Object.op_Equality((UnityEngine.Object) stampWindow, (UnityEngine.Object) null) || stampWindow.Sprites == null || index < 0 || index >= stampWindow.Sprites.Length ? (Sprite) null : stampWindow.Sprites[index];
          GameObject prefab = UnityEngine.Object.op_Equality((UnityEngine.Object) stampWindow, (UnityEngine.Object) null) || stampWindow.Prefabs == null || index < 0 || index >= stampWindow.Prefabs.Length ? (GameObject) null : stampWindow.Prefabs[index];
          this.PopupGoodJob(position, prefab, sprite);
        }
      }
      this.mRecvGoodJob.Clear();
    }

    public bool SendFinishLoad()
    {
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      if (instance1.AudienceMode || instance1.IsVSCpuBattle)
        return true;
      MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
      MyPhoton.MyPlayer me = instance2.GetMyPlayer();
      SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerID == me.playerID));
      byte[] sendBinary = this.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.FINISH_LOAD, 0, (List<SceneBattle.MultiPlayInput>) null);
      bool flag = instance2.SendRoomMessageBinary(true, sendBinary);
      if (multiPlayer != null)
        multiPlayer.FinishLoad = true;
      return flag;
    }

    public void SendResumeSuccess()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || this.mResumeSend || (double) this.mRestResumeSuccessInterval > 0.0)
        return;
      this.mRestResumeSuccessInterval = this.RESUME_SUCCESS_INTERVAL;
      PunMonoSingleton<MyPhoton>.Instance.SendRoomMessageBinary(true, this.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.RESUME_SUCCESS, 0, (List<SceneBattle.MultiPlayInput>) null));
    }

    public void SendRequestResume()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || !this.mResumeMultiPlay || this.mResumeSend || (double) this.mRestResumeRequestInterval > 0.0)
        return;
      this.mRestResumeRequestInterval = this.RESUME_REQUEST_INTERVAL;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (this.Battle.IsMultiPlay && this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.Multi && !instance.GetCurrentRoom().battle)
      {
        instance.Disconnect();
      }
      else
      {
        byte[] sendBinary = this.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.REQUEST_RESUME, 0, (List<SceneBattle.MultiPlayInput>) null);
        instance.SendRoomMessageBinary(true, sendBinary);
        foreach (MyPhoton.MyPlayer roomPlayer in instance.GetRoomPlayerList())
        {
          MyPhoton.MyPlayer player = roomPlayer;
          SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerID == player.playerID));
          if (player.start)
          {
            this.mResumeAlreadyStartPlayer.Add(player.playerID);
            if (multiPlayer != null)
              multiPlayer.FinishLoad = true;
          }
        }
      }
    }

    public int SearchUnitIndex(Unit target)
    {
      if (target == null)
        return -1;
      for (int index = 0; index < this.Battle.AllUnits.Count; ++index)
      {
        if (this.Battle.AllUnits[index].Equals((object) target))
          return index;
      }
      return -1;
    }

    public void SendResumeInfo()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode)
        return;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (!this.Battle.IsResume || !instance.IsHost() || this.mResumeSend || this.mRecvResumeRequest.Count <= 0)
        return;
      byte[] sendResumeInfo = this.CreateSendResumeInfo();
      if (sendResumeInfo != null)
        this.mResumeSend = instance.SendRoomMessageBinary(true, sendResumeInfo, MyPhoton.SEND_TYPE.Resume);
      Debug.Log((object) "Send ResumeInfo!!!");
    }

    private byte[] CreateSendResumeInfo()
    {
      MultiPlayResumeParam data = new MultiPlayResumeParam();
      data.unit = new MultiPlayResumeUnitData[this.Battle.AllUnits.Count];
      string msg = string.Empty;
      for (int index1 = 0; index1 < this.Battle.AllUnits.Count; ++index1)
      {
        Unit allUnit = this.Battle.AllUnits[index1];
        if (allUnit != null)
        {
          data.unit[index1] = new MultiPlayResumeUnitData();
          BaseStatus currentStatus = allUnit.CurrentStatus;
          MultiPlayResumeUnitData playResumeUnitData = data.unit[index1];
          playResumeUnitData.name = allUnit.UnitName;
          playResumeUnitData.hp = (int) currentStatus.param.hp;
          playResumeUnitData.chp = allUnit.UnitChangedHp;
          playResumeUnitData.gem = allUnit.Gems;
          playResumeUnitData.dir = (int) allUnit.Direction;
          playResumeUnitData.x = allUnit.x;
          playResumeUnitData.y = allUnit.y;
          playResumeUnitData.target = this.SearchUnitIndex(allUnit.Target);
          playResumeUnitData.ragetarget = this.SearchUnitIndex(allUnit.RageTarget);
          playResumeUnitData.aiindex = (int) allUnit.AIActionIndex;
          playResumeUnitData.aiturn = (int) allUnit.AIActionTurnCount;
          playResumeUnitData.aipatrol = (int) allUnit.AIPatrolIndex;
          playResumeUnitData.flag = allUnit.UnitFlag;
          playResumeUnitData.casttarget = -1;
          if (allUnit.CastSkill != null)
          {
            playResumeUnitData.castskill = allUnit.CastSkill.SkillParam.iname;
            playResumeUnitData.casttime = (int) allUnit.CastTime;
            playResumeUnitData.castindex = (int) allUnit.CastIndex;
            if (allUnit.CastSkillGridMap != null)
            {
              playResumeUnitData.grid_w = allUnit.CastSkillGridMap.w;
              playResumeUnitData.grid_h = allUnit.CastSkillGridMap.h;
              if (allUnit.CastSkillGridMap.data != null)
                playResumeUnitData.castgrid = new int[allUnit.CastSkillGridMap.data.Length];
              for (int index2 = 0; index2 < allUnit.CastSkillGridMap.data.Length; ++index2)
                playResumeUnitData.castgrid[index2] = !allUnit.CastSkillGridMap.data[index2] ? 0 : 1;
            }
            playResumeUnitData.ctx = allUnit.GridTarget == null ? -1 : allUnit.GridTarget.x;
            playResumeUnitData.cty = allUnit.GridTarget == null ? -1 : allUnit.GridTarget.y;
            playResumeUnitData.casttarget = this.SearchUnitIndex(allUnit.UnitTarget);
          }
          playResumeUnitData.chargetime = (int) allUnit.ChargeTime;
          playResumeUnitData.isDead = !allUnit.IsGimmick ? (!allUnit.IsDead ? 0 : 1) : (!allUnit.IsDisableGimmick() ? 0 : 1);
          playResumeUnitData.deathcnt = allUnit.DeathCount;
          playResumeUnitData.autojewel = allUnit.AutoJewel;
          playResumeUnitData.waitturn = allUnit.WaitClock;
          playResumeUnitData.moveturn = allUnit.WaitMoveTurn;
          playResumeUnitData.actcnt = allUnit.ActionCount;
          playResumeUnitData.turncnt = allUnit.TurnCount;
          playResumeUnitData.entry = !allUnit.IsUnitFlag(EUnitFlag.Reinforcement) ? 0 : 1;
          playResumeUnitData.trgcnt = 0;
          if (allUnit.EventTrigger != null)
            playResumeUnitData.trgcnt = allUnit.EventTrigger.Count;
          playResumeUnitData.killcnt = allUnit.KillCount;
          playResumeUnitData.boi = allUnit.CreateBreakObjId;
          playResumeUnitData.boc = allUnit.CreateBreakObjClock;
          playResumeUnitData.own = allUnit.OwnerPlayerIndex;
          playResumeUnitData.ist = allUnit.InfinitySpawnTag;
          playResumeUnitData.isd = allUnit.InfinitySpawnDeck;
          playResumeUnitData.did = allUnit.DtuParam == null ? (string) null : allUnit.DtuParam.Iname;
          playResumeUnitData.dfu = this.SearchUnitIndex(allUnit.DtuFromUnit);
          playResumeUnitData.drt = allUnit.DtuRemainTurn;
          playResumeUnitData.okd = allUnit.OverKillDamage;
          if (allUnit.EntryTriggers != null)
          {
            playResumeUnitData.etr = new int[allUnit.EntryTriggers.Count];
            for (int index3 = 0; index3 < allUnit.EntryTriggers.Count; ++index3)
              playResumeUnitData.etr[index3] = !allUnit.EntryTriggers[index3].on ? 0 : 1;
          }
          if (allUnit.AbilityChangeLists.Count != 0)
          {
            playResumeUnitData.abilchgs = new MultiPlayResumeAbilChg[allUnit.AbilityChangeLists.Count];
            for (int index4 = 0; index4 < allUnit.AbilityChangeLists.Count; ++index4)
            {
              Unit.AbilityChange abilityChangeList = allUnit.AbilityChangeLists[index4];
              if (abilityChangeList != null && abilityChangeList.mDataLists.Count != 0)
              {
                playResumeUnitData.abilchgs[index4] = new MultiPlayResumeAbilChg();
                playResumeUnitData.abilchgs[index4].acd = new MultiPlayResumeAbilChg.Data[abilityChangeList.mDataLists.Count];
                for (int index5 = 0; index5 < abilityChangeList.mDataLists.Count; ++index5)
                {
                  Unit.AbilityChange.Data mDataList = abilityChangeList.mDataLists[index5];
                  playResumeUnitData.abilchgs[index4].acd[index5] = new MultiPlayResumeAbilChg.Data();
                  playResumeUnitData.abilchgs[index4].acd[index5].fid = mDataList.mFromAp.iname;
                  playResumeUnitData.abilchgs[index4].acd[index5].tid = mDataList.mToAp.iname;
                  playResumeUnitData.abilchgs[index4].acd[index5].tur = mDataList.mTurn;
                  playResumeUnitData.abilchgs[index4].acd[index5].irs = !mDataList.mIsReset ? 0 : 1;
                  playResumeUnitData.abilchgs[index4].acd[index5].exp = mDataList.mExp;
                  playResumeUnitData.abilchgs[index4].acd[index5].iif = !mDataList.mIsInfinite ? 0 : 1;
                }
              }
            }
          }
          if (allUnit.AddedAbilitys.Count != 0)
          {
            playResumeUnitData.addedabils = new MultiPlayResumeAddedAbil[allUnit.AddedAbilitys.Count];
            for (int index6 = 0; index6 < allUnit.AddedAbilitys.Count; ++index6)
            {
              AbilityData addedAbility = allUnit.AddedAbilitys[index6];
              playResumeUnitData.addedabils[index6] = new MultiPlayResumeAddedAbil();
              playResumeUnitData.addedabils[index6].aid = addedAbility.AbilityID;
              playResumeUnitData.addedabils[index6].exp = addedAbility.Exp;
              playResumeUnitData.addedabils[index6].nct = addedAbility.IsNoneCategory;
            }
          }
          Dictionary<SkillData, OInt> skillUseCount = allUnit.GetSkillUseCount();
          int count = skillUseCount.Keys.Count;
          if (count > 0)
          {
            playResumeUnitData.skillname = new string[count];
            playResumeUnitData.skillcnt = new int[count];
            for (int index7 = 0; index7 < count; ++index7)
            {
              playResumeUnitData.skillname[index7] = skillUseCount.Keys.ToArray<SkillData>()[index7].SkillParam.iname;
              playResumeUnitData.skillcnt[index7] = (int) skillUseCount.Values.ToArray<OInt>()[index7];
            }
          }
          List<BuffAttachment> buffAttachments = allUnit.BuffAttachments;
          if (buffAttachments.Count > 0)
          {
            List<MultiPlayResumeBuff> multiPlayResumeBuffList = new List<MultiPlayResumeBuff>();
            for (int index8 = 0; index8 < buffAttachments.Count; ++index8)
            {
              if (buffAttachments[index8].CheckTiming != EffectCheckTimings.Moment && buffAttachments[index8].CheckTiming != EffectCheckTimings.MomentStart)
              {
                MultiPlayResumeBuff multiPlayResumeBuff = new MultiPlayResumeBuff();
                multiPlayResumeBuff.unitindex = this.SearchUnitIndex(buffAttachments[index8].user);
                multiPlayResumeBuff.checkunit = this.SearchUnitIndex(buffAttachments[index8].CheckTarget);
                multiPlayResumeBuff.timing = (int) buffAttachments[index8].CheckTiming;
                multiPlayResumeBuff.condition = (int) buffAttachments[index8].UseCondition;
                multiPlayResumeBuff.turn = (int) buffAttachments[index8].turn;
                multiPlayResumeBuff.passive = (bool) buffAttachments[index8].IsPassive;
                multiPlayResumeBuff.type = (int) buffAttachments[index8].BuffType;
                multiPlayResumeBuff.vtp = !buffAttachments[index8].IsNegativeValueIsBuff ? 0 : 1;
                multiPlayResumeBuff.calc = (int) buffAttachments[index8].CalcType;
                multiPlayResumeBuff.skilltarget = (int) buffAttachments[index8].skilltarget;
                multiPlayResumeBuff.lid = buffAttachments[index8].LinkageID;
                multiPlayResumeBuff.ubc = (int) buffAttachments[index8].UpBuffCount;
                multiPlayResumeBuff.atl.Clear();
                if (buffAttachments[index8].AagTargetLists != null)
                {
                  for (int index9 = 0; index9 < buffAttachments[index8].AagTargetLists.Count; ++index9)
                  {
                    int num = this.SearchUnitIndex(buffAttachments[index8].AagTargetLists[index9]);
                    if (num >= 0)
                      multiPlayResumeBuff.atl.Add(num);
                  }
                }
                multiPlayResumeBuff.rsl.Clear();
                if (buffAttachments[index8].ResistStatusBuffList != null)
                {
                  for (int index10 = 0; index10 < buffAttachments[index8].ResistStatusBuffList.Count; ++index10)
                    multiPlayResumeBuff.rsl.Add(new MultiPlayResumeBuff.ResistStatus()
                    {
                      rst = (int) buffAttachments[index8].ResistStatusBuffList[index10].mType,
                      rsv = (int) buffAttachments[index8].ResistStatusBuffList[index10].mVal
                    });
                }
                multiPlayResumeBuff.iname = (string) null;
                if (buffAttachments[index8].skill != null)
                  multiPlayResumeBuff.iname = buffAttachments[index8].skill.SkillParam.iname;
                multiPlayResumeBuffList.Add(multiPlayResumeBuff);
              }
            }
            playResumeUnitData.buff = multiPlayResumeBuffList.ToArray();
          }
          List<CondAttachment> condAttachments = allUnit.CondAttachments;
          if (condAttachments.Count > 0)
          {
            playResumeUnitData.cond = new MultiPlayResumeBuff[condAttachments.Count];
            for (int index11 = 0; index11 < condAttachments.Count; ++index11)
            {
              playResumeUnitData.cond[index11] = new MultiPlayResumeBuff();
              playResumeUnitData.cond[index11].unitindex = this.SearchUnitIndex(condAttachments[index11].user);
              playResumeUnitData.cond[index11].checkunit = this.SearchUnitIndex(condAttachments[index11].CheckTarget);
              playResumeUnitData.cond[index11].timing = (int) condAttachments[index11].CheckTiming;
              playResumeUnitData.cond[index11].condition = (int) condAttachments[index11].UseCondition;
              playResumeUnitData.cond[index11].turn = (int) condAttachments[index11].turn;
              playResumeUnitData.cond[index11].passive = (bool) condAttachments[index11].IsPassive;
              playResumeUnitData.cond[index11].type = (int) condAttachments[index11].CondType;
              playResumeUnitData.cond[index11].calc = (int) condAttachments[index11].Condition;
              playResumeUnitData.cond[index11].curse = !condAttachments[index11].IsCurse ? 0 : 1;
              playResumeUnitData.cond[index11].skilltarget = (int) condAttachments[index11].skilltarget;
              playResumeUnitData.cond[index11].bc_id = condAttachments[index11].CondId;
              playResumeUnitData.cond[index11].lid = condAttachments[index11].LinkageID;
              playResumeUnitData.cond[index11].iname = (string) null;
              if (condAttachments[index11].skill != null)
                playResumeUnitData.cond[index11].iname = condAttachments[index11].skill.SkillParam.iname;
            }
          }
          if (allUnit.Shields != null && allUnit.Shields.Count != 0)
          {
            playResumeUnitData.shields = new MultiPlayResumeShield[allUnit.Shields.Count];
            for (int index12 = 0; index12 < allUnit.Shields.Count; ++index12)
            {
              Unit.UnitShield shield = allUnit.Shields[index12];
              playResumeUnitData.shields[index12] = new MultiPlayResumeShield();
              playResumeUnitData.shields[index12].inm = shield.skill_param.iname;
              playResumeUnitData.shields[index12].nhp = (int) shield.hp;
              playResumeUnitData.shields[index12].mhp = (int) shield.hpMax;
              playResumeUnitData.shields[index12].ntu = (int) shield.turn;
              playResumeUnitData.shields[index12].mtu = (int) shield.turnMax;
              playResumeUnitData.shields[index12].drt = (int) shield.damage_rate;
              playResumeUnitData.shields[index12].dvl = (int) shield.damage_value;
            }
          }
          if (allUnit.JudgeHpLists != null && allUnit.JudgeHpLists.Count != 0)
          {
            playResumeUnitData.hpis = new string[allUnit.JudgeHpLists.Count];
            for (int index13 = 0; index13 < allUnit.JudgeHpLists.Count; ++index13)
              playResumeUnitData.hpis[index13] = allUnit.JudgeHpLists[index13].SkillID;
          }
          if (allUnit.MhmDamageLists != null && allUnit.MhmDamageLists.Count != 0)
          {
            playResumeUnitData.mhm_dmgs = new MultiPlayResumeMhmDmg[allUnit.MhmDamageLists.Count];
            for (int index14 = 0; index14 < allUnit.MhmDamageLists.Count; ++index14)
            {
              Unit.UnitMhmDamage mhmDamageList = allUnit.MhmDamageLists[index14];
              playResumeUnitData.mhm_dmgs[index14] = new MultiPlayResumeMhmDmg();
              playResumeUnitData.mhm_dmgs[index14].typ = (int) mhmDamageList.mType;
              playResumeUnitData.mhm_dmgs[index14].dmg = (int) mhmDamageList.mDamage;
            }
          }
          if (allUnit.FtgtTargetList != null && allUnit.FtgtTargetList.Count != 0)
          {
            playResumeUnitData.tfl = new MultiPlayResumeFtgt[allUnit.FtgtTargetList.Count];
            for (int index15 = 0; index15 < allUnit.FtgtTargetList.Count; ++index15)
            {
              Unit.UnitForcedTargeting ftgtTarget = allUnit.FtgtTargetList[index15];
              playResumeUnitData.tfl[index15] = new MultiPlayResumeFtgt();
              playResumeUnitData.tfl[index15].uni = this.SearchUnitIndex(ftgtTarget.mUnit);
              playResumeUnitData.tfl[index15].tur = ftgtTarget.mTurn;
            }
          }
          if (allUnit.FtgtFromList != null && allUnit.FtgtFromList.Count != 0)
          {
            playResumeUnitData.ffl = new MultiPlayResumeFtgt[allUnit.FtgtFromList.Count];
            for (int index16 = 0; index16 < allUnit.FtgtFromList.Count; ++index16)
            {
              Unit.UnitForcedTargeting ftgtFrom = allUnit.FtgtFromList[index16];
              playResumeUnitData.ffl[index16] = new MultiPlayResumeFtgt();
              playResumeUnitData.ffl[index16].uni = this.SearchUnitIndex(ftgtFrom.mUnit);
              playResumeUnitData.ffl[index16].tur = ftgtFrom.mTurn;
            }
          }
          if (allUnit.Protects != null && allUnit.Protects.Count != 0)
          {
            playResumeUnitData.protects = new List<MultiPlayResumeProtect>();
            for (int index17 = 0; index17 < allUnit.Protects.Count; ++index17)
            {
              Unit.UnitProtect protect = allUnit.Protects[index17];
              playResumeUnitData.protects.Add(new MultiPlayResumeProtect()
              {
                target = this.SearchUnitIndex(protect.target),
                value = (int) protect.value,
                skillIname = protect.skillIname
              });
            }
          }
          if (allUnit.Guards != null && allUnit.Guards.Count != 0)
          {
            playResumeUnitData.guards = new List<MultiPlayResumeProtect>();
            for (int index18 = 0; index18 < allUnit.Guards.Count; ++index18)
            {
              Unit.UnitProtect guard = allUnit.Guards[index18];
              playResumeUnitData.guards.Add(new MultiPlayResumeProtect()
              {
                target = this.SearchUnitIndex(guard.target),
                value = (int) guard.value,
                skillIname = guard.skillIname
              });
            }
          }
          msg = msg + allUnit.UnitName + "- pos:" + (object) allUnit.x + "," + (object) allUnit.y;
        }
      }
      DebugUtility.LogWarning(msg);
      data.unitcastindex = (int) Unit.UNIT_CAST_INDEX;
      RandXorshift randXorshift1 = this.Battle.CloneRand();
      RandXorshift randXorshift2 = this.Battle.CloneRandDamage();
      uint[] seed1 = randXorshift1.GetSeed();
      uint[] seed2 = randXorshift2.GetSeed();
      data.rndseed = new uint[4];
      data.dmgrndseed = new uint[4];
      for (int index = 0; index < 4; ++index)
      {
        data.rndseed[index] = seed1[index];
        data.dmgrndseed[index] = seed2[index];
      }
      data.seed = this.Battle.Seed;
      data.damageseed = this.Battle.DamageSeed;
      data.unitstartcount = this.UnitStartCountTotal;
      data.treasurecount = this.TreasureCount;
      data.versusturn = this.Battle.VersusTurnCount;
      data.ctm = this.Battle.ClockTime;
      data.ctt = this.Battle.ClockTimeTotal;
      if (this.mRecvResumeRequest != null && this.mRecvResumeRequest.Count > 0)
        data.resumeID = this.mRecvResumeRequest[0].pidx;
      if (this.mRecvResumeRequest.Count > 1)
      {
        data.otherresume = new int[this.mRecvResumeRequest.Count - 1];
        for (int index = 1; index < this.mRecvResumeRequest.Count; ++index)
          data.otherresume[index - 1] = this.mRecvResumeRequest[index].pidx;
      }
      List<GimmickEvent> gimmickEventList = this.Battle.GimmickEventList;
      if (gimmickEventList.Count > 0)
      {
        data.gimmick = new MultiPlayGimmickEventParam[gimmickEventList.Count];
        for (int index = 0; index < gimmickEventList.Count; ++index)
        {
          data.gimmick[index] = new MultiPlayGimmickEventParam();
          data.gimmick[index].count = gimmickEventList[index].count;
          data.gimmick[index].completed = !gimmickEventList[index].IsCompleted ? 0 : 1;
        }
      }
      List<TrickData> effectAll = TrickData.GetEffectAll();
      if (effectAll.Count > 0)
      {
        data.trick = new MultiPlayTrickParam[effectAll.Count];
        for (int index = 0; index < effectAll.Count; ++index)
        {
          data.trick[index] = new MultiPlayTrickParam();
          data.trick[index].tid = effectAll[index].TrickParam.Iname;
          data.trick[index].val = (bool) effectAll[index].Valid;
          data.trick[index].cun = this.SearchUnitIndex(effectAll[index].CreateUnit);
          data.trick[index].rnk = (int) effectAll[index].Rank;
          data.trick[index].rcp = (int) effectAll[index].RankCap;
          data.trick[index].grx = (int) effectAll[index].GridX;
          data.trick[index].gry = (int) effectAll[index].GridY;
          data.trick[index].rac = (int) effectAll[index].RestActionCount;
          data.trick[index].ccl = (int) effectAll[index].CreateClock;
          data.trick[index].tag = effectAll[index].Tag;
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEventScript, (UnityEngine.Object) null) && this.mEventScript.mSequences != null && this.mEventScript.mSequences.Length != 0)
      {
        data.scr_ev_trg = new bool[this.mEventScript.mSequences.Length];
        for (int index = 0; index < this.mEventScript.mSequences.Length; ++index)
          data.scr_ev_trg[index] = this.mEventScript.mSequences[index].Triggered;
      }
      data.wti.wid = (string) null;
      WeatherData currentWeatherData = WeatherData.CurrentWeatherData;
      if (currentWeatherData != null)
      {
        data.wti.wid = currentWeatherData.WeatherParam.Iname;
        data.wti.mun = this.SearchUnitIndex(currentWeatherData.ModifyUnit);
        data.wti.rnk = (int) currentWeatherData.Rank;
        data.wti.rcp = (int) currentWeatherData.RankCap;
        data.wti.ccl = (int) currentWeatherData.ChangeClock;
      }
      return GameUtility.Object2Binary<MultiPlayResumeParam>(data);
    }

    [DebuggerHidden]
    private IEnumerator RecvResume(byte[] resumedata)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CRecvResume\u003Ec__Iterator6()
      {
        resumedata = resumedata,
        \u0024this = this
      };
    }

    private void RecvBuildBuffAndCond(
      ref List<MultiPlayResumeSkillData> buff,
      ref List<MultiPlayResumeSkillData> cond,
      MultiPlayResumeUnitData unit)
    {
      buff.Clear();
      if (unit.buff != null)
      {
        for (int index = 0; index < unit.buff.Length; ++index)
        {
          MultiPlayResumeBuff multiPlayResumeBuff = unit.buff[index];
          MultiPlayResumeSkillData playResumeSkillData = new MultiPlayResumeSkillData()
          {
            user = multiPlayResumeBuff.unitindex == -1 ? (Unit) null : this.Battle.AllUnits[multiPlayResumeBuff.unitindex],
            check = multiPlayResumeBuff.checkunit == -1 ? (Unit) null : this.Battle.AllUnits[multiPlayResumeBuff.checkunit]
          };
          playResumeSkillData.skill = playResumeSkillData.user == null ? (SkillData) null : playResumeSkillData.user.GetSkillData(multiPlayResumeBuff.iname);
          if (playResumeSkillData.skill == null && playResumeSkillData.user != null && playResumeSkillData.user.IsUnitFlag(EUnitFlag.IsDynamicTransform))
          {
            Unit unit1 = playResumeSkillData.user;
            while (unit1.IsUnitFlag(EUnitFlag.IsDynamicTransform))
            {
              unit1 = unit1.DtuFromUnit;
              if (unit1 != null)
              {
                playResumeSkillData.skill = unit1 == null ? (SkillData) null : unit1.GetSkillData(multiPlayResumeBuff.iname);
                if (playResumeSkillData.skill != null)
                  break;
              }
              else
                break;
            }
          }
          if (playResumeSkillData.skill == null)
            playResumeSkillData.skill = playResumeSkillData.user == null ? (SkillData) null : playResumeSkillData.user.SearchArtifactSkill(multiPlayResumeBuff.iname);
          buff.Add(playResumeSkillData);
        }
      }
      cond.Clear();
      if (unit.cond == null)
        return;
      for (int index = 0; index < unit.cond.Length; ++index)
      {
        MultiPlayResumeBuff multiPlayResumeBuff = unit.cond[index];
        MultiPlayResumeSkillData playResumeSkillData = new MultiPlayResumeSkillData()
        {
          user = multiPlayResumeBuff.unitindex == -1 ? (Unit) null : this.Battle.AllUnits[multiPlayResumeBuff.unitindex],
          check = multiPlayResumeBuff.checkunit == -1 ? (Unit) null : this.Battle.AllUnits[multiPlayResumeBuff.checkunit]
        };
        playResumeSkillData.skill = playResumeSkillData.user == null ? (SkillData) null : playResumeSkillData.user.GetSkillData(multiPlayResumeBuff.iname);
        if (playResumeSkillData.skill == null && playResumeSkillData.user != null && playResumeSkillData.user.IsUnitFlag(EUnitFlag.IsDynamicTransform))
        {
          Unit unit2 = playResumeSkillData.user;
          while (unit2.IsUnitFlag(EUnitFlag.IsDynamicTransform))
          {
            unit2 = unit2.DtuFromUnit;
            if (unit2 != null)
            {
              playResumeSkillData.skill = unit2 == null ? (SkillData) null : unit2.GetSkillData(multiPlayResumeBuff.iname);
              if (playResumeSkillData.skill != null)
                break;
            }
            else
              break;
          }
        }
        if (playResumeSkillData.skill == null)
          playResumeSkillData.skill = playResumeSkillData.user == null ? (SkillData) null : playResumeSkillData.user.SearchArtifactSkill(multiPlayResumeBuff.iname);
        cond.Add(playResumeSkillData);
      }
    }

    private void RecvUnitDead(TacticsUnitController controller, Unit target)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) controller, (UnityEngine.Object) null) || target == null)
        return;
      if (target.IsDead)
      {
        this.Battle.ResumeDead(target);
        ((Component) controller).gameObject.SetActive(false);
        controller.ShowHPGauge(false);
        controller.ShowVersusCursor(false);
        this.mTacticsUnits.Remove(controller);
      }
      else
      {
        if (controller.IsJumpCant())
          controller.SetCastJump();
        controller.UpdateShields();
        controller.ClearBadStatusLocks();
        controller.UpdateBadStatus();
      }
      if (!target.IsGimmick)
        return;
      if (target.IsBreakObj)
        controller.ReflectDispModel();
      if (!target.IsDisableGimmick())
        return;
      ((Component) controller).gameObject.SetActive(false);
    }

    [DebuggerHidden]
    private IEnumerator RecvReinforcementUnit()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CRecvReinforcementUnit\u003Ec__Iterator7()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator RestoreWeather(WeatherData wth_data)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CRestoreWeather\u003Ec__Iterator8()
      {
        wth_data = wth_data,
        \u0024this = this
      };
    }

    public void SendCheat(SceneBattle.CHEAT_TYPE type, int uid)
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode)
        return;
      PunMonoSingleton<MyPhoton>.Instance.SendRoomMessageBinary(true, this.CreateSendBinary((SceneBattle.EMultiPlayRecvDataHeader) (14 + type), uid, (List<SceneBattle.MultiPlayInput>) null));
    }

    public BattleCore.QuestResult CheckAudienceResult()
    {
      BattleCore.QuestResult questResult = BattleCore.QuestResult.Pending;
      if (this.Battle.IsVSForceWin)
      {
        foreach (SceneBattle.MultiPlayer multiPlayer in this.mMultiPlayer)
        {
          if (multiPlayer != null && multiPlayer.NotifyDisconnected)
          {
            questResult = multiPlayer.PlayerIndex != 1 ? BattleCore.QuestResult.Win : BattleCore.QuestResult.Lose;
            break;
          }
        }
      }
      else
        questResult = this.Battle.GetQuestResult();
      return questResult;
    }

    private void UpdateInterval()
    {
      this.mRestSyncInterval = Mathf.Max(this.mRestSyncInterval - Time.deltaTime, 0.0f);
      this.mRestResumeRequestInterval = Mathf.Max(this.mRestResumeRequestInterval - Time.deltaTime, 0.0f);
      this.mRestResumeSuccessInterval = Mathf.Max(this.mRestResumeSuccessInterval - Time.deltaTime, 0.0f);
    }

    public SceneBattle.MoveInput VirtualStickMoveInput => this.mMoveInput;

    public EventScript EventScript => this.mEventScript;

    public GameObject continueWindowRes => this.mContinueWindowRes;

    private UnitGauge GetGaugeTemplateFor(Unit unit)
    {
      if (unit.Side != EUnitSide.Enemy && !unit.IsBreakObj)
        return this.GaugeOverlayTemplate;
      return unit.IsRaidBoss ? (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EnemyBossGaugeOverlayTemplate, (UnityEngine.Object) null) ? this.EnemyBossGaugeOverlayTemplate : this.GaugeOverlayTemplate) : (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EnemyGaugeOverlayTemplate, (UnityEngine.Object) null) ? this.EnemyGaugeOverlayTemplate : this.GaugeOverlayTemplate);
    }

    public void OnGimmickUpdate()
    {
      this.mUnitList.Clear();
      this.mGimmickList.Clear();
      for (int index = 0; index < this.mBattle.Units.Count; ++index)
      {
        TacticsUnitController unitController = this.FindUnitController(this.mBattle.Units[index]);
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          if (this.mBattle.Units[index].IsGimmick && !this.mBattle.Units[index].IsDisableGimmick() && !this.mBattle.Units[index].IsBreakObj)
            this.mGimmickList.Add(unitController);
          else if (!this.mBattle.Units[index].IsDead && !this.mBattle.Units[index].IsJump)
            this.mUnitList.Add(unitController);
        }
      }
      for (int index1 = 0; index1 < this.mUnitList.Count; ++index1)
      {
        TacticsUnitController mUnit = this.mUnitList[index1];
        IntVector2 intVector2_1 = this.CalcCoord(mUnit.CenterPosition);
        bool flag = false;
        for (int index2 = 0; index2 < this.mGimmickList.Count; ++index2)
        {
          TacticsUnitController mGimmick = this.mGimmickList[index2];
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) mUnit, (UnityEngine.Object) mGimmick))
          {
            IntVector2 intVector2_2 = this.CalcCoord(mGimmick.CenterPosition);
            if (intVector2_1 == intVector2_2)
            {
              if (this.mUnitList[index1].Unit.CastSkill == null)
              {
                flag = true;
                mUnit.SetGimmickIcon(this.mGimmickList[index2].Unit);
              }
              mGimmick.ScaleHide();
              this.mGimmickList.Remove(mGimmick);
              break;
            }
            mGimmick.ScaleShow();
          }
        }
        if (!flag)
        {
          if (this.mUnitList[index1].Unit == this.Battle.CurrentUnit)
            mUnit.HideGimmickIcon(this.mUnitList[index1].Unit.UnitType);
          else
            mUnit.DeleteGimmickIconAll();
        }
      }
    }

    private void DeleteOnGimmickIcon()
    {
      for (int index = 0; index < this.mBattle.Units.Count; ++index)
      {
        if (!this.mBattle.Units[index].IsGimmick)
        {
          TacticsUnitController unitController = this.FindUnitController(this.mBattle.Units[index]);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
            unitController.DeleteGimmickIconAll();
        }
      }
    }

    private void SetUnitUiHeight(Unit FocusUnit)
    {
      MapHeight gameObject = GameObjectID.FindGameObject<MapHeight>(this.mBattleUI.MapHeightID);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      gameObject.FocusUnit = FocusUnit;
    }

    private void SetUiHeight(int Height)
    {
      MapHeight gameObject = GameObjectID.FindGameObject<MapHeight>(this.mBattleUI.MapHeightID);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      gameObject.FocusUnit = (Unit) null;
      gameObject.Height = Height;
    }

    private void ArenaActionCountEnable(bool enable)
    {
      GameObject gameObject = GameObjectID.FindGameObject(this.mBattleUI.ArenaActionCountID);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
        return;
      gameObject.SetActive(enable);
    }

    private void ArenaActionCountSet(uint count)
    {
      ArenaActionCount gameObject = GameObjectID.FindGameObject<ArenaActionCount>(this.mBattleUI.ArenaActionCountID);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject) || (int) gameObject.ActionCount == (int) count)
        return;
      gameObject.ActionCount = count;
      gameObject.PlayEffect();
    }

    private void RemainingActionCountEnable(bool pc_enable, bool ec_enable)
    {
      GameObject gameObject1 = GameObjectID.FindGameObject(this.mBattleUI.PlayerActionCountID);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject1))
        gameObject1.SetActive(pc_enable);
      GameObject gameObject2 = GameObjectID.FindGameObject(this.mBattleUI.EnemyActionCountID);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject2))
        return;
      gameObject2.SetActive(ec_enable);
    }

    private void RemainingActionCountSet(uint pc_count, uint ec_count)
    {
      ArenaActionCount gameObject1 = GameObjectID.FindGameObject<ArenaActionCount>(this.mBattleUI.PlayerActionCountID);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null) && ((Behaviour) gameObject1).isActiveAndEnabled && (int) gameObject1.ActionCount != (int) pc_count)
      {
        gameObject1.ActionCount = pc_count;
        gameObject1.PlayEffect();
      }
      ArenaActionCount gameObject2 = GameObjectID.FindGameObject<ArenaActionCount>(this.mBattleUI.EnemyActionCountID);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null) || !((Behaviour) gameObject2).isActiveAndEnabled || (int) gameObject2.ActionCount == (int) ec_count)
        return;
      gameObject2.ActionCount = ec_count;
      gameObject2.PlayEffect();
    }

    private void ReflectWeatherInfo(WeatherData wd = null)
    {
      if (wd == null)
        wd = WeatherData.CurrentWeatherData;
      WeatherInfo gameObject = GameObjectID.FindGameObject<WeatherInfo>(this.mBattleUI.WeatherInfoID);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
        return;
      gameObject.Refresh(wd);
    }

    private GameObject GetWeatherEffectAttach() => this.mGoWeatherAttach;

    private void EnableWeatherEffect(bool is_enable)
    {
      GameObject weatherEffectAttach = this.GetWeatherEffectAttach();
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) weatherEffectAttach))
        return;
      weatherEffectAttach.SetActive(is_enable);
    }

    private void RankingQuestActionCountEnable(bool enable)
    {
      GameObject gameObject = GameObjectID.FindGameObject(this.mBattleUI.RankingQuestActionCountID);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
        return;
      gameObject.SetActive(enable);
    }

    private void RankingQuestActionCountSet(uint count)
    {
      RankingQuestActionCount gameObject = GameObjectID.FindGameObject<RankingQuestActionCount>(this.mBattleUI.RankingQuestActionCountID);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject) || (int) gameObject.ActionCount == (int) count)
        return;
      gameObject.ActionCount = count;
      gameObject.PlayEffect();
    }

    private void StepToNear(Unit unit)
    {
      TacticsUnitController unitController = this.FindUnitController(unit);
      IntVector2 intVector2 = this.CalcCoord(unitController.CenterPosition);
      Grid current = this.mBattle.CurrentMap[intVector2.x, intVector2.y];
      unitController.StepTo(this.CalcGridCenter(current));
    }

    private void GotoState_WaitSignal<T>() where T : State<SceneBattle>, new()
    {
      this.GotoState<SceneBattle.State_WaitSignal<T>>();
    }

    private void ReflectUnitChgButton(Unit unit, bool is_update = false)
    {
      bool is_active;
      if (!is_update)
      {
        this.mIsUnitChgActive = false;
        if (unit.Side == EUnitSide.Player && !unit.IsUnitFlag(EUnitFlag.DisableUnitChange) && !unit.IsUnitFlag(EUnitFlag.Moved | EUnitFlag.Action) && this.mBattle.Player != null)
        {
          foreach (Unit unit1 in this.mBattle.Player)
          {
            if (!this.mBattle.StartingMembers.Contains(unit1))
            {
              this.mIsUnitChgActive = true;
              break;
            }
          }
        }
        is_active = this.mIsUnitChgActive;
      }
      else
      {
        if (!this.mBattle.IsUnitChange || !this.mIsUnitChgActive || !this.mBattleUI.CommandWindow.IsEnableUnitChgButton)
          return;
        is_active = true;
        TacticsUnitController unitController = this.FindUnitController(this.mBattle.CurrentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          IntVector2 intVector2 = this.CalcCoord(unitController.CenterPosition);
          if (intVector2.x != unit.x || intVector2.y != unit.y)
            is_active = false;
        }
      }
      this.mBattleUI.CommandWindow.EnableUnitChgButton(this.mBattle.IsUnitChange, is_active);
    }

    private void RefreshMapCommands()
    {
      Unit currentUnit = this.mBattle.CurrentUnit;
      UnitCommands.ButtonTypes buttonTypes1 = (UnitCommands.ButtonTypes) 0;
      if (currentUnit.IsEnableMoveCondition())
        buttonTypes1 |= UnitCommands.ButtonTypes.Move;
      if (!currentUnit.IsUnitFlag(EUnitFlag.Action))
      {
        buttonTypes1 |= UnitCommands.ButtonTypes.Action;
        if (currentUnit.IsEnableItemCondition() && !this.mBattle.CheckDisableItems(currentUnit))
          buttonTypes1 |= UnitCommands.ButtonTypes.Item;
        if (currentUnit.IsEnableAttackCondition())
        {
          buttonTypes1 |= UnitCommands.ButtonTypes.Attack;
          if (this.mBattle.HelperUnits.Count > 0)
            buttonTypes1 |= UnitCommands.ButtonTypes.IsRenkei;
        }
        if (currentUnit.IsEnableSkillCondition() && !this.mBattle.CheckDisableAbilities(currentUnit))
          buttonTypes1 |= UnitCommands.ButtonTypes.Skill;
      }
      UnitCommands.ButtonTypes buttonTypes2 = buttonTypes1 | UnitCommands.ButtonTypes.Misc | UnitCommands.ButtonTypes.Map;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.CommandWindow, (UnityEngine.Object) null))
        return;
      this.mBattleUI.CommandWindow.CancelButton.SetActive(true);
      this.mBattleUI.CommandWindow.OKButton.SetActive(true);
      this.mBattleUI.CommandWindow.VisibleButtons = buttonTypes2;
    }

    private void RefreshOnlyMapCommand()
    {
      UnitCommands.ButtonTypes buttonTypes = (UnitCommands.ButtonTypes) (0 | 256);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.CommandWindow, (UnityEngine.Object) null))
        return;
      this.mBattleUI.CommandWindow.CancelButton.SetActive(false);
      this.mBattleUI.CommandWindow.OKButton.SetActive(false);
      this.mBattleUI.CommandWindow.VisibleButtons = buttonTypes;
    }

    private void GotoMapViewMode()
    {
      this.mBattleUI.OnMapViewStart();
      this.GotoSelectTarget((SkillData) null, new SceneBattle.SelectTargetCallback(this.GotoMapCommand), (SceneBattle.SelectTargetPositionWithSkill) null);
    }

    private void GotoItemSelect()
    {
      this.mBattleUI.OnItemSelectStart();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.ItemWindow, (UnityEngine.Object) null))
        this.mBattleUI.ItemWindow.Refresh();
      this.GotoState_WaitSignal<SceneBattle.State_SelectItemV2>();
    }

    private void OnSelectItemTarget(int x, int y, ItemData item)
    {
      if (!this.ApplyUnitMovement())
        return;
      this.HideAllHPGauges();
      this.HideAllUnitOwnerIndex();
      if (this.Battle.IsMultiPlay)
      {
        Debug.LogError((object) "Item! Cheat!");
        this.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
      }
      else
      {
        this.mBattle.UseItem(this.mBattle.CurrentUnit, x, y, item);
        this.mBattle.SetManualPlayFlag();
        this.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
      }
    }

    private void GotoUnitChgSelect(bool is_back = false)
    {
      this.mBattleUI.OnUnitChgSelectStart();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.UnitChgWindow, (UnityEngine.Object) null))
        this.mBattleUI.UnitChgWindow.Refresh();
      if (!is_back)
      {
        this.HideGrid();
        TacticsUnitController unitController = this.FindUnitController(this.Battle.CurrentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          IntVector2 intVector2 = this.CalcCoord(unitController.CenterPosition);
          GridMap<Color32> grid1 = new GridMap<Color32>(this.Battle.CurrentMap.Width, this.Battle.CurrentMap.Height);
          grid1.set(intVector2.x, intVector2.y, Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea));
          GridMap<Color32> grid2 = new GridMap<Color32>(this.Battle.CurrentMap.Width, this.Battle.CurrentMap.Height);
          grid2.set(intVector2.x, intVector2.y, Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea2));
          this.mTacticsSceneRoot.ShowGridLayer(1, grid1, true);
          this.mTacticsSceneRoot.ShowGridLayer(2, grid2, false);
        }
      }
      this.GotoState_WaitSignal<SceneBattle.State_SelectUnitChgV2>();
    }

    private void GotoSelectTarget(
      SkillData skill,
      SceneBattle.SelectTargetCallback cancel,
      SceneBattle.SelectTargetPositionWithSkill accept,
      Unit defaultTarget = null,
      bool allowTargetChange = true)
    {
      this.mTargetSelectorParam.Skill = skill;
      this.mTargetSelectorParam.Item = (ItemData) null;
      this.mTargetSelectorParam.OnAccept = (object) accept;
      this.mTargetSelectorParam.OnCancel = cancel;
      this.mTargetSelectorParam.DefaultTarget = defaultTarget;
      this.mTargetSelectorParam.AllowTargetChange = allowTargetChange;
      this.mTargetSelectorParam.IsThrowTargetSelect = false;
      this.mTargetSelectorParam.DefaultThrowTarget = (Unit) null;
      this.mTargetSelectorParam.ThrowTarget = (Unit) null;
      if (skill != null)
      {
        if (skill.EffectType == SkillEffectTypes.Throw)
        {
          this.mTargetSelectorParam.IsThrowTargetSelect = true;
          this.GotoState_WaitSignal<SceneBattle.State_PreThrowTargetSelect>();
        }
        else
          this.GotoState_WaitSignal<SceneBattle.State_PreSelectTargetV2>();
      }
      else
        this.GotoState_WaitSignal<SceneBattle.State_PreMapviewV2>();
    }

    private void GotoSelectTarget(
      ItemData item,
      SceneBattle.SelectTargetCallback cancel,
      SceneBattle.SelectTargetPositionWithItem accept,
      Unit defaultTarget = null,
      bool allowTargetChange = true)
    {
      this.mTargetSelectorParam.Item = item;
      this.mTargetSelectorParam.Skill = item == null ? (SkillData) null : item.Skill;
      this.mTargetSelectorParam.OnAccept = (object) accept;
      this.mTargetSelectorParam.OnCancel = cancel;
      this.mTargetSelectorParam.DefaultTarget = defaultTarget;
      this.mTargetSelectorParam.AllowTargetChange = allowTargetChange;
      this.mTargetSelectorParam.IsThrowTargetSelect = false;
      this.mTargetSelectorParam.DefaultThrowTarget = (Unit) null;
      this.mTargetSelectorParam.ThrowTarget = (Unit) null;
      this.GotoState_WaitSignal<SceneBattle.State_PreSelectTargetV2>();
    }

    private EUnitDirection GetSkillDirectionByTargetArea(
      Unit unit,
      int curX,
      int curY,
      GridMap<bool> targetArea)
    {
      int x1 = 0;
      int y1 = 0;
      for (int y2 = 0; y2 < targetArea.h; ++y2)
      {
        for (int x2 = 0; x2 < targetArea.w; ++x2)
        {
          if (targetArea.get(x2, y2))
          {
            x1 += x2 - curX;
            y1 += y2 - curY;
          }
        }
      }
      return BattleCore.UnitDirectionFromVector(unit, x1, y1);
    }

    public void SelectUnitDir(EUnitDirection dir)
    {
      if (!this.mState.IsInState<SceneBattle.State_InputDirection>())
        return;
      ((SceneBattle.State_InputDirection) this.mState.State).SelectDirection(dir);
    }

    private void GotoSkillSelect()
    {
      UnitAbilitySkillList skillWindow = this.mBattleUI.SkillWindow;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) skillWindow, (UnityEngine.Object) null))
      {
        DataSource.Bind<AbilityData>(((Component) skillWindow).gameObject, this.UIParam_CurrentAbility);
        skillWindow.Refresh(this.mBattle.CurrentUnit);
      }
      this.mBattleUI.OnSkillSelectStart();
      this.mIsBackSelectSkill = true;
      this.GotoState_WaitSignal<SceneBattle.State_SelectSkillV2>();
      this.mIsBackSelectSkill = false;
    }

    private void OnSelectSkillTarget(int x, int y, SkillData skill, bool bUnitLockTarget)
    {
      if (!this.ApplyUnitMovement())
        return;
      this.HideAllHPGauges();
      this.HideAllUnitOwnerIndex();
      if (skill.EffectType == SkillEffectTypes.Throw)
        this.mSelectedTarget = this.mTargetSelectorParam.ThrowTarget;
      if (this.Battle.IsMultiPlay)
        this.SendInputEntryBattle(EBattleCommand.Skill, this.Battle.CurrentUnit, this.mSelectedTarget, skill, (ItemData) null, x, y, bUnitLockTarget);
      if (skill.EffectType == SkillEffectTypes.Throw)
        this.mBattle.UseSkill(this.mBattle.CurrentUnit, x, y, skill, bUnitLockTarget, this.mSelectedTarget.x, this.mSelectedTarget.y);
      else
        this.mBattle.UseSkill(this.mBattle.CurrentUnit, x, y, skill, bUnitLockTarget);
      this.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
    }

    private TutorialButtonImage[] TutorialButtonImages
    {
      get
      {
        if (this.mTutorialButtonImages == null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI, (UnityEngine.Object) null))
          this.mTutorialButtonImages = ((Component) this.mBattleUI).GetComponentsInChildren<TutorialButtonImage>(true);
        return this.mTutorialButtonImages;
      }
    }

    public bool EnableControlBattleUI(SceneBattle.eMaskBattleUI emask, bool is_enable)
    {
      return this.EnableControlBattleUI((uint) emask, is_enable);
    }

    public bool EnableControlBattleUI(uint mask, bool is_enable)
    {
      if (this.mCurrentQuest == null || this.mCurrentQuest.type != QuestTypes.Tutorial || mask == 0U)
        return false;
      uint controlDisableMask = this.mControlDisableMask;
      if (is_enable)
        this.mControlDisableMask &= ~mask;
      else
        this.mControlDisableMask |= mask;
      List<TutorialButtonImage> tutorialButtonImageList = new List<TutorialButtonImage>((IEnumerable<TutorialButtonImage>) this.TutorialButtonImages);
      for (int index1 = 0; index1 < SceneBattle.MAX_MASK_BATTLE_UI; ++index1)
      {
        SceneBattle.eMaskBattleUI emask = (SceneBattle.eMaskBattleUI) (1 << index1);
        bool flag1 = ((SceneBattle.eMaskBattleUI) this.mControlDisableMask & emask) != (SceneBattle.eMaskBattleUI) 0;
        bool flag2 = ((SceneBattle.eMaskBattleUI) controlDisableMask & emask) != (SceneBattle.eMaskBattleUI) 0;
        if (flag1 != flag2)
        {
          List<TutorialButtonImage> all = tutorialButtonImageList.FindAll((Predicate<TutorialButtonImage>) (tbi => tbi.MaskType == emask));
          for (int index2 = 0; index2 < all.Count; ++index2)
          {
            TutorialButtonImage tutorialButtonImage = all[index2];
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) tutorialButtonImage))
              ((Component) tutorialButtonImage).gameObject.SetActive(flag1);
          }
        }
      }
      return (int) controlDisableMask != (int) this.mControlDisableMask;
    }

    public bool IsControlBattleUI(SceneBattle.eMaskBattleUI emask)
    {
      return ((SceneBattle.eMaskBattleUI) this.mControlDisableMask & emask) == (SceneBattle.eMaskBattleUI) 0;
    }

    public GameObject KnockBackEffect => this.mKnockBackEffect;

    public GameObject TrickMarker => this.mTrickMarker;

    public Dictionary<string, GameObject> TrickMarkerDics => this.mTrickMarkerDics;

    public GameObject JumpFallEffect => this.mJumpFallEffect;

    private Canvas OverlayCanvas
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTouchController, (UnityEngine.Object) null) ? ((Component) this.mTouchController).GetComponent<Canvas>() : (Canvas) null;
      }
    }

    private void ShowAllHPGauges()
    {
      this.ShowPlayerHPGauges();
      this.ShowEnemyHPGauges();
      this.ShowBreakObjHPGauges();
    }

    private bool IsHPGaugeChanging
    {
      get
      {
        for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        {
          if (this.mTacticsUnits[index].IsHPGaugeChanging)
            return true;
        }
        return false;
      }
    }

    private void HideAllHPGauges()
    {
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        this.mTacticsUnits[index].ShowHPGauge(false);
    }

    private void HideAllUnitOwnerIndex()
    {
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        this.mTacticsUnits[index].ShowOwnerIndexUI(false);
    }

    private void ShowPlayerHPGauges()
    {
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
      {
        if (this.mTacticsUnits[index].Unit.Side == EUnitSide.Player && !this.mTacticsUnits[index].Unit.IsDead)
          this.mTacticsUnits[index].ShowHPGauge(true);
      }
    }

    private void ShowEnemyHPGauges()
    {
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
      {
        if (this.mTacticsUnits[index].Unit.Side == EUnitSide.Enemy && !this.mTacticsUnits[index].Unit.IsDead)
          this.mTacticsUnits[index].ShowHPGauge(true);
      }
    }

    private void ShowBreakObjHPGauges()
    {
      foreach (TacticsUnitController mTacticsUnit in this.mTacticsUnits)
      {
        if (mTacticsUnit.Unit.IsBreakObj)
          mTacticsUnit.ShowHPGauge(true);
      }
    }

    private void RefreshUnitStatus(Unit unit)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) UnitQueue.Instance, (UnityEngine.Object) null))
        return;
      UnitQueue.Instance.Refresh(unit);
    }

    private float CalcHeight(float x, float y)
    {
      float num1 = 100f;
      float num2 = Mathf.Floor(x - 0.5f) + 0.5f;
      float num3 = Mathf.Floor(y - 0.5f) + 0.5f;
      float num4 = Mathf.Ceil(x - 0.5f) + 0.5f;
      float num5 = Mathf.Ceil(y - 0.5f) + 0.5f;
      float num6 = 0.0f;
      float num7 = 0.0f;
      float num8 = 0.0f;
      float num9 = 0.0f;
      RaycastHit raycastHit;
      if (Physics.Raycast(new Vector3(num2, num1, num3), Vector3.op_UnaryNegation(Vector3.up), ref raycastHit))
        num6 = ((RaycastHit) ref raycastHit).point.y;
      if (Physics.Raycast(new Vector3(num4, num1, num3), Vector3.op_UnaryNegation(Vector3.up), ref raycastHit))
        num7 = ((RaycastHit) ref raycastHit).point.y;
      if (Physics.Raycast(new Vector3(num2, num1, num5), Vector3.op_UnaryNegation(Vector3.up), ref raycastHit))
        num8 = ((RaycastHit) ref raycastHit).point.y;
      if (Physics.Raycast(new Vector3(num4, num1, num5), Vector3.op_UnaryNegation(Vector3.up), ref raycastHit))
        num9 = ((RaycastHit) ref raycastHit).point.y;
      float num10 = x - num2;
      float num11 = y - num3;
      return Mathf.Lerp(Mathf.Lerp(num6, num7, num10), Mathf.Lerp(num8, num9, num10), num11);
    }

    private IntVector2 CalcClickedGrid(Vector2 screenPosition)
    {
      Ray ray = Camera.main.ScreenPointToRay(Vector2.op_Implicit(screenPosition));
      IntVector2 intVector2 = new IntVector2(-1, -1);
      RaycastHit raycastHit;
      if (Physics.Raycast(ray, ref raycastHit))
      {
        float num = float.MaxValue;
        for (int y = 0; y < this.mHeightMap.h; ++y)
        {
          for (int x = 0; x < this.mHeightMap.w; ++x)
          {
            Vector3 vector3 = Vector3.op_Subtraction(((RaycastHit) ref raycastHit).point, new Vector3((float) x + 0.5f, this.mHeightMap.get(x, y), (float) y + 0.5f));
            float magnitude = ((Vector3) ref vector3).magnitude;
            if ((double) magnitude < (double) num)
            {
              intVector2.x = x;
              intVector2.y = y;
              num = magnitude;
            }
          }
        }
      }
      return intVector2;
    }

    private void OnClickBackground(Vector2 screenPosition)
    {
      if (this.mOnGridClick != null)
      {
        IntVector2 intVector2 = this.CalcClickedGrid(screenPosition);
        Grid current = this.mBattle.CurrentMap[intVector2.x, intVector2.y];
        if (intVector2.x != -1 && intVector2.y != -1 && current != null)
          this.mOnGridClick(current);
      }
      if (this.mOnUnitClick != null)
      {
        float num1 = float.MaxValue;
        TacticsUnitController controller = (TacticsUnitController) null;
        Vector3 vector3 = Vector3.op_Multiply(Vector3.up, 0.5f);
        Camera main = Camera.main;
        float num2 = (float) (Screen.height / 5);
        for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        {
          Vector2 vector2 = Vector2.op_Subtraction(Vector2.op_Implicit(main.WorldToScreenPoint(Vector3.op_Addition(((Component) this.mTacticsUnits[index]).transform.position, vector3))), screenPosition);
          float magnitude = ((Vector2) ref vector2).magnitude;
          if (!this.mTacticsUnits[index].Unit.IsNormalSize)
          {
            float num3 = (float) (((double) (this.mTacticsUnits[index].Unit.SizeX + this.mTacticsUnits[index].Unit.SizeY) / 2.0 + 1.0) / 2.0);
            if ((double) magnitude <= (double) num2 * (double) num3)
              magnitude /= num3;
          }
          if ((double) magnitude < (double) num1 && (double) magnitude <= (double) num2)
          {
            controller = this.mTacticsUnits[index];
            num1 = magnitude;
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) controller, (UnityEngine.Object) null))
          this.mOnUnitClick(controller);
      }
      if (this.mOnScreenClick == null)
        return;
      this.mOnScreenClick(screenPosition);
    }

    private T InstantiateSafe<T>(UnityEngine.Object obj) where T : UnityEngine.Object
    {
      return (T) UnityEngine.Object.Instantiate(obj);
    }

    private void UpdateLoadProgress()
    {
      ProgressWindow.SetLoadProgress((this.mLoadProgress_UI + this.mLoadProgress_Scene + this.mLoadProgress_Animation) * 0.5f);
    }

    [DebuggerHidden]
    private IEnumerator LoadUIAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CLoadUIAsync\u003Ec__Iterator9()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator LoadUIGvGAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CLoadUIGvGAsync\u003Ec__IteratorA()
      {
        \u0024this = this
      };
    }

    private void InitTouchArea()
    {
      GameObject gameObject = new GameObject("TouchArea", new System.Type[6]
      {
        typeof (Canvas),
        typeof (GraphicRaycaster),
        typeof (CanvasStack),
        typeof (NullGraphic),
        typeof (TouchController),
        typeof (SRPG_CanvasScaler)
      });
      this.mTouchController = gameObject.GetComponent<TouchController>();
      this.mTouchController.OnClick = new TouchController.ClickEvent(this.OnClickBackground);
      this.mTouchController.OnDragDelegate += new TouchController.DragEvent(this.OnDrag);
      this.mTouchController.OnDragEndDelegate += new TouchController.DragEvent(this.OnDragEnd);
      gameObject.GetComponent<Canvas>().renderMode = (RenderMode) 0;
      gameObject.GetComponent<CanvasStack>().Priority = 0;
      this.mGoWeatherAttach = new GameObject("WeatherAttach");
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mGoWeatherAttach))
        return;
      this.mGoWeatherAttach.transform.SetParent(gameObject.transform, false);
    }

    private void DestroyUI(bool is_part = false)
    {
      BadStatusEffects.UnloadEffects();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillTargetWindow, (UnityEngine.Object) null))
      {
        GameUtility.DestroyGameObject((Component) this.mSkillTargetWindow);
        this.mSkillTargetWindow = (SkillTargetWindow) null;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTouchController, (UnityEngine.Object) null))
      {
        this.mTouchController.OnDragDelegate -= new TouchController.DragEvent(this.OnDrag);
        this.mTouchController.OnDragEndDelegate -= new TouchController.DragEvent(this.OnDragEnd);
      }
      if (!is_part && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI, (UnityEngine.Object) null))
      {
        GameUtility.DestroyGameObject((Component) this.mBattleUI);
        this.mBattleUI = (FlowNode_BattleUI) null;
      }
      this.mBattleSceneRoot = (BattleSceneSettings) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDefaultBattleScene, (UnityEngine.Object) null))
      {
        GameUtility.DestroyGameObject((Component) this.mDefaultBattleScene);
        this.mDefaultBattleScene = (BattleSceneSettings) null;
      }
      for (int index = 0; index < this.mBattleScenes.Count; ++index)
        GameUtility.DestroyGameObject((Component) this.mBattleScenes[index]);
      this.mBattleScenes.Clear();
      if (this.mUnitMarkers == null)
        return;
      for (int index = 0; index < this.mUnitMarkers.Length; ++index)
        GameUtility.DestroyGameObjects(this.mUnitMarkers[index]);
      this.mUnitMarkers = (List<GameObject>[]) null;
    }

    private void HideUnitMarkers(bool deactivate = false)
    {
      for (int markerType = this.mUnitMarkers.Length - 1; markerType >= 0; --markerType)
        this.HideUnitMarkers((SceneBattle.UnitMarkerTypes) markerType);
      if (!deactivate)
        return;
      this.DeactivateUnusedUnitMarkers();
    }

    private void HideUnitMarkers(SceneBattle.UnitMarkerTypes markerType)
    {
      int index1 = (int) markerType;
      for (int index2 = this.mUnitMarkers[index1].Count - 1; index2 >= 0; --index2)
      {
        this.mUnitMarkers[index1][index2].transform.SetParent((Transform) null, false);
        GameUtility.SetLayer(this.mUnitMarkers[index1][index2], GameUtility.LayerHidden, true);
      }
    }

    private void HideUnitMarkers(Unit unit)
    {
      TacticsUnitController unitController = this.FindUnitController(unit);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        return;
      Transform transform = ((Component) unitController).transform;
      for (int index1 = this.mUnitMarkers.Length - 1; index1 >= 0; --index1)
      {
        for (int index2 = this.mUnitMarkers[index1].Count - 1; index2 >= 0; --index2)
        {
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitMarkers[index1][index2].transform.parent, (UnityEngine.Object) transform))
          {
            GameUtility.SetLayer(this.mUnitMarkers[index1][index2], GameUtility.LayerHidden, true);
            this.mUnitMarkers[index1][index2].transform.SetParent((Transform) null, false);
          }
        }
      }
    }

    private void ShowUnitMarker(List<Unit> units, SceneBattle.UnitMarkerTypes markerType)
    {
      for (int index = units.Count - 1; index >= 0; --index)
        this.ShowUnitMarker(units[index], markerType);
    }

    private void ShowUnitMarker(Unit unit, SceneBattle.UnitMarkerTypes markerType)
    {
      TacticsUnitController unitController = this.FindUnitController(unit);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        return;
      Transform transform = ((Component) unitController).transform;
      List<GameObject> mUnitMarker = this.mUnitMarkers[(int) markerType];
      for (int index = mUnitMarker.Count - 1; index >= 0; --index)
      {
        if (mUnitMarker[index].layer != GameUtility.LayerHidden && UnityEngine.Object.op_Equality((UnityEngine.Object) mUnitMarker[index].transform.parent, (UnityEngine.Object) transform))
          return;
      }
      for (int index = mUnitMarker.Count - 1; index >= 0; --index)
      {
        if (mUnitMarker[index].layer == GameUtility.LayerHidden)
        {
          mUnitMarker[index].transform.SetParent(transform, false);
          int num = 1;
          if (!unit.IsNormalSize)
            num = unit.SizeZ;
          mUnitMarker[index].transform.localPosition = Vector3.op_Multiply(Vector3.up, (float) num + 1f);
          GameUtility.SetLayer(mUnitMarker[index], GameUtility.LayerUI, true);
          mUnitMarker[index].SetActive(true);
          return;
        }
      }
      GameObject go = UnityEngine.Object.Instantiate<GameObject>(this.mUnitMarkerTemplates[(int) markerType], Vector3.zero, Quaternion.identity);
      go.transform.SetParent(((Component) unitController).transform, false);
      int num1 = 1;
      if (!unit.IsNormalSize)
        num1 = unit.SizeZ;
      go.transform.localPosition = Vector3.op_Multiply(Vector3.up, (float) num1 + 1f);
      GameUtility.SetLayer(go, GameUtility.LayerUI, true);
      mUnitMarker.Add(go);
    }

    private void DeactivateUnusedUnitMarkers()
    {
      for (int index1 = 0; index1 < this.mUnitMarkers.Length; ++index1)
      {
        for (int index2 = 0; index2 < this.mUnitMarkers[index1].Count; ++index2)
        {
          if (this.mUnitMarkers[index1][index2].layer == GameUtility.LayerHidden)
            this.mUnitMarkers[index1][index2].SetActive(false);
        }
      }
    }

    private void ShowEnemyUnitMarkers()
    {
      this.HideUnitMarkers(SceneBattle.UnitMarkerTypes.Enemy);
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
      {
        Unit unit = this.mTacticsUnits[index].Unit;
        if (unit.Side == EUnitSide.Enemy && !unit.IsDead)
          this.ShowUnitMarker(unit, SceneBattle.UnitMarkerTypes.Enemy);
      }
    }

    public GameObject PopupDamageNumber(TacticsUnitController tuc, int number, float offs_y = 0.0f)
    {
      return this.PopupNumber(tuc, number, Color.white, this.mDamageTemplate, offs_y);
    }

    public GameObject PopupHpHealNumber(TacticsUnitController tuc, int number)
    {
      return this.PopupNumber(tuc, number, Color.white, this.mHpHealTemplate);
    }

    public GameObject PopupMpHealNumber(TacticsUnitController tuc, int number)
    {
      return this.PopupNumber(tuc, number, Color.white, this.mMpHealTemplate);
    }

    public GameObject PopupNumber(
      TacticsUnitController tuc,
      int number,
      Color color,
      DamagePopup popup,
      float offs_y = 0.0f)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) popup, (UnityEngine.Object) null))
      {
        Debug.LogError((object) "mDamageTemplate == null");
        return (GameObject) null;
      }
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) tuc))
        return (GameObject) null;
      Vector3 uiDispPosition = tuc.UIDispPosition;
      DamagePopup damagePopup = UnityEngine.Object.Instantiate<DamagePopup>(popup);
      damagePopup.Value = number;
      damagePopup.DigitColor = color;
      SceneBattle.Popup2D(((Component) damagePopup).gameObject, uiDispPosition, 1, offs_y);
      return ((Component) damagePopup).gameObject;
    }

    public GameObject PopupDamageDsgNumber(
      TacticsUnitController tuc,
      int number,
      eDamageDispType ddt)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mDamageDsgTemplate))
      {
        Debug.LogError((object) "mDamageDsgTemplate == null");
        return (GameObject) null;
      }
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) tuc))
        return (GameObject) null;
      Vector3 uiDispPosition = tuc.UIDispPosition;
      DamageDsgPopup damageDsgPopup = UnityEngine.Object.Instantiate<DamageDsgPopup>(this.mDamageDsgTemplate);
      damageDsgPopup.Setup(number, Color.white, ddt);
      SceneBattle.Popup2D(((Component) damageDsgPopup).gameObject, uiDispPosition, 1);
      return ((Component) damageDsgPopup).gameObject;
    }

    public bool HasMissPopup => UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mMissPopup, (UnityEngine.Object) null);

    public bool HasPerfectAvoidPopup
    {
      get => UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPerfectAvoidPopup, (UnityEngine.Object) null);
    }

    public bool HasGuardPopup => UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGuardPopup, (UnityEngine.Object) null);

    public bool HasAbsorbPopup => UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mAbsorbPopup, (UnityEngine.Object) null);

    public bool HasCriticalPopup
    {
      get => UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCriticalPopup, (UnityEngine.Object) null);
    }

    public bool HasBackstabPopup
    {
      get => UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBackstabPopup, (UnityEngine.Object) null);
    }

    public bool HasWeakPopup => UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mWeakPopup, (UnityEngine.Object) null);

    public bool HasResistPopup => UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mResistPopup, (UnityEngine.Object) null);

    public bool HasGutsPopup => UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGutsPopup, (UnityEngine.Object) null);

    public void PopupMiss(Vector3 position, float yOffset = 0.0f)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mMissPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D(UnityEngine.Object.Instantiate<GameObject>(this.mMissPopup), position, yOffset: yOffset);
    }

    public void PopupPefectAvoid(Vector3 position, float yOffset = 0.0f)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPerfectAvoidPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D(UnityEngine.Object.Instantiate<GameObject>(this.mPerfectAvoidPopup), position, yOffset: yOffset);
    }

    public void PopupGuard(Vector3 position, float yOffset = 0.0f)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGuardPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D(UnityEngine.Object.Instantiate<GameObject>(this.mGuardPopup), position, yOffset: yOffset);
    }

    public void PopupAbsorb(Vector3 position, float yOffset = 0.0f)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mAbsorbPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D(UnityEngine.Object.Instantiate<GameObject>(this.mAbsorbPopup), position, yOffset: yOffset);
    }

    public void PopupCritical(Vector3 position, float yOffset = 0.0f)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCriticalPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D(UnityEngine.Object.Instantiate<GameObject>(this.mCriticalPopup), position, yOffset: yOffset);
    }

    public void PopupBackstab(Vector3 position, float yOffset = 0.0f)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBackstabPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D(UnityEngine.Object.Instantiate<GameObject>(this.mBackstabPopup), position, yOffset: yOffset);
    }

    public void PopupWeak(Vector3 position, float yOffset = 0.0f)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mWeakPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D(UnityEngine.Object.Instantiate<GameObject>(this.mWeakPopup), position, yOffset: yOffset);
    }

    public void PopupResist(Vector3 position, float yOffset = 0.0f)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mResistPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D(UnityEngine.Object.Instantiate<GameObject>(this.mResistPopup), position, yOffset: yOffset);
    }

    public void PopupGuts(Vector3 position, float yOffset = 0.0f)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGutsPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D(UnityEngine.Object.Instantiate<GameObject>(this.mGutsPopup), position, yOffset: yOffset);
    }

    public void PopupGoodJob(Vector3 position, GameObject prefab, Sprite sprite = null)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) prefab, (UnityEngine.Object) null))
        return;
      GameObject go = UnityEngine.Object.Instantiate<GameObject>(prefab);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
        return;
      Image componentInChildren = !UnityEngine.Object.op_Equality((UnityEngine.Object) sprite, (UnityEngine.Object) null) ? go.GetComponentInChildren<Image>() : (Image) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        componentInChildren.sprite = sprite;
      SceneBattle.Popup2D(go, position);
    }

    private Transform CreateParamChangeEffect(ParamTypes type, bool isDebuff)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mParamChangeEffectTemplate, (UnityEngine.Object) null))
        return (Transform) null;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mParamChangeEffectTemplate);
      BuffEffectText component = gameObject.GetComponent<BuffEffectText>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        component.SetText(type, isDebuff);
      return gameObject.transform;
    }

    private Transform CreateEffectChangeCT(int val)
    {
      if (val == 0)
        return (Transform) null;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mParamChangeEffectTemplate, (UnityEngine.Object) null))
        return (Transform) null;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mParamChangeEffectTemplate);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
        return (Transform) null;
      BuffEffectText component = gameObject.GetComponent<BuffEffectText>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) component.Text, (UnityEngine.Object) null))
      {
        string key = "quest.POP_INC_CT";
        component.Text.BottomColor = GameSettings.Instance.Buff_TextBottomColor;
        component.Text.TopColor = GameSettings.Instance.Buff_TextTopColor;
        if (val < 0)
        {
          val *= -1;
          key = "quest.POP_DEC_CT";
          component.Text.BottomColor = GameSettings.Instance.Debuff_TextBottomColor;
          component.Text.TopColor = GameSettings.Instance.Debuff_TextTopColor;
        }
        ((Text) component.Text).text = string.Format(LocalizedText.Get(key), (object) val);
      }
      return gameObject.transform;
    }

    public void PopupParamChange(
      Vector3 position,
      BuffBit buff,
      BuffBit debuff,
      int ct_change_val = 0)
    {
      if (buff == null || debuff == null)
        return;
      GameObject go = new GameObject("Params", new System.Type[2]
      {
        typeof (RectTransform),
        typeof (DelayStart)
      });
      Transform transform = go.transform;
      for (int index = 0; index < SkillParam.MAX_PARAMTYPES; ++index)
      {
        ParamTypes type = (ParamTypes) index;
        if (buff.CheckBit(type))
        {
          Transform paramChangeEffect = this.CreateParamChangeEffect(type, false);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) paramChangeEffect, (UnityEngine.Object) null))
            paramChangeEffect.SetParent(transform, false);
        }
        if (debuff.CheckBit(type))
        {
          Transform paramChangeEffect = this.CreateParamChangeEffect(type, true);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) paramChangeEffect, (UnityEngine.Object) null))
            paramChangeEffect.SetParent(transform, false);
        }
      }
      int val = ct_change_val / 10;
      if (val != 0)
      {
        Transform effectChangeCt = this.CreateEffectChangeCT(val);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) effectChangeCt, (UnityEngine.Object) null))
          effectChangeCt.SetParent(transform, false);
      }
      SceneBattle.Popup2D(go, position);
    }

    private Transform CreateConditionChangeEffect(EUnitCondition condition)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mConditionChangeEffectTemplate, (UnityEngine.Object) null))
        return (Transform) null;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mConditionChangeEffectTemplate);
      CondEffectText component = gameObject.GetComponent<CondEffectText>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        component.SetText(condition);
      return gameObject.transform;
    }

    public void PopupConditionChange(Vector3 position, EUnitCondition fails)
    {
      GameObject go = new GameObject("Conditions", new System.Type[2]
      {
        typeof (RectTransform),
        typeof (DelayStart)
      });
      Transform transform = go.transform;
      for (int index = 0; index < (int) Unit.MAX_UNIT_CONDITION; ++index)
      {
        EUnitCondition condition = (EUnitCondition) (1L << index);
        if ((condition & fails) != (EUnitCondition) 0)
        {
          Transform conditionChangeEffect = this.CreateConditionChangeEffect(condition);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) conditionChangeEffect, (UnityEngine.Object) null))
            conditionChangeEffect.SetParent(transform, false);
        }
      }
      SceneBattle.Popup2D(go, position);
    }

    public void ShowSkillNamePlate(Unit unit, SkillData skill, string skill_name = "", float speed = 1f)
    {
      if (skill == null)
        this.mSkillNamePlate.SetSkillName(skill_name, unit.Side);
      else
        this.mSkillNamePlate.SetSkillName(skill.Name, unit.Side, skill.ElementType, skill.AttackDetailType, skill.AttackType);
      this.mSkillNamePlate.Open(speed);
      ((Component) this.mSkillNamePlate).transform.SetParent(((Component) this.OverlayCanvas).transform, false);
    }

    public void ShowSkillNamePlate(string skill_name, EUnitSide side = EUnitSide.Player, float speed = 1f)
    {
      this.mSkillNamePlate.SetSkillName(skill_name, side);
      this.mSkillNamePlate.Open(speed);
      ((Component) this.mSkillNamePlate).transform.SetParent(((Component) this.OverlayCanvas).transform, false);
    }

    public bool IsClosedSkillNamePlate() => this.mSkillNamePlate.IsClosed();

    public void ShowInspirationTelopPlate(float disp_time, float speed = 1f)
    {
      this.mInspirationTelopPlate.Open(speed, disp_time);
      ((Component) this.mInspirationTelopPlate).transform.SetParent(((Component) this.OverlayCanvas).transform, false);
    }

    public static void Popup2D(GameObject go, Vector3 position, int priority = 0, float yOffset = 0.0f)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
        return;
      SceneBattle.Instance.InternalPopup2D(go, position, priority, yOffset);
    }

    private void InternalPopup2D(GameObject go, Vector3 position, int priority, float yOffset = 0.0f)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
        return;
      this.mPopups.Add(new KeyValuePair<SceneBattle.PupupData, GameObject>(new SceneBattle.PupupData(position, priority, yOffset), go));
      go.transform.SetParent(((Component) this.OverlayCanvas).transform, false);
      this.mPopupPositionCache.Clear();
      RectTransform transform = go.transform as RectTransform;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
        return;
      transform.anchoredPosition = new Vector2((float) -Screen.width, (float) -Screen.height);
    }

    private void LayoutPopups(Camera cam)
    {
      Canvas overlayCanvas = this.OverlayCanvas;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) overlayCanvas, (UnityEngine.Object) null))
        return;
      RectTransform transform1 = ((Component) overlayCanvas).transform as RectTransform;
      int[] array = new int[this.mPopups.Count];
      for (int index = 0; index < this.mPopups.Count; ++index)
      {
        GameObject gameObject = this.mPopups[index].Value;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          array[index] = gameObject.transform.GetSiblingIndex();
      }
      Array.Sort<int>(array);
      int[] numArray = new int[this.mPopups.Count];
      GameObject[] gameObjectArray = new GameObject[this.mPopups.Count];
      for (int index = 0; index < this.mPopups.Count; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPopups[index].Value, (UnityEngine.Object) null))
        {
          numArray[index] = this.mPopups[index].Key.priority;
          gameObjectArray[index] = this.mPopups[index].Value;
        }
      }
      for (int index1 = 0; index1 < numArray.Length - 1; ++index1)
      {
        for (int index2 = index1 + 1; index2 < numArray.Length; ++index2)
        {
          if (numArray[index1] > numArray[index2])
          {
            int num = numArray[index1];
            numArray[index1] = numArray[index2];
            numArray[index2] = num;
            GameObject gameObject = gameObjectArray[index1];
            gameObjectArray[index1] = gameObjectArray[index2];
            gameObjectArray[index2] = gameObject;
          }
        }
      }
      for (int index = 0; index < gameObjectArray.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObjectArray[index], (UnityEngine.Object) null))
          gameObjectArray[index].transform.SetSiblingIndex(array[index]);
      }
      for (int index = 0; index < this.mPopups.Count; ++index)
      {
        GameObject gameObject = this.mPopups[index].Value;
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        {
          this.mPopups.RemoveAt(index--);
        }
        else
        {
          Vector3 screenPoint = cam.WorldToScreenPoint(this.mPopups[index].Key.position);
          ref Vector3 local1 = ref screenPoint;
          double num1 = (double) screenPoint.x / (double) Screen.width;
          Rect rect1 = transform1.rect;
          double width = (double) ((Rect) ref rect1).width;
          double num2 = num1 * width;
          local1.x = (float) num2;
          ref Vector3 local2 = ref screenPoint;
          double num3 = (double) screenPoint.y / (double) Screen.height;
          Rect rect2 = transform1.rect;
          double height = (double) ((Rect) ref rect2).height;
          double num4 = num3 * height;
          local2.y = (float) num4;
          screenPoint.y += this.mPopups[index].Key.yOffset;
          if (this.isScreenMirroring)
          {
            ref Vector3 local3 = ref screenPoint;
            Rect rect3 = transform1.rect;
            double num5 = (double) ((Rect) ref rect3).width - (double) screenPoint.x;
            local3.x = (float) num5;
          }
          RectTransform transform2 = gameObject.transform as RectTransform;
          if (!this.mPopupPositionCache.ContainsKey(transform2) || Vector3.op_Inequality(this.mPopupPositionCache[transform2], screenPoint))
          {
            RectTransform rectTransform = transform2;
            Vector2 zero = Vector2.zero;
            transform2.anchorMax = zero;
            Vector2 vector2 = zero;
            rectTransform.anchorMin = vector2;
            transform2.anchoredPosition = Vector2.op_Implicit(screenPoint);
            this.mPopupPositionCache[transform2] = screenPoint;
          }
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBlockedGridMarker, (UnityEngine.Object) null))
        return;
      if (this.mDisplayBlockedGridMarker)
      {
        TacticsUnitController unitController = this.FindUnitController(this.mBattle.CurrentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          Vector3 vector3 = unitController.CenterPosition;
          if (this.mGridDisplayBlockedGridMarker != null)
            vector3 = Vector3.op_Addition(this.CalcGridCenter(this.mGridDisplayBlockedGridMarker), Vector3.op_Multiply(Vector3.up, unitController.Height * 0.5f));
          Vector3 screenPoint = cam.WorldToScreenPoint(vector3);
          Vector2 vector2;
          RectTransformUtility.ScreenPointToLocalPointInRectangle(this.mTouchController.GetRectTransform(), Vector2.op_Implicit(screenPoint), (Camera) null, ref vector2);
          RectTransform component = this.mBlockedGridMarker.GetComponent<RectTransform>();
          if (!this.mPopupPositionCache.ContainsKey(component) || Vector3.op_Inequality(this.mPopupPositionCache[component], screenPoint))
          {
            this.mPopupPositionCache[component] = screenPoint;
            component.anchoredPosition = vector2;
          }
        }
      }
      else
        GameUtility.ToggleGraphic(this.mBlockedGridMarker, true);
      this.mBlockedGridMarker.GetComponent<Animator>().SetBool("visible", this.mDisplayBlockedGridMarker);
    }

    private void LayoutGauges(Camera cam)
    {
      Canvas overlayCanvas = this.OverlayCanvas;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) overlayCanvas, (UnityEngine.Object) null))
        return;
      List<TacticsUnitController> instances = TacticsUnitController.Instances;
      RectTransform transform = ((Component) overlayCanvas).transform as RectTransform;
      this.mLayoutGauges.Clear();
      List<Vector3> vector3List = new List<Vector3>();
      for (int index = 0; index < instances.Count; ++index)
      {
        TacticsUnitController tacticsUnitController = instances[index];
        RectTransform hpGaugeTransform = tacticsUnitController.HPGaugeTransform;
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) hpGaugeTransform, (UnityEngine.Object) null) && ((Component) hpGaugeTransform).gameObject.activeInHierarchy)
        {
          Vector3 uiDispPosition = tacticsUnitController.UIDispPosition;
          uiDispPosition.y += 0.5f;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) ((Transform) hpGaugeTransform).parent, (UnityEngine.Object) transform))
            ((Transform) hpGaugeTransform).SetParent((Transform) transform, false);
          Vector3 screenPoint = cam.WorldToScreenPoint(uiDispPosition);
          ref Vector3 local1 = ref screenPoint;
          double num1 = (double) screenPoint.x / (double) Screen.width;
          Rect rect1 = transform.rect;
          double width = (double) ((Rect) ref rect1).width;
          double num2 = num1 * width;
          local1.x = (float) num2;
          ref Vector3 local2 = ref screenPoint;
          double num3 = (double) screenPoint.y / (double) Screen.height;
          Rect rect2 = transform.rect;
          double height = (double) ((Rect) ref rect2).height;
          double num4 = num3 * height;
          local2.y = (float) num4;
          vector3List.Add(screenPoint);
          this.mLayoutGauges.Add(hpGaugeTransform);
        }
      }
      Vector4[] points = new Vector4[this.mLayoutGauges.Count];
      for (int index = 0; index < this.mLayoutGauges.Count; ++index)
      {
        points[index].x = vector3List[index].x;
        points[index].y = vector3List[index].y;
        points[index].z = this.mGaugeCollisionRadius;
        points[index].w = 0.0f;
      }
      GameUtility.ApplyAvoidance(ref points, 3, this.mGaugeYAspectRatio);
      for (int index = 0; index < this.mLayoutGauges.Count; ++index)
      {
        RectTransform mLayoutGauge = this.mLayoutGauges[index];
        Vector2 vector2_1;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_1).\u002Ector(points[index].x, points[index].y);
        if (!this.mGaugePositionCache.ContainsKey(mLayoutGauge) || Vector2.op_Inequality(this.mGaugePositionCache[mLayoutGauge], vector2_1))
        {
          this.mGaugePositionCache[mLayoutGauge] = vector2_1;
          RectTransform rectTransform = mLayoutGauge;
          Vector2 zero = Vector2.zero;
          mLayoutGauge.anchorMax = zero;
          Vector2 vector2_2 = zero;
          rectTransform.anchorMin = vector2_2;
          mLayoutGauge.anchoredPosition = vector2_1;
        }
      }
    }

    public bool UISignal
    {
      get => this.mUISignal;
      set => this.mUISignal = value;
    }

    private void SetPrioritizedUnit(TacticsUnitController controller)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) controller, (UnityEngine.Object) null))
        controller.AlwaysUpdate = true;
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTacticsUnits[index], (UnityEngine.Object) controller))
          this.mTacticsUnits[index].AlwaysUpdate = false;
      }
    }

    private void SetPrioritizedUnits(List<TacticsUnitController> controllers)
    {
      if (controllers == null)
      {
        for (int index = 0; index < this.mTacticsUnits.Count; ++index)
          this.mTacticsUnits[index].AlwaysUpdate = false;
      }
      else
      {
        for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        {
          if (controllers.Contains(this.mTacticsUnits[index]))
            this.mTacticsUnits[index].AlwaysUpdate = true;
          else
            this.mTacticsUnits[index].AlwaysUpdate = false;
        }
      }
    }

    private void ToggleRenkeiAura(bool visible)
    {
      if (visible)
      {
        List<Unit> helperUnits = this.mBattle.HelperUnits;
        if (helperUnits.Count > 0)
        {
          TacticsUnitController unitController = this.FindUnitController(this.mBattle.CurrentUnit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
            unitController.SetRenkeiAura(this.mRenkeiAuraEffect);
          for (int index = 0; index < this.mTacticsUnits.Count; ++index)
          {
            if (helperUnits.Contains(this.mTacticsUnits[index].Unit))
              this.mTacticsUnits[index].SetRenkeiAura(this.mRenkeiAuraEffect);
          }
          return;
        }
      }
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        this.mTacticsUnits[index].StopRenkeiAura();
    }

    private void OnDrag()
    {
      if (!this.mBattle.IsMapCommand || this.mBattle.IsUnitAuto(this.mBattle.CurrentUnit))
        return;
      TacticsUnitController unitController = this.FindUnitController(this.mBattle.CurrentUnit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) && unitController.IsStartSkill())
        return;
      VirtualStick2 virtualStick = this.mBattleUI.VirtualStick;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) virtualStick, (UnityEngine.Object) null) || !this.IsControlBattleUI(SceneBattle.eMaskBattleUI.VS_SWIPE))
        return;
      virtualStick.Visible = true;
      virtualStick.SetPosition(this.mTouchController.DragStart);
      virtualStick.Delta = this.mTouchController.DragDelta;
    }

    private void OnDragEnd()
    {
      if (!this.mBattle.IsMapCommand || this.mBattle.IsUnitAuto(this.mBattle.CurrentUnit))
        return;
      TacticsUnitController unitController = this.FindUnitController(this.mBattle.CurrentUnit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) && unitController.IsStartSkill())
        return;
      VirtualStick2 virtualStick = this.mBattleUI.VirtualStick;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) virtualStick, (UnityEngine.Object) null))
        return;
      virtualStick.Visible = false;
    }

    private void ToggleJumpSpots(bool visible)
    {
      for (int index = 0; index < this.mJumpSpotEffects.Count; ++index)
        this.mJumpSpotEffects[index].Value.SetActive(visible);
    }

    private void RefreshJumpSpots()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mJumpSpotEffectTemplate, (UnityEngine.Object) null))
        return;
      for (int index1 = 0; index1 < this.mBattle.Units.Count; ++index1)
      {
        Unit unit = this.mBattle.Units[index1];
        if (!unit.IsDead && unit.CastSkill != null && (unit.CastSkill.CastType == ECastTypes.Jump || unit.CastSkill.TeleportType != eTeleportType.None))
        {
          bool flag = false;
          for (int index2 = 0; index2 < this.mJumpSpotEffects.Count; ++index2)
          {
            if (this.mJumpSpotEffects[index2].Key == unit)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            int x = unit.x;
            int y = unit.y;
            if (unit.CastSkill.TeleportType != eTeleportType.None)
            {
              x = unit.GridTarget.x;
              y = unit.GridTarget.y;
            }
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mJumpSpotEffectTemplate, this.CalcGridCenter(x, y), Quaternion.identity);
            if (!unit.IsNormalSize && UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
            {
              int num = unit.SizeX <= unit.SizeY ? unit.SizeY : unit.SizeX;
              gameObject.transform.localScale = new Vector3((float) num, (float) num, (float) num);
            }
            this.mJumpSpotEffects.Add(new KeyValuePair<Unit, GameObject>(unit, gameObject));
          }
        }
      }
      for (int index = this.mJumpSpotEffects.Count - 1; index >= 0; --index)
      {
        Unit key = this.mJumpSpotEffects[index].Key;
        if (key.IsDead || key.CastSkill == null || key.CastSkill.CastType != ECastTypes.Jump && key.CastSkill.TeleportType == eTeleportType.None)
        {
          GameUtility.StopEmitters(this.mJumpSpotEffects[index].Value);
          this.mJumpSpotEffects[index].Value.AddComponent<OneShotParticle>();
          this.mJumpSpotEffects.RemoveAt(index);
        }
      }
    }

    public GameObject GetJumpSpotEffect(Unit unit)
    {
      for (int index = 0; index < this.mJumpSpotEffects.Count; ++index)
      {
        if (this.mJumpSpotEffects[index].Key == unit)
          return this.mJumpSpotEffects[index].Value;
      }
      return (GameObject) null;
    }

    private void LoadShieldEffects()
    {
      List<SkillParam> skillParamList = new List<SkillParam>();
      foreach (Unit allUnit in this.mBattle.AllUnits)
      {
        for (int index = 0; index < allUnit.Shields.Count; ++index)
          skillParamList.Add(allUnit.Shields[index].skill_param);
      }
      foreach (TacticsUnitController mTacticsUnit in this.mTacticsUnits)
      {
        foreach (TacticsUnitController.ShieldState shieldState in mTacticsUnit.ShieldStates)
          skillParamList.Add(shieldState.Target.skill_param);
      }
      List<SkillParam> skills = new List<SkillParam>();
      foreach (SkillParam key in skillParamList)
      {
        if (!string.IsNullOrEmpty(key.defend_effect) && !this.mShieldEffects.ContainsKey(key) && !skills.Contains(key))
          skills.Add(key);
      }
      if (skills.Count == 0)
        return;
      this.mLoadingShieldEffects = true;
      this.StartCoroutine(this.LoadShieldEffectsAsync(skills));
    }

    [DebuggerHidden]
    private IEnumerator LoadShieldEffectsAsync(List<SkillParam> skills)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CLoadShieldEffectsAsync\u003Ec__IteratorB()
      {
        skills = skills,
        \u0024this = this
      };
    }

    private GameObject SpawnShieldEffect(
      TacticsUnitController unit,
      SkillParam skill,
      int value,
      int valueMax,
      int turn,
      int turnMax)
    {
      if (!this.mShieldEffects.ContainsKey(skill))
        return (GameObject) null;
      GameObject mShieldEffect = this.mShieldEffects[skill];
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) mShieldEffect, (UnityEngine.Object) null))
        return (GameObject) null;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(mShieldEffect, ((Component) unit).transform.position, Quaternion.identity);
      Animator component = gameObject.GetComponent<Animator>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
      {
        component.SetInteger("shield_val", value);
        if (valueMax > 0)
        {
          component.SetFloat("shield_val_norm", (float) value / (float) valueMax);
          float num = (float) value * 100f / (float) valueMax;
          component.SetInteger("shield_val_per", (double) num >= 50.0 ? Mathf.FloorToInt(num) : Mathf.CeilToInt(num));
        }
        component.SetInteger("shield_turn", turn);
        if (turnMax > 0)
        {
          component.SetFloat("shield_turn_norm", (float) turn / (float) turnMax);
          float num = (float) turn * 100f / (float) turnMax;
          component.SetInteger("shield_turn_per", (double) num >= 50.0 ? Mathf.FloorToInt(num) : Mathf.CeilToInt(num));
        }
      }
      return gameObject;
    }

    public class MoveInput
    {
      public SceneBattle SceneOwner;
      public SceneBattle.MoveInput.TargetSelectEvent OnAttackTargetSelect;

      public virtual void MoveUnit(Vector3 target_screen_pos)
      {
      }

      public virtual void Start()
      {
      }

      public virtual void End()
      {
      }

      public virtual bool IsBusy => false;

      public virtual void Reset()
      {
      }

      public virtual void Update()
      {
      }

      protected void SelectAttackTarget(Unit unit)
      {
        if (this.OnAttackTargetSelect == null)
          return;
        this.OnAttackTargetSelect(unit);
      }

      public delegate void TargetSelectEvent(Unit unit);
    }

    public class SimpleEvent
    {
      private static Dictionary<int, SceneBattle.SimpleEvent.Interface> m_Group = new Dictionary<int, SceneBattle.SimpleEvent.Interface>();

      public static void Clear() => SceneBattle.SimpleEvent.m_Group.Clear();

      public static bool HasGroup(int group) => SceneBattle.SimpleEvent.m_Group.ContainsKey(group);

      public static void Send(int group, string key, object obj)
      {
        SceneBattle.SimpleEvent.Interface nterface = (SceneBattle.SimpleEvent.Interface) null;
        if (!SceneBattle.SimpleEvent.m_Group.TryGetValue(group, out nterface))
          return;
        nterface.OnEvent(key, obj);
      }

      public static void Add(int group, SceneBattle.SimpleEvent.Interface inst)
      {
        if (SceneBattle.SimpleEvent.HasGroup(group))
          return;
        SceneBattle.SimpleEvent.m_Group.Add(group, inst);
      }

      public static void Remove(int group) => SceneBattle.SimpleEvent.m_Group.Remove(group);

      public interface Interface
      {
        void OnEvent(string key, object obj);
      }
    }

    private enum eArenaSubmitMode
    {
      BUSY,
      SUCCESS,
      FAILED,
    }

    public enum StateTransitionTypes
    {
      Forward,
      Back,
    }

    public delegate void StateTransitionRequest(SceneBattle.StateTransitionTypes type);

    private class State_ReqBtlComReq : State<SceneBattle>
    {
      private GUIEventListener mGUIEvent;
      private bool mShowInventory;
      private Vector2 mInventoryScrollPos;
      private int mSelectChangeInventoryIndex = -1;
      private bool mShowArtifact;
      private int mArtifactTab;
      private int mSelectChangeArtifactUnitIndex = -1;
      private int mSelectChangeArtifactJobIndex = -1;
      private int mSelectChangeArtifactIndex = -1;
      private string mFindQuestName = string.Empty;
      private List<string> mQuestHistoryIds = new List<string>();
      private bool mShowHistory;
      private const int HistoryMax = 10;
      private List<QuestParam> QuestParamList;
      private int mQuestFilterFlg;
      private static readonly SceneBattle.State_ReqBtlComReq.EQuestFilter[] mQuestFilters = new SceneBattle.State_ReqBtlComReq.EQuestFilter[11]
      {
        SceneBattle.State_ReqBtlComReq.EQuestFilter.Story,
        SceneBattle.State_ReqBtlComReq.EQuestFilter.Seiseki,
        SceneBattle.State_ReqBtlComReq.EQuestFilter.Babel,
        SceneBattle.State_ReqBtlComReq.EQuestFilter.Genesis,
        SceneBattle.State_ReqBtlComReq.EQuestFilter.Event,
        SceneBattle.State_ReqBtlComReq.EQuestFilter.Chara,
        SceneBattle.State_ReqBtlComReq.EQuestFilter.Advance,
        SceneBattle.State_ReqBtlComReq.EQuestFilter.Multi,
        SceneBattle.State_ReqBtlComReq.EQuestFilter.Tower,
        SceneBattle.State_ReqBtlComReq.EQuestFilter.Raid,
        SceneBattle.State_ReqBtlComReq.EQuestFilter.Other
      };
      private static readonly string[] mQuestFilterNames = new string[11]
      {
        "Story",
        "聖石",
        "バベル",
        "創世",
        "イベ",
        "キャラ",
        "Adv",
        "マルチ",
        "塔",
        "レイド",
        "他"
      };
      private bool[] mHitQuests;
      private List<QuestParam> mQuestList;

      public override void Begin(SceneBattle self)
      {
      }

      public override void End(SceneBattle self)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGUIEvent, (UnityEngine.Object) null))
          return;
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mGUIEvent);
        this.mGUIEvent = (GUIEventListener) null;
      }

      public override void Update(SceneBattle self)
      {
        if (!string.IsNullOrEmpty(GlobalVars.SelectedQuestID) || !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mGUIEvent, (UnityEngine.Object) null) || Network.Mode != Network.EConnectMode.Offline)
          return;
        this.mGUIEvent = ((Component) self).gameObject.AddComponent<GUIEventListener>();
        this.mGUIEvent.Listeners = new GUIEventListener.GUIEvent(this.OnGUI);
        GameUtility.FadeOut(0.0f);
      }

      private void SetQuestFilter(SceneBattle.State_ReqBtlComReq.EQuestFilter type, bool flag)
      {
        if (flag)
          this.mQuestFilterFlg |= (int) type;
        else
          this.mQuestFilterFlg &= (int) ~type;
      }

      private bool GetQuestFilter(SceneBattle.State_ReqBtlComReq.EQuestFilter type)
      {
        return (SceneBattle.State_ReqBtlComReq.EQuestFilter) 0 < ((SceneBattle.State_ReqBtlComReq.EQuestFilter) this.mQuestFilterFlg & type);
      }

      private void AddHistory(string newIdName)
      {
        if (this.mQuestHistoryIds.Contains(newIdName))
          this.mQuestHistoryIds.Remove(newIdName);
        this.mQuestHistoryIds.Insert(0, newIdName);
        if (this.mQuestHistoryIds.Count <= 10)
          return;
        this.mQuestHistoryIds.RemoveRange(10, this.mQuestHistoryIds.Count - 10);
      }

      private void CreateSaveHistory()
      {
        if (this.mQuestHistoryIds.Count <= 10)
          return;
        this.mQuestHistoryIds.RemoveRange(10, this.mQuestHistoryIds.Count - 10);
      }

      private void OnGUI(GameObject go)
      {
      }

      private enum EQuestFilter
      {
        Story = 1,
        Seiseki = 2,
        Babel = 4,
        Genesis = 8,
        Event = 16, // 0x00000010
        Chara = 32, // 0x00000020
        Advance = 64, // 0x00000040
        Multi = 128, // 0x00000080
        Tower = 256, // 0x00000100
        Raid = 512, // 0x00000200
        Other = 1024, // 0x00000400
      }
    }

    private class State_Start : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        QuestParam mCurrentQuest = self.mCurrentQuest;
        if (string.IsNullOrEmpty(mCurrentQuest.event_start) || !self.mIsFirstPlay || self.Battle.CheckEnableSuspendStart())
          this.GotoNextState();
        else
          self.StartCoroutine(this.LoadAndExecuteEvent(mCurrentQuest.event_start));
      }

      [DebuggerHidden]
      private IEnumerator LoadAndExecuteEvent(string eventName)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_Start.\u003CLoadAndExecuteEvent\u003Ec__Iterator0()
        {
          eventName = eventName,
          \u0024this = this
        };
      }

      private void GotoNextState()
      {
        if (this.self.mCurrentQuest.IsScenario)
        {
          this.self.GotoState<SceneBattle.State_ExitQuest>();
        }
        else
        {
          GameUtility.SetNeverSleep();
          if (this.self.mIsFirstPlay && !string.IsNullOrEmpty(this.self.mCurrentQuest.event_start))
            ProgressWindow.OpenQuestLoadScreen((string) null, (string) null);
          else
            ProgressWindow.OpenQuestLoadScreen(this.self.mCurrentQuest);
          this.self.GotoState<SceneBattle.State_InitUI>();
        }
      }
    }

    private class State_InitUI : State<SceneBattle>
    {
      private float mWait;
      private float mElapsed;
      private bool mLoaded;

      public override void Begin(SceneBattle self)
      {
        GameSettings instance = GameSettings.Instance;
        string loadOkyakusamaCode = instance.QuestLoad_OkyakusamaCode;
        if (!GameUtility.IsDebugBuild || MonoSingleton<GameManager>.Instance.Player.OkyakusamaCode != loadOkyakusamaCode)
          this.mWait = 0.0f;
        else
          this.mWait = instance.QuestLoad_WaitSecond;
      }

      public override void Update(SceneBattle self)
      {
        this.mElapsed += Time.deltaTime;
        if ((double) this.mWait >= (double) this.mElapsed || this.mLoaded)
          return;
        if (self.IsPlayingGvGQuest && !(bool) GlobalVars.GvGBattleReplay)
          self.StartCoroutine(self.LoadUIGvGAsync());
        else
          self.StartCoroutine(self.LoadUIAsync());
        this.mLoaded = true;
      }
    }

    private class State_WaitForLog : State<SceneBattle>
    {
      private bool CheckShieldEffect()
      {
        TacticsUnitController unitController = this.self.FindUnitController(this.self.Battle.CurrentUnit);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController))
        {
          unitController.UpdateShields();
          if (this.self.FindChangedShield(out TacticsUnitController _, out TacticsUnitController.ShieldState _))
          {
            this.self.mIgnoreShieldEffect.Clear();
            this.self.GotoState<SceneBattle.State_SpawnShieldEffects>();
            return true;
          }
        }
        return false;
      }

      public override void Update(SceneBattle self)
      {
        BattleLogServer logs = self.Battle.Logs;
        while (logs.Num > 0)
        {
          BattleLog peek = logs.Peek;
          switch (peek)
          {
            case LogError _:
              self.GotoState<SceneBattle.State_Error>();
              return;
            case LogConnect _:
              self.GotoState<SceneBattle.State_Connect>();
              return;
            case LogUnitStart _:
              if (self.IsNoCountUpUnitStartCount)
              {
                self.IsNoCountUpUnitStartCount = false;
              }
              else
              {
                ++self.mUnitStartCount;
                ++self.mUnitStartCountTotal;
              }
              self.mBattleUI.OnUnitStart();
              if (!UnityEngine.Object.op_Equality((UnityEngine.Object) self.mBattleUI_MultiPlay, (UnityEngine.Object) null))
              {
                if (self.Battle.CurrentUnit.OwnerPlayerIndex > 0 && self.Battle.CurrentUnit.OwnerPlayerIndex == self.Battle.MyPlayerIndex)
                {
                  self.mBattleUI_MultiPlay.OnMyUnitStart();
                }
                else
                {
                  Unit unit = self.Battle.AllUnits.Find((Predicate<Unit>) (u => u.OwnerPlayerIndex == self.Battle.MyPlayerIndex));
                  EUnitSide eunitSide = unit == null ? self.Battle.CurrentUnit.Side : unit.Side;
                  if (self.Battle.CurrentUnit.Side == eunitSide)
                    self.mBattleUI_MultiPlay.OnOtherUnitStart();
                  else if (self.Battle.IsMultiVersus)
                    self.mBattleUI_MultiPlay.OnOtherUnitStart();
                  else
                    self.mBattleUI_MultiPlay.OnEnemyUnitStart();
                }
              }
              self.GotoState<SceneBattle.State_PreUnitStart>();
              return;
            case LogUnitEntry _:
              self.GotoState<SceneBattle.State_SpawnUnit>();
              return;
            case LogUnitWithdraw _:
              self.GotoState<SceneBattle.State_UnitWithdraw>();
              return;
            case LogCancelTransform _:
              self.GotoState<SceneBattle.State_CancelTransform>();
              return;
            case LogInspiration _:
              self.GotoState<SceneBattle.State_Inspiration>();
              return;
            case LogWeather _:
              self.GotoState<SceneBattle.State_Weather>();
              return;
            case LogMapCommand _:
              if (this.CheckShieldEffect())
                return;
              self.RefreshUnitStatus(self.mBattle.CurrentUnit);
              self.RemoveLog();
              self.GotoMapCommand();
              return;
            case LogMapWait _:
              self.Battle.SetManualPlayFlag();
              self.GotoState<SceneBattle.State_MapWait>();
              return;
            case LogMapMove _:
              self.Battle.SetManualPlayFlag();
              self.GotoState<SceneBattle.State_AnimateMove>();
              return;
            case LogMapEvent _:
              if (self.ConditionalGotoGimmickState())
                return;
              continue;
            case LogSkill _:
              if (((LogSkill) peek).skill.AttackType == AttackTypes.MagAttack)
                self.Battle.SetManualPlayFlag();
              self.GotoPrepareSkill();
              return;
            case LogCastSkillStart _:
              self.mBattleUI.OnCastSkillStart();
              self.GotoState<SceneBattle.State_CastSkillStart>();
              return;
            case LogCastSkillEnd _:
              self.RefreshUnitStatus(self.mBattle.CurrentUnit);
              self.GotoState<SceneBattle.State_CastSkillEnd>();
              return;
            case LogMapTrick _:
              self.GotoState<SceneBattle.State_MapTrick>();
              return;
            case LogUnitEnd _:
              if (this.CheckShieldEffect())
                return;
              if (!UnityEngine.Object.op_Equality((UnityEngine.Object) self.mBattleUI_MultiPlay, (UnityEngine.Object) null))
              {
                if (self.Battle.CurrentUnit.OwnerPlayerIndex > 0 && self.Battle.CurrentUnit.OwnerPlayerIndex == self.Battle.MyPlayerIndex)
                {
                  self.mBattleUI_MultiPlay.OnMyUnitEnd();
                }
                else
                {
                  Unit unit = self.Battle.AllUnits.Find((Predicate<Unit>) (u => u.OwnerPlayerIndex == self.Battle.MyPlayerIndex));
                  EUnitSide eunitSide = unit == null ? self.Battle.CurrentUnit.Side : unit.Side;
                  if (self.Battle.CurrentUnit.Side == eunitSide)
                    self.mBattleUI_MultiPlay.OnOtherUnitEnd();
                  else
                    self.mBattleUI_MultiPlay.OnEnemyUnitEnd();
                }
              }
              if (self.Battle.IsMultiVersus)
              {
                self.Battle.RemainVersusTurnCount = (uint) self.UnitStartCountTotal;
                self.ArenaActionCountSet(self.Battle.RemainVersusTurnCount);
              }
              LogUnitEnd logUnitEnd = peek as LogUnitEnd;
              if (logUnitEnd.self != null)
              {
                TacticsUnitController unitController = self.FindUnitController(logUnitEnd.self);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
                  unitController.ClearBadStatusLocks();
              }
              self.GotoState<SceneBattle.State_UnitEnd>();
              return;
            case LogOrdealChangeNext _:
              self.GotoState<SceneBattle.State_OrdealChangeNext>();
              return;
            case LogDelay _:
              self.GotoState<SceneBattle.State_LogDelay>();
              return;
            case LogMapEnd _:
              if (self.IsPlayingPreCalcResultQuest)
                self.ArenaActionCountSet(self.Battle.ArenaActionCount);
              self.GotoMapEnd();
              return;
            case LogDead _:
              if (!self.Battle.IsBattle)
              {
                self.GotoState<SceneBattle.State_EventMapDead>();
                return;
              }
              self.GotoState<SceneBattle.State_BattleDead>();
              return;
            case LogGetTreasure _:
              self.GotoState<SceneBattle.State_MapDead>();
              return;
            case LogRevive _:
              if (!self.Battle.IsBattle)
              {
                self.GotoState<SceneBattle.State_MapRevive>();
                return;
              }
              self.RemoveLog();
              continue;
            case LogDamage _:
              DebugUtility.LogWarning("warning damage.");
              self.RemoveLog();
              continue;
            case LogAutoHeal _:
              self.GotoState<SceneBattle.State_AutoHeal>();
              return;
            case LogFailCondition _:
              LogFailCondition logFailCondition = peek as LogFailCondition;
              if (logFailCondition.self != null)
              {
                TacticsUnitController unitController = self.FindUnitController(logFailCondition.self);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
                {
                  unitController.UnlockUpdateBadStatus(logFailCondition.condition);
                  unitController.UpdateBadStatus();
                }
              }
              self.RemoveLog();
              continue;
            case LogCureCondition _:
              self.RemoveLog();
              continue;
            case LogCast _:
              self.Battle.SetManualPlayFlag();
              self.GotoState<SceneBattle.State_PrepareCast>();
              return;
            case LogFall _:
              self.GotoState<SceneBattle.State_JumpFall>();
              return;
            case LogSync _:
              self.RemoveLog();
              self.GotoState<SceneBattle.State_MultiPlaySync>();
              return;
            default:
              DebugUtility.LogError("不明なログを検出しました " + peek.GetType().ToString());
              self.RemoveLog();
              return;
          }
        }
      }
    }

    private class State_Error : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
      }

      public override void Update(SceneBattle self)
      {
      }

      public override void End(SceneBattle self) => self.RemoveLog();
    }

    private class State_Connect : State<SceneBattle>
    {
      public override void Begin(SceneBattle self) => self.RemoveLog();

      public override void Update(SceneBattle self)
      {
        if (Network.IsBusy)
          return;
        if (Network.IsError)
        {
          DebugUtility.LogError("connection error.");
        }
        else
        {
          if (self.Battle.Logs.Num == 0)
            DebugUtility.LogError("failed. logs not found.");
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
      }
    }

    private class State_QuestStartV2 : State<SceneBattle>
    {
      private bool is_call_map_start;

      public override void Update(SceneBattle self)
      {
        if (this.is_call_map_start)
          return;
        self.GotoMapStart();
        this.is_call_map_start = true;
      }
    }

    private class State_MapCommandAI : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          return;
        self.HideUnitCursor(unitController);
      }

      public override void Update(SceneBattle self)
      {
        if (self.IsCameraMoving)
          return;
        if (self.mBattle.ConditionalUnitEnd(false))
        {
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
        else
        {
          bool forceAI = self.Battle.IsMultiPlay && !self.Battle.IsUnitAuto(self.Battle.CurrentUnit);
          self.Battle.UpdateMapAI(forceAI);
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
      }
    }

    private class State_MapCommandMultiPlay : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.ShowAllHPGauges();
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          return;
        self.HideUnitCursor(unitController);
      }

      public override void End(SceneBattle self)
      {
      }

      private void SyncCameraPosition(SceneBattle self)
      {
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          return;
        GameSettings instance = GameSettings.Instance;
        Transform transform = ((Component) Camera.main).transform;
        transform.position = Vector3.op_Addition(((Component) unitController).transform.position, ((Component) instance.Quest.MoveCamera).transform.position);
        transform.rotation = ((Component) instance.Quest.MoveCamera).transform.rotation;
        self.SetCameraTarget((Component) unitController);
        self.mUpdateCameraPosition = true;
      }

      public override void Update(SceneBattle self)
      {
        this.SyncCameraPosition(self);
        if (!self.mBattle.ConditionalUnitEnd(true))
          return;
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    public class CloseBattleUIWindow
    {
      public List<Win_Btn_Decide_Title_Flx> mMsgBox = new List<Win_Btn_Decide_Title_Flx>();
      public List<Win_Btn_DecideCancel_FL_C> mYesNoDlg = new List<Win_Btn_DecideCancel_FL_C>();

      public void Add(GameObject go)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
          return;
        Win_Btn_Decide_Title_Flx component1 = go.GetComponent<Win_Btn_Decide_Title_Flx>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
          this.mMsgBox.Add(component1);
        Win_Btn_DecideCancel_FL_C component2 = go.GetComponent<Win_Btn_DecideCancel_FL_C>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
          return;
        this.mYesNoDlg.Add(component2);
      }

      public void CloseAll()
      {
        foreach (Win_Btn_Decide_Title_Flx btnDecideTitleFlx in this.mMsgBox)
          btnDecideTitleFlx.BeginClose();
        this.mMsgBox.Clear();
        foreach (Win_Btn_DecideCancel_FL_C btnDecideCancelFlC in this.mYesNoDlg)
          btnDecideCancelFlC.BeginClose();
        this.mYesNoDlg.Clear();
      }
    }

    private class ChargeTarget
    {
      public Unit mUnit;
      public uint mAttr;

      public ChargeTarget(Unit unit, uint attr)
      {
        this.mUnit = unit;
        this.mAttr = attr;
      }

      public void AddAttr(uint attr) => this.mAttr |= attr;
    }

    private class State_MapWait : State<SceneBattle>
    {
      public override void Begin(SceneBattle self) => self.HideUnitCursor(self.mBattle.CurrentUnit);

      public override void Update(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null))
        {
          TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          {
            self.mEventSequence = self.mEventScript.OnStandbyGrid(unitController, self.mIsFirstPlay);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
            {
              self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_WaitForLog>>();
              return;
            }
          }
        }
        self.GotoState<SceneBattle.State_WaitForLog>();
      }

      public override void End(SceneBattle self) => self.RemoveLog();
    }

    private class State_MapMoveSelect_Stick : State<SceneBattle>
    {
      private SceneBattle mScene;
      private TacticsUnitController mController;
      private Vector3 mTapStart;
      private float mPressTime;
      private bool mTapped;
      private GridMap<int> mAccessMap;
      private GridMap<int> mWalkableField;
      private Vector3 mStart;
      private bool mAllowTap;
      private int mDestX;
      private int mDestY;
      private Grid mStartGrid;
      private Vector2 mBasePos = Vector2.zero;
      private Vector2 mTargetPos = Vector2.zero;
      private bool mTargetSet;
      private bool mMoveStarted;
      private bool mClickedOK;
      private float mGridSnapTime;
      private bool mJumping;
      private bool mMoving;
      private bool mHasDesiredRotation;
      private bool mGridSnapping;
      private Quaternion mDesiredRotation;
      private const float STOP_RADIUS = 0.1f;

      private void Reset()
      {
        ((Component) this.mController).transform.position = this.mStart;
        this.mController.CancelAction();
        this.mScene.SendInputMoveEnd(this.mScene.Battle.CurrentUnit, true);
        this.mMoveStarted = false;
      }

      public override void Begin(SceneBattle self)
      {
        this.mScene = self;
        self.mAllowCameraTranslation = false;
        Unit currentUnit = self.Battle.CurrentUnit;
        this.mController = self.FindUnitController(currentUnit);
        this.mController.AutoUpdateRotation = false;
        self.ShowUnitCursorOnCurrent();
        BattleMap currentMap = self.Battle.CurrentMap;
        this.mAccessMap = self.CreateCurrentAccessMap();
        this.mWalkableField = this.mAccessMap.clone();
        this.mController.WalkableField = this.mWalkableField;
        self.ShowWalkableGrids(this.mWalkableField, 0);
        Grid grid = currentMap[currentUnit.x, currentUnit.y];
        this.mStartGrid = grid;
        this.mStart = self.CalcGridCenter(grid);
        this.mDestX = Mathf.FloorToInt(this.mStart.x);
        this.mDestY = Mathf.FloorToInt(this.mStart.z - ((Component) self.mTacticsSceneRoot).transform.position.z);
        this.mBasePos.x = (float) currentUnit.x + 0.5f;
        this.mBasePos.y = (float) currentUnit.y + 0.5f;
        this.mTargetPos = this.mBasePos;
        self.SetMoveCamera();
        self.mOnRequestStateChange = new SceneBattle.StateTransitionRequest(this.OnStateChange);
        self.SendInputMoveStart(self.Battle.CurrentUnit);
      }

      private void OnStateChange(SceneBattle.StateTransitionTypes transition)
      {
        switch (transition)
        {
          case SceneBattle.StateTransitionTypes.Forward:
            Grid current = this.mScene.mBattle.CurrentMap[this.mDestX, this.mDestY];
            EUnitDirection direction = this.mController.CalcUnitDirectionFromRotation();
            if (this.mScene.Battle.Move(this.mScene.Battle.CurrentUnit, current, direction, true))
            {
              this.mClickedOK = true;
              this.self.HideUnitCursor(this.mController);
              ((Component) this.mController).transform.position = this.mScene.CalcGridCenter(current);
              this.mScene.SendInputGridXY(this.mScene.Battle.CurrentUnit, this.mDestX, this.mDestY, this.mScene.Battle.CurrentUnit.Direction);
              this.mScene.SendInputMoveEnd(this.mScene.Battle.CurrentUnit, false);
              this.mScene.mBattleUI.OnInputMoveEnd();
              if (current == this.mStartGrid)
                break;
              this.self.mAutoActivateGimmick = true;
              break;
            }
            this.self.mCloseBattleUIWindow.Add(UIUtility.NegativeSystemMessage(LocalizedText.Get("sys.TITLE"), LocalizedText.Get("err.TARGET_GRID_BLOCKED"), (UIUtility.DialogResultEvent) null));
            break;
          case SceneBattle.StateTransitionTypes.Back:
            this.Reset();
            this.mClickedOK = true;
            this.mScene.GotoMapCommand();
            break;
        }
      }

      public override void End(SceneBattle self)
      {
        self.mAllowCameraTranslation = true;
        if (!this.mClickedOK)
          this.Reset();
        this.mController.AutoUpdateRotation = true;
        this.mController.StopRunning();
        this.mController.WalkableField = (GridMap<int>) null;
        self.HideUnitCursor(this.mController);
        self.mTacticsSceneRoot.HideGridLayer(0);
      }

      private bool IsGridBlocked(Vector2 co) => this.IsGridBlocked(co.x, co.y);

      private bool IsGridBlocked(float x, float y)
      {
        int x1 = Mathf.FloorToInt(x);
        int y1 = Mathf.FloorToInt(y);
        return !this.mAccessMap.isValid(x1, y1) || this.mAccessMap.get(x1, y1) < 0;
      }

      private bool CanMoveToAdj(Vector2 from, Vector2 to)
      {
        if (this.IsGridBlocked(to))
          return false;
        return !this.IsGridBlocked(from.x, to.y) || !this.IsGridBlocked(to.x, from.y);
      }

      private bool CanMoveToAdjDirect(Vector2 from, Vector2 to)
      {
        return !this.IsGridBlocked(from.x, to.y) && !this.IsGridBlocked(to.x, from.y);
      }

      private bool GridEqualIn2D(Vector2 a, Vector2 b)
      {
        return (int) a.x == (int) b.x && (int) a.y == (int) b.y;
      }

      private void AdjustTargetPos(
        ref Vector2 basePos,
        ref Vector2 targetPos,
        Vector2 inputDir,
        Vector2 unitPos)
      {
        if (this.CanMoveToAdj(basePos, targetPos))
          return;
        bool flag = false;
        Vector2 vector2 = Vector2.op_Subtraction(targetPos, basePos);
        if ((double) Vector3.Dot(Vector2.op_Implicit(((Vector2) ref vector2).normalized), Vector2.op_Implicit(Vector2.op_Subtraction(unitPos, basePos))) >= -0.10000000149011612)
        {
          Vector2[] vector2Array = new Vector2[8]
          {
            new Vector2(-1f, 1f),
            new Vector2(0.0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0.0f),
            new Vector2(1f, -1f),
            new Vector2(0.0f, -1f),
            new Vector2(-1f, -1f),
            new Vector2(-1f, 0.0f)
          };
          float[] numArray = new float[vector2Array.Length];
          ((Vector2) ref inputDir).Normalize();
          for (int index = 0; index < vector2Array.Length; ++index)
            numArray[index] = Vector2.Dot(((Vector2) ref vector2Array[index]).normalized, inputDir);
          for (int index1 = 0; index1 < vector2Array.Length; ++index1)
          {
            for (int index2 = index1 + 1; index2 < vector2Array.Length; ++index2)
            {
              if ((double) numArray[index1] < (double) numArray[index2])
              {
                GameUtility.swap<float>(ref numArray[index1], ref numArray[index2]);
                GameUtility.swap<Vector2>(ref vector2Array[index1], ref vector2Array[index2]);
              }
            }
          }
          Vector2 zero = Vector2.zero;
          for (int index = 1; index < vector2Array.Length && (double) numArray[index] >= 0.5; ++index)
          {
            zero.x = basePos.x + vector2Array[index].x;
            zero.y = basePos.y + vector2Array[index].y;
            if (this.CanMoveToAdj(this.mBasePos, zero))
            {
              targetPos = zero;
              flag = true;
              break;
            }
          }
        }
        if (flag && this.CanMoveToAdj(basePos, targetPos))
          return;
        targetPos = basePos;
      }

      private Vector2 Velocity
      {
        get
        {
          return UnityEngine.Object.op_Inequality((UnityEngine.Object) VirtualStick.Instance, (UnityEngine.Object) null) ? VirtualStick.Instance.GetVelocity(((Component) Camera.main).transform) : Vector2.zero;
        }
      }

      private void SyncCameraPosition()
      {
        this.mScene.SetCameraTarget((Component) this.mController, true);
      }

      public override void Update(SceneBattle self)
      {
        if (this.mGridSnapping)
        {
          this.SyncCameraPosition();
          if (!this.mController.isIdle)
            return;
          this.mGridSnapping = false;
        }
        if (!this.mMoving && (double) this.mGridSnapTime <= 0.0 && self.Battle.EntryBattleMultiPlayTimeUp)
        {
          self.Battle.EntryBattleMultiPlayTimeUp = false;
          Unit currentUnit = self.Battle.CurrentUnit;
          EUnitDirection direction = this.mController.CalcUnitDirectionFromRotation();
          if (!self.Battle.MoveMultiPlayer(currentUnit, this.mDestX, this.mDestY, direction))
          {
            this.Reset();
          }
          else
          {
            self.SendInputGridXY(currentUnit, currentUnit.x, currentUnit.y, currentUnit.Direction);
            self.SendInputMove(currentUnit, this.mController);
            self.SendInputMoveEnd(currentUnit, false);
            DebugUtility.Log("MoveEnd x:" + (object) currentUnit.x + " y:" + (object) currentUnit.y + " action:" + (object) this.mController.IsPlayingFieldAction);
          }
          self.SendInputUnitTimeLimit(currentUnit);
          self.SendInputFlush();
          self.Battle.EntryBattleMultiPlayTimeUp = true;
          this.mClickedOK = true;
          self.CloseBattleUI();
          self.GotoMapCommand();
        }
        else if (this.mClickedOK)
        {
          self.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
        }
        else
        {
          if (ObjectAnimator.Get((Component) Camera.main).isMoving)
            return;
          if (this.mController.IsPlayingFieldAction)
          {
            this.SyncCameraPosition();
          }
          else
          {
            if (this.mJumping)
            {
              this.mJumping = false;
              bool battleMultiPlayTimeUp = this.mScene.Battle.EntryBattleMultiPlayTimeUp;
              self.Battle.EntryBattleMultiPlayTimeUp = false;
              self.SendInputMove(self.Battle.CurrentUnit, this.mController);
              self.Battle.EntryBattleMultiPlayTimeUp = battleMultiPlayTimeUp;
            }
            Transform transform1 = ((Component) this.mController).transform;
            Vector2 vector2;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2).\u002Ector(transform1.position.x, transform1.position.z);
            vector2.y -= ((Component) self.mTacticsSceneRoot).transform.position.z;
            if (this.mTargetSet && this.GridEqualIn2D(vector2, this.mTargetPos))
            {
              this.mBasePos = this.mTargetPos;
              this.mDestX = Mathf.FloorToInt(this.mBasePos.x);
              this.mDestY = Mathf.FloorToInt(this.mBasePos.y);
              bool battleMultiPlayTimeUp = this.mScene.Battle.EntryBattleMultiPlayTimeUp;
              self.Battle.EntryBattleMultiPlayTimeUp = false;
              self.Battle.EntryBattleMultiPlayTimeUp = battleMultiPlayTimeUp;
            }
            Vector2 inputDir = this.Velocity;
            if (self.Battle.EntryBattleMultiPlayTimeUp)
              inputDir = Vector2.zero;
            if ((double) ((Vector2) ref inputDir).sqrMagnitude > 0.0)
            {
              if (!this.mMoveStarted)
                this.mMoveStarted = true;
              float num1 = Mathf.Floor((float) (((double) (Mathf.Atan2(inputDir.y, inputDir.x) * 57.29578f) + 22.5) / 45.0)) * 45f;
              float num2 = Mathf.Cos(num1 * ((float) Math.PI / 180f));
              float num3 = Mathf.Sin(num1 * ((float) Math.PI / 180f));
              float num4 = (double) Mathf.Abs(num2) < 9.9999997473787516E-05 ? 0.0f : Mathf.Sign(num2);
              float num5 = (double) Mathf.Abs(num3) < 9.9999997473787516E-05 ? 0.0f : Mathf.Sign(num3);
              this.mTargetPos.x = this.mBasePos.x + num4;
              this.mTargetPos.y = this.mBasePos.y + num5;
              this.AdjustTargetPos(ref this.mBasePos, ref this.mTargetPos, inputDir, vector2);
              Vector3 vector3;
              // ISSUE: explicit constructor call
              ((Vector3) ref vector3).\u002Ector(inputDir.x, 0.0f, inputDir.y);
              this.mDesiredRotation = Quaternion.LookRotation(vector3);
              this.mHasDesiredRotation = true;
              this.mTargetSet = true;
              this.mGridSnapTime = GameSettings.Instance.Quest.GridSnapDelay;
            }
            else
            {
              this.mMoving = false;
              this.mTargetSet = false;
              this.mController.StopRunning();
              if ((double) this.mGridSnapTime >= 0.0 && !this.mHasDesiredRotation)
              {
                this.mGridSnapTime -= Time.deltaTime;
                if ((double) this.mGridSnapTime <= 0.0)
                {
                  Grid current = self.mBattle.CurrentMap[this.mDestX, this.mDestY];
                  this.mController.StepTo(self.CalcGridCenter(current));
                  this.mGridSnapping = true;
                  if (self.Battle.CurrentUnit != null)
                    self.SendInputGridXY(self.Battle.CurrentUnit, this.mDestX, this.mDestY, self.Battle.CurrentUnit.Direction);
                }
              }
            }
            Vector3 vector3_1 = Vector3.zero;
            if (this.mTargetSet)
            {
              if (this.CanMoveToAdjDirect(this.mBasePos, this.mTargetPos))
                vector3_1 = Vector2.op_Implicit(Vector2.op_Subtraction(this.mTargetPos, vector2));
              else if (this.IsGridBlocked(vector2.x, this.mTargetPos.y))
                vector3_1.x = this.mTargetPos.x - vector2.x;
              else if (this.IsGridBlocked(this.mTargetPos.x, vector2.y))
                vector3_1.y = this.mTargetPos.y - vector2.y;
              else
                vector3_1 = Vector2.op_Implicit(Vector2.op_Subtraction(this.mTargetPos, vector2));
              if ((double) ((Vector3) ref vector3_1).magnitude < 0.10000000149011612)
                vector3_1 = Vector2.op_Implicit(Vector2.zero);
            }
            bool flag = (double) ((Vector3) ref vector3_1).sqrMagnitude > 0.0;
            if (this.mHasDesiredRotation || flag)
            {
              Quaternion quaternion = !flag ? this.mDesiredRotation : Quaternion.LookRotation(new Vector3(vector3_1.x, 0.0f, vector3_1.y));
              GameSettings instance = GameSettings.Instance;
              float magnitude = ((Vector2) ref inputDir).magnitude;
              float num6 = 0.0f;
              if (this.mMoving)
              {
                num6 = magnitude;
                ((Component) this.mController).transform.rotation = Quaternion.Slerp(((Component) this.mController).transform.rotation, quaternion, Time.deltaTime * 5f);
              }
              else
              {
                ((Component) this.mController).transform.rotation = Quaternion.Slerp(((Component) this.mController).transform.rotation, quaternion, Time.deltaTime * 10f);
                float num7 = Quaternion.Angle(quaternion, ((Component) this.mController).transform.rotation);
                if ((double) magnitude > 0.10000000149011612)
                {
                  if ((double) num7 < 1.0)
                  {
                    this.mMoving = true;
                    num6 = magnitude;
                  }
                  else
                  {
                    float num8 = 15f;
                    float num9 = Mathf.Clamp01((float) (1.0 - (double) num7 / (double) num8));
                    num6 = magnitude * num9;
                  }
                }
                if ((double) num7 < 1.0)
                {
                  ((Component) this.mController).transform.rotation = quaternion;
                  this.mHasDesiredRotation = false;
                }
              }
              if ((double) num6 > 0.0 && flag)
              {
                this.mController.StartRunning();
                float num10 = Mathf.Lerp(instance.Quest.MapRunSpeedMin, instance.Quest.MapRunSpeedMax, num6);
                Vector3 vector3_2;
                // ISSUE: explicit constructor call
                ((Vector3) ref vector3_2).\u002Ector(vector3_1.x, 0.0f, vector3_1.y);
                Vector3 velocity = Vector3.op_Multiply(((Vector3) ref vector3_2).normalized, num10);
                if (this.mController.TriggerFieldAction(velocity))
                {
                  Vector2 fieldActionPoint = this.mController.FieldActionPoint;
                  this.mDestX = Mathf.FloorToInt(fieldActionPoint.x);
                  this.mDestY = Mathf.FloorToInt(fieldActionPoint.y);
                  this.mHasDesiredRotation = false;
                  this.mTargetSet = false;
                  this.mJumping = true;
                  bool battleMultiPlayTimeUp = this.mScene.Battle.EntryBattleMultiPlayTimeUp;
                  self.Battle.EntryBattleMultiPlayTimeUp = false;
                  self.SendInputGridXY(self.Battle.CurrentUnit, this.mDestX, this.mDestY, self.Battle.CurrentUnit.Direction);
                  self.Battle.EntryBattleMultiPlayTimeUp = battleMultiPlayTimeUp;
                }
                else
                {
                  Transform transform2 = transform1;
                  transform2.position = Vector3.op_Addition(transform2.position, Vector3.op_Multiply(velocity, Time.deltaTime));
                }
              }
              bool battleMultiPlayTimeUp1 = this.mScene.Battle.EntryBattleMultiPlayTimeUp;
              self.Battle.EntryBattleMultiPlayTimeUp = false;
              self.SendInputMove(self.Battle.CurrentUnit, this.mController);
              self.Battle.EntryBattleMultiPlayTimeUp = battleMultiPlayTimeUp1;
            }
            this.SyncCameraPosition();
          }
        }
      }
    }

    private class State_AnimateMove : State<SceneBattle>
    {
      private Unit mMovingUnit;
      private TacticsUnitController mController;
      private bool mMoveUnit;
      private Vector3 mStartPosition;
      private Vector3 mEndPosition;
      private float mWaitCount = 1f;

      public override void Begin(SceneBattle self)
      {
        LogMapMove peek = (LogMapMove) self.mBattle.Logs.Peek;
        this.mMovingUnit = peek.self;
        this.mController = self.FindUnitController(this.mMovingUnit);
        this.mController.WalkableField = self.CreateCurrentAccessMap();
        this.mMoveUnit = peek.auto || this.mMovingUnit.IsUnitFlag(EUnitFlag.ForceMoved);
        if (self.Battle.IsMultiPlay && !self.Battle.IsUnitAuto(this.mMovingUnit) && self.Battle.EntryBattleMultiPlayTimeUp)
          this.mMoveUnit = true;
        IntVector2 intVector2 = self.CalcCoord(this.mController.CenterPosition);
        EUnitDirection eunitDirection = this.mController.CalcUnitDirectionFromRotation();
        if (intVector2.x == this.mMovingUnit.x && intVector2.y == this.mMovingUnit.y && eunitDirection == this.mMovingUnit.Direction)
          this.mMoveUnit = false;
        self.mUpdateCameraPosition = false;
        BattleCameraFukan gameObject = GameObjectID.FindGameObject<BattleCameraFukan>(self.mBattleUI.FukanCameraID);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null) && gameObject.isDisp)
          gameObject.SetDisp(false);
        if (this.mMoveUnit)
        {
          self.ShowWalkableGrids(self.CreateCurrentAccessMap(), 0);
        }
        else
        {
          while (self.mBattle.Logs.Num > 0 && self.mBattle.Logs.Peek is LogMapMove && ((LogMapMove) self.mBattle.Logs.Peek).self == this.mMovingUnit)
            self.RemoveLog();
        }
      }

      public override void Update(SceneBattle self)
      {
        if (this.mMoveUnit)
        {
          float num = GameSettings.Instance.AiUnit_MoveWait * Time.deltaTime;
          float mWaitCount = this.mWaitCount;
          this.mWaitCount -= num;
          if (0.0 < (double) this.mWaitCount)
            return;
          if (0.0 < (double) mWaitCount)
            this.Move();
        }
        if (!this.mController.isIdle)
        {
          self.OnGimmickUpdate();
        }
        else
        {
          self.ResetCameraTarget();
          self.mUpdateCameraPosition = true;
          BattleLog peek = self.mBattle.Logs.Peek;
          if (peek == null || !(peek is LogMapMove))
          {
            BattleCameraFukan gameObject = GameObjectID.FindGameObject<BattleCameraFukan>(self.mBattleUI.FukanCameraID);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null) && !gameObject.isDisp)
              gameObject.SetDisp(true);
          }
          if (self.mBattle.Logs.Num > 0)
            self.GotoState<SceneBattle.State_WaitForLog>();
          else
            self.GotoMapCommand();
        }
      }

      private void Move()
      {
        LogMapMove peek1 = (LogMapMove) this.self.mBattle.Logs.Peek;
        int length = 0;
        for (int offset = 0; offset < this.self.mBattle.Logs.Num && this.self.mBattle.Logs[offset] is LogMapMove; ++offset)
        {
          LogMapMove log = (LogMapMove) this.self.mBattle.Logs[offset];
          if (log != null && log.self == peek1.self)
            ++length;
          else
            break;
        }
        BattleMap currentMap = this.self.mBattle.CurrentMap;
        Vector3[] route = new Vector3[length];
        for (int index = 0; index < length; ++index)
        {
          LogMapMove peek2 = (LogMapMove) this.self.mBattle.Logs.Peek;
          route[index] = this.self.CalcGridCenter(currentMap[peek2.ex, peek2.ey]);
          this.self.RemoveLog();
        }
        this.mStartPosition = route[0];
        this.mEndPosition = route[route.Length - 1];
        float duration = this.mController.StartMove(route);
        ObjectAnimator.Get((Component) ((Component) Camera.main).transform).AnimateTo(Vector3.op_Addition(((Component) Camera.main).transform.position, Vector3.op_Subtraction(this.mEndPosition, this.mStartPosition)), ((Component) Camera.main).transform.rotation, duration, ObjectAnimator.CurveType.EaseInOut);
      }
    }

    private class State_EventMapDead : State<SceneBattle>
    {
      private TacticsUnitController mController;

      public override void Begin(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null) && self.mBattle.Logs.Peek is LogDead peek)
        {
          List<TacticsUnitController> tacticsUnitControllerList = new List<TacticsUnitController>();
          foreach (LogDead.Param obj in peek.list_normal)
          {
            TacticsUnitController unitController = self.FindUnitController(obj.self);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
              tacticsUnitControllerList.Add(unitController);
          }
          foreach (LogDead.Param obj in peek.list_sentence)
          {
            TacticsUnitController unitController = self.FindUnitController(obj.self);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
              tacticsUnitControllerList.Add(unitController);
          }
          foreach (TacticsUnitController controller in tacticsUnitControllerList)
          {
            self.mEventSequence = self.mEventScript.OnUnitDead(controller, self.mIsFirstPlay);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
            {
              this.mController = controller;
              self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
              self.InterpCameraTarget((Component) this.mController);
              return;
            }
          }
        }
        self.GotoState<SceneBattle.State_MapDead>();
      }

      public override void Update(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mController, (UnityEngine.Object) null) && self.IsCameraMoving)
          return;
        self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_EventMapDead>>();
      }
    }

    private class State_MapDead : State<SceneBattle>
    {
      private List<SceneBattle.State_MapDead.DirectionBase> mList = new List<SceneBattle.State_MapDead.DirectionBase>();

      public List<SceneBattle.State_MapDead.DirectionBase> list => this.mList;

      public override void Begin(SceneBattle self)
      {
        BattleLog peek = self.mBattle.Logs.Peek;
        switch (peek)
        {
          case LogGetTreasure _:
            LogGetTreasure logGetTreasure = peek as LogGetTreasure;
            if (logGetTreasure.self != null && logGetTreasure.self.Drop != null && self.mBattle.CurrentMap != null)
            {
              Unit self1 = logGetTreasure.self;
              Grid current = self.mBattle.CurrentMap[self1.x, self1.y];
              if ((self1.Side == EUnitSide.Enemy || self1.IsBreakObj) && self1.Drop.IsEnableDrop() && current != null)
              {
                Vector3 pos = self.CalcGridCenter(current);
                SceneBattle.State_MapDead.TreasureDrop.Normal normal1 = new SceneBattle.State_MapDead.TreasureDrop.Normal(self, this);
                normal1.Initialize(10, self1, self1.Drop, pos);
                this.mList.Add((SceneBattle.State_MapDead.DirectionBase) normal1);
                if (logGetTreasure.is_camera_move)
                {
                  SceneBattle.State_MapDead.Camera.Normal normal2 = new SceneBattle.State_MapDead.Camera.Normal(self, this);
                  normal2.Initialize(0, new Vector3[1]
                  {
                    pos
                  });
                  this.mList.Add((SceneBattle.State_MapDead.DirectionBase) normal2);
                  break;
                }
                break;
              }
              break;
            }
            break;
          case LogDead _:
            LogDead logDead = peek as LogDead;
            List<Vector3> vector3List = new List<Vector3>();
            for (int index = 0; index < logDead.list_normal.Count; ++index)
            {
              LogDead.Param obj = logDead.list_normal[index];
              TacticsUnitController unitController = self.FindUnitController(obj.self);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
              {
                SceneBattle.State_MapDead.Dead.Normal normal = new SceneBattle.State_MapDead.Dead.Normal(self, this);
                normal.Initialize(10, unitController, obj.type);
                this.mList.Add((SceneBattle.State_MapDead.DirectionBase) normal);
                vector3List.Add(unitController.CenterPosition);
              }
            }
            if (vector3List.Count > 0)
            {
              SceneBattle.State_MapDead.Camera.Normal normal = new SceneBattle.State_MapDead.Camera.Normal(self, this);
              normal.Initialize(0, vector3List.ToArray());
              this.mList.Add((SceneBattle.State_MapDead.DirectionBase) normal);
            }
            for (int index = 0; index < logDead.list_sentence.Count; ++index)
            {
              LogDead.Param obj = logDead.list_sentence[index];
              TacticsUnitController unitController = self.FindUnitController(obj.self);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
              {
                SceneBattle.State_MapDead.Dead.CameraForcus cameraForcus = new SceneBattle.State_MapDead.Dead.CameraForcus(self, this);
                cameraForcus.Initialize(100 + index, unitController, obj.type);
                this.mList.Add((SceneBattle.State_MapDead.DirectionBase) cameraForcus);
              }
            }
            break;
        }
        self.RemoveLog();
      }

      public override void Update(SceneBattle self)
      {
        for (int index = 0; index < this.mList.Count; ++index)
        {
          SceneBattle.State_MapDead.DirectionBase m = this.mList[index];
          if (!m.Update())
          {
            m.Release();
            this.mList.RemoveAt(index);
            --index;
          }
        }
        if (this.mList.Count != 0)
          return;
        self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
        self.OnGimmickUpdate();
        self.RefreshJumpSpots();
        self.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>();
      }

      public override void End(SceneBattle self)
      {
        for (int index = 0; index < this.mList.Count; ++index)
          this.mList[index].Release();
        this.mList.Clear();
      }

      public class DirectionBase
      {
        private SceneBattle mSceneBattle;
        private SceneBattle.State_MapDead mState;
        private int mPriority;
        private bool mDeleteFlag;
        private SceneBattle.State_MapDead.DirectionBase.Proc mProc;
        private int mProcCount;

        public DirectionBase(SceneBattle self, SceneBattle.State_MapDead state)
        {
          this.mSceneBattle = self;
          this.mState = state;
          this.SetProc(SceneBattle.State_MapDead.DirectionBase.Proc.PREPARE);
        }

        public SceneBattle scene => this.mSceneBattle;

        public SceneBattle.State_MapDead state => this.mState;

        public bool Update()
        {
          if (this.GetPriority() >= 0)
          {
            List<SceneBattle.State_MapDead.DirectionBase> list = this.mState.list;
            for (int index = 0; index < list.Count; ++index)
            {
              if (list[index].GetPriority() < this.GetPriority())
                return true;
            }
          }
          switch (this.mProc)
          {
            case SceneBattle.State_MapDead.DirectionBase.Proc.PREPARE:
              this.OnPrepare();
              this.SetProc(SceneBattle.State_MapDead.DirectionBase.Proc.BEGIN);
              break;
            case SceneBattle.State_MapDead.DirectionBase.Proc.BEGIN:
              if (!this.OnLoading())
              {
                this.OnBegin();
                this.SetProc(SceneBattle.State_MapDead.DirectionBase.Proc.MAIN);
                break;
              }
              break;
            case SceneBattle.State_MapDead.DirectionBase.Proc.MAIN:
              if (!this.OnMain())
              {
                this.Delete();
                break;
              }
              break;
          }
          if (this.mDeleteFlag)
          {
            this.SetProc(SceneBattle.State_MapDead.DirectionBase.Proc.END);
            this.OnEnd();
          }
          return !this.mDeleteFlag;
        }

        public void Delete() => this.mDeleteFlag = true;

        public void SetPriority(int pri) => this.mPriority = pri;

        public int GetPriority() => this.mPriority;

        protected void SetProc(SceneBattle.State_MapDead.DirectionBase.Proc proc)
        {
          this.mProc = proc;
          this.mProcCount = 0;
        }

        protected SceneBattle.State_MapDead.DirectionBase.Proc GetProc() => this.mProc;

        protected int GetProcCount() => this.mProcCount;

        protected void SetProcCount(int value) => this.mProcCount = value;

        protected void IncProcCount() => ++this.mProcCount;

        protected void DecProcCount() => --this.mProcCount;

        public virtual void Initialize(int priority) => this.mPriority = priority;

        public virtual void Release()
        {
        }

        protected virtual void OnPrepare()
        {
        }

        protected virtual bool OnLoading() => false;

        protected virtual void OnBegin()
        {
        }

        protected virtual bool OnMain() => false;

        protected virtual void OnEnd()
        {
        }

        protected enum Proc
        {
          PREPARE,
          BEGIN,
          MAIN,
          END,
        }
      }

      public class Camera : SceneBattle.State_MapDead.DirectionBase
      {
        protected Vector3 m_Center;
        protected float m_Distance;

        public Camera(SceneBattle self, SceneBattle.State_MapDead state)
          : base(self, state)
        {
        }

        public void Initialize(int priority, Vector3[] targets)
        {
          this.Initialize(priority);
          this.scene.GetCameraTargetView(out this.m_Center, out this.m_Distance, targets);
        }

        protected override void OnBegin()
        {
          this.scene.InterpCameraDistance(this.m_Distance);
          this.scene.InterpCameraTarget(this.m_Center);
        }

        protected override bool OnMain()
        {
          Vector3 vector3 = Vector3.op_Subtraction(this.scene.mCameraTarget, this.m_Center);
          vector3.y = 0.0f;
          return (double) ((Vector3) ref vector3).magnitude * (double) ((Vector3) ref vector3).magnitude >= 0.25;
        }

        public class Normal : SceneBattle.State_MapDead.Camera
        {
          public Normal(SceneBattle self, SceneBattle.State_MapDead state)
            : base(self, state)
          {
          }
        }
      }

      public class Dead : SceneBattle.State_MapDead.DirectionBase
      {
        protected TacticsUnitController mController;
        protected Unit mUnit;
        protected DeadTypes mDeadType;

        public Dead(SceneBattle self, SceneBattle.State_MapDead state)
          : base(self, state)
        {
        }

        public TacticsUnitController controller => this.mController;

        public Unit unit => this.mUnit;

        public DeadTypes deadType => this.mDeadType;

        public void Initialize(int priority, TacticsUnitController controller, DeadTypes deadType)
        {
          this.Initialize(priority);
          this.mController = controller;
          this.mUnit = controller.Unit;
          this.mDeadType = deadType;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mController, (UnityEngine.Object) null))
            return;
          this.mController.ReflectDispModel();
          this.mController.LoadDeathAnimation(TacticsUnitController.DeathAnimationTypes.Normal);
          if (this.mUnit == null || this.mUnit.Drop == null || this.mUnit.Side != EUnitSide.Enemy && !this.mUnit.IsBreakObj || !this.mUnit.Drop.IsEnableDrop())
            return;
          SceneBattle.State_MapDead.TreasureDrop.Normal normal = new SceneBattle.State_MapDead.TreasureDrop.Normal(this.scene, this.state);
          normal.Initialize(this.GetPriority() + 1, this.mUnit, this.mUnit.Drop, ((Component) controller).transform.position);
          this.state.list.Add((SceneBattle.State_MapDead.DirectionBase) normal);
        }

        protected override bool OnLoading() => this.mController.IsLoading;

        protected override void OnBegin()
        {
          this.mController.PlayDead(TacticsUnitController.DeathAnimationTypes.Normal);
          this.scene.OnUnitDeath(this.mController.Unit);
          this.scene.mTacticsUnits.Remove(this.mController);
          this.mController.ShowHPGauge(false);
          this.mController.ShowVersusCursor(false);
        }

        protected override bool OnMain()
        {
          return !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mController, (UnityEngine.Object) null);
        }

        public class Normal : SceneBattle.State_MapDead.Dead
        {
          public Normal(SceneBattle self, SceneBattle.State_MapDead state)
            : base(self, state)
          {
          }
        }

        public class CameraForcus : SceneBattle.State_MapDead.Dead
        {
          public CameraForcus(SceneBattle self, SceneBattle.State_MapDead state)
            : base(self, state)
          {
          }

          protected override void OnPrepare()
          {
            this.scene.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
            this.scene.InterpCameraTarget((Component) this.mController);
          }

          protected override bool OnLoading()
          {
            if (base.OnLoading())
              return true;
            switch (this.GetProcCount())
            {
              case 0:
                Vector3 vector3 = Vector3.op_Subtraction(this.scene.mCameraTarget, ((Component) this.mController).transform.position);
                vector3.y = 0.0f;
                if ((double) ((Vector3) ref vector3).magnitude * (double) ((Vector3) ref vector3).magnitude < 0.25)
                {
                  this.mController.DeathSentenceCountDown(true, 1f);
                  this.IncProcCount();
                  break;
                }
                break;
              case 1:
                if (this.mController.IsDeathSentenceCountDownPlaying())
                  return true;
                break;
            }
            return false;
          }
        }
      }

      public class TreasureDrop : SceneBattle.State_MapDead.DirectionBase
      {
        protected Unit m_Owner;
        protected Unit.UnitDrop m_Drop;
        protected TreasureBox m_TreasureBox;

        public TreasureDrop(SceneBattle self, SceneBattle.State_MapDead state)
          : base(self, state)
        {
        }

        public void Initialize(int priority, Unit owner, Unit.UnitDrop drop, Vector3 pos)
        {
          this.Initialize(priority);
          this.m_Owner = owner;
          if (this.m_Owner != null)
            this.m_Owner.BeginDropDirection();
          this.m_Drop = drop;
          this.m_TreasureBox = UnityEngine.Object.Instantiate<TreasureBox>(this.scene.mTreasureBoxTemplate);
          this.m_TreasureBox.owner = owner;
          ((Component) this.m_TreasureBox).transform.parent = ((Component) this.scene).transform;
          ((Component) this.m_TreasureBox).transform.position = pos;
          ((Component) this.m_TreasureBox).gameObject.SetActive(false);
        }

        protected override void OnBegin()
        {
          Unit.DropItem DropItem = (Unit.DropItem) null;
          for (int index = this.m_Drop.items.Count - 1; index >= 0; --index)
          {
            if (!Unit.DropItem.IsNullOrEmpty(this.m_Drop.items[index]))
            {
              DropItem = this.m_Drop.items[index];
              break;
            }
          }
          SceneBattle.Instance.AddTreasureCount(1);
          this.m_TreasureBox.Open(DropItem, this.scene.mTreasureDropTemplate, (int) this.m_Drop.gold, this.scene.mTreasureGoldTemplate);
        }

        protected override bool OnMain()
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_TreasureBox, (UnityEngine.Object) null))
            return false;
          if (!((Component) this.m_TreasureBox).gameObject.activeInHierarchy)
            ((Component) this.m_TreasureBox).gameObject.SetActive(true);
          return true;
        }

        public class Normal : SceneBattle.State_MapDead.TreasureDrop
        {
          public Normal(SceneBattle self, SceneBattle.State_MapDead state)
            : base(self, state)
          {
          }
        }
      }
    }

    public class TreasureEvent : SceneBattle.SimpleEvent.Interface
    {
      public static int GROUP = nameof (TreasureEvent).GetHashCode();

      public void OnEvent(string key, object obj)
      {
        switch (key)
        {
          case "DropItemEffect.End":
            DropItemEffect dropItemEffect = obj as DropItemEffect;
            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) dropItemEffect, (UnityEngine.Object) null))
              break;
            dropItemEffect.DropOwner?.EndDropDirection();
            SceneBattle.Instance.AddDispTreasureCount(1);
            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) dropItemEffect, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) dropItemEffect.TargetRect, (UnityEngine.Object) null))
              break;
            GameParameter.UpdateAll(((Component) dropItemEffect.TargetRect).gameObject);
            break;
          case "DropGoldEffect.End":
            DropGoldEffect dropGoldEffect = obj as DropGoldEffect;
            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) dropGoldEffect, (UnityEngine.Object) null))
              break;
            dropGoldEffect.DropOwner?.EndDropDirection();
            SceneBattle.Instance.AddGoldCount(dropGoldEffect.Gold);
            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) dropGoldEffect.TargetRect, (UnityEngine.Object) null))
              break;
            GameParameter.UpdateAll(((Component) dropGoldEffect.TargetRect).gameObject);
            break;
        }
      }
    }

    private class State_MapRevive : SceneBattle.State_SpawnUnit
    {
    }

    private class State_PickupGimmick : State<SceneBattle>
    {
      private SceneBattle mScene;
      private TacticsUnitController mUnitController;
      private TacticsUnitController mGimmickController;
      private Unit.UnitDrop mDrop;
      private MapPickup mPickup;
      private bool mLoadFinished;
      private bool mItemDrop;
      private LogMapEvent mLog;
      private DropItemEffect mDropItemEffect;

      public override void Begin(SceneBattle self)
      {
        this.mLog = (LogMapEvent) self.mBattle.Logs.Peek;
        this.mScene = self;
        this.mGimmickController = self.FindUnitController(this.mLog.target);
        this.mUnitController = self.FindUnitController(this.mLog.self);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitController, (UnityEngine.Object) null))
        {
          self.RemoveLog();
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
        else
        {
          if (this.mLog.target != null)
            this.mLog.target.BeginDropDirection();
          self.mUpdateCameraPosition = true;
          self.SetCameraOffset(((Component) GameSettings.Instance.Quest.UnitCamera).transform);
          self.InterpCameraTarget((Component) this.mUnitController);
          this.mGimmickController.ResetScale();
          this.mDrop = this.mLog.target.Drop;
          if (this.mDrop != null)
          {
            this.mItemDrop = this.mGimmickController.Unit != null && this.mGimmickController.Unit.CheckItemDrop();
            if (this.mItemDrop)
              SceneBattle.Instance.AddTreasureCount(1);
          }
          this.mUnitController.BeginLoadPickupAnimation();
          this.mPickup = ((Component) this.mGimmickController).GetComponentInChildren<MapPickup>();
          this.mPickup.OnPickup = new MapPickup.PickupEvent(this.OnPickupDone);
          this.mGimmickController.BeginLoadPickupAnimation();
          if (!this.mItemDrop || this.mDrop == null || this.mDrop.items.Count <= 0)
            return;
          this.mDropItemEffect = UnityEngine.Object.Instantiate<DropItemEffect>(this.mScene.mTreasureDropTemplate, Vector3.op_Addition(((Component) this.mGimmickController).transform.position, this.mPickup.DropEffectOffset), new Quaternion());
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDropItemEffect, (UnityEngine.Object) null))
            return;
          ((Component) this.mDropItemEffect).transform.SetParent(((Component) self.OverlayCanvas).transform, false);
          ((Component) this.mDropItemEffect).gameObject.GetComponent<DropItemIcon>().UpdateValue();
          ((Component) this.mDropItemEffect).gameObject.SetActive(false);
        }
      }

      public override void Update(SceneBattle self)
      {
        if (this.mUnitController.IsLoading || this.mGimmickController.IsLoading)
          return;
        this.mGimmickController.CollideGround = false;
        if (!this.mLoadFinished)
        {
          this.mLoadFinished = true;
          if (((LogMapEvent) self.mBattle.Logs.Peek).type == EEventType.Treasure)
          {
            MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0504");
            MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0501");
          }
          this.mUnitController.PlayPickup(((Component) this.mGimmickController).gameObject);
        }
        if (!this.mUnitController.isIdle)
          return;
        self.RemoveLog();
        self.FinishGimmickState();
      }

      public override void End(SceneBattle self)
      {
        this.mPickup.OnPickup = (MapPickup.PickupEvent) null;
        this.mGimmickController.PlayIdle();
        this.mGimmickController.UnloadPickupAnimation();
        ((Component) this.mGimmickController).gameObject.SetActive(false);
        self.RefreshUnitStatus(this.mUnitController.Unit);
        if (this.mLog.target == null)
          return;
        this.mLog.target.EndDropDirection();
      }

      private void OnPickupDone()
      {
        if (this.mUnitController.Unit.Side != EUnitSide.Player)
          return;
        if (this.mDrop != null)
        {
          Vector3 position = ((Component) this.mGimmickController).transform.position;
          if ((int) this.mDrop.gold > 0)
          {
            DropGoldEffect dropGoldEffect = UnityEngine.Object.Instantiate<DropGoldEffect>(this.mScene.mTreasureGoldTemplate, position, Quaternion.identity);
            dropGoldEffect.DropOwner = this.mLog.target;
            dropGoldEffect.Gold = (int) this.mDrop.gold;
          }
          if (this.mDrop.items.Count > 0 && this.mItemDrop)
          {
            ((Component) this.mDropItemEffect).gameObject.SetActive(true);
            this.mDropItemEffect.DropOwner = this.mLog.target;
            this.mDropItemEffect.DropItem = this.mDrop.items[this.mDrop.items.Count - 1];
          }
          if ((int) this.mDrop.gems > 0)
            this.self.StartCoroutine(this.GemPramChangePopup());
        }
        this.mGimmickController.PlayTakenAnimation();
      }

      [DebuggerHidden]
      private IEnumerator GemPramChangePopup()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_PickupGimmick.\u003CGemPramChangePopup\u003Ec__Iterator0()
        {
          \u0024this = this
        };
      }
    }

    private class State_BattleDead : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        LogDead peek = (LogDead) self.mBattle.Logs.Peek;
        List<LogDead.Param> objList = new List<LogDead.Param>();
        objList.AddRange((IEnumerable<LogDead.Param>) peek.list_normal);
        objList.AddRange((IEnumerable<LogDead.Param>) peek.list_sentence);
        foreach (LogDead.Param obj in objList)
        {
          Unit self1 = obj.self;
          self.OnUnitDeath(self1);
          self.HideUnitMarkers(self1);
          TacticsUnitController unitController = self.FindUnitController(self1);
          self.mTacticsUnits.Remove(unitController);
          UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) unitController).gameObject);
        }
        self.RemoveLog();
        self.GotoState<SceneBattle.State_WaitForLog>();
      }

      public override void Update(SceneBattle self)
      {
      }

      public override void End(SceneBattle self)
      {
      }
    }

    private class State_CastSkillStart : State<SceneBattle>
    {
      private TacticsUnitController mCasterController;

      public override void Begin(SceneBattle self)
      {
        this.mCasterController = self.FindUnitController(self.mBattle.CurrentUnit);
        SkillData castSkill = this.mCasterController.Unit.CastSkill;
        if (castSkill != null)
        {
          if (castSkill.CastType == ECastTypes.Jump)
          {
            self.GotoState<SceneBattle.State_CastSkillStartJump>();
            return;
          }
          this.mCasterController.ChargeIcon.Close();
          if (castSkill.EffectType == SkillEffectTypes.Changing)
            self.GotoState<SceneBattle.State_CastSkillStartChange>();
        }
        self.Battle.CastSkillStart();
        self.RemoveLog();
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_CastSkillStartJump : State<SceneBattle>
    {
      private TacticsUnitController mCasterController;

      public override void Begin(SceneBattle self)
      {
        this.mCasterController = self.FindUnitController(self.mBattle.CurrentUnit);
        self.Battle.CastSkillStart();
        self.RemoveLog();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCasterController, (UnityEngine.Object) null))
          this.mCasterController.ChargeIcon.Close();
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_CastSkillStartChange : State<SceneBattle>
    {
      private TacticsUnitController mCasterController;

      public override void Begin(SceneBattle self)
      {
        this.mCasterController = self.FindUnitController(self.mBattle.CurrentUnit);
        self.Battle.CastSkillStart();
        self.RemoveLog();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCasterController, (UnityEngine.Object) null))
          ;
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_CastSkillEnd : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.Battle.CastSkillEnd();
        self.RemoveLog();
        self.RefreshJumpSpots();
      }

      public override void Update(SceneBattle self)
      {
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_MapTrick : State<SceneBattle>
    {
      private SceneBattle.State_MapTrick.eSeqState mSeqState;
      private float mPassedTime;
      private LogMapTrick mLog;
      private LoadRequest mLoadReqEffect;
      private List<SceneBattle.State_MapTrick.TucMapTrick> mTucMapTrickLists = new List<SceneBattle.State_MapTrick.TucMapTrick>();
      private bool mIsAction;
      private bool mIsDamaged;
      private List<GameObject> mGoEffectLists = new List<GameObject>();
      private List<GameObject> mGoPopupLists = new List<GameObject>();

      public override void Begin(SceneBattle self)
      {
        this.mLog = self.Battle.Logs.Peek as LogMapTrick;
        if (this.mLog != null && this.mLog.TrickData != null)
        {
          this.mTucMapTrickLists.Clear();
          foreach (LogMapTrick.TargetInfo targetInfoList in this.mLog.TargetInfoLists)
          {
            TacticsUnitController unitController = self.FindUnitController(targetInfoList.Target);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
              this.mTucMapTrickLists.Add(new SceneBattle.State_MapTrick.TucMapTrick(unitController, targetInfoList));
          }
          if (this.mTucMapTrickLists.Count != 0)
          {
            if (!string.IsNullOrEmpty(this.mLog.TrickData.TrickParam.EffectName))
              this.mLoadReqEffect = AssetManager.LoadAsync<GameObject>(AssetPath.TrickEffect(this.mLog.TrickData.TrickParam.EffectName));
            foreach (SceneBattle.State_MapTrick.TucMapTrick mTucMapTrickList in this.mTucMapTrickLists)
            {
              LogMapTrick.TargetInfo mTargetInfo = mTucMapTrickList.mTargetInfo;
              if (mTargetInfo.Damage != 0 || mTargetInfo.Heal != 0 || mTargetInfo.KnockBackGrid != null)
              {
                if (mTargetInfo.Damage != 0)
                  mTucMapTrickList.mController.LoadDamageAnimations();
                this.mIsAction = true;
              }
            }
            if (this.mLoadReqEffect != null || this.mIsAction)
            {
              this.mSeqState = SceneBattle.State_MapTrick.eSeqState.INIT;
              return;
            }
          }
        }
        this.mSeqState = SceneBattle.State_MapTrick.eSeqState.EXIT;
      }

      public override void Update(SceneBattle self)
      {
        if (this.mLog == null || this.mLog.TrickData == null || this.mTucMapTrickLists.Count == 0)
          this.mSeqState = SceneBattle.State_MapTrick.eSeqState.EXIT;
        switch (this.mSeqState)
        {
          case SceneBattle.State_MapTrick.eSeqState.INIT:
            if (this.mLoadReqEffect != null)
            {
              this.mSeqState = SceneBattle.State_MapTrick.eSeqState.EFF_LOAD;
              break;
            }
            this.mSeqState = SceneBattle.State_MapTrick.eSeqState.ACT_INIT;
            break;
          case SceneBattle.State_MapTrick.eSeqState.EFF_LOAD:
            if (this.mLoadReqEffect != null)
            {
              if (!this.mLoadReqEffect.isDone)
                break;
              this.mGoEffectLists.Clear();
              GameObject asset = this.mLoadReqEffect.asset as GameObject;
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) asset))
              {
                foreach (SceneBattle.State_MapTrick.TucMapTrick mTucMapTrickList in this.mTucMapTrickLists)
                {
                  GameObject go = UnityEngine.Object.Instantiate<GameObject>(asset);
                  if (UnityEngine.Object.op_Implicit((UnityEngine.Object) go))
                  {
                    go.transform.SetParent(((Component) mTucMapTrickList.mController).transform, false);
                    ParticleSystem[] componentsInChildren = go.GetComponentsInChildren<ParticleSystem>();
                    if (componentsInChildren != null)
                    {
                      foreach (ParticleSystem particleSystem in componentsInChildren)
                      {
                        ParticleSystem.SubEmittersModule subEmitters = particleSystem.subEmitters;
                        ((ParticleSystem.SubEmittersModule) ref subEmitters).enabled = true;
                      }
                    }
                    GameUtility.RequireComponent<OneShotParticle>(go);
                    this.mGoEffectLists.Add(go);
                  }
                }
              }
              this.mLoadReqEffect = (LoadRequest) null;
              this.mPassedTime = 0.0f;
              this.mSeqState = SceneBattle.State_MapTrick.eSeqState.EFF_PLAY;
              break;
            }
            this.mSeqState = SceneBattle.State_MapTrick.eSeqState.ACT_INIT;
            break;
          case SceneBattle.State_MapTrick.eSeqState.EFF_PLAY:
            bool flag1 = false;
            this.mPassedTime += Time.deltaTime;
            if (this.mGoEffectLists.Count != 0)
            {
              for (int index = 0; index < this.mGoEffectLists.Count; ++index)
              {
                if ((double) this.mPassedTime < (double) GameSettings.Instance.Quest.TrickEffectWaitMaxTime && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mGoEffectLists[index]))
                {
                  flag1 = true;
                  break;
                }
                this.mGoEffectLists[index] = (GameObject) null;
              }
            }
            if (flag1)
              break;
            this.mGoEffectLists.Clear();
            this.mSeqState = SceneBattle.State_MapTrick.eSeqState.ACT_INIT;
            break;
          case SceneBattle.State_MapTrick.eSeqState.ACT_INIT:
            if (this.mIsAction)
            {
              bool flag2 = false;
              foreach (SceneBattle.State_MapTrick.TucMapTrick mTucMapTrickList in this.mTucMapTrickLists)
              {
                if (mTucMapTrickList.mController.IsLoading)
                {
                  flag2 = true;
                  break;
                }
              }
              if (flag2)
                break;
              this.mGoPopupLists.Clear();
              foreach (SceneBattle.State_MapTrick.TucMapTrick mTucMapTrickList in this.mTucMapTrickLists)
              {
                TacticsUnitController mController = mTucMapTrickList.mController;
                LogMapTrick.TargetInfo mTargetInfo = mTucMapTrickList.mTargetInfo;
                GameObject gameObject = (GameObject) null;
                if (mTargetInfo.Damage != 0)
                {
                  gameObject = self.PopupDamageNumber(mController, mTargetInfo.Damage);
                  mController.PlayDamage(HitReactionTypes.Normal);
                  MonoSingleton<GameManager>.Instance.Player.OnDamageToEnemy(this.mLog.TrickData.CreateUnit, mTargetInfo.Target, mTargetInfo.Damage);
                  this.mIsDamaged = true;
                }
                else if (mTargetInfo.Heal != 0)
                  gameObject = self.PopupHpHealNumber(mController, mTargetInfo.Heal);
                if (UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
                  this.mGoPopupLists.Add(gameObject);
                if (mTargetInfo.KnockBackGrid != null)
                {
                  mController.KnockBackGrid = mTargetInfo.KnockBackGrid;
                  mController.PlayTrickKnockBack();
                }
              }
              this.mSeqState = SceneBattle.State_MapTrick.eSeqState.ACT_PLAY;
              break;
            }
            this.mSeqState = SceneBattle.State_MapTrick.eSeqState.EXIT;
            break;
          case SceneBattle.State_MapTrick.eSeqState.ACT_PLAY:
            bool flag3 = false;
            if (this.mGoPopupLists.Count != 0)
            {
              for (int index = 0; index < this.mGoPopupLists.Count; ++index)
              {
                if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mGoPopupLists[index]))
                {
                  flag3 = true;
                  break;
                }
                this.mGoPopupLists[index] = (GameObject) null;
              }
            }
            foreach (SceneBattle.State_MapTrick.TucMapTrick mTucMapTrickList in this.mTucMapTrickLists)
            {
              if (mTucMapTrickList.mController.IsBusy)
              {
                flag3 = true;
                break;
              }
            }
            if (flag3)
              break;
            this.mGoPopupLists.Clear();
            foreach (SceneBattle.State_MapTrick.TucMapTrick mTucMapTrickList in this.mTucMapTrickLists)
              mTucMapTrickList.mController.UnloadBattleAnimations();
            this.mSeqState = SceneBattle.State_MapTrick.eSeqState.EXIT;
            break;
          case SceneBattle.State_MapTrick.eSeqState.EXIT:
            TrickData.CheckRemoveMarker(this.mLog.TrickData);
            self.RefreshJumpSpots();
            this.mLog = (LogMapTrick) null;
            self.RemoveLog();
            if (this.mIsDamaged)
            {
              self.GotoState<SceneBattle.State_TriggerHPEvents>();
              break;
            }
            self.GotoState<SceneBattle.State_WaitForLog>();
            break;
        }
      }

      private enum eSeqState
      {
        INIT,
        EFF_LOAD,
        EFF_PLAY,
        ACT_INIT,
        ACT_PLAY,
        EXIT,
      }

      private class TucMapTrick
      {
        public TacticsUnitController mController;
        public LogMapTrick.TargetInfo mTargetInfo;

        public TucMapTrick(TacticsUnitController tuc, LogMapTrick.TargetInfo ti)
        {
          this.mController = tuc;
          this.mTargetInfo = ti;
        }
      }
    }

    private class State_UnitEnd : State<SceneBattle>
    {
      private bool mIsPopDamage;
      private bool mIsShakeStart;
      private Unit mCurrentUnit;
      private TacticsUnitController mController;
      private bool mIsDamaged;

      public override void Begin(SceneBattle self)
      {
        self.ToggleRenkeiAura(false);
        if (self.mTutorialTriggers != null)
        {
          TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          {
            for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
              self.mTutorialTriggers[index].OnUnitEnd(self.mBattle.CurrentUnit, unitController.Unit.TurnCount);
          }
        }
        self.HideUnitCursor(((LogUnitEnd) self.mBattle.Logs.Peek).self);
        if (self.mBattle.IsUnitAuto(self.mBattle.CurrentUnit))
          self.HideGrid();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mTouchController, (UnityEngine.Object) null))
          self.mTouchController.IgnoreCurrentTouch();
        VirtualStick2 virtualStick = self.mBattleUI.VirtualStick;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) virtualStick, (UnityEngine.Object) null))
          virtualStick.Visible = false;
        this.mCurrentUnit = self.Battle.CurrentUnit;
        this.mController = self.FindUnitController(this.mCurrentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) null, (UnityEngine.Object) this.mController))
        {
          this.mController.InitShake(((Component) this.mController).transform.position, this.mCurrentUnit.Direction.ToVector());
          if (self.Battle.IsMultiVersus)
            this.mController.PlayVersusCursor(false);
        }
        self.RemoveLog();
      }

      public override void Update(SceneBattle self)
      {
        if (self.IsHPGaugeChanging)
          return;
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) null, (UnityEngine.Object) this.mController))
        {
          if (self.Battle.IsMultiPlay)
            self.EndMultiPlayer();
          self.Battle.UnitEnd();
          if (self.IsPlayingPreCalcResultQuest)
            self.ArenaActionCountSet(self.Battle.ArenaActionCount);
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
        else
        {
          if (!this.mController.ShakeStart)
          {
            if (self.Battle.IsMultiPlay)
              self.EndMultiPlayer();
            bool isDead = this.mCurrentUnit.IsDead;
            self.Battle.UnitEnd();
            if (self.IsPlayingPreCalcResultQuest)
              self.ArenaActionCountSet(self.Battle.ArenaActionCount);
            if (self.Battle.Logs.Peek is LogDamage peek && !this.mCurrentUnit.IsDead)
            {
              this.mController.ShakeStart = true;
              return;
            }
            if (peek != null && isDead != this.mCurrentUnit.IsDead)
            {
              self.PopupDamageNumber(this.mController, peek.damage);
              this.mController.ReflectDispModel();
              this.mIsDamaged = true;
              for (int index = 0; index < this.mCurrentUnit.CondAttachments.Count; ++index)
              {
                if (this.mCurrentUnit.CondAttachments[index].user != null)
                {
                  MonoSingleton<GameManager>.Instance.Player.OnDamageToEnemy(this.mCurrentUnit.CondAttachments[index].user, this.mCurrentUnit, peek.damage);
                  break;
                }
              }
            }
          }
          else
          {
            this.mController.AdvanceShake();
            if (0.30000001192092896 <= (double) this.mController.GetShakeProgress() && !this.mIsPopDamage)
            {
              this.mIsPopDamage = true;
              LogDamage peek = self.Battle.Logs.Peek as LogDamage;
              self.PopupDamageNumber(this.mController, peek.damage);
              this.mController.ReflectDispModel();
              this.mIsDamaged = true;
              for (int index = 0; index < this.mCurrentUnit.CondAttachments.Count; ++index)
              {
                if (this.mCurrentUnit.CondAttachments[index].user != null)
                {
                  MonoSingleton<GameManager>.Instance.Player.OnDamageToEnemy(this.mCurrentUnit.CondAttachments[index].user, this.mCurrentUnit, peek.damage);
                  break;
                }
              }
            }
            if (!this.mController.IsShakeEnd())
              return;
          }
          if (this.mIsDamaged)
            self.GotoState<SceneBattle.State_TriggerHPEvents>();
          else
            self.GotoState<SceneBattle.State_WaitForLog>();
        }
      }

      public override void End(SceneBattle self)
      {
        if (this.mCurrentUnit.OwnerPlayerIndex > 0)
          self.SendCheckMultiPlay();
        self.HideAllHPGauges();
        if (self.mFirstTurn)
          self.mFirstTurn = false;
        self.DeleteOnGimmickIcon();
      }
    }

    private class State_OrdealChangeNext : State<SceneBattle>
    {
      private bool mIsFinished;

      public override void Begin(SceneBattle self)
      {
        this.mIsFinished = false;
        self.StartCoroutine(this.StepOrdealChangeNextTeam());
      }

      [DebuggerHidden]
      private IEnumerator StepOrdealChangeNextTeam()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_OrdealChangeNext.\u003CStepOrdealChangeNextTeam\u003Ec__Iterator0()
        {
          \u0024this = this
        };
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mIsFinished)
          return;
        self.RemoveLog();
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_LogDelay : State<SceneBattle>
    {
      private float mTimer;
      private float mNow;

      public override void Begin(SceneBattle self)
      {
        this.mNow = 0.0f;
        this.mTimer = 0.0f;
        if (!(self.Battle.Logs.Peek is LogDelay peek))
          return;
        this.mTimer = peek.timer;
      }

      public override void Update(SceneBattle self)
      {
        this.mNow += Time.deltaTime;
        if ((double) this.mNow < (double) this.mTimer)
          return;
        self.RemoveLog();
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_MapEndV2 : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.SendCheckMultiPlay();
        self.RemoveLog();
        if (self.mTutorialTriggers != null)
        {
          for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
            self.mTutorialTriggers[index].OnMapEnd();
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) && (MonoSingleton<GameManager>.Instance.Player.IsAutoRepeatQuestMeasuring || !self.CurrentQuest.IsMissionCompleteALL()))
          MonoSingleton<GameManager>.Instance.EndQuestPlayTime();
        switch (self.Battle.GetQuestResult())
        {
          case BattleCore.QuestResult.Win:
            if (self.Battle.CheckNextMap())
            {
              self.mTacticsUnits.Clear();
              self.Battle.IncrementMap();
              self.GotoMapStart();
              break;
            }
            self.TriggerWinEvent();
            break;
          default:
            self.TriggerWinEvent();
            break;
        }
      }
    }

    public enum ExitRequests
    {
      None,
      End,
      Restart,
    }

    private class State_ArenaSkipWait : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        SRPG_TouchInputModule.UnlockInput();
        self.Battle.Logs.Reset();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI, (UnityEngine.Object) null))
        {
          self.mBattleUI.OnQuestEnd();
          self.mBattleUI.OnMapEnd();
        }
        self.GotoState_WaitSignal<SceneBattle.State_PreQuestResult>();
      }
    }

    public delegate void QuestEndEvent();

    private class State_PreQuestResult : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        BattleCore.QuestResult questResult = self.Battle.GetQuestResult();
        if (!self.IsPlayingPreCalcResultQuest)
        {
          self.SaveResult();
          GlobalVars.LastQuestResult.Set(questResult);
        }
        if (self.Battle.IsMultiTower)
        {
          MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
            instance.ForceCloseRoom();
        }
        switch (questResult)
        {
          case BattleCore.QuestResult.Win:
            if (self.Battle.IsMultiPlay && !self.Battle.IsMultiTower && !self.Battle.IsMultiVersus)
            {
              MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
                instance.ForceCloseRoom();
            }
            self.SendIgnoreMyDisconnect();
            self.OnQuestEnd();
            if (self.Battle.IsMultiPlay)
              MonoSingleton<GameManager>.Instance.Player.IncrementChallengeMultiNum();
            if (!self.IsPlayingPreCalcResultQuest)
              self.SubmitResult();
            if (!self.CurrentQuest.Silent)
            {
              if (self.IsPlayingArenaQuest)
                self.mBattleUI.OnArenaWin();
              else if (self.Battle.IsGenesisBoss)
                self.mBattleUI.OnGenesisBossWin();
              else if (self.Battle.IsAdvanceBoss)
                self.mBattleUI.OnAdvanceBossWin();
              else if (self.Battle.IsMultiVersus)
                self.mBattleUI.OnVersusWin();
              else if (!self.IsPlayingGvGQuest)
                self.mBattleUI.OnQuestWin();
              MonoSingleton<MySound>.Instance.PlayJingle("JIN_0002");
              Unit unit;
              if (self.IsPlayingPreCalcResultQuest && self.Battle.IsArenaSkip)
              {
                unit = self.Battle.Units.Find((Predicate<Unit>) (u => u == self.Battle.Leader && u.Side == EUnitSide.Player && u.IsEntry && !u.IsSub));
              }
              else
              {
                unit = self.Battle.CurrentUnit;
                if (unit != null && unit.Side != EUnitSide.Player && self.mLastPlayerSideUseSkillUnit != null)
                  unit = self.mLastPlayerSideUseSkillUnit;
              }
              unit?.PlayBattleVoice("battle_0029");
            }
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI_MultiPlay, (UnityEngine.Object) null))
            {
              if (!self.Battle.IsMultiVersus)
                self.mBattleUI_MultiPlay.OnQuestWin();
              else
                self.mExecDisconnected = true;
            }
            if (self.IsPlayingPreCalcResultQuest)
            {
              GameUtility.SetDefaultSleepSetting();
              self.GotoState_WaitSignal<SceneBattle.State_Result>();
              break;
            }
            self.GotoState_WaitSignal<SceneBattle.State_Result>();
            break;
          case BattleCore.QuestResult.Lose:
            self.OnQuestEnd();
            if (self.IsPlayingArenaQuest)
              self.mBattleUI.OnArenaLose();
            else if (!self.IsPlayingGvGQuest)
            {
              if (self.Battle.IsWorldRaid && !self.Battle.IsAllDead(EUnitSide.Player))
                self.mBattleUI.OnWorldRaidLose();
              else if (self.Battle.IsMultiVersus)
                self.mBattleUI.OnVersusLose();
              else
                self.mBattleUI.OnQuestLose();
            }
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI_MultiPlay, (UnityEngine.Object) null) && !self.Battle.IsMultiVersus)
              self.mBattleUI_MultiPlay.OnQuestLose();
            MonoSingleton<MySound>.Instance.PlayJingle("JIN_0003");
            if (self.Battle.IsMultiPlay)
            {
              if (!self.CurrentQuest.CheckDisableContinue() && !self.Battle.IsMultiVersus)
              {
                self.GotoState_WaitSignal<SceneBattle.State_MultiPlayContinue>();
                break;
              }
              self.mExecDisconnected = true;
              self.SendIgnoreMyDisconnect();
              if (!self.Battle.IsMultiVersus)
              {
                self.GotoState_WaitSignal<SceneBattle.State_ExitQuest>();
                break;
              }
              if (!self.mQuestResultSent)
                self.SubmitResult();
              self.GotoState_WaitSignal<SceneBattle.State_Result>();
              break;
            }
            if (self.IsPlayingPreCalcResultQuest)
            {
              if (self.Battle.IsArenaSkip)
                self.Battle.Units.Find((Predicate<Unit>) (u => u == self.Battle.Leader && u.Side == EUnitSide.Player && u.IsEntry && !u.IsSub))?.PlayBattleVoice("battle_0028");
              GameUtility.SetDefaultSleepSetting();
              self.GotoState_WaitSignal<SceneBattle.State_Result>();
              break;
            }
            if (self.IsPlayingTower)
            {
              if (self.mCurrentQuest.HasMission())
              {
                if (!self.mQuestResultSent)
                  self.SubmitResult();
                self.GotoState_WaitSignal<SceneBattle.State_Result>();
                break;
              }
              self.GotoState_WaitSignal<SceneBattle.State_ExitQuest>();
              break;
            }
            if (self.Battle.QuestType == QuestTypes.Raid || self.Battle.QuestType == QuestTypes.GuildRaid || self.Battle.QuestType == QuestTypes.GenesisBoss || self.Battle.QuestType == QuestTypes.AdvanceBoss || self.Battle.QuestType == QuestTypes.GvG || self.Battle.IsWorldRaid)
            {
              if (!self.mQuestResultSent)
                self.SubmitResult();
              self.GotoState_WaitSignal<SceneBattle.State_Result>();
              break;
            }
            bool flag = !self.CurrentQuest.CheckDisableContinue();
            if (flag)
            {
              BattleMap currentMap = self.Battle.CurrentMap;
              if (currentMap != null)
                flag = !self.Battle.CheckMonitorActionCount(currentMap.LoseMonitorCondition);
            }
            if (self.Battle.IsOrdeal)
              flag = false;
            if (flag)
            {
              self.GotoState_WaitSignal<SceneBattle.State_ConfirmContinue>();
              break;
            }
            self.GotoState_WaitSignal<SceneBattle.State_ExitQuest>();
            break;
          case BattleCore.QuestResult.Draw:
            if (!self.Battle.IsMultiVersus)
              break;
            self.OnQuestEnd();
            self.mBattleUI.OnVersusDraw();
            self.mExecDisconnected = true;
            self.SendIgnoreMyDisconnect();
            if (!self.mQuestResultSent)
              self.SubmitResult();
            self.GotoState_WaitSignal<SceneBattle.State_Result>();
            break;
          default:
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI_MultiPlay, (UnityEngine.Object) null))
              self.mBattleUI_MultiPlay.OnQuestRetreat();
            GameUtility.FadeOut(2f);
            self.GotoState<SceneBattle.State_ExitQuest>();
            break;
        }
      }
    }

    private class State_Result : State<SceneBattle>
    {
      private bool mIsResDestroy;
      private LoadRequest mResLoadReq;
      private float mResPassedTime;
      private bool mOnResult;

      public override void Begin(SceneBattle self)
      {
        BattleCore.Record questRecord = self.mBattle.GetQuestRecord();
        if (self.IsPlayingPreCalcResultQuest)
        {
          this.mOnResult = true;
          MonoSingleton<MySound>.Instance.PlayBGM("BGM_0006");
          if (self.IsPlayingGvGQuest)
          {
            GlobalVars.GvGBattleReplay.Set(false);
            self.mBattleUI.OnResult_GvG();
          }
          else
            self.mBattleUI.OnResult_Arena();
        }
        else
        {
          bool flag1 = false;
          bool flag2 = false;
          if (self.Battle.IsMultiPlay)
          {
            List<MultiFuid> multiFuids = MonoSingleton<GameManager>.Instance.Player.MultiFuids;
            MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
            List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
            if (myPlayersStarted != null && MonoSingleton<GameManager>.Instance.Player != null)
            {
              for (int index = 0; index < myPlayersStarted.Count; ++index)
              {
                JSON_MyPhotonPlayerParam startedPlayer = myPlayersStarted[index];
                if (startedPlayer != null && startedPlayer.playerIndex != instance.MyPlayerIndex && !string.IsNullOrEmpty(startedPlayer.FUID))
                {
                  MultiFuid multiFuid = multiFuids?.Find((Predicate<MultiFuid>) (f => f.fuid != null && f.fuid.Equals(startedPlayer.FUID)));
                  if (multiFuid != null && multiFuid.status.Equals("follower"))
                  {
                    flag1 = true;
                    break;
                  }
                }
              }
              flag2 = instance.IsMultiVersus;
            }
          }
          if (questRecord.IsZero && !flag1 && self.CurrentQuest.type != QuestTypes.Tower && (self.CurrentQuest.type != QuestTypes.Raid || MonoSingleton<GameManager>.Instance.RaidBtlEndParam.is_timeover != 0) && !self.CurrentQuest.IsGuildRaid && !self.CurrentQuest.IsWorldRaid && (self.CurrentQuest.type != QuestTypes.GenesisBoss || questRecord.mGenesisBossResultRewards == null) && (self.CurrentQuest.type != QuestTypes.AdvanceBoss || questRecord.mAdvanceBossResultRewards == null) && !flag2 && !self.Battle.IsMultiTower && !MonoSingleton<GameManager>.Instance.IsVSCpuBattle)
          {
            self.ExitRequest = SceneBattle.ExitRequests.End;
          }
          else
          {
            if (self.IsOrdealQuest && !string.IsNullOrEmpty(self.FirstClearItemId))
            {
              GameManager instance = MonoSingleton<GameManager>.Instance;
              string firstClearItemId = self.FirstClearItemId;
              ItemParam itemParam = instance.GetItemParam(firstClearItemId);
              if (itemParam != null && itemParam.type == EItemType.Unit)
              {
                UnitParam unitParam = instance.GetUnitParam(firstClearItemId);
                if (unitParam != null)
                  DownloadUtility.DownloadUnit(unitParam);
              }
            }
            for (int index = 0; index < questRecord.items.Count; ++index)
            {
              BattleCore.DropItemParam dropItemParam = questRecord.items[index];
              if (dropItemParam.IsConceptCard && !string.IsNullOrEmpty(dropItemParam.conceptCardParam.first_get_unit))
                DownloadUtility.DownloadConceptCard(dropItemParam.conceptCardParam);
            }
            if (!AssetDownloader.isDone)
              AssetDownloader.StartDownload(false);
            string resultBgResourcePath = GameSettings.Instance.BattleResultBg_ResourcePath;
            if (!string.IsNullOrEmpty(resultBgResourcePath))
            {
              this.mResLoadReq = AssetManager.LoadAsync<GameObject>(resultBgResourcePath);
              if (this.mResLoadReq != null)
                return;
              this.mIsResDestroy = true;
            }
            else
              this.mIsResDestroy = true;
          }
        }
      }

      private void StartResult()
      {
        if (this.mOnResult)
          return;
        this.mOnResult = true;
        MonoSingleton<MySound>.Instance.PlayBGM("BGM_0006");
        if (this.self.Battle.IsMultiPlay)
        {
          if (this.self.Battle.IsMultiVersus)
          {
            if (this.self.Battle.QuestType == QuestTypes.RankMatch)
              this.self.mBattleUI.OnResult_RankMatch();
            else
              this.self.mBattleUI.OnResult_Versus();
          }
          else if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.TOWER)
            this.self.mBattleUI.OnResult_MultiTower();
          else
            this.self.mBattleUI.OnResult_MP();
        }
        else if (this.self.Battle.QuestType == QuestTypes.Tower)
          this.self.mBattleUI.OnResult_Tower();
        else if (this.self.Battle.QuestType == QuestTypes.VersusFree || this.self.Battle.QuestType == QuestTypes.VersusRank)
          this.self.mBattleUI.OnResult_Versus();
        else if (this.self.Battle.QuestType == QuestTypes.Raid)
          this.self.mBattleUI.OnResult_Raid();
        else if (this.self.Battle.QuestType == QuestTypes.GuildRaid)
          this.self.mBattleUI.OnResult_Raid();
        else if (this.self.Battle.QuestType == QuestTypes.GenesisBoss)
          this.self.mBattleUI.OnResult_GenesisBoss();
        else if (this.self.Battle.QuestType == QuestTypes.AdvanceBoss)
          this.self.mBattleUI.OnResult_AdvanceBoss();
        else if (this.self.Battle.IsWorldRaid)
          this.self.mBattleUI.OnResult_WorldRaid();
        else
          this.self.mBattleUI.OnResult();
      }

      public override void Update(SceneBattle self)
      {
        if (!self.mQuestResultSent || !AssetDownloader.isDone)
          return;
        if (!this.mIsResDestroy)
        {
          if (this.mResLoadReq != null)
          {
            if (!this.mResLoadReq.isDone)
              return;
            this.mIsResDestroy = true;
            if (UnityEngine.Object.op_Inequality(this.mResLoadReq.asset, (UnityEngine.Object) null))
            {
              self.GoResultBg = UnityEngine.Object.Instantiate(this.mResLoadReq.asset) as GameObject;
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.GoResultBg))
              {
                self.GoResultBg.transform.SetParent(((Component) self.mBattleUI).transform, false);
                if (GameSettings.Instance.BattleResultBg_UseBattleBG)
                {
                  ResultMask component = self.GoResultBg.GetComponent<ResultMask>();
                  if (UnityEngine.Object.op_Implicit((UnityEngine.Object) component) && UnityEngine.Object.op_Implicit((UnityEngine.Object) Camera.main))
                  {
                    RenderPipeline renderPipeline = GameUtility.RequireComponent<RenderPipeline>(((Component) Camera.main).gameObject);
                    if (UnityEngine.Object.op_Implicit((UnityEngine.Object) renderPipeline))
                      component.SetBg(renderPipeline.BackgroundImage as Texture2D);
                  }
                }
                this.mIsResDestroy = false;
              }
            }
            this.mResLoadReq = (LoadRequest) null;
            return;
          }
          this.mResPassedTime += Time.deltaTime;
          if ((double) this.mResPassedTime < (double) GameSettings.Instance.BattleResultBg_WaitTime)
            return;
          for (int index = self.mTacticsUnits.Count - 1; index >= 0; --index)
          {
            UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) self.mTacticsUnits[index]).gameObject);
            self.mTacticsUnits.RemoveAt(index);
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mTacticsSceneRoot, (UnityEngine.Object) null))
          {
            UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) self.mTacticsSceneRoot).gameObject);
            self.mTacticsSceneRoot = (TacticsSceneSettings) null;
          }
          self.DestroyUI(true);
          this.mIsResDestroy = true;
        }
        if (self.ExitRequest == SceneBattle.ExitRequests.End)
        {
          if (self.mBattle.GetQuestResult() == BattleCore.QuestResult.Win && !this.mOnResult && self.IsPlayingMultiQuest && self.mFirstContact > 0)
            self.mBattleUI.OnFirstContact();
          self.GotoState_WaitSignal<SceneBattle.State_ExitQuest>();
        }
        else if (self.ExitRequest == SceneBattle.ExitRequests.Restart)
          self.GotoState<SceneBattle.State_Restart_SelectSupport>();
        else
          this.StartResult();
      }
    }

    private class State_RestartQuest : State<SceneBattle>
    {
      private bool mRequested;

      public override void Begin(SceneBattle self) => GameUtility.FadeOut(1f);

      public override void Update(SceneBattle self)
      {
        if (GameUtility.IsScreenFading || this.mRequested)
          return;
        this.mRequested = true;
        GameUtility.RequestScene("Battle");
      }
    }

    private class State_ContinueQuest : State<SceneBattle>
    {
      private bool mRequested;
      private SceneBattle mScene;

      public override void Begin(SceneBattle self)
      {
        this.mScene = self;
        GameUtility.FadeOut(1f);
        BattleSpeedController.OnContinue();
      }

      public override void Update(SceneBattle self)
      {
        if (GameUtility.IsScreenFading || this.mRequested)
          return;
        BattleCore.Record questRecord = self.Battle.GetQuestRecord();
        if (Network.Mode == Network.EConnectMode.Online)
        {
          Network.RequestAPI((WebAPI) new ReqBtlComCont(self.Battle.BtlID, questRecord, new Network.ResponseCallback(this.OnSuccess), PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay, self.Battle.IsMultiTower));
        }
        else
        {
          BattleCore.Json_Battle response = new BattleCore.Json_Battle()
          {
            btlid = this.mScene.Battle.BtlID,
            btlinfo = new BattleCore.Json_BtlInfo()
          };
          response.btlinfo.seed = 0;
          this.ResBtlComCont(response);
        }
        this.mRequested = true;
      }

      private void OnSuccess(WWWResult www)
      {
        if (Network.IsError)
        {
          switch (Network.ErrCode)
          {
            case Network.EErrCode.ContinueCostShort:
              this.self.GotoState<SceneBattle.State_ExitQuest>();
              break;
            case Network.EErrCode.CantContinue:
              FlowNode_Network.Failed();
              break;
            default:
              FlowNode_Network.Retry();
              break;
          }
        }
        else
        {
          WebAPI.JSON_BodyResponse<BattleCore.Json_Battle> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<BattleCore.Json_Battle>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            FlowNode_Network.Retry();
          }
          else
          {
            Network.RemoveAPI();
            MyMetaps.TrackSpendCoin("ContinueQuest", (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCost);
            this.ResBtlComCont(jsonObject.body);
          }
        }
      }

      private void ResBtlComCont(BattleCore.Json_Battle response)
      {
        if (response == null)
          return;
        this.mScene.CloseBattleUI();
        List<Unit> player = this.mScene.Battle.Player;
        for (int index = 0; index < player.Count; ++index)
        {
          TacticsUnitController unitController = this.mScene.FindUnitController(player[index]);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          {
            if (player[index].IsJump)
              unitController.SetVisible(true);
            if (unitController.Unit.IsDead)
            {
              this.mScene.mTacticsUnits.Remove(unitController);
              GameUtility.DestroyGameObject(((Component) unitController).gameObject);
            }
          }
        }
        GameUtility.FadeIn(1f);
        this.mScene.mUnitStartCount = 0;
        foreach (Unit unit in this.mScene.Battle.ContinueStart(response.btlid, response.btlinfo.seed))
        {
          TacticsUnitController unitController = this.mScene.FindUnitController(unit);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          {
            this.mScene.mTacticsUnits.Remove(unitController);
            GameUtility.DestroyGameObject(((Component) unitController).gameObject);
          }
        }
        this.mScene.RefreshJumpSpots();
        for (int index = 0; index < player.Count; ++index)
        {
          TacticsUnitController unitController = this.mScene.FindUnitController(player[index]);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
            unitController.UpdateBadStatus();
        }
        UnitQueue.Instance.Refresh();
        this.self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_ExitQuest : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        if (!self.mQuestResultSent && !MonoSingleton<GameManager>.Instance.AudienceMode)
          self.SubmitResult();
        self.mPauseReqCount = 0;
        TimeManager.SetTimeScale(TimeManager.TimeScaleGroups.Game, 1f);
        self.StartCoroutine(this.ExitAsync());
      }

      [DebuggerHidden]
      private IEnumerator ExitAsync()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_ExitQuest.\u003CExitAsync\u003Ec__Iterator0()
        {
          \u0024this = this
        };
      }
    }

    private class State_Restart_SelectSupport : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.mOnRequestStateChange = new SceneBattle.StateTransitionRequest(this.OnStateChange);
        self.mBattleUI.OnSupportSelectStart();
      }

      public override void End(SceneBattle self)
      {
        self.mOnRequestStateChange = (SceneBattle.StateTransitionRequest) null;
      }

      private void OnStateChange(SceneBattle.StateTransitionTypes req)
      {
        if (req == SceneBattle.StateTransitionTypes.Forward)
        {
          SupportData selectedSupport = (SupportData) GlobalVars.SelectedSupport;
          GlobalVars.SelectedFriendID = selectedSupport == null ? (string) null : selectedSupport.FUID;
          this.self.GotoState<SceneBattle.State_RestartQuest>();
        }
        else
          this.self.GotoState<SceneBattle.State_ExitQuest>();
      }
    }

    private class State_ConfirmContinue : State<SceneBattle>
    {
      private void OnDecide(GameObject dialog)
      {
        if (MonoSingleton<GameManager>.Instance.Player.Coin >= (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCost)
        {
          MonoSingleton<GameManager>.Instance.Player.DEBUG_CONSUME_COIN((int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCost);
          if (this.self.CurrentQuest.IsAutoRepeatQuestFlag && MonoSingleton<GameManager>.Instance.Player.IsAutoRepeatQuestMeasuring)
            UIUtility.SystemMessage(LocalizedText.Get("sys.AUTO_REPEAT_QUEST_CAUTION_FAILED_QUEST_CONTINUE"), (UIUtility.DialogResultEvent) (go =>
            {
              if (MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress != null)
              {
                MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.Reset();
                this.self.mBattleUI.OnAutoBattleToggle();
                this.self.mBattleUI.OnBattleSpeedToggle();
              }
              this.self.GotoState<SceneBattle.State_ContinueQuest>();
            }));
          else
            this.self.GotoState<SceneBattle.State_ContinueQuest>();
        }
        else
          MonoSingleton<GameManager>.Instance.ConfirmBuyCoin(new GameManager.BuyCoinEvent(this.OnBuyCoinEnd), new GameManager.BuyCoinEvent(this.OnBuyCoinCancel));
      }

      private void OnBuyCoinEnd() => this.Confirm();

      private void OnBuyCoinCancel() => this.self.GotoState<SceneBattle.State_ExitQuest>();

      private void OnCancel(GameObject dialog)
      {
        this.self.GotoState<SceneBattle.State_ExitQuest>();
      }

      public override void Begin(SceneBattle self) => this.Confirm();

      private void Confirm()
      {
        if (ContinueWindow.Create(this.self.mContinueWindowRes, new ContinueWindow.ResultEvent(this.OnDecide), new ContinueWindow.ResultEvent(this.OnCancel)))
          return;
        this.OnCancel((GameObject) null);
      }
    }

    private struct PosRot
    {
      public Vector3 Position;
      public Quaternion Rotation;

      public void Apply(Transform target)
      {
        target.position = this.Position;
        target.rotation = this.Rotation;
      }

      public void Apply(Transform target, Vector3 deltaPosition)
      {
        target.position = Vector3.op_Addition(this.Position, deltaPosition);
        target.rotation = this.Rotation;
      }

      public void AddPosition(Component component)
      {
        ref SceneBattle.PosRot local = ref this;
        local.Position = Vector3.op_Addition(local.Position, component.transform.position);
      }
    }

    private class State_AutoHeal : State<SceneBattle>
    {
      private GameObject mHealEffect;
      private TacticsUnitController mController;
      private LogAutoHeal mLog;
      private SceneBattle.State_AutoHeal.State mState;
      private int mHealdHp;
      private int mHealdMp;

      public override void Begin(SceneBattle self)
      {
        this.mLog = self.Battle.Logs.Peek as LogAutoHeal;
        this.mController = self.FindUnitController(this.mLog.self);
        this.mHealdHp = (int) this.mLog.self.CurrentStatus.param.hp;
        this.mHealdMp = (int) this.mLog.self.CurrentStatus.param.mp;
        this.mLog.self.CurrentStatus.param.hp = (OInt) this.mLog.beforeHp;
        if (this.mLog.type == LogAutoHeal.HealType.Hp)
          this.mController.ResetHPGauge();
        else
          this.mController.ShowHPGauge(false);
        this.mState = SceneBattle.State_AutoHeal.State.Start;
        self.RemoveLog();
      }

      public override void End(SceneBattle self)
      {
        this.mLog.self.CurrentStatus.param.hp = (OInt) this.mHealdHp;
        this.mLog.self.CurrentStatus.param.mp = (OShort) this.mHealdMp;
        this.mController.ResetHPGauge();
        this.mController.ShowHPGauge(false);
        this.mController.ShowOwnerIndexUI(false);
        base.End(self);
      }

      public override void Update(SceneBattle self)
      {
        if (this.mState == SceneBattle.State_AutoHeal.State.Start)
        {
          this.mState = SceneBattle.State_AutoHeal.State.Wait;
          self.StartCoroutine(this.Wait(0.1f));
        }
        else
        {
          if (this.mState == SceneBattle.State_AutoHeal.State.Wait)
            return;
          if (this.mState == SceneBattle.State_AutoHeal.State.EffectStart)
            this.StartEffect();
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mHealEffect, (UnityEngine.Object) null) || this.mController.IsHPGaugeChanging)
            return;
          if (this.mLog.type == LogAutoHeal.HealType.Jewel)
            UnitQueue.Instance.Refresh(this.mLog.self);
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
      }

      [DebuggerHidden]
      private IEnumerator Wait(float wait)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_AutoHeal.\u003CWait\u003Ec__Iterator0()
        {
          wait = wait,
          \u0024this = this
        };
      }

      private void StartEffect()
      {
        GameObject gameObject = this.mLog.type != LogAutoHeal.HealType.Hp ? this.self.mMapAddGemEffectTemplate : this.self.mAutoHealEffectTemplate;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        {
          this.mHealEffect = UnityEngine.Object.Instantiate<GameObject>(gameObject, ((Component) this.mController).transform.position, Quaternion.identity);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mHealEffect, (UnityEngine.Object) null))
          {
            this.mHealEffect.transform.SetParent(((Component) this.mController).transform);
            this.mHealEffect.RequireComponent<OneShotParticle>();
          }
        }
        SceneBattle.Instance.PopupHpHealNumber(this.mController, this.mLog.value);
        this.mController.ReflectDispModel();
        if (this.mLog.type != LogAutoHeal.HealType.Jewel)
          this.mController.UpdateHPRelative(this.mLog.value);
        this.mState = SceneBattle.State_AutoHeal.State.Effect;
      }

      private enum State
      {
        Start,
        Wait,
        End,
        EffectStart,
        Effect,
      }
    }

    private class State_PrepareCast : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        LogCast peek = self.Battle.Logs.Peek as LogCast;
        switch (peek.type)
        {
          case ECastTypes.Chant:
            this.EnterChant(peek);
            self.RemoveLog();
            self.GotoState<SceneBattle.State_WaitForLog>();
            break;
          case ECastTypes.Charge:
            this.EnterChant(peek);
            self.RemoveLog();
            self.GotoState<SceneBattle.State_WaitForLog>();
            break;
          case ECastTypes.Jump:
            self.GotoState<SceneBattle.State_JumpCastStart>();
            break;
        }
      }

      private void EnterChant(LogCast Log)
      {
        TacticsUnitController unitController = this.self.FindUnitController(Log.self);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          unitController.ChargeIcon.Open();
        if (Log.self == null || Log.self.CastSkill == null || Log.self.CastSkill.TeleportType == eTeleportType.None)
          return;
        this.self.RefreshJumpSpots();
      }
    }

    private class State_JumpCastStart : State<SceneBattle>
    {
      private TacticsUnitController mCasterController;
      private float WaitTime = 1f;
      private float CountTime;
      private bool mDirection = true;

      public override void Begin(SceneBattle self)
      {
        LogCast peek = self.Battle.Logs.Peek as LogCast;
        this.mCasterController = self.FindUnitController(peek.self);
        self.RemoveLog();
        this.mDirection = self.Battle.IsSkillDirection;
        if (!this.mDirection)
          return;
        self.mUpdateCameraPosition = true;
        self.InterpCameraTarget((Component) this.mCasterController);
        float skillCameraDistance = GameSettings.Instance.GameCamera_SkillCameraDistance;
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mCasterController) && this.mCasterController.Unit != null && !this.mCasterController.Unit.IsNormalSize)
          skillCameraDistance += GameSettings.Instance.GameCamera_BigUnitAddDistance;
        self.InterpCameraDistance(skillCameraDistance);
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mDirection)
        {
          this.mCasterController.SetVisible(false);
          this.mCasterController.ChargeIcon.Open();
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
        else
        {
          if ((double) this.WaitTime > (double) this.CountTime)
          {
            this.CountTime += Time.deltaTime;
            if ((double) this.WaitTime > (double) this.CountTime)
              return;
            this.mCasterController.CastJump();
            this.mCasterController.ChargeIcon.Open();
          }
          if (!this.mCasterController.IsCastJumpStartComplete())
            return;
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
      }

      public override void End(SceneBattle self)
      {
        self.RefreshJumpSpots();
        self.mUpdateCameraPosition = false;
      }
    }

    private class State_JumpFall : State<SceneBattle>
    {
      private const float WAIT_TIME = 0.5f;
      private const float DELAY_TIME = 0.1f;
      private LogFall mLog;
      private bool mDirection = true;
      private SceneBattle.State_JumpFall.eDirectionMode mDirectionMode;
      private List<SceneBattle.State_JumpFall.Caster> mCasterLists = new List<SceneBattle.State_JumpFall.Caster>();

      public override void Begin(SceneBattle self)
      {
        this.mLog = self.Battle.Logs.Peek as LogFall;
        if (this.mLog == null || this.mLog.mLists.Count == 0)
        {
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
        else
        {
          for (int index = 0; index < this.mLog.mLists.Count; ++index)
          {
            LogFall.Param mList = this.mLog.mLists[index];
            TacticsUnitController unitController = self.FindUnitController(mList.mSelf);
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
            {
              if (mList.mLanding != null)
              {
                Vector3 pos = self.CalcGridCenter(mList.mLanding);
                unitController.SetStartPos(pos);
                ((Component) unitController).transform.position = pos;
              }
              this.mCasterLists.Add(new SceneBattle.State_JumpFall.Caster(unitController));
            }
          }
          self.RemoveLog();
          this.mDirection = self.Battle.IsSkillDirection;
          if (!this.mDirection)
            return;
          Vector3 center = Vector3.zero;
          float distance = GameSettings.Instance.GameCamera_SkillCameraDistance;
          List<Vector3> vector3List = new List<Vector3>();
          for (int index = 0; index < this.mCasterLists.Count; ++index)
            vector3List.Add(this.mCasterLists[index].mController.CenterPosition);
          self.GetCameraTargetView(out center, out distance, vector3List.ToArray());
          self.mUpdateCameraPosition = true;
          self.InterpCameraTarget(center);
          self.InterpCameraDistance(distance);
          this.mDirectionMode = SceneBattle.State_JumpFall.eDirectionMode.FALL;
        }
      }

      public override void Update(SceneBattle self)
      {
        if (this.mDirection)
        {
          switch (this.mDirectionMode)
          {
            case SceneBattle.State_JumpFall.eDirectionMode.FALL:
              bool flag = true;
              for (int index = 0; index < this.mCasterLists.Count; ++index)
              {
                float num = (float) (0.5 + 0.10000000149011612 * (double) index);
                SceneBattle.State_JumpFall.Caster mCasterList = this.mCasterLists[index];
                TacticsUnitController mController = mCasterList.mController;
                if ((double) mCasterList.mPassedTime < (double) num)
                {
                  mCasterList.mPassedTime += Time.deltaTime;
                  if ((double) mCasterList.mPassedTime >= (double) num)
                  {
                    mController.CastJumpFall(this.mLog.mIsPlayDamageMotion);
                    mController.ChargeIcon.Close();
                  }
                  flag = false;
                }
                else if (!mController.IsFinishedCastJumpFall())
                  flag = false;
              }
              if (!flag)
                break;
              self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
              this.mDirectionMode = SceneBattle.State_JumpFall.eDirectionMode.CAMERA_WAIT;
              break;
            case SceneBattle.State_JumpFall.eDirectionMode.CAMERA_WAIT:
              this.mDirectionMode = SceneBattle.State_JumpFall.eDirectionMode.EXIT;
              break;
            case SceneBattle.State_JumpFall.eDirectionMode.EXIT:
              self.GotoState<SceneBattle.State_WaitForLog>();
              break;
          }
        }
        else
        {
          for (int index = 0; index < this.mCasterLists.Count; ++index)
          {
            TacticsUnitController mController = this.mCasterLists[index].mController;
            mController.SetVisible(true);
            mController.ChargeIcon.Close();
          }
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
      }

      public override void End(SceneBattle self)
      {
        self.RefreshJumpSpots();
        self.mUpdateCameraPosition = false;
      }

      private enum eDirectionMode
      {
        FALL,
        CAMERA_WAIT,
        EXIT,
      }

      private class Caster
      {
        public TacticsUnitController mController;
        public float mPassedTime;

        public Caster(TacticsUnitController controller)
        {
          this.mController = controller;
          this.mPassedTime = 0.0f;
        }
      }
    }

    private enum eCsImage
    {
      U2_MAIN,
      U2_SUB,
      UE_MAIN,
      UE_SUB,
      MAX,
    }

    private class State_PrepareSkill : State<SceneBattle>
    {
      private bool mLoadSplash;
      private float mWaitCount;
      private bool mIsLoadTransformUnit;
      private bool mIsLoadBreakObjUnit;

      public override void Begin(SceneBattle self)
      {
        LogSkill peek = self.mBattle.Logs.Peek as LogSkill;
        SkillData skill = peek.skill;
        if (skill == null)
        {
          DebugUtility.LogError("SkillParam が存在しません。");
        }
        else
        {
          this.mLoadSplash = !peek.is_append;
          self.LoadShieldEffects();
          Unit self1 = peek.self;
          TacticsUnitController unitController = self.FindUnitController(self1);
          this.mIsLoadTransformUnit = skill.IsTransformSkill();
          if (this.mIsLoadTransformUnit)
          {
            if (peek.targets != null && peek.targets.Count != 0)
              self.StartCoroutine(SceneBattle.State_PrepareSkill.loadTransformUnit(self, peek.targets[0].target, unitController, (Action) (() => this.mIsLoadTransformUnit = false)));
            else
              this.mIsLoadTransformUnit = false;
          }
          this.mIsLoadBreakObjUnit = skill.IsSetBreakObjSkill();
          if (this.mIsLoadBreakObjUnit)
          {
            if (peek.targets != null && peek.targets.Count != 0)
              self.StartCoroutine(SceneBattle.State_PrepareSkill.loadBreakObjUnit(self, peek.targets[0].target, (Action) (() => this.mIsLoadBreakObjUnit = false)));
            else
              this.mIsLoadBreakObjUnit = false;
          }
          if (skill.SkillParam.absorb_damage_rate > 0)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mDrainMpEffectTemplate, (UnityEngine.Object) null) && skill.SkillParam.IsJewelAttack())
              unitController.SetDrainEffect(self.mDrainMpEffectTemplate);
            else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mDrainHpEffectTemplate, (UnityEngine.Object) null))
              unitController.SetDrainEffect(self.mDrainHpEffectTemplate);
          }
          if ((self.Battle.IsUnitAuto(self1) || self.Battle.EntryBattleMultiPlayTimeUp) && !skill.IsTransformSkill())
          {
            IntVector2 intVector2 = self.CalcCoord(unitController.CenterPosition);
            GridMap<bool> selectGridMap = self.Battle.CreateSelectGridMap(self1, intVector2.x, intVector2.y, skill);
            Color32 src1 = Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea);
            GridMap<Color32> grid1 = new GridMap<Color32>(selectGridMap.w, selectGridMap.h);
            for (int x = 0; x < selectGridMap.w; ++x)
            {
              for (int y = 0; y < selectGridMap.h; ++y)
              {
                if (selectGridMap.get(x, y))
                  grid1.set(x, y, src1);
              }
            }
            GridMap<bool> scopeGridMap = self.mBattle.CreateScopeGridMap(self1, intVector2.x, intVector2.y, peek.pos.x, peek.pos.y, skill);
            Color32 src2 = Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea2);
            GridMap<Color32> grid2 = new GridMap<Color32>(scopeGridMap.w, scopeGridMap.h);
            for (int x = 0; x < scopeGridMap.w; ++x)
            {
              for (int y = 0; y < scopeGridMap.h; ++y)
              {
                if (scopeGridMap.get(x, y))
                  grid2.set(x, y, src2);
              }
            }
            self.mTacticsSceneRoot.ShowGridLayer(1, grid1, true);
            self.mTacticsSceneRoot.ShowGridLayer(2, grid2, false);
            this.mWaitCount = 1f;
          }
          self.DeleteOnGimmickIcon();
        }
      }

      [DebuggerHidden]
      private IEnumerator PrepareSkill(Unit unit, SkillData skill)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_PrepareSkill.\u003CPrepareSkill\u003Ec__Iterator0()
        {
          skill = skill,
          unit = unit,
          \u0024this = this
        };
      }

      [DebuggerHidden]
      private IEnumerator cutinVoice(Unit unit, SkillData skill)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_PrepareSkill.\u003CcutinVoice\u003Ec__Iterator1()
        {
          unit = unit,
          skill = skill,
          \u0024this = this
        };
      }

      public override void Update(SceneBattle self)
      {
        if (this.mIsLoadTransformUnit || this.mIsLoadBreakObjUnit)
          return;
        this.mWaitCount -= GameSettings.Instance.AiUnit_SkillWait * Time.deltaTime;
        if (0.0 < (double) this.mWaitCount)
          return;
        LogSkill peek = self.mBattle.Logs.Peek as LogSkill;
        SkillData skill = peek.skill;
        self.mCollaboMainUnit = peek.self;
        self.mCollaboTargetTuc = (TacticsUnitController) null;
        self.mIsInstigatorSubUnit = false;
        if ((bool) peek.skill.IsCollabo)
        {
          Unit unitUseCollaboSkill = peek.self.GetUnitUseCollaboSkill(peek.skill, peek.self.x, peek.self.y);
          if (unitUseCollaboSkill != null)
          {
            self.mCollaboTargetTuc = self.FindUnitController(unitUseCollaboSkill);
            if (unitUseCollaboSkill.UnitParam.iname == skill.SkillParam.CollaboMainId)
              self.mIsInstigatorSubUnit = true;
          }
        }
        self.StartCoroutine(this.PrepareSkill(peek.self, skill));
        Unit self1 = peek.self;
        if (self.Battle.IsUnitAuto(self1) || self.Battle.EntryBattleMultiPlayTimeUp)
        {
          self.mTacticsSceneRoot.HideGridLayer(1);
          self.mTacticsSceneRoot.HideGridLayer(2);
        }
        if (!self.mSkillMotion.IsBattleScene)
        {
          self.GotoState<SceneBattle.State_Map_PrepareSkill>();
        }
        else
        {
          self.mBattleUI.OnBattleStart();
          self.mBattleUI.OnCommandSelect();
          self.GotoState_WaitSignal<SceneBattle.State_Battle_PrepareSkill>();
        }
      }

      [DebuggerHidden]
      public static IEnumerator loadTransformUnit(
        SceneBattle self,
        Unit unit,
        TacticsUnitController bef_tuc = null,
        Action callback = null)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_PrepareSkill.\u003CloadTransformUnit\u003Ec__Iterator2()
        {
          unit = unit,
          self = self,
          bef_tuc = bef_tuc,
          callback = callback
        };
      }

      [DebuggerHidden]
      public static IEnumerator loadBreakObjUnit(SceneBattle self, Unit unit, Action callback = null)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_PrepareSkill.\u003CloadBreakObjUnit\u003Ec__Iterator3()
        {
          unit = unit,
          self = self,
          callback = callback
        };
      }
    }

    private class State_Battle_PrepareSkill : State<SceneBattle>
    {
      private SkillParam mSkill;
      private TacticsUnitController mInstigator;
      private List<TacticsUnitController> mTargets = new List<TacticsUnitController>();

      public override void Begin(SceneBattle self)
      {
        LogSkill peek = self.mBattle.Logs.Peek as LogSkill;
        this.mInstigator = self.FindUnitController(peek.self);
        self.mUnitsInBattle.Clear();
        this.mSkill = peek.skill.SkillParam;
        bool flag = peek.skill.IsDamagedSkill();
        this.mInstigator.AutoUpdateRotation = false;
        this.mInstigator.LoadSkillSequence(peek.skill.SkillParam, self.mSkillMotion, false, true, (bool) peek.skill.IsCollabo, self.mIsInstigatorSubUnit);
        if (!string.IsNullOrEmpty(self.mSkillMotion.EffectId))
          this.mInstigator.LoadSkillEffect(self.mSkillMotion.EffectId, self.mIsInstigatorSubUnit);
        self.mUnitsInBattle.Add(this.mInstigator);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
        {
          self.mCollaboTargetTuc.AutoUpdateRotation = false;
          self.mCollaboTargetTuc.LoadSkillSequence(peek.skill.SkillParam, self.mSkillMotion, false, true, (bool) peek.skill.IsCollabo, !self.mIsInstigatorSubUnit);
          if (!string.IsNullOrEmpty(self.mSkillMotion.EffectId))
            self.mCollaboTargetTuc.LoadSkillEffect(self.mSkillMotion.EffectId, !self.mIsInstigatorSubUnit);
          self.mUnitsInBattle.Add(self.mCollaboTargetTuc);
        }
        if (0 < this.mSkill.hp_cost)
        {
          this.mInstigator.SetHpCostSkill(peek.skill.GetHpCost(peek.self));
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
            self.mCollaboTargetTuc.SetHpCostSkill(peek.skill.GetHpCost(peek.self));
        }
        for (int index = 0; index < peek.targets.Count; ++index)
        {
          LogSkill.Target target = peek.targets[index];
          TacticsUnitController unitController = self.FindUnitController(target.target);
          this.mTargets.Add(unitController);
          if (this.mSkill.IsTransformSkill())
            unitController.LoadTransformAnimation(this.mInstigator.GetSkillSequence());
          if (!unitController.Unit.IsNormalSize)
            unitController.LoadBigUnitBsAnimation();
          if (flag)
          {
            if (peek.targets[index].IsAvoid())
            {
              unitController.LoadDodgeAnimation();
            }
            else
            {
              if (peek.targets[index].IsCombo() && peek.targets[index].IsAvoidJustOne())
                unitController.LoadDodgeAnimation();
              unitController.LoadDamageAnimations();
            }
          }
          unitController.AutoUpdateRotation = false;
          self.mUnitsInBattle.Add(unitController);
        }
        self.SetPrioritizedUnits(self.mUnitsInBattle);
        self.DisableUserInterface();
        self.ToggleRenkeiAura(false);
        GameUtility.FadeOut(0.2f);
      }

      public override void Update(SceneBattle self)
      {
        if (GameUtility.IsScreenFading)
          return;
        for (int index = self.mUnitsInBattle.Count - 1; index >= 0; --index)
        {
          if (self.mUnitsInBattle[index].IsLoading)
            return;
        }
        if (self.mLoadingShieldEffects)
          return;
        LogSkill peek = self.mBattle.Logs.Peek as LogSkill;
        self.mUpdateCameraPosition = false;
        self.SetScreenMirroring(this.mInstigator.IsSkillMirror());
        string sceneName = peek.skill.SkillParam.SceneName;
        if (this.mTargets.Count != 0 && !this.mTargets[0].Unit.IsNormalSize && !string.IsNullOrEmpty(peek.skill.SkillParam.SceneNameBigUnit))
          sceneName = peek.skill.SkillParam.SceneNameBigUnit;
        self.ToggleBattleScene(true, sceneName);
        self.EnableWeatherEffect(false);
        Vector3 vector3_1 = self.mBattleSceneRoot.PlayerStart2.position;
        Vector3 vector3_2 = self.mBattleSceneRoot.PlayerStart2.position;
        Vector3 vector3_3 = self.mBattleSceneRoot.PlayerStart1.position;
        if (this.mInstigator.IsSkillParentPosZero)
        {
          Vector3 zero;
          vector3_3 = zero = Vector3.zero;
          vector3_2 = zero;
          vector3_1 = zero;
        }
        if (self.mIsInstigatorSubUnit)
        {
          Vector3 vector3_4 = vector3_1;
          vector3_1 = vector3_2;
          vector3_2 = vector3_4;
        }
        ((Component) this.mInstigator).transform.position = vector3_1;
        ((Component) this.mInstigator).transform.rotation = Quaternion.LookRotation(Vector3.op_Subtraction(self.mBattleSceneRoot.PlayerStart1.position, self.mBattleSceneRoot.PlayerStart2.position));
        ((Component) this.mInstigator).transform.SetParent(((Component) self.mBattleSceneRoot).transform, false);
        this.mInstigator.EnableDispOffBadStatus(true);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
        {
          ((Component) self.mCollaboTargetTuc).transform.position = vector3_2;
          ((Component) self.mCollaboTargetTuc).transform.rotation = Quaternion.LookRotation(Vector3.op_Subtraction(self.mBattleSceneRoot.PlayerStart1.position, self.mBattleSceneRoot.PlayerStart2.position));
          ((Component) self.mCollaboTargetTuc).transform.SetParent(((Component) self.mBattleSceneRoot).transform, false);
          self.mCollaboTargetTuc.EnableDispOffBadStatus(true);
        }
        if (this.mTargets.Count == 1 && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTargets[0], (UnityEngine.Object) this.mInstigator))
        {
          TacticsUnitController mTarget = this.mTargets[0];
          if (peek.skill.IsTransformSkill())
          {
            ((Component) mTarget).transform.position = vector3_1;
            ((Component) mTarget).transform.rotation = Quaternion.LookRotation(Vector3.op_Subtraction(self.mBattleSceneRoot.PlayerStart1.position, self.mBattleSceneRoot.PlayerStart2.position));
            ((Component) mTarget).transform.SetParent(((Component) self.mBattleSceneRoot).transform, false);
          }
          else
          {
            ((Component) mTarget).transform.position = vector3_3;
            ((Component) mTarget).transform.rotation = Quaternion.LookRotation(Vector3.op_Subtraction(self.mBattleSceneRoot.PlayerStart2.position, self.mBattleSceneRoot.PlayerStart1.position));
            ((Component) mTarget).transform.SetParent(((Component) self.mBattleSceneRoot).transform, false);
          }
          mTarget.EnableDispOffBadStatus(true);
        }
        Transform transform = ((Component) Camera.main).transform;
        transform.position = Vector3.op_Addition(GameSettings.Instance.Quest.BattleCamera.position, self.mBattleSceneRoot.PlayerStart2.position);
        transform.rotation = GameSettings.Instance.Quest.BattleCamera.rotation;
        this.mInstigator.SetHitInfoSelf(peek.self_effect);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
          self.mCollaboTargetTuc.SetHitInfoSelf(new LogSkill.Target());
        for (int index = 0; index < peek.targets.Count; ++index)
        {
          TacticsUnitController unitController = self.FindUnitController(peek.targets[index].target);
          LogSkill.Target target = peek.targets[index];
          unitController.ShouldDodgeHits = target.IsAvoid();
          unitController.ShouldPerfectDodge = target.IsPerfectAvoid();
          unitController.SetHitInfo(peek.targets[index]);
        }
        self.ToggleJumpSpots(false);
        if (peek.skill.CastType == ECastTypes.Jump)
          this.mInstigator.SetLandingGrid(peek.landing);
        if (peek.skill.TeleportType != eTeleportType.None)
          this.mInstigator.SetTeleportGrid(peek.TeleportGrid);
        this.mInstigator.StartSkill(self.CalcGridCenter(peek.pos.x, peek.pos.y), Camera.main, this.mTargets.ToArray(), (Vector3[]) null, this.mSkill);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
          self.mCollaboTargetTuc.StartSkill(self.CalcGridCenter(self.mCollaboTargetTuc.Unit.x, self.mCollaboTargetTuc.Unit.y), Camera.main, this.mTargets.ToArray(), (Vector3[]) null, this.mSkill);
        GameUtility.FadeIn(0.2f);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillSplash, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mSkillSplash))
            self.mSkillSplash.Close();
          self.mSkillSplash = (SkillSplash) null;
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillSplashCollabo, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mSkillSplashCollabo))
            self.mSkillSplashCollabo.Close();
          self.mSkillSplashCollabo = (SkillSplashCollabo) null;
        }
        self.GotoState<SceneBattle.State_Battle_PlaySkill>();
      }
    }

    private class State_Battle_PlaySkill : State<SceneBattle>
    {
      public override void Update(SceneBattle self)
      {
        for (int index = 0; index < self.mUnitsInBattle.Count; ++index)
        {
          if (!self.mUnitsInBattle[index].isIdle)
            return;
        }
        GameUtility.FadeOut(0.25f);
        self.GotoState<SceneBattle.State_Battle_EndSkill>();
      }
    }

    private class State_Battle_EndSkill : State<SceneBattle>
    {
      private bool mIsBusy;
      private SkillData mSkillData;
      private LogSkill mLogSkill;

      public override void Begin(SceneBattle self)
      {
        this.mIsBusy = true;
        this.mLogSkill = self.mBattle.Logs.Peek as LogSkill;
        self.StartCoroutine(this.AsyncUpdate());
      }

      private void UpdateHPGauges()
      {
        LogSkill peek = this.self.mBattle.Logs.Peek as LogSkill;
        for (int index = 0; index < peek.targets.Count; ++index)
        {
          int delta = -peek.targets[index].GetTotalHpDamage() + peek.targets[index].GetTotalHpHeal();
          if (delta != 0)
          {
            TacticsUnitController unitController = this.self.FindUnitController(peek.targets[index].target);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
              unitController.UpdateHPRelative(delta);
          }
        }
      }

      [DebuggerHidden]
      private IEnumerator AsyncUpdate()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_Battle_EndSkill.\u003CAsyncUpdate\u003Ec__Iterator0()
        {
          \u0024this = this
        };
      }

      public override void Update(SceneBattle self)
      {
        if (this.mIsBusy)
          return;
        if (this.mSkillData != null && this.mSkillData.IsTransformSkill())
        {
          self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
          self.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>();
        }
        else
        {
          self.mIgnoreShieldEffect.Clear();
          if (this.mLogSkill != null)
          {
            TacticsUnitController unitController1 = self.FindUnitController(this.mLogSkill.self);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController1, (UnityEngine.Object) null))
              self.mIgnoreShieldEffect.Add(unitController1);
            foreach (LogSkill.Target target in this.mLogSkill.targets)
            {
              if (!target.isProcShield)
              {
                TacticsUnitController unitController2 = self.FindUnitController(target.target);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController2, (UnityEngine.Object) null))
                  self.mIgnoreShieldEffect.Add(unitController2);
              }
            }
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null))
            {
              TacticsUnitController unitController3 = self.FindUnitController(this.mLogSkill.self);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController3, (UnityEngine.Object) null) && this.mLogSkill.targets != null && this.mLogSkill.targets.Count != 0)
              {
                List<TacticsUnitController> TargetLists = new List<TacticsUnitController>();
                foreach (LogSkill.Target target in this.mLogSkill.targets)
                {
                  TacticsUnitController unitController4 = self.FindUnitController(target.target);
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController4, (UnityEngine.Object) null))
                    TargetLists.Add(unitController4);
                }
                if (TargetLists.Count != 0)
                {
                  self.mEventSequence = self.mEventScript.OnUseSkill(EventScript.SkillTiming.AFTER, unitController3, this.mLogSkill.skill, TargetLists, self.mIsFirstPlay);
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
                  {
                    self.GotoState_WaitSignal<SceneBattle.State_WaitEvent<SceneBattle.State_SpawnShieldEffects>>();
                    return;
                  }
                }
              }
            }
          }
          self.GotoState_WaitSignal<SceneBattle.State_SpawnShieldEffects>();
        }
      }
    }

    private class State_Map_PrepareSkill : State<SceneBattle>
    {
      private TacticsUnitController controller;
      private LogSkill log;

      public override void Begin(SceneBattle self)
      {
        this.log = self.mBattle.Logs.Peek as LogSkill;
        LogSkill log = this.log;
        SkillParam skillParam = log.skill.SkillParam;
        this.controller = self.FindUnitController(log.self);
        self.mUnitsInBattle.Clear();
        self.mUnitsInBattle.Add(this.controller);
        this.controller.AutoUpdateRotation = false;
        this.controller.LoadSkillSequence(skillParam, self.mSkillMotion, false, false, (bool) log.skill.IsCollabo, self.mIsInstigatorSubUnit);
        if (!string.IsNullOrEmpty(self.mSkillMotion.EffectId))
          this.controller.LoadSkillEffect(self.mSkillMotion.EffectId);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
        {
          self.mCollaboTargetTuc.LoadSkillSequence(skillParam, self.mSkillMotion, false, false, (bool) log.skill.IsCollabo, !self.mIsInstigatorSubUnit);
          if (!string.IsNullOrEmpty(self.mSkillMotion.EffectId))
            self.mCollaboTargetTuc.LoadSkillEffect(self.mSkillMotion.EffectId, !self.mIsInstigatorSubUnit);
          self.mUnitsInBattle.Add(self.mCollaboTargetTuc);
        }
        if (0 < skillParam.hp_cost)
        {
          this.controller.SetHpCostSkill(log.skill.GetHpCost(log.self));
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
            self.mCollaboTargetTuc.SetHpCostSkill(log.skill.GetHpCost(log.self));
        }
        for (int index = 0; index < log.targets.Count; ++index)
        {
          TacticsUnitController unitController = self.FindUnitController(log.targets[index].target);
          if (skillParam.IsTransformSkill())
            unitController.LoadTransformAnimation(this.controller.GetSkillSequence());
          if (log.targets[index].IsAvoid())
          {
            unitController.LoadDodgeAnimation();
          }
          else
          {
            if (log.targets[index].IsCombo() && log.targets[index].IsAvoidJustOne())
              unitController.LoadDodgeAnimation();
            unitController.LoadDamageAnimations();
          }
          self.mUnitsInBattle.Add(unitController);
        }
      }

      public override void Update(SceneBattle self)
      {
        LogSkill log = this.log;
        SkillParam skillParam = log.skill.SkillParam;
        if (!string.IsNullOrEmpty(self.mSkillMotion.EffectId) && !this.controller.IsFinishedLoadSkillEffect())
          return;
        EUnitDirection AxisDirection = self.mSkillDirectionByKouka;
        if (self.Battle.IsUnitAuto(log.self) || self.Battle.IsMultiPlay && self.Battle.CurrentUnit.OwnerPlayerIndex != self.Battle.MyPlayerIndex || log.skill.IsCastSkill() || log.skill.IsNeedResetDirection)
        {
          IntVector2 intVector2 = self.CalcCoord(this.controller.CenterPosition);
          GridMap<bool> scopeGridMap = self.mBattle.CreateScopeGridMap(this.controller.Unit, intVector2.x, intVector2.y, log.pos.x, log.pos.y, log.skill);
          scopeGridMap?.set(log.pos.x, log.pos.y, true);
          AxisDirection = self.GetSkillDirectionByTargetArea(this.controller.Unit, intVector2.x, intVector2.y, scopeGridMap);
        }
        this.controller.SkillTurn(log, AxisDirection);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
          self.mCollaboTargetTuc.SkillTurn(log, AxisDirection);
        self.SetPrioritizedUnits(self.mUnitsInBattle);
        bool flag1 = this.log.IsRenkei();
        bool flag2 = self.Battle.ProtectUnits.Count > 0 && !skillParam.IsReactionSkill();
        self.mAllowCameraRotation = false;
        self.mAllowCameraTranslation = false;
        if (flag2)
          self.GotoState<SceneBattle.State_Map_LoadSkill_Protect>();
        else if (flag1)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.controller, (UnityEngine.Object) null))
            this.controller.LoadGenkidamaAnimation(true);
          self.GotoState<SceneBattle.State_Map_LoadSkill_Renkei1>();
        }
        else
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.controller, (UnityEngine.Object) null))
          {
            self.SetCameraOffset(((Component) GameSettings.Instance.Quest.UnitCamera).transform);
            self.InterpCameraTarget((Component) this.controller);
            bool flag3 = false;
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.controller) && this.controller.Unit != null && !this.controller.Unit.IsNormalSize)
              flag3 = true;
            else if (log.targets != null)
            {
              for (int index = 0; index < log.targets.Count; ++index)
              {
                TacticsUnitController unitController = self.FindUnitController(log.targets[index].target);
                if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController) && unitController.Unit != null && !unitController.Unit.IsNormalSize)
                {
                  flag3 = true;
                  break;
                }
              }
            }
            float skillCameraDistance = GameSettings.Instance.GameCamera_SkillCameraDistance;
            if (flag3)
              skillCameraDistance += GameSettings.Instance.GameCamera_BigUnitAddDistance;
            self.InterpCameraDistance(skillCameraDistance);
          }
          self.GotoState<SceneBattle.State_Map_LoadSkill>();
        }
      }
    }

    private class State_Map_LoadSkill : State<SceneBattle>
    {
      private float mWaitTime;

      public override void Begin(SceneBattle self) => this.mWaitTime = 0.75f;

      public override void Update(SceneBattle self)
      {
        if ((double) this.mWaitTime > 0.0)
          this.mWaitTime -= Time.deltaTime;
        if (self.mDesiredCameraTargetSet || ObjectAnimator.Get((Component) Camera.main).isMoving)
          return;
        for (int index = self.mUnitsInBattle.Count - 1; index >= 0; --index)
        {
          if (self.mUnitsInBattle[index].IsLoading || !self.mUnitsInBattle[index].isIdle)
            return;
        }
        if (self.mLoadingShieldEffects || (double) this.mWaitTime > 0.0)
          return;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillSplash, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mSkillSplash))
            self.mSkillSplash.Close();
          if (self.mWaitSkillSplashClose)
            return;
          self.mSkillSplash = (SkillSplash) null;
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillSplashCollabo, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mSkillSplashCollabo))
            self.mSkillSplashCollabo.Close();
          if (self.mWaitSkillSplashClose)
            return;
          self.mSkillSplashCollabo = (SkillSplashCollabo) null;
        }
        LogSkill peek = self.mBattle.Logs.Peek as LogSkill;
        TacticsUnitController unitController = self.FindUnitController(peek.self);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) && unitController.HasPreSkillAnimation)
        {
          self.mBattleUI.OnCommandSelect();
          self.GotoState<SceneBattle.State_Map_PlayPreSkillAnim>();
        }
        else
          self.GotoState<SceneBattle.State_Map_PlayPreDodge>();
      }
    }

    private class State_Map_PlayPreSkillAnim : State<SceneBattle>
    {
      private TacticsUnitController mInstigator;
      private bool mFadeIn;
      private TacticsUnitController mTransformTuc;
      private Quaternion mInstigatorQuat;
      private Quaternion mCollaboQuat;
      private Quaternion mTransformQuat;

      public override void Begin(SceneBattle self)
      {
        LogSkill peek = self.mBattle.Logs.Peek as LogSkill;
        this.mInstigator = self.FindUnitController(peek.self);
        self.SetScreenMirroring(this.mInstigator.IsSkillMirror());
        self.ToggleBattleScene(true, peek.skill.SkillParam.SceneName);
        self.EnableWeatherEffect(false);
        Vector3 vector3_1 = self.mBattleSceneRoot.PlayerStart2.position;
        Vector3 vector3_2 = self.mBattleSceneRoot.PlayerStart2.position;
        if (this.mInstigator.IsPreSkillParentPosZero)
          vector3_1 = vector3_2 = Vector3.zero;
        if (self.mIsInstigatorSubUnit)
        {
          Vector3 vector3_3 = vector3_1;
          vector3_1 = vector3_2;
          vector3_2 = vector3_3;
        }
        Transform transform1 = ((Component) this.mInstigator).transform;
        this.mInstigatorQuat = transform1.rotation;
        transform1.position = vector3_1;
        transform1.rotation = Quaternion.LookRotation(Vector3.op_Subtraction(self.mBattleSceneRoot.PlayerStart1.position, self.mBattleSceneRoot.PlayerStart2.position));
        transform1.SetParent(((Component) self.mBattleSceneRoot).transform, false);
        this.mInstigator.EnableDispOffBadStatus(true);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
        {
          this.mCollaboQuat = ((Component) self.mCollaboTargetTuc).transform.rotation;
          ((Component) self.mCollaboTargetTuc).transform.position = vector3_2;
          ((Component) self.mCollaboTargetTuc).transform.rotation = Quaternion.LookRotation(Vector3.op_Subtraction(self.mBattleSceneRoot.PlayerStart1.position, self.mBattleSceneRoot.PlayerStart2.position));
          ((Component) self.mCollaboTargetTuc).transform.SetParent(((Component) self.mBattleSceneRoot).transform, false);
          self.mCollaboTargetTuc.EnableDispOffBadStatus(true);
        }
        if (peek.skill.IsTransformSkill() && peek.targets.Count != 0)
        {
          this.mTransformTuc = self.FindUnitController(peek.targets[0].target);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTransformTuc))
          {
            this.mTransformQuat = ((Component) this.mTransformTuc).transform.rotation;
            ((Component) this.mTransformTuc).transform.position = vector3_1;
            ((Component) this.mTransformTuc).transform.rotation = Quaternion.LookRotation(Vector3.op_Subtraction(self.mBattleSceneRoot.PlayerStart1.position, self.mBattleSceneRoot.PlayerStart2.position));
            ((Component) this.mTransformTuc).transform.SetParent(((Component) self.mBattleSceneRoot).transform, false);
            this.mTransformTuc.EnableDispOffBadStatus(true);
          }
        }
        Transform transform2 = ((Component) Camera.main).transform;
        transform2.position = Vector3.op_Addition(GameSettings.Instance.Quest.BattleCamera.position, self.mBattleSceneRoot.PlayerStart2.position);
        transform2.rotation = GameSettings.Instance.Quest.BattleCamera.rotation;
        self.mUpdateCameraPosition = false;
        self.UpdateCameraControl(true);
        self.DisableUserInterface();
        if (self.Battle.IsMultiVersus)
        {
          self.HideAllHPGauges();
          self.CloseBattleUI();
        }
        self.ToggleRenkeiAura(false);
        GameUtility.FadeOut(0.2f);
        self.StartCoroutine(this.AsyncWork(peek));
      }

      [DebuggerHidden]
      private IEnumerator AsyncWork(LogSkill log)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_Map_PlayPreSkillAnim.\u003CAsyncWork\u003Ec__Iterator0()
        {
          log = log,
          \u0024this = this
        };
      }
    }

    private class State_Map_LoadSkill_Renkei1 : State<SceneBattle>
    {
      private int mRenkeiUnitIndex;
      private float mInterval;

      public override void Begin(SceneBattle self)
      {
        FadeController.Instance.BeginSceneFade(new Color(0.25f, 0.25f, 0.25f), 0.5f, self.mUnitsInBattle.ToArray(), (TacticsUnitController[]) null);
        self.mBattle.CurrentUnit?.PlayBattleVoice("battle_0015");
      }

      public override void Update(SceneBattle self)
      {
        if ((double) this.mInterval > 0.0)
          this.mInterval = -Time.deltaTime;
        if (self.mDesiredCameraTargetSet || ObjectAnimator.Get((Component) Camera.main).isMoving)
          return;
        if (this.mRenkeiUnitIndex < self.mBattle.HelperUnits.Count)
        {
          TacticsUnitController unitController = self.FindUnitController(self.mBattle.HelperUnits[this.mRenkeiUnitIndex]);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          {
            self.SetCameraOffset(((Component) GameSettings.Instance.Quest.UnitCamera).transform);
            self.InterpCameraTarget((Component) unitController);
            float skillCameraDistance = GameSettings.Instance.GameCamera_SkillCameraDistance;
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController) && unitController.Unit != null && !unitController.Unit.IsNormalSize)
              skillCameraDistance += GameSettings.Instance.GameCamera_BigUnitAddDistance;
            self.InterpCameraDistance(skillCameraDistance);
            GameUtility.SpawnParticle(self.mRenkeiAssistEffect, ((Component) unitController).transform, ((Component) self).gameObject);
            this.mInterval = 0.5f;
          }
          ++this.mRenkeiUnitIndex;
        }
        else
        {
          for (int index = self.mUnitsInBattle.Count - 1; index >= 0; --index)
          {
            if (self.mUnitsInBattle[index].IsLoading)
              return;
          }
          self.GotoState<SceneBattle.State_Map_LoadSkill_Renkei2>();
        }
      }
    }

    private class State_Map_LoadSkill_Renkei2 : State<SceneBattle>
    {
      private float mDelay;

      public override void Begin(SceneBattle self)
      {
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          self.SetCameraOffset(((Component) GameSettings.Instance.Quest.UnitCamera).transform);
          self.InterpCameraTarget((Component) unitController);
          float skillCameraDistance = GameSettings.Instance.GameCamera_SkillCameraDistance;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController) && unitController.Unit != null && !unitController.Unit.IsNormalSize)
            skillCameraDistance += GameSettings.Instance.GameCamera_BigUnitAddDistance;
          self.InterpCameraDistance(skillCameraDistance);
          unitController.PlayGenkidama();
          GameUtility.SpawnParticle(self.mRenkeiChargeEffect, ((Component) unitController).transform, ((Component) self).gameObject);
          unitController.FadeBlendColor(SceneBattle.RenkeiChargeColor, 1f);
          unitController.SetLastHitEffect(self.mRenkeiHitEffect);
        }
        this.mDelay = 1f;
      }

      public override void Update(SceneBattle self)
      {
        if ((double) this.mDelay > 0.0)
        {
          this.mDelay -= Time.deltaTime;
        }
        else
        {
          if (self.mDesiredCameraTargetSet || ObjectAnimator.Get((Component) Camera.main).isMoving)
            return;
          for (int index = self.mUnitsInBattle.Count - 1; index >= 0; --index)
          {
            if (self.mUnitsInBattle[index].IsLoading || !self.mUnitsInBattle[index].isIdle)
              return;
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillSplash, (UnityEngine.Object) null))
          {
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mSkillSplash))
              self.mSkillSplash.Close();
            self.mSkillSplash = (SkillSplash) null;
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillSplashCollabo, (UnityEngine.Object) null))
          {
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mSkillSplashCollabo))
              self.mSkillSplashCollabo.Close();
            self.mSkillSplashCollabo = (SkillSplashCollabo) null;
          }
          self.GotoState<SceneBattle.State_Map_PlayPreDodge>();
        }
      }
    }

    private class State_Map_LoadSkill_Protect : State<SceneBattle>
    {
      private bool mIsFinished;
      private int mUnitProtectIndex;
      private int mUnitGuardIndex;

      public override void Begin(SceneBattle self) => this.mIsFinished = true;

      [DebuggerHidden]
      private IEnumerator ProtectEffect(Unit unit, bool isProtectTarget)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_Map_LoadSkill_Protect.\u003CProtectEffect\u003Ec__Iterator0()
        {
          unit = unit,
          isProtectTarget = isProtectTarget,
          \u0024this = this
        };
      }

      public override void Update(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillSplash, (UnityEngine.Object) null) && self.mSkillSplash.NoLoop || self.IsCameraMoving || !this.mIsFinished)
          return;
        if (this.mUnitProtectIndex < self.mBattle.ProtectUnits.Count)
        {
          self.StartCoroutine(this.ProtectEffect(self.mBattle.ProtectUnits[this.mUnitProtectIndex], true));
          ++this.mUnitProtectIndex;
        }
        else if (this.mUnitGuardIndex < self.mBattle.GuardUnits.Count)
        {
          self.StartCoroutine(this.ProtectEffect(self.mBattle.GuardUnits[this.mUnitGuardIndex], false));
          ++this.mUnitGuardIndex;
        }
        else
        {
          for (int index = self.mUnitsInBattle.Count - 1; index >= 0; --index)
          {
            if (self.mUnitsInBattle[index].IsLoading || !self.mUnitsInBattle[index].isIdle)
              return;
          }
          self.GotoState<SceneBattle.State_Map_LoadSkill_Protect_Next>();
        }
      }
    }

    private class State_Map_LoadSkill_Protect_Next : State<SceneBattle>
    {
      private LogSkill log;
      private TacticsUnitController controller;

      public override void Begin(SceneBattle self)
      {
        this.log = self.mBattle.Logs.Peek as LogSkill;
        LogSkill log = this.log;
        this.controller = self.FindUnitController(log.self);
        self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
        self.InterpCameraTarget((Component) this.controller);
      }

      public override void Update(SceneBattle self)
      {
        if (self.IsCameraMoving)
          return;
        if (this.log.IsRenkei())
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.controller, (UnityEngine.Object) null))
            this.controller.LoadGenkidamaAnimation(true);
          self.GotoState<SceneBattle.State_Map_LoadSkill_Renkei1>();
        }
        else
          self.GotoState<SceneBattle.State_Map_PlayPreDodge>();
      }
    }

    private class State_Map_PlayPreDodge : State<SceneBattle>
    {
      private List<TacticsUnitController> mPollControllers = new List<TacticsUnitController>();
      private float mDelay = 0.1f;

      public override void Begin(SceneBattle self)
      {
        BattleLog peek = self.mBattle.Logs.Peek;
        if (peek is LogSkill)
        {
          LogSkill logSkill = peek as LogSkill;
          TacticsUnitController unitController1 = self.FindUnitController(logSkill.self);
          for (int index = 0; index < logSkill.targets.Count; ++index)
          {
            if (logSkill.targets[index].IsAvoid())
            {
              TacticsUnitController unitController2 = self.FindUnitController(logSkill.targets[index].target);
              unitController2.AutoUpdateRotation = false;
              unitController2.LookAt(((Component) unitController1).transform.position);
              this.mPollControllers.Add(unitController2);
            }
          }
        }
        if (this.mPollControllers.Count > 0)
          return;
        self.GotoState<SceneBattle.State_Map_PlaySkill>();
      }

      public override void Update(SceneBattle self)
      {
        for (int index = this.mPollControllers.Count - 1; index >= 0; --index)
        {
          if (!this.mPollControllers[index].isIdle)
            return;
        }
        if ((double) this.mDelay > 0.0)
          this.mDelay -= Time.deltaTime;
        else
          self.GotoState<SceneBattle.State_Map_PlaySkill>();
      }
    }

    private class State_Map_PlaySkill : State<SceneBattle>
    {
      private TacticsUnitController mInstigator;
      private List<TacticsUnitController> mTargets = new List<TacticsUnitController>();
      private float mEndWait;
      private LogSkill mActionInfo;
      private IntVector2 mCameraStart;

      public override void Begin(SceneBattle self)
      {
        this.mActionInfo = self.mBattle.Logs.Peek as LogSkill;
        this.mInstigator = self.FindUnitController(this.mActionInfo.self);
        if (this.mActionInfo.skill.CastType != ECastTypes.Jump)
          self.mUpdateCameraPosition = false;
        bool flag1 = false;
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mInstigator) && this.mInstigator.Unit != null && !this.mInstigator.Unit.IsNormalSize)
        {
          flag1 = true;
        }
        else
        {
          foreach (LogSkill.Target target in this.mActionInfo.targets)
          {
            TacticsUnitController unitController = self.FindUnitController(target.target);
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController) && unitController.Unit != null && !unitController.Unit.IsNormalSize)
            {
              flag1 = true;
              break;
            }
          }
        }
        if (this.mActionInfo.skill.EffectType == SkillEffectTypes.Changing && this.mActionInfo.targets.Count > 0 && this.mActionInfo.targets[0] != null)
        {
          this.mCameraStart.x = this.mActionInfo.targets[0].target.x;
          this.mCameraStart.y = this.mActionInfo.targets[0].target.y;
          float skillCameraDistance = GameSettings.Instance.GameCamera_SkillCameraDistance;
          if (flag1)
            skillCameraDistance += GameSettings.Instance.GameCamera_BigUnitAddDistance;
          self.InterpCameraDistance(skillCameraDistance * 1.5f);
        }
        else
        {
          switch (this.mInstigator.GetSkillCameraType())
          {
            case SkillSequence.MapCameraTypes.FirstTargetCenter:
              this.mCameraStart.x = this.mActionInfo.self.x;
              this.mCameraStart.y = this.mActionInfo.self.y;
              float skillCameraDistance1 = GameSettings.Instance.GameCamera_SkillCameraDistance;
              if (flag1)
                skillCameraDistance1 += GameSettings.Instance.GameCamera_BigUnitAddDistance;
              self.InterpCameraDistance(skillCameraDistance1 * 1.5f);
              break;
            case SkillSequence.MapCameraTypes.FarDistance:
              self.mUpdateCameraPosition = true;
              self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
              break;
            case SkillSequence.MapCameraTypes.MoreFarDistance:
              self.mUpdateCameraPosition = true;
              self.InterpCameraDistance(GameSettings.Instance.GameCamera_MoreFarDistance);
              break;
            case SkillSequence.MapCameraTypes.AllTargetsCenter:
            case SkillSequence.MapCameraTypes.AllTargetsAndSelfCenter:
              Vector3 center = Vector3.zero;
              float distance = GameSettings.Instance.GameCamera_SkillCameraDistance;
              if (flag1)
                distance += GameSettings.Instance.GameCamera_BigUnitAddDistance;
              List<Vector3> vector3List1 = new List<Vector3>();
              switch (this.mInstigator.GetSkillCameraType())
              {
                case SkillSequence.MapCameraTypes.AllTargetsCenter:
                  using (List<LogSkill.Target>.Enumerator enumerator = this.mActionInfo.targets.GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      LogSkill.Target current = enumerator.Current;
                      TacticsUnitController unitController = self.FindUnitController(current.target);
                      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController))
                      {
                        Vector3 centerPosition = unitController.CenterPosition;
                        if (unitController.Unit != null && !unitController.Unit.IsNormalSize)
                          centerPosition.y += unitController.Unit.OffsZ;
                        vector3List1.Add(centerPosition);
                      }
                    }
                    break;
                  }
                case SkillSequence.MapCameraTypes.AllTargetsAndSelfCenter:
                  Vector3 centerPosition1 = this.mInstigator.CenterPosition;
                  if (this.mInstigator.Unit != null && !this.mInstigator.Unit.IsNormalSize)
                    centerPosition1.y += this.mInstigator.Unit.OffsZ;
                  vector3List1.Add(centerPosition1);
                  goto case SkillSequence.MapCameraTypes.AllTargetsCenter;
              }
              if (vector3List1.Count != 0)
              {
                self.GetCameraTargetView(out center, out distance, vector3List1.ToArray());
                self.InterpCameraTarget(center);
                self.InterpCameraDistance(distance);
                break;
              }
              break;
          }
        }
        SkillData skill = this.mActionInfo.skill;
        SkillParam skillParam = skill.SkillParam;
        List<Vector3> vector3List2 = (List<Vector3>) null;
        if (skillParam.IsAreaSkill())
        {
          vector3List2 = new List<Vector3>();
          GridMap<bool> scopeGridMap = self.mBattle.CreateScopeGridMap(this.mActionInfo.self, this.mActionInfo.self.x, this.mActionInfo.self.y, this.mActionInfo.pos.x, this.mActionInfo.pos.y, skill);
          for (int y = 0; y < scopeGridMap.h; ++y)
          {
            for (int x = 0; x < scopeGridMap.w; ++x)
            {
              if (scopeGridMap.get(x, y))
                vector3List2.Add(self.CalcGridCenter(x, y));
            }
          }
        }
        this.mInstigator.SetArrowTrajectoryHeight((float) this.mActionInfo.height / 100f);
        SkillEffect loadedSkillEffect = this.mInstigator.LoadedSkillEffect;
        bool flag2 = false;
        GameSettings instance = GameSettings.Instance;
        if (this.mActionInfo.skill.CastType == ECastTypes.Jump)
        {
          self.SetCameraOffset(((Component) GameSettings.Instance.Quest.UnitCamera).transform);
          float skillCameraDistance2 = GameSettings.Instance.GameCamera_SkillCameraDistance;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mInstigator) && this.mInstigator.Unit != null && !this.mInstigator.Unit.IsNormalSize)
            skillCameraDistance2 += GameSettings.Instance.GameCamera_BigUnitAddDistance;
          self.InterpCameraDistance(skillCameraDistance2);
        }
        this.mInstigator.SetHitInfoSelf(this.mActionInfo.self_effect);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
          self.mCollaboTargetTuc.SetHitInfoSelf(new LogSkill.Target());
        TacticsUnitController[] targets = new TacticsUnitController[this.mActionInfo.targets.Count];
        for (int index = 0; index < this.mActionInfo.targets.Count; ++index)
        {
          LogSkill.Target target = this.mActionInfo.targets[index];
          TacticsUnitController unitController = self.FindUnitController(this.mActionInfo.targets[index].target);
          targets[index] = unitController;
          unitController.ShouldDodgeHits = target.IsAvoid();
          unitController.ShouldPerfectDodge = target.IsPerfectAvoid();
          unitController.ShouldDefendHits = target.IsDefend();
          unitController.SetHitInfo(target);
          if (!unitController.ShouldDodgeHits)
          {
            unitController.ShowCriticalEffectOnHit = target.IsCritical();
            unitController.ShowBackstabEffectOnHit = (target.hitType & LogSkill.EHitTypes.BackAttack) != (LogSkill.EHitTypes) 0 && skill.IsNormalAttack();
            unitController.ShouldDefendHits = target.IsDefend();
            unitController.ShowElementEffectOnHit = !target.IsNormalEffectElement() ? (!target.IsWeakEffectElement() ? TacticsUnitController.EElementEffectTypes.NotEffective : TacticsUnitController.EElementEffectTypes.Effective) : TacticsUnitController.EElementEffectTypes.Normal;
          }
          else
          {
            unitController.ShowCriticalEffectOnHit = false;
            unitController.ShowBackstabEffectOnHit = false;
            unitController.ShouldDefendHits = false;
            unitController.ShowElementEffectOnHit = TacticsUnitController.EElementEffectTypes.Normal;
          }
          if (target.gems > 0)
          {
            if ((target.hitType & LogSkill.EHitTypes.SideAttack) != (LogSkill.EHitTypes) 0)
            {
              unitController.DrainGemsOnHit = instance.Gem_DrainCount_SideHit;
              unitController.GemDrainEffects = self.mGemDrainEffect_Side;
            }
            else if ((target.hitType & LogSkill.EHitTypes.BackAttack) != (LogSkill.EHitTypes) 0)
            {
              unitController.DrainGemsOnHit = instance.Gem_DrainCount_BackHit;
              unitController.GemDrainEffects = self.mGemDrainEffect_Back;
            }
            else
            {
              unitController.DrainGemsOnHit = instance.Gem_DrainCount_FrontHit;
              unitController.GemDrainEffects = self.mGemDrainEffect_Front;
            }
            unitController.GemDrainHitEffect = self.mGemDrainHitEffect;
          }
          else
            unitController.DrainGemsOnHit = 0;
          unitController.KnockBackGrid = target.KnockBackGrid;
          this.mTargets.Add(unitController);
        }
        foreach (SkillEffect.SFX explosionSound in loadedSkillEffect.ExplosionSounds)
          explosionSound.IsCritical = false;
        if (this.mActionInfo.skill.IsNormalAttack() && loadedSkillEffect.ExplosionSounds.Length > 0)
          loadedSkillEffect.ExplosionSounds[0].IsCritical = flag2;
        if (this.mActionInfo.skill.CastType == ECastTypes.Jump)
          this.mInstigator.SetLandingGrid(this.mActionInfo.landing);
        if (this.mActionInfo.skill.TeleportType != eTeleportType.None)
          this.mInstigator.SetTeleportGrid(this.mActionInfo.TeleportGrid);
        this.mInstigator.StartSkill(self.CalcGridCenter(this.mActionInfo.pos.x, this.mActionInfo.pos.y), Camera.main, targets, vector3List2 == null ? (Vector3[]) null : vector3List2.ToArray(), skill.SkillParam);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
          self.mCollaboTargetTuc.StartSkill(self.CalcGridCenter(this.mActionInfo.pos.x, this.mActionInfo.pos.y), Camera.main, targets, vector3List2 == null ? (Vector3[]) null : vector3List2.ToArray(), skill.SkillParam);
        if (skill.IsShowSkillNamePlate)
          self.ShowSkillNamePlate(this.mActionInfo.self, skill, string.Empty);
        this.mEndWait = GameSettings.Instance.Quest.BattleTurnEndWait;
        self.RemoveLog();
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mInstigator.isIdle)
        {
          if (this.mActionInfo.skill.CastType == ECastTypes.Jump)
          {
            Vector3 position = self.CalcGridCenter(this.mActionInfo.pos.x, this.mActionInfo.pos.y);
            self.InterpCameraTarget(position);
          }
          else
          {
            if (this.mActionInfo.skill.EffectType != SkillEffectTypes.Changing && this.mInstigator.GetSkillCameraType() != SkillSequence.MapCameraTypes.FirstTargetCenter)
              return;
            IntVector2 intVector2 = new IntVector2((this.mActionInfo.pos.x - this.mCameraStart.x) / 2, (this.mActionInfo.pos.y - this.mCameraStart.y) / 2);
            Vector3 position = self.CalcGridCenter(this.mCameraStart.x + intVector2.x, this.mCameraStart.y + intVector2.y);
            self.InterpCameraTarget(position);
          }
        }
        else
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null) && !self.mCollaboTargetTuc.isIdle)
            return;
          foreach (TacticsUnitController mTarget in this.mTargets)
          {
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) mTarget) && mTarget.IsBusy)
              return;
          }
          if ((double) this.mEndWait > 0.0)
          {
            this.mEndWait -= Time.deltaTime;
          }
          else
          {
            if (self.IsHPGaugeChanging)
              return;
            self.HideAllHPGauges();
            for (int index = 0; index < this.mTargets.Count; ++index)
            {
              this.mTargets[index].ShouldDodgeHits = false;
              this.mTargets[index].ShouldPerfectDodge = false;
              this.mTargets[index].ShouldDefendHits = false;
              this.mTargets[index].ShowCriticalEffectOnHit = false;
              this.mTargets[index].ShowBackstabEffectOnHit = false;
              this.mTargets[index].DrainGemsOnHit = 0;
              this.mTargets[index].ShowElementEffectOnHit = TacticsUnitController.EElementEffectTypes.Normal;
            }
            if (this.mActionInfo.IsRenkei())
              self.ToggleRenkeiAura(false);
            self.RefreshJumpSpots();
            TrickData.AddMarker();
            this.mInstigator.AnimateVessel(0.0f, 0.5f);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
              self.mCollaboTargetTuc.AnimateVessel(0.0f, 0.5f);
            FadeController.Instance.BeginSceneFade(Color.white, 0.5f, self.mUnitsInBattle.ToArray(), (TacticsUnitController[]) null);
            self.RefreshUnitStatus(this.mInstigator.Unit);
            SkillData skill = this.mActionInfo.skill;
            if ((bool) skill.IsCollabo)
            {
              Unit unitUseCollaboSkill = self.Battle.GetUnitUseCollaboSkill(this.mInstigator.Unit, skill);
              if (unitUseCollaboSkill != null)
                self.RefreshUnitStatus(unitUseCollaboSkill);
            }
            self.mSkillNamePlate.Close();
            self.ResetCameraTarget();
            if (this.mActionInfo.skill.IsTransformSkill())
            {
              self.HideUnitMarkers(this.mInstigator.Unit);
              self.mTacticsUnits.Remove(this.mInstigator);
              UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mInstigator).gameObject);
              this.mInstigator = (TacticsUnitController) null;
              if (this.mTargets.Count != 0)
              {
                this.mTargets[0].SetVisible(true);
                this.mTargets[0].UpdateShields();
              }
              self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
              self.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>();
            }
            else
            {
              if (this.mActionInfo.skill.IsSetBreakObjSkill() && this.mTargets.Count != 0)
              {
                this.mTargets[0].SetVisible(true);
                self.OnGimmickUpdate();
              }
              self.mIgnoreShieldEffect.Clear();
              if (this.mActionInfo != null)
              {
                self.mIgnoreShieldEffect.Add(this.mInstigator);
                foreach (LogSkill.Target target in this.mActionInfo.targets)
                {
                  if (!target.isProcShield)
                  {
                    TacticsUnitController unitController = self.FindUnitController(target.target);
                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
                      self.mIgnoreShieldEffect.Add(unitController);
                  }
                }
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null))
                {
                  TacticsUnitController unitController1 = self.FindUnitController(this.mActionInfo.self);
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController1, (UnityEngine.Object) null) && this.mActionInfo.targets != null && this.mActionInfo.targets.Count != 0)
                  {
                    List<TacticsUnitController> TargetLists = new List<TacticsUnitController>();
                    foreach (LogSkill.Target target in this.mActionInfo.targets)
                    {
                      TacticsUnitController unitController2 = self.FindUnitController(target.target);
                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController2, (UnityEngine.Object) null))
                        TargetLists.Add(unitController2);
                    }
                    if (TargetLists.Count != 0)
                    {
                      self.mEventSequence = self.mEventScript.OnUseSkill(EventScript.SkillTiming.AFTER, unitController1, this.mActionInfo.skill, TargetLists, self.mIsFirstPlay);
                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
                      {
                        self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_SpawnShieldEffects>>();
                        return;
                      }
                    }
                  }
                }
              }
              self.GotoState<SceneBattle.State_SpawnShieldEffects>();
            }
          }
        }
      }
    }

    private class FindShield
    {
      public TacticsUnitController mTuc;
      public TacticsUnitController.ShieldState mShield;

      public FindShield(TacticsUnitController tuc, TacticsUnitController.ShieldState shield)
      {
        this.mTuc = tuc;
        this.mShield = shield;
      }
    }

    private class State_SpawnShieldEffects : State<SceneBattle>
    {
      private TacticsUnitController mUnit;
      private TacticsUnitController.ShieldState mShield;
      private bool mIsFinished;

      public override void Begin(SceneBattle self)
      {
        foreach (TacticsUnitController mTacticsUnit in self.mTacticsUnits)
        {
          bool is_check_dirty = !self.mIgnoreShieldEffect.Contains(mTacticsUnit);
          mTacticsUnit.UpdateShields(false, is_check_dirty);
        }
        if (!self.FindChangedShield(out this.mUnit, out this.mShield))
        {
          self.GotoState<SceneBattle.State_TriggerHPEvents>();
        }
        else
        {
          this.mIsFinished = false;
          self.StartCoroutine(this.SpawnEffectsAsync());
        }
      }

      [DebuggerHidden]
      private IEnumerator SpawnEffectsAsync()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_SpawnShieldEffects.\u003CSpawnEffectsAsync\u003Ec__Iterator0()
        {
          \u0024this = this
        };
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mIsFinished)
          return;
        self.mIgnoreShieldEffect.Clear();
        self.GotoState<SceneBattle.State_TriggerHPEvents>();
      }
    }

    private class EventRecvSkillUnit
    {
      public bool mValid;
      public SceneBattle.EventRecvSkillUnit.eType mType;
      public TacticsUnitController mController;
      public EElement mElem;
      public EUnitCondition mCond;

      public EventRecvSkillUnit()
      {
      }

      public EventRecvSkillUnit(TacticsUnitController tuc, EElement elem)
      {
        this.mType = SceneBattle.EventRecvSkillUnit.eType.ELEM;
        this.mController = tuc;
        this.mElem = elem;
        this.mValid = true;
      }

      public EventRecvSkillUnit(TacticsUnitController tuc, EUnitCondition cond)
      {
        this.mType = SceneBattle.EventRecvSkillUnit.eType.COND;
        this.mController = tuc;
        this.mCond = cond;
        this.mValid = true;
      }

      public enum eType
      {
        UNKNOWN,
        ELEM,
        COND,
      }
    }

    private class State_TriggerHPEvents : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null))
        {
          for (int index = 0; index < self.mTacticsUnits.Count; ++index)
          {
            self.mEventSequence = self.mEventScript.OnUnitHPChange(self.mTacticsUnits[index]);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
            {
              self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_TriggerHPEvents>>();
              return;
            }
          }
          foreach (TacticsUnitController mTacticsUnit in self.mTacticsUnits)
          {
            self.mEventSequence = self.mEventScript.OnUnitRestHP(mTacticsUnit, self.mIsFirstPlay);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
            {
              self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_TriggerHPEvents>>();
              return;
            }
          }
          foreach (SceneBattle.EventRecvSkillUnit recvSkillUnitList in self.mEventRecvSkillUnitLists)
          {
            if (recvSkillUnitList.mValid)
            {
              self.mEventSequence = (EventScript.Sequence) null;
              switch (recvSkillUnitList.mType)
              {
                case SceneBattle.EventRecvSkillUnit.eType.ELEM:
                  self.mEventSequence = self.mEventScript.OnRecvSkillElem(recvSkillUnitList.mController, recvSkillUnitList.mElem, self.mIsFirstPlay);
                  recvSkillUnitList.mValid = false;
                  break;
                case SceneBattle.EventRecvSkillUnit.eType.COND:
                  self.mEventSequence = self.mEventScript.OnRecvSkillCond(recvSkillUnitList.mController, recvSkillUnitList.mCond, self.mIsFirstPlay);
                  recvSkillUnitList.mValid = false;
                  break;
              }
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
              {
                self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_TriggerHPEvents>>();
                return;
              }
            }
          }
        }
        self.mEventRecvSkillUnitLists.Clear();
        self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
        self.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>();
      }
    }

    private class State_WaitGC<NextState> : State<SceneBattle> where NextState : State<SceneBattle>, new()
    {
      private AsyncOperation mAsyncOp;

      public override void Begin(SceneBattle self)
      {
      }

      public override void Update(SceneBattle self) => self.GotoState<NextState>();
    }

    private class State_Weather : State<SceneBattle>
    {
      private LogWeather mLog;
      private bool mIsFinished;

      public override void Begin(SceneBattle self)
      {
        this.mLog = self.Battle.Logs.Peek as LogWeather;
        if (this.mLog == null || this.mLog.WeatherData == null)
          return;
        this.mIsFinished = false;
        self.StartCoroutine(this.AsyncWeather());
      }

      [DebuggerHidden]
      private IEnumerator AsyncWeather()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_Weather.\u003CAsyncWeather\u003Ec__Iterator0()
        {
          \u0024this = this
        };
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mIsFinished)
          return;
        this.mLog = (LogWeather) null;
        self.RemoveLog();
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_SpawnUnit : State<SceneBattle>
    {
      private LoadRequest mLoadRequest;
      private SceneBattle mScene;
      private bool mIsFinished;
      private Unit mSpawnUnit;

      public override void Begin(SceneBattle self)
      {
        this.mScene = self;
        this.mIsFinished = true;
        if (!(self.mBattle.Logs.Peek is LogUnitEntry peek))
          return;
        this.mIsFinished = false;
        this.mSpawnUnit = peek.self;
        self.StartCoroutine(this.AsyncWork(peek.self, peek.kill_unit));
        self.RemoveLog();
      }

      [DebuggerHidden]
      private IEnumerator AsyncWork(Unit unit, Unit kill_unit)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_SpawnUnit.\u003CAsyncWork\u003Ec__Iterator0()
        {
          unit = unit,
          kill_unit = kill_unit,
          \u0024this = this
        };
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mIsFinished)
          return;
        if (this.mSpawnUnit != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null))
        {
          TacticsUnitController unitController = self.FindUnitController(this.mSpawnUnit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          {
            self.mEventSequence = self.mEventScript.OnUnitAppear(unitController, self.mIsFirstPlay);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
            {
              self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>>();
              return;
            }
          }
        }
        self.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>();
      }
    }

    private class State_UnitWithdraw : State<SceneBattle>
    {
      private bool mIsFinished;

      public override void Begin(SceneBattle self)
      {
        this.mIsFinished = true;
        if (!(self.mBattle.Logs.Peek is LogUnitWithdraw peek))
          return;
        this.mIsFinished = false;
        self.StartCoroutine(this.AsyncWork(peek.self));
      }

      [DebuggerHidden]
      private IEnumerator AsyncWork(Unit unit)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_UnitWithdraw.\u003CAsyncWork\u003Ec__Iterator0()
        {
          unit = unit,
          \u0024this = this
        };
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mIsFinished)
          return;
        self.RemoveLog();
        self.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>();
      }
    }

    private class State_CancelTransform : State<SceneBattle>
    {
      private bool mIsFinished;

      public override void Begin(SceneBattle self)
      {
        this.mIsFinished = true;
        if (!(self.mBattle.Logs.Peek is LogCancelTransform peek) || peek.self == null || peek.next_unit == null || peek.dtu == null)
          return;
        this.mIsFinished = false;
        self.StartCoroutine(this.AsyncWork(peek.self, peek.next_unit, peek.dtu));
      }

      [DebuggerHidden]
      private IEnumerator AsyncWork(Unit from_unit, Unit next_unit, DynamicTransformUnitParam dtu)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_CancelTransform.\u003CAsyncWork\u003Ec__Iterator0()
        {
          from_unit = from_unit,
          dtu = dtu,
          next_unit = next_unit,
          \u0024this = this
        };
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mIsFinished)
          return;
        self.RemoveLog();
        self.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>();
      }
    }

    private class State_Inspiration : State<SceneBattle>
    {
      private bool mIsFinished;

      public override void Begin(SceneBattle self)
      {
        this.mIsFinished = true;
        if (!(self.mBattle.Logs.Peek is LogInspiration peek) || peek.self == null)
          return;
        this.mIsFinished = false;
        self.StartCoroutine(this.InspirationEffect(peek.self));
      }

      [DebuggerHidden]
      private IEnumerator InspirationEffect(Unit unit)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_Inspiration.\u003CInspirationEffect\u003Ec__Iterator0()
        {
          unit = unit,
          \u0024this = this
        };
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mIsFinished)
          return;
        self.RemoveLog();
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_ArenaCalc : State<SceneBattle>
    {
      private GameObject mGoWin;

      public override void Begin(SceneBattle self)
      {
        self.Battle.ArenaCalcStart();
        self.ArenaActionCountSet(self.Battle.ArenaActionCount);
      }

      public override void Update(SceneBattle self)
      {
        if (!self.Battle.IsArenaCalc || !self.Battle.ArenaCalcStep())
          return;
        self.Battle.ArenaCalcFinish();
        self.StartCoroutine(this.sendResultStartReplay(self));
      }

      [DebuggerHidden]
      private IEnumerator sendResultStartReplay(SceneBattle self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_ArenaCalc.\u003CsendResultStartReplay\u003Ec__Iterator0()
        {
          self = self
        };
      }
    }

    private class State_GvGCalc : State<SceneBattle>
    {
      private GameObject mGoWin;

      public override void Begin(SceneBattle self)
      {
        self.Battle.ArenaCalcStart();
        self.ArenaActionCountSet(self.Battle.ArenaActionCount);
      }

      public override void Update(SceneBattle self)
      {
        if (!self.Battle.IsArenaCalc || !self.Battle.ArenaCalcStep())
          return;
        self.Battle.ArenaCalcFinish();
        self.StartCoroutine(this.sendResultStart(self));
      }

      [DebuggerHidden]
      private IEnumerator sendResultStart(SceneBattle self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_GvGCalc.\u003CsendResultStart\u003Ec__Iterator0()
        {
          self = self
        };
      }
    }

    private class State_DirectionOffSkill : State<SceneBattle>
    {
      private IEnumerator mTask;
      private IEnumerator mTaskNext;
      private LogSkill mActionInfo;
      private TacticsUnitController mInstigator;
      private List<TacticsUnitController> mTargets = new List<TacticsUnitController>();

      private void ResetTask()
      {
        this.mTask = (IEnumerator) null;
        this.mTaskNext = (IEnumerator) null;
      }

      private void SetTask(IEnumerator enumrator) => this.mTaskNext = enumrator;

      private bool NextTask()
      {
        if (this.mTaskNext == null)
          return false;
        this.mTask = this.mTaskNext;
        this.mTaskNext = (IEnumerator) null;
        return true;
      }

      public override void Begin(SceneBattle self)
      {
        LogSkill peek = self.mBattle.Logs.Peek as LogSkill;
        this.ResetTask();
        if (peek.skill == null)
        {
          self.RemoveLog();
          DebugUtility.LogError("SkillParam が存在しません。");
        }
        else
        {
          this.mActionInfo = peek;
          this.mInstigator = self.FindUnitController(this.mActionInfo.self);
          this.SetTask(this.Task_PrepareSkill(self));
          self.RemoveLog();
        }
      }

      public override void Update(SceneBattle self)
      {
        bool flag = !this.NextTask();
        if (this.mTask != null)
          flag = !this.mTask.MoveNext() && !this.NextTask();
        if (!flag)
          return;
        this.mTask = (IEnumerator) null;
      }

      public override void End(SceneBattle self)
      {
      }

      [DebuggerHidden]
      private IEnumerator Task_PrepareSkill(SceneBattle self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_DirectionOffSkill.\u003CTask_PrepareSkill\u003Ec__Iterator0()
        {
          self = self,
          \u0024this = this
        };
      }

      [DebuggerHidden]
      private IEnumerator Task_MapPrepareSkill(SceneBattle self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_DirectionOffSkill.\u003CTask_MapPrepareSkill\u003Ec__Iterator1()
        {
          self = self,
          \u0024this = this
        };
      }

      [DebuggerHidden]
      private IEnumerator Task_MapPlaySkill(SceneBattle self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_DirectionOffSkill.\u003CTask_MapPlaySkill\u003Ec__Iterator2()
        {
          self = self,
          \u0024this = this
        };
      }

      [DebuggerHidden]
      private IEnumerator Task_Execute(SceneBattle self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_DirectionOffSkill.\u003CTask_Execute\u003Ec__Iterator3()
        {
          self = self,
          \u0024this = this
        };
      }

      private enum EndGotoState
      {
        NONE,
        WAIT_FOR_LOG,
        WAITEVENT_SPAWN_SHIELD_EFFECT,
        SPAWN_SHIELD_EFFECT,
      }
    }

    public enum CameraMode
    {
      DEFAULT,
      UPVIEW,
    }

    public enum EMultiPlayCommand
    {
      NOP,
      MOVE_START,
      MOVE,
      GRID_XY,
      MOVE_END,
      MOVE_CANCEL,
      ENTRY_BATTLE,
      GRID_EVENT,
      UNIT_END,
      UNIT_TIME_LIMIT,
      UNIT_XYDIR,
      NUM,
    }

    public class MultiPlayInput
    {
      public int b;
      public int t;
      public int c;
      public int u = -1;
      public string s = string.Empty;
      public string i = string.Empty;
      public int gx;
      public int gy;
      public int ul;
      public int d = 1;
      public float x;
      public float z;
      public float r;

      public bool IsValid(SceneBattle self)
      {
        if (this.b < 0 || this.t < 0 || this.c < 0 || this.c >= 11 || this.u < -1 || this.u >= self.Battle.AllUnits.Count || this.s == null || this.s != string.Empty && MonoSingleton<GameManager>.Instance.GetSkillParam(this.s) == null || this.i == null || this.i != string.Empty && MonoSingleton<GameManager>.Instance.GetItemParam(this.i) == null || this.gx < 0 || this.gx >= self.Battle.CurrentMap.Width || this.gy < 0 || this.gy >= self.Battle.CurrentMap.Height)
          return false;
        if (this.c == 6)
        {
          if (this.d != 1 && this.d != 2 && this.d != 3 && this.d != 4)
            return false;
        }
        else if (this.d != 0 && this.d != 2 && this.d != 1 && this.d != 3)
          return false;
        return (double) this.x >= 0.0 && (double) this.x <= (double) self.Battle.CurrentMap.Width && (double) this.z >= 0.0 && (double) this.z <= (double) self.Battle.CurrentMap.Height;
      }
    }

    public enum EMultiPlayRecvDataHeader
    {
      NOP,
      INPUT,
      CHECK,
      GOODJOB,
      CONTINUE,
      IGNORE_MY_DISCONNECT,
      FINISH_LOAD,
      REQUEST_RESUME,
      RESUME_INFO,
      RESUME_SUCCESS,
      SYNC,
      SYNC_RESUME,
      VS_RETIRE,
      VS_RETIRE_COMFIRM,
      CHEAT_MOVE,
      CHEAT_MP,
      CHEAT_RANGE,
      OTHERPLAYER_DISCONNECT,
      START_AUTO,
      NUM,
    }

    public enum CHEAT_TYPE
    {
      MOVE,
      MP,
      RANGE,
    }

    [MessagePackObject(true)]
    public class MultiPlayRecvData
    {
      public string qst = "quest";
      public int sq;
      public int h;
      public int b;
      public int pidx;
      public int pid;
      public int uid;
      public int[] c;
      public int[] u;
      public string[] s;
      public string[] i;
      public int[] gx;
      public int[] gy;
      public int[] ul;
      public int[] d;
      public float[] x;
      public float[] z;
      public float[] r;
    }

    public class MultiPlayRecvBinData
    {
      public byte[] bin;
    }

    public class MultiPlayCheck
    {
      public int work;
      public int playerIndex;
      public int playerID;
      public int battleTurn;
      public int[] hp;
      public int[] gx;
      public int[] gy;
      public int[] dir;
      public string rnd;

      private bool IsEqual(int[] s0, int[] s1)
      {
        if (s0 == null && s1 == null)
          return true;
        return s0 != null && s1 != null && ((IEnumerable<int>) s0).SequenceEqual<int>((IEnumerable<int>) s1);
      }

      public bool IsEqual(SceneBattle.MultiPlayCheck dst)
      {
        if (this.battleTurn != dst.battleTurn)
        {
          DebugUtility.LogError("battleTurn not match");
          return false;
        }
        if (!this.IsEqual(this.hp, dst.hp))
        {
          DebugUtility.LogError("hp not match");
          return false;
        }
        if (!this.IsEqual(this.gx, dst.gx))
        {
          DebugUtility.LogError("gx not match");
          return false;
        }
        if (!this.IsEqual(this.gy, dst.gy))
        {
          DebugUtility.LogError("gy not match");
          return false;
        }
        if (!this.IsEqual(this.dir, dst.dir))
        {
          DebugUtility.LogError("dir not match");
          return false;
        }
        if (string.IsNullOrEmpty(this.rnd) || string.IsNullOrEmpty(dst.rnd) || !(this.rnd != dst.rnd))
          return true;
        DebugUtility.LogError("rnd not match");
        return false;
      }

      private string GetIntListString(int[] a)
      {
        if (a == null)
          return "[null]";
        string intListString = string.Empty;
        for (int index = 0; index < a.Length; ++index)
          intListString = intListString + a[index].ToString() + ",";
        return intListString;
      }

      public override string ToString()
      {
        string str = string.Empty + "pid:" + (object) this.playerID + "pidx:" + (object) this.playerIndex + " bt:" + (object) this.battleTurn + " gx:" + this.GetIntListString(this.gx) + " gy:" + this.GetIntListString(this.gy) + " hp:" + this.GetIntListString(this.hp) + " dir:" + this.GetIntListString(this.dir);
        if (!string.IsNullOrEmpty(this.rnd))
          str = str + " rnd:" + this.rnd;
        return str;
      }
    }

    private enum EDisconnectType
    {
      DISCONNECTED,
      BAN,
      SEQUENCE_ERROR,
    }

    public enum ENotifyDisconnectedPlayerType
    {
      NORMAL,
      OWNER,
      OWNER_AND_I_AM_OWNER,
    }

    public class ReqCreateBreakObjUc
    {
      public SkillData mSkill;
      public Unit mTargetUnit;

      public ReqCreateBreakObjUc(SkillData skill, Unit target_unit)
      {
        this.mSkill = skill;
        this.mTargetUnit = target_unit;
      }
    }

    public class ReqCreateDtuUc
    {
      public SkillData mSkill;
      public Unit mTargetUnit;

      public ReqCreateDtuUc(SkillData skill, Unit target_unit)
      {
        this.mSkill = skill;
        this.mTargetUnit = target_unit;
      }
    }

    private class State_Disconnected : State<SceneBattle>
    {
    }

    private class State_MultiPlayContinueBase : State<SceneBattle>
    {
      private List<SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest> mReqPool = new List<SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest>();
      private bool mInitFlag;
      private int mRoomOwnerPlayerID;

      public int Seed { get; set; }

      public long BtlID { get; set; }

      private SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest SendMultiPlayContinueRequest(
        SceneBattle self,
        bool flag,
        List<int> units,
        int seed,
        long btlid)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        MyPhoton.MyPlayer myPlayer = instance.GetMyPlayer();
        if (myPlayer == null)
          return (SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest) null;
        List<SceneBattle.MultiPlayInput> sendList = new List<SceneBattle.MultiPlayInput>();
        if (units == null || units.Count <= 0)
        {
          sendList.Add(new SceneBattle.MultiPlayInput()
          {
            s = seed.ToString(),
            i = btlid.ToString()
          });
        }
        else
        {
          for (int index = 0; index < units.Count; ++index)
            sendList.Add(new SceneBattle.MultiPlayInput()
            {
              s = seed.ToString(),
              i = btlid.ToString(),
              u = units[index]
            });
        }
        byte[] sendBinary = self.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.CONTINUE, !flag ? 0 : 1, sendList);
        instance.SendRoomMessageBinary(true, sendBinary);
        SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest playContinueRequest = new SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest();
        playContinueRequest.playerID = myPlayer.playerID;
        playContinueRequest.flag = flag;
        playContinueRequest.units = units;
        playContinueRequest.seed = seed;
        playContinueRequest.btlid = btlid;
        this.mReqPool.Add(playContinueRequest);
        return playContinueRequest;
      }

      public override void Update(SceneBattle self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        MyPhoton.MyPlayer me = instance.GetMyPlayer();
        List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
        List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
        if (me == null || roomPlayerList == null || myPlayersStarted == null || UnityEngine.Object.op_Equality((UnityEngine.Object) self.mBattleUI_MultiPlay, (UnityEngine.Object) null))
        {
          this.Cancel();
        }
        else
        {
          if (!this.mInitFlag)
          {
            GlobalVars.SelectedMultiPlayContinue = GlobalVars.EMultiPlayContinue.PENDING;
            GlobalVars.SelectedMultiPlayerUnitIDs = (List<int>) null;
            this.mReqPool.Clear();
            this.mRoomOwnerPlayerID = instance.GetOldestPlayer();
            this.OpenUI(this.mRoomOwnerPlayerID == me.playerID);
            this.mInitFlag = true;
          }
          while (self.mRecvContinue.Count > 0)
          {
            SceneBattle.MultiPlayRecvData data = self.mRecvContinue[0];
            if (data.b > self.UnitStartCountTotal)
            {
              DebugUtility.LogWarning("[PUN] new turn data. sq:" + (object) data.sq + " h:" + (object) (SceneBattle.EMultiPlayRecvDataHeader) data.h + " b:" + (object) data.b + "/" + (object) self.UnitStartCountTotal + " test:" + (object) self.mRecvBattle.FindIndex((Predicate<SceneBattle.MultiPlayRecvData>) (r => r.b < data.b)));
              break;
            }
            if (data.b < self.UnitStartCountTotal)
              DebugUtility.LogWarning("[PUN] old turn data. sq:" + (object) data.sq + " h:" + (object) (SceneBattle.EMultiPlayRecvDataHeader) data.h + " b:" + (object) data.b + "/" + (object) self.UnitStartCountTotal);
            else if (data.h == 4)
            {
              this.mReqPool.RemoveAll((Predicate<SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest>) (r => r.playerID == data.pid));
              SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest playContinueRequest = new SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest();
              playContinueRequest.playerID = data.pid;
              playContinueRequest.flag = data.uid != 0;
              if (data.i != null && data.i.Length > 0)
                long.TryParse(data.i[0], out playContinueRequest.btlid);
              if (data.s != null && data.s.Length > 0)
                int.TryParse(data.s[0], out playContinueRequest.seed);
              if (data.u != null)
              {
                for (int index = 0; index < data.u.Length; ++index)
                {
                  if (data.u[index] >= 0)
                    playContinueRequest.units.Add(data.u[index]);
                }
              }
              this.mReqPool.Add(playContinueRequest);
            }
            self.mRecvContinue.RemoveAt(0);
          }
          if (this.mReqPool.Count == 0 && roomPlayerList.Find((Predicate<MyPhoton.MyPlayer>) (p => p.playerID == this.mRoomOwnerPlayerID)) == null)
          {
            this.mInitFlag = false;
            this.CloseUI(this.mRoomOwnerPlayerID == me.playerID, false);
          }
          else
          {
            SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest playContinueRequest1 = this.mReqPool.Find((Predicate<SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest>) (r => r.playerID == me.playerID));
            if (playContinueRequest1 == null)
            {
              if (me.playerID == this.mRoomOwnerPlayerID)
              {
                if (GlobalVars.SelectedMultiPlayContinue == GlobalVars.EMultiPlayContinue.PENDING)
                  return;
                if (GlobalVars.MultiPlayBattleCont != null && GlobalVars.MultiPlayBattleCont.btlinfo != null)
                {
                  this.BtlID = GlobalVars.MultiPlayBattleCont.btlid;
                  this.Seed = GlobalVars.MultiPlayBattleCont.btlinfo.seed;
                }
                playContinueRequest1 = this.SendMultiPlayContinueRequest(self, GlobalVars.SelectedMultiPlayContinue == GlobalVars.EMultiPlayContinue.CONTINUE, GlobalVars.SelectedMultiPlayerUnitIDs, this.Seed, this.BtlID);
              }
              else
              {
                int roomOwnerPlayerID = instance.GetOldestPlayer();
                SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest playContinueRequest2 = this.mReqPool.Find((Predicate<SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest>) (r => r.playerID == roomOwnerPlayerID));
                if (playContinueRequest2 == null)
                  return;
                this.BtlID = playContinueRequest2.btlid;
                this.Seed = playContinueRequest2.seed;
                playContinueRequest1 = this.SendMultiPlayContinueRequest(self, playContinueRequest2.flag, playContinueRequest2.units, this.Seed, this.BtlID);
              }
            }
            foreach (MyPhoton.MyPlayer myPlayer in roomPlayerList)
            {
              MyPhoton.MyPlayer player = myPlayer;
              if (player.start && this.mReqPool.Find((Predicate<SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest>) (r => r.playerID == player.playerID)) == null)
                return;
            }
            foreach (SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest playContinueRequest3 in this.mReqPool)
            {
              if (playContinueRequest1.flag != playContinueRequest3.flag || !playContinueRequest1.units.SequenceEqual<int>((IEnumerable<int>) playContinueRequest3.units) || playContinueRequest1.seed != playContinueRequest3.seed || playContinueRequest1.btlid != playContinueRequest3.btlid)
              {
                this.mInitFlag = false;
                this.CloseUI(this.mRoomOwnerPlayerID == me.playerID, false);
                return;
              }
            }
            this.CloseUI(this.mRoomOwnerPlayerID == me.playerID, true);
            if (!playContinueRequest1.flag)
            {
              this.Cancel();
            }
            else
            {
              this.ExecContinue(playContinueRequest1.units);
              self.ResetCheckData();
              BattleSpeedController.OnContinue();
            }
          }
        }
      }

      protected virtual void OpenUI(bool roomOwner)
      {
      }

      protected virtual void CloseUI(bool roomOwner, bool decided)
      {
      }

      protected virtual void ExecContinue(List<int> units)
      {
      }

      protected virtual void Cancel()
      {
      }

      protected class MultiPlayContinueRequest
      {
        public int playerID;
        public bool flag;
        public long btlid;
        public int seed;
        public List<int> units = new List<int>();
      }
    }

    private class State_MultiPlayRevive : SceneBattle.State_MultiPlayContinueBase
    {
      public override void Begin(SceneBattle self)
      {
        int num1 = 0;
        foreach (Unit unit in self.Battle.Units)
        {
          if (unit.OwnerPlayerIndex > 0 && unit.IsDead)
            ++num1;
        }
        QuestParam quest = self.Battle.GetQuest();
        int num2 = Math.Max(1, quest != null ? (int) quest.multiDead : 1);
        if (num1 >= num2)
          return;
        this.Cancel();
      }

      protected override void OpenUI(bool roomOwner)
      {
        if (roomOwner)
          this.self.mBattleUI_MultiPlay.StartSelectRevive();
        else
          this.self.mBattleUI_MultiPlay.ShowWaitRevive();
      }

      protected override void CloseUI(bool roomOwner, bool decided)
      {
        if (roomOwner)
          return;
        this.self.mBattleUI_MultiPlay.HideWaitRevive();
      }

      protected override void ExecContinue(List<int> units)
      {
        if (units != null)
        {
          for (int index = 0; index < units.Count; ++index)
          {
            if (units[index] >= 0 && units[index] < this.self.Battle.AllUnits.Count)
            {
              Unit allUnit = this.self.Battle.AllUnits[units[index]];
              if (allUnit != null && allUnit.IsDead)
                allUnit.ReqRevive = true;
            }
          }
        }
        this.self.GotoState<SceneBattle.State_WaitForLog>();
      }

      protected override void Cancel() => this.self.GotoState<SceneBattle.State_WaitForLog>();
    }

    private class State_MultiPlayContinue : SceneBattle.State_MultiPlayContinueBase
    {
      private bool mOpenMenu;
      private bool mOpenMenuReq;

      protected override void OpenUI(bool roomOwner)
      {
        if (roomOwner)
        {
          this.self.mBattleUI_MultiPlay.StartSelectContinue();
          this.mOpenMenuReq = false;
        }
        else
        {
          this.self.mBattleUI_MultiPlay.ShowWaitContinue();
          this.mOpenMenuReq = true;
        }
      }

      protected override void CloseUI(bool roomOwner, bool decided)
      {
        if (roomOwner)
        {
          this.mOpenMenuReq = false;
        }
        else
        {
          this.self.mBattleUI_MultiPlay.HideWaitContinue();
          if (!decided)
            return;
          this.mOpenMenuReq = false;
        }
      }

      public override void Update(SceneBattle self)
      {
        if (this.mOpenMenu && !this.mOpenMenuReq)
        {
          self.mBattleUI.OnMPSelectContinueWaitingEnd();
          this.mOpenMenu = this.mOpenMenuReq;
        }
        else if (!this.mOpenMenu && this.mOpenMenuReq && UnityEngine.Object.op_Equality((UnityEngine.Object) ((Component) self.mBattleUI).gameObject.transform.Find("quest_lose(Clone)"), (UnityEngine.Object) null))
        {
          self.mBattleUI.OnMPSelectContinueWaitingStart();
          this.mOpenMenu = this.mOpenMenuReq;
        }
        base.Update(self);
      }

      protected override void ExecContinue(List<int> units)
      {
        if (this.mOpenMenu)
          this.self.mBattleUI.OnMPSelectContinueWaitingEnd();
        this.self.MultiPlayLog("[MultiPlayContinue] btlid:" + (object) this.self.Battle.BtlID + " > " + (object) this.BtlID + ", seed:" + (object) this.Seed);
        List<Unit> unitList = this.self.Battle.ContinueStart(this.self.Battle.BtlID, this.Seed);
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (instance.IsHost())
        {
          bool isMultiTower = this.self.Battle.IsMultiTower;
          instance.OpenRoom(isMultiTower, true);
        }
        foreach (Unit unit in unitList)
        {
          TacticsUnitController unitController = this.self.FindUnitController(unit);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          {
            this.self.mTacticsUnits.Remove(unitController);
            GameUtility.DestroyGameObject(((Component) unitController).gameObject);
          }
        }
        this.self.RefreshJumpSpots();
        UnitQueue.Instance.Refresh();
        this.self.GotoState<SceneBattle.State_WaitForLog>();
      }

      protected override void Cancel()
      {
        if (this.mOpenMenu)
          this.self.mBattleUI.OnMPSelectContinueWaitingEnd();
        this.self.mExecDisconnected = true;
        this.self.SendIgnoreMyDisconnect();
        this.self.GotoState_WaitSignal<SceneBattle.State_ExitQuest>();
      }
    }

    private class State_MultiPlaySync : State<SceneBattle>
    {
      private bool mSend;
      private bool mSendResume;

      public override void Begin(SceneBattle self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        this.mSend = false;
        this.mSendResume = false;
        if (instance.GetCurrentRoom().playerCount <= 1)
          return;
        self.mBattleUI_MultiPlay.OnOtherPlayerSyncStart();
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mSend || !this.mSendResume && self.Battle.ResumeState != BattleCore.RESUME_STATE.NONE)
          this.SendSync();
        if (!self.CheckSync() && !self.CheckResumeSync())
          return;
        self.ResetSync();
        if (self.ResumeSuccess)
        {
          self.Battle.StartOrder(false, false);
          self.ResumeSuccess = false;
        }
        if (self.IsExistResume)
        {
          self.Battle.SetResumeWait();
          self.GotoState<SceneBattle.State_SyncResume>();
        }
        else
        {
          self.mBattleUI_MultiPlay.OnOtherPlayerSyncEnd();
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
      }

      public override void End(SceneBattle self)
      {
      }

      private void SendSync()
      {
        if (MonoSingleton<GameManager>.Instance.AudienceMode || (double) this.self.mRestSyncInterval > 0.0)
          return;
        this.self.mRestSyncInterval = this.self.SYNC_INTERVAL;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (this.self.Battle.ResumeState != BattleCore.RESUME_STATE.NONE)
        {
          byte[] sendBinary = this.self.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.SYNC_RESUME, 0, (List<SceneBattle.MultiPlayInput>) null);
          instance.SendRoomMessageBinary(true, sendBinary);
          this.mSendResume = true;
        }
        else
        {
          byte[] sendBinary = this.self.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.SYNC, 0, (List<SceneBattle.MultiPlayInput>) null);
          instance.SendRoomMessageBinary(true, sendBinary);
        }
        if (!this.self.ResumeSuccess)
          return;
        this.self.SendResumeSuccess();
        if (!this.self.IsExistResume)
          return;
        this.self.ResetSync();
        this.self.GotoState<SceneBattle.State_SyncResume>();
      }
    }

    private class State_SyncResume : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.mBattleUI_MultiPlay.OnOtherPlayerSyncStart();
      }

      public override void Update(SceneBattle self)
      {
        if (self.Battle.ResumeState != BattleCore.RESUME_STATE.NONE)
          return;
        self.GotoState<SceneBattle.State_MultiPlaySync>();
      }

      public override void End(SceneBattle self)
      {
      }
    }

    private class State_MapCommandVersus : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.CommandWindow, (UnityEngine.Object) null))
          self.mBattleUI.CommandWindow.OnCommandSelect = new UnitCommands.CommandEvent(this.OnCommandSelect);
        self.ShowAllHPGauges();
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          self.HideUnitCursor(unitController);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.TargetMain, (UnityEngine.Object) null))
          self.mBattleUI.TargetMain.Close();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.TargetSub, (UnityEngine.Object) null))
          self.mBattleUI.TargetSub.Close();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.TargetObjectSub, (UnityEngine.Object) null))
          self.mBattleUI.TargetObjectSub.Close();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.TargetTrickSub, (UnityEngine.Object) null))
          self.mBattleUI.TargetTrickSub.Close();
        self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
        self.VersusMapView = false;
        if (!MonoSingleton<GameManager>.Instance.AudienceMode)
          return;
        self.AudiencePause = false;
      }

      public override void Update(SceneBattle self)
      {
        this.SyncCameraPosition(self);
        if (!self.mBattle.ConditionalUnitEnd(true))
          return;
        self.GotoState<SceneBattle.State_WaitForLog>();
      }

      public override void End(SceneBattle self)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.CommandWindow, (UnityEngine.Object) null))
          return;
        self.mBattleUI.CommandWindow.OnCommandSelect = (UnitCommands.CommandEvent) null;
      }

      private void SyncCameraPosition(SceneBattle self)
      {
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          return;
        GameSettings instance = GameSettings.Instance;
        Transform transform = ((Component) Camera.main).transform;
        transform.position = Vector3.op_Addition(((Component) unitController).transform.position, ((Component) instance.Quest.MoveCamera).transform.position);
        transform.rotation = ((Component) instance.Quest.MoveCamera).transform.rotation;
        self.SetCameraTarget((Component) unitController, true);
        self.mUpdateCameraPosition = true;
      }

      private void OnCommandSelect(UnitCommands.CommandTypes command, object ability)
      {
        this.self.mBattleUI.OnCommandSelect();
        if (command != UnitCommands.CommandTypes.Map)
          return;
        this.self.VersusMapView = true;
        this.self.GotoMapViewMode();
        if (!MonoSingleton<GameManager>.Instance.AudienceMode)
          return;
        this.self.AudiencePause = true;
      }
    }

    private class State_ComfirmFinishbattle : State<SceneBattle>
    {
      private bool mIsFadeIn;

      public override void Begin(SceneBattle self)
      {
        this.mIsFadeIn = false;
        GameUtility.SetDefaultSleepSetting();
        ProgressWindow.Close();
        GameUtility.FadeOut(1f);
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mIsFadeIn)
        {
          if (GameUtility.IsScreenFading)
            return;
          GameUtility.FadeIn(1f);
          self.mBattleUI_MultiPlay.OnAlreadyFinish();
          this.mIsFadeIn = true;
        }
        if (!self.AlreadyEndBattle)
          return;
        self.ForceEndQuest();
      }

      public override void End(SceneBattle self)
      {
      }
    }

    private class State_ForceWinComfirm : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
      }

      public override void Update(SceneBattle self)
      {
        if (!self.Battle.IsVSForceWinComfirm)
          return;
        self.mBattleUI.OnCommandSelect();
        self.Battle.StartOrder(false, false);
        self.GotoState<SceneBattle.State_WaitForLog>();
      }

      public override void End(SceneBattle self)
      {
      }
    }

    private class State_AudienceRetire : State<SceneBattle>
    {
      private bool mClose;

      public override void Begin(SceneBattle self)
      {
        BattleCore.QuestResult questResult = self.CheckAudienceResult();
        string msg = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_AUDIENCE_RETIRE"), questResult != BattleCore.QuestResult.Win ? (object) "1P" : (object) "2P");
        this.mClose = false;
        UIUtility.SystemMessage(string.Empty, msg, new UIUtility.DialogResultEvent(this.ClickEvent), systemModalPriority: 0);
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mClose)
          return;
        self.GotoState<SceneBattle.State_AudienceEnd>();
      }

      public void ClickEvent(GameObject go) => this.mClose = true;
    }

    private class State_AudienceEnd : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        switch (self.CheckAudienceResult())
        {
          case BattleCore.QuestResult.Win:
          case BattleCore.QuestResult.Lose:
            self.mBattleUI.OnAudienceWin();
            break;
          default:
            self.mBattleUI.OnVersusDraw();
            break;
        }
        self.GotoState_WaitSignal<SceneBattle.State_ExitQuest>();
        Network.Abort();
      }
    }

    private class State_AudienceForceEnd : State<SceneBattle>
    {
      private bool mClose;

      public override void Begin(SceneBattle self)
      {
        string msg = LocalizedText.Get("sys.MULTI_VERSUS_AUDIENCE_END");
        this.mClose = false;
        UIUtility.SystemMessage(string.Empty, msg, new UIUtility.DialogResultEvent(this.ClickEvent), systemModalPriority: 0);
        Network.ResetError();
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mClose)
          return;
        self.GotoState<SceneBattle.State_ExitQuest>();
      }

      public void ClickEvent(GameObject go) => this.mClose = true;
    }

    private class MultiPlayer
    {
      private List<int> AutoTurn = new List<int>();

      public MultiPlayer(
        SceneBattle self,
        int playerIndex,
        int playerID,
        bool isSupport = false,
        bool isAutoTreasure = false,
        bool isAutoNoSkill = false,
        int isAuto = 0)
      {
        this.PlayerIndex = playerIndex;
        this.PlayerID = playerID;
        this.NotifyDisconnected = false;
        this.Disconnected = false;
        this.IsSupportAI = isSupport;
        this.IsAutoTreasure = isAutoTreasure;
        this.IsAutoNoSkill = isAutoNoSkill;
        this.IsAutoForDisplay = isAuto != 0 || isSupport;
        self.MultiPlayLog("[PUN] new MultiPlayer playerIndex:" + (object) playerIndex + " playerID:" + (object) playerID);
      }

      public int PlayerIndex { get; private set; }

      public int PlayerID { get; private set; }

      public bool NotifyDisconnected { get; set; }

      public bool Disconnected { get; set; }

      public int RecvInputNum { get; set; }

      public bool FinishLoad { get; set; }

      public bool SyncWait { get; set; }

      public bool SyncResumeWait { get; set; }

      public bool StartBegin { get; set; }

      public bool IsAutoPlayer
      {
        get
        {
          return this.Disconnected || this.IsSupportAI || this.AutoTurn.Contains(SceneBattle.Instance.UnitStartCountTotal);
        }
      }

      public bool IsAutoTreasure { get; private set; }

      public bool IsAutoNoSkill { get; private set; }

      public bool IsSupportAI { get; private set; }

      public bool IsAutoForDisplay { get; private set; }

      public void Begin(SceneBattle self) => this.StartBegin = true;

      public void Update(SceneBattle self)
      {
      }

      public void End(SceneBattle self) => this.StartBegin = false;

      public void SetAutoTurn(int turn) => this.AutoTurn.Add(turn);

      public void ToggleAutoPlay(bool enable) => this.IsAutoForDisplay = enable;
    }

    private class MultiPlayerUnit
    {
      private Unit mUnit;
      private EUnitDirection mDir = EUnitDirection.NegativeX;
      private int mGridX;
      private int mGridY;
      private int mTargetX;
      private int mTargetY;
      private SceneBattle.MultiPlayerUnit.EGridSnap mGridSnap;
      private bool mIsRunning;
      private List<SceneBattle.MultiPlayInput> mRecv = new List<SceneBattle.MultiPlayInput>();
      private int mRecvTurnNum;
      private int mCurrentTurn;
      private float mTurnSec;
      private int mTurnCmdNum;
      private int mTurnCmdDoneNum;
      private GridMap<int> mMoveGrid;
      private bool mBegin;

      public MultiPlayerUnit(
        SceneBattle self,
        int unitID,
        Unit unit,
        SceneBattle.MultiPlayer owner)
      {
        this.Owner = owner;
        this.UnitID = unitID;
        this.mUnit = unit;
        self.MultiPlayLog("[PUN] new MultiPlayerUnit unitID:" + (object) unitID + " name:" + unit.UnitName + " ownerPlayerIndex:" + (object) (owner != null ? owner.PlayerIndex : 0));
      }

      public SceneBattle.MultiPlayer Owner { get; private set; }

      public int UnitID { get; private set; }

      public Unit Unit => this.mUnit;

      public void SetUnit(int index, Unit unit)
      {
        this.UnitID = index;
        this.mUnit = unit;
      }

      public bool IsMoveCompleted(
        SceneBattle self,
        int x,
        int y,
        TacticsUnitController controller)
      {
        if (!this.mBegin)
          return true;
        if (this.mGridSnap != SceneBattle.MultiPlayerUnit.EGridSnap.DONE || this.mIsRunning)
          return false;
        IntVector2 intVector2 = self.CalcCoord(((Component) controller).transform.position);
        return intVector2.x == x && intVector2.y == y;
      }

      public bool IsExistRecvData => this.mRecv.Count > 0;

      public void RecvInput(SceneBattle self, SceneBattle.MultiPlayRecvData data)
      {
        int num = Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(0, data.c != null ? data.c.Length : 0), data.u != null ? data.u.Length : 0), data.s != null ? data.s.Length : 0), data.i != null ? data.i.Length : 0), data.gx != null ? data.gx.Length : 0), data.gy != null ? data.gy.Length : 0), data.d != null ? data.d.Length : 0), data.x != null ? data.x.Length : 0), data.z != null ? data.z.Length : 0), data.r != null ? data.r.Length : 0);
        for (int index = 0; index < num; ++index)
        {
          if (data.c[index] != 0)
          {
            SceneBattle.MultiPlayInput multiPlayInput = new SceneBattle.MultiPlayInput()
            {
              b = data.b,
              t = this.mRecvTurnNum
            };
            multiPlayInput.c = data.c == null || index >= data.c.Length ? multiPlayInput.c : data.c[index];
            multiPlayInput.u = data.u == null || index >= data.u.Length ? multiPlayInput.u : data.u[index];
            multiPlayInput.s = data.s == null || index >= data.s.Length ? multiPlayInput.s : data.s[index];
            multiPlayInput.i = data.i == null || index >= data.i.Length ? multiPlayInput.i : data.i[index];
            multiPlayInput.gx = data.gx == null || index >= data.gx.Length ? multiPlayInput.gx : data.gx[index];
            multiPlayInput.gy = data.gy == null || index >= data.gy.Length ? multiPlayInput.gy : data.gy[index];
            multiPlayInput.ul = data.ul == null || index >= data.ul.Length ? multiPlayInput.ul : data.ul[index];
            multiPlayInput.d = data.d == null || index >= data.d.Length ? multiPlayInput.d : data.d[index];
            multiPlayInput.x = data.x == null || index >= data.x.Length ? multiPlayInput.x : data.x[index];
            multiPlayInput.z = data.z == null || index >= data.z.Length ? multiPlayInput.z : data.z[index];
            multiPlayInput.r = data.r == null || index >= data.r.Length ? multiPlayInput.r : data.r[index];
            if (!multiPlayInput.IsValid(self))
            {
              DebugUtility.LogError("[PUN] illegal input recv.");
            }
            else
            {
              if (multiPlayInput.c == 4)
                self.MultiPlayLog("[PUN] recv MOVE_END");
              else if (multiPlayInput.c == 6)
                self.MultiPlayLog("[PUN] recv ENTRY_BATTLE");
              else if (multiPlayInput.c == 7)
                self.MultiPlayLog("[PUN] recv GRID_EVENT");
              else if (multiPlayInput.c == 8)
                self.MultiPlayLog("[PUN] recv UNIT_END");
              else if (multiPlayInput.c == 9)
                self.MultiPlayLog("[PUN] recv TIME_LIMIT");
              this.mRecv.Add(multiPlayInput);
            }
          }
        }
        ++this.mRecvTurnNum;
        self.MultiPlayLog("[PUN]RecvInput from unitID:" + (object) this.UnitID + " " + (object) this.mRecvTurnNum + " sq:" + (object) data.sq + " b:" + (object) data.b + "/" + (object) self.UnitStartCountTotal);
      }

      public void Begin(SceneBattle self)
      {
        if (this.mBegin)
          return;
        this.mBegin = true;
        this.mRecvTurnNum = 0;
        this.mCurrentTurn = 0;
        this.mTurnSec = 0.0f;
        this.mTurnCmdNum = -1;
        this.mTurnCmdDoneNum = 0;
        this.mRecv.RemoveAll((Predicate<SceneBattle.MultiPlayInput>) (r => r.b < self.mUnitStartCountTotal));
        TacticsUnitController unitController = self.FindUnitController(this.mUnit);
        self.MultiPlayLog("[PUN]Begin MultiPlayer unitID: " + (object) this.UnitID + " name:" + this.mUnit.UnitName + " ux:" + (object) this.mUnit.x + " uy:" + (object) this.mUnit.y + " dir:" + (object) this.mUnit.Direction + " sx:" + (object) this.mUnit.startX + " sy:" + (object) this.mUnit.startY);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null) || this.mUnit.IsDead)
          return;
        unitController.AutoUpdateRotation = false;
        this.mGridSnap = SceneBattle.MultiPlayerUnit.EGridSnap.DONE;
        this.mIsRunning = false;
        this.mGridX = this.mTargetX = this.mUnit.x;
        this.mGridY = this.mTargetY = this.mUnit.y;
        unitController.WalkableField = self.CreateCurrentAccessMap();
        if (!self.Battle.IsMultiVersus)
          unitController.ShowOwnerIndexUI(true);
        this.mMoveGrid = self.CreateCurrentAccessMap();
      }

      public void End(SceneBattle self)
      {
        if (!this.mBegin)
          return;
        this.mBegin = false;
        TacticsUnitController unitController = self.FindUnitController(this.mUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          if (this.mIsRunning)
          {
            this.mIsRunning = false;
            unitController.StopRunning();
          }
          ((Component) unitController).transform.position = self.CalcGridCenter(this.mUnit.x, this.mUnit.y);
          unitController.AutoUpdateRotation = true;
          unitController.WalkableField = (GridMap<int>) null;
          unitController.ShowOwnerIndexUI(false);
        }
        self.MultiPlayLog("MultiPlayerEnd. ux:" + (object) this.mUnit.x + " uy:" + (object) this.mUnit.y + " gx:" + (object) this.mGridX + " gy:" + (object) this.mGridY);
      }

      private Vector3 GetPosition3D(SceneBattle self, float x, float z)
      {
        Vector3 position;
        // ISSUE: explicit constructor call
        ((Vector3) ref position).\u002Ector(x, 0.0f, z);
        IntVector2 intVector2 = self.CalcCoord(position);
        Vector3 vector3 = self.CalcGridCenter(intVector2.x, intVector2.y);
        position.y = vector3.y;
        return position;
      }

      private bool CheckMoveable(int x, int y)
      {
        return this.mMoveGrid == null || this.mMoveGrid.get(x, y) >= 0;
      }

      public void Update(SceneBattle self)
      {
        if (!this.mBegin)
          return;
        TacticsUnitController unitController = self.FindUnitController(this.mUnit);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null) || this.mUnit.IsDead || !self.IsInState<SceneBattle.State_MapCommandMultiPlay>() && !self.IsInState<SceneBattle.State_MapCommandVersus>() && !self.IsInState<SceneBattle.State_SelectTargetV2>())
          return;
        unitController.AutoUpdateRotation = false;
        if (unitController.IsPlayingFieldAction)
          return;
        GameManager instance1 = MonoSingleton<GameManager>.Instance;
        bool audienceMode = instance1.AudienceMode;
        if (this.mRecv.Count <= 0 && !audienceMode && !instance1.IsVSCpuBattle || audienceMode && (this.Owner.Disconnected || this.Owner.IsAutoPlayer) && this.mRecv.Count <= 0 || instance1.IsVSCpuBattle && unitController.Unit.OwnerPlayerIndex == 2)
        {
          if (this.Owner != null && this.IsMoveCompleted(self, this.mGridX, this.mGridY, unitController))
          {
            MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
            List<MyPhoton.MyPlayer> roomPlayerList = instance2.GetRoomPlayerList();
            MyPhoton.MyPlayer player = instance2.FindPlayer(roomPlayerList, this.Owner.PlayerID, this.Owner.PlayerIndex);
            if (player == null || !player.start && this.Owner.Disconnected || this.Owner.IsAutoPlayer)
            {
              ((Component) unitController).transform.position = self.CalcGridCenter(this.mUnit.x, this.mUnit.y);
              unitController.CancelAction();
              ((Component) unitController).transform.rotation = this.mUnit.Direction.ToRotation();
              unitController.AutoUpdateRotation = true;
              self.Battle.EntryBattleMultiPlayTimeUp = true;
              self.Battle.MultiPlayDisconnectAutoBattle = true;
              self.GotoMapCommand();
              DebugUtility.LogWarning("[PUN] detect player disconnected");
            }
          }
        }
        else if (this.Owner != null)
        {
          this.mTurnSec += Time.deltaTime;
          if (this.mTurnCmdNum < 0 && this.mRecv.Count > 0)
          {
            this.mTurnCmdNum = 0;
            int t = this.mRecv[0].t;
            while (this.mTurnCmdNum < this.mRecv.Count && this.mRecv[this.mTurnCmdNum].t == t)
              ++this.mTurnCmdNum;
            self.MultiPlayLog("[PUN] start send turn:" + (object) t + "/" + (object) this.mCurrentTurn + " cmdNum:" + (object) this.mTurnCmdNum + " recvTurnNum:" + (object) this.mRecvTurnNum + " recvCount:" + (object) this.mRecv.Count);
          }
          int num1 = Math.Min(this.mTurnCmdNum, (int) ((double) this.mTurnSec * (double) this.mTurnCmdNum) + 1);
          while (this.mTurnCmdDoneNum < num1 && this.mRecv.Count > 0)
          {
            int num2 = 0;
            SceneBattle.MultiPlayInput multiPlayInput = this.mRecv[0];
            if (multiPlayInput.c == 4 || multiPlayInput.c == 3 || multiPlayInput.c == 7 || multiPlayInput.c == 8)
            {
              if (multiPlayInput.c != 4 && multiPlayInput.c != 8 || !unitController.isMoving)
              {
                IntVector2 intVector2 = self.CalcCoord(((Component) unitController).transform.position);
                if (this.mGridX != intVector2.x || this.mGridY != intVector2.y)
                {
                  this.mGridX = intVector2.x;
                  this.mGridY = intVector2.y;
                }
                this.mTargetX = multiPlayInput.gx;
                this.mTargetY = multiPlayInput.gy;
                if (this.mTargetX != this.mGridX || this.mTargetY != this.mGridY)
                {
                  if (multiPlayInput.c == 3)
                  {
                    this.mDir = (EUnitDirection) multiPlayInput.d;
                    this.mRecv.RemoveAt(0);
                    break;
                  }
                  break;
                }
              }
              else
                break;
            }
            if (multiPlayInput.c != 0)
            {
              if (multiPlayInput.c == 3)
              {
                this.mDir = (EUnitDirection) multiPlayInput.d;
                self.MultiPlayLog("recv GRID_XY:" + (object) this.mGridX + " " + (object) this.mGridY);
              }
              else if (multiPlayInput.c != 1)
              {
                if (multiPlayInput.c == 4)
                {
                  self.MultiPlayLog("recv MOVE_END:" + (object) this.mGridX + " " + (object) this.mGridY);
                  if (this.mGridX != multiPlayInput.gx || this.mGridY != multiPlayInput.gy)
                    DebugUtility.LogWarning("move pos not match gx:" + (object) this.mGridX + " gy:" + (object) this.mGridY + " tx:" + (object) multiPlayInput.gx + " ty:" + (object) multiPlayInput.gy);
                  self.Battle.MoveMultiPlayer(this.mUnit, this.mGridX, this.mGridY, this.mDir);
                  if (!this.CheckMoveable(this.mGridX, this.mGridY))
                    self.SendCheat(SceneBattle.CHEAT_TYPE.MOVE, this.Owner.PlayerIndex);
                }
                else if (multiPlayInput.c == 5)
                {
                  this.mGridX = this.mTargetX = this.mUnit.startX;
                  this.mGridY = this.mTargetY = this.mUnit.startY;
                  this.mDir = this.mUnit.startDir;
                  self.ResetMultiPlayerTransform(this.mUnit);
                  self.MultiPlayLog("[PUN] recv MOVE_CANCEL (" + (object) this.mGridX + "," + (object) this.mGridY + ")" + (object) this.mDir);
                }
                else if (multiPlayInput.c != 2)
                {
                  if (multiPlayInput.c == 6)
                  {
                    EBattleCommand d = (EBattleCommand) multiPlayInput.d;
                    if (d == EBattleCommand.Wait)
                      self.Battle.MapCommandEnd(this.mUnit);
                    else if (!unitController.isMoving)
                    {
                      Unit allUnit = multiPlayInput.u >= 0 ? self.Battle.AllUnits[multiPlayInput.u] : (Unit) null;
                      SkillData skillData = this.mUnit.GetSkillData(multiPlayInput.s);
                      if (skillData != null && !this.mUnit.CheckEnableUseSkill(skillData, true))
                        self.SendCheat(SceneBattle.CHEAT_TYPE.MP, this.Owner.PlayerIndex);
                      ItemData inventoryByItemId = MonoSingleton<GameManager>.Instance.Player.FindInventoryByItemID(multiPlayInput.i);
                      int gx = multiPlayInput.gx;
                      int gy = multiPlayInput.gy;
                      bool unitLockTarget = multiPlayInput.ul != 0;
                      if (!this.IsMoveCompleted(self, this.mUnit.x, this.mUnit.y, unitController))
                      {
                        self.MultiPlayLog("[PUN]waiting move completed for ENTRY_BATTLE... SNAP:" + (object) this.mGridSnap + " RUN:" + (object) this.mIsRunning + " ux:" + (object) this.mUnit.x + " uy:" + (object) this.mUnit.y);
                        IntVector2 intVector2 = self.CalcCoord(((Component) unitController).transform.position);
                        this.mGridX = intVector2.x;
                        this.mGridY = intVector2.y;
                        break;
                      }
                      if (skillData != null && !self.Battle.CreateSelectGridMap(this.mUnit, this.mUnit.x, this.mUnit.y, skillData).get(gx, gy))
                        self.SendCheat(SceneBattle.CHEAT_TYPE.RANGE, this.Owner.PlayerIndex);
                      self.MultiPlayLog("[PUN]MultiPlayerUnit ENTRY_BATTLE");
                      self.HideAllHPGauges();
                      self.HideAllUnitOwnerIndex();
                      self.Battle.EntryBattleMultiPlay(d, this.mUnit, allUnit, skillData, inventoryByItemId, gx, gy, unitLockTarget);
                      num2 = 1;
                      this.mDir = this.mUnit.Direction;
                      this.mGridX = this.mTargetX = this.mUnit.x;
                      this.mGridY = this.mTargetY = this.mUnit.y;
                    }
                    else
                      break;
                  }
                  else if (multiPlayInput.c == 7)
                  {
                    if (this.mGridX != multiPlayInput.gx || this.mGridY != multiPlayInput.gy)
                      DebugUtility.LogWarning("GRID_EVENT move pos not match gx:" + (object) this.mGridX + " gy:" + (object) this.mGridY + " tx:" + (object) multiPlayInput.gx + " ty:" + (object) multiPlayInput.gy);
                    if (!this.IsMoveCompleted(self, this.mUnit.x, this.mUnit.y, unitController))
                    {
                      self.MultiPlayLog("[PUN]waiting move completed for GRID_EVENT...begin:" + (object) this.mBegin + " run:" + (object) this.mIsRunning + " snap:" + (object) this.mGridSnap + " ux:" + (object) this.mUnit.x + " uy:" + (object) this.mUnit.y);
                      break;
                    }
                    self.MultiPlayLog("[PUN]MultiPlayerUnit GRID_EVENT");
                    self.Battle.ExecuteEventTriggerOnGrid(this.mUnit, EEventTrigger.ExecuteOnGrid);
                    num2 = 1;
                  }
                  else if (multiPlayInput.c == 8)
                  {
                    if (this.mGridX != multiPlayInput.gx || this.mGridY != multiPlayInput.gy)
                    {
                      DebugUtility.LogWarning("UNIT_END move pos not match gx:" + (object) this.mGridX + " gy:" + (object) this.mGridY + " tx:" + (object) multiPlayInput.gx + " ty:" + (object) multiPlayInput.gy);
                      this.mGridX = multiPlayInput.gx;
                      this.mGridY = multiPlayInput.gy;
                      break;
                    }
                    if (!this.IsMoveCompleted(self, this.mUnit.x, this.mUnit.y, unitController))
                    {
                      self.MultiPlayLog("[PUN]waiting move completed for UNIT_END...begin:" + (object) this.mBegin + " run:" + (object) this.mIsRunning + " snap:" + (object) this.mGridSnap + " ux:" + (object) this.mUnit.x + " uy:" + (object) this.mUnit.y);
                      break;
                    }
                    self.MultiPlayLog("[PUN]MultiPlayerUnit UNIT_END");
                    self.Battle.CommandWait((EUnitDirection) multiPlayInput.d);
                    num2 = 1;
                  }
                  else if (multiPlayInput.c == 9)
                  {
                    unitController.StopRunning();
                    this.mUnit.x = this.mGridX = this.mTargetX = multiPlayInput.gx;
                    this.mUnit.y = this.mGridY = this.mTargetY = multiPlayInput.gy;
                    ((Component) unitController).transform.position = self.CalcGridCenter(multiPlayInput.gx, multiPlayInput.gy);
                    if (!this.IsMoveCompleted(self, this.mUnit.x, this.mUnit.y, unitController))
                    {
                      self.MultiPlayLog("[PUN]waiting move completed for UNIT_TIME_LIMIT...begin:" + (object) this.mBegin + " run:" + (object) this.mIsRunning + " snap:" + (object) this.mGridSnap + " ux:" + (object) this.mUnit.x + " uy:" + (object) this.mUnit.y);
                      break;
                    }
                    this.mUnit.Direction = (EUnitDirection) multiPlayInput.d;
                    num2 = 2;
                    self.MultiPlayLog("[PUN]UnitTimeUp!");
                  }
                  else if (multiPlayInput.c == 10)
                  {
                    this.mUnit.x = this.mGridX = multiPlayInput.gx;
                    this.mUnit.y = this.mGridY = multiPlayInput.gy;
                    this.mUnit.Direction = this.mDir = (EUnitDirection) multiPlayInput.d;
                    self.MultiPlayLog("recv UNIT_XYDIR:" + (object) this.mGridX + " " + (object) this.mGridY + " " + (object) this.mDir);
                  }
                }
              }
            }
            this.mRecv.RemoveAt(0);
            ++this.mTurnCmdDoneNum;
            switch (num2)
            {
              case 1:
                unitController.AutoUpdateRotation = true;
                self.GotoState<SceneBattle.State_WaitForLog>();
                goto label_70;
              case 2:
                unitController.AutoUpdateRotation = true;
                self.Battle.EntryBattleMultiPlayTimeUp = true;
                self.GotoMapCommand();
                goto label_70;
              default:
                continue;
            }
          }
label_70:
          if (this.mTurnCmdDoneNum >= this.mTurnCmdNum)
          {
            self.MultiPlayLog("[PUN] done send turn:" + (object) this.mCurrentTurn + " cmdNum:" + (object) this.mTurnCmdNum + " recvTurnNum:" + (object) this.mRecvTurnNum + " recvCount:" + (object) this.mRecv.Count);
            ++this.mCurrentTurn;
            this.mTurnSec = 0.0f;
            this.mTurnCmdDoneNum = 0;
            this.mTurnCmdNum = -1;
          }
        }
        if (unitController.AutoUpdateRotation)
          return;
        this.Move(self, this.mTargetX, this.mTargetY);
      }

      private void Move(SceneBattle self, int tx, int ty)
      {
        if (tx == this.mGridX && ty == this.mGridY)
          return;
        this.mMoveGrid = self.CreateCurrentAccessMap();
        TacticsUnitController unitController = self.FindUnitController(this.mUnit);
        Vector3[] path = self.FindPath(this.mUnit, this.mGridX, this.mGridY, tx, ty, this.mUnit.DisableMoveGridHeight, this.mMoveGrid);
        if (path != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          double num = (double) unitController.StartMove(path);
          this.mGridX = tx;
          this.mGridY = ty;
        }
        else
        {
          this.mGridX = tx;
          this.mGridY = ty;
        }
      }

      public void UpdateSkip(SceneBattle self)
      {
        if (!this.mBegin)
          return;
        TacticsUnitController unitController = self.FindUnitController(this.mUnit);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null) || this.mUnit.IsDead)
          return;
        unitController.AutoUpdateRotation = false;
        if (this.mRecv.Count <= 0)
        {
          if (!this.Owner.Disconnected && !this.Owner.IsAutoPlayer && this.mUnit.IsControl)
            return;
          do
            ;
          while (self.Battle.UpdateMapAI(false));
          ((Component) unitController).transform.position = self.CalcGridCenter(this.mUnit.x, this.mUnit.y);
        }
        else
        {
          if (this.Owner == null)
            return;
          if (!this.mUnit.IsControl)
          {
            do
              ;
            while (self.Battle.UpdateMapAI(false) && !this.mUnit.IsControl);
            ((Component) unitController).transform.position = self.CalcGridCenter(this.mUnit.x, this.mUnit.y);
          }
          while (this.mRecv.Count > 0)
          {
            SceneBattle.MultiPlayInput multiPlayInput = this.mRecv[0];
            if (multiPlayInput.c == 4 || multiPlayInput.c == 3 || multiPlayInput.c == 7 || multiPlayInput.c == 8)
            {
              this.mUnit.x = this.mGridX = this.mTargetX = multiPlayInput.gx;
              this.mUnit.y = this.mGridY = this.mTargetY = multiPlayInput.gy;
              ((Component) unitController).transform.position = self.CalcGridCenter(multiPlayInput.gx, multiPlayInput.gy);
            }
            if (multiPlayInput.c == 3)
              this.mDir = (EUnitDirection) multiPlayInput.d;
            else if (multiPlayInput.c == 4)
              self.Battle.MoveMultiPlayer(this.mUnit, this.mGridX, this.mGridY, this.mDir);
            else if (multiPlayInput.c == 5)
            {
              this.mGridX = this.mTargetX = this.mUnit.startX;
              this.mGridY = this.mTargetY = this.mUnit.startY;
              this.mDir = this.mUnit.startDir;
              self.ResetMultiPlayerTransform(this.mUnit);
            }
            else if (multiPlayInput.c == 6)
            {
              EBattleCommand d = (EBattleCommand) multiPlayInput.d;
              if (d == EBattleCommand.Wait)
              {
                self.Battle.MapCommandEnd(this.mUnit);
              }
              else
              {
                Unit allUnit = multiPlayInput.u >= 0 ? self.Battle.AllUnits[multiPlayInput.u] : (Unit) null;
                SkillData skillData = this.mUnit.GetSkillData(multiPlayInput.s);
                ItemData inventoryByItemId = MonoSingleton<GameManager>.Instance.Player.FindInventoryByItemID(multiPlayInput.i);
                int gx = multiPlayInput.gx;
                int gy = multiPlayInput.gy;
                bool unitLockTarget = multiPlayInput.ul != 0;
                self.Battle.EntryBattleMultiPlay(d, this.mUnit, allUnit, skillData, inventoryByItemId, gx, gy, unitLockTarget);
                if (skillData != null)
                {
                  if (skillData.IsSetBreakObjSkill())
                  {
                    Unit gimmickAtGrid = self.Battle.FindGimmickAtGrid(gx, gy);
                    if (gimmickAtGrid != null && gimmickAtGrid.IsBreakObj)
                      self.ReqCreateBreakObjUcLists.Add(new SceneBattle.ReqCreateBreakObjUc(skillData, gimmickAtGrid));
                  }
                  else if (skillData.IsDynamicTransformSkill())
                  {
                    Unit unitAtGrid = self.Battle.FindUnitAtGrid(gx, gy);
                    if (unitAtGrid != null && unitAtGrid.IsUnitFlag(EUnitFlag.IsDynamicTransform))
                      self.ReqCreateDtuUcLists.Add(new SceneBattle.ReqCreateDtuUc(skillData, unitAtGrid));
                  }
                }
                this.mDir = this.mUnit.Direction;
                this.mGridX = this.mTargetX = this.mUnit.x;
                this.mGridY = this.mTargetY = this.mUnit.y;
              }
            }
            else if (multiPlayInput.c == 7)
              self.Battle.ExecuteEventTriggerOnGrid(this.mUnit, EEventTrigger.ExecuteOnGrid);
            else if (multiPlayInput.c == 8)
              this.mUnit.Direction = (EUnitDirection) multiPlayInput.d;
            else if (multiPlayInput.c == 9)
            {
              unitController.StopRunning();
              this.mGridX = this.mTargetX = multiPlayInput.gx;
              this.mGridY = this.mTargetY = multiPlayInput.gy;
              ((Component) unitController).transform.position = self.CalcGridCenter(multiPlayInput.gx, multiPlayInput.gy);
              this.mUnit.Direction = (EUnitDirection) multiPlayInput.d;
            }
            else if (multiPlayInput.c == 10)
            {
              this.mUnit.x = this.mGridX = multiPlayInput.gx;
              this.mUnit.y = this.mGridY = multiPlayInput.gy;
              this.mUnit.Direction = this.mDir = (EUnitDirection) multiPlayInput.d;
            }
            this.mRecv.RemoveAt(0);
          }
          if (!this.Owner.Disconnected && !this.Owner.IsAutoPlayer && this.mUnit.IsControl)
            return;
          do
            ;
          while (self.Battle.UpdateMapAI(false));
          ((Component) unitController).transform.position = self.CalcGridCenter(this.mUnit.x, this.mUnit.y);
        }
      }

      private enum EGridSnap
      {
        NOP,
        ACTIVE,
        DONE,
        NUM,
      }
    }

    public enum TargetActionTypes
    {
      None,
      Attack,
      Skill,
      Heal,
    }

    private class State_WaitSignal<T> : State<SceneBattle> where T : State<SceneBattle>, new()
    {
      public override void Begin(SceneBattle self)
      {
        self.mIsWaitingForBattleSignal = true;
        this.Update(self);
      }

      public override void Update(SceneBattle self)
      {
        if (self.mUISignal)
          return;
        self.mIsWaitingForBattleSignal = false;
        self.GotoState<T>();
      }
    }

    private class State_LoadMapV2 : State<SceneBattle>
    {
      private bool mReady;

      public override void Begin(SceneBattle self)
      {
        while (TacticsSceneSettings.SceneCount > 0)
          TacticsSceneSettings.PopFirstScene();
        self.IsLoaded = true;
        self.mUpdateCameraPosition = true;
        Debug.Log((object) ("======= LOAD MAP START (" + (object) self.Battle.MapIndex + ")"));
        self.BeginLoadMapAsync();
        self.SetUnitUiHeight(self.mBattle.CurrentUnit);
      }

      public override void Update(SceneBattle self)
      {
        if (self.IsCameraMoving || !self.mMapReady || ProgressWindow.ShouldKeepVisible)
          return;
        if (!this.mReady)
        {
          GameManager instance1 = MonoSingleton<GameManager>.Instance;
          if (self.Battle.IsMultiPlay && !instance1.AudienceMode)
          {
            if (instance1.IsVSCpuBattle)
            {
              self.Battle.MultiFinishLoad = true;
            }
            else
            {
              if (!self.Battle.MultiFinishLoad)
              {
                MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
                self.Battle.MultiFinishLoad = self.SendFinishLoad();
                if (!instance2.IsConnected())
                  self.Battle.MultiFinishLoad = true;
              }
              if (!self.Battle.SyncStart)
                return;
            }
          }
          else if (instance1.AudienceMode && !self.Battle.MultiFinishLoad)
          {
            if (self.AudienceSkip)
            {
              self.SkipLog();
              return;
            }
            self.AudienceSkip = true;
            instance1.AudienceManager.FinishLoad();
            instance1.AudienceManager.SkipMode = true;
            return;
          }
          if (self.mTutorialTriggers != null)
          {
            for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
              self.mTutorialTriggers[index].OnMapStart();
          }
          this.mReady = true;
          GameUtility.SetDefaultSleepSetting();
          GameUtility.FadeOut(1f);
        }
        if (GameUtility.IsScreenFading)
          return;
        ProgressWindow.SetDestroyDelay(0.0f);
        ProgressWindow.Close();
        AssetManager.UnloadUnusedAssets();
        GlobalEvent.Invoke("BATTLE_MAP_LOADED", (object) null);
        GlobalEvent.Invoke("CLOSE_UNIT_TOOLTIP", (object) null);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) (self.mEventSequence = self.mEventScript.OnPostMapLoad()), (UnityEngine.Object) null))
        {
          self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_StartEvent>>();
        }
        else
        {
          GameUtility.FadeIn(1f);
          self.GotoState<SceneBattle.State_WaitFade<SceneBattle.State_StartEvent>>();
        }
      }
    }

    private class State_StartEvent : State<SceneBattle>
    {
      private EventScript.Sequence mSequence;

      public override void Begin(SceneBattle self)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null))
        {
          this.Finish();
        }
        else
        {
          this.mSequence = self.mEventScript.OnStart(is_auto_forward: true);
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSequence, (UnityEngine.Object) null))
            this.Finish();
          else
            self.mUpdateCameraPosition = false;
        }
      }

      public override void Update(SceneBattle self)
      {
        if (this.mSequence.IsPlaying)
          return;
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mSequence);
        self.ResetCameraTarget();
        this.Finish();
      }

      private void Finish()
      {
        if (this.self.IsPlayingPreCalcResultQuest)
          this.self.mBattleUI.OnQuestStart_Arena();
        else if (this.self.Battle.IsMultiVersus)
        {
          if (this.self.Battle.IsRankMatch)
            this.self.mBattleUI.OnQuestStart_RankMatch();
          else
            this.self.mBattleUI.OnQuestStart_VS();
        }
        else if (string.IsNullOrEmpty(this.self.mCurrentQuest.cond))
          this.self.mBattleUI.OnQuestStart_Short();
        else if (this.self.Battle.IsRaidQuest)
          this.self.mBattleUI.OnQuestStart_Raid();
        else if (this.self.Battle.IsGuildRaidQuest)
        {
          if (GlobalVars.CurrentBattleType.Get() == GuildRaidBattleType.Main)
            this.self.mBattleUI.OnQuestStart_Raid();
          else
            this.self.mBattleUI.OnQuestStart_GuildRaidMock();
        }
        else if (this.self.Battle.IsGenesisBoss)
          this.self.mBattleUI.OnQuestStart_GenesisBoss();
        else
          this.self.mBattleUI.OnQuestStart();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI_MultiPlay, (UnityEngine.Object) null))
          this.self.mBattleUI_MultiPlay.OnMapStart();
        this.self.GotoState_WaitSignal<SceneBattle.State_MapStartV2>();
      }
    }

    private class State_MapStartV2 : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.QuestStart = true;
        if (self.CurrentQuest.CheckAllowedAutoBattle() && self.Battle.RequestAutoBattle)
          GameUtility.SetNeverSleep();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) && self.Battle.RequestAutoBattle && MonoSingleton<GameManager>.Instance.Player.IsAutoRepeatQuestMeasuring)
          MonoSingleton<GameManager>.Instance.StartQuestPlayTime();
        BattleSpeedController.SetUp();
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_PreUnitStart : State<SceneBattle>
    {
      private IEnumerator m_Task;

      [DebuggerHidden]
      private IEnumerator Task_Begin(SceneBattle self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_PreUnitStart.\u003CTask_Begin\u003Ec__Iterator0()
        {
          self = self
        };
      }

      public override void Begin(SceneBattle self)
      {
        self.RemoveLog();
        this.m_Task = this.Task_Begin(self);
      }

      public override void Update(SceneBattle self)
      {
        if (this.m_Task != null)
        {
          if (this.m_Task.MoveNext())
            return;
          this.m_Task = (IEnumerator) null;
        }
        if (self.Battle.IsUnitRestart)
        {
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
        else
        {
          for (int index = 0; index < self.mTacticsUnits.Count; ++index)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mTacticsUnits[index], (UnityEngine.Object) null) && self.mTacticsUnits[index].IsHPGaugeChanging)
              return;
          }
          for (int index = 0; index < self.mTacticsUnits.Count; ++index)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mTacticsUnits[index], (UnityEngine.Object) null))
              self.mTacticsUnits[index].ResetHPGauge();
          }
          TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null))
            self.mEventSequence = self.mEventScript.OnUnitStart(unitController);
          self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_UnitStartV2>>();
        }
      }
    }

    private class State_UnitStartV2 : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        self.mAutoActivateGimmick = false;
        if (self.mTutorialTriggers != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
            self.mTutorialTriggers[index].OnUnitStart(self.mBattle.CurrentUnit, unitController.Unit.TurnCount);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          self.SetPrioritizedUnit(unitController);
          Unit currentUnit = self.Battle.CurrentUnit;
          if (currentUnit != null && !currentUnit.IsDead)
          {
            self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
            self.InterpCameraTarget((Component) unitController);
          }
          else
            Debug.Log((object) "[PUN]I'm dead or null...");
          self.mCurrentUnitStartX = unitController.Unit.startX;
          self.mCurrentUnitStartY = unitController.Unit.startY;
        }
        self.ToggleRenkeiAura(true);
        if (!self.Battle.IsMultiPlay)
          return;
        self.BeginMultiPlayer();
      }

      public override void Update(SceneBattle self)
      {
        Unit currentUnit = self.mBattle.CurrentUnit;
        if (currentUnit != null && !currentUnit.IsDead)
        {
          if (self.IsCameraMoving)
            return;
          TacticsUnitController unitController1 = self.FindUnitController(currentUnit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController1, (UnityEngine.Object) null))
            unitController1.ResetRotation();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.CommandWindow, (UnityEngine.Object) null))
          {
            List<AbilityData> abilityDataList1 = new List<AbilityData>((IEnumerable<AbilityData>) currentUnit.BattleAbilitys);
            List<AbilityData> abilityDataList2 = new List<AbilityData>();
            for (int index = 0; index < abilityDataList1.Count; ++index)
            {
              if (abilityDataList1[index].IsNoneCategory && abilityDataList1[index].AbilityType == EAbilityType.Active)
              {
                abilityDataList2.Add(abilityDataList1[index]);
                abilityDataList1.RemoveAt(index--);
              }
              else if (abilityDataList1[index].AbilityType != EAbilityType.Active)
                abilityDataList1.RemoveAt(index--);
            }
            if (abilityDataList2.Count > 0)
              abilityDataList1.Add((AbilityData) AbilityData.ToMix(abilityDataList2.ToArray(), self.mBattleUI.CommandWindow.OtherSkillName, self.mBattleUI.CommandWindow.OtherSkillIconName));
            self.mBattleUI.CommandWindow.SetAbilities(abilityDataList1.ToArray(), currentUnit);
            self.ReflectUnitChgButton(currentUnit);
            bool is_no_use_item = currentUnit.IsUnitFlag(EUnitFlag.IsDynamicTransform) && currentUnit.DtuParam != null && currentUnit.DtuParam.IsNoItems;
            self.mBattleUI.CommandWindow.EnableUseItem(is_no_use_item);
          }
          if (self.mTutorialTriggers != null)
          {
            TacticsUnitController unitController2 = self.FindUnitController(self.mBattle.CurrentUnit);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController2, (UnityEngine.Object) null))
            {
              for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
                self.mTutorialTriggers[index].OnFinishCameraUnitFocus(self.mBattle.CurrentUnit, unitController2.Unit.TurnCount);
            }
          }
        }
        for (int index = 0; index < self.mTacticsUnits.Count; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mTacticsUnits[index], (UnityEngine.Object) null))
            self.mTacticsUnits[index].UpdateBadStatus();
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null))
        {
          TacticsUnitController unitController = self.FindUnitController(currentUnit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
            self.mEventSequence = self.mEventScript.OnUnitTurnStart(unitController, self.mIsFirstPlay);
        }
        self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_WaitForLog>>();
      }
    }

    private class State_WaitFade<T> : State<SceneBattle> where T : State<SceneBattle>, new()
    {
      public override void Begin(SceneBattle self) => this.Update(self);

      public override void Update(SceneBattle self)
      {
        if (GameUtility.IsScreenFading)
          return;
        self.GotoState<T>();
      }
    }

    private class State_WaitEvent<T> : State<SceneBattle> where T : State<SceneBattle>, new()
    {
      public override void Begin(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI, (UnityEngine.Object) null))
            self.mBattleUI.Hide();
          self.mUpdateCameraPosition = false;
          self.SetPrioritizedUnit((TacticsUnitController) null);
          MonoSingleton<GameManager>.Instance.EnableAnimationFrameSkipping = false;
          self.HideAllHPGauges();
          self.HideAllUnitOwnerIndex();
          for (int index = 0; index < self.mTacticsUnits.Count; ++index)
            self.mTacticsUnits[index].AutoUpdateRotation = false;
        }
        this.Update(self);
      }

      public override void End(SceneBattle self)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI, (UnityEngine.Object) null))
          return;
        self.mBattleUI.Show();
      }

      public override void Update(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null) && self.IsCameraMoving || !UnityEngine.Object.op_Equality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null) && self.mEventSequence.IsPlaying)
          return;
        for (int index = 0; index < self.mTacticsUnits.Count; ++index)
          self.mTacticsUnits[index].AutoUpdateRotation = true;
        self.ResetCameraTarget();
        self.mUpdateCameraPosition = true;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
        {
          UnityEngine.Object.DestroyImmediate((UnityEngine.Object) ((Component) self.mEventSequence).gameObject);
          self.mEventSequence = (EventScript.Sequence) null;
        }
        MonoSingleton<GameManager>.Instance.EnableAnimationFrameSkipping = true;
        self.GotoState<T>();
      }
    }

    private class State_MapCommandV2 : State<SceneBattle>
    {
      private Unit mQueuedTarget;
      private UnitCommands.CommandTypes mQueuedCommand;
      private object mSelectedAbility;
      private bool mShouldRefreshCommands;
      private bool mMoved;
      private int mStartX;
      private int mStartY;
      private Unit mCurrentUnit;
      private int mHotTargets;
      private TacticsUnitController mCurrentController;

      public override void Begin(SceneBattle self)
      {
        this.mCurrentUnit = self.mBattle.CurrentUnit;
        this.mStartX = this.mCurrentUnit.x;
        this.mStartY = this.mCurrentUnit.y;
        self.InterpCameraTarget((Component) self.FindUnitController(this.mCurrentUnit));
        self.mTouchController.IgnoreCurrentTouch();
        this.mCurrentController = self.FindUnitController(this.mCurrentUnit);
        this.mCurrentController.ResetHPGauge();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.CommandWindow, (UnityEngine.Object) null))
        {
          self.mBattleUI.CommandWindow.OnCommandSelect = new UnitCommands.CommandEvent(this.OnCommandSelect);
          self.mBattleUI.CommandWindow.OnUnitChgSelect = new UnitCommands.UnitChgEvent(this.OnUnitChgSelect);
        }
        self.m_AllowCameraRotation = true;
        self.m_AllowCameraTranslation = false;
        self.ShowAllHPGauges();
        if (!this.mCurrentUnit.IsUnitFlag(EUnitFlag.Moved))
          self.ShowWalkableGrids(self.CreateCurrentAccessMap(), 0);
        if (self.mBattle.CurrentUnit.IsUnitFlag(EUnitFlag.Moved))
          return;
        self.mMoveInput = (SceneBattle.MoveInput) new SceneBattle.VirtualStickInput();
        self.mMoveInput.SceneOwner = self;
        self.mMoveInput.OnAttackTargetSelect = new SceneBattle.MoveInput.TargetSelectEvent(this.OnTargetSelect);
        self.mMoveInput.Start();
      }

      private void CanNotMove()
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.MOVE_BLOCKED"), (UIUtility.DialogResultEvent) null);
      }

      public override void End(SceneBattle self)
      {
        if (self.mMoveInput != null)
        {
          self.mMoveInput.End();
          self.mMoveInput = (SceneBattle.MoveInput) null;
        }
        self.m_AllowCameraRotation = false;
        self.m_AllowCameraTranslation = false;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.CommandWindow, (UnityEngine.Object) null))
          return;
        self.mBattleUI.CommandWindow.OnCommandSelect = (UnitCommands.CommandEvent) null;
      }

      public override void Update(SceneBattle self)
      {
        if (self.mMoveInput != null)
        {
          self.mMoveInput.Update();
          self.ReflectUnitChgButton(self.mBattle.CurrentUnit, true);
          IntVector2 intVector2 = self.CalcCoord(this.mCurrentController.CenterPosition);
          if (!this.mMoved && (intVector2.x != this.mStartX || intVector2.y != this.mStartY))
          {
            TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) && self.mTutorialTriggers != null)
            {
              for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
                self.mTutorialTriggers[index].OnUnitMoveStart(this.mCurrentUnit, unitController.Unit.TurnCount);
            }
            this.mMoved = true;
            if (self.Battle.IsMultiPlay)
              self.ExtentionMultiInputTime(true);
          }
          if (self.mNumHotTargets != this.mHotTargets)
          {
            TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) && self.mTutorialTriggers != null && self.mNumHotTargets > 0)
            {
              for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
                self.mTutorialTriggers[index].OnHotTargetsChange(this.mCurrentUnit, self.mHotTargets[0].Unit, unitController.Unit.TurnCount);
            }
            this.mHotTargets = self.mNumHotTargets;
          }
          self.OnGimmickUpdate();
        }
        if (!this.IsInputBusy)
        {
          if (!self.IsPause())
          {
            self.Battle.IsAutoBattle = self.Battle.RequestAutoBattle;
            if (self.Battle.IsAutoBattle)
            {
              TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
              {
                ((Component) unitController).transform.position = self.CalcGridCenter(self.mBattle.CurrentUnit.x, self.mBattle.CurrentUnit.y);
                self.InterpCameraTarget((Component) unitController);
              }
              self.HideAllHPGauges();
              self.HideAllUnitOwnerIndex();
              self.CloseBattleUI();
              self.OnGimmickUpdate();
              self.SendAuto();
              self.GotoState_WaitSignal<SceneBattle.State_MapCommandAI>();
              return;
            }
          }
          if (this.mShouldRefreshCommands)
          {
            this.mShouldRefreshCommands = false;
            self.RefreshMapCommands();
          }
          if (this.mQueuedTarget != null)
          {
            if (!this.IsGridValid)
              this.CanNotMove();
            else
              this.SelectAttackTarget(this.mQueuedTarget);
            this.mQueuedTarget = (Unit) null;
          }
          else
          {
            if (this.mQueuedCommand == UnitCommands.CommandTypes.None)
              return;
            if (!this.IsGridValid)
              this.CanNotMove();
            else
              this.SelectCommand(this.mQueuedCommand, this.mSelectedAbility);
            this.mQueuedCommand = UnitCommands.CommandTypes.None;
          }
        }
        else
          this.mShouldRefreshCommands = true;
      }

      private bool IsInputBusy => this.self.mMoveInput != null && this.self.mMoveInput.IsBusy;

      private bool IsGridValid => this.self.ApplyUnitMovement(true);

      private void OnTargetSelect(Unit target)
      {
        if (this.mQueuedTarget != null || this.mQueuedCommand != UnitCommands.CommandTypes.None)
          return;
        if (this.IsInputBusy)
          this.mQueuedTarget = target;
        else if (!this.IsGridValid)
          this.CanNotMove();
        else
          this.SelectAttackTarget(target);
      }

      private void SelectAttackTarget(Unit target)
      {
        Unit currentUnit = this.self.mBattle.CurrentUnit;
        SkillData attackSkill = currentUnit.GetAttackSkill();
        TacticsUnitController unitController = this.self.FindUnitController(currentUnit);
        IntVector2 intVector2 = this.self.CalcCoord(unitController.CenterPosition);
        unitController.Unit.RefreshMomentBuff(this.self.mBattle.Units, true, intVector2.x, intVector2.y);
        this.self.mBattle.CacheClearForcedTargeting();
        GridMap<bool> selectGridMap = this.self.mBattle.CreateSelectGridMap(currentUnit, intVector2.x, intVector2.y, attackSkill);
        if (this.self.Battle.IsValidForcedTargeting(currentUnit, intVector2.x, intVector2.y, attackSkill) && target != currentUnit.FtgtFromActiveUnit || !selectGridMap.isValid(target.x, target.y) || !selectGridMap.get(target.x, target.y) || !this.self.mBattle.CheckSkillTarget(currentUnit, target, attackSkill))
          return;
        this.self.GotoSelectTarget(attackSkill, new SceneBattle.SelectTargetCallback(this.self.GotoMapCommand), new SceneBattle.SelectTargetPositionWithSkill(this.self.OnSelectAttackTarget), target);
      }

      private void SelectCommand(UnitCommands.CommandTypes command, object ability)
      {
        if (this.self.Battle.IsMultiPlay)
        {
          if (command != UnitCommands.CommandTypes.Attack && command != UnitCommands.CommandTypes.Ability)
          {
            if (command == UnitCommands.CommandTypes.Map)
              this.self.ExtentionMultiInputTime(true);
          }
          else
            this.self.ExtentionMultiInputTime(false);
          switch (command - 2)
          {
            case UnitCommands.CommandTypes.None:
            case UnitCommands.CommandTypes.Move:
            case UnitCommands.CommandTypes.Attack:
            case UnitCommands.CommandTypes.Ability:
            case UnitCommands.CommandTypes.Item:
            case UnitCommands.CommandTypes.Gimmick:
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mInGameMenu, (UnityEngine.Object) null))
              {
                this.self.mInGameMenu.MultiAutoDisplay(false);
                break;
              }
              break;
          }
        }
        Unit currentUnit = this.self.mBattle.CurrentUnit;
        IntVector2 intVector2 = this.self.CalcCoord(this.self.FindUnitController(currentUnit).CenterPosition);
        currentUnit.RefreshMomentBuff(this.self.mBattle.Units, true, intVector2.x, intVector2.y);
        this.self.mBattle.CacheClearForcedTargeting();
        if (command == UnitCommands.CommandTypes.Gimmick)
        {
          Grid current = this.self.mBattle.CurrentMap[intVector2.x, intVector2.y];
          if (!this.self.mBattle.CheckGridEventTrigger(currentUnit, current, EEventTrigger.ExecuteOnGrid))
            return;
        }
        this.self.mBattleUI.OnCommandSelect();
        switch (command - 1)
        {
          case UnitCommands.CommandTypes.None:
            this.self.GotoInputMovement();
            break;
          case UnitCommands.CommandTypes.Move:
            this.self.GotoSelectAttackTarget();
            break;
          case UnitCommands.CommandTypes.Attack:
            this.self.UIParam_CurrentAbility = (AbilityData) ability;
            this.self.GotoSkillSelect();
            break;
          case UnitCommands.CommandTypes.Ability:
            this.self.GotoItemSelect();
            break;
          case UnitCommands.CommandTypes.Item:
            this.self.GotoState_WaitSignal<SceneBattle.State_SelectGridEventV2>();
            break;
          case UnitCommands.CommandTypes.Gimmick:
            this.self.GotoMapViewMode();
            break;
          case UnitCommands.CommandTypes.Map:
            this.self.HideGrid();
            this.self.mBattleUI.OnInputDirectionStart();
            this.self.GotoState_WaitSignal<SceneBattle.State_InputDirection>();
            break;
        }
      }

      private void OnCommandSelect(UnitCommands.CommandTypes command, object ability)
      {
        if (this.mQueuedTarget != null)
          return;
        if (this.IsInputBusy)
        {
          this.mQueuedCommand = command;
          this.mSelectedAbility = ability;
        }
        else if (!this.IsGridValid)
          this.CanNotMove();
        else
          this.SelectCommand(command, ability);
      }

      private void OnUnitChgSelect()
      {
        this.self.ReflectUnitChgButton(this.self.mBattle.CurrentUnit, true);
        if (!this.self.mBattleUI.CommandWindow.IsEnableUnitChgButton || !this.self.mBattleUI.CommandWindow.IsActiveUnitChgButton)
          return;
        this.self.mBattleUI.OnCommandSelect();
        this.self.GotoUnitChgSelect();
      }
    }

    private class State_SelectItemV2 : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.InterpCameraTargetToCurrent();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.ItemWindow, (UnityEngine.Object) null))
          self.mBattleUI.ItemWindow.OnSelectItem = new BattleInventory.SelectEvent(this.OnSelectItem);
        self.mOnRequestStateChange = new SceneBattle.StateTransitionRequest(this.OnStateChange);
        self.StepToNear(self.Battle.CurrentUnit);
      }

      public override void End(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.ItemWindow, (UnityEngine.Object) null))
          self.mBattleUI.ItemWindow.OnSelectItem = (BattleInventory.SelectEvent) null;
        self.mOnRequestStateChange = (SceneBattle.StateTransitionRequest) null;
      }

      private void OnSelectItem(ItemData itemData)
      {
        this.self.mBattleUI.OnItemSelectEnd();
        this.self.GotoSelectTarget(itemData, new SceneBattle.SelectTargetCallback(this.self.GotoItemSelect), new SceneBattle.SelectTargetPositionWithItem(this.self.OnSelectItemTarget));
      }

      private void OnStateChange(SceneBattle.StateTransitionTypes req)
      {
        if (req == SceneBattle.StateTransitionTypes.Forward)
          return;
        this.self.mBattleUI.OnItemSelectEnd();
        this.self.GotoMapCommand();
      }
    }

    private class State_SelectUnitChgV2 : State<SceneBattle>
    {
      private Unit mUnitChgTo;
      private List<Unit> mTargets = new List<Unit>(2);

      public override void Begin(SceneBattle self)
      {
        self.InterpCameraTargetToCurrent();
        this.mTargets.Clear();
        foreach (Unit unit in self.Battle.Player)
        {
          if (!self.Battle.StartingMembers.Contains(unit) && !unit.IsDead && unit.IsEntry && unit.IsSub)
            this.mTargets.Add(unit);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.UnitChgWindow, (UnityEngine.Object) null))
          self.mBattleUI.UnitChgWindow.OnSelectUnit = new BattleUnitChg.SelectEvent(this.OnSelectUnit);
        self.mOnRequestStateChange = new SceneBattle.StateTransitionRequest(this.OnStateChange);
        self.StepToNear(self.Battle.CurrentUnit);
      }

      public override void End(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.TargetSub, (UnityEngine.Object) null))
        {
          self.mBattleUI.TargetSub.SetNextTargetArrowActive(false);
          self.mBattleUI.TargetSub.SetPrevTargetArrowActive(false);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.UnitChgWindow, (UnityEngine.Object) null))
          self.mBattleUI.UnitChgWindow.OnSelectUnit = (BattleUnitChg.SelectEvent) null;
        self.mOnRequestStateChange = (SceneBattle.StateTransitionRequest) null;
      }

      private void OnSelectUnit(Unit unit_chg_to)
      {
        this.self.mBattleUI.OnUnitChgSelectEnd();
        this.mUnitChgTo = unit_chg_to;
        Unit currentUnit = this.self.mBattle.CurrentUnit;
        TacticsUnitController unitController = this.self.FindUnitController(currentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          this.self.HideUnitMarkers();
          this.self.ShowUnitMarker(currentUnit, SceneBattle.UnitMarkerTypes.Target);
          unitController.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Change);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetMain, (UnityEngine.Object) null))
          {
            this.self.mBattleUI.TargetMain.ResetHpGauge(currentUnit.Side, currentUnit.GetCurrentHP(), currentUnit.GetMaximumHP());
            this.self.mBattleUI.TargetMain.SetNoAction(currentUnit);
            this.self.mBattleUI.TargetMain.Open();
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetSub, (UnityEngine.Object) null))
          {
            this.self.mBattleUI.TargetSub.ResetHpGauge(unit_chg_to.Side, unit_chg_to.GetCurrentHP(), unit_chg_to.GetMaximumHP());
            if (this.mTargets.Count > 1)
            {
              this.self.mBattleUI.TargetSub.ActivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextTargetClick));
              this.self.mBattleUI.TargetSub.ActivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevTargetClick));
            }
            else
            {
              this.self.mBattleUI.TargetSub.SetNextTargetArrowActive(false);
              this.self.mBattleUI.TargetSub.SetPrevTargetArrowActive(false);
            }
            this.self.mBattleUI.TargetSub.SetNoAction(unit_chg_to);
            this.self.mBattleUI.TargetSub.SetEnableFlipButton(false);
            this.self.mBattleUI.TargetSub.Open();
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetObjectSub, (UnityEngine.Object) null))
            this.self.mBattleUI.TargetObjectSub.Close();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetTrickSub, (UnityEngine.Object) null))
            this.self.mBattleUI.TargetTrickSub.Close();
          this.self.mBattleUI.OnVersusStart();
        }
        this.self.mBattleUI.CommandWindow.OnYesNoSelect += new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        this.self.mBattleUI.OnUnitChgConfirmStart();
      }

      private void OnStateChange(SceneBattle.StateTransitionTypes req)
      {
        if (req == SceneBattle.StateTransitionTypes.Forward)
          return;
        this.self.mBattleUI.OnUnitChgSelectEnd();
        this.self.HideGrid();
        this.self.GotoMapCommand();
      }

      private void ShiftTarget(int delta)
      {
        if (this.mTargets.Count == 0 || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetSub, (UnityEngine.Object) null))
          return;
        int num = this.mTargets.IndexOf(this.mUnitChgTo);
        if (num < 0)
          num = 0;
        this.mUnitChgTo = this.mTargets[(num + delta + this.mTargets.Count) % this.mTargets.Count];
        this.self.mBattleUI.TargetSub.ResetHpGauge(this.mUnitChgTo.Side, this.mUnitChgTo.GetCurrentHP(), this.mUnitChgTo.GetMaximumHP());
        this.self.mBattleUI.TargetSub.SetNoAction(this.mUnitChgTo);
        this.self.mBattleUI.TargetSub.Open();
      }

      private void OnNextTargetClick(GameObject go) => this.ShiftTarget(1);

      private void OnPrevTargetClick(GameObject go) => this.ShiftTarget(-1);

      private void OnYesNoSelect(bool yes)
      {
        this.self.mBattleUI.CommandWindow.OnYesNoSelect -= new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        this.self.mBattleUI.OnUnitChgConfirmEnd();
        Unit currentUnit = this.self.Battle.CurrentUnit;
        Unit mUnitChgTo = this.mUnitChgTo;
        TacticsUnitController unitController1 = this.self.FindUnitController(currentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController1, (UnityEngine.Object) null))
        {
          this.self.HideUnitMarkers(UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController1));
          unitController1.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetMain, (UnityEngine.Object) null))
          this.self.mBattleUI.TargetMain.ForceClose();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetSub, (UnityEngine.Object) null))
        {
          this.self.mBattleUI.TargetSub.SetNextTargetArrowActive(false);
          this.self.mBattleUI.TargetSub.SetPrevTargetArrowActive(false);
          this.self.mBattleUI.TargetSub.ForceClose();
        }
        this.self.mBattleUI.OnVersusEnd();
        if (yes)
        {
          this.self.HideGrid();
          if (currentUnit != null)
          {
            IntVector2 intVector2 = new IntVector2(currentUnit.x, currentUnit.y);
            EUnitDirection dir = currentUnit.Direction;
            TacticsUnitController unitController2 = this.self.FindUnitController(currentUnit);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController2, (UnityEngine.Object) null))
            {
              intVector2 = this.self.CalcCoord(unitController2.CenterPosition);
              dir = unitController2.CalcUnitDirectionFromRotation();
            }
            this.self.Battle.UnitChange(currentUnit, intVector2.x, intVector2.y, dir, mUnitChgTo);
          }
          this.self.Battle.MapCommandEnd(this.self.Battle.CurrentUnit);
          this.self.Battle.CommandWait(true);
          this.self.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
        }
        else
          this.self.GotoUnitChgSelect(true);
      }
    }

    private delegate void SelectTargetCallback();

    private delegate void SelectTargetPositionWithSkill(
      int x,
      int y,
      SkillData skill,
      bool bUnitTarget);

    private delegate void SelectTargetPositionWithItem(int x, int y, ItemData item);

    private struct TargetSelectorParam
    {
      public ItemData Item;
      public SkillData Skill;
      public SceneBattle.SelectTargetCallback OnCancel;
      public object OnAccept;
      public Unit DefaultTarget;
      public bool AllowTargetChange;
      public bool IsThrowTargetSelect;
      public Unit DefaultThrowTarget;
      public Unit ThrowTarget;
    }

    private class State_PreThrowTargetSelect : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.UIParam_TargetValid = false;
        self.mBattleUI.OnThrowTargetSelectStart();
        self.mBattleUI.OnVersusStart();
        self.GotoState_WaitSignal<SceneBattle.State_ThrowTargetSelect>();
      }
    }

    private class State_ThrowTargetSelect : State<SceneBattle>
    {
      private const int THROW_TARGET_RANGE = 1;
      private TacticsUnitController mSelectedTarget;
      private GridMap<bool> mTargetGrids;
      private IntVector2 mTargetPosition;
      private List<TacticsUnitController> mTargets = new List<TacticsUnitController>(4);
      private bool mDragScroll;
      private float mYScrollPos;
      private bool mIgnoreDragVelocity;
      private float mDragY;
      private bool mIsValidFtgt;

      public override void Begin(SceneBattle self)
      {
        bool flag = true;
        Unit currentUnit = self.mBattle.CurrentUnit;
        TacticsUnitController unitController1 = self.FindUnitController(currentUnit);
        IntVector2 intVector2 = self.CalcCoord(unitController1.CenterPosition);
        this.mTargetGrids = new GridMap<bool>(self.Battle.CurrentMap.Width, self.Battle.CurrentMap.Height);
        Grid current1 = self.Battle.CurrentMap[intVector2.x, intVector2.y];
        self.Battle.CreateGridMapCross(current1, 0, 1, ref this.mTargetGrids);
        int throwHeight = (int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.ThrowHeight;
        for (int x = 0; x < this.mTargetGrids.w; ++x)
        {
          for (int y = 0; y < this.mTargetGrids.h; ++y)
          {
            if (this.mTargetGrids.get(x, y))
            {
              Grid current2 = self.Battle.CurrentMap[x, y];
              if (current2 != null)
              {
                if (current2.IsDisableStopped())
                  this.mTargetGrids.set(x, y, false);
                if (Math.Abs(current1.height - current2.height) > throwHeight)
                  this.mTargetGrids.set(x, y, false);
              }
            }
          }
        }
        Color32 src = Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea);
        GridMap<Color32> grid = new GridMap<Color32>(this.mTargetGrids.w, this.mTargetGrids.h);
        for (int x = 0; x < this.mTargetGrids.w; ++x)
        {
          for (int y = 0; y < this.mTargetGrids.h; ++y)
          {
            if (this.mTargetGrids.get(x, y))
              grid.set(x, y, src);
          }
        }
        self.mTacticsSceneRoot.ShowGridLayer(1, grid, true);
        this.mTargets.Clear();
        for (int x = 0; x < self.Battle.CurrentMap.Width; ++x)
        {
          for (int y = 0; y < self.Battle.CurrentMap.Height; ++y)
          {
            if (this.mTargetGrids.get(x, y))
            {
              Unit unit = self.Battle.FindUnitAtGrid(self.Battle.CurrentMap[x, y]);
              if (unit == null)
              {
                unit = self.Battle.FindGimmickAtGrid(self.Battle.CurrentMap[x, y]);
                if (unit == null || !unit.IsBreakObj)
                  continue;
              }
              if (unit != currentUnit && unit.IsThrow && unit.IsNormalSize && !unit.IsJump)
              {
                TacticsUnitController unitController2 = self.FindUnitController(unit);
                if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController2))
                  this.mTargets.Add(unitController2);
              }
            }
          }
        }
        this.mIsValidFtgt = false;
        if (currentUnit != null && currentUnit.FtgtFromActiveUnit != null)
        {
          Unit unit_from = currentUnit.FtgtFromActiveUnit;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTargets.Find((Predicate<TacticsUnitController>) (t => t.Unit == unit_from)), (UnityEngine.Object) null))
          {
            for (int index = this.mTargets.Count - 1; index >= 0; --index)
            {
              if (this.mTargets[index].Unit != unit_from)
                this.mTargets.RemoveAt(index);
            }
            this.mIsValidFtgt = true;
          }
        }
        if (this.mTargets.Count > 0)
        {
          self.mTargetSelectorParam.DefaultThrowTarget = this.mTargets[0].Unit;
          flag = false;
        }
        self.mOnUnitClick = new SceneBattle.UnitClickEvent(this.OnClickUnit);
        self.mOnUnitFocus = new SceneBattle.UnitFocusEvent(this.OnFocus);
        self.m_AllowCameraRotation = flag;
        self.m_AllowCameraTranslation = flag;
        if (self.mTargetSelectorParam.DefaultThrowTarget != null)
        {
          TacticsUnitController unitController3 = self.FindUnitController(self.mTargetSelectorParam.DefaultThrowTarget);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController3, (UnityEngine.Object) null))
            this.OnFocus(unitController3);
          self.InterpCameraTarget((Component) unitController3);
          this.SetYesButtonEnable(true);
        }
        self.mBattleUI.CommandWindow.OnYesNoSelect += new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        self.mTouchController.OnDragDelegate += new TouchController.DragEvent(this.OnDrag);
        self.mTouchController.OnDragEndDelegate += new TouchController.DragEvent(this.OnDragEnd);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mBattleUI.TargetSub))
        {
          if (this.mTargets.Count >= 2)
          {
            self.mBattleUI.TargetSub.ActivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextTargetClick));
            self.mBattleUI.TargetSub.ActivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevTargetClick));
          }
          else
          {
            self.mBattleUI.TargetSub.SetNextTargetArrowActive(false);
            self.mBattleUI.TargetSub.SetPrevTargetArrowActive(false);
          }
        }
        self.StepToNear(self.Battle.CurrentUnit);
        self.OnGimmickUpdate();
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mDragScroll)
          return;
        this.mYScrollPos += (float) (((double) self.mTouchController.DragDelta.y <= 0.0 ? -1.0 : 1.0) * (double) Time.unscaledDeltaTime * 2.0);
        if (!this.mIgnoreDragVelocity)
          this.mYScrollPos += this.mDragY / 20f;
        if ((double) this.mYScrollPos <= -1.0)
        {
          this.mYScrollPos = 0.0f;
          this.mIgnoreDragVelocity = true;
          this.ShiftTarget(-1);
        }
        else
        {
          if ((double) this.mYScrollPos < 1.0)
            return;
          this.mYScrollPos = 0.0f;
          this.mIgnoreDragVelocity = true;
          this.ShiftTarget(1);
        }
      }

      public override void End(SceneBattle self)
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mBattleUI.TargetSub))
        {
          self.mBattleUI.TargetSub.DeactivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextTargetClick));
          self.mBattleUI.TargetSub.DeactivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevTargetClick));
        }
        this.SetYesButtonEnable(true);
        self.mBattleUI.CommandWindow.OnYesNoSelect -= new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        self.mTouchController.OnDragDelegate -= new TouchController.DragEvent(this.OnDrag);
        self.mTouchController.OnDragEndDelegate -= new TouchController.DragEvent(this.OnDragEnd);
        self.HideUnitMarkers();
        self.m_AllowCameraRotation = false;
        self.m_AllowCameraTranslation = false;
        self.mOnUnitFocus = (SceneBattle.UnitFocusEvent) null;
        self.mOnUnitClick = (SceneBattle.UnitClickEvent) null;
        self.mOnGridClick = (SceneBattle.GridClickEvent) null;
        self.HideGrid();
        self.SetUnitUiHeight(self.mBattle.CurrentUnit);
      }

      private void SetYesButtonEnable(bool enable)
      {
        Selectable component = this.self.mBattleUI.CommandWindow.OKButton.GetComponent<Selectable>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        component.interactable = enable;
      }

      private void OnYesNoSelect(bool yes)
      {
        for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
        {
          this.self.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal);
          this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue);
        }
        this.self.mBattleUI.OnThrowTargetSelectEnd();
        if (yes)
        {
          this.self.mTargetSelectorParam.ThrowTarget = this.mSelectedTarget.Unit;
          this.self.mBattleUI.HideTargetWindows();
          this.self.GotoState_WaitSignal<SceneBattle.State_PreSelectTargetV2>();
        }
        else
        {
          this.self.mTargetSelectorParam.OnCancel();
          this.self.mBattleUI.HideTargetWindows();
          this.self.mBattleUI.OnVersusEnd();
        }
      }

      private bool IsGridSelectable(int x, int y)
      {
        if (this.mTargetGrids == null)
          return false;
        bool flag = this.mTargetGrids.get(x, y);
        if (flag)
        {
          Unit unit = this.self.Battle.FindUnitAtGrid(this.self.Battle.CurrentMap[x, y]);
          if (unit == null)
          {
            unit = this.self.Battle.FindGimmickAtGrid(this.self.Battle.CurrentMap[x, y]);
            if (unit != null && !unit.IsBreakObj)
              unit = (Unit) null;
          }
          if (unit == null || unit == this.self.mBattle.CurrentUnit || !unit.IsThrow || !unit.IsNormalSize || unit.IsJump)
            flag = false;
        }
        return flag;
      }

      private bool IsGridSelectable(Unit unit)
      {
        if (unit == null)
          return false;
        int x = unit.x;
        int y = unit.y;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mFocusedUnit, (UnityEngine.Object) null) && this.self.mFocusedUnit.Unit != null && this.self.mFocusedUnit.Unit == unit)
        {
          IntVector2 intVector2 = this.self.CalcCoord(this.self.mFocusedUnit.CenterPosition);
          x = intVector2.x;
          y = intVector2.y;
        }
        return this.IsGridSelectable(x, y);
      }

      private bool IsThrowValidForcedTargeting(Unit target_unit = null)
      {
        Unit currentUnit = this.self.mBattle.CurrentUnit;
        return this.mIsValidFtgt && (target_unit == null || target_unit != currentUnit.FtgtFromActiveUnit);
      }

      private void OnFocus(TacticsUnitController controller, Grid focus_grid = null)
      {
        if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) controller) || this.IsThrowValidForcedTargeting(controller.Unit) || controller.Unit.IsGimmick && !this.self.Battle.IsTargetBreakUnit(this.self.mBattle.CurrentUnit, controller.Unit))
          return;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSelectedTarget, (UnityEngine.Object) null))
          this.self.HideUnitMarkers(this.mSelectedTarget.Unit);
        this.self.ShowUnitMarker(controller.Unit, SceneBattle.UnitMarkerTypes.Target);
        this.mSelectedTarget = controller;
        IntVector2 intVector2 = this.self.CalcCoord(controller.CenterPosition);
        this.mTargetPosition.x = intVector2.x;
        this.mTargetPosition.y = intVector2.y;
        this.self.mSelectedTarget = controller.Unit;
        this.self.UIParam_TargetValid = false;
        Unit currentUnit = this.self.mBattle.CurrentUnit;
        SkillData skill = this.self.mTargetSelectorParam.Skill;
        TacticsUnitController unitController = this.self.FindUnitController(currentUnit);
        int hpCost = skill == null ? 0 : skill.GetHpCost(currentUnit);
        unitController.SetHPChangeYosou(currentUnit.GetCurrentHP() - (long) hpCost);
        this.self.mBattleUI.TargetMain.SetHpGaugeParam(currentUnit.Side, currentUnit.GetCurrentHP(), currentUnit.GetMaximumHP(), hpCost);
        this.self.mBattleUI.TargetMain.UpdateHpGauge();
        this.self.mBattleUI.TargetSub.ResetHpGauge(this.self.mSelectedTarget.Side, this.self.mSelectedTarget.GetCurrentHP(), this.self.mSelectedTarget.GetMaximumHP());
        for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
        {
          this.self.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal);
          if (0 >= hpCost || !UnityEngine.Object.op_Equality((UnityEngine.Object) this.self.mTacticsUnits[index], (UnityEngine.Object) controller))
            this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue);
        }
        if (this.IsGridSelectable(this.self.mSelectedTarget))
        {
          this.self.UIParam_TargetValid = true;
          this.mSelectedTarget.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Target);
          GridMap<bool> grid = new GridMap<bool>(this.self.Battle.CurrentMap.Width, this.self.Battle.CurrentMap.Height);
          grid.set(this.mTargetPosition.x, this.mTargetPosition.y, true);
          this.self.mTacticsSceneRoot.ShowGridLayer(2, grid, Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea2));
        }
        this.SetYesButtonEnable(this.self.UIParam_TargetValid);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetMain, (UnityEngine.Object) null))
        {
          this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit);
          this.self.mBattleUI.TargetMain.Open();
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetSub, (UnityEngine.Object) null))
        {
          if (this.self.UIParam_TargetValid)
          {
            this.self.mBattleUI.TargetSub.SetNoAction(this.self.mSelectedTarget);
            this.self.mBattleUI.TargetSub.Open();
          }
          else if (this.self.mSelectedTarget.UnitType == EUnitType.Unit || this.self.mSelectedTarget.UnitType == EUnitType.EventUnit || this.self.mSelectedTarget.IsBreakObj && this.self.mSelectedTarget.IsBreakDispUI)
          {
            this.self.mBattleUI.TargetSub.SetNoAction(this.self.mSelectedTarget);
            this.self.mBattleUI.TargetSub.Open();
          }
          else
            this.self.mBattleUI.TargetSub.ForceClose(false);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetObjectSub, (UnityEngine.Object) null))
          this.self.mBattleUI.TargetObjectSub.Close();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetTrickSub, (UnityEngine.Object) null))
          this.self.mBattleUI.TargetTrickSub.Close();
        this.self.OnGimmickUpdate();
        this.self.SetUnitUiHeight(controller.Unit);
      }

      private void OnClickUnit(TacticsUnitController controller)
      {
        if (!this.IsThrowValidForcedTargeting(!UnityEngine.Object.op_Inequality((UnityEngine.Object) controller, (UnityEngine.Object) null) ? (Unit) null : controller.Unit))
          this.self.InterpCameraTarget((Component) controller);
        this.self.mFocusedUnit = controller;
        this.OnFocus(controller);
      }

      private void ShiftTarget(int delta)
      {
        if (this.mTargets.Count == 0)
          return;
        int num = this.mTargets.IndexOf(this.self.mFocusedUnit);
        if (num < 0)
          num = 0;
        this.OnClickUnit(this.mTargets[(num + delta + this.mTargets.Count) % this.mTargets.Count]);
      }

      private void OnNextTargetClick(GameObject go) => this.ShiftTarget(1);

      private void OnPrevTargetClick(GameObject go) => this.ShiftTarget(-1);

      private void OnDrag()
      {
        if (!this.mIgnoreDragVelocity)
          this.mDragY += this.self.mTouchController.DragDelta.y;
        if (this.mTargets.Count == 0)
          return;
        this.mDragScroll = true;
      }

      private void OnDragEnd()
      {
        this.mDragY = 0.0f;
        this.mYScrollPos = 0.0f;
        this.mDragScroll = false;
        this.mIgnoreDragVelocity = false;
      }
    }

    private class State_PreSelectTargetV2 : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.UIParam_TargetValid = false;
        self.mBattleUI.OnVersusStart();
        self.mBattleUI.OnTargetSelectStart();
        self.GotoState_WaitSignal<SceneBattle.State_SelectTargetV2>();
      }
    }

    private class State_PreMapviewV2 : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.UIParam_TargetValid = false;
        self.mBattleUI.OnMapStart();
        self.GotoState_WaitSignal<SceneBattle.State_SelectTargetV2>();
      }
    }

    private class State_SelectTargetV2 : State<SceneBattle>
    {
      private SceneBattle mScene;
      private TacticsUnitController mSelectedTarget;
      private GridMap<bool> mTargetGrids;
      private GridMap<bool> mTargetAreaGridMap;
      private bool mSelectGrid = true;
      private IntVector2 mTargetPosition;
      private List<TacticsUnitController> mTargets = new List<TacticsUnitController>(32);
      private bool mDragScroll;
      private float mYScrollPos;
      private bool mIgnoreDragVelocity;
      private float mDragY;
      private GUIEventListener mGUIEvent;

      public override void Begin(SceneBattle self)
      {
        bool flag1 = true;
        this.mScene = self;
        SkillData skill = self.mTargetSelectorParam.Skill;
        self.mSelectedTarget = self.Battle.CurrentUnit;
        Unit current = self.mBattle.CurrentUnit;
        TacticsUnitController unitController1 = self.FindUnitController(current);
        IntVector2 intVector2_1 = self.CalcCoord(unitController1.CenterPosition);
        this.mTargets.Clear();
        if (skill != null)
        {
          this.mSelectGrid = skill.SkillParam.IsAreaSkill();
          if (skill.IsTargetGridNoUnit || skill.IsTargetValidGrid)
            this.mSelectGrid = true;
          this.mTargetGrids = self.Battle.CreateSelectGridMap(current, intVector2_1.x, intVector2_1.y, self.mTargetSelectorParam.Skill);
          if (!this.mSelectGrid)
          {
            int x = current.x;
            int y = current.y;
            current.x = intVector2_1.x;
            current.y = intVector2_1.y;
            List<Unit> attackTargetsAi = self.mBattle.CreateAttackTargetsAI(current, self.mTargetSelectorParam.Skill, false);
            if (attackTargetsAi.Count > 0)
            {
              for (int index = 0; index < attackTargetsAi.Count; ++index)
              {
                TacticsUnitController unitController2 = self.FindUnitController(attackTargetsAi[index]);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController2, (UnityEngine.Object) null) && self.Battle.IsTargetUnitForGridMap(attackTargetsAi[index], this.mTargetGrids) && !unitController2.Unit.IsDead && (!unitController2.Unit.IsBreakObj || unitController2.Unit.IsBreakDispUI))
                  this.mTargets.Add(unitController2);
              }
              flag1 = false;
            }
            current.x = x;
            current.y = y;
          }
          Color32 src = Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea);
          GridMap<Color32> grid = new GridMap<Color32>(this.mTargetGrids.w, this.mTargetGrids.h);
          for (int x = 0; x < this.mTargetGrids.w; ++x)
          {
            for (int y = 0; y < this.mTargetGrids.h; ++y)
            {
              if (this.mTargetGrids.get(x, y))
                grid.set(x, y, src);
            }
          }
          bool flag2 = false;
          if (self.mTargetSelectorParam.DefaultTarget == null)
          {
            Unit unit = (Unit) null;
            if (this.IsValidForcedTargeting())
            {
              if (this.mSelectGrid)
                unit = current.FtgtFromActiveUnit;
              else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTargets.Find((Predicate<TacticsUnitController>) (t => t.Unit == current.FtgtFromActiveUnit)), (UnityEngine.Object) null))
                unit = current.FtgtFromActiveUnit;
              if (unit == null)
              {
                self.UIParam_TargetValid = false;
                this.SetYesButtonEnable(self.UIParam_TargetValid);
              }
            }
            else
            {
              switch (skill.SkillParam.target)
              {
                case ESkillTarget.Self:
                case ESkillTarget.SelfSide:
                case ESkillTarget.SelfSideNotSelf:
                  unit = self.FindTarget(current, skill, this.mTargetGrids, current.Side == EUnitSide.Enemy ? EUnitSide.Enemy : EUnitSide.Player, skill.SkillParam.target == ESkillTarget.SelfSideNotSelf);
                  break;
                case ESkillTarget.EnemySide:
                  unit = self.FindTarget(current, skill, this.mTargetGrids, current.Side == EUnitSide.Enemy ? EUnitSide.Player : EUnitSide.Enemy);
                  break;
                case ESkillTarget.UnitAll:
                case ESkillTarget.NotSelf:
                  unit = self.FindTarget(current, skill, this.mTargetGrids, current.Side == EUnitSide.Enemy ? EUnitSide.Player : EUnitSide.Enemy);
                  if (unit == null)
                  {
                    unit = self.FindTarget(current, skill, this.mTargetGrids, current.Side == EUnitSide.Enemy ? EUnitSide.Enemy : EUnitSide.Player);
                    if (unit != null && !skill.SkillParam.IsSelfTargetSelect() && unit.x == current.x && unit.y == current.y)
                      unit = (Unit) null;
                  }
                  if (unit == null)
                  {
                    unit = self.FindTarget(current, skill, this.mTargetGrids, EUnitSide.Neutral);
                    break;
                  }
                  break;
                case ESkillTarget.GridNoUnit:
                case ESkillTarget.ValidGrid:
                  unit = current;
                  break;
              }
            }
            self.mTargetSelectorParam.DefaultTarget = unit;
            flag2 = unit != null;
          }
          self.mTacticsSceneRoot.ShowGridLayer(1, grid, true);
          self.ShowCastSkill();
          if (self.mTargetSelectorParam.AllowTargetChange && !flag2 && (self.mTargetSelectorParam.DefaultTarget == null || !self.mTargetSelectorParam.DefaultTarget.IsBreakObj || self.mTargetSelectorParam.DefaultTarget.IsBreakDispUI))
            this.OnFocusGrid(self.Battle.CurrentMap[intVector2_1.x, intVector2_1.y]);
        }
        else
        {
          int x = current.x;
          int y = current.y;
          current.x = intVector2_1.x;
          current.y = intVector2_1.y;
          GridMap<int> moveMap = self.mBattle.CreateMoveMap(current);
          current.x = x;
          current.y = y;
          self.ShowWalkableGrids(moveMap, 0);
          this.HilitNormalAttack(self.mBattle.CurrentUnit);
          self.InterpCameraDistance(GameSettings.Instance.GameCamera_MapDistance);
          this.OnFocusGrid(self.Battle.CurrentMap[self.mSelectedTarget.x, self.mSelectedTarget.y]);
          foreach (Unit unit in self.mBattle.Units)
          {
            if ((!unit.IsGimmick || unit.IsBreakObj) && !unit.IsDead)
            {
              TacticsUnitController unitController3 = self.FindUnitController(unit);
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController3))
                this.mTargets.Add(unitController3);
            }
          }
        }
        if (self.mTargetSelectorParam.AllowTargetChange)
        {
          if (this.mSelectGrid)
          {
            self.mOnGridClick = new SceneBattle.GridClickEvent(this.OnClickGrid);
            this.mTargetPosition.x = Mathf.FloorToInt(self.m_CameraPosition.x);
            this.mTargetPosition.y = Mathf.FloorToInt(self.m_CameraPosition.y);
          }
          else
          {
            self.mOnUnitClick = new SceneBattle.UnitClickEvent(this.OnClickUnit);
            self.mOnUnitFocus = new SceneBattle.UnitFocusEvent(this.OnFocus);
          }
          self.m_AllowCameraRotation = flag1;
          self.m_AllowCameraTranslation = flag1;
        }
        else
        {
          self.m_AllowCameraRotation = false;
          self.m_AllowCameraTranslation = false;
        }
        if (self.mTargetSelectorParam.DefaultTarget != null)
        {
          TacticsUnitController unitController4 = self.FindUnitController(self.mTargetSelectorParam.DefaultTarget);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController4, (UnityEngine.Object) null))
          {
            this.OnFocus(unitController4);
            IntVector2 intVector2_2 = self.CalcCoord(unitController4.CenterPosition);
            Grid current1 = self.Battle.CurrentMap[intVector2_2.x, intVector2_2.y];
            this.HilitArea(current1.x, current1.y);
          }
          self.InterpCameraTarget((Component) unitController4);
          bool flag3 = true;
          if (skill.IsTargetGridNoUnit)
            flag3 = false;
          else if (skill.IsTargetValidGrid)
          {
            self.mFocusedUnit = unitController4;
            self.mMapModeFocusedUnit = self.mFocusedUnit;
            flag3 = this.IsGridSelectable(self.mTargetSelectorParam.DefaultTarget);
          }
          if (skill.IsTargetTeleport && self.mTargetSelectorParam.DefaultTarget != null)
          {
            bool is_teleport = false;
            self.mBattle.GetTeleportGrid(current, intVector2_1.x, intVector2_1.y, self.mTargetSelectorParam.DefaultTarget, skill, ref is_teleport);
            if (!is_teleport)
              flag3 = false;
          }
          self.UIParam_TargetValid = flag3;
          this.SetYesButtonEnable(self.UIParam_TargetValid);
        }
        self.mBattleUI.CommandWindow.OnYesNoSelect += new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        self.mBattleUI.CommandWindow.OnMapExitSelect += new UnitCommands.MapExitEvent(this.OnMapExitSelect);
        self.mTouchController.OnDragDelegate += new TouchController.DragEvent(this.OnDrag);
        self.mTouchController.OnDragEndDelegate += new TouchController.DragEvent(this.OnDragEnd);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mBattleUI.TargetSub))
        {
          if (this.mTargets.Count >= 2)
          {
            self.mBattleUI.TargetSub.ActivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextTargetClick));
            self.mBattleUI.TargetSub.ActivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevTargetClick));
          }
          else
          {
            self.mBattleUI.TargetSub.SetNextTargetArrowActive(false);
            self.mBattleUI.TargetSub.SetPrevTargetArrowActive(false);
          }
        }
        self.StepToNear(self.Battle.CurrentUnit);
        self.OnGimmickUpdate();
      }

      public override void End(SceneBattle self)
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mBattleUI.TargetSub))
        {
          self.mBattleUI.TargetSub.DeactivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextTargetClick));
          self.mBattleUI.TargetSub.DeactivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevTargetClick));
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGUIEvent, (UnityEngine.Object) null))
        {
          UnityEngine.Object.Destroy((UnityEngine.Object) this.mGUIEvent);
          this.mGUIEvent = (GUIEventListener) null;
        }
        this.SetYesButtonEnable(true);
        self.mBattleUI.CommandWindow.OnYesNoSelect -= new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        self.mBattleUI.CommandWindow.OnMapExitSelect -= new UnitCommands.MapExitEvent(this.OnMapExitSelect);
        self.mTouchController.OnDragDelegate -= new TouchController.DragEvent(this.OnDrag);
        self.mTouchController.OnDragEndDelegate -= new TouchController.DragEvent(this.OnDragEnd);
        self.HideUnitMarkers();
        self.m_AllowCameraRotation = false;
        self.m_AllowCameraTranslation = false;
        self.mOnUnitFocus = (SceneBattle.UnitFocusEvent) null;
        self.mOnUnitClick = (SceneBattle.UnitClickEvent) null;
        self.mOnGridClick = (SceneBattle.GridClickEvent) null;
        SkillData skill = self.mTargetSelectorParam.Skill;
        if (skill != null && skill.IsTargetTeleport)
        {
          self.mDisplayBlockedGridMarker = false;
          self.mGridDisplayBlockedGridMarker = (Grid) null;
        }
        if (self.mIsBackSelectSkill)
        {
          self.mTacticsSceneRoot.HideGridLayer(0);
          self.mTacticsSceneRoot.HideGridLayer(1);
          self.mTacticsSceneRoot.HideGridLayer(2);
        }
        else
          self.HideGrid();
        self.SetUnitUiHeight(self.mBattle.CurrentUnit);
      }

      private bool NeedsTargetKakunin(Unit unit)
      {
        SkillData skill = this.self.mTargetSelectorParam.Skill;
        if (skill == null || !skill.IsCastSkill() || !skill.IsEnableUnitLockTarget())
          return false;
        Grid current = this.mScene.Battle.CurrentMap[this.mTargetPosition.x, this.mTargetPosition.y];
        IntVector2 intVector2 = this.self.CalcCoord(this.self.FindUnitController(unit).CenterPosition);
        int x = unit.x;
        int y = unit.y;
        unit.x = intVector2.x;
        unit.y = intVector2.y;
        Unit unitAtGrid = this.mScene.Battle.FindUnitAtGrid(current);
        bool flag = false;
        if (unitAtGrid != null && !unitAtGrid.IsNormalSize && (unitAtGrid.x != this.mTargetPosition.x || unitAtGrid.y != this.mTargetPosition.y))
          flag = true;
        unit.x = x;
        unit.y = y;
        return !flag && this.mScene.Battle.CheckSkillTarget(this.self.Battle.CurrentUnit, unitAtGrid, skill);
      }

      private void OnNextTargetClick(GameObject go) => this.ShiftTarget(1);

      private void OnPrevTargetClick(GameObject go) => this.ShiftTarget(-1);

      private void OnDragEnd()
      {
        this.mDragY = 0.0f;
        this.mYScrollPos = 0.0f;
        this.mDragScroll = false;
        this.mIgnoreDragVelocity = false;
      }

      private void OnDrag()
      {
        if (!this.mIgnoreDragVelocity)
          this.mDragY += this.self.mTouchController.DragDelta.y;
        if (this.mTargets.Count == 0)
          return;
        this.mDragScroll = true;
      }

      private void SetYesButtonEnable(bool enable)
      {
        Selectable component = this.self.mBattleUI.CommandWindow.OKButton.GetComponent<Selectable>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        component.interactable = enable;
      }

      private void ResetHPGaugeYosouDamage()
      {
        for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
        {
          this.self.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal);
          this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue);
        }
      }

      private void OnYesNoSelect(bool yes)
      {
        bool flag1 = false;
        if (yes && this.self.UIParam_TargetValid && this.self.mTargetSelectorParam.Item == null)
          flag1 = this.NeedsTargetKakunin(this.self.Battle.CurrentUnit);
        if (!flag1)
          this.ResetHPGaugeYosouDamage();
        bool flag2 = false;
        if (yes)
        {
          if (this.self.UIParam_TargetValid)
          {
            if (this.self.mTargetSelectorParam.Item != null)
            {
              this.self.mBattleUI.OnTargetSelectEnd();
              ((SceneBattle.SelectTargetPositionWithItem) this.self.mTargetSelectorParam.OnAccept)(this.mTargetPosition.x, this.mTargetPosition.y, this.self.mTargetSelectorParam.Item);
              this.self.mBattleUI.HideTargetWindows();
            }
            else
            {
              Unit currentUnit1 = this.self.Battle.CurrentUnit;
              bool flag3 = false;
              if (this.self.mTargetSelectorParam.Skill.EffectType == SkillEffectTypes.Changing)
              {
                IntVector2 intVector2 = this.self.CalcCoord(this.self.FindUnitController(currentUnit1).CenterPosition);
                if (currentUnit1.x == intVector2.x && currentUnit1.y == intVector2.y)
                  flag3 = true;
              }
              if (flag1)
              {
                this.StartTargetKakunin();
                flag2 = true;
              }
              else
              {
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self, (UnityEngine.Object) null) && this.self.mBattle != null)
                  this.self.mBattle.SetManualPlayFlag();
                this.self.mBattleUI.OnTargetSelectEnd();
                bool bUnitTarget = false;
                SkillData skill = this.self.mTargetSelectorParam.Skill;
                if (skill != null && skill.IsForceUnitLock() && skill.SkillParam.range_max == 0)
                  bUnitTarget = true;
                ((SceneBattle.SelectTargetPositionWithSkill) this.self.mTargetSelectorParam.OnAccept)(this.mTargetPosition.x, this.mTargetPosition.y, skill, bUnitTarget);
                this.self.mBattleUI.HideTargetWindows();
              }
              if (flag3)
              {
                this.self.mCurrentUnitStartX = currentUnit1.x;
                this.self.mCurrentUnitStartY = currentUnit1.y;
              }
              Unit currentUnit2 = this.self.Battle.CurrentUnit;
              this.self.mSkillDirectionByKouka = this.self.GetSkillDirectionByTargetArea(currentUnit2, currentUnit2.x, currentUnit2.y, this.mTargetAreaGridMap);
            }
          }
        }
        else
        {
          this.self.mBattleUI.OnTargetSelectEnd();
          if (this.self.mTargetSelectorParam.IsThrowTargetSelect)
          {
            this.self.GotoState_WaitSignal<SceneBattle.State_PreThrowTargetSelect>();
            flag2 = true;
          }
          else
            this.self.mTargetSelectorParam.OnCancel();
          this.self.mBattleUI.HideTargetWindows();
        }
        if (flag2)
          return;
        this.self.mBattleUI.OnVersusEnd();
      }

      private void StartTargetKakunin()
      {
        this.self.mSkillTargetWindow.OnCancel = new SkillTargetWindow.CancelEvent(this.OnCancelTarget);
        this.self.mSkillTargetWindow.OnTargetSelect = new SkillTargetWindow.TargetSelectEvent(this.OnSelectTargetMode);
        this.self.mSkillTargetWindow.Show();
      }

      private void OnCancelTarget() => this.self.mSkillTargetWindow.Hide();

      private void OnSelectTargetMode(bool targetIsGrid)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self, (UnityEngine.Object) null) && this.self.mBattle != null)
          this.self.mBattle.SetManualPlayFlag();
        this.ResetHPGaugeYosouDamage();
        this.self.mSkillTargetWindow.Hide();
        this.self.mBattleUI.OnTargetSelectEnd();
        ((SceneBattle.SelectTargetPositionWithSkill) this.self.mTargetSelectorParam.OnAccept)(this.mTargetPosition.x, this.mTargetPosition.y, this.self.mTargetSelectorParam.Skill, !targetIsGrid);
        this.self.mBattleUI.HideTargetWindows();
        this.self.mBattleUI.OnVersusEnd();
      }

      private void OnMapExitSelect()
      {
        for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
        {
          this.self.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal);
          this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue);
        }
        this.self.mBattleUI.OnTargetSelectEnd();
        this.self.mTargetSelectorParam.OnCancel();
        this.self.mBattleUI.HideTargetWindows();
        this.self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
        this.self.mBattleUI.OnMapViewEnd();
      }

      private void HilitArea(int x, int y)
      {
        SkillData skill = this.self.mTargetSelectorParam.Skill;
        if (skill == null)
          return;
        IntVector2 intVector2 = this.self.CalcCoord(this.self.FindUnitController(this.self.mBattle.CurrentUnit).CenterPosition);
        Unit unitAtGrid = this.self.Battle.FindUnitAtGrid(x, y);
        if (unitAtGrid != null && !unitAtGrid.IsNormalSize && !skill.IsTargetGridNoUnit && !skill.IsTargetValidGrid)
        {
          IntVector2 gridForSkillRange = this.self.Battle.GetValidGridForSkillRange(this.self.mBattle.CurrentUnit, intVector2.x, intVector2.y, skill, x, y);
          x = gridForSkillRange.x;
          y = gridForSkillRange.y;
        }
        this.mTargetAreaGridMap = this.self.mBattle.CreateScopeGridMap(this.self.mBattle.CurrentUnit, intVector2.x, intVector2.y, x, y, skill);
        if (SkillParam.IsTypeLaser(skill.SkillParam.select_scope) && !this.mTargetAreaGridMap.get(x, y))
          this.mTargetAreaGridMap.fill(false);
        this.self.mTacticsSceneRoot.ShowGridLayer(2, this.mTargetAreaGridMap, Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea2));
      }

      private void HilitNormalAttack(Unit Attacker, bool showOnly = false)
      {
        if (!showOnly)
        {
          SkillData attackSkill = Attacker.GetAttackSkill();
          IntVector2 intVector2 = this.self.CalcCoord(this.self.FindUnitController(Attacker).CenterPosition);
          this.mTargetAreaGridMap = this.self.mBattle.CreateSelectGridMap(Attacker, intVector2.x, intVector2.y, attackSkill);
        }
        this.self.mTacticsSceneRoot.ShowGridLayer(1, this.mTargetAreaGridMap, Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea), true);
        this.self.ShowCastSkill();
      }

      private bool IsValidForcedTargeting(Unit target_unit = null)
      {
        SkillData skill = this.self.mTargetSelectorParam.Skill;
        if (skill == null)
          return false;
        Unit currentUnit = this.self.mBattle.CurrentUnit;
        TacticsUnitController unitController = this.self.FindUnitController(currentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          IntVector2 intVector2 = this.self.CalcCoord(unitController.CenterPosition);
          if (this.self.Battle.IsValidForcedTargeting(currentUnit, intVector2.x, intVector2.y, skill) && (target_unit == null || target_unit != currentUnit.FtgtFromActiveUnit))
            return true;
        }
        return false;
      }

      private void OnClickGrid(Grid grid)
      {
        if (!this.IsValidForcedTargeting())
          this.self.InterpCameraTarget(this.self.CalcGridCenter(grid));
        this.OnFocusGrid(grid);
      }

      private void OnClickUnit(TacticsUnitController controller)
      {
        if (!this.IsValidForcedTargeting(!UnityEngine.Object.op_Inequality((UnityEngine.Object) controller, (UnityEngine.Object) null) ? (Unit) null : controller.Unit))
          this.self.InterpCameraTarget((Component) controller);
        this.self.mFocusedUnit = controller;
        this.self.mMapModeFocusedUnit = this.self.mFocusedUnit;
        this.OnFocus(controller);
      }

      private void OnFocusGrid(Grid grid)
      {
        if (this.IsValidForcedTargeting())
          return;
        Unit current = this.self.mBattle.CurrentUnit;
        TacticsUnitController unitController1 = this.self.FindUnitController(current);
        IntVector2 intVector2_1;
        intVector2_1.x = current.x;
        intVector2_1.y = current.y;
        IntVector2 intVector2_2 = this.self.CalcCoord(unitController1.CenterPosition);
        current.x = intVector2_2.x;
        current.y = intVector2_2.y;
        this.HilitArea(grid.x, grid.y);
        Unit unitAtGrid = this.self.mBattle.FindUnitAtGrid(grid);
        if (unitAtGrid != null)
        {
          TacticsUnitController unitController2 = this.self.FindUnitController(unitAtGrid);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController2, (UnityEngine.Object) null))
          {
            this.self.mFocusedUnit = unitController2;
            this.self.mMapModeFocusedUnit = this.self.mFocusedUnit;
            this.OnFocus(unitController2, !unitAtGrid.IsNormalSize ? grid : (Grid) null);
          }
        }
        else if (this.self.mTargetSelectorParam.Skill != null && this.self.mTargetSelectorParam.Skill.IsAllEffect())
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController1, (UnityEngine.Object) null))
          {
            this.self.mFocusedUnit = unitController1;
            this.self.mMapModeFocusedUnit = this.self.mFocusedUnit;
            this.OnFocus(unitController1);
          }
        }
        else
        {
          this.mSelectedTarget = (TacticsUnitController) null;
          this.self.UIParam_TargetValid = false;
          for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
          {
            this.self.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal);
            this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue);
          }
          SkillData skill = this.self.mTargetSelectorParam.Skill;
          Unit unit1 = (Unit) null;
          if (skill != null)
          {
            int hpCost = skill.GetHpCost(current);
            unitController1.SetHPChangeYosou(current.GetCurrentHP() - (long) hpCost);
            this.self.mBattleUI.TargetMain.SetHpGaugeParam(current.Side, current.GetCurrentHP(), current.GetMaximumHP(), hpCost);
            this.self.mBattleUI.TargetMain.UpdateHpGauge();
            if (this.self.mTargetSelectorParam.OnAccept != null && this.mTargetGrids.get(grid.x, grid.y))
            {
              bool flag = true;
              if (skill.IsTargetGridNoUnit)
              {
                if (!current.IsNormalSize)
                {
                  flag = ((Func<Grid, bool>) (gr =>
                  {
                    for (int minColX = current.MinColX; minColX < current.MaxColX; ++minColX)
                    {
                      for (int minColY = current.MinColY; minColY < current.MaxColY; ++minColY)
                      {
                        Grid current1 = this.self.mBattle.CurrentMap[gr.x + minColX, gr.y + minColY];
                        if (current1 == null || current1.IsDisableStopped())
                          return false;
                        Unit unit2 = this.self.mBattle.FindUnitAtGrid(current1);
                        if (unit2 == null)
                        {
                          unit2 = this.self.mBattle.FindGimmickAtGrid(current1);
                          if (unit2 != null && !unit2.IsBreakObj)
                            unit2 = (Unit) null;
                        }
                        if (unit2 != null && unit2 != current)
                          return false;
                      }
                    }
                    return true;
                  }))(grid);
                }
                else
                {
                  Unit unit3 = this.self.mBattle.FindUnitAtGrid(grid);
                  if (unit3 == null)
                  {
                    unit3 = this.self.mBattle.FindGimmickAtGrid(grid);
                    if (unit3 != null && !unit3.IsBreakObj)
                      unit3 = (Unit) null;
                  }
                  flag = unit3 == null;
                }
              }
              if (flag)
              {
                BattleCore.CommandResult commandResult = this.self.mBattle.GetCommandResult(current, intVector2_2.x, intVector2_2.y, grid.x, grid.y, skill, true);
                Unit protectTarget = this.self.Battle.GetProtectTarget(this.self.mSelectedTarget, skill);
                if (protectTarget != null)
                  this.self.mSelectedTarget = protectTarget;
                if (commandResult != null && commandResult.targets != null)
                {
                  for (int index = 0; index < commandResult.targets.Count; ++index)
                  {
                    TacticsUnitController unitController3 = this.self.FindUnitController(commandResult.targets[index].unit);
                    if (commandResult.skill.IsDamagedSkill())
                    {
                      unitController3.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Attack, skill, current);
                      long newHP = commandResult.targets[index].unit.GetCurrentHP() - (long) commandResult.targets[index].hp_damage;
                      int hpmax_damage = 0;
                      if (commandResult.skill.IsMhmDamage())
                      {
                        newHP = commandResult.targets[index].unit.GetCurrentHP();
                        hpmax_damage = commandResult.targets[index].hp_damage;
                      }
                      unitController3.SetHPChangeYosou(newHP, hpmax_damage);
                      this.self.mBattleUI.TargetSub.SetHpGaugeParam(commandResult.targets[index].unit.Side, commandResult.targets[index].unit.GetCurrentHP(), commandResult.targets[index].unit.GetMaximumHP(), commandResult.targets[index].hp_damage, is_max_damage: commandResult.skill.IsMhmDamage());
                    }
                    else
                    {
                      unitController3.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Target);
                      unitController3.SetHPChangeYosou(commandResult.targets[index].unit.GetCurrentHP() + (long) commandResult.targets[index].hp_heal);
                      this.self.mBattleUI.TargetSub.SetHpGaugeParam(commandResult.targets[index].unit.Side, commandResult.targets[index].unit.GetCurrentHP(), commandResult.targets[index].unit.GetMaximumHP(), YosokuHealHp: commandResult.targets[index].hp_heal);
                    }
                    this.self.mBattleUI.TargetSub.UpdateHpGauge();
                    this.self.UIParam_TargetValid = true;
                  }
                }
              }
              if (skill.IsTargetGridNoUnit)
              {
                this.self.UIParam_TargetValid = flag;
                if (this.self.UIParam_TargetValid && skill.EffectType == SkillEffectTypes.SetTrick)
                {
                  TrickData trickData = TrickData.SearchEffect(grid.x, grid.y);
                  if (trickData != null && trickData.TrickParam.IsNoOverWrite)
                    this.self.UIParam_TargetValid = false;
                }
              }
              else if (skill.IsTargetValidGrid)
                this.self.UIParam_TargetValid = true;
              else if (skill.IsCastSkill() && !this.self.UIParam_TargetValid)
                this.self.UIParam_TargetValid = true;
            }
          }
          else
            unit1 = this.self.mBattle.FindGimmickAtGrid(grid);
          this.mTargetPosition.x = grid.x;
          this.mTargetPosition.y = grid.y;
          this.SetYesButtonEnable(this.self.UIParam_TargetValid);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetMain, (UnityEngine.Object) null))
          {
            if (skill == null)
            {
              this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit);
              this.self.mBattleUI.TargetMain.Close();
            }
            else
            {
              if (!this.self.UIParam_TargetValid)
                this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit);
              this.self.mBattleUI.TargetMain.Open();
            }
          }
          this.self.mBattleUI.ClearEnableAll();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetSub, (UnityEngine.Object) null))
          {
            if (skill == null)
            {
              if (unit1 != null)
              {
                if (unit1.IsBreakObj && unit1.IsBreakDispUI)
                {
                  this.self.mBattleUI.TargetSub.ResetHpGauge(unit1.Side, unit1.GetCurrentHP(), unit1.GetMaximumHP());
                  this.self.mBattleUI.TargetSub.SetNoAction(unit1);
                  this.self.mBattleUI.TargetSub.Open();
                  this.self.mBattleUI.OnMapViewSelectUnit();
                  this.self.mBattleUI.IsEnableUnit = true;
                }
                else
                {
                  this.self.mBattleUI.OnMapViewSelectGrid();
                  this.self.mBattleUI.TargetSub.ForceClose(false);
                }
              }
              else
              {
                this.self.mBattleUI.OnMapViewSelectGrid();
                this.self.mBattleUI.TargetSub.ForceClose(false);
              }
            }
            else
            {
              unit1 = this.self.mBattle.FindGimmickAtGrid(grid);
              if (unit1 != null && unit1.IsBreakObj && unit1.IsBreakDispUI)
              {
                this.self.mBattleUI.TargetSub.ResetHpGauge(unit1.Side, unit1.GetCurrentHP(), unit1.GetMaximumHP());
                this.self.mBattleUI.TargetSub.SetNoAction(unit1);
                this.self.mBattleUI.TargetSub.Open();
              }
              else
                this.self.mBattleUI.TargetSub.ForceClose(false);
            }
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetObjectSub, (UnityEngine.Object) null))
          {
            if (skill == null)
            {
              if (unit1 != null)
              {
                if (!unit1.IsBreakObj)
                {
                  this.self.mBattleUI.TargetObjectSub.SetNoAction(unit1);
                  this.self.mBattleUI.TargetObjectSub.Open();
                  this.self.mBattleUI.OnMapViewSelectGrid();
                  this.self.mBattleUI.IsEnableGimmick = true;
                }
                else
                  this.self.mBattleUI.TargetObjectSub.Close();
              }
              else
              {
                this.self.mBattleUI.TargetObjectSub.Close();
                this.self.mBattleUI.OnMapViewSelectGrid();
              }
            }
            else
              this.self.mBattleUI.TargetObjectSub.Close();
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetTrickSub, (UnityEngine.Object) null))
          {
            if (skill == null)
            {
              TrickData trickData = TrickData.SearchEffect(grid.x, grid.y);
              if (trickData != null && trickData.IsVisualized())
              {
                this.self.mBattleUI.TargetTrickSub.SetTrick(trickData.TrickParam);
                this.self.mBattleUI.IsEnableTrick = true;
              }
              if (this.self.mBattleUI.IsEnableTrick && unit1 == null)
                this.self.mBattleUI.TargetTrickSub.Open();
              else
                this.self.mBattleUI.TargetTrickSub.Close();
              if (unit1 == null || !unit1.IsBreakObj)
                this.self.mBattleUI.OnMapViewSelectGrid();
            }
            else
              this.self.mBattleUI.TargetTrickSub.Close();
          }
          bool is_enable = this.self.mBattleUI.IsNeedFlip();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetSub, (UnityEngine.Object) null))
            this.self.mBattleUI.TargetSub.SetEnableFlipButton(is_enable);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetObjectSub, (UnityEngine.Object) null))
            this.self.mBattleUI.TargetObjectSub.SetEnableFlipButton(is_enable);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetTrickSub, (UnityEngine.Object) null))
            this.self.mBattleUI.TargetTrickSub.SetEnableFlipButton(is_enable);
          this.self.OnGimmickUpdate();
          this.self.SetUiHeight(grid.height);
        }
        current.x = intVector2_1.x;
        current.y = intVector2_1.y;
      }

      private bool IsGridSelectable(int x, int y) => this.mTargetGrids.get(x, y);

      private bool IsGridSelectable(Unit unit)
      {
        int x = unit.x;
        int y = unit.y;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mScene.mFocusedUnit, (UnityEngine.Object) null) && this.mScene.mFocusedUnit.Unit != null && this.mScene.mFocusedUnit.Unit == unit)
        {
          IntVector2 intVector2 = this.mScene.CalcCoord(this.mScene.mFocusedUnit.CenterPosition);
          x = intVector2.x;
          y = intVector2.y;
        }
        if (!unit.IsNormalSize)
        {
          for (int idx = 0; idx < unit.SizeX * unit.SizeY; ++idx)
          {
            IntVector2 bigUnitOffsetPos = this.self.mBattle.GetBigUnitOffsetPos(unit, idx);
            if (this.IsGridSelectable(x + bigUnitOffsetPos.x, y + bigUnitOffsetPos.y))
              return true;
          }
        }
        return this.IsGridSelectable(x, y);
      }

      private bool IsValidSkillTarget(Unit target)
      {
        return target != null && this.self.mTargetSelectorParam.Skill != null && this.IsGridSelectable(target) && this.self.mBattle.CheckSkillTarget(this.self.mBattle.CurrentUnit, target, this.self.mTargetSelectorParam.Skill);
      }

      private void OnFocus(TacticsUnitController controller, Grid focus_grid = null)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) controller, (UnityEngine.Object) null) || this.IsValidForcedTargeting(controller.Unit))
          return;
        SkillData skill = this.self.mTargetSelectorParam.Skill;
        if (skill == null && controller.Unit.IsGimmick)
        {
          if (this.self.mBattle.CurrentMap == null)
            return;
          this.OnFocusGrid(this.self.Battle.CurrentMap[controller.Unit.x, controller.Unit.y]);
        }
        else
        {
          if (skill != null)
          {
            if (skill.IsAllEffect())
              controller = this.self.FindUnitController(this.self.mBattle.CurrentUnit);
            else if (!this.IsValidSkillTarget(controller.Unit))
            {
              for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
              {
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mTacticsUnits[index], (UnityEngine.Object) controller) && this.self.mTacticsUnits[index].Unit.x == controller.Unit.x && this.self.mTacticsUnits[index].Unit.y == controller.Unit.y && this.IsValidSkillTarget(this.self.mTacticsUnits[index].Unit))
                {
                  controller = this.self.mTacticsUnits[index];
                  break;
                }
              }
            }
          }
          if (controller.Unit.IsGimmick && controller.Unit.IsDisableGimmick())
            return;
          if (!this.mSelectGrid)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSelectedTarget, (UnityEngine.Object) null))
              this.self.HideUnitMarkers(this.mSelectedTarget.Unit);
            this.self.ShowUnitMarker(controller.Unit, SceneBattle.UnitMarkerTypes.Target);
          }
          this.mSelectedTarget = controller;
          IntVector2 intVector2_1 = this.self.CalcCoord(controller.CenterPosition);
          this.mTargetPosition.x = intVector2_1.x;
          this.mTargetPosition.y = intVector2_1.y;
          if (focus_grid != null)
          {
            this.mTargetPosition.x = focus_grid.x;
            this.mTargetPosition.y = focus_grid.y;
          }
          this.self.mSelectedTarget = controller.Unit;
          this.self.UIParam_TargetValid = false;
          Unit currentUnit = this.self.mBattle.CurrentUnit;
          TacticsUnitController unitController1 = this.self.FindUnitController(currentUnit);
          int hpCost = skill == null ? 0 : skill.GetHpCost(currentUnit);
          if (skill == null)
            this.self.ShowWalkableGrids(this.self.mBattle.CreateMoveMap(controller.Unit, (int) controller.Unit.CurrentStatus.param.mov), 0);
          else if (skill.IsTargetTeleport)
          {
            this.self.mTacticsSceneRoot.HideGridLayer(0);
            this.self.mDisplayBlockedGridMarker = false;
            this.self.mGridDisplayBlockedGridMarker = (Grid) null;
          }
          unitController1.SetHPChangeYosou(currentUnit.GetCurrentHP() - (long) hpCost);
          this.self.mBattleUI.TargetMain.SetHpGaugeParam(currentUnit.Side, currentUnit.GetCurrentHP(), currentUnit.GetMaximumHP(), hpCost);
          this.self.mBattleUI.TargetMain.UpdateHpGauge();
          this.self.mBattleUI.TargetSub.ResetHpGauge(this.self.mSelectedTarget.Side, this.self.mSelectedTarget.GetCurrentHP(), this.self.mSelectedTarget.GetMaximumHP());
          bool flag1 = false;
          BattleCore.UnitResult unitResult = (BattleCore.UnitResult) null;
          if (this.self.mTargetSelectorParam.Skill != null)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) controller, (UnityEngine.Object) null) && this.self.mTutorialTriggers != null)
            {
              for (int index = 0; index < this.self.mTutorialTriggers.Length; ++index)
                this.self.mTutorialTriggers[index].OnTargetChange(this.mSelectedTarget.Unit, controller.Unit.TurnCount);
            }
            for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
            {
              this.self.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal);
              if (0 >= hpCost || !UnityEngine.Object.op_Equality((UnityEngine.Object) this.self.mTacticsUnits[index], (UnityEngine.Object) unitController1))
                this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue);
            }
            if (this.self.mTargetSelectorParam.OnAccept != null && this.IsGridSelectable(this.self.mSelectedTarget))
            {
              IntVector2 intVector2_2 = this.self.CalcCoord(unitController1.CenterPosition);
              int x = this.self.mSelectedTarget.x;
              int y = this.self.mSelectedTarget.y;
              bool flag2 = true;
              if (focus_grid != null && skill != null && (skill.IsAreaSkill() || skill.IsTargetValidGrid))
              {
                x = focus_grid.x;
                y = focus_grid.y;
                flag2 = this.IsGridSelectable(x, y);
              }
              if (currentUnit == this.self.mSelectedTarget)
              {
                x = intVector2_2.x;
                y = intVector2_2.y;
              }
              if (!this.mSelectGrid)
                this.HilitArea(x, y);
              if (!skill.IsTargetGridNoUnit)
              {
                if (skill.IsTargetValidGrid)
                  this.self.UIParam_TargetValid = flag2;
                else if (flag2)
                {
                  BattleCore.CommandResult commandResult = this.self.mBattle.GetCommandResult(currentUnit, intVector2_2.x, intVector2_2.y, x, y, skill, true);
                  Unit protectTarget = this.self.Battle.GetProtectTarget(this.self.mSelectedTarget, skill);
                  if (protectTarget != null)
                    this.self.mSelectedTarget = protectTarget;
                  if (commandResult != null && commandResult.targets != null)
                  {
                    for (int index = 0; index < commandResult.reactions.Count; ++index)
                    {
                      if (this.self.mSelectedTarget == commandResult.reactions[index].react_unit && (currentUnit == commandResult.reactions[index].unit || commandResult.reactions[index].react_unit == commandResult.reactions[index].unit))
                      {
                        unitResult = commandResult.reactions[index];
                        break;
                      }
                    }
                    for (int index = 0; index < commandResult.targets.Count; ++index)
                    {
                      TacticsUnitController unitController2 = this.self.FindUnitController(commandResult.targets[index].unit);
                      if (commandResult.skill.IsDamagedSkill())
                      {
                        unitController2.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Attack, skill, currentUnit);
                        long newHP = commandResult.targets[index].unit.GetCurrentHP() - (long) commandResult.targets[index].hp_damage;
                        int hpmax_damage = 0;
                        if (commandResult.skill.IsMhmDamage())
                        {
                          newHP = commandResult.targets[index].unit.GetCurrentHP();
                          hpmax_damage = commandResult.targets[index].hp_damage;
                        }
                        unitController2.SetHPChangeYosou(newHP, hpmax_damage);
                      }
                      else
                      {
                        unitController2.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Target);
                        unitController2.SetHPChangeYosou(commandResult.targets[index].unit.GetCurrentHP() + (long) commandResult.targets[index].hp_heal);
                      }
                    }
                    if (commandResult.self_effect.hp_damage > 0 || commandResult.self_effect.hp_heal > 0)
                    {
                      long newHP = Math.Min(currentUnit.GetCurrentHP() - (long) commandResult.self_effect.hp_damage + (long) commandResult.self_effect.hp_heal - (long) hpCost, currentUnit.GetMaximumHP());
                      unitController1.SetHPChangeYosou(newHP);
                      this.self.mBattleUI.TargetMain.SetHpGaugeParam(currentUnit.Side, currentUnit.GetCurrentHP(), currentUnit.GetMaximumHP(), commandResult.self_effect.hp_damage + hpCost, commandResult.self_effect.hp_heal);
                      this.self.mBattleUI.TargetMain.UpdateHpGauge();
                    }
                    if (commandResult.targets.Count > 0)
                    {
                      bool flag3 = skill.IsNormalAttack();
                      int index1 = commandResult.targets.FindIndex((Predicate<BattleCore.UnitResult>) (p => p.unit == this.self.mSelectedTarget));
                      if (index1 != -1 && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetMain, (UnityEngine.Object) null))
                      {
                        BattleCore.UnitResult target = commandResult.targets[index1];
                        EUnitSide side = commandResult.targets[index1].unit.Side;
                        long currentHp = commandResult.targets[index1].unit.GetCurrentHP();
                        long maximumHp = commandResult.targets[index1].unit.GetMaximumHP();
                        if (skill.SkillParam.IsHealSkill())
                        {
                          this.self.mBattleUI.TargetMain.SetHealAction(this.self.mBattle.CurrentUnit, target.hp_heal, target.critical, target.avoid);
                          this.self.mBattleUI.TargetSub.SetHpGaugeParam(side, currentHp, maximumHp, YosokuHealHp: commandResult.targets[index1].hp_heal);
                        }
                        else if (skill.SkillParam.IsDamagedSkill() || skill.SkillParam.effect_type == SkillEffectTypes.Debuff)
                        {
                          if (flag3)
                            this.self.mBattleUI.TargetMain.SetAttackAction(this.self.mBattle.CurrentUnit, target.hp_damage <= 0 ? target.mp_damage : target.hp_damage, target.critical, 100 - target.avoid, target.cond_hit_lists);
                          else
                            this.self.mBattleUI.TargetMain.SetSkillAction(this.self.mBattle.CurrentUnit, target.hp_damage <= 0 ? target.mp_damage : target.hp_damage, target.critical, 100 - target.avoid, target.cond_hit_lists);
                          this.self.mBattleUI.TargetSub.SetHpGaugeParam(side, currentHp, maximumHp, commandResult.targets[index1].hp_damage, is_max_damage: skill.IsMhmDamage());
                        }
                        else
                          this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit, target.cond_hit_lists);
                        this.self.mBattleUI.TargetSub.UpdateHpGauge();
                        flag1 = true;
                      }
                      this.self.UIParam_TargetValid = true;
                      if (!skill.IsAreaSkill() && this.self.mSelectedTarget != null)
                        this.self.UIParam_TargetValid = this.self.Battle.CheckSkillTarget(currentUnit, this.self.mSelectedTarget, skill);
                      if (skill.IsTargetTeleport && this.self.mSelectedTarget != null)
                      {
                        bool is_teleport = false;
                        Grid teleportGrid = this.self.mBattle.GetTeleportGrid(currentUnit, intVector2_2.x, intVector2_2.y, this.self.mSelectedTarget, skill, ref is_teleport);
                        if (teleportGrid != null && this.self.mBattle.CurrentMap != null)
                        {
                          BattleMap currentMap = this.self.mBattle.CurrentMap;
                          GridMap<int> accessMap = new GridMap<int>(currentMap.Width, currentMap.Height);
                          accessMap.fill(-1);
                          accessMap.set(teleportGrid.x, teleportGrid.y, 0);
                          this.self.ShowWalkableGrids(accessMap, 0);
                        }
                        if (teleportGrid == null)
                        {
                          for (int index2 = 0; index2 < commandResult.targets.Count; ++index2)
                          {
                            if (commandResult.targets[index2] != null)
                            {
                              TacticsUnitController unitController3 = this.self.FindUnitController(commandResult.targets[index2].unit);
                              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController3, (UnityEngine.Object) null))
                              {
                                unitController3.ResetHPGauge();
                                this.self.mBattleUI.TargetSub.ResetHpGauge(commandResult.targets[index2].unit.Side, commandResult.targets[index2].unit.GetCurrentHP(), (long) commandResult.targets[index2].unit.MaximumStatusHp);
                              }
                            }
                          }
                          this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit);
                        }
                        if (!is_teleport)
                        {
                          this.self.mDisplayBlockedGridMarker = true;
                          this.self.mGridDisplayBlockedGridMarker = teleportGrid;
                          this.self.UIParam_TargetValid = false;
                        }
                      }
                      this.self.mBattleUI.TargetSub.SetProtectAction(protectTarget != null && protectTarget == this.self.mSelectedTarget);
                    }
                  }
                  else if (skill.IsCastSkill() && skill.IsAreaSkill())
                    this.self.UIParam_TargetValid = true;
                }
              }
            }
          }
          else
            this.HilitNormalAttack(controller.Unit);
          this.SetYesButtonEnable(this.self.UIParam_TargetValid);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetMain, (UnityEngine.Object) null))
          {
            if (skill == null)
            {
              this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit);
              this.self.mBattleUI.TargetMain.Close();
            }
            else
            {
              if (!flag1)
                this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit);
              this.self.mBattleUI.TargetMain.Open();
            }
          }
          this.self.mBattleUI.ClearEnableAll();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetSub, (UnityEngine.Object) null))
          {
            if (skill == null)
            {
              if (controller.Unit.IsGimmick)
              {
                this.self.mBattleUI.TargetSub.ForceClose(false);
                this.self.mBattleUI.OnMapViewSelectGrid();
              }
              else
              {
                this.self.mBattleUI.TargetSub.SetNoAction(this.self.mSelectedTarget);
                this.self.mBattleUI.TargetSub.Open();
                this.self.mBattleUI.OnMapViewSelectUnit();
                this.self.mBattleUI.IsEnableUnit = true;
              }
            }
            else if (flag1)
            {
              if (!this.self.mSelectedTarget.IsBreakObj || this.self.mSelectedTarget.IsBreakDispUI)
              {
                if (unitResult != null)
                  this.self.mBattleUI.TargetSub.SetAttackAction(this.self.mSelectedTarget, unitResult.hp_damage <= 0 ? unitResult.mp_damage : unitResult.hp_damage, 0, 100 - unitResult.avoid, unitResult.cond_hit_lists);
                else
                  this.self.mBattleUI.TargetSub.SetNoAction(this.self.mSelectedTarget);
                this.self.mBattleUI.TargetSub.Open();
              }
              else
                this.self.mBattleUI.TargetSub.ForceClose(false);
            }
            else if (this.self.mSelectedTarget.UnitType == EUnitType.Unit || this.self.mSelectedTarget.UnitType == EUnitType.EventUnit || this.self.mSelectedTarget.IsBreakObj && this.self.mSelectedTarget.IsBreakDispUI)
            {
              this.self.mBattleUI.TargetSub.SetNoAction(this.self.mSelectedTarget);
              this.self.mBattleUI.TargetSub.Open();
            }
            else
              this.self.mBattleUI.TargetSub.ForceClose(false);
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetObjectSub, (UnityEngine.Object) null))
          {
            if (skill == null)
            {
              if (controller.Unit.IsGimmick && !controller.Unit.IsDisableGimmick())
              {
                this.self.mBattleUI.OnMapViewSelectGrid();
                this.self.mBattleUI.TargetObjectSub.Open();
                this.self.mBattleUI.IsEnableGimmick = true;
              }
              else
              {
                this.self.mBattleUI.OnMapViewSelectUnit();
                this.self.mBattleUI.TargetObjectSub.Close();
              }
            }
            else
              this.self.mBattleUI.TargetObjectSub.Close();
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetTrickSub, (UnityEngine.Object) null))
          {
            IntVector2 intVector2_3 = new IntVector2(intVector2_1.x, intVector2_1.y);
            if (focus_grid != null)
            {
              intVector2_3.x = focus_grid.x;
              intVector2_3.y = focus_grid.y;
            }
            TrickData trickData = TrickData.SearchEffect(intVector2_3.x, intVector2_3.y);
            if (trickData != null && trickData.IsVisualized())
            {
              this.self.mBattleUI.TargetTrickSub.SetTrick(trickData.TrickParam);
              this.self.mBattleUI.IsEnableTrick = true;
            }
            this.self.mBattleUI.TargetTrickSub.Close();
          }
          bool is_enable = this.self.mBattleUI.IsNeedFlip();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetSub, (UnityEngine.Object) null))
            this.self.mBattleUI.TargetSub.SetEnableFlipButton(is_enable);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetObjectSub, (UnityEngine.Object) null))
            this.self.mBattleUI.TargetObjectSub.SetEnableFlipButton(is_enable);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetTrickSub, (UnityEngine.Object) null))
            this.self.mBattleUI.TargetTrickSub.SetEnableFlipButton(is_enable);
          this.self.OnGimmickUpdate();
          this.self.SetUnitUiHeight(controller.Unit);
        }
      }

      public override void Update(SceneBattle self)
      {
        if (this.mDragScroll)
        {
          this.mYScrollPos += (float) (((double) self.mTouchController.DragDelta.y <= 0.0 ? -1.0 : 1.0) * (double) Time.unscaledDeltaTime * 2.0);
          if (!this.mIgnoreDragVelocity)
            this.mYScrollPos += this.mDragY / 20f;
          if ((double) this.mYScrollPos <= -1.0)
          {
            this.mYScrollPos = 0.0f;
            this.mIgnoreDragVelocity = true;
            if (self.mTargetSelectorParam.Skill != null)
              this.ShiftTarget(-1);
          }
          else if ((double) this.mYScrollPos >= 1.0)
          {
            this.mYScrollPos = 0.0f;
            this.mIgnoreDragVelocity = true;
            if (self.mTargetSelectorParam.Skill != null)
              this.ShiftTarget(1);
          }
        }
        if (!this.mSelectGrid || self.m_TargetCameraPositionInterp)
          return;
        int x = Mathf.Clamp(Mathf.FloorToInt(self.m_CameraPosition.x), 0, self.mBattle.CurrentMap.Width);
        int y = Mathf.Clamp(Mathf.FloorToInt(self.m_CameraPosition.z), 0, self.mBattle.CurrentMap.Height);
        if (this.mTargetPosition.x == x && this.mTargetPosition.y == y)
          return;
        Grid current = self.mBattle.CurrentMap[x, y];
        if (current == null)
          return;
        this.OnFocusGrid(current);
      }

      private void ShiftTarget(int delta)
      {
        if (this.mTargets.Count == 0)
          return;
        TacticsUnitController tacticsUnitController = this.self.mFocusedUnit;
        if (this.self.mTargetSelectorParam.Skill == null)
          tacticsUnitController = this.self.mMapModeFocusedUnit;
        int num = this.mTargets.IndexOf(tacticsUnitController);
        if (num < 0)
          num = 0;
        this.OnClickUnit(this.mTargets[(num + delta + this.mTargets.Count) % this.mTargets.Count]);
        if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) BattleUnitDetail.Instance))
          return;
        BattleUnitDetail.Instance.Refresh(this.self.mFocusedUnit.Unit);
      }
    }

    private class State_InputDirection : State<SceneBattle>
    {
      private DirectionArrow[] mArrows = new DirectionArrow[4];
      private int mSelectedDirection = -1;
      private Unit mCurrentUnit;
      private TacticsUnitController mController;
      private bool mIsStepEnd;
      private bool mCancelButtonActive = true;
      private bool mIsAuto;

      public override void Begin(SceneBattle self)
      {
        this.mCurrentUnit = self.mBattle.CurrentUnit;
        this.mController = self.FindUnitController(this.mCurrentUnit);
        this.mController.AutoUpdateRotation = true;
        self.ToggleRenkeiAura(false);
        self.InterpCameraTarget((Component) this.mController);
        self.HideAllHPGauges();
        self.HideAllUnitOwnerIndex();
        Selectable component = self.mBattleUI.CommandWindow.CancelButton.GetComponent<Selectable>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          this.mCancelButtonActive = component.interactable;
          bool flag = this.mCurrentUnit.IsEnableMoveCondition();
          component.interactable = this.mCurrentUnit.IsEnableActionCondition() || flag;
        }
        self.mBattleUI.CommandWindow.OKButton.SetActive(true);
        IntVector2 intVector2 = self.CalcCoord(this.mController.CenterPosition);
        Grid current = self.mBattle.CurrentMap[intVector2.x, intVector2.y];
        this.mController.StepTo(self.CalcGridCenter(current));
        this.mCurrentUnit.Direction = self.FindUnitController(self.mBattle.CurrentUnit).CalcUnitDirectionFromRotation();
      }

      private void OnStepEnd()
      {
        this.self.mOnScreenClick = new SceneBattle.ScreenClickEvent(this.OnScreenClick);
        for (int direction = 0; direction < 4; ++direction)
        {
          Quaternion rotation = ((EUnitDirection) direction).ToRotation();
          Vector3 vector3 = Quaternion.op_Multiply(rotation, ((Component) this.self.DirectionArrowTemplate).transform.position);
          this.mArrows[direction] = UnityEngine.Object.Instantiate<DirectionArrow>(this.self.DirectionArrowTemplate, Vector3.op_Addition(((Component) this.mController).transform.position, vector3), rotation);
        }
        this.UpdateArrows();
        this.self.mOnRequestStateChange = new SceneBattle.StateTransitionRequest(this.OnStateChange);
        this.self.mOnScreenClick = new SceneBattle.ScreenClickEvent(this.OnScreenClick);
        this.self.mBattleUI.CommandWindow.OnYesNoSelect += new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        TacticsUnitController unitController = this.self.FindUnitController(this.self.mBattle.CurrentUnit);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) || this.self.mTutorialTriggers == null)
          return;
        for (int index = 0; index < this.self.mTutorialTriggers.Length; ++index)
          this.self.mTutorialTriggers[index].OnSelectDirectionStart(this.mCurrentUnit, unitController.Unit.TurnCount);
      }

      private void OnYesNoSelect(bool yes)
      {
        if (yes)
        {
          this.SelectDirection(this.self.mBattle.CurrentUnit.Direction);
          this.self.Battle.MapCommandEnd(this.self.Battle.CurrentUnit);
          if (!this.self.Battle.IsMultiPlay)
            return;
          this.self.SendInputEntryBattle(EBattleCommand.Wait, this.self.Battle.CurrentUnit, (Unit) null, (SkillData) null, (ItemData) null, this.self.Battle.CurrentUnit.x, this.self.Battle.CurrentUnit.y, false);
        }
        else
        {
          this.self.mBattleUI.OnInputDirectionEnd();
          this.self.ToggleRenkeiAura(true);
          this.self.GotoMapCommand();
        }
      }

      private void UpdateArrows()
      {
        for (int index = 0; index < 4; ++index)
        {
          EUnitDirection eunitDirection = (EUnitDirection) index;
          this.mArrows[index].State = eunitDirection != this.mCurrentUnit.Direction ? DirectionArrow.ArrowStates.Normal : DirectionArrow.ArrowStates.Hilit;
          this.mArrows[index].Direction = eunitDirection;
        }
      }

      public override void Update(SceneBattle self)
      {
        self.Battle.IsAutoBattle = self.Battle.RequestAutoBattle;
        if (this.mController.isIdle && !this.mIsStepEnd)
        {
          this.OnStepEnd();
          this.mIsStepEnd = true;
          if (self.Battle.IsAutoBattle)
          {
            this.SelectDirection(this.mCurrentUnit.Direction);
            return;
          }
        }
        if (self.Battle.IsAutoBattle)
          this.SelectDirection(this.mCurrentUnit.Direction);
        else if ((double) Time.timeScale <= 0.0)
        {
          if (self.Battle.IsAutoBattle)
            this.mIsAuto = true;
          else
            this.mIsAuto = false;
        }
        else if (this.mIsAuto)
          this.SelectDirection(this.mCurrentUnit.Direction);
        else if (self.Battle.IsAutoBattle)
        {
          if ((double) Time.timeScale > 0.0)
            return;
          this.SelectDirection(this.mCurrentUnit.Direction);
        }
        else
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mTouchController, (UnityEngine.Object) null))
            return;
          Vector2 worldSpaceVelocity = self.mTouchController.WorldSpaceVelocity;
          if ((double) ((Vector2) ref worldSpaceVelocity).magnitude <= 5.0)
            return;
          ((Vector2) ref worldSpaceVelocity).Normalize();
          this.mCurrentUnit.Direction = TacticsUnitController.CalcUnitDirection(worldSpaceVelocity.x, worldSpaceVelocity.y);
          this.UpdateArrows();
        }
      }

      public override void End(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self, (UnityEngine.Object) null) && self.mBattle != null)
          self.mBattle.SetManualPlayFlag();
        self.mBattleUI.CommandWindow.OnYesNoSelect -= new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        for (int index = 0; index < 4; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mArrows[index], (UnityEngine.Object) null) && index != this.mSelectedDirection)
            this.mArrows[index].State = DirectionArrow.ArrowStates.Close;
        }
        Selectable component = self.mBattleUI.CommandWindow.CancelButton.GetComponent<Selectable>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.interactable = this.mCancelButtonActive;
        self.mOnScreenClick = (SceneBattle.ScreenClickEvent) null;
        self.mOnRequestStateChange = (SceneBattle.StateTransitionRequest) null;
      }

      private void OnScreenClick(Vector2 position)
      {
        Camera main = Camera.main;
        float num1 = (float) (Screen.height / 5);
        int dir = -1;
        float num2 = float.MaxValue;
        for (int index = 0; index < this.mArrows.Length; ++index)
        {
          Vector2 vector2 = Vector2.op_Subtraction(Vector2.op_Implicit(main.WorldToScreenPoint(((Component) this.mArrows[index]).transform.position)), position);
          float magnitude = ((Vector2) ref vector2).magnitude;
          if ((double) magnitude < (double) num1 && (double) magnitude < (double) num2)
          {
            num2 = magnitude;
            dir = index;
          }
        }
        if (dir == -1)
          return;
        this.SelectDirection((EUnitDirection) dir);
      }

      public void SelectDirection(EUnitDirection dir)
      {
        this.self.ApplyUnitMovement();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mArrows[(int) dir], (UnityEngine.Object) null))
        {
          this.mArrows[(int) dir].State = DirectionArrow.ArrowStates.Press;
          MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0003");
        }
        this.mSelectedDirection = (int) dir;
        this.self.mBattleUI.OnInputDirectionEnd();
        if (this.self.Battle.IsMultiPlay)
        {
          this.self.SendInputUnitEnd(this.self.Battle.CurrentUnit, dir);
          this.self.SendInputFlush();
        }
        this.self.mBattle.CommandWait(dir);
        this.self.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
      }

      private void OnStateChange(SceneBattle.StateTransitionTypes req)
      {
        if (req == SceneBattle.StateTransitionTypes.Forward)
          return;
        this.self.mBattleUI.OnInputDirectionEnd();
        this.self.GotoMapCommand();
      }
    }

    private class State_SelectSkillV2 : State<SceneBattle>
    {
      private UnitAbilitySkillList mSkillList;

      public override void Begin(SceneBattle self)
      {
        self.InterpCameraTargetToCurrent();
        this.mSkillList = self.mBattleUI.SkillWindow;
        Unit currentUnit = self.mBattle.CurrentUnit;
        TacticsUnitController unitController = self.FindUnitController(currentUnit);
        EUnitDirection direction = unitController.CalcUnitDirectionFromRotation();
        ((Component) unitController).transform.rotation = direction.ToRotation();
        self.mOnRequestStateChange = new SceneBattle.StateTransitionRequest(this.OnStateChange);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillList, (UnityEngine.Object) null))
          this.mSkillList.OnSelectSkill = new UnitAbilitySkillList.SelectSkillEvent(this.OnSelectSkill);
        self.StepToNear(currentUnit);
      }

      public override void End(SceneBattle self)
      {
        self.mOnRequestStateChange = (SceneBattle.StateTransitionRequest) null;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillList, (UnityEngine.Object) null))
          return;
        this.mSkillList.OnSelectSkill = (UnitAbilitySkillList.SelectSkillEvent) null;
      }

      private void OnSelectSkill(SkillData skill)
      {
        this.self.mBattleUI.OnSkillSelectEnd();
        this.self.GotoSelectTarget(skill, new SceneBattle.SelectTargetCallback(this.self.GotoSkillSelect), new SceneBattle.SelectTargetPositionWithSkill(this.self.OnSelectSkillTarget));
      }

      private void OnStateChange(SceneBattle.StateTransitionTypes req)
      {
        if (req == SceneBattle.StateTransitionTypes.Forward)
        {
          this.self.mBattleUI.OnSkillSelectEnd();
        }
        else
        {
          this.self.mBattleUI.OnSkillSelectEnd();
          this.self.GotoMapCommand();
        }
      }
    }

    private class State_SelectGridEventV2 : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.InterpCameraTargetToCurrent();
        self.mBattleUI.OnGridEventSelectStart();
        self.mOnRequestStateChange = new SceneBattle.StateTransitionRequest(this.OnStateChange);
      }

      public override void End(SceneBattle self)
      {
        self.mOnRequestStateChange = (SceneBattle.StateTransitionRequest) null;
      }

      private void OnStateChange(SceneBattle.StateTransitionTypes req)
      {
        if (req == SceneBattle.StateTransitionTypes.Forward)
        {
          if (!this.self.ApplyUnitMovement())
            return;
          if (this.self.Battle.ExecuteEventTriggerOnGrid(this.self.Battle.CurrentUnit, EEventTrigger.ExecuteOnGrid))
            this.self.SendInputGridEvent(this.self.Battle.CurrentUnit);
          this.self.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
        }
        else
        {
          this.self.mBattleUI.OnGridEventSelectEnd();
          this.self.GotoMapCommand();
        }
      }
    }

    public enum eMaskBattleUI
    {
      MENU = 1,
      MAP = 2,
      CAMERA = 4,
      CMD_ATTACK = 8,
      CMD_ABILITYS = 16, // 0x00000010
      CMD_ITEM = 32, // 0x00000020
      CMD_END = 64, // 0x00000040
      VS_GRID_TAP = 128, // 0x00000080
      VS_SWIPE = 256, // 0x00000100
      BACK_KEY = 512, // 0x00000200
    }

    private enum UnitMarkerTypes
    {
      Target,
      Enemy,
      Assist,
    }

    private delegate void GridClickEvent(Grid grid);

    private delegate void UnitClickEvent(TacticsUnitController controller);

    private delegate void UnitFocusEvent(TacticsUnitController controller, Grid focus_grid = null);

    private delegate void ScreenClickEvent(Vector2 position);

    private delegate void FocusTargetEvent(Unit unit);

    private delegate void SelectTargetEvent(Unit unit);

    private delegate void CancelTargetSelectEvent();

    private class PupupData
    {
      public int priority;
      public Vector3 position;
      public float yOffset;

      public PupupData(Vector3 position, int priority, float yOffset)
      {
        this.priority = priority;
        this.position = position;
        this.yOffset = yOffset;
      }
    }

    public class VirtualStickInput : SceneBattle.MoveInput
    {
      private TacticsUnitController mController;
      private GridMap<int> mWalkableGrids;
      private Vector3 mStart;
      private int mDestX;
      private int mDestY;
      private Vector2 mBasePos = Vector2.zero;
      private Vector2 mTargetPos = Vector2.zero;
      private bool mTargetSet;
      private bool mMoveStarted;
      private bool mClickedOK;
      private float mGridSnapTime;
      private bool mJumping;
      private bool mHasInput;
      private int mCurrentX;
      private int mCurrentY;
      private GridMap<bool> mShateiGrid;
      private float mStopTime;
      private bool mRouteSet;
      private bool mFullAccel;
      private bool mHasDesiredRotation;
      private bool mGridSnapping;
      private Quaternion mDesiredRotation;
      private const float STOP_RADIUS = 0.1f;
      private bool mUpdateShateiGrid;
      private bool mShateiVisible;

      public override bool IsBusy
      {
        get
        {
          return this.mHasInput || this.mGridSnapping || (double) this.mGridSnapTime >= 0.0 || this.mHasDesiredRotation;
        }
      }

      public override void Reset()
      {
        ((Component) this.mController).transform.position = this.mStart;
        this.mController.CancelAction();
        this.SceneOwner.SetCameraTarget((Component) this.mController);
        this.SceneOwner.SendInputMoveEnd(this.SceneOwner.Battle.CurrentUnit, true);
        this.mMoveStarted = false;
      }

      public override void Start()
      {
        this.SceneOwner.m_AllowCameraTranslation = false;
        this.mController = this.SceneOwner.FindUnitController(this.SceneOwner.Battle.CurrentUnit);
        this.mController.AutoUpdateRotation = false;
        this.SceneOwner.ShowUnitCursorOnCurrent();
        BattleMap currentMap = this.SceneOwner.Battle.CurrentMap;
        this.mWalkableGrids = this.SceneOwner.CreateCurrentAccessMap();
        this.mController.WalkableField = this.mWalkableGrids;
        this.SceneOwner.ShowWalkableGrids(this.mWalkableGrids, 0);
        IntVector2 intVector2 = this.SceneOwner.CalcCoord(this.mController.CenterPosition);
        this.mStart = this.SceneOwner.CalcGridCenter(currentMap[intVector2.x, intVector2.y]);
        this.mDestX = intVector2.x;
        this.mDestY = intVector2.y;
        this.mCurrentX = this.mDestX;
        this.mCurrentY = this.mDestY;
        this.mBasePos.x = (float) intVector2.x + 0.5f;
        this.mBasePos.y = (float) intVector2.y + 0.5f;
        this.mTargetPos = this.mBasePos;
        this.SceneOwner.ResetMoveCamera();
        this.SceneOwner.SendInputMoveStart(this.SceneOwner.Battle.CurrentUnit);
        this.SceneOwner.mTouchController.OnClick += new TouchController.ClickEvent(this.OnClick);
        this.SceneOwner.mTouchController.OnDragDelegate += new TouchController.DragEvent(this.OnDrag);
        this.SceneOwner.mTouchController.OnDragEndDelegate += new TouchController.DragEvent(this.OnDragEnd);
        this.RecalcAttackTargets();
      }

      public override void End()
      {
        this.SceneOwner.mTouchController.OnClick -= new TouchController.ClickEvent(this.OnClick);
        this.SceneOwner.mTouchController.OnDragDelegate -= new TouchController.DragEvent(this.OnDrag);
        this.SceneOwner.mTouchController.OnDragEndDelegate -= new TouchController.DragEvent(this.OnDragEnd);
        this.SceneOwner.m_AllowCameraTranslation = true;
        this.SceneOwner.mDisplayBlockedGridMarker = false;
        List<TacticsUnitController> mTacticsUnits = this.SceneOwner.mTacticsUnits;
        for (int index = 0; index < mTacticsUnits.Count; ++index)
        {
          mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal);
          mTacticsUnits[index].SetHPChangeYosou(mTacticsUnits[index].VisibleHPValue);
        }
        if (this.SceneOwner.Battle.EntryBattleMultiPlayTimeUp)
          this.mController.AutoUpdateRotation = true;
        this.mController.StopRunning();
        this.mController.WalkableField = (GridMap<int>) null;
        this.SceneOwner.HideUnitCursor(this.mController);
        this.SceneOwner.mTacticsSceneRoot.HideGridLayer(0);
      }

      public override void MoveUnit(Vector3 target_screen_pos)
      {
        this.OnClick(Vector2.op_Implicit(target_screen_pos));
      }

      private void OnDrag()
      {
      }

      private void OnDragEnd()
      {
        this.SceneOwner.SendInputGridXY(this.SceneOwner.Battle.CurrentUnit, this.mDestX, this.mDestY, this.SceneOwner.Battle.CurrentUnit.Direction);
      }

      private void OnClick(Vector2 screenPos)
      {
        if (this.SceneOwner.Battle.EntryBattleMultiPlayTimeUp || !this.SceneOwner.IsControlBattleUI(SceneBattle.eMaskBattleUI.VS_GRID_TAP))
          return;
        Unit unit1 = this.mController.Unit;
        if (this.mShateiGrid != null)
        {
          RectTransform transform = ((Component) this.SceneOwner.mTouchController).transform as RectTransform;
          Vector2 vector2_1;
          RectTransformUtility.ScreenPointToLocalPointInRectangle(transform, screenPos, (Camera) null, ref vector2_1);
          Vector2 vector2_2 = vector2_1;
          Rect rect = transform.rect;
          Vector2 position = ((Rect) ref rect).position;
          Vector2 vector2_3 = Vector2.op_Subtraction(vector2_2, position);
          Unit unit2 = (Unit) null;
          float num = float.MaxValue;
          SkillData attackSkill = unit1.GetAttackSkill();
          for (int index = 0; index < this.SceneOwner.mTacticsUnits.Count; ++index)
          {
            TacticsUnitController mTacticsUnit = this.SceneOwner.mTacticsUnits[index];
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) mTacticsUnit, (UnityEngine.Object) null))
            {
              Unit unit3 = mTacticsUnit.Unit;
              if (unit3 != null)
              {
                RectTransform hpGaugeTransform = mTacticsUnit.HPGaugeTransform;
                if (!UnityEngine.Object.op_Equality((UnityEngine.Object) hpGaugeTransform, (UnityEngine.Object) null) && ((Component) hpGaugeTransform).gameObject.activeInHierarchy && this.mShateiGrid.isValid(unit3.x, unit3.y) && this.mShateiGrid.get(unit3.x, unit3.y) && this.SceneOwner.mBattle.CheckSkillTarget(unit1, unit3, attackSkill))
                {
                  Vector2 vector2_4 = Vector2.op_Subtraction(hpGaugeTransform.anchoredPosition, vector2_3);
                  float magnitude = ((Vector2) ref vector2_4).magnitude;
                  if ((double) magnitude < (double) num && (double) magnitude < 80.0)
                  {
                    unit2 = unit3;
                    num = magnitude;
                  }
                }
              }
            }
          }
          if (unit2 != null)
          {
            this.SelectAttackTarget(unit2);
            return;
          }
        }
        IntVector2 intVector2_1 = this.SceneOwner.CalcClickedGrid(screenPos);
        IntVector2 intVector2_2 = this.SceneOwner.CalcCoord(((Component) this.mController).transform.position);
        if (!(intVector2_2 != intVector2_1) || this.mController.IsPlayingFieldAction)
          return;
        Vector3[] path = this.SceneOwner.FindPath(this.SceneOwner.Battle.CurrentUnit, intVector2_2.x, intVector2_2.y, intVector2_1.x, intVector2_1.y, unit1.DisableMoveGridHeight, this.mWalkableGrids);
        if (path == null)
          return;
        path[0] = this.mController.CenterPosition;
        double num1 = (double) this.mController.StartMove(path);
        if (!this.mRouteSet)
          this.HideShatei();
        this.mRouteSet = true;
        this.mTargetSet = false;
        this.mGridSnapping = false;
        this.mDestX = intVector2_1.x;
        this.mDestY = intVector2_1.y;
        this.SceneOwner.SendInputGridXY(this.SceneOwner.Battle.CurrentUnit, intVector2_1.x, intVector2_1.y, this.SceneOwner.Battle.CurrentUnit.Direction);
      }

      private void UpdateBlockMarker()
      {
        IntVector2 intVector2 = this.SceneOwner.CalcCoord(this.mController.CenterPosition);
        Grid current = this.SceneOwner.mBattle.CurrentMap[intVector2.x, intVector2.y];
        this.SceneOwner.mDisplayBlockedGridMarker = current == null || !this.SceneOwner.mBattle.CheckMove(this.mController.Unit, current);
      }

      private bool IsGridBlocked(Vector2 co) => this.IsGridBlocked(co.x, co.y);

      private bool IsGridBlocked(float x, float y)
      {
        int x1 = Mathf.FloorToInt(x);
        int y1 = Mathf.FloorToInt(y);
        return !this.mWalkableGrids.isValid(x1, y1) || this.mWalkableGrids.get(x1, y1) < 0;
      }

      private bool CanMoveToAdj(Vector2 from, Vector2 to)
      {
        if (this.IsGridBlocked(to))
          return false;
        return !this.IsGridBlocked(from.x, to.y) || !this.IsGridBlocked(to.x, from.y);
      }

      private bool CanMoveToAdjDirect(Vector2 from, Vector2 to)
      {
        return !this.IsGridBlocked(from.x, to.y) && !this.IsGridBlocked(to.x, from.y);
      }

      private bool GridEqualIn2D(Vector2 a, Vector2 b)
      {
        return (int) a.x == (int) b.x && (int) a.y == (int) b.y;
      }

      private void AdjustTargetPos(
        ref Vector2 basePos,
        ref Vector2 targetPos,
        Vector2 inputDir,
        Vector2 unitPos)
      {
        if (this.CanMoveToAdj(basePos, targetPos))
          return;
        bool flag = false;
        Vector2 vector2 = Vector2.op_Subtraction(targetPos, basePos);
        if ((double) Vector3.Dot(Vector2.op_Implicit(((Vector2) ref vector2).normalized), Vector2.op_Implicit(Vector2.op_Subtraction(unitPos, basePos))) >= -0.10000000149011612)
        {
          Vector2[] vector2Array = new Vector2[8]
          {
            new Vector2(-1f, 1f),
            new Vector2(0.0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0.0f),
            new Vector2(1f, -1f),
            new Vector2(0.0f, -1f),
            new Vector2(-1f, -1f),
            new Vector2(-1f, 0.0f)
          };
          float[] numArray = new float[vector2Array.Length];
          ((Vector2) ref inputDir).Normalize();
          for (int index = 0; index < vector2Array.Length; ++index)
            numArray[index] = Vector2.Dot(((Vector2) ref vector2Array[index]).normalized, inputDir);
          for (int index1 = 0; index1 < vector2Array.Length; ++index1)
          {
            for (int index2 = index1 + 1; index2 < vector2Array.Length; ++index2)
            {
              if ((double) numArray[index1] < (double) numArray[index2])
              {
                GameUtility.swap<float>(ref numArray[index1], ref numArray[index2]);
                GameUtility.swap<Vector2>(ref vector2Array[index1], ref vector2Array[index2]);
              }
            }
          }
          Vector2 zero = Vector2.zero;
          for (int index = 1; index < vector2Array.Length && (double) numArray[index] >= 0.5; ++index)
          {
            zero.x = basePos.x + vector2Array[index].x;
            zero.y = basePos.y + vector2Array[index].y;
            if (this.CanMoveToAdj(this.mBasePos, zero))
            {
              targetPos = zero;
              flag = true;
              break;
            }
          }
        }
        if (flag && this.CanMoveToAdj(basePos, targetPos))
          return;
        targetPos = basePos;
      }

      private Vector2 Velocity
      {
        get
        {
          Camera main = Camera.main;
          VirtualStick2 virtualStick = this.SceneOwner.mBattleUI.VirtualStick;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) main, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) virtualStick, (UnityEngine.Object) null) || !this.SceneOwner.IsControlBattleUI(SceneBattle.eMaskBattleUI.VS_SWIPE))
            return Vector2.zero;
          Transform transform = ((Component) main).transform;
          Vector3 vector3 = !this.SceneOwner.isUpView ? transform.forward : transform.up;
          Vector3 right = transform.right;
          vector3.y = 0.0f;
          ((Vector3) ref vector3).Normalize();
          right.y = 0.0f;
          ((Vector3) ref right).Normalize();
          Vector2 velocity = virtualStick.Velocity;
          return new Vector2((float) ((double) right.x * (double) velocity.x + (double) vector3.x * (double) velocity.y), (float) ((double) right.z * (double) velocity.x + (double) vector3.z * (double) velocity.y));
        }
      }

      private void SyncCameraPosition()
      {
        this.SceneOwner.SetCameraTarget((Component) this.mController, true);
      }

      public override void Update()
      {
        if (this.mGridSnapping)
        {
          this.SyncCameraPosition();
          if (!this.mController.isIdle)
            return;
          this.UpdateBlockMarker();
          this.mGridSnapping = false;
        }
        if (this.mClickedOK)
        {
          this.SceneOwner.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
        }
        else
        {
          if (this.SceneOwner.IsCameraMoving)
            return;
          if (this.mController.IsPlayingFieldAction)
          {
            this.SyncCameraPosition();
          }
          else
          {
            if (this.mJumping)
            {
              this.mJumping = false;
              this.SceneOwner.SendInputMove(this.SceneOwner.Battle.CurrentUnit, this.mController);
            }
            Transform transform = ((Component) this.mController).transform;
            Vector3 pos = this.mController.CenterPosition;
            bool flag1 = false;
            Vector2 vector2;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2).\u002Ector(pos.x, pos.z);
            if (this.mTargetSet && this.GridEqualIn2D(vector2, this.mTargetPos))
            {
              this.mBasePos = this.mTargetPos;
              this.mDestX = Mathf.FloorToInt(this.mBasePos.x);
              this.mDestY = Mathf.FloorToInt(this.mBasePos.y);
              this.SceneOwner.SendInputGridXY(this.SceneOwner.Battle.CurrentUnit, this.mDestX, this.mDestY, this.SceneOwner.Battle.CurrentUnit.Direction, false);
            }
            Vector2 velocity1 = this.Velocity;
            if (this.mRouteSet)
            {
              this.SyncCameraPosition();
              if (this.mController.isMoving)
              {
                Vector2 velocity2 = this.Velocity;
                if ((double) ((Vector2) ref velocity2).magnitude < 0.10000000149011612 || this.mController.IsPlayingFieldAction)
                  goto label_17;
              }
              this.mRouteSet = false;
              this.mTargetSet = false;
              this.mGridSnapping = false;
              this.mController.StopRunning();
              IntVector2 intVector2 = this.SceneOwner.CalcCoord(this.mController.CenterPosition);
              this.mDestX = intVector2.x;
              this.mDestY = intVector2.y;
              this.mBasePos.x = (float) this.mDestX + 0.5f;
              this.mBasePos.y = (float) this.mDestY + 0.5f;
label_17:
              this.UpdateBlockMarker();
            }
            else
            {
              if ((double) ((Vector2) ref velocity1).sqrMagnitude > 0.0)
              {
                if (!this.mMoveStarted)
                  this.mMoveStarted = true;
                float num1 = Mathf.Floor((float) (((double) (Mathf.Atan2(velocity1.y, velocity1.x) * 57.29578f) + 22.5) / 45.0)) * 45f;
                float num2 = Mathf.Cos(num1 * ((float) Math.PI / 180f));
                float num3 = Mathf.Sin(num1 * ((float) Math.PI / 180f));
                float num4 = (double) Mathf.Abs(num2) < 9.9999997473787516E-05 ? 0.0f : Mathf.Sign(num2);
                float num5 = (double) Mathf.Abs(num3) < 9.9999997473787516E-05 ? 0.0f : Mathf.Sign(num3);
                this.mTargetPos.x = this.mBasePos.x + num4;
                this.mTargetPos.y = this.mBasePos.y + num5;
                this.AdjustTargetPos(ref this.mBasePos, ref this.mTargetPos, velocity1, vector2);
                Vector3 vector3;
                // ISSUE: explicit constructor call
                ((Vector3) ref vector3).\u002Ector(velocity1.x, 0.0f, velocity1.y);
                this.mDesiredRotation = Quaternion.LookRotation(vector3);
                this.mHasDesiredRotation = true;
                this.mTargetSet = true;
                this.mGridSnapTime = GameSettings.Instance.Quest.GridSnapDelay;
                this.mHasInput = true;
              }
              else
              {
                this.mFullAccel = false;
                this.mTargetSet = false;
                this.mHasInput = false;
                this.mController.StopRunning();
                if ((double) this.mGridSnapTime >= 0.0 && !this.mHasDesiredRotation)
                {
                  this.mGridSnapTime -= Time.deltaTime;
                  if ((double) this.mGridSnapTime <= 0.0)
                  {
                    this.mController.StepTo(this.SceneOwner.CalcGridCenter(this.SceneOwner.mBattle.CurrentMap[this.mDestX, this.mDestY]));
                    this.mGridSnapping = true;
                    if (this.SceneOwner.Battle.CurrentUnit != null)
                      this.SceneOwner.SendInputGridXY(this.SceneOwner.Battle.CurrentUnit, this.mDestX, this.mDestY, this.SceneOwner.Battle.CurrentUnit.Direction);
                  }
                }
              }
              Vector3 vector3_1 = Vector3.zero;
              if (this.mTargetSet)
              {
                if (this.CanMoveToAdjDirect(this.mBasePos, this.mTargetPos))
                  vector3_1 = Vector2.op_Implicit(Vector2.op_Subtraction(this.mTargetPos, vector2));
                else if (this.IsGridBlocked(vector2.x, this.mTargetPos.y))
                  vector3_1.x = this.mTargetPos.x - vector2.x;
                else if (this.IsGridBlocked(this.mTargetPos.x, vector2.y))
                  vector3_1.y = this.mTargetPos.y - vector2.y;
                else
                  vector3_1 = Vector2.op_Implicit(Vector2.op_Subtraction(this.mTargetPos, vector2));
                if ((double) ((Vector3) ref vector3_1).magnitude < 0.10000000149011612)
                  vector3_1 = Vector2.op_Implicit(Vector2.zero);
              }
              bool flag2 = (double) ((Vector3) ref vector3_1).sqrMagnitude > 0.0;
              if (this.mHasDesiredRotation || flag2)
              {
                Quaternion quaternion = !flag2 ? this.mDesiredRotation : Quaternion.LookRotation(new Vector3(vector3_1.x, 0.0f, vector3_1.y));
                GameSettings instance = GameSettings.Instance;
                float magnitude = ((Vector2) ref velocity1).magnitude;
                float num6 = 0.0f;
                if (this.mFullAccel)
                {
                  num6 = magnitude;
                  ((Component) this.mController).transform.rotation = Quaternion.Slerp(((Component) this.mController).transform.rotation, quaternion, Time.deltaTime * 5f);
                }
                else
                {
                  ((Component) this.mController).transform.rotation = Quaternion.Slerp(((Component) this.mController).transform.rotation, quaternion, Time.deltaTime * 10f);
                  float num7 = Quaternion.Angle(quaternion, ((Component) this.mController).transform.rotation);
                  if ((double) magnitude > 0.10000000149011612)
                  {
                    if ((double) num7 < 1.0)
                    {
                      this.mFullAccel = true;
                      num6 = magnitude;
                    }
                    else
                    {
                      float num8 = 15f;
                      float num9 = Mathf.Clamp01((float) (1.0 - (double) num7 / (double) num8));
                      num6 = magnitude * num9;
                    }
                  }
                  if ((double) num7 < 1.0)
                  {
                    ((Component) this.mController).transform.rotation = quaternion;
                    this.mHasDesiredRotation = false;
                  }
                }
                if ((double) num6 > 0.0 && flag2)
                {
                  this.mController.StartRunning();
                  float num10 = Mathf.Lerp(instance.Quest.MapRunSpeedMin, instance.Quest.MapRunSpeedMax, num6);
                  Vector3 vector3_2;
                  // ISSUE: explicit constructor call
                  ((Vector3) ref vector3_2).\u002Ector(vector3_1.x, 0.0f, vector3_1.y);
                  Vector3 velocity3 = Vector3.op_Multiply(((Vector3) ref vector3_2).normalized, num10);
                  if (this.mController.TriggerFieldAction(velocity3))
                  {
                    Vector2 fieldActionPoint = this.mController.FieldActionPoint;
                    this.mDestX = Mathf.FloorToInt(fieldActionPoint.x);
                    this.mDestY = Mathf.FloorToInt(fieldActionPoint.y);
                    this.mHasDesiredRotation = false;
                    this.mTargetSet = false;
                    this.mJumping = true;
                    this.mRouteSet = true;
                  }
                  else
                  {
                    pos = Vector3.op_Addition(pos, Vector3.op_Multiply(velocity3, Time.deltaTime));
                    flag1 = true;
                    for (int index1 = -1; index1 <= 1; ++index1)
                    {
                      for (int index2 = -1; index2 <= 1; ++index2)
                      {
                        if ((index2 != 0 || index1 != 0) && this.IsGridBlocked((float) Mathf.FloorToInt(pos.x + (float) index2 * 0.3f), (float) Mathf.FloorToInt(pos.z + (float) index1 * 0.3f)))
                        {
                          if (index2 < 0)
                            this.mController.AdjustMovePos(EUnitDirection.NegativeX, ref pos);
                          if (index2 > 0)
                            this.mController.AdjustMovePos(EUnitDirection.PositiveX, ref pos);
                          if (index1 < 0)
                            this.mController.AdjustMovePos(EUnitDirection.NegativeY, ref pos);
                          if (index1 > 0)
                            this.mController.AdjustMovePos(EUnitDirection.PositiveY, ref pos);
                        }
                      }
                    }
                  }
                }
                this.SceneOwner.SendInputMove(this.SceneOwner.Battle.CurrentUnit, this.mController);
              }
              bool flag3 = false;
              IntVector2 intVector2 = this.SceneOwner.CalcCoord(pos);
              BattleMap currentMap = this.SceneOwner.Battle.CurrentMap;
              intVector2.x = Math.Min(Math.Max(0, intVector2.x), currentMap.Width - 1);
              intVector2.y = Math.Min(Math.Max(0, intVector2.y), currentMap.Height - 1);
              if (intVector2.x != this.mCurrentX || intVector2.y != this.mCurrentY)
              {
                this.mCurrentX = intVector2.x;
                this.mCurrentY = intVector2.y;
                this.RecalcAttackTargets();
                flag3 = true;
              }
              if (flag1)
              {
                transform.position = pos;
                this.UpdateBlockMarker();
                this.mStopTime = 0.0f;
                this.HideShatei();
              }
              else
              {
                this.mStopTime += Time.deltaTime;
                if ((double) this.mStopTime > 0.30000001192092896 && (this.mUpdateShateiGrid || !this.mShateiVisible) && (double) this.mGridSnapTime <= 0.0 && this.mController.isIdle)
                {
                  this.mUpdateShateiGrid = false;
                  if (this.mShateiGrid != null)
                    this.SceneOwner.mTacticsSceneRoot.ShowGridLayer(1, this.mShateiGrid, Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea), true);
                  this.mShateiVisible = true;
                  this.SceneOwner.ShowCastSkill();
                  if (!flag3)
                  {
                    this.mController.Unit.RefreshMomentBuff(this.SceneOwner.Battle.Units, true, intVector2.x, intVector2.y);
                    this.SceneOwner.Battle.CacheClearForcedTargeting();
                  }
                }
              }
              this.SyncCameraPosition();
            }
          }
        }
      }

      private void HideShatei()
      {
        this.SceneOwner.mTacticsSceneRoot.HideGridLayer(1);
        this.mShateiVisible = false;
        this.SceneOwner.HideCastSkill();
      }

      private void ResetHPGauge(TacticsUnitController tuc)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) tuc, (UnityEngine.Object) null))
          return;
        tuc.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal);
        tuc.SetHPChangeYosou(tuc.VisibleHPValue);
      }

      private void RecalcAttackTargets()
      {
        Unit unit1 = this.mController.Unit;
        if (!unit1.IsEnableAttackCondition())
          return;
        this.mController.Unit.RefreshMomentBuff(this.SceneOwner.Battle.Units, true, this.mCurrentX, this.mCurrentY);
        SkillData attackSkill = unit1.GetAttackSkill();
        this.mUpdateShateiGrid = true;
        this.mShateiGrid = this.SceneOwner.Battle.CreateSelectGridMap(unit1, this.mCurrentX, this.mCurrentY, attackSkill);
        this.SceneOwner.mNumHotTargets = 0;
        this.SceneOwner.mHotTargets.Clear();
        if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.SceneOwner))
          return;
        BattleCore mBattle = this.SceneOwner.mBattle;
        if (mBattle == null || mBattle.Units == null)
          return;
        List<Unit> targets = new List<Unit>(mBattle.Units.Count);
        foreach (Unit unit2 in mBattle.Units)
        {
          if (this.mShateiGrid.isValid(unit2.x, unit2.y) && mBattle.IsTargetUnitForGridMap(unit2, this.mShateiGrid) && mBattle.CheckSkillTarget(unit1, unit2, attackSkill))
            targets.Add(unit2);
          this.ResetHPGauge(this.SceneOwner.FindUnitController(unit2));
        }
        mBattle.CacheClearForcedTargeting();
        mBattle.ReflectForcedTargeting(unit1, this.mCurrentX, this.mCurrentY, attackSkill, ref targets);
        foreach (Unit unit3 in targets)
        {
          TacticsUnitController unitController1 = this.SceneOwner.FindUnitController(unit3);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController1, (UnityEngine.Object) null))
          {
            BattleCore.CommandResult commandResult = mBattle.GetCommandResult(unit1, this.mCurrentX, this.mCurrentY, unit3.x, unit3.y, attackSkill);
            if (commandResult != null && commandResult.targets != null && commandResult.targets.Count > 0)
            {
              bool flag = false;
              for (int index = 0; index < commandResult.targets.Count; ++index)
              {
                Unit unit4 = commandResult.targets[index].unit;
                TacticsUnitController unitController2 = this.SceneOwner.FindUnitController(unit4);
                if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController2, (UnityEngine.Object) null))
                {
                  long newHP = unit4.GetCurrentHP() - (long) commandResult.targets[index].hp_damage;
                  if (commandResult.skill != null && commandResult.skill.IsMhmDamage())
                    newHP = unit4.GetCurrentHP();
                  unitController2.SetHPChangeYosou(newHP);
                  unitController2.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Attack, attackSkill, unit1);
                  ++this.SceneOwner.mNumHotTargets;
                  this.SceneOwner.mHotTargets.Add(unitController1);
                  if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController1, (UnityEngine.Object) unitController2))
                    flag = true;
                }
              }
              if (!flag)
                this.ResetHPGauge(unitController1);
            }
          }
        }
      }
    }
  }
}
