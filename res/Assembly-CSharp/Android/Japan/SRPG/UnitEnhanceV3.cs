﻿// Decompiled with JetBrains decompiler
// Type: SRPG.UnitEnhanceV3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "初期化完了", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(100, "閉じる", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(110, "ViewerMode ON", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(111, "ViewerMode OFF", FlowNode.PinTypes.Input, 111)]
  [FlowNode.Pin(200, "UnitVoiceOff(CleanUp)", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(201, "UnitVoiceOff", FlowNode.PinTypes.Input, 201)]
  [FlowNode.Pin(202, "StopPlayLeftVoice On", FlowNode.PinTypes.Input, 202)]
  [FlowNode.Pin(203, "StopPlayLeftVoice OFF", FlowNode.PinTypes.Input, 203)]
  [FlowNode.Pin(210, "UnitVoiceOff Output", FlowNode.PinTypes.Output, 210)]
  [FlowNode.Pin(301, "真理開眼開放", FlowNode.PinTypes.Input, 301)]
  [FlowNode.Pin(302, "真理開眼遷移チェック", FlowNode.PinTypes.Input, 302)]
  [FlowNode.Pin(401, "ユニット詳細表示更新", FlowNode.PinTypes.Input, 401)]
  [FlowNode.Pin(500, "TIPS表示", FlowNode.PinTypes.Output, 500)]
  public class UnitEnhanceV3 : MonoBehaviour, IFlowInterface
  {
    public static UnitEnhanceV3 Instance = (UnitEnhanceV3) null;
    private static readonly string CONCEPT_CARD_EQUIP_WINDOW_PREFAB_PATH = "UI/ConceptCardSelect";
    public static readonly string UNIT_EXPMAX_UI_PATH = "UI/UnitLevelupWindow";
    private static readonly int PLAY_LEFTVOICE_SPAN = 60;
    private static string[] UnitHourVoices = new string[24]{ "chara_0007", "chara_0007", "chara_0007", "chara_0007", "chara_0002", "chara_0002", "chara_0002", "chara_0002", "chara_0003", "chara_0003", "chara_0003", "chara_0003", "chara_0004", "chara_0004", "chara_0004", "chara_0004", "chara_0005", "chara_0005", "chara_0005", "chara_0005", "chara_0006", "chara_0006", "chara_0006", "chara_0006" };
    private List<ItemData> mTmpItems = new List<ItemData>();
    [Space(10f)]
    public bool FastStart = true;
    public Color32 DefaultParamColor = (Color32) Color.white;
    public Color32 ParamUpColor = (Color32) Color.green;
    public Color32 ParamDownColor = (Color32) Color.red;
    public string AbilityRankUpTrigger = "on";
    private string PREFAB_PATH_TOBIRA_OPEN = "UI/TobiraOpenEffect";
    private string PREFAB_PATH_TOBIRA_LEVELUP = "UI/TobiraEnhanceEffect";
    private string PREFAB_PATH_TOBIRA_LEVEL_MAX = "UI/TobiraEnhanceEffectMax";
    [SerializeField]
    private string PREFAB_PATH_CHARA_QUEST_LIST_WINDOW = "UI/UnitCharacterQuestWindow";
    [SerializeField]
    private string PREFAB_PATH_UNITDATA_UNLOCK_POPUP = "UI/QuestClearUnlockPopup";
    [SerializeField]
    private string PREFAB_PATH_EVOLUTION_WINDOW = "UI/UnitEvolutionWindow3";
    [SerializeField]
    private string PREFAB_PATH_UNLOCK_TOBIRA_WINDOW = "UI/TobiraOpenCheckWindow";
    [SerializeField]
    private string PREFAB_PATH_ARTIFACT_WINDOW = "UI/ArtiSelect";
    [SerializeField]
    private string PREFAB_PATH_MODEL_VIEWER = "UI/UnitModelViewer";
    [Space(10f)]
    public bool ShowNoStockExpPotions = true;
    [Space(10f)]
    public Vector3 PreviewWindowDir = new Vector3(0.0f, 0.5f, -1f);
    public Color32 UnitLevelColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
    public Color32 CappedUnitLevelColor = new Color32(byte.MaxValue, (byte) 0, (byte) 0, byte.MaxValue);
    public string BGAnimatorPlayTrigger = "play";
    [SerializeField]
    private string BGTobiraImageID = "OPEN_BG_CANVAS";
    [SerializeField]
    private string BGTobiraEffectImageID = "OPEN_BG_EFFECT";
    private Dictionary<int, int[]> mJobSetDatas = new Dictionary<int, int[]>();
    private List<UnitPreview> mPreviewControllers = new List<UnitPreview>();
    private List<string> mPreviewJobNames = new List<string>();
    private long[] mOriginalAbilities = new long[5];
    private List<long> mRankedUpAbilities = new List<long>();
    private Dictionary<string, int> mUsedExpItems = new Dictionary<string, int>();
    private List<long> mDirtyUnits = new List<long>();
    private long mStartSelectUnitUniqueID = -1;
    private List<GameObject> mExpItemGameObjects = new List<GameObject>();
    private List<long> m_UnitList = new List<long>();
    public string SceneNameHome = "Home";
    private int mCacheAwakeLv = 1;
    public const int UNLOCK_TOBIRA = 301;
    public const int CHECK_TOBIRA = 302;
    public const int REFRESH_UNIT_DETAIL = 401;
    public const int SHOW_TOBIRA_TIPS = 500;
    private const float ExpAnimSpan = 1f;
    public GameObject EquipSlotEffect;
    public GameObject JobChangeButtonEffect;
    public GameObject ParamUpEffect;
    public GameObject ParamDownEffect;
    public GameObject AbilityRankUpEffect;
    public GameObject JobRankUpEffect;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_JobRankUp;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_JobUnlock;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_JobCC;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_NewAbilityList;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_NewSkillList;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_NewSkillList2;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_ReturnItems;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_LeveUp;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_Kakusei;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_Evolution;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_JobChange;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_JobMaster;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_Unlock_Tobira;
    private GameObject mTobiraLevelupMaxEffectObject;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_UnitPieceWindow;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_ProfileWindow;
    public Tooltip Prefab_LockedJobTooltip;
    public Tooltip Prefab_ParamTooltip;
    public SRPG_Button CharaQuestButton;
    public SRPG_Button SkinButton;
    [StringIsResourcePath(typeof (GameObject))]
    public string SkinSelectTemplate;
    [Space(10f)]
    public RectTransform ActiveJobIndicator;
    public SRPG_ToggleButton JobChangeButton;
    public UnityEngine.UI.Text JobRank;
    [Space(10f)]
    public SRPG_ToggleButton Tab_Equipments;
    public SRPG_ToggleButton Tab_Kyoka;
    public SRPG_ToggleButton Tab_AbilityList;
    public SRPG_ToggleButton Tab_AbilitySlot;
    public SRPG_Button UnitKakuseiButton;
    public SRPG_Button UnitEvolutionButton;
    public Button UnitUnlockTobiraButton;
    public Button UnitGoTobiraButton;
    public string UnitKakuseiButtonHilitBool;
    public string UnitEvolutionButtonHilitBool;
    public string UnitTobiraButtonHilitBool;
    public string JobRankUpButtonHilitBool;
    public string AllEquipButtonHilitBool;
    [Space(10f)]
    public RectTransform TabPageParent;
    public UnitEnhancePanel Prefab_Equipments;
    public UnitEnhancePanel Prefab_Kyoka;
    public UnitEnhancePanel Prefab_AbilityList;
    public UnitEnhancePanel Prefab_AbilitySlots;
    public UnitAbilityPicker Prefab_AbilityPicker;
    public GameObject Prefab_IkkatsuEquip;
    [Space(10f)]
    public SRPG_Button NextUnitButton;
    public SRPG_Button PrevUnitButton;
    [Space(10f)]
    public UnityEngine.UI.Text Status_HP;
    public UnityEngine.UI.Text Status_MP;
    public UnityEngine.UI.Text Status_MPIni;
    public UnityEngine.UI.Text Status_Atk;
    public UnityEngine.UI.Text Status_Def;
    public UnityEngine.UI.Text Status_Mag;
    public UnityEngine.UI.Text Status_Mnd;
    public UnityEngine.UI.Text Status_Rec;
    public UnityEngine.UI.Text Status_Dex;
    public UnityEngine.UI.Text Status_Spd;
    public UnityEngine.UI.Text Status_Cri;
    public UnityEngine.UI.Text Status_Luk;
    public UnityEngine.UI.Text Status_Mov;
    public UnityEngine.UI.Text Status_Jmp;
    public UnityEngine.UI.Text Param_Renkei;
    [Space(10f)]
    [SerializeField]
    private Animator UnitinfoViewAnimator;
    public GameObject UnitInfo;
    public GameObject UnitParamInfo;
    public GameObject JobInfo;
    public GenericSlot LeaderSkillInfo;
    public SRPG_Button LeaderSkillDetailButton;
    public GameObject Prefab_LeaderSkillDetail;
    public GameObject UnitExpInfo;
    public GameObject UnitRarityInfo;
    public SliderAnimator UnitEXPSlider;
    public UnityEngine.UI.Text UnitLevel;
    public GameObject UnitLevelCapInfo;
    public UnityEngine.UI.Text UnitExp;
    public UnityEngine.UI.Text UnitExpMax;
    public UnityEngine.UI.Text UnitExpNext;
    [Space(10f)]
    public string BGAnimatorID;
    public string BGUnitImageID;
    public float BGUnitImageFadeTime;
    public string ExpOverflowWarning;
    public Tooltip Prefab_LockedArtifactSlotTooltip;
    [SerializeField]
    private Transform mUnitModelViewerParent;
    private UnitModelViewer mUnitModelViewer;
    public CanvasGroup LeftGroup;
    public CanvasGroup RightGroup;
    public Toggle Favorite;
    private bool mStarted;
    [SerializeField]
    private JobIconScrollListController mJobIconScrollListController;
    private List<UnitInventoryJobIcon> mUnitJobIconSetList;
    private ScrollClamped_JobIcons mScrollClampedJobIcons;
    private Canvas mOverlayCanvas;
    private Animator mBGAnimator;
    private RawImage mBGUnitImage;
    private Animator mBGTobiraAnimator;
    private Animator mBGTobiraEffectAnimator;
    private long mCurrentUnitID;
    private long mCurrentJobUniqueID;
    private long mIsSetJobUniqueID;
    private UnitData mCurrentUnit;
    private float mLastSyncTime;
    private UnitEnhancePanel mEquipmentPanel;
    private UnitEnhancePanel mKyokaPanel;
    private UnitEnhancePanel mAbilityListPanel;
    private UnitEnhancePanel mAbilitySlotPanel;
    private UnitAbilityPicker mAbilityPicker;
    private UnitEvolutionWindow mEvolutionWindow;
    private UnitUnlockTobiraWindow mUnlockTobiraWindow;
    private GameObject mSkinSelectWindow;
    private JobParam mUnlockJobParam;
    private bool mEquipmentPanelDirty;
    private bool mAbilityListDirty;
    private bool mAbilitySlotDirty;
    private Tooltip mJobUnlockTooltip;
    private int mJobUnlockTooltipIndex;
    private Tooltip mParamTooltip;
    private GameObject mParamTooltipTarget;
    private Tooltip mEquipArtifactUnlockTooltip;
    private int mSelectedEquipmentSlot;
    private UnitPreview mCurrentPreview;
    private int mSelectedJobIndex;
    private Dictionary<UnitEnhanceV3.eTabButtons, SRPG_ToggleButton> mTabButtons;
    private UnitEnhancePanel[] mTabPages;
    private UnitEnhanceV3.eTabButtons mActiveTabIndex;
    private bool mReloading;
    private UnityEngine.UI.Text[] mStatusParamSlots;
    private bool mCloseRequested;
    private float mBGUnitImgAlphaStart;
    private float mBGUnitImgAlphaEnd;
    private float mBGUnitImgFadeTime;
    private float mBGUnitImgFadeTimeMax;
    private UnitEquipmentWindow mEquipmentWindow;
    private UnitKakeraWindow mKakeraWindow;
    private UnitCharacterQuestWindow mCharacterQuestWindow;
    private UnitTobiraInventory mTobiraInventoryWindow;
    private float mExpStart;
    private float mExpEnd;
    private float mExpAnimTime;
    private float mLeftTime;
    private UnitEnhanceV3.DeferredJob mDefferedCallFunc;
    private float mDefferedCallDelay;
    public UnitEnhanceV3.CloseEvent OnUserClose;
    private UnitUnlockWindow mUnitUnlockWindow;
    private UnitProfileWindow mUnitProfileWindow;
    private GameObject mLeaderSkillDetail;
    private MySound.Voice mUnitVoice;
    private GameObject mArtifactSelector;
    private UnitJobRankUpConfirm mIkkatsuEquipWindow;
    private UnitData.CharacterQuestUnlockProgress mChQuestProg;
    private string mCurrentUnitUnlocks;
    [Space(10f)]
    public bool HoldUseItemEnable;
    public bool HoldUseItemLvUpStop;
    private ScrollRect mKyoukaItemScroll;
    private UnitEnhanceV3.ExpItemTouchController mCurrentKyoukaItemHold;
    private List<UnitData> SortedUnits;
    private bool IsBulk;
    private bool mIsCommon;
    private UnitData mCacheUnitData;
    private bool IsStopPlayLeftVoice;
    [Space(10f)]
    public SRPG_Button ButtonMapEffectJob;
    [StringIsResourcePath(typeof (GameObject))]
    public string PrefabMapEffectJob;
    private LoadRequest mReqMapEffectJob;
    public string m_VO_TobiraUnlock;
    public string mVO_TobiraOpenCueID;
    public string mVO_TobiraEnhanceCueID;
    public string mVO_TobiraMaxCueID;
    private Transform mTrHomeHeader;
    private bool mSceneChanging;
    private bool mIsJobLvUpAllEquip;
    private bool mIsJobLvMaxAllEquip;
    private BaseStatus mCurrentStatus;
    private int mCurrentRenkei;
    [NonSerialized]
    public bool MuteVoice;
    private GameObject ClickItem;
    private bool mSendingKyokaRequest;
    private bool mKeepWindowLocked;
    private bool mJobChangeRequestSent;
    private string mNextJobID;
    private string mPrevJobID;
    private bool mJobRankUpRequestSent;
    private bool mDesiredPreviewVisibility;
    private bool mUpdatePreviewVisibility;
    private bool mAppPausing;
    private UnitAbilityListItemEvents.ListItemTouchController mCurrentAbilityRankUpHold;
    private bool IsFirstPlay;
    private bool mAbilityRankUpRequestSent;
    private LoadRequest mProfileWindowLoadRequest;
    private bool mKakuseiRequestSent;
    private bool mEvolutionRequestSent;
    private bool mUnlockTobiraRequestSent;

    private void OnEnable()
    {
      if (!((UnityEngine.Object) UnitEnhanceV3.Instance == (UnityEngine.Object) null))
        return;
      UnitEnhanceV3.Instance = this;
    }

    private void OnDisable()
    {
      if (!((UnityEngine.Object) UnitEnhanceV3.Instance == (UnityEngine.Object) this))
        return;
      UnitEnhanceV3.Instance = (UnitEnhanceV3) null;
    }

    public UnitData CurrentUnit
    {
      get
      {
        return this.mCurrentUnit;
      }
    }

    public List<ItemData> TmpExpItems
    {
      get
      {
        return this.mTmpItems;
      }
    }

    private List<UnitInventoryJobIcon> UnitJobIconSetList
    {
      get
      {
        if (this.mUnitJobIconSetList == null)
        {
          this.mUnitJobIconSetList = new List<UnitInventoryJobIcon>();
          for (int index = 0; index < this.mJobIconScrollListController.Items.Count; ++index)
            this.mUnitJobIconSetList.Add(this.mJobIconScrollListController.Items[index].job_icon);
        }
        return this.mUnitJobIconSetList;
      }
    }

    private ScrollClamped_JobIcons ScrollClampedJobIcons
    {
      get
      {
        if ((UnityEngine.Object) this.mScrollClampedJobIcons == (UnityEngine.Object) null)
        {
          this.mScrollClampedJobIcons = this.mJobIconScrollListController.GetComponent<ScrollClamped_JobIcons>();
          this.mScrollClampedJobIcons.OnFrameOutItem = new ScrollClamped_JobIcons.FrameOutItem(this.RefreshJobIcon);
        }
        return this.mScrollClampedJobIcons;
      }
    }

    public long IsSetJobUniqueID
    {
      get
      {
        return this.mIsSetJobUniqueID;
      }
    }

    public List<long> UnitList
    {
      set
      {
        this.m_UnitList = value;
      }
      get
      {
        return this.m_UnitList;
      }
    }

    private Transform TrHomeHeader
    {
      get
      {
        if ((UnityEngine.Object) this.mTrHomeHeader == (UnityEngine.Object) null)
        {
          UnityEngine.SceneManagement.Scene sceneByName = SceneManager.GetSceneByName(this.SceneNameHome);
          if (sceneByName.IsValid())
          {
            GameObject[] rootGameObjects = sceneByName.GetRootGameObjects();
            if (rootGameObjects != null)
            {
              foreach (GameObject gameObject in rootGameObjects)
              {
                HomeWindow component = gameObject.GetComponent<HomeWindow>();
                if ((bool) ((UnityEngine.Object) component))
                {
                  this.mTrHomeHeader = component.transform;
                  break;
                }
              }
            }
          }
        }
        return this.mTrHomeHeader;
      }
    }

    private void OpenMapEffectJob()
    {
      if (this.mCurrentUnit == null || this.mReqMapEffectJob == null || (!this.mReqMapEffectJob.isDone || this.mReqMapEffectJob.asset == (UnityEngine.Object) null))
        return;
      Transform parent = this.TrHomeHeader;
      if (!(bool) ((UnityEngine.Object) parent))
        parent = this.transform;
      GameObject instance = MapEffectQuest.CreateInstance(this.mReqMapEffectJob.asset as GameObject, parent);
      if (!(bool) ((UnityEngine.Object) instance))
        return;
      DataSource.Bind<JobParam>(instance, this.mCurrentUnit.CurrentJob.Param, false);
      instance.SetActive(true);
      MapEffectJob component = instance.GetComponent<MapEffectJob>();
      if (!(bool) ((UnityEngine.Object) component))
        return;
      component.Setup();
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CStart\u003Ec__Iterator0() { \u0024this = this };
    }

    private void OnDestroy()
    {
      this.DestroyOverlay();
      if ((UnityEngine.Object) this.mArtifactSelector != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mArtifactSelector.gameObject);
        this.mArtifactSelector = (GameObject) null;
      }
      if ((UnityEngine.Object) this.mParamTooltip != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mParamTooltip.gameObject);
        this.mParamTooltip = (Tooltip) null;
      }
      if ((UnityEngine.Object) this.mUnitProfileWindow != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mUnitProfileWindow.gameObject);
        this.mUnitProfileWindow = (UnitProfileWindow) null;
      }
      if ((UnityEngine.Object) this.mUnitUnlockWindow != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mUnitUnlockWindow.gameObject);
        this.mUnitUnlockWindow = (UnitUnlockWindow) null;
      }
      if ((UnityEngine.Object) this.mJobUnlockTooltip != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mJobUnlockTooltip.gameObject);
        this.mJobUnlockTooltip = (Tooltip) null;
      }
      if ((UnityEngine.Object) this.mLeaderSkillDetail != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mLeaderSkillDetail.gameObject);
      if ((UnityEngine.Object) this.mEvolutionWindow != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mEvolutionWindow.gameObject);
      if ((UnityEngine.Object) this.mUnlockTobiraWindow != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mUnlockTobiraWindow.gameObject);
      if ((UnityEngine.Object) this.mSkinSelectWindow != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mSkinSelectWindow.gameObject);
      if ((UnityEngine.Object) this.mAbilityPicker != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mAbilityPicker.gameObject);
      if ((UnityEngine.Object) this.mCharacterQuestWindow != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mCharacterQuestWindow.gameObject);
        this.mCharacterQuestWindow = (UnitCharacterQuestWindow) null;
      }
      if ((UnityEngine.Object) this.mIkkatsuEquipWindow != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mIkkatsuEquipWindow.gameObject);
        this.mIkkatsuEquipWindow = (UnitJobRankUpConfirm) null;
      }
      if ((UnityEngine.Object) this.mEquipArtifactUnlockTooltip != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mEquipArtifactUnlockTooltip.gameObject);
        this.mEquipArtifactUnlockTooltip = (Tooltip) null;
      }
      if ((UnityEngine.Object) this.mTobiraLevelupMaxEffectObject != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mTobiraLevelupMaxEffectObject);
        this.mTobiraLevelupMaxEffectObject = (GameObject) null;
      }
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) instanceDirect != (UnityEngine.Object) null)
      {
        instanceDirect.OnSceneChange -= new GameManager.SceneChangeEvent(this.OnSceneCHange);
        instanceDirect.OnAbilityRankUpCountPreReset -= new GameManager.RankUpCountChangeEvent(this.OnAbilityRankUpCountPreReset);
      }
      GameUtility.DestroyGameObjects<UnitPreview>(this.mPreviewControllers);
      if (this.mUnitVoice == null)
        return;
      this.mUnitVoice.StopAll(1f);
      this.mUnitVoice.Cleanup();
      this.mUnitVoice = (MySound.Voice) null;
    }

    private bool OnSceneCHange()
    {
      if (!this.mSceneChanging)
      {
        this.mSceneChanging = true;
        MonoSingleton<GameManager>.Instance.RegisterImportantJob(this.StartCoroutine(this.OnSceneChangeAsync()));
      }
      return true;
    }

    [DebuggerHidden]
    private IEnumerator OnSceneChangeAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003COnSceneChangeAsync\u003Ec__Iterator1() { \u0024this = this };
    }

    private void OnEquipNoCommon()
    {
      this.OnEquip(false);
    }

    private void OnEquipCommon()
    {
      this.OnEquip(true);
    }

    private void OnEquip(bool is_common)
    {
      JobSetParam jobSetParam = this.mCurrentUnit.GetJobSetParam(this.mSelectedJobIndex);
      if (jobSetParam == null)
        return;
      this.EquipmentWindow.GetComponent<WindowController>().OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      WindowController.CloseIfAvailable((Component) this.EquipmentWindow);
      if (Network.Mode == Network.EConnectMode.Online)
      {
        if (is_common)
          Network.RequestAPI((WebAPI) new ReqJobEquipV2(this.mCurrentUnit.UniqueID, jobSetParam.iname, (long) this.mSelectedEquipmentSlot, is_common, new Network.ResponseCallback(this.OnCommonEquipResult)), false);
        else
          Network.RequestAPI((WebAPI) new ReqJobEquipV2(this.mCurrentUnit.UniqueID, jobSetParam.iname, (long) this.mSelectedEquipmentSlot, is_common, new Network.ResponseCallback(this.OnEquipResult)), false);
      }
      else
        this.StartCoroutine(this.PostEquip(true));
      this.SetUnitDirty();
    }

    private void OnEquipResult(WWWResult www)
    {
      this.OnEquipResult(www, false);
    }

    private void OnCommonEquipResult(WWWResult www)
    {
      this.OnEquipResult(www, true);
    }

    private void OnEquipResult(WWWResult www, bool is_common)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoJobSetEquip:
            FlowNode_Network.Failed();
            break;
          case Network.EErrCode.NoEquipItem:
          case Network.EErrCode.Equipped:
            Network.RemoveAPI();
            Network.ResetError();
            this.GetComponent<WindowController>().SetCollision(true);
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        try
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          instance.Deserialize(jsonObject.body.player);
          instance.Deserialize(jsonObject.body.units);
          instance.Deserialize(jsonObject.body.items);
          this.mLastSyncTime = Time.realtimeSinceStartup;
        }
        catch (Exception ex)
        {
          Debug.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        Network.RemoveAPI();
        this.RefreshSortedUnits();
        if (is_common)
          this.EquipmentWindow.CommonEquiped();
        this.StartCoroutine(this.PostEquip(true));
      }
    }

    private void OnEquipAll(bool AllIn = false)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Prefab_IkkatsuEquip);
      if ((UnityEngine.Object) gameObject == (UnityEngine.Object) null)
        return;
      DataSource.Bind<UnitData>(gameObject, this.mCurrentUnit, false);
      UnitJobRankUpConfirm component = gameObject.GetComponent<UnitJobRankUpConfirm>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      component.IsAllIn = AllIn;
      this.IsBulk = AllIn;
      if (this.mCurrentUnit == null || this.mCurrentUnit.CurrentJob == null)
        return;
      component.OnAllEquipAccept = this.mCurrentUnit.CurrentJob.GetJobRankCap(this.mCurrentUnit) <= this.mCurrentUnit.CurrentJob.Rank ? new UnitJobRankUpConfirm.OnAccept(this.OnEquipAllAccept) : new UnitJobRankUpConfirm.OnAccept(this.OnJobRankUpEquipAllAccept);
      component.SetCommonFlag = new UnitJobRankUpConfirm.SetFlag(this.SetIsCommon);
    }

    public void SetIsCommon(bool is_common)
    {
      this.mIsCommon = is_common;
    }

    private void OnJobRankUpEquipAllAccept(int target_rank = -1, bool can_jobmaster = false, bool can_jobmax = false)
    {
      this.mIsJobLvUpAllEquip = true;
      this.StartJobRankUp(target_rank, can_jobmaster, can_jobmax);
    }

    private void OnEquipAllAccept(int target_rank = -1, bool can_jobmaster = false, bool can_jobmax = false)
    {
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.GetComponent<WindowController>().SetCollision(false);
        JobSetParam jobSetParam = this.mCurrentUnit.GetJobSetParam(this.mSelectedJobIndex);
        int isEquips = 0;
        if (can_jobmaster)
          isEquips = 1;
        else if (target_rank > 0 && this.mCurrentUnit.GetJobRankCap() != JobParam.MAX_JOB_RANK && this.mCurrentUnit.GetJobRankCap() == target_rank)
        {
          if (this.IsBulk)
            isEquips = !can_jobmax ? 0 : 1;
          else if (this.mCurrentUnit.CurrentJob.Rank == target_rank)
            isEquips = 1;
        }
        this.SetCacheUnitData(this.mCurrentUnit, this.mSelectedJobIndex);
        if (jobSetParam != null)
        {
          Network.RequestAPI((WebAPI) new ReqJobRankupAll(this.mCurrentUnit.UniqueID, jobSetParam.iname, this.mIsCommon, this.mCurrentUnit.CurrentJob.Rank, target_rank, isEquips, new Network.ResponseCallback(this.OnEquipAllResult)), false);
          this.SetIsCommon(false);
        }
        else
          this.StartCoroutine(this.PostEquip(false));
      }
      this.SetUnitDirty();
    }

    private void OnEquipAllResult(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoJobSetEquip:
            FlowNode_Network.Failed();
            break;
          case Network.EErrCode.NoEquipItem:
            FlowNode_Network.Failed();
            this.mJobRankUpRequestSent = true;
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        UnitData unitDataByUniqueId1 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
        EquipData[] rankupEquips = unitDataByUniqueId1.GetRankupEquips(this.mSelectedJobIndex);
        int num1 = 0;
        for (int index = 0; index < rankupEquips.Length; ++index)
        {
          if (rankupEquips[index].IsEquiped())
            ++num1;
        }
        JobData jobData1 = unitDataByUniqueId1.GetJobData(this.mSelectedJobIndex);
        if (jobData1 != null)
        {
          if (jobData1.Rank > 1)
            num1 += (jobData1.Rank - 1) * 6;
        }
        try
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          instance.Deserialize(jsonObject.body.player);
          instance.Deserialize(jsonObject.body.units);
          instance.Deserialize(jsonObject.body.items);
          this.mLastSyncTime = Time.realtimeSinceStartup;
        }
        catch (Exception ex)
        {
          Debug.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        Network.RemoveAPI();
        this.SetNotifyReleaseClassChangeJob();
        this.UpdateTrophy_OnJobLevelChange();
        this.RefreshSortedUnits();
        UnitData unitDataByUniqueId2 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
        int num2 = 0;
        foreach (EquipData rankupEquip in unitDataByUniqueId2.GetRankupEquips(this.mSelectedJobIndex))
        {
          if (rankupEquip.IsEquiped())
            ++num2;
        }
        JobData jobData2 = unitDataByUniqueId2.GetJobData(this.mSelectedJobIndex);
        if (jobData2 != null && jobData2.Rank > 1)
          num2 += (jobData2.Rank - 1) * 6;
        MonoSingleton<GameManager>.Instance.Player.OnSoubiSet(this.mCurrentUnit.UnitID, num2 - num1);
        GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) null);
        GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_COIN_STATUS.ToString(), (object) null);
        if (this.mIsJobLvMaxAllEquip)
          this.StartCoroutine(this.PostEquip(false));
        else
          this.mJobRankUpRequestSent = true;
      }
    }

    public void BeginStatusChangeCheck()
    {
      this.mCurrentStatus = new BaseStatus(this.mCurrentUnit.Status);
      this.mCurrentRenkei = this.mCurrentUnit.GetCombination();
    }

    public void SpawnStatusChangeEffects()
    {
      BaseStatus status = this.mCurrentUnit.Status;
      for (int index = 0; index < StatusParam.MAX_STATUS; ++index)
      {
        if (!((UnityEngine.Object) this.mStatusParamSlots[index] == (UnityEngine.Object) null))
        {
          int delta = (int) status.param[(StatusTypes) index] - (int) this.mCurrentStatus.param[(StatusTypes) index];
          if (delta != 0)
            this.SpawnParamDeltaEffect(this.mStatusParamSlots[index].gameObject, delta);
        }
      }
      if (!((UnityEngine.Object) this.Param_Renkei != (UnityEngine.Object) null))
        return;
      this.SpawnParamDeltaEffect(this.Param_Renkei.gameObject, this.mCurrentUnit.GetCombination() - this.mCurrentRenkei);
    }

    private void SpawnParamDeltaEffect(GameObject go, int delta)
    {
      if (delta == 0)
        return;
      GameObject original = (GameObject) null;
      StringBuilder stringBuilder = GameUtility.GetStringBuilder();
      if (delta > 0)
      {
        original = this.ParamUpEffect;
        stringBuilder.Append('+');
      }
      else if (delta < 0)
        original = this.ParamDownEffect;
      if ((UnityEngine.Object) original == (UnityEngine.Object) null)
        return;
      GameObject go1 = UnityEngine.Object.Instantiate<GameObject>(original);
      go1.transform.SetParent(go.transform, false);
      (go1.transform as RectTransform).anchoredPosition = Vector2.zero;
      stringBuilder.Append(delta);
      UnityEngine.UI.Text componentInChildren = go1.GetComponentInChildren<UnityEngine.UI.Text>();
      if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
        componentInChildren.text = stringBuilder.ToString();
      go1.RequireComponent<DestructTimer>();
    }

    [DebuggerHidden]
    private IEnumerator PostEquip(bool doEquipCountUp = true)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPostEquip\u003Ec__Iterator2() { doEquipCountUp = doEquipCountUp, \u0024this = this };
    }

    [DebuggerHidden]
    private IEnumerator PostJobMaster()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPostJobMaster\u003Ec__Iterator3() { \u0024this = this };
    }

    private void PlayReaction()
    {
      if ((UnityEngine.Object) this.mCurrentPreview == (UnityEngine.Object) null)
        return;
      this.mCurrentPreview.PlayAction = true;
    }

    private void SetUpUnitVoice()
    {
      if (this.mUnitVoice != null)
      {
        this.mUnitVoice.StopAll(1f);
        this.mUnitVoice.Cleanup();
        this.mUnitVoice = (MySound.Voice) null;
      }
      string skinVoiceSheetName = this.mCurrentUnit.GetUnitSkinVoiceSheetName(-1);
      if (string.IsNullOrEmpty(skinVoiceSheetName))
      {
        DebugUtility.LogError("UnitDataにボイス設定が存在しません");
      }
      else
      {
        string sheetName = "VO_" + skinVoiceSheetName;
        string cueNamePrefix = this.mCurrentUnit.GetUnitSkinVoiceCueName(-1) + "_";
        this.mUnitVoice = new MySound.Voice(sheetName, skinVoiceSheetName, cueNamePrefix, false);
      }
    }

    private void PlayUnitVoice(string name)
    {
      if (this.MuteVoice || this.mCurrentUnit == null)
        return;
      if (this.mUnitVoice == null)
      {
        DebugUtility.LogError("UnitVoiceが存在しません");
      }
      else
      {
        this.mUnitVoice.Play(name, 0.0f, true);
        this.mLeftTime = Time.realtimeSinceStartup;
      }
    }

    private void StopUnitVoice()
    {
      string skinVoiceSheetName = this.mCurrentUnit.GetUnitSkinVoiceSheetName(-1);
      if (string.IsNullOrEmpty(skinVoiceSheetName))
        DebugUtility.LogError("UnitDataにボイス設定が存在しません");
      else
        MySound.Voice.StopAll(skinVoiceSheetName, 0.0f, true);
    }

    private void SpawnJobChangeButtonEffect()
    {
      if ((UnityEngine.Object) this.JobChangeButton == (UnityEngine.Object) null || (UnityEngine.Object) this.JobChangeButton == (UnityEngine.Object) null)
        return;
      UIUtility.SpawnParticle(this.JobChangeButtonEffect, this.JobChangeButton.transform as RectTransform, new Vector2(0.5f, 0.5f));
    }

    private void SpawnEquipEffect(int slot)
    {
      if ((UnityEngine.Object) this.EquipSlotEffect == (UnityEngine.Object) null || slot < 0 || (slot >= this.mEquipmentPanel.EquipmentSlots.Length || (UnityEngine.Object) this.mEquipmentPanel.EquipmentSlots[slot] == (UnityEngine.Object) null))
        return;
      UIUtility.SpawnParticle(this.EquipSlotEffect, this.mEquipmentPanel.EquipmentSlots[slot].transform as RectTransform, new Vector2(0.5f, 0.5f));
    }

    private void OnTabChange(SRPG_Button button)
    {
      if (!this.TabChange(button))
        return;
      UnitEnhancePanel mTabPage = this.mTabPages[(int) this.mActiveTabIndex];
      this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
      if ((UnityEngine.Object) mTabPage == (UnityEngine.Object) this.mEquipmentPanel)
      {
        this.PlayUnitVoice("chara_0012");
        if (this.mEquipmentPanelDirty)
          this.RefreshEquipments(-1);
      }
      if ((UnityEngine.Object) mTabPage == (UnityEngine.Object) this.mAbilityListPanel)
      {
        this.PlayUnitVoice("chara_0014");
        if (this.mAbilityListDirty)
          this.RefreshAbilityList();
      }
      if ((UnityEngine.Object) mTabPage == (UnityEngine.Object) this.mAbilitySlotPanel)
      {
        this.PlayUnitVoice("chara_0015");
        if (this.mAbilitySlotDirty)
          this.RefreshAbilitySlots(false);
      }
      if ((UnityEngine.Object) mTabPage == (UnityEngine.Object) this.mKyokaPanel)
        this.PlayUnitVoice("chara_0016");
      this.mLeftTime = Time.realtimeSinceStartup;
    }

    private bool TabChange(SRPG_Button button)
    {
      if (!button.IsInteractable())
        return false;
      foreach (UnitEnhanceV3.eTabButtons key in this.mTabButtons.Keys)
      {
        if (this.mTabButtons.ContainsKey(key) && !((UnityEngine.Object) this.mTabButtons[key] == (UnityEngine.Object) null))
        {
          bool flag = (UnityEngine.Object) this.mTabButtons[key] == (UnityEngine.Object) button;
          if (flag)
          {
            this.mActiveTabIndex = key;
            if ((UnityEngine.Object) this.mTabPages[(int) this.mActiveTabIndex] == (UnityEngine.Object) null)
              this.CreateTabPage(this.mActiveTabIndex);
          }
          this.mTabButtons[key].IsOn = flag;
          if ((UnityEngine.Object) this.mTabPages[(int) key] != (UnityEngine.Object) null)
          {
            Canvas component = this.mTabPages[(int) key].GetComponent<Canvas>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.enabled = flag;
          }
        }
      }
      return true;
    }

    private void CreateTabPage(UnitEnhanceV3.eTabButtons tab)
    {
      switch (tab)
      {
        case UnitEnhanceV3.eTabButtons.Equipments:
          this.mEquipmentPanel = this.InitTabPage((int) tab, this.Prefab_Equipments, true);
          this.InitEquipmentPanel(this.mEquipmentPanel);
          break;
        case UnitEnhanceV3.eTabButtons.Kyoka:
          this.mKyokaPanel = this.InitTabPage((int) tab, this.Prefab_Kyoka, false);
          this.InitKyokaPanel(this.mKyokaPanel);
          break;
        case UnitEnhanceV3.eTabButtons.AbilityList:
          this.mAbilityListPanel = this.InitTabPage((int) tab, this.Prefab_AbilityList, false);
          this.mAbilityListPanel.AbilityList.OnAbilityRankUp = new UnitAbilityList.AbilityEvent(this.OnAbilityRankUpSet);
          this.mAbilityListPanel.AbilityList.OnRankUpBtnPress = new UnitAbilityList.AbilityEvent(this.OnRankUpButtonPressHold);
          this.mAbilityListPanel.AbilityList.OnAbilityRankUpExec = new UnitAbilityList.AbilityEvent(this.OnAbilityRankUpExec);
          break;
        case UnitEnhanceV3.eTabButtons.AbilitySlot:
          this.mAbilitySlotPanel = this.InitTabPage((int) tab, this.Prefab_AbilitySlots, false);
          this.mAbilitySlotPanel.AbilitySlots.OnAbilityRankUp = new UnitAbilityList.AbilityEvent(this.OnAbilityRankUp);
          this.mAbilitySlotPanel.AbilitySlots.OnSlotSelect = new UnitAbilityList.AbilitySlotEvent(this.OnAbilitySlotSelect);
          break;
      }
    }

    private UnitEnhancePanel InitTabPage(int pageIndex, UnitEnhancePanel prefab, bool visible)
    {
      if ((UnityEngine.Object) prefab == (UnityEngine.Object) null)
        return (UnitEnhancePanel) null;
      this.mTabPages[pageIndex] = UnityEngine.Object.Instantiate<UnitEnhancePanel>(prefab);
      this.mTabPages[pageIndex].transform.SetParent((Transform) this.TabPageParent, false);
      Canvas component = this.mTabPages[pageIndex].GetComponent<Canvas>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.enabled = visible;
      return this.mTabPages[pageIndex];
    }

    private void InitEquipmentPanel(UnitEnhancePanel panel)
    {
      for (int index = 0; index < panel.EquipmentSlots.Length; ++index)
      {
        if ((UnityEngine.Object) panel.EquipmentSlots[index] != (UnityEngine.Object) null)
          panel.EquipmentSlots[index].OnSelect = new ListItemEvents.ListItemEvent(this.OnEquipmentSlotSelect);
      }
      if ((UnityEngine.Object) panel.JobRankUpButton != (UnityEngine.Object) null)
        panel.JobRankUpButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnJobRankUpClick));
      if ((UnityEngine.Object) panel.JobUnlockButton != (UnityEngine.Object) null)
        panel.JobUnlockButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnJobRankUpClick));
      if ((UnityEngine.Object) panel.AllEquipButton != (UnityEngine.Object) null)
        panel.AllEquipButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnJobRankUpClick));
      if ((UnityEngine.Object) panel.ArtifactSlot != (UnityEngine.Object) null)
        panel.ArtifactSlot.OnSelect = new GenericSlot.SelectEvent(this.OnArtifactClick);
      if ((UnityEngine.Object) panel.ArtifactSlot2 != (UnityEngine.Object) null)
        panel.ArtifactSlot2.OnSelect = new GenericSlot.SelectEvent(this.OnArtifactClick);
      if ((UnityEngine.Object) panel.ArtifactSlot3 != (UnityEngine.Object) null)
        panel.ArtifactSlot3.OnSelect = new GenericSlot.SelectEvent(this.OnArtifactClick);
      if ((UnityEngine.Object) panel.JobRankupAllIn != (UnityEngine.Object) null)
        panel.JobRankupAllIn.AddListener(new SRPG_Button.ButtonClickEvent(this.OnJobRankUpClick));
      if (!((UnityEngine.Object) panel.mConceptCardSlot != (UnityEngine.Object) null))
        return;
      panel.mConceptCardSlot.SelectButton.onClick.AddListener(new UnityAction(this.OnEquipCardSlotSelect));
    }

    private void InitKyokaPanel(UnitEnhancePanel panel)
    {
      RectTransform expItemList = panel.ExpItemList;
      ListItemEvents expItemTemplate = panel.ExpItemTemplate;
      SRPG_Button unitLevelupButton = panel.UnitLevelupButton;
      if ((UnityEngine.Object) expItemList == (UnityEngine.Object) null || (UnityEngine.Object) expItemTemplate == (UnityEngine.Object) null || (UnityEngine.Object) unitLevelupButton == (UnityEngine.Object) null)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = 0; index < player.Items.Count; ++index)
      {
        ItemData itemData = player.Items[index];
        if (itemData != null && itemData.Param != null && (itemData.Param.type == EItemType.ExpUpUnit && this.ShowNoStockExpPotions) && itemData.Num > 0)
        {
          ItemData data = new ItemData();
          data.Setup(itemData.UniqueID, itemData.Param, itemData.NumNonCap);
          ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(expItemTemplate);
          if (this.HoldUseItemEnable)
          {
            listItemEvents.gameObject.AddComponent<UnitEnhanceV3.ExpItemTouchController>();
            UnitEnhanceV3.ExpItemTouchController component = listItemEvents.gameObject.GetComponent<UnitEnhanceV3.ExpItemTouchController>();
            component.OnPointerDownFunc = new UnitEnhanceV3.ExpItemTouchController.DelegateOnPointerDown(this.OnExpItemButtonDown);
            component.OnPointerUpFunc = new UnitEnhanceV3.ExpItemTouchController.DelegateOnPointerDown(this.OnExpItemButtonUp);
            component.UseItemFunc = new UnitEnhanceV3.ExpItemTouchController.DelegateUseItem(this.OnExpItemHoldUse);
          }
          else
            listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnExpItemClick);
          this.mExpItemGameObjects.Add(listItemEvents.gameObject);
          DataSource.Bind<ItemData>(listItemEvents.gameObject, data, false);
          listItemEvents.gameObject.SetActive(true);
          listItemEvents.transform.SetParent(expItemList.transform, false);
          this.mTmpItems.Add(data);
        }
      }
      unitLevelupButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnExpMaxOpen));
    }

    private void OnExpItemButtonDown(UnitEnhanceV3.ExpItemTouchController controller)
    {
      this.mCurrentKyoukaItemHold = controller;
    }

    private void OnExpItemButtonUp(UnitEnhanceV3.ExpItemTouchController controller)
    {
      if (!((UnityEngine.Object) this.mCurrentKyoukaItemHold != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mKyoukaItemScroll != (UnityEngine.Object) null))
        return;
      this.mKyoukaItemScroll.enabled = true;
      this.mKyoukaItemScroll = (ScrollRect) null;
    }

    private void OnExpItemHoldUse(GameObject listItem)
    {
      this.OnExpItemClick(listItem);
      if (!((UnityEngine.Object) this.mCurrentKyoukaItemHold != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mKyoukaItemScroll == (UnityEngine.Object) null))
        return;
      this.mKyoukaItemScroll = this.mKyokaPanel.GetComponentInChildren<ScrollRect>();
      if (!((UnityEngine.Object) this.mKyoukaItemScroll != (UnityEngine.Object) null))
        return;
      this.mKyoukaItemScroll.enabled = false;
    }

    private void OnExpOverflowOk(GameObject dialog)
    {
      this.OnExpItemClick(this.ClickItem);
    }

    private void OnExpOverflowNo(GameObject dialog)
    {
      if ((UnityEngine.Object) this.ClickItem != (UnityEngine.Object) null)
      {
        UnitEnhanceV3.ExpItemTouchController component = this.ClickItem.GetComponent<UnitEnhanceV3.ExpItemTouchController>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.StatusReset();
      }
      this.ClickItem = (GameObject) null;
      if (!((UnityEngine.Object) this.mKyoukaItemScroll != (UnityEngine.Object) null))
        return;
      this.mKyoukaItemScroll.enabled = true;
      this.mKyoukaItemScroll = (ScrollRect) null;
    }

    private void OnExpItemClick(GameObject go)
    {
      if (this.mSceneChanging || this.ExecQueuedKyokaRequest(new UnitEnhanceV3.DeferredJob(this.SubmitUnitKyoka)))
        return;
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
      if (dataOfClass == null || dataOfClass.Num <= 0)
      {
        Button component = go.GetComponent<Button>();
        if (!((UnityEngine.Object) component != (UnityEngine.Object) null) || !component.interactable)
          return;
        component.interactable = false;
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0015", 0.0f);
      }
      else if (!this.mCurrentUnit.CheckGainExp())
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.LEVEL_CAPPED"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        if (!this.HoldUseItemEnable || !((UnityEngine.Object) this.mCurrentKyoukaItemHold != (UnityEngine.Object) null))
          return;
        this.mCurrentKyoukaItemHold.StatusReset();
        this.mCurrentKyoukaItemHold = (UnitEnhanceV3.ExpItemTouchController) null;
      }
      else
      {
        if ((UnityEngine.Object) this.ClickItem == (UnityEngine.Object) null)
        {
          GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
          int lv = instanceDirect.Player.Lv;
          int levelCap = this.mCurrentUnit.GetLevelCap(false);
          if ((lv >= levelCap ? instanceDirect.MasterParam.GetUnitLevelExp(levelCap) - this.mCurrentUnit.Exp : instanceDirect.MasterParam.GetUnitLevelExp(lv + 1) - 1 - this.mCurrentUnit.Exp) < dataOfClass.Param.value)
          {
            this.ClickItem = go;
            this.mCurrentKyoukaItemHold.StatusReset();
            UIUtility.ConfirmBox(LocalizedText.Get(this.ExpOverflowWarning), new UIUtility.DialogResultEvent(this.OnExpOverflowOk), new UIUtility.DialogResultEvent(this.OnExpOverflowNo), (GameObject) null, true, -1, (string) null, (string) null);
            return;
          }
        }
        CustomSound2 component1 = go.GetComponent<CustomSound2>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          component1.Play();
        if ((UnityEngine.Object) this.ClickItem != (UnityEngine.Object) null)
          this.ClickItem = (GameObject) null;
        this.mCurrentUnitUnlocks = this.mCurrentUnit.UnlockedSkillIds();
        if (this.mUsedExpItems.ContainsKey(dataOfClass.Param.iname))
        {
          Dictionary<string, int> mUsedExpItems;
          string iname;
          (mUsedExpItems = this.mUsedExpItems)[iname = dataOfClass.Param.iname] = mUsedExpItems[iname] + 1;
        }
        else
          this.mUsedExpItems.Add(dataOfClass.Param.iname, 1);
        int lv1 = this.mCurrentUnit.Lv;
        int exp = this.mCurrentUnit.Exp;
        this.BeginStatusChangeCheck();
        if (!MonoSingleton<GameManager>.Instance.Player.UseExpPotion(this.mCurrentUnit, dataOfClass))
          return;
        if (dataOfClass.Num <= 0)
        {
          Button component2 = go.GetComponent<Button>();
          if ((UnityEngine.Object) component2 != (UnityEngine.Object) null && component2.interactable)
          {
            component2.interactable = false;
            MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0015", 0.0f);
          }
        }
        int delta = this.mCurrentUnit.Exp - exp;
        if (delta <= 0)
          this.SpawnAddExpEffect(delta, dataOfClass.Param);
        this.AnimateGainExp(this.mCurrentUnit.Exp);
        this.QueueKyokaRequest(new UnitEnhanceV3.DeferredJob(this.SubmitUnitKyoka), 0.0f);
        GameParameter.UpdateAll(go);
        if (this.mCurrentUnit.Lv == lv1)
          return;
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.Unit);
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.ItemEquipment);
        this.mKeepWindowLocked = true;
        this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
        if (this.HoldUseItemEnable && this.HoldUseItemLvUpStop && (UnityEngine.Object) this.mCurrentKyoukaItemHold != (UnityEngine.Object) null)
        {
          this.mCurrentKyoukaItemHold.StatusReset();
          this.mCurrentKyoukaItemHold = (UnitEnhanceV3.ExpItemTouchController) null;
        }
        this.StartCoroutine(this.PostUnitLevelUp(lv1));
      }
    }

    private void RefreshExpItemsButtonState()
    {
      if (this.mExpItemGameObjects == null || this.mExpItemGameObjects.Count < 0)
        return;
      for (int index = 0; index < this.mExpItemGameObjects.Count; ++index)
      {
        GameObject expItemGameObject = this.mExpItemGameObjects[index];
        if ((UnityEngine.Object) expItemGameObject != (UnityEngine.Object) null)
        {
          ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(expItemGameObject, (ItemData) null);
          if (dataOfClass != null && dataOfClass.Num <= 0)
          {
            Button component = expItemGameObject.GetComponent<Button>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null && component.interactable)
              component.interactable = false;
          }
        }
      }
    }

    private void OnUnitBulkLevelUp(Dictionary<string, int> data)
    {
      if (this.mSceneChanging || data == null || data.Count <= 0)
        return;
      this.mCurrentUnitUnlocks = this.mCurrentUnit.UnlockedSkillIds();
      int lv = this.mCurrentUnit.Lv;
      this.BeginStatusChangeCheck();
      foreach (KeyValuePair<string, int> keyValuePair in data)
      {
        KeyValuePair<string, int> p = keyValuePair;
        ItemData itemData = this.mTmpItems.Find((Predicate<ItemData>) (tmp => tmp.ItemID == p.Key));
        if (itemData != null)
        {
          for (int index = 0; index < p.Value; ++index)
          {
            if (!MonoSingleton<GameManager>.Instance.Player.UseExpPotion(this.mCurrentUnit, itemData))
              return;
          }
        }
      }
      foreach (KeyValuePair<string, int> keyValuePair in data)
      {
        if (this.mUsedExpItems.ContainsKey(keyValuePair.Key))
        {
          Dictionary<string, int> mUsedExpItems;
          string key;
          (mUsedExpItems = this.mUsedExpItems)[key = keyValuePair.Key] = mUsedExpItems[key] + keyValuePair.Value;
        }
        else
          this.mUsedExpItems.Add(keyValuePair.Key, keyValuePair.Value);
      }
      this.AnimateGainExp(this.mCurrentUnit.Exp);
      this.QueueKyokaRequest(new UnitEnhanceV3.DeferredJob(this.SubmitUnitKyoka), 0.0f);
      GameParameter.UpdateAll(this.mKyokaPanel.gameObject);
      this.RefreshExpItemsButtonState();
      if (this.mCurrentUnit.Lv == lv)
        return;
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.Unit);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.ItemEquipment);
      this.mKeepWindowLocked = true;
      this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
      this.StartCoroutine(this.PostUnitLevelUp(lv));
    }

    private void SpawnAddExpEffect(int delta, ItemParam item)
    {
    }

    private void AnimateGainExp(int desiredExp)
    {
      if ((double) this.mExpStart < (double) this.mExpEnd && (double) this.mExpAnimTime < 1.0)
        this.mExpStart = Mathf.Lerp(this.mExpStart, this.mExpEnd, this.mExpAnimTime / 1f);
      this.mExpAnimTime = 0.0f;
      this.mExpEnd = (float) desiredExp;
    }

    [DebuggerHidden]
    private IEnumerator PostUnitLevelUp(int prevLv)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPostUnitLevelUp\u003Ec__Iterator4() { prevLv = prevLv, \u0024this = this };
    }

    private void RefreshLevelCap()
    {
      int lv = MonoSingleton<GameManager>.Instance.Player.Lv;
      int levelCap = this.mCurrentUnit.GetLevelCap(false);
      if ((UnityEngine.Object) this.UnitLevel != (UnityEngine.Object) null)
      {
        this.UnitLevel.text = this.mCurrentUnit.Lv.ToString();
        this.UnitLevel.color = (Color) (this.mCurrentUnit.Lv >= levelCap || this.mCurrentUnit.Lv >= lv ? this.CappedUnitLevelColor : this.UnitLevelColor);
      }
      if (!((UnityEngine.Object) this.UnitLevelCapInfo != (UnityEngine.Object) null))
        return;
      this.UnitLevelCapInfo.SetActive(this.mCurrentUnit.Lv >= lv);
    }

    private void RefreshEXPImmediate()
    {
      if ((UnityEngine.Object) this.UnitEXPSlider == (UnityEngine.Object) null)
        return;
      int exp1 = this.mCurrentUnit.Exp;
      int exp2 = this.mCurrentUnit.GetExp();
      int num = exp2 + this.mCurrentUnit.GetNextExp();
      this.mExpStart = this.mExpEnd = (float) exp1;
      this.RefreshLevelCap();
      if ((UnityEngine.Object) this.UnitExp != (UnityEngine.Object) null)
        this.UnitExp.text = exp1.ToString();
      if ((UnityEngine.Object) this.UnitExpMax != (UnityEngine.Object) null)
        this.UnitExpMax.text = num.ToString();
      if ((UnityEngine.Object) this.UnitExpNext != (UnityEngine.Object) null)
        this.UnitExpNext.text = this.mCurrentUnit.GetNextExp().ToString();
      if (num <= 0)
        num = 1;
      this.UnitEXPSlider.AnimateValue(Mathf.Clamp01((float) exp2 / (float) num), 0.0f);
    }

    private bool ExecQueuedKyokaRequest(UnitEnhanceV3.DeferredJob func)
    {
      if (this.mDefferedCallFunc == null || !(this.mDefferedCallFunc != func))
        return this.mSendingKyokaRequest;
      this.GetComponent<WindowController>().SetCollision(false);
      UnitEnhanceV3.DeferredJob defferedCallFunc = this.mDefferedCallFunc;
      this.mDefferedCallFunc = (UnitEnhanceV3.DeferredJob) null;
      this.mSendingKyokaRequest = true;
      this.mDefferedCallDelay = 0.0f;
      defferedCallFunc();
      return true;
    }

    private bool IsKyokaRequestQueued
    {
      get
      {
        return this.mDefferedCallFunc != null;
      }
    }

    private void FinishKyokaRequest()
    {
      this.mSendingKyokaRequest = false;
      this.RefreshSortedUnits();
      this.UpdateJobRankUpButtonState();
      if (!this.mKeepWindowLocked && this.GetComponent<WindowController>().IsOpened)
        this.GetComponent<WindowController>().SetCollision(true);
      this.CheckPlayBackUnlock();
    }

    private Coroutine SyncKyokaRequest()
    {
      return this.StartCoroutine(this.WaitForKyokaRequestAsync(true));
    }

    [DebuggerHidden]
    private IEnumerator WaitForKyokaRequestAsync(bool unlockWindow)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CWaitForKyokaRequestAsync\u003Ec__Iterator5() { unlockWindow = unlockWindow, \u0024this = this };
    }

    private void QueueKyokaRequest(UnitEnhanceV3.DeferredJob func, float delay)
    {
      this.mDefferedCallFunc = func;
      this.mDefferedCallDelay = delay;
    }

    private bool IsUnitImageFading
    {
      get
      {
        return (double) this.mBGUnitImgFadeTime < (double) this.mBGUnitImgFadeTimeMax;
      }
    }

    private void FadeUnitImage(float alphaStart, float alphaEnd, float duration)
    {
      this.mBGUnitImgAlphaStart = alphaStart;
      this.mBGUnitImgAlphaEnd = alphaEnd;
      this.mBGUnitImgFadeTime = 0.0f;
      this.mBGUnitImgFadeTimeMax = duration;
      if ((double) duration > 0.0)
        return;
      this.SetUnitImageAlpha(this.mBGUnitImgAlphaEnd);
    }

    private void SetUnitImageAlpha(float alpha)
    {
      if ((UnityEngine.Object) this.mBGUnitImage == (UnityEngine.Object) null)
        return;
      Color color = this.mBGUnitImage.color;
      color.a = alpha;
      this.mBGUnitImage.color = color;
    }

    private void Update()
    {
      if (this.GetComponent<WindowController>().IsOpened)
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnitID);
        if (unitDataByUniqueId != null && (double) unitDataByUniqueId.LastSyncTime > (double) this.mLastSyncTime)
        {
          long jobUniqueID = GlobalVars.SelectedJobUniqueID.Get();
          this.Refresh(this.mCurrentUnitID, unitDataByUniqueId.Jobs[this.mSelectedJobIndex].UniqueID, true, true);
          this.RefreshReturningJobState(jobUniqueID);
        }
      }
      if (this.mUpdatePreviewVisibility && this.mDesiredPreviewVisibility && ((UnityEngine.Object) this.mCurrentPreview != (UnityEngine.Object) null && !this.mCurrentPreview.IsLoading) && !this.mCurrentPreview.LoadThreadFlag)
      {
        GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerCH, true);
        this.mUpdatePreviewVisibility = false;
      }
      if ((double) this.mBGUnitImgFadeTime < (double) this.mBGUnitImgFadeTimeMax && (UnityEngine.Object) this.mBGUnitImage != (UnityEngine.Object) null)
      {
        this.mBGUnitImgFadeTime += Time.unscaledDeltaTime;
        float t = Mathf.Clamp01(this.mBGUnitImgFadeTime / this.mBGUnitImgFadeTimeMax);
        this.SetUnitImageAlpha(Mathf.Lerp(this.mBGUnitImgAlphaStart, this.mBGUnitImgAlphaEnd, t));
        if ((double) t >= 1.0)
        {
          this.mBGUnitImgFadeTime = 0.0f;
          this.mBGUnitImgFadeTimeMax = 0.0f;
        }
      }
      if ((double) this.mExpStart < (double) this.mExpEnd)
        this.AnimateExp();
      if ((double) this.mDefferedCallDelay > 0.0)
      {
        this.mDefferedCallDelay -= Time.unscaledDeltaTime;
        if ((double) this.mDefferedCallDelay <= 0.0)
          this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
      }
      if ((UnityEngine.Object) this.mCurrentKyoukaItemHold != (UnityEngine.Object) null && !this.mKeepWindowLocked)
      {
        if (this.mCurrentKyoukaItemHold.Holding)
        {
          this.mCurrentKyoukaItemHold.UpdateTimer(Time.unscaledDeltaTime);
        }
        else
        {
          this.mCurrentKyoukaItemHold = (UnitEnhanceV3.ExpItemTouchController) null;
          if ((UnityEngine.Object) this.mKyoukaItemScroll != (UnityEngine.Object) null)
            this.mKyoukaItemScroll.scrollSensitivity = 1f;
        }
      }
      if ((UnityEngine.Object) this.mCurrentAbilityRankUpHold != (UnityEngine.Object) null)
      {
        if (this.mCurrentAbilityRankUpHold.Holding)
        {
          this.mCurrentAbilityRankUpHold.UpdatePress(Time.unscaledDeltaTime);
        }
        else
        {
          this.mCurrentAbilityRankUpHold = (UnitAbilityListItemEvents.ListItemTouchController) null;
          this.UpdateArtifactSetResult();
        }
      }
      if ((double) Time.realtimeSinceStartup - (double) this.mLeftTime <= (double) UnitEnhanceV3.PLAY_LEFTVOICE_SPAN || !this.IsCanPlayLeftVoice())
        return;
      this.PlayUnitVoice("chara_0008");
    }

    private bool IsCanPlayLeftVoice()
    {
      return !this.IsStopPlayLeftVoice && (!((UnityEngine.Object) this.mUnitModelViewer != (UnityEngine.Object) null) || !this.mUnitModelViewer.gameObject.activeSelf) && (!((UnityEngine.Object) this.mTobiraInventoryWindow != (UnityEngine.Object) null) || !this.mTobiraInventoryWindow.gameObject.activeSelf);
    }

    private void AnimateExp()
    {
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      int levelCap = this.mCurrentUnit.GetLevelCap(false);
      float f1 = Mathf.Lerp(this.mExpStart, this.mExpEnd, this.mExpAnimTime / 1f);
      this.mExpAnimTime += Time.unscaledDeltaTime;
      float f2 = Mathf.Lerp(this.mExpStart, this.mExpEnd, Mathf.Clamp01(this.mExpAnimTime / 1f));
      int totalExp1 = Mathf.FloorToInt(f1);
      int totalExp2 = Mathf.FloorToInt(f2);
      int num1 = masterParam.CalcUnitLevel(totalExp1, levelCap);
      int lv = masterParam.CalcUnitLevel(totalExp2, levelCap);
      if (num1 == lv)
        ;
      int unitLevelExp = masterParam.GetUnitLevelExp(lv);
      int num2 = masterParam.GetUnitLevelExp(Mathf.Min(lv + 1, levelCap)) - unitLevelExp;
      if ((UnityEngine.Object) this.UnitLevel != (UnityEngine.Object) null)
        this.UnitLevel.text = lv.ToString();
      if ((UnityEngine.Object) this.UnitExp != (UnityEngine.Object) null)
        this.UnitExp.text = (totalExp2 - unitLevelExp).ToString();
      if ((UnityEngine.Object) this.UnitExpMax != (UnityEngine.Object) null)
        this.UnitExpMax.text = num2.ToString();
      if ((UnityEngine.Object) this.UnitExpNext != (UnityEngine.Object) null)
        this.UnitExpNext.text = Mathf.FloorToInt((float) (num2 - (totalExp2 - unitLevelExp))).ToString();
      if ((UnityEngine.Object) this.UnitEXPSlider != (UnityEngine.Object) null)
        this.UnitEXPSlider.AnimateValue(num2 <= 0 ? 1f : (f2 - (float) unitLevelExp) / (float) num2, 0.0f);
      if ((double) this.mExpAnimTime < 1.0)
        return;
      this.mExpStart = this.mExpEnd;
      this.mExpAnimTime = 0.0f;
      this.RefreshLevelCap();
    }

    private void SubmitUnitKyoka()
    {
      if (Network.Mode == Network.EConnectMode.Online)
        Network.RequestAPI((WebAPI) new ReqUnitExpAdd(this.mCurrentUnitID, this.mUsedExpItems, new Network.ResponseCallback(this.OnUnitKyokaResult)), false);
      else
        this.FinishKyokaRequest();
      this.mUsedExpItems.Clear();
      this.SetUnitDirty();
    }

    private void OnUnitKyokaResult(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.ExpMaterialShort)
          FlowNode_Network.Failed();
        else
          FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        if (jsonObject.body == null)
        {
          FlowNode_Network.Failed();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
            this.mLastSyncTime = Time.realtimeSinceStartup;
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Failed();
            return;
          }
          Network.RemoveAPI();
          this.FinishKyokaRequest();
        }
      }
    }

    public void OpenEquipmentSlot(int slotIndex)
    {
      if (slotIndex < 0)
        return;
      this.mSelectedEquipmentSlot = slotIndex;
      this.GetComponent<WindowController>().SetCollision(false);
      this.EquipmentWindow.GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnEquipmentWindowCancel);
      WindowController.OpenIfAvailable((Component) this.EquipmentWindow);
      if ((UnityEngine.Object) this.EquipmentWindow.SubWindow != (UnityEngine.Object) null)
        this.EquipmentWindow.SubWindow.SetActive(false);
      this.EquipmentWindow.Refresh(this.mCurrentUnit, slotIndex);
    }

    private void OnEquipmentSlotSelect(GameObject go)
    {
      int slotIndex = Array.IndexOf<UnitEquipmentSlotEvents>(this.mEquipmentPanel.EquipmentSlots, go.GetComponent<UnitEquipmentSlotEvents>());
      GlobalVars.SelectedEquipmentSlot.Set(slotIndex);
      this.OpenEquipmentSlot(slotIndex);
    }

    private void OnEquipCardSlotSelect()
    {
      GameObject original = AssetManager.Load<GameObject>(UnitEnhanceV3.CONCEPT_CARD_EQUIP_WINDOW_PREFAB_PATH);
      if ((UnityEngine.Object) original == (UnityEngine.Object) null)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
      ConceptCardEquipWindow component = gameObject.GetComponent<ConceptCardEquipWindow>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.Init(this.mCurrentUnit);
      gameObject.AddComponent<DestroyEventListener>().Listeners += (DestroyEventListener.DestroyEvent) (go => this.OnCloseEquipConceptCardWindow());
      this.BeginStatusChangeCheck();
    }

    public void OnEquipConceptCardSelect()
    {
      if (MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnitID) == null || this.mCurrentUnit == null)
        return;
      this.RebuildUnitData();
      this.SetUnitDirty();
      this.RefreshBasicParameters(false);
      this.RefreshSortedUnits();
      this.RefreshUnitShiftButton();
      this.ShowSkinSetResult();
      this.FadeUnitImage(0.0f, 0.0f, 0.0f);
      this.StartCoroutine(this.RefreshUnitImage());
      this.FadeUnitImage(0.0f, 1f, 1f);
    }

    private void OnCloseEquipConceptCardWindow()
    {
      this.SpawnStatusChangeEffects();
    }

    private void OnEquipmentWindowCancel(GameObject go, bool visible)
    {
      if (visible)
        return;
      this.EquipmentWindow.GetComponent<WindowController>().OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      this.RefreshEquipments(-1);
      this.GetComponent<WindowController>().SetCollision(true);
    }

    private void OnJobChangeClick(SRPG_Button button)
    {
      this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
      if (this.mCurrentUnit.CurrentJob.Rank <= 0)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.UE_JOB_NOT_UNLOCKED"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        if (this.mCurrentUnit.CurrentJob.UniqueID == this.mCurrentJobUniqueID || !button.IsInteractable())
          return;
        JobParam jobParam = this.mCurrentUnit.CurrentJob.Param;
        StringBuilder stringBuilder = GameUtility.GetStringBuilder();
        stringBuilder.Append(LocalizedText.Get("sys.UE_JOBCHANGE_CONFIRM", new object[1]
        {
          (object) jobParam.name
        }));
        stringBuilder.Append('\n');
        string[] jobChangeItems = jobParam.GetJobChangeItems(this.mCurrentUnit.CurrentJob.Rank);
        int[] jobChangeItemNums = jobParam.GetJobChangeItemNums(this.mCurrentUnit.CurrentJob.Rank);
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if (jobChangeItems != null && jobChangeItemNums != null)
        {
          for (int index = 0; index < jobChangeItems.Length; ++index)
          {
            if (!string.IsNullOrEmpty(jobChangeItems[index]))
            {
              ItemParam itemParam = instance.GetItemParam(jobChangeItems[index]);
              if (itemParam != null)
              {
                int num = jobChangeItemNums[index];
                stringBuilder.Append(LocalizedText.Get("sys.UE_JOBCHANGE_REQITEM", (object) itemParam.name, (object) num));
                stringBuilder.Append('\n');
              }
            }
          }
        }
        int jobChangeCost = jobParam.GetJobChangeCost(this.mCurrentUnit.CurrentJob.Rank);
        if (jobChangeCost > 0)
          stringBuilder.Append(LocalizedText.Get("sys.UE_JOBCHANGE_REQGOLD", new object[1]
          {
            (object) jobChangeCost
          }));
        this.mPrevJobID = (string) null;
        for (int index = 0; index < this.mCurrentUnit.Jobs.Length; ++index)
        {
          if (this.mCurrentUnit.Jobs[index].UniqueID == this.mCurrentJobUniqueID)
            this.mPrevJobID = this.mCurrentUnit.Jobs[index].Param.iname;
        }
        this.mNextJobID = this.mCurrentUnit.CurrentJob.Param.iname;
        UIUtility.ConfirmBox(stringBuilder.ToString(), new UIUtility.DialogResultEvent(this.OnJobChangeAccept), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
      }
    }

    private void OnJobChangeAccept(GameObject go)
    {
      JobParam jobParam = this.mCurrentUnit.CurrentJob.Param;
      string[] jobChangeItems = jobParam.GetJobChangeItems(this.mCurrentUnit.CurrentJob.Rank);
      int[] jobChangeItemNums = jobParam.GetJobChangeItemNums(this.mCurrentUnit.CurrentJob.Rank);
      bool flag1 = true;
      bool flag2 = true;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (jobChangeItems != null && jobChangeItemNums != null)
      {
        for (int index = 0; index < jobChangeItems.Length; ++index)
        {
          if (!string.IsNullOrEmpty(jobChangeItems[index]))
          {
            ItemParam itemParam = instance.GetItemParam(jobChangeItems[index]);
            if (itemParam != null && flag1 && instance.Player.FindItemDataByItemParam(itemParam).Num < jobChangeItemNums[index])
              flag1 = false;
          }
        }
      }
      int jobChangeCost = jobParam.GetJobChangeCost(this.mCurrentUnit.CurrentJob.Rank);
      if (jobChangeCost > 0 && instance.Player.Gold < jobChangeCost)
        flag2 = false;
      if (!flag1 && !flag2)
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.UE_JOBCHANGE_NOITEMGOLD"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else if (!flag1)
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.UE_JOBCHANGE_NOITEM"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else if (!flag2)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.UE_JOBCHANGE_NOGOLD"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        this.SpawnJobChangeButtonEffect();
        this.mKeepWindowLocked = true;
        this.GetComponent<WindowController>().SetCollision(false);
        this.mJobChangeRequestSent = false;
        if (Network.Mode == Network.EConnectMode.Online)
        {
          this.RequestAPI((WebAPI) new ReqUnitJob(this.mCurrentUnit.UniqueID, this.mCurrentUnit.CurrentJob.UniqueID, new Network.ResponseCallback(this.OnJobChangeResult)));
        }
        else
        {
          MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnitID).SetJobIndex(this.mCurrentUnit.JobIndex);
          GlobalVars.SelectedUnitJobIndex.Set(this.mCurrentUnit.JobIndex);
          this.mJobChangeRequestSent = true;
          instance.Player.OnJobChange(this.mCurrentUnit.UnitID);
        }
        this.SetUnitDirty();
        this.StartCoroutine(this.PostJobChange());
      }
    }

    [DebuggerHidden]
    private IEnumerator PostJobChange()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPostJobChange\u003Ec__Iterator6() { \u0024this = this };
    }

    private void OnJobChangeResult(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoJobSetJob:
          case Network.EErrCode.CantSelectJob:
          case Network.EErrCode.NoUnitSetJob:
            FlowNode_Network.Failed();
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
          this.mLastSyncTime = Time.realtimeSinceStartup;
          Network.RemoveAPI();
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        this.mJobChangeRequestSent = true;
        this.RefreshSortedUnits();
        MonoSingleton<GameManager>.Instance.Player.OnJobChange(this.mCurrentUnit.UnitID);
        GlobalVars.SelectedUnitJobIndex.Set(this.mCurrentUnit.JobIndex);
      }
    }

    private void ShowLockEquipArtifactTooltip(GenericSlot slot)
    {
      if ((UnityEngine.Object) slot == (UnityEngine.Object) null)
        return;
      int index1 = 0;
      for (int index2 = 0; index2 < this.mCurrentUnit.Jobs.Length; ++index2)
      {
        if (this.mCurrentUnit.Jobs[index2].UniqueID == (long) GlobalVars.SelectedJobUniqueID)
        {
          index1 = index2;
          break;
        }
      }
      bool flag = this.mCurrentUnit.Jobs[index1].Rank > 0;
      if (flag && (UnityEngine.Object) slot == (UnityEngine.Object) this.mEquipmentPanel.ArtifactSlot)
        return;
      int index3 = 0;
      if ((UnityEngine.Object) slot == (UnityEngine.Object) this.mEquipmentPanel.ArtifactSlot2)
        index3 = 1;
      else if ((UnityEngine.Object) slot == (UnityEngine.Object) this.mEquipmentPanel.ArtifactSlot3)
        index3 = 2;
      if ((UnityEngine.Object) this.Prefab_LockedArtifactSlotTooltip == (UnityEngine.Object) null)
        return;
      if ((UnityEngine.Object) this.mEquipArtifactUnlockTooltip != (UnityEngine.Object) null)
      {
        this.mEquipArtifactUnlockTooltip.Close();
        this.mEquipArtifactUnlockTooltip = (Tooltip) null;
      }
      else
      {
        Tooltip.SetTooltipPosition(this.mEquipmentPanel.ArtifactSlot2.transform as RectTransform, new Vector2(0.5f, 0.5f));
        if ((UnityEngine.Object) this.mEquipArtifactUnlockTooltip == (UnityEngine.Object) null)
          this.mEquipArtifactUnlockTooltip = UnityEngine.Object.Instantiate<Tooltip>(this.Prefab_LockedArtifactSlotTooltip);
        else
          this.mEquipArtifactUnlockTooltip.ResetPosition();
        if (!((UnityEngine.Object) this.mEquipArtifactUnlockTooltip.TooltipText != (UnityEngine.Object) null))
          return;
        if (flag)
        {
          FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
          if (fixParam == null || fixParam.EquipArtifactSlotUnlock == null || fixParam.EquipArtifactSlotUnlock.Length <= 0)
            return;
          this.mEquipArtifactUnlockTooltip.TooltipText.text = LocalizedText.Get("sys.EQUIP_ARTIFACT_SLOT_TOOLTIP", new object[1]
          {
            (object) fixParam.EquipArtifactSlotUnlock[index3].ToString()
          });
        }
        else
          this.mEquipArtifactUnlockTooltip.TooltipText.text = LocalizedText.Get("sys.TOOLTIP_ARIFACT_UNLOCK");
      }
    }

    private void OnArtifactClick(GenericSlot slot, bool interactable)
    {
      if (!interactable)
      {
        this.ShowLockEquipArtifactTooltip(slot);
      }
      else
      {
        if (string.IsNullOrEmpty(this.PREFAB_PATH_ARTIFACT_WINDOW))
          return;
        this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
        bool flag = false;
        if ((UnityEngine.Object) this.mArtifactSelector == (UnityEngine.Object) null)
        {
          this.mArtifactSelector = UnityEngine.Object.Instantiate<GameObject>(AssetManager.Load<GameObject>(this.PREFAB_PATH_ARTIFACT_WINDOW));
        }
        else
        {
          this.mArtifactSelector.SetActive(true);
          flag = true;
        }
        ArtifactWindow component = this.mArtifactSelector.GetComponent<ArtifactWindow>();
        if ((UnityEngine.Object) component == (UnityEngine.Object) null)
          return;
        if ((UnityEngine.Object) slot != (UnityEngine.Object) null)
        {
          if ((UnityEngine.Object) slot == (UnityEngine.Object) this.mEquipmentPanel.ArtifactSlot)
            component.SelectArtifactSlot = ArtifactTypes.Arms;
          else if ((UnityEngine.Object) slot == (UnityEngine.Object) this.mEquipmentPanel.ArtifactSlot2)
            component.SelectArtifactSlot = ArtifactTypes.Armor;
          else if ((UnityEngine.Object) slot == (UnityEngine.Object) this.mEquipmentPanel.ArtifactSlot3)
            component.SelectArtifactSlot = ArtifactTypes.Accessory;
        }
        component.OnEquip = new ArtifactWindow.EquipEvent(this.OnArtifactSelect);
        component.OnDestroyCallBack = new ArtifactWindow.EquipEvent(this.OnArtifactDestroy);
        component.SetOwnerUnit(this.mCurrentUnit, this.mSelectedJobIndex);
        if (!((UnityEngine.Object) component.ArtifactList != (UnityEngine.Object) null))
          return;
        if (component.ArtifactList.TestOwner != this.mCurrentUnit)
        {
          component.ArtifactList.TestOwner = this.mCurrentUnit;
          flag = true;
        }
        ArtifactData dataOfClass = DataSource.FindDataOfClass<ArtifactData>(slot.gameObject, (ArtifactData) null);
        long iid = 0;
        if (this.mCurrentUnit.CurrentJob.Artifacts != null)
        {
          long num = (long) (dataOfClass == null ? (OLong) 0L : dataOfClass.UniqueID);
          for (int index = 0; index < this.mCurrentUnit.CurrentJob.Artifacts.Length; ++index)
          {
            if (this.mCurrentUnit.CurrentJob.Artifacts[index] != 0L && this.mCurrentUnit.CurrentJob.Artifacts[index] == num)
            {
              iid = this.mCurrentUnit.CurrentJob.Artifacts[index];
              break;
            }
          }
        }
        ArtifactData artifactData = (ArtifactData) null;
        if (iid != 0L)
          artifactData = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(iid);
        if (artifactData != null)
          component.ArtifactList.SetSelection((object[]) new ArtifactData[1]
          {
            artifactData
          }, true, true);
        else
          component.ArtifactList.SetSelection((object[]) new ArtifactData[0], true, true);
        ArtifactTypes select_slot_artifact_type = dataOfClass == null ? ArtifactTypes.None : dataOfClass.ArtifactParam.type;
        component.ArtifactList.FiltersPriority = this.SetEquipArtifactFilters(dataOfClass, select_slot_artifact_type);
        if (flag)
          component.ArtifactList.Refresh();
        this.RefreshEquipments(-1);
      }
    }

    private string[] SetEquipArtifactFilters(ArtifactData data, ArtifactTypes select_slot_artifact_type = ArtifactTypes.None)
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < RarityParam.MAX_RARITY; ++index)
        stringList.Add("RARE:" + (object) index);
      JobData currentJob = this.mCurrentUnit.CurrentJob;
      ArtifactData[] artifactDatas = currentJob.ArtifactDatas;
      if (artifactDatas != null)
      {
        for (int index = 0; index < artifactDatas.Length; ++index)
        {
          if (artifactDatas[index] != null && (data == null || (long) artifactDatas[index].UniqueID != (long) data.UniqueID))
            stringList.Add("SAME:" + artifactDatas[index].ArtifactParam.iname);
        }
      }
      for (int index = 0; index < currentJob.ArtifactDatas.Length; ++index)
      {
        ArtifactData artifactData = currentJob.ArtifactDatas[index];
        if (artifactData == null || artifactData.ArtifactParam.type == ArtifactTypes.Accessory)
          stringList.Add("TYPE:" + (object) (index + 1));
        else if (artifactData.ArtifactParam.type == select_slot_artifact_type)
          stringList.Add("TYPE:" + (object) (index + 1));
      }
      return stringList.ToArray();
    }

    public int GetUnlockAritfactSlot()
    {
      int a = 0;
      int awakeLv = this.mCurrentUnit.AwakeLv;
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      if (fixParam != null && fixParam.EquipArtifactSlotUnlock != null && fixParam.EquipArtifactSlotUnlock.Length > 0)
      {
        for (int index = 0; index < fixParam.EquipArtifactSlotUnlock.Length; ++index)
        {
          if (awakeLv >= (int) fixParam.EquipArtifactSlotUnlock[index])
            ++a;
        }
      }
      return Mathf.Max(a, 1);
    }

    private void OnArtifactDestroy(ArtifactData artifact, ArtifactTypes type = ArtifactTypes.None)
    {
      this.UpdateArtifactSetResult();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 220);
    }

    private void OnArtifactSelect(ArtifactData artifact, ArtifactTypes type = ArtifactTypes.None)
    {
      this.BeginStatusChangeCheck();
      if (artifact != null)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        UnitData unit = (UnitData) null;
        JobData job = (JobData) null;
        if (player.FindOwner(artifact, out unit, out job))
        {
          for (int slot = 0; slot < job.Artifacts.Length; ++slot)
          {
            if (job.Artifacts[slot] == (long) artifact.UniqueID)
            {
              job.SetEquipArtifact(slot, (ArtifactData) null, false);
              break;
            }
          }
        }
      }
      ArtifactData[] view_artifact_datas = this.GetViewArtifact();
      int artifactSlotIndex = JobData.GetArtifactSlotIndex(type);
      List<ArtifactData> artifactDataList1 = new List<ArtifactData>((IEnumerable<ArtifactData>) this.CurrentUnit.CurrentJob.ArtifactDatas);
      if (artifactDataList1.Count != view_artifact_datas.Length)
      {
        DebugUtility.LogError("画面上の武具データと実際の武具データがあっていません。");
      }
      else
      {
        for (int i = 0; i < view_artifact_datas.Length; ++i)
        {
          if (view_artifact_datas[i] != null && artifactDataList1.Find((Predicate<ArtifactData>) (x =>
          {
            if (x != null)
              return (long) x.UniqueID == (long) view_artifact_datas[i].UniqueID;
            return false;
          })) == null)
          {
            DebugUtility.LogError("画面上の武具データと実際の武具データがあっていません。");
            return;
          }
        }
        view_artifact_datas[artifactSlotIndex] = artifact;
        List<ArtifactData> artifactDataList2 = new List<ArtifactData>();
        for (int slot = 0; slot < this.mCurrentUnit.CurrentJob.ArtifactDatas.Length; ++slot)
          this.mCurrentUnit.CurrentJob.SetEquipArtifact(slot, (ArtifactData) null, false);
        for (int index = 0; index < view_artifact_datas.Length; ++index)
        {
          if (view_artifact_datas[index] != null)
          {
            if (view_artifact_datas[index].ArtifactParam.type == ArtifactTypes.Accessory)
              artifactDataList2.Add(view_artifact_datas[index]);
            else
              this.mCurrentUnit.CurrentJob.SetEquipArtifact(JobData.GetArtifactSlotIndex(view_artifact_datas[index].ArtifactParam.type), view_artifact_datas[index], false);
          }
        }
        for (int index = 0; index < artifactDataList2.Count; ++index)
        {
          for (int slot = 0; slot < this.mCurrentUnit.CurrentJob.ArtifactDatas.Length; ++slot)
          {
            if (this.mCurrentUnit.CurrentJob.ArtifactDatas[slot] == null)
            {
              this.mCurrentUnit.CurrentJob.SetEquipArtifact(slot, artifactDataList2[index], false);
              break;
            }
          }
        }
        this.mCurrentUnit.UpdateArtifact(this.mCurrentUnit.JobIndex, true, false);
        if (Network.Mode == Network.EConnectMode.Online)
          Network.RequestAPI((WebAPI) new ReqArtifactSet(this.mCurrentUnit.UniqueID, this.mCurrentUnit.CurrentJob.UniqueID, this.mCurrentUnit.CurrentJob.Artifacts, new Network.ResponseCallback(this.OnArtifactSetResult)), false);
        else
          this.ShowArtifactSetResult();
        this.SetUnitDirty();
      }
    }

    private ArtifactData[] GetViewArtifact()
    {
      return new List<ArtifactData>() { DataSource.FindDataOfClass<ArtifactData>(this.mEquipmentPanel.ArtifactSlot.gameObject, (ArtifactData) null), DataSource.FindDataOfClass<ArtifactData>(this.mEquipmentPanel.ArtifactSlot2.gameObject, (ArtifactData) null), DataSource.FindDataOfClass<ArtifactData>(this.mEquipmentPanel.ArtifactSlot3.gameObject, (ArtifactData) null) }.ToArray();
    }

    private void OnArtifactSetResult(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
            if (jsonObject.body.artifacts != null)
              MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.artifacts, true);
            MonoSingleton<GameManager>.Instance.Player.UpdateArtifactOwner();
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Retry();
            return;
          }
          Network.RemoveAPI();
          this.RefreshSortedUnits();
          this.RebuildUnitData();
          this.ShowArtifactSetResult();
        }
      }
    }

    private void ShowArtifactSetResult()
    {
      this.SpawnStatusChangeEffects();
      this.RebuildUnitData();
      this.ReloadPreviewModels();
      this.SetPreviewVisible(true);
      this.RefreshArtifactSlots();
      this.RefreshAbilitySlots(false);
      this.RefreshBasicParameters(false);
    }

    private void UpdateArtifactSetResult()
    {
      this.mCurrentUnit.UpdateArtifact(this.mCurrentUnit.JobIndex, true, false);
      this.RebuildUnitData();
      this.RefreshArtifactSlots();
      this.RefreshAbilitySlots(false);
      this.RefreshBasicParameters(false);
    }

    private void ShowSkinSetResult()
    {
      this.ReloadPreviewModels();
      this.SetPreviewVisible(true);
      this.SetUpUnitVoice();
    }

    private void OnJobRankUpClick(SRPG_Button button)
    {
      if (!button.IsInteractable())
        return;
      if (Array.FindIndex<EquipData>(this.mCurrentUnit.CurrentEquips, (Predicate<EquipData>) (eq => !eq.IsEquiped())) != -1)
      {
        this.OnEquipAll((UnityEngine.Object) button == (UnityEngine.Object) this.mEquipmentPanel.JobRankupAllIn);
      }
      else
      {
        if (this.mCurrentUnit.CurrentJob.GetJobRankCap(this.mCurrentUnit) <= this.mCurrentUnit.CurrentJob.Rank)
          return;
        if ((UnityEngine.Object) button == (UnityEngine.Object) this.mEquipmentPanel.JobRankupAllIn)
          this.OnEquipAll(true);
        else if (this.mCurrentUnit.JobIndex >= this.mCurrentUnit.NumJobsAvailable)
        {
          JobData baseJob = this.mCurrentUnit.GetBaseJob(this.mCurrentUnit.CurrentJob.JobID);
          if (Array.IndexOf<JobData>(this.mCurrentUnit.Jobs, baseJob) < 0)
            return;
          UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.CONFIRM_CLASSCHANGE"), baseJob == null ? (object) string.Empty : (object) baseJob.Name, (object) this.mCurrentUnit.CurrentJob.Name), (UIUtility.DialogResultEvent) (go => this.StartJobRankUp(-1, false, false)), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
        }
        else
          this.StartJobRankUp(-1, false, false);
      }
    }

    private void StartJobRankUp(int target_rank = -1, bool can_jobmaster = false, bool can_jobmax = false)
    {
      this.GetComponent<WindowController>().SetCollision(false);
      CriticalSection.Enter(CriticalSections.Network);
      this.SetCacheUnitData(this.mCurrentUnit, this.mSelectedJobIndex);
      this.StartCoroutine(this.PostJobRankUp(target_rank, can_jobmaster, can_jobmax));
    }

    private void OnJobRankUpResult(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoJobLvUpEquip:
            FlowNode_Network.Failed();
            break;
          case Network.EErrCode.EquipNotComp:
            FlowNode_Network.Failed();
            this.mJobRankUpRequestSent = true;
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        ArtifactParam selectedSkin1 = this.mCurrentUnit.GetSelectedSkin(-1);
        UnitData unitDataByUniqueId1 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
        EquipData[] rankupEquips = unitDataByUniqueId1.GetRankupEquips(this.mSelectedJobIndex);
        int num1 = 0;
        for (int index = 0; index < rankupEquips.Length; ++index)
        {
          if (rankupEquips[index].IsEquiped())
            ++num1;
        }
        JobData jobData1 = unitDataByUniqueId1.GetJobData(this.mSelectedJobIndex);
        if (jobData1 != null && jobData1.Rank > 1)
          num1 += (jobData1.Rank - 1) * 6;
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
          this.mLastSyncTime = Time.realtimeSinceStartup;
          Network.RemoveAPI();
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        this.SetNotifyReleaseClassChangeJob();
        this.UpdateTrophy_OnJobLevelChange();
        UnitData unitDataByUniqueId2 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
        int num2 = 0;
        foreach (EquipData rankupEquip in unitDataByUniqueId2.GetRankupEquips(this.mCurrentUnit.JobIndex))
        {
          if (rankupEquip.IsEquiped())
            ++num2;
        }
        JobData jobData2 = unitDataByUniqueId2.GetJobData(this.mCurrentUnit.JobIndex);
        if (jobData2 != null)
        {
          if (jobData2.Rank == 1)
            num2 = 6;
          if (jobData2.Rank > 1)
            num2 += (jobData2.Rank - 1) * 6;
        }
        MonoSingleton<GameManager>.Instance.Player.OnSoubiSet(this.mCurrentUnit.UnitID, num2 - num1);
        this.RefreshSortedUnits();
        GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) null);
        GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_COIN_STATUS.ToString(), (object) null);
        this.mJobRankUpRequestSent = true;
        UnitData unitDataByUniqueId3 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
        if (unitDataByUniqueId3 == null)
          return;
        ArtifactParam selectedSkin2 = unitDataByUniqueId3.GetSelectedSkin(-1);
        if (!((selectedSkin1 == null ? (string) null : selectedSkin1.iname) != (selectedSkin2 == null ? (string) null : selectedSkin2.iname)))
          return;
        this.ShowSkinParamChange(selectedSkin2, true);
      }
    }

    private bool CheckTargetRankAllEquip(int rank)
    {
      bool flag = false;
      string[] rankupItems = this.mCurrentUnit.Jobs[this.mSelectedJobIndex].GetRankupItems(rank);
      EquipData[] equips = new EquipData[6];
      if (rankupItems != null && rankupItems.Length > 0)
      {
        for (int index = 0; index < equips.Length; ++index)
        {
          equips[index] = new EquipData();
          equips[index].Setup(rankupItems[index]);
        }
        flag = MonoSingleton<GameManager>.Instance.Player.CheckEnableCreateEquipItemAll(this.mCurrentUnit, equips, (NeedEquipItemList) null);
      }
      return flag;
    }

    [DebuggerHidden]
    private IEnumerator PostJobRankUp(int target_rank = -1, bool can_jobmaster = false, bool can_jobmax = false)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPostJobRankUp\u003Ec__Iterator7() { target_rank = target_rank, can_jobmaster = can_jobmaster, can_jobmax = can_jobmax, \u0024this = this };
    }

    private void RebuildUnitData()
    {
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnitID);
      this.mSelectedJobIndex = unitDataByUniqueId.JobIndex;
      this.mLastSyncTime = unitDataByUniqueId.LastSyncTime;
      string str = this.mCurrentUnit == null ? (string) null : this.mCurrentUnit.CurrentJob.JobID;
      this.mCurrentUnit = new UnitData();
      this.mCurrentUnit.Setup(unitDataByUniqueId);
      if ((UnityEngine.Object) this.mEquipmentPanel.mEquipConceptCardIcon != (UnityEngine.Object) null)
        this.mEquipmentPanel.mEquipConceptCardIcon.Setup(this.mCurrentUnit.ConceptCard);
      this.StartCoroutine(this.LoadAllUnitImage());
      this.mIsSetJobUniqueID = this.mCurrentUnit.Jobs[this.mCurrentUnit.JobIndex].UniqueID;
      if (string.IsNullOrEmpty(str))
        return;
      for (int jobNo = 0; jobNo < this.mCurrentUnit.Jobs.Length; ++jobNo)
      {
        if (this.mCurrentUnit.Jobs[jobNo].JobID == str)
        {
          this.mCurrentUnit.SetJobIndex(jobNo);
          this.mSelectedJobIndex = jobNo;
          GlobalVars.SelectedJobUniqueID.Set(this.mCurrentUnit.Jobs[jobNo].UniqueID);
          break;
        }
      }
    }

    public void Refresh(long uniqueID, long jobUniqueID = 0, bool immediate = false, bool is_job_icon_hide = true)
    {
      if (this.mReloading)
        return;
      long mCurrentUnitId = this.mCurrentUnitID;
      this.mCurrentUnitID = uniqueID;
      this.mCurrentUnit = (UnitData) null;
      if (this.mStartSelectUnitUniqueID < 0L)
        this.mStartSelectUnitUniqueID = uniqueID;
      this.RefreshSortedUnits();
      GlobalVars.SelectedUnitUniqueID.Set(uniqueID);
      GlobalVars.PreBattleUnitUniqueID.Set(uniqueID);
      this.RebuildUnitData();
      this.RefreshUnitShiftButton();
      if (mCurrentUnitId != uniqueID)
        this.SetUpUnitVoice();
      this.mSelectedJobIndex = this.mCurrentUnit.JobIndex;
      this.CharaQuestButton.gameObject.SetActive(this.mCurrentUnit.IsOpenCharacterQuest());
      this.SkinButton.gameObject.SetActive(this.mCurrentUnit.IsSkinUnlocked());
      this.mOriginalAbilities = (long[]) this.mCurrentUnit.CurrentJob.AbilitySlots.Clone();
      this.mCurrentJobUniqueID = this.mCurrentUnit.CurrentJob.UniqueID;
      GlobalVars.SelectedJobUniqueID.Set(this.mCurrentJobUniqueID);
      if ((UnityEngine.Object) this.Favorite != (UnityEngine.Object) null)
        this.Favorite.isOn = this.mCurrentUnit.IsFavorite;
      this.CheckPlayBackUnlock();
      this.mReloading = true;
      this.StartCoroutine(this.RefreshAsync(immediate, is_job_icon_hide));
    }

    [DebuggerHidden]
    private IEnumerator RefreshAsync(bool immediate, bool is_job_icon_hide)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CRefreshAsync\u003Ec__Iterator8() { immediate = immediate, is_job_icon_hide = is_job_icon_hide, \u0024this = this };
    }

    public void RefreshReturningJobState(long jobUniqueID = 0)
    {
      if (jobUniqueID != 0L && this.mIsSetJobUniqueID != 0L)
      {
        for (int index = 0; index < this.mCurrentUnit.Jobs.Length; ++index)
        {
          if (this.mCurrentUnit.Jobs[index].UniqueID == jobUniqueID && this.mIsSetJobUniqueID != jobUniqueID)
          {
            this.ChangeJobSlot(index, (GameObject) null);
            this.RefreshJobIcons(false);
          }
        }
      }
      this.mSelectedJobIndex = this.mCurrentUnit.JobIndex;
    }

    private void SetPreviewVisible(bool visible)
    {
      if (!((UnityEngine.Object) this.mCurrentPreview != (UnityEngine.Object) null))
        return;
      this.mDesiredPreviewVisibility = visible;
      if (!visible)
      {
        GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerHidden, true);
        this.mCurrentPreview.gameObject.SetActive(false);
      }
      else
        this.mUpdatePreviewVisibility = true;
      if (!((UnityEngine.Object) UnitManagementWindow.Instance.UnitModelPreviewBase != (UnityEngine.Object) null) || UnitManagementWindow.Instance.UnitModelPreviewBase.gameObject.activeSelf || !visible)
        return;
      UnitManagementWindow.Instance.UnitModelPreviewBase.gameObject.SetActive(true);
    }

    private void UpdateJobRankUpButtonState()
    {
      if (this.mCurrentUnit == null)
        return;
      bool flag1 = this.mCurrentUnit.CurrentJob.Rank == 0;
      bool flag2 = !flag1 ? this.mCurrentUnit.CheckJobRankUpAllEquip(this.mCurrentUnit.JobIndex, true) : this.mCurrentUnit.CheckJobUnlock(this.mCurrentUnit.JobIndex, true);
      bool flag3 = this.mCurrentUnit.CurrentJob.GetJobRankCap(this.mCurrentUnit) <= this.mCurrentUnit.CurrentJob.Rank;
      if ((UnityEngine.Object) this.mEquipmentPanel.JobRankUpButton != (UnityEngine.Object) null)
      {
        this.mEquipmentPanel.JobRankUpButton.gameObject.SetActive(!flag1 && !flag3);
        this.mEquipmentPanel.JobRankUpButton.interactable = flag2;
        this.mEquipmentPanel.JobRankUpButton.UpdateButtonState();
        if (!string.IsNullOrEmpty(this.JobRankUpButtonHilitBool))
        {
          Animator component = this.mEquipmentPanel.JobRankUpButton.GetComponent<Animator>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          {
            component.SetBool(this.JobRankUpButtonHilitBool, flag2);
            component.Update(0.0f);
          }
        }
      }
      if ((UnityEngine.Object) this.mEquipmentPanel.JobUnlockButton != (UnityEngine.Object) null)
      {
        this.mEquipmentPanel.JobUnlockButton.gameObject.SetActive(flag1 && !flag3);
        this.mEquipmentPanel.JobUnlockButton.interactable = flag2;
        this.mEquipmentPanel.JobUnlockButton.UpdateButtonState();
        if (!string.IsNullOrEmpty(this.JobRankUpButtonHilitBool))
        {
          Animator component = this.mEquipmentPanel.JobUnlockButton.GetComponent<Animator>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          {
            component.SetBool(this.JobRankUpButtonHilitBool, flag2);
            component.Update(0.0f);
          }
        }
      }
      this.mIsJobLvMaxAllEquip = false;
      bool flag4 = false;
      if ((UnityEngine.Object) this.mEquipmentPanel.AllEquipButton != (UnityEngine.Object) null)
      {
        bool flag5 = -1 == Array.FindIndex<EquipData>(this.mCurrentUnit.CurrentEquips, (Predicate<EquipData>) (eq => !eq.IsEquiped()));
        NeedEquipItemList item_list = new NeedEquipItemList();
        bool equipItemAll = MonoSingleton<GameManager>.Instance.Player.CheckEnableCreateEquipItemAll(this.mCurrentUnit, this.mCurrentUnit.CurrentEquips, item_list);
        if (!flag3 || flag5)
        {
          this.mEquipmentPanel.AllEquipButton.gameObject.SetActive(false);
        }
        else
        {
          this.mIsJobLvMaxAllEquip = true;
          flag4 = true;
          this.mEquipmentPanel.AllEquipButton.gameObject.SetActive(true);
          this.mEquipmentPanel.AllEquipButton.interactable = equipItemAll || item_list.IsEnoughCommon();
        }
        this.mEquipmentPanel.AllEquipButton.UpdateButtonState();
        if (!string.IsNullOrEmpty(this.AllEquipButtonHilitBool))
        {
          Animator component = this.mEquipmentPanel.AllEquipButton.GetComponent<Animator>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          {
            component.SetBool(this.AllEquipButtonHilitBool, flag2);
            component.Update(0.0f);
          }
        }
      }
      if ((UnityEngine.Object) this.mEquipmentPanel.JobRankupAllIn != (UnityEngine.Object) null)
      {
        this.mEquipmentPanel.JobRankupAllIn.gameObject.SetActive(!flag1 && !flag3);
        this.mEquipmentPanel.JobRankupAllIn.interactable = this.mCurrentUnit.CheckJobRankUpAllEquip(this.mCurrentUnit.JobIndex, false);
        this.mEquipmentPanel.JobRankupAllIn.UpdateButtonState();
      }
      if ((UnityEngine.Object) this.mEquipmentPanel.JobRankCapCaution != (UnityEngine.Object) null)
        this.mEquipmentPanel.JobRankCapCaution.SetActive(flag3 && !flag4);
      Canvas component1 = this.mEquipmentPanel.GetComponent<Canvas>();
      if (!((UnityEngine.Object) component1 != (UnityEngine.Object) null) || !component1.enabled)
        return;
      component1.enabled = false;
      component1.enabled = true;
    }

    private void ReloadPreviewModels()
    {
      if (this.mCurrentUnit == null || (UnityEngine.Object) UnitManagementWindow.Instance.UnitModelPreviewParent == (UnityEngine.Object) null)
        return;
      GameUtility.DestroyGameObjects<UnitPreview>(this.mPreviewControllers);
      this.mPreviewControllers.Clear();
      this.mPreviewJobNames.Clear();
      this.mCurrentPreview = (UnitPreview) null;
      for (int jobIndex = 0; jobIndex < this.mCurrentUnit.Jobs.Length; ++jobIndex)
      {
        UnitPreview unitPreview = (UnitPreview) null;
        if (this.mCurrentUnit.Jobs[jobIndex] != null && this.mCurrentUnit.Jobs[jobIndex].Param != null)
        {
          GameObject gameObject = new GameObject("Preview", new System.Type[1]{ typeof (UnitPreview) });
          gameObject.SetActive(false);
          unitPreview = gameObject.GetComponent<UnitPreview>();
          unitPreview.DefaultLayer = GameUtility.LayerCH;
          unitPreview.SetupUnit(this.mCurrentUnit, jobIndex);
          gameObject.transform.SetParent(UnitManagementWindow.Instance.UnitModelPreviewParent, false);
          if (jobIndex == this.mCurrentUnit.JobIndex)
          {
            this.mCurrentPreview = unitPreview;
            unitPreview.gameObject.SetActive(true);
          }
        }
        this.mPreviewControllers.Add(unitPreview);
        this.mPreviewJobNames.Add(this.mCurrentUnit.Jobs[jobIndex].JobID);
      }
      if (!((UnityEngine.Object) this.mCurrentPreview == (UnityEngine.Object) null))
        return;
      GameObject gameObject1 = new GameObject("Preview", new System.Type[1]{ typeof (UnitPreview) });
      this.mCurrentPreview = gameObject1.GetComponent<UnitPreview>();
      this.mCurrentPreview.DefaultLayer = GameUtility.LayerCH;
      this.mCurrentPreview.SetupUnit(this.mCurrentUnit, -1);
      gameObject1.transform.SetParent(UnitManagementWindow.Instance.UnitModelPreviewParent, false);
      this.mPreviewControllers.Add(this.mCurrentPreview);
      this.mCurrentPreview.gameObject.SetActive(true);
    }

    [DebuggerHidden]
    private IEnumerator LoadAllUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CLoadAllUnitImage\u003Ec__Iterator9() { \u0024this = this };
    }

    [DebuggerHidden]
    private IEnumerator RefreshUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CRefreshUnitImage\u003Ec__IteratorA() { \u0024this = this };
    }

    private void RefreshJobInfo()
    {
      if ((UnityEngine.Object) this.JobInfo == (UnityEngine.Object) null)
        return;
      DataSource.Bind<UnitData>(this.JobInfo, this.mCurrentUnit, false);
      GameParameter.UpdateAll(this.JobInfo);
    }

    private int ClampJobIconIndex(int index)
    {
      if (index >= 0)
      {
        index %= this.mJobSetDatas.Count;
      }
      else
      {
        index = Mathf.Abs(index);
        index %= this.mJobSetDatas.Count;
        index = this.mJobSetDatas.Count - index;
        index %= this.mJobSetDatas.Count;
      }
      return index;
    }

    public void RefreshJobIcon(GameObject target, int job_index)
    {
      if (this.mCurrentUnit == null)
        return;
      int index = this.ClampJobIconIndex(job_index);
      bool is_avtive_job = Array.FindIndex<JobData>(this.mCurrentUnit.Jobs, (Predicate<JobData>) (job => job.UniqueID == this.mCurrentJobUniqueID)) == index;
      target.GetComponent<UnitInventoryJobIcon>().SetParam(this.mCurrentUnit, this.mJobSetDatas[index][0], this.mJobSetDatas[index][1], is_avtive_job, UnitInventoryJobIcon.eViewMode.UNIT_DETAIL);
      this.UpdateJobSlotStates(true);
    }

    private void RefreshJobIcons(bool is_hide = false)
    {
      this.mJobIconScrollListController.m_ScrollMode = JobIconScrollListController.Mode.Reverse;
      if (this.mCurrentUnit == null)
        return;
      for (int index = 0; index < this.mJobIconScrollListController.Items.Count; ++index)
        this.mJobIconScrollListController.Items[index].job_icon.ResetParam();
      JobSetParam[] cc_jobset_array = MonoSingleton<GameManager>.Instance.GetClassChangeJobSetParam(this.mCurrentUnit.UnitID);
      if (cc_jobset_array == null)
        cc_jobset_array = new JobSetParam[0];
      this.mJobSetDatas.Clear();
      int key = 0;
      for (int i = 0; i < this.mCurrentUnit.Jobs.Length; ++i)
      {
        int index = Array.FindIndex<JobSetParam>(cc_jobset_array, (Predicate<JobSetParam>) (jobset => jobset.job == this.mCurrentUnit.Jobs[i].JobID));
        if (index >= 0)
        {
          JobSetParam base_jobset_param = MonoSingleton<GameManager>.Instance.GetJobSetParam(cc_jobset_array[index].jobchange);
          if (Array.FindIndex<JobData>(this.mCurrentUnit.Jobs, (Predicate<JobData>) (job => job.JobID == base_jobset_param.job)) < 0)
          {
            int[] numArray = new int[2]{ i, -1 };
            this.mJobSetDatas.Add(key, numArray);
            ++key;
          }
        }
        else
        {
          int num = -1;
          for (int j = 0; j < cc_jobset_array.Length; ++j)
          {
            if (MonoSingleton<GameManager>.Instance.GetJobSetParam(cc_jobset_array[j].jobchange).job == this.mCurrentUnit.Jobs[i].JobID)
            {
              num = Array.FindIndex<JobData>(this.mCurrentUnit.Jobs, (Predicate<JobData>) (job => job.JobID == cc_jobset_array[j].job));
              break;
            }
          }
          int[] numArray = new int[2]{ i, num };
          this.mJobSetDatas.Add(key, numArray);
          ++key;
        }
      }
      for (int index = 0; index < this.mJobIconScrollListController.Items.Count; ++index)
        this.mJobIconScrollListController.Items[index].job_icon.gameObject.SetActive(false);
      if (this.mJobSetDatas.Count <= 2)
      {
        for (int job_index = 0; job_index < this.mJobSetDatas.Count; ++job_index)
        {
          this.mJobIconScrollListController.Items[job_index].job_icon.gameObject.SetActive(true);
          this.RefreshJobIcon(this.mJobIconScrollListController.Items[job_index].job_icon.gameObject, job_index);
        }
      }
      else
      {
        for (int index = 0; index < this.mJobIconScrollListController.Items.Count; ++index)
        {
          this.mJobIconScrollListController.Items[index].job_icon.gameObject.SetActive(true);
          int job_index = int.Parse(this.mJobIconScrollListController.Items[index].job_icon.name);
          this.RefreshJobIcon(this.mJobIconScrollListController.Items[index].job_icon.gameObject, job_index);
        }
      }
      this.mJobIconScrollListController.Repotision();
      List<GameObject> objects = new List<GameObject>();
      for (int index = 0; index < this.mJobIconScrollListController.Items.Count; ++index)
      {
        if (this.mJobIconScrollListController.Items[index].job_icon.BaseJobIconButton.name == this.mSelectedJobIndex.ToString())
          objects.Add(this.mJobIconScrollListController.Items[index].job_icon.gameObject);
        if (this.mJobIconScrollListController.Items[index].job_icon.CcJobButton.gameObject.activeSelf && this.mJobIconScrollListController.Items[index].job_icon.CcJobButton.name == this.mSelectedJobIndex.ToString())
          objects.Add(this.mJobIconScrollListController.Items[index].job_icon.gameObject);
      }
      this.ScrollClampedJobIcons.Focus(objects, true, is_hide, 0.0f);
      this.mJobIconScrollListController.Step();
      this.mJobIconScrollListController.m_ScrollMode = this.mJobSetDatas.Count > 2 ? JobIconScrollListController.Mode.Reverse : JobIconScrollListController.Mode.Normal;
      this.UpdateJobSlotStates(true);
    }

    private void SetActivePreview(string jobID)
    {
      int index = this.mPreviewJobNames.IndexOf(jobID);
      if (index < 0)
        index = 0;
      this.SetActivePreview(index);
    }

    private void SetActivePreview(int index)
    {
      UnitPreview previewController = this.mPreviewControllers[index];
      if ((UnityEngine.Object) previewController == (UnityEngine.Object) this.mCurrentPreview)
        return;
      GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerHidden, true);
      this.mCurrentPreview.gameObject.SetActive(false);
      previewController.gameObject.SetActive(true);
      GameUtility.SetLayer((Component) previewController, GameUtility.LayerCH, true);
      this.mCurrentPreview = previewController;
    }

    private void UpdateJobSlotStates(bool immediate)
    {
      UnitInventoryJobIcon[] componentsInChildren = this.ScrollClampedJobIcons.GetComponentsInChildren<UnitInventoryJobIcon>();
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        bool is_on = componentsInChildren[index].BaseJobIconButton.name == this.mCurrentUnit.JobIndex.ToString();
        componentsInChildren[index].SetSelectIconAnim(is_on);
      }
    }

    private void UpdateJobChangeButtonState()
    {
      if (!((UnityEngine.Object) this.JobChangeButton != (UnityEngine.Object) null))
        return;
      this.JobChangeButton.IsOn = this.mCurrentUnit.CurrentJob.UniqueID == this.mCurrentJobUniqueID;
      this.JobChangeButton.UpdateButtonState();
      if (this.JobChangeButton.transform.childCount <= 0)
        return;
      for (int index = 0; index < this.JobChangeButton.transform.childCount; ++index)
      {
        Transform child = this.JobChangeButton.transform.GetChild(index);
        if ((UnityEngine.Object) child != (UnityEngine.Object) null)
          child.gameObject.SetActive(!this.JobChangeButton.IsOn);
      }
    }

    private void UpdateUnitKakuseiButtonState()
    {
      if (!((UnityEngine.Object) this.UnitKakuseiButton != (UnityEngine.Object) null))
        return;
      this.UnitKakuseiButton.interactable = this.mCurrentUnit.AwakeLv < this.mCurrentUnit.GetAwakeLevelCap();
      if (string.IsNullOrEmpty(this.UnitKakuseiButtonHilitBool))
        return;
      Animator component = this.UnitKakuseiButton.GetComponent<Animator>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.SetBool(this.UnitKakuseiButtonHilitBool, this.mCurrentUnit.CheckUnitAwaking());
      component.Update(0.0f);
    }

    private void UpdateUnitEvolutionButtonState(bool immediate = false)
    {
      if (!((UnityEngine.Object) this.UnitEvolutionButton != (UnityEngine.Object) null))
        return;
      this.UnitEvolutionButton.gameObject.SetActive(true);
      this.UnitEvolutionButton.interactable = this.mCurrentUnit.Rarity < this.mCurrentUnit.GetRarityCap();
      if (immediate)
        this.UnitEvolutionButton.UpdateButtonState();
      if (string.IsNullOrEmpty(this.UnitEvolutionButtonHilitBool))
        return;
      Animator component = this.UnitEvolutionButton.GetComponent<Animator>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.SetBool(this.UnitEvolutionButtonHilitBool, this.mCurrentUnit.CheckUnitRarityUp());
      component.Update(0.0f);
    }

    private void UpdateUnitTobiraButtonState()
    {
      this.UnitUnlockTobiraButton.gameObject.SetActive(false);
      this.UnitGoTobiraButton.gameObject.SetActive(false);
      if (!this.mCurrentUnit.CanUnlockTobira())
        return;
      bool flag1 = this.mCurrentUnit.Rarity >= this.mCurrentUnit.GetRarityCap();
      List<UnitData.TobiraConditioError> errors = (List<UnitData.TobiraConditioError>) null;
      bool flag2 = this.mCurrentUnit.CanUnlockTobira() && this.mCurrentUnit.MeetsTobiraConditions(TobiraParam.Category.START, out errors);
      if (this.mCurrentUnit.IsUnlockTobira)
      {
        if ((UnityEngine.Object) this.UnitGoTobiraButton != (UnityEngine.Object) null)
          this.UnitGoTobiraButton.gameObject.SetActive(true);
        this.UnitEvolutionButton.gameObject.SetActive(false);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 500);
      }
      else
      {
        if (!flag1)
          return;
        this.UnitEvolutionButton.gameObject.SetActive(false);
        this.UnitUnlockTobiraButton.gameObject.SetActive(true);
        if (string.IsNullOrEmpty(this.UnitTobiraButtonHilitBool))
          return;
        Animator component = this.UnitUnlockTobiraButton.GetComponent<Animator>();
        if ((UnityEngine.Object) component == (UnityEngine.Object) null || !flag2)
          return;
        List<ConditionsResult> conditionsResultList = TobiraUtility.TobiraRecipeCheck(this.mCurrentUnit, TobiraParam.Category.START, 0);
        bool flag3 = true;
        for (int index = 0; index < conditionsResultList.Count; ++index)
        {
          if (!conditionsResultList[index].isClear)
          {
            flag3 = false;
            break;
          }
        }
        component.SetBool(this.UnitTobiraButtonHilitBool, flag3);
        component.Update(0.0f);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 500);
      }
    }

    private void RefreshBasicParameters(bool bDisableJobMaster = false)
    {
      DataSource.Bind<UnitData>(this.UnitInfo, this.mCurrentUnit, false);
      DataSource.Bind<UnitData>(this.UnitParamInfo, this.mCurrentUnit, false);
      int jobNo = -1;
      for (int index = 0; index < this.mCurrentUnit.Jobs.Length; ++index)
      {
        if (this.mCurrentUnit.Jobs[index].UniqueID == this.mCurrentJobUniqueID)
        {
          jobNo = index;
          break;
        }
      }
      int num = Array.IndexOf<JobData>(this.mCurrentUnit.Jobs, this.mCurrentUnit.CurrentJob);
      if (jobNo >= 0 && num >= 0 && jobNo != num)
      {
        this.mCurrentUnit.SetJobIndex(jobNo);
        BaseStatus status1 = new BaseStatus(this.mCurrentUnit.Status);
        if (bDisableJobMaster)
        {
          this.mCurrentUnit.CalcStatus(this.mCurrentUnit.Lv, jobNo, ref status1, num);
          status1.CopyTo(this.mCurrentUnit.Status);
        }
        int combination1 = this.mCurrentUnit.GetCombination();
        this.mCurrentUnit.SetJobIndex(num);
        BaseStatus status2 = new BaseStatus(this.mCurrentUnit.Status);
        if (bDisableJobMaster)
        {
          this.mCurrentUnit.CalcStatus(this.mCurrentUnit.Lv, num, ref status2, num);
          status2.CopyTo(this.mCurrentUnit.Status);
        }
        int combination2 = this.mCurrentUnit.GetCombination();
        for (int index = 0; index < StatusParam.MAX_STATUS; ++index)
        {
          if ((UnityEngine.Object) this.mStatusParamSlots[index] != (UnityEngine.Object) null)
          {
            this.SetParamColor((Graphic) this.mStatusParamSlots[index], (int) status2.param[(StatusTypes) index] - (int) status1.param[(StatusTypes) index]);
            this.mStatusParamSlots[index].text = this.mCurrentUnit.Status.param[(StatusTypes) index].ToString();
          }
        }
        if ((UnityEngine.Object) this.Param_Renkei != (UnityEngine.Object) null)
        {
          this.SetParamColor((Graphic) this.Param_Renkei, combination2 - combination1);
          this.Param_Renkei.text = this.mCurrentUnit.GetCombination().ToString();
        }
      }
      else
      {
        for (int index = 0; index < StatusParam.MAX_STATUS; ++index)
        {
          if ((UnityEngine.Object) this.mStatusParamSlots[index] != (UnityEngine.Object) null)
          {
            this.SetParamColor((Graphic) this.mStatusParamSlots[index], 0);
            this.mStatusParamSlots[index].text = this.mCurrentUnit.Status.param[(StatusTypes) index].ToString();
          }
        }
        if ((UnityEngine.Object) this.Param_Renkei != (UnityEngine.Object) null)
        {
          this.SetParamColor((Graphic) this.Param_Renkei, 0);
          this.Param_Renkei.text = this.mCurrentUnit.GetCombination().ToString();
        }
      }
      GameParameter.UpdateAll(this.UnitInfo);
      GameParameter.UpdateAll(this.UnitParamInfo);
      if (!((UnityEngine.Object) this.LeaderSkillInfo != (UnityEngine.Object) null))
        return;
      this.LeaderSkillInfo.SetSlotData<SkillData>(this.mCurrentUnit.LeaderSkill);
    }

    private void SetParamColor(Graphic g, int delta)
    {
      if (delta < 0)
        g.color = (Color) this.ParamDownColor;
      else if (delta > 0)
        g.color = (Color) this.ParamUpColor;
      else
        g.color = (Color) this.DefaultParamColor;
    }

    private void RefreshEquipments(int slot = -1)
    {
      if (this.mActiveTabIndex != UnitEnhanceV3.eTabButtons.Equipments)
      {
        this.mEquipmentPanelDirty = true;
      }
      else
      {
        this.mEquipmentPanelDirty = false;
        if (this.mCurrentUnit == null)
          return;
        EquipData[] rankupEquips = this.mCurrentUnit.GetRankupEquips(this.mCurrentUnit.JobIndex);
        UnitEquipmentSlotEvents[] equipmentSlots = this.mEquipmentPanel.EquipmentSlots;
        GameManager instance = MonoSingleton<GameManager>.Instance;
        PlayerData player = instance.Player;
        int num1 = 0;
        int num2 = 0;
        for (int index = 0; index < equipmentSlots.Length; ++index)
        {
          if (!((UnityEngine.Object) equipmentSlots[index] == (UnityEngine.Object) null) && (slot == -1 || index == slot))
          {
            if (!rankupEquips[index].IsValid())
            {
              equipmentSlots[index].gameObject.SetActive(false);
            }
            else
            {
              equipmentSlots[index].gameObject.SetActive(true);
              ItemParam itemParam = rankupEquips[index].ItemParam;
              DataSource.Bind<ItemParam>(equipmentSlots[index].gameObject, itemParam, false);
              DataSource.Bind<EquipData>(equipmentSlots[index].gameObject, rankupEquips[index], false);
              GameParameter.UpdateAll(equipmentSlots[index].gameObject);
              bool flag1 = true;
              bool flag2 = true;
              if (this.mCurrentUnit.CurrentJob.Rank == 0)
              {
                ItemParam commonEquip = instance.MasterParam.GetCommonEquip(itemParam, true);
                ItemData itemData = commonEquip == null ? (ItemData) null : instance.Player.FindItemDataByItemID(commonEquip.iname);
                ItemData itemDataByItemId = player.FindItemDataByItemID(itemParam.iname);
                if (itemDataByItemId != null)
                  flag1 = num1 < itemDataByItemId.Num;
                if (itemData != null)
                  flag2 = num2 < itemData.Num;
              }
              UnitEquipmentSlotEvents.SlotStateTypes slotStateTypes;
              if (rankupEquips[index].IsEquiped())
                slotStateTypes = UnitEquipmentSlotEvents.SlotStateTypes.Equipped;
              else if (player.HasItem(itemParam.iname) && flag1)
              {
                ++num1;
                slotStateTypes = itemParam.equipLv <= this.mCurrentUnit.Lv ? UnitEquipmentSlotEvents.SlotStateTypes.HasEquipment : UnitEquipmentSlotEvents.SlotStateTypes.NeedMoreLevel;
              }
              else
              {
                NeedEquipItemList item_list = new NeedEquipItemList();
                if (player.CheckEnableCreateItem(itemParam, true, 1, item_list))
                  slotStateTypes = itemParam.equipLv <= this.mCurrentUnit.Lv ? UnitEquipmentSlotEvents.SlotStateTypes.EnableCraft : UnitEquipmentSlotEvents.SlotStateTypes.EnableCraftNeedMoreLevel;
                else if (item_list.IsEnoughCommon() && flag2)
                {
                  if (itemParam.equipLv > this.mCurrentUnit.Lv)
                    slotStateTypes = UnitEquipmentSlotEvents.SlotStateTypes.EnableCommonSoulNeedMoreLevel;
                  else if (this.mCurrentUnit.CurrentJob.Rank == 0)
                  {
                    slotStateTypes = UnitEquipmentSlotEvents.SlotStateTypes.EnableCommonSoul;
                    ++num2;
                  }
                  else
                    slotStateTypes = UnitEquipmentSlotEvents.SlotStateTypes.EnableCommon;
                }
                else
                  slotStateTypes = UnitEquipmentSlotEvents.SlotStateTypes.Empty;
              }
              equipmentSlots[index].StateType = slotStateTypes;
            }
          }
        }
        this.RefreshArtifactSlots();
      }
    }

    public bool CheckEquipArtifactSlot(int slot, JobData job, UnitData unit)
    {
      if (job == null || unit == null)
        return false;
      int awakeLv = unit.AwakeLv;
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      if (fixParam == null || fixParam.EquipArtifactSlotUnlock == null || (fixParam.EquipArtifactSlotUnlock.Length <= 0 || fixParam.EquipArtifactSlotUnlock.Length < slot))
        return false;
      return (int) fixParam.EquipArtifactSlotUnlock[slot] <= awakeLv;
    }

    public void RefreshArtifactSlots()
    {
      long[] numArray = new long[3];
      int index1 = 0;
      JobData currentJob = this.mCurrentUnit.CurrentJob;
      for (int index2 = 0; index2 < currentJob.Artifacts.Length; ++index2)
      {
        if (currentJob.Artifacts[index2] != 0L)
        {
          numArray[index1] = currentJob.Artifacts[index2];
          ++index1;
        }
      }
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      List<ArtifactData> artifacts_sort_list = new List<ArtifactData>();
      for (int index2 = 0; index2 < numArray.Length; ++index2)
      {
        if (numArray[index2] != 0L)
        {
          ArtifactData artifactByUniqueId = player.FindArtifactByUniqueID(numArray[index2]);
          if (artifactByUniqueId != null)
            artifacts_sort_list.Add(artifactByUniqueId);
        }
      }
      Dictionary<int, List<ArtifactData>> dictionary = new Dictionary<int, List<ArtifactData>>();
      for (int index2 = 0; index2 < artifacts_sort_list.Count; ++index2)
      {
        int type = (int) artifacts_sort_list[index2].ArtifactParam.type;
        if (!dictionary.ContainsKey(type))
          dictionary[type] = new List<ArtifactData>();
        dictionary[type].Add(artifacts_sort_list[index2]);
      }
      artifacts_sort_list.Clear();
      List<int> intList = new List<int>((IEnumerable<int>) dictionary.Keys);
      intList.Sort((Comparison<int>) ((x, y) => x - y));
      for (int index2 = 0; index2 < intList.Count; ++index2)
        artifacts_sort_list.AddRange((IEnumerable<ArtifactData>) dictionary[intList[index2]]);
      this.RefreshArtifactSlot(this.mEquipmentPanel.ArtifactSlot, artifacts_sort_list, 0);
      this.RefreshArtifactSlot(this.mEquipmentPanel.ArtifactSlot2, artifacts_sort_list, 1);
      this.RefreshArtifactSlot(this.mEquipmentPanel.ArtifactSlot3, artifacts_sort_list, 2);
    }

    public void RefreshArtifactSlot(GenericSlot slot, List<ArtifactData> artifacts_sort_list, int index)
    {
      JobData currentJob = this.mCurrentUnit.CurrentJob;
      if (!((UnityEngine.Object) this.mEquipmentPanel != (UnityEngine.Object) null) || !((UnityEngine.Object) slot != (UnityEngine.Object) null))
        return;
      if (this.mCurrentUnit.CurrentJob.Rank > 0)
      {
        ArtifactData data = artifacts_sort_list.Count <= index ? (ArtifactData) null : artifacts_sort_list[index];
        slot.SetLocked(!this.CheckEquipArtifactSlot(index, currentJob, this.mCurrentUnit));
        slot.SetSlotData<ArtifactData>(data);
      }
      else
      {
        slot.SetLocked(true);
        slot.SetSlotData<ArtifactData>((ArtifactData) null);
      }
    }

    public void OnJobSlotClick(GameObject target)
    {
      this.ChangeJobSlot(int.Parse(target.name), target);
    }

    private void ChangeJobSlot(int index, GameObject target = null)
    {
      if (this.mCurrentUnit == null || index < 0)
        return;
      if (this.mCurrentUnit.JobIndex != index)
      {
        if (!this.mCurrentUnit.CheckJobUnlockable(index))
        {
          if ((UnityEngine.Object) this.Prefab_LockedJobTooltip == (UnityEngine.Object) null)
            return;
          JobSetParam jobSetParam = this.mCurrentUnit.GetJobSetParam(index);
          if (jobSetParam == null)
            return;
          if ((UnityEngine.Object) this.mJobUnlockTooltip != (UnityEngine.Object) null)
          {
            this.mJobUnlockTooltip.Close();
            this.mJobUnlockTooltip = (Tooltip) null;
            if (this.mJobUnlockTooltipIndex == index)
              return;
          }
          if ((UnityEngine.Object) target != (UnityEngine.Object) null)
            Tooltip.SetTooltipPosition(target.transform as RectTransform, new Vector2(0.5f, 0.5f));
          Tooltip tooltip = UnityEngine.Object.Instantiate<Tooltip>(this.Prefab_LockedJobTooltip);
          DataSource.Bind<JobSetParam>(tooltip.gameObject, jobSetParam, false);
          JobParam jobParam = MonoSingleton<GameManager>.Instance.GetJobParam(jobSetParam.job);
          DataSource.Bind<JobParam>(tooltip.gameObject, jobParam, false);
          this.mJobUnlockTooltip = tooltip;
          this.mJobUnlockTooltipIndex = index;
          return;
        }
        this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
        this.mCurrentUnit.SetJobIndex(index);
        this.RefreshBasicParameters(false);
        this.mOriginalAbilities = (long[]) this.mCurrentUnit.CurrentJob.AbilitySlots.Clone();
        this.SetActivePreview(this.mCurrentUnit.CurrentJob.JobID);
        this.mSelectedJobIndex = index;
        this.RefreshJobInfo();
        this.RefreshEquipments(-1);
        this.UpdateJobChangeButtonState();
        this.UpdateJobRankUpButtonState();
        this.RefreshAbilitySlotButtonState();
        if (this.mCurrentUnit.CurrentJob.Rank == 0)
        {
          if (this.mActiveTabIndex == UnitEnhanceV3.eTabButtons.AbilitySlot)
            this.OnTabChange((SRPG_Button) this.Tab_Equipments);
        }
        else
          this.RefreshAbilitySlots(false);
        this.FadeUnitImage(0.0f, 0.0f, 0.0f);
        this.StartCoroutine(this.RefreshUnitImage());
        this.FadeUnitImage(0.0f, 1f, this.BGUnitImageFadeTime);
        GlobalVars.SelectedJobUniqueID.Set(this.mCurrentUnit.CurrentJob.UniqueID);
      }
      if ((UnityEngine.Object) target == (UnityEngine.Object) null)
        this.ScrollClampedJobIcons.Focus(this.mJobIconScrollListController.Items[index].job_icon.gameObject, false, false, 0.0f);
      else
        this.ScrollClampedJobIcons.Focus(target.transform.parent.gameObject, false, false, 0.0f);
      this.UpdateJobSlotStates(true);
    }

    private void RefreshAbilitySlotButtonState()
    {
      if (!((UnityEngine.Object) this.Tab_AbilitySlot != (UnityEngine.Object) null))
        return;
      this.Tab_AbilitySlot.interactable = this.mCurrentUnit.CurrentJob.Rank > 0;
    }

    private void Req_UpdateEquippedAbility()
    {
      bool flag = false;
      for (int index = 0; index < this.mCurrentUnit.CurrentJob.AbilitySlots.Length; ++index)
      {
        if (this.mCurrentUnit.CurrentJob.AbilitySlots[index] != this.mOriginalAbilities[index])
        {
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        this.FinishKyokaRequest();
      }
      else
      {
        long uniqueId = this.mCurrentUnit.CurrentJob.UniqueID;
        this.mOriginalAbilities = (long[]) this.mCurrentUnit.CurrentJob.AbilitySlots.Clone();
        this.RequestAPI((WebAPI) new ReqJobAbility(uniqueId, this.mCurrentUnit.CurrentJob.AbilitySlots, new Network.ResponseCallback(this.Res_UpdateEquippedAbility)));
        this.SetUnitDirty();
      }
    }

    private void Res_UpdateEquippedAbility(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoAbilitySetAbility:
          case Network.EErrCode.NoJobSetAbility:
          case Network.EErrCode.UnsetAbility:
            FlowNode_Network.Failed();
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          this.mLastSyncTime = Time.realtimeSinceStartup;
        }
        catch (Exception ex)
        {
          Debug.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        Network.RemoveAPI();
        this.FinishKyokaRequest();
      }
    }

    public UnitEquipmentWindow EquipmentWindow
    {
      set
      {
        this.mEquipmentWindow = value;
        if (!((UnityEngine.Object) this.mEquipmentWindow != (UnityEngine.Object) null))
          return;
        this.mEquipmentWindow.OnEquip = new UnitEquipmentWindow.EquipEvent(this.OnEquipNoCommon);
        this.mEquipmentWindow.OnCommonEquip = new UnitEquipmentWindow.EquipEvent(this.OnEquipCommon);
        this.mEquipmentWindow.OnReload = new UnitEquipmentWindow.EquipReloadEvent(this.UpdateJobRankUpButtonState);
      }
      get
      {
        if ((UnityEngine.Object) this.mEquipmentWindow == (UnityEngine.Object) null && (UnityEngine.Object) UnitManagementWindow.Instance != (UnityEngine.Object) null)
        {
          GameObject original = AssetManager.Load<GameObject>(UnitManagementWindow.Instance.PATH_UNIT_EQUIPMENT_WINDOW);
          if ((UnityEngine.Object) original != (UnityEngine.Object) null)
          {
            UnitEquipmentWindow component = UnityEngine.Object.Instantiate<GameObject>(original).GetComponent<UnitEquipmentWindow>();
            component.transform.SetParent(UnitManagementWindow.Instance.transform, false);
            this.EquipmentWindow = component;
          }
        }
        return this.mEquipmentWindow;
      }
    }

    public UnitKakeraWindow KakeraWindow
    {
      set
      {
        this.mKakeraWindow = value;
        if (!((UnityEngine.Object) this.mKakeraWindow != (UnityEngine.Object) null))
          return;
        this.mKakeraWindow.OnAwakeAccept = new UnitKakeraWindow.AwakeEvent(this.OnUnitAwake2);
      }
      get
      {
        if ((UnityEngine.Object) this.mKakeraWindow == (UnityEngine.Object) null && (UnityEngine.Object) UnitManagementWindow.Instance != (UnityEngine.Object) null)
        {
          GameObject original = AssetManager.Load<GameObject>(UnitManagementWindow.Instance.PATH_UNIT_KAKERA_WINDOW);
          if ((UnityEngine.Object) original != (UnityEngine.Object) null)
          {
            UnitKakeraWindow component = UnityEngine.Object.Instantiate<GameObject>(original).GetComponent<UnitKakeraWindow>();
            component.transform.SetParent(UnitManagementWindow.Instance.transform, false);
            this.KakeraWindow = component;
          }
        }
        return this.mKakeraWindow;
      }
    }

    public UnitTobiraInventory TobiraInventoryWindow
    {
      set
      {
        this.mTobiraInventoryWindow = value;
        if ((UnityEngine.Object) this.mTobiraInventoryWindow != (UnityEngine.Object) null)
          ;
      }
      get
      {
        if ((UnityEngine.Object) this.mTobiraInventoryWindow == (UnityEngine.Object) null && (UnityEngine.Object) UnitManagementWindow.Instance != (UnityEngine.Object) null)
        {
          GameObject original = AssetManager.Load<GameObject>(UnitManagementWindow.Instance.PATH_TOBIRA_INVENTORY_WINDOW);
          if ((UnityEngine.Object) original != (UnityEngine.Object) null)
          {
            UnitTobiraInventory component = UnityEngine.Object.Instantiate<GameObject>(original).GetComponent<UnitTobiraInventory>();
            component.transform.SetParent(UnitManagementWindow.Instance.transform, false);
            this.TobiraInventoryWindow = component;
          }
        }
        return this.mTobiraInventoryWindow;
      }
    }

    private void OnWindowStateChange(GameObject go, bool visible)
    {
      if (visible || !this.mCloseRequested)
        return;
      this.mCloseRequested = false;
      this.FadeUnitImage(0.0f, 0.0f, 0.0f);
      this.SetPreviewVisible(false);
      if ((UnityEngine.Object) UnitManagementWindow.Instance.UnitModelPreviewBase != (UnityEngine.Object) null)
        UnitManagementWindow.Instance.UnitModelPreviewBase.gameObject.SetActive(false);
      if (this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null))
        this.StartCoroutine(this.WaitForKyokaRequestAndInvokeUserClose());
      else
        this.InvokeUserClose();
    }

    private void InvokeUserClose()
    {
      if (this.mStartSelectUnitUniqueID > 0L)
      {
        long num = -1;
        if (this.mStartSelectUnitUniqueID != this.mCurrentUnitID)
        {
          num = this.mCurrentUnitID;
          this.SetUnitDirty();
        }
        FlowNode_Variable.Set("LAST_SELECTED_UNITID", num <= 0L ? string.Empty : num.ToString());
      }
      this.mStartSelectUnitUniqueID = -1L;
      if (this.OnUserClose == null)
        return;
      this.OnUserClose();
    }

    [DebuggerHidden]
    private IEnumerator WaitForKyokaRequestAndInvokeUserClose()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CWaitForKyokaRequestAndInvokeUserClose\u003Ec__IteratorB() { \u0024this = this };
    }

    private void RequestAPI(WebAPI api)
    {
      if (this.mAppPausing)
        Network.RequestAPIImmediate(api, true);
      else
        Network.RequestAPI(api, false);
    }

    private void RefreshSortedUnits()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (this.SortedUnits == null)
        this.SortedUnits = new List<UnitData>();
      else
        this.SortedUnits.Clear();
      UnitListRootWindow.Tab currentTab = UnitManagementWindow.Instance.GetCurrentTab();
      EElement element = UnitListRootWindow.GetElement(currentTab);
      for (int index = 0; index < this.m_UnitList.Count; ++index)
      {
        UnitData unitData = player.GetUnitData(this.m_UnitList[index]);
        if (unitData != null)
        {
          switch (currentTab)
          {
            case UnitListRootWindow.Tab.FAVORITE:
              if (unitData.IsFavorite)
              {
                this.SortedUnits.Add(unitData);
                continue;
              }
              break;
            case UnitListRootWindow.Tab.ALL:
              this.SortedUnits.Add(unitData);
              continue;
          }
          if (unitData.Element == element)
            this.SortedUnits.Add(unitData);
        }
      }
    }

    private void RefreshUnitShiftButton()
    {
      int count = this.SortedUnits.Count;
      if (this.mCurrentUnit != null && this.SortedUnits.Find((Predicate<UnitData>) (unit => unit.UniqueID == this.mCurrentUnit.UniqueID)) == null)
        ++count;
      bool flag = count >= 2;
      this.PrevUnitButton.interactable = flag;
      this.NextUnitButton.interactable = flag;
    }

    private void RefreshAbilityList()
    {
      if (this.mActiveTabIndex != UnitEnhanceV3.eTabButtons.AbilityList)
      {
        this.mAbilityListDirty = true;
      }
      else
      {
        this.mAbilityListDirty = false;
        if ((UnityEngine.Object) this.mAbilityListPanel == (UnityEngine.Object) null || (UnityEngine.Object) this.mAbilityListPanel.AbilityList == (UnityEngine.Object) null)
          return;
        this.mAbilityListPanel.AbilityList.Unit = this.mCurrentUnit;
        this.mAbilityListPanel.AbilityList.DisplayAll();
      }
    }

    private void RefreshAbilitySlots(bool resetScrollPos = false)
    {
      if (this.mActiveTabIndex != UnitEnhanceV3.eTabButtons.AbilitySlot)
      {
        this.mAbilitySlotDirty = true;
      }
      else
      {
        this.mAbilitySlotDirty = false;
        if ((UnityEngine.Object) this.mAbilitySlotPanel == (UnityEngine.Object) null || (UnityEngine.Object) this.mAbilitySlotPanel.AbilitySlots == (UnityEngine.Object) null)
          return;
        this.mAbilitySlotPanel.AbilitySlots.Unit = this.mCurrentUnit;
        this.mAbilitySlotPanel.AbilitySlots.DisplaySlots();
        if (!resetScrollPos)
          return;
        this.mAbilitySlotPanel.AbilitySlots.ResetScrollPos();
      }
    }

    private void OnAbilitySlotSelect(int slotIndex)
    {
      if ((UnityEngine.Object) this.mAbilityPicker == (UnityEngine.Object) null)
        return;
      this.ExecQueuedKyokaRequest(new UnitEnhanceV3.DeferredJob(this.Req_UpdateEquippedAbility));
      this.mAbilityPicker.UnitData = this.mCurrentUnit;
      this.mAbilityPicker.AbilitySlot = slotIndex;
      this.mAbilityPicker.Refresh();
      this.mAbilityPicker.GetComponent<WindowController>().Open();
    }

    private void OnSlotAbilitySelect(AbilityData ability, GameObject itemGO)
    {
      int abilitySlot = this.mAbilityPicker.AbilitySlot;
      this.mAbilityPicker.GetComponent<WindowController>().Close();
      this.BeginStatusChangeCheck();
      this.mCurrentUnit.SetEquipAbility(this.mCurrentUnit.JobIndex, abilitySlot, ability == null ? 0L : ability.UniqueID);
      MonoSingleton<GameManager>.Instance.Player.OnChangeAbilitySet(this.mCurrentUnit.UnitID);
      this.SpawnStatusChangeEffects();
      this.RefreshBasicParameters(false);
      this.RefreshAbilitySlots(false);
      this.QueueKyokaRequest(new UnitEnhanceV3.DeferredJob(this.Req_UpdateEquippedAbility), 0.0f);
    }

    private void OnRankUpButtonPressHold(AbilityData abilityData, GameObject gobj)
    {
      if (!((UnityEngine.Object) gobj != (UnityEngine.Object) null))
        return;
      UnitAbilityListItemEvents component = gobj.GetComponent<UnitAbilityListItemEvents>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      this.mCurrentAbilityRankUpHold = component.ItemTouchController;
    }

    private void OnAbilityRankUpSet(AbilityData abilityData, GameObject itemGO)
    {
      if (this.ExecQueuedKyokaRequest(new UnitEnhanceV3.DeferredJob(this.SubmitAbilityRankUpRequest)))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!instance.Player.CheckRankUpAbility(abilityData))
        return;
      this.mCurrentUnitUnlocks = this.mCurrentUnit.UnlockedSkillIds();
      if (!instance.Player.RankUpAbility(abilityData, false))
        return;
      int rank = abilityData.Rank;
      MonoSingleton<GameManager>.Instance.NotifyAbilityRankUpCountChanged();
      MonoSingleton<GameManager>.Instance.Player.OnAbilityPowerUp(this.mCurrentUnit.UnitID, abilityData.AbilityID, rank, false);
      if ((UnityEngine.Object) itemGO != (UnityEngine.Object) null)
      {
        if (!string.IsNullOrEmpty(this.AbilityRankUpTrigger))
        {
          Animator component = itemGO.GetComponent<Animator>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.SetTrigger(this.AbilityRankUpTrigger);
        }
        if ((UnityEngine.Object) this.AbilityRankUpEffect != (UnityEngine.Object) null)
          UIUtility.SpawnParticle(this.AbilityRankUpEffect, itemGO.transform as RectTransform, new Vector2(0.5f, 0.5f));
      }
      this.mAbilityPicker.ListBody.UpdateItem(abilityData);
      if ((UnityEngine.Object) this.mAbilityListPanel != (UnityEngine.Object) null)
        this.mAbilityListPanel.AbilityList.UpdateItem(abilityData);
      if ((UnityEngine.Object) this.mAbilitySlotPanel != (UnityEngine.Object) null)
        this.mAbilitySlotPanel.AbilitySlots.UpdateItem(abilityData);
      this.mRankedUpAbilities.Add(abilityData.UniqueID);
      MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0013", 0.0f);
      if (this.IsFirstPlay)
        return;
      this.IsFirstPlay = true;
      this.PlayUnitVoice("chara_0017");
    }

    private void OnAbilityRankUpExec(AbilityData abilityData, GameObject go)
    {
      if (abilityData == null || this.mRankedUpAbilities == null || this.mRankedUpAbilities.Count <= 0)
        return;
      this.QueueKyokaRequest(new UnitEnhanceV3.DeferredJob(this.SubmitAbilityRankUpRequest), 0.0f);
      this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
      this.IsFirstPlay = false;
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) null);
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_COIN_STATUS.ToString(), (object) null);
      this.mAbilitySlotDirty = true;
      List<string> learningSkillList2 = this.mCurrentUnit.GetAbilityData(abilityData.UniqueID).GetLearningSkillList2(abilityData.Rank);
      if (learningSkillList2 != null && learningSkillList2.Count > 0)
      {
        List<SkillParam> newSkills = new List<SkillParam>(learningSkillList2.Count);
        GameManager instance = MonoSingleton<GameManager>.Instance;
        for (int index = 0; index < learningSkillList2.Count; ++index)
        {
          try
          {
            SkillParam skillParam = instance.GetSkillParam(learningSkillList2[index]);
            newSkills.Add(skillParam);
          }
          catch (Exception ex)
          {
          }
        }
        this.mKeepWindowLocked = true;
        this.StartCoroutine(this.PostAbilityRankUp(newSkills));
      }
      else
      {
        this.GetComponent<WindowController>().SetCollision(false);
        this.mKeepWindowLocked = true;
        this.StartCoroutine(this.AbilityRankUpSkillUnlockEffect());
      }
    }

    private void OnAbilityRankUp(AbilityData abilityData, GameObject itemGO)
    {
      if (this.ExecQueuedKyokaRequest(new UnitEnhanceV3.DeferredJob(this.SubmitAbilityRankUpRequest)) || !MonoSingleton<GameManager>.Instance.Player.CheckRankUpAbility(abilityData))
        return;
      this.mChQuestProg = (UnitData.CharacterQuestUnlockProgress) null;
      if (this.mCurrentUnit.IsOpenCharacterQuest())
        this.mChQuestProg = this.mCurrentUnit.SaveUnlockProgress();
      this.mCurrentUnitUnlocks = this.mCurrentUnit.UnlockedSkillIds();
      if (!MonoSingleton<GameManager>.Instance.Player.RankUpAbility(abilityData, true))
        return;
      int rank = abilityData.Rank;
      MonoSingleton<GameManager>.Instance.NotifyAbilityRankUpCountChanged();
      MonoSingleton<GameManager>.Instance.Player.OnAbilityPowerUp(this.mCurrentUnit.UnitID, abilityData.AbilityID, rank, false);
      if ((UnityEngine.Object) itemGO != (UnityEngine.Object) null)
      {
        if (!string.IsNullOrEmpty(this.AbilityRankUpTrigger))
        {
          Animator component = itemGO.GetComponent<Animator>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.SetTrigger(this.AbilityRankUpTrigger);
        }
        if ((UnityEngine.Object) this.AbilityRankUpEffect != (UnityEngine.Object) null)
          UIUtility.SpawnParticle(this.AbilityRankUpEffect, itemGO.transform as RectTransform, new Vector2(0.5f, 0.5f));
      }
      this.mAbilityPicker.ListBody.UpdateItem(abilityData);
      if ((UnityEngine.Object) this.mAbilityListPanel != (UnityEngine.Object) null)
        this.mAbilityListPanel.AbilityList.UpdateItem(abilityData);
      if ((UnityEngine.Object) this.mAbilitySlotPanel != (UnityEngine.Object) null)
        this.mAbilitySlotPanel.AbilitySlots.UpdateItem(abilityData);
      this.mRankedUpAbilities.Add(abilityData.UniqueID);
      this.QueueKyokaRequest(new UnitEnhanceV3.DeferredJob(this.SubmitAbilityRankUpRequest), 0.0f);
      this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
      this.PlayUnitVoice("chara_0017");
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) null);
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_COIN_STATUS.ToString(), (object) null);
      List<string> learningSkillList = abilityData.GetLearningSkillList(rank);
      if (learningSkillList != null && learningSkillList.Count > 0)
      {
        List<SkillParam> newSkills = new List<SkillParam>(learningSkillList.Count);
        GameManager instance = MonoSingleton<GameManager>.Instance;
        for (int index = 0; index < learningSkillList.Count; ++index)
        {
          try
          {
            SkillParam skillParam = instance.GetSkillParam(learningSkillList[index]);
            newSkills.Add(skillParam);
          }
          catch (Exception ex)
          {
          }
        }
        this.mKeepWindowLocked = true;
        this.StartCoroutine(this.PostAbilityRankUp(newSkills));
      }
      else
      {
        this.GetComponent<WindowController>().SetCollision(false);
        this.mKeepWindowLocked = true;
        this.StartCoroutine(this.AbilityRankUpSkillUnlockEffect());
      }
    }

    [DebuggerHidden]
    private IEnumerator AbilityRankUpSkillUnlockEffect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CAbilityRankUpSkillUnlockEffect\u003Ec__IteratorC() { \u0024this = this };
    }

    private void SubmitAbilityRankUpRequest()
    {
      this.mKeepWindowLocked = false;
      Dictionary<long, int> abilities = new Dictionary<long, int>();
      for (int index1 = 0; index1 < this.mRankedUpAbilities.Count; ++index1)
      {
        long mRankedUpAbility = this.mRankedUpAbilities[index1];
        if (abilities.ContainsKey(mRankedUpAbility))
        {
          Dictionary<long, int> dictionary;
          long index2;
          (dictionary = abilities)[index2 = mRankedUpAbility] = dictionary[index2] + 1;
        }
        else
          abilities[mRankedUpAbility] = 1;
      }
      this.mRankedUpAbilities.Clear();
      this.mAbilityRankUpRequestSent = false;
      string trophy_progs;
      string bingo_progs;
      MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
      this.RequestAPI((WebAPI) new ReqAbilityRankUp(abilities, new Network.ResponseCallback(this.OnSubmitAbilityRankUpResult), trophy_progs, bingo_progs));
      this.SetUnitDirty();
    }

    private void OnSubmitAbilityRankUpResult(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.AbilityMaterialShort)
          FlowNode_Network.Back();
        else
          FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          this.mLastSyncTime = Time.realtimeSinceStartup;
          MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
          Network.RemoveAPI();
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        this.FinishKyokaRequest();
        this.mAbilityRankUpRequestSent = true;
        MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
      }
    }

    private void OnAbilityRankUpCountPreReset(int unused)
    {
      if (this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null))
        ;
    }

    [DebuggerHidden]
    private IEnumerator PostAbilityRankUp(List<SkillParam> newSkills)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPostAbilityRankUp\u003Ec__IteratorD() { newSkills = newSkills, \u0024this = this };
    }

    private void OnApplicationPause(bool pausing)
    {
      if (!pausing)
        return;
      this.mAppPausing = true;
      this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
      this.mAppPausing = false;
    }

    private void OnApplicationFocus(bool focus)
    {
      if (focus)
        return;
      this.OnApplicationPause(true);
    }

    private void OnKakuseiButtonClick(SRPG_Button button)
    {
      this.OpenKakeraWindow();
    }

    public void OpenKakeraWindow()
    {
      if ((UnityEngine.Object) this.KakeraWindow == (UnityEngine.Object) null)
        return;
      this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
      this.mUnlockJobParam = (JobParam) null;
      if (this.mCurrentUnit.Jobs != null)
      {
        int rarity = this.mCurrentUnit.Rarity;
        int awakeLv = this.mCurrentUnit.AwakeLv;
        int awakeLevelCap = this.mCurrentUnit.GetAwakeLevelCap();
        if (awakeLv < awakeLevelCap)
        {
          MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
          for (int jobNo = 0; jobNo < this.mCurrentUnit.Jobs.Length; ++jobNo)
          {
            if (!this.mCurrentUnit.Jobs[jobNo].IsActivated)
            {
              JobSetParam jobSetParam = this.mCurrentUnit.GetJobSetParam(jobNo);
              if (jobSetParam != null && rarity >= jobSetParam.lock_rarity)
              {
                JobSetParam[] changeJobSetParam = masterParam.GetClassChangeJobSetParam(this.mCurrentUnit.UnitParam.iname);
                if (changeJobSetParam != null && changeJobSetParam.Length > 0)
                {
                  bool flag = false;
                  for (int index = 0; index < changeJobSetParam.Length; ++index)
                  {
                    if (changeJobSetParam[index].iname == jobSetParam.iname)
                    {
                      flag = true;
                      break;
                    }
                  }
                  if (flag)
                    continue;
                }
                if (jobSetParam.lock_jobs != null)
                {
                  for (int index = 0; index < jobSetParam.lock_jobs.Length; ++index)
                  {
                    if (jobSetParam.lock_jobs[index] != null)
                    {
                      string iname = jobSetParam.lock_jobs[index].iname;
                      if (jobSetParam.lock_jobs[index].lv <= this.mCurrentUnit.GetJobLevelByJobID(iname))
                        ;
                    }
                  }
                }
                if (awakeLv + 1 == jobSetParam.lock_awakelv)
                {
                  this.mUnlockJobParam = this.mCurrentUnit.Jobs[jobNo].Param;
                  break;
                }
              }
            }
          }
        }
      }
      WindowController.OpenIfAvailable((Component) this.KakeraWindow);
      this.KakeraWindow.Refresh(this.mCurrentUnit, this.mUnlockJobParam);
      this.GetComponent<WindowController>().SetCollision(false);
      this.KakeraWindow.GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnKakuseiCancel);
    }

    public void OpenProfile()
    {
      if (!((UnityEngine.Object) this.mUnitProfileWindow == (UnityEngine.Object) null) || this.mProfileWindowLoadRequest != null)
        return;
      this.StartCoroutine(this._OpenProfileWindow());
    }

    [DebuggerHidden]
    private IEnumerator _OpenProfileWindow()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003C_OpenProfileWindow\u003Ec__IteratorE() { \u0024this = this };
    }

    private void OnUnitKakusei()
    {
      if (this.IsKyokaRequestQueued)
        return;
      if (!this.mCurrentUnit.CheckUnitAwaking())
      {
        Debug.LogError((object) "なんかエラーだす");
      }
      else
      {
        this.KakeraWindow.GetComponent<WindowController>().Close();
        this.KakeraWindow.GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnKakuseiAccept);
        this.mKakuseiRequestSent = false;
        if (Network.Mode == Network.EConnectMode.Online)
        {
          MonoSingleton<GameManager>.Instance.Player.OnLimitBreak(this.mCurrentUnit.UnitID, 1);
          string trophy_progs;
          string bingo_progs;
          MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
          this.RequestAPI((WebAPI) new ReqUnitPlus(this.mCurrentUnit.UniqueID, new Network.ResponseCallback(this.OnUnitKakuseiResult), trophy_progs, bingo_progs));
        }
        else
        {
          this.mKakuseiRequestSent = true;
          MonoSingleton<GameManager>.GetInstanceDirect().Player.OnLimitBreak(this.mCurrentUnit.UnitID, 1);
        }
        this.SetUnitDirty();
      }
    }

    private void OnUnitAwake2(int awake_lv)
    {
      if (this.IsKyokaRequestQueued)
        return;
      if (!this.mCurrentUnit.CheckUnitAwaking(awake_lv))
      {
        DebugUtility.LogWarning("[UnitEnhanceV3]OnUnitAwake2=>Not Unit Awake!");
      }
      else
      {
        this.KakeraWindow.GetComponent<WindowController>().Close();
        this.KakeraWindow.GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnKakuseiAccept);
        this.mKakuseiRequestSent = false;
        MonoSingleton<GameManager>.Instance.Player.OnLimitBreak(this.mCurrentUnit.UnitID, awake_lv);
        string trophy_progs = string.Empty;
        string bingo_progs = string.Empty;
        MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
        this.mCacheAwakeLv = awake_lv;
        this.RequestAPI((WebAPI) new ReqUnitAwake(this.mCurrentUnit.UniqueID, new Network.ResponseCallback(this.OnUnitKakuseiResult), trophy_progs, bingo_progs, this.mCurrentUnit.AwakeLv + awake_lv));
        this.SetUnitDirty();
      }
    }

    private void OnUnitKakuseiResult(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.PlusMaterialShot)
          FlowNode_Network.Failed();
        else
          FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          this.mLastSyncTime = Time.realtimeSinceStartup;
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        Network.RemoveAPI();
        this.RefreshSortedUnits();
        this.mKakuseiRequestSent = true;
        MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
      }
    }

    private void OnKakuseiAccept(GameObject go, bool visible)
    {
      if (visible)
        return;
      this.KakeraWindow.GetComponent<WindowController>().OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      this.StartCoroutine(this.PostUnitKakusei());
    }

    [DebuggerHidden]
    private IEnumerator PostUnitKakusei()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPostUnitKakusei\u003Ec__IteratorF() { \u0024this = this };
    }

    private void OnKakuseiCancel(GameObject go, bool visible)
    {
      if (visible)
        return;
      go.GetComponent<WindowController>().OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      this.GetComponent<WindowController>().SetCollision(true);
    }

    private void OnEvolutionButtonClick(SRPG_Button button)
    {
      if (this.mCurrentUnit.GetRarityCap() <= this.mCurrentUnit.Rarity)
      {
        UIUtility.NegativeSystemMessage(string.Empty, LocalizedText.Get("sys.DISABLE_EVOLUTION"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
        this.StartCoroutine(this.EvolutionButtonClickSync());
      }
    }

    [DebuggerHidden]
    private IEnumerator EvolutionButtonClickSync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CEvolutionButtonClickSync\u003Ec__Iterator10() { \u0024this = this };
    }

    private void OnEvolutionWindowClose()
    {
      this.GetComponent<WindowController>().SetCollision(true);
      this.mKeepWindowLocked = false;
    }

    public void OnEvolutionRestore()
    {
      this.OnEvolutionButtonClick(this.UnitEvolutionButton);
    }

    public void OnUnlockTobiraRestore()
    {
      this.OpenUnlockTobiraWindow();
    }

    public void OnEnhanceTobiraRestore()
    {
      this.TobiraUIActive(true, true);
    }

    private void OnUnitEvolve()
    {
      if (this.IsKyokaRequestQueued)
        return;
      if (!this.mCurrentUnit.CheckUnitRarityUp())
      {
        Debug.LogError((object) "なんかエラーだす");
      }
      else
      {
        this.mEvolutionWindow.GetComponent<WindowController>().Close();
        this.mEvolutionWindow.GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnEvolutionStart);
        this.mEvolutionRequestSent = false;
        if (Network.Mode == Network.EConnectMode.Online)
        {
          MonoSingleton<GameManager>.Instance.Player.OnEvolutionChange(this.mCurrentUnit.UnitID, this.mCurrentUnit.Rarity);
          string trophy_progs;
          string bingo_progs;
          MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
          this.RequestAPI((WebAPI) new ReqUnitRare(this.mCurrentUnit.UniqueID, new Network.ResponseCallback(this.OnUnitEvolutionResult), trophy_progs, bingo_progs));
        }
        else
        {
          this.mEvolutionRequestSent = true;
          MonoSingleton<GameManager>.GetInstanceDirect().Player.OnEvolutionChange(this.mCurrentUnit.UnitID, this.mCurrentUnit.Rarity);
        }
        this.SetUnitDirty();
      }
    }

    private void OnEvolutionStart(GameObject go, bool visible)
    {
      if (visible)
        return;
      go.GetComponent<WindowController>().OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      this.StartCoroutine(this.PostUnitEvolution());
    }

    private void OnEvolutionCancel(GameObject go, bool visible)
    {
      if (visible)
        return;
      go.GetComponent<WindowController>().OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      this.GetComponent<WindowController>().SetCollision(true);
    }

    private void OnUnitEvolutionResult(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.RareMaterialShort)
          FlowNode_Network.Failed();
        else
          FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          this.mLastSyncTime = Time.realtimeSinceStartup;
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Retry();
          return;
        }
        Network.RemoveAPI();
        this.mEvolutionRequestSent = true;
        this.RebuildUnitData();
        this.RefreshSortedUnits();
        this.RefreshJobInfo();
        this.RefreshJobIcons(false);
        this.RefreshEquipments(-1);
        this.RefreshBasicParameters(false);
        this.UpdateJobRankUpButtonState();
        GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) null);
        GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_COIN_STATUS.ToString(), (object) null);
        MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
        this.CheckPlayBackUnlock();
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.Unit);
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.UnitUnlock);
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.ItemEquipment);
      }
    }

    [DebuggerHidden]
    private IEnumerator PostUnitEvolution()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPostUnitEvolution\u003Ec__Iterator11() { \u0024this = this };
    }

    private void OpenUnlockTobiraWindow()
    {
      if (!this.mCurrentUnit.CanUnlockTobira() || string.IsNullOrEmpty(this.PREFAB_PATH_UNLOCK_TOBIRA_WINDOW))
        return;
      GameObject original = AssetManager.Load<GameObject>(this.PREFAB_PATH_UNLOCK_TOBIRA_WINDOW);
      if (!((UnityEngine.Object) original != (UnityEngine.Object) null))
        return;
      this.mUnlockTobiraWindow = UnityEngine.Object.Instantiate<GameObject>(original).GetComponent<UnitUnlockTobiraWindow>();
      this.mUnlockTobiraWindow.OnCallback = new UnitUnlockTobiraWindow.CallbackEvent(this.OnUnitUnlockTobira);
    }

    public void TobiraUIActive(bool is_active, bool is_immediate = false)
    {
      if (!this.mCurrentUnit.IsUnlockTobira || (UnityEngine.Object) this.UnitinfoViewAnimator == (UnityEngine.Object) null)
        return;
      this.StartCoroutine(this._TobiraUIActive(is_active, is_immediate));
    }

    [DebuggerHidden]
    private IEnumerator _TobiraUIActive(bool is_active, bool is_immediate = false)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003C_TobiraUIActive\u003Ec__Iterator12() { is_immediate = is_immediate, is_active = is_active, \u0024this = this };
    }

    private void OnUnitUnlockTobira()
    {
      this.StartCoroutine(this.UnitUnlockTobira());
    }

    [DebuggerHidden]
    private IEnumerator UnitUnlockTobira()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CUnitUnlockTobira\u003Ec__Iterator13() { \u0024this = this };
    }

    private void OnUnitUnlockTobiraResult(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.RareMaterialShort)
          FlowNode_Network.Failed();
        else
          FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          this.mLastSyncTime = Time.realtimeSinceStartup;
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Retry();
          return;
        }
        Network.RemoveAPI();
        this.mUnlockTobiraRequestSent = true;
        this.RebuildUnitData();
        this.RefreshSortedUnits();
        this.RefreshJobInfo();
        this.RefreshJobIcons(false);
        this.RefreshEquipments(-1);
        this.RefreshBasicParameters(false);
        this.UpdateJobRankUpButtonState();
        GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) null);
        GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_COIN_STATUS.ToString(), (object) null);
        MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
        this.CheckPlayBackUnlock();
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.Unit);
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.UnitUnlock);
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.ItemEquipment);
        MonoSingleton<GameManager>.Instance.Player.OnUnlockTobiraTrophy((long) GlobalVars.SelectedUnitUniqueID);
      }
    }

    [DebuggerHidden]
    private IEnumerator PostUnitUnlockTobira()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPostUnitUnlockTobira\u003Ec__Iterator14() { \u0024this = this };
    }

    private void RefreshExpInfo()
    {
      if (!((UnityEngine.Object) this.UnitExpInfo != (UnityEngine.Object) null))
        return;
      GameParameter.UpdateAll(this.UnitExpInfo);
    }

    private void OnShiftUnit(SRPG_Button button)
    {
      if (!button.interactable)
        return;
      int num = !((UnityEngine.Object) button == (UnityEngine.Object) this.NextUnitButton) ? -1 : 1;
      int count = this.Units.Count;
      if (count == 0)
        return;
      long uniqueId = this.Units[(this.CurrentUnitIndex + num + count) % count].UniqueID;
      this.GetComponent<WindowController>().SetCollision(false);
      this.mKeepWindowLocked = true;
      this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
      this.StartCoroutine(this.ShiftUnitAsync(uniqueId));
    }

    [DebuggerHidden]
    private IEnumerator ShiftUnitAsync(long unitUniqueID)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CShiftUnitAsync\u003Ec__Iterator15() { unitUniqueID = unitUniqueID, \u0024this = this };
    }

    private int CurrentUnitIndex
    {
      get
      {
        return this.Units.IndexOf(MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnitID));
      }
    }

    private List<UnitData> Units
    {
      get
      {
        return this.SortedUnits;
      }
    }

    private void CreateOverlay()
    {
      if (!((UnityEngine.Object) this.mOverlayCanvas == (UnityEngine.Object) null))
        return;
      this.mOverlayCanvas = UIUtility.PushCanvas(false, -1);
    }

    private void AttachOverlay(GameObject go)
    {
      this.CreateOverlay();
      go.transform.SetParent(this.mOverlayCanvas.transform, false);
    }

    private void DestroyOverlay()
    {
      if (!((UnityEngine.Object) this.mOverlayCanvas != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mOverlayCanvas.gameObject);
    }

    public bool IsLoading
    {
      get
      {
        return this.mReloading;
      }
    }

    private void SetUnitDirty()
    {
      if (!this.mDirtyUnits.Contains(this.mCurrentUnitID))
        this.mDirtyUnits.Add(this.mCurrentUnitID);
      this.mLeftTime = Time.realtimeSinceStartup;
    }

    public long[] GetDirtyUnits()
    {
      return this.mDirtyUnits.ToArray();
    }

    public void ClearDirtyUnits()
    {
      this.mDirtyUnits.Clear();
    }

    private void OpenLeaderSkillDetail(SRPG_Button button)
    {
      if (!button.IsInteractable() || !((UnityEngine.Object) this.LeaderSkillDetailButton != (UnityEngine.Object) null) || (!((UnityEngine.Object) this.Prefab_LeaderSkillDetail != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mLeaderSkillDetail == (UnityEngine.Object) null)))
        return;
      this.mLeaderSkillDetail = UnityEngine.Object.Instantiate<GameObject>(this.Prefab_LeaderSkillDetail);
      DataSource.Bind<UnitData>(this.mLeaderSkillDetail, this.mCurrentUnit, false);
    }

    private void ShowParamTooltip(PointerEventData eventData)
    {
      if ((UnityEngine.Object) this.Prefab_ParamTooltip == (UnityEngine.Object) null)
        return;
      GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;
      if ((UnityEngine.Object) gameObject == (UnityEngine.Object) null || (UnityEngine.Object) gameObject == (UnityEngine.Object) this.mParamTooltipTarget)
        return;
      this.mParamTooltipTarget = gameObject;
      Tooltip.SetTooltipPosition(gameObject.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f));
      if ((UnityEngine.Object) this.mParamTooltip == (UnityEngine.Object) null)
        this.mParamTooltip = UnityEngine.Object.Instantiate<Tooltip>(this.Prefab_ParamTooltip);
      else
        this.mParamTooltip.ResetPosition();
      if (!((UnityEngine.Object) this.mParamTooltip.TooltipText != (UnityEngine.Object) null))
        return;
      this.mParamTooltip.TooltipText.text = LocalizedText.Get("sys.UE_HELP_" + gameObject.name.ToUpper());
    }

    private void AddParamTooltip(GameObject go)
    {
      if (!((UnityEngine.Object) go != (UnityEngine.Object) null))
        return;
      UIEventListener uiEventListener = go.RequireComponent<UIEventListener>();
      if (uiEventListener.onMove == null)
        uiEventListener.onPointerEnter = new UIEventListener.PointerEvent(this.ShowParamTooltip);
      else
        uiEventListener.onPointerEnter += new UIEventListener.PointerEvent(this.ShowParamTooltip);
    }

    private void OnCharacterQuestOpen(SRPG_Button button)
    {
      this.OnCharacterQuestOpen(button, false);
    }

    private void OnCharacterQuestOpen(SRPG_Button button, bool isRestore)
    {
      if (string.IsNullOrEmpty(this.PREFAB_PATH_CHARA_QUEST_LIST_WINDOW))
        return;
      GameObject original = AssetManager.Load<GameObject>(this.PREFAB_PATH_CHARA_QUEST_LIST_WINDOW);
      if ((UnityEngine.Object) original == (UnityEngine.Object) null)
        return;
      UnitCharacterQuestWindow component = UnityEngine.Object.Instantiate<GameObject>(original).GetComponent<UnitCharacterQuestWindow>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      component.IsRestore = isRestore;
      GlobalVars.SelectedUnitUniqueID.Set(this.mCurrentUnit.UniqueID);
      GlobalVars.PreBattleUnitUniqueID.Set(this.mCurrentUnit.UniqueID);
      this.mCharacterQuestWindow = component;
      this.mCharacterQuestWindow.CurrentUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
      this.GetComponent<WindowController>().SetCollision(false);
      this.mCharacterQuestWindow.GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnCharacterQuestSelectCancel);
      WindowController.OpenIfAvailable((Component) this.mCharacterQuestWindow);
    }

    private void OnCharacterQuestSelectCancel(GameObject go, bool visible)
    {
      if (visible)
        return;
      this.GetComponent<WindowController>().SetCollision(true);
      if ((UnityEngine.Object) this.mCharacterQuestWindow == (UnityEngine.Object) null || visible)
        return;
      this.mCharacterQuestWindow.GetComponent<WindowController>().OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mCharacterQuestWindow.gameObject);
      this.mCharacterQuestWindow = (UnitCharacterQuestWindow) null;
    }

    public void OnCharacterQuestRestore()
    {
      this.OnCharacterQuestOpen(this.CharaQuestButton, true);
    }

    private void OpenCharacterQuestPopupInternal()
    {
      MonoSingleton<GameManager>.Instance.AddCharacterQuestPopup(this.mCurrentUnit);
      MonoSingleton<GameManager>.Instance.ShowCharacterQuestPopup(GameSettings.Instance.CharacterQuest_Unlock);
    }

    [DebuggerHidden]
    private IEnumerator OpenCharacterQuestPopupInternalAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003COpenCharacterQuestPopupInternalAsync\u003Ec__Iterator16() { \u0024this = this };
    }

    public bool HasStarted
    {
      get
      {
        return this.mStarted;
      }
    }

    private void OnSkinSelectOpen(SRPG_Button button)
    {
      if (this.SkinSelectTemplate == null || this.mCurrentUnit == null)
        return;
      this.StartCoroutine(this.OnSkinSelectOpenAsync(false));
    }

    private void OnSkinSelectOpenForViewer(SRPG_Button button)
    {
      if (this.SkinSelectTemplate == null || this.mCurrentUnit == null)
        return;
      this.StartCoroutine(this.OnSkinSelectOpenAsync(true));
    }

    private void OnSkinSetResult(WWWResult www)
    {
      this.GetComponent<WindowController>().SetCollision(true);
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
            this.mLastSyncTime = Time.realtimeSinceStartup;
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Retry();
            return;
          }
          Network.RemoveAPI();
          this.RefreshSortedUnits();
          this.SetUnitDirty();
        }
      }
    }

    [DebuggerHidden]
    private IEnumerator OnSkinSelectOpenAsync(bool isView = false)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003COnSkinSelectOpenAsync\u003Ec__Iterator17() { isView = isView, \u0024this = this };
    }

    private void OnSkinSelect(ArtifactParam artifact)
    {
      this.ShowSkinParamChange(artifact, true);
    }

    private void OnSkinDecide(ArtifactParam artifact)
    {
      this.ShowSkinParamChange(artifact, false);
    }

    private void OnSkinDecideAll(ArtifactParam artifact)
    {
      this.UpdataSkinParamChange(artifact, true);
    }

    private void OnSkinRemoved()
    {
      this.UpdataSkinParamChange((ArtifactParam) null, true);
    }

    private void OnSkinRemovedAll()
    {
      this.UpdataSkinParamChange((ArtifactParam) null, true);
    }

    private void OnSkinWindowClose()
    {
      this.GetComponent<WindowController>().SetCollision(true);
      this.mKeepWindowLocked = false;
    }

    private void UpdataSkinParamChange(ArtifactParam artifact, bool isAll)
    {
      if (Network.Mode != Network.EConnectMode.Online)
        return;
      Dictionary<long, string> sets = new Dictionary<long, string>();
      if (isAll)
      {
        for (int index = 0; index < this.mCurrentUnit.Jobs.Length; ++index)
        {
          if (this.mCurrentUnit.Jobs[index] != null && this.mCurrentUnit.Jobs[index].IsActivated)
            sets[this.mCurrentUnit.Jobs[index].UniqueID] = artifact == null ? string.Empty : artifact.iname;
        }
      }
      else if (this.mCurrentUnit.Jobs[this.mCurrentUnit.JobIndex] != null)
        sets[this.mCurrentUnit.Jobs[this.mCurrentUnit.JobIndex].UniqueID] = artifact == null || artifact.iname == null ? string.Empty : artifact.iname;
      this.GetComponent<WindowController>().SetCollision(false);
      Network.RequestAPI((WebAPI) new ReqSkinSet(sets, new Network.ResponseCallback(this.OnSkinSetResult)), false);
    }

    private void ShowSkinParamChange(ArtifactParam artifact, bool isAll)
    {
      if (isAll)
      {
        for (int jobIndex = 0; jobIndex < this.mCurrentUnit.Jobs.Length; ++jobIndex)
          this.mCurrentUnit.SetJobSkin(artifact == null ? (string) null : artifact.iname, jobIndex, true);
      }
      else
        this.mCurrentUnit.SetJobSkin(artifact == null ? (string) null : artifact.iname, this.mCurrentUnit.JobIndex, true);
      this.ShowSkinSetResult();
      this.FadeUnitImage(0.0f, 0.0f, 0.0f);
      this.StartCoroutine(this.RefreshUnitImage());
      this.FadeUnitImage(0.0f, 1f, 1f);
    }

    [DebuggerHidden]
    private IEnumerator ShowUnlockSkillEffect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CShowUnlockSkillEffect\u003Ec__Iterator18() { \u0024this = this };
    }

    private void UpdateMainUIAnimator(bool visible)
    {
      WindowController component1 = this.GetComponent<WindowController>();
      if (!((UnityEngine.Object) component1 != (UnityEngine.Object) null) || string.IsNullOrEmpty(component1.StateInt))
        return;
      Animator component2 = this.GetComponent<Animator>();
      if (!((UnityEngine.Object) component2 != (UnityEngine.Object) null))
        return;
      component2.SetInteger(component1.StateInt, !visible ? 0 : 1);
    }

    private void OnExpMaxOpen(SRPG_Button button)
    {
      if (!this.mCurrentUnit.CheckGainExp())
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.LEVEL_CAPPED"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        GameObject original = AssetManager.Load<GameObject>(UnitEnhanceV3.UNIT_EXPMAX_UI_PATH);
        if (!((UnityEngine.Object) original != (UnityEngine.Object) null))
          return;
        GameObject root = UnityEngine.Object.Instantiate<GameObject>(original);
        if (!((UnityEngine.Object) root != (UnityEngine.Object) null))
          return;
        UnitLevelUpWindow componentInChildren = root.GetComponentInChildren<UnitLevelUpWindow>();
        if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
          componentInChildren.OnDecideEvent = new UnitLevelUpWindow.OnUnitLevelupEvent(this.OnUnitBulkLevelUp);
        DataSource.Bind<UnitData>(root, this.mCurrentUnit, false);
        DataSource.Bind<UnitEnhanceV3>(root, this, false);
        GameParameter.UpdateAll(root);
      }
    }

    [DebuggerHidden]
    public IEnumerator PlayTobiraOpenEffect(TobiraParam.Category selected_tobira_category)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPlayTobiraOpenEffect\u003Ec__Iterator19() { selected_tobira_category = selected_tobira_category, \u0024this = this };
    }

    [DebuggerHidden]
    public IEnumerator PlayTobiraLevelupEffect(TobiraData tobira)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPlayTobiraLevelupEffect\u003Ec__Iterator1A() { tobira = tobira, \u0024this = this };
    }

    [DebuggerHidden]
    private IEnumerator PlayTobiraLevelupMaxEffect(AbilityData newAbilityData, AbilityData oldAbilityData, SkillData newLeaderSkillData)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPlayTobiraLevelupMaxEffect\u003Ec__Iterator1B() { newAbilityData = newAbilityData, oldAbilityData = oldAbilityData, newLeaderSkillData = newLeaderSkillData, \u0024this = this };
    }

    public void RefreshTobiraBgAnimation(TobiraData tobira, bool is_immediate = false)
    {
      if ((UnityEngine.Object) this.mBGTobiraAnimator == (UnityEngine.Object) null)
        return;
      string str1 = !is_immediate ? "open" : "opened";
      string str2 = !is_immediate ? "close" : "closed";
      bool flag = tobira == null || !tobira.IsUnlocked;
      string stateName = !flag ? str1 : str2;
      AnimatorStateInfo animatorStateInfo = this.mBGTobiraAnimator.GetCurrentAnimatorStateInfo(0);
      if (!flag ? animatorStateInfo.IsName("open") || animatorStateInfo.IsName("opened") : animatorStateInfo.IsName("close") || animatorStateInfo.IsName("closed"))
        return;
      this.mBGTobiraAnimator.Play(stateName);
    }

    private void CheckPlayBackUnlock()
    {
      UnitData.UnitPlaybackVoiceData playbackVoiceData1 = (UnitData.UnitPlaybackVoiceData) null;
      try
      {
        playbackVoiceData1 = this.mCurrentUnit.GetUnitPlaybackVoiceData();
        if (playbackVoiceData1 == null)
          return;
        bool flag = false;
        UnitData.Json_PlaybackVoiceData playbackVoiceData2;
        if (!PlayerPrefsUtility.HasKey(this.mCurrentUnit.UniqueID.ToString()))
        {
          playbackVoiceData2 = new UnitData.Json_PlaybackVoiceData();
          flag = true;
        }
        else
        {
          playbackVoiceData2 = JsonUtility.FromJson<UnitData.Json_PlaybackVoiceData>(PlayerPrefsUtility.GetString(this.mCurrentUnit.UniqueID.ToString(), string.Empty));
          if (playbackVoiceData2 == null)
          {
            playbackVoiceData2 = new UnitData.Json_PlaybackVoiceData();
            flag = true;
          }
        }
        if (playbackVoiceData2.playback_voice_unlocked <= 0 && this.mCurrentUnit.CheckUnlockPlaybackVoice())
        {
          NotifyList.Push(string.Format(LocalizedText.Get("sys.NOTIFY_UNLOCK_PLAYBACK_FUNCTION"), (object) this.mCurrentUnit.UnitParam.name));
          playbackVoiceData2.playback_voice_unlocked = 1;
          flag = true;
        }
        int num = 0;
        for (int index = 0; index < playbackVoiceData1.VoiceCueList.Count; ++index)
        {
          if (playbackVoiceData1.VoiceCueList[index].is_new && !playbackVoiceData2.cue_names.Contains(playbackVoiceData1.VoiceCueList[index].cueInfo.name))
          {
            ++num;
            playbackVoiceData2.cue_names.Add(playbackVoiceData1.VoiceCueList[index].cueInfo.name);
            flag = true;
          }
        }
        if (!flag)
          return;
        if (num > 0)
          NotifyList.Push(string.Format(LocalizedText.Get("sys.NOTIFY_UNLOCK_PLAYBACK_UNITVOICE2"), (object) this.mCurrentUnit.UnitParam.name, (object) num));
        PlayerPrefsUtility.SetString(this.mCurrentUnit.UniqueID.ToString(), JsonUtility.ToJson((object) playbackVoiceData2), false);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
      }
      finally
      {
        playbackVoiceData1?.Cleanup();
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 100:
          this.mCloseRequested = true;
          this.GetComponent<WindowController>().Close();
          break;
        case 110:
          this.UpdateMainUIAnimator(false);
          if ((UnityEngine.Object) this.LeftGroup != (UnityEngine.Object) null)
            this.LeftGroup.blocksRaycasts = false;
          if ((UnityEngine.Object) this.RightGroup != (UnityEngine.Object) null)
            this.RightGroup.blocksRaycasts = false;
          if ((UnityEngine.Object) this.mUnitModelViewer == (UnityEngine.Object) null && (UnityEngine.Object) this.mUnitModelViewerParent != (UnityEngine.Object) null && !string.IsNullOrEmpty(this.PREFAB_PATH_MODEL_VIEWER))
          {
            this.mUnitModelViewer = UnityEngine.Object.Instantiate<GameObject>(AssetManager.Load<GameObject>(this.PREFAB_PATH_MODEL_VIEWER)).GetComponent<UnitModelViewer>();
            this.mUnitModelViewer.transform.SetParent(this.transform, false);
            this.mUnitModelViewer.transform.SetParent(this.mUnitModelViewerParent, false);
            this.mUnitModelViewer.Initalize();
          }
          if (!((UnityEngine.Object) this.mUnitModelViewer != (UnityEngine.Object) null))
            break;
          this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
          this.StopUnitVoice();
          this.mUnitModelViewer.OnChangeJobSlot = new UnitModelViewer.ChangeJobSlotEvent(this.ChangeJobSlot);
          this.mUnitModelViewer.OnSkinSelect = new UnitModelViewer.SkinSelectEvent(this.OnSkinSelectOpen);
          this.mUnitModelViewer.OnPlayReaction = new UnitModelViewer.PlayReaction(this.PlayReaction);
          this.mUnitModelViewer.gameObject.SetActive(true);
          break;
        case 111:
          if ((UnityEngine.Object) this.mUnitModelViewer != (UnityEngine.Object) null)
          {
            this.mUnitModelViewer.ResetTouchControlArea();
            this.mUnitModelViewer.gameObject.SetActive(false);
          }
          this.UpdateMainUIAnimator(true);
          if ((UnityEngine.Object) this.LeftGroup != (UnityEngine.Object) null)
            this.LeftGroup.blocksRaycasts = true;
          if ((UnityEngine.Object) this.RightGroup != (UnityEngine.Object) null)
            this.RightGroup.blocksRaycasts = true;
          this.Refresh(this.mCurrentUnitID, 0L, false, false);
          break;
        case 200:
        case 201:
          this.StopUnitVoice();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 210);
          break;
        case 202:
          this.IsStopPlayLeftVoice = true;
          break;
        case 203:
          this.IsStopPlayLeftVoice = false;
          break;
        case 301:
          this.OpenUnlockTobiraWindow();
          break;
        case 302:
          this.TobiraUIActive(true, false);
          break;
        case 401:
          this.RefreshUnitDetail();
          break;
      }
    }

    private void RefreshUnitDetail()
    {
      Canvas component = this.GetComponent<Canvas>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null) || !component.enabled || this.mCurrentUnitID <= 0L)
        return;
      this.RefreshEquipments(-1);
      this.UpdateJobRankUpButtonState();
      this.UpdateJobChangeButtonState();
      this.UpdateUnitKakuseiButtonState();
      this.UpdateUnitEvolutionButtonState(true);
      this.UpdateUnitTobiraButtonState();
      this.RefreshAbilitySlotButtonState();
      this.RefreshAbilityList();
      this.RefreshAbilitySlots(false);
    }

    private void SetCacheUnitData(UnitData original, int selectedJobIndex)
    {
      this.mCacheUnitData = new UnitData();
      this.mCacheUnitData.Setup(original);
      this.mCacheUnitData.SetJobIndex(selectedJobIndex);
    }

    private void UpdateTrophy_OnJobLevelChange()
    {
      if (this.mCacheUnitData == null)
        DebugUtility.LogError("mCacheUnitDataがnullです。リクエストを投げる前に「SetCacheUnitData」を使用してユニットデータをキャッシュしてください。");
      else if (this.mCacheUnitData.CurrentJob.Rank < 1)
      {
        this.mCacheUnitData = (UnitData) null;
      }
      else
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCacheUnitData.UniqueID);
        if (unitDataByUniqueId != null)
        {
          JobData jobData = unitDataByUniqueId.GetJobData(this.mCacheUnitData.JobIndex);
          if (jobData.UniqueID == this.mCacheUnitData.CurrentJob.UniqueID)
          {
            int num1 = jobData.Rank - this.mCacheUnitData.CurrentJob.Rank;
            if (num1 > 0)
            {
              PlayerData player = MonoSingleton<GameManager>.Instance.Player;
              string iname1 = unitDataByUniqueId.UnitParam.iname;
              string iname2 = jobData.Param.iname;
              int rank1 = jobData.Rank;
              int num2 = num1;
              string unitID = iname1;
              string jobID = iname2;
              int rank2 = rank1;
              int num3 = 0;
              int rankDelta = num2;
              player.OnJobLevelChange(unitID, jobID, rank2, num3 != 0, rankDelta);
              MonoSingleton<GameManager>.Instance.Player.OnUnitLevelAndJobLevelChange(unitDataByUniqueId.UnitParam.iname, unitDataByUniqueId.Lv, jobData);
            }
          }
        }
        this.mCacheUnitData = (UnitData) null;
      }
    }

    private void SetNotifyReleaseClassChangeJob()
    {
      if (this.mCacheUnitData == null)
        return;
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCacheUnitData.UniqueID);
      if (unitDataByUniqueId == null)
        return;
      JobSetParam[] cc_jobset_array = MonoSingleton<GameManager>.Instance.GetClassChangeJobSetParam(unitDataByUniqueId.UnitID);
      if (cc_jobset_array == null || cc_jobset_array.Length <= 0)
        return;
      for (int i = 0; i < cc_jobset_array.Length; ++i)
      {
        int index = Array.FindIndex<JobData>(unitDataByUniqueId.Jobs, (Predicate<JobData>) (job => job.JobID == cc_jobset_array[i].job));
        if (index >= 0)
        {
          bool flag1 = unitDataByUniqueId.Jobs[index].Rank == 0 && (unitDataByUniqueId.CheckJobUnlock(index, false) || unitDataByUniqueId.CheckJobRankUpAllEquip(index, true));
          bool flag2 = this.mCacheUnitData.Jobs[index].Rank == 0 && (this.mCacheUnitData.CheckJobUnlock(index, false) || this.mCacheUnitData.CheckJobRankUpAllEquip(index, true));
          if (flag1 && flag1 != flag2)
            NotifyList.Push(string.Format(LocalizedText.Get("sys.UNITLIST_NOTIFY_UNLOCK_CC_JOB"), (object) unitDataByUniqueId.Jobs[index].Name));
        }
      }
    }

    private enum eTabButtons
    {
      Equipments,
      Kyoka,
      AbilityList,
      AbilitySlot,
      Max,
    }

    private class ExpItemTouchController : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
    {
      public float UseItemSpan = 0.5f;
      public UnitEnhanceV3.ExpItemTouchController.DelegateOnPointerDown OnPointerDownFunc;
      public UnitEnhanceV3.ExpItemTouchController.DelegateOnPointerDown OnPointerUpFunc;
      public UnitEnhanceV3.ExpItemTouchController.DelegateUseItem UseItemFunc;
      public float HoldDuration;
      public bool Holding;
      private int UseNum;
      private int NextSettingIndex;
      private Vector2 mDragStartPos;

      public void OnPointerDown(PointerEventData eventData)
      {
        if (this.OnPointerDownFunc == null)
          return;
        this.OnPointerDownFunc(this);
        this.Holding = true;
        this.mDragStartPos = eventData.position;
      }

      public void OnPointerUp()
      {
        if (this.OnPointerUpFunc != null)
          this.OnPointerUpFunc(this);
        this.StatusReset();
      }

      public void OnDestroy()
      {
        this.StatusReset();
        if (this.OnPointerDownFunc != null)
          this.OnPointerDownFunc = (UnitEnhanceV3.ExpItemTouchController.DelegateOnPointerDown) null;
        if (this.UseItemFunc == null)
          return;
        this.UseItemFunc = (UnitEnhanceV3.ExpItemTouchController.DelegateUseItem) null;
      }

      public void UpdateTimer(float deltaTime)
      {
        if (this.UseItemFunc == null)
          return;
        if (this.Holding && !Input.GetMouseButton(0))
        {
          if ((double) this.HoldDuration < (double) this.UseItemSpan && this.UseNum < 1)
          {
            this.UseItemFunc(this.gameObject);
            ++this.UseNum;
          }
          this.OnPointerUp();
        }
        else
        {
          GameSettings instance = GameSettings.Instance;
          if ((double) this.HoldDuration < (double) this.UseItemSpan && this.UseNum < 1 && (double) (this.mDragStartPos - (Vector2) Input.mousePosition).sqrMagnitude > (double) (instance.HoldMargin * instance.HoldMargin))
          {
            this.StatusReset();
          }
          else
          {
            if (!this.Holding)
              return;
            this.HoldDuration += Time.unscaledDeltaTime;
            if ((double) this.HoldDuration < (double) this.UseItemSpan)
              return;
            this.HoldDuration -= this.UseItemSpan;
            this.UseItemFunc(this.gameObject);
            ++this.UseNum;
            GameSettings.HoldCountSettings[] holdCount = instance.HoldCount;
            if (holdCount.Length <= this.NextSettingIndex || holdCount[this.NextSettingIndex].Count >= this.UseNum)
              return;
            this.UseItemSpan = holdCount[this.NextSettingIndex].UseSpan;
            ++this.NextSettingIndex;
          }
        }
      }

      public void StatusReset()
      {
        this.HoldDuration = 0.0f;
        this.Holding = false;
        this.UseNum = 0;
        this.NextSettingIndex = 0;
        this.mDragStartPos.Set(0.0f, 0.0f);
      }

      public delegate void DelegateOnPointerDown(UnitEnhanceV3.ExpItemTouchController controller);

      public delegate void DelegateOnPointerUp(UnitEnhanceV3.ExpItemTouchController controller);

      public delegate void DelegateUseItem(GameObject listItem);
    }

    private delegate void DeferredJob();

    public delegate void CloseEvent();
  }
}
