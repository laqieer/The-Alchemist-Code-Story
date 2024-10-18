// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPieceShopBuyWindow
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
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Item Select", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "Bought", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(11, "Sort Window Open", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "Sort Window Close", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(13, "Sort Decide", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(14, "Sort Toggle Change", FlowNode.PinTypes.Input, 14)]
  [FlowNode.Pin(21, "Filter Window Open", FlowNode.PinTypes.Input, 21)]
  [FlowNode.Pin(22, "Filter Window Close", FlowNode.PinTypes.Input, 22)]
  [FlowNode.Pin(23, "Filter Decide", FlowNode.PinTypes.Input, 23)]
  [FlowNode.Pin(25, "Filter Toggle All", FlowNode.PinTypes.Input, 25)]
  [FlowNode.Pin(26, "Filter Toggle Clear", FlowNode.PinTypes.Input, 26)]
  [FlowNode.Pin(27, "Filter Tab Change", FlowNode.PinTypes.Input, 27)]
  [FlowNode.Pin(100, "Buy Window Open", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(104, "商品の期限切れ警告表示", FlowNode.PinTypes.Output, 104)]
  public class UnitPieceShopBuyWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_REFRESH = 1;
    private const int PIN_IN_SELECT_ITEM = 2;
    private const int PIN_IN_BOUGHT = 3;
    private const int PIN_IN_SORT_WINDOW_OPEN = 11;
    private const int PIN_IN_SORT_WINDOW_CLOSE = 12;
    private const int PIN_IN_SORT_WINDOW_OK = 13;
    private const int PIN_IN_SORT_WINDOW_TGLCHANGE = 14;
    private const int PIN_IN_FILTER_WINDOW_OPEN = 21;
    private const int PIN_IN_FILTER_WINDOW_CLOSE = 22;
    private const int PIN_IN_FILTER_WINDOW_OK = 23;
    private const int PIN_IN_FILTER_WINDOW_ALL = 25;
    private const int PIN_IN_FILTER_WINDOW_CLEAR = 26;
    private const int PIN_IN_FILTER_WINDOW_TABCHANGE = 27;
    private const int PIN_OUT_OPEN_POPUP = 100;
    [SerializeField]
    private Text TextCoinNum;
    [SerializeField]
    private GameObject ImageCoinIcon;
    [SerializeField]
    private ContentController mContentController;
    private UnitPieceShopBuyWindow.ItemSource mItemSource;
    [SerializeField]
    private TabMaker m_Tab;
    [SerializeField]
    private Toggle mSoldOutToggle;
    public UnitListSortWindow.SerializeParam m_SortWindowParam;
    public UnitListFilterWindow.SerializeParam m_FilterWindowParam;
    private UnitListSortWindow mSortWindow;
    private UnitListFilterWindow mFilterWindow;
    private UnitListSortWindow.SelectType mSortType;
    [SerializeField]
    private Text mSortButtonText;
    [SerializeField]
    private Image mFilterButtonImage;
    [SerializeField]
    private Sprite mFilterImageDefault;
    [SerializeField]
    private Sprite mFilterImageOn;

    private bool TabEqual(UnitPieceShopBuyWindow.Tab tab, EElement element)
    {
      switch (tab)
      {
        case UnitPieceShopBuyWindow.Tab.FIRE:
          return element == EElement.Fire;
        case UnitPieceShopBuyWindow.Tab.WATER:
          return element == EElement.Water;
        default:
          if (tab == UnitPieceShopBuyWindow.Tab.THUNDER)
            return element == EElement.Thunder;
          if (tab == UnitPieceShopBuyWindow.Tab.WIND)
            return element == EElement.Wind;
          if (tab == UnitPieceShopBuyWindow.Tab.LIGHT)
            return element == EElement.Shine;
          if (tab == UnitPieceShopBuyWindow.Tab.DARK)
            return element == EElement.Dark;
          return tab == UnitPieceShopBuyWindow.Tab.ALL;
      }
    }

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mContentController, (UnityEngine.Object) null))
        this.mContentController.SetWork((object) this);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Tab, (UnityEngine.Object) null))
      {
        this.m_Tab.Create(new string[7]
        {
          UnitPieceShopBuyWindow.Tab.ALL.ToString(),
          UnitPieceShopBuyWindow.Tab.FIRE.ToString(),
          UnitPieceShopBuyWindow.Tab.WATER.ToString(),
          UnitPieceShopBuyWindow.Tab.THUNDER.ToString(),
          UnitPieceShopBuyWindow.Tab.WIND.ToString(),
          UnitPieceShopBuyWindow.Tab.LIGHT.ToString(),
          UnitPieceShopBuyWindow.Tab.DARK.ToString()
        }, new Action<GameObject, SerializeValueList>(this.SetupTabNode));
        this.m_Tab.SetOn((Enum) UnitPieceShopBuyWindow.Tab.ALL, true);
      }
      this.mSortWindow = Activator.CreateInstance(this.m_SortWindowParam.type) as UnitListSortWindow;
      if (this.mSortWindow != null)
        this.mSortWindow.Initialize((FlowWindowBase.SerializeParamBase) this.m_SortWindowParam);
      this.mFilterWindow = Activator.CreateInstance(this.m_FilterWindowParam.type) as UnitListFilterWindow;
      if (this.mFilterWindow != null)
        this.mFilterWindow.Initialize((FlowWindowBase.SerializeParamBase) this.m_FilterWindowParam);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSoldOutToggle, (UnityEngine.Object) null))
        return;
      ((Component) this.mSoldOutToggle).gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mContentController, (UnityEngine.Object) null))
        this.mContentController.Release();
      this.mContentController = (ContentController) null;
      this.mItemSource = (UnitPieceShopBuyWindow.ItemSource) null;
      GlobalVars.TimeOutShopItems = (List<ShopItem>) null;
    }

    private void Update()
    {
      if (this.mSortWindow != null)
      {
        this.mSortWindow.Update();
        UnitListSortWindow.SelectType sectionReg = this.mSortWindow.GetSectionReg();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSortButtonText, (UnityEngine.Object) null) && this.mSortType != sectionReg)
        {
          this.mSortType = sectionReg;
          string text = UnitListSortWindow.GetText(this.mSortType);
          if (!string.IsNullOrEmpty(text) && this.mSortButtonText.text != text)
            this.mSortButtonText.text = text;
        }
      }
      if (this.mFilterWindow == null)
        return;
      this.mFilterWindow.Update();
      if (this.mFilterWindow.GetSelectReg().Count > 0)
        this.mFilterButtonImage.sprite = this.mFilterImageOn;
      else
        this.mFilterButtonImage.sprite = this.mFilterImageDefault;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 21:
          this.mFilterWindow.OnActivate(600);
          break;
        case 22:
          this.mFilterWindow.OnActivate(610);
          break;
        case 23:
          this.mFilterWindow.OnActivate(620);
          this.Refresh();
          break;
        case 25:
          this.mFilterWindow.OnActivate(640);
          break;
        case 26:
          this.mFilterWindow.OnActivate(650);
          break;
        case 27:
          this.mFilterWindow.OnActivate(660);
          break;
        default:
          switch (pinID - 11)
          {
            case 0:
              this.mSortWindow.OnActivate(500);
              return;
            case 1:
              this.mSortWindow.OnActivate(510);
              return;
            case 2:
              this.mSortWindow.OnActivate(520);
              this.Refresh();
              return;
            case 3:
              this.mSortWindow.OnActivate(530);
              return;
            default:
              switch (pinID - 1)
              {
                case 0:
                  this.Refresh();
                  ButtonEvent.ResetLock("unitlist");
                  return;
                case 1:
                  this.OnSelect();
                  return;
                case 2:
                  this.Refresh(true);
                  ButtonEvent.ResetLock("unitlist");
                  return;
                default:
                  return;
              }
          }
      }
    }

    private void SetupTabNode(GameObject gobj, SerializeValueList value)
    {
      TabMaker.Element element = value.GetObject<TabMaker.Element>("element", (TabMaker.Element) null);
      if (element == null)
        return;
      object obj = Enum.Parse(typeof (UnitPieceShopBuyWindow.Tab), element.key);
      if (obj == null)
        return;
      element.value = (int) obj;
    }

    private UnitPieceShopBuyWindow.Tab GetCurrentTab()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Tab, (UnityEngine.Object) null))
      {
        TabMaker.Info onIfno = this.m_Tab.GetOnIfno();
        if (onIfno != null)
          return (UnitPieceShopBuyWindow.Tab) onIfno.element.value;
      }
      return UnitPieceShopBuyWindow.Tab.ALL;
    }

    private void Refresh(bool keepScrollPos = false)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mContentController, (UnityEngine.Object) null))
        return;
      this.mContentController.scroller.StopMovement();
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      player.UpdateEventCoin();
      this.SetPossessionCoinParam();
      GameParameter.UpdateAll(((Component) this).gameObject);
      DebugUtility.Assert(player.UnitPieceShopData != null, "ショップ情報が存在しない");
      FilterUtility.FilterPrefs filter = FilterUtility.Load_UnitFilter();
      UnitPieceShopBuyWindow.Tab currentTab = this.GetCurrentTab();
      this.mItemSource = new UnitPieceShopBuyWindow.ItemSource();
      List<UnitData> list = new List<UnitData>();
      Dictionary<string, UnitPieceShopItem> dict = new Dictionary<string, UnitPieceShopItem>();
      for (int index = 0; index < player.UnitPieceShopData.ShopItems.Count; ++index)
      {
        UnitPieceShopItem shopItem = player.UnitPieceShopData.ShopItems[index];
        UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParamForPiece(shopItem.IName).iname);
        if (unitDataByUnitId != null && this.TabEqual(currentTab, unitDataByUnitId.UnitParam.element) && (!this.mSoldOutToggle.isOn || !shopItem.IsSoldOut) && FilterUtility.MatchCondition(unitDataByUnitId.UnitParam, filter))
        {
          list.Add(unitDataByUnitId);
          dict.Add(unitDataByUnitId.UnitParam.iname, shopItem);
        }
      }
      this.SortItem(dict, list);
      for (int index = 0; index < list.Count; ++index)
        this.mItemSource.Add(new UnitPieceShopBuyWindow.ItemSource.ItemParam(dict[list[index].UnitParam.iname]));
      if (keepScrollPos)
        this.mContentController.Initialize((ContentSource) this.mItemSource, this.mContentController.anchoredPosition);
      else
        this.mContentController.Initialize((ContentSource) this.mItemSource, Vector2.zero);
    }

    private void SortItem(Dictionary<string, UnitPieceShopItem> dict, List<UnitData> list)
    {
      SortUtility.StableSort<UnitData>(list, (Comparison<UnitData>) ((p1, p2) =>
      {
        if (!dict[p1.UnitParam.iname].IsSoldOut && dict[p2.UnitParam.iname].IsSoldOut)
          return -1;
        return dict[p1.UnitParam.iname].IsSoldOut && !dict[p2.UnitParam.iname].IsSoldOut ? 1 : this.GetSortPriority(p2, UnitListSortWindow.SelectType.RARITY).CompareTo(this.GetSortPriority(p1, UnitListSortWindow.SelectType.RARITY));
      }));
    }

    private int GetSortPriority(UnitData unit, UnitListSortWindow.SelectType type)
    {
      StatusParam statusParam = unit.Status.param;
      switch (type)
      {
        case UnitListSortWindow.SelectType.TIME:
          return MonoSingleton<GameManager>.Instance.Player.Units.IndexOf(unit);
        case UnitListSortWindow.SelectType.RARITY:
          return (int) unit.UnitParam.rare;
        case UnitListSortWindow.SelectType.LEVEL:
          return unit.Lv;
        case UnitListSortWindow.SelectType.TOTAL:
          return (int) statusParam.atk + (int) statusParam.def + (int) statusParam.mag + (int) statusParam.mnd + (int) statusParam.spd + (int) statusParam.dex + (int) statusParam.cri + (int) statusParam.luk;
        default:
          if (type == UnitListSortWindow.SelectType.ATK)
            return (int) statusParam.atk;
          if (type == UnitListSortWindow.SelectType.DEF)
            return (int) statusParam.def;
          if (type == UnitListSortWindow.SelectType.MAG)
            return (int) statusParam.mag;
          if (type == UnitListSortWindow.SelectType.MND)
            return (int) statusParam.mnd;
          if (type == UnitListSortWindow.SelectType.HP)
            return (int) statusParam.hp;
          if (type == UnitListSortWindow.SelectType.SPD)
            return (int) statusParam.spd;
          if (type == UnitListSortWindow.SelectType.COMBINATION)
            return unit.GetCombination();
          if (type == UnitListSortWindow.SelectType.JOBRANK)
            return unit.CurrentJob.Rank;
          if (type == UnitListSortWindow.SelectType.AWAKE)
            return unit.AwakeLv;
          if (type == UnitListSortWindow.SelectType.DEX)
            return (int) statusParam.dex;
          if (type == UnitListSortWindow.SelectType.CRI)
            return (int) statusParam.cri;
          return type == UnitListSortWindow.SelectType.LUK ? (int) statusParam.luk : 0;
      }
    }

    private void OnSelect()
    {
      this.mContentController.scroller.StopMovement();
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      ItemParam itemParam = currentValue.GetDataSource<ItemParam>("_self");
      if (itemParam == null)
        return;
      UnitPieceShopItem unitPieceShopItem = MonoSingleton<GameManager>.Instance.Player.UnitPieceShopData.ShopItems.Find((Predicate<UnitPieceShopItem>) (shopItem => shopItem.IName == itemParam.iname));
      if (unitPieceShopItem == null)
        return;
      GlobalVars.BuyUnitPieceShopItem = unitPieceShopItem;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void SetPossessionCoinParam()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ImageCoinIcon, (UnityEngine.Object) null))
        DataSource.Bind<ItemParam>(this.ImageCoinIcon, MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.EventShopItem.shop_cost_iname));
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextCoinNum, (UnityEngine.Object) null))
        return;
      DataSource.Bind<EventCoinData>(((Component) this.TextCoinNum).gameObject, MonoSingleton<GameManager>.Instance.Player.EventCoinList.Find((Predicate<EventCoinData>) (f => f.iname.Equals(GlobalVars.EventShopItem.shop_cost_iname))));
    }

    private enum Tab
    {
      NONE = 0,
      FAVORITE = 1,
      FIRE = 2,
      WATER = 4,
      THUNDER = 8,
      WIND = 16, // 0x00000010
      LIGHT = 32, // 0x00000020
      DARK = 64, // 0x00000040
      ALL = 65535, // 0x0000FFFF
    }

    public class ItemSource : ContentSource
    {
      private List<UnitPieceShopBuyWindow.ItemSource.ItemParam> m_Params = new List<UnitPieceShopBuyWindow.ItemSource.ItemParam>();

      public override ContentNode Instantiate(ContentNode res)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(((Component) res).gameObject);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          return (ContentNode) null;
        ContentNode contentNode = gameObject.GetComponent<ContentNode>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) contentNode, (UnityEngine.Object) null))
          contentNode = gameObject.AddComponent<ContentNode>();
        return contentNode;
      }

      public override void Initialize(ContentController controller)
      {
        base.Initialize(controller);
        this.SetTable((ContentSource.Param[]) this.m_Params.ToArray());
      }

      public void Add(UnitPieceShopBuyWindow.ItemSource.ItemParam param)
      {
        if (!param.IsValid())
          return;
        this.m_Params.Add(param);
      }

      public class ItemParam : ContentSource.Param
      {
        private UnitPieceShopItem m_Item;

        public ItemParam(UnitPieceShopItem item) => this.m_Item = item;

        public override bool IsValid() => this.m_Item != null;

        public UnitPieceShopItem data => this.m_Item;

        public override void OnSetup(ContentNode node)
        {
          UnitPieceShopBuyList component = ((Component) node).GetComponent<UnitPieceShopBuyList>();
          DataSource dataSource = DataSource.Create(((Component) node).gameObject);
          dataSource.Clear();
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(this.m_Item.IName);
          dataSource.Add(typeof (ItemParam), (object) itemParam);
          component.SetUp(this.m_Item);
          GameParameter.UpdateAll(((Component) node).gameObject);
        }
      }
    }
  }
}
