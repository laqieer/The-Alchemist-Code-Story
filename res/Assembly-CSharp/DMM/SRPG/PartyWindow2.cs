// Decompiled with JetBrains decompiler
// Type: SRPG.PartyWindow2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(3, "戻る", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(4, "開く", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(10, "ユニット選択開始", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "ユニット選択完了", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(20, "アイテム選択開始", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(21, "アイテム選択完了", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(40, "チーム名変更完了", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(41, "おまかせ編成完了", FlowNode.PinTypes.Input, 41)]
  [FlowNode.Pin(50, "クエスト更新", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(1, "進む", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(5, "画面ロック", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(6, "画面アンロック", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(7, "AP回復アイテム", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(8, "マルチタワー用進む", FlowNode.PinTypes.Output, 8)]
  [FlowNode.Pin(100, "ユニット選択", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "サポートユニット選択", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(110, "ユニット解除", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(111, "サポートユニット解除", FlowNode.PinTypes.Input, 111)]
  [FlowNode.Pin(119, "ユニットリストウィンド開いた", FlowNode.PinTypes.Input, 119)]
  [FlowNode.Pin(120, "ユニットリストウィンド閉じ始めた", FlowNode.PinTypes.Input, 120)]
  [FlowNode.Pin(130, "強制的に更新", FlowNode.PinTypes.Input, 130)]
  [FlowNode.Pin(140, "強制的に更新(チームのリロードなし)", FlowNode.PinTypes.Input, 140)]
  [FlowNode.Pin(150, "真理念装表示更新", FlowNode.PinTypes.Input, 150)]
  [FlowNode.Pin(200, "フレンドがサポートに設定された", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(210, "フレンド以外がサポートが設定された", FlowNode.PinTypes.Output, 210)]
  [FlowNode.Pin(220, "サポートが解除された", FlowNode.PinTypes.Output, 220)]
  [FlowNode.Pin(250, "リネームボタンが押された", FlowNode.PinTypes.Output, 250)]
  [FlowNode.Pin(300, "パーティ情報の更新（開始）", FlowNode.PinTypes.Input, 300)]
  [FlowNode.Pin(310, "パーティ情報の更新（終了）", FlowNode.PinTypes.Output, 310)]
  [FlowNode.Pin(319, "ストーリーEX挑戦回数リセット", FlowNode.PinTypes.Output, 319)]
  [FlowNode.Pin(320, "ストーリーEXトータル挑戦回数リセット", FlowNode.PinTypes.Output, 320)]
  [FlowNode.Pin(330, "表示更新(挑戦回数リセット)", FlowNode.PinTypes.Input, 330)]
  public class PartyWindow2 : MonoBehaviour, IFlowInterface, ISortableList
  {
    public const int PIN_OUTPUT_BACK = 3;
    public const int PIN_OUTPUT_OPEN = 4;
    public const int PIN_OUTPUT_UNIT_SELSTART = 10;
    public const int PIN_OUTPUT_UNIT_SELEND = 11;
    public const int PIN_OUTPUT_ITEM_SELSTART = 20;
    public const int PIN_OUTPUT_ITEM_SELEND = 21;
    public const int PIN_INPUT_TEAM_NAME = 40;
    public const int PIN_INPUT_RECOMMEND_TEAM = 41;
    public const int PIN_INPUT_QUEST_REFRESH = 50;
    public const int PIN_OUTPUT_GO = 1;
    public const int PIN_OUTPUT_LOCK = 5;
    public const int PIN_OUTPUT_UNLOCK = 6;
    public const int PIN_OUTPUT_AP_HEAL = 7;
    public const int PIN_OUTPUT_MULTI_GO = 8;
    public const int PIN_INPUT_UNIT_SELECT = 100;
    public const int PIN_INPUT_SUPPORT_SELECT = 101;
    public const int PIN_INPUT_UNIT_RELEASE = 110;
    public const int PIN_INPUT_SUPPORT_RELEASE = 111;
    public const int PIN_INPUT_UNITLIST_OPEN = 119;
    public const int PIN_INPUT_UNITLIST_CLOSE = 120;
    public const int PIN_INPUT_FORCE_REFRESH = 130;
    public const int PIN_INPUT_FORCE_REFRESH_NOTEAM = 140;
    public const int PIN_INPUT_CONCEPTCARD_REFRESH = 150;
    public const int PIN_OUTPUT_SUPPORT_FRIEND = 200;
    public const int PIN_OUTPUT_SUPPORT_OTHER = 210;
    public const int PIN_OUTPUT_SUPPORT_RELEASE = 220;
    public const int PIN_OUTPUT_RENAME = 250;
    public const int PIN_INPUT_PARTY_REFRESH_START = 300;
    public const int PIN_OUTPUT_PARTY_REFRESH_END = 310;
    public const int PIN_OUTPUT_STORYEX_RESET = 319;
    public const int PIN_OUTPUT_STORYEX_TOTAL_RESET = 320;
    public const int PIN_INPUT_CHALLENGE_RESET = 330;
    public const int PIN_INPUT_VERSUS_EDIT_PLACE_START = 10010;
    public const int PIN_OUTPUT_VERSUS_EDIT_PLACE_END = 10100;
    public const int PIN_OUTPUT_VERSUS_EDIT_PLACE_FAILED = 10200;
    public int MaxRaidNum = 50;
    public int DefaultRaidNum = 10;
    public PartyWindow2.EditPartyTypes PartyType = PartyWindow2.EditPartyTypes.Normal;
    private static PartyWindow2 mInstance;
    [Space(10f)]
    [SerializeField]
    private GenericSlot UnitSlotTemplate;
    [SerializeField]
    private GenericSlot NpcSlotTemplate;
    [SerializeField]
    private Transform MainMemberHolder;
    [SerializeField]
    private Transform SubMemberHolder;
    [SerializeField]
    private GenericSlot CardSlotTemplate;
    [SerializeField]
    private Transform MainMemberCardHolder;
    [SerializeField]
    private Transform SubMemberCardHolder;
    [SerializeField]
    protected GenericSlot[] UnitSlots;
    [SerializeField]
    private GenericSlot[] CardSlots;
    [SerializeField]
    private GenericSlot FriendSlot;
    [SerializeField]
    private GenericSlot FriendCardSlot;
    [SerializeField]
    private string SlotChangeTrigger;
    [Space(10f)]
    [SerializeField]
    private GameObject AddMainUnitOverlay;
    [SerializeField]
    private GameObject AddSubUnitOverlay;
    [SerializeField]
    private GameObject AddItemOverlay;
    [SerializeField]
    private GameObject AddPopupItemOverlay;
    [Space(10f)]
    [SerializeField]
    private GenericSlot[] ItemSlots;
    [SerializeField]
    private SRPG_Button ItemButton;
    [SerializeField]
    private GenericSlot[] PopupItemSlots;
    [Space(10f)]
    [SerializeField]
    private FixedScrollablePulldown TeamPulldown;
    [SerializeField]
    protected Toggle[] TeamTabs = new Toggle[0];
    [Space(10f)]
    [FormerlySerializedAs("TotalAtk")]
    [SerializeField]
    private UnityEngine.UI.Text TotalCombatPower;
    [SerializeField]
    private GenericSlot LeaderSkill;
    [SerializeField]
    private ImageArray LeaderSkillBGImageArray;
    [SerializeField]
    private GenericSlot SupportSkill;
    [SerializeField]
    private GameObject ConceptCardBonus;
    [SerializeField]
    private UnityEngine.UI.Text ConceptCardBonusRate;
    [SerializeField]
    private SRPG_Button ChangeLeaderSkillButton;
    [SerializeField]
    private GameObject QuestInfo;
    [SerializeField]
    private SRPG_Button QuestInfoButton;
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string QuestDetail;
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string QuestDetailMulti;
    public bool ShowQuestInfo = true;
    public bool UseQuestInfo = true;
    public bool ShowRaidInfo = true;
    private SRPG_Button CurrentForwardButton;
    public SRPG_Button ForwardButton;
    public bool ShowForwardButton = true;
    public SRPG_Button ChallangeCountResetButton;
    [SerializeField]
    private UnityEngine.UI.Text ChallangeCountResetButtonText;
    public SRPG_Button LimitForwardButton;
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string CHALLENGE_COUNT_RESET_CONFIRM = "UI/ChallengeCountResetConfirm";
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string CHALLENGE_COUNT_RESET_COMPLETE = "UI/ChallengeCountResetComplete";
    [Space(10f)]
    public SRPG_Button BackButton;
    public bool ShowBackButton = true;
    public bool EnableTeamAssign;
    public GameObject NoItemText;
    public GameObject Prefab_SankaFuka;
    public float SankaFukaOpacity = 0.5f;
    public SRPG_Button RecommendTeamButton;
    public SRPG_Button BreakupButton;
    public SRPG_Button RenameButton;
    public SRPG_Button PrevButton;
    public SRPG_Button NextButton;
    public SRPG_Button RecentTeamButton;
    public UnityEngine.UI.Text TextFixParty;
    [Space(10f)]
    public RectTransform[] ChosenUnitBadges = new RectTransform[0];
    public RectTransform[] ChosenItemBadges = new RectTransform[0];
    public RectTransform ChosenSupportBadge;
    [Space(10f)]
    public RectTransform MainRect;
    public VirtualList UnitList;
    public RectTransform UnitListHilit;
    public string UnitListHilitParent;
    public GameObject NoMatchingUnit;
    public bool AlwaysShowRemoveUnit;
    public UnityEngine.UI.Text SortModeCaption;
    public GameObject AscendingIcon;
    public GameObject DescendingIcon;
    [Space(10f)]
    public VirtualList ItemList;
    public ListItemEvents ItemListItem;
    public SRPG_Button ItemRemoveItem;
    public RectTransform ItemListHilit;
    public string ItemListHilitParent;
    public SRPG_Button CloseItemList;
    public RectTransform ItemListPopupHilit;
    public string ItemListPopupHilitParent;
    public SRPG_ToggleButton ItemFilter_All;
    public SRPG_ToggleButton ItemFilter_Offense;
    public SRPG_ToggleButton ItemFilter_Support;
    public bool AlwaysShowRemoveItem;
    [Space(10f)]
    [SerializeField]
    private GameObject RaidInfo;
    [SerializeField]
    private UnityEngine.UI.Text RaidTicketNum;
    [SerializeField]
    private SRPG_Button Raid;
    [SerializeField]
    private SRPG_Button RaidN;
    [SerializeField]
    private UnityEngine.UI.Text RaidNCount;
    [StringIsResourcePath(typeof (RaidResultWindow))]
    [SerializeField]
    private string RaidResultPrefab;
    [SerializeField]
    private SRPG_Button RaidTicketPlus;
    [SerializeField]
    private SRPG_Button RaidTicketMinus;
    [SerializeField]
    private UnityEngine.UI.Text RaidAPCurrent;
    [SerializeField]
    private UnityEngine.UI.Text RaidAPAfter;
    [SerializeField]
    private Color RaidAPColorEnable = new Color(0.0235f, 1f, 0.0f);
    [SerializeField]
    private Color RaidAPColorDisable = new Color(1f, 0.0f, 0.0f);
    public GameObject RaidSettingsTemplate;
    [SerializeField]
    private GameObject RaidIconAp;
    [SerializeField]
    private GameObject RaidIconItem;
    [Space(10f)]
    public Toggle ToggleDirectineCut;
    [Space(10f)]
    [SerializeField]
    private QuestCampaignCreate QuestCampaigns;
    [Space(10f)]
    public GameObject QuestUnitCond;
    public PlayerPartyTypes[] SaveJobs = new PlayerPartyTypes[1]
    {
      PlayerPartyTypes.ArenaDef
    };
    [Space(10f)]
    public bool EnableHeroSolo = true;
    [Space(10f)]
    public SRPG_Button BattleSettingButton;
    public SRPG_Button HelpButton;
    public GameObject Filter;
    [Space(10f)]
    public string UNIT_LIST_PATH = "UI/UnitPickerWindowEx";
    [Space(10f)]
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string UNITLIST_WINDOW_PATH = "UI/UnitListWindow";
    private UnitListWindow mUnitListWindow;
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string CARDLIST_WINDOW_PATH = "UI/ConceptCardSelect";
    protected List<UnitData> mOwnUnits;
    private List<ItemData> mOwnItems;
    protected QuestParam mCurrentQuest;
    protected List<UnitData> mGuestUnit = new List<UnitData>();
    protected List<PartySlotData> mSlotData = new List<PartySlotData>();
    private PartySlotData mSupportSlotData;
    protected int mCurrentTeamIndex;
    protected int mSelectedRenameTeamIndex = -1;
    protected int mMaxTeamCount;
    protected PartyWindow2.EditPartyTypes mCurrentPartyType;
    private List<SupportData> mSupports = new List<SupportData>();
    protected SupportData mCurrentSupport;
    private SupportData mSelectedSupport;
    protected ItemData[] mCurrentItems = new ItemData[5];
    private List<RectTransform> mUnitPoolA = new List<RectTransform>();
    private List<RectTransform> mUnitPoolB = new List<RectTransform>();
    private List<RectTransform> mItemPoolA = new List<RectTransform>();
    private List<RectTransform> mItemPoolB = new List<RectTransform>();
    private List<RectTransform> mSupportPoolA = new List<RectTransform>();
    private List<RectTransform> mSupportPoolB = new List<RectTransform>();
    protected int mSelectedSlotIndex;
    protected List<PartyEditData> mTeams = new List<PartyEditData>();
    protected int mLockedPartySlots;
    protected bool mSupportLocked;
    private bool mItemsLocked;
    protected bool mIsSaving;
    private PartyWindow2.Callback mOnPartySaveSuccess;
    private PartyWindow2.Callback mOnPartySaveFail;
    private RaidResultWindow mRaidResultWindow;
    private RaidResult mRaidResult;
    private LoadRequest mReqRaidResultWindow;
    private LoadRequest mReqQuestDetail;
    private string[] mUnitFilter;
    private bool mReverse;
    private SRPG_ToggleButton[] mItemFilterToggles;
    private PartyWindow2.ItemFilterTypes mItemFilter;
    protected GameObject[] mSankaFukaIcons;
    private RaidSettingsWindow mRaidSettings;
    private int mMultiRaidNum = -1;
    private int mNumRaids;
    private ItemParam GenAdvBossChItemParam;
    private int GenAdvBossChItemNeedNum;
    private bool mInitialized;
    private bool mIsHeloOnly;
    protected bool mIsLockCurrentParty;
    [Space(10f)]
    public SRPG_Button ButtonMapEffectQuest;
    [StringIsResourcePath(typeof (GameObject))]
    public string PrefabMapEffectQuest;
    private LoadRequest mReqMapEffectQuest;
    public string SceneNameHome = "Home";
    private GameObject mMultiErrorMsg;
    private bool mIsShowDownloadPopup = true;
    private GameObject mConceptCardSelector;
    [Space(10f)]
    [SerializeField]
    private SRPG_Button AutoRepeatQuestBtn;
    [SerializeField]
    private SRPG_Button AutoRepeatQuestBtnMask;
    [SerializeField]
    private SRPG_Button AutoRepeatQuestBestTimeBtn;
    [SerializeField]
    private SRPG_Button AutoRepeatQuestBestTimeBtnMask;
    [SerializeField]
    private UnityEngine.UI.Text AutoRepeatQuestBestTimeText;
    [SerializeField]
    private GameObject mGuildRaidForcedDeck;
    [SerializeField]
    private GameObject StoryExChallengeCount;
    private Transform mTrHomeHeader;
    private bool mUnitSlotSelected;
    private static bool ForceUpdateBattleParty;

    public PartyWindow2.EditPartyTypes CurrentPartyType => this.mCurrentPartyType;

    public static PartyWindow2 Instance => PartyWindow2.mInstance;

    public SupportData CurrentSupport => this.mCurrentSupport;

    public RaidSettingsWindow RaidSettings => this.mRaidSettings;

    public int MultiRaidNum => this.mNumRaids;

    public List<PartyEditData> Teams => this.mTeams;

    private bool IsShowDownloadPopup
    {
      get
      {
        return AssetManager.UseDLC && AssetDownloader.IsEnableShowSizeBeforeDownloading() && this.mIsShowDownloadPopup;
      }
    }

    protected void SetCurrentParty(PartyEditData party_data)
    {
      int num = this.mTeams.IndexOf(party_data);
      if (num <= -1)
      {
        num = 0;
        DebugUtility.LogError("out of range => mTeams index");
      }
      this.mCurrentTeamIndex = num;
    }

    protected void SetCurrentParty(int index)
    {
      if (index <= -1)
      {
        index = 0;
        DebugUtility.LogError("out of range => mTeams index");
      }
      this.mCurrentTeamIndex = index;
    }

    protected PartyEditData CurrentParty
    {
      get
      {
        if (this.mTeams == null || this.mTeams.Count <= 0)
          return (PartyEditData) null;
        if (this.mCurrentTeamIndex >= this.mTeams.Count)
        {
          this.mCurrentTeamIndex = 0;
          DebugUtility.LogError("out of range => mTeams index is " + (object) this.mCurrentTeamIndex);
        }
        return this.mTeams[this.mCurrentTeamIndex];
      }
    }

    public static string EditPartyType2String(PartyWindow2.EditPartyTypes edit_party_type)
    {
      if (edit_party_type == PartyWindow2.EditPartyTypes.Arena)
        return "col";
      return edit_party_type == PartyWindow2.EditPartyTypes.ArenaDef ? "col_def" : string.Empty;
    }

    protected bool IsEventTypeQuest()
    {
      if (this.mCurrentQuest == null)
        return false;
      switch (this.mCurrentQuest.type)
      {
        case QuestTypes.Event:
        case QuestTypes.AdvanceStory:
        case QuestTypes.AdvanceBoss:
        case QuestTypes.UnitRental:
          return true;
        default:
          return false;
      }
    }

    private void OpenQuestDetail()
    {
      if (this.mCurrentQuest == null || this.mReqQuestDetail == null || !this.mReqQuestDetail.isDone || !UnityEngine.Object.op_Inequality(this.mReqQuestDetail.asset, (UnityEngine.Object) null))
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate(this.mReqQuestDetail.asset) as GameObject;
      DataSource.Bind<QuestParam>(gameObject, this.mCurrentQuest);
      if (this.mGuestUnit != null && this.mGuestUnit.Count > 0 && this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Character)
        DataSource.Bind<UnitData>(gameObject, this.mGuestUnit[0]);
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(this.mCurrentQuest);
      DataSource.Bind<QuestCampaignData[]>(gameObject, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
      gameObject.SetActive(true);
    }

    private Transform TrHomeHeader
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTrHomeHeader, (UnityEngine.Object) null))
        {
          Scene sceneByName = SceneManager.GetSceneByName(this.SceneNameHome);
          if (((Scene) ref sceneByName).IsValid())
          {
            GameObject[] rootGameObjects = ((Scene) ref sceneByName).GetRootGameObjects();
            if (rootGameObjects != null)
            {
              foreach (GameObject gameObject in rootGameObjects)
              {
                HomeWindow component = gameObject.GetComponent<HomeWindow>();
                if (UnityEngine.Object.op_Implicit((UnityEngine.Object) component))
                {
                  this.mTrHomeHeader = ((Component) component).transform;
                  break;
                }
              }
            }
          }
        }
        return this.mTrHomeHeader;
      }
    }

    private void OpenMapEffectQuest()
    {
      if (this.mCurrentQuest == null || this.mReqMapEffectQuest == null || !this.mReqMapEffectQuest.isDone || UnityEngine.Object.op_Equality(this.mReqMapEffectQuest.asset, (UnityEngine.Object) null))
        return;
      Transform parent = this.TrHomeHeader;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) parent))
        parent = ((Component) this).transform;
      GameObject instance = MapEffectQuest.CreateInstance(this.mReqMapEffectQuest.asset as GameObject, parent);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return;
      DataSource.Bind<QuestParam>(instance, this.mCurrentQuest);
      instance.SetActive(true);
      MapEffectQuest component = instance.GetComponent<MapEffectQuest>();
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) component))
        return;
      component.Setup();
    }

    protected virtual void Init()
    {
    }

    private void Awake() => PartyWindow2.mInstance = this;

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PartyWindow2.\u003CStart\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    protected virtual void OnDestroy()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect(), (UnityEngine.Object) null))
      {
        MonoSingleton<GameManager>.Instance.OnStaminaChange -= new GameManager.StaminaChangeEvent(this.OnStaminaChange);
        MonoSingleton<GameManager>.Instance.OnSceneChange -= new GameManager.SceneChangeEvent(this.OnHomeMenuChange);
      }
      GameUtility.DestroyGameObject((Component) this.UnitListHilit);
      GameUtility.DestroyGameObject((Component) this.ItemListHilit);
      GameUtility.DestroyGameObject((Component) this.ItemListPopupHilit);
      GameUtility.DestroyGameObjects<RectTransform>(this.ChosenUnitBadges);
      GameUtility.DestroyGameObjects<RectTransform>(this.ChosenItemBadges);
      GameUtility.DestroyGameObject((Component) this.ChosenSupportBadge);
      GameUtility.DestroyGameObject((Component) this.ItemRemoveItem);
      GameUtility.DestroyGameObjects<RectTransform>(this.mItemPoolA);
      GameUtility.DestroyGameObjects<RectTransform>(this.mItemPoolB);
      GameUtility.DestroyGameObjects<RectTransform>(this.mUnitPoolA);
      GameUtility.DestroyGameObjects<RectTransform>(this.mUnitPoolB);
      GameUtility.DestroyGameObjects<RectTransform>(this.mSupportPoolA);
      GameUtility.DestroyGameObjects<RectTransform>(this.mSupportPoolB);
      GameUtility.DestroyGameObject((Component) this.mRaidSettings);
      this.UnitList_Remove();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mMultiErrorMsg, (UnityEngine.Object) null))
      {
        UIUtility.PopCanvas();
        this.mMultiErrorMsg = (GameObject) null;
      }
      GameUtility.DestroyGameObjects<GenericSlot>(this.UnitSlots);
      GameUtility.DestroyGameObjects<GenericSlot>(this.CardSlots);
      GameUtility.DestroyGameObject(this.mConceptCardSelector);
      PartyWindow2.mInstance = (PartyWindow2) null;
    }

    private void OnCloseItemListClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 21);
    }

    private void AttachAndEnable(Transform go, Transform parent, string subPath)
    {
      if (!string.IsNullOrEmpty(subPath))
      {
        Transform transform = parent.Find(subPath);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
          parent = transform;
      }
      go.SetParent(parent, false);
      ((Component) go).gameObject.SetActive(true);
    }

    private void MoveToOrigin(GameObject go)
    {
      RectTransform component = go.GetComponent<RectTransform>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.anchoredPosition = Vector2.zero;
    }

    protected void ChangeEnabledArrowButtons(int index, int max)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextButton, (UnityEngine.Object) null))
        ((Selectable) this.NextButton).interactable = index < max - 1;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.PrevButton).interactable = index > 0;
    }

    protected virtual void ChangeEnabledTeamTabs(int index)
    {
      if (this.TeamTabs == null)
        return;
      for (int index1 = 0; index1 < this.TeamTabs.Length; ++index1)
        this.TeamTabs[index1].isOn = index1 == index;
    }

    protected virtual void OnNextTeamChange()
    {
      if (!this.OnTeamChangeImpl(this.mCurrentTeamIndex + 1) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TeamPulldown, (UnityEngine.Object) null))
        return;
      this.TeamPulldown.PrevSelection = this.mCurrentTeamIndex;
      this.TeamPulldown.Selection = this.mCurrentTeamIndex;
    }

    protected virtual void OnPrevTeamChange()
    {
      if (!this.OnTeamChangeImpl(this.mCurrentTeamIndex - 1) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TeamPulldown, (UnityEngine.Object) null))
        return;
      this.TeamPulldown.PrevSelection = this.TeamPulldown.Selection;
      this.TeamPulldown.Selection = this.mCurrentTeamIndex;
    }

    protected bool OnTeamChangeImpl(int index)
    {
      if (this.mCurrentTeamIndex == index || index >= this.mMaxTeamCount || index < 0)
        return false;
      this.ChangeEnabledArrowButtons(index, this.mMaxTeamCount);
      this.ChangeEnabledTeamTabs(index);
      this.SetCurrentParty(index);
      this.TeamChangeImpl(this.CurrentParty);
      return true;
    }

    protected bool TeamChangeImpl(PartyEditData party)
    {
      this.SetCurrentParty(party);
      this.mGuestUnit.Clear();
      this.AssignUnits(party);
      for (int slotIndex = 0; slotIndex < party.PartyData.MAX_UNIT; ++slotIndex)
        this.SetPartyUnit(slotIndex, party.Units[slotIndex]);
      if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Ordeal)
      {
        this.mCurrentSupport = this.mSupports[this.mCurrentTeamIndex];
        this.SetSupport(this.mCurrentSupport);
        if (this.mCurrentSupport != null)
        {
          if (this.mCurrentSupport.IsFriend())
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
          else
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 210);
        }
        else
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 220);
      }
      this.OnPartyMemberChange();
      this.SaveTeamPresets();
      return true;
    }

    private void OnTeamChange(int index) => this.OnTeamChangeImpl(index);

    private RectTransform OnGetItemListItem(int id, int old, RectTransform current)
    {
      if (id == 0)
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemRemoveItem, (UnityEngine.Object) null) ? ((Component) this.ItemRemoveItem).transform as RectTransform : (RectTransform) null;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemListItem, (UnityEngine.Object) null))
        return (RectTransform) null;
      RectTransform parent;
      if (old <= 0)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemRemoveItem, (UnityEngine.Object) null) && UnityEngine.Object.op_Equality((UnityEngine.Object) ((Component) this.ItemRemoveItem).transform, (UnityEngine.Object) current))
          ((Component) this.ItemRemoveItem).transform.SetParent((Transform) UIUtility.Pool, false);
        if (this.mItemPoolA.Count <= 0)
        {
          parent = ((Component) this.CreateItemListItem()).transform as RectTransform;
          this.mItemPoolB.Add(parent);
        }
        else
        {
          parent = this.mItemPoolA[this.mItemPoolA.Count - 1];
          this.mItemPoolA.RemoveAt(this.mItemPoolA.Count - 1);
          this.mItemPoolB.Add(parent);
        }
      }
      else
        parent = current;
      DataSource.Bind<ItemData>(((Component) parent).gameObject, this.mOwnItems[id - 1]);
      ((Component) parent).gameObject.SetActive(true);
      GameParameter.UpdateAll(((Component) parent).gameObject);
      int index1 = Array.FindIndex<ItemData>(this.mCurrentItems, (Predicate<ItemData>) (p => p != null && p.Param == this.mOwnItems[id - 1].Param));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemListHilit, (UnityEngine.Object) null))
      {
        if (this.mSelectedSlotIndex == index1)
          this.AttachAndEnable((Transform) this.ItemListHilit, (Transform) parent, this.ItemListHilitParent);
        else if (UnityEngine.Object.op_Equality((UnityEngine.Object) ((Transform) this.ItemListHilit).parent, (UnityEngine.Object) ((Transform) parent).Find(this.ItemListHilitParent)))
        {
          ((Transform) this.ItemListHilit).SetParent((Transform) UIUtility.Pool, false);
          ((Component) this.ItemListHilit).gameObject.SetActive(false);
        }
      }
      if (index1 >= 0)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChosenItemBadges[index1], (UnityEngine.Object) null))
        {
          ((Transform) this.ChosenItemBadges[index1]).SetParent((Transform) parent, false);
          ((Component) this.ChosenItemBadges[index1]).gameObject.SetActive(true);
        }
      }
      else
      {
        for (int index2 = 0; index2 < this.ChosenItemBadges.Length; ++index2)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChosenItemBadges[index2], (UnityEngine.Object) null) && UnityEngine.Object.op_Equality((UnityEngine.Object) ((Transform) this.ChosenItemBadges[index2]).parent, (UnityEngine.Object) parent))
          {
            ((Transform) this.ChosenItemBadges[index2]).SetParent((Transform) UIUtility.Pool, false);
            ((Component) this.ChosenItemBadges[index2]).gameObject.SetActive(false);
            break;
          }
        }
      }
      return parent;
    }

    private ListItemEvents CreateItemListItem()
    {
      ListItemEvents itemListItem = UnityEngine.Object.Instantiate<ListItemEvents>(this.ItemListItem);
      itemListItem.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
      return itemListItem;
    }

    private void RefreshItemList()
    {
      ItemData currentItem = this.mCurrentItems[this.mSelectedSlotIndex];
      this.ItemList.ClearItems();
      if (currentItem != null || this.AlwaysShowRemoveItem)
        this.ItemList.AddItem(0);
      List<ItemData> itemDataList = new List<ItemData>((IEnumerable<ItemData>) this.mOwnItems);
      for (int index = 0; index < itemDataList.Count; ++index)
      {
        if (itemDataList[index].ItemType != EItemType.Used || itemDataList[index].Skill == null)
          itemDataList.RemoveAt(index--);
      }
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = 0; index < this.mCurrentItems.Length; ++index)
      {
        if (this.mCurrentItems[index] != null)
        {
          ItemData itemDataByItemParam = player.FindItemDataByItemParam(this.mCurrentItems[index].Param);
          if (!itemDataByItemParam.Param.IsExpire)
          {
            this.ItemList.AddItem(this.mOwnItems.IndexOf(itemDataByItemParam) + 1);
            itemDataList.Remove(itemDataByItemParam);
          }
        }
      }
      switch (this.mItemFilter)
      {
        case PartyWindow2.ItemFilterTypes.All:
          for (int index = 0; index < itemDataList.Count; ++index)
          {
            if (!itemDataList[index].Param.IsExpire && itemDataList[index].ItemType == EItemType.Used && itemDataList[index].Skill != null)
              this.ItemList.AddItem(this.mOwnItems.IndexOf(itemDataList[index]) + 1);
          }
          break;
        case PartyWindow2.ItemFilterTypes.Offense:
          for (int index = 0; index < itemDataList.Count; ++index)
          {
            if (!itemDataList[index].Param.IsExpire && itemDataList[index].ItemType == EItemType.Used && itemDataList[index].Skill != null && (itemDataList[index].Skill.EffectType == SkillEffectTypes.Attack || itemDataList[index].Skill.EffectType == SkillEffectTypes.Debuff || itemDataList[index].Skill.EffectType == SkillEffectTypes.FailCondition || itemDataList[index].Skill.EffectType == SkillEffectTypes.RateDamage || itemDataList[index].Skill.EffectType == SkillEffectTypes.RateDamageCurrent))
              this.ItemList.AddItem(this.mOwnItems.IndexOf(itemDataList[index]) + 1);
          }
          break;
        case PartyWindow2.ItemFilterTypes.Support:
          for (int index = 0; index < itemDataList.Count; ++index)
          {
            if (!itemDataList[index].Param.IsExpire && itemDataList[index].ItemType == EItemType.Used && itemDataList[index].Skill != null && (itemDataList[index].Skill.EffectType == SkillEffectTypes.Heal || itemDataList[index].Skill.EffectType == SkillEffectTypes.RateHeal || itemDataList[index].Skill.EffectType == SkillEffectTypes.Buff || itemDataList[index].Skill.EffectType == SkillEffectTypes.CureCondition || itemDataList[index].Skill.EffectType == SkillEffectTypes.DisableCondition || itemDataList[index].Skill.EffectType == SkillEffectTypes.Revive || itemDataList[index].Skill.EffectType == SkillEffectTypes.GemsIncDec))
              this.ItemList.AddItem(this.mOwnItems.IndexOf(itemDataList[index]) + 1);
          }
          break;
      }
      this.ItemList.Refresh(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemListHilit, (UnityEngine.Object) null))
      {
        ((Component) this.ItemListHilit).gameObject.SetActive(false);
        ((Transform) this.ItemListHilit).SetParent(((Component) this).transform, false);
        if (currentItem != null)
        {
          int itemID = this.mOwnItems.FindIndex((Predicate<ItemData>) (p => p.Param == currentItem.Param)) + 1;
          if (itemID > 0)
          {
            RectTransform parent = this.ItemList.FindItem(itemID);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) parent, (UnityEngine.Object) null))
              this.AttachAndEnable((Transform) this.ItemListHilit, (Transform) parent, this.ItemListHilitParent);
          }
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemListPopupHilit, (UnityEngine.Object) null))
      {
        ((Component) this.ItemListPopupHilit).gameObject.SetActive(false);
        ((Transform) this.ItemListPopupHilit).SetParent(((Component) this).transform, false);
        this.AttachAndEnable((Transform) this.ItemListPopupHilit, ((Component) this.PopupItemSlots[this.mSelectedSlotIndex]).transform, this.ItemListPopupHilitParent);
        if (currentItem == null)
          ;
      }
      for (int index = 0; index < this.ChosenItemBadges.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChosenItemBadges[index], (UnityEngine.Object) null))
        {
          ((Transform) this.ChosenItemBadges[index]).SetParent((Transform) UIUtility.Pool, false);
          ((Component) this.ChosenItemBadges[index]).gameObject.SetActive(false);
        }
      }
      for (int i = 0; i < this.ChosenItemBadges.Length; ++i)
      {
        if (this.mCurrentItems[i] != null)
        {
          RectTransform rectTransform = this.ItemList.FindItem(this.mOwnItems.FindIndex((Predicate<ItemData>) (p => p.Param == this.mCurrentItems[i].Param)) + 1);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) rectTransform, (UnityEngine.Object) null))
          {
            ((Transform) this.ChosenItemBadges[i]).SetParent((Transform) rectTransform, false);
            ((Component) this.ChosenItemBadges[i]).gameObject.SetActive(true);
          }
        }
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 20);
    }

    private void OnItemSlotClick(GenericSlot slot, bool interactable)
    {
      if (!interactable || !this.mInitialized)
        return;
      int num = Array.IndexOf<GenericSlot>(this.ItemSlots, slot);
      if (num < 0 || 5 <= num)
        return;
      this.mSelectedSlotIndex = num;
      this.RefreshItemList();
    }

    private void OnPopupItemSlotClick(GenericSlot slot, bool interactable)
    {
      if (!interactable || !this.mInitialized)
        return;
      int num = Array.IndexOf<GenericSlot>(this.PopupItemSlots, slot);
      if (num < 0 || 5 <= num)
        return;
      this.mSelectedSlotIndex = num;
      this.RefreshItemList();
    }

    private void OnItemRemoveSelect(SRPG_Button button)
    {
      this.SetItemSlot(this.mSelectedSlotIndex, (ItemData) null);
      this.OnItemSlotsChange();
      this.RefreshItemList();
      this.SaveInventory();
      if (this.PopupItemSlots != null && this.PopupItemSlots.Length > 0)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 21);
    }

    private void OnItemSlotOpenClick(SRPG_Button go)
    {
      if (!this.mInitialized)
        return;
      this.mSelectedSlotIndex = 0;
      this.RefreshItemList();
    }

    private void OnItemSelect(GameObject go)
    {
      int selectedSlotIndex = this.mSelectedSlotIndex;
      ItemData mCurrentItem = this.mCurrentItems[this.mSelectedSlotIndex];
      while (0 < selectedSlotIndex && this.mCurrentItems[selectedSlotIndex - 1] == null)
        --selectedSlotIndex;
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
      if (dataOfClass != null && dataOfClass != mCurrentItem)
      {
        int slotIndex = -1;
        for (int index = 0; index < this.mCurrentItems.Length; ++index)
        {
          if (this.mCurrentItems[index] != null && this.mCurrentItems[index].Param == dataOfClass.Param)
          {
            slotIndex = index;
            break;
          }
        }
        if (slotIndex >= 0)
        {
          if (mCurrentItem != null)
          {
            this.SetItemSlot(selectedSlotIndex, dataOfClass);
            this.SetItemSlot(slotIndex, mCurrentItem);
          }
          else
            --selectedSlotIndex;
        }
        else
          this.SetItemSlot(selectedSlotIndex, dataOfClass);
      }
      this.mSelectedSlotIndex = Mathf.Min(selectedSlotIndex + 1, 4);
      this.RefreshItemList();
      this.OnItemSlotsChange();
      this.SaveInventory();
      if (this.PopupItemSlots != null && this.PopupItemSlots.Length > 0)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 21);
    }

    private void OnPartyMemberChange()
    {
      this.RefreshSankaStates();
      this.RefreshSameUnitStates();
      this.RecalcTotalCombatPower();
      this.UpdateLeaderSkills();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AddMainUnitOverlay, (UnityEngine.Object) null))
      {
        this.AddMainUnitOverlay.SetActive(false);
        for (int mainmemberStart = this.CurrentParty.PartyData.MAINMEMBER_START; mainmemberStart <= this.CurrentParty.PartyData.MAINMEMBER_END; ++mainmemberStart)
        {
          if (this.CurrentParty.Units[mainmemberStart] == null && mainmemberStart < this.UnitSlots.Length && mainmemberStart < this.mSlotData.Count && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[mainmemberStart], (UnityEngine.Object) null) && (this.mLockedPartySlots & 1 << mainmemberStart) == 0 && this.mSlotData[mainmemberStart].Type == SRPG.PartySlotType.Free)
          {
            this.AddMainUnitOverlay.transform.SetParent(((Component) this.UnitSlots[mainmemberStart]).transform, false);
            this.AddMainUnitOverlay.SetActive(true);
            break;
          }
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AddSubUnitOverlay, (UnityEngine.Object) null))
        return;
      this.AddSubUnitOverlay.SetActive(false);
      for (int submemberStart = this.CurrentParty.PartyData.SUBMEMBER_START; submemberStart <= this.CurrentParty.PartyData.SUBMEMBER_END; ++submemberStart)
      {
        if (this.CurrentParty.Units[submemberStart] == null && submemberStart < this.UnitSlots.Length && submemberStart < this.mSlotData.Count && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[submemberStart], (UnityEngine.Object) null) && (this.mLockedPartySlots & 1 << submemberStart) == 0 && this.mSlotData[submemberStart].Type == SRPG.PartySlotType.Free)
        {
          this.AddSubUnitOverlay.transform.SetParent(((Component) this.UnitSlots[submemberStart]).transform, false);
          this.AddSubUnitOverlay.SetActive(true);
          break;
        }
      }
    }

    protected virtual void OnItemSlotsChange()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AddItemOverlay, (UnityEngine.Object) null))
      {
        this.AddItemOverlay.SetActive(false);
        for (int index = 0; index < this.mCurrentItems.Length; ++index)
        {
          if (this.mCurrentItems[index] == null && index < this.ItemSlots.Length && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemSlots[index], (UnityEngine.Object) null) && !this.mItemsLocked)
          {
            this.AddItemOverlay.transform.SetParent(((Component) this.ItemSlots[index]).transform, false);
            this.AddItemOverlay.SetActive(true);
            break;
          }
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AddPopupItemOverlay, (UnityEngine.Object) null))
        return;
      this.AddPopupItemOverlay.SetActive(false);
      for (int index = 0; index < this.mCurrentItems.Length; ++index)
      {
        if (this.mCurrentItems[index] == null && index < this.PopupItemSlots.Length && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PopupItemSlots[index], (UnityEngine.Object) null) && !this.mItemsLocked)
        {
          this.AddPopupItemOverlay.transform.SetParent(((Component) this.PopupItemSlots[index]).transform, false);
          this.AddPopupItemOverlay.SetActive(true);
          break;
        }
      }
    }

    private void UpdateLeaderSkills()
    {
      UnitData leader = (UnitData) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeaderSkill, (UnityEngine.Object) null))
      {
        SkillParam data = (SkillParam) null;
        if (this.mIsHeloOnly)
        {
          if (this.mGuestUnit != null && this.mGuestUnit.Count > 0)
          {
            if (this.mGuestUnit[0].CurrentLeaderSkill != null)
              data = this.mGuestUnit[0].CurrentLeaderSkill.SkillParam;
            leader = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mGuestUnit[0].UniqueID);
          }
        }
        else if (this.CurrentParty.Units[0] != null)
        {
          if (this.CurrentParty.Units[0].CurrentLeaderSkill != null)
            data = this.CurrentParty.Units[0].CurrentLeaderSkill.SkillParam;
          leader = this.CurrentParty.Units[0];
        }
        else if (this.mSlotData[0].Type == SRPG.PartySlotType.Npc || this.mSlotData[0].Type == SRPG.PartySlotType.NpcHero)
        {
          UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.mSlotData[0].UnitName);
          if (unitParam != null && unitParam.leader_skills != null && unitParam.leader_skills.Length >= 4)
          {
            string leaderSkill = unitParam.leader_skills[4];
            if (!string.IsNullOrEmpty(leaderSkill))
              data = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(leaderSkill);
          }
        }
        else if (this.EnableHeroSolo && this.mGuestUnit != null && this.mGuestUnit.Count > 0 && this.mGuestUnit[0].CurrentLeaderSkill != null)
        {
          data = this.mGuestUnit[0].CurrentLeaderSkill.SkillParam;
          leader = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mGuestUnit[0].UniqueID);
        }
        if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.GuildRaid)
        {
          GuildRaidManager instance = GuildRaidManager.Instance;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.IsForcedDeck && instance.BattleType == GuildRaidBattleType.Main)
          {
            if (this.CurrentParty.Units[0].CurrentLeaderSkill != null)
              data = this.CurrentParty.Units[0].CurrentLeaderSkill.SkillParam;
            leader = this.CurrentParty.Units[0];
            ConceptCardSlot component = ((Component) this.CardSlots[0]).GetComponent<ConceptCardSlot>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.SetLeaderUnit(leader);
          }
        }
        this.LeaderSkill.SetSlotData<SkillParam>(data);
        long uniqueId = leader == null ? 0L : leader.UniqueID;
        GlobalVars.SelectedLSChangeUnitUniqueID.Set(uniqueId);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChangeLeaderSkillButton, (UnityEngine.Object) null))
          ((Selectable) this.ChangeLeaderSkillButton).interactable = leader != null && leader.MainConceptCard != null && leader.MainConceptCard.LeaderSkillIsAvailable();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeaderSkillBGImageArray, (UnityEngine.Object) null))
          this.LeaderSkillBGImageArray.ImageIndex = leader == null || !leader.IsEquipConceptLeaderSkill() ? 0 : 1;
      }
      bool flag1 = false;
      float num = 1f;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SupportSkill, (UnityEngine.Object) null))
      {
        SkillParam data = (SkillParam) null;
        if (this.mCurrentSupport != null)
        {
          if (this.mCurrentSupport.Unit.CurrentLeaderSkill != null)
            data = this.mCurrentSupport.Unit.CurrentLeaderSkill.SkillParam;
          if (leader != null && leader.IsEquipConceptLeaderSkill())
          {
            data = (SkillParam) null;
            ConceptCardData mainConceptCard1 = leader.MainConceptCard;
            ConceptCardData mainConceptCard2 = this.mCurrentSupport.Unit.MainConceptCard;
            if (this.mCurrentSupport.IsFriend() && mainConceptCard1 != null && mainConceptCard1.Param.concept_card_groups != null && mainConceptCard2 != null)
            {
              bool flag2 = false;
              for (int index = 0; index < mainConceptCard1.Param.concept_card_groups.Length; ++index)
                flag2 |= MonoSingleton<GameManager>.Instance.MasterParam.CheckConceptCardgroup(mainConceptCard1.Param.concept_card_groups[index], mainConceptCard2.Param);
              if (flag2)
              {
                flag1 = true;
                num = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardLsBuffFriendCoef((int) mainConceptCard2.Rarity, (int) mainConceptCard2.AwakeCount);
              }
            }
          }
        }
        this.SupportSkill.SetSlotData<SkillParam>(data);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConceptCardBonus, (UnityEngine.Object) null))
      {
        this.ConceptCardBonus.SetActive(flag1);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConceptCardBonusRate, (UnityEngine.Object) null))
          this.ConceptCardBonusRate.text = num.ToString();
      }
      for (int index = 0; index < this.CardSlots.Length; ++index)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.CardSlots[index], (UnityEngine.Object) null) && !UnityEngine.Object.op_Equality((UnityEngine.Object) ((Component) this.CardSlots[index]).gameObject, (UnityEngine.Object) null))
          GameParameter.UpdateAll(((Component) this.CardSlots[index]).gameObject);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendCardSlot, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) ((Component) this.FriendCardSlot).gameObject, (UnityEngine.Object) null))
        return;
      GameParameter.UpdateAll(((Component) this.FriendCardSlot).gameObject);
    }

    private void OnSlotChange(GameObject go)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) go, (UnityEngine.Object) null) || string.IsNullOrEmpty(this.SlotChangeTrigger))
        return;
      Animator component = go.GetComponent<Animator>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.SetTrigger(this.SlotChangeTrigger);
    }

    private void RefreshUnitSlots()
    {
      if (this.UnitSlots == null)
        return;
      for (int index = 0; index < this.UnitSlots.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null))
        {
          this.OnSlotChange(((Component) this.UnitSlots[index]).gameObject);
          if (this.CardSlots != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[index], (UnityEngine.Object) null))
            this.OnSlotChange(((Component) this.CardSlots[index]).gameObject);
        }
      }
    }

    private void SetSupport(SupportData support)
    {
      if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Ordeal)
      {
        this.mSupports[this.mCurrentTeamIndex] = support;
      }
      else
      {
        for (int index = 0; index < this.mSupports.Count; ++index)
          this.mSupports[index] = support;
      }
      this.mCurrentSupport = support;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
        return;
      this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
      this.FriendSlot.SetSlotData<SupportData>(support);
      if (support == null)
      {
        this.FriendSlot.SetSlotData<UnitData>((UnitData) null);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendCardSlot, (UnityEngine.Object) null))
          return;
        DataSource.Bind<UnitData>(((Component) this.FriendCardSlot).gameObject, (UnitData) null);
        this.FriendCardSlot.SetSlotData<ConceptCardData>((ConceptCardData) null);
        ((Component) this.FriendCardSlot).GetComponent<ConceptCardIcon>().Setup((ConceptCardData) null);
      }
      else
      {
        this.FriendSlot.SetSlotData<UnitData>(support.Unit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendCardSlot, (UnityEngine.Object) null))
        {
          DataSource.Bind<UnitData>(((Component) this.FriendCardSlot).gameObject, support.Unit);
          this.FriendCardSlot.SetSlotData<ConceptCardData>(support.Unit.MainConceptCard);
          ((Component) this.FriendCardSlot).GetComponent<ConceptCardIcon>().Setup(support.Unit.MainConceptCard);
        }
        this.OnSlotChange(((Component) this.FriendSlot).gameObject);
        if (support.Unit.MainConceptCard == null || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendCardSlot, (UnityEngine.Object) null))
          return;
        this.OnSlotChange(((Component) this.FriendCardSlot).gameObject);
      }
    }

    protected virtual void SetItemSlot(int slotIndex, ItemData item)
    {
      if (item == null)
      {
        for (int index = slotIndex; index < 4; ++index)
        {
          this.mCurrentItems[index] = this.mCurrentItems[index + 1];
          this.ItemSlots[index].SetSlotData<ItemData>(this.mCurrentItems[index]);
          if (index < this.PopupItemSlots.Length && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PopupItemSlots[index], (UnityEngine.Object) null))
            this.PopupItemSlots[index].SetSlotData<ItemData>(this.mCurrentItems[index]);
        }
        int index1 = this.mCurrentItems.Length - 1;
        this.mCurrentItems[index1] = (ItemData) null;
        this.ItemSlots[index1].SetSlotData<ItemData>(this.mCurrentItems[index1]);
        if (index1 >= this.PopupItemSlots.Length || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PopupItemSlots[index1], (UnityEngine.Object) null))
          return;
        this.PopupItemSlots[index1].SetSlotData<ItemData>(this.mCurrentItems[index1]);
      }
      else
      {
        int num = Math.Min(item.Num, item.Param.invcap);
        ItemData data = new ItemData();
        data.Setup(item.UniqueID, item.Param, num);
        this.mCurrentItems[slotIndex] = data;
        this.ItemSlots[slotIndex].SetSlotData<ItemData>(data);
        if (slotIndex < this.PopupItemSlots.Length && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PopupItemSlots[slotIndex], (UnityEngine.Object) null))
        {
          this.PopupItemSlots[slotIndex].SetSlotData<ItemData>(data);
          this.OnSlotChange(((Component) this.PopupItemSlots[slotIndex]).gameObject);
        }
        this.OnSlotChange(((Component) this.ItemSlots[slotIndex]).gameObject);
      }
    }

    private void SetPartyUnit(
      int slotIndex,
      UnitData unit,
      bool isSlotChange = true,
      bool edit_party_only = false)
    {
      if (slotIndex < 0 || slotIndex >= this.mSlotData.Count || !this.IsSettableSlot(this.mSlotData[slotIndex]))
        return;
      if (unit == null)
      {
        int num1 = slotIndex >= this.CurrentParty.PartyData.MAX_MAINMEMBER ? this.CurrentParty.PartyData.MAX_UNIT : this.CurrentParty.PartyData.MAX_MAINMEMBER;
        for (int index = slotIndex; index < num1; ++index)
        {
          if (this.mSlotData[index].Type == SRPG.PartySlotType.Free)
          {
            int num2 = 1;
            while (index + num2 < num1 && this.mSlotData[index + num2].Type != SRPG.PartySlotType.Free)
              ++num2;
            if (index + num2 < num1)
            {
              this.CurrentParty.Units[index] = this.CurrentParty.Units[index + num2];
              if (!edit_party_only)
              {
                this.UnitSlots[index].SetSlotData<QuestParam>(this.mCurrentQuest);
                this.UnitSlots[index].SetSlotData<UnitData>(this.CurrentParty.Units[index]);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[index], (UnityEngine.Object) null))
                {
                  DataSource.Bind<UnitData>(((Component) this.CardSlots[index]).gameObject, this.CurrentParty.Units[index]);
                  this.CardSlots[index].SetSlotData<ConceptCardData>(this.CurrentParty.Units[index] != null ? this.CurrentParty.Units[index].MainConceptCard : (ConceptCardData) null);
                  ((Component) this.CardSlots[index]).GetComponent<ConceptCardIcon>().Setup(this.CurrentParty.Units[index] != null ? this.CurrentParty.Units[index].MainConceptCard : (ConceptCardData) null);
                }
              }
              this.CurrentParty.Units[index + num2] = (UnitData) null;
              if (!edit_party_only)
              {
                this.UnitSlots[index + num2].SetSlotData<QuestParam>(this.mCurrentQuest);
                this.UnitSlots[index + num2].SetSlotData<UnitData>((UnitData) null);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[index + num2], (UnityEngine.Object) null))
                {
                  DataSource.Bind<UnitData>(((Component) this.CardSlots[index + num2]).gameObject, (UnitData) null);
                  this.CardSlots[index + num2].SetSlotData<ConceptCardData>((ConceptCardData) null);
                  ((Component) this.CardSlots[index + num2]).GetComponent<ConceptCardIcon>().Setup((ConceptCardData) null);
                }
              }
            }
            else
            {
              this.CurrentParty.Units[index] = (UnitData) null;
              if (!edit_party_only)
              {
                this.UnitSlots[index].SetSlotData<QuestParam>(this.mCurrentQuest);
                this.UnitSlots[index].SetSlotData<UnitData>((UnitData) null);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[index], (UnityEngine.Object) null))
                {
                  DataSource.Bind<UnitData>(((Component) this.CardSlots[index]).gameObject, (UnitData) null);
                  this.CardSlots[index].SetSlotData<ConceptCardData>((ConceptCardData) null);
                  ((Component) this.CardSlots[index]).GetComponent<ConceptCardIcon>().Setup((ConceptCardData) null);
                }
              }
            }
          }
        }
        this.BeforeSetPartyUnit();
      }
      else
      {
        this.CurrentParty.Units[slotIndex] = unit;
        if (!edit_party_only)
        {
          this.UnitSlots[slotIndex].SetSlotData<QuestParam>(this.mCurrentQuest);
          this.UnitSlots[slotIndex].SetSlotData<UnitData>(unit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[slotIndex], (UnityEngine.Object) null))
          {
            DataSource.Bind<UnitData>(((Component) this.CardSlots[slotIndex]).gameObject, unit);
            this.CardSlots[slotIndex].SetSlotData<ConceptCardData>(unit.MainConceptCard);
            ((Component) this.CardSlots[slotIndex]).GetComponent<ConceptCardIcon>().Setup(unit.MainConceptCard);
          }
        }
        if (!edit_party_only)
        {
          this.RefreshSankaStates();
          this.RefreshSameUnitStates();
          if (isSlotChange)
          {
            this.OnSlotChange(((Component) this.UnitSlots[slotIndex]).gameObject);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[slotIndex], (UnityEngine.Object) null))
              this.OnSlotChange(((Component) this.CardSlots[slotIndex]).gameObject);
          }
        }
        this.BeforeSetPartyUnit();
      }
    }

    protected virtual void BeforeSetPartyUnit()
    {
    }

    private void SetPartyUnitForce(int slotIndex, UnitData unit, bool isSlotChange = true)
    {
      if (slotIndex < 0 || slotIndex >= this.mSlotData.Count || !this.IsSettableSlot(this.mSlotData[slotIndex]))
        return;
      this.CurrentParty.Units[slotIndex] = unit;
      this.UnitSlots[slotIndex].SetSlotData<QuestParam>(this.mCurrentQuest);
      this.UnitSlots[slotIndex].SetSlotData<UnitData>(unit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[slotIndex], (UnityEngine.Object) null))
      {
        DataSource.Bind<UnitData>(((Component) this.CardSlots[slotIndex]).gameObject, unit);
        this.CardSlots[slotIndex].SetSlotData<ConceptCardData>(unit?.MainConceptCard);
        ((Component) this.CardSlots[slotIndex]).GetComponent<ConceptCardIcon>().Setup(unit?.MainConceptCard);
      }
      this.RefreshSankaStates();
      this.RefreshSameUnitStates();
      if (!isSlotChange)
        return;
      this.OnSlotChange(((Component) this.UnitSlots[slotIndex]).gameObject);
      if (unit == null || unit.MainConceptCard == null)
        return;
      this.OnSlotChange(((Component) this.CardSlots[slotIndex]).gameObject);
    }

    private bool OnHomeMenuChange()
    {
      if (this.mCurrentQuest != null && this.mCurrentQuest.IsGvG)
        return true;
      if (this.IsPartyDirty)
      {
        if (!this.mIsSaving)
          this.SaveParty((PartyWindow2.Callback) null, (PartyWindow2.Callback) null);
        MonoSingleton<GameManager>.Instance.RegisterImportantJob(this.StartCoroutine(this.WaitForSave()));
      }
      this.SaveInventory();
      return true;
    }

    [DebuggerHidden]
    private IEnumerator PopulateItemList()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PartyWindow2.\u003CPopulateItemList\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    protected virtual void RecalcTotalCombatPower()
    {
      long num = (long) PartyUtility.CalcTotalCombatPower(this.CurrentParty, this.mLockedPartySlots, this.mOwnUnits, !this.mSupportLocked ? this.mCurrentSupport : (SupportData) null, this.mGuestUnit);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TotalCombatPower, (UnityEngine.Object) null))
        return;
      this.TotalCombatPower.text = num.ToString();
    }

    private bool IsMultiTowerPartySlot(int index)
    {
      return this.mCurrentPartyType == PartyWindow2.EditPartyTypes.MultiTower && (index == 0 || index == 1 || index == 2);
    }

    protected virtual void OnUnitSlotClick(GenericSlot slot, bool interactable)
    {
      if (!this.mInitialized || this.mIsSaving)
        return;
      this.Refresh(true);
      int index = Array.IndexOf<GenericSlot>(this.UnitSlots, slot);
      if (0 <= index && index < this.CurrentParty.PartyData.MAX_UNIT)
      {
        this.mUnitSlotSelected = true;
        this.mSelectedSlotIndex = index;
        this.UnitList_Show();
      }
      else
      {
        if (!this.IsMultiTowerPartySlot(index))
          return;
        this.mUnitSlotSelected = true;
        this.mSelectedSlotIndex = index;
        this.UnitList_Show();
      }
    }

    private void OnCardSlotClick(GenericSlot slot, bool interactable)
    {
      if (!this.mInitialized || this.mIsSaving)
        return;
      this.Refresh(true);
      int index = Array.IndexOf<GenericSlot>(this.CardSlots, slot);
      if (0 <= index && index < this.CurrentParty.PartyData.MAX_UNIT)
      {
        this.mSelectedSlotIndex = index;
        this.CardList_Show();
      }
      else
      {
        if (!this.IsMultiTowerPartySlot(index))
          return;
        this.mSelectedSlotIndex = index;
        this.CardList_Show();
      }
    }

    private void OnSupportUnitSlotClick(GenericSlot slot, bool interactable)
    {
      if (!this.mInitialized || this.mIsSaving)
        return;
      this.Refresh(true);
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) slot))
        return;
      this.mUnitSlotSelected = true;
      this.SupportList_Show();
    }

    private UnitData[] RefreshUnits(UnitData[] units)
    {
      List<UnitData> source = new List<UnitData>((IEnumerable<UnitData>) this.mOwnUnits);
      List<UnitData> unitDataList = new List<UnitData>();
      bool flag1 = this.CurrentParty.Units[this.mSelectedSlotIndex] == null;
      bool flag2 = PartyUtility.IsHeroesAvailable(this.mCurrentPartyType, this.mCurrentQuest);
      if (this.UseQuestInfo)
      {
        string[] strArray = this.mCurrentQuest.questParty == null ? this.mCurrentQuest.units.GetList() : ((IEnumerable<PartySlotTypeUnitPair>) this.mCurrentQuest.questParty.GetMainSubSlots()).Where<PartySlotTypeUnitPair>((Func<PartySlotTypeUnitPair, bool>) (slot => slot.Type == SRPG.PartySlotType.ForcedHero || slot.Type == SRPG.PartySlotType.Forced)).Select<PartySlotTypeUnitPair, string>((Func<PartySlotTypeUnitPair, string>) (slot => slot.Unit)).ToArray<string>();
        if (strArray != null)
        {
          for (int index = 0; index < strArray.Length; ++index)
          {
            string chQuestHeroId = strArray[index];
            UnitData unitData = source.FirstOrDefault<UnitData>((Func<UnitData, bool>) (u => u.UnitParam.iname == chQuestHeroId));
            if (unitData != null)
              source.Remove(unitData);
          }
        }
      }
      int num = 0;
      for (int mainmemberStart = this.CurrentParty.PartyData.MAINMEMBER_START; mainmemberStart <= this.CurrentParty.PartyData.MAINMEMBER_END; ++mainmemberStart)
      {
        if (this.CurrentParty.Units[mainmemberStart] != null)
          ++num;
      }
      for (int i = 0; i < this.CurrentParty.PartyData.MAX_UNIT; ++i)
      {
        if (this.CurrentParty.Units[i] != null && (flag2 || !this.CurrentParty.Units[i].UnitParam.IsHero()) && (this.CurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.CurrentParty.PartyData.SUBMEMBER_END || i != 0 || !flag1 || num > 1))
        {
          if (this.UseQuestInfo)
          {
            string empty = string.Empty;
            if (!this.mCurrentQuest.IsEntryQuestCondition(this.CurrentParty.Units[i], ref empty))
              continue;
          }
          UnitData unitData = source.Find((Predicate<UnitData>) (v => v.UniqueID == this.CurrentParty.Units[i].UniqueID));
          if (unitData != null)
            unitDataList.Add(unitData);
          int index = source.FindIndex((Predicate<UnitData>) (v => v.UniqueID == this.CurrentParty.Units[i].UniqueID));
          if (index >= 0)
            source.RemoveAt(index);
        }
      }
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      bool flag3 = UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.CurrentState == MyPhoton.MyState.ROOM;
      List<UnitData> collection = new List<UnitData>();
      for (int index = 0; index < source.Count; ++index)
      {
        if ((flag2 || !source[index].UnitParam.IsHero()) && (this.CurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.CurrentParty.PartyData.SUBMEMBER_END || source[index] != this.CurrentParty.Units[0] || !flag1 || num > 1))
        {
          if (flag3)
          {
            MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
            if (currentRoom != null)
            {
              JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
              if (source[index].CalcLevel() < myPhotonRoomParam.unitlv)
                continue;
            }
          }
          collection.Add(source[index]);
        }
      }
      unitDataList.AddRange((IEnumerable<UnitData>) collection);
      for (int index = 0; index < unitDataList.Count; ++index)
        unitDataList[index] = UnitOverWriteUtility.Apply(unitDataList[index], (eOverWritePartyType) GlobalVars.OverWritePartyType);
      return unitDataList.ToArray();
    }

    private bool IsPartyDirty
    {
      get
      {
        PartyData partyOfType = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(this.mCurrentPartyType.ToPlayerPartyType());
        for (int index = 0; index < partyOfType.MAX_UNIT; ++index)
        {
          long uniqueId = this.CurrentParty.Units[index] == null ? 0L : this.CurrentParty.Units[index].UniqueID;
          if (partyOfType.GetUnitUniqueID(index) != uniqueId)
            return true;
        }
        return false;
      }
    }

    private void OnRaidClick(SRPG_Button button)
    {
      if (this.mCurrentQuest == null || this.mCurrentQuest.type == QuestTypes.StoryExtra && MonoSingleton<GameManager>.Instance.Player.StoryExChallengeCount.IsRestChallengeCount_Zero() || !this.mCurrentQuest.CheckEnableChallange() || this.IsSameUnitInParty(this.CurrentParty.Units))
        return;
      if (!this.mCurrentQuest.IsGenAdvBoss)
      {
        if (MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mCurrentQuest.ticket) <= 0)
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentQuest.ticket);
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.NO_RAID_TICKET", itemParam == null ? (object) (string) null : (object) itemParam.name), (UIUtility.DialogResultEvent) (go => { }));
          return;
        }
      }
      else if (this.GenAdvBossNoTicketConfirm())
        return;
      if (MonoSingleton<GameManager>.Instance.Player != null && MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress != null && MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.IsExistRecord)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
      else if (MonoSingleton<GameManager>.Instance.Player.IsRuneStorageFull)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) this.Raid))
        this.PrepareRaid(1, !((Selectable) button).IsInteractable());
      else
        this.ShowRaidSettings();
    }

    private void OnRaidAPPlus(SRPG_Button button, bool is_refresh_btn_interactable)
    {
      this.mMultiRaidNum = Mathf.Min(this.MaxRaidNum, this.mMultiRaidNum + 1);
      this.RefreshRaidButtons(is_refresh_btn_interactable);
    }

    private void OnRaidAPMinus(SRPG_Button button, bool is_refresh_btn_interactable)
    {
      this.mMultiRaidNum = Mathf.Max(1, this.mMultiRaidNum - 1);
      this.RefreshRaidButtons(is_refresh_btn_interactable);
    }

    private void OnRaidAccept(GameObject go)
    {
      if (this.mNumRaids <= 0)
        return;
      this.LockWindow(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRaidSettings, (UnityEngine.Object) null))
      {
        this.mRaidSettings.Close();
        this.mRaidSettings = (RaidSettingsWindow) null;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRaidResultWindow, (UnityEngine.Object) null) && this.mReqRaidResultWindow == null)
        this.mReqRaidResultWindow = AssetManager.LoadAsync<RaidResultWindow>(this.RaidResultPrefab);
      if (this.IsPartyDirty)
        this.SaveParty((PartyWindow2.Callback) (() => this.StartRaid()), (PartyWindow2.Callback) null);
      else
        this.StartRaid();
    }

    private void StartRaid()
    {
      for (int index = 0; index < this.mNumRaids; ++index)
        MonoSingleton<GameManager>.Instance.Player.IncrementQuestChallangeNumDaily(this.mCurrentQuest.iname);
      if (this.mCurrentQuest.IsGenesisBoss)
        SRPG.Network.RequestAPI((WebAPI) new ReqGenesisBossBtlSkip(this.mCurrentQuest.ChapterID, this.mCurrentQuest.iname, this.mCurrentQuest.difficulty, this.mNumRaids, new SRPG.Network.ResponseCallback(this.RecvRaidResult)));
      else if (this.mCurrentQuest.IsAdvanceBoss)
        SRPG.Network.RequestAPI((WebAPI) new ReqAdvanceBossBtlSkip(this.mCurrentQuest.ChapterID, this.mCurrentQuest.iname, this.mCurrentQuest.difficulty, this.mNumRaids, new SRPG.Network.ResponseCallback(this.RecvRaidResult)));
      else
        SRPG.Network.RequestAPI((WebAPI) new ReqBtlComRaid(this.mCurrentQuest.iname, this.mNumRaids, new SRPG.Network.ResponseCallback(this.RecvRaidResult), 0));
    }

    private void RecvRaidResult(WWWResult www)
    {
      if (SRPG.Network.IsError)
      {
        switch (SRPG.Network.ErrCode)
        {
          case SRPG.Network.EErrCode.ChallengeLimit:
            this.LockWindow(false);
            FlowNode_Network.Back();
            break;
          case SRPG.Network.EErrCode.QuestArchive_ArchiveNotOpened:
            this.LockWindow(false);
            FlowNode_Network.Back();
            break;
          default:
            FlowNode_Network.Failed();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<PartyWindow2.JSON_ReqBtlComRaidResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<PartyWindow2.JSON_ReqBtlComRaidResponse>>(www.text);
        GameManager instance = MonoSingleton<GameManager>.Instance;
        PlayerData player = instance.Player;
        int exp = player.Exp;
        int lv = player.Lv;
        if (this.mRaidResult == null)
          this.mRaidResult = new RaidResult(this.mCurrentPartyType.ToPlayerPartyType());
        this.mRaidResult.quest = this.mCurrentQuest;
        this.mRaidResult.members.Clear();
        this.mRaidResult.results.Clear();
        for (int mainmemberStart = this.CurrentParty.PartyData.MAINMEMBER_START; mainmemberStart <= this.CurrentParty.PartyData.MAINMEMBER_END; ++mainmemberStart)
        {
          long uniqueId = this.CurrentParty.Units[mainmemberStart] == null ? 0L : this.CurrentParty.Units[mainmemberStart].UniqueID;
          if (uniqueId > 0L)
          {
            UnitData unitDataByUniqueId = player.FindUnitDataByUniqueID(uniqueId);
            if (unitDataByUniqueId != null)
            {
              UnitData unitData = new UnitData();
              unitData.Setup(unitDataByUniqueId);
              this.mRaidResult.members.Add(unitData);
            }
          }
        }
        if (this.mCurrentQuest.units.IsNotNull())
        {
          for (int index = 0; index < this.mCurrentQuest.units.Length; ++index)
          {
            UnitData unitDataByUnitId = instance.Player.FindUnitDataByUnitID(this.mCurrentQuest.units.Get(index));
            if (unitDataByUnitId != null)
            {
              UnitData unitData = new UnitData();
              unitData.Setup(unitDataByUnitId);
              this.mRaidResult.members.Add(unitData);
            }
          }
        }
        for (int submemberStart = this.CurrentParty.PartyData.SUBMEMBER_START; submemberStart <= this.CurrentParty.PartyData.SUBMEMBER_END; ++submemberStart)
        {
          long uniqueId = this.CurrentParty.Units[submemberStart] == null ? 0L : this.CurrentParty.Units[submemberStart].UniqueID;
          if (uniqueId > 0L)
          {
            UnitData unitDataByUniqueId = player.FindUnitDataByUniqueID(uniqueId);
            if (unitDataByUniqueId != null)
            {
              UnitData unitData = new UnitData();
              unitData.Setup(unitDataByUniqueId);
              this.mRaidResult.members.Add(unitData);
            }
          }
        }
        try
        {
          instance.Deserialize(jsonObject.body.player);
          instance.Deserialize(jsonObject.body.items);
          instance.Deserialize(jsonObject.body.units);
          player.Deserialize(jsonObject.body.story_ex_challenge);
          instance.Player.GuildTrophyData.OverwriteGuildTrophyProgress(jsonObject.body.guild_trophies);
          if (jsonObject.body.artifacts != null)
          {
            instance.Deserialize(jsonObject.body.artifacts, true);
            instance.Player.UpdateArtifactOwner();
          }
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        try
        {
          MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(jsonObject.body.trophyprogs);
          MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(jsonObject.body.bingoprogs);
          JukeBoxWindow.UnlockMusic(jsonObject.body.bgms);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        SRPG.Network.RemoveAPI();
        if (jsonObject.body.cards != null)
        {
          for (int index = 0; index < jsonObject.body.cards.Length; ++index)
          {
            MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
            if (jsonObject.body.cards[index].IsGetUnit)
              FlowNode_ConceptCardGetUnit.AddConceptCardData(ConceptCardData.CreateConceptCardDataForDisplay(jsonObject.body.cards[index].iname));
          }
        }
        if (jsonObject.body.btlinfos != null && RuneUtility.CountRuneNum(jsonObject.body.btlinfos) > 0)
        {
          MonoSingleton<GameManager>.Instance.Player.OnDirtyRuneData();
          MonoSingleton<GameManager>.Instance.Player.SetRuneStorageUsedNum(jsonObject.body.rune_storage_used);
        }
        int addCnt = 1;
        if (jsonObject.body.btlinfos != null)
        {
          addCnt = jsonObject.body.btlinfos.Length;
          for (int index = 0; index < jsonObject.body.btlinfos.Length; ++index)
          {
            BattleCore.Json_BtlInfo btlinfo = jsonObject.body.btlinfos[index];
            int length = btlinfo.drops == null ? 0 : btlinfo.drops.Length;
            RaidQuestResult raidQuestResult = new RaidQuestResult();
            raidQuestResult.index = index;
            raidQuestResult.pexp = this.mCurrentQuest.pexp;
            raidQuestResult.uexp = this.mCurrentQuest.uexp;
            raidQuestResult.gold = this.mCurrentQuest.gold;
            raidQuestResult.drops = new QuestResult.DropItemData[length];
            if (btlinfo.drops != null)
            {
              for (int iid = 0; iid < btlinfo.drops.Length; ++iid)
              {
                ItemParam itemParam = (ItemParam) null;
                ConceptCardParam conceptCardParam = (ConceptCardParam) null;
                if (btlinfo.drops[iid].dropItemType != EBattleRewardType.None)
                {
                  if (btlinfo.drops[iid].dropItemType == EBattleRewardType.Item)
                    itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(btlinfo.drops[iid].iname);
                  else if (btlinfo.drops[iid].dropItemType == EBattleRewardType.ConceptCard)
                    conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(btlinfo.drops[iid].iname);
                  else
                    DebugUtility.LogError(string.Format("不明なドロップ品が登録されています。iname:{0} (itype:{1})", (object) btlinfo.drops[iid].iname, (object) btlinfo.drops[iid].itype));
                  if (itemParam != null)
                  {
                    raidQuestResult.drops[iid] = new QuestResult.DropItemData();
                    raidQuestResult.drops[iid].SetupDropItemData(EBattleRewardType.Item, (long) iid, btlinfo.drops[iid].iname, btlinfo.drops[iid].num);
                  }
                  else if (conceptCardParam != null)
                  {
                    MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
                    raidQuestResult.drops[iid] = new QuestResult.DropItemData();
                    raidQuestResult.drops[iid].SetupDropItemData(EBattleRewardType.ConceptCard, (long) iid, btlinfo.drops[iid].iname, btlinfo.drops[iid].num);
                  }
                }
              }
            }
            this.mRaidResult.campaignIds = btlinfo.campaigns;
            this.mRaidResult.results.Add(raidQuestResult);
          }
        }
        if (jsonObject.body.boss_rewards != null)
        {
          addCnt = jsonObject.body.boss_rewards.Length;
          for (int index = 0; index < jsonObject.body.boss_rewards.Length; ++index)
          {
            PartyWindow2.JSON_ReqBtlComRaidResponse.BossReward bossReward = jsonObject.body.boss_rewards[index];
            int length = bossReward.rewards == null ? 0 : bossReward.rewards.Length;
            RaidQuestResult raidQuestResult = new RaidQuestResult();
            raidQuestResult.index = index;
            raidQuestResult.drops = new QuestResult.DropItemData[length];
            if (bossReward.rewards != null)
            {
              for (int iid = 0; iid < length; ++iid)
              {
                PartyWindow2.JSON_ReqBtlComRaidResponse.BossReward.Reward reward = bossReward.rewards[iid];
                if (reward.itype == 0)
                {
                  if (MonoSingleton<GameManager>.Instance.GetItemParam(reward.iname) != null)
                  {
                    raidQuestResult.drops[iid] = new QuestResult.DropItemData();
                    raidQuestResult.drops[iid].SetupDropItemData(EBattleRewardType.Item, (long) iid, reward.iname, reward.num);
                  }
                }
                else if (reward.itype == 5)
                {
                  if (MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(reward.iname) != null)
                  {
                    MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
                    raidQuestResult.drops[iid] = new QuestResult.DropItemData();
                    raidQuestResult.drops[iid].SetupDropItemData(EBattleRewardType.ConceptCard, (long) iid, reward.iname, reward.num);
                  }
                }
                else
                  DebugUtility.LogError(string.Format("未対応なドロップ品が登録されています。iname:{0} (itype:{1})", (object) reward.iname, (object) reward.itype));
              }
            }
            this.mRaidResult.results.Add(raidQuestResult);
          }
        }
        if (player.Lv > lv)
          player.OnPlayerLevelChange(player.Lv - lv);
        player.OnQuestWin(this.mCurrentQuest.iname, currentUnits: this.mRaidResult.members.ToArray(), addCnt: addCnt);
        this.mRaidResult.pexp = player.Exp - exp;
        this.mRaidResult.uexp = this.mCurrentQuest.uexp * this.mNumRaids;
        this.mRaidResult.gold = this.mCurrentQuest.gold * this.mNumRaids;
        player.OnGoldChange(this.mRaidResult.gold);
        this.mRaidResult.chquest = new QuestParam[this.mRaidResult.members.Count];
        for (int index = 0; index < this.mRaidResult.members.Count; ++index)
        {
          UnitData.CharacterQuestParam charaEpisodeData = this.mRaidResult.members[index].GetCurrentCharaEpisodeData();
          if (charaEpisodeData != null)
            this.mRaidResult.chquest[index] = charaEpisodeData.Param;
        }
        if (jsonObject.body.runes_detail != null && jsonObject.body.runes_detail.Length > 0)
        {
          List<RuneData> runeDataList = new List<RuneData>();
          foreach (Json_RuneData json in jsonObject.body.runes_detail)
          {
            RuneData runeData = new RuneData();
            runeData.Deserialize(json);
            runeDataList.Add(runeData);
          }
          this.mRaidResult.runes_detail = runeDataList;
        }
        GlobalVars.RaidResult = this.mRaidResult;
        GlobalVars.PlayerExpOld.Set(exp);
        GlobalVars.PlayerExpNew.Set(player.Exp);
        GlobalVars.PlayerLevelChanged.Set(player.Lv != lv);
        if (jsonObject.body.guildraid_bp_charge > 0)
          GuildRaidManager.SetNotifyPush();
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.All);
        this.StartCoroutine(this.ShowRaidResultAsync());
      }
    }

    [DebuggerHidden]
    private IEnumerator ShowRaidResultAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PartyWindow2.\u003CShowRaidResultAsync\u003Ec__Iterator2()
      {
        \u0024this = this
      };
    }

    private void OnResetChallenge(WWWResult www, Action<bool> callback)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (SRPG.Network.IsError)
      {
        int errCode = (int) SRPG.Network.ErrCode;
        FlowNode_Network.Failed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<PartyWindow2.JSON_ReqBtlComResetResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<PartyWindow2.JSON_ReqBtlComResetResponse>>(www.text);
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
            if (!MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.quests))
            {
              FlowNode_Network.Failed();
              return;
            }
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Failed();
            return;
          }
          SRPG.Network.RemoveAPI();
          this.Refresh_ResetChallengeCount();
          GameObject gameObject1 = AssetManager.Load<GameObject>(this.CHALLENGE_COUNT_RESET_COMPLETE);
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
            return;
          GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
          gameObject2.transform.SetParent(((Component) this).transform, false);
          ChallengeCountResetCompleteWindow component = gameObject2.GetComponent<ChallengeCountResetCompleteWindow>();
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            return;
          component.Setup((int) this.mCurrentQuest.dailyReset, this.mCurrentQuest.ResetMax, (ChallengeCountResetCompleteWindow.DecideButtonEvent) (g =>
          {
            if (callback == null)
              return;
            callback(true);
          }));
        }
      }
    }

    private void Refresh_ResetChallengeCount()
    {
      this.Refresh_StoryExChallengeCount();
      this.RefreshRaidTicketNum();
      this.RefreshRaidButtons();
      this.UpdateCurrentForwardButton();
      this.RefreshAutoRepeatQuestButton();
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_COIN_STATUS.ToString(), (object) null);
    }

    private void HardQuestDropPiecesUpdate()
    {
      GameObject gameObject = GameObjectID.FindGameObject("WorldMapQuestList");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      GameParameter.UpdateAll(gameObject);
    }

    public void PrepareStartQuest()
    {
      if (MonoSingleton<GameManager>.Instance.Player != null && MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress != null && MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.IsExistRecord)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
      else if (this.mCurrentQuest == null)
      {
        this.GoToForward(false);
      }
      else
      {
        if (this.mCurrentQuest.type == QuestTypes.StoryExtra && !this.CheckStoryExChallengeCount())
          return;
        this.ResetChallengeCountIfNeeded(new Action<bool>(this.GoToForward));
      }
    }

    private void ResetChallengeCountIfNeeded(Action<bool> callback)
    {
      if (this.mCurrentQuest.CheckEnableChallange())
      {
        if (callback == null)
          return;
        callback(false);
      }
      else if (!this.mCurrentQuest.CheckEnableReset())
      {
        string empty = string.Empty;
        UIUtility.NegativeSystemMessage((string) null, this.mCurrentQuest.dayReset <= 0 ? LocalizedText.Get("sys.QUEST_SPAN_CHALLENGE_NO_RESET") : LocalizedText.Get("sys.QUEST_CHALLENGE_NO_RESET"), (UIUtility.DialogResultEvent) (g => { }));
      }
      else
      {
        if (!string.IsNullOrEmpty(this.mCurrentQuest.ResetCost))
        {
          ResetCostParam resetCost = MonoSingleton<GameManager>.Instance.MasterParam.FindResetCost(this.mCurrentQuest.ResetCost);
          if (resetCost != null && (resetCost.IsEnableCoinCost() || resetCost.IsEnableItemCost()))
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 319);
            return;
          }
        }
        if (string.IsNullOrEmpty(this.mCurrentQuest.ResetItem))
        {
          DebugUtility.LogError("クエスト[" + this.mCurrentQuest.iname + "] の挑戦回復アイテムの指定が見つかりません。");
        }
        else
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          ItemParam itemParam = instance.GetItemParam(this.mCurrentQuest.ResetItem);
          if (itemParam != null)
          {
            FixParam fixParam = instance.MasterParam.FixParam;
            int dailyReset = (int) this.mCurrentQuest.dailyReset;
            if (dailyReset < 0)
              DebugUtility.LogError("クエスト[" + this.mCurrentQuest.iname + "] の挑戦回数の現在のリセット回数が異常な値です：" + (object) dailyReset);
            else if (fixParam.QuestResetCost == null || fixParam.QuestResetCost.Length < 1)
            {
              DebugUtility.LogError("FixParamのQuestResetCostが空です。");
            }
            else
            {
              int use_num = dailyReset >= fixParam.QuestResetCost.Length ? ((IEnumerable<int>) fixParam.QuestResetCost).Last<int>() : fixParam.QuestResetCost[dailyReset];
              ItemData itemDataByItemId = instance.Player.FindItemDataByItemID(itemParam.iname);
              if (itemDataByItemId == null || itemDataByItemId.Num <= 0 || itemDataByItemId.Num < use_num)
              {
                UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.QUEST_CHALLENGE_RESET_ITEM_NOT_ENOUGH", (object) itemParam.name), (UIUtility.DialogResultEvent) (gob => { }));
              }
              else
              {
                GameObject gameObject1 = AssetManager.Load<GameObject>(this.CHALLENGE_COUNT_RESET_CONFIRM);
                if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
                  return;
                GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
                gameObject2.transform.SetParent(((Component) this).transform, false);
                ChallengeCountResetConfirmWindow component = gameObject2.GetComponent<ChallengeCountResetConfirmWindow>();
                if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                  return;
                component.Setup(itemDataByItemId, use_num, dailyReset, this.mCurrentQuest.ResetMax, (ChallengeCountResetConfirmWindow.ChallengeCountResetEvent) (g => SRPG.Network.RequestAPI((WebAPI) new ReqBtlComReset(this.mCurrentQuest.iname, eResetCostType.Item, (SRPG.Network.ResponseCallback) (www => this.OnResetChallenge(www, callback))))), (ChallengeCountResetConfirmWindow.ChallengeCountResetEvent) (g => { }));
              }
            }
          }
          else
            DebugUtility.LogError("iname:" + this.mCurrentQuest.ResetItem + "のItemParamがありません.");
        }
      }
    }

    private bool CheckStoryExChallengeCount()
    {
      if (MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StoryExChallengeMax <= 0 || !MonoSingleton<GameManager>.Instance.Player.StoryExChallengeCount.IsRestChallengeCount_Zero())
        return true;
      if (MonoSingleton<GameManager>.Instance.Player.StoryExChallengeCount.RestResetCount <= 0)
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.QUEST_CHALLENGE_NO_RESET"), (UIUtility.DialogResultEvent) (g => { }));
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 320);
      return false;
    }

    private void GoToForward(bool didReset)
    {
      if (this.mCurrentQuest != null)
      {
        if (!this.mCurrentQuest.IsMulti)
        {
          if (!this.mCurrentQuest.IsDateUnlock() && !MonoSingleton<GameManager>.Instance.Player.IsQuestArchiveOpenByArea(this.mCurrentQuest.ChapterID))
          {
            if (this.mCurrentQuest.IsBeginner)
            {
              UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.BEGINNER_QUEST_OUT_OF_DATE"), (UIUtility.DialogResultEvent) null);
              return;
            }
            UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), new UIUtility.DialogResultEvent(this.JumpToBefore));
            return;
          }
          if (this.mCurrentQuest.IsKeyQuest && !this.mCurrentQuest.IsKeyUnlock())
          {
            UIUtility.SystemMessage(LocalizedText.Get("sys.KEYQUEST_UNLOCK"), LocalizedText.Get("sys.KEYQUEST_AVAILABLE_CAUTION"), (UIUtility.DialogResultEvent) null);
            return;
          }
          if (MonoSingleton<GameManager>.Instance.Player.Stamina < this.mCurrentQuest.RequiredApWithPlayerLv(MonoSingleton<GameManager>.Instance.Player.Lv))
          {
            MonoSingleton<GameManager>.Instance.StartBuyStaminaSequence(true, this);
            return;
          }
        }
        else
        {
          MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.CurrentState == MyPhoton.MyState.ROOM)
          {
            MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
            if (currentRoom != null)
            {
              JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
              QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(myPhotonRoomParam.iname);
              int unitlv = myPhotonRoomParam.unitlv;
              bool flag = true;
              if (quest != null && unitlv > 0)
              {
                for (int index = 0; index < (int) quest.unitNum; ++index)
                {
                  UnitData unit = this.CurrentParty.Units[index];
                  if (unit != null)
                    flag &= unit.CalcLevel() >= unitlv;
                }
                if (!flag)
                {
                  this.mMultiErrorMsg = UIUtility.SystemMessage(LocalizedText.Get("sys.TITLE"), LocalizedText.Get("sys.PARTYEDITOR_ULV"), (UIUtility.DialogResultEvent) (dialog => this.mMultiErrorMsg = (GameObject) null));
                  return;
                }
              }
            }
          }
        }
      }
      if (didReset)
        return;
      if (this.mCurrentQuest != null)
      {
        if (this.mCurrentQuest.IsQuestDrops && UnityEngine.Object.op_Inequality((UnityEngine.Object) QuestDropParam.Instance, (UnityEngine.Object) null))
        {
          bool flag = QuestDropParam.Instance.IsChangedQuestDrops(this.mCurrentQuest);
          GlobalVars.SetDropTableGeneratedTime();
          if (flag)
          {
            this.HardQuestDropPiecesUpdate();
            if (!QuestDropParam.Instance.IsWarningPopupDisable)
            {
              UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.PARTYEDITOR_DROP_TABLE"), (UIUtility.DialogResultEvent) (dialog => this.OpenQuestDetail()));
              return;
            }
          }
        }
        int numMainUnits = 0;
        int num1 = 0;
        int num2 = 0;
        int availableMainMemberSlots = this.AvailableMainMemberSlots;
        List<string> stringList = new List<string>();
        bool flag1 = false;
        for (int index = 0; index < availableMainMemberSlots; ++index)
        {
          if (this.CurrentParty.Units.Length > index)
          {
            if (this.CurrentParty.Units[index] != null || this.mSlotData[index].Type == SRPG.PartySlotType.Npc || this.mSlotData[index].Type == SRPG.PartySlotType.NpcHero || this.mSlotData[index].Type == SRPG.PartySlotType.ForcedHero)
              ++numMainUnits;
            if ((this.mLockedPartySlots & 1 << index) == 0)
            {
              ++num1;
              if (this.CurrentParty.Units[index] != null && !this.mCurrentQuest.IsUnitAllowed(this.CurrentParty.Units[index]))
                ++num2;
            }
          }
        }
        if (this.EnableHeroSolo && this.mGuestUnit != null && this.mGuestUnit.Count > 0)
        {
          int count = this.mGuestUnit.Count;
          if (numMainUnits < 1 && count == 1)
            flag1 = true;
          numMainUnits += count;
          num1 += count;
        }
        string[] force_units = (string[]) null;
        if (this.mCurrentQuest.questParty != null)
          force_units = ((IEnumerable<PartySlotTypeUnitPair>) this.mCurrentQuest.questParty.GetMainSubSlots()).Where<PartySlotTypeUnitPair>((Func<PartySlotTypeUnitPair, bool>) (slot => slot.Type == SRPG.PartySlotType.ForcedHero || slot.Type == SRPG.PartySlotType.Forced)).Select<PartySlotTypeUnitPair, string>((Func<PartySlotTypeUnitPair, string>) (slot => slot.Unit)).ToArray<string>();
        else
          force_units = this.mCurrentQuest.units.GetList();
        if (force_units != null)
        {
          for (int i = 0; i < force_units.Length; ++i)
          {
            if (0 > MonoSingleton<GameManager>.Instance.Player.Units.FindIndex((Predicate<UnitData>) (u => u.UnitParam.iname == force_units[i])))
            {
              UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(force_units[i]);
              stringList.Add(unitParam.name);
            }
          }
        }
        if (1 <= stringList.Count)
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.PARTYEDITOR_NOHERO", (object) string.Join(",", stringList.ToArray())), (UIUtility.DialogResultEvent) (dialog => { }));
          return;
        }
        if (!this.CheckMember(numMainUnits))
          return;
        string empty1 = string.Empty;
        if (!this.mCurrentQuest.IsEntryQuestCondition((IEnumerable<UnitData>) this.CurrentParty.Units, ref empty1) && (!this.EnableHeroSolo || !this.mCurrentQuest.IsEntryQuestCondition((IEnumerable<UnitData>) this.mGuestUnit, ref empty1)))
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(empty1), (UIUtility.DialogResultEvent) (dialog => { }));
          return;
        }
        UnitData[] list = this.CurrentParty.Units;
        if (this.mCurrentQuest.IsMulti)
        {
          List<UnitData> multiActiveUnitList = this.GetMultiActiveUnitList();
          if (multiActiveUnitList != null)
            list = multiActiveUnitList.ToArray();
        }
        if (this.PartyType != PartyWindow2.EditPartyTypes.MultiTower && this.IsSameUnitInParty(list))
          return;
        if (this.mCurrentSupport != null && this.mCurrentSupport.Unit != null && !this.mCurrentQuest.IsEntryQuestCondition(this.mCurrentSupport.Unit, ref empty1))
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(empty1), (UIUtility.DialogResultEvent) (dialog => { }));
          return;
        }
        if (this.mCurrentQuest.IsCharacterQuest())
        {
          List<UnitData> unitDataList = new List<UnitData>((IEnumerable<UnitData>) this.CurrentParty.Units);
          unitDataList.Add(this.mGuestUnit[0]);
          for (int index1 = 0; index1 < unitDataList.Count; ++index1)
          {
            UnitData unitData = unitDataList[index1];
            if (unitData != null && unitData.UnitID == this.mGuestUnit[0].UnitID)
            {
              string empty2 = string.Empty;
              List<QuestClearUnlockUnitDataParam> skillUnlocks = unitData.SkillUnlocks;
              for (int index2 = 0; index2 < skillUnlocks.Count; ++index2)
              {
                QuestClearUnlockUnitDataParam unlockUnitDataParam = skillUnlocks[index2];
                if (unlockUnitDataParam != null && !unlockUnitDataParam.add && unlockUnitDataParam.qids != null && Array.FindIndex<string>(unlockUnitDataParam.qids, (Predicate<string>) (p => p == this.mCurrentQuest.iname)) != -1)
                  empty2 += LocalizedText.Get("sys.UNITLIST_REWRITE_TARGET", (object) unlockUnitDataParam.GetUnlockTypeText(), (object) unlockUnitDataParam.GetRewriteName());
              }
              if (!string.IsNullOrEmpty(empty2))
              {
                UIUtility.ConfirmBox(LocalizedText.Get("sys.UNITLIST_DATA_REWRITE", (object) empty2), (UIUtility.DialogResultEvent) (dialog => this.PostForwardPressed()), (UIUtility.DialogResultEvent) null);
                return;
              }
            }
          }
        }
        if (num2 > 0)
        {
          UIUtility.ConfirmBox(LocalizedText.Get("sys.PARTYEDITOR_SANKAFUKA"), (UIUtility.DialogResultEvent) (dialog => this.PostForwardPressed()), (UIUtility.DialogResultEvent) null);
          return;
        }
        if (!flag1 && numMainUnits < num1 && this.PartyType != PartyWindow2.EditPartyTypes.MultiTower)
        {
          if (this.PartyType == PartyWindow2.EditPartyTypes.RankMatch)
          {
            UIUtility.SystemMessage(LocalizedText.Get("sys.PARTYEDITOR_PARTYNOTFULL_INVALID"), (UIUtility.DialogResultEvent) null);
            return;
          }
          UIUtility.ConfirmBox(LocalizedText.Get("sys.PARTYEDITOR_PARTYNOTFULL"), (UIUtility.DialogResultEvent) (dialog => this.PostForwardPressed()), (UIUtility.DialogResultEvent) null);
          return;
        }
      }
      this.PostForwardPressed();
    }

    protected virtual void OnForwardOrBackButtonClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
      {
        if (!this.mCurrentQuest.IsGenAdvBoss)
          return;
        this.GenAdvBossNoTicketConfirm(true);
      }
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) this.CurrentForwardButton))
        this.PrepareStartQuest();
      else if (this.PartyType == PartyWindow2.EditPartyTypes.MultiTower)
        this.SaveAndActivatePin(8);
      else if (this.PartyType == PartyWindow2.EditPartyTypes.Ordeal)
      {
        GlobalVars.OrdealParties = this.mTeams;
        GlobalVars.OrdealSupports = this.mSupports;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);
      }
      else
        this.SaveAndActivatePin(3);
    }

    protected virtual bool CheckMember(int numMainUnits)
    {
      if (numMainUnits <= 0)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.PARTYEDITOR_CANTSTART"), (UIUtility.DialogResultEvent) (dialog => { }));
        return false;
      }
      if (this.mCurrentQuest != null && !this.mCurrentQuest.IsMultiTower)
      {
        string empty = string.Empty;
        if (!this.mCurrentQuest.IsEntryQuestCondition((IEnumerable<UnitData>) this.CurrentParty.Units, ref empty))
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(empty), (UIUtility.DialogResultEvent) (dialog => { }));
          return false;
        }
      }
      return true;
    }

    protected virtual void PostForwardPressed()
    {
      GlobalEvent.Invoke("DISABLE_MAINMENU_TOP_COMMAND", (object) null);
      this.LockWindow(true);
      GlobalVars.SelectedSupport.Set(this.mCurrentSupport);
      GlobalVars.SelectedFriendID = this.mCurrentSupport == null ? (string) null : this.mCurrentSupport.FUID;
      if (this.PartyType == PartyWindow2.EditPartyTypes.MultiTower)
        this.SaveAndActivatePin(8);
      else if (this.mCurrentQuest.IsVersus && (GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.Free || GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.Friend))
        this.SaveAndActivatePin(1);
      else if (this.IsShowDownloadPopup)
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
        AssetDownloader.StartConfirmDownloadQuestContentYesNo(this.GetBattleEntryUnits(), this.GetBattleEntryItems(), quest, (UIUtility.DialogResultEvent) (ok => this.SaveAndActivatePin(1)), (UIUtility.DialogResultEvent) (no =>
        {
          GlobalEvent.Invoke("ENABLE_MAINMENU_TOP_COMMAND", (object) null);
          this.LockWindow(false);
        }));
      }
      else
        this.SaveAndActivatePin(1);
    }

    protected void SaveAndActivatePin(int pinID, int ErrPinID = -1)
    {
      if (!this.mInitialized)
        return;
      this.SaveInventory();
      if (!this.IsPartyDirty && !PartyWindow2.ForceUpdateBattleParty)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
      }
      else
      {
        PartyWindow2.ForceUpdateBattleParty = true;
        GlobalVars.SelectedSupport.Set(this.mCurrentSupport);
        this.SaveParty((PartyWindow2.Callback) (() =>
        {
          PartyWindow2.ForceUpdateBattleParty = false;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
        }), (PartyWindow2.Callback) (() =>
        {
          if (ErrPinID >= 0)
            FlowNode_GameObject.ActivateOutputLinks((Component) this, ErrPinID);
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ILLEGAL_PARTY"), (UIUtility.DialogResultEvent) null);
        }));
      }
    }

    protected void SaveParty(PartyWindow2.Callback cbSuccess, PartyWindow2.Callback cbError)
    {
      this.LockWindow(true);
      this.mIsSaving = true;
      this.mOnPartySaveSuccess = cbSuccess;
      this.mOnPartySaveFail = cbError;
      if (!this.IsPartyDirty && !PartyWindow2.ForceUpdateBattleParty)
      {
        if (this.mOnPartySaveSuccess == null)
          return;
        this.mOnPartySaveSuccess();
      }
      else
      {
        this.SaveTeamPresets();
        PartyData partyOfType = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(this.mCurrentPartyType.ToPlayerPartyType());
        List<UnitData> unitDataList = new List<UnitData>();
        for (int mainmemberStart = this.CurrentParty.PartyData.MAINMEMBER_START; mainmemberStart < this.CurrentParty.PartyData.MAX_MAINMEMBER; ++mainmemberStart)
        {
          PartySlotData partySlotData = this.mSlotData[mainmemberStart];
          if (partySlotData.Type != SRPG.PartySlotType.Npc && partySlotData.Type != SRPG.PartySlotType.NpcHero)
            unitDataList.Add(this.CurrentParty.Units[mainmemberStart]);
        }
        for (int count = unitDataList.Count; count < this.CurrentParty.PartyData.MAX_MAINMEMBER; ++count)
          unitDataList.Add((UnitData) null);
        for (int submemberStart = this.CurrentParty.PartyData.SUBMEMBER_START; submemberStart <= this.CurrentParty.PartyData.SUBMEMBER_END; ++submemberStart)
        {
          PartySlotData partySlotData = this.mSlotData[submemberStart];
          if (partySlotData.Type != SRPG.PartySlotType.Npc && partySlotData.Type != SRPG.PartySlotType.NpcHero)
            unitDataList.Add(this.CurrentParty.Units[submemberStart]);
        }
        for (int count = unitDataList.Count; count < this.CurrentParty.PartyData.MAX_UNIT; ++count)
          unitDataList.Add((UnitData) null);
        for (int index = 0; index < unitDataList.Count; ++index)
        {
          long uniqueId = unitDataList[index] == null ? 0L : unitDataList[index].UniqueID;
          partyOfType.SetUnitUniqueID(index, uniqueId);
        }
        SRPG.Network.RequestAPI((WebAPI) new ReqParty(new SRPG.Network.ResponseCallback(this.SavePartyCallback), ignoreEmpty: false));
      }
    }

    private void SaveInventory(bool force_save = false)
    {
      if (!force_save && (!this.mInitialized || !this.InventoryDirty))
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = 0; index < 5; ++index)
      {
        ItemData itemData = (ItemData) null;
        if (index < this.mCurrentItems.Length && this.mCurrentItems[index] != null)
          itemData = player.FindItemDataByItemParam(this.mCurrentItems[index].Param);
        player.SetInventory(index, itemData);
      }
      player.SaveInventory();
    }

    private bool InventoryDirty
    {
      get
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        for (int index = 0; index < 5; ++index)
        {
          ItemData itemData = (ItemData) null;
          if (index < this.mCurrentItems.Length && this.mCurrentItems[index] != null)
            itemData = player.FindItemDataByItemParam(this.mCurrentItems[index].Param);
          if (player.Inventory[index] != itemData)
            return true;
        }
        return false;
      }
    }

    private void JumpToBefore(GameObject dialog) => BackHandler.Invoke();

    [DebuggerHidden]
    private IEnumerator WaitForSave()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PartyWindow2.\u003CWaitForSave\u003Ec__Iterator3()
      {
        \u0024this = this
      };
    }

    private void SavePartyCallback(WWWResult www)
    {
      if (SRPG.Network.IsError)
      {
        switch (SRPG.Network.ErrCode)
        {
          case SRPG.Network.EErrCode.NoUnitParty:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          case SRPG.Network.EErrCode.IllegalParty:
            SRPG.Network.RemoveAPI();
            SRPG.Network.ResetError();
            break;
          default:
            FlowNode_Network.Retry();
            return;
        }
        this.LockWindow(false);
        this.mIsSaving = false;
        if (this.mOnPartySaveFail == null)
          return;
        this.mOnPartySaveFail();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        GameManager instance = MonoSingleton<GameManager>.Instance;
        try
        {
          if (jsonObject.body == null)
            throw new InvalidJSONException();
          instance.Deserialize(jsonObject.body.player);
          instance.Deserialize(jsonObject.body.parties);
          instance.Player.Deserialize(jsonObject.body.party_decks);
        }
        catch (Exception ex)
        {
          FlowNode_Network.Retry();
          return;
        }
        SRPG.Network.RemoveAPI();
        this.LockWindow(false);
        this.mIsSaving = false;
        if (this.mOnPartySaveSuccess == null)
          return;
        this.mOnPartySaveSuccess();
      }
    }

    protected void RefreshQuest()
    {
      this.mCurrentPartyType = this.PartyType;
      if (!this.UseQuestInfo)
        return;
      QuestParam mCurrentQuest = this.mCurrentQuest;
      this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      if (this.PartyType == PartyWindow2.EditPartyTypes.Auto)
      {
        this.mCurrentPartyType = PartyWindow2.EditPartyTypes.Auto;
        if (this.mCurrentQuest == null)
          DebugUtility.LogError("Quest not selected");
        else
          this.mCurrentPartyType = PartyUtility.GetEditPartyTypes(this.mCurrentQuest);
        if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Auto)
          this.mCurrentPartyType = PartyWindow2.EditPartyTypes.Normal;
      }
      if (this.mCurrentQuest != mCurrentQuest)
        this.mMultiRaidNum = -1;
      DataSource.Bind<QuestParam>(((Component) this).gameObject, this.mCurrentQuest);
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(this.mCurrentQuest);
      DataSource.Bind<QuestCampaignData[]>(((Component) this).gameObject, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestCampaigns, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestCampaigns.GetQuestCampaignList, (UnityEngine.Object) null))
        this.QuestCampaigns.GetQuestCampaignList.RefreshIcons();
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    protected void RefreshNoneQuestInfo(bool keepTeam)
    {
      this.mIsHeloOnly = false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_SankaFuka, (UnityEngine.Object) null))
      {
        for (int index = 1; index < this.mSankaFukaIcons.Length && index < this.UnitSlots.Length; ++index)
        {
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSankaFukaIcons[index], (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null))
          {
            this.mSankaFukaIcons[index] = UnityEngine.Object.Instantiate<GameObject>(this.Prefab_SankaFuka);
            RectTransform transform = this.mSankaFukaIcons[index].transform as RectTransform;
            transform.anchoredPosition = Vector2.zero;
            ((Transform) transform).SetParent(((Component) this.UnitSlots[index]).transform, false);
          }
        }
      }
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      PlayerPartyTypes playerPartyType = this.mCurrentPartyType.ToPlayerPartyType();
      if (Array.IndexOf<PlayerPartyTypes>(this.SaveJobs, playerPartyType) >= 0)
      {
        this.mOwnUnits = new List<UnitData>(player.Units.Count);
        for (int index = 0; index < player.Units.Count; ++index)
        {
          UnitData unitData = new UnitData();
          unitData.Setup(player.Units[index]);
          unitData.TempFlags |= UnitData.TemporaryFlags.TemporaryUnitData | UnitData.TemporaryFlags.AllowJobChange;
          unitData.SetJob(playerPartyType);
          this.mOwnUnits.Add(unitData);
        }
      }
      else
      {
        this.mOwnUnits = new List<UnitData>((IEnumerable<UnitData>) player.Units);
        for (int index = 0; index < this.mOwnUnits.Count; ++index)
          this.mOwnUnits[index].TempFlags |= UnitData.TemporaryFlags.AllowJobChange;
      }
      DataSource.Bind<PlayerPartyTypes>(((Component) this).gameObject, playerPartyType);
      if (!keepTeam)
      {
        if (this.mIsLockCurrentParty)
          this.OverrideLoadTeam();
        else
          this.LoadTeam(this.mCurrentPartyType);
      }
      else
        this.mTeams = UnitOverWriteUtility.ApplyTeams(this.mTeams, this.mCurrentPartyType);
      int index1 = 0;
      for (int index2 = 0; index2 < this.UnitSlots.Length && index2 < this.CurrentParty.Units.Length && index2 < this.mSlotData.Count; ++index2)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index2], (UnityEngine.Object) null))
        {
          this.UnitSlots[index2].SetSlotData<QuestParam>(this.mCurrentQuest);
          PartySlotData partySlotData = this.mSlotData[index2];
          if (partySlotData.Type == SRPG.PartySlotType.ForcedHero)
          {
            if (this.mGuestUnit != null && index1 < this.mGuestUnit.Count)
            {
              UnitData unit = PartyUtility.FindUnit(this.mGuestUnit[index1], this.mOwnUnits);
              this.UnitSlots[index2].SetSlotData<UnitData>(unit);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[index2], (UnityEngine.Object) null))
              {
                DataSource.Bind<UnitData>(((Component) this.CardSlots[index2]).gameObject, unit);
                this.CardSlots[index2].SetSlotData<ConceptCardData>(unit?.MainConceptCard);
                ((Component) this.CardSlots[index2]).GetComponent<ConceptCardIcon>().Setup(unit?.MainConceptCard);
              }
            }
            ++index1;
          }
          else if (partySlotData.Type == SRPG.PartySlotType.Npc || partySlotData.Type == SRPG.PartySlotType.NpcHero)
          {
            UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(partySlotData.UnitName);
            this.UnitSlots[index2].SetSlotData<UnitParam>(unitParam);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[index2], (UnityEngine.Object) null))
            {
              DataSource.Bind<UnitData>(((Component) this.CardSlots[index2]).gameObject, (UnitData) null);
              this.CardSlots[index2].SetSlotData<ConceptCardData>((ConceptCardData) null);
              ((Component) this.CardSlots[index2]).GetComponent<ConceptCardIcon>().Setup((ConceptCardData) null);
            }
          }
          else
          {
            UnitData unit = PartyUtility.FindUnit(this.CurrentParty.Units[index2], this.mOwnUnits);
            this.UnitSlots[index2].SetSlotData<UnitData>(unit);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[index2], (UnityEngine.Object) null))
            {
              DataSource.Bind<UnitData>(((Component) this.CardSlots[index2]).gameObject, unit);
              this.CardSlots[index2].SetSlotData<ConceptCardData>(unit?.MainConceptCard);
              ((Component) this.CardSlots[index2]).GetComponent<ConceptCardIcon>().Setup(unit?.MainConceptCard);
            }
          }
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
      {
        if (this.mSupportSlotData != null && this.mSupportSlotData.Type != SRPG.PartySlotType.Free)
          this.mCurrentSupport = (SupportData) null;
        this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
        this.FriendSlot.SetSlotData<SupportData>(this.mCurrentSupport);
        this.FriendSlot.SetSlotData<UnitData>(this.mCurrentSupport == null ? (UnitData) null : this.mCurrentSupport.Unit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendCardSlot, (UnityEngine.Object) null))
        {
          DataSource.Bind<UnitData>(((Component) this.FriendCardSlot).gameObject, this.mCurrentSupport == null ? (UnitData) null : this.mCurrentSupport.Unit);
          this.FriendCardSlot.SetSlotData<ConceptCardData>(this.mCurrentSupport == null ? (ConceptCardData) null : this.mCurrentSupport.Unit.MainConceptCard);
          ((Component) this.FriendCardSlot).GetComponent<ConceptCardIcon>().Setup(this.mCurrentSupport == null ? (ConceptCardData) null : this.mCurrentSupport.Unit.MainConceptCard);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestInfo, (UnityEngine.Object) null))
        this.QuestInfo.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CurrentForwardButton, (UnityEngine.Object) null))
      {
        ((Component) this.CurrentForwardButton).gameObject.SetActive(this.ShowForwardButton);
        BackHandler component = ((Component) this.CurrentForwardButton).GetComponent<BackHandler>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          ((Behaviour) component).enabled = !this.ShowBackButton;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackButton, (UnityEngine.Object) null))
        ((Component) this.BackButton).gameObject.SetActive(this.ShowBackButton);
      bool flag = this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Normal || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Character || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Event || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Tower || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Raid || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.GuildRaid || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.StoryExtra || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.GvG || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.WorldRaid;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecommendTeamButton, (UnityEngine.Object) null))
      {
        ((Component) this.RecommendTeamButton).gameObject.SetActive(true);
        ((Selectable) this.RecommendTeamButton).interactable = false;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BreakupButton, (UnityEngine.Object) null))
      {
        ((Component) this.BreakupButton).gameObject.SetActive(true);
        ((Selectable) this.BreakupButton).interactable = false;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RenameButton, (UnityEngine.Object) null))
        ((Component) this.RenameButton).gameObject.SetActive(flag);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevButton, (UnityEngine.Object) null))
        ((Component) this.PrevButton).gameObject.SetActive(flag);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextButton, (UnityEngine.Object) null))
        ((Component) this.NextButton).gameObject.SetActive(flag);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecentTeamButton, (UnityEngine.Object) null))
      {
        if (this.mCurrentQuest != null && (this.mCurrentQuest.type == QuestTypes.Tutorial || this.mCurrentQuest.type == QuestTypes.GenesisBoss || this.mCurrentQuest.type == QuestTypes.AdvanceBoss))
          ((Component) this.RecentTeamButton).gameObject.SetActive(false);
        else
          ((Component) this.RecentTeamButton).gameObject.SetActive(flag);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextFixParty, (UnityEngine.Object) null))
        ((Component) this.TextFixParty).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BattleSettingButton, (UnityEngine.Object) null))
        ((Component) this.BattleSettingButton).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HelpButton, (UnityEngine.Object) null))
        ((Component) this.HelpButton).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filter, (UnityEngine.Object) null))
        this.Filter.SetActive(true);
      this.ToggleRaidInfo();
      this.RefreshRaidTicketNum();
      this.RefreshRaidButtons();
      this.RefreshGuildRaid();
      this.LockSlots();
      this.OnPartyMemberChange();
      this.LoadInventory();
      this.RefreshAutoRepeatQuestButton();
    }

    public void RefreshArenaDefUnits()
    {
      if (this.CurrentParty == null)
      {
        this.mOwnUnits = new List<UnitData>((IEnumerable<UnitData>) ArenaDefenceUnits.mArenaDefUnits);
        for (int index = 0; index < this.mOwnUnits.Count; ++index)
          this.mOwnUnits[index].TempFlags |= UnitData.TemporaryFlags.TemporaryUnitData | UnitData.TemporaryFlags.AllowJobChange;
      }
      else
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        this.mOwnUnits = new List<UnitData>((IEnumerable<UnitData>) ArenaDefenceUnits.mArenaDefUnits);
        int num = 3;
        for (int index1 = 0; index1 < this.mOwnUnits.Count; ++index1)
        {
          for (int index2 = 0; index2 < num; ++index2)
          {
            if (this.CurrentParty.Units[index2] != null && this.mOwnUnits[index1].UnitID == this.CurrentParty.Units[index2].UnitID)
            {
              this.mOwnUnits[index1].Setup(player.Units[index1]);
              this.mOwnUnits[index1].TempFlags |= UnitData.TemporaryFlags.TemporaryUnitData | UnitData.TemporaryFlags.AllowJobChange;
              this.mOwnUnits[index1].SetJob(PlayerPartyTypes.ArenaDef);
            }
          }
        }
      }
    }

    public void Refresh(bool keepTeam = false)
    {
      this.Refresh_StoryExChallengeCount();
      if (!this.UseQuestInfo)
      {
        this.RefreshNoneQuestInfo(keepTeam);
      }
      else
      {
        this.mIsHeloOnly = this.IsHeroSoloParty();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestInfo, (UnityEngine.Object) null))
        {
          if (this.ShowQuestInfo)
          {
            DataSource.Bind<QuestParam>(this.QuestInfo, this.mCurrentQuest);
            GameParameter.UpdateAll(this.QuestInfo);
            this.QuestInfo.SetActive(true);
          }
          else
            this.QuestInfo.SetActive(false);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_SankaFuka, (UnityEngine.Object) null))
          {
            for (int index = 0; index < this.mSankaFukaIcons.Length && index < this.UnitSlots.Length; ++index)
            {
              if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSankaFukaIcons[index], (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null))
              {
                this.mSankaFukaIcons[index] = UnityEngine.Object.Instantiate<GameObject>(this.Prefab_SankaFuka);
                RectTransform transform = this.mSankaFukaIcons[index].transform as RectTransform;
                transform.anchoredPosition = Vector2.zero;
                ((Transform) transform).SetParent(((Component) this.UnitSlots[index]).transform, false);
              }
            }
          }
        }
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        PlayerPartyTypes playerPartyType = this.mCurrentPartyType.ToPlayerPartyType();
        if (Array.IndexOf<PlayerPartyTypes>(this.SaveJobs, playerPartyType) >= 0)
        {
          if (playerPartyType == PlayerPartyTypes.ArenaDef)
          {
            ArenaDefenceUnits.CompleteLoading();
            this.RefreshArenaDefUnits();
          }
          else
          {
            this.mOwnUnits = new List<UnitData>(player.Units.Count);
            for (int index = 0; index < player.Units.Count; ++index)
            {
              UnitData unitData = new UnitData();
              unitData.Setup(player.Units[index]);
              unitData.TempFlags |= UnitData.TemporaryFlags.TemporaryUnitData | UnitData.TemporaryFlags.AllowJobChange;
              unitData.SetJob(playerPartyType);
              this.mOwnUnits.Add(unitData);
            }
          }
        }
        else
        {
          this.mOwnUnits = new List<UnitData>((IEnumerable<UnitData>) player.Units);
          for (int index = 0; index < this.mOwnUnits.Count; ++index)
            this.mOwnUnits[index].TempFlags |= UnitData.TemporaryFlags.AllowJobChange;
        }
        DataSource.Bind<PlayerPartyTypes>(((Component) this).gameObject, playerPartyType);
        if (!keepTeam)
        {
          if (this.mIsLockCurrentParty)
            this.OverrideLoadTeam();
          else
            this.LoadTeam(this.mCurrentPartyType);
        }
        else
          this.mTeams = UnitOverWriteUtility.ApplyTeams(this.mTeams, this.mCurrentPartyType);
        int index1 = 0;
        for (int index2 = 0; index2 < this.UnitSlots.Length && index2 < this.CurrentParty.Units.Length && index2 < this.mSlotData.Count; ++index2)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index2], (UnityEngine.Object) null))
          {
            this.UnitSlots[index2].SetSlotData<QuestParam>(this.mCurrentQuest);
            PartySlotData partySlotData = this.mSlotData[index2];
            if (partySlotData.Type == SRPG.PartySlotType.ForcedHero)
            {
              if (this.mGuestUnit != null && index1 < this.mGuestUnit.Count)
              {
                UnitData unit = PartyUtility.FindUnit(this.mGuestUnit[index1], this.mOwnUnits);
                this.UnitSlots[index2].SetSlotData<UnitData>(unit);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[index2], (UnityEngine.Object) null))
                {
                  DataSource.Bind<UnitData>(((Component) this.CardSlots[index2]).gameObject, unit);
                  this.CardSlots[index2].SetSlotData<ConceptCardData>(unit?.MainConceptCard);
                  ((Component) this.CardSlots[index2]).GetComponent<ConceptCardIcon>().Setup(unit?.MainConceptCard);
                }
              }
              ++index1;
            }
            else if (partySlotData.Type == SRPG.PartySlotType.Npc || partySlotData.Type == SRPG.PartySlotType.NpcHero)
            {
              UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(partySlotData.UnitName);
              this.UnitSlots[index2].SetSlotData<UnitParam>(unitParam);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[index2], (UnityEngine.Object) null))
              {
                DataSource.Bind<UnitData>(((Component) this.CardSlots[index2]).gameObject, (UnitData) null);
                this.CardSlots[index2].SetSlotData<ConceptCardData>((ConceptCardData) null);
                ((Component) this.CardSlots[index2]).GetComponent<ConceptCardIcon>().Setup((ConceptCardData) null);
              }
            }
            else
            {
              UnitData unit = PartyUtility.FindUnit(this.CurrentParty.Units[index2], this.mOwnUnits);
              this.UnitSlots[index2].SetSlotData<UnitData>(unit);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[index2], (UnityEngine.Object) null))
              {
                DataSource.Bind<UnitData>(((Component) this.CardSlots[index2]).gameObject, unit);
                this.CardSlots[index2].SetSlotData<ConceptCardData>(unit?.MainConceptCard);
                ((Component) this.CardSlots[index2]).GetComponent<ConceptCardIcon>().Setup(unit?.MainConceptCard);
              }
            }
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
        {
          if (this.mSupportSlotData != null && this.mSupportSlotData.Type != SRPG.PartySlotType.Free)
            this.mCurrentSupport = (SupportData) null;
          this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
          this.FriendSlot.SetSlotData<SupportData>(this.mCurrentSupport);
          this.FriendSlot.SetSlotData<UnitData>(this.mCurrentSupport == null ? (UnitData) null : this.mCurrentSupport.Unit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendCardSlot, (UnityEngine.Object) null))
          {
            DataSource.Bind<UnitData>(((Component) this.FriendCardSlot).gameObject, this.mCurrentSupport == null ? (UnitData) null : this.mCurrentSupport.Unit);
            this.FriendCardSlot.SetSlotData<ConceptCardData>(this.mCurrentSupport == null ? (ConceptCardData) null : this.mCurrentSupport.Unit.MainConceptCard);
            ((Component) this.FriendCardSlot).GetComponent<ConceptCardIcon>().Setup(this.mCurrentSupport == null ? (ConceptCardData) null : this.mCurrentSupport.Unit.MainConceptCard);
          }
        }
        if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Character && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestUnitCond, (UnityEngine.Object) null) && this.mGuestUnit != null && this.mGuestUnit.Count > 0)
        {
          DataSource.Bind<UnitData>(this.QuestUnitCond, this.mGuestUnit[0]);
          GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.QUEST_UNIT_ENTRYCONDITION);
        }
        this.UpdateCurrentForwardButton();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CurrentForwardButton, (UnityEngine.Object) null))
        {
          ((Component) this.CurrentForwardButton).gameObject.SetActive(this.ShowForwardButton);
          BackHandler component = ((Component) this.CurrentForwardButton).GetComponent<BackHandler>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            ((Behaviour) component).enabled = !this.ShowBackButton;
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackButton, (UnityEngine.Object) null))
          ((Component) this.BackButton).gameObject.SetActive(this.ShowBackButton);
        if (this.GetPartyCondType() == PartyCondType.Forced || this.IsFixedParty(true) && this.mCurrentPartyType != PartyWindow2.EditPartyTypes.GvG)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecommendTeamButton, (UnityEngine.Object) null))
          {
            ((Component) this.RecommendTeamButton).gameObject.SetActive(true);
            ((Selectable) this.RecommendTeamButton).interactable = false;
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BreakupButton, (UnityEngine.Object) null))
          {
            ((Component) this.BreakupButton).gameObject.SetActive(true);
            ((Selectable) this.BreakupButton).interactable = false;
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RenameButton, (UnityEngine.Object) null))
            ((Component) this.RenameButton).gameObject.SetActive(false);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevButton, (UnityEngine.Object) null))
            ((Component) this.PrevButton).gameObject.SetActive(false);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextButton, (UnityEngine.Object) null))
            ((Component) this.NextButton).gameObject.SetActive(false);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TeamPulldown, (UnityEngine.Object) null))
            ((Component) this.TeamPulldown).gameObject.SetActive(false);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextFixParty, (UnityEngine.Object) null))
            ((Component) this.TextFixParty).gameObject.SetActive(true);
        }
        else
        {
          bool flag = this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Normal || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Character || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Event || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Tower || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Ordeal || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Raid || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.GuildRaid || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.StoryExtra || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.GvG || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.WorldRaid;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecommendTeamButton, (UnityEngine.Object) null))
          {
            ((Component) this.RecommendTeamButton).gameObject.SetActive(flag);
            ((Selectable) this.RecommendTeamButton).interactable = true;
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BreakupButton, (UnityEngine.Object) null))
          {
            ((Component) this.BreakupButton).gameObject.SetActive(flag);
            ((Selectable) this.BreakupButton).interactable = true;
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RenameButton, (UnityEngine.Object) null))
          {
            if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Event && this.ContainsNotFree())
              ((Component) this.RenameButton).gameObject.SetActive(false);
            else
              ((Component) this.RenameButton).gameObject.SetActive(flag);
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevButton, (UnityEngine.Object) null))
            ((Component) this.PrevButton).gameObject.SetActive(flag);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextButton, (UnityEngine.Object) null))
            ((Component) this.NextButton).gameObject.SetActive(flag);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecentTeamButton, (UnityEngine.Object) null))
          {
            if (this.mCurrentQuest != null && (this.mCurrentQuest.type == QuestTypes.Tutorial || this.mCurrentQuest.type == QuestTypes.GenesisBoss || this.mCurrentQuest.type == QuestTypes.AdvanceBoss))
              ((Component) this.RecentTeamButton).gameObject.SetActive(false);
            else
              ((Component) this.RecentTeamButton).gameObject.SetActive(flag);
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextFixParty, (UnityEngine.Object) null))
            ((Component) this.TextFixParty).gameObject.SetActive(false);
        }
        if (this.mCurrentQuest != null && this.mCurrentQuest.IsMulti && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BattleSettingButton, (UnityEngine.Object) null))
          ((Selectable) this.BattleSettingButton).interactable = false;
        this.ToggleRaidInfo();
        this.RefreshRaidTicketNum();
        this.RefreshRaidButtons();
        this.RefreshGuildRaid();
        this.LockSlots();
        this.OnPartyMemberChange();
        this.LoadInventory();
        this.RefreshAutoRepeatQuestButton();
      }
    }

    private void CreateSlots()
    {
      GameUtility.DestroyGameObjects<GenericSlot>(this.UnitSlots);
      GameUtility.DestroyGameObjects<GenericSlot>(this.CardSlots);
      GameUtility.DestroyGameObject((Component) this.FriendSlot);
      GameUtility.DestroyGameObject((Component) this.FriendCardSlot);
      this.mSlotData.Clear();
      List<PartySlotData> collection1 = new List<PartySlotData>();
      List<PartySlotData> collection2 = new List<PartySlotData>();
      PartySlotData slotData1 = (PartySlotData) null;
      if (this.mCurrentQuest == null)
      {
        if (this.PartyType == PartyWindow2.EditPartyTypes.GvG)
        {
          collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main1));
          collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main2));
          collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main3));
        }
        else
        {
          collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main1));
          collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main2));
          collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main3));
          collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main4));
          slotData1 = new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Support);
          collection2.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Sub1));
          collection2.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Sub2));
        }
      }
      else if (this.mCurrentQuest.questParty != null)
      {
        QuestPartyParam questParty = this.mCurrentQuest.questParty;
        collection1.Add(new PartySlotData(questParty.type_1, questParty.unit_1, PartySlotIndex.Main1));
        collection1.Add(new PartySlotData(questParty.type_2, questParty.unit_2, PartySlotIndex.Main2));
        collection1.Add(new PartySlotData(questParty.type_3, questParty.unit_3, PartySlotIndex.Main3));
        collection1.Add(new PartySlotData(questParty.type_4, questParty.unit_4, PartySlotIndex.Main4));
        slotData1 = new PartySlotData(questParty.support_type, (string) null, PartySlotIndex.Support);
        collection2.Add(new PartySlotData(questParty.subtype_1, questParty.subunit_1, PartySlotIndex.Sub1));
        collection2.Add(new PartySlotData(questParty.subtype_2, questParty.subunit_2, PartySlotIndex.Sub2));
      }
      else
      {
        string unitName1 = this.mCurrentQuest.units == null || this.mCurrentQuest.units.Length <= 0 ? (string) null : this.mCurrentQuest.units.GetList()[0];
        if (this.GetPartyCondType() != PartyCondType.Forced)
        {
          if (this.mCurrentQuest.type == QuestTypes.Tower || this.mCurrentQuest.type == QuestTypes.Raid || this.mCurrentQuest.type == QuestTypes.GuildRaid)
          {
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main1));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main2));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main3));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main4));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main5));
            slotData1 = new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Support);
            collection2.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Sub1));
            collection2.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Sub2));
          }
          else if (this.mCurrentQuest.type == QuestTypes.WorldRaid)
          {
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main1));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main2));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main3));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main4));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main5));
            collection2.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Sub1));
            collection2.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Sub2));
          }
          else if (this.mCurrentQuest.type == QuestTypes.Ordeal)
          {
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main1));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main2));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main3));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main4));
            slotData1 = new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Support);
          }
          else if (this.mCurrentQuest.type == QuestTypes.VersusFree || this.mCurrentQuest.type == QuestTypes.RankMatch || this.mCurrentQuest.type == QuestTypes.VersusRank)
          {
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main1));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main2));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main3));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Main4));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Main5));
          }
          else if (this.mCurrentQuest.type == QuestTypes.Arena)
          {
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main1));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main2));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main3));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Main4));
            slotData1 = new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Support);
            collection2.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Sub1));
            collection2.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Sub2));
          }
          else if (this.mCurrentQuest.type == QuestTypes.GvG)
          {
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main1));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main2));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main3));
          }
          else if (this.mCurrentQuest.type == QuestTypes.Multi)
          {
            MyPhoton.MyRoom currentRoom = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
            JSON_MyPhotonRoomParam myPhotonRoomParam = currentRoom == null || string.IsNullOrEmpty(currentRoom.json) ? (JSON_MyPhotonRoomParam) null : JSON_MyPhotonRoomParam.Parse(currentRoom.json);
            if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.RAID && myPhotonRoomParam != null)
            {
              int unitSlotNum = myPhotonRoomParam.GetUnitSlotNum();
              for (int index = 0; index < 4; ++index)
              {
                if (index < unitSlotNum)
                  collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, (PartySlotIndex) index));
                else
                  collection1.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, (PartySlotIndex) index, true));
              }
            }
            slotData1 = new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Support, true);
            collection2.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Sub1, true));
            collection2.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Sub2, true));
          }
          else
          {
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main1));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main2));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main3));
            if (string.IsNullOrEmpty(unitName1))
              collection1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main4));
            else
              collection1.Add(new PartySlotData(SRPG.PartySlotType.ForcedHero, unitName1, PartySlotIndex.Main4));
            slotData1 = new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Support);
            collection2.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Sub1));
            collection2.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Sub2));
          }
        }
        else
        {
          QuestCondParam cond = this.GetPartyCondition();
          if (cond.unit.Length > 1)
          {
            int index = 0;
            Func<string> func = (Func<string>) (() =>
            {
              if (index >= cond.unit.Length)
                return (string) null;
              string[] unit = cond.unit;
              int num;
              index = (num = index) + 1;
              int index1 = num;
              return unit[index1];
            });
            string unitName2 = func();
            collection1.Add(new PartySlotData(unitName2 != null ? SRPG.PartySlotType.Forced : SRPG.PartySlotType.Locked, unitName2, PartySlotIndex.Main1));
            string unitName3 = func();
            collection1.Add(new PartySlotData(unitName3 != null ? SRPG.PartySlotType.Forced : SRPG.PartySlotType.Locked, unitName3, PartySlotIndex.Main2));
            string unitName4 = func();
            collection1.Add(new PartySlotData(unitName4 != null ? SRPG.PartySlotType.Forced : SRPG.PartySlotType.Locked, unitName4, PartySlotIndex.Main3));
            string str = func();
            collection1.Add(new PartySlotData(SRPG.PartySlotType.ForcedHero, unitName1, PartySlotIndex.Main4));
            slotData1 = new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Support);
            string unitName5 = func();
            collection2.Add(new PartySlotData(unitName5 != null ? SRPG.PartySlotType.Forced : SRPG.PartySlotType.Locked, unitName5, PartySlotIndex.Sub1));
            string unitName6 = func();
            collection2.Add(new PartySlotData(unitName6 != null ? SRPG.PartySlotType.Forced : SRPG.PartySlotType.Locked, unitName6, PartySlotIndex.Sub2));
          }
          else
          {
            if (cond.unit[0] == unitName1)
              collection1.Add(new PartySlotData(SRPG.PartySlotType.ForcedHero, unitName1, PartySlotIndex.Main1));
            else
              collection1.Add(new PartySlotData(SRPG.PartySlotType.Forced, cond.unit[0], PartySlotIndex.Main1));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Main2));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Main3));
            collection1.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Main4));
            slotData1 = new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Support);
            collection2.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Sub1));
            collection2.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Sub2));
          }
        }
      }
      List<GenericSlot> genericSlotList1 = new List<GenericSlot>();
      List<GenericSlot> genericSlotList2 = new List<GenericSlot>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MainMemberHolder, (UnityEngine.Object) null) && collection1.Count > 0)
      {
        foreach (PartySlotData slotData2 in collection1)
        {
          GenericSlot genericSlot = slotData2.Type == SRPG.PartySlotType.Npc || slotData2.Type == SRPG.PartySlotType.NpcHero ? this.CreateSlotObject(slotData2, this.NpcSlotTemplate, this.MainMemberHolder) : this.CreateSlotObject(slotData2, this.UnitSlotTemplate, this.MainMemberHolder);
          genericSlotList1.Add(genericSlot);
          GenericSlot slotObject = this.CreateSlotObject(slotData2, this.CardSlotTemplate, this.MainMemberCardHolder);
          if (!GlobalVars.IsTutorialEnd)
            GameUtility.SetButtonIntaractable(((Component) slotObject).gameObject, false);
          genericSlotList2.Add(slotObject);
        }
        if (slotData1 != null)
        {
          this.FriendSlot = this.CreateSlotObject(slotData1, this.UnitSlotTemplate, this.MainMemberHolder);
          this.FriendCardSlot = this.CreateSlotObject(slotData1, this.CardSlotTemplate, this.MainMemberCardHolder);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SubMemberHolder, (UnityEngine.Object) null) && collection2.Count > 0)
      {
        foreach (PartySlotData slotData3 in collection2)
        {
          GenericSlot genericSlot = slotData3.Type == SRPG.PartySlotType.Npc || slotData3.Type == SRPG.PartySlotType.NpcHero ? this.CreateSlotObject(slotData3, this.NpcSlotTemplate, this.SubMemberHolder) : this.CreateSlotObject(slotData3, this.UnitSlotTemplate, this.SubMemberHolder);
          genericSlotList1.Add(genericSlot);
          GenericSlot slotObject = this.CreateSlotObject(slotData3, this.CardSlotTemplate, this.SubMemberCardHolder);
          genericSlotList2.Add(slotObject);
        }
      }
      this.mSlotData.AddRange((IEnumerable<PartySlotData>) collection1);
      this.mSlotData.AddRange((IEnumerable<PartySlotData>) collection2);
      this.mSupportSlotData = slotData1;
      this.UnitSlots = genericSlotList1.ToArray();
      this.CardSlots = genericSlotList2.ToArray();
      for (int index = 0; index < this.UnitSlots.Length && index < this.mSlotData.Count; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null) && this.mSlotData[index].Type == SRPG.PartySlotType.Free)
        {
          this.UnitSlots[index].OnSelect = new GenericSlot.SelectEvent(this.OnUnitSlotClick);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[index], (UnityEngine.Object) null))
            this.CardSlots[index].OnSelect = new GenericSlot.SelectEvent(this.OnCardSlotClick);
        }
        else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null) && (this.mSlotData[index].Type == SRPG.PartySlotType.Forced || this.mSlotData[index].Type == SRPG.PartySlotType.ForcedHero) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[index], (UnityEngine.Object) null))
          this.CardSlots[index].OnSelect = new GenericSlot.SelectEvent(this.OnCardSlotClick);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
        return;
      if (this.mSupportSlotData != null && this.mSupportSlotData.Type == SRPG.PartySlotType.Free)
        this.FriendSlot.OnSelect = new GenericSlot.SelectEvent(this.OnSupportUnitSlotClick);
      if (this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.Tower)
      {
        TowerFloorParam towerFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(this.mCurrentQuest.iname);
        if (towerFloor != null)
        {
          ((Component) this.FriendSlot).gameObject.SetActive(towerFloor.can_help);
          ((Component) this.SupportSkill).gameObject.SetActive(towerFloor.can_help);
        }
      }
      if (this.mCurrentQuest == null || this.mCurrentQuest.type != QuestTypes.Raid && this.mCurrentQuest.type != QuestTypes.GuildRaid && this.mCurrentQuest.type != QuestTypes.WorldRaid)
        return;
      bool flag = false;
      ((Component) this.FriendSlot).gameObject.SetActive(flag);
      ((Component) this.SupportSkill).gameObject.SetActive(flag);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendCardSlot, (UnityEngine.Object) null))
        return;
      ((Component) this.FriendCardSlot).gameObject.SetActive(flag);
    }

    private GenericSlot CreateSlotObject(
      PartySlotData slotData,
      GenericSlot template,
      Transform parent)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) template, (UnityEngine.Object) null))
        return (GenericSlot) null;
      GenericSlot component = UnityEngine.Object.Instantiate<GameObject>(((Component) template).gameObject).GetComponent<GenericSlot>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GuildRaidManager.Instance, (UnityEngine.Object) null) && GuildRaidManager.Instance.IsForcedDeck)
      {
        UnitIcon componentInChildren = ((Component) component).GetComponentInChildren<UnitIcon>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
          componentInChildren.Tooltip = false;
      }
      ((Component) component).transform.SetParent(parent, false);
      ((Component) component).gameObject.SetActive(true);
      component.SetSlotData<PartySlotData>(slotData);
      return component;
    }

    private void RefreshSankaStates()
    {
      if (this.mCurrentQuest == null)
      {
        for (int index = 0; index < this.mSankaFukaIcons.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSankaFukaIcons[index], (UnityEngine.Object) null))
            this.mSankaFukaIcons[index].SetActive(false);
          if ((this.CurrentParty.Units.Length > index || this.PartyType != PartyWindow2.EditPartyTypes.GvG) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null))
            this.UnitSlots[index].SetMainColor(Color.white);
        }
      }
      else
      {
        for (int index = 0; index < this.mSankaFukaIcons.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSankaFukaIcons[index], (UnityEngine.Object) null))
          {
            bool flag = true;
            if (this.CurrentParty.Units.Length <= index)
              break;
            if (this.CurrentParty.Units[index] != null)
              flag = this.mCurrentQuest.IsUnitAllowed(this.CurrentParty.Units[index]);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null))
            {
              if (flag)
                this.UnitSlots[index].SetMainColor(Color.white);
              else
                this.UnitSlots[index].SetMainColor(new Color(this.SankaFukaOpacity, this.SankaFukaOpacity, this.SankaFukaOpacity, 1f));
            }
            this.mSankaFukaIcons[index].SetActive(!flag);
          }
        }
      }
    }

    private void ToggleRaidInfo()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RaidInfo, (UnityEngine.Object) null))
        return;
      bool flag = false;
      if (this.mCurrentQuest != null && !string.IsNullOrEmpty(this.mCurrentQuest.ticket) && this.mCurrentQuest.state == QuestStates.Cleared)
      {
        if (MonoSingleton<GameManager>.Instance.MasterParam.FixParam.RaidEffectiveTime <= TimeManager.FromDateTime(TimeManager.ServerTime))
        {
          if (this.mCurrentQuest.IsMissionCompleteALL())
            flag = true;
        }
        else
          flag = true;
      }
      if (!this.ShowRaidInfo)
        flag = false;
      this.RaidInfo.SetActive(flag);
    }

    private void RefreshRaidTicketNum()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RaidTicketNum, (UnityEngine.Object) null))
        return;
      int num = 0;
      if (this.mCurrentQuest != null && !string.IsNullOrEmpty(this.mCurrentQuest.ticket))
      {
        ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mCurrentQuest.ticket);
        if (itemDataByItemId != null)
          num = itemDataByItemId.Num;
      }
      this.RaidTicketNum.text = num.ToString();
    }

    private void LockSlots()
    {
      this.mLockedPartySlots = 0;
      this.mSupportLocked = false;
      this.mItemsLocked = false;
      if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.MP)
      {
        this.mSupportLocked = true;
        this.mItemsLocked = true;
        if (GameUtility.GetCurrentScene() == GameUtility.EScene.HOME_MULTI)
        {
          MyPhoton.MyRoom currentRoom = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
          JSON_MyPhotonRoomParam myPhotonRoomParam = currentRoom == null || string.IsNullOrEmpty(currentRoom.json) ? (JSON_MyPhotonRoomParam) null : JSON_MyPhotonRoomParam.Parse(currentRoom.json);
          if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.RAID)
          {
            if (myPhotonRoomParam != null)
            {
              int unitSlotNum = myPhotonRoomParam.GetUnitSlotNum();
              for (int index = 0; index < this.CurrentParty.PartyData.MAX_UNIT; ++index)
              {
                if (index >= unitSlotNum)
                  this.mLockedPartySlots |= 1 << index;
                if (index < this.UnitSlots.Length && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index].SelectButton, (UnityEngine.Object) null))
                  ((Selectable) this.UnitSlots[index].SelectButton).interactable = index < unitSlotNum;
              }
            }
          }
          else
          {
            for (int submemberStart = this.CurrentParty.PartyData.SUBMEMBER_START; submemberStart < this.CurrentParty.PartyData.MAX_UNIT; ++submemberStart)
            {
              this.mLockedPartySlots |= 1 << submemberStart;
              if (submemberStart < this.UnitSlots.Length && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[submemberStart], (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[submemberStart].SelectButton, (UnityEngine.Object) null))
                ((Selectable) this.UnitSlots[submemberStart].SelectButton).interactable = false;
            }
          }
        }
      }
      else if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Arena || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.ArenaDef)
      {
        this.mSupportLocked = true;
        this.mItemsLocked = true;
        for (int index = 0; index < this.CurrentParty.PartyData.MAX_UNIT; ++index)
        {
          if (index >= 3)
            this.mLockedPartySlots |= 1 << index;
        }
      }
      else if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.MultiTower)
      {
        this.mSupportLocked = true;
        this.mItemsLocked = true;
        for (int index = 0; index < this.UnitSlots.Length; ++index)
          ((Selectable) this.UnitSlots[index].SelectButton).interactable = true;
      }
      else if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Versus)
      {
        if (MonoSingleton<GameManager>.Instance.GetVSMode() == VS_MODE.THREE_ON_THREE)
        {
          for (int vswaitmemberStart = this.CurrentParty.PartyData.VSWAITMEMBER_START; vswaitmemberStart < this.CurrentParty.PartyData.VSWAITMEMBER_END; ++vswaitmemberStart)
            this.mLockedPartySlots |= 1 << vswaitmemberStart;
        }
      }
      else if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.RankMatch)
      {
        if (MonoSingleton<GameManager>.Instance.GetVSMode() == VS_MODE.THREE_ON_THREE)
        {
          for (int vswaitmemberStart = this.CurrentParty.PartyData.VSWAITMEMBER_START; vswaitmemberStart < this.CurrentParty.PartyData.VSWAITMEMBER_END; ++vswaitmemberStart)
            this.mLockedPartySlots |= 1 << vswaitmemberStart;
        }
      }
      else if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Normal || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Character || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Event)
      {
        this.mSupportLocked = false;
        this.mItemsLocked = false;
        for (int index = 0; index < this.mSlotData.Count; ++index)
        {
          PartySlotData partySlotData = this.mSlotData[index];
          if (partySlotData.Type == SRPG.PartySlotType.Locked || partySlotData.Type == SRPG.PartySlotType.Npc || partySlotData.Type == SRPG.PartySlotType.NpcHero)
            this.mLockedPartySlots |= 1 << index;
        }
      }
      if (this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.Tutorial)
      {
        this.mItemsLocked = true;
        this.mSupportLocked = true;
        for (int submemberStart = this.CurrentParty.PartyData.SUBMEMBER_START; submemberStart <= this.CurrentParty.PartyData.SUBMEMBER_END; ++submemberStart)
          this.mLockedPartySlots |= 1 << submemberStart;
      }
      if (this.mCurrentQuest != null && this.mIsLockCurrentParty && this.mCurrentQuest.type == QuestTypes.GuildRaid)
      {
        this.mSupportLocked = true;
        this.mItemsLocked = false;
      }
      if (this.mCurrentQuest != null && !this.mCurrentQuest.UseSupportUnit)
        this.mSupportLocked = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NoItemText, (UnityEngine.Object) null))
        this.NoItemText.SetActive(this.mItemsLocked);
      for (int index = 0; index < this.UnitSlots.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null))
          this.UnitSlots[index].SetLocked((this.mLockedPartySlots & 1 << index) != 0);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
        this.FriendSlot.SetLocked(this.mSupportLocked);
      for (int index = 0; index < this.ItemSlots.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemSlots[index], (UnityEngine.Object) null))
          this.ItemSlots[index].SetLocked(this.mItemsLocked);
      }
      for (int index = 0; index < this.PopupItemSlots.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PopupItemSlots[index], (UnityEngine.Object) null))
          this.PopupItemSlots[index].SetLocked(this.mItemsLocked);
      }
    }

    protected virtual void LoadInventory()
    {
      this.RefreshAllItemSlots();
      if (this.RemoveExpiredItem_Inventory())
        this.RefreshAllItemSlots();
      this.OnItemSlotsChange();
    }

    private void RefreshAllItemSlots()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int slotIndex = 0; slotIndex < this.mCurrentItems.Length && slotIndex < player.Inventory.Length; ++slotIndex)
        this.SetItemSlot(slotIndex, player.Inventory[slotIndex]);
      this.OnItemSlotsChange();
    }

    private bool RemoveExpiredItem_Inventory()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (!player.IsExistExpireItem_Inventory())
        return false;
      List<ItemData> itemDataList = new List<ItemData>();
      for (int index = 0; index < 5; ++index)
      {
        if (player.Inventory[index] != null && !player.Inventory[index].Param.IsExpire)
          itemDataList.Add(player.Inventory[index]);
      }
      this.mCurrentItems = new ItemData[5];
      for (int index = 0; index < 5 && index < itemDataList.Count; ++index)
        this.mCurrentItems[index] = itemDataList[index];
      this.SaveInventory(true);
      return true;
    }

    public void LoadRecommendedTeamSetting()
    {
      GlobalVars.RecommendTeamSetting recommendTeamSetting = (GlobalVars.RecommendTeamSetting) null;
      if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.RECOMMENDED_TEAM_SETTING_KEY))
      {
        string str = PlayerPrefsUtility.GetString(PlayerPrefsUtility.RECOMMENDED_TEAM_SETTING_KEY, string.Empty);
        try
        {
          recommendTeamSetting = JsonUtility.FromJson<GlobalVars.RecommendTeamSetting>(str);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
        }
      }
      GlobalVars.RecommendTeamSettingValue = recommendTeamSetting;
    }

    private void SaveRecommendedTeamSetting(GlobalVars.RecommendTeamSetting setting)
    {
      string json = JsonUtility.ToJson((object) setting);
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.RECOMMENDED_TEAM_SETTING_KEY, json, true);
    }

    protected virtual void SaveTeamPresets()
    {
      if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Arena || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.ArenaDef || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.MP || (this.mCurrentQuest == null || !this.IsEventTypeQuest()) && this.ContainsNpcOrForced())
        return;
      if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.GvG)
      {
        PartyUtility.SaveTeamPresets(this.mCurrentPartyType, this.mCurrentTeamIndex, this.mTeams, this.ContainsNotFree(), this.mSlotData);
      }
      else
      {
        if (this.GetPartyCondType() == PartyCondType.Forced || this.IsFixedParty(true))
          return;
        PartyUtility.SaveTeamPresets(this.mCurrentPartyType, this.mCurrentTeamIndex, this.mTeams, this.ContainsNotFree(), this.mSlotData);
      }
    }

    protected void LoadTeam(PartyWindow2.EditPartyTypes edit_party_type)
    {
      if (edit_party_type == PartyWindow2.EditPartyTypes.Auto)
        throw new InvalidPartyTypeException();
      this.mGuestUnit.Clear();
      this.mMaxTeamCount = edit_party_type.GetMaxTeamCount();
      PlayerPartyTypes playerPartyType = edit_party_type.ToPlayerPartyType();
      this.mTeams.Clear();
      if (this.GetPartyCondType() == PartyCondType.Forced || this.IsFixedParty())
      {
        PartyData partyOfType = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(playerPartyType);
        UnitData[] src = new UnitData[this.mSlotData.Count];
        for (int index = 0; index < this.mSlotData.Count; ++index)
        {
          UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(this.mSlotData[index].UnitName);
          if (this.mSlotData[index].Type == SRPG.PartySlotType.ForcedHero)
          {
            this.mGuestUnit.Add(unitDataByUnitId);
            src[index] = (UnitData) null;
          }
          else
            src[index] = this.mSlotData[index].Type == SRPG.PartySlotType.Npc || this.mSlotData[index].Type == SRPG.PartySlotType.NpcHero || this.mSlotData[index].Type == SRPG.PartySlotType.Locked && !this.mSlotData[index].IsSettable ? (UnitData) null : unitDataByUnitId;
        }
        PartyEditData partyEditData = new PartyEditData(string.Empty, partyOfType);
        partyEditData.SetUnitsForce(src);
        this.mTeams.Add(partyEditData);
        this.SetCurrentParty(0);
      }
      else
      {
        if (edit_party_type != PartyWindow2.EditPartyTypes.Arena && edit_party_type != PartyWindow2.EditPartyTypes.ArenaDef && edit_party_type != PartyWindow2.EditPartyTypes.MP)
        {
          bool containsNotFree = this.ContainsNotFree();
          int lastSelectionIndex;
          List<PartyEditData> partyEditDataList = PartyUtility.LoadTeamPresets(edit_party_type, out lastSelectionIndex, containsNotFree);
          if (this.mCurrentQuest != null && this.IsEventTypeQuest() && containsNotFree)
            this.mMaxTeamCount = 1;
          if (partyEditDataList != null)
          {
            this.mTeams = partyEditDataList;
            this.mCurrentTeamIndex = lastSelectionIndex;
            if (this.mCurrentTeamIndex < 0 || this.mMaxTeamCount <= this.mCurrentTeamIndex)
              this.mCurrentTeamIndex = 0;
          }
        }
        if (this.mTeams.Count > this.mMaxTeamCount)
          this.mTeams = this.mTeams.Take<PartyEditData>(this.mMaxTeamCount).ToList<PartyEditData>();
        else if (this.mTeams.Count < this.mMaxTeamCount)
        {
          PartyData partyOfType = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(playerPartyType);
          if (playerPartyType == PlayerPartyTypes.Ordeal)
          {
            for (int count = this.mTeams.Count; count < this.mMaxTeamCount; ++count)
              this.mTeams.Add(new PartyEditData(PartyUtility.CreateOrdealPartyNameFromIndex(count), partyOfType));
          }
          else
          {
            for (int count = this.mTeams.Count; count < this.mMaxTeamCount; ++count)
              this.mTeams.Add(new PartyEditData(PartyUtility.CreateDefaultPartyNameFromIndex(count), partyOfType));
          }
        }
        if (this.mCurrentTeamIndex < 0 || this.mCurrentTeamIndex >= this.mTeams.Count)
          this.mCurrentTeamIndex = 0;
        if (this.EnableTeamAssign && (GlobalVars.SelectedTeamIndex >= 0 || GlobalVars.SelectedTeamIndex < this.mMaxTeamCount))
          this.SetCurrentParty(GlobalVars.SelectedTeamIndex);
        this.AssignUnits(this.CurrentParty);
      }
      this.mTeams = UnitOverWriteUtility.ApplyTeams(this.mTeams, edit_party_type);
      this.ValidateSupport(this.mMaxTeamCount);
      this.mCurrentSupport = this.mSupports[this.mCurrentTeamIndex];
      this.ResetTeamPulldown(this.mTeams, this.mMaxTeamCount, this.mCurrentQuest);
      this.ChangeEnabledArrowButtons(this.mCurrentTeamIndex, this.mMaxTeamCount);
      this.ChangeEnabledTeamTabs(this.mCurrentTeamIndex);
    }

    protected virtual void OverrideLoadTeam() => this.LoadTeam(this.mCurrentPartyType);

    private void AssignUnits(PartyEditData partyEditData)
    {
      UnitData[] unitDataArray = new UnitData[this.mSlotData.Count];
      UnitData[] array1 = ((IEnumerable<UnitData>) partyEditData.Units).ToArray<UnitData>();
      string[] array2 = this.mSlotData.Where<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.Forced || slot.Type == SRPG.PartySlotType.ForcedHero)).Select<PartySlotData, string>((Func<PartySlotData, string>) (slot => slot.UnitName)).ToArray<string>();
      int num1 = 0;
      for (int index = 0; index < unitDataArray.Length && index < this.mSlotData.Count; ++index)
      {
        PartySlotData partySlotData = this.mSlotData[index];
        if (partySlotData.Type == SRPG.PartySlotType.ForcedHero)
        {
          UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(partySlotData.UnitName);
          if (unitDataByUnitId != null)
            this.mGuestUnit.Add(unitDataByUnitId);
          unitDataArray[index] = (UnitData) null;
        }
        else if (partySlotData.Type == SRPG.PartySlotType.Forced)
          unitDataArray[index] = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(partySlotData.UnitName);
        else if (partySlotData.Type == SRPG.PartySlotType.Npc || partySlotData.Type == SRPG.PartySlotType.NpcHero || partySlotData.Type == SRPG.PartySlotType.Locked && !partySlotData.IsSettable)
        {
          unitDataArray[index] = (UnitData) null;
        }
        else
        {
          int num2 = index >= this.CurrentParty.PartyData.MAX_MAINMEMBER ? this.CurrentParty.PartyData.MAX_UNIT : this.CurrentParty.PartyData.MAX_MAINMEMBER;
          while (num1 < num2 && num1 < array1.Length)
          {
            UnitData unitData = array1[num1++];
            if (unitData != null && !((IEnumerable<string>) array2).Contains<string>(unitData.UnitID))
            {
              unitDataArray[index] = unitData;
              break;
            }
          }
        }
      }
      for (int index = 0; index < partyEditData.Units.Length && index < unitDataArray.Length; ++index)
        partyEditData.Units[index] = unitDataArray[index];
      this.ValidateTeam(partyEditData);
    }

    private bool IsFixedParty(bool isSetParty = false)
    {
      return isSetParty && this.mIsLockCurrentParty || !this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.Free));
    }

    protected bool ContainsNotFree()
    {
      return this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type != SRPG.PartySlotType.Free));
    }

    private bool ContainsNpcOrForcedOrForcedHero()
    {
      return this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.Npc || slot.Type == SRPG.PartySlotType.NpcHero || slot.Type == SRPG.PartySlotType.Forced || slot.Type == SRPG.PartySlotType.ForcedHero));
    }

    private bool ContainsNpcOrForced()
    {
      return this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.Npc || slot.Type == SRPG.PartySlotType.NpcHero || slot.Type == SRPG.PartySlotType.Forced));
    }

    private bool ContainsNpcOrForcedOrLocked()
    {
      return this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.Npc || slot.Type == SRPG.PartySlotType.NpcHero || slot.Type == SRPG.PartySlotType.Forced || slot.Type == SRPG.PartySlotType.Locked));
    }

    private bool ContainsNpcOrForcedHero()
    {
      return this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.Npc || slot.Type == SRPG.PartySlotType.NpcHero || slot.Type == SRPG.PartySlotType.ForcedHero));
    }

    private bool ContainsNpc()
    {
      return this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.Npc || slot.Type == SRPG.PartySlotType.NpcHero));
    }

    private bool ContainsForcedHero()
    {
      return this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.ForcedHero));
    }

    private bool IsSettableSlot(PartySlotData slotData)
    {
      if (slotData.Type == SRPG.PartySlotType.Free)
        return true;
      return slotData.Type == SRPG.PartySlotType.Locked && slotData.IsSettable;
    }

    private bool IsHeroSoloParty()
    {
      bool flag1 = this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.ForcedHero));
      bool flag2 = !this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.Free));
      bool flag3 = !this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.Forced));
      bool flag4 = !this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.Npc || slot.Type == SRPG.PartySlotType.NpcHero));
      return flag1 && flag2 && flag3 && flag4;
    }

    private void ValidateTeam(PartyEditData partyEditData)
    {
      bool flag = false;
      string[] array1 = this.mSlotData.Where<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.Forced || slot.Type == SRPG.PartySlotType.ForcedHero)).Select<PartySlotData, string>((Func<PartySlotData, string>) (slot => slot.UnitName)).ToArray<string>();
      if ((this.mCurrentQuest == null || this.mCurrentQuest.type != QuestTypes.Ordeal) && this.mSlotData[0].Type == SRPG.PartySlotType.Free && partyEditData.Units[0] == null)
        flag |= PartyUtility.AutoSetLeaderUnit(this.mCurrentQuest, partyEditData, array1, MonoSingleton<GameManager>.Instance.Player.Units, this.mSlotData);
      for (int index = 0; index < partyEditData.Units.Length && index < this.mSlotData.Count; ++index)
      {
        PartySlotData partySlotData = this.mSlotData[index];
        if (partySlotData.Type == SRPG.PartySlotType.Npc || partySlotData.Type == SRPG.PartySlotType.NpcHero || partySlotData.Type == SRPG.PartySlotType.ForcedHero || partySlotData.Type == SRPG.PartySlotType.Locked && !partySlotData.IsSettable)
          partyEditData.Units[index] = (UnitData) null;
        else if (partySlotData.Type == SRPG.PartySlotType.Forced)
          partyEditData.Units[index] = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(partySlotData.UnitName);
      }
      if (this.mCurrentQuest != null)
      {
        if (this.IsEventTypeQuest() || this.mCurrentQuest.type == QuestTypes.Gps || this.mCurrentQuest.type == QuestTypes.Beginner)
          flag |= PartyUtility.SetUnitIfEmptyParty(this.mCurrentQuest, this.mTeams, array1, this.mSlotData);
        else if (PartyUtility.IsHeloQuest(this.mCurrentQuest))
        {
          string[] array2 = this.mSlotData.Where<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.ForcedHero)).Select<PartySlotData, string>((Func<PartySlotData, string>) (slot => slot.UnitName)).ToArray<string>();
          for (int index = 0; index < this.mTeams.Count; ++index)
            flag |= PartyUtility.PartyUnitsRemoveHelo(this.mTeams[index], array2);
          if (!this.mIsHeloOnly)
            flag |= PartyUtility.SetUnitIfEmptyParty(this.mCurrentQuest, this.mTeams, array1, this.mSlotData);
        }
      }
      if (!flag)
        return;
      this.SaveTeamPresets();
    }

    private void ValidateSupport(int maxTeamCount)
    {
      if (this.mSupports.Count < maxTeamCount)
      {
        for (int count = this.mSupports.Count; count < maxTeamCount; ++count)
          this.mSupports.Add((SupportData) null);
      }
      else
      {
        if (this.mSupports.Count <= maxTeamCount)
          return;
        this.mSupports.Take<SupportData>(maxTeamCount);
      }
    }

    private PartyCondType GetPartyCondType()
    {
      if (this.mCurrentQuest != null)
      {
        if (this.mCurrentQuest.type == QuestTypes.Character && this.mCurrentQuest.EntryConditionCh != null)
          return this.mCurrentQuest.EntryConditionCh.party_type;
        if (this.mCurrentQuest.EntryCondition != null)
          return this.mCurrentQuest.EntryCondition.party_type;
      }
      return PartyCondType.None;
    }

    private QuestCondParam GetPartyCondition()
    {
      if (this.mCurrentQuest == null)
        return (QuestCondParam) null;
      return this.mCurrentQuest.type == QuestTypes.Character ? this.mCurrentQuest.EntryConditionCh : this.mCurrentQuest.EntryCondition;
    }

    protected void ResetTeamPulldown(
      List<PartyEditData> teams,
      int maxTeams,
      QuestParam currentQuest,
      FixedScrollablePulldown pullDown = null)
    {
      if (this.GetPartyCondType() == PartyCondType.Forced || this.mIsHeloOnly)
        return;
      FixedScrollablePulldown fsPullDown = this.TeamPulldown;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) pullDown, (UnityEngine.Object) null))
        fsPullDown = pullDown;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) fsPullDown, (UnityEngine.Object) null))
        return;
      if (maxTeams <= 1)
      {
        ((Component) fsPullDown).gameObject.SetActive(false);
      }
      else
      {
        fsPullDown.ResetAllItems();
        for (int index = 0; index < teams.Count && index < fsPullDown.ItemCount; ++index)
          fsPullDown.SetItem(teams[index].Name, index, index);
        fsPullDown.Selection = this.mCurrentTeamIndex;
        ((Component) fsPullDown).gameObject.SetActive(true);
        for (int index = 0; index < fsPullDown.ItemCount; ++index)
        {
          Transform transform = ((Component) fsPullDown.GetItemAt(index)).transform.Find("Button_Rename");
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) transform, (UnityEngine.Object) null))
          {
            SRPG_Button component = ((Component) transform).GetComponent<SRPG_Button>();
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.AddListener((SRPG_Button.ButtonClickEvent) (go =>
              {
                this.mSelectedRenameTeamIndex = fsPullDown.IndexOf(((Component) ((Component) go).transform.parent).GetComponent<PulldownItem>());
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 250);
              }));
          }
        }
      }
    }

    private void RefreshGuildRaid()
    {
      if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.GuildRaid)
      {
        GuildRaidManager instance = GuildRaidManager.Instance;
        for (int index = 0; index < this.CurrentParty.PartyData.MAX_UNIT; ++index)
        {
          if (index < this.CardSlots.Length && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[index], (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[index].SelectButton, (UnityEngine.Object) null))
            ((Selectable) this.CardSlots[index].SelectButton).interactable = true;
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.IsForcedDeck && instance.BattleType == GuildRaidBattleType.Main)
        {
          this.SetForcedConceptCardUnit(this.CurrentParty.Units);
          GameUtility.SetGameObjectActive(this.mGuildRaidForcedDeck, true);
          return;
        }
      }
      GameUtility.SetGameObjectActive(this.mGuildRaidForcedDeck, false);
    }

    private void RefreshRaidButtons(bool is_refresh_btn_interactable = true)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int num1 = 0;
      ItemData itemData = (ItemData) null;
      ItemParam data1 = (ItemParam) null;
      if (this.mCurrentQuest != null)
      {
        num1 = this.mCurrentQuest.RequiredApWithPlayerLv(player.Lv);
        itemData = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mCurrentQuest.ticket);
        if (itemData != null)
          data1 = itemData.Param;
        else if (!string.IsNullOrEmpty(this.mCurrentQuest.ticket))
          data1 = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentQuest.ticket);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RaidInfo, (UnityEngine.Object) null))
        {
          DataSource.Bind<ItemParam>(this.RaidInfo.gameObject, data1);
          GameParameter.UpdateAll(this.RaidInfo.gameObject);
        }
      }
      int num2 = 0;
      if (itemData != null)
        num2 = itemData.Num;
      if (this.mCurrentQuest != null)
      {
        bool isGenAdvBoss = this.mCurrentQuest.IsGenAdvBoss;
        if (isGenAdvBoss && this.GenAdvBossChItemParam == null)
        {
          if (this.mCurrentQuest.IsGenesisBoss)
            GenesisBossInfo.GetBossChallengeItemInfo(ref this.GenAdvBossChItemParam, ref this.GenAdvBossChItemNeedNum);
          if (this.mCurrentQuest.IsAdvanceBoss)
            AdvanceBossInfo.GetBossChallengeItemInfo(ref this.GenAdvBossChItemParam, ref this.GenAdvBossChItemNeedNum);
        }
        ItemData itemDataByItemParam = this.GenAdvBossChItemParam == null ? (ItemData) null : MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(this.GenAdvBossChItemParam);
        int num3 = itemDataByItemParam == null ? 0 : itemDataByItemParam.Num;
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.RaidIconAp))
          this.RaidIconAp.SetActive(!isGenAdvBoss);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.RaidIconItem))
        {
          this.RaidIconItem.SetActive(isGenAdvBoss);
          if (this.GenAdvBossChItemParam != null)
          {
            ItemData data2 = new ItemData();
            data2.Setup(0L, this.GenAdvBossChItemParam, this.GenAdvBossChItemNeedNum);
            DataSource.Bind<ItemData>(this.RaidIconItem, data2, true);
            ItemIcon component = this.RaidIconItem.GetComponent<ItemIcon>();
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) component))
              component.UpdateValue();
          }
        }
        int num4 = this.MaxRaidNum;
        if (!isGenAdvBoss)
        {
          if (num1 > 0)
            num4 = Mathf.Min(player.Stamina / num1, this.MaxRaidNum);
        }
        else if (this.GenAdvBossChItemNeedNum > 0)
          num4 = Mathf.Min(num3 / this.GenAdvBossChItemNeedNum, this.MaxRaidNum);
        int num5 = Mathf.Min(num2, num4);
        int ticketCountLimitMax = QuestParam.GetRaidTicketCount_LimitMax(this.mCurrentQuest, this.MaxRaidNum);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RaidN, (UnityEngine.Object) null))
        {
          if (this.mMultiRaidNum <= 0)
            this.mMultiRaidNum = Mathf.Min(this.MaxRaidNum, this.DefaultRaidNum);
          this.mMultiRaidNum = Mathf.Min(new int[3]
          {
            this.mMultiRaidNum,
            num5,
            ticketCountLimitMax
          });
          this.RaidNCount.text = LocalizedText.Get("sys.RAIDNUM", (object) Mathf.Max(this.mMultiRaidNum, 1));
          if (!isGenAdvBoss)
          {
            ((Selectable) this.RaidN).interactable = ticketCountLimitMax >= 1 && num2 >= 1;
          }
          else
          {
            ((Selectable) this.RaidN).interactable = ticketCountLimitMax >= 1 && num2 >= 1 && this.mMultiRaidNum >= 1;
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ForwardButton))
              ((Selectable) this.ForwardButton).interactable = num3 >= this.GenAdvBossChItemNeedNum;
          }
        }
        if (!isGenAdvBoss)
        {
          this.RefreshRaidAP(Mathf.Max(this.mMultiRaidNum, 1));
        }
        else
        {
          int num6 = Mathf.Max(this.mMultiRaidNum, 1);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RaidAPCurrent, (UnityEngine.Object) null))
            this.RaidAPCurrent.text = num3.ToString();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RaidAPAfter, (UnityEngine.Object) null))
          {
            int num7 = num3 - this.GenAdvBossChItemNeedNum * num6;
            this.RaidAPAfter.text = num7.ToString();
            if (num7 >= 0)
              ((Graphic) this.RaidAPAfter).color = this.RaidAPColorEnable;
            else
              ((Graphic) this.RaidAPAfter).color = this.RaidAPColorDisable;
          }
        }
        if (!is_refresh_btn_interactable)
          return;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RaidTicketPlus, (UnityEngine.Object) null))
          ((Selectable) this.RaidTicketPlus).interactable = this.mMultiRaidNum < num5 && this.mMultiRaidNum < ticketCountLimitMax;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RaidTicketMinus, (UnityEngine.Object) null))
          return;
        ((Selectable) this.RaidTicketMinus).interactable = this.mMultiRaidNum > 1;
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Raid, (UnityEngine.Object) null))
          ((Selectable) this.Raid).interactable = false;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RaidN, (UnityEngine.Object) null))
          return;
        ((Selectable) this.RaidN).interactable = false;
      }
    }

    private void RefreshRaidAP(int count)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RaidAPCurrent, (UnityEngine.Object) null))
        this.RaidAPCurrent.text = MonoSingleton<GameManager>.Instance.Player.Stamina.ToString();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RaidAPAfter, (UnityEngine.Object) null))
        return;
      int num = MonoSingleton<GameManager>.Instance.Player.Stamina - this.mCurrentQuest.RequiredApWithPlayerLv(MonoSingleton<GameManager>.Instance.Player.Lv) * count;
      this.RaidAPAfter.text = num.ToString();
      if (num >= 0)
        ((Graphic) this.RaidAPAfter).color = this.RaidAPColorEnable;
      else
        ((Graphic) this.RaidAPAfter).color = this.RaidAPColorDisable;
    }

    private UnitData[] GetDefaultTeam()
    {
      UnitData[] defaultTeam = new UnitData[this.CurrentParty.PartyData.MAX_UNIT];
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int index1 = 0;
      for (int index2 = 0; index2 < player.Units.Count && index2 < defaultTeam.Length; ++index2)
      {
        if (!player.Units[index2].UnitParam.IsHero())
        {
          defaultTeam[index1] = player.Units[index2];
          ++index1;
        }
      }
      return defaultTeam;
    }

    private bool TeamsAvailable
    {
      get
      {
        return this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Normal || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Event;
      }
    }

    protected virtual int AvailableMainMemberSlots => this.CurrentParty.PartyData.MAX_MAINMEMBER;

    public virtual void Activated(int pinID)
    {
      switch (pinID)
      {
        case 40:
          if (string.IsNullOrEmpty(GlobalVars.TeamName))
            break;
          if (this.mSelectedRenameTeamIndex >= 0)
          {
            this.mTeams[this.mSelectedRenameTeamIndex].Name = GlobalVars.TeamName;
            this.mSelectedRenameTeamIndex = -1;
          }
          else
            this.CurrentParty.Name = GlobalVars.TeamName;
          this.SaveTeamPresets();
          this.ResetTeamPulldown(this.mTeams, this.mCurrentPartyType.GetMaxTeamCount(), this.mCurrentQuest);
          break;
        case 41:
          if (GlobalVars.RecommendTeamSettingValue == null)
            break;
          this.SaveRecommendedTeamSetting(GlobalVars.RecommendTeamSettingValue);
          this.OrganizeRecommendedParty(GlobalVars.RecommendTeamSettingValue.recommendedType, GlobalVars.RecommendTeamSettingValue.recommendedElement);
          if (this.AutoEquipConceptCardToParty())
            break;
          this.RefreshUnitSlots();
          break;
        case 50:
          this.RefreshQuest();
          break;
        case 130:
          this.Refresh();
          break;
        case 140:
          this.Refresh(true);
          break;
        case 150:
          this.ForceRefreshCardData();
          break;
        case 300:
          this.ForceSavePartyData();
          break;
        case 330:
          this.Refresh_ResetChallengeCount();
          break;
        default:
          this.UnitList_Activated(pinID);
          break;
      }
    }

    private void OnStaminaChange()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
        return;
      this.RefreshRaidButtons();
    }

    private void RefreshUnitList()
    {
      List<UnitData> unitDataList = new List<UnitData>((IEnumerable<UnitData>) this.mOwnUnits);
      this.UnitList.ClearItems();
      bool selectedSlotIsEmpty = this.CurrentParty.Units[this.mSelectedSlotIndex] == null;
      if ((this.mSelectedSlotIndex > 0 || this.mIsHeloOnly) && (!selectedSlotIsEmpty || this.AlwaysShowRemoveUnit))
        this.UnitList.AddItem(0);
      bool heroesAvailable = PartyUtility.IsHeroesAvailable(this.mCurrentPartyType, this.mCurrentQuest);
      if (this.UseQuestInfo && (this.IsEventTypeQuest() || this.mCurrentQuest.type == QuestTypes.Beginner || this.PartyType == PartyWindow2.EditPartyTypes.Character))
      {
        string[] strArray = this.mCurrentQuest.questParty == null ? this.mCurrentQuest.units.GetList() : ((IEnumerable<PartySlotTypeUnitPair>) this.mCurrentQuest.questParty.GetMainSubSlots()).Where<PartySlotTypeUnitPair>((Func<PartySlotTypeUnitPair, bool>) (slot => slot.Type == SRPG.PartySlotType.ForcedHero)).Select<PartySlotTypeUnitPair, string>((Func<PartySlotTypeUnitPair, string>) (slot => slot.Unit)).ToArray<string>();
        if (strArray != null)
        {
          for (int index = 0; index < strArray.Length; ++index)
          {
            string chQuestHeroId = strArray[index];
            UnitData unitData = unitDataList.FirstOrDefault<UnitData>((Func<UnitData, bool>) (u => u.UnitParam.iname == chQuestHeroId));
            if (unitData != null)
              unitDataList.Remove(unitData);
          }
        }
      }
      int numMainMembers = 0;
      for (int mainmemberStart = this.CurrentParty.PartyData.MAINMEMBER_START; mainmemberStart <= this.CurrentParty.PartyData.MAINMEMBER_END; ++mainmemberStart)
      {
        if (this.CurrentParty.Units[mainmemberStart] != null)
          ++numMainMembers;
      }
      for (int index = 0; index < this.CurrentParty.PartyData.MAX_UNIT; ++index)
      {
        if (this.CurrentParty.Units[index] != null && (heroesAvailable || !this.CurrentParty.Units[index].UnitParam.IsHero()) && (this.CurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.CurrentParty.PartyData.SUBMEMBER_END || index != 0 || !selectedSlotIsEmpty || numMainMembers > 1))
        {
          if (this.UseQuestInfo)
          {
            string empty = string.Empty;
            if (!this.mCurrentQuest.IsEntryQuestCondition(this.CurrentParty.Units[index], ref empty))
              continue;
          }
          this.UnitList.AddItem(this.mOwnUnits.IndexOf(PartyUtility.FindUnit(this.CurrentParty.Units[index], this.mOwnUnits)) + 1);
          unitDataList.Remove(this.CurrentParty.Units[index]);
        }
      }
      int count = unitDataList.Count;
      UnitListV2.FilterUnits(unitDataList, (List<int>) null, this.mUnitFilter);
      if (this.mReverse)
        unitDataList.Reverse();
      this.RegistPartyMember(unitDataList, heroesAvailable, selectedSlotIsEmpty, numMainMembers);
      this.UnitList.Refresh(true);
      this.UnitList.ForceUpdateItems();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitListHilit, (UnityEngine.Object) null))
      {
        ((Component) this.UnitListHilit).gameObject.SetActive(false);
        ((Transform) this.UnitListHilit).SetParent(((Component) this).transform, false);
        UnitData unit = this.CurrentParty.Units[this.mSelectedSlotIndex];
        if (unit != null)
        {
          int itemID = this.mOwnUnits.IndexOf(unit) + 1;
          if (itemID > 0)
          {
            RectTransform parent = this.UnitList.FindItem(itemID);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) parent, (UnityEngine.Object) null))
              this.AttachAndEnable((Transform) this.UnitListHilit, (Transform) parent, this.UnitListHilitParent);
          }
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NoMatchingUnit, (UnityEngine.Object) null))
        return;
      this.NoMatchingUnit.SetActive(count > 0 && this.UnitList.NumItems <= 0);
    }

    protected virtual void RegistPartyMember(
      List<UnitData> allUnits,
      bool heroesAvailable,
      bool selectedSlotIsEmpty,
      int numMainMembers)
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      bool flag = UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.CurrentState == MyPhoton.MyState.ROOM;
      for (int index = 0; index < allUnits.Count; ++index)
      {
        if ((heroesAvailable || !allUnits[index].UnitParam.IsHero()) && (this.CurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.CurrentParty.PartyData.SUBMEMBER_END || allUnits[index] != this.CurrentParty.Units[0] || !selectedSlotIsEmpty || numMainMembers > 1))
        {
          if (flag)
          {
            MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
            if (currentRoom != null)
            {
              JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
              if (allUnits[index].CalcLevel() < myPhotonRoomParam.unitlv)
                continue;
            }
          }
          this.UnitList.AddItem(this.mOwnUnits.IndexOf(allUnits[index]) + 1);
        }
      }
    }

    private void OnItemFilterChange(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      PartyWindow2.ItemFilterTypes itemFilterTypes = PartyWindow2.ItemFilterTypes.All;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) this.ItemFilter_Offense))
        itemFilterTypes = PartyWindow2.ItemFilterTypes.Offense;
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) this.ItemFilter_Support))
        itemFilterTypes = PartyWindow2.ItemFilterTypes.Support;
      if (this.mItemFilter == itemFilterTypes)
        return;
      this.mItemFilter = itemFilterTypes;
      for (int index = 0; index < this.mItemFilterToggles.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mItemFilterToggles[index], (UnityEngine.Object) null))
          this.mItemFilterToggles[index].IsOn = (PartyWindow2.ItemFilterTypes) index == itemFilterTypes;
      }
      this.RefreshItemList();
    }

    private static void ToggleBlockRaycasts(Component component, bool block)
    {
      CanvasGroup component1 = component.GetComponent<CanvasGroup>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        return;
      component1.blocksRaycasts = block;
    }

    protected void LockWindow(bool y)
    {
      if (y)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 5);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 6);
    }

    public virtual void Reopen(bool farceRefresh = false)
    {
      if (farceRefresh || this.mCurrentQuest != null && this.mCurrentQuest.iname != GlobalVars.SelectedQuestID)
      {
        this.RefreshQuest();
        this.CreateSlots();
        this.Refresh();
      }
      else if (this.mCurrentQuest != null && this.mCurrentQuest.IsKeyQuest)
        this.Refresh();
      if (this.RemoveExpiredItem_Inventory())
        this.RefreshAllItemSlots();
      this.GoToUnitList();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 4);
    }

    private void ShowRaidSettings()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int num = this.mCurrentQuest.RequiredApWithPlayerLv(player.Lv);
      if (player.Stamina < num)
      {
        MonoSingleton<GameManager>.Instance.StartBuyStaminaSequence(true, this);
      }
      else
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.RaidSettingsTemplate, (UnityEngine.Object) null) || !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRaidSettings, (UnityEngine.Object) null))
          return;
        this.mRaidSettings = UnityEngine.Object.Instantiate<GameObject>(this.RaidSettingsTemplate).GetComponent<RaidSettingsWindow>();
        this.mRaidSettings.OnAccept = new RaidSettingsWindow.RaidSettingsEvent(this.RaidSettingsAccepted);
        this.mRaidSettings.Setup(this.mCurrentQuest, this.mMultiRaidNum, this.MaxRaidNum);
      }
    }

    private bool PrepareRaid(int num, bool validateOnly, bool skipConfirm = false)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int num1 = this.mCurrentQuest.RequiredApWithPlayerLv(player.Lv);
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mCurrentQuest.ticket);
      int num2 = itemDataByItemId == null ? 0 : itemDataByItemId.Num;
      this.mNumRaids = num;
      if (this.mCurrentQuest.GetChapterChallangeLimit() > 0)
        this.mNumRaids = Mathf.Min(this.mNumRaids, this.mCurrentQuest.GetChapterChallangeLimit() - this.mCurrentQuest.GetChapterChallangeCount());
      else if (this.mCurrentQuest.GetChallangeLimit() > 0)
        this.mNumRaids = Mathf.Min(this.mNumRaids, this.mCurrentQuest.GetChallangeLimit() - this.mCurrentQuest.GetChallangeCount());
      int num3 = 0;
      bool isGenAdvBoss = this.mCurrentQuest.IsGenAdvBoss;
      if (!isGenAdvBoss)
      {
        num3 = num1 * this.mNumRaids;
        if (player.Stamina < num3)
        {
          MonoSingleton<GameManager>.Instance.StartBuyStaminaSequence(true, this);
          return false;
        }
      }
      else if (this.GenAdvBossNoTicketConfirm())
        return false;
      if (this.mCurrentQuest.IsQuestDrops && UnityEngine.Object.op_Inequality((UnityEngine.Object) QuestDropParam.Instance, (UnityEngine.Object) null))
      {
        bool flag = QuestDropParam.Instance.IsChangedQuestDrops(this.mCurrentQuest);
        GlobalVars.SetDropTableGeneratedTime();
        if (flag)
        {
          this.HardQuestDropPiecesUpdate();
          if (!QuestDropParam.Instance.IsWarningPopupDisable)
          {
            UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.PARTYEDITOR_DROP_TABLE"), (UIUtility.DialogResultEvent) (go => this.OpenQuestDetail()));
            return false;
          }
        }
      }
      ItemParam itemParam = itemDataByItemId == null ? MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentQuest.ticket) : itemDataByItemId.Param;
      if (num2 < this.mNumRaids)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.NO_RAID_TICKET", (object) itemParam.name), (UIUtility.DialogResultEvent) (go => { }));
        return false;
      }
      this.mMultiRaidNum = num;
      if (validateOnly)
        return false;
      if (skipConfirm)
      {
        this.OnRaidAccept((GameObject) null);
        return true;
      }
      string empty = string.Empty;
      string text;
      if (!isGenAdvBoss)
        text = LocalizedText.Get("sys.CONFIRM_RAID", (object) this.mNumRaids, (object) num3, (object) itemParam.name);
      else
        text = LocalizedText.Get("sys.CONFIRM_RAID_GEN_ADV_BOSS", (object) this.mNumRaids, (object) num3, (object) itemParam.name);
      UIUtility.ConfirmBox(text, (string) null, new UIUtility.DialogResultEvent(this.OnRaidAccept), (UIUtility.DialogResultEvent) null);
      return true;
    }

    private bool GenAdvBossNoTicketConfirm(bool is_ticket_no_check = false)
    {
      if (this.mCurrentQuest == null || !this.mCurrentQuest.IsGenAdvBoss)
        return false;
      int num1 = Mathf.Max(this.mMultiRaidNum, 1);
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mCurrentQuest.ticket);
      int num2 = itemDataByItemId == null ? 0 : itemDataByItemId.Num;
      ItemParam itemParam = itemDataByItemId == null ? MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentQuest.ticket) : itemDataByItemId.Param;
      string str1 = itemParam == null ? LocalizedText.Get("sys.SKIPBATTLE_TICKET_GEN_ADV_BOSS") : itemParam.name;
      bool flag1 = num2 <= 0 || num2 < num1;
      if (is_ticket_no_check)
        flag1 = false;
      if (this.GenAdvBossChItemParam == null)
      {
        if (this.mCurrentQuest.IsGenesisBoss)
          GenesisBossInfo.GetBossChallengeItemInfo(ref this.GenAdvBossChItemParam, ref this.GenAdvBossChItemNeedNum);
        if (this.mCurrentQuest.IsAdvanceBoss)
          AdvanceBossInfo.GetBossChallengeItemInfo(ref this.GenAdvBossChItemParam, ref this.GenAdvBossChItemNeedNum);
      }
      ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(this.GenAdvBossChItemParam);
      int num3 = itemDataByItemParam == null ? 0 : itemDataByItemParam.Num;
      int num4 = this.GenAdvBossChItemNeedNum * num1;
      string str2 = this.GenAdvBossChItemParam == null ? LocalizedText.Get("sys.SKIPBATTLE_TICKET_GEN_ADV_BOSS") : this.GenAdvBossChItemParam.name;
      bool flag2 = num3 < num4;
      if (!flag1 && !flag2)
        return false;
      string msg = LocalizedText.Get("sys.NO_RAID_TICKET", (object) str2);
      if (flag1 && flag2)
        msg = LocalizedText.Get("sys.NO_GEN_ADV_BOSS_RAID_TICKET", (object) str1, (object) str2);
      else if (flag1)
        msg = LocalizedText.Get("sys.NO_RAID_TICKET", (object) str1);
      UIUtility.NegativeSystemMessage((string) null, msg, (UIUtility.DialogResultEvent) (go => { }));
      return true;
    }

    public void RaidSettingsAccepted(RaidSettingsWindow window)
    {
      this.PrepareRaid(window.Count, false, true);
    }

    public void SetSortMethod(string method, bool ascending, string[] filters)
    {
      GameUtility.UnitSortModes unitSortModes = GameUtility.UnitSortModes.Time;
      try
      {
        if (!string.IsNullOrEmpty(method))
          unitSortModes = (GameUtility.UnitSortModes) Enum.Parse(typeof (GameUtility.UnitSortModes), method, true);
      }
      catch (Exception ex)
      {
        if (GameUtility.IsDebugBuild)
          DebugUtility.LogError("Unknown sort mode: " + method);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AscendingIcon, (UnityEngine.Object) null))
        this.AscendingIcon.SetActive(ascending);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DescendingIcon, (UnityEngine.Object) null))
        this.DescendingIcon.SetActive(!ascending);
      if (unitSortModes == GameUtility.UnitSortModes.Time)
        ascending = !ascending;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SortModeCaption, (UnityEngine.Object) null))
        this.SortModeCaption.text = LocalizedText.Get("sys.SORT_" + unitSortModes.ToString().ToUpper());
      this.mReverse = ascending;
      this.mUnitFilter = filters;
      if (!this.mUnitSlotSelected)
        return;
      this.RefreshUnitList();
    }

    private void OnEnable()
    {
      UnitJobDropdown.OnJobChange += new UnitJobDropdown.JobChangeEvent(this.OnUnitJobChange);
    }

    private void OnDisable()
    {
      UnitJobDropdown.OnJobChange -= new UnitJobDropdown.JobChangeEvent(this.OnUnitJobChange);
    }

    private void OnUnitJobChange(long unitUniqueID) => this.Refresh(true);

    private T GetComponents<T>(GameObject root, string targetName, bool includeInactive) where T : Component
    {
      foreach (T componentsInChild in root.GetComponentsInChildren<T>(includeInactive))
      {
        if (componentsInChild.name == targetName)
          return componentsInChild;
      }
      return (T) null;
    }

    public void ResetTeamName() => GlobalVars.TeamName = string.Empty;

    public void BreakupTeam()
    {
      this.BreakupTeamImpl();
      this.OnPartyMemberChange();
      this.SaveTeamPresets();
    }

    private void BreakupTeamImpl(bool isSlotChange = true)
    {
      int num = 0;
      for (int index = 0; index < this.mSlotData.Count; ++index)
      {
        PartySlotData partySlotData = this.mSlotData[index];
        if (partySlotData.Type == SRPG.PartySlotType.Free || partySlotData.Type == SRPG.PartySlotType.Forced || partySlotData.Type == SRPG.PartySlotType.ForcedHero || partySlotData.Type == SRPG.PartySlotType.Npc || partySlotData.Type == SRPG.PartySlotType.NpcHero)
        {
          num = index;
          break;
        }
      }
      for (int slotIndex = 0; slotIndex < this.CurrentParty.PartyData.MAX_UNIT; ++slotIndex)
      {
        if (slotIndex != num)
          this.SetPartyUnitForce(slotIndex, (UnitData) null, isSlotChange);
      }
      this.SetSupport((SupportData) null);
    }

    public void PrevTeam() => this.OnPrevTeamChange();

    public void NextTeam() => this.OnNextTeamChange();

    public void OnTeamTabChange()
    {
      this.OnTeamChangeImpl((FlowNode_ButtonEvent.currentValue as SerializeValueList).GetInt("tab_index"));
    }

    public void GoToUnitList()
    {
      if (this.PartyType != PartyWindow2.EditPartyTypes.MultiTower)
        return;
      int towerMultiPartyIndex = GlobalVars.SelectedTowerMultiPartyIndex;
      if (!this.IsMultiTowerPartySlot(towerMultiPartyIndex))
        return;
      this.mSelectedSlotIndex = towerMultiPartyIndex;
      this.UnitList_Show(true);
    }

    private List<List<UnitData>> SeparateUnitByElement(
      List<UnitData> allUnits,
      IEnumerable<string> kyouseiUnits,
      EElement targetElement,
      bool isHeroAvailable)
    {
      List<UnitData> unitDataList1 = new List<UnitData>();
      List<UnitData> unitDataList2 = new List<UnitData>();
      List<UnitData> unitDataList3 = new List<UnitData>();
      List<UnitData> unitDataList4 = new List<UnitData>();
      List<UnitData> unitDataList5 = new List<UnitData>();
      List<UnitData> unitDataList6 = new List<UnitData>();
      List<UnitData> unitDataList7 = new List<UnitData>();
      HashSet<string> source = kyouseiUnits != null ? new HashSet<string>(kyouseiUnits) : new HashSet<string>();
      if (this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.Ordeal)
      {
        for (int index = 0; index < this.mMaxTeamCount; ++index)
        {
          if (index != this.mCurrentTeamIndex)
          {
            foreach (UnitData unit in this.mTeams[index].Units)
            {
              if (unit != null)
                source.Add(unit.UnitID);
            }
          }
        }
      }
      foreach (UnitData allUnit in allUnits)
      {
        UnitData unit = allUnit;
        string str = source.FirstOrDefault<string>((Func<string, bool>) (iname => iname == unit.UnitParam.iname));
        if (str != null)
          source.Remove(str);
        else if ((isHeroAvailable || !unit.UnitParam.IsHero()) && (this.mCurrentQuest == null || this.mCurrentQuest.IsEntryQuestCondition(unit)))
        {
          if (targetElement == EElement.None)
          {
            unitDataList1.Add(unit);
          }
          else
          {
            switch (unit.Element)
            {
              case EElement.Fire:
                unitDataList2.Add(unit);
                continue;
              case EElement.Water:
                unitDataList3.Add(unit);
                continue;
              case EElement.Wind:
                unitDataList4.Add(unit);
                continue;
              case EElement.Thunder:
                unitDataList5.Add(unit);
                continue;
              case EElement.Shine:
                unitDataList6.Add(unit);
                continue;
              case EElement.Dark:
                unitDataList7.Add(unit);
                continue;
              default:
                continue;
            }
          }
        }
      }
      List<List<UnitData>> unitDataListList;
      if (targetElement == EElement.None)
      {
        unitDataListList = new List<List<UnitData>>()
        {
          unitDataList1
        };
      }
      else
      {
        unitDataListList = new List<List<UnitData>>()
        {
          unitDataList2,
          unitDataList3,
          unitDataList4,
          unitDataList5,
          unitDataList6,
          unitDataList7
        };
        int index = (int) (targetElement - (byte) 1);
        if (index >= 0 && index < unitDataListList.Count)
        {
          List<UnitData> unitDataList8 = unitDataListList[index];
          unitDataListList.RemoveAt(index);
          unitDataListList.Insert(0, unitDataList8);
        }
      }
      return unitDataListList;
    }

    private void OrganizeRecommendedParty(
      GlobalVars.RecommendType targetType,
      EElement targetElement)
    {
      this.BreakupTeamImpl(false);
      List<UnitSameGroupParam> unitSameGroupParamList = new List<UnitSameGroupParam>();
      List<string> removeTarget = new List<string>();
      foreach (PartySlotData partySlotData in this.mSlotData)
      {
        if (partySlotData.Type == SRPG.PartySlotType.Forced || partySlotData.Type == SRPG.PartySlotType.ForcedHero)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) && MonoSingleton<GameManager>.Instance.MasterParam != null)
          {
            UnitSameGroupParam unitSameGroup = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitSameGroup(partySlotData.UnitName);
            if (unitSameGroup != null)
              unitSameGroupParamList.Add(unitSameGroup);
          }
          removeTarget.Add(partySlotData.UnitName);
        }
      }
      if (this.mCurrentQuest != null && this.mCurrentQuest.IsGvG && UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null))
      {
        GvGRuleParam currentRule = GvGManager.Instance.CurrentRule;
        List<UnitData> unitDataList = new List<UnitData>();
        if (currentRule != null)
          unitDataList.AddRange((IEnumerable<UnitData>) currentRule.GetDisableUnits(MonoSingleton<GameManager>.Instance.Player.Units));
        if (GvGManager.Instance.UsedUnitList != null)
          unitDataList.AddRange((IEnumerable<UnitData>) MonoSingleton<GameManager>.Instance.Player.Units.FindAll((Predicate<UnitData>) (u => GvGManager.Instance.UsedUnitList.Contains(u.UniqueID))));
        unitDataList.ForEach((Action<UnitData>) (u =>
        {
          if (removeTarget.Contains(u.UnitID))
            return;
          removeTarget.Add(u.UnitID);
        }));
      }
      if (this.mCurrentQuest != null && this.mCurrentQuest.IsWorldRaid && UnityEngine.Object.op_Inequality((UnityEngine.Object) WorldRaidBossManager.Instance, (UnityEngine.Object) null))
      {
        foreach (string bossUsedUnitIname in WorldRaidBossManager.GetCurrentBossUsedUnitInameList())
        {
          if (!removeTarget.Contains(bossUsedUnitIname))
            removeTarget.Add(bossUsedUnitIname);
        }
      }
      List<UnitData> list = MonoSingleton<GameManager>.Instance.Player.Units.Where<UnitData>((Func<UnitData, bool>) (unit => !removeTarget.Contains(unit.UnitID))).ToList<UnitData>();
      if (this.mCurrentQuest == null || !this.mCurrentQuest.EnableRentalUnit)
        list.RemoveAll((Predicate<UnitData>) (unit => unit.IsRental));
      List<List<UnitData>> unitDataListList = this.mCurrentQuest != null ? this.SeparateUnitByElement(list, (IEnumerable<string>) this.mCurrentQuest.units.GetList(), targetElement, PartyUtility.IsHeroesAvailable(this.mCurrentPartyType, this.mCurrentQuest)) : this.SeparateUnitByElement(list, (IEnumerable<string>) null, targetElement, PartyUtility.IsHeroesAvailable(this.mCurrentPartyType, (QuestParam) null));
      List<Comparison<UnitData>> comparisonList1 = new List<Comparison<UnitData>>();
      List<Comparison<UnitData>> comparisonList2 = comparisonList1;
      // ISSUE: reference to a compiler-generated field
      if (PartyWindow2.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PartyWindow2.\u003C\u003Ef__mg\u0024cache0 = new Comparison<UnitData>(PartyWindow2.CompareTo_Total);
      }
      // ISSUE: reference to a compiler-generated field
      Comparison<UnitData> fMgCache0 = PartyWindow2.\u003C\u003Ef__mg\u0024cache0;
      comparisonList2.Add(fMgCache0);
      List<Comparison<UnitData>> comparisonList3 = comparisonList1;
      // ISSUE: reference to a compiler-generated field
      if (PartyWindow2.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PartyWindow2.\u003C\u003Ef__mg\u0024cache1 = new Comparison<UnitData>(PartyWindow2.CompareTo_HP);
      }
      // ISSUE: reference to a compiler-generated field
      Comparison<UnitData> fMgCache1 = PartyWindow2.\u003C\u003Ef__mg\u0024cache1;
      comparisonList3.Add(fMgCache1);
      List<Comparison<UnitData>> comparisonList4 = comparisonList1;
      // ISSUE: reference to a compiler-generated field
      if (PartyWindow2.\u003C\u003Ef__mg\u0024cache2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PartyWindow2.\u003C\u003Ef__mg\u0024cache2 = new Comparison<UnitData>(PartyWindow2.CompareTo_Attack);
      }
      // ISSUE: reference to a compiler-generated field
      Comparison<UnitData> fMgCache2 = PartyWindow2.\u003C\u003Ef__mg\u0024cache2;
      comparisonList4.Add(fMgCache2);
      List<Comparison<UnitData>> comparisonList5 = comparisonList1;
      // ISSUE: reference to a compiler-generated field
      if (PartyWindow2.\u003C\u003Ef__mg\u0024cache3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PartyWindow2.\u003C\u003Ef__mg\u0024cache3 = new Comparison<UnitData>(PartyWindow2.CompareTo_Defense);
      }
      // ISSUE: reference to a compiler-generated field
      Comparison<UnitData> fMgCache3 = PartyWindow2.\u003C\u003Ef__mg\u0024cache3;
      comparisonList5.Add(fMgCache3);
      List<Comparison<UnitData>> comparisonList6 = comparisonList1;
      // ISSUE: reference to a compiler-generated field
      if (PartyWindow2.\u003C\u003Ef__mg\u0024cache4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PartyWindow2.\u003C\u003Ef__mg\u0024cache4 = new Comparison<UnitData>(PartyWindow2.CompareTo_Magic);
      }
      // ISSUE: reference to a compiler-generated field
      Comparison<UnitData> fMgCache4 = PartyWindow2.\u003C\u003Ef__mg\u0024cache4;
      comparisonList6.Add(fMgCache4);
      List<Comparison<UnitData>> comparisonList7 = comparisonList1;
      // ISSUE: reference to a compiler-generated field
      if (PartyWindow2.\u003C\u003Ef__mg\u0024cache5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PartyWindow2.\u003C\u003Ef__mg\u0024cache5 = new Comparison<UnitData>(PartyWindow2.CompareTo_Mind);
      }
      // ISSUE: reference to a compiler-generated field
      Comparison<UnitData> fMgCache5 = PartyWindow2.\u003C\u003Ef__mg\u0024cache5;
      comparisonList7.Add(fMgCache5);
      List<Comparison<UnitData>> comparisonList8 = comparisonList1;
      // ISSUE: reference to a compiler-generated field
      if (PartyWindow2.\u003C\u003Ef__mg\u0024cache6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PartyWindow2.\u003C\u003Ef__mg\u0024cache6 = new Comparison<UnitData>(PartyWindow2.CompareTo_Speed);
      }
      // ISSUE: reference to a compiler-generated field
      Comparison<UnitData> fMgCache6 = PartyWindow2.\u003C\u003Ef__mg\u0024cache6;
      comparisonList8.Add(fMgCache6);
      List<Comparison<UnitData>> comparisonList9 = comparisonList1;
      // ISSUE: reference to a compiler-generated field
      if (PartyWindow2.\u003C\u003Ef__mg\u0024cache7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PartyWindow2.\u003C\u003Ef__mg\u0024cache7 = new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeSlash);
      }
      // ISSUE: reference to a compiler-generated field
      Comparison<UnitData> fMgCache7 = PartyWindow2.\u003C\u003Ef__mg\u0024cache7;
      comparisonList9.Add(fMgCache7);
      List<Comparison<UnitData>> comparisonList10 = comparisonList1;
      // ISSUE: reference to a compiler-generated field
      if (PartyWindow2.\u003C\u003Ef__mg\u0024cache8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PartyWindow2.\u003C\u003Ef__mg\u0024cache8 = new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeStab);
      }
      // ISSUE: reference to a compiler-generated field
      Comparison<UnitData> fMgCache8 = PartyWindow2.\u003C\u003Ef__mg\u0024cache8;
      comparisonList10.Add(fMgCache8);
      List<Comparison<UnitData>> comparisonList11 = comparisonList1;
      // ISSUE: reference to a compiler-generated field
      if (PartyWindow2.\u003C\u003Ef__mg\u0024cache9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PartyWindow2.\u003C\u003Ef__mg\u0024cache9 = new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeBlow);
      }
      // ISSUE: reference to a compiler-generated field
      Comparison<UnitData> fMgCache9 = PartyWindow2.\u003C\u003Ef__mg\u0024cache9;
      comparisonList11.Add(fMgCache9);
      List<Comparison<UnitData>> comparisonList12 = comparisonList1;
      // ISSUE: reference to a compiler-generated field
      if (PartyWindow2.\u003C\u003Ef__mg\u0024cacheA == null)
      {
        // ISSUE: reference to a compiler-generated field
        PartyWindow2.\u003C\u003Ef__mg\u0024cacheA = new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeShot);
      }
      // ISSUE: reference to a compiler-generated field
      Comparison<UnitData> fMgCacheA = PartyWindow2.\u003C\u003Ef__mg\u0024cacheA;
      comparisonList12.Add(fMgCacheA);
      List<Comparison<UnitData>> comparisonList13 = comparisonList1;
      // ISSUE: reference to a compiler-generated field
      if (PartyWindow2.\u003C\u003Ef__mg\u0024cacheB == null)
      {
        // ISSUE: reference to a compiler-generated field
        PartyWindow2.\u003C\u003Ef__mg\u0024cacheB = new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeMagic);
      }
      // ISSUE: reference to a compiler-generated field
      Comparison<UnitData> fMgCacheB = PartyWindow2.\u003C\u003Ef__mg\u0024cacheB;
      comparisonList13.Add(fMgCacheB);
      List<Comparison<UnitData>> comparisonList14 = comparisonList1;
      // ISSUE: reference to a compiler-generated field
      if (PartyWindow2.\u003C\u003Ef__mg\u0024cacheC == null)
      {
        // ISSUE: reference to a compiler-generated field
        PartyWindow2.\u003C\u003Ef__mg\u0024cacheC = new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeNone);
      }
      // ISSUE: reference to a compiler-generated field
      Comparison<UnitData> fMgCacheC = PartyWindow2.\u003C\u003Ef__mg\u0024cacheC;
      comparisonList14.Add(fMgCacheC);
      List<Comparison<UnitData>> targetComparators = comparisonList1;
      int comparatorOrder = PartyUtility.RecommendTypeToComparatorOrder(targetType);
      if (comparatorOrder >= 0 && comparatorOrder < targetComparators.Count)
      {
        Comparison<UnitData> comparison = targetComparators[comparatorOrder];
        targetComparators.RemoveAt(comparatorOrder);
        targetComparators.Insert(0, comparison);
      }
      foreach (List<UnitData> unitDataList in unitDataListList)
        unitDataList.Sort((Comparison<UnitData>) ((x, y) =>
        {
          foreach (Comparison<UnitData> comparison in targetComparators)
          {
            int num = comparison(x, y);
            if (num != 0)
              return num;
          }
          return UnitData.CompareTo_Iname(x, y);
        }));
      int num1 = this.CurrentParty.PartyData.MAX_UNIT;
      if (this.mCurrentQuest != null && this.mCurrentQuest.IsGvG && UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null) && GvGManager.Instance.CurrentRule != null && GvGManager.Instance.CurrentRuleUnitCount > 0)
        num1 = Mathf.Max(0, GvGManager.Instance.CurrentRuleUnitCount - (GvGManager.Instance.UsedUnitList != null ? GvGManager.Instance.UsedUnitTodayCount : 0));
      List<UnitData> unitDataList1 = new List<UnitData>();
      foreach (List<UnitData> unitDataList2 in unitDataListList)
      {
        if (num1 > 0)
        {
          List<int> source = new List<int>();
          for (int index1 = 0; index1 < unitDataList2.Count; ++index1)
          {
            bool flag = false;
            for (int index2 = 0; index2 < unitSameGroupParamList.Count; ++index2)
            {
              if (unitSameGroupParamList[index2].IsInGroup(unitDataList2[index1].UnitID))
                flag = true;
            }
            if (flag)
            {
              source.Add(index1);
            }
            else
            {
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) && MonoSingleton<GameManager>.Instance.MasterParam != null)
              {
                UnitSameGroupParam unitSameGroup = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitSameGroup(unitDataList2[index1].UnitID);
                if (unitSameGroup != null)
                  unitSameGroupParamList.Add(unitSameGroup);
              }
              unitDataList1.Add(unitDataList2[index1]);
              source.Add(index1);
              if (--num1 <= 0)
                break;
            }
          }
          foreach (int index in source.Reverse<int>())
            unitDataList2.RemoveAt(index);
        }
        else
          break;
      }
      PartyData partyData = this.CurrentParty.PartyData;
      int num2 = 0;
      for (int index = 0; index < partyData.MAX_UNIT && index < this.mSlotData.Count && num2 < unitDataList1.Count; ++index)
      {
        if (this.IsSettableSlot(this.mSlotData[index]))
          this.SetPartyUnit(index, unitDataList1[num2++], false);
      }
      this.OnPartyMemberChange();
      this.SaveTeamPresets();
    }

    private static int CompareTo_Total(UnitData unit1, UnitData unit2)
    {
      int num = unit1.CalcTotalParameter();
      return unit2.CalcTotalParameter() - num;
    }

    private static int CompareTo_Attack(UnitData unit1, UnitData unit2)
    {
      return (int) unit2.Status.param.atk - (int) unit1.Status.param.atk;
    }

    private static int CompareTo_Magic(UnitData unit1, UnitData unit2)
    {
      return (int) unit2.Status.param.mag - (int) unit1.Status.param.mag;
    }

    private static int CompareTo_Defense(UnitData unit1, UnitData unit2)
    {
      return (int) unit2.Status.param.def - (int) unit1.Status.param.def;
    }

    private static int CompareTo_Mind(UnitData unit1, UnitData unit2)
    {
      return (int) unit2.Status.param.mnd - (int) unit1.Status.param.mnd;
    }

    private static int CompareTo_Speed(UnitData unit1, UnitData unit2)
    {
      return (int) unit2.Status.param.spd - (int) unit1.Status.param.spd;
    }

    private static int CompareTo_HP(UnitData unit1, UnitData unit2)
    {
      return (int) unit2.Status.param.hp - (int) unit1.Status.param.hp;
    }

    private static int CompareTo_AttackType(UnitData unit1, UnitData unit2, AttackDetailTypes type)
    {
      AttackDetailTypes attackDetailType1 = unit1.GetAttackSkill().AttackDetailType;
      AttackDetailTypes attackDetailType2 = unit2.GetAttackSkill().AttackDetailType;
      if (attackDetailType1 == type && attackDetailType2 == type)
        return 0;
      if (attackDetailType1 == type && attackDetailType2 != type)
        return -1;
      return attackDetailType1 != type && attackDetailType2 == type ? 1 : 0;
    }

    private static int CompareTo_AttackTypeSlash(UnitData unit1, UnitData unit2)
    {
      return PartyWindow2.CompareTo_AttackType(unit1, unit2, AttackDetailTypes.Slash);
    }

    private static int CompareTo_AttackTypeStab(UnitData unit1, UnitData unit2)
    {
      return PartyWindow2.CompareTo_AttackType(unit1, unit2, AttackDetailTypes.Stab);
    }

    private static int CompareTo_AttackTypeBlow(UnitData unit1, UnitData unit2)
    {
      return PartyWindow2.CompareTo_AttackType(unit1, unit2, AttackDetailTypes.Blow);
    }

    private static int CompareTo_AttackTypeShot(UnitData unit1, UnitData unit2)
    {
      return PartyWindow2.CompareTo_AttackType(unit1, unit2, AttackDetailTypes.Shot);
    }

    private static int CompareTo_AttackTypeMagic(UnitData unit1, UnitData unit2)
    {
      return PartyWindow2.CompareTo_AttackType(unit1, unit2, AttackDetailTypes.Magic);
    }

    private static int CompareTo_AttackTypeNone(UnitData unit1, UnitData unit2)
    {
      return PartyWindow2.CompareTo_AttackType(unit1, unit2, AttackDetailTypes.None);
    }

    private bool AutoEquipConceptCardToParty()
    {
      if (!GlobalVars.IsAutoEquipConceptCard || MonoSingleton<GameManager>.Instance.Player.ConceptCards == null || MonoSingleton<GameManager>.Instance.Player.ConceptCardNum <= 0)
        return false;
      List<UnitData> unitDataList = new List<UnitData>();
      unitDataList.AddRange((IEnumerable<UnitData>) this.mGuestUnit);
      unitDataList.AddRange((IEnumerable<UnitData>) this.CurrentParty.Units);
      unitDataList.RemoveAll((Predicate<UnitData>) (c => c == null));
      unitDataList.RemoveAll((Predicate<UnitData>) (c => c.MainConceptCard != null));
      if (unitDataList == null)
        return false;
      bool party = false;
      Dictionary<long, long> req_set_list = new Dictionary<long, long>();
      List<ConceptCardData> conceptCardDataList = new List<ConceptCardData>(unitDataList.Count);
      for (int index = 0; index < unitDataList.Count; ++index)
      {
        UnitData unit = unitDataList[index];
        List<ConceptCardData> equipConceptCard = AutoEquipConceptCard.CreateAutoEquipConceptCard(unit, 0, conceptCardDataList.ToArray());
        if (equipConceptCard != null && equipConceptCard.Count >= 1)
        {
          ConceptCardData conceptCardData = equipConceptCard[0];
          if (conceptCardData != null)
          {
            if (unit.MainConceptCard == null || (long) unit.MainConceptCard.UniqueID != (long) conceptCardData.UniqueID)
              party = true;
            req_set_list.Add(unit.UniqueID, (long) conceptCardData.UniqueID);
            conceptCardDataList.Add(conceptCardData);
          }
        }
      }
      if (party)
        SRPG.Network.RequestAPI((WebAPI) new ReqSetConceptCardList(req_set_list, new SRPG.Network.ResponseCallback(this.AutoEquipConceptCardToPartyCallback), EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
      return party;
    }

    private void AutoEquipConceptCardToPartyCallback(WWWResult www)
    {
      if (SRPG.Network.IsError)
      {
        switch (SRPG.Network.ErrCode)
        {
          case SRPG.Network.EErrCode.NoUnitParty:
          case SRPG.Network.EErrCode.NotExistConceptCard:
            FlowNode_Network.Failed();
            return;
        }
      }
      ReqSetConceptCardList.Response body;
      if (EncodingTypes.IsJsonSerializeCompressSelected(!GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK : GlobalVars.SelectedSerializeCompressMethod))
      {
        WebAPI.JSON_BodyResponse<ReqSetConceptCardList.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqSetConceptCardList.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "jsonRes == null");
        body = jsonObject.body;
      }
      else
      {
        PartyWindow2.MP_Response_SetConceptCardList setConceptCardList = SerializerCompressorHelper.Decode<PartyWindow2.MP_Response_SetConceptCardList>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
        DebugUtility.Assert(setConceptCardList != null, "mpRes == null");
        body = setConceptCardList.body;
      }
      try
      {
        if (body == null)
          throw new Exception("response parse error!");
        MonoSingleton<GameManager>.Instance.Player.Deserialize(body.player);
        MonoSingleton<GameManager>.Instance.Player.Deserialize(body.units);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
      }
      SRPG.Network.RemoveAPI();
      this.Refresh(true);
      this.RefreshUnitSlots();
    }

    protected List<UnitData> GetBattleEntryUnits()
    {
      List<UnitData> result = new List<UnitData>();
      Action<GameObject> action = (Action<GameObject>) (go =>
      {
        PartySlotData dataOfClass1 = DataSource.FindDataOfClass<PartySlotData>(go, (PartySlotData) null);
        UnitData dataOfClass2 = DataSource.FindDataOfClass<UnitData>(go, (UnitData) null);
        if (dataOfClass1 == null || dataOfClass2 == null)
          return;
        result.Add(dataOfClass2);
      });
      for (int index = 0; index < this.UnitSlots.Length; ++index)
        action(((Component) this.UnitSlots[index]).gameObject);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
        action(((Component) this.FriendSlot).gameObject);
      return result;
    }

    protected List<ItemData> GetBattleEntryItems()
    {
      List<ItemData> battleEntryItems = new List<ItemData>();
      for (int index = 0; index < 5 && index < this.mCurrentItems.Length; ++index)
      {
        if (this.mCurrentItems[index] != null)
        {
          ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(this.mCurrentItems[index].Param);
          if (itemDataByItemParam != null)
            battleEntryItems.Add(itemDataByItemParam);
        }
      }
      return battleEntryItems;
    }

    public void OnAutoBattleSetting(
      string name,
      ActionCall.EventType eventType,
      SerializeValueList list)
    {
      bool sw = false;
      if (this.mCurrentQuest != null)
        sw = this.mCurrentQuest.FirstAutoPlayProhibit && this.mCurrentQuest.state != QuestStates.Cleared;
      switch (name)
      {
        case "PROHIBIT":
          if (eventType != ActionCall.EventType.START && eventType != ActionCall.EventType.OPEN)
            break;
          list.SetActive("item", sw);
          break;
        case "SETTING":
          Toggle uiToggle1 = list.GetUIToggle("btn_auto");
          Toggle uiToggle2 = list.GetUIToggle("btn_treasure");
          Toggle uiToggle3 = list.GetUIToggle("btn_skill");
          switch (eventType)
          {
            case ActionCall.EventType.START:
            case ActionCall.EventType.OPEN:
              list.SetActive("item", sw);
              if (sw)
              {
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle1, (UnityEngine.Object) null))
                {
                  Toggle toggle = uiToggle1;
                  bool flag = false;
                  ((Selectable) uiToggle1).interactable = flag;
                  int num = flag ? 1 : 0;
                  toggle.isOn = num != 0;
                  list.SetActive("off", true);
                }
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle2, (UnityEngine.Object) null))
                  uiToggle2.isOn = false;
                if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle3, (UnityEngine.Object) null))
                  return;
                uiToggle3.isOn = false;
                return;
              }
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle1, (UnityEngine.Object) null))
              {
                uiToggle1.isOn = GameUtility.Config_UseAutoPlay.Value;
                ((Selectable) uiToggle1).interactable = true;
              }
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle2, (UnityEngine.Object) null))
                uiToggle2.isOn = GameUtility.Config_AutoMode_Treasure.Value;
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle3, (UnityEngine.Object) null))
                uiToggle3.isOn = GameUtility.Config_AutoMode_DisableSkill.Value;
              list.SetActive("off", !GameUtility.Config_UseAutoPlay.Value);
              return;
            case ActionCall.EventType.UPDATE:
              if (sw)
                return;
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle1, (UnityEngine.Object) null))
                GameUtility.Config_UseAutoPlay.Value = uiToggle1.isOn;
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle2, (UnityEngine.Object) null))
                GameUtility.Config_AutoMode_Treasure.Value = uiToggle2.isOn;
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle3, (UnityEngine.Object) null))
                GameUtility.Config_AutoMode_DisableSkill.Value = uiToggle3.isOn;
              list.SetActive("off", !GameUtility.Config_UseAutoPlay.Value);
              return;
            default:
              return;
          }
      }
    }

    private void UnitList_Activated(int pinID)
    {
      switch (pinID)
      {
        case 100:
          this.UnitList_OnSelect();
          break;
        case 101:
          this.UnitList_OnSelectSupport();
          break;
        case 110:
          this.UnitList_OnRemove();
          break;
        case 111:
          this.UnitList_OnRemoveSupport();
          break;
        case 119:
          this.LockWindow(false);
          ButtonEvent.ResetLock("PartyWindow");
          break;
        case 120:
          SRPG_Button button = FlowNode_ButtonEvent.currentValue as SRPG_Button;
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) null))
            button = this.BackButton;
          this.UnitList_OnClosing(button);
          break;
      }
    }

    private void UnitList_Create()
    {
      if (string.IsNullOrEmpty(this.UNITLIST_WINDOW_PATH))
        return;
      GameObject gameObject1 = AssetManager.Load<GameObject>(this.UNITLIST_WINDOW_PATH);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        return;
      GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
        return;
      CanvasStack component1 = gameObject2.GetComponent<CanvasStack>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
      {
        CanvasStack component2 = ((Component) this).GetComponent<CanvasStack>();
        component1.Priority = component2.Priority + 10;
      }
      this.mUnitListWindow = gameObject2.GetComponent<UnitListWindow>();
      this.mUnitListWindow.Enabled(false);
    }

    private void UnitList_Remove()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mUnitListWindow, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) ((Component) this.mUnitListWindow).gameObject, (UnityEngine.Object) null))
        GameUtility.DestroyGameObject(((Component) this.mUnitListWindow).gameObject);
      this.mUnitListWindow = (UnitListWindow) null;
    }

    private void UnitList_Show(bool isAsync = false)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitListWindow, (UnityEngine.Object) null))
        return;
      this.mUnitListWindow.Enabled(true);
      this.LockWindow(true);
      this.mUnitListWindow.AddData("data_units", (object) this.RefreshUnits(this.mOwnUnits.ToArray()));
      this.mUnitListWindow.AddData("data_party", (object) this.mTeams.ToArray());
      this.mUnitListWindow.AddData("data_party_index", (object) this.mCurrentTeamIndex);
      this.mUnitListWindow.AddData("data_quest", (object) this.mCurrentQuest);
      this.mUnitListWindow.AddData("data_slot", (object) this.mSelectedSlotIndex);
      this.mUnitListWindow.AddData("data_heroOnly", (object) this.mIsHeloOnly);
      if (isAsync)
      {
        this.StartCoroutine(this.ShowUnitListCoroutine());
      }
      else
      {
        if (this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.Tower)
          ButtonEvent.Invoke("UNITLIST_BTN_TWPARTY_OPEN", (object) null);
        else
          ButtonEvent.Invoke("UNITLIST_BTN_PARTY_OPEN", (object) null);
        ButtonEvent.Lock("PartyWindow");
      }
    }

    [DebuggerHidden]
    private IEnumerator ShowUnitListCoroutine()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PartyWindow2.\u003CShowUnitListCoroutine\u003Ec__Iterator4()
      {
        \u0024this = this
      };
    }

    private void UnitList_OnSelect(bool IsSameUnitSkip = false)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitListWindow, (UnityEngine.Object) null) || !this.mUnitListWindow.IsEnabled())
        return;
      int selectedSlotIndex = this.mSelectedSlotIndex;
      UnitData unit = this.CurrentParty.Units[this.mSelectedSlotIndex];
      int slotIndex1;
      if (selectedSlotIndex < this.CurrentParty.PartyData.MAX_MAINMEMBER)
      {
        int num = selectedSlotIndex;
        for (int index = selectedSlotIndex; index >= 0; --index)
        {
          if (this.IsSettableSlot(this.mSlotData[index]) && this.CurrentParty.Units[index] == null)
            num = index;
        }
        slotIndex1 = num;
      }
      else
      {
        int num = selectedSlotIndex;
        for (int index = selectedSlotIndex; index >= this.CurrentParty.PartyData.MAX_MAINMEMBER; --index)
        {
          if (this.IsSettableSlot(this.mSlotData[index]) && this.CurrentParty.Units[index] == null)
            num = index;
        }
        slotIndex1 = num;
      }
      UnitData unitData = (UnitData) null;
      if (FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue)
        unitData = currentValue.GetDataSource<UnitData>("_self");
      if (unitData != null && unitData != unit)
      {
        bool edit_party_only = UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType);
        int slotIndex2 = this.CurrentParty.IndexOf(unitData);
        if (slotIndex2 >= 0 && slotIndex1 != slotIndex2)
        {
          this.SetPartyUnit(slotIndex1, unitData, edit_party_only: edit_party_only);
          this.SetPartyUnit(slotIndex2, unit, edit_party_only: edit_party_only);
        }
        else
        {
          UnitSameGroupParam unitSameGroup = UnitSameGroupParam.IsSameUnitInParty(this.CurrentParty.Units, unit, unitData);
          if (unitSameGroup != null && !IsSameUnitSkip)
          {
            int index1 = 0;
            for (int index2 = 0; index2 < this.CurrentParty.Units.Length; ++index2)
            {
              if (this.CurrentParty.Units[index2] != null && unitSameGroup.IsInGroup(this.CurrentParty.Units[index2].UnitID))
              {
                index1 = index2;
                break;
              }
            }
            if (index1 < 0 || index1 >= this.mSlotData.Count || !this.IsSettableSlot(this.mSlotData[index1]))
            {
              UIUtility.NegativeSystemMessage((string) null, string.Format(LocalizedText.Get("sys.PARTYEDITOR_SAMEUNIT_NOT_CHANGE"), (object) unitSameGroup.GetGroupUnitOtherNameText(unitData.UnitID)), (UIUtility.DialogResultEvent) null);
              return;
            }
            UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.PARTYEDITOR_SAMEUNIT_CHANGE"), (object) unitSameGroup.GetGroupUnitOtherNameText(unitData.UnitID)), (string) null, (UIUtility.DialogResultEvent) (go1 =>
            {
              for (int index3 = 0; index3 < this.CurrentParty.Units.Length; ++index3)
              {
                if (this.CurrentParty.Units[index3] != null && unitSameGroup.IsInGroup(this.CurrentParty.Units[index3].UnitID))
                {
                  this.mSelectedSlotIndex = index3;
                  break;
                }
              }
              this.UnitList_OnSelect(true);
            }), (UIUtility.DialogResultEvent) (go2 => { }));
            return;
          }
          this.SetPartyUnit(slotIndex1, unitData, edit_party_only: edit_party_only);
        }
      }
      this.OnPartyMemberChange();
      this.SaveTeamPresets();
      ButtonEvent.Invoke("UNITLIST_BTN_CLOSE", (object) this.CurrentForwardButton);
      this.UnitList_OnSelect_RefreshOverWrite(unitData);
    }

    protected virtual void UnitList_OnSelect_RefreshOverWrite(UnitData selected_unit)
    {
      if (!UnitOverWriteUtility.IsNeedOverWrite(this.mCurrentPartyType))
        return;
      this.SaveParty((PartyWindow2.Callback) (() =>
      {
        this.Refresh(true);
        this.LockWindow(false);
        this.mIsSaving = false;
      }), (PartyWindow2.Callback) (() => UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ILLEGAL_PARTY"), (UIUtility.DialogResultEvent) null)));
    }

    private void UnitList_OnRemove()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitListWindow, (UnityEngine.Object) null) || !this.mUnitListWindow.IsEnabled())
        return;
      this.SetPartyUnit(this.mSelectedSlotIndex, (UnitData) null, edit_party_only: UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType));
      this.OnPartyMemberChange();
      this.SaveTeamPresets();
      ButtonEvent.Invoke("UNITLIST_BTN_CLOSE", (object) this.CurrentForwardButton);
      this.UnitList_OnSelect_RefreshOverWrite((UnitData) null);
    }

    private void UnitList_OnClosing(SRPG_Button button)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mUnitListWindow, (UnityEngine.Object) null))
      {
        if (!this.mUnitListWindow.IsEnabled())
          return;
        this.mUnitListWindow.ClearData();
        UnitListRootWindow.ListData listData = this.mUnitListWindow.rootWindow.GetListData("unitlist");
        if (listData != null)
          listData.selectUniqueID = 0L;
        this.mUnitListWindow.Enabled(false);
      }
      if (this.PartyType != PartyWindow2.EditPartyTypes.MultiTower || !UnityEngine.Object.op_Inequality((UnityEngine.Object) button, (UnityEngine.Object) null))
        return;
      this.OnForwardOrBackButtonClick(button);
    }

    private void SupportList_Show()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitListWindow, (UnityEngine.Object) null))
        return;
      this.mUnitListWindow.Enabled(true);
      this.LockWindow(true);
      this.mUnitListWindow.AddData("data_support", (object) this.mSupports.ToArray());
      this.mUnitListWindow.AddData("data_party_index", (object) this.mCurrentTeamIndex);
      this.mUnitListWindow.AddData("data_quest", (object) this.mCurrentQuest);
      ButtonEvent.Invoke("UNITLIST_BTN_SUPPORT_OPEN", (object) null);
      ButtonEvent.Lock("PartyWindow");
    }

    private void UnitList_OnSelectSupport()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitListWindow, (UnityEngine.Object) null) || !this.mUnitListWindow.IsEnabled())
        return;
      this.mSelectedSupport = !(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue) ? (SupportData) null : currentValue.GetDataSource<SupportData>("_self");
      if (this.mCurrentSupport == this.mSelectedSupport)
        ButtonEvent.Invoke("UNITLIST_BTN_CLOSE", (object) this.CurrentForwardButton);
      else
        UIUtility.ConfirmBox(LocalizedText.Get(this.mSelectedSupport.GetCost() <= 0 ? "sys.SUPPORT_CONFIRM1" : "sys.SUPPORT_CONFIRM2", (object) this.mSelectedSupport.PlayerName, (object) CurrencyBitmapText.CreateFormatedText(this.mSelectedSupport.GetCost().ToString())), (string) null, new UIUtility.DialogResultEvent(this.UnitList_OnAcceptSupport), (UIUtility.DialogResultEvent) null);
    }

    private void UnitList_OnAcceptSupport(GameObject go)
    {
      if (MonoSingleton<GameManager>.Instance.Player.Gold < this.CalculateTotalSupportCost(this.mSelectedSupport))
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.SUPPORT_NOGOLD"), (UIUtility.DialogResultEvent) null);
      }
      else
      {
        this.SetSupport(this.mSelectedSupport);
        this.OnPartyMemberChange();
        ButtonEvent.Invoke("UNITLIST_BTN_CLOSE", (object) this.CurrentForwardButton);
        if (this.mSelectedSupport != null)
        {
          if (this.mSelectedSupport.IsFriend())
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
          else
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 210);
        }
        else
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 220);
      }
    }

    private int CalculateTotalSupportCost(SupportData support)
    {
      if (this.mCurrentPartyType != PartyWindow2.EditPartyTypes.Ordeal)
        return support.GetCost();
      int cost = support.GetCost();
      for (int index = 0; index < this.mSupports.Count; ++index)
      {
        if (index != this.mCurrentTeamIndex && this.mSupports[index] != null)
          cost += this.mSupports[index].GetCost();
      }
      return cost;
    }

    private void UnitList_OnRemoveSupport()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitListWindow, (UnityEngine.Object) null) || !this.mUnitListWindow.IsEnabled())
        return;
      this.SetSupport((SupportData) null);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
      {
        this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
        this.FriendSlot.SetSlotData<UnitData>((UnitData) null);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendCardSlot, (UnityEngine.Object) null))
        {
          DataSource.Bind<UnitData>(((Component) this.FriendCardSlot).gameObject, (UnitData) null);
          this.FriendCardSlot.SetSlotData<ConceptCardData>((ConceptCardData) null);
          ((Component) this.FriendCardSlot).GetComponent<ConceptCardIcon>().Setup((ConceptCardData) null);
        }
      }
      this.OnPartyMemberChange();
      this.SaveTeamPresets();
      ButtonEvent.Invoke("UNITLIST_BTN_CLOSE", (object) null);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 220);
    }

    private void CardList_Show()
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this.UnitSlots[this.mSelectedSlotIndex]).gameObject, (UnitData) null);
      if (dataOfClass == null)
        return;
      GameObject gameObject = AssetManager.Load<GameObject>(this.CARDLIST_WINDOW_PATH);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      this.mConceptCardSelector = UnityEngine.Object.Instantiate<GameObject>(gameObject);
      ConceptCardEquipWindow component = this.mConceptCardSelector.GetComponent<ConceptCardEquipWindow>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.Init(dataOfClass, 0);
      component.OnChangeAction = (Action) (() => this.mConceptCardSelector.AddComponent<DestroyEventListener>().Listeners += (DestroyEventListener.DestroyEvent) (go => this.ForceRefreshCardData()));
    }

    private void ForceRefreshCardData()
    {
      if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.MP)
      {
        for (int index = 0; index < this.CurrentParty.Units.Length; ++index)
        {
          UnitData unit = PartyUtility.FindUnit(this.CurrentParty.Units[index], this.mOwnUnits);
          this.UnitSlots[index].SetSlotData<UnitData>(unit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardSlots[index], (UnityEngine.Object) null))
          {
            DataSource.Bind<UnitData>(((Component) this.CardSlots[index]).gameObject, unit);
            this.CardSlots[index].SetSlotData<ConceptCardData>(unit?.MainConceptCard);
            ((Component) this.CardSlots[index]).GetComponent<ConceptCardIcon>().Setup(unit?.MainConceptCard);
          }
        }
      }
      else
        this.Refresh();
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this.UnitSlots[this.mSelectedSlotIndex]).gameObject, (UnitData) null);
      if (dataOfClass != null && dataOfClass.MainConceptCard != null)
        this.OnSlotChange(((Component) this.CardSlots[this.mSelectedSlotIndex]).gameObject);
      this.OnPartyMemberChange();
    }

    public void SetIsShowDownloadPopup(bool isShowDownloadPopup)
    {
      this.mIsShowDownloadPopup = isShowDownloadPopup;
    }

    private void UpdateCurrentForwardButton()
    {
      if (this.mCurrentQuest == null)
      {
        this.CurrentForwardButton = this.ForwardButton;
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ForwardButton, (UnityEngine.Object) null))
          ((Component) this.ForwardButton).gameObject.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChallangeCountResetButton, (UnityEngine.Object) null))
          ((Component) this.ChallangeCountResetButton).gameObject.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LimitForwardButton, (UnityEngine.Object) null))
          ((Component) this.LimitForwardButton).gameObject.SetActive(false);
        if (this.mCurrentQuest.type == QuestTypes.StoryExtra && MonoSingleton<GameManager>.Instance.Player.StoryExChallengeCount.IsRestChallengeCount_Zero())
        {
          this.CurrentForwardButton = this.LimitForwardButton;
          if (!MonoSingleton<GameManager>.Instance.Player.StoryExChallengeCount.IsRestResetCount_Zero())
          {
            this.CurrentForwardButton = this.ChallangeCountResetButton;
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChallangeCountResetButtonText, (UnityEngine.Object) null))
              this.ChallangeCountResetButtonText.text = LocalizedText.Get("sys.QUEST_CHALLENGE_RESET_BUTTON_TEXT_STORYEX");
          }
        }
        else if (!this.mCurrentQuest.CheckEnableChallange() && this.mCurrentQuest.CheckEnableReset())
        {
          this.CurrentForwardButton = this.ChallangeCountResetButton;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChallangeCountResetButtonText, (UnityEngine.Object) null))
            this.ChallangeCountResetButtonText.text = LocalizedText.Get("sys.QUEST_CHALLENGE_RESET_BUTTON_TEXT_NORMAL");
        }
        else
          this.CurrentForwardButton = !this.mCurrentQuest.CheckEnableChallange() ? this.LimitForwardButton : this.ForwardButton;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CurrentForwardButton, (UnityEngine.Object) null))
          return;
        ((Component) this.CurrentForwardButton).gameObject.SetActive(this.ShowForwardButton);
      }
    }

    private void Refresh_StoryExChallengeCount()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.StoryExChallengeCount, (UnityEngine.Object) null))
        return;
      bool active = false;
      if (this.mCurrentQuest != null)
        active = this.mCurrentQuest.type == QuestTypes.StoryExtra;
      GameUtility.SetGameObjectActive(this.StoryExChallengeCount, active);
      GameParameter.UpdateAll(this.StoryExChallengeCount);
    }

    public static bool IsAutoRepeatQuestCheck
    {
      get => UnityEngine.Object.op_Inequality((UnityEngine.Object) AutoRepeatQuestStart.Instance, (UnityEngine.Object) null);
    }

    private void RefreshAutoRepeatQuestButton()
    {
      GameUtility.SetGameObjectActive((Component) this.AutoRepeatQuestBtn, false);
      GameUtility.SetGameObjectActive((Component) this.AutoRepeatQuestBestTimeBtn, false);
      SRPG_Button srpgButton1 = this.AutoRepeatQuestBtn;
      SRPG_Button srpgButton2 = this.AutoRepeatQuestBtnMask;
      if (this.mCurrentQuest != null && this.mCurrentQuest.best_clear_time > 0)
      {
        srpgButton1 = this.AutoRepeatQuestBestTimeBtn;
        srpgButton2 = this.AutoRepeatQuestBestTimeBtnMask;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AutoRepeatQuestBestTimeText, (UnityEngine.Object) null))
          this.AutoRepeatQuestBestTimeText.text = this.mCurrentQuest.GetClearBestTime();
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) srpgButton1, (UnityEngine.Object) null))
        return;
      bool flag1 = this.mCurrentQuest != null && this.mCurrentQuest.IsAutoRepeatQuestFlag;
      bool flag2 = this.mCurrentQuest != null && this.mCurrentQuest.CheckEnableChallange() && MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.AutoRepeatQuest);
      if (flag2 && this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.StoryExtra)
        flag2 = !MonoSingleton<GameManager>.Instance.Player.StoryExChallengeCount.IsRestChallengeCount_Zero();
      if (MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress != null)
        flag2 = flag2 && MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.State == AutoRepeatQuestData.eState.IDLE;
      ((Component) srpgButton1).gameObject.SetActive(flag1);
      ((Selectable) srpgButton1).interactable = flag2;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) srpgButton2, (UnityEngine.Object) null))
        return;
      ((Component) srpgButton2).gameObject.SetActive(!flag2);
    }

    private void OnAutoRepeatQuestMask(SRPG_Button button)
    {
      if (this.mCurrentQuest == null)
        DebugUtility.LogError("クエストが設定されていません.");
      else if (MonoSingleton<GameManager>.Instance.Player != null && MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress != null && MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.IsExistRecord)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
      }
      else
      {
        string title = LocalizedText.Get("sys.AUTO_REPEAT_QUEST_TITLE");
        StringBuilder stringBuilder = GameUtility.GetStringBuilder();
        if (!this.mCurrentQuest.CheckEnableChallange() || this.mCurrentQuest.type == QuestTypes.StoryExtra && MonoSingleton<GameManager>.Instance.Player.StoryExChallengeCount.IsRestChallengeCount_Zero())
        {
          if (string.IsNullOrEmpty(stringBuilder.ToString()))
            stringBuilder.Append(LocalizedText.Get("sys.AUTO_REPEAT_QUEST_CAUTION_FAILED_START"));
          stringBuilder.Append(LocalizedText.Get("sys.AUTO_REPEAT_QUEST_CAUTION_FAILED_CHALLENGE_LIMIT"));
        }
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(stringBuilder.ToString()))
          return;
        stringBuilder.Append(LocalizedText.Get("sys.AUTO_REPEAT_QUEST_CAUTION_FAILED_END"));
        UIUtility.SystemMessage(title, stringBuilder.ToString(), (UIUtility.DialogResultEvent) null);
      }
    }

    private void ForceSavePartyData()
    {
      if (this.IsSameUnitInParty(this.CurrentParty.Units))
        return;
      if (this.IsPartyDirty)
        this.SaveParty((PartyWindow2.Callback) (() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 310)), (PartyWindow2.Callback) (() => UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ILLEGAL_PARTY"), (UIUtility.DialogResultEvent) null)));
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 310);
    }

    public void SetForcedConceptCardUnit(UnitData[] units)
    {
      for (int index = 0; index < units.Length; ++index)
      {
        this.UnitSlots[index].SetSlotData<UnitData>(units[index]);
        DataSource.Bind<UnitData>(((Component) this.CardSlots[index]).gameObject, units[index]);
        this.CardSlots[index].SetSlotData<ConceptCardData>(units[index] != null ? units[index].MainConceptCard : (ConceptCardData) null);
        ((Component) this.CardSlots[index]).GetComponent<ConceptCardIcon>().Setup(units[index] != null ? units[index].MainConceptCard : (ConceptCardData) null);
      }
      this.OnPartyMemberChange();
    }

    private void RefreshSameUnitStates()
    {
      List<UnitData> unitDataList = new List<UnitData>((IEnumerable<UnitData>) this.CurrentParty.Units);
      if (this.mCurrentQuest != null && this.mCurrentQuest.IsMulti)
        unitDataList = this.GetMultiActiveUnitList();
      unitDataList.AddRange((IEnumerable<UnitData>) this.mGuestUnit);
      for (int index = 0; index < this.UnitSlots.Length; ++index)
      {
        bool active = false;
        if (this.CurrentParty.Units.Length <= index)
          break;
        if (this.CurrentParty.Units[index] != null)
          active = UnitSameGroupParam.IsSameUnitInParty(unitDataList.ToArray(), this.CurrentParty.Units[index].UnitID);
        if (index >= unitDataList.Count)
          active = false;
        UnitSlot component = ((Component) this.UnitSlots[index]).GetComponent<UnitSlot>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.SetSameUnitIcon(active);
      }
    }

    private bool IsSameUnitInParty(UnitData[] list)
    {
      if (list == null)
        return false;
      string[] forceUnits = this.GetForceUnits();
      List<UnitSameGroupParam> unitSameGroupParamList = UnitSameGroupParam.IsSameUnitInParty(list);
      if (forceUnits != null)
      {
        for (int index = 0; index < forceUnits.Length; ++index)
        {
          if (!string.IsNullOrEmpty(forceUnits[index]) && UnitSameGroupParam.IsSameUnitInParty(list, forceUnits[index]))
          {
            UnitSameGroupParam unitSameGroup = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitSameGroup(forceUnits[index]);
            if (unitSameGroup != null)
            {
              if (unitSameGroupParamList == null)
                unitSameGroupParamList = new List<UnitSameGroupParam>();
              if (!unitSameGroupParamList.Contains(unitSameGroup))
                unitSameGroupParamList.Add(unitSameGroup);
            }
          }
        }
      }
      if (unitSameGroupParamList == null || unitSameGroupParamList.Count <= 0)
        return false;
      string empty = string.Empty;
      for (int index = 0; index < unitSameGroupParamList.Count; ++index)
      {
        if (unitSameGroupParamList[index] != null)
        {
          if (index != 0)
            empty += LocalizedText.Get("sys.PARTYEDITOR_SAMEUNIT_PLUS");
          empty += unitSameGroupParamList[index].GetGroupUnitAllNameText();
        }
      }
      if (!string.IsNullOrEmpty(empty))
        UIUtility.NegativeSystemMessage((string) null, string.Format(LocalizedText.Get("sys.PARTY_SAMEUNIT_INPARTY"), (object) empty), (UIUtility.DialogResultEvent) (dialog => { }));
      return true;
    }

    private string[] GetForceUnits()
    {
      return this.mCurrentQuest.questParty == null ? this.mCurrentQuest.units.GetList() : ((IEnumerable<PartySlotTypeUnitPair>) this.mCurrentQuest.questParty.GetMainSubSlots()).Where<PartySlotTypeUnitPair>((Func<PartySlotTypeUnitPair, bool>) (slot => slot.Type == SRPG.PartySlotType.ForcedHero || slot.Type == SRPG.PartySlotType.Forced)).Select<PartySlotTypeUnitPair, string>((Func<PartySlotTypeUnitPair, string>) (slot => slot.Unit)).ToArray<string>();
    }

    private List<UnitData> GetMultiActiveUnitList()
    {
      List<UnitData> multiActiveUnitList = new List<UnitData>();
      for (int index = 0; index < this.UnitSlots.Length; ++index)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null))
        {
          UnitSlot component = ((Component) this.UnitSlots[index]).GetComponent<UnitSlot>();
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null) && !component.IsOverlayImage)
          {
            UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this.UnitSlots[index]).gameObject, (UnitData) null);
            if (dataOfClass != null)
              multiActiveUnitList.Add(dataOfClass);
          }
        }
      }
      return multiActiveUnitList;
    }

    public enum EditPartyTypes
    {
      Auto = 0,
      Normal = 1,
      Event = 2,
      MP = 3,
      Arena = 4,
      ArenaDef = 5,
      Character = 6,
      Tower = 7,
      Versus = 8,
      MultiTower = 9,
      Ordeal = 10, // 0x0000000A
      RankMatch = 11, // 0x0000000B
      Raid = 12, // 0x0000000C
      GuildRaid = 13, // 0x0000000D
      StoryExtra = 14, // 0x0000000E
      GvG = 16, // 0x00000010
      WorldRaid = 17, // 0x00000011
      Max = 18, // 0x00000012
    }

    public enum PartySlotType
    {
      Main0,
      Main1,
      Sub0,
    }

    protected delegate void Callback();

    private enum ItemFilterTypes
    {
      All,
      Offense,
      Support,
    }

    public class JSON_ReqBtlComRaidResponse
    {
      public Json_PlayerData player;
      public Json_Item[] items;
      public Json_Unit[] units;
      public Json_BtlRewardConceptCard[] cards;
      public BattleCore.Json_BtlInfo[] btlinfos;
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;
      public int guildraid_bp_charge;
      public string[] bgms;
      public int rune_storage_used;
      public JSON_StoryExChallengeCount story_ex_challenge;
      public Json_RuneData[] runes_detail;
      public JSON_TrophyProgress[] guild_trophies;
      public Json_Artifact[] artifacts;
      public PartyWindow2.JSON_ReqBtlComRaidResponse.BossReward[] boss_rewards;

      public class BossReward
      {
        public PartyWindow2.JSON_ReqBtlComRaidResponse.BossReward.Reward[] rewards;

        public class Reward
        {
          public int itype;
          public string iname;
          public int num;
        }
      }
    }

    public class JSON_ReqBtlComResetResponse
    {
      public Json_PlayerData player;
      public JSON_QuestProgress[] quests;
      public Json_Item[] items;
    }

    [MessagePackObject(true)]
    public class MP_Response_SetConceptCardList : WebAPI.JSON_BaseResponse
    {
      public ReqSetConceptCardList.Response body;
    }

    private class HoldObserver : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
    {
      private float[] HoldSpan = new float[5]
      {
        0.55f,
        0.25f,
        0.25f,
        0.1f,
        0.05f
      };
      private float HoldDuration;
      private bool Holding;
      private int ActionCount;
      private Vector2 mDragStartPos;
      public PartyWindow2.HoldObserver.DelegateOnHoldEvent OnHoldStart;
      public PartyWindow2.HoldObserver.DelegateOnHoldEvent OnHoldEnd;
      public PartyWindow2.HoldObserver.DelegateOnHoldEvent OnHoldUpdate;

      public void OnPointerDown(PointerEventData eventData)
      {
        if (this.OnHoldStart == null)
          return;
        this.OnHoldStart();
        this.Holding = true;
        this.mDragStartPos = eventData.position;
      }

      public void OnPointerUp()
      {
        if (this.OnHoldEnd != null)
          this.OnHoldEnd();
        this.StatusReset();
      }

      public void StatusReset()
      {
        this.Holding = false;
        this.ActionCount = 0;
        this.HoldDuration = 0.0f;
        ((Vector2) ref this.mDragStartPos).Set(0.0f, 0.0f);
      }

      public void Update()
      {
        if (this.OnHoldUpdate == null)
          return;
        float unscaledDeltaTime = Time.unscaledDeltaTime;
        if (this.Holding && !Input.GetMouseButton(0))
        {
          this.OnPointerUp();
        }
        else
        {
          GameSettings instance = GameSettings.Instance;
          float num = (float) (instance.HoldMargin * instance.HoldMargin);
          Vector2 vector2 = Vector2.op_Subtraction(this.mDragStartPos, Vector2.op_Implicit(Input.mousePosition));
          bool flag = (double) ((Vector2) ref vector2).sqrMagnitude > (double) num;
          if ((double) this.HoldDuration < (double) this.HoldSpan[this.ActionCount] && this.ActionCount < 1 && flag)
          {
            this.StatusReset();
          }
          else
          {
            if (!this.Holding)
              return;
            this.HoldDuration += unscaledDeltaTime;
            if ((double) this.HoldDuration < (double) this.HoldSpan[this.ActionCount])
              return;
            this.HoldDuration -= this.HoldSpan[this.ActionCount];
            this.OnHoldUpdate();
            if (this.ActionCount >= this.HoldSpan.Length - 1)
              return;
            ++this.ActionCount;
          }
        }
      }

      public void OnDestroy()
      {
        this.StatusReset();
        this.OnHoldStart = (PartyWindow2.HoldObserver.DelegateOnHoldEvent) null;
        this.OnHoldEnd = (PartyWindow2.HoldObserver.DelegateOnHoldEvent) null;
      }

      public delegate void DelegateOnHoldEvent();
    }
  }
}
