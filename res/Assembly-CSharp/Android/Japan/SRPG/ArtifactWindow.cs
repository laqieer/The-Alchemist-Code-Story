// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(5, "Refresh", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(6, "Refresh Exp Items", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(11, "Show Selection", FlowNode.PinTypes.Input, 7)]
  [FlowNode.Pin(10, "Flush Requests", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "Finalize", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "Finalize Begin", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "Finalize End", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(151, "AddExp Begin", FlowNode.PinTypes.Output, 151)]
  [FlowNode.Pin(152, "AddExp End", FlowNode.PinTypes.Output, 152)]
  [FlowNode.Pin(200, "Rarity Up", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(201, "Rarity Up End", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "Rarity Up Dialog Close", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(300, "Transmute", FlowNode.PinTypes.Input, 300)]
  [FlowNode.Pin(301, "Transmute Begin", FlowNode.PinTypes.Output, 301)]
  [FlowNode.Pin(302, "Transmute End", FlowNode.PinTypes.Output, 302)]
  [FlowNode.Pin(400, "Decompose", FlowNode.PinTypes.Input, 400)]
  [FlowNode.Pin(402, "Decompose Begin", FlowNode.PinTypes.Output, 401)]
  [FlowNode.Pin(401, "Decompose End", FlowNode.PinTypes.Output, 402)]
  [FlowNode.Pin(500, "Equip", FlowNode.PinTypes.Output, 500)]
  [FlowNode.Pin(600, "Sell", FlowNode.PinTypes.Input, 600)]
  [FlowNode.Pin(601, "Sell Begin", FlowNode.PinTypes.Output, 601)]
  [FlowNode.Pin(602, "Sell End", FlowNode.PinTypes.Output, 602)]
  [FlowNode.Pin(650, "InspSkill DefaultTab", FlowNode.PinTypes.Input, 650)]
  [FlowNode.Pin(651, "InspSkill Begin", FlowNode.PinTypes.Output, 651)]
  [FlowNode.Pin(652, "InspSkill End", FlowNode.PinTypes.Output, 652)]
  [FlowNode.Pin(653, "InspSkill ResetTab", FlowNode.PinTypes.Input, 653)]
  [FlowNode.Pin(654, "InspSkill Refresh", FlowNode.PinTypes.Input, 654)]
  [FlowNode.Pin(660, "InspSkill 強化選択", FlowNode.PinTypes.Output, 660)]
  [FlowNode.Pin(661, "InspSkill 装備選択", FlowNode.PinTypes.Output, 661)]
  [FlowNode.Pin(662, "InspSkill リセット選択", FlowNode.PinTypes.Output, 662)]
  [FlowNode.Pin(663, "InspSkill スロット開放選択", FlowNode.PinTypes.Output, 663)]
  [FlowNode.Pin(664, "InspSkill 強化素材長押し", FlowNode.PinTypes.Output, 664)]
  [FlowNode.Pin(665, "InspSkill コスト不足(GOLD)", FlowNode.PinTypes.Output, 665)]
  [FlowNode.Pin(666, "InspSkill コスト不足(COIN)", FlowNode.PinTypes.Output, 666)]
  [FlowNode.Pin(667, "InspSkill コスト不足(COIN_P)", FlowNode.PinTypes.Output, 667)]
  [FlowNode.Pin(670, "InspSkill Enhance", FlowNode.PinTypes.Input, 670)]
  [FlowNode.Pin(671, "InspSkill EnhanceConfirm", FlowNode.PinTypes.Input, 671)]
  [FlowNode.Pin(672, "InspSkill EnhanceConfirm", FlowNode.PinTypes.Output, 672)]
  [FlowNode.Pin(673, "InspSkill 合成成功演出再生開始", FlowNode.PinTypes.Input, 673)]
  [FlowNode.Pin(674, "InspSkill 合成成功演出再生開始", FlowNode.PinTypes.Output, 674)]
  [FlowNode.Pin(675, "InspSkill EnhanceEnd", FlowNode.PinTypes.Input, 675)]
  [FlowNode.Pin(676, "InspSkill EnhanceEnd", FlowNode.PinTypes.Output, 676)]
  [FlowNode.Pin(677, "InspSkill 強化素材リセット", FlowNode.PinTypes.Input, 677)]
  [FlowNode.Pin(678, "InspSkill 強化素材リセット", FlowNode.PinTypes.Output, 678)]
  [FlowNode.Pin(680, "InspSkill EquipConfirm", FlowNode.PinTypes.Input, 680)]
  [FlowNode.Pin(681, "InspSkill EquipConfirm", FlowNode.PinTypes.Output, 681)]
  [FlowNode.Pin(690, "InspSkill ResetConfirm", FlowNode.PinTypes.Input, 690)]
  [FlowNode.Pin(691, "InspSkill ResetConfirm", FlowNode.PinTypes.Output, 691)]
  [FlowNode.Pin(700, "Reset Sending Request", FlowNode.PinTypes.Input, 700)]
  public class ArtifactWindow : MonoBehaviour, IFlowInterface
  {
    private static readonly string ARTIFACT_EXPMAX_UI_PATH = "UI/ArtifactLevelUpWindow";
    private static readonly string ARTIFACT_RARITY_CHECK_UI_PATH = "UI/ArtifactRarityCheck";
    public float AutoFlushRequests = 1f;
    public int KyokaPanel_LvCapped = 1;
    public int KyokaPanel_LvMax = 2;
    public int RarityUpPanel_MaxRarity = 1;
    public int RarityUpPanel_NoItem = 2;
    public int RarityUpCost_NoGold = -1;
    public int TransmutePanel_Disabled = 1;
    public int TransmuteCost_NoGold = -1;
    public int AbilityListItem_Locked = 1;
    public int AbilityListItem_Unlocked = 2;
    public int DecomposePanel_Disabled = 1;
    public int DecomposeWarningRarity = 2;
    public int DecomposeCost_NoGold = -1;
    private List<GameObject> mAbilityItems = new List<GameObject>(4);
    private List<GameObject> mKyokaItems = new List<GameObject>(8);
    private List<GameObject> mRarityUpItems = new List<GameObject>(3);
    private List<ArtifactWindow.Request> mRequests = new List<ArtifactWindow.Request>();
    private List<ArtifactIcon> mSelectionItems = new List<ArtifactIcon>(10);
    private List<ItemData> mTmpItems = new List<ItemData>();
    private List<ArtifactData> m_SelectedArtifactList = new List<ArtifactData>();
    private List<GameObject> m_InspSkillItems = new List<GameObject>();
    private List<GameObject> m_InspSkillEnhanceItems = new List<GameObject>();
    private List<GameObject> m_InspSkillEnhanceConfirmItems = new List<GameObject>();
    private const int PIN_OT_SELECT_INSPSKILL_ENHANCE = 660;
    private const int PIN_OT_SELECT_INSPSKILL_EQUIP = 661;
    private const int PIN_OT_SELECT_INSPSKILL_RESET = 662;
    private const int PIN_OT_SELECT_INSPSKILL_UNLOCK = 663;
    private const int PIN_OT_SELECT_INSPSKILL_DETAIL = 664;
    private const int PIN_OT_COSTSHORT_GOLD = 665;
    private const int PIN_OT_COSTSHORT_COIN = 666;
    private const int PIN_OT_COSTSHORT_COIN_P = 667;
    private const int PIN_OT_CONFIRM_INSPSKILL_ENHANCE = 672;
    public ArtifactList ArtifactList;
    [StringIsGameObjectID]
    public string ArtifactListID;
    public GenericSlot ArtifactSlot;
    public SRPG_Button artiProssessButton;
    public GameObject WindowBody;
    public GameObject WindowBodyAdd;
    public bool RefreshOnStart;
    [FourCC]
    public int ArtifactDataID;
    [FourCC]
    public int ItemDataID;
    [FourCC]
    public int ChangedAbilityID;
    [FourCC]
    public int NewAbilityID;
    [Space(16f)]
    public GameObject DetailWindow;
    public MaterialPanel DetailMaterial;
    [Space(16f)]
    public GameObject KyokaPanel;
    public string KyokaPanelState;
    public int KyokaPanel_Enable;
    public GameObject KyokaList;
    public GameObject KyokaListItem;
    public string KyokaSE;
    public SRPG_Button ExpMaxLvupBtn;
    [Space(16f)]
    public StatusList Status;
    public StatusList StatusAdd;
    public ExpPanel ExpPanel;
    public ExpPanel ExpPanelAdd;
    [Space(16f)]
    public GameObject RarityUpPanel;
    public string RarityUpPanelState;
    public int RarityUpPanel_Normal;
    public Text RarityUpCost;
    public string RarityUpCostState;
    public int RarityUpCost_Normal;
    public Text RarityUpCondition;
    public MaterialPanel RarityUpMaterial;
    public GameObject RarityUpResult;
    public GameObject RarityUpList;
    public GameObject RarityUpListItem;
    public GameObject RarityUpIconRoot;
    public GameObject RarityUpHilit;
    public Button RarityUpButton;
    [Space(16f)]
    public GameObject TransmutePanel;
    public string TransmutePanelState;
    public int TransmutePanel_Normal;
    public Text TransmuteCost;
    public string TransmuteCostState;
    public int TransmuteCost_Normal;
    public Text TransmuteCondition;
    public MaterialPanel TransmuteMaterial;
    public GameObject TransmuteResult;
    [Space(16f)]
    public GenericSlot OwnerSlot;
    [Space(16f)]
    public GameObject AbilityList;
    public GameObject AbilityListItem;
    public GameObject AbilityListItemAdd;
    public string AbilityListItemState;
    public int AbilityListItem_Hidden;
    [Space(16f)]
    public GameObject EquipInspSkill;
    public GameObject EquipInspSkillItem;
    public GameObject EquipInspSkillItemAdd;
    public Toggle EquipInspSkillTab;
    public Toggle WeaponAbilityTab;
    public Toggle EquipInspSkillTabAdd;
    public Toggle WeaponAbilityTabAdd;
    [Space(16f)]
    public GameObject SellResult;
    public Text SellPrice;
    public string SellSE;
    [Space(16f)]
    public GameObject DecomposePanel;
    public string DecomposePanelState;
    public int DecomposePanel_Normal;
    public MaterialPanel DecomposeItem;
    public GameObject DecomposeResult;
    public Text DecomposeHelp;
    public Text DecomposeCost;
    public Text DecomposeCostTotal;
    public Text DecomposeKakeraNumOld;
    public Text DecomposeKakeraNumNew;
    public GameObject DecomposeWarning;
    public string DecomposeCostState;
    public int DecomposeCost_Normal;
    public string DecomposeSE;
    [Space(16f)]
    public GenericSlot ArtifactOwnerSlot;
    public ArtifactWindow.EquipEvent OnEquip;
    public ArtifactWindow.EquipEvent OnDestroyCallBack;
    [Space(16f)]
    public Text SelectionNum;
    public ArtifactIcon SelectionListItem;
    public GameObject SelectionList;
    [Space(16f)]
    public Toggle ProcessToggle_Enhance;
    public Toggle ProcessToggle_Evolution;
    public Toggle ProcessToggle_Detail;
    [Space(16f)]
    public Toggle ToggleExcludeEquiped;
    public int RarityCheckValue;
    [HeaderBar("▼セット効果確認用のボタン")]
    [SerializeField]
    private Button m_SetEffectsButton;
    [SerializeField]
    private Button m_SetEffectsButtonAdd;
    private List<ChangeListData> mChangeSet;
    private ArtifactData mCurrentArtifact;
    private ArtifactParam mCurrentArtifactParam;
    private object[] mSelectedArtifacts;
    private List<GameObject> mDecomposeItems;
    private bool mFinalizing;
    private bool mSendingRequests;
    private bool mSceneChanging;
    private long mTotalSellPrice;
    private UnitData mOwnerUnitData;
    private int mOwnerUnitJobIndex;
    private GameObject mDetailWindow;
    private GameObject mResultWindow;
    private float mFlushTimer;
    private bool mBusy;
    private bool mDisableFlushRequest;
    private int mUseEnhanceItemNum;
    private UnitData mCurrentArtifactOwner;
    private JobData mCurrentArtifactOwnerJob;
    private ArtifactData[] mCachedArtifacts;
    private GameObject mConfirmDialogGo;
    private ArtifactWindow.StatusCache mStatusCache;
    private ArtifactTypes mSelectArtifactSlot;
    [Space(16f)]
    public GameObject m_InspSkillDefaultPanel;
    public GameObject m_InspSkillEnhancePanel;
    public Transform m_InspSkillList;
    public GameObject m_InspSkillListItem;
    public Transform m_InspSkillEnhanceList;
    public GameObject m_InspSkillEnhanceListItem;
    public GameObject m_InspSkillEnhanceBeforePanel;
    public GameObject m_InspSkillEnhanceAfterPanel;
    public Text m_InspSkillEnhanceCost;
    public GameObject m_InspSkillEnhanceStatus;
    public GameObject m_InspSkillEnhanceStatusParam;
    public GameObject m_InspSkillEnhanceStatusSkill;
    public Button m_InspSkillEnhanceConfirmButton;
    public GameObject m_InspSkillEnhanceListNotItem;
    public GameObject m_InspSkillEnhanceSuccessEffect;
    public GameObject m_InspSkillEnhanceDetail;
    public GameObject m_InspSkillEnhanceConfirmList;
    public GameObject m_InspSkillEnhanceConfirmListItem;
    public GameObject m_InspSkillEquipConfirm;
    public GameObject m_InspSkillResetConfirm;
    public GameObject m_InspSkillResetBefore;
    public GameObject m_InspSkillResetAfter;
    public Text m_InspSkillResetTitle;
    public Text m_InspSkillResetText;
    public GameObject m_InspSkillEnhanceDetailList;
    public GameObject m_InspSkillEnhanceDetailListItem;
    public Text m_InspSkillSlotUnlockConfirmText;
    private InspirationSkillData m_CurrentInspSkill;
    private bool m_InspSkillIsResetMode;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 670:
          this.RefreshInspSkillEnhance();
          break;
        case 671:
          this.OnConfirmInspSkillEnhance();
          break;
        case 673:
          this.OnSuccessInspSkillEnhance();
          break;
        case 675:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 676);
          break;
        case 677:
          this.OnResetEnhanceListItem();
          break;
        default:
          switch (pinID - 5)
          {
            case 0:
              this.RefreshWindow();
              return;
            case 1:
              this.RefreshKyokaList();
              return;
            case 5:
              this.FlushRequests(false);
              return;
            case 6:
              this.ShowSelection();
              return;
            default:
              switch (pinID - 650)
              {
                case 0:
                  this.m_InspSkillIsResetMode = false;
                  this.RefreshInspSkill(this.m_InspSkillIsResetMode);
                  return;
                case 3:
                  this.m_InspSkillIsResetMode = true;
                  this.RefreshInspSkill(this.m_InspSkillIsResetMode);
                  return;
                case 4:
                  this.RefreshInspSkill(this.m_InspSkillIsResetMode);
                  return;
                default:
                  if (pinID != 100)
                  {
                    if (pinID != 200)
                    {
                      if (pinID != 300)
                      {
                        if (pinID != 400)
                        {
                          if (pinID != 600)
                          {
                            if (pinID != 700)
                              return;
                            this.mSendingRequests = false;
                            return;
                          }
                          this.SellArtifacts();
                          return;
                        }
                        this.DecomposeArtifact();
                        return;
                      }
                      this.TransmuteArtifact();
                      return;
                    }
                    this.AddRarity();
                    return;
                  }
                  this.mDisableFlushRequest = false;
                  this.mSendingRequests = true;
                  this.mFinalizing = true;
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
                  return;
              }
          }
      }
    }

    public List<ItemData> TmpItems
    {
      get
      {
        return this.mTmpItems;
      }
    }

    public ArtifactTypes SelectArtifactSlot
    {
      get
      {
        return this.mSelectArtifactSlot;
      }
      set
      {
        this.mSelectArtifactSlot = value;
      }
    }

    private void Start()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) instanceDirect != (UnityEngine.Object) null)
        instanceDirect.OnSceneChange += new GameManager.SceneChangeEvent(this.OnSceneCHange);
      if ((UnityEngine.Object) this.ExpPanel != (UnityEngine.Object) null)
      {
        ExpPanel expPanel = this.ExpPanel;
        // ISSUE: reference to a compiler-generated field
        if (ArtifactWindow.\u003C\u003Ef__mg\u0024cache0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ArtifactWindow.\u003C\u003Ef__mg\u0024cache0 = new ExpPanel.CalcEvent(ArtifactData.StaticCalcExpFromLevel);
        }
        // ISSUE: reference to a compiler-generated field
        ExpPanel.CalcEvent fMgCache0 = ArtifactWindow.\u003C\u003Ef__mg\u0024cache0;
        // ISSUE: reference to a compiler-generated field
        if (ArtifactWindow.\u003C\u003Ef__mg\u0024cache1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ArtifactWindow.\u003C\u003Ef__mg\u0024cache1 = new ExpPanel.CalcEvent(ArtifactData.StaticCalcLevelFromExp);
        }
        // ISSUE: reference to a compiler-generated field
        ExpPanel.CalcEvent fMgCache1 = ArtifactWindow.\u003C\u003Ef__mg\u0024cache1;
        expPanel.SetDelegate(fMgCache0, fMgCache1);
        this.ExpPanel.OnLevelChange = new ExpPanel.LevelChangeEvent(this.OnLevelChange);
        this.ExpPanel.OnFinish = new ExpPanel.ExpPanelEvent(this.OnKyokaEnd);
      }
      if ((UnityEngine.Object) this.ExpPanelAdd != (UnityEngine.Object) null)
      {
        ExpPanel expPanelAdd = this.ExpPanelAdd;
        // ISSUE: reference to a compiler-generated field
        if (ArtifactWindow.\u003C\u003Ef__mg\u0024cache2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ArtifactWindow.\u003C\u003Ef__mg\u0024cache2 = new ExpPanel.CalcEvent(ArtifactData.StaticCalcExpFromLevel);
        }
        // ISSUE: reference to a compiler-generated field
        ExpPanel.CalcEvent fMgCache2 = ArtifactWindow.\u003C\u003Ef__mg\u0024cache2;
        // ISSUE: reference to a compiler-generated field
        if (ArtifactWindow.\u003C\u003Ef__mg\u0024cache3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ArtifactWindow.\u003C\u003Ef__mg\u0024cache3 = new ExpPanel.CalcEvent(ArtifactData.StaticCalcLevelFromExp);
        }
        // ISSUE: reference to a compiler-generated field
        ExpPanel.CalcEvent fMgCache3 = ArtifactWindow.\u003C\u003Ef__mg\u0024cache3;
        expPanelAdd.SetDelegate(fMgCache2, fMgCache3);
        this.ExpPanelAdd.OnLevelChange = new ExpPanel.LevelChangeEvent(this.OnLevelChange);
        this.ExpPanelAdd.OnFinish = new ExpPanel.ExpPanelEvent(this.OnKyokaEnd);
      }
      if ((UnityEngine.Object) this.ExpMaxLvupBtn != (UnityEngine.Object) null)
        this.ExpMaxLvupBtn.AddListener(new SRPG_Button.ButtonClickEvent(this.OnExpMaxOpen));
      if (!string.IsNullOrEmpty(this.ArtifactListID))
        this.ArtifactList = GameObjectID.FindGameObject<ArtifactList>(this.ArtifactListID);
      if ((UnityEngine.Object) this.ArtifactList != (UnityEngine.Object) null)
        this.ArtifactList.OnSelectionChange += new ArtifactList.SelectionChangeEvent(this.OnArtifactSelect);
      if ((UnityEngine.Object) this.KyokaListItem != (UnityEngine.Object) null && this.KyokaListItem.activeInHierarchy)
        this.KyokaListItem.SetActive(false);
      if ((UnityEngine.Object) this.SelectionListItem != (UnityEngine.Object) null && this.SelectionListItem.gameObject.activeSelf)
        this.SelectionListItem.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.ArtifactSlot != (UnityEngine.Object) null)
        this.ArtifactSlot.SetSlotData<ArtifactData>((ArtifactData) null);
      if ((UnityEngine.Object) this.artiProssessButton != (UnityEngine.Object) null)
        this.artiProssessButton.interactable = false;
      if ((UnityEngine.Object) this.ToggleExcludeEquiped != (UnityEngine.Object) null)
        this.ToggleExcludeEquiped.isOn = this.ReadExcludeEquipedSettingValue();
      if ((UnityEngine.Object) this.m_InspSkillListItem != (UnityEngine.Object) null)
        this.m_InspSkillListItem.SetActive(false);
      if ((UnityEngine.Object) this.m_InspSkillEnhanceListItem != (UnityEngine.Object) null)
        this.m_InspSkillEnhanceListItem.SetActive(false);
      if ((UnityEngine.Object) this.m_InspSkillEnhanceConfirmListItem != (UnityEngine.Object) null)
        this.m_InspSkillEnhanceConfirmListItem.SetActive(false);
      this.mCurrentArtifact = DataSource.FindDataOfClass<ArtifactData>(this.gameObject, (ArtifactData) null);
      if (this.mCurrentArtifact != null)
        this.mCurrentArtifactParam = this.mCurrentArtifact.ArtifactParam;
      this.Rebind();
      if (!this.RefreshOnStart)
        return;
      this.RefreshWindow();
    }

    private void OnDestroy()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) instanceDirect != (UnityEngine.Object) null)
        instanceDirect.OnSceneChange -= new GameManager.SceneChangeEvent(this.OnSceneCHange);
      if ((UnityEngine.Object) this.mDetailWindow != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mDetailWindow.gameObject);
        this.mDetailWindow = (GameObject) null;
      }
      if ((UnityEngine.Object) this.mResultWindow != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mResultWindow);
        this.mResultWindow = (GameObject) null;
      }
      if ((UnityEngine.Object) this.ArtifactList != (UnityEngine.Object) null)
        this.ArtifactList.OnSelectionChange -= new ArtifactList.SelectionChangeEvent(this.OnArtifactSelect);
      if (this.OnDestroyCallBack == null)
        return;
      this.OnDestroyCallBack((ArtifactData) null, ArtifactTypes.None);
    }

    private bool IsConnectableNetwork()
    {
      switch (Application.internetReachability)
      {
        case NetworkReachability.NotReachable:
          return false;
        default:
          return true;
      }
    }

    private void FlushRequests(bool immediate)
    {
      if (immediate)
      {
        if (!this.IsConnectableNetwork())
          return;
        while (this.mRequests.Count > 0 && !Network.IsConnecting)
        {
          WebAPI api = this.mRequests[0].Compose();
          this.mRequests.RemoveAt(0);
          if (Network.Mode == Network.EConnectMode.Online)
            Network.RequestAPIImmediate(api, true);
        }
        this.RefreshArtifactList();
      }
      else
      {
        if (this.mRequests.Count <= 0)
          return;
        this.mSendingRequests = true;
      }
    }

    private void OnApplicationPause(bool pausing)
    {
      if (!pausing)
        return;
      this.FlushRequests(true);
    }

    private void OnApplicationFocus(bool focus)
    {
      if (focus)
        return;
      this.FlushRequests(true);
    }

    private bool IsRequestPending<T>()
    {
      return this.mRequests.Count > 0 && this.mRequests[0] is T;
    }

    private void Update()
    {
      if (this.mSendingRequests)
      {
        if (Network.IsConnecting)
          return;
        if (this.mRequests.Count > 0)
        {
          if (Network.Mode == Network.EConnectMode.Online)
          {
            Network.RequestAPI(this.mRequests[0].Compose(), false);
            this.mRequests.RemoveAt(0);
          }
          this.mSendingRequests = true;
          return;
        }
      }
      if (this.mFinalizing)
      {
        if (this.IsBusy)
          return;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
        this.mFinalizing = false;
        this.mSendingRequests = false;
      }
      else
      {
        if (this.mDisableFlushRequest || (double) this.mFlushTimer <= 0.0)
          return;
        this.mFlushTimer -= Time.unscaledDeltaTime;
        if ((double) this.mFlushTimer > 0.0)
          return;
        this.FlushRequests(false);
      }
    }

    public void RefreshKyokaList()
    {
      if ((UnityEngine.Object) this.KyokaList == (UnityEngine.Object) null || (UnityEngine.Object) this.KyokaListItem == (UnityEngine.Object) null || ((UnityEngine.Object) this.KyokaList == (UnityEngine.Object) this.KyokaListItem || this.KyokaList.transform.IsChildOf(this.KyokaListItem.transform)))
        return;
      List<ItemData> items = MonoSingleton<GameManager>.Instance.Player.Items;
      int index1 = 0;
      Transform transform = this.KyokaList.transform;
      for (int index2 = 0; index2 < items.Count; ++index2)
      {
        if (items[index2].ItemType == EItemType.ExpUpArtifact)
        {
          ItemData data = new ItemData();
          data.Setup(items[index2].UniqueID, items[index2].Param, items[index2].NumNonCap);
          GameObject root;
          if (this.mKyokaItems.Count <= index1)
          {
            root = UnityEngine.Object.Instantiate<GameObject>(this.KyokaListItem);
            if ((UnityEngine.Object) root != (UnityEngine.Object) null)
            {
              this.mKyokaItems.Add(root);
              root.transform.SetParent(transform, false);
              ListItemEvents component = root.GetComponent<ListItemEvents>();
              if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                component.OnSelect = new ListItemEvents.ListItemEvent(this.OnKyokaItemSelect);
            }
          }
          else
          {
            root = this.mKyokaItems[index1];
            data = this.mTmpItems[index1];
          }
          DataSource.Bind<ItemData>(root, data, false);
          root.SetActive(true);
          GameParameter.UpdateAll(root);
          if (!this.mTmpItems.Contains(data))
            this.mTmpItems.Add(data);
          ++index1;
        }
      }
      for (; index1 < this.mKyokaItems.Count; ++index1)
        this.mKyokaItems[index1].SetActive(false);
      this.mDisableFlushRequest = true;
    }

    private void OnKyokaItemSelect(GameObject go)
    {
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
      if (dataOfClass == null || dataOfClass.Num <= 0)
        return;
      this.RequestUseAddExpItem(dataOfClass, go);
    }

    private void OnArtifactBulkLevelUp(Dictionary<string, int> data)
    {
      List<ItemData> itemDataList = new List<ItemData>();
      foreach (KeyValuePair<string, int> keyValuePair in data)
      {
        string iname = keyValuePair.Key;
        int num = keyValuePair.Value;
        ItemData itemData = this.mTmpItems.Find((Predicate<ItemData>) (tmp => tmp.Param.iname == iname));
        if (itemData != null)
        {
          for (int index = 0; index < num; ++index)
            itemDataList.Add(itemData);
        }
      }
      if (itemDataList.Count <= 0)
        return;
      this.RequestUseAddExpItem((IEnumerable<ItemData>) itemDataList, (GameObject) null);
    }

    private void RequestUseAddExpItem(ItemData item, GameObject updataTarget)
    {
      this.RequestUseAddExpItem((IEnumerable<ItemData>) new List<ItemData>()
      {
        item
      }, updataTarget);
    }

    private void RequestUseAddExpItem(IEnumerable<ItemData> itemList, GameObject updataTarget)
    {
      if (this.IsBusy || this.mCurrentArtifact == null)
        return;
      if ((int) this.mCurrentArtifact.Lv >= this.mCurrentArtifact.GetLevelCap())
      {
        if (!((UnityEngine.Object) this.ExpPanel != (UnityEngine.Object) null) || this.ExpPanel.IsBusy)
          return;
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get((int) this.mCurrentArtifact.Rarity < (int) this.mCurrentArtifact.RarityCap ? "sys.ARTI_EXPFULL1" : "sys.ARTI_EXPFULL2"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        if (!this.ExpPanel.IsBusy)
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 151);
        if (!string.IsNullOrEmpty(this.KyokaSE))
          MonoSingleton<MySound>.Instance.PlaySEOneShot(this.KyokaSE, 0.0f);
        ArtifactWindow.RequestAddExp requestAddExp = (ArtifactWindow.RequestAddExp) null;
        if (this.mRequests.Count > 0 && this.mRequests[this.mRequests.Count - 1] is ArtifactWindow.RequestAddExp)
          requestAddExp = this.mRequests[this.mRequests.Count - 1] as ArtifactWindow.RequestAddExp;
        if (requestAddExp == null)
        {
          requestAddExp = new ArtifactWindow.RequestAddExp();
          requestAddExp.UniqueID = (long) this.mCurrentArtifact.UniqueID;
          requestAddExp.Callback = new Network.ResponseCallback(this.AddExpResult);
          this.AddAsyncRequest((ArtifactWindow.Request) requestAddExp);
        }
        if (this.mStatusCache == null)
        {
          this.mStatusCache = new ArtifactWindow.StatusCache();
          UnitData unit = (UnitData) null;
          int job_index = -1;
          if ((UnityEngine.Object) this.OwnerSlot != (UnityEngine.Object) null)
          {
            JobData job = (JobData) null;
            if (MonoSingleton<GameManager>.Instance.Player.FindOwner(this.mCurrentArtifact, out unit, out job))
              job_index = Array.IndexOf<JobData>(unit.Jobs, job);
          }
          if (this.mOwnerUnitData != null)
          {
            unit = this.mOwnerUnitData;
            job_index = this.mOwnerUnitJobIndex;
          }
          this.mCurrentArtifact.GetHomePassiveBuffStatus(ref this.mStatusCache.BaseAdd, ref this.mStatusCache.BaseMul, (UnitData) null, 0, true);
          this.mCurrentArtifact.GetHomePassiveBuffStatus(ref this.mStatusCache.UnitAdd, ref this.mStatusCache.UnitMul, unit, job_index, true);
        }
        foreach (ItemData itemData in itemList)
        {
          requestAddExp.Items.Add(itemData.Param);
          this.mCurrentArtifact.GainExp(itemData.Param.value);
          itemData.Used(1);
          ++this.mUseEnhanceItemNum;
        }
        if ((UnityEngine.Object) updataTarget != (UnityEngine.Object) null)
          GameParameter.UpdateAll(updataTarget);
        if ((UnityEngine.Object) this.ExpPanel != (UnityEngine.Object) null)
          this.ExpPanel.AnimateTo(this.mCurrentArtifact.Exp);
        else
          this.OnKyokaEnd();
      }
    }

    private void AddExpResult(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.ArtifactMatShort)
          FlowNode_Network.Back();
        else
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
          int beforeLevel = -1;
          long iid = -1;
          if (this.mCurrentArtifact != null)
          {
            iid = (long) this.mCurrentArtifact.UniqueID;
            ArtifactData artifactByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(iid);
            if (artifactByUniqueId != null)
              beforeLevel = (int) artifactByUniqueId.Lv;
          }
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
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
          if (!Network.IsImmediateMode)
            this.RefreshArtifactList();
          Network.RemoveAPI();
          this.mSendingRequests = false;
          ArtifactData artifactByUniqueId1 = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(iid);
          MonoSingleton<GameManager>.Instance.Player.OnArtifactStrength(this.mCurrentArtifactParam.iname, this.mUseEnhanceItemNum, beforeLevel, (int) artifactByUniqueId1.Lv);
          this.mUseEnhanceItemNum = 0;
        }
      }
    }

    public void RefreshArtifactList()
    {
      if (!((UnityEngine.Object) this.ArtifactList != (UnityEngine.Object) null))
        return;
      this.ArtifactList.Refresh();
    }

    private void OnArtifactSelect(ArtifactList list)
    {
      object[] selection = list.Selection;
      this.mTotalSellPrice = 0L;
      this.mSelectedArtifacts = selection;
      if (selection.Length <= 0)
      {
        this.mCurrentArtifact = (ArtifactData) null;
        this.mCurrentArtifactParam = (ArtifactParam) null;
        if ((UnityEngine.Object) this.ArtifactSlot != (UnityEngine.Object) null)
          this.ArtifactSlot.SetSlotData<ArtifactData>((ArtifactData) null);
        if ((UnityEngine.Object) this.artiProssessButton != (UnityEngine.Object) null)
          this.artiProssessButton.interactable = false;
        this.RefreshWindow();
      }
      else
      {
        if ((UnityEngine.Object) this.SellPrice != (UnityEngine.Object) null)
        {
          for (int index = 0; index < selection.Length; ++index)
          {
            if (selection[index] is ArtifactData)
              this.mTotalSellPrice += (long) ((ArtifactData) selection[index]).GetSellPrice();
            else if (selection[index] is ArtifactParam)
              this.mTotalSellPrice += (long) ((ArtifactParam) selection[index]).sell;
          }
        }
        if ((UnityEngine.Object) this.DecomposeCostTotal != (UnityEngine.Object) null)
        {
          long num = 0;
          for (int index = 0; index < selection.Length; ++index)
          {
            if (selection[index] is ArtifactData)
              num += (long) (int) ((ArtifactData) selection[index]).RarityParam.ArtifactChangeCost;
          }
          this.DecomposeCostTotal.text = num.ToString();
        }
        this.mCurrentArtifact = (ArtifactData) null;
        this.mCurrentArtifactParam = (ArtifactParam) null;
        if (!(selection[0] is ArtifactData))
        {
          if (selection[0] is ArtifactParam)
          {
            this.mCurrentArtifactParam = (ArtifactParam) selection[0];
            if (this.mCurrentArtifactParam == null)
            {
              Debug.LogWarning((object) "Non ArtifactParam Selected");
              return;
            }
          }
          else
          {
            Debug.LogWarning((object) "Non ArtifactData Selected");
            return;
          }
        }
        if (selection[0] is ArtifactData)
        {
          this.mCurrentArtifact = (ArtifactData) selection[0];
          this.mCurrentArtifact = this.mCurrentArtifact.Copy();
          this.mCurrentArtifactParam = this.mCurrentArtifact.ArtifactParam;
          GlobalVars.SelectedArtifactUniqueID.Set((long) this.mCurrentArtifact.UniqueID);
        }
        else
        {
          Json_Artifact json = new Json_Artifact();
          json.iname = this.mCurrentArtifactParam.iname;
          json.rare = this.mCurrentArtifactParam.rareini;
          this.mCurrentArtifact = new ArtifactData();
          this.mCurrentArtifact.Deserialize(json);
        }
        this.Rebind();
        this.RefreshWindow();
        if (!((UnityEngine.Object) list != (UnityEngine.Object) null) || list.GetAutoSelected(true))
          return;
        if (this.mCurrentArtifact.CheckEnableRarityUp() == ArtifactData.RarityUpResults.Success)
        {
          if (!((UnityEngine.Object) this.ProcessToggle_Evolution != (UnityEngine.Object) null))
            return;
          this.ProcessToggle_Evolution.isOn = true;
        }
        else
        {
          if (!((UnityEngine.Object) this.ProcessToggle_Enhance != (UnityEngine.Object) null))
            return;
          this.ProcessToggle_Enhance.isOn = true;
        }
      }
    }

    public void RefreshAbilities()
    {
      if (this.mCurrentArtifact == null || (UnityEngine.Object) this.AbilityListItem == (UnityEngine.Object) null)
        return;
      DataSource.Bind<AbilityParam>(this.AbilityListItem, (AbilityParam) null, false);
      DataSource.Bind<AbilityData>(this.AbilityListItem, (AbilityData) null, false);
      DataSource.Bind<SkillAbilityDeriveData>(this.AbilityListItem, (SkillAbilityDeriveData) null, false);
      if ((UnityEngine.Object) this.AbilityListItemAdd != (UnityEngine.Object) null)
      {
        DataSource.Bind<AbilityParam>(this.AbilityListItemAdd, (AbilityParam) null, false);
        DataSource.Bind<AbilityData>(this.AbilityListItemAdd, (AbilityData) null, false);
        DataSource.Bind<SkillAbilityDeriveData>(this.AbilityListItemAdd, (SkillAbilityDeriveData) null, false);
      }
      List<AbilityDeriveParam> abilityDeriveParamList = (List<AbilityDeriveParam>) null;
      if (this.mOwnerUnitData != null)
        abilityDeriveParamList = this.mOwnerUnitData.GetSkillAbilityDeriveData(this.mOwnerUnitData.Jobs[this.mOwnerUnitJobIndex], this.mSelectArtifactSlot, this.mCurrentArtifact.ArtifactParam)?.GetAvailableAbilityDeriveParams();
      if ((UnityEngine.Object) this.OwnerSlot != (UnityEngine.Object) null)
      {
        UnitData unit = (UnitData) null;
        int num = -1;
        if ((UnityEngine.Object) this.OwnerSlot != (UnityEngine.Object) null)
        {
          JobData job = (JobData) null;
          if (MonoSingleton<GameManager>.Instance.Player.FindOwner(this.mCurrentArtifact, out unit, out job))
            num = Array.IndexOf<JobData>(unit.Jobs, job);
          if (this.mOwnerUnitData != null)
          {
            unit = this.mOwnerUnitData;
            num = this.mOwnerUnitJobIndex;
          }
          if (unit == null || num == -1)
            ;
        }
      }
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      ArtifactParam artifactParam = this.mCurrentArtifact.ArtifactParam;
      int index1 = 0;
      List<AbilityData> learningAbilities = this.mCurrentArtifact.LearningAbilities;
      if (artifactParam.abil_inames != null)
      {
        for (int index2 = 0; index2 < artifactParam.abil_inames.Length; ++index2)
        {
          if (!string.IsNullOrEmpty(artifactParam.abil_inames[index2]) && artifactParam.abil_shows[index2] != 0)
          {
            AbilityParam abilityParam = masterParam.GetAbilityParam(artifactParam.abil_inames[index2]);
            if (abilityParam != null)
            {
              GameObject gameObject1;
              if ((UnityEngine.Object) this.AbilityList == (UnityEngine.Object) null)
              {
                if (DataSource.FindDataOfClass<AbilityParam>(this.AbilityListItem, (AbilityParam) null) == null && DataSource.FindDataOfClass<AbilityData>(this.AbilityListItem, (AbilityData) null) == null)
                {
                  DataSource.Bind<AbilityParam>(this.AbilityListItem, abilityParam, false);
                  if ((UnityEngine.Object) this.AbilityListItemAdd != (UnityEngine.Object) null)
                    DataSource.Bind<AbilityParam>(this.AbilityListItemAdd, abilityParam, false);
                }
                gameObject1 = this.AbilityListItem;
              }
              else
              {
                if (this.mAbilityItems.Count <= index1)
                {
                  GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.AbilityListItem);
                  gameObject2.transform.SetParent(this.AbilityList.transform, false);
                  this.mAbilityItems.Add(gameObject2);
                }
                DataSource.Bind<AbilityParam>(this.mAbilityItems[index1], abilityParam, false);
                this.mAbilityItems[index1].SetActive(true);
                gameObject1 = this.mAbilityItems[index1];
                if ((UnityEngine.Object) this.AbilityListItemAdd != (UnityEngine.Object) null)
                  this.AbilityListItemAdd = this.mAbilityItems[index1];
              }
              DataSource.Bind<AbilityData>(gameObject1, (AbilityData) null, false);
              if ((UnityEngine.Object) this.AbilityListItemAdd != (UnityEngine.Object) null)
                DataSource.Bind<AbilityData>(this.AbilityListItemAdd, (AbilityData) null, false);
              Animator component = gameObject1.GetComponent<Animator>();
              bool flag = false;
              if ((UnityEngine.Object) component != (UnityEngine.Object) null && learningAbilities != null && !string.IsNullOrEmpty(this.AbilityListItemState))
              {
                if (learningAbilities != null)
                {
                  for (int index3 = 0; index3 < learningAbilities.Count; ++index3)
                  {
                    string iname = learningAbilities[index3].Param.iname;
                    if (artifactParam.abil_inames[index2] == iname)
                    {
                      AbilityData deriveAbility = learningAbilities[index3];
                      if (abilityDeriveParamList != null)
                      {
                        AbilityDeriveParam deriveParam = abilityDeriveParamList.Find((Predicate<AbilityDeriveParam>) (derive_param => derive_param.BaseAbilityIname == iname));
                        if (deriveParam != null)
                          deriveAbility = learningAbilities[index3].CreateDeriveAbility(deriveParam);
                      }
                      DataSource.Bind<AbilityData>(gameObject1, deriveAbility, false);
                      DataSource.Bind<AbilityParam>(gameObject1, deriveAbility.Param, false);
                      if ((UnityEngine.Object) this.AbilityListItemAdd != (UnityEngine.Object) null)
                      {
                        DataSource.Bind<AbilityData>(this.AbilityListItemAdd, deriveAbility, false);
                        DataSource.Bind<AbilityParam>(this.AbilityListItemAdd, deriveAbility.Param, false);
                      }
                      flag = this.mOwnerUnitData == null || abilityParam.CheckEnableUseAbility(this.mOwnerUnitData, this.mOwnerUnitJobIndex);
                      break;
                    }
                  }
                }
                if (flag)
                {
                  component.SetInteger(this.AbilityListItemState, this.AbilityListItem_Unlocked);
                  if ((UnityEngine.Object) this.AbilityListItemAdd != (UnityEngine.Object) null)
                    this.AbilityListItemAdd.GetComponent<Animator>().SetInteger(this.AbilityListItemState, this.AbilityListItem_Unlocked);
                }
                else
                {
                  component.SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
                  if ((UnityEngine.Object) this.AbilityListItemAdd != (UnityEngine.Object) null)
                    this.AbilityListItemAdd.GetComponent<Animator>().SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
                }
              }
              ++index1;
              if (flag)
                break;
            }
          }
        }
      }
      if ((UnityEngine.Object) this.AbilityList == (UnityEngine.Object) null && index1 == 0)
      {
        DataSource.Bind<AbilityParam>(this.AbilityListItem, (AbilityParam) null, false);
        DataSource.Bind<AbilityData>(this.AbilityListItem, (AbilityData) null, false);
        if ((UnityEngine.Object) this.AbilityListItemAdd != (UnityEngine.Object) null)
        {
          DataSource.Bind<AbilityParam>(this.AbilityListItemAdd, (AbilityParam) null, false);
          DataSource.Bind<AbilityData>(this.AbilityListItemAdd, (AbilityData) null, false);
        }
        Animator component1 = this.AbilityListItem.GetComponent<Animator>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null && !string.IsNullOrEmpty(this.AbilityListItemState))
          component1.SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
        if ((UnityEngine.Object) this.AbilityListItemAdd != (UnityEngine.Object) null)
        {
          Animator component2 = this.AbilityListItemAdd.GetComponent<Animator>();
          if ((UnityEngine.Object) component2 != (UnityEngine.Object) null && !string.IsNullOrEmpty(this.AbilityListItemState))
            component2.SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
        }
      }
      for (; index1 < this.mAbilityItems.Count; ++index1)
        this.mAbilityItems[index1].SetActive(false);
      if (this.mCurrentArtifact == null || (UnityEngine.Object) this.EquipInspSkillItem == (UnityEngine.Object) null)
        return;
      DataSource.Bind<AbilityParam>(this.EquipInspSkillItem, (AbilityParam) null, false);
      DataSource.Bind<AbilityData>(this.EquipInspSkillItem, (AbilityData) null, false);
      if ((UnityEngine.Object) this.EquipInspSkillItemAdd != (UnityEngine.Object) null)
      {
        DataSource.Bind<AbilityParam>(this.EquipInspSkillItemAdd, (AbilityParam) null, false);
        DataSource.Bind<AbilityData>(this.EquipInspSkillItemAdd, (AbilityData) null, false);
      }
      Animator component3 = this.EquipInspSkillItem.GetComponent<Animator>();
      bool flag1 = false;
      if (this.mCurrentArtifact.IsInspiration())
      {
        ArtifactData artifactData = MonoSingleton<GameManager>.Instance.Player.Artifacts.Find((Predicate<ArtifactData>) (select => (long) select.UniqueID == (long) this.mCurrentArtifact.UniqueID));
        if (artifactData != null)
        {
          InspirationSkillData inspirationSkillData = artifactData.GetCurrentInspirationSkillData();
          if (inspirationSkillData != null)
          {
            DataSource.Bind<AbilityParam>(this.EquipInspSkillItem, inspirationSkillData.AbilityData.Param, false);
            DataSource.Bind<AbilityData>(this.EquipInspSkillItem, inspirationSkillData.AbilityData, false);
            if ((UnityEngine.Object) this.EquipInspSkillItemAdd != (UnityEngine.Object) null)
            {
              DataSource.Bind<AbilityParam>(this.EquipInspSkillItemAdd, inspirationSkillData.AbilityData.Param, false);
              DataSource.Bind<AbilityData>(this.EquipInspSkillItemAdd, inspirationSkillData.AbilityData, false);
            }
            flag1 = true;
          }
        }
      }
      if ((UnityEngine.Object) component3 != (UnityEngine.Object) null)
      {
        if (flag1)
          component3.SetInteger(this.AbilityListItemState, this.AbilityListItem_Unlocked);
        else
          component3.SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
      }
      if ((UnityEngine.Object) this.EquipInspSkillItemAdd != (UnityEngine.Object) null)
      {
        Animator component1 = this.EquipInspSkillItemAdd.GetComponent<Animator>();
        if (flag1)
          component1.SetInteger(this.AbilityListItemState, this.AbilityListItem_Unlocked);
        else
          component1.SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
        GameParameter.UpdateAll(this.EquipInspSkillItemAdd);
      }
      GameParameter.UpdateAll(this.EquipInspSkillItem);
      if (!((UnityEngine.Object) this.EquipInspSkillTab != (UnityEngine.Object) null))
        return;
      if (this.mCurrentArtifact.IsInspiration())
      {
        this.EquipInspSkillTab.gameObject.SetActive(true);
        if (!((UnityEngine.Object) this.EquipInspSkillTabAdd != (UnityEngine.Object) null))
          return;
        this.EquipInspSkillTabAdd.gameObject.SetActive(true);
      }
      else
      {
        this.EquipInspSkillTab.gameObject.SetActive(false);
        this.EquipInspSkillTab.isOn = false;
        if ((UnityEngine.Object) this.EquipInspSkillTabAdd != (UnityEngine.Object) null)
        {
          this.EquipInspSkillTabAdd.gameObject.SetActive(false);
          this.EquipInspSkillTabAdd.isOn = false;
        }
        this.WeaponAbilityTab.isOn = true;
        if (!((UnityEngine.Object) this.WeaponAbilityTabAdd != (UnityEngine.Object) null))
          return;
        this.WeaponAbilityTabAdd.isOn = true;
      }
    }

    public void ShowSelection()
    {
      if ((UnityEngine.Object) this.SelectionListItem == (UnityEngine.Object) null)
        return;
      for (int index = 0; index < this.mSelectionItems.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mSelectionItems[index].gameObject);
      this.mSelectionItems.Clear();
      if (this.mSelectedArtifacts == null)
        return;
      if ((UnityEngine.Object) this.SelectionNum != (UnityEngine.Object) null)
        this.SelectionNum.text = this.mSelectedArtifacts.Length.ToString();
      Transform parent = !((UnityEngine.Object) this.SelectionList == (UnityEngine.Object) null) ? this.SelectionList.transform : this.SelectionListItem.transform.parent;
      for (int index = 0; index < this.mSelectedArtifacts.Length; ++index)
      {
        ArtifactIcon artifactIcon = UnityEngine.Object.Instantiate<ArtifactIcon>(this.SelectionListItem);
        this.mSelectionItems.Add(artifactIcon);
        DataSource.Bind(artifactIcon.gameObject, this.mSelectedArtifacts[index].GetType(), this.mSelectedArtifacts[index], false);
        artifactIcon.transform.SetParent(parent, false);
        artifactIcon.gameObject.SetActive(true);
      }
    }

    public void RefreshWindow()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      UnitData unit = (UnitData) null;
      int job_index = 0;
      if ((UnityEngine.Object) this.OwnerSlot != (UnityEngine.Object) null && this.mCurrentArtifact != null)
      {
        JobData job = (JobData) null;
        if (player.FindOwner(this.mCurrentArtifact, out unit, out job))
          job_index = Array.IndexOf<JobData>(unit.Jobs, job);
      }
      if (this.mOwnerUnitData != null)
      {
        unit = this.mOwnerUnitData;
        job_index = this.mOwnerUnitJobIndex;
      }
      if ((UnityEngine.Object) this.Status != (UnityEngine.Object) null)
      {
        if (this.mCurrentArtifact != null)
        {
          BaseStatus fixed_status1 = new BaseStatus();
          BaseStatus scale_status1 = new BaseStatus();
          this.mCurrentArtifact.GetHomePassiveBuffStatus(ref fixed_status1, ref scale_status1, (UnitData) null, 0, true);
          BaseStatus fixed_status2 = new BaseStatus();
          BaseStatus scale_status2 = new BaseStatus();
          this.mCurrentArtifact.GetHomePassiveBuffStatus(ref fixed_status2, ref scale_status2, unit, job_index, true);
          this.Status.SetValues(fixed_status1, scale_status1, fixed_status2, scale_status2, false);
          if ((UnityEngine.Object) this.StatusAdd != (UnityEngine.Object) null)
            this.StatusAdd.SetValues(fixed_status1, scale_status1, fixed_status2, scale_status2, false);
        }
        else
        {
          BaseStatus baseStatus = new BaseStatus();
          this.Status.SetValues(baseStatus, baseStatus, false);
          if ((UnityEngine.Object) this.StatusAdd != (UnityEngine.Object) null)
            this.StatusAdd.SetValues(baseStatus, baseStatus, false);
        }
      }
      if ((UnityEngine.Object) this.OwnerSlot != (UnityEngine.Object) null)
      {
        this.OwnerSlot.SetSlotData<UnitData>(unit);
        if (unit != null && job_index != -1)
          DataSource.Bind<JobData>(this.OwnerSlot.gameObject, unit.Jobs[job_index], false);
        else
          DataSource.Bind<JobData>(this.OwnerSlot.gameObject, (JobData) null, false);
      }
      this.RefreshKyokaList();
      this.RefreshDecomposeInfo();
      this.RefreshTransmuteInfo();
      this.RefreshRarityUpInfo();
      this.RefreshAbilities();
      if ((UnityEngine.Object) this.DetailMaterial != (UnityEngine.Object) null && this.mCurrentArtifact != null)
        this.DetailMaterial.SetMaterial(0, this.mCurrentArtifact.Kakera);
      if ((UnityEngine.Object) this.KyokaPanel != (UnityEngine.Object) null && this.mCurrentArtifact != null)
      {
        Animator component = this.KyokaPanel.GetComponent<Animator>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null && !string.IsNullOrEmpty(this.KyokaPanelState))
        {
          if ((int) this.mCurrentArtifact.Lv >= (int) this.mCurrentArtifact.LvCap)
          {
            if ((int) this.mCurrentArtifact.Rarity < (int) this.mCurrentArtifact.RarityCap)
              component.SetInteger(this.KyokaPanelState, this.KyokaPanel_LvCapped);
            else
              component.SetInteger(this.KyokaPanelState, this.KyokaPanel_LvMax);
          }
          else
            component.SetInteger(this.KyokaPanelState, this.KyokaPanel_Enable);
        }
      }
      if ((UnityEngine.Object) this.SellPrice != (UnityEngine.Object) null)
        this.SellPrice.text = this.mTotalSellPrice.ToString();
      if ((UnityEngine.Object) this.RarityUpCost != (UnityEngine.Object) null && this.mCurrentArtifact != null)
      {
        Animator component = this.RarityUpCost.GetComponent<Animator>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null && !string.IsNullOrEmpty(this.RarityUpCostState))
        {
          if ((int) this.mCurrentArtifact.RarityParam.ArtifactRarityUpCost <= player.Gold)
            component.SetInteger(this.RarityUpCostState, this.RarityUpCost_Normal);
          else
            component.SetInteger(this.RarityUpCostState, this.RarityUpCost_NoGold);
        }
      }
      if ((UnityEngine.Object) this.ExpPanel != (UnityEngine.Object) null && this.mCurrentArtifact != null)
      {
        this.ExpPanel.LevelCap = this.mCurrentArtifact.GetLevelCap();
        this.ExpPanel.Exp = this.mCurrentArtifact.Exp;
      }
      if ((UnityEngine.Object) this.ExpPanelAdd != (UnityEngine.Object) null && this.mCurrentArtifact != null)
      {
        this.ExpPanelAdd.LevelCap = this.mCurrentArtifact.GetLevelCap();
        this.ExpPanelAdd.Exp = this.mCurrentArtifact.Exp;
      }
      if ((UnityEngine.Object) this.WindowBody != (UnityEngine.Object) null)
        GameParameter.UpdateAll(this.WindowBody.gameObject);
      if (!((UnityEngine.Object) this.WindowBodyAdd != (UnityEngine.Object) null))
        return;
      GameParameter.UpdateAll(this.WindowBodyAdd.gameObject);
    }

    private void OnLevelChange(int prev, int next)
    {
    }

    private void OnKyokaEnd()
    {
      UnitData unit = (UnitData) null;
      int job_index = 0;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if ((UnityEngine.Object) this.OwnerSlot != (UnityEngine.Object) null)
      {
        JobData job = (JobData) null;
        if (player.FindOwner(this.mCurrentArtifact, out unit, out job))
          job_index = Array.IndexOf<JobData>(unit.Jobs, job);
      }
      if (this.mOwnerUnitData != null)
      {
        unit = this.mOwnerUnitData;
        job_index = this.mOwnerUnitJobIndex;
      }
      BaseStatus fixed_status1 = new BaseStatus();
      BaseStatus scale_status1 = new BaseStatus();
      this.mCurrentArtifact.GetHomePassiveBuffStatus(ref fixed_status1, ref scale_status1, (UnitData) null, 0, true);
      BaseStatus fixed_status2 = new BaseStatus();
      BaseStatus scale_status2 = new BaseStatus();
      this.mCurrentArtifact.GetHomePassiveBuffStatus(ref fixed_status2, ref scale_status2, unit, job_index, true);
      this.RefreshWindow();
      this.RefreshArtifactList();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 152);
    }

    public void AddRarity()
    {
      if (this.IsBusy || this.mCurrentArtifact == null || (int) this.mCurrentArtifact.Rarity >= (int) this.mCurrentArtifact.RarityCap || (UnityEngine.Object) this.ExpPanel != (UnityEngine.Object) null && this.ExpPanel.IsBusy)
        return;
      ArtifactData.RarityUpResults rarityUpResults = this.mCurrentArtifact.CheckEnableRarityUp();
      if (rarityUpResults == ArtifactData.RarityUpResults.Success)
      {
        UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_RARITYUP_CONFIRM", new object[1]
        {
          (object) this.mCurrentArtifactParam.name
        }), new UIUtility.DialogResultEvent(this.OnAddRarityAccept), new UIUtility.DialogResultEvent(this.OnAddRarityCancel), (GameObject) null, false, -1, (string) null, (string) null);
      }
      else
      {
        string key = (string) null;
        if ((rarityUpResults & ArtifactData.RarityUpResults.RarityMaxed) != ArtifactData.RarityUpResults.Success)
          key = "sys.ARTI_RARITYUP_MAX";
        else if ((rarityUpResults & ArtifactData.RarityUpResults.NoLv) != ArtifactData.RarityUpResults.Success)
          key = "sys.ARTI_RARITYUP_NOLV";
        else if ((rarityUpResults & ArtifactData.RarityUpResults.NoGold) != ArtifactData.RarityUpResults.Success)
          key = "sys.ARTI_RARITYUP_NOGOLD";
        else if ((rarityUpResults & ArtifactData.RarityUpResults.NoKakera) != ArtifactData.RarityUpResults.Success)
          key = "sys.ARTI_RARITYUP_NOMTRL";
        if (key == null)
          return;
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(key), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
    }

    private void OnAddRarityAccept(GameObject go)
    {
      int levelCap1 = this.mCurrentArtifact.GetLevelCap();
      List<AbilityData> abilityDataList1 = new List<AbilityData>(4);
      List<int> intList = new List<int>(4);
      List<AbilityData> abilityDataList2 = new List<AbilityData>(4);
      ArtifactParam artifactParam = this.mCurrentArtifact.ArtifactParam;
      if (artifactParam.abil_shows != null && this.mCurrentArtifact.LearningAbilities != null)
      {
        for (int index = 0; index < this.mCurrentArtifact.LearningAbilities.Count; ++index)
        {
          string iname = this.mCurrentArtifact.LearningAbilities[index].Param.iname;
          if (Array.IndexOf((Array) artifactParam.abil_shows, (object) iname) >= 0)
          {
            abilityDataList1.Add(this.mCurrentArtifact.LearningAbilities[index]);
            intList.Add(this.mCurrentArtifact.LearningAbilities[index].Rank);
          }
        }
      }
      this.mCurrentArtifact.RarityUp();
      if (artifactParam.abil_shows != null && this.mCurrentArtifact.LearningAbilities != null)
      {
        for (int index = 0; index < this.mCurrentArtifact.LearningAbilities.Count; ++index)
        {
          if (!abilityDataList1.Contains(this.mCurrentArtifact.LearningAbilities[index]))
          {
            string iname = this.mCurrentArtifact.LearningAbilities[index].Param.iname;
            if (Array.IndexOf((Array) artifactParam.abil_shows, (object) iname) >= 0)
              abilityDataList2.Add(this.mCurrentArtifact.LearningAbilities[index]);
          }
        }
      }
      int levelCap2 = this.mCurrentArtifact.GetLevelCap();
      int num = 0;
      this.mChangeSet = new List<ChangeListData>(4);
      this.mChangeSet.Add(new ChangeListData());
      this.mChangeSet[0].ItemID = this.ArtifactDataID;
      this.mChangeSet[0].Label = LocalizedText.Get("sys.ARTI_MAXRULV");
      this.mChangeSet[0].ValOld = levelCap1.ToString();
      this.mChangeSet[0].ValNew = levelCap2.ToString();
      int index1 = num + 1;
      for (int index2 = 0; index2 < abilityDataList1.Count; ++index2)
      {
        if (abilityDataList1[index2].Rank != intList[index2])
        {
          this.mChangeSet.Add(new ChangeListData());
          this.mChangeSet[index1].ItemID = this.ChangedAbilityID;
          this.mChangeSet[index1].MetaDataType = typeof (AbilityData);
          this.mChangeSet[index1].MetaData = (object) abilityDataList1[index2];
          this.mChangeSet[index1].ValOld = intList[index2].ToString();
          this.mChangeSet[index1].ValNew = abilityDataList1[index2].Rank.ToString();
          ++index1;
        }
      }
      for (int index2 = 0; index2 < abilityDataList2.Count; ++index2)
      {
        if (abilityDataList1[index2].Rank != intList[index2])
        {
          this.mChangeSet.Add(new ChangeListData());
          this.mChangeSet[index1].ItemID = this.NewAbilityID;
          this.mChangeSet[index1].MetaDataType = typeof (AbilityData);
          this.mChangeSet[index1].MetaData = (object) abilityDataList2[index2];
          this.mChangeSet[index1].ValOld = abilityDataList1[index2].Rank.ToString();
          this.mChangeSet[index1].ValNew = intList[index2].ToString();
          ++index1;
        }
      }
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) this);
      this.StartCoroutine(this.AddRareAsync());
      MonoSingleton<GameManager>.Instance.Player.OnArtifactEvolution(this.mCurrentArtifactParam.iname);
      string trophy_progs;
      string bingo_progs;
      MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
      ArtifactWindow.RequestAddRare requestAddRare = new ArtifactWindow.RequestAddRare();
      requestAddRare.UniqueID = (long) this.mCurrentArtifact.UniqueID;
      requestAddRare.trophyprog = trophy_progs;
      requestAddRare.bingoprog = bingo_progs;
      requestAddRare.Callback = new Network.ResponseCallback(this.AddRarityResult);
      this.AddAsyncRequest((ArtifactWindow.Request) requestAddRare);
      this.mBusy = true;
      this.FlushRequests(false);
    }

    private void OnAddRarityCancel(GameObject go)
    {
    }

    [DebuggerHidden]
    private IEnumerator AddRareAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ArtifactWindow.\u003CAddRareAsync\u003Ec__Iterator0() { \u0024this = this };
    }

    private void AddAsyncRequest(ArtifactWindow.Request req)
    {
      this.mRequests.Add(req);
      this.mFlushTimer = this.AutoFlushRequests;
    }

    private void AddRarityResult(WWWResult www)
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
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
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
          if (!Network.IsImmediateMode)
          {
            this.SyncArtifactData();
            this.RefreshWindow();
            this.RefreshArtifactList();
          }
          this.mBusy = false;
          Network.RemoveAPI();
          MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 201);
        }
      }
    }

    public void RefreshDecomposeInfo()
    {
      if (this.mCurrentArtifact == null || (long) this.mCurrentArtifact.UniqueID == 0L)
        return;
      if ((UnityEngine.Object) this.DecomposePanel != (UnityEngine.Object) null && !string.IsNullOrEmpty(this.DecomposePanelState))
      {
        Animator component = this.DecomposePanel.GetComponent<Animator>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          if (this.mCurrentArtifact.Kakera != null)
            component.SetInteger(this.DecomposePanelState, this.DecomposePanel_Normal);
          else
            component.SetInteger(this.DecomposePanelState, this.DecomposePanel_Disabled);
        }
      }
      if (this.mCurrentArtifact.Kakera != null)
      {
        if ((UnityEngine.Object) this.DecomposeHelp != (UnityEngine.Object) null)
          this.DecomposeHelp.text = LocalizedText.Get("sys.ARTI_DECOMPOSE_HELP", (object) this.mCurrentArtifact.ArtifactParam.name, (object) this.mCurrentArtifact.Kakera.name, (object) this.mCurrentArtifact.GetKakeraChangeNum());
        if ((UnityEngine.Object) this.DecomposeCost != (UnityEngine.Object) null)
        {
          long num = (long) Math.Abs((int) this.mCurrentArtifact.RarityParam.ArtifactChangeCost);
          this.DecomposeCost.text = num.ToString();
          if (!string.IsNullOrEmpty(this.DecomposeCostState))
          {
            Animator component = this.DecomposeCost.GetComponent<Animator>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            {
              if ((long) MonoSingleton<GameManager>.Instance.Player.Gold >= num)
                component.SetInteger(this.DecomposeCostState, this.DecomposeCost_Normal);
              else
                component.SetInteger(this.DecomposeCostState, this.DecomposeCost_NoGold);
            }
          }
        }
        if ((UnityEngine.Object) this.DecomposeItem != (UnityEngine.Object) null)
          this.DecomposeItem.SetMaterial(this.mCurrentArtifact.GetKakeraChangeNum(), this.mCurrentArtifact.Kakera);
        int itemAmount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mCurrentArtifact.Kakera.iname);
        if ((UnityEngine.Object) this.DecomposeKakeraNumOld != (UnityEngine.Object) null)
          this.DecomposeKakeraNumOld.text = itemAmount.ToString();
        if ((UnityEngine.Object) this.DecomposeKakeraNumNew != (UnityEngine.Object) null)
          this.DecomposeKakeraNumNew.text = Mathf.Min(itemAmount + this.mCurrentArtifact.GetKakeraChangeNum(), this.mCurrentArtifact.Kakera.cap).ToString();
      }
      if (!((UnityEngine.Object) this.DecomposeWarning != (UnityEngine.Object) null))
        return;
      bool flag = false;
      if (this.mSelectedArtifacts != null)
      {
        for (int index = 0; index < this.mSelectedArtifacts.Length; ++index)
        {
          if (this.mSelectedArtifacts[index] is ArtifactData && (int) ((ArtifactData) this.mSelectedArtifacts[index]).Rarity >= this.DecomposeWarningRarity)
          {
            flag = true;
            break;
          }
        }
      }
      this.DecomposeWarning.gameObject.SetActive(flag);
    }

    public void RefreshTransmuteInfo()
    {
      if (this.mCurrentArtifactParam == null)
        return;
      if ((UnityEngine.Object) this.TransmutePanel != (UnityEngine.Object) null)
      {
        Animator component = this.TransmutePanel.GetComponent<Animator>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null && !string.IsNullOrEmpty(this.TransmutePanelState))
        {
          if (this.mCurrentArtifactParam.is_create)
            component.SetInteger(this.TransmutePanelState, this.TransmutePanel_Normal);
          else
            component.SetInteger(this.TransmutePanelState, this.TransmutePanel_Disabled);
        }
      }
      GameManager instance = MonoSingleton<GameManager>.Instance;
      RarityParam rarityParam = instance.MasterParam.GetRarityParam(this.mCurrentArtifactParam.rareini);
      ItemParam itemParam = instance.GetItemParam(this.mCurrentArtifactParam.kakera);
      if ((UnityEngine.Object) this.TransmuteMaterial != (UnityEngine.Object) null)
        this.TransmuteMaterial.SetMaterial((int) rarityParam.ArtifactCreatePieceNum, itemParam);
      if ((UnityEngine.Object) this.TransmuteCost != (UnityEngine.Object) null)
        this.TransmuteCost.text = (int) rarityParam.ArtifactCreateCost.ToString();
      if (!((UnityEngine.Object) this.TransmuteCondition != (UnityEngine.Object) null))
        return;
      int kakeraCreateNum = this.mCurrentArtifact.GetKakeraCreateNum();
      if (itemParam == null || kakeraCreateNum <= 0)
        return;
      this.TransmuteCondition.text = LocalizedText.Get("sys.ARTI_TRANSMUTE_REQ", (object) itemParam.name, (object) kakeraCreateNum);
    }

    public void RefreshRarityUpInfo()
    {
      if (this.mCurrentArtifact == null)
        return;
      bool flag = this.mCurrentArtifact.CheckEnableRarityUp() == ArtifactData.RarityUpResults.Success;
      if ((UnityEngine.Object) this.RarityUpPanel != (UnityEngine.Object) null)
      {
        Animator component = this.RarityUpPanel.GetComponent<Animator>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null && !string.IsNullOrEmpty(this.RarityUpPanelState))
        {
          if ((int) this.mCurrentArtifact.Rarity < (int) this.mCurrentArtifact.RarityCap)
          {
            bool hasKakera = false;
            this.mCurrentArtifact.GetKakeraDataListForRarityUp().ForEach((Action<ItemData>) (kakera =>
            {
              if (kakera.Num <= 0)
                return;
              hasKakera = true;
            }));
            if (hasKakera)
              component.SetInteger(this.RarityUpPanelState, this.RarityUpPanel_Normal);
            else
              component.SetInteger(this.RarityUpPanelState, this.RarityUpPanel_NoItem);
          }
          else
            component.SetInteger(this.RarityUpPanelState, this.RarityUpPanel_MaxRarity);
        }
      }
      if ((UnityEngine.Object) this.RarityUpButton != (UnityEngine.Object) null)
        this.RarityUpButton.interactable = flag;
      if ((UnityEngine.Object) this.RarityUpHilit != (UnityEngine.Object) null)
        this.RarityUpHilit.SetActive(flag);
      if ((UnityEngine.Object) this.RarityUpIconRoot != (UnityEngine.Object) null)
        this.RarityUpIconRoot.SetActive(flag);
      if ((int) this.mCurrentArtifact.Rarity >= (int) this.mCurrentArtifact.RarityCap)
        return;
      if ((UnityEngine.Object) this.RarityUpCost != (UnityEngine.Object) null)
        this.RarityUpCost.text = (int) this.mCurrentArtifact.RarityParam.ArtifactRarityUpCost.ToString();
      if ((UnityEngine.Object) this.RarityUpIconRoot != (UnityEngine.Object) null)
        this.RarityUpIconRoot.SetActive(flag);
      if (!((UnityEngine.Object) this.RarityUpList != (UnityEngine.Object) null) || !((UnityEngine.Object) this.RarityUpListItem != (UnityEngine.Object) null))
        return;
      List<ItemData> dataListForRarityUp = this.mCurrentArtifact.GetKakeraDataListForRarityUp();
      int index1 = 0;
      Transform transform = this.RarityUpList.transform;
      int num = Mathf.Max(dataListForRarityUp.Count, this.mRarityUpItems.Count);
      int kakeraNeedNum = this.mCurrentArtifact.GetKakeraNeedNum();
      for (int index2 = 0; index2 < num; ++index2)
      {
        GameObject root;
        if (this.mRarityUpItems.Count <= index1)
        {
          root = UnityEngine.Object.Instantiate<GameObject>(this.RarityUpListItem);
          if ((UnityEngine.Object) root != (UnityEngine.Object) null)
          {
            this.mRarityUpItems.Add(root);
            root.transform.SetParent(transform, false);
          }
        }
        else
          root = this.mRarityUpItems[index1];
        if (index2 < dataListForRarityUp.Count)
        {
          DataSource.Bind<ItemData>(root, dataListForRarityUp[index2], false);
          int reqNum = Mathf.Min(kakeraNeedNum, dataListForRarityUp[index2].Num);
          if (reqNum > 0)
          {
            MaterialPanel component = root.GetComponent<MaterialPanel>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.SetMaterial(reqNum, dataListForRarityUp[index2].Param);
            root.SetActive(true);
          }
          else
            root.SetActive(false);
          kakeraNeedNum -= Mathf.Min(kakeraNeedNum, dataListForRarityUp[index2].Num);
          GameParameter.UpdateAll(root);
          ++index1;
        }
      }
      for (; index1 < this.mRarityUpItems.Count; ++index1)
        this.mRarityUpItems[index1].SetActive(false);
      this.mDisableFlushRequest = true;
    }

    public void TransmuteArtifact()
    {
      if (this.IsBusy || this.mCurrentArtifactParam == null)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int num1 = 0;
      List<ArtifactData> artifacts = player.Artifacts;
      for (int index = 0; index < artifacts.Count; ++index)
      {
        if (artifacts[index].ArtifactParam == this.mCurrentArtifactParam)
          ++num1;
      }
      if (num1 >= this.mCurrentArtifactParam.maxnum)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_TRANSMUTE_MAXNUM", (object) this.mCurrentArtifactParam.name, (object) this.mCurrentArtifactParam.maxnum), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        string kakera = this.mCurrentArtifactParam.kakera;
        ItemData itemDataByItemId = player.FindItemDataByItemID(kakera);
        int num2 = itemDataByItemId == null ? 0 : itemDataByItemId.Num;
        RarityParam rarityParam = MonoSingleton<GameManager>.Instance.GetRarityParam(this.mCurrentArtifactParam.rareini);
        if (num2 < (int) rarityParam.ArtifactCreatePieceNum)
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_TRANSMUTE_NOKAKERA", (object) this.mCurrentArtifactParam.name, (object) rarityParam.ArtifactCreatePieceNum, (object) ((int) rarityParam.ArtifactCreatePieceNum - num2), (object) num2), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        else if (player.Gold < (int) rarityParam.ArtifactCreateCost)
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_TRANSMUTE_NOGOLD", (object) this.mCurrentArtifactParam.name, (object) rarityParam.ArtifactCreateCost, (object) ((int) rarityParam.ArtifactCreateCost - player.Gold), (object) player.Gold), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        else
          UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_TRANSMUTE_CONFIRM", new object[1]
          {
            (object) this.mCurrentArtifactParam.name
          }), new UIUtility.DialogResultEvent(this.OnTransmuteAccept), new UIUtility.DialogResultEvent(this.OnTransmuteCancel), (GameObject) null, false, -1, (string) null, (string) null);
      }
    }

    private void OnTransmuteAccept(GameObject go)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      RarityParam rarityParam = MonoSingleton<GameManager>.Instance.GetRarityParam(this.mCurrentArtifactParam.rareini);
      this.mCachedArtifacts = player.Artifacts.ToArray();
      player.GainGold(-(int) rarityParam.ArtifactCreateCost);
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) this);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 301);
      if (Network.Mode == Network.EConnectMode.Online)
      {
        player.OnArtifactTransmute(this.mCurrentArtifactParam.iname);
        string trophy_progs;
        string bingo_progs;
        MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
        Network.RequestAPI((WebAPI) new ReqArtifactAdd(this.mCurrentArtifactParam.iname, new Network.ResponseCallback(this.OnTransmuteResult), trophy_progs, bingo_progs), false);
      }
      else
      {
        player.FindItemDataByItemID(this.mCurrentArtifactParam.kakera)?.Used((int) rarityParam.ArtifactCreatePieceNum);
        Json_Artifact[] json = new Json_Artifact[1]{ new Json_Artifact() };
        json[0].iid = (long) Random.Range(1, int.MaxValue);
        json[0].iname = this.mCurrentArtifactParam.iname;
        MonoSingleton<GameManager>.Instance.Deserialize(json, false);
        this.ShowTransmuteResult();
        player.OnArtifactTransmute(this.mCurrentArtifactParam.iname);
      }
    }

    private void OnTransmuteCancel(GameObject go)
    {
    }

    private void OnTransmuteResult(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.ArtifactBoxLimit:
            FlowNode_Network.Back();
            UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_FULL"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
            break;
          case Network.EErrCode.ArtifactPieceShort:
            UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_NOKAKERA_ERR"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
            FlowNode_Network.Back();
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
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          try
          {
            instance.Deserialize(jsonObject.body.player);
            instance.Deserialize(jsonObject.body.items);
            instance.Deserialize(jsonObject.body.units);
            if (jsonObject.body.artifacts != null)
              instance.Deserialize(jsonObject.body.artifacts, true);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Retry();
            return;
          }
          Network.RemoveAPI();
          MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
          if (Network.IsImmediateMode)
            return;
          this.ShowTransmuteResult();
        }
      }
    }

    private void ShowTransmuteResult()
    {
      List<ArtifactData> artifacts = MonoSingleton<GameManager>.Instance.Player.Artifacts;
      List<ChangeListData> changeListDataList = new List<ChangeListData>(4);
      for (int index1 = 0; index1 < artifacts.Count; ++index1)
      {
        bool flag = false;
        for (int index2 = 0; index2 < this.mCachedArtifacts.Length; ++index2)
        {
          if ((long) artifacts[index1].UniqueID == (long) this.mCachedArtifacts[index2].UniqueID)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          changeListDataList.Add(new ChangeListData()
          {
            ItemID = this.ArtifactDataID,
            MetaData = (object) artifacts[index1],
            MetaDataType = typeof (ArtifactData)
          });
      }
      this.RefreshArtifactList();
      this.StartCoroutine(this.ShowTransmuteResultAsync(changeListDataList.ToArray()));
    }

    [DebuggerHidden]
    private IEnumerator ShowTransmuteResultAsync(ChangeListData[] changeset)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ArtifactWindow.\u003CShowTransmuteResultAsync\u003Ec__Iterator1() { changeset = changeset, \u0024this = this };
    }

    private ArtifactData[] GetSelectedArtifacts()
    {
      if ((UnityEngine.Object) this.ArtifactList == (UnityEngine.Object) null)
        return new ArtifactData[0];
      object[] selection = this.ArtifactList.Selection;
      List<ArtifactData> artifactDataList = new List<ArtifactData>(selection.Length);
      for (int index = 0; index < selection.Length; ++index)
      {
        if (selection[index] is ArtifactData && !artifactDataList.Contains(selection[index] as ArtifactData))
          artifactDataList.Add(selection[index] as ArtifactData);
      }
      return artifactDataList.ToArray();
    }

    private long CalcDecomposeCost(ArtifactData[] artifacts)
    {
      long num = 0;
      for (int index = 0; index < artifacts.Length; ++index)
      {
        if (artifacts[index].Kakera != null)
          num += (long) (int) artifacts[index].RarityParam.ArtifactChangeCost;
      }
      return num;
    }

    public void DecomposeArtifact()
    {
      if (this.IsBusy || this.mSelectedArtifacts == null && this.mSelectedArtifacts.Length < 1)
        return;
      int num = 0;
      for (int index = 0; index < this.mSelectedArtifacts.Length; ++index)
      {
        ArtifactData selectedArtifact = (ArtifactData) this.mSelectedArtifacts[index];
        if (selectedArtifact == null || (long) selectedArtifact.UniqueID == 0L)
          return;
        if (selectedArtifact.Kakera == null)
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_DECOMPOSE_NODEC"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
          return;
        }
        num += Math.Abs((int) selectedArtifact.RarityParam.ArtifactChangeCost);
      }
      if (MonoSingleton<GameManager>.Instance.Player.Gold < num)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_DECOMPOSE_NOGOLD"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        this.mConfirmDialogGo = UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_DECOMPOSE_CONFIRM"), new UIUtility.DialogResultEvent(this.OnDecomposeRarityCheck), new UIUtility.DialogResultEvent(this.OnDecomposeCancel), (GameObject) null, false, -1, (string) null, (string) null);
        if (!((UnityEngine.Object) this.mConfirmDialogGo != (UnityEngine.Object) null))
          return;
        Win_Btn_DecideCancel_FL_C component = this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
        if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
          return;
        component.AutoClose = false;
      }
    }

    private void OnDecomposeRarityCheck(GameObject go)
    {
      bool flag = false;
      ArtifactData[] selectedArtifacts = this.GetSelectedArtifacts();
      for (int index = 0; index < selectedArtifacts.Length; ++index)
      {
        if ((int) selectedArtifacts[index].Rarity + 1 >= this.RarityCheckValue)
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        GameObject original = AssetManager.Load<GameObject>(ArtifactWindow.ARTIFACT_RARITY_CHECK_UI_PATH);
        if ((UnityEngine.Object) original != (UnityEngine.Object) null)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
          if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
          {
            ArtifactRarityCheck componentInChildren = gameObject.GetComponentInChildren<ArtifactRarityCheck>();
            if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
            {
              componentInChildren.Setup(ArtifactRarityCheck.Type.DECOMPOSE, go, selectedArtifacts, this.RarityCheckValue);
              componentInChildren.OnDecideEvent = new ArtifactRarityCheck.OnArtifactRarityCheckDecideEvent(this.OnDecomposeAccept);
              componentInChildren.OnCancelEvent = new ArtifactRarityCheck.OnArtifactRarityCheckCancelEvent(this.OnDecomposeCancel);
              return;
            }
          }
        }
      }
      this.OnDecomposeAccept(go);
    }

    private void OnDecomposeAccept(GameObject go)
    {
      if ((UnityEngine.Object) this.mConfirmDialogGo != (UnityEngine.Object) null)
      {
        Win_Btn_DecideCancel_FL_C component = this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null && !component.AutoClose)
          component.BeginClose();
      }
      GameManager instance = MonoSingleton<GameManager>.Instance;
      bool flag = false;
      for (int index = 0; index < this.mSelectedArtifacts.Length; ++index)
      {
        ArtifactData selectedArtifact = (ArtifactData) this.mSelectedArtifacts[index];
        UnitData unit;
        JobData job;
        if (instance.Player.FindOwner(selectedArtifact, out unit, out job))
        {
          flag = true;
          break;
        }
      }
      if (flag)
        UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_DECOMPOSE_CONFIRM2"), new UIUtility.DialogResultEvent(this.OnDecomposeAccept2), new UIUtility.DialogResultEvent(this.OnDecomposeCancel), (GameObject) null, false, -1, (string) null, (string) null);
      else
        this.OnDecomposeAccept2(go);
    }

    private void OnDecomposeAccept2(GameObject go)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int num = 0;
      for (int index = 0; index < this.mSelectedArtifacts.Length; ++index)
      {
        ArtifactData selectedArtifact = (ArtifactData) this.mSelectedArtifacts[index];
        num += Math.Abs((int) selectedArtifact.RarityParam.ArtifactChangeCost);
      }
      instance.Player.GainGold(-num);
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) this);
      if (!string.IsNullOrEmpty(this.DecomposeSE))
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.DecomposeSE, 0.0f);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 402);
      if (Network.Mode == Network.EConnectMode.Online)
      {
        long[] artifact_iids;
        if (this.mSelectedArtifacts != null && this.mSelectedArtifacts.Length > 0)
        {
          artifact_iids = new long[this.mSelectedArtifacts.Length];
          for (int index = 0; index < artifact_iids.Length; ++index)
            artifact_iids[index] = (long) ((ArtifactData) this.mSelectedArtifacts[index]).UniqueID;
        }
        else
          artifact_iids = new long[1]
          {
            (long) this.mCurrentArtifact.UniqueID
          };
        Network.RequestAPI((WebAPI) new ReqArtifactConvert(artifact_iids, new Network.ResponseCallback(this.OnDecomposeResult)), false);
      }
      else
      {
        Dictionary<ItemParam, int> itemSnapshot = instance.Player.CreateItemSnapshot();
        instance.Player.GainItem(this.mCurrentArtifact.Kakera.iname, (int) this.mCurrentArtifact.RarityParam.ArtifactChangePieceNum);
        this.CreateItemChangeSet(itemSnapshot);
        this.ShowDecomposeResult();
        for (int index = 0; index < instance.Player.Artifacts.Count; ++index)
        {
          if ((long) instance.Player.Artifacts[index].UniqueID == (long) this.mCurrentArtifact.UniqueID)
          {
            instance.Player.Artifacts.RemoveAt(index);
            break;
          }
        }
      }
    }

    private void OnDecomposeCancel(GameObject go)
    {
      if (!((UnityEngine.Object) this.mConfirmDialogGo != (UnityEngine.Object) null))
        return;
      Win_Btn_DecideCancel_FL_C component = this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null) || component.AutoClose)
        return;
      component.BeginClose();
    }

    private void OnDecomposeResult(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqArtifactConvert.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqArtifactConvert.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          Dictionary<ItemParam, int> itemSnapshot = instance.Player.CreateItemSnapshot();
          try
          {
            instance.Deserialize(jsonObject.body.player);
            instance.Deserialize(jsonObject.body.units);
            instance.Deserialize(jsonObject.body.items);
            if (jsonObject.body.iids != null)
            {
              for (int index = 0; index < jsonObject.body.iids.Length; ++index)
                instance.Player.RemoveArtifactByUniqueID(jsonObject.body.iids[index]);
            }
            else if (jsonObject.body.artifacts != null)
              instance.Deserialize(jsonObject.body.artifacts, false);
            instance.Player.UpdateArtifactOwner();
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Retry();
            return;
          }
          Network.RemoveAPI();
          this.CreateItemChangeSet(itemSnapshot);
          this.ShowDecomposeResult();
        }
      }
    }

    private void ShowDecomposeResult()
    {
      this.StartCoroutine(this.ShowDecomposeResultAsync());
    }

    [DebuggerHidden]
    private IEnumerator ShowDecomposeResultAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ArtifactWindow.\u003CShowDecomposeResultAsync\u003Ec__Iterator2() { \u0024this = this };
    }

    public void SellArtifacts()
    {
      if (this.IsBusy)
        return;
      if ((UnityEngine.Object) this.ArtifactList == (UnityEngine.Object) null)
        DebugUtility.LogWarning("ArtifactList is not set");
      else if (this.GetSelectedArtifacts().Length <= 0)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_NOTHING2SELL"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        this.mConfirmDialogGo = UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_SELL_CONFIRM"), new UIUtility.DialogResultEvent(this.OnSellRarityCheck), new UIUtility.DialogResultEvent(this.OnSellCancel), (GameObject) null, false, -1, (string) null, (string) null);
        if (!((UnityEngine.Object) this.mConfirmDialogGo != (UnityEngine.Object) null))
          return;
        Win_Btn_DecideCancel_FL_C component = this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
        if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
          return;
        component.AutoClose = false;
      }
    }

    private void OnSellRarityCheck(GameObject go)
    {
      bool flag = false;
      ArtifactData[] selectedArtifacts = this.GetSelectedArtifacts();
      for (int index = 0; index < selectedArtifacts.Length; ++index)
      {
        if ((int) selectedArtifacts[index].Rarity + 1 >= this.RarityCheckValue)
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        GameObject original = AssetManager.Load<GameObject>(ArtifactWindow.ARTIFACT_RARITY_CHECK_UI_PATH);
        if ((UnityEngine.Object) original != (UnityEngine.Object) null)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
          if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
          {
            ArtifactRarityCheck componentInChildren = gameObject.GetComponentInChildren<ArtifactRarityCheck>();
            if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
            {
              componentInChildren.Setup(ArtifactRarityCheck.Type.SELL, go, selectedArtifacts, this.RarityCheckValue);
              componentInChildren.OnDecideEvent = new ArtifactRarityCheck.OnArtifactRarityCheckDecideEvent(this.OnSellAccept);
              componentInChildren.OnCancelEvent = new ArtifactRarityCheck.OnArtifactRarityCheckCancelEvent(this.OnSellCancel);
              return;
            }
          }
        }
      }
      this.OnSellAccept(go);
    }

    private void OnSellAccept(GameObject go)
    {
      if ((UnityEngine.Object) this.mConfirmDialogGo != (UnityEngine.Object) null)
      {
        Win_Btn_DecideCancel_FL_C component = this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null && !component.AutoClose)
          component.BeginClose();
      }
      ArtifactData[] selectedArtifacts = this.GetSelectedArtifacts();
      bool flag = false;
      for (int index = 0; index < selectedArtifacts.Length; ++index)
      {
        UnitData unit = (UnitData) null;
        JobData job = (JobData) null;
        if (MonoSingleton<GameManager>.Instance.Player.FindOwner(selectedArtifacts[index], out unit, out job))
        {
          flag = true;
          break;
        }
      }
      if (flag)
        UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_SELL_CONFIRM2"), new UIUtility.DialogResultEvent(this.OnSellAccept2), new UIUtility.DialogResultEvent(this.OnSellCancel), (GameObject) null, false, -1, (string) null, (string) null);
      else
        this.OnSellAccept2(go);
    }

    private void OnSellAccept2(GameObject go)
    {
      ArtifactData[] selectedArtifacts = this.GetSelectedArtifacts();
      if (!string.IsNullOrEmpty(this.SellSE))
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.SellSE, 0.0f);
      long[] artifact_iids = new long[selectedArtifacts.Length];
      for (int index = 0; index < selectedArtifacts.Length; ++index)
        artifact_iids[index] = (long) selectedArtifacts[index].UniqueID;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 601);
      if (Network.Mode == Network.EConnectMode.Online)
      {
        Network.RequestAPI((WebAPI) new ReqArtifactSell(artifact_iids, new Network.ResponseCallback(this.OnSellResult)), false);
      }
      else
      {
        CurrencyTracker track = new CurrencyTracker();
        MonoSingleton<GameManager>.Instance.Player.OfflineSellArtifacts(selectedArtifacts);
        track.EndTracking();
        List<ChangeListData> changeset = new List<ChangeListData>(4);
        this.AddCurrencyChangeSet(track, changeset);
        this.mChangeSet = changeset;
        this.ShowSellResult();
      }
    }

    private void OnSellCancel(GameObject go)
    {
      if (!((UnityEngine.Object) this.mConfirmDialogGo != (UnityEngine.Object) null))
        return;
      Win_Btn_DecideCancel_FL_C component = this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null) || component.AutoClose)
        return;
      component.BeginClose();
    }

    private void OnSellResult(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        FlowNode_Network.Retry();
      }
      else
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        CurrencyTracker track = new CurrencyTracker();
        WebAPI.JSON_BodyResponse<ReqArtifactSell.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqArtifactSell.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          try
          {
            instance.Deserialize(jsonObject.body.player);
            instance.Deserialize(jsonObject.body.units);
            if (jsonObject.body.sells != null)
            {
              for (int index = 0; index < jsonObject.body.sells.Length; ++index)
                instance.Player.RemoveArtifactByUniqueID(jsonObject.body.sells[index]);
            }
            else if (jsonObject.body.artifacts != null)
              instance.Deserialize(jsonObject.body.artifacts, false);
            instance.Player.UpdateArtifactOwner();
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Retry();
            return;
          }
          Network.RemoveAPI();
          track.EndTracking();
          List<ChangeListData> changeset = new List<ChangeListData>(4);
          this.AddCurrencyChangeSet(track, changeset);
          this.mChangeSet = changeset;
          this.ShowSellResult();
        }
      }
    }

    private void ShowSellResult()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 602);
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) this);
      if ((UnityEngine.Object) this.SellResult != (UnityEngine.Object) null)
      {
        ChangeList componentInChildren = UnityEngine.Object.Instantiate<GameObject>(this.SellResult).GetComponentInChildren<ChangeList>();
        if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
          componentInChildren.SetData(this.mChangeSet.ToArray());
      }
      this.mChangeSet = (List<ChangeListData>) null;
      if (!((UnityEngine.Object) this.ArtifactList != (UnityEngine.Object) null))
        return;
      this.ArtifactList.ClearSelection();
      this.ArtifactList.Refresh();
    }

    private void AddCurrencyChangeSet(CurrencyTracker track, List<ChangeListData> changeset)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (track.Gold != 0)
        changeset.Add(new ChangeListData()
        {
          ItemID = 1145851719,
          ValOld = (instance.Player.Gold - track.Gold).ToString(),
          ValNew = instance.Player.Gold.ToString()
        });
      if (track.Coin != 0)
        changeset.Add(new ChangeListData()
        {
          ItemID = 1313427267,
          ValOld = (instance.Player.Coin - track.Coin).ToString(),
          ValNew = instance.Player.Coin.ToString()
        });
      if (track.MultiCoin != 0)
        changeset.Add(new ChangeListData()
        {
          ItemID = 1313817421,
          ValOld = (instance.Player.MultiCoin - track.MultiCoin).ToString(),
          ValNew = instance.Player.MultiCoin.ToString()
        });
      if (track.ArenaCoin == 0)
        return;
      changeset.Add(new ChangeListData()
      {
        ItemID = 1313817409,
        ValOld = (instance.Player.ArenaCoin - track.ArenaCoin).ToString(),
        ValNew = instance.Player.ArenaCoin.ToString()
      });
    }

    private void CreateItemChangeSet(Dictionary<ItemParam, int> snapshot)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      this.mChangeSet = new List<ChangeListData>();
      List<ItemData> items = instance.Player.Items;
      for (int index = 0; index < items.Count; ++index)
      {
        int num = 0;
        int numNonCap = items[index].NumNonCap;
        if (snapshot.ContainsKey(items[index].Param))
          num = snapshot[items[index].Param];
        if (num != numNonCap)
          this.mChangeSet.Add(new ChangeListData()
          {
            ItemID = this.ItemDataID,
            MetaData = (object) items[index].Param,
            MetaDataType = typeof (ItemParam),
            ValOld = num.ToString(),
            ValNew = numNonCap.ToString()
          });
      }
    }

    public void SetOwnerUnit(UnitData unit, int jobIndex)
    {
      this.mOwnerUnitData = unit;
      this.mOwnerUnitJobIndex = jobIndex;
    }

    public void EquipArtifact()
    {
      if (this.IsBusy || (UnityEngine.Object) this.ArtifactList == (UnityEngine.Object) null)
        return;
      ArtifactData[] selectedArtifacts = this.GetSelectedArtifacts();
      if (selectedArtifacts.Length <= 0)
      {
        this.EndEquip();
      }
      else
      {
        UnitData unit;
        JobData job;
        bool owner = MonoSingleton<GameManager>.Instance.Player.FindOwner(selectedArtifacts[0], out unit, out job);
        if (owner && unit.UniqueID == this.mOwnerUnitData.UniqueID && this.mOwnerUnitData.Jobs[this.mOwnerUnitJobIndex].UniqueID == job.UniqueID)
          this.EndEquip();
        else if (this.mOwnerUnitData != null && !selectedArtifacts[0].CheckEnableEquip(this.mOwnerUnitData, this.mOwnerUnitJobIndex))
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_CANT_EQUIP"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        else if (owner && (unit.UniqueID != this.mOwnerUnitData.UniqueID || this.mOwnerUnitData.Jobs[this.mOwnerUnitJobIndex].UniqueID != job.UniqueID))
          UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_EQUIP_CONFIRM", (object) unit.UnitParam.name, (object) job.Name), new UIUtility.DialogResultEvent(this.OnEquipArtifactAccept), new UIUtility.DialogResultEvent(this.OnEquipArtifactCancel), (GameObject) null, false, -1, (string) null, (string) null);
        else
          this.OnEquipArtifactAccept((GameObject) null);
      }
    }

    private void OnEquipArtifactAccept(GameObject go)
    {
      ArtifactData[] selectedArtifacts = this.GetSelectedArtifacts();
      if (selectedArtifacts.Length < 0)
        return;
      this.OnEquip(selectedArtifacts[0], this.SelectArtifactSlot);
      this.EndEquip();
    }

    private void EndEquip()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 500);
    }

    private void OnEquipArtifactCancel(GameObject go)
    {
    }

    public void UnequipArtifact()
    {
      if (this.IsBusy)
        return;
      bool flag = false;
      if (this.mOwnerUnitData != null)
      {
        for (int index = 0; index < this.mOwnerUnitData.Jobs[this.mOwnerUnitJobIndex].Artifacts.Length; ++index)
        {
          if (this.mOwnerUnitData.Jobs[this.mOwnerUnitJobIndex].Artifacts[index] != 0L)
          {
            flag = true;
            break;
          }
        }
      }
      if (flag && this.OnEquip != null)
        this.OnEquip((ArtifactData) null, this.SelectArtifactSlot);
      this.EndEquip();
    }

    public void ShowDetail()
    {
      if ((UnityEngine.Object) this.mDetailWindow != (UnityEngine.Object) null || this.mCurrentArtifact == null || (UnityEngine.Object) this.DetailWindow == (UnityEngine.Object) null)
        return;
      this.StartCoroutine(this.ShowDetailAsync());
    }

    [DebuggerHidden]
    private IEnumerator ShowDetailAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ArtifactWindow.\u003CShowDetailAsync\u003Ec__Iterator3() { \u0024this = this };
    }

    private bool OnSceneCHange()
    {
      this.mSceneChanging = true;
      this.FlushRequests(false);
      if (this.gameObject.activeInHierarchy)
        MonoSingleton<GameManager>.Instance.RegisterImportantJob(this.StartCoroutine(this.OnSceneChangeAsync()));
      return true;
    }

    [DebuggerHidden]
    private IEnumerator OnSceneChangeAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ArtifactWindow.\u003COnSceneChangeAsync\u003Ec__Iterator4() { \u0024this = this };
    }

    private void SyncArtifactData()
    {
      if (this.mCurrentArtifact == null || (long) this.mCurrentArtifact.UniqueID == 0L)
        return;
      ArtifactData artifactByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID((long) this.mCurrentArtifact.UniqueID);
      if (artifactByUniqueId == null)
        return;
      this.mCurrentArtifact = artifactByUniqueId.Copy();
      this.mCurrentArtifactParam = this.mCurrentArtifact.ArtifactParam;
      this.Rebind();
    }

    private void UpdateArtifactOwner()
    {
      this.mCurrentArtifactOwner = (UnitData) null;
      this.mCurrentArtifactOwnerJob = (JobData) null;
      if ((UnityEngine.Object) this.ArtifactOwnerSlot == (UnityEngine.Object) null)
        return;
      if (this.mCurrentArtifact == null)
      {
        this.ArtifactOwnerSlot.SetSlotData<UnitData>((UnitData) null);
      }
      else
      {
        UnitData unit;
        JobData job;
        if (MonoSingleton<GameManager>.GetInstanceDirect().Player.FindOwner(this.mCurrentArtifact, out unit, out job))
        {
          this.mCurrentArtifactOwner = unit;
          this.mCurrentArtifactOwnerJob = job;
        }
        this.ArtifactOwnerSlot.SetSlotData<UnitData>(this.mCurrentArtifactOwner);
      }
    }

    private void Rebind()
    {
      this.UpdateArtifactOwner();
      if ((UnityEngine.Object) this.ArtifactSlot != (UnityEngine.Object) null)
        this.ArtifactSlot.SetSlotData<ArtifactData>(this.mCurrentArtifact);
      if ((UnityEngine.Object) this.artiProssessButton != (UnityEngine.Object) null)
      {
        if (this.mCurrentArtifact != null)
          this.artiProssessButton.interactable = true;
        else
          this.artiProssessButton.interactable = false;
      }
      if ((UnityEngine.Object) this.WindowBody != (UnityEngine.Object) null)
      {
        DataSource.Bind<ArtifactData>(this.WindowBody, this.mCurrentArtifact, false);
        DataSource.Bind<UnitData>(this.WindowBody, this.mCurrentArtifactOwner, false);
        DataSource.Bind<JobData>(this.WindowBody, this.mCurrentArtifactOwnerJob, false);
      }
      if ((UnityEngine.Object) this.WindowBodyAdd != (UnityEngine.Object) null)
      {
        DataSource.Bind<ArtifactData>(this.WindowBodyAdd, this.mCurrentArtifact, false);
        DataSource.Bind<UnitData>(this.WindowBodyAdd, this.mCurrentArtifactOwner, false);
        DataSource.Bind<JobData>(this.WindowBodyAdd, this.mCurrentArtifactOwnerJob, false);
      }
      if (!((UnityEngine.Object) this.m_SetEffectsButton != (UnityEngine.Object) null) || this.mCurrentArtifact == null)
        return;
      this.m_SetEffectsButton.interactable = MonoSingleton<GameManager>.Instance.MasterParam.ExistSkillAbilityDeriveDataWithArtifact(this.mCurrentArtifact.ArtifactParam.iname);
      if ((UnityEngine.Object) this.m_SetEffectsButtonAdd != (UnityEngine.Object) null)
        this.m_SetEffectsButtonAdd.interactable = MonoSingleton<GameManager>.Instance.MasterParam.ExistSkillAbilityDeriveDataWithArtifact(this.mCurrentArtifact.ArtifactParam.iname);
      ArtifactSetList.SetSelectedArtifactParam(this.mCurrentArtifact.ArtifactParam);
    }

    private bool IsBusy
    {
      get
      {
        if (!this.mSceneChanging)
          return this.mBusy;
        return true;
      }
    }

    public void SetArtifactData()
    {
      if (this.mCurrentArtifact == null)
        return;
      GlobalVars.ConditionJobs = this.mCurrentArtifact.ArtifactParam.condition_jobs;
    }

    public void OnExpMaxOpen(SRPG_Button button)
    {
      if ((int) this.mCurrentArtifact.Lv >= this.mCurrentArtifact.GetLevelCap())
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.LEVEL_CAPPED"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        GameObject original = AssetManager.Load<GameObject>(ArtifactWindow.ARTIFACT_EXPMAX_UI_PATH);
        if (!((UnityEngine.Object) original != (UnityEngine.Object) null))
          return;
        GameObject root = UnityEngine.Object.Instantiate<GameObject>(original);
        if (!((UnityEngine.Object) root != (UnityEngine.Object) null))
          return;
        ArtifactLevelUpWindow componentInChildren = root.GetComponentInChildren<ArtifactLevelUpWindow>();
        if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
          componentInChildren.OnDecideEvent = new ArtifactLevelUpWindow.OnArtifactLevelupEvent(this.OnArtifactBulkLevelUp);
        DataSource.Bind<ArtifactData>(root, this.mCurrentArtifact, false);
        DataSource.Bind<ArtifactWindow>(root, this, false);
        GameParameter.UpdateAll(root);
      }
    }

    public void OnExcludeEquipedValueChanged(bool isOn)
    {
      if (!((UnityEngine.Object) this.ArtifactList != (UnityEngine.Object) null))
        return;
      this.WriteExcludeEquipedSettingValue(isOn);
      this.ArtifactList.ExcludeEquiped = isOn;
      this.ArtifactList.Refresh();
    }

    private bool ReadExcludeEquipedSettingValue()
    {
      return PlayerPrefsUtility.GetInt(PlayerPrefsUtility.ARTIFACT_EXCLUDE_EQUIPED, 0) != 0;
    }

    private void WriteExcludeEquipedSettingValue(bool isOn)
    {
      int num = isOn ? 1 : 0;
      PlayerPrefsUtility.SetInt(PlayerPrefsUtility.ARTIFACT_EXCLUDE_EQUIPED, num, false);
    }

    public void RefreshInspSkillEnhance()
    {
      this.m_SelectedArtifactList.Clear();
      this.SyncArtifactData();
      this.m_CurrentInspSkill = this.mCurrentArtifact.InspirationSkillList.Find((Predicate<InspirationSkillData>) (select => (long) select.UniqueID == (long) this.m_CurrentInspSkill.UniqueID));
      if ((UnityEngine.Object) this.m_InspSkillEnhanceConfirmButton != (UnityEngine.Object) null)
        this.m_InspSkillEnhanceConfirmButton.interactable = false;
      if ((UnityEngine.Object) this.m_InspSkillEnhanceStatus != (UnityEngine.Object) null)
      {
        ArtifactData data = MonoSingleton<GameManager>.Instance.Player.Artifacts.Find((Predicate<ArtifactData>) (select => (long) select.UniqueID == (long) this.mCurrentArtifact.UniqueID));
        DataSource.Bind<ArtifactData>(this.m_InspSkillEnhanceStatus, data, false);
        DataSource.Bind<ArtifactParam>(this.m_InspSkillEnhanceStatus, data.ArtifactParam, false);
        GameParameter.UpdateAll(this.m_InspSkillEnhanceStatus);
      }
      if ((UnityEngine.Object) this.m_InspSkillEnhanceCost != (UnityEngine.Object) null)
        this.m_InspSkillEnhanceCost.text = "-";
      this.RefreshInspSkillEnhanceList();
      DataSource.Bind<AbilityData>(this.m_InspSkillEnhanceBeforePanel, (AbilityData) null, false);
      GameParameter.UpdateAll(this.m_InspSkillEnhanceBeforePanel);
      DataSource.Bind<AbilityData>(this.m_InspSkillEnhanceBeforePanel, MonoSingleton<GameManager>.Instance.Player.Artifacts.Find((Predicate<ArtifactData>) (select => (long) select.UniqueID == (long) this.mCurrentArtifact.UniqueID)).InspirationSkillList.Find((Predicate<InspirationSkillData>) (select => (long) select.UniqueID == (long) this.m_CurrentInspSkill.UniqueID)).AbilityData, false);
      this.m_InspSkillEnhanceBeforePanel.GetComponent<Animator>().SetInteger(this.AbilityListItemState, this.AbilityListItem_Unlocked);
      DataSource.Bind<AbilityData>(this.m_InspSkillEnhanceAfterPanel, (AbilityData) null, false);
      this.m_InspSkillEnhanceAfterPanel.GetComponent<Animator>().SetInteger(this.AbilityListItemState, this.AbilityListItem_Unlocked);
      GameParameter.UpdateAll(this.m_InspSkillEnhanceBeforePanel);
      GameParameter.UpdateAll(this.m_InspSkillEnhanceAfterPanel);
    }

    public void RefreshInspSkill(bool resetmode = false)
    {
      this.m_SelectedArtifactList.Clear();
      this.m_CurrentInspSkill = (InspirationSkillData) null;
      this.SyncArtifactData();
      this.RefreshInspSkillList(resetmode);
    }

    public void RefreshInspSkillList(bool resetmode = false)
    {
      if ((UnityEngine.Object) this.m_InspSkillList == (UnityEngine.Object) null | (UnityEngine.Object) this.m_InspSkillListItem == (UnityEngine.Object) null)
        return;
      ArtifactData artifactData = MonoSingleton<GameManager>.Instance.Player.Artifacts.Find((Predicate<ArtifactData>) (select => (long) select.UniqueID == (long) this.mCurrentArtifact.UniqueID));
      List<InspirationSkillData> inspirationSkillList = artifactData.InspirationSkillList;
      int inspirationSkillSlotNum = (int) artifactData.InspirationSkillSlotNum;
      int index1 = 0;
      int inspirationSkillSlotMax = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.InspirationSkillSlotMax;
      for (int index2 = 0; index2 < inspirationSkillSlotMax; ++index2)
      {
        GameObject root;
        if (this.m_InspSkillItems.Count <= index1)
        {
          root = UnityEngine.Object.Instantiate<GameObject>(this.m_InspSkillListItem);
          if ((UnityEngine.Object) root != (UnityEngine.Object) null)
          {
            this.m_InspSkillItems.Add(root);
            root.transform.SetParent(this.m_InspSkillList, false);
            SerializeValueBehaviour component = root.GetComponent<SerializeValueBehaviour>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            {
              SerializeValueList list = component.list;
              if (list != null)
              {
                list.GetUIButton("enhance").AddClickListener(new ButtonExt.ButtonClickEvent(this.OnSelectEnhanceInspSkill));
                list.GetUIButton("equip").AddClickListener(new ButtonExt.ButtonClickEvent(this.OnSelectEquipInspSkill));
                list.GetUIButton("reset").AddClickListener(new ButtonExt.ButtonClickEvent(this.OnSelectResetInspSkill));
                list.GetUIButton("unlock").AddClickListener(new ButtonExt.ButtonClickEvent(this.OnSelectUnlockInspSkill));
              }
            }
          }
        }
        else
          root = this.m_InspSkillItems[index2];
        DataSource.Bind<InspirationSkillData>(root, (InspirationSkillData) null, false);
        DataSource.Bind<AbilityData>(root, (AbilityData) null, false);
        GameParameter.UpdateAll(root);
        root.SetActive(false);
        ArtifactWindow.InspSkillState inspSkillState = ArtifactWindow.InspSkillState.LOCK;
        if (inspirationSkillList.Count > index2)
          inspSkillState = ArtifactWindow.InspSkillState.LEANED;
        else if (index2 < inspirationSkillSlotNum)
          inspSkillState = ArtifactWindow.InspSkillState.UNLOCK;
        GameObject gameObject1 = (GameObject) null;
        Button button = (Button) null;
        GameObject gameObject2 = (GameObject) null;
        GameObject gameObject3 = (GameObject) null;
        GameObject gameObject4 = (GameObject) null;
        GameObject gameObject5 = (GameObject) null;
        GameObject gameObject6 = (GameObject) null;
        Text text1 = (Text) null;
        Text text2 = (Text) null;
        GameObject gameObject7 = (GameObject) null;
        GameObject gameObject8 = (GameObject) null;
        SerializeValueBehaviour component1 = root.GetComponent<SerializeValueBehaviour>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
        {
          SerializeValueList list = component1.list;
          if (list != null)
          {
            gameObject1 = list.GetGameObject("enhancebtn");
            GameObject gameObject9 = list.GetGameObject("enhance_content");
            button = list.GetUIButton("enhance");
            gameObject2 = list.GetGameObject("resetbtn");
            gameObject3 = list.GetGameObject("lockobj");
            gameObject4 = list.GetGameObject("lockbtn");
            gameObject5 = list.GetGameObject("openobj");
            text1 = list.GetUILabel("unlock_cost");
            text2 = list.GetUILabel("reset_cost");
            gameObject7 = list.GetGameObject("unlock_cost_icon");
            gameObject8 = list.GetGameObject("reset_cost_icon");
            gameObject6 = list.GetGameObject("badge");
            gameObject6.SetActive(false);
            gameObject1.SetActive(true);
            gameObject9.SetActive(false);
            gameObject9.SetActive(true);
            button.interactable = false;
          }
        }
        if (inspSkillState == ArtifactWindow.InspSkillState.LEANED)
        {
          gameObject3.SetActive(false);
          gameObject4.SetActive(false);
          gameObject5.SetActive(false);
          gameObject1.SetActive(!resetmode);
          gameObject2.SetActive(resetmode);
          DataSource.Bind<InspirationSkillData>(root, inspirationSkillList[index2], false);
          gameObject6.SetActive((bool) inspirationSkillList[index2].IsSet);
          button.interactable = (int) inspirationSkillList[index2].Lv < inspirationSkillList[index2].InspSkillParam.LvCap;
          InspSkillParam inspSkillParam = inspirationSkillList[index2].InspSkillParam;
          if (inspSkillParam != null)
          {
            InspSkillCostParam resetCost = inspSkillParam.GetResetCost();
            text2.text = resetCost.Num.ToString();
            ImageArray component2 = gameObject8.GetComponent<ImageArray>();
            if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
            {
              int type = (int) resetCost.Type;
              component2.ImageIndex = type > component2.Images.Length ? 0 : type;
            }
          }
          AbilityData abilityData = inspirationSkillList[index2].AbilityData;
          DataSource.Bind<AbilityData>(root, abilityData, false);
          root.SetActive(true);
          GameParameter.UpdateAll(root);
          ++index1;
        }
        else
        {
          bool flag = inspSkillState == ArtifactWindow.InspSkillState.UNLOCK;
          gameObject3.SetActive(!flag);
          gameObject4.SetActive(!flag);
          gameObject5.SetActive(flag);
          gameObject1.SetActive(false);
          gameObject2.SetActive(false);
          if (!flag)
          {
            InspSkillCostParam inspSkillOpenCost = MonoSingleton<GameManager>.Instance.MasterParam.GetInspSkillOpenCost(artifactData.GetNextOpenInspSlot());
            text1.text = inspSkillOpenCost.Num.ToString();
            ImageArray component2 = gameObject7.GetComponent<ImageArray>();
            if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
            {
              int type = (int) inspSkillOpenCost.Type;
              component2.ImageIndex = type > component2.Images.Length ? 0 : type;
            }
          }
          root.SetActive(true);
          ++index1;
        }
      }
      if (index1 > inspirationSkillSlotNum)
      {
        index1 = inspirationSkillSlotNum <= 0 ? 1 : inspirationSkillSlotNum;
        if (index1 + 1 <= (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.InspirationSkillSlotMax)
          ++index1;
      }
      for (; index1 < this.m_InspSkillItems.Count; ++index1)
        this.m_InspSkillItems[index1].SetActive(false);
    }

    public void RefreshInspSkillEnhanceList()
    {
      if ((UnityEngine.Object) this.m_InspSkillEnhanceList == (UnityEngine.Object) null || (UnityEngine.Object) this.m_InspSkillEnhanceListItem == (UnityEngine.Object) null)
        return;
      this.m_SelectedArtifactList.Clear();
      List<ArtifactData> skillLvUpArtifacts = MonoSingleton<GameManager>.Instance.Player.GetInspirationSkillLvUpArtifacts(this.mCurrentArtifact, this.mCurrentArtifact.ArtifactParam.iname, this.m_CurrentInspSkill.InspSkillParam.Iname);
      int index1 = 0;
      InspirationSkillData inspirationSkillData = MonoSingleton<GameManager>.Instance.Player.Artifacts.Find((Predicate<ArtifactData>) (select => (long) select.UniqueID == (long) this.mCurrentArtifact.UniqueID)).InspirationSkillList.Find((Predicate<InspirationSkillData>) (select => (long) select.UniqueID == (long) this.m_CurrentInspSkill.UniqueID));
      for (int index2 = 0; index2 < skillLvUpArtifacts.Count; ++index2)
      {
        GameObject root;
        if (this.m_InspSkillEnhanceItems.Count <= index1)
        {
          root = UnityEngine.Object.Instantiate<GameObject>(this.m_InspSkillEnhanceListItem);
          if ((UnityEngine.Object) root != (UnityEngine.Object) null)
          {
            this.m_InspSkillEnhanceItems.Add(root);
            root.transform.SetParent(this.m_InspSkillEnhanceList, false);
            ListItemEvents component = root.GetComponent<ListItemEvents>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            {
              component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectEnhanceListItem);
              component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenInspirationSkillDetail);
            }
          }
        }
        else
          root = this.m_InspSkillEnhanceItems[index2];
        bool flag = (int) inspirationSkillData.Lv >= inspirationSkillData.InspSkillParam.LvCap;
        SerializeValueBehaviour component1 = root.GetComponent<SerializeValueBehaviour>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
        {
          GameObject gameObject1 = component1.list.GetGameObject("select");
          GameObject gameObject2 = component1.list.GetGameObject("mask");
          gameObject1.SetActive(false);
          gameObject2.SetActive(skillLvUpArtifacts[index2].IsEquipped() || skillLvUpArtifacts[index2].IsFavorite || flag);
        }
        DataSource.Bind<ArtifactData>(root, skillLvUpArtifacts[index2], false);
        root.SetActive(true);
        GameParameter.UpdateAll(root);
        ++index1;
      }
      for (; index1 < this.m_InspSkillEnhanceItems.Count; ++index1)
        this.m_InspSkillEnhanceItems[index1].SetActive(false);
      this.m_InspSkillEnhanceListNotItem.SetActive(skillLvUpArtifacts.Count <= 0);
    }

    private void RefreshInspSkillEnhanceListNotSelect(bool active = true)
    {
      if (this.m_InspSkillEnhanceItems == null || this.m_InspSkillEnhanceItems.Count <= 0)
        return;
      for (int index = 0; index < this.m_InspSkillEnhanceItems.Count; ++index)
      {
        GameObject skillEnhanceItem = this.m_InspSkillEnhanceItems[index];
        if (skillEnhanceItem.activeInHierarchy)
        {
          ArtifactData item_artifact = DataSource.FindDataOfClass<ArtifactData>(skillEnhanceItem, (ArtifactData) null);
          if (item_artifact != null && !item_artifact.IsEquipped() && (!item_artifact.IsFavorite && this.m_SelectedArtifactList.FindIndex((Predicate<ArtifactData>) (select => (long) select.UniqueID == (long) item_artifact.UniqueID)) < 0))
          {
            SerializeValueBehaviour component = skillEnhanceItem.GetComponent<SerializeValueBehaviour>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.list.GetGameObject("mask").SetActive(active);
          }
        }
      }
    }

    private void OnSelectEnhanceInspSkill(GameObject go)
    {
      InspirationSkillData dataOfClass = DataSource.FindDataOfClass<InspirationSkillData>(go, (InspirationSkillData) null);
      if (dataOfClass != null)
        this.m_CurrentInspSkill = dataOfClass;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 660);
    }

    private void OnSelectEquipInspSkill(GameObject go)
    {
      InspirationSkillData dataOfClass = DataSource.FindDataOfClass<InspirationSkillData>(go, (InspirationSkillData) null);
      if (dataOfClass != null)
        this.m_CurrentInspSkill = dataOfClass;
      DataSource.Bind<InspirationSkillData>(this.m_InspSkillEquipConfirm, dataOfClass, false);
      DataSource.Bind<AbilityData>(this.m_InspSkillEquipConfirm, dataOfClass.AbilityData, false);
      GameParameter.UpdateAll(this.m_InspSkillEquipConfirm);
      GlobalVars.TargetInspSkill = dataOfClass;
      GlobalVars.TargetInspSkillArtifact = this.mCurrentArtifact;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 661);
    }

    private void OnSelectResetInspSkill(GameObject go)
    {
      InspirationSkillData dataOfClass = DataSource.FindDataOfClass<InspirationSkillData>(go, (InspirationSkillData) null);
      if (dataOfClass != null)
        this.m_CurrentInspSkill = dataOfClass;
      InspSkillCostParam resetCost = dataOfClass.InspSkillParam.GetResetCost();
      if (resetCost == null)
        DebugUtility.LogError("Error:Not InspSkillResetCost!");
      else if (!this.CheckCost(resetCost))
      {
        int pinID = -1;
        if (resetCost.Type == InspSkillCostType.GOLD)
          pinID = 665;
        else if (resetCost.Type == InspSkillCostType.COIN)
          pinID = 666;
        else if (resetCost.Type == InspSkillCostType.COIN_P)
          pinID = 667;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
      }
      else
      {
        InspirationSkillData inspirationSkillData = new InspirationSkillData();
        InspSkillParam parent = dataOfClass.InspSkillParam.Parent;
        if (parent != null && parent.Parent != null)
          inspirationSkillData.Deserialize(new Json_InspirationSkill()
          {
            iid = (long) dataOfClass.UniqueID,
            iname = parent.Iname,
            is_set = !(bool) dataOfClass.IsSet ? 0 : 1,
            level = parent.LvCap,
            slot = (int) dataOfClass.Slot
          });
        DataSource.Bind<AbilityData>(this.m_InspSkillResetBefore, dataOfClass.AbilityData, false);
        DataSource.Bind<AbilityData>(this.m_InspSkillResetAfter, inspirationSkillData.AbilityData, false);
        GameParameter.UpdateAll(this.m_InspSkillResetBefore);
        GameParameter.UpdateAll(this.m_InspSkillResetAfter);
        if ((UnityEngine.Object) this.m_InspSkillResetText != (UnityEngine.Object) null && (UnityEngine.Object) this.m_InspSkillResetTitle != (UnityEngine.Object) null)
        {
          string str1 = inspirationSkillData == null || inspirationSkillData.AbilityData == null ? LocalizedText.Get("sys.ARTIFACT_INSPIRATIONSKILL_RESET_TITLE") : LocalizedText.Get("sys.ARTIFACT_INSPIRATIONSKILL_RESET_TITLE2");
          string str2 = inspirationSkillData == null || inspirationSkillData.AbilityData == null ? LocalizedText.Get("sys.ARTIFACT_INSPIRATIONSKILL_RESET_TEXT") : LocalizedText.Get("sys.ARTIFACT_INSPIRATIONSKILL_RESET_TEXT2");
          this.m_InspSkillResetTitle.text = str1;
          this.m_InspSkillResetText.text = str2;
        }
        GlobalVars.TargetInspSkill = dataOfClass;
        GlobalVars.TargetInspSkillArtifact = this.mCurrentArtifact;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 662);
      }
    }

    private void OnSelectEnhanceListItem(GameObject go)
    {
      ArtifactData data = DataSource.FindDataOfClass<ArtifactData>(go, (ArtifactData) null);
      if (data == null || data.IsFavorite || data.IsEquipped())
        return;
      InspirationSkillData inspirationSkillData = MonoSingleton<GameManager>.Instance.Player.Artifacts.Find((Predicate<ArtifactData>) (select => (long) select.UniqueID == (long) this.mCurrentArtifact.UniqueID)).InspirationSkillList.Find((Predicate<InspirationSkillData>) (select => (long) select.UniqueID == (long) this.m_CurrentInspSkill.UniqueID));
      if ((int) inspirationSkillData.Lv >= inspirationSkillData.InspSkillParam.LvCap)
        return;
      SerializeValueBehaviour component = go.GetComponent<SerializeValueBehaviour>();
      GameObject gameObject = (GameObject) null;
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        gameObject = component.list.GetGameObject("select");
      int index = this.m_SelectedArtifactList.FindIndex((Predicate<ArtifactData>) (p => (long) p.UniqueID == (long) data.UniqueID));
      if (index >= 0)
      {
        this.m_SelectedArtifactList.RemoveAt(index);
      }
      else
      {
        if ((int) this.m_CurrentInspSkill.Lv + this.mCurrentArtifact.GetTotalInspLvUpCount(this.m_CurrentInspSkill.InspSkillParam, this.m_SelectedArtifactList) >= this.m_CurrentInspSkill.InspSkillParam.LvCap)
          return;
        this.m_SelectedArtifactList.Add(data);
      }
      if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
        gameObject.SetActive(index < 0);
      InspirationSkillData data1 = new InspirationSkillData();
      if (this.m_SelectedArtifactList.Count > 0)
      {
        int totalInspLvUpCount = this.mCurrentArtifact.GetTotalInspLvUpCount(this.m_CurrentInspSkill.InspSkillParam, this.m_SelectedArtifactList);
        Json_InspirationSkill json = this.m_CurrentInspSkill.ToJson();
        json.level += totalInspLvUpCount;
        data1.Deserialize(json);
      }
      DataSource.Bind<InspirationSkillData>(this.m_InspSkillEnhanceAfterPanel, data1, false);
      DataSource.Bind<AbilityData>(this.m_InspSkillEnhanceAfterPanel, data1.AbilityData, false);
      GameParameter.UpdateAll(this.m_InspSkillEnhanceAfterPanel);
      if ((UnityEngine.Object) this.m_InspSkillEnhanceCost != (UnityEngine.Object) null)
      {
        int lv1 = (int) this.m_CurrentInspSkill.Lv;
        int lv2 = (int) data1.Lv;
        this.m_InspSkillEnhanceCost.text = InspSkillParam.GetInspLvUpCostTotal(this.m_CurrentInspSkill.InspSkillParam.Iname, lv1, lv2 - lv1).ToString();
      }
      if ((UnityEngine.Object) this.m_InspSkillEnhanceConfirmButton != (UnityEngine.Object) null)
        this.m_InspSkillEnhanceConfirmButton.interactable = this.m_SelectedArtifactList.Count > 0;
      bool active = false;
      if (data1 != null && data1.InspSkillParam != null)
        active = (int) data1.Lv >= data1.InspSkillParam.LvCap;
      this.RefreshInspSkillEnhanceListNotSelect(active);
    }

    private void OnSelectUnlockInspSkill(GameObject go)
    {
      if ((UnityEngine.Object) this.m_InspSkillSlotUnlockConfirmText == (UnityEngine.Object) null)
      {
        DebugUtility.LogError("m_InspSkillSlotUnlockConfirmText is NullorEmpty!");
      }
      else
      {
        ArtifactData artifactData = MonoSingleton<GameManager>.Instance.Player.Artifacts.Find((Predicate<ArtifactData>) (select => (long) select.UniqueID == (long) this.mCurrentArtifact.UniqueID));
        InspSkillCostParam inspSkillOpenCost = MonoSingleton<GameManager>.Instance.MasterParam.GetInspSkillOpenCost((int) artifactData.InspirationSkillSlotNum + 1);
        if (inspSkillOpenCost == null)
          DebugUtility.LogError("Error:Not InspSkillCostParam!");
        else if (!this.CheckCost(inspSkillOpenCost))
        {
          int pinID = -1;
          if (inspSkillOpenCost.Type == InspSkillCostType.GOLD)
            pinID = 665;
          else if (inspSkillOpenCost.Type == InspSkillCostType.COIN)
            pinID = 666;
          else if (inspSkillOpenCost.Type == InspSkillCostType.COIN_P)
            pinID = 667;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
        }
        else
        {
          this.m_InspSkillSlotUnlockConfirmText.text = LocalizedText.Get("sys.ARTIFACT_INSPIRATIONSKILL_OPEN_TEXT", new object[1]
          {
            (object) LocalizedText.Get("sys.ARTIFACT_INSPIRATIONSKILL_COST_PREFIX_" + inspSkillOpenCost.Type.ToString(), new object[1]
            {
              (object) inspSkillOpenCost.Num
            })
          });
          GlobalVars.TargetInspSkillArtifact = artifactData;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 663);
        }
      }
    }

    private bool CheckCost(InspSkillCostParam cost)
    {
      bool flag = false;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (cost.Type == InspSkillCostType.GOLD)
        flag = instance.Player.Gold > cost.Num;
      else if (cost.Type == InspSkillCostType.COIN)
        flag = instance.Player.Coin > cost.Num;
      else if (cost.Type == InspSkillCostType.COIN_P)
        flag = instance.Player.PaidCoin > cost.Num;
      return flag;
    }

    private void OnOpenInspirationSkillDetail(GameObject go)
    {
    }

    private void OnConfirmInspSkillEnhance()
    {
      for (int index = 0; index < this.m_InspSkillEnhanceConfirmItems.Count; ++index)
      {
        DataSource.Bind<ArtifactData>(this.m_InspSkillEnhanceConfirmItems[index], (ArtifactData) null, false);
        GameParameter.UpdateAll(this.m_InspSkillEnhanceConfirmItems[index]);
        this.m_InspSkillEnhanceConfirmItems[index].SetActive(false);
      }
      for (int index = 0; index < this.m_SelectedArtifactList.Count; ++index)
      {
        GameObject root;
        if (this.m_InspSkillEnhanceConfirmItems.Count > index)
        {
          root = this.m_InspSkillEnhanceConfirmItems[index];
        }
        else
        {
          root = UnityEngine.Object.Instantiate<GameObject>(this.m_InspSkillEnhanceConfirmListItem);
          if ((UnityEngine.Object) root != (UnityEngine.Object) null)
          {
            this.m_InspSkillEnhanceConfirmItems.Add(root);
            root.transform.SetParent(this.m_InspSkillEnhanceConfirmList.transform, false);
          }
        }
        DataSource.Bind<ArtifactData>(root, this.m_SelectedArtifactList[index], false);
        GameParameter.UpdateAll(root);
        root.SetActive(true);
      }
      int inspLvUpCostTotal = InspSkillParam.GetInspLvUpCostTotal(this.m_CurrentInspSkill.InspSkillParam.Iname, (int) this.m_CurrentInspSkill.Lv, this.mCurrentArtifact.GetTotalInspLvUpCount(this.m_CurrentInspSkill.InspSkillParam, this.m_SelectedArtifactList));
      if (inspLvUpCostTotal == -1)
        DebugUtility.LogError("Error:Not InspSkillEnhance Cost!");
      else if (MonoSingleton<GameManager>.Instance.Player.Gold < inspLvUpCostTotal)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 665);
      }
      else
      {
        GlobalVars.TargetInspSkillArtifact = MonoSingleton<GameManager>.Instance.Player.Artifacts.Find((Predicate<ArtifactData>) (select => (long) select.UniqueID == (long) this.mCurrentArtifact.UniqueID));
        GlobalVars.TargetInspSkill = this.m_CurrentInspSkill;
        if (GlobalVars.MixInspSkillArtifactList == null)
          GlobalVars.MixInspSkillArtifactList = new List<ArtifactData>();
        GlobalVars.MixInspSkillArtifactList.Clear();
        GlobalVars.MixInspSkillArtifactList.AddRange((IEnumerable<ArtifactData>) this.m_SelectedArtifactList.ToArray());
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 672);
      }
    }

    private void OnResetEnhanceListItem()
    {
      this.m_SelectedArtifactList.Clear();
      DataSource.Bind<InspirationSkillData>(this.m_InspSkillEnhanceAfterPanel, (InspirationSkillData) null, false);
      DataSource.Bind<AbilityData>(this.m_InspSkillEnhanceAfterPanel, (AbilityData) null, false);
      GameParameter.UpdateAll(this.m_InspSkillEnhanceAfterPanel);
      if ((UnityEngine.Object) this.m_InspSkillEnhanceCost != (UnityEngine.Object) null)
        this.m_InspSkillEnhanceCost.text = "-";
      if ((UnityEngine.Object) this.m_InspSkillEnhanceConfirmButton != (UnityEngine.Object) null)
        this.m_InspSkillEnhanceConfirmButton.interactable = false;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 678);
    }

    private void OnSuccessInspSkillEnhance()
    {
      if ((UnityEngine.Object) this.m_InspSkillEnhanceSuccessEffect == (UnityEngine.Object) null)
        return;
      DataSource.Bind<AbilityData>(this.m_InspSkillEnhanceSuccessEffect, (AbilityData) null, false);
      long artifact_iid = (long) this.mCurrentArtifact.UniqueID;
      long insp_iid = (long) this.m_CurrentInspSkill.UniqueID;
      DataSource.Bind<AbilityData>(this.m_InspSkillEnhanceSuccessEffect, MonoSingleton<GameManager>.Instance.Player.Artifacts.Find((Predicate<ArtifactData>) (select => (long) select.UniqueID == artifact_iid)).InspirationSkillList.Find((Predicate<InspirationSkillData>) (select => (long) select.UniqueID == insp_iid)).AbilityData, false);
      GameParameter.UpdateAll(this.m_InspSkillEnhanceSuccessEffect);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 674);
    }

    public delegate void EquipEvent(ArtifactData artifact, ArtifactTypes type = ArtifactTypes.None);

    private class StatusCache
    {
      public BaseStatus BaseAdd = new BaseStatus();
      public BaseStatus BaseMul = new BaseStatus();
      public BaseStatus UnitAdd = new BaseStatus();
      public BaseStatus UnitMul = new BaseStatus();
    }

    private abstract class Request
    {
      public Network.ResponseCallback Callback;

      public abstract WebAPI Compose();
    }

    private class RequestAddRare : ArtifactWindow.Request
    {
      public long UniqueID;
      public string trophyprog;
      public string bingoprog;

      public override WebAPI Compose()
      {
        return (WebAPI) new ReqArtifactAddRare(this.UniqueID, this.Callback, this.trophyprog, this.bingoprog);
      }
    }

    private class RequestAddExp : ArtifactWindow.Request
    {
      public List<ItemParam> Items = new List<ItemParam>(32);
      public long UniqueID;

      public override WebAPI Compose()
      {
        Dictionary<string, int> usedItems = new Dictionary<string, int>();
        for (int index = 0; index < this.Items.Count; ++index)
        {
          if (usedItems.ContainsKey(this.Items[index].iname))
          {
            Dictionary<string, int> dictionary;
            string iname;
            (dictionary = usedItems)[iname = this.Items[index].iname] = dictionary[iname] + 1;
          }
          else
            usedItems[this.Items[index].iname] = 1;
        }
        return (WebAPI) new ReqArtifactEnforce(this.UniqueID, usedItems, this.Callback);
      }
    }

    private enum InspSkillState : byte
    {
      NONE,
      LOCK,
      UNLOCK,
      LEANED,
    }
  }
}
