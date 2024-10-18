// Decompiled with JetBrains decompiler
// Type: SRPG.RuneFilterWindow
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
  [FlowNode.Pin(1, "全選択", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "全選択解除", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "キャンセル", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "タブ切替", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(10, "Save完了", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "キャンセル完了", FlowNode.PinTypes.Output, 11)]
  public class RuneFilterWindow : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_SAVE = 0;
    public const int PIN_INPUT_ALL_SELECT = 1;
    public const int PIN_INPUT_ALL_CLEAR_SELECT = 2;
    public const int PIN_INPUT_CANCEL = 3;
    public const int PIN_INPUT_TAB_CHANGE = 4;
    public const int PIN_OUTPUT_SAVE_END = 10;
    public const int PIN_OUTPUT_CANCEL_END = 11;
    private const string SVB_KEY_CATEGORY_RARITY = "category_rarity";
    private const string SVB_KEY_CATEGORY_SETEFF = "category_seteff";
    private const string SVB_KEY_CATEGORY_EVOSTATUS = "category_evostatus";
    [SerializeField]
    private Transform mTabParent;
    [SerializeField]
    private GameObject mTabTemplate;
    [SerializeField]
    private ScrollRect mScrollRect;
    private FilterRunePrefs mCurrentFilterPrefs;
    private Dictionary<string, RuneFilterTab> mTabs = new Dictionary<string, RuneFilterTab>();
    private List<RuneFilterCategory> mCategoryObjects = new List<RuneFilterCategory>();
    private string mCurrentTabName;
    private IEnumerator mToggleFadeAnimation;
    private static RuneFilterWindow mInstance;

    public static RuneFilterWindow Instance => RuneFilterWindow.mInstance;

    public FilterRunePrefs CurrentFilterPrefs => this.mCurrentFilterPrefs;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, !this.SavePrefs() ? 11 : 10);
          break;
        case 1:
          this.OnSelectAll(true);
          break;
        case 2:
          this.OnSelectAll(false);
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

    private void Awake() => RuneFilterWindow.mInstance = this;

    private void Start() => this.Init();

    private void Init()
    {
      this.Init_Tab();
      this.Init_Category();
      this.SelectTab(this.mCurrentTabName);
      this.UpdateTabState();
    }

    private void Init_Tab()
    {
      GameUtility.SetGameObjectActive(this.mTabTemplate, false);
      foreach (FilterRuneParam filterRuneParam in MonoSingleton<GameManager>.Instance.MasterParam.FilterRuneParams)
      {
        if (!this.mTabs.ContainsKey(filterRuneParam.tab_name))
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mTabTemplate, this.mTabParent, false);
          RuneFilterTab componentInChildren1 = gameObject.GetComponentInChildren<RuneFilterTab>();
          gameObject.SetActive(true);
          this.mTabs.Add(filterRuneParam.tab_name, componentInChildren1);
          DataSource.Bind<FilterRuneParam>(gameObject, filterRuneParam);
          Text componentInChildren2 = gameObject.GetComponentInChildren<Text>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren2, (UnityEngine.Object) null))
            componentInChildren2.text = filterRuneParam.tab_name;
          if (string.IsNullOrEmpty(this.mCurrentTabName))
            this.mCurrentTabName = filterRuneParam.tab_name;
        }
      }
      if (this.mTabs.Count > 1 || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTabParent, (UnityEngine.Object) null))
        return;
      ((Component) this.mTabParent).gameObject.SetActive(false);
    }

    private void Init_Category()
    {
      this.mCurrentFilterPrefs = FilterRunePrefs.Load();
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      Dictionary<string, RuneFilterCategory> dictionary = new Dictionary<string, RuneFilterCategory>();
      dictionary.Add("category_rarity", component.list.GetComponent<RuneFilterCategory>("category_rarity"));
      dictionary.Add("category_seteff", component.list.GetComponent<RuneFilterCategory>("category_seteff"));
      dictionary.Add("category_evostatus", component.list.GetComponent<RuneFilterCategory>("category_evostatus"));
      RuneFilterCategory runeFilterCategory = (RuneFilterCategory) null;
      FilterRuneParam[] filterRuneParams = MonoSingleton<GameManager>.Instance.MasterParam.FilterRuneParams;
      for (int index = 0; index < filterRuneParams.Length; ++index)
      {
        if (filterRuneParams[index].IsEnableFilterType(eRuneFilterTypes.Rarity))
          runeFilterCategory = dictionary["category_rarity"];
        else if (filterRuneParams[index].IsEnableFilterType(eRuneFilterTypes.SetEff))
          runeFilterCategory = dictionary["category_seteff"];
        else if (filterRuneParams[index].IsEnableFilterType(eRuneFilterTypes.EvoStatue))
          runeFilterCategory = dictionary["category_evostatus"];
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) runeFilterCategory, (UnityEngine.Object) null))
        {
          runeFilterCategory.Init(filterRuneParams[index]);
          if (!this.mCategoryObjects.Contains(runeFilterCategory))
            this.mCategoryObjects.Add(runeFilterCategory);
        }
      }
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
        bool isFiltered = this.IsExistActiveFilterInTab(key);
        this.mTabs[key].SetIsFiltered(isFiltered);
      }
    }

    private bool IsExistActiveFilterInTab(string tab)
    {
      List<RuneFilterCategory> all = this.mCategoryObjects.FindAll((Predicate<RuneFilterCategory>) (category => category.FilterParam.tab_name == tab));
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
      foreach (RuneFilterCategory mCategoryObject in this.mCategoryObjects)
      {
        CanvasGroup component = ((Component) mCategoryObject).GetComponent<CanvasGroup>();
        if (mCategoryObject.FilterParam.tab_name == tab_name)
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
      FilterRunePrefs data = FilterRunePrefs.Load();
      List<bool> flagList = data.CreateFlagList();
      for (int index1 = 0; index1 < this.mCategoryObjects.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.mCategoryObjects[index1].Toggles.Count; ++index2)
        {
          Toggle toggle = this.mCategoryObjects[index1].Toggles[index2];
          FilterRuneConditionParam dataOfClass = DataSource.FindDataOfClass<FilterRuneConditionParam>(((Component) toggle).gameObject, (FilterRuneConditionParam) null);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) toggle, (UnityEngine.Object) null) && dataOfClass != null)
            data.SetValue(dataOfClass.parent.iname, dataOfClass.cnds_iname, toggle.isOn);
        }
      }
      FilterRunePrefs.Save(data);
      return data.IsDiff(flagList);
    }

    private void OnSelectAll(bool is_on)
    {
      for (int index1 = 0; index1 < this.mCategoryObjects.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.mCategoryObjects[index1].Toggles.Count; ++index2)
          this.mCategoryObjects[index1].Toggles[index2].isOn = is_on;
      }
      this.UpdateTabState();
    }
  }
}
