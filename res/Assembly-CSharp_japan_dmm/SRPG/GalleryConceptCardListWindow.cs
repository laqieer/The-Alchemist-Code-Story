// Decompiled with JetBrains decompiler
// Type: SRPG.GalleryConceptCardListWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
  [FlowNode.Pin(10, "更新", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "ソート設定変更ダイアログ表示", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "フィルター設定変更ダイアログ表示", FlowNode.PinTypes.Output, 101)]
  public class GalleryConceptCardListWindow : MonoBehaviour, IFlowInterface
  {
    private const int OPEN = 0;
    private const int NEXT_PAGE = 1;
    private const int PREV_PAGE = 2;
    private const int TAB_CHANGE = 3;
    private const int REQ_SORT_SETTING = 4;
    private const int UPDATED_SORT_SETTING = 5;
    private const int REQ_FILTER_SETTING = 6;
    private const int UPDATED_FILTER_SETTING = 7;
    private const int PIN_IN_REFRESH = 10;
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
    private GalleryConceptCardListWindow.TabType mCurrentTabType;
    private GallerySettings mSettings;
    private List<ConceptCardParam> mAllItems = new List<ConceptCardParam>();
    private List<GameObject> mItemObjects = new List<GameObject>();
    private Dictionary<GalleryConceptCardListWindow.TabType, GalleryConceptCardListWindow.TabInfo> mTabInfoList = new Dictionary<GalleryConceptCardListWindow.TabType, GalleryConceptCardListWindow.TabInfo>();
    private GalleryConceptCardListWindow.TabInfo mCurrentTabInfo;
    private bool mInitialized;

    private void Start()
    {
      this.mCellCount = this.GetCellCount();
      this.mCurrentTabType = GalleryConceptCardListWindow.TabType.Equipment;
      this.mSettings = this.LoadSetting();
      this.ChangeFilterButtonSprite();
      this.ChangeSortButtonText();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        this.ItemTemplate.SetActive(false);
      this.RequestTabItems();
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
          this.ChangeTab((GalleryConceptCardListWindow.TabType) num);
          break;
        case 4:
          this.SaveSettingAndOutputPin(100);
          break;
        case 5:
          if (this.mCurrentTabInfo.Setting.IsSameSortSetting(this.mSettings))
            break;
          this.mCurrentTabInfo.Setting = this.mSettings.Clone();
          this.SaveSetting(this.mSettings);
          this.mCurrentTabInfo.AllItems = this.GetVisibleItems(this.GetAvailableItems(this.mCurrentTabType), this.mCurrentTabInfo.AllAvailableItems);
          this.ChangeSortButtonText();
          this.RefreshPage();
          break;
        case 6:
          this.SaveSettingAndOutputPin(101);
          break;
        case 7:
          if (this.mCurrentTabInfo.Setting.IsSameFilter(this.mSettings))
            break;
          this.mCurrentTabInfo.Setting = this.mSettings.Clone();
          this.SaveSetting(this.mSettings);
          this.ChangeFilterButtonSprite();
          this.RequestTabItems();
          break;
        case 10:
          this.SetDirtyAllTabInfo();
          this.RequestTabItems();
          break;
      }
    }

    private List<ConceptCardParam> GetAvailableItems(GalleryConceptCardListWindow.TabType tabtype)
    {
      return GalleryConceptCardListWindow.SortBySettings(GalleryConceptCardListWindow.FilterBySettings((IEnumerable<ConceptCardParam>) MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParams(), tabtype, this.mSettings), this.mSettings).ToList<ConceptCardParam>();
    }

    public static IEnumerable<ConceptCardParam> FilterBySettings(
      IEnumerable<ConceptCardParam> list,
      GalleryConceptCardListWindow.TabType tabtype,
      GallerySettings settings)
    {
      return list.Where<ConceptCardParam>((Func<ConceptCardParam, bool>) (item =>
      {
        if (!GalleryConceptCardListWindow.IsSameTabType(item, tabtype) || item.gallery_view == GalleryVisibilityType.Hide)
          return false;
        return settings.IsFilterTotallyOn() || settings.IsFilterTotallyOff() || ((IEnumerable<int>) settings.rareFilters).Any<int>((Func<int, bool>) (rare => rare == item.rare));
      }));
    }

    private static bool IsSameTabType(
      ConceptCardParam conceptCard,
      GalleryConceptCardListWindow.TabType tabType)
    {
      if (conceptCard.is_other)
        return tabType == GalleryConceptCardListWindow.TabType.Others;
      switch (conceptCard.type)
      {
        case eCardType.Equipment:
          return tabType == GalleryConceptCardListWindow.TabType.Equipment;
        case eCardType.Enhance_exp:
        case eCardType.Enhance_trust:
          return tabType == GalleryConceptCardListWindow.TabType.Enhance;
        default:
          return false;
      }
    }

    public static IEnumerable<ConceptCardParam> SortBySettings(
      IEnumerable<ConceptCardParam> list,
      GallerySettings settings)
    {
      list = settings.sortType != GallerySettings.SortType.Rarity ? (!settings.isNameAscending ? (IEnumerable<ConceptCardParam>) list.OrderByDescending<ConceptCardParam, string>((Func<ConceptCardParam, string>) (item => item.name)) : (IEnumerable<ConceptCardParam>) list.OrderBy<ConceptCardParam, string>((Func<ConceptCardParam, string>) (item => item.name))) : (!settings.isRarityAscending ? (IEnumerable<ConceptCardParam>) list.OrderByDescending<ConceptCardParam, int>((Func<ConceptCardParam, int>) (item => item.rare)) : (IEnumerable<ConceptCardParam>) list.OrderBy<ConceptCardParam, int>((Func<ConceptCardParam, int>) (item => item.rare)));
      return list;
    }

    private void ChangeFilterButtonSprite()
    {
      ((Selectable) this.FilterButton).image.sprite = this.mSettings.IsFilterTotallyOn() || this.mSettings.IsFilterTotallyOff() ? this.FilterButtonAllOnImage : this.FilterButtonNotAllImage;
    }

    private void ChangeSortButtonText()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SortButtonTitle, (UnityEngine.Object) null))
        return;
      this.SortButtonTitle.text = LocalizedText.Get(this.mSettings.sortType != GallerySettings.SortType.Rarity ? "sys.SORT_NAME" : "sys.SORT_RARITY");
    }

    private void SaveSettingAndOutputPin(int pinID)
    {
      SerializeValueList serializeValueList = new SerializeValueList();
      serializeValueList.SetObject("settings", (object) this.mSettings);
      FlowNode_ButtonEvent.currentValue = (object) serializeValueList;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
    }

    private GallerySettings LoadSetting()
    {
      if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.GALLERY_CONCEPTCARD_SETTING))
      {
        string str = PlayerPrefsUtility.GetString(PlayerPrefsUtility.GALLERY_CONCEPTCARD_SETTING, string.Empty);
        if (!string.IsNullOrEmpty(str))
        {
          try
          {
            return JsonUtility.FromJson<GallerySettings>(str);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
        }
      }
      GallerySettings defaultSettings = GallerySettings.CreateDefaultSettings();
      string json = JsonUtility.ToJson((object) defaultSettings);
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.GALLERY_CONCEPTCARD_SETTING, json, true);
      return defaultSettings;
    }

    private void SaveSetting(GallerySettings settings)
    {
      string json = JsonUtility.ToJson((object) settings);
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.GALLERY_CONCEPTCARD_SETTING, json, true);
    }

    private void SetDirtyAllTabInfo()
    {
      foreach (GalleryConceptCardListWindow.TabInfo tabInfo in this.mTabInfoList.Values)
        tabInfo.IsDirty = true;
    }

    private void RequestTabItems()
    {
      this.mAllItems.Clear();
      this.mAllItems = this.GetAvailableItems(this.mCurrentTabType);
      List<ConceptCardParam> cards;
      if (this.mCurrentTabInfo != null)
      {
        HashSet<string> alreadyAvailable = this.mCurrentTabInfo.AllAvailableItems;
        cards = this.mAllItems.Where<ConceptCardParam>((Func<ConceptCardParam, bool>) (item => !alreadyAvailable.Contains(item.iname))).ToList<ConceptCardParam>();
      }
      else
        cards = this.mAllItems;
      if (cards.Count > 0)
        Network.RequestAPI((WebAPI) new ReqGalleryItem(cards, new Network.ResponseCallback(this.ResponseCallback)));
      else if (this.mCurrentTabInfo != null)
        this.ConnectionSucceeded(this.mCurrentTabInfo.AllAvailableItems);
      else
        this.ConnectionSucceeded(new HashSet<string>());
    }

    private void ChangeTab(GalleryConceptCardListWindow.TabType tabtype)
    {
      if (this.mCurrentTabType == tabtype)
        return;
      this.mCurrentTabType = tabtype;
      GalleryConceptCardListWindow.TabInfo tabInfo;
      if (this.mTabInfoList.TryGetValue(this.mCurrentTabType, out tabInfo))
      {
        this.mCurrentTabInfo = tabInfo;
        GallerySettings setting = this.mCurrentTabInfo.Setting;
        this.mCurrentTabInfo.Setting = this.mSettings.Clone();
        if (this.mCurrentTabInfo.IsDirty)
          this.RequestTabItems();
        else if (!this.mSettings.IsSameFilter(setting))
          this.RequestTabItems();
        else if (!this.mSettings.IsSameSortSetting(setting))
        {
          this.mCurrentTabInfo.AllItems = this.GetVisibleItems(this.GetAvailableItems(this.mCurrentTabType), this.mCurrentTabInfo.AllAvailableItems);
          this.RefreshPage();
        }
        else
          this.RefreshPage();
      }
      else
        this.RequestTabItems();
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
        WebAPI.JSON_BodyResponse<GalleryConceptCardListWindow.JSON_Body> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<GalleryConceptCardListWindow.JSON_Body>>(www.text);
        HashSet<string> itemAvailable = this.mCurrentTabInfo == null ? new HashSet<string>() : this.mCurrentTabInfo.AllAvailableItems;
        if (jsonObject.body != null && jsonObject.body.items != null && jsonObject.body.items.Length > 0)
        {
          foreach (string str in jsonObject.body.items)
            itemAvailable.Add(str);
        }
        this.ConnectionSucceeded(itemAvailable);
        Network.RemoveAPI();
      }
    }

    private void ConnectionSucceeded(HashSet<string> itemAvailable)
    {
      List<ConceptCardParam> visibleItems = this.GetVisibleItems(this.mAllItems, itemAvailable);
      this.mAllItems.Clear();
      int num1 = Mathf.CeilToInt((float) visibleItems.Count / (float) this.mCellCount);
      int num2 = 0;
      GalleryConceptCardListWindow.TabInfo tabInfo;
      if (this.mTabInfoList.TryGetValue(this.mCurrentTabType, out tabInfo))
      {
        int val2 = Math.Max(num1 - 1, 0);
        num2 = Math.Min(tabInfo.CurrentPage, val2);
      }
      this.mCurrentTabInfo = new GalleryConceptCardListWindow.TabInfo()
      {
        CurrentPage = num2,
        TotalPage = num1,
        AllItems = visibleItems,
        AllAvailableItems = itemAvailable,
        Setting = this.mSettings.Clone()
      };
      this.mTabInfoList[this.mCurrentTabType] = this.mCurrentTabInfo;
      this.RefreshPage();
      this.mCurrentTabInfo.IsDirty = false;
      this.mInitialized = true;
    }

    private List<ConceptCardParam> GetVisibleItems(
      List<ConceptCardParam> allItems,
      HashSet<string> availables)
    {
      return allItems.Count <= 0 ? new List<ConceptCardParam>() : allItems.Where<ConceptCardParam>((Func<ConceptCardParam, bool>) (item =>
      {
        if (item.gallery_view == GalleryVisibilityType.Hide)
          return false;
        return item.gallery_view != GalleryVisibilityType.OnlyPossession || availables.Contains(item.iname);
      })).ToList<ConceptCardParam>();
    }

    private void RefreshPage()
    {
      List<ConceptCardParam> allItems = this.mCurrentTabInfo.AllItems;
      if (allItems == null || allItems.Count <= 0)
      {
        this.TotalPage.text = "0";
        this.CurrentPage.text = "0";
        this.ChangeEnabledArrowButtons(0, 0);
        this.ClearAndDisableAll(this.mItemObjects);
      }
      else
      {
        int currentPage = this.mCurrentTabInfo.CurrentPage;
        int totalPage = this.mCurrentTabInfo.TotalPage;
        this.TotalPage.text = totalPage.ToString();
        this.CurrentPage.text = Math.Min(currentPage + 1, totalPage).ToString();
        List<ConceptCardParam> list = this.mCurrentTabInfo.AllItems.Skip<ConceptCardParam>(currentPage * this.mCellCount).Take<ConceptCardParam>(this.mCellCount).ToList<ConceptCardParam>();
        HashSet<string> allAvailableItems = this.mCurrentTabInfo.AllAvailableItems;
        if (this.mItemObjects.Count > this.mCellCount)
        {
          for (int index = this.mItemObjects.Count - 1; index >= this.mCellCount; --index)
          {
            UnityEngine.Object.Destroy((UnityEngine.Object) this.mItemObjects[index]);
            this.mItemObjects.RemoveAt(index);
          }
        }
        for (int index = 0; index < this.mCellCount; ++index)
        {
          if (index < list.Count)
          {
            ConceptCardParam conceptCardParam = list[index];
            GameObject gameObject;
            if (index < this.mItemObjects.Count)
            {
              gameObject = this.mItemObjects[index];
            }
            else
            {
              gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
              this.mItemObjects.Add(gameObject);
            }
            gameObject.SetActive(false);
            this.SetDummyCardData(this.mItemObjects[index], conceptCardParam);
            this.SetItemInteractable(this.mItemObjects[index], allAvailableItems.Contains(conceptCardParam.iname));
            this.mItemObjects[index].transform.SetParent(((Component) this.ItemGrid).transform, false);
            this.mItemObjects[index].SetActive(true);
            GameParameter.UpdateAll(this.mItemObjects[index]);
          }
          else if (index < this.mItemObjects.Count)
          {
            this.RemoveBindedDataIfExists(this.mItemObjects[index]);
            this.SetItemInteractable(this.mItemObjects[index], false);
            this.mItemObjects[index].SetActive(false);
          }
        }
        this.ChangeEnabledArrowButtons(currentPage, totalPage);
      }
    }

    private void SetDummyCardData(GameObject obj, ConceptCardParam param)
    {
      ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(param.iname);
      ConceptCardIcon component = obj.GetComponent<ConceptCardIcon>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.Setup(cardDataForDisplay);
    }

    private void ClearAndDisableAll(List<GameObject> list)
    {
      foreach (GameObject gameObject in list)
      {
        this.RemoveBindedDataIfExists(gameObject);
        ConceptCardIcon component = gameObject.GetComponent<ConceptCardIcon>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.Setup((ConceptCardData) null);
        this.SetItemInteractable(gameObject, false);
        gameObject.SetActive(false);
      }
    }

    private DataSource RemoveBindedDataIfExists(GameObject obj)
    {
      DataSource component = obj.GetComponent<DataSource>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        component.Clear();
      return component;
    }

    private void SetItemInteractable(GameObject obj, bool isInteractable)
    {
      GalleryItem component = obj.GetComponent<GalleryItem>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.SetAvailable(isInteractable);
    }

    private void NextPage()
    {
      if (this.mCurrentTabInfo.CurrentPage + 1 >= this.mCurrentTabInfo.TotalPage)
        return;
      ++this.mCurrentTabInfo.CurrentPage;
      this.RefreshPage();
    }

    private void PrevPage()
    {
      if (this.mCurrentTabInfo.CurrentPage - 1 < 0)
        return;
      --this.mCurrentTabInfo.CurrentPage;
      this.RefreshPage();
    }

    private void ChangeEnabledArrowButtons(int index, int max)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextButton, (UnityEngine.Object) null))
        ((Selectable) this.NextButton).interactable = index < max - 1;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.PrevButton).interactable = index > 0;
    }

    private int GetCellCount()
    {
      float x1 = this.ItemGrid.cellSize.x;
      float y1 = this.ItemGrid.cellSize.y;
      float x2 = this.ItemGrid.spacing.x;
      float y2 = this.ItemGrid.spacing.y;
      float horizontal = (float) ((LayoutGroup) this.ItemGrid).padding.horizontal;
      float vertical = (float) ((LayoutGroup) this.ItemGrid).padding.vertical;
      Rect rect = ((RectTransform) ((Component) this.ItemGrid).transform).rect;
      float num1 = ((Rect) ref rect).width - horizontal + x2;
      float num2 = ((Rect) ref rect).height - vertical + y2;
      return Mathf.FloorToInt(num1 / (x1 + x2)) * Mathf.FloorToInt(num2 / (y1 + y2));
    }

    public enum TabType
    {
      Equipment,
      Enhance,
      Others,
    }

    private class TabInfo
    {
      public int CurrentPage;
      public int TotalPage;
      public List<ConceptCardParam> AllItems;
      public HashSet<string> AllAvailableItems;
      public GallerySettings Setting;
      public bool IsDirty;
    }

    public class JSON_Body
    {
      public string[] items;
    }
  }
}
