﻿// Decompiled with JetBrains decompiler
// Type: SRPG.GalleryItemListWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "NextPage", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "PrevPage", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "TabChange", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "ソート設定変更ダイアログ表示要求", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(5, "ソート設定変更完了", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(6, "フィルタ設定変更ダイアログ表示要求", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(7, "フィルタ設定変更完了", FlowNode.PinTypes.Input, 7)]
  [FlowNode.Pin(100, "ソート設定変更ダイアログ表示", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "フィルター設定変更ダイアログ表示", FlowNode.PinTypes.Output, 101)]
  public class GalleryItemListWindow : MonoBehaviour, IFlowInterface
  {
    private static readonly int[] DefaultFilter = new int[5]{ 0, 1, 2, 3, 4 };
    private bool mIsRarityAscending = true;
    private bool mIsNameAscending = true;
    private int[] mRareFilters = new int[0];
    private List<GameObject> mItemObjects = new List<GameObject>();
    private Dictionary<EItemTabType, int> mRecentPages = new Dictionary<EItemTabType, int>();
    private Dictionary<EItemTabType, int> mLoadCompletedPage = new Dictionary<EItemTabType, int>();
    private HashSet<string> mItemAvailable = new HashSet<string>();
    private const int OPEN = 0;
    private const int NEXT_PAGE = 1;
    private const int PREV_PAGE = 2;
    private const int TAB_CHANGE = 3;
    private const int REQ_SORT_SETTING = 4;
    private const int UPDATED_SORT_SETTING = 5;
    private const int REQ_FILTER_SETTING = 6;
    private const int UPDATED_FILTER_SETTING = 7;
    private const int OUTPUT_SORT_SETTING = 100;
    private const int OUTPUT_FILTER_SETTING = 101;
    [SerializeField]
    private GridLayoutGroup ItemGrid;
    [SerializeField]
    private Text CurrentPage;
    [SerializeField]
    private Text TotalPage;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private Button NextButton;
    [SerializeField]
    private Button PrevButton;
    [SerializeField]
    private Button FilterButton;
    [SerializeField]
    private Sprite FilterButtonAllOnImage;
    [SerializeField]
    private Sprite FilterButtonNotAllImage;
    [SerializeField]
    private Text SortButtonTitle;
    private int mCellCount;
    private int mCurrentPage;
    private int mTotalPage;
    private int mTotalItemNum;
    private GalleryItemListWindow.SortType mSortType;
    private EItemTabType mCurrentTabType;
    private GalleryItemListWindow.Settings mSettings;
    private List<ItemParam> mAllItems;
    private List<ItemParam> mItems;
    private bool mInitialized;

    private void Start()
    {
      this.mCellCount = this.GetCellCount();
      this.mCurrentTabType = EItemTabType.Used;
      this.mSettings = this.LoadSetting();
      this.mSortType = this.mSettings.sortType;
      this.mIsRarityAscending = this.mSettings.isRarityAscending;
      this.mIsNameAscending = this.mSettings.isNameAscending;
      this.mRareFilters = this.mSettings.rareFilters;
      this.ChangeFilterButtonSprite();
      this.ChangeSortButtonText();
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
        this.ItemTemplate.SetActive(false);
      this.RefreshNewPage(this.mCurrentTabType, 0);
    }

    public void Activated(int pinID)
    {
      if (!this.mInitialized)
        return;
      switch (pinID)
      {
        case 1:
          this.NextPage();
          break;
        case 2:
          this.PrevPage();
          break;
        case 3:
          int num = (FlowNode_ButtonEvent.currentValue as SerializeValueList).GetInt("tabtype");
          if (!Enum.IsDefined(typeof (EItemTabType), (object) num))
            break;
          this.ChangeTab((EItemTabType) num);
          break;
        case 4:
          this.SaveSettingAndOutputPin(100);
          break;
        case 5:
          if (this.mSortType == this.mSettings.sortType && this.mIsRarityAscending == this.mSettings.isRarityAscending && this.mIsNameAscending == this.mSettings.isNameAscending)
            break;
          this.SaveSetting(this.mSettings);
          this.mSortType = this.mSettings.sortType;
          this.mIsRarityAscending = this.mSettings.isRarityAscending;
          this.mIsNameAscending = this.mSettings.isNameAscending;
          this.mCurrentPage = 0;
          this.ChangeSortButtonText();
          this.ClearAllChache();
          this.RefreshNewPage(this.mCurrentTabType, 0);
          break;
        case 6:
          this.SaveSettingAndOutputPin(101);
          break;
        case 7:
          if (this.IsSameFilter(this.mRareFilters, this.mSettings.rareFilters))
            break;
          this.SaveSetting(this.mSettings);
          this.mRareFilters = this.mSettings.rareFilters;
          this.ChangeFilterButtonSprite();
          this.ClearAllChache();
          this.RefreshNewPage(this.mCurrentTabType, 0);
          break;
      }
    }

    private List<ItemParam> GetAvailableItems(EItemTabType tabtype)
    {
      IEnumerable<ItemParam> source = MonoSingleton<GameManager>.Instance.MasterParam.Items.Where<ItemParam>((Func<ItemParam, bool>) (item =>
      {
        if (item.tabtype == tabtype)
          return ((IEnumerable<int>) this.mRareFilters).Any<int>((Func<int, bool>) (rare => rare == item.rare));
        return false;
      }));
      return (this.mSortType != GalleryItemListWindow.SortType.Rarity ? (!this.mIsNameAscending ? (IEnumerable<ItemParam>) source.OrderByDescending<ItemParam, string>((Func<ItemParam, string>) (item => item.name)).ToList<ItemParam>() : (IEnumerable<ItemParam>) source.OrderBy<ItemParam, string>((Func<ItemParam, string>) (item => item.name)).ToList<ItemParam>()) : (!this.mIsRarityAscending ? (IEnumerable<ItemParam>) source.OrderByDescending<ItemParam, int>((Func<ItemParam, int>) (item => item.rare)) : (IEnumerable<ItemParam>) source.OrderBy<ItemParam, int>((Func<ItemParam, int>) (item => item.rare)))).ToList<ItemParam>();
    }

    private void ChangeFilterButtonSprite()
    {
      this.FilterButton.image.sprite = !this.IsSameFilter(this.mRareFilters, GalleryItemListWindow.DefaultFilter) ? this.FilterButtonNotAllImage : this.FilterButtonAllOnImage;
    }

    private void ChangeSortButtonText()
    {
      if (!((UnityEngine.Object) this.SortButtonTitle != (UnityEngine.Object) null))
        return;
      this.SortButtonTitle.text = LocalizedText.Get(this.mSortType != GalleryItemListWindow.SortType.Rarity ? "sys.SORT_NAME" : "sys.SORT_RARITY");
    }

    private bool IsSameFilter(int[] fil1, int[] fil2)
    {
      return fil1 == fil2 || ((IEnumerable<int>) fil1).OrderBy<int, int>((Func<int, int>) (x => x)).SequenceEqual<int>((IEnumerable<int>) ((IEnumerable<int>) fil2).OrderBy<int, int>((Func<int, int>) (x => x)));
    }

    private void SaveSettingAndOutputPin(int pinID)
    {
      SerializeValueList serializeValueList = new SerializeValueList();
      serializeValueList.SetObject("settings", (object) this.mSettings);
      FlowNode_ButtonEvent.currentValue = (object) serializeValueList;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
    }

    private GalleryItemListWindow.Settings LoadSetting()
    {
      if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.GALLERY_SETTING))
      {
        string json = PlayerPrefsUtility.GetString(PlayerPrefsUtility.GALLERY_SETTING, string.Empty);
        if (!string.IsNullOrEmpty(json))
          return JsonUtility.FromJson<GalleryItemListWindow.Settings>(json);
      }
      GalleryItemListWindow.Settings settings = new GalleryItemListWindow.Settings();
      settings.sortType = GalleryItemListWindow.SortType.Rarity;
      settings.isRarityAscending = true;
      settings.isNameAscending = true;
      settings.rareFilters = GalleryItemListWindow.DefaultFilter;
      string json1 = JsonUtility.ToJson((object) settings);
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.GALLERY_SETTING, json1, true);
      return settings;
    }

    private void SaveSetting(GalleryItemListWindow.Settings settings)
    {
      string json = JsonUtility.ToJson((object) settings);
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.GALLERY_SETTING, json, true);
    }

    private void ClearAllChache()
    {
      this.mRecentPages.Clear();
      this.mLoadCompletedPage.Clear();
      this.mItemAvailable.Clear();
    }

    private void RefreshNewPage(EItemTabType tabtype, int page)
    {
      this.mCurrentTabType = tabtype;
      this.mCurrentPage = page;
      this.mAllItems = this.GetAvailableItems(this.mCurrentTabType);
      this.mTotalPage = Mathf.CeilToInt((float) this.mAllItems.Count / (float) this.mCellCount);
      this.RequestItems();
    }

    private void RefreshNewPage(EItemTabType tabtype)
    {
      this.mCurrentTabType = tabtype;
      int num;
      this.mCurrentPage = !this.mRecentPages.TryGetValue(tabtype, out num) ? 0 : num;
      this.mAllItems = this.GetAvailableItems(this.mCurrentTabType);
      this.mTotalPage = Mathf.CeilToInt((float) this.mAllItems.Count / (float) this.mCellCount);
      this.RequestItems();
    }

    private void RequestItems()
    {
      this.mRecentPages[this.mCurrentTabType] = this.mCurrentPage;
      this.mItems = this.mAllItems.Skip<ItemParam>(this.mCurrentPage * this.mCellCount).Take<ItemParam>(this.mCellCount).ToList<ItemParam>();
      if (this.mItems.Count <= 0)
        this.RefreshPage((HashSet<string>) null);
      int num;
      if (this.mLoadCompletedPage.TryGetValue(this.mCurrentTabType, out num) && this.mCurrentPage <= num)
        this.RefreshPage(this.mItemAvailable);
      else
        Network.RequestAPI((WebAPI) new ReqGalleryItem(this.mItems, new Network.ResponseCallback(this.ResponseCallback)), false);
    }

    private void ChangeTab(EItemTabType tabtype)
    {
      if (this.mCurrentTabType == tabtype)
        return;
      this.RefreshNewPage(tabtype);
    }

    private void ResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<GalleryItemListWindow.JSON_Body> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<GalleryItemListWindow.JSON_Body>>(www.text);
        if (jsonObject.body != null && jsonObject.body.items != null)
        {
          foreach (string str in jsonObject.body.items)
            this.mItemAvailable.Add(str);
        }
        this.RefreshPage(this.mItemAvailable);
        this.mLoadCompletedPage[this.mCurrentTabType] = this.mCurrentPage;
        this.mInitialized = true;
        Network.RemoveAPI();
      }
    }

    private void RefreshPage(HashSet<string> availables)
    {
      GameUtility.DestroyGameObjects(this.mItemObjects);
      this.mItemObjects.Clear();
      if (availables == null)
      {
        this.mCurrentPage = 0;
        this.mTotalPage = 0;
        this.TotalPage.text = 0.ToString();
        this.CurrentPage.text = 0.ToString();
        this.ChangeEnabledArrowButtons(this.mCurrentPage, this.mTotalPage);
      }
      else
      {
        this.TotalPage.text = this.mTotalPage.ToString();
        this.CurrentPage.text = Math.Min(this.mCurrentPage + 1, this.mTotalPage).ToString();
        foreach (ItemParam mItem in this.mItems)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
          GalleryItem component = gameObject.GetComponent<GalleryItem>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.SetAvailable(availables.Contains(mItem.iname));
          DataSource.Bind<ItemParam>(gameObject, mItem, false);
          gameObject.transform.SetParent(this.ItemGrid.transform, false);
          gameObject.SetActive(true);
          this.mItemObjects.Add(gameObject);
        }
        this.ChangeEnabledArrowButtons(this.mCurrentPage, this.mTotalPage);
      }
    }

    private void NextPage()
    {
      if (this.mCurrentPage + 1 >= this.mTotalPage)
        return;
      ++this.mCurrentPage;
      this.RequestItems();
    }

    private void PrevPage()
    {
      if (this.mCurrentPage - 1 < 0)
        return;
      --this.mCurrentPage;
      this.RequestItems();
    }

    private void ChangeEnabledArrowButtons(int index, int max)
    {
      if ((UnityEngine.Object) this.NextButton != (UnityEngine.Object) null)
        this.NextButton.interactable = index < max - 1;
      if (!((UnityEngine.Object) this.PrevButton != (UnityEngine.Object) null))
        return;
      this.PrevButton.interactable = index > 0;
    }

    private int GetCellCount()
    {
      float x1 = this.ItemGrid.cellSize.x;
      float y1 = this.ItemGrid.cellSize.y;
      float x2 = this.ItemGrid.spacing.x;
      float y2 = this.ItemGrid.spacing.y;
      float horizontal = (float) this.ItemGrid.padding.horizontal;
      float vertical = (float) this.ItemGrid.padding.vertical;
      Rect rect = ((RectTransform) this.ItemGrid.transform).rect;
      float num1 = rect.width - horizontal + x2;
      float num2 = rect.height - vertical + y2;
      return Mathf.FloorToInt(num1 / (x1 + x2)) * Mathf.FloorToInt(num2 / (y1 + y2));
    }

    public enum SortType
    {
      Rarity,
      Name,
    }

    [Serializable]
    public class Settings
    {
      public GalleryItemListWindow.SortType sortType;
      public bool isRarityAscending;
      public bool isNameAscending;
      public int[] rareFilters;
    }

    public class JSON_Body
    {
      public string[] items;
    }
  }
}
