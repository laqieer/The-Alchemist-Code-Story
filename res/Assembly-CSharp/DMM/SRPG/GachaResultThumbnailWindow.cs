﻿// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultThumbnailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "BackToUnitDetail", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Back Top", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(11, "Back Top", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(20, "OneMore Exec", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(21, "OneMore Exec", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(100, "UnitDetail", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(111, "UnitDetailSingle", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(101, "ItemDetail", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "PieceDetail", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "ArtifactDetail", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "ConceptCardDetail", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(105, "RuneDetail", FlowNode.PinTypes.Output, 105)]
  [FlowNode.Pin(200, "BackGachaTop", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(300, "ユニット詳細から復帰(簡易演出時)", FlowNode.PinTypes.Output, 300)]
  [FlowNode.Pin(210, "Redraw Confirm", FlowNode.PinTypes.Output, 210)]
  [FlowNode.Pin(50, "Click Icon(アイコン選択)", FlowNode.PinTypes.Input, 210)]
  [FlowNode.Pin(51, "Click Card Unit Icon", FlowNode.PinTypes.Input, 211)]
  [FlowNode.Pin(60, "アイコン表示初期化完了", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(59, "アイコン表示初期化完了(ユニット単発の場合))", FlowNode.PinTypes.Output, 59)]
  [FlowNode.Pin(61, "オマケ表示スタート", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(70, "オマケ表示終了", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(71, "Click Bonus(おまけ表示)", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(80, "引き直し確定確認", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(81, "引き直し終了", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(82, "引き直し終了", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(90, "Interactable更新(True)", FlowNode.PinTypes.Input, 90)]
  [FlowNode.Pin(91, "Interactable更新(False", FlowNode.PinTypes.Input, 91)]
  [FlowNode.Pin(400, "ExtraPopupConfirm", FlowNode.PinTypes.Input, 400)]
  public class GachaResultThumbnailWindow : MonoBehaviour, IFlowInterface
  {
    private readonly int IN_REFRESH;
    private readonly int IN_BACKTO_UNITDETAIL = 1;
    private readonly int IN_BACK_TOP = 10;
    private readonly int OUT_BACK_TOP = 11;
    private readonly int IN_ONEMORE_GACHA = 20;
    private readonly int OUT_ONEMORE_GACHA = 21;
    private readonly int OUT_UNITDETAIL = 100;
    private readonly int OUT_UNITDETAIL_SINGLE = 111;
    private readonly int OUT_ITEM_DETAIL = 101;
    private readonly int OUT_ARTIFACT_DETAIL = 103;
    private readonly int OUT_CONCEPTCARD_DETAIL = 104;
    private const int OUT_RUNE_DETAIL = 105;
    private const int PIN_OT_GACHA_CONFIRM = 210;
    private const int PIN_IN_CLICK_ICON = 50;
    private const int PIN_IN_CLICK_CARDUNIT_ICON = 51;
    private const int PIN_OT_ICON_INITALIZED_UNIT = 59;
    private const int PIN_OT_ICON_INITALIZED = 60;
    private const int PIN_IN_START_BONUS = 61;
    private const int PIN_OT_FINISHED_BONUS = 70;
    private const int PIN_IN_CLICK_BONUS = 71;
    private const int PIN_OT_CONFIRM_REDRAW = 80;
    private const int PIN_IN_CLOSE_REDRAW = 81;
    private const int PIN_OT_CLOSE_REDRAW = 82;
    private const int PIN_IN_BTN_INTERACTABLE_TRUE = 90;
    private const int PIN_IN_BTN_INTERACTABLE_FALSE = 91;
    private const int PIN_OT_GACHAWINDOW_ON = 300;
    private const int PIN_IN_EXTRA_POPUPCONFIRM = 400;
    public static readonly int CONTENT_VIEW_MAX = 10;
    public GameObject CautionText;
    private bool is_gift;
    private bool m_inialize;
    private bool m_FinishedBonusEffect;
    private GameObject m_Thumbnail;
    private GameObject m_UnitDetail;
    [SerializeField]
    private GameObject BlockTemplate;
    private GameObject m_ResultBlock;
    private List<GameObject> m_ResultIconRootList = new List<GameObject>();
    [SerializeField]
    private GameObject DetailRoot;
    [SerializeField]
    private string BonusWindowPrefab = "UI/GachaBonusWindow";
    [SerializeField]
    private float WaitBonusWindow = 0.5f;
    [SerializeField]
    private Transform BonusRoot;
    [SerializeField]
    private SerializeValueBehaviour ButtonList;
    private Button m_OnemoreBtn;
    private Button m_DefaultBtn;
    private Button m_BonusBtn;
    private Button m_RedrawBtn;
    private GachaRequestParam mRequest;
    private bool m_simple;
    [SerializeField]
    private string AnimTriggerNameStart = "cut";
    [SerializeField]
    private string AnimStateName_End = "end";
    private List<UnitParam> mPopupExtraUnitList = new List<UnitParam>();
    public static readonly int VIEW_COUNT = 10;

    public bool IsInialize => this.m_inialize;

    public bool IsFinishedBonus => this.m_FinishedBonusEffect;

    public bool IsSimple => this.m_simple;

    public void Activated(int pinID)
    {
      if (pinID == this.IN_REFRESH)
      {
        if (!this.m_inialize || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Thumbnail, (UnityEngine.Object) null))
          return;
        this.m_UnitDetail.SetActive(false);
        this.m_Thumbnail.SetActive(true);
      }
      else if (pinID == this.IN_BACKTO_UNITDETAIL)
      {
        if (this.CheckTutorial())
        {
          this.m_inialize = false;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 210);
        }
        else
        {
          this.m_UnitDetail.SetActive(false);
          this.m_Thumbnail.SetActive(true);
          if (!this.IsSimple)
            return;
          this.StartCoroutine(this.ShowIcons(300, string.Empty));
        }
      }
      else if (pinID == this.IN_BACK_TOP)
      {
        if (GachaResultData.IsRedrawGacha)
        {
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 80);
        }
        else
        {
          this.m_inialize = false;
          this.m_FinishedBonusEffect = false;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, this.OUT_BACK_TOP);
        }
      }
      else if (pinID == this.IN_ONEMORE_GACHA)
      {
        this.m_inialize = false;
        this.m_FinishedBonusEffect = false;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Thumbnail, (UnityEngine.Object) null))
          this.m_Thumbnail.SetActive(false);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, this.OUT_ONEMORE_GACHA);
      }
      else
      {
        switch (pinID)
        {
          case 50:
            this.OnSelectIcon();
            break;
          case 51:
            this.OnSelectCardUnitIcon();
            break;
          case 61:
            NotifyList.mNotifyEnable = true;
            if (!this.IsFinishedBonus && this.IsThumbnailActive())
            {
              if (GachaResultData.dropMails != null && GachaResultData.dropMails.Length > 0)
              {
                this.StartCoroutine(this.ShowBonus());
                break;
              }
              ButtonEvent.UnLock("gacha_result_initalize");
              this.m_FinishedBonusEffect = true;
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 70);
              break;
            }
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 70);
            break;
          case 71:
            if (GachaResultData.dropMails != null && GachaResultData.dropMails.Length > 0)
            {
              this.StartCoroutine(this.ShowBonus(false));
              break;
            }
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 70);
            break;
          case 81:
            NotifyList.mNotifyEnable = true;
            FlowNode_Variable.Set("REDRAW_GACHA_PENDING", "0");
            GlobalEvent.Invoke("ENABLE_HOME_BUTTON", (object) this);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 82);
            break;
          case 90:
            this.RefreshButtonInteractable(true);
            break;
          case 91:
            this.RefreshButtonInteractable(false);
            break;
          case 400:
            FlowNode_ExtraUnitOpenPopup.OpenPopup((UIUtility.DialogResultEvent) null);
            break;
        }
      }
    }

    private void Awake() => this.InitButton();

    private void Start()
    {
    }

    private void OnEnable()
    {
      if (GachaResultData.drops == null || GachaResultData.drops.Length <= 0)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) HomeWindow.Current, (UnityEngine.Object) null))
        HomeWindow.Current.SetVisible(true);
      this.Initalize();
    }

    private void Reset()
    {
      GameUtility.DestroyGameObjects(this.m_ResultIconRootList);
      this.m_ResultIconRootList.Clear();
    }

    private void OnDestroy() => this.Reset();

    private void Refresh()
    {
      if (this.IsSimple)
      {
        this.CreateContentList();
        this.Reset();
        this.RefreshResult(GachaResultData.drops, this.m_ResultBlock, 0, true);
        this.RefreshButtonInteractable(false);
        this.StartCoroutine(this.ShowIcons(60, this.AnimTriggerNameStart));
      }
      else
      {
        if (this.CheckSingleDropForUnit())
          this.Select(0, 1);
        this.CreateContentList();
        this.Reset();
        this.RefreshResult(GachaResultData.drops, this.m_ResultBlock, 0);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CautionText, (UnityEngine.Object) null))
          this.CautionText.SetActive(this.is_gift);
        if (!this.CheckSingleDropForUnit())
        {
          this.RefreshIcons(true);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 60);
        }
        else
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 59);
        this.RefreshButtonInteractable(false);
      }
    }

    private void Initalize()
    {
      if (this.IsInialize)
      {
        this.RefreshIcons(true);
        if (this.IsFinishedBonus)
          return;
        ButtonEvent.Lock("gacha_initialize");
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 60);
      }
      else
      {
        if (GachaResultData.drops == null)
          return;
        if (GachaResultData.IsRedrawGacha)
          GlobalEvent.Invoke("DISABLE_HOME_BUTTON", (object) this);
        this.mRequest = (GachaRequestParam) null;
        GachaRequestParam dataOfClass = DataSource.FindDataOfClass<GachaRequestParam>(((Component) this).gameObject, (GachaRequestParam) null);
        if (dataOfClass != null)
          this.mRequest = dataOfClass;
        this.SetDetailActiveStatus(false);
        this.is_gift = this.CheckIsGiftData(GachaResultData.drops);
        for (int index = 0; index < GachaResultData.drops.Length; ++index)
        {
          if (GachaResultData.drops[index].type == GachaDropData.Type.Unit)
          {
            MonoSingleton<GameManager>.Instance.Player.UpdateUnitTrophyStates(false);
            break;
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_OnemoreBtn, (UnityEngine.Object) null))
        {
          this.RefreshGachaCostObject(((Component) this.m_OnemoreBtn).gameObject);
          ((Component) this.m_OnemoreBtn).gameObject.SetActive(GachaResultData.UseOneMore && !GachaResultData.IsRedrawGacha);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_BonusBtn, (UnityEngine.Object) null))
          ((Component) this.m_BonusBtn).gameObject.SetActive(GachaResultData.dropMails != null && GachaResultData.dropMails.Length > 0);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_RedrawBtn, (UnityEngine.Object) null))
        {
          SerializeValueBehaviour component = ((Component) this.m_RedrawBtn).GetComponent<SerializeValueBehaviour>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            GameObject gameObject = component.list.GetGameObject("option");
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
              gameObject.SetActive(GachaResultData.IsPending);
            Text uiLabel = component.list.GetUILabel("txt_count");
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiLabel, (UnityEngine.Object) null))
            {
              string empty;
              if (GachaResultData.IsPending)
                empty = LocalizedText.Get("sys.GACHA_REDRAW_COUNT_LIMIT", (object) GachaResultData.RedrawRest);
              else
                empty = string.Empty;
              string str = empty;
              uiLabel.text = str;
            }
          }
          ((Component) this.m_RedrawBtn).gameObject.SetActive(GachaResultData.IsRedrawGacha);
          ((Selectable) this.m_RedrawBtn).interactable = GachaResultData.IsPending && GachaResultData.RedrawRest > 0;
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_DefaultBtn, (UnityEngine.Object) null))
        {
          SerializeValueBehaviour component = ((Component) this.m_DefaultBtn).GetComponent<SerializeValueBehaviour>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            component.list.GetUILabel("text").text = !GachaResultData.IsPending ? LocalizedText.Get("sys.BTN_GACHA_OK") : LocalizedText.Get("sys.BTN_DECIDE_CONFIRM");
        }
        this.Refresh();
        this.m_inialize = true;
      }
    }

    public void SetDetailActiveStatus(bool _active)
    {
      SerializeValueBehaviour component = ((Component) this).gameObject.GetComponent<SerializeValueBehaviour>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_Thumbnail, (UnityEngine.Object) null))
      {
        this.m_Thumbnail = component.list.GetGameObject("thumbnail");
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_Thumbnail, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("GachaResultThumbnailWindow.cs:unit_detailの指定がありません.");
          return;
        }
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_UnitDetail, (UnityEngine.Object) null))
      {
        this.m_UnitDetail = component.list.GetGameObject("unit_detail");
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_UnitDetail, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("GachaResultThumbnailWindow.cs:unit_detailの指定がありません.");
          return;
        }
      }
      this.m_UnitDetail.SetActive(_active);
    }

    private void CreateContentList()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.BlockTemplate, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("召喚結果等を表示するブロックのテンプレートが指定されていません");
      }
      else
      {
        this.BlockTemplate.SetActive(false);
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_ResultBlock, (UnityEngine.Object) null))
          return;
        this.m_ResultBlock = UnityEngine.Object.Instantiate<GameObject>(this.BlockTemplate);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_ResultBlock, (UnityEngine.Object) null))
          DebugUtility.LogError("召喚結果表示ブロックの生成に失敗しました");
        else
          this.m_ResultBlock.transform.SetParent(this.BlockTemplate.transform.parent, false);
      }
    }

    private void RefreshResult(
      GachaDropData[] _drops,
      GameObject _block,
      int _block_type,
      bool _is_anim = false)
    {
      if (_drops == null || _drops.Length < 0 || UnityEngine.Object.op_Equality((UnityEngine.Object) _block, (UnityEngine.Object) null))
        return;
      if (this.mPopupExtraUnitList != null)
        this.mPopupExtraUnitList.Clear();
      _block.SetActive(true);
      int length = _drops.Length;
      SerializeValueBehaviour component1 = _block.GetComponent<SerializeValueBehaviour>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
      {
        GameObject gameObject1 = component1.list.GetGameObject("icon");
        gameObject1.SetActive(false);
        for (int index1 = 0; index1 < length; ++index1)
        {
          GachaDropData drop = _drops[index1];
          int index2 = index1;
          if (drop != null)
          {
            GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
            gameObject2.transform.SetParent(gameObject1.transform.parent, false);
            ((Behaviour) gameObject2.GetComponent<Animator>()).enabled = _is_anim;
            GachaResultThumbnailItem component2 = gameObject2.GetComponent<GachaResultThumbnailItem>();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) component2, (UnityEngine.Object) null))
            {
              DebugUtility.LogError("GachaResultThumbnailItemがアタッチされていません.");
            }
            else
            {
              component2.Setup(drop, index2);
              this.m_ResultIconRootList.Add(gameObject2);
              if (drop.type == GachaDropData.Type.Unit)
              {
                if (drop.isNew && drop.unit != null)
                  FlowNode_ExtraUnitOpenPopup.ReserveOpenExtraQuestPopup(drop.unit.iname);
              }
              else if (drop.type == GachaDropData.Type.ConceptCard && drop.isNew && !string.IsNullOrEmpty(drop.conceptcard.first_get_unit))
                FlowNode_ExtraUnitOpenPopup.ReserveOpenExtraQuestPopup(drop.conceptcard.first_get_unit);
              if (_block_type == 0)
                this.m_ResultIconRootList.Add(gameObject2);
            }
          }
        }
      }
      SerializeValueBehaviour component3 = ((Component) this).gameObject.GetComponent<SerializeValueBehaviour>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component3, (UnityEngine.Object) null))
        return;
      GameObject gameObject = component3.list.GetGameObject("space");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      gameObject.transform.SetAsLastSibling();
      gameObject.SetActive(length > GachaResultThumbnailWindow.VIEW_COUNT);
    }

    private void RefreshIcons(bool _active)
    {
      if (this.m_ResultIconRootList == null || this.m_ResultIconRootList.Count <= 0)
        return;
      for (int index = 0; index < this.m_ResultIconRootList.Count; ++index)
        this.m_ResultIconRootList[index].SetActive(_active);
    }

    [DebuggerHidden]
    private IEnumerator ShowBonus(bool _is_anim = true)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaResultThumbnailWindow.\u003CShowBonus\u003Ec__Iterator0()
      {
        _is_anim = _is_anim,
        \u0024this = this
      };
    }

    private void OnSelectCardUnitIcon()
    {
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      string key = currentValue.GetString("select_unit");
      if (string.IsNullOrEmpty(key))
        return;
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(key);
      if (unitParam == null)
        return;
      this.m_UnitDetail.GetComponent<GachaResultUnitDetail>().Setup(this.CreateUnitData(unitParam));
      ButtonEvent.Invoke("CONCEPT_CARD_DETAIL_BTN_CLOSE", (object) null);
      this.m_Thumbnail.SetActive(false);
      this.m_UnitDetail.gameObject.SetActive(true);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, this.OUT_UNITDETAIL);
    }

    private void OnSelectIcon()
    {
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      this.Select(currentValue.GetInt("index"), currentValue.GetInt("type"), currentValue.GetBool("bonus"));
    }

    private void Select(int _index, int _type, bool _bonus = false)
    {
      int pinID = 0;
      switch (_type)
      {
        case 1:
          GachaResultUnitDetail component = this.m_UnitDetail.GetComponent<GachaResultUnitDetail>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            UnitData unitData = this.CreateUnitData(!_bonus ? GachaResultData.drops[_index].unit : GachaResultData.dropMails[_index].unit);
            component.Setup(unitData);
            int num = !this.CheckSingleDropForUnit() ? this.OUT_UNITDETAIL : this.OUT_UNITDETAIL_SINGLE;
          }
          this.m_UnitDetail.SetActive(true);
          this.m_Thumbnail.SetActive(false);
          pinID = this.OUT_UNITDETAIL;
          break;
        case 2:
        case 3:
          ItemParam itemParam = !_bonus ? GachaResultData.drops[_index].item : GachaResultData.dropMails[_index].item;
          if (this.SelectItem(itemParam))
          {
            pinID = itemParam.type != EItemType.Rune ? this.OUT_ITEM_DETAIL : 105;
            break;
          }
          break;
        case 4:
          if (this.SelectArtifact(this.CreateArtifactData(!_bonus ? GachaResultData.drops[_index].artifact : GachaResultData.dropMails[_index].artifact, !_bonus ? GachaResultData.drops[_index].Rare : GachaResultData.dropMails[_index].Rare)))
          {
            pinID = this.OUT_ARTIFACT_DETAIL;
            break;
          }
          break;
        case 5:
          if (this.SelectConceptCard(!_bonus ? GachaResultData.drops[_index].conceptcard.iname : GachaResultData.dropMails[_index].conceptcard.iname))
          {
            pinID = this.OUT_CONCEPTCARD_DETAIL;
            break;
          }
          break;
      }
      if (pinID <= 0)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
    }

    private bool SelectItem(ItemParam _param)
    {
      if (_param == null || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailRoot, (UnityEngine.Object) null))
        return false;
      DataSource.Bind<ItemParam>(this.DetailRoot, (ItemParam) null);
      DataSource.Bind<ItemData>(this.DetailRoot, (ItemData) null);
      DataSource.Bind<ItemParam>(this.DetailRoot, _param);
      DataSource.Bind<ItemData>(this.DetailRoot, MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(_param.iname));
      return true;
    }

    private bool SelectArtifact(ArtifactData _data)
    {
      if (_data == null || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailRoot, (UnityEngine.Object) null))
        return false;
      DataSource.Bind<ArtifactData>(this.DetailRoot, (ArtifactData) null);
      DataSource.Bind<ArtifactData>(this.DetailRoot, _data);
      return true;
    }

    private bool SelectConceptCard(string _iname)
    {
      if (string.IsNullOrEmpty(_iname))
        return false;
      ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(_iname);
      GlobalVars.SelectedConceptCardData.Set(cardDataForDisplay);
      return true;
    }

    private ItemData CreateItemData(ItemParam iparam, int num)
    {
      ItemData itemData = new ItemData();
      itemData.Setup(0L, iparam, num);
      return itemData;
    }

    private ArtifactData CreateArtifactData(ArtifactParam param, int rarity)
    {
      ArtifactData artifactData = new ArtifactData();
      artifactData.Deserialize(new Json_Artifact()
      {
        iid = 1L,
        exp = 0,
        iname = param.iname,
        fav = 0,
        rare = Math.Min(Math.Max(param.rareini, rarity), param.raremax)
      });
      return artifactData;
    }

    private UnitData CreateUnitData(UnitParam uparam)
    {
      UnitData unitData = new UnitData();
      Json_Unit json = new Json_Unit()
      {
        iid = 1,
        iname = uparam.iname,
        exp = 0,
        lv = 1,
        plus = 0,
        rare = 0,
        select = new Json_UnitSelectable()
      };
      json.select.job = 0L;
      json.jobs = (Json_Job[]) null;
      json.abil = (Json_MasterAbility) null;
      if (uparam.jobsets != null && uparam.jobsets.Length > 0)
      {
        List<Json_Job> jsonJobList = new List<Json_Job>(uparam.jobsets.Length);
        int num = 1;
        for (int index = 0; index < uparam.jobsets.Length; ++index)
        {
          JobSetParam jobSetParam = MonoSingleton<GameManager>.Instance.GetJobSetParam(uparam.jobsets[index]);
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

    private bool CheckTutorial()
    {
      return (MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) == 0L;
    }

    private bool CheckSingleDropForUnit()
    {
      return GachaResultData.drops != null && GachaResultData.drops.Length == 1 && GachaResultData.drops[0].type == GachaDropData.Type.Unit;
    }

    private bool CheckIsGiftData(GachaDropData[] _data)
    {
      bool flag = false;
      if (_data != null)
      {
        for (int index = 0; index < _data.Length; ++index)
        {
          if (_data[index].isGift)
          {
            flag = true;
            break;
          }
        }
      }
      return flag;
    }

    private void RefreshGachaCostObject(GameObject button)
    {
      GachaCostObject gachaCostObject1 = button.GetComponent<GachaCostObject>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gachaCostObject1, (UnityEngine.Object) null))
      {
        GachaCostObject gachaCostObject2 = button.AddComponent<GachaCostObject>();
        SerializeValueBehaviour component = button.GetComponent<SerializeValueBehaviour>();
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("再召喚ボタン用のSerializeValueListがありません");
          return;
        }
        gachaCostObject2.RootObject = button;
        gachaCostObject2.TicketObject = component.list.GetGameObject("ticket");
        gachaCostObject2.DefaultObject = component.list.GetGameObject("default");
        gachaCostObject2.DefaultBGObject = component.list.GetGameObject("bg");
        gachaCostObject1 = gachaCostObject2;
      }
      gachaCostObject1.Refresh();
    }

    private void InitButton()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ButtonList, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_DefaultBtn, (UnityEngine.Object) null))
      {
        Button component = this.ButtonList.list.GetComponent<Button>("default");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          this.m_DefaultBtn = component;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_OnemoreBtn, (UnityEngine.Object) null))
      {
        Button component = this.ButtonList.list.GetComponent<Button>("onemore");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          this.m_OnemoreBtn = component;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_BonusBtn, (UnityEngine.Object) null))
      {
        Button component = this.ButtonList.list.GetComponent<Button>("bonus");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          this.m_BonusBtn = component;
      }
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_RedrawBtn, (UnityEngine.Object) null))
        return;
      Button component1 = this.ButtonList.list.GetComponent<Button>("redraw");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        return;
      this.m_RedrawBtn = component1;
    }

    private bool IsThumbnailActive()
    {
      bool flag = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_UnitDetail, (UnityEngine.Object) null))
        flag = !this.m_UnitDetail.GetActive();
      return flag;
    }

    private void RefreshButtonInteractable(bool _active)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_RedrawBtn, (UnityEngine.Object) null))
        ((Selectable) this.m_RedrawBtn).interactable = !_active ? _active : GachaResultData.IsPending && GachaResultData.RedrawRest > 0;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_OnemoreBtn, (UnityEngine.Object) null))
      {
        bool flag = _active;
        if (this.mRequest != null && this.mRequest.IsTicketGacha && _active)
          flag = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mRequest.Ticket) > 0;
        ((Selectable) this.m_OnemoreBtn).interactable = flag;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_BonusBtn, (UnityEngine.Object) null))
        ((Selectable) this.m_BonusBtn).interactable = _active;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_DefaultBtn, (UnityEngine.Object) null))
        return;
      ((Selectable) this.m_DefaultBtn).interactable = _active;
    }

    [DebuggerHidden]
    private IEnumerator ShowIcons(int pinID, string trigger = "")
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaResultThumbnailWindow.\u003CShowIcons\u003Ec__Iterator1()
      {
        trigger = trigger,
        pinID = pinID,
        \u0024this = this
      };
    }

    public void SetSimpleAnimFlag(bool flag = false) => this.m_simple = flag;

    public enum GachaResultType
    {
      None,
      Unit,
      Item,
      Piece,
      Artifact,
      ConceptCard,
      End,
    }
  }
}