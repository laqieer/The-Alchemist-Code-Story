// Decompiled with JetBrains decompiler
// Type: SRPG.GachaWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "ExecFailed", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "DecisionRedraw", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(10, "GachaConfirm", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "ガチャ実行", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Opened", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(102, "Closed", FlowNode.PinTypes.Input, 102)]
  [FlowNode.Pin(110, "チケット召喚(単発)を選択", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(111, "チケット召喚を行う", FlowNode.PinTypes.Input, 111)]
  [FlowNode.Pin(112, "チケット召喚をキャンセル", FlowNode.PinTypes.Input, 112)]
  [FlowNode.Pin(113, "リフレッシュ完了", FlowNode.PinTypes.Output, 113)]
  [FlowNode.Pin(150, "召喚トップ背景をタップ", FlowNode.PinTypes.Input, 150)]
  [FlowNode.Pin(160, "Tap Event Detail", FlowNode.PinTypes.Input, 160)]
  [FlowNode.Pin(170, "Tap Event Description", FlowNode.PinTypes.Input, 170)]
  [FlowNode.Pin(180, "3Dモデル表示部分を非表示", FlowNode.PinTypes.Input, 180)]
  [FlowNode.Pin(200, "召喚結果へ強制遷移", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(210, "ピックアップ選択へ誘導", FlowNode.PinTypes.Output, 210)]
  [FlowNode.Pin(20, "Refresh(背景更新しない)", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(21, "ピックアップ選択後の召喚実行誘導をリセット", FlowNode.PinTypes.Input, 21)]
  [FlowNode.Pin(999, "WebView起動", FlowNode.PinTypes.Output, 999)]
  public class GachaWindow : MonoBehaviour, IFlowInterface
  {
    private const int IN_GACHA_CONFIRM = 10;
    private const int PIN_OT_TO_RESULT = 200;
    private const int PIN_IN_DECISION_REDRAW_GACHA = 3;
    private const int PIN_IN_TAP_GACHA_TOP_BACKGROUND = 150;
    private const int PIN_IN_TAP_GACHA_DETAIL = 160;
    private const int PIN_IN_TAP_GACHA_DESCRIPTION = 170;
    private const int PIN_IN_VISIBLE_PREVIEWS = 180;
    private const int PIN_OT_MOVE_GACHA_PICKUP_SELECT = 210;
    private const int PIN_IN_REFRESH_NOT_REFRESH_BG = 20;
    private const int PIN_IN_RESET_MOVE_GACHAEXEC = 21;
    private static readonly string GACHA_URL_PREFIX = "notice/detail/gacha/";
    private static readonly float WAIT_SWAP_BG = 5f;
    private static readonly string DEFAULT_BGIMAGE_PATH = "GachaImages_Default";
    private static readonly string BG_TEXTURE_PATH = "Gachas/BGTables";
    private static readonly string TAB_SPRITES_PATH = "Gachas/GachaTabSprites";
    private static readonly string TEXT_CONFIRM_GACHA_COIN = "sys.CONFIRM_GACHA_COIN";
    private static readonly string TEXT_COST_GACHA_COIN = "sys.GACHA_COST_COIN";
    private static readonly string TEXT_COST_GACHA_PAIDCOIN = "sys.GACHA_COST_PAIDCOIN";
    private static readonly string TEXT_CONFIRM_GACHA_COST_ZERO = "sys.CONFIRM_GACHA_COST_ZERO";
    public GameObject SelectJobIcon;
    public float mWaitSecondsChangeUnitJob = 5f;
    public string PickupPreviewParentID;
    public string PickUpPreviewBaseID = "GACHAUNITPREVIEWBASE";
    public string PickUpCameraID = "GACHAUNITPRVIEWCAMERA";
    public string BGUnitImageID = "GACHA_TOP_UNIT_IMG";
    private Transform mPreviewParent;
    private GameObject mPreviewBase;
    private Camera mPreviewCamera;
    private GachaUnitPreview mCurrentPreview;
    private List<GachaUnitPreview> mPreviewControllers = new List<GachaUnitPreview>();
    private RawImage mBGUnitImage;
    private UnitData mCurrentUnit;
    private List<UnitData> mPickupUnits;
    private int mCurrentIndex;
    private int mCurrentJobIndex;
    private StateMachine<GachaWindow> mState;
    private bool mInitialized;
    private bool mChangeUnit;
    private bool mChangeJob;
    private bool mClicked;
    public GameObject ChangeUnitEffectObj;
    public GameObject ChangeArtifactEffectObj;
    public float ChangeEffectWaitTime = 1f;
    public float ChangeUnitWaitEffectTime = 1f;
    public float ChangeJobWaitEffectTime = 1f;
    public float WaitTimeNextAction = 1f;
    public GameObject UnitEffectObj;
    public GameObject JobEffectObj;
    public GameObject ArtifactEffectObj;
    private RenderTexture mPreviewUnitRT;
    public RawImage PreviewImage;
    private bool mDesiredPreviewVisibility;
    private bool mUpdatePreviewVisibility;
    private static GachaWindow mInstance;
    public string PickupPreviewArtifact = "GACHAARTIFACTPREVIEW";
    public GameObject DefaultPanel;
    public GameObject TicketPanel;
    public GameObject ButtonPanel;
    public GameObject TabPanel;
    public GameObject OptionPanel;
    public GameObject TicketButtonTemplate;
    public Transform TicketListRoot;
    public GameObject TicketNotListView;
    private List<GachaTopParamNew> mGachaListRare = new List<GachaTopParamNew>();
    private List<GachaTopParamNew> mGachaListNormal = new List<GachaTopParamNew>();
    private List<GachaTopParamNew> mGachaListTicket = new List<GachaTopParamNew>();
    private List<GachaTopParamNew> mGachaListArtifact = new List<GachaTopParamNew>();
    private List<GachaWindow.GachaTopParamNewGroups> mGachaListSpecials = new List<GachaWindow.GachaTopParamNewGroups>();
    private List<GachaTopParamNew> mGachaListAll = new List<GachaTopParamNew>();
    public SRPG_Button RareTab;
    public SRPG_Button NormalTab;
    public SRPG_Button TicketTab;
    public SRPG_Button ArtifactTab;
    public SRPG_Button TabTemplate;
    private List<SRPG_Button> mTabList = new List<SRPG_Button>();
    private GachaWindow.GachaTabCategory mSelectTab;
    public GameObject UnitInfoPanel;
    public GameObject ArtifactInfoPanel;
    public GameObject BonusPanel;
    public UnityEngine.UI.Text BonusItemName;
    public GameObject BonusPanelItem;
    public GameObject BonusPanelUnit;
    public GameObject BonusPanelArtifact;
    public GameObject BonusPanelConceptCard;
    public StatusList Status;
    public GameObject WeaponAbilityInfo;
    public GameObject ArtifactRarityPanel;
    public UnityEngine.UI.Text ArtifactType;
    private List<ArtifactData> mPickupArtifacts;
    private ArtifactData mCurrentArtifact;
    public RawImage PreviewArtifactImage;
    private Transform mCurrentArtifactPreview;
    private Transform mPreviewArtifact;
    private List<Transform> mPreviewArtifactControllers = new List<Transform>();
    public Transform BGRoot;
    public GameObject BonusMsgPanel;
    public UnityEngine.UI.Text BonusMsgText;
    private int mCurrentTabSPIndex = -1;
    private int mCurrentPickupArtIndex;
    private Dictionary<string, GameObject> mTicketButtonLists = new Dictionary<string, GameObject>();
    private bool mLoadGachaTabSprites;
    private Dictionary<string, Sprite> mCacheTabImages = new Dictionary<string, Sprite>();
    private List<Transform> mBGObjects = new List<Transform>();
    private bool mLoadBackGroundTexture;
    private Dictionary<string, Texture2D> mCacheBGImages = new Dictionary<string, Texture2D>();
    private Texture2D mDefaultBG;
    private bool IsTabChanging;
    private int mCurrentTabIndex = -1;
    private float mWaitSwapBGTime;
    private bool mExistSwapBG;
    private int mEnableBGIndex;
    private List<GachaWindow.GachaTabCategory> mTabCategoryList = new List<GachaWindow.GachaTabCategory>();
    private static string mDetailURL = string.Empty;
    public string DESCRIPTION_URL = "description";
    private string mDescriptionURL = string.Empty;
    [SerializeField]
    private ButtonEvent DetailButtonEvent;
    [SerializeField]
    private Button DetailButton;
    [SerializeField]
    private Button DescriptionButton;
    [SerializeField]
    private GameObject GachaConfirmWindow;
    private GachaRequestParam m_request;
    [SerializeField]
    private Transform RootObject;
    private float mBGUnitImgAlphaStart;
    private float mBGUnitImgAlphaEnd;
    private float mBGUnitImgFadeTime;
    private float mBGUnitImgFadeTimeMax;
    private bool IsRefreshingGachaBG;
    [SerializeField]
    private GameObject GachaButtonTemplate;
    private List<GachaButton> m_GachaButtons = new List<GachaButton>();
    private List<GachaTopParamNew> m_CacheList_Gold = new List<GachaTopParamNew>();
    private List<GachaTopParamNew> m_CacheList_Coin = new List<GachaTopParamNew>();
    private List<GachaTopParamNew> m_CacheList_CoinPaid = new List<GachaTopParamNew>();
    [SerializeField]
    private GameObject SummonCoin;
    [SerializeField]
    private Button DiscountButton;
    private static string m_SelectDiscountGachaID = string.Empty;
    private static string m_SelectDiscountGroupID = string.Empty;
    public GachaWindow.BackGround BG01;
    public GachaWindow.BackGround BG02;
    private Dictionary<string, GachaWindow.BackGroundTextureGroup> mCacheBackGroundImages = new Dictionary<string, GachaWindow.BackGroundTextureGroup>();
    [Header("召喚トップの背景画像の参照を管理するScriptableObjectのパス")]
    [SerializeField]
    private string BGTABLE_PATH = "Gachas/BGTablesDummy";
    [SerializeField]
    private string BGTEXTURE_SUFFIX_LEFT = "_l";
    [SerializeField]
    private string BGTEXTURE_SUFFIX_RIGHT = "_r";
    [SerializeField]
    private int MAX_BGTEXTURE_CACHE = 10;
    [Header("召喚トップで使用する背景画像を格納するフォルダのパス")]
    [SerializeField]
    private string GACHABG_FOLDER_PATH = "GachaImages/";
    [Header("BGTablesを使用して背景画像を表示する機能を利用する")]
    [SerializeField]
    private bool IsUseBGTables = true;
    private const int MAX_GACHATEXTURE_COUNT = 2;
    [Space(10f)]
    [Header("オプションパーツ")]
    [SerializeField]
    private GameObject BonusParts;
    public GachaWindow.OptionParts OptionPartsObject;
    [SerializeField]
    private UnityEngine.UI.Text BonusText;
    [SerializeField]
    private GachaWindow.BonusIcon[] BonusIcons;
    [SerializeField]
    private UnityEngine.UI.Text AppealText;
    private string CONFIRM_WINDOW_PATH = "UI/GachaConfirmWindow";
    private GachaRequestParam m_CurrentGachaRequestParam;
    private GachaButton PrevGachaButton;
    [SerializeField]
    private Button PickupSelectButton;
    [SerializeField]
    private GameObject PickupSelectAnimationObject;
    private Dictionary<string, GachaPickupSelectParam> mGachaPickupSelectLists = new Dictionary<string, GachaPickupSelectParam>();
    private List<GachaDropData> mRequestPickupSelectDatas = new List<GachaDropData>();

    public static GachaWindow Instance => GachaWindow.mInstance;

    public ArtifactData CurrentArtifact => this.mCurrentArtifact;

    public static string BaseDetailUrl => GachaWindow.mDetailURL;

    public static string SelectDiscountGachaID => GachaWindow.m_SelectDiscountGachaID;

    public static string SelectDiscountGroupID => GachaWindow.m_SelectDiscountGroupID;

    public bool ChangeUnit
    {
      get => this.mChangeUnit;
      set => this.mChangeUnit = value;
    }

    public bool ChangeJob
    {
      get => this.mChangeJob;
      set => this.mChangeJob = value;
    }

    public bool Initialized => this.mInitialized;

    public UnitData CurrentUnit => this.mCurrentUnit;

    public bool Clicked => this.mClicked;

    private void Awake()
    {
      GachaWindow.mInstance = this;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DefaultPanel, (UnityEngine.Object) null))
        this.DefaultPanel.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TicketPanel, (UnityEngine.Object) null))
        this.TicketPanel.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ButtonPanel, (UnityEngine.Object) null))
        this.ButtonPanel.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RareTab, (UnityEngine.Object) null))
        ((Component) this.RareTab).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactTab, (UnityEngine.Object) null))
        ((Component) this.ArtifactTab).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TicketTab, (UnityEngine.Object) null))
        ((Component) this.TicketTab).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NormalTab, (UnityEngine.Object) null))
        ((Component) this.NormalTab).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TabTemplate, (UnityEngine.Object) null))
        ((Component) this.TabTemplate).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TicketButtonTemplate, (UnityEngine.Object) null))
        this.TicketButtonTemplate.SetActive(false);
      this.mPreviewUnitRT = this.CreateRenderTexture();
      this.mPreviewCamera = GameObjectID.FindGameObject<Camera>(this.PickUpCameraID);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewCamera, (UnityEngine.Object) null))
        this.mPreviewCamera.targetTexture = this.mPreviewUnitRT;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Network.SiteHost);
      stringBuilder.Append(GachaWindow.GACHA_URL_PREFIX);
      stringBuilder.Append(this.DESCRIPTION_URL);
      this.mDescriptionURL = stringBuilder.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DescriptionButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.DescriptionButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnClickDescription)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GachaButtonTemplate, (UnityEngine.Object) null))
        this.GachaButtonTemplate.SetActive(false);
      GachaWindow.m_SelectDiscountGachaID = string.Empty;
      GachaWindow.m_SelectDiscountGroupID = string.Empty;
    }

    private void Start()
    {
      this.SetupGachaList(MonoSingleton<GameManager>.Instance.Gachas);
      this.ClearTabSprites();
      this.ClearBGSprites();
      this.StartCoroutine(this.LoadGachaTabSprites());
      if (this.mGachaListSpecials != null && this.mGachaListSpecials.Count > 0)
      {
        List<string> stringList = new List<string>();
        foreach (GachaWindow.GachaTopParamNewGroups gachaListSpecial in this.mGachaListSpecials)
        {
          if (gachaListSpecial != null && !string.IsNullOrEmpty(gachaListSpecial.bg_image))
          {
            stringList.Add(gachaListSpecial.bg_image);
            if (stringList.Count >= this.MAX_BGTEXTURE_CACHE)
              break;
          }
        }
        if (stringList.Count > 0)
          this.StartCoroutine(!this.IsUseBGTables ? this.LoadBackGroundTextures(stringList.ToArray()) : this.LoadBackGroundTextureForBGTables(stringList.ToArray()));
      }
      this.RefreshSummonCoin();
      this.InitGachaUnitPreview(false);
      this.mState = new StateMachine<GachaWindow>(this);
      this.mState.GotoState<GachaWindow.State_Init>();
    }

    private void OnEnable()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) HomeWindow.Current, (UnityEngine.Object) null))
        return;
      HomeWindow.Current.SetVisible(true);
    }

    private void Update()
    {
      if (this.mState != null)
        this.mState.Update();
      if (this.mUpdatePreviewVisibility && this.mDesiredPreviewVisibility)
      {
        if ((this.mSelectTab == GachaWindow.GachaTabCategory.RARE || this.mSelectTab == GachaWindow.GachaTabCategory.ARTIFACT) && this.IsTabChanging)
        {
          this.PlayChangeEffect();
          this.IsTabChanging = false;
        }
        if (this.mSelectTab == GachaWindow.GachaTabCategory.RARE)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCurrentPreview, (UnityEngine.Object) null) && !this.mCurrentPreview.IsLoading)
          {
            GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerCH, true);
            GameUtility.SetLayer((Component) this.mCurrentArtifactPreview, GameUtility.LayerHidden, true);
            this.mUpdatePreviewVisibility = false;
          }
        }
        else if (this.mSelectTab == GachaWindow.GachaTabCategory.ARTIFACT && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCurrentArtifactPreview, (UnityEngine.Object) null))
        {
          GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerHidden, true);
          GameUtility.SetLayer((Component) this.mCurrentArtifactPreview, GameUtility.LayerCH, true);
          this.mUpdatePreviewVisibility = false;
        }
      }
      if ((double) this.mBGUnitImgFadeTime < (double) this.mBGUnitImgFadeTimeMax && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBGUnitImage, (UnityEngine.Object) null))
      {
        this.mBGUnitImgFadeTime += Time.unscaledDeltaTime;
        float num = Mathf.Clamp01(this.mBGUnitImgFadeTime / this.mBGUnitImgFadeTimeMax);
        this.SetUnitImageAlpha(Mathf.Lerp(this.mBGUnitImgAlphaStart, this.mBGUnitImgAlphaEnd, num));
        if ((double) num >= 1.0)
        {
          this.mBGUnitImgFadeTime = 0.0f;
          this.mBGUnitImgFadeTimeMax = 0.0f;
        }
      }
      this.UpdateBackGround();
    }

    private void OnDestroy()
    {
      GachaWindow.mInstance = (GachaWindow) null;
      GameUtility.DestroyGameObject((Component) this.mCurrentPreview);
      this.mCurrentPreview = (GachaUnitPreview) null;
      GameUtility.DestroyGameObjects<GachaUnitPreview>(this.mPreviewControllers);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewUnitRT, (UnityEngine.Object) null))
      {
        RenderTexture.ReleaseTemporary(this.mPreviewUnitRT);
        this.mPreviewUnitRT = (RenderTexture) null;
      }
      GameUtility.DestroyGameObject((Component) this.mCurrentArtifactPreview);
      this.mCurrentArtifactPreview = (Transform) null;
      GameUtility.DestroyGameObjects<Transform>(this.mPreviewArtifactControllers);
      this.ClearTabSprites();
      GameUtility.DestroyGameObjects<SRPG_Button>(this.mTabList.ToArray());
      this.mTabList.Clear();
      this.mDefaultBG = (Texture2D) null;
      this.ClearBGSprites();
      foreach (string key in this.mTicketButtonLists.Keys)
        GameUtility.DestroyGameObject(this.mTicketButtonLists[key]);
      this.mTicketButtonLists = (Dictionary<string, GameObject>) null;
      GachaWindow.m_SelectDiscountGachaID = string.Empty;
      GachaWindow.m_SelectDiscountGroupID = string.Empty;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Refresh();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 113);
          break;
        case 2:
          this.mClicked = false;
          break;
        case 3:
          this.OnDecisionRedrawGacha();
          break;
        case 10:
          this.CheckPrevGachaRequest();
          break;
        case 20:
          this.Refresh(false);
          if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.PrevGachaButton))
            break;
          this.PrevGachaButton.OnClick();
          break;
        case 21:
          this.PrevGachaButton = (GachaButton) null;
          break;
        case 111:
          this.OnDecideForTicketSelect();
          break;
        case 112:
          this.OnCancel();
          break;
        case 150:
          this.UpdateBGForce();
          break;
        case 160:
          this.OnClickDetail();
          break;
        case 170:
          this.OnClickDescription();
          break;
        case 180:
          this.SetVisiblaePreviews();
          this.mState.GotoState<GachaWindow.State_PauseState>();
          break;
      }
    }

    private bool IsGachaPending()
    {
      bool flag = false;
      string str = FlowNode_Variable.Get("REDRAW_GACHA_PENDING");
      if (!string.IsNullOrEmpty(str))
      {
        if (str != "1")
        {
          if (GachaResultData.IsPending)
            flag = true;
        }
        else
          flag = true;
      }
      return flag;
    }

    public GachaTopParamNew[] GetCurrentGachaLists(GachaWindow.GachaTabCategory category)
    {
      switch (category)
      {
        case GachaWindow.GachaTabCategory.RARE:
          return this.mGachaListRare.ToArray();
        case GachaWindow.GachaTabCategory.ARTIFACT:
          return this.mGachaListArtifact.ToArray();
        case GachaWindow.GachaTabCategory.TICKET:
          return this.mGachaListTicket.ToArray();
        case GachaWindow.GachaTabCategory.NORMAL:
          return this.mGachaListNormal.ToArray();
        case GachaWindow.GachaTabCategory.SPECIAL:
          if (this.mGachaListSpecials != null && this.mCurrentTabSPIndex >= 0 && this.mGachaListSpecials.Count > this.mCurrentTabSPIndex)
            return this.mGachaListSpecials[this.mCurrentTabSPIndex].lists.ToArray();
          break;
      }
      return (GachaTopParamNew[]) null;
    }

    public bool IsFreePause(GachaTopParamNew[] _list)
    {
      bool flag = false;
      for (int index = 0; index < _list.Length; ++index)
      {
        if (_list[index].IsFreePause)
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    private void Refresh(bool is_refresh_bg = true)
    {
      this.SetupGachaList(MonoSingleton<GameManager>.Instance.Gachas);
      this.SetupTabList();
      this.RefreshTabList();
      this.RefreshTabState(this.mCurrentTabIndex, this.mCurrentTabSPIndex);
      this.RefreshTabActive(true);
      this.RefreshDefaultPanel(is_refresh_bg);
      this.RefreshTicketButtonList();
      this.RefreshGachaState();
      this.RefreshSummonCoin();
      this.mClicked = false;
    }

    private void RefreshSummonCoin()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SummonCoin, (UnityEngine.Object) null))
        return;
      MonoSingleton<GameManager>.Instance.Player.UpdateEventCoin();
      DataSource.Bind<EventCoinData>(this.SummonCoin, MonoSingleton<GameManager>.Instance.Player.EventCoinList.Find((Predicate<EventCoinData>) (f => f.iname.Equals("IT_US_SUMMONS_01"))));
      GameParameter.UpdateAll(this.SummonCoin);
    }

    private void RefreshTicketButtonList()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      GachaWindow.\u003CRefreshTicketButtonList\u003Ec__AnonStorey8 listCAnonStorey8 = new GachaWindow.\u003CRefreshTicketButtonList\u003Ec__AnonStorey8();
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey8.\u0024this = this;
      if (this.mSelectTab != GachaWindow.GachaTabCategory.TICKET)
        return;
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey8.iname = this.m_CurrentGachaRequestParam.Iname;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (string.IsNullOrEmpty(listCAnonStorey8.iname) || !this.mTicketButtonLists.ContainsKey(listCAnonStorey8.iname))
        return;
      if (this.mGachaListTicket == null || this.mGachaListTicket.Count <= 0)
      {
        this.TicketNotListView.SetActive(true);
        // ISSUE: reference to a compiler-generated field
        this.mTicketButtonLists[listCAnonStorey8.iname].SetActive(false);
      }
      else
      {
        // ISSUE: reference to a compiler-generated method
        if (this.mGachaListTicket.FindIndex(new Predicate<GachaTopParamNew>(listCAnonStorey8.\u003C\u003Em__0)) >= 0)
          return;
        // ISSUE: reference to a compiler-generated field
        GameUtility.DestroyGameObject(this.mTicketButtonLists[listCAnonStorey8.iname]);
        // ISSUE: reference to a compiler-generated field
        this.mTicketButtonLists.Remove(listCAnonStorey8.iname);
        for (int index = 0; index < this.mGachaListTicket.Count; ++index)
        {
          if (this.mTicketButtonLists.ContainsKey(this.mGachaListTicket[index].iname))
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            GachaWindow.\u003CRefreshTicketButtonList\u003Ec__AnonStorey9 listCAnonStorey9 = new GachaWindow.\u003CRefreshTicketButtonList\u003Ec__AnonStorey9();
            // ISSUE: reference to a compiler-generated field
            listCAnonStorey9.\u003C\u003Ef__ref\u00248 = listCAnonStorey8;
            GameObject ticketButtonList = this.mTicketButtonLists[this.mGachaListTicket[index].iname];
            // ISSUE: reference to a compiler-generated field
            listCAnonStorey9.ticketlistitem = ticketButtonList.GetComponent<GachaTicketListItem>();
            // ISSUE: reference to a compiler-generated field
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) listCAnonStorey9.ticketlistitem, (UnityEngine.Object) null))
            {
              // ISSUE: reference to a compiler-generated field
              listCAnonStorey9.ticketlistitem.Refresh(this.mGachaListTicket[index], index);
              // ISSUE: reference to a compiler-generated field
              // ISSUE: method pointer
              listCAnonStorey9.ticketlistitem.SetGachaButtonEvent(new UnityAction((object) listCAnonStorey9, __methodptr(\u003C\u003Em__0)));
            }
          }
        }
      }
    }

    public void SetupGachaList(GachaParam[] gparms)
    {
      this.mGachaListRare.Clear();
      this.mGachaListNormal.Clear();
      this.mGachaListArtifact.Clear();
      this.mGachaListTicket.Clear();
      this.mGachaListSpecials.Clear();
      this.mGachaListAll.Clear();
      for (int index = 0; index < gparms.Length; ++index)
      {
        GachaTopParamNew gachaTopParamNew = new GachaTopParamNew();
        gachaTopParamNew.Deserialize(gparms[index]);
        if (gachaTopParamNew.category.Contains("coin"))
          this.mGachaListRare.Add(gachaTopParamNew);
        else if (gachaTopParamNew.category.Contains("gold"))
          this.mGachaListNormal.Add(gachaTopParamNew);
        else if (gachaTopParamNew.group.Contains("bugu-"))
          this.mGachaListArtifact.Add(gachaTopParamNew);
        else if (!string.IsNullOrEmpty(gachaTopParamNew.ticket_iname))
          this.mGachaListTicket.Add(gachaTopParamNew);
        else if (!string.IsNullOrEmpty(gachaTopParamNew.asset_bg) || !string.IsNullOrEmpty(gachaTopParamNew.asset_title))
        {
          string group = gachaTopParamNew.group;
          if (this.mGachaListSpecials != null && !string.IsNullOrEmpty(group) && this.mGachaListSpecials.FindIndex((Predicate<GachaWindow.GachaTopParamNewGroups>) (s => s.group == group)) != -1)
            this.mGachaListSpecials[this.mGachaListSpecials.FindIndex((Predicate<GachaWindow.GachaTopParamNewGroups>) (s => s.group == group))].lists.Add(gachaTopParamNew);
          else
            this.mGachaListSpecials.Add(new GachaWindow.GachaTopParamNewGroups()
            {
              lists = {
                gachaTopParamNew
              },
              group = group,
              tab_image = gachaTopParamNew.asset_title,
              bg_image = gachaTopParamNew.asset_bg
            });
        }
        else
          continue;
        this.mGachaListAll.Add(gachaTopParamNew);
      }
      if (this.mGachaListRare != null && this.mGachaListRare.Count > 1)
      {
        GachaTopParamNew[] collection = this.SortGachaList(this.mGachaListRare);
        this.mGachaListRare.Clear();
        this.mGachaListRare.AddRange((IEnumerable<GachaTopParamNew>) collection);
      }
      if (this.mGachaListNormal != null && this.mGachaListNormal.Count > 1)
        this.mGachaListNormal.Sort((Comparison<GachaTopParamNew>) ((a, b) => a.num - b.num));
      if (this.mGachaListSpecials == null || this.mGachaListSpecials.Count <= 0)
        return;
      foreach (GachaWindow.GachaTopParamNewGroups gachaListSpecial in this.mGachaListSpecials)
      {
        if (gachaListSpecial != null)
        {
          GachaTopParamNew[] collection = this.SortGachaList(gachaListSpecial.lists);
          gachaListSpecial.lists.Clear();
          gachaListSpecial.lists.AddRange((IEnumerable<GachaTopParamNew>) collection);
        }
      }
    }

    public GachaTopParamNew[] SortGachaList(List<GachaTopParamNew> _list)
    {
      List<GachaTopParamNew> gachaTopParamNewList = new List<GachaTopParamNew>();
      this.m_CacheList_Coin.Clear();
      this.m_CacheList_Gold.Clear();
      this.m_CacheList_CoinPaid.Clear();
      foreach (GachaTopParamNew gachaTopParamNew in _list)
      {
        if (gachaTopParamNew.CostType == GachaCostType.COIN)
          this.m_CacheList_Coin.Add(gachaTopParamNew);
        else if (gachaTopParamNew.CostType == GachaCostType.COIN_P)
          this.m_CacheList_CoinPaid.Add(gachaTopParamNew);
        else if (gachaTopParamNew.CostType == GachaCostType.GOLD)
          this.m_CacheList_Gold.Add(gachaTopParamNew);
      }
      this.m_CacheList_Coin.Sort((Comparison<GachaTopParamNew>) ((a, b) => a.num - b.num));
      this.m_CacheList_CoinPaid.Sort((Comparison<GachaTopParamNew>) ((a, b) => a.num - b.num));
      this.m_CacheList_Gold.Sort((Comparison<GachaTopParamNew>) ((a, b) => a.num - b.num));
      gachaTopParamNewList.AddRange((IEnumerable<GachaTopParamNew>) this.m_CacheList_Gold);
      gachaTopParamNewList.AddRange((IEnumerable<GachaTopParamNew>) this.m_CacheList_Coin);
      gachaTopParamNewList.AddRange((IEnumerable<GachaTopParamNew>) this.m_CacheList_CoinPaid);
      return gachaTopParamNewList.ToArray();
    }

    [DebuggerHidden]
    private IEnumerator LoadGachaBGTextures()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaWindow.\u003CLoadGachaBGTextures\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator LoadGachaTabSprites()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaWindow.\u003CLoadGachaTabSprites\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    private void ClearTabSprites()
    {
      if (this.mCacheTabImages == null || this.mCacheTabImages.Count <= 0)
        return;
      this.mCacheTabImages.Clear();
      this.mCacheTabImages = (Dictionary<string, Sprite>) null;
    }

    private void ClearBGSprites()
    {
      if (this.mCacheBGImages != null && this.mCacheBGImages.Count > 0)
      {
        this.mCacheBGImages.Clear();
        this.mCacheBGImages = (Dictionary<string, Texture2D>) null;
      }
      if (this.mCacheBackGroundImages == null || this.mCacheBackGroundImages.Count <= 0)
        return;
      this.mCacheBackGroundImages.Clear();
      this.mCacheBackGroundImages = (Dictionary<string, GachaWindow.BackGroundTextureGroup>) null;
    }

    private void SetupTabList()
    {
      for (int index = 0; index < this.mTabList.Count; ++index)
      {
        if (((UnityEngine.Object) this.mTabList[index]).name.IndexOf("sp") != -1)
          UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mTabList[index]).gameObject);
      }
      this.mTabList.Clear();
      this.mTabCategoryList.Clear();
      if (this.mGachaListSpecials != null && this.mGachaListSpecials.Count > 0)
      {
        for (int index = 0; index < this.mGachaListSpecials.Count; ++index)
        {
          GachaTopParamNew list = this.mGachaListSpecials[index].lists[0];
          SRPG_Button tab = UnityEngine.Object.Instantiate<SRPG_Button>(this.TabTemplate);
          this.mTabList.Add(tab);
          int tab_index = index;
          ((Component) tab).transform.SetParent(((Component) this.TabTemplate).transform.parent, false);
          ((UnityEngine.Object) tab).name = "sp" + (object) index;
          this.SetTabValue(tab, list, GachaWindow.GachaTabCategory.SPECIAL, tab_index, false);
          Image component1 = ((Component) tab).gameObject.GetComponent<Image>();
          string tabImage = this.mGachaListSpecials[index].tab_image;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null) && !string.IsNullOrEmpty(tabImage))
          {
            if (this.mCacheTabImages.ContainsKey(tabImage))
            {
              Sprite mCacheTabImage = this.mCacheTabImages[tabImage];
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) mCacheTabImage, (UnityEngine.Object) null))
                component1.sprite = mCacheTabImage;
            }
          }
          else
            component1.sprite = (Sprite) null;
          GachaTabListItem component2 = ((Component) tab).GetComponent<GachaTabListItem>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
          {
            component2.EndAt = this.mGachaListSpecials[index].lists[0].GetTimerAt();
            component2.Disabled = this.mGachaListSpecials[index].lists[0].disabled;
            component2.GachaStartAt = this.mGachaListSpecials[index].lists[0].startat;
            component2.GachaEndtAt = this.mGachaListSpecials[index].lists[0].endat;
            component2.ListIndex = tab_index;
          }
          ((Selectable) tab).interactable = !list.disabled;
        }
      }
      this.mTabList.Add(this.RareTab);
      this.SetTabValue(this.RareTab, this.mGachaListRare == null || this.mGachaListRare.Count <= 0 ? (GachaTopParamNew) null : this.mGachaListRare[0], GachaWindow.GachaTabCategory.RARE);
      if (this.IsFreePause(this.mGachaListRare.ToArray()))
      {
        BadgeValidator componentInChildren = ((Component) this.RareTab).GetComponentInChildren<BadgeValidator>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        {
          ((Behaviour) componentInChildren).enabled = false;
          ((Component) componentInChildren).gameObject.SetActive(false);
        }
      }
      this.mTabList.Add(this.TicketTab);
      this.SetTabValue(this.TicketTab, this.mGachaListTicket == null || this.mGachaListTicket.Count <= 0 ? (GachaTopParamNew) null : this.mGachaListTicket[0], GachaWindow.GachaTabCategory.TICKET);
      this.mTabList.Add(this.NormalTab);
      this.SetTabValue(this.NormalTab, this.mGachaListNormal == null || this.mGachaListNormal.Count <= 0 ? (GachaTopParamNew) null : this.mGachaListNormal[0], GachaWindow.GachaTabCategory.NORMAL);
    }

    private void SetTabValue(
      SRPG_Button tab,
      GachaTopParamNew param,
      GachaWindow.GachaTabCategory tab_category,
      int tab_index = -1,
      bool is_sibling = true)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      GachaWindow.\u003CSetTabValue\u003Ec__AnonStoreyB valueCAnonStoreyB = new GachaWindow.\u003CSetTabValue\u003Ec__AnonStoreyB();
      // ISSUE: reference to a compiler-generated field
      valueCAnonStoreyB.tab = tab;
      // ISSUE: reference to a compiler-generated field
      valueCAnonStoreyB.tab_category = tab_category;
      // ISSUE: reference to a compiler-generated field
      valueCAnonStoreyB.tab_index = tab_index;
      // ISSUE: reference to a compiler-generated field
      valueCAnonStoreyB.\u0024this = this;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) valueCAnonStoreyB.tab, (UnityEngine.Object) null) || valueCAnonStoreyB.tab_category == GachaWindow.GachaTabCategory.NONE)
        return;
      if (param != null)
      {
        // ISSUE: reference to a compiler-generated field
        DataSource.Bind<GachaTopParamNew>(((Component) valueCAnonStoreyB.tab).gameObject, param);
      }
      // ISSUE: reference to a compiler-generated field
      ((Component) valueCAnonStoreyB.tab).gameObject.SetActive(false);
      if (is_sibling)
      {
        // ISSUE: reference to a compiler-generated field
        ((Component) valueCAnonStoreyB.tab).transform.SetAsLastSibling();
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((UnityEvent) valueCAnonStoreyB.tab.onClick).AddListener(new UnityAction((object) valueCAnonStoreyB, __methodptr(\u003C\u003Em__0)));
      // ISSUE: reference to a compiler-generated field
      SerializeValueBehaviour component = ((Component) valueCAnonStoreyB.tab).GetComponent<SerializeValueBehaviour>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      // ISSUE: reference to a compiler-generated field
      component.list.SetField("category", (int) valueCAnonStoreyB.tab_category);
    }

    private GachaWindow.GachaTabCategory GetTabCategory(SRPG_Button tab)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) tab, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("召喚タブが指定されていませ.");
        return GachaWindow.GachaTabCategory.NONE;
      }
      GachaWindow.GachaTabCategory tabCategory = GachaWindow.GachaTabCategory.NONE;
      SerializeValueBehaviour component = ((Component) tab).GetComponent<SerializeValueBehaviour>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        tabCategory = (GachaWindow.GachaTabCategory) component.list.GetInt("category");
      return tabCategory;
    }

    private void RefreshTabList()
    {
      if (MonoSingleton<GameManager>.Instance.Player.CheckFreeGachaCoin())
      {
        int index = this.mTabList.FindIndex((Predicate<SRPG_Button>) (tab => UnityEngine.Object.op_Equality((UnityEngine.Object) tab, (UnityEngine.Object) this.RareTab)));
        if (index != -1)
        {
          this.mTabList.RemoveAt(index);
          this.mTabList.Insert(0, this.RareTab);
        }
        ((Component) this.RareTab).transform.SetAsFirstSibling();
      }
      else
      {
        int index1 = this.mTabList.FindIndex((Predicate<SRPG_Button>) (tab => UnityEngine.Object.op_Equality((UnityEngine.Object) tab, (UnityEngine.Object) this.RareTab)));
        if (index1 != -1)
        {
          this.mTabList.RemoveAt(index1);
          int index2 = this.mTabList.FindIndex((Predicate<SRPG_Button>) (tab => UnityEngine.Object.op_Equality((UnityEngine.Object) tab, (UnityEngine.Object) this.TicketTab)));
          if (index2 != -1)
            this.mTabList.Insert(index2, this.RareTab);
        }
        ((Component) this.RareTab).transform.SetAsLastSibling();
      }
      ((Component) this.TicketTab).transform.SetAsLastSibling();
      ((Component) this.NormalTab).transform.SetAsLastSibling();
    }

    private void OnTabChange(SRPG_Button button, GachaWindow.GachaTabCategory category, int index = -1)
    {
      if (!this.TabChange(button, category, index))
        return;
      this.RefreshGachaDetailSelectID(this.mSelectTab);
      this.mState.GotoState<GachaWindow.State_CheckInitState>();
    }

    private void RefreshTabState(int index, int sp_index)
    {
      if (index < 0 || index >= this.mTabList.Count)
      {
        index = 0;
        sp_index = 0;
      }
      GachaTopParamNew dataOfClass1 = DataSource.FindDataOfClass<GachaTopParamNew>(((Component) this.mTabList[index]).gameObject, (GachaTopParamNew) null);
      if (dataOfClass1 != null && dataOfClass1.disabled)
      {
        for (int index1 = 0; index1 < this.mTabList.Count; ++index1)
        {
          if (index1 != index)
          {
            SRPG_Button mTab = this.mTabList[index1];
            GachaTopParamNew dataOfClass2 = DataSource.FindDataOfClass<GachaTopParamNew>(((Component) mTab).gameObject, (GachaTopParamNew) null);
            if (dataOfClass2 != null && !dataOfClass2.disabled)
            {
              index = index1;
              GachaTabListItem component = ((Component) mTab).GetComponent<GachaTabListItem>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && component.ListIndex >= 0)
              {
                sp_index = component.ListIndex;
                break;
              }
              break;
            }
          }
        }
      }
      else if (dataOfClass1 == null)
      {
        index = Mathf.Max(0, index - 1);
        sp_index = Mathf.Max(0, sp_index - 1);
      }
      SRPG_Button[] array = this.mTabList.ToArray();
      for (int index2 = 0; index2 < array.Length; ++index2)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) array[index2], (UnityEngine.Object) null))
        {
          Transform transform = ((Component) array[index2]).transform.Find("cursor");
          if (index2 == index)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
              ((Component) transform).gameObject.SetActive(true);
            ((Component) array[index2]).transform.localScale = new Vector3(1f, 1f, 1f);
          }
          else
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
              ((Component) transform).gameObject.SetActive(false);
            ((Component) array[index2]).transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
          }
        }
      }
      this.mSelectTab = this.GetTabCategory(this.mTabList[index]);
      this.mCurrentTabIndex = Mathf.Max(index, 0);
      this.mCurrentTabSPIndex = this.mSelectTab == GachaWindow.GachaTabCategory.SPECIAL ? Mathf.Max(sp_index, 0) : -1;
      this.RefreshGachaDetailSelectID(this.mSelectTab);
    }

    private bool TabChange(SRPG_Button button, GachaWindow.GachaTabCategory category, int index = -1)
    {
      if (!((Selectable) button).IsInteractable())
        return false;
      SRPG_Button[] array = this.mTabList.ToArray();
      int num = Array.IndexOf<SRPG_Button>(array, button);
      if (category == GachaWindow.GachaTabCategory.SPECIAL)
      {
        if (this.mCurrentTabSPIndex == index)
          return false;
      }
      else if (this.mSelectTab == category)
        return false;
      for (int index1 = 0; index1 < array.Length; ++index1)
      {
        bool flag = index1 == num;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) array[index1], (UnityEngine.Object) null))
        {
          Transform transform = ((Component) array[index1]).transform.Find("cursor");
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
            ((Component) transform).gameObject.SetActive(flag);
        }
        if (flag)
        {
          ((Component) array[index1]).transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
          ((Component) array[index1]).transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
          GachaTabListItem component = ((Component) array[index1]).GetComponent<GachaTabListItem>();
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) || !component.Disabled)
            ((Behaviour) array[index1]).enabled = false;
        }
      }
      this.mSelectTab = category;
      this.mCurrentTabSPIndex = -1;
      if (category == GachaWindow.GachaTabCategory.SPECIAL)
        this.mCurrentTabSPIndex = index;
      bool flag1 = true;
      if (category == GachaWindow.GachaTabCategory.TICKET)
        flag1 = false;
      this.ResetChangeEffect();
      this.DefaultPanel.SetActive(flag1);
      this.ButtonPanel.SetActive(flag1);
      this.TicketPanel.SetActive(!flag1);
      if (this.DefaultPanel.activeInHierarchy)
        this.RefreshDefaultPanel();
      if (this.TicketPanel.activeInHierarchy)
      {
        this.RefreshGachaBackGround();
        this.SetTicketButtonList();
        GameUtility.SetGameObjectActive((Component) this.DetailButton, false);
        GameUtility.SetGameObjectActive((Component) this.PickupSelectButton, false);
      }
      this.IsTabChanging = true;
      this.mCurrentTabIndex = num;
      this.RefreshGachaDetailSelectID(this.mSelectTab);
      return true;
    }

    private void RefreshTabEnable(bool state)
    {
      foreach (SRPG_Button srpgButton in this.mTabList.ToArray())
      {
        GachaTabListItem component = ((Component) srpgButton).GetComponent<GachaTabListItem>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) || !component.Disabled)
          ((Behaviour) srpgButton).enabled = state;
      }
    }

    private void RefreshTabActive(bool value)
    {
      foreach (Component component in this.mTabList.ToArray())
        component.gameObject.SetActive(value);
    }

    private void SetTicketButtonList()
    {
      if (this.mGachaListTicket == null || this.mGachaListTicket.Count <= 0)
      {
        this.TicketNotListView.SetActive(true);
      }
      else
      {
        for (int index = 0; index < this.mGachaListTicket.Count; ++index)
        {
          if (!this.mTicketButtonLists.ContainsKey(this.mGachaListTicket[index].iname))
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            GachaWindow.\u003CSetTicketButtonList\u003Ec__AnonStoreyC listCAnonStoreyC = new GachaWindow.\u003CSetTicketButtonList\u003Ec__AnonStoreyC();
            // ISSUE: reference to a compiler-generated field
            listCAnonStoreyC.\u0024this = this;
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TicketButtonTemplate);
            // ISSUE: reference to a compiler-generated field
            listCAnonStoreyC.ticketlistitem = gameObject.GetComponent<GachaTicketListItem>();
            // ISSUE: reference to a compiler-generated field
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) listCAnonStoreyC.ticketlistitem, (UnityEngine.Object) null))
            {
              // ISSUE: reference to a compiler-generated field
              listCAnonStoreyC.ticketlistitem.Refresh(this.mGachaListTicket[index], index);
              // ISSUE: reference to a compiler-generated field
              // ISSUE: method pointer
              listCAnonStoreyC.ticketlistitem.SetGachaButtonEvent(new UnityAction((object) listCAnonStoreyC, __methodptr(\u003C\u003Em__0)));
              gameObject.transform.SetParent(this.TicketListRoot, false);
              gameObject.SetActive(true);
            }
            this.mTicketButtonLists.Add(this.mGachaListTicket[index].iname, gameObject);
          }
        }
      }
    }

    public GachaTopParamNew LastSelectTicketGacha { get; private set; }

    private void OnSelectTicket(int index)
    {
      GachaTopParamNew gachaTopParamNew = this.mGachaListTicket[index];
      this.LastSelectTicketGacha = gachaTopParamNew;
      this.OnExecGacha2(gachaTopParamNew);
    }

    private void RefreshDefaultPanel(bool is_refresh_bg = true)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitInfoPanel, (UnityEngine.Object) null))
        this.UnitInfoPanel.SetActive(this.mSelectTab == GachaWindow.GachaTabCategory.RARE);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactInfoPanel, (UnityEngine.Object) null))
        this.ArtifactInfoPanel.SetActive(this.mSelectTab == GachaWindow.GachaTabCategory.ARTIFACT);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanel, (UnityEngine.Object) null))
        this.BonusPanel.SetActive(this.mSelectTab == GachaWindow.GachaTabCategory.SPECIAL);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BGRoot, (UnityEngine.Object) null) && is_refresh_bg)
        this.RefreshGachaBackGround();
      this.RefreshUnitInfoPanel();
      this.RefreshArtifactInfoPanel();
      this.RefreshButtonPanel();
      this.RefreshPreviews();
    }

    private void RefreshUnitInfoPanel()
    {
      if (this.mSelectTab != GachaWindow.GachaTabCategory.RARE || this.mGachaListRare == null || this.mGachaListRare.Count <= 0)
        return;
      if (!string.IsNullOrEmpty(this.BGUnitImageID))
        this.mBGUnitImage = GameObjectID.FindGameObject<RawImage>(this.BGUnitImageID);
      this.CreatePickupUnitsList(this.mGachaListRare[0].units.ToArray());
      if (this.mPickupUnits == null || this.mPickupUnits.Count <= 0)
        return;
      this.mCurrentIndex = 0;
      this.mCurrentJobIndex = 0;
      this.RefreshUnitInfo();
    }

    private void RefreshUnitInfo()
    {
      this.UpdateCurrentUnitInfo();
      this.RefreshJobs();
      if (this.mPickupUnits != null && this.mPickupUnits.Count > 0)
      {
        DataSource.Bind<UnitData>(this.UnitInfoPanel, this.mPickupUnits[this.mCurrentIndex]);
        GameParameter.UpdateAll(this.UnitInfoPanel);
      }
      this.FadeUnitImage(0.0f, 0.0f, 0.0f);
      this.StartCoroutine(this.RefreshUnitImage());
      this.FadeUnitImage(0.0f, 1f, 1f);
      this.InitGachaUnitPreview();
      this.SetGachaPreviewCamera();
    }

    private void RefreshArtifactInfoPanel()
    {
      if (this.mSelectTab != GachaWindow.GachaTabCategory.ARTIFACT || this.mGachaListArtifact == null || this.mGachaListArtifact.Count <= 0)
        return;
      this.CreatePickupArtifactlist(this.mGachaListArtifact[0].artifacts.ToArray());
      if (this.mPickupArtifacts == null || this.mPickupArtifacts.Count <= 0)
        return;
      this.mCurrentPickupArtIndex = 0;
      this.RefreshArtifactInfo();
    }

    private void RefreshArtifactInfo()
    {
      this.UpdateCurrentArtifactInfo();
      if (this.mCurrentArtifact == null)
        return;
      List<AbilityData> learningAbilities = this.mCurrentArtifact.LearningAbilities;
      DataSource.Bind<AbilityData>(this.WeaponAbilityInfo, (AbilityData) null);
      if (learningAbilities != null && learningAbilities.Count > 0)
      {
        this.WeaponAbilityInfo.SetActive(true);
        DataSource.Bind<AbilityData>(this.WeaponAbilityInfo, learningAbilities[0]);
      }
      else
        this.WeaponAbilityInfo.SetActive(false);
      DataSource.Bind<ArtifactParam>(this.ArtifactInfoPanel, this.mCurrentArtifact.ArtifactParam);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactRarityPanel, (UnityEngine.Object) null))
        DataSource.Bind<ArtifactData>(this.ArtifactRarityPanel, this.mCurrentArtifact);
      GameParameter.UpdateAll(this.ArtifactInfoPanel);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Status, (UnityEngine.Object) null) && this.mCurrentArtifact != null)
      {
        BaseStatus fixed_status = new BaseStatus();
        BaseStatus scale_status = new BaseStatus();
        this.mCurrentArtifact.GetHomePassiveBuffStatus(ref fixed_status, ref scale_status);
        this.Status.SetValues(fixed_status, scale_status);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactType, (UnityEngine.Object) null) && this.mCurrentArtifact != null)
        this.ArtifactType.text = LocalizedText.Get("sys.TITLE_ARTIFACT_TYPE", (object) this.mCurrentArtifact.ArtifactParam.tag);
      this.InitGachaArtifactPreview();
      this.SetGachaPreviewArtifactCamera();
    }

    private void RefreshButtonPanel()
    {
      GachaTopParamNew[] currentGachaLists = this.GetCurrentGachaLists(this.mSelectTab);
      if (currentGachaLists == null || currentGachaLists.Length <= 0)
        return;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.GachaButtonTemplate, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("召喚ボタンのテンプレートが指定されていません.");
      }
      else
      {
        this.BonusPanel.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanelItem, (UnityEngine.Object) null))
          this.BonusPanelItem.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanelUnit, (UnityEngine.Object) null))
          this.BonusPanelUnit.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanelArtifact, (UnityEngine.Object) null))
          this.BonusPanelArtifact.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanelConceptCard, (UnityEngine.Object) null))
          this.BonusPanelConceptCard.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusMsgPanel, (UnityEngine.Object) null))
          this.BonusMsgPanel.SetActive(false);
        if (this.m_GachaButtons.Count < currentGachaLists.Length)
        {
          int num = currentGachaLists.Length - this.m_GachaButtons.Count;
          for (int index = 0; index < num; ++index)
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GachaButtonTemplate);
            gameObject.transform.SetParent(this.ButtonPanel.transform, false);
            this.m_GachaButtons.Add(gameObject.GetComponent<GachaButton>());
          }
        }
        foreach (Component gachaButton in this.m_GachaButtons)
          gachaButton.gameObject.SetActive(false);
        bool flag1 = false;
        bool flag2 = false;
        for (int index = 0; index < currentGachaLists.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          GachaWindow.\u003CRefreshButtonPanel\u003Ec__AnonStoreyD panelCAnonStoreyD = new GachaWindow.\u003CRefreshButtonPanel\u003Ec__AnonStoreyD();
          // ISSUE: reference to a compiler-generated field
          panelCAnonStoreyD.\u0024this = this;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStoreyD.gbtn = this.m_GachaButtons[index];
          // ISSUE: reference to a compiler-generated field
          panelCAnonStoreyD.param = currentGachaLists[index];
          // ISSUE: reference to a compiler-generated field
          flag1 = panelCAnonStoreyD.param.IsUseDiscount;
          // ISSUE: reference to a compiler-generated field
          flag2 = panelCAnonStoreyD.param.IsDiscount;
          // ISSUE: reference to a compiler-generated field
          GachaButtonParam gachaButtonParam = new GachaButtonParam(panelCAnonStoreyD.param);
          // ISSUE: reference to a compiler-generated field
          panelCAnonStoreyD.gbtn.SetupGachaButtonParam(gachaButtonParam);
          // ISSUE: reference to a compiler-generated field
          panelCAnonStoreyD.gbtn.UpdateTrigger = true;
          // ISSUE: reference to a compiler-generated field
          ((Component) panelCAnonStoreyD.gbtn).gameObject.SetActive(true);
          // ISSUE: reference to a compiler-generated field
          if (panelCAnonStoreyD.param.IsUsePickupSelect)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: method pointer
            panelCAnonStoreyD.gbtn.SetGachaButtonEvent(new UnityAction((object) panelCAnonStoreyD, __methodptr(\u003C\u003Em__0)));
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: method pointer
            panelCAnonStoreyD.gbtn.SetGachaButtonEvent(new UnityAction((object) panelCAnonStoreyD, __methodptr(\u003C\u003Em__1)));
          }
        }
        this.RefreshBonusParts();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DiscountButton, (UnityEngine.Object) null))
        {
          ((Selectable) this.DiscountButton).interactable = !flag2;
          ((Component) this.DiscountButton).gameObject.SetActive(flag1);
          ((Component) this.DiscountButton).transform.SetAsLastSibling();
        }
        GameUtility.SetGameObjectActive((Component) this.DetailButton, true);
        GameUtility.SetGameObjectActive((Component) this.PickupSelectButton, currentGachaLists[0].IsUsePickupSelect);
        GameUtility.SetGameObjectActive(this.PickupSelectAnimationObject, !currentGachaLists[0].IsSelectedPickup);
        GachaWindow.m_SelectDiscountGachaID = currentGachaLists[0].iname;
        GachaWindow.m_SelectDiscountGroupID = currentGachaLists[0].group;
      }
    }

    private void RefreshGachaDetailSelectID(GachaWindow.GachaTabCategory category)
    {
      FlowNode_Variable.Set("SHARED_WEBWINDOW_TITLE", LocalizedText.Get("sys.TITLE_POPUP_GACHA_DETAIL"));
      GachaTopParamNew[] currentGachaLists = this.GetCurrentGachaLists(category);
      if (currentGachaLists == null || currentGachaLists.Length <= 0)
        return;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Network.SiteHost);
      stringBuilder.Append(GachaWindow.GACHA_URL_PREFIX);
      stringBuilder.Append(currentGachaLists[0].detail_url);
      GachaWindow.mDetailURL = stringBuilder.ToString();
      FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", GachaWindow.mDetailURL);
      FlowNode_Variable.Set("SHARED_WEBWINDOW_URL2", GachaWindow.mDetailURL);
      this.DetailButtonEvent.GetEvent("TAP_GACHA_DETAIL")?.valueList.SetField("select_gachaid", currentGachaLists[0].iname);
    }

    private void OnClickDetail()
    {
      FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", GachaWindow.mDetailURL);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 999);
    }

    private void OnClickDescription()
    {
      FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", this.mDescriptionURL);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 999);
    }

    private void CreatePickupUnitsList(UnitParam[] units)
    {
      this.mPickupUnits = new List<UnitData>();
      for (int index = 0; index < units.Length; ++index)
      {
        UnitData unitData = this.CreateUnitData(units[index]);
        if (unitData != null)
          this.mPickupUnits.Add(unitData);
      }
    }

    private void CreatePickupArtifactlist(ArtifactParam[] artifacts)
    {
      this.mPickupArtifacts = new List<ArtifactData>();
      for (int index = 0; index < artifacts.Length; ++index)
      {
        ArtifactData artifactData = this.CreateArtifactData(artifacts[index]);
        if (artifacts != null)
          this.mPickupArtifacts.Add(artifactData);
      }
    }

    private void PlayChangeEffect()
    {
      if (this.mSelectTab != GachaWindow.GachaTabCategory.RARE && this.mSelectTab != GachaWindow.GachaTabCategory.ARTIFACT)
        return;
      Animator animator = (Animator) null;
      if (this.mSelectTab == GachaWindow.GachaTabCategory.RARE)
        animator = this.ChangeUnitEffectObj.GetComponent<Animator>();
      else if (this.mSelectTab == GachaWindow.GachaTabCategory.ARTIFACT)
        animator = this.ChangeArtifactEffectObj.GetComponent<Animator>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) animator, (UnityEngine.Object) null))
        return;
      animator.ResetTrigger("onResetTrigger");
      if (this.ChangeJob)
        animator.SetTrigger("onChangeJobF");
      else
        animator.SetTrigger("onChangeUnitF");
    }

    private void ResetChangeEffect()
    {
      Animator component1 = this.ChangeUnitEffectObj.GetComponent<Animator>();
      component1.ResetTrigger("onChangeJobF");
      component1.ResetTrigger("onChangeUnitF");
      component1.SetTrigger("onResetTrigger");
      Animator component2 = this.ChangeArtifactEffectObj.GetComponent<Animator>();
      component2.ResetTrigger("onChangeJobF");
      component2.ResetTrigger("onChangeUnitF");
      component2.SetTrigger("onResetTrigger");
    }

    private void RefreshGachaState()
    {
      if (this.mSelectTab == GachaWindow.GachaTabCategory.RARE)
      {
        this.mCurrentIndex = this.mCurrentJobIndex = 0;
        this.UpdateCurrentUnitInfo();
        this.mState.GotoState<GachaWindow.State_WaitActionAnimation>();
      }
      else
      {
        if (this.mSelectTab != GachaWindow.GachaTabCategory.ARTIFACT)
          return;
        bool flag = false;
        this.ChangeJob = flag;
        this.ChangeUnit = flag;
        this.mState.GotoState<GachaWindow.State_WaitActionAnimation>();
      }
    }

    private void RefreshGachaBackGround()
    {
      if (this.mSelectTab == GachaWindow.GachaTabCategory.TICKET)
        GameUtility.SetGameObjectActive((Component) this.BGRoot, false);
      else
        this.RefreshBackGround();
    }

    private void RefreshGachaBGImage(string image)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mDefaultBG, (UnityEngine.Object) null) && this.mCacheBGImages != null && this.mCacheBGImages.Count > 0)
      {
        DebugUtility.Log("=== GachaWindow.cs->RefreshGachaBGImage:mDefaultBG Initalize");
        this.mDefaultBG = !this.mCacheBGImages.ContainsKey(GachaWindow.DEFAULT_BGIMAGE_PATH) ? (Texture2D) null : this.mCacheBGImages[GachaWindow.DEFAULT_BGIMAGE_PATH];
      }
      Transform bg00 = this.mBGObjects == null || this.mBGObjects.Count < 2 ? this.BGRoot.Find("bg00") : this.mBGObjects[0];
      Transform bg01 = this.mBGObjects == null || this.mBGObjects.Count < 2 ? this.BGRoot.Find("bg01") : this.mBGObjects[1];
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) bg00, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) bg01, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("=== GachaWindow.cs->RefreshGachaBGImage2():bg00 or bg01 is Null ===");
        this.IsRefreshingGachaBG = false;
      }
      else
      {
        RawImage component1 = ((Component) bg00).GetComponent<RawImage>();
        RawImage component2 = ((Component) bg01).GetComponent<RawImage>();
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component1, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) component2, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("=== GachaWindow.cs->RefreshGachaBGImage2():bg00_raw or bg01_raw is Null ===");
          this.IsRefreshingGachaBG = false;
        }
        else
        {
          CanvasGroup component3 = ((Component) bg00).gameObject.GetComponent<CanvasGroup>();
          CanvasGroup component4 = ((Component) bg01).gameObject.GetComponent<CanvasGroup>();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) component3, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) component4, (UnityEngine.Object) null))
          {
            DebugUtility.LogError("=== GachaWindow.cs->RefreshGachaBGImage2():canvas00 or canvas01 is Null ===");
            this.IsRefreshingGachaBG = false;
          }
          else
          {
            if (this.mBGObjects.FindIndex((Predicate<Transform>) (s => UnityEngine.Object.op_Equality((UnityEngine.Object) ((Component) s).gameObject, (UnityEngine.Object) ((Component) bg00).gameObject))) == -1)
              this.mBGObjects.Add(bg00);
            if (this.mBGObjects.FindIndex((Predicate<Transform>) (s => UnityEngine.Object.op_Equality((UnityEngine.Object) ((Component) s).gameObject, (UnityEngine.Object) ((Component) bg01).gameObject))) == -1)
              this.mBGObjects.Add(bg01);
            if (this.mCacheBGImages != null && this.mCacheBGImages.Count > 0 && !string.IsNullOrEmpty(image))
            {
              Texture2D mDefaultBg = this.mDefaultBG;
              Texture2D texture2D1 = (Texture2D) null;
              Texture2D texture2D2 = !this.mCacheBGImages.ContainsKey(image + "_0") ? mDefaultBg : this.mCacheBGImages[image + "_0"];
              Texture2D texture2D3 = !this.mCacheBGImages.ContainsKey(image + "_1") ? texture2D1 : this.mCacheBGImages[image + "_1"];
              component1.texture = (Texture) texture2D2;
              component2.texture = (Texture) texture2D3;
            }
            else
            {
              component1.texture = (Texture) this.mDefaultBG;
              component2.texture = (Texture) null;
            }
            component3.alpha = 1f;
            component4.alpha = 0.0f;
            this.mExistSwapBG = UnityEngine.Object.op_Inequality((UnityEngine.Object) component2.texture, (UnityEngine.Object) null);
            this.mWaitSwapBGTime = GachaWindow.WAIT_SWAP_BG;
            this.mEnableBGIndex = 0;
            this.IsRefreshingGachaBG = false;
          }
        }
      }
    }

    private void UpdateBG()
    {
      if (!this.mExistSwapBG)
        return;
      this.mWaitSwapBGTime -= Time.deltaTime;
      if ((double) this.mWaitSwapBGTime >= 0.0)
        return;
      Transform transform1 = this.mBGObjects != null || this.mBGObjects.Count < 2 || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBGObjects[0], (UnityEngine.Object) null) ? this.BGRoot.Find("bg00") : this.mBGObjects[0];
      Transform transform2 = this.mBGObjects != null || this.mBGObjects.Count < 2 || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBGObjects[1], (UnityEngine.Object) null) ? this.BGRoot.Find("bg01") : this.mBGObjects[1];
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) transform1, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) transform2, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("=== GachaWindow->UpdateBG:bg00 or bg01 is null ===");
      }
      else
      {
        RawImage component1 = ((Component) transform1).GetComponent<RawImage>();
        RawImage component2 = ((Component) transform2).GetComponent<RawImage>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("=== GachaWindow->UpdateBG:bg00_image & bg01_image is null ===");
        }
        else
        {
          CanvasGroup component3 = ((Component) component1).gameObject.GetComponent<CanvasGroup>();
          CanvasGroup component4 = ((Component) component2).gameObject.GetComponent<CanvasGroup>();
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component3, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) component4, (UnityEngine.Object) null))
          {
            DebugUtility.LogError("=== GachaWindow->UpdateBG:canvas00 & canvas01 is null ===");
          }
          else
          {
            float deltaTime = Time.deltaTime;
            component3.alpha += this.mEnableBGIndex != 0 ? deltaTime : -deltaTime;
            component4.alpha += this.mEnableBGIndex != 0 ? -deltaTime : deltaTime;
            component3.alpha = Mathf.Clamp(component3.alpha + (this.mEnableBGIndex != 0 ? deltaTime : -deltaTime), 0.0f, 1f);
            component4.alpha = Mathf.Clamp(component4.alpha + (this.mEnableBGIndex != 0 ? -deltaTime : deltaTime), 0.0f, 1f);
            if ((double) component3.alpha > 0.0 && (double) component3.alpha < 1.0)
              return;
            this.mEnableBGIndex ^= 1;
            this.mWaitSwapBGTime = GachaWindow.WAIT_SWAP_BG;
          }
        }
      }
    }

    private void UpdateBGForce()
    {
      if (!this.mExistSwapBG)
        return;
      this.mWaitSwapBGTime = 0.0f;
    }

    private void RefreshBackGround()
    {
      GachaTopParamNew[] currentGachaLists = this.GetCurrentGachaLists(this.mSelectTab);
      if (currentGachaLists == null || currentGachaLists.Length <= 0)
      {
        DebugUtility.LogWarning("指定された召喚スケジュールがありません.");
      }
      else
      {
        string assetBg = currentGachaLists[0].asset_bg;
        this.IsRefreshingGachaBG = true;
        if (string.IsNullOrEmpty(assetBg))
        {
          DebugUtility.LogWarning("背景画像名が指定されていません.");
          this.IsRefreshingGachaBG = false;
          GameUtility.SetGameObjectActive((Component) this.BGRoot, false);
        }
        else if (this.mCacheBackGroundImages != null && this.mCacheBackGroundImages.Count > 0)
        {
          if (this.mCacheBackGroundImages.ContainsKey(assetBg))
            this.SetBackGroundImage(this.mCacheBackGroundImages[assetBg]);
          else
            this.StartCoroutine(this.SetBackGroundImageAsync(assetBg));
        }
        else
          this.StartCoroutine(this.SetBackGroundImageAsync(assetBg));
      }
    }

    private void SetBackGroundImage(GachaWindow.BackGroundTextureGroup bg_group)
    {
      if (bg_group != null && bg_group.IsEnableData)
      {
        GachaWindow.BackGroundTexture background1 = bg_group.backgrounds[0];
        this.BG01.BackGroundLeft.texture = (Texture) background1.left;
        this.BG01.BackGroundRight.texture = (Texture) background1.right;
        GachaWindow.BackGroundTexture background2 = bg_group.backgrounds.Length < 2 ? (GachaWindow.BackGroundTexture) null : bg_group.backgrounds[1];
        this.BG02.BackGroundLeft.texture = background2 == null ? (Texture) null : (Texture) background2.left;
        this.BG02.BackGroundRight.texture = background2 == null ? (Texture) null : (Texture) background2.right;
        this.BG01.RootCanvasGroup.alpha = 1f;
        this.BG02.RootCanvasGroup.alpha = 0.0f;
        this.mExistSwapBG = this.BG02.IsTextureSet;
        this.mWaitSwapBGTime = GachaWindow.WAIT_SWAP_BG;
        this.mEnableBGIndex = 0;
        GameUtility.SetGameObjectActive((Component) this.BGRoot, true);
      }
      this.IsRefreshingGachaBG = false;
    }

    [DebuggerHidden]
    private IEnumerator SetBackGroundImageAsync(string iname)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaWindow.\u003CSetBackGroundImageAsync\u003Ec__Iterator2()
      {
        iname = iname,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator LoadBackGroundTextureForBGTables(string[] inames)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaWindow.\u003CLoadBackGroundTextureForBGTables\u003Ec__Iterator3()
      {
        inames = inames,
        \u0024this = this
      };
    }

    private GachaWindow.BackGroundTexture GetBackGroundTexture(Texture2D[] textures, string iname)
    {
      if (textures == null || string.IsNullOrEmpty(iname))
        return (GachaWindow.BackGroundTexture) null;
      Texture2D texture2D1 = (Texture2D) null;
      Texture2D texture2D2 = (Texture2D) null;
      int index1 = Array.FindIndex<Texture2D>(textures, (Predicate<Texture2D>) (t => ((UnityEngine.Object) t).name == iname + this.BGTEXTURE_SUFFIX_LEFT));
      if (index1 >= 0)
        texture2D1 = textures[index1];
      int index2 = Array.FindIndex<Texture2D>(textures, (Predicate<Texture2D>) (t => ((UnityEngine.Object) t).name == iname + this.BGTEXTURE_SUFFIX_RIGHT));
      if (index2 >= 0)
        texture2D2 = textures[index2];
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) texture2D1, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) texture2D2, (UnityEngine.Object) null))
        return (GachaWindow.BackGroundTexture) null;
      return new GachaWindow.BackGroundTexture()
      {
        left = texture2D1,
        right = texture2D2
      };
    }

    private void UpdateBackGround()
    {
      if (!this.mExistSwapBG)
        return;
      this.mWaitSwapBGTime -= Time.deltaTime;
      if ((double) this.mWaitSwapBGTime >= 0.0 || this.BG01 == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.BG01.RootCanvasGroup, (UnityEngine.Object) null) || this.BG02 == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.BG02.RootCanvasGroup, (UnityEngine.Object) null))
        return;
      float deltaTime = Time.deltaTime;
      this.BG01.RootCanvasGroup.alpha += this.mEnableBGIndex != 0 ? deltaTime : -deltaTime;
      this.BG02.RootCanvasGroup.alpha += this.mEnableBGIndex != 0 ? -deltaTime : deltaTime;
      this.BG01.RootCanvasGroup.alpha = Mathf.Clamp(this.BG01.RootCanvasGroup.alpha + (this.mEnableBGIndex != 0 ? deltaTime : -deltaTime), 0.0f, 1f);
      this.BG02.RootCanvasGroup.alpha = Mathf.Clamp(this.BG02.RootCanvasGroup.alpha + (this.mEnableBGIndex != 0 ? -deltaTime : deltaTime), 0.0f, 1f);
      if ((double) this.BG01.RootCanvasGroup.alpha > 0.0 && (double) this.BG01.RootCanvasGroup.alpha < 1.0)
        return;
      this.mEnableBGIndex ^= 1;
      this.mWaitSwapBGTime = GachaWindow.WAIT_SWAP_BG;
    }

    [DebuggerHidden]
    private IEnumerator LoadBackGroundTextures(string[] inames)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaWindow.\u003CLoadBackGroundTextures\u003Ec__Iterator4()
      {
        inames = inames,
        \u0024this = this
      };
    }

    private void RefreshBonusParts()
    {
      GachaTopParamNew[] currentGachaLists = this.GetCurrentGachaLists(this.mSelectTab);
      if (currentGachaLists == null || currentGachaLists.Length <= 0)
        return;
      GachaTopParamNew gachaTopParamNew = (GachaTopParamNew) null;
      for (int index = 0; index < currentGachaLists.Length; ++index)
      {
        if (currentGachaLists[index] != null)
          gachaTopParamNew = currentGachaLists[index];
      }
      if (gachaTopParamNew == null)
        return;
      bool flag1 = false;
      bool flag2 = false;
      if (this.OptionPartsObject != null)
        this.OptionPartsObject.Refresh(gachaTopParamNew);
      this.ResetBonusObject();
      if (!string.IsNullOrEmpty(gachaTopParamNew.bonus_msg))
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusText, (UnityEngine.Object) null))
        {
          this.BonusText.text = gachaTopParamNew.bonus_msg.Replace("<br>", "\n");
          flag1 = true;
        }
      }
      else if (gachaTopParamNew.bonus_items != null && gachaTopParamNew.bonus_items.Count > 0)
      {
        this.RefreshBonusObject(gachaTopParamNew.bonus_items.ToArray());
        flag1 = true;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealText, (UnityEngine.Object) null))
      {
        flag2 = !string.IsNullOrEmpty(gachaTopParamNew.appeal_message);
        this.AppealText.text = gachaTopParamNew.appeal_message;
        ((Component) this.AppealText).gameObject.SetActive(flag2);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusParts, (UnityEngine.Object) null) || !flag1 && !flag2)
        return;
      this.BonusParts.transform.SetAsLastSibling();
      this.BonusParts.SetActive(true);
    }

    private void ResetBonusObject()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusText, (UnityEngine.Object) null))
      {
        this.BonusText.text = string.Empty;
        ((Component) this.BonusText).gameObject.SetActive(false);
      }
      if (this.BonusIcons == null || this.BonusIcons.Length <= 0)
        return;
      for (int index = 0; index < this.BonusIcons.Length; ++index)
      {
        if (this.BonusIcons[index] != null)
          this.BonusIcons[index].Reset();
      }
    }

    private void RefreshBonusObject(GachaBonusParam[] bonus)
    {
      if (bonus == null || bonus.Length <= 0)
        return;
      string str1 = string.Empty;
      for (int index = 0; index < bonus.Length && this.BonusIcons != null && this.BonusIcons.Length > 0 && this.BonusIcons.Length > index; ++index)
      {
        if (bonus[index] != null && !string.IsNullOrEmpty(bonus[index].iname))
        {
          string str2 = bonus[index].iname.Split('_')[0];
          switch (str2)
          {
            case "IT":
              ItemParam itemParam1 = MonoSingleton<GameManager>.Instance.GetItemParam(bonus[index].iname);
              if (itemParam1 != null)
              {
                if (index == 0)
                  str1 = itemParam1.name + "x" + bonus[index].num.ToString();
                if (this.BonusIcons[index] != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusIcons[index].Root, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusIcons[index].ItemIcon, (UnityEngine.Object) null))
                {
                  DataSource.Bind<ItemParam>(this.BonusIcons[index].ItemIcon, itemParam1, true);
                  this.BonusIcons[index].ItemIcon.SetActive(true);
                  this.BonusIcons[index].Root.SetActive(true);
                  continue;
                }
                continue;
              }
              continue;
            case "UN":
              UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(bonus[index].iname);
              if (unitParam != null)
              {
                UnitData unitDataForDisplay = UnitData.CreateUnitDataForDisplay(unitParam.iname);
                if (unitDataForDisplay != null)
                {
                  if (index == 0)
                    str1 = unitParam.name;
                  if (this.BonusIcons[index] != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusIcons[index].Root, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusIcons[index].UnitIcon, (UnityEngine.Object) null))
                  {
                    DataSource.Bind<UnitData>(this.BonusIcons[index].UnitIcon, unitDataForDisplay, true);
                    this.BonusIcons[index].UnitIcon.SetActive(true);
                    this.BonusIcons[index].Root.SetActive(true);
                    continue;
                  }
                  continue;
                }
                continue;
              }
              continue;
            case "AF":
              ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(bonus[index].iname);
              if (artifactParam != null)
              {
                if (index == 0)
                  str1 = artifactParam.name + "x" + bonus[index].num.ToString();
                if (this.BonusIcons[index] != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusIcons[index].Root, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusIcons[index].ArtifactIcon, (UnityEngine.Object) null))
                {
                  DataSource.Bind<ArtifactParam>(this.BonusIcons[index].ArtifactIcon, artifactParam, true);
                  ArtifactIcon component = this.BonusIcons[index].ArtifactIcon.GetComponent<ArtifactIcon>();
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                    component.UpdateValue();
                  this.BonusIcons[index].ArtifactIcon.SetActive(true);
                  this.BonusIcons[index].Root.SetActive(true);
                  continue;
                }
                continue;
              }
              continue;
            case "TS":
              ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(bonus[index].iname);
              if (cardDataForDisplay != null && cardDataForDisplay.Param != null)
              {
                if (index == 0)
                  str1 = cardDataForDisplay.Param.name + "x" + bonus[index].num.ToString();
                if (this.BonusIcons[index] != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusIcons[index].Root, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusIcons[index].ConceptCardIcon, (UnityEngine.Object) null))
                {
                  ConceptCardIcon component = this.BonusIcons[index].ConceptCardIcon.GetComponent<ConceptCardIcon>();
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                    component.Setup(cardDataForDisplay);
                  this.BonusIcons[index].ConceptCardIcon.SetActive(true);
                  this.BonusIcons[index].Root.SetActive(true);
                  continue;
                }
                continue;
              }
              continue;
            default:
              if (str2 == "$COIN")
              {
                ItemParam itemParam2 = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam("$COIN");
                if (itemParam2 != null)
                {
                  if (index == 0)
                    str1 = itemParam2.name + "x" + bonus[index].num.ToString();
                  if (this.BonusIcons[index] != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusIcons[index].Root, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusIcons[index].ItemIcon, (UnityEngine.Object) null))
                  {
                    DataSource.Bind<ItemParam>(this.BonusIcons[index].ItemIcon, itemParam2, true);
                    this.BonusIcons[index].ItemIcon.SetActive(true);
                    this.BonusIcons[index].Root.SetActive(true);
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
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusText, (UnityEngine.Object) null))
        return;
      UnityEngine.UI.Text bonusText = this.BonusText;
      string str3;
      if (bonus.Length > 1)
        str3 = LocalizedText.Get("sys.TEXT_GACHA_BOUNS_ITEMS", (object) str1.ToString());
      else
        str3 = str1.ToString();
      bonusText.text = str3;
      ((Component) this.BonusText).gameObject.SetActive(!string.IsNullOrEmpty(this.BonusText.text));
    }

    private void InitGachaUnitPreview(bool reload = true)
    {
      if (!string.IsNullOrEmpty(this.PickupPreviewParentID))
        this.mPreviewParent = GameObjectID.FindGameObject<Transform>(this.PickupPreviewParentID);
      if (!string.IsNullOrEmpty(this.PickUpPreviewBaseID))
      {
        this.mPreviewBase = GameObjectID.FindGameObject(this.PickUpPreviewBaseID);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewBase, (UnityEngine.Object) null))
          this.mPreviewBase.SetActive(false);
      }
      if (!string.IsNullOrEmpty(this.PickUpCameraID))
        this.mPreviewCamera = GameObjectID.FindGameObject<Camera>(this.PickUpCameraID);
      if (!reload)
        return;
      this.ReloadPickUpUnitView();
    }

    private void InitGachaArtifactPreview(bool reload = true)
    {
      if (!string.IsNullOrEmpty(this.PickupPreviewArtifact))
        this.mPreviewArtifact = GameObjectID.FindGameObject<Transform>(this.PickupPreviewArtifact);
      if (!string.IsNullOrEmpty(this.PickUpCameraID))
        this.mPreviewCamera = GameObjectID.FindGameObject<Camera>(this.PickUpCameraID);
      if (!reload)
        return;
      this.StartCoroutine(this.ReloadPickupArtifactPreview());
    }

    [DebuggerHidden]
    private IEnumerator ReloadPickupArtifactPreview(bool reload = true)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaWindow.\u003CReloadPickupArtifactPreview\u003Ec__Iterator5()
      {
        \u0024this = this
      };
    }

    private void ReloadPickUpUnitView()
    {
      if (this.CurrentUnit == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mPreviewParent, (UnityEngine.Object) null))
        return;
      GameUtility.DestroyGameObjects<GachaUnitPreview>(this.mPreviewControllers);
      this.mPreviewControllers.Clear();
      this.mCurrentPreview = (GachaUnitPreview) null;
      for (int index = 0; index < this.mCurrentUnit.UnitParam.jobsets.Length; ++index)
      {
        GachaUnitPreview gachaUnitPreview = (GachaUnitPreview) null;
        if (this.mCurrentUnit.Jobs[index] != null && this.mCurrentUnit.Jobs[index].Param != null)
        {
          GameObject gameObject = new GameObject("Preview", new System.Type[1]
          {
            typeof (GachaUnitPreview)
          });
          gachaUnitPreview = gameObject.GetComponent<GachaUnitPreview>();
          gachaUnitPreview.DefaultLayer = GameUtility.LayerHidden;
          gachaUnitPreview.SetGachaUnitData(this.mCurrentUnit, this.mCurrentUnit.Jobs[index].JobID);
          gachaUnitPreview.SetupUnit(this.mCurrentUnit.UnitParam.iname, this.mCurrentUnit.Jobs[index].JobID);
          gameObject.transform.SetParent(this.mPreviewParent, false);
          if (index == this.mCurrentUnit.JobIndex)
            this.mCurrentPreview = gachaUnitPreview;
        }
        this.mPreviewControllers.Add(gachaUnitPreview);
      }
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCurrentPreview, (UnityEngine.Object) null))
        return;
      GameObject gameObject1 = new GameObject("Preview", new System.Type[1]
      {
        typeof (GachaUnitPreview)
      });
      this.mCurrentPreview = gameObject1.GetComponent<GachaUnitPreview>();
      this.mCurrentPreview.DefaultLayer = GameUtility.LayerHidden;
      this.mCurrentPreview.SetupUnit(this.mCurrentUnit);
      gameObject1.transform.SetParent(this.mPreviewParent, false);
      this.mPreviewControllers.Add(this.mCurrentPreview);
    }

    private void UpdateCurrentUnitInfo()
    {
      if (this.mPickupUnits.Count > 0)
      {
        this.mCurrentUnit = this.mPickupUnits[this.mCurrentIndex];
        this.mCurrentUnit.SetJobIndex(this.mCurrentJobIndex);
      }
      else
        Debug.LogError((object) "mPickupUnits.unitsがNullもしくはCount=0です.");
    }

    private void UpdateCurrentArtifactInfo()
    {
      if (this.mPickupArtifacts.Count <= 0)
        return;
      this.mCurrentArtifact = this.mPickupArtifacts[this.mCurrentPickupArtIndex];
    }

    private void ChangePreviewUnit()
    {
      ++this.mCurrentIndex;
      if (this.mCurrentIndex <= this.mPickupUnits.Count - 1)
        return;
      this.mCurrentIndex = 0;
    }

    private void ChangePreviewArtifact()
    {
      ++this.mCurrentPickupArtIndex;
      if (this.mCurrentPickupArtIndex <= this.mPickupArtifacts.Count - 1)
        return;
      this.mCurrentPickupArtIndex = 0;
    }

    private void RefreshPreviews()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewBase, (UnityEngine.Object) null) && !this.mPreviewBase.activeSelf)
      {
        GameUtility.SetLayer(this.mPreviewBase, GameUtility.LayerCH, true);
        this.mPreviewBase.SetActive(true);
      }
      this.mDesiredPreviewVisibility = true;
      this.mUpdatePreviewVisibility = true;
    }

    private void SetActivePreview(int index)
    {
      GachaUnitPreview previewController = this.mPreviewControllers[index];
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) previewController, (UnityEngine.Object) this.mCurrentPreview))
        return;
      GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerHidden, true);
      GameUtility.SetLayer((Component) previewController, GameUtility.LayerCH, true);
      this.mCurrentPreview = previewController;
    }

    private void SetVisiblaePreviews(bool visible = true)
    {
      if (this.mPreviewControllers != null && this.mPreviewControllers.Count > 0)
      {
        for (int index = 0; index < this.mPreviewControllers.Count; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewControllers[index], (UnityEngine.Object) null))
            GameUtility.SetLayer((Component) this.mPreviewControllers[index], !visible ? GameUtility.LayerCH : GameUtility.LayerHidden, true);
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewBase, (UnityEngine.Object) null))
        return;
      GameUtility.SetLayer(this.mPreviewBase, !visible ? GameUtility.LayerCH : GameUtility.LayerHidden, true);
    }

    private void SetActivePreviewArtifact(int index)
    {
      if (this.mPreviewArtifactControllers.Count <= index)
        return;
      Transform artifactController = this.mPreviewArtifactControllers[index];
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) artifactController, (UnityEngine.Object) null))
        return;
      GameUtility.SetLayer((Component) this.mCurrentArtifactPreview, GameUtility.LayerHidden, true);
      GameUtility.SetLayer((Component) artifactController, GameUtility.LayerCH, true);
      this.mCurrentArtifactPreview = artifactController;
    }

    private bool SelectJob()
    {
      ++this.mCurrentJobIndex;
      if (this.mCurrentJobIndex >= this.mCurrentUnit.NumJobsAvailable)
      {
        this.mChangeJob = false;
        this.mCurrentJobIndex = 0;
        return false;
      }
      this.mChangeJob = true;
      return true;
    }

    private void RefreshJobs()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.SelectJobIcon, (UnityEngine.Object) null))
        return;
      this.SelectJobIcon.GetComponent<GameParameter>().Index = this.mCurrentJobIndex;
    }

    [DebuggerHidden]
    private IEnumerator CreateConfirm(GachaRequestParam _param, bool _is_coin_status = true)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaWindow.\u003CCreateConfirm\u003Ec__Iterator6()
      {
        _param = _param,
        \u0024this = this
      };
    }

    private void CreateGachaConfirmWindow(
      string confirm,
      int cost,
      string ticket = "",
      GachaCostType type = GachaCostType.NONE,
      bool isShowCoinStatus = true)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.GachaConfirmWindow, (UnityEngine.Object) null))
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GachaConfirmWindow);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      SRPG.GachaConfirmWindow component = gameObject.GetComponent<SRPG.GachaConfirmWindow>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.ConfirmText = confirm;
      component.Cost = cost;
      component.UseTicket = ticket;
      component.GachaCostType = type;
      component.IsShowCoinStatus = isShowCoinStatus;
      component.OnDecide = new SRPG.GachaConfirmWindow.DecideEvent(this.OnDecide);
      component.OnCancel = new SRPG.GachaConfirmWindow.CancelEvent(this.OnCancel);
      DataSource.Bind<ItemParam>(gameObject, (ItemParam) null);
      if (string.IsNullOrEmpty(ticket))
        return;
      ItemParam itemParam = MonoSingleton<GameManager>.GetInstanceDirect().GetItemParam(ticket);
      if (itemParam == null)
        return;
      DataSource.Bind<ItemParam>(gameObject, itemParam);
    }

    private void OnExecGachaNotSelectPickup(GachaButton btn, GachaTopParamNew param)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) btn, (UnityEngine.Object) null) || param == null)
        return;
      if (param.IsUsePickupSelect && !param.IsSelectedPickup)
      {
        this.PrevGachaButton = btn;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 210);
      }
      else
      {
        this.PrevGachaButton = (GachaButton) null;
        this.OnExecGacha2(param);
      }
    }

    private void OnExecGacha2(GachaTopParamNew param)
    {
      DataSource.Bind<GachaRequestParam>(((Component) this.RootObject.parent).gameObject, (GachaRequestParam) null);
      GachaRequestParam gachaRequest = this.CreateGachaRequest(param);
      this.m_CurrentGachaRequestParam = gachaRequest;
      if (gachaRequest.CostType == GachaCostType.GOLD || gachaRequest.IsFree || gachaRequest.IsDailyFree)
        this.OnDecide();
      else if (gachaRequest.IsTicketGacha)
      {
        FlowNode_Variable.Set("USE_TICKET_INAME", gachaRequest.Ticket);
        FlowNode_Variable.Set("USE_TICKET_MAX", !gachaRequest.IsRedrawGacha ? string.Empty : "1");
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
      }
      else
        this.StartCoroutine(this.CreateConfirm(gachaRequest));
    }

    private void OnExecGacha2(
      string _iname,
      int _cost,
      string _category,
      string _ticket,
      int _num,
      string _confirm,
      GachaCostType _cost_type,
      bool _is_use_onemore = false,
      bool _is_no_use_free = false,
      int _redraw_rest = 0,
      int _redraw_num = 0,
      int _discount = -1)
    {
      if (!this.Initialized || this.Clicked)
        return;
      this.mClicked = true;
      DataSource.Bind<GachaRequestParam>(((Component) this.RootObject.parent).gameObject, (GachaRequestParam) null);
      GachaRequestParam gachaRequest = this.CreateGachaRequest(_iname, _cost, _category, _ticket, _num, _confirm, _cost_type, _is_use_onemore, _is_no_use_free, _redraw_rest, _redraw_num, _discount);
      this.m_CurrentGachaRequestParam = gachaRequest;
      if (_cost_type == GachaCostType.GOLD || gachaRequest.IsFree)
        this.OnDecide();
      else if (gachaRequest.IsTicketGacha)
      {
        FlowNode_Variable.Set("USE_TICKET_INAME", _ticket);
        GachaTopParamNew gachaTopParamNew = this.mGachaListTicket.Find((Predicate<GachaTopParamNew>) (p => p.iname == _iname));
        if (gachaTopParamNew != null)
          FlowNode_Variable.Set("USE_TICKET_MAX", !gachaTopParamNew.redraw ? string.Empty : "1");
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
      }
      else
        this.StartCoroutine(this.CreateConfirm(gachaRequest));
    }

    private void OnDecide(GameObject dialog) => this.OnDecide();

    private void OnDecideForTicketSelect()
    {
      int useTicketNum = MonoSingleton<GachaManager>.Instance.UseTicketNum;
      if (this.m_CurrentGachaRequestParam != null)
        this.m_CurrentGachaRequestParam.SetNum(useTicketNum);
      this.OnDecide();
    }

    private void OnDecisionRedrawGacha()
    {
      if (this.m_CurrentGachaRequestParam == null)
      {
        if (GachaResultData.drops != null && GachaResultData.IsPending)
        {
          this.m_CurrentGachaRequestParam = this.CreateGachaRequest(GachaResultData.receipt.iname);
          if (this.m_CurrentGachaRequestParam == null)
          {
            DebugUtility.LogError("召喚リクエストがありません.");
            return;
          }
        }
        else
        {
          DebugUtility.LogError("前回の召喚リクエストが存在しません");
          return;
        }
      }
      FlowNode_ExecGacha2 componentInParent = ((Component) this).GetComponentInParent<FlowNode_ExecGacha2>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) componentInParent, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("FlowNode_ExecGacha2がありません.");
      }
      else
      {
        this.mState.GotoState<GachaWindow.State_PauseState>();
        componentInParent.OnExecGachaDecision(this.m_CurrentGachaRequestParam);
      }
    }

    private void OnDecide()
    {
      this.mClicked = false;
      FlowNode_ExecGacha2 componentInParent = ((Component) this).GetComponentInParent<FlowNode_ExecGacha2>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) componentInParent, (UnityEngine.Object) null) || this.m_CurrentGachaRequestParam == null)
        return;
      DataSource.Bind<GachaRequestParam>(((Component) this.RootObject.parent).gameObject, this.m_CurrentGachaRequestParam);
      this.mState.GotoState<GachaWindow.State_PauseState>();
      componentInParent.OnExecGacha(this.m_CurrentGachaRequestParam);
      this.m_CurrentGachaRequestParam.ResetExtraParam();
    }

    private void OnCancel(GameObject dialog) => this.OnCancel();

    private void OnCancel() => this.mClicked = false;

    private GachaRequestParam CreateGachaRequest(string _iname)
    {
      GachaRequestParam gachaRequest = (GachaRequestParam) null;
      if (!string.IsNullOrEmpty(_iname))
      {
        GachaTopParamNew gachaTopParamNew = this.mGachaListAll.Find((Predicate<GachaTopParamNew>) (p => p.iname == _iname));
        if (gachaTopParamNew == null)
        {
          DebugUtility.LogError("前回の召喚リクエスト情報がありません");
        }
        else
        {
          gachaRequest = this.CreateGachaRequest(gachaTopParamNew);
          if (gachaRequest == null)
            DebugUtility.LogError("召喚リクエストの生成に失敗しました.");
        }
      }
      return gachaRequest;
    }

    private void CheckPrevGachaRequest()
    {
      if (this.m_CurrentGachaRequestParam == null)
      {
        if (GachaResultData.drops != null && GachaResultData.IsPending)
        {
          GachaRequestParam gachaRequest = this.CreateGachaRequest(GachaResultData.receipt.iname);
          if (gachaRequest == null)
          {
            DebugUtility.LogError("召喚リクエストの生成に失敗しました.");
            return;
          }
          this.m_CurrentGachaRequestParam = gachaRequest;
        }
        else
        {
          DebugUtility.LogError("前回の召喚リクエストが存在しません");
          return;
        }
      }
      if (this.m_CurrentGachaRequestParam.IsRedrawGacha && GachaResultData.drops != null)
        this.m_CurrentGachaRequestParam.SetRedraw(GachaResultData.RedrawRest, this.m_CurrentGachaRequestParam.RedrawNum);
      this.m_CurrentGachaRequestParam.SetConfirmText(this.GetConfirmText(this.m_CurrentGachaRequestParam.FixCost, this.m_CurrentGachaRequestParam.IsPaid, string.Empty, this.m_CurrentGachaRequestParam.IsDiscount));
      if (this.m_CurrentGachaRequestParam.IsTicketGacha)
      {
        if (this.m_CurrentGachaRequestParam.IsRedrawGacha)
        {
          this.StartCoroutine(this.CreateConfirm(this.m_CurrentGachaRequestParam));
        }
        else
        {
          FlowNode_Variable.Set("USE_TICKET_INAME", this.m_CurrentGachaRequestParam.Ticket);
          GachaTopParamNew gachaTopParamNew = this.mGachaListTicket.Find((Predicate<GachaTopParamNew>) (p => p.iname == this.m_CurrentGachaRequestParam.Iname));
          if (gachaTopParamNew != null)
            FlowNode_Variable.Set("USE_TICKET_MAX", !gachaTopParamNew.redraw ? string.Empty : "1");
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
        }
      }
      else
      {
        if (this.m_CurrentGachaRequestParam.IsSingle)
        {
          int _free = this.m_CurrentGachaRequestParam.Free;
          PlayerData player = MonoSingleton<GameManager>.Instance.Player;
          if (this.m_CurrentGachaRequestParam.Category == GachaCategory.DEFAULT_NORMAL)
          {
            if (!this.IsFreePause(this.mGachaListNormal.ToArray()))
              _free = !player.CheckFreeGachaGold() || player.CheckFreeGachaGoldMax() ? 0 : 1;
          }
          else if (this.m_CurrentGachaRequestParam.Category == GachaCategory.DEFAULT_RARE && this.m_CurrentGachaRequestParam.CostType == GachaCostType.COIN && !this.IsFreePause(this.mGachaListRare.ToArray()))
            _free = !player.CheckFreeGachaCoin() ? 0 : 1;
          this.m_CurrentGachaRequestParam.SetFree(_free);
        }
        if (this.m_CurrentGachaRequestParam.IsGold || this.m_CurrentGachaRequestParam.IsFree)
          this.OnDecide();
        else
          this.StartCoroutine(this.CreateConfirm(this.m_CurrentGachaRequestParam));
      }
    }

    private string GetConfirmText(int _cost, bool _ispaid, string _confirm, bool _is_discount)
    {
      string key = GachaWindow.TEXT_COST_GACHA_COIN;
      string confirmGachaCoin = GachaWindow.TEXT_CONFIRM_GACHA_COIN;
      if (_ispaid)
        key = GachaWindow.TEXT_COST_GACHA_PAIDCOIN;
      string str = LocalizedText.Get(key);
      string confirmText = LocalizedText.Get(_confirm);
      if (string.IsNullOrEmpty(confirmText))
        confirmText = LocalizedText.Get(confirmGachaCoin, (object) str, (object) _cost.ToString());
      if (_cost == 0 && !_is_discount)
        confirmText = LocalizedText.Get(GachaWindow.TEXT_CONFIRM_GACHA_COST_ZERO);
      return confirmText;
    }

    private GachaRequestParam CreateGachaRequest(
      string _iname,
      int _cost,
      string _category,
      string _ticket,
      int _num,
      string _confirm,
      GachaCostType _cost_type,
      bool _is_use_onemore = false,
      bool _is_no_use_free = false,
      int _redraw_rest = 0,
      int _redraw_num = 0,
      int _discount = -1)
    {
      GachaCategory _category1 = GachaCategory.NONE;
      if (!string.IsNullOrEmpty(_category))
      {
        if (_category.Contains("gold"))
          _category1 = GachaCategory.DEFAULT_NORMAL;
        else if (_category.Contains("coin"))
          _category1 = GachaCategory.DEFAULT_RARE;
      }
      int _free = 0;
      if (_category1 == GachaCategory.DEFAULT_RARE && _num == 1 && _cost_type == GachaCostType.COIN && !_is_no_use_free)
        _free = !MonoSingleton<GameManager>.Instance.Player.CheckFreeGachaCoin() ? 0 : 1;
      else if (_category1 == GachaCategory.DEFAULT_NORMAL && _num == 1)
        _free = !MonoSingleton<GameManager>.Instance.Player.CheckFreeGachaGold() || MonoSingleton<GameManager>.Instance.Player.CheckFreeGachaGoldMax() ? 0 : 1;
      string confirmText = this.GetConfirmText(_discount <= -1 ? _cost : _discount, _cost_type == GachaCostType.COIN_P, _confirm, _discount >= 0);
      GachaRequestParam gachaRequest = new GachaRequestParam(_iname, _cost, confirmText, _cost_type, _category1, _is_use_onemore);
      if (!string.IsNullOrEmpty(_ticket))
        gachaRequest.SetTicketInfo(_ticket, _num);
      if (_redraw_rest > 0 && _redraw_num > 0)
        gachaRequest.SetRedraw(_redraw_rest, _redraw_num);
      gachaRequest.SetFree(_free);
      gachaRequest.SetNum(_num);
      gachaRequest.SetDiscountCost(_discount);
      return gachaRequest;
    }

    private GachaRequestParam CreateGachaRequest(GachaTopParamNew param)
    {
      int _free = 0;
      if (param.IsDefaultRareFree)
        _free = !MonoSingleton<GameManager>.Instance.Player.CheckFreeGachaCoin() ? 0 : 1;
      else if (param.IsDefaultNormalFree)
        _free = !MonoSingleton<GameManager>.Instance.Player.CheckFreeGachaGold() || MonoSingleton<GameManager>.Instance.Player.CheckFreeGachaGoldMax() ? 0 : 1;
      int _num = !string.IsNullOrEmpty(param.ticket_iname) ? 1 : param.num;
      string confirmText = this.GetConfirmText(param.DiscountCost <= -1 ? param.Cost : param.DiscountCost, param.CostType == GachaCostType.COIN_P, param.confirm, param.DiscountCost >= 0);
      GachaRequestParam gachaRequest = new GachaRequestParam(param.iname, param.Cost, confirmText, param.CostType, param.Category, param.IsUseOneMore);
      if (!string.IsNullOrEmpty(param.ticket_iname))
        gachaRequest.SetTicketInfo(param.ticket_iname, _num);
      if (param.redraw)
        gachaRequest.SetRedraw(param.redraw_rest, param.redraw_num);
      gachaRequest.SetFree(_free);
      gachaRequest.SetNum(_num);
      gachaRequest.SetDiscountCost(param.DiscountCost);
      gachaRequest.SetDailyFree(!param.is_daily_free ? 0 : 1);
      gachaRequest.SetSimpleAnim(param.is_simple_anim);
      return gachaRequest;
    }

    private RenderTexture CreateRenderTexture()
    {
      int num = Mathf.FloorToInt(864f);
      return RenderTexture.GetTemporary(num, num, 16, (RenderTextureFormat) 7);
    }

    public void SetGachaPreviewCamera()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mPreviewCamera, (UnityEngine.Object) null))
        return;
      this.mPreviewCamera.targetTexture = this.mPreviewUnitRT;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PreviewImage, (UnityEngine.Object) null))
        return;
      this.PreviewImage.texture = (Texture) this.mPreviewUnitRT;
    }

    public void SetGachaPreviewArtifactCamera()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mPreviewCamera, (UnityEngine.Object) null))
        return;
      this.mPreviewCamera.targetTexture = this.mPreviewUnitRT;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PreviewArtifactImage, (UnityEngine.Object) null))
        return;
      this.PreviewArtifactImage.texture = (Texture) this.mPreviewUnitRT;
    }

    private void FadeUnitImage(float alphastart, float alphaend, float duration)
    {
      this.mBGUnitImgAlphaStart = alphastart;
      this.mBGUnitImgAlphaEnd = alphaend;
      this.mBGUnitImgFadeTime = 0.0f;
      this.mBGUnitImgFadeTimeMax = duration;
      if ((double) duration > 0.0)
        return;
      this.SetUnitImageAlpha(this.mBGUnitImgAlphaEnd);
    }

    private void SetUnitImageAlpha(float alpha)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mBGUnitImage, (UnityEngine.Object) null))
        return;
      Color color = ((Graphic) this.mBGUnitImage).color;
      color.a = alpha;
      ((Graphic) this.mBGUnitImage).color = color;
    }

    [DebuggerHidden]
    private IEnumerator RefreshUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaWindow.\u003CRefreshUnitImage\u003Ec__Iterator7()
      {
        \u0024this = this
      };
    }

    public List<GachaDropData> RequestPickupSelectDatas => this.mRequestPickupSelectDatas;

    public void SetupGachaSelectList(string key, Json_GachaPickups[] jsons, int num)
    {
      if (string.IsNullOrEmpty(key) || jsons == null || jsons.Length <= 0 || this.mGachaPickupSelectLists.ContainsKey(key))
        return;
      GachaPickupSelectParam pickupSelectParam = new GachaPickupSelectParam();
      pickupSelectParam.Setup(num, jsons);
      this.mGachaPickupSelectLists.Add(key, pickupSelectParam);
    }

    public bool IsAlreadyPickupSelectList(string gname)
    {
      return this.mGachaPickupSelectLists != null && this.mGachaPickupSelectLists.ContainsKey(gname);
    }

    public GachaTopParamNew[] GetCurrentGacha() => this.GetCurrentGachaLists(this.mSelectTab);

    public string GetCurrentGachaId()
    {
      GachaTopParamNew[] currentGachaLists = this.GetCurrentGachaLists(this.mSelectTab);
      return currentGachaLists == null || currentGachaLists.Length <= 0 ? string.Empty : currentGachaLists[0].iname;
    }

    public GachaPickupSelectParam GetCurrentGachaPickupSelectParam()
    {
      string currentGachaId = this.GetCurrentGachaId();
      if (string.IsNullOrEmpty(currentGachaId))
        return (GachaPickupSelectParam) null;
      return this.mGachaPickupSelectLists != null && this.mGachaPickupSelectLists.ContainsKey(currentGachaId) ? this.mGachaPickupSelectLists[currentGachaId] : (GachaPickupSelectParam) null;
    }

    public void SetRequestPickupSelectData(List<GachaDropData> lists)
    {
      if (lists == null || lists.Count <= 0)
        return;
      this.mRequestPickupSelectDatas.Clear();
      for (int index = 0; index < lists.Count; ++index)
      {
        GachaDropData data = new GachaDropData();
        lists[index].CopyTo(data);
        this.mRequestPickupSelectDatas.Add(data);
      }
    }

    private UnitData CreateUnitData(UnitParam param)
    {
      UnitData unitData = new UnitData();
      Json_Unit json = new Json_Unit()
      {
        iid = 1,
        iname = param.iname,
        exp = 0,
        lv = 1,
        plus = 0,
        rare = 0,
        select = new Json_UnitSelectable()
      };
      json.select.job = 0L;
      json.jobs = (Json_Job[]) null;
      json.abil = (Json_MasterAbility) null;
      json.abil = (Json_MasterAbility) null;
      if (param.jobsets != null && param.jobsets.Length > 0)
      {
        List<Json_Job> jsonJobList = new List<Json_Job>(param.jobsets.Length);
        int num = 1;
        for (int index = 0; index < param.jobsets.Length; ++index)
        {
          JobSetParam jobSetParam = MonoSingleton<GameManager>.Instance.GetJobSetParam(param.jobsets[index]);
          if (jobSetParam != null)
            jsonJobList.Add(new Json_Job()
            {
              iid = (long) num++,
              iname = jobSetParam.job,
              rank = 0,
              equips = (Json_Equip[]) null,
              abils = (Json_Ability[]) null
            });
        }
        json.jobs = jsonJobList.ToArray();
      }
      unitData.Deserialize(json);
      unitData.SetUniqueID(1L);
      unitData.JobRankUp(0);
      return unitData;
    }

    private ArtifactData CreateArtifactData(ArtifactParam param)
    {
      ArtifactData artifactData = new ArtifactData();
      Json_Artifact json = new Json_Artifact();
      json.iid = 1L;
      json.iname = param.iname;
      json.rare = param.raremax;
      json.fav = 0;
      RarityParam rarityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam(param.raremax);
      json.exp = ArtifactData.StaticCalcExpFromLevel((int) rarityParam.ArtifactLvCap);
      artifactData.Deserialize(json);
      return artifactData;
    }

    public class GachaTopParamNewGroups
    {
      public List<GachaTopParamNew> lists = new List<GachaTopParamNew>();
      public string group;
      public string tab_image;
      public string bg_image;
    }

    public enum GachaTabCategory : byte
    {
      NONE,
      RARE,
      ARTIFACT,
      TICKET,
      NORMAL,
      SPECIAL,
    }

    private class State_Init : State<GachaWindow>
    {
      public override void Update(GachaWindow self)
      {
        if (!self.mLoadGachaTabSprites || !self.mLoadBackGroundTexture)
          return;
        self.SetupTabList();
        self.RefreshTabList();
        SRPG_Button mTab = self.mTabList[0];
        GachaWindow.GachaTabCategory tabCategory = self.GetTabCategory(mTab);
        int index = 0;
        self.TabChange(mTab, tabCategory, index);
        self.RefreshGachaDetailSelectID(self.mSelectTab);
        self.mInitialized = true;
        self.mClicked = false;
        self.mState.GotoState<GachaWindow.State_CheckInitState>();
      }
    }

    private class State_CheckInitState : State<GachaWindow>
    {
      public override void Update(GachaWindow self)
      {
        if (self.IsRefreshingGachaBG)
          return;
        self.RefreshTabActive(true);
        self.RefreshTabEnable(true);
        if (self.IsGachaPending())
          self.mState.GotoState<GachaWindow.State_ToGachaResult>();
        else if (self.mSelectTab == GachaWindow.GachaTabCategory.RARE || self.mSelectTab == GachaWindow.GachaTabCategory.ARTIFACT)
        {
          GachaWindow gachaWindow = self;
          bool flag = false;
          self.ChangeJob = flag;
          int num = flag ? 1 : 0;
          gachaWindow.ChangeUnit = num != 0;
          self.mState.GotoState<GachaWindow.State_WaitActionAnimation>();
        }
        else
          self.mState.GotoState<GachaWindow.State_PauseState>();
      }
    }

    private class State_RefreshPreview : State<GachaWindow>
    {
      private WaitForSeconds wait;

      public override void Begin(GachaWindow self)
      {
        this.wait = new WaitForSeconds(!self.ChangeUnit ? self.ChangeJobWaitEffectTime : self.ChangeUnitWaitEffectTime);
        self.StartCoroutine(this.RebuildPreviewController(self));
      }

      [DebuggerHidden]
      private IEnumerator RebuildPreviewController(GachaWindow self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new GachaWindow.State_RefreshPreview.\u003CRebuildPreviewController\u003Ec__Iterator0()
        {
          self = self,
          \u0024this = this
        };
      }

      private void RebuildUnitPreview()
      {
        this.self.UpdateCurrentUnitInfo();
        if (this.self.ChangeUnit)
        {
          this.self.RefreshUnitInfo();
          this.self.RefreshPreviews();
        }
        else
        {
          this.self.RefreshJobs();
          this.self.SetActivePreview(this.self.mCurrentJobIndex);
        }
        if (!this.self.Initialized)
          return;
        GameParameter.UpdateAll(((Component) this.self).gameObject);
      }

      private void RebuildArtifactPreview()
      {
        this.self.RefreshArtifactInfo();
        this.self.SetActivePreviewArtifact(this.self.mCurrentPickupArtIndex);
        this.self.RefreshPreviews();
      }
    }

    private class State_WaitActionAnimation : State<GachaWindow>
    {
      private float mTimer;

      public override void Begin(GachaWindow self)
      {
        if (self.mSelectTab == GachaWindow.GachaTabCategory.ARTIFACT)
        {
          self.mState.GotoState<GachaWindow.State_WaitPreviewUnit>();
        }
        else
        {
          this.mTimer = self.WaitTimeNextAction;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mCurrentPreview))
            return;
          DebugUtility.LogError("mCurrentPreviewがNullです");
        }
      }

      public override void Update(GachaWindow self)
      {
        this.mTimer -= Time.deltaTime;
        if ((double) this.mTimer > 0.0 || !UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCurrentPreview, (UnityEngine.Object) null))
          return;
        self.mCurrentPreview.PlayAction = true;
        self.mState.GotoState<GachaWindow.State_WaitPreviewUnit>();
      }
    }

    private class State_WaitPreviewUnit : State<GachaWindow>
    {
      private float mTimer;

      public override void Begin(GachaWindow self) => this.mTimer = self.mWaitSecondsChangeUnitJob;

      public override void Update(GachaWindow self)
      {
        this.mTimer -= Time.deltaTime;
        if ((double) this.mTimer > 0.0)
          return;
        self.mState.GotoState<GachaWindow.State_CheckPreviewState>();
      }
    }

    private class State_CheckPreviewState : State<GachaWindow>
    {
      public override void Begin(GachaWindow self)
      {
        if (self.mSelectTab == GachaWindow.GachaTabCategory.ARTIFACT)
          self.ChangePreviewArtifact();
        else if (self.mSelectTab == GachaWindow.GachaTabCategory.RARE)
        {
          self.ChangeJob = false;
          self.ChangeUnit = false;
          if (!self.SelectJob())
          {
            self.ChangePreviewUnit();
            self.ChangeUnit = true;
          }
        }
        self.mState.GotoState<GachaWindow.State_RefreshPreview>();
      }
    }

    private class State_PauseState : State<GachaWindow>
    {
      public override void Begin(GachaWindow self)
      {
      }
    }

    private class State_ToGachaResult : State<GachaWindow>
    {
      public override void Begin(GachaWindow self)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 200);
        self.mState.GotoState<GachaWindow.State_PauseState>();
      }
    }

    [Serializable]
    public class BackGround
    {
      public CanvasGroup RootCanvasGroup;
      public RawImage BackGroundLeft;
      public RawImage BackGroundRight;

      public bool IsTextureSet
      {
        get
        {
          return !UnityEngine.Object.op_Equality((UnityEngine.Object) this.BackGroundLeft, (UnityEngine.Object) null) && !UnityEngine.Object.op_Equality((UnityEngine.Object) this.BackGroundRight, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackGroundLeft.texture, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackGroundRight.texture, (UnityEngine.Object) null);
        }
      }
    }

    private class BackGroundTexture
    {
      public Texture2D left { get; set; }

      public Texture2D right { get; set; }
    }

    private class BackGroundTextureGroup
    {
      public GachaWindow.BackGroundTexture[] backgrounds;

      public bool IsEnableData => this.backgrounds != null && this.backgrounds.Length > 0;
    }

    private enum eGachaBGParts
    {
      LEFT,
      RIGHT,
      MAX,
    }

    [Serializable]
    public class OptionParts
    {
      public GameObject OptionRoot;
      public GameObject StepupBase;
      public UnityEngine.UI.Text StepupNumText;
      public GameObject LimitBase;
      public UnityEngine.UI.Text LimitNumText;

      public void Refresh(GachaTopParamNew param)
      {
        GameUtility.SetGameObjectActive(this.OptionRoot, false);
        GameUtility.SetGameObjectActive(this.StepupBase, false);
        GameUtility.SetGameObjectActive(this.LimitBase, false);
        if (param == null || param.IsOptionUIHide)
          return;
        bool flag = false;
        if (param.step)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StepupNumText, (UnityEngine.Object) null))
            this.StepupNumText.text = param.ShowCurrentStepNum.ToString();
          GameUtility.SetGameObjectActive(this.StepupBase, true);
          flag = true;
        }
        else if (param.limit_cnt)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LimitNumText, (UnityEngine.Object) null))
            this.LimitNumText.text = param.limit_cnt_rest.ToString();
          GameUtility.SetGameObjectActive(this.LimitBase, true);
          flag = true;
        }
        if (!flag)
          return;
        GameUtility.SetGameObjectActive(this.OptionRoot, true);
      }
    }

    [Serializable]
    public class BonusIcon
    {
      public GameObject Root;
      public GameObject ItemIcon;
      public GameObject UnitIcon;
      public GameObject ArtifactIcon;
      public GameObject ConceptCardIcon;

      public void Reset()
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Root, (UnityEngine.Object) null))
          this.Root.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemIcon, (UnityEngine.Object) null))
        {
          DataSource.Clear(this.ItemIcon);
          this.ItemIcon.SetActive(false);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitIcon, (UnityEngine.Object) null))
        {
          DataSource.Clear(this.UnitIcon);
          this.UnitIcon.SetActive(false);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactIcon, (UnityEngine.Object) null))
        {
          DataSource.Clear(this.ArtifactIcon);
          this.ArtifactIcon.SetActive(false);
        }
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConceptCardIcon, (UnityEngine.Object) null))
          return;
        DataSource.Clear(this.ConceptCardIcon);
        this.ConceptCardIcon.SetActive(false);
      }
    }
  }
}
