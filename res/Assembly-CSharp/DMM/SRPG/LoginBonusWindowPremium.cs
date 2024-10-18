// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusWindowPremium
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "ログインボーナス取得演出", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "前のページボタン", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "次のページボタン", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "次のページへ切り替え", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(100, "アイテムアイコンの選択", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "ログインボーナス取得演出後", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "読み込み完了", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(105, "ログインカウントが0か指定回数以上の場合", FlowNode.PinTypes.Output, 105)]
  [FlowNode.Pin(106, "ページ切り替えボタンを押した後", FlowNode.PinTypes.Output, 106)]
  [FlowNode.Pin(107, "コンフィグ対応から来たときの出口", FlowNode.PinTypes.Output, 107)]
  [FlowNode.Pin(108, "ページ最後のログインボーナスを受け取り次のページへ", FlowNode.PinTypes.Output, 108)]
  [FlowNode.Pin(109, "次のページへ切り替えが終わった後", FlowNode.PinTypes.Output, 109)]
  public class LoginBonusWindowPremium : MonoBehaviour, IFlowInterface
  {
    private const int PIN_TAKE_BONUS = 1;
    private const int PIN_PREV_BUTTON = 2;
    private const int PIN_NEXT_BUTTON = 3;
    private const int PIN_AUTO_NEXT_PAGE = 4;
    private const int PIN_SELECT_ITEM_ICON = 100;
    private const int PIN_TAKED_BONUS = 101;
    private const int PIN_READ_COMPLETE = 102;
    private const int PIN_NOT_TAKED_BONUS = 105;
    private const int PIN_PUSH_PAGE_BUTTON = 106;
    private const int PIN_CONFIG_BUTTON = 107;
    private const int PIN_TAKED_BONUS_NEXTPAGE = 108;
    private const int PIN_AFTER_NEXT_PAGE = 109;
    [HeaderBar("▼ViewList関連")]
    [SerializeField]
    private Transform ParentViewList;
    [SerializeField]
    private GameObject BaseList;
    [SerializeField]
    private int PateItemNum = 15;
    [HeaderBar("▼アイコン表示用オブジェクト")]
    [SerializeField]
    private string ParentItemGridPath = "viewport/content";
    [SerializeField]
    private string TemplateItemGridPath = "viewport/content/item";
    [SerializeField]
    private ListItemEvents Item_Normal;
    [SerializeField]
    private ListItemEvents Item_Taken;
    [HeaderBar("▼演出時のアイコン表示用オブジェクト")]
    [SerializeField]
    private RectTransform TodayBadge;
    [SerializeField]
    private RectTransform TommorowBadge;
    [HeaderBar("▼残り日数")]
    [SerializeField]
    private Text RemainingCount;
    [HeaderBar("▼今フィル画面の表示かどうか")]
    [SerializeField]
    private bool IsConfigWindow = true;
    [HeaderBar("▼ゲームオブジェクト名")]
    [SerializeField]
    private string CheckName = "CHECK";
    [HeaderBar("▼初日のみ非表示にするゲームオブジェクト")]
    [SerializeField]
    private string[] DisabledFirstDayNames;
    [HeaderBar("▼今日ゲットするアイテムに付けるエフェクト")]
    [SerializeField]
    private GameObject BonusParticleEffect;
    [HeaderBar("▼ウィンドウにあるメッセージテキスト")]
    [SerializeField]
    private Text WindowMessageText;
    [HeaderBar("▼アイテムアイコンにあるカウント")]
    [SerializeField]
    private string ItemIconCountPath = "badge/count";
    [HeaderBar("▼アイテム数表示")]
    [SerializeField]
    private string ItemNumPath = "amount/Text_num";
    [HeaderBar("▼ページ数表示")]
    [SerializeField]
    private Text PageCountText;
    [SerializeField]
    private Text PageCountTextMax;
    [HeaderBar("▼ページ切り替えボタン")]
    [SerializeField]
    private Button PrevPageButton;
    [SerializeField]
    private Button NextPageButton;
    private int mLoginBonusCount;
    private UnitPreview mCurrentPreview;
    private RenderTexture mPreviewUnitRT;
    private LoginBonusWindow mMessageWindow;
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    private List<GameObject> mViewLists = new List<GameObject>();
    private int mPageNum;
    private int mPageNumMax;

    public bool ConfigWindow
    {
      set => this.IsConfigWindow = value;
    }

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Normal, (UnityEngine.Object) null))
        ((Component) this.Item_Normal).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Taken, (UnityEngine.Object) null))
        ((Component) this.Item_Taken).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TodayBadge, (UnityEngine.Object) null))
        ((Component) this.TodayBadge).gameObject.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TommorowBadge, (UnityEngine.Object) null))
        return;
      ((Component) this.TommorowBadge).gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
      GameUtility.DestroyGameObject((Component) this.mCurrentPreview);
      this.mCurrentPreview = (UnitPreview) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewUnitRT, (UnityEngine.Object) null))
      {
        RenderTexture.ReleaseTemporary(this.mPreviewUnitRT);
        this.mPreviewUnitRT = (RenderTexture) null;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mMessageWindow, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mMessageWindow).gameObject);
      this.mMessageWindow = (LoginBonusWindow) null;
    }

    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      Json_PremiumLoginBonus[] premiumBonuses = instance.Player.PremiumLoginBonus.premium_bonuses;
      this.mLoginBonusCount = instance.Player.PremiumLoginBonus.count;
      bool flag1 = false;
      if (this.mLoginBonusCount > premiumBonuses.Length)
      {
        this.mLoginBonusCount = premiumBonuses.Length;
        flag1 = true;
      }
      if (premiumBonuses == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.Item_Normal, (UnityEngine.Object) null))
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this);
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RemainingCount, (UnityEngine.Object) null))
          this.RemainingCount.text = Math.Max(premiumBonuses.Length - this.mLoginBonusCount, 0).ToString();
        this.mPageNumMax = (premiumBonuses.Length - 1) / this.PateItemNum + 1;
        if (this.mPageNumMax <= 0)
        {
          UnityEngine.Object.Destroy((UnityEngine.Object) this);
        }
        else
        {
          for (int index = 0; index < this.mPageNumMax; ++index)
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BaseList);
            gameObject.transform.SetParent(this.ParentViewList, false);
            this.mViewLists.Add(gameObject);
          }
          this.BaseList.SetActive(false);
          List<GiftRecieveItemData> giftRecieveItemDataList = new List<GiftRecieveItemData>();
          for (int index1 = 0; index1 < this.mViewLists.Count; ++index1)
          {
            GameObject gameObject1 = ((Component) this.mViewLists[index1].transform.Find(this.TemplateItemGridPath)).gameObject;
            gameObject1.SetActive(false);
            Transform transform1 = this.mViewLists[index1].transform.Find(this.ParentItemGridPath);
            int num1 = (index1 + 1) * this.PateItemNum;
            if (num1 > premiumBonuses.Length)
              num1 = premiumBonuses.Length;
            for (int index2 = index1 * this.PateItemNum; index2 < num1; ++index2)
            {
              GiftRecieveItemData data = new GiftRecieveItemData();
              giftRecieveItemDataList.Add(data);
              string str = (string) null;
              int num2 = 0;
              if (!string.IsNullOrEmpty(premiumBonuses[index2].icon))
                str = premiumBonuses[index2].icon;
              else if (premiumBonuses[index2].coin > 0 && premiumBonuses[index2].item == null)
                str = "$COIN";
              else if (premiumBonuses[index2].item != null && premiumBonuses[index2].item.Length > 0)
                str = premiumBonuses[index2].item[0].iname;
              if (!string.IsNullOrEmpty(str))
              {
                num2 = premiumBonuses[index2].coin <= 0 || premiumBonuses[index2].item != null ? (premiumBonuses[index2].gold <= 0 || premiumBonuses[index2].item != null ? (premiumBonuses[index2].item == null || premiumBonuses[index2].item.Length != 1 || premiumBonuses[index2].coin != 0 ? 1 : premiumBonuses[index2].item[0].num) : premiumBonuses[index2].gold) : premiumBonuses[index2].coin;
                bool flag2 = false;
                ItemParam itemParam = instance.MasterParam.GetItemParam(str, false);
                if (itemParam != null)
                {
                  data.Set(str, GiftTypes.Item, itemParam.rare, num2);
                  data.name = itemParam.name;
                  flag2 = true;
                }
                ConceptCardParam conceptCardParam = instance.MasterParam.GetConceptCardParam(str);
                if (conceptCardParam != null)
                {
                  data.Set(str, GiftTypes.ConceptCard, conceptCardParam.rare, num2);
                  data.name = conceptCardParam.name;
                  flag2 = true;
                }
                if (!flag2 && instance.MasterParam.GetArtifactParam(str, false) != null)
                {
                  ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(str);
                  if (artifactParam != null)
                  {
                    data.Set(str, GiftTypes.Artifact, artifactParam.rareini, num2);
                    data.name = artifactParam.name;
                    flag2 = true;
                  }
                }
                if (!flag2 && instance.MasterParam.ContainsUnitID(str))
                {
                  UnitParam unitParam = instance.MasterParam.GetUnitParam(str);
                  if (unitParam != null)
                  {
                    data.Set(str, GiftTypes.Unit, (int) unitParam.rare, num2);
                    data.name = unitParam.name;
                    flag2 = true;
                  }
                }
                if (!flag2 && instance.MasterParam.ContainsAwardID(str))
                {
                  AwardParam awardParam = instance.MasterParam.GetAwardParam(str);
                  if (awardParam != null)
                  {
                    data.Set(str, GiftTypes.Award, 1, num2);
                    data.name = awardParam.name;
                    flag2 = true;
                  }
                }
                if (!flag2)
                  DebugUtility.LogError(string.Format("不明な識別子が報酬として設定されています。itemID => {0}", (object) str));
              }
              else if (premiumBonuses[index2].gold > 0)
              {
                data.Set(string.Empty, GiftTypes.Gold, 0, premiumBonuses[index2].gold);
                data.name = premiumBonuses[index2].gold.ToString() + LocalizedText.Get("sys.GOLD");
                num2 = premiumBonuses[index2].gold;
              }
              else
                DebugUtility.LogError(string.Format("itemIDが設定できない premiumLoginBonusList index => {0} ", (object) index2));
              int num3 = this.mLoginBonusCount - (!this.IsConfigWindow ? 1 : 0);
              ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(index2 >= num3 ? this.Item_Normal : this.Item_Taken);
              ((UnityEngine.Object) ((Component) listItemEvents).gameObject).name = index2.ToString();
              listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
              Transform transform2 = ((Component) listItemEvents).transform.Find(this.ItemIconCountPath);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform2, (UnityEngine.Object) null))
                ((Component) transform2).GetComponent<Text>().text = (index2 + 1).ToString();
              Transform transform3 = ((Component) listItemEvents).transform.Find(this.ItemNumPath);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform3, (UnityEngine.Object) null) && num2 > 0)
                ((Component) transform3).GetComponent<Text>().text = num2.ToString();
              SetItemObject component1 = ((Component) listItemEvents).GetComponent<SetItemObject>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null) && (data.type == GiftTypes.Gold || data.type == GiftTypes.Award))
                component1.SetIconActive(data.type);
              DataSource.Bind<GiftRecieveItemData>(((Component) listItemEvents).gameObject, data);
              if (!flag1)
              {
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TodayBadge, (UnityEngine.Object) null) && index2 == this.mLoginBonusCount - 1)
                {
                  ((Transform) this.TodayBadge).SetParent(((Component) listItemEvents).transform, false);
                  this.TodayBadge.anchoredPosition = Vector2.zero;
                  ((Component) this.TodayBadge).gameObject.SetActive(true);
                }
                else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TommorowBadge, (UnityEngine.Object) null) && index2 == this.mLoginBonusCount)
                {
                  ((Transform) this.TommorowBadge).SetParent(((Component) listItemEvents).transform, false);
                  this.TommorowBadge.anchoredPosition = Vector2.zero;
                  ((Component) this.TommorowBadge).gameObject.SetActive(true);
                }
              }
              if (index2 < this.mLoginBonusCount - 1)
              {
                Transform transform4 = ((Component) listItemEvents).transform.Find(this.CheckName);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform4, (UnityEngine.Object) null))
                {
                  Animator component2 = ((Component) transform4).GetComponent<Animator>();
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
                    ((Behaviour) component2).enabled = false;
                }
              }
              if (index2 == 0)
                this.DisableFirstDayHiddenOject(((Component) listItemEvents).gameObject);
              GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
              ((UnityEngine.Object) gameObject2).name = ((UnityEngine.Object) gameObject2).name + (index2 + 1).ToString();
              gameObject2.transform.SetParent(transform1, false);
              gameObject2.SetActive(true);
              ((Component) listItemEvents).transform.SetParent(gameObject2.transform, false);
              ((Component) listItemEvents).gameObject.SetActive(true);
              GameParameter.UpdateAll(((Component) listItemEvents).gameObject);
              this.mItems.Add(listItemEvents);
            }
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.WindowMessageText, (UnityEngine.Object) null))
            this.WindowMessageText.text = string.Format(LocalizedText.Get(this.WindowMessageText.text), (object) this.mLoginBonusCount);
          int num = (this.mLoginBonusCount - 1) / this.PateItemNum;
          for (int index = 0; index < this.mViewLists.Count; ++index)
          {
            if (index == num)
              this.mViewLists[index].SetActive(true);
            else
              this.mViewLists[index].SetActive(false);
          }
          this.mPageNum = num + 1;
          this.PageCountText.text = this.mPageNum.ToString();
          this.PageCountTextMax.text = this.mPageNumMax.ToString();
          if (this.mPageNumMax == 1)
          {
            ((Selectable) this.PrevPageButton).interactable = false;
            ((Selectable) this.NextPageButton).interactable = false;
          }
          else if (this.mPageNum == 1)
            ((Selectable) this.PrevPageButton).interactable = false;
          else if (this.mPageNum == this.mPageNumMax)
            ((Selectable) this.NextPageButton).interactable = false;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
        }
      }
    }

    private void OnItemSelect(GameObject go)
    {
      if (DataSource.FindDataOfClass<GiftRecieveItemData>(go, (GiftRecieveItemData) null) == null)
        return;
      GlobalVars.SelectedItemID = ((UnityEngine.Object) go).name;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void DisableFirstDayHiddenOject(GameObject parent)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) parent, (UnityEngine.Object) null) || this.DisabledFirstDayNames == null)
        return;
      for (int index = 0; index < this.DisabledFirstDayNames.Length; ++index)
      {
        string disabledFirstDayName = this.DisabledFirstDayNames[index];
        if (!string.IsNullOrEmpty(disabledFirstDayName))
        {
          Transform transform = parent.transform.Find(disabledFirstDayName);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
            ((Component) transform).gameObject.SetActive(false);
        }
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (!this.IsConfigWindow)
          {
            this.FlipTodaysItem();
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 107);
          break;
        case 2:
          this.PushPrevButton();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 106);
          break;
        case 3:
          this.PushNextButton();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 106);
          break;
        case 4:
          this.PushNextButton();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 109);
          break;
      }
    }

    private void FlipTodaysItem()
    {
      if (this.mLoginBonusCount <= 0 || this.mItems.Count < this.mLoginBonusCount)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 105);
      }
      else
      {
        int index = Math.Max(this.mLoginBonusCount - 1, 0);
        ListItemEvents mItem = this.mItems[index];
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusParticleEffect, (UnityEngine.Object) null))
          UIUtility.SpawnParticle(this.BonusParticleEffect, ((Component) mItem).transform as RectTransform, new Vector2(0.5f, 0.5f));
        GiftRecieveItemData dataOfClass = DataSource.FindDataOfClass<GiftRecieveItemData>(((Component) mItem).gameObject, (GiftRecieveItemData) null);
        ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.Item_Taken);
        DataSource.Bind<GiftRecieveItemData>(((Component) listItemEvents).gameObject, dataOfClass);
        ((Component) listItemEvents).transform.SetParent(((Component) mItem).transform.parent, false);
        ((Component) listItemEvents).transform.SetSiblingIndex(((Component) mItem).transform.GetSiblingIndex());
        listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
        ((UnityEngine.Object) ((Component) listItemEvents).gameObject).name = index.ToString();
        GiftRecieveItem componentInChildren = ((Component) listItemEvents).GetComponentInChildren<GiftRecieveItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
          componentInChildren.UpdateValue();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TodayBadge, (UnityEngine.Object) null))
        {
          ((Transform) this.TodayBadge).SetParent(((Component) listItemEvents).transform, false);
          Transform transform1 = ((Component) listItemEvents).transform.Find(this.ItemIconCountPath);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform1, (UnityEngine.Object) null))
            ((Component) transform1).GetComponent<Text>().text = this.mLoginBonusCount.ToString();
          Transform transform2 = ((Component) mItem).transform.Find(this.ItemNumPath);
          Transform transform3 = ((Component) listItemEvents).transform.Find(this.ItemNumPath);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform3, (UnityEngine.Object) null))
            ((Component) transform3).GetComponent<Text>().text = ((Component) transform2).GetComponent<Text>().text;
          SetItemObject component = ((Component) listItemEvents).GetComponent<SetItemObject>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && (dataOfClass.type == GiftTypes.Gold || dataOfClass.type == GiftTypes.Award))
            component.SetIconActive(dataOfClass.type);
        }
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) mItem).gameObject);
        ((Component) listItemEvents).gameObject.SetActive(true);
        Transform transform = ((Component) listItemEvents).transform.Find(this.CheckName);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
        {
          Animator component = ((Component) transform).GetComponent<Animator>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            ((Behaviour) component).enabled = true;
        }
        if (index == 0)
          this.DisableFirstDayHiddenOject(((Component) listItemEvents).gameObject);
        this.mItems[index] = listItemEvents;
        if (this.mLoginBonusCount == this.mPageNum * this.PateItemNum && this.mPageNum < this.mPageNumMax)
        {
          ((Selectable) this.PrevPageButton).interactable = false;
          ((Selectable) this.NextPageButton).interactable = false;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 108);
        }
        else
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
    }

    private void PushPrevButton()
    {
      if (this.mPageNum <= 1)
        return;
      --this.mPageNum;
      for (int index = 0; index < this.mViewLists.Count; ++index)
      {
        if (index == this.mPageNum - 1)
          this.mViewLists[index].SetActive(true);
        else
          this.mViewLists[index].SetActive(false);
      }
      if (this.mPageNum == 1)
        ((Selectable) this.PrevPageButton).interactable = false;
      ((Selectable) this.NextPageButton).interactable = true;
      this.PageCountText.text = this.mPageNum.ToString();
    }

    private void PushNextButton()
    {
      if (this.mPageNum >= this.mPageNumMax)
        return;
      ++this.mPageNum;
      for (int index = 0; index < this.mViewLists.Count; ++index)
      {
        if (index == this.mPageNum - 1)
          this.mViewLists[index].SetActive(true);
        else
          this.mViewLists[index].SetActive(false);
      }
      if (this.mPageNum == this.mPageNumMax)
        ((Selectable) this.NextPageButton).interactable = false;
      ((Selectable) this.PrevPageButton).interactable = true;
      this.PageCountText.text = this.mPageNum.ToString();
    }
  }
}
