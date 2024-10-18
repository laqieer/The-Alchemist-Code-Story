// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyTabList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class TrophyTabList : MonoBehaviour
  {
    [SerializeField]
    private int CREATE_TAB_COUNT_MAX = 50;
    [SerializeField]
    private TrophyCategorys TrophyCategory;
    [SerializeField]
    private TrophyWindow TrophyWindow;
    [SerializeField]
    private TrophyList TrophyList;
    [SerializeField]
    private Transform TabHolder;
    [SerializeField]
    private TrophyRecordTab OriginalTab;
    [SerializeField]
    private ScrollRect ScrollRect;
    [SerializeField]
    private Button ButtonGetAll;
    private List<TrophyRecordTab> mTabs = new List<TrophyRecordTab>();
    private int mCurrentTabIndex;
    private CanvasGroup mCanvasGroup;

    private void OnEnable()
    {
      this.ScrollRect.normalizedPosition = new Vector2(1f, 1f);
      this.CreateTrophyRecord();
      MonoSingleton<GameManager>.Instance.OnDayChange += new GameManager.DayChangeEvent(this.OnTrophyReset);
    }

    private void OnDisable()
    {
      this.ClearAllItems();
      if (Object.op_Inequality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
        MonoSingleton<GameManager>.Instance.OnDayChange -= new GameManager.DayChangeEvent(this.OnTrophyReset);
      this.ScrollRect.normalizedPosition = new Vector2(1f, 1f);
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.OriginalTab, (Object) null))
        ((Component) this.OriginalTab).gameObject.SetActive(false);
      this.mCanvasGroup = ((Component) this).GetComponent<CanvasGroup>();
      if (!Object.op_Equality((Object) this.mCanvasGroup, (Object) null))
        return;
      this.mCanvasGroup = ((Component) this).gameObject.AddComponent<CanvasGroup>();
    }

    private void Update()
    {
      if (Object.op_Inequality((Object) this.mCanvasGroup, (Object) null) && (double) this.mCanvasGroup.alpha < 1.0)
        this.mCanvasGroup.alpha = Mathf.Clamp01(this.mCanvasGroup.alpha + Time.unscaledDeltaTime * 3.33333325f);
      if (!Object.op_Inequality((Object) this.ButtonGetAll, (Object) null) || this.mTabs.Count <= 0)
        return;
      bool isInCompletedData = this.mTabs[this.mCurrentTabIndex].CategoryData.IsInCompletedData;
      if (isInCompletedData == ((Selectable) this.ButtonGetAll).interactable)
        return;
      ((Selectable) this.ButtonGetAll).interactable = isInCompletedData;
    }

    private void OnTrophyReset() => this.CreateTrophyRecord();

    private void ClearAllItems()
    {
      foreach (Component mTab in this.mTabs)
        Object.Destroy((Object) mTab.gameObject);
      this.mTabs.Clear();
    }

    public TrophyRecordTab GetCurrentSelectionTab() => this.mTabs[this.mCurrentTabIndex];

    public void CreateTrophyRecord(bool resetSelection = true)
    {
      if (Object.op_Inequality((Object) this.mCanvasGroup, (Object) null))
        this.mCanvasGroup.alpha = 0.0f;
      this.ClearAllItems();
      if (!this.TrophyWindow.TrophyRecordData.ContainsKey(this.TrophyCategory))
      {
        ((Selectable) this.ButtonGetAll).interactable = false;
        this.TrophyList.ClearAllItems();
      }
      else
      {
        List<TrophyCategoryData> trophyCategoryDataList = this.TrophyWindow.TrophyRecordData[this.TrophyCategory];
        if (trophyCategoryDataList == null || trophyCategoryDataList.Count <= 0)
        {
          ((Selectable) this.ButtonGetAll).interactable = false;
          this.TrophyList.ClearAllItems();
        }
        else
        {
          int createTabCountMax = this.CREATE_TAB_COUNT_MAX;
          SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("RecordMission/RecordMission_Images");
          for (int index = 0; index < trophyCategoryDataList.Count && createTabCountMax != 0; ++index)
          {
            TrophyCategoryData _category_data = trophyCategoryDataList[index];
            Sprite banner = (Sprite) null;
            if (Object.op_Inequality((Object) spriteSheet, (Object) null) && _category_data.Param.banner != null)
              banner = spriteSheet.GetSprite(_category_data.Param.banner);
            TrophyRecordTab trophyRecordTab = this.MakeTrophyCategoryTab(_category_data.Param.iname, banner);
            if (Object.op_Inequality((Object) trophyRecordTab, (Object) null))
            {
              --createTabCountMax;
              trophyRecordTab.Setup(index);
              trophyRecordTab.SetCategoryData(_category_data);
              trophyRecordTab.RefreshDisplayParam();
              this.mTabs.Add(trophyRecordTab);
              ((Component) trophyRecordTab).GetComponent<SRPG_Button>().AddListener(new SRPG_Button.ButtonClickEvent(this.OnItemSelect));
            }
            if (this.mTabs.Count >= this.CREATE_TAB_COUNT_MAX)
              break;
          }
          this.mCurrentTabIndex = !resetSelection ? (this.mTabs.Count <= 0 || this.mCurrentTabIndex <= this.mTabs.Count - 1 ? 0 : this.mTabs.Count - 1) : 0;
          this.ActivateAllTabCursor(this.mCurrentTabIndex);
          TrophyRecordTab mTab = this.mTabs[this.mCurrentTabIndex];
          mTab.SetCursor(true);
          ((Selectable) this.ButtonGetAll).interactable = mTab.CategoryData.IsInCompletedData;
          this.TrophyList.CreateTrophies(mTab.CategoryData.Trophies);
          LayoutRebuilder.MarkLayoutForRebuild(((Component) this.ScrollRect).transform as RectTransform);
        }
      }
    }

    public void RefreshTrophyRecord()
    {
      if (Object.op_Inequality((Object) this.mCanvasGroup, (Object) null))
        this.mCanvasGroup.alpha = 0.0f;
      if (!this.TrophyWindow.TrophyRecordData.ContainsKey(this.TrophyCategory))
      {
        this.ClearAllItems();
        ((Selectable) this.ButtonGetAll).interactable = false;
        this.TrophyList.ClearAllItems();
      }
      else
      {
        List<TrophyCategoryData> trophyCategoryDataList = this.TrophyWindow.TrophyRecordData[this.TrophyCategory];
        if (trophyCategoryDataList == null || trophyCategoryDataList.Count <= 0)
        {
          this.ClearAllItems();
          ((Selectable) this.ButtonGetAll).interactable = false;
          this.TrophyList.ClearAllItems();
        }
        else
        {
          int count = this.mTabs.Count;
          for (int index1 = 0; index1 < trophyCategoryDataList.Count; ++index1)
          {
            for (int index2 = 0; index2 < this.mTabs.Count; ++index2)
            {
              if (trophyCategoryDataList[index1].Param.hash_code == this.mTabs[index2].HashCode)
              {
                this.mTabs[index2].SetCategoryData(trophyCategoryDataList[index1]);
                --count;
              }
            }
          }
          if (count != 0)
          {
            this.CreateTrophyRecord(false);
          }
          else
          {
            for (int index = 0; index < this.mTabs.Count; ++index)
              this.mTabs[index].RefreshDisplayParam();
            this.ActivateAllTabCursor(this.mCurrentTabIndex);
            TrophyRecordTab mTab = this.mTabs[this.mCurrentTabIndex];
            mTab.SetCursor(true);
            ((Selectable) this.ButtonGetAll).interactable = mTab.CategoryData.IsInCompletedData;
            this.TrophyList.CreateTrophies(mTab.CategoryData.Trophies);
          }
        }
      }
    }

    private TrophyRecordTab MakeTrophyCategoryTab(string title, Sprite banner = null)
    {
      TrophyRecordTab component = Object.Instantiate<GameObject>(((Component) this.OriginalTab).gameObject).GetComponent<TrophyRecordTab>();
      component.Init(title, banner);
      ((Component) component).transform.SetParent(this.TabHolder, false);
      return component;
    }

    private void OnItemSelect(SRPG_Button button)
    {
      TrophyRecordTab component = ((Component) button).GetComponent<TrophyRecordTab>();
      if (component.Index == this.mCurrentTabIndex)
        return;
      this.mCurrentTabIndex = component.Index;
      this.ActivateAllTabCursor(this.mCurrentTabIndex);
      component.SetCursor(true);
      TrophyCategoryData categoryData = component.CategoryData;
      this.TrophyList.CreateTrophies(categoryData.Trophies);
      ((Selectable) this.ButtonGetAll).interactable = categoryData.IsInCompletedData;
    }

    private void ActivateAllTabCursor(int activeIndex)
    {
      for (int index = 0; index < this.mTabs.Count; ++index)
        this.mTabs[index].SetCursor(index == activeIndex);
    }
  }
}
