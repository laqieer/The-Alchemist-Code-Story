// Decompiled with JetBrains decompiler
// Type: SRPG.ShopSellWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Open", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "売却", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "売却数の選択", FlowNode.PinTypes.Output, 101)]
  public class ShopSellWindow : SRPG_FixedList, IFlowInterface
  {
    public RectTransform ItemLayoutParent;
    public GameObject ItemTemplate;
    public Toggle ToggleShowAll;
    public Toggle ToggleShowUsed;
    public Toggle ToggleShowEquip;
    public Toggle ToggleShowUnitPierce;
    public Toggle ToggleShowItemPierce;
    public Toggle ToggleShowMaterial;
    public Button BtnSort;
    public Button BtnCleared;
    public Button BtnSell;
    public Text TxtSort;
    public string Msg_NoSelection;
    private const int SELL_ITEM_MAX = 10;
    private List<GameObject> mSellItemGameObjects;
    private List<SellItem> mSellItemListSelected;
    private List<SellItem> mSellItemList;
    public ShopSellWindow.SellListConfig ListConfig;
    private ShopSellWindow.FilterTypes mFilterType;
    private static List<EItemType>[] FilterItemTypes = new List<EItemType>[6]
    {
      null,
      new List<EItemType>()
      {
        EItemType.ExpUpPlayer,
        EItemType.ExpUpUnit,
        EItemType.ExpUpSkill,
        EItemType.ExpUpEquip,
        EItemType.ExpUpArtifact,
        EItemType.GoldConvert,
        EItemType.Ticket,
        EItemType.Used,
        EItemType.ApHeal,
        EItemType.ExpGuildFacility
      },
      new List<EItemType>() { EItemType.Equip },
      new List<EItemType>()
      {
        EItemType.ItemPiece,
        EItemType.ItemPiecePiece,
        EItemType.ArtifactPiece
      },
      new List<EItemType>() { EItemType.Material },
      new List<EItemType>() { EItemType.UnitPiece }
    };
    private static string[] SortTypeTexts = new string[2]
    {
      "sys.SORT_INDEX",
      "sys.SORT_RARITY"
    };
    private int[] mSortValues;
    private int filteredCnt;
    private ShopSellWindow.SortTypes sortType;

    protected override int DataCount => this.filteredCnt;

    protected virtual void Awake()
    {
    }

    public override RectTransform ListParent
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemLayoutParent, (UnityEngine.Object) null) ? ((Component) this.ItemLayoutParent).GetComponent<RectTransform>() : (RectTransform) null;
      }
    }

    protected override void Start()
    {
      base.Start();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
      {
        this.ItemTemplate.transform.SetSiblingIndex(0);
        this.ItemTemplate.SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleShowAll, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleShowAll.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__0)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleShowUsed, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleShowUsed.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__1)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleShowEquip, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleShowEquip.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__2)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleShowUnitPierce, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleShowUnitPierce.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__3)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleShowItemPierce, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleShowItemPierce.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__4)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleShowMaterial, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleShowMaterial.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__5)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnSort, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnSort.onClick).AddListener(new UnityAction((object) this, __methodptr(OnSort)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnCleared, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnCleared.onClick).AddListener(new UnityAction((object) this, __methodptr(OnCleared)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnSell, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnSell.onClick).AddListener(new UnityAction((object) this, __methodptr(OnSell)));
      }
      this.mPageSize = this.CellCount;
      List<ItemData> currentItem = this.getCurrentItem();
      this.mSellItemList = new List<SellItem>();
      this.mSellItemListSelected = new List<SellItem>(10);
      this.mSellItemGameObjects = new List<GameObject>(currentItem.Count);
      this.firstSetupDisplayItem();
      this.sortType = ShopSellWindow.SortTypes.Index;
      this.mFilterType = ShopSellWindow.FilterTypes.All;
      ((Component) this.ItemLayoutParent).transform.position = new Vector3((float) (Screen.width / 2), ((Component) this.ItemLayoutParent).transform.position.y, ((Component) this.ItemLayoutParent).transform.position.z);
    }

    protected override void Update() => base.Update();

    public void Activated(int pinID)
    {
      if (pinID == 1)
        this.Refresh();
      if (pinID != 2)
        return;
      this.OnCleared();
    }

    protected override GameObject CreateItem() => UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);

    private bool isSellNGUnit(ItemData item)
    {
      List<UnitData> units = MonoSingleton<GameManager>.Instance.Player.Units;
      UnitParam uParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParamForPiece(item.ItemID, false);
      if (uParam == null)
        return true;
      UnitData unitData = units.Find((Predicate<UnitData>) (u => u.UnitParam.iname == uParam.iname));
      return unitData == null || unitData.GetRarityCap() > unitData.Rarity || unitData.AwakeLv < (int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam(unitData.GetRarityCap()).UnitAwakeLvCap;
    }

    private void adjustPages()
    {
      if (this.mPageSize > 0)
      {
        this.mMaxPages = (this.DataCount + this.mPageSize - 1) / this.mPageSize;
        if (this.mMaxPages < 1)
          this.mMaxPages = 1;
      }
      else
        this.mMaxPages = 1;
      this.mPage = Mathf.Clamp(this.mPage, 0, this.mMaxPages - 1);
    }

    private List<ItemData> getCurrentItem()
    {
      List<ItemData> currentItem = new List<ItemData>();
      List<EItemType> filterItemType = ShopSellWindow.FilterItemTypes[(int) this.mFilterType];
      if (this.ListConfig.MaxGentotuOnly)
      {
        foreach (ItemData itemData in MonoSingleton<GameManager>.Instance.Player.Items)
        {
          if (itemData.Num > 0 && itemData.ItemType == EItemType.UnitPiece && !this.isSellNGUnit(itemData))
            currentItem.Add(itemData);
        }
      }
      else
      {
        foreach (ItemData itemData in MonoSingleton<GameManager>.Instance.Player.Items)
        {
          if (itemData.Num > 0 && itemData.ItemType != EItemType.Other && itemData.ItemType != EItemType.EventCoin && !itemData.Param.is_valuables)
          {
            if (filterItemType != null)
            {
              foreach (EItemType eitemType in filterItemType)
              {
                if (itemData.ItemType == eitemType)
                {
                  currentItem.Add(itemData);
                  break;
                }
              }
            }
            else
              currentItem.Add(itemData);
          }
        }
      }
      this.filteredCnt = currentItem.Count;
      switch (this.sortType)
      {
        case ShopSellWindow.SortTypes.Index:
          currentItem.Sort((Comparison<ItemData>) ((src, dsc) => src.No - dsc.No));
          break;
        case ShopSellWindow.SortTypes.Rarity:
          currentItem.Sort((Comparison<ItemData>) ((src, dsc) => dsc.Rarity == src.Rarity ? src.No - dsc.No : dsc.Rarity - src.Rarity));
          break;
        default:
          Debug.Log((object) ("invalid sortType:" + (object) this.sortType));
          break;
      }
      this.adjustPages();
      int count = this.filteredCnt - this.mPage * this.mPageSize;
      if (count > this.mPageSize)
        count = this.mPageSize;
      if (count > 0)
        currentItem = currentItem.GetRange(this.mPage * this.mPageSize, count);
      else
        Debug.Log((object) ("invalid get:" + (object) count));
      return currentItem;
    }

    protected override void OnItemSelect(GameObject go) => this.OnSelect(go);

    private void UpdateSellIndex()
    {
      for (int index = 0; index < this.mSellItemListSelected.Count; ++index)
        this.mSellItemListSelected[index].index = index;
    }

    private SellItem SearchFromSelectedItem(ItemData item)
    {
      for (int index = 0; index < this.mSellItemListSelected.Count; ++index)
      {
        if (this.mSellItemListSelected[index].item == item)
          return this.mSellItemListSelected[index];
      }
      return (SellItem) null;
    }

    private SellItem CreateOrSearchSellItem(ItemData item)
    {
      SellItem orSearchSellItem = this.SearchFromSelectedItem(item);
      if (orSearchSellItem == null)
      {
        orSearchSellItem = new SellItem();
        orSearchSellItem.item = item;
        orSearchSellItem.num = 0;
        orSearchSellItem.index = -1;
      }
      else
        Debug.Log((object) ("Exist SellItem num=" + (object) orSearchSellItem.num));
      return orSearchSellItem;
    }

    private GameObject createDisplayObject()
    {
      GameObject displayObject = this.CreateItem();
      displayObject.transform.SetParent((Transform) this.ListParent, false);
      ListItemEvents component = displayObject.GetComponent<ListItemEvents>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
      return displayObject;
    }

    private void firstSetupDisplayItem()
    {
      for (int index = 0; index < this.mPageSize; ++index)
        this.mSellItemGameObjects.Add(this.createDisplayObject());
    }

    private void UpdateDispalyItem(List<ItemData> list)
    {
      List<SellItem> sellItemList = new List<SellItem>();
      for (int index = 0; index < this.mSellItemGameObjects.Count; ++index)
      {
        GameObject sellItemGameObject = this.mSellItemGameObjects[index];
        sellItemGameObject.SetActive(true);
        if (index < list.Count)
        {
          SellItem orSearchSellItem = this.CreateOrSearchSellItem(list[index]);
          sellItemList.Add(orSearchSellItem);
          DataSource.Bind<SellItem>(sellItemGameObject, orSearchSellItem);
          sellItemGameObject.transform.localScale = Vector3.one;
        }
        else
          sellItemGameObject.transform.localScale = Vector3.zero;
      }
      this.mSellItemList = sellItemList;
    }

    private void createDisplaySellItem(List<ItemData> list)
    {
      List<SellItem> sellItemList = new List<SellItem>();
      foreach (ItemData itemData in list)
      {
        GameObject gameObject = this.CreateItem();
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("CreateItem returned NULL");
          return;
        }
        gameObject.transform.SetParent((Transform) this.ListParent, false);
        ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        if (itemData.ItemType != EItemType.Other)
        {
          this.mSellItemGameObjects.Add(gameObject);
          SellItem orSearchSellItem = this.CreateOrSearchSellItem(itemData);
          sellItemList.Add(orSearchSellItem);
          DataSource.Bind<SellItem>(gameObject, orSearchSellItem);
        }
      }
      this.mSellItemList = sellItemList;
    }

    protected override void RefreshItems()
    {
      List<ItemData> currentItem = this.getCurrentItem();
      this.UpdateDispalyItem(currentItem);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtSort, (UnityEngine.Object) null))
        this.TxtSort.text = LocalizedText.Get(ShopSellWindow.SortTypeTexts[(int) this.sortType]);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListConfig.EmptyTemplate, (UnityEngine.Object) null))
      {
        bool flag = currentItem.Count == 0;
        this.ListConfig.EmptyTemplate.SetActive(this.ListConfig.ShowEmpty && flag);
      }
      this.UpdateSellIndex();
      DataSource.Bind<List<SellItem>>(((Component) this).gameObject, this.mSellItemListSelected);
      GlobalVars.SelectSellItem = (SellItem) null;
      GlobalVars.SellItemList = (List<SellItem>) null;
      GameParameter.UpdateAll(((Component) this).gameObject);
      this.UpdatePage();
    }

    public override void UpdatePage()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageScrollBar, (UnityEngine.Object) null))
      {
        if (this.mMaxPages >= 2)
        {
          this.PageScrollBar.size = 1f / (float) this.mMaxPages;
          this.PageScrollBar.value = (float) this.mPage / ((float) this.mMaxPages - 1f);
        }
        else
        {
          this.PageScrollBar.size = 1f;
          this.PageScrollBar.value = 0.0f;
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageIndex, (UnityEngine.Object) null))
        this.PageIndex.text = Mathf.Min(this.mPage + 1, this.mMaxPages).ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageIndexMax, (UnityEngine.Object) null))
        this.PageIndexMax.text = this.mMaxPages.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ForwardButton, (UnityEngine.Object) null))
        ((Selectable) this.ForwardButton).interactable = this.mPage < this.mMaxPages - 1;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.BackButton).interactable = this.mPage > 0;
    }

    private void OnSelect(GameObject go)
    {
      if (this.IsRefreshing)
        return;
      SellItem dataOfClass = DataSource.FindDataOfClass<SellItem>(go, (SellItem) null);
      if (dataOfClass == null || dataOfClass.item == null || dataOfClass.item.Num == 0)
      {
        Debug.Log((object) "invalid state");
      }
      else
      {
        GlobalVars.SelectSellItem = dataOfClass;
        GlobalVars.SellItemList = this.mSellItemListSelected;
        ItemData itemData = dataOfClass.item;
        if (GlobalVars.ShopType != EShopType.AwakePiece && !this.mSellItemListSelected.Contains(dataOfClass))
        {
          int num1 = 0;
          try
          {
            foreach (SellItem sellItem in this.mSellItemListSelected)
              checked { num1 += sellItem.item.Sell * sellItem.num; }
            int num2 = checked (num1 + MonoSingleton<GameManager>.Instance.Player.Gold + dataOfClass.item.Sell);
          }
          catch (OverflowException ex)
          {
            UIUtility.SystemMessage(LocalizedText.Get("sys.SHOP_CANT_SELL_BY_GOLD_CAPPED"), (UIUtility.DialogResultEvent) null, systemModal: true);
            return;
          }
        }
        if (itemData.Num > 1)
        {
          if (!this.mSellItemListSelected.Contains(dataOfClass) && this.mSellItemListSelected.Count == 10)
            return;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
        }
        else if (this.mSellItemListSelected.Remove(dataOfClass))
        {
          dataOfClass.index = -1;
          dataOfClass.num = 0;
          this.UpdateSellIndex();
          GameParameter.UpdateAll(((Component) this).gameObject);
        }
        else
        {
          if (this.mSellItemListSelected.Count == 10)
            return;
          dataOfClass.num = itemData.Num;
          this.mSellItemListSelected.Add(dataOfClass);
          this.UpdateSellIndex();
          GameParameter.UpdateAll(((Component) this).gameObject);
        }
      }
    }

    private void OnSort()
    {
      if (this.IsRefreshing)
        return;
      this.sortType = (ShopSellWindow.SortTypes) ((int) (this.sortType + 1) % Enum.GetNames(typeof (ShopSellWindow.SortTypes)).Length);
      this.Refresh();
    }

    private void OnCleared()
    {
      if (this.IsRefreshing || this.mSellItemList == null)
        return;
      for (int index = 0; index < this.mSellItemList.Count; ++index)
      {
        this.mSellItemList[index].index = -1;
        this.mSellItemList[index].num = 0;
      }
      this.mSellItemListSelected.Clear();
      this.Refresh();
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void OnSell()
    {
      if (this.IsRefreshing)
        return;
      if (this.mSellItemListSelected.Count == 0)
      {
        if (string.IsNullOrEmpty(this.Msg_NoSelection))
          this.Msg_NoSelection = "sys.CONFIRM_SELL_ITEM_NOT_SELECT";
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(this.Msg_NoSelection), (UIUtility.DialogResultEvent) null);
      }
      else
      {
        GlobalVars.SellItemList = this.mSellItemListSelected;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    [Serializable]
    public class SellListConfig
    {
      public bool MaxGentotuOnly;
      public bool ShowEmpty;
      public GameObject EmptyTemplate;
    }

    [Serializable]
    public enum FilterTypes
    {
      All,
      Used,
      Equip,
      ItemPiece,
      Material,
      UnitPiece,
    }

    private enum SortTypes
    {
      Index,
      Rarity,
    }
  }
}
