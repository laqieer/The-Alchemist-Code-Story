// Decompiled with JetBrains decompiler
// Type: SRPG.RuneSortWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Save", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "降順", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "昇順", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "キャンセル", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "タブ切替", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(10, "Save完了", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "キャンセル完了", FlowNode.PinTypes.Output, 11)]
  public class RuneSortWindow : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_SAVE = 0;
    public const int PIN_INPUT_TO_DESCENDING = 1;
    public const int PIN_INPUT_TO_ASCENDING = 2;
    public const int PIN_INPUT_CANCEL = 3;
    public const int PIN_INPUT_TAB_CHANGE = 4;
    public const int PIN_OUTPUT_SAVE_END = 10;
    public const int PIN_OUTPUT_CANCEL_END = 11;
    private const string SVB_KEY_CATEGORY_PARAMS = "category_params";
    [SerializeField]
    private Transform mTabParent;
    [SerializeField]
    private GameObject mTabTemplate;
    [SerializeField]
    private ScrollRect mScrollRect;
    [SerializeField]
    private Toggle mToggleAscending;
    [SerializeField]
    private Toggle mToggleDescending;
    [SerializeField]
    private Toggle mToggleBaseParamSort;
    [SerializeField]
    private Toggle mToggleEvoParamSort;
    [SerializeField]
    private Toggle mToggleSetParamSort;
    private SortUtility.SortRunePrefs mCurrentSortPrefs;
    private Dictionary<string, RuneFilterTab> mTabs = new Dictionary<string, RuneFilterTab>();
    private List<RuneSortCategory> mCategoryObjects = new List<RuneSortCategory>();
    private string mCurrentTabName;
    private bool mIsAscending = true;
    private bool mIsBaseParamSort = true;
    private bool mIsEvoParamSort;
    private bool mIsSetParamSort;
    private IEnumerator mToggleFadeAnimation;
    private static RuneSortWindow mInstance;

    public static RuneSortWindow Instance => RuneSortWindow.mInstance;

    public SortUtility.SortRunePrefs CurrentSortPrefs => this.mCurrentSortPrefs;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, !this.SavePrefs() ? 11 : 10);
          break;
        case 1:
          this.mIsAscending = false;
          break;
        case 2:
          this.mIsAscending = true;
          break;
        case 3:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
          break;
        case 4:
          if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
            break;
          FilterRuneParam dataSource = currentValue.GetDataSource<FilterRuneParam>("_self", (FilterRuneParam) null);
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) currentValue.GetUIToggle("_self"), (UnityEngine.Object) null) || dataSource == null || !(this.mCurrentTabName != dataSource.tab_name))
            break;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mScrollRect, (UnityEngine.Object) null))
          {
            this.mScrollRect.velocity = Vector2.zero;
            this.mScrollRect.normalizedPosition = Vector2.up;
          }
          this.SelectTab(dataSource.tab_name);
          this.UpdateTabState();
          break;
      }
    }

    private void Awake() => RuneSortWindow.mInstance = this;

    private void Start() => this.Init();

    private void Init()
    {
      this.Init_Tab();
      this.Init_Category();
      this.Init_Order();
      this.SelectTab(this.mCurrentTabName);
      this.UpdateTabState();
    }

    private void Init_Tab()
    {
      GameUtility.SetGameObjectActive(this.mTabTemplate, false);
      foreach (SortRuneParam sortRuneParam in MonoSingleton<GameManager>.Instance.MasterParam.SortRuneParams)
      {
        if (!this.mTabs.ContainsKey(sortRuneParam.tab_name))
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mTabTemplate, this.mTabParent, false);
          RuneFilterTab componentInChildren1 = gameObject.GetComponentInChildren<RuneFilterTab>();
          gameObject.SetActive(true);
          this.mTabs.Add(sortRuneParam.tab_name, componentInChildren1);
          DataSource.Bind<SortRuneParam>(gameObject, sortRuneParam);
          Text componentInChildren2 = gameObject.GetComponentInChildren<Text>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren2, (UnityEngine.Object) null))
            componentInChildren2.text = sortRuneParam.tab_name;
          if (string.IsNullOrEmpty(this.mCurrentTabName))
            this.mCurrentTabName = sortRuneParam.tab_name;
        }
      }
    }

    private void Init_Category()
    {
      this.mCurrentSortPrefs = SortUtility.Load_RuneSortFromCache();
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      Dictionary<string, RuneSortCategory> dictionary = new Dictionary<string, RuneSortCategory>();
      dictionary.Add("category_params", component.list.GetComponent<RuneSortCategory>("category_params"));
      foreach (SortRuneParam sortRuneParam in MonoSingleton<GameManager>.Instance.MasterParam.SortRuneParams)
      {
        RuneSortCategory runeSortCategory = dictionary["category_params"];
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) runeSortCategory, (UnityEngine.Object) null))
        {
          runeSortCategory.Init(sortRuneParam);
          if (!this.mCategoryObjects.Contains(runeSortCategory))
            this.mCategoryObjects.Add(runeSortCategory);
        }
      }
    }

    private void Init_Order()
    {
      this.mCurrentSortPrefs = SortUtility.Load_RuneSortFromCache();
      this.mIsAscending = this.mCurrentSortPrefs.IsAscending;
      this.mIsBaseParamSort = this.mCurrentSortPrefs.IsBaseParamSort;
      this.mIsEvoParamSort = this.mCurrentSortPrefs.IsEvoParamSort;
      this.mIsSetParamSort = this.mCurrentSortPrefs.IsSetParamSort;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mToggleAscending, (UnityEngine.Object) null))
        GameUtility.SetToggle(this.mToggleAscending, this.mIsAscending);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mToggleDescending, (UnityEngine.Object) null))
        GameUtility.SetToggle(this.mToggleDescending, !this.mIsAscending);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mToggleBaseParamSort, (UnityEngine.Object) null))
        GameUtility.SetToggle(this.mToggleBaseParamSort, this.mIsBaseParamSort);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mToggleEvoParamSort, (UnityEngine.Object) null))
        GameUtility.SetToggle(this.mToggleEvoParamSort, this.mIsEvoParamSort);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mToggleSetParamSort, (UnityEngine.Object) null))
        return;
      GameUtility.SetToggle(this.mToggleSetParamSort, this.mIsSetParamSort);
    }

    private void SelectTab(string tab_name)
    {
      this.mCurrentTabName = tab_name;
      this.mTabs[this.mCurrentTabName].isOn = true;
      this.SwitchToggleActiveWithTabName(tab_name);
    }

    public void UpdateTabState()
    {
      foreach (string key in this.mTabs.Keys)
      {
        bool isFiltered = this.IsExistActiveSortInTab(key);
        this.mTabs[key].SetIsFiltered(isFiltered);
      }
    }

    private bool IsExistActiveSortInTab(string tab)
    {
      List<RuneSortCategory> all = this.mCategoryObjects.FindAll((Predicate<RuneSortCategory>) (category => category.SortParam.tab_name == tab));
      for (int index = 0; index < all.Count; ++index)
      {
        if (all[index].IsExistActiveToggle())
          return true;
      }
      return false;
    }

    private void SwitchToggleActiveWithTabName(string tab_name)
    {
      this.StopSwitchVisibleAnimation();
      List<CanvasGroup> list_off = new List<CanvasGroup>();
      List<CanvasGroup> list_on = new List<CanvasGroup>();
      foreach (RuneSortCategory mCategoryObject in this.mCategoryObjects)
      {
        CanvasGroup component = ((Component) mCategoryObject).GetComponent<CanvasGroup>();
        if (mCategoryObject.SortParam.tab_name == tab_name)
        {
          ((Component) mCategoryObject).gameObject.SetActive(true);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            list_on.Add(component);
        }
        else
        {
          ((Component) mCategoryObject).gameObject.SetActive(false);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            list_off.Add(component);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mScrollRect, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mScrollRect.verticalScrollbar, (UnityEngine.Object) null))
      {
        CanvasGroup component = ((Component) this.mScrollRect.verticalScrollbar).GetComponent<CanvasGroup>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          list_on.Add(component);
      }
      this.StartSwitchVisibleAnimation(list_on, list_off);
    }

    private void StartSwitchVisibleAnimation(List<CanvasGroup> list_on, List<CanvasGroup> list_off)
    {
      this.mToggleFadeAnimation = UIUtility.SwitchVisibleAsync(list_on, list_off);
      this.StartCoroutine(this.mToggleFadeAnimation);
    }

    private void StopSwitchVisibleAnimation()
    {
      if (this.mToggleFadeAnimation != null)
        this.StartCoroutine(this.mToggleFadeAnimation);
      this.mToggleFadeAnimation = (IEnumerator) null;
    }

    private bool SavePrefs()
    {
      SortUtility.SortRunePrefs prefs = SortUtility.Load_RuneSortFromCache();
      SortUtility.SortTempDiff tempDiffData = prefs.CreateTempDiffData();
      for (int index1 = 0; index1 < this.mCategoryObjects.Count; ++index1)
      {
        RuneSortToggleItem[] toggleValues = this.mCategoryObjects[index1].ToggleValues;
        for (int index2 = 0; index2 < toggleValues.Length; ++index2)
        {
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) toggleValues[index2], (UnityEngine.Object) null))
          {
            SortRuneConditionParam dataOfClass = DataSource.FindDataOfClass<SortRuneConditionParam>(((Component) toggleValues[index2]).gameObject, (SortRuneConditionParam) null);
            if (dataOfClass != null)
              prefs.SetValue(dataOfClass.parent.iname, dataOfClass.cnds_iname, toggleValues[index2].isOn);
          }
        }
      }
      prefs.SetAscending(this.mIsAscending);
      this.mIsBaseParamSort = UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mToggleBaseParamSort, (UnityEngine.Object) null) && this.mToggleBaseParamSort.isOn;
      this.mIsEvoParamSort = UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mToggleEvoParamSort, (UnityEngine.Object) null) && this.mToggleEvoParamSort.isOn;
      this.mIsSetParamSort = UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mToggleSetParamSort, (UnityEngine.Object) null) && this.mToggleSetParamSort.isOn;
      prefs.SetIsBaseParam(this.mIsBaseParamSort);
      prefs.SetIsEvoParam(this.mIsEvoParamSort);
      prefs.SetIsSetParam(this.mIsSetParamSort);
      SortUtility.Save_RuneSortForCache(prefs);
      return prefs.IsDiff(tempDiffData);
    }
  }
}
