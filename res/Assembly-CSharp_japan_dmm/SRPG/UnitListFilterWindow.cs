// Decompiled with JetBrains decompiler
// Type: SRPG.UnitListFilterWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class UnitListFilterWindow : FlowWindowBase
  {
    private const string SAVEKEY = "FilterUnit";
    private const string SAVEKEY_OLD = "UNITLIST";
    private const string SVB_KEY_SCROLL_RECT = "scroll_rect";
    private const string SVB_KEY_TAB_PARENT = "tab_btn_parent";
    private const string SVB_KEY_TAB_TEMPLATE = "tab_btn_template";
    private const string SVB_KEY_CATEGORY_RARITY = "category_rarity";
    private const string SVB_KEY_CATEGORY_JOB = "category_job";
    private const string SVB_KEY_CATEGORY_BIRTH = "category_birth";
    private const string SVB_KEY_CATEGORY_SEX = "category_sex";
    private const string SVB_KEY_CATEGORY_UNIT = "category_unit";
    private const string SVB_KEY_CATEGORY_ARTIFACT = "category_artifact";
    private UnitListFilterWindow.SerializeParam m_Param;
    private SerializeValueList m_ValueList;
    private UnitListWindow m_Root;
    private SerializeValueBehaviour m_TargetPrefabSVB;
    private UnitListFilterWindow.Result m_Result;
    private Dictionary<string, UnitListFilterWindow.FilterValue> m_SelectType = new Dictionary<string, UnitListFilterWindow.FilterValue>();
    private Dictionary<string, UnitListFilterWindow.FilterValue> m_SelectTypeReg = new Dictionary<string, UnitListFilterWindow.FilterValue>();
    private Dictionary<string, Toggle> m_Toggles = new Dictionary<string, Toggle>();
    private Dictionary<string, FilterUnitTab> m_Tabs = new Dictionary<string, FilterUnitTab>();
    private Dictionary<string, GameObject> m_ToggleCategory = new Dictionary<string, GameObject>();
    private string m_CurrentTab;
    private string m_FirstTab;
    private IEnumerator m_ToggleFadeAnimation;
    private ScrollRect m_ScrollRect;

    public override string name => nameof (UnitListFilterWindow);

    public override void Initialize(FlowWindowBase.SerializeParamBase param)
    {
      base.Initialize(param);
      this.m_Param = param as UnitListFilterWindow.SerializeParam;
      if (this.m_Param == null)
        throw new Exception(this.ToString() + " > Failed serializeParam null.");
      SerializeValueBehaviour childComponent = this.GetChildComponent<SerializeValueBehaviour>("filter");
      this.m_ValueList = !UnityEngine.Object.op_Inequality((UnityEngine.Object) childComponent, (UnityEngine.Object) null) ? new SerializeValueList() : childComponent.list;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Window, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Animator, (UnityEngine.Object) null))
        this.CacheToggleParam(((Component) this.m_Animator).gameObject);
      this.LoadSelectType();
      this.Close(true);
    }

    private void CreateInstance()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Window, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Animator, (UnityEngine.Object) null))
        return;
      string name = this.m_ValueList.GetString("path");
      if (string.IsNullOrEmpty(name))
        return;
      GameObject gameObject = AssetManager.Load<GameObject>(name);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      GameObject toggle_parent_obj = (GameObject) null;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_Animator, (UnityEngine.Object) null))
      {
        toggle_parent_obj = UnityEngine.Object.Instantiate<GameObject>(gameObject);
        toggle_parent_obj.transform.SetParent(this.m_Window.transform, false);
        this.m_Animator = toggle_parent_obj.GetComponent<Animator>();
        this.m_TargetPrefabSVB = toggle_parent_obj.GetComponent<SerializeValueBehaviour>();
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_TargetPrefabSVB, (UnityEngine.Object) null) || this.m_TargetPrefabSVB.list == null)
      {
        DebugUtility.LogError("「SerializeValueBehaviour」の取得に失敗");
      }
      else
      {
        this.m_ScrollRect = this.m_TargetPrefabSVB.list.GetComponent<ScrollRect>("scroll_rect");
        this.CacheTabParam();
        this.CacheToggleParam(toggle_parent_obj);
        this.UpdateTabState();
      }
    }

    private void CacheTabParam()
    {
      SerializeValueList list = this.m_TargetPrefabSVB.list;
      GameObject gameObject1 = list.GetGameObject("tab_btn_parent");
      GameObject gameObject2 = list.GetGameObject("tab_btn_template");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        DebugUtility.LogError(string.Format("「{0}」の SerializeValueList に「{1}」が宣言されていません。", (object) ((UnityEngine.Object) this.m_TargetPrefabSVB).name, (object) "tab_btn_parent"));
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
      {
        DebugUtility.LogError(string.Format("「{0}」の SerializeValueList に「{1}」が宣言されていません。", (object) ((UnityEngine.Object) this.m_TargetPrefabSVB).name, (object) "tab_btn_template"));
      }
      else
      {
        GameUtility.SetGameObjectActive(gameObject2, false);
        foreach (FilterUnitParam filterUnitParam in MonoSingleton<GameManager>.Instance.MasterParam.FilterUnitParams)
        {
          if (!this.m_Tabs.ContainsKey(filterUnitParam.tab_name))
          {
            GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject2, gameObject1.transform, false);
            FilterUnitTab componentInChildren1 = gameObject3.GetComponentInChildren<FilterUnitTab>();
            gameObject3.SetActive(true);
            DataSource.Bind<FilterUnitParam>(gameObject3, filterUnitParam);
            Text componentInChildren2 = gameObject3.GetComponentInChildren<Text>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren2, (UnityEngine.Object) null))
              componentInChildren2.text = filterUnitParam.tab_name;
            this.m_Tabs.Add(filterUnitParam.tab_name, componentInChildren1);
            if (string.IsNullOrEmpty(this.m_FirstTab))
              this.m_FirstTab = filterUnitParam.tab_name;
          }
        }
        this.m_CurrentTab = this.m_FirstTab;
      }
    }

    private void CacheToggleParam(GameObject toggle_parent_obj)
    {
      SerializeValueList list = this.m_TargetPrefabSVB.list;
      Dictionary<string, FilterUnitCategory> dictionary = new Dictionary<string, FilterUnitCategory>();
      dictionary.Add("category_rarity", list.GetComponent<FilterUnitCategory>("category_rarity"));
      dictionary.Add("category_job", list.GetComponent<FilterUnitCategory>("category_job"));
      dictionary.Add("category_birth", list.GetComponent<FilterUnitCategory>("category_birth"));
      dictionary.Add("category_sex", list.GetComponent<FilterUnitCategory>("category_sex"));
      dictionary.Add("category_unit", list.GetComponent<FilterUnitCategory>("category_unit"));
      dictionary.Add("category_artifact", list.GetComponent<FilterUnitCategory>("category_artifact"));
      foreach (KeyValuePair<string, FilterUnitCategory> keyValuePair in dictionary)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) keyValuePair.Value, (UnityEngine.Object) null))
        {
          DebugUtility.LogError(string.Format("「{0}」の SerializeValueList に「{1}」が宣言されていません。", (object) ((UnityEngine.Object) toggle_parent_obj).name, (object) keyValuePair.Key));
          return;
        }
        ((Component) keyValuePair.Value).gameObject.SetActive(false);
      }
      int num = 0;
      foreach (FilterUnitParam filterUnitParam in MonoSingleton<GameManager>.Instance.MasterParam.FilterUnitParams)
      {
        FilterUnitCategory filterUnitCategory;
        if (filterUnitParam.IsEnableFilterType(eFilterUnitTypes.RarityIni))
          filterUnitCategory = dictionary["category_rarity"];
        else if (filterUnitParam.IsEnableFilterType(eFilterUnitTypes.JobGroup))
          filterUnitCategory = dictionary["category_job"];
        else if (filterUnitParam.IsEnableFilterType(eFilterUnitTypes.Birth))
          filterUnitCategory = dictionary["category_birth"];
        else if (filterUnitParam.IsEnableFilterType(eFilterUnitTypes.Sex))
          filterUnitCategory = dictionary["category_sex"];
        else if (filterUnitParam.IsEnableFilterType(eFilterUnitTypes.UnitGroup))
          filterUnitCategory = dictionary["category_unit"];
        else if (filterUnitParam.IsEnableFilterType(eFilterUnitTypes.DefArtifact))
        {
          filterUnitCategory = dictionary["category_artifact"];
        }
        else
        {
          DebugUtility.LogError(string.Format("FilterUnit「{0}」にフィルタの条件が1つも設定されていません。何か1つでも設定してください。", (object) filterUnitParam.iname));
          continue;
        }
        if (!this.m_ToggleCategory.ContainsKey(filterUnitParam.iname))
        {
          filterUnitCategory.SetHeaderText(filterUnitParam.name);
          ((Component) filterUnitCategory).gameObject.SetActive(true);
          this.m_ToggleCategory.Add(filterUnitParam.iname, ((Component) filterUnitCategory).gameObject);
          ((Transform) (((Component) filterUnitCategory).transform as RectTransform)).SetSiblingIndex(num++);
        }
        for (int index = 0; index < filterUnitParam.conditions.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          UnitListFilterWindow.\u003CCacheToggleParam\u003Ec__AnonStorey0 paramCAnonStorey0 = new UnitListFilterWindow.\u003CCacheToggleParam\u003Ec__AnonStorey0();
          // ISSUE: reference to a compiler-generated field
          paramCAnonStorey0.\u0024this = this;
          FilterUnitConditionParam condition = filterUnitParam.conditions[index];
          // ISSUE: reference to a compiler-generated field
          paramCAnonStorey0.toggle = filterUnitCategory.CreateFilterButton(condition);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ((UnityEvent<bool>) paramCAnonStorey0.toggle.onValueChanged).AddListener(new UnityAction<bool>((object) paramCAnonStorey0, __methodptr(\u003C\u003Em__0)));
          // ISSUE: reference to a compiler-generated field
          this.m_Toggles.Add(condition.PrefsKey, paramCAnonStorey0.toggle);
        }
      }
    }

    public override void Release() => base.Release();

    public override int Update()
    {
      base.Update();
      if (this.m_Result != UnitListFilterWindow.Result.NONE)
      {
        if (this.isClosed)
          this.SetActiveChild(false);
      }
      else if (this.isClosed)
        this.SetActiveChild(false);
      return -1;
    }

    private void InitializeToggleContent()
    {
      foreach (KeyValuePair<string, Toggle> toggle in this.m_Toggles)
      {
        UnitListFilterWindow.FilterValue filterValue = (UnitListFilterWindow.FilterValue) null;
        this.m_SelectType.TryGetValue(toggle.Key, out filterValue);
        GameUtility.SetToggle(toggle.Value, filterValue.Value);
      }
      UnitListFilterWindow.SelectCopyTo(this.m_SelectType, ref this.m_SelectTypeReg);
    }

    private void ReleaseToggleContent()
    {
    }

    private void SwitchToggleActiveWithTabName(string tab_name)
    {
      this.StopSwitchVisibleAnimation();
      List<FilterUnitParam> list = ((IEnumerable<FilterUnitParam>) MonoSingleton<GameManager>.Instance.MasterParam.FilterUnitParams).Where<FilterUnitParam>((Func<FilterUnitParam, bool>) (param => param.tab_name == tab_name)).ToList<FilterUnitParam>();
      List<CanvasGroup> list_off = new List<CanvasGroup>();
      List<CanvasGroup> list_on = new List<CanvasGroup>();
      foreach (KeyValuePair<string, GameObject> keyValuePair in this.m_ToggleCategory)
      {
        KeyValuePair<string, GameObject> toggle_category = keyValuePair;
        if (list.Find((Predicate<FilterUnitParam>) (param => param.iname == toggle_category.Key)) == null)
        {
          toggle_category.Value.SetActive(false);
          CanvasGroup component = toggle_category.Value.GetComponent<CanvasGroup>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            list_off.Add(component);
          else
            DebugUtility.LogError(string.Format("toggle_category 「{0}」に CanvasGroup コンポーネントがアタッチされていません。", (object) ((UnityEngine.Object) toggle_category.Value).name));
        }
        else
        {
          toggle_category.Value.SetActive(true);
          CanvasGroup component = toggle_category.Value.GetComponent<CanvasGroup>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            list_on.Add(component);
          else
            DebugUtility.LogError(string.Format("toggle_category 「{0}」に CanvasGroup コンポーネントがアタッチされていません。", (object) ((UnityEngine.Object) toggle_category.Value).name));
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ScrollRect, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ScrollRect.verticalScrollbar, (UnityEngine.Object) null))
      {
        CanvasGroup component = ((Component) this.m_ScrollRect.verticalScrollbar).GetComponent<CanvasGroup>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          list_on.Add(component);
        else
          DebugUtility.LogError("m_ScrollRect.verticalScrollbar に CanvasGroup コンポーネントがアタッチされていません。");
      }
      this.StartSwitchVisibleAnimation(list_on, list_off);
    }

    private void StartSwitchVisibleAnimation(List<CanvasGroup> list_on, List<CanvasGroup> list_off)
    {
      this.m_ToggleFadeAnimation = UIUtility.SwitchVisibleAsync(list_on, list_off);
      this.m_TargetPrefabSVB.StartCoroutine(this.m_ToggleFadeAnimation);
    }

    private void StopSwitchVisibleAnimation()
    {
      if (this.m_ToggleFadeAnimation != null)
        this.m_TargetPrefabSVB.StopCoroutine(this.m_ToggleFadeAnimation);
      this.m_ToggleFadeAnimation = (IEnumerator) null;
    }

    public void SetRoot(UnitListWindow root) => this.m_Root = root;

    private void SetSelect(string selectType)
    {
      if (!this.m_SelectType.ContainsKey(selectType))
        return;
      this.m_SelectType[selectType].Value = true;
    }

    private void ResetSelect(string selectType)
    {
      if (!this.m_SelectType.ContainsKey(selectType))
        return;
      this.m_SelectType[selectType].Value = false;
    }

    public List<string> GetSelect()
    {
      return this.m_SelectType.Where<KeyValuePair<string, UnitListFilterWindow.FilterValue>>((Func<KeyValuePair<string, UnitListFilterWindow.FilterValue>, bool>) (pair => pair.Value.Value)).Select<KeyValuePair<string, UnitListFilterWindow.FilterValue>, string>((Func<KeyValuePair<string, UnitListFilterWindow.FilterValue>, string>) (pair => pair.Key)).ToList<string>();
    }

    public List<string> GetSelectReg()
    {
      return this.m_SelectTypeReg.Where<KeyValuePair<string, UnitListFilterWindow.FilterValue>>((Func<KeyValuePair<string, UnitListFilterWindow.FilterValue>, bool>) (pair => pair.Value.Value)).Select<KeyValuePair<string, UnitListFilterWindow.FilterValue>, string>((Func<KeyValuePair<string, UnitListFilterWindow.FilterValue>, string>) (pair => pair.Key)).ToList<string>();
    }

    private static void SelectCopyTo(
      Dictionary<string, UnitListFilterWindow.FilterValue> src,
      ref Dictionary<string, UnitListFilterWindow.FilterValue> dst)
    {
      dst = new Dictionary<string, UnitListFilterWindow.FilterValue>(src.Count);
      foreach (KeyValuePair<string, UnitListFilterWindow.FilterValue> keyValuePair in src)
        dst.Add(keyValuePair.Key, keyValuePair.Value.Clone());
    }

    public void CalcUnit(List<UnitListWindow.Data> list, FilterUtility.FilterPrefs filter)
    {
      List<UnitParam> units = list.Where<UnitListWindow.Data>((Func<UnitListWindow.Data, bool>) (data => data.unit != null)).Select<UnitListWindow.Data, UnitParam>((Func<UnitListWindow.Data, UnitParam>) (data => data.unit == null ? (UnitParam) null : data.unit.UnitParam)).ToList<UnitParam>();
      FilterUtility.FilterUnit(ref units, filter);
      list.RemoveAll((Predicate<UnitListWindow.Data>) (data => data.unit != null && !units.Contains(data.unit.UnitParam)));
    }

    private void SelectTab(string tabName)
    {
      this.m_CurrentTab = tabName;
      this.m_Tabs[this.m_CurrentTab].isOn = true;
      this.SwitchToggleActiveWithTabName(tabName);
      this.ResetScrollPos();
    }

    private void UpdateTabState()
    {
      foreach (KeyValuePair<string, FilterUnitTab> tab in this.m_Tabs)
      {
        bool isFiltered = this.ContainsActiveFilterInTab(tab.Key);
        tab.Value.SetIsFiltered(isFiltered);
      }
    }

    private bool ContainsActiveFilterInTab(string tab)
    {
      foreach (FilterUnitParam filterUnitParam in MonoSingleton<GameManager>.Instance.MasterParam.FilterUnitParams)
      {
        if (!(filterUnitParam.tab_name != tab))
        {
          foreach (FilterUnitConditionParam condition in filterUnitParam.conditions)
          {
            if (this.m_Toggles.ContainsKey(condition.PrefsKey) && this.m_Toggles[condition.PrefsKey].isOn)
              return true;
          }
        }
      }
      return false;
    }

    public void LoadSelectType()
    {
      this.m_SelectType = new Dictionary<string, UnitListFilterWindow.FilterValue>();
      FilterUtility.FilterPrefs filterPrefs = FilterUtility.Load_UnitFilter();
      if (filterPrefs == null)
        return;
      foreach (FilterUnitParam filterUnitParam in MonoSingleton<GameManager>.Instance.MasterParam.FilterUnitParams)
      {
        for (int index = 0; index < filterUnitParam.conditions.Length; ++index)
        {
          FilterUnitConditionParam condition = filterUnitParam.conditions[index];
          bool flag = filterPrefs.GetValue(filterUnitParam.iname, condition.cnds_iname);
          UnitListFilterWindow.FilterValue filterValue = new UnitListFilterWindow.FilterValue(filterUnitParam.iname, condition.cnds_iname, flag);
          this.m_SelectType.Add(condition.PrefsKey, filterValue);
        }
      }
      UnitListFilterWindow.SelectCopyTo(this.m_SelectType, ref this.m_SelectTypeReg);
    }

    public void SaveSelectType()
    {
      FilterUtility.FilterPrefs filterPrefs = FilterUtility.Load_UnitFilter();
      foreach (KeyValuePair<string, UnitListFilterWindow.FilterValue> keyValuePair in this.m_SelectType)
        filterPrefs.SetValue(keyValuePair.Value.MajorKey, keyValuePair.Value.Minorkey, keyValuePair.Value.Value);
      FilterUtility.FilterPrefs.Save(filterPrefs, true);
      foreach (KeyValuePair<string, UnitListFilterWindow.FilterValue> keyValuePair in this.m_SelectType)
        keyValuePair.Value.Value = filterPrefs.GetValue(keyValuePair.Value.MajorKey, keyValuePair.Value.Minorkey);
    }

    public override int OnActivate(int pinId)
    {
      switch (pinId)
      {
        case 600:
          this.CreateInstance();
          this.LoadSelectType();
          this.InitializeToggleContent();
          this.UpdateTabState();
          this.Open();
          this.SelectTab(this.m_FirstTab);
          break;
        case 610:
          this.ReleaseToggleContent();
          UnitListFilterWindow.SelectCopyTo(this.m_SelectTypeReg, ref this.m_SelectType);
          this.m_Result = UnitListFilterWindow.Result.CANCEL;
          this.Close();
          return 691;
        case 620:
          this.SaveSelectType();
          this.ReleaseToggleContent();
          UnitListFilterWindow.SelectCopyTo(this.m_SelectType, ref this.m_SelectTypeReg);
          this.m_Result = UnitListFilterWindow.Result.CONFIRM;
          this.Close();
          return 690;
        case 640:
          foreach (KeyValuePair<string, Toggle> toggle in this.m_Toggles)
          {
            this.SetSelect(toggle.Key);
            GameUtility.SetToggle(toggle.Value, true);
          }
          this.UpdateTabState();
          break;
        case 650:
          foreach (KeyValuePair<string, Toggle> toggle in this.m_Toggles)
          {
            this.ResetSelect(toggle.Key);
            GameUtility.SetToggle(toggle.Value, false);
          }
          this.UpdateTabState();
          break;
        case 660:
          if (FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue)
          {
            FilterUnitParam dataSource = currentValue.GetDataSource<FilterUnitParam>("_self", (FilterUnitParam) null);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) currentValue.GetUIToggle("_self"), (UnityEngine.Object) null) && dataSource != null && this.m_CurrentTab != dataSource.tab_name)
            {
              this.SelectTab(dataSource.tab_name);
              break;
            }
            break;
          }
          break;
      }
      return -1;
    }

    private void OnFilterValueChanged(Toggle toggle, bool value)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) toggle, (UnityEngine.Object) null))
        return;
      FilterUnitConditionParam dataOfClass = DataSource.FindDataOfClass<FilterUnitConditionParam>(((Component) toggle).gameObject, (FilterUnitConditionParam) null);
      if (dataOfClass == null)
        return;
      if (value)
        this.SetSelect(dataOfClass.PrefsKey);
      else
        this.ResetSelect(dataOfClass.PrefsKey);
      this.UpdateTabState();
    }

    private void ResetScrollPos()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ScrollRect, (UnityEngine.Object) null))
        return;
      this.m_ScrollRect.velocity = Vector2.zero;
      this.m_ScrollRect.normalizedPosition = Vector2.up;
    }

    public enum Result
    {
      NONE,
      CONFIRM,
      CANCEL,
    }

    private class FilterValue
    {
      private string m_Majorkey;
      private string m_Minorkey;
      private bool m_Value;

      public FilterValue(string majorKey, string minorKey, bool value)
      {
        this.m_Majorkey = majorKey;
        this.m_Minorkey = minorKey;
        this.m_Value = value;
      }

      public string MajorKey => this.m_Majorkey;

      public string Minorkey => this.m_Minorkey;

      public bool Value
      {
        get => this.m_Value;
        set => this.m_Value = value;
      }

      public UnitListFilterWindow.FilterValue Clone()
      {
        return new UnitListFilterWindow.FilterValue(this.m_Majorkey, this.m_Minorkey, this.m_Value);
      }
    }

    [Serializable]
    public class SerializeParam : FlowWindowBase.SerializeParamBase
    {
      public override System.Type type => typeof (UnitListFilterWindow);
    }
  }
}
