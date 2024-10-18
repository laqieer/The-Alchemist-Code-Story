// Decompiled with JetBrains decompiler
// Type: SRPG.ShopSellWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Open", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "売却", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "売却数の選択", FlowNode.PinTypes.Output, 101)]
  public class ShopSellWindow : SRPG_FixedList, IFlowInterface
  {
    private static List<EItemType>[] FilterItemTypes = new List<EItemType>[6]{ null, new List<EItemType>() { EItemType.ExpUpPlayer, EItemType.ExpUpUnit, EItemType.ExpUpSkill, EItemType.ExpUpEquip, EItemType.ExpUpArtifact, EItemType.GoldConvert, EItemType.Ticket, EItemType.Used }, new List<EItemType>() { EItemType.Equip }, new List<EItemType>() { EItemType.ItemPiece, EItemType.ItemPiecePiece, EItemType.ArtifactPiece }, new List<EItemType>() { EItemType.Material }, new List<EItemType>() { EItemType.UnitPiece } };
    private static string[] SortTypeTexts = new string[2]{ "sys.SORT_INDEX", "sys.SORT_RARITY" };
    private const int SELL_ITEM_MAX = 10;
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
    private List<GameObject> mSellItemGameObjects;
    private List<SellItem> mSellItemListSelected;
    private List<SellItem> mSellItemList;
    public ShopSellWindow.SellListConfig ListConfig;
    private ShopSellWindow.FilterTypes mFilterType;
    private int[] mSortValues;
    private int filteredCnt;
    private ShopSellWindow.SortTypes sortType;

    protected override int DataCount
    {
      get
      {
        return this.filteredCnt;
      }
    }

    protected override void Awake()
    {
    }

    public override RectTransform ListParent
    {
      get
      {
        if ((UnityEngine.Object) this.ItemLayoutParent != (UnityEngine.Object) null)
          return this.ItemLayoutParent.GetComponent<RectTransform>();
        return (RectTransform) null;
      }
    }

    protected override void Start()
    {
      base.Start();
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
      {
        this.ItemTemplate.transform.SetSiblingIndex(0);
        this.ItemTemplate.SetActive(false);
      }
      if ((UnityEngine.Object) this.ToggleShowAll != (UnityEngine.Object) null)
        this.ToggleShowAll.onValueChanged.AddListener((UnityAction<bool>) (isActive =>
        {
          if (!isActive)
            return;
          this.mFilterType = ShopSellWindow.FilterTypes.All;
          this.Refresh();
        }));
      if ((UnityEngine.Object) this.ToggleShowUsed != (UnityEngine.Object) null)
        this.ToggleShowUsed.onValueChanged.AddListener((UnityAction<bool>) (isActive =>
        {
          if (!isActive)
            return;
          this.mFilterType = ShopSellWindow.FilterTypes.Used;
          this.Refresh();
        }));
      if ((UnityEngine.Object) this.ToggleShowEquip != (UnityEngine.Object) null)
        this.ToggleShowEquip.onValueChanged.AddListener((UnityAction<bool>) (isActive =>
        {
          if (!isActive)
            return;
          this.mFilterType = ShopSellWindow.FilterTypes.Equip;
          this.Refresh();
        }));
      if ((UnityEngine.Object) this.ToggleShowUnitPierce != (UnityEngine.Object) null)
        this.ToggleShowUnitPierce.onValueChanged.AddListener((UnityAction<bool>) (isActive =>
        {
          if (!isActive)
            return;
          this.mFilterType = ShopSellWindow.FilterTypes.UnitPiece;
          this.Refresh();
        }));
      if ((UnityEngine.Object) this.ToggleShowItemPierce != (UnityEngine.Object) null)
        this.ToggleShowItemPierce.onValueChanged.AddListener((UnityAction<bool>) (isActive =>
        {
          if (!isActive)
            return;
          this.mFilterType = ShopSellWindow.FilterTypes.ItemPiece;
          this.Refresh();
        }));
      if ((UnityEngine.Object) this.ToggleShowMaterial != (UnityEngine.Object) null)
        this.ToggleShowMaterial.onValueChanged.AddListener((UnityAction<bool>) (isActive =>
        {
          if (!isActive)
            return;
          this.mFilterType = ShopSellWindow.FilterTypes.Material;
          this.Refresh();
        }));
      if ((UnityEngine.Object) this.BtnSort != (UnityEngine.Object) null)
        this.BtnSort.onClick.AddListener(new UnityAction(this.OnSort));
      if ((UnityEngine.Object) this.BtnCleared != (UnityEngine.Object) null)
        this.BtnCleared.onClick.AddListener(new UnityAction(this.OnCleared));
      if ((UnityEngine.Object) this.BtnSell != (UnityEngine.Object) null)
        this.BtnSell.onClick.AddListener(new UnityAction(this.OnSell));
      this.mPageSize = this.CellCount;
      List<ItemData> currentItem = this.getCurrentItem();
      this.mSellItemList = new List<SellItem>();
      this.mSellItemListSelected = new List<SellItem>(10);
      this.mSellItemGameObjects = new List<GameObject>(currentItem.Count);
      this.firstSetupDisplayItem();
      this.sortType = ShopSellWindow.SortTypes.Index;
      this.mFilterType = ShopSellWindow.FilterTypes.All;
      this.ItemLayoutParent.transform.position = new Vector3((float) (Screen.width / 2), this.ItemLayoutParent.transform.position.y, this.ItemLayoutParent.transform.position.z);
    }

    protected override void Update()
    {
      base.Update();
    }

    public void Activated(int pinID)
    {
      if (pinID == 1)
        this.Refresh();
      if (pinID != 2)
        return;
      this.OnCleared();
    }

    protected override GameObject CreateItem()
    {
      return UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
    }

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
      List<ItemData> itemDataList = new List<ItemData>();
      List<EItemType> filterItemType = ShopSellWindow.FilterItemTypes[(int) this.mFilterType];
      if (this.ListConfig.MaxGentotuOnly)
      {
        using (List<ItemData>.Enumerator enumerator = MonoSingleton<GameManager>.Instance.Player.Items.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            ItemData current = enumerator.Current;
            if (current.Num > 0 && current.ItemType == EItemType.UnitPiece && !this.isSellNGUnit(current))
              itemDataList.Add(current);
          }
        }
      }
      else
      {
        using (List<ItemData>.Enumerator enumerator1 = MonoSingleton<GameManager>.Instance.Player.Items.GetEnumerator())
        {
          while (enumerator1.MoveNext())
          {
            ItemData current1 = enumerator1.Current;
            if (current1.Num > 0 && current1.ItemType != EItemType.Other && (current1.ItemType != EItemType.EventCoin && !current1.Param.is_valuables))
            {
              if (filterItemType != null)
              {
                using (List<EItemType>.Enumerator enumerator2 = filterItemType.GetEnumerator())
                {
                  while (enumerator2.MoveNext())
                  {
                    EItemType current2 = enumerator2.Current;
                    if (current1.ItemType == current2)
                    {
                      itemDataList.Add(current1);
                      break;
                    }
                  }
                }
              }
              else
                itemDataList.Add(current1);
            }
          }
        }
      }
      this.filteredCnt = itemDataList.Count;
      switch (this.sortType)
      {
        case ShopSellWindow.SortTypes.Index:
          itemDataList.Sort((Comparison<ItemData>) ((src, dsc) => src.No - dsc.No));
          break;
        case ShopSellWindow.SortTypes.Rarity:
          itemDataList.Sort((Comparison<ItemData>) ((src, dsc) =>
          {
            if (dsc.Rarity == src.Rarity)
              return src.No - dsc.No;
            return dsc.Rarity - src.Rarity;
          }));
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
        itemDataList = itemDataList.GetRange(this.mPage * this.mPageSize, count);
      else
        Debug.Log((object) ("invalid get:" + (object) count));
      return itemDataList;
    }

    protected override void OnItemSelect(GameObject go)
    {
      this.OnSelect(go);
    }

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
      SellItem sellItem = this.SearchFromSelectedItem(item);
      if (sellItem == null)
      {
        sellItem = new SellItem();
        sellItem.item = item;
        sellItem.num = 0;
        sellItem.index = -1;
      }
      else
        Debug.Log((object) ("Exist SellItem num=" + (object) sellItem.num));
      return sellItem;
    }

    private GameObject createDisplayObject()
    {
      GameObject gameObject = this.CreateItem();
      gameObject.transform.SetParent((Transform) this.ListParent, false);
      ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
      return gameObject;
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
      using (List<ItemData>.Enumerator enumerator = list.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ItemData current = enumerator.Current;
          GameObject gameObject = this.CreateItem();
          if ((UnityEngine.Object) gameObject == (UnityEngine.Object) null)
          {
            DebugUtility.LogError("CreateItem returned NULL");
            return;
          }
          gameObject.transform.SetParent((Transform) this.ListParent, false);
          ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
          if (current.ItemType != EItemType.Other)
          {
            this.mSellItemGameObjects.Add(gameObject);
            SellItem orSearchSellItem = this.CreateOrSearchSellItem(current);
            sellItemList.Add(orSearchSellItem);
            DataSource.Bind<SellItem>(gameObject, orSearchSellItem);
          }
        }
      }
      this.mSellItemList = sellItemList;
    }

    protected override void RefreshItems()
    {
      List<ItemData> currentItem = this.getCurrentItem();
      this.UpdateDispalyItem(currentItem);
      if ((UnityEngine.Object) this.TxtSort != (UnityEngine.Object) null)
        this.TxtSort.text = LocalizedText.Get(ShopSellWindow.SortTypeTexts[(int) this.sortType]);
      if ((UnityEngine.Object) this.ListConfig.EmptyTemplate != (UnityEngine.Object) null)
        this.ListConfig.EmptyTemplate.SetActive(this.ListConfig.ShowEmpty && currentItem.Count == 0);
      this.UpdateSellIndex();
      DataSource.Bind<List<SellItem>>(this.gameObject, this.mSellItemListSelected);
      GlobalVars.SelectSellItem = (SellItem) null;
      GlobalVars.SellItemList = (List<SellItem>) null;
      GameParameter.UpdateAll(this.gameObject);
      this.UpdatePage();
    }

    public override void UpdatePage()
    {
      if ((UnityEngine.Object) this.PageScrollBar != (UnityEngine.Object) null)
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
      if ((UnityEngine.Object) this.PageIndex != (UnityEngine.Object) null)
        this.PageIndex.text = Mathf.Min(this.mPage + 1, this.mMaxPages).ToString();
      if ((UnityEngine.Object) this.PageIndexMax != (UnityEngine.Object) null)
        this.PageIndexMax.text = this.mMaxPages.ToString();
      if ((UnityEngine.Object) this.ForwardButton != (UnityEngine.Object) null)
        this.ForwardButton.interactable = this.mPage < this.mMaxPages - 1;
      if (!((UnityEngine.Object) this.BackButton != (UnityEngine.Object) null))
        return;
      this.BackButton.interactable = this.mPage > 0;
    }

    private void OnSelect(GameObject go)
    {
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
          GameParameter.UpdateAll(this.gameObject);
        }
        else
        {
          if (this.mSellItemListSelected.Count == 10)
            return;
          dataOfClass.num = itemData.Num;
          this.mSellItemListSelected.Add(dataOfClass);
          this.UpdateSellIndex();
          GameParameter.UpdateAll(this.gameObject);
        }
      }
    }

    private void OnSort()
    {
      this.sortType = (ShopSellWindow.SortTypes) ((int) (this.sortType + 1) % Enum.GetNames(typeof (ShopSellWindow.SortTypes)).Length);
      this.Refresh();
    }

    private void OnCleared()
    {
      if (this.mSellItemList == null)
        return;
      for (int index = 0; index < this.mSellItemList.Count; ++index)
      {
        this.mSellItemList[index].index = -1;
        this.mSellItemList[index].num = 0;
      }
      this.mSellItemListSelected.Clear();
      this.Refresh();
      GameParameter.UpdateAll(this.gameObject);
    }

    private void OnSell()
    {
      if (this.mSellItemListSelected.Count == 0)
      {
        if (string.IsNullOrEmpty(this.Msg_NoSelection))
          this.Msg_NoSelection = "sys.CONFIRM_SELL_ITEM_NOT_SELECT";
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(this.Msg_NoSelection), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
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
