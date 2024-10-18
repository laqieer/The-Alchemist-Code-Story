// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyTabList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TrophyTabList : MonoBehaviour
  {
    [SerializeField]
    private int CREATE_TAB_COUNT_MAX = 50;
    private List<TrophyRecordTab> mTabs = new List<TrophyRecordTab>();
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
    private int mCurrentTabIndex;
    private CanvasGroup mCanvasGroup;

    private void OnEnable()
    {
      this.ScrollRect.normalizedPosition = new Vector2(1f, 1f);
      this.CreateTrophyRecord(true);
      MonoSingleton<GameManager>.Instance.OnDayChange += new GameManager.DayChangeEvent(this.OnTrophyReset);
    }

    private void OnDisable()
    {
      this.ClearAllItems();
      if ((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect() != (UnityEngine.Object) null)
        MonoSingleton<GameManager>.Instance.OnDayChange -= new GameManager.DayChangeEvent(this.OnTrophyReset);
      this.ScrollRect.normalizedPosition = new Vector2(1f, 1f);
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this.OriginalTab != (UnityEngine.Object) null)
        this.OriginalTab.gameObject.SetActive(false);
      this.mCanvasGroup = this.GetComponent<CanvasGroup>();
      if (!((UnityEngine.Object) this.mCanvasGroup == (UnityEngine.Object) null))
        return;
      this.mCanvasGroup = this.gameObject.AddComponent<CanvasGroup>();
    }

    private void Update()
    {
      if ((UnityEngine.Object) this.mCanvasGroup != (UnityEngine.Object) null && (double) this.mCanvasGroup.alpha < 1.0)
        this.mCanvasGroup.alpha = Mathf.Clamp01(this.mCanvasGroup.alpha + Time.unscaledDeltaTime * 3.333333f);
      if (!((UnityEngine.Object) this.ButtonGetAll != (UnityEngine.Object) null) || this.mTabs.Count <= 0)
        return;
      bool isInCompletedData = this.mTabs[this.mCurrentTabIndex].CategoryData.IsInCompletedData;
      if (isInCompletedData == this.ButtonGetAll.interactable)
        return;
      this.ButtonGetAll.interactable = isInCompletedData;
    }

    private void OnTrophyReset()
    {
      this.CreateTrophyRecord(true);
    }

    private void ClearAllItems()
    {
      foreach (Component mTab in this.mTabs)
        UnityEngine.Object.Destroy((UnityEngine.Object) mTab.gameObject);
      this.mTabs.Clear();
    }

    public TrophyRecordTab GetCurrentSelectionTab()
    {
      return this.mTabs[this.mCurrentTabIndex];
    }

    public void CreateTrophyRecord(bool resetSelection = true)
    {
      if ((UnityEngine.Object) this.mCanvasGroup != (UnityEngine.Object) null)
        this.mCanvasGroup.alpha = 0.0f;
      this.ClearAllItems();
      if (!this.TrophyWindow.TrophyRecordDatas.ContainsKey(this.TrophyCategory))
      {
        this.ButtonGetAll.interactable = false;
        this.TrophyList.ClearAllItems();
      }
      else
      {
        List<TrophyCategoryData> trophyRecordData = this.TrophyWindow.TrophyRecordDatas[this.TrophyCategory];
        if (trophyRecordData == null || trophyRecordData.Count <= 0)
        {
          this.ButtonGetAll.interactable = false;
          this.TrophyList.ClearAllItems();
        }
        else
        {
          int createTabCountMax = this.CREATE_TAB_COUNT_MAX;
          SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("RecordMission/RecordMission_Images");
          for (int _index = 0; _index < trophyRecordData.Count && createTabCountMax != 0; ++_index)
          {
            TrophyCategoryData _category_data = trophyRecordData[_index];
            Sprite banner = (Sprite) null;
            if ((UnityEngine.Object) spriteSheet != (UnityEngine.Object) null && _category_data.Param.banner != null)
              banner = spriteSheet.GetSprite(_category_data.Param.banner);
            TrophyRecordTab trophyRecordTab = this.MakeTrophyCategoryTab(_category_data.Param.iname, banner);
            if ((UnityEngine.Object) trophyRecordTab != (UnityEngine.Object) null)
            {
              --createTabCountMax;
              trophyRecordTab.Setup(_index);
              trophyRecordTab.SetCategoryData(_category_data);
              trophyRecordTab.RefreshDisplayParam();
              this.mTabs.Add(trophyRecordTab);
              trophyRecordTab.GetComponent<SRPG_Button>().AddListener(new SRPG_Button.ButtonClickEvent(this.OnItemSelect));
            }
            if (this.mTabs.Count >= this.CREATE_TAB_COUNT_MAX)
              break;
          }
          this.mCurrentTabIndex = !resetSelection ? (this.mTabs.Count <= 0 || this.mCurrentTabIndex <= this.mTabs.Count - 1 ? 0 : this.mTabs.Count - 1) : 0;
          this.ActivateAllTabCursor(this.mCurrentTabIndex);
          TrophyRecordTab mTab = this.mTabs[this.mCurrentTabIndex];
          mTab.SetCursor(true);
          this.ButtonGetAll.interactable = mTab.CategoryData.IsInCompletedData;
          this.TrophyList.CreateTrophies(mTab.CategoryData.Trophies);
          LayoutRebuilder.MarkLayoutForRebuild(this.ScrollRect.transform as RectTransform);
        }
      }
    }

    public void RefreshTrophyRecord()
    {
      if ((UnityEngine.Object) this.mCanvasGroup != (UnityEngine.Object) null)
        this.mCanvasGroup.alpha = 0.0f;
      if (!this.TrophyWindow.TrophyRecordDatas.ContainsKey(this.TrophyCategory))
      {
        this.ClearAllItems();
        this.ButtonGetAll.interactable = false;
        this.TrophyList.ClearAllItems();
      }
      else
      {
        List<TrophyCategoryData> trophyRecordData = this.TrophyWindow.TrophyRecordDatas[this.TrophyCategory];
        if (trophyRecordData == null || trophyRecordData.Count <= 0)
        {
          this.ClearAllItems();
          this.ButtonGetAll.interactable = false;
          this.TrophyList.ClearAllItems();
        }
        else
        {
          int count = this.mTabs.Count;
          for (int index1 = 0; index1 < trophyRecordData.Count; ++index1)
          {
            for (int index2 = 0; index2 < this.mTabs.Count; ++index2)
            {
              if (trophyRecordData[index1].Param.hash_code == this.mTabs[index2].HashCode)
              {
                this.mTabs[index2].SetCategoryData(trophyRecordData[index1]);
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
            this.ButtonGetAll.interactable = mTab.CategoryData.IsInCompletedData;
            this.TrophyList.CreateTrophies(mTab.CategoryData.Trophies);
          }
        }
      }
    }

    private TrophyRecordTab MakeTrophyCategoryTab(string title, Sprite banner = null)
    {
      TrophyRecordTab component = UnityEngine.Object.Instantiate<GameObject>(this.OriginalTab.gameObject).GetComponent<TrophyRecordTab>();
      component.Init(title, banner);
      component.transform.SetParent(this.TabHolder, false);
      return component;
    }

    private void OnItemSelect(SRPG_Button button)
    {
      TrophyRecordTab component = button.GetComponent<TrophyRecordTab>();
      if (component.Index == this.mCurrentTabIndex)
        return;
      this.mCurrentTabIndex = component.Index;
      this.ActivateAllTabCursor(this.mCurrentTabIndex);
      component.SetCursor(true);
      TrophyCategoryData categoryData = component.CategoryData;
      this.TrophyList.CreateTrophies(categoryData.Trophies);
      this.ButtonGetAll.interactable = categoryData.IsInCompletedData;
    }

    private void ActivateAllTabCursor(int activeIndex)
    {
      for (int index = 0; index < this.mTabs.Count; ++index)
        this.mTabs[index].SetCursor(index == activeIndex);
    }
  }
}
